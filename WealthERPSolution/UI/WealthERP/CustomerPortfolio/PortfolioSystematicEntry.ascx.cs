using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using BoProductMaster;
using WealthERP.Base;
using VoUser;
using BoCommon;


namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioSystematicEntry : System.Web.UI.UserControl
    {
        SystematicSetupVo systematicSetupVo;
        SystematicSetupBo systematicSetupBo = new SystematicSetupBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        ProductMFBo productMFBo = new ProductMFBo();
        AssetBo assetBo = new AssetBo();
        PortfolioBo portfolioBo = new PortfolioBo();

        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        DataSet dsCustomerMFAccounts;
        DataTable dtSystematicTransactionType;
        DataTable dtFrequency;
        TimeSpan tsPeriod;


        int portfolioId;
        int schemePlanCode;
        int systematicSetupId;
        string folioId;
        string path;
        string AssetGroupCode = "MF";
        string Manage = string.Empty;
        string PageRecon = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
              
            // Check Querystring to see if its an Edit/View/Entry

            //BindFolioDropDown();
            if (Request.QueryString["action"] != null)
                Manage = Request.QueryString["action"].ToString();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            if (!IsPostBack)
            {
                userVo = (UserVo)Session[SessionContents.UserVo];
                
                //customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                if (Session["systematicSetupVo"] != null)
                {
                    systematicSetupVo = (SystematicSetupVo)Session["systematicSetupVo"];
                }

                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                customerAccountsVo = (CustomerAccountsVo)Session[SessionContents.CustomerMFAccount];

                if (Request.QueryString["FromPage"] == "CustomerMFAccountAdd" && Request.QueryString["FolioNumber"] != null)
                {  
                    folioId = Request.QueryString["FolioNumber"];
                    if (Session["Action"] != null)
                    {
                        Manage = Session["Action"].ToString();
                        //Manage = "entry";
                    }
                    if (Manage == "edit")
                    {
                        MaintainPageStateForControls();
                        SetControlsForFolioAdd(Manage);
                    }
                    else if (Manage == "entry")
                    {
                        
                          SetControlsForFolioAdd(Manage);                        
                          MaintainPageStateForControls();                       
 
                    }
                  
                }
                else 
                { 
                    if (systematicSetupVo != null)
                    {
                        if (Manage == "edit")
                        {
                            SetControls("edit", systematicSetupVo, path);
                        }
                        else if (Manage == "view")
                        {
                            SetControls("view", systematicSetupVo, path);
                        }
                        else if (Manage == "entry")
                        {
                            SetControls("entry", systematicSetupVo, path);
                        }
                    }
                    else
                    {
                        SetControls("entry", systematicSetupVo, path);
                    }
                }
                
            }
        
        }

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlportfolio.DataSource = ds;
            ddlportfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlportfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlportfolio.DataBind();
            //ddlportfolio.SelectedValue = portfolioId.ToString(); 
        }

        private void MaintainPageStateForControls()
        {
            BindDropDowns(path);
            //BindPortfolioDropDown();

            if ((Session["SystematicHT"] != null) && (Session["SystematicHT"].ToString() != ""))
            {
                Hashtable hashRetainPreviousState = new Hashtable();
                hashRetainPreviousState = (Hashtable)Session["SystematicHT"];

                ddlSystematicType.SelectedValue = hashRetainPreviousState["SystematicType"].ToString();
                if (ddlSystematicType.SelectedValue == "SIP")
                {
                    trSipChequeDate.Visible = true;
                    trSipChequeNo.Visible = true;
                    trPaymentMode.Visible = true;
                    trSystematicDate.Visible = false;

                }
                else if (ddlSystematicType.SelectedValue == "STP")
                {
                    trPaymentMode.Visible = true;
                    trSystematicDate.Visible = false;
                }
                else if (ddlSystematicType.SelectedValue == "SWP")
                {
                    trSystematicDate.Visible = false;
                }
                txtSearchScheme.Text = hashRetainPreviousState["SearchScheme"].ToString();
                txtSwicthSchemeSearch.Text = hashRetainPreviousState["SwicthSchemeSearch"].ToString();

                //ddlFolioNumber.SelectedValue = hashRetainPreviousState["FolioNumber"].ToString();

                txtSchemeCode.Value = hashRetainPreviousState["SchemeCode"].ToString();


                BindPortfolioDropDown();
                ddlportfolio.SelectedValue = hashRetainPreviousState["Portfolio"].ToString();
                schemePlanCode = int.Parse(hashRetainPreviousState["SchemeCode"].ToString());
                BindFolioDropDown(int.Parse(ddlportfolio.SelectedValue.ToString()));
                ddlFolioNumber.SelectedValue = folioId;

                txtStartDate.Text = hashRetainPreviousState["StartDate"].ToString();
                txtSipChequeDate.Text = hashRetainPreviousState["SipChequeDate"].ToString();
                txtSipChecqueNo.Text = hashRetainPreviousState["SipChecqueNo"].ToString();
                if (Session["MaintainCheckBoxes"] != null)
                {
                    SetPreviousValuesForCheckboxes();
                }

                txtSystematicDate.Text = hashRetainPreviousState["SystematicDate"].ToString();
                ddlFrequency.SelectedValue = hashRetainPreviousState["Frequency"].ToString();
                txtAmount.Text = hashRetainPreviousState["Amount"].ToString();
                txtPeriod.Text = hashRetainPreviousState["Period"].ToString();
                ddlPeriodSelection.SelectedValue = hashRetainPreviousState["PeriodSelection"].ToString();
                txtEndDate.Text = hashRetainPreviousState["EndDate"].ToString();
                txtRegistrationDate.Text = hashRetainPreviousState["RegistrationDate"].ToString();
                ddlPaymentMode.SelectedValue = hashRetainPreviousState["PaymentMode"].ToString();

                Session.Remove("SystematicHT");
            }
            else
            {
                BindPortfolioDropDown();
                ddlFolioNumber.Items.Insert(0, "Select");
            }

           
        }

        private void SetPreviousValuesForCheckboxes()
        {
            Hashtable hashSetPreviousValuesForCheckboxes = new Hashtable();
            hashSetPreviousValuesForCheckboxes = (Hashtable)Session["MaintainCheckBoxes"];
            if (hashSetPreviousValuesForCheckboxes["chkDate1"] == "1")
                chkDate1.Checked = true;
            else
                chkDate1.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate2"] == "1")
                chkDate2.Checked = true;
            else
                chkDate2.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate3"] == "1")
                chkDate3.Checked = true;
            else
                chkDate3.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate4"] == "1")
                chkDate4.Checked = true;
            else
                chkDate4.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate5"] == "1")
                chkDate5.Checked = true;
            else
                chkDate5.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate6"] == "1")
                chkDate6.Checked = true;
            else
                chkDate6.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate7"] == "1")
                chkDate7.Checked = true;
            else
                chkDate7.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate8"] == "1")
                chkDate8.Checked = true;
            else
                chkDate8.Checked = false;


            if (hashSetPreviousValuesForCheckboxes["chkDate9"] == "1")
                chkDate9.Checked = true;
            else
                chkDate9.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate10"] == "1")
                chkDate10.Checked = true;
            else
                chkDate10.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate11"] == "1")
                chkDate11.Checked = true;
            else
                chkDate11.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate12"] == "1")
                chkDate12.Checked = true;
            else
                chkDate12.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate13"] == "1")
                chkDate13.Checked = true;
            else
                chkDate13.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate13"] == "1")
                chkDate13.Checked = true;
            else
                chkDate13.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate14"] == "1")
                chkDate14.Checked = true;
            else
                chkDate14.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate15"] == "1")
                chkDate15.Checked = true;
            else
                chkDate15.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate16"] == "1")
                chkDate16.Checked = true;
            else
                chkDate16.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate17"] == "1")
                chkDate17.Checked = true;
            else
                chkDate17.Checked = false;
            if (hashSetPreviousValuesForCheckboxes["chkDate18"] == "1")
                chkDate18.Checked = true;
            else
                chkDate18.Checked = false;
            if (hashSetPreviousValuesForCheckboxes["chkDate19"] == "1")
                chkDate19.Checked = true;
            else
                chkDate19.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate20"] == "1")
                chkDate20.Checked = true;
            else
                chkDate20.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate21"] == "1")
                chkDate21.Checked = true;
            else
                chkDate21.Checked = false;
            if (hashSetPreviousValuesForCheckboxes["chkDate22"] == "1")
                chkDate22.Checked = true;
            else
                chkDate22.Checked = false;
            if (hashSetPreviousValuesForCheckboxes["chkDate23"] == "1")
                chkDate23.Checked = true;
            else
                chkDate23.Checked = false;
            if (hashSetPreviousValuesForCheckboxes["chkDate24"] == "1")
                chkDate24.Checked = true;
            else
                chkDate24.Checked = false;
            if (hashSetPreviousValuesForCheckboxes["chkDate25"] == "1")
                chkDate25.Checked = true;
            else
                chkDate25.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate26"] == "1")
                chkDate26.Checked = true;
            else
                chkDate26.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate27"] == "1")
                chkDate27.Checked = true;
            else
                chkDate27.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate28"] == "1")
                chkDate28.Checked = true;
            else
                chkDate28.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate29"] == "1")
                chkDate29.Checked = true;
            else
                chkDate29.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate30"] == "1")
                chkDate30.Checked = true;
            else
                chkDate30.Checked = false;

            if (hashSetPreviousValuesForCheckboxes["chkDate31"] == "1")
                chkDate31.Checked = true;
            else
                chkDate31.Checked = false;


        }

        /// <summary>
        /// Binds all the Dropdowns with the necessary data
        /// </summary>
        private void BindDropDowns(string path)
        {
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            //portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());

            //Bind the Systematic Transaction Types to dropdown
            dtSystematicTransactionType = XMLBo.GetSystematicTransactionType(path);
            ddlSystematicType.DataSource = dtSystematicTransactionType;
            ddlSystematicType.DataTextField = "SystematicType";
            ddlSystematicType.DataValueField = "SystemationTypeCode";
            ddlSystematicType.DataBind();
            ddlSystematicType.Items.Insert(0, "Select");

            // Bind the Frequency to dropdown
            dtFrequency = assetBo.GetFrequencyCode(path);
            ddlFrequency.DataSource = dtFrequency;
            ddlFrequency.DataTextField = "Frequency";
            ddlFrequency.DataValueField = "FrequencyCode";
            ddlFrequency.DataBind();
            ddlFrequency.Items.Insert(0, "Select");

            ////Bind the Customer Account Details to dropdown


            //if (schemePlanCode != 0)
            //    dsCustomerMFAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, AssetGroupCode, schemePlanCode);
            //else
            //    dsCustomerMFAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, AssetGroupCode, 0);

            //ddlFolioNumber.DataSource = dsCustomerMFAccounts.Tables[0];
            //ddlFolioNumber.DataTextField = "CMFA_FolioNum";
            //ddlFolioNumber.DataValueField = "CMFA_AccountId";
            //ddlFolioNumber.DataBind();
            //ddlFolioNumber.Items.Insert(0, "Select a Folio");

        }

        /// <summary>
        /// Sets the Controls with the approprite values based on the action to be performed (View, Edit or Entry)
        /// </summary>
        /// <param name="action">action to be performed</param>
        /// <param name="systematicSetupVo">Object that holds the data to be filled in the controls</param>
        /// <param name="customerAccountsVo"></param>
        /// <param name="path">path of XML</param>
        private void SetControls(string action, SystematicSetupVo systematicSetupVo, string path)
        { 

            BindDropDowns(path);
            BindPortfolioDropDown();
            BindFolioDropDown(int.Parse(ddlportfolio.SelectedValue));
            EnableDisableControls(action);
            Session["Action"] = action;
            if (action == "entry")
            {
                //Code to check Entry
                if (systematicSetupVo == null)
                {
                    //BindFolioDropDown(0);
                    txtStartDate_CalendarExtender.Enabled = true;
                    txtStartDate_TextBoxWatermarkExtender.Enabled = true;
                    ddlSystematicType.SelectedIndex = -1;
                    ddlportfolio.SelectedIndex = 0;
                    ddlFolioNumber.SelectedIndex = -1;
                    txtStartDate.Text = "";
                    txtSystematicDate.Text = "";
                    ddlFrequency.SelectedIndex = -1;
                    txtAmount.Text = "";
                    txtPeriod.Text = "";
                    SipChequeDate_CalendarExtender.Enabled=true;
                    SipChequeDate_TextBoxWatermarkExtender.Enabled=true;
                    txtSipChequeDate.Text = "";
                    txtSipChecqueNo.Text = "";
                    txtRegistrationDate.Text = "";
                    ddlPeriodSelection.SelectedIndex = -1;
                    RegistrationDate_CalendarExtender.Enabled = true;
                    RegistrationDate_TextBoxWatermarkExtender.Enabled = true;
                    ddlPaymentMode.SelectedIndex= -1;
                }
                else
                {
                    txtStartDate_CalendarExtender.Enabled = true;
                    txtStartDate_TextBoxWatermarkExtender.Enabled = true;
                    ddlSystematicType.SelectedValue = systematicSetupVo.SystematicTypeCode.Trim();
                    ddlportfolio.SelectedItem.Value = systematicSetupVo.Portfolio;

                    txtSchemeCode.Value = systematicSetupVo.SchemePlanCode.ToString();
                    schemePlanCode = systematicSetupVo.SchemePlanCode;
                    //BindFolioDropDown(systematicSetupVo.PortfolioId);


                    ddlFolioNumber.SelectedItem.Text = systematicSetupVo.Folio;
                    txtSearchScheme.Text = systematicSetupVo.SchemePlan.ToString();
                    txtSchemeCode.Value = systematicSetupVo.SchemePlanCode.ToString();
                    if (systematicSetupVo.SchemePlan != "")
                        txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = txtSchemeCode.Value;
                    txtStartDate.Text = "";
                    txtSystematicDate.Text = "";
                    ddlFrequency.SelectedValue = "";
                    txtAmount.Text = systematicSetupVo.Amount.ToString();
                    SipChequeDate_CalendarExtender.Enabled = true;
                    SipChequeDate_TextBoxWatermarkExtender.Enabled = true;
                    //tsPeriod=systematicSetupVo.EndDate.Subtract(systematicSetupVo.StartDate);
                    txtPeriod.Text = systematicSetupVo.Period.ToString();

                    if (systematicSetupVo.SipChequeNo != 0)
                        txtSipChecqueNo.Text = systematicSetupVo.SipChequeNo.ToString();
                    else
                        txtSipChecqueNo.Text = "";
                    
                    if (systematicSetupVo.SipChequeDate != DateTime.MinValue)
                        txtSipChequeDate.Text = systematicSetupVo.SipChequeDate.ToShortDateString();
                    else
                        txtSipChequeDate.Text = "dd/mm/yyyy";
                    //txtSipChequeDate.Text = systematicSetupVo.SipChequeDate.ToShortDateString();
                    if (systematicSetupVo.PeriodSelection!=null)
                        ddlPeriodSelection.SelectedValue = systematicSetupVo.PeriodSelection.ToString();
                    else
                        ddlPeriodSelection.SelectedValue = "DA";
                    if (systematicSetupVo.RegistrationDate != DateTime.MinValue)
                        txtRegistrationDate.Text = systematicSetupVo.RegistrationDate.ToShortDateString();
                    else
                        txtRegistrationDate.Text = "dd/mm/yyyy";
                    RegistrationDate_CalendarExtender.Enabled = true;
                    RegistrationDate_TextBoxWatermarkExtender.Enabled = true;
                    if (systematicSetupVo.PaymentMode != null)
                        ddlPaymentMode.SelectedValue = systematicSetupVo.PaymentMode.ToString();
                    else
                        ddlPaymentMode.SelectedValue = "ES";
                    //if (systematicSetupVo.SystematicTypeCode.Trim() == "STP")
                    //{
                    //    txtSwitchSchemeCode.Value = systematicSetupVo.SchemePlanCodeSwitch.ToString();
                    //}

                }
            }
            // Code to check Edit and View
            else
            {
                ddlSystematicType.SelectedValue = systematicSetupVo.SystematicTypeCode.Trim();
                if (systematicSetupVo.SystematicTypeCode.Trim() == "STP")
                {
                    trSwitchScheme.Visible = true;
                    if (systematicSetupVo.SchemePlanSwitch != "")
                    {
                        txtSwicthSchemeSearch.Text = systematicSetupVo.SchemePlanSwitch;
                        txtSwitchSchemeCode.Value = systematicSetupVo.SchemePlanCodeSwitch.ToString();
                    }
                }
                
               
                txtSearchScheme.Text = systematicSetupVo.SchemePlan.ToString();
                txtSchemeCode.Value = systematicSetupVo.SchemePlanCode.ToString();
                ddlportfolio.SelectedValue = systematicSetupVo.PortfolioId.ToString();

                schemePlanCode = systematicSetupVo.SchemePlanCode;
                BindFolioDropDown(systematicSetupVo.PortfolioId);
                ddlFolioNumber.SelectedValue = systematicSetupVo.AccountId.ToString();

                txtStartDate.Text = systematicSetupVo.StartDate.ToShortDateString();
                txtSystematicDate.Text = systematicSetupVo.SystematicDate.ToString();
                ddlFrequency.SelectedValue = systematicSetupVo.FrequencyCode.Trim();
                txtAmount.Text = systematicSetupVo.Amount.ToString();
                txtPeriod.Text = systematicSetupVo.Period.ToString();
                txtEndDate.Text = systematicSetupVo.EndDate.ToShortDateString();
                //tsPeriod=systematicSetupVo.EndDate.Subtract(systematicSetupVo.StartDate);
                //txtPeriod.Text = (-12 * (systematicSetupVo.StartDate.Year - systematicSetupVo.EndDate.Year) + systematicSetupVo.StartDate.Month - systematicSetupVo.EndDate.Month).ToString();
                systematicSetupId = systematicSetupVo.SystematicSetupId;
                if (systematicSetupVo.SipChequeDate== DateTime.MinValue)
                    txtSipChequeDate.Text = "";
                else
                    txtSipChequeDate.Text = systematicSetupVo.SipChequeDate.ToShortDateString();
                if (systematicSetupVo.SipChequeNo.ToString() == "0")
                    txtSipChecqueNo.Text = "";
                else
                    txtSipChecqueNo.Text = systematicSetupVo.SipChequeNo.ToString();

   
                ddlPeriodSelection.SelectedItem.Text = systematicSetupVo.PeriodSelection;


                if (systematicSetupVo.RegistrationDate == DateTime.MinValue)
                    txtRegistrationDate.Text = "" ;
                else
                    txtRegistrationDate.Text = systematicSetupVo.RegistrationDate.ToShortDateString();

                if (systematicSetupVo.PaymentModeCode == "PD")
                    ddlPaymentMode.SelectedValue = "PD";
                else if (systematicSetupVo.PaymentModeCode == "ES")
                    ddlPaymentMode.SelectedValue = "ES";
    
            }

        }

        private void SetControlsForFolioAdd(string action)
        {

            EnableDisableControls(action);
            if (action == "entry")
            {
                  //Code to check Entry

                    txtStartDate_CalendarExtender.Enabled = true;
                    txtStartDate_TextBoxWatermarkExtender.Enabled = true;
                    ddlSystematicType.SelectedIndex = -1;
                    ddlportfolio.SelectedIndex = 0;
                    ddlFolioNumber.SelectedIndex = -1;
                    txtStartDate.Text = "";
                    txtSystematicDate.Text = "";
                    ddlFrequency.SelectedIndex = -1;
                    txtAmount.Text = "";
                    txtPeriod.Text = "";
                    SipChequeDate_CalendarExtender.Enabled = true;
                    SipChequeDate_TextBoxWatermarkExtender.Enabled = true;
                    txtSipChequeDate.Text = "";
                    txtSipChecqueNo.Text = "";
                    txtRegistrationDate.Text = "";
                    ddlPeriodSelection.SelectedIndex = -1;
                    RegistrationDate_CalendarExtender.Enabled = true;
                    RegistrationDate_TextBoxWatermarkExtender.Enabled = true;
                    ddlPaymentMode.SelectedIndex = -1;
              
                
            }
                   
        }

        /// <summary>
        /// Function to Enable or Disable the controls based on action to be performed (View, Edit or Entry)
        /// </summary>
        /// <param name="action">Acoint to be performed</param>
        private void EnableDisableControls(string action)
        {
            if (action == "view")
            {
                ddlSystematicType.Enabled = false;
                ddlportfolio.Enabled = false;
                ddlFolioNumber.Enabled = false;
                txtSearchScheme.Enabled = false;
                txtStartDate.Enabled = false;
                //chkDate1.Enabled = false;
                //chkDate2.Enabled = false;
                //chkDate3.Enabled = false;
                //chkDate4.Enabled = false;
                //chkDate5.Enabled = false;
                //chkDate6.Enabled = false;
                //chkDate7.Enabled = false;
                //chkDate8.Enabled = false;
                //chkDate9.Enabled = false;
                //chkDate10.Enabled = false;
                //chkDate11.Enabled = false;
                //chkDate12.Enabled = false;
                //chkDate13.Enabled = false;
                //chkDate14.Enabled = false;
                //chkDate15.Enabled = false;
                //chkDate16.Enabled = false;
                //chkDate17.Enabled = false;
                //chkDate18.Enabled = false;
                //chkDate19.Enabled = false;
                //chkDate20.Enabled = false;
                //chkDate21.Enabled = false;
                //chkDate22.Enabled = false;
                //chkDate23.Enabled = false;
                //chkDate24.Enabled = false;
                //chkDate25.Enabled = false;
                //chkDate26.Enabled = false;
                //chkDate27.Enabled = false;
                //chkDate28.Enabled = false;
                //chkDate29.Enabled = false;
                //chkDate30.Enabled = false;
                //chkDate31.Enabled = false;
                trSystematicDateChk1.Visible = false;
                trSystematicDateChk2.Visible = false;
                trSystematicDateChk3.Visible = false;
                ddlFrequency.Enabled = false;
                txtAmount.Enabled = false;
                txtPeriod.Enabled = false;
                txtSystematicDate.Enabled = false;
                trSystematicDate.Visible = true;
                txtSipChequeDate.Enabled = false;
                
                txtSipChecqueNo.Enabled = false;
                
                ddlPeriodSelection.Enabled = false;
                txtRegistrationDate.Enabled = false;
                txtEndDate.Enabled = false;
                btnSubmit.Visible = false;
                btnAddFolio.Visible = false;
                
                if (systematicSetupVo.SystematicTypeCode == "SIP")
                {
                    trSipChequeNo.Visible = true;
                    trSipChequeDate.Visible = true;
                    trSwitchScheme.Visible = false;
                    lblScheme.Text = "Choose Scheme:";
                    trPaymentMode.Visible = true;
                    ddlPaymentMode.Enabled = false;
                    txtPeriod.Text = systematicSetupVo.Period.ToString();

                }
                if (systematicSetupVo.SystematicTypeCode == "STP")
                {
                    trSwitchScheme.Visible = true;
                    txtSwicthSchemeSearch.Enabled = false;
                    lblScheme.Text = "Choose Invested Scheme:";
                    ddlPaymentMode.Enabled = false;
                    trPaymentMode.Visible = true;
                    trSipChequeDate.Visible = false;
                    trSipChequeNo.Visible = false;
                    txtPeriod.Text = systematicSetupVo.Period.ToString();
                }
                if (systematicSetupVo.SystematicTypeCode == "SWP")
                {
                    trSipChequeNo.Visible = false;
                    trSipChequeDate.Visible = false;
                    trSwitchScheme.Visible = false;
                    lblScheme.Text = "Choose Invested Scheme:";
                    trPaymentMode.Visible = false;
                    ddlPaymentMode.Enabled = false;
                    txtPeriod.Text = systematicSetupVo.Period.ToString();

                }

            }
            else if (action == "entry")
            {
                ddlSystematicType.Enabled = true;
                ddlportfolio.Enabled = true;
                ddlFolioNumber.Enabled = true;
                txtSearchScheme.Enabled = true;
                txtStartDate.Enabled = true;
                chkDate1.Enabled = true;
                chkDate2.Enabled = true;
                chkDate3.Enabled = true;
                chkDate4.Enabled = true;
                chkDate5.Enabled = true;
                chkDate6.Enabled = true;
                chkDate7.Enabled = true;
                chkDate8.Enabled = true;
                chkDate9.Enabled = true;
                chkDate10.Enabled = true;
                chkDate11.Enabled = true;
                chkDate12.Enabled = true;
                chkDate13.Enabled = true;
                chkDate14.Enabled = true;
                chkDate15.Enabled = true;
                chkDate16.Enabled = true;
                chkDate17.Enabled = true;
                chkDate18.Enabled = true;
                chkDate19.Enabled = true;
                chkDate20.Enabled = true;
                chkDate21.Enabled = true;
                chkDate22.Enabled = true;
                chkDate23.Enabled = true;
                chkDate24.Enabled = true;
                chkDate25.Enabled = true;
                chkDate26.Enabled = true;
                chkDate27.Enabled = true;
                chkDate28.Enabled = true;
                chkDate29.Enabled = true;
                chkDate30.Enabled = true;
                chkDate31.Enabled = true;
                ddlFrequency.Enabled = true;
                txtAmount.Enabled = true;
                txtPeriod.Enabled = true;
                trSystematicDate.Visible = false;
                trSystematicDateChk1.Visible = true;
                trSystematicDateChk2.Visible = true;
                trSystematicDateChk3.Visible = true;
                txtSipChequeDate.Enabled = true;
                txtEndDate.Enabled = false;
                btnSubmit.Text = "Submit";
                btnSubmit.Visible = true;
                ddlPeriodSelection.Enabled = true;
                txtRegistrationDate.Enabled = true;
                ddlPaymentMode.Visible = true;


            }

            else if (action == "edit")
            {
                
                ddlSystematicType.Enabled = true;
                ddlportfolio.Enabled = true;
                ddlFolioNumber.Enabled = true;
                txtSearchScheme.Enabled = true;
                txtStartDate.Enabled = true;
                //chkDate1.Enabled = true;
                //chkDate2.Enabled = true;
                //chkDate3.Enabled = true;
                //chkDate4.Enabled = true;
                //chkDate5.Enabled = true;
                //chkDate6.Enabled = true;
                //chkDate7.Enabled = true;
                //chkDate8.Enabled = true;
                //chkDate9.Enabled = true;
                //chkDate10.Enabled = true;
                //chkDate11.Enabled = true;
                //chkDate12.Enabled = true;
                //chkDate13.Enabled = true;
                //chkDate14.Enabled = true;
                //chkDate15.Enabled = true;
                //chkDate16.Enabled = true;
                //chkDate17.Enabled = true;
                //chkDate18.Enabled = true;
                //chkDate19.Enabled = true;
                //chkDate20.Enabled = true;
                //chkDate21.Enabled = true;
                //chkDate22.Enabled = true;
                //chkDate23.Enabled = true;
                //chkDate24.Enabled = true;
                //chkDate25.Enabled = true;
                //chkDate26.Enabled = true;
                //chkDate27.Enabled = true;
                //chkDate28.Enabled = true;
                //chkDate29.Enabled = true;
                //chkDate30.Enabled = true;
                //chkDate31.Enabled = true;
                ddlFrequency.Enabled = true;
                txtAmount.Enabled = true;
                txtPeriod.Enabled = true;
                trSystematicDate.Visible = true;
                txtSystematicDate.Enabled = true;
                trSystematicDateChk1.Visible = false;
                trSystematicDateChk2.Visible = false;
                trSystematicDateChk3.Visible = false;
                           
                txtEndDate.Enabled = false;
                txtRegistrationDate.Enabled = true;
               
                
                ddlPeriodSelection.Enabled = true;

                if ((systematicSetupVo.SystematicTypeCode == "SIP") || (systematicSetupVo.SystematicTypeCode == "STP"))
                {
                    trPaymentMode.Visible = true;
                    ddlPaymentMode.Enabled = true;

                    //if (systematicSetupVo.PaymentModeCode == "PD")
                    //    ddlPaymentMode.SelectedItem.Text = "PDC";
                    //else if (systematicSetupVo.PaymentModeCode == "ES")
                    //    ddlPaymentMode.SelectedItem.Text = "ECS";
                }

                if (systematicSetupVo.SystematicTypeCode == "SIP")
                {
                    trSipChequeNo.Visible = true;
                    trSipChequeDate.Visible = true;
                    trSwitchScheme.Visible = false;
                    lblScheme.Text = "Choose  Scheme:";
                }
                if (systematicSetupVo.SystematicTypeCode == "STP")
                {
                    trSwitchScheme.Visible = true;
                    lblScheme.Text = "Choose Invested Scheme:";
                    trSipChequeDate.Visible = false;
                    trSipChequeNo.Visible = false;
                }
                if (systematicSetupVo.SystematicTypeCode == "SWP")
                {
                    trSwitchScheme.Visible = false;
                    lblScheme.Text = "Choose Invested Scheme:";
                    trSipChequeDate.Visible = false;
                    trSipChequeNo.Visible = false;
                }
                txtPeriod.Text = systematicSetupVo.Period.ToString();
                if (systematicSetupVo.PeriodSelection == "Days")
                    ddlPeriodSelection.SelectedValue = "DA";
                if (systematicSetupVo.PeriodSelection == "Months")
                    ddlPeriodSelection.SelectedValue = "MN";
                if (systematicSetupVo.PeriodSelection == "Years")
                    ddlPeriodSelection.SelectedValue = "YR";

                //if(systematicSetupVo.FrequencyCode == "DA")
                //    ddlPeriodSelection.SelectedItem.Text = "Days";
                //if(systematicSetupVo.FrequencyCode=="MN")
                //    ddlPeriodSelection.SelectedItem.Text = "Months";
                //if(systematicSetupVo.FrequencyCode=="YR")
                //    ddlPeriodSelection.SelectedItem.Text = "Years";
                //ddlPeriodSelection. = systematicSetupVo.FrequencyCode;
                btnSubmit.Text = "Update";

                btnSubmit.Visible = true;
            }
        }

        /// <summary>
        /// Function to submit and Update the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int folioAccountId = 0;
            if (ddlPaymentMode.SelectedItem.Value == "ES")
            {
                hdnddlPaymentMode.Value = "ES";
            }
            else if (ddlPaymentMode.SelectedItem.Value == "PD")
            {
                hdnddlPaymentMode.Value = "PD";
            }
            if (ddlPeriodSelection.SelectedItem.Value == "DA")
            {
                hdnddlPeriodSelection.Value = "Days";
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "MN")
            {
                hdnddlPeriodSelection.Value = "Months";
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "YR")
            {
                hdnddlPeriodSelection.Value = "Years";
            }
            userVo = (UserVo)Session["userVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            
            if (btnSubmit.Text == "Submit")
            {
                if (Session["systematicSetupVo"] != null)
                    systematicSetupVo = (SystematicSetupVo)Session["systematicSetupVo"];
                if (systematicSetupVo == null )
                {
                    systematicSetupVo = new SystematicSetupVo();
                    //systematicSetupVo.SchemePlanCode = int.Parse(txtSchemeCode.Value);
                }
               
                    
            }
            if (btnSubmit.Text == "Update")
            {
                systematicSetupVo = (SystematicSetupVo)Session["systematicSetupVo"];
            }
            if (txtSchemeCode.Value.ToString() != "")
                systematicSetupVo.SchemePlanCode = int.Parse(txtSchemeCode.Value);
            else
                systematicSetupVo.SchemePlanCode = 0;
            if (!string.IsNullOrEmpty(txtSearchScheme.Text.ToString().Trim()))
                systematicSetupVo.SchemePlan = txtSearchScheme.Text;
            else
                systematicSetupVo.SchemePlan = "";
            systematicSetupVo.SystematicTypeCode = ddlSystematicType.SelectedItem.Value.ToString();
            systematicSetupVo.Portfolio = ddlportfolio.SelectedItem.Value;
            systematicSetupVo.Folio= ddlFolioNumber.SelectedItem.Text;

            folioAccountId = systematicSetupBo.GetAccountIdAccodingToFolio(systematicSetupVo.Folio);
            systematicSetupVo.AccountId = folioAccountId;

            //else
            //    systematicSetupVo.AccountId = int.Parse(ddlFolioNumber.SelectedItem.Value.ToString());
            systematicSetupVo.StartDate = DateTime.Parse(txtStartDate.Text.ToString());
            systematicSetupVo.FrequencyCode = ddlFrequency.SelectedItem.Value.ToString();
            systematicSetupVo.Amount = double.Parse(txtAmount.Text.ToString().Trim());
            //systematicSetupVo.EndDate = CalcEndDate(int.Parse(txtPeriod.Text.ToString()), DateTime.Parse(txtStartDate.Text.ToString()));
            systematicSetupVo.EndDate = DateTime.Parse(txtEndDate.Text);
            systematicSetupVo.Period = int.Parse(txtPeriod.Text.ToString());
            systematicSetupVo.IsManual = 1;
            if (!string.IsNullOrEmpty(txtSipChequeDate.Text.ToString().Trim()) )
                systematicSetupVo.SipChequeDate = DateTime.Parse(txtSipChequeDate.Text.ToString());
            else
                systematicSetupVo.SipChequeDate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtSipChecqueNo.Text.ToString().Trim()))
                systematicSetupVo.SipChequeNo = Int64.Parse(txtSipChecqueNo.Text.ToString());
            else
                systematicSetupVo.SipChequeNo = 0;

            if (!string.IsNullOrEmpty(txtRegistrationDate.Text.ToString().Trim()))
                 systematicSetupVo.RegistrationDate = DateTime.Parse(txtRegistrationDate.Text.ToString());
            else
                systematicSetupVo.RegistrationDate = DateTime.MinValue;
            systematicSetupVo.PeriodSelection = hdnddlPeriodSelection.Value;
         
            systematicSetupVo.PaymentModeCode = hdnddlPaymentMode.Value;
            systematicSetupVo.SourceCode = "WP";
            if (systematicSetupVo.SystematicTypeCode == "STP")
            {
                if (!string.IsNullOrEmpty(txtSwicthSchemeSearch.Text.ToString().Trim()))
                {
                    systematicSetupVo.SchemePlanSwitch = txtSwicthSchemeSearch.Text.ToString();
                    systematicSetupVo.SchemePlanCodeSwitch = int.Parse(txtSwitchSchemeCode.Value);
                }

                systematicSetupVo.PaymentMode = hdnddlPaymentMode.Value;
            }
            if (systematicSetupVo.SystematicTypeCode == "SIP")
            {
                if (!string.IsNullOrEmpty(txtSipChequeDate.Text.ToString().Trim()))
                    systematicSetupVo.SipChequeDate = DateTime.Parse(txtSipChequeDate.Text.ToString());
                if (!string.IsNullOrEmpty(txtSipChecqueNo.Text.ToString().Trim()))
                    systematicSetupVo.SipChequeNo = Int64.Parse(txtSipChecqueNo.Text.ToString());
                if(ddlPaymentMode.SelectedItem.Value != "")
                    systematicSetupVo.PaymentMode = ddlPaymentMode.SelectedItem.Value.ToString();
            }

            if (btnSubmit.Text == "Submit")
            {
                //CheckBox chk = new CheckBox();
                //for (int i = 1; i <= 31; i++)
                //{
                //    chk.ID = "chkDate" + i.ToString();
                if (chkDate1.Checked)
                {
                    systematicSetupVo.SystematicDate = 1;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate2.Checked)
                {
                    systematicSetupVo.SystematicDate = 2;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate3.Checked)
                {
                    systematicSetupVo.SystematicDate = 3;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate4.Checked)
                {
                    systematicSetupVo.SystematicDate = 4;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate5.Checked)
                {
                    systematicSetupVo.SystematicDate = 5;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate6.Checked)
                {
                    systematicSetupVo.SystematicDate = 6;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate7.Checked)
                {
                    systematicSetupVo.SystematicDate = 7;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate8.Checked)
                {
                    systematicSetupVo.SystematicDate = 8;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                } 
                if (chkDate9.Checked)
                {
                    systematicSetupVo.SystematicDate = 9;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate10.Checked)
                {
                    systematicSetupVo.SystematicDate = 10;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate11.Checked)
                {
                    systematicSetupVo.SystematicDate = 11;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate12.Checked)
                {
                    systematicSetupVo.SystematicDate = 12;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate13.Checked)
                {
                    systematicSetupVo.SystematicDate = 13;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate14.Checked)
                {
                    systematicSetupVo.SystematicDate = 14;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate15.Checked)
                {
                    systematicSetupVo.SystematicDate = 15;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate16.Checked)
                {
                    systematicSetupVo.SystematicDate = 16;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate17.Checked)
                {
                    systematicSetupVo.SystematicDate = 17;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate18.Checked)
                {
                    systematicSetupVo.SystematicDate = 18;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate19.Checked)
                {
                    systematicSetupVo.SystematicDate = 19;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate20.Checked)
                {
                    systematicSetupVo.SystematicDate = 20;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate21.Checked)
                {
                    systematicSetupVo.SystematicDate = 21;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate22.Checked)
                {
                    systematicSetupVo.SystematicDate = 22;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate23.Checked)
                {
                    systematicSetupVo.SystematicDate = 23;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate24.Checked)
                {
                    systematicSetupVo.SystematicDate = 24;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate25.Checked)
                {
                    systematicSetupVo.SystematicDate = 25;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate26.Checked)
                {
                    systematicSetupVo.SystematicDate = 26;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate27.Checked)
                {
                    systematicSetupVo.SystematicDate = 27;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate28.Checked)
                {
                    systematicSetupVo.SystematicDate = 28;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate29.Checked)
                {
                    systematicSetupVo.SystematicDate = 29;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate30.Checked)
                {
                    systematicSetupVo.SystematicDate = 30;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                }
                if (chkDate31.Checked)
                {
                    systematicSetupVo.SystematicDate = 31;
                    systematicSetupBo.CreateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                    
                }
                if (Session["SourcePage"] != null && Session["SourcePage"].ToString() == "ReconReport")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('CustomerMFSystematicTransactionReport','none');", true);
                    Session["SourcePage"] = "";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioSystematicView','none');", true);

                }
                //}

            }
            if (btnSubmit.Text == "Update")
            {
                //systematicSetupVo.SystematicSetupId = systematicSetupId;
                systematicSetupVo.SystematicDate = int.Parse(txtSystematicDate.Text.ToString());
                systematicSetupBo.UpdateSystematicSchemeSetup(systematicSetupVo, userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioSystematicView','none');", true);

            }

        }


        /// <summary>
        /// Calculates the End Date based on the period by adding it to the Start Date
        /// </summary>
        /// <param name="period"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private DateTime CalcEndDate(int period, DateTime startDate)
        {
            DateTime endDate = new DateTime();
            if (ddlPeriodSelection.SelectedItem.Value == "DA")
            {
                endDate = startDate.AddDays(period);
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "MN")
            {
                endDate = startDate.AddMonths(period);
            }
            else if (ddlPeriodSelection.SelectedItem.Value == "YR")
            {
                endDate = startDate.AddYears(period);
            }
            
            
            
            return endDate;
        }

        /// <summary>
        /// Function to get the Scheme Code from the selected scheme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtScheme_TextChanged(object sender, EventArgs e)
        {
            //lblScheme.Text = txtSearchScheme.Text;
            //schemePlanCode = productMFBo.GetScheme(txtScheme.Text.ToString());

        }
        protected void txtSchemeCode_ValueChanged(object sender, EventArgs e)
        {
            schemePlanCode = int.Parse(txtSchemeCode.Value);
            txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = schemePlanCode.ToString();            
            BindFolioDropDown(int.Parse(ddlportfolio.SelectedValue));
        }
    /// <summary>
    /// Binding the Folio number according to the selection of Scheme name.
    /// </summary>
        private void BindFolioDropDown(int portfolioId)
        {
          
            DataSet dsCustomerAccounts=new DataSet();
            DataTable dtCustomerAccounts;
           
            if (schemePlanCode != 0)
            {
                portfolioId = int.Parse(ddlportfolio.SelectedItem.Value.ToString());
                dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", schemePlanCode);
            }
            //else
            //    dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", 0);

            if (dsCustomerAccounts.Tables.Count > 0)
            {
                dtCustomerAccounts = dsCustomerAccounts.Tables[0];

                ddlFolioNumber.DataSource = dtCustomerAccounts;
                ddlFolioNumber.DataTextField = "CMFA_FolioNum";
                ddlFolioNumber.DataValueField = "CMFA_AccountId";
                ddlFolioNumber.DataBind();
            }
            ddlFolioNumber.Items.Insert(0, "Select");
        }

        protected void ddlSystematicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSystematicType.SelectedValue == "STP")
            {
                Session["SystematicType"] = "STP";
                trSwitchScheme.Visible = true;
                lblScheme.Text = "Choose Invested Scheme:";
                trPaymentMode.Visible = true;
                trSipChequeDate.Visible = false;
                trSipChequeNo.Visible = false;
            }
            if (ddlSystematicType.SelectedValue == "SIP")
            {
                Session["SystematicType"] = "SIP";
                trSwitchScheme.Visible = false;
                lblScheme.Text = "Choose Scheme:";
                trPaymentMode.Visible = true;
                trSipChequeDate.Visible = true;
                trSipChequeNo.Visible = true;

            }
            if (ddlSystematicType.SelectedValue == "SWP")
            {
                Session["SystematicType"] = "SWP";
                trSwitchScheme.Visible = false;
                lblScheme.Text = "Choose Invested Scheme:";
                trPaymentMode.Visible = false;
                trSipChequeDate.Visible = false;
                trSipChequeNo.Visible = false;
            }
            //else
            //{
            //    trSwitchScheme.Visible = false;
            //}
        }

        protected void ddlPeriodSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = CalcEndDate(int.Parse(txtPeriod.Text.ToString()), DateTime.Parse(txtStartDate.Text.ToString())).ToShortDateString();
        }

        protected void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = CalcEndDate(int.Parse(txtPeriod.Text.ToString()), DateTime.Parse(txtStartDate.Text.ToString())).ToShortDateString();
 
        }

        protected void btnAddFolio_Click(object sender, EventArgs e)
        {
            string portfolio = string.Empty;
            if(ddlSystematicType.SelectedIndex!=0 && !string.IsNullOrEmpty(txtSearchScheme.Text.Trim()))
                 SaveCurrentPageState();
            portfolio = ddlportfolio.SelectedItem.Text;
            portfolioId = systematicSetupBo.GetPortFolioId(portfolio, customerVo.CustomerId);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?FromSysPage=PortfolioSystematicEntry');", true);
            //Response.Redirect("ControlHost.aspx?pageid=CustomerMFAccountAdd&FromPage=" + "PortfolioSystematicEntry" + "&PortFolioId=" +  portfolioId +  , false);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CustomerMFAccount", "loadcontrol('CustomerMFAccountAdd','?PortFolioId=" + portfolioId + "  ');", true);



            
        }

        private void SaveCurrentPageState()
        {
            Hashtable hashSaveCurrentPageStatus = new Hashtable();
            hashSaveCurrentPageStatus.Add("SystematicType", ddlSystematicType.SelectedValue);
            hashSaveCurrentPageStatus.Add("SearchScheme", txtSearchScheme.Text);
            hashSaveCurrentPageStatus.Add("SchemeCode", txtSchemeCode.Value);
            hashSaveCurrentPageStatus.Add("SwicthSchemeSearch", txtSwicthSchemeSearch.Text);
           // BindPortfolioDropDown();
            hashSaveCurrentPageStatus.Add("Portfolio", ddlportfolio.SelectedItem.Value);
            hashSaveCurrentPageStatus.Add("FolioNumber", ddlFolioNumber.SelectedValue);
            hashSaveCurrentPageStatus.Add("StartDate", txtStartDate.Text);
            hashSaveCurrentPageStatus.Add("SipChequeDate", txtSipChequeDate.Text);
            hashSaveCurrentPageStatus.Add("SipChecqueNo", txtSipChecqueNo.Text);
            SaveCheckboxes();
            hashSaveCurrentPageStatus.Add("SystematicDate", txtSystematicDate.Text);
            hashSaveCurrentPageStatus.Add("Frequency", ddlFrequency.SelectedValue);
            hashSaveCurrentPageStatus.Add("Amount", txtAmount.Text);
            hashSaveCurrentPageStatus.Add("Period", txtPeriod.Text);
            hashSaveCurrentPageStatus.Add("PeriodSelection", ddlPeriodSelection.SelectedValue);
            hashSaveCurrentPageStatus.Add("EndDate", txtEndDate.Text);
            hashSaveCurrentPageStatus.Add("RegistrationDate", txtRegistrationDate.Text);
            hashSaveCurrentPageStatus.Add("PaymentMode", ddlPaymentMode.SelectedValue);

            Session["SystematicHT"] = hashSaveCurrentPageStatus;
        }

        private void SaveCheckboxes()
        {
            Hashtable hashSavecheckBoxes = new Hashtable();
            if (chkDate1.Checked == true)
                hashSavecheckBoxes.Add("chkDate1", 1);
            else
                hashSavecheckBoxes.Add("chkDate1", 0);
            if (chkDate2.Checked == true)
                hashSavecheckBoxes.Add("chkDate2", 1);
            else
                hashSavecheckBoxes.Add("chkDate2", 0);
            if (chkDate3.Checked == true)
                hashSavecheckBoxes.Add("chkDate3", 1);
            else
                hashSavecheckBoxes.Add("chkDate3", 0);
            if (chkDate4.Checked == true)
                hashSavecheckBoxes.Add("chkDate4", 1);
            else
                hashSavecheckBoxes.Add("chkDate4", 0);
            if (chkDate5.Checked == true)
                hashSavecheckBoxes.Add("chkDate5", 1);
            else
                hashSavecheckBoxes.Add("chkDate5", 0);
            if (chkDate6.Checked == true)
                hashSavecheckBoxes.Add("chkDate6", 1);
            else
                hashSavecheckBoxes.Add("chkDate6", 0);
            if (chkDate7.Checked == true)
                hashSavecheckBoxes.Add("chkDate7", 1);
            else
                hashSavecheckBoxes.Add("chkDate7", 0);
            if (chkDate8.Checked == true)
                hashSavecheckBoxes.Add("chkDate8", 1);
            else
                hashSavecheckBoxes.Add("chkDate8", 0);
            if (chkDate10.Checked == true)
                hashSavecheckBoxes.Add("chkDate10", 1);
            else
                hashSavecheckBoxes.Add("chkDate10", 0);

            if (chkDate11.Checked == true)
                hashSavecheckBoxes.Add("chkDate11", 1);
            else
                hashSavecheckBoxes.Add("chkDate11", 0);
            if (chkDate12.Checked == true)
                hashSavecheckBoxes.Add("chkDate12", 1);
            else
                hashSavecheckBoxes.Add("chkDate12", 0);
            if (chkDate13.Checked == true)
                hashSavecheckBoxes.Add("chkDate13", 1);
            else
                hashSavecheckBoxes.Add("chkDate13", 0);
            if (chkDate14.Checked == true)
                hashSavecheckBoxes.Add("chkDate14", 1);
            else
                hashSavecheckBoxes.Add("chkDate14", 0);
            if (chkDate15.Checked == true)
                hashSavecheckBoxes.Add("chkDate15", 1);
            else
                hashSavecheckBoxes.Add("chkDate15", 0);
            if (chkDate16.Checked == true)
                hashSavecheckBoxes.Add("chkDate16", 1);
            else
                hashSavecheckBoxes.Add("chkDate16", 0);
            if (chkDate17.Checked == true)
                hashSavecheckBoxes.Add("chkDate17", 1);
            else
                hashSavecheckBoxes.Add("chkDate17", 0);
            if (chkDate18.Checked == true)
                hashSavecheckBoxes.Add("chkDate18", 1);
            else
                hashSavecheckBoxes.Add("chkDate18", 0);
            if (chkDate19.Checked == true)
                hashSavecheckBoxes.Add("chkDate19", 1);
            else
                hashSavecheckBoxes.Add("chkDate19", 0);
            if (chkDate20.Checked == true)
                hashSavecheckBoxes.Add("chkDate20", 1);
            else
                hashSavecheckBoxes.Add("chkDate20", 0);
            if (chkDate21.Checked == true)
                hashSavecheckBoxes.Add("chkDate21", 1);
            else
                hashSavecheckBoxes.Add("chkDate21", 0);
            if (chkDate22.Checked == true)
                hashSavecheckBoxes.Add("chkDate22", 1);
            else
                hashSavecheckBoxes.Add("chkDate22", 0);
            if (chkDate23.Checked == true)
                hashSavecheckBoxes.Add("chkDate23", 1);
            else
                hashSavecheckBoxes.Add("chkDate23", 0);
            if (chkDate24.Checked == true)
                hashSavecheckBoxes.Add("chkDate24", 1);
            else
                hashSavecheckBoxes.Add("chkDate24", 0);
            if (chkDate25.Checked == true)
                hashSavecheckBoxes.Add("chkDate25", 1);
            else
                hashSavecheckBoxes.Add("chkDate25", 0);
            if (chkDate26.Checked == true)
                hashSavecheckBoxes.Add("chkDate26", 1);
            else
                hashSavecheckBoxes.Add("chkDate26", 0);
            if (chkDate27.Checked == true)
                hashSavecheckBoxes.Add("chkDate27", 1);
            else
                hashSavecheckBoxes.Add("chkDate27", 0);
            if (chkDate28.Checked == true)
                hashSavecheckBoxes.Add("chkDate28", 1);
            else
                hashSavecheckBoxes.Add("chkDate28", 0);
            if (chkDate29.Checked == true)
                hashSavecheckBoxes.Add("chkDate29", 1);
            else
                hashSavecheckBoxes.Add("chkDate29", 0);
            if (chkDate30.Checked == true)
                hashSavecheckBoxes.Add("chkDate30", 1);
            else
                hashSavecheckBoxes.Add("chkDate30", 0);
            if (chkDate31.Checked == true)
                hashSavecheckBoxes.Add("chkDate31", 1);
            else
                hashSavecheckBoxes.Add("chkDate31", 0);

            Session["MaintainCheckBoxes"] = hashSavecheckBoxes;
        }

        //protected void ddlportfolio_ValueChanged(object sender, EventArgs e)
        //{
        //    portfolioId = int.Parse(ddlportfolio.SelectedItem.Value.ToString());
        //    //Session[SessionContents.PortfolioId] = portfolioId;
        //    BindFolioDropDown(portfolioId);
        //}

        protected void ddlportfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlportfolio.SelectedItem.Value.ToString());
            if (txtSchemeCode.Value != "")
                schemePlanCode = int.Parse(txtSchemeCode.Value);
            else
                schemePlanCode = 0;
            BindFolioDropDown(portfolioId);
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPeriod.Text.ToString().Trim()))
            {
                txtEndDate.Text = CalcEndDate(int.Parse(txtPeriod.Text.ToString()), DateTime.Parse(txtStartDate.Text.ToString())).ToShortDateString();
            }
        }

      }
}