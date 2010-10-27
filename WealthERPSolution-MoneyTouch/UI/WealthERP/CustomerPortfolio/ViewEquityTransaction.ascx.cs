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
using VoUser;
using WealthERP.Base;
using BoCommon;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;


namespace WealthERP.CustomerPortfolio
{
    public partial class ViewEquityTransaction : System.Web.UI.UserControl
    {
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        EQTransactionVo eqTransactionVo;
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        int portfolioId;
        string path;

        ProductEquityBo productEqutiyBo = new ProductEquityBo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        static float tempService;
        static float tempEducation;
        double tempSTT;
        static int scripCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                eqTransactionVo = (EQTransactionVo)Session["EquityTransactionVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session["userVo"];
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                if (!IsPostBack)
                {
                    LoadViewField();
                }
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityTransaction.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = portfolioId;
                objects[1] = customerVo;
                objects[2] = eqTransactionVo;
                objects[3] = userVo;
                objects[4] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void LoadViewField()
        {

            try
            {
                SetViewFields(1);
                if (eqTransactionVo.IsSourceManual == 1)
                {
                    LinkButton1.Visible = true;
                }
                else
                {
                    LinkButton1.Visible = false;
                }
                LoadExchangeType(path);
                ddlExchange.SelectedValue = eqTransactionVo.Exchange.ToString();

                LoadTransactionType(path);
                if (eqTransactionVo.TransactionCode == 1)
                {
                    ddlTranType.SelectedValue = "PUR";
                }
                else if (eqTransactionVo.TransactionCode == 2)
                {
                    ddlTranType.SelectedValue = "SELL";
                }
                else if (eqTransactionVo.TransactionCode == 13)
                {
                    ddlTranType.SelectedValue = "HLD";
                }

                LoadEquityTradeNumbers();
                ddlTradeAcc.SelectedValue = eqTransactionVo.AccountId.ToString();

                txtTradeDate.Text = eqTransactionVo.TradeDate.ToShortDateString().ToString();
                if (eqTransactionVo.BrokerCode != "")
                    txtBroker.Text = XMLBo.GetBrokerName(path, eqTransactionVo.BrokerCode);
                txtBrokerage.Text = eqTransactionVo.Brokerage.ToString();
                txtNumShares.Text = eqTransactionVo.Quantity.ToString();
                txtOtherCharge.Text = eqTransactionVo.OtherCharges.ToString();
                txtRate.Text = eqTransactionVo.Rate.ToString();
                txtRateIncBrokerage.Text = eqTransactionVo.RateInclBrokerage.ToString();
                txtScrip.Text = productEqutiyBo.GetScripName(eqTransactionVo.ScripCode);
                txtSTT.Text = eqTransactionVo.STT.ToString();
                txtTax.Text = eqTransactionVo.ServiceTax.ToString();
                txtTotal.Text = eqTransactionVo.TradeTotal.ToString();

                DataSet ds = productEqutiyBo.GetScripCode(txtScrip.Text.ToString());
                txtTicker.Text = ds.Tables[0].Rows[0]["PEM_Ticker"].ToString();
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityTransaction.ascx:LoadViewField()");
                object[] objects = new object[1];
                objects[0] = eqTransactionVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void LoadEquityTradeNumbers()
        {
            try
            {
                if (ddlTradeAcc.Items.Count == 0)
                {
                    DataSet dsEqutiyTradeNumbers = customerAccountBo.GetCustomerEQAccounts(portfolioId, "DE");

                    DataTable dtCustomerAccounts = dsEqutiyTradeNumbers.Tables[0];
                    ddlTradeAcc.DataSource = dtCustomerAccounts;
                    ddlTradeAcc.DataTextField = "CETA_TradeAccountNum";
                    ddlTradeAcc.DataValueField = "CETA_AccountId";
                    ddlTradeAcc.DataBind();
                    ddlTradeAcc.Items.Insert(0, new ListItem("Select the Trade Number", "Select the Trade Number"));
                }
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityTransaction.ascx:LoadEquityTradeNumbers()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void LoadTransactionType(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetTransactionType(path);
                ddlTranType.DataSource = dt;
                ddlTranType.DataTextField = "TransactionName";
                ddlTranType.DataValueField = "TransactionCode";
                ddlTranType.DataBind();
                ddlTranType.Items.Insert(0, new ListItem("Select Transaction", "Select Transaction"));
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityTransaction.ascx:LoadTransactionType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void SetViewFields(int i)
        {
            // i = 1 displays the View Form
            // i = 0 displays the Edit Form
            if (i == 1)
            {
                txtTotal.Enabled = false;
                txtTicker.Enabled = false;
                txtTax.Enabled = false;
                txtSTT.Enabled = false;
                txtScrip.Enabled = false;
                txtRateIncBrokerage.Enabled = false;
                txtRate.Enabled = false;
                txtOtherCharge.Enabled = false;
                txtNumShares.Enabled = false;
                txtBrokerage.Enabled = false;
                txtBroker.Enabled = false;
                ddlTranType.Enabled = false;
                ddlExchange.Enabled = false;
                ddlTradeAcc.Enabled = false;
                txtTradeDate.Enabled = false;
                btnSubmit.Visible = false;
                btnCalculate.Visible = false;



                // Hide the validation Divs. Not required for View Form

                dvBrokerage.Visible = false;
                dvExchange.Visible = false;
                dvNumShares.Visible = false;
                dvOtherCharge.Visible = false;
                dvRate.Visible = false;
                dvRateIncBrokerage.Visible = false;
                dvScrip.Visible = false;
                dvSTT.Visible = false;
                dvTax.Visible = false;
                dvTicker.Visible = false;
                dvTotal.Visible = false;
                dvTradeAccount.Visible = false;
                dvTradeDate.Visible = false;
                dvTransactionType.Visible = false;
            }
            else
            {
                txtTotal.Enabled = true;
                txtTicker.Enabled = true;
                txtTax.Enabled = true;
                txtSTT.Enabled = true;
                txtScrip.Enabled = true;
                txtRateIncBrokerage.Enabled = true;
                txtRate.Enabled = true;
                txtOtherCharge.Enabled = true;
                txtNumShares.Enabled = true;
                txtBrokerage.Enabled = true;
                txtBroker.Enabled = true;
                ddlTranType.Enabled = true;
                ddlExchange.Enabled = true;
                ddlTradeAcc.Enabled = true;
                txtTradeDate.Enabled = true;
                btnSubmit.Visible = true;
                btnSubmit.Text = "Update";
                btnCalculate.Visible = true;

                // Un-Hide the validation Divs for the Edit Form

                dvBrokerage.Visible = true;
                dvExchange.Visible = true;
                dvNumShares.Visible = true;
                dvOtherCharge.Visible = true;
                dvRate.Visible = true;
                dvRateIncBrokerage.Visible = true;
                dvScrip.Visible = true;
                dvSTT.Visible = true;
                dvTax.Visible = true;
                dvTicker.Visible = true;
                dvTotal.Visible = true;
                dvTradeAccount.Visible = true;
                dvTradeDate.Visible = true;
                dvTransactionType.Visible = true;
            }
        }
        private void LoadExchangeType(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetExchangeType(path);
                if (ddlExchange.Items.Count == 0)
                {
                    ddlExchange.DataTextField = "ExchangeName";
                    ddlExchange.DataValueField = "ExchangeCode";
                    ddlExchange.DataSource = dt;
                    ddlExchange.DataBind();
                    ddlExchange.Items.Insert(0, new ListItem("Select an Exchange", "Select an Exchange"));
                }
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityTransaction.ascx:LoadExchangeType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            SetViewFields(0);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EQTransactionVo newEqTransactionVo = new EQTransactionVo();
            decimal temp = 0;
            try
            {
                newEqTransactionVo.TransactionId = eqTransactionVo.TransactionId;
                newEqTransactionVo.IsSourceManual = eqTransactionVo.IsSourceManual;
                DataTable dt = productEqutiyBo.GetBrokerCode(portfolioId, ddlTradeAcc.SelectedItem.Text.ToString());
                if (txtBrokerage.Text != "")
                    newEqTransactionVo.Brokerage = float.Parse(txtBrokerage.Text);
                newEqTransactionVo.BrokerCode = dt.Rows[0]["XB_BrokerCode"].ToString();

                if (ddlTranType.SelectedItem.Text.ToString() == "Purchase")
                {
                    newEqTransactionVo.BuySell = "B";
                    newEqTransactionVo.TransactionCode = 1;
                }
                if (ddlTranType.SelectedItem.Text.ToString() == "Sell")
                {
                    newEqTransactionVo.BuySell = "S";
                    newEqTransactionVo.TransactionCode = 2;
                }
                if (ddlTranType.SelectedItem.Text.ToString() == "Holdings")
                {
                    newEqTransactionVo.BuySell = "B";
                    newEqTransactionVo.TransactionCode = 13;
                }
                newEqTransactionVo.CustomerId = customerVo.CustomerId;
                newEqTransactionVo.IsCorpAction = 0;
                newEqTransactionVo.EducationCess = (float)tempEducation;

                DataSet ds = productEqutiyBo.GetScripCode(txtScrip.Text.ToString());
                txtTicker.Text = ds.Tables[0].Rows[0]["PEM_Ticker"].ToString();
                scripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());
                newEqTransactionVo.TradeNum = 0;
                //long.Parse(ddlTradeAcc.SelectedItem.Text.ToString());
                newEqTransactionVo.AccountId = int.Parse(ddlTradeAcc.SelectedValue.ToString());
                newEqTransactionVo.ScripCode = scripCode;
                if (ddlExchange.SelectedItem.Value != "Select an Exchange")
                    newEqTransactionVo.Exchange = ddlExchange.SelectedItem.Value.ToString();
                else
                    newEqTransactionVo.Exchange = "NSE";
                if (txtOtherCharge.Text != "")
                    newEqTransactionVo.OtherCharges = float.Parse(txtOtherCharge.Text);
                newEqTransactionVo.Quantity = float.Parse(txtNumShares.Text);
                newEqTransactionVo.IsSpeculative = 0;
                newEqTransactionVo.TradeType = "D";
                newEqTransactionVo.AccountId = int.Parse(ddlTradeAcc.SelectedItem.Value.ToString());
                newEqTransactionVo.Rate = float.Parse(txtRate.Text);
                newEqTransactionVo.RateInclBrokerage = float.Parse(txtRateIncBrokerage.Text);
                temp = decimal.Round(Convert.ToDecimal(tempService), 3);
                if (txtTax.Text != "")
                    newEqTransactionVo.ServiceTax = float.Parse(txtTax.Text);
                if (txtSTT.Text != "")
                    newEqTransactionVo.STT = float.Parse(txtSTT.Text);
                newEqTransactionVo.TradeDate = DateTime.Parse(txtTradeDate.Text);

                newEqTransactionVo.TradeTotal = float.Parse(txtTotal.Text);

                customerTransactionBo.UpdateEquityTransaction(newEqTransactionVo, userVo.UserId);
                btnSubmit.Enabled = false;

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityTransaction.ascx:btnSubmit_Click()");
                object[] objects = new object[2];
                objects[0] = newEqTransactionVo;
                objects[1] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void txtScrip_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = productEqutiyBo.GetScripCode(txtScrip.Text.ToString());

            txtTicker.Text = ds.Tables[0].Rows[0]["PEM_Ticker"].ToString();
            scripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());
        }

        protected void ddlTradeAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTradeAcc.SelectedIndex != 0)
            {
                LoadBrokerCode();
            }
            else
            {
                txtBroker.Text = "";
            }
            SetBrokerage();
        }
        private void LoadBrokerCode()
        {
            ProductEquityBo productEquityBo = new ProductEquityBo();
            try
            {
                DataTable dt = productEquityBo.GetBrokerCode(portfolioId, ddlTradeAcc.SelectedItem.Text.ToString());
                txtBroker.Text = XMLBo.GetBrokerName(path, dt.Rows[0]["XB_BrokerCode"].ToString());
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityTransaction.ascx:LoadBrokerCode()");
                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            Calculate();

        }
        public void Calculate()
        {
            string alertMsg = "";
            double Stax = 0, STT = 0, rateIncBroker = 0, brokerage = 0, otherCharges = 0;

            if (txtRate.Text != "" && txtBroker.Text != "")
            {

                if (txtBrokerage.Text != "")
                    brokerage = double.Parse(txtBrokerage.Text);
                else
                    brokerage = 0;
                if (txtOtherCharge.Text != "")
                    otherCharges = double.Parse(txtOtherCharge.Text);
                else
                    otherCharges = 0;
                Stax = (12.36 / 100) * brokerage;
                // temp = decimal.Round((decimal)Stax, 2);
                // Stax = (double)temp;
                txtTax.Text = Math.Round(Stax, 4).ToString();


                // STT 

                STT = (0.125 / 100) * brokerage;
                //  temp = decimal.Round((decimal)STT, 2);
                // STT = (double)temp;
                txtSTT.Text = Math.Round(STT, 5).ToString();


                // Rate Inclusive Brokerage

                if (ddlTranType.SelectedItem.Text.ToString() == "Purchase")
                {
                    // rateIncBroker = double.Parse(decimal.Round((decimal)(double.Parse(txtRate.Text)) + (decimal)STT + (decimal)Stax + (decimal)(double.Parse(txtBrokerage.Text)) + (decimal)double.Parse(txtOtherCharge.Text), 2).ToString());
                    rateIncBroker = double.Parse(txtRate.Text) + STT + Stax + brokerage + otherCharges;

                }
                else
                {
                    //rateIncBroker = double.Parse(decimal.Round((decimal)(double.Parse(txtRate.Text)) - (decimal)STT - (decimal)Stax - (decimal)(double.Parse(txtBrokerage.Text)) - (decimal)double.Parse(txtOtherCharge.Text), 2).ToString());
                    rateIncBroker = double.Parse(txtRate.Text) - STT - Stax - brokerage - brokerage;
                }

                txtRateIncBrokerage.Text = Math.Round(rateIncBroker, 4).ToString();


            }
            if (txtNumShares.Text != "")
                txtTotal.Text = Math.Round(rateIncBroker * double.Parse(txtNumShares.Text), 4).ToString();
        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('EquityTransactionsView','none');", true);
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            SetBrokerage();
        }
        public void SetBrokerage()
        {
            if (txtRate.Text != "" && txtBroker.Text != "")
            {
                double brokerage, otherCharges;

                CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
                customerAccountsVo = customerTransactionBo.GetCustomerEQAccountDetails(int.Parse(ddlTradeAcc.SelectedItem.Value.ToString()), portfolioId);
                // Stax= Service Tax 
                if (eqTransactionVo.IsSpeculative != 1)
                    brokerage = double.Parse(txtRate.Text) * (customerAccountsVo.BrokerageDeliveryPercentage / 100);
                else
                    brokerage = double.Parse(txtRate.Text) * (customerAccountsVo.BrokerageSpeculativePercentage / 100);
                otherCharges = double.Parse(txtRate.Text) * (customerAccountsVo.OtherCharges / 100);
                txtBrokerage.Text = Math.Round(brokerage, 4).ToString();

                txtOtherCharge.Text = Math.Round(otherCharges, 4).ToString();

                Calculate();
            }
        }
    }
}