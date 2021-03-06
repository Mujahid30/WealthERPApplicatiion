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
using System.Configuration;
using VoOps;
using iTextSharp.text.pdf;
using System.IO;


namespace WealthERP.OPS
{
    public partial class OrderEntry : System.Web.UI.UserControl
    {
        PortfolioBo portfolioBo = new PortfolioBo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo;
        RMVo rmVo = new RMVo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        OperationBo operationBo = new OperationBo();
        OperationVo operationVo=new OperationVo();
        AssetBo assetBo = new AssetBo();
        int amcCode;
        int accountId;
        string categoryCode;
        int portfolioId;
        int schemePlanCode;
        int customerId;
        string path;
        int flag = 0;
        int Sflag = 0;
        int Fflag=0;
        int orderNumber = 0;
        string ViewForm = string.Empty;
        DataTable dtBankName = new DataTable();

        DataTable dtFrequency;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            orderNumber = operationBo.GetOrderNumber();
            orderNumber = orderNumber + 1;
            //txtOrderDate.SelectedDate = DateTime.Now;
            lblGetOrderNo.Text = orderNumber.ToString();
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Request.QueryString["action"] != null)
            {
                ViewForm = Request.QueryString["action"].ToString();
                operationVo = (OperationVo)Session["operationVo"];
                txtOrderDate.SelectedDate = operationVo.OrderDate;
                lblGetOrderNo.Text = operationVo.OrderNumber.ToString();
            }
            if (Request.QueryString["FromPage"] != null)
            {
                lnkBtnEdit.Visible = false;
            }
          
            if (Session["operationVo"] != null)
            {
                operationVo = (OperationVo)Session["operationVo"];
            }
            lnlBack.Visible = false;
            msgRecordStatus.Visible = false;
            rmVo = (RMVo)Session[SessionContents.RmVo];
            //customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            //customerId = customerVo.CustomerId;
            int bmID = rmVo.RMId;
            
