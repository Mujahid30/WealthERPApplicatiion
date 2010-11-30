using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoUser;
using BoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;

namespace WealthERP.CustomerPortfolio
{
    public partial class PensionAndGratuities : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        DataSet ds = new DataSet();
        
        AssetBo assetBo = new AssetBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerVo customerVo = new CustomerVo();
        CustomerAccountsVo customerAccountVo;
        PensionAndGratuitiesVo pensionAndGratuitiesVo = new PensionAndGratuitiesVo();
        static int portfolioId;
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        string path;
        string AssetGroupCode = "PG";
        string Manage;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["customerVo"];
                customerAccountVo = (CustomerAccountsVo)Session["customerAccountVo"];
                pensionAndGratuitiesVo = (PensionAndGratuitiesVo)Session["pensionAndGratuitiesVo"];
          
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();

                    LoadAccountDetails(path);
                    DisableAllTRs();
                    BindDropDowns(path, customerAccountVo.AssetCategory.ToString().Trim());

                    // Check Querystring to see if its an Edit/View/Entry
                    if (Request.QueryString["action"] != null)
                        Manage = Request.QueryString["action"].ToString();

                    if (pensionAndGratuitiesVo != null)
                    {
                        if (Manage == "edit")
                        {
                            trEditButton.Visible = false;
                            trEditSpace.Visible = false;

                            LoadFields(pensionAndGratuitiesVo, customerAccountVo, "edit");
                        }
                        else if (Manage == "view")
                        {
                            trEditButton.Visible = true;
                            trEditSpace.Visible = true;

                            LoadFields(pensionAndGratuitiesVo, customerAccountVo, "view");
                        }
                        else
                        {
                            trEditButton.Visible = false;
                            trEditSpace.Visible = false;

                            LoadFields(pensionAndGratuitiesVo, customerAccountVo, "entry");
                        }
                    }
                    else
                    {
                        trEditButton.Visible = false;
                        trEditSpace.Visible = false;

                        LoadFields(pensionAndGratuitiesVo, customerAccountVo, "entry");
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
                FunctionInfo.Add("Method", "PensionAndGratuities.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = pensionAndGratuitiesVo;
                objects[1] = customerVo;
                objects[2] = userVo;
                objects[3] = customerAccountVo;
                objects[4] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }
        private void BindDropDowns(string path, string AssetInstrumentCat)
        {
            DataTable dtFiscal;
            DataTable dtInterestBasis;
            DataTable dtInterestFreq;
            try
            {

                dtFiscal = XMLBo.GetFiscalYearCode(path);
                dtInterestBasis = XMLBo.GetInterestBasis(path);
                dtInterestFreq = XMLBo.GetFrequency(path);

                if (AssetInstrumentCat == "PGGY")
                {
                    // Do Nothin
                }
                else if (AssetInstrumentCat == "PGEF")
                {
                    // Bind Interest Basis Drop Downs
                    ddlEPFInterestBasis.DataSource = dtInterestBasis;
                    ddlEPFInterestBasis.DataTextField = dtInterestBasis.Columns["InterestBasisType"].ToString();
                    ddlEPFInterestBasis.DataValueField = dtInterestBasis.Columns["InterestBasisCode"].ToString();
                    ddlEPFInterestBasis.DataBind();
                    ddlEPFInterestBasis.Items.Insert(0, new ListItem("Select an Interest Basis", "Select an Interest Basis"));

                    // Bind the EPF Accum Fiscal Year Drop Down
                    ddlEPFAccumFiscalYear.DataSource = dtFiscal;
                    ddlEPFAccumFiscalYear.DataTextField = dtFiscal.Columns["FiscalYear"].ToString();
                    ddlEPFAccumFiscalYear.DataValueField = dtFiscal.Columns["FiscalYearCode"].ToString();
                    ddlEPFAccumFiscalYear.DataBind();
                    ddlEPFAccumFiscalYear.Items.Insert(0, new ListItem("Select a Fiscal Year", "Select a Fiscal Year"));

                    // Bind Interest FReq Codes
                    ddlEPFInterestCalFreq.DataSource = dtInterestFreq;
                    ddlEPFInterestCalFreq.DataTextField = dtInterestFreq.Columns["Frequency"].ToString();
                    ddlEPFInterestCalFreq.DataValueField = dtInterestFreq.Columns["FrequencyCode"].ToString();
                    ddlEPFInterestCalFreq.DataBind();
                    ddlEPFInterestCalFreq.Items.Insert(0, new ListItem("Select a Frequency", "Select a Frequency"));

                }
                else if (AssetInstrumentCat == "PGPF")
                {
                    // Bind Interest Basis Drop Downs
                    ddlPPFInterestBasis.DataSource = dtInterestBasis;
                    ddlPPFInterestBasis.DataTextField = dtInterestBasis.Columns["InterestBasisType"].ToString();
                    ddlPPFInterestBasis.DataValueField = dtInterestBasis.Columns["InterestBasisCode"].ToString();
                    ddlPPFInterestBasis.DataBind();
                    ddlPPFInterestBasis.Items.Insert(0, new ListItem("Select an Interest Basis", "Select an Interest Basis"));

                    // Bind the PPF Accum Fiscal Year Drop Down
                    ddlPPFAccumFiscal.DataSource = dtFiscal;
                    ddlPPFAccumFiscal.DataTextField = dtFiscal.Columns["FiscalYear"].ToString();
                    ddlPPFAccumFiscal.DataValueField = dtFiscal.Columns["FiscalYearCode"].ToString();
                    ddlPPFAccumFiscal.DataBind();
                    ddlPPFAccumFiscal.Items.Insert(0, new ListItem("Select a Fiscal Year", "Select a Fiscal Year"));

                    // Bind Interest FReq Codes
                    ddlPPFInterestFrequency.DataSource = dtInterestFreq;
                    ddlPPFInterestFrequency.DataTextField = dtInterestFreq.Columns["Frequency"].ToString();
                    ddlPPFInterestFrequency.DataValueField = dtInterestFreq.Columns["FrequencyCode"].ToString();
                    ddlPPFInterestFrequency.DataBind();
                    ddlPPFInterestFrequency.Items.Insert(0, new ListItem("Select a Frequency", "Select a Frequency"));
                }
                else if (AssetInstrumentCat == "PGSN")
                {
                    // Bind Interest Basis Drop Downs
                    ddlSuperInterestBasis.DataSource = dtInterestBasis;
                    ddlSuperInterestBasis.DataTextField = dtInterestBasis.Columns["InterestBasisType"].ToString();
                    ddlSuperInterestBasis.DataValueField = dtInterestBasis.Columns["InterestBasisCode"].ToString();
                    ddlSuperInterestBasis.DataBind();
                    ddlSuperInterestBasis.Items.Insert(0, new ListItem("Select an Interest Basis", "Select an Interest Basis"));

                    // Bind the SuperAnnuation Accum Fiscal Year Drop Down
                    ddlSuperAccumFiscal.DataSource = dtFiscal;
                    ddlSuperAccumFiscal.DataTextField = dtFiscal.Columns["FiscalYear"].ToString();
                    ddlSuperAccumFiscal.DataValueField = dtFiscal.Columns["FiscalYearCode"].ToString();
                    ddlSuperAccumFiscal.DataBind();
                    ddlSuperAccumFiscal.Items.Insert(0, new ListItem("Select a Fiscal Year", "Select a Fiscal Year"));

                    // Bind Interest FReq Codes
                    ddlSuperInterestCalcFreq.DataSource = dtInterestFreq;
                    ddlSuperInterestCalcFreq.DataTextField = dtInterestFreq.Columns["Frequency"].ToString();
                    ddlSuperInterestCalcFreq.DataValueField = dtInterestFreq.Columns["FrequencyCode"].ToString();
                    ddlSuperInterestCalcFreq.DataBind();
                    ddlSuperInterestCalcFreq.Items.Insert(0, new ListItem("Select a Frequency", "Select a Frequency"));
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
                FunctionInfo.Add("Method", "PensionAndGratuities.ascx:BindDropDowns()");
                object[] objects = new object[2];
                objects[0] = AssetInstrumentCat;
                objects[1] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void DisableAllTRs()
        {
            trGraOrganisation.Visible = false;
            trGraAmount.Visible = false;
            trGraRemarks.Visible = false;

            trEPFOrganisation.Visible = false;
            trEPFAccum.Visible = false;
            trEPFEmployee.Visible = false;
            trEPFInterestDetails.Visible = false;
            trEPFInterestRate.Visible = false;
            trEPFInterestBasis.Visible = false;
            trEPFInterestAccum.Visible = false;
            trEPFCurrentVal.Visible = false;
            trEPFRemarks.Visible = false;

            trPPFAccum.Visible = false;
            trPPFYearlyContribution.Visible = false;
            trPPFInterestDetails.Visible = false;
            trPPFInterestRate.Visible = false;
            trPPFInterestBasis.Visible = false;
            trPPFInterestAccumulated.Visible = false;
            trPPFCurrentValue.Visible = false;
            trPPFRemarks.Visible = false;

            trSuperAccum.Visible = false;
            trSuperYearlyContribution.Visible = false;
            trSuperInterestDetails.Visible = false;
            trSuperInterestRate.Visible = false;
            trSuperInterestBasis.Visible = false;
            trSuperInterestAccum.Visible = false;
            trSuperCurrentValue.Visible = false;
            trSuperRemarks.Visible = false;
        }

        public void LoadAccountDetails(string xmlPath)
        {
            try
            {
                lblAccountNum.Text = customerAccountVo.AccountNum.ToString();
                if (customerAccountVo.AccountOpeningDate != DateTime.MinValue)
                    lblOpeningDate.Text = customerAccountVo.AccountOpeningDate.ToShortDateString();
                lblAccWith.Text = customerAccountVo.AccountSource.ToString();
                lblModeOfHolding.Text = XMLBo.GetModeOfHoldingName(xmlPath, customerAccountVo.ModeOfHolding.ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PensionAndGratuities.ascx:LoadAccountDetails()");
                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = xmlPath;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void LoadFields(PensionAndGratuitiesVo pensionAndGratuitiesVo, CustomerAccountsVo customerAccountVo, string action)
        {
            try
            {
                lblInstrumentCategory.Text = customerAccountVo.AssetCategoryName;

                // Enable/Disable Concerned trs of table
                if (customerAccountVo.AssetCategory.ToString().Trim() == "PGGY")
                {
                    trGraOrganisation.Visible = true;
                    trGraAmount.Visible = true;
                    trGraRemarks.Visible = true;
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGEF")
                {
                    trEPFOrganisation.Visible = true;
                    trEPFAccum.Visible = true;
                    trEPFEmployee.Visible = true;
                    trEPFInterestDetails.Visible = true;
                    trEPFInterestRate.Visible = true;
                    trEPFInterestBasis.Visible = true;
                    trEPFInterestAccum.Visible = true;
                    trEPFCurrentVal.Visible = true;
                    trEPFRemarks.Visible = true;
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGPF")
                {
                    trPPFAccum.Visible = true;
                    trPPFYearlyContribution.Visible = true;
                    trPPFInterestDetails.Visible = true;
                    trPPFInterestRate.Visible = true;
                    trPPFInterestBasis.Visible = true;
                    trPPFInterestAccumulated.Visible = true;
                    trPPFCurrentValue.Visible = true;
                    trPPFRemarks.Visible = true;
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGSN")
                {
                    trSuperAccum.Visible = true;
                    trSuperYearlyContribution.Visible = true;
                    trSuperInterestDetails.Visible = true;
                    trSuperInterestRate.Visible = true;
                    trSuperInterestBasis.Visible = true;
                    trSuperInterestAccum.Visible = true;
                    trSuperCurrentValue.Visible = true;
                    trSuperRemarks.Visible = true;
                }

                if (action == "entry")
                {
                    trEditButton.Visible = false;
                    trEditSpace.Visible = false;
                    trButton.Visible = true;
                    btnSubmit.Text = "Submit";
                    ddlEPFAccumFiscalYear.Enabled = true;
                    ddlPPFAccumFiscal.Enabled = true;
                    ddlSuperAccumFiscal.Enabled = true;

                    // Clear Control Values Here
                    if (customerAccountVo.AssetCategory.ToString().Trim() == "PGGY")
                    {
                        txtGraOrganisationName.Text = "";
                        txtGraAmount.Text = "";
                        txtGraRemarks.Text = "";
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGEF")
                    {
                        txtEPFOrganisationName.Text = "";
                        txtEPFAccum.Text = "";
                        ddlEPFAccumFiscalYear.SelectedIndex = -1;
                        txtEPFEmployeeContribution.Text = "";
                        txtEPFEmployerContribution.Text = "";
                        txtEPFInterestRate.Text = "";
                        ddlEPFInterestBasis.SelectedIndex = -1;
                        ddlEPFInterestCalFreq.SelectedIndex = -1;
                        txtEPFInterestAccum.Text = "";
                        txtEPFCurrentValue.Text = "";
                        txtEPFRemarks.Text = "";
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGPF")
                    {
                        txtPPFAccum.Text = "";
                        ddlPPFAccumFiscal.SelectedIndex = -1;
                        txtPPFYearlyContribution.Text = "";
                        txtPPFInterestRate.Text = "";
                        ddlPPFInterestBasis.SelectedIndex = -1;
                        ddlPPFInterestFrequency.SelectedIndex = -1;
                        txtPPFInterestAccumulated.Text = "";
                        txtPPFCurrentValue.Text = "";
                        txtPPFRemarks.Text = "";
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGSN")
                    {
                        txtSuperAccum.Text = "";
                        ddlSuperAccumFiscal.SelectedIndex = -1;
                        txtSuperYearlyContribution.Text = "";
                        txtSuperInterestRate.Text = "";
                        ddlSuperInterestBasis.SelectedIndex = -1;
                        ddlSuperInterestCalcFreq.SelectedIndex = -1;
                        txtSuperInterestAccum.Text = "";
                        txtSuperCurrentValue.Text = "";
                        txtSuperRemarks.Text = "";
                    }
                }
                else
                {
                    //Enable/Disable Controls Here
                    EnableDisableConrols(action, customerAccountVo.AssetCategory.ToString().Trim());

                    // Bind Values Here
                    if (customerAccountVo.AssetCategory.ToString().Trim() == "PGGY")
                    {
                        txtGraOrganisationName.Text = pensionAndGratuitiesVo.OrganizationName.ToString().Trim();
                        txtGraAmount.Text = pensionAndGratuitiesVo.DepositAmount.ToString().Trim();
                        if (pensionAndGratuitiesVo.Remarks != null)
                            txtGraRemarks.Text = pensionAndGratuitiesVo.Remarks.ToString().Trim();
                        else
                            txtGraRemarks.Text = "";
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGEF")
                    {
                        txtEPFOrganisationName.Text = pensionAndGratuitiesVo.OrganizationName.ToString().Trim();
                        txtEPFAccum.Text = pensionAndGratuitiesVo.DepositAmount.ToString();
                        ddlEPFAccumFiscalYear.SelectedValue = pensionAndGratuitiesVo.FiscalYearCode.ToString().Trim();
                        txtEPFEmployeeContribution.Text = pensionAndGratuitiesVo.EmployeeContribution.ToString();
                        txtEPFEmployerContribution.Text = pensionAndGratuitiesVo.EmployerContribution.ToString();
                        txtEPFInterestRate.Text = pensionAndGratuitiesVo.InterestRate.ToString();
                        ddlEPFInterestBasis.SelectedValue = pensionAndGratuitiesVo.InterestBasis.ToString().Trim();
                        if (ddlEPFInterestBasis.SelectedValue == "CI")
                        {
                            ddlEPFInterestCalFreq.SelectedValue = pensionAndGratuitiesVo.CompoundInterestFrequencyCode.ToString().Trim();
                        }
                        else
                        {
                            ddlEPFInterestCalFreq.SelectedValue = pensionAndGratuitiesVo.InterestPayableFrequencyCode.ToString().Trim();
                        }

                        if (ddlEPFInterestCalFreq.SelectedValue == "AM")
                        {
                            lblEPFInterestAccum.Text = "Interest Accumulated:";
                            txtEPFInterestAccum.Text = pensionAndGratuitiesVo.InterestAmtAccumalated.ToString();
                        }
                        else
                        {
                            lblEPFInterestAccum.Text = "Interest Paid Out:";
                            txtEPFInterestAccum.Text = pensionAndGratuitiesVo.InterestAmtPaidOut.ToString();
                        }

                        txtEPFCurrentValue.Text = pensionAndGratuitiesVo.CurrentValue.ToString();
                        if (pensionAndGratuitiesVo.Remarks != null)
                            txtEPFRemarks.Text = pensionAndGratuitiesVo.Remarks.ToString().Trim();
                        else
                            txtEPFRemarks.Text = "";
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGPF")
                    {
                        txtPPFAccum.Text = pensionAndGratuitiesVo.DepositAmount.ToString();
                        ddlPPFAccumFiscal.SelectedValue = pensionAndGratuitiesVo.FiscalYearCode.ToString().Trim();
                        txtPPFYearlyContribution.Text = pensionAndGratuitiesVo.EmployeeContribution.ToString();
                        txtPPFInterestRate.Text = pensionAndGratuitiesVo.InterestRate.ToString();
                        ddlPPFInterestBasis.SelectedValue = pensionAndGratuitiesVo.InterestBasis.ToString().Trim();
                        if (ddlPPFInterestBasis.SelectedValue == "CI")
                        {
                            ddlPPFInterestFrequency.SelectedValue = pensionAndGratuitiesVo.CompoundInterestFrequencyCode.ToString().Trim();
                        }
                        else
                        {
                            ddlPPFInterestFrequency.SelectedValue = pensionAndGratuitiesVo.InterestPayableFrequencyCode.ToString().Trim();
                        }

                        if (ddlPPFInterestFrequency.SelectedValue == "AM")
                        {
                            lblPPFInterestAccumulated.Text = "Interest Accumulated:";
                            txtPPFInterestAccumulated.Text = pensionAndGratuitiesVo.InterestAmtAccumalated.ToString();
                        }
                        else
                        {
                            lblPPFInterestAccumulated.Text = "Interest Paid Out:";
                            txtPPFInterestAccumulated.Text = pensionAndGratuitiesVo.InterestAmtPaidOut.ToString();
                        }

                        txtPPFCurrentValue.Text = pensionAndGratuitiesVo.CurrentValue.ToString();
                        if (pensionAndGratuitiesVo.Remarks != null)
                            txtPPFRemarks.Text = pensionAndGratuitiesVo.Remarks.ToString().Trim();
                        else
                            txtPPFRemarks.Text = "";
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGSN")
                    {
                        txtSuperAccum.Text = pensionAndGratuitiesVo.DepositAmount.ToString();
                        ddlSuperAccumFiscal.SelectedValue = pensionAndGratuitiesVo.FiscalYearCode.ToString().Trim();
                        txtSuperYearlyContribution.Text = pensionAndGratuitiesVo.EmployeeContribution.ToString();
                        txtSuperInterestRate.Text = pensionAndGratuitiesVo.InterestRate.ToString();
                        ddlSuperInterestBasis.SelectedValue = pensionAndGratuitiesVo.InterestBasis.ToString().Trim();
                        if (ddlSuperInterestBasis.SelectedValue == "CI")
                        {
                            ddlSuperInterestCalcFreq.SelectedValue = pensionAndGratuitiesVo.CompoundInterestFrequencyCode.ToString().Trim();
                        }
                        else
                        {
                            ddlSuperInterestCalcFreq.SelectedValue = pensionAndGratuitiesVo.InterestPayableFrequencyCode.ToString().Trim();
                        }

                        if (ddlSuperInterestCalcFreq.SelectedValue == "AM")
                        {
                            lblSuperInterestAccum.Text = "Interest Accumulated:";
                            txtSuperInterestAccum.Text = pensionAndGratuitiesVo.InterestAmtAccumalated.ToString();
                        }
                        else
                        {
                            lblSuperInterestAccum.Text = "Interest Paid Out:";
                            txtSuperInterestAccum.Text = pensionAndGratuitiesVo.InterestAmtPaidOut.ToString();
                        }

                        txtSuperCurrentValue.Text = pensionAndGratuitiesVo.CurrentValue.ToString();
                        if (pensionAndGratuitiesVo.Remarks != null)
                            txtSuperRemarks.Text = pensionAndGratuitiesVo.Remarks.ToString().Trim();
                        else
                            txtSuperRemarks.Text = "";
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
                FunctionInfo.Add("Method", "PensionAndGratuities.ascx:LoadFields()");
                object[] objects = new object[3];
                objects[0] = pensionAndGratuitiesVo;
                objects[1] = customerAccountVo;
                objects[2] = action;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void EnableDisableConrols(string action, string AssetCategoryCode)
        {
            try
            {
                if (action == "edit")
                {
                    trEditButton.Visible = false;
                    trEditSpace.Visible = false;
                    trButton.Visible = true;
                    btnSubmit.Text = "Update";
                }
                else if (action == "view")
                {
                    trEditButton.Visible = true;
                    trEditSpace.Visible = true;
                    trButton.Visible = false;
                    btnSubmit.Text = "";
                }

                if (AssetCategoryCode == "PGGY")
                {
                    if (action == "edit")
                    {
                        txtGraOrganisationName.Enabled = true;
                        txtGraAmount.Enabled = true;
                        txtGraRemarks.Enabled = true;
                    }
                    else if (action == "view")
                    {
                        txtGraOrganisationName.Enabled = false;
                        txtGraAmount.Enabled = false;
                        txtGraRemarks.Enabled = false;
                    }
                }
                else if (AssetCategoryCode == "PGEF")
                {
                    if (action == "edit")
                    {
                        txtEPFOrganisationName.Enabled = true;
                        txtEPFAccum.Enabled = true;
                        ddlEPFAccumFiscalYear.Enabled = true;
                        txtEPFEmployeeContribution.Enabled = true;
                        txtEPFEmployerContribution.Enabled = true;
                        txtEPFInterestRate.Enabled = true;
                        ddlEPFInterestBasis.Enabled = true;
                        ddlEPFInterestCalFreq.Enabled = true;
                        txtEPFInterestAccum.Enabled = true;
                        txtEPFCurrentValue.Enabled = true;
                        txtEPFRemarks.Enabled = true;
                    }
                    else if (action == "view")
                    {
                        txtEPFOrganisationName.Enabled = false;
                        txtEPFAccum.Enabled = false;
                        ddlEPFAccumFiscalYear.Enabled = false;
                        txtEPFEmployeeContribution.Enabled = false;
                        txtEPFEmployerContribution.Enabled = false;
                        txtEPFInterestRate.Enabled = false;
                        ddlEPFInterestBasis.Enabled = false;
                        ddlEPFInterestCalFreq.Enabled = false;
                        txtEPFInterestAccum.Enabled = false;
                        txtEPFCurrentValue.Enabled = false;
                        txtEPFRemarks.Enabled = false;
                    }
                }
                else if (AssetCategoryCode == "PGPF")
                {
                    if (action == "edit")
                    {
                        txtPPFAccum.Enabled = true;
                        ddlPPFAccumFiscal.Enabled = true;
                        txtPPFYearlyContribution.Enabled = true;
                        txtPPFInterestRate.Enabled = true;
                        ddlPPFInterestBasis.Enabled = true;
                        ddlPPFInterestFrequency.Enabled = true;
                        txtPPFInterestAccumulated.Enabled = true;
                        txtPPFCurrentValue.Enabled = true;
                        txtPPFRemarks.Enabled = true;
                    }
                    else if (action == "view")
                    {
                        txtPPFAccum.Enabled = false;
                        ddlPPFAccumFiscal.Enabled = false;
                        txtPPFYearlyContribution.Enabled = false;
                        txtPPFInterestRate.Enabled = false;
                        ddlPPFInterestBasis.Enabled = false;
                        ddlPPFInterestFrequency.Enabled = false;
                        txtPPFInterestAccumulated.Enabled = false;
                        txtPPFCurrentValue.Enabled = false;
                        txtPPFRemarks.Enabled = false;
                    }
                }
                else if (AssetCategoryCode == "PGSN")
                {
                    if (action == "edit")
                    {
                        txtSuperAccum.Enabled = true;
                        ddlSuperAccumFiscal.Enabled = true;
                        txtSuperYearlyContribution.Enabled = true;
                        txtSuperInterestRate.Enabled = true;
                        ddlSuperInterestBasis.Enabled = true;
                        ddlSuperInterestCalcFreq.Enabled = true;
                        txtSuperInterestAccum.Enabled = true;
                        txtSuperCurrentValue.Enabled = true;
                        txtSuperRemarks.Enabled = true;
                    }
                    else if (action == "view")
                    {
                        txtSuperAccum.Enabled = false;
                        ddlSuperAccumFiscal.Enabled = false;
                        txtSuperYearlyContribution.Enabled = false;
                        txtSuperInterestRate.Enabled = false;
                        ddlSuperInterestBasis.Enabled = false;
                        ddlSuperInterestCalcFreq.Enabled = false;
                        txtSuperInterestAccum.Enabled = false;
                        txtSuperCurrentValue.Enabled = false;
                        txtSuperRemarks.Enabled = false;
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
                FunctionInfo.Add("Method", "PensionAndGratuities.ascx:EnableDisableConrols()");
                object[] objects = new object[2];
                objects[0] = AssetCategoryCode;
                objects[1] = action;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PensionAndGratuitiesBo pensionAndGratuitiesBo = new PensionAndGratuitiesBo();
            PensionAndGratuitiesVo pensionAndGratuitiesVo = new PensionAndGratuitiesVo();
            try
            {
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["customerVo"];
                customerAccountVo = (CustomerAccountsVo)Session["customerAccountVo"];

                if (btnSubmit.Text == "Submit")
                {
                    // Bind Account Details Here
                    pensionAndGratuitiesVo.AccountId = customerAccountVo.AccountId;
                    pensionAndGratuitiesVo.AssetGroupCode = customerAccountVo.AssetClass.ToString().Trim();
                    pensionAndGratuitiesVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory.ToString().Trim();

                    // Bind VO Details Here
                    if (customerAccountVo.AssetCategory.ToString().Trim() == "PGGY")
                    {
                        // Fill the VO with Entered Values
                        pensionAndGratuitiesVo.OrganizationName = txtGraOrganisationName.Text.Trim();
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtGraAmount.Text.Trim());
                        pensionAndGratuitiesVo.Remarks = txtGraRemarks.Text.Trim();

                        // Fill in Default Values for the rest
                        pensionAndGratuitiesVo.FiscalYearCode = "";
                        pensionAndGratuitiesVo.EmployeeContribution = 0;
                        pensionAndGratuitiesVo.EmployerContribution = 0;
                        pensionAndGratuitiesVo.InterestRate = 0;
                        pensionAndGratuitiesVo.InterestBasis = "";
                        pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                        pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                        pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        pensionAndGratuitiesVo.CurrentValue = 0;

                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGEF")
                    {
                        pensionAndGratuitiesVo.OrganizationName = txtEPFOrganisationName.Text.Trim();
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtEPFAccum.Text.Trim());
                        pensionAndGratuitiesVo.FiscalYearCode = ddlEPFAccumFiscalYear.SelectedValue;
                        pensionAndGratuitiesVo.EmployeeContribution = float.Parse(txtEPFEmployeeContribution.Text);
                        pensionAndGratuitiesVo.EmployerContribution = float.Parse(txtEPFEmployerContribution.Text);
                        pensionAndGratuitiesVo.InterestRate = float.Parse(txtEPFInterestRate.Text);
                        pensionAndGratuitiesVo.InterestBasis = ddlEPFInterestBasis.SelectedValue;
                        if (ddlEPFInterestBasis.SelectedValue == "CI")
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = ddlEPFInterestCalFreq.SelectedValue;
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        }
                        else
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlEPFInterestCalFreq.SelectedValue;
                        }

                        if (ddlEPFInterestCalFreq.SelectedValue == "AM")
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 1;
                            pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(txtEPFInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                            pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(txtEPFInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        }

                        pensionAndGratuitiesVo.CurrentValue = float.Parse(txtEPFCurrentValue.Text);
                        pensionAndGratuitiesVo.Remarks = txtEPFRemarks.Text.Trim();
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGPF")
                    {
                        pensionAndGratuitiesVo.OrganizationName = "";
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtPPFAccum.Text.Trim());
                        pensionAndGratuitiesVo.FiscalYearCode = ddlPPFAccumFiscal.SelectedValue;
                        pensionAndGratuitiesVo.EmployeeContribution = float.Parse(txtPPFYearlyContribution.Text);
                        pensionAndGratuitiesVo.InterestRate = float.Parse(txtPPFInterestRate.Text);
                        pensionAndGratuitiesVo.InterestBasis = ddlPPFInterestBasis.SelectedValue;
                        if (ddlPPFInterestBasis.SelectedValue == "CI")
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = ddlPPFInterestFrequency.SelectedValue;
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        }
                        else
                        {
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlPPFInterestFrequency.SelectedValue;
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                        }

                        if (ddlPPFInterestFrequency.SelectedValue == "AM")
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 1;
                            pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(txtPPFInterestAccumulated.Text);
                            pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                            pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(txtPPFInterestAccumulated.Text);
                            pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        }

                        pensionAndGratuitiesVo.CurrentValue = float.Parse(txtPPFCurrentValue.Text);
                        pensionAndGratuitiesVo.Remarks = txtPPFRemarks.Text.Trim();

                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGSN")
                    {
                        pensionAndGratuitiesVo.OrganizationName = "";
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtSuperAccum.Text.Trim());
                        pensionAndGratuitiesVo.FiscalYearCode = ddlSuperAccumFiscal.SelectedValue;
                        pensionAndGratuitiesVo.EmployeeContribution = float.Parse(txtSuperYearlyContribution.Text);
                        pensionAndGratuitiesVo.InterestRate = float.Parse(txtSuperInterestRate.Text);
                        pensionAndGratuitiesVo.InterestBasis = ddlSuperInterestBasis.SelectedValue;
                        if (ddlSuperInterestBasis.SelectedValue == "CI")
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = ddlSuperInterestBasis.SelectedValue;
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        }
                        else
                        {
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlSuperInterestCalcFreq.SelectedValue;
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                        }

                        if (ddlSuperInterestCalcFreq.SelectedValue == "AM")
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 1;
                            pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(txtSuperInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                            pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(txtSuperInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        }

                        pensionAndGratuitiesVo.CurrentValue = float.Parse(txtSuperCurrentValue.Text);
                        pensionAndGratuitiesVo.Remarks = txtSuperRemarks.Text.Trim();
                    }

                    if (pensionAndGratuitiesBo.CreatePensionAndGratuities(pensionAndGratuitiesVo, userVo.UserId))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PensionPortfolio','none');", true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PensionPortfolio','none');", true);
                    }
                    else
                    {
                        // Display Error Message
                    }
                }
                else if (btnSubmit.Text == "Update")
                {
                    // Get Existing Pension Details from Session
                    pensionAndGratuitiesVo = (PensionAndGratuitiesVo)Session["pensionAndGratuitiesVo"];

                    // Bind Values Here
                    if (customerAccountVo.AssetCategory.ToString().Trim() == "PGGY")
                    {
                        // Fill the VO with Entered Values
                        pensionAndGratuitiesVo.OrganizationName = txtGraOrganisationName.Text.Trim();
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtGraAmount.Text.Trim());
                        pensionAndGratuitiesVo.Remarks = txtGraRemarks.Text.Trim();

                        // Fill in Default Values for the rest
                        pensionAndGratuitiesVo.FiscalYearCode = "";
                        pensionAndGratuitiesVo.EmployeeContribution = 0;
                        pensionAndGratuitiesVo.EmployerContribution = 0;
                        pensionAndGratuitiesVo.InterestRate = 0;
                        pensionAndGratuitiesVo.InterestBasis = "";
                        pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                        pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                        pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        pensionAndGratuitiesVo.CurrentValue = 0;
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGEF")
                    {
                        pensionAndGratuitiesVo.OrganizationName = txtEPFOrganisationName.Text.Trim();
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtEPFAccum.Text.Trim());
                        pensionAndGratuitiesVo.FiscalYearCode = ddlEPFAccumFiscalYear.SelectedValue;
                        pensionAndGratuitiesVo.EmployeeContribution = float.Parse(txtEPFEmployeeContribution.Text);
                        pensionAndGratuitiesVo.EmployerContribution = float.Parse(txtEPFEmployerContribution.Text);
                        pensionAndGratuitiesVo.InterestRate = float.Parse(txtEPFInterestRate.Text);
                        pensionAndGratuitiesVo.InterestBasis = ddlEPFInterestBasis.SelectedValue;
                        if (ddlEPFInterestBasis.SelectedValue == "CI")
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = ddlEPFInterestCalFreq.SelectedValue;
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        }
                        else
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlEPFInterestCalFreq.SelectedValue;
                        }

                        if (ddlEPFInterestCalFreq.SelectedValue == "AM")
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 1;
                            pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(txtEPFInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                            pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(txtEPFInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        }

                        pensionAndGratuitiesVo.CurrentValue = float.Parse(txtEPFCurrentValue.Text);
                        pensionAndGratuitiesVo.Remarks = txtEPFRemarks.Text.Trim();
                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGPF")
                    {
                        pensionAndGratuitiesVo.OrganizationName = "";
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtPPFAccum.Text.Trim());
                        pensionAndGratuitiesVo.FiscalYearCode = ddlPPFAccumFiscal.SelectedValue;
                        pensionAndGratuitiesVo.EmployeeContribution = float.Parse(txtPPFYearlyContribution.Text);
                        pensionAndGratuitiesVo.InterestRate = float.Parse(txtPPFInterestRate.Text);
                        pensionAndGratuitiesVo.InterestBasis = ddlPPFInterestBasis.SelectedValue;
                        if (ddlPPFInterestBasis.SelectedValue == "CI")
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = ddlPPFInterestFrequency.SelectedValue;
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        }
                        else
                        {
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlPPFInterestFrequency.SelectedValue;
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                        }

                        if (ddlPPFInterestFrequency.SelectedValue == "AM")
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 1;
                            pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(txtPPFInterestAccumulated.Text);
                            pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                            pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(txtPPFInterestAccumulated.Text);
                            pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        }

                        pensionAndGratuitiesVo.CurrentValue = float.Parse(txtPPFCurrentValue.Text);
                        pensionAndGratuitiesVo.Remarks = txtPPFRemarks.Text.Trim();

                    }
                    else if (customerAccountVo.AssetCategory.ToString().Trim() == "PGSN")
                    {
                        pensionAndGratuitiesVo.OrganizationName = "";
                        pensionAndGratuitiesVo.DepositAmount = float.Parse(txtSuperAccum.Text.Trim());
                        pensionAndGratuitiesVo.FiscalYearCode = ddlSuperAccumFiscal.SelectedValue;
                        pensionAndGratuitiesVo.EmployeeContribution = float.Parse(txtSuperYearlyContribution.Text);
                        pensionAndGratuitiesVo.InterestRate = float.Parse(txtSuperInterestRate.Text);
                        pensionAndGratuitiesVo.InterestBasis = ddlSuperInterestBasis.SelectedValue;
                        if (ddlPPFInterestBasis.SelectedValue == "CI")
                        {
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = ddlPPFInterestFrequency.SelectedValue;
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = "";
                        }
                        else
                        {
                            pensionAndGratuitiesVo.InterestPayableFrequencyCode = ddlSuperInterestCalcFreq.SelectedValue;
                            pensionAndGratuitiesVo.CompoundInterestFrequencyCode = "";
                        }

                        if (ddlPPFInterestFrequency.SelectedValue == "AM")
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 1;
                            pensionAndGratuitiesVo.InterestAmtAccumalated = float.Parse(txtSuperInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            pensionAndGratuitiesVo.IsInterestAccumalated = 0;
                            pensionAndGratuitiesVo.InterestAmtPaidOut = float.Parse(txtSuperInterestAccum.Text);
                            pensionAndGratuitiesVo.InterestAmtAccumalated = 0;
                        }

                        pensionAndGratuitiesVo.CurrentValue = float.Parse(txtSuperCurrentValue.Text);
                        pensionAndGratuitiesVo.Remarks = txtSuperRemarks.Text.Trim();
                    }

                    if (pensionAndGratuitiesBo.UpdatePensionAndGratuities(pensionAndGratuitiesVo, userVo.UserId))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PensionPortfolio','none');", true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PensionPortfolio','none');", true);
                    }
                    else
                    {
                        // Display Error Message
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
                FunctionInfo.Add("Method", "PensionAndGratuities.ascx:Button1_Click()");
                object[] objects = new object[4];
                objects[0] = customerAccountVo;
                objects[1] = userVo;
                objects[2] = customerVo;
                objects[3] = pensionAndGratuitiesVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                customerAccountVo = (CustomerAccountsVo)Session["customerAccountVo"];
                EnableDisableConrols("edit", customerAccountVo.AssetCategory.ToString().Trim());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PensionAndGratuities.ascx:lnkEdit_Click()");
                object[] objects = new object[1];
                objects[0] = customerAccountVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void ddlSuperInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSuperInterestBasis.SelectedValue == "CI")
            {
                ddlSuperInterestCalcFreq.SelectedIndex = -1;
            }
            else
            {
                ddlSuperInterestCalcFreq.SelectedIndex = -1;
            }
        }

        protected void ddlSuperInterestCalcFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSuperInterestCalcFreq.SelectedValue == "AM")
            {
                lblSuperInterestAccum.Text = "Interest Accumulated:";
            }
            else
            {
                lblSuperInterestAccum.Text = "Interest Paid Out:";
            }
        }

        protected void ddlPPFInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPPFInterestBasis.SelectedValue == "CI")
            {
                ddlPPFInterestFrequency.SelectedIndex = -1;
            }
            else if (ddlPPFInterestBasis.SelectedValue == "SI")
            {
                ddlPPFInterestFrequency.SelectedIndex = -1;
            }
        }

        protected void ddlPPFInterestFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPPFInterestFrequency.SelectedValue == "AM")
            {
                lblPPFInterestAccumulated.Text = "Interest Accumulated:";
            }
            else
            {
                lblPPFInterestAccumulated.Text = "Interest Paid Out:";
            }
        }

        protected void ddlEPFInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEPFInterestBasis.SelectedValue == "CI")
            {
                ddlEPFInterestCalFreq.SelectedIndex = -1;
            }
            else if (ddlEPFInterestBasis.SelectedValue == "SI")
            {
                ddlEPFInterestCalFreq.SelectedIndex = -1;
            }
        }
         protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
        }
        protected void ddlEPFInterestCalFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEPFInterestCalFreq.SelectedValue == "AM")
            {
                lblEPFInterestAccum.Text = "Interest Accumulated:";
            }
            else
            {
                lblEPFInterestAccum.Text = "Interest Paid Out:";
            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            if(Manage=="Edit" || Manage=="View")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PensionPortfolio', 'none')", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerAccountAdd', 'action=PG')", true);

        }
    }
}