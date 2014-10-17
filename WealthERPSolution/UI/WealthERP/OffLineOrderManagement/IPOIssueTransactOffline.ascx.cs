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
        protected void Page_Load(object sender, EventArgs e)
            {

            SessionBo.CheckSession();

            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            rwDematDetails.VisibleOnPageLoad = false;
            GetUserType();

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
                AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode;
                AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";

            }

            this.txtApplicationNo.Text = string.Empty;
            if (!IsPostBack)
            {

                if (AgentCode != null)
                {
                    txtAssociateSearch.Text = AgentCode;
                    OnAssociateTextchanged(this, null);
                }
                //gvJointHoldersList.Visible = false;

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
                    //BindPortfolioDropdown(customerId);
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


        //protected void ShowTransactionType(int type)
        //{
        //    if (type == 0)
        //    {
        //        trAmount.Visible = false;
        //        trPINo.Visible = false;
        //        trBankName.Visible = false;
        //        trFrequency.Visible = false;
        //        trSIPStartDate.Visible = false;
        //        trAddress6.Visible = false;

        //        trSection2.Visible = false;

        //        trGetAmount.Visible = false;
        //        trRedeemed.Visible = false;
        //        trScheme.Visible = false;
        //        trFrequencySTP.Visible = false;
        //        trSTPStart.Visible = false;

        //        trSection3.Visible = false;

        //        trAddress1.Visible = false;
        //        trOldLine1.Visible = false;
        //        trOldLine3.Visible = false;
        //        trOldCity.Visible = false;
        //        trOldPin.Visible = false;
        //        trAddress6.Visible = false;
        //        trNewLine1.Visible = false;
        //        trNewLine3.Visible = false;
        //        trNewCity.Visible = false;
        //        trNewPin.Visible = false;



        //        trBtnSubmit.Visible = false;
        //    }
        //    else if (type == 1)
        //    {
        //        trAmount.Visible = true;
        //        trPINo.Visible = true;
        //        trBankName.Visible = true;
        //        trFrequency.Visible = true;
        //        trSIPStartDate.Visible = true;
        //        trAddress6.Visible = true;

        //        trSection2.Visible = false;

        //        trGetAmount.Visible = false;
        //        trRedeemed.Visible = false;
        //        trScheme.Visible = false;
        //        trFrequencySTP.Visible = false;
        //        trSTPStart.Visible = false;

        //        trSection3.Visible = false;

        //        trAddress1.Visible = false;
        //        trOldLine1.Visible = false;
        //        trOldLine3.Visible = false;
        //        trOldCity.Visible = false;
        //        trOldPin.Visible = false;
        //        trAddress6.Visible = false;
        //        trNewLine1.Visible = false;
        //        trNewLine3.Visible = false;
        //        trNewCity.Visible = false;
        //        trNewPin.Visible = false;

        //        trBtnSubmit.Visible = true;

        //    }
        //    else if (type == 2)
        //    {
        //        trAmount.Visible = false;
        //        trPINo.Visible = false;
        //        trBankName.Visible = false;
        //        trFrequency.Visible = false;
        //        trSIPStartDate.Visible = false;
        //        trAddress6.Visible = false;

        //        trSection2.Visible = false;

        //        if (advisorVo.A_AgentCodeBased == 1)
        //        {
        //            trGetAmount.Visible = false;
        //        }
        //        else
        //        {
        //            trGetAmount.Visible = false;
        //        }
        //        trRedeemed.Visible = true;
        //        //sai-D   trScheme.Visible = true;
        //        //sai-D trFrequencySTP.Visible = true;
        //        //sai-D   trSTPStart.Visible = true;

        //        trSection3.Visible = false;

        //        trAddress1.Visible = false;
        //        trOldLine1.Visible = false;
        //        trOldLine3.Visible = false;
        //        trOldCity.Visible = false;
        //        trOldPin.Visible = false;
        //        trAddress6.Visible = false;
        //        trNewLine1.Visible = false;
        //        trNewLine3.Visible = false;
        //        trNewCity.Visible = false;
        //        trNewPin.Visible = false;

        //        trBtnSubmit.Visible = true;

        //    }
        //    if (type == 3)
        //    {
        //        trAmount.Visible = false;
        //        trPINo.Visible = false;
        //        trBankName.Visible = false;
        //        trFrequency.Visible = false;
        //        trSIPStartDate.Visible = false;
        //        trAddress6.Visible = false;

        //        trSection2.Visible = false;

        //        trGetAmount.Visible = false;
        //        trRedeemed.Visible = false;
        //        trScheme.Visible = false;
        //        trFrequencySTP.Visible = false;
        //        trSTPStart.Visible = false;

        //        //sai    trSystematicDateChk1.Visible = false ;;


        //        trSection3.Visible = false;

        //        trAddress1.Visible = true;
        //        trOldLine1.Visible = true;
        //        trOldLine3.Visible = true;
        //        trOldCity.Visible = true;
        //        trOldPin.Visible = true;
        //        trAddress6.Visible = true;
        //        trNewLine1.Visible = true;
        //        trNewLine3.Visible = true;
        //        trNewCity.Visible = true;
        //        trNewPin.Visible = true;

        //        trBtnSubmit.Visible = true;
        //    }

        //}
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
            if (ddlsearch.SelectedValue == "2")
            {
                lblgetcust.Text = "";

            }
        }

        protected void OnAssociateTextchanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtAssociateSearch.Text))
            {
                int recCount = customerBo.ChkAssociateCode(advisorVo.advisorId, txtAssociateSearch.Text);
                if (recCount == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Agent Code is invalid!');", true);
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('SubBroker Code is invalid!');", true);

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
            lblgetPan.Text = customerVo.PANNum;
            hdnCustomerId.Value = txtCustomerId.Value;
            hdnPortfolioId.Value = customerPortfolioVo.PortfolioId.ToString();
            customerId = int.Parse(txtCustomerId.Value);
            if (ddlsearch.SelectedItem.Value == "2")
                lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
            BindBank();
            BindISAList();
            BindCustomerNCDIssueList();
            GetDematAccountDetails(int.Parse(txtCustomerId.Value));
            Panel1.Visible = true;

        }
        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {

                GetcustomerDetails();
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
            dtIssueList = onlineNCDBackOfficeBO.GetIssueList(advisorVo.advisorId, 1, int.Parse(hdnCustomerId.Value), "IP");
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

                BindIPOIssueList(ddlIssueList.SelectedValue.ToString());
                BindIPOBidGrid(3);
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

        private void BindIPOIssueList(string issueId)
        {
            dtOnlineIPOIssueList = OfflineIPOOrderBo.GetIPOIssueList(advisorVo.advisorId, Convert.ToInt32(issueId), 1, int.Parse(txtCustomerId.Value));
            RadGridIPOIssueList.DataSource = dtOnlineIPOIssueList;
            RadGridIPOIssueList.DataBind();
            pnlIPOControlContainer.Visible = true;

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
                dtIPOBid.Rows.Add(drIPOBid);

            }

            RadGridIPOBid.DataSource = dtIPOBid;
            RadGridIPOBid.DataBind();


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
            CustomValidator1.IsValid = true;
            Page.Validate("btnConfirmOrder");

        }

        protected void CutOffCheckBox_Changed(object sender, EventArgs e)
        {
            int currentRowindex = (((GridDataItem)((CheckBox)sender).NamingContainer).RowIndex / 2) - 1;
            ReseIssueBidValues(currentRowindex, false);
            //CheckBox chkCutOff = (CheckBox)RadGridIPOBid.MasterTableView.Items[currentRowindex]["CheckCutOff"].FindControl("cbCutOffCheck");
            Page.Validate("btnConfirmOrder");

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
            Page.Validate();
            if (Page.IsValid)
            {
                isBidsVallid = ValidateIPOBids(out errorMsg);
                if (!isBidsVallid)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + errorMsg + "');", true);
                    return;
                }
                else
                {
                    CreateIPOOrder();
                    ControlsVisblity(true);
                 
                }
            }





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
            DataRow drIPOBid;
            onlineIPOOrderVo.CustomerId = int.Parse(txtCustomerId.Value);
            onlineIPOOrderVo.IssueId = issueId;
            onlineIPOOrderVo.AssetGroup = "IP";
            onlineIPOOrderVo.IsOrderClosed = false;
            onlineIPOOrderVo.IsOnlineOrder = false;
            onlineIPOOrderVo.IsDeclarationAccepted = true;
            onlineIPOOrderVo.OrderDate = DateTime.Now;
            int radgridRowNo = 0;
            int dematAccountId = 0;
            foreach (GridDataItem gvr in gvDematDetailsTeleR.MasterTableView.Items)
            {
                if (((CheckBox)gvr.FindControl("chkDematId")).Checked == true)
                {
                    dematAccountId = int.Parse(gvDematDetailsTeleR.MasterTableView.DataKeyValues[gvr.ItemIndex]["CEDA_DematAccountId"].ToString());
                    break;
                }

            }
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

                drIPOBid["BankName"] = ddlBankName.SelectedItem.Text;

                if (!string.IsNullOrEmpty(txtBranchName.Text.Trim()))
                    drIPOBid["BranchName"] = txtBranchName.Text.Trim();
                else
                    drIPOBid["BranchName"] = DBNull.Value;

                if (ddlPaymentMode.SelectedValue == "CQ")
                {
                    if (!string.IsNullOrEmpty(txtPaymentNumber.Text.Trim()))
                        drIPOBid["ChequeNo"] = txtPaymentNumber.Text.Trim();
                    if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString()))
                        drIPOBid["ChequeDate"] = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());

                }
                drIPOBid["DematId"] = dematAccountId;

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
            int agentId = 0;
            if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
                dtAgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            if (dtAgentId.Rows.Count > 0)
            {
                agentId = int.Parse(dtAgentId.Rows[0][1].ToString());
            }
            orderId = OfflineIPOOrderBo.CreateIPOBidOrderDetails(advisorVo.advisorId, userVo.UserId, dtIPOBidTransactionDettails, onlineIPOOrderVo, agentId, txtAssociateSearch.Text);
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
                OfflineBondOrderBo.CreateOfflineCustomerOrderAssociation(dtJntHld, userVo.UserId, orderId);
            }
            userMessage = CreateUserMessage(orderId, accountDebitStatus, isCutOffTimeOver);

            ShowMessage(userMessage);
            

        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
          

        }

        private string CreateUserMessage(int orderId, bool accountDebitStatus, bool isCutOffTimeOver)
        {
            string userMessage = string.Empty;
            if (orderId != 0)
            {

                userMessage = "Order placed successfully, Order reference no is " + orderId.ToString();
            }

            return userMessage;

        }

        public void btnAddMore_Click(object sender, EventArgs e)
        {
           
            string errorMsg = string.Empty;
            bool isBidsVallid = false;
            Page.Validate();
            if (Page.IsValid)
            {
                isBidsVallid = ValidateIPOBids(out errorMsg);
                if (!isBidsVallid)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + errorMsg + "');", true);
                    return;
                }
                else
                {

                    CreateIPOOrder();
                    //ControlsVisblity(true);
                    ClearAllFields();
                    btnConfirmOrder.Visible = true;
                    tblMessage.Visible = false;
                }

            }
        }
        public void ClearAllFields()
        {
            txtCustomerName.Text= "";
            ddlsearch.SelectedIndex = -1;
            txtCustomerName.Text =null;
            txtAssociateSearch.Text = "";
            lblgetPan.Text = "";
            lblGetBranch.Text = "";
            txtAssociateSearch.Text = "";
            lblAssociatetext.Text = "";
            ddlIssueList.SelectedIndex = 0;
            txtApplicationNo.Text = null;
            ddlPaymentMode.SelectedIndex = 0;
            ddlBankName.SelectedIndex = -1;
            txtBranchName.Text = "";
            txtAmount.Text = "";
            txtPaymentNumber.Text = "";
            txtPaymentInstDate.SelectedDate = null;
            txtDematid.Text = string.Empty;
            txtASBANO.Text = "";
            pnlIPOControlContainer.Visible = false;
            pnlJointHolderNominee.Visible = false;
            
           
        }
        protected void lnkTermsCondition_Click(object sender, EventArgs e)
        {
            rwTermsCondition.VisibleOnPageLoad = true;
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
            btnAddMore.Visible = false;

        }

        private bool ValidateIPOBids(out string msg)
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
            if (minBidAmount > maxPaybleAmount || maxBidAmount < maxPaybleAmount)
            {
                msg = "Bid Value (Amount Payable) should be greater than the Min bid amount and less than the Max bid amount";
                isBidValid = false;
                return isBidValid;
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

                if (bidAmountPayble <= 0 && int.Parse(item.GetDataKeyValue("IssueBidNo").ToString()) == 1)
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




                        //else if (e.Item is GridFooterItem)
                        //{
                        //    GridFooterItem footerItem = (GridFooterItem)e.Item;
                        //    CompareValidator cmpMaxBidAmount = (CompareValidator)footerItem.FindControl("cmpFinalBidAmountPayable");

                        //    cmpMaxBidAmount.ValueToCompare = 0.ToString();
                        //}
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
            RadDepository.VisibleOnPageLoad = true;

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
        protected void lnkBtnDemat_onClick(object sender, EventArgs e)
        {
            GetDematAccountDetails(int.Parse(txtCustomerId.Value));
            rwDematDetails.VisibleOnPageLoad = true;
        }

    }
}
