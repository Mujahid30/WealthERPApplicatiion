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
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VOAssociates;
using BOAssociates;
using iTextSharp.text.pdf;
using System.IO;
using System.Resources;
using System.Web.UI.HtmlControls;
namespace WealthERP.OPS
{
   

    public partial class ProductOrderMaster : System.Web.UI.UserControl
    {


        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        OperationBo operationBo = new OperationBo();
        MFOrderBo mfOrderBo = new MFOrderBo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();

        AssetBo assetBo = new AssetBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        OrderBo orderbo = new OrderBo();
        MFOrderVo mforderVo = new MFOrderVo();
        OrderVo orderVo = new OrderVo();
        FIOrderBo fiorderBo = new FIOrderBo();
        FIOrderVo fiorderVo = new FIOrderVo();
        RMVo rmVo = new RMVo();
        AssociatesBo associatesBo = new AssociatesBo();
        AssociatesVO associatesVo = new AssociatesVO();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        UserVo userVo;
        PriceBo priceBo = new PriceBo();
        string path;
        DataTable dtBankName = new DataTable();
        DataTable dtFrequency;
        DataTable ISAList;
        int customerId;
        int amcCode;
        string categoryCode;
        int portfolioId;
        int schemePlanCode;
        int Aflag = 0;
        int Sflag = 0;
        int orderId;
        int orderNumber = 0;
        string ViewForm = string.Empty;
        string updatedStatus = "";
        string updatedReason = "";
        bool result = false;
        string userType = string.Empty;
        string mail = string.Empty;
        string AgentCode;
        DataTable AgentId;
        DataTable Agentname;
        String TransType;
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetProductWiseControls();

            //if (GetProductWiseControls()==false  )
            //{
            //    ShowTransactionType(0);
            //    return;

            //}

            //divOrderDet.Visible = false;
 
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
            SessionBo.CheckSession();
            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            orderNumber = mfOrderBo.GetOrderNumber();
            orderNumber = orderNumber + 1;
           //sai //sai  lblGetOrderNo.Text = orderNumber.ToString();
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
            if (Session["mforderVo"] != null && Session["orderVo"] != null)
            {
                mforderVo = (MFOrderVo)Session["mforderVo"];
                orderVo = (OrderVo)Session["orderVo"];
            }
               lblGetBranch.Visible = false;
               lblBranch.Visible = false;
             lblRM.Visible = false;
             lblGetRM.Visible = false;
            //sai cvOrderDate.ValueToCompare = DateTime.Now.ToShortDateString();
            //CVPaymentdate2.ValueToCompare = //sai  txtOrderDate.SelectedDate.ToString();
             
                //CVPaymentdate2.ValueToCompare = //sai  txtOrderDate.SelectedDate.ToString();

                   gvJointHoldersList.Visible = false;
                BindARNNo(advisorVo.advisorId);
                //BindAgentDropList(userType);
                //BindSearchDropdownList(sender, e);
                //sai  hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowInitialIsa();", true);

              //sai //sai             ddlAMCList..Enabled = false;
                if (userType == "associates")
                {
                    // BindSubBrokerAgentCode(AgentCode);
                }
                if (Request.QueryString["action"] != null)
                {

                    //sai  lnlBack.Visible = true;
                    ViewForm = Request.QueryString["action"].ToString();
                    //sai  txtOrderDate.SelectedDate = orderVo.OrderDate;
                    //sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    //sai  btnViewInPDFNew.Visible = false;
                    //sai  btnViewInDOCNew.Visible = false;
                }
                if (Request.QueryString["action"] != null)
                {




                }

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
                if (Request.QueryString["CustomerId"] != null)
                {
                    customerId = Convert.ToInt32(Request.QueryString["CustomerId"]);
                    customerVo = customerBo.GetCustomer(customerId);

                    hdnCustomerId.Value = customerVo.CustomerId.ToString();
                    txtCustomerName.Text = customerVo.FirstName + customerVo.MiddleName + customerVo.LastName;
                    lblGetBranch.Text = customerVo.BranchName;
                    lblGetRM.Text = customerVo.RMName;
                    lblgetPan.Text = customerVo.PANNum;
                    BindPortfolioDropdown(customerId);
                }
                //sai cvAppRcvDate.ValueToCompare = DateTime.Today.ToShortDateString();
                //sai cvFutureDate1.ValueToCompare = DateTime.Today.ToShortDateString();
               // BindAMC(0);
               // BindScheme(0);
               // ////sai        BindFolioNumber(0);
               // //BindOrderStatus();
               // BindCategory();
               ////sai  BindState();
               // BindFrequency();
               //sai ShowHideFields(1);
                  


                //if (mforderVo != null && orderVo != null)
                //{
                //    if (ViewForm == "View")
                //    {
                //        SetControls("View", mforderVo, orderVo);
                //       // btnAddMore.Visible = false;
                //    }
                //    else if (ViewForm == "Edit")
                //    {
                //        SetControls("Edit", mforderVo, orderVo);
                //      //  btnAddMore.Visible = false;
                //    }
                //    else if (ViewForm == "entry")
                //    {
                //        SetControls("Entry", mforderVo, orderVo);
                //    }
                //    else
                //    {
                //        //cvAppRcvDate.ValueToCompare = DateTime.Now.ToShortDateString();
                //        //cvOrderDate.ValueToCompare = DateTime.Now.ToShortDateString();
                //        //cvFutureDate1.ValueToCompare = DateTime.Now.ToShortDateString();
                //    }
                //}
                //else
                //{
                //    SetControls("Entry", mforderVo, orderVo);

                //}

       
             //sai trbtnSubmit.Visible = true;
               //sai trbtnSubmit.Visible = true;

            if (!IsPostBack)
            {
                trpan.Visible = false;
                trCust.Visible = false;
                PlaceHolder1.Visible = false;
                FixedIncomePlaceHolder.Visible = false;
                ShowTransactionType(0);
                GetProductWiseControls();
                pnlOrderSteps.Visible = false;
                tr1.Visible = false;
                //Session.Remove("ChildControl");
            }
            GetProductWiseControls();
            //// ShowHideFields(1);

             //if (Session["ChildControl"] != null)
             //{
             //    PlaceHolder1.Visible = true;
             //    if (Session["ChildControl"].ToString() == "MF")                  
             //        ChildControls("ProductOrderDetailsMF");
             //    else if (Session["ChildControl"].ToString() == "FI")
             //        ChildControls("ProductOrderDetailsMF");

             //}
             //else{

             //   // PlaceHolder1.Visible = false;
             //}

             //if (Session["MFTranstype"] != null)
             //{
             //    hdnTransType_ValueChanged1(this, null);
             //    //ShowTransactionType(1);
             //}
             //else
             //{

             //    ShowTransactionType(0);
             //}
            // GetFIPaymentSection();
            //string pageID = "ProductOrderDetailsMF";
            ////string path = "~/OPS/ProductOrderDetailsMF.ascx";
            //string path1 = Getpagepath(pageID);
            //UserControl uc1 = new UserControl();
            //uc1 = (UserControl)this.Page.LoadControl(path1);
            //uc1.ID = "ctrl_" + pageID;
            //PlaceHolder1.Controls.Add(uc1);
            OnTaxStatus();
        }

        private void GetFIPaymentSection()
        {
            if (Session["ChildControl"] != null)
            {
                if (Session["ChildControl"].ToString() == "FI")
                {
                    ShowTransactionType(1);
                }
            }
        }