            lnkBtnEdit.Visible = false;
            btnGeneratePDF.Visible = false;
            if(chkCA.Checked==false)
              ddlOrderStatus.Enabled = false;
            if (!IsPostBack)
            {
                hyperLinkFillablePdfForm.Visible = false;
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
                trReportButtons.Visible = false;
                if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                {
                    txtCustomerName_autoCompleteExtender.ContextKey = advisorVo.advisorId.ToString();
                    txtCustomerName_autoCompleteExtender.ServiceMethod = "GetAdviserCustomerName";

                }
                btnAddMore.Visible = true;
                trSectionTwo10.Visible = false;
                //trFutureTrigger.Visible = false;
                //ShowHideFields(0);
                //BindBranchDropDown();
                //BindRMDropDown();
                BindOrderStatus();
                
                lblOrderPendingReason.Visible = false;
                ddlOrderPendingReason.Visible = false;
                BindCategory();
                BindState();
                BindFrequency();
                 ShowHideFields(1);
                ShowTransactionType(4);
                btnSubmit.Visible = true;
                cvAppRcvDate.ValueToCompare = DateTime.Now.ToShortDateString();
                cvOrderDate.ValueToCompare = DateTime.MinValue.ToShortDateString();
                cvFutureDate1.ValueToCompare = DateTime.Now.ToShortDateString();
                btnUpdate.Visible = false;
                BindBankName();
                 
                if (operationVo != null)
                {
                    if (ViewForm == "View")
                    {
                        SetControls("View", operationVo);
                        btnAddMore.Visible = false;
                    }
                    else if (ViewForm == "Edit")
                    {
                        SetControls("Edit", operationVo);
                        btnAddMore.Visible = false;
                    }
                    else if (ViewForm == "entry")
                    {
                        SetControls("Entry", operationVo);
                    }
                    else
                    {
                        cvAppRcvDate.ValueToCompare = DateTime.Now.ToShortDateString();
                        cvOrderDate.ValueToCompare = DateTime.Now.ToShortDateString();
                        cvFutureDate1.ValueToCompare = DateTime.Now.ToShortDateString();
                    }
                }
                else
                {
                    SetControls("Entry", operationVo);
                    
                }

            }
            msgUpdate.Visible = false;
            

        }
        private void BindBankName()
        {
            dtBankName = XMLBo.GetBankName(path);
            ddlBankName.DataSource = dtBankName;
            ddlBankName.DataTextField = "BankFullName";
            ddlBankName.DataValueField = "BankNameCode";
            ddlBankName.DataBind();
            ddlBankName.Items.RemoveAt(0);
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

        private void SetControls(string action, OperationVo operationVo)
        {
            if (action == "Entry")
            {
                if (operationVo == null)
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
                    ddlOrderPendingReason.SelectedIndex = 0;
                    ddlOrderStatus.SelectedIndex = 0;
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
                if (operationVo != null)
                {
                    SetEditViewMode(false);
                    txtCustomerName.Enabled = false;
                    txtCustomerName.Text = operationVo.CustomerName;
                    if (operationVo.CustomerId != 0)
                        hdnCustomerId.Value = operationVo.CustomerId.ToString();
                    BindPortfolioDropdown(operationVo.CustomerId);
                    customerVo = customerBo.GetCustomer(operationVo.CustomerId);
                    lblGetBranch.Text = operationVo.BMName;
                    lblGetRM.Text = operationVo.RMName;
                    lblgetPan.Text = operationVo.PanNo;
                    ddltransType.SelectedValue = operationVo.TransactionCode;
                    txtOrderDate.SelectedDate = operationVo.OrderDate;
                    
                    lblGetOrderNo.Text = operationVo.OrderNumber.ToString();
                    hdnType.Value = operationVo.TransactionCode;
                    
                    if (ddltransType.SelectedValue == "CAF")
                    {
                        ShowTransactionType(3);
                        lblAMC.Visible = false; ddlAMCList.Visible = false;
                        Label7.Visible = false; ddlCategory.Visible = false;
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
                        ddlAMCList.SelectedValue = operationVo.Amccode.ToString();
                        BindCategory();
                        ddlCategory.SelectedValue = operationVo.category;
                        BindPortfolioDropdown(operationVo.CustomerId);
                        ddlPortfolio.SelectedValue = operationVo.portfolioId.ToString();
                        BindScheme(0);
                        ddlAmcSchemeList.SelectedItem.Value= operationVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = operationVo.SchemePlanCode.ToString();
                        
                    }
                    portfolioId = operationVo.portfolioId;
                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "ABY")
                        ShowTransactionType(1);
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        ShowTransactionType(1);
                        trSipFrequency.Visible = true;
                        trSIPDate.Visible = true;
                        ddlFrequencySIP.SelectedValue = operationVo.FrequencyCode;
                        if (operationVo.StartDate != DateTime.MinValue)
                            txtstartDateSIP.SelectedDate = operationVo.StartDate;
                        else
                            txtstartDateSIP.SelectedDate = null;
                        if (operationVo.EndDate != DateTime.MinValue)
                            txtendDateSIP.SelectedDate = operationVo.EndDate;
                        else
                            txtendDateSIP.SelectedDate = null;
                    }
                    else if (ddltransType.SelectedValue == "SWB")
                    {
                        ShowTransactionType(2);
                        trSell3.Visible = true;

                    }
                    else if (ddltransType.SelectedValue == "STB")
                    {
                        ShowTransactionType(2);
                        trfrequencySTP.Visible = true;
                        trDateSTP.Visible = true;
                        trSell3.Visible = true;
                        if (operationVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = operationVo.FrequencyCode;
                        if (operationVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = operationVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (operationVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = operationVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    else if (ddltransType.SelectedValue == "Sel")
                    {
                        ShowTransactionType(2);
                        trSell3.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        ShowTransactionType(2);
                        trSell3.Visible = false;
                        trfrequencySTP.Visible = true;
                        trDateSTP.Visible = true;
                        if (operationVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = operationVo.FrequencyCode;
                        if (operationVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = operationVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (operationVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = operationVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    if (ddltransType.SelectedValue == "SIP" || ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "CAF")
                    {
                        //BindFolioNumber(0);
                        if (operationVo.accountid != 0)
                            ddlFolioNumber.SelectedValue = operationVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {
                        BindFolioNumber(0);
                        if (operationVo.accountid != 0)
                            ddlFolioNumber.SelectedValue = operationVo.accountid.ToString();
                        else
                            ddlFolioNumber.SelectedValue = "";
                    }
                    txtReceivedDate.Enabled = false;
                    txtReceivedDate.SelectedDate = operationVo.ApplicationReceivedDate;
                    txtApplicationNumber.Enabled = false;
                    txtApplicationNumber.Text = operationVo.ApplicationNumber;
                    txtOrderDate.SelectedDate = operationVo.OrderDate;
                    lblGetOrderNo.Text = operationVo.OrderNumber.ToString();
                    ddlOrderStatus.SelectedValue = operationVo.StatusCode;
                    BindRejectStatus(operationVo.StatusCode);
                    if (operationVo.StatusCode == "OMEX" || operationVo.StatusCode == "OMIP")
                    {
                        lblOrderPendingReason.Visible = false;
                        ddlOrderPendingReason.Visible = false;
                    }
                    else
                    {
                        lblOrderPendingReason.Visible = true; ddlOrderPendingReason.Visible = true;
                        ddlOrderPendingReason.SelectedValue = operationVo.StatusReasonCode;
                    }
                    if (operationVo.FutureExecutionDate != DateTime.MinValue)
                        txtFutureDate.SelectedDate = operationVo.FutureExecutionDate;
                    else
                        txtFutureDate.SelectedDate = null;
                    if (operationVo.IsImmediate == 1)
                        rbtnImmediate.Checked = true;
                    else
                    {
                        rbtnFuture.Checked = true;
                        trSectionTwo10.Visible = true;
                    }
                    txtFutureTrigger.Text = operationVo.FutureTriggerCondition;
                    txtAmount.Text = operationVo.Amount.ToString();
                    if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                    {
                        if (operationVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = operationVo.Amount.ToString();
                        }
                        else
                        {
                            rbtAmount.Checked = false;
                            txtNewAmount.Text = "";
                        }

                        if (operationVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = operationVo.Units.ToString();
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
                    
                    ddlPaymentMode.SelectedValue= operationVo.PaymentMode;
                    txtPaymentNumber.Text = operationVo.ChequeNumber;
                    if (operationVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = operationVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate =null;
                    ddlBankName.SelectedItem.Text = operationVo.BankName;
                    txtBranchName.Text =operationVo.BranchName;
                    //lblGetAvailableAmount.Text = ;
                    //lblGetAvailableUnits.Text = "";
                    //BindSchemeSwitch();
                    //if( operationVo.SchemePlanSwitch !=0)
                    //    ddlSchemeSwitch.SelectedValue = operationVo.SchemePlanSwitch.ToString();
                    txtCorrAdrLine1.Text = operationVo.AddrLine1;
                    txtCorrAdrLine2.Text = operationVo.AddrLine2;
                    txtCorrAdrLine3.Text = operationVo.AddrLine3;
                    if (operationVo.LivingSince != DateTime.MinValue)
                        txtLivingSince.SelectedDate = operationVo.LivingSince;
                    else
                        txtLivingSince.SelectedDate =null;
                    //txtLivingSince.Text = operationVo.LivingSince.ToShortDateString();
                    txtCorrAdrCity.Text = operationVo.City;
                    if(!string.IsNullOrEmpty(operationVo.State.ToString().Trim()))
                        ddlCorrAdrState.SelectedItem.Text = operationVo.State;
                    txtCorrAdrPinCode.Text = operationVo.Pincode;

                    chkCA.Enabled = false;
                    if (operationVo.IsApproved == 1)
                        chkCA.Checked = true;
                    else
                        chkCA.Checked = false;

                    btnSubmit.Visible = false;
                    btnUpdate.Visible = true;
                    trReportButtons.Visible = true;
                    btnAddCustomer.Visible = false;
                    btnAddMore.Visible = false;
                }
            }

            else if (action == "View")
            {
                if (operationVo != null)
                {
                    SetEditViewMode(true);
                    txtCustomerName.Enabled = false;
                    txtCustomerName.Text = operationVo.CustomerName;
                    if (operationVo.CustomerId != 0)
                        hdnCustomerId.Value = operationVo.CustomerId.ToString();
                    BindPortfolioDropdown(operationVo.CustomerId);
                    customerVo = customerBo.GetCustomer(operationVo.CustomerId);
                    lblGetBranch.Text = operationVo.BMName;
                    lblGetRM.Text = operationVo.RMName;
                    lblgetPan.Text = operationVo.PanNo;
                    ddltransType.Enabled = false;
                    ddltransType.SelectedValue = operationVo.TransactionCode;
                    txtOrderDate.SelectedDate = operationVo.OrderDate;
                    lblGetOrderNo.Text = operationVo.OrderNumber.ToString();
                    hdnType.Value = operationVo.TransactionCode;
                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "ABY")
                        ShowTransactionType(1);
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        ShowTransactionType(1);
                        trSipFrequency.Visible = true;
                        trSIPDate.Visible = true;
                        ddlFrequencySIP.Enabled = false;
                        ddlFrequencySIP.SelectedValue = operationVo.FrequencyCode;
                        txtstartDateSIP.Enabled = false;
                        if (operationVo.StartDate != DateTime.MinValue)
                            txtstartDateSIP.SelectedDate = operationVo.StartDate;
                        else
                            txtstartDateSIP.SelectedDate = null;
                        if (operationVo.EndDate != DateTime.MinValue)
                            txtendDateSIP.SelectedDate = operationVo.EndDate;
                        else
                            txtendDateSIP.SelectedDate = null;
                    }
                    else if (ddltransType.SelectedValue == "SWB")
                    {
                        ShowTransactionType(2);
                        trSell3.Visible = true;

                    }
                    else if (ddltransType.SelectedValue == "STB" )
                    {
                        ShowTransactionType(2);
                        trfrequencySTP.Visible = true;
                        trDateSTP.Visible = true;
                        trSell3.Visible = true;
                        if (operationVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = operationVo.FrequencyCode;
                        else
                            ddlFrequencySTP.SelectedValue = "DA";
                        if (operationVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = operationVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (operationVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = operationVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    else if (ddltransType.SelectedValue == "Sel")
                    {
                        ShowTransactionType(2);
                        trSell3.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        ShowTransactionType(2);
                        trSell3.Visible = false;
                        trfrequencySTP.Visible = true;
                        trDateSTP.Visible = true;
                        if (operationVo.FrequencyCode != "")
                            ddlFrequencySTP.SelectedValue = operationVo.FrequencyCode;
                        else
                            ddlFrequencySTP.SelectedValue = "DA";
                        if (operationVo.StartDate != DateTime.MinValue)
                            txtstartDateSTP.SelectedDate = operationVo.StartDate;
                        else
                            txtstartDateSTP.SelectedDate = null;
                        if (operationVo.EndDate != DateTime.MinValue)
                            txtendDateSTP.SelectedDate = operationVo.EndDate;
                        else
                            txtendDateSTP.SelectedDate = null;
                    }
                    if (ddltransType.SelectedValue == "CAF")
                    {
                        ShowTransactionType(3);
                        lblAMC.Visible = false; ddlAMCList.Visible = false;
                        Label7.Visible = false; ddlCategory.Visible = false;
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
                        ddlAMCList.SelectedValue = operationVo.Amccode.ToString();
                        BindCategory();
                        ddlCategory.SelectedValue = operationVo.category;
                        BindPortfolioDropdown(operationVo.CustomerId);
                        ddlPortfolio.SelectedValue = operationVo.portfolioId.ToString();
                        BindScheme(0);
                        ddlAmcSchemeList.SelectedValue = operationVo.SchemePlanCode.ToString();
                        hdnSchemeCode.Value = operationVo.SchemePlanCode.ToString();
                        BindSchemeSwitch();
                        ddlSchemeSwitch.SelectedValue = operationVo.SchemePlanSwitch.ToString();
                    }
                    portfolioId = operationVo.portfolioId;
                    if (ddltransType.SelectedValue == "SIP" || ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "CAF")
                    {
                        //BindFolioNumber(0);
                        if (operationVo.accountid != 0)
                            ddlFolioNumber.SelectedValue = operationVo.accountid.ToString();
                        //else
                        //    ddlFolioNumber.SelectedValue = "";
                    }
                    else
                    {
                        BindFolioNumber(0);
                        if (operationVo.accountid != 0)
                            ddlFolioNumber.SelectedValue = operationVo.accountid.ToString();
                        else
                            ddlFolioNumber.SelectedValue = "";
                    }
                    txtReceivedDate.Enabled = false;
                    txtReceivedDate.SelectedDate = operationVo.ApplicationReceivedDate;
                    txtApplicationNumber.Enabled = false;
                    txtApplicationNumber.Text = operationVo.ApplicationNumber;
                    txtOrderDate.SelectedDate = operationVo.OrderDate;
                    lblGetOrderNo.Text = operationVo.OrderNumber.ToString();
                    ddlOrderStatus.SelectedValue = operationVo.StatusCode;
                    BindRejectStatus(operationVo.StatusCode);
                    if (operationVo.StatusCode == "OMEX" || operationVo.StatusCode == "OMIP")
                    {
                        lblOrderPendingReason.Visible = false;
                        ddlOrderPendingReason.Visible = false;
                    }
                    else
                    {
                        lblOrderPendingReason.Visible = true;
                        ddlOrderPendingReason.Visible = true;
                        ddlOrderPendingReason.SelectedValue = operationVo.StatusReasonCode;
                    }
                    ddlOrderPendingReason.SelectedValue = operationVo.StatusReasonCode;
                    if (operationVo.StatusCode == "OMEX")
                        lnkBtnEdit.Visible = false;
                    else
                        lnkBtnEdit.Visible = true;

                    if (operationVo.IsImmediate == 1)
                        rbtnImmediate.Checked = true;
                    else
                    {
                        rbtnFuture.Checked = true;
                        trSectionTwo10.Visible = true;
                    }
                    if (operationVo.FutureExecutionDate != DateTime.MinValue)
                        txtFutureDate.SelectedDate = operationVo.FutureExecutionDate;
                    else
                        txtFutureDate.SelectedDate = null;
                    txtFutureTrigger.Text = operationVo.FutureTriggerCondition;
                    txtAmount.Text = operationVo.Amount.ToString();
                    if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                    {
                        if (operationVo.Amount != 0)
                        {
                            rbtAmount.Checked = true;
                            txtNewAmount.Text = operationVo.Amount.ToString();
                        }
                        else
                        {
                            rbtAmount.Checked = false;
                            txtNewAmount.Text = "";
                        }

                        if (operationVo.Units != 0)
                        {
                            rbtUnit.Checked = true;
                            txtNewAmount.Text = operationVo.Units.ToString();
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
                    
                    ddlPaymentMode.SelectedValue = operationVo.PaymentMode;
                    txtPaymentNumber.Text = operationVo.ChequeNumber;
                    if (operationVo.PaymentDate != DateTime.MinValue)
                        txtPaymentInstDate.SelectedDate = operationVo.PaymentDate;
                    else
                        txtPaymentInstDate.SelectedDate = null;
                    ddlBankName.SelectedItem.Text = operationVo.BankName;
                    txtBranchName.Text = operationVo.BranchName;
                    //lblGetAvailableAmount.Text = ;
                    //lblGetAvailableUnits.Text = "";
                    
                    txtCorrAdrLine1.Text = operationVo.AddrLine1;
                    txtCorrAdrLine2.Text = operationVo.AddrLine2;
                    txtCorrAdrLine3.Text = operationVo.AddrLine3;
                    if (operationVo.LivingSince != DateTime.MinValue)
                        txtLivingSince.SelectedDate = operationVo.LivingSince;
                    else
                        txtLivingSince.SelectedDate = null;
                    txtCorrAdrCity.Text = operationVo.City;
                    ddlCorrAdrState.SelectedItem.Text = operationVo.State;
                    txtCorrAdrPinCode.Text = operationVo.Pincode;

                    if (operationVo.IsApproved == 1)
                        chkCA.Checked = true;
                    else
                        chkCA.Checked = false;

                    Session["operationVo"] = operationVo;
                    btnSubmit.Visible = false;
                    btnUpdate.Visible = false;
                    trReportButtons.Visible = true;
                    btnAddCustomer.Visible = false;
                    if (Request.QueryString["FromPage"] != null)
                    {
                        lnkBtnEdit.Visible = false;
                        btnGeneratePDF.Visible = false;
                        lnlBack.Visible = true;
                    }
                    
                }
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

        private void BindRejectStatus(string statusCode)
        {
            DataSet dsStatusReject;
            DataTable dtStatusReject;
            statusCode = ddlOrderStatus.SelectedValue;
            dsStatusReject = operationBo.GetRejectStatus(statusCode);
            dtStatusReject = dsStatusReject.Tables[0];
            if (dtStatusReject.Rows.Count > 0)
            {
                ddlOrderPendingReason.DataSource = dtStatusReject;
                ddlOrderPendingReason.DataValueField = dtStatusReject.Columns["XSR_StatusReasonCode"].ToString();
                ddlOrderPendingReason.DataTextField = dtStatusReject.Columns["XSR_StatusReason"].ToString();
                ddlOrderPendingReason.DataBind();
            }
            //ddlOrderPendingReason.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void BindAMC(int flag)
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;
            
            try
            {
                if (flag == 0)
                    dsProductAmc = productMFBo.GetProductAmc();
                else
                    dsProductAmc = operationBo.GetAMCForOrderEntry(flag, int.Parse(hdnCustomerId.Value));

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
                    if(txtCustomerId.Value =="")
                        dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode,1, 1);
                    else
                        dsScheme = operationBo.GetSchemeForOrderEntry(amcCode, categoryCode,Sflag,int.Parse(txtCustomerId.Value));
                }
                else if (ddlAMCList.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue.ToString());
                    categoryCode = ddlCategory.SelectedValue;
                    if (Sflag == 0)
                        dsScheme = productMFBo.GetSchemeName(amcCode, categoryCode, 0,1);
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
        protected void txtCustomerId_ValueChanged(object sender, EventArgs e)
        {
            //ShowHideFields(1);
            if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
            {
                customerVo = customerBo.GetCustomer(int.Parse(txtCustomerId.Value));
                Session["customerVo"] = customerVo;
                lblGetBranch.Text = customerVo.BranchName;
                lblGetRM.Text = customerVo.RMName;
                lblgetPan.Text = customerVo.PANNum;
                hdnCustomerId.Value = txtCustomerId.Value;
                customerId = int.Parse(txtCustomerId.Value);
                BindPortfolioDropdown(customerId);
                ddltransType.SelectedIndex = 0;
                CleanAllFields();
                //BindAMC(0);
                //BindCategory();
                //BindScheme(0);
                //BindFolioNumber(0);
            }
            
        }
        private void BindFolioNumber(int Fflag)
        {
            int IsaNo=3;
            DataSet dsgetfolioNo = new DataSet();
            DataTable dtgetfolioNo;
            try
            {
                if (ddlAMCList.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMCList.SelectedValue);
                    if (txtCustomerId.Value == "")
                        dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 1);
                    else
                        dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value),IsaNo,"");
                }
                else
                {
                    if (txtCustomerId.Value == "")
                        dsgetfolioNo = productMFBo.GetFolioNumber(portfolioId, amcCode, 0);
                    else
                        dsgetfolioNo = operationBo.GetFolioForOrderEntry(schemePlanCode, amcCode, Fflag, int.Parse(txtCustomerId.Value),IsaNo,"");
                }
                if (dsgetfolioNo.Tables.Count > 0)
                {
                    dtgetfolioNo = dsgetfolioNo.Tables[0];
                    ddlFolioNumber.DataSource = dtgetfolioNo;
                    ddlFolioNumber.DataTextField = dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolioNumber.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolioNumber.DataBind();
                    //ddlFolioNumber.Items.Insert(0, new ListItem("Select", "Select"));
                }
                else
                {
                    ddlFolioNumber.Items.Clear();
                    ddlFolioNumber.DataSource = null;
                    ddlFolioNumber.DataBind();
                    //ddlFolioNumber.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
       
        private void BindPortfolioDropdown(int customerId)
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
        }
        private void BindOrderStatus()
        {
            DataSet dsOrderStaus;
            DataTable dtOrderStatus;
            dsOrderStaus = operationBo.GetOrderStatus();
            dtOrderStatus = dsOrderStaus.Tables[0];
            if (dtOrderStatus.Rows.Count > 0)
            {
                ddlOrderStatus.DataSource = dtOrderStatus;
                ddlOrderStatus.DataValueField = dtOrderStatus.Columns["XS_StatusCode"].ToString();
                ddlOrderStatus.DataTextField = dtOrderStatus.Columns["XS_Status"].ToString();
                ddlOrderStatus.DataBind();
            }
            //ddlOrderStatus.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void rbtnImmediate_CheckedChanged(object sender, EventArgs e)
        {
            trSectionTwo10.Visible = false;

        }

        protected void rbtnFuture_CheckedChanged(object sender, EventArgs e)
        {
           trSectionTwo10.Visible = true;
        }
        //private void BindRMDropDown()
        //{
        //    AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        //    DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlRM.DataSource = dt;
        //        ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
        //        ddlRM.DataTextField = dt.Columns["RMName"].ToString();
        //        ddlRM.DataBind();
        //    }
        //    ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
        //}
        //private void BindBranchDropDown()
        //{

        //    RMVo rmVo = new RMVo();
        //    rmVo = (RMVo)Session[SessionContents.RmVo];
        //    int bmID = rmVo.RMId;

        //    UploadCommonBo uploadsCommonDao = new UploadCommonBo();
        //    DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
        //    if (ds != null)
        //    {
        //        ddlBranch.DataSource = ds;
        //        ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
        //        ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
        //        ddlBranch.DataBind();
        //    }
        //    ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));

        //}

        //private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        //{

        //    DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
        //    if (ds != null)
        //    {
        //        ddlRM.DataSource = ds.Tables[0]; ;
        //        ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
        //        ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
        //        ddlRM.DataBind();
        //    }
        //    ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));

        //}

        //protected void aplToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        //{
        //    if (e.Item.Value == "Edit")
        //    {
        //        ViewState["ActionEditViewMode"] = "Edit";
        //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerAssumptionsPreferencesSetup','login');", true);
        //        SetEditViewMode(false);
        //    }
        //    if (e.Item.Value == "PrintTransactionSlip")
        //    {
               
        //    }
        //}


        public void SetEditViewMode(bool Bool)
        {

            if (Bool)
            {

                //txtOrederNumber.Enabled = false;
                //txtOrderDate.Enabled = false;
                //ddlBranch.Enabled = false;
                //ddlRM.Enabled = false;
                txtCustomerName.Enabled = false;
                btnAddCustomer.Enabled = false;
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
                ddlOrderStatus.Enabled = false;
                ddlOrderPendingReason.Enabled = false;
                txtOrderDate.Enabled = false;

                txtAmount.Enabled = false;
                ddlPaymentMode.Enabled = false;
                txtPaymentInstDate.Enabled = false;
                txtPaymentNumber.Enabled = false;
                rbtAmount.Enabled = false;
                rbtUnit.Enabled = false;
                txtNewAmount.Enabled = false;
                ddlBankName.Enabled= false;
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

                chkCA.Enabled = false;
                
                btnSubmit.Enabled = false;
                btnUpdate.Visible = false;
                btnAddMore.Visible = false;
                

            }
            else
            {
                //txtOrederNumber.Enabled = true;
                //txtOrderDate.Enabled = true;
                //ddlBranch.Enabled = true;
                //ddlRM.Enabled = true;
                txtCustomerName.Enabled = true;
                btnAddCustomer.Enabled = true;
                ddltransType.Enabled = true;
                ddlPortfolio.Enabled = true;
                ddlFolioNumber.Enabled = true;
                //btnAddFolio.Enabled = true;
                ddlAMCList.Enabled = true;
                ddlAmcSchemeList.Enabled = true;
                ddlCategory.Enabled = true;
                txtReceivedDate.Enabled = true;
                txtApplicationNumber.Enabled = true;
                rbtnImmediate.Enabled = true;
                rbtnFuture.Enabled = true;
                txtFutureDate.Enabled = true;
                txtFutureTrigger.Enabled = true;
                ddlOrderStatus.Enabled = true;
                ddlOrderPendingReason.Enabled = true;
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

                chkCA.Enabled = false;

                btnSubmit.Enabled = true;
                btnUpdate.Visible = true;
                btnAddMore.Visible = true;

            }
        
        
        }

        
     

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            //int result = 1234;
            operationVo.CustomerId = int.Parse(txtCustomerId.Value);
            operationVo.CustomerName = txtCustomerName.Text;
            operationVo.BMName = lblGetBranch.Text;
            operationVo.RMName = lblGetRM.Text;
            operationVo.PanNo = lblgetPan.Text;
            operationVo.TransactionCode = ddltransType.SelectedValue;
            operationVo.ApplicationReceivedDate =DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            operationVo.ApplicationNumber = txtApplicationNumber.Text;
            if (ddlAMCList.SelectedIndex != 0)
                operationVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            else
                operationVo.Amccode = 0;
            operationVo.category = ddlCategory.SelectedValue;
            if (ddlAmcSchemeList.SelectedIndex != 0)
                operationVo.SchemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
            else
                operationVo.SchemePlanCode = 0;
            operationVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            if (ddlFolioNumber.SelectedIndex != -1)
                operationVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
            else
                operationVo.accountid = 0;
            operationVo.OrderDate = Convert.ToDateTime(txtOrderDate.SelectedDate);
            operationVo.OrderNumber = int.Parse(lblGetOrderNo.Text);
            operationVo.StatusCode = ddlOrderStatus.SelectedValue;
            operationVo.StatusReasonCode = ddlOrderPendingReason.SelectedValue;
            if (rbtnImmediate.Checked == true)
                operationVo.IsImmediate = 1;
            else
                operationVo.IsImmediate = 0;
            if (!string.IsNullOrEmpty((txtFutureDate.SelectedDate).ToString().Trim()))
                operationVo.FutureExecutionDate = DateTime.Parse(txtFutureDate.SelectedDate.ToString());
            else
                operationVo.FutureExecutionDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty((txtFutureTrigger.Text).ToString().Trim()))
                operationVo.FutureTriggerCondition = txtFutureTrigger.Text;
            else
                operationVo.FutureTriggerCondition = "";
            if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
                operationVo.Amount = double.Parse(txtAmount.Text);
            else
                operationVo.Amount = 0;
            if (!string.IsNullOrEmpty((lblGetAvailableUnits.Text).ToString().Trim()))
                operationVo.Units = double.Parse(lblGetAvailableUnits.Text);
            else
                operationVo.Units = 0;

            if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
            {
                if (rbtAmount.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        operationVo.Amount = double.Parse(txtNewAmount.Text);
                    else
                        operationVo.Amount = 0;
                }
                if (rbtUnit.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        operationVo.Units = double.Parse(txtNewAmount.Text);
                    else
                        operationVo.Units = 0;
                }
               
            }
            
            if (ddlPaymentMode.SelectedValue == "ES")
                operationVo.PaymentMode = "ES";
            else if (ddlPaymentMode.SelectedValue == "DF")
                operationVo.PaymentMode = "DF";
            else if (ddlPaymentMode.SelectedValue == "CQ")
                operationVo.PaymentMode = "CQ";
            if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
                operationVo.ChequeNumber = txtPaymentNumber.Text;
            else
                operationVo.ChequeNumber = "";
            if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
                operationVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
            else
                operationVo.PaymentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
                operationVo.BankName = ddlBankName.SelectedItem.Text;
            else
                operationVo.BankName = "";
            if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
                operationVo.BranchName = txtBranchName.Text;
            else
                operationVo.BranchName = "";
            if (ddlSchemeSwitch.SelectedValue != "")
            {
                if (ddlSchemeSwitch.SelectedIndex != 0)
                    operationVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
                operationVo.AddrLine1 = txtCorrAdrLine1.Text;
            else
                operationVo.AddrLine1 = "";
            if (txtCorrAdrLine2.Text != "" || txtCorrAdrLine2.Text!=null)
                operationVo.AddrLine2 = txtCorrAdrLine2.Text;
            else
                operationVo.AddrLine2 = "";
            if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
                operationVo.AddrLine3 = txtCorrAdrLine3.Text;
            else
                operationVo.AddrLine3 = "";
            if (!string.IsNullOrEmpty(txtLivingSince.SelectedDate.ToString().Trim()))
                operationVo.LivingSince = DateTime.Parse(txtLivingSince.SelectedDate.ToString());
            else
                operationVo.LivingSince = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
                operationVo.City = txtCorrAdrCity.Text;
            else
                operationVo.City = "";
            if (ddlCorrAdrState.SelectedIndex != 0)
                operationVo.State = ddlCorrAdrState.SelectedItem.Text;
            else
                operationVo.State = "";
            if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
                operationVo.Pincode = txtCorrAdrPinCode.Text;
            else
                operationVo.Pincode = "";
            operationVo.Country = ddlCorrAdrCountry.SelectedValue;

            if (chkCA.Checked == true)
                operationVo.IsApproved = 1;
            else
                operationVo.IsApproved = 0;

            if (ddltransType.SelectedValue == "SIP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
                    operationVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
                if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
                    operationVo.StartDate =DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
                else
                    operationVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
                    operationVo.EndDate =DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
                else
                    operationVo.EndDate = DateTime.MinValue;
            }
            else if (ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
                    operationVo.FrequencyCode = ddlFrequencySTP.SelectedValue;
                if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
                    operationVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
                else
                    operationVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
                    operationVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
                else
                    operationVo.EndDate = DateTime.MinValue;
            }

            operationBo.CreateMFOrderTracking(operationVo);
            trReportButtons.Visible = true;
            msgRecordStatus.Visible = true;

            txtCustomerName.Text = "";
            lblGetBranch.Text = "";
            lblGetRM.Text = "";
            lblgetPan.Text = "";
            CleanAllFields();
            lblGetOrderNo.Text = "";
            txtOrderDate.SelectedDate = null;
;
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            lnkBtnEdit.Visible = false;
            btnAddMore.Visible = true;
           

            //msgRecordStatus.Visible = true;
            //CleanAllFields();
                 
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "GoalFundPage", "loadcontrol('OrderMIS','?result=" + result + "');", true);
        }

        private void CleanAllFields()
        {
            //ddlBranch.SelectedIndex = 0;
            //ddlRM.SelectedIndex = 0;
            //txtCustomerName.Text = "";
            ddltransType.SelectedIndex = 0;
            txtReceivedDate.SelectedDate =null;
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
            //txtOrderDate.Text = "";
            //lblGetOrderNo.Text = "";
            //ddlOrderPendingReason.SelectedIndex = 0;
            //ddlOrderStatus.SelectedIndex = 0;
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
            ddlSchemeSwitch.SelectedIndex =-1;
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

        protected void ddltransType_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                        BindFolioNumber(0);
                        trSipFrequency.Visible = false;
                        trSIPDate.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "SIP")
                    {
                        BindAMC(0);
                        BindScheme(0);
                        BindFolioNumber(0);
                        trSipFrequency.Visible = true;
                        trSIPDate.Visible = true;
                    }
                    else
                    {
                        BindAMC(1);
                        BindScheme(1);
                        BindFolioNumber(1);
                        trSipFrequency.Visible = false;
                        trSIPDate.Visible = false;
                    }
                }
                else if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                {
                    ShowTransactionType(2);
                    if (ddltransType.SelectedValue == "SWB")
                    {
                        //BindSchemeSwitch();
                        trSell3.Visible = true;
                        trfrequencySTP.Visible = false;
                        trDateSTP.Visible = false;
                    }
                    else if (ddltransType.SelectedValue == "STB")
                    {
                        trSell3.Visible = true;
                        trfrequencySTP.Visible = true;
                        trDateSTP.Visible = true;
                    }
                    else if (ddltransType.SelectedValue == "SWP")
                    {
                        trSell3.Visible = false;
                        trfrequencySTP.Visible = true;
                        trDateSTP.Visible = true;
                    }
                    else
                    {
                        trfrequencySTP.Visible = false;
                        trDateSTP.Visible = false;

                    }
                    BindAMC(1);
                    BindScheme(1);
                    BindFolioNumber(1);

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
                    Label7.Visible = false; ddlCategory.Visible = false;
                    lblSearchScheme.Visible = false; ddlAmcSchemeList.Visible = false;
                    lblFolioNumber.Visible = false; ddlFolioNumber.Visible = false;
                    spnAMC.Visible = false; spnScheme.Visible = false;
                    CompareValidator1.Visible = false; CompareValidator2.Visible = false;
                    //BindAMC(1);
                    //BindScheme(1);
                    //BindFolioNumber(1);
                }

                btnSubmit.Visible = true;
                //btnAddMore.Visible = true;
            }
        }



        private void BindSchemeSwitch()
        {
            DataSet dsSwitchScheme = new DataSet();
            DataTable dtSwitchScheme;
            if(ddlAMCList.SelectedIndex!=0)
            {
                amcCode=int.Parse(ddlAMCList.SelectedValue);
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


        protected void ShowTransactionType(int type)
        {
            if (type == 0)
            {
                trAddress1.Visible = false;
                trAddress2.Visible = false;
                trAddress3.Visible = false;
                trAddress4.Visible = false;
                trAddress5.Visible = false;
                trAddress6.Visible = false;
                trAddress7.Visible = false;
                trAddress8.Visible = false;
                trAddress9.Visible = false;
                trAddress10.Visible = false;

                trPurchase.Visible = false;
                trPurchase1.Visible = false;
                trPurchase2.Visible = false;
                trPurchase3.Visible = false;
                trPurchase4.Visible = false;

                trSell1.Visible = false;
                trSell2.Visible = false;
                trSell3.Visible = false;
                trSell4.Visible = false;

                trBtnSubmit.Visible = false;
                trSipFrequency.Visible = false;
                trSIPDate.Visible = false;
                trfrequencySTP.Visible = false;
                trDateSTP.Visible = false;

               trBtnSubmit.Visible = false;
 
            }
            else if (type == 1)
            {
                trAddress1.Visible = false;
                trAddress2.Visible = false;
                trAddress3.Visible = false;
                trAddress4.Visible = false;
                trAddress5.Visible = false;
                trAddress6.Visible = false;
                trAddress7.Visible = false;
                trAddress8.Visible = false;
                trAddress9.Visible = false;
                trAddress10.Visible = false;

                trPurchase.Visible = true;
                trPurchase1.Visible = true;
                trPurchase2.Visible = true;
                trPurchase3.Visible = true;
                trPurchase4.Visible = true;
                trSipFrequency.Visible = true;
                trSIPDate.Visible = true;

                trSell1.Visible = false;
                trSell2.Visible = false;
                trSell3.Visible = false;
                trSell4.Visible = false;

                trBtnSubmit.Visible = false;
                trSipFrequency.Visible = false;
                trSIPDate.Visible = false;
                trfrequencySTP.Visible = false;
                trDateSTP.Visible = false;

                trBtnSubmit.Visible = true;
 
            }
            else if (type == 2)
            {
                trAddress1.Visible = false;
                trAddress2.Visible = false;
                trAddress3.Visible = false;
                trAddress4.Visible = false;
                trAddress5.Visible = false;
                trAddress6.Visible = false;
                trAddress7.Visible = false;
                trAddress8.Visible = false;
                trAddress9.Visible = false;
                trAddress10.Visible = false;

                trPurchase.Visible = true;
                trPurchase1.Visible = false;
                trPurchase2.Visible = false;
                trPurchase3.Visible = false;
                trPurchase4.Visible = false;

                trSell1.Visible = true;
                
                tdamount.Visible=true;
                lblAmount.Visible = true;
                trSell2.Visible = true;
                trSell3.Visible = false;
                trSell4.Visible = true;

                trBtnSubmit.Visible = false;
                trSipFrequency.Visible = false;
                trSIPDate.Visible = false;
                trfrequencySTP.Visible = false;
                trDateSTP.Visible = false;

                trBtnSubmit.Visible = true;
 
            }
            if (type == 3)
            {
                trAddress1.Visible = true;
                trAddress2.Visible = true;
                trAddress3.Visible = true;
                trAddress4.Visible = true;
                trAddress5.Visible = true;
                trAddress6.Visible = true;
                trAddress7.Visible = true;
                trAddress8.Visible = true;
                trAddress9.Visible = true;
                trAddress10.Visible = true;

                trPurchase.Visible = true;
                trPurchase1.Visible = false;
                trPurchase2.Visible = false;
                trPurchase3.Visible = false;
                trPurchase4.Visible = false;

                trSell1.Visible = false;
                trSell2.Visible = false;
                trSell3.Visible = false;
                trSell4.Visible = false;

                trBtnSubmit.Visible = false;
                trSipFrequency.Visible = false;
                trSIPDate.Visible = false;
                trfrequencySTP.Visible = false;
                trDateSTP.Visible = false;

                trBtnSubmit.Visible = true;
            }
            if (type == 4)
            {
                trAddress1.Visible = false;
                trAddress2.Visible = false;
                trAddress3.Visible = false;
                trAddress4.Visible = false;
                trAddress5.Visible = false;
                trAddress6.Visible = false;
                trAddress7.Visible = false;
                trAddress8.Visible = false;
                trAddress9.Visible = false;
                trAddress10.Visible = false;

                trPurchase.Visible = false;
                trPurchase1.Visible = false;
                trPurchase2.Visible = false;
                trPurchase3.Visible = false;
                trPurchase4.Visible = false;

                trSell1.Visible = false;
                trSell2.Visible = false;
                trSell3.Visible = false;
                trSell4.Visible = false;

                trBtnSubmit.Visible = false;
                trSipFrequency.Visible=false;
                trSIPDate.Visible=false;
                trfrequencySTP.Visible=false;
                trDateSTP.Visible = false;

            }
        }



        protected void ShowHideFields(int flag)
        {
            if (flag == 0)
            {
                trSectionTwo1.Visible = false;
                trSectionTwo2.Visible = false;
                trSectionTwo3.Visible = false;
                trSectionTwo4.Visible = false;
                trSectionTwo5.Visible = false;
                trSectionTwo6.Visible = false;
                trSectionTwo7.Visible = false;
                trSectionTwo8.Visible = false;
                //trSectionTwo9.Visible = false;
                trSectionTwo10.Visible = false;
                ShowTransactionType(0);
            }
            else if (flag == 1)
            {
                trSectionTwo1.Visible = true;
                trSectionTwo2.Visible = true;
                trSectionTwo3.Visible = true;
                trSectionTwo4.Visible = true;
                trSectionTwo5.Visible = true;
                trSectionTwo6.Visible = true;
                trSectionTwo7.Visible = true;
                trSectionTwo8.Visible = true;
                //trSectionTwo9.Visible = true;
                
 
            }
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OrderEntry", "loadcontrol('CustomerType','page=Entry');", true);
        }

        //protected void ddlAssetType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlAssetType.SelectedIndex != 0)
        //    {
        //        ShowHideFields(1);
        //    }
        //    else if (ddlAssetType.SelectedIndex == 0)
        //    {
        //        ShowHideFields(0);
        //        ddltransType.SelectedIndex = 0;
        //        rbtnImmediate.Checked = true;
        //        trSell3.Visible = false;
        //        trBtnSubmit.Visible = false;
        //    }
        //}

        //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int bmID = rmVo.RMId;
        //    if (ddlBranch.SelectedIndex == 0)
        //    {
        //        BindRMforBranchDropdown(0, bmID);
        //    }
        //    else
        //    {
        //        BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
        //    }
        //}

        protected void ddlAMCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMCList.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlAMCList.SelectedValue);
                if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "SIP" || ddltransType.SelectedValue == "CAF")
                {
                    BindScheme(0);
                    BindFolioNumber(0);
                }
                else
                {
                    BindScheme(1);
                    BindFolioNumber(1);
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
                categoryCode = productMFBo.GetCategoryNameFromSChemeCode(schemePlanCode);
                ddlCategory.SelectedValue = categoryCode;
                if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
                {
                    if (!string.IsNullOrEmpty(txtCustomerId.Value.ToString().Trim()))
                    {
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
            }
            //if(ddltransType.SelectedItem.Text != "")
            //    transactionType = ddltransType.SelectedItem.Text;

            //if(ddlAmcSchemeList.SelectedIndex != 0)
            //    schemeCode = Convert.ToInt32(ddlAmcSchemeList.SelectedValue);

            //FileName = CheckPDFFormAvailabilty(transactionType, schemeCode);

            //if (FileName != "")
            //{
            //    hyperLinkFillablePdfForm.Visible = true;
            //    hyperLinkFillablePdfForm.NavigateUrl = "~/FillablePDFForms/" + FileName;
            //    hyperLinkFillablePdfForm.Text = "(" + FileName+" )";
            //    Session["FileName"] = FileName;
            //}
            //else
            //{
            //    hyperLinkFillablePdfForm.Visible = false;
            //}
        }

        private string CheckPDFFormAvailabilty(string transactionType, int schemeCode)
        {
            string fileName = "";
            DataTable dtPdfForm = new DataTable();
            dtPdfForm = operationBo.CheckPDFFormAvailabilty(transactionType, schemeCode);
            if (dtPdfForm.Rows.Count > 0)
            {
                fileName = dtPdfForm.Rows[0]["XMLPDFForm_FilePath"].ToString();
            }
            return fileName;
        }

        protected void ddlFolioNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolioNumber.SelectedIndex != 0)
            {

                accountId = int.Parse(ddlFolioNumber.SelectedValue);
                amcCode = productMFBo.GetAMCfromFolioNo(accountId);
                //ddlAMCList.SelectedValue = amcCode.ToString();
                if (ddlAmcSchemeList.SelectedIndex == 0)
                {
                    if (ddltransType.SelectedValue == "BUY" || ddltransType.SelectedValue == "SIP")
                        BindScheme(0);
                    else
                        BindScheme(1);
                }

            }
        }

        protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrderStatus.SelectedItem.Selected == true)
            {
                string statuscode = "";
                statuscode = ddlOrderStatus.SelectedValue;
                if (ddlOrderStatus.SelectedValue == "OMPD" || ddlOrderStatus.SelectedValue == "OMRJ" || ddlOrderStatus.SelectedValue == "OMCN")
                {
                    lblOrderPendingReason.Visible = true;
                    ddlOrderPendingReason.Visible = true;
                    BindRejectStatus(statuscode);
                }
                else
                {
                    ddlOrderPendingReason.Visible = false;
                    lblOrderPendingReason.Visible = false;
                }
                
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            operationVo = (OperationVo)Session["operationVo"];
            //operationVo.CustomerId = int.Parse(txtCustomerId.Value);
            operationVo.CustomerName = txtCustomerName.Text;
            if (operationVo.CustomerId != 0)
                hdnCustomerId.Value = operationVo.CustomerId.ToString();
            operationVo.BMName = lblGetBranch.Text;
            operationVo.RMName = lblGetRM.Text;
            operationVo.PanNo = lblgetPan.Text;
            operationVo.TransactionCode = ddltransType.SelectedValue;
            operationVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            operationVo.ApplicationNumber = txtApplicationNumber.Text;
            if (ddlAMCList.SelectedIndex != -1)
                operationVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            else
                operationVo.Amccode = 0;
            operationVo.category = ddlCategory.SelectedValue;
            if (ddlAmcSchemeList.SelectedIndex != -1)
                operationVo.SchemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
            else
                operationVo.SchemePlanCode = 0;
            operationVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            if (ddlFolioNumber.SelectedIndex != -1)
                operationVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
            else
                operationVo.accountid = 0;
            //operationVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            //if (ddlFolioNumber.SelectedIndex == 0)
            //    operationVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
            //else
            //    operationVo.accountid = 0;
            operationVo.OrderDate = Convert.ToDateTime(txtOrderDate.SelectedDate);
            operationVo.OrderNumber = int.Parse(lblGetOrderNo.Text);
            operationVo.StatusCode = ddlOrderStatus.SelectedValue;
            operationVo.StatusReasonCode = ddlOrderPendingReason.SelectedValue;
            if (rbtnImmediate.Checked == true)
                operationVo.IsImmediate = 1;
            else
                operationVo.IsImmediate = 0;
            if (!string.IsNullOrEmpty(txtFutureDate.SelectedDate.ToString().Trim()))
                operationVo.FutureExecutionDate = DateTime.Parse(txtFutureDate.SelectedDate.ToString());
            else
                operationVo.FutureExecutionDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty((txtFutureTrigger.Text).ToString().Trim()))
                operationVo.FutureTriggerCondition = txtFutureTrigger.Text;
            else
                operationVo.FutureTriggerCondition = "";
            if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
                operationVo.Amount = double.Parse(txtAmount.Text);
            else
                operationVo.Amount = 0;

            if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STP" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
            {
                if (rbtAmount.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        operationVo.Amount = double.Parse(txtNewAmount.Text);
                    else
                        operationVo.Amount = 0;
                }
                if (rbtUnit.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        operationVo.Units = double.Parse(txtNewAmount.Text);
                    else
                        operationVo.Units = 0;
                }
            }
            if (ddltransType.SelectedValue == "SIP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
                    operationVo.FrequencyCode = ddlFrequencySIP.SelectedValue;
                if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
                    operationVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
                else
                    operationVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
                    operationVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
                else
                    operationVo.EndDate = DateTime.MinValue;
            }
            else if (ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
                    operationVo.FrequencyCode = ddlFrequencySTP.SelectedValue;

                if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
                    operationVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
                else
                    operationVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
                    operationVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
                else
                    operationVo.EndDate = DateTime.MinValue;
            }

            

            //if (!string.IsNullOrEmpty((lblGetAvailableUnits.Text).ToString().Trim()))
            //    operationVo.Units = double.Parse(lblGetAvailableUnits.Text);
            //else
            //    operationVo.Units = 0;
            if (ddlPaymentMode.SelectedValue == "ES")
                operationVo.PaymentMode = "ES";
            else if (ddlPaymentMode.SelectedValue == "DF")
                operationVo.PaymentMode = "DF";
            else if (ddlPaymentMode.SelectedValue == "CQ")
                operationVo.PaymentMode = "CQ";
            if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
                operationVo.ChequeNumber = txtPaymentNumber.Text;
            else
                operationVo.ChequeNumber = "";
            if (txtPaymentInstDate.SelectedDate != null)
                operationVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
            else
                operationVo.PaymentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
                operationVo.BankName = ddlBankName.SelectedItem.Text;
            else
                operationVo.BankName = "";
            if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
                operationVo.BranchName = txtBranchName.Text;
            else
                operationVo.BranchName = "";
            if (ddlSchemeSwitch.SelectedValue != "")
            {
                if (ddlSchemeSwitch.SelectedIndex != 0)
                    operationVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
                operationVo.AddrLine1 = txtCorrAdrLine1.Text;
            else
                operationVo.AddrLine1 = "";
            if (!string.IsNullOrEmpty(txtCorrAdrLine2.Text.ToString().Trim()))
                operationVo.AddrLine2 = txtCorrAdrLine2.Text;
            else
                operationVo.AddrLine2 = "";
            if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
                operationVo.AddrLine3 = txtCorrAdrLine3.Text;
            else
                operationVo.AddrLine3 = "";
            if (txtLivingSince.SelectedDate.ToString() != "d4d/mm/yyyy")
                operationVo.LivingSince = DateTime.MinValue;
            else
                operationVo.LivingSince = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
                operationVo.City = txtCorrAdrCity.Text;
            else
                operationVo.City = "";
            if (ddlCorrAdrState.SelectedIndex != 0)
                operationVo.State = ddlCorrAdrState.SelectedItem.Text;
            else
                operationVo.State = "";
            if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
                operationVo.Pincode = txtCorrAdrPinCode.Text;
            else
                operationVo.Pincode = "";
            operationVo.Country = ddlCorrAdrCountry.SelectedValue;

            if (chkCA.Checked == true)
                operationVo.IsApproved = 1;
            else
                operationVo.IsApproved = 0;

            operationBo.UpdateOrderTracking(operationVo);
            msgUpdate.Visible = true;
            btnSubmit.Enabled = false;
            btnUpdate.Enabled = true;
            trReportButtons.Visible = true;

            txtCustomerName.Enabled = false;
            ddltransType.Enabled = false;
            txtReceivedDate.Enabled = false;
            txtApplicationNumber.Enabled = false;
            ddlAMCList.Enabled = false;
            ddlCategory.Enabled = false;
            ddlAmcSchemeList.Enabled = false;
            ddlPortfolio.Enabled = false;
            ddlFolioNumber.Enabled = false;
            txtOrderDate.Enabled = false;
            lblGetOrderNo.Enabled = false;
            ddlOrderPendingReason.Enabled = false;
            ddlOrderStatus.Enabled = false;
            txtFutureDate.Enabled = false;
            txtFutureTrigger.Enabled = false;
            txtAmount.Enabled = false;
            ddlPaymentMode.Enabled = false;
            txtPaymentNumber.Enabled = false;
            txtPaymentInstDate.Enabled = false;
            ddlBankName.Enabled = false;
            txtBranchName.Enabled = false;
            lblGetAvailableAmount.Enabled = false;
            lblGetAvailableUnits.Enabled = false;

            rbtAmount.Enabled = false;
            txtNewAmount.Enabled = false;

            ddlSchemeSwitch.Enabled = false;
            txtCorrAdrLine1.Enabled = false;
            txtCorrAdrLine2.Enabled = false;
            txtCorrAdrLine3.Enabled = false;
            txtLivingSince.Enabled = false;
            txtCorrAdrCity.Enabled = false;
            ddlCorrAdrState.Enabled = false;
            txtCorrAdrPinCode.Enabled = false;
            ddlCorrAdrCountry.Enabled = false;
            ddlFrequencySTP.Enabled = false;
            ddlFrequencySIP.Enabled = false;
            txtendDateSIP.Enabled = false;
            txtendDateSTP.Enabled = false;
            txtstartDateSIP.Enabled = false;
            txtstartDateSTP.Enabled = false;
            
        }

        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = false;
            operationVo.CustomerId = int.Parse(txtCustomerId.Value);
            operationVo.CustomerName = txtCustomerName.Text;
            operationVo.BMName = lblGetBranch.Text;
            operationVo.RMName = lblGetRM.Text;
            operationVo.PanNo = lblgetPan.Text;
            operationVo.TransactionCode = ddltransType.SelectedValue;
            operationVo.ApplicationReceivedDate = DateTime.Parse(txtReceivedDate.SelectedDate.ToString());
            operationVo.ApplicationNumber = txtApplicationNumber.Text;
            if (ddlAMCList.SelectedIndex != 0)
                operationVo.Amccode = int.Parse(ddlAMCList.SelectedValue);
            else
                operationVo.Amccode = 0;
            operationVo.category = ddlCategory.SelectedValue;
            if (ddlAmcSchemeList.SelectedIndex != 0)
                operationVo.SchemePlanCode = int.Parse(ddlAmcSchemeList.SelectedValue);
            else
                operationVo.SchemePlanCode = 0;
            operationVo.portfolioId = int.Parse(ddlPortfolio.SelectedValue);
            if (ddlFolioNumber.SelectedIndex != -1)
                operationVo.accountid = int.Parse(ddlFolioNumber.SelectedValue);
            else
                operationVo.accountid = 0;
            operationVo.OrderDate = Convert.ToDateTime(txtOrderDate.SelectedDate);
            operationVo.OrderNumber = int.Parse(lblGetOrderNo.Text);
            operationVo.StatusCode = ddlOrderStatus.SelectedValue;
            operationVo.StatusReasonCode = ddlOrderPendingReason.SelectedValue;
            if (rbtnImmediate.Checked == true)
                operationVo.IsImmediate = 1;
            else
                operationVo.IsImmediate = 0;
            if (!string.IsNullOrEmpty((txtFutureDate.SelectedDate).ToString().Trim()))
                operationVo.FutureExecutionDate = DateTime.Parse(txtFutureDate.SelectedDate.ToString());
            else
                operationVo.FutureExecutionDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty((txtFutureTrigger.Text).ToString().Trim()))
                operationVo.FutureTriggerCondition = txtFutureTrigger.Text;
            else
                operationVo.FutureTriggerCondition = "";
            if (!string.IsNullOrEmpty((txtAmount.Text).ToString().Trim()))
                operationVo.Amount = double.Parse(txtAmount.Text);
            else
                operationVo.Amount = 0;

            if (ddltransType.SelectedValue == "Sel" || ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP" || ddltransType.SelectedValue == "SWB")
            {
                if (rbtAmount.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        operationVo.Amount = double.Parse(txtNewAmount.Text);
                    else
                        operationVo.Amount = 0;
                }
                if (rbtUnit.Checked == true)
                {
                    if (!string.IsNullOrEmpty((txtNewAmount.Text).ToString().Trim()))
                        operationVo.Units = double.Parse(txtNewAmount.Text);
                    else
                        operationVo.Units = 0;
                }

            }
            if (!string.IsNullOrEmpty((lblGetAvailableUnits.Text).ToString().Trim()))
                operationVo.Units = double.Parse(lblGetAvailableUnits.Text);
            else
                operationVo.Units = 0;
            if (ddlPaymentMode.SelectedValue == "ES")
                operationVo.PaymentMode = "ES";
            else if (ddlPaymentMode.SelectedValue == "DF")
                operationVo.PaymentMode = "DF";
            else if (ddlPaymentMode.SelectedValue == "CQ")
                operationVo.PaymentMode = "CQ";
            if (!string.IsNullOrEmpty(txtPaymentNumber.Text.ToString().Trim()))
                operationVo.ChequeNumber = txtPaymentNumber.Text;
            else
                operationVo.ChequeNumber = "";
            if (!string.IsNullOrEmpty(txtPaymentInstDate.SelectedDate.ToString().Trim()))
                operationVo.PaymentDate = DateTime.Parse(txtPaymentInstDate.SelectedDate.ToString());
            else
                operationVo.PaymentDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(ddlBankName.SelectedValue))
                operationVo.BankName = ddlBankName.SelectedItem.Text;
            else
                operationVo.BankName = "";
            if (!string.IsNullOrEmpty(txtBranchName.Text.ToString().Trim()))
                operationVo.BranchName = txtBranchName.Text;
            else
                operationVo.BranchName = "";
            if (ddlSchemeSwitch.SelectedValue != "")
            {
                if (ddlSchemeSwitch.SelectedIndex != 0)
                    operationVo.SchemePlanSwitch = int.Parse(ddlSchemeSwitch.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtCorrAdrLine1.Text.ToString().Trim()))
                operationVo.AddrLine1 = txtCorrAdrLine1.Text;
            else
                operationVo.AddrLine1 = "";
            if (!string.IsNullOrEmpty(txtCorrAdrLine2.Text.ToString().Trim()))
                operationVo.AddrLine2 = txtCorrAdrLine2.Text;
            else
                operationVo.AddrLine2 = "";
            if (!string.IsNullOrEmpty(txtCorrAdrLine3.Text.ToString().Trim()))
                operationVo.AddrLine3 = txtCorrAdrLine3.Text;
            else
                operationVo.AddrLine3 = "";
            if (!string.IsNullOrEmpty(txtLivingSince.SelectedDate.ToString().Trim()))
                operationVo.LivingSince = DateTime.Parse(txtLivingSince.SelectedDate.ToString());
            else
                operationVo.LivingSince = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtCorrAdrCity.Text.ToString().Trim()))
                operationVo.City = txtCorrAdrCity.Text;
            else
                operationVo.City = "";
            if (ddlCorrAdrState.SelectedIndex != 0)
                operationVo.State = ddlCorrAdrState.SelectedItem.Text;
            else
                operationVo.State = "";
            if (!string.IsNullOrEmpty(txtCorrAdrPinCode.Text.ToString().Trim()))
                operationVo.Pincode = txtCorrAdrPinCode.Text;
            else
                operationVo.Pincode = "";
            operationVo.Country = ddlCorrAdrCountry.SelectedValue;

            if (ddltransType.SelectedValue == "SIP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySIP.SelectedValue).ToString().Trim()))
                    operationVo.FrequencyCode = ddlFrequencySIP.SelectedValue;

                if (!string.IsNullOrEmpty((txtstartDateSIP.SelectedDate).ToString().Trim()))
                    operationVo.StartDate = DateTime.Parse(txtstartDateSIP.SelectedDate.ToString());
                else
                    operationVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSIP.SelectedDate).ToString().Trim()))
                    operationVo.EndDate = DateTime.Parse(txtendDateSIP.SelectedDate.ToString());
                else
                    operationVo.EndDate = DateTime.MinValue;
            }
            else if (ddltransType.SelectedValue == "STB" || ddltransType.SelectedValue == "SWP")
            {
                if (!string.IsNullOrEmpty((ddlFrequencySTP.SelectedValue).ToString().Trim()))
                    operationVo.FrequencyCode = ddlFrequencySTP.SelectedValue;

                if (!string.IsNullOrEmpty((txtstartDateSTP.SelectedDate).ToString().Trim()))
                    operationVo.StartDate = DateTime.Parse(txtstartDateSTP.SelectedDate.ToString());
                else
                    operationVo.StartDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty((txtendDateSTP.SelectedDate).ToString().Trim()))
                    operationVo.EndDate = DateTime.Parse(txtendDateSTP.SelectedDate.ToString());
                else
                    operationVo.EndDate = DateTime.MinValue;
            }
            if (chkCA.Checked == true)
                operationVo.IsApproved = 1;
            else
                operationVo.IsApproved = 0;

            operationBo.CreateMFOrderTracking(operationVo);
           
            trReportButtons.Visible = true;
            msgRecordStatus.Visible = true;

            txtCustomerName.Text = "";
            lblGetBranch.Text = "";
            lblGetRM.Text = "";
            lblgetPan.Text = "";
            CleanAllFields();
            lblGetOrderNo.Text = "";
            txtOrderDate.SelectedDate = DateTime.Now;
            btnUpdate.Visible = false;
            lnkBtnEdit.Visible = false;

        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            ViewForm = "Edit";
            if (Session["operationVo"] != null)
            {
                operationVo = (OperationVo)Session["operationVo"];
            }
            if (operationVo != null)
            {
                if (ViewForm == "Edit")
                {
                    SetControls("Edit", operationVo);
                }
            }
            else
            {
                SetControls("Entry", operationVo);
            }
            if (operationVo.StatusCode == "OMEX")
                SetEditViewMode(true);
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            string newFile = "";
            string InputFilePath = Server.MapPath("FillablePDFForms");
            string OutPutFilePath = System.Configuration.ConfigurationManager.AppSettings["OutPutFillablePDFFiles"];
            if (Session["FileName"] != null)
            {
                string pdfTemplate = @InputFilePath + "\\" + Session["FileName"].ToString();
                if (Directory.Exists(OutPutFilePath))
                {
                    if(!File.Exists(OutPutFilePath + "\\" + Session["FileName"].ToString()))
                        File.Create(OutPutFilePath + "\\" + Session["FileName"].ToString());

                    newFile = @OutPutFilePath + Session["FileName"].ToString();
                    Session["OutPutFilePath"] = newFile;
                }
               
                PdfReader pdfReader = new PdfReader(pdfTemplate);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                            newFile, FileMode.Create));

                AcroFields pdfFormFields = pdfStamper.AcroFields;
                
                // The first worksheet and W-4 form
                if (lblGetBranch.Text != "")
                    pdfFormFields.SetField("WERP_BRANCH", lblGetBranch.Text);

                if (txtCustomerName.Text != "")
                    pdfFormFields.SetField("WERP_ACCHOLDER", txtCustomerName.Text);

                if (txtCustomerName.Text != "")
                    pdfFormFields.SetField("WERP_INVESTORNAME", txtCustomerName.Text);

                if (ddlFolioNumber.SelectedValue != "")
                    pdfFormFields.SetField("WERP_FOLIO", ddlFolioNumber.SelectedValue);

                if (ddlAmcSchemeList.SelectedItem.Text != "")
                    pdfFormFields.SetField("WERP_SCHEME", ddlAmcSchemeList.SelectedItem.Text);

                if (txtReceivedDate.SelectedDate != null)
                    pdfFormFields.SetField("WERP_DATE",txtReceivedDate.SelectedDate.ToString());

                if (txtstartDateSIP.SelectedDate != null)
                    pdfFormFields.SetField("WERP_ECSFROM", txtstartDateSIP.SelectedDate.ToString());

                if (txtendDateSIP.SelectedDate != null)
                    pdfFormFields.SetField("WERP_ECSTO", txtendDateSIP.SelectedDate.ToString());

                if (lblgetPan.Text != "")
                    pdfFormFields.SetField("WERP_PAN", lblgetPan.Text);

                if (ddlCategory.SelectedValue != "")
                    pdfFormFields.SetField("WERP_PLAN", ddlCategory.SelectedValue);

                if (txtPaymentInstDate.SelectedDate != null)
                    pdfFormFields.SetField("WERP_SIPCHQDATE", txtPaymentInstDate.SelectedDate.ToString());

                if (ddlBankName.SelectedValue != "")
                    pdfFormFields.SetField("WERP_BANKNAME", ddlBankName.SelectedValue);

                if (txtAmount.Text != "")
                    pdfFormFields.SetField("WERP_SIPAMOUNT", txtAmount.Text);

                if (ddlFrequencySIP.SelectedIndex != 0)
                {
                    switch (ddlFrequencySIP.SelectedValue)
                    {
                        case "DA":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_Daily", "Yes");
                                break;
                            }
                        case "FN":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_FortNightly", "Yes");
                                break;
                            }
                        case "HY":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_HalfYearly", "Yes");
                                break;
                            }
                        case "MN":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_Monthly", "Yes");
                                break;
                            }
                        case "QT":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_Quarterly", "Yes");
                                break;
                            }
                        case "WK":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_Weekly", "Yes");
                                break;
                            }
                        case "YR":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_Yearly", "Yes");
                                break;
                            }
                        case "AM":
                            {
                                pdfFormFields.SetField("WERP_SIPFrequency_AtMaturity", "Yes");
                                break;
                            }
                    }
                }
                //pdfFormFields.SetField("WERP_ACCTYPE1", txtScheme.Text);
                //pdfFormFields.SetField("WERP_ACCTYPE2", txtScheme.Text);
                //pdfFormFields.SetField("WERP_ACCTYPE3", txtScheme.Text);



                //pdfFormFields.SetField("WERP_OPTION", txtScheme.Text);

                //pdfFormFields.SetField("WERP_SIGN2", txtScheme.Text);


                //pdfFormFields.SetField("WERP_INVSIGN1", txtScheme.Text);
                //pdfFormFields.SetField("WERP_SIPCHQNO", txtScheme.Text);
                //pdfFormFields.SetField("WERP_MICR", txtScheme.Text);
                //pdfFormFields.SetField("WERP_SIGNINV3", txtScheme.Text);

                //pdfFormFields.SetField("WERP_SIGNINV3", txtScheme.Text);
                //pdfFormFields.SetField("WERP_SIGN1", txtScheme.Text);

                //pdfFormFields.SetField("WERP_SIGN3", txtScheme.Text);
                //pdfFormFields.SetField("WERP_SIPACCNO", txtScheme.Text);
                //pdfFormFields.SetField("WERP_SIGNINV2", txtScheme.Text);


                //// The form's checkboxes

                //pdfFormFields.SetField("Radio Button7", "Yes");
                //pdfFormFields.SetField("RADD1", "Yes");
                //pdfFormFields.SetField("RADD1", "Yes");
                //pdfFormFields.SetField("RADD1", "Yes");
                //pdfFormFields.SetField("RADD2", "Yes");
                //pdfFormFields.SetField("RADD3", "Yes");
                //pdfFormFields.SetField("Radio Button9", "Yes");


                //// report by reading values from completed PDF
                //string sTmp = pdfFormFields.GetField("SCHEME") + "Application form filled successfully for " + pdfFormFields.GetField("FOLIO") + " ";

                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + sTmp + "');", true);

                // flatten the form to remove editting options, set it to false
                // to leave the form open to subsequent manual edits
                pdfStamper.FormFlattening = false;

                // close the pdf
                pdfStamper.Close();

                OpenPdfFile();
            }
        }

        private void OpenPdfFile()
        {
            if (Session["OutPutFilePath"] != null)
            {
                System.Diagnostics.Process.Start(@Session["OutPutFilePath"].ToString());
            }
        }

        protected void chkCA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCA.Checked == true)
                ddlOrderStatus.Enabled = true;
            else
            {
                ddlOrderStatus.Enabled = false;
                ddlOrderStatus.SelectedValue = "OMIP";
                ddlOrderPendingReason.Visible = false;
                lblOrderPendingReason.Visible = false;

            }
        }

        protected void lnlBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FromPage"] != null)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerOrderList','none');", true);
            }
        }

    }
}
