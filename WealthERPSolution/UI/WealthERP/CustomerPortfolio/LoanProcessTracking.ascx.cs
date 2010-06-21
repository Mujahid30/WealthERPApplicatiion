using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BoCommon;
using System.Configuration;
using BoCustomerPortfolio;
using BoCustomerProfiling;
using VoUser;
using WealthERP.Base;
using BoAdvisorProfiling;
using System.Text;
using VoCustomerPortfolio;
using System.Web.UI.HtmlControls;

namespace WealthERP.CustomerPortfolio
{
    public partial class LoanProcessTracking : System.Web.UI.UserControl
    {
        string path;
        string LoanPartner = string.Empty;
        string LoanType = string.Empty;
        string Scheme = string.Empty;
        string ClientName = string.Empty;
        string AssociateName = string.Empty;
        string tmpProofTypeName = string.Empty;

        int branchId;

        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        LiabilitiesBo liabilitiesBo;
        AdvisorVo adviserVo = new AdvisorVo();
        AdvisorStaffBo advStaffBo = new AdvisorStaffBo();
        string openImagePath = "";
        string closeImagePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session[SessionContents.UserVo];
            openImagePath = "~\\Images\\greenStatus.png";
            closeImagePath = "~\\Images\\redStatus.png";
            /***************/
            branchId = advStaffBo.GetRMBranchId(rmVo.RMId);

            if (!IsPostBack)
            {
                
                imgApplicationStatus.Src = openImagePath;
                imgEligibilityStatus2.ImageUrl = openImagePath;

                imgSanctionStatus.ImageUrl = openImagePath;
                imgDisbursalStatus.ImageUrl = openImagePath;
                imgClosureStatus.ImageUrl = openImagePath;

                imgApplicationStatus.Attributes.Add("alt", "Open");
                imgApplicationStatus.Attributes.Add("title", "Open");

                imgEligibilityStatus2.Attributes.Add("alt", "Open");
                imgEligibilityStatus2.Attributes.Add("title", "Open");

                imgSanctionStatus.Attributes.Add("alt", "Open");
                imgSanctionStatus.Attributes.Add("title", "Open");

                imgDisbursalStatus.Attributes.Add("alt", "Open");
                imgDisbursalStatus.Attributes.Add("title", "Open");


                imgClosureStatus.Attributes.Add("alt", "Open");
                imgClosureStatus.Attributes.Add("title", "Open");


                Session["CoBorrower_Table"] = null;
                ViewState["BorrowerGridDataTable"] = null;
                ViewState["CurrentTable"] = null;
                ViewState["stage"] = null;

                if (rmVo.IsExternal == 1)
                {
                    trAssociate.Visible = true;
                    lblAssociate.Text = rmVo.FirstName + " " + rmVo.MiddleName + " " + rmVo.LastName;
                }
                else
                {
                    trAssociate.Visible = false;
                }

                pnlPickAsset.Visible = false;
                pnlAssetContent.Visible = false;
                pnlAsset.Visible = false;

                if (Session[SessionContents.LoanProcessTracking] != null)
                {
                    if (Session[SessionContents.LoanProcessTracking].ToString() == "true")
                    {
                        InitializeForm("add");
                    }
                    else
                    {
                        if (Session["LoanProcessAction"] != null)
                        {
                            string action = Session["LoanProcessAction"].ToString();
                            InitializeForm(action);
                        }
                    }
                }
                else if (Session["LoanProcessAction"] != null)
                {
                    string action = Session["LoanProcessAction"].ToString();
                    InitializeForm(action);
                }

                trEligiDeclineReason.Visible = false;
                trLoanSanctionReason.Visible = false;
            }

            /* BIND THE DYNAMIC CO-BORROWER DROP DOWNS 
             * AND THE EVENT ATTACHED TO THE LAST DROP DOWN */
            if (Session["CoBorrower_Table"] != null)
            {
                if (Session["CoBorrower_Table"].ToString() != "")
                {
                    Table tbl = new Table();
                    tbl = (Table)Session["CoBorrower_Table"];
                    dvCoBorrowers.Controls.Add(tbl);

                    DropDownList ddlLastCoBorrower = new DropDownList();
                    ddlLastCoBorrower = ((DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + tbl.Rows.Count.ToString()));
                    ddlLastCoBorrower.AutoPostBack = true;
                    ddlLastCoBorrower.SelectedIndexChanged += new EventHandler(ddlLastCoBorrower_SelectedIndexChanged);
                }
            }

            if (ViewState["BorrowerGridDataTable"] != null)
            {
                if (ViewState["BorrowerGridDataTable"].ToString() != "")
                {
                    DataTable dt = (DataTable)ViewState["BorrowerGridDataTable"];
                    gvCoBorrower.DataSource = dt;
                    gvCoBorrower.DataBind();
                }

            }
            if (Session["LoanProcessAction"].ToString().ToLower() == "view")
            {
                BindDocumentsGrid();
            }
            else if (Session["LoanProcessAction"].ToString().ToLower() == "edit")
            {
                BindDocumentsGrid();
            }
            /***************/

        }

        private void InitializeForm(string action)
        {
            if (action.ToLower() == "add")
            {
                InitializeAddForm(path);
            }
            else if (action.ToLower() == "view")
            {
                InitializeViewForm(path);
            }
            else if (action.ToLower() == "edit")
            {
                InitializeEditForm(path);
            }
        }

        private void InitializeAddForm(string xmlpath)
        {
            // Bind All Drop Downs
            BindDropDowns(xmlpath);
            BindEMIDropDowns();

            if (Session[SessionContents.LoanProcessTracking] != null)
            {
                if (Session[SessionContents.LoanProcessTracking].ToString() == "true")
                {
                    BindSessionProposalDetails();
                }
            }

            lnkBtnEdit.Visible = false;
            dvDocDropDown.Visible = false;
            pnlAddDocuments.Visible = false;
            btnAddAsset.Visible = true;
            ddlExistingAssets.Enabled = true;
        }

        private void BindSessionProposalDetails()
        {
            LoanProposalVo loanProposalVo = new LoanProposalVo();
            loanProposalVo = (LoanProposalVo)Session["loanProposalVo"];

            txtApplicationNo.Text = loanProposalVo.ApplicationNum.ToString();
            txtAppliedLoanAmt.Text = loanProposalVo.AppliedLoanAmount.ToString("f2");
            txtAppliedLoanPeriod.Text = loanProposalVo.AppliedLoanPeriod.ToString();
            if (loanProposalVo.IsFloatingRate == 0)
                rbtnNo.Checked = true;
            else
                rbtnYes.Checked = true;
            txtInterestRate.Text = loanProposalVo.SanctionInterestRate.ToString("f2");
            txtIntroducer.Text = loanProposalVo.Introducer.ToString();
            if (loanProposalVo.SanctionDate != DateTime.MinValue)
                txtSanctionDate.Text = loanProposalVo.SanctionDate.ToShortDateString();
            txtBankReference.Text = loanProposalVo.BankReferenceNum.ToString();
            txtSanctionAmount.Text = loanProposalVo.SanctionAmount.ToString("f2");
            txtSanctionInterestRate.Text = loanProposalVo.SanctionInterestRate.ToString("f2");
            txtEMIAmount.Text = loanProposalVo.EMIAmount.ToString("f2");
            ddlEMIDate.SelectedValue = loanProposalVo.EMIDate.ToString();
            if (loanProposalVo.EMIFrequency != null)
                ddlEMIFrequency.SelectedValue = loanProposalVo.EMIFrequency.ToString();
            if (loanProposalVo.RepaymentType != null)
                ddlRepaymentType.SelectedValue = loanProposalVo.RepaymentType.ToString();
            if (loanProposalVo.InstallmentStartDate != DateTime.MinValue)
                txtInstallStartDate.Text = loanProposalVo.InstallmentStartDate.ToShortDateString();
            if (loanProposalVo.InstallmentEndDate != DateTime.MinValue)
                txtInstallEndDate.Text = loanProposalVo.InstallmentEndDate.ToShortDateString();
            txtRemarks.Text = loanProposalVo.Remark.ToString();

            ddlLoanPartner.SelectedValue = loanProposalVo.LoanPartnerId.ToString();
            ddlLoanType.SelectedValue = loanProposalVo.LoanTypeId.ToString();

            BindSchemesDropDown(Int32.Parse(ddlLoanType.SelectedValue), Int32.Parse(ddlLoanPartner.SelectedValue));
            ddlScheme.SelectedValue = loanProposalVo.SchemeId.ToString();

            BindAllClientsDropDown();
            ddlClientName.SelectedValue = loanProposalVo.ClientId;

            BindGuarantors();
            ddlGuarantorName.SelectedValue = loanProposalVo.GuarantorId;

            BindSchemeDetails();
            ddlInterestCat.SelectedValue = loanProposalVo.InterestCategoryId.ToString();

        }

        private void InitializeViewForm(string xmlpath)
        {
            btnSubmit.Text = "Submit";
            dvDocDropDown.Visible = true;
            btnAddAsset.Visible = false;
            ddlExistingAssets.Enabled = false;
            // Bind All Drop Downs
            BindDropDowns(xmlpath);
            BindEMIDropDowns();
            DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];
            BindProposalDetails(dsLoanProposal.Tables[0], dsLoanProposal.Tables[1], dsLoanProposal.Tables[2]);
            BindProposalStageDetails(dsLoanProposal.Tables[3], dsLoanProposal.Tables[4], dsLoanProposal.Tables[5], dsLoanProposal.Tables[6], dsLoanProposal.Tables[7], dsLoanProposal.Tables[8]);

