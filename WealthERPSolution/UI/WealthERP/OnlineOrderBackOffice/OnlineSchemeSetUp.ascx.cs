using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using VoUser;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCustomerPortfolio;
using System.Globalization;
using BoCustomerProfiling;
using WealthERP.Base;
using BoProductMaster;
using BoWerpAdmin;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;


namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineSchemeSetUp : System.Web.UI.UserControl
    {
        ProductMFBo productMFBo = new ProductMFBo();
        PriceBo priceBo = new PriceBo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo = new OnlineOrderBackOfficeVo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        int schemeId = 0;
        int DetailsId = 0;
        string categoryCode;
        string subcategoryCode;
        int schemeplancode = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            //BindAMC();
            //BindCategory();
            ////BindSubCategory();
            ////BindSubSubCategory();
            //BindBankName();
            if (!IsPostBack)
            {
                BindAMC();
                BindCategory();
                ////BindSubCategory();
                ////BindSubSubCategory();
                BindBankName();
                BindFrequency();
                if (Request.QueryString["SchemePlanCode"] != null)
                {
                    schemeplancode = int.Parse(Request.QueryString["SchemePlanCode"].ToString());

                }
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                {
                    if (Request.QueryString["strAction"].Trim() == "Edit")
                    {

                        EditSchemeDetails();
                        lbBack.Visible = true;
                        ControlViewEditMode(false);

                    }
                    else if (Request.QueryString["strAction"].Trim() == "View")
                    {
                        ViewSchemeDetails();
                        lbBack.Visible = true;
                        ControlViewEditMode(true);

                    }
                }
            }
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlAmc.SelectedValue.ToString());
                if (ddlcategory.SelectedIndex != 0)
                {
                    LoadAllSchemeList(amcCode);
                }
            }
            if (ddlcategory.SelectedIndex != 0)
                categoryCode = ddlcategory.SelectedValue;
            ViewState["Category"] = categoryCode;

            BindSubCategory(categoryCode);

        }
        protected void ddlScategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlScategory.SelectedIndex != 0)
                subcategoryCode = ddlScategory.SelectedValue;
            categoryCode = Convert.ToString(ViewState["Category"]);
            BindSubSubCategory(categoryCode, subcategoryCode);
        }
        private void BindAMC()
        {
            //ddlAmc.Items.Clear();
            try
            {
                PriceBo priceBo = new PriceBo();
                DataTable dtGetAMCList = new DataTable();
                dtGetAMCList = priceBo.GetMutualFundList();
                ddlAmc.DataSource = dtGetAMCList;
                ddlAmc.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
                ddlAmc.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataBind();
                ddlAmc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindAmcDropDown()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindCategory()
        {
            //ddlcategory.Items.Clear();
            try
            {
                DataSet dsProductAssetCategory;
                dsProductAssetCategory = productMFBo.GetProductAssetCategory();
                DataTable dtCategory = dsProductAssetCategory.Tables[0];
                if (dtCategory != null)
                {
                    ddlcategory.DataSource = dtCategory;
                    ddlcategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlcategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlcategory.DataBind();
                }
                ddlcategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindCategoryDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindSubCategory(string CategoryCode)
        {
            ddlScategory.Items.Clear();
            DataSet dsBindSubCategory;
            DataTable dtBindSubCategory;
            try
            {
                dsBindSubCategory = OnlineOrderBackOfficeBo.GetSubCategory(CategoryCode);
                if (dsBindSubCategory.Tables[0].Rows.Count > 0)
                {
                    dtBindSubCategory = dsBindSubCategory.Tables[0];
                    ddlScategory.DataSource = dtBindSubCategory;
                    ddlScategory.DataTextField = dtBindSubCategory.Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
                    ddlScategory.DataValueField = dtBindSubCategory.Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    ddlScategory.DataBind();
                }
                ddlScategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindSubCategory()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindSubSubCategory(string CategoryCode, string SubCategoryCode)
        {
            ddlSScategory.Items.Clear();
            DataSet dsBindSubSubCategory;
            DataTable dtBindSubSubCategory;
            try
            {
                dsBindSubSubCategory = OnlineOrderBackOfficeBo.GetSubSubCategory(CategoryCode, SubCategoryCode);
                if (dsBindSubSubCategory.Tables[0].Rows.Count > 0)
                {
                    dtBindSubSubCategory = dsBindSubSubCategory.Tables[0];
                    ddlSScategory.DataSource = dtBindSubSubCategory;
                    ddlSScategory.DataTextField = dtBindSubSubCategory.Columns["PAISSC_AssetInstrumentSubSubCategoryName"].ToString();
                    ddlSScategory.DataValueField = dtBindSubSubCategory.Columns["PAISSC_AssetInstrumentSubSubCategoryCode"].ToString();
                    ddlSScategory.DataBind();
                }
                ddlSScategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:BindSubSubCategory()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public void BindBankName()
        {
            DataTable dtBankName = new DataTable();
            dtBankName = customerBankAccountBo.GetALLBankName();
            ddlBname.DataSource = dtBankName;
            ddlBname.DataValueField = dtBankName.Columns["WERPBM_BankCode"].ToString();
            ddlBname.DataTextField = dtBankName.Columns["WERPBM_BankName"].ToString();
            ddlBname.DataBind();
            ddlBname.Items.Insert(0, new ListItem("Select", "Select"));
        }
        public void BindFrequency()
        {
            DataTable dtFrequency = new DataTable();
            DataSet dsFrequency = new DataSet();
            dsFrequency = OnlineOrderBackOfficeBo.GetFrequency();
            dtFrequency = dsFrequency.Tables[0];
            ddlGenerationfreq.DataSource = dtFrequency;
            ddlGenerationfreq.DataValueField = dtFrequency.Columns["XF_FrequencyCode"].ToString();
            ddlGenerationfreq.DataTextField = dtFrequency.Columns["XF_Frequency"].ToString();
            ddlGenerationfreq.DataBind();
            ddlGenerationfreq.Items.Insert(0, new ListItem("Select", "Select"));
        }
        protected void ddlAmcCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex != 0)
            {
                int amcCode = int.Parse(ddlAmc.SelectedValue);
                if (ddlAmc.SelectedIndex != 0)
                {
                    LoadAllSchemeList(amcCode);
                }
                else
                    ddlAmc.SelectedItem.Text = "";
            }
        }
        private void LoadAllSchemeList(int amcCode)
        {
            DataSet dsLoadAllScheme = new DataSet();
            DataTable dtLoadAllScheme = new DataTable();
            if (ddlAmc.SelectedIndex != 0 && ddlcategory.SelectedIndex == 0)
            {
                amcCode = int.Parse(ddlAmc.SelectedValue.ToString());
                categoryCode = ddlcategory.SelectedValue;
                dsLoadAllScheme = OnlineOrderBackOfficeBo.GetSchemeSetUpFromOverAllCategoryList(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }
            if (ddlAmc.SelectedIndex != 0 && ddlcategory.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlAmc.SelectedValue.ToString());
                categoryCode = ddlcategory.SelectedValue;
                dsLoadAllScheme = OnlineOrderBackOfficeBo.GetSchemeSetUpFromOverAllCategoryList(amcCode, categoryCode);
                dtLoadAllScheme = dsLoadAllScheme.Tables[0];
            }
            if (dtLoadAllScheme.Rows.Count > 0)
            {
                ddlSchemeList.DataSource = dtLoadAllScheme;
                ddlSchemeList.DataTextField = dtLoadAllScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlSchemeList.DataValueField = dtLoadAllScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlSchemeList.Items.Clear();
                ddlSchemeList.DataSource = null;
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
            }

        }
        protected void SaveSchemeDetails()
        {
            OnlineOrderBackOfficeVo.Product = ddlProduct.SelectedValue;
            if (ChkNRI.Checked)
            {
                OnlineOrderBackOfficeVo.CustomerSubTypeCode = "IND";

            }
            if (ChkBO.Checked)
            {
                OnlineOrderBackOfficeVo.CustomerSubTypeCode = "NIND";

            }
            if (chkonline.Checked)
            {
                OnlineOrderBackOfficeVo.IsOnline = 1;

            }
            else
            {
                OnlineOrderBackOfficeVo.IsOnline = 0;
            }
            OnlineOrderBackOfficeVo.SchemePlanCode = int.Parse(ddlSchemeList.SelectedValue);
            OnlineOrderBackOfficeVo.SchemePlanName = ddlSchemeList.SelectedItem.Text;
            OnlineOrderBackOfficeVo.ExternalCode = txtESSchemecode.Text.ToString();
            OnlineOrderBackOfficeVo.AMCCode = int.Parse(ddlAmc.SelectedValue);
            OnlineOrderBackOfficeVo.AssetCategoryCode = ddlcategory.SelectedValue;
            if (!string.IsNullOrEmpty(txtFvale.Text))
            {
                OnlineOrderBackOfficeVo.FaceValue = Convert.ToDouble(txtFvale.Text);
            }
            OnlineOrderBackOfficeVo.AssetSubCategoryCode = ddlScategory.SelectedValue;
            OnlineOrderBackOfficeVo.AssetSubSubCategory = ddlSScategory.SelectedValue;
            OnlineOrderBackOfficeVo.SchemeType = ddlSctype.SelectedValue;
            OnlineOrderBackOfficeVo.SchemeOption = ddlOption.SelectedValue;
            OnlineOrderBackOfficeVo.BankName = ddlBname.SelectedItem.ToString();
            OnlineOrderBackOfficeVo.WERPBM_BankCode = ddlBname.SelectedValue;
            OnlineOrderBackOfficeVo.Branch = txtBranch.Text;
            OnlineOrderBackOfficeVo.AccountNumber = txtACno.Text;
            OnlineOrderBackOfficeVo.DividendFrequency = ddlDFrequency.SelectedValue;
            OnlineOrderBackOfficeVo.GenerationFrequency = ddlGenerationfreq.SelectedValue;
            if (chkInfo.Checked)
            {
                OnlineOrderBackOfficeVo.IsNFO = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsNFO = 0;
            }
            if (txtNFOStartDate.SelectedDate.ToString() != null && txtNFOStartDate.SelectedDate.ToString() != string.Empty)
            {
                OnlineOrderBackOfficeVo.NFOStartDate = DateTime.Parse(txtNFOStartDate.SelectedDate.ToString());
            }
            if (txtNFOendDate.SelectedDate.ToString() != null && txtNFOendDate.SelectedDate.ToString() != string.Empty)
            {
                OnlineOrderBackOfficeVo.NFOEndDate = DateTime.Parse(txtNFOendDate.SelectedDate.ToString());
            }
            if (!string.IsNullOrEmpty(txtLIperiod.Text))
            {
                OnlineOrderBackOfficeVo.LockInPeriod = int.Parse(txtLIperiod.Text.ToString());
            }
            //DateTime time1 = Convert.ToDateTime(txtHH.Text);
            //DateTime time2 =Convert.ToDateTime(txtMM.Text);  
            //DateTime time3 =Convert.ToDateTime(txtSS.Text);

            if (!string.IsNullOrEmpty(txtHH.Text) && (!string.IsNullOrEmpty(txtMM.Text)) && (!string.IsNullOrEmpty(txtSS.Text)))
            {
                string Time = (txtHH.Text + ':' + txtMM.Text + ':' + txtSS.Text);
                if (Time != null || Time == "")
                {
                    TimeSpan CutOff = TimeSpan.Parse(Time);
                    OnlineOrderBackOfficeVo.CutOffTime = (CutOff);
                }
                else
                {
                    TimeSpan CutOff = TimeSpan.Parse("00:00:00");
                    OnlineOrderBackOfficeVo.CutOffTime = (CutOff);
                }
            }
            if (!string.IsNullOrEmpty(txtEload.Text))
            {
                OnlineOrderBackOfficeVo.EntryLoadPercentag = Convert.ToDouble(txtEload.Text);
            }
            OnlineOrderBackOfficeVo.EntryLoadRemark = txtELremark.Text;

            {
                if (!string.IsNullOrEmpty(txtExitLoad.Text))
                    OnlineOrderBackOfficeVo.ExitLoadPercentage = Convert.ToDouble(txtExitLoad.Text);
            }
            OnlineOrderBackOfficeVo.ExitLoadRemark = txtExitLremark.Text;
            if (ChkISPurchage.Checked)
            {
                OnlineOrderBackOfficeVo.IsPurchaseAvailable = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsPurchaseAvailable = 0;
            }
            if (ChkISRedeem.Checked)
            {
                OnlineOrderBackOfficeVo.IsRedeemAvailable = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsRedeemAvailable = 0;
            }
            if (ChkISSIP.Checked)
            {
                OnlineOrderBackOfficeVo.IsSIPAvailable = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsSIPAvailable = 0;
            }
            if (ChkISSTP.Checked)
            {
                OnlineOrderBackOfficeVo.IsSTPAvailable = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsSTPAvailable = 0;
            }
            if (ChkISSwitch.Checked)
            {
                OnlineOrderBackOfficeVo.IsSwitchAvailable = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsSwitchAvailable = 0;
            }
            if (ChkISSWP.Checked)
            {
                OnlineOrderBackOfficeVo.IsSWPAvailable = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsSWPAvailable = 0;
            }
            if (ChkISactive.Checked)
            {
                OnlineOrderBackOfficeVo.Status = "Active";
            }
            else
            {
                OnlineOrderBackOfficeVo.Status = "Liquidated";
            }
            if (!string.IsNullOrEmpty(txtInitalPamount.Text))
            {
                OnlineOrderBackOfficeVo.InitialPurchaseAmount = Convert.ToDouble(txtInitalPamount.Text);
            }
            if (!string.IsNullOrEmpty(txtIMultipleamount.Text))
            {
                OnlineOrderBackOfficeVo.InitialMultipleAmount = Convert.ToDouble(txtIMultipleamount.Text);
            }
            if (!string.IsNullOrEmpty(txtAdditional.Text))
            {
                OnlineOrderBackOfficeVo.AdditionalPruchaseAmount = Convert.ToDouble(txtAdditional.Text);
            }
            if (!string.IsNullOrEmpty(txtAddMultipleamount.Text))
            {
                OnlineOrderBackOfficeVo.AdditionalMultipleAmount = Convert.ToDouble(txtAddMultipleamount.Text);
            }
            if (!string.IsNullOrEmpty(txtMinRedemption.Text))
            {
                OnlineOrderBackOfficeVo.MinRedemptionAmount = Convert.ToDouble(txtMinRedemption.Text);
            }
            if (!string.IsNullOrEmpty(txtMinRedemptioUnits.Text))
            {
                OnlineOrderBackOfficeVo.MinRedemptionUnits = Convert.ToInt32(txtMinRedemptioUnits.Text);
            }
            if (!string.IsNullOrEmpty(txtMinSwitchAmount.Text))
            {
                OnlineOrderBackOfficeVo.MinSwitchAmount = Convert.ToDouble(txtMinSwitchAmount.Text);
            }
            if (!string.IsNullOrEmpty(txtMinSwitchUnits.Text))
            {
                OnlineOrderBackOfficeVo.MinSwitchUnits = Convert.ToInt32(txtMinSwitchUnits.Text);
            }
            if (!string.IsNullOrEmpty(txtRedemptionMultiplesUnits.Text))
            {
                OnlineOrderBackOfficeVo.RedemptionMultiplesUnits = Convert.ToInt32(txtRedemptionMultiplesUnits.Text);
            }
            if (!string.IsNullOrEmpty(txtExitLoad.Text))
                OnlineOrderBackOfficeVo.SecurityCode = txtSecuritycode.Text;
            OnlineOrderBackOfficeVo.ExternalType = ddlRT.SelectedValue;
            if (!string.IsNullOrEmpty(txtinvestment.Text))
            {
                OnlineOrderBackOfficeVo.PASPD_MaxInvestment = Convert.ToDouble(txtinvestment.Text);
            }

        }

        private void ControlViewEditMode(bool isViewMode)
        {
            if (isViewMode)
            {
                txtScname.Enabled = false;
                txtAMFI.Enabled = false;
                ddlAmc.Enabled = false;
                ddlcategory.Enabled = false;
                txtFvale.Enabled = false;
                ddlScategory.Enabled = false;
                ddlSScategory.Enabled = false;
                ddlSctype.Enabled = false;
                ddlOption.Enabled = false;
                ddlBname.Enabled = false;
                txtBranch.Enabled = false;
                txtACno.Enabled = false;
                chkInfo.Enabled = false;
                ddlSchemeList.Enabled = false;
                //txtNFOStartDate.ToString()="";
                //OnlineOrderBackOfficeVo.NFOEndDate = DateTime.Parse(txtNFOendDate.SelectedDate.ToString());
                txtLIperiod.Enabled = false;
                //DateTime time = Convert.ToDateTime(txtHH.Text + "" + txtMM.Text + " " + txtSS.Text.ToString());
                //OnlineOrderBackOfficeVo.CutOffTime = time.ToLocalTime;
                txtHH.Enabled = false;
                txtMM.Enabled = false;
                txtSS.Enabled = false;
                txtEload.Enabled = false;
                txtELremark.Enabled = false;
                txtExitLoad.Enabled = false;
                txtExitLremark.Enabled = false;
                ChkISPurchage.Enabled = false;
                ChkISRedeem.Enabled = false;
                ChkISSIP.Enabled = false;
                ChkISSTP.Enabled = false;
                ChkISSwitch.Enabled = false;
                ChkISSWP.Enabled = false;
                ChkISactive.Enabled = false;
                txtInitalPamount.Enabled = false;
                txtIMultipleamount.Enabled = false;
                txtAdditional.Enabled = false;
                txtAddMultipleamount.Enabled = false;
                txtMinRedemption.Enabled = false;
                txtMinRedemptioUnits.Enabled = false;
                txtMinSwitchAmount.Enabled = false;
                txtMinSwitchUnits.Enabled = false;
                txtRedemptionMultiplesUnits.Enabled = false;
                txtSecuritycode.Enabled = false;
                ddlRT.Enabled = false;
                txtinvestment.Enabled = false;
                txtESSchemecode.Enabled = false;
                ddlGenerationfreq.Enabled = false;
                ddlDFrequency.Enabled = false;
                btnupdate.Visible = false;
            }
            else
            {
                txtScname.Enabled = false;
                txtAMFI.Enabled = false;
                ddlAmc.Enabled = false;
                ddlcategory.Enabled = false;
                txtFvale.Enabled = true;
                ddlScategory.Enabled = false;
                ddlSScategory.Enabled = false;
                ddlSctype.Enabled = false;
                ddlOption.Enabled = true;
                ddlBname.Enabled = true;
                txtBranch.Enabled = true;
                txtACno.Enabled = true;
                chkInfo.Enabled = true;
                txtLIperiod.Enabled = true;
                txtHH.Enabled = true;
                txtMM.Enabled = true;
                txtSS.Enabled = true;
                txtEload.Enabled = true;
                txtELremark.Enabled = true;
                txtExitLoad.Enabled = true;
                txtExitLremark.Enabled = true;
                ChkISPurchage.Enabled = true;
                ChkISRedeem.Enabled = true;
                ChkISSIP.Enabled = true;
                ChkISSTP.Enabled = true;
                ChkISSwitch.Enabled = true;
                ChkISSWP.Enabled = true;
                ddlSchemeList.Enabled = true;
                ChkISactive.Enabled = false;
                txtInitalPamount.Enabled = true;
                txtIMultipleamount.Enabled = true;
                txtAdditional.Enabled = true;
                txtAddMultipleamount.Enabled = true;
                txtMinRedemption.Enabled = true;
                txtMinRedemptioUnits.Enabled = true;
                txtMinSwitchAmount.Enabled = true;
                txtMinSwitchUnits.Enabled = true;
                txtRedemptionMultiplesUnits.Enabled = true;
                txtSecuritycode.Enabled = true;
                ddlRT.Enabled = true;
                txtinvestment.Enabled = true;
                txtESSchemecode.Enabled = true;
                ddlGenerationfreq.Enabled = true;
                ddlDFrequency.Enabled = true;
                btnsubmit.Visible = false;
                btnupdate.Visible = true;
                tblMessage.Visible =false;
                lbBack.Visible = false;
            }

        }
        protected void ViewSchemeDetails()
        {
            OnlineOrderBackOfficeVo = (OnlineOrderBackOfficeVo)Session["SchemeList"];

            // txtScname.Text = OnlineOrderBackOfficeVo.SchemePlanName;
            txtESSchemecode.Text = OnlineOrderBackOfficeVo.ExternalCode;
            //txtAMFI.Text = OnlineOrderBackOfficeVo.ExternalCode;
            //BindAMC();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.DividendFrequency))
            {
                ddlDFrequency.SelectedValue = OnlineOrderBackOfficeVo.DividendFrequency.ToString();
            }
            else
            {
                ddlDFrequency.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.GenerationFrequency))
            {
                ddlGenerationfreq.SelectedValue = OnlineOrderBackOfficeVo.GenerationFrequency.ToString();

            }
            else
            {
                ddlGenerationfreq.SelectedValue = "0";
            }
            if (OnlineOrderBackOfficeVo.AMCCode != 0)
            {

                ddlAmc.SelectedValue = OnlineOrderBackOfficeVo.AMCCode.ToString();
            }
            else
            {
                ddlAmc.SelectedValue = "0";
            }




            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetCategoryCode))
            {
                ddlcategory.SelectedValue = OnlineOrderBackOfficeVo.AssetCategoryCode.ToString();
            }
            else
            {
                ddlcategory.SelectedValue = "0";
            }

            LoadAllSchemeList(Convert.ToInt32(ddlAmc.SelectedValue));

            if (OnlineOrderBackOfficeVo.SchemePlanCode != 0)
            {
                ddlSchemeList.SelectedValue = OnlineOrderBackOfficeVo.SchemePlanCode.ToString();
            }
            else
            {
                ddlSchemeList.SelectedValue = "0";
            }
            txtFvale.Text = OnlineOrderBackOfficeVo.FaceValue.ToString();
            BindSubCategory(ddlcategory.SelectedValue);
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetSubCategoryCode))
            {
                ddlScategory.SelectedValue = OnlineOrderBackOfficeVo.AssetSubCategoryCode.ToString();
            }
            else
            {
                ddlScategory.SelectedValue = "0";
            }
            BindSubSubCategory(ddlcategory.SelectedValue, ddlScategory.SelectedValue);
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetSubSubCategory))
            {
                ddlSScategory.SelectedValue = OnlineOrderBackOfficeVo.AssetSubSubCategory.ToString();
            }
            else
            {
                ddlSScategory.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SchemeType))
            {
                ddlSctype.Enabled = true;
                ddlSctype.SelectedValue = OnlineOrderBackOfficeVo.SchemeType.ToString();
                ddlSctype.Enabled = false;
            }
            else
            {
                ddlSctype.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SchemeOption))
            {
                ddlOption.SelectedValue = OnlineOrderBackOfficeVo.SchemeOption.ToString();
            }
            else
            {
                ddlOption.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.WERPBM_BankCode))
            {
                ddlBname.SelectedValue = OnlineOrderBackOfficeVo.WERPBM_BankCode.ToString();
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.Branch))
            {
                txtBranch.Text = OnlineOrderBackOfficeVo.Branch.ToString();
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AccountNumber))
            {
                txtACno.Text = OnlineOrderBackOfficeVo.AccountNumber.ToString();
            }
            if (OnlineOrderBackOfficeVo.CustomerSubTypeCode == "IND")
            {
                ChkNRI.Checked = true;
            }
            if (OnlineOrderBackOfficeVo.CustomerSubTypeCode == "NIND")
            {
                ChkBO.Checked = true;
            }
            if (OnlineOrderBackOfficeVo.IsOnline == 1)
            {
                chkonline.Checked = true;
                chkoffline.Checked = false;
            }
            else
            {
                chkonline.Checked = false;
                chkoffline.Checked = true;
            }


            if (OnlineOrderBackOfficeVo.IsNFO == 1)
            {
                chkInfo.Checked = true;
            }
            else
            {
                chkInfo.Checked = false;
            }

            if (OnlineOrderBackOfficeVo.NFOStartDate != DateTime.MinValue)
            {
                txtNFOStartDate.SelectedDate = OnlineOrderBackOfficeVo.NFOStartDate;

            }
            //else
            //{
            //    txtNFOStartDate.SelectedDate = DateTime.MinValue;  
            //}
            if (OnlineOrderBackOfficeVo.NFOEndDate != DateTime.MinValue)
            {
                txtNFOendDate.SelectedDate = OnlineOrderBackOfficeVo.NFOEndDate;
            }
            //else
            //{
            //    txtNFOendDate.SelectedDate = DateTime.MinValue;
            //}
            txtLIperiod.Text = OnlineOrderBackOfficeVo.LockInPeriod.ToString();
            string Time = (txtHH.Text + ':' + txtMM.Text + ':' + txtSS.Text);
            Time = Convert.ToString(OnlineOrderBackOfficeVo.CutOffTime).ToString();
            string pattern = @":|:|:";
            string[] result = Regex.Split(Time, pattern);
            for (int ctr = 0; ctr < result.Length; ctr++)
            {
                if (ctr == 0)
                {
                    txtHH.Text = result[ctr];
                }
                if (ctr == 1)
                {
                    txtMM.Text = result[ctr];
                }
                if (ctr == 2)
                {
                    txtSS.Text = result[ctr];
                }
            }
            //if (ctr < result.Length - 1)              
            //{
            //}
            //foreach (string result in Regex.Split(Time, pattern))
            //{  
            //   txtHH.Text = result;
            //    //txtMM.Text = result;
            //   // txtSS.Text = result;
            //}

            //DateTime time = Convert.ToDateTime(txtHH.Text + "" + txtMM.Text + " " + txtSS.Text.ToString());
            //OnlineOrderBackOfficeVo.CutOffTime = time.ToLocalTime;
            txtEload.Text = OnlineOrderBackOfficeVo.EntryLoadPercentag.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.EntryLoadRemark))
                txtELremark.Text = OnlineOrderBackOfficeVo.EntryLoadRemark.ToString();
            txtExitLoad.Text = OnlineOrderBackOfficeVo.ExitLoadPercentage.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.ExitLoadRemark))
                txtExitLremark.Text = OnlineOrderBackOfficeVo.ExitLoadRemark.ToString();
            if (OnlineOrderBackOfficeVo.IsPurchaseAvailable == 1)
            {
                ChkISPurchage.Checked = true;
            }
            else
            {
                ChkISPurchage.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsRedeemAvailable == 1)
            {
                ChkISRedeem.Checked = true;
            }

            else
            {
                ChkISRedeem.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSIPAvailable == 1)
            {
                ChkISSIP.Checked = true;
            }
            else
            {
                ChkISSIP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSTPAvailable == 1)
            {
                ChkISSTP.Checked = true;
            }
            else
            {
                ChkISSTP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSwitchAvailable == 1)
            {
                ChkISSwitch.Checked = true;
            }
            else
            {
                ChkISSwitch.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSWPAvailable == 1)
            {
                ChkISSWP.Checked = true;
            }
            else
            {
                ChkISSWP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.Status == "Active")
            {
                ChkISactive.Checked = true;
            }
            else
            {
                ChkISactive.Checked = false;
            }
            txtInitalPamount.Text = OnlineOrderBackOfficeVo.InitialPurchaseAmount.ToString();
            txtIMultipleamount.Text = OnlineOrderBackOfficeVo.InitialMultipleAmount.ToString();
            txtAdditional.Text = OnlineOrderBackOfficeVo.AdditionalPruchaseAmount.ToString();
            txtAddMultipleamount.Text = OnlineOrderBackOfficeVo.AdditionalMultipleAmount.ToString();
            txtMinRedemption.Text = OnlineOrderBackOfficeVo.MinRedemptionAmount.ToString();
            txtMinRedemptioUnits.Text = OnlineOrderBackOfficeVo.MinRedemptionUnits.ToString();
            txtMinSwitchAmount.Text = OnlineOrderBackOfficeVo.SwitchMultipleAmount.ToString();
            txtMinSwitchUnits.Text = OnlineOrderBackOfficeVo.SwitchMultiplesUnits.ToString();
            txtRedemptionMultiplesUnits.Text = OnlineOrderBackOfficeVo.RedemptionMultiplesUnits.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SecurityCode))
                txtSecuritycode.Text = OnlineOrderBackOfficeVo.SecurityCode.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.ExternalType))
            {
                ddlRT.SelectedValue = OnlineOrderBackOfficeVo.ExternalType.ToString();
            }
            txtinvestment.Text = OnlineOrderBackOfficeVo.PASPD_MaxInvestment.ToString();
        }



        protected void EditSchemeDetails()
        {
            OnlineOrderBackOfficeVo = (OnlineOrderBackOfficeVo)Session["SchemeList"];

            // txtScname.Text = OnlineOrderBackOfficeVo.SchemePlanName;
            txtESSchemecode.Text = OnlineOrderBackOfficeVo.ExternalCode;
            //txtAMFI.Text = OnlineOrderBackOfficeVo.ExternalCode;
            //BindAMC();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.DividendFrequency))
            {
                ddlDFrequency.SelectedValue = OnlineOrderBackOfficeVo.DividendFrequency.ToString();
            }
            else
            {
                ddlDFrequency.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.GenerationFrequency))
            {
                ddlGenerationfreq.SelectedValue = OnlineOrderBackOfficeVo.GenerationFrequency.ToString();

            }
            else
            {
                ddlGenerationfreq.SelectedValue = "0";
            }
            if (OnlineOrderBackOfficeVo.AMCCode != 0)
            {

                ddlAmc.SelectedValue = OnlineOrderBackOfficeVo.AMCCode.ToString();
            }
            else
            {
                ddlAmc.SelectedValue = "0";
            }




            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetCategoryCode))
            {
                ddlcategory.SelectedValue = OnlineOrderBackOfficeVo.AssetCategoryCode.ToString();
            }
            else
            {
                ddlcategory.SelectedValue = "0";
            }

            LoadAllSchemeList(Convert.ToInt32(ddlAmc.SelectedValue));

            if (OnlineOrderBackOfficeVo.SchemePlanCode != 0)
            {
                ddlSchemeList.SelectedValue = OnlineOrderBackOfficeVo.SchemePlanCode.ToString();
            }
            else
            {
                ddlSchemeList.SelectedValue = "0";
            }
            txtFvale.Text = OnlineOrderBackOfficeVo.FaceValue.ToString();
            BindSubCategory(ddlcategory.SelectedValue);
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetSubCategoryCode))
            {
                ddlScategory.SelectedValue = OnlineOrderBackOfficeVo.AssetSubCategoryCode.ToString();
            }
            else
            {
                ddlScategory.SelectedValue = "0";
            }
            BindSubSubCategory(ddlcategory.SelectedValue, ddlScategory.SelectedValue);
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetSubSubCategory))
            {
                ddlSScategory.SelectedValue = OnlineOrderBackOfficeVo.AssetSubSubCategory.ToString();
            }
            else
            {
                ddlSScategory.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SchemeType))
            {
                ddlSctype.Enabled = true;
                ddlSctype.SelectedValue = OnlineOrderBackOfficeVo.SchemeType.ToString();
                ddlSctype.Enabled = false;
            }
            else
            {
                ddlSctype.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SchemeOption))
            {
                ddlOption.SelectedValue = OnlineOrderBackOfficeVo.SchemeOption.ToString();
            }
            else
            {
                ddlOption.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.WERPBM_BankCode))
            {
                ddlBname.SelectedValue = OnlineOrderBackOfficeVo.WERPBM_BankCode.ToString();
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.Branch))
            {
                txtBranch.Text = OnlineOrderBackOfficeVo.Branch.ToString();
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AccountNumber))
            {
                txtACno.Text = OnlineOrderBackOfficeVo.AccountNumber.ToString();
            }
            if (OnlineOrderBackOfficeVo.CustomerSubTypeCode == "IND")
            {
                ChkNRI.Checked = true;
            }
            if (OnlineOrderBackOfficeVo.CustomerSubTypeCode == "NIND")
            {
                ChkBO.Checked = true;
            }
            if (OnlineOrderBackOfficeVo.IsOnline == 1)
            {
                chkonline.Checked = true;
                chkoffline.Checked = false;
            }
            else
            {
                chkonline.Checked = false;
                chkoffline.Checked = true;
            }


            if (OnlineOrderBackOfficeVo.IsNFO == 1)
            {
                chkInfo.Checked = true;
            }
            else
            {
                chkInfo.Checked = false;
            }

            if (OnlineOrderBackOfficeVo.NFOStartDate != DateTime.MinValue)
            {
                txtNFOStartDate.SelectedDate = OnlineOrderBackOfficeVo.NFOStartDate;

            }
            //else
            //{
            //    txtNFOStartDate.SelectedDate = DateTime.MinValue;  
            //}
            if (OnlineOrderBackOfficeVo.NFOEndDate != DateTime.MinValue)
            {
                txtNFOendDate.SelectedDate = OnlineOrderBackOfficeVo.NFOEndDate;
            }
            //else
            //{
            //    txtNFOendDate.SelectedDate = DateTime.MinValue;
            //}
            txtLIperiod.Text = OnlineOrderBackOfficeVo.LockInPeriod.ToString();
            string Time = (txtHH.Text + ':' + txtMM.Text + ':' + txtSS.Text);
            Time = Convert.ToString(OnlineOrderBackOfficeVo.CutOffTime).ToString();
            string pattern = @":|:|:";
            string[] result = Regex.Split(Time, pattern);
            for (int ctr = 0; ctr < result.Length; ctr++)
            {
                if (ctr == 0)
                {
                    txtHH.Text = result[ctr];
                }
                if (ctr == 1)
                {
                    txtMM.Text = result[ctr];
                }
                if (ctr == 2)
                {
                    txtSS.Text = result[ctr];
                }
            }
            //if (ctr < result.Length - 1)              
            //{
            //}
            //foreach (string result in Regex.Split(Time, pattern))
            //{  
            //   txtHH.Text = result;
            //    //txtMM.Text = result;
            //   // txtSS.Text = result;
            //}

            //DateTime time = Convert.ToDateTime(txtHH.Text + "" + txtMM.Text + " " + txtSS.Text.ToString());
            //OnlineOrderBackOfficeVo.CutOffTime = time.ToLocalTime;
            txtEload.Text = OnlineOrderBackOfficeVo.EntryLoadPercentag.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.EntryLoadRemark))
                txtELremark.Text = OnlineOrderBackOfficeVo.EntryLoadRemark.ToString();
            txtExitLoad.Text = OnlineOrderBackOfficeVo.ExitLoadPercentage.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.ExitLoadRemark))
                txtExitLremark.Text = OnlineOrderBackOfficeVo.ExitLoadRemark.ToString();
            if (OnlineOrderBackOfficeVo.IsPurchaseAvailable == 1)
            {
                ChkISPurchage.Checked = true;
            }
            else
            {
                ChkISPurchage.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsRedeemAvailable == 1)
            {
                ChkISRedeem.Checked = true;
            }

            else
            {
                ChkISRedeem.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSIPAvailable == 1)
            {
                ChkISSIP.Checked = true;
            }
            else
            {
                ChkISSIP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSTPAvailable == 1)
            {
                ChkISSTP.Checked = true;
            }
            else
            {
                ChkISSTP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSwitchAvailable == 1)
            {
                ChkISSwitch.Checked = true;
            }
            else
            {
                ChkISSwitch.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSWPAvailable == 1)
            {
                ChkISSWP.Checked = true;
            }
            else
            {
                ChkISSWP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.Status == "Active")
            {
                ChkISactive.Checked = true;
            }
            else
            {
                ChkISactive.Checked = false;
            }
            txtInitalPamount.Text = OnlineOrderBackOfficeVo.InitialPurchaseAmount.ToString();
            txtIMultipleamount.Text = OnlineOrderBackOfficeVo.InitialMultipleAmount.ToString();
            txtAdditional.Text = OnlineOrderBackOfficeVo.AdditionalPruchaseAmount.ToString();
            txtAddMultipleamount.Text = OnlineOrderBackOfficeVo.AdditionalMultipleAmount.ToString();
            txtMinRedemption.Text = OnlineOrderBackOfficeVo.MinRedemptionAmount.ToString();
            txtMinRedemptioUnits.Text = OnlineOrderBackOfficeVo.MinRedemptionUnits.ToString();
            txtMinSwitchAmount.Text = OnlineOrderBackOfficeVo.SwitchMultipleAmount.ToString();
            txtMinSwitchUnits.Text = OnlineOrderBackOfficeVo.SwitchMultiplesUnits.ToString();
            txtRedemptionMultiplesUnits.Text = OnlineOrderBackOfficeVo.RedemptionMultiplesUnits.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SecurityCode))
                txtSecuritycode.Text = OnlineOrderBackOfficeVo.SecurityCode.ToString();
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.ExternalType))
            {
                ddlRT.SelectedValue = OnlineOrderBackOfficeVo.ExternalType.ToString();
            }
            txtinvestment.Text = OnlineOrderBackOfficeVo.PASPD_MaxInvestment.ToString();



        }
        protected void Clearallcontrols()
        {
            //ddlProduct.Items.Clear();
            txtScname.Text = "";
            txtAMFI.Text = "";
            ddlAmc.Items.Clear();
            ddlcategory.Items.Clear();
            txtFvale.Text = ""; ;
            ddlScategory.Items.Clear();
            ddlSScategory.Items.Clear();
            ddlSctype.Items.Clear();
            ddlOption.Items.Clear();
            ddlBname.Items.Clear();
            txtBranch.Text = ""; ;
            txtACno.Text = ""; ;
            chkInfo.Checked = false;
            //txtNFOStartDate.ToString()="";
            //OnlineOrderBackOfficeVo.NFOEndDate = DateTime.Parse(txtNFOendDate.SelectedDate.ToString());
            txtLIperiod.Text = "";
            //DateTime time = Convert.ToDateTime(txtHH.Text + "" + txtMM.Text + " " + txtSS.Text.ToString());
            //OnlineOrderBackOfficeVo.CutOffTime = time.ToLocalTime;
            txtEload.Text = "";
            txtELremark.Text = "";
            txtExitLoad.Text = "";
            txtExitLremark.Text = "";
            ChkISPurchage.Checked = false;
            ChkISRedeem.Checked = false;
            ChkISSIP.Checked = false;
            ChkISSTP.Checked = false;
            ChkISSwitch.Checked = false;
            ChkISSWP.Checked = false;
            ChkISactive.Checked = false;
            txtInitalPamount.Text = "";
            txtIMultipleamount.Text = "";
            txtAdditional.Text = "";
            txtAddMultipleamount.Text = "";
            txtMinRedemption.Text = "";
            txtMinRedemptioUnits.Text = "";
            txtMinSwitchAmount.Text = "";
            txtMinSwitchUnits.Text = "";
            txtRedemptionMultiplesUnits.Text = "";
            txtSecuritycode.Text = "";
            ddlRT.Items.Clear();
            txtinvestment.Text = "";


        }
        protected void lbBack_OnClick(object sender, EventArgs e)
        {
            ControlViewEditMode(false);
        }
        protected void ddlSchemeList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList.SelectedIndex != 0)
            {
                int schemepalncode = int.Parse(ddlSchemeList.SelectedValue);

                ViewState["Schemecode"] = schemepalncode;
            }
        }
        private bool AMFIValidation(string externalcode)
        {
            bool result = true;
            if (schemeId != 0)
            {
                schemeId = int.Parse(ViewState["Schemecode"].ToString());
            }
            try
            {
                if (OnlineOrderBackOfficeBo.AMFIduplicateCheck(schemeId, externalcode))
                {
                    result = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('AMFI Code already exists !!');", true);
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        protected void btnsubmit_click(object sender, EventArgs e)
        {
            SaveSchemeDetails();
            List<int> SchemePlancodes = new List<int>();
            if (AMFIValidation(txtAMFI.Text))
            {
                SchemePlancodes = OnlineOrderBackOfficeBo.CreateOnlineSchemeSetUp(OnlineOrderBackOfficeVo, userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Scheme Submit Successfully!!');", true);
                Clearallcontrols();
            }
        }

        protected void btnUpdate_click(object sender, EventArgs e)
        {
            string message = string.Empty;
            OnlineOrderBackOfficeVo = (OnlineOrderBackOfficeVo)Session["SchemeList"];
            OnlineOrderBackOfficeVo.Product = ddlProduct.SelectedValue;

            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetCategoryCode))
            {
                OnlineOrderBackOfficeVo.AssetCategoryCode = ddlcategory.SelectedValue;
            }
            else
            {
                ddlcategory.SelectedValue = "0";
            }


            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetSubCategoryCode))
            {
                OnlineOrderBackOfficeVo.AssetSubCategoryCode = ddlScategory.SelectedValue;
            }
            else
            {
                ddlScategory.SelectedValue = "Select";
            }
            BindSubSubCategory(ddlcategory.SelectedValue, ddlScategory.SelectedValue);
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AssetSubSubCategory))
            {
                OnlineOrderBackOfficeVo.AssetSubSubCategory = ddlSScategory.SelectedValue;
            }
            else
            {
                ddlSScategory.SelectedValue = "0";
            }

            //if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.DividendFrequency))
            //{
            //    OnlineOrderBackOfficeVo.DividendFrequency = ddlDFrequency.SelectedValue;
            //}
            //else
            //{
            //    ddlDFrequency.SelectedValue = "0";
            //}
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.WERPBM_BankCode))
            {
                OnlineOrderBackOfficeVo.WERPBM_BankCode = ddlBname.SelectedValue;
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SchemeType))
            {
                ddlSctype.Enabled = true;
                OnlineOrderBackOfficeVo.SchemeType = ddlSctype.SelectedValue;
                ddlSctype.Enabled = false;
            }
            else
            {
                ddlSctype.SelectedValue = "Select";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SchemeOption))
            {
                OnlineOrderBackOfficeVo.SchemeOption = ddlOption.SelectedValue;
            }
            else
            {
                ddlOption.SelectedValue = "Select";
            }

            //if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.GenerationFrequency))
            //{
            //    OnlineOrderBackOfficeVo.GenerationFrequency = ddlGenerationfreq.SelectedValue;

            //}
            //else
            //{
            //    ddlGenerationfreq.SelectedValue = "0";
            //}
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.ExternalType))
            {
                OnlineOrderBackOfficeVo.ExternalType = ddlRT.SelectedValue;
            }
            else
            {
                ddlRT.SelectedValue = "Select";
            }

            if (!string.IsNullOrEmpty(txtACno.Text))
            {
                OnlineOrderBackOfficeVo.AccountNumber = txtACno.Text;
            }
            else
            {
                txtACno.Text = "0";
            }

            if (OnlineOrderBackOfficeVo.NFOStartDate != DateTime.MinValue)
            {
                OnlineOrderBackOfficeVo.NFOStartDate = DateTime.Parse(txtNFOStartDate.SelectedDate.ToString());
            }
            if (OnlineOrderBackOfficeVo.NFOEndDate != DateTime.MinValue)
            {
                OnlineOrderBackOfficeVo.NFOEndDate = DateTime.Parse(txtNFOendDate.SelectedDate.ToString());
            }
            if (!string.IsNullOrEmpty(txtInitalPamount.Text))
            {
                OnlineOrderBackOfficeVo.InitialPurchaseAmount =Convert.ToDouble(txtInitalPamount.Text.ToString());
            }
            else
            {
                txtInitalPamount.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtIMultipleamount.Text))
            {
                OnlineOrderBackOfficeVo.InitialMultipleAmount = Convert.ToDouble(txtIMultipleamount.Text.ToString());
            }
            else
            {
                txtIMultipleamount.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtAdditional.Text))
            {
                OnlineOrderBackOfficeVo.AdditionalPruchaseAmount = Convert.ToDouble(txtAdditional.Text.ToString());
            }
            else
            {
                txtAdditional.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.AdditionalMultipleAmount.ToString()))
            {
                OnlineOrderBackOfficeVo.AdditionalMultipleAmount = Convert.ToDouble(txtAddMultipleamount.Text.ToString());
            }
            else
            {
                txtAddMultipleamount.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.MinRedemptionAmount.ToString()))
            {
                OnlineOrderBackOfficeVo.MinRedemptionAmount = Convert.ToDouble(txtMinRedemption.Text.ToString());
            }
            else
            {
                txtMinRedemption.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtRedemptionmultiple.Text))
            {
                OnlineOrderBackOfficeVo.RedemptionMultipleAmount = Convert.ToDouble(txtRedemptionmultiple.Text.ToString());
            }
            else
            {
                txtRedemptionmultiple.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.MinRedemptionUnits.ToString()))
            {
                OnlineOrderBackOfficeVo.MinRedemptionUnits = Convert.ToInt32(txtMinRedemptioUnits.Text.ToString());
            }
            else
            {
                txtMinRedemptioUnits.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.RedemptionMultiplesUnits.ToString()))
            {
                OnlineOrderBackOfficeVo.RedemptionMultiplesUnits = Convert.ToInt32(txtRedemptionMultiplesUnits.Text.ToString());
            }
            else
            {
                txtRedemptionMultiplesUnits.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.MinSwitchAmount.ToString()))
            {
                OnlineOrderBackOfficeVo.MinSwitchAmount = Convert.ToDouble(txtMinSwitchAmount.Text.ToString());
            }
            else
            {
                txtMinSwitchAmount.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtSwitchMultipleAmount.Text))
            {
                OnlineOrderBackOfficeVo.SwitchMultipleAmount = Convert.ToDouble(txtSwitchMultipleAmount.Text.ToString());
            }
            else
            {
                txtSwitchMultipleAmount.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtSwitchMultipleUnits.Text))
            {
                OnlineOrderBackOfficeVo.SwitchMultiplesUnits = Convert.ToInt32(txtSwitchMultipleUnits.Text.ToString());
            }
            else
            {
                txtSwitchMultipleUnits.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtMinSwitchUnits.Text))
            {
                OnlineOrderBackOfficeVo.MinSwitchUnits =Convert.ToInt32(txtMinSwitchUnits.Text.ToString());
            }
            else
            {
                txtMinSwitchUnits.Text = "0";
            }
            if (!string.IsNullOrEmpty(txtinvestment.Text))
            {
                OnlineOrderBackOfficeVo.PASPD_MaxInvestment =Convert.ToDouble(txtinvestment.Text.ToString());
            }
            else
            {
                txtinvestment.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.FaceValue.ToString()))
            {
                OnlineOrderBackOfficeVo.FaceValue = Convert.ToDouble(txtFvale.Text.ToString());
            }
            else
            {
                txtFvale.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.LockInPeriod.ToString()))
            {
                OnlineOrderBackOfficeVo.LockInPeriod = Convert.ToInt32(txtLIperiod.Text.ToString());
            }
            else
            {
                txtLIperiod.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.EntryLoadPercentag.ToString()))
            {
                OnlineOrderBackOfficeVo.EntryLoadPercentag = Convert.ToDouble(txtEload.Text.ToString());
            }
            else
            {
                txtEload.Text = "0";
            }

            if (OnlineOrderBackOfficeVo.AMCCode != 0)
            {

                OnlineOrderBackOfficeVo.AMCCode = Convert.ToInt32(ddlAmc.SelectedValue.ToString());
            }
            else
            {
                ddlAmc.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(txtExitLoad.Text))
            {
                OnlineOrderBackOfficeVo.EntryLoadRemark = txtELremark.Text;
            }
            else
            {
                txtELremark.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.ExitLoadPercentage.ToString()))
            {
                OnlineOrderBackOfficeVo.ExitLoadPercentage = Convert.ToDouble(txtExitLoad.Text.ToString());
            }
            else
            {
                txtExitLoad.Text = "0";
            }

            if (!string.IsNullOrEmpty(txtExitLoad.Text))
            {
                OnlineOrderBackOfficeVo.ExitLoadRemark = txtExitLoad.Text;
            }
            else
            {
                txtExitLoad.Text = "0";
            }
            if (OnlineOrderBackOfficeVo.IsPurchaseAvailable == 1)
            {
                ChkISPurchage.Checked = true;
            }
            else
            {
                ChkISPurchage.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsRedeemAvailable == 1)
            {
                ChkISRedeem.Checked = true;
            }

            else
            {
                ChkISRedeem.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSIPAvailable == 1)
            {
                ChkISSIP.Checked = true;
            }
            else
            {
                ChkISSIP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSTPAvailable == 1)
            {
                ChkISSTP.Checked = true;
            }
            else
            {
                ChkISSTP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSwitchAvailable == 1)
            {
                ChkISSwitch.Checked = true;
            }
            else
            {
                ChkISSwitch.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.IsSWPAvailable == 1)
            {
                ChkISSWP.Checked = true;
            }
            else
            {
                ChkISSWP.Checked = false;
            }
            if (OnlineOrderBackOfficeVo.Status == "Active")
            {
                ChkISactive.Checked = true;
            }
            else
            {
                ChkISactive.Checked = false;
            }
            if (!string.IsNullOrEmpty(txtSecuritycode.Text))
            {
                OnlineOrderBackOfficeVo.SecurityCode = txtSecuritycode.Text;
            }
            else
            {
                txtSecuritycode.Text = "0";
            }
            if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.ExternalType))
            {
                OnlineOrderBackOfficeVo.ExternalType = ddlRT.SelectedValue.ToString();
            }
            if (chkInfo.Checked)
            {
                OnlineOrderBackOfficeVo.IsNFO = 1;
            }
            else
            {
                OnlineOrderBackOfficeVo.IsNFO = 0;
            }
            if (ChkISactive.Checked)
            {
                OnlineOrderBackOfficeVo.Status = "Active";
            }
            else
            {
                OnlineOrderBackOfficeVo.Status = "Liquidated";
            }
            if (OnlineOrderBackOfficeVo.IsOnline == 1)
            {
                chkonline.Checked = true;
                chkoffline.Checked = false;
            }
            else
            {
                chkonline.Checked = false;
                chkoffline.Checked = true;
            }
            txtLIperiod.Text = OnlineOrderBackOfficeVo.LockInPeriod.ToString();

            //if(!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.GenerationFrequency))
            //{
            OnlineOrderBackOfficeVo.GenerationFrequency = ddlGenerationfreq.SelectedValue;
            //}
            //else
            //{
            //    ddlGenerationfreq.SelectedValue="0";
            //}
            if (!string.IsNullOrEmpty(txtBranch.Text))
            {
                OnlineOrderBackOfficeVo.Branch = txtBranch.Text;
            }
            else
            {
                txtBranch.Text = "0";
            }

            bool bResult = OnlineOrderBackOfficeBo.UpdateSchemeSetUpDetail(OnlineOrderBackOfficeVo, schemeplancode);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Scheme Successfully Updated!!');", true);
            message = CreateUserMessage(schemeplancode);
            ShowMessage(message);
            lbBack.Visible = true;
            ControlMode(false);
        }

        private void ControlMode(bool isViewMode)
        {
            if (isViewMode)
            {
                txtScname.Enabled = false;
                txtAMFI.Enabled = false;
                ddlAmc.Enabled = false;
                ddlcategory.Enabled = false;
                txtFvale.Enabled = false;
                ddlScategory.Enabled = false;
                ddlSScategory.Enabled = false;
                ddlSctype.Enabled = false;
                ddlOption.Enabled = false;
                ddlBname.Enabled = false;
                txtBranch.Enabled = false;
                txtACno.Enabled = false;
                chkInfo.Enabled = false;
                ddlSchemeList.Enabled = false;
                //txtNFOStartDate.ToString()="";
                //OnlineOrderBackOfficeVo.NFOEndDate = DateTime.Parse(txtNFOendDate.SelectedDate.ToString());
                txtLIperiod.Enabled = false;
                //DateTime time = Convert.ToDateTime(txtHH.Text + "" + txtMM.Text + " " + txtSS.Text.ToString());
                //OnlineOrderBackOfficeVo.CutOffTime = time.ToLocalTime;
                txtHH.Enabled = false;
                txtMM.Enabled = false;
                txtSS.Enabled = false;
                txtEload.Enabled = false;
                txtELremark.Enabled = false;
                txtExitLoad.Enabled = false;
                txtExitLremark.Enabled = false;
                ChkISPurchage.Enabled = false;
                ChkISRedeem.Enabled = false;
                ChkISSIP.Enabled = false;
                ChkISSTP.Enabled = false;
                ChkISSwitch.Enabled = false;
                ChkISSWP.Enabled = false;
                ChkISactive.Enabled = false;
                txtInitalPamount.Enabled = false;
                txtIMultipleamount.Enabled = false;
                txtAdditional.Enabled = false;
                txtAddMultipleamount.Enabled = false;
                txtMinRedemption.Enabled = false;
                txtMinRedemptioUnits.Enabled = false;
                txtMinSwitchAmount.Enabled = false;
                txtMinSwitchUnits.Enabled = false;
                txtRedemptionMultiplesUnits.Enabled = false;
                txtSecuritycode.Enabled = false;
                ddlRT.Enabled = false;
                txtinvestment.Enabled = false;
                txtESSchemecode.Enabled = false;
                ddlGenerationfreq.Enabled = false;
                ddlDFrequency.Enabled = false;
                btnupdate.Visible = false;
            }
            else
            {
                ddlProduct.Enabled = false;
                ChkNRI.Enabled = false;
                ChkBO.Enabled = false;
                txtScname.Enabled = false;
                txtAMFI.Enabled = false;
                ddlAmc.Enabled = false;
                ddlcategory.Enabled = false;
                txtFvale.Enabled = false;
                ddlScategory.Enabled = false;
                ddlSScategory.Enabled = false;
                ddlSctype.Enabled = false;
                ddlOption.Enabled = false;
                ddlBname.Enabled = false;
                txtBranch.Enabled = false;
                txtACno.Enabled = false;
                chkInfo.Enabled = false;
                txtLIperiod.Enabled = false;
                txtHH.Enabled = false;
                txtMM.Enabled = false;
                txtSS.Enabled = false;
                txtEload.Enabled = false;
                txtELremark.Enabled = false;
                txtExitLoad.Enabled = false;
                txtExitLremark.Enabled = false;
                ChkISPurchage.Enabled = false;
                ChkISRedeem.Enabled = false;
                ChkISSIP.Enabled = false;
                ChkISSTP.Enabled = false;
                ChkISSwitch.Enabled = false;
                ChkISSWP.Enabled = false;
                ddlSchemeList.Enabled = false;
                ChkISactive.Enabled = false;
                txtInitalPamount.Enabled = false;
                txtIMultipleamount.Enabled = false;
                txtAdditional.Enabled = false;
                txtAddMultipleamount.Enabled = false;
                txtMinRedemption.Enabled = false;
                txtMinRedemptioUnits.Enabled = false;
                txtMinSwitchAmount.Enabled = false;
                txtMinSwitchUnits.Enabled = false;
                txtRedemptionMultiplesUnits.Enabled = false;
                txtSecuritycode.Enabled =false;
                ddlRT.Enabled = false;
                txtinvestment.Enabled = false;
                txtESSchemecode.Enabled = false;
                ddlGenerationfreq.Enabled = false;
                ddlDFrequency.Enabled = false;
                btnsubmit.Visible = false;
                btnupdate.Visible = false;
            }
        }


        private string CreateUserMessage(int schemeplancode)
        {
            string userMessage = string.Empty;
            if (schemeplancode != 0)
            {
                //if (isCutOffTimeOver)
                userMessage = "your Information Is Not Updated";
                //else
                //    userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();
            }
            else
            {
                userMessage = "You have Updated successfully";
            }

            return userMessage;

        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
    }
}