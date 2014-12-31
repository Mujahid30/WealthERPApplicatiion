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

        SystematicSetupVo systematicSetupVo = new SystematicSetupVo();

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();

            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            RadDepository.VisibleOnPageLoad = false;
            rwDematDetails.VisibleOnPageLoad = false;
            tblMessage.Visible = false;
            GetUserType();
            rwTermsCondition.VisibleOnPageLoad = false;
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";
                AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";

            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
            }
            else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
            {
                txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
                AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";

                AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode + "/" + advisorVo.advisorId.ToString();      
                AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";

            }


            if (!IsPostBack)
            {
                btnAddMore.Visible = false;
                lblApplicationDuplicate.Visible = false;
                if (AgentCode != null)
                {
                    txtAssociateSearch.Text = AgentCode;
                    OnAssociateTextchanged(this, null);
                }
                hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();
                if (hdnIsSubscripted.Value == "True")
                {
                    trIsa.Visible = true;
                }
                else
                {
                    trIsa.Visible = false;
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

        //private void ShowHideFields(int flag)
        //{
        //    if (flag == 0)
        //    {
        //        //trTransactionType.Visible = false;

        //        //rgvOrderSteps.Visible = false;
        //        lnkBtnEdit.Visible = false;
        //        lnlBack.Visible = false;
        //        //btnUpdate.Visible = false;
        //        lnkDelete.Visible = false;
        //        btnViewReport.Visible = false;
        //        btnViewInPDF.Visible = false;
        //        btnViewInDOC.Visible = false;
        //    }
        //    else if (flag == 1)
        //    {
        //        //trTransactionType.Visible = true;
        //        //trARDate.Visible = true;
        //        //trAplNumber.Visible = true;
        //        //trOrderDate.Visible = true;
        //        //trOrderNo.Visible = true;
        //        //trfutureDate.Visible = false;
        //        //rgvOrderSteps.Visible = false;
        //        lnkBtnEdit.Visible = false;
        //        lnlBack.Visible = false;
        //        //btnUpdate.Visible = false;
        //        lnkDelete.Visible = false;
        //        btnViewReport.Visible = false;
        //        btnViewInPDF.Visible = false;
        //        btnViewInDOC.Visible = false;
        //    }
        //}

        protected void ShowPaymentSectionBasedOnTransactionType(string transType, string mode)
        {
            // bool enablement = false; ;

            //pnl_BUY_ABY_SIP_PaymentSection.Visible = false;
            //pnl_SIP_PaymentSection.Visible = false;
            //pnl_SEL_PaymentSection.Visible = false;
            //Tr1.Visible = true;

            if (transType == "BUY" | transType == "ABY")
            {

                // pnl_BUY_ABY_SIP_PaymentSection.Enabled = enablement;
                //pnl_BUY_ABY_SIP_PaymentSection.Visible = true;
                //if (transType == "BUY")
                //    //Tr1.Visible = false;

            }
            else if (transType == "Sel")
            {
                //pnl_SEL_PaymentSection.Visible = true;
                //trScheme.Visible = false;

            }
            else if (transType == "SIP")
            {
                //pnl_BUY_ABY_SIP_PaymentSection.Enabled = enablement;
                //pnl_BUY_ABY_SIP_PaymentSection.Visible = true;
                //pnl_SIP_PaymentSection.Visible = true;
                //pnl_SIP_PaymentSection.Enabled = enablement;

            }
            else if (transType == "SWP")
            {
                ////pnl_SEL_PaymentSection.Enabled = enablement;
                //pnl_SEL_PaymentSection.Visible = true;
                ////trScheme.Visible = false;
                //pnl_SIP_PaymentSection.Visible = true;

            }
            else if (transType == "STB")
            {
                //pnl_SEL_PaymentSection.Enabled = enablement;
                //pnl_SEL_PaymentSection.Visible = true;
                //trScheme.Visible = true;
                //pnl_SIP_PaymentSection.Visible = true;

            }
            else if (transType == "SWB")
            {
                //pnl_SEL_PaymentSection.Visible = true;
                //trScheme.Visible = true;
            }

        }


        protected void ddlCustomerISAAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    DataTable GetHoldersName = new DataTable();
            //    if (ddlCustomerISAAccount.SelectedItem.Value != "Select")
            //    {
            //        GetHoldersName = customerBo.GetholdersName(int.Parse(ddlCustomerISAAccount.SelectedItem.Value.ToString()));
            //        if (GetHoldersName.Rows.Count > 0)
            //        {
            //            gvJointHoldersList.DataSource = GetHoldersName;
            //            gvJointHoldersList.DataBind();
            //            gvJointHoldersList.Visible = true;
            //            //pnlJointholders.Visible = true;
            //        }
            //        else
            //        {
            //            gvJointHoldersList.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        gvJointHoldersList.Visible = false;
            //    }
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
       
        protected void OnAssociateTextchanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtAssociateSearch.Text))
            {
                int recCount = 0;
                //customerBo.ChkAssociateCode(advisorVo.advisorId, txtAssociateSearch.Text);
                if (userType == "associates")
                {
                    recCount = customerBo.ChkAssociateCode(advisorVo.advisorId, associateuserheirarchyVo.AgentCode, txtAssociateSearch.Text, userType);
                }
                else
                {
                    recCount = customerBo.ChkAssociateCode(advisorVo.advisorId, "", txtAssociateSearch.Text, userType);

                }
                if (recCount == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('SubBroker Code is invalid!');", true);
                    txtAssociateSearch.Text = string.Empty;
                    return;
                }
                Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                if (Agentname.Rows.Count > 0)
                {
                    lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                }
                else
                {
                    lblAssociatetext.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Agent Code is invalid!');", true);

                    txtAssociateSearch.Text = "";
                }

            }

        }

        protected void txtAgentId_ValueChanged1(object sender, EventArgs e)
        {

        }

        protected void HiddenField1_ValueChanged1(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank();
        }
        protected void ImageddlSyndicate_Click(object sender, EventArgs e)
        {
            RadDepository.VisibleOnPageLoad = true;

        }
        protected void chkjnthld_OnCheckedChanged(object sender, EventArgs e)
        {
            //CheckBox chkJntHld = (CheckBox)sender;
            //GridDataItem item = (GridDataItem)chkJntHld.NamingContainer;
            //CompareValidator1.Enabled = false;
            //foreach (GridDataItem gvr in gvJointHolder.MasterTableView.Items)
            //{
            //    if (((CheckBox)gvr.FindControl("chkjnthld")).Checked == true)
            //    {


            //        CompareValidator1.Enabled = true;
            //        return;
            //    }

            //}
        }
        protected void btnDepository_OnClick(object sender, EventArgs e)
        {
            CommonLookupBo boCommonLookup = new CommonLookupBo();
            Boolean IsDuplicate = boCommonLookup.CheckDuplicateDepositoryType(txtDepository.Text.ToString());
            if (!IsDuplicate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Depository Type Exists.');", true);
                return;
            }

            boCommonLookup.CreateDepositoryType(txtDepository.Text, txtDescription.Text);
        }


        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {

                GetcustomerDetails();
            }
        }
        ////protected void btnImgAddCustomer_click(object sender, EventArgs e)
        ////{
        ////    ScriptManager.RegisterStartupScript(this, GetType(), "openpopupAddCustomer", "openpopupAddCustomer();", true);
        ////}
        protected void GetcustomerDetails()
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(int.Parse(txtCustomerId.Value));
            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
            Session["customerVo"] = customerVo;
            lblGetBranch.Text = customerVo.BranchName;
            lblgetPan.Text = customerVo.PANNum;
            hdnCustomerId.Value = txtCustomerId.Value;
            txtCustomerName.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            hdnPortfolioId.Value = customerPortfolioVo.PortfolioId.ToString();
            customerId = int.Parse(txtCustomerId.Value);
            //if (ddlsearch.SelectedItem.Value == "2")
            txtPansearch.Text = customerVo.PANNum;
            lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            BindBank();
            BindISAList();
            BindCustomerNCDIssueList();
            GetDematAccountDetails(int.Parse(txtCustomerId.Value));
            //Panel1.Visible = true;
            Panel2.Visible = true;
        }

        private void BindJointHolderNominee()
        {
            //DataTable dtJointHolderNomiee = new DataTable();
            //DataTable dtNominee = new DataTable();
            //dtJointHolderNomiee = LoadNomineesJointHolder("JH");
            //gvJointHolder.DataSource = dtJointHolderNomiee;
            //gvJointHolder.DataBind();
            //pnlJointHolder.Visible = true;
            //dtNominee = LoadNomineesJointHolder("N");
            //gvNominee.DataSource = null;
            //gvNominee.DataSource = dtNominee;
            //gvNominee.DataBind();
            //pnlNominee.Visible = true;

        }

        private void BindBank()
        {
            CommonLookupBo commonLookupBo = new CommonLookupBo();
            ddlBankName.Items.Clear();
            DataTable dtBankName = new DataTable();
            dtBankName = commonLookupBo.GetWERPLookupMasterValueList(7000, 0); ;
            ddlBankName.DataSource = dtBankName;
            ddlBankName.DataValueField = dtBankName.Columns["WCMV_LookupId"].ToString();
            ddlBankName.DataTextField = dtBankName.Columns["WCMV_Name"].ToString();
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }

        public void clearPancustomerDetails()
        {
            lblgetPan.Text = "";
            txtCustomerName.Text = "";
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






        private void Pan_Cust_Search(string seacrch)
        {
            if (seacrch == "2")
                seacrch = "Pan";
            else
                seacrch = "Customer";

            if (seacrch == "Customer")
            {
                clearPancustomerDetails();
                trCust.Visible = true;
                trpan.Visible = false;
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }

            }
            else if (seacrch == "Pan")
            {
                clearPancustomerDetails();
                trCust.Visible = false;
                trpan.Visible = true;
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
                    AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";

                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
                }
                else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
                }

            }
        }
        protected void ddlsearch_Selectedindexchanged(object sender, EventArgs e)
        {
            Pan_Cust_Search(ddlsearch.SelectedValue);
        }


        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaymentMode(ddlPaymentMode.SelectedValue);
        }

        private void PaymentMode(string type)
        {
            trPINo.Visible = false;
            trASBA.Visible = false;
            RequiredFieldValidator8.Enabled = false;
            CompareValidator14.Enabled = false;
            RequiredFieldValidator9.Enabled = false;
            if (ddlPaymentMode.SelectedValue == "CQ")
            {
                trPINo.Visible = true;
                RequiredFieldValidator8.Enabled = true;
                CompareValidator14.Enabled = true;

            }
            else if (ddlPaymentMode.SelectedValue == "ES")
            {
                trASBA.Visible = true;
                RequiredFieldValidator9.Enabled = true;
            }

        }

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


        private void BindCustomerNCDIssueList()
        {
            DataTable dtIssueList = new DataTable();
            dtIssueList = onlineNCDBackOfficeBO.GetIssueList(advisorVo.advisorId, 1, int.Parse(hdnCustomerId.Value), "FI");
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
                BindStructureRuleGrid();
                BindStructureRuleGrid(int.Parse(ddlIssueList.SelectedValue));
                pnlNCDOOrder.Visible = true;
            }
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



        //protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int BankAccountId = 0;
        //    DataTable dtgetBankBranch;
        //    if (ddlBankName.SelectedIndex != 0)
        //    {
        //        BankAccountId = int.Parse(ddlBankName.SelectedValue);
        //        dtgetBankBranch = mfOrderBo.GetBankBranch(BankAccountId);
        //        if (dtgetBankBranch.Rows.Count > 0)
        //        {
        //            DataRow dr = dtgetBankBranch.Rows[0];
        //            txtBranchName.Text = dr["BBL_BranchName"].ToString();
        //        }
        //        hdnBankName.Value = ddlBankName.SelectedItem.Text;
        //    }
        //}

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
        protected void BindStructureRuleGrid()
        {
            customerVo = (CustomerVo)Session["customerVo"];
            DataTable dtIssue = new DataTable();
            //1--- For Curent Issues
            pnlIssuList.Visible = true;
            dtIssue = offlineBondBo.GetOfflineAdviserIssuerList(advisorVo.advisorId, int.Parse(ddlIssueList.SelectedValue), 1, int.Parse(hdnCustomerId.Value), customerVo.TaxStatusCustomerSubTypeId).Tables[0];

            if (dtIssue.Rows.Count > 0)
            {
                if (Cache["NCDIssueList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NCDIssueList" + userVo.UserId.ToString(), dtIssue);
                }
                else
                {
                    Cache.Remove("NCDIssueList" + userVo.UserId.ToString());
                    Cache.Insert("NCDIssueList" + userVo.UserId.ToString(), dtIssue);
                }

                gvIssueList.DataSource = dtIssue;
                gvIssueList.DataBind();

            }
            else
            {

                gvIssueList.DataSource = dtIssue;
                gvIssueList.DataBind();

            }


        }
        protected void BindStructureRuleGrid(int IssuerId)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            DataSet dsStructureRules = offlineBondBo.GetOfflineLiveBondTransaction(IssuerId, customerVo.CustomerId, customerVo.TaxStatusCustomerSubTypeId);
            DataTable dtTransact = dsStructureRules.Tables[0];
            if (dtTransact.Rows.Count > 0)
            {
                if (Cache["NCDTransactList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("NCDTransactList" + userVo.UserId.ToString(), dtTransact);
                }
                else
                {
                    Cache.Remove("NCDTransactList" + userVo.UserId.ToString());
                    Cache.Insert("NCDTransactList" + userVo.UserId.ToString(), dtTransact);
                }
                gvCommMgmt.DataSource = dtTransact;
                ViewState["Transact"] = dtTransact;
                gvCommMgmt.DataBind();
                pnlNCDTransact.Visible = true;
                trTermsCondition.Visible = true;
                trSubmit.Visible = true;


            }
            else
            {
                gvCommMgmt.DataSource = dtTransact;
                gvCommMgmt.DataBind();
                pnlNCDTransact.Visible = true;
                trTermsCondition.Visible = false;
                trSubmit.Visible = false;
            }
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
                Regex re = new Regex(@"^[1-9]\d*$");
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
                                sum = sum + Convert.ToInt32(txtsumAmount.Text);
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
        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }
        protected void lnkBtnDemat_onClick(object sender, EventArgs e)
        {
            GetDematAccountDetails(int.Parse(txtCustomerId.Value));
            rwDematDetails.VisibleOnPageLoad = true;

        }
        protected void lnkTermsCondition_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = true;
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
        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            int issueDetId = 0;
            int catId = 0;
            int agentId = 0;

            Button Button = (Button)sender;
            if (gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString() == "" || gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_FaceValue"].ToString() == "")
                return;
            int MaxAppNo = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_MaxApplNo"].ToString());
            int FaceValue = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[0]["AIM_FaceValue"].ToString());
            int minQty = int.Parse(gvIssueList.MasterTableView.DataKeyValues[0]["AIM_MInQty"].ToString());
            int maxQty = int.Parse(gvIssueList.MasterTableView.DataKeyValues[0]["AIM_MaxQty"].ToString());
            DataTable dt = new DataTable();
            bool isValid = false;
            //Need to be collect from Session...
            dt.Columns.Add("CustomerId");
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
            dt.Columns.Add("DematId");
            dt.Columns.Add("ChequeDate");
            dt.Columns.Add("ChequeNo");
            dt.Columns.Add("Remarks");

            int rowNo = 0;
            int tableRow = 0;
            int dematAccountId = 0;
            foreach (GridDataItem gvr in gvDematDetailsTeleR.MasterTableView.Items)
            {
                if (((CheckBox)gvr.FindControl("chkDematId")).Checked == true)
                {
                    dematAccountId = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DematAccountId"].ToString());
                    break;
                }

            }
            foreach (GridDataItem CBOrder in gvCommMgmt.MasterTableView.Items)
            {

                TextBox txtQuantity = (TextBox)gvCommMgmt.MasterTableView.Items[rowNo]["Quantity"].FindControl("txtQuantity");
                if (txtQuantity.Text == "0" || txtQuantity.Text == string.Empty)
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
                if (ddlPaymentMode.SelectedValue == "CQ")
                {
                    OnlineBondVo.ChequeNumber = txtPaymentNumber.Text;
                    OnlineBondVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
                }

                OnlineBondVo.CustomerId = int.Parse(txtCustomerId.Value);
                OnlineBondVo.BankAccid = 1002321521;
                OnlineBondVo.PFISD_SeriesId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AID_IssueDetailId"].ToString());
                OnlineBondVo.IssueId = Convert.ToInt32(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIM_IssueId"].ToString());
                CheckBox Check = (CheckBox)gvCommMgmt.MasterTableView.Items[rowNo]["Check"].FindControl("cbOrderCheck");
                catId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[rowNo]["AIDCSR_Id"].ToString());
                if (Check.Checked == true)
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
                        dt.Rows[tableRow]["CustomerId"] = OnlineBondVo.CustomerId;
                        dt.Rows[tableRow]["AID_IssueDetailId"] = OnlineBondVo.PFISD_SeriesId;
                        dt.Rows[tableRow]["AIM_IssueId"] = OnlineBondVo.IssueId;
                        dt.Rows[tableRow]["Qty"] = OnlineBondVo.Qty;
                        dt.Rows[tableRow]["Amount"] = OnlineBondVo.Amount;
                        dt.Rows[tableRow]["ApplicationNO"] = OnlineBondVo.ApplicationNumber;
                        dt.Rows[tableRow]["ModeOfPayment"] = OnlineBondVo.PaymentMode;
                        dt.Rows[tableRow]["ASBAAccNo"] = txtASBANO.Text;
                        dt.Rows[tableRow]["BankName"] = ddlBankName.SelectedItem.Text;
                        dt.Rows[tableRow]["BranchName"] = OnlineBondVo.BankBranchName;
                        dt.Rows[tableRow]["DematId"] = dematAccountId;
                        dt.Rows[tableRow]["Remarks"] = OnlineBondVo.Remarks;
                        if (ddlPaymentMode.SelectedValue == "CQ")
                        {
                            dt.Rows[tableRow]["ChequeDate"] = OnlineBondVo.PaymentDate.ToString("yyyy/MM/dd");
                            dt.Rows[tableRow]["ChequeNo"] = OnlineBondVo.ChequeNumber;
                        }
                        GridFooterItem footerItemAmount = (GridFooterItem)gvCommMgmt.MasterTableView.GetItems(GridItemType.Footer)[0];
                        Label lblSum = (Label)footerItemAmount.FindControl("lblAmount");
                        txtAmount.Text = OnlineBondVo.Amount.ToString();

                        OnlineBondBo.GetCustomerCat(OnlineBondVo.IssueId, int.Parse(txtCustomerId.Value), advisorVo.advisorId, Convert.ToDouble(lblSum.Text), ref catName, ref issueDetId, ref EligblecatId, ref Description);

                        if (EligblecatId == 0)
                        {
                            ShowMessage("Application amount should be between Min Quantity and Max Quantity.");
                            return;
                        }
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

            if (isValid)
            {
                if (Validation())
                {
                    Quantity = int.Parse(ViewState["Qty"].ToString());
                    sum = int.Parse(ViewState["Sum"].ToString());




                    if (ViewState["CustCat"] == null)
                    {

                        string category = (string)ViewState["CustCat"];
                        if (category == string.Empty)
                            ShowMessage("Please enter no of bonds within the range permissible.");


                    }
                    else if (FaceValue > sum)
                    {
                        ShowMessage("Application amount is less than minimum application amount.");
                        //  tdsubmit.Visible = false;
                        // lnlBack.Visible = true;

                    }
                    else if (Quantity < minQty)
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
                            // ShowMessage(message);
                            txtsumQuantity.Text = "";
                            txtsumAmount.Text = "";
                            lblQty.Text = "";
                            lblSum.Text = "";
                        }
                    }
                    else if (Quantity > maxQty)
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
                            // ShowMessage(message);

                            txtsumQuantity.Text = "";
                            txtsumAmount.Text = "";
                            lblQty.Text = "";
                            lblSum.Text = "";
                        }
                    }
                    else
                    {
                        DataTable dtJntHld = new DataTable();
                        DataTable dtNominee = new DataTable();
                        dtJntHld.Columns.Add("AssociateId");
                        dtJntHld.Columns.Add("AssociateType");
                        // placing order 
                        IDictionary<string, string> orderIds = new Dictionary<string, string>();
                        //IssuerId = int.Parse(ViewState["IssueId"].ToString());
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
                        else
                            agentId = 0;
                        orderIds = offlineBondBo.OfflineBOndtransact(dt, advisorVo.advisorId, OnlineBondVo.IssueId, agentId, txtAssociateSearch.Text, userVo.UserId);
                        orderId = int.Parse(orderIds["Order_Id"].ToString());
                        aplicationNoStatus = orderIds["aplicationNoStatus"].ToString();

                        int rowNodt = 0;
                        if (gvAssociate.MasterTableView.Items.Count > 0)
                        {
                            foreach (GridDataItem gvr in gvAssociate.Items)
                            {

                                dtJntHld.Rows.Add();
                                dtJntHld.Rows[rowNodt]["AssociateId"] = int.Parse(gvAssociate.MasterTableView.DataKeyValues[gvr.ItemIndex]["CDAA_Id"].ToString());
                                dtJntHld.Rows[rowNodt]["AssociateType"] = gvAssociate.MasterTableView.DataKeyValues[gvr.ItemIndex]["AssociateType"].ToString();
                                rowNodt++;




                            }
                        }

                        if (dtJntHld.Rows.Count > 0)
                        {
                            offlineBondBo.CreateOfflineCustomerOrderAssociation(dtJntHld, userVo.UserId, orderId);
                        }
                        ViewState["OrderId"] = orderId;
                        btnConfirmOrder.Enabled = false;
                        Label3.Visible = false;
                        tdsubmit.Visible = false;
                        //LoadJScript();
                        message = CreateUserMessage(orderId, aplicationNoStatus);
                        ShowMessage(message);
                        btnConfirmOrder.Visible = false;
                        btnAddMore.Visible = true;

                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Enter Quantity')", true);
            }
        }
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = false;
            chkTermsCondition.Checked = true;
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
                //  userMessage = "Please Contact sbi team to fill Aplications";

            }

            else if (orderId == 0)
            {
                userMessage = "Order cannot be processed. Issue Got Closed";

            }
            return userMessage;
        }
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            ClearAllFields();
            btnAddMore.Visible = false;
            tdsubmit.Visible = true;
            Label3.Visible = true;
            btnConfirmOrder.Visible = true;
        }
        //internal void LoadJScript()
        //{
        //    ClientScriptManager script = Page.ClientScript;
        //    //prevent duplicate script
        //    if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
        //    {
        //        script.RegisterStartupScript(this.GetType(), "HideLabel",
        //        "<script type='text/javascript'>HideLabel('" + tblMessage.ClientID + "')</script>");
        //    }
        //}
        private void ClearAllFields()
        {


            ddlsearch.SelectedIndex = -1;
            txtCustomerName.Text = "";
            lblgetPan.Text = "";
            lblGetBranch.Text = "";
            //txtAssociateSearch.Text = "";
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
            BindStructureRuleGrid(0);
            BindStructureRuleGrid(0);
            pnlJointHolderNominee.Visible = false;
            pnlNCDOOrder.Visible = false;
            pnlIssuList.Visible = false;
            pnlNCDTransact.Visible = false;
            txtRemarks.Text = "";
            trSumbitSuccess.Visible = false;

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
        private void GetFIModeOfHolding()
        {
            FIOrderBo fiorderBo = new FIOrderBo();
            DataSet dsDepoBank = fiorderBo.GetFIModeOfHolding();


            //if (dsDepoBank.Tables[0].Rows.Count > 0)
            //{

            //    ddlModeofHOldingFI.DataSource = dsDepoBank.Tables[0];
            //    ddlModeofHOldingFI.DataValueField = dsDepoBank.Tables[0].Columns["XMOH_ModeOfHoldingCode"].ToString();
            //    ddlModeofHOldingFI.DataTextField = dsDepoBank.Tables[0].Columns["XMOH_ModeOfHolding"].ToString();
            //    ddlModeofHOldingFI.DataBind();

            //    ddlModeofHOldingFI.Items.Insert(0, new ListItem("Select", "Select"));

            //}
            //else
            //{
            //    ddlModeofHOldingFI.Items.Clear();
            //    ddlModeofHOldingFI.DataSource = null;
            //    ddlModeofHOldingFI.DataBind();
            //    ddlModeofHOldingFI.Items.Insert(0, new ListItem("Select", "Select"));
            //}
        }
        protected void rbtnYes_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnYes.Checked)
            //{
            //    ddlModeofHOldingFI.Enabled = true;
            //}
            //else
            //{
            //    ddlModeofHOldingFI.SelectedValue = "SI";
            //    ddlModeofHOldingFI.Enabled = false;

            //}


        }
        protected void btnAddDemat_Click(object sender, EventArgs e)
        {
            int dematAccountId = 0;
            foreach (GridDataItem gvr in gvDematDetailsTeleR.MasterTableView.Items)
            {
                if (((CheckBox)gvr.FindControl("chkDematId")).Checked == true)
                {
                    dematAccountId = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DematAccountId"].ToString());
                    txtDematid.Text = gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DPClientId"].ToString();
                    break;
                }

            }
            BindgvFamilyAssociate(dematAccountId);

        }
        private void BindgvFamilyAssociate(int demataccountid)
        {
            gvAssociate.Visible = true;
            DataSet dsAssociate = boDematAccount.GetCustomerDematAccountAssociates(demataccountid);
            gvAssociate.DataSource = dsAssociate;
            gvAssociate.DataBind();
            pnlJointHolderNominee.Visible = true;
            if (Cache["gvAssociate" + userVo.UserId] == null)
            {
                Cache.Insert("gvAssociate" + userVo.UserId, dsAssociate);
            }
            else
            {
                Cache.Remove("gvAssociate" + userVo.UserId);
                Cache.Insert("gvAssociate" + userVo.UserId, dsAssociate);
            }
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
        protected void rbtnIndividual_CheckedChanged(object sender, EventArgs e)
        {
            ddlAdviserBranchList.Items.Clear();
            ddlAdviseRMList.Items.Clear();
            BindListBranch();
            BindRMforBranchDropdown(0, 0);
            BindSubTypeDropDown(1001);
            trIndividualName.Visible = true;
            trNonIndividualName.Visible = false;

        }
        protected void rbtnNonIndividual_CheckedChanged(object sender, EventArgs e)
        {


            BindSubTypeDropDown(1002);
            ddlAdviserBranchList.Items.Clear();
            ddlAdviseRMList.Items.Clear();
            BindListBranch();
            BindRMforBranchDropdown(0, 0);
            trIndividualName.Visible = false;
            trNonIndividualName.Visible = true;

        }
        private void BindListBranch()
        {
            if (advisorVo.A_AgentCodeBased != 1)
            {
                UploadCommonBo uploadCommonBo = new UploadCommonBo();
                DataSet ds = uploadCommonBo.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviserBranchList.DataSource = ds.Tables[0];
                    ddlAdviserBranchList.DataTextField = "AB_BranchName";
                    ddlAdviserBranchList.DataValueField = "AB_BranchId";
                    //ddlAdviserBranchList.SelectedValue = "1339";
                    ddlAdviserBranchList.DataBind();
                    ddlAdviserBranchList.Items.Insert(0, new ListItem("Select", "Select"));
                }
                else
                {
                    ddlAdviserBranchList.Items.Insert(0, new ListItem("No Branches Available to Associate", "No Branches Available to Associate"));
                    ddlAdviserBranchList_CompareValidator2.ValueToCompare = "No Branches Available to Associate";
                    ddlAdviserBranchList_CompareValidator2.ErrorMessage = "Cannot Add Customer Without a Branch";
                }
            }
            else
            {
                DataSet BMDs = new DataSet();
                DataTable BMList = new DataTable();
                //BMList.Columns.Add("AB_BranchId");
                //BMList.Columns.Add("AB_BranchName");
                BMDs = advisorBranchBo.GetBMRoleForAgentBased(advisorVo.advisorId);

                if (BMDs.Tables[0].Rows.Count > 0)
                {
                    ddlAdviserBranchList.DataSource = BMDs;
                    ddlAdviserBranchList.DataValueField = BMDs.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlAdviserBranchList.DataTextField = BMDs.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlAdviserBranchList.DataBind();
                    ddlAdviserBranchList.Enabled = false;
                }
                else
                {
                    ddlAdviseRMList.Enabled = false;
                }

            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {


            if (advisorVo.A_AgentCodeBased != 1)
            {
                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviseRMList.DataSource = ds.Tables[0];
                    ddlAdviseRMList.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlAdviseRMList.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlAdviseRMList.DataBind();

                }
                else
                {
                    if (!IsPostBack)
                    {
                        ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                        CompareValidator2.ValueToCompare = "Select";
                        CompareValidator2.ErrorMessage = "Please select a RM";

                    }
                    else
                    {
                        if (rbtnNonIndividual.Checked == true)
                        {
                            if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                CompareValidator2.ValueToCompare = "Select";
                                CompareValidator2.ErrorMessage = "Please select a RM";
                            }
                            else
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Remove("Select");
                                ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                CompareValidator2.ValueToCompare = "No RM Available";
                                CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";


                            }
                        }
                        else
                        {
                            if ((IsPostBack) && (ddlAdviserBranchList.SelectedIndex == 0))
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Insert(0, new ListItem("Select", "Select"));
                                CompareValidator2.ValueToCompare = "Select";
                                CompareValidator2.ErrorMessage = "Please select a RM";
                            }
                            else
                            {
                                ddlAdviseRMList.Items.Clear();
                                ddlAdviseRMList.Items.Remove("Select");
                                ddlAdviseRMList.Items.Insert(0, new ListItem("No RM Available", "No RM Available"));
                                CompareValidator2.ValueToCompare = "No RM Available";
                                CompareValidator2.ErrorMessage = "Cannot Add Customer Without a RM";
                            }
                        }
                    }
                }
            }
            else
            {
                DataSet Rmds = new DataSet();

                Rmds = advisorBranchBo.GetRMRoleForAgentBased(advisorVo.advisorId);

                if (Rmds.Tables[0].Rows.Count > 0)
                {
                    ddlAdviseRMList.DataSource = Rmds;
                    ddlAdviseRMList.DataValueField = Rmds.Tables[0].Columns["RmID"].ToString();
                    ddlAdviseRMList.DataTextField = Rmds.Tables[0].Columns["RMName"].ToString();
                    ddlAdviseRMList.DataBind();
                    ddlAdviseRMList.Enabled = false;
                }
                else
                {
                    ddlAdviseRMList.Enabled = false;
                }
            }

        }
        private void BindSubTypeDropDown(int lookupParentId)
        {
            DataTable dtCustomerTaxSubTypeLookup = new DataTable();

            dtCustomerTaxSubTypeLookup = commonLookupBo.GetWERPLookupMasterValueList(2000, lookupParentId);
            ddlCustomerSubType.DataSource = dtCustomerTaxSubTypeLookup;
            ddlCustomerSubType.DataTextField = "WCMV_Name";
            ddlCustomerSubType.DataValueField = "WCMV_LookupId";
            ddlCustomerSubType.DataBind();
            if (rbtnIndividual.Checked == true)
                ddlCustomerSubType.SelectedValue = "2017";
            //ddlCustomerSubType.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<int> customerIds = null;
            try
            {

                Nullable<DateTime> dt = new DateTime();
                customerIds = new List<int>();
                lblPanDuplicate.Visible = false;
                if (Validation1())
                {
                    lblPanDuplicate.Visible = false;
                    userVo = new UserVo();
                    if (rbtnIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        //customerVo.RmId = rmVo.RMId;
                        if (customerVo.RmId == rmVo.RMId)
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                        }
                        else
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue);
                        }
                        customerVo.Type = "IND";

                        customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                        //customerVo.IsRealInvestor = chkRealInvestor.Checked;
                        customerVo.FirstName = txtFirstName.Text.ToString();
                        //customerVo.MiddleName = txtMiddleName.Text.ToString();
                        //customerVo.LastName = txtLastName.Text.ToString();
                        //if (ddlSalutation.SelectedIndex == 0)
                        //{
                        //    customerVo.Salutation = "";
                        //}
                        //else
                        //{
                        //    customerVo.Salutation = ddlSalutation.SelectedValue.ToString();
                        //}

                        userVo.FirstName = txtFirstName.Text.ToString();
                        //userVo.MiddleName = txtMiddleName.Text.ToString();
                        //userVo.LastName = txtLastName.Text.ToString();
                    }
                    else if (rbtnNonIndividual.Checked)
                    {
                        rmVo = (RMVo)Session["rmVo"];
                        tempUserVo = (UserVo)Session["userVo"];
                        //customerVo.RmId = rmVo.RMId;
                        //customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                        if (customerVo.RmId == rmVo.RMId)
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue.ToString());
                        }
                        else
                        {
                            customerVo.RmId = int.Parse(ddlAdviseRMList.SelectedValue);
                        }
                        customerVo.Type = "NIND";

                        customerVo.TaxStatusCustomerSubTypeId = Int16.Parse(ddlCustomerSubType.SelectedValue.ToString());
                        //customerVo.IsRealInvestor = chkRealInvestor.Checked;
                        customerVo.CompanyName = txtCompanyName.Text.ToString();
                        customerVo.FirstName = txtCompanyName.Text.ToString();
                        userVo.FirstName = txtCompanyName.Text.ToString();
                    }
                    if (customerVo.BranchId == rmVo.BranchId)
                    {
                        customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    }
                    else
                    {
                        customerVo.BranchId = int.Parse(ddlAdviserBranchList.SelectedValue);
                    }

                    //if (chkprospect.Checked)
                    //{
                    //    customerVo.IsProspect = 1;
                    //}
                    //else
                    //{
                    //    customerVo.IsProspect = 0;
                    //}

                    //customerVo.SubType = ddlCustomerSubType.SelectedItem.Value;
                    //customerVo.Email = txtEmail.Text.ToString();
                    customerVo.PANNum = txtPanNumber.Text.ToString();

                    customerVo.Dob = DateTime.MinValue;
                    customerVo.RBIApprovalDate = DateTime.MinValue;
                    customerVo.CommencementDate = DateTime.MinValue;
                    customerVo.RegistrationDate = DateTime.MinValue;
                    customerVo.Adr1State = null;
                    customerVo.Adr2State = null;
                    customerVo.ProfilingDate = DateTime.Today;
                    customerVo.UserId = userVo.UserId;
                    //userVo.Email = txtEmail.Text.ToString();
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerVo.ViaSMS = 1;
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, tempUserVo.UserId);
                    Session["Customer"] = "Customer";
                    int customerid = customerIds[1];
                    txtCustomerId.Value = customerid.ToString();
                    GetcustomerDetails();
                    ViewState["customerID"] = customerIds[1];
                    //BindCustomerNCDIssueList();

                    if (customerIds != null)
                    {

                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
                        trCustomerAdd.Visible = false;
                        //DataTable dtcustomer = OfflineIPOOrderBo.GetAddedCustomer(customerid);
                        //foreach (DataRow dr in dtcustomer.Rows)
                        //{
                        //    lblgetPan.Text = dr["C_PANNum"].ToString();
                        //    txtCustomerName.Text = dr["CustomerName"].ToString();
                        //    lblGetBranch.Text = dr["AB_BranchName"].ToString();
                        //}

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
                FunctionInfo.Add("Method", "CustomerType.ascx:btnSubmit_Click()");
                object[] objects = new object[5];
                objects[0] = customerIds;
                objects[1] = customerVo;
                objects[2] = rmVo;
                objects[3] = userVo;
                objects[4] = customerPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
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
        protected void lnkAddcustomer_OnClick(object sender, EventArgs e)
        {
            trCustomerAdd.Visible = true;
            lblPanDuplicate.Visible = false;
            rbtnIndividual.Checked = true;
            trNonIndividualName.Visible = false;
            BindListBranch();
            BindRMforBranchDropdown(0, 0);
            //BindListBranch(rmVo.RMId, "rm");
            BindSubTypeDropDown(1001);
            txtPanNumber.Text = "";
            txtFirstName.Text = "";
            txtPanNumber.Text = txtPansearch.Text;
            trPanExist.Visible = false;
        }
        protected void DematebtnSubmit_Click(object sender, EventArgs e)
        {
            int result = 0;
            string accountopeningdate = txtAccountOpeningDate.SelectedDate.ToString();
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
                    BindgvFamilyAssociate(result);
                    GetDematAccountDetails(customerId);
                    tdDemate.Visible = false;
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
        protected void ddlDepositoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepositoryName.SelectedItem.Text == "NSDL")
            {
                txtDPId.Enabled = true;
            }
            else if (ddlDepositoryName.SelectedItem.Text == "CDSL")
            {
                txtDPId.Enabled = false;
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
        public void ImageButton1_OnClick(object sender, ImageClickEventArgs e)
        {
            tdDemate.Visible = true;
            BindDepositoryType();
            BindModeofHolding();
            txtDpClientId.Text = "";
            txtDPId.Text = "";
            txtDpName.Text = "";

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
    }
}
