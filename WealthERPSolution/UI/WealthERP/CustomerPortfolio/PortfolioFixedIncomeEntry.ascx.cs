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
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioFixedIncomeEntry : System.Web.UI.UserControl
    {
        DataSet ds = new DataSet();

        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        FixedIncomeVo fixedincomeVo = new FixedIncomeVo();
        CustomerAccountsVo customerAccountsVo;
        AssetBo assetBo = new AssetBo();
        FixedIncomeBo fixedincomeBo = new FixedIncomeBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        string path;
        string command;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                customerAccountsVo = new CustomerAccountsVo();
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["customerVo"];
                customerAccountsVo = (CustomerAccountsVo)Session["customerAccountVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                command = Session["action"].ToString();

                if (!IsPostBack)
                {
                    ClearFields();

                    LoadModeOfHolding();
                    LoadDebtIssuerCode(path);
                    LoadInterestBasis(path);
                    LoadFrequencyCode(path);

                    if (command == "FI")
                    {
                        lnkEdit.Visible = false;
                        lnkBtnBack.Visible = false;
                        switch (customerAccountsVo.AssetCategory.ToString())
                        {
                            case "FICB":
                                lblInstrumentCategory.Text = "Nabard Capital Gain Bonds";
                                break;
                            case "FICD":
                                lblInstrumentCategory.Text = "Company Fixed Deposits";
                                break;
                            case "FIDB":
                                lblInstrumentCategory.Text = "Deep Discount Bond";
                                break;
                            case "FIFD":
                                lblInstrumentCategory.Text = "Fixed Deposit";
                                break;
                            case "FIGS":
                                lblInstrumentCategory.Text = "FI securities G-secs";
                                break;
                            case "FIIB":
                                lblInstrumentCategory.Text = "Tax Saving Infrastructure Bonds";
                                break;
                            case "FIRD":
                                lblInstrumentCategory.Text = "Recurring Deposit";
                                break;
                            case "FISD":
                                lblInstrumentCategory.Text = "FI Securities Debentures";
                                break;
                            case "FITB":
                                lblInstrumentCategory.Text = "Tax Savings Bonds - Banks";
                                break;
                            case "FICE":
                                lblInstrumentCategory.Text = "Certificate of Deposit";
                                break;
                            case "FIML":
                                lblInstrumentCategory.Text = "Market linked";
                                break;
                            case "FIOT":
                                lblInstrumentCategory.Text = "Others";
                                break;
                            case "FIPS":
                                lblInstrumentCategory.Text = "PSU & PFI Bonds";
                                break;
                            default:
                                lblInstrumentCategory.Text = "N/A";
                                break;
                        }

                        txtAccountId.Text = customerAccountsVo.AccountNum;
                        txtAccountWith.Text = customerAccountsVo.AccountSource;
                        ddlModeOfHolding.SelectedValue = customerAccountsVo.ModeOfHolding.ToString().Trim();//XMLBo.GetModeOfHoldingName(path, customerAccountsVo.ModeOfHolding.ToString());

                        SetVisibilityCategoryWise(customerAccountsVo);
                    }
                    else
                    {
                        if (command == "ViewFI")
                        {
                            SetViewFields();
                        }
                        else if (command == "EditFI")
                        {
                            lnkEdit.Visible = false;
                            LoadEditableFields();
                        }
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
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:Page_Load()");
                object[] objects = new object[5];
                objects[0] = userVo;
                objects[1] = customerVo;
                objects[2] = customerAccountsVo;
                objects[3] = path;
                objects[4] = command;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void SetVisibilityCategoryWise(CustomerAccountsVo custAccVo)
        {
            if (custAccVo.AssetCategory.ToString().Trim() == "FIFD" || custAccVo.AssetCategory.ToString().Trim() == "FICD")
            {
                SetFDCFD();
            }
            if (custAccVo.AssetCategory.ToString().Trim() == "FITB" || custAccVo.AssetCategory.ToString().Trim() == "FIIB" || custAccVo.AssetCategory.ToString().Trim() == "FICB")
            {
                SetTaxSaving();
            }
            if (custAccVo.AssetCategory.ToString().Trim() == "FIRD")
            {
                SetFIRD();
            }
            if (custAccVo.AssetCategory.ToString().Trim() == "FISD" || custAccVo.AssetCategory.ToString().Trim() == "FIGS")
            {
                SetFISDGsec();
            }
            if (custAccVo.AssetCategory.ToString().Trim() == "FIDB")
            {
                SetDDB();
            }
            if (custAccVo.AssetCategory.ToString().Trim() == "FIML" || custAccVo.AssetCategory.ToString().Trim() == "FIOT")
            {
                SetOthers();
            }
            if (custAccVo.AssetCategory.ToString().Trim() == "FICE" || custAccVo.AssetCategory.ToString().Trim() == "FIPS")
            {
                SetMarketLink();
            }
        }

        private void SetOthers()
        {
            trDepositDetails.Visible = false;
            trDepoDateMatDate.Visible = false;
            trFaceValDebNum.Visible = false;
            trDeposit.Visible = false;
            trDepositDetailsSpace.Visible = false;

            trDDBDetails.Visible = false;
            trDDBIssuePurchaseDate.Visible = false;
            trDDBPurchPriceMatDate.Visible = false;
            trDDBFaceValue.Visible = false;
            trDDBNoofDebentures.Visible = false;
            trDDBSpace.Visible = false;

            trPurchaseDetails.Visible = false;
            trIssueDate.Visible = false;
            trPurchPrice.Visible = false;
            trFaceValue.Visible = false;
            trPurchCost.Visible = false;
            trPurchaseSpace.Visible = false;

            trDepositSchedule.Visible = false;
            trDepositAmt.Visible = false;
            trDepositScheduleSpace.Visible = false;

            trInterestDetails.Visible = true;
            trIntRateIntBasis.Visible = true;
            trIntFreqIntAmt.Visible = true;
            trIntFreqCompound.Visible = false;
            trInterestDetailsSpace.Visible = true;

            ddlInterestBasis.SelectedValue = "SI";
            ddlPayableFrequencyCode.SelectedValue = "HY";
        }
        private void SetMarketLink()
        {
            trDepositDetails.Visible = false;
            trDepoDateMatDate.Visible = false;
            trFaceValDebNum.Visible = false;
            trDeposit.Visible = false;
            trDepositDetailsSpace.Visible = false;

            trDDBDetails.Visible = true;
            trDDBIssuePurchaseDate.Visible = false;
            trDDBPurchPriceMatDate.Visible = true;
            trDDBFaceValue.Visible = true;
            trDDBNoofDebentures.Visible = true;
            trDDBSpace.Visible = true;

            trPurchaseDetails.Visible = false;
            trIssueDate.Visible = false;
            trPurchPrice.Visible = false;
            trFaceValue.Visible = false;
            trPurchCost.Visible = false;
            trPurchaseSpace.Visible = false;

            trDepositSchedule.Visible = false;
            trDepositAmt.Visible = false;
            //trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;

            trInterestDetails.Visible = true;
            trIntRateIntBasis.Visible = true;
            trIntFreqIntAmt.Visible = true;
            trIntFreqCompound.Visible = false;
            trInterestDetailsSpace.Visible = true;

            ddlInterestBasis.SelectedValue = "SI";
            ddlPayableFrequencyCode.SelectedValue = "HY";
        }

        private void SetViewFields()
        {
            try
            {
                FixedIncomeVo fixedIncomeVo = (FixedIncomeVo)Session["fixedIncomeVo"];
                CustomerAccountsVo customerAccountVo = customerAccountBo.GetCustomerFixedIncomeAccount(fixedIncomeVo.AccountId);

                btnSubmit.Visible = false;
                btnSaveChanges.Visible = false;

                // Set Account Details
                switch (customerAccountVo.AssetCategory.ToString())
                {
                    case "FICB":
                        lblInstrumentCategory.Text = "Nabard Capital Gain Bonds";
                        break;
                    case "FICD":
                        lblInstrumentCategory.Text = "Company Fixed Deposits";
                        break;
                    case "FIDB":
                        lblInstrumentCategory.Text = "Deep Discount Bond";
                        break;
                    case "FIFD":
                        lblInstrumentCategory.Text = "Fixed Deposit";
                        break;
                    case "FIGS":
                        lblInstrumentCategory.Text = "FI securities G-secs";
                        break;
                    case "FIIB":
                        lblInstrumentCategory.Text = "Tax Saving Infrastructure Bonds";
                        break;
                    case "FIRD":
                        lblInstrumentCategory.Text = "Recurring Deposit";
                        break;
                    case "FISD":
                        lblInstrumentCategory.Text = "FI Securities Debentures";
                        break;
                    case "FITB":
                        lblInstrumentCategory.Text = "Tax Savings Bonds - Banks";
                        break;
                    case "FICE":
                        lblInstrumentCategory.Text = "Certificate of Deposit";
                        break;
                    case "FIML":
                        lblInstrumentCategory.Text = "Market linked";
                        break;
                    case "FIOT":
                        lblInstrumentCategory.Text = "Others";
                        break;
                    case "FIPS":
                        lblInstrumentCategory.Text = "PSU & PFI Bonds";
                        break;
                    default:
                        lblInstrumentCategory.Text = "N/A";
                        break;
                }

                SetVisibilityCategoryWise(customerAccountVo);

                txtAccountId.Text = customerAccountVo.AccountNum;
                txtAccountId.Enabled = false;

                txtAccountWith.Text = customerAccountVo.AccountSource;
                txtAccountWith.Enabled = false;
                txtAccOpenDate.Text = customerAccountVo.AccountOpeningDate.ToShortDateString();
                txtAccOpenDate.Enabled = false;

                ddlModeOfHolding.SelectedValue = customerAccountVo.ModeOfHolding.ToString().Trim();
                ddlModeOfHolding.Enabled = false;

                // Asset Issuer
                lblAssetIssuer.Visible = true;
                ddlDebtIssuerCode.Visible = true;
                LoadDebtIssuerCode(path);
                ddlDebtIssuerCode.SelectedValue = fixedIncomeVo.DebtIssuerCode.ToString().Trim();
                ddlDebtIssuerCode.Enabled = false;

                // Asset Particular ot Name
                lblName.Visible = true;
                txtAssetParticulars.Visible = true;
                txtAssetParticulars.Text = fixedIncomeVo.Name.ToString();
                txtAssetParticulars.Enabled = false;

                // Setting Visibility of Account Source
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FITB" || customerAccountVo.AssetCategory.ToString().Trim() == "FIRD" || customerAccountVo.AssetCategory.ToString().Trim() == "FICB")
                {
                    trAccountSource.Visible = true;
                    //txtAccountWith.Visible = false;
                }
                else
                {
                    trAccountSource.Visible = false;
                }

                if (customerAccountVo.AssetCategory.ToString().Trim() == "FICD" || customerAccountVo.AssetCategory.ToString().Trim() == "FIFD")
                {   // Company Fixed Deposits and Fixed Deposit

                    txtDepositDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDepositDate.Enabled = false;
                    txtMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtMaturityDate.Enabled = false;
                    txtDepositAmount.Text = fixedIncomeVo.PrinciaplAmount.ToString();
                    txtDepositAmount.Enabled = false;
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = false;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = false;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = false;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = false;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FIIB" || customerAccountVo.AssetCategory.ToString().Trim() == "FITB" || customerAccountVo.AssetCategory.ToString().Trim() == "FICB")
                {   // Tax Saving Infrastructure Bonds and Tax Savings Bonds - Banks

                    txtDepositDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDepositDate.Enabled = false;
                    txtMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtMaturityDate.Enabled = false;
                    txtFaceValue.Text = fixedIncomeVo.FaceValue.ToString();
                    txtFaceValue.Enabled = false;
                    txtNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtNoofDebentures.Enabled = false;
                    txtDepositAmount.Text = fixedIncomeVo.PrinciaplAmount.ToString();
                    txtDepositAmount.Enabled = false;
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = false;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = false;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = false;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = false;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FISD" || customerAccountVo.AssetCategory.ToString().Trim() == "FIGS")
                {   // FI securities Debentures and FI securities G-secs

                    txtIssueDate.Text = fixedIncomeVo.IssueDate.ToShortDateString();
                    txtIssueDate.Enabled = false;
                    txtPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtPurchaseDate.Enabled = false;
                    //if (txtIssueDate.Text.Trim() == txtPurchaseDate.Text.Trim())
                    //{
                    trPurchPrice.Visible = true;
                    txtPurPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtPurPurchasePrice.Enabled = false;
                    //}
                    //else
                    //{
                    //    trPurchPrice.Visible = false;
                    //}
                    txtPMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtPMaturityDate.Enabled = false;
                    txtPurFaceValue.Text = fixedIncomeVo.FaceValue.ToString();
                    txtPurFaceValue.Enabled = false;
                    txtPurNoOfDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtPurNoOfDebentures.Enabled = false;
                    txtPurPurchaseCost.Text = fixedIncomeVo.PurchaseValue.ToString();
                    txtPurPurchaseCost.Enabled = false;

                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = false;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = false;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = false;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = false;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FIDB")
                {   // Deep Discount Bond

                    txtDDBIssueDate.Text = fixedIncomeVo.IssueDate.ToShortDateString();
                    txtDDBIssueDate.Enabled = false;
                    txtDDBPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDDBPurchaseDate.Enabled = false;
                    txtDDBPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtDDBPurchasePrice.Enabled = false;
                    txtDDBMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtDDBMaturityDate.Enabled = false;
                    txtDDBMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtDDBMaturityDate.Enabled = false;
                    txtDDBFaceValueIssue.Text = fixedIncomeVo.FaceValue.ToString();
                    txtDDBFaceValueIssue.Enabled = false;
                    txtDDBFaceValueMat.Text = fixedIncomeVo.MaturityFaceValue.ToString();
                    txtDDBFaceValueMat.Enabled = false;
                    txtDDBNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtDDBNoofDebentures.Enabled = false;
                    txtDDBPurchaseCost.Text = fixedIncomeVo.PurchaseValue.ToString();
                    txtDDBPurchaseCost.Enabled = false;
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FIRD")
                {

                    txtDepositDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDepositDate.Enabled = false;
                    txtMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtMaturityDate.Enabled = false;

                    txtNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtNoofDebentures.Enabled = false;
                    txtDepositAmount.Text = fixedIncomeVo.PrinciaplAmount.ToString();
                    txtDepositAmount.Enabled = false;
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = false;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = false;
                    ddlFrequencyOfDeposit.SelectedValue = fixedIncomeVo.DepositFrequencyCode.ToString().Trim();
                    ddlFrequencyOfDeposit.Enabled = false;
                    txtSubsequentDepositAmt.Text = fixedIncomeVo.SubsequentDepositAmount.ToString();
                    txtSubsequentDepositAmt.Enabled = false;
                    txtDDBIssueDate.Text = fixedIncomeVo.IssueDate.ToShortDateString();
                    txtDDBIssueDate.Enabled = false;
                    txtDDBPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDDBPurchaseDate.Enabled = false;
                    txtDDBPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtDDBPurchasePrice.Enabled = false;
                    txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                    txtInterestAmtCredited.Enabled = false;

                }
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FICE" || customerAccountVo.AssetCategory.ToString().Trim() == "FIPS")
                {
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = false;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = false;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = false;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = false;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    txtDDBPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDDBPurchaseDate.Enabled = false;
                    txtDDBPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtDDBPurchasePrice.Enabled = false;
                    txtDDBMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtDDBMaturityDate.Enabled = false;
                    txtDDBMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtDDBMaturityDate.Enabled = false;
                    txtDDBFaceValueMat.Text = fixedIncomeVo.MaturityFaceValue.ToString();
                    txtDDBFaceValueMat.Enabled = false;
                    txtDDBNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtDDBNoofDebentures.Enabled = false;
                    txtDDBPurchaseCost.Text = fixedIncomeVo.PurchaseValue.ToString();
                    txtDDBPurchaseCost.Enabled = false;
                }
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FIML" || customerAccountVo.AssetCategory.ToString().Trim() == "FIOT")
                {
                     txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = false;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = false;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = false;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = false;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }

                }
                // Valuation
                txtCurrentValue.Text = fixedIncomeVo.CurrentValue.ToString().Trim();
                txtMaturityValue.Text = fixedIncomeVo.MaturityValue.ToString().Trim();
                txtRemarks.Text = fixedIncomeVo.Remark.ToString();
                txtCurrentValue.Enabled = false;
                txtMaturityValue.Enabled = false;
                txtRemarks.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:SetViewFields()");
                object[] objects = new object[2];
                objects[0] = customerAccountsVo;
                objects[1] = fixedincomeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void LoadModeOfHolding()
        {
            try
            {
                DataTable dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:LoadModeOfHolding()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void LoadEditableFields()
        {
            FixedIncomeVo fixedIncomeVo = new FixedIncomeVo();
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            try
            {
                btnSaveChanges.Visible = true;
                btnSubmit.Visible = false;

                fixedIncomeVo = (FixedIncomeVo)Session["fixedIncomeVo"];
                customerAccountVo = customerAccountBo.GetCustomerFixedIncomeAccount(fixedIncomeVo.AccountId);

                // Set Account Details
                switch (customerAccountVo.AssetCategory.ToString())
                {
                    case "FICB":
                        lblInstrumentCategory.Text = "Nabard Capital Gain Bonds";
                        break;
                    case "FICD":
                        lblInstrumentCategory.Text = "Company Fixed Deposits";
                        break;
                    case "FIDB":
                        lblInstrumentCategory.Text = "Deep Discount Bond";
                        break;
                    case "FIFD":
                        lblInstrumentCategory.Text = "Fixed Deposit";
                        break;
                    case "FIGS":
                        lblInstrumentCategory.Text = "FI securities G-secs";
                        break;
                    case "FIIB":
                        lblInstrumentCategory.Text = "Tax Saving Infrastructure Bonds";
                        break;
                    case "FIRD":
                        lblInstrumentCategory.Text = "Recurring Deposit";
                        break;
                    case "FISD":
                        lblInstrumentCategory.Text = "FI Securities Debentures";
                        break;
                    case "FITB":
                        lblInstrumentCategory.Text = "Tax Savings Bonds - Banks";
                        break;
                    case "FICE":
                        lblInstrumentCategory.Text = "Certificate of Deposit";
                        break;
                    case "FIML":
                        lblInstrumentCategory.Text = "Market linked";
                        break;
                    case "FIOT":
                        lblInstrumentCategory.Text = "Others";
                        break;
                    case "FIPS":
                        lblInstrumentCategory.Text = "PSU & PFI Bonds";
                        break;
                    default:
                        lblInstrumentCategory.Text = "N/A";
                        break;
                }

                SetVisibilityCategoryWise(customerAccountVo);

                txtAccountId.Text = customerAccountVo.AccountNum;
                txtAccountId.Enabled = true;
                txtAccountWith.Text = customerAccountVo.AccountSource;
                txtAccountWith.Enabled = true;
                txtAccOpenDate.Text = customerAccountVo.AccountOpeningDate.ToShortDateString();
                txtAccOpenDate.Enabled = true;

                ddlModeOfHolding.SelectedValue = customerAccountVo.ModeOfHolding.ToString().Trim();
                ddlModeOfHolding.Enabled = true;

                // Asset Issuer
                LoadDebtIssuerCode(path);
                ddlDebtIssuerCode.SelectedValue = fixedIncomeVo.DebtIssuerCode.ToString().Trim();
                ddlDebtIssuerCode.Enabled = true;

                // Asset Particular ot Name
                txtAssetParticulars.Text = fixedIncomeVo.Name.ToString();
                txtAssetParticulars.Enabled = true;

                // Setting Visibility of Account Source
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FITB" || customerAccountVo.AssetCategory.ToString().Trim() == "FIRD")
                {
                    trAccountSource.Visible = true;
                    //txtAccountWith.Visible = false;
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FICB")
                {
                    trAccountSource.Visible = true;
                }
                else
                {
                    trAccountSource.Visible = false;
                }

                if (customerAccountVo.AssetCategory.ToString().Trim() == "FICD" || customerAccountVo.AssetCategory.ToString().Trim() == "FIFD")
                {   // Company Fixed Deposits and Fixed Deposit

                    txtDepositDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDepositDate.Enabled = true;
                    txtMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtMaturityDate.Enabled = true;
                    txtDepositAmount.Text = fixedIncomeVo.PrinciaplAmount.ToString();
                    txtDepositAmount.Enabled = true;
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = true;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = true;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = true;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = true;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = true;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = true;
                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FIIB" || customerAccountVo.AssetCategory.ToString().Trim() == "FITB" || customerAccountVo.AssetCategory.ToString().Trim() == "FICB")
                {   // Tax Saving Infrastructure Bonds and Tax Savings Bonds - Banks

                    txtDepositDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDepositDate.Enabled = true;
                    txtMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtMaturityDate.Enabled = true;
                    txtFaceValue.Text = fixedIncomeVo.FaceValue.ToString();
                    txtFaceValue.Enabled = true;
                    txtNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtNoofDebentures.Enabled = true;
                    txtDepositAmount.Text = fixedIncomeVo.PrinciaplAmount.ToString();
                    txtDepositAmount.Enabled = true;
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = true;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = true;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = true;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = true;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                        txtInterestAmtCredited.Visible = true;

                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FISD" || customerAccountVo.AssetCategory.ToString().Trim() == "FIGS")
                {   // FI securities Debentures and FI securities G-secs

                    txtIssueDate.Text = fixedIncomeVo.IssueDate.ToShortDateString();
                    txtIssueDate.Enabled = true;
                    txtPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtPurchaseDate.Enabled = true;
                    //if (txtIssueDate.Text.Trim() == txtPurchaseDate.Text.Trim())
                    //{
                    trPurchPrice.Visible = true;
                    txtPurPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtPurPurchasePrice.Enabled = true;
                    //}
                    //else
                    //{
                    //    trPurchPrice.Visible = true;
                    //}
                    txtPMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtPMaturityDate.Enabled = true;
                    txtPurFaceValue.Text = fixedIncomeVo.FaceValue.ToString();
                    txtPurFaceValue.Enabled = true;
                    txtPurNoOfDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtPurNoOfDebentures.Enabled = true;
                    txtPurPurchaseCost.Text = fixedIncomeVo.PurchaseValue.ToString();
                    txtPurPurchaseCost.Enabled = true;

                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = true;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = true;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = true;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = true;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FIDB")
                {   // Deep Discount Bond

                    txtDDBIssueDate.Text = fixedIncomeVo.IssueDate.ToShortDateString();
                    txtDDBIssueDate.Enabled = true;
                    txtDDBPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDDBPurchaseDate.Enabled = true;
                    txtDDBPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtDDBPurchasePrice.Enabled = true;
                    txtDDBMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtDDBMaturityDate.Enabled = true;
                    txtMaturityValue.Text = fixedIncomeVo.MaturityValue.ToString();
                    txtMaturityValue.Enabled = true;
                    txtDDBFaceValueIssue.Text = fixedIncomeVo.FaceValue.ToString();
                    txtDDBFaceValueIssue.Enabled = true;
                    txtDDBFaceValueMat.Text = fixedIncomeVo.MaturityFaceValue.ToString();
                    txtDDBFaceValueMat.Enabled = true;
                    txtDDBNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtDDBNoofDebentures.Enabled = true;
                    txtDDBPurchaseCost.Text = fixedIncomeVo.PurchaseValue.ToString();
                    txtDDBPurchaseCost.Enabled = true;
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "FIRD")
                {

                    txtDepositDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDepositDate.Enabled = true;
                    txtMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtMaturityDate.Enabled = true;
                  
                    txtNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtNoofDebentures.Enabled = true;
                    txtDepositAmount.Text = fixedIncomeVo.PrinciaplAmount.ToString();
                    txtDepositAmount.Enabled = true;
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = true;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = true;
                    ddlFrequencyOfDeposit.SelectedValue = fixedIncomeVo.DepositFrequencyCode.ToString().Trim();
                    ddlFrequencyOfDeposit.Enabled = true;
                    txtSubsequentDepositAmt.Text = fixedIncomeVo.SubsequentDepositAmount.ToString();
                    txtSubsequentDepositAmt.Enabled = true;
                    txtDDBIssueDate.Text = fixedIncomeVo.IssueDate.ToShortDateString();
                    txtDDBIssueDate.Enabled = true;
                    txtDDBPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDDBPurchaseDate.Enabled = true;
                    txtDDBPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtDDBPurchasePrice.Enabled = true;
                    txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                    txtInterestAmtCredited.Enabled = true;
                     
                    txtIssueDate.Enabled = true;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = true;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = true;

                }
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FICE" || customerAccountVo.AssetCategory.ToString().Trim() == "FIPS")
                {
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = true;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = true;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = true;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = true;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = true;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = true;
                    }
                    txtDDBPurchaseDate.Text = fixedIncomeVo.PurchaseDate.ToShortDateString();
                    txtDDBPurchaseDate.Enabled = true;
                    txtDDBPurchasePrice.Text = fixedIncomeVo.PurchasePrice.ToString();
                    txtDDBPurchasePrice.Enabled = true;
                    txtDDBMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtDDBMaturityDate.Enabled = true;
                    txtDDBMaturityDate.Text = fixedIncomeVo.MaturityDate.ToShortDateString();
                    txtDDBMaturityDate.Enabled = true;
                    txtDDBFaceValueMat.Text = fixedIncomeVo.MaturityFaceValue.ToString();
                    txtDDBFaceValueMat.Enabled = true;
                    txtDDBNoofDebentures.Text = fixedIncomeVo.DebentureNum.ToString();
                    txtDDBNoofDebentures.Enabled = true;
                    txtDDBPurchaseCost.Text = fixedIncomeVo.PurchaseValue.ToString();
                    txtDDBPurchaseCost.Enabled = true;
                }
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FIML" || customerAccountVo.AssetCategory.ToString().Trim() == "FIOT")
                {
                    txtInterstRate.Text = fixedIncomeVo.InterestRate.ToString();
                    txtInterstRate.Enabled = true;
                    ddlInterestBasis.SelectedValue = fixedIncomeVo.InterestBasisCode.ToString().Trim();
                    ddlInterestBasis.Enabled = true;
                    if (fixedIncomeVo.InterestBasisCode.ToString().Trim() == "CI")
                    {
                        ddlCompoundInterestFreq.SelectedValue = fixedIncomeVo.CompoundInterestFrequencyCode.ToString().Trim();
                        ddlCompoundInterestFreq.Enabled = true;
                        trIntFreqCompound.Visible = true;
                    }
                    ddlPayableFrequencyCode.SelectedValue = fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim();
                    ddlPayableFrequencyCode.Enabled = true;
                    if (fixedIncomeVo.InterestPayableFrequencyCode.ToString().Trim() == "AM")
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtAccumulated.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                    else
                    {
                        txtInterestAmtCredited.Text = fixedIncomeVo.InterestAmtPaidOut.ToString();
                        txtInterestAmtCredited.Enabled = false;
                    }
                }
                // Valuation
                txtCurrentValue.Text = fixedIncomeVo.CurrentValue.ToString().Trim();
                txtMaturityValue.Text = fixedIncomeVo.MaturityValue.ToString().Trim();
                txtRemarks.Text = fixedIncomeVo.Remark.ToString();
                txtCurrentValue.Enabled = true;
                txtMaturityValue.Enabled = true;
                txtRemarks.Enabled = true;
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:LoadEditableFields()");
                object[] objects = new object[2];
                objects[0] = customerAccountsVo;
                objects[1] = fixedincomeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void LoadInterestBasis(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetInterestBasis(path);
                ddlInterestBasis.DataSource = dt;
                ddlInterestBasis.DataTextField = dt.Columns["InterestBasisType"].ToString();
                ddlInterestBasis.DataValueField = dt.Columns["InterestBasisCode"].ToString();
                ddlInterestBasis.DataBind();
                ddlInterestBasis.Items.Insert(0, "Select an Interest Basis");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:LoadInterestBasis()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void LoadFrequencyCode(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetFrequency(path);

                ddlPayableFrequencyCode.DataSource = dt;
                ddlPayableFrequencyCode.DataTextField = "Frequency";
                ddlPayableFrequencyCode.DataValueField = "FrequencyCode";
                ddlPayableFrequencyCode.DataBind();
                ddlPayableFrequencyCode.Items.Insert(0, "Select a Frequency");

                ddlCompoundInterestFreq.DataSource = dt;
                ddlCompoundInterestFreq.DataTextField = "Frequency";
                ddlCompoundInterestFreq.DataValueField = "FrequencyCode";
                ddlCompoundInterestFreq.DataBind();
                ddlCompoundInterestFreq.Items.Insert(0, "Select a Frequency");

                ddlFrequencyOfDeposit.DataSource = dt;
                ddlFrequencyOfDeposit.DataTextField = "Frequency";
                ddlFrequencyOfDeposit.DataValueField = "FrequencyCode";
                ddlFrequencyOfDeposit.DataBind();
                ddlFrequencyOfDeposit.Items.Insert(0, "Select a Frequency");
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:LoadFrequencyCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void LoadDebtIssuerCode(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetDebtIssuer(path);
                ddlDebtIssuerCode.DataSource = dt;
                ddlDebtIssuerCode.DataTextField = dt.Columns["DebtIssuerName"].ToString();
                ddlDebtIssuerCode.DataValueField = dt.Columns["DebtIssuerCode"].ToString();
                ddlDebtIssuerCode.DataBind();
                ddlDebtIssuerCode.Items.Insert(0, "Select a Asset Issuer");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:LoadDebtIssuerCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool blResult = false;

            try
            {

                fixedincomeVo.Remark = txtRemarks.Text;
                UpdateFixedIncomeAccount(customerAccountsVo);

                if (customerAccountsVo.AssetCategory.ToString().Trim() == "FIFD" || customerAccountsVo.AssetCategory.ToString().Trim() == "FICD")
                {
                    fixedincomeVo.CustomerId = customerVo.CustomerId;
                    fixedincomeVo.AccountId = customerAccountsVo.AccountId;
                    fixedincomeVo.AssetGroupCode = "FI";
                    fixedincomeVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;
                    fixedincomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    fixedincomeVo.Name = txtAssetParticulars.Text;
                    fixedincomeVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text.Trim());
                    fixedincomeVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text.Trim());
                    fixedincomeVo.PrinciaplAmount = float.Parse(txtDepositAmount.Text.ToString());
                    fixedincomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    fixedincomeVo.InterestBasisCode = ddlInterestBasis.SelectedValue.ToString();
                    //fixedincomeVo.IssueDate = DateTime.MinValue;
                    if (fixedincomeVo.InterestBasisCode == "CI")
                    {
                        fixedincomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    fixedincomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedValue.ToString();
                    if (ddlPayableFrequencyCode.SelectedValue.ToString().Trim() == "AM")
                    {
                        fixedincomeVo.IsInterestAccumulated = 1;
                        fixedincomeVo.InterestAmtAccumulated = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    else
                    {
                        fixedincomeVo.IsInterestAccumulated = 0;
                        if (txtInterestAmtCredited.Text == "")
                        {
                            fixedincomeVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            fixedincomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                        }
                    }

                    if (txtCurrentValue.Text.ToString() != "")
                        fixedincomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text.ToString() != "")
                        fixedincomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    blResult = fixedincomeBo.CreateFixedIncome(fixedincomeVo, userVo.UserId);
                }
                else if (customerAccountsVo.AssetCategory.ToString().Trim() == "FITB" || customerAccountsVo.AssetCategory.ToString().Trim() == "FIIB" || customerAccountsVo.AssetCategory.ToString().Trim() == "FICB")
                {
                    fixedincomeVo.CustomerId = customerVo.CustomerId;
                    fixedincomeVo.AccountId = customerAccountsVo.AccountId;
                    fixedincomeVo.AssetGroupCode = "FI";
                    fixedincomeVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;
                    fixedincomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    fixedincomeVo.Name = txtAssetParticulars.Text;
                    fixedincomeVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text.Trim());
                    fixedincomeVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text.Trim());
                    fixedincomeVo.FaceValue = float.Parse(txtFaceValue.Text.ToString());
                    fixedincomeVo.DebentureNum = int.Parse(txtNoofDebentures.Text.ToString());
                    fixedincomeVo.PrinciaplAmount = float.Parse(txtDepositAmount.Text.ToString());
                    fixedincomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    fixedincomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();
                    //fixedincomeVo.IssueDate = DateTime.MinValue;
                    if (fixedincomeVo.InterestBasisCode == "CI")
                    {
                        fixedincomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    fixedincomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (ddlPayableFrequencyCode.SelectedValue.ToString().Trim() == "AM")
                    {
                        fixedincomeVo.IsInterestAccumulated = 1;
                        fixedincomeVo.InterestAmtAccumulated = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    else
                    {
                        fixedincomeVo.IsInterestAccumulated = 0;
                        if (txtInterestAmtCredited.Text == "")
                        {
                            fixedincomeVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            fixedincomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                        }
                    }
                    if (txtCurrentValue.Text.ToString() != "")
                        fixedincomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text.ToString() != "")
                        fixedincomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    blResult = fixedincomeBo.CreateFixedIncome(fixedincomeVo, userVo.UserId);
                }
                else if (customerAccountsVo.AssetCategory.ToString().Trim() == "FIRD")
                {
                    fixedincomeVo.CustomerId = customerVo.CustomerId;
                    fixedincomeVo.AccountId = customerAccountsVo.AccountId;
                    fixedincomeVo.AssetGroupCode = "FI";
                    fixedincomeVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;
                    fixedincomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    fixedincomeVo.Name = txtAssetParticulars.Text;
                    fixedincomeVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text.Trim());
                    fixedincomeVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text.Trim());
                    fixedincomeVo.PrinciaplAmount = float.Parse(txtDepositAmount.Text.ToString());
                    fixedincomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    fixedincomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();
                  
                    if (fixedincomeVo.InterestBasisCode == "CI")
                    {
                        fixedincomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    fixedincomeVo.DepositFrequencyCode = ddlFrequencyOfDeposit.SelectedItem.Value.ToString();
                    fixedincomeVo.SubsequentDepositAmount = float.Parse(txtSubsequentDepositAmt.Text.ToString());
                    fixedincomeVo.IssueDate = DateTime.Parse(txtDDBIssueDate.Text.Trim());
                    fixedincomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (ddlPayableFrequencyCode.SelectedValue.ToString().Trim() == "AM")
                    {
                        fixedincomeVo.IsInterestAccumulated = 1;
                        if(txtInterestAmtCredited.Text != "")
                            fixedincomeVo.InterestAmtAccumulated = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    else
                    {
                        fixedincomeVo.IsInterestAccumulated = 0;
                        if (txtInterestAmtCredited.Text != "")
                        {
                            fixedincomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                        }
                    }
                    if (txtCurrentValue.Text.ToString() != "")
                        fixedincomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text.ToString() != "")
                        fixedincomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    blResult = fixedincomeBo.CreateFixedIncome(fixedincomeVo, userVo.UserId);
                }
                else if (customerAccountsVo.AssetCategory.ToString().Trim() == "FISD" || customerAccountsVo.AssetCategory.ToString().Trim() == "FIGS")
                {
                    fixedincomeVo.CustomerId = customerVo.CustomerId;
                    fixedincomeVo.AccountId = customerAccountsVo.AccountId;
                    fixedincomeVo.AssetGroupCode = "FI";
                    fixedincomeVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;
                    fixedincomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    fixedincomeVo.Name = txtAssetParticulars.Text;
                    fixedincomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    fixedincomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();
                    if (fixedincomeVo.InterestBasisCode == "CI")
                    {
                        fixedincomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    fixedincomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (ddlPayableFrequencyCode.SelectedValue.ToString().Trim() == "AM")
                    {
                        fixedincomeVo.IsInterestAccumulated = 1;
                        fixedincomeVo.InterestAmtAccumulated = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    else
                    {
                        fixedincomeVo.IsInterestAccumulated = 0;
                        if (txtInterestAmtCredited.Text == "")
                        {
                            fixedincomeVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            fixedincomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                        }
                    }
                    fixedincomeVo.IssueDate = DateTime.Parse(txtIssueDate.Text.Trim());
                    fixedincomeVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text.Trim());
                    fixedincomeVo.MaturityDate = DateTime.Parse(txtPMaturityDate.Text.Trim());
                    fixedincomeVo.PurchasePrice = float.Parse(txtPurPurchasePrice.Text.ToString());
                    fixedincomeVo.FaceValue = float.Parse(txtPurFaceValue.Text.ToString());
                    fixedincomeVo.DebentureNum = int.Parse(txtPurNoOfDebentures.Text.ToString());
                    fixedincomeVo.PurchaseValue = float.Parse(txtPurPurchaseCost.Text.ToString());
                    if (txtCurrentValue.Text.ToString() != "")
                        fixedincomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text.ToString() != "")
                        fixedincomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    blResult = fixedincomeBo.CreateFixedIncome(fixedincomeVo, userVo.UserId);
                }
                else if (customerAccountsVo.AssetCategory.ToString().Trim() == "FIDB")
                {
                    fixedincomeVo.CustomerId = customerVo.CustomerId;
                    fixedincomeVo.AccountId = customerAccountsVo.AccountId;
                    fixedincomeVo.AssetGroupCode = "FI";
                    fixedincomeVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;
                    fixedincomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    fixedincomeVo.Name = txtAssetParticulars.Text;
                    fixedincomeVo.IssueDate = DateTime.Parse(txtDDBIssueDate.Text.Trim());
                   fixedincomeVo.MaturityDate = DateTime.Parse(txtDDBMaturityDate.Text.Trim());
                    fixedincomeVo.PurchaseDate = DateTime.Parse(txtDDBPurchaseDate.Text.Trim());
                    fixedincomeVo.FaceValue = float.Parse(txtDDBFaceValueIssue.Text.ToString());
                    fixedincomeVo.MaturityFaceValue = float.Parse(txtDDBFaceValueMat.Text.ToString());
                    fixedincomeVo.PurchasePrice = float.Parse(txtDDBPurchasePrice.Text.ToString());
                    fixedincomeVo.PurchaseValue = float.Parse(txtDDBPurchaseCost.Text.ToString());
                    fixedincomeVo.DebentureNum = int.Parse(txtDDBNoofDebentures.Text.ToString());
                    if (txtCurrentValue.Text.ToString() != "")
                        fixedincomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text.ToString() != "")
                        fixedincomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    blResult = fixedincomeBo.CreateFixedIncome(fixedincomeVo, userVo.UserId);
                }
                else if (customerAccountsVo.AssetCategory.ToString().Trim() == "FIML" || customerAccountsVo.AssetCategory.ToString().Trim() == "FIOT")
                {
                    fixedincomeVo.CustomerId = customerVo.CustomerId;
                    fixedincomeVo.AccountId = customerAccountsVo.AccountId;
                    fixedincomeVo.AssetGroupCode = "FI";
                    fixedincomeVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;
                    fixedincomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    fixedincomeVo.Name = txtAssetParticulars.Text;
                    fixedincomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    fixedincomeVo.InterestBasisCode = ddlInterestBasis.SelectedValue.ToString();
                    if (fixedincomeVo.InterestBasisCode == "CI")
                    {
                        fixedincomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    fixedincomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedValue.ToString();
                    if (ddlPayableFrequencyCode.SelectedValue.ToString().Trim() == "AM")
                    {
                        fixedincomeVo.IsInterestAccumulated = 1;
                        fixedincomeVo.InterestAmtAccumulated = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    else
                    {
                        fixedincomeVo.IsInterestAccumulated = 0;
                        if (txtInterestAmtCredited.Text == "")
                        {
                            fixedincomeVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            fixedincomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                        }
                    }
                    if (txtCurrentValue.Text.ToString() != "")
                        fixedincomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text.ToString() != "")
                        fixedincomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    blResult = fixedincomeBo.CreateFixedIncome(fixedincomeVo, userVo.UserId);
                }

                else if (customerAccountsVo.AssetCategory.ToString().Trim() == "FICE" || customerAccountsVo.AssetCategory.ToString().Trim() == "FIPS")
                {
                    fixedincomeVo.CustomerId = customerVo.CustomerId;
                    fixedincomeVo.AccountId = customerAccountsVo.AccountId;
                    fixedincomeVo.AssetGroupCode = "FI";
                    fixedincomeVo.AssetInstrumentCategoryCode = customerAccountsVo.AssetCategory;
                    fixedincomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    fixedincomeVo.Name = txtAssetParticulars.Text;
                    fixedincomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    fixedincomeVo.InterestBasisCode = ddlInterestBasis.SelectedValue.ToString();
                    if (fixedincomeVo.InterestBasisCode == "CI")
                    {
                        fixedincomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    fixedincomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedValue.ToString();
                    if (ddlPayableFrequencyCode.SelectedValue.ToString().Trim() == "AM")
                    {
                        fixedincomeVo.IsInterestAccumulated = 1;
                        fixedincomeVo.InterestAmtAccumulated = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    else
                    {
                        fixedincomeVo.IsInterestAccumulated = 0;
                        if (txtInterestAmtCredited.Text == "")
                        {
                            fixedincomeVo.InterestAmtPaidOut = 0;
                        }
                        else
                        {
                            fixedincomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                        }
                    }
                    fixedincomeVo.MaturityDate = DateTime.Parse(txtDDBMaturityDate.Text.Trim());
                    fixedincomeVo.PurchaseDate = DateTime.Parse(txtDDBPurchaseDate.Text.Trim());
                    fixedincomeVo.MaturityFaceValue = float.Parse(txtDDBFaceValueMat.Text.ToString());
                    fixedincomeVo.PurchasePrice = float.Parse(txtDDBPurchasePrice.Text.ToString());
                    fixedincomeVo.PurchaseValue = float.Parse(txtDDBPurchaseCost.Text.ToString());
                    fixedincomeVo.DebentureNum = int.Parse(txtDDBNoofDebentures.Text.ToString());
                    if (txtCurrentValue.Text.ToString() != "")
                        fixedincomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text.ToString() != "")
                        fixedincomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    blResult = fixedincomeBo.CreateFixedIncome(fixedincomeVo, userVo.UserId);
                }

                if (blResult)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PortFolioFixedIncomeView", "loadcontrol('PortfolioFixedIncomeView','none');", true);
                }
                else
                {
                    // Display Error Message
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
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:btnSubmit_Click()");
                object[] objects = new object[3];
                objects[0] = fixedincomeVo;
                objects[1] = userVo;
                objects[2] = customerAccountsVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void ClearFields()
        {
            btnSaveChanges.Visible = false;

            txtAccountId.Text = "";
            txtAccountWith.Text = "";
            ddlModeOfHolding.SelectedIndex = -1;

            trDepositDetails.Visible = false;
            trDepoDateMatDate.Visible = false;
            trFaceValDebNum.Visible = false;
            trDeposit.Visible = false;
            trDepositDetailsSpace.Visible = false;
            trDDBDetails.Visible = false;
            trDDBIssuePurchaseDate.Visible = false;
            trDDBPurchPriceMatDate.Visible = false;
            trDDBFaceValue.Visible = false;
            trDDBNoofDebentures.Visible = false;
            trDDBSpace.Visible = false;
            trPurchaseDetails.Visible = false;
            trIssueDate.Visible = false;
            trPurchPrice.Visible = false;
            trPurMaturityDate.Visible = false;
            trFaceValue.Visible = false;
            trPurchCost.Visible = false;
            trPurchaseSpace.Visible = false;
            trDepositSchedule.Visible = false;
            trDepositAmt.Visible = false;
            //trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;
            trDepositAmt.Visible = false;
            //trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;
            trInterestDetails.Visible = false;
            trIntRateIntBasis.Visible = false;
            trIntFreqIntAmt.Visible = false;
            trIntFreqCompound.Visible = false;
            trInterestDetailsSpace.Visible = false;

            txtDepositAmount.Text = "";
            txtDDBIssueDate.Text = "";
            txtDDBPurchaseDate.Text = "";
            txtDDBPurchasePrice.Text = "";
            txtDDBMaturityDate.Text = "";
            txtDDBFaceValueIssue.Text = "";
            txtDDBFaceValueMat.Text = "";
            txtDDBNoofDebentures.Text = "";
            txtDDBPurchaseCost.Text = "";
            txtIssueDate.Text = "";
            txtPurchaseDate.Text = "";
            txtPurPurchasePrice.Text = "";
            txtPMaturityDate.Text = "";
            txtPurFaceValue.Text = "";
            txtPurNoOfDebentures.Text = "";
            txtPurPurchaseCost.Text = "";
            txtSubsequentDepositAmt.Text = "";
            ddlFrequencyOfDeposit.SelectedIndex = -1;
            //txtDBScheduleDate.Text = "";
            txtInterstRate.Text = "";
            ddlInterestBasis.SelectedIndex = -1;
            ddlPayableFrequencyCode.SelectedIndex = -1;
            ddlCompoundInterestFreq.SelectedIndex = -1;
            txtInterestAmtCredited.Text = "";
            txtCurrentValue.Text = "";
            txtMaturityValue.Text = "";
            txtRemarks.Text = "";
        }

        public void SetFDCFD()
        {           
            trDepositDetails.Visible = true;
            trDepoDateMatDate.Visible = true;
            trFaceValDebNum.Visible = false;
            trDeposit.Visible = true;
            trDepositDetailsSpace.Visible = true;

            trDDBDetails.Visible = false;
            trDDBIssuePurchaseDate.Visible = false;
            trDDBPurchPriceMatDate.Visible = false;
            trDDBFaceValue.Visible = false;
            trDDBNoofDebentures.Visible = false;
            trDDBSpace.Visible = false;

            trPurchaseDetails.Visible = false;
            trIssueDate.Visible = false;
            trPurchPrice.Visible = false;
            trFaceValue.Visible = false;
            trPurchCost.Visible = false;
            trPurchaseSpace.Visible = false;

            trDepositSchedule.Visible = false;
            trDepositAmt.Visible = false;
            //trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;

            trInterestDetails.Visible = true;
            trIntRateIntBasis.Visible = true;
            trIntFreqIntAmt.Visible = true;            
            trInterestDetailsSpace.Visible = true;
            trIntFreqCompound.Visible = false;

            ddlInterestBasis.SelectedValue = "SI";
            ddlPayableFrequencyCode.SelectedValue = "HY";
        }

        public void SetTaxSaving()
        {
            trDepositDetails.Visible = true;
            trDepoDateMatDate.Visible = true;
            trFaceValDebNum.Visible = true;
            trDeposit.Visible = true;
            trDepositDetailsSpace.Visible = true;

            trDDBDetails.Visible = false;
            trDDBIssuePurchaseDate.Visible = false;
            trDDBPurchPriceMatDate.Visible = false;
            trDDBFaceValue.Visible = false;
            trDDBNoofDebentures.Visible = false;
            trDDBSpace.Visible = false;

            trPurchaseDetails.Visible = false;
            trIssueDate.Visible = false;
            trPurchPrice.Visible = false;
            trFaceValue.Visible = false;
            trPurchCost.Visible = false;
            trPurchaseSpace.Visible = false;

            trDepositSchedule.Visible = false;
            trDepositAmt.Visible = false;
           // trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;

            trDepositAmt.Visible = false;
           // trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;

            trInterestDetails.Visible = true;
            trIntRateIntBasis.Visible = true;
            trIntFreqIntAmt.Visible = true;
            trIntFreqCompound.Visible = false;
            trInterestDetailsSpace.Visible = true;

            ddlInterestBasis.SelectedValue = "SI";
        }

        public void SetFIRD()
        {
            trDepositDetails.Visible = true;
            trDepoDateMatDate.Visible = true;
            trFaceValDebNum.Visible = false;
            trDeposit.Visible = true;
            trDepositDetailsSpace.Visible = true;
            lblDepositDate.Text = "Initial Deposit Date:";
            lblDepositAmount.Text = "Initial Deposit Amount:";

            trDDBDetails.Visible = false;
            trDDBIssuePurchaseDate.Visible = false;
            trDDBPurchPriceMatDate.Visible = false;
            trDDBFaceValue.Visible = false;
            trDDBNoofDebentures.Visible = false;
            trDDBSpace.Visible = false;
            trDDBIssuePurchaseDate.Visible = true;

            trPurchaseDetails.Visible = false;
            trIssueDate.Visible = false;
            trPurchPrice.Visible = false;
            trFaceValue.Visible = false;
            trPurchCost.Visible = false;
            trPurchaseSpace.Visible = false;


            trDepositSchedule.Visible = true;
            trDepositAmt.Visible = true;
            //trDepositDate.Visible = true;
            trDepositScheduleSpace.Visible = true;

            trInterestDetails.Visible = true;
            trIntRateIntBasis.Visible = true;
            trIntFreqIntAmt.Visible = true;
            trIntFreqCompound.Visible = false;
            trInterestDetailsSpace.Visible = true;

            ddlInterestBasis.SelectedValue = "CI";
            ddlCompoundInterestFreq.SelectedValue = "QT";
            ddlPayableFrequencyCode.SelectedValue = "AM";
            lblInterestAmtCredited.Text = "Interest Amt Accumulated:";
        }

        public void SetFISDGsec()
        {
            trDepositDetails.Visible = false;
            trDepoDateMatDate.Visible = false;
            trFaceValDebNum.Visible = false;
            trDeposit.Visible = false;
            trDepositDetailsSpace.Visible = false;

            trDDBDetails.Visible = false;
            trDDBIssuePurchaseDate.Visible = false;
            trDDBPurchPriceMatDate.Visible = false;
            trDDBFaceValue.Visible = false;
            trDDBNoofDebentures.Visible = false;
            trDDBSpace.Visible = false;

            trPurchaseDetails.Visible = true;
            trIssueDate.Visible = true;
            trPurchPrice.Visible = true;
            trFaceValue.Visible = true;
            trPurchCost.Visible = true;
            trPurchaseSpace.Visible = true;
            trPurMaturityDate.Visible = true;

            trDepositSchedule.Visible = false;
            trDepositAmt.Visible = false;
            //trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;

            trInterestDetails.Visible = true;
            trIntRateIntBasis.Visible = true;
            trIntFreqIntAmt.Visible = true;
            trIntFreqCompound.Visible = false;
            trInterestDetailsSpace.Visible = true;

            ddlInterestBasis.SelectedValue = "SI";
            ddlPayableFrequencyCode.SelectedValue = "HY";
            lblInterestAmtCredited.Text = "Interest Amt Credited:";
        }

        public void SetDDB()
        {
            trDepositDetails.Visible = false;
            trDepoDateMatDate.Visible = false;
            trFaceValDebNum.Visible = false;
            trDeposit.Visible = false;
            trDepositDetailsSpace.Visible = false;

            trDDBDetails.Visible = true;
            trDDBIssuePurchaseDate.Visible = true;
            trDDBPurchPriceMatDate.Visible = true;
            trDDBFaceValue.Visible = true;
            trDDBNoofDebentures.Visible = true;
            trDDBSpace.Visible = true;

            trPurchaseDetails.Visible = false;
            trIssueDate.Visible = false;
            trPurchPrice.Visible = false;
            trFaceValue.Visible = false;
            trPurchCost.Visible = false;
            trPurchaseSpace.Visible = false;

            trDepositSchedule.Visible = false;
            trDepositAmt.Visible = false;
            //trDepositDate.Visible = false;
            trDepositScheduleSpace.Visible = false;

            trInterestDetails.Visible = false;
            trIntRateIntBasis.Visible = false;
            trIntFreqIntAmt.Visible = false;
            trIntFreqCompound.Visible = false;
            trInterestDetailsSpace.Visible = true;
        }

        protected void ddlInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInterestBasis.SelectedItem.Value == "CI")
            {
                trIntFreqCompound.Visible = true;
            }
            else
            {
                trIntFreqCompound.Visible = false;
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {

            FixedIncomeVo newFixedIncomeVo = new FixedIncomeVo();
            FixedIncomeVo fixedIncomeVo = new FixedIncomeVo();
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            try
            {
                fixedIncomeVo = (FixedIncomeVo)Session["fixedIncomeVo"];
                customerAccountVo = customerAccountBo.GetCustomerFixedIncomeAccount(fixedIncomeVo.AccountId);
                UpdateFixedIncomeAccount(customerAccountVo);
                UpdateFixedIncomePortfolio(newFixedIncomeVo, fixedIncomeVo, customerAccountVo);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('PortfolioFixedIncomeView','none');", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:btnSaveChanges_Click()");
                object[] objects = new object[3];
                objects[0] = fixedincomeVo;
                objects[1] = newFixedIncomeVo;
                objects[2] = customerAccountVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void UpdateFixedIncomeAccount(CustomerAccountsVo customerAccountVo)
        {
            CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
            try
            {
                newAccountVo.AccountId = customerAccountVo.AccountId;
                newAccountVo.AccountNum = txtAccountId.Text;
                newAccountVo.AccountSource = txtAccountWith.Text;
                if (txtAccOpenDate.Text.ToString() != "")
                    newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccOpenDate.Text.ToString());
                newAccountVo.ModeOfHolding = ddlModeOfHolding.SelectedItem.Value.ToString().Trim();
                fixedincomeBo.UpdateFixedIncomeAccount(newAccountVo, userVo.UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:UpdateFixedIncomeAccount()");
                object[] objects = new object[1];
                objects[0] = customerAccountVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void UpdateFixedIncomePortfolio(FixedIncomeVo newFixedIncomeVo, FixedIncomeVo fixedIncomeVo, CustomerAccountsVo customerAccountVo)
        {
            try
            {
                newFixedIncomeVo.FITransactionId = fixedIncomeVo.FITransactionId;
                newFixedIncomeVo.Remark = txtRemarks.Text;
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FIFD" || customerAccountVo.AssetCategory.ToString().Trim() == "FICD")
                {

                    newFixedIncomeVo.CustomerId = customerVo.CustomerId;
                    newFixedIncomeVo.AccountId = customerAccountVo.AccountId;
                    newFixedIncomeVo.AssetGroupCode = "FI";
                    newFixedIncomeVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                    newFixedIncomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    newFixedIncomeVo.Name = txtAssetParticulars.Text;
                    //   newFixedIncomeVo.PurchaseDate = DateTime.Parse(ddlPurchaseDay.SelectedItem.Value.ToString() + "/" + ddlPurchaseMonth.SelectedItem.Value.ToString() + "/" + ddlPurchaseYear.SelectedItem.Text.ToString());
                    newFixedIncomeVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text);
                    //newFixedIncomeVo.MaturityDate = DateTime.Parse(ddlMaturityDay.SelectedItem.Value.ToString() + "/" + ddlMaturityMonth.SelectedItem.Value.ToString() + "/" + ddlMaturityYear.SelectedItem.Text.ToString());
                    newFixedIncomeVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text);
                    newFixedIncomeVo.PrinciaplAmount = float.Parse(txtDepositAmount.Text.ToString());
                    newFixedIncomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    newFixedIncomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();
                    //newFixedIncomeVo.IssueDate = DateTime.Parse("01/01/1990");
                    if (newFixedIncomeVo.InterestBasisCode == "CI")
                    {
                        newFixedIncomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }

                    newFixedIncomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (txtInterestAmtCredited.Text == "")
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    newFixedIncomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    newFixedIncomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    fixedincomeBo.UpdateFixedIncomePortfolio(newFixedIncomeVo, userVo.UserId);

                }
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FITB" || customerAccountVo.AssetCategory.ToString().Trim() == "FIIB" || customerAccountVo.AssetCategory.ToString().Trim() == "FICB")
                {
                    newFixedIncomeVo.CustomerId = customerVo.CustomerId;
                    newFixedIncomeVo.AccountId = customerAccountVo.AccountId;
                    newFixedIncomeVo.AssetGroupCode = "FI";
                    newFixedIncomeVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                    newFixedIncomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    newFixedIncomeVo.Name = txtAssetParticulars.Text;
                    // newFixedIncomeVo.PurchaseDate = DateTime.Parse(ddlPurchaseDay.SelectedItem.Value.ToString() + "/" + ddlPurchaseMonth.SelectedItem.Value.ToString() + "/" + ddlPurchaseYear.SelectedItem.Value.ToString());
                    newFixedIncomeVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text);
                    //newFixedIncomeVo.MaturityDate = DateTime.Parse(ddlMaturityDay.SelectedItem.Value.ToString() + "/" + ddlMaturityMonth.SelectedItem.Value.ToString() + "/" + ddlMaturityYear.SelectedItem.Value.ToString());
                    newFixedIncomeVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text);
                    newFixedIncomeVo.FaceValue = float.Parse(txtFaceValue.Text.ToString());
                    newFixedIncomeVo.DebentureNum = int.Parse(txtNoofDebentures.Text.ToString());
                    newFixedIncomeVo.PrinciaplAmount = float.Parse(txtDepositAmount.Text.ToString());
                    newFixedIncomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    newFixedIncomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();
                    //newFixedIncomeVo.IssueDate = DateTime.Parse("01/01/1990");
                    if (newFixedIncomeVo.InterestBasisCode == "CI")
                    {
                        newFixedIncomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    newFixedIncomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (txtInterestAmtCredited.Text == "")
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    newFixedIncomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    newFixedIncomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    fixedincomeBo.UpdateFixedIncomePortfolio(newFixedIncomeVo, userVo.UserId);
                }

                if (customerAccountVo.AssetCategory.ToString().Trim() == "FIRD")
                {
                    newFixedIncomeVo.CustomerId = customerVo.CustomerId;
                    newFixedIncomeVo.AccountId = customerAccountVo.AccountId;
                    newFixedIncomeVo.AssetGroupCode = "FI";
                    newFixedIncomeVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                    newFixedIncomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    newFixedIncomeVo.Name = txtAssetParticulars.Text;
                    //newFixedIncomeVo.PurchaseDate = DateTime.Parse(ddlPurchaseDay.SelectedItem.Value.ToString() + "/" + ddlPurchaseMonth.SelectedItem.Value.ToString() + "/" + ddlPurchaseYear.SelectedItem.Text.ToString());
                    newFixedIncomeVo.PurchaseDate = DateTime.Parse(txtDepositDate.Text);
                    // newFixedIncomeVo.MaturityDate = DateTime.Parse(ddlMaturityDay.SelectedItem.Value.ToString() + "/" + ddlMaturityMonth.SelectedItem.Value.ToString() + "/" + ddlMaturityYear.SelectedItem.Text.ToString());
                    newFixedIncomeVo.MaturityDate = DateTime.Parse(txtMaturityDate.Text);
                    newFixedIncomeVo.PrinciaplAmount = float.Parse(txtDepositAmount.Text.ToString());
                    newFixedIncomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    newFixedIncomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();

                    if (newFixedIncomeVo.InterestBasisCode == "CI")
                    {
                        newFixedIncomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    newFixedIncomeVo.DepositFrequencyCode = ddlFrequencyOfDeposit.SelectedItem.Value.ToString();
                    newFixedIncomeVo.SubsequentDepositAmount = float.Parse(txtSubsequentDepositAmt.Text.ToString());
                    //  newFixedIncomeVo.IssueDate = DateTime.Parse(ddlDepositDay.SelectedItem.Value.ToString() + "/" + ddlDepositMonth.SelectedItem.Value.ToString() + "/" + ddlDepositYear.SelectedItem.Text.ToString());
                    newFixedIncomeVo.IssueDate = DateTime.Parse(txtDDBIssueDate.Text);
                    newFixedIncomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (txtInterestAmtCredited.Text == "")
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    newFixedIncomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    newFixedIncomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    fixedincomeBo.UpdateFixedIncomePortfolio(newFixedIncomeVo, userVo.UserId);
                }

                if (customerAccountVo.AssetCategory.ToString().Trim() == "FISD" || customerAccountVo.AssetCategory.ToString().Trim() == "FIGS")
                {
                    newFixedIncomeVo.CustomerId = customerVo.CustomerId;
                    newFixedIncomeVo.AccountId = customerAccountVo.AccountId;
                    newFixedIncomeVo.AssetGroupCode = "FI";
                    newFixedIncomeVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                    newFixedIncomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    newFixedIncomeVo.Name = txtAssetParticulars.Text;
                    newFixedIncomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    newFixedIncomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();

                    if (newFixedIncomeVo.InterestBasisCode == "CI")
                    {
                        newFixedIncomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    newFixedIncomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (txtInterestAmtCredited.Text == "")
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }
                    // newFixedIncomeVo.IssueDate = DateTime.Parse(ddlIssueDay.SelectedItem.Value.ToString() + "/" + ddlIssueMonth.SelectedItem.Value.ToString() + "/" + ddlIssueYear.SelectedItem.Text.ToString());
                    if(txtIssueDate.Text!="")
                    newFixedIncomeVo.IssueDate = DateTime.Parse(txtIssueDate.Text);
                    //  newFixedIncomeVo.PurchaseDate = DateTime.Parse(ddlPurPurchaseDay.SelectedItem.Value.ToString() + "/" + ddlPurPurchaseMonth.SelectedItem.Value.ToString() + "/" + ddlPurPurchaseYear.SelectedItem.Text.ToString());
                    newFixedIncomeVo.PurchaseDate = DateTime.Parse(txtPurchaseDate.Text);
                    //newFixedIncomeVo.MaturityDate = DateTime.Parse(ddlPurMaturityDay.SelectedItem.Value.ToString() + "/" + ddlPurMaturityMonth.SelectedItem.Value.ToString() + "/" + ddlPurMaturityYear.SelectedItem.Text.ToString());
                    newFixedIncomeVo.MaturityDate = DateTime.Parse(txtPMaturityDate.Text);
                    newFixedIncomeVo.PurchasePrice = float.Parse(txtPurPurchasePrice.Text.ToString());
                    newFixedIncomeVo.FaceValue = float.Parse(txtPurFaceValue.Text.ToString());
                    newFixedIncomeVo.DebentureNum = int.Parse(txtPurNoOfDebentures.Text.ToString());
                    newFixedIncomeVo.PurchaseValue = float.Parse(txtPurPurchaseCost.Text.ToString());
                    newFixedIncomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if(txtMaturityValue.Text!="")
                    newFixedIncomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    fixedincomeBo.UpdateFixedIncomePortfolio(newFixedIncomeVo, userVo.UserId);


                }

                if (customerAccountVo.AssetCategory.ToString().Trim() == "FIDB")
                {
                    newFixedIncomeVo.CustomerId = customerVo.CustomerId;
                    newFixedIncomeVo.AccountId = customerAccountVo.AccountId;
                    newFixedIncomeVo.AssetGroupCode = "FI";
                    newFixedIncomeVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                    newFixedIncomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    newFixedIncomeVo.Name = txtAssetParticulars.Text;
                    //  newFixedIncomeVo.IssueDate = DateTime.Parse(ddlDDBIssueDay.SelectedItem.Value.ToString() + "/" + ddlDDBIssueMonth.SelectedItem.Value.ToString() + "/" + ddlDDBIssueYear.SelectedItem.Text.ToString());
                    // newFixedIncomeVo.MaturityDate = DateTime.Parse(ddlDDBMaturityDay.SelectedItem.Value.ToString() + "/" + ddlDDBMaturityMonth.SelectedItem.Value.ToString() + "/" + ddlDDBMaturityYear.SelectedItem.Text.ToString());
                    //newFixedIncomeVo.PurchaseDate = DateTime.Parse(ddlDDBPurchaseDay.SelectedItem.Value.ToString() + "/" + ddlDDBPurchaseMonth.SelectedItem.Value.ToString() + "/" + ddlDDBPurchaseYear.SelectedItem.Text.ToString());
                    if (txtDDBMaturityDate.Text != "")
                    newFixedIncomeVo.MaturityDate = DateTime.Parse(txtDDBMaturityDate.Text);
                    if (txtDDBPurchaseDate.Text != "")
                    newFixedIncomeVo.PurchaseDate = DateTime.Parse(txtDDBPurchaseDate.Text);
                    if (txtDDBIssueDate.Text != "")
                    newFixedIncomeVo.IssueDate = DateTime.Parse(txtDDBIssueDate.Text);
                    if (txtDDBFaceValueIssue.Text != "")
                    newFixedIncomeVo.FaceValue = float.Parse(txtDDBFaceValueIssue.Text.ToString());
                    if (txtDDBFaceValueMat.Text != "")
                    newFixedIncomeVo.MaturityFaceValue = float.Parse(txtDDBFaceValueMat.Text.ToString());
                    if (txtDDBPurchasePrice.Text != "")
                    newFixedIncomeVo.PurchasePrice = float.Parse(txtDDBPurchasePrice.Text.ToString());
                    if (txtDDBPurchaseCost.Text != "")
                    newFixedIncomeVo.PurchaseValue = float.Parse(txtDDBPurchaseCost.Text.ToString());
                    if (txtDDBNoofDebentures.Text != "")
                    newFixedIncomeVo.DebentureNum = int.Parse(txtDDBNoofDebentures.Text.ToString());
                    if (txtCurrentValue.Text != "")
                    newFixedIncomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if(txtMaturityValue.Text!="")
                    newFixedIncomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    fixedincomeBo.UpdateFixedIncomePortfolio(newFixedIncomeVo, userVo.UserId);
                }
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FIPS" || customerAccountVo.AssetCategory.ToString().Trim() == "FICE")
                {
                    newFixedIncomeVo.CustomerId = customerVo.CustomerId;
                    newFixedIncomeVo.AccountId = customerAccountVo.AccountId;
                    newFixedIncomeVo.AssetGroupCode = "FI";
                    newFixedIncomeVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                    newFixedIncomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    newFixedIncomeVo.Name = txtAssetParticulars.Text;

                    newFixedIncomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    newFixedIncomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();

                    if (newFixedIncomeVo.InterestBasisCode == "CI")
                    {
                        newFixedIncomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    newFixedIncomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (txtInterestAmtCredited.Text == "")
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }

                    if (txtDDBMaturityDate.Text != "")
                    newFixedIncomeVo.MaturityDate = DateTime.Parse(txtDDBMaturityDate.Text);
                    if (txtDDBPurchaseDate.Text != "")
                    newFixedIncomeVo.PurchaseDate = DateTime.Parse(txtDDBPurchaseDate.Text);
                    if (txtDDBFaceValueMat.Text != "")
                    newFixedIncomeVo.MaturityFaceValue = float.Parse(txtDDBFaceValueMat.Text.ToString());
                    if (txtDDBPurchasePrice.Text != "")
                    newFixedIncomeVo.PurchasePrice = float.Parse(txtDDBPurchasePrice.Text.ToString());
                    if (txtDDBPurchaseCost.Text != "")
                    newFixedIncomeVo.PurchaseValue = float.Parse(txtDDBPurchaseCost.Text.ToString());
                    if (txtDDBNoofDebentures.Text != "")
                        newFixedIncomeVo.DebentureNum = int.Parse(txtDDBNoofDebentures.Text.ToString());

                    if (txtCurrentValue.Text != "")
                        newFixedIncomeVo.CurrentValue = float.Parse(txtCurrentValue.Text.ToString());
                    if (txtMaturityValue.Text != "")
                        newFixedIncomeVo.MaturityValue = float.Parse(txtMaturityValue.Text.ToString());

                    fixedincomeBo.UpdateFixedIncomePortfolio(newFixedIncomeVo, userVo.UserId);
                }
                if (customerAccountVo.AssetCategory.ToString().Trim() == "FIML" || customerAccountVo.AssetCategory.ToString().Trim() == "FIOT")
                {
                    newFixedIncomeVo.CustomerId = customerVo.CustomerId;
                    newFixedIncomeVo.AccountId = customerAccountVo.AccountId;
                    newFixedIncomeVo.AssetGroupCode = "FI";
                    newFixedIncomeVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                    newFixedIncomeVo.DebtIssuerCode = ddlDebtIssuerCode.SelectedItem.Value.ToString();
                    newFixedIncomeVo.Name = txtAssetParticulars.Text;

                    newFixedIncomeVo.InterestRate = float.Parse(txtInterstRate.Text.ToString());
                    newFixedIncomeVo.InterestBasisCode = ddlInterestBasis.SelectedItem.Value.ToString();

                    if (newFixedIncomeVo.InterestBasisCode == "CI")
                    {
                        newFixedIncomeVo.CompoundInterestFrequencyCode = ddlCompoundInterestFreq.SelectedItem.Value.ToString();
                    }
                    newFixedIncomeVo.InterestPayableFrequencyCode = ddlPayableFrequencyCode.SelectedItem.Value.ToString();
                    if (txtInterestAmtCredited.Text == "")
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = 0;
                    }
                    else
                    {
                        newFixedIncomeVo.InterestAmtPaidOut = int.Parse(txtInterestAmtCredited.Text.ToString());
                    }

                    fixedincomeBo.UpdateFixedIncomePortfolio(newFixedIncomeVo, userVo.UserId);
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

                FunctionInfo.Add("Method", "PortfolioFixedIncomeEntry.ascx.cs:UpdateFixedIncomePortfolio()");
                object[] objects = new object[1];
                objects[0] = customerAccountVo;
                objects[1] = newFixedIncomeVo;
                objects[2] = fixedincomeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LoadEditableFields();
        }

        protected void ddlPayableFrequencyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayableFrequencyCode.SelectedValue == "AM")
            {
                lblInterestAmtCredited.Text = "Interest Amt Accumulated:";
            }
            else
            {
                lblInterestAmtCredited.Text = "Interest Amt Credited:";
            }
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('PortfolioFixedIncomeView','none');", true);
        }

    }
}
