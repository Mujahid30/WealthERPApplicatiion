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
using System.Text.RegularExpressions;

namespace WealthERP.OffLineOrderManagement
{
    public partial class IPOIssueTransactOffline : System.Web.UI.UserControl
    {
        OnlineBondOrderBo OnlineBondBo = new OnlineBondOrderBo();
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
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();

            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());


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


            if (!IsPostBack)
            {

                if (AgentCode != null)
                {
                    txtAssociateSearch.Text = AgentCode;
                    OnAssociateTextchanged(this, null);
                }
                //gvJointHoldersList.Visible = false;
                BindARNNo(advisorVo.advisorId);
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
                    lblGetRM.Text = customerVo.RMName;
                    lblgetPan.Text = customerVo.PANNum;
                    BindPortfolioDropdown(customerId);
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

        private void BindARNNo(int adviserId)
        {
            DataSet dsArnNo = mfOrderBo.GetARNNo(adviserId);
            if (dsArnNo.Tables[0].Rows.Count > 0)
            {
                ddlARNNo.DataSource = dsArnNo;
                ddlARNNo.DataValueField = dsArnNo.Tables[0].Columns["Identifier"].ToString();
                ddlARNNo.DataTextField = dsArnNo.Tables[0].Columns["Identifier"].ToString();
                ddlARNNo.DataBind();
            }
            ddlARNNo.Items.Insert(0, new ListItem("Select", "Select"));
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


        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {

                //ddlAMCList.Enabled = true;
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                lblGetBranch.Text = customerVo.BranchName;
                lblGetRM.Text = customerVo.RMName;
                lblgetPan.Text = customerVo.PANNum;
                hdnCustomerId.Value = txtCustomerId.Value;
                customerId = int.Parse(txtCustomerId.Value);
                if (ddlsearch.SelectedItem.Value == "2")
                    lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;
                BindBank();
                BindPortfolioDropdown(customerId);
                BindISAList();
                BindCustomerNCDIssueList();
            }
        }

        private void BindBank()
        {
            CommonLookupBo commonLookupBo = new CommonLookupBo();
            //ddlBankName.Items.Clear();
            //DataTable dtBankName = new DataTable();
            //dtBankName = commonLookupBo.GetWERPLookupMasterValueList(7000, 0); ;
            //ddlBankName.DataSource = dtBankName;
            //ddlBankName.DataValueField = dtBankName.Columns["WCMV_LookupId"].ToString();
            //ddlBankName.DataTextField = dtBankName.Columns["WCMV_Name"].ToString();
            //ddlBankName.DataBind();
            //ddlBankName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));

        }
        private void BindPortfolioDropdown(int customerId)
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlPortfolio.DataSource = ds;
                //ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
                //ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
                //ddlPortfolio.DataBind();
                //hdnPortfolioId.Value = ddlPortfolio.SelectedValue;
            }
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

        }

        private void PaymentMode(string type)
        {
            //if (ddlPaymentMode.SelectedValue == "CQ")
            //{
            //    trPINo.Visible = true;
            //    txtPaymentInstDate.MaxDate = txtOrderDate.MaxDate;
            //}
            //else
            //{
            //    trPINo.Visible = false;
            //}
        }

        protected void ddlAMCList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }







        protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddMore_Click(object sender, EventArgs e)
        {



        }
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
                //BindStructureRuleGrid();
                //BindStructureRuleGrid(int.Parse(ddlIssueList.SelectedValue));
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



        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}