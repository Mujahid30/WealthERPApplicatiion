using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoCustomerPortfolio;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using BoProductMaster;
using VoProductMaster;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using WealthERP.Base;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;


namespace WealthERP.CustomerPortfolio
{
    public partial class MFManualSingleTran : System.Web.UI.UserControl
    {
        
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        ProductMFBo productMFBo = new ProductMFBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        DataSet dsCustomerAccounts;
        DataTable dtCustomerAccounts;
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        float stt;
        static int schemePlanCode;
        int transactionId;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        static int portfolioId;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AssetBo assetBo = new AssetBo();
        CommonProgrammingBo commonMethods = new CommonProgrammingBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session["userVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                BindPortfolioDropDown();
                BindFolioNumber();
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    //BindPortfolioDropDown();
                    tdSchemeNameLabel.Visible = false;
                    tdSchemeNameValue.Visible = false;
                    tdSchemeToLabel.Visible = false;
                    tdSchemeToValue.Visible = false;
                    tdNAVPurchasedLabel.Visible = false;
                    tdNAVPurchasedValue.Visible = false;
                    tdPricePurchasedLabel.Visible = false;
                    tdPricePurchasedValue.Visible = false;
                    tdAmtPurchasedLabel.Visible = false;
                    tdAmtPurchasedValue.Visible = false;
                    tdUnitsAllotedLabel.Visible = false;
                    tdUnitsAllotedValue.Visible = false;
                    // BindFolioNumber();

                    customerAccountsVo = (CustomerAccountsVo)Session[SessionContents.CustomerMFAccount];
                    if (Session[SessionContents.CustomerMFAccount] != null)
                    {
                        customerAccountsVo = (CustomerAccountsVo)Session[SessionContents.CustomerMFAccount];
                        ddlFolioNum.SelectedValue = customerAccountsVo.AccountId.ToString();
                    }
                    RestorePreviousState();
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
                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = userVo;
                objects[1] = customerVo;
                objects[2] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void BindFolioNumber()
        {
            dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", schemePlanCode);
            dtCustomerAccounts = dsCustomerAccounts.Tables[0];
            ddlFolioNum.DataSource = dtCustomerAccounts;
            ddlFolioNum.DataTextField = "CMFA_FolioNum";
            ddlFolioNum.DataValueField = "CMFA_AccountId";
            ddlFolioNum.DataBind();
            ddlFolioNum.Items.Insert(0, new ListItem("Select a Folio Number", "Select a Folio Number"));
        }

        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindFolioNumber();

        }
        protected void setVisibility()
        {
            if (ddlTransactionType.SelectedItem.Value == "Sell")
            {
                tdUnitsLabel.Visible = true;
                tdUnitsValue.Visible = true;
                tdSTTLabel.Visible = true;
                tdSTTValue.Visible = true;
                trDividentRate.Visible = false;
                tdSchemeToLabel.Visible = false;
                tdSchemeToValue.Visible = false;
                tdNAVPurchasedLabel.Visible = false;
                tdNAVPurchasedValue.Visible = false;
                tdSwitchUseNAV.Visible = false;
                tdPricePurchasedLabel.Visible = false;
                tdPricePurchasedValue.Visible = false;
                tdAmtPurchasedLabel.Visible = false;
                tdAmtPurchasedValue.Visible = false;
                tdUnitsAllotedLabel.Visible = false;
                tdUnitsAllotedValue.Visible = false;


            }
            if (ddlTransactionType.SelectedItem.Value == "Buy" || ddlTransactionType.SelectedItem.Value == "Holdings")
            {
                tdUnitsLabel.Visible = true;
                tdUnitsValue.Visible = true;
                tdSTTLabel.Visible = false;
                tdSTTValue.Visible = false;
                trDividentRate.Visible = false;
                tdSchemeToLabel.Visible = false;
                tdSchemeToValue.Visible = false;
                tdNAVPurchasedLabel.Visible = false;
                tdNAVPurchasedValue.Visible = false;
                tdSwitchUseNAV.Visible = false;
                tdPricePurchasedLabel.Visible = false;
                tdPricePurchasedValue.Visible = false;
                tdAmtPurchasedLabel.Visible = false;
                tdAmtPurchasedValue.Visible = false;
                tdUnitsAllotedLabel.Visible = false;
                tdUnitsAllotedValue.Visible = false;

            }
            if (ddlTransactionType.SelectedItem.Value == "Dividend Reinvestment")
            {
                trDividentRate.Visible = true;
                tdUnitsLabel.Visible = true;
                tdUnitsValue.Visible = true;
                tdSTTLabel.Visible = false;
                tdSTTValue.Visible = false;
                tdSchemeToLabel.Visible = false;
                tdSchemeToValue.Visible = false;
                tdNAVPurchasedLabel.Visible = false;
                tdNAVPurchasedValue.Visible = false;
                tdSwitchUseNAV.Visible = false;
                tdPricePurchasedLabel.Visible = false;
                tdPricePurchasedValue.Visible = false;
                tdAmtPurchasedLabel.Visible = false;
                tdAmtPurchasedValue.Visible = false;
                tdUnitsAllotedLabel.Visible = false;
                tdUnitsAllotedValue.Visible = false;
            }
            if (ddlTransactionType.SelectedItem.Value == "SIP")
            {
                tdUnitsLabel.Visible = true;
                tdUnitsValue.Visible = true;
                tdSTTLabel.Visible = false;
                tdSTTValue.Visible = false;
                trDividentRate.Visible = false;
                tdSchemeToLabel.Visible = false;
                tdSchemeToValue.Visible = false;
                tdNAVPurchasedLabel.Visible = false;
                tdNAVPurchasedValue.Visible = false;
                tdSwitchUseNAV.Visible = false;
                tdPricePurchasedLabel.Visible = false;
                tdPricePurchasedValue.Visible = false;
                tdAmtPurchasedLabel.Visible = false;
                tdAmtPurchasedValue.Visible = false;
                tdUnitsAllotedLabel.Visible = false;
                tdUnitsAllotedValue.Visible = false;


                //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopUpScript", "showSIPDetails();", true);

            }
            if (ddlTransactionType.SelectedItem.Value == "Switch")
            {
                trDividentRate.Visible = false;
                tdUnitsLabel.Visible = true;
                tdUnitsValue.Visible = true;
                tdSTTLabel.Visible = true;
                tdSTTValue.Visible = true;
                tdSchemeToLabel.Visible = true;
                tdSchemeToValue.Visible = true;
                tdNAVPurchasedLabel.Visible = true;
                tdNAVPurchasedValue.Visible = true;
                tdSwitchUseNAV.Visible = true;
                tdPricePurchasedLabel.Visible = true;
                tdPricePurchasedValue.Visible = true;
                tdAmtPurchasedLabel.Visible = true;
                tdAmtPurchasedValue.Visible = true;
                tdUnitsAllotedLabel.Visible = true;
                tdUnitsAllotedValue.Visible = true;
            }
            if (ddlTransactionType.SelectedItem.Value == "SWP")
            {
                trDividentRate.Visible = false;
                tdUnitsLabel.Visible = true;
                tdUnitsValue.Visible = true;
                tdSTTLabel.Visible = true;
                tdSTTValue.Visible = true;
                tdSchemeToLabel.Visible = false;
                tdSchemeToValue.Visible = false;
                tdNAVPurchasedLabel.Visible = false;
                tdNAVPurchasedValue.Visible = false;
                tdPricePurchasedLabel.Visible = false;
                tdPricePurchasedValue.Visible = false;
                tdSwitchUseNAV.Visible = false;
                tdAmtPurchasedLabel.Visible = false;
                tdAmtPurchasedValue.Visible = false;
                tdUnitsAllotedLabel.Visible = false;
                tdUnitsAllotedValue.Visible = false;
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopUpScript", "showSWPDetails();", true);

            }
            if (ddlTransactionType.SelectedItem.Value == "STP")
            {
                trDividentRate.Visible = false;
                tdUnitsLabel.Visible = true;
                tdUnitsValue.Visible = true;
                tdSTTLabel.Visible = true;
                tdSTTValue.Visible = true;
                tdSchemeToLabel.Visible = true;
                tdSchemeToValue.Visible = true;
                tdNAVPurchasedLabel.Visible = true;
                tdNAVPurchasedValue.Visible = true;
                tdSwitchUseNAV.Visible = true;
                tdPricePurchasedLabel.Visible = true;
                tdPricePurchasedValue.Visible = true;
                tdAmtPurchasedLabel.Visible = true;
                tdAmtPurchasedValue.Visible = true;
                tdUnitsAllotedLabel.Visible = true;
                tdUnitsAllotedValue.Visible = true;
            }
            if (ddlTransactionType.SelectedItem.Value == "Dividend Payout")
            {
                trDividentRate.Visible = true;
                tdUnitsLabel.Visible = false;
                tdUnitsValue.Visible = false;
                tdSTTLabel.Visible = false;
                tdSTTValue.Visible = false;
                tdSchemeToLabel.Visible = false;
                tdSchemeToValue.Visible = false;
                tdNAVPurchasedLabel.Visible = false;
                tdNAVPurchasedValue.Visible = false;
                tdPricePurchasedLabel.Visible = false;
                tdPricePurchasedValue.Visible = false;
                tdSwitchUseNAV.Visible = false;
                tdAmtPurchasedLabel.Visible = false;
                tdAmtPurchasedValue.Visible = false;
                tdUnitsAllotedLabel.Visible = false;
                tdUnitsAllotedValue.Visible = false;
            }
        }
        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setVisibility();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtSearchScheme.Text != "" && lblScheme.Text == txtSearchScheme.Text)
                {
                    mfTransactionVo.CustomerId = customerVo.CustomerId;
                    //mfTransactionVo.AccountId = "acc1";
                    mfTransactionVo.AccountId = int.Parse(ddlFolioNum.SelectedItem.Value.ToString());
                    mfTransactionVo.MFCode = int.Parse(txtSchemeCode.Value);
                    mfTransactionVo.FinancialFlag = 1;
                    mfTransactionVo.TransactionDate = DateTime.Parse(txtTransactionDate.Text);//ddlTransactionDateDay.SelectedItem.Value + "/" + ddlTransactionDateMonth.SelectedItem.Value + "/" + ddlTransactionDateYear.SelectedItem.Value
                    mfTransactionVo.Source = "WP";
                    mfTransactionVo.IsSourceManual = 1;


                    if (ddlTransactionType.SelectedItem.Value == "Buy")
                    {
                        mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "BUY";
                        mfTransactionVo.BuySell = "B";

                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }
                    }
                
