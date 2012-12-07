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
using System.Configuration;
using VoOps;
using iTextSharp.text.pdf;
using System.IO;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            orderNumber = mfOrderBo.GetOrderNumber();
            orderNumber = orderNumber + 1;
            lblGetOrderNo.Text = orderNumber.ToString();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            if (Session["mforderVo"] != null && Session["orderVo"] != null)
            {
                mforderVo = (MFOrderVo)Session["mforderVo"];
                orderVo = (OrderVo)Session["orderVo"];
            }


            if (!IsPostBack)
            {
                gvJointHoldersList.Visible = false;
                //trRegretMsg.Visible = false;
                hdnIsSubscripted.Value = advisorVo.IsISASubscribed.ToString();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowInitialIsa();", true);
               
                ddlAMCList.Enabled = false;
                if (Request.QueryString["action"] != null)
                {
                    
                    lnlBack.Visible = true;
                    ViewForm = Request.QueryString["action"].ToString();
                    txtOrderDate.SelectedDate = orderVo.OrderDate;
                    lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    btnViewInPDFNew.Visible = false;
                    btnViewInDOCNew.Visible = false;
                }

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
                if (Request.QueryString["CustomerId"] != null)
                {
                    customerId = Convert.ToInt32(Request.QueryString["CustomerId"]);
                    customerVo = customerBo.GetCustomer(customerId);

                    //ISAList = customerBo.GetISaList(customerId);
                    //if (ISAList != null)
                    //{
                    //    string formatstring = "";
                    //    string[] arr = new string[ISAList.Rows.Count];
                    //    int i;
                    //    for (i = 0; i < ISAList.Rows.Count; i++)
                    //    {
                    //        if (!string.IsNullOrEmpty(formatstring))
                    //        {
                    //            formatstring = formatstring + "," + arr[i];
                    //        }
                    //        else
                    //        {
                    //            formatstring = arr[i];
                    //        }
                    //    }
                    //    lblIsaNo.Text = formatstring;

                    //}
                    //else
                    //{
                    //    trRegretMsg.Visible = true;
                    //}
                    hdnCustomerId.Value = customerVo.CustomerId.ToString();
                    txtCustomerName.Text = customerVo.FirstName + customerVo.MiddleName + customerVo.LastName;
                    lblGetBranch.Text = customerVo.BranchName;
                    lblGetRM.Text = customerVo.RMName;
                    lblgetPan.Text = customerVo.PANNum;
                    BindPortfolioDropdown(customerId);
                }
                cvAppRcvDate.ValueToCompare = DateTime.Today.ToShortDateString();
                cvFutureDate1.ValueToCompare = DateTime.Today.ToShortDateString();
                BindAMC(0);
                BindScheme(0);
                //BindFolioNumber(0);
                //BindOrderStatus();
                BindCategory();
                BindState();
                BindFrequency();
                ShowHideFields(1);
                ShowTransactionType(0);


                if (mforderVo != null && orderVo != null)
                {
                    if (ViewForm == "View")
                    {
                        SetControls("View", mforderVo, orderVo);
                        btnAddMore.Visible = false;
                    }
                    else if (ViewForm == "Edit")
                    {
                        SetControls("Edit", mforderVo, orderVo);
                        btnAddMore.Visible = false;
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
                }
                else
                {
                    SetControls("Entry", mforderVo, orderVo);

                }

            }


            //ShowHideFields(1);
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
                    txtReceivedDate.SelectedDate = null;
                    txtApplicationNumber.Text = "";
                    ddlAMCList.SelectedIndex = 0;
                    ddlCategory.SelectedIndex = 0;
                    ddlAmcSchemeList.SelectedIndex = 0;
                    ddlPortfolio.SelectedIndex = -1;
                    ddlFolioNumber.SelectedIndex = 0;
                    txtOrderDate.SelectedDate = null;
                    lblGetOrderNo.Text = "";
                    //ddlOrderPendingReason.SelectedIndex = 0;
                    //ddlOrderStatus.SelectedIndex = 0;
                    txtFutureDate.SelectedDate = null;
                    txtFutureTrigger.Text = "";
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
                }
            }
            else if (action == "Edit")
            {
                if (mforderVo != null && orderVo != null)
                {
                    SetEditViewMode(false);
                    orderId = orderVo.OrderId;
                    txtCustomerName.Enabled = false;
                    txtCustomerName.Text = mforderVo.CustomerName;
                    if (orderVo.CustomerId != 0)
                        hdnCustomerId.Value = orderVo.CustomerId.ToString();
                    BindPortfolioDropdown(orderVo.CustomerId);
                    customerVo = customerBo.GetCustomer(orderVo.CustomerId);
                    lblGetBranch.Text = mforderVo.BMName;
                    lblGetRM.Text = mforderVo.RMName;
                    lblgetPan.Text = mforderVo.PanNo;
                    ddltransType.SelectedValue = mforderVo.TransactionCode;
                    txtOrderDate.SelectedDate = orderVo.OrderDate;

                    lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    hdnType.Value = mforderVo.TransactionCode;

                    if (ddltransType.SelectedValue == "CAF")
                    {
                        ShowTransactionType(3);
                        lblAMC.Visible = false; ddlAMCList.Visible = false;
                        lblCategory.Visible = false; ddlCategory.Visible = false;
                        lblSearchScheme.Visible = false; ddlAmcSchemeList.Visible = false;
                        lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                        spnAMC.Visible = false; spnScheme.Visible = false;
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
                        ddlPortfolio.SelectedValue = mforderVo.portfolioId.ToString();
                        BindScheme(0);
                        ddlAmcSchemeList.SelectedValue = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();

                    }
                    portfolioId = mforderVo.portfolioId;
                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "ABY")
                    {
                        ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        ShowTransactionType(1);
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
                    else if (ddltransType.SelectedValue == "SWB")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = true;

                    }
                    else if (ddltransType.SelectedValue == "STB")
                    {
                        ShowTransactionType(2);
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
                    else if (ddltransType.SelectedValue == "Sel")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        ShowTransactionType(2);
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
                    if (ddltransType.SelectedValue == "SIP" || ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "CAF")
                    {
                        //BindFolioNumber(0);
                        if (mforderVo.accountid != 0)
                            ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {

                        if (mforderVo.accountid != 0)
                        {
                            BindFolioNumber(0);
                            ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        }
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    txtReceivedDate.Enabled = false;
                    txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
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
                    btnViewReport.Visible = true;
                    btnViewInPDF.Visible = true;
                    btnViewInDOC.Visible = true;
                    lnlBack.Visible = true;
                    lnkDelete.Visible = true;
                }
            }

            else if (action == "View")
            {
                if (mforderVo != null && orderVo != null)
                {
                    SetEditViewMode(true);
                    orderId = orderVo.OrderId;
                    txtCustomerName.Enabled = false;
                    txtCustomerName.Text = mforderVo.CustomerName;
                    if (orderVo.CustomerId != 0)
                        hdnCustomerId.Value = orderVo.CustomerId.ToString();
                    BindPortfolioDropdown(orderVo.CustomerId);
                    customerVo = customerBo.GetCustomer(orderVo.CustomerId);
                    lblGetBranch.Text = mforderVo.BMName;
                    lblGetRM.Text = mforderVo.RMName;
                    lblgetPan.Text = mforderVo.PanNo;
                    ddltransType.Enabled = false;
                    ddltransType.SelectedValue = mforderVo.TransactionCode;
                    txtOrderDate.SelectedDate = orderVo.OrderDate;
                    lblGetOrderNo.Text = mforderVo.OrderNumber.ToString();
                    hdnType.Value = mforderVo.TransactionCode;
                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "ABY")
                    {
                        ShowTransactionType(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        ShowTransactionType(1);
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
                    else if (ddltransType.SelectedValue == "SWB")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = true;

                    }
                    else if (ddltransType.SelectedValue == "STB")
                    {
                        ShowTransactionType(2);
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
                    else if (ddltransType.SelectedValue == "Sel")
                    {
                        ShowTransactionType(2);
                        trScheme.Visible = false;
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        ShowTransactionType(2);
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
                    if (ddltransType.SelectedValue == "CAF")
                    {
                        ShowTransactionType(3);
                        lblAMC.Visible = false; ddlAMCList.Visible = false;
                        lblCategory.Visible = false; ddlCategory.Visible = false;
                        lblSearchScheme.Visible = false; ddlAmcSchemeList.Visible = false;
                        lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                        spnAMC.Visible = false; spnScheme.Visible = false;
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
                        ddlAmcSchemeList.SelectedValue = mforderVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = mforderVo.SchemePlanCode.ToString();
                        BindSchemeSwitch();
                        ddlSchemeSwitch.SelectedValue = mforderVo.SchemePlanSwitch.ToString();
                    }
                    portfolioId = mforderVo.portfolioId;
                    if (ddltransType.SelectedValue == "SIP" || ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "CAF")
                    {
                        //BindFolioNumber(0);
                        if (mforderVo.accountid != 0)
                            ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {

                        if (mforderVo.accountid != 0)
                        {
                            BindFolioNumber(0);
                            ddlFolioNumber.SelectedValue = mforderVo.accountid.ToString();
                        }
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    txtReceivedDate.Enabled = false;
                    txtReceivedDate.SelectedDate = orderVo.ApplicationReceivedDate;
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

                    Session["mforderVo"] = mforderVo;
                    Session["orderVo"] = orderVo;

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = false;
                    //trReportButtons.Visible = true;
                    btnImgAddCustomer.Visible = false;
                    if(userType == "bm")
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
                    btnViewReport.Visible = true;
                    btnViewInPDF.Visible = true;
                    btnViewInDOC.Visible = true;

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
                trOrderType.Visible = false;
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
                trOrderType.Visible = true;
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
                           
                for (i = 0; i <= ISAList.Rows.Count; i++)
                {
                    
                }
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
                }

            }

        }
        protected void txtCustomerId_ValueChanged1(object sender, EventArgs e)
        {
            
           
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                //trJointHoldersList.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", " ShowIsa();", true);
                ddlAMCList.Enabled = true;
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                lblGetBranch.Text = customerVo.BranchName;
                lblGetRM.Text = customerVo.RMName;
                lblgetPan.Text = customerVo.PANNum;
                hdnCustomerId.Value = txtCustomerId.Value;
                customerId = int.Parse(txtCustomerId.Value);
                BindBank(customerId);
                BindPortfolioDropdown(customerId);
                ddltransType.SelectedIndex = 0;
                BindISAList();
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
            //    //ClearAllFields();

            }
        }

        private void BindBank(int customerId)
        {
            DataSet dsBankName = mfOrderBo.GetCustomerBank(customerId);
            if (dsBankName.Tables[0].Rows.Count > 0)
            {
                ddlBankName.DataSource = dsBankName;
                ddlBankName.DataValueField = dsBankName.Tables[0].Columns["CB_CustBankAccId"].ToString();
                ddlBankName.DataTextField = dsBankName.Tables[0].Columns["CB_BankName"].ToString();
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
        private void ClearAllFields()
        {


            ddltransType.SelectedIndex = 0;
            txtReceivedDate.SelectedDate = null;
            txtApplicationNumber.Text = "";
            BindAMC(0);
            ddlAMCList.SelectedIndex = 0;
            BindCategory();
            ddlCategory.SelectedIndex = 0;
            BindScheme(0);
            ddlAmcSchemeList.SelectedIndex = 0;
            //ddlPortfolio.SelectedIndex = 0;
            BindFolioNumber(0);
            ddlFolioNumber.SelectedIndex = -1;
            txtFutureDate.SelectedDate = null;
            txtFutureTrigger.Text = "";
            txtAmount.Text = "";
            ddlPaymentMode.SelectedIndex = -1;
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
                        dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 0, 1);
                    else
                        dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, int.Parse(txtCustomerId.Value));
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
        private void BindFolioNumber(int Fflag)
        {
            DataSet dsgetfolioNo = new DataSet();
            DataTable dtgetfolioNo;
            try
            {
                if (ddlAMCList.SelectedIndex != 0 && ddlAmcSchemeList.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue);
                    schemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
                    if (txtCustomerId.Value == "")
                        dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 1);
                    else
                        dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value),int.Parse(ddlCustomerISAAccount.SelectedItem.Value));
                }
                else
                {
                    if (txtCustomerId.Value == "")
                        dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 0);
                    else
                        dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value), int.Parse(ddlCustomerISAAccount.SelectedItem.Value));
                }
                if (dsgetfolioNo.Tables[0].Rows.Count > 0)
                {
                    dtgetfolioNo = dsgetfolioNo.Tables[0];
                    ddlFolioNumber.DataSource = dtgetfolioNo;
                    ddlFolioNumber.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolioNumber.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolioNumber.DataBind();
                    hdnAccountId.Value = ddlFolioNumber.SelectedItem.Text;
                }
                else
                {
                    ddlFolioNumber.Items.Clear();
                    ddlFolioNumber.DataSource = null;
                    ddlFolioNumber.DataBind();
                }
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
            lblFolioNumber.Visible = true; ddlFolioNumber.Visible = true;
            spnAMC.Visible = true; spnScheme.Visible = true;
            CompareValidator1.Visible = true; CompareValidator2.Visible = true;

            if (txtCustomerName.Text == "")
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
                        //BindFolioNumber(0);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        BindAMC(0);
                        BindScheme(0);
                        //BindFolioNumber(0);
                        trFrequency.Visible = true;
                        trSIPStartDate.Visible = true;
                    }
                    else
                    {
                        BindAMC(1);
                        BindScheme(1);
                        //BindFolioNumber(1);
                        trFrequency.Visible = false;
                        trSIPStartDate.Visible = false;
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
                    }
                    else
                    {
                        trFrequencySTP.Visible = false;
                        trSTPStart.Visible = false;
                        trScheme.Visible = false;

                    }
                    BindAMC(1);
                    BindScheme(1);
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
                    lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                    spnAMC.Visible = false; spnScheme.Visible = false;
                    CompareValidator1.Visible = false; CompareValidator2.Visible = false;
                }


                btnSubmit.Visible = true;
            }
            btnViewInPDFNew.Visible = true;
            btnViewInDOCNew.Visible = true;
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

        protected void ddlAMCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMCList.SelectedIndex != 0)
            {
                hdnAmcCode.Value = ddlAMCList.SelectedItem.Text;
                amcCode = int.Parse(ddlAMCList.SelectedValue);
                if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "SIP")
                {
                    BindScheme(0);
                    BindFolioNumber(0);
                }
                else
                {
                    BindScheme(1);
                    //BindFolioNumber(1);
                }
                BindSchemeSwitch();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex != 0)
            {
                if (ddlAMCList.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue);
                    categoryCode = ddlCategory.SelectedValue;
                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "SIP")
                        BindScheme(0);
                    else
                        BindScheme(1);
                }
            }
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
                        BindFolioNumber(1);
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
                    BindFolioNumber(0);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>();
            SaveOrderDetails();
            OrderIds = mfOrderBo.CreateCustomerMFOrderDetails(orderVo, mforderVo);
            rgvOrderSteps.Visible = true;
            orderId = int.Parse(OrderIds[0].ToString());
            Session["CO_OrderId"] = orderId;
            orderVo.OrderId = orderId;
            rgvOrderSteps.Enabled = true;
            BindOrderStepsGrid();
            SetEditViewMode(true);
            btnSubmit.Visible = false;
            lnkBtnEdit.Visible = true;
            lnlBack.Visible = false;
            imgBtnRefereshBank.Enabled = false;
            btnViewReport.Visible = true;
            btnViewInPDF.Visible = true;
            btnViewInDOC.Visible = true;
            btnViewInPDFNew.Visible = false;
            btnViewInDOCNew.Visible = false;
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
            orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            orderVo.ApplicationNumber = txtApplicationNumber.Text;
            if (ddlAMCList.SelectedIndex != 0)
                mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            else
                mforderVo.Amccode = 0;
            mforderVo.category = ddlCategory.SelectedValue;
            if (ddlAmcSchemeList.SelectedIndex != 0)
                mforderVo.SchemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
            else
                mforderVo.SchemePlanCode = 0;
            mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            if (ddlFolioNumber.SelectedIndex != -1)
                mforderVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
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
            OrderIds = mfOrderBo.CreateCustomerMFOrderDetails(orderVo, mforderVo);
            rgvOrderSteps.Visible = false;
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
                //txtOrderDate.Enabled = false;
                //ddlBranch.Enabled = false;
                //ddlRM.Enabled = false;
                txtCustomerName.Enabled = false;
                btnImgAddCustomer.Enabled = false;
                ddltransType.Enabled = false;
                ddlPortfolio.Enabled = false;
                ddlFolioNumber.Enabled = false;
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

                txtCorrAdrLine1.Enabled = false;
                txtCorrAdrLine2.Enabled = false;
                txtCorrAdrLine3.Enabled = false;
                txtLivingSince.Enabled = false;
                txtCorrAdrCity.Enabled = false;
                ddlCorrAdrState.Enabled = false;
                txtCorrAdrPinCode.Enabled = false;
                ddlCorrAdrCountry.Enabled = false;

                btnSubmit.Enabled = false;
                btnAddMore.Visible = false;


            }
            else
            {
                //txtOrederNumber.Enabled = true;
                //txtOrderDate.Enabled = true;
                //ddlBranch.Enabled = true;
                //ddlRM.Enabled = true;
                txtCustomerName.Enabled = false;
                btnImgAddCustomer.Enabled = false;
                ddltransType.Enabled = true;
                ddlPortfolio.Enabled = true;
                ddlFolioNumber.Enabled = true;
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

                txtCorrAdrLine1.Enabled = true;
                txtCorrAdrLine2.Enabled = true;
                txtCorrAdrLine3.Enabled = true;
                txtLivingSince.Enabled = true;
                txtCorrAdrCity.Enabled = true;
                ddlCorrAdrState.Enabled = true;
                txtCorrAdrPinCode.Enabled = true;
                ddlCorrAdrCountry.Enabled = true;

                btnSubmit.Enabled = true;
                btnAddMore.Visible = false;

            }


        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            SetEditViewMode(false);
            ViewForm = "Edit";
            lnkDelete.Visible = true;
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
                    lnlBack.Visible = true;
                }
            }
            else
            {
                SetControls("Entry", mforderVo, orderVo);
            }
            btnSubmit.Visible = false;
            rgvOrderSteps.Enabled = true;
            btnAddMore.Visible = false;
            btnUpdate.Visible = true;
            lnkBtnEdit.Visible = false;
        }

        protected void lnlBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FromPage"] != null)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerOrderList','none');", true);
            }
            else if (Request.QueryString["action"] != null)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('OrderMIS','none');", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<int> OrderIds = new List<int>();
            UpdateMFOrderDetails();
            mfOrderBo.UpdateCustomerMFOrderDetails(orderVo, mforderVo);
            SetEditViewMode(true);
            imgBtnRefereshBank.Enabled = false;
            btnUpdate.Visible = false;
            btnViewInPDFNew.Visible = false;
            btnViewInDOCNew.Visible = false;
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
            orderVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            orderVo.ApplicationNumber = txtApplicationNumber.Text;
            if (ddlAMCList.SelectedIndex != -1)
                mforderVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            else
                mforderVo.Amccode = 0;
            mforderVo.category = ddlCategory.SelectedValue;
            if (ddlAmcSchemeList.SelectedIndex != -1)
                mforderVo.SchemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
            else
                mforderVo.SchemePlanCode = 0;
            mforderVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            if (ddlFolioNumber.SelectedIndex != -1)
                mforderVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
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

            if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STP" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
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
                    txtOrderDate.SelectedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                    lblGetOrderNo.Text = ((mfOrderBo.GetOrderNumber()) + 1).ToString();
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

                }

            }

        }

    }
}