            EnableDiableControls(false);
        }

        private void InitializeEditForm(string xmlpath)
        {
            btnSubmit.Text = "Update";
            dvDocDropDown.Visible = true;
            btnAddAsset.Visible = false;
            ddlExistingAssets.Enabled = false;
            // Bind All Drop Downs
            BindDropDowns(xmlpath);
            BindEMIDropDowns();
            DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];
            BindProposalDetails(dsLoanProposal.Tables[0], dsLoanProposal.Tables[1], dsLoanProposal.Tables[2]);
            BindProposalStageDetails(dsLoanProposal.Tables[3], dsLoanProposal.Tables[4], dsLoanProposal.Tables[5], dsLoanProposal.Tables[6], dsLoanProposal.Tables[7], dsLoanProposal.Tables[8]);

            EnableDiableControls(true);
        }

        private void EnableDiableControls(bool blEnabled)
        {
            // Hide all buttons
            btnSubmit.Visible = blEnabled;
            btnApplicationEntry.Visible = blEnabled;
            btnEligibility.Visible = blEnabled;
            btnLoanSanction.Visible = blEnabled;
            btnLoanDisbursed.Visible = blEnabled;
            btnClosure.Visible = blEnabled;
            lnkBtnEdit.Visible = !blEnabled;

            if (Session["LoanProcessAction"].ToString().ToLower() == "add")
            {
                btnSubmit.Text = "Submit";
            }
            else if (Session["LoanProcessAction"].ToString().ToLower() == "edit")
            {
                btnSubmit.Text = "Update";
            }

            int stage = 0;

            // Disable all stage controls
            if (ViewState["stage"] != null)
            {
                if (Session["LoanProcessAction"].ToString().ToLower() == "view")
                    stage = 0;
                else
                    stage = Int32.Parse(ViewState["stage"].ToString());
            }

            switch (stage)
            {
                case 1: // First Stage Open
                    // First Stage 
                    chkDocCollection.Enabled = true;
                    chkEntry.Enabled = true;
                    txtApplicationEntryRemark.Enabled = true;
                    btnApplicationEntry.Enabled = true;
                    // Second Stage
                    rbtnEligibilityApproved.Enabled = false;
                    rbtnEligibilityDeclined.Enabled = false;
                    rbtnEligibilityAdditionalInfo.Enabled = false;
                    ddlEligibilityDeclineReason.Enabled = false;
                    txtEligibilityRemark.Enabled = false;
                    btnEligibility.Enabled = false;
                    // Third Stage
                    rbtnLoanSanctionApproved.Enabled = false;
                    rbtnLoanSanctionDeclined.Enabled = false;
                    rbtnLoanSanctionAdditionalInfo.Enabled = false;
                    ddlLoanSanctionDeclineReason.Enabled = false;
                    txtLoanSanctionRemark.Enabled = false;
                    btnLoanSanction.Enabled = false;
                    // Fourth Stage
                    chkLoanDisbursed.Enabled = false;
                    txtLoanDisbursedRemark.Enabled = false;
                    btnLoanDisbursed.Enabled = false;
                    // Fifth Stage
                    chkLoanClosed.Enabled = false;
                    txtClosureRemark.Enabled = false;
                    btnClosure.Enabled = false;
                    break;
                case 2:
                    // First Stage 
                    chkDocCollection.Enabled = false;
                    chkEntry.Enabled = false;
                    txtApplicationEntryRemark.Enabled = false;
                    btnApplicationEntry.Enabled = false;
                    // Second Stage
                    rbtnEligibilityApproved.Enabled = true;
                    rbtnEligibilityDeclined.Enabled = true;
                    rbtnEligibilityAdditionalInfo.Enabled = true;
                    ddlEligibilityDeclineReason.Enabled = true;
                    txtEligibilityRemark.Enabled = true;
                    btnEligibility.Enabled = true;
                    // Third Stage
                    rbtnLoanSanctionApproved.Enabled = false;
                    rbtnLoanSanctionDeclined.Enabled = false;
                    rbtnLoanSanctionAdditionalInfo.Enabled = false;
                    ddlLoanSanctionDeclineReason.Enabled = false;
                    txtLoanSanctionRemark.Enabled = false;
                    btnLoanSanction.Enabled = false;
                    // Fourth Stage
                    chkLoanDisbursed.Enabled = false;
                    txtLoanDisbursedRemark.Enabled = false;
                    btnLoanDisbursed.Enabled = false;
                    // Fifth Stage
                    chkLoanClosed.Enabled = false;
                    txtClosureRemark.Enabled = false;
                    btnClosure.Enabled = false;
                    break;
                case 3:
                    // First Stage 
                    chkDocCollection.Enabled = false;
                    chkEntry.Enabled = false;
                    txtApplicationEntryRemark.Enabled = false;
                    btnApplicationEntry.Enabled = false;
                    // Second Stage
                    rbtnEligibilityApproved.Enabled = false;
                    rbtnEligibilityDeclined.Enabled = false;
                    rbtnEligibilityAdditionalInfo.Enabled = false;
                    ddlEligibilityDeclineReason.Enabled = false;
                    txtEligibilityRemark.Enabled = false;
                    btnEligibility.Enabled = false;
                    // Third Stage
                    rbtnLoanSanctionApproved.Enabled = true;
                    rbtnLoanSanctionDeclined.Enabled = true;
                    rbtnLoanSanctionAdditionalInfo.Enabled = true;
                    ddlLoanSanctionDeclineReason.Enabled = true;
                    txtLoanSanctionRemark.Enabled = true;
                    btnLoanSanction.Enabled = true;
                    // Fourth Stage
                    chkLoanDisbursed.Enabled = false;
                    txtLoanDisbursedRemark.Enabled = false;
                    btnLoanDisbursed.Enabled = false;
                    // Fifth Stage
                    chkLoanClosed.Enabled = false;
                    txtClosureRemark.Enabled = false;
                    btnClosure.Enabled = false;
                    break;
                case 4:
                    // First Stage 
                    chkDocCollection.Enabled = false;
                    chkEntry.Enabled = false;
                    txtApplicationEntryRemark.Enabled = false;
                    btnApplicationEntry.Enabled = false;
                    // Second Stage
                    rbtnEligibilityApproved.Enabled = false;
                    rbtnEligibilityDeclined.Enabled = false;
                    rbtnEligibilityAdditionalInfo.Enabled = false;
                    ddlEligibilityDeclineReason.Enabled = false;
                    txtEligibilityRemark.Enabled = false;
                    btnEligibility.Enabled = false;
                    // Third Stage
                    rbtnLoanSanctionApproved.Enabled = false;
                    rbtnLoanSanctionDeclined.Enabled = false;
                    rbtnLoanSanctionAdditionalInfo.Enabled = false;
                    ddlLoanSanctionDeclineReason.Enabled = false;
                    txtLoanSanctionRemark.Enabled = false;
                    btnLoanSanction.Enabled = false;
                    // Fourth Stage
                    chkLoanDisbursed.Enabled = true;
                    txtLoanDisbursedRemark.Enabled = true;
                    btnLoanDisbursed.Enabled = true;
                    // Fifth Stage
                    chkLoanClosed.Enabled = false;
                    txtClosureRemark.Enabled = false;
                    btnClosure.Enabled = false;
                    break;
                case 5:
                    // First Stage 
                    chkDocCollection.Enabled = false;
                    chkEntry.Enabled = false;
                    txtApplicationEntryRemark.Enabled = false;
                    btnApplicationEntry.Enabled = false;
                    // Second Stage
                    rbtnEligibilityApproved.Enabled = false;
                    rbtnEligibilityDeclined.Enabled = false;
                    rbtnEligibilityAdditionalInfo.Enabled = false;
                    ddlEligibilityDeclineReason.Enabled = false;
                    txtEligibilityRemark.Enabled = false;
                    btnEligibility.Enabled = false;
                    // Third Stage
                    rbtnLoanSanctionApproved.Enabled = false;
                    rbtnLoanSanctionDeclined.Enabled = false;
                    rbtnLoanSanctionAdditionalInfo.Enabled = false;
                    ddlLoanSanctionDeclineReason.Enabled = false;
                    txtLoanSanctionRemark.Enabled = false;
                    btnLoanSanction.Enabled = false;
                    // Fourth Stage
                    chkLoanDisbursed.Enabled = false;
                    txtLoanDisbursedRemark.Enabled = false;
                    btnLoanDisbursed.Enabled = false;
                    // Fifth Stage
                    chkLoanClosed.Enabled = true;
                    txtClosureRemark.Enabled = true;
                    btnClosure.Enabled = true;
                    break;
                default: // No Stage Open OR View Part
                    // First Stage 
                    chkDocCollection.Enabled = false;
                    chkEntry.Enabled = false;
                    txtApplicationEntryRemark.Enabled = false;
                    btnApplicationEntry.Enabled = false;
                    // Second Stage
                    rbtnEligibilityApproved.Enabled = false;
                    rbtnEligibilityDeclined.Enabled = false;
                    rbtnEligibilityAdditionalInfo.Enabled = false;
                    ddlEligibilityDeclineReason.Enabled = false;
                    txtEligibilityRemark.Enabled = false;
                    btnEligibility.Enabled = false;
                    // Third Stage
                    rbtnLoanSanctionApproved.Enabled = false;
                    rbtnLoanSanctionDeclined.Enabled = false;
                    rbtnLoanSanctionAdditionalInfo.Enabled = false;
                    ddlLoanSanctionDeclineReason.Enabled = false;
                    txtLoanSanctionRemark.Enabled = false;
                    btnLoanSanction.Enabled = false;
                    // Fourth Stage
                    chkLoanDisbursed.Enabled = false;
                    txtLoanDisbursedRemark.Enabled = false;
                    btnLoanDisbursed.Enabled = false;
                    // Fifth Stage
                    chkLoanClosed.Enabled = false;
                    txtClosureRemark.Enabled = false;
                    btnClosure.Enabled = false;
                    break;
            }

            // Disable all proposal controls
            ddlLoanPartner.Enabled = blEnabled;
            ddlLoanType.Enabled = blEnabled;
            ddlScheme.Enabled = blEnabled;
            ddlClientName.Enabled = blEnabled;
            chkIsMainBorrower.Enabled = blEnabled;
            txtNoOfCustomers.Enabled = blEnabled;
            btnGo.Enabled = blEnabled;
            txtIntroducer.Enabled = blEnabled;
            ddlGuarantorName.Enabled = blEnabled;
            chklstAssets.Enabled = blEnabled;

            gvCoBorrower.Enabled = blEnabled;
            txtApplicationNo.Enabled = blEnabled;
            txtAppliedLoanAmt.Enabled = blEnabled;
            txtAppliedLoanPeriod.Enabled = blEnabled;
            ddlInterestCat.Enabled = blEnabled;
            rbtnYes.Enabled = blEnabled;
            rbtnNo.Enabled = blEnabled;
            txtInterestRate.Enabled = blEnabled;
            txtMargin.Enabled = blEnabled;
            txtProcessCharges.Enabled = blEnabled;
            txtPreclosingCharges.Enabled = blEnabled;
            txtSanctionDate.Enabled = blEnabled;
            txtBankReference.Enabled = blEnabled;
            txtSanctionAmount.Enabled = blEnabled;
            txtSanctionInterestRate.Enabled = blEnabled;
            txtEMIAmount.Enabled = blEnabled;
            ddlEMIDate.Enabled = blEnabled;
            ddlRepaymentType.Enabled = blEnabled;
            ddlEMIFrequency.Enabled = blEnabled;
            txtNoOfInstallments.Enabled = blEnabled;
            txtAmountPrepaid.Enabled = blEnabled;
            txtInstallStartDate.Enabled = blEnabled;
            txtInstallEndDate.Enabled = blEnabled;
            txtRemarks.Enabled = blEnabled;

            // For Documents Stage
            btnAddDoc.Visible = blEnabled;
        }

        private void BindProposalDetails(DataTable dtLoanDetails, DataTable dtCoBorrowers, DataTable dtAssociatedAssets)
        {
            LiabilitiesBo liabilititesBo = new LiabilitiesBo();
            Double TotalIncome = 0;
            Double TotalExpense = 0;
            Double TotalNetWorth = 0;
            int loanProposalId = 0;

            if (dtLoanDetails.Rows.Count > 0)
            {
                loanProposalId = Int32.Parse(dtLoanDetails.Rows[0]["ALP_LoanProposalId"].ToString());
                txtApplicationNo.Text = dtLoanDetails.Rows[0]["ALP_ApplicationNum"].ToString();
                txtAppliedLoanAmt.Text = Double.Parse(dtLoanDetails.Rows[0]["ALP_AppliedLoanAmount"].ToString()).ToString("f2");
                txtAppliedLoanPeriod.Text = dtLoanDetails.Rows[0]["ALP_AppliedLoanPeriod"].ToString();
                if (dtLoanDetails.Rows[0]["CL_IsFloatingRateInterest"].ToString() == "0")
                    rbtnNo.Checked = true;
                else
                    rbtnYes.Checked = true;
                txtInterestRate.Text = Double.Parse(dtLoanDetails.Rows[0]["CL_RateOfInterest"].ToString()).ToString("f2");
                txtIntroducer.Text = dtLoanDetails.Rows[0]["ALP_Introducer"].ToString();
                if (dtLoanDetails.Rows[0]["ALP_SanctionDate"].ToString() != "")
                    txtSanctionDate.Text = DateTime.Parse(dtLoanDetails.Rows[0]["ALP_SanctionDate"].ToString()).ToShortDateString();

                txtBankReference.Text = dtLoanDetails.Rows[0]["ALP_BankReferenceNum"].ToString();
                txtSanctionAmount.Text = Double.Parse(dtLoanDetails.Rows[0]["CL_LoanAmount"].ToString()).ToString("f2");
                txtSanctionInterestRate.Text = float.Parse(dtLoanDetails.Rows[0]["CL_RateOfInterest"].ToString()).ToString("f2");
                if (dtLoanDetails.Rows[0]["CL_EMIAmount"].ToString() != "")
                    txtEMIAmount.Text = Double.Parse(dtLoanDetails.Rows[0]["CL_EMIAmount"].ToString()).ToString("f2");
                if (dtLoanDetails.Rows[0]["CL_EMIDate"].ToString() != "")
                    ddlEMIDate.SelectedValue = dtLoanDetails.Rows[0]["CL_EMIDate"].ToString();
                if (dtLoanDetails.Rows[0]["XF_FrequencyCodeEMI"].ToString() != "")
                    ddlEMIFrequency.SelectedValue = dtLoanDetails.Rows[0]["XF_FrequencyCodeEMI"].ToString();
                if (dtLoanDetails.Rows[0]["XRT_RepaymentTypeCode"].ToString() != "")
                    ddlRepaymentType.SelectedValue = dtLoanDetails.Rows[0]["XRT_RepaymentTypeCode"].ToString();
                if (dtLoanDetails.Rows[0]["CL_InstallmentStartDate"].ToString() != "")
                    txtInstallStartDate.Text = DateTime.Parse(dtLoanDetails.Rows[0]["CL_InstallmentStartDate"].ToString()).ToShortDateString();
                if (dtLoanDetails.Rows[0]["CL_InstallmentEndDate"].ToString() != "")
                    txtInstallEndDate.Text = DateTime.Parse(dtLoanDetails.Rows[0]["CL_InstallmentEndDate"].ToString()).ToShortDateString();
                txtNoOfInstallments.Text = dtLoanDetails.Rows[0]["CL_NoOfInstallments"].ToString();
                txtAmountPrepaid.Text = Double.Parse(dtLoanDetails.Rows[0]["CL_AmountPrepaid"].ToString()).ToString("f2");
                txtRemarks.Text = dtLoanDetails.Rows[0]["ALP_Remark"].ToString();

                ddlLoanPartner.SelectedValue = dtLoanDetails.Rows[0]["XLP_LoanPartnerCode"].ToString();
                ddlLoanType.SelectedValue = dtLoanDetails.Rows[0]["XLT_LoanTypeCode"].ToString();

                BindSchemesDropDown(Int32.Parse(ddlLoanType.SelectedValue), Int32.Parse(ddlLoanPartner.SelectedValue));
                ddlScheme.SelectedValue = dtLoanDetails.Rows[0]["ALS_LoanSchemeId"].ToString();

                BindAllClientsDropDown();
                ddlClientName.SelectedValue = dtLoanDetails.Rows[0]["C_CustomerId"].ToString();

                if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "2" || ddlLoanType.SelectedValue == "8")
                {
                    pnlAsset.Visible = true;
                    pnlAssetContent.Visible = true;
                    pnlPickAsset.Visible = false;
                    trExistingAssets.Visible = true;
                    trExistingAssetsSpace.Visible = true;
                    trExistingAssetsGrid.Visible = true;



                }
                else if (ddlLoanType.SelectedValue == "3" || ddlLoanType.SelectedValue == "4")
                {
                    pnlAsset.Visible = false;
                    pnlAssetContent.Visible = false;
                    pnlPickAsset.Visible = true;
                    trExistingAssets.Visible = false;
                    trExistingAssetsSpace.Visible = false;
                    trExistingAssetsGrid.Visible = false;
                }
                else
                {
                    pnlAsset.Visible = false;
                    pnlAssetContent.Visible = false;
                    pnlPickAsset.Visible = true;
                    trExistingAssets.Visible = false;
                    trExistingAssetsSpace.Visible = false;
                    trExistingAssetsGrid.Visible = false;
                }

                if (dtLoanDetails.Rows[0]["ALP_IsMainBorrowerMinor"].ToString() == "0")
                    chkIsMainBorrower.Checked = false;
                else
                    chkIsMainBorrower.Checked = true;

                BindSchemeDetails();

                ddlInterestCat.SelectedValue = dtLoanDetails.Rows[0]["ALSIR_LoanSchemeInterestRateId"].ToString(); ;

                /* Get these details using the scheme id returned */
                txtMargin.Text = Double.Parse(dtLoanDetails.Rows[0]["ALS_MarginMaintained"].ToString()).ToString("f2");
                txtProcessCharges.Text = Double.Parse(dtLoanDetails.Rows[0]["ALSIR_ProcessingCharges"].ToString()).ToString("f2");
                txtPreclosingCharges.Text = Double.Parse(dtLoanDetails.Rows[0]["ALSIR_PreClosingCharges"].ToString()).ToString("f2");
            }

            if (dtCoBorrowers.Rows.Count > 0)
            {
                int CB_Count = 0;
                // Bind Co-Borrower Grid
                for (int i = 1; i <= dtCoBorrowers.Rows.Count; i++)
                {
                    if (dtCoBorrowers.Rows[i - 1]["XLAT_LoanAssociateCode"].ToString() != "GR")
                    {
                        CB_Count++;
                    }
                }
                BindCoBorrowersViewGrid(CB_Count);
                Table tblCoBorr = (Table)dvCoBorrowers.FindControl("tblCoBorrowers");
                int j = 0;

                for (int i = 1; i <= dtCoBorrowers.Rows.Count; i++)
                {
                    if (dtCoBorrowers.Rows[i - 1]["XLAT_LoanAssociateCode"].ToString() != "GR")
                    {
                        j++;    // Increment the count of Co-Borrowers minus the Guarantor
                        DropDownList ddl = (DropDownList)tblCoBorr.FindControl("ddlCoBorrower" + j.ToString());
                        ddl.SelectedValue = dtCoBorrowers.Rows[i - 1]["C_AssociateCustomerId"].ToString();
                    }
                }
                txtNoOfCustomers.Text = j.ToString();

                // Get all co-borrower Ids
                List<int> custBorrowerIds = new List<int>();

                if (txtNoOfCustomers.Text.Trim() != "" && txtNoOfCustomers.Text.Trim() != "0")
                {
                    for (int i = 1; i <= Convert.ToInt32(txtNoOfCustomers.Text); i++)
                    {
                        DropDownList ddlBorrow = new DropDownList();
                        ddlBorrow = (DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + i.ToString());
                        int custId = Convert.ToInt32(ddlBorrow.SelectedValue);
                        custBorrowerIds.Add(custId);
                    }
                }

                // Bind all property/personal assets
                BindAssetsDropDown();

                DataSet dsCustAndBorrowerDetails = liabilititesBo.GetLoanCustEligibiltyCriteria(Convert.ToInt32(ddlClientName.SelectedValue), custBorrowerIds, out TotalIncome, out TotalExpense, out TotalNetWorth);

                txtAgeValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["CustAge"].ToString();
                txtResidenceValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["Residence_Stability"].ToString();
                txtJobStabValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["Job_Stability"].ToString();
                txtIncomeValue.Text = TotalIncome.ToString("f2");
                txtExpenseValue.Text = TotalExpense.ToString("f2");
                txtNetworthValue.Text = TotalNetWorth.ToString("f2");

            }

            BindGuarantors();

            foreach (DataRow drNew in dtCoBorrowers.Rows)
            {
                if (drNew["XLAT_LoanAssociateCode"].ToString() == "GR")
                {
                    ddlGuarantorName.SelectedValue = drNew["CA_AssociationId"].ToString();
                    ViewState["LiabilityAssociationId"] = drNew["CLA_LiabilitiesAssociationId"].ToString();
                }
            }

            // to the drop down for the customer and his co-borrower
            if (dtAssociatedAssets.Rows.Count > 0)
            {
                if (dtAssociatedAssets.Rows[0]["CA_AssociationId"].ToString() != "0")
                {
                    // Bind Co-Borrower Assets
                    ddlExistingAssets.SelectedValue = dtAssociatedAssets.Rows[0]["AssetId"].ToString();

                    if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "8")
                    {
                        /* Home Loan */
                        #region Home Loan

                        DataTable dt = new DataTable();
                        DataRow dr;
                        dt.Columns.Add("CLA_LiabilitiesAssociationId");
                        dt.Columns.Add("CustomerId");
                        dt.Columns.Add("AssociationId");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("AssetOwnership");
                        dt.Columns.Add("LoanObligation");
                        dt.Columns.Add("Margin");

                        dr = dt.NewRow();

                        dr[0] = dtLoanDetails.Rows[0]["CLA_LiabilitiesAssociationId"].ToString();// Liabilities Asso Id
                        dr[1] = ddlClientName.SelectedValue;// Main Client Id
                        dr[3] = ddlClientName.SelectedItem.Text;// Main Client Name
                        dr[2] = dtLoanDetails.Rows[0]["CA_AssociationId"].ToString();
                        Double MainShare = 0.0;
                        Double jointShares = 0.0;
                        for (int i = 1; i <= Convert.ToInt32(txtNoOfCustomers.Text); i++)
                        {
                            jointShares += Double.Parse(dtAssociatedAssets.Rows[i - 1]["CPAA_JointholderShare"].ToString());
                        }
                        MainShare = 100.0 - jointShares;
                        dr[4] = MainShare.ToString("f2"); // Share
                        dr[5] = dtLoanDetails.Rows[0]["CLA_LiabilitySharePer"].ToString();// Loan Obligation
                        dr[6] = dtLoanDetails.Rows[0]["CLA_MarginPer"].ToString();// Loan Margin

                        dt.Rows.Add(dr);

                        DataTable dtCoBrws = new DataTable();
                        dtCoBrws.Columns.Add("CLA_LiabilitiesAssociationId");
                        dtCoBrws.Columns.Add("CA_AssociationId");
                        dtCoBrws.Columns.Add("XLAT_LoanAssociateCode");
                        dtCoBrws.Columns.Add("CLA_LiabilitySharePer");
                        dtCoBrws.Columns.Add("CLA_MarginPer");
                        dtCoBrws.Columns.Add("CL_LiabilitiesId");
                        dtCoBrws.Columns.Add("C_AssociateCustomerId");
                        foreach (DataRow drNew in dtCoBorrowers.Rows)
                        {
                            if (drNew["XLAT_LoanAssociateCode"].ToString() != "GR")
                            {
                                dtCoBrws.ImportRow(drNew);
                            }
                        }

                        for (int i = 1; i <= Convert.ToInt32(txtNoOfCustomers.Text); i++)
                        {
                            dr = dt.NewRow();

                            DropDownList ddlBorrow = new DropDownList();
                            ddlBorrow = (DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + i.ToString());

                            dr[0] = dtCoBrws.Rows[i - 1]["CLA_LiabilitiesAssociationId"].ToString();
                            dr[1] = ddlBorrow.SelectedValue;
                            dr[3] = ddlBorrow.SelectedItem.Text;
                            dr[2] = dtCoBrws.Rows[i - 1]["CA_AssociationId"].ToString();
                            dr[5] = dtCoBrws.Rows[i - 1]["CLA_LiabilitySharePer"].ToString();
                            dr[6] = dtCoBrws.Rows[i - 1]["CLA_MarginPer"].ToString();
                            dr[4] = dtAssociatedAssets.Rows[i - 1]["CPAA_JointholderShare"].ToString();

                            dt.Rows.Add(dr);
                        }

                        ViewState["BorrowerGridDataTable"] = dt;
                        gvCoBorrower.DataSource = dt;
                        gvCoBorrower.DataBind();
                        gvCoBorrower.Visible = true;

                        #endregion
                    }
                    else if (ddlLoanType.SelectedValue == "2")
                    {
                        /* Auto Loan */

                    }
                    else if (ddlLoanType.SelectedValue == "3")
                    {
                        /* LAP */
                        foreach (DataRow drAssets in dtAssociatedAssets.Rows)
                        {
                            foreach (ListItem chklist in chklstAssets.Items)
                            {
                                if (chklist.Value == drAssets["AssetId"].ToString())
                                {
                                    chklist.Selected = true;
                                }
                            }
                        }
                    }
                    else if (ddlLoanType.SelectedValue == "4")
                    {
                        /* LAS */
                        foreach (DataRow drAssets in dtAssociatedAssets.Rows)
                        {
                            foreach (ListItem chklist in chklstAssets.Items)
                            {
                                if (chklist.Value == drAssets["AssetId"].ToString())
                                {
                                    chklist.Selected = true;
                                }
                            }
                        }
                    }
                }
            }

            // Bind Documents Customer Drop Down
            BindDocCustDropDown(loanProposalId);

        }

        private void BindDocCustDropDown(int proposalId)
        {
            liabilitiesBo = new LiabilitiesBo();

            // Get a list of all customers associated to the Loan except the Guarantor
            DataSet dsCust = liabilitiesBo.GetDocCustomerDropDown(proposalId);

            if (dsCust.Tables[0].Rows.Count > 0)
            {
                ddlDocCustomerList.DataSource = dsCust.Tables[0];
                ddlDocCustomerList.DataTextField = "CustName";
                ddlDocCustomerList.DataValueField = "CLA_LiabilitiesAssociationId";
                ddlDocCustomerList.DataBind();
                ddlDocCustomerList.Items.Insert(0, new ListItem("Select Borrower", "Select Borrower"));
            }
            else
            {
                ddlDocCustomerList.Items.Clear();
            }
        }

        private void BindProposalStageDetails(DataTable dtFirstStgDetails, DataTable dtSecondStgDetails, DataTable dtThirdStgDetails, DataTable dtFourthStgDetails, DataTable dtFifthStgDetails, DataTable dtLogDetails)
        {

            bool firstOpenStageEncountered = false;
            int stage = 0;    // To be used when EDIT link is clicked.

            if (dtFirstStgDetails.Rows.Count > 0)
            {
                if (dtFirstStgDetails.Rows[0]["ALPSA_IsComplete"].ToString() == "1")
                    chkDocCollection.Checked = true;
                else
                    chkDocCollection.Checked = false;

                if (dtFirstStgDetails.Rows[1]["ALPSA_IsComplete"].ToString() == "1")
                    chkEntry.Checked = true;
                else
                    chkEntry.Checked = false;

                if (chkDocCollection.Checked && chkEntry.Checked)
                {
                    // Set the Image of the Application Entry Stage
                    imgApplicationStatus.Src = closeImagePath;
                    imgApplicationStatus.Attributes.Add("alt", "Closed");
                    imgApplicationStatus.Attributes.Add("title", "Closed");
                    // Disable the controls of this stage
                    chkDocCollection.Enabled = false;
                    chkEntry.Enabled = false;
                    txtApplicationEntryRemark.Enabled = false;
                    btnApplicationEntry.Enabled = false;
                }
                else
                {
                    // Enable the controls of this stage
                    chkDocCollection.Enabled = true;
                    chkEntry.Enabled = true;
                    txtApplicationEntryRemark.Enabled = true;
                    btnApplicationEntry.Enabled = true;
                    firstOpenStageEncountered = true;
                    stage = 1;
                }

                txtApplicationEntryRemark.Text = dtFirstStgDetails.Rows[0]["ALPS_Remark"].ToString();

                // Store in ViewState the stage id
                ViewState["ApplEntryStageId"] = dtFirstStgDetails.Rows[0]["ALPS_ProposalStageId"].ToString();
                ViewState["ApplActivityId1"] = dtFirstStgDetails.Rows[0]["ALPSA_StageActivityId"].ToString();
                ViewState["ApplActivityId2"] = dtFirstStgDetails.Rows[1]["ALPSA_StageActivityId"].ToString();
            }

            if (dtSecondStgDetails.Rows.Count > 0)
            {
                if (dtSecondStgDetails.Rows[0]["ALPS_IsOpen"].ToString() == "0")
                {
                    // Set the image of the Eligibility Stage
                    imgEligibilityStatus2.ImageUrl = closeImagePath;
                    imgEligibilityStatus2.Attributes.Add("alt", "Closed");
                    imgEligibilityStatus2.Attributes.Add("title", "Closed");
                    // Disable the controls in this stage
                    rbtnEligibilityApproved.Enabled = false;
                    rbtnEligibilityDeclined.Enabled = false;
                    rbtnEligibilityAdditionalInfo.Enabled = false;
                    ddlEligibilityDeclineReason.Enabled = false;
                    txtEligibilityRemark.Enabled = false;
                    btnEligibility.Enabled = false;
                }
                else
                {
                    if (!firstOpenStageEncountered)
                    {
                        // Enable the controls in this stage
                        rbtnEligibilityApproved.Enabled = true;
                        rbtnEligibilityDeclined.Enabled = true;
                        rbtnEligibilityAdditionalInfo.Enabled = true;
                        ddlEligibilityDeclineReason.Enabled = true;
                        txtEligibilityRemark.Enabled = true;
                        btnEligibility.Enabled = true;

                        firstOpenStageEncountered = true;
                        stage = 2;
                    }
                    else
                    {
                        // Disable the controls in this stage
                        rbtnEligibilityApproved.Enabled = false;
                        rbtnEligibilityDeclined.Enabled = false;
                        rbtnEligibilityAdditionalInfo.Enabled = false;
                        ddlEligibilityDeclineReason.Enabled = false;
                        txtEligibilityRemark.Enabled = false;
                        btnEligibility.Enabled = false;
                    }
                }

                if (dtSecondStgDetails.Rows[0]["XD_DecisionCode"].ToString() == "AIR")
                {
                    rbtnEligibilityAdditionalInfo.Checked = true;
                    trEligiDeclineReason.Visible = false;
                }
                else if (dtSecondStgDetails.Rows[0]["XD_DecisionCode"].ToString() == "AP")
                {
                    rbtnEligibilityApproved.Checked = true;
                    trEligiDeclineReason.Visible = false;
                }
                else if (dtSecondStgDetails.Rows[0]["XD_DecisionCode"].ToString() == "DC")
                {
                    rbtnEligibilityDeclined.Checked = true;
                    trEligiDeclineReason.Visible = true;
                }

                // Update The Decline Reason Drop Down
                if (dtSecondStgDetails.Rows[0]["XDR_DeclineReasonCode"].ToString() != "")
                    ddlEligibilityDeclineReason.SelectedValue = dtSecondStgDetails.Rows[0]["XDR_DeclineReasonCode"].ToString();
                txtEligibilityRemark.Text = dtSecondStgDetails.Rows[0]["ALPS_Remark"].ToString();

                // Store in ViewState the stage id
                ViewState["EligibilityStageId"] = dtSecondStgDetails.Rows[0]["ALPS_ProposalStageId"].ToString();
            }

            if (dtThirdStgDetails.Rows.Count > 0)
            {
                if (dtThirdStgDetails.Rows[0]["ALPS_IsOpen"].ToString() == "0")
                {
                    // Set the image of the Eligibility Stage
                    imgSanctionStatus.ImageUrl = closeImagePath;
                    imgSanctionStatus.Attributes.Add("alt", "Closed");
                    imgSanctionStatus.Attributes.Add("title", "Closed");
                    // Disable the controls in this stage
                    rbtnLoanSanctionAdditionalInfo.Enabled = false;
                    rbtnLoanSanctionApproved.Enabled = false;
                    rbtnLoanSanctionDeclined.Enabled = false;
                    ddlLoanSanctionDeclineReason.Enabled = false;
                    txtLoanSanctionRemark.Enabled = false;
                    btnLoanSanction.Enabled = false;
                }
                else
                {
                    if (!firstOpenStageEncountered)
                    {
                        // Enable the controls in this stage
                        rbtnLoanSanctionAdditionalInfo.Enabled = true;
                        rbtnLoanSanctionApproved.Enabled = true;
                        rbtnLoanSanctionDeclined.Enabled = true;
                        ddlLoanSanctionDeclineReason.Enabled = true;
                        txtLoanSanctionRemark.Enabled = true;
                        btnLoanSanction.Enabled = true;

                        firstOpenStageEncountered = true;
                        stage = 3;
                    }
                    else
                    {
                        // Disable the controls in this stage
                        rbtnLoanSanctionAdditionalInfo.Enabled = false;
                        rbtnLoanSanctionApproved.Enabled = false;
                        rbtnLoanSanctionDeclined.Enabled = false;
                        ddlLoanSanctionDeclineReason.Enabled = false;
                        txtLoanSanctionRemark.Enabled = false;
                        btnLoanSanction.Enabled = false;
                    }
                }

                if (dtThirdStgDetails.Rows[0]["XD_DecisionCode"].ToString() == "AIR")
                {
                    rbtnLoanSanctionAdditionalInfo.Checked = true;
                    trLoanSanctionReason.Visible = false;
                }
                else if (dtThirdStgDetails.Rows[0]["XD_DecisionCode"].ToString() == "AP")
                {
                    rbtnLoanSanctionApproved.Checked = true;
                    trLoanSanctionReason.Visible = false;
                }
                else if (dtThirdStgDetails.Rows[0]["XD_DecisionCode"].ToString() == "DC")
                {
                    rbtnLoanSanctionDeclined.Checked = true;
                    trLoanSanctionReason.Visible = true;
                }

                // Update The Decline Reason Drop Down
                if (dtThirdStgDetails.Rows[0]["XDR_DeclineReasonCode"].ToString() != "")
                    ddlLoanSanctionDeclineReason.SelectedValue = dtSecondStgDetails.Rows[0]["XDR_DeclineReasonCode"].ToString();
                txtLoanSanctionRemark.Text = dtThirdStgDetails.Rows[0]["ALPS_Remark"].ToString();

                // Store in ViewState the stage id
                ViewState["SanctionStageId"] = dtThirdStgDetails.Rows[0]["ALPS_ProposalStageId"].ToString();
            }

            if (dtFourthStgDetails.Rows.Count > 0)
            {
                if (dtFourthStgDetails.Rows[0]["ALPS_IsOpen"].ToString() == "0")
                {
                    // Set the image of the Eligibility Stage
                    imgDisbursalStatus.ImageUrl = closeImagePath;
                    imgDisbursalStatus.Attributes.Add("alt", "Closed");
                    imgDisbursalStatus.Attributes.Add("title", "Closed");
                    chkLoanDisbursed.Checked = true;


                    // Disable the contrls in this stage
                    chkLoanDisbursed.Enabled = false;
                    txtLoanDisbursedRemark.Enabled = false;
                    btnLoanDisbursed.Enabled = false;
                }
                else
                {
                    chkLoanDisbursed.Checked = false;

                    if (!firstOpenStageEncountered)
                    {
                        // Enable the contrls in this stage
                        chkLoanDisbursed.Enabled = true;
                        txtLoanDisbursedRemark.Enabled = true;
                        btnLoanDisbursed.Enabled = true;

                        firstOpenStageEncountered = true;
                        stage = 4;
                    }
                    else
                    {
                        // Disable the contrls in this stage
                        chkLoanDisbursed.Enabled = false;
                        txtLoanDisbursedRemark.Enabled = false;
                        btnLoanDisbursed.Enabled = false;
                    }
                }
                txtLoanDisbursedRemark.Text = dtFourthStgDetails.Rows[0]["ALPS_Remark"].ToString();

                // Store in ViewState the stage id
                ViewState["DisbursalStageId"] = dtFourthStgDetails.Rows[0]["ALPS_ProposalStageId"].ToString();
            }

            if (dtFifthStgDetails.Rows.Count > 0)
            {
                if (dtFifthStgDetails.Rows[0]["ALPS_IsOpen"].ToString() == "0")
                {
                    // Set the image of the Eligibility Stage
                    imgClosureStatus.ImageUrl = closeImagePath;
                    imgClosureStatus.Attributes.Add("alt", "Closed");
                    imgClosureStatus.Attributes.Add("title", "Closed");
                    chkLoanClosed.Checked = true;

                    // Disable controls in this stage
                    chkLoanClosed.Enabled = false;
                    txtClosureRemark.Enabled = false;
                    btnClosure.Enabled = false;
                }
                else
                {
                    chkLoanClosed.Checked = false;

                    if (!firstOpenStageEncountered)
                    {
                        // Enable controls in this stage
                        chkLoanClosed.Enabled = true;
                        txtClosureRemark.Enabled = true;
                        btnClosure.Enabled = true;
                        stage = 5;
                    }
                    else
                    {
                        // Disable controls in this stage
                        chkLoanClosed.Enabled = false;
                        txtClosureRemark.Enabled = false;
                        btnClosure.Enabled = false;
                    }
                }


                txtClosureRemark.Text = dtFifthStgDetails.Rows[0]["ALPS_Remark"].ToString();

                // Store in ViewState the stage id
                ViewState["ClosureStageId"] = dtFifthStgDetails.Rows[0]["ALPS_ProposalStageId"].ToString();

            }

            ViewState["stage"] = stage; // Used for Edit Part

            if (dtLogDetails.Rows.Count > 0)
            {
                string SearchExpression = "";

                // Bind Application Entry Log
                SearchExpression = "WPS_ProposalStageId = 1";
                dtLogDetails.DefaultView.RowFilter = SearchExpression;

                if (dtLogDetails.DefaultView.Count > 0)
                {
                    gvApplEntryDecisionLog.DataSource = dtLogDetails.DefaultView;
                    gvApplEntryDecisionLog.DataBind();
                    gvApplEntryDecisionLog.Visible = true;
                }

                // Bind Eligibility Log
                SearchExpression = "WPS_ProposalStageId = 2";
                dtLogDetails.DefaultView.RowFilter = SearchExpression;

                if (dtLogDetails.DefaultView.Count > 0)
                {
                    gvEligibilityDecisionLog.DataSource = dtLogDetails.DefaultView;
                    gvEligibilityDecisionLog.DataBind();
                    gvEligibilityDecisionLog.Visible = true;
                }

                // Bind Bank Sanction Log
                SearchExpression = "WPS_ProposalStageId = 3";
                dtLogDetails.DefaultView.RowFilter = SearchExpression;

                if (dtLogDetails.DefaultView.Count > 0)
                {
                    gvLoanSanctionDecisionLog.DataSource = dtLogDetails.DefaultView;
                    gvLoanSanctionDecisionLog.DataBind();
                    gvLoanSanctionDecisionLog.Visible = true;
                }

                // Bind Loan Disbursal Log
                SearchExpression = "WPS_ProposalStageId = 4";
                dtLogDetails.DefaultView.RowFilter = SearchExpression;

                if (dtLogDetails.DefaultView.Count > 0)
                {
                    gvLoanDisbursal.DataSource = dtLogDetails.DefaultView;
                    gvLoanDisbursal.DataBind();
                    gvLoanDisbursal.Visible = true;
                }

                // Bind Closure Log
                SearchExpression = "WPS_ProposalStageId = 5";
                dtLogDetails.DefaultView.RowFilter = SearchExpression;

                if (dtLogDetails.DefaultView.Count > 0)
                {
                    gvClosure.DataSource = dtLogDetails.DefaultView;
                    gvClosure.DataBind();
                    gvClosure.Visible = true;
                }
            }
        }

        private void BindDropDowns(string xmlpath)
        {
            DataTable dtDeclineReason = new DataTable();
            DataTable dtLoanPartner = new DataTable();
            DataTable dtAssociates = new DataTable();
            DataTable dtLoanType = new DataTable();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();

            // Bind Eligibility Decline Reason Drop Down
            dtDeclineReason = XMLBo.GetDeclineReason(xmlpath);
            ddlEligibilityDeclineReason.DataSource = dtDeclineReason;
            ddlEligibilityDeclineReason.DataTextField = "XDR_DeclineReason";
            ddlEligibilityDeclineReason.DataValueField = "XDR_DeclineReasonCode";
            ddlEligibilityDeclineReason.DataBind();
            ddlEligibilityDeclineReason.Items.Insert(0, new ListItem("Select a Decline Reason", "Select a Decline Reason"));

            // Bind Bank Sanction Decline Reason Drop Down
            ddlLoanSanctionDeclineReason.DataSource = dtDeclineReason;
            ddlLoanSanctionDeclineReason.DataTextField = "XDR_DeclineReason";
            ddlLoanSanctionDeclineReason.DataValueField = "XDR_DeclineReasonCode";
            ddlLoanSanctionDeclineReason.DataBind();
            ddlLoanSanctionDeclineReason.Items.Insert(0, new ListItem("Select a Decline Reason", "Select a Decline Reason"));

            // Bind Loan Partner Drop Down
            dtLoanPartner = XMLBo.GetLoanPartner(xmlpath);
            ddlLoanPartner.DataSource = dtLoanPartner;
            ddlLoanPartner.DataTextField = "XLP_LoanPartner";
            ddlLoanPartner.DataValueField = "XLP_LoanPartnerCode";
            ddlLoanPartner.DataBind();
            ddlLoanPartner.Items.Insert(0, new ListItem("Select a Loan Partner", "Select a Loan Partner"));

            // Bind Loan Type Drop Down
            dtLoanType = XMLBo.GetLoanType(xmlpath);
            ddlLoanType.DataSource = dtLoanType;
            ddlLoanType.DataTextField = "XLT_LoanType";
            ddlLoanType.DataValueField = "XLT_LoanTypeCode";
            ddlLoanType.DataBind();
            ddlLoanType.Items.Insert(0, new ListItem("Select a Loan Type", "Select a Loan Type"));

            // BIND REPAYMENT TYPE DROP DOWN
            ddlRepaymentType.DataSource = XMLBo.GetRepaymentType(xmlpath);
            ddlRepaymentType.DataTextField = "XRT_RepaymentType";
            ddlRepaymentType.DataValueField = "XRT_RepaymentTypeCode";
            ddlRepaymentType.DataBind();
            ddlRepaymentType.Items.Insert(0, new ListItem("Select a Repayment Type", "Select a Repayment Type"));
        }

        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLoanType.SelectedIndex != 0)
            {
                // Bind List of Clients for that Scheme
                int LoanTypeCode = 0;
                int LoanPartnerCode = 0;
                LoanTypeCode = Int32.Parse(ddlLoanType.SelectedValue.ToString().Trim());
                LoanPartnerCode = Int32.Parse(ddlLoanPartner.SelectedValue.ToString().Trim());

                BindSchemesDropDown(LoanTypeCode, LoanPartnerCode);

                if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "2" || ddlLoanType.SelectedValue == "8")
                {
                    pnlAsset.Visible = true;
                    pnlAssetContent.Visible = true;
                    pnlPickAsset.Visible = false;
                    trExistingAssets.Visible = true;
                    trExistingAssetsSpace.Visible = true;
                    trExistingAssetsGrid.Visible = true;
                }
                else if (ddlLoanType.SelectedValue == "3" || ddlLoanType.SelectedValue == "4")
                {
                    pnlAsset.Visible = false;
                    pnlAssetContent.Visible = false;
                    pnlPickAsset.Visible = true;
                    trExistingAssets.Visible = false;
                    trExistingAssetsSpace.Visible = false;
                    trExistingAssetsGrid.Visible = false;
                }
                else
                {
                    pnlAsset.Visible = false;
                    pnlAssetContent.Visible = false;
                    pnlPickAsset.Visible = false;
                    trExistingAssets.Visible = false;
                    trExistingAssetsSpace.Visible = false;
                    trExistingAssetsGrid.Visible = false;
                }
            }
            else
            {
                // Clear the drop down list of all values
                ddlScheme.Items.Clear();
            }
        }

        private void BindLAP(List<int> borrowerIds)
        {
            List<PropertyVo> propertyListVo;
            DataTable dtLAP;
            liabilitiesBo = new LiabilitiesBo();

            // Make the panels visible
            pnlPickAsset.Visible = true;

            // Get all property details for the customer and his associates
            propertyListVo = new List<PropertyVo>();
            if (borrowerIds != null && borrowerIds.Count > 0)
                propertyListVo = liabilitiesBo.GetCustomerAndAssociateProperty(Int32.Parse(ddlClientName.SelectedValue), borrowerIds);
            else
                propertyListVo = liabilitiesBo.GetCustomerAndAssociateProperty(Int32.Parse(ddlClientName.SelectedValue), null);


            if (propertyListVo.Count > 0)
            {
                dtLAP = new DataTable();
                dtLAP.Columns.Add("Id");
                dtLAP.Columns.Add("PropertyName");

                DataRow drProperty;
                for (int i = 0; i < propertyListVo.Count; i++)
                {
                    drProperty = dtLAP.NewRow();
                    drProperty[0] = propertyListVo[i].PropertyId.ToString();
                    drProperty[1] = propertyListVo[i].Name.ToString();
                    dtLAP.Rows.Add(drProperty);
                }

                chklstAssets.DataSource = dtLAP;
                chklstAssets.DataTextField = "PropertyName";
                chklstAssets.DataValueField = "Id";
                chklstAssets.DataBind();
            }
        }

        private void BindLAS()
        {
            List<EQPortfolioVo> EQListVo;
            DataTable dtLAS;
            LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
            DateTime EQValuationDate = new DateTime();
            PortfolioBo portfolioBo = new PortfolioBo();

            // Make the panels visible
            pnlPickAsset.Visible = true;

            // Get all property details for the customer and his associates
            EQListVo = new List<EQPortfolioVo>();

            EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserVo.advisorId, "EQ").ToString());

            EQListVo = liabilitiesBo.GetCustomerAndAssociateShares(Int32.Parse(ddlClientName.SelectedValue), EQValuationDate);

            if (EQListVo.Count > 0)
            {
                dtLAS = new DataTable();
                dtLAS.Columns.Add("Id");
                dtLAS.Columns.Add("ShareName");

                DataRow drProperty;
                for (int i = 0; i < EQListVo.Count; i++)
                {
                    drProperty = dtLAS.NewRow();
                    drProperty[0] = EQListVo[i].EQNetPositionId.ToString();
                    drProperty[1] = EQListVo[i].ScripName.ToString();
                    dtLAS.Rows.Add(drProperty);
                }

                chklstAssets.DataSource = dtLAS;
                chklstAssets.DataTextField = "ShareName";
                chklstAssets.DataValueField = "Id";
                chklstAssets.DataBind();
            }
        }

        private void BindSchemesDropDown(int LoanTypeCode, int LoanPartnerCode)
        {
            DataSet dsSchemeList = new DataSet();
            liabilitiesBo = new LiabilitiesBo();

            dsSchemeList = liabilitiesBo.GetLoanScheme(LoanTypeCode, LoanPartnerCode, rmVo.RMId);
            if (dsSchemeList.Tables[0].Rows.Count > 0)
            {
                ddlScheme.DataSource = dsSchemeList.Tables[0];
                ddlScheme.DataTextField = "SchemeName";
                ddlScheme.DataValueField = "SchemeId";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select a Scheme", "Select a Scheme"));
            }
            else
            {
                ddlScheme.Items.Clear();
            }
        }

        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsClientList = new DataSet();

            if (Session["LoanProcessAction"].ToString() == "add")
            {
                BindSchemeDetails();
                BindAllClientsDropDown();
            }
            else if (Session["LoanProcessAction"].ToString() == "view")
            {
                if (ddlScheme.SelectedIndex != 0)
                {
                    dsClientList = BindClientListDropDown();

                    // Bind Eligibility Criteria
                    int totalResidenceMonths = 0;
                    int totaljobStabMonths = 0;
                    int residenceMonths = 0;
                    int residenceYears = 0;
                    int jobStabMonths = 0;
                    int jobStabYears = 0;

                    if (dsClientList.Tables[1].Rows[0]["Residence_Stability"].ToString() != "")
                        totalResidenceMonths = Int32.Parse(dsClientList.Tables[1].Rows[0]["Residence_Stability"].ToString());

                    if (dsClientList.Tables[1].Rows[0]["Job_Stability"].ToString() != "")
                        totaljobStabMonths = Int32.Parse(dsClientList.Tables[1].Rows[0]["Job_Stability"].ToString());

                    residenceYears = totalResidenceMonths / 12;
                    residenceMonths = totalResidenceMonths % 12;

                    jobStabYears = totaljobStabMonths / 12;
                    jobStabMonths = totaljobStabMonths % 12;

                    txtAgeValue.Text = dsClientList.Tables[1].Rows[0]["ALS_MinimumAge"].ToString();
                    txtResidenceValue.Text = residenceYears + " years and " + residenceMonths + " months";
                    txtJobStabValue.Text = jobStabYears + " years and " + jobStabMonths + " months";
                    txtIncomeValue.Text = dsClientList.Tables[2].Rows[0]["Income"].ToString();
                    txtExpenseValue.Text = dsClientList.Tables[2].Rows[0]["Expense"].ToString();
                    txtNetworthValue.Text = dsClientList.Tables[2].Rows[0]["Networth"].ToString();
                }
                else
                {
                    // Clear the drop down list of all values
                    ddlClientName.Items.Clear();
                }
            }
        }

        private DataSet BindClientListDropDown()
        {
            DataSet dsClientList = new DataSet();
            liabilitiesBo = new LiabilitiesBo();

            dsClientList = liabilitiesBo.GetClientListForScheme(Int32.Parse(ddlScheme.SelectedValue.Trim()));

            // Bind List of Clients for that Scheme
            ddlClientName.DataSource = dsClientList.Tables[0];
            ddlClientName.DataTextField = "CustomerName";
            ddlClientName.DataValueField = "C_CustomerId";
            ddlClientName.DataBind();
            ddlClientName.Items.Insert(0, new ListItem("Select a Client", "Select a Client"));
            return dsClientList;
        }

        private void BindSchemeDetails()
        {
            // Bind Details pertaining to the scheme selected
            DataSet ds = new DataSet();
            liabilitiesBo = new LiabilitiesBo();

            ds = liabilitiesBo.GetLoanSchemeDetails(Int32.Parse(ddlScheme.SelectedValue));

            // Bind Eligibilty Details
            BindEligibilityQualification(ds.Tables[0]);

            // Bind Interest Category
            BindInterestCategoryDDL(ds.Tables[1]);

            //Bind Required Documents Grid View
            if (Session["LoanProcessAction"].ToString() == "add")
            {
                dvDocDropDown.Visible = false;
                gvDocuments.Visible = false;
            }
            else if (Session["LoanProcessAction"].ToString() == "view")
            {
                dvDocDropDown.Visible = true;
                gvDocuments.Visible = true;
            }
            else if (Session["LoanProcessAction"].ToString() == "edit")
            {
                dvDocDropDown.Visible = true;
                gvDocuments.Visible = true;
            }
        }

        protected void ddlDocCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDocumentsGrid();
        }

        private void BindDocumentsGrid()
        {
            if (ddlDocCustomerList.SelectedIndex != 0 && ddlDocCustomerList.SelectedIndex != -1)
            {
                liabilitiesBo = new LiabilitiesBo();
                DataSet dsSubmittedDocs = new DataSet();
                DataSet dsRequiredDocs = new DataSet();
                int liabilitiesAssoId = Int32.Parse(ddlDocCustomerList.SelectedValue);
                int schemeId = Int32.Parse(ddlScheme.SelectedValue);

                // GET SELECTED CUSTOMER SUBMITTED DOCUMENTS
                dsSubmittedDocs = liabilitiesBo.GetCustSubmittedDocs(liabilitiesAssoId);
                ViewState["SubmittedDocs"] = dsSubmittedDocs;

                // GET ALL DOCUMENTS TO BE SUBMITTED FOR THAT SCHEME
                dsRequiredDocs = liabilitiesBo.GetSchemeDocs(schemeId);

                // BIND GRID DOCS
                DataTable dtGridDocs = new DataTable();
                dtGridDocs.Columns.Add("ProposalDocId");
                dtGridDocs.Columns.Add("ProofTypeCode");
                dtGridDocs.Columns.Add("ProofType");
                dtGridDocs.Columns.Add("ProofName");
                //dtGridDocs.Columns.Add("IsMandatory");
                dtGridDocs.Columns.Add("SubmissionDate");
                dtGridDocs.Columns.Add("IsAccepted");
                dtGridDocs.Columns.Add("AcceptedDate");
                dtGridDocs.Columns.Add("AcceptedBy");
                dtGridDocs.Columns.Add("CopyType");
                DataRow drGridDocs;

                if (dsRequiredDocs.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsRequiredDocs.Tables[0].Rows.Count; i++)
                    {
                        drGridDocs = dtGridDocs.NewRow();

                        drGridDocs[0] = 0;
                        drGridDocs[1] = dsRequiredDocs.Tables[0].Rows[i]["XPRT_ProofTypeCode"].ToString();
                        drGridDocs[2] = dsRequiredDocs.Tables[0].Rows[i]["XPRT_ProofType"].ToString();
                        drGridDocs[3] = dsRequiredDocs.Tables[0].Rows[i]["ALSP_ProofName"].ToString();
                        drGridDocs[4] = "";
                        drGridDocs[5] = "";
                        drGridDocs[6] = "";
                        drGridDocs[7] = "";
                        drGridDocs[8] = "";

                        dtGridDocs.Rows.Add(drGridDocs);
                    }
                }

                if (dtGridDocs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGridDocs.Rows)
                    {
                        if (dsSubmittedDocs.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsSubmittedDocs.Tables[0].Rows.Count; j++)
                            {
                                string reqProofTypeCode = dr["ProofTypeCode"].ToString();
                                string reqProofName = dr["ProofName"].ToString();
                                string subProofTypeCode = dsSubmittedDocs.Tables[0].Rows[j]["XPRT_ProofTypeCode"].ToString();
                                string subProofName = dsSubmittedDocs.Tables[0].Rows[j]["ALPD_ProofName"].ToString();

                                if (reqProofTypeCode == subProofTypeCode && reqProofName == subProofName)
                                {
                                    dr[0] = dsSubmittedDocs.Tables[0].Rows[j]["ALPD_ProposalDocId"].ToString();
                                    dr[4] = dsSubmittedDocs.Tables[0].Rows[j]["ALPD_SubmissionDate"].ToString();
                                    dr[5] = dsSubmittedDocs.Tables[0].Rows[j]["ALPD_IsAccepted"].ToString();
                                    dr[6] = dsSubmittedDocs.Tables[0].Rows[j]["ALPD_AcceptedDate"].ToString();
                                    dr[7] = dsSubmittedDocs.Tables[0].Rows[j]["ALPD_AcceptedBy"].ToString();
                                    dr[8] = dsSubmittedDocs.Tables[0].Rows[j]["XPCT_ProofCopyTypeCode"].ToString();
                                }

                            }
                        }
                    }
                    gvDocuments.DataSource = dtGridDocs;
                    gvDocuments.DataBind();

                    if (Session["LoanProcessAction"] != null)
                    {
                        string action = Session["LoanProcessAction"].ToString();

                        if (action == "view")
                        {
                            gvDocuments.EnableViewState = false;
                        }
                        else if (action == "edit")
                        {
                            gvDocuments.EnableViewState = true;
                        }
                    }
                }
                else
                {
                    gvDocuments.DataSource = null;
                    gvDocuments.DataBind();
                }

                BindAdditionalDocs("dropdownlist");
            }
            else
            {
                gvDocuments.DataSource = null;
                gvDocuments.DataBind();
                ViewState["CurrentTable"] = null;
                BindAdditionalDocs("");
            }
        }

        private void BindDocumentsVo(DataTable dtDocuments)
        {
            LoanProposalDocVo loanPropDocVo = new LoanProposalDocVo();
            List<LoanProposalDocVo> loanPropDocListVo = new List<LoanProposalDocVo>();

            if (dtDocuments.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDocuments.Rows)
                {
                    loanPropDocVo.DocProofTypeCode = Int32.Parse(dr["XPRT_ProofTypeCode"].ToString());
                    loanPropDocVo.DocProofTypeName = dr["XPRT_ProofType"].ToString();
                    loanPropDocVo.DocProofName = dr["ALSP_ProofName"].ToString();
                    loanPropDocVo.IsAccepted = 0;

                    loanPropDocListVo.Add(loanPropDocVo);
                }
            }

            Session["LoanProposalDocListVo"] = loanPropDocListVo;
        }

        private void BindAllClientsDropDown()
        {
            DataSet ds = new DataSet();
            LiabilitiesBo liabilitiesBo = new LiabilitiesBo();

            ds = liabilitiesBo.GetRMClientList(rmVo.RMId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlClientName.DataSource = ds.Tables[0];
                ddlClientName.DataTextField = "CustomerName";
                ddlClientName.DataValueField = "C_CustomerId";
                ddlClientName.DataBind();
                ddlClientName.Items.Insert(0, new ListItem("Select a Client", "Select a Client"));
            }
            else
            {
                ddlClientName.Items.Clear();
            }
        }

        private void BindInterestCategoryDDL(DataTable dataTable)
        {
            ddlInterestCat.DataSource = dataTable;
            ddlInterestCat.DataTextField = "InterestCat";
            ddlInterestCat.DataValueField = "InterestRateId";
            ddlInterestCat.DataBind();
            ddlInterestCat.Items.Insert(0, new ListItem("Select Category", "Select Category"));
        }

        private void BindEligibilityQualification(DataTable dt)
        {
            // Bind Scheme Details
            if (dt.Rows[0]["MinAge"].ToString() != "" && dt.Rows[0]["MaxAge"].ToString() != "")
                lblSchemeAgeValue.Text = dt.Rows[0]["MinAge"].ToString() + " to " + dt.Rows[0]["MaxAge"].ToString();
            else
                lblSchemeAgeValue.Text = "N/A";
            lblSchemeResidenceValue.Text = "N/A";
            lblSchemeJobStabValue.Text = "N/A";
            lblSchemeIncomeValue.Text = Double.Parse(dt.Rows[0]["MinSal"].ToString()).ToString("f2");
            lblSchemeExpenseValue.Text = "N/A";
            lblSchemeNetworthValue.Text = "N/A";

            if (dt.Rows[0]["IsFloatingRateInterest"].ToString() == "0")
                rbtnNo.Checked = true;
            else
                rbtnYes.Checked = true;
            txtMargin.Text = Double.Parse(dt.Rows[0]["Margin"].ToString()).ToString("f2");
        }

        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["LoanProcessAction"].ToString() == "view")
            {
                BindProposalDetails();

                BindProposalStageInformation();
            }
            else if (Session["LoanProcessAction"].ToString() == "add")
            {
                BindGuarantors();
            }
        }

        private void BindGuarantors()
        {
            // Get a list of guarantors for that client
            DataTable dt;
            CustomerFamilyBo custFamilyBo = new CustomerFamilyBo();
            dt = custFamilyBo.GetCustomerAssociates(Convert.ToInt32(ddlClientName.SelectedValue));

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlGuarantorName.Items.Clear();
                    ddlGuarantorName.DataSource = dt;
                    ddlGuarantorName.DataTextField = "CustomerName";
                    ddlGuarantorName.DataValueField = "CA_AssociationId";
                    ddlGuarantorName.DataBind();
                    ddlGuarantorName.Items.Insert(0, new ListItem("Select Guarantor", "Select Guarantor"));
                }
            }
        }

        private void BindProposalDetails()
        {
            // Get Entire Scheme Tracking Details into a DataSet
            DataSet dsProposalDetails;
            LiabilitiesBo liabilitiesBo = new LiabilitiesBo();

            dsProposalDetails = liabilitiesBo.GetLoanProcessDetails(Int32.Parse(ddlClientName.SelectedValue), Int32.Parse(ddlLoanType.SelectedValue), Int32.Parse(ddlLoanPartner.SelectedValue), Int32.Parse(ddlScheme.SelectedValue), rmVo.RMId);

            // Pass the corresponding DataSet Tables to these methods
            BindBorrowers(dsProposalDetails.Tables[0]);
            BindEligibilityQualification(dsProposalDetails.Tables[1]);
            BindProposalDetails(dsProposalDetails.Tables[2], dsProposalDetails.Tables[3], dsProposalDetails.Tables[4]);

            txtRemarks.Text = "";
        }

        private void BindBorrowers(DataTable dt)
        {
            int CoBorrowers = dt.Rows.Count;
            txtNoOfCustomers.Text = CoBorrowers.ToString();
            Table tbl = new Table();
            DataTable dtCustAssocitates;
            CustomerFamilyBo custFamilyBo = new CustomerFamilyBo();

            for (int i = 1; i <= CoBorrowers; i++)
            {
                TableRow tr = new TableRow();
                TableCell td1 = new TableCell();
                TableCell td2 = new TableCell();
                if (i == 1 && chkIsMainBorrower.Checked == true)
                    td1.Text = "Guardian Name:";
                else
                    td1.Text = "Co-Borrower " + i + ":";

                dtCustAssocitates = custFamilyBo.GetCustomerAssociates(Convert.ToInt32(ddlClientName.SelectedValue));

                // Drop Down List for Co-Borrowers
                DropDownList ddlCoBorrower = new DropDownList();
                ddlCoBorrower.ID = "ddlCoBorrower" + i.ToString();
                ddlCoBorrower.CssClass = "cmbField";
                ddlCoBorrower.DataSource = dtCustAssocitates;
                ddlCoBorrower.DataTextField = "";
                ddlCoBorrower.DataValueField = "";
                ddlCoBorrower.Items.Insert(0, new ListItem("Select from List", "Select from List"));
                ddlCoBorrower.SelectedValue = dt.Rows[i - 1]["C_CustomerId"].ToString();

                td2.Controls.Add(ddlCoBorrower);
                tr.Cells.Add(td1);
                tr.Cells.Add(td2);
                tbl.Rows.Add(tr);
            }

            dvCoBorrowers.Controls.Add(tbl);

            DropDownList ddlLastCoBorrower = new DropDownList();
            ddlLastCoBorrower = ((DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + CoBorrowers.ToString()));
            ddlLastCoBorrower.AutoPostBack = true;
            ddlLastCoBorrower.SelectedIndexChanged += new EventHandler(ddlLastCoBorrower_SelectedIndexChanged);
        }

        private void BindProposalStageInformation()
        {
            liabilitiesBo = new LiabilitiesBo();
            DataSet dsStageDetails = new DataSet();

            if (ddlLoanPartner.SelectedIndex != 0)
                LoanPartner = ddlLoanPartner.SelectedValue;
            if (ddlLoanType.SelectedIndex != 0)
                LoanType = ddlLoanType.SelectedValue;
            if (ddlScheme.SelectedIndex != 0)
                Scheme = ddlScheme.SelectedValue;
            if (ddlClientName.SelectedIndex != 0)
                ClientName = ddlClientName.SelectedValue;

            if (LoanPartner != string.Empty && LoanType != string.Empty && Scheme != string.Empty && ClientName != string.Empty)
            {
                dsStageDetails = liabilitiesBo.GetProposalStageDetails(LoanPartner, LoanType, Scheme, ClientName, AssociateName, adviserVo.advisorId);
            }

            // Get Application Entry Stage Details
            SetApplicationEntryStageDetails(dsStageDetails.Tables[0], dsStageDetails.Tables[1]);

            // Get Eligibility Status Stage Details
            SetEligibilityStageDetails(dsStageDetails.Tables[2], dsStageDetails.Tables[3]);

            // Get Bank Loan Sanction Stage Details
            SetBankLoanStageDetails(dsStageDetails.Tables[4], dsStageDetails.Tables[5]);

            // Get Loan Disbursal Stage Details
            SetLoanDisbursalStageDetails(dsStageDetails.Tables[6], dsStageDetails.Tables[7]);

            // Get Closure Stage Details
            SetClosureStageDetails(dsStageDetails.Tables[8], dsStageDetails.Tables[9]);
        }

        private void SetApplicationEntryStageDetails(DataTable dtEntryDetails, DataTable dtEntryLogDetails)
        {
            txtApplicationEntryRemark.Text = dtEntryDetails.Rows[0]["ALPS_Remark"].ToString();

            for (int i = 0; i < dtEntryDetails.Rows.Count; i++)
            {
                if (dtEntryDetails.Rows[i]["WPSA_ProposalStageActivityId"].ToString() == "1")
                {
                    if (dtEntryDetails.Rows[i]["ALPSA_IsComplete"].ToString() == "1")
                    {
                        chkDocCollection.Checked = true;
                    }
                    else
                    {
                        chkDocCollection.Checked = false;
                    }
                }
                else if (dtEntryDetails.Rows[i]["WPSA_ProposalStageActivityId"].ToString() == "2")
                {
                    if (dtEntryDetails.Rows[i]["ALPSA_IsComplete"].ToString() == "0")
                    {
                        chkEntry.Checked = true;
                    }
                    else
                    {
                        chkEntry.Checked = false;
                    }
                }
            }

            // BIND APPLICATION ENTRY LOG DETAILS
            BindEntryLogGrid(dtEntryLogDetails);
        }

        private void BindEntryLogGrid(DataTable dtEntryLogDetails)
        {
            if (dtEntryLogDetails.Rows.Count > 0)
            {
                gvApplEntryDecisionLog.DataSource = dtEntryLogDetails;
                gvApplEntryDecisionLog.DataBind();
            }
        }

        private void SetEligibilityStageDetails(DataTable dtEligibilityDetails, DataTable dtEligibilityLogDetails)
        {
            txtEligibilityRemark.Text = dtEligibilityDetails.Rows[0]["ALPS_Remark"].ToString();
            if (dtEligibilityDetails.Rows[0]["XD_DecisionCode"].ToString() == "AIR")
            {
                rbtnEligibilityAdditionalInfo.Checked = true;
            }
            else if (dtEligibilityDetails.Rows[0]["XD_DecisionCode"].ToString() == "AP")
            {
                rbtnEligibilityApproved.Checked = true;
            }
            else if (dtEligibilityDetails.Rows[0]["XD_DecisionCode"].ToString() == "DC")
            {
                rbtnEligibilityDeclined.Checked = true;
            }

            if (rbtnEligibilityDeclined.Checked)
            {
                trEligiDeclineReason.Visible = true;
                ddlEligibilityDeclineReason.SelectedValue = dtEligibilityDetails.Rows[0]["XDR_DeclineReasonCode"].ToString();
            }

            BindEligibilityLogGrid(dtEligibilityLogDetails);
        }

        private void BindEligibilityLogGrid(DataTable dtEligibilityLogDetails)
        {
            if (dtEligibilityLogDetails.Rows.Count > 0)
            {
                // BIND ELIGIBILITY LOG DETAILS
                gvEligibilityDecisionLog.DataSource = dtEligibilityLogDetails;
                gvEligibilityDecisionLog.DataBind();
            }
        }

        private void SetBankLoanStageDetails(DataTable dtBankStageDetails, DataTable dtBankStageLogDetails)
        {
            txtEligibilityRemark.Text = dtBankStageDetails.Rows[0]["ALPS_Remark"].ToString();
            if (dtBankStageDetails.Rows[0]["XD_DecisionCode"].ToString() == "AIR")
            {
                rbtnEligibilityAdditionalInfo.Checked = true;
            }
            else if (dtBankStageDetails.Rows[0]["XD_DecisionCode"].ToString() == "AP")
            {
                rbtnEligibilityApproved.Checked = true;
            }
            else if (dtBankStageDetails.Rows[0]["XD_DecisionCode"].ToString() == "DC")
            {
                rbtnEligibilityDeclined.Checked = true;
            }

            if (rbtnEligibilityDeclined.Checked)
            {
                trEligiDeclineReason.Visible = true;
                ddlEligibilityDeclineReason.SelectedValue = dtBankStageDetails.Rows[0]["XDR_DeclineReasonCode"].ToString();
            }

            BindBankStageLogGrid(dtBankStageLogDetails);
        }

        private void BindBankStageLogGrid(DataTable dtBankStageLogDetails)
        {
            if (dtBankStageLogDetails.Rows.Count > 0)
            {
                // BIND BANK SANCTION LOG DETAILS
                gvEligibilityDecisionLog.DataSource = dtBankStageLogDetails;
                gvEligibilityDecisionLog.DataBind();
            }
        }

        private void SetLoanDisbursalStageDetails(DataTable dtDisbursalDetails, DataTable dtDisbursalLogDetails)
        {
            txtLoanDisbursedRemark.Text = dtDisbursalDetails.Rows[0]["ALPS_Remark"].ToString();
            BindDisbursalLogGrid(dtDisbursalLogDetails);
        }

        private void BindDisbursalLogGrid(DataTable dtDisbursalLogDetails)
        {
            if (dtDisbursalLogDetails.Rows.Count > 0)
            {
                // BIND BANK SANCTION LOG DETAILS
                gvLoanDisbursal.DataSource = dtDisbursalLogDetails;
                gvLoanDisbursal.DataBind();
            }
        }

        private void SetClosureStageDetails(DataTable dtClosureDetails, DataTable dtClosureLogDetails)
        {
            txtClosureRemark.Text = dtClosureDetails.Rows[0]["ALPS_Remark"].ToString();
            BindClosureLogDetails(dtClosureLogDetails);
        }

        private void BindClosureLogDetails(DataTable dtClosureLogDetails)
        {
            if (dtClosureLogDetails.Rows.Count > 0)
            {
                // BIND BANK SANCTION LOG DETAILS
                gvClosure.DataSource = dtClosureLogDetails;
                gvClosure.DataBind();
            }
        }

        private static Dictionary<int, string> GetEMIDates()
        {
            Dictionary<int, string> genEMIDates = new Dictionary<int, string>();

            for (int i = 1; i <= 31; i++)
            {
                genEMIDates.Add(i, i.ToString());
            }

            return genEMIDates;
        }

        private void BindEMIDropDowns()
        {
            // Bind EMI Dates
            ddlEMIDate.DataSource = GetEMIDates();
            ddlEMIDate.DataTextField = "Key";
            ddlEMIDate.DataValueField = "Value";
            ddlEMIDate.DataBind();
            ddlEMIDate.Items.Insert(0, new ListItem("Select Date", "Select Date"));

            // Bind EMI Frequency
            ddlEMIFrequency.DataSource = XMLBo.GetFrequency(path);
            ddlEMIFrequency.DataTextField = "Frequency";
            ddlEMIFrequency.DataValueField = "FrequencyCode";
            ddlEMIFrequency.DataBind();
            ddlEMIFrequency.Items.Insert(0, new ListItem("Select Frequency", "Select Frequency"));
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            int numOfBorrowers = Convert.ToInt32(txtNoOfCustomers.Text.Trim());

            BindCoBorrowersViewGrid(numOfBorrowers);
        }

        private void BindCoBorrowersViewGrid(int numOfBorrowers)
        {
            Table tbl = new Table();
            DataTable dtCustAssocitates;
            LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
            DataSet dsCustAndBorrowerDetails;
            Double TotalIncome = 0;
            Double TotalExpense = 0;
            Double TotalNetWorth = 0;

            dvCoBorrowers.Controls.Clear();
            tbl.ID = "tblCoBorrowers";

            if (numOfBorrowers > 0)
            {
                dtCustAssocitates = liabilitiesBo.GetCustomerAssociates(Convert.ToInt32(ddlClientName.SelectedValue));//, Int32.Parse(ddlLoanType.SelectedValue)

                tbl.Rows.Clear();
                if (dtCustAssocitates.Rows.Count > 0)
                {
                    for (int i = 1; i <= numOfBorrowers; i++)
                    {
                        TableRow tr = new TableRow();
                        TableCell td1 = new TableCell();
                        TableCell td2 = new TableCell();
                        if (i == 1 && chkIsMainBorrower.Checked == true)
                        {
                            Label lblCoBorrower = new Label();
                            lblCoBorrower.CssClass = "FieldName";
                            lblCoBorrower.Text = "Guardian Name:";
                            td1.Controls.Add(lblCoBorrower);
                        }
                        else
                        {
                            Label lblCoBorrower = new Label();
                            lblCoBorrower.CssClass = "FieldName";
                            lblCoBorrower.Text = "Co-Borrower " + i + ":";
                            td1.Controls.Add(lblCoBorrower);
                        }

                        // Drop Down List for Co-Borrowers
                        DropDownList ddlCoBorrower = new DropDownList();
                        ddlCoBorrower.ID = "ddlCoBorrower" + i.ToString();
                        ddlCoBorrower.CssClass = "cmbField";
                        ddlCoBorrower.DataSource = dtCustAssocitates;
                        ddlCoBorrower.DataTextField = "AssociateName";
                        ddlCoBorrower.DataValueField = "AssociateId";
                        ddlCoBorrower.DataBind();
                        ddlCoBorrower.Items.Insert(0, new ListItem("Select from List", "Select from List"));

                        td2.Controls.Add(ddlCoBorrower);
                        tr.Cells.Add(td1);
                        tr.Cells.Add(td2);
                        tbl.Rows.Add(tr);

                        Session["CoBorrower_Table"] = tbl;
                    }

                    dvCoBorrowers.Controls.Add(tbl);

                    DropDownList ddlLastCoBorrower = new DropDownList();
                    ddlLastCoBorrower = ((DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + numOfBorrowers.ToString()));
                    ddlLastCoBorrower.AutoPostBack = true;
                    ddlLastCoBorrower.SelectedIndexChanged += new EventHandler(ddlLastCoBorrower_SelectedIndexChanged);
                }
                else
                {
                    TableRow tr = new TableRow();
                    TableCell td1 = new TableCell();
                    td1.BackColor = System.Drawing.Color.White;
                    td1.ForeColor = System.Drawing.Color.Black;
                    td1.HorizontalAlign = HorizontalAlign.Center;
                    Label lblNoAssociates = new Label();
                    lblNoAssociates.CssClass = "Field";
                    lblNoAssociates.Text = "You have no associates!";
                    td1.Controls.Add(lblNoAssociates);
                    tr.Cells.Add(td1);
                    tbl.Rows.Add(tr);

                    dvCoBorrowers.Controls.Add(tbl);
                }
            }
            else
            {
                // Clear the CoBorrower Division
                dvCoBorrowers.Controls.Clear();

                dsCustAndBorrowerDetails = liabilitiesBo.GetLoanCustEligibiltyCriteria(Convert.ToInt32(ddlClientName.SelectedValue), null, out TotalIncome, out TotalExpense, out TotalNetWorth);

                txtAgeValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["CustAge"].ToString();
                txtResidenceValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["Residence_Stability"].ToString();
                txtJobStabValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["Job_Stability"].ToString();
                txtIncomeValue.Text = TotalIncome.ToString("f2");
                txtExpenseValue.Text = TotalExpense.ToString("f2");
                txtNetworthValue.Text = TotalNetWorth.ToString("f2");

                if (ddlLoanType.SelectedValue == "3")
                {   /* LAP */
                    BindLAP(null);
                }
                else if (ddlLoanType.SelectedValue == "4")
                {   /* LAS */
                    BindLAS();
                }
                else if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "2" || ddlLoanType.SelectedValue == "8")
                {   /* HL or AL */
                    BindAssetsDropDown();
                }

            }
        }

        protected void ddlLastCoBorrower_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PropertyVo> propertyListVo;
            DataTable dtLAP;
            DataTable dtLAS;
            DataSet dsSchemeDetails;
            DataSet dsCustAndBorrowerDetails;
            LiabilitiesBo liabilititesBo = new LiabilitiesBo();
            List<int> custBorrowerIds = new List<int>();
            Double TotalIncome = 0;
            Double TotalExpense = 0;
            Double TotalNetWorth = 0;

            for (int i = 1; i <= Convert.ToInt32(txtNoOfCustomers.Text); i++)
            {
                DropDownList ddlBorrow = new DropDownList();
                ddlBorrow = (DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + i.ToString());
                int custId = Convert.ToInt32(ddlBorrow.SelectedValue);
                custBorrowerIds.Add(custId);
            }

            dsSchemeDetails = liabilititesBo.GetLoanSchemeEligilityCriteria(Convert.ToInt32(ddlScheme.SelectedValue));

            lblSchemeAgeValue.Text = dsSchemeDetails.Tables[0].Rows[0]["ALS_MinimumAge"].ToString() + " to " + dsSchemeDetails.Tables[0].Rows[0]["ALS_MaximumAge"].ToString();
            lblSchemeResidenceValue.Text = "N/A";
            lblSchemeJobStabValue.Text = "N/A";
            lblSchemeIncomeValue.Text = Double.Parse(dsSchemeDetails.Tables[0].Rows[0]["ALS_MinimumSalary"].ToString()).ToString("f2");
            lblSchemeExpenseValue.Text = "N/A";
            lblSchemeNetworthValue.Text = "N/A";

            dsCustAndBorrowerDetails = liabilititesBo.GetLoanCustEligibiltyCriteria(Convert.ToInt32(ddlClientName.SelectedValue), custBorrowerIds, out TotalIncome, out TotalExpense, out TotalNetWorth);

            txtAgeValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["CustAge"].ToString();
            txtResidenceValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["Residence_Stability"].ToString();
            txtJobStabValue.Text = dsCustAndBorrowerDetails.Tables[1].Rows[0]["Job_Stability"].ToString();
            txtIncomeValue.Text = TotalIncome.ToString("f2");
            txtExpenseValue.Text = TotalExpense.ToString("f2");
            txtNetworthValue.Text = TotalNetWorth.ToString("f2");

            if (ddlLoanType.SelectedValue == "3")
            {   /* LAP */
                // Make the panels visible
                pnlPickAsset.Visible = true;

                BindLAP(custBorrowerIds);

                //// Get all property details for the customer and his associates
                //propertyListVo = new List<PropertyVo>();
                //propertyListVo = liabilititesBo.GetCustomerAndAssociateProperty(Int32.Parse(ddlClientName.SelectedValue), custBorrowerIds);

                //if (propertyListVo.Count > 0)
                //{
                //    dtLAP = new DataTable();
                //    dtLAP.Columns.Add("Id");
                //    dtLAP.Columns.Add("PropertyName");

                //    DataRow drProperty;
                //    for (int i = 0; i < propertyListVo.Count; i++)
                //    {
                //        drProperty = dtLAP.NewRow();
                //        drProperty[0] = propertyListVo[i].PropertyId.ToString();
                //        drProperty[1] = propertyListVo[i].Name.ToString();
                //        dtLAP.Rows.Add(drProperty);
                //    }

                //    chklstAssets.DataSource = dtLAP;
                //    chklstAssets.DataTextField = "PropertyName";
                //    chklstAssets.DataValueField = "Id";
                //    chklstAssets.DataBind();
                //}
            }
            else if (ddlLoanType.SelectedValue == "4")
            {   /* LAS */
                // Get all share details for the customer and his associates

            }
            else if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "2" || ddlLoanType.SelectedValue == "8")
            {   /* HL or AL */
                BindAssetsDropDown();
            }
        }

        private void BindAssetsDropDown()
        {
            DataSet dsAssets = new DataSet();
            Dictionary<int, string> dictAssets = new Dictionary<int, string>();
            PropertyBo propertyBo = new PropertyBo();
            PersonalBo personalBo = new PersonalBo();
            List<int> custBorrowerIds = new List<int>();

            if (txtNoOfCustomers.Text.Trim() != "0" && txtNoOfCustomers.Text.Trim() != "")
            {
                for (int i = 1; i <= Convert.ToInt32(txtNoOfCustomers.Text); i++)
                {
                    DropDownList ddlBorrow = new DropDownList();
                    ddlBorrow = (DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + i.ToString());
                    int custId = Convert.ToInt32(ddlBorrow.SelectedValue);
                    custBorrowerIds.Add(custId);
                }
            }

            if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "8")
            {
                dictAssets = propertyBo.GetPropertyDropDown(ddlClientName.SelectedValue, custBorrowerIds);

                ddlExistingAssets.DataSource = dictAssets;
                ddlExistingAssets.DataTextField = "Value";
                ddlExistingAssets.DataValueField = "Key";
                ddlExistingAssets.DataBind();
                ddlExistingAssets.Items.Insert(0, new ListItem("Pick One", "Pick One"));
            }
            else if (ddlLoanType.SelectedValue == "2")
            {
                dsAssets = personalBo.GetPersonalDropDown(ddlClientName.SelectedValue);

                ddlExistingAssets.DataSource = dsAssets.Tables[0];
                ddlExistingAssets.DataTextField = "AssetName";
                ddlExistingAssets.DataValueField = "AssetId";
                ddlExistingAssets.DataBind();
                ddlExistingAssets.Items.Insert(0, new ListItem("Pick One", "Pick One"));
            }

        }

        private void BindCoBorrowerGrid(string LoanTypeCode, string AssetId)
        {
            AssetBo assetBo = new AssetBo();
            if (LoanTypeCode == "1" || ddlLoanType.SelectedValue == "8")
            {
                /* Home Loan */
                List<int> custBorrowerIds = new List<int>();
                DataTable dt = new DataTable();
                DataSet dsMain = new DataSet();
                DataRow dr;
                dt.Columns.Add("CLA_LiabilitiesAssociationId");
                dt.Columns.Add("CustomerId");
                dt.Columns.Add("AssociationId");
                dt.Columns.Add("Name");
                dt.Columns.Add("AssetOwnership");
                dt.Columns.Add("LoanObligation");
                dt.Columns.Add("Margin");

                dsMain = assetBo.GetAssetOwnerShip(Int32.Parse(ddlExistingAssets.SelectedValue), Contants.Property, Int32.Parse(ddlClientName.SelectedValue), 0, 1);

                dr = dt.NewRow();

                dr[0] = 0;
                dr[1] = ddlClientName.SelectedValue;
                dr[3] = ddlClientName.SelectedItem.Text;
                if (dsMain.Tables[0].Rows.Count > 0)
                {
                    dr[2] = dsMain.Tables[0].Rows[0]["AssociationId"].ToString();
                    dr[4] = dsMain.Tables[0].Rows[0]["AssetOwnerShip"].ToString();
                }
                else
                {
                    dr[2] = "0";
                    dr[4] = "";
                }
                dr[5] = "";
                dr[6] = "";
                dt.Rows.Add(dr);

                for (int i = 1; i <= Convert.ToInt32(txtNoOfCustomers.Text); i++)
                {
                    DataSet dsBorrower = new DataSet();

                    dr = dt.NewRow();

                    DropDownList ddlBorrow = new DropDownList();
                    ddlBorrow = (DropDownList)dvCoBorrowers.FindControl("ddlCoBorrower" + i.ToString());
                    int custId = Convert.ToInt32(ddlBorrow.SelectedValue);
                    custBorrowerIds.Add(custId);

                    dsBorrower = assetBo.GetAssetOwnerShip(Int32.Parse(ddlExistingAssets.SelectedValue), Contants.Property, Int32.Parse(ddlClientName.SelectedValue), custId, 0);

                    dr[0] = 0;
                    dr[1] = ddlBorrow.SelectedValue;
                    dr[3] = ddlBorrow.SelectedItem.Text;
                    if (dsBorrower.Tables[0].Rows.Count > 0)
                    {
                        dr[2] = dsBorrower.Tables[0].Rows[0]["AssociationId"].ToString();
                        dr[4] = dsBorrower.Tables[0].Rows[0]["AssetOwnerShip"].ToString();
                    }
                    else
                    {
                        dr[2] = "0";
                        dr[4] = "";
                    }
                    dr[5] = "";
                    dr[6] = "";
                    dt.Rows.Add(dr);
                }
                ViewState["BorrowerGridDataTable"] = dt;
                gvCoBorrower.DataSource = dt;
                gvCoBorrower.DataBind();
                gvCoBorrower.Visible = true;

            }
            else if (LoanTypeCode == "2")
            {
                /* Auto Loan */


            }
        }

        protected void ddlInterestCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Bind Interest Details for the Interest Category and Scheme
            DataSet ds = new DataSet();
            liabilitiesBo = new LiabilitiesBo();

            ds = liabilitiesBo.GetInterestDetails(ddlInterestCat.SelectedValue);

            txtInterestRate.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
            txtProcessCharges.Text = ds.Tables[0].Rows[0]["ProcessingCharges"].ToString();
            txtPreclosingCharges.Text = ds.Tables[0].Rows[0]["PreClosingCharges"].ToString();
            txtSanctionInterestRate.Text = ds.Tables[0].Rows[0]["InterestRate"].ToString();
        }

        protected void txtInterestRate_TextChanged(object sender, EventArgs e)
        {
            if (txtInterestRate.Text.Trim() != "")
            {
                txtSanctionInterestRate.Text = Double.Parse(txtInterestRate.Text.Trim()).ToString("f2");
            }
        }

        protected void btnApplicationEntry_Click(object sender, EventArgs e)
        {
            LoanApplicationEntryVo loanApplEntryVo = new LoanApplicationEntryVo();
            liabilitiesBo = new LiabilitiesBo();
            DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];

            loanApplEntryVo.LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());

            loanApplEntryVo.ProposalStageId = Int32.Parse(ViewState["ApplEntryStageId"].ToString());
            loanApplEntryVo.ApplActivityId1 = Int32.Parse(ViewState["ApplActivityId1"].ToString());
            loanApplEntryVo.ApplActivityId2 = Int32.Parse(ViewState["ApplActivityId2"].ToString());

            if (chkEntry.Checked)
                loanApplEntryVo.Entry = 1;
            else
                loanApplEntryVo.Entry = 0;

            if (chkDocCollection.Checked)
                loanApplEntryVo.DocumentCollection = 1;
            else
                loanApplEntryVo.DocumentCollection = 0;

            loanApplEntryVo.Remark = txtApplicationEntryRemark.Text;

            if (liabilitiesBo.UpdateApplicationEntry(loanApplEntryVo, rmVo.UserId))
            {
                //BindProposalStageInformation();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
            }

        }

        protected void btnEligibility_Click(object sender, EventArgs e)
        {
            LoanEligibilityStatusVo loanEligiVo = new LoanEligibilityStatusVo();
            liabilitiesBo = new LiabilitiesBo();
            DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];

            loanEligiVo.LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());
            loanEligiVo.ProposalStageId = Int32.Parse(ViewState["EligibilityStageId"].ToString());
            if (rbtnEligibilityApproved.Checked)
            {
                loanEligiVo.DecisionCode = Contants.DecisionApproved;
            }
            else if (rbtnEligibilityAdditionalInfo.Checked)
            {
                loanEligiVo.DecisionCode = Contants.DecisionAdditionalInfo;
            }
            else if (rbtnEligibilityDeclined.Checked)
            {
                trEligiDeclineReason.Visible = true;
                loanEligiVo.DecisionCode = Contants.DecisionDeclined;
                loanEligiVo.DeclineReasonCode = Int32.Parse(ddlEligibilityDeclineReason.SelectedValue);
            }

            loanEligiVo.Remark = txtEligibilityRemark.Text;

            if (liabilitiesBo.UpdateEligility(loanEligiVo, rmVo.UserId))
            {
                //BindProposalStageInformation();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
            }
        }

        protected void btnLoanSanction_Click(object sender, EventArgs e)
        {
            LoanSanctionVo loanSancVo = new LoanSanctionVo();
            liabilitiesBo = new LiabilitiesBo();
            DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];

            loanSancVo.LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());
            loanSancVo.ProposalStageId = Int32.Parse(ViewState["SanctionStageId"].ToString());

            if (rbtnLoanSanctionApproved.Checked)
            {
                loanSancVo.DecisionCode = Contants.DecisionApproved;
            }
            else if (rbtnLoanSanctionAdditionalInfo.Checked)
            {
                loanSancVo.DecisionCode = Contants.DecisionAdditionalInfo;
            }
            else if (rbtnLoanSanctionDeclined.Checked)
            {
                trLoanSanctionReason.Visible = true;
                loanSancVo.DecisionCode = Contants.DecisionDeclined;
                loanSancVo.DeclineReasonCode = Int32.Parse(ddlLoanSanctionDeclineReason.SelectedValue);
            }

            loanSancVo.Remark = txtLoanSanctionRemark.Text;

            if (liabilitiesBo.UpdateLoanSanction(loanSancVo, rmVo.UserId))
            {
                //BindProposalStageInformation();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
            }

        }

        protected void btnLoanDisbursed_Click(object sender, EventArgs e)
        {
            LoanDisbursalVo loanDisVo = new LoanDisbursalVo();
            liabilitiesBo = new LiabilitiesBo();
            DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];

            loanDisVo.LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());
            loanDisVo.ProposalStageId = Int32.Parse(ViewState["DisbursalStageId"].ToString());
            if (chkLoanDisbursed.Checked)
                loanDisVo.IsOpen = 0;
            else
                loanDisVo.IsOpen = 1;
            loanDisVo.Remark = txtLoanDisbursedRemark.Text.Trim();

            if (liabilitiesBo.UpdateLoanDisbursed(loanDisVo, rmVo.UserId))
            {
                //BindProposalStageInformation();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
            }
        }

        protected void btnClosure_Click(object sender, EventArgs e)
        {
            LoanClosureVo loanClosureVo = new LoanClosureVo();
            liabilitiesBo = new LiabilitiesBo();
            DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];

            loanClosureVo.LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());
            loanClosureVo.ProposalStageId = Int32.Parse(ViewState["ClosureStageId"].ToString());
            if (chkLoanClosed.Checked)
                loanClosureVo.IsOpen = 0;
            else
                loanClosureVo.IsOpen = 1;
            loanClosureVo.Remark = txtClosureRemark.Text.Trim();

            if (liabilitiesBo.UpdateLoanClosure(loanClosureVo, rmVo.UserId))
            {
                //BindProposalStageInformation();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoanProposalVo loanProposalVo = new LoanProposalVo();
            LoanProposalStageVo loanProposalStageVo = new LoanProposalStageVo();
            LoanProposalDocVo loanProposalDocVo = new LoanProposalDocVo();
            LiabilitiesBo liabilitiesBo = new LiabilitiesBo();
            AssetAssociationVo assetAssociationVo;
            List<AssetAssociationVo> assetAssoVoList;
            LiabilityAssociateVo liabilityAssociateVo;
            bool blAssetAssociationDone = false;

            if (btnSubmit.Text == "Submit")
            {
                #region SubmitCode

                int LoanProposalId = 0;
                int LiabilitiesId = 0;
                int associationId = 0;

                // Loan Application Entry Stage Details
                if (chkDocCollection.Checked)
                    loanProposalStageVo.Application_DocumentCollection = 1;
                else
                    loanProposalStageVo.Application_DocumentCollection = 0;

                if (chkEntry.Checked)
                    loanProposalStageVo.Application_Entry = 1;
                else
                    loanProposalStageVo.Application_Entry = 0;

                loanProposalStageVo.Application_Remark = txtApplicationEntryRemark.Text;
                loanProposalStageVo.Application_ProposalStageId = 1;

                loanProposalStageVo.Application_IsOpen = 1;
                loanProposalStageVo.Eligibility_IsOpen = 1;
                loanProposalStageVo.BankSanction_IsOpen = 1;
                loanProposalStageVo.Disbursal_IsOpen = 1;
                loanProposalStageVo.Closure_IsOpen = 1;

                // Proposal Details 
                loanProposalVo.LoanPartnerId = Int32.Parse(ddlLoanPartner.SelectedValue);
                loanProposalVo.LoanTypeId = Int32.Parse(ddlLoanType.SelectedValue);
                loanProposalVo.SchemeId = Int32.Parse(ddlScheme.SelectedValue);
                loanProposalVo.ApplicationNum = Int32.Parse(txtApplicationNo.Text.Trim());
                loanProposalVo.AppliedLoanAmount = Double.Parse(txtAppliedLoanAmt.Text.Trim());
                loanProposalVo.AppliedLoanPeriod = Int32.Parse(txtAppliedLoanPeriod.Text.Trim());
                loanProposalVo.BankReferenceNum = txtBankReference.Text.Trim();

                // Get Branch Id based on the associate or adviser id
                loanProposalVo.BranchId = branchId;

                loanProposalVo.Introducer = txtIntroducer.Text.Trim();
                if (chkIsMainBorrower.Checked)
                    loanProposalVo.IsMainBorrowerMinor = 1;
                else
                    loanProposalVo.IsMainBorrowerMinor = 0;
                loanProposalVo.Remark = txtRemarks.Text;
                if (txtSanctionDate.Text.Trim() != "")
                    loanProposalVo.SanctionDate = DateTime.Parse(txtSanctionDate.Text.Trim());
                loanProposalVo.SanctionAmount = Double.Parse(txtSanctionAmount.Text.Trim());
                loanProposalVo.SanctionInterestRate = float.Parse(txtSanctionInterestRate.Text.Trim());
                if (txtEMIAmount.Text.Trim() != "")
                    loanProposalVo.EMIAmount = Double.Parse(txtEMIAmount.Text.Trim());
                if (ddlEMIDate.SelectedValue.Trim() != "Select Date")
                    loanProposalVo.EMIDate = Int32.Parse(ddlEMIDate.SelectedValue.Trim());
                if (ddlRepaymentType.SelectedValue != "Select a Repayment Type")
                    loanProposalVo.RepaymentType = ddlRepaymentType.SelectedValue;
                if (ddlEMIFrequency.SelectedValue != "Select Frequency")
                    loanProposalVo.EMIFrequency = ddlEMIFrequency.SelectedValue.Trim();
                if (txtNoOfInstallments.Text.Trim() != "")
                    loanProposalVo.NoOfInstallments = Int32.Parse(txtNoOfInstallments.Text.Trim());
                if (txtAmountPrepaid.Text.Trim() != "")
                    loanProposalVo.AmountPrepaid = Double.Parse(txtAmountPrepaid.Text.Trim());
                if (txtInstallStartDate.Text.Trim() != "")
                    loanProposalVo.InstallmentStartDate = DateTime.Parse(txtInstallStartDate.Text.Trim());
                if (txtInstallEndDate.Text.Trim() != "")
                    loanProposalVo.InstallmentEndDate = DateTime.Now;
                loanProposalVo.InterestCategoryId = Int32.Parse(ddlInterestCat.SelectedValue);

                // Add Documents entries into the Vo
                if (liabilitiesBo.AddLoanProposal(loanProposalVo, loanProposalStageVo, loanProposalDocVo, userVo.UserId, out LoanProposalId, out LiabilitiesId))
                {
                    if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "2" || ddlLoanType.SelectedValue == "8")
                    {

                        assetAssociationVo = new AssetAssociationVo();
                        if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "8")
                        {
                            assetAssociationVo.AssetGroupCode = "PR";
                            assetAssociationVo.AssetTable = "CustomerPropertyNetPosition";
                            assetAssociationVo.AssetId = Int32.Parse(ddlExistingAssets.SelectedValue);
                        }
                        else if (ddlLoanType.SelectedValue == "2")
                        {
                            assetAssociationVo.AssetGroupCode = "PI";
                            assetAssociationVo.AssetTable = "CustomerPersonalNetPosition";
                            assetAssociationVo.AssetId = Int32.Parse(ddlExistingAssets.SelectedValue);
                        }

                        assetAssociationVo.CreatedBy = userVo.UserId;
                        assetAssociationVo.ModifiedBy = userVo.UserId;
                        assetAssociationVo.LiabilitiesId = LiabilitiesId;

                        blAssetAssociationDone = liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);

                    }
                    else if (ddlLoanType.SelectedValue == "3" || ddlLoanType.SelectedValue == "4")
                    {
                        assetAssoVoList = new List<AssetAssociationVo>();
                        assetAssociationVo = new AssetAssociationVo();
                        assetAssoVoList = null;


                        if (ddlLoanType.SelectedValue == "3")
                        {   // LAP
                            foreach (ListItem li in chklstAssets.Items)
                            {
                                if (li.Selected == true)
                                {
                                    assetAssociationVo.AssetGroupCode = "PR";
                                    assetAssociationVo.AssetTable = "CustomerPropertyNetPosition";
                                    assetAssociationVo.AssetId = Int32.Parse(li.Value);
                                    assetAssociationVo.CreatedBy = userVo.UserId;
                                    assetAssociationVo.ModifiedBy = userVo.UserId;
                                    assetAssociationVo.LiabilitiesId = LiabilitiesId;

                                    blAssetAssociationDone = liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                                }
                            }
                        }
                        else if (ddlLoanType.SelectedValue == "4")
                        {   // LAS
                            foreach (ListItem li in chklstAssets.Items)
                            {
                                if (li.Selected == true)
                                {
                                    assetAssociationVo.AssetGroupCode = "DE";
                                    assetAssociationVo.AssetTable = "CustomerEquityNetPosition";
                                    assetAssociationVo.AssetId = Int32.Parse(li.Value);
                                    assetAssociationVo.CreatedBy = userVo.UserId;
                                    assetAssociationVo.ModifiedBy = userVo.UserId;
                                    assetAssociationVo.LiabilitiesId = LiabilitiesId;

                                    blAssetAssociationDone = liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                                }
                            }
                        }
                    }
                    else
                    {
                        blAssetAssociationDone = true;
                    }

                    if (blAssetAssociationDone)
                    {
                        /* CREATE LIABILITY ASSOCIATE FOR GUARANTOR */
                        if (ddlGuarantorName.SelectedIndex != 0)
                        {
                            liabilityAssociateVo = new LiabilityAssociateVo();
                            liabilityAssociateVo.AssociationId = Int32.Parse(ddlGuarantorName.SelectedValue);
                            liabilityAssociateVo.LoanAssociateCode = "GR";
                            liabilityAssociateVo.LiabilitySharePer = 0;
                            liabilityAssociateVo.MarginPer = 0;
                            liabilityAssociateVo.LiabilitiesId = LiabilitiesId;
                            liabilityAssociateVo.CreatedBy = userVo.UserId;
                            liabilityAssociateVo.ModifiedBy = userVo.UserId;
                            liabilitiesBo.CreateLiabilityAssociates(liabilityAssociateVo);
                        }

                        /* Create Liabilities Associates for Co-Borrowers and Main Customer */
                        bool blResult = false;

                        if (txtNoOfCustomers.Text.Trim() != "" && txtNoOfCustomers.Text.Trim() != "0")
                        {
                            foreach (GridViewRow gvr in gvCoBorrower.Rows)
                            {
                                liabilityAssociateVo = new LiabilityAssociateVo();
                                int selectedRow = gvr.RowIndex;
                                associationId = int.Parse(gvCoBorrower.DataKeys[selectedRow].Values["AssociationId"].ToString());
                                liabilityAssociateVo.AssociationId = associationId;

                                if (selectedRow == 0)
                                {
                                    /* Main Customer */
                                    liabilityAssociateVo.LoanAssociateCode = "MC";
                                }
                                else if (selectedRow > 0)
                                {
                                    /* Co - Borrowers */
                                    liabilityAssociateVo.LoanAssociateCode = "CB";
                                }


                                if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "8")
                                {
                                    if ((TextBox)gvr.FindControl("txtAssetOwnership") != null)
                                    {
                                        TextBox txtAsset = (TextBox)gvr.FindControl("txtAssetOwnership");
                                        float share = float.Parse(txtAsset.Text.Trim());

                                        PropertyVo propVo = new PropertyVo();
                                        PropertyBo propBo = new PropertyBo();
                                        propVo = propBo.GetPropertyAsset(Int32.Parse(ddlExistingAssets.SelectedValue));

                                        liabilitiesBo.UpdatePropertyAccountAssociates(LiabilitiesId, share, liabilityAssociateVo.AssociationId, liabilityAssociateVo.LoanAssociateCode);
                                    }
                                }
                                if ((TextBox)gvr.FindControl("txtLoanObligation") != null)
                                {
                                    TextBox txtObligation = (TextBox)gvr.FindControl("txtLoanObligation");
                                    liabilityAssociateVo.LiabilitySharePer = float.Parse(txtObligation.Text);
                                }
                                if ((TextBox)gvr.FindControl("txtMargin") != null)
                                {
                                    TextBox txtMargin = (TextBox)gvr.FindControl("txtMargin");
                                    liabilityAssociateVo.MarginPer = float.Parse(txtMargin.Text);
                                }
                                liabilityAssociateVo.LiabilitiesId = LiabilitiesId;
                                liabilityAssociateVo.CreatedBy = userVo.UserId;
                                liabilityAssociateVo.ModifiedBy = userVo.UserId;

                                blResult = liabilitiesBo.CreateLiabilityAssociates(liabilityAssociateVo);
                            }

                            if (blResult)
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
                            }
                        }
                        else
                        {
                            CustomerFamilyBo custFamilyBo = new CustomerFamilyBo();
                            liabilityAssociateVo = new LiabilityAssociateVo();
                            associationId = custFamilyBo.GetCustomersAssociationId(Int32.Parse(ddlClientName.SelectedValue));
                            liabilityAssociateVo.AssociationId = associationId;
                            liabilityAssociateVo.LoanAssociateCode = "MC";
                            liabilityAssociateVo.LiabilitiesId = LiabilitiesId;
                            liabilityAssociateVo.CreatedBy = userVo.UserId;
                            liabilityAssociateVo.ModifiedBy = userVo.UserId;

                            if (liabilitiesBo.CreateLiabilityAssociates(liabilityAssociateVo))
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
                            }
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Error Adding Proposal!');", true);
                }

                #endregion
            }
            else if (btnSubmit.Text == "Update")
            {
                #region UpdateCode

                DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];

                int LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());
                int LiabilitiesId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["CL_LiabilitiesId"].ToString());
                int associationId = 0;

                // Proposal Details 
                loanProposalVo.LoanProposalId = LoanProposalId;
                loanProposalVo.LoanPartnerId = Int32.Parse(ddlLoanPartner.SelectedValue);
                loanProposalVo.LoanTypeId = Int32.Parse(ddlLoanType.SelectedValue);
                loanProposalVo.SchemeId = Int32.Parse(ddlScheme.SelectedValue);
                loanProposalVo.ApplicationNum = Int32.Parse(txtApplicationNo.Text.Trim());
                loanProposalVo.AppliedLoanAmount = Double.Parse(txtAppliedLoanAmt.Text.Trim());
                loanProposalVo.AppliedLoanPeriod = Int32.Parse(txtAppliedLoanPeriod.Text.Trim());
                loanProposalVo.BankReferenceNum = txtBankReference.Text.Trim();

                // Get Branch Id based on the associate or adviser id
                loanProposalVo.BranchId = branchId;

                loanProposalVo.Introducer = txtIntroducer.Text.Trim();
                if (chkIsMainBorrower.Checked)
                    loanProposalVo.IsMainBorrowerMinor = 1;
                else
                    loanProposalVo.IsMainBorrowerMinor = 0;
                loanProposalVo.Remark = txtRemarks.Text;
                if (txtSanctionDate.Text.Trim() != "")
                    loanProposalVo.SanctionDate = DateTime.Parse(txtSanctionDate.Text.Trim());
                loanProposalVo.SanctionAmount = Double.Parse(txtSanctionAmount.Text.Trim());
                loanProposalVo.SanctionInterestRate = float.Parse(txtSanctionInterestRate.Text.Trim());
                if (txtEMIAmount.Text.Trim() != "")
                    loanProposalVo.EMIAmount = Double.Parse(txtEMIAmount.Text.Trim());
                if (ddlEMIDate.SelectedValue.Trim() != "Select Date")
                    loanProposalVo.EMIDate = Int32.Parse(ddlEMIDate.SelectedValue.Trim());
                if (ddlRepaymentType.SelectedValue != "Select a Repayment Type")
                    loanProposalVo.RepaymentType = ddlRepaymentType.SelectedValue;
                if (ddlEMIFrequency.SelectedValue != "Select Frequency")
                    loanProposalVo.EMIFrequency = ddlEMIFrequency.SelectedValue.Trim();

                if (txtNoOfInstallments.Text.Trim() != "")
                    loanProposalVo.NoOfInstallments = Int32.Parse(txtNoOfInstallments.Text.Trim());
                if (txtAmountPrepaid.Text.Trim() != "")
                    loanProposalVo.AmountPrepaid = Double.Parse(txtAmountPrepaid.Text.Trim());
                if (txtInstallStartDate.Text.Trim() != "")
                    loanProposalVo.InstallmentStartDate = DateTime.Parse(txtInstallStartDate.Text.Trim());
                if (txtInstallEndDate.Text.Trim() != "")
                    loanProposalVo.InstallmentEndDate = DateTime.Parse(txtInstallEndDate.Text.Trim());
                loanProposalVo.InterestCategoryId = Int32.Parse(ddlInterestCat.SelectedValue);

                // Add Documents entries into the Vo
                if (liabilitiesBo.UpdateLoanProposal(loanProposalVo, userVo.UserId))
                {
                    if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "2" || ddlLoanType.SelectedValue == "8")
                    {
                        if (liabilitiesBo.DeleteLiabilityAssetAssociation(LiabilitiesId))
                        {
                            assetAssociationVo = new AssetAssociationVo();
                            if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "8")
                            {
                                assetAssociationVo.AssetGroupCode = "PR";
                                assetAssociationVo.AssetTable = "CustomerPropertyNetPosition";
                                assetAssociationVo.AssetId = Int32.Parse(ddlExistingAssets.SelectedValue);
                            }
                            else if (ddlLoanType.SelectedValue == "2")
                            {
                                assetAssociationVo.AssetGroupCode = "PI";
                                assetAssociationVo.AssetTable = "CustomerPersonalNetPosition";
                                assetAssociationVo.AssetId = Int32.Parse(ddlExistingAssets.SelectedValue);
                            }

                            assetAssociationVo.ModifiedBy = userVo.UserId;
                            assetAssociationVo.LiabilitiesId = LiabilitiesId;

                            blAssetAssociationDone = liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                        }
                    }
                    else if (ddlLoanType.SelectedValue == "3" || ddlLoanType.SelectedValue == "4")
                    {
                        assetAssoVoList = new List<AssetAssociationVo>();
                        assetAssociationVo = new AssetAssociationVo();
                        assetAssoVoList = null;

                        if (ddlLoanType.SelectedValue == "3")
                        {   // LAP

                            // Delete Existing Asset Associations
                            if (liabilitiesBo.DeleteLiabilityAssetAssociation(LiabilitiesId))
                            {
                                // Create/Update New Ones
                                foreach (ListItem li in chklstAssets.Items)
                                {
                                    if (li.Selected == true)
                                    {
                                        assetAssociationVo.AssetGroupCode = "PR";
                                        assetAssociationVo.AssetTable = "CustomerPropertyNetPosition";
                                        assetAssociationVo.AssetId = Int32.Parse(li.Value);
                                        assetAssociationVo.CreatedBy = userVo.UserId;
                                        assetAssociationVo.ModifiedBy = userVo.UserId;
                                        assetAssociationVo.LiabilitiesId = LiabilitiesId;

                                        blAssetAssociationDone = liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                                    }
                                }
                            }
                        }
                        else if (ddlLoanType.SelectedValue == "4")
                        {   // LAS

                            // Delete Existing Asset Associations
                            if (liabilitiesBo.DeleteLiabilityAssetAssociation(LiabilitiesId))
                            {
                                foreach (ListItem li in chklstAssets.Items)
                                {
                                    if (li.Selected == true)
                                    {
                                        assetAssociationVo.AssetGroupCode = "DE";
                                        assetAssociationVo.AssetTable = "CustomerEquityNetPosition";
                                        assetAssociationVo.AssetId = Int32.Parse(li.Value);
                                        assetAssociationVo.CreatedBy = userVo.UserId;
                                        assetAssociationVo.ModifiedBy = userVo.UserId;
                                        assetAssociationVo.LiabilitiesId = LiabilitiesId;

                                        blAssetAssociationDone = liabilitiesBo.CreatLiabilityAssetAssociation(assetAssociationVo);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        blAssetAssociationDone = true;
                    }


                    if (blAssetAssociationDone)
                    {
                        /* CREATE LIABILITY ASSOCIATE FOR GUARANTOR */
                        if (ddlGuarantorName.SelectedIndex != 0)
                        {
                            liabilityAssociateVo = new LiabilityAssociateVo();
                            liabilityAssociateVo.LiabilitiesAssociationId = Int32.Parse(ViewState["LiabilityAssociationId"].ToString());
                            liabilityAssociateVo.AssociationId = Int32.Parse(ddlGuarantorName.SelectedValue);
                            liabilityAssociateVo.LoanAssociateCode = "GR";
                            liabilityAssociateVo.LiabilitySharePer = 0;
                            liabilityAssociateVo.MarginPer = 0;
                            liabilityAssociateVo.LiabilitiesId = LiabilitiesId;
                            liabilityAssociateVo.ModifiedBy = userVo.UserId;
                            liabilitiesBo.UpdateLiabilityAssociates(liabilityAssociateVo);
                        }

                        if (txtNoOfCustomers.Text.Trim() != "" && txtNoOfCustomers.Text.Trim() != "0")
                        {
                            /* Create Liabilities Associates for Co-Borrowers */
                            bool blResult = false;

                            foreach (GridViewRow gvr in gvCoBorrower.Rows)
                            {
                                liabilityAssociateVo = new LiabilityAssociateVo();
                                int selectedRow = gvr.RowIndex;
                                associationId = int.Parse(gvCoBorrower.DataKeys[selectedRow].Values["AssociationId"].ToString());
                                liabilityAssociateVo.AssociationId = associationId;

                                if (selectedRow == 0)
                                {
                                    /* Main Customer */
                                    liabilityAssociateVo.LoanAssociateCode = "MC";
                                }
                                else if (selectedRow > 0)
                                {
                                    /* Co - Borrowers */
                                    liabilityAssociateVo.LoanAssociateCode = "CB";
                                }

                                if (ddlLoanType.SelectedValue == "1" || ddlLoanType.SelectedValue == "8")
                                {
                                    if ((TextBox)gvr.FindControl("txtAssetOwnership") != null)
                                    {
                                        TextBox txtAsset = (TextBox)gvr.FindControl("txtAssetOwnership");
                                        float share = float.Parse(txtAsset.Text.Trim());

                                        PropertyVo propVo = new PropertyVo();
                                        PropertyBo propBo = new PropertyBo();
                                        propVo = propBo.GetPropertyAsset(Int32.Parse(ddlExistingAssets.SelectedValue));

                                        liabilitiesBo.UpdatePropertyAccountAssociates(LiabilitiesId, share, liabilityAssociateVo.AssociationId, liabilityAssociateVo.LoanAssociateCode);
                                    }
                                }
                                if ((TextBox)gvr.FindControl("txtLoanObligation") != null)
                                {
                                    TextBox txtObligation = (TextBox)gvr.FindControl("txtLoanObligation");
                                    liabilityAssociateVo.LiabilitySharePer = float.Parse(txtObligation.Text);
                                }
                                if ((TextBox)gvr.FindControl("txtMargin") != null)
                                {
                                    TextBox txtMargin = (TextBox)gvr.FindControl("txtMargin");
                                    liabilityAssociateVo.MarginPer = float.Parse(txtMargin.Text);
                                }
                                liabilityAssociateVo.LiabilitiesId = LiabilitiesId;
                                liabilityAssociateVo.CreatedBy = userVo.UserId;
                                liabilityAssociateVo.ModifiedBy = userVo.UserId;

                                blResult = liabilitiesBo.UpdateLiabilityAssociates(liabilityAssociateVo);
                            }

                            if (blResult)
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
                            }
                        }
                        else
                        {
                            CustomerFamilyBo custFamilyBo = new CustomerFamilyBo();
                            liabilityAssociateVo = new LiabilityAssociateVo();
                            associationId = custFamilyBo.GetCustomersAssociationId(Int32.Parse(ddlClientName.SelectedValue));
                            liabilityAssociateVo.AssociationId = associationId;
                            liabilityAssociateVo.LoanAssociateCode = "MC";
                            liabilityAssociateVo.LiabilitiesId = LiabilitiesId;
                            liabilityAssociateVo.CreatedBy = userVo.UserId;
                            liabilityAssociateVo.ModifiedBy = userVo.UserId;

                            if (liabilitiesBo.UpdateLiabilityAssociates(liabilityAssociateVo))
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
                            }
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Error Adding/Updating Proposal!');", true);
                }

                #endregion
            }
        }

        protected void chkIsMainBorrower_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddAsset_Click(object sender, EventArgs e)
        {
            LoanProposalVo loanProposalVo = new LoanProposalVo();
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerVo customerVo = new CustomerVo();
            CustomerBo customerBo = new CustomerBo();

            // Store Proposal Details in Session
            loanProposalVo.LoanPartnerId = Int32.Parse(ddlLoanPartner.SelectedValue);
            loanProposalVo.LoanTypeId = Int32.Parse(ddlLoanType.SelectedValue);
            loanProposalVo.SchemeId = Int32.Parse(ddlScheme.SelectedValue);
            if (txtApplicationNo.Text.Trim() != "")
                loanProposalVo.ApplicationNum = Int32.Parse(txtApplicationNo.Text.Trim());
            if (txtAppliedLoanAmt.Text.Trim() != "")
                loanProposalVo.AppliedLoanAmount = Double.Parse(txtAppliedLoanAmt.Text.Trim());
            if (txtAppliedLoanPeriod.Text.Trim() != "")
                loanProposalVo.AppliedLoanPeriod = Int32.Parse(txtAppliedLoanPeriod.Text.Trim());
            loanProposalVo.BankReferenceNum = txtBankReference.Text.Trim();

            // Get Branch Id based on the associate or adviser id
            loanProposalVo.BranchId = branchId;

            loanProposalVo.Introducer = txtIntroducer.Text.Trim();
            if (chkIsMainBorrower.Checked)
                loanProposalVo.IsMainBorrowerMinor = 1;
            else
                loanProposalVo.IsMainBorrowerMinor = 0;
            loanProposalVo.Remark = txtRemarks.Text;
            if (txtSanctionDate.Text.Trim() != "")
                loanProposalVo.SanctionDate = DateTime.Parse(txtSanctionDate.Text.Trim());
            if (txtSanctionAmount.Text.Trim() != "")
                loanProposalVo.SanctionAmount = Double.Parse(txtSanctionAmount.Text.Trim());
            if (txtSanctionInterestRate.Text.Trim() != "")
                loanProposalVo.SanctionInterestRate = float.Parse(txtSanctionInterestRate.Text.Trim());
            if (txtEMIAmount.Text.Trim() != "")
                loanProposalVo.EMIAmount = Double.Parse(txtEMIAmount.Text.Trim());
            if (ddlEMIDate.SelectedValue.Trim() != "Select Date")
                loanProposalVo.EMIDate = Int32.Parse(ddlEMIDate.SelectedValue.Trim());
            if (ddlRepaymentType.SelectedValue != "Select a Repayment Type")
                loanProposalVo.RepaymentType = ddlRepaymentType.SelectedValue;
            if (ddlEMIFrequency.SelectedValue != "Select Frequency")
                loanProposalVo.EMIFrequency = ddlEMIFrequency.SelectedValue.Trim();
            if (txtNoOfInstallments.Text.Trim() != "")
                loanProposalVo.NoOfInstallments = Int32.Parse(txtNoOfInstallments.Text.Trim());
            if (txtAmountPrepaid.Text.Trim() != "")
                loanProposalVo.AmountPrepaid = Double.Parse(txtAmountPrepaid.Text.Trim());
            if (txtInstallStartDate.Text.Trim() != "")
                loanProposalVo.InstallmentStartDate = DateTime.Parse(txtInstallStartDate.Text.Trim());
            if (txtInstallEndDate.Text.Trim() != "")
                loanProposalVo.InstallmentEndDate = DateTime.Now;
            if (ddlInterestCat.SelectedValue != "Select Category")
                loanProposalVo.InterestCategoryId = Int32.Parse(ddlInterestCat.SelectedValue);
            loanProposalVo.ClientId = ddlClientName.SelectedValue.ToString();
            if (ddlGuarantorName.SelectedValue != "Select Guarantor")
                loanProposalVo.GuarantorId = ddlGuarantorName.SelectedValue;

            Session["loanProposalVo"] = loanProposalVo;

            customerVo = customerBo.GetCustomer(Int32.Parse(loanProposalVo.ClientId));
            Session["CustomerVo"] = customerVo;
            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(Int32.Parse(ddlClientName.SelectedValue));
            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
            Session[SessionContents.LoanProcessTracking] = "true";
            if (ddlLoanType.SelectedValue == "1")
            {
                Session["action"] = "PR";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('CustomerAccountAdd','none');", true);
            }
            else if (ddlLoanType.SelectedValue == "2")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioPersonalEntry','none');", true);
            }
        }

        protected void rbtnEligibility_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnEligibilityDeclined.Checked)
            {
                trEligiDeclineReason.Visible = true;
            }
            else
            {
                trEligiDeclineReason.Visible = false;
            }
        }

        protected void rbtnLoanSanction_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoanSanctionDeclined.Checked)
            {
                trLoanSanctionReason.Visible = true;
            }
            else
            {
                trLoanSanctionReason.Visible = false;
            }
        }

        protected void gvCoBorrower_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["BorrowerGridDataTable"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow gvr = e.Row;
                TextBox txtOwnership = e.Row.FindControl("txtAssetOwnership") as TextBox;
                TextBox txtLoanObli = e.Row.FindControl("txtLoanObligation") as TextBox;
                TextBox txtMargin = e.Row.FindControl("txtMargin") as TextBox;
                if (txtOwnership != null)
                {
                    txtOwnership.Text = dt.Rows[gvr.RowIndex]["AssetOwnerShip"].ToString();
                }
                if (txtLoanObli != null)
                {
                    txtLoanObli.Text = dt.Rows[gvr.RowIndex]["LoanObligation"].ToString();
                }
                if (txtMargin != null)
                {
                    txtMargin.Text = dt.Rows[gvr.RowIndex]["Margin"].ToString();
                }
            }
        }

        protected void txtAppliedLoanAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtAppliedLoanAmt.Text.Trim() != "")
            {
                txtSanctionAmount.Text = Double.Parse(txtAppliedLoanAmt.Text.Trim()).ToString("f2");
            }
        }

        protected void ddlExistingAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExistingAssets.SelectedIndex != 0)
            {
                BindCoBorrowerGrid(ddlLoanType.SelectedValue, ddlExistingAssets.SelectedValue);
            }
        }

        protected void gvDocuments_RowDatabound(object sender, GridViewRowEventArgs e)
        {

            DataSet dsSubmitted = (DataSet)ViewState["SubmittedDocs"];

            // If the current row is a DataRow (and not a Header or Footer row), then do stuff.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                CheckBox chk;
                RadioButton rbtnYes;
                RadioButton rbtnNo;
                TextBox txtAcceptedBy;
                Label lblSubDate;
                Label lblAcceptedDate;
                DropDownList ddlCopyType;

                chk = e.Row.FindControl("chkBx") as CheckBox;
                rbtnYes = e.Row.FindControl("rbtnAcceptedYes") as RadioButton;
                rbtnNo = e.Row.FindControl("rbtnAcceptedNo") as RadioButton;
                txtAcceptedBy = e.Row.FindControl("txtAcceptedBy") as TextBox;
                lblSubDate = e.Row.FindControl("lblSubmissionDate") as Label;
                lblAcceptedDate = e.Row.FindControl("lblAcceptedDate") as Label;
                ddlCopyType = e.Row.FindControl("ddlCopyType") as DropDownList;

                if (ddlCopyType != null)
                {
                    DataTable dtCopy = GetCopyType();

                    ddlCopyType.DataSource = dtCopy;
                    ddlCopyType.DataTextField = "XPCT_ProofCopyType";
                    ddlCopyType.DataValueField = "XPCT_ProofCopyTypeCode";
                    ddlCopyType.DataBind();
                    ddlCopyType.Items.Insert(0, new ListItem("Select Copy Type", "Select Copy Type"));
                }


                if (drv["ProposalDocId"].ToString() != "0" && drv["ProposalDocId"].ToString() != "")
                {
                    if (chk != null)
                    {
                        chk.Checked = true;
                    }

                    if (drv["IsAccepted"].ToString() == "1")
                    {
                        rbtnYes.Checked = true;
                    }
                    else
                    {
                        rbtnNo.Checked = true;
                    }

                    if (txtAcceptedBy != null)
                    {
                        txtAcceptedBy.Text = drv["AcceptedBy"].ToString();
                    }
                    if (ddlCopyType != null)
                    {
                        ddlCopyType.SelectedValue = drv["CopyType"].ToString();
                    }
                    if (lblSubDate != null)
                    {
                        if (drv["SubmissionDate"].ToString() != "")
                            lblSubDate.Text = DateTime.Parse(drv["SubmissionDate"].ToString()).ToShortDateString();
                        else
                            lblSubDate.Text = "";
                    }
                    if (lblAcceptedDate != null)
                    {
                        if (drv["AcceptedDate"].ToString() != "")
                            lblAcceptedDate.Text = DateTime.Parse(drv["AcceptedDate"].ToString()).ToShortDateString();
                        else
                            lblAcceptedDate.Text = "";
                    }
                }

                if (Session["LoanProcessAction"] != null)
                {
                    string action = Session["LoanProcessAction"].ToString();
                    if (action == "edit")
                    {
                        chk.Enabled = true;
                        rbtnYes.Enabled = true;
                        rbtnNo.Enabled = true;
                        txtAcceptedBy.Enabled = true;
                        lblSubDate.Enabled = true;
                        lblAcceptedDate.Enabled = true;
                        ddlCopyType.Enabled = true;
                    }
                    else if (action == "view")
                    {
                        chk.Enabled = false;
                        rbtnYes.Enabled = false;
                        rbtnNo.Enabled = false;
                        txtAcceptedBy.Enabled = false;
                        lblSubDate.Enabled = false;
                        lblAcceptedDate.Enabled = false;
                        ddlCopyType.Enabled = false;
                    }
                }

                if (tmpProofTypeName != drv["ProofType"].ToString())
                {
                    tmpProofTypeName = drv["ProofType"].ToString();

                    // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
                    Table tbl = e.Row.Parent as Table;

                    if (tbl != null)
                    {
                        GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                        TableCell cell = new TableCell();
                        // Span the row across all of the columns in the Gridview
                        cell.ColumnSpan = this.gvDocuments.Columns.Count;
                        cell.Width = Unit.Percentage(100);
                        cell.Style.Add("font-weight", "bold");
                        cell.Style.Add("background-color", "#c0c0c0");
                        cell.Style.Add("color", "black");

                        HtmlGenericControl span = new HtmlGenericControl("span");
                        span.InnerHtml = tmpProofTypeName;

                        cell.Controls.Add(span);
                        row.Cells.Add(cell);
                        tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Button btn = e.Row.FindControl("btnSubmitDocument") as Button;

                if (Session["LoanProcessAction"] != null)
                {
                    string action = Session["LoanProcessAction"].ToString();
                    if (action == "edit")
                    {
                        btn.Enabled = true;
                    }
                    else if (action == "view")
                    {
                        btn.Enabled = false;
                    }
                }
            }
        }

        private DataTable GetCopyType()
        {
            DataTable dt;
            dt = XMLBo.GetCopyType(path);
            return dt;
        }

        private DataTable GetProofType()
        {
            DataTable dt;
            dt = XMLBo.GetProofType(path);
            return dt;
        }

        protected void btnSubmitDocument_Click(object sender, EventArgs e)
        {
            bool blResult = false;

            foreach (GridViewRow dr in gvDocuments.Rows)
            {
                liabilitiesBo = new LiabilitiesBo();
                LoanProposalDocVo lpdVo = new LoanProposalDocVo();
                CheckBox checkBox = (CheckBox)dr.FindControl("chkBx");
                RadioButton rbtnYes = (RadioButton)dr.FindControl("rbtnAcceptedYes");
                RadioButton rbtnNo = (RadioButton)dr.FindControl("rbtnAcceptedNo");
                TextBox txtAccepted = (TextBox)dr.FindControl("txtAcceptedBy");
                DropDownList ddl = (DropDownList)dr.FindControl("ddlCopyType");
                Label lblProofName = (Label)dr.FindControl("lblProofName");

                lpdVo.ProposalDocId = Convert.ToInt32(gvDocuments.DataKeys[dr.RowIndex].Values["ProposalDocId"]);

                if (lpdVo.ProposalDocId != 0)
                {
                    // Existing Document
                    if (checkBox.Checked)
                    {
                        // UPDATE DOCUMENT DETAILS
                        if (rbtnYes.Checked)
                            lpdVo.IsAccepted = 1;
                        else
                            lpdVo.IsAccepted = 0;
                        lpdVo.DocAcceptedBy = txtAccepted.Text;
                        lpdVo.DocProofCopyTypeCode = ddl.SelectedValue;

                        if (liabilitiesBo.UpdateDocumentDetails(lpdVo, userVo.UserId))
                        {
                            blResult = true;
                        }
                    }
                    else
                    {
                        // Delete Existing Document
                        if (liabilitiesBo.DeleteDocumentDetails(lpdVo.ProposalDocId))
                        {
                            blResult = true;
                        }
                    }
                }
                else
                {
                    // New Document
                    if (checkBox.Checked)
                    {
                        // ADD DOCUMENT DETAILS
                        DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];
                        lpdVo.LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());
                        if (rbtnYes.Checked)
                            lpdVo.IsAccepted = 1;
                        else
                            lpdVo.IsAccepted = 0;
                        lpdVo.DocAcceptedBy = txtAccepted.Text;
                        lpdVo.DocProofCopyTypeCode = ddl.SelectedValue;
                        lpdVo.DocProofName = lblProofName.Text;
                        lpdVo.LiabilitiesAssociationId = Int32.Parse(ddlDocCustomerList.SelectedValue.ToString());
                        lpdVo.DocProofTypeCode = Convert.ToInt32(gvDocuments.DataKeys[dr.RowIndex].Values["ProofTypeCode"]);

                        if (liabilitiesBo.SubmitDocumentDetails(lpdVo, userVo.UserId))
                        {
                            blResult = true;
                        }
                    }
                    else
                    {
                        // Do Nothing
                    }
                }
            }

            if (blResult)
            {
                BindDocumentsGrid();
            }
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            ddlDocCustomerList.SelectedIndex = -1;
            gvDocuments.DataSource = null;
            gvDocuments.DataBind();
            Session["LoanProcessAction"] = "edit";
            dvDocDropDown.Visible = true;
            btnAddAsset.Visible = false;
            pnlAddDocuments.Visible = true;
            EnableDiableControls(true);

        }

        protected void btnAddDoc_Click(object sender, EventArgs e)
        {
            if (ddlDocCustomerList.SelectedIndex != 0 && ddlDocCustomerList.SelectedIndex != -1)
            {
                liabilitiesBo = new LiabilitiesBo();
                DataTable dtProofType = GetProofType();
                DataTable dtCopyType = GetCopyType();
                LoanProposalDocVo loanProposalDocVo = new LoanProposalDocVo();
                DataSet dsLoanProposal = (DataSet)Session[SessionContents.LoanProposalDataSet];
                int LoanProposalId = Int32.Parse(dsLoanProposal.Tables[0].Rows[0]["ALP_LoanProposalId"].ToString());

                for (int i = 0; i < gvAddDocs.Rows.Count; i++)
                {
                    if (gvAddDocs.Rows[i].RowType == DataControlRowType.DataRow && gvAddDocs.DataKeys[i].Value != "-1")
                    {
                        GridViewRow row = gvAddDocs.Rows[i];
                        string proofTypeName = ((Label)row.FindControl("lblProofType")).Text.Trim();
                        string copyTypeName = ((Label)row.FindControl("lblCopyType")).Text.Trim();

                        DataRow[] dr1 = (dtProofType.Select("XPRT_ProofType = '" + proofTypeName + "'"));
                        if (dr1.Length > 0)
                        {
                            loanProposalDocVo.DocProofTypeCode = Int32.Parse(dr1[0][0].ToString());
                        }
                        DataRow[] dr2 = (dtCopyType.Select("XPCT_ProofCopyType = '" + copyTypeName + "'"));
                        if (dr2.Length > 0)
                        {
                            loanProposalDocVo.DocProofCopyTypeCode = dr2[0][0].ToString();
                        }

                        loanProposalDocVo.LoanProposalId = LoanProposalId;
                        loanProposalDocVo.LiabilitiesAssociationId = Int32.Parse(ddlDocCustomerList.SelectedValue.ToString());
                        loanProposalDocVo.DocProofName = ((Label)row.FindControl("lblProofName")).Text.Trim();
                        loanProposalDocVo.DocSubmissionDate = DateTime.Parse(((Label)row.FindControl("lblSubDate")).Text.Trim());

                        if (((Label)row.FindControl("lblIsAccepted")).Text.Trim().ToLower() == "yes")
                        {
                            loanProposalDocVo.IsAccepted = 1;
                            loanProposalDocVo.DocAcceptedDate = DateTime.Parse(((Label)row.FindControl("lblAcceptedDate")).Text.Trim());
                            loanProposalDocVo.DocAcceptedBy = ((Label)row.FindControl("lblAcceptedBy")).Text.Trim();
                        }
                        else
                        {
                            loanProposalDocVo.IsAccepted = 0;
                        }

                        int userId = userVo.UserId;
                        bool isUpdated = false;
                        if (gvAddDocs.DataKeys[i] != null && !(gvAddDocs.DataKeys[i].Value.ToString().Contains("temp")))
                        {
                            loanProposalDocVo.ProposalDocId = Int32.Parse(gvAddDocs.DataKeys[i].Value.ToString());
                            isUpdated = liabilitiesBo.UpdateCustAdditionalDocs(loanProposalDocVo, userId);
                            ViewState["Updated"] = null;
                        }
                        else
                        {
                            isUpdated = liabilitiesBo.AddCustAdditionalDocs(loanProposalDocVo, userId);
                            ViewState["Updated"] = null;
                        }
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Select a Borrower First!');", true);
            }
        }

        private void BindAdditionalDocs(string controlFrom)
        {
            liabilitiesBo = new LiabilitiesBo();
            DataTable dt;
            string action = Session["LoanProcessAction"].ToString().ToLower();
            if (action == "edit" || action == "view")
            {
                if (ViewState["Updated"] != null)
                {
                    if (ViewState["Updated"].ToString().ToLower() == "true")
                        controlFrom = "";
                }

                if (controlFrom == "dropdownlist")
                {
                    int liabilitiesAssoId = Int32.Parse(ddlDocCustomerList.SelectedValue);
                    DataSet ds = liabilitiesBo.GetCustAdditionalDocs(liabilitiesAssoId);
                    gvAddDocs.DataSource = ds;
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dt = GetGridviewTable();
                        foreach (DataRow drSource in ds.Tables[0].Rows)
                        {
                            DataRow dr = dt.NewRow();
                            dr["DocumentId"] = drSource["DocumentId"].ToString();
                            dr["ProofTypeCode"] = drSource["ProofTypeCode"].ToString();
                            dr["ProofName"] = drSource["ProofName"].ToString();
                            dr["SubmissionDate"] = drSource["SubmissionDate"].ToString();
                            dr["IsAccepted"] = drSource["IsAccepted"].ToString();
                            dr["AcceptedBy"] = drSource["AcceptedBy"].ToString();
                            dr["AcceptedDate"] = drSource["AcceptedDate"].ToString();
                            dr["CopyType"] = drSource["CopyType"].ToString();
                            dt.Rows.Add(dr);
                        }


                        ViewState["CurrentTable"] = dt;
                        gvAddDocs.DataBind();
                    }
                    else
                    {
                        if ((DataTable)ViewState["CurrentTable"] != null)
                        {
                            gvAddDocs.DataSource = (DataTable)ViewState["CurrentTable"];
                            gvAddDocs.DataBind();
                        }
                        else
                            SetDummyRow();
                    }

                }
                else
                {
                    if ((DataTable)ViewState["CurrentTable"] != null)
                    {
                        gvAddDocs.DataSource = (DataTable)ViewState["CurrentTable"];
                        gvAddDocs.DataBind();
                    }
                    else
                        SetDummyRow();
                }
            }
            DisplayFooter();
            if (action == "edit")
                gvAddDocs.Enabled = true;
            else if (action == "view")
                gvAddDocs.Enabled = false;

        }

        private void DisplayFooter()
        {
            Button btnAdd = new Button();
            btnAdd.Text = "Add Record";
            btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            if (gvAddDocs.FooterRow != null)
            {
                gvAddDocs.FooterRow.Cells[0].Controls.Add(btnAdd);
                gvAddDocs.FooterRow.Visible = true;
            }
        }

        private void SetDummyRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = GetGridviewTable();
            dr = dt.NewRow();
            dr["DocumentId"] = "-1";
            dt.Rows.Add(dr);
            //Store the DataTable in ViewState
            //ViewState["CurrentTable"] = dt;
            gvAddDocs.DataSource = dt;
            gvAddDocs.DataBind();
            gvAddDocs.Rows[0].Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlDocCustomerList.SelectedIndex != 0 && ddlDocCustomerList.SelectedIndex != -1)
            {
                DataTable dt = null;
                if (ViewState["CurrentTable"] != null)
                    dt = (DataTable)ViewState["CurrentTable"];
                else
                    dt = GetGridviewTable();

                DataRow dr = dt.NewRow();

                TableCellCollection footerRow = gvAddDocs.FooterRow.Cells;

                if (((DropDownList)footerRow[1].FindControl("ddlProofType_Add")).SelectedIndex != 0 || ((DropDownList)footerRow[1].FindControl("ddlProofType_Add")).SelectedIndex != -1)
                    dr["ProofTypeCode"] = ((DropDownList)footerRow[1].FindControl("ddlProofType_Add")).SelectedValue;
                else
                    dr["ProofTypeCode"] = "0";

                dr["ProofName"] = ((TextBox)footerRow[2].FindControl("txtProofName_Add")).Text.Trim();
                dr["SubmissionDate"] = DateTime.Now.ToShortDateString();

                if (((RadioButton)footerRow[3].FindControl("rbtnAcceptedYes_Add")).Checked == true)
                {
                    dr["IsAccepted"] = "1";
                }
                else
                {
                    dr["IsAccepted"] = "0";
                }

                if (((RadioButton)footerRow[3].FindControl("rbtnAcceptedYes_Add")).Checked == true)
                {
                    dr["AcceptedDate"] = DateTime.Now.ToShortDateString();
                }


                dr["AcceptedBy"] = ((TextBox)footerRow[4].FindControl("txtAcceptedBy_Add")).Text.Trim();

                if (((DropDownList)footerRow[5].FindControl("ddlCopyType_Add")).SelectedIndex != 0 || ((DropDownList)footerRow[5].FindControl("ddlCopyType_Add")).SelectedIndex != -1)
                    dr["CopyType"] = ((DropDownList)footerRow[5].FindControl("ddlCopyType_Add")).SelectedValue;
                else
                    dr["CopyType"] = "0";
                dr["DocumentId"] = "temp" + dt.Rows.Count + 1;
                dt.Rows.Add(dr);
                ViewState["CurrentTable"] = dt;
                gvAddDocs.DataSource = dt;
                gvAddDocs.DataBind();
                DisplayFooter();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "alert('Select a Borrower First!');", true);
            }
        }

        private DataTable GetGridviewTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DocumentId", typeof(string)));
            dt.Columns.Add(new DataColumn("ProofTypeCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ProofName", typeof(string)));
            dt.Columns.Add(new DataColumn("SubmissionDate", typeof(string)));
            dt.Columns.Add(new DataColumn("IsAccepted", typeof(string)));
            dt.Columns.Add(new DataColumn("AcceptedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("AcceptedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("CopyType", typeof(string)));
            return dt;
        }

        protected void gvAddDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridViewRow gvr = e.Row;

                    // For View Part
                    Label lblProofName = e.Row.FindControl("lblProofName") as Label;
                    Label lblSubDate = e.Row.FindControl("lblSubDate") as Label;
                    Label lblIsAccepted = e.Row.FindControl("lblIsAccepted") as Label;
                    Label lblAcceptedDate = e.Row.FindControl("lblAcceptedDate") as Label;
                    Label lblAcceptedBy = e.Row.FindControl("lblAcceptedBy") as Label;
                    Label lblCopyType = e.Row.FindControl("lblCopyType") as Label;
                    Label lblProofType = e.Row.FindControl("lblProofType") as Label;

                    // For Edit Part
                    TextBox txtProofName = e.Row.FindControl("txtProofName_Edit") as TextBox;
                    Label lblSubDate_Edit = e.Row.FindControl("lblSubDate_Edit") as Label;
                    RadioButton rbtnAcceptedYes = e.Row.FindControl("rbtnAcceptedYes_Edit") as RadioButton;
                    RadioButton rbtnAcceptedNo = e.Row.FindControl("rbtnAcceptedNo_Edit") as RadioButton;
                    Label lblAcceptedDate_Edit = e.Row.FindControl("lblAcceptedDate_Edit") as Label;
                    TextBox txtAcceptedBy = e.Row.FindControl("txtAcceptedBy_Edit") as TextBox;
                    DropDownList ddlCopyType = e.Row.FindControl("ddlCopyType_Edit") as DropDownList;
                    DropDownList ddlProofType = e.Row.FindControl("ddlProofType_Edit") as DropDownList;

                    if (ddlCopyType != null)
                    {
                        ddlCopyType.DataSource = GetCopyType();
                        ddlCopyType.DataTextField = "XPCT_ProofCopyType";
                        ddlCopyType.DataValueField = "XPCT_ProofCopyTypeCode";
                        ddlCopyType.DataBind();
                        ddlCopyType.Items.Insert(0, new ListItem("Select Copy Type", "Select Copy Type"));
                    }

                    if (ddlProofType != null)
                    {
                        ddlProofType.DataSource = GetProofType();
                        ddlProofType.DataTextField = "XPRT_ProofType";
                        ddlProofType.DataValueField = "XPRT_ProofTypeCode";
                        ddlProofType.DataBind();
                        ddlProofType.Items.Insert(0, new ListItem("Select Proof Type", "Select Proof Type"));
                    }

                    if (lblProofName != null)
                    {
                        lblProofName.Text = dt.Rows[gvr.RowIndex]["ProofName"].ToString();
                    }
                    if (lblSubDate != null)
                    {
                        if (dt.Rows[gvr.RowIndex]["SubmissionDate"].ToString() != "")
                            lblSubDate.Text = DateTime.Parse(dt.Rows[gvr.RowIndex]["SubmissionDate"].ToString()).ToShortDateString();
                        else
                            lblSubDate.Text = "";
                    }
                    if (lblIsAccepted != null)
                    {
                        if (dt.Rows[gvr.RowIndex]["IsAccepted"].ToString() == "1")
                        {
                            lblIsAccepted.Text = "Yes";
                        }
                        else
                        {
                            lblIsAccepted.Text = "No";
                        }
                    }
                    if (lblAcceptedDate != null)
                    {
                        if (dt.Rows[gvr.RowIndex]["AcceptedDate"].ToString() != "")
                            lblAcceptedDate.Text = dt.Rows[gvr.RowIndex]["AcceptedDate"].ToString();
                        else
                            lblAcceptedDate.Text = "";
                    }
                    if (lblAcceptedBy != null)
                    {
                        lblAcceptedBy.Text = dt.Rows[gvr.RowIndex]["AcceptedBy"].ToString();
                    }
                    if (lblCopyType != null)
                    {
                        string copyTypeCode = dt.Rows[gvr.RowIndex]["CopyType"].ToString();
                        if (copyTypeCode != "0" && copyTypeCode != "")
                        {
                            string CopyTypeName = XMLBo.GetCopyTypeName(path, copyTypeCode);
                            lblCopyType.Text = CopyTypeName;
                        }
                        else
                        {
                            lblCopyType.Text = "NA";
                        }
                    }
                    if (lblProofType != null)
                    {
                        string proofTypeCode = dt.Rows[gvr.RowIndex]["ProofTypeCode"].ToString();
                        if (proofTypeCode != "0" && proofTypeCode != "")
                        {
                            string ProofTypeName = XMLBo.GetProofTypeName(path, proofTypeCode);
                            lblProofType.Text = ProofTypeName;
                        }
                        else
                        {
                            lblProofType.Text = "NA";
                        }
                    }


                    //////////////////////////////////////////////////////////////////////////////
                    if (txtProofName != null)
                    {
                        txtProofName.Text = dt.Rows[gvr.RowIndex]["ProofName"].ToString();
                    }
                    if (lblSubDate_Edit != null)
                    {
                        lblSubDate_Edit.Text = DateTime.Parse(dt.Rows[gvr.RowIndex]["SubmissionDate"].ToString()).ToShortDateString();
                    }
                    if (rbtnAcceptedYes != null && rbtnAcceptedNo != null)
                    {
                        if (dt.Rows[gvr.RowIndex]["IsAccepted"].ToString() == "0")
                        {
                            rbtnAcceptedNo.Checked = true;
                        }
                        else if (dt.Rows[gvr.RowIndex]["IsAccepted"].ToString() == "1")
                        {
                            rbtnAcceptedYes.Checked = true;
                        }
                    }
                    if (lblAcceptedDate_Edit != null)
                    {
                        if (dt.Rows[gvr.RowIndex]["AcceptedDate"].ToString() != "")
                            lblAcceptedDate_Edit.Text = DateTime.Parse(dt.Rows[gvr.RowIndex]["AcceptedDate"].ToString()).ToShortDateString();
                        else
                            lblAcceptedDate_Edit.Text = "";
                    }
                    if (txtAcceptedBy != null)
                    {
                        txtAcceptedBy.Text = dt.Rows[gvr.RowIndex]["AcceptedBy"].ToString();
                    }
                    if (ddlCopyType != null)
                    {
                        string copyTypeCode = dt.Rows[gvr.RowIndex]["CopyType"].ToString();
                        if (copyTypeCode != "0")
                        {
                            ddlCopyType.SelectedValue = copyTypeCode;
                        }
                    }

                    if (ddlProofType != null)
                    {
                        string proofTypeCode = dt.Rows[gvr.RowIndex]["ProofTypeCode"].ToString();
                        if (proofTypeCode != "0")
                        {
                            ddlProofType.SelectedValue = proofTypeCode;
                        }
                    }
                }
                else
                {
                    DropDownList ddlCopyType = e.Row.FindControl("ddlCopyType_Add") as DropDownList;
                    DropDownList ddlProofType = e.Row.FindControl("ddlProofType_Add") as DropDownList;

                    if (ddlCopyType != null)
                    {
                        ddlCopyType.DataSource = GetCopyType();
                        ddlCopyType.DataTextField = "XPCT_ProofCopyType";
                        ddlCopyType.DataValueField = "XPCT_ProofCopyTypeCode";
                        ddlCopyType.DataBind();
                        ddlCopyType.Items.Insert(0, new ListItem("Select Copy Type", "Select Copy Type"));
                    }

                    if (ddlProofType != null)
                    {
                        ddlProofType.DataSource = GetProofType();
                        ddlProofType.DataTextField = "XPRT_ProofType";
                        ddlProofType.DataValueField = "XPRT_ProofTypeCode";
                        ddlProofType.DataBind();
                        ddlProofType.Items.Insert(0, new ListItem("Select Proof Type", "Select Proof Type"));
                    }
                }
            }
            else
            {
                DropDownList ddlCopyType = e.Row.FindControl("ddlCopyType_Add") as DropDownList;
                DropDownList ddlProofType = e.Row.FindControl("ddlProofType_Add") as DropDownList;

                if (ddlCopyType != null)
                {
                    ddlCopyType.DataSource = GetCopyType();
                    ddlCopyType.DataTextField = "XPCT_ProofCopyType";
                    ddlCopyType.DataValueField = "XPCT_ProofCopyTypeCode";
                    ddlCopyType.DataBind();
                    ddlCopyType.Items.Insert(0, new ListItem("Select Copy Type", "Select Copy Type"));
                }

                if (ddlProofType != null)
                {
                    ddlProofType.DataSource = GetProofType();
                    ddlProofType.DataTextField = "XPRT_ProofType";
                    ddlProofType.DataValueField = "XPRT_ProofTypeCode";
                    ddlProofType.DataBind();
                    ddlProofType.Items.Insert(0, new ListItem("Select Proof Type", "Select Proof Type"));
                }
            }
        }

        protected void gvAddDocs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAddDocs.EditIndex = -1;
            gvAddDocs.DataSource = (DataTable)ViewState["CurrentTable"];
            gvAddDocs.DataBind();
            DisplayFooter();
        }

        protected void gvAddDocs_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAddDocs.EditIndex = e.NewEditIndex;
            gvAddDocs.DataSource = (DataTable)ViewState["CurrentTable"];
            gvAddDocs.DataBind();
        }

        protected void gvAddDocs_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            string editKey = gvAddDocs.DataKeys[e.RowIndex].Value.ToString();
            for (int i = 0; i < gvAddDocs.Rows.Count; i++)
            {
                if (dt.Rows[i]["DocumentId"].ToString() == editKey)
                {
                    TableCellCollection row = gvAddDocs.Rows[i].Cells;

                    if (((DropDownList)row[1].FindControl("ddlProofType_Edit")).SelectedIndex != 0)
                        dt.Rows[i]["ProofTypeCode"] = ((DropDownList)row[6].FindControl("ddlProofType_Edit")).SelectedValue;
                    else
                        dt.Rows[i]["ProofTypeCode"] = "0";

                    dt.Rows[i]["ProofName"] = ((TextBox)row[2].FindControl("txtProofName_Edit")).Text;
                    dt.Rows[i]["SubmissionDate"] = ((Label)row[3].FindControl("lblSubDate_Edit")).Text;

                    if (((RadioButton)row[4].FindControl("rbtnAcceptedYes_Edit")).Checked == true)
                    {
                        dt.Rows[i]["IsAccepted"] = "1";
                        dt.Rows[i]["AcceptedDate"] = DateTime.Now.ToShortDateString();
                        dt.Rows[i]["AcceptedBy"] = ((TextBox)row[6].FindControl("txtAcceptedBy_Edit")).Text;
                    }
                    else
                    {
                        dt.Rows[i]["IsAccepted"] = "0";
                    }



                    if (((DropDownList)row[7].FindControl("ddlCopyType_Edit")).SelectedIndex != 0)
                        dt.Rows[i]["CopyType"] = ((DropDownList)row[6].FindControl("ddlCopyType_Edit")).SelectedValue;
                    else
                        dt.Rows[i]["CopyType"] = "0";

                    dt.Rows[i]["DocumentId"] = editKey;
                }
            }
            ViewState["CurrentTable"] = dt;
            ViewState["Updated"] = true;
            gvAddDocs.EditIndex = -1;
            gvAddDocs.DataSource = (DataTable)ViewState["CurrentTable"];
            gvAddDocs.DataBind();
            DisplayFooter();
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('LoanTrackingGrid','login');", true);
        }
    }
}