        private void  GetProductWiseControls()
        {

            if (DdlLoad.SelectedValue == "0")
            {
                trCustSect.Visible = false;
                trCustSearch.Visible = false;                
                trCust.Visible = false;
               PlaceHolder1.Visible = false;
               FixedIncomePlaceHolder.Visible = false;
                trAssociateSearch.Visible = false;
                trOrderSection.Visible = false;
                trDepositedBank.Visible = false;
                trTaxStatus.Visible = false;
                
            }
            else
            {
                trCustSect.Visible = true;
                trCustSearch.Visible = true;
                trCust.Visible = true;
                
              // PlaceHolder1 .Visible = true;
              //  trpan.Visible = true;
                                trAssociateSearch.Visible = true;
                trOrderSection.Visible = true;
                //return true;
                ShowTransactionType(1);
                if (DdlLoad.SelectedValue == "1")
                {
                    ddlARNNo.Visible = true ;
                    lblARNNo.Visible = true;
                    
                                        //PlaceHolder1.Visible = true;
                    //FixedIncomePlaceHolder.Visible = false;
                }
                if (DdlLoad.SelectedValue == "2")
                {
                    PlaceHolder1.Visible = false;
                    FixedIncomePlaceHolder.Visible = true;
                    trFrequency.Visible = false;
                    trSIPStartDate.Visible = false;
                    trDepositedBank.Visible = true;
                    txtAmount.Visible = false;
                    lblAmount.Visible = false;
                    trTaxStatus.Visible = false;
                    ddlARNNo.Visible = false;
                    lblARNNo.Visible = false;
                    trTaxStatus.Visible = true;
                }

            }


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

        //protected void BindSubBrokerAgentCode(string AgentCode)
        //{
        //    DataTable dtAgentListList = new DataTable();
        //    dtAgentListList = orderbo.GetSubBrokerAgentCode(AgentCode);

        //    if (dtAgentListList.Rows.Count > 0)
        //    {
        //        ddlAssociate.DataSource = dtAgentListList;
        //        ddlAssociate.DataValueField = dtAgentListList.Columns["ACC_AgentId"].ToString();
        //        ddlAssociate.DataTextField = dtAgentListList.Columns["AAC_AgentCode"].ToString();
        //        ddlAssociate.DataBind();
        //    }
        //    ddlAssociate.Items.Insert(0, new ListItem("Select", "0"));
        //}


        //private void BindAgentDropList(string userRole)
        //{
        //    DataTable dtAgentListList = new DataTable();
        //    if (userRole.ToLower() == "ops" || userRole.ToLower() == "advisor")
        //    {
        //        dtAgentListList = orderbo.GetAllAgentListForOrder(advisorVo.advisorId, "admin");
        //    }
        //    else if (userRole == "rm")
        //    {
        //        dtAgentListList = orderbo.GetAllAgentListForOrder(advisorVo.advisorId, userRole);
        //    }
        //    //else if (userRole == "associates")
        //    //{
        //    //    dtAgentListList = orderbo.GetSubBrokerAgentCode(AgentCode);
        //    //}

        //    if (dtAgentListList.Rows.Count > 0)
        //    {
        //        ddlAssociate.DataSource = dtAgentListList;
        //        ddlAssociate.DataValueField = dtAgentListList.Columns["AgentId"].ToString();
        //        ddlAssociate.DataTextField = dtAgentListList.Columns["AgentName"].ToString();
        //        ddlAssociate.DataBind();
        //    }
        //    ddlAssociate.Items.Insert(0, new ListItem("Select(SubBroker Code/Name/Type)", "0"));
        //}

        protected void imgBtnRefereshBank_OnClick(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank(Convert.ToInt32(txtCustomerId.Value));
            BindDepositedBank(Convert.ToInt32(txtCustomerId.Value));
        }
        private void FISetControls(string action,FIOrderVo  fiorderVo,OrderVo orderVo)
        {
            if (action == "Entry")
            {
                if (mforderVo != null && orderVo != null)
                {
                    SetEditViewMode(false);
                    //ddlBranch.SelectedIndex = 0;
                    //ddlRM.SelectedIndex = 0;
                    txtCustomerName.Text = "";
                    //sai
                    //ddltransType.SelectedIndex = 0;
                    //txtReceivedDate.SelectedDate = null;
                    //txtApplicationNumber.Text = "";
                    //sai             ddlAMCList..SelectedIndex = 0;
                    // ddlCategory.SelectedIndex = 0;
                    //sai      ddlAmcSchemeList.SelectedIndex = 0;
                    //ddlPortfolio.SelectedIndex = -1;
                    //ddlFolioNumber.SelectedIndex = 0;
                    //  //sai  txtOrderDate.SelectedDate = null;
                    // //sai  lblGetOrderNo.Text = "";
                    //ddlOrderPendingReason.SelectedIndex = 0;
                    //ddlOrderStatus.SelectedIndex = 0;
                    // //sai txtFutureDate.SelectedDate = null;
                    // //sai txtFutureTrigger.Text = "";
                    txtAmount.Text = "";
                    ddlPaymentMode.SelectedIndex = 0;
                    txtPaymentNumber.Text = "";
                    txtPaymentInstDate.SelectedDate = null;
                    ddlBankName.SelectedIndex = 0;
                    txtBranchName.Text = "";
                    lblGetAvailableAmount.Text = "";
                    lblGetAvailableUnits.Text = "";
                    ddlSchemeSwitch.SelectedIndex = 0;
                    txtCorrAdrLine1.Text = "";
                    txtCorrAdrLine2.Text = "";
                    txtCorrAdrLine3.Text = "";
                    txtLivingSince.SelectedDate = null;
                    txtCorrAdrCity.Text = "";
                    ddlCorrAdrState.SelectedIndex = 0;
                    txtCorrAdrPinCode.Text = "";
                    ddlFrequencySIP.SelectedIndex = -1;
                    ddlFrequencySTP.SelectedIndex = -1;
                    txtstartDateSIP.SelectedDate = null;
                    txtendDateSIP.SelectedDate = null;
                    txtstartDateSTP.SelectedDate = null;
                    txtendDateSTP.SelectedDate = null;
                    txtNewAmount.Text = "";
                    //   ddlAssociate.SelectedIndex = 0;
                    ddlARNNo.SelectedIndex = 0;
                }
            }
            else if (action == "Edit")
            {
                if (mforderVo != null && orderVo != null)
                {
                    SetEditViewMode(false);
                    if (orderVo.AgentId != 0)
                    //    ddlAssociate.SelectedValue = orderVo.AgentId.ToString();
                    {
                        AgentId = customerBo.GetAgentId(advisorVo.advisorId, int.Parse(orderVo.AgentId.ToString()));
                        if (AgentId.Rows.Count > 0)
                        {
                            txtAssociateSearch.Text = AgentId.Rows[0][2].ToString();
                        }
                        else
                            txtAssociateSearch.Text = string.Empty;
                        Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                        if (Agentname.Rows.Count > 0)
                        {
                            lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                        }
                        lblAssociatetext.Text = string.Empty;

                    }
                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedValue = mforderVo.ARNNo;
                    orderId = orderVo.OrderId;
                    ddlsearch.Enabled = true;
                    txtAssociateSearch.Enabled = true;
                    txtCustomerName.Enabled = true;
                    txtCustomerName.Text = mforderVo.CustomerName;
                    if (orderVo.CustomerId != 0)
                        hdnCustomerId.Value = orderVo.CustomerId.ToString();
                    BindPortfolioDropdown(orderVo.CustomerId);
                    customerVo = customerBo.GetCustomer(orderVo.CustomerId);
                    lblGetBranch.Text = mforderVo.BMName;
                    lblGetRM.Text = mforderVo.RMName;
                    lblgetPan.Text = mforderVo.PanNo;
                    //TransType = mforderVo.TransactionCode;
                    // //sai  txtOrderDate.SelectedDate = orderVo.OrderDate;

                    //  //sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    hdnType.Value = mforderVo.TransactionCode;

                    if (TransType == "CAF")
                    {
                        //sai  ShowTransactionType(3);
                        //lblAMC.Visible = false;
                        ////ddlAMCList.Visible = false;
                        //lblCategory.Visible = false; ddlCategory.Visible = false;
                        //lblSearchScheme.Visible = false; //sai      ddlAmcSchemeList.Visible = false;
                        //lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                        //spnAMC.Visible = false; spnScheme.Visible = false;
                        if (customerVo != null)
                        {
                            lblGetLine1.Text = customerVo.Adr1Line1;
                            lblGetLine2.Text = customerVo.Adr1Line2;
                            lblGetline3.Text = customerVo.Adr1Line3;
                            lblgetCity.Text = customerVo.Adr1City;
                            lblGetstate.Text = customerVo.Adr1State;
                            lblGetPin.Text = customerVo.Adr1PinCode.ToString();
                            lblGetCountry.Text = customerVo.Adr1Country;
                        }
                        else
                        {
                            lblGetLine1.Text = "";
                            lblGetLine2.Text = "";
                            lblGetline3.Text = "";
                            lblgetCity.Text = "";
                            lblGetstate.Text = "";
                            lblGetPin.Text = "";
                            lblGetCountry.Text = "";
                        }
                    }
                    else
                    {
                        BindAMC(0);
                        //sai             ddlAMCList..SelectedValue = mforderVo.Amccode.ToString();
                        BindCategory();
                        // ddlCategory.SelectedValue = mforderVo.category;
                        BindPortfolioDropdown(orderVo.CustomerId);
                        //  ddlPortfolio.SelectedValue = mforderVo.portfolioId.ToString();
                        BindScheme(0);
                        //sai      ddlAmcSchemeList.SelectedItem.Value = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();

                    }
                    portfolioId = mforderVo.portfolioId;
                    if (TransType == "BUY" || TransType == "ABY")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (TransType == "SIP")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = true;
                        ddlFrequencySIP.SelectedValue = mforderVo.FrequencyCode;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSIP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSIP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSIP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSIP.SelectedDate = null;
                    }
                    else if (TransType == "SWB")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = true;

                    }
                    else if (TransType == "STB")
                    {
                        //sai  ShowTransactionType(2);
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        trScheme.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    else if (TransType == "Sel")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                    }
                    else if (TransType == "SWP")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    if (TransType == "SIP" || TransType == "BUY" || TransType == "CAF")
                    {
                        ////sai        BindFolioNumber(0);
                        // if (mforderVo.accountid != 0)
                        //ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {

                        if (mforderVo.accountid != 0)
                        {
                            //sai        BindFolioNumber(0);
                            //   ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        }
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    //  txtReceivedDate.Enabled = false;
                    if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    {
                        // txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
                    }

                    else
                        //    txtReceivedDate.SelectedDate = DateTime.Now;
                        //txtApplicationNumber.Enabled = false;
                        //txtApplicationNumber.Text = orderVo.ApplicationNumber;
                        ////sai  txtOrderDate.SelectedDate = orderVo.OrderDate;
                        ////sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();

                        if (mforderVo.FutureExecutionDate != DateTime.MinValue)
                        {
                            //  //sai txtFutureDate.SelectedDate = mforderVo.FutureExecutionDate;
                        }
                        else
                        {
                            //  //sai txtFutureDate.SelectedDate = null;
                        }
                    if (mforderVo.IsImmediate == 1)
                    {
                        //  rbtnImmediate.Checked = true;
                    }
                    else
                    {
                        //  rbtnFuture.Checked = true;
                        // trfutureDate.Visible = true;
                    }
                    //  //sai txtFutureTrigger.Text = mforderVo.FutureTriggerCondition;
                    txtAmount.Text = mforderVo.Amount.ToString();
                    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
                    {
                        if (mforderVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = mforderVo.Amount.ToString();
                        }
                        else
                        {
                            rbtAmount.Checked = false;
                            txtNewAmount.Text = "";
                        }

                        if (mforderVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = mforderVo.Units.ToString();
                        }
                        else
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = "";
                        }

                        //lblGetAvailableAmount.Text = operationVo.Amount.ToString();
                        //lblGetAvailableUnits.Text = operationVo.Units.ToString();
                        lblAvailableAmount.Visible = false;
                        lblAvailableUnits.Visible = false;
                    }

                    ddlPaymentMode.SelectedValue = orderVo.PaymentMode;
                    txtPaymentNumber.Text = orderVo.ChequeNumber;
                    if (orderVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = orderVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate = null;
                    BindBank(orderVo.CustomerId);
                    if (orderVo.CustBankAccId != 0)
                        ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();
                    else
                        ddlBankName.SelectedValue = "Select";

                    txtBranchName.Text = mforderVo.BranchName;
                    txtCorrAdrLine1.Text = mforderVo.AddrLine1;
                    txtCorrAdrLine2.Text = mforderVo.AddrLine2;
                    txtCorrAdrLine3.Text = mforderVo.AddrLine3;
                    if (mforderVo.LivingSince != DateTime.MinValue)
                        txtLivingSince.SelectedDate = mforderVo.LivingSince;
                    else
                        txtLivingSince.SelectedDate = null;
                    //txtLivingSince.Text = operationVo.LivingSince.ToShortDateString();
                    txtCorrAdrCity.Text = mforderVo.City;
                    if (!string.IsNullOrEmpty(mforderVo.State.ToString().Trim()))
                        ddlCorrAdrState.SelectedItem.Text = mforderVo.State;
                    txtCorrAdrPinCode.Text = mforderVo.Pincode;

                    //  //sai btnSubmit.Visible = false;
                    ////sai btnUpdate.Visible = true;
                    //trReportButtons.Visible = true;
                    btnImgAddCustomer.Visible = false;
                    //btnAddMore.Visible = false;
                    ////sai rgvOrderSteps.Visible = true;
                    ////sai rgvOrderSteps.Enabled = true;
                    if (Request.QueryString["action"] != null)
                        orderId = orderVo.OrderId;
                    else
                        orderId = (int)Session["CO_OrderId"];
                    BindOrderStepsGrid();
                    //btnViewReport.Visible = true;
                    //btnViewInPDF.Visible = true;
                    //btnViewInDOC.Visible = true;
                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedItem.Text = mforderVo.ARNNo;
                    else
                        ddlARNNo.SelectedIndex = 0;
                    ////sai  lnlBack.Visible = true;
                    ////sai lnkDelete.Visible = true;
                }
            }

            else if (action == "View")
            {
                if (mforderVo != null && orderVo != null)
                {
                    ddlsearch.SelectedItem.Value = "0";
                    ddlsearch.Enabled = false;

                    SetEditViewMode(true);
                    orderId = orderVo.OrderId;
                    txtCustomerName.Enabled = false;
                    txtCustomerName.Text = mforderVo.CustomerName;
                    if (orderVo.CustomerId != 0)
                        hdnCustomerId.Value = orderVo.CustomerId.ToString();
                    BindPortfolioDropdown(orderVo.CustomerId);
                    txtAssociateSearch.Enabled = false;
                    customerVo = customerBo.GetCustomer(orderVo.CustomerId);
                    lblGetBranch.Text = mforderVo.BMName;
                    lblGetRM.Text = mforderVo.RMName;
                    lblgetPan.Text = mforderVo.PanNo;
                    //  ddltransType.Enabled = false;
                    TransType = mforderVo.TransactionCode;
                    // //sai  txtOrderDate.SelectedDate = orderVo.OrderDate;
                    // //sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    hdnType.Value = mforderVo.TransactionCode;
                    if (orderVo.AgentId != 0)
                    //ddlAssociate.SelectedValue = orderVo.AgentId.ToString();
                    {
                        AgentId = customerBo.GetAgentId(advisorVo.advisorId, int.Parse(orderVo.AgentId.ToString()));
                        if (AgentId.Rows.Count > 0)
                        {
                            txtAssociateSearch.Text = AgentId.Rows[0][2].ToString();
                        }
                        Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                        if (Agentname.Rows.Count > 0)
                        {
                            lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                        }
                    }
                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedValue = mforderVo.ARNNo;

                    if (TransType == "BUY" || TransType == "ABY")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (TransType == "SIP")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = true;
                        trSIPStartDate.Visible = true;
                        ddlFrequencySIP.Enabled = false;
                        ddlFrequencySIP.SelectedValue = mforderVo.FrequencyCode;
                        txtstartDateSIP.Enabled = false;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSIP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSIP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSIP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSIP.SelectedDate = null;
                    }
                    else if (TransType == "SWB")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = true;

                    }
                    else if (TransType == "STB")
                    {
                        //sai  ShowTransactionType(2);
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        trScheme.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        else
                            ddlFrequencySTP.SelectedValue = "DA";
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    else if (TransType == "Sel")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                    }
                    else if (TransType == "SWP")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        else
                            ddlFrequencySTP.SelectedValue = "DA";
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    if (TransType == "CAF")
                    {
                        //sai  ShowTransactionType(3);
                        //lblAMC.Visible = false;//sai             ddlAMCList..Visible = false;
                        //lblCategory.Visible = false; ddlCategory.Visible = false;
                        //lblSearchScheme.Visible = false; //sai      ddlAmcSchemeList.Visible = false;
                        //lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                        //spnAMC.Visible = false; spnScheme.Visible = false;
                        if (customerVo != null)
                        {
                            lblGetLine1.Text = customerVo.Adr1Line1;
                            lblGetLine2.Text = customerVo.Adr1Line2;
                            lblGetline3.Text = customerVo.Adr1Line3;
                            lblgetCity.Text = customerVo.Adr1City;
                            lblGetstate.Text = customerVo.Adr1State;
                            lblGetPin.Text = customerVo.Adr1PinCode.ToString();
                            lblGetCountry.Text = customerVo.Adr1Country;
                        }
                        else
                        {
                            lblGetLine1.Text = "";
                            lblGetLine2.Text = "";
                            lblGetline3.Text = "";
                            lblgetCity.Text = "";
                            lblGetstate.Text = "";
                            lblGetPin.Text = "";
                            lblGetCountry.Text = "";
                        }

                    }
                    else
                    {
                        BindAMC(0);
                        //ddlAMCList.SelectedValue = mforderVo.Amccode.ToString();
                        BindCategory();
                        // ddlCategory.SelectedValue = mforderVo.category;
                        BindPortfolioDropdown(mforderVo.CustomerId);
                        //  ddlPortfolio.SelectedValue = mforderVo.portfolioId.ToString();
                        BindScheme(0);
                        //sai      ddlAmcSchemeList.SelectedItem.Value = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                        BindSchemeSwitch();
                        ddlSchemeSwitch.SelectedValue = mforderVo.SchemePlanSwitch.ToString();
                    }
                    portfolioId = mforderVo.portfolioId;
                    if (TransType == "SIP" || TransType == "BUY" || TransType == "CAF")
                    {
                        ////sai        BindFolioNumber(0);
                        //if (mforderVo.accountid != 0)
                        //ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {

                        if (mforderVo.accountid != 0)
                        {
                            //sai        BindFolioNumber(0);
                            //  ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        }
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    //   txtReceivedDate.Enabled = false;
                    if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    {
                        //txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
                    }
                    else
                        //    txtReceivedDate.SelectedDate = DateTime.Now;
                        //txtApplicationNumber.Enabled = false;
                        //txtApplicationNumber.Text = orderVo.ApplicationNumber;
                        //sai  txtOrderDate.SelectedDate = orderVo.OrderDate;
                        //sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();

                        //if (mforderVo.IsImmediate == 1)
                        //    rbtnImmediate.Checked = true;
                        //else
                        //{
                        //    rbtnFuture.Checked = true;
                        //    trfutureDate.Visible = true;
                        //}
                        //if (mforderVo.FutureExecutionDate != DateTime.MinValue)
                        //    //sai txtFutureDate.SelectedDate = mforderVo.FutureExecutionDate;
                        //else


                        //    //sai txtFutureDate.SelectedDate = null;
                        ////sai txtFutureTrigger.Text = mforderVo.FutureTriggerCondition;
                        txtAmount.Text = mforderVo.Amount.ToString();
                    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
                    {
                        if (mforderVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = mforderVo.Amount.ToString();
                        }
                        else
                        {
                            rbtAmount.Checked = false;
                            txtNewAmount.Text = "";
                        }

                        if (mforderVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = mforderVo.Units.ToString();
                        }
                        else
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = "";
                        }

                        lblAvailableAmount.Visible = false;
                        lblAvailableUnits.Visible = false;
                        //lblGetAvailableAmount.Text = operationVo.Amount.ToString();
                        //lblGetAvailableUnits.Text = operationVo.Units.ToString();
                    }

                    ddlPaymentMode.SelectedValue = orderVo.PaymentMode;
                    txtPaymentNumber.Text = orderVo.ChequeNumber;
                    if (orderVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = orderVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate = null;
                    BindBank(orderVo.CustomerId);
                    if (orderVo.CustBankAccId != 0)
                        ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();
                    else
                        ddlBankName.SelectedValue = "";
                    txtBranchName.Text = mforderVo.BranchName;
                    //lblGetAvailableAmount.Text = ;
                    //lblGetAvailableUnits.Text = "";

                    txtCorrAdrLine1.Text = mforderVo.AddrLine1;
                    txtCorrAdrLine2.Text = mforderVo.AddrLine2;
                    txtCorrAdrLine3.Text = mforderVo.AddrLine3;
                    if (mforderVo.LivingSince != DateTime.MinValue)
                        txtLivingSince.SelectedDate = mforderVo.LivingSince;
                    else
                        txtLivingSince.SelectedDate = null;
                    txtCorrAdrCity.Text = mforderVo.City;
                    ddlCorrAdrState.SelectedItem.Text = mforderVo.State;
                    txtCorrAdrPinCode.Text = mforderVo.Pincode;

                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedItem.Text = mforderVo.ARNNo;
                    else
                        ddlARNNo.SelectedIndex = 0;

                    Session["mforderVo"] = mforderVo;
                    Session["orderVo"] = orderVo;

                    //sai btnSubmit.Visible = false;
                    //sai btnUpdate.Visible = false;
                    //trReportButtons.Visible = true;
                    btnImgAddCustomer.Visible = false;
                    if (userType == "bm")
                    {
                        //sai lnkBtnEdit.Visible = false;
                    }
                    else
                    {
                        //sai lnkBtnEdit.Visible = true;
                    }
                    if (Request.QueryString["FromPage"] != null)
                    {
                        //sai lnkBtnEdit.Visible = false;
                        //sai  lnlBack.Visible = true;
                    }
                    //sai rgvOrderSteps.Visible = true;
                    //sai rgvOrderSteps.Enabled = true;
                    BindOrderStepsGrid();
                    //sai lnkDelete.Visible = false;
                    //btnViewReport.Visible = true;
                    //btnViewInPDF.Visible = true;
                    //btnViewInDOC.Visible = true;

                }
            }
        }


        private void SetControls(string action, MFOrderVo mforderVo, OrderVo orderVo)
        {
            if (action == "Entry")
            {
                if (mforderVo != null && orderVo != null)
                {
                    SetEditViewMode(false);
                    //ddlBranch.SelectedIndex = 0;
                    //ddlRM.SelectedIndex = 0;
                    txtCustomerName.Text = "";
                    //sai
                    //ddltransType.SelectedIndex = 0;
                    //txtReceivedDate.SelectedDate = null;
                    //txtApplicationNumber.Text = "";
                   //sai             ddlAMCList..SelectedIndex = 0;
                   // ddlCategory.SelectedIndex = 0;
                    //sai      ddlAmcSchemeList.SelectedIndex = 0;
                    //ddlPortfolio.SelectedIndex = -1;
                    //ddlFolioNumber.SelectedIndex = 0;
                  //  //sai  txtOrderDate.SelectedDate = null;
                   // //sai  lblGetOrderNo.Text = "";
                    //ddlOrderPendingReason.SelectedIndex = 0;
                    //ddlOrderStatus.SelectedIndex = 0;
                   // //sai txtFutureDate.SelectedDate = null;
                   // //sai txtFutureTrigger.Text = "";
                    txtAmount.Text = "";
                    ddlPaymentMode.SelectedIndex = 0;
                    txtPaymentNumber.Text = "";
                    txtPaymentInstDate.SelectedDate = null;
                    ddlBankName.SelectedIndex = 0;
                    txtBranchName.Text = "";
                    lblGetAvailableAmount.Text = "";
                    lblGetAvailableUnits.Text = "";
                    ddlSchemeSwitch.SelectedIndex = 0;
                    txtCorrAdrLine1.Text = "";
                    txtCorrAdrLine2.Text = "";
                    txtCorrAdrLine3.Text = "";
                    txtLivingSince.SelectedDate = null;
                    txtCorrAdrCity.Text = "";
                    ddlCorrAdrState.SelectedIndex = 0;
                    txtCorrAdrPinCode.Text = "";
                    ddlFrequencySIP.SelectedIndex = -1;
                    ddlFrequencySTP.SelectedIndex = -1;
                    txtstartDateSIP.SelectedDate = null;
                    txtendDateSIP.SelectedDate = null;
                    txtstartDateSTP.SelectedDate = null;
                    txtendDateSTP.SelectedDate = null;
                    txtNewAmount.Text = "";
                 //   ddlAssociate.SelectedIndex = 0;
                    ddlARNNo.SelectedIndex = 0;
                }
            }
            else if (action == "Edit")
            {
                if (mforderVo != null && orderVo != null)
                {
                    SetEditViewMode(false);
                    if (orderVo.AgentId != 0)
                    //    ddlAssociate.SelectedValue = orderVo.AgentId.ToString();
                    {
                        AgentId = customerBo.GetAgentId(advisorVo.advisorId, int.Parse(orderVo.AgentId.ToString()));
                        if (AgentId.Rows.Count > 0)
                        {
                            txtAssociateSearch.Text = AgentId.Rows[0][2].ToString();
                        }
                        else
                            txtAssociateSearch.Text = string.Empty;
                        Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                        if (Agentname.Rows.Count > 0)
                        {
                            lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                        }
                        lblAssociatetext.Text = string.Empty;

                    }
                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedValue = mforderVo.ARNNo;
                    orderId = orderVo.OrderId;
                    ddlsearch.Enabled = true;
                    txtAssociateSearch.Enabled = true;
                    txtCustomerName.Enabled = true;
                    txtCustomerName.Text = mforderVo.CustomerName;
                    if (orderVo.CustomerId != 0)
                        hdnCustomerId.Value = orderVo.CustomerId.ToString();
                    BindPortfolioDropdown(orderVo.CustomerId);
                    customerVo = customerBo.GetCustomer(orderVo.CustomerId);
                    lblGetBranch.Text = mforderVo.BMName;
                    lblGetRM.Text = mforderVo.RMName;
                    lblgetPan.Text = mforderVo.PanNo;
                    //TransType = mforderVo.TransactionCode;
                   // //sai  txtOrderDate.SelectedDate = orderVo.OrderDate;

                  //  //sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    hdnType.Value = mforderVo.TransactionCode;

                    if (TransType == "CAF")
                    {
                        //sai  ShowTransactionType(3);
                        //lblAMC.Visible = false;
                        ////ddlAMCList.Visible = false;
                        //lblCategory.Visible = false; ddlCategory.Visible = false;
                        //lblSearchScheme.Visible = false; //sai      ddlAmcSchemeList.Visible = false;
                        //lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                        //spnAMC.Visible = false; spnScheme.Visible = false;
                        if (customerVo != null)
                        {
                            lblGetLine1.Text = customerVo.Adr1Line1;
                            lblGetLine2.Text = customerVo.Adr1Line2;
                            lblGetline3.Text = customerVo.Adr1Line3;
                            lblgetCity.Text = customerVo.Adr1City;
                            lblGetstate.Text = customerVo.Adr1State;
                            lblGetPin.Text = customerVo.Adr1PinCode.ToString();
                            lblGetCountry.Text = customerVo.Adr1Country;
                        }
                        else
                        {
                            lblGetLine1.Text = "";
                            lblGetLine2.Text = "";
                            lblGetline3.Text = "";
                            lblgetCity.Text = "";
                            lblGetstate.Text = "";
                            lblGetPin.Text = "";
                            lblGetCountry.Text = "";
                        }
                    }
                    else
                    {
                        BindAMC(0);
                       //sai             ddlAMCList..SelectedValue = mforderVo.Amccode.ToString();
                        BindCategory();
                       // ddlCategory.SelectedValue = mforderVo.category;
                        BindPortfolioDropdown(orderVo.CustomerId);
                      //  ddlPortfolio.SelectedValue = mforderVo.portfolioId.ToString();
                        BindScheme(0);
                        //sai      ddlAmcSchemeList.SelectedItem.Value = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();

                    }
                    portfolioId = mforderVo.portfolioId;
                    if (TransType == "BUY" || TransType == "ABY")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (TransType == "SIP")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = true;
                        ddlFrequencySIP.SelectedValue = mforderVo.FrequencyCode;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSIP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSIP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSIP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSIP.SelectedDate = null;
                    }
                    else if (TransType == "SWB")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = true;

                    }
                    else if (TransType == "STB")
                    {
                        //sai  ShowTransactionType(2);
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        trScheme.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    else if (TransType == "Sel")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                    }
                    else if (TransType == "SWP")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    if (TransType == "SIP" || TransType == "BUY" || TransType == "CAF")
                    {
                        ////sai        BindFolioNumber(0);
                       // if (mforderVo.accountid != 0)
                            //ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {

                        if (mforderVo.accountid != 0)
                        {
                            //sai        BindFolioNumber(0);
                         //   ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        }
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                  //  txtReceivedDate.Enabled = false;
                    if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    {
                       // txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
                    }

                    else
                    //    txtReceivedDate.SelectedDate = DateTime.Now;
                    //txtApplicationNumber.Enabled = false;
                    //txtApplicationNumber.Text = orderVo.ApplicationNumber;
                    ////sai  txtOrderDate.SelectedDate = orderVo.OrderDate;
                    ////sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();

                    if (mforderVo.FutureExecutionDate != DateTime.MinValue)
                    {
                      //  //sai txtFutureDate.SelectedDate = mforderVo.FutureExecutionDate;
                    }
                    else
                    {
                      //  //sai txtFutureDate.SelectedDate = null;
                    }
                    if (mforderVo.IsImmediate == 1)
                    {
                        //  rbtnImmediate.Checked = true;
                    }
                    else
                    {
                        //  rbtnFuture.Checked = true;
                        // trfutureDate.Visible = true;
                    }
                  //  //sai txtFutureTrigger.Text = mforderVo.FutureTriggerCondition;
                    txtAmount.Text = mforderVo.Amount.ToString();
                    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
                    {
                        if (mforderVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = mforderVo.Amount.ToString();
                        }
                        else
                        {
                            rbtAmount.Checked = false;
                            txtNewAmount.Text = "";
                        }

                        if (mforderVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = mforderVo.Units.ToString();
                        }
                        else
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = "";
                        }

                        //lblGetAvailableAmount.Text = operationVo.Amount.ToString();
                        //lblGetAvailableUnits.Text = operationVo.Units.ToString();
                        lblAvailableAmount.Visible = false;
                        lblAvailableUnits.Visible = false;
                    }

                    ddlPaymentMode.SelectedValue = orderVo.PaymentMode;
                    txtPaymentNumber.Text = orderVo.ChequeNumber;
                    if (orderVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = orderVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate = null;
                    BindBank(orderVo.CustomerId);
                    if (orderVo.CustBankAccId != 0)
                        ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();
                    else
                        ddlBankName.SelectedValue = "Select";

                    txtBranchName.Text = mforderVo.BranchName;
                    txtCorrAdrLine1.Text = mforderVo.AddrLine1;
                    txtCorrAdrLine2.Text = mforderVo.AddrLine2;
                    txtCorrAdrLine3.Text = mforderVo.AddrLine3;
                    if (mforderVo.LivingSince != DateTime.MinValue)
                        txtLivingSince.SelectedDate = mforderVo.LivingSince;
                    else
                        txtLivingSince.SelectedDate = null;
                    //txtLivingSince.Text = operationVo.LivingSince.ToShortDateString();
                    txtCorrAdrCity.Text = mforderVo.City;
                    if (!string.IsNullOrEmpty(mforderVo.State.ToString().Trim()))
                        ddlCorrAdrState.SelectedItem.Text = mforderVo.State;
                    txtCorrAdrPinCode.Text = mforderVo.Pincode;

                  //  //sai btnSubmit.Visible = false;
                    ////sai btnUpdate.Visible = true;
                    //trReportButtons.Visible = true;
                    btnImgAddCustomer.Visible = false;
                    //btnAddMore.Visible = false;
                    ////sai rgvOrderSteps.Visible = true;
                    ////sai rgvOrderSteps.Enabled = true;
                    if (Request.QueryString["action"] != null)
                        orderId = orderVo.OrderId;
                    else
                        orderId = (int)Session["CO_OrderId"];
                    BindOrderStepsGrid();
                    //btnViewReport.Visible = true;
                    //btnViewInPDF.Visible = true;
                    //btnViewInDOC.Visible = true;
                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedItem.Text = mforderVo.ARNNo;
                    else
                        ddlARNNo.SelectedIndex = 0;
                    ////sai  lnlBack.Visible = true;
                    ////sai lnkDelete.Visible = true;
                }
            }

            else if (action == "View")
            {
                if (mforderVo != null && orderVo != null)
                {
                    ddlsearch.SelectedItem.Value = "0";
                    ddlsearch.Enabled = false;

                    SetEditViewMode(true);
                    orderId = orderVo.OrderId;
                    txtCustomerName.Enabled = false;
                    txtCustomerName.Text = mforderVo.CustomerName;
                    if (orderVo.CustomerId != 0)
                        hdnCustomerId.Value = orderVo.CustomerId.ToString();
                    BindPortfolioDropdown(orderVo.CustomerId);
                    txtAssociateSearch.Enabled = false;
                    customerVo = customerBo.GetCustomer(orderVo.CustomerId);
                    lblGetBranch.Text = mforderVo.BMName;
                    lblGetRM.Text = mforderVo.RMName;
                    lblgetPan.Text = mforderVo.PanNo;
                //  ddltransType.Enabled = false;
                    TransType = mforderVo.TransactionCode;
                   // //sai  txtOrderDate.SelectedDate = orderVo.OrderDate;
                   // //sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    hdnType.Value = mforderVo.TransactionCode;
                    if (orderVo.AgentId != 0)
                    //ddlAssociate.SelectedValue = orderVo.AgentId.ToString();
                    {
                        AgentId = customerBo.GetAgentId(advisorVo.advisorId, int.Parse(orderVo.AgentId.ToString()));
                        if (AgentId.Rows.Count > 0)
                        {
                            txtAssociateSearch.Text = AgentId.Rows[0][2].ToString();
                        }
                        Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                        if (Agentname.Rows.Count > 0)
                        {
                            lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                        }
                    }
                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedValue = mforderVo.ARNNo;

                    if (TransType == "BUY" || TransType == "ABY")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (TransType == "SIP")
                    {
                        //sai  ShowTransactionType(1);
                        trFrequency.Visible = true;
                        trSIPStartDate.Visible = true;
                        ddlFrequencySIP.Enabled = false;
                        ddlFrequencySIP.SelectedValue = mforderVo.FrequencyCode;
                        txtstartDateSIP.Enabled = false;
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSIP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSIP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSIP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSIP.SelectedDate = null;
                    }
                    else if (TransType == "SWB")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = true;

                    }
                    else if (TransType == "STB")
                    {
                        //sai  ShowTransactionType(2);
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        trScheme.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        else
                            ddlFrequencySTP.SelectedValue = "DA";
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    else if (TransType == "Sel")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                    }
                    else if (TransType == "SWP")
                    {
                        //sai  ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        if (mforderVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = mforderVo.FrequencyCode;
                        else
                            ddlFrequencySTP.SelectedValue = "DA";
                        if (mforderVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = mforderVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (mforderVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = mforderVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    if (TransType == "CAF")
                    {
                        //sai  ShowTransactionType(3);
                        //lblAMC.Visible = false;//sai             ddlAMCList..Visible = false;
                        //lblCategory.Visible = false; ddlCategory.Visible = false;
                        //lblSearchScheme.Visible = false; //sai      ddlAmcSchemeList.Visible = false;
                        //lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                        //spnAMC.Visible = false; spnScheme.Visible = false;
                        if (customerVo != null)
                        {
                            lblGetLine1.Text = customerVo.Adr1Line1;
                            lblGetLine2.Text = customerVo.Adr1Line2;
                            lblGetline3.Text = customerVo.Adr1Line3;
                            lblgetCity.Text = customerVo.Adr1City;
                            lblGetstate.Text = customerVo.Adr1State;
                            lblGetPin.Text = customerVo.Adr1PinCode.ToString();
                            lblGetCountry.Text = customerVo.Adr1Country;
                        }
                        else
                        {
                            lblGetLine1.Text = "";
                            lblGetLine2.Text = "";
                            lblGetline3.Text = "";
                            lblgetCity.Text = "";
                            lblGetstate.Text = "";
                            lblGetPin.Text = "";
                            lblGetCountry.Text = "";
                        }

                    }
                    else
                    {
                        BindAMC(0);
                        //ddlAMCList.SelectedValue = mforderVo.Amccode.ToString();
                        BindCategory();
                       // ddlCategory.SelectedValue = mforderVo.category;
                        BindPortfolioDropdown(mforderVo.CustomerId);
                      //  ddlPortfolio.SelectedValue = mforderVo.portfolioId.ToString();
                        BindScheme(0);
                        //sai      ddlAmcSchemeList.SelectedItem.Value = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                        BindSchemeSwitch();
                        ddlSchemeSwitch.SelectedValue = mforderVo.SchemePlanSwitch.ToString();
                    }
                    portfolioId = mforderVo.portfolioId;
                    if (TransType == "SIP" || TransType == "BUY" || TransType == "CAF")
                    {
                        ////sai        BindFolioNumber(0);
                        //if (mforderVo.accountid != 0)
                            //ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {

                        if (mforderVo.accountid != 0)
                        {
                            //sai        BindFolioNumber(0);
                          //  ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        }
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                 //   txtReceivedDate.Enabled = false;
                    if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    {
                        //txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
                    }
                    else
                    //    txtReceivedDate.SelectedDate = DateTime.Now;
                    //txtApplicationNumber.Enabled = false;
                    //txtApplicationNumber.Text = orderVo.ApplicationNumber;
                    //sai  txtOrderDate.SelectedDate = orderVo.OrderDate;
                    //sai  lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();

                    //if (mforderVo.IsImmediate == 1)
                    //    rbtnImmediate.Checked = true;
                    //else
                    //{
                    //    rbtnFuture.Checked = true;
                    //    trfutureDate.Visible = true;
                    //}
                    //if (mforderVo.FutureExecutionDate != DateTime.MinValue)
                    //    //sai txtFutureDate.SelectedDate = mforderVo.FutureExecutionDate;
                    //else
                     
 
                    //    //sai txtFutureDate.SelectedDate = null;
                    ////sai txtFutureTrigger.Text = mforderVo.FutureTriggerCondition;
                    txtAmount.Text = mforderVo.Amount.ToString();
                    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
                    {
                        if (mforderVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = mforderVo.Amount.ToString();
                        }
                        else
                        {
                            rbtAmount.Checked = false;
                            txtNewAmount.Text = "";
                        }

                        if (mforderVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = mforderVo.Units.ToString();
                        }
                        else
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = "";
                        }

                        lblAvailableAmount.Visible = false;
                        lblAvailableUnits.Visible = false;
                        //lblGetAvailableAmount.Text = operationVo.Amount.ToString();
                        //lblGetAvailableUnits.Text = operationVo.Units.ToString();
                    }

                    ddlPaymentMode.SelectedValue = orderVo.PaymentMode;
                    txtPaymentNumber.Text = orderVo.ChequeNumber;
                    if (orderVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = orderVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate = null;
                    BindBank(orderVo.CustomerId);
                    if (orderVo.CustBankAccId != 0)
                        ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();
                    else
                        ddlBankName.SelectedValue = "";
                    txtBranchName.Text = mforderVo.BranchName;
                    //lblGetAvailableAmount.Text = ;
                    //lblGetAvailableUnits.Text = "";

                    txtCorrAdrLine1.Text = mforderVo.AddrLine1;
                    txtCorrAdrLine2.Text = mforderVo.AddrLine2;
                    txtCorrAdrLine3.Text = mforderVo.AddrLine3;
                    if (mforderVo.LivingSince != DateTime.MinValue)
                        txtLivingSince.SelectedDate = mforderVo.LivingSince;
                    else
                        txtLivingSince.SelectedDate = null;
                    txtCorrAdrCity.Text = mforderVo.City;
                    ddlCorrAdrState.SelectedItem.Text = mforderVo.State;
                    txtCorrAdrPinCode.Text = mforderVo.Pincode;

                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedItem.Text = mforderVo.ARNNo;
                    else
                        ddlARNNo.SelectedIndex = 0;

                    Session["mforderVo"] = mforderVo;
                    Session["orderVo"] = orderVo;

                    //sai btnSubmit.Visible = false;
                    //sai btnUpdate.Visible = false;
                    //trReportButtons.Visible = true;
                    btnImgAddCustomer.Visible = false;
                    if (userType == "bm")
                    {
                        //sai lnkBtnEdit.Visible = false;
                    }
                    else
                    {
                        //sai lnkBtnEdit.Visible = true;
                    }
                    if (Request.QueryString["FromPage"] != null)
                    {
                        //sai lnkBtnEdit.Visible = false;
                        //sai  lnlBack.Visible = true;
                    }
                    //sai rgvOrderSteps.Visible = true;
                    //sai rgvOrderSteps.Enabled = true;
                    BindOrderStepsGrid();
                    //sai lnkDelete.Visible = false;
                    //btnViewReport.Visible = true;
                    //btnViewInPDF.Visible = true;
                    //btnViewInDOC.Visible = true;

                }
            }
        }
        //private void BindOrderStatus()
        //{
        //    DataSet dsOrderStaus;
        //    DataTable dtOrderStatus;
        //    dsOrderStaus = operationBo.GetOrderStatus();
        //    dtOrderStatus = dsOrderStaus.Tables[0];
        //    if (dtOrderStatus.Rows.Count > 0)
        //    {
        //        ddlOrderStatus.DataSource = dtOrderStatus;
        //        ddlOrderStatus.DataValueField = dtOrderStatus.Columns["XS_StatusCode"].ToString();
        //        ddlOrderStatus.DataTextField = dtOrderStatus.Columns["XS_Status"].ToString();
        //        ddlOrderStatus.DataBind();
        //    }

        //}
        private void BindCategory()
        {
            try
            {
                //DataSet dsSchemeCategory;
                //DataTable dtSchemeCategory;
                //dsSchemeCategory = priceBo.GetNavOverAllCategoryList();
                ////----------------------------------------------Scheme Category Binding------------------------
                //if (dsSchemeCategory.Tables.Count > 0)
                //{
                //    dtSchemeCategory = dsSchemeCategory.Tables[0];
                //    ddlCategory.DataSource = dtSchemeCategory;
                //    ddlCategory.DataValueField = dtSchemeCategory.Columns["Category_Code"].ToString();
                //    ddlCategory.DataTextField = dtSchemeCategory.Columns["Category_Name"].ToString();
                //    ddlCategory.DataBind();

                //}
                //ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        private void BindState()
        {
            DataTable dtStates = new DataTable();
            dtStates = XMLBo.GetStates(path);
            ddlCorrAdrState.DataSource = dtStates;
            ddlCorrAdrState.DataTextField = "StateName";
            ddlCorrAdrState.DataValueField = "StateCode";
            ddlCorrAdrState.DataBind();
            ddlCorrAdrState.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindFrequency()
        {
            //sai
            //dtFrequency = assetBo.GetFrequencyCode(path);
            //ddlFrequencySIP.DataSource = dtFrequency;
            //ddlFrequencySIP.DataTextField = "Frequency";
            //ddlFrequencySIP.DataValueField = "FrequencyCode";
            //ddlFrequencySIP.DataBind();
            ////---------------------------------------------
            //ddlFrequencySTP.DataSource = dtFrequency;
            //ddlFrequencySTP.DataTextField = "Frequency";
            //ddlFrequencySTP.DataValueField = "FrequencyCode";
            //ddlFrequencySTP.DataBind();

        }
        public void ISA_Onclick(object obj, EventArgs e)
        {
           // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerISARequest','');", true);
        }

        private void ShowHideFields(int flag)
        {
            //if (flag == 0)
            //{
            //    trTransactionType.Visible = false;
            //    trARDate.Visible = false;
            //    trAplNumber.Visible = false;
            //    trOrderDate.Visible = false;
            //    trOrderNo.Visible = false;
            //    trOrderType.Visible = false;
            //    //trrejectReason.Visible = false;
            //    trfutureDate.Visible = false;
            //    //sai
            //    ////sai rgvOrderSteps.Visible = false;
            //    ////sai lnkBtnEdit.Visible = false;
            //    ////sai  lnlBack.Visible = false;
            //    //sai btnUpdate.Visible = false;
            //    //sai
            //    //sai lnkDelete.Visible = false;
            //    btnViewReport.Visible = false;
            //    btnViewInPDF.Visible = false;
            //    btnViewInDOC.Visible = false;
            //    ////sai  ShowTransactionType(0);
            //}
            //else if (flag == 1)
            //{
            //    //sai
            //    //trTransactionType.Visible = true;
            //    //trARDate.Visible = true;
            //    //trAplNumber.Visible = true;
            //    //trOrderDate.Visible = true;
            //    //trOrderNo.Visible = true;
            //    //trOrderType.Visible = true;
            //    //trrejectReason.Visible = false;
            //   //sai trfutureDate.Visible = false;
            //    //sai
            //    ////sai rgvOrderSteps.Visible = false;
            //    ////sai lnkBtnEdit.Visible = false;
            //    ////sai  lnlBack.Visible = false;
            //   //sai //sai btnUpdate.Visible = false;
            ////sai  
            //    ////sai lnkDelete.Visible = false;
            //    //btnViewReport.Visible = false;
            //    //btnViewInPDF.Visible = false;
            //    //btnViewInDOC.Visible = false;
            //}
        }

        protected void   ShowTransactionType(int type)
        {
            if (type == 0)
            {
                trAmount.Visible = false;
                trPINo.Visible = false;
                trBankName.Visible = false;
                trFrequency.Visible = false;
                trSIPStartDate.Visible = false;
                 trAddress6.Visible = false;
                 trSection1.Visible = false;
                 trSection2.Visible = false;

                trGetAmount.Visible = false;
                trRedeemed.Visible = false;
                trScheme.Visible = false;
                trFrequencySTP.Visible = false;
                trSTPStart.Visible = false;

                trSection3.Visible = false;

                trAddress1.Visible = false;
                 trOldLine1.Visible = false;
             
                trOldLine3.Visible = false;
                trOldCity.Visible = false;
                trOldPin.Visible = false;
                trAddress6.Visible = false;
                trNewLine1.Visible = false;
                trNewLine3.Visible = false;
                trNewCity.Visible = false;
                trNewPin.Visible = false;

                trBtnSubmit.Visible = false;
            }
            else if (type == 1)
            {
                trSection1.Visible = true;
                trAmount.Visible = true;
                trPINo.Visible = true;
                trBankName.Visible = true;
                trFrequency.Visible = true;
                trSIPStartDate.Visible = true;
              trAddress6.Visible = true;

                trSection2.Visible = false;

                trGetAmount.Visible = false;
                trRedeemed.Visible = false;
                trScheme.Visible = false;
                trFrequencySTP.Visible = false;
                trSTPStart.Visible = false;

                trSection3.Visible = false;

                trAddress1.Visible = false;
                trOldLine1.Visible = false;
                trOldLine3.Visible = false;
                trOldCity.Visible = false;
                trOldPin.Visible = false;
                trAddress6.Visible = false;
                trNewLine1.Visible = false;
                trNewLine3.Visible = false;
                trNewCity.Visible = false;
                trNewPin.Visible = false;

                trBtnSubmit.Visible = true;

            }
            else if (type == 2)
            {

                trSection1.Visible = true;
                trAmount.Visible = false;
                trPINo.Visible = false;
                trBankName.Visible = false;
                trFrequency.Visible = false;
                trSIPStartDate.Visible = false;
               trAddress6.Visible = false;

                trSection2.Visible = false;

                trGetAmount.Visible = true;
                trRedeemed.Visible = true;
                trScheme.Visible = true;
                trFrequencySTP.Visible = true;
                trSTPStart.Visible = true;

                trSection3.Visible = false;

                trAddress1.Visible = false;
                trOldLine1.Visible = false;
                trOldLine3.Visible = false;
                trOldCity.Visible = false;
                trOldPin.Visible = false;
                trAddress6.Visible = false;
                trNewLine1.Visible = false;
                trNewLine3.Visible = false;
                trNewCity.Visible = false;
                trNewPin.Visible = false;

                trBtnSubmit.Visible = true;

            }
            if (type == 3)
            {
                trSection1.Visible = true ;
                trAmount.Visible = false;
                trPINo.Visible = false;
                trBankName.Visible = false;
                trFrequency.Visible = false;
                trSIPStartDate.Visible = false;
               trAddress6.Visible = false;

                trSection2.Visible = false;

                trGetAmount.Visible = false;
                trRedeemed.Visible = false;
                trScheme.Visible = false;
                trFrequencySTP.Visible = false;
                trSTPStart.Visible = false;

               trSection3.Visible = false;

               trAddress1.Visible = true;
               trOldLine1.Visible = true;
               trOldLine3.Visible = true;
               trOldCity.Visible = true;
               trOldPin.Visible = true;
               trAddress6.Visible = true;
               trNewLine1.Visible = true;
               trNewLine3.Visible = true;
               trNewCity.Visible = true;
               trNewPin.Visible = true;

               trBtnSubmit.Visible = true;
            }

        }
        protected void ddlCustomerISAAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable GetHoldersName = new DataTable();
            if (ddlCustomerISAAccount.SelectedItem.Value != "Select")
            {
                GetHoldersName = customerBo.GetholdersName(int.Parse(ddlCustomerISAAccount.SelectedItem.Value.ToString()));
                if (GetHoldersName.Rows.Count > 0)
                {
                    gvJointHoldersList.DataSource = GetHoldersName;
                    gvJointHoldersList.DataBind();
                    gvJointHoldersList.Visible = true;
              //  pnlJointholders.Visible = true;
                }
                else
                {
                    gvJointHoldersList.Visible = false;
                }
            }
            else
            {
                gvJointHoldersList.Visible = false;
            }
        }
        private void BindISAList()
        {
            //DataTable ISAList;
            //if (!string.IsNullOrEmpty(txtCustomerId.Value))
            //{
            //    ISAList = customerBo.GetISaList(customerId);
            //    DataTable ISANewList = new DataTable();
            //    int i;

            //    //ISANewList.Rows.Count = ISAList.Rows.Count;
            //    ISANewList.Columns.Add("CISAA_accountid");
            //    ISANewList.Columns.Add("CISAA_AccountNumber");

            //    for (i = 0; i <= ISAList.Rows.Count; i++)
            //    {

            //    }
            //    if (ISAList.Rows.Count > 0)
            //    {
            //        //pnlJointholders.Visible = true;
            //        //trJointHoldersList.Visible = true;
            //        ddlCustomerISAAccount.DataSource = ISAList;
            //        ddlCustomerISAAccount.DataValueField = ISAList.Columns["CISAA_accountid"].ToString();
            //        ddlCustomerISAAccount.DataTextField = ISAList.Columns["CISAA_AccountNumber"].ToString();
            //        ddlCustomerISAAccount.DataBind();
            //        ddlCustomerISAAccount.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
            //        //trRegretMsg.Visible = false;

            //        ddlCustomerISAAccount.Visible = true;
            //    }
            //    else
            //    {
            //        // trJointHoldersList.Visible = false;
            //        //pnlJointholders.Visible = false;
            //        ddlCustomerISAAccount.Visible = true;
            //        //trRegretMsg.Visible = true;

            //        ddlCustomerISAAccount.Items.Clear();
            //        ddlCustomerISAAccount.DataSource = null;
            //        ddlCustomerISAAccount.DataBind();
            //        ddlCustomerISAAccount.Items.Insert(0, new ListItem("Select", "Select"));
            //    }

            //}

        }
        protected void OnAssociateTextchanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAssociateSearch.Text))
            {

                Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                if (Agentname.Rows.Count > 0)
                {
                    lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                }
                else
                    lblAssociatetext.Text = "";
            }

        }

        protected void hdnTransType_ValueChanged1(object sender, EventArgs e)
        {
            if (Session["MFTranstype"]  == null)
            {
                ShowTransactionType(0);
                return;
            }
         

            string TransType = Session["MFTranstype"] .ToString();

            if (!string.IsNullOrEmpty(TransType.Trim()))
            {
                if (TransType == "BUY" || TransType == "ABY" || TransType == "SIP")
                {

                    ShowTransactionType(1);
                    if (TransType == "BUY")
                    {
                        //    BindAMC(0);
                        //    BindScheme(0);
                        //    ////sai        BindFolioNumber(0);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (TransType == "SIP")
                    {
                        //    BindAMC(0);
                        //    BindScheme(0);
                        //    ////sai        BindFolioNumber(0);
                        trFrequency.Visible = true;
                        trSIPStartDate.Visible = true;
                    }
                    else
                    {
                        //    BindAMC(1);
                        //    BindScheme(1);
                        //    ////sai        BindFolioNumber(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                }
                else if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
                {
                    ShowTransactionType(2);
                    if (TransType == "SWB")
                    {
                        //    //BindSchemeSwitch();
                           trScheme.Visible = true;
                         trFrequencySTP.Visible = false;
                       trSTPStart.Visible = false;
                        }
                        else if (TransType == "STB")
                        {
                          trScheme.Visible = true;
                         trFrequencySTP.Visible = true;
                          trSTPStart.Visible = true;
                        }
                        else if (TransType == "SWP")
                        {
                       trScheme.Visible = false;
                         trFrequencySTP.Visible = true;
                       trSTPStart.Visible = true;
                        }
                        else
                        {
                         trFrequencySTP.Visible = false;
                       trSTPStart.Visible = false;
                       trScheme.Visible = false;

                        }
                        //BindAMC(1);
                        //BindScheme(1);
                        ////sai        BindFolioNumber(1);

                    }
                    else if (TransType == "CAF")
                    {
                        ShowTransactionType(3);
                        if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
                        {
                            customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                            Session["customerVo"] = customerVo;
                            if (customerVo != null)
                            {
                                lblGetLine1.Text = customerVo.Adr1Line1;
                                lblGetLine2.Text = customerVo.Adr1Line2;
                                lblGetline3.Text = customerVo.Adr1Line3;
                                lblgetCity.Text = customerVo.Adr1City;
                                lblGetstate.Text = customerVo.Adr1State;
                                lblGetPin.Text = customerVo.Adr1PinCode.ToString();
                                lblGetCountry.Text = customerVo.Adr1Country;
                            }
                            else
                            {
                                lblGetLine1.Text = "";
                                lblGetLine2.Text = "";
                                lblGetline3.Text = "";
                                lblgetCity.Text = "";
                                lblGetstate.Text = "";
                                lblGetPin.Text = "";
                                lblGetCountry.Text = "";
                            }

                        }
                        //lblAMC.Visible = false;//sai             ddlAMCList..Visible = false;
                        //lblCategory.Visible = false; ddlCategory.Visible = false;
                        //lblSearchScheme.Visible = false; //sai      ddlAmcSchemeList.Visible = false;
                        //lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                        //spnAMC.Visible = false; spnScheme.Visible = false;
                        //CompareValidator1.Visible = false; CompareValidator2.Visible = false;
                    }

                }
            }
        

        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {
            

            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                Session["customerid"] = txtCustomerId.Value.ToString();
                //trJointHoldersList.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                //sai//sai             ddlAMCList..Enabled = true;
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                lblGetBranch.Text = customerVo.BranchName;
                lblGetRM.Text = customerVo.RMName;
                lblgetPan.Text = customerVo.PANNum;
                hdnCustomerId.Value = txtCustomerId.Value;
                customerId = int.Parse(txtCustomerId.Value);
                if (ddlsearch.SelectedItem.Value == "2")
                    lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;

                //= rmVo.FirstName + ' ' + rmVo.MiddleName + ' ' + rmVo.LastName;
                BindBank(customerId);
                BindDepositedBank(customerId);
                BindPortfolioDropdown(customerId);
                //sai ddltransType.SelectedIndex = 0;
             //sai   BindISAList();
               
               //sai btnreport.Visible = true;
                //sai  btnpdfReport.Visible = true;
                //    if (ISAList.Rows.Count!=0)
                //    {
                //        string formatstring = "";

                //        foreach (DataRow dRow in ISAList.Rows)
                //        {
                //            if (!string.IsNullOrEmpty(formatstring))
                //            {
                //                formatstring = formatstring + "," + dRow[0];
                //            }
                //            else
                //            {
                //                formatstring = dRow[0].ToString();
                //            }
                //        }
                //        lblIsaNo.Text = formatstring;
                //        trRegretMsg.Visible = false;
                //        BtnIsa.Visible = false;
                //    }

                //    else
                //    {
                //        lblIsaNo.Text = null;
                //        trRegretMsg.Visible = true;
                //        BtnIsa.Visible = true;
                //    }
              //sai  ClearAllFields();

            }
        }

        private void BindBank(int customerId)
        {
            DataSet dsBankName = mfOrderBo.GetCustomerBank(customerId);
            if (dsBankName.Tables[0].Rows.Count > 0)
            {
                ddlBankName.DataSource = dsBankName;
                ddlBankName.DataValueField = dsBankName.Tables[0].Columns["CB_CustBankAccId"].ToString();
                ddlBankName.DataTextField = dsBankName.Tables[0].Columns["WERPBM_BankName"].ToString();
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlBankName.Items.Clear();
                ddlBankName.DataSource = null;
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        private void BindDepositedBank(int customerId)
        {
            DataSet dsBankName = mfOrderBo.GetCustomerBank(customerId);
            if (dsBankName.Tables[0].Rows.Count > 0)
            {
                ddlDepoBank.DataSource = dsBankName;
                ddlDepoBank.DataValueField = dsBankName.Tables[0].Columns["CB_CustBankAccId"].ToString();
                ddlDepoBank.DataTextField = dsBankName.Tables[0].Columns["WERPBM_BankName"].ToString();
                ddlDepoBank.DataBind();
                ddlDepoBank.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlDepoBank.Items.Clear();
                ddlDepoBank.DataSource = null;
                ddlDepoBank.DataBind();
                ddlDepoBank.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        private void BindPortfolioDropdown(int customerId)
        {
            //DataSet ds = portfolioBo.GetCustomerPortfolio(customerId);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlPortfolio.DataSource = ds;
            //    ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            //    ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            //    ddlPortfolio.DataBind();
            //    hdnPortfolioId.Value = ddlPortfolio.SelectedValue;
            //}
        }
        public void clearPancustomerDetails()
        {
            lblgetPan.Text = "";
            txtCustomerName.Text = "";
            txtPansearch.Text = "";
            lblgetcust.Text = "";
            //            txtCustomerId_ValueChanged1( );
        }
        private void ClearAllFields()
        {
            //sai

          //  ddltransType.SelectedIndex = 0;
          //  txtReceivedDate.SelectedDate = null;
          //  txtApplicationNumber.Text = "";
          //  BindAMC(0);
          // //sai             ddlAMCList..SelectedIndex = 0;
          //  BindCategory();
          //  ddlCategory.SelectedIndex = 0;
          //  BindScheme(0);
          //  //sai      ddlAmcSchemeList.SelectedIndex = 0;
          //  //ddlPortfolio.SelectedIndex = 0;
          //  //sai        BindFolioNumber(0);
          //  ddlFolioNumber.SelectedIndex = -1;
          //  //sai txtFutureDate.SelectedDate = null;
          //  //sai txtFutureTrigger.Text = "";
          //  txtAmount.Text = "";
          //  ddlPaymentMode.SelectedIndex = -1;
          //  txtPaymentNumber.Text = "";
          //  txtPaymentInstDate.SelectedDate = null;
          //  ddlBankName.SelectedIndex = -1;
          //  txtBranchName.Text = "";
          //  lblGetAvailableAmount.Text = "";
          //  lblGetAvailableUnits.Text = "";
          //  ddlSchemeSwitch.SelectedIndex = -1;
          ////sai 
          //  //txtCorrAdrLine1.Text = "";
          //  //txtCorrAdrLine2.Text = "";
          //  //txtCorrAdrLine3.Text = "";
          //  //txtLivingSince.SelectedDate = null;
          //  //txtCorrAdrCity.Text = "";
          //  //ddlCorrAdrState.SelectedIndex = -1;
          //  //txtCorrAdrPinCode.Text = "";
          //  ddlFrequencySIP.SelectedIndex = -1;
          //  ddlFrequencySTP.SelectedIndex = -1;
          //  txtstartDateSIP.SelectedDate = null;
          //  txtendDateSIP.SelectedDate = null;
          //  txtstartDateSTP.SelectedDate = null;
          //  txtendDateSTP.SelectedDate = null;
          //  //sai
          //  //lblGetLine1.Text = "";
          //  //lblGetLine2.Text = "";
          //  //lblGetline3.Text = "";
          //  //lblGetLivingSince.Text = "";
          //  //lblgetCity.Text = "";
          //  //lblGetstate.Text = "";
          //  //lblGetPin.Text = "";
          //  //lblGetCountry.Text = "";

          //  txtNewAmount.Text = "";

        }

        private void BindAMC(int Aflag)
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;

            try
            {
                //if (Aflag == 0)
                //    dsProductAmc = productMFBo.GetProductAmc();
                //else
                //    dsProductAmc = operationBo.GetAMCForOrderEntry(Aflag, int.Parse(hdnCustomerId.Value));

                //if (dsProductAmc.Tables.Count > 0)
                //{
                //    dtProductAMC = dsProductAmc.Tables[0];
                //   //sai             ddlAMCList..DataSource = dtProductAMC;
                //   //sai             ddlAMCList..DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                //   //sai             ddlAMCList..DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                //   //sai             ddlAMCList..DataBind();
                //   //sai             ddlAMCList..Items.Insert(0, new ListItem("Select", "Select"));
                //}
                //else
                //{
                //   //sai             ddlAMCList..Items.Clear();
                //   //sai             ddlAMCList..DataSource = null;
                //   //sai             ddlAMCList..DataBind();
                //   //sai             ddlAMCList..Items.Insert(0, new ListItem("Select", "Select"));
                //}

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        private void BindScheme(int Sflag)
        {
            try
            {
                DataSet dsScheme = new DataSet();
                DataTable dtScheme;

                //if (ddlAMCList.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
                //{
                //    amcCode = int.Parse(ddlAMCList.SelectedValue.ToString());
                //    categoryCode = ddlCategory.SelectedValue;
                //    if (txtCustomerId.Value == "")
                //        dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 1, 1);
                //    else
                //        dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));
                //}
                //else if (ddlAMCList.SelectedIndex != 0)
                //{
                //    amcCode = int.Parse(ddlAMCList.SelectedValue.ToString());
                //    categoryCode = ddlCategory.SelectedValue;
                //    if (Sflag == 0)
                //        dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 0, 1);
                //    else
                //        dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));
                //}
                //if (dsScheme.Tables.Count > 0)
                //{
                //    dtScheme = dsScheme.Tables[0];
                //    //sai      ddlAmcSchemeList.DataSource = dtScheme;
                //    //sai      ddlAmcSchemeList.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                //    //sai      ddlAmcSchemeList.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                //    //sai      ddlAmcSchemeList.DataBind();
                //    //sai      ddlAmcSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
                //}
                //else
                //{
                //    //sai      ddlAmcSchemeList.Items.Clear();
                //    //sai      ddlAmcSchemeList.DataSource = null;
                //    //sai      ddlAmcSchemeList.DataBind();
                //    //sai      ddlAmcSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        //private void //sai        BindFolioNumber(int Fflag)
        //{
        //    DataSet dsgetfolioNo = new DataSet();
        //    DataTable dtgetfolioNo;
        //    try
        //    {
        //        if (ddlAMCList.SelectedIndex != 0 && //sai      ddlAmcSchemeList.SelectedIndex != 0)
        //        {
        //            amcCode = int.Parse(ddlAMCList.SelectedValue);
        //            schemePlanCode = int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
        //            if (txtCustomerId.Value == "")
        //                dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 1);
        //            else
        //                if (ddlCustomerISAAccount.SelectedItem.Value != "Select")
        //                    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), int.Parse(ddlCustomerISAAccount.SelectedItem.Value));
        //                else
        //                    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), 0);
        //        }
        //        else
        //        {
        //            if (txtCustomerId.Value == "")
        //                dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 0);
        //            else
        //                if (ddlCustomerISAAccount.SelectedItem.Value != "Select")
        //                    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), int.Parse(ddlCustomerISAAccount.SelectedItem.Value));
        //                else
        //                    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), 0);
        //        }

        //        if (dsgetfolioNo.Tables[0].Rows.Count > 0)
        //        {
        //            dtgetfolioNo = dsgetfolioNo.Tables[0];
        //            ddlFolioNumber.DataSource = dtgetfolioNo;
        //            ddlFolioNumber.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
        //            ddlFolioNumber.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
        //            ddlFolioNumber.DataBind();
        //            hdnAccountId.Value = ddlFolioNumber.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            ddlFolioNumber.Items.Clear();
        //            ddlFolioNumber.DataSource = null;
        //            ddlFolioNumber.DataBind();
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw (Ex);
        //    }
        //}

        //protected void ddltransType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblAMC.Visible = true;//sai             ddlAMCList..Visible = true;
        //    lblCategory.Visible = true; ddlCategory.Visible = true;
        //    lblSearchScheme.Visible = true; //sai      ddlAmcSchemeList.Visible = true;
        //    lblFolioNumber.Visible = true; ddlFolioNumber.Visible = true;
        //    spnAMC.Visible = true; spnScheme.Visible = true;
        //    CompareValidator1.Visible = true; CompareValidator2.Visible = true;

        //    if ((string.IsNullOrEmpty(txtPansearch.Text) && string.IsNullOrEmpty(txtCustomerName.Text)))
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select a customer');", true);
        //    else
        //    {
        //        hdnType.Value = TransType.ToString();
        //        if (TransType == "BUY" || TransType == "ABY" || TransType == "SIP")
        //        {

        //            ShowTransactionType(1);
        //            if (TransType == "BUY")
        //            {
        //                BindAMC(0);
        //                BindScheme(0);
        //                ////sai        BindFolioNumber(0);
        //                trFrequency.Visible = false;
        //                trSIPStartDate.Visible = false;
        //            }
        //            else if (TransType == "SIP")
        //            {
        //                BindAMC(0);
        //                BindScheme(0);
        //                ////sai        BindFolioNumber(0);
        //                trFrequency.Visible = true;
        //                trSIPStartDate.Visible = true;
        //            }
        //            else
        //            {
        //                BindAMC(1);
        //                BindScheme(1);
        //                ////sai        BindFolioNumber(1);
        //                trFrequency.Visible = false;
        //                trSIPStartDate.Visible = false;
        //            }
        //        }
        //        else if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
        //        {
        //            ShowTransactionType(2);
        //            if (TransType == "SWB")
        //            {
        //                //BindSchemeSwitch();
        //                trScheme.Visible = true;
        //                trFrequencySTP.Visible = false;
        //                trSTPStart.Visible = false;
        //            }
        //            else if (TransType == "STB")
        //            {
        //                trScheme.Visible = true;
        //                trFrequencySTP.Visible = true;
        //                trSTPStart.Visible = true;
        //            }
        //            else if (TransType == "SWP")
        //            {
        //                trScheme.Visible = false;
        //                trFrequencySTP.Visible = true;
        //                trSTPStart.Visible = true;
        //            }
        //            else
        //            {
        //                trFrequencySTP.Visible = false;
        //                trSTPStart.Visible = false;
        //                trScheme.Visible = false;

        //            }
        //            BindAMC(1);
        //            BindScheme(1);
        //            ////sai        BindFolioNumber(1);

        //        }
        //        else if (TransType == "CAF")
        //        {
        //            ShowTransactionType(3);
        //            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
        //            {
        //                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
        //                Session["customerVo"] = customerVo;
        //                if (customerVo != null)
        //                {
        //                    lblGetLine1.Text = customerVo.Adr1Line1;
        //                    lblGetLine2.Text = customerVo.Adr1Line2;
        //                    lblGetline3.Text = customerVo.Adr1Line3;
        //                    lblgetCity.Text = customerVo.Adr1City;
        //                    lblGetstate.Text = customerVo.Adr1State;
        //                    lblGetPin.Text = customerVo.Adr1PinCode.ToString();
        //                    lblGetCountry.Text = customerVo.Adr1Country;
        //                }
        //                else
        //                {
        //                    lblGetLine1.Text = "";
        //                    lblGetLine2.Text = "";
        //                    lblGetline3.Text = "";
        //                    lblgetCity.Text = "";
        //                    lblGetstate.Text = "";
        //                    lblGetPin.Text = "";
        //                    lblGetCountry.Text = "";
        //                }

        //            }
        //            lblAMC.Visible = false;//sai             ddlAMCList..Visible = false;
        //            lblCategory.Visible = false; ddlCategory.Visible = false;
        //            lblSearchScheme.Visible = false; //sai      ddlAmcSchemeList.Visible = false;
        //            lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
        //            spnAMC.Visible = false; spnScheme.Visible = false;
        //            CompareValidator1.Visible = false; CompareValidator2.Visible = false;
        //        }


        //        //sai btnSubmit.Visible = true;
        //    }
        //    ////sai  btnViewInPDFNew.Visible = true;
        //    ////sai  btnViewInDOCNew.Visible = true;
        //}



        //protected void btnAddCustomer_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('CustomerType','page=Entry');", true);
        //}

        //public void BindOrderStepsGrid()
        //{
        //    //sai
        //    DataSet dsOrderSteps = new DataSet();
        //    DataTable dtOrderDetails;
        //    //SetControls(false);
        //    dsOrderSteps = orderbo.GetOrderStepsDetails(orderId);
        //    dtOrderDetails = dsOrderSteps.Tables[0];
        //    //if (dtOrderDetails.Rows.Count == 0)
        //    //{
        //    //    lblPickNominee.Text = "You have not placed any Order";
        //    //}
        //    //else
        //    //{
        //    //lblPickNominee.Text = "Order Steps";
        //    //sai rgvOrderSteps.DataSource = dtOrderDetails;
        //    //sai rgvOrderSteps.DataBind();
        //    Session["OrderDetails"] = dtOrderDetails;
        //    //}
        //}

        protected void  rgvOrderSteps_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

                //comboOrderStatus.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(comboOrderStatus_SelectedIndexChanged);
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
            BindRadComboBoxPendingReason(rcPendingReason, statusOrderCode);
        }

        protected void  rgvOrderSteps_ItemDataBound(object sender, GridItemEventArgs e)
        {
            DataTable dt = (DataTable)Session["OrderDetails"];
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                //((Literal)dataItem["DropDownColumnStatus"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();
                //((Literal)dataItem["DropDownColumnStatusReason"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();

                TemplateColumn tm = new TemplateColumn();
                Label lblStatusCode = new Label();
                Label lblOrderStep = new Label();
                LinkButton editButton = dataItem["EditCommandColumn"].Controls[0] as LinkButton;
                Label lblOrderStatus = new Label();
                Label lblOrderStatusReason = new Label();
                lblStatusCode = (Label)e.Item.FindControl("lblStatusCode");
                lblOrderStep = (Label)e.Item.FindControl("lblOrderStepCode");
                lblOrderStatus = (Label)e.Item.FindControl("lblOrderStatus");
                lblOrderStatusReason = (Label)e.Item.FindControl("lblOrderStatusReason");
                Label lblCMFOS_Date = (Label)e.Item.FindControl("lblCMFOS_Date");
                if (lblOrderStep.Text.Trim() == "IP")
                {
                    if (lblStatusCode.Text == "OMIP")
                    {
                        editButton.Text = "Mark as Pending";
                      //  result = mfOrderBo.MFOrderAutoMatch(orderVo.OrderId, mforderVo.SchemePlanCode, mforderVo.accountid, mforderVo.TransactionCode, orderVo.CustomerId, mforderVo.Amount, orderVo.OrderDate);
                        if (result == true)
                        {
                            editButton.Text = "";
                            lblOrderStatusReason.Text = "";
                        }

                    }

                    else if (lblStatusCode.Text == "OMPD")
                    {
                        editButton.Text = "Mark as InProcess";
                    }

                }
                else if (lblOrderStep.Text.Trim() == "PR")
                {
                    if (result == true)
                    {
                        lblOrderStatus.Text = "Executed";
                        lblOrderStatusReason.Text = "Order Confirmed";
                    }
                    else
                    {
                        lblOrderStatus.Text = "";
                        lblOrderStatusReason.Text = "";
                        lblCMFOS_Date.Text = "";
                    }
                    editButton.Text = "";
                }
                else
                {
                    lblOrderStatusReason.Text = "";
                    editButton.Text = "";
                }

                //string editColumn = dataItem["COS_IsEditable"].Text;
                //if (editColumn == "1")
                //{
                //    editButton.Enabled = true;
                //}
                //else
                //{
                //    editButton.Enabled = false;
                //}
            }
        }

        

       

        //protected void rcStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    RadComboBox rcStatus = (RadComboBox)o;
        //    GridEditableItem editedItem = rcStatus.NamingContainer as GridEditableItem;
        //    RadComboBox rcPendingReason = editedItem.FindControl("rcbPendingReason") as RadComboBox;
        //    string statusOrderCode = rcStatus.SelectedValue;
        //    BindRadComboBoxPendingReason(rcPendingReason, statusOrderCode);
        //}

        protected void ddlsearch_Selectedindexchanged(object sender, EventArgs e)
        {
            if (ddlsearch.SelectedItem.Text == "Customer")
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
            else if (ddlsearch.SelectedItem.Text == "Pan")
            {
                clearPancustomerDetails();
                trCust.Visible = false;
                trpan.Visible = true;
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
                    //txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
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
        protected void BindRadComboBoxPendingReason(RadComboBox rcPendingReason, string statusOrderCode)
        {
            DataTable dtReason = orderbo.GetOrderStatusPendingReason(statusOrderCode);
            if (dtReason.Rows.Count > 0)
            {
                rcPendingReason.DataSource = dtReason;
                rcPendingReason.DataValueField = dtReason.Columns["XSR_StatusReasonCode"].ToString();
                rcPendingReason.DataTextField = dtReason.Columns["XSR_StatusReason"].ToString();
                rcPendingReason.DataBind();
                if (rcPendingReason.SelectedIndex == 0)
                {
                    rcPendingReason.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Select Reason", "0"));
                }
            }
        }
        private void BindSchemeSwitch()
        {
            //DataSet dsSwitchScheme = new DataSet();
            //DataTable dtSwitchScheme;
            //if (ddlAMCList.SelectedIndex != 0)
            //{
            //    amcCode = int.Parse(ddlAMCList.SelectedValue);
            //    dsSwitchScheme = operationBo.GetSwitchScheme(amcCode);
            //}
            //if (dsSwitchScheme.Tables.Count > 0)
            //{
            //    dtSwitchScheme = dsSwitchScheme.Tables[0];
            //    ddlSchemeSwitch.DataSource = dtSwitchScheme;
            //    ddlSchemeSwitch.DataTextField = dtSwitchScheme.Columns["PASP_SchemePlanName"].ToString();
            //    ddlSchemeSwitch.DataValueField = dtSwitchScheme.Columns["PASP_SchemePlanCode"].ToString();
            //    ddlSchemeSwitch.DataBind();
            //    ddlSchemeSwitch.Items.Insert(0, new ListItem("Select", "Select"));
            //}
            //else
            //{
            //    ddlSchemeSwitch.Items.Clear();
            //    ddlSchemeSwitch.DataSource = null;
            //    ddlSchemeSwitch.DataBind();
            //    ddlSchemeSwitch.Items.Insert(0, new ListItem("Select", "Select"));
            //}
        }
        protected string Getpagepath(string pageID)
        {
            ResourceManager resourceMessages = new ResourceManager("WealthERP.ControlMapping", typeof(ControlHost).Assembly);
            return resourceMessages.GetString(pageID);
        }
        protected void DdlLoad_Selectedindexchanged(object sender, EventArgs e)
        {

            if (DdlLoad.SelectedIndex == 0)
            {
                PlaceHolder1.Visible = false;
                FixedIncomePlaceHolder.Visible = false;
                GetProductWiseControls();
                ShowTransactionType(0);
            }

            if (DdlLoad.SelectedIndex == 1)
            {
                PlaceHolder1.Visible = true ;
                FixedIncomePlaceHolder.Visible = false;
                tr1.Visible = false;
              //  GetProductWiseControls();
                //Session.Remove("ChildControl");
                //PlaceHolder1.Controls.Clear();
                //PlaceHolder1.Visible = true;
                //Session["ChildControl"] = "MF";
                //PlaceHolder1.Controls.Clear();
                //ChildControls("ProductOrderDetailsMF");
                
            }

            if (DdlLoad.SelectedIndex == 2)
            {
                PlaceHolder1.Visible = false ;
                FixedIncomePlaceHolder.Visible = true;

                tr1.Visible = true;

                Label6.Text = "Fixed Income order Entry";
                //Session.Remove("ChildControl");
                //PlaceHolder1.Controls.Clear();
                //Session["ChildControl"] = "FI";
                //PlaceHolder1.Controls.Clear();
                //ChildControls("FixedIncomeOrderEntry");
                         

            }
            GetProductWiseControls();
        
        }

        private void ChildControls(String pageID)
        {
                //string path = Getpagepath(pageID);
                //UserControl uc1 = new UserControl();
                //uc1 = (UserControl)this.Page.LoadControl(path);
                //uc1.ID = "ctrl_" + pageID;
                //PlaceHolder1.Controls.Add(uc1);
              //  uc1. += new uc1.IndexChangedEventHandler(type);

        }
       


        //protected void//sai             ddlAMCList._SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlAMCList.SelectedIndex != 0)
        //    {
        //        hdnAmcCode.Value =//sai             ddlAMCList..SelectedItem.Text;
        //        amcCode = int.Parse(ddlAMCList.SelectedValue);
        //        if (TransType == "BUY" || TransType == "SIP")
        //        {
        //            BindScheme(0);
        //            //sai        BindFolioNumber(0);
        //        }
        //        else
        //        {
        //            BindScheme(1);
        //            ////sai        BindFolioNumber(1);
        //        }
        //        BindSchemeSwitch();
        //    }
        //}

        //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCategory.SelectedIndex != 0)
        //    {
        //        if (ddlAMCList.SelectedIndex != 0)
        //        {
        //            amcCode = int.Parse(ddlAMCList.SelectedValue);
        //            categoryCode = ddlCategory.SelectedValue;
        //            if (TransType == "BUY" || TransType == "SIP")
        //                BindScheme(0);
        //            else
        //                BindScheme(1);
        //        }
        //    }
        //}

        //protected void //sai      ddlAmcSchemeList_SelectedIndexChanged(object sender, EventArgs e)
        //{
            //string transactionType = "";
            //int schemeCode = 0;
            //string FileName = "";
            //if (//sai      ddlAmcSchemeList.SelectedIndex != 0)
            //{
            //    schemePlanCode = int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
            //    hdnSchemeCode.Value = //sai      ddlAmcSchemeList.SelectedValue.ToString();
            //    hdnSchemeName.Value = //sai      ddlAmcSchemeList.SelectedItem.Text;
            //    categoryCode = productMFBo.GetCategoryNameFromSChemeCode(schemePlanCode);
            //    ddlCategory.SelectedValue = categoryCode;
            //    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
            //    {
            //        if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            //        {
            //            //sai        BindFolioNumber(1);
            //            DataSet dsGetAmountUnits;
            //            DataTable dtGetAmountUnits;
            //            dsGetAmountUnits = operationBo.GetAmountUnits(schemePlanCode, int.Parse(txtCustomerId.Value));
            //            dtGetAmountUnits = dsGetAmountUnits.Tables[0];
            //            lblGetAvailableAmount.Text = dtGetAmountUnits.Rows[0]["CMFNP_CurrentValue"].ToString();
            //            lblGetAvailableUnits.Text = dtGetAmountUnits.Rows[0]["CMFNP_NetHoldings"].ToString();
            //            if ((TransType == "STB" || TransType == "Switch") &&//sai             ddlAMCList..SelectedIndex != 0)
            //            {
            //                BindSchemeSwitch();
            //            }
            //        }
            //    }
            //    else
            //        //sai        BindFolioNumber(0);
            //}
        //}
        //protected void //sai  txtOrderDate_DateChanged(object sender, EventArgs e)
        //{
        //    string ddlTrxnType = "";
        //    DateTime dt = Convert.ToDateTime(//sai  txtOrderDate.SelectedDate);
        //    //if (ddlTransactionType.SelectedItem.Value == "Sell")
        //    //{
        //    //    ddlTrxnType = "SEL";
        //    //}
        //    if (ddltransType.SelectedItem.Value == "BUY")
        //    {
        //        ddlTrxnType = "BUY";
        //    }
        //    if (!String.IsNullOrEmpty(hdnSchemeCode.Value.ToString()))
        //        schemePlanCode = int.Parse(hdnSchemeCode.Value);
        //    else
        //        schemePlanCode = 0;
        //    DataSet dsNavDetails = new DataSet();
        //    dsNavDetails = customerPortfolioBo.GetMFSchemePlanPurchaseDateAndValue(schemePlanCode, dt, ddlTrxnType);

        //    if (dsNavDetails != null)
        //    {
        //        if (dsNavDetails.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drNavDetails in dsNavDetails.Tables[0].Rows)
        //            {
        //                if (drNavDetails["PSP_RepurchasePrice"].ToString() != null && drNavDetails["PSP_RepurchasePrice"].ToString() != "")
        //                {
        //                    txtNAV.Text = drNavDetails["PSP_RepurchasePrice"].ToString();
        //                    lblNavAsOnDate.Text = Convert.ToDateTime(drNavDetails["PSP_Date"]).ToShortDateString();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            txtNAV.Text = "0";
        //            lblNavAsOnDate.Text = "Not Available";
        //        }
        //    }
        //}
        
            protected void   btnView_Click(object sender, EventArgs e)
               {


                
          BindGvOrderList();

                }
        protected void   btnSubmit_Click(object sender, EventArgs e)
        {
            orderVo.CustomerId = int.Parse(txtCustomerId.Value);
            if (DdlLoad.SelectedIndex == 1)
            {
                orderVo.AssetGroup = "MF";
                GetMFCOntrols();
            }
            else if (DdlLoad.SelectedIndex == 2)
            {
                orderVo.AssetGroup = "FI";
            }
            GetFICOntrolsValues();
            List<int> OrderIds = new List<int>();
            OrderIds = fiorderBo.CreateOrderFIDetails(orderVo, fiorderVo, userVo.UserId);
            rgvOrderSteps.Enabled = true;
            orderId = int.Parse(OrderIds[0].ToString());
            Session["CO_OrderId"] = orderId;
            orderVo.OrderId = orderId;
            BindOrderStepsGrid();
            // if (Session["MFTranstype"]  != null)
            //   Response.Write(Session["MFTranstype"] );

            // UserControl r1 = (UserControl)this.FindControl("FixedIncomeOrder");             
            //DropDownList ddl = (DropDownList)r1.FindControl("ddltransType");



          //  UserControl p1 = (UserControl)Page.FindControl("ctrl_ProductOrderMaster");

         //   //ctrl_ProductOrderMaster$TestControl$ddltransType
         //   UserControl p = (UserControl)Page.FindControl("ctrl_ProductOrderMaster").FindControl("PlaceHolder1").FindControl("ctrl_ProductOrderDetailsMF");
          //  PlaceHolder p = (PlaceHolder)Page.FindControl("ctrl_ProductOrderMaster").FindControl("PlaceHolder1");
         //   UserControl q = (UserControl)this.FindControl("ctrl_ProductOrderDetailsMF");
         //   //ctrl_ProductOrderMaster$ctrl_ProductOrderDetailsMF
           //UserControl r1 = (UserControl)p.FindControl("TestControl");
           // //ctrl_ProductOrderMaster$TestControl
           // DropDownList ddl = (DropDownList)q.FindControl("ddltransType");
            

         //   UserControl r = (UserControl)this.FindControl("ctrl_ProductOrderMaster");
          //UserControl objTestControl = (UserControl)Child.FindControl("ctrl_ProductOrderMaster_TestControl");
          //  DropDownList ddl = (DropDownList)objTestControl.FindControl("ddltransType");

         //   DropDownList ddl = (DropDownList)Page.FindControl("ddltransType");
              //Response.Write(ddl.SelectedValue);
        // Response.Write(ddl.SelectedValue);
           
          // SaveOrderDetails();
          // List<int> OrderIds = new List<int>();
          //  OrderIds = mfOrderBo.CreateCustomerMFOrderDetails(orderVo, mforderVo, userVo.UserId);
            ////sai rgvOrderSteps.Visible = true;
            //orderId = int.Parse(OrderIds[0].ToString());
            //Session["CO_OrderId"] = orderId;
            //orderVo.OrderId = orderId;
            ////sai rgvOrderSteps.Enabled = true;
            //BindOrderStepsGrid();
            //SetEditViewMode(true);
            ////sai btnSubmit.Visible = false;
            ////sai lnkBtnEdit.Visible = true;
            ////sai  lnlBack.Visible = false;
            //imgBtnRefereshBank.Enabled = false;
            //btnViewReport.Visible = true;
            //btnViewInPDF.Visible = true;
            //btnViewInDOC.Visible = true;
            ////sai  btnViewInPDFNew.Visible = false;
            ////sai  btnViewInDOCNew.Visible = false;
        }
        private void GetFICOntrolsValues()
        {
//            CO_OrderDate
//C_CustomerId fil
//WOSR_SourceCode fil
//CO_ApplicationNumber  fil
//CO_ApplicationReceivedDate fil
//CO_ChequeNumber fil
//CO_PaymentDate fil
//CB_CustBankAccId  fil
//AA_AdviserAssociateId 
//AAC_AdviserAgentId 
//AAC_AgentCode 
//CO_CreatedOn
//CO_ModifiedBy
//CO_ModifiedOn
//CO_CreatedBy
//PAG_AssetGroupCode fil
//XPM_PaymentModeCode 
//CO_IsClose
            int i = 0;
            UserControl FIControls = (UserControl)this.FindControl("FixedIncomeOrder");

            DropDownList ddlCategory = (DropDownList)FIControls.FindControl("ddlCategory");//ddlIssuer
            DropDownList ddlIssuer = (DropDownList)FIControls.FindControl("ddlIssuer");
            DropDownList ddlScheme = (DropDownList)FIControls.FindControl("ddlScheme");
            DropDownList ddlSeries = (DropDownList)FIControls.FindControl("ddlSeries");
            DropDownList ddltransType = (DropDownList)FIControls.FindControl("ddltransType");
            TextBox txtSeries = (TextBox)FIControls.FindControl("txtSeries");
            DropDownList ddlSchemeOption = (DropDownList)FIControls.FindControl("ddlSchemeOption");
            DropDownList ddlFrequency = (DropDownList)FIControls.FindControl("ddlFrequency");

            DropDownList ddlModeofHOlding = (DropDownList)FIControls.FindControl("ddlModeofHOlding");

            Label lblGetOrderNo = (Label)FIControls.FindControl("lblGetOrderNo");
            RadDatePicker txtOrderDate = (RadDatePicker)FIControls.FindControl("txtOrderDate");
            TextBox txtApplicationNumber = (TextBox)FIControls.FindControl("txtApplicationNumber");
            RadDatePicker txtApplicationDate = (RadDatePicker)FIControls.FindControl("txtApplicationDate");
            TextBox txtExistDepositreceiptno = (TextBox)FIControls.FindControl("txtExistDepositreceiptno");
            TextBox txtRenAmt = (TextBox)FIControls.FindControl("txtRenAmt");
            RadDatePicker txtMaturDate = (RadDatePicker)FIControls.FindControl("txtMaturDate");
            TextBox txtMatAmt = (TextBox)FIControls.FindControl("txtMatAmt");
            TextBox txtPayAmt = (TextBox)FIControls.FindControl("txtPayAmt");
            CheckBox ChkSeniorcitizens = (CheckBox)FIControls.FindControl("ChkSeniorcitizens");
            CheckBox ChkWidow = (CheckBox)FIControls.FindControl("ChkWidow");
            CheckBox ChkArmedForcePersonnel = (CheckBox)FIControls.FindControl("ChkArmedForcePersonnel");
            CheckBox CHKExistingrelationship = (CheckBox)FIControls.FindControl("CHKExistingrelationship");
            CheckBox ChkFirstholder = (CheckBox)FIControls.FindControl("ChkFirstholder");
            CheckBox ChkEORS = (CheckBox)FIControls.FindControl("ChkEORS");
            RadioButton rbtnYes = (RadioButton)FIControls.FindControl("rbtnYes");
            RadioButton rbtnNo = (RadioButton)FIControls.FindControl("rbtnNo");
            GridView gvJointHoldersList = (GridView)FIControls.FindControl("gvJointHoldersList");
            //trError
            HtmlTableRow trError = (HtmlTableRow)FIControls.FindControl("trError");
            Label lblError = (Label)FIControls.FindControl("lblError");


            if (rbtnNo.Checked)
            {
                customerAccountsVo.IsJointHolding = 0;
            }
            if (rbtnYes.Checked)
            {
                customerAccountsVo.IsJointHolding = 1;
            }


            if (gvJointHoldersList.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in gvJointHoldersList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                    {
                        i++;
                        customerAccountAssociationVo.AssociationId = int.Parse(gvJointHoldersList.DataKeys[gvr.RowIndex].Values[1].ToString());
                        customerAccountAssociationVo.AssociationType = "Joint Holder";
                        customerAccountBo.CreateFixedIncomeAccountAssociation(customerAccountAssociationVo, userVo.UserId);
                    }

                }
            }
            else
            {
                i = -1;
            }


            if (rbtnYes.Checked)
            {
                if (i == 0)
                {
                    trError.Visible = true;
                    lblError.Text = "Please select a Joint Holder";
                    return;
                    //blResult = false;
                }
                else
                {
                    trError.Visible = false;
                }
            }












            orderVo.CustomerId = int.Parse(txtCustomerId.Value);

            orderVo.AssetGroup = "FI";
            if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
                orderVo.ChequeNumber = txtPaymentNumber.Text;
            else
                orderVo.ChequeNumber = "";
            if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
                orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
            else
                orderVo.PaymentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
            {
                if (ddlBankName.SelectedValue != "Select")
                    orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
                else
                    orderVo.CustBankAccId = 0;
            }
            else
                orderVo.CustBankAccId = 0;

            if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
                AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            if (AgentId.Rows.Count > 0)
            {
                fiorderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
            }
            else
                fiorderVo.AgentId = 0;

            //orderVo.AA_AdviserAssociateId = 0;
            //orderVo.AAC_AdviserAgentId = 0;
            //orderVo.AAC_AgentCode = "";
            //orderVo.PaymentMode = "";

            fiorderVo.DepCustBankAccId = int.Parse(ddlDepoBank.SelectedValue);

           
            hdnFromdate.Value = txtOrderDate.SelectedDate.ToString() ;
            hdnTodate.Value = txtApplicationDate.SelectedDate.ToString() ;
            //mforderVo.OrderNumber
             orderVo.OrderNumber = Convert.ToInt32(lblGetOrderNo.Text);
             if (ChkSeniorcitizens.Checked==true)
             {
                 fiorderVo.Privilidge = "Seniorcitizens";
             }
             else if(ChkWidow.Checked==true){
                 fiorderVo.Privilidge = "Widow";
             }
             else if(ChkArmedForcePersonnel.Checked==true){
                 fiorderVo.Privilidge = "ArmedForcePersonnel";
             }
             else if (CHKExistingrelationship.Checked==true)
             {
                 fiorderVo.Privilidge = "Existingrelationship";
             }

             if (ChkFirstholder.Checked == true)
             {
                 fiorderVo.Depositpayableto = "Firstholder";
             }
             else if (ChkEORS.Checked == true)
             {
                 fiorderVo.Depositpayableto = "Either or survivor";
             }

             orderVo.OrderDate = Convert.ToDateTime (txtOrderDate.SelectedDate);
            orderVo.ApplicationReceivedDate = Convert.ToDateTime (txtApplicationDate.FocusedDate);
            orderVo.ApplicationNumber = txtApplicationNumber.Text;

            fiorderVo.SeriesDetails = txtSeries.Text;
            fiorderVo.TransactionType = ddltransType.SelectedValue;
            fiorderVo.AssetInstrumentCategoryCode = ddlCategory.SelectedValue ;
            fiorderVo.IssuerId =  ddlIssuer.SelectedValue;
            fiorderVo.ExisitingDepositreceiptno = txtExistDepositreceiptno.Text ;
            if (!string.IsNullOrEmpty(txtRenAmt.Text))
                fiorderVo.RenewalAmount = Convert.ToDouble(txtRenAmt.Text);
            else
                fiorderVo.RenewalAmount = 0;

            fiorderVo.MaturityDate = Convert.ToDateTime(txtMaturDate.FocusedDate);

            if (!string.IsNullOrEmpty(txtMatAmt.Text))
            fiorderVo.MaturityAmount = Convert.ToDouble(txtMatAmt.Text);
            else
                fiorderVo.MaturityAmount = 0;
      
            fiorderVo.SchemeId = Convert.ToInt32(ddlScheme.SelectedValue);
            fiorderVo.SeriesId =Convert.ToInt32( ddlSeries.SelectedValue);
            if (!string.IsNullOrEmpty(txtPayAmt.Text))
            fiorderVo.AmountPayable =Convert.ToDouble( txtPayAmt.Text);
            else
                fiorderVo.AmountPayable = 0;

            fiorderVo.ModeOfHolding = "0"; //ddlModeofHOlding.SelectedValue;
            fiorderVo.Schemeoption = ddlSchemeOption.SelectedValue;
            if (!string.IsNullOrEmpty(txtExistDepositreceiptno.Text))
            fiorderVo.ExisitingDepositreceiptno = txtExistDepositreceiptno.Text;
            else
                fiorderVo.ExisitingDepositreceiptno ="" ;
           
            fiorderVo.Frequency = ddlFrequency.SelectedValue; 
           // fiorderVo.Privilidge = "";



          

        }

        private void GetMFCOntrols()
        {
          
            //ht["Perimeter");

            if (Session["hashtab"] != null)
            {
                System.Collections.Hashtable ht = (System.Collections.Hashtable)Session["hashtab"];

                mforderVo.TransactionCode = ht["TransactionCode"].ToString();
                TransType = mforderVo.TransactionCode;
                if (ht["ApplicationReceivedDate"]!=null)
                {
                    orderVo.ApplicationReceivedDate = DateTime.Parse(ht["ApplicationReceivedDate"].ToString());
                }
                else
                    orderVo.ApplicationReceivedDate = DateTime.MinValue;

                orderVo.ApplicationNumber = ht["ApplicationNumber"].ToString();   //txtApplicationNumber.Text;
                if (Convert.ToInt32(ht["Amccode"]) != 0)
                    mforderVo.Amccode =Convert.ToInt32( ht["Amccode"]);// int.Parse(ddlAMCList.SelectedValue);
                else
                    mforderVo.Amccode = 0;
                mforderVo.category = ht["category"].ToString(); // ddlCategory.SelectedValue;
                if (Convert.ToInt32(ht["SchemePlanCode"].ToString()) != 0)
                    mforderVo.SchemePlanCode = Convert.ToInt32(ht["SchemePlanCode"]);//int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
                else
                    mforderVo.SchemePlanCode = 0;
                if (!string.IsNullOrEmpty(ht["portfolioId"].ToString()))
                mforderVo.portfolioId =Convert.ToInt32( ht["portfolioId"]);// int.TryParse(ht["portfolioId"]);

                if (!string.IsNullOrEmpty(ht["FolioNumber"].ToString())) //ht["FolioNumber"]  != null)
                    mforderVo.accountid = int.Parse(ht["FolioNumber"].ToString());
                else
                    mforderVo.accountid = 0;

                orderVo.OrderDate = Convert.ToDateTime(ht["OrderDate"]);
                mforderVo.OrderNumber = int.Parse(ht["OrderNumber"].ToString());  // int.Parse(//sai  lblGetOrderNo.Text);
                //orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
                //orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
                if (Convert.ToBoolean(ht["IsImmediate"]) == true)
                    mforderVo.IsImmediate = 1;
                else
                    mforderVo.IsImmediate = 0;
                
                //    if (!string.IsNullOrEmpty((//sai txtFutureDate.SelectedDate).ToString().Trim()))

                if (!string.IsNullOrEmpty(ht["IsImmediate"].ToString()))
                   
                    mforderVo.FutureExecutionDate = DateTime.Parse(ht["FutureExecutionDate"].ToString()); // DateTime.Parse(//sai txtFutureDate.SelectedDate.ToString());
                else
                    mforderVo.FutureExecutionDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((ht["FutureTriggerCondition"].ToString())))
                          {
                              //sai mforderVo.FutureTriggerCondition =  txtFutureTrigger.Text;
                    }
                else
                    {
                    mforderVo.FutureTriggerCondition = "";
                    }
                if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
                    mforderVo.Amount = double.Parse(txtAmount.Text);
                else
                    mforderVo.Amount = 0;
            }


        }
        private void SaveOrderDetails()
        {
            orderVo.CustomerId = int.Parse(txtCustomerId.Value);
            if (DdlLoad.SelectedIndex == 1)
            {
                orderVo.AssetGroup = "MF";
                GetMFCOntrols();
            }
            else if (DdlLoad.SelectedIndex == 2)
            {
                orderVo.AssetGroup = "FI";
            }
            GetFICOntrolsValues();
            //mforderVo.CustomerName = txtCustomerName.Text;
            //mforderVo.BMName = lblGetBranch.Text;
            //mforderVo.RMName = lblGetRM.Text;
            //mforderVo.PanNo = lblgetPan.Text;
           
            //mforderVo.TransactionCode = TransType;
            //if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString().Trim()))
            //{
            //    orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            //}
            //else
            //    orderVo.ApplicationReceivedDate = DateTime.MinValue;
            //orderVo.ApplicationNumber = txtApplicationNumber.Text;
            //if (ddlAMCList.SelectedIndex != 0)
            //    mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            //else
            //    mforderVo.Amccode = 0;
            //mforderVo.category = ddlCategory.SelectedValue;
            //if (//sai      ddlAmcSchemeList.SelectedIndex != 0)
            //    mforderVo.SchemePlanCode = int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
            //else
            //    mforderVo.SchemePlanCode = 0;
            //mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            //if (ddlFolioNumber.SelectedIndex != -1)
            //    mforderVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
            //else
            //    mforderVo.accountid = 0;
            //orderVo.OrderDate = Convert.ToDateTime(//sai  txtOrderDate.SelectedDate);
            //mforderVo.OrderNumber = int.Parse(//sai  lblGetOrderNo.Text);
            ////orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
            ////orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
            //if (rbtnImmediate.Checked == true)
            //    mforderVo.IsImmediate = 1;
            //else
            //    mforderVo.IsImmediate = 0;
            //if (!string.IsNullOrEmpty((//sai txtFutureDate.SelectedDate).ToString().Trim()))
            //    mforderVo.FutureExecutionDate = DateTime.Parse(//sai txtFutureDate.SelectedDate.ToString());
            //else
            //    mforderVo.FutureExecutionDate = DateTime.MinValue;
            //if (!string.IsNullOrEmpty((//sai txtFutureTrigger.Text).ToString().Trim()))
            //    mforderVo.FutureTriggerCondition = //sai txtFutureTrigger.Text;
            //else
            //    mforderVo.FutureTriggerCondition = "";
            if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
                mforderVo.Amount = double.Parse(txtAmount.Text);
            else
                mforderVo.Amount = 0;
            //if (!string.IsNullOrEmpty((lblGetAvailableUnits.Text).ToString().Trim()))
            //    mforderVo.Units = double.Parse(lblGetAvailableUnits.Text);
            //else
            //    mforderVo.Units = 0;
            if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
            {
                if (rbtAmount.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        mforderVo.Amount = double.Parse(txtNewAmount.Text);
                    else
                        mforderVo.Amount = 0;
                }
                if (rbtUnit.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        mforderVo.Units = double.Parse(txtNewAmount.Text);
                    else
                        mforderVo.Units = 0;
                }

            }
             orderVo.PaymentMode = ddlPaymentMode.SelectedValue;
            
            if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
                orderVo.ChequeNumber = txtPaymentNumber.Text;
            else
                orderVo.ChequeNumber = "";
            if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
                orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
            else
                orderVo.PaymentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
            {
                if (ddlBankName.SelectedValue != "Select")
                    orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
                else
                    orderVo.CustBankAccId = 0;
            }
            else
                orderVo.CustBankAccId = 0;
            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
            {
                if (ddlBankName.SelectedValue != "Select")
                    mforderVo.BankName = ddlBankName.SelectedItem.Text;
                else
                    mforderVo.BankName = "";
            }
            else
                mforderVo.BankName = "";
            if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
                mforderVo.BranchName = txtBranchName.Text;
            else
                mforderVo.BranchName = "";
            if (ddlSchemeSwitch.SelectedValue != "")
            {
                if (ddlSchemeSwitch.SelectedIndex != 0)
                    mforderVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
                mforderVo.AddrLine1 = txtCorrAdrLine1.Text;
            else
                mforderVo.AddrLine1 = "";
            if (txtCorrAdrLine2.Text != "" || txtCorrAdrLine2.Text != null)
                mforderVo.AddrLine2 = txtCorrAdrLine2.Text;
            else
                mforderVo.AddrLine2 = "";
            if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
                mforderVo.AddrLine3 = txtCorrAdrLine3.Text;
            else
                mforderVo.AddrLine3 = "";
            if (!string.IsNullOrEmpty(txtLivingSince.SelectedDate.ToString().Trim()))
                mforderVo.LivingSince = DateTime.Parse(txtLivingSince.SelectedDate.ToString());
            else
                mforderVo.LivingSince = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
                mforderVo.City = txtCorrAdrCity.Text;
            else
                mforderVo.City = "";
            if (ddlCorrAdrState.SelectedIndex > 0)  //!= 0)
                mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
            else
                mforderVo.State = "";
            if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
                mforderVo.Pincode = txtCorrAdrPinCode.Text;
            else
                mforderVo.Pincode = "";
            mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
            if (TransType == "SIP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
                    mforderVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
                if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
                    mforderVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
                else
                    mforderVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
                    mforderVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
                else
                    mforderVo.EndDate = DateTime.MinValue;
            }
            else if (TransType == "STB" || TransType == "SWP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
                    mforderVo.FrequencyCode = ddlFrequencySTP.SelectedValue;
                if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
                    mforderVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
                else
                    mforderVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
                    mforderVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
                else
                    mforderVo.EndDate = DateTime.MinValue;
            }
            if (ddlARNNo.SelectedIndex != 0)
                mforderVo.ARNNo = ddlARNNo.SelectedItem.Text;
            if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
                AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            if (AgentId.Rows.Count > 0)
            {
                mforderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
            }
            else
                mforderVo.AgentId = 0;
            //Convert.ToInt32(ddlAssociate.SelectedValue);

            Session["orderVo"] = orderVo;
            Session["mforderVo"] = mforderVo;

        }

        //private void SaveOrderDetails()
        //{
        //    orderVo.CustomerId = int.Parse(txtCustomerId.Value);
        //    orderVo.AssetGroup = "MF";
        //    mforderVo.CustomerName = txtCustomerName.Text;
        //    mforderVo.BMName = lblGetBranch.Text;
        //    mforderVo.RMName = lblGetRM.Text;
        //    mforderVo.PanNo = lblgetPan.Text;

        //    mforderVo.TransactionCode = TransType;
        //    if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString().Trim()))
        //    {
        //        orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
        //    }
        //    else
        //        orderVo.ApplicationReceivedDate = DateTime.MinValue;
        //    orderVo.ApplicationNumber = txtApplicationNumber.Text;
        //    if (ddlAMCList.SelectedIndex != 0)
        //        mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
        //    else
        //        mforderVo.Amccode = 0;
        //    mforderVo.category = ddlCategory.SelectedValue;
        //    if (//sai      ddlAmcSchemeList.SelectedIndex != 0)
        //        mforderVo.SchemePlanCode = int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
        //    else
        //        mforderVo.SchemePlanCode = 0;
        //    mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
        //    if (ddlFolioNumber.SelectedIndex != -1)
        //        mforderVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
        //    else
        //        mforderVo.accountid = 0;
        //    orderVo.OrderDate = Convert.ToDateTime(//sai  txtOrderDate.SelectedDate);
        //    mforderVo.OrderNumber = int.Parse(//sai  lblGetOrderNo.Text);
        //    //orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
        //    //orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
        //    if (rbtnImmediate.Checked == true)
        //        mforderVo.IsImmediate = 1;
        //    else
        //        mforderVo.IsImmediate = 0;
        //    if (!string.IsNullOrEmpty((//sai txtFutureDate.SelectedDate).ToString().Trim()))
        //        mforderVo.FutureExecutionDate = DateTime.Parse(//sai txtFutureDate.SelectedDate.ToString());
        //    else
        //        mforderVo.FutureExecutionDate = DateTime.MinValue;
        //    if (!string.IsNullOrEmpty((//sai txtFutureTrigger.Text).ToString().Trim()))
        //        mforderVo.FutureTriggerCondition = //sai txtFutureTrigger.Text;
        //    else
        //        mforderVo.FutureTriggerCondition = "";
        //    if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
        //        mforderVo.Amount = double.Parse(txtAmount.Text);
        //    else
        //        mforderVo.Amount = 0;
        //    //if (!string.IsNullOrEmpty((lblGetAvailableUnits.Text).ToString().Trim()))
        //    //    mforderVo.Units = double.Parse(lblGetAvailableUnits.Text);
        //    //else
        //    //    mforderVo.Units = 0;
        //    if (TransType == "Sel" || TransType == "STB" || TransType == "SWP" || TransType == "SWB")
        //    {
        //        if (rbtAmount.Checked == true)
        //        {
        //            if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        //                mforderVo.Amount = double.Parse(txtNewAmount.Text);
        //            else
        //                mforderVo.Amount = 0;
        //        }
        //        if (rbtUnit.Checked == true)
        //        {
        //            if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
        //                mforderVo.Units = double.Parse(txtNewAmount.Text);
        //            else
        //                mforderVo.Units = 0;
        //        }

        //    }
        //    orderVo.PaymentMode = ddlPaymentMode.SelectedValue;
        //    if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
        //        orderVo.ChequeNumber = txtPaymentNumber.Text;
        //    else
        //        orderVo.ChequeNumber = "";
        //    if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
        //        orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
        //    else
        //        orderVo.PaymentDate = DateTime.MinValue;
        //    if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        //    {
        //        if (ddlBankName.SelectedValue != "Select")
        //            orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
        //        else
        //            orderVo.CustBankAccId = 0;
        //    }
        //    else
        //        orderVo.CustBankAccId = 0;
        //    if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
        //    {
        //        if (ddlBankName.SelectedValue != "Select")
        //            mforderVo.BankName = ddlBankName.SelectedItem.Text;
        //        else
        //            mforderVo.BankName = "";
        //    }
        //    else
        //        mforderVo.BankName = "";
        //    if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
        //        mforderVo.BranchName = txtBranchName.Text;
        //    else
        //        mforderVo.BranchName = "";
        //    if (ddlSchemeSwitch.SelectedValue != "")
        //    {
        //        if (ddlSchemeSwitch.SelectedIndex != 0)
        //            mforderVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
        //    }
        //    if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
        //        mforderVo.AddrLine1 = txtCorrAdrLine1.Text;
        //    else
        //        mforderVo.AddrLine1 = "";
        //    if (txtCorrAdrLine2.Text != "" || txtCorrAdrLine2.Text != null)
        //        mforderVo.AddrLine2 = txtCorrAdrLine2.Text;
        //    else
        //        mforderVo.AddrLine2 = "";
        //    if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
        //        mforderVo.AddrLine3 = txtCorrAdrLine3.Text;
        //    else
        //        mforderVo.AddrLine3 = "";
        //    if (!string.IsNullOrEmpty(txtLivingSince.SelectedDate.ToString().Trim()))
        //        mforderVo.LivingSince = DateTime.Parse(txtLivingSince.SelectedDate.ToString());
        //    else
        //        mforderVo.LivingSince = DateTime.MinValue;
        //    if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
        //        mforderVo.City = txtCorrAdrCity.Text;
        //    else
        //        mforderVo.City = "";
        //    if (ddlCorrAdrState.SelectedIndex != 0)
        //        mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
        //    else
        //        mforderVo.State = "";
        //    if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
        //        mforderVo.Pincode = txtCorrAdrPinCode.Text;
        //    else
        //        mforderVo.Pincode = "";
        //    mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
        //    if (TransType == "SIP")
        //    {
        //        if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
        //            mforderVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
        //        if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
        //            mforderVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
        //        else
        //            mforderVo.StartDate = DateTime.MinValue;
        //        if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
        //            mforderVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
        //        else
        //            mforderVo.EndDate = DateTime.MinValue;
        //    }
        //    else if (TransType == "STB" || TransType == "SWP")
        //    {
        //        if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
        //            mforderVo.FrequencyCode = ddlFrequencySTP.SelectedValue;
        //        if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
        //            mforderVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
        //        else
        //            mforderVo.StartDate = DateTime.MinValue;
        //        if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
        //            mforderVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
        //        else
        //            mforderVo.EndDate = DateTime.MinValue;
        //    }
        //    if (ddlARNNo.SelectedIndex != 0)
        //        mforderVo.ARNNo = ddlARNNo.SelectedItem.Text;
        //    if (!String.IsNullOrEmpty(txtAssociateSearch.Text))
        //        AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
        //    if (AgentId.Rows.Count > 0)
        //    {
        //        mforderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
        //    }
        //    else
        //        mforderVo.AgentId = 0;
        //    //Convert.ToInt32(ddlAssociate.SelectedValue);

        //    Session["orderVo"] = orderVo;
        //    Session["mforderVo"] = mforderVo;

        //}

        protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtnImmediate.Checked)
            //    trfutureDate.Visible = false;
            //else
            //    trfutureDate.Visible = true;
        }

        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>(); ;
            SaveOrderDetails();
            OrderIds = mfOrderBo.CreateCustomerMFOrderDetails(orderVo, mforderVo, userVo.UserId);
            //sai rgvOrderSteps.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order added successfully.');", true);

            ClearAllFields();

            txtCustomerName.Text = "";
            lblGetRM.Text = "";
            lblGetBranch.Text = "";
            lblgetPan.Text = "";
        }



        public void SetEditViewMode(bool Bool)
        {

            if (Bool)
            {

                //txtOrederNumber.Enabled = false;
                ////sai  txtOrderDate.Enabled = false;
                //ddlBranch.Enabled = false;
                //ddlRM.Enabled = false;
                txtCustomerName.Enabled = false;
                btnImgAddCustomer.Enabled = false;
               // ddltransType.Enabled = false;
                //ddlPortfolio.Enabled = false;
               // ddlFolioNumber.Enabled = false;
                //btnAddFolio.Enabled = false;
               //sai             ddlAMCList..Enabled = false;
                //sai      ddlAmcSchemeList.Enabled = false;
                //ddlCategory.Enabled = false;
                //txtReceivedDate.Enabled = false;
                //txtApplicationNumber.Enabled = false;
                //rbtnImmediate.Enabled = false;
                //rbtnFuture.Enabled = false;
                ////sai txtFutureDate.Enabled = false;
                ////sai txtFutureTrigger.Enabled = false;
                //ddlOrderStatus.Enabled = false;
                //ddlOrderPendingReason.Enabled = false;
                //sai  txtOrderDate.Enabled = false;

                txtAmount.Enabled = false;
                ddlPaymentMode.Enabled = false;
                txtPaymentInstDate.Enabled = false;
                txtPaymentNumber.Enabled = false;
                rbtAmount.Enabled = false;
                rbtUnit.Enabled = false;
                txtNewAmount.Enabled = false;
                ddlBankName.Enabled = false;
                txtBranchName.Enabled = false;
                ddlFrequencySIP.Enabled = false;
                txtstartDateSIP.Enabled = false;
                txtendDateSIP.Enabled = false;

                ddlSchemeSwitch.Enabled = false;
                ddlFrequencySTP.Enabled = false;
                txtstartDateSTP.Enabled = false;
                txtendDateSTP.Enabled = false;

                txtCorrAdrLine1.Enabled = false;
                txtCorrAdrLine2.Enabled = false;
                txtCorrAdrLine3.Enabled = false;
                txtLivingSince.Enabled = false;
                txtCorrAdrCity.Enabled = false;
                ddlCorrAdrState.Enabled = false;
                txtCorrAdrPinCode.Enabled = false;
                ddlCorrAdrCountry.Enabled = false;
                ddlARNNo.Enabled = false;

                //sai btnSubmit.Enabled = false;
                //btnAddMore.Visible = false;
                //ddlAssociate.Enabled = false;

            }
            else
            {
                //txtOrederNumber.Enabled = true;
                ////sai  txtOrderDate.Enabled = true;
                //ddlBranch.Enabled = true;
                //ddlRM.Enabled = true;
                txtCustomerName.Enabled = false;
                btnImgAddCustomer.Enabled = false;
              //  ddltransType.Enabled = true;
                //ddlPortfolio.Enabled = true;
              //  ddlFolioNumber.Enabled = true;
                //btnAddFolio.Enabled = true;
               //sai             ddlAMCList..Enabled = false;
                //sai      ddlAmcSchemeList.Enabled = true;
                //ddlCategory.Enabled = true;
                //txtReceivedDate.Enabled = true;
                //txtApplicationNumber.Enabled = true;
                //rbtnImmediate.Enabled = true;
                //rbtnFuture.Enabled = true;
                ////sai txtFutureDate.Enabled = true;
                ////sai txtFutureTrigger.Enabled = true;
                //ddlOrderStatus.Enabled = true;
                //ddlOrderPendingReason.Enabled = true;
                ////sai  txtOrderDate.Enabled = false;

                txtAmount.Enabled = true;
                ddlPaymentMode.Enabled = true;
                txtPaymentInstDate.Enabled = true;
                txtPaymentNumber.Enabled = true;
                rbtAmount.Enabled = true;
                rbtUnit.Enabled = true;
                txtNewAmount.Enabled = true;
                ddlBankName.Enabled = true;
                txtBranchName.Enabled = true;
                ddlFrequencySIP.Enabled = true;
                txtstartDateSIP.Enabled = true;
                txtendDateSIP.Enabled = true;

                ddlSchemeSwitch.Enabled = true;
                ddlFrequencySTP.Enabled = true;
                txtstartDateSTP.Enabled = true;
                txtendDateSTP.Enabled = true;

                txtCorrAdrLine1.Enabled = true;
                txtCorrAdrLine2.Enabled = true;
                txtCorrAdrLine3.Enabled = true;
                txtLivingSince.Enabled = true;
                txtCorrAdrCity.Enabled = true;
                ddlCorrAdrState.Enabled = true;
                txtCorrAdrPinCode.Enabled = true;
                ddlCorrAdrCountry.Enabled = true;
                ddlARNNo.Enabled = true;

                ////sai btnSubmit.Enabled = true;
                //btnAddMore.Visible = false;
                //ddlAssociate.Enabled = true;
            }


        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            SetEditViewMode(false);
            ViewForm = "Edit";
           // //sai lnkDelete.Visible = true;
            if (mforderVo != null && orderVo != null)
            {
                mforderVo = (MFOrderVo)Session["mforderVo"];
                orderVo = (OrderVo)Session["orderVo"];
            }
            if (mforderVo != null && orderVo != null)
            {
                if (ViewForm == "Edit")
                {
                    SetControls("Edit", mforderVo, orderVo);
                  //  //sai  lnlBack.Visible = true;
                }
            }
            else
            {
                SetControls("Entry", mforderVo, orderVo);
            }
            ////sai btnSubmit.Visible = false;
            ////sai rgvOrderSteps.Enabled = true;
            //btnAddMore.Visible = false;
            ////sai btnUpdate.Visible = true;
            ////sai lnkBtnEdit.Visible = false;
            //btnreport.Visible = true;
            //btnpdfReport.Visible = true;
        }

        protected void    lnlBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FromPage"] != null)
            {
               // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerOrderList','none');", true);
            }
            else if (Request.QueryString["action"] != null)
            {
              //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OrderList','none');", true);
            }
        }

        protected void  btnUpdate_Click(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>();
            UpdateMFOrderDetails();
            mfOrderBo.UpdateCustomerMFOrderDetails(orderVo, mforderVo, userVo.UserId);
            SetEditViewMode(true);
            imgBtnRefereshBank.Enabled = false;
            ////sai btnUpdate.Visible = false;
            ////sai  btnViewInPDFNew.Visible = false;
            ////sai  btnViewInDOCNew.Visible = false;
        }

        private void UpdateMFOrderDetails()
        {

            //operationVo.CustomerId = int.Parse(txtCustomerId.Value);
            //mforderVo.CustomerName = txtCustomerName.Text;
            //if (orderVo.CustomerId != 0)
            //    hdnCustomerId.Value = orderVo.CustomerId.ToString();
            //mforderVo.BMName = lblGetBranch.Text;
            //mforderVo.RMName = lblGetRM.Text;
            //mforderVo.PanNo = lblgetPan.Text;
            //mforderVo.TransactionCode = TransType;
            //if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString()))
            //    orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            //else
            //    orderVo.ApplicationReceivedDate = DateTime.MinValue;
            //orderVo.ApplicationNumber = txtApplicationNumber.Text;
            //if (ddlAMCList.SelectedIndex != -1)
            //    mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            //else
            //    mforderVo.Amccode = 0;
            //mforderVo.category = ddlCategory.SelectedValue;
            //if (//sai      ddlAmcSchemeList.SelectedIndex != -1)
            //    mforderVo.SchemePlanCode = int.Parse(//sai      ddlAmcSchemeList.SelectedValue);
            //else
            //    mforderVo.SchemePlanCode = 0;
            //// mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            //if (ddlFolioNumber.SelectedIndex != -1)
            //    mforderVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
            //else
            //    mforderVo.accountid = 0;
            //orderVo.OrderDate = Convert.ToDateTime(//sai  txtOrderDate.SelectedDate);
            //mforderVo.OrderNumber = int.Parse(//sai  lblGetOrderNo.Text);
            ////orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
            ////orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
            //if (rbtnImmediate.Checked == true)
            //    mforderVo.IsImmediate = 1;
            //else
            //    mforderVo.IsImmediate = 0;
            //if (!string.IsNullOrEmpty(//sai txtFutureDate.SelectedDate.ToString().Trim()))
            //    mforderVo.FutureExecutionDate = DateTime.Parse(//sai txtFutureDate.SelectedDate.ToString());
            //else
            //    mforderVo.FutureExecutionDate = DateTime.MinValue;
            //if (!string.IsNullOrEmpty((//sai txtFutureTrigger.Text).ToString().Trim()))
            //    mforderVo.FutureTriggerCondition = //sai txtFutureTrigger.Text;
            //else
            //    mforderVo.FutureTriggerCondition = "";
            //if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
            //    mforderVo.Amount = double.Parse(txtAmount.Text);
            //else
            //    mforderVo.Amount = 0;

            //if (TransType == "Sel" || TransType == "STP" || TransType == "SWP" || TransType == "SWB")
            //{
            //    if (rbtAmount.Checked == true)
            //    {
            //        if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
            //            mforderVo.Amount = double.Parse(txtNewAmount.Text);
            //        else
            //            mforderVo.Amount = 0;
            //    }
            //    if (rbtUnit.Checked == true)
            //    {
            //        if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
            //            mforderVo.Units = double.Parse(txtNewAmount.Text);
            //        else
            //            mforderVo.Units = 0;
            //    }
            //}
            //if (TransType == "SIP")
            //{
            //    if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
            //        mforderVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
            //    if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
            //        mforderVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
            //    else
            //        mforderVo.StartDate = DateTime.MinValue;
            //    if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
            //        mforderVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
            //    else
            //        mforderVo.EndDate = DateTime.MinValue;
            //}
            //else if (TransType == "STB" || TransType == "SWP")
            //{
            //    if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
            //        mforderVo.FrequencyCode = ddlFrequencySTP.SelectedValue;

            //    if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
            //        mforderVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
            //    else
            //        mforderVo.StartDate = DateTime.MinValue;
            //    if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
            //        mforderVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
            //    else
            //        mforderVo.EndDate = DateTime.MinValue;
            //}

            //if (ddlPaymentMode.SelectedValue == "ES")
            //    orderVo.PaymentMode = "ES";
            //else if (ddlPaymentMode.SelectedValue == "DF")
            //    orderVo.PaymentMode = "DF";
            //else if (ddlPaymentMode.SelectedValue == "CQ")
            //    orderVo.PaymentMode = "CQ";
            //if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
            //    orderVo.ChequeNumber = txtPaymentNumber.Text;
            //else
            //    orderVo.ChequeNumber = "";
            //if (txtPaymentInstDate.SelectedDate != null)
            //    orderVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
            //else
            //    orderVo.PaymentDate = DateTime.MinValue;
            //if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
            //{
            //    if (ddlBankName.SelectedValue != "Select")
            //        orderVo.CustBankAccId = int.Parse(ddlBankName.SelectedValue);
            //    else
            //        orderVo.CustBankAccId = 0;
            //}
            //else
            //    orderVo.CustBankAccId = 0;
            //if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
            //{
            //    if (ddlBankName.SelectedValue != "Select")
            //        mforderVo.BankName = ddlBankName.SelectedItem.Text;
            //    else
            //        mforderVo.BankName = "";
            //}
            //else
            //    mforderVo.BankName = "";
            //if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
            //    mforderVo.BranchName = txtBranchName.Text;
            //else
            //    mforderVo.BranchName = "";
            //if (ddlSchemeSwitch.SelectedValue != "")
            //{
            //    if (ddlSchemeSwitch.SelectedIndex != 0)
            //        mforderVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
            //    mforderVo.AddrLine1 = txtCorrAdrLine1.Text;
            //else
            //    mforderVo.AddrLine1 = "";
            //if (!string.IsNullOrEmpty(txtCorrAdrLine2.Text.ToString().Trim()))
            //    mforderVo.AddrLine2 = txtCorrAdrLine2.Text;
            //else
            //    mforderVo.AddrLine2 = "";
            //if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
            //    mforderVo.AddrLine3 = txtCorrAdrLine3.Text;
            //else
            //    mforderVo.AddrLine3 = "";
            //if (txtLivingSince.SelectedDate.ToString() != "dd/mm/yyyy")
            //    mforderVo.LivingSince = DateTime.MinValue;
            //else
            //    mforderVo.LivingSince = DateTime.MinValue;
            //if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
            //    mforderVo.City = txtCorrAdrCity.Text;
            //else
            //    mforderVo.City = "";
            //if (ddlCorrAdrState.SelectedIndex != 0)
            //    mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
            //else
            //    mforderVo.State = "";
            //if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
            //    mforderVo.Pincode = txtCorrAdrPinCode.Text;
            //else
            //    mforderVo.Pincode = "";
            //mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
            //if (ddlARNNo.SelectedIndex != 0)
            //    mforderVo.ARNNo = ddlARNNo.SelectedItem.Text;
            //if (!string.IsNullOrEmpty(txtAssociateSearch.Text))
            //{

            //    AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
            //    if (AgentId.Rows.Count > 0)
            //    {
            //        orderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
            //    }
            //    else
            //        orderVo.AgentId = 0;
            //}
            //if (ddlAssociate.SelectedIndex != 0)
            //    orderVo.AgentId = Convert.ToInt32(ddlAssociate.SelectedValue);
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int BankAccountId = 0;
            DataTable dtgetBankBranch;
            if (ddlBankName.SelectedIndex != 0)
            {
                BankAccountId = int.Parse(ddlBankName.SelectedValue);
                dtgetBankBranch = mfOrderBo.GetBankBranch(BankAccountId);
                if (dtgetBankBranch.Rows.Count > 0)
                {
                    DataRow dr = dtgetBankBranch.Rows[0];
                    txtBranchName.Text = dr["CB_BranchName"].ToString();
                }
                hdnBankName.Value = ddlBankName.SelectedItem.Text;
            }
        }
        protected void ddlDepoBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            int BankAccountId = 0;
            DataTable dtgetBankBranch;
            if (ddlBankName.SelectedIndex != 0)
            {
                BankAccountId = int.Parse(ddlDepoBank.SelectedValue);
                dtgetBankBranch = mfOrderBo.GetBankBranch(BankAccountId);
                if (dtgetBankBranch.Rows.Count > 0)
                {
                    DataRow dr = dtgetBankBranch.Rows[0];
                    txtDepositedBranch.Text = dr["CB_BranchName"].ToString();
                }
             //   hdnBankName.Value = ddlBankName.SelectedItem.Text;
            }
        }
        protected void   lnkDelete_Click(object sender, EventArgs e)
        {
            if (mforderVo != null && orderVo != null)
            {
                orderId = orderVo.OrderId;
                if (orderId != 0)
                {
                    mfOrderBo.DeleteMFOrder(orderId);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order has been deleted.');", true);
                    ClearAllFields();

                    lblGetRM.Text = "";
                    lblGetBranch.Text = "";
                    lblgetPan.Text = "";
                    ////sai  txtOrderDate.SelectedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                    ////sai  lblGetOrderNo.Text = ((mfOrderBo.GetOrderNumber()) + 1).ToString();
                    //txtApplicationNumber.Enabled = true;
                    ////sai lnkBtnEdit.Visible = false;
                    ////sai  lnlBack.Visible = false;
                    ////sai lnkDelete.Visible = false;
                    ////sai btnUpdate.Visible = false;
                    ////sai btnSubmit.Visible = true;
                    //btnAddMore.Visible = true;
                    ////sai rgvOrderSteps.Visible = false;
                    //SetEditViewMode(false);
                    //btnImgAddCustomer.Enabled = true;
                    //btnImgAddCustomer.Visible = true;
                    //txtCustomerName.Enabled = true;
                    //txtCustomerName.Text = "";

                }

            }

        }

        protected void btnreport_Click(object sender, EventArgs e)
        {
            mail = "0";
            DisplayTransactionSlip();

        }

        private void DisplayTransactionSlip()
        {
            string schemeSwitch = ""; string bankName = ""; string arnno = "";
            if (!string.IsNullOrEmpty(hdnCustomerId.Value.ToString().Trim()))
                customerId = int.Parse(hdnCustomerId.Value);
            if (!string.IsNullOrEmpty(hdnPortfolioId.Value.ToString().Trim()))
                portfolioId = int.Parse(hdnPortfolioId.Value);
            if (ddlSchemeSwitch.SelectedIndex != -1 && ddlSchemeSwitch.SelectedIndex != 0)
                schemeSwitch = ddlSchemeSwitch.SelectedItem.Text;
            if (ddlBankName.SelectedIndex != -1 && ddlBankName.SelectedIndex != 0)
                bankName = ddlBankName.SelectedItem.Text;
            if (ddlARNNo.SelectedIndex != 0)
                arnno = ddlARNNo.SelectedItem.Text;

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Display", "loadcontrol('Display','action=Order');", true);
            //Response.Write("<script type='text/javascript'>detailedresults= window.open('Display.aspx?PageId=Display&result1=" + var1 + "&result2=" + var2 + "', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no');</script>");
            Response.Write("<script type='text/javascript'>detailedresults=window.open('Reports/Display.aspx?Page=MFOrder&CustomerId=" + customerId + "&AmcCode=" + hdnAmcCode.Value +
                "&AccoutId=" + hdnAccountId.Value + "&SchemeCode=" + hdnSchemeName.Value + "&Type=" + hdnType.Value + "&Portfolio=" + portfolioId +
                "&BankName=" + bankName + "&BranchName=" + txtBranchName.Text + "&Amount=" + txtAmount.Text + "&ChequeNo=" + txtPaymentNumber.Text + "&ChequeDate=" + txtPaymentInstDate.SelectedDate +
                "&StartDateSIP=" + txtstartDateSIP.SelectedDate + "&StartDateSTP=" + txtstartDateSTP.SelectedDate + "&NewAmount=" + txtNewAmount.Text +
                "&EndDateSIP=" + txtendDateSIP.SelectedDate + "&EndDateSTP=" + txtendDateSTP.SelectedDate + "&SchemeSwitch=" + schemeSwitch +
                "&RbtnUnits=" + rbtUnit.Checked + "&RbtnAmounts=" + rbtAmount.Checked + "&ArnNo=" + arnno + "&mail=" + mail +
                "','mywindow', 'width=1000,height=450,scrollbars=yes,location=center');</script>");
        }

        protected void btnpdfReport_Click(object sender, EventArgs e)
        {
            mail = "2";
            DisplayTransactionSlip();
        }
        //CustomerVo customerVo = new CustomerVo();
        //CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        //CustomerBo customerBo = new CustomerBo();
        //AdvisorVo advisorVo;
        //UserVo userVo;
        //RMVo rmVo = new RMVo();
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " GetCustomerId();", true);

        //    userVo = (UserVo)Session[SessionContents.UserVo];
        //    if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
        //        advisorVo = (AdvisorVo)Session["advisorVo"];
        //    if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
        //        rmVo = (RMVo)Session[SessionContents.RmVo];
        //    if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
        //    {
        //        txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
        //        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
        //        AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
        //        AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";
        //        //AutoCompleteExtender2.ContextKey = advisorVo.advisorId.ToString();
        //        //AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetails";
        //    }
        //    else if (Session[SessionContents.CurrentUserRole].ToString() == "BM")
        //    {
        //        txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        //        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetBMIndividualCustomerNames";

        //    }
        //    else if (Session[SessionContents.CurrentUserRole].ToString() == "RM")
        //    {
        //        txtCustomerName_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
        //        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetMemberCustomerName";
        //    }
        //    else if (Session[SessionContents.CurrentUserRole].ToString() == "Associates")
        //    {
        //        txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
        //        txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";
        //        AutoCompleteExtender1.ContextKey = advisorVo.advisorId.ToString();
        //        AutoCompleteExtender1.ServiceMethod = "GetAdviserCustomerPan";
        //        //AutoCompleteExtender2.ContextKey = associateuserheirarchyVo.AgentCode;
        //        //AutoCompleteExtender2.ServiceMethod = "GetAgentCodeAssociateDetailsForAssociates";

        //    }
        //   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " GetCustomerId();", true);
        //}
        //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}
        //protected void ddlIssuer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}
        //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}

        //protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}

        //protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        //protected void //sai  txtOrderDate_DateChanged(object sender, EventArgs e)
        //{
        //}

        //protected void txtApplicationDt_DateChanged(object sender, EventArgs e)
        //{
        //}
        //protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
        //    {
        //        //trJointHoldersList.Visible = false;
        //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
        //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
        //        ////sai             ddlAMCList..Enabled = true;
        //        customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
        //        Session["customerVo"] = customerVo;
        //        txtBranchName.Text = customerVo.BranchName;
        //        lblRM.Text = customerVo.RMName;
        //        lblgetPan.Text = customerVo.PANNum;
        //        //hdnCustomerId.Value = txtCustomerId.Value;
        //        //customerId = int.Parse(txtCustomerId.Value);
        //        // if (ddlsearch.SelectedItem.Value == "2")
        //        //     lblgetcust.Text = customerVo.FirstName + ' ' + customerVo.MiddleName + ' ' + customerVo.LastName;

        //        //= rmVo.FirstName + ' ' + rmVo.MiddleName + ' ' + rmVo.LastName;
        //        // BindBank(customerId);
        //        //BindPortfolioDropdown(customerId);
        //        // ddltransType.SelectedIndex = 0;
        //        //  BindISAList();
        //    }
        //}

        //  protected void rgvOrderSteps_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["OrderDetails"];
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem dataItem = e.Item as GridDataItem;
        //        //((Literal)dataItem["DropDownColumnStatus"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();
        //        //((Literal)dataItem["DropDownColumnStatusReason"].Controls[0]).Text = dataItem.GetDataKeyValue("WOS_OrderStepCode").ToString();

        //        TemplateColumn tm = new TemplateColumn();
        //        Label lblStatusCode = new Label();
        //        Label lblOrderStep = new Label();
        //        LinkButton editButton = dataItem["EditCommandColumn"].Controls[0] as LinkButton;
        //        Label lblOrderStatus = new Label();
        //        Label lblOrderStatusReason = new Label();
        //        lblStatusCode = (Label)e.Item.FindControl("lblStatusCode");
        //        lblOrderStep = (Label)e.Item.FindControl("lblOrderStepCode");
        //        lblOrderStatus = (Label)e.Item.FindControl("lblOrderStatus");
        //        lblOrderStatusReason = (Label)e.Item.FindControl("lblOrderStatusReason");
        //        Label lblCMFOS_Date = (Label)e.Item.FindControl("lblCMFOS_Date");
        //        if (lblOrderStep.Text.Trim() == "IP")
        //        {
        //            if (lblStatusCode.Text == "OMIP")
        //            {
        //                editButton.Text = "Mark as Pending";
        //                result = mfOrderBo.MFOrderAutoMatch(orderVo.OrderId, mforderVo.SchemePlanCode, mforderVo.accountid, mforderVo.TransactionCode, orderVo.CustomerId, mforderVo.Amount, orderVo.OrderDate);
        //                if (result == true)
        //                {
        //                    editButton.Text = "";
        //                    lblOrderStatusReason.Text = "";
        //                }

        //            }

        //            else if (lblStatusCode.Text == "OMPD")
        //            {
        //                editButton.Text = "Mark as InProcess";
        //            }

        //        }
        //        else if (lblOrderStep.Text.Trim() == "PR")
        //        {
        //            if (result == true)
        //            {
        //                lblOrderStatus.Text = "Executed";
        //                lblOrderStatusReason.Text = "Order Confirmed";
        //            }
        //            else
        //            {
        //                lblOrderStatus.Text = "";
        //                lblOrderStatusReason.Text = "";
        //                lblCMFOS_Date.Text = "";
        //            }
        //            editButton.Text = "";
        //        }
        //        else
        //        {
        //            lblOrderStatusReason.Text = "";
        //            editButton.Text = "";
        //        }

        //        //string editColumn = dataItem["COS_IsEditable"].Text;
        //        //if (editColumn == "1")
        //        //{
        //        //    editButton.Enabled = true;
        //        //}
        //        //else
        //        //{
        //        //    editButton.Enabled = false;
        //        //}
        //    }
        //}
         public void BindOrderStepsGrid()
        {
            DataSet dsOrderSteps = new DataSet();
            DataTable dtOrderDetails;
            pnlOrderSteps.Visible = true;
            //SetControls(false);
            dsOrderSteps = orderbo.GetOrderStepsDetails(orderId);
            dtOrderDetails = dsOrderSteps.Tables[0];
            //if (dtOrderDetails.Rows.Count == 0)
            //{
            //    lblPickNominee.Text = "You have not placed any Order";
            //}
            //else
            //{
            //lblPickNominee.Text = "Order Steps";
            rgvOrderSteps.DataSource = dtOrderDetails;
            rgvOrderSteps.DataBind();
            Session["OrderDetails"] = dtOrderDetails;
            //}
        }
        protected void rgvOrderSteps_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.rgvOrderSteps.DataSource = (DataTable)Session["OrderDetails"];
        }

        protected void rgvOrderSteps_ItemCommand(object source, GridCommandEventArgs e)
        {
            bool bResult = false;
            if (e.CommandName == "Update")
            {
                GridEditableItem edititem = e.Item as GridEditableItem;
                GridEditFormItem editform = (GridEditFormItem)e.Item;


                RadComboBox rcStatus = edititem.FindControl("ddlCustomerOrderStatus") as RadComboBox;
                RadComboBox rcPendingReason = edititem.FindControl("ddlCustomerOrderStatusReason") as RadComboBox;

                DataTable dt = new DataTable();
                dt = (DataTable)Session["OrderDetails"];

                string orderStepCode = dt.Rows[e.Item.ItemIndex]["WOS_OrderStepCode"].ToString().Trim();
                orderId = int.Parse(dt.Rows[e.Item.ItemIndex]["CO_OrderId"].ToString().Trim());
                string updatedStatus = rcStatus.SelectedValue;
                string updatedReason = rcPendingReason.SelectedValue;


                bResult = orderbo.UpdateOrderStep(updatedStatus, updatedReason, orderId, orderStepCode);
                if (bResult == true)
                {
                    rgvOrderSteps.Controls.Add(new LiteralControl("<strong>Successfully Updated</strong>"));
                }
                else
                {
                    rgvOrderSteps.Controls.Add(new LiteralControl("<strong>Unable to update value</strong>"));
                    e.Canceled = true;
                }
                BindOrderStepsGrid();
            }
        }

        protected void rcStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox rcStatus = (RadComboBox)o;
            GridEditableItem editedItem = rcStatus.NamingContainer as GridEditableItem;
            RadComboBox rcPendingReason = editedItem.FindControl("rcbPendingReason") as RadComboBox;
            string statusOrderCode = rcStatus.SelectedValue;
            BindRadComboBoxPendingReason(rcPendingReason, statusOrderCode);
        }

        // View and edit

        private void SetParameters()
        {
            //if (ddlBranch.SelectedIndex != 0)
            //    hdnBranchId.Value = ddlBranch.SelectedValue;
            //else
            //    hdnBranchId.Value = "";

            //if (ddlRM.SelectedIndex != 0)
            //    hdnRMId.Value = ddlRM.SelectedValue;
            //else
            //    hdnRMId.Value = "";

            //if (txtFromDate.SelectedDate.ToString() != "")
            //    hdnFromdate.Value = DateTime.Parse(txtFromDate.SelectedDate.ToString()).ToString();
            //else
            //    hdnFromdate.Value = DateTime.MinValue.ToString();

            //if (txtToDate.SelectedDate.ToString() != "")
            //    hdnTodate.Value = DateTime.Parse(txtToDate.SelectedDate.ToString()).ToString();
            //else
            //    hdnTodate.Value = DateTime.MinValue.ToString();

            //if (ddlOrderStatus.SelectedIndex != 0)
            //{
            //    hdnOrderStatus.Value = ddlOrderStatus.SelectedValue.ToString();
            //}
            //else
            //{
            //    hdnOrderStatus.Value = "OMIP";
            //}
        }
        //private void SetParameterSubbroker()
        //{
            //if (userType == "advisor" || userType == "rm" || userType == "bm")
            //{
            //    hdnAgentCode.Value = "0";
            //    hdnAgentId.Value = "0";
            //    if (ddlBrokerCode.SelectedIndex != 0)
            //    {
            //        hdnSubBrokerCode.Value = ddlBrokerCode.SelectedItem.Value.ToString();
            //        ViewState["SubBrokerCode"] = hdnSubBrokerCode.Value;
            //    }
            //    else
            //    {
            //        hdnSubBrokerCode.Value = "0";
            //    }
            //}
            //else if (userType == "associates")
            //{

            //    if (ddlBrokerCode.SelectedIndex != 0)
            //    {
            //        hdnAgentCode.Value = ddlBrokerCode.SelectedItem.ToString();
            //        hdnAgentId.Value = ddlBrokerCode.SelectedItem.Value.ToString();

            //    }
            //    else
            //    {
            //        hdnAgentCode.Value = AgentCode;
            //        hdnAgentId.Value = "0";
            //        //AgentId = int.Parse(ddlBrokerCode.SelectedItem.Value.ToString());
            //    }
                //AgentCode = ddlBrokerCode.SelectedValue.ToString();

        //    }
            
        //}
        protected void BindGvOrderList()
        {
            //  if (userType != "associates")
            // {
            //SetParameters();
            //SetParameterSubbroker();
            // }
            DataTable dtOrder = new DataTable();
            dtOrder = fiorderBo.GetOrderList(advisorVo.advisorId, "", "", Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), "", hdnCustomerId.Value,  "FI", userType, 0, "", "");

            //dtOrder = fiorderBo.GetOrderList(advisorVo.advisorId, "", "", Convert.ToDateTime(hdnTodate.Value), Convert.ToDateTime(hdnFromdate.Value), hdnOrderStatus.Value, hdnCustomerId.Value, hdnOrderType.Value, userType, int.Parse(hdnAgentId.Value), hdnSubBrokerCode.Value, hdnAgentCode.Value);
            if (dtOrder.Rows.Count > 0)
            {
               // trExportFilteredDupData.Visible = true;
                if (Cache["OrderList" + advisorVo.advisorId] == null)
                {
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
                }
                else
                {
                    Cache.Remove("OrderList" + advisorVo.advisorId);
                    Cache.Insert("OrderList" + advisorVo.advisorId, dtOrder);
                }
                gvOrderList.DataSource = dtOrder;
                gvOrderList.DataBind();
                gvOrderList.Visible = true;
                //ErrorMessage.Visible = false;
                //tblMessage.Visible = false;
                //pnlOrderList.Visible = true;
                //  btnExportFilteredDupData.Visible = true;
            }
            else
            {
                gvOrderList.Visible = false;
                //tblMessage.Visible = true;
                //ErrorMessage.Visible = true;
                //btnExportFilteredDupData.Visible = false;
                //ErrorMessage.InnerText = "No Records Found...!";
                //pnlOrderList.Visible = false;
            }
        }



        protected void gvOrderList_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
          //  trExportFilteredDupData.Visible = true;
            DataTable dtGIDetails = new DataTable();
            dtGIDetails = (DataTable)Cache["OrderList" + advisorVo.advisorId];
            gvOrderList.Visible = true;
            this.gvOrderList.DataSource = dtGIDetails;
        }

        protected void gvOrderList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {

                GridDataItem item = (GridDataItem)e.Item;
                string orderId = item.GetDataKeyValue("CO_OrderId").ToString();
                string customerId = item.GetDataKeyValue("C_CustomerId").ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LifeInsuranceOrderEntry", "loadcontrol('LifeInsuranceOrderEntry','?strOrderId=" + orderId + "&strCustomerId=" + customerId + " ');", true);
            }
        }
        protected void gvOrderList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (e.Item as GridDataItem);
                RadComboBox rcb = new RadComboBox();
                TemplateColumn tm = new TemplateColumn();
                RadComboBox lbl = new RadComboBox();
                if (userType == "bm")
                {

                    rcb = (RadComboBox)e.Item.FindControl("ddlMenu");
                    if (rcb != null)
                    {
                        rcb.Items.FindItemByValue("Edit").Remove();
                    }
                }


            }
        }

        private void OnTaxStatus( )
        {
 
            if (!string.IsNullOrEmpty(txtCustomerId.Value))
            {

                txtTax.Text = fiorderBo.GetTaxStatus(Convert.ToInt32 (txtCustomerId.Value));
            }

        }








    }
}