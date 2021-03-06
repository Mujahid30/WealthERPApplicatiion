﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using VoUser;
using BoCustomerProfiling;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoAdvisorProfiling;
using BoUploads;
using Telerik.Web.UI;
using BoProductMaster;
using BoOps;
using BOAssociates;
using System.Configuration;
using VoOps;
using BoWerpAdmin;
using VoCustomerPortfolio;
using VOAssociates;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using BoOfflineOrderManagement;
using System.Text.RegularExpressions;
using VoCustomerProfiling;

namespace WealthERP.OffLineOrderManagement
{
    public partial class NCDIssueTransactOffline : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
        OfflineBondOrderBo offlineBondBo = new OfflineBondOrderBo();
        DematAccountVo demataccountvo = new DematAccountVo();
        BoCustomerPortfolio.BoDematAccount bodemataccount = new BoDematAccount();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        UserVo tempUserVo = new UserVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        OnlineBondOrderVo OnlineBondVo = new OnlineBondOrderVo();
        OperationBo operationBo = new OperationBo();
        MFOrderBo mfOrderBo = new MFOrderBo();
        ProductMFBo productMFBo = new ProductMFBo();
        AssetBo assetBo = new AssetBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        OrderBo orderbo = new OrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        RMVo rmVo = new RMVo();
        AssociatesBo associatesBo = new AssociatesBo();
        AssociatesVO associatesVo = new AssociatesVO();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBO = new OnlineNCDBackOfficeBo();
        OfflineIPOOrderBo OfflineIPOOrderBo = new OfflineIPOOrderBo();
        OfflineNCDIPOBackOfficeBo OfflineNCDIPOBackOfficeBo = new OfflineNCDIPOBackOfficeBo();
        BoDematAccount boDematAccount = new BoDematAccount();
        List<DataSet> applicationNoDup = new List<DataSet>();
        UserVo userVo;
        PriceBo priceBo = new PriceBo();
        string path;
        DataTable dtBankName = new DataTable();
        string userType = string.Empty;
        string mail = string.Empty;
        string AgentCode;
        DataTable Agentname;
        DataTable dtAgentId;
        int customerId;
        double sum = 0;
        int Quantity = 0;
        int IssuerId = 0;
        int minQty = 0;
        int maxQty = 0;
        int EligblecatId = 0;
        DataTable BLPName;
        SystematicSetupVo systematicSetupVo = new SystematicSetupVo();

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();

            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            GetUserType();
          
          

