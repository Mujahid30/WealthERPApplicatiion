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

namespace WealthERP.OPS
{
    public partial class MFOrderEntry : System.Web.UI.UserControl
    {
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
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
        List<DataSet> applicationNoDup = new List<DataSet>();
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
        SystematicSetupVo systematicSetupVo = new SystematicSetupVo();

        protected void Page_Load(object sender, EventArgs e)
        {



            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
            SessionBo.CheckSession();

            associatesVo = (AssociatesVO)Session["associatesVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            //orderNumber = mfOrderBo.GetOrderNumber();
            //orderNumber = orderNumber + 1;
            //lblGetOrderNo.Text = orderNumber.ToString();
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

            //CheckDates();
            //txtSearchScheme.Visible = false;
            //CVPaymentdate2.ValueToCompare = txtOrderDate.SelectedDate.ToString();
            if (!IsPostBack)
            {
                Sipvisblity("", "");

                //CVPaymentdate2.ValueToCompare = txtOrderDate.SelectedDate.ToString();
                //orderNumber = mfOrderBo.GetOrderNumber();
                //orderNumber = orderNumber + 1;
                lblGetOrderNo.Text = orderNumber.ToString();
                if (AgentCode != null)
                {
                    txtAssociateSearch.Text = AgentCode;
                    OnAssociateTextchanged(this, null);
                }

                gvJointHoldersList.Visible = false;
                BindARNNo(advisorVo.advisorId);
                //BindAgentDropList(userType);
                //BindSearchDropdownList(sender, e);
                hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();

                if (hdnIsSubscripted.Value == "True")
                {
                    trIsa.Visible = true;
                    trJointHoldersList.Visible = true;
                    //document.getElementById("<%= trIsa.ClientID %>").style.visibility = 'visible';
                    //document.getElementById("<%= trJointHoldersList.ClientID %>").style.visibility = 'visible';

                }
                else
                {
                    trIsa.Visible = false;
                    trJointHoldersList.Visible = false;
                    //document.getElementById("<%= trIsa.ClientID %>").style.visibility = 'collapse';
                    //document.getElementById("<%= trJointHoldersList.ClientID %>").style.visibility = 'collapse';

                }
                trpan.Visible = false;
                trCust.Visible = false;
                //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowInitialIsa();", true);

                ddlAMCList.Enabled = false;
                if (userType == "associates")
                {
                    //BindSubBrokerAgentCode(AgentCode);
                }

                //  CompareValidator6.ValueToCompare = hdnAplicationNo.Value;

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
                //  txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = txtSchemeCode.Value;
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
                //sai cvAppRcvDate.ValueToCompare = DateTime.Today.ToShortDateString();
                cvFutureDate1.ValueToCompare = DateTime.Today.ToShortDateString();
                BindAMC(0);
                BindScheme(0);
                Sflag = 0;
                //BindFolioNumber(0);
                //BindOrderStatus();
                BindCategory();
                BindState();
                BindFrequency();
                ShowHideFields(1);
                ShowTransactionType(0);


                // dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));


                if (Request.QueryString["action"] != null)
                {
                    txtCustomerId.Value = mforderVo.CustomerId.ToString();
                    hdnCustomerId.Value = mforderVo.CustomerId.ToString();
                    lnlBack.Visible = true;
                    ViewForm = Request.QueryString["action"].ToString();
                    txtOrderDate.SelectedDate = orderVo.OrderDate;
                    lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    btnViewInPDFNew.Visible = false;
                    btnViewInDOCNew.Visible = false;
                    BindISAList();
                }

                if (mforderVo != null && orderVo != null)
                {

                    if (ViewForm == "View")
                    {
                        SetControls("View", mforderVo, orderVo);
                        btnAddMore.Visible = false;
                        ddlsearch.SelectedValue = "1";
                        lblOrderNumber.Visible = true;
                        lblGetOrderNo.Visible = true;
                        // Sipvisblity(hdnType.Value, "View");

                    }
                    else if (ViewForm == "Edit")
                    {
                        SetControls("Edit", mforderVo, orderVo);
                        btnAddMore.Visible = false;
                        ddlsearch.SelectedValue = "1";
                        lblOrderNumber.Visible = true;
                        lblGetOrderNo.Visible = true;
                        //  Sipvisblity(hdnType.Value, "Edit");
                    }
                    else if (ViewForm == "entry")
                    {
                        SetControls("Entry", mforderVo, orderVo);
                    }
                    else
                    {
                        //cvAppRcvDate.ValueToCompare = DateTime.Now.ToShortDateString();
                        //cvOrderDate.ValueToCompare = DateTime.Now.ToShortDateString();
                        //cvFutureDate1.ValueToCompare = DateTime.Now.ToShortDateString();
                    }
                    if (mforderVo.OrderStatusCode.ToUpper() == "RJ".ToUpper())
                    {
                        lnkBtnEdit.Visible = false;
                    }
                    else
                    {
                        lnkBtnEdit.Visible = true;

                    }
                }
                else
                {
                    SetControls("Entry", mforderVo, orderVo);

                }

                if (string.IsNullOrEmpty(ViewForm))
                    RadDateControlBusinessDateValidation(ref txtReceivedDate, 3, DateTime.Now, 1);

                //ddlsearch_Selectedindexchanged(this, null);

            }
            Session["MForderToCustomer"] = "Customer";
            //bindSearchScheme();

            //    applicationNoDup = mfOrderBo.AplicationNODuplicates();
            //     int result = applicationNoDup.Find(item => item > 20);

            // if (txtReceivedDate.SelectedDate==DateTi)
            //txtReceivedDate.SelectedDate = DateTime.Now;
            //ShowHideFields(1);
        }

        //private void RadDateControlBusinessDateValidation(ref RadDatePicker txtReceivedDate, int p, DateTime dateTime, int p_4)
        //{
        //    throw new NotImplementedException();
        //}
        private void bindSearchScheme()
        {
            try
            {
                DataSet dsScheme = new DataSet();
                DataTable dtScheme;

                String parameters = string.Empty;
                //parameters = ddlAMCList.SelectedValue + '/' + ddlCategory.SelectedValue + '/' + 1 + '/' + txtCustomerId.Value;

                if (ddlAMCList.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue.ToString());
                    categoryCode = ddlCategory.SelectedValue;
                    if (txtCustomerId.Value == "")
                    {
                        parameters = string.Empty;
                        parameters = (amcCode + "/" + categoryCode + "/" + 1 + "/" + 1);
                        txtSearchScheme_autoCompleteExtender.ContextKey = parameters;
                        txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeName";

                        //   dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 1, 1);

                    }
                    else
                    {
                        parameters = string.Empty;
                        parameters = (amcCode + "/" + categoryCode + "/" + 1 + "/" + 1);
                        txtSearchScheme_autoCompleteExtender.ContextKey = parameters;
                        txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeName";

                    }

                    // txtSearchScheme_autoCompleteExtender.ContextKey = parameters;
                    //else
                    //{
                    //    parameters = string.Empty;
                    //    parameters = amcCode + "/" + categoryCode + "/" + Sflag + "/" + txtCustomerId.Value;
                    //    txtSearchScheme_autoCompleteExtender.ContextKey = parameters;
                    //    txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeForOrderEntry";

                    //    //  dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));
                    //}
                }
                else if (ddlAMCList.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue.ToString());
                    categoryCode = ddlCategory.SelectedValue;
                    if (Sflag == 0)
                    {
                        parameters = string.Empty;
                        parameters = amcCode.ToString() + "/" + categoryCode + "/" + "0" + "/" + "1";
                        txtSearchScheme_autoCompleteExtender.ContextKey = parameters;
                        txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeName";

                        //txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeList";
                        //    dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 0, 1);

                    }
                    else
                    {
                        parameters = string.Empty;
                        parameters = amcCode + "/" + categoryCode + "/" + Sflag + "/" + txtCustomerId.Value;
                        txtSearchScheme_autoCompleteExtender.ContextKey = parameters;
                        txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeForOrderEntry";
                        //dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));
                    }
                }
                else if (ddlAMCList.SelectedIndex == 0)
                {
                    parameters = string.Empty;
                    parameters = amcCode.ToString() + "/" + categoryCode + "/" + "0" + "/" + "1";
                    txtSearchScheme_autoCompleteExtender.ContextKey = parameters;
                    txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeName";

                }
                //if (dsScheme.Tables.Count > 0)
                //{
                //    dtScheme = dsScheme.Tables[0];
                //    ddlAmcSchemeList.DataSource = dtScheme;
                //    ddlAmcSchemeList.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                //    ddlAmcSchemeList.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                //    ddlAmcSchemeList.DataBind();
                //    ddlAmcSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
                //}
                //else
                //{
                //    ddlAmcSchemeList.Items.Clear();
                //    ddlAmcSchemeList.DataSource = null;
                //    ddlAmcSchemeList.DataBind();
                //    ddlAmcSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }


            //if (ddlAMCList.SelectedIndex > 0 & ddlCategory.SelectedIndex >= 0)
            //{
            //    string parameters;
            //    parameters = ddlAMCList.SelectedValue + '/'+ddlCategory.SelectedValue + '/'+1 + '/'+txtCustomerId.Value;

            //    txtSearchScheme_autoCompleteExtender.ContextKey = parameters;


            //    txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeName";
            //}

        }
        private void CheckDates()
        {
            DateTime dt = orderbo.GetServerTime();
            ///RadCalendarDay calendarDay = new RadCalendarDay();
            RadDatePicker Rdt = new RadDatePicker();
            Rdt.DbSelectedDate = dt;

            //txtReceivedDate.SelectedDate = Convert.ToDateTime(Rdt.SelectedDate);
            //txtReceivedDate.FocusedDate = Convert.ToDateTime(Rdt.SelectedDate);



            //txtOrderDate.MaxDate = Convert.ToDateTime(Rdt.SelectedDate);
            //txtOrderDate.SelectedDate = Convert.ToDateTime(Rdt.SelectedDate);
            //txtOrderDate.FocusedDate = Convert.ToDateTime(Rdt.SelectedDate);

            //DateTime recDt = txtReceivedDate.SelectedDate.Value ;
            //txtOrderDate.SelectedDate = recDt.AddDays(3);
            //txtOrderDate.Enabled = false;
            //   calendarDay.ItemStyle.BackColor = System.Drawing.Color.Red ;
            // calendarDay.Date = dt.Date;
            //  txtOrderDate.Calendar.SpecialDays.Add(calendarDay);



            txtPaymentInstDate.SelectedDate = Convert.ToDateTime(Rdt.SelectedDate);
            txtPaymentInstDate.FocusedDate = Convert.ToDateTime(Rdt.SelectedDate);



            //txtReceivedDate.Calendar.SpecialDays.Add(calendarDay);
            //txtOrderDate.Calendar.SpecialDays.Add(calendarDay);
        }

