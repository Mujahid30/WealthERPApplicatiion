using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCommon;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using VoUser;
using BoCustomerPortfolio;
using System.Configuration;
using BoProductMaster;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;


namespace WealthERP.OnlineOrderManagement
{
    public partial class MFOrderSIPTransType : System.Web.UI.UserControl
    {
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = new CustomerVo();
        int CustomerId;
        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();

        DataSet dsCustomerAssociates;
        DataTable dtCustomerAssociatesRaw;
        DataTable dtCustomerAssociates;
        DataRow drCustomerAssociates;
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        string path;
        DataTable dtgetfolioNo;
        DataTable dtModeOfHolding;
        DataTable dtGetAllSIPDataForOrder;
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        ProductMFBo productMfBo = new ProductMFBo();
        OnlineMFOrderBo boOnlineOrder = new OnlineMFOrderBo();
        OnlineMFOrderVo onlineMFOrderVo = new OnlineMFOrderVo();

        string[] AllSipDates;


        protected void Page_Load(object sender, EventArgs e)
        {
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            rmVo = (RMVo)Session["rmVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];

            if (!IsPostBack)
            {
                AmcBind();
                //LoadNominees();
                BindCategory();
                BindModeOfHolding();
            }
        }

        protected void AmcBind()
        {
            DataTable dtAmc = new DataTable();
            dtAmc = commonLookupBo.GetProdAmc();
            if (dtAmc.Rows.Count > 0)
            {
                ddlAmc.DataSource = dtAmc;
                ddlAmc.DataValueField = dtAmc.Columns["PA_AMCCode"].ToString();
                ddlAmc.DataTextField = dtAmc.Columns["PA_AMCName"].ToString();
                ddlAmc.DataBind();
            }
            ddlAmc.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        public void ddlAmc_OnSelectedIndexChanged(object sender, EventArgs e)
        {            
            BindFolioNumber(Convert.ToInt32(ddlAmc.SelectedValue));
            SchemeBind(Convert.ToInt32(ddlAmc.SelectedValue), "ALL");
            DataTable dtAmc = commonLookupBo.GetProdAmc(int.Parse(ddlAmc.SelectedValue));
            lnkFactSheet.PostBackUrl = dtAmc.Rows[0]["PA_Url"].ToString();
        }

        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SchemeBind(Convert.ToInt32(ddlAmc.SelectedValue), ddlCategory.SelectedValue);
        }

        private void BindCategory()
        {
            try
            {
                DataSet dsProductAssetCategory;
                dsProductAssetCategory = productMfBo.GetProductAssetCategory();
                DataTable dtCategory = dsProductAssetCategory.Tables[0];
                if (dtCategory != null)
                {
                    ddlCategory.DataSource = dtCategory;
                    ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlCategory.DataBind();
                }
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void SchemeBind(int amccode, string category)
        {
            DataTable dtScheme = new DataTable();
            dtScheme = commonLookupBo.GetAmcSchemeList(amccode, category);
            ddlScheme.Items.Clear();
            if (dtScheme.Rows.Count > 0)
            {
                ddlScheme.DataSource = dtScheme;
                ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlScheme.DataBind();
            }
            ddlScheme.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }

        protected void BindSIPUI()
        {

        }

        private void SaveOrderDetails()
        {
            onlineMFOrderVo.SchemePlanCode = int.Parse(ddlScheme.SelectedValue);
            onlineMFOrderVo.AccountId =  string.IsNullOrEmpty(ddlFolio.SelectedValue) ? 0 : int.Parse(ddlFolio.SelectedValue); 
            onlineMFOrderVo.SystematicTypeCode = "SIP";

            if (!string.IsNullOrEmpty((txtStartDate.Text )))
                onlineMFOrderVo.StartDate = DateTime.Parse(txtStartDate.Text.ToString());

            if (!string.IsNullOrEmpty((txtEndDate.Text)))            
            onlineMFOrderVo.EndDate = Convert.ToDateTime(txtEndDate.Text);

            onlineMFOrderVo.SystematicDate = 0;
            onlineMFOrderVo.Amount = int.Parse(txtAmount.Text);
            onlineMFOrderVo.SourceCode = "";
            onlineMFOrderVo.FrequencyCode =ddlFrequency.SelectedValue;
            onlineMFOrderVo.CustomerId = customerVo.CustomerId;

            //string DelimitedDateVals = dtGetAllSIPDataForOrder.Row
            //string[] dates = dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString().Split(';');

            //if (tdSipDates == null) return;
            string sipDates = "5,10,15,20,25,30";
            //int i = 0;
            //foreach (string date in AllSipDates)
            //{
            //    i++;
            //    if (string.IsNullOrEmpty(date)) continue;
            //    sipDates += date;
            //    if (i < AllSipDates.Length)
            //        sipDates += ",";
            //}

            onlineMFOrderVo.SystematicDates = sipDates;

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Order has been placed');", true);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int retVal = commonLookupBo.IsRuleCorrect(float.Parse(txtAmount.Text), float.Parse(txtMinAmtReqd.Text), float.Parse(txtAmount.Text), float.Parse(txtMultiplesThereAfter.Text), DateTime.Parse(txtCutOffTime.Text));
            if (retVal != 0) { ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Rules defined were incorrect');", true); return; } 
            List<int> OrderIds = new List<int>();
            SaveOrderDetails();
            OrderIds = boOnlineOrder.CreateOrderMFSipDetails(onlineMFOrderVo, userVo.UserId);
        }
        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSIPUIONSchemeSelection();
        }

        protected void ddlFrequency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSIPDetailsONFrequencySelection(ddlScheme.SelectedValue, ddlFrequency.SelectedValue);
        }
        protected void BindSIPDetailsONFrequencySelection(string schemeId, string freq)
        {
            DataSet dsSipDetails = boOnlineOrder.GetSipDetails(int.Parse(schemeId), freq);
            if (dsSipDetails == null) return;
            DataRow dtSipDet = dsSipDetails.Tables[0].Rows[0];
            txtMinAmtReqd.Text = dtSipDet["PASPSD_MinAmount"].ToString();
            txtMultiplesThereAfter.Text = dtSipDet["PASPSD_MultipleAmount"].ToString();
            txtCutOffTime.Text = dtSipDet["PASPD_CutOffTime"].ToString();
        }
        protected void hidFolioNumber_ValueChanged(object sender, EventArgs e)
        {
        }

        protected void SetLatestNav()
        {
            DataSet ds = commonLookupBo.GetLatestNav(int.Parse(ddlScheme.SelectedValue));
            txtLatestNAV.Text = ds.Tables[0].Rows[0][0].ToString();
        }

        protected void SetOptionsList()
        {
 
        }

        protected void BindSIPUIONSchemeSelection()
        {
            dtGetAllSIPDataForOrder = commonLookupBo.GetAllSIPDataForOrder(Convert.ToInt32(ddlScheme.SelectedValue));
            BindFrequency();
            BindAllControlsWithSIPData();
            SetLatestNav();
            BindFolioNumber(int.Parse(ddlAmc.SelectedValue));
            BindModeOfHolding();
            //LoadNominees();
            //ShowSipDates();
        }

        protected void BindFrequency()
        {
            ddlFrequency.Items.Clear();
            foreach (DataRow row in dtGetAllSIPDataForOrder.Rows)
            {
                if (row["PASP_SchemePlanCode"].ToString() == ddlScheme.SelectedValue.ToString()) {
                    ddlFrequency.Items.Add(new ListItem(row["XF_Frequency"].ToString(), row["XF_FrequencyCode"].ToString()));
                }
 
            }
            //dtGetAllSIPDataForOrder.
            //if (dtGetAllSIPDataForOrder != null)
            //{
            //    ddlFrequency.DataSource = dtGetAllSIPDataForOrder;
            //    ddlFrequency.DataTextField = dtGetAllSIPDataForOrder.Columns["XF_Frequency"].ToString();
            //    ddlFrequency.DataValueField = dtGetAllSIPDataForOrder.Columns["XF_FrequencyCode"].ToString();
            //    ddlFrequency.DataBind();

            //}
        }

        private void BindModeOfHolding()
        {
            dtModeOfHolding = XMLBo.GetModeOfHolding(path);
            ddlModeOfHolding.DataSource = dtModeOfHolding;
            ddlModeOfHolding.DataTextField = "ModeOfHolding";
            ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.Items.Insert(0, new ListItem("Select", "0"));
        }


        public void BindAllControlsWithSIPData()
        {
            if (dtGetAllSIPDataForOrder.Rows.Count > 0 )
            {              
                txtMinAmtReqd.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MinAmount"].ToString();
                txtMultiplesThereAfter.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPSD_MultipleAmount"].ToString();
                txtCutOffTime.Text = dtGetAllSIPDataForOrder.Rows[0]["PASPD_CutOffTime"].ToString();
                BindFrequency();
                ddlFrequency.SelectedValue = dtGetAllSIPDataForOrder.Rows[0]["XF_FrequencyCode"].ToString();
                ShowSipDates(dtGetAllSIPDataForOrder.Rows[0]["PASPSD_StatingDates"].ToString());
            }

        }


        protected void ShowSipDates(string DelimitedDateVals)
        {
            //string DelimitedDateVals = dtGetAllSIPDataForOrder.Row
            AllSipDates = DelimitedDateVals.Split(';');

            //if (tdSipDates == null) return;
            int i = 0;
            foreach (string date in AllSipDates)
            {
                if(string.IsNullOrEmpty(date)) continue;
                CheckBox chk = new CheckBox();
                chk.ID = "chk_Sip_" + i;
                chk.Text = date;
                chk.Visible = true;
                tdSipDates.Controls.Add(chk);
                i++;
            }
        }

        private void BindFolioNumber(int amcCode)
        {
            DataTable dtScheme = new DataTable();
            DataTable dtgetfolioNo;
            try
            {
                dtgetfolioNo = commonLookupBo.GetFolioNumberForSIP(Convert.ToInt32(ddlAmc.SelectedValue),customerVo.CustomerId);

                if (dtgetfolioNo.Rows.Count > 0)
                {
                    ddlFolio.DataSource = dtgetfolioNo;
                    ddlFolio.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolio.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolio.DataBind();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

        protected void LoadNominees()
        {
            try
            {
                dsCustomerAssociates = customerAccountBo.GetCustomerAssociatedRel(customerVo.CustomerId);
                dtCustomerAssociatesRaw = dsCustomerAssociates.Tables[0];

                dtCustomerAssociatesRaw.Columns.Add("MemberCustomerId");
                //dtCustomerAssociates.Columns.Add("AssociationId");
                dtCustomerAssociatesRaw.Columns.Add("Name");
                //dtCustomerAssociates.Columns.Add("Relationship");


                foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
                {
                    drCustomerAssociates = dtCustomerAssociates.NewRow();
                    drCustomerAssociates[0] = dr["C_AssociateCustomerId"].ToString();
                    //drCustomerAssociates[1] = dr["CA_AssociationId"].ToString();
                    drCustomerAssociates[2] = dr["C_FirstName"].ToString() + " " + dr["C_LastName"].ToString();
                    //drCustomerAssociates[3] = dr["XR_Relationship"].ToString();
                    dtCustomerAssociates.Rows.Add(drCustomerAssociates);
                }

                if (dtCustomerAssociates != null)
                {
                    ddlNominee.DataSource = dtCustomerAssociates;
                    ddlNominee.DataValueField = dtCustomerAssociates.Columns["PASP_SchemePlanCode"].ToString();
                    ddlNominee.DataTextField = dtCustomerAssociates.Columns["PASP_SchemePlanName"].ToString();
                    ddlNominee.DataBind();

                    ddlHolder.DataSource = dtCustomerAssociates;
                    ddlHolder.DataValueField = dtCustomerAssociates.Columns["PASP_SchemePlanCode"].ToString();
                    ddlHolder.DataTextField = dtCustomerAssociates.Columns["PASP_SchemePlanName"].ToString();
                    ddlHolder.DataBind();
                }
                //else
                //{
                //    trNominees.Visible = false;
                //}

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGeneralInsuranceAccountAdd.ascx:LoadNominees()");
                object[] objects = new object[1];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkFactSheet_Click(object sender, EventArgs e)
        {

        }
    }
}