                if (ddlTransactionType.SelectedItem.Value == "Holdings")
                {
                    mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                    mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                    mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                    mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                    mfTransactionVo.TransactionClassificationCode = "HLD";
                    mfTransactionVo.BuySell = "B";

                    if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                    {
                        customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                    }
                }
                if (ddlTransactionType.SelectedItem.Value == "Sell")
                {
                    mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                    mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                    mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                    mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                    mfTransactionVo.STT = float.Parse(txtSTT.Text.ToString());
                    mfTransactionVo.TransactionClassificationCode = "SEL";
                    mfTransactionVo.BuySell = "S";

                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }
                    }
                    if (ddlTransactionType.SelectedItem.Value == "Dividend Reinvestment")
                    {
                        if (txtDividentRate.Text != string.Empty || txtDividentRate.Text != "")
                            mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                        mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "DVR";

                        mfTransactionVo.BuySell = "B";

                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }
                    }
                    if (ddlTransactionType.SelectedItem.Value == "SIP")
                    {
                        //mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                        mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "SIP";
                        mfTransactionVo.BuySell = "B";

                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }
                    }
                    if (ddlTransactionType.SelectedItem.Value == "SWP")
                    {
                        //mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                        mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                        mfTransactionVo.STT = float.Parse(txtSTT.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "SWP";
                        mfTransactionVo.BuySell = "S";

                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }
                    }
                    if (ddlTransactionType.SelectedItem.Value == "STP")
                    {
                        mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                        mfTransactionVo.STT = float.Parse(txtSTT.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "STS";
                        mfTransactionVo.BuySell = "S";

                        if ((transactionId = customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId)) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }

                        mfTransactionVo.MFCode = int.Parse(txtSwitchSchemeCode.Value);
                        mfTransactionVo.NAV = float.Parse(txtNAVPurchased.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPricePurchased.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmtPurchased.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnitsAlloted.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "STB";
                        mfTransactionVo.SwitchSourceTrxId = transactionId;
                        //mfTransactionVo.TransactionId = customerBo.GenerateId();
                        mfTransactionVo.BuySell = "B";

                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }
                    }
                    if (ddlTransactionType.SelectedItem.Value == "Dividend Payout")
                    {
                        if (txtDividentRate.Text != string.Empty || txtDividentRate.Text != "")
                            mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                        mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "DVP";
                        mfTransactionVo.BuySell = "S";

                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }
                    }
                    if (ddlTransactionType.SelectedItem.Value == "Switch")
                    {
                        mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                        mfTransactionVo.STT = float.Parse(txtSTT.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "SWS";
                        mfTransactionVo.BuySell = "S";

                        if ((transactionId = customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId)) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }

                        //  transactionId = customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId);

                        mfTransactionVo.MFCode = int.Parse(txtSwitchSchemeCode.Value);
                        mfTransactionVo.NAV = float.Parse(txtNAVPurchased.Text.ToString());
                        mfTransactionVo.Price = float.Parse(txtPricePurchased.Text.ToString());
                        mfTransactionVo.Amount = float.Parse(txtAmtPurchased.Text.ToString());
                        mfTransactionVo.Units = float.Parse(txtUnitsAlloted.Text.ToString());
                        mfTransactionVo.TransactionClassificationCode = "SWB";
                        mfTransactionVo.SwitchSourceTrxId = transactionId;
                        //mfTransactionVo.TransactionId = customerBo.GenerateId();
                        mfTransactionVo.BuySell = "B";
                        if (customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId) != 0)
                        {
                            customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "MF", mfTransactionVo.TransactionDate);
                        }

                    }

                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);

                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);

                    Response.Redirect("ControlHost.aspx?pageid=TransactionsView", false);
                }
                else
                {
                    RequiredFieldValidator3.ErrorMessage = "Please Select Proper Scheme Name";
                    RequiredFieldValidator3.IsValid = false;
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
                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:btnSubmit_Click()");
                object[] objects = new object[2];
                objects[0] = mfTransactionVo;
                objects[1] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void txtSearchScheme_TextChanged(object sender, EventArgs e)
        {
            
        }
        protected void txtSchemeCode_ValueChanged(object sender, EventArgs e)
        {
            tdSchemeNameLabel.Visible = true;
            tdSchemeNameValue.Visible = true;
            lblScheme.Text = txtSearchScheme.Text;
            schemePlanCode = int.Parse(txtSchemeCode.Value);
            txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = schemePlanCode.ToString();
            BindFolioNumber();
        }
        protected void btnNewFolioAdd_Click(object sender, EventArgs e)
        {
            SaveCurrentPageState();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?FromPage=MFManualSingleTran');", true);

        }

        protected void txtTransactionDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(txtTransactionDate.Text);
            txtNAV.Text = customerPortfolioBo.GetMFSchemePlanNAV(schemePlanCode, dt).ToString();
        }

        protected void btnUseNAV_Click(object sender, EventArgs e)
        {
            txtPrice.Text = txtNAV.Text;
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text != "" && commonMethods.IsNumeric(txtAmount.Text))
            {                
                txtUnits.Text = (Math.Round((double.Parse(txtAmount.Text) / double.Parse(txtPrice.Text)), 4)).ToString();
                txtSTT.Text = (Math.Round((double.Parse(txtAmount.Text) - Math.Round((double.Parse(txtUnits.Text) * double.Parse(txtNAV.Text)), 2)), 2)).ToString();
                if (txtAmtPurchased.Visible)
                {
                    txtAmtPurchased.Text = txtAmount.Text;
                    if (txtPricePurchased.Text != "" && txtAmtPurchased.Text != "" && txtAmtPurchased.Text != "0")
                    {
                        txtUnitsAlloted.Text = (Math.Round((double.Parse(txtAmtPurchased.Text) / double.Parse(txtPricePurchased.Text)), 4)).ToString();
                    }
                }
            }
            else
            {
                txtUnits.Text = "";
                txtSTT.Text = "";
            }
        }

        protected void txtUnits_TextChanged(object sender, EventArgs e)
        {
            if (txtUnits.Text != "" && commonMethods.IsNumeric(txtUnits.Text))
                txtAmount.Text = (Math.Round((double.Parse(txtPrice.Text) * double.Parse(txtUnits.Text)), 4)).ToString();
            else
            {
                txtAmount.Text = "";
                txtSTT.Text = "";
            
            }
           
        }
        //public static bool IsNumeric(string s)
        //{
        //    try
        //    {
        //        Double.Parse(s);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        private void SaveCurrentPageState()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("Portfolio", ddlPortfolio.SelectedItem.Value);
            hashtable.Add("SchemeSearch", txtSearchScheme.Text);
            hashtable.Add("SchemeCode", txtSchemeCode.Value);
            hashtable.Add("TransactionType", ddlTransactionType.SelectedValue);
            hashtable.Add("SchemeName", lblScheme.Text);
            hashtable.Add("TransactionDate", txtTransactionDate.Text);
            hashtable.Add("NAV", txtNAV.Text);
            hashtable.Add("DividendRate", txtDividentRate.Text);
            hashtable.Add("SchemeTo", txtSwicthSchemeSearch.Text);
            hashtable.Add("SwitchSchemeCode", txtSwitchSchemeCode.Value);

            hashtable.Add("Price", txtPrice.Text);
            hashtable.Add("NAVofSchemePur", txtNAVPurchased.Text);
            hashtable.Add("Amount", txtAmount.Text);
            hashtable.Add("PriceofSchemePur", txtPricePurchased.Text);
            hashtable.Add("Units", txtUnits.Text);
            hashtable.Add("UnitsAllotted", txtUnitsAlloted.Text);
            hashtable.Add("STT", txtSTT.Text);
            hashtable.Add("AmountPur", txtAmtPurchased.Text);

            Session["MFManualSingleTranHT"] = hashtable;
        }

        private void RestorePreviousState()
        {
            if (Session["MFManualSingleTranHT"] != null && Request.QueryString["prevPage"] == "CustomerMFAccountAdd")
            {
                tdSchemeNameLabel.Visible = true;
                tdSchemeNameValue.Visible = true;
                Hashtable hashtable = new Hashtable();
                hashtable = (Hashtable)Session["MFManualSingleTranHT"];
                ddlPortfolio.SelectedValue = hashtable["Portfolio"].ToString();
                txtSearchScheme.Text = hashtable["SchemeSearch"].ToString();
                lblScheme.Text = hashtable["SchemeName"].ToString();
                txtSchemeCode.Value = hashtable["SchemeCode"].ToString();
                ddlTransactionType.SelectedValue = hashtable["TransactionType"].ToString();
                txtTransactionDate.Text = hashtable["TransactionDate"].ToString();
                txtNAV.Text = hashtable["NAV"].ToString();
                txtDividentRate.Text = hashtable["DividendRate"].ToString();
                txtSwicthSchemeSearch.Text = hashtable["SchemeTo"].ToString();
                txtSwitchSchemeCode.Value = hashtable["SwitchSchemeCode"].ToString();
                txtPrice.Text = hashtable["Price"].ToString();
                txtNAVPurchased.Text = hashtable["NAVofSchemePur"].ToString();
                txtAmount.Text = hashtable["Amount"].ToString();
                txtPricePurchased.Text = hashtable["PriceofSchemePur"].ToString();
                txtUnits.Text = hashtable["Units"].ToString();
                txtUnitsAlloted.Text = hashtable["UnitsAllotted"].ToString();
                txtSTT.Text = hashtable["STT"].ToString();
                txtAmtPurchased.Text = hashtable["AmountPur"].ToString();
            }
            Session["MFManualSingleTranHT"] = null;
            txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = txtSwitchSchemeCode.Value;
            setVisibility();
        }

        protected void txtSwitchSchemeCode_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(txtTransactionDate.Text);
            txtNAVPurchased.Text = customerPortfolioBo.GetMFSchemePlanNAV(int.Parse(txtSwitchSchemeCode.Value), dt).ToString();
        }

        protected void btnSwitchNAV_Click(object sender, EventArgs e)
        {
            txtPricePurchased.Text = txtNAVPurchased.Text;
        }

        protected void txtAmtPurchased_TextChanged(object sender, EventArgs e)
        {
            if (txtPricePurchased.Text != "" && txtAmtPurchased.Text != "" && txtAmtPurchased.Text!="0")
            {
                txtUnitsAlloted.Text = (Math.Round((double.Parse(txtAmtPurchased.Text) / double.Parse(txtPricePurchased.Text)),4)).ToString();
            }
        }

       

    }
}