        protected void txtSchemeCode_ValueChanged(object sender, EventArgs e)
        {
            schemePlanCode = int.Parse(txtSchemeCode.Value);
            if (schemePlanCode != 0)
            {
                string transactionType = "";
                int schemeCode = 0;
                string FileName = "";
                //if (ddlAmcSchemeList.SelectedIndex != 0)
                //{
                // schemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
                //Dbt  hdnSchemeCode.Value = ddlAmcSchemeList.SelectedValue.ToString();
                //dbt  hdnSchemeName.Value = ddlAmcSchemeList.SelectedItem.Text;
                hdnSchemeCode.Value = schemePlanCode.ToString();
                categoryCode = productMFBo.GetCategoryNameFromSChemeCode(schemePlanCode);

                txtSearchScheme.Text = productMFBo.GetSChemeName(schemePlanCode);
                ddlCategory.SelectedValue = categoryCode;
                if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                {
                    if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
                    {
                        BindFolioNumberSearch(0);
                        DataSet dsGetAmountUnits;
                        DataTable dtGetAmountUnits;
                        dsGetAmountUnits = operationBo.GetAmountUnits(schemePlanCode, int.Parse(txtCustomerId.Value));
                        if (dsGetAmountUnits.Tables[0] != null)
                        {
                            if ((dsGetAmountUnits.Tables[0].Rows.Count > 0))
                            {
                                dtGetAmountUnits = dsGetAmountUnits.Tables[0];
                                lblGetAvailableAmount.Text = dtGetAmountUnits.Rows[0]["CMFNP_CurrentValue"].ToString();
                                lblGetAvailableUnits.Text = dtGetAmountUnits.Rows[0]["CMFNP_NetHoldings"].ToString();
                            }
                        }
                        if ((ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "Switch") && ddlAMCList.SelectedIndex != 0)
                        {
                            BindSchemeSwitch();
                        }
                    }
                    //}

                }
                else
                    BindFolioNumberSearch(0);
            }
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


        protected void imgAddBank_OnClick(object sender, EventArgs e)
        {
            // window.open('PopUp.aspx?AddMFCustLinkId=mf&pageID=CustomerType&', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('PopUp.aspx?PageId=AddBankAccount', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no');", true);
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank(customerVo.CustomerId);
        }

        protected void imgBtnRefereshBank_OnClick(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank(customerVo.CustomerId);
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
                    ddltransType.SelectedIndex = 0;
                    //sai  txtReceivedDate.SelectedDate = null;
                    txtApplicationNumber.Text = "";
                    ddlAMCList.SelectedIndex = 0;
                    ddlCategory.SelectedIndex = 0;
                    txtSchemeCode.Value = "0";
                    ddlAmcSchemeList.SelectedIndex = 0;
                    //ddlPortfolio.SelectedIndex = -1;
                    txtFolioNumber.Text = "";
                    txtOrderDate.SelectedDate = null;
                    lblGetOrderNo.Text = "";
                    //ddlOrderPendingReason.SelectedIndex = 0;
                    //ddlOrderStatus.SelectedIndex = 0;
                    txtFutureDate.SelectedDate = null;
                    txtFutureTrigger.Text = "";
                    txtAmount.Text = "";
                    // ddlPaymentMode.SelectedIndex = 0;
                    ddlPaymentMode_SelectedIndexChanged(this, null);
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
                    //    ddlAssociate.SelectedIndex = 0;
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
                        else
                            lblAssociatetext.Text = string.Empty;

                    }
                    if (mforderVo.ARNNo != null)
                        trCust.Visible = true;
                    ddlARNNo.SelectedItem.Value = mforderVo.ARNNo;
                    orderId = orderVo.OrderId;

                    //sai ddlsearch.Enabled = true;
                    //ddlAmcSchemeList.Text
                    txtAssociateSearch.Enabled = true;
                    txtCustomerName.Enabled = true;
                    txtCustomerName.Visible = true;
                    txtCustomerName.Text = mforderVo.CustomerName;
                    lblgetPan.Visible = true;

                    if (orderVo.CustomerId != 0)
                        hdnCustomerId.Value = orderVo.CustomerId.ToString();
                    BindPortfolioDropdown(orderVo.CustomerId);
                    customerVo = customerBo.GetCustomer(orderVo.CustomerId);
                    lblGetBranch.Text = mforderVo.BMName;
                    lblGetRM.Text = mforderVo.RMName;
                    lblgetPan.Text = mforderVo.PanNo;
                    ddltransType.SelectedValue = mforderVo.TransactionCode;
                    txtOrderDate.SelectedDate = orderVo.OrderDate;

                    lblGetOrderNo.Text = orderVo.OrderNumber.ToString();
                    hdnType.Value = mforderVo.TransactionCode;
                    txtFolioNumber.Text = mforderVo.FolioNumber;
                    hidFolioNumber.Value = mforderVo.accountid.ToString();
                    if (mforderVo.SchemePlanSwitch != 0)
                    {
                        ddlSchemeSwitch.SelectedValue = mforderVo.SchemePlanSwitch.ToString();
                    }
                    if (ddltransType.SelectedValue == "CAF")
                    {
                        ShowTransactionType(3);
                        lblAMC.Visible = false; ddlAMCList.Visible = false;
                        lblCategory.Visible = false; ddlCategory.Visible = false;
                        lblSearchScheme.Visible = false;
                        ddlAmcSchemeList.Visible = false;
                        lblFolioNumber.Visible = false;
                        txtFolioNumber.Visible = false;
                        spnAMC.Visible = false; spnScheme.Visible = false;
                        txtSearchScheme.Visible = false;
                        Span9.Visible = false;
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
                        ddlAMCList.SelectedValue = mforderVo.Amccode.ToString();
                        BindCategory();
                        ddlCategory.SelectedValue = mforderVo.category;
                        BindPortfolioDropdown(orderVo.CustomerId);
                        ddlPortfolio.SelectedItem.Value = mforderVo.portfolioId.ToString();
                        BindScheme(0);
                        Sflag = 0;
                        txtSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                        ddlAmcSchemeList.SelectedItem.Value = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();


                    }
                    portfolioId = mforderVo.portfolioId;
                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "ABY")
                    {
                        ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;


                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        ShowTransactionType(1);
                        trFrequency.Visible = true;

                        //trSystematicDateChk1.Visible = true;
                        //trSystematicDateChk2.Visible = true;
                        //trSystematicDateChk3.Visible = true;
                        //trSystematicDate.Visible = true;
                        //trRegistrationDate.Visible = true;
                        //trCeaseDate.Visible = true;

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
                    else if (ddltransType.SelectedValue == "SWB")
                    {
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;

                        BindSchemeSwitch();
                        ddlSchemeSwitch.SelectedValue = mforderVo.SchemePlanSwitch.ToString();
                        ShowTransactionType(2);
                        trScheme.Visible = true;

                    }
                    else if (ddltransType.SelectedValue == "STB")
                    {
                        ShowTransactionType(2);
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        trScheme.Visible = true;


                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;


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
                    else if (ddltransType.SelectedValue == "Sel")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                        trPINo.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;


                        if (advisorVo.A_AgentCodeBased == 1)
                        {
                            trGetAmount.Visible = false;
                            // trRedeemed.Visible = false;
                        }
                        else
                        {
                            trGetAmount.Visible = false;
                            //  trRedeemed.Visible = false;
                        }
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;

                        //trSystematicDateChk1.Visible = true;
                        //trSystematicDateChk2.Visible = true;
                        //trSystematicDateChk3.Visible = true;
                        //trSystematicDate.Visible = true;
                        //trRegistrationDate.Visible = true;
                        //trCeaseDate.Visible = true;

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
                    if (ddltransType.SelectedValue == "SIP" || ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "CAF")
                    {
                        //BindFolioNumber(0);

                        //ListItem li = ddlFolioNumber.Items.FindByValue(mforderVo.accountid.ToString());
                        //if (li != null)
                        //{
                        txtFolioNumber.Text = mforderVo.FolioNumber;

                        // value found
                        //}


                        //sai txtFolioNumber.Text= mforderVo.accountid.ToString();

                        //else
                        //    txtFolioNumber.Text= "";
                    }
                    else
                    {
                        BindFolioNumberSearch(0);
                        //ListItem li = ddlFolioNumber.Items.FindByValue(mforderVo.accountid.ToString());
                        //if (li != null)
                        //{
                        txtFolioNumber.Text = mforderVo.FolioNumber;

                        // value found
                        //}
                        //  txtFolioNumber.Text= mforderVo.accountid.ToString();

                        //else
                        //    txtFolioNumber.Text= "";
                    }
                    txtReceivedDate.Enabled = false;
                    if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    {
                        txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
                    }

                    else
                        txtReceivedDate.SelectedDate = DateTime.Now;
                    txtApplicationNumber.Enabled = false;
                    txtApplicationNumber.Text = orderVo.ApplicationNumber;
                    txtOrderDate.SelectedDate = orderVo.OrderDate;
                    lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();

                    if (mforderVo.FutureExecutionDate != DateTime.MinValue)
                        txtFutureDate.SelectedDate = mforderVo.FutureExecutionDate;
                    else
                        txtFutureDate.SelectedDate = null;
                    if (mforderVo.IsImmediate == 1)
                        rbtnImmediate.Checked = true;
                    else
                    {
                        rbtnFuture.Checked = true;
                        trfutureDate.Visible = true;
                    }
                    txtFutureTrigger.Text = mforderVo.FutureTriggerCondition;
                    txtAmount.Text = mforderVo.Amount.ToString();
                    if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                    {
                        if (mforderVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = mforderVo.Amount.ToString();
                        }
                        else
                        {
                            //rbtAmount.Checked = false;
                            //txtNewAmount.Text = "";
                        }

                        if (mforderVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = mforderVo.Units.ToString();
                        }
                        else
                        {
                            //rbtUnit.Checked = true;
                            //txtNewAmount.Text = "";
                        }

                        //lblGetAvailableAmount.Text = operationVo.Amount.ToString();
                        //lblGetAvailableUnits.Text = operationVo.Units.ToString();
                        //sai   lblAvailableAmount.Visible = false;
                        //sai   lblAvailableUnits.Visible = false;
                    }


                    ListItem liPayMode = ddlPaymentMode.Items.FindByValue(orderVo.PaymentMode.ToString());
                    if (liPayMode != null)
                    {
                        ddlPaymentMode.SelectedValue = orderVo.PaymentMode;
                        ddlPaymentMode_SelectedIndexChanged(this, null);

                        // value found
                    }
                    //else
                    //{
                    //    ddlPaymentMode.SelectedIndex = 0;

                    //    //Value not found
                    //}


                    txtPaymentNumber.Text = orderVo.ChequeNumber;




                    if (orderVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = orderVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate = null;
                    BindBank(orderVo.CustomerId);

                    ListItem liBank = ddlBankName.Items.FindByValue(orderVo.CustBankAccId.ToString());
                    if (liBank != null)
                    {
                        ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();

                        // value found
                    }
                    //else
                    //{
                    //    ddlBankName.SelectedIndex=-1;

                    //    //Value not found
                    //}
                    //if (orderVo.CustBankAccId != 0)
                    //  //  ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();
                    //else
                    //BindBank(orderVo.CustomerId);
                    //if (orderVo.CustBankAccId != 0)
                    //    ddlBankName.SelectedItem.Value = orderVo.CustBankAccId.ToString();
                    //else
                    //    ddlBankName.SelectedValue = "Select";

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

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    //trReportButtons.Visible = true;
                    btnImgAddCustomer.Visible = false;
                    btnAddMore.Visible = false;
                    rgvOrderSteps.Visible = true;
                    rgvOrderSteps.Enabled = true;
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
                    txtSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                    txtSchemeCode_ValueChanged(this, null);
                    lnlBack.Visible = true;
                    lnkDelete.Visible = true;
                    txtFolioNumber.Text = mforderVo.FolioNumber;

                    if (ddltransType.SelectedValue == "SWB")
                    {
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;

                    }
                    if (ddltransType.SelectedValue == "BUY")
                    {
                        txtFolioNumber.Enabled = false;
                        imgFolioAdd.Enabled = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;
                    }

                }
                imgAddBank.Visible = true;
                imgBtnRefereshBank.Visible = true;

            }

            else if (action == "View")
            {
                if (mforderVo != null && orderVo != null)
                {
                    //sai ddlsearch.SelectedItem.Value = "0";
                    //sai ddlsearch.Enabled = false;
                   

                    txtAssociateSearch.Text = orderVo.AgentCode;
                    trCust.Visible = true;
                    //sai ddlsearch.SelectedItem.Value = "1";
                    Agentname = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                    if (Agentname.Rows.Count > 0)
                    {
                        lblAssociatetext.Text = Agentname.Rows[0][0].ToString();
                    }
                    //lblAssociatetext.Text;
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
                    ddltransType.Enabled = false;
                    ddltransType.SelectedValue = mforderVo.TransactionCode;
                    txtOrderDate.SelectedDate = orderVo.OrderDate;
                    lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
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
                    //BindARNNo(advisorVo.advisorId);
                    //ddlARNNo.SelectedValue = "";
                    if (mforderVo.ARNNo != null)
                        ddlARNNo.SelectedItem.Value = mforderVo.ARNNo;

                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "ABY")
                    {
                        ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;

                    }
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        ShowTransactionType(1);
                        trFrequency.Visible = true;
                        trSIPStartDate.Visible = true;
                        ddlFrequencySIP.Enabled = false;

                        //trSystematicDateChk1.Visible = true;
                        //trSystematicDateChk2.Visible = true;
                        //trSystematicDateChk3.Visible = true;
                        //trSystematicDate.Visible = true;
                        //trRegistrationDate.Visible = true;
                        //trCeaseDate.Visible = true;

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
                    else if (ddltransType.SelectedValue == "SWB")
                    {
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                        ShowTransactionType(2);
                        trScheme.Visible = true;


                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;



                    }
                    else if (ddltransType.SelectedValue == "STB")
                    {
                        ShowTransactionType(2);
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                        trScheme.Visible = true;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;


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
                    else if (ddltransType.SelectedValue == "Sel")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                        trPINo.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;


                        if (advisorVo.A_AgentCodeBased == 1)
                        {
                            trGetAmount.Visible = false;
                            //   trRedeemed.Visible = false;
                        }
                        else
                        {
                            trGetAmount.Visible = false;
                            // trRedeemed.Visible = false;
                        }
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;


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
                    if (ddltransType.SelectedValue == "CAF")
                    {
                        ShowTransactionType(3);
                        lblAMC.Visible = false; ddlAMCList.Visible = false;
                        lblCategory.Visible = false; ddlCategory.Visible = false;
                        lblSearchScheme.Visible = false;
                        ddlAmcSchemeList.Visible = false;
                        lblFolioNumber.Visible = false; txtFolioNumber.Visible = false;
                        spnAMC.Visible = false; spnScheme.Visible = false;
                        txtSearchScheme.Visible = false;
                        Span9.Visible = false;
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
                        ddlAMCList.SelectedValue = mforderVo.Amccode.ToString();
                        BindCategory();
                        ddlCategory.SelectedValue = mforderVo.category;
                        BindPortfolioDropdown(mforderVo.CustomerId);
                        ddlPortfolio.SelectedValue = mforderVo.portfolioId.ToString();
                        BindScheme(0);
                        Sflag = 0;
                        txtSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                        ddlAmcSchemeList.SelectedItem.Value = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                        BindSchemeSwitch();
                        ddlSchemeSwitch.SelectedValue = mforderVo.SchemePlanSwitch.ToString();
                    }
                    portfolioId = mforderVo.portfolioId;
                    if (ddltransType.SelectedValue == "SIP" || ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "CAF")
                    {
                        //BindFolioNumber(0);

                        txtFolioNumber.Text = mforderVo.FolioNumber;
                        //else
                        //    txtFolioNumber.Text= "";
                    }
                    else
                    {


                        //BindFolioNumberSearch(0);
                        txtFolioNumber.Text = mforderVo.FolioNumber;

                        //else
                        //    txtFolioNumber.Text= "";
                    }
                    txtReceivedDate.Enabled = false;
                    if (orderVo.ApplicationReceivedDate != DateTime.MinValue)
                    {

                        txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
                    }
                    else
                        txtReceivedDate.SelectedDate = DateTime.Now;
                    txtApplicationNumber.Enabled = false;
                    txtApplicationNumber.Text = orderVo.ApplicationNumber;
                    txtOrderDate.SelectedDate = orderVo.OrderDate;
                    lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();

                    if (mforderVo.IsImmediate == 1)
                        rbtnImmediate.Checked = true;
                    else
                    {
                        rbtnFuture.Checked = true;
                        trfutureDate.Visible = true;
                    }
                    if (mforderVo.FutureExecutionDate != DateTime.MinValue)
                        txtFutureDate.SelectedDate = mforderVo.FutureExecutionDate;
                    else
                        txtFutureDate.SelectedDate = null;
                    txtFutureTrigger.Text = mforderVo.FutureTriggerCondition;
                    txtAmount.Text = mforderVo.Amount.ToString();
                    if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                    {
                        if (mforderVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = mforderVo.Amount.ToString();
                        }
                        else
                        {
                            //rbtAmount.Checked = false;
                            //txtNewAmount.Text = "";
                        }

                        if (mforderVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = mforderVo.Units.ToString();
                        }
                        else
                        {
                            //  rbtUnit.Checked = true;
                            // txtNewAmount.Text = "";
                        }

                        lblAvailableAmount.Visible = false;
                        lblAvailableUnits.Visible = false;
                        //lblGetAvailableAmount.Text = operationVo.Amount.ToString();
                        //lblGetAvailableUnits.Text = operationVo.Units.ToString();
                    }

                    ddlPaymentMode.SelectedValue = orderVo.PaymentMode;
                    ddlPaymentMode_SelectedIndexChanged(this, null);
                    txtPaymentNumber.Text = orderVo.ChequeNumber;
                    if (orderVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = orderVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate = null;
                    BindBank(orderVo.CustomerId);
                    ListItem li = ddlBankName.Items.FindByValue(orderVo.CustBankAccId.ToString());
                    if (li != null)
                    {
                        ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();

                        // value found
                    }
                    else
                    {
                        ddlBankName.SelectedIndex = 0;
                    }
                    //if (orderVo.CustBankAccId != 0)
                    //    ddlBankName.SelectedValue = orderVo.CustBankAccId.ToString();
                    //else
                    //    ddlBankName.SelectedValue = "";
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

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = false;
                    //trReportButtons.Visible = true;
                    btnImgAddCustomer.Visible = false;
                    if (userType == "bm")
                        lnkBtnEdit.Visible = false;
                    else
                        lnkBtnEdit.Visible = true;
                    if (Request.QueryString["FromPage"] != null)
                    {
                        lnkBtnEdit.Visible = false;
                        lnlBack.Visible = true;
                    }
                    rgvOrderSteps.Visible = true;
                    rgvOrderSteps.Enabled = true;
                    BindOrderStepsGrid();
                    lnkDelete.Visible = false;
                    txtSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                    txtSchemeCode_ValueChanged(this, null);
                    //btnViewReport.Visible = true;
                    //btnViewInPDF.Visible = true;
                    //btnViewInDOC.Visible = true;
                    txtFolioNumber.Text = mforderVo.FolioNumber;
                    hidFolioNumber.Value = mforderVo.accountid.ToString();
                    if (ddltransType.SelectedValue == "SWB")
                    {
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                    }
                    if (ddltransType.SelectedValue == "BUY")
                    {
                        txtFolioNumber.Enabled = false;
                        imgFolioAdd.Enabled = false;
                    }

                }
                imgAddBank.Visible = false;
                imgBtnRefereshBank.Visible = false;
            }
            GetSipDetails(hdnType.Value);
            Sipvisblity(hdnType.Value, action);
            //sai ddlsearch.SelectedItem.Value = "1";
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

        private void GetSipDetails(string transactionType)
        {
            if (transactionType == "SIP" | transactionType == "SWP" | transactionType == "STB")
            {
                DataSet dsSip;

                dsSip = mfOrderBo.GetSipDetails(orderVo.OrderId);
                if (dsSip.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsSip.Tables[0].Rows)
                    {

                        //if ( txtSystematicdates.SelectedDate !=null )
                        //{
                        // //   txtSystematicdates.SelectedDate.Value.Day;
                        //}



                        txtPeriod.Text = dr["CMFSS_Tenure"].ToString();
                        txtSystematicdates.Text = dr["CMFSS_SystematicDate"].ToString();
                        ddlPeriodSelection.SelectedValue = dr["CMFSS_TenureCycle"].ToString();
                        txtRegistrationDate.Text = dr["CMFSS_RegistrationDate"].ToString();
                        txtCeaseDate.Text = dr["CMFSS_CEASEDATE"].ToString();





                    }
                }

            }



        }
        private void BindCategory()
        {
            try
            {
                DataSet dsSchemeCategory;
                DataTable dtSchemeCategory;
                dsSchemeCategory = priceBo.GetNavOverAllCategoryList();
                //----------------------------------------------Scheme Category Binding------------------------
                if (dsSchemeCategory.Tables.Count > 0)
                {
                    dtSchemeCategory = dsSchemeCategory.Tables[0];
                    ddlCategory.DataSource = dtSchemeCategory;
                    ddlCategory.DataValueField = dtSchemeCategory.Columns["Category_Code"].ToString();
                    ddlCategory.DataTextField = dtSchemeCategory.Columns["Category_Name"].ToString();
                    ddlCategory.DataBind();

                }
                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));

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
            dtFrequency = assetBo.GetFrequencyCode(path);
            ddlFrequencySIP.DataSource = dtFrequency;
            ddlFrequencySIP.DataTextField = "Frequency";
            ddlFrequencySIP.DataValueField = "FrequencyCode";
            ddlFrequencySIP.DataBind();
            //---------------------------------------------
            ddlFrequencySTP.DataSource = dtFrequency;
            ddlFrequencySTP.DataTextField = "Frequency";
            ddlFrequencySTP.DataValueField = "FrequencyCode";
            ddlFrequencySTP.DataBind();

        }
        public void ISA_Onclick(object obj, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerISARequest','');", true);
        }