            if (!IsPostBack)
            {
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";
                    AutoCompleteExtender4.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender4.ServiceMethod = "GetBLPNameDetails";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                {
                    AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode + "/" + advisorVo.advisorId.ToString() + "/" + associateuserheirarchyVo.IsBranchOps;
                    AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";
                    AutoCompleteExtender4.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender4.ServiceMethod = "GetBLPNameDetails";


                }
                if (AgentCode != null)
                {
                    txtAssociateSearch.Text = AgentCode;
                    OnAssociateTextchanged(this, null);
                }
                BindDepositoryType();
                BindCustomerNCDIssueList();
                //BindSubTypeDropDown(1001);
                btnAddMore.Visible = false;
                //lblApplicationDuplicate.Visible = false;
                if (AgentCode != null)
                {
                    txtAssociateSearch.Text = AgentCode;
                    OnAssociateTextchanged(this, null);
                }
                hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();
                if (hdnIsSubscripted.Value == "True")
                {

                }
                else
                {

                }

                if (Request.QueryString["action"] != null)
                {
                    string action1 = Request.QueryString["action"];
                    int orderId = Convert.ToInt32(Request.QueryString["orderId"].ToString());
                    hdnOrderId.Value = orderId.ToString();
                    ViewOrderList(orderId);
                    btnConfirmOrder.Visible = false;
                    btnAddMore.Visible = false;
                    txtBLPSearch.Text = Request.QueryString["EmpName"];
                    lblBLPText.Text = Request.QueryString["EmpName"];
                    lblBLPCodeText.Text = Request.QueryString["EmpId"];
                    if (action1 == "View")
                    {
                        SetCOntrolsEnablity(false);
                        btnUpdate.Visible = false;
                        gvCommMgmt.Enabled = false;
                        txtBLPSearch.Enabled = false;
                        lblBLPText.Enabled = false;
                        lblBLPCodeText.Enabled = false;
                        Label3.Visible = false;
                        if (Request.QueryString["OrderStepCode"].ToString() == "REJECTED")
                        {
                            lnkEdit.Visible = false;
                            tdtxtReject.Visible = true;
                            tdlblReject.Visible = true;
                        }
                        else
                        {
                            lnkEdit.Visible = true;
                        }
                        if ( Request.QueryString["OrderStepCode"].ToString() == "EXECUTED")
                        {
                            lnkEdit.Visible = false;
                        }
                    }
                    else
                    {
                        if (Request.QueryString["OrderStepCode"].ToString()=="ORDERED")
                        {
                            SetCOntrolsEnablity(false);
                            gvCommMgmt.Enabled = true;

                        }
                        else
                        {
                            SetCOntrolsEnablity(true);

                        }
                        btnUpdate.Visible = true;
                        Label3.Visible = false;
                        txtBLPSearch.Enabled = true;
                        lblBLPText.Enabled = true;
                        lblBLPCodeText.Enabled = true;

                    }
                }
                if (Request.QueryString["CustomerId"] != null)
                {
                    customerId = Convert.ToInt32(Request.QueryString["CustomerId"]);
                    customerVo = customerBo.GetCustomer(customerId);
                    txtCustomerId.Value = customerId.ToString();
                    hdnCustomerId.Value = customerVo.CustomerId.ToString();
                    txtCustomerName.Text = customerVo.FirstName + customerVo.MiddleName + customerVo.LastName;
                    lblGetBranch.Text = customerVo.BranchName;
                    //lblGetRM.Text = customerVo.RMName;
                    lblgetPan.Text = customerVo.PANNum;

                }



            }
        }
        public void GetUserType()
        {

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                if (associateuserheirarchyVo.AgentCode != null)
                {
                    AgentCode = associateuserheirarchyVo.AgentCode.ToString();

                }
                else
                    AgentCode = "0";
            }
        }

        public void BindBankName()
        {

        }


        protected void BindDepositoryType()
        {
            DataTable DsDepositoryNames = new DataTable();
            DsDepositoryNames = boDematAccount.GetDepositoryName();
            ddlDepositoryName.DataSource = DsDepositoryNames;
            if (DsDepositoryNames.Rows.Count > 0)
            {
                ddlDepositoryName.DataTextField = "WCMV_Code";
                ddlDepositoryName.DataValueField = "WCMV_Code";
                ddlDepositoryName.DataBind();
            }
            ddlDepositoryName.Items.Insert(0, new ListItem("Select", "Select"));

        }
        protected void OnBLPTextchanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBLPSearch.Text))
            {
                BLPName = customerBo.GetBLPName(advisorVo.advisorId, txtBLPSearch.Text);
                if (BLPName.Rows.Count > 0)
                {
                    lblBLPCodeText.Text = BLPName.Rows[0][0].ToString();
                    lblBLPText.Text = BLPName.Rows[0][1].ToString();
                }
            }

        }
        protected void OnAssociateTextchanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAssociateSearch.Text))
            {
                Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                if (!string.IsNullOrEmpty(Agentname.Rows[0][2].ToString()))
                {
                    lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                    lblAssociateReportTo.Text = Agentname.Rows[0][2].ToString();
                }
                else
                {
                    lblAssociatetext.Text = "";
                    lblAssociateReportTo.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Agent Code is invalid!');", true);
                    txtAssociateSearch.Text = "";
                }
            }
            txtAssociateSearch.Focus();
        }

        protected void txtAgentId_ValueChanged1(object sender, EventArgs e)
        {
            txtAssociateSearch.Focus();
        }

        protected void HiddenField1_ValueChanged1(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank();
        }

        protected void GetcustomerDetails()
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            if (!string.IsNullOrEmpty(txtCustomerId.Value))
            {
                ViewState["CustomerId"] = txtCustomerId.Value;
            }
            else
            {
                txtCustomerId.Value = hdnCustomerId.Value;
            }
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(int.Parse(txtCustomerId.Value));
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            Session["customerVo"] = customerVo;
            lblGetBranch.Text = customerVo.BranchName;
            lblgetPan.Text = customerVo.PANNum;
            hdnCustomerId.Value = txtCustomerId.Value;
            txtCustomerName.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            hdnPortfolioId.Value = customerPortfolioVo.PortfolioId.ToString();
            ViewState["PortfolioId"] = customerPortfolioVo.PortfolioId.ToString();
            customerId = int.Parse(txtCustomerId.Value);
            txtPansearch.Text = customerVo.PANNum;
            lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            BindBank();
            Panel2.Visible = true;
        }

        private void BindBank()
        {
            CommonLookupBo commonLookupBo = new CommonLookupBo();
            ddlBankName.Items.Clear();
            DataTable dtBankName = new DataTable();
            if (ddlPaymentMode.SelectedValue == "ES")
            {
                dtBankName = commonLookupBo.GetWERPLookupMasterValueList(7000, 1);
            }
            else
            {
                dtBankName = commonLookupBo.GetWERPLookupMasterValueList(7000, 0);
            }
            ddlBankName.DataSource = dtBankName;
            ddlBankName.DataValueField = dtBankName.Columns["WCMV_LookupId"].ToString();
            ddlBankName.DataTextField = dtBankName.Columns["WCMV_Name"].ToString();
            ddlBankName.DataBind();
            //ddlBankName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }

        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaymentMode(ddlPaymentMode.SelectedValue);
            BindBank();
            ddlPaymentMode.Focus();
        }

        private void PaymentMode(string type)
        {
            trPINo.Visible = false;
            trASBA.Visible = false;
            RequiredFieldValidator8.Enabled = false;
            //CompareValidator14.Enabled = false;
            RequiredFieldValidator9.Enabled = false;
            if (ddlPaymentMode.SelectedValue == "CQ")
            {
                trPINo.Visible = true;
                RequiredFieldValidator8.Enabled = true;
                //CompareValidator14.Enabled = true;
                Td3.Visible = true;
                Td4.Visible = true;
                txtASBANO.Text = "";
            }
            else if (ddlPaymentMode.SelectedValue == "ES")
            {
                trASBA.Visible = true;
                RequiredFieldValidator9.Enabled = true;
                Td3.Visible = false;
                Td4.Visible = false;
                txtPaymentNumber.Text = "";
            }

        }
        protected void ddlCustomerSubType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomerSubType.SelectedIndex < -1) return;

        }
        private void BindCustomerNCDIssueList()
        {
            DataTable dtIssueList = new DataTable();
            dtIssueList = onlineNCDBackOfficeBO.GetIssueList(advisorVo.advisorId, 1, "FI");
            ddlIssueList.DataTextField = dtIssueList.Columns["AIM_IssueName"].ToString();
            ddlIssueList.DataValueField = dtIssueList.Columns["AIM_IssueId"].ToString();
            ddlIssueList.DataSource = dtIssueList;
            ddlIssueList.DataBind();
            ddlIssueList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
        }
        protected void ddlIssueList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueList.SelectedValue.ToLower() != "select")
            {
                BindStructureRuleGrid(advisorVo.advisorId, int.Parse(ddlIssueList.SelectedValue), 1,0);
               
                BindIssueCategory(int.Parse(ddlIssueList.SelectedValue));
                BindSubbroker(int.Parse(ddlIssueList.SelectedValue));
            }
            ddlIssueList.Focus();
        }
        private void BindIssueCategory(int issueId)
        {
            DataTable dt = OfflineIPOOrderBo.GetIssueCategory(issueId);
            ddlCategory.DataSource = dt;
            ddlCategory.DataValueField = "AIIC_InvestorCatgeoryId";
            ddlCategory.DataTextField = "AIIC_InvestorCatgeoryName";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIssueList.SelectedValue != "0")
            {
                BindStructureRuleGridList(int.Parse(ddlIssueList.SelectedValue), 0,int.Parse(ddlCategory.SelectedValue));
            }
        }
        private void RadDateControlBusinessDateValidation(ref RadDatePicker rdtp, int noOfDays, DateTime dtDate, int isPastdateReq)
        {
            DataTable dtTradaDate = mfOrderBo.GetTradeDateListForOrder(dtDate, isPastdateReq, noOfDays);

            DateTime dtMinDate = Convert.ToDateTime(dtTradaDate.Compute("min(WTD_Date)", string.Empty));
            DateTime dtMaxDate = Convert.ToDateTime(dtTradaDate.Compute("max(WTD_Date)", string.Empty));

            rdtp.MinDate = dtMinDate;
            rdtp.MaxDate = dtMaxDate;
            DateTime dtTempIncrement;

            while (dtMinDate < dtMaxDate)
            {
                dtTempIncrement = dtMinDate.AddDays(1);

                DataRow[] foundRows = dtTradaDate.Select(String.Format("WTD_Date='{0}'", dtTempIncrement.ToString("O")));
                if (foundRows.Count() == 0)
                {
                    RadCalendarDay holiday = new RadCalendarDay();
                    holiday.Date = dtTempIncrement.Date;
                    holiday.IsSelectable = false;
                    holiday.IsDisabled = true;
                    rdtp.Calendar.SpecialDays.Add(holiday);
                }

                dtMinDate = dtTempIncrement;
            }

        }
        protected void BindStructureRuleGrid(int adviserId, int issueId, int IssueStatus,int category)
        {

            DataTable dtIssue = new DataTable();
            //1--- For Curent Issues
            tblgvIssueList.Visible = true;
            pnlNCDIssueList.Visible = true;
            dtIssue = offlineBondBo.GetOfflineAdviserIssuerList(adviserId, issueId, IssueStatus,0).Tables[0];


            if (Cache["NCDIssueList" + userVo.UserId.ToString()] != null)
            {
                Cache.Remove("NCDIssueList" + userVo.UserId.ToString());
            }
            Cache.Insert("NCDIssueList" + userVo.UserId.ToString(), dtIssue);

            gvIssueList.DataSource = dtIssue;
            gvIssueList.DataBind();

        }
        protected void BindStructureRuleGridList(int IssuerId,  int orderId,int category)
        {
            DataSet dsStructureRules = offlineBondBo.GetOfflineLiveBondTransaction(IssuerId, orderId, category);
            DataTable dtTransact = dsStructureRules.Tables[0];

            if (Cache["NCDTransactList" + userVo.UserId.ToString()] != null)
            {
                Cache.Remove("NCDTransactList" + userVo.UserId.ToString());
            }
            Cache.Insert("NCDTransactList" + userVo.UserId.ToString(), dtTransact);

            gvCommMgmt.DataSource = dtTransact;
            ViewState["Transact"] = dtTransact;
            gvCommMgmt.DataBind();
            pnlNCDIssueList.Visible = true;
            tblgvCommMgmt.Visible = true;

        }
        protected void gvCommMgmt_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail = (DataTable)Cache["NCDTransactList" + userVo.UserId.ToString()];
            if (dtIssueDetail != null)
            {
                gvCommMgmt.DataSource = dtIssueDetail;
            }

        }
        protected void gvCommMgmt_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                GridDataItem dataform = (GridDataItem)e.Item;
                {
                    if (Request.QueryString["action"] != null && (e.Item.ItemIndex != -1))
                    {
                        gvCommMgmt.MasterTableView.GetColumn("COID_ExchangeRefrenceNo").Visible = true;
                        gvCommMgmt.MasterTableView.GetColumn("DeleteBid").Visible = true;

                    }
                }

            }
            if (Request.QueryString["action"] != null && ViewState["amt"] !=null)
            {
                if (e.Item is GridFooterItem)
                {
                    GridFooterItem ftItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                    Label lblAmount = (Label)ftItemAmount["Amount"].FindControl("lblAmount");
                    Label lblQuantity = (Label)ftItemAmount["Quantity"].FindControl("lblQuantity");
                        lblAmount.Text = ViewState["amt"].ToString();
                        lblQuantity.Text = ViewState["qty"].ToString();
                }
            }

        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int rowindex1 = ((GridDataItem)((TextBox)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            int issueId = Convert.ToInt32(Request.QueryString["IssuerId"]);
            string catName = string.Empty;
            string Description = string.Empty;
            int catId = 0;
            int issuedetId = 0;
            double AIM_FaceValue = 0.0;
            TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowindex]["Quantity"].FindControl("txtQuantity");
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                string message = string.Empty;
                int rowno = 0;
                int PFISD_InMultiplesOf = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AIM_TradingInMultipleOf"].ToString());
                // Regex re = new Regex(@"[@\\*+#^\\.\$\-?A-Za-z]+");
                Regex re = new Regex(@"^[0-9]\d*$");
                if (re.IsMatch(txtQuantity.Text))
                {
                    int Qty = Convert.ToInt32(txtQuantity.Text);
                    int Mod = Qty % PFISD_InMultiplesOf;
                    if (Mod != 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please enter quantity greater than or equal to min quantity required and in multiples of 1')", true);
                        txtQuantity.Text = "";
                        return;
                    }
                    if (gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AID_SeriesFaceValue"].ToString() == "")
                        return;
                    AIM_FaceValue = Convert.ToDouble(gvCommMgmt.MasterTableView.DataKeyValues[rowindex]["AID_SeriesFaceValue"].ToString());
                    TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowindex]["Amount"].FindControl("txtAmount");
                    txtAmount.Text = Convert.ToString(Qty * AIM_FaceValue);
                    CheckBox cbSelectOrder = (CheckBox)gvCommMgmt.MasterTableView.Items[rowindex]["Check"].FindControl("cbOrderCheck");
                    cbSelectOrder.Checked = true;
                    foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                    {
                        TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowno]["Quantity"].FindControl("txtQuantity");
                        TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowno]["Amount"].FindControl("txtAmount");
                        GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                        GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                        if (cbSelectOrder.Checked == true)
                            if (!string.IsNullOrEmpty(txtsumQuantity.Text) && !string.IsNullOrEmpty(txtsumAmount.Text))
                            {

                                Quantity = Quantity + Convert.ToInt32(txtsumQuantity.Text);
                                ViewState["Qty"] = Quantity;
                                sum = sum + Convert.ToDouble(txtsumAmount.Text);
                                ViewState["Sum"] = sum;
                                lblQty.Text = Quantity.ToString();
                                lblSum.Text = sum.ToString();
                                OnlineBondBo.GetCustomerCat(issueId, customerVo.CustomerId, advisorVo.advisorId, Convert.ToDouble(lblSum.Text), ref catName, ref issuedetId, ref catId, ref Description);

                                ViewState["CustCat"] = catName;

                            }
                        if (rowno < gvCommMgmt.MasterTableView.Items.Count)
                        {
                            rowno++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please enter only Valid Numbers & in multiples of 1')", true);
                }
            }
            else
            {
                foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                {
                    TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Quantity"].FindControl("txtQuantity");
                    TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Amount"].FindControl("txtAmount");
                    GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                    Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                    GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                    Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                    txtQuantity.Text = "";
                    txtsumQuantity.Text = "";
                    txtsumAmount.Text = " ";
                    lblQty.Text = "";
                    lblSum.Text = "";
                }


            }
        }
        //private void ShowMessage(string msg)
        //{
        //    trMessage.Visible = true;
        //    msgRecordStatus.InnerText = msg;
        //}
        private void ShowMessage(string msg, char type)
        {
            //--S(success)
            //--F(failure)
            //--W(warning)
            //--I(information)
            trMessage.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + type.ToString() + "');", true);
        }

        private bool CollectOrderDetails(object sender, EventArgs e, out DataTable dtOrderDetails)
        {
            int issueDetId = 0;
            int catId = 0;
            int rowNo = 0;
            int tableRow = 0;
            int FaceValue = 0;
            dtOrderDetails = null;
            double mincategory=0;
            double maxcategory = 0;
            if (gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString() == "" || gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_FaceValue"].ToString() == "")
                return false;
            string MaxAppNo = gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString();
            FaceValue = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_FaceValue"].ToString());
            maxcategory = Convert.ToDouble(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIIC_MaxBidAmount"].ToString());
            mincategory = Convert.ToDouble(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIIC_MInBidAmount"].ToString());
            int minQty = int.Parse(gvIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString());
            int maxQty = int.Parse(gvIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString());
            DataTable dt = new DataTable();
            bool isValid = false;
            dt.Columns.Add("AID_IssueDetailId");
            dt.Columns.Add("AIM_IssueId");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("CatId");
            dt.Columns.Add("AcceptableCatId");
            dt.Columns.Add("ApplicationNO");
            dt.Columns.Add("ModeOfPayment");
            dt.Columns.Add("ASBAAccNo");
            dt.Columns.Add("BankName");
            dt.Columns.Add("BranchName");
            dt.Columns.Add("ChequeDate");
            dt.Columns.Add("ChequeNo");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("BrokerCode");
            dt.Columns.Add("AgentCode");


            foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
            {

                TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");
                if (txtQuantity.Text == string.Empty)
                {
                    if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
                    {
                        rowNo = rowNo + 1;
                    }
                    continue;
                }
                OnlineBondVo.ApplicationNumber = txtApplicationNo.Text;
                OnlineBondVo.PaymentMode = ddlPaymentMode.SelectedValue;
                OnlineBondVo.BankBranchName = txtBranchName.Text;
                OnlineBondVo.Remarks = txtRemarks.Text;
                OnlineBondVo.CustomerName = txtFirstName.Text;
                OnlineBondVo.DematBeneficiaryAccountNum = txtDpClientId.Text;
                OnlineBondVo.DematDPId = txtDPId.Text;
                OnlineBondVo.DematDepositoryName = ddlDepositoryName.SelectedValue;
                OnlineBondVo.CustomerSubTypeId = int.Parse(ddlCategory.SelectedValue);
                if (rbtnIndividual.Checked)
                    OnlineBondVo.CustomerType = "IND";
                else
                    OnlineBondVo.CustomerType = "NIND";
                if (!string.IsNullOrEmpty(ddlBrokerCode.SelectedValue))
                {
                    OnlineBondVo.BrokerCode = ddlBrokerCode.SelectedValue;
                }
                else
                {
                    OnlineBondVo.BrokerCode = DBNull.Value.ToString();
                }
                if (ddlPaymentMode.SelectedValue == "CQ")
                {
                    OnlineBondVo.ChequeNumber = txtPaymentNumber.Text;
                    if(txtPaymentInstDate.SelectedDate!=null)
                    OnlineBondVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
                    if (!string.IsNullOrEmpty(txtBankAccount.Text))
                        OnlineBondVo.BankAccountNo = Int64.Parse(txtBankAccount.Text);
                }
                OnlineBondVo.PanNo = txtPanNumber.Text;
                OnlineBondVo.AgentNo = txtAssociateSearch.Text;

                OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AID_IssueDetailId"].ToString());
                OnlineBondVo.IssueId = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIM_IssueId"].ToString());
                OnlineBondVo.PFISM_SchemeId = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AID_Sequence"].ToString());
                CheckBox Check = (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
                catId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIDCSR_Id"].ToString());
                EligblecatId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIIC_InvestorCatgeoryId"].ToString());
                if (Check.Checked == true || Request.QueryString["action"] != null)
                {
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        isValid = true;
                        txtQuantity.Enabled = true;

                        string catName = string.Empty;
                        string Description = string.Empty;
                        OnlineBondVo.Qty = Convert.ToInt32(txtQuantity.Text);
                        TextBox txtAmount = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Amount"].FindControl("txtAmount");
                        OnlineBondVo.Amount = Convert.ToDouble(txtAmount.Text);
                        dt.Rows.Add();
                        dt.Rows[tableRow]["AID_IssueDetailId"] = OnlineBondVo.PFISD_SeriesId;
                        dt.Rows[tableRow]["AIM_IssueId"] = OnlineBondVo.IssueId;
                        dt.Rows[tableRow]["Qty"] = OnlineBondVo.Qty;
                        dt.Rows[tableRow]["Amount"] = OnlineBondVo.Amount;
                        dt.Rows[tableRow]["ApplicationNO"] = OnlineBondVo.ApplicationNumber;
                        dt.Rows[tableRow]["ModeOfPayment"] = OnlineBondVo.PaymentMode;
                        dt.Rows[tableRow]["ASBAAccNo"] = txtASBANO.Text;
                        dt.Rows[tableRow]["BankName"] = ddlBankName.SelectedValue;
                        dt.Rows[tableRow]["BranchName"] = OnlineBondVo.BankBranchName;
                        dt.Rows[tableRow]["Remarks"] = OnlineBondVo.Remarks;
                        dt.Rows[tableRow]["BrokerCode"] = OnlineBondVo.BrokerCode;
                        dt.Rows[tableRow]["AgentCode"] = OnlineBondVo.AgentNo;
                        if (ddlPaymentMode.SelectedValue == "CQ")
                        {
                            if (txtPaymentInstDate.SelectedDate != null)
                            dt.Rows[tableRow]["ChequeDate"] = OnlineBondVo.PaymentDate.ToString("yyyy/MM/dd");
                            dt.Rows[tableRow]["ChequeNo"] = OnlineBondVo.ChequeNumber;
                        }
                        GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                        txtAmount.Text = OnlineBondVo.Amount.ToString();
                        dtOrderDetails = dt;
                        //if (Request.QueryString["action"] == null)
                        //{
                        //    offlineBondBo.GetCustomerCat(OnlineBondVo.IssueId, advisorVo.advisorId, int.Parse(ddlCategory.SelectedValue), Convert.ToDouble(lblSum.Text), ref catName, ref issueDetId, ref EligblecatId, ref Description);
                            ViewState["CustCat"] = ddlCategory.SelectedItem.Text;
                            //if (EligblecatId == 0)
                            //{
                            //    ShowMessage("Application amount should be between Min Quantity and Max Quantity.", 'W');
                            //    return false;
                            //}
                        //}
                        dt.Rows[tableRow]["CatId"] = catId;
                        dt.Rows[tableRow]["AcceptableCatId"] = EligblecatId;
                        
                    }

                }
                if (rowNo < gvCommMgmt.MasterTableView.Items.Count)
                {
                    if (dt.Rows.Count >= 1)
                    {
                        rowNo = rowNo + 1;
                        tableRow++;
                    }
                }
                else
                    break;
            }
            GridFooterItem ftItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
            Label lbltotAmt = (Label)ftItemAmount.FindControl("lblAmount");
            Label lblQuantity = (Label)ftItemAmount.FindControl("lblQuantity");
            if (isValid)
            {
                //isValid = false;
                //if (Validation())
                //{

                if (Request.QueryString["action"] != null)
                {
                    Quantity = int.Parse(lblQuantity.Text);
                    sum = Convert.ToDouble(lbltotAmt.Text);
                }
                else
                {
                    Quantity = int.Parse(ViewState["Qty"].ToString());
                    sum = int.Parse(ViewState["Sum"].ToString());
                }
                //if ( Request.QueryString["action"] == null)
                //{

                //    string category = (string)ViewState["CustCat"];
                //    if (category == string.Empty)
                //        ShowMessage("Please enter no of bonds within the range permissible.", 'w');

                //}
                 if (sum < mincategory)
                {
                    ShowMessage("Application amount should be between min. Amt. and Max. Amt.", 'w');
                    isValid = false;
                }
                else if (Quantity < minQty && Quantity > 0)
                {
                    foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                    {
                        TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Quantity"].FindControl("txtQuantity");
                        TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Amount"].FindControl("txtAmount");
                        GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                        GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order cannot be processed.Please enter quantity greater than or equal to min quantity required')", true);
                        //txtsumQuantity.Text = "";
                        //txtsumAmount.Text = "";
                        //lblQty.Text = "";
                        //lblSum.Text = "";
                        isValid = false;
                    }
                }
                else if (Quantity > maxQty && Quantity > 0)
                {
                    foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
                    {
                        TextBox txtsumQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Quantity"].FindControl("txtQuantity");
                        TextBox txtsumAmount = (TextBox)gvCommMgmt.MasterTableView.Items[CBOrder.ItemIndex]["Amount"].FindControl("txtAmount");
                        GridFooterItem footerItem = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblQty = (Label)footerItem.FindControl("lblQuantity");
                        GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Order cannot be processed.Please enter quantity less than or equal to maximum quantity allowed for this issue')", true);
                        isValid = false;
                        //txtsumQuantity.Text = "";
                        //txtsumAmount.Text = "";
                        //lblQty.Text = "";
                        //lblSum.Text = "";
                    }

                    //}
                }
                else
                    isValid = true;

                //}
                //else
                //    isValid = true;


            }
            return isValid;

        }
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            int agentId = 0;
            DataTable dtOrderDetails = new DataTable();
            bool isValid = CollectOrderDetails(sender, e, out dtOrderDetails);
            GridFooterItem ftItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
            Label lbltotAmt = (Label)ftItemAmount.FindControl("lblAmount");
            Page.Validate("btnConfirmOrder");
            if (Page.IsValid)
            {
                if (isValid)
                {

                    IDictionary<string, string> orderIds = new Dictionary<string, string>();

                    int totalOrderAmt = int.Parse(ViewState["Sum"].ToString());
                    string message;
                    string aplicationNoStatus = string.Empty;
                    int orderId = 0;
                    if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
                        dtAgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                    if (dtAgentId.Rows.Count > 0)
                    {
                        agentId = int.Parse(dtAgentId.Rows[0][1].ToString());
                    }
                    if (!String.IsNullOrEmpty(txtBLPSearch.Text))
                    {
                        BLPName = customerBo.GetBLPName(advisorVo.advisorId, txtBLPSearch.Text);
                        if (BLPName.Rows.Count > 0)
                        {
                            mforderVo.EmpId = int.Parse(BLPName.Rows[0][2].ToString());
                        }
                        else
                        {
                            mforderVo.EmpId = 0;
                        }
                    }

                    orderIds = offlineBondBo.OfflineBOndtransact(dtOrderDetails, advisorVo.advisorId, OnlineBondVo, agentId, txtAssociateSearch.Text, userVo.UserId, mforderVo.EmpId);
                    orderId = int.Parse(orderIds["Order_Id"].ToString());
                    aplicationNoStatus = orderIds["aplicationNoStatus"].ToString();
                    ViewState["OrderId"] = orderId;
                    hdnOrderId.Value = orderId.ToString();
                    btnConfirmOrder.Enabled = false;
                    Label3.Visible = false;
                    message = CreateUserMessage(orderId, aplicationNoStatus);
                    ShowMessage(message, 'S');
                    btnConfirmOrder.Visible = false;
                    btnAddMore.Visible = true;
                    SetCOntrolsEnablity(false);
                    btnAddMore.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert(' Quantity should be between Min Quantity and Max Quantity.')", true);
                }
            }
        }

        private string CreateUserMessage(int orderId, string aplicationNoStatus)
        {
            string userMessage = string.Empty;
            string cutOffTimeType = string.Empty;

            if (orderId != 0)
            {
                userMessage = "Order placed successfully, Order reference no. is " + orderId.ToString();
            }
            else if (aplicationNoStatus == "Refill")
            {
                userMessage = "Order cannot be placed , Application oversubscribed. Please contact your relationship manager or contact call centre";
            }

            else if (orderId == 0)
            {
                userMessage = "Order cannot be processed. Issue Got Closed";
            }
            return userMessage;
        }
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('NCDIssueTransactOffline','login');", true);
            tdsubmit.Visible = true;
            Label3.Visible = true;
            btnConfirmOrder.Visible = true;
        }

        private void ClearAllFields()
        {
            txtCustomerName.Text = "";
            lblgetPan.Text = "";
            lblGetBranch.Text = "";
            lblAssociatetext.Text = "";
            ddlIssueList.SelectedIndex = 0;
            txtApplicationNo.Text = "";
            ddlPaymentMode.SelectedIndex = 0;
            ddlBankName.SelectedIndex = -1;
            txtBranchName.Text = "";
            txtAmount.Text = "";
            txtPaymentNumber.Text = "";
            txtPaymentInstDate.SelectedDate = null;
            txtDematid.Text = "";
            txtASBANO.Text = "";
            pnlJointHolderNominee.Visible = false;
            pnlNCDOOrder.Visible = false;
            pnlIssuList.Visible = false;
            pnlNCDTransact.Visible = false;
            txtRemarks.Text = "";
            trSumbitSuccess.Visible = false;
        }
        public bool Validation()
        {
            bool result = true;
            int issueId = int.Parse(ddlIssueList.SelectedValue.ToString());
            try
            {
                if (Request.QueryString["action"] == null)
                {
                    if (OfflineIPOOrderBo.ApplicationDuplicateCheck(issueId, int.Parse(txtApplicationNo.Text)))
                    {
                        result = false;
                        lblApplicationDuplicate.Visible = true;
                    }
                    else
                    {
                        lblApplicationDuplicate.Visible = false;
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
                FunctionInfo.Add("Method", "IPOIssueTransactOffline.ascx:Validation()");
                object[] objects = new object[1];
                objects[0] = result;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        protected void txtPaymentInstDate_OnSelectedDateChanged(object sender, EventArgs e)
        {
            txtPaymentInstDate.Focus();
        }
        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown(1001);
        }
        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown(1002);
        }

        private void BindSubTypeDropDown(int lookupParentId)
        {
            DataTable dtCustomerTaxSubTypeLookup = new DataTable();

            dtCustomerTaxSubTypeLookup = commonLookupBo.GetWERPLookupMasterValueList(2000, lookupParentId);
            ddlCustomerSubType.DataSource = dtCustomerTaxSubTypeLookup;
            ddlCustomerSubType.DataTextField = "WCMV_Name";
            ddlCustomerSubType.DataValueField = "WCMV_LookupId";
            ddlCustomerSubType.DataBind();
            ddlCustomerSubType.Items.Insert(0, new ListItem("Select", "0"));
            //if (rbtnIndividual.Checked == true)
            //    ddlCustomerSubType.SelectedValue = "2017";
        }
        protected void ddlDepositoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepositoryName.SelectedItem.Text == "NSDL")
            {
                txtDPId.Enabled = true;
                txtDpClientId.MaxLength = 8;
                txtDpClientId.Text = "";
            }
            else if (ddlDepositoryName.SelectedItem.Text == "CDSL")
            {
                txtDPId.Enabled = false;
                txtDpClientId.MaxLength = 16;
                txtDPId.Text = "";
            }
            ddlDepositoryName.Focus();
        }

        protected void BindModeofHolding()
        {
            DataSet dsModeOfHolding = bodemataccount.GetXmlModeOfHolding();
            ddlModeOfHolding.DataSource = dsModeOfHolding;
            ddlModeOfHolding.DataTextField = "XMOH_ModeOfHolding";
            ddlModeOfHolding.DataValueField = "XMOH_ModeOfHoldingCode";
            ddlModeOfHolding.DataBind();
            ddlModeOfHolding.SelectedIndex = 8;
        }

        protected void ViewOrderList(int orderId)
        {
            DataSet dtOrderDetails = OfflineNCDIPOBackOfficeBo.GetNCDIssueOrderDetails(orderId);
            if (dtOrderDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dtOrderDetails.Tables[0].Rows)
                {
                    Agentname = customerBo.GetAssociateName(advisorVo.advisorId, dr["AAC_AgentCode"].ToString());
                    if (Agentname.Rows.Count > 0)
                    {
                        lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                        lblAssociateReportTo.Text = Agentname.Rows[0][2].ToString();
                    }
                    else
                    {
                        lblAssociatetext.Text = string.Empty;
                        lblAssociateReportTo.Text = string.Empty;
                    }

                    txtAssociateSearch.Text = dr["AAC_AgentCode"].ToString();
                    txtAssociateSearch.Text = dr["AAC_AgentCode"].ToString();
                    string issue = dr["AIM_IssueId"].ToString();
                    BindIssueCategory(int.Parse(dr["AIM_IssueId"].ToString()));
                    ddlCategory.SelectedValue = dr["AIIC_ApplicableInvestorCatgeoryId"].ToString();
                    BindStructureRuleGrid(advisorVo.advisorId, int.Parse(dr["AIM_IssueId"].ToString()), 1,0);
                    BindStructureRuleGridList(int.Parse(dr["AIM_IssueId"].ToString()), orderId, int.Parse(dr["AIIC_ApplicableInvestorCatgeoryId"].ToString()));
                    BindCustomerNCDIssueList();
                    ddlIssueList.SelectedValue = dr["AIM_IssueId"].ToString();
                    BindSubbroker(int.Parse(dr["AIM_IssueId"].ToString()));
                    txtApplicationNo.Text = dr["CO_ApplicationNo"].ToString();
                    hdnApplicationNo.Value = txtApplicationNo.Text;
                    txtFirstName.Text = dr["OCD_Name"].ToString();
                    txtPanNumber.Text = dr["OCD_Pan"].ToString();
                    //if (dr["XCT_CustomerTypeCode"].ToString() == "IND")
                    //{
                    //    rbtnIndividual.Checked = true;
                    //    //BindSubTypeDropDown(1001);
                    //    //ddlCustomerSubType.SelectedValue = dr["OCD_WCMV_TaxStatus_Id"].ToString();
                    //}
                    //else
                    //{
                    //    rbtnNonIndividual.Checked = true;
                    //    //BindSubTypeDropDown(1002);
                    //    //ddlCustomerSubType.SelectedValue = dr["OCD_WCMV_TaxStatus_Id"].ToString();
                    //}
                    txtDpClientId.Text = dr["OCD_BeneficiaryAccountNum"].ToString();
                    ddlDepositoryName.SelectedValue = dr["OCD_DepositoryName"].ToString();
                    txtDPId.Text = dr["OCD_DPId"].ToString();

                    txtRemarks.Text = dr["CO_Remarks"].ToString();
                    BindBank();
                    ddlBankName.SelectedValue = dr["CO_BankName"].ToString();
                    ddlBrokerCode.SelectedValue = dr["XB_BrokerIdentifier"].ToString();
                    if (dr["CO_ASBAAccNo"].ToString() != "")
                    {

                        ddlPaymentMode.SelectedValue = "ES";
                        txtASBANO.Text = dr["CO_ASBAAccNo"].ToString();
                        txtBranchName.Text = dr["CO_BankBranchName"].ToString();
                        trASBA.Visible = true;
                    }
                    else
                    {
                        txtBranchName.Text = dr["CO_BankBranchName"].ToString();
                        ddlPaymentMode.SelectedValue = "CQ";
                        txtPaymentNumber.Text = dr["CO_ChequeNumber"].ToString();
                        if (dr["CO_PaymentDate"].ToString() != null && dr["CO_PaymentDate"].ToString() !="")
                        txtPaymentInstDate.SelectedDate = Convert.ToDateTime(dr["CO_PaymentDate"].ToString());
                        if (dr["COID_DepCustBankAccId"].ToString() != string.Empty)
                            txtBankAccount.Text = dr["COID_DepCustBankAccId"].ToString().Substring(0, dr["COID_DepCustBankAccId"].ToString().IndexOf('.'));
                        Td3.Visible = true;
                        Td4.Visible = true;
                        trPINo.Visible = true;
                    }

                    ddlBankName.SelectedValue = dr["CO_BankName"].ToString();
                }
            }
            foreach (DataRow dr1 in dtOrderDetails.Tables[1].Rows)
            {
                GridFooterItem ftItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                Label lblAmount = (Label)ftItemAmount["Amount"].FindControl("lblAmount");
                Label lblQuantity = (Label)ftItemAmount["Quantity"].FindControl("lblQuantity");
                lblAmount.Text = dr1["payable"].ToString();
                lblQuantity.Text = dr1["Quantity"].ToString();
                foreach (GridDataItem gdi in gvCommMgmt.MasterTableView.Items)
                {
                    int detailsid = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[gdi.ItemIndex]["AID_IssueDetailId"].ToString());
                    TextBox txtQuantity = (TextBox)gdi.FindControl("txtQuantity");
                    TextBox txtAmount = (TextBox)gdi.FindControl("txtAmount");
                    if (detailsid == int.Parse(dr1["AID_IssueDetailId"].ToString()))
                    {
                        if (dr1["COID_TransactionType"].ToString() == "D" || dr1["COID_TransactionType"].ToString() == "ND")
                        {
                            txtQuantity.CssClass = "txtDisableField";
                            txtQuantity.ToolTip = "The Category Cannot be edited because it was Cancelled previously";
                            txtQuantity.ReadOnly = true;
                            txtAmount.CssClass = "txtDisableField";
                            txtAmount.ToolTip = "The Amount Cannot be edited because it was Cancelled previously";
                            txtAmount.ReadOnly = true;
                             gvCommMgmt.MasterTableView.GetColumn("DeleteBid").Display = false;
                          
                        }
                        else
                            gvCommMgmt.MasterTableView.GetColumn("DeleteBid").Display = true;

                    }
                }
            }
            //BindQtyandAmt(dtOrderDetails.Tables[1]);
        }
        protected void BindQtyandAmt(DataTable dtOrderDetails)
        {

            foreach (DataRow dr1 in dtOrderDetails.Rows)
            {
                GridFooterItem ftItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                Label lblAmount = (Label)ftItemAmount["Amount"].FindControl("lblAmount");
                Label lblQuantity = (Label)ftItemAmount["Quantity"].FindControl("lblQuantity");
                lblAmount.Text = dr1["payable"].ToString();
                lblQuantity.Text = dr1["Quantity"].ToString();
                if (Cache["NCDTransactList" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("NCDTransactList" + userVo.UserId.ToString());
                }
                Cache.Insert("NCDTransactList" + userVo.UserId.ToString(), dtOrderDetails);
                foreach (GridDataItem gdi in gvCommMgmt.MasterTableView.Items)
                {
                    int detailsid = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[gdi.ItemIndex]["AID_IssueDetailId"].ToString());
                    TextBox txtQuantity = (TextBox)gdi.FindControl("txtQuantity");
                    TextBox txtAmount = (TextBox)gdi.FindControl("txtAmount");
                    if (detailsid == int.Parse(dr1["AID_IssueDetailId"].ToString()))
                    {
                        txtQuantity.Text = dr1["COID_Quantity"].ToString();
                        txtAmount.Text = dr1["COID_AmountPayable"].ToString();

                        if (dr1["COID_TransactionType"].ToString() == "D" || dr1["COID_TransactionType"].ToString() == "ND")
                        {
                            txtQuantity.CssClass = "txtDisableField";
                            txtQuantity.ToolTip = "The Category Cannot be edited because it was Cancelled previously";
                            txtQuantity.ReadOnly = true;
                            txtAmount.CssClass = "txtDisableField";
                            txtAmount.ToolTip = "The Amount Cannot be edited because it was Cancelled previously";
                            txtAmount.ReadOnly = true;
                        }
                    }
                }
            }
        }
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            int agentId = 0;
            OfflineNCDIPOBackOfficeBo OfflineNCDIPOBackOfficeBo = new OfflineNCDIPOBackOfficeBo();
            DataTable dtOrderDetails = new DataTable();
            bool isValid = CollectOrderDetails(sender, e, out dtOrderDetails);
            if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
                dtAgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            if (dtAgentId.Rows.Count > 0)
            {
                agentId = int.Parse(dtAgentId.Rows[0][1].ToString());
            }
            if (!String.IsNullOrEmpty(txtBLPSearch.Text))
                BLPName = customerBo.GetBLPName(advisorVo.advisorId, txtBLPSearch.Text);
            if (BLPName.Rows.Count > 0)
            {
                mforderVo.EmpId = int.Parse(BLPName.Rows[0][2].ToString());
            }
            else
            {
                mforderVo.EmpId = 0;
            }
            Page.Validate("btnConfirmOrder");
            if (Page.IsValid)
            {
                if (isValid == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Quantity')", true);
                    return;
                }
                else
                {
                    bool resule = false;
                    resule = OfflineNCDIPOBackOfficeBo.UpdateNCDDetails(int.Parse(hdnOrderId.Value), userVo.UserId, dtOrderDetails, ddlBrokerCode.SelectedValue, agentId, OnlineBondVo, mforderVo.EmpId);
                    if (resule != false)
                    {
                        lnkEdit.Visible = true;
                        btnUpdate.Visible = false;
                        SetCOntrolsEnablity(false);
                        gvCommMgmt.Enabled = false;
                        ShowMessage("NCD Order Updated Successfully,Order reference no. is " + hdnOrderId.Value.ToString(), 'S');
                    }
                }
            }
            txtBLPSearch.Enabled = false;
        }
        protected void lnkEdit_LinkButtons(object sender, EventArgs e)
        {

            btnUpdate.Visible = true;
            lnkEdit.Visible = false;
            if (("ORDERED" == Request.QueryString["OrderStepCode"].ToString()))
            {
                SetCOntrolsEnablity(false);
                gvCommMgmt.Enabled = true;
            }
            else
            {
                SetCOntrolsEnablity(true);
                gvCommMgmt.Enabled = true;
            }
            txtBLPSearch.Enabled = true;
            lblBLPText.Enabled = true;
            lblBLPCodeText.Enabled = true;
        }

        protected void BindSubbroker(int issueId)
        {
            FIOrderBo fiorderBo = new FIOrderBo();
            DataTable dtBindSubbroker = fiorderBo.GetSubBroker(issueId);
            if (dtBindSubbroker.Rows.Count > 0)
            {
                ddlBrokerCode.DataSource = dtBindSubbroker;
                ddlBrokerCode.DataValueField = dtBindSubbroker.Columns["XB_BrokerIdentifier"].ToString();
                ddlBrokerCode.DataTextField = dtBindSubbroker.Columns["XB_BrokerShortName"].ToString();
                ddlBrokerCode.DataBind();
                if (dtBindSubbroker.Columns.Count > 1)
                {
                    ddlBrokerCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                }
            }
        }
        private void SetCOntrolsEnablity(bool Val)
        {
            txtFirstName.Enabled = Val;
            txtPanNumber.Enabled = Val;
            txtDpClientId.Enabled = Val;
            ddlIssueList.Enabled = Val;
            txtApplicationNo.Enabled = Val;
            ddlPaymentMode.Enabled = Val;
            txtPaymentInstDate.Enabled = Val;
            ddlBankName.Enabled = Val;
            txtASBANO.Enabled = Val;
            txtBranchName.Enabled = Val;
            txtRemarks.Enabled = Val;
            ddlBrokerCode.Enabled = Val;
            ddlDepositoryName.Enabled = Val;
            ddlCustomerSubType.Enabled = Val;
            txtDPId.Enabled = Val;
            txtAssociateSearch.Enabled = Val;
        }
        protected void gvCommMgmt_OnItemCommand(object source, GridCommandEventArgs e)
        {
            DataTable dtOrderDetails;

            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                int id = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AID_IssueDetailId"].ToString());
                dtOrderDetails = (DataTable)Cache["NCDTransactList" + userVo.UserId.ToString()];
                foreach (DataRow dr in dtOrderDetails.Rows)
                {
                    if (id == int.Parse(dr["AID_IssueDetailId"].ToString()))
                    {
                        dtOrderDetails.Rows[e.Item.ItemIndex]["COID_Quantity"] = "0";
                        dtOrderDetails.Rows[e.Item.ItemIndex]["COID_AmountPayable"] = "0";
                    }
                }


                dtOrderDetails.AcceptChanges();
                //for (int i = 0; i <= dtOrderDetails.Rows.Count; i++)
                //{
                int qty = 0, amt = 0;
                foreach (DataRow dr in dtOrderDetails.Rows)
                {

                    if (dr["COID_Quantity"].ToString() != "")
                    {
                        qty += int.Parse(dr["COID_Quantity"].ToString());
                        //j = j + r;
                    }
                    if (dr["COID_AmountPayable"].ToString() != "")
                    {
                        amt += int.Parse(dr["COID_AmountPayable"].ToString());
                    }
                }
                ViewState["qty"] = qty.ToString();
                ViewState["amt"] = amt.ToString();
                DataTable dt = dtOrderDetails;

                if (Cache["NCDTransactList" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("NCDTransactList" + userVo.UserId.ToString());
                }
                Cache.Insert("NCDTransactList" + userVo.UserId.ToString(), dtOrderDetails);
            }
        }
        protected void clearText(TextBox txtQuantity, TextBox txtAmount, int id)
        {
            txtQuantity.Text = "0";
            txtAmount.Text = "0";
        }
        protected void CVApplicationNo_ServerValidat(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            int issueId = int.Parse(ddlIssueList.SelectedValue.ToString());
            if (OfflineIPOOrderBo.ApplicationDuplicateCheck(issueId, int.Parse(txtApplicationNo.Text)))
            {
                if (hdnApplicationNo.Value != txtApplicationNo.Text)
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void CVBenificalNo_ServerValidat(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (ddlDepositoryName.SelectedValue=="NSDL" && txtDpClientId.Text.Length==8)

                args.IsValid = true;

            else if (ddlDepositoryName.SelectedValue == "CDSL" && txtDpClientId.Text.Length == 16)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }

}
