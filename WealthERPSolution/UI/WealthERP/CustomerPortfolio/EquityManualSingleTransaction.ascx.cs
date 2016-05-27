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
        int AddmoreFlag = 0;
        double facevalue;
        int NoOfShares;
        double SebiPerRate;
        double TrxnPerRate;
        double StampPerRate;
        DateTime TradeDate;
        Hashtable ht = new Hashtable();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        string path;
        string Type;
        string exchangetype;
        int TransactionMode;
        AdvisorVo advisorVo = new AdvisorVo();
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();
        ProductEquityBo productEqutiyBo = new ProductEquityBo();

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
                Session["DematDetailsView"] = "Add";
                if (advisorVo.A_IsFamilyOffice == true)
                {
                    DivManaged.Visible = true;

                }
                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                BindPortfolioDropDown();
                lblScripName.Visible = false;



                if (rbtnDomestic.Checked == true)
                {
                    exchangetype = "Dom";
                }
                else if (rbtnInternational.Checked == true)
                {
                    exchangetype = "Int";
                }

                LoadExchangeType(exchangetype);
                LoadDematAccountNumber(portfolioId);

                if (!IsPostBack)
                {
                    LoadEquityTradeNumbers();
                    LoadManagedby();
                    BindTransactionType();
                    if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                    {
                        eqTransactionVo = (EQTransactionVo)Session["EquityTransactionVo"];
                        BindTransactionType();
                        EQAccountDetails();
                        ShowFields(eqTransactionVo.TransactionCode);
                        string a = Request.QueryString["Currency"].ToString();


                        if (Request.QueryString["action"].Trim() == "Edit")
                        {
                            if (Request.QueryString["Currency"].Trim() == "INR")
                            {

                                BtnSetVisiblity(1);
                                setvisibility(1);
                            }
                            else
                            {
                                BtnSetVisiblity(0);
                                setvisibility(0);
                            }


                        }
                        else if (Request.QueryString["action"].Trim() == "View")
                        {
                            BtnSetVisiblity(0);
                            setvisibility(0);

                        }
                    }

                }
                else
                {
                    portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
                }


                if (Session["Trade"] != null)
                {
                    ht = (Hashtable)Session["EqHT"];
                    txtScrip.Text = ht["Scrip"].ToString();
                    LoadExchangeType(exchangetype);
                    ddlExchange.SelectedValue = ht["Exchange"].ToString();
                    BindTransactionType();
                    ddlTransactionType.SelectedValue = ht["TranType"].ToString();
                    lblticker.Text = ht["Ticker"].ToString();
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


        private void EQAccountDetails()
        {

            if (eqTransactionVo.ManagedBy != 0)
                ddl_Managedby.SelectedValue = eqTransactionVo.ManagedBy.ToString();

            if (eqTransactionVo.DematAccountNo != 0)
                Ddl_dematAcc.SelectedValue = eqTransactionVo.DematAccountNo.ToString();
            ddlTradeAcc.SelectedValue = eqTransactionVo.AccountId.ToString();
            TxtBillNo.Text = eqTransactionVo.BillNo;
            TxtSettlemntNo.Text = eqTransactionVo.SettlementNo;

            if (eqTransactionVo.SettlementDate != DateTime.MinValue)
                txtSettlmntDate.SelectedDate = eqTransactionVo.SettlementDate;
            if (eqTransactionVo.BrokerCode != "")
            {
                txtBroker.Text = XMLBo.GetBrokerName(path, eqTransactionVo.BrokerCode);
            }
            if (eqTransactionVo.Type != null)
                ddl_type.SelectedValue = eqTransactionVo.Type.ToString();
            txtScrip.Text = productEqutiyBo.GetScripName(eqTransactionVo.ScripCode);
            if (eqTransactionVo.Exchange.ToString() != "")
                ddlExchange.SelectedValue = eqTransactionVo.Exchange.ToString();
            if (eqTransactionVo.Exchange.ToString() == "NSE" || eqTransactionVo.Exchange.ToString() == "BSE")
            {
                rbtnDomestic.Checked = true;
            }
            else if (eqTransactionVo.Exchange.ToString() == "NASDAQ" || eqTransactionVo.Exchange.ToString() == "NYSE")
            {
                rbtnInternational.Checked = true;
            }
            txtInternationalRates.Text = eqTransactionVo.FXCurencyRate.ToString();
            ddlInternationalCurrency.SelectedValue = eqTransactionVo.FXCurencyType;
            ddlTransactionMode.SelectedValue = eqTransactionVo.IsSpeculative.ToString();
            ddlTransactionType.SelectedValue = eqTransactionVo.TransactionCode.ToString();

            if (eqTransactionVo.TransactionCode == 6 && eqTransactionVo.BuySell == "B")
            {
                ddlTransactionType.SelectedValue = 6.ToString();
                DataSet ds = productEquityBo.GetScripCode(txtScrip.Text.ToString());
                int ScripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());
                txtTradeDate.Text = eqTransactionVo.TradeDate.ToShortDateString().ToString();
                LoadDividendHistory(ScripCode);
                if (!string.IsNullOrEmpty(eqTransactionVo.DailyCorpAxnId.ToString()))
                {
                    if (eqTransactionVo.DailyCorpAxnId != 0)
                    {
                        ddl_divhistory.SelectedValue = eqTransactionVo.DailyCorpAxnId.ToString();
                        txtTradeDate.Enabled = false;
                    }
                }
                Txt_NoOfSharesForDiv.Text = Convert.ToDouble(eqTransactionVo.NoOfSharesForDiv).ToString();
                if (eqTransactionVo.DividendRecieved == true)
                {
                    ddl_DivStatus.SelectedItem.Text = "Recieved";

                }
                else if (eqTransactionVo.DividendRecieved == false)
                {
                    ddl_DivStatus.SelectedItem.Text = "Recievable";
                }
                txtbankrefno.Text = eqTransactionVo.BankReferenceNo;
                Txt_DivAmount.Text = Convert.ToDouble(eqTransactionVo.TradeTotal).ToString();

            }
            else
            {
                txtTradeDate.Text = eqTransactionVo.TradeDate.ToShortDateString().ToString();
                txtRate.Text = eqTransactionVo.Rate.ToString();
                DateTime dt = Convert.ToDateTime(txtTradeDate.Text);
                txtPrice.Text = customerTransactionBo.GetEQScripPrice(eqTransactionVo.ScripCode, dt, eqTransactionVo.Currency).ToString();
                txtNumShares.Text = Convert.ToDouble(eqTransactionVo.Quantity).ToString();
                txtBrokerage.Text = eqTransactionVo.Brokerage.ToString();
                txtRateIncBrokerage.Text = eqTransactionVo.RateInclBrokerage.ToString();
                txtSebiTurnOver.Text = Math.Round(eqTransactionVo.SebiTurnOverFee, 6).ToString();
                TxtTrxnChrgs.Text = Math.Round(eqTransactionVo.TransactionCharges, 6).ToString();
                TxtStampCharges.Text = Math.Round(eqTransactionVo.StampCharges, 6).ToString();
                txtTax.Text = Math.Round(eqTransactionVo.ServiceTax, 6).ToString();
                txtSTT.Text = Math.Round(eqTransactionVo.STT, 6).ToString();

                if (txtRate.Text != "")
                    txtGrossConsideration.Text = Math.Round(eqTransactionVo.TradeTotal, 4).ToString();
                if (txtBrokerage.Text != "" && txtNumShares.Text != "")
                {
                    txtbrokerageamt.Text = Math.Round((double.Parse(txtBrokerage.Text) * double.Parse(txtNumShares.Text)), 4).ToString();
                }
                if (txtNumShares.Text != "")
                    TxtTradetotalIncBrkrg.Text = Math.Round(eqTransactionVo.TradeTotalIncBrokerage, 4).ToString();

                txtSebiTurnOverAmount.Text = Math.Round(eqTransactionVo.SebiTurnOverFee * double.Parse(txtNumShares.Text), 4).ToString();
                txtStampChargeAmt.Text = Math.Round(eqTransactionVo.StampCharges * double.Parse(txtNumShares.Text), 4).ToString();
                txtTrxnAmount.Text = Math.Round(eqTransactionVo.TransactionCharges * double.Parse(txtNumShares.Text), 4).ToString();
                txtServiceAmt.Text = Math.Round(eqTransactionVo.ServiceTax * double.Parse(txtNumShares.Text), 4).ToString();
                txtSttAmt.Text = Math.Round(double.Parse(eqTransactionVo.STT.ToString()) * double.Parse(txtNumShares.Text), 4).ToString();
                trothercharge.Visible = true;
                txt_OtherCharges.Text = Math.Round(eqTransactionVo.OtherCharges, 6).ToString();
                TxtOtherChargeAmt.Text = Math.Round(double.Parse(eqTransactionVo.OtherCharges.ToString()) * double.Parse(txtNumShares.Text), 6).ToString();
                TxtDiffInBrkrg.Text = eqTransactionVo.DifferenceInBrokerage.ToString();
                if (txtNumShares.Text != "")
                {
                    txtAdditionalAdjAmt.Text = Math.Round((double.Parse(TxtDiffInBrkrg.Text) * double.Parse(txtNumShares.Text)), 4).ToString();
                }

                TxtRateInBrkrgAllCharges.Text = (eqTransactionVo.RateIncBrokerageAllCharges).ToString();
                txtTotal.Text = Math.Round(eqTransactionVo.GrossConsideration, 4).ToString();
                Txt_DematCharge.Text = Math.Round(float.Parse(eqTransactionVo.DematCharge.ToString()), 4).ToString();
                if (eqTransactionVo.DematAccountNo == 0 || eqTransactionVo.DematAccountNo.ToString() != "")
                    trDematAndOtherCharg.Visible = false;

            }
        }


        private void BtnSetVisiblity(int p)
        {
            if (p == 1)
            {
                btnUpdate.Visible = true;
                btnSubmit.Visible = false;
                Bttn_SubmitAddmore.Visible = false;
                lnkBack.Visible = true;
                lnkEdit.Visible = false;
            }
            else
            {
                btnUpdate.Visible = false;
                btnSubmit.Visible = false;
                Bttn_SubmitAddmore.Visible = false;
                lnkBack.Visible = true;
                lnkEdit.Visible = true;
            }
        }
        private void setvisibility(int p)
        {
            if (p == 1)
            {
                ddl_Managedby.Enabled = true;
                ddlTradeAcc.Enabled = true;
                TxtBillNo.Enabled = true;
                TxtSettlemntNo.Enabled = true;
                Ddl_dematAcc.Enabled = true;
                txtSettlmntDate.Enabled = true;
                txtScrip.Enabled = true;
                ddl_type.Enabled = true;
                ddlTransactionType.Enabled = true;
                ddlExchange.Enabled = true;
                lblticker.Enabled = true;
                txtTradeDate.Enabled = true;
                txtPrice.Enabled = true;
                txtRate.Enabled = true;
                txtNumShares.Enabled = true;
                txtBrokerage.Enabled = true;
                txtTax.Enabled = true;
                txtSTT.Enabled = true;
                txtRateIncBrokerage.Enabled = true;
                txtTotal.Enabled = true;
                lblScripName.Enabled = true;
                ddl_divhistory.Enabled = true;
                Txt_NoOfSharesForDiv.Enabled = true;
                Txt_DivAmount.Enabled = true;
                ddl_DivStatus.Enabled = true;
                txtSebiTurnOver.Enabled = true;
                TxtTrxnChrgs.Enabled = true;
                TxtStampCharges.Enabled = true;
                TxtDiffInBrkrg.Enabled = true;
                Txt_DematCharge.Enabled = true;
                TxtTradetotalIncBrkrg.Enabled = true;
                txtbrokerageamt.Enabled = true;
                txtSebiTurnOverAmount.Enabled = true;
                txtTrxnAmount.Enabled = true;
                txtStampChargeAmt.Enabled = true;
                txtServiceAmt.Enabled = true;
                TxtOtherChargeAmt.Enabled = true;
                txt_OtherCharges.Enabled = true;
                txtSttAmt.Enabled = true;
                txtAdditionalAdjAmt.Enabled = true;
                TxtRateInBrkrgAllCharges.Enabled = true;
                btnUsePrice.Enabled = true;
                BtnCalculateDiv.Enabled = true;
                txtGrossConsideration.Enabled = true;
                btnAdddividend.Enabled = true;
                ddlTransactionMode.Enabled = true;
                rbtnInternational.Enabled = true;
                rbtnDomestic.Enabled = true;
                txtRemark.Enabled = true;
                txtFromDate.Enabled = true;
            }
            else
            {
                ddl_Managedby.Enabled = false;
                ddlTradeAcc.Enabled = false;
                TxtBillNo.Enabled = false;
                TxtSettlemntNo.Enabled = false;
                Ddl_dematAcc.Enabled = false;
                txtSettlmntDate.Enabled = false;
                txtScrip.Enabled = false;
                ddl_type.Enabled = false;
                ddlTransactionType.Enabled = false;
                ddlExchange.Enabled = false;
                lblticker.Enabled = false;
                txtTradeDate.Enabled = false;
                txtPrice.Enabled = false;
                txtRate.Enabled = false;
                txtNumShares.Enabled = false;
                txtBrokerage.Enabled = false;
                txtTax.Enabled = false;
                txtSTT.Enabled = false;
                txtRateIncBrokerage.Enabled = false;
                txtTotal.Enabled = false;
                lblScripName.Enabled = false;
                ddl_divhistory.Enabled = false;
                Txt_NoOfSharesForDiv.Enabled = false;
                Txt_DivAmount.Enabled = false;
                ddl_DivStatus.Enabled = false;
                txtSebiTurnOver.Enabled = false;
                TxtTrxnChrgs.Enabled = false;
                TxtStampCharges.Enabled = false;
                TxtDiffInBrkrg.Enabled = false;
                Txt_DematCharge.Enabled = false;
                TxtTradetotalIncBrkrg.Enabled = false;
                txtbrokerageamt.Enabled = false;
                txtSebiTurnOverAmount.Enabled = false;
                txtTrxnAmount.Enabled = false;
                txtStampChargeAmt.Enabled = false;
                txtServiceAmt.Enabled = false;
                TxtOtherChargeAmt.Enabled = false;
                txt_OtherCharges.Enabled = false;
                txtSttAmt.Enabled = false;
                txtAdditionalAdjAmt.Enabled = false;
                TxtRateInBrkrgAllCharges.Enabled = false;
                btnUsePrice.Enabled = false;
                BtnCalculateDiv.Enabled = false;
                txtGrossConsideration.Enabled = false;
                btnAdddividend.Enabled = false;
                ddlTransactionMode.Enabled = false;
                rbtnInternational.Enabled = false;
                rbtnDomestic.Enabled = false;
                txtRemark.Enabled = false;
                txtFromDate.Enabled = false;
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            decimal temp;
            DataTable dt;
            CultureInfo ci = new CultureInfo("en-GB");
            eqTransactionVo.TransactionId = Convert.ToInt32(Session["TransactionId"]);
            eqTransactionVo.CustomerId = customerVo.CustomerId;

            DataSet ds = productEquityBo.GetScripCode(txtScrip.Text.ToString());
            if (ddlTransactionType.SelectedValue != "14" && ddlTransactionType.SelectedValue != "Select")
            {
                eqTransactionVo.ScripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());
            }
            if (ddl_Managedby.SelectedValue != "Select")
            {
                eqTransactionVo.ManagedBy = Convert.ToInt32(ddl_Managedby.SelectedValue);
            }
            if (Ddl_dematAcc.SelectedIndex != 0)
                eqTransactionVo.DemateAccountId = int.Parse(Ddl_dematAcc.SelectedItem.Value.ToString());
            eqTransactionVo.AccountId = int.Parse(ddlTradeAcc.SelectedItem.Value.ToString());

            if (txtInternationalRates.Text != "")
            {
                if (float.Parse(txtInternationalRates.Text) != 0)
                    eqTransactionVo.FXCurencyType = ddlInternationalCurrency.SelectedValue;
                else
                    eqTransactionVo.FXCurencyType = String.Empty; ;
            }
            else
            {
                eqTransactionVo.FXCurencyType = String.Empty;
            }

            if (!string.IsNullOrEmpty(txtInternationalRates.Text))
            {
                eqTransactionVo.FXCurencyRate = Convert.ToDouble(txtInternationalRates.Text);
            }
            else
            {
                eqTransactionVo.FXCurencyRate = 0.0;
            }

            if (ddl_type.SelectedItem.Text != "Select")
            {
                if (ddl_type.SelectedItem.Text == "Core Investment")
                {
                    eqTransactionVo.Type = 1;
                }
                else
                {
                    eqTransactionVo.Type = 0;
                }
            }
            else
            {
                eqTransactionVo.Type = 2;
            }
            if (TxtBillNo.Text != "")
                eqTransactionVo.BillNo = TxtBillNo.Text.ToString();
            if (TxtSettlemntNo.Text != "")
                eqTransactionVo.SettlementNo = TxtSettlemntNo.Text.ToString();
            if (txtSettlmntDate.SelectedDate != null || txtSettlmntDate.SelectedDate != DateTime.MinValue)
                eqTransactionVo.SettlementDate = Convert.ToDateTime(txtSettlmntDate.SelectedDate);

            if (ddlTransactionType.SelectedItem.Text.ToString() == "Dividend")
            {
                eqTransactionVo.BuySell = "B";
                eqTransactionVo.TransactionCode = 6;
                var DivHistory = ddl_divhistory.SelectedItem.Text.ToString();
                if (DivHistory != "Dividend Declared Date,Dividend Percentage,Face Value")
                {
                    var List = DivHistory.Split(',');
                    TradeDate = DateTime.Parse(List[0]);
                    eqTransactionVo.TradeDate = TradeDate;
                    eqTransactionVo.DailyCorpAxnId = Convert.ToInt16(ddl_divhistory.SelectedValue);

                }
                else
                {
                    eqTransactionVo.TradeDate = DateTime.Parse(txtTradeDate.Text);
                }
                if (ddl_DivStatus.SelectedValue != "Select")
                    eqTransactionVo.DividendRecieved = Boolean.Parse(ddl_DivStatus.SelectedValue);
                if (Txt_NoOfSharesForDiv.Text != "")
                    eqTransactionVo.NoOfSharesForDiv = double.Parse(Txt_NoOfSharesForDiv.Text);
                eqTransactionVo.BankReferenceNo = txtbankrefno.Text;
                eqTransactionVo.TradeTotal = double.Parse(Txt_DivAmount.Text);
                eqTransactionVo.GrossConsideration = double.Parse(Txt_DivAmount.Text);
                eqTransactionVo.TradeTotalIncBrokerage = double.Parse(Txt_DivAmount.Text);



                customerTransactionBo.UpdateEquityTransaction(eqTransactionVo, userVo.UserId);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','none');", true);
            }
            else
            {
                dt = productEquityBo.GetBrokerCode(portfolioId, ddlTradeAcc.SelectedItem.Text.ToString());

                eqTransactionVo.IsSourceManual = 1;
                if (txtBrokerage.Text != "")
                    eqTransactionVo.Brokerage = float.Parse(txtBrokerage.Text);
                eqTransactionVo.BrokerCode = dt.Rows[0]["XB_BrokerCode"].ToString();
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Buy")
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
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Stocks Split")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 3;
                }
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Bonus Issue")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 4;
                }
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Right Issue")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 5;
                }

                if (ddlTransactionType.SelectedItem.Text.ToString() == "Amalgamtion")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 7;
                }

                if (ddlTransactionType.SelectedItem.Text.ToString() == "Capital Reduction")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 8;
                }

                if (ddlTransactionType.SelectedItem.Text.ToString() == "Merger")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 9;
                }
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Demerger")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 10;
                }
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Buy Back Offer")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 11;
                }
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Open Offer")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 12;
                }
                if (ddlTransactionType.SelectedValue == "14")
                {
                    eqTransactionVo.BuySell = "S";
                    eqTransactionVo.TransactionCode = 14;
                }
                eqTransactionVo.IsCorpAction = 0;
                if (ddlTransactionMode.SelectedItem.Text == "Speculative")
                {
                    eqTransactionVo.IsSpeculative = 1;
                }
                else if (ddlTransactionMode.SelectedItem.Text == "Delivery")
                {
                    eqTransactionVo.IsSpeculative = 0;
                }
                eqTransactionVo.EducationCess = (float)tempEducation;

                if (ddlExchange.SelectedItem.Value.ToString() != "Select an Exchange")
                    eqTransactionVo.Exchange = ddlExchange.SelectedItem.Value.ToString();
                else
                    eqTransactionVo.Exchange = "NSE";

                if (txt_OtherCharges.Text != "")
                {
                    hdnOtherCharge.Value = txt_OtherCharges.Text;
                    eqTransactionVo.OtherCharges = float.Parse(hdnOtherCharge.Value);
                   
                }
                if (txtNumShares.Text != "")
                {
                    eqTransactionVo.Quantity = float.Parse(txtNumShares.Text);
                }
                eqTransactionVo.TradeType = "D";
                if (txtRate.Text != "")
                {
                    eqTransactionVo.Rate = float.Parse(txtRate.Text);
                }
                if (txtRateIncBrokerage.Text != "")
                {
                    eqTransactionVo.RateInclBrokerage = float.Parse(txtRateIncBrokerage.Text);

                }
                temp = decimal.Round(Convert.ToDecimal(tempService), 3);
                if (txtTax.Text != "")
                {
                    hdnServiceTax.Value = txtTax.Text;
                }
                if (txtTax.Text != "")
                {
                    eqTransactionVo.ServiceTax = float.Parse(txtTax.Text);
                }
                if (txtSTT.Text != "")
                {
                    hdnStt.Value = txtSTT.Text;
                }
                if (txtSTT.Text != "")
                {
                    eqTransactionVo.STT = float.Parse(txtSTT.Text);
                }
                if (txtTradeDate.Text != "")
                {
                    eqTransactionVo.TradeDate = Convert.ToDateTime(txtTradeDate.Text.Trim(), ci);
                }
                if (txtTotal.Text != "")
                {
                    eqTransactionVo.GrossConsideration = double.Parse(txtTotal.Text);

                }
                eqTransactionVo.TradeAccountNum = "";

                if (txtSebiTurnOver.Text != "")
                {
                    hdnSebiCharge.Value = txtSebiTurnOver.Text;
                }
                if (txtSebiTurnOver.Text != "")
                {
                    eqTransactionVo.SebiTurnOverFee = double.Parse(txtSebiTurnOver.Text);
                }
                if (TxtTrxnChrgs.Text != "")
                {
                    hdnTrxnCharge.Value = TxtTrxnChrgs.Text;
                }
                if (TxtTrxnChrgs.Text != "")
                {
                    eqTransactionVo.TransactionCharges = double.Parse(TxtTrxnChrgs.Text);
                }
                if (TxtStampCharges.Text != "")
                {
                    hdnStampCharge.Value = TxtStampCharges.Text;
                }
                if (TxtStampCharges.Text != "")
                {
                    eqTransactionVo.StampCharges = double.Parse(TxtStampCharges.Text);
                }
                if (TxtDiffInBrkrg.Text != "")
                {
                    eqTransactionVo.DifferenceInBrokerage = double.Parse(TxtDiffInBrkrg.Text);
                }
                if (Txt_DematCharge.Text != "" && Ddl_dematAcc.SelectedIndex != 0)
                    eqTransactionVo.DematCharge = float.Parse(Txt_DematCharge.Text);
                if (TxtTradetotalIncBrkrg.Text != "")
                {
                    eqTransactionVo.TradeTotalIncBrokerage = double.Parse(TxtTradetotalIncBrkrg.Text);
                }
                if (TxtRateInBrkrgAllCharges.Text != "")
                {
                    eqTransactionVo.RateIncBrokerageAllCharges = double.Parse(TxtRateInBrkrgAllCharges.Text);
                }
                eqTransactionVo.TradeTotal = double.Parse(txtGrossConsideration.Text);
                if (txtRemark.Text != "")
                {
                    eqTransactionVo.Remark = txtRemark.Text;
                }
               
                customerTransactionBo.UpdateEquityTransaction(eqTransactionVo, userVo.UserId);
                msgUpdatedStatus.Visible = true;
            }
        }

        private void LoadManagedby()
        {
            DataSet dsManagedby = customerTransactionBo.GetManagedby(advisorVo.advisorId);
            ddl_Managedby.DataSource = dsManagedby;
            ddl_Managedby.DataValueField = "C_CustomerId";
            ddl_Managedby.DataTextField = "C_FirstName";
            ddl_Managedby.DataBind();
            ddl_Managedby.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void LoadType()
        {
            DataSet dsGetType = customerTransactionBo.GetType();
            ddl_type.DataSource = dsGetType;
            ddl_type.DataValueField = "WCMV_LookupId";
            ddl_type.DataTextField = "WCMV_Name";
            ddl_type.DataBind();
            ddl_type.Items.Insert(0, new ListItem("Select", "Select"));
        }
        private void LoadDematAccountNumber(int portfolioId)
        {
            DataSet dsDmatAcc = customerTransactionBo.GetDematAccountNumber(portfolioId);
            Ddl_dematAcc.DataSource = dsDmatAcc;
            Ddl_dematAcc.DataValueField = "CEDA_DematAccountId";
            Ddl_dematAcc.DataTextField = "CEDA_DPClientId";
            Ddl_dematAcc.DataBind();
            Ddl_dematAcc.Items.Insert(0, new ListItem("Select the Demat Account Number", "Select the Demat Account Number"));
        }
        protected void BtnDmatRefresh_Click(object sender, EventArgs e)
        {
            LoadDematAccountNumber(portfolioId);
        }
        protected void BtnTradeRefresh_Click(object sender, EventArgs e)
        {
            LoadEquityTradeNumbers();
        }
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
            var keyValuePair = genDictPortfolioDetails.FirstOrDefault(x => x.Key == portfolioId);
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
            hdnIsMainPortfolio.Value = keyValuePair.Value.ToString();
            hdnIsCustomerLogin.Value = userVo.UserType;
            LoadEquityTradeNumbers();
        }
        private void SetFields(int i)
        {
            if (i == 0)
            {
                trBroker.Visible = false;
                trRate.Visible = false;
                lblScripName.Visible = false;
                trSTT.Visible = false;
                trTotal.Visible = false;
            }
            else
            {
                trBroker.Visible = true;
                trRate.Visible = true;
                trSTT.Visible = true;
                trTotal.Visible = true;
            }
        }
        protected void BindTransactionType()
        {
            DataSet dsTransactionType = new DataSet();
            dsTransactionType = customerTransactionBo.GetTransactionType();
            ddlTransactionType.DataSource = dsTransactionType;
            ddlTransactionType.DataValueField = "WETT_TransactionCode";
            ddlTransactionType.DataTextField = "WETT_TransactionTypeName";
            ddlTransactionType.DataBind();
            ddlTransactionType.Items.Insert(0, new ListItem("Select", "Select"));

        }

        private void LoadExchangeType(string EType)
        {
            try
            {

                DataSet ds = XMLBo.GetExchangeType(EType);
               ddlExchange.DataSource = ds;
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
        protected void rbtnDomestic_CheckedChanged(object sender, EventArgs e)
        {
            exchangetype = "Dom";
            LoadExchangeType(exchangetype);
            lblDollarPrice.Text = string.Empty;
        }
        protected void rbtnInternational_CheckedChanged(object sender, EventArgs e)
        {
            exchangetype = "Int";
            LoadExchangeType(exchangetype);
        }
        private void LoadInternationalCurencyTypes(bool bln)
        {
            if (bln == true && rbtnInternational.Checked == true)
            {
                tdFxRate.Visible = true;
                tdFxRateDetails.Visible = true;
            }
            else
            {
                tdFxRate.Visible = false;
                tdFxRateDetails.Visible = false;
            }
        }
        protected void ddlExchange_OnselectedChange(object sender, EventArgs e)
        {
            if (ddlExchange.SelectedValue == "NASDAQ" || ddlExchange.SelectedValue == "NYSE")
            {
                if (txtTradeDate.Text != null && txtTradeDate.Text != "")
                    lblDollarPrice.Text = "1" + customerTransactionBo.GetDollarRate(Convert.ToDateTime(txtTradeDate.Text.Trim())) + "RS";
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
                    ddlTradeAcc.Items.Insert(0, new ListItem("Select the Trade Number", "0"));
                }
                else
                {

                    DataTable dtCustomerAccounts = dsEqutiyTradeNumbers.Tables[0];
                    ddlTradeAcc.DataSource = dtCustomerAccounts;
                    ddlTradeAcc.DataTextField = "CETA_TradeAccountNum";
                    ddlTradeAcc.DataValueField = "CETA_AccountId";
                    ddlTradeAcc.DataBind();
                    ddlTradeAcc.Items.Insert(0, new ListItem("Select the Trade Number", "0"));
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
            AddmoreFlag = 0;
            submit();
        }
        protected void btnSubmit_AddMore_Click(object sender, EventArgs e)
        {
            AddmoreFlag = 1;
            submit();
        }
        private void submit()
        {
            decimal temp;
            DataTable dt;
            CultureInfo ci = new CultureInfo("en-GB");
            try
            {
                eqTransactionVo.CustomerId = customerVo.CustomerId;
                eqTransactionVo.ScripCode = scripCode;

                if (txtInternationalRates.Text != "")
                {
                    if (float.Parse(txtInternationalRates.Text) != 0)
                        eqTransactionVo.FXCurencyType = ddlInternationalCurrency.SelectedValue;
                    else
                        eqTransactionVo.FXCurencyType = String.Empty; ;
                }
                else
                {
                    eqTransactionVo.FXCurencyType = String.Empty;
                }

                if (!string.IsNullOrEmpty(txtInternationalRates.Text))
                {
                    eqTransactionVo.FXCurencyRate = Convert.ToDouble(txtInternationalRates.Text);
                }
                else
                {
                    eqTransactionVo.FXCurencyRate = 0.0;
                }

                if (ddl_Managedby.SelectedValue != "Select")
                {
                    eqTransactionVo.ManagedBy = Convert.ToInt32(ddl_Managedby.SelectedValue);
                }
                if (Ddl_dematAcc.SelectedIndex != 0)
                    eqTransactionVo.DemateAccountId = int.Parse(Ddl_dematAcc.SelectedItem.Value.ToString());
                if (ddlTradeAcc.SelectedValue != "0")
                {
                    eqTransactionVo.AccountId = int.Parse(ddlTradeAcc.SelectedItem.Value.ToString());
                    hdnAccountId.Value = eqTransactionVo.AccountId.ToString();
                }
                else
                {
                    eqTransactionVo.AccountId = int.Parse(hdnAccountId.Value.ToString());
                }
                if (ddl_type.SelectedItem.Text != "Select")
                {
                    if (ddl_type.SelectedItem.Text == "Core Investment")
                    {
                        eqTransactionVo.Type = 1;
                    }
                    else
                    {
                        eqTransactionVo.Type = 0;
                    }
                }
                else
                {
                    eqTransactionVo.Type = 2;
                }
                if (TxtBillNo.Text != "")
                    eqTransactionVo.BillNo = TxtBillNo.Text.ToString();
                if (TxtSettlemntNo.Text != "")
                    eqTransactionVo.SettlementNo = TxtSettlemntNo.Text.ToString();
                if (txtSettlmntDate.SelectedDate != null || txtSettlmntDate.SelectedDate != DateTime.MinValue)
                    eqTransactionVo.SettlementDate = Convert.ToDateTime(txtSettlmntDate.SelectedDate);
                if (ddlTransactionType.SelectedItem.Text.ToString() == "Dividend")
                {
                    eqTransactionVo.BuySell = "B";
                    eqTransactionVo.TransactionCode = 6;
                    var DivHistory = ddl_divhistory.SelectedItem.Text.ToString();
                    if (DivHistory != "Dividend Declared Date,Dividend Percentage,Face Value")
                    {
                        var List = DivHistory.Split(',');
                        TradeDate = DateTime.Parse(List[0]);
                        eqTransactionVo.TradeDate = TradeDate;
                        eqTransactionVo.DailyCorpAxnId = Convert.ToInt16(ddl_divhistory.SelectedValue);

                    }
                    else
                    {
                        eqTransactionVo.TradeDate = DateTime.Parse(txtTradeDate.Text);
                    }
                    if (ddl_DivStatus.SelectedValue != "Select")
                        eqTransactionVo.DividendRecieved = Boolean.Parse(ddl_DivStatus.SelectedValue);
                    if (Txt_NoOfSharesForDiv.Text != "")
                        eqTransactionVo.NoOfSharesForDiv = double.Parse(Txt_NoOfSharesForDiv.Text);
                    eqTransactionVo.BankReferenceNo = txtbankrefno.Text;
                    eqTransactionVo.TradeTotal = double.Parse(Txt_DivAmount.Text);
                    eqTransactionVo.GrossConsideration = double.Parse(Txt_DivAmount.Text);
                    eqTransactionVo.TradeTotalIncBrokerage = double.Parse(Txt_DivAmount.Text);


                }
                else
                {
                    dt = productEquityBo.GetBrokerCode(portfolioId, ddlTradeAcc.SelectedItem.Text.ToString());

                    eqTransactionVo.IsSourceManual = 1;
                    if (txtBrokerage.Text != "")
                        eqTransactionVo.Brokerage = float.Parse(txtBrokerage.Text);
                    if (eqTransactionVo.BrokerCode != null)
                        eqTransactionVo.BrokerCode = dt.Rows[0]["XB_BrokerCode"].ToString();
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Buy")
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
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Stocks Split")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 3;
                    }
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Bonus Issue")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 4;
                    }
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Right Issue")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 5;
                    }

                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Amalgamtion")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 7;
                    }

                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Capital Reduction")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 8;
                    }

                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Merger")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 9;
                    }
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Demerger")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 10;
                    }
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Buy Back Offer")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 11;
                    }
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Open Offer")
                    {
                        eqTransactionVo.BuySell = "B";
                        eqTransactionVo.TransactionCode = 12;
                    }
                    eqTransactionVo.IsCorpAction = 0;
                    if (ddlTransactionMode.SelectedItem.Text == "Speculative")
                    {
                        eqTransactionVo.IsSpeculative = 1;
                    }
                    else if (ddlTransactionMode.SelectedItem.Text == "Delivery")
                    {
                        eqTransactionVo.IsSpeculative = 0;
                    }
                    if (ddlTransactionType.SelectedItem.Text.ToString() == "Demat Charges")
                    {
                        eqTransactionVo.BuySell = "S";
                        eqTransactionVo.TransactionCode = 14;
                    }

                    eqTransactionVo.EducationCess = (float)tempEducation;
                    if (ddlExchange.SelectedItem.Value.ToString() != "Select an Exchange")
                        eqTransactionVo.Exchange = ddlExchange.SelectedItem.Value.ToString();
                    else
                        eqTransactionVo.Exchange = "NSE";

                    if (txt_OtherCharges.Text != "")
                    {
                        if (txt_OtherCharges.Visible == true)
                        {
                            eqTransactionVo.OtherCharges = float.Parse(Request.Form[txt_OtherCharges.UniqueID]);
                        }
                    }
                    if (txtNumShares.Text != "")
                    {
                        eqTransactionVo.Quantity = float.Parse(txtNumShares.Text);
                    }
                    eqTransactionVo.TradeType = "D";
                    if (txtRate.Text != "")
                    {
                        eqTransactionVo.Rate = float.Parse(txtRate.Text);
                    }
                    if (txtRateIncBrokerage.Text != "")
                    {
                        eqTransactionVo.RateInclBrokerage = float.Parse(txtRateIncBrokerage.Text);
                    }
                    temp = decimal.Round(Convert.ToDecimal(tempService), 3);
                    if (txtTax.Text != "")
                    {
                        eqTransactionVo.ServiceTax = float.Parse(txtTax.Text);
                    }
                    if (txtSTT.Text != "")
                    {
                        eqTransactionVo.STT = float.Parse(txtSTT.Text);
                    }
                    if (txtTradeDate.Text != "")
                    {
                        eqTransactionVo.TradeDate = Convert.ToDateTime(txtTradeDate.Text.Trim(), ci);
                        // DateTime.Parse(txtTradeDate.Text);//ddlDay.SelectedItem.Text.ToString() + "/" + ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYear.SelectedItem.Value.ToString()
                    }
                    if (txtTotal.Text != "")
                    {
                        eqTransactionVo.GrossConsideration = double.Parse(txtTotal.Text);
                    }
                    eqTransactionVo.TradeAccountNum = "";

                    if (txtSebiTurnOver.Text != "")
                    {
                        eqTransactionVo.SebiTurnOverFee = double.Parse(txtSebiTurnOver.Text);
                    }
                    if (TxtTrxnChrgs.Text != "")
                    {
                        eqTransactionVo.TransactionCharges = double.Parse(TxtTrxnChrgs.Text);
                    }
                    if (TxtStampCharges.Text != "")
                    {
                        eqTransactionVo.StampCharges = double.Parse(TxtStampCharges.Text);
                    }

                    if (TxtDiffInBrkrg.Text != "")
                        eqTransactionVo.DifferenceInBrokerage = double.Parse(TxtDiffInBrkrg.Text);

                    if (TxtTradetotalIncBrkrg.Text != "")
                    {
                        eqTransactionVo.TradeTotalIncBrokerage = double.Parse(TxtTradetotalIncBrkrg.Text);
                    }
                    if (TxtRateInBrkrgAllCharges.Text != "")
                    {
                        eqTransactionVo.RateIncBrokerageAllCharges = double.Parse(TxtRateInBrkrgAllCharges.Text);
                    }
                    if (txtGrossConsideration.Text != "")
                    {
                        eqTransactionVo.TradeTotal = double.Parse(txtGrossConsideration.Text);

                    }
                    if (Txt_DematCharge.Text != "")
                        eqTransactionVo.DematCharge = float.Parse(Txt_DematCharge.Text);
                    if (txtRemark.Text != "")
                    {
                        eqTransactionVo.Remark = txtRemark.Text.ToString();
                    }
                    trothercharge.Visible = false;
                    trDematAndOtherCharg.Visible = false;
                }
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
            if (AddmoreFlag == 1)
            {
                ddl_Managedby.Enabled = false;
                ddlTradeAcc.Enabled = false;
                TxtBillNo.Enabled = false;
                TxtSettlemntNo.Enabled = false;
                Ddl_dematAcc.Enabled = false;
                txtSettlmntDate.Enabled = false;
                txtScrip.Text = string.Empty;
                ddl_type.SelectedIndex = 0;
                ddlTransactionType.SelectedIndex = 0;
                ddlExchange.SelectedIndex = 0;
                lblticker.Text = string.Empty;
                txtTradeDate.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtNumShares.Text = string.Empty;
                txtBrokerage.Text = string.Empty;
                txtTax.Text = string.Empty;
                txtSTT.Text = string.Empty;
                txtRateIncBrokerage.Text = string.Empty;
                txtTotal.Text = string.Empty;
                lblScripName.Visible = false;
                Txt_NoOfSharesForDiv.Text = string.Empty;
                Txt_DivAmount.Text = string.Empty;
                ddl_DivStatus.SelectedIndex = 0;
                txtSebiTurnOver.Text = string.Empty;
                TxtTrxnChrgs.Text = string.Empty;
                TxtStampCharges.Text = string.Empty;
                TxtDiffInBrkrg.Text = string.Empty;
                Txt_DematCharge.Text = string.Empty;
                TxtTradetotalIncBrkrg.Text = string.Empty;
                txtbrokerageamt.Text = string.Empty;
                txtSebiTurnOverAmount.Text = string.Empty;
                txtTrxnAmount.Text = string.Empty;
                txtStampChargeAmt.Text = string.Empty;
                txtServiceAmt.Text = string.Empty;
                TxtOtherChargeAmt.Text = string.Empty;
                txt_OtherCharges.Text = string.Empty;
                txtSttAmt.Text = string.Empty;
                txtAdditionalAdjAmt.Text = string.Empty;
                TxtRateInBrkrgAllCharges.Text = string.Empty;
                txtGrossConsideration.Text = string.Empty;
                txtbankrefno.Text = string.Empty;
                rbtnDomestic.Checked = true;
                txtInternationalRates.Text = string.Empty;
            }
            else
            {
                txtScrip.Text = string.Empty;
                ddlTransactionType.SelectedIndex = 0;
                ddlExchange.SelectedIndex = 0;
                ddl_Managedby.SelectedIndex = 0;
                ddl_type.SelectedIndex = 0;
                ddlTradeAcc.SelectedIndex = 0;
                lblticker.Text = string.Empty;
                txtTradeDate.Text = string.Empty;
                Ddl_dematAcc.SelectedIndex = 0;
                TxtBillNo.Text = string.Empty;
                TxtSettlemntNo.Text = string.Empty;
                txtSettlmntDate.Clear();
                txtPrice.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtNumShares.Text = string.Empty;
                txtBroker.Text = string.Empty;
                txtBrokerage.Text = string.Empty;
                txtTax.Text = string.Empty;
                txtSTT.Text = string.Empty;
                txtRateIncBrokerage.Text = string.Empty;
                txtTotal.Text = string.Empty;
                Txt_NoOfSharesForDiv.Text = string.Empty;
                Txt_DivAmount.Text = string.Empty;
                ddl_DivStatus.SelectedIndex = 0;
                lblScripName.Visible = false;
                txtSebiTurnOver.Text = string.Empty;
                TxtTrxnChrgs.Text = string.Empty;
                TxtStampCharges.Text = string.Empty;
                TxtDiffInBrkrg.Text = string.Empty;
                Txt_DematCharge.Text = string.Empty;
                ddl_Managedby.Enabled = true;
                ddl_type.Enabled = true;
                ddlTradeAcc.Enabled = true;
                TxtBillNo.Enabled = true;
                TxtSettlemntNo.Enabled = true;
                Ddl_dematAcc.Enabled = true;
                txtSettlmntDate.Enabled = true;
                TxtTradetotalIncBrkrg.Text = string.Empty;
                txtbrokerageamt.Text = string.Empty;
                txtSebiTurnOverAmount.Text = string.Empty;
                txtTrxnAmount.Text = string.Empty;
                txtStampChargeAmt.Text = string.Empty;
                txtServiceAmt.Text = string.Empty;
                TxtOtherChargeAmt.Text = string.Empty;
                txt_OtherCharges.Text = string.Empty;
                txtSttAmt.Text = string.Empty;
                txtAdditionalAdjAmt.Text = string.Empty;
                TxtRateInBrkrgAllCharges.Text = string.Empty;
                txtGrossConsideration.Text = string.Empty;
                txtbankrefno.Text = string.Empty;
                txtInternationalRates.Text = string.Empty;
                ddl_divhistory.Items.Clear();
            }
        }

        protected void ddlDematAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dematcharge();
        }
        protected void ddlTrMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dematcharge();
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            SetBrokerage();
        }
        public void Dematcharge()
        {
            if (ddlTransactionMode.SelectedItem.Text == "Speculative")
            {
                TransactionMode = 1;
            }
            else if (ddlTransactionMode.SelectedItem.Text == "Delivery")
            {
                TransactionMode = 0;
            }
            if (ddlTransactionType.SelectedItem.Text.ToString() == "Buy")
            {
                Type = "B";
            }
            if (ddlTransactionType.SelectedItem.Text.ToString() == "Sell")
            {
                Type = "S";
            }

            if (Type == "S" && TransactionMode == 0 && Ddl_dematAcc.SelectedIndex != 0)
            {
                trDematAndOtherCharg.Visible = true;
            }
        }


        public void SetBrokerage()
        {
            if (string.IsNullOrEmpty(Request.Form[btnUpdate.UniqueID]))
            {
                {
                    CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                    CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
                    double brokerage = 0, otherCharges, SebiTurnOverFee, TransactionCharges, StampCharges, Stax = 0, STT = 0, rateIncBroker = 0, rateAndAllCharges = 0, StaxFinal = 0;
                    if (txtRate.Text != "")
                    {
                        if (ddlTransactionMode.SelectedItem.Text == "Speculative")
                        {
                            TransactionMode = 1;
                        }
                        else if (ddlTransactionMode.SelectedItem.Text == "Delivery")
                        {
                            TransactionMode = 0;
                        }
                        if (ddlTransactionType.SelectedItem.Text.ToString() == "Buy")
                        {
                            Type = "B";
                        }
                        if (ddlTransactionType.SelectedItem.Text.ToString() == "Sell")
                        {
                            Type = "S";
                        }
                        DateTime Tradedate = Convert.ToDateTime(txtTradeDate.Text.Trim());
                        if (ddlTransactionType.SelectedIndex != 0 && ddlTransactionMode.SelectedIndex != 0)
                            customerAccountsVo = customerTransactionBo.GetEquityRateForTransaction(int.Parse(ddlTradeAcc.SelectedItem.Value.ToString()), TransactionMode, Type, Tradedate);
                        if (customerAccountsVo.EndDate == DateTime.MinValue)
                        {
                            customerAccountsVo.EndDate = System.DateTime.Now;
                        }
                        brokerage = Math.Round((double.Parse(txtRate.Text) * (customerAccountsVo.Rate / 100)), 2);
                        SebiTurnOverFee = Math.Round((double.Parse(txtRate.Text) * (customerAccountsVo.SebiTurnOverFee / 100)), 6);
                        TransactionCharges = Math.Round((double.Parse(txtRate.Text) * (customerAccountsVo.TransactionCharges / 100)), 6);
                        StampCharges = Math.Round((double.Parse(txtRate.Text) * (customerAccountsVo.StampCharges / 100)), 6);
                        if (customerAccountsVo.ServiceTax == 0.0)
                        {
                            customerAccountsVo.ServiceTax = 12.36;
                        }
                        hdnstax.Value = Convert.ToDouble(customerAccountsVo.ServiceTax / 100).ToString();

                        if (customerAccountsVo.IsBrApplicableToStax == 1)
                        {
                            Stax = Stax + (customerAccountsVo.ServiceTax / 100) * brokerage;
                        }
                        if (customerAccountsVo.IsSebiApplicableToStax == 1)
                        {
                            Stax = Stax + (customerAccountsVo.ServiceTax / 100) * SebiTurnOverFee;
                        }
                        if (customerAccountsVo.IsTrxnApplicableToStax == 1)
                        {
                            Stax = Stax + (customerAccountsVo.ServiceTax / 100) * TransactionCharges;
                        }
                        if (customerAccountsVo.IsStampApplicableToStax == 1)
                        {
                            Stax = Stax + (customerAccountsVo.ServiceTax / 100) * StampCharges;
                        }
                        StaxFinal = Math.Round(Stax, 6);
                        STT = Math.Round((double.Parse(txtRate.Text) * (customerAccountsVo.Stt / 100)), 6);
                        if (Convert.ToDateTime(txtTradeDate.Text) >= customerAccountsVo.StartDate && Convert.ToDateTime(txtTradeDate.Text) <= customerAccountsVo.EndDate)
                        {

                            txtBrokerage.Text = brokerage.ToString();
                            if (txtBrokerage.Text != "" && txtNumShares.Text != "")
                            {

                                double brokerageamt = (double.Parse(txtBrokerage.Text) * double.Parse(txtNumShares.Text));
                                txtbrokerageamt.Text = Math.Round(brokerageamt, 4).ToString();
                            }

                            if (ddlTransactionType.SelectedItem.Text.ToString() != "Sell")
                            {


                                rateIncBroker = double.Parse(txtRate.Text) + brokerage;

                            }
                            else
                            {
                                rateIncBroker = double.Parse(txtRate.Text) - brokerage;
                            }

                            txtRateIncBrokerage.Text = Math.Round(rateIncBroker, 4).ToString();
                            if (txtNumShares.Text != "")
                                TxtTradetotalIncBrkrg.Text = Convert.ToDouble(Convert.ToDouble(rateIncBroker) * double.Parse(txtNumShares.Text)).ToString();
                            txtSebiTurnOver.Text = SebiTurnOverFee.ToString();
                            TxtTrxnChrgs.Text = TransactionCharges.ToString();
                            TxtStampCharges.Text = StampCharges.ToString();
                            txtTax.Text = StaxFinal.ToString();

                            if (ddlTransactionType.SelectedItem.Text.ToString() == "Buy" || ddlTransactionType.SelectedItem.Text.ToString() == "Sell")
                            {
                                txtSTT.Text = STT.ToString();
                                if (txtNumShares.Text != "")
                                    txtSttAmt.Text = Math.Round(STT * double.Parse(txtNumShares.Text), 6).ToString();
                            }
                            else
                            {
                                hdnStt.Value = 0.ToString();
                                txtSTT.Text = 0.ToString();
                                txtSttAmt.Text = 0.ToString();
                            }
                            otherCharges = Math.Round((SebiTurnOverFee + TransactionCharges + StampCharges + STT + StaxFinal), 6);


                            txt_OtherCharges.Text = otherCharges.ToString();
                            if (txtNumShares.Text != "")
                            {
                                txtSebiTurnOverAmount.Text = Math.Round(SebiTurnOverFee * double.Parse(txtNumShares.Text), 6).ToString();
                                txtStampChargeAmt.Text = Math.Round(StampCharges * double.Parse(txtNumShares.Text), 6).ToString();
                                txtTrxnAmount.Text = Math.Round(TransactionCharges * double.Parse(txtNumShares.Text), 6).ToString();
                                txtServiceAmt.Text = Math.Round(StaxFinal * double.Parse(txtNumShares.Text), 6).ToString();
                                TxtOtherChargeAmt.Text = Math.Round(otherCharges * double.Parse(txtNumShares.Text), 4).ToString();
                            }
                        }
                        else if (Convert.ToDateTime(txtTradeDate.Text) <= customerAccountsVo.StartDate && Convert.ToDateTime(txtTradeDate.Text) >= customerAccountsVo.EndDate)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Rates are not applicable');", true);
                        }
                        else
                        {
                            hdnSebiCharge.Value = 0.ToString();
                            hdnTrxnCharge.Value = 0.ToString();
                            hdnStampCharge.Value = 0.ToString();
                            hdnServiceTax.Value = 0.ToString();
                            hdnStt.Value = 0.ToString();

                        }
                        if (TxtDiffInBrkrg.Text == "")
                        {
                            TxtDiffInBrkrg.Text = 0.ToString();
                        }
                        if (txtRate.Text != "")
                        {
                            if (ddlTransactionType.SelectedItem.Text.ToString() != "Sell")
                            {
                                rateAndAllCharges = Convert.ToDouble(txtRate.Text) + brokerage + SebiTurnOverFee
                             + TransactionCharges + StampCharges + Stax +
                              STT + Convert.ToDouble(TxtDiffInBrkrg.Text);
                            }
                            else
                            {
                                rateAndAllCharges = Convert.ToDouble(txtRate.Text) - brokerage - SebiTurnOverFee
                           - TransactionCharges - StampCharges - Stax -
                          STT - Convert.ToDouble(TxtDiffInBrkrg.Text);

                            }
                        }
                        TxtRateInBrkrgAllCharges.Text = Math.Round(rateAndAllCharges, 4).ToString();
                        if (txtNumShares.Text != "")
                            txtTotal.Text = Math.Round((double.Parse(TxtRateInBrkrgAllCharges.Text) * double.Parse(txtNumShares.Text)), 4).ToString();
                        trothercharge.Visible = true;
                        if (txtRate.Text != "" && txtNumShares.Text != "")
                            txtGrossConsideration.Text = Math.Round(double.Parse(txtRate.Text) * double.Parse(txtNumShares.Text), 4).ToString();
                        if (txtNumShares.Text != "" && TxtDiffInBrkrg.Text != "")
                        {
                            txtAdditionalAdjAmt.Text = (double.Parse(TxtDiffInBrkrg.Text) * double.Parse(txtNumShares.Text)).ToString();
                        }
                    }
                }
            }
        }
        private void LoadDividendHistory(int scripCode)
        {
            DataSet dsDividendHistory = new DataSet();
            dsDividendHistory = customerTransactionBo.GetDividendHistory(scripCode);
            ddl_divhistory.DataValueField = "PECA_DailyCorpAxnId";
            ddl_divhistory.DataTextField = "DividendHistory";
            ddl_divhistory.DataSource = dsDividendHistory;
            ddl_divhistory.DataBind();
            ddl_divhistory.Items.Insert(0, new ListItem("Dividend Declared Date,Dividend Percentage,Face Value", "Dividend Declared Date,Dividend Percentage,Face Value"));
        }
        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransactionType.SelectedValue == "6" && txtScrip.Text.ToString() != "")
            {
                LoadDividendHistory(scripCode);

                DivDividend.Visible = true;
                trdivtype.Visible = true;
                trdivamt.Visible = true;
                trexchng.Visible = false;
                trRate.Visible = false;
                trqnty.Visible = false;
                trBroker.Visible = false;
                trbrokerage.Visible = false;
                trsebi.Visible = false;
                trtrxn.Visible = false;
                trStampCharg.Visible = false;
                trService.Visible = false;
                trSTT.Visible = false;
                trothercharge.Visible = false;
                trTotal.Visible = false;
                trnet.Visible = false;
                trRemark.Visible = false;





            }
            else if (ddlTransactionType.SelectedValue != "6" && txtScrip.Text.ToString() != "")
            {

                if (ddlTransactionType.SelectedValue == "13")
                {
                    ddlTransactionMode.Items.FindByValue("Speculative").Enabled = false;

                }

                DivDividend.Visible = false;
                trdivamt.Visible = false;
                trdivtype.Visible = false;
                divtradedate.Visible = true;
                trexchng.Visible = true;
                trRate.Visible = true;
                trqnty.Visible = true;
                trBroker.Visible = true;
                trbrokerage.Visible = true;
                trsebi.Visible = true;
                trtrxn.Visible = true;
                trStampCharg.Visible = true;
                trService.Visible = true;
                trSTT.Visible = true;
                trothercharge.Visible = true;
                trTotal.Visible = true;
                trnet.Visible = true;
                trRemark.Visible = false;
                Dematcharge();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a Scrip!');", true);
                ddlTransactionType.SelectedValue = "Select";
            }

        }
        protected void ddl_divhistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtScrip.Text.ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Please select a Scrip!');", true);
            }

            else
            {
                int managedby = 0;
                int DematAccountNum = 0;
                string DivHistory = ddl_divhistory.SelectedItem.Text;
                if (DivHistory != "Dividend Declared Date,Dividend Percentage,Face Value")
                {
                    var List = DivHistory.Split(',');
                    TradeDate = DateTime.Parse(List[0]);
                    var List1 = List[1].Split('%');
                    double rate = double.Parse(List1[0]);
                    facevalue = double.Parse(List[2]);

                    if (ddl_Managedby.SelectedIndex != 0)
                        managedby = Convert.ToInt32(ddl_Managedby.SelectedValue);
                    if (Ddl_dematAcc.SelectedIndex != 0)
                        DematAccountNum = Convert.ToInt32(Ddl_dematAcc.SelectedValue);
                    NoOfShares = customerTransactionBo.GetNoOfShares(Convert.ToInt32(ddlTradeAcc.SelectedValue), scripCode, TradeDate, DematAccountNum, managedby);
                    Txt_NoOfSharesForDiv.Text = NoOfShares.ToString();
                    Txt_NoOfSharesForDiv.Visible = true;
                    BtnCalculateDiv.Visible = true;
                    txtTradeDate.Text = TradeDate.ToShortDateString().ToString();
                    txtTradeDate.Enabled = false;
                }
                else
                {
                    Txt_NoOfSharesForDiv.Text = "";
                    BtnCalculateDiv.Visible = false; ;
                    txtTradeDate.Enabled = true;
                    Lbl_DivAmount.Text = "Dividend Amount:";

                }
            }
        }
        protected void BtnCalculateDiv_Click(object sender, EventArgs e)
        {
            if (ddl_divhistory.SelectedIndex != 0)
            {
                var DivHistory = ddl_divhistory.SelectedItem.Text.ToString();
                var List = DivHistory.Split(',');
                var List1 = List[1].Split('%');
                double rate = double.Parse(List1[0]);
                facevalue = double.Parse(List[2]);
                double divamount = Convert.ToDouble(Txt_NoOfSharesForDiv.Text) * (facevalue * rate / 100);
                Txt_DivAmount.Text = divamount.ToString();
                Lbl_DivAmount.Text = "Dividend Amount(System Generated):";
            }
        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {

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

            lblScripName.Text = txtScrip.Text;
            lblticker.Text = ds.Tables[0].Rows[0]["PEM_Ticker"].ToString();

            scripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());


            autoCompleteExtender.ContextKey = schemePlanCode.ToString();

            LoadDividendHistory(scripCode);
            if (txtScrip.Text != "" && txtTradeDate.Text != "")
            {
                DateTime dt = Convert.ToDateTime(txtTradeDate.Text);
                txtPrice.Text = customerPortfolioBo.GetEQScripPrice(scripCode, dt).ToString();
            }
        }
        protected void btnUsePrice_Click(object sender, EventArgs e)
        {
            txtRate.Text = txtPrice.Text;
        }
        protected void txtNumShares_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRate.Text.Trim()) && (!string.IsNullOrEmpty(txtNumShares.Text.Trim())))
            {
                txtGrossConsideration.Text = Math.Round((double.Parse(txtRate.Text) * double.Parse(txtNumShares.Text)), 4).ToString();
            }
            else
            {
                txtNumShares.Text = string.Empty;
                txtGrossConsideration.Text = string.Empty;
            }
            SetBrokerage();
        }
        protected void txtSettlmntDate_SelectedDateChanged(object sender, EventArgs e)
        {
        }

        protected void Addtrdacc_Click(object sender, EventArgs e)
        {
            LoadEquityTradeNumbers();
        }
        protected void Adddividend_click(object sender, EventArgs e)
        {
            if (txtScrip.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Please Select a scrip');", true);

            }
            else
            {
                radAplicationPopUp.VisibleOnPageLoad = true;
            }
        }
        protected void btnSubmitDiv_Click(object sender, EventArgs e)
        {
            DataSet ds = productEquityBo.GetScripCode(txtScrip.Text.ToString());
            int ScripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());

            DateTime DivDeclaredDate = Convert.ToDateTime(txtFromDate.SelectedDate);
            double DivPercentage = double.Parse(txt_Percentage.Text);
            double facevalue = double.Parse(txt_FaceValue.Text);
            string CorpAxnCode = "DIV";
            customerTransactionBo.AddDividend(CorpAxnCode, ScripCode, DivDeclaredDate, DivPercentage, facevalue);
            LoadDividendHistory(ScripCode);
            radAplicationPopUp.VisibleOnPageLoad = false;
        }

        private void ShowFields(int transactionType)
        {
            if (transactionType == 6)
            {
                DivDividend.Visible = true;
                trdivamt.Visible = true;
                trdivtype.Visible = true;
                trexchng.Visible = false;
                trRate.Visible = false;
                trqnty.Visible = false;
                trBroker.Visible = false;
                trbrokerage.Visible = false;
                trsebi.Visible = false;
                trtrxn.Visible = false;
                trStampCharg.Visible = false;
                trService.Visible = false;
                trSTT.Visible = false;
                trothercharge.Visible = false;
                trTotal.Visible = false;
                trnet.Visible = false;
                trRemark.Visible = false;
                trdivamt.Visible = true;


            }
            else
            {
                if (transactionType == 13)
                {
                    ddlTransactionMode.Items.FindByValue("Speculative").Enabled = false;

                }
                if (transactionType == 2)
                {
                }

                DivDividend.Visible = false;
                trdivamt.Visible = false; ;
                trdivtype.Visible = false;
                divtradedate.Visible = true;
                trexchng.Visible = true;
                trRate.Visible = true;
                trqnty.Visible = true;
                trBroker.Visible = true;
                trbrokerage.Visible = true;
                trsebi.Visible = true;
                trtrxn.Visible = true;
                trStampCharg.Visible = true;
                trService.Visible = true;
                trSTT.Visible = true;
                trothercharge.Visible = true;
                trTotal.Visible = true;
                trnet.Visible = true;
                trRemark.Visible = false;


            }

        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            eqTransactionVo = (EQTransactionVo)Session["EquityTransactionVo"];
            if (Request.QueryString["Currency"].Trim() == "INR")
            {

                BtnSetVisiblity(1);
                setvisibility(1);
            }
            else
            {
                BtnSetVisiblity(0);
                setvisibility(0);
            }
            EQAccountDetails();
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            int CurrentPageIndex = int.Parse(Request.QueryString["CurrentPageIndex"]);
            int CustomerId = int.Parse(customerVo.CustomerId.ToString());
            int PortfolioId = int.Parse(Request.QueryString["portfolioId"].ToString());
            string FrmDt = Request.QueryString["FrmDt"].ToString();
            string ToDt = Request.QueryString["ToDt"].ToString();
            string Currency = Request.QueryString["Currency"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityTransactionsView','?CurrentPageIndex=" + CurrentPageIndex + "&FrmDt=" + FrmDt.ToString() + "&ToDt=" + ToDt + "&Currency=" + Currency + "&PortfolioId=" + portfolioId + "');", true);
        }
    }
}

