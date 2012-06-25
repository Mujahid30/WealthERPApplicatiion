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
        ProductMFBo productMfBo = new ProductMFBo();
        float stt;
        static int schemePlanCode;
        int amcCode;
        int accountId;
        string categoryCode;

        int flag = 0;
        //string categoryName;
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
                CompareValidator2.ValueToCompare = DateTime.Now.ToShortDateString(); 
                SessionBo.CheckSession();
                this.Page.Culture = "en-GB";
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session["userVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                BindPortfolioDropDown();
                Label19.Text = "Purchase Price :";
                
                
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    //BindPortfolioDropDown();
                    BindAMC();
                    BindCategory();
                    BindScheme();
                    BindFolioNumber(0);
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
                        //BindFolioNumber(1);
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

        private void BindScheme()
        {
            try
            {
                int status = 1;
                DataSet dsScheme = new DataSet();
                DataTable dtScheme;
                status = int.Parse(ddlSchemeType.SelectedValue);
                if (ddlAMC.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMC.SelectedValue.ToString());
                    categoryCode = ddlCategory.SelectedValue;
                    dsScheme = productMfBo.GetSchemeName(amcCode, categoryCode, 1,status);
                }
                else if (ddlAMC.SelectedIndex != 0)
                {
                    amcCode = int.Parse(ddlAMC.SelectedValue.ToString());
                    categoryCode = ddlCategory.SelectedValue;
                    dsScheme = productMfBo.GetSchemeName(amcCode, categoryCode, 0, status);
                }
                if (dsScheme.Tables.Count > 0)
                {
                    dtScheme = dsScheme.Tables[0];
                    ddlScheme.DataSource = dtScheme;
                    ddlScheme.DataValueField = dtScheme.Columns["PASP_SchemePlanCode"].ToString();
                    ddlScheme.DataTextField = dtScheme.Columns["PASP_SchemePlanName"].ToString();
                    ddlScheme.DataBind();
                    ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
                }
                 else
                {
                    ddlScheme.Items.Clear();
                    ddlScheme.DataSource = null;
                    ddlScheme.DataBind();
                    ddlScheme.Items.Insert(0, new ListItem("Select", "Select"));
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
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

        private void BindCategory()
        {
            try
            {
                DataSet dsProductAssetCategory;
                dsProductAssetCategory = productMfBo.GetProductAssetCategory();
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
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        //private void BindFolioNumber()
        //{
        //    dsCustomerAccounts = customerAccountBo.GetCustomerMFAccounts(portfolioId, "MF", schemePlanCode);
        //    dtCustomerAccounts = dsCustomerAccounts.Tables[0];
        //    ddlFolioNum.DataSource = dtCustomerAccounts;
        //    ddlFolioNum.DataTextField = "CMFA_FolioNum";
        //    ddlFolioNum.DataValueField = "CMFA_AccountId";
        //    ddlFolioNum.DataBind();
        //    ddlFolioNum.Items.Insert(0, new ListItem("Select a Folio Number", "Select a Folio Number"));
        //}
        private void BindFolioNumber(int flag)
        {
            DataSet dsgetfolioNo= new DataSet();
            DataTable dtgetfolioNo;
            try
            {
                if (flag != 0)
                {
                    amcCode = int.Parse(ddlAMC.SelectedValue);
                    dsgetfolioNo = productMfBo.GetFolioNumber(portfolioId, amcCode, 1);
                }
                else
                {
                    dsgetfolioNo = productMfBo.GetFolioNumber(portfolioId, amcCode, 0);
                }
                if (dsgetfolioNo.Tables.Count > 0)
                {
                    dtgetfolioNo = dsgetfolioNo.Tables[0];
                    ddlFolioNum.DataSource = dtgetfolioNo;
                    ddlFolioNum.DataTextField =dtgetfolioNo.Columns["CMFA_FolioNum"].ToString();
                    ddlFolioNum.DataValueField = dtgetfolioNo.Columns["CMFA_AccountId"].ToString();
                    ddlFolioNum.DataBind();
                    ddlFolioNum.Items.Insert(0, new ListItem("Select", "0"));
                }
                
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFManualSingleTran.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

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
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            BindFolioNumber(0);

        }
        protected void setVisibility()
        {
            tdLblNav.Visible = true;
            tdtxtNAV.Visible = true;
            tdNavButton.Visible = true;
            if (ddlTransactionType.SelectedItem.Value == "Sell")
            {
                trPrice.Visible = true;
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
                Label19.Text = "Sell Price";

            }
            if (ddlTransactionType.SelectedItem.Value == "Buy" || ddlTransactionType.SelectedItem.Value == "Holdings")
            {
                trPrice.Visible = true;
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
                Label19.Text = "Purchase Price :";
            }
            if (ddlTransactionType.SelectedItem.Value == "Dividend Reinvestment")
            {
                trPrice.Visible = true;
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
                Label19.Text = "Purchase Price :";
            }
            if (ddlTransactionType.SelectedItem.Value == "SIP")
            {
                trPrice.Visible = true;
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
                Label19.Text = "Purchase Price :";

                //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopUpScript", "showSIPDetails();", true);

            }
            if (ddlTransactionType.SelectedItem.Value == "Switch")
            {
                trPrice.Visible = true;
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
                Label19.Text = "Sell Price";
            }
            if (ddlTransactionType.SelectedItem.Value == "SWP")
            {
                trPrice.Visible = true;
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
                Label19.Text = "Sell Price";
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopUpScript", "showSWPDetails();", true);

            }
            if (ddlTransactionType.SelectedItem.Value == "STP")
            {
                trPrice.Visible = true;
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
                Label19.Text = "Purchase Price :";
            }
            if (ddlTransactionType.SelectedItem.Value == "Dividend Payout")
            {
                trPrice.Visible = false;
                txtPrice.Text = null;
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
                Label19.Text = "Purchase Price :";
                tdLblNav.Visible = false;
                tdtxtNAV.Visible = false;
                tdNavButton.Visible = false;
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
                //if (txtSearchScheme.Text != "" && lblScheme.Text == txtSearchScheme.Text)
                //if(ddlScheme.SelectedIndex !=0 && lblScheme.Text == ddlScheme.SelectedItem.Text)
                //{
                    mfTransactionVo.CustomerId = customerVo.CustomerId;
                    //mfTransactionVo.AccountId = "acc1";
                    mfTransactionVo.AccountId = int.Parse(ddlFolioNum.SelectedItem.Value.ToString());
                    mfTransactionVo.AMCCode = int.Parse(ddlAMC.SelectedValue.ToString());
                    mfTransactionVo.CategoryCode = ddlCategory.SelectedValue;
                    mfTransactionVo.MFCode = schemePlanCode;
                    //mfTransactionVo.MFCode = int.Parse(txtSchemeCode.Value);
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
                        //mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
                        mfTransactionVo.Price = 0;
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
                    List<MFPortfolioVo> mfPortfolioVoList = new List<MFPortfolioVo>();
                    Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
                    DateTime tradeDate = new DateTime();
                    if (Session["ValuationDate"] != null)
                    {
                        genDict = (Dictionary<string, DateTime>)Session["ValuationDate"];
                        tradeDate = DateTime.Parse(genDict["MFDate"].ToString());
                    }
                    if (tradeDate == DateTime.MinValue)
                    {
                        tradeDate = DateTime.Today;
                    }
                    mfPortfolioVoList = customerPortfolioBo.GetCustomerMFPortfolio(customerVo.CustomerId, portfolioId, tradeDate, lblScheme.Text, ddlFolioNum.SelectedItem.Text, "");
                    if (mfPortfolioVoList != null && mfPortfolioVoList.Count > 0)
                    {
                        customerPortfolioBo.DeleteMutualFundNetPosition(mfPortfolioVoList[0].MFCode, mfPortfolioVoList[0].AccountId, tradeDate);
                        customerPortfolioBo.AddMutualFundNetPosition(mfPortfolioVoList[0], userVo.UserId);
                    }

                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);

                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
                    msgRecordStatus.Visible = true;
                    cleanAllFields();
                
                 if (Cache[advisorVo.advisorId.ToString()] != null)
                    {
                        Cache.Remove(advisorVo.advisorId.ToString());
                    }
                 
                   

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView", false);
                //}
                //else
                //{
                //    //msgRecordStatus.InnerText = "Adding records is not successfull";
                //    //msgRecordStatus.Style.Add("background", "Red");


                //    //RequiredFieldValidator3.ErrorMessage = "Please Select Proper Scheme Name";
                //    //RequiredFieldValidator3.IsValid = false;
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
        public void cleanAllFields()
        {
         //txtSearchScheme.Text="";
        ddlScheme.SelectedIndex = 0;
         ddlAMC.SelectedIndex=0;
         ddlCategory.SelectedIndex = 0;
        ddlTransactionType.SelectedIndex=0;
        ddlFolioNum.SelectedIndex=0;
        txtTransactionDate.Text="";
        txtNAV.Text="";
        txtPrice.Text="";
        txtAmount.Text="";
        txtUnits.Text="";
        txtSTT.Text="";
        txtNAVPurchased.Text="";
        txtPricePurchased.Text="";
        txtAmtPurchased.Text="";
        txtUnitsAlloted.Text="";
        txtSwicthSchemeSearch.Text="";
        txtDividentRate.Text = "";
        lblScheme.Text = "";
        ddlPortfolio.SelectedIndex = 0;
        lblSchemeName.Visible = false;


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
        tdSwitchUseNAV.Visible = false;
        tdSTTLabel.Visible = true;
        tdSTTValue.Visible = true;
        }

        protected void txtSearchScheme_TextChanged(object sender, EventArgs e)
        {
            
        }
        //protected void txtSchemeCode_ValueChanged(object sender, EventArgs e)
        //{
        //    msgRecordStatus.Visible = false;
        //    tdSchemeNameLabel.Visible = true;
        //    tdSchemeNameValue.Visible = true;
        //    lblSchemeName.Visible = true;
        //    lblScheme.Text = txtSearchScheme.Text;
        //    schemePlanCode = int.Parse(txtSchemeCode.Value);
        //    txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = schemePlanCode.ToString();
        //    BindFolioNumber();
        //}
        protected void btnNewFolioAdd_Click(object sender, EventArgs e)
        {
            SaveCurrentPageState();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerMFAccountAdd','?FromPage=MFManualSingleTran');", true);

        }

        protected void txtTransactionDate_TextChanged(object sender, EventArgs e)
        {
            string ddlTrxnType = "";
            DateTime dt = Convert.ToDateTime(txtTransactionDate.Text);
            if (ddlTransactionType.SelectedItem.Value == "Sell")
            {
               ddlTrxnType = "SEL";
            }
            else if( ddlTransactionType.SelectedItem.Value == "Buy" )
            {
                ddlTrxnType = "BUY";
            }
            else if( ddlTransactionType.SelectedItem.Value == "Dividend Reinvestment" )
            {
                ddlTrxnType = "DVR";
            }
            else if( ddlTransactionType.SelectedItem.Value == "SIP" )
            {
                ddlTrxnType = "SIP";
            }
            else if( ddlTransactionType.SelectedItem.Value == "SWP" )
            {
                ddlTrxnType = "SWP";
            }
            else if( ddlTransactionType.SelectedItem.Value == "STP" )
            {
                ddlTrxnType = "STP";
            }
            else if( ddlTransactionType.SelectedItem.Value == "Dividend Payout" )
            {
                ddlTrxnType = "DVP";
            }
            else if( ddlTransactionType.SelectedItem.Value == "Switch" )
            {
                ddlTrxnType = "SWS";
            }
            else if( ddlTransactionType.SelectedItem.Value == "Holdings" )
            {
                ddlTrxnType = "HLD";
            }

            DataSet dsNavDetails = new DataSet();
            dsNavDetails = customerPortfolioBo.GetMFSchemePlanPurchaseDateAndValue(schemePlanCode, dt, ddlTrxnType);

            if(dsNavDetails != null)
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

        protected void btnUseNAV_Click(object sender, EventArgs e)
        {
               txtPrice.Text = txtNAV.Text;
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text != "" && commonMethods.IsNumeric(txtAmount.Text))
            {
                if (ddlTransactionType.SelectedItem.Value != "Dividend Payout")
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
            hashtable.Add("SchemeSearch", ddlScheme.SelectedValue);
            //hashtable.Add("SchemeSearch", txtSearchScheme.Text);
            //hashtable.Add("SchemeCode", txtSchemeCode.Value);
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
            hashtable.Add("AMC", ddlAMC.SelectedValue);
            hashtable.Add("Category", ddlCategory.SelectedValue);

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
                ddlAMC.SelectedValue = hashtable["AMC"].ToString();
                ddlCategory.SelectedValue = hashtable["Category"].ToString();
                BindScheme();
                ddlScheme.SelectedValue = hashtable["SchemeSearch"].ToString();
                //txtSearchScheme.Text = hashtable["SchemeSearch"].ToString();
                lblScheme.Text = hashtable["SchemeName"].ToString();
                BindFolioNumber(1);
                //txtSchemeCode.Value = hashtable["SchemeCode"].ToString();
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

        private void BindAMC()
        {
            DataSet dsProductAmc;
            DataTable dtProductAMC;
            try
            {
                dsProductAmc = productMfBo.GetProductAmc();
                if (dsProductAmc.Tables.Count > 0)
                {
                    dtProductAMC = dsProductAmc.Tables[0];
                    ddlAMC.DataSource = dtProductAMC;
                    ddlAMC.DataTextField = dtProductAMC.Columns["PA_AMCName"].ToString();
                    ddlAMC.DataValueField = dtProductAMC.Columns["PA_AMCCode"].ToString();
                    ddlAMC.DataBind();
                }
                ddlAMC.Items.Insert(0, new ListItem("Select", "Select"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }



        protected void ddlAMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAMC.SelectedIndex != 0)
            {
                amcCode = int.Parse(ddlAMC.SelectedValue);
                BindScheme();
                BindFolioNumber(1);
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedIndex != 0)
            {
                if(ddlAMC.SelectedIndex !=0)
                    amcCode = int.Parse(ddlAMC.SelectedValue);
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please select AMC First');", true);
                categoryCode = ddlCategory.SelectedValue;
                BindScheme();
            }
        }

        protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            msgRecordStatus.Visible = false;
            if (ddlScheme.SelectedIndex != 0)
            {
                tdSchemeNameLabel.Visible = true;
                tdSchemeNameValue.Visible = true;
                lblSchemeName.Visible = true;
                lblScheme.Text = ddlScheme.SelectedItem.Text;
                schemePlanCode = int.Parse(ddlScheme.SelectedValue);
                categoryCode = productMfBo.GetCategoryNameFromSChemeCode(schemePlanCode);
                ddlCategory.SelectedValue = categoryCode;
                txtSwitchSchemeCode_AutoCompleteExtender.ContextKey = schemePlanCode.ToString();
                
            }
        }

        protected void ddlFolioNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFolioNum.SelectedIndex != 0)
            {
                
                accountId = int.Parse(ddlFolioNum.SelectedValue);
                amcCode = productMfBo.GetAMCfromFolioNo(accountId);
                ddlAMC.SelectedValue = amcCode.ToString();
                if(ddlScheme.SelectedIndex == 0)
                     BindScheme();
 
            }
        }

        protected void ddlSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindScheme();
        }

    }
}
