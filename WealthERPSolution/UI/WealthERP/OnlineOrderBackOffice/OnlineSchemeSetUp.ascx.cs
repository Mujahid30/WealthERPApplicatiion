using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Globalization;
using BoCommon;
using VoUser;
using BoCustomerPortfolio;
using BoCustomerProfiling;
using WealthERP.Base;
using BoProductMaster;
using BoWerpAdmin;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;
using Telerik.Web.UI;


namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineSchemeSetUp : System.Web.UI.UserControl
    {
        ProductMFBo productMFBo = new ProductMFBo();
        PriceBo priceBo = new PriceBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo = new MFProductAMCSchemePlanDetailsVo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        int schemeId = 0;
        string categoryCode;
        string subcategoryCode;
        int schemeplancode;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            if (!IsPostBack)
            {
                BindAMC();
                BindCategory();
                BindBankName();
                BindFrequency();
                Bindscheme(schemeplancode);
                BindSchemeLoockUpType();
                if (Request.QueryString["SchemePlanCode"] != null)
                {
                    schemeplancode = int.Parse(Request.QueryString["SchemePlanCode"].ToString());
                    hdnSchemePlanCode.Value = schemeplancode.ToString();
                    ViewState["Schemeplancode"] = schemeplancode.ToString();
                }
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                {
                    if (Request.QueryString["strAction"].Trim() == "Edit")
                    {
                        EditSchemeDetails();
                        lbBack.Visible = true;
                        lblBack.Visible = true;
                        ControlViewEditMode(false);
                        btnsubmit.Visible = false;
                        txtESSchemecode.Enabled = true;
                        lnkMargeScheme.Visible = true;
                        ddlNFoStatus.Items[3].Enabled = true;
                        lnkMargeScheme.Visible = true;
                        lblAllproductcode.Visible = true;
                        lnkProductcode.Visible = true;

                        ddlMargeScheme.Enabled = true;
                        lblSchemeplancode.Visible = true;
                        lblschemeplanecodetext.Visible = true;
                        txtISIN.Enabled = true;
                        btnupdate.Visible = true;
                        txtpayOutExtCode.Enabled = true;
                        txtPay.Enabled = true;
                        TextINV.Enabled = true;
                        txtReInv.Enabled = true;
                        chkBSE.Enabled = true;
                        chkRTA.Enabled = true;
                        rbtnBSE.Enabled = true;
                        rbtnRTA.Enabled = true;
                    }
                    else if (Request.QueryString["strAction"].Trim() == "View")
                    {
                        ViewSchemeDetails();
                        lbBack.Visible = true;
                        lblBack.Visible = true;
                        btnsubmit.Visible = false;
                        ControlViewEditMode(true);
                        lnkMargeScheme.Visible = true;
                        ddlNFoStatus.Items[3].Enabled = true;
                        ddlNFoStatus.Enabled = false;
                        lnkMargeEdit.Visible = true;
                        lnkMargeScheme.Visible = true;
                        ddlMargeScheme.Enabled = false;
                        txtSchemeMargeDate.Enabled = false;
                        btnMSSubmit.Visible = false;
                        btnReset.Visible = false;
                        lblAllproductcode.Visible = true;
                        lnkProductcode.Visible = true;
                        chkOnlineEnablement.Enabled = false;
                        lblSchemeplancode.Visible = true;
                        lblschemeplanecodetext.Visible = true;
                        txtISIN.Enabled = false;
                        txtpayOutExtCode.Enabled = false;
                        txtPay.Enabled = false;
                        TextINV.Enabled = false;
                        txtReInv.Enabled = false;
                        chkBSE.Enabled = false;
                        chkRTA.Enabled = false;
                        rbtnBSE.Enabled = false;
                        rbtnRTA.Enabled = false;
                        if (ddlNFoStatus.SelectedValue == "Merged")
                        {
                            lbBack.Visible = false;
                            lnkEdit.Visible = false;
                        }
                    }
                }
                BindProductcode();
            }

        }

        protected void ddlNFoStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNFoStatus.SelectedValue == "NFO")
            {
                trNFODate.Visible = true;
                txtNFOendDate.Enabled = true;
                txtNFOStartDate.Enabled = true;
            }
            else
            {
                trNFODate.Visible = false;
                txtNFOendDate.Enabled = false;
                txtNFOStartDate.Enabled = false;
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
        protected void ddlOption_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOption.SelectedValue == "DV")
            {
                ddlDFrequency.Visible = true;
                lblddlDFrequency.Visible = true;
                BindSchemeLoockUpType();
            }
            else
            {
                if (rbtnBSE.Checked)
                {
                    ddlDFrequency.Visible = false;
                    lblddlDFrequency.Visible = false;
                    lblESSchemecode.Visible = true;
                    txtESSchemecode.Visible = true;
                    trISISN.Visible = true;
                    tdlblpay.Visible = true;
                    tdtxtpay.Visible = true;
                    LbldivPay.Text = "ISINNO:";
                    trDivReInv.Visible = false;
                    tdPayOurExternalCode.Visible = false;
                    tdtxtPayExternalCode.Visible = false;
                }
                else
                {
                    ddlDFrequency.Visible = false;
                    lblddlDFrequency.Visible = false;
                    lblESSchemecode.Visible = true;
                    txtESSchemecode.Visible = true;
                    tdPayOurExternalCode.Visible = false;
                    tdtxtPayExternalCode.Visible = false;
                }
            }
        }
        protected void BindSchemeLoockUpType()
        {
            DataTable dt;
            string dividentType = ddlOption.SelectedValue;
            if (dividentType != "")
            {
                ddlDFrequency.Items.Clear();
                dt = OnlineOrderBackOfficeBo.GetSchemeLookupType(dividentType);
                ddlDFrequency.DataSource = dt;
                ddlDFrequency.DataTextField = dt.Columns["PSLV_LookupValue"].ToString();
                ddlDFrequency.DataValueField = dt.Columns["PSLV_LookupValueCode"].ToString();
                ddlDFrequency.DataBind();
                ddlDFrequency.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            else
            {
                dt = OnlineOrderBackOfficeBo.GetSchemeLookupType(dividentType);
                ddlOption.DataSource = dt;
                ddlOption.DataTextField = dt.Columns["PSLV_LookupValue"].ToString();
                ddlOption.DataValueField = dt.Columns["PSLV_LookupValueCode"].ToString();
                ddlOption.DataBind();
                ddlOption.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
        }
        private void BindAMC()
        {
            ddlAmc.Items.Clear();
            try
            {
                PriceBo priceBo = new PriceBo();
                DataTable dtGetAMCList = new DataTable();

                dtGetAMCList = priceBo.GetMutualFundList();
                ddlAmc.DataSource = dtGetAMCList;
                ddlAmc.DataTextField = dtGetAMCList.Columns["PA_AMCName"].ToString();
                ddlAmc.DataValueField = dtGetAMCList.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataBind();

                ddlAmc.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
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
        protected void Bindscheme(int SchemePlanCode)
        {
            try
            {

                DataTable dtRandT;
                dtRandT = OnlineOrderBackOfficeBo.OnlinebindRandT(SchemePlanCode);
                ddlRT.DataSource = dtRandT;

                ddlRT.DataValueField = dtRandT.Columns["XES_SourceCode"].ToString();
                ddlRT.DataTextField = dtRandT.Columns["XES_SourceName"].ToString();
                ddlRT.DataBind();
                ddlRT.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:Bindscheme()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindCategory()
        {
            ddlcategory.Items.Clear();
            DataSet dsProductAssetCategory;
            try
            {

                dsProductAssetCategory = productMFBo.GetProductAssetCategory();
                DataTable dtCategory = dsProductAssetCategory.Tables[0];
                if (dtCategory != null)
                {
                    ddlcategory.DataSource = dtCategory;
                    ddlcategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlcategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlcategory.DataBind();
                }

                ddlcategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

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
            ddlBname.Items.Clear();
            DataTable dtBankName = new DataTable();
            dtBankName = commonLookupBo.GetWERPLookupMasterValueList(7000, 0); ;
            ddlBname.DataSource = dtBankName;
            ddlBname.DataValueField = dtBankName.Columns["WCMV_LookupId"].ToString();
            ddlBname.DataTextField = dtBankName.Columns["WCMV_Name"].ToString();
            ddlBname.DataBind();
            ddlBname.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        public void BindFrequency()
        {
            ddlGenerationfreq.Items.Clear();
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
                ddlSchemeList.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlSchemeList.Items.Clear();
                ddlSchemeList.DataSource = null;
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        protected void SaveSchemeDetails()
        {
            mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(Session["newschemeplancode"].ToString());
            if (ChkNRI.Checked)
            {
                mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode = "NRI";

            }
            if (ChkBO.Checked)
            {
                mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode = "NIND";
            }
            if (!string.IsNullOrEmpty(txtESSchemecode.Text))
            {
                mfProductAMCSchemePlanDetailsVo.ExternalCode = txtESSchemecode.Text.ToString();
            }

            if (!string.IsNullOrEmpty(txtFvale.Text))
            {
                mfProductAMCSchemePlanDetailsVo.FaceValue = Convert.ToDouble(txtFvale.Text);
            }

            mfProductAMCSchemePlanDetailsVo.SchemeType = ddlSctype.SelectedValue;
            mfProductAMCSchemePlanDetailsVo.SchemeOption = ddlOption.SelectedValue;
            if (!string.IsNullOrEmpty(ddlBname.SelectedItem.Value))
            {
                mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId = int.Parse(ddlBname.SelectedItem.Value);
            }
            else
            {
                ddlBname.SelectedValue = "0";
            }

            mfProductAMCSchemePlanDetailsVo.Branch = txtBranch.Text;
            mfProductAMCSchemePlanDetailsVo.AccountNumber = txtACno.Text;
            if (!string.IsNullOrEmpty(ddlDFrequency.SelectedValue))
            {
                mfProductAMCSchemePlanDetailsVo.DividendFrequency = ddlDFrequency.SelectedValue;
            }
            mfProductAMCSchemePlanDetailsVo.GenerationFrequency = ddlGenerationfreq.SelectedValue;
            mfProductAMCSchemePlanDetailsVo.IsNFO = chkInfo.Checked ? 1 : 0;
            mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement = chkOnlineEnablement.Checked ? 1 : 0;
            if (!string.IsNullOrEmpty(txtLIperiod.Text))
            {
                mfProductAMCSchemePlanDetailsVo.LockInPeriod = int.Parse(txtLIperiod.Text.ToString());
            }

            if (!string.IsNullOrEmpty(txtHH.Text) && (!string.IsNullOrEmpty(txtMM.Text)) && (!string.IsNullOrEmpty(txtSS.Text)))
            {
                string Time = (txtHH.Text + ':' + txtMM.Text + ':' + txtSS.Text);
                if (Time != null || Time == "")
                {
                    TimeSpan CutOff = TimeSpan.Parse(Time);
                    mfProductAMCSchemePlanDetailsVo.CutOffTime = (CutOff);
                }
                else
                {
                    TimeSpan CutOff = TimeSpan.Parse("00:00:00");
                    mfProductAMCSchemePlanDetailsVo.CutOffTime = (CutOff);
                }
            }
            if (!string.IsNullOrEmpty(txtEload.Text))
            {
                mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag = Convert.ToDouble(txtEload.Text);
            }
            mfProductAMCSchemePlanDetailsVo.EntryLoadRemark = txtELremark.Text;
            if (!string.IsNullOrEmpty(txtExitLoad.Text))
            {
                mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage = Convert.ToDouble(txtExitLoad.Text);
            }
            mfProductAMCSchemePlanDetailsVo.ExitLoadRemark = txtExitLremark.Text;
            mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable = ChkISPurchage.Checked ? 1 : 0;
            mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable = ChkISRedeem.Checked ? 1 : 0;
            if (ChkISSIP.Checked)
            {
                mfProductAMCSchemePlanDetailsVo.IsSIPAvailable = 1;
                pnlSIPDetails.Visible = true;
            }
            else
            {
                mfProductAMCSchemePlanDetailsVo.IsSIPAvailable = 0;
            }

            mfProductAMCSchemePlanDetailsVo.IsSTPAvailable = ChkISSTP.Checked ? 1 : 0;


            mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable = ChkISSwitch.Checked ? 1 : 0;


            mfProductAMCSchemePlanDetailsVo.IsSWPAvailable = ChkISSWP.Checked ? 1 : 0;

            if (!string.IsNullOrEmpty(txtInitalPamount.Text))
            {
                mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount = Convert.ToDouble(txtInitalPamount.Text);
            }
            if (!string.IsNullOrEmpty(txtIMultipleamount.Text))
            {
                mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount = Convert.ToDouble(txtIMultipleamount.Text);
            }
            if (!string.IsNullOrEmpty(txtAdditional.Text))
            {
                mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount = Convert.ToDouble(txtAdditional.Text);
            }
            if (!string.IsNullOrEmpty(txtAddMultipleamount.Text))
            {
                mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount = Convert.ToDouble(txtAddMultipleamount.Text);
            }
            if (!string.IsNullOrEmpty(txtMinRedemption.Text))
            {
                mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount = Convert.ToDouble(txtMinRedemption.Text);
            }
            if (!string.IsNullOrEmpty(txtMinRedemptioUnits.Text))
            {
                mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits = Convert.ToDouble(txtMinRedemptioUnits.Text);
            }
            if (!string.IsNullOrEmpty(txtMinSwitchAmount.Text))
            {
                mfProductAMCSchemePlanDetailsVo.MinSwitchAmount = Convert.ToDouble(txtMinSwitchAmount.Text);
            }
            if (!string.IsNullOrEmpty(txtMinSwitchUnits.Text))
            {
                mfProductAMCSchemePlanDetailsVo.MinSwitchUnits = Convert.ToInt32(txtMinSwitchUnits.Text);
            }
            if (!string.IsNullOrEmpty(txtRedemptionMultiplesUnits.Text))
            {
                mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits = Convert.ToDouble(txtRedemptionMultiplesUnits.Text);
            }
            if (!string.IsNullOrEmpty(txtRedemptionmultiple.Text))
            {
                mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount = Convert.ToDouble(txtRedemptionmultiple.Text);
            }
            if (!string.IsNullOrEmpty(txtSwitchMultipleAmount.Text))
            {
                mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount = Convert.ToInt32(txtSwitchMultipleAmount.Text);
            }
            if (!string.IsNullOrEmpty(txtSwitchMultipleUnits.Text))
            {
                mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits = Convert.ToInt32(txtSwitchMultipleUnits.Text);
            }
            if (!string.IsNullOrEmpty(txtExitLoad.Text))
                mfProductAMCSchemePlanDetailsVo.SecurityCode = txtSecuritycode.Text;
            mfProductAMCSchemePlanDetailsVo.SourceCode = ddlRT.SelectedValue;
            mfProductAMCSchemePlanDetailsVo.ExternalType = ddlRT.SelectedItem.Text;
            mfProductAMCSchemePlanDetailsVo.IsETFT = (chkIsETFT.Checked) ? 1 : 0;
            if (!string.IsNullOrEmpty(txtinvestment.Text))
            {
                mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment = Convert.ToDouble(txtinvestment.Text);
            }
            if (ddlDFrequency.SelectedValue == "BT" && rbtnBSE.Checked == true)
            {
                mfProductAMCSchemePlanDetailsVo.DVPISIN = txtPay.Text;
                mfProductAMCSchemePlanDetailsVo.DVRISIN = TextINV.Text;

            }
            else if (ddlDFrequency.SelectedValue == "DVR" && rbtnBSE.Checked == true)
            {
                mfProductAMCSchemePlanDetailsVo.DVRISIN = TextINV.Text;
            }
            else if (ddlDFrequency.SelectedValue == "DVP" && rbtnBSE.Checked == true)
            {
                mfProductAMCSchemePlanDetailsVo.DVPISIN = txtPay.Text;
            }
            mfProductAMCSchemePlanDetailsVo.RTA = (rbtnRTA.Checked) ? 1 : 0;
            mfProductAMCSchemePlanDetailsVo.BSE = (rbtnBSE.Checked) ? 1 : 0;
            mfProductAMCSchemePlanDetailsVo.DVPExternalCode = txtpayOutExtCode.Text;
            mfProductAMCSchemePlanDetailsVo.DVRExternalCode = txtReInv.Text;
        }

        private void ControlViewEditMode(bool isViewMode)
        {
            if (isViewMode)
            {
                txtMaturityDate.Enabled = false;
                txtSchemeStartDate.Enabled = false;
                ddlProduct.Enabled = false;
                txtRedemptionmultiple.Enabled = false;
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
                btnsubmit.Visible = false;
                txtScname.Visible = true;
                lblScname.Visible = true;
                ddlSchemeList.Visible = false;
                Label4.Visible = false;
                txtSwitchMultipleAmount.Enabled = false;
                txtSwitchMultipleUnits.Enabled = false;
                txtNFOStartDate.Enabled = false;
                txtNFOendDate.Enabled = false;
                ChkNRI.Enabled = false;
                ChkBO.Enabled = false;
                chkonline.Enabled = false;
                btnBasicDSubmit.Visible = false;
                btnBasicDupdate.Visible = false;
                lnkEdit.Visible = true;
                txtProductCode.Enabled = false;
                btnupdate.Visible = false;
                chkIsETFT.Enabled = false;
                chkBSE.Enabled = false;
                chkRTA.Enabled = false;
            }
            else
            {
                chkonline.Enabled = true;
                ddlNFoStatus.Items[3].Enabled = true;
                btnBasicDSubmit.Visible = false;
                btnBasicDupdate.Visible = true;
                Label4.Visible = false;
                ddlSchemeList.Visible = false;
                txtScname.Visible = true;
                lblScname.Visible = true;
                txtScname.Enabled = true;
                txtAMFI.Enabled = true;
                ddlAmc.Enabled = true;
                ddlcategory.Enabled = true;
                ddlProduct.Enabled = false;
                ddlScategory.Enabled = true;
                ddlSScategory.Enabled = true;
                ddlRT.Enabled = false;
                tblMessage.Visible = false;
                lbBack.Visible = false;
                txtProductCode.Enabled = true;
                ddlNFoStatus.Enabled = true;
                txtNFOendDate.Enabled = true;
                txtNFOStartDate.Enabled = false;
                txtSchemeStartDate.Enabled = true;
                txtMaturityDate.Enabled = true;
                radwindowPopup.VisibleOnPageLoad = false;
                ChkISSIP.Enabled = true;
                txtISIN.Enabled = true;

                chkBSE.Enabled = true;
                chkRTA.Enabled = true;

            }

        }
        protected void ViewSchemeDetails()
        {
            mfProductAMCSchemePlanDetailsVo = (MFProductAMCSchemePlanDetailsVo)Session["SchemeList"];
            //  Bindscheme(schemeplancode);
            lblschemeplanecodetext.Text = mfProductAMCSchemePlanDetailsVo.SchemePlanCode.ToString();
            txtScname.Text = mfProductAMCSchemePlanDetailsVo.SchemePlanName;
            txtESSchemecode.Text = mfProductAMCSchemePlanDetailsVo.ExternalCode;
            txtProductCode.Text = mfProductAMCSchemePlanDetailsVo.productcode;
            txtAMFI.Text = mfProductAMCSchemePlanDetailsVo.AMFIcode;
            txtISIN.Text = mfProductAMCSchemePlanDetailsVo.ISINNo;
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.Allproductcode))
            {
                lnkProductcode.Text = mfProductAMCSchemePlanDetailsVo.Allproductcode;
            }

            if (mfProductAMCSchemePlanDetailsVo.Mergecode != 0)
            {
                lnkMargeScheme.Text = "Merged Scheme";
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.GenerationFrequency))
            {
                ddlGenerationfreq.SelectedValue = mfProductAMCSchemePlanDetailsVo.GenerationFrequency.ToString();

            }
            else
            {
                ddlGenerationfreq.SelectedValue = "0";
            }
            if (mfProductAMCSchemePlanDetailsVo.AMCCode != 0)
            {

                ddlAmc.SelectedValue = mfProductAMCSchemePlanDetailsVo.AMCCode.ToString();
            }



            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AssetCategoryCode))
            {
                ddlcategory.SelectedValue = mfProductAMCSchemePlanDetailsVo.AssetCategoryCode.ToString();
            }
            else
            {
                ddlcategory.SelectedValue = "0";
            }

            LoadAllSchemeList(Convert.ToInt32(ddlAmc.SelectedValue));

            if (mfProductAMCSchemePlanDetailsVo.SchemePlanCode != 0)
            {
                ddlSchemeList.SelectedValue = mfProductAMCSchemePlanDetailsVo.SchemePlanCode.ToString();
            }
            else
            {
                ddlSchemeList.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.SourceCode))
            {
                ddlRT.SelectedValue = mfProductAMCSchemePlanDetailsVo.SourceCode;
            }
            else
            {
                ddlRT.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.ExternalType))
            {
                ddlRT.SelectedItem.Text = mfProductAMCSchemePlanDetailsVo.ExternalType.ToUpper();
            }
            else
            {
                ddlRT.SelectedItem.Text = "0";
            }

            txtFvale.Text = mfProductAMCSchemePlanDetailsVo.FaceValue.ToString();
            BindSubCategory(ddlcategory.SelectedValue);
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode))
            {
                ddlScategory.SelectedValue = mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode.ToString();
            }
            else
            {
                ddlScategory.SelectedValue = "0";
            }
            BindSubSubCategory(ddlcategory.SelectedValue, ddlScategory.SelectedValue);
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory))
            {
                ddlSScategory.SelectedValue = mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory.ToString();
            }
            else
            {
                ddlSScategory.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.SchemeType))
            {
                ddlSctype.Enabled = true;
                ddlSctype.SelectedValue = mfProductAMCSchemePlanDetailsVo.SchemeType.ToString();
                ddlSctype.Enabled = false;
            }
            else
            {
                ddlSctype.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.SchemeOption))
            {
                ddlOption.SelectedValue = mfProductAMCSchemePlanDetailsVo.SchemeOption.ToString();

                if (ddlOption.SelectedValue == "DV")
                {
                    BindSchemeLoockUpType();
                    ddlDFrequency.Visible = true;
                    lblddlDFrequency.Visible = true;

                    if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.DividendFrequency))
                    {
                        ddlDFrequency.SelectedValue = mfProductAMCSchemePlanDetailsVo.DividendFrequency.ToString();
                    }
                    else
                    {
                        ddlDFrequency.Visible = true;
                    }
                    if (ddlDFrequency.SelectedValue == "BT" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                    {
                        trISISN.Visible = true;
                        tdlblINV.Visible = true;
                        tdTextINV.Visible = true;
                        tdlblpay.Visible = true;
                        tdtxtpay.Visible = true;
                        trDivReInv.Visible = true;
                        tdlblDInv.Visible = true;
                        tdtxtDInv.Visible = true;
                        tdPayOurExternalCode.Visible = true;
                        tdtxtPayExternalCode.Visible = true;
                        txtpayOutExtCode.Text = mfProductAMCSchemePlanDetailsVo.DVPExternalCode;
                        txtPay.Text = mfProductAMCSchemePlanDetailsVo.DVPISIN;
                        TextINV.Text = mfProductAMCSchemePlanDetailsVo.DVRISIN;
                        txtReInv.Text = mfProductAMCSchemePlanDetailsVo.DVRExternalCode;
                    }
                    else if (ddlDFrequency.SelectedValue == "DVR" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                    {
                        trDivReInv.Visible = true;
                        tdlblINV.Visible = true;
                        tdTextINV.Visible = true;
                        tdlblDInv.Visible = false;
                        tdtxtDInv.Visible = false;
                        TextINV.Text = mfProductAMCSchemePlanDetailsVo.DVRISIN;
                        txtReInv.Text = mfProductAMCSchemePlanDetailsVo.DVRExternalCode;
                    }
                    else if (ddlDFrequency.SelectedValue == "DVP" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                    {
                        trISISN.Visible = true;
                        tdlblpay.Visible = true;
                        tdtxtpay.Visible = true;
                        tdPayOurExternalCode.Visible = false;
                        tdtxtPayExternalCode.Visible = false;
                        txtpayOutExtCode.Text = mfProductAMCSchemePlanDetailsVo.DVPExternalCode;
                        txtPay.Text = mfProductAMCSchemePlanDetailsVo.DVPISIN;

                    }
                }
                else if (ddlOption.SelectedValue == "GR" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                {
                    trISISN.Visible = true;
                    tdlblpay.Visible = true;
                    tdtxtpay.Visible = true;
                    txtESSchemecode.Visible = true;
                    lblESSchemecode.Visible = true;
                    txtPay.Text = mfProductAMCSchemePlanDetailsVo.DVPISIN;
                }

            }

            else
            {
                ddlOption.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId.ToString()))
            {
                ddlBname.SelectedValue = mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId.ToString();
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.Branch))
            {
                txtBranch.Text = mfProductAMCSchemePlanDetailsVo.Branch.ToString();
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AccountNumber))
            {
                txtACno.Text = mfProductAMCSchemePlanDetailsVo.AccountNumber.ToString();
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AMFIcode))
            {
                txtAMFI.Text = mfProductAMCSchemePlanDetailsVo.AMFIcode.ToString();
            }
            if (mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode == "NRI")
            {
                ChkNRI.Checked = true;
            }
            if (mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode == "IOTH")
            {
                ChkBO.Checked = true;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsOnline == 1)
            {
                chkonline.Checked = true;
                //schemedetails.Visible = true;

            }
            else
            {
                chkonline.Checked = false;
                //schemedetails.Visible = false;

            }
            if (mfProductAMCSchemePlanDetailsVo.IsETFT == 1)
            {
                chkIsETFT.Checked = true;
                ddlOption.Items[1].Enabled = false;
            }
            else
            {
                chkIsETFT.Checked = false;
                ddlOption.Items[1].Enabled = true;
            }

            if (mfProductAMCSchemePlanDetailsVo.IsNFO == 1)
            {
                chkInfo.Checked = true;
            }
            else
            {
                chkInfo.Checked = false;
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.Status))
            {
                ddlNFoStatus.SelectedValue = mfProductAMCSchemePlanDetailsVo.Status.ToString();
                if (ddlNFoStatus.SelectedValue == "NFO")
                {
                    trNFODate.Visible = true;
                }
                if (ddlNFoStatus.SelectedValue == "CloseNFO")
                {
                    ddlNFoStatus.Items[5].Enabled = true;
                }
                if (ddlNFoStatus.SelectedValue == "Merged")
                {
                    ddlNFoStatus.Items[4].Enabled = true;
                    lbBack.Enabled = false;
                    lnkEdit.Enabled = false;
                }
            }
            else
            {
                ddlNFoStatus.SelectedValue = "0";
            }
            if (mfProductAMCSchemePlanDetailsVo.NFOStartDate != DateTime.MinValue)
            {
                txtNFOStartDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.NFOStartDate;
                if (txtNFOStartDate.SelectedDate <= DateTime.Now)
                {
                    txtNFOendDate.Enabled = false;
                }
            }
            if (mfProductAMCSchemePlanDetailsVo.NFOEndDate != DateTime.MinValue)
            {
                txtNFOendDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.NFOEndDate;
            }
            if (mfProductAMCSchemePlanDetailsVo.SchemeStartDate != DateTime.MinValue)
            {
                txtSchemeStartDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.SchemeStartDate;
            }
            if (mfProductAMCSchemePlanDetailsVo.MaturityDate != DateTime.MinValue)
            {
                txtMaturityDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.MaturityDate;
            }
            txtLIperiod.Text = mfProductAMCSchemePlanDetailsVo.LockInPeriod.ToString();
            string Time = (txtHH.Text + ':' + txtMM.Text + ':' + txtSS.Text);
            Time = Convert.ToString(mfProductAMCSchemePlanDetailsVo.CutOffTime).ToString();
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
            //lnkProductcode.Text = mfProductAMCSchemePlanDetailsVo.Allproductcode;
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag.ToString()))
            {
                txtEload.Text = mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag.ToString();
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.EntryLoadRemark))
                txtELremark.Text = mfProductAMCSchemePlanDetailsVo.EntryLoadRemark.ToString();
            txtExitLoad.Text = mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage.ToString();
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.ExitLoadRemark))
                txtExitLremark.Text = mfProductAMCSchemePlanDetailsVo.ExitLoadRemark.ToString();
            if (mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable == 1)
            {
                ChkISPurchage.Checked = true;
                trIPAmount.Visible = true;
            }
            else
            {
                ChkISPurchage.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable == 1)
            {
                ChkISRedeem.Checked = true;
                trMINRedemPtion.Visible = true;
            }

            else
            {
                ChkISRedeem.Checked = false;

            }
            if (mfProductAMCSchemePlanDetailsVo.IsSIPAvailable == 1)
            {
                ChkISSIP.Checked = true;
                gvSIPDetails.Visible = true;
                pnlSIPDetails.Visible = true;
                BindSystematicDetails();
            }
            else
            {
                ChkISSIP.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsSTPAvailable == 1)
            {
                ChkISSTP.Checked = true;
                gvSIPDetails.Visible = true;
                pnlSIPDetails.Visible = true;
                BindSystematicDetails();
            }
            else
            {
                ChkISSTP.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable == 1)
            {
                ChkISSwitch.Checked = true;
                trSwitchPavailable.Visible = true;

            }
            else
            {
                ChkISSwitch.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsSWPAvailable == 1)
            {
                ChkISSWP.Checked = true;
                gvSIPDetails.Visible = true;
                pnlSIPDetails.Visible = true;
                BindSystematicDetails();
            }
            else
            {
                ChkISSWP.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement == 1)
            {
                chkOnlineEnablement.Checked = true;

            }
            else
            {
                chkOnlineEnablement.Checked = false;
            }

            txtInitalPamount.Text = mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount.ToString();
            txtIMultipleamount.Text = mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount.ToString();
            txtAdditional.Text = mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount.ToString();
            txtAddMultipleamount.Text = mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount.ToString();
            txtMinRedemption.Text = mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount.ToString();
            txtMinRedemptioUnits.Text = mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits.ToString();
            txtMinSwitchAmount.Text = mfProductAMCSchemePlanDetailsVo.MinSwitchAmount.ToString();
            txtMinSwitchUnits.Text = mfProductAMCSchemePlanDetailsVo.MinSwitchUnits.ToString();
            txtRedemptionmultiple.Text = mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount.ToString();
            txtRedemptionMultiplesUnits.Text = mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits.ToString();
            txtSwitchMultipleAmount.Text = mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount.ToString();
            txtSwitchMultipleUnits.Text = mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits.ToString();
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.SecurityCode))
                txtSecuritycode.Text = mfProductAMCSchemePlanDetailsVo.SecurityCode.ToString();
            txtinvestment.Text = mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment.ToString();

            rbtnBSE.Checked = (mfProductAMCSchemePlanDetailsVo.BSE == 1) ? true : false;

            rbtnRTA.Checked = (mfProductAMCSchemePlanDetailsVo.RTA == 1) ? true : false;

            chkRTA.Checked = (mfProductAMCSchemePlanDetailsVo.BRTA == 1) ? true : false;

            chkBSE.Checked = (mfProductAMCSchemePlanDetailsVo.BBSE == 1) ? true : false;

        }




        protected void EditSchemeDetails()
        {
            mfProductAMCSchemePlanDetailsVo = (MFProductAMCSchemePlanDetailsVo)Session["SchemeList"];
            // Bindscheme(schemeplancode);
            lblschemeplanecodetext.Text = mfProductAMCSchemePlanDetailsVo.SchemePlanCode.ToString();
            txtScname.Text = mfProductAMCSchemePlanDetailsVo.SchemePlanName;
            txtESSchemecode.Text = mfProductAMCSchemePlanDetailsVo.ExternalCode;
            txtProductCode.Text = mfProductAMCSchemePlanDetailsVo.productcode;
            txtISIN.Text = mfProductAMCSchemePlanDetailsVo.ISINNo;
            txtAMFI.Text = mfProductAMCSchemePlanDetailsVo.AMFIcode;
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.Allproductcode))
            {
                lnkProductcode.Text = mfProductAMCSchemePlanDetailsVo.Allproductcode;
            }

            if (mfProductAMCSchemePlanDetailsVo.Mergecode != 0)
            {
                lnkMargeScheme.Text = "Merged Scheme";
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.GenerationFrequency))
            {
                ddlGenerationfreq.SelectedValue = mfProductAMCSchemePlanDetailsVo.GenerationFrequency.ToString();

            }
            else
            {
                ddlGenerationfreq.SelectedValue = "0";
            }
            if (mfProductAMCSchemePlanDetailsVo.AMCCode != 0)
            {

                ddlAmc.SelectedValue = mfProductAMCSchemePlanDetailsVo.AMCCode.ToString();
            }
            else
            {
                ddlAmc.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AssetCategoryCode))
            {
                ddlcategory.SelectedValue = mfProductAMCSchemePlanDetailsVo.AssetCategoryCode.ToString();
            }
            else
            {
                ddlcategory.SelectedValue = "0";
            }

            LoadAllSchemeList(Convert.ToInt32(ddlAmc.SelectedValue));

            if (mfProductAMCSchemePlanDetailsVo.SchemePlanCode != 0)
            {
                ddlSchemeList.SelectedValue = mfProductAMCSchemePlanDetailsVo.SchemePlanCode.ToString();
            }
            else
            {
                ddlSchemeList.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.ExternalType))
            {
                ddlRT.SelectedItem.Text = mfProductAMCSchemePlanDetailsVo.ExternalType.ToString().ToUpper();
            }
            else
            {
                ddlRT.SelectedValue = "0";
            }
            txtFvale.Text = mfProductAMCSchemePlanDetailsVo.FaceValue.ToString();
            BindSubCategory(ddlcategory.SelectedValue);
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode))
            {
                ddlScategory.SelectedValue = mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode.ToString();
            }
            else
            {
                ddlScategory.SelectedValue = "0";
            }
            BindSubSubCategory(ddlcategory.SelectedValue, ddlScategory.SelectedValue);
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory))
            {
                ddlSScategory.SelectedValue = mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory.ToString();
            }
            else
            {
                ddlSScategory.SelectedValue = "0";
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.SchemeType))
            {
                ddlSctype.Enabled = true;
                ddlSctype.SelectedValue = mfProductAMCSchemePlanDetailsVo.SchemeType.ToString();
                ddlSctype.Enabled = false;
            }
            else
            {
                ddlSctype.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.SchemeOption))
            {
                ddlOption.SelectedValue = mfProductAMCSchemePlanDetailsVo.SchemeOption.ToString();
                if (ddlOption.SelectedValue == "DV")
                {
                    BindSchemeLoockUpType();
                    ddlDFrequency.Visible = true;
                    lblddlDFrequency.Visible = true;
                    if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.DividendFrequency))
                    {
                        ddlDFrequency.SelectedValue = mfProductAMCSchemePlanDetailsVo.DividendFrequency.ToString();
                    }

                    else
                    {
                        ddlDFrequency.Visible = true;
                    }
                    if (ddlDFrequency.SelectedValue == "BT" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                    {
                        trISISN.Visible = true;
                        tdlblINV.Visible = true;
                        tdTextINV.Visible = true;
                        tdlblpay.Visible = true;
                        tdtxtpay.Visible = true;
                        trDivReInv.Visible = true;
                        tdlblDInv.Visible = true;
                        tdtxtDInv.Visible = true;
                        tdPayOurExternalCode.Visible = true;
                        tdtxtPayExternalCode.Visible = true;
                        txtpayOutExtCode.Text = mfProductAMCSchemePlanDetailsVo.DVPExternalCode;
                        txtPay.Text = mfProductAMCSchemePlanDetailsVo.DVPISIN;
                        TextINV.Text = mfProductAMCSchemePlanDetailsVo.DVRISIN;
                        txtReInv.Text = mfProductAMCSchemePlanDetailsVo.DVRExternalCode;
                        txtESSchemecode.Visible = false;
                        lblESSchemecode.Visible = false;
                    }
                    else if (ddlDFrequency.SelectedValue == "DVR" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                    {
                        trDivReInv.Visible = true;
                        tdlblINV.Visible = true;
                        tdTextINV.Visible = true;
                        tdlblDInv.Visible = false;
                        tdtxtDInv.Visible = false;
                        TextINV.Text = mfProductAMCSchemePlanDetailsVo.DVRISIN;
                        txtReInv.Text = mfProductAMCSchemePlanDetailsVo.DVRExternalCode;
                        txtESSchemecode.Visible = false;
                        lblESSchemecode.Visible = false;
                    }
                    else if (ddlDFrequency.SelectedValue == "DVP" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                    {
                        trISISN.Visible = true;
                        tdlblpay.Visible = true;
                        tdtxtpay.Visible = true;
                        tdPayOurExternalCode.Visible = false;
                        tdtxtPayExternalCode.Visible = false;
                        txtpayOutExtCode.Text = mfProductAMCSchemePlanDetailsVo.DVPExternalCode;
                        txtPay.Text = mfProductAMCSchemePlanDetailsVo.DVPISIN;
                        txtESSchemecode.Visible = false;
                        lblESSchemecode.Visible = false;

                    }
                    else if (ddlOption.SelectedValue == "GR" && mfProductAMCSchemePlanDetailsVo.BSE == 1)
                    {
                        trISISN.Visible = true;
                        tdlblpay.Visible = true;
                        tdtxtpay.Visible = true;
                        txtESSchemecode.Visible = true;
                        lblESSchemecode.Visible = true;
                    }
                }
            }
            else
            {
                ddlOption.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId.ToString()))
            {
                ddlBname.SelectedValue = mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId.ToString();
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.Branch))
            {
                txtBranch.Text = mfProductAMCSchemePlanDetailsVo.Branch.ToString();
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.AccountNumber))
            {
                txtACno.Text = mfProductAMCSchemePlanDetailsVo.AccountNumber.ToString();
            }
            if (mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode == "IND")
            {
                ChkNRI.Checked = true;
            }
            if (mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode == "NIND")
            {
                ChkBO.Checked = true;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsOnline != 1)
            {
                chkonline.Checked = false;
                btnupdate.Visible = false;
            }
            else
            {
                chkonline.Checked = true;
                btnupdate.Visible = true;
            }


            if (mfProductAMCSchemePlanDetailsVo.IsNFO == 1)
            {
                chkInfo.Checked = true;
            }
            else
            {
                chkInfo.Checked = false;
            }

            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.Status))
            {
                ddlNFoStatus.SelectedValue = mfProductAMCSchemePlanDetailsVo.Status.ToString();
                if (ddlNFoStatus.SelectedValue == "NFO")
                {
                    trNFODate.Visible = true;
                }
                if (ddlNFoStatus.SelectedValue == "CloseNFO")
                {
                    ddlNFoStatus.Items[5].Enabled = true;
                }
                if (ddlNFoStatus.SelectedValue == "Merged")
                {
                    ddlNFoStatus.Items[4].Enabled = true;
                }
            }
            else
            {
                ddlNFoStatus.SelectedItem.Text = "Select";
            }
            if (mfProductAMCSchemePlanDetailsVo.NFOStartDate != DateTime.MinValue)
            {
                txtNFOStartDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.NFOStartDate;
                if (txtNFOStartDate.SelectedDate <= DateTime.Now)
                {
                    txtNFOendDate.Enabled = false;
                }
            }
            if (mfProductAMCSchemePlanDetailsVo.NFOEndDate != DateTime.MinValue)
            {
                txtNFOendDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.NFOEndDate;
            }
            txtLIperiod.Text = mfProductAMCSchemePlanDetailsVo.LockInPeriod.ToString();
            string Time = (txtHH.Text + ':' + txtMM.Text + ':' + txtSS.Text);
            Time = Convert.ToString(mfProductAMCSchemePlanDetailsVo.CutOffTime).ToString();
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
            if (mfProductAMCSchemePlanDetailsVo.SchemeStartDate != DateTime.MinValue)
            {
                txtSchemeStartDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.SchemeStartDate;
            }
            if (mfProductAMCSchemePlanDetailsVo.MaturityDate != DateTime.MinValue)
            {
                txtMaturityDate.SelectedDate = mfProductAMCSchemePlanDetailsVo.MaturityDate;
            }
            txtEload.Text = mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag.ToString();
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.EntryLoadRemark))
                txtELremark.Text = mfProductAMCSchemePlanDetailsVo.EntryLoadRemark.ToString();
            txtExitLoad.Text = mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage.ToString();
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.ExitLoadRemark))
                txtExitLremark.Text = mfProductAMCSchemePlanDetailsVo.ExitLoadRemark.ToString();
            if (mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable == 1)
            {
                ChkISPurchage.Checked = true;
                trIPAmount.Visible = true;
            }
            else
            {
                ChkISPurchage.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable == 1)
            {
                ChkISRedeem.Checked = true;
                trMINRedemPtion.Visible = true;
            }

            else
            {
                ChkISRedeem.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsSIPAvailable == 1)
            {
                ChkISSIP.Checked = true;
                gvSIPDetails.Visible = true;
                BindSystematicDetails();
            }
            else
            {
                ChkISSIP.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsSTPAvailable == 1)
            {
                ChkISSTP.Checked = true;
                gvSIPDetails.Visible = true;
                BindSystematicDetails();
            }
            else
            {
                ChkISSTP.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable == 1)
            {
                ChkISSwitch.Checked = true;
                trSwitchPavailable.Visible = true;
            }
            else
            {
                ChkISSwitch.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsSWPAvailable == 1)
            {
                ChkISSWP.Checked = true;
                gvSIPDetails.Visible = true;
                BindSystematicDetails();
            }
            else
            {
                ChkISSWP.Checked = false;
            }
            if (mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement == 1)
            {
                chkOnlineEnablement.Checked = true;

            }
            else
            {
                chkOnlineEnablement.Checked = false;
            }
            txtSwitchMultipleAmount.Text = mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount.ToString();
            txtRedemptionmultiple.Text = mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount.ToString();
            txtSwitchMultipleUnits.Text = mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits.ToString();
            txtInitalPamount.Text = mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount.ToString();
            txtIMultipleamount.Text = mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount.ToString();
            txtAdditional.Text = mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount.ToString();
            txtAddMultipleamount.Text = mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount.ToString();
            txtMinRedemption.Text = mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount.ToString();
            txtMinRedemptioUnits.Text = mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits.ToString();
            txtMinSwitchAmount.Text = mfProductAMCSchemePlanDetailsVo.MinSwitchAmount.ToString();
            txtMinSwitchUnits.Text = mfProductAMCSchemePlanDetailsVo.MinSwitchUnits.ToString();
            txtRedemptionMultiplesUnits.Text = mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits.ToString();
            if (mfProductAMCSchemePlanDetailsVo.IsETFT == 1)
            {
                chkIsETFT.Checked = true;
                ddlOption.Items[1].Enabled = false;
            }
            else
            {
                chkIsETFT.Checked = false;
                ddlOption.Items[1].Enabled = true;
            }
            if (!string.IsNullOrEmpty(mfProductAMCSchemePlanDetailsVo.SecurityCode))
                txtSecuritycode.Text = mfProductAMCSchemePlanDetailsVo.SecurityCode.ToString();
            txtinvestment.Text = mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment.ToString();
            // btnupdate.Visible = true;
            txtESSchemecode.Enabled = false;
            rbtnBSE.Checked = (mfProductAMCSchemePlanDetailsVo.BSE == 1) ? true : false;

            rbtnRTA.Checked = (mfProductAMCSchemePlanDetailsVo.RTA == 1) ? true : false;

            chkRTA.Checked = (mfProductAMCSchemePlanDetailsVo.BRTA == 1) ? true : false;

            chkBSE.Checked = (mfProductAMCSchemePlanDetailsVo.BBSE == 1) ? true : false;


        }
        protected void Clearallcontrols(bool basicsubmit)
        {
            if (basicsubmit)
            {
                ddlProduct.Enabled = false;
                txtScname.Enabled = false;
                txtAMFI.Enabled = false;
                ddlAmc.Enabled = false;
                ddlcategory.Enabled = false;
                ddlScategory.Enabled = false;
                ddlSScategory.Enabled = false;
                chkonline.Enabled = false;
                //ChkISactive.Enabled = false;
                txtProductCode.Enabled = false;
                ddlRT.Enabled = false;
                ddlMargeScheme.Enabled = false;
                ddlNFoStatus.Enabled = false;
                txtNFOStartDate.Enabled = false;
                txtNFOendDate.Enabled = false;
                txtMaturityDate.Enabled = false;
                txtSchemeStartDate.Enabled = false;
                txtISIN.Enabled = false;
                chkBSE.Enabled = false;
                chkRTA.Enabled = false;
            }
            else
            {
                chkIsETFT.Enabled = false;
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
                txtFvale.Enabled = false;
                ChkNRI.Enabled = false;
                ChkBO.Enabled = false;
                txtELremark.Enabled = false;
                txtExitLoad.Enabled = false;
                txtExitLremark.Enabled = false;
                ChkISPurchage.Enabled = false;
                ChkISRedeem.Enabled = false;
                ChkISSIP.Enabled = true;
                ChkISSTP.Enabled = false;
                ChkISSwitch.Enabled = false;
                ChkISSWP.Enabled = false;
                ddlSchemeList.Enabled = false;
                chkOnlineEnablement.Enabled = false;
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
                lblddlDFrequency.Visible = false;
                btnsubmit.Visible = false;
                btnupdate.Visible = true;
                txtRedemptionmultiple.Enabled = false;


            }
        }
        protected void lbBack_OnClick(object sender, EventArgs e)
        {
            ControlViewEditMode(false);
        }
        protected void ControlEditSubmit()
        {
            lnkEdit.Visible = false;
            ChkNRI.Enabled = true;
            ChkBO.Enabled = true;
            chkOnlineEnablement.Enabled = true;
            txtSwitchMultipleUnits.Enabled = true;
            txtSwitchMultipleAmount.Enabled = true;
            txtRedemptionmultiple.Enabled = true;
            txtFvale.Enabled = true;
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
            ddlSchemeList.Enabled = false;
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
            txtinvestment.Enabled = true;
            txtESSchemecode.Enabled = true;
            ddlGenerationfreq.Enabled = true;
            ddlDFrequency.Enabled = true;
            ddlSctype.Enabled = true;

            chkIsETFT.Enabled = true;
            txtPay.Enabled = true;
            TextINV.Enabled = true;
            txtReInv.Enabled = true;

            rbtnBSE.Enabled = true;
            rbtnRTA.Enabled = true;
        }
        protected void lnkEdit_OnClick(object sender, EventArgs e)
        {
            ControlEditSubmit();
            btnsubmit.Visible = false;
            btnupdate.Visible = true;
        }
        protected void ddlSchemeList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeList.SelectedIndex != 0)
            {

                int schemepalncode = int.Parse(ddlSchemeList.SelectedValue);

                ViewState["Schemecode"] = schemepalncode;
                hdnSchemePlanCode.Value = schemepalncode.ToString();
                Bindscheme(schemepalncode);
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
        protected void btnBasicDSubmit_click(object sender, EventArgs e)
        {

            mfProductAMCSchemePlanDetailsVo.Product = ddlProduct.SelectedValue;


            mfProductAMCSchemePlanDetailsVo.IsOnline = chkonline.Checked ? 1 : 0;

            mfProductAMCSchemePlanDetailsVo.BBSE = chkBSE.Checked ? 1 : 0;

            mfProductAMCSchemePlanDetailsVo.BRTA = chkRTA.Checked ? 1 : 0;
            mfProductAMCSchemePlanDetailsVo.AMCCode = (!string.IsNullOrEmpty(ddlAmc.SelectedValue)) ? int.Parse(ddlAmc.SelectedValue) : 0;
            mfProductAMCSchemePlanDetailsVo.Status = (!string.IsNullOrEmpty(ddlNFoStatus.SelectedValue)) ? ddlNFoStatus.SelectedValue : "0";
            if (txtNFOStartDate.SelectedDate.ToString() != null && txtNFOStartDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.NFOStartDate = DateTime.Parse(txtNFOStartDate.SelectedDate.ToString());
            }
            if (txtNFOendDate.SelectedDate.ToString() != null && txtNFOendDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.NFOEndDate = DateTime.Parse(txtNFOendDate.SelectedDate.ToString());
            }
            if (txtSchemeStartDate.SelectedDate.ToString() != null && txtSchemeStartDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.SchemeStartDate = DateTime.Parse(txtSchemeStartDate.SelectedDate.ToString());
            }
            if (txtMaturityDate.SelectedDate.ToString() != null && txtMaturityDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.MaturityDate = DateTime.Parse(txtMaturityDate.SelectedDate.ToString());
            }
            mfProductAMCSchemePlanDetailsVo.SchemePlanName = txtScname.Text;
            mfProductAMCSchemePlanDetailsVo.AssetCategoryCode = ddlcategory.SelectedValue;
            mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode = ddlScategory.SelectedValue;
            mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory = ddlSScategory.SelectedValue;
            mfProductAMCSchemePlanDetailsVo.SourceCode = ddlRT.SelectedValue;
            mfProductAMCSchemePlanDetailsVo.productcode = txtProductCode.Text;
            mfProductAMCSchemePlanDetailsVo.AMFIcode = txtAMFI.Text;
            if (!string.IsNullOrEmpty(txtISIN.Text))
                mfProductAMCSchemePlanDetailsVo.ISINNo = txtISIN.Text;


            mfProductAMCSchemePlanDetailsVo.ExternalType = ddlRT.SelectedItem.Text;
            if (txtNFOStartDate.SelectedDate != null)
            {
                int countNFOstart = OnlineOrderBackOfficeBo.BussinessDateCheck(Convert.ToDateTime(txtNFOStartDate.SelectedDate));
                if (countNFOstart == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Select Bussiness Date For NFO Start!!');", true);
                    return;
                }
            }
            if (txtSchemeStartDate.SelectedDate != null)
            {
                int countSchemeOpen = OnlineOrderBackOfficeBo.BussinessDateCheck(Convert.ToDateTime(txtSchemeStartDate.SelectedDate));
                if (countSchemeOpen == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Select Bussiness Date For Scheme Start Date!!');", true);
                    return;
                }
            }
            if (txtNFOendDate.SelectedDate != null && ddlNFoStatus.SelectedValue == "NFO")
            {
                int count = OnlineOrderBackOfficeBo.BussinessDateCheck(Convert.ToDateTime(txtNFOendDate.SelectedDate));
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Select Bussiness Date For NFO End!!');", true);
                }

                else
                {
                    OnlineOrderBackOfficeBo.CreateOnlineSchemeSetupPlan(mfProductAMCSchemePlanDetailsVo, userVo.UserId, ref  schemeplancode);
                    Session["newschemeplancode"] = schemeplancode;
                    lbBack.Visible = true;
                    btnBasicDSubmit.Visible = false;
                    btnsubmit.Visible = true;
                    lblAllproductcode.Visible = true;
                    lnkProductcode.Visible = true;
                    Clearallcontrols(true);
                }
            }
            else
            {
                OnlineOrderBackOfficeBo.CreateOnlineSchemeSetupPlan(mfProductAMCSchemePlanDetailsVo, userVo.UserId, ref  schemeplancode);
                Session["newschemeplancode"] = schemeplancode;
                lbBack.Visible = true;
                btnBasicDSubmit.Visible = false;
                schemedetails.Visible = true;
                btnsubmit.Visible = true;
                lblAllproductcode.Visible = true;
                lnkProductcode.Visible = true;
                Clearallcontrols(true);
            }
        }
        protected void btnsubmit_click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtESSchemecode.Text) && !string.IsNullOrEmpty(ddlAmc.SelectedValue) && chkBSE.Checked == false)
            {

                int count = OnlineOrderBackOfficeBo.SchemeCodeonline(txtESSchemecode.Text, int.Parse(ddlAmc.SelectedValue));
                if (count >= 1)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Unique External System Scheme Code.');", true);
                    return;
                }
                else
                {
                    if (count == 0)
                    {
                        SaveSchemeDetails();

                        OnlineOrderBackOfficeBo.CreateOnlineSchemeSetupPlanDetails(mfProductAMCSchemePlanDetailsVo, userVo.UserId);
                        string msg = "Scheme Submit Successfully!!";
                        ShowMessage(msg);
                        Clearallcontrols(false);
                        lnkEdit.Visible = true;
                        btnupdate.Visible = false;
                        lblddlDFrequency.Visible = false;
                    }
                }
            }
            else
            {
                SaveSchemeDetails();

                OnlineOrderBackOfficeBo.CreateOnlineSchemeSetupPlanDetails(mfProductAMCSchemePlanDetailsVo, userVo.UserId);
                string msg = "Scheme Submit Successfully!!";
                ShowMessage(msg);
                Clearallcontrols(false);
                lnkEdit.Visible = true;
                btnupdate.Visible = false;
                lblddlDFrequency.Visible = false;
            }
        }
        protected void oncheckedOnlin_OnCheckedChanged(object sender, EventArgs e)
        {
        }
        protected void btnBasicDupdate_click(object sender, EventArgs e)
        {

            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());

                }
            }
            else
            {
                mfProductAMCSchemePlanDetailsVo = new MFProductAMCSchemePlanDetailsVo();
                mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(Session["newschemeplancode"].ToString());
            }

            mfProductAMCSchemePlanDetailsVo.AMCCode = (!string.IsNullOrEmpty(ddlAmc.SelectedValue)) ? int.Parse(ddlAmc.SelectedValue) : 0;

            if (ddlRT.SelectedItem.Text != "")
            {
                mfProductAMCSchemePlanDetailsVo.SourceCode = OnlineOrderBackOfficeBo.ExternalCode(ddlRT.SelectedItem.Text);
            }

            mfProductAMCSchemePlanDetailsVo.Product = (!string.IsNullOrEmpty(ddlProduct.SelectedValue)) ? ddlProduct.SelectedValue : "0";


            mfProductAMCSchemePlanDetailsVo.SchemePlanName = (!string.IsNullOrEmpty(txtScname.Text)) ? txtScname.Text : "0";

            mfProductAMCSchemePlanDetailsVo.productcode = (!string.IsNullOrEmpty(txtProductCode.Text)) ? txtProductCode.Text : "0";

            mfProductAMCSchemePlanDetailsVo.AMFIcode = (!string.IsNullOrEmpty(txtAMFI.Text)) ? txtAMFI.Text : "0";
            mfProductAMCSchemePlanDetailsVo.ISINNo = (!string.IsNullOrEmpty(txtISIN.Text)) ? txtISIN.Text : "0";

            mfProductAMCSchemePlanDetailsVo.AssetCategoryCode = (!string.IsNullOrEmpty(ddlcategory.SelectedValue)) ? ddlcategory.SelectedValue : "0";
            mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode = (!string.IsNullOrEmpty(ddlScategory.SelectedValue)) ? ddlScategory.SelectedValue : "0";
            mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory = (!string.IsNullOrEmpty(ddlSScategory.SelectedValue)) ? ddlSScategory.SelectedValue : "0";
            mfProductAMCSchemePlanDetailsVo.ExternalType = ddlRT.SelectedItem.Text;
            if (chkonline.Checked == true)
            {
                mfProductAMCSchemePlanDetailsVo.IsOnline = 1;

                schemedetails.Visible = true;
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                {
                    if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                    {
                        btnsubmit.Visible = false;
                    }
                }
                else
                {
                    if (ddlOption.SelectedValue != "0")
                        btnsubmit.Visible = false;
                    else
                        btnsubmit.Visible = true; ;

                }
            }
            else
            {
                mfProductAMCSchemePlanDetailsVo.IsOnline = 0;
            }

            mfProductAMCSchemePlanDetailsVo.BBSE = (chkBSE.Checked) ? 1 : 0;


            mfProductAMCSchemePlanDetailsVo.BRTA = (chkRTA.Checked) ? 1 : 0;
            mfProductAMCSchemePlanDetailsVo.Status = (!string.IsNullOrEmpty(ddlNFoStatus.SelectedValue)) ? ddlNFoStatus.SelectedValue : "0";
            if (txtNFOStartDate.SelectedDate.ToString() != null && txtNFOStartDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.NFOStartDate = DateTime.Parse(txtNFOStartDate.SelectedDate.ToString());
            }
            if (txtNFOendDate.SelectedDate.ToString() != null && txtNFOendDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.NFOEndDate = DateTime.Parse(txtNFOendDate.SelectedDate.ToString());
            }
            if (txtSchemeStartDate.SelectedDate.ToString() != null && txtSchemeStartDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.SchemeStartDate = DateTime.Parse(txtSchemeStartDate.SelectedDate.ToString());
            }
            if (txtMaturityDate.SelectedDate.ToString() != null && txtMaturityDate.SelectedDate.ToString() != string.Empty)
            {
                mfProductAMCSchemePlanDetailsVo.MaturityDate = DateTime.Parse(txtMaturityDate.SelectedDate.ToString());
            }

            if (txtSchemeStartDate.SelectedDate != null)
            {
                int countSchemeOpen = OnlineOrderBackOfficeBo.BussinessDateCheck(Convert.ToDateTime(txtSchemeStartDate.SelectedDate));
                if (countSchemeOpen == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Select Bussiness Date For NFO RE-Open Date!!');", true);
                }
            }
            if (txtNFOStartDate.SelectedDate != null)
            {
                int countNFOstart = OnlineOrderBackOfficeBo.BussinessDateCheck(Convert.ToDateTime(txtNFOStartDate.SelectedDate));
                if (countNFOstart == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Select Bussiness Date For NFO Start!!');", true);
                    return;
                }
            }
            if (txtNFOendDate.SelectedDate != null && ddlNFoStatus.SelectedValue == "NFO")
            {
                int count = OnlineOrderBackOfficeBo.BussinessDateCheck(Convert.ToDateTime(txtNFOendDate.SelectedDate));
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Select Bussiness Date For NFO End!!');", true);
                }
                else
                {
                    bool bResult = OnlineOrderBackOfficeBo.Updateproductamcscheme(mfProductAMCSchemePlanDetailsVo, schemeplancode, userVo.UserId);
                    lbBack.Visible = true;
                    btnBasicDupdate.Visible = false;
                    lblAllproductcode.Visible = true;
                    lnkProductcode.Visible = true;
                    Clearallcontrols(true);
                }
            }
            else
            {
                bool bResult = OnlineOrderBackOfficeBo.Updateproductamcscheme(mfProductAMCSchemePlanDetailsVo, schemeplancode, userVo.UserId);
                lbBack.Visible = true;
                btnBasicDupdate.Visible = false;
                lblAllproductcode.Visible = true;
                lnkProductcode.Visible = true;
                Clearallcontrols(true);
            }
        }

        protected void btnUpdate_click(object sender, EventArgs e)
        {
            try
            {
                string message = string.Empty;
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                {
                    if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                    {
                        mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                    }
                }
                else
                {
                    mfProductAMCSchemePlanDetailsVo = new MFProductAMCSchemePlanDetailsVo();
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(Session["newschemeplancode"].ToString());//
                }

                if (ddlRT.SelectedItem.Text != "")
                {
                    mfProductAMCSchemePlanDetailsVo.SourceCode = OnlineOrderBackOfficeBo.ExternalCode(ddlRT.SelectedItem.Text);
                }

                if (!string.IsNullOrEmpty(ddlRT.SelectedItem.Text))
                {
                    mfProductAMCSchemePlanDetailsVo.ExternalType = ddlRT.SelectedItem.Text;
                }
                if (!string.IsNullOrEmpty(txtESSchemecode.Text))
                {
                    mfProductAMCSchemePlanDetailsVo.ExternalCode = txtESSchemecode.Text;
                }

                if (ddlBname.SelectedIndex != 0)
                {
                    mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId = int.Parse(ddlBname.SelectedValue.ToString());
                }

                mfProductAMCSchemePlanDetailsVo.SchemeType = (!string.IsNullOrEmpty(ddlSctype.SelectedValue)) ? ddlSctype.SelectedValue : "0";


                mfProductAMCSchemePlanDetailsVo.SchemeOption = (!string.IsNullOrEmpty(ddlOption.SelectedValue)) ? ddlOption.SelectedValue : "0";

                if (!string.IsNullOrEmpty(ddlDFrequency.SelectedValue))
                {
                    mfProductAMCSchemePlanDetailsVo.DividendFrequency = ddlDFrequency.SelectedValue;
                }

                mfProductAMCSchemePlanDetailsVo.AccountNumber = (!string.IsNullOrEmpty(txtACno.Text)) ? txtACno.Text : "0";

                mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount = (!string.IsNullOrEmpty(txtInitalPamount.Text)) ? Convert.ToDouble(txtInitalPamount.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount = (!string.IsNullOrEmpty(txtIMultipleamount.Text)) ? Convert.ToDouble(txtIMultipleamount.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount = (!string.IsNullOrEmpty(txtAdditional.Text)) ? Convert.ToDouble(txtAdditional.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount = (!string.IsNullOrEmpty(txtAddMultipleamount.Text)) ? Convert.ToDouble(txtAddMultipleamount.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount = (!string.IsNullOrEmpty(txtMinRedemption.Text)) ? Convert.ToDouble(txtMinRedemption.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount = (!string.IsNullOrEmpty(txtRedemptionmultiple.Text)) ? Convert.ToDouble(txtRedemptionmultiple.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits = (!string.IsNullOrEmpty(txtMinRedemptioUnits.Text)) ? Convert.ToDouble(txtMinRedemptioUnits.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits = (!string.IsNullOrEmpty(txtRedemptionMultiplesUnits.Text)) ? Convert.ToDouble(txtRedemptionMultiplesUnits.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.MinSwitchAmount = (!string.IsNullOrEmpty(txtMinSwitchAmount.Text)) ? Convert.ToDouble(txtMinSwitchAmount.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount = (!string.IsNullOrEmpty(txtSwitchMultipleAmount.Text)) ? Convert.ToDouble(txtSwitchMultipleAmount.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits = (!string.IsNullOrEmpty(txtSwitchMultipleUnits.Text)) ? Convert.ToInt32(txtSwitchMultipleUnits.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.MinSwitchUnits = (!string.IsNullOrEmpty(txtMinSwitchUnits.Text)) ? Convert.ToInt32(txtMinSwitchUnits.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment = (!string.IsNullOrEmpty(txtinvestment.Text)) ? Convert.ToDouble(txtinvestment.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.FaceValue = (!string.IsNullOrEmpty(txtFvale.Text)) ? Convert.ToDouble(txtFvale.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.LockInPeriod = (!string.IsNullOrEmpty(txtLIperiod.Text)) ? Convert.ToInt32(txtLIperiod.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag = (!string.IsNullOrEmpty(txtEload.Text)) ? Convert.ToDouble(txtEload.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.ExitLoadRemark = (!string.IsNullOrEmpty(txtExitLremark.Text)) ? txtExitLremark.Text : "0";

                mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage = (!string.IsNullOrEmpty(txtExitLoad.Text)) ? Convert.ToDouble(txtExitLoad.Text.ToString()) : 0;

                mfProductAMCSchemePlanDetailsVo.EntryLoadRemark = (!string.IsNullOrEmpty(txtELremark.Text)) ? txtELremark.Text : "0";

                mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable = (ChkISPurchage.Checked) ? 1 : 0;

                mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable = (ChkISRedeem.Checked) ? 1 : 0;

                mfProductAMCSchemePlanDetailsVo.IsSIPAvailable = (ChkISSIP.Checked) ? 1 : 0;

                mfProductAMCSchemePlanDetailsVo.IsSTPAvailable = (ChkISSTP.Checked) ? 1 : 0;

                mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable = (ChkISSwitch.Checked) ? 1 : 0;

                mfProductAMCSchemePlanDetailsVo.IsSWPAvailable = (ChkISSWP.Checked) ? 1 : 0;

                mfProductAMCSchemePlanDetailsVo.SecurityCode = (!string.IsNullOrEmpty(txtSecuritycode.Text)) ? txtSecuritycode.Text : "0";


                mfProductAMCSchemePlanDetailsVo.IsNFO = (chkInfo.Checked) ? 1 : 0;


                mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement = (chkOnlineEnablement.Checked) ? 1 : 0;


                if (ChkNRI.Checked)
                {
                    mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode = "NRI";


                }
                if (ChkBO.Checked)
                {
                    mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode = "NIND";

                }
                mfProductAMCSchemePlanDetailsVo.GenerationFrequency = ddlGenerationfreq.SelectedValue;
                mfProductAMCSchemePlanDetailsVo.DVPExternalCode = (!string.IsNullOrEmpty(txtpayOutExtCode.Text)) ? txtpayOutExtCode.Text : "0";
                mfProductAMCSchemePlanDetailsVo.DVRExternalCode = (!string.IsNullOrEmpty(txtReInv.Text)) ? txtReInv.Text : "0";
                mfProductAMCSchemePlanDetailsVo.BSE = (rbtnBSE.Checked) ? 1 : 0;
                mfProductAMCSchemePlanDetailsVo.RTA = (rbtnRTA.Checked) ? 1 : 0;
                mfProductAMCSchemePlanDetailsVo.DVPISIN = (!string.IsNullOrEmpty(txtPay.Text)) ? txtPay.Text : "0";
                mfProductAMCSchemePlanDetailsVo.DVRISIN = (!string.IsNullOrEmpty(TextINV.Text)) ? TextINV.Text : "0";

                mfProductAMCSchemePlanDetailsVo.Branch = (!string.IsNullOrEmpty(txtBranch.Text)) ? txtBranch.Text : "0";

                mfProductAMCSchemePlanDetailsVo.IsETFT = (chkIsETFT.Checked) ? 1 : 0;
                if (!string.IsNullOrEmpty(txtHH.Text) && (!string.IsNullOrEmpty(txtMM.Text)) && (!string.IsNullOrEmpty(txtSS.Text)))
                {
                    string Time = (txtHH.Text + ':' + txtMM.Text + ':' + txtSS.Text);
                    if (Time != null || Time == "")
                    {
                        TimeSpan CutOff = TimeSpan.Parse(Time);
                        mfProductAMCSchemePlanDetailsVo.CutOffTime = (CutOff);
                    }
                    else
                    {
                        TimeSpan CutOff = TimeSpan.Parse("00:00:00");
                        mfProductAMCSchemePlanDetailsVo.CutOffTime = (CutOff);
                    }
                }
                if (!string.IsNullOrEmpty(txtESSchemecode.Text))
                {
                    string extCode1 = OnlineOrderBackOfficeBo.GetExternalCodeOnline(mfProductAMCSchemePlanDetailsVo.SchemePlanCode);
                    if (txtESSchemecode.Text != extCode1)
                    {
                        int count = OnlineOrderBackOfficeBo.SchemeCodeonline(txtESSchemecode.Text, int.Parse(ddlAmc.SelectedValue));

                        if (count >= 1 && txtESSchemecode.Text != string.Empty)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Unique External System Scheme Code.');", true);
                            return;
                        }
                        else
                        {
                            bool bResult = OnlineOrderBackOfficeBo.UpdateSchemeSetUpDetail(mfProductAMCSchemePlanDetailsVo, schemeplancode, userVo.UserId);
                            message = CreateUserMessage(schemeplancode);
                            ShowMessage(message);
                            lbBack.Visible = true;
                            lnkEdit.Visible = true;
                            btnupdate.Visible = false;
                            ControlMode(false);

                        }
                    }
                    else
                    {
                        bool bResult = OnlineOrderBackOfficeBo.UpdateSchemeSetUpDetail(mfProductAMCSchemePlanDetailsVo, schemeplancode, userVo.UserId);
                        message = CreateUserMessage(schemeplancode);
                        ShowMessage(message);
                        lbBack.Visible = true;
                        lnkEdit.Visible = true;
                        btnupdate.Visible = false;
                        ControlMode(false);
                    }
                }
                else
                {
                    bool bResult = OnlineOrderBackOfficeBo.UpdateSchemeSetUpDetail(mfProductAMCSchemePlanDetailsVo, schemeplancode, userVo.UserId);
                    message = CreateUserMessage(schemeplancode);
                    ShowMessage(message);
                    lbBack.Visible = true;
                    lnkEdit.Visible = true;
                    btnupdate.Visible = false;
                    ControlMode(false);
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
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.ascx:btnUpdate_click()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void ControlMode(bool isViewMode)
        {
            if (isViewMode)
            {

            }
            else
            {
                ChkNRI.Enabled = false;
                ChkBO.Enabled = false;
                chkOnlineEnablement.Enabled = false;
                txtFvale.Enabled = false;
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
                txtinvestment.Enabled = false;
                txtESSchemecode.Enabled = false;
                ddlGenerationfreq.Enabled = false;
                ddlDFrequency.Enabled = false;
                btnsubmit.Visible = false;
                btnupdate.Visible = false;
                txtSwitchMultipleUnits.Enabled = false;
                txtSwitchMultipleAmount.Enabled = false;
                txtRedemptionmultiple.Enabled = false;
                txtISIN.Enabled = false;
                txtpayOutExtCode.Enabled = false;
                txtPay.Enabled = false;
                TextINV.Enabled = false;
                txtReInv.Enabled = false;
                rbtnBSE.Enabled = false;
                rbtnRTA.Enabled = false;
            }
        }
        private string CreateUserMessage(int schemeplancode)
        {
            string userMessage = string.Empty;
            if (schemeplancode != 0)
            {
                userMessage = "your Information Is Not Updated";
            }
            else
            {
                userMessage = "Scheme Updated successfully";
            }

            return userMessage;

        }
        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        protected void BindSystematicDetails()
        {
            DataSet dsSystematicDetails;
            DataTable dtSystematicDetails;

            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    schemeplancode = int.Parse(ViewState["Schemeplancode"].ToString());
                }
            }
            else
            {
                schemeplancode = int.Parse(Session["newschemeplancode"].ToString());//
            }

            dsSystematicDetails = OnlineOrderBackOfficeBo.GetSystematicDetails(schemeplancode);
            dtSystematicDetails = dsSystematicDetails.Tables[0];
            if (dtSystematicDetails.Rows.Count > 0)
            {
                if (Cache["SIPDetails" + userVo.UserId] == null)
                {
                    Cache.Insert("SIPDetails" + userVo.UserId, dtSystematicDetails);
                }
                else
                {
                    Cache.Remove("SIPDetails" + userVo.UserId);
                    Cache.Insert("SIPDetails" + userVo.UserId, dtSystematicDetails);
                }
                gvSIPDetails.DataSource = dtSystematicDetails;
                gvSIPDetails.DataBind();
                pnlSIPDetails.Visible = true;
            }
            else
            {
                Cache.Remove("SIPDetails" + userVo.UserId);
                Cache.Insert("SIPDetails" + userVo.UserId, dtSystematicDetails);
                gvSIPDetails.DataSource = dtSystematicDetails;
                gvSIPDetails.DataBind();
                pnlSIPDetails.Visible = true;
            }

        }
        protected void oncheckedISpurchage_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ChkISPurchage.Checked)
            {
                trIPAmount.Visible = true;
            }
            else
            {
                trIPAmount.Visible = false;
            }
        }
        protected void oncheckedredemavaliable_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ChkISRedeem.Checked)
            {
                trMINRedemPtion.Visible = true;
            }
            else
            {
                trMINRedemPtion.Visible = false;
            }
        }
        protected void oncheckedSwtchAvaliable_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ChkISSwitch.Checked)
            {
                trSwitchPavailable.Visible = true;

            }
            else
            {
                trSwitchPavailable.Visible = false;
            }
        }
        protected void ChkISSIP_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ChkISSIP.Checked)
            {
                BindSystematicDetails();
                pnlSIPDetails.Visible = true;

            }
            else
            {
                pnlSIPDetails.Visible = false;
            }
        }
        protected void ChkISSWP_OnCheckedChanged(object sender, EventArgs e)
        {

            if (ChkISSWP.Checked)
            {
                BindSystematicDetails();
                pnlSIPDetails.Visible = true;

            }
            else
            {
            }

        }
        protected void ChkISSTP_OnCheckedChanged(object sender, EventArgs e)
        {

            if (ChkISSTP.Checked)
            {
                BindSystematicDetails();
                pnlSIPDetails.Visible = true;

            }
            else
            {
            }

        }
        protected void gvSIPDetails_OnItemCommand(object source, GridCommandEventArgs e)
        {
            if ((e.CommandName == "Update" && Page.IsValid == true) || (e.CommandName == "PerformInsert" && Page.IsValid == true) || e.CommandName == "Edit" || e.CommandName == "Insert" || e.CommandName == "InitInsert")
            {
                int schemecode = 0;
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                {
                    if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                    {
                        schemecode = int.Parse(ViewState["Schemeplancode"].ToString());
                    }
                }
                else
                {


                    schemecode = int.Parse(Session["newschemeplancode"].ToString());//
                }

                if (e.CommandName == RadGrid.PerformInsertCommandName)
                {

                    GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                    DropDownList ddlFrquency = (DropDownList)e.Item.FindControl("ddlFrquency");
                    TextBox txtstartDate = (TextBox)e.Item.FindControl("txtstartDate");
                    TextBox txtMinDues = (TextBox)e.Item.FindControl("txtMinDues");
                    TextBox txtMaxDues = (TextBox)e.Item.FindControl("txtMaxDues");
                    TextBox txtMinAmount = (TextBox)e.Item.FindControl("txtMinAmount");
                    TextBox txtMultipleAmount = (TextBox)e.Item.FindControl("txtMultipleAmount");
                    TextBox txtSydetails = (TextBox)e.Item.FindControl("txtSydetails");
                    DropDownList ddlSystematicType = (DropDownList)e.Item.FindControl("ddlSystematicType");
                    MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo = new MFProductAMCSchemePlanDetailsVo();
                    mfProductAMCSchemePlanDetailsVo.SystematicCode = ddlSystematicType.SelectedValue;
                    mfProductAMCSchemePlanDetailsVo.Frequency = ddlFrquency.SelectedValue.ToString();
                    mfProductAMCSchemePlanDetailsVo.StartDate = txtstartDate.Text.ToString();
                    mfProductAMCSchemePlanDetailsVo.MinDues = int.Parse(txtMinDues.Text.ToString());
                    mfProductAMCSchemePlanDetailsVo.MaxDues = int.Parse(txtMaxDues.Text.ToString());
                    mfProductAMCSchemePlanDetailsVo.MinAmount = Convert.ToDouble(txtMinAmount.Text.ToString());
                    mfProductAMCSchemePlanDetailsVo.MultipleAmount = Convert.ToDouble(txtMultipleAmount.Text.ToString());
                    OnlineOrderBackOfficeBo.CreateSystematicDetails(mfProductAMCSchemePlanDetailsVo, schemecode, userVo.UserId);

                }
                if (e.CommandName == RadGrid.UpdateCommandName)
                {
                    bool isUpdated = false;
                    GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                    DropDownList ddlFrquency = (DropDownList)e.Item.FindControl("ddlFrquency");
                    TextBox txtstartDate = (TextBox)e.Item.FindControl("txtstartDate");
                    TextBox txtMinDues = (TextBox)e.Item.FindControl("txtMinDues");
                    TextBox txtMaxDues = (TextBox)e.Item.FindControl("txtMaxDues");
                    TextBox txtMinAmount = (TextBox)e.Item.FindControl("txtMinAmount");
                    TextBox txtMultipleAmount = (TextBox)e.Item.FindControl("txtMultipleAmount");
                    DropDownList ddlSystematicType = (DropDownList)e.Item.FindControl("ddlSystematicType");

                    int schemeplanecode = int.Parse(gvSIPDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASP_SchemePlanCode"].ToString());
                    int detailsid = int.Parse(gvSIPDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASPSD_SystematicDetailsId"].ToString());

                    MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo = new MFProductAMCSchemePlanDetailsVo();
                    mfProductAMCSchemePlanDetailsVo.SystematicCode = ddlSystematicType.SelectedValue;
                    mfProductAMCSchemePlanDetailsVo.Frequency = ddlFrquency.SelectedValue.ToString();
                    mfProductAMCSchemePlanDetailsVo.StartDate = txtstartDate.Text.ToString();
                    mfProductAMCSchemePlanDetailsVo.MinDues = int.Parse(txtMinDues.Text.ToString());
                    mfProductAMCSchemePlanDetailsVo.MaxDues = int.Parse(txtMaxDues.Text.ToString());
                    mfProductAMCSchemePlanDetailsVo.MinAmount = Convert.ToDouble(txtMinAmount.Text.ToString());
                    mfProductAMCSchemePlanDetailsVo.MultipleAmount = Convert.ToDouble(txtMultipleAmount.Text.ToString());
                    isUpdated = OnlineOrderBackOfficeBo.EditSystematicDetails(mfProductAMCSchemePlanDetailsVo, schemeplanecode, detailsid, userVo.UserId);
                }

                if (e.CommandName == RadGrid.RebindGridCommandName)
                {
                    gvSIPDetails.Rebind();
                }
                BindSystematicDetails();
            }
        }
        protected void gvSIPDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtSystematicDetails = new DataTable();
            dtSystematicDetails = (DataTable)Cache["SIPDetails" + userVo.UserId];
            if (dtSystematicDetails != null)
            {
                gvSIPDetails.DataSource = dtSystematicDetails;
            }
        }
        protected void gvSIPDetails_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = false;
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlFrquency = (DropDownList)gefi.FindControl("ddlFrquency");
                DropDownList ddlSystematicType = (DropDownList)gefi.FindControl("ddlSystematicType");

                if (ChkISSIP.Checked == true)

                    ddlSystematicType.Items[0].Enabled = true;

                else
                    ddlSystematicType.Items[0].Enabled = false;
                if (ChkISSWP.Checked == true)

                    ddlSystematicType.Items[1].Enabled = true;

                else
                    ddlSystematicType.Items[1].Enabled = false;
                if (ChkISSTP.Checked == true)

                    ddlSystematicType.Items[2].Enabled = true;

                else
                    ddlSystematicType.Items[2].Enabled = false;
                DataSet dsFrequency = OnlineOrderBackOfficeBo.GetFrequency();
                DataTable dtFrequency;
                dtFrequency = dsFrequency.Tables[0];
                ddlFrquency.DataSource = dtFrequency;
                ddlFrquency.DataValueField = dtFrequency.Columns["XF_FrequencyCode"].ToString();

                ddlFrquency.DataTextField = dtFrequency.Columns["XF_Frequency"].ToString();
                ddlFrquency.DataBind();
                ddlFrquency.Items.Insert(0, new ListItem("Select", "Select"));
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlFrquency = (DropDownList)gefi.FindControl("ddlFrquency");
                DropDownList ddlSystematicType = (DropDownList)gefi.FindControl("ddlSystematicType");
                ddlSystematicType.SelectedValue = gvSIPDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XSTT_SystematicTypeCode"].ToString();
                if (ddlSystematicType.SelectedValue == "SIP" || ChkISSIP.Checked)
                    ddlSystematicType.Items[0].Enabled = true;
                if (ddlSystematicType.SelectedValue == "SWP" || ChkISSWP.Checked)
                    ddlSystematicType.Items[1].Enabled = true;
                if (ddlSystematicType.SelectedValue == "STP" || ChkISSTP.Checked)
                    ddlSystematicType.Items[2].Enabled = true;
                DataSet dsFrequency = OnlineOrderBackOfficeBo.GetFrequency();
                DataTable dtFrequency;
                dtFrequency = dsFrequency.Tables[0];
                ddlFrquency.DataSource = dtFrequency;
                ddlFrquency.DataValueField = dtFrequency.Columns["XF_FrequencyCode"].ToString();
                ddlFrquency.DataTextField = dtFrequency.Columns["XF_Frequency"].ToString();
                ddlFrquency.DataBind();

                if (ddlFrquency.Items.Count > 0)
                    ddlFrquency.SelectedValue = gvSIPDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XF_FrequencyCode"].ToString();

            }
        }

        protected void lbBack1_OnClick(object sender, EventArgs e)
        {
            string product;
            string strAction;
            string type;
            string status;
            if (Request.QueryString["strAction"] != null)
            {
                schemeplancode = int.Parse(Request.QueryString["schemeplancode"].ToString());
                strAction = Request.QueryString["strAction"].ToString();
                product = Request.QueryString["product"].ToString();
                type = Request.QueryString["type"].ToString();
                status = Request.QueryString["status"].ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OnlineSchemeMIS", "loadcontrol('OnlineSchemeMIS','?SchemePlanCode=" + schemeplancode + "&strAction=" + strAction + "&product=" + product + "&type=" + type + "&status=" + status + "');", true);
            }
        }
        private void BindSchemeForMarge()
        {
            int schemeplancode1 = 0;
            string Type = "";
            try
            {
                if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
                {
                    if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                    {
                        mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                        schemeplancode1 = mfProductAMCSchemePlanDetailsVo.SchemePlanCode;

                    }
                }

                DataTable dtGetSchemeForMarge = new DataTable();
                Type = OnlineOrderBackOfficeBo.DividentType(schemeplancode1);
                dtGetSchemeForMarge = OnlineOrderBackOfficeBo.GetSchemeForMarge(int.Parse(ddlAmc.SelectedValue), schemeplancode1, Type);

                ddlMargeScheme.DataSource = dtGetSchemeForMarge;
                ddlMargeScheme.DataTextField = dtGetSchemeForMarge.Columns["PASP_SchemePlanName"].ToString();
                ddlMargeScheme.DataValueField = dtGetSchemeForMarge.Columns["PASP_SchemePlanCode"].ToString();
                ddlMargeScheme.DataBind();

                ddlMargeScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
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
        protected void lnkMargeScheme_Click(object sender, EventArgs e)
        {
            radproductcode.VisibleOnPageLoad = false;
            radwindowPopup.VisibleOnPageLoad = true;
            GetBussinessDate();
            GetmergedScheme();
            SchemeStatus();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = false;
        }
        protected void btnMSSubmit_Click(object sender, EventArgs e)
        {
            int schemeplancode1 = 0;
            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                    schemeplancode1 = mfProductAMCSchemePlanDetailsVo.SchemePlanCode;
                }
            }

            int count = OnlineOrderBackOfficeBo.BussinessDateCheck(Convert.ToDateTime(txtSchemeMargeDate.SelectedDate));
            if (count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Select Bussiness Date !!');", true);
            }
            else
            {
                OnlineOrderBackOfficeBo.CreateMargeScheme(schemeplancode1, int.Parse(ddlMargeScheme.SelectedValue), Convert.ToDateTime(txtSchemeMargeDate.SelectedDate), userVo.UserId);
                btnMSSubmit.Visible = false;
                btnReset.Visible = true;
                lnkMargeScheme.Text = "Merged Scheme";
                radwindowPopup.VisibleOnPageLoad = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Scheme Merged Successfully');", true);
            }
        }
        protected void GetBussinessDate()
        {

            string comparetime = "13:00";
            DateTime Time = DateTime.Now;
            string time1 = Time.ToShortTimeString();
            int Fromcomparetime = int.Parse(comparetime.Split(':')[0]);
            int TOcpmaretime = int.Parse(time1.Split(':')[0]);
            if (Fromcomparetime <= TOcpmaretime)
            {
                txtSchemeMargeDate.SelectedDate = DateTime.Now.AddDays(2); //txtSchemeMargeDate.MinDate;
            }
            else
            {
                txtSchemeMargeDate.SelectedDate = DateTime.Now.AddDays(1);
            }
        }

        protected void btnMSUpdate_Click(object sender, EventArgs e)
        {
        }
        private void SchemeStatus()
        {
            int schemeplancode1 = 0;
            string status = "";

            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                    schemeplancode1 = mfProductAMCSchemePlanDetailsVo.SchemePlanCode;
                }
            }
            status = OnlineOrderBackOfficeBo.SchemeStatus(schemeplancode1);
            lblPStatus.Text = status;
            if (status == "Merged")
            {
                ddlMargeScheme.Enabled = false;
                txtSchemeMargeDate.Enabled = false;
                btnMSSubmit.Visible = false;
                btnReset.Visible = false;
                lnkMargeEdit.Visible = false;
            }
            ddlMargeScheme.Enabled = true;

        }
        public void GetmergedScheme()
        {
            int schemeplancode1 = 0;


            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                    schemeplancode1 = mfProductAMCSchemePlanDetailsVo.SchemePlanCode;
                }
            }

            BindSchemeForMarge();
            DataTable dt = new DataTable();

            dt = OnlineOrderBackOfficeBo.GetMergeScheme(schemeplancode1);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PASP_MargeToScheme"].ToString() != "")
                {
                    ddlMargeScheme.SelectedValue = dr["PASP_MargeToScheme"].ToString();
                    lnkMargeScheme.Text = "Merged Scheme";
                    lnkMargeEdit.Visible = true;
                }

                if (dr["PASP_MargeDate"].ToString() != "")
                {
                    if (txtSchemeMargeDate.SelectedDate != Convert.ToDateTime(dr["PASP_MargeDate"].ToString()))
                    {
                        txtSchemeMargeDate.SelectedDate = Convert.ToDateTime(dr["PASP_MargeDate"].ToString());
                    }
                }

            }

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            int schemeplancode1 = 0;
            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                    schemeplancode1 = mfProductAMCSchemePlanDetailsVo.SchemePlanCode;
                }
            }
            OnlineOrderBackOfficeBo.CreateMargeScheme(schemeplancode1, 0, DateTime.MaxValue, userVo.UserId);
            BindSchemeForMarge();
            lnkMargeScheme.Text = "Merge Scheme";
            btnMSSubmit.Visible = true;
            GetBussinessDate();

        }
        protected void lnkMargeEdit_Click(object sender, EventArgs e)
        {
            ddlMargeScheme.Enabled = true;
            txtSchemeMargeDate.Enabled = true;
            btnMSSubmit.Visible = true;
            btnReset.Visible = true;
            lnkMargeEdit.Visible = false;
        }
        protected void lnkProductcode_OnClick(object sender, EventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = false;
            radproductcode.VisibleOnPageLoad = true;
            BindProductcode();
        }
        protected void BindProductcode()
        {
            DataSet dsBindProductcode;
            DataTable dtBindProductcode;
            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                    schemeplancode = mfProductAMCSchemePlanDetailsVo.SchemePlanCode;
                }
            }
            else
            {
                if (Session["newschemeplancode"] == null)
                    return;

                schemeplancode = int.Parse(Session["newschemeplancode"].ToString());
            }

            dsBindProductcode = OnlineOrderBackOfficeBo.Getproductcode(schemeplancode);
            dtBindProductcode = dsBindProductcode.Tables[0];
            if (dtBindProductcode.Rows.Count > 0)
            {
                if (Cache["Productcodelist" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("Productcodelist" + advisorVo.advisorId, dtBindProductcode);
                }
                else
                {
                    Cache.Remove("Productcodelist" + advisorVo.advisorId);
                    Cache.Insert("Productcodelist" + advisorVo.advisorId, dtBindProductcode);
                }
                gvproductcode.DataSource = dtBindProductcode;
                gvproductcode.DataBind();

            }
            else
            {
                gvproductcode.DataSource = dtBindProductcode;
                gvproductcode.DataBind();

            }

        }
        protected void gvproductcode_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindProductcode = new DataTable();
            dtBindProductcode = (DataTable)Cache[advisorVo.advisorId.ToString() + "Productcodelist"];
            if (dtBindProductcode != null)
            {
                gvproductcode.DataSource = dtBindProductcode;
            }
        }
        protected void gvproductcode_OnItemCommand(object source, GridCommandEventArgs e)
        {
            string externalcode = "";
            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    schemeplancode = int.Parse(ViewState["Schemeplancode"].ToString());
                }
            }
            else
            {
                schemeplancode = int.Parse(Session["newschemeplancode"].ToString());
            }
            if (ddlRT.SelectedItem.Text != "")
            {
                externalcode = OnlineOrderBackOfficeBo.ExternalCode(ddlRT.SelectedItem.Text);
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                bool iscreate = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtgproductcode = (TextBox)e.Item.FindControl("txtgproductcode");
                if (!string.IsNullOrEmpty(txtgproductcode.Text) && !string.IsNullOrEmpty(ddlAmc.SelectedValue))
                {

                    int count = OnlineOrderBackOfficeBo.SchemeCode(txtgproductcode.Text, int.Parse(ddlAmc.SelectedValue));
                    if (count >= 1)
                    {


                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Unique Product Code.');", true);
                        BindProductcode();
                        return;

                    }
                    else if (count == 0)
                    {
                        iscreate = OnlineOrderBackOfficeBo.Createproductcode(schemeplancode, txtgproductcode.Text, ddlRT.SelectedItem.Text, externalcode, userVo.UserId);

                    }

                }
                else
                    iscreate = OnlineOrderBackOfficeBo.Createproductcode(schemeplancode, txtgproductcode.Text, ddlRT.SelectedItem.Text, externalcode, userVo.UserId);

            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtgproductcode = (TextBox)e.Item.FindControl("txtgproductcode");
                int productmappingcode = int.Parse(gvproductcode.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASM_Id"].ToString());
                {
                    string extCode1 = OnlineOrderBackOfficeBo.GetExternalCode(schemeplancode, productmappingcode);
                    if (txtgproductcode.Text != extCode1)
                    {
                        int count = OnlineOrderBackOfficeBo.SchemeCode(txtgproductcode.Text, int.Parse(ddlAmc.SelectedValue));

                        if (count >= 1 && txtESSchemecode.Text != string.Empty)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Unique Product Code.');", true);
                            return;
                        }
                        else if (count == 0)
                        {
                            isUpdated = OnlineOrderBackOfficeBo.UpdateProductcode(productmappingcode, txtgproductcode.Text, userVo.UserId);

                        }
                    }
                    else
                    {
                        isUpdated = OnlineOrderBackOfficeBo.UpdateProductcode(productmappingcode, txtgproductcode.Text, userVo.UserId);
                    }
                }

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDelete = false;
                int productmappingcode = int.Parse(gvproductcode.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASM_Id"].ToString());
                isDelete = OnlineOrderBackOfficeBo.ProductcodeDelete(productmappingcode);
                if (isDelete == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Product Code Deleted.');", true);
                }

            }
            BindProductcode();
        }
        protected void btncancelproductcode_OnClick(object sender, EventArgs e)
        {
            radproductcode.VisibleOnPageLoad = false;
            string productcode = "";
            if (Request.QueryString["strAction"] != "" && Request.QueryString["strAction"] != null)
            {
                if (Request.QueryString["strAction"].Trim() == "Edit" || Request.QueryString["strAction"].Trim() == "View")
                {
                    mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(ViewState["Schemeplancode"].ToString());
                    schemeplancode = mfProductAMCSchemePlanDetailsVo.SchemePlanCode;
                }
            }
            else
            {
                if (Session["newschemeplancode"] == null)
                    return;
                schemeplancode = int.Parse(Session["newschemeplancode"].ToString());
            }
            productcode = OnlineOrderBackOfficeBo.GetProductAddedCode(schemeplancode);
            lnkProductcode.Text = productcode;
        }
        protected void txtstartDate_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            string[] strsplit = args.Value.Split(';');
            for (int i = 0; i < strsplit.Length; i++)
            {
                if (int.Parse(strsplit[i]) >= 32)
                {
                    args.IsValid = false;
                }
            }
        }
        protected void chkIsETFT_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkIsETFT.Checked)
            {
                ddlOption.Items[1].Enabled = false;
            }
            else
            {
                ddlOption.Items[1].Enabled = true;
            }
        }
        protected void rbtnBSE_OnCheckedChanged(object sender, EventArgs e)
        {
            ChkISSIP.Visible = false;
            ChkISSTP.Visible = false;
            ChkISSWP.Visible = false;
            pnlSIPDetails.Visible = false;
            ChkISSwitch.Visible = false;
            LalChkISSwitch.Visible = false;
            btnsubmit.Visible = true;
            lblESSchemecode.Visible = false;
            txtESSchemecode.Visible = false;
            ddlDFrequency.SelectedValue = "0";
            ControlEditSubmit();

        }
        protected void rbtnRTA_OnCheckedChanged(object sender, EventArgs e)
        {
            ChkISSIP.Visible = true;
            ChkISSTP.Visible = true;
            ChkISSWP.Visible = true;
            ChkISSIP.Visible = true;
            ChkISSIP.Enabled = true;
            lblESSchemecode.Visible = true;
            txtESSchemecode.Visible = true;
            trISISN.Visible = false;
            trDivReInv.Visible = false;
            ddlOption.SelectedValue = "0";
            ControlEditSubmit();
        }
        protected void ddlDFrequency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDFrequency.SelectedValue == "BT" && rbtnBSE.Checked == true)
            {
                trISISN.Visible = true;
                tdlblINV.Visible = true;
                tdTextINV.Visible = true;
                tdlblpay.Visible = true;
                tdtxtpay.Visible = true;
                LbldivPay.Text = "ISIN PayOut";
                trDivReInv.Visible = true;
                tdlblDInv.Visible = true;
                tdtxtDInv.Visible = true;
                tdPayOurExternalCode.Visible = true;
                tdtxtPayExternalCode.Visible = true;
            }
            else if (ddlDFrequency.SelectedValue == "DVR" && rbtnBSE.Checked == true)
            {
                trDivReInv.Visible = true;
                tdlblINV.Visible = true;
                tdTextINV.Visible = true;
                tdlblDInv.Visible = true;
                tdtxtDInv.Visible = true;
                trISISN.Visible = false;

            }
            else if (ddlDFrequency.SelectedValue == "DVP" && rbtnBSE.Checked == true)
            {
                trDivReInv.Visible = false;
                trISISN.Visible = true;
                tdlblpay.Visible = true;
                tdtxtpay.Visible = true;
                tdPayOurExternalCode.Visible = true;
                tdtxtPayExternalCode.Visible = true;
                LbldivPay.Text = "ISIN PayOut";

            }
            else
            {
                trISISN.Visible = false;
                tdlblINV.Visible = false;
                tdTextINV.Visible = false;
                tdlblpay.Visible = false;
                tdtxtpay.Visible = false;
                trDivReInv.Visible = false;
                tdlblDInv.Visible = false;
                tdtxtDInv.Visible = false;
                tdPayOurExternalCode.Visible = false;
                tdtxtPayExternalCode.Visible = false;
            }
        }
        protected void chkRTA_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkRTA.Checked == true)
            {
                rbtnRTA.Checked = true;
                rbtnBSE.Enabled = false;
                lblESSchemecode.Visible = true;
                txtESSchemecode.Visible = true;
            }
            else
            {
                rbtnRTA.Checked = false;
                lblESSchemecode.Visible = false;
                txtESSchemecode.Visible = false;
                rbtnBSE.Enabled = true;
            }
            if (chkRTA.Checked == true && chkBSE.Checked == true)
            {
                rbtnRTA.Checked = true;
                rbtnRTA.Enabled = true;
                rbtnBSE.Enabled = true;
            }
            if (chkRTA.Checked != true && chkBSE.Checked != true)
            {
                rbtnRTA.Enabled = false;
                rbtnBSE.Enabled = false;
            }
            if (chkBSE.Checked == true)
            {
                rbtnBSE.Checked = true;
            }
            if (chkRTA.Checked != true)
            {
                rbtnRTA.Enabled = false;
            }
        }
        protected void chkBSE_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkBSE.Checked == true && rbtnBSE.Checked == true)
            {
                rbtnBSE.Checked = true;
                lblESSchemecode.Visible = false;
                txtESSchemecode.Visible = false;
                rbtnRTA.Enabled = false;
            }
            else
            {
                rbtnBSE.Checked = false;
                lblESSchemecode.Visible = true;
                txtESSchemecode.Visible = true;
                rbtnRTA.Enabled = true;
            }
            if (chkRTA.Checked == true && chkBSE.Checked == true)
            {

                rbtnRTA.Checked = true;
                rbtnRTA.Enabled = true;
                rbtnBSE.Checked = false;
                rbtnBSE.Enabled = true;
            }
            if (chkRTA.Checked != true && chkBSE.Checked != true)
            {
                rbtnRTA.Enabled = false;
                rbtnBSE.Enabled = false;
            }
            if (chkRTA.Checked == true)
            {
                rbtnRTA.Checked = true;
            }
            if (chkBSE.Checked != true)
            {
                rbtnBSE.Enabled = false;
            }
        }
    }
}
