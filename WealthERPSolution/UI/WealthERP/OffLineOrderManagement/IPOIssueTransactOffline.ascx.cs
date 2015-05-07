using System;
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
using BOAssociates;
using System.Globalization;
using BoOnlineOrderManagement;
using BoOfflineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using VoCustomerProfiling;
using System.Collections;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using BoOfflineOrderManagement;

namespace WealthERP.OffLineOrderManagement
{
    public partial class IPOIssueTransactOffline : System.Web.UI.UserControl
    {
        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
        OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        DematAccountVo demataccountvo = new DematAccountVo();
        BoCustomerPortfolio.BoDematAccount bodemataccount = new BoDematAccount();
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
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        UserVo tempUserVo = new UserVo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        string page = string.Empty;
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
        OnlineIPOOrderBo onlineIPOOrderBo = new OnlineIPOOrderBo();
        OnlineIPOOrderVo onlineIPOOrderVo = new OnlineIPOOrderVo();
        OnlineNCDBackOfficeBo onlineNCDBackOfficeBo = new OnlineNCDBackOfficeBo();
        OfflineIPOOrderBo OfflineIPOOrderBo = new OfflineIPOOrderBo();
        BoDematAccount boDematAccount = new BoDematAccount();
        DataTable dtOnlineIPOIssueList;
        DataTable AgentId;
        CustomerPortfolioVo customerportfoliovo = new CustomerPortfolioVo();
        FIOrderBo fiorderBo = new FIOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            associatesVo = (AssociatesVO)Session["associatesVo"];

            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            GetUserType();


            if (!IsPostBack)
            {
                BindCustomerIPOIssueList(1);
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                {
                    AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode + "/" + advisorVo.advisorId.ToString();
                    AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";
                }
                BindDepositoryType();
                txtPaymentInstDate.MinDate = DateTime.Now.AddDays(-10);
                txtPaymentInstDate.MaxDate = DateTime.Now.AddDays(10);
                btnAddMore.Visible = false;
                rbtnIndividual.Checked = true;
                BindSubTypeDropDown(1001);
                BindIssueListBasedOnCustomerTypeSelection();
                if (AgentCode != null)
                {
                    txtAssociateSearch.Text = AgentCode;
                    OnAssociateTextchanged(this, null);
                }
                hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();


                if (Request.QueryString["action"] != null)
                {
                    string action1 = Request.QueryString["action"];
                    int orderId = Convert.ToInt32(Request.QueryString["orderId"].ToString());
                    ViewState["orderId"] = orderId.ToString();
                    ViewOrderList(orderId, Convert.ToDateTime(Request.QueryString["CloseDate"].ToString()));
                    btnConfirmOrder.Visible = false;
                    btnAddMore.Visible = false;
                    lblAssociateReport.Visible = true;
                    if (action1 == "View")
                    {
                        SetFICOntrolsEnablity(false);
                        RadGridIPOBid.Enabled = false;
                        if (("RJ" == Request.QueryString["OrderStepCode"].ToString()) || (DateTime.Now > Convert.ToDateTime(Request.QueryString["CloseDate"].ToString())))
                        {
                            lnkEdit.Visible = false;
                        }
                        else
                        {
                            lnkEdit.Visible = true;
                        }
                        if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                            GetUserType();
                    }
                    else
                    {
                        if (("OR" == Request.QueryString["OrderStepCode"].ToString()))
                        {
                            SetFICOntrolsEnablity(false);
                            RadGridIPOBid.Enabled = true;
                        }
                        else
                        {
                            SetFICOntrolsEnablity(true);
                        }
                        btnUpdate.Visible = true;
                        if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                            GetUserType();
                    }

                    //    //ShowPaymentSectionBasedOnTransactionType(ddltransType.SelectedValue, ViewForm);
                    //    //ButtonsEnablement(ViewForm);
                    //    // FrequencyEnablityForTransactionType(ddltransType.SelectedValue);
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
                    //lblgetPan.Text = customerVo.PANNum;
                    //BindPortfolioDropdown(customerId);
                }



            }
        }
        private void controlvisiblity(bool value)
        {
            //ddlsearch.Enabled = false;
            //txtCustomerName.Enabled = value;
            //txtAssociateSearch.Enabled = value;
            //ddlIssueList.Enabled = value;
            //txtApplicationNo.Enabled = value;
            //ddlPaymentMode.Enabled = value;
            //txtASBANO.Enabled = value;
            //ddlBankName.Enabled = value;
            //txtBranchName.Enabled = value;
            //txtBankAccount.Enabled = value;
            //txtPaymentNumber.Enabled = value;
            //txtPansearch.Enabled = value;
            //GetDematAccountDetails(int.Parse(txtCustomerId.Value));
        }
        protected void lnkEdit_OnClick(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;

            lnkEdit.Visible = false;
            if (("ORDERED" == Request.QueryString["OrderStepCode"].ToString()))
            {
                SetFICOntrolsEnablity(false);
                RadGridIPOBid.Enabled = true;
            }
            else
            {
                SetFICOntrolsEnablity(true);
                RadGridIPOBid.Enabled = true;
            }
        }
        private void ViewOrderList(int orderId, DateTime issueCloseDate)
        {
            DataSet dsGetMFOrderDetails = OfflineIPOOrderBo.GetIPOIssueOrderDetails(orderId);
            if (dsGetMFOrderDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsGetMFOrderDetails.Tables[0].Rows)
                {
                    Agentname = customerBo.GetAssociateName(advisorVo.advisorId, dr["AAC_AgentCode"].ToString());
                    if (Agentname.Rows.Count > 0)
                    {
                        lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                        lblAssociateReportTo.Text = Agentname.Rows[0][2].ToString();
                    }
                    else
                    {
                        lblAssociateReportTo.Text = string.Empty;
                        lblAssociatetext.Text = string.Empty;
                    }
                    txtAssociateSearch.Text = dr["AAC_AgentCode"].ToString();

                    txtAssociateSearch.Text = dr["AAC_AgentCode"].ToString();

                    string issue = dr["AIM_IssueId"].ToString();

                    ddlIssueList.SelectedValue = dr["AIM_IssueId"].ToString();
                    txtApplicationNo.Text = dr["CO_ApplicationNo"].ToString();
                    hdnApplicationNo.Value = txtApplicationNo.Text;
                    txtRemarks.Text = dr["CO_Remarks"].ToString();
                    BindSubbroker(int.Parse(dr["AIM_IssueId"].ToString()));
                    ddlBrokerCode.SelectedValue = dr["XB_BrokerIdentifier"].ToString();

                    txtFirstName.Text = dr["Customer_Name"].ToString();
                    txtPanNumber.Text = dr["OCD_Pan"].ToString();
                    if (dr["XCT_CustomerTypeCode"].ToString() == "IND")
                    {
                        rbtnIndividual.Checked = true;
                        BindSubTypeDropDown(1001);
                    }
                    else
                    {
                        rbtnIndividual.Checked = true;
                        BindSubTypeDropDown(1002);
                    }
                    //ddlCustomerSubType.SelectedValue = dr["OCD_WCMV_TaxStatus_Id"].ToString();
                    txtDpClientId.Text = dr["OCD_BeneficiaryAccountNum"].ToString();
                    ddlDepositoryName.SelectedValue = dr["OCD_DepositoryName"].ToString();
                    if (ddlDepositoryName.SelectedValue == "NSDL")
                    {
                        if (!string.IsNullOrEmpty(dr["OCD_DPId"].ToString()))
                            txtDPId.Text = dr["OCD_DPId"].ToString();
                    }


                    if (dr["CO_ASBAAccNo"].ToString() != "")
                    {
                        txtASBALocation.Text = dr["CO_BankBranchName"].ToString();
                        ddlPaymentMode.SelectedValue = "ES";
                        txtASBANO.Text = dr["CO_ASBAAccNo"].ToString();
                        txtBranchName.Text = dr["CO_BankBranchName"].ToString();

                        txtBranchName.Visible = false;
                        lblBranchName.Visible = false;
                        trASBA.Visible = true;
                    }
                    else
                    {
                        txtBranchName.Text = dr["CO_BankBranchName"].ToString();
                        ddlPaymentMode.SelectedValue = "CQ";
                        txtPaymentNumber.Text = dr["CO_ChequeNumber"].ToString();
                        if (!string.IsNullOrEmpty(dr["CO_PaymentDate"].ToString()))
                        {
                            txtPaymentInstDate.MinDate = Convert.ToDateTime(dr["CO_PaymentDate"].ToString());
                            txtPaymentInstDate.SelectedDate = Convert.ToDateTime(dr["CO_PaymentDate"].ToString());
                        }
                        if (dr["COID_DepCustBankAccId"].ToString() != string.Empty)
                            txtBankAccount.Text = dr["COID_DepCustBankAccId"].ToString().Substring(0, dr["COID_DepCustBankAccId"].ToString().IndexOf('.'));
                        trPINo.Visible = true;
                        lblBankAccount.Visible = true;
                        txtBankAccount.Visible = true;
                        txtBranchName.Text = dr["CO_BankBranchName"].ToString();
                        txtBranchName.Visible = true;
                        lblBranchName.Visible = true;
                    }
                    BindCustomerIPOIssueList((issueCloseDate >= DateTime.Now) ? 1 : 2);
                    BindIssueCategory(Convert.ToInt16(dr["AIM_IssueId"].ToString()));
                    ddlCategory.SelectedValue = dr["OCD_WCMV_TaxStatus_Id"].ToString();
                    BindIPOIssueList(Convert.ToInt16(dr["AIM_IssueId"].ToString()), (issueCloseDate >= DateTime.Now) ? 1 : 2, int.Parse(dr["OCD_WCMV_TaxStatus_Id"].ToString()));
                    BindBank();
                    ddlBankName.SelectedValue = dr["CO_BankName"].ToString();


                }
            }

