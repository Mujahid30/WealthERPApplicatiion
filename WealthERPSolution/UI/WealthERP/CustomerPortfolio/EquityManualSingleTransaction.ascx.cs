﻿using System;
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
        CustomerPortfolioVo customerPortfolioVo;
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
        static int schemePlanCode;
        Hashtable ht = new Hashtable();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        string path;
        AdvisorVo advisorVo = new AdvisorVo();
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            tdScripName.Visible = false;
            tdScripNameValue.Visible = false;
            //if (!IsPostBack)
            //{
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
                    LoadTransactionType(path);
                    LoadExchangeType(path);
                    LoadEquityTradeNumbers();
                }
                else
                {

                    portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                    // btnAddDP.Visible = false;
                }


                if (Session["Trade"] != null)
                {
                    ht = (Hashtable)Session["EqHT"];
                    txtScrip.Text = ht["Scrip"].ToString();
                    LoadExchangeType(path);
                    ddlExchange.SelectedValue = ht["Exchange"].ToString();
                    LoadTransactionType(path);
                    ddlTransactionType.SelectedValue = ht["TranType"].ToString();
                    txtTicker.Text = ht["Ticker"].ToString();
                    SetFields(1);
                    Session.Remove("Trade");

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
        //}
        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                genDictPortfolioDetails.Add(int.Parse(dr["CP_PortfolioId"].ToString()), int.Parse(dr["CP_IsMainPortfolio"].ToString()));
            }

            ddlPortfolio.SelectedValue = portfolioId.ToString();
            // customerPortfolioVo.PortfolioId = portfolioId;
            //customerPortfolioVo.PortfolioId = portfolioId;

            //var selectedValues = keysToSelect.Where(genDictPortfolioDetails.ContainsKey)
            //         .Select(x => genDictPortfolioDetails[x])
            //         .ToList();

            //  var evenFrenchNumbers =
            //from entry in genDictPortfolioDetails
            //where entry.Key == portfolioId
            //select entry.Value;


            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;

            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            Session["genDictPortfolioDetails"] = genDictPortfolioDetails;
            hdnIsCustomerLogin.Value = userVo.UserType;
        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            if (Session["genDictPortfolioDetails"] != null)
            {
                genDictPortfolioDetails = (Dictionary<int, int>)Session["genDictPortfolioDetails"];
            }
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
            //int value = keyValuePair.Value;           
            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;

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
                ddlTransactionType.DataSource = dt;
                ddlTransactionType.DataTextField = "TransactionName";
                ddlTransactionType.DataValueField = "TransactionCode";
                ddlTransactionType.DataBind();
                ddlTransactionType.Items.Insert(0, new ListItem("Select Transaction", "Select Transaction"));
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
                    ddlTradeAcc.Items.Insert(0, new ListItem("Select the Trade Number", "Select the Trade Number"));
                }
                else
                {

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
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Purchase")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 1;
                }
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Sell")
                {
                    eqTransactionVo.BuySell = "S";
                    eqTransactionVo.TransactionCode = 2;
                }
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Holdings")
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
                //eqTransactionVo.IsSpeculative = 0;
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
                eqTransactionVo.TradeAccountNum = "";

                if (customerTransactionBo.AddEquityTransaction(eqTransactionVo, customerVo.UserId))
                {
                    customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "EQ", eqTransactionVo.TradeDate);
                }

                List<EQPortfolioVo> eqPortfolioVoList = new List<EQPortfolioVo>();
                Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
                DateTime tradeDate = new DateTime();
                if (Session["ValuationDate"] != null)
                {
                    genDict = (Dictionary<string, DateTime>)Session["ValuationDate"];
                    tradeDate = DateTime.Parse(genDict["EQDate"].ToString());
                }
                if (tradeDate == DateTime.MinValue)
                {
                    tradeDate = DateTime.Today;
                }
                eqPortfolioVoList = customerPortfolioBo.GetCustomerEquityPortfolio(customerVo.CustomerId, portfolioId, tradeDate, txtScrip.Text, ddlTradeAcc.SelectedItem.Value.ToString());
                if (eqPortfolioVoList != null && eqPortfolioVoList.Count > 0)
                {
                    customerPortfolioBo.DeleteEquityNetPosition(eqPortfolioVoList[0].EQCode, eqPortfolioVoList[0].AccountId, tradeDate);
                    customerPortfolioBo.AddEquityNetPosition(eqPortfolioVoList[0], userVo.UserId);
                }
                msgRecordStatus.Visible = true;
                resetControls();
                //btnSubmit.Enabled = false;
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
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

        public void resetControls()
        {
            txtScrip.Text = string.Empty;
            ddlTransactionType.SelectedIndex = 0;
            ddlExchange.SelectedIndex = 0;
            ddlTradeAcc.SelectedIndex = 0;
            txtTicker.Text = string.Empty;
            txtTradeDate.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtNumShares.Text = string.Empty;
            txtBroker.Text = string.Empty;
            txtBrokerage.Text = string.Empty;
            txtOtherCharge.Text = string.Empty;
            txtTax.Text = string.Empty;
            txtSTT.Text = string.Empty;
            txtRateIncBrokerage.Text = string.Empty;
            txtTotal.Text = string.Empty;
            lblScripName.Visible = false;
        }

        protected void btnAddDP_Click(object sender, EventArgs e)
        {
            ht.Add("Scrip", txtScrip.Text.ToString());
            ht.Add("TranType", ddlTransactionType.SelectedItem.Value.ToString());
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
                //if (txtBrokerage.Text == "")
                //{
                if (rbtnDelivery.Checked)
                {
                    brokerage = double.Parse(txtRate.Text) * (customerAccountsVo.BrokerageDeliveryPercentage / 100);
                    txtBrokerage.Text = Math.Round(brokerage, 4).ToString();
                }
                else
                {
                    brokerage = double.Parse(txtRate.Text) * (customerAccountsVo.BrokerageSpeculativePercentage / 100);
                    txtBrokerage.Text = Math.Round(brokerage, 4).ToString();

                }
                //}

                //if (txtOtherCharge.Text == "")
                //{
                otherCharges = double.Parse(txtRate.Text) * (customerAccountsVo.OtherCharges / 100);
                if (txtOtherCharge.Text == "")
                    txtOtherCharge.Text = "0";
                if (otherCharges != 0)
                    txtOtherCharge.Text = Math.Round(otherCharges, 4).ToString();
                else
                    txtOtherCharge.Text = (double.Parse(txtRate.Text) * double.Parse(txtOtherCharge.Text) / 100).ToString();
                //}

                Calculate();
            }
        }

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransactionType.SelectedValue == "HOLD")
            {
                //Session["Holdings"] = ddlTransactionType.SelectedValue;
                trBroker.Visible = false;
                trBrokerage.Visible = false;
                trOthers.Visible = false;
                trRateInc.Visible = false;
                trServiceTax.Visible = false;
                trSTT.Visible = false;
                rbtnSpeculation.Visible = false;
            }
            else
            {
                SetFields(1);
                trTransactionMode.Visible = true;
                rbtnSpeculation.Visible = true;
            }
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


                txtTax.Text = Math.Round(Stax, 4).ToString();


                STT = (0.125 / 100) * brokerage;


                txtSTT.Text = Math.Round(STT, 5).ToString();


                // Rate Inclusive Brokerage

                if (ddlTransactionType.SelectedItem.Text.ToString() == "Purchase")
                {

                    rateIncBroker = double.Parse(txtRate.Text) + STT + Stax + brokerage + otherCharges;

                }
                else
                {

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

        protected void txtTradeDate_TextChanged(object sender, EventArgs e)
        {
            if (txtTradeDate.Text != "dd/mm/yyyy")
            {
                DateTime dt = Convert.ToDateTime(txtTradeDate.Text);
                txtPrice.Text = customerPortfolioBo.GetEQScripPrice(scripCode, dt).ToString();
            }
            else
            {
                txtPrice.Text = string.Empty;
            }



        }

        protected void txtScripCode_ValueChanged(object sender, EventArgs e)
        {
            DataSet ds = productEquityBo.GetScripCode(txtScrip.Text.ToString());
            lblScripName.Visible = true;
            // scripCode = productEquityBo.GetScripCode(txtScrip.Text.ToString());
            lblScripName.Text = txtScrip.Text;
            txtTicker.Text = ds.Tables[0].Rows[0]["PEM_Ticker"].ToString();
            scripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());
            autoCompleteExtender.ContextKey = schemePlanCode.ToString();
            trTransactionMode.Visible = true;
        }

        protected void btnUsePrice_Click(object sender, EventArgs e)
        {
            txtRate.Text = txtPrice.Text;
        }

        protected void txtNumShares_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRate.Text.Trim()) && (!string.IsNullOrEmpty(txtNumShares.Text.Trim())))
            {
                txtTotal.Text = (float.Parse(txtRate.Text) * float.Parse(txtNumShares.Text)).ToString();
            }
            else
            {
                txtNumShares.Text = string.Empty;
                txtTotal.Text = string.Empty;
            }
            SetBrokerage();
        }

        protected void rbtnDelivery_CheckedChanged(object sender, EventArgs e)
        {
            SetBrokerage();
        }

        protected void rbtnSpeculation_CheckedChanged(object sender, EventArgs e)
        {
            SetBrokerage();
        }


    }
}