        private void ShowHideFields(int flag)
        {
            if (flag == 0)
            {
                trTransactionType.Visible = false;
                trARDate.Visible = false;
                trAplNumber.Visible = false;
                trOrderDate.Visible = false;
                trOrderNo.Visible = false;
                //Sbi trOrderType.Visible = false;
                //trrejectReason.Visible = false;
                trfutureDate.Visible = false;
                rgvOrderSteps.Visible = false;
                lnkBtnEdit.Visible = false;
                lnlBack.Visible = false;
                btnUpdate.Visible = false;
                lnkDelete.Visible = false;
                btnViewReport.Visible = false;
                btnViewInPDF.Visible = false;
                btnViewInDOC.Visible = false;
                //ShowTransactionType(0);
            }
            else if (flag == 1)
            {
                trTransactionType.Visible = true;
                trARDate.Visible = true;
                trAplNumber.Visible = true;
                trOrderDate.Visible = true;
                trOrderNo.Visible = true;
                //Sbi trOrderType.Visible = true;
                //trrejectReason.Visible = false;
                trfutureDate.Visible = false;
                rgvOrderSteps.Visible = false;
                lnkBtnEdit.Visible = false;
                lnlBack.Visible = false;
                btnUpdate.Visible = false;
                lnkDelete.Visible = false;
                btnViewReport.Visible = false;
                btnViewInPDF.Visible = false;
                btnViewInDOC.Visible = false;
            }
        }

