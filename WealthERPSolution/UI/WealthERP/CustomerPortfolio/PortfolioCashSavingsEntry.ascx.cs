using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using WealthERP.General;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using BoCommon;
using WealthERP.Base;


namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioCashSavingsEntry : System.Web.UI.UserControl
    {
        CustomerAccountBo customerAccountsBo = new CustomerAccountBo();
        CashAndSavingsBo customerCashSavingsPortfolioBo = new CashAndSavingsBo();
        CashAndSavingsVo customerCashSavingsPortfolioVo = new CashAndSavingsVo();
        CashAndSavingsVo cashSavingsVo = new CashAndSavingsVo();
        CustomerAccountsVo customerAccountVo;
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        AssetBo assetBo = new AssetBo();
        int customerId;
        string assetGroupCode = "CS";
        string assetGroupName = "Cash & Savings";
        int userId;
        string path;
        string command;
        int CashSavingsPortfolioId;
        static int portfolioId = 0;
        PortfolioBo portfolioBo = new PortfolioBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                customerAccountVo = (CustomerAccountsVo)Session["customerAccountVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());

                command = Session["action"].ToString().Trim();

                if (command == "CS")
                {
                    LoadAccountDetails();
                    SetFields();
                    LoadFields("Entry");
                    lnkBtnBack.Visible = false;
                }
                else
                {
                    CashSavingsPortfolioId = Int32.Parse(Session["CashSavingsPortfolioId"].ToString());
                    cashSavingsVo = customerCashSavingsPortfolioBo.GetSpecificCashSavings(CashSavingsPortfolioId, customerVo.CustomerId);
                    customerAccountVo = customerAccountsBo.GetCashAndSavingsAccount(Convert.ToInt32(cashSavingsVo.AccountId));
                    if (command == "EditCS")
                    {
                        LoadAccountDetails();
                        SetFields();
                        LoadFields("Edit");
                        lnkEdit.Visible = false;
                        btnSaveChanges.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    else if (command == "ViewCS")
                    {
                        //SetViewFields();
                        LoadAccountDetails();
                        SetFields();
                        LoadFields("View");
                        lnkEdit.Visible = true;
                        btnSaveChanges.Visible = false;
                        btnSubmit.Visible = false;
                    }
                    lnkBtnBack.Visible = true;
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
                FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerAccountVo;
                objects[3] = path;
                objects[4] = command;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }
        //private void BindPortfolioDropDown()
        //{
        //    DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
        //    ddlPortfolio.DataSource = ds;
        //    ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
        //    ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
        //    ddlPortfolio.DataBind();
        //    ddlPortfolio.Items.Insert(0, "Select the Portfolio");

        //    ddlPortfolio.SelectedValue = portfolioId.ToString();

        //}
        //protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
        //    Session[SessionContents.PortfolioId] = portfolioId;

        //}
        private void LoadModeOfHolding()
        {
            try
            {
                DataTable dtModeOfHolding = XMLBo.GetModeOfHolding(path);
                ddlModeOfHolding.DataSource = dtModeOfHolding;
                ddlModeOfHolding.DataTextField = "ModeOfHolding";
                ddlModeOfHolding.DataValueField = "ModeOfHoldingCode";
                ddlModeOfHolding.DataBind();
                ddlModeOfHolding.Items.Insert(0, new ListItem("Select Mode of Holding", "Select Mode of Holding"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:LoadModeOfHolding()");
                object[] objects = new object[1];
                objects[3] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        //private void LoadEditableFields()
        //{
        //    CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
        //    CashAndSavingsVo cashSavingsVo = new CashAndSavingsVo();
        //    try
        //    {
        //        lblHeader.Text = "Cash and Savings Edit Form";

        //        btnSaveChanges.Visible = true;
        //        btnSubmit.Visible = false;
        //        lnkEdit.Visible = false;

        //        CashSavingsPortfolioId = Int32.Parse(Session["CashSavingsPortfolioId"].ToString());
        //        cashSavingsVo = customerCashSavingsPortfolioBo.GetSpecificCashSavings(CashSavingsPortfolioId, customerVo.CustomerId);
        //        customerAccountVo = customerAccountsBo.GetCashAndSavingsAccount(Convert.ToInt32(cashSavingsVo.AccountId));

        //        // Set the Account Details
        //        lblAssetGroup.Text = "Cash and Savings";
        //        lblInsCategory.Text = customerAccountVo.AssetCategoryName.ToString();

        //        // Hiding the Labels used for View
        //        lblAccId.Visible = false;
        //        lblAccWith.Visible = false;
        //        lblModeOfHolding.Visible = false;
        //        //lblOpeningDate.Visible = false;

        //        txtAccountNumber.Text = customerAccountVo.AccountNum.ToString();
        //        //trAccountNumber.Visible = true;
        //        txtAccountNumber.Enabled = true;


        //        txtAccountWith.Text = customerAccountVo.BankName.ToString();
        //        txtAccountWith.Visible = true;
        //        txtAccountWith.Enabled = true;

        //        LoadModeOfHolding();
        //        ddlModeOfHolding.SelectedValue = customerAccountVo.ModeOfHolding.ToString().Trim();
        //        ddlModeOfHolding.Visible = true;
        //        ddlModeOfHolding.Enabled = true;

        //        // Load Fields for Current Account
        //        if (customerAccountVo.AssetCategory.ToString().Trim() == "CSCA")
        //        {
        //            // Deposit Amount
        //            trDepositAmount.Visible = true;
        //            txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
        //            txtDepositAmount.Enabled = true;

        //            // Remarks
        //            trRemarks.Visible = true;
        //            if (cashSavingsVo.Remarks != null)
        //                txtRemarks.Text = cashSavingsVo.Remarks.ToString();
        //            txtRemarks.Enabled = true;

        //            trAmountOfLoan.Visible = false;
        //            trName.Visible = false;
        //            trInterestPayable.Visible = false;
        //            trDepositDate.Visible = false;
        //            trInterestAmount.Visible = false;
        //            trInterestPayable.Visible = false;
        //        }
        //        else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSSA")
        //        {
        //            trName.Visible = false;
        //            trDepositDate.Visible = false;
        //            trAmountOfLoan.Visible = false;

        //            // Deposit Amount
        //            trDepositAmount.Visible = true;
        //            txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
        //            txtDepositAmount.Enabled = true;

        //            //  Interest Amount Details
        //            if (customerCashSavingsPortfolioVo.IsInterestAccumalated == 1)
        //            {
        //                trInterestAmount.Visible = true;
        //                txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
        //            }
        //            else
        //            {
        //                trInterestAmount.Visible = false;
        //                txtInterestAmountAccum.Visible = false;
        //            }

        //            LoadFrequencyCode(path);
        //            trInterestPayable.Visible = true;
        //            ddlIPFrequency.SelectedValue = cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim();
        //            ddlIPFrequency.Enabled = true;

        //            trName.Visible = true;
        //            trRemarks.Visible = true;
        //            if (cashSavingsVo.Remarks != null)
        //                txtRemarks.Text = cashSavingsVo.Remarks.ToString();
        //            txtRemarks.Enabled = true;
        //        }
        //        else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSLA")
        //        {

        //            trName.Visible = true;
        //            txtName.Text = cashSavingsVo.Name.ToString();
        //            txtName.Enabled = true;

        //            trDepositDate.Visible = true;
        //            if (cashSavingsVo.DepositDate != DateTime.MinValue)
        //                txtDepositDate.Text = cashSavingsVo.DepositDate.ToShortDateString().ToString();
        //            txtDepositDate.Enabled = true;

        //            trAmountOfLoan.Visible = true;
        //            if (cashSavingsVo.DepositAmount != null)
        //                txtAmountOfLoan.Text = cashSavingsVo.DepositAmount.ToString();
        //            txtAmountOfLoan.Enabled = true;


        //            trDepositAmount.Visible = true;
        //            txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
        //            txtDepositAmount.Enabled = true;


        //            trInterestAmount.Visible = true;
        //            trInterestAmount.Visible = true;
        //            txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
        //            txtInterestAmountAccum.Enabled = true;
        //            // }


        //            trInterestPayable.Visible = true;
        //            trRemarks.Visible = true;
        //            LoadFrequencyCode(path);



        //            LoadFrequencyCode(path);
        //            ddlIPFrequency.SelectedValue = cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim();
        //            ddlIPFrequency.Enabled = true;

        //            // Remarks
        //            txtRemarks.Visible = true;
        //            if (cashSavingsVo.Remarks != null)
        //                txtRemarks.Text = cashSavingsVo.Remarks.ToString();
        //            txtRemarks.Enabled = true;
        //        }
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:LoadEditableFields()");
        //        object[] objects = new object[3];
        //        objects[0] = cashSavingsVo;
        //        objects[1] = CashSavingsPortfolioId;
        //        objects[2] = customerAccountVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //private void SetViewFields()
        //{
        //    CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();

        //    try
        //    {
        //        lblHeader.Text = "Cash and Savings View Form";



        //        // Set the Account Details
        //        lblAccId.Text = customerAccountVo.AccountNum.ToString();
        //        lblAccWith.Text = customerAccountVo.BankName.ToString();
        //        lblModeOfHolding.Text = XMLBo.GetModeOfHoldingName(path, customerAccountVo.ModeOfHolding.ToString());
        //        //lblOpeningDate.Text = customerAccountVo.AccountOpeningDate.ToShortDateString();
        //        lblAssetGroup.Text = "Cash and Savings";
        //        lblInsCategory.Text = customerAccountVo.AssetCategoryName.ToString();

        //        trAccountNo.Visible = false;

        //        trAccWithBank.Visible = false;

        //        ddlModeOfHolding.Visible = false;

        //        // Load Fields for Current Account
        //        if (customerAccountVo.AssetCategory.ToString().Trim() == "CSCA")
        //        {
        //            trAccountNo.Visible = true;
        //            trAccWithBank.Visible = true;
        //            txtAccountNumber.Visible = false;
        //            txtAccountWith.Visible = false;

        //            // Deposit Amount
        //            trDepositAmount.Visible = true;
        //            txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
        //            txtDepositAmount.Enabled = false;

        //            // Remarks
        //            trRemarks.Visible = true;
        //            if (cashSavingsVo.Remarks != null)
        //                txtRemarks.Text = cashSavingsVo.Remarks.ToString();
        //            txtRemarks.Enabled = false;

        //            // Deposit Date
        //            trDepositDate.Visible = false;
        //            trAmountOfLoan.Visible = false;
        //            trInterestAmount.Visible = false;
        //            trName.Visible = false;
        //            trInterestPayable.Visible = false;
        //        }
        //        else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSSA")
        //        {
        //            trAccountNo.Visible = true;
        //            trAccWithBank.Visible = true;
        //            txtAccountNumber.Visible = false;
        //            txtAccountWith.Visible = false;
        //            //txtAccountOpeningDate.Visible = false;

        //            trAmountOfLoan.Visible = false;

        //            // Deposit Amount
        //            trDepositDate.Visible = true;
        //            txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
        //            txtDepositAmount.Enabled = false;

        //            //  Interest Amount Details
        //            if (customerCashSavingsPortfolioVo.IsInterestAccumalated == 1)
        //            {
        //                trInterestAmount.Visible = true;
        //                txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
        //            }
        //            else
        //            {
        //                trInterestAmount.Visible = false;
        //            }

        //            LoadFrequencyCode(path);

        //            trInterestPayable.Visible = true;
        //            ddlIPFrequency.SelectedValue = cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim();
        //            ddlIPFrequency.Enabled = false;

        //            trName.Visible = false;
        //            if (cashSavingsVo.Remarks != null)
        //                txtRemarks.Text = cashSavingsVo.Remarks.ToString();
        //            txtRemarks.Enabled = false;
        //        }
        //        else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSLA")
        //        {
        //            // Name of the Borrower
        //            trName.Visible = true;
        //            txtName.Text = cashSavingsVo.Name.ToString();
        //            txtName.Enabled = false;

        //            // Deposit Date
        //            trDepositDate.Visible = true;
        //            if (cashSavingsVo.DepositDate != DateTime.MinValue)
        //                txtDepositDate.Text = cashSavingsVo.DepositDate.ToShortDateString().ToString();

        //            txtDepositDate.Enabled = false;

        //            // Amount of Loan
        //            trAmountOfLoan.Visible = false;

        //            // Deposit Amount
        //            trDepositAmount.Visible = true;
        //            txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
        //            txtDepositAmount.Enabled = false;

        //            // Interest Amount Details
        //            trInterestAmount.Visible = true;

        //            //if (cashSavingsVo.IsInterestAccumalated == 0)
        //            //{
        //            //    trInterestAmount.Visible = false;
        //            //}
        //            //else
        //            //{
        //            trInterestAmount.Visible = true;
        //            txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
        //            txtInterestAmountAccum.Enabled = false;
        //            //}

        //            // Frequency Interest
        //            trInterestPayable.Visible = true;
        //            LoadFrequencyCode(path);
        //            ddlIPFrequency.SelectedValue = cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim();
        //            ddlIPFrequency.Enabled = false;

        //            // Remarks
        //            trRemarks.Visible = true;
        //            if (cashSavingsVo.Remarks != null)
        //                txtRemarks.Text = cashSavingsVo.Remarks.ToString();
        //            txtRemarks.Enabled = false;
        //        }
        //        btnSaveChanges.Visible = false;
        //        btnSubmit.Visible = false;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:SetViewFields()");
        //        object[] objects = new object[3];
        //        objects[0] = cashSavingsVo;
        //        objects[1] = CashSavingsPortfolioId;
        //        objects[2] = customerAccountVo;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        public void LoadAccountDetails()
        {
            lblAccId.Text = customerAccountVo.AccountNum.ToString();
            lblAccWith.Text = customerAccountVo.BankName.ToString();
            lblModeOfHolding.Text = XMLBo.GetModeOfHoldingName(path, customerAccountVo.ModeOfHolding.ToString());
            //lblOpeningDate.Text = customerAccountVo.AccountOpeningDate.ToShortDateString();
            lblAssetGroup.Text = "Cash and Savings";
            lblInsCategory.Text = customerAccountVo.AssetCategoryName.ToString();
        }

        public void SetFields()
        {
            trAccWithBank.Visible = false;
            //trAccOpenDate.Visible = false;
            trAccountNo.Visible = false;

            trName.Visible = false;
            trRemarks.Visible = false;
            trInterestPayable.Visible = false;
            trInterestAmount.Visible = false;
            trDepositDate.Visible = false;
            trDepositAmount.Visible = false;
            trAmountOfLoan.Visible = false;
        }

        public void LoadFields(string action)
        {
            try
            {
                ddlModeOfHolding.Visible = false;
                btnSaveChanges.Visible = false;
                lnkEdit.Visible = false;
                trDepositDate.Visible = false;

                if (customerAccountVo.AssetCategory.ToString().Trim() == "CSCA")
                {
                    if (action == "Entry")
                    {
                        trAccountNo.Visible = true;
                        trAccWithBank.Visible = true;
                        txtAccountNumber.Visible = false;
                        txtAccountWith.Visible = false;

                        trDepositAmount.Visible = true;
                        trRemarks.Visible = true;
                        trAmountOfLoan.Visible = false;
                        trDepositDate.Visible = false;
                        trInterestPayable.Visible = false;
                        trInterestAmount.Visible = false;
                        trName.Visible = false;
                    }
                    else if (action == "Edit")
                    {
                        trDepositDate.Visible = false;
                        trAmountOfLoan.Visible = false;
                        trInterestAmount.Visible = false;
                        trName.Visible = false;
                        trInterestPayable.Visible = false;

                        trAccountNo.Visible = true;
                        trAccWithBank.Visible = true;
                        txtAccountNumber.Visible = false;
                        txtAccountWith.Visible = false;

                        // Deposit Amount
                        trDepositAmount.Visible = true;
                        txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
                        txtDepositAmount.Enabled = true;

                        // Remarks
                        trRemarks.Visible = true;
                        if (cashSavingsVo.Remarks != null)
                            txtRemarks.Text = cashSavingsVo.Remarks.ToString();
                        txtRemarks.Enabled = true;

                        // Deposit Date
                      
                    }
                    else if (action == "View")
                    {
                        trAccountNo.Visible = true;
                        trAccWithBank.Visible = true;
                        txtAccountNumber.Visible = false;
                        txtAccountWith.Visible = false;

                        // Deposit Amount
                        trDepositAmount.Visible = true;
                        txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
                        txtDepositAmount.Enabled = false;

                        // Remarks
                        trRemarks.Visible = true;
                        if (cashSavingsVo.Remarks != null)
                            txtRemarks.Text = cashSavingsVo.Remarks.ToString();
                        txtRemarks.Enabled = false;

                        // Deposit Date
                        trDepositDate.Visible = false;
                        trAmountOfLoan.Visible = false;
                        trInterestAmount.Visible = false;
                        trName.Visible = false;
                        trInterestPayable.Visible = false;
                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSSA")
                {
                    if (action == "Entry")
                    {
                        trAccountNo.Visible = true;
                        trAccWithBank.Visible = true;
                        txtAccountNumber.Visible = false;
                        txtAccountWith.Visible = false;

                        trDepositAmount.Visible = true;
                        trRemarks.Visible = true;
                        trAmountOfLoan.Visible = false;
                        trDepositDate.Visible = false;
                        trInterestPayable.Visible = false;
                        trInterestAmount.Visible = false;
                        trName.Visible = false;
                    }
                    else if (action == "View")
                    {
                        trDepositDate.Visible = false;
                        trAmountOfLoan.Visible = false;
                        trInterestAmount.Visible = false;
                        trName.Visible = false;
                        trInterestPayable.Visible = false;

                        trAccountNo.Visible = true;
                        trAccWithBank.Visible = true;
                        txtAccountNumber.Visible = false;
                        txtAccountWith.Visible = false;

                        // Deposit Amount
                        trDepositAmount.Visible = true;
                        txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
                        txtDepositAmount.Enabled = false;

                        // Remarks
                        trRemarks.Visible = true;
                        if (cashSavingsVo.Remarks != null)
                            txtRemarks.Text = cashSavingsVo.Remarks.ToString();
                        txtRemarks.Enabled = false;
                    }
                    else if (action == "Edit")
                    {
                        trDepositDate.Visible = false;
                        trAmountOfLoan.Visible = false;
                        trInterestAmount.Visible = false;
                        trName.Visible = false;
                        trInterestPayable.Visible = false;

                        trAccountNo.Visible = true;
                        trAccWithBank.Visible = true;
                        txtAccountNumber.Visible = false;
                        txtAccountWith.Visible = false;

                        // Deposit Amount
                        trDepositAmount.Visible = true;
                        txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
                        txtDepositAmount.Enabled = true;

                        // Remarks
                        trRemarks.Visible = true;
                        if (cashSavingsVo.Remarks != null)
                            txtRemarks.Text = cashSavingsVo.Remarks.ToString();
                        txtRemarks.Enabled = true;

                    }
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSLA")
                {
                    if (action == "Entry")
                    {
                        trAccountNo.Visible = true;
                        trAccWithBank.Visible = true;
                        txtAccountNumber.Visible = false;
                        txtAccountWith.Visible = false;
                        trDepositAmount.Visible = false;
                        trRemarks.Visible = true;
                        trAmountOfLoan.Visible = true;
                        trDepositDate.Visible = true;
                        trInterestPayable.Visible = true;
                        trInterestAmount.Visible = true;
                        trName.Visible = true;
                        LoadFrequencyCode(path);
                    }
                    else if (action == "View")
                    {
                        trName.Visible = true;
                        txtName.Text = cashSavingsVo.Name.ToString();
                        txtName.Enabled = false;

                        trDepositDate.Visible = true;
                        if (cashSavingsVo.DepositDate != DateTime.MinValue)
                            txtDepositDate.Text = cashSavingsVo.DepositDate.ToShortDateString().ToString();
                        txtDepositDate.Enabled = false;

                        trAmountOfLoan.Visible = true;
                        if (cashSavingsVo.DepositAmount != null)
                            txtAmountOfLoan.Text = cashSavingsVo.DepositAmount.ToString();
                        txtAmountOfLoan.Enabled = false;


                        //trDepositAmount.Visible = true;
                        //txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
                        //txtDepositAmount.Enabled = false;


                        trInterestAmount.Visible = true;
                        trInterestAmount.Visible = true;
                        //txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
                        //txtInterestAmountAccum.Enabled = false;
                        // }


                        trInterestPayable.Visible = true;
                        trRemarks.Visible = true;
                        LoadFrequencyCode(path);



                        LoadFrequencyCode(path);
                        if (cashSavingsVo.InterestPayoutFrequencyCode != null)
                        {
                            ddlIPFrequency.SelectedValue = cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim();
                            ddlIPFrequency.Enabled = false;
                            if (cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim() != "AM")
                            {
                                txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntPaidOut.ToString();
                            }
                            else
                            {
                                txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
                            }
                            txtInterestAmountAccum.Enabled = false;
                        }

                        // Remarks
                        txtRemarks.Visible = true;
                        if (cashSavingsVo.Remarks != null)
                            txtRemarks.Text = cashSavingsVo.Remarks.ToString();
                        txtRemarks.Enabled = false;
                    }
                    else if (action == "Edit")
                    {
                        trName.Visible = true;
                        txtName.Text = cashSavingsVo.Name.ToString();
                        txtName.Enabled = true;

                        trDepositDate.Visible = true;
                        if (cashSavingsVo.DepositDate != DateTime.MinValue)
                            txtDepositDate.Text = cashSavingsVo.DepositDate.ToShortDateString().ToString();
                        txtDepositDate.Enabled = true;

                        trAmountOfLoan.Visible = true;
                        if (cashSavingsVo.DepositAmount != null)
                            txtAmountOfLoan.Text = cashSavingsVo.DepositAmount.ToString();
                        txtAmountOfLoan.Enabled = true;


                        //trDepositAmount.Visible = true;
                        //txtDepositAmount.Text = cashSavingsVo.DepositAmount.ToString();
                        //txtDepositAmount.Enabled = false;


                        trInterestAmount.Visible = true;
                       
                        //txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
                       
                        // }


                        trInterestPayable.Visible = true;
                        trRemarks.Visible = true;
                        LoadFrequencyCode(path);



                        LoadFrequencyCode(path);
                        if (cashSavingsVo.InterestPayoutFrequencyCode != null)
                        {
                            ddlIPFrequency.SelectedValue = cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim();
                            ddlIPFrequency.Enabled = true;
                            if (cashSavingsVo.InterestPayoutFrequencyCode.ToString().Trim() != "AM")
                            {
                                txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntPaidOut.ToString();
                            }
                            else
                            {
                                txtInterestAmountAccum.Text = cashSavingsVo.InterestAmntAccumulated.ToString();
                            }
                            txtInterestAmountAccum.Enabled = true;
                        }

                        // Remarks
                        txtRemarks.Visible = true;
                        if (cashSavingsVo.Remarks != null)
                            txtRemarks.Text = cashSavingsVo.Remarks.ToString();
                        txtRemarks.Enabled = true;
                    }
                }
                //else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSSA")
                //{
                //    trAccWithBank.Visible = true;
                //    trAccOpenDate.Visible = true;
                //    trAccountNo.Visible = true;
                //    txtAccountNumber.Visible = false;
                //    txtAccountWith.Visible = false;
                //    txtAccountOpeningDate.Visible = false;

                //    trDepositAmount.Visible = true;
                //    trInterestAmount.Visible = true;
                //    trRemarks.Visible = true;
                //    trInterestPayable.Visible = true;
                //    LoadFrequencyCode(path);

                //    lblInterestAmount.Text = "Interest Amount:";
                //    lblInterestPayableFrequency.Text = "Interest Credit Frequency:";
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:LoadFields()");
                object[] objects = new object[1];

                objects[0] = customerAccountVo;
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
                ddlIPFrequency.DataSource = dt;
                ddlIPFrequency.DataTextField = dt.Columns["Frequency"].ToString();
                ddlIPFrequency.DataValueField = dt.Columns["FrequencyCode"].ToString();
                ddlIPFrequency.DataBind();
                ddlIPFrequency.Items.Insert(0, new ListItem("Select a Frequency", "Select a Frequency"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:LoadFrequencyCode()");
                object[] objects = new object[1];

                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnAddDetails_Click(object sender, EventArgs e)
        {
            CashAndSavingsVo customerCashSavingsPortfolioVo = new CashAndSavingsVo();
            CashAndSavingsBo customerCashSavingsPortfolioBo = new CashAndSavingsBo();
            try
            {
                // Get Session Values
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;

                customerCashSavingsPortfolioVo.AccountId = customerAccountVo.AccountId;
                customerCashSavingsPortfolioVo.AssetGroupCode = assetGroupCode;
                customerCashSavingsPortfolioVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory;
                customerCashSavingsPortfolioVo.CustomerId = customerVo.CustomerId;

                if (customerAccountVo.AssetCategory.ToString().Trim() == "CSCA")
                {
                    customerCashSavingsPortfolioVo.DepositAmount = float.Parse(txtDepositAmount.Text);
                    customerCashSavingsPortfolioVo.DepositDate = DateTime.MinValue;
                    customerCashSavingsPortfolioVo.InterestAmntAccumulated = 0;
                    customerCashSavingsPortfolioVo.InterestAmntPaidOut = 0;
                    customerCashSavingsPortfolioVo.InterestBasis = "";
                    customerCashSavingsPortfolioVo.InterestRate = 0;
                    customerCashSavingsPortfolioVo.IsInterestAccumalated = 0;
                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSSA")
                {
                    customerCashSavingsPortfolioVo.DepositAmount = float.Parse(txtDepositAmount.Text);
                    customerCashSavingsPortfolioVo.DepositDate = DateTime.MinValue;
                    customerCashSavingsPortfolioVo.InstrumentCategory = "";
                    if (ddlIPFrequency.SelectedValue == "AM")
                    {
                        customerCashSavingsPortfolioVo.IsInterestAccumalated = 1;
                        if (txtInterestAmountAccum.Text != "")
                            customerCashSavingsPortfolioVo.InterestAmntAccumulated = float.Parse(txtInterestAmountAccum.Text.ToString());
                        else
                            customerCashSavingsPortfolioVo.InterestAmntAccumulated = 0;
                    }
                    else
                    {
                        customerCashSavingsPortfolioVo.IsInterestAccumalated = 0;
                        if (txtInterestAmountAccum.Text != "")
                            customerCashSavingsPortfolioVo.InterestAmntPaidOut = float.Parse(txtInterestAmountAccum.Text.ToString());
                        else
                            customerCashSavingsPortfolioVo.InterestAmntPaidOut = 0;
                    }

                  //  customerCashSavingsPortfolioVo.InterestPayoutFrequencyCode = ddlIPFrequency.SelectedItem.Value.ToString();

                }
                else if (customerAccountVo.AssetCategory.ToString().Trim() == "CSLA")
                {
                    customerCashSavingsPortfolioVo.Name = txtName.Text;
                    if (txtDepositDate.Text != "")
                        customerCashSavingsPortfolioVo.DepositDate = DateTime.Parse(txtDepositDate.Text.Trim());
                    else
                        customerCashSavingsPortfolioVo.DepositDate = DateTime.MinValue;
                    customerCashSavingsPortfolioVo.DepositAmount = float.Parse(txtAmountOfLoan.Text.ToString());
                    customerCashSavingsPortfolioVo.InterestPayoutFrequencyCode = ddlIPFrequency.SelectedItem.Value.ToString();
                    if (ddlIPFrequency.SelectedValue == "AM")
                    {
                        customerCashSavingsPortfolioVo.IsInterestAccumalated = 1;
                        if (txtInterestAmountAccum.Text != "")
                            customerCashSavingsPortfolioVo.InterestAmntAccumulated = float.Parse(txtInterestAmountAccum.Text.ToString());
                        else
                            customerCashSavingsPortfolioVo.InterestAmntAccumulated = 0;
                    }
                    else
                    {
                        customerCashSavingsPortfolioVo.IsInterestAccumalated = 0;
                        if (txtInterestAmountAccum.Text != "")
                            customerCashSavingsPortfolioVo.InterestAmntPaidOut = float.Parse(txtInterestAmountAccum.Text.ToString());
                        else
                            customerCashSavingsPortfolioVo.InterestAmntPaidOut = 0;

                    }

                }
                customerCashSavingsPortfolioVo.Remarks = txtRemarks.Text;

                customerCashSavingsPortfolioBo.AddCustomerCashSavingsDetails(customerCashSavingsPortfolioVo, userId);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioCashSavingsView','none')", true);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:btnAddDetails_Click()");
                object[] objects = new object[1];
                objects[0] = customerCashSavingsPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void ddlInterestBasis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAssetIC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkbtnAddAccounts_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadscript", "loadcontrol('CustomerAccountsAdd','none');", true);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LoadAccountDetails();
            SetFields();
            LoadFields("Edit");
            btnSaveChanges.Visible = true;
            btnSubmit.Visible = false;
        }

        private void UpdateCashSavingsAccount()
        {
            CashAndSavingsBo cashSavingsBo = new CashAndSavingsBo();
            CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
            CashAndSavingsVo cashSavingsVo = new CashAndSavingsVo();
            try
            {
                CashSavingsPortfolioId = Int32.Parse(Session["CashSavingsPortfolioId"].ToString());
                userVo = (UserVo)Session["userVo"];
                cashSavingsVo = customerCashSavingsPortfolioBo.GetSpecificCashSavings(CashSavingsPortfolioId, customerVo.CustomerId);
                customerAccountVo = customerAccountsBo.GetCashAndSavingsAccount(Convert.ToInt32(cashSavingsVo.AccountId));

                newAccountVo.AccountId = customerAccountVo.AccountId;
                newAccountVo.AccountNum = txtAccountNumber.Text.ToString();

                //newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountOpeningDate.Text.ToString());
                newAccountVo.BankName = txtAccountWith.Text;
                newAccountVo.ModeOfHolding = customerAccountVo.ModeOfHolding.ToString();
                cashSavingsBo.UpdateCashSavingsAccount(newAccountVo, userVo.UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:UpdateCashSavingsAccount()");
                object[] objects = new object[4];

                objects[0] = CashSavingsPortfolioId;
                objects[1] = userVo;
                objects[2] = cashSavingsVo;
                objects[3] = customerAccountVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void UpdateCashSavingsPortfolio()
        {

            CashAndSavingsVo newCashSavingsVo = new CashAndSavingsVo();
            CashAndSavingsBo cashSavingsBo = new CashAndSavingsBo();
            CashAndSavingsVo cashSavingsVo = new CashAndSavingsVo();
            try
            {
                CashSavingsPortfolioId = Int32.Parse(Session["CashSavingsPortfolioId"].ToString());
                userVo = (UserVo)Session["userVo"];
                cashSavingsVo = customerCashSavingsPortfolioBo.GetSpecificCashSavings(CashSavingsPortfolioId, customerVo.CustomerId);
                customerAccountVo = customerAccountsBo.GetCashAndSavingsAccount(Convert.ToInt32(cashSavingsVo.AccountId));
                newCashSavingsVo.AccountId = customerAccountVo.AccountId;
                newCashSavingsVo.AccountNumber = customerAccountVo.AccountNum;
                newCashSavingsVo.AssetGroupCode = "CS";
                newCashSavingsVo.AssetInstrumentCategoryCode = customerAccountVo.AssetCategory.ToString().Trim();
                newCashSavingsVo.CashSavingsPortfolioId = cashSavingsVo.CashSavingsPortfolioId;
                newCashSavingsVo.CustomerId = customerVo.CustomerId;
                //if (txtDepositAmount.Text != "")
                //    newCashSavingsVo.DepositAmount = double.Parse(txtDepositAmount.Text);
                //else
                //    newCashSavingsVo.DepositAmount = 0;
                if (cashSavingsVo.AssetInstrumentCategoryCode.ToString().Trim() == "CSSA" || cashSavingsVo.AssetInstrumentCategoryCode.ToString().Trim() == "CSCA")
                {
                    newCashSavingsVo.DepositDate = DateTime.MinValue;
                    if (txtDepositAmount.Text != "")
                        newCashSavingsVo.DepositAmount = double.Parse(txtDepositAmount.Text);
                    else
                        newCashSavingsVo.DepositAmount = 0;
                }
                else
                {
                    if (txtDepositDate.Text != "")
                        newCashSavingsVo.DepositDate = DateTime.Parse(txtDepositDate.Text);
                    else
                        newCashSavingsVo.DepositDate = DateTime.MinValue;
                    if (txtAmountOfLoan.Text != "")
                        newCashSavingsVo.DepositAmount = double.Parse(txtAmountOfLoan.Text);
                    else
                        newCashSavingsVo.DepositAmount = 0;
                    newCashSavingsVo.InterestPayoutFrequencyCode = ddlIPFrequency.SelectedValue.ToString().Trim();
                }
                if (cashSavingsVo.AssetInstrumentCategoryCode.ToString().Trim() != "CSCA")
                {
                   // 
                    newCashSavingsVo.Name = txtName.Text;
                    if (ddlIPFrequency.SelectedValue.ToString().Trim() != "AM")
                    {
                        if (txtInterestAmountAccum.Text != "")
                            newCashSavingsVo.InterestAmntPaidOut = float.Parse(txtInterestAmountAccum.Text);
                        else
                            newCashSavingsVo.InterestAmntPaidOut = 0;
                        newCashSavingsVo.IsInterestAccumalated = 0;
                    }
                    else
                    {
                        newCashSavingsVo.IsInterestAccumalated = 1;
                        if (txtInterestAmountAccum.Text != "")
                            newCashSavingsVo.InterestAmntAccumulated = float.Parse(txtInterestAmountAccum.Text);
                        else
                            newCashSavingsVo.InterestAmntAccumulated = 0;
                    }
                }


                newCashSavingsVo.Remarks = txtRemarks.Text;
                cashSavingsBo.UpdateCashSavingsDetails(newCashSavingsVo, userVo.UserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioCashSavingsEntry.ascx:UpdateCashSavingsPortfolio()");
                object[] objects = new object[5];

                objects[0] = newCashSavingsVo;
                objects[1] = CashSavingsPortfolioId;
                objects[2] = userVo;
                objects[3] = cashSavingsVo;
                objects[4] = customerAccountVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            UpdateCashSavingsAccount();
            UpdateCashSavingsPortfolio();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('PortfolioCashSavingsView','none');", true);
        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioCashSavingsView', 'none')", true);
        }
    }
}
