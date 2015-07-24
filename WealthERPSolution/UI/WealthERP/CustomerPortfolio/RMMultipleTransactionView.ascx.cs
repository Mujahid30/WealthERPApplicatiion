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
using VoCustomerPortfolio;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Collections;
using BoProductMaster;
using VoProductMaster;
using VOAssociates;
using BOAssociates;
using BoOps;
using VoOps;
using Telerik.Web.UI.GridExcelBuilder;
using BoWerpAdmin;

namespace WealthERP.CustomerPortfolio
{
    public partial class RMMultipleTransactionView : System.Web.UI.UserControl
    {

        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        string userType;
        string path = string.Empty;
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        OrderBo orderbo = new OrderBo();
        UserVo userVo = new UserVo();
        int GroupHead = 0;
        int customerId = 0;
        List<MFTransactionVo> mfTransactionList = null;
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        List<MFTransactionVo> mfBalanceList = null;
        MFTransactionVo mfBalanceVo = new MFTransactionVo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        DateTime dtTo = new DateTime();
        DateBo dtBo = new DateBo();
        DateTime dtFrom = new DateTime();
        static DateTime convertedFromDate = new DateTime();
        static DateTime convertedToDate = new DateTime();
        static double totalAmount = 0;
        static double totalUnits = 0;
        int PasssedFolioValue = 0;
        bool GridViewCultureFlag = true;
        String DisplayType;
        Hashtable ht = new Hashtable();
        int schemePlanCode = 0;
        int count = 0;
        string Category = string.Empty;
        int accountIdForMerge = 0;
        int isMergeComplete = 0;
        int isMergeManual = 0;
        int transactionIdForMerge = 0;
        int trailIdForMerge = 0;
        string folionoForMerge = string.Empty;
        int schemeplancodeForMerge = 0;
        string transactionnoForMerge = string.Empty;
        double unitsForMerge;
        double amountForMerge = 0;
        int AgentId;
        string AgentCode;
        string UserTitle;
        int IsAssociates;
        DateTime transactionDateForMerge;
        string column;
        string custCode;
        int IsfolioOnline;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                if (Request.QueryString["reqId"] != null)
                {
                    this.Page.Culture = "en-GB";
                    userVo = (UserVo)Session["userVo"];
                    advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                    rmVo = (RMVo)Session[SessionContents.RmVo];
                    associatesVo = (AssociatesVO)Session["associatesVo"];
                    ViewState.Remove("TransactionStatus");
                    BindGrid(convertedFromDate, convertedToDate);
                    hdnExportType.Value = "TV";
                    gvBalanceView.Visible = false;
                    Panel1.Visible = false;
                    gvTrail.Visible = false;
                    divTrail.Visible = false;
                    trRangeNcustomer.Visible = false;
                    trCustomer.Visible = false;
                    trAMC.Visible = false;
                    trBtnGo.Visible = false;
                }
                else
                {
                    this.Page.Culture = "en-GB";
                    userVo = (UserVo)Session["userVo"];
                    advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                    rmVo = (RMVo)Session[SessionContents.RmVo];
                    associatesVo = (AssociatesVO)Session["associatesVo"];
                    path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                    if (advisorVo.A_AgentCodeBased == 0)
                    {
                        if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                            userType = "advisor";

                        else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                            userType = "rm";
                        else if (Session["IsCustomerDrillDown"] != null)
                        {
                            userType = "Customer";

                        }
                        //else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "Customer")
                        //    userType = "Customer";
                        else
                            userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

                    }
                    else if (advisorVo.A_AgentCodeBased == 1)
                    {
                        if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin")
                            userType = "advisor";

                        else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                            userType = "rm";
                        else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                        {
                            tdCustomerGroup.Visible = false;
                            ddlAgentCode.Items[1].Enabled = false;

                            userType = "associates";
                            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                            if (associateuserheirarchyVo.UserTitle == "SubBroker")
                            {
                                if (associateuserheirarchyVo.AgentCode != null)
                                {
                                    AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                                }
                                else
                                {
                                    AgentCode = "0";
                                }
                            }
                            else
                            {
                                if (associateuserheirarchyVo.AgentCode != null)
                                {
                                    AgentCode = associateuserheirarchyVo.AgentCode.ToString();
                                }
                                else
                                {
                                    AgentCode = "0";
                                }
                            }
                        }
                        else if (Session["IsCustomerDrillDown"] != null)
                        {
                            userType = "Customer";

                        }
                        //else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "Customer")
                        //    userType = "Customer";
                        else
                            userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
                    }
                    customerVo = (CustomerVo)Session["CustomerVo"];
                    if (Session["CustomerVo"] != null)
                    {
                        customerId = customerVo.CustomerId;
                        trRangeNcustomer.Visible = false;
                        tdCustomerGroup.Visible = false;
                        lblAgentCode.Visible = false;
                        ddlAgentCode.Visible = false;
                        //trRange.Visible = false;

                    }
                    lbBack.Attributes.Add("onClick", "javascript:history.back(); return false;");


                    if (!IsPostBack)
                    {


                        if (Session["CustomerVo"] != null)
                        {
                            ddlDisplayType.Items.RemoveAt(2);
                        }

                        BindAMC();
                        //BindCategory();
                        Bindscheme();
                        Cache.Remove("ViewTrailCommissionDetails" + advisorVo.advisorId);
                        trGroupHead.Visible = false;
                        hdnProcessIdSearch.Value = "0";
                        Panel2.Visible = false;
                        Panel1.Visible = false;
                        gvMFTransactions.Visible = false;
                        gvBalanceView.Visible = false;
                        gvTrail.Visible = false;
                        divTrail.Visible = false;
                        hdnSchemeSearch.Value = string.Empty;
                        hdnTranType.Value = string.Empty;
                        hdnCustomerNameSearch.Value = string.Empty;
                        hdnFolioNumber.Value = string.Empty;
                        hdnCategory.Value = string.Empty;
                        hdnAMC.Value = "0";
                        rbtnPickDate.Checked = true;
                        rbtnPickPeriod.Checked = false;
                        trPeriod.Visible = false;
                        if (userType == "associates")
                        {
                            BindSubBrokerAgentCode(AgentCode);//commented
                        }
                        if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                        {
                            txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                            txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetParentCustomerName";
                        }
                        else if (Session[SessionContents.CurrentUserRole].ToString() == "Admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                        {
                            txtParentCustomer_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                            txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetAdviserGroupCustomerName";
                            txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                            txtClientCode_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                            txtClientCode_autoCompleteExtender.ServiceMethod = "GetCustCode";

                        }
                        else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                        {
                            txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                            txtParentCustomer_autoCompleteExtender.ServiceMethod = "GetBMParentCustomerNames";
                        }
                        BindLink();
                        lnkBackHolding.Attributes.Add("onClick", "javascript:Window.history.go(-2); return true;");
                        ErrorMessage.Visible = false;
                    }
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
                FunctionInfo.Add("Method", "RMMultipleTransactionView.ascx:PageLoad()");
                object[] objects = new object[2];
                objects[0] = advisorVo;
                objects[1] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void BindSubBrokerAgentCode(string AgentCode)
        {
            DataTable dtSubbrokerCode = new DataTable();

            dtSubbrokerCode = orderbo.GetSubBrokerAgentCode(advisorVo.advisorId, AgentCode);

            if (dtSubbrokerCode.Rows.Count > 0)
            {
                ddlBrokerCode.DataSource = dtSubbrokerCode;
                ddlBrokerCode.DataValueField = dtSubbrokerCode.Columns["ACC_AgentId"].ToString();
                ddlBrokerCode.DataTextField = dtSubbrokerCode.Columns["AAC_AgentCode"].ToString();
                ddlBrokerCode.DataBind();
            }
            ddlBrokerCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

        }
        protected void ddlOptionSearch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOptionSearch.SelectedValue == "Name")
            {
                txtcustomerName.Visible = true;
                txtCustCode.Visible = false;

            }
            else
            {
                txtCustCode.Visible = true;
                txtcustomerName.Visible = false;
            }
        }
        protected void ddlsearchcustomertype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsearchcustomertype.SelectedValue == "Individual")
            {
                trGroupHead.Visible = true;
                txtParentCustomer.Visible = false;
                ddlOptionSearch.Visible = true;
                lblGroupHead.Visible = false;
                lblCustomerSearch.Visible = true;
                txtcustomerName.Text = "";
                txtCustCode.Text = "";
            }
            else
            {
                trGroupHead.Visible = false;
            }
        }
        protected void rbtnIndividual_OnCheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnIndividual.Checked == true)
            //{

            //    trGroupHead.Visible = true;
            //    txtParentCustomer.Visible = false;
            //    ddlOptionSearch.Visible = true;
            //    lblGroupHead.Visible = false;
            //    lblCustomerSearch.Visible = true;

            //}
            //else
            //{
            //    trGroupHead.Visible = true;
            //    txtParentCustomer.Visible = true;
            //    lblGroupHead.Visible = true;
            //}
        }
        protected void btnAutoMatch_Click(object sender, EventArgs e)
        {
            bool isMergeCompleted = false;

            Session["accountIdForMerge"] = null;
            Session["TrailComissionSetUpId"] = null;
            Session["folionoForMerge"] = null;
            Session["schemeplancodeForMerge"] = null;
            Session["transactionnoForMerge"] = null;

            radwindowForManualMerge.VisibleOnPageLoad = true;
            DataSet dsTransactionForMerge = new DataSet();



            foreach (GridDataItem item in this.gvTrail.Items)
            {
                if (((CheckBox)item.FindControl("cbOne")).Checked == true)
                {
                    accountIdForMerge = Convert.ToInt32(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFA_AccountId"]);
                    trailIdForMerge = Convert.ToInt32(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFTCSU_TrailComissionSetUpId"]);
                    folionoForMerge = gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFA_FolioNum"].ToString();
                    schemeplancodeForMerge = Convert.ToInt32(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["PASP_SchemePlanCode"]);
                    transactionnoForMerge = gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFT_TransactionNumber"].ToString();
                    unitsForMerge = Convert.ToDouble(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFTCSU_Units"].ToString());
                    amountForMerge = Convert.ToDouble(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFTCSU_Amount"].ToString());
                    transactionDateForMerge = Convert.ToDateTime(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFTCSU_TransactionDate"].ToString());
                    break;
                }

            }
            isMergeCompleted = customerTransactionBo.MergeTrailDetailsWithTransaction(accountIdForMerge, trailIdForMerge, transactionIdForMerge, isMergeComplete, 0, folionoForMerge, schemeplancodeForMerge, transactionnoForMerge, unitsForMerge, amountForMerge, transactionDateForMerge, advisorVo.advisorId);
            if (isMergeCompleted == false)
                Response.Write(@"<script language='javascript'>alert('Error updating Trail Data for: \n" + accountIdForMerge + "');</script>");
            else
                Response.Write(@"<script language='javascript'>alert('Trail Data Updated for: \n" + accountIdForMerge + " successfully.');</script>");

        }
        protected void btnManualMerge_Click(object sender, EventArgs e)
        {
            bool isMergeCompleted = false;
            accountIdForMerge = Convert.ToInt32(Session["accountIdForMerge"].ToString());
            trailIdForMerge = Convert.ToInt32(Session["TrailComissionSetUpId"].ToString());

            foreach (GridDataItem item in this.gvManualMerge.Items)
            {
                if (((CheckBox)item.FindControl("cbRecons")).Checked == true)
                {
                    transactionIdForMerge = Convert.ToInt32(gvManualMerge.MasterTableView.DataKeyValues[item.ItemIndex]["CMFT_MFTransId"]);
                    break;
                }

            }
            isMergeCompleted = customerTransactionBo.MergeTrailDetailsWithTransaction(accountIdForMerge, trailIdForMerge, transactionIdForMerge, isMergeComplete, 1, folionoForMerge, schemeplancodeForMerge, transactionnoForMerge, unitsForMerge, amountForMerge, transactionDateForMerge, advisorVo.advisorId);
            if (isMergeCompleted == false)
                Response.Write(@"<script language='javascript'>alert('Error updating Trail Data for: \n" + accountIdForMerge + "');</script>");
            else
                Response.Write(@"<script language='javascript'>alert('Trail Data Updated for: \n" + accountIdForMerge + " successfully.');</script>");

        }

        protected void btnShowTransactionForManualMerge_Click(object sender, EventArgs e)
        {

            Session["accountIdForMerge"] = null;
            Session["TrailComissionSetUpId"] = null;
            Session["folionoForMerge"] = null;
            Session["schemeplancodeForMerge"] = null;
            Session["transactionnoForMerge"] = null;

            radwindowForManualMerge.VisibleOnPageLoad = true;
            DataSet dsTransactionForMerge = new DataSet();



            foreach (GridDataItem item in this.gvTrail.Items)
            {
                if (((CheckBox)item.FindControl("cbOne")).Checked == true)
                {
                    accountIdForMerge = Convert.ToInt32(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFA_AccountId"]);
                    trailIdForMerge = Convert.ToInt32(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFTCSU_TrailComissionSetUpId"]);
                    folionoForMerge = gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFA_FolioNum"].ToString();
                    schemeplancodeForMerge = Convert.ToInt32(gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["PASP_SchemePlanCode"]);
                    transactionnoForMerge = gvTrail.MasterTableView.DataKeyValues[item.ItemIndex]["CMFT_TransactionNumber"].ToString();
                    break;
                }

            }

            Session["accountIdForMerge"] = accountIdForMerge;
            Session["TrailComissionSetUpId"] = trailIdForMerge;
            Session["folionoForMerge"] = folionoForMerge;
            Session["schemeplancodeForMerge"] = schemeplancodeForMerge;
            Session["transactionnoForMerge"] = transactionnoForMerge;


            dsTransactionForMerge = customerTransactionBo.GetTransactionDetailsForTrail(accountIdForMerge, trailIdForMerge, folionoForMerge, schemeplancodeForMerge, transactionnoForMerge, advisorVo.advisorId);


            if (Cache["TrnxToBeMergedDetails" + userVo.UserId + userType] == null)
            {
                Cache.Insert("TrnxToBeMergedDetails" + userVo.UserId + userType, dsTransactionForMerge);
            }
            else
            {
                Cache.Remove("TrnxToBeMergedDetails" + userVo.UserId + userType);
                Cache.Insert("TrnxToBeMergedDetails" + userVo.UserId + userType, dsTransactionForMerge);
            }

            if (dsTransactionForMerge.Tables[0].Rows.Count > 0)
            {
                gvManualMerge.DataSource = dsTransactionForMerge;
                gvManualMerge.DataBind();
                gvManualMerge.Visible = true;
                btnExportTrnxToBeMerged.Visible = true;
                Button1.Visible = true;
            }
            else
            {
                gvManualMerge.DataSource = dsTransactionForMerge;
                btnExportTrnxToBeMerged.Visible = false;
                Button1.Visible = false;
                gvManualMerge.Visible = true;
            }
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            int i = 0;
            DateTime gvOrderDate = DateTime.MinValue;
            bool result = false;
            foreach (GridDataItem gvRow in gvTrail.Items)
            {

                CheckBox chk = (CheckBox)gvRow.FindControl("cbRecons");
                if (chk.Checked)
                {
                    i++;
                }

            }
            if (i == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a record!');", true);
                //BindMISGridView();
            }
            else
            {
                foreach (GridDataItem gdi in gvTrail.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbRecons")).Checked == true)
                    {
                        int selectedRow = gdi.ItemIndex + 1;
                        //gvOrderId = Convert.ToInt32(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFOD_OrderDetailsId"].ToString());
                        //gvPortfolioId = Convert.ToInt32(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CP_portfolioId"].ToString());
                        //gvSchemeCode = Convert.ToInt32(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["PASP_SchemePlanCode"].ToString());
                        //if (!string.IsNullOrEmpty(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFA_AccountId"].ToString().Trim()))
                        //    gvaccountId = Convert.ToInt32(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFA_AccountId"].ToString());
                        //else
                        //    gvaccountId = 0;
                        //gvTrxType = gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["WMTT_TransactionClassificationCode"].ToString();
                        //gvAmount = Convert.ToDouble(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CMFOD_Amount"].ToString());
                        //gvOrderDate = Convert.ToDateTime(gvCustomerOrderMIS.MasterTableView.DataKeyValues[selectedRow - 1]["CO_OrderDate"].ToString());
                        //result = operationBo.UpdateMFTransaction(gvOrderId, gvSchemeCode, gvaccountId, gvTrxType, gvPortfolioId, gvAmount, gvOrderDate);
                        if (result == true)
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Match is done');", true);
                        else
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Not able to match');", true);
                    }
                }
                //BindMISGridView();
            }
        }

        private void BindLastTradeDate()
        {
            DataSet ds = customerTransactionBo.GetLastTradeDate();
            txtFromDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());
            txtToDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());

        }

        protected void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPickDate.Checked == true)
            {
                //trRange.Visible = true;
                trPeriod.Visible = false;
            }
            else if (rbtnPickPeriod.Checked == true)
            {
                // trRange.Visible = false;
                trPeriod.Visible = true;
                BindPeriodDropDown();
            }
        }

        private void BindPeriodDropDown()
        {
            DataTable dtPeriod;
            dtPeriod = XMLBo.GetDatePeriod(path);

            ddlPeriod.DataSource = dtPeriod;
            ddlPeriod.DataTextField = "PeriodType";
            ddlPeriod.DataValueField = "PeriodCode";
            ddlPeriod.DataBind();
            ddlPeriod.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select a Period", "Select a Period"));
            ddlPeriod.Items.RemoveAt(15);

        }



        protected void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnAll.Checked == true)
            //{
            //    //lblGroupHead.Visible = false;
            //    //txtParentCustomer.Visible = false;
            //    //tdGroupHead.Visible = false;
            //    trGroupHead.Visible = false;

            //}
            //else if (rbtnGroup.Checked == true)
            //{
            //    //lblGroupHead.Visible = true;
            //    ddlOptionSearch.Visible = false;
            //    lblGroupHead.Visible = true;
            //    txtCustCode.Visible = false;
            //    txtcustomerName.Visible = false;
            //    txtParentCustomer.Visible = true;
            //    trGroupHead.Visible = true;
            //    lblCustomerSearch.Visible = false;
            //    BindGroupHead();

            //}
        }

        private void BindAMC()
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;
            try
            {
                //PriceBo priceBo = new PriceBo();
                //dtProductAMC = priceBo.GetMutualFundList();
                dsProductAmc = productMFBo.GetProductAmcList();
                if (dsProductAmc.Tables[0].Rows.Count > 0)
                {
                    dtProductAMC = dsProductAmc.Tables[0];
                    ddlAMC.DataSource = dtProductAMC;
                    ddlAMC.DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                }
                ddlAMC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindCategory()
        {
            try
            {
                DataSet dsProductAssetCategory;
                dsProductAssetCategory = productMFBo.GetProductAssetCategory();
                DataTable dtCategory = dsProductAssetCategory.Tables[0];
                if (dtCategory != null)
                {
                    ddlCategory.DataSource = dtCategory;
                    ddlCategory.DataValueField = dtCategory.Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
                    ddlCategory.DataTextField = dtCategory.Columns["PAIC_AssetInstrumentCategoryName"].ToString();
                    ddlCategory.DataBind();
                }
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

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
        protected void ddlAMC_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Bindscheme();
        }
        private void Bindscheme()
        {
            DataTable dt;
            if (ddlAMC.SelectedValue == "0")
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                ddlSchemeList.DataSource = dt;
                ddlSchemeList.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            else
            {
                dt = productMFBo.GetSchemePlanName(int.Parse(ddlAMC.SelectedValue));
                ddlSchemeList.DataSource = dt;
                ddlSchemeList.DataValueField = "PASP_SchemePlanCode";
                ddlSchemeList.DataTextField = "PASP_SchemePlanName";
                ddlSchemeList.DataBind();
                ddlSchemeList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

            }
        }
        private void BindGroupHead()
        {

        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            DisplayType = ddlDisplayType.SelectedValue;
            hdnSchemeSearch.Value = string.Empty;
            hdnTranType.Value = string.Empty;
            hdnCustomerNameSearch.Value = string.Empty;

            hdnFolioNumber.Value = string.Empty;
            hdnProcessIdSearch.Value = "0";
            if (rbtnPickDate.Checked)
            {
                convertedFromDate = Convert.ToDateTime(txtFromDate.SelectedDate);
                convertedToDate = Convert.ToDateTime(txtToDate.SelectedDate);
            }
            else
            {
                if (ddlPeriod.SelectedIndex != 0)
                {
                    dtBo.CalculateFromToDatesUsingPeriod(ddlPeriod.SelectedValue, out dtFrom, out dtTo);
                    convertedFromDate = dtFrom;
                    convertedToDate = dtTo;
                }
            }
            if (Session["CustomerVo"] != null)
            {
                //trRange.Visible = true;
            }

            if (DisplayType == "TV")
            {
                ViewState.Remove("TransactionStatus");
                BindGrid(convertedFromDate, convertedToDate);
                hdnExportType.Value = "TV";
                gvBalanceView.Visible = false;
                Panel1.Visible = false;
                gvTrail.Visible = false;
                divTrail.Visible = false;

            }
            if (DisplayType == "RHV")
            {
                BindGridBalance(convertedFromDate, convertedToDate);
                hdnExportType.Value = "RHV";
                gvMFTransactions.Visible = false;
                Panel2.Visible = false;
                gvTrail.Visible = false;
                divTrail.Visible = false;

            }

            if (DisplayType == "TCV")
            {
                BindGridTrailCommission(convertedFromDate, convertedToDate);
                hdnExportType.Value = "TCV";
                gvMFTransactions.Visible = false;
                Panel2.Visible = false;
                gvBalanceView.Visible = false;
                Panel1.Visible = false;

            }


        }

        protected void BindLink()
        {
            if (Request.QueryString["folionum"] != null && Request.QueryString["SchemePlanCode"] != null && Request.QueryString["AMC"] != null || Request.QueryString["subBrokerCode"] != null)
            {
                int accountId = int.Parse(Request.QueryString["folionum"].ToString());
                int SchemePlanCode = int.Parse(Request.QueryString["SchemePlanCode"].ToString());
                if (Request.QueryString["subBrokerCode"] != null)
                {
                    string subBrokerCode = Request.QueryString["subBrokerCode"].ToString();
                    if (subBrokerCode == "" || subBrokerCode == "N/A")
                    {
                        ddlAgentCode.SelectedValue = "2";
                    }
                    else
                    {
                        ddlAgentCode.SelectedValue = "1";
                    }
                }
                ddlSchemeList.SelectedValue = SchemePlanCode.ToString();
                int AMC = int.Parse(Request.QueryString["AMC"].ToString());
                ddlAMC.SelectedValue = AMC.ToString();
                PasssedFolioValue = accountId;
                BindLastTradeDate();
                string fromdate = "01-01-1990";
                txtFromDate.SelectedDate = DateTime.Parse(fromdate);
                ViewState["SchemePlanCode"] = SchemePlanCode;
                ViewState["AMC"] = ddlAMC.SelectedValue;
                if (Request.QueryString["name"] != null)
                {
                    column = Request.QueryString["name"].ToString();
                    if (column == "Select1" || column == "SelectAmt")
                    {
                        ddlDisplayType.SelectedValue = "TV";
                        BindGrid(DateTime.Parse(fromdate), DateTime.Parse(txtToDate.SelectedDate.ToString()));
                        Panel2.Visible = true;
                        Panel1.Visible = false;
                        divTrail.Visible = false;
                    }
                    else if (column == "Select2" || column == "SelectTrail")
                    {
                        ddlDisplayType.SelectedValue = "TCV";
                        BindGridTrailCommission(DateTime.Parse(fromdate), DateTime.Parse(txtToDate.SelectedDate.ToString()));
                        Panel2.Visible = false;
                        Panel1.Visible = false;
                        divTrail.Visible = true;
                        hdnExportType.Value = "TCV";
                    }
                    else if (column == "Select")
                    {
                        ddlDisplayType.SelectedValue = "RHV";
                        lbBack.Visible = true;
                        BindGridBalance(DateTime.Parse(fromdate), DateTime.Parse(txtToDate.SelectedDate.ToString()));
                        Panel2.Visible = false;
                        Panel1.Visible = true;
                        divTrail.Visible = false;
                        hdnExportType.Value = "RHV";
                    }
                }
                else
                {
                    ddlDisplayType.SelectedValue = "TV";
                    BindGrid(DateTime.Parse(fromdate), DateTime.Parse(txtToDate.SelectedDate.ToString()));
                    lnkBackHolding.Visible = true;
                    if (ddlAgentCode.SelectedValue == "1")
                    {
                        Panel2.Visible = true;
                    }
                    Panel1.Visible = false;
                    divTrail.Visible = false;
                    hdnExportType.Value = "TV";
                }
            }
            else if (Request.QueryString["CategoryCode"] != null)
            {
                string CategoryCode = Request.QueryString["CategoryCode"].ToString();
                BindLastTradeDate();
                string fromdate = "01-01-1990";
                txtFromDate.SelectedDate = DateTime.Parse(fromdate);
                ViewState["CategoryCode"] = CategoryCode;
                if (Request.QueryString["name"] != null)
                {
                    column = Request.QueryString["name"].ToString();
                    if (column == "SelectAmt")
                    {
                        ddlDisplayType.SelectedValue = "TV";
                        BindGrid(DateTime.Parse(fromdate), DateTime.Parse(txtToDate.SelectedDate.ToString()));
                        Panel2.Visible = true;
                        Panel1.Visible = false;
                        divTrail.Visible = false;
                        hdnExportType.Value = "TV";
                    }
                    else if (column == "SelectTrail")
                    {
                        ddlDisplayType.SelectedValue = "TCV";
                        BindGridTrailCommission(DateTime.Parse(fromdate), DateTime.Parse(txtToDate.SelectedDate.ToString()));
                        Panel2.Visible = false;
                        Panel1.Visible = false;
                        divTrail.Visible = true;
                        hdnExportType.Value = "TCV";
                    }
                }
            }
            else if (Request.QueryString["RMMultipleTransactionView"] != null)
            {

            }
            else
            {
                BindLastTradeDate();

                // this session to fil gvMFTransactions grid while clicking on back button->ViewMfTRansaction
                if (Session["gvMFTransactions"] != null & lnkBackHolding.Visible == true)
                {
                    Panel2.Visible = true;
                    Panel1.Visible = true;
                    gvMFTransactions.Visible = true;
                    string fromdate = "01-01-1990";
                    txtFromDate.SelectedDate = DateTime.Parse(fromdate);
                    gvMFTransactions.DataSource = (DataTable)Session["gvMFTransactions"];
                    gvMFTransactions.DataBind();
                    Session.Remove("gvMFTransactions");
                }

            }

        }

        protected void TabClick(object sender, RadTabStripEventArgs e)
        {

        }

        protected void gvMFTransactions_PreRender(object sender, EventArgs e)
        {
            if (gvMFTransactions.MasterTableView.FilterExpression != string.Empty)
            {
                RefreshCombos();
            }
        }

        protected void RefreshCombos()
        {
            DataTable dtMFTransaction = new DataTable();
            dtMFTransaction = (DataTable)Cache["ViewTransaction" + userVo.UserId + userType];
            DataView view = new DataView(dtMFTransaction);
            DataTable distinctValues = view.ToTable();
            DataRow[] rows = distinctValues.Select(gvMFTransactions.MasterTableView.FilterExpression.ToString());
            gvMFTransactions.MasterTableView.Rebind();

        }
        protected void gvMFTransactions_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
        protected void gvBalanceView_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlAMC.SelectedIndex != 0)
            //{
            //    strAmcCode = ddlAMC.SelectedValue.ToString();
            //    dsSystematicMIS = systematicSetupBo.GetAllDropdownBinding(strAmcCode);
            //    SchemeDropdown(dsSystematicMIS.Tables[1]);
            //}
            //else
            //{
            //    ddlAMC.SelectedIndex = 0;
            //}
        }
        private void SetParameter()
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                hdnAMC.Value = ddlAMC.SelectedValue;
                ViewState["AMCDropDown"] = hdnAMC.Value;
            }
            //else if (ViewState["AMCDropDown"] != null)
            //{
            //    hdnAMC.Value = ViewState["AMCDropDown"].ToString();
            //}
            else
            {
                hdnAMC.Value = "0";
            }
            if (ddlCategory.SelectedIndex != 0)
            {
                hdnCategory.Value = ddlCategory.SelectedValue;
                ViewState["CategoryDropDown"] = hdnCategory.Value;
            }
            //else if (ViewState["CategoryDropDown"] != null)
            //{
            //    hdnCategory.Value = ViewState["CategoryDropDown"].ToString();
            //}
            else
            {
                hdnCategory.Value = "0";
            }
            if (userType == "associates")
            {
                if (ddlBrokerCode.SelectedIndex != 0)
                {
                    hdnAgentCode.Value = ddlBrokerCode.SelectedItem.ToString();
                    //hdnAgentId.Value = ddlBrokerCode.SelectedItem.Value.ToString();

                }
                else
                {
                    hdnAgentCode.Value = AgentCode;
                    //hdnAgentId.Value = "0";
                    //AgentId = int.Parse(ddlBrokerCode.SelectedItem.Value.ToString());
                }
            }
        }
        private void BindGrid(DateTime convertedFromDate, DateTime convertedToDate)
        {

            //Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            //Dictionary<string, string> genDictCategory = new Dictionary<string, string>();
            //Dictionary<string, int> genDictAMC = new Dictionary<string, int>();

            if (Request.QueryString["strPortfolio"] != null)
            {
                string portfolio = Request.QueryString["strPortfolio"].ToString();
                //if (portfolio != "MyPortfolio")
                //{
                //    ddlPortfolioGroup.SelectedItem.Value = "0";
                //    ddlPortfolioGroup.SelectedItem.Text = "UnManaged";
                //}
                //else
                //{
                //    ddlPortfolioGroup.SelectedItem.Value = "1";
                //    ddlPortfolioGroup.SelectedItem.Text = "Managed";
                //}

            }
            SetParameter();
            DataSet ds = new DataSet();
            //int Count = 0;
            //totalAmount = 0;
            //totalUnits = 0;
            int rmID = 0;
            int AdviserId = 0;
            //AmcCode =  hdnAMC.Value.ToString();
            //Category = hdnCategory.Value;
            if (advisorVo.A_AgentCodeBased == 0)
            {
                if (userType == "advisor" || userType == "ops")
                {
                    AdviserId = advisorVo.advisorId;
                    IsAssociates = 0;
                    //gvMFTransactions.Columns[20].Visible = false;
                    //gvMFTransactions.Columns[21].Visible = false;
                    //gvMFTransactions.Columns[22].Visible = false;
                    //gvMFTransactions.Columns[23].Visible = false;
                    //gvMFTransactions.Columns[24].Visible = false;
                    //gvMFTransactions.Columns[25].Visible = false;
                    //gvMFTransactions.Columns[26].Visible = false;
                    //gvMFTransactions.Columns[27].Visible = false;
                    //gvMFTransactions.Columns[28].Visible = false;
                }
                else if (userType == "rm")
                {
                    rmID = rmVo.RMId;
                    IsAssociates = 0;
                }
            }
            else if (advisorVo.A_AgentCodeBased == 1)
            {
                if (userType == "advisor" || userType == "ops")
                {
                    AdviserId = advisorVo.advisorId;
                    hdnAgentCode.Value = "0";

                }
                else if (userType == "associates")
                {
                    gvMFTransactions.Columns[13].Visible = false;
                    gvMFTransactions.Columns[14].Visible = false;
                    AdviserId = advisorVo.advisorId;
                    AgentId = 0;

                }
            }
            //schemePlanCode = 0;
            if (!string.IsNullOrEmpty(ddlSchemeList.SelectedValue))
            {
                schemePlanCode = int.Parse(ddlSchemeList.SelectedValue);
            }
            else
            {
                ddlSchemeList.SelectedValue = "0";
            }
            //schemePlanCode = Convert.ToInt32(ViewState["SchemePlanCode"]);
            //int categorycode = Convert.ToInt32(ViewState["CategoryCode"]);
            if (column == "SelectAmt")
            {
                hdnCategory.Value = (ViewState["CategoryCode"]).ToString();
            }
            //Convert.ToString(categorycode).ToString();
            if (!string.IsNullOrEmpty(txtParentCustomerId.Value.ToString().Trim()))
                customerId = int.Parse(txtParentCustomerId.Value);
            if (ddlsearchcustomertype.SelectedValue != "All")
            {
                if (!string.IsNullOrEmpty(txtcustomerId.Value.ToString().Trim()))
                    customerId = int.Parse(txtcustomerId.Value);
            }
            if (!string.IsNullOrEmpty(txtCustCode.Text))
            {
                string custCode = txtCustCode.Text;
            }
            //if (ddlType.SelectedValue=="Online")
            //{
            //    IsfolioOnline = 1;
            //}
            //if (ddlType.SelectedValue == "Offline")
            //{
            IsfolioOnline = 0;
            //}
            try
            {//if (rbtnGroup.Checked || rbtnIndividual.Checked)
                if (Request.QueryString["reqId"] != null)
                {
                    int requestId = int.Parse(Request.QueryString["reqId"]);
                    mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(rmID, advisorVo.advisorId, customerVo.CustomerId, IsfolioOnline, convertedFromDate, convertedToDate, 1, PasssedFolioValue, false, 0, 0, string.Empty, advisorVo.A_AgentCodeBased, string.Empty, null, int.Parse(ddlAgentCode.SelectedValue), requestId);
                }
                else
                {
                    if (ddlsearchcustomertype.SelectedValue != "All" && ddlsearchcustomertype.SelectedValue != "Individual")
                    {
                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(rmID, AdviserId, customerId, IsfolioOnline, convertedFromDate, convertedToDate, 1, PasssedFolioValue, false, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType, int.Parse(ddlAgentCode.SelectedValue), 0);
                    }
                    else if (Convert.ToString(Session["IsCustomerDrillDown"]) == "Yes")
                    {
                        customerId = customerVo.CustomerId;
                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(rmID, AdviserId, customerId, IsfolioOnline, convertedFromDate, convertedToDate, 1, PasssedFolioValue, true, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, "Customer", int.Parse(ddlAgentCode.SelectedValue), 0);
                    }
                    else
                    {
                        mfTransactionList = customerTransactionBo.GetRMCustomerMFTransactions(rmID, AdviserId, customerId, IsfolioOnline, convertedFromDate, convertedToDate, 1, PasssedFolioValue, false, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType, int.Parse(ddlAgentCode.SelectedValue), 0);
                    }
                }
                if (mfTransactionList.Count != 0)
                {
                    ErrorMessage.Visible = false;
                    Panel2.Visible = true;
                    DataTable dtMFTransactions = new DataTable();
                    Session["gvMFTransactions"] = dtMFTransactions;
                    dtMFTransactions.Columns.Add("TransactionId");
                    dtMFTransactions.Columns.Add("Customer Name");
                    dtMFTransactions.Columns.Add("Folio Number");
                    dtMFTransactions.Columns.Add("Scheme Name");
                    dtMFTransactions.Columns.Add("Transaction Type");
                    dtMFTransactions.Columns.Add("Transaction Date", typeof(DateTime));
                    dtMFTransactions.Columns.Add("Price", typeof(double));
                    dtMFTransactions.Columns.Add("Units", typeof(double));
                    dtMFTransactions.Columns.Add("Amount", typeof(double));
                    dtMFTransactions.Columns.Add("STT", typeof(double));
                    dtMFTransactions.Columns.Add("Portfolio Name");
                    dtMFTransactions.Columns.Add("TransactionStatus");
                    dtMFTransactions.Columns.Add("Category");
                    dtMFTransactions.Columns.Add("AMC");
                    dtMFTransactions.Columns.Add("WR_RequestId");
                    dtMFTransactions.Columns.Add("CMFT_SubBrokerCode");
                    dtMFTransactions.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
                    dtMFTransactions.Columns.Add("CreatedOn");
                    dtMFTransactions.Columns.Add("CMFT_ExternalBrokerageAmount", typeof(double));
                    dtMFTransactions.Columns.Add("CMFT_Area");
                    dtMFTransactions.Columns.Add("CMFT_EUIN");
                    dtMFTransactions.Columns.Add("ZonalManagerName");
                    dtMFTransactions.Columns.Add("AreaManager");
                    dtMFTransactions.Columns.Add("AssociatesName");
                    dtMFTransactions.Columns.Add("ChannelName");
                    dtMFTransactions.Columns.Add("Titles");
                    dtMFTransactions.Columns.Add("ClusterManager");
                    dtMFTransactions.Columns.Add("ReportingManagerName");
                    dtMFTransactions.Columns.Add("UserType");
                    dtMFTransactions.Columns.Add("DeuptyHead");
                    dtMFTransactions.Columns.Add("CMFT_UserTransactionNo");
                    dtMFTransactions.Columns.Add("CityGroup");
                    

                    DataRow drMFTransaction;
                    for (int i = 0; i < mfTransactionList.Count; i++)
                    {
                        drMFTransaction = dtMFTransactions.NewRow();
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo = mfTransactionList[i];

                        drMFTransaction[0] = mfTransactionVo.TransactionId.ToString();
                        drMFTransaction[1] = mfTransactionVo.CustomerName.ToString();
                        drMFTransaction[2] = mfTransactionVo.Folio.ToString();
                        drMFTransaction[3] = mfTransactionVo.SchemePlan.ToString();
                        drMFTransaction[4] = mfTransactionVo.TransactionType.ToString();
                        drMFTransaction[5] = mfTransactionVo.TransactionDate.ToShortDateString().ToString();
                        if (GridViewCultureFlag == true)
                            drMFTransaction[6] = decimal.Parse(mfTransactionVo.Price.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFTransaction[6] = decimal.Parse(mfTransactionVo.Price.ToString());
                        }
                        //drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        //drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        totalUnits = totalUnits + mfTransactionVo.Units;
                        if (GridViewCultureFlag == true)
                            drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString());
                        }
                        totalAmount = totalAmount + mfTransactionVo.Amount;
                        if (GridViewCultureFlag == true)
                            drMFTransaction[9] = decimal.Parse(mfTransactionVo.STT.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFTransaction[9] = decimal.Parse(mfTransactionVo.STT.ToString());
                        }
                        drMFTransaction[10] = mfTransactionVo.PortfolioName.ToString();
                        drMFTransaction[11] = mfTransactionVo.TransactionStatus.ToString();
                        drMFTransaction[12] = mfTransactionVo.Category;
                        drMFTransaction[13] = mfTransactionVo.AMCName;

                        if (mfTransactionVo.RequestId == 0)
                            drMFTransaction[14] = "N/A";
                        else
                            drMFTransaction[14] = int.Parse(mfTransactionVo.RequestId.ToString());
                        drMFTransaction[15] = mfTransactionVo.SubBrokerCode;
                        drMFTransaction[16] = mfTransactionVo.SubCategoryName;
                        drMFTransaction[17] = mfTransactionVo.CreatedOn;
                        drMFTransaction[18] = decimal.Parse(mfTransactionVo.BrokerageAmount.ToString());
                        drMFTransaction["CMFT_Area"] = mfTransactionVo.Area.ToString();
                        drMFTransaction["CMFT_EUIN"] = mfTransactionVo.EUIN.ToString();
                        drMFTransaction["CityGroup"] = mfTransactionVo.Citygroup;
                        if (ddlAgentCode.SelectedValue == "1")
                        {
                            drMFTransaction["ZonalManagerName"] = mfTransactionVo.ZMName.ToString();
                            drMFTransaction["AreaManager"] = mfTransactionVo.AName.ToString();
                            drMFTransaction["AssociatesName"] = mfTransactionVo.SubbrokerName.ToString();
                            drMFTransaction["ChannelName"] = mfTransactionVo.Channel.ToString();
                            drMFTransaction["Titles"] = mfTransactionVo.Titles.ToString();
                            drMFTransaction["ClusterManager"] = mfTransactionVo.ClusterMgr.ToString();
                            drMFTransaction["ReportingManagerName"] = mfTransactionVo.ReportingManagerName.ToString();
                            drMFTransaction["UserType"] = mfTransactionVo.UserType.ToString();
                            drMFTransaction["DeuptyHead"] = mfTransactionVo.DeuptyHead.ToString();
                            drMFTransaction["CMFT_UserTransactionNo"] = mfTransactionVo.UserTransactionNo.ToString();
                        }

                        dtMFTransactions.Rows.Add(drMFTransaction);
                    }
                    GridBoundColumn gbcCustomer = gvMFTransactions.MasterTableView.Columns.FindByUniqueName("Customer Name") as GridBoundColumn;
                    GridBoundColumn gbcPortfolio = gvMFTransactions.MasterTableView.Columns.FindByUniqueName("Portfolio Name") as GridBoundColumn;
                    GridBoundColumn gbCMFT_ExternalBrokerageAmount = gvMFTransactions.MasterTableView.Columns.FindByUniqueName("CMFT_ExternalBrokerageAmount") as GridBoundColumn;
                    if (Convert.ToString(Session["IsCustomerDrillDown"]) == "Yes")
                    {
                        gbcCustomer.Visible = false;
                        gbcPortfolio.Visible = false;
                        gbCMFT_ExternalBrokerageAmount.Visible = false;
                    }
                    else
                        gbcCustomer.Visible = true;
                    if (ddlAgentCode.SelectedValue == "1")
                    {
                        if (dtMFTransactions.Rows.Count > 0)
                        {
                            if (Cache["ViewTransaction" + userVo.UserId + userType] == null)
                            {
                                Cache.Insert("ViewTransaction" + userVo.UserId + userType, dtMFTransactions);
                            }
                            else
                            {
                                Cache.Remove("ViewTransaction" + userVo.UserId + userType);
                                Cache.Insert("ViewTransaction" + userVo.UserId + userType, dtMFTransactions);
                            }

                            gvMFTransactions.CurrentPageIndex = 0;
                            gvMFTransactions.DataSource = dtMFTransactions;
                            Session["gvMFTransactions"] = dtMFTransactions;
                            gvMFTransactions.DataBind();
                            Panel2.Visible = true;
                            ErrorMessage.Visible = false;
                            gvMFTransactions.Visible = true;
                            btnTrnxExport.Visible = true;
                            pnlMFTransactionWithoutAgentCode.Visible = false;

                        }
                        else
                        {
                            gvMFTransactions.Visible = false;
                            hdnRecordCount.Value = "0";
                            ErrorMessage.Visible = true;
                            Panel2.Visible = false;
                            btnTrnxExport.Visible = false;
                            pnlMFTransactionWithoutAgentCode.Visible = false;

                        }
                    }
                    else
                    {
                        if (dtMFTransactions.Rows.Count > 0)
                        {
                            if (Cache["ViewTransactionWithoutAgent" + userVo.UserId + userType] == null)
                            {
                                Cache.Insert("ViewTransactionWithoutAgent" + userVo.UserId + userType, dtMFTransactions);
                            }
                            else
                            {
                                Cache.Remove("ViewTransactionWithoutAgent" + userVo.UserId + userType);
                                Cache.Insert("ViewTransactionWithoutAgent" + userVo.UserId + userType, dtMFTransactions);
                            }

                            gvMFTransactionWithoutAgentCode.CurrentPageIndex = 0;
                            gvMFTransactionWithoutAgentCode.DataSource = dtMFTransactions;
                            Session["ViewTransactionWithoutAgent"] = dtMFTransactions;
                            gvMFTransactionWithoutAgentCode.DataBind();
                            pnlMFTransactionWithoutAgentCode.Visible = true;
                            ErrorMessage.Visible = false;
                            gvMFTransactionWithoutAgentCode.Visible = true;
                            btnTrnxExportMFOffLineWithoutSubbroker.Visible = true;
                            Panel2.Visible = false;
                        }
                        else
                        {
                            gvMFTransactionWithoutAgentCode.Visible = true;
                            hdnRecordCount.Value = "0";
                            ErrorMessage.Visible = true;
                            Panel2.Visible = false;
                            pnlMFTransactionWithoutAgentCode.Visible = false;
                        }
                    }
                }

                gvTrail.Visible = false;
                if (mfTransactionList.Count == 0)
                {
                    ErrorMessage.Visible = true;
                    gvMFTransactions.Visible = false;
                    gvMFTransactionWithoutAgentCode.Visible = true;
                    Panel2.Visible = false;
                    pnlMFTransactionWithoutAgentCode.Visible = false;

                }

            }
            catch (Exception e)
            {
            }
        }
        protected void gvMFTransactionWithoutAgentCode_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            //DataTable dtMFTransactions = (DataTable)Cache["ViewTransactionWithoutAgent" + userVo.UserId];
            //if (dtMFTransactions != null)
            //{
            //    gvMFTransactionWithoutAgentCode.DataSource = dtMFTransactions;
            //}
            DataTable dtMFTransaction = new DataTable();
            dtMFTransaction = (DataTable)Cache["ViewTransactionWithoutAgent" + userVo.UserId + userType];
            if (dtMFTransaction != null)
            {
                if (ViewState["TransactionStatus"] != null)
                    rcbType = ViewState["TransactionStatus"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtMFTransaction, "TransactionStatus = '" + rcbType + "'", "Customer Name,Folio Number,Category,AMC,Scheme Name,Transaction Type,Transaction Date,ADUL_ProcessId", DataViewRowState.CurrentRows);
                    gvMFTransactionWithoutAgentCode.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvMFTransactionWithoutAgentCode.DataSource = dtMFTransaction;

                }
            }
        }
        private void BindGridBalance(DateTime convertedFromDate, DateTime convertedToDate)
        {
            //Dictionary<string, string> genDictTranType = new Dictionary<string, string>();
            //Dictionary<string, string> genDictCategory = new Dictionary<string, string>();
            //Dictionary<string, int> genDictAMC = new Dictionary<string, int>();
            DataSet ds = new DataSet();
            //int Count = 0;
            //totalAmount = 0;
            //totalUnits = 0;
            SetParameter();
            int rmID = 0;
            int AdviserId = 0;

            if (advisorVo.A_AgentCodeBased == 0)
            {
                if (userType == "advisor" || userType == "ops")
                {
                    AdviserId = advisorVo.advisorId;
                    gvBalanceView.Columns[17].Visible = false;
                    gvBalanceView.Columns[18].Visible = false;
                    gvBalanceView.Columns[19].Visible = false;
                    gvBalanceView.Columns[20].Visible = false;
                    gvBalanceView.Columns[21].Visible = false;
                    gvBalanceView.Columns[22].Visible = false;
                    gvBalanceView.Columns[23].Visible = false;
                    gvBalanceView.Columns[24].Visible = false;
                    gvBalanceView.Columns[25].Visible = false;
                    gvBalanceView.Columns[26].Visible = false;
                }
                else if (userType == "rm")
                {
                    rmID = rmVo.RMId;

                }
            }
            else if (advisorVo.A_AgentCodeBased == 1)
            {
                if (userType == "advisor" || userType == "ops")
                {
                    AdviserId = advisorVo.advisorId;
                    hdnAgentCode.Value = "0";

                }
                else if (userType == "associates")
                {
                    AdviserId = advisorVo.advisorId;
                }
            }
            //schemePlanCode = 0;
            if (!string.IsNullOrEmpty(ddlSchemeList.SelectedValue))
            {
                //schemePlanCode = Convert.ToInt32(ViewState["SchemePlanCode"]);
                schemePlanCode = int.Parse(ddlSchemeList.SelectedValue);

            }
            else
            {
                ddlSchemeList.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(txtParentCustomerId.Value.ToString().Trim()))
                customerId = int.Parse(txtParentCustomerId.Value);
            try
            {//pramod
                if (rbtnGroup.Checked)
                {
                    mfBalanceList = customerTransactionBo.GetRMCustomerMFBalance(rmID, AdviserId, customerId, convertedFromDate, convertedToDate, 1, PasssedFolioValue, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType);
                }
                else if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    customerId = customerVo.CustomerId;
                    mfBalanceList = customerTransactionBo.GetRMCustomerMFBalance(rmID, AdviserId, customerId, convertedFromDate, convertedToDate, 1, PasssedFolioValue, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType);
                }
                else
                {
                    mfBalanceList = customerTransactionBo.GetRMCustomerMFBalance(rmID, AdviserId, 0, convertedFromDate, convertedToDate, 1, PasssedFolioValue, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType);
                }
                if (mfBalanceList.Count != 0)
                {
                    ErrorMessage.Visible = false;
                    Panel1.Visible = true;
                    DataTable dtMFBalance = new DataTable();

                    dtMFBalance.Columns.Add("TransactionId");
                    dtMFBalance.Columns.Add("Customer Name");
                    dtMFBalance.Columns.Add("Folio Number");
                    dtMFBalance.Columns.Add("AccountId");
                    dtMFBalance.Columns.Add("SchemePlanCode");
                    dtMFBalance.Columns.Add("Scheme Name");
                    dtMFBalance.Columns.Add("CurrentValue", typeof(double));
                    dtMFBalance.Columns.Add("Transaction Type");
                    dtMFBalance.Columns.Add("Transaction Date", typeof(DateTime));
                    dtMFBalance.Columns.Add("Category");
                    dtMFBalance.Columns.Add("PAISC_AssetInstrumentSubCategoryName");
                    dtMFBalance.Columns.Add("AMC");
                    dtMFBalance.Columns.Add("CMFT_SubBrokerCode");
                    dtMFBalance.Columns.Add("Price", typeof(double));
                    dtMFBalance.Columns.Add("Units", typeof(double));
                    dtMFBalance.Columns.Add("Amount", typeof(double));
                    dtMFBalance.Columns.Add("NAV", typeof(double));
                    dtMFBalance.Columns.Add("Age");
                    dtMFBalance.Columns.Add("Balance", typeof(double));
                    dtMFBalance.Columns.Add("ZonalManagerName");
                    dtMFBalance.Columns.Add("AreaManager");
                    dtMFBalance.Columns.Add("AssociatesName");
                    dtMFBalance.Columns.Add("ChannelName");
                    dtMFBalance.Columns.Add("Titles");
                    dtMFBalance.Columns.Add("ClusterManager");
                    dtMFBalance.Columns.Add("ReportingManagerName");
                    dtMFBalance.Columns.Add("UserType");
                    dtMFBalance.Columns.Add("DeuptyHead");
                    dtMFBalance.Columns.Add("AMCCode");

                    DataRow drMFBalance;

                    for (int i = 0; i < mfBalanceList.Count; i++)
                    {
                        drMFBalance = dtMFBalance.NewRow();
                        mfBalanceVo = new MFTransactionVo();
                        mfBalanceVo = mfBalanceList[i];
                        drMFBalance["TransactionId"] = mfBalanceVo.TransactionId.ToString();
                        drMFBalance["Customer Name"] = mfBalanceVo.CustomerName.ToString();
                        drMFBalance["Folio Number"] = mfBalanceVo.Folio.ToString();
                        drMFBalance["AccountId"] = mfBalanceVo.AccountId.ToString();
                        drMFBalance["SchemePlanCode"] = mfBalanceVo.MFCode.ToString();
                        drMFBalance["AMCCode"] = mfBalanceVo.AMCCode.ToString();
                        drMFBalance["Scheme Name"] = mfBalanceVo.SchemePlan.ToString();
                        if (GridViewCultureFlag == true)
                            drMFBalance["CurrentValue"] = decimal.Parse(mfBalanceVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFBalance["CurrentValue"] = decimal.Parse(mfBalanceVo.CurrentValue.ToString());
                        }
                        //drMFBalance["CurrentValue"] = mfBalanceVo.CurrentValue.ToString("f3"); 
                        drMFBalance["Transaction Type"] = mfBalanceVo.TransactionType.ToString();
                        drMFBalance["Transaction Date"] = mfBalanceVo.TransactionDate.ToShortDateString().ToString();
                        drMFBalance["Category"] = mfBalanceVo.Category.ToString();
                        drMFBalance["PAISC_AssetInstrumentSubCategoryName"] = mfBalanceVo.SubCategoryName.ToString();
                        drMFBalance["AMC"] = mfBalanceVo.AMCName;
                        drMFBalance["CMFT_SubBrokerCode"] = mfBalanceVo.SubBrokerCode;
                        if (GridViewCultureFlag == true)
                            drMFBalance["Price"] = decimal.Parse(mfBalanceVo.Price.ToString()).ToString("n4", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFBalance["Price"] = decimal.Parse(mfBalanceVo.Price.ToString());
                        }
                        //drMFTransaction[7] = mfTransactionVo.Units.ToString("f4");
                        //drMFTransaction[8] = decimal.Parse(mfTransactionVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drMFBalance["Units"] = mfBalanceVo.Units.ToString("f4");
                        totalUnits = totalUnits + mfBalanceVo.Units;
                        if (GridViewCultureFlag == true)
                            drMFBalance["Amount"] = decimal.Parse(mfBalanceVo.Amount.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        else
                        {
                            drMFBalance["Amount"] = decimal.Parse(mfBalanceVo.Amount.ToString());
                        }
                        totalAmount = totalAmount + mfBalanceVo.Amount;
                        drMFBalance["NAV"] = mfBalanceVo.NAV.ToString();
                        drMFBalance["Age"] = mfBalanceVo.Age;
                        drMFBalance["Balance"] = mfBalanceVo.Balance.ToString();
                        if (advisorVo.A_AgentCodeBased == 1)
                        {
                            drMFBalance["ZonalManagerName"] = mfBalanceVo.ZMName.ToString();
                            drMFBalance["AreaManager"] = mfBalanceVo.AName.ToString();
                            drMFBalance["AssociatesName"] = mfBalanceVo.SubbrokerName.ToString();
                            drMFBalance["ChannelName"] = mfBalanceVo.Channel.ToString();
                            drMFBalance["Titles"] = mfBalanceVo.Titles.ToString();
                            drMFBalance["ClusterManager"] = mfBalanceVo.ClusterMgr.ToString();
                            drMFBalance["ReportingManagerName"] = mfBalanceVo.ReportingManagerName.ToString();
                            drMFBalance["UserType"] = mfBalanceVo.UserType.ToString();
                            drMFBalance["DeuptyHead"] = mfBalanceVo.DeuptyHead.ToString();
                        }
                        dtMFBalance.Rows.Add(drMFBalance);
                    }

                    GridBoundColumn gbcCustomer = gvBalanceView.MasterTableView.Columns.FindByUniqueName("Customer Name") as GridBoundColumn;
                    //GridBoundColumn gbcPortfolio = gvBalanceView.MasterTableView.Columns.FindByUniqueName("Portfolio Name") as GridBoundColumn;
                    if (Session["CustomerVo"] != null)
                    {
                        gbcCustomer.Visible = false;
                        //    gbcPortfolio.Visible = false;
                    }
                    else
                        gbcCustomer.Visible = true;

                    if (Cache["ViewBalance" + userVo.UserId + userType] == null)
                    {
                        Cache.Insert("ViewBalance" + userVo.UserId + userType, dtMFBalance);
                    }
                    else
                    {
                        Cache.Remove("ViewBalance" + userVo.UserId + userType);
                        Cache.Insert("ViewBalance" + userVo.UserId + userType, dtMFBalance);
                    }
                    gvBalanceView.DataSource = dtMFBalance;
                    gvBalanceView.DataBind();
                    Panel1.Visible = true;
                    ErrorMessage.Visible = false;
                    gvBalanceView.Visible = true;
                    btnTrnxExport.Visible = true;
                }

                else
                {
                    gvBalanceView.Visible = false;
                    ErrorMessage.Visible = true;
                    Panel1.Visible = false;
                    hdnRecordCount.Value = "0";
                    btnTrnxExport.Visible = false;
                }
                gvTrail.Visible = false;
            }
            catch (Exception e)
            {
            }
        }

        protected void CallAllGridBindingFunctions()
        {
        }
        protected void ExportGrid(string hdnExportType)
        {
            if (hdnExportType == "TV")
            {

                gvMFTransactions.ExportSettings.OpenInNewWindow = true;
                gvMFTransactions.ExportSettings.IgnorePaging = true;
                gvMFTransactions.ExportSettings.HideStructureColumns = true;
                gvMFTransactions.ExportSettings.ExportOnlyData = true;
                gvMFTransactions.ExportSettings.FileName = "View Transactions Details";
                gvMFTransactions.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvMFTransactions.MasterTableView.ExportToExcel();
            }
            if (hdnExportType == "TVW")
            {

                gvMFTransactionWithoutAgentCode.ExportSettings.OpenInNewWindow = true;
                gvMFTransactionWithoutAgentCode.ExportSettings.IgnorePaging = true;
                gvMFTransactionWithoutAgentCode.ExportSettings.HideStructureColumns = true;
                gvMFTransactionWithoutAgentCode.ExportSettings.ExportOnlyData = true;
                gvMFTransactionWithoutAgentCode.ExportSettings.FileName = "View Transactions Details";
                gvMFTransactionWithoutAgentCode.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvMFTransactionWithoutAgentCode.MasterTableView.ExportToExcel();
            }
            if (hdnExportType == "RHV")
            {
                gvBalanceView.ExportSettings.OpenInNewWindow = true;
                gvBalanceView.ExportSettings.IgnorePaging = true;
                gvBalanceView.ExportSettings.HideStructureColumns = true;
                gvBalanceView.ExportSettings.ExportOnlyData = true;
                gvBalanceView.ExportSettings.FileName = "View ReturnHolding Details";
                gvBalanceView.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvBalanceView.MasterTableView.ExportToExcel();

            }

            if (hdnExportType == "TCV")
            {
                //DataSet dtFolioDetails = new DataSet();

                //dtFolioDetails = (DataSet)Cache["ViewTrailCommissionDetails" + advisorVo.advisorId.ToString()];
                //gvTrail.DataSource = dtFolioDetails;

                //gvTrail.DataSource = dtFolioDetails;
                gvTrail.ExportSettings.OpenInNewWindow = true;
                gvTrail.ExportSettings.IgnorePaging = true;
                gvTrail.ExportSettings.HideStructureColumns = true;
                gvTrail.ExportSettings.ExportOnlyData = true;
                gvTrail.ExportSettings.FileName = "Trail Details";
                gvTrail.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                gvTrail.MasterTableView.ExportToExcel();

            }

        }

        protected void gvMFTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnkView = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkView.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int transactionId = int.Parse((gvMFTransactions.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString()));
            //int transactionId = int.Parse((gvBalanceView.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString()));
            mfTransactionVo = customerTransactionBo.GetMFTransaction(transactionId);
            Session["MFTransactionVo"] = mfTransactionVo;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('ViewMFTransaction','login');", true);
            //  Response.Write("<script type='text/javascript'>detailedresults=window.open('CustomerPortfolio/ViewMFTransaction.aspx','mywindow', 'width=1000,height=450,scrollbars=yes,location=center');</script>");
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMFTransaction','none');", true);
        }
        protected void lnkViewWithoutAgentCode_Click(object sender, EventArgs e)
        {
            LinkButton lnkViewWithoutAgentCode = (LinkButton)sender;
            GridDataItem gdi;
            gdi = (GridDataItem)lnkViewWithoutAgentCode.NamingContainer;
            int selectedRow = gdi.ItemIndex + 1;
            int transactionId = int.Parse((gvMFTransactionWithoutAgentCode.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString()));
            //int transactionId = int.Parse((gvBalanceView.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString()));
            mfTransactionVo = customerTransactionBo.GetMFTransaction(transactionId);
            Session["MFTransactionVo"] = mfTransactionVo;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMCustomerIndividualDashboard", "loadcontrol('ViewMFTransaction','login');", true);
            //  Response.Write("<script type='text/javascript'>detailedresults=window.open('CustomerPortfolio/ViewMFTransaction.aspx','mywindow', 'width=1000,height=450,scrollbars=yes,location=center');</script>");
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMFTransaction','none');", true);
        }
        protected void gvMFTransactions_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //if (e.CommandName == "Scheme")
            //{
            //    LinkButton lnkScheme = (LinkButton)e.Item.FindControl("lnkprAmc");
            //    GridDataItem gdi;
            //    gdi = (GridDataItem)lnkScheme.NamingContainer;
            //    int selectedRow = gdi.ItemIndex + 1;
            //    int transactionId = int.Parse((gvMFTransactions.MasterTableView.DataKeyValues[selectedRow - 1]["TransactionId"].ToString()));
            //    mfTransactionVo = customerTransactionBo.GetMFTransaction(transactionId);
            //    int month = 0;
            //    int amcCode = mfTransactionVo.AMCCode;
            //    int schemeCode = mfTransactionVo.MFCode;
            //    int year = 0;

            //    if (DateTime.Now.Month != 1)
            //    {
            //        month = DateTime.Now.Month - 1;
            //        year = DateTime.Now.Year;
            //    }
            //    else
            //    {
            //        month = 12;
            //        year = DateTime.Now.Year - 1;
            //    }
            //    string schemeName = mfTransactionVo.SchemePlan;
            //    Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + schemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + schemeName + "&AMCCode=" + amcCode + "", false);
            //}
        }


        #region added for Trail transaction

        private void BindGridTrailCommission(DateTime convertedFromDate, DateTime convertedToDate)
        {
            SetParameter();
            DataSet dsTrailCommissionDetails = new DataSet();
            DataSet ds = new DataSet();
            int rmID = 0;
            int AdviserId = 0;
            if (advisorVo.A_AgentCodeBased == 0)
            {
                if (userType == "advisor" || userType == "ops")
                {
                    AdviserId = advisorVo.advisorId;
                    IsAssociates = 0;
                    gvTrail.Columns[25].Visible = false;
                    gvTrail.Columns[26].Visible = false;
                    gvTrail.Columns[27].Visible = false;
                    gvTrail.Columns[28].Visible = false;
                    gvTrail.Columns[29].Visible = false;
                    gvTrail.Columns[30].Visible = false;
                    gvTrail.Columns[31].Visible = false;
                    gvTrail.Columns[32].Visible = false;
                    gvTrail.Columns[33].Visible = false;
                    gvTrail.Columns[34].Visible = false;
                }
                else if (userType == "rm")
                {
                    rmID = rmVo.RMId;
                    IsAssociates = 0;
                }
            }
            else if (advisorVo.A_AgentCodeBased == 1)
            {
                if (userType == "advisor" || userType == "ops")
                {
                    AdviserId = advisorVo.advisorId;
                    hdnAgentCode.Value = "0";

                }
                else if (userType == "associates")
                {
                    AdviserId = advisorVo.advisorId;


                }

            }
            schemePlanCode = Convert.ToInt32(ViewState["SchemePlanCode"]);
            if (column == "SelectTrail")
            {
                hdnCategory.Value = (ViewState["CategoryCode"]).ToString();
            }
            if (!string.IsNullOrEmpty(txtParentCustomerId.Value.ToString().Trim()))
                customerId = int.Parse(txtParentCustomerId.Value);
            try
            {
                if (rbtnGroup.Checked)
                {
                    dsTrailCommissionDetails = customerTransactionBo.GetRMCustomerTrailCommission(rmID, AdviserId, customerId, convertedFromDate, convertedToDate, 1, PasssedFolioValue, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType);
                }
                else if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    customerId = customerVo.CustomerId;
                    dsTrailCommissionDetails = customerTransactionBo.GetRMCustomerTrailCommission(rmID, AdviserId, customerId, convertedFromDate, convertedToDate, 1, PasssedFolioValue, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType);
                }
                else
                {
                    dsTrailCommissionDetails = customerTransactionBo.GetRMCustomerTrailCommission(rmID, AdviserId, 0, convertedFromDate, convertedToDate, 1, PasssedFolioValue, schemePlanCode, int.Parse(hdnAMC.Value), hdnCategory.Value, advisorVo.A_AgentCodeBased, hdnAgentCode.Value, userType);
                }
                if (dsTrailCommissionDetails.Tables[0].Rows.Count != 0)
                {
                    dsTrailCommissionDetails.Tables[0].Columns.Add("Conditioning");
                    int i = 0;
                    foreach (DataRow dr in dsTrailCommissionDetails.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(dsTrailCommissionDetails.Tables[0].Rows[i]["CMFT_MFTransId"].ToString()))
                            dsTrailCommissionDetails.Tables[0].Rows[i]["Conditioning"] = "Matched";
                        else
                            dsTrailCommissionDetails.Tables[0].Rows[i]["Conditioning"] = "Not Matched";
                        i++;
                    }


                    if (Cache["ViewTrailCommissionDetails" + advisorVo.advisorId] == null)
                    {
                        Cache.Insert("ViewTrailCommissionDetails" + advisorVo.advisorId, dsTrailCommissionDetails);
                    }
                    else
                    {
                        Cache.Remove("ViewTrailCommissionDetails" + advisorVo.advisorId);
                        Cache.Insert("ViewTrailCommissionDetails" + advisorVo.advisorId, dsTrailCommissionDetails);
                    }

                    btnTrnxExport.Visible = true;
                    ErrorMessage.Visible = false;
                    Panel1.Visible = false;

                    GridBoundColumn gbcCustomer = gvBalanceView.MasterTableView.Columns.FindByUniqueName("Customer Name") as GridBoundColumn;

                    gvTrail.DataSource = dsTrailCommissionDetails;
                    gvTrail.DataBind();
                    divTrail.Visible = true;
                    Div3.Visible = true;
                    gvTrail.Visible = true;
                    //imgBtnTrail.Visible = true;
                    ErrorMessage.Visible = false;
                }

                else
                {
                    gvTrail.Visible = false;
                    Div3.Visible = false;
                    divTrail.Visible = false;
                    ErrorMessage.Visible = true;
                    hdnRecordCount.Value = "0";
                    btnTrnxExport.Visible = false;
                    //imgBtnTrail.Visible = false;
                }
                radwindowForManualMerge.VisibleOnPageLoad = false;

                Panel2.Visible = false;
                Panel1.Visible = false;
                //Div1.Visible = false;
                //tbl.Visible = false;

            }
            catch (Exception e)
            {
            }
        }

        protected void RCBForTrailCondiotining_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  
            if (ViewState["Conditioning"] != null)
            {
                Combo.SelectedValue = ViewState["Conditioning"].ToString();
            }

        }

        protected void gvTrail_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && e.Item.ItemIndex == -1)
            {
                DataSet dsRejectedSIP = new DataSet();
                DataTable dtRejectedSIP = new DataTable();
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                RadComboBox RadComboBoxRR = (RadComboBox)filterItem.FindControl("RadComboBoxRR");
                dsRejectedSIP = (DataSet)Cache["ViewTrailCommissionDetails" + advisorVo.advisorId.ToString()];
                dtRejectedSIP = dsRejectedSIP.Tables[0];
                DataTable dtTSIP = new DataTable();
                dtTSIP.Columns.Add("ConditioningCode");
                dtTSIP.Columns.Add("ConditioningDescription");
                DataRow drTSIP;
                foreach (DataRow dr in dtRejectedSIP.Rows)
                {
                    drTSIP = dtTSIP.NewRow();
                    drTSIP["ConditioningCode"] = dr["Conditioning"].ToString();
                    drTSIP["ConditioningDescription"] = dr["Conditioning"].ToString();
                    dtTSIP.Rows.Add(drTSIP);
                }
                //RadComboBoxRR.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("ALL", "0"));
                DataView view = new DataView(dtTSIP);
                DataTable distinctValues = view.ToTable(true, "ConditioningDescription", "ConditioningCode");
                RadComboBoxRR.DataSource = distinctValues;
                RadComboBoxRR.DataValueField = dtTSIP.Columns["ConditioningCode"].ToString();
                RadComboBoxRR.DataTextField = dtTSIP.Columns["ConditioningDescription"].ToString();
                RadComboBoxRR.DataBind();

            }

        }

        protected void gvTrail_PreRender(object sender, EventArgs e)
        {
            if (gvTrail.MasterTableView.FilterExpression != string.Empty)
            {
                DataSet dsRejectedSIP = new DataSet();
                DataTable dtRejectedSIP = new DataTable();
                try
                {
                    dsRejectedSIP = (DataSet)Cache["ViewTrailCommissionDetails" + advisorVo.advisorId.ToString()];
                    dtRejectedSIP = dsRejectedSIP.Tables[0];
                    DataView view = new DataView(dtRejectedSIP);
                    DataTable distinctValues = view.ToTable();
                    DataRow[] rows = distinctValues.Select(gvTrail.MasterTableView.FilterExpression.ToString());
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    gvTrail.MasterTableView.Rebind();
                }
            }
        }

        protected void RCBForTrailCondiotining_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = o as RadComboBox;
            ViewState["Conditioning"] = dropdown.SelectedValue.ToString();
            if (ViewState["Conditioning"] != "")
            {
                GridColumn column = gvTrail.MasterTableView.GetColumnSafe("Conditioning");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvTrail.MasterTableView.Rebind();
            }
            else
            {
                GridColumn column = gvTrail.MasterTableView.GetColumnSafe("Conditioning");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvTrail.MasterTableView.Rebind();
            }

        }

        protected void gvTrail_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //radwindowForManualMerge.VisibleOnPageLoad = true;

            string controlName = this.Request.Params.Get("ctrl_RMMultipleTransactionView$btnMannualMatch");

            if (controlName == "Manual Match")
            {
                radwindowForManualMerge.VisibleOnPageLoad = true;
            }
            else
                radwindowForManualMerge.VisibleOnPageLoad = false;


            DataSet dtTrailstransactionDetails = new DataSet();
            dtTrailstransactionDetails = (DataSet)Cache["ViewTrailCommissionDetails" + advisorVo.advisorId.ToString()];
            gvTrail.DataSource = dtTrailstransactionDetails;
            gvTrail.Visible = true;

            string rcbType = string.Empty;

            DataTable dtrr = new DataTable();
            dtTrailstransactionDetails = (DataSet)Cache["ViewTrailCommissionDetails" + advisorVo.advisorId.ToString()];
            if (dtTrailstransactionDetails != null)
            {
                dtrr = dtTrailstransactionDetails.Tables[0];
                if (ViewState["Conditioning"] != null)
                    rcbType = ViewState["Conditioning"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtrr, "Conditioning = '" + rcbType + "'", "", DataViewRowState.CurrentRows);
                    gvTrail.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvTrail.DataSource = dtrr;

                }
            }
        }


        protected void gvManualMerge_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dtTrailstransactionDetails = new DataSet();
            dtTrailstransactionDetails = (DataSet)Cache["TrnxToBeMergedDetails" + userVo.UserId + userType];
            gvManualMerge.DataSource = dtTrailstransactionDetails;
            gvTrail.Visible = true;
        }

        protected void btnExportTrnxToBeMerged_Click(object sender, ImageClickEventArgs e)
        {
            DataSet dtFolioDetails = new DataSet();

            dtFolioDetails = (DataSet)Cache["TrnxToBeMergedDetails" + advisorVo.advisorId.ToString()];
            gvManualMerge.DataSource = dtFolioDetails;
            gvManualMerge.Visible = true;

            gvManualMerge.DataSource = dtFolioDetails;
            gvManualMerge.ExportSettings.OpenInNewWindow = true;
            gvManualMerge.ExportSettings.IgnorePaging = true;
            gvManualMerge.ExportSettings.HideStructureColumns = true;
            gvManualMerge.ExportSettings.ExportOnlyData = true;
            gvManualMerge.ExportSettings.FileName = "TrnxToBeMerged Details";
            gvManualMerge.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvManualMerge.MasterTableView.ExportToExcel();
        }

        protected void btnTrailExport_Click(object sender, ImageClickEventArgs e)
        {
            DataSet dtFolioDetails = new DataSet();

            dtFolioDetails = (DataSet)Cache["ViewTrailCommissionDetails" + advisorVo.advisorId.ToString()];
            gvTrail.DataSource = dtFolioDetails;
            gvTrail.Visible = true;

            gvTrail.DataSource = dtFolioDetails;
            gvTrail.ExportSettings.OpenInNewWindow = true;
            gvTrail.ExportSettings.IgnorePaging = true;
            gvTrail.ExportSettings.HideStructureColumns = true;
            gvTrail.ExportSettings.ExportOnlyData = true;
            gvTrail.ExportSettings.FileName = "Trail Details";
            gvTrail.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvTrail.MasterTableView.ExportToExcel();
        }

        #endregion

        protected void gvMFTransactions_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string rcbType = string.Empty;
            DataTable dtMFTransaction = new DataTable();
            dtMFTransaction = (DataTable)Cache["ViewTransaction" + userVo.UserId + userType];
            if (dtMFTransaction != null)
            {
                if (ViewState["TransactionStatus"] != null)
                    rcbType = ViewState["TransactionStatus"].ToString();
                if (!string.IsNullOrEmpty(rcbType))
                {
                    DataView dvStaffList = new DataView(dtMFTransaction, "TransactionStatus = '" + rcbType + "'", "Customer Name,Folio Number,Category,AMC,Scheme Name,Transaction Type,Transaction Date,ADUL_ProcessId", DataViewRowState.CurrentRows);
                    gvMFTransactions.DataSource = dvStaffList.ToTable();

                }
                else
                {
                    gvMFTransactions.DataSource = dtMFTransaction;

                }
            }
            //if (Session["IsCustomerDrillDown"] != null)
            //{
            //    trRange.Visible = true;
            //}
            //gvMFTransactions.Visible = true;
            //DataTable dtMFTransactions = new DataTable();
            //dtMFTransactions = (DataTable)Cache["ViewTransaction" + userVo.UserId ];
            //gvMFTransactions.DataSource = dtMFTransactions;

        }

        protected void gvBalanceView_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            gvBalanceView.Visible = true;
            DataTable dtMFBalance = new DataTable();
            dtMFBalance = (DataTable)Cache["ViewBalance" + userVo.UserId + userType];
            gvBalanceView.DataSource = dtMFBalance;
            if (Session["IsCustomerDrillDown"] != null)
            {
                //trRange.Visible = true;
            }

        }
        protected void lbBack_Click(object sender, EventArgs e)
        {

            // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio','none');", true);

        }
        protected void lnkBackHolding_Click(object sender, EventArgs e)
        {


            // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio','none');", true);

        }
        protected void btnTrnxExport_Click(object sender, ImageClickEventArgs e)
        {
            //if (accountId == true)
            //{
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Transaction all ready exist first delete Transactions !!');", true);
            //}
            ExportGrid(hdnExportType.Value);
        }
        protected void btnbalncExport_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void Transaction_PreRender(object sender, EventArgs e)
        {
            RadComboBox Combo = sender as RadComboBox;
            ////persist the combo selected value  
            if (ViewState["TransactionStatus"] != null)
            {
                Combo.SelectedValue = ViewState["TransactionStatus"].ToString();
            }

        }
        protected void RadComboBoxTS_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox dropdown = o as RadComboBox;
            ViewState["TransactionStatus"] = dropdown.SelectedValue.ToString();
            if (ViewState["TransactionStatus"] != "")
            {
                GridColumn column = gvMFTransactions.MasterTableView.GetColumnSafe("TransactionStatus");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvMFTransactions.CurrentPageIndex = 0;
                gvMFTransactions.MasterTableView.Rebind();

            }
            else
            {
                GridColumn column = gvMFTransactions.MasterTableView.GetColumnSafe("TransactionStatus");
                column.CurrentFilterFunction = GridKnownFunction.EqualTo;
                gvMFTransactions.CurrentPageIndex = 0;
                gvMFTransactions.MasterTableView.Rebind();


            }

        }

        protected void gvMFTransactions_OnExcelMLExportStylesCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
        {
            BorderStylesCollection borders = new BorderStylesCollection();
            BorderStyles borderStyle = null;
            for (int i = 1; i <= 3; i++)
            {
                borderStyle = new BorderStyles();
                borderStyle.PositionType = (PositionType)i;
                borderStyle.Color = System.Drawing.Color.Black;
                borderStyle.LineStyle = LineStyle.Continuous;
                borderStyle.Weight = 1.0;
                borders.Add(borderStyle);
            }


            foreach (Telerik.Web.UI.GridExcelBuilder.StyleElement style in e.Styles)
            {
                foreach (BorderStyles border in borders)
                {
                    style.Borders.Add(border);
                }
                if (style.Id == "headerStyle")
                {
                    style.FontStyle.Bold = true;
                    style.FontStyle.Color = System.Drawing.Color.White;
                    style.InteriorStyle.Color = System.Drawing.Color.Black;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "itemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "dateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingDateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
            }

            Telerik.Web.UI.GridExcelBuilder.StyleElement myStyle = new Telerik.Web.UI.GridExcelBuilder.StyleElement("MyCustomStyle");
            myStyle.FontStyle.Bold = true;
            myStyle.FontStyle.Italic = true;
            myStyle.InteriorStyle.Color = System.Drawing.Color.Gray;
            myStyle.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            e.Styles.Add(myStyle);
        }
        protected void gvMFTransactions_OnExcelMLExportRowCreated(object source, GridExportExcelMLRowCreatedArgs e)
        {

            if (e.RowType == GridExportExcelMLRowType.HeaderRow)
            {
                int rowIndex = 0;
                RowElement row = new RowElement();
                CellElement cell = new CellElement();
                cell.MergeAcross = e.Row.Cells.Count - 1;
                cell.Data.DataItem = "Adviser" + ": " + advisorVo.OrganizationName;
                row.Cells.Add(cell);
                e.Worksheet.Table.Rows.Insert(rowIndex, row);
                rowIndex++;
                if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    RowElement row1 = new RowElement();
                    CellElement cell1 = new CellElement();
                    cell1.MergeAcross = e.Row.Cells.Count - 1;
                    cell1.Data.DataItem = "Customer" + ": " + customerVo.FirstName + "  " + customerVo.MiddleName + "  " + customerVo.LastName;
                    row1.Cells.Add(cell1);

                    e.Worksheet.Table.Rows.Insert(rowIndex, row1);
                    rowIndex++;
                }


                RowElement DateRow = new RowElement();
                CellElement Datecell = new CellElement();
                Datecell.MergeAcross = e.Row.Cells.Count - 1;
                Datecell.Data.DataItem = "From" + ": " + Convert.ToDateTime(txtFromDate.SelectedDate).ToLongDateString() + "  " + "To" + ": " + Convert.ToDateTime(txtToDate.SelectedDate).ToLongDateString();
                DateRow.Cells.Add(Datecell);
                e.Worksheet.Table.Rows.Insert(rowIndex, DateRow);
                rowIndex++;


                RowElement BlankRow = new RowElement();
                CellElement Blankcell = new CellElement();
                Blankcell.MergeAcross = e.Row.Cells.Count - 1;
                Blankcell.Data.DataItem = "";
                BlankRow.Cells.Add(Blankcell);
                e.Worksheet.Table.Rows.Insert(rowIndex, BlankRow);



                //e.Worksheet.AutoFilter.Range = e.Worksheet.AutoFilter.Range.Replace("R1", "R2");
            }
        }
        protected void gvBalanceView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                GridDataItem gvr = (GridDataItem)e.Item;
                                int selectedRow = gvr.ItemIndex + 1;
                                int folio = int.Parse(gvr.GetDataKeyValue("AccountId").ToString());
                                int SchemePlanCode = int.Parse(gvr.GetDataKeyValue("SchemePlanCode").ToString());
                                int AMC = int.Parse(gvr.GetDataKeyValue("AMCCode").ToString());
                                string subBrokerCode = gvr.GetDataKeyValue("CMFT_SubBrokerCode").ToString();
                                if (e.CommandName == "SelectTransaction")
                                {
                                    //string name = "SelectAmt";
                                    Response.Redirect("ControlHost.aspx?pageid=RMMultipleTransactionView&folionum=" + folio + "&SchemePlanCode=" + SchemePlanCode + "&AMC=" + AMC + "&subBrokerCode=" + subBrokerCode + "", false);
                                }
                                //if (e.CommandName == "SelectTrail")
                                //{
                                //    string name = "SelectTrail";
                                //    Response.Redirect("ControlHost.aspx?pageid=RMMultipleTransactionView&CategoryCode=" + CategoryCode + "&name=" + name + "", false);
                                //}
                            }
                        }
                    }
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioUnrealized_RowCommand()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void btnTrnxExportMFOffLineWithoutSubbroker_Click(object sender, ImageClickEventArgs e)
        {
            ExportGrid("TVW");
           
        }
    }
}


