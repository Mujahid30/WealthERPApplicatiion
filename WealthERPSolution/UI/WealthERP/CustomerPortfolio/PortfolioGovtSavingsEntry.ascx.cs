using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using WealthERP.Base;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoUser;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.CustomerPortfolio
{
    enum Mode { Add, Edit, View };

    public partial class PortfolioGovtSavingsEntry : System.Web.UI.UserControl
    {

        Mode mode = Mode.Add;
        UserVo userVo = new UserVo();
        DataSet ds = new DataSet();
        AssetBo assetBo = new AssetBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        GovtSavingsBo govtSavingsBo = new GovtSavingsBo();
        GovtSavingsVo govtSavingsVo = new GovtSavingsVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();

        int portfolioId;
        string command;
        string path;


        /// <summary>
        /// This method will loop through the child controls in user control and disable the controls.
        /// This method is called in view mode.
        /// </summary>
        /// <param name="control"></param>
        private void DisableControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl.GetType().Name == "TextBox")
                {
                    ((TextBox)(ctrl)).Enabled = false;
                }
                else if (ctrl.GetType().Name == "DropDownList")
                {
                    ((DropDownList)(ctrl)).Enabled = false;

                }
                else if (ctrl.GetType().Name == "RadioButton")
                {
                    ((RadioButton)(ctrl)).Enabled = false;
                }
                if (ctrl.Controls.Count > 0)
                {
                    DisableControls(ctrl);
                }
            }
        }
        /// <summary>
        /// Load the Govt Savings values to the UI elements.
        /// </summary>
        private void SetValues()
        {
            try
            {
                lblInstrumentCategory.Text = customerAccountsVo.AssetCategoryName;
                txtAccountId.Text = customerAccountsVo.AccountNum;
                txtAssetParticulars.Text = govtSavingsVo.Name;
                if (txtAccOpenDate != null && customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                    txtAccOpenDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
                txtAccountWith.Text = customerAccountsVo.AccountSource;

                if (txtDepositDate != null && govtSavingsVo.PurchaseDate != DateTime.MinValue)
                    txtDepositDate.Text = govtSavingsVo.PurchaseDate.ToShortDateString();
                if (txtMaturityDate != null && govtSavingsVo.MaturityDate != DateTime.MinValue)
                    txtMaturityDate.Text = govtSavingsVo.MaturityDate.ToShortDateString();
                if (txtDepositAmount != null)
                    txtDepositAmount.Text = govtSavingsVo.DepositAmt.ToString();
                if (txtSubsqntDepositAmount != null)
                    txtSubsqntDepositAmount.Text = govtSavingsVo.SubsqntDepositAmount.ToString();
                if (txtSubsqntDepositDate != null && govtSavingsVo.SubsqntDepositDate != DateTime.MinValue)
                    txtSubsqntDepositDate.Text = govtSavingsVo.SubsqntDepositDate.ToShortDateString();
                if (txtInterstRate != null)
                    txtInterstRate.Text = govtSavingsVo.InterestRate.ToString();
                if (txtInterestAmtCredited != null)
                    txtInterestAmtCredited.Text = govtSavingsVo.InterestAmtPaidOut.ToString();
                if (txtCurrentValue != null)
                    txtCurrentValue.Text = govtSavingsVo.CurrentValue.ToString();
                if (txtMaturityValue != null)
                    txtMaturityValue.Text = govtSavingsVo.MaturityValue.ToString();
                if (txtRemarks != null)
                    txtRemarks.Text = govtSavingsVo.Remarks.ToString();

                //Assign dropdown selected values
                if (ddlModeOfHolding != null && ddlModeOfHolding.Items.Count > 0)
                    ddlModeOfHolding.SelectedValue = customerAccountsVo.ModeOfHolding.Trim();

                if (ddlDebtIssuerCode != null && ddlDebtIssuerCode.Items.Count > 0)
                    ddlDebtIssuerCode.SelectedValue = govtSavingsVo.DebtIssuerCode.Trim();

                if (ddlInterestBasis != null && ddlInterestBasis.Items.Count > 0)
                    ddlInterestBasis.SelectedValue = govtSavingsVo.InterestBasisCode.ToString().Trim();
                if (govtSavingsVo.InterestBasisCode == "SI") //Simple interest
                {
                    trSimpleInterest.Visible = true;
                    if (ddlSimpleInterestFC != null && ddlSimpleInterestFC.Items.Count > 0)
                        ddlSimpleInterestFC.SelectedValue = govtSavingsVo.InterestPayableFrequencyCode.ToString().Trim();
                }
                else if (govtSavingsVo.InterestBasisCode == "CI") //Compound interest
                {
                    trCompoundInterest.Visible = true;
                    if (ddlCompoundInterestFC != null && ddlCompoundInterestFC.Items.Count > 0)
                        ddlCompoundInterestFC.SelectedValue = govtSavingsVo.CompoundInterestFrequencyCode;
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
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:LoadViewDetails()");
                object[] objects = new object[2];
                objects[0] = govtSavingsVo;
                objects[1] = customerAccountsVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// Get the Govt Savings related data for the particular Id
        /// </summary>
        private void GetData()
        {
            govtSavingsVo = (GovtSavingsVo)Session["govtSavingsVo"];
            customerAccountsVo = customerAccountBo.GetGovtSavingsAccount(govtSavingsVo.AccountId);
        }
        /// <summary>
        /// Load dropdown boxes with values.
        /// </summary>
        private void LoadDropdownValues()
        {
            LoadModeOfHolding(path);
            LoadDebtIssuerCode(path);
            LoadInterestBasis(path);
            LoadFrequencyCode(path);
            LoadCompoundFrequencyCode(path);
        }
        /// <summary>
        /// Set the mode to Edit/Add/View based on the Session["action"] value
        /// </summary>
        private void setMode()
        {
            if (Session["action"].ToString() == "Edit")
                mode = Mode.Edit;
            else if (Session["action"].ToString() == "View")
                mode = Mode.View;
            else
                mode = Mode.Add;
        }
        /// <summary>
        /// Show/hide  fields/validations based on the asset categories selected.
        /// </summary>
        public void SetFields()
        {
            if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccOpenDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
            txtAccountId.Text = customerAccountsVo.AccountId.ToString();
            if (customerAccountsVo.AccountSource != null)
                txtAccountWith.Text = customerAccountsVo.AccountSource.ToString();
            if (customerAccountsVo.AssetCategory != null)
                lblInstrumentCategory.Text = customerAccountsVo.AssetCategory.ToString();
            ddlModeOfHolding.SelectedValue = customerAccountsVo.ModeOfHolding.Trim();
            switch (customerAccountsVo.AssetCategory)
            {
                case "GSIP": // IVP
                    break;
                case "GSKP": //KVP
                    break;
                case "GSNC": //NSC
                    break;
                case "GSNS": //NSS
                    break;
                case "GSSC": //Senior Citizens Savings Scheme

                    break;
                case "GSMS": //Post Office MIS
                    break;
                case "GSTD": //Post Office Time Deposit Account
                    //----------------
                    break;
                case "GSTB": //GOI Tax Savings Bonds 
                    break;
                case "GSRB": //GOI Relief Bonds 
                    break;
                case "GSNB": //Nabard Rural Bonds
                    break;
                //----------------
                case "GSSB": //Post Office Savings Bank Account 
                    spanValidationMaturityDate.Visible = false;
                    tdDepositAndMaturityDate.Visible = false;
                    lblDepositAmount.Text = "Account Balance as on date";
                    tdMaturityValue.Visible = false;
                    lblMaturityValue.Visible = false;
                    break;
                //---------------------
                case "GSRD": //Post Office Recurring Deposit Account 
                    lblDepositDate.Text = "Initial Deposit date:";
                    lblDepositAmount.Text = "Initial Deposit Amount:";
                    trSubsequent.Visible = true;
                    trSubsequentFrequency.Visible = true;
                    LoadDepositFrequencyCode(path);
                    if (govtSavingsVo.DepositFrequencyCode != null)
                        ddlDepositFrequency.SelectedValue = govtSavingsVo.DepositFrequencyCode;
                    break;
            }
        }
        /// <summary>
        /// Load Deposit frequency dropdown
        /// </summary>
        /// <param name="path"></param>
        public void LoadDepositFrequencyCode(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetFrequency(path);
                ddlDepositFrequency.DataSource = dt;
                ddlDepositFrequency.DataTextField = dt.Columns["Frequency"].ToString();
                ddlDepositFrequency.DataValueField = dt.Columns["FrequencyCode"].ToString();
                ddlDepositFrequency.DataBind();
                ddlDepositFrequency.Items.Insert(0, new ListItem("Select a Frequency", "NA"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:LoadDepositFrequencyCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// Load Frequency code dropdown
        /// </summary>
        /// <param name="path"></param>
        public void LoadFrequencyCode(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetFrequency(path);
                ddlSimpleInterestFC.DataSource = dt;
                ddlSimpleInterestFC.DataTextField = dt.Columns["Frequency"].ToString();
                ddlSimpleInterestFC.DataValueField = dt.Columns["FrequencyCode"].ToString();
                ddlSimpleInterestFC.DataBind();
                ddlSimpleInterestFC.Items.Insert(0, new ListItem("Select a Frequency", "NA"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:LoadFrequencyCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// Load compound frequency code
        /// </summary>
        /// <param name="path"></param>
        public void LoadCompoundFrequencyCode(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetFrequency(path);
                ddlCompoundInterestFC.DataSource = dt;
                ddlCompoundInterestFC.DataTextField = dt.Columns["Frequency"].ToString();
                ddlCompoundInterestFC.DataValueField = dt.Columns["FrequencyCode"].ToString();
                ddlCompoundInterestFC.DataBind();
                ddlCompoundInterestFC.Items.Insert(0, new ListItem("Select a Frequency", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:LoadCompoundFrequencyCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// Load Interest Basis
        /// </summary>
        /// <param name="path"></param>
        public void LoadInterestBasis(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetInterestBasis(path);
                ddlInterestBasis.DataSource = dt;
                ddlInterestBasis.DataTextField = dt.Columns["InterestBasisType"].ToString();
                ddlInterestBasis.DataValueField = dt.Columns["InterestBasisCode"].ToString();
                ddlInterestBasis.DataBind();
                ddlInterestBasis.Items.Insert(0, new ListItem("Select an Interest Basis", "0"));
                ddlInterestBasis.SelectedIndex = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:LoadInterestBasis()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// Load Debt Issuer code
        /// </summary>
        /// <param name="path"></param>
        public void LoadDebtIssuerCode(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetDebtIssuer(path);
                ddlDebtIssuerCode.DataSource = dt;
                ddlDebtIssuerCode.DataTextField = dt.Columns["DebtIssuerName"].ToString();
                ddlDebtIssuerCode.DataValueField = dt.Columns["DebtIssuerCode"].ToString();
                ddlDebtIssuerCode.DataBind();
                ddlDebtIssuerCode.Items.Insert(0, new ListItem("Select an Asset Issuer", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:LoadDebtIssuerCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// Load mode of holding dropdown
        /// </summary>
        /// <param name="path"></param>
        public void LoadModeOfHolding(string path)
        {
            try
            {
                DataTable dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.Items.Insert(0, new ListItem("Select a Mode of Holding", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:LoadModeOfHolding()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// Read the user entered values from fields and assign it to GovtSavingsVo object.
        /// </summary>
        /// <returns></returns>
        private GovtSavingsVo GetValuesFromFields()
        {

            GovtSavingsVo newGovtSavingsVo = new GovtSavingsVo();

            try
            {
                customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                customerAccountsVo.AccountId = customerAccountsVo.AccountId;//Convert.ToInt32(txtAccountId.Text);
                if (txtAccOpenDate != null && txtAccOpenDate.Text != string.Empty)
                    customerAccountsVo.AccountOpeningDate = Convert.ToDateTime(txtAccOpenDate.Text);

                customerAccountsVo.AccountSource = txtAccountWith.Text;
                //If Edit, get the Id of  govt savings net data.
                if (mode == Mode.Edit)
                {
                    GovtSavingsVo govtSavngs = (GovtSavingsVo)Session["govtSavingsVo"];
                    newGovtSavingsVo.GoveSavingsPortfolioId = govtSavngs.GoveSavingsPortfolioId;
                }
                newGovtSavingsVo.PortfolioId = customerAccountsVo.PortfolioId;
                newGovtSavingsVo.AccountId = customerAccountsVo.AccountId;
                newGovtSavingsVo.AssetGroupCode = customerAccountsVo.AssetClass;
                newGovtSavingsVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;

                if (ddlDebtIssuerCode != null && ddlDebtIssuerCode.SelectedItem.Value != string.Empty)
                    newGovtSavingsVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();

                newGovtSavingsVo.Name = txtAssetParticulars.Text;

                if (txtCurrentValue != null && txtCurrentValue.Text != "")
                    newGovtSavingsVo.CurrentValue = float.Parse(txtCurrentValue.Text);



                if (txtDepositDate != null && txtDepositDate.Text != "")
                    newGovtSavingsVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text.ToString());

                if (txtMaturityValue != null && txtMaturityValue.Text != "")
                    newGovtSavingsVo.MaturityValue = float.Parse(txtMaturityValue.Text);
                if (txtMaturityDate != null && txtMaturityDate.Text != "")
                    newGovtSavingsVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text.ToString());
                if (txtDepositAmount != null && txtDepositAmount.Text != "")
                    newGovtSavingsVo.DepositAmt = float.Parse(txtDepositAmount.Text);
                if (txtInterstRate != null && txtInterstRate.Text != "")
                    newGovtSavingsVo.InterestRate = float.Parse(txtInterstRate.Text);
                if (ddlInterestBasis != null)
                    newGovtSavingsVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();

                if (newGovtSavingsVo.InterestBasisCode == "SI")
                {
                    newGovtSavingsVo.InterestPayableFrequencyCode = ddlSimpleInterestFC.SelectedItem.Value.ToString();
                    newGovtSavingsVo.CompoundInterestFrequencyCode = "NA";
                }
                else if (newGovtSavingsVo.InterestBasisCode == "CI")
                {
                    newGovtSavingsVo.InterestPayableFrequencyCode = "NA";
                    newGovtSavingsVo.CompoundInterestFrequencyCode = ddlCompoundInterestFC.SelectedItem.Value.ToString();
                }
                else
                {
                    newGovtSavingsVo.InterestBasisCode = null;
                }

                if (rbtnAccumulated.Checked)
                {
                    newGovtSavingsVo.IsInterestAccumalated = 1;
                    if (txtInterestAmtCredited != null && txtInterestAmtCredited.Text != string.Empty)
                        newGovtSavingsVo.InterestAmtAccumalated = float.Parse(txtInterestAmtCredited.Text);
                    newGovtSavingsVo.InterestAmtPaidOut = 0;
                }
                else
                {
                    newGovtSavingsVo.IsInterestAccumalated = 0;
                    if (txtInterestAmtCredited != null && txtInterestAmtCredited.Text != string.Empty)
                        newGovtSavingsVo.InterestAmtPaidOut = float.Parse(txtInterestAmtCredited.Text);
                    newGovtSavingsVo.InterestAmtAccumalated = 0;
                }

                //Post Office Recurring Deposit Account- Subsequent deposit details
                if (customerAccountsVo.AssetCategory != null && customerAccountsVo.AssetCategory.ToString().Trim() == "GSRD")
                {
                    if (txtSubsqntDepositAmount != null && txtSubsqntDepositAmount.Text != string.Empty)
                        newGovtSavingsVo.SubsqntDepositAmount = float.Parse(txtSubsqntDepositAmount.Text.Trim());
                    if (txtSubsqntDepositDate != null && txtSubsqntDepositDate.Text != string.Empty)
                        newGovtSavingsVo.SubsqntDepositDate = Convert.ToDateTime(txtSubsqntDepositDate.Text);

                    if (ddlDepositFrequency != null && ddlDepositFrequency.SelectedValue != string.Empty)
                        newGovtSavingsVo.DepositFrequencyCode = ddlDepositFrequency.SelectedValue;
                }

                newGovtSavingsVo.Remarks = txtRemarks.Text;
                return newGovtSavingsVo;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:GetValuesFromFields()");
                object[] objects = new object[3];
                //objects[0] = govtSavingsVo;
                //objects[1] = customerAccountsVo;
                //objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void UpdateAccountDetails(CustomerAccountsVo customerAccountVo)
        {
            CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
            try
            {
                newAccountVo.AccountId = customerAccountVo.AccountId;
                newAccountVo.AccountNum = txtAccountId.Text;
                if (txtAccOpenDate.Text.Trim() != string.Empty)
                newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccOpenDate.Text.Trim());
                newAccountVo.AccountSource = txtAccountWith.Text.ToString();
                newAccountVo.AssetCategory = customerAccountVo.AssetCategory.Trim();
                newAccountVo.AssetClass = customerAccountVo.AssetClass.Trim();
                newAccountVo.ModeOfHolding = customerAccountVo.ModeOfHolding.Trim();

                Session["newAccountVo"] = newAccountVo;
                govtSavingsBo.UpdateGovtSavingsAccount(newAccountVo, userVo.UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:UpdateAccountDetails()");
                object[] objects = new object[2];
                objects[0] = newAccountVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
            //cvAccopenDateCheckCurrent.ValueToCompare = DateTime.Now.ToShortDateString();
            try
            {

                cvDepositDate1.ValueToCompare = DateTime.Now.ToShortDateString();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                setMode(); //Set Add/Edit/View Mode based on the query string parameter.

                if (Session[SessionContents.PortfolioId] != null)
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());

                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["customerVo"];

                if (!Page.IsPostBack)
                {

                    if (userVo == null || customerVo == null || portfolioId == 0 || customerAccountsVo == null || govtSavingsVo == null)
                    {
                        //Invalid data. Write redirect code here. or show error message.
                    }
                    if (mode == Mode.Edit)
                    {
                        govtSavingsVo = (GovtSavingsVo)Session["govtSavingsVo"];
                        if (govtSavingsVo != null)
                            customerAccountsVo = customerAccountBo.GetGovtSavingsAccount(govtSavingsVo.AccountId);
                        Session["customerAccountVo"] = customerAccountsVo;
                        GetData();
                        LoadDropdownValues();
                        SetFields();
                        SetValues();
                        lnkEdit.Visible = false;
                        btnSubmit.Visible = false;
                        btnSaveChanges.Visible = true;
                    }
                    else if (mode == Mode.View)
                    {
                        govtSavingsVo = (GovtSavingsVo)Session["govtSavingsVo"];
                        if (govtSavingsVo != null)
                            customerAccountsVo = customerAccountBo.GetGovtSavingsAccount(govtSavingsVo.AccountId);

                        GetData();
                        LoadDropdownValues();
                        SetFields();
                        SetValues();

                        btnSubmit.Visible = false;
                        btnSaveChanges.Visible = false;
                        lnkEdit.Visible = true;
                        DisableControls(this);
                    }
                    else if (mode == Mode.Add)
                    {
                        customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                        LoadDropdownValues();
                        SetFields();
                        btnSubmit.Visible = true;
                        btnSaveChanges.Visible = false;
                        lnkEdit.Visible = false;
                    }
                }
                else
                {
                    if (mode == Mode.Add)
                    {
                        customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
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
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:Page_Load()");
                object[] objects = new object[6];
                objects[0] = userVo;
                objects[1] = customerVo;
                objects[2] = portfolioId;
                objects[3] = customerAccountsVo;
                objects[4] = command;
                objects[5] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            govtSavingsVo = GetValuesFromFields();
            bool bResult = govtSavingsBo.CreateGovtSavingsNP(govtSavingsVo, userVo.UserId);
            UpdateAccountDetails(customerAccountsVo);
            if (bResult)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('ViewGovtSavings','none');", true);
            }
        }

        protected void ddlInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInterestBasis.SelectedItem.Value.ToString().Trim() == "CI")
            {   // If Compound Interest then
                LoadCompoundFrequencyCode(path);
                trCompoundInterest.Visible = true;
                trSimpleInterest.Visible = false;

            }
            else if (ddlInterestBasis.SelectedItem.Value.ToString().Trim() == "SI")   // If Simple Interest 
            {
                LoadFrequencyCode(path);
                trCompoundInterest.Visible = false;
                trSimpleInterest.Visible = true;
            }
            else
            {
                trCompoundInterest.Visible = false;
                trSimpleInterest.Visible = false;
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioGovtSavingsEntry','action=Edit');", true);
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                govtSavingsVo = GetValuesFromFields();
                //TODO : Check if the below two lines are needed.
                customerAccountsVo = customerAccountBo.GetGovtSavingsAccount(govtSavingsVo.AccountId);
                UpdateAccountDetails(customerAccountsVo);
                //UpdatePortfolioDetails(govtSavingsVo);
                govtSavingsBo.UpdateGovtSavingsNP(govtSavingsVo, userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGovtSavings','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioGovtSavingsEntry.ascx:btnSaveChanges_Click()");
                object[] objects = new object[2];
                objects[0] = govtSavingsVo;
                objects[1] = customerAccountsVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            if(mode==Mode.Edit || mode == Mode.View)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('ViewGovtSavings', 'none')", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', 'action=GS')", true);
        }
        #endregion Events

        //protected void txtDepositAmount_TextChanged(object sender, EventArgs e)
        //{
        //    txtCurrentValue.Text = txtDepositAmount.Text;
        //}
    }
}