        protected void ShowTransactionType(int type)
        {
            if (type == 0)
            {
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

                // sip rows
                //sai    trSystematicDateChk1.Visible = false ;;
                // trSystematicDateChk2.Visible = false;
                // trSystematicDateChk3.Visible = false;
                // trSystematicDate.Visible = false;
                // trRegistrationDate.Visible = false;
                // trCeaseDate.Visible = false;



                trBtnSubmit.Visible = false;
            }
            else if (type == 1)
            {
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
                trAmount.Visible = false;
                trPINo.Visible = false;
                trBankName.Visible = false;
                trFrequency.Visible = false;
                trSIPStartDate.Visible = false;
                trAddress6.Visible = false;

                trSection2.Visible = false;

                if (advisorVo.A_AgentCodeBased == 1)
                {
                    trGetAmount.Visible = false;
                    // trRedeemed.Visible = false;
                }
                else
                {
                    trGetAmount.Visible = false;
                    //  trRedeemed.Visible = false;
                }
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

                //sai    trSystematicDateChk1.Visible = false ;;
                // trSystematicDateChk2.Visible = false;
                // trSystematicDateChk3.Visible = false;
                // trSystematicDate.Visible = false;
                // trRegistrationDate.Visible = false;
                // trCeaseDate.Visible = false;


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
                    //pnlJointholders.Visible = true;
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
            DataTable ISAList;
            if (!string.IsNullOrEmpty(txtCustomerId.Value))
            {
                ISAList = customerBo.GetISaList(customerId);
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
                    //pnlJointholders.Visible = true;
                    //trJointHoldersList.Visible = true;
                    ddlCustomerISAAccount.DataSource = ISAList;
                    ddlCustomerISAAccount.DataValueField = ISAList.Columns["CISAA_accountid"].ToString();
                    ddlCustomerISAAccount.DataTextField = ISAList.Columns["CISAA_AccountNumber"].ToString();
                    ddlCustomerISAAccount.DataBind();
                    ddlCustomerISAAccount.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    //trRegretMsg.Visible = false;

                    ddlCustomerISAAccount.Visible = true;
                }
                else
                {
                    // trJointHoldersList.Visible = false;
                    //pnlJointholders.Visible = false;
                    ddlCustomerISAAccount.Visible = true;
                    //trRegretMsg.Visible = true;

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
            //if (ddlsearch.SelectedValue == "2")
            //{
            //    if (lblgetcust.Text == "")
            //    {
            //        txtPansearch.Focus();

            //        txtAssociateSearch.Text = "";
            //        return;
            //    }
            //}
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
            //if (string.IsNullOrEmpty(txtAgentId.Value.ToString().Trim()))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Agentcode is not in existed list!');", true);

            //}
        }

        protected void HiddenField1_ValueChanged1(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["customerVo"];
            BindBank(customerVo.CustomerId);
        }


        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                //trJointHoldersList.Visible = false;
                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                ddlAMCList.Enabled = true;
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
                BindPortfolioDropdown(customerId);
                ddltransType.SelectedIndex = 0;
                BindISAList();
                btnreport.Visible = true;
                btnpdfReport.Visible = true;
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
                ClearAllFields();

            }
        }

        private void BindBank(int customerId)
        {
            DataSet dsBankName = mfOrderBo.GetCustomerBank(customerId);

            ddlBankName.Items.Clear();
            ddlBankName.DataSource = null;

            if (dsBankName.Tables[0].Rows.Count > 0)
            {
                ddlBankName.DataSource = dsBankName;
                ddlBankName.DataValueField = dsBankName.Tables[0].Columns["CB_CustBankAccId"].ToString();
                ddlBankName.DataTextField = dsBankName.Tables[0].Columns["WERPBM_BankName"].ToString();
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            }
            //else
            //{
            //    //ddlBankName.Items.Clear();
            //    //ddlBankName.DataSource = null;
            //    //ddlBankName.DataBind();
            //    //ddlBankName.Items.Insert(0, new ListItem("Select", "Select"));
            //}
        }
        private void BindPortfolioDropdown(int customerId)
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPortfolio.DataSource = ds;
                ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
                ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
                ddlPortfolio.DataBind();
                hdnPortfolioId.Value = ddlPortfolio.SelectedValue;
            }
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

            txtAssociateSearch.Text = "";
            txtSearchScheme.Text = "";
            //sai ddlARNNo.SelectedIndex = -1;
            ddltransType.SelectedIndex = 0;
            //sai  txtReceivedDate.SelectedDate = null;
            txtApplicationNumber.Text = "";
            BindAMC(0);
            ddlAMCList.SelectedIndex = 0;
            BindCategory();
            ddlCategory.SelectedIndex = 0;
            BindScheme(0);
            Sflag = 0;
            txtSchemeCode.Value = "0";
            //sai ddlAmcSchemeList.SelectedIndex = 0;
            //ddlPortfolio.SelectedIndex = 0;
            BindFolioNumberSearch(0);
            txtFolioNumber.Text = "";
            txtFutureDate.SelectedDate = null;
            txtFutureTrigger.Text = "";
            txtAmount.Text = "";
            // sai  ddlPaymentMode.SelectedIndex = -1;
            txtPaymentNumber.Text = "";
            txtPaymentInstDate.SelectedDate = null;
            ddlBankName.SelectedIndex = -1;
            txtBranchName.Text = "";
            lblGetAvailableAmount.Text = "";
            lblGetAvailableUnits.Text = "";
            ddlSchemeSwitch.SelectedIndex = -1;
            txtCorrAdrLine1.Text = "";
            txtCorrAdrLine2.Text = "";
            txtCorrAdrLine3.Text = "";
            txtLivingSince.SelectedDate = null;
            txtCorrAdrCity.Text = "";
            ddlCorrAdrState.SelectedIndex = -1;
            txtCorrAdrPinCode.Text = "";
            ddlFrequencySIP.SelectedIndex = -1;
            ddlFrequencySTP.SelectedIndex = -1;
            txtstartDateSIP.SelectedDate = null;
            txtendDateSIP.SelectedDate = null;
            txtstartDateSTP.SelectedDate = null;
            txtendDateSTP.SelectedDate = null;