            BindIPOBidGrid(3);
            //GetDematAccountDetails(int.Parse(txtCustomerId.Value));

            if (dsGetMFOrderDetails.Tables[2].Rows.Count > 0)
            {
                ViewState["Detais"] = dsGetMFOrderDetails.Tables[2];
                DataTable dtOrderDetails;
                dtOrderDetails = dsGetMFOrderDetails.Tables[2];
                if (Cache["IPOTransactList" + userVo.UserId.ToString()] != null)
                {
                    Cache.Remove("IPOTransactList" + userVo.UserId.ToString());
                }
                Cache.Insert("IPOTransactList" + userVo.UserId.ToString(), dtOrderDetails);

                RadGridIPOBid.DataSource = dtOrderDetails;
                RadGridIPOBid.DataBind();
                foreach (DataRow dr1 in dsGetMFOrderDetails.Tables[2].Rows)
                {
                    GridFooterItem ftItemAmount = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
                    //foreach (GridFooterItem footeritem in RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer))
                    //{
                    Label lblFinalBidAmountPayable = (Label)ftItemAmount["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                    lblFinalBidAmountPayable.Text = dr1["payable"].ToString();
                    decimal maxPaybleAmount = Convert.ToDecimal(((TextBox)ftItemAmount.FindControl("txtFinalBidValue")).Text);//accessing Button inside 
                    maxPaybleAmount = Convert.ToDecimal(dr1["payable"].ToString());
                    //}
                }
            }
        }
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            int orderNo = 0;
            int agentId = 0;
            string errorMsg = string.Empty;
            bool isBidsVallid = false;
            Page.Validate("btnConfirmOrder");
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                orderNo = Convert.ToInt32(Request.QueryString["orderId"].ToString());
            DataTable dtIPOBidTransactionDettails = CreateTable();
            DataTable dtJntHld = new DataTable();
            DataTable dtNominee = new DataTable();
            dtJntHld.Columns.Add("AssociateId");
            dtJntHld.Columns.Add("AssociateType");
            string userMessage = String.Empty;
            double totalBidAmount = 0;
            double totalBidAmountPayable = 0;
            string applicationNo = String.Empty;
            string apllicationNoStatus = String.Empty;
            double maxPaybleBidAmount = 0;
            bool lbResult = false;
            bool extractResult = false;
            hdnApplicationNo.Value = txtApplicationNo.Text;
            DateTime cutOff = DateTime.Now;
            extractResult = OfflineIPOOrderBo.OrderedDuplicateCheck(orderNo);
            if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
                dtAgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            if (dtAgentId.Rows.Count > 0)
            {
                agentId = int.Parse(dtAgentId.Rows[0][1].ToString());
            }
            int issueId = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString()))
                cutOff = Convert.ToDateTime(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString());
            DataRow drIPOBid;
            onlineIPOOrderVo.IssueId = issueId;
            onlineIPOOrderVo.AssetGroup = "IP";
            onlineIPOOrderVo.IsOrderClosed = false;
            onlineIPOOrderVo.IsOnlineOrder = false;
            onlineIPOOrderVo.IsDeclarationAccepted = true;
            onlineIPOOrderVo.OrderDate = DateTime.Now;

            onlineIPOOrderVo.CustomerName = txtFirstName.Text.Trim();
            onlineIPOOrderVo.CustomerPAN = txtPanNumber.Text.Trim();
            if (rbtnIndividual.Checked)
                onlineIPOOrderVo.CustomerType = "IND";
            else
                onlineIPOOrderVo.CustomerType = "NIND";
            onlineIPOOrderVo.CustomerSubTypeId = Convert.ToInt16(ddlCustomerSubType.SelectedValue);
            onlineIPOOrderVo.DematBeneficiaryAccountNum = txtDpClientId.Text.Trim();
            onlineIPOOrderVo.DematDepositoryName = ddlDepositoryName.SelectedValue;
            if (ddlDepositoryName.SelectedValue == "NSDL")
            {
                if (!string.IsNullOrEmpty(txtDPId.Text.Trim()))
                    onlineIPOOrderVo.DematDPId = txtDPId.Text.Trim();
            }
            onlineIPOOrderVo.AgentNo = txtAssociateSearch.Text;
            onlineIPOOrderVo.AgentId = agentId;
            int radgridRowNo = 0;
            int dematAccountId = 0;
            foreach (GridDataItem radItem in RadGridIPOBid.MasterTableView.Items)
            {
                drIPOBid = dtIPOBidTransactionDettails.NewRow();

                CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["CheckCutOff"].FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidQuantity"].FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidPrice"].FindControl("txtBidPrice");
                TextBox txtBidAmount = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmount"].FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmountPayable"].FindControl("txtBidAmountPayable");
                drIPOBid["IssueBidNo"] = RadGridIPOBid.MasterTableView.DataKeyValues[radgridRowNo]["IssueBidNo"].ToString();
                drIPOBid["IsCutOffApplicable"] = chkCutOff.Checked ? true : false;

                if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()))
                    drIPOBid["IPOIssueBidQuantity"] = txtBidQuantity.Text.Trim();
                else
                    drIPOBid["IPOIssueBidQuantity"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
                    drIPOBid["IPOIssueBidPrice"] = txtBidPrice.Text.Trim();
                else
                    drIPOBid["IPOIssueBidPrice"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                {
                    drIPOBid["IPOIssueBidAmount"] = txtBidAmount.Text.Trim();
                    totalBidAmount += Convert.ToDouble(txtBidAmount.Text.Trim());
                }
                else
                    drIPOBid["IPOIssueBidAmount"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidAmountPayable.Text.Trim()))
                {
                    drIPOBid["IPOIssueBidAmountPayable"] = txtBidAmountPayable.Text.Trim();
                    totalBidAmountPayable += Convert.ToDouble(txtBidAmountPayable.Text.Trim());
                }
                else
                    drIPOBid["IPOIssueBidAmountPayable"] = DBNull.Value;


                if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                    drIPOBid["TransactionStatusCode"] = 1;
                else
                    drIPOBid["TransactionStatusCode"] = 5;

                if (!string.IsNullOrEmpty(txtApplicationNo.Text.Trim()))
                    drIPOBid["ApplicationNO"] = txtApplicationNo.Text.Trim();
                else
                    drIPOBid["ApplicationNO"] = DBNull.Value;

                if (!string.IsNullOrEmpty(ddlPaymentMode.SelectedValue))
                    drIPOBid["ModeOfPayment"] = ddlPaymentMode.SelectedValue;
                else
                    drIPOBid["ModeOfPayment"] = DBNull.Value;
                if (!string.IsNullOrEmpty(txtASBANO.Text.Trim()))
                    drIPOBid["ASBAAccNo"] = txtASBANO.Text.Trim();
                else
                    drIPOBid["ASBAAccNo"] = DBNull.Value;

                drIPOBid["BankName"] = ddlBankName.SelectedValue;

                if (!string.IsNullOrEmpty(txtBranchName.Text.Trim()))
                    drIPOBid["BranchName"] = txtBranchName.Text.Trim();
                else
                    drIPOBid["BranchName"] = DBNull.Value;

                if (ddlPaymentMode.SelectedValue == "CQ")
                {
                    if (!string.IsNullOrEmpty(txtPaymentNumber.Text.Trim()))
                        drIPOBid["ChequeNo"] = txtPaymentNumber.Text.Trim();
                    if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString()))
                        drIPOBid["ChequeDate"] = txtPaymentInstDate.SelectedDate.Value.ToString("yyyy/MM/dd");
                    if (!string.IsNullOrEmpty(txtBankAccount.Text))
                        onlineIPOOrderVo.BankAccountNo = Int64.Parse(txtBankAccount.Text);

                }
                if (ddlPaymentMode.SelectedValue == "ES")
                    drIPOBid["BranchName"] = txtASBALocation.Text.Trim();
                else
                    drIPOBid["BranchName"] = txtBranchName.Text.Trim();
                drIPOBid["DematId"] = dematAccountId;
                drIPOBid["Remarks"] = txtRemarks.Text.Trim();
                DataTable dr = (DataTable)ViewState["Detais"];
                //foreach(DataRow dr1 in dr.Rows)
                drIPOBid["DetailsId"] = dr.Rows[radgridRowNo]["COID_DetailsId"].ToString();
                drIPOBid["OrderID"] = orderNo.ToString();
                dtIPOBidTransactionDettails.Rows.Add(drIPOBid);
                if (radgridRowNo < RadGridIPOBid.MasterTableView.Items.Count)
                    radgridRowNo++;
                else
                    break;
            }

            foreach (GridFooterItem footeritem in RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer))
            {
                Label lblBidHighestValue = (Label)footeritem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                maxPaybleBidAmount = Convert.ToDouble(lblBidHighestValue.Text.Trim());
            }
            if (Page.IsValid)
            {
                isBidsVallid = ValidateIPOBids(out errorMsg, 1);

                if (!isBidsVallid)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + errorMsg + "');", true);
                    return;
                }
                else
                {
                    OfflineIPOOrderBo.UpdateIPOBidOrderDetails(dtIPOBidTransactionDettails, orderNo, string.Empty, ddlBrokerCode.SelectedValue, userVo.UserId, onlineIPOOrderVo);
                    ShowMessage("IPO Order Updated Successfully,Order reference no. is " + orderNo.ToString());
                    btnUpdate.Visible = false;
                    lnkEdit.Visible = true;
                    SetFICOntrolsEnablity(false);
                    RadGridIPOBid.Enabled = false;
                }
            }
        }

        public void GetUserType()
        {
            string usertype = string.Empty;
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
                    if (Request.QueryString["action"] != null)
                    {
                        usertype = fiorderBo.GetUserType(advisorVo.advisorId, associateuserheirarchyVo.AdviserAgentId);
                    }
                }
                else
                    AgentCode = "0";
            }
        }








        public void BindBankName()
        {

        }




        protected void hidFolioNumber_ValueChanged(object sender, EventArgs e)
        {


        }

        protected void imgAddBank_OnClick(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank();
        }

        protected void imgBtnRefereshBank_OnClick(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank();
        }


        public void ISA_Onclick(object obj, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerISARequest','');", true);
        }



        protected void ShowPaymentSectionBasedOnTransactionType(string transType, string mode)
        {

        }


        protected void ddlCustomerISAAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void BindISAList()
        {
            DataTable ISAList;
            if (!string.IsNullOrEmpty(txtCustomerId.Value))
            {
                ISAList = customerBo.GetISaList(customerVo.CustomerId);
                DataTable ISANewList = new DataTable();
                int i;

                //ISANewList.Rows.Count = ISAList.Rows.Count;
                ISANewList.Columns.Add("CISAA_accountid");
                ISANewList.Columns.Add("CISAA_AccountNumber");

                //for (i = 0; i <= ISAList.Rows.Count; i++)
                //{

                //}
                if (ISAList.Rows.Count > 0)
                {

                    ddlCustomerISAAccount.DataSource = ISAList;
                    ddlCustomerISAAccount.DataValueField = ISAList.Columns["CISAA_accountid"].ToString();
                    ddlCustomerISAAccount.DataTextField = ISAList.Columns["CISAA_AccountNumber"].ToString();
                    ddlCustomerISAAccount.DataBind();
                    ddlCustomerISAAccount.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

                    ddlCustomerISAAccount.Visible = true;
                }
                else
                {
                    ddlCustomerISAAccount.Visible = true;

                    ddlCustomerISAAccount.Items.Clear();
                    ddlCustomerISAAccount.DataSource = null;
                    ddlCustomerISAAccount.DataBind();
                    ddlCustomerISAAccount.Items.Insert(0, new ListItem("Select", "Select"));
                    ddlCustomerISAAccount.SelectedIndex = -1;
                }

            }

        }
        protected void OnAssociateTextchanged1(object sender, EventArgs e)
        {
            if (txtPansearch.Text != "" && txtCustomerId.Value != "")
            {
                trPanExist.Visible = false;
                GetcustomerDetails();
                GetDematAccountDetails(int.Parse(txtCustomerId.Value));
            }
            else
            {

                trPanExist.Visible = true;
                lblPANNotExist.Text = "Entered PAN not found.";
                //txtPansearch.Text = "";
            }
            txtPansearch.Focus();
        }
        protected void HiddenField1_ValueChanged1(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank();
        }

        private void GetDematAccountDetails(int customerId)
        {
            try
            {
                DataSet dsDematDetails = boDematAccount.GetDematAccountHolderDetails(customerId);
                gvDematDetailsTeleR.Visible = true;
                gvDematDetailsTeleR.DataSource = dsDematDetails.Tables[0];
                gvDematDetailsTeleR.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetcustomerDetails()
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(int.Parse(txtCustomerId.Value));
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            Session["customerVo"] = customerVo;
            lblGetBranch.Text = customerVo.BranchName;
            //lblgetPan.Text = customerVo.PANNum;
            ViewState["customerID"] = txtCustomerId.Value;
            hdnCustomerId.Value = txtCustomerId.Value;
            lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            hdnPortfolioId.Value = customerPortfolioVo.PortfolioId.ToString();
            ViewState["PortfolioId"] = customerPortfolioVo.PortfolioId.ToString();
            customerId = int.Parse(txtCustomerId.Value);
            //if (ddlsearch.SelectedItem.Value == "2")
            txtPansearch.Text = customerVo.PANNum;
            lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            BindBank();
            BindISAList();
            //BindCustomerNCDIssueList();
            GetDematAccountDetails(int.Parse(txtCustomerId.Value));
            Panel1.Visible = true;

        }
        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {

                GetcustomerDetails();
                trCustomerAdd.Visible = false;
            }
        }

        public DataTable LoadNomineesJointHolder(string type)
        {
            DataTable dtCustomerAssociatesRaw = new DataTable();
            DataTable dtCustomerAssociates = new DataTable();
            DataRow drCustomerAssociates;

            dtCustomerAssociatesRaw = customerAccountBo.GetCustomerDematAccountAssociatesDetails(int.Parse(txtCustomerId.Value), type).Tables[0];
            dtCustomerAssociates.Columns.Add("AssociateId");
            dtCustomerAssociates.Columns.Add("AssociateName");
            dtCustomerAssociates.Columns.Add("AssociationType");
            foreach (DataRow dr in dtCustomerAssociatesRaw.Rows)
            {

                drCustomerAssociates = dtCustomerAssociates.NewRow();
                drCustomerAssociates[0] = dr["CDAA_Id"].ToString();
                drCustomerAssociates[1] = dr["CDAA_Name"].ToString();
                drCustomerAssociates[2] = dr["AssociationType"].ToString();
                dtCustomerAssociates.Rows.Add(drCustomerAssociates);
            }
            return dtCustomerAssociates;
        }

        protected void txtASBALocation_OnTextChanged(object sender, EventArgs e)
        {
            txtASBALocation.Focus();
        }
        protected void txtPaymentInstDate_OnSelectedDateChanged(object sender, EventArgs e)
        {
            txtPaymentInstDate.Focus();
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
            lblBankAccount.Visible = false;
            txtBankAccount.Visible = false;
            RequiredFieldValidator8.Enabled = false;
            CompareValidator14.Enabled = false;
            RequiredFieldValidator9.Enabled = false;
            if (ddlPaymentMode.SelectedValue == "CQ")
            {
                trPINo.Visible = true;
                RequiredFieldValidator8.Enabled = true;
                CompareValidator14.Enabled = true;

                txtBranchName.Visible = true;
                //lblBankBranchName.Visible = true;
                //RequiredFieldValidator7.Enabled = true;
                lblBankAccount.Visible = true;
                txtBankAccount.Visible = true;
                lblBranchName.Visible = true;
                txtASBANO.Text = "";
                txtASBALocation.Text = "";

            }
            else if (ddlPaymentMode.SelectedValue == "ES")
            {
                trASBA.Visible = true;
                RequiredFieldValidator9.Enabled = true;
                lblBranchName.Visible = false;
                txtBranchName.Visible = false;
                //lblBankBranchName.Visible = false;
                //RequiredFieldValidator7.Enabled = false;
                txtPaymentNumber.Text = "";
                txtBankAccount.Text = "";
                txtPaymentInstDate.SelectedDate = null;
            }


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
            // ddlBankName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }
        //private void BindPortfolioDropdown(int customerId)
        //{
        //    DataSet ds = portfolioBo.GetCustomerPortfolio(customerId);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        //ddlPortfolio.DataSource = ds;
        //        //ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
        //        //ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
        //        //ddlPortfolio.DataBind();
        //        //hdnPortfolioId.Value = ddlPortfolio.SelectedValue;
        //    }
        //}
        public void clearPancustomerDetails()
        {
            //lblgetPan.Text = "";
            //txtCustomerName.Text = "";
            txtPansearch.Text = "";
            lblgetcust.Text = "";
        }


        protected void rgvOrderSteps_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                DataTable dt = (DataTable)Session["OrderDetails"];

                GridEditableItem editItem = e.Item as GridEditableItem;
                RadComboBox comboOrderStatus = (RadComboBox)editItem.FindControl("ddlCustomerOrderStatus");
                RadComboBox comboOrderStatusReason = (RadComboBox)editItem.FindControl("ddlCustomerOrderStatusReason");
                string orderstepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();

                comboOrderStatus.DataSource = orderbo.GetCustomerOrderStepStatus(orderstepCode);
                comboOrderStatus.DataTextField = "XS_Status";
                comboOrderStatus.DataValueField = "XS_StatusCode";

                comboOrderStatusReason.DataSource = orderbo.GetCustomerOrderStepStatusRejectReason(orderstepCode);
                comboOrderStatusReason.DataTextField = "XSR_StatusReason";
                comboOrderStatusReason.DataValueField = "XSR_StatusReasonCode";
            }
        }

        protected void ddlCustomerOrderStatus_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox rcStatus = (RadComboBox)o;
            GridEditableItem editedItem = rcStatus.NamingContainer as GridEditableItem;
            RadComboBox ddlCustomerOrderStatus = editedItem.FindControl("ddlCustomerOrderStatus") as RadComboBox;
            RadComboBox rcPendingReason = editedItem.FindControl("ddlCustomerOrderStatusReason") as RadComboBox;

            string statusOrderCode = ddlCustomerOrderStatus.SelectedValue;

        }



        protected void rgvOrderSteps_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //this.rgvOrderSteps.DataSource = (DataTable)Session["OrderDetails"];
        }






        //private void Pan_Cust_Search(string seacrch)
        //{
        //    if (seacrch == "2")
        //        seacrch = "Pan";
        //    else
        //        seacrch = "Customer";

        //    if (seacrch == "Customer")
        //    {
        //        clearPancustomerDetails();
        //        trCust.Visible = true;
        //        trpan.Visible = false;
        //        if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
        //        {
        //            txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
        //            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

        //        }
        //        else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
        //        {
        //            txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        //            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
        //        }
        //        else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
        //        {
        //            txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        //            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
        //        }
        //        else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
        //        {
        //            txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
        //            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
        //        }

        //    }
        //    else if (seacrch == "Pan")
        //    {
        //        clearPancustomerDetails();
        //        trCust.Visible = false;
        //        trpan.Visible = true;
        //        if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
        //        {
        //            AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
        //            AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";

        //        }
        //        else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
        //        {
        //            txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        //            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
        //        }
        //        else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
        //        {
        //            txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        //            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
        //        }
        //        else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
        //        {
        //            txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
        //            txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
        //        }

        //    }
        //}
        //protected void ddlsearch_Selectedindexchanged(object sender, EventArgs e)
        //{
        //    Pan_Cust_Search(ddlsearch.SelectedValue);
        //}






        protected void ddlAMCList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }







        protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        {

        }

        //protected void btnAddMore_Click(object sender, EventArgs e)
        //{



        //}
        protected void ddlPeriodSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        protected void txtPeriod_OnTextChanged(object sender, EventArgs e)
        {



        }




        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {


        }

        protected void lnlBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }





        protected void lnkDelete_Click(object sender, EventArgs e)
        {


        }

        protected void btnreport_Click(object sender, EventArgs e)
        {
            mail = "0";


        }



        protected void btnpdfReport_Click(object sender, EventArgs e)
        {
            mail = "2";

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }



        protected void txtReceivedDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {


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
                //dtTradaDate.Select("CONVERT(VARCHAR,WTD_Date,103)='" + dtTempIncrement.ToShortDateString() + "'");
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
        protected void CVBidQtyMultiple_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            int issueQtyMultiple = 0;
            int issueMinQty = 0;
            int issueMaxQty = 0;
            int bidQuantity = Convert.ToInt32(args.Value.ToString());

            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString()))
                issueQtyMultiple = Convert.ToInt16(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString()))
                issueMinQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString()))
                issueMaxQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString());

            if ((bidQuantity - issueMinQty) % issueQtyMultiple != 0 && bidQuantity != issueMinQty && bidQuantity != issueMaxQty)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }


        }

        protected void BidQuantity_TextChanged(object sender, EventArgs e)
        {

            int currentRowidex = (((GridDataItem)((TextBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowidex, false);
            Page.Validate("btnConfirmOrder");
        }

        protected void BidPrice_TextChanged(object sender, EventArgs e)
        {

            int currentRowidex = (((GridDataItem)((TextBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowidex, true);
            Page.Validate("btnConfirmOrder");
        }

        protected void CutOffCheckBox_Changed(object sender, EventArgs e)
        {
            int currentRowindex = (((GridDataItem)((CheckBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowindex, false);
            CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[currentRowindex]["CheckCutOff"].FindControl("cbCutOffCheck");
            Page.Validate("btnConfirmOrder");
            chkCutOff.Focus();
        }

        protected void ReseIssueBidValues(int row, bool isBidPriceChange)
        {
            double bidAmount = 0;
            double ipoPriceDiscountValue = 0;
            CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[row]["CheckCutOff"].FindControl("cbCutOffCheck");
            TextBox txtBidQuantity = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidQuantity"].FindControl("txtBidQuantity");
            TextBox txtBidPrice = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidPrice"].FindControl("txtBidPrice");
            TextBox txtBidAmount = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidAmount"].FindControl("txtBidAmount");
            TextBox txtBidAmountPayable = (TextBox)RadGridIPOBid.MasterTableView.Items[row]["BidAmountPayable"].FindControl("txtBidAmountPayable");


            double capPrice = Convert.ToDouble(RadGridIPOIssueList.MasterTableView.Items[0]["AIM_CapPrice"].Text.Trim());
            string ipoPriceDiscountType = RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_PriceDiscountType"].ToString();
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_PriceDiscountValue"].ToString()))
                ipoPriceDiscountValue = Convert.ToDouble(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_PriceDiscountValue"].ToString());

            double bidAmountPayable = 0;
            if (chkCutOff.Checked)
            {
                txtBidPrice.Text = capPrice.ToString();
                txtBidPrice.Enabled = false;
                txtBidPrice.CssClass = "txtDisableField";
            }

            if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()) && !string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
            {
                txtBidAmount.Text = (Convert.ToInt32(txtBidQuantity.Text.Trim()) * Convert.ToDecimal(txtBidPrice.Text.Trim())).ToString();

                bidAmount = double.Parse(txtBidAmount.Text);
                bidAmountPayable = bidAmount;

                if (!string.IsNullOrEmpty(ipoPriceDiscountType.Trim()))
                {
                    switch (ipoPriceDiscountType)
                    {
                        case "AM":
                            {
                                bidAmountPayable = (Convert.ToDouble(txtBidPrice.Text.Trim()) - ipoPriceDiscountValue) * (Convert.ToInt32(txtBidQuantity.Text.Trim()));
                                break;
                            }
                        case "PE":
                            {
                                bidAmountPayable = (Convert.ToDouble(txtBidPrice.Text.Trim()) - ((ipoPriceDiscountValue / 100) * Convert.ToDouble(txtBidPrice.Text.Trim()))) * (Convert.ToInt32(txtBidQuantity.Text.Trim()));
                                break;
                            }
                    }
                }

                txtBidAmountPayable.Text = Math.Round(bidAmountPayable, 2).ToString();

            }
            else
            {
                txtBidAmount.Text = 0.ToString();
                txtBidAmountPayable.Text = 0.ToString();

            }

            if (chkCutOff.Checked)
                EnableDisableBids(true, 3, row, isBidPriceChange);
            else
                EnableDisableBids(false, 3, row, isBidPriceChange);

        }

        protected void EnableDisableBids(bool isChecked, int noOfBid, int rowNum, bool isBidPriceChange)
        {
            double[] bidMaxPayableAmount = new double[noOfBid];
            int count = 0;
            double finalBidPayableAmount = 0;
            List<string> iPOBids = new List<string>();
            string bidDuplicateChk = string.Empty;

            foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
            {
                CheckBox chkCutOff = (CheckBox)item.FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)item.FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)item.FindControl("txtBidPrice");

                TextBox txtBidAmount = (TextBox)item.FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)item.FindControl("txtBidAmountPayable");
                if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()) && !string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
                {
                    bidDuplicateChk = txtBidQuantity.Text.Trim() + "-" + txtBidPrice.Text.Trim();
                    if (!iPOBids.Contains(bidDuplicateChk))
                    {
                        iPOBids.Add(bidDuplicateChk);
                    }
                    else
                    {
                        TextBox txtBidQuantity1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidQuantity"].FindControl("txtBidQuantity");
                        TextBox txtBidPrice1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidPrice"].FindControl("txtBidPrice");
                        TextBox txtBidAmount1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidAmount"].FindControl("txtBidAmount");
                        TextBox txtBidAmountPayable1 = (TextBox)RadGridIPOBid.MasterTableView.Items[rowNum]["BidAmountPayable"].FindControl("txtBidAmountPayable");
                        if (isBidPriceChange)
                        {
                            txtBidPrice1.Text = string.Empty;
                            txtBidPrice1.Focus();
                        }
                        else
                        {
                            txtBidQuantity1.Text = string.Empty;
                            txtBidQuantity1.Focus();
                        }
                        txtBidAmount1.Text = "0";
                        txtBidAmountPayable1.Text = "0";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Duplicate bids found.Each bid should have unique combination of price and quantity!');", true);
                        //return;
                    }
                }

                if (isChecked)
                {
                    if (chkCutOff.Checked)
                    {
                        txtBidQuantity.Enabled = true;
                        txtBidQuantity.CssClass = "txtField";

                        txtBidPrice.Enabled = false;
                        txtBidPrice.CssClass = "txtDisableField";
                    }
                    else
                    {
                        chkCutOff.Enabled = false;

                        txtBidQuantity.Enabled = false;
                        txtBidQuantity.CssClass = "txtDisableField";

                        txtBidPrice.Enabled = false;
                        txtBidPrice.CssClass = "txtDisableField";

                        txtBidQuantity.Text = string.Empty;
                        txtBidPrice.Text = string.Empty;
                        txtBidAmount.Text = "0";
                        txtBidAmountPayable.Text = "0";

                    }


                }
                else
                {
                    chkCutOff.Enabled = true;

                    txtBidQuantity.Enabled = true;
                    txtBidQuantity.CssClass = "txtField";

                    txtBidPrice.Enabled = true;
                    txtBidPrice.CssClass = "txtField";

                }
                if (!string.IsNullOrEmpty(txtBidAmountPayable.Text.Trim()))
                {
                    bidMaxPayableAmount[count] = Convert.ToDouble(txtBidAmountPayable.Text);
                    count = count + 1;
                }

            }

            finalBidPayableAmount = bidMaxPayableAmount.Max();

            foreach (GridFooterItem footeritem in RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer))
            {
                Label lblBidHighestValue = (Label)footeritem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                TextBox txtFinalBidAmount = (TextBox)footeritem["BidAmountPayable"].FindControl("txtFinalBidValue");

                lblBidHighestValue.Text = finalBidPayableAmount.ToString();
                txtFinalBidAmount.Text = lblBidHighestValue.Text.Trim();
                //Session["finalprice"] = lblBidHighestValue.Text;
            }


        }




        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            bool isBidsVallid = false;
            Page.Validate("btnConfirmOrder");
            if (Page.IsValid)
            {
                isBidsVallid = ValidateIPOBids(out errorMsg, 0);

                if (!isBidsVallid)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + errorMsg + "');", true);
                    return;
                }
                else
                {
                    //LoadJScript();
                    CreateIPOOrder();
                    ControlsVisblity(true);
                    btnAddMore.Visible = true;
                    SetFICOntrolsEnablity(false);
                    btnAddMore.Focus();
                    RadGridIPOBid.Enabled = false;
                    RadGridIPOBid.Enabled = false;

                }
            }





        }

        protected DataTable CreateTable()
        {
            DataTable dtIPOBidTransactionDettails = new DataTable();
            dtIPOBidTransactionDettails.Columns.Add("IssueBidNo", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("IsCutOffApplicable", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidQuantity", typeof(Int64), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidPrice", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidAmount", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("IPOIssueBidAmountPayable", typeof(decimal), null);
            dtIPOBidTransactionDettails.Columns.Add("TransactionStatusCode", typeof(Int16));
            dtIPOBidTransactionDettails.Columns.Add("ApplicationNO");
            dtIPOBidTransactionDettails.Columns.Add("ModeOfPayment");
            dtIPOBidTransactionDettails.Columns.Add("ASBAAccNo");
            dtIPOBidTransactionDettails.Columns.Add("BankName");
            dtIPOBidTransactionDettails.Columns.Add("BranchName");
            dtIPOBidTransactionDettails.Columns.Add("DematId");
            dtIPOBidTransactionDettails.Columns.Add("ChequeDate");
            dtIPOBidTransactionDettails.Columns.Add("ChequeNo");
            dtIPOBidTransactionDettails.Columns.Add("Remarks");
            dtIPOBidTransactionDettails.Columns.Add("BankAccountNo");
            dtIPOBidTransactionDettails.Columns.Add("DetailsId", typeof(Int32), null);
            dtIPOBidTransactionDettails.Columns.Add("OrderId", typeof(Int32), null);
            dtIPOBidTransactionDettails.Columns.Add("BrokerCode");
            return dtIPOBidTransactionDettails;
        }
        private void CreateIPOOrder()
        {
            DataTable dtJntHld = new DataTable();
            DataTable dtNominee = new DataTable();
            dtJntHld.Columns.Add("AssociateId");
            dtJntHld.Columns.Add("AssociateType");
            string userMessage = String.Empty;
            bool accountDebitStatus = false;
            int orderId = 0;
            double totalBidAmount = 0;
            double totalBidAmountPayable = 0;
            string applicationNo = String.Empty;
            string apllicationNoStatus = String.Empty;
            double maxPaybleBidAmount = 0;
            DateTime cutOff = DateTime.Now;
            bool isCutOffTimeOver = false;
            int issueId = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString()))
                cutOff = Convert.ToDateTime(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CutOffTime"].ToString());
            DataTable dtIPOBidTransactionDettails = CreateTable();
            DataRow drIPOBid;

            //onlineIPOOrderVo.CustomerId = int.Parse(txtCustomerId.Value);
            onlineIPOOrderVo.IssueId = issueId;
            onlineIPOOrderVo.AssetGroup = "IP";
            onlineIPOOrderVo.IsOrderClosed = false;
            onlineIPOOrderVo.IsOnlineOrder = false;
            onlineIPOOrderVo.IsDeclarationAccepted = true;
            onlineIPOOrderVo.OrderDate = DateTime.Now;

            onlineIPOOrderVo.CustomerName = txtFirstName.Text.Trim();
            onlineIPOOrderVo.CustomerPAN = txtPanNumber.Text.Trim();
            if (rbtnIndividual.Checked)
                onlineIPOOrderVo.CustomerType = "IND";
            else
                onlineIPOOrderVo.CustomerType = "NIND";
            onlineIPOOrderVo.CustomerSubTypeId = Convert.ToInt16(ddlCustomerSubType.SelectedValue);
            onlineIPOOrderVo.DematBeneficiaryAccountNum = txtDpClientId.Text.Trim();
            onlineIPOOrderVo.DematDepositoryName = ddlDepositoryName.SelectedValue;
            if (ddlDepositoryName.SelectedValue == "NSDL")
            {
                if (!string.IsNullOrEmpty(txtDPId.Text.Trim()))
                    onlineIPOOrderVo.DematDPId = txtDPId.Text.Trim();
            }

            //onlineIPOOrderVo.CustBankAccId = int.Parse(txtBankAccount.Text);
            int radgridRowNo = 0;
            //int dematAccountId = 0;
            //foreach (GridDataItem gvr in gvDematDetailsTeleR.MasterTableView.Items)
            //{
            //    if (((CheckBox)gvr.FindControl("chkDematId")).Checked == true)
            //    {
            //        dematAccountId = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DematAccountId"].ToString());
            //        break;
            //    }

            //}
            foreach (GridDataItem radItem in RadGridIPOBid.MasterTableView.Items)
            {
                drIPOBid = dtIPOBidTransactionDettails.NewRow();

                CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["CheckCutOff"].FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidQuantity"].FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidPrice"].FindControl("txtBidPrice");
                TextBox txtBidAmount = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmount"].FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)RadGridIPOBid.MasterTableView.Items[radgridRowNo]["BidAmountPayable"].FindControl("txtBidAmountPayable");
                drIPOBid["IssueBidNo"] = RadGridIPOBid.MasterTableView.DataKeyValues[radgridRowNo]["IssueBidNo"].ToString();
                drIPOBid["IsCutOffApplicable"] = chkCutOff.Checked ? true : false;

                if (!string.IsNullOrEmpty(txtBidQuantity.Text.Trim()))
                    drIPOBid["IPOIssueBidQuantity"] = txtBidQuantity.Text.Trim();
                else
                    drIPOBid["IPOIssueBidQuantity"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidPrice.Text.Trim()))
                    drIPOBid["IPOIssueBidPrice"] = txtBidPrice.Text.Trim();
                else
                    drIPOBid["IPOIssueBidPrice"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                {
                    drIPOBid["IPOIssueBidAmount"] = txtBidAmount.Text.Trim();
                    totalBidAmount += Convert.ToDouble(txtBidAmount.Text.Trim());
                }
                else
                    drIPOBid["IPOIssueBidAmount"] = DBNull.Value;

                if (!string.IsNullOrEmpty(txtBidAmountPayable.Text.Trim()))
                {
                    drIPOBid["IPOIssueBidAmountPayable"] = txtBidAmountPayable.Text.Trim();
                    totalBidAmountPayable += Convert.ToDouble(txtBidAmountPayable.Text.Trim());
                }
                else
                    drIPOBid["IPOIssueBidAmountPayable"] = DBNull.Value;


                if (!string.IsNullOrEmpty(txtBidAmount.Text.Trim()))
                    drIPOBid["TransactionStatusCode"] = 1;
                else
                    drIPOBid["TransactionStatusCode"] = 5;

                if (!string.IsNullOrEmpty(txtApplicationNo.Text.Trim()))
                    drIPOBid["ApplicationNO"] = txtApplicationNo.Text.Trim();
                else
                    drIPOBid["ApplicationNO"] = DBNull.Value;

                if (!string.IsNullOrEmpty(ddlPaymentMode.SelectedValue))
                    drIPOBid["ModeOfPayment"] = ddlPaymentMode.SelectedValue;
                else
                    drIPOBid["ModeOfPayment"] = DBNull.Value;
                if (!string.IsNullOrEmpty(txtASBANO.Text.Trim()))
                    drIPOBid["ASBAAccNo"] = txtASBANO.Text.Trim();
                else
                    drIPOBid["ASBAAccNo"] = DBNull.Value;
                if (ddlPaymentMode.SelectedValue == "ES")
                {
                    drIPOBid["BankName"] = ddlBankName.SelectedValue;
                }
                else
                    drIPOBid["BankName"] = ddlBankName.SelectedValue;

                if (ddlPaymentMode.SelectedValue == "ES")
                    drIPOBid["BranchName"] = txtASBALocation.Text.Trim();
                else
                    drIPOBid["BranchName"] = txtBranchName.Text.Trim();
                if (!string.IsNullOrEmpty(ddlBrokerCode.SelectedValue))
                {
                    drIPOBid["BrokerCode"] = ddlBrokerCode.SelectedValue;
                }
                else
                {
                    drIPOBid["BrokerCode"] = DBNull.Value;
                }
                if (ddlPaymentMode.SelectedValue == "CQ")
                {
                    if (!string.IsNullOrEmpty(txtPaymentNumber.Text.Trim()))
                        drIPOBid["ChequeNo"] = txtPaymentNumber.Text.Trim();
                    if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString()))
                        drIPOBid["ChequeDate"] = txtPaymentInstDate.SelectedDate.Value.ToString("yyyy/MM/dd");
                    if (!string.IsNullOrEmpty(txtBankAccount.Text.Trim()))
                        drIPOBid["BankAccountNo"] = txtBankAccount.Text.Trim();

                }
                drIPOBid["DematId"] = string.Empty;
                drIPOBid["Remarks"] = txtRemarks.Text.Trim();
                dtIPOBidTransactionDettails.Rows.Add(drIPOBid);
                if (radgridRowNo < RadGridIPOBid.MasterTableView.Items.Count)
                    radgridRowNo++;
                else
                    break;
            }

            foreach (GridFooterItem footeritem in RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer))
            {
                Label lblBidHighestValue = (Label)footeritem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
                maxPaybleBidAmount = Convert.ToDouble(lblBidHighestValue.Text.Trim());
            }
            if (DateTime.Now.TimeOfDay > cutOff.TimeOfDay && cutOff.TimeOfDay < System.TimeSpan.Parse("23:59:59"))
                isCutOffTimeOver = true;
            int agentId = 0;
            if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
                dtAgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            if (dtAgentId.Rows.Count > 0)
            {
                agentId = int.Parse(dtAgentId.Rows[0][1].ToString());
            }
            orderId = OfflineIPOOrderBo.CreateIPOBidOrderDetails(advisorVo.advisorId, userVo.UserId, dtIPOBidTransactionDettails, onlineIPOOrderVo, agentId, txtAssociateSearch.Text);
            if (orderId != 0)
            {
                userMessage = CreateUserMessage(orderId, accountDebitStatus, isCutOffTimeOver);
                ShowMessage(userMessage);
            }


        }

        private void ShowMessage(string msg)
        {
            //tblMessagee.Visible = true;
            //msgRecordStatus.InnerText = msg;
            //--S(success)
            //--F(failure)
            //--W(warning)
            //--I(information)
            tblMessagee.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "wsedrftgyhjukloghjnnnghj", " showMsg('" + msg + "','" + "S" + "');", true);
        }

        private string CreateUserMessage(int orderId, bool accountDebitStatus, bool isCutOffTimeOver)
        {
            string userMessage = string.Empty;
            if (orderId != 0)
                if (isCutOffTimeOver)
                {
                    userMessage = "Order placed successfully, Order reference no. is " + orderId.ToString() + ", Order will process next business day.";
                }
                else
                {

                    userMessage = "Order placed successfully, Order reference no. is " + orderId.ToString();
                }

            return userMessage;

        }

        public void btnAddMore_Click(object sender, EventArgs e)
        {

            //ClearAllFields();
            //SetFICOntrolsEnablity(true);
            //btnAddMore.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IPOIssueTransactOffline','login');", true);
            //btnConfirmOrder.Visible = true;

        }
        internal void LoadJScript()
        {
            ClientScriptManager script = Page.ClientScript;
            //prevent duplicate script
            if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
            {
                script.RegisterStartupScript(this.GetType(), "HideLabel",
                "<script type='text/javascript'>HideLabel('" + tblMessagee.ClientID + "')</script>");
            }
        }
        public void ClearAllFields()
        {
            lblAssociatetext.Text = "";
            ddlIssueList.SelectedIndex = 0;
            txtApplicationNo.Text = null;
            ddlPaymentMode.SelectedIndex = 0;
            ddlBankName.SelectedIndex = -1;
            txtBranchName.Text = "";
            txtBankAccount.Text = "";
            txtPaymentNumber.Text = "";
            txtPaymentInstDate.SelectedDate = null;
            txtASBANO.Text = "";
            tblgvCommMgmt.Visible = false;
            tblgvIssueList.Visible = false;
            pnlIPOIssueList.Visible = false;
            txtRemarks.Text = "";

            txtAssociateSearch.Text = "";
            ddlPaymentMode.SelectedIndex = 0;
            txtASBALocation.Text = "";
            trPINo.Visible = false;
            trASBA.Visible = false;
            lblBankAccount.Visible = false;
            txtBankAccount.Visible = false;
            txtFirstName.Text = string.Empty;
            txtPanNumber.Text = string.Empty;
            rbtnIndividual.Checked = false;
            ddlCustomerSubType.SelectedValue = "2017";
            txtDpClientId.Text = string.Empty;
            ddlDepositoryName.SelectedValue = "NSDL";
            txtDPId.Text = string.Empty;



        }
        protected void lnkTermsCondition_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = true;
        }

        protected void txtApplicationNo_OnTextChanged(object sender, EventArgs e)
        {
            txtApplicationNo.Focus();
        }
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = false;
            chkTermsCondition.Checked = true;
        }

        public void TermsConditionCheckBox(object o, ServerValidateEventArgs e)
        {
            if (chkTermsCondition.Checked)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {

        }

        private void ControlsVisblity(bool visble)
        {
            btnConfirmOrder.Visible = false;
            btnAddMore.Visible = true;

        }

        private bool ValidateIPOBids(out string msg, int typeOfvalidation)
        {
            bool isBidValid = true;
            msg = string.Empty;
            int validBidSum = 0;
            int issueQtyMultiple = 0;
            int issueMinQty = 0;
            int issueMaxQty = 0;
            int bidId = 1;
            DateTime dtCloseDate = Convert.ToDateTime(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_CloseDate"].ToString());
            //dtCloseDate = DateTime.Now.AddHours(-1);
            decimal minBidAmount = Convert.ToDecimal(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_MInBidAmount"].ToString());
            decimal maxBidAmount = Convert.ToDecimal(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIIC_MaxBidAmount"].ToString());
            GridFooterItem footerItem = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
            Label lblFinalBidAmountPayable = (Label)footerItem["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
            decimal maxPaybleAmount1 = Convert.ToDecimal(lblFinalBidAmountPayable.Text);
            decimal maxPaybleAmount = Convert.ToDecimal(((TextBox)footerItem.FindControl("txtFinalBidValue")).Text);//accessing Button inside 
            Boolean isMultipleApplicationAllowed = Convert.ToBoolean(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IsMultipleApplicationsallowed"].ToString());
            int issueId = int.Parse(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_IssueId"].ToString());
            if (isMultipleApplicationAllowed == false)
            {
                int issueApplicationSubmitCount = onlineNCDBackOfficeBo.CustomerMultipleOrder(customerVo.CustomerId, issueId);
                if (issueApplicationSubmitCount > 0)
                {
                    msg = "You have already invested in selected issue, Please check the order book for the status.Multiple Investment is not allowed in same issue";
                    isBidValid = false;
                    return isBidValid;
                }
            }
            if (dtCloseDate < DateTime.Now)
            {
                msg = "Issue is closed now, order can not accept";
                isBidValid = false;
                return isBidValid;
            }

            if (maxPaybleAmount > 0)
            {
                if (minBidAmount > maxPaybleAmount || maxBidAmount < maxPaybleAmount)
                {
                    msg = "Bid Value (Amount Payable) should be greater than the Min bid amount and less than the Max bid amount";
                    isBidValid = false;
                    return isBidValid;
                }
            }
            else if (typeOfvalidation != 1 && maxPaybleAmount < 0)
            {
                if (minBidAmount > maxPaybleAmount1 || maxBidAmount < maxPaybleAmount1)
                {
                    msg = "Bid Value (Amount Payable) should be greater than the Min bid amount and less than the Max bid amount";
                    isBidValid = false;
                    return isBidValid;
                }
            }

            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString()))
                issueQtyMultiple = Convert.ToInt16(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_TradingInMultipleOf"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString()))
                issueMinQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString());
            if (!string.IsNullOrEmpty(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString()))
                issueMaxQty = Convert.ToInt32(RadGridIPOIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString());
            foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
            {
                double bidAmountPayble = 0;

                //CheckBox chkCutOff = (CheckBox)item.FindControl("cbCutOffCheck");
                TextBox txtBidQuantity = (TextBox)item.FindControl("txtBidQuantity");
                TextBox txtBidPrice = (TextBox)item.FindControl("txtBidPrice");

                TextBox txtBidAmount = (TextBox)item.FindControl("txtBidAmount");
                TextBox txtBidAmountPayable = (TextBox)item.FindControl("txtBidAmountPayable");
                double.TryParse(txtBidAmountPayable.Text, out bidAmountPayble);
                if (!string.IsNullOrEmpty(txtBidQuantity.Text))
                {
                    //Bid Quantity Multiple Validation

                    if ((Convert.ToInt64(txtBidQuantity.Text) - issueMinQty) % issueQtyMultiple != 0 && Convert.ToInt64(txtBidQuantity.Text) != issueMinQty && Convert.ToInt64(txtBidQuantity.Text) != issueMaxQty)
                    {
                        msg = "Please enter Quantity in multiples permissibile for this issue";
                        isBidValid = false;
                        return isBidValid;
                    }
                }
                if (bidAmountPayble > 0)
                    validBidSum += int.Parse(item.GetDataKeyValue("IssueBidNo").ToString());

                if (typeOfvalidation != 1 && bidAmountPayble <= 0 && int.Parse(item.GetDataKeyValue("IssueBidNo").ToString()) == 1)
                {
                    msg = "Bid found missing.Please enter the bids in sequence starting from the top!";
                    isBidValid = false;
                    return isBidValid;
                }
                else if ((!string.IsNullOrEmpty(txtBidQuantity.Text) || !string.IsNullOrEmpty(txtBidPrice.Text)) && bidAmountPayble == 0)
                {
                    msg = "Please complete the Bid Option" + item.GetDataKeyValue("IssueBidNo").ToString();
                    isBidValid = false;
                    return isBidValid;
                }
                bidId++;

            }
            if (validBidSum == 4)
            {
                msg = "Bid found missing.Please enter the bids in sequence starting from the top!";
                isBidValid = false;
                return isBidValid;
            }

            return isBidValid;

        }

        protected void RadGridIPOBid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (dtOnlineIPOIssueList != null)
            {

                if (dtOnlineIPOIssueList.Rows.Count > 0)
                {
                    if (e.Item is GridDataItem)
                    {
                        GridDataItem dataform = (GridDataItem)e.Item;
                        RangeValidator rvQuantity = (RangeValidator)dataform.FindControl("rvQuantity");
                        RangeValidator rvBidPrice = (RangeValidator)dataform.FindControl("rvBidPrice");
                        int minQuantity = 0;
                        int maxQuantity = 0;
                        double minBidPrice = 0;
                        double maxBidPrice = 0;

                        {
                            int.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_MInQty"].ToString(), out minQuantity);
                            int.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_MaxQty"].ToString(), out maxQuantity);
                            string basic = dtOnlineIPOIssueList.Rows[0]["AIM_IsBookBuilding"].ToString();
                            double.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_FloorPrice"].ToString(), out minBidPrice);
                            double.TryParse(dtOnlineIPOIssueList.Rows[0]["AIM_CapPrice"].ToString(), out maxBidPrice);

                            if (e.Item.RowIndex != -1)
                            {
                                rvQuantity.MinimumValue = minQuantity.ToString();
                                rvQuantity.MaximumValue = maxQuantity.ToString();

                                rvBidPrice.MinimumValue = minBidPrice.ToString();
                                rvBidPrice.MaximumValue = maxBidPrice.ToString();

                                if (basic == "Fixed" && dataform.RowIndex == 4)
                                {
                                    int currentRowindex = (dataform.RowIndex / 4) - 1;
                                    CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[currentRowindex]["CheckCutOff"].FindControl("cbCutOffCheck");
                                    chkCutOff.Checked = true;
                                    chkCutOff.Enabled = false;
                                    ReseIssueBidValues(currentRowindex, false);

                                }

                            }
                            if (Request.QueryString["action"] != null && (e.Item.ItemIndex != -1))
                            {
                                RadGridIPOBid.MasterTableView.GetColumn("COID_ExchangeRefrenceNo").Visible = true;
                                RadGridIPOBid.MasterTableView.GetColumn("DeleteBid").Visible = true;
                            }
                        }
                    }
                }
            }
         


        }
        protected void lnlktoviewIPOissue_Click(object sender, EventArgs e)
        {
            if (Session["PageDefaultSetting"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('IPOIssueList');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IPOIssueList", "loadcontrol('IPOIssueList');", true);
            }
        }
        protected void ImageddlSyndicate_Click(object sender, EventArgs e)
        {
            //  RadDepository.VisibleOnPageLoad = true;

        }
        public bool Validation()
        {
            bool result = true;
            int issueId = int.Parse(ddlIssueList.SelectedValue.ToString());
            try
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







        public bool Validation1()
        {
            bool result = true;
            int adviserId = ((AdvisorVo)Session["advisorVo"]).advisorId;
            try
            {
                if (customerBo.PANNumberDuplicateCheck(adviserId, txtPanNumber.Text.ToString(), customerVo.CustomerId))
                {
                    result = false;
                    lblPanDuplicate.Visible = true;
                }
                else
                {
                    lblPanDuplicate.Visible = false;
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        private void MakeReadonlyControls()
        {
            ddlAdviserBranchList.Enabled = false;
            ddlAdviseRMList.Enabled = false;
            ddlCustomerSubType.Enabled = false;
            txtPanNumber.Enabled = false;
            ddlSalutation.Enabled = false;
            txtFirstName.Enabled = false;
            txtMiddleName.Enabled = false;
            txtLastName.Enabled = false;
            txtEmail.Enabled = false;
            btnSubmit.Enabled = false;

        }
        protected void ddlModeOfHolding_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DematebtnSubmit_Click(object sender, EventArgs e)
        {
            int result = 0;
            string accountopeningdate = Convert.ToDateTime(txtAccountOpeningDate.Text).ToString();
            int customerId = 0;
            if (ViewState["customerID"] != null)
            {
                customerId = int.Parse(ViewState["customerID"].ToString());
            }
            else
            {
                customerVo = (CustomerVo)Session["customerVo"];
                customerId = customerVo.CustomerId;
            }
            if (Page.IsValid)
            {

                try
                {

                    if (!string.IsNullOrEmpty(accountopeningdate.Trim()))
                        //GridViewRow grdrow = (GridViewRow)((gvDematDetailsTeleR)sender).NamingContainer;
                        //GridDataItem gvr in gvDematDetailsTeleR.MasterTableVie
                        //CheckBox chkDematId=(CheckBox)gvr.FindControl("chkDematId");
                        hdnPortfolioId.Value = Convert.ToString(ViewState["PortfolioId"].ToString());
                    demataccountvo.AccountOpeningDate = DateTime.Parse(accountopeningdate);
                    demataccountvo.DpclientId = txtDpClientId.Text;
                    demataccountvo.DpId = txtDPId.Text;
                    demataccountvo.DpName = txtDpName.Text;
                    demataccountvo.DepositoryName = ddlDepositoryName.SelectedValue;
                    if (rbtnYes.Checked == true)
                        demataccountvo.IsHeldJointly = 1;
                    else
                        demataccountvo.IsHeldJointly = 0;
                    demataccountvo.ModeOfHolding = ddlModeOfHolding.SelectedValue.ToString();
                    result = bodemataccount.AddDematDetails(customerId, int.Parse(hdnPortfolioId.Value), demataccountvo, rmVo);
                    txtDematid.Text = txtDpClientId.Text;
                    ViewState["demateId"] = result;

                    GetDematAccountDetails(customerId);
                    tdDemate.Visible = false;
                    ddlIssueList.Focus();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected void rbtnNo_CheckChanged(object sender, EventArgs e)
        {
            ddlModeOfHolding.SelectedIndex = 8;
            ddlModeOfHolding.Enabled = false;

        }
        protected void RadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (rbtnYes.Checked && !(rbtnNo.Checked))
            {
                ddlModeOfHolding.SelectedIndex = 4;

                ddlModeOfHolding.Enabled = true;
            }
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
        protected void gvDematDetailsTeleR_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            CheckBox chk = (CheckBox)e.Item.FindControl("chkDematId");
            if (e.Item is GridDataItem)
            {
                int dpid = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CEDA_DematAccountId"].ToString());
                if (ViewState["demateId"] != null)
                {
                    if (dpid == int.Parse(ViewState["demateId"].ToString()))
                    {
                        chk.Checked = true;
                    }
                }
            }
        }
        private void SetFICOntrolsEnablity(bool Val)
        {
            txtPaymentNumber.Enabled = Val;
            txtAssociateSearch.Enabled = Val;
            ddlIssueList.Enabled = Val;
            txtApplicationNo.Enabled = Val;
            ddlPaymentMode.Enabled = Val;
            txtBankAccount.Enabled = Val;
            txtPaymentInstDate.Enabled = Val;
            ddlBankName.Enabled = Val;
            txtASBANO.Enabled = Val;
            txtASBALocation.Enabled = Val;
            txtBranchName.Enabled = Val;
            txtRemarks.Enabled = Val;
            ddlBrokerCode.Enabled = Val;

            txtFirstName.Enabled = Val;
            txtPanNumber.Enabled = Val;
            rbtnIndividual.Enabled = Val;
            ddlCustomerSubType.Enabled = Val;
            txtDpClientId.Enabled = Val;
            ddlDepositoryName.Enabled = Val;
            txtDPId.Enabled = Val;

        }
        protected void rbtnAuthentication_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAuthentication.Checked)
                lblAuthenticatedBy.Text = userVo.FirstName + ' ' + userVo.MiddleName + ' ' + userVo.LastName;
            else
                lblAuthenticatedBy.Text = "";
            tdlblReject.Visible = false;
            tdtxtReject.Visible = false;
            txtRejectReseaon.Text = "";
        }
        protected void rbtnReject_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnReject.Checked)
                lblAuthenticatedBy.Text = userVo.FirstName + ' ' + userVo.MiddleName + ' ' + userVo.LastName;
            else
                lblAuthenticatedBy.Text = "";
            tdlblReject.Visible = true;
            tdtxtReject.Visible = true;
        }
        protected void ddlDepositoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepositoryName.SelectedItem.Text == "NSDL")
            {
                txtDPId.Enabled = true;
            }
            else if (ddlDepositoryName.SelectedItem.Text == "CDSL")
            {
                txtDPId.Text = "";
                txtDPId.Enabled = false;
            }
            ddlDepositoryName.Focus();
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
                txtAssociateSearch.Focus();
            }
        }


        protected void ddlIssueList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ddlIssueList.SelectedIndex < 1)) return;
            BindIssueCategory(Convert.ToInt16(ddlIssueList.SelectedValue));
            BindIPOIssueList(Convert.ToInt16(ddlIssueList.SelectedValue), 1, 2017);
            BindIPOBidGrid(3);
            BindSubbroker(int.Parse(ddlIssueList.SelectedValue));
            ddlIssueList.Focus();
        }
        private void BindIssueCategory(int issueId)
        {
            DataTable dt = OfflineIPOOrderBo.GetIssueCategory(issueId);
            ddlCategory.DataSource = dt;
            ddlCategory.DataValueField = "WCMV_Lookup_SubTypeId";
            ddlCategory.DataTextField = "AIIC_InvestorCatgeoryName";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--SELECT--", "1"));
        }


        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            BindSubTypeDropDown(1001);
            BindIssueListBasedOnCustomerTypeSelection();

        }
        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {

            BindSubTypeDropDown(1002);
            BindIssueListBasedOnCustomerTypeSelection();

        }

        protected void ddlCustomerSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindIssueListBasedOnCustomerTypeSelection();
            ddlCustomerSubType.Focus();
        }

        private void BindIssueListBasedOnCustomerTypeSelection()
        {
            if (ddlCustomerSubType.SelectedIndex < -1) return;
          
        }
        private void BindCustomerIPOIssueList( int type)
        {
            DataTable dtIssueList = dtIssueList = onlineNCDBackOfficeBO.GetIssueList(advisorVo.advisorId, type, "IP");
            ddlIssueList.DataTextField = dtIssueList.Columns["AIM_IssueName"].ToString();
            ddlIssueList.DataValueField = dtIssueList.Columns["AIM_IssueId"].ToString();
            ddlIssueList.DataSource = dtIssueList;
            ddlIssueList.DataBind();
            ddlIssueList.Items.Insert(0, new ListItem("--SELECT--", "1"));
        }
        private void BindSubTypeDropDown(int lookupParentId)
        {
            DataTable dtCustomerTaxSubTypeLookup = dtCustomerTaxSubTypeLookup = commonLookupBo.GetWERPLookupMasterValueList(2000, lookupParentId);
            ddlCustomerSubType.DataSource = dtCustomerTaxSubTypeLookup;
            ddlCustomerSubType.DataTextField = "WCMV_Name";
            ddlCustomerSubType.DataValueField = "WCMV_LookupId";
            ddlCustomerSubType.DataBind();
            ddlCustomerSubType.Items.Insert(0, new ListItem("--SELECT--", "0"));
            if (rbtnIndividual.Checked == true)
                ddlCustomerSubType.SelectedValue = "2017";

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
                if (ddlBrokerCode.Items.Count > 1)
                {
                    ddlBrokerCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                }
            }
        }

        private void BindIPOIssueList(int issueId, int type, int customerSubTypeId)
        {

            tblgvCommMgmt.Visible = true;
            tblgvIssueList.Visible = true;
            pnlIPOIssueList.Visible = true;
            dtOnlineIPOIssueList = OfflineIPOOrderBo.GetIPOIssueList(advisorVo.advisorId, issueId, type, customerSubTypeId);
            RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
            RadGridIPOIssueList.DataBind();


        }

        private void BindIPOBidGrid(int noOfBid)
        {
            DataTable dtIPOBid = new DataTable();
            DataRow drIPOBid;
            dtIPOBid.Columns.Add("IssueBidNo");
            dtIPOBid.Columns.Add("BidOptions");
            dtIPOBid.Columns.Add("IsCutOff");
            dtIPOBid.Columns.Add("BidPrice");
            dtIPOBid.Columns.Add("BidQty");
            dtIPOBid.Columns.Add("BidAmountPayable", typeof(double));
            dtIPOBid.Columns.Add("BidAmount", typeof(double));
            dtIPOBid.Columns.Add("COID_TransactionType");

            for (int i = 1; i <= noOfBid; i++)
            {
                drIPOBid = dtIPOBid.NewRow();
                drIPOBid["IssueBidNo"] = i.ToString();
                drIPOBid["BidOptions"] = "Bid Option" + i.ToString();
                drIPOBid["IsCutOff"] = 0;
                drIPOBid["BidPrice"] = null;
                drIPOBid["BidQty"] = null;
                drIPOBid["BidAmountPayable"] = 0;
                drIPOBid["BidAmount"] = 0;
                drIPOBid["COID_TransactionType"] = "N";
                dtIPOBid.Rows.Add(drIPOBid);

            }
            if (Cache["IPOTransactList" + userVo.UserId.ToString()] != null)
            {
                Cache.Remove("IPOTransactList" + userVo.UserId.ToString());
            }
            Cache.Insert("IPOTransactList" + userVo.UserId.ToString(), dtIPOBid);
            RadGridIPOBid.DataSource = dtIPOBid;
            RadGridIPOBid.DataBind();


        }

        protected void BindDepositoryType()
        {
            DataTable DsDepositoryNames = new DataTable();
            DsDepositoryNames = bodemataccount.GetDepositoryName();
            ddlDepositoryName.DataSource = DsDepositoryNames;
            if (DsDepositoryNames.Rows.Count > 0)
            {
                ddlDepositoryName.DataTextField = "WCMV_Code";
                ddlDepositoryName.DataValueField = "WCMV_Code";
                ddlDepositoryName.DataBind();
            }
            ddlDepositoryName.Items.Insert(0, new ListItem("Select", "Select"));

        }
        protected void RadGridIPOBid_OnItemCommand(object source, GridCommandEventArgs e)
        {
          
        }
        protected void RadGridIPOBid_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIssueDetail;
            dtIssueDetail = (DataTable)Cache["IPOTransactList" + userVo.UserId.ToString()];
            if (dtIssueDetail != null)
            {
                RadGridIPOBid.DataSource = dtIssueDetail;
            }

        }
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            GridDataItem RadGridIPO = (GridDataItem)btnDelete.NamingContainer;
            TextBox txtBidQuantity = (TextBox)RadGridIPO.FindControl("txtBidQuantity");
            TextBox txtBidPrice = (TextBox)RadGridIPO.FindControl("txtBidPrice");
            TextBox txtBidAmountPayable = (TextBox)RadGridIPO.FindControl("txtBidAmountPayable");
            TextBox txtBidAmount = (TextBox)RadGridIPO.FindControl("txtBidAmount");
            txtBidAmountPayable.Text = "";
            txtBidAmount.Text = "";
            txtBidQuantity.Text = "";
            txtBidPrice.Text = "";
            double[] bidMaxPayableAmount = new double[3];
            int count = 0;
            double finalBidPayableAmount = 0;
            foreach (GridDataItem item in RadGridIPOBid.MasterTableView.Items)
            {
                TextBox txtBidAmountPayabl = (TextBox)item.FindControl("txtBidAmountPayable");
                if (!string.IsNullOrEmpty(txtBidAmountPayabl.Text.Trim()))
                {
                    bidMaxPayableAmount[count] = Convert.ToDouble(txtBidAmountPayabl.Text);
                    count = count + 1;
                }

            }

            finalBidPayableAmount = bidMaxPayableAmount.Max();
            GridFooterItem ftItemAmount = (GridFooterItem)RadGridIPOBid.MasterTableView.GetItems(GridItemType.Footer)[0];
            Label lblFinalBidAmountPayable = (Label)ftItemAmount["BidAmountPayable"].FindControl("lblFinalBidAmountPayable");
            lblFinalBidAmountPayable.Text = finalBidPayableAmount.ToString();
        }

    }

}
