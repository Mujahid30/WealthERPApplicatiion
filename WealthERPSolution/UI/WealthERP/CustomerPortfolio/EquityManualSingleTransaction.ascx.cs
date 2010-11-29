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
using System.Globalization;

namespace WealthERP.CustomerPortfolio
{
    public partial class EquityManualSingleTransaction : System.Web.UI.UserControl
    {
        EQTransactionVo eqTransactionVo = new EQTransactionVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerVo customerVo;
        UserVo userVo;
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        ProductEquityBo productEquityBo = new ProductEquityBo();
        static float tempService;
        static float tempEducation;
        double tempSTT;
        double total;
        string flag;
        static int scripCode;
        int portfolioId;
        string trade = "";
        Hashtable ht = new Hashtable();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        string path;
        AdvisorVo advisorVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                customerVo = (CustomerVo)Session["CustomerVo"];
                userVo = (UserVo)Session["userVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                if (!IsPostBack)
                {

                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    lblScripName.Visible = false;


                }
                else
                {

                    portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                    // btnAddDP.Visible = false;
                }
                LoadExchangeType(path);
                LoadTransactionType(path);
                LoadEquityTradeNumbers();
                if (Session["Trade"] != null)
                {
                    ht = (Hashtable)Session["EqHT"];
                    txtScrip.Text = ht["Scrip"].ToString();
                    LoadExchangeType(path);
                    ddlExchange.SelectedValue = ht["Exchange"].ToString();
                    LoadTransactionType(path);
                    ddlTranType.SelectedValue = ht["TranType"].ToString();
                    txtTicker.Text = ht["Ticker"].ToString();
                    SetFields(1);
                    Session.Remove("Trade");

                }
                //trTransactionMode.Visible = false;
                //trScrip.Visible = false;
                //
                //LoadExchangeType(path);
                //SetFields(0);
                //if (Session["Trade"] != null)
                //{
                //    ht = (Hashtable)Session["EqHT"];
                //    txtScrip.Text = ht["Scrip"].ToString();
                //    ddlExchange.SelectedValue = ht["Exchange"].ToString();
                //    LoadExchangeType(path);
                //    ddlTranType.SelectedValue = ht["TranType"].ToString();
                //    LoadTransactionType(path);
                //    txtTicker.Text = ht["Ticker"].ToString();
                //    SetFields(1);
                //    Session.Remove("Trade");




            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityManualSingleTransaction.ascx:Page_Load()");
                object[] objects = new object[5];
                objects[0] = path;
                objects[1] = userVo;
                objects[2] = customerVo;
                objects[3] = portfolioId;
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

        }
        private void SetFields(int i)
        {
            if (i == 0)
            {
                trBroker.Visible = false;
                trBrokerage.Visible = false;
                trNoShares.Visible = false;
                trOthers.Visible = false;
                trRate.Visible = false;
                trRateInc.Visible = false;
                lblScripName.Visible = false;
                trServiceTax.Visible = false;
                trSTT.Visible = false;
                trTotal.Visible = false;
                trTradeDate.Visible = false;
            }
            else
            {
                trBroker.Visible = true;
                trBrokerage.Visible = true;
                trNoShares.Visible = true;
                trOthers.Visible = true;
                trRate.Visible = true;
                trRateInc.Visible = true;

                trServiceTax.Visible = true;
                trSTT.Visible = true;
                trTotal.Visible = true;
                trTradeDate.Visible = true;
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
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityManualSingleTransaction.ascx:LoadTransactionType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        private void LoadExchangeType(string path)
        {
            try
            {
                DataTable dt = XMLBo.GetExchangeType(path);
                ddlExchange.DataSource = dt;
                ddlExchange.DataTextField = "ExchangeName";
                ddlExchange.DataValueField = "ExchangeCode";
                ddlExchange.DataBind();
                ddlExchange.Items.Insert(0, new ListItem("Select an Exchange", "Select an Exchange"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityManualSingleTransaction.ascx:LoadExchangeType()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void LoadEquityTradeNumbers()
        {
            DataSet dsEqutiyTradeNumbers;
            try
            {
                dsEqutiyTradeNumbers = customerAccountBo.GetCustomerEQAccounts(portfolioId, "DE");
                if (dsEqutiyTradeNumbers.Tables[0].Rows.Count == 0)
                {
                    btnAddDP.Visible = true;
                }
                else
                {
                    // btnAddDP.Visible = false;
                    DataTable dtCustomerAccounts = dsEqutiyTradeNumbers.Tables[0];
                    ddlTradeAcc.DataSource = dtCustomerAccounts;
                    ddlTradeAcc.DataTextField = "CETA_TradeAccountNum";
                    ddlTradeAcc.DataValueField = "CETA_AccountId";
                    ddlTradeAcc.DataBind();
                    ddlTradeAcc.Items.Insert(0, new ListItem("Select the Trade Number", "Select the Trade Number"));
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
                FunctionInfo.Add("Method", "EquityManualSingleTransaction.ascx:LoadEquityTradeNumbers()");
                object[] objects = new object[1];
                objects[0] = portfolioId; ;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            decimal temp;
            DataTable dt;
            CultureInfo ci = new CultureInfo("en-GB");
            try
            {
                dt = productEquityBo.GetBrokerCode(portfolioId, ddlTradeAcc.SelectedItem.Text.ToString());

                eqTransactionVo.IsSourceManual = 1;
                if (txtBrokerage.Text != "")
                    eqTransactionVo.Brokerage = float.Parse(txtBrokerage.Text);
                eqTransactionVo.BrokerCode = dt.Rows[0]["XB_BrokerCode"].ToString();
                if (ddlTranType.SelectedItem.Text.ToString() == "Purchase")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 1;
                }
                if (ddlTranType.SelectedItem.Text.ToString() == "Sell")
                {
                    eqTransactionVo.BuySell = "S";
                    eqTransactionVo.TransactionCode = 2;
                }
                if (ddlTranType.SelectedItem.Text.ToString() == "Holdings")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 13;
                }


                eqTransactionVo.CustomerId = customerVo.CustomerId;
                eqTransactionVo.IsCorpAction = 0;
                if (rbtnDelivery.Checked)
                {
                    eqTransactionVo.IsSpeculative = 0;
                }
                if (rbtnSpeculation.Checked)
                {
                    eqTransactionVo.IsSpeculative = 1;
                }

                eqTransactionVo.EducationCess = (float)tempEducation;
                eqTransactionVo.ScripCode = scripCode;
                if (ddlExchange.SelectedItem.Value.ToString() != "Select an Exchange")
                    eqTransactionVo.Exchange = ddlExchange.SelectedItem.Value.ToString();
                else
                    eqTransactionVo.Exchange = "NSE";
                if (txtOtherCharge.Text != "")
                    eqTransactionVo.OtherCharges = float.Parse(txtOtherCharge.Text);
                eqTransactionVo.Quantity = float.Parse(txtNumShares.Text);
                eqTransactionVo.IsSpeculative = 0;
                eqTransactionVo.TradeType = "D";



                eqTransactionVo.AccountId = int.Parse(ddlTradeAcc.SelectedItem.Value.ToString());
                eqTransactionVo.Rate = float.Parse(txtRate.Text);
                eqTransactionVo.RateInclBrokerage = float.Parse(txtRateIncBrokerage.Text);
                temp = decimal.Round(Convert.ToDecimal(tempService), 3);
                if (txtTax.Text != "")
                    eqTransactionVo.ServiceTax = float.Parse(txtTax.Text);
                if (txtSTT.Text != "")
                    eqTransactionVo.STT = float.Parse(txtSTT.Text);

                eqTransactionVo.TradeDate = Convert.ToDateTime(txtTradeDate.Text.Trim(), ci);// DateTime.Parse(txtTradeDate.Text);//ddlDay.SelectedItem.Text.ToString() + "/" + ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYear.SelectedItem.Value.ToString()
                eqTransactionVo.TradeTotal = float.Parse(txtTotal.Text);
                eqTransactionVo.TradeNum = 0;
                //long.Parse(ddlTradeAcc.SelectedItem.Text.ToString());
                if (customerTransactionBo.AddEquityTransaction(eqTransactionVo, customerVo.UserId))
                {
                    customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "EQ", eqTransactionVo.TradeDate);
                }
                btnSubmit.Enabled = false;

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "EquityManualSingleTransaction.ascx:btnSubmit_Click()");
                object[] objects = new object[3];
                objects[0] = portfolioId; ;
                objects[1] = eqTransactionVo;
                objects[2] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void btnAddDP_Click(object sender, EventArgs e)
        {
            ht.Add("Scrip", txtScrip.Text.ToString());
            ht.Add("TranType", ddlTranType.SelectedItem.Value.ToString());
            ht.Add("Exchange", ddlExchange.SelectedItem.Value.ToString());
            ht.Add("Ticker", txtTicker.Text.ToString());
            Session["EqHT"] = ht;
            Session["Trade"] = "NewTrade";
            string queryString = "?prevPage=EquityManualSingleTransaction";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountAdd','" + queryString + "');", true);
        }



        protected void txtBrokerage_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txtOtherCharge_TextChanged(object sender, EventArgs e)
        {

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
                if (rbtnDelivery.Checked)
                    brokerage = double.Parse(txtRate.Text) * (customerAccountsVo.BrokerageDeliveryPercentage / 100);
                else
                    brokerage = double.Parse(txtRate.Text) * (customerAccountsVo.BrokerageSpeculativePercentage / 100);
                otherCharges = double.Parse(txtRate.Text) * (customerAccountsVo.OtherCharges / 100);
                if(txtBrokerage.Text=="")
                    txtBrokerage.Text = Math.Round(brokerage, 4).ToString();             

                if(txtOtherCharge.Text=="")
                    txtOtherCharge.Text = Math.Round(otherCharges, 4).ToString();

                Calculate();
            }
        }

        protected void txtScrip_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = productEquityBo.GetScripCode(txtScrip.Text.ToString());
            lblScripName.Visible = true;
            // scripCode = productEquityBo.GetScripCode(txtScrip.Text.ToString());
            lblScripName.Text = txtScrip.Text;
            txtTicker.Text = ds.Tables[0].Rows[0]["PEM_Ticker"].ToString();
            scripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());
            trTransactionMode.Visible = true;
        }

        protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFields(1);
            trTransactionMode.Visible = true;

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            Calculate();
        }
        private void Calculate()
        {

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
        protected void ddlTradeAcc_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTradeAcc.SelectedIndex != 0)
            {
                LoadBrokerCode();
            }
            else
            {
                txtBroker.Text = "";
                txtBroker.Enabled = true;
            }

            SetBrokerage();
        }

        private void LoadBrokerCode()
        {
            ProductEquityBo productEquityBo = new ProductEquityBo();
            try
            {

                DataTable dt = productEquityBo.GetBrokerCode(portfolioId, ddlTradeAcc.SelectedItem.Text.ToString());
                if (dt.Rows[0]["XB_BrokerCode"].ToString() != "")
                {
                    txtBroker.Text = XMLBo.GetBrokerName(path, dt.Rows[0]["XB_BrokerCode"].ToString());
                    txtBroker.Enabled = false;
                }
                else
                {
                    txtBroker.Text = "";
                    txtBroker.Enabled = true;
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
                FunctionInfo.Add("Method", "EquityManualSingleTransaction.ascx:LoadBrokerCode()");
                object[] objects = new object[2];
                objects[0] = path;
                objects[1] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void txtScrip_DataBinding(object sender, EventArgs e)
        {

        }

    }
}