            lblGetLine1.Text = "";
            lblGetLine2.Text = "";
            lblGetline3.Text = "";
            lblGetLivingSince.Text = "";
            lblgetCity.Text = "";
            lblGetstate.Text = "";
            lblGetPin.Text = "";
            lblGetCountry.Text = "";

            txtNewAmount.Text = "";




            txtSystematicdates.Text = "";
            txtPeriod.Text = "";
            //ddlPeriodSelection.Text = "";
            txtRegistrationDate.Text = "";
            txtCeaseDate.Text = "";


        }

        private void BindAMC(int Aflag)
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;

            try
            {
                if (Aflag == 0)
                    dsProductAmc = productMFBo.GetProductAmc();
                else
                    dsProductAmc = operationBo.GetAMCForOrderEntry(Aflag, int.Parse(hdnCustomerId.Value));

                if (dsProductAmc.Tables.Count > 0)
                {
                    dtProductAMC = dsProductAmc.Tables[0];
                    ddlAMCList.DataSource = dtProductAMC;
                    ddlAMCList.DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                    ddlAMCList.DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMCList.DataBind();
                    ddlAMCList.Items.Insert(0, new ListItem("Select", "Select"));
                }
                else
                {
                    ddlAMCList.Items.Clear();
                    ddlAMCList.DataSource = null;
                    ddlAMCList.DataBind();
                    ddlAMCList.Items.Insert(0, new ListItem("Select", "Select"));
                }

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

                if (ddlAMCList.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue.ToString());
                    categoryCode = ddlCategory.SelectedValue;
                    if (txtCustomerId.Value == "")
                        dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 1, 1);
                    else
                        dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));
                }
                else if (ddlAMCList.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue.ToString());
                    categoryCode = ddlCategory.SelectedValue;
                    if (Sflag == 0)
                    {
                        //txtSearchScheme_autoCompleteExtender.ServiceMethod = "GetSchemeList";
                        dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 0, 1);

                    }
                    else
                    {

                        dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));
                    }
                }
                if (dsScheme.Tables.Count > 0)
                {
                    dtScheme = dsScheme.Tables[0];
                    ddlAmcSchemeList.DataSource = dtScheme;
                    ddlAmcSchemeList.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                    ddlAmcSchemeList.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                    ddlAmcSchemeList.DataBind();
                    ddlAmcSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
                }
                else
                {
                    ddlAmcSchemeList.Items.Clear();
                    ddlAmcSchemeList.DataSource = null;
                    ddlAmcSchemeList.DataBind();
                    ddlAmcSchemeList.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

        private void BindFolioNumberSearch(int Fflag)
        {

            DataSet dsgetfolioNo = new DataSet();
            DataTable dtgetfolioNo;
            string parameters = string.Empty;
            int isaAccount = 0;
            try
            {
                if (ddlAMCList.SelectedIndex != 0 && txtSchemeCode.Value != "0")// ddlAmcSchemeList.SelectedIndex != 0)
                {

                    if (ddlAMCList.SelectedValue != "Select")
                        amcCode = int.Parse(ddlAMCList.SelectedValue);
                    schemePlanCode = int.Parse(txtSchemeCode.Value);//ddlAmcSchemeList.SelectedValue);
                    parameters = string.Empty;
                    parameters = txtCustomerId.Value + "/" + amcCode + "/" + schemePlanCode + "/" + Fflag + "/" + isaAccount;
                    txtFolioNumber_autoCompleteExtender.ContextKey = parameters;
                    txtFolioNumber_autoCompleteExtender.ServiceMethod = "GetCustomerFolioAccount";

                    //if (txtCustomerId.Value == "")
                    //    dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 1);
                    //else
                    //if (ddlCustomerISAAccount.SelectedItem.Value != "Select")
                    //{
                    //    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), int.Parse(ddlCustomerISAAccount.SelectedItem.Value));
                    //else
                    //    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), 0);
                }
                else
                {

                    if (ddlAMCList.SelectedValue != "Select")
                        amcCode = int.Parse(ddlAMCList.SelectedValue);
                    schemePlanCode = int.Parse(txtSchemeCode.Value);//ddlAmcSchemeList.SelectedValue);
                    parameters = string.Empty;
                    parameters = txtCustomerId.Value + "/" + amcCode + "/" + schemePlanCode + "/" + Fflag + "/" + isaAccount;
                    txtFolioNumber_autoCompleteExtender.ContextKey = parameters;
                    txtFolioNumber_autoCompleteExtender.ServiceMethod = "GetCustomerFolioAccount";

                    //if (txtCustomerId.Value == "")
                    //    dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 0);
                    //else
                    //if (ddlCustomerISAAccount.SelectedItem.Value != "Select")
                    //    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), int.Parse(ddlCustomerISAAccount.SelectedItem.Value));
                    //else
                    //    dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), 0);
                }

                //if (dsgetfolioNo.Tables[0].Rows.Count > 0)
                //{
                //    dtgetfolioNo = dsgetfolioNo.Tables[0];
                //    ddlFolioNumber.DataSource = dtgetfolioNo;
                //    ddlFolioNumber.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                //    ddlFolioNumber.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                //    ddlFolioNumber.DataBind();
                //    hdnAccountId.Value = ddlFolioNumber.SelectedItem.Text;
                //}
                //else
                //{
                //    ddlFolioNumber.Items.Clear();
                //    ddlFolioNumber.DataSource = null;
                //    //ddlFolioNumber.DataBind();
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

        protected void ddltransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAMC.Visible = true; ddlAMCList.Visible = true;
            lblCategory.Visible = true; ddlCategory.Visible = true;
            lblSearchScheme.Visible = true; ddlAmcSchemeList.Visible = true;
            lblFolioNumber.Visible = true; txtFolioNumber.Visible = true;
            spnAMC.Visible = true; spnScheme.Visible = true;
            CompareValidator1.Visible = true; CompareValidator2.Visible = true;
            txtSearchScheme.Visible = true; Span9.Visible = true; imgFolioAdd.Visible = true;
            if ((string.IsNullOrEmpty(txtPansearch.Text) && string.IsNullOrEmpty(txtCustomerName.Text)))
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select a customer');", true);
            else
            {
                hdnType.Value = ddltransType.SelectedValue.ToString();
                if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "ABY" || ddltransType.SelectedValue == "SIP")
                {

                    ShowTransactionType(1);

                    if (ddltransType.SelectedValue == "BUY")
                    {
                        BindAMC(0);
                        BindScheme(0);
                        Sflag = 0;
                        //BindFolioNumber(0);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;


                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;

                        txtFolioNumber.Enabled = false;
                        imgFolioAdd.Enabled = false;
                    }
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        BindAMC(0);
                        BindScheme(0);
                        Sflag = 0;
                        //BindFolioNumber(0);
                        trFrequency.Visible = true;
                        trSIPStartDate.Visible = true;

                    }
                    else
                    {
                        BindAMC(0);
                        BindScheme(0);
                        Sflag = 0;
                        //BindFolioNumber(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;
                    }
                }
                else if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                {
                    ShowTransactionType(2);
                    if (ddltransType.SelectedValue == "SWB")
                    {
                        //BindSchemeSwitch();
                        trScheme.Visible = true;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;

                    }
                    else if (ddltransType.SelectedValue == "STB")
                    {
                        trScheme.Visible = true;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = true;
                        trSTPStart.Visible = true;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = true;
                    }
                    else
                    {
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                        trScheme.Visible = false;
                        trPINo.Visible = false;

                        //sai    trSystematicDateChk1.Visible = false ;;
                        // trSystematicDateChk2.Visible = false;
                        // trSystematicDateChk3.Visible = false;
                        // trSystematicDate.Visible = false;
                        // trRegistrationDate.Visible = false;
                        // trCeaseDate.Visible = false;

                    }
                    BindAMC(0);
                    BindScheme(0);
                    Sflag = 0;
                    //BindFolioNumber(1);

                }
                else if (ddltransType.SelectedValue == "CAF")
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
                    lblAMC.Visible = false; ddlAMCList.Visible = false;
                    lblCategory.Visible = false; ddlCategory.Visible = false;
                    lblSearchScheme.Visible = false; ddlAmcSchemeList.Visible = false;
                    lblFolioNumber.Visible = false; txtFolioNumber.Visible = false;
                    imgFolioAdd.Visible = false;
                    spnAMC.Visible = false; spnScheme.Visible = false;
                    CompareValidator1.Visible = false; CompareValidator2.Visible = false;
                    txtSearchScheme.Visible = false;
                    RequiredFieldValidator9.Visible = false;
                    Span9.Visible = false;
                }


                btnSubmit.Visible = true;
            }
            //btnViewInPDFNew.Visible = true;
            //btnViewInDOCNew.Visible = true;
            //if (ddlPaymentMode.SelectedValue == "CQ")
            //{
            //    trPINo.Visible = true;
            //}
            //else
            //{
            //    trPINo.Visible = false ;
            //}
            if (advisorVo.A_AgentCodeBased == 1)
            {
                trGetAmount.Visible = false;
                //  trRedeemed.Visible = false;
            }
            else
            {
                trGetAmount.Visible = true;
                //  trRedeemed.Visible = true;
            }
            ddlPaymentMode_SelectedIndexChanged(this, null);
            Sipvisblity(hdnType.Value, "");

        }

        private void Sipvisblity(string transactiontype, string mode)
        {

            if ((transactiontype == "SIP" | transactiontype == "SWP" | transactiontype == "STB"))
            {
                trSystematicDateChk1.Visible = true;
                trSystematicDateChk2.Visible = true;
                trSystematicDateChk3.Visible = true;
                trSystematicDate.Visible = true;
                //trRegistrationDate.Visible = true;
                //trCeaseDate.Visible = true;

                if (mode == "View")
                {
                    SipEnablity(false);
                }
                else
                {
                    SipEnablity(true);
                }
            }
            else
            {

                trSystematicDateChk1.Visible = false;
                trSystematicDateChk2.Visible = false;
                trSystematicDateChk3.Visible = false;
                trSystematicDate.Visible = false;
                //trRegistrationDate.Visible = false;
                //trCeaseDate.Visible = false;
                SipEnablity(false);
            }




        }
        private void SipEnablity(bool bln)
        {



            lblSystematicDate.Enabled = bln;
            txtSystematicdates.Enabled = bln;
            lblPeriod.Enabled = bln;
            txtPeriod.Enabled = bln;
            ddlPeriodSelection.Enabled = bln;
            lblRegistrationDate.Enabled = bln;
            txtRegistrationDate.Enabled = bln;
            lblCeaseDate.Enabled = bln;
            txtCeaseDate.Enabled = bln;


        }


        //protected void btnAddCustomer_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('CustomerType','page=Entry');", true);
        //}

        public void BindOrderStepsGrid()
        {
            DataSet dsOrderSteps = new DataSet();
            DataTable dtOrderDetails;
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

        protected void rgvOrderSteps_ItemDataBound(object sender, GridItemEventArgs e)
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

                if (lblOrderStep.Text.Trim() == "CAP" | lblOrderStep.Text.Trim() == "IP")  //set your condition for hiding the row
                {
                    dataItem.Display = false;  //hide the row
                }

                if (lblOrderStep.Text.Trim() == "IP")
                {
                    if (lblStatusCode.Text == "OMIP")
                    {
                        editButton.Text = "Mark as Pending";
                        result = mfOrderBo.MFOrderAutoMatch(orderVo.OrderId, mforderVo.SchemePlanCode, mforderVo.accountid, mforderVo.TransactionCode, orderVo.CustomerId, mforderVo.Amount, orderVo.OrderDate);
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
            DataSet dsSwitchScheme = new DataSet();
            DataTable dtSwitchScheme;
            if (ddlAMCList.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlAMCList.SelectedValue);
                dsSwitchScheme = operationBo.GetSwitchScheme(amcCode);
            }
            if (dsSwitchScheme.Tables.Count > 0)
            {
                dtSwitchScheme = dsSwitchScheme.Tables[0];
                ddlSchemeSwitch.DataSource = dtSwitchScheme;
                ddlSchemeSwitch.DataTextField = dtSwitchScheme.Columns["PASP_SchemePlanName"].ToString();
                ddlSchemeSwitch.DataValueField = dtSwitchScheme.Columns["PASP_SchemePlanCode"].ToString();
                ddlSchemeSwitch.DataBind();
                ddlSchemeSwitch.Items.Insert(0, new ListItem("Select", "Select"));
            }
            else
            {
                ddlSchemeSwitch.Items.Clear();
                ddlSchemeSwitch.DataSource = null;
                ddlSchemeSwitch.DataBind();
                ddlSchemeSwitch.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentMode.SelectedValue == "CQ")
            {
                trPINo.Visible = true;
                txtPaymentInstDate.MaxDate = txtOrderDate.MaxDate;
            }
            else
            {
                trPINo.Visible = false;
            }
        }

        protected void ddlAMCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMCList.SelectedIndex != 0)
            {
                BindFolioNumberSearch(0);
                txtSearchScheme.Text = "";
                hdnAmcCode.Value = ddlAMCList.SelectedItem.Text;
                amcCode = int.Parse(ddlAMCList.SelectedValue);
                if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "SIP")
                {
                    BindScheme(0);
                    Sflag = 0;
                }
                else
                {


                    Sflag = 0;

                    //sai BindScheme(1);
                    //BindFolioNumber(1);
                }
                BindSchemeSwitch();
            }

            bindSearchScheme();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex != 0)
            {
                if (ddlAMCList.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue);
                    categoryCode = ddlCategory.SelectedValue;
                    //if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "SIP")
                    //{
                    //    BindScheme(0);
                    //    Sflag = 0;
                    //}
                    //else
                    //{
                    //    bindSearchScheme();
                    //    Sflag = 1;
                    //}
                }
            }
            bindSearchScheme();
        }

        protected void ddlAmcSchemeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string transactionType = "";
            int schemeCode = 0;
            string FileName = "";
            if (ddlAmcSchemeList.SelectedIndex != 0)
            {
                schemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
                hdnSchemeCode.Value = ddlAmcSchemeList.SelectedValue.ToString();
                hdnSchemeName.Value = ddlAmcSchemeList.SelectedItem.Text;
                categoryCode = productMFBo.GetCategoryNameFromSChemeCode(schemePlanCode);
                ddlCategory.SelectedValue = categoryCode;
                if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                {
                    if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
                    {
                        BindFolioNumberSearch(1);
                        DataSet dsGetAmountUnits;
                        DataTable dtGetAmountUnits;
                        dsGetAmountUnits = operationBo.GetAmountUnits(schemePlanCode, int.Parse(txtCustomerId.Value));
                        dtGetAmountUnits = dsGetAmountUnits.Tables[0];
                        lblGetAvailableAmount.Text = dtGetAmountUnits.Rows[0]["CMFNP_CurrentValue"].ToString();
                        lblGetAvailableUnits.Text = dtGetAmountUnits.Rows[0]["CMFNP_NetHoldings"].ToString();
                        if ((ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "Switch") && ddlAMCList.SelectedIndex != 0)
                        {
                            BindSchemeSwitch();
                        }
                    }
                }
                else
                    BindFolioNumberSearch(0);
            }
        }

        protected void txtReceivedDate_DateChanged(object sender, EventArgs e)
        {
            if (txtReceivedDate.SelectedDate == null)
            {
                //   txtOrderDate.d = "";
                return;
            }

            txtOrderDate.SelectedDate = Convert.ToDateTime(txtOrderDate.SelectedDate).AddDays(3); ;

        }

        protected void txtOrderDate_DateChanged(object sender, EventArgs e)
        {

            string ddlTrxnType = "";

            if (txtOrderDate.SelectedDate == null)
            {
                return;
            }
            DateTime dt = Convert.ToDateTime(txtOrderDate.SelectedDate);
            //if (ddlTransactionType.SelectedItem.Value == "Sell")
            //{
            //    ddlTrxnType = "SEL";
            //}
            if (ddltransType.SelectedItem.Value == "BUY")
            {
                ddlTrxnType = "BUY";
            }
            if (!String.IsNullOrEmpty(hdnSchemeCode.Value.ToString()))
                schemePlanCode = int.Parse(hdnSchemeCode.Value);
            else
                schemePlanCode = 0;
            DataSet dsNavDetails = new DataSet();
            dsNavDetails = customerPortfolioBo.GetMFSchemePlanPurchaseDateAndValue(schemePlanCode, dt, ddlTrxnType);

            if (dsNavDetails != null)
            {
                if (dsNavDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drNavDetails in dsNavDetails.Tables[0].Rows)
                    {
                        if (drNavDetails["PSP_RepurchasePrice"].ToString() != null && drNavDetails["PSP_RepurchasePrice"].ToString() != "")
                        {
                            txtNAV.Text = drNavDetails["PSP_RepurchasePrice"].ToString();
                            lblNavAsOnDate.Text = Convert.ToDateTime(drNavDetails["PSP_Date"]).ToShortDateString();
                        }
                    }
                }
                else
                {
                    txtNAV.Text = "0";
                    lblNavAsOnDate.Text = "Not Available";
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            List<int> OrderIds = new List<int>();
            SaveOrderDetails();
            OrderIds = mfOrderBo.CreateCustomerMFOrderDetails(orderVo, mforderVo, userVo.UserId, systematicSetupVo);
            rgvOrderSteps.Visible = true;
            orderId = int.Parse(OrderIds[0].ToString());
            Session["CO_OrderId"] = orderId;
            orderVo.OrderId = orderId;
            rgvOrderSteps.Enabled = true;
            BindOrderStepsGrid();
            Sipvisblity(hdnType.Value, "View");

            SetEditViewMode(true);
            btnSubmit.Visible = false;
            lnkBtnEdit.Visible = true;
            lnlBack.Visible = false;
            imgBtnRefereshBank.Enabled = false;
            imgAddBank.Enabled = false;
            orderNumber = mfOrderBo.GetOrderNumber(orderId);
            lblGetOrderNo.Text = orderNumber.ToString();
            lblOrderNumber.Visible = true;
            lblGetOrderNo.Visible = true;

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order added successfully.');", true);



            //btnViewReport.Visible = true;
            //btnViewInPDF.Visible = true;
            //btnViewInDOC.Visible = true;
            //btnViewInPDFNew.Visible = false;
            //btnViewInDOCNew.Visible = false;
        }

        private void SaveOrderDetails()
        {
            orderVo.CustomerId = int.Parse(txtCustomerId.Value);
            orderVo.AssetGroup = "MF";
            mforderVo.CustomerName = txtCustomerName.Text;
            mforderVo.BMName = lblGetBranch.Text;
            mforderVo.RMName = lblGetRM.Text;
            mforderVo.PanNo = lblgetPan.Text;
            mforderVo.TransactionCode = ddltransType.SelectedValue;
            if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString().Trim()))
            {
                orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            }
            else
                orderVo.ApplicationReceivedDate = DateTime.MinValue;
            orderVo.ApplicationNumber = txtApplicationNumber.Text;
            if (ddlAMCList.SelectedIndex != 0)
                mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            else
                mforderVo.Amccode = 0;
            mforderVo.category = ddlCategory.SelectedValue;
            if (txtSchemeCode.Value != "0")
                //if (ddlAmcSchemeList.SelectedIndex != 0)
                mforderVo.SchemePlanCode = int.Parse(txtSchemeCode.Value);//ddlAmcSchemeList.SelectedValue);
            else
                mforderVo.SchemePlanCode = 0;
            mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            if (!string.IsNullOrEmpty(hidFolioNumber.Value))
                mforderVo.accountid = int.Parse(hidFolioNumber.Value);
            else
                mforderVo.accountid = 0;
            mforderVo.FolioNumber = txtFolioNumber.Text;
            orderVo.OrderDate = Convert.ToDateTime(txtOrderDate.SelectedDate);
            mforderVo.OrderNumber = int.Parse(lblGetOrderNo.Text);
            //orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
            //orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
            if (rbtnImmediate.Checked == true)
                mforderVo.IsImmediate = 1;
            else
                mforderVo.IsImmediate = 0;
            if (!string.IsNullOrEmpty((txtFutureDate.SelectedDate).ToString().Trim()))
                mforderVo.FutureExecutionDate = DateTime.Parse(txtFutureDate.SelectedDate.ToString());
            else
                mforderVo.FutureExecutionDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty((txtFutureTrigger.Text).ToString().Trim()))
                mforderVo.FutureTriggerCondition = txtFutureTrigger.Text;
            else
                mforderVo.FutureTriggerCondition = "";
            if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
                mforderVo.Amount = double.Parse(txtAmount.Text);
            else
                mforderVo.Amount = 0;
            //if (!string.IsNullOrEmpty((lblGetAvailableUnits.Text).ToString().Trim()))
            //    mforderVo.Units = double.Parse(lblGetAvailableUnits.Text);
            //else
            //    mforderVo.Units = 0;
            if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
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
            if (ddlPaymentMode.SelectedIndex != 0)
                orderVo.PaymentMode = ddlPaymentMode.SelectedValue;
            else
                orderVo.PaymentMode = "ES";

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
            //  if (ddlSchemeSwitch.SelectedValue != "")
            // {
            if (ddlSchemeSwitch.SelectedIndex > 0)
            {
                mforderVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
            }
            else
            {
                mforderVo.SchemePlanSwitch = 0;
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
            if (ddlCorrAdrState.SelectedIndex != 0)
                mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
            else
                mforderVo.State = "";
            if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
                mforderVo.Pincode = txtCorrAdrPinCode.Text;
            else
                mforderVo.Pincode = "";
            mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
            if (ddltransType.SelectedValue == "SIP")
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
            else if (ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP")
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
            if (!String.IsNullOrEmpty(txtSystematicdates.Text))
                systematicSetupVo.SystematicDate = Convert.ToInt32(txtSystematicdates.Text);
            //txtSystematicdates.SelectedDate.Value.Day;


            if (!string.IsNullOrEmpty(txtPeriod.Text))
                systematicSetupVo.Period = Convert.ToInt32(txtPeriod.Text);


            systematicSetupVo.PeriodSelection = ddlPeriodSelection.SelectedValue;
            if (!string.IsNullOrEmpty(txtRegistrationDate.Text))

                systematicSetupVo.RegistrationDate = DateTime.Parse(txtRegistrationDate.Text);
            else
                systematicSetupVo.RegistrationDate = DateTime.MinValue;


            if (!string.IsNullOrEmpty(txtCeaseDate.Text.ToString().Trim()) && txtCeaseDate.Text != "dd/mm/yyyy")
                systematicSetupVo.CeaseDate = DateTime.Parse(txtCeaseDate.Text.ToString());
            else
                systematicSetupVo.CeaseDate = DateTime.MinValue;


            Session["orderVo"] = orderVo;
            Session["mforderVo"] = mforderVo;

        }


        protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnImmediate.Checked)
                trfutureDate.Visible = false;
            else
                trfutureDate.Visible = true;
        }

        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>(); ;
            SaveOrderDetails();
            OrderIds = mfOrderBo.CreateCustomerMFOrderDetails(orderVo, mforderVo, userVo.UserId, systematicSetupVo);
            orderId = int.Parse(OrderIds[0].ToString());
            rgvOrderSteps.Visible = false;
            orderNumber = mfOrderBo.GetOrderNumber(orderId);
            lblOrderNumber.Visible = true;
            lblGetOrderNo.Visible = true;
            // orderNumber = orderNumber + 1;
            lblGetOrderNo.Text = orderNumber.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Your order added successfully.');", true);

            ClearAllFields();

            txtCustomerName.Text = "";
            lblGetRM.Text = "";
            lblGetBranch.Text = "";
            lblgetPan.Text = "";


        }
        protected void ddlPeriodSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   txtendDateSIP.SelectedDate = CalcEndDate(int.Parse(txtPeriod.Text.ToString()), DateTime.Parse(txtstartDateSIP.SelectedDate.ToString()));
        }

        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            //txtendDateSIP.SelectedDate = CalcEndDate(int.Parse(txtPeriod.Text.ToString()), DateTime.Parse(txtstartDateSIP.SelectedDate.ToString()));

        }
        private DateTime CalcEndDate(int period, DateTime startDate)
        {
            DateTime endDate = new DateTime();
            if (ddlPeriodSelection.SelectedItem.Value == "DA")
            {
                endDate = startDate.AddDays(period - 1);
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "MN")
            {
                endDate = startDate.AddMonths((period - 1));
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "YR")
            {
                period = period * 12;
                endDate = startDate.AddMonths(period - 1);
            }



            return endDate;
        }
        public void SetEditViewMode(bool Bool)
        {

            if (Bool)
            {

                //txtOrederNumber.Enabled = false;
                // txtOrderDate.Enabled = false;
                //ddlBranch.Enabled = false;
                //ddlRM.Enabled = false;
                lblOrderNumber.Visible = false;
                lblGetOrderNo.Visible = false;
                txtCustomerName.Enabled = false;
                btnImgAddCustomer.Enabled = false;
                ddltransType.Enabled = false;
                //ddlPortfolio.Enabled = false;
                txtFolioNumber.Enabled = false;
                //btnAddFolio.Enabled = false;
                ddlAMCList.Enabled = false;
                ddlAmcSchemeList.Enabled = false;
                ddlCategory.Enabled = false;
                txtReceivedDate.Enabled = false;
                txtApplicationNumber.Enabled = false;
                rbtnImmediate.Enabled = false;
                rbtnFuture.Enabled = false;
                txtFutureDate.Enabled = false;
                txtFutureTrigger.Enabled = false;
                //ddlOrderStatus.Enabled = false;
                //ddlOrderPendingReason.Enabled = false;
                txtOrderDate.Enabled = false;

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

                //sip
                //sai    trSystematicDateChk1.Visible = false ;;
                // trSystematicDateChk2.Visible = false;
                // trSystematicDateChk3.Visible = false;
                // trSystematicDate.Visible = false;
                // trRegistrationDate.Visible = false;
                // trCeaseDate.Visible = false;



                txtCorrAdrLine1.Enabled = false;
                txtCorrAdrLine2.Enabled = false;
                txtCorrAdrLine3.Enabled = false;
                txtLivingSince.Enabled = false;
                txtCorrAdrCity.Enabled = false;
                ddlCorrAdrState.Enabled = false;
                txtCorrAdrPinCode.Enabled = false;
                ddlCorrAdrCountry.Enabled = false;
                ddlARNNo.Enabled = false;

                ddlsearch.Enabled = false;
                txtAssociateSearch.Enabled = false;
                txtSearchScheme.Enabled = false;

                btnSubmit.Enabled = false;
                btnAddMore.Visible = false;
                //ddlAssociate.Enabled = false;
                imgFolioAdd.Visible = false;
                // imgAddBank.Visible = false;

            }
            else
            {
                //lblOrderNumber.Visible = true;
                //lblGetOrderNo.Visible = true;
                ddlsearch.Enabled = false;
                txtAssociateSearch.Enabled = false;
                txtSearchScheme.Enabled = false;
                //txtOrederNumber.Enabled = true;
                //txtOrderDate.Enabled = true;
                //ddlBranch.Enabled = true;
                //ddlRM.Enabled = true;
                txtCustomerName.Enabled = false;
                btnImgAddCustomer.Enabled = false;
                ddltransType.Enabled = true;
                //ddlPortfolio.Enabled = true;
                txtFolioNumber.Enabled = true;
                //btnAddFolio.Enabled = true;
                ddlAMCList.Enabled = false;
                ddlAmcSchemeList.Enabled = true;
                ddlCategory.Enabled = true;
                txtReceivedDate.Enabled = true;
                txtApplicationNumber.Enabled = true;
                rbtnImmediate.Enabled = true;
                rbtnFuture.Enabled = true;
                txtFutureDate.Enabled = true;
                txtFutureTrigger.Enabled = true;
                //ddlOrderStatus.Enabled = true;
                //ddlOrderPendingReason.Enabled = true;
                txtOrderDate.Enabled = false;

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



                //sai    trSystematicDateChk1.Visible = false ;;
                // trSystematicDateChk2.Visible = false;
                // trSystematicDateChk3.Visible = false;
                // trSystematicDate.Visible = false;
                // trRegistrationDate.Visible = false;
                // trCeaseDate.Visible = false;



                txtCorrAdrLine1.Enabled = true;
                txtCorrAdrLine2.Enabled = true;
                txtCorrAdrLine3.Enabled = true;
                txtLivingSince.Enabled = true;
                txtCorrAdrCity.Enabled = true;
                ddlCorrAdrState.Enabled = true;
                txtCorrAdrPinCode.Enabled = true;
                ddlCorrAdrCountry.Enabled = true;
                ddlARNNo.Enabled = true;

                btnSubmit.Enabled = true;
                btnAddMore.Visible = false;
                imgFolioAdd.Visible = true;
                //ddlAssociate.Enabled = true;
            }


        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            SetEditViewMode(false);
            ViewForm = "Edit";
            lnkDelete.Visible = true;
            //  folioNo = txtFolioNumber.Text;

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
                    //   lblGetOrderNo.Text = orderId.ToString();
                    lnlBack.Visible = true;

                }
            }
            else
            {
                SetControls("Entry", mforderVo, orderVo);
            }
            if (Request.QueryString["action"] == null)
            {
                orderNumber = mfOrderBo.GetOrderNumber(orderId);
                lblGetOrderNo.Text = orderNumber.ToString();
                lblOrderNumber.Visible = true;
                lblGetOrderNo.Visible = true;
            }
            //  txtFolioNumber.Text=folioNo;
            btnSubmit.Visible = false;
            rgvOrderSteps.Enabled = true;
            btnAddMore.Visible = false;
            btnUpdate.Visible = true;
            lnkBtnEdit.Visible = false;
            btnreport.Visible = true;
            btnpdfReport.Visible = true;
        }

        protected void lnlBack_Click(object sender, EventArgs e)
        {
            string Mfaction = string.Empty;
            if (Request.QueryString["FromPage"] != null)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerOrderList','none');", true);
            }
            else if (Request.QueryString["action"] != null)
            {
                Mfaction = "MF";
                //"loadcontrol('ProductOrderMaster','fiaction=Edit');", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderList", "loadcontrol('OrderList','Mfaction=MF');", true);
                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OrderList','Mfaction=MF');", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>();
            UpdateMFOrderDetails();

            orderVo.AssetGroup = "MF";
            mfOrderBo.UpdateCustomerMFOrderDetails(orderVo, mforderVo, userVo.UserId, systematicSetupVo);
            SetEditViewMode(true);
            imgBtnRefereshBank.Enabled = false;
            btnUpdate.Visible = false;
            btnViewInPDFNew.Visible = false;
            btnViewInDOCNew.Visible = false;
            lblOrderNumber.Visible = true;
            lblGetOrderNo.Visible = true;
        }

        private void UpdateMFOrderDetails()
        {

            //operationVo.CustomerId = int.Parse(txtCustomerId.Value);
            mforderVo.CustomerName = txtCustomerName.Text;
            if (orderVo.CustomerId != 0)
                hdnCustomerId.Value = orderVo.CustomerId.ToString();
            mforderVo.BMName = lblGetBranch.Text;
            mforderVo.RMName = lblGetRM.Text;
            mforderVo.PanNo = lblgetPan.Text;
            mforderVo.TransactionCode = ddltransType.SelectedValue;
            if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString()))
                orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            else
                orderVo.ApplicationReceivedDate = DateTime.MinValue;

            orderVo.ApplicationNumber = txtApplicationNumber.Text;
            if (ddlAMCList.SelectedIndex != 0)
                mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            else
                mforderVo.Amccode = 0;
            mforderVo.category = ddlCategory.SelectedValue;
            if (txtSchemeCode.Value != "0")
                // if (ddlAmcSchemeList.SelectedIndex != -1)
                mforderVo.SchemePlanCode = int.Parse(txtSchemeCode.Value);//ddlAmcSchemeList.SelectedValue);
            else
                mforderVo.SchemePlanCode = 0;
            // mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            if (!string.IsNullOrEmpty(hidFolioNumber.Value))
                mforderVo.accountid = int.Parse(hidFolioNumber.Value);
            else
                mforderVo.accountid = 0;
            orderVo.OrderDate = Convert.ToDateTime(txtOrderDate.SelectedDate);
            mforderVo.OrderNumber = int.Parse(lblGetOrderNo.Text);
            //orderVo.OrderStatusCode = ddlOrderStatus.SelectedValue;
            //orderVo.ReasonCode = ddlOrderPendingReason.SelectedValue;
            if (rbtnImmediate.Checked == true)
                mforderVo.IsImmediate = 1;
            else
                mforderVo.IsImmediate = 0;
            if (!string.IsNullOrEmpty(txtFutureDate.SelectedDate.ToString().Trim()))
                mforderVo.FutureExecutionDate = DateTime.Parse(txtFutureDate.SelectedDate.ToString());
            else
                mforderVo.FutureExecutionDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty((txtFutureTrigger.Text).ToString().Trim()))
                mforderVo.FutureTriggerCondition = txtFutureTrigger.Text;
            else
                mforderVo.FutureTriggerCondition = "";
            if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
                mforderVo.Amount = double.Parse(txtAmount.Text);
            else
                mforderVo.Amount = 0;

            if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
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
            if (ddltransType.SelectedValue == "SIP")
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
            else if (ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP")
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

            if (ddlPaymentMode.SelectedValue == "ES")
                orderVo.PaymentMode = "ES";
            else if (ddlPaymentMode.SelectedValue == "DF")
                orderVo.PaymentMode = "DF";
            else if (ddlPaymentMode.SelectedValue == "CQ")
                orderVo.PaymentMode = "CQ";
            if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
                orderVo.ChequeNumber = txtPaymentNumber.Text;
            else
                orderVo.ChequeNumber = "";
            if (txtPaymentInstDate.SelectedDate != null)
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
            if (!string.IsNullOrEmpty(txtCorrAdrLine2.Text.ToString().Trim()))
                mforderVo.AddrLine2 = txtCorrAdrLine2.Text;
            else
                mforderVo.AddrLine2 = "";
            if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
                mforderVo.AddrLine3 = txtCorrAdrLine3.Text;
            else
                mforderVo.AddrLine3 = "";
            if (txtLivingSince.SelectedDate.ToString() != "dd/mm/yyyy")
                mforderVo.LivingSince = DateTime.MinValue;
            else
                mforderVo.LivingSince = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
                mforderVo.City = txtCorrAdrCity.Text;
            else
                mforderVo.City = "";
            if (ddlCorrAdrState.SelectedIndex != 0)
                mforderVo.State = ddlCorrAdrState.SelectedItem.Text;
            else
                mforderVo.State = "";
            if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
                mforderVo.Pincode = txtCorrAdrPinCode.Text;
            else
                mforderVo.Pincode = "";
            mforderVo.Country = ddlCorrAdrCountry.SelectedValue;
            if (ddlARNNo.SelectedIndex != 0)
                mforderVo.ARNNo = ddlARNNo.SelectedItem.Text;
            if (!string.IsNullOrEmpty(txtAssociateSearch.Text))
            {

                AgentId = customerBo.GetAssociateName(advisorVo.advisorId, txtAssociateSearch.Text);
                if (AgentId.Rows.Count > 0)
                {
                    orderVo.AgentId = int.Parse(AgentId.Rows[0][1].ToString());
                }
                else
                    orderVo.AgentId = 0;
            }



            if (!String.IsNullOrEmpty(txtSystematicdates.Text))
                systematicSetupVo.SystematicDate = Convert.ToInt32(txtSystematicdates.Text);
            //txtSystematicdates.SelectedDate.Value.Day;


            if (!string.IsNullOrEmpty(txtPeriod.Text))
                systematicSetupVo.Period = Convert.ToInt32(txtPeriod.Text);


            systematicSetupVo.PeriodSelection = ddlPeriodSelection.SelectedValue;
            if (!string.IsNullOrEmpty(txtRegistrationDate.Text))

                systematicSetupVo.RegistrationDate = DateTime.Parse(txtRegistrationDate.Text);
            else
                systematicSetupVo.RegistrationDate = DateTime.MinValue;


            if (!string.IsNullOrEmpty(txtCeaseDate.Text.ToString().Trim()) && txtCeaseDate.Text != "dd/mm/yyyy")
                systematicSetupVo.CeaseDate = DateTime.Parse(txtCeaseDate.Text.ToString());
            else
                systematicSetupVo.CeaseDate = DateTime.MinValue;
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

        protected void lnkDelete_Click(object sender, EventArgs e)
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
                    //  txtOrderDate.S = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                    //     lblGetOrderNo.Text = ((mfOrderBo.GetOrderNumber()) + 1).ToString();
                    txtApplicationNumber.Enabled = true;
                    lnkBtnEdit.Visible = false;
                    lnlBack.Visible = false;
                    lnkDelete.Visible = false;
                    btnUpdate.Visible = false;
                    btnSubmit.Visible = true;
                    btnAddMore.Visible = true;
                    rgvOrderSteps.Visible = false;
                    SetEditViewMode(false);
                    btnImgAddCustomer.Enabled = true;
                    btnImgAddCustomer.Visible = true;
                    txtCustomerName.Enabled = true;
                    txtCustomerName.Text = "";
                    lblOrderNumber.Visible = false;
                    lblGetOrderNo.Visible = false;

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

        protected void btnOpenPopup_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Value) || txtCustomerId.Value == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select customer first!');", true);
            }
            else if (ddlAMCList.SelectedIndex > 0)
            {
                radwindowPopup.VisibleOnPageLoad = true;
                lblFolioAMC.Text = ddlAMCList.SelectedItem.Text;
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select AMC first!');", true);

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            radwindowPopup.VisibleOnPageLoad = false;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            int accountId;

            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            customerAccountVo.CustomerId = int.Parse(txtCustomerId.Value);
            customerAccountVo.AccountNum = txtNewFolio.Text;
            customerAccountVo.AMCCode = int.Parse(ddlAMCList.SelectedValue);
            if (!customerAccountBo.CheckFolioDuplicate(advisorVo.advisorId, customerAccountVo.AccountNum))
            {
                accountId = customerAccountBo.CreateCustomerMFAccountBasic(customerAccountVo, userVo.UserId);
                if (accountId != 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Folio created successfully, now search for folio !');", true);
                    radwindowPopup.VisibleOnPageLoad = false;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Folio already Exists !');", true);
            }

        }

        protected void txtReceivedDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtReceivedDate.SelectedDate.ToString()))
            {
                RadDateControlBusinessDateValidation(ref txtOrderDate, 3, DateTime.Parse(txtReceivedDate.SelectedDate.ToString()), 0);
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
