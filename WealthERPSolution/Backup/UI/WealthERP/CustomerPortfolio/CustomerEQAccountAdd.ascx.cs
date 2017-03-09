using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using WealthERP.Base;
using BoCommon;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoCustomerProfiling;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoCustomerPortfolio;
using BoOps;
using System.Web.Services;
using Telerik.Web.UI;
namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerEQAccountAdd : System.Web.UI.UserControl
    {
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        DataSet dsDPAccounts;
        DataTable dtBroker;
        DataTable dtDPAccounts = new DataTable();
        DataTable dtDPAccountsRaw = new DataTable();
        DataRow drDPAccounts;
        int tradeAccountId;
        static int portfolioId;
        int dpAccountId;
        int isDefault = 0;
        string path;
        string Id;
        string account;
        int CB_CustBankAccId;
        MFOrderBo mforderBo = new MFOrderBo();
        Dictionary<int, int> genDictPortfolioDetails = new Dictionary<int, int>();

        CustomerAccountDao checkAccDao = new CustomerAccountDao();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        [WebMethod]
        public static bool CheckTradeNoAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            CustomerAccountDao checkAccDao = new CustomerAccountDao();
            return checkAccDao.CheckTradeNoAvailability(TradeAccNo, BrokerCode, PortfolioId);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //txtTradeNum.Attributes.Add("onchange", "javascript:checkLoginId(value);");
            try
            {
                SessionBo.CheckSession();
                path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
                trDPAccount.Visible = false;
                trDPAccountsGrid.Visible = false;
                userVo = (UserVo)Session["userVo"];
                customerVo = (CustomerVo)Session["CustomerVo"];


                if (!IsPostBack)
                {
                    gvEqMIS.Visible = false;
                    if (Session[SessionContents.PortfolioId] != null)
                    {
                        portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                        BindPortfolioDropDown();
                        BindCustomerBankList();
                        BindAccountNum();
                        dtBroker = XMLBo.GetBroker(path);
                        ddlBrokerCode.DataSource = dtBroker;
                        ddlBrokerCode.DataTextField = "Broker";
                        ddlBrokerCode.DataValueField = "BrokerCode";
                        ddlBrokerCode.DataBind();
                        //Txt_ServiceTax.Text = 12.36.ToString();
                        if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                        {

                            customerAccountsVo = (CustomerAccountsVo)Session["EQAccountVoRow"];
                            if (Request.QueryString["action"].Trim() == "Edit")
                            {
                                BtnSetVisiblity(1);
                                EditEQAccountDetails();
                            }
                            else if (Request.QueryString["action"].Trim() == "View")
                            {
                                BtnSetVisiblity(0);
                                lnkBack.Visible = true;
                                ViewEQAccountDetails();

                            }
                            if (ddlBankList.SelectedIndex != 0 && ddlAccountNum.SelectedIndex != -1)
                            {
                                BindEQLedgerMIS(customerVo.CustomerId, customerAccountsVo.AccountId, customerAccountsVo.BankId);
                            }
                            else
                            {
                                tblMessage.Visible = true;
                                ErrorMessage.Visible = true;
                                ErrorMessage.InnerText = "Bank is not associated with Current trade  Account selected...!";
                            }

                        }
                        else
                        {
                            ddlBrokerCode.Items.Insert(0, new ListItem("Select a Broker Code", "Select a Broker Code"));
                            //dateValidator.MinimumValue = "1/1/1900";
                            //dateValidator.MaximumValue = DateTime.Today.ToString();
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
                FunctionInfo.Add("Method", "CustomerEQAccountAdd.ascx:Page_Load()");
                object[] objects = new object[4];
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

        private void BindEQLedgerMIS(int CustomerId, int TradeAccountId, int BankAccountId)
        {
            DataSet dsGetEqLedgerMIS;
            DataTable dtGetEqLedgerMIS;
            dsGetEqLedgerMIS = customerTransactionBo.GetEquityLedgerMIS(CustomerId, TradeAccountId, BankAccountId);
            dtGetEqLedgerMIS = dsGetEqLedgerMIS.Tables[0];

            DataTable dtEqLedgerMISDetails = new DataTable();
            dtEqLedgerMISDetails.Columns.Add("MarginDetails");
            dtEqLedgerMISDetails.Columns.Add("Amount");
            dtEqLedgerMISDetails.Columns.Add("CurrentValuation");
            dtEqLedgerMISDetails.Columns.Add("ProfitLoss");

            DataRow drEqLedgerMISDetails;
            foreach (DataRow dr in dtGetEqLedgerMIS.Rows)
            {
                drEqLedgerMISDetails = dtEqLedgerMISDetails.NewRow();
                if (dr["TYPE"].ToString() == "A")
                    drEqLedgerMISDetails["MarginDetails"] = "Cheques received (deposit)";
                else if (dr["TYPE"].ToString() == "B")
                    drEqLedgerMISDetails["MarginDetails"] = "Withdrawl to Date";
                else if (dr["TYPE"].ToString() == "C")
                    drEqLedgerMISDetails["MarginDetails"] = "Amount Invested";
                else if (dr["TYPE"].ToString() == "D")
                    drEqLedgerMISDetails["MarginDetails"] = "Balance Margin";

                if (Math.Round(decimal.Parse(dr["Amount"].ToString()), 0) != 0)
                    drEqLedgerMISDetails["Amount"] = Math.Round(decimal.Parse(dr["Amount"].ToString()), 0);
                else
                    drEqLedgerMISDetails["Amount"] = "";

                if (Math.Round(decimal.Parse(dr["CurrentValue"].ToString()), 0) != 0)
                    drEqLedgerMISDetails["CurrentValuation"] = Math.Round(decimal.Parse(dr["CurrentValue"].ToString()), 0);
                else
                    drEqLedgerMISDetails["CurrentValuation"] = "";
                if (Math.Round(decimal.Parse(dr["ProfitLoss"].ToString()), 0) != 0)
                    drEqLedgerMISDetails["ProfitLoss"] = Math.Round(decimal.Parse(dr["ProfitLoss"].ToString()), 0);
                else
                    drEqLedgerMISDetails["ProfitLoss"] = "";

                dtEqLedgerMISDetails.Rows.Add(drEqLedgerMISDetails);
            }
            if (dtEqLedgerMISDetails != null)
            {
                gvEqMIS.Visible = true;
                imgbtnEqMIS.Visible = true;
                gvEqMIS.DataSource = dtEqLedgerMISDetails;
                gvEqMIS.DataBind();
            }
            else
            {
                gvEqMIS.Visible = false;
                gvEqMIS.DataSource = null;
                gvEqMIS.DataBind();
                imgbtnEqMIS.Visible = false;
            }

            if (Cache["gvEqMIS" + userVo.UserId] == null)
            {
                Cache.Insert("gvEqMIS" + userVo.UserId, dtEqLedgerMISDetails);
            }
            else
            {
                Cache.Remove("gvEqMIS" + userVo.UserId);
                Cache.Insert("gvEqMIS" + userVo.UserId, dtEqLedgerMISDetails);
            }

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
        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");
            ddlPortfolio.SelectedValue = portfolioId.ToString();

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string accountopeningdate = txtAccountStartingDate.Text;
            try
            {
                btnSubmit.Enabled = false;
                customerAccountsVo.CustomerId = customerVo.CustomerId;
                customerAccountsVo.PortfolioId = int.Parse(ddlPortfolio.SelectedValue.ToString());
                customerAccountsVo.TradeNum = txtTradeNum.Text.ToString();
                customerAccountsVo.BrokerCode = ddlBrokerCode.SelectedItem.Value.ToString();
                if (!string.IsNullOrEmpty(accountopeningdate.Trim()))
                    customerAccountsVo.AccountOpeningDate = DateTime.Parse(accountopeningdate);// ddlDay.SelectedItem.Text.ToString() + "/" + ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYear.SelectedItem.Value.ToString()
                //if (txtBrokeragePerDelivery.Text == "")
                //    txtBrokeragePerDelivery.Text = "0";
                //if (txtBrokeragePerSpeculative.Text == "")
                //    txtBrokeragePerSpeculative.Text = "0";
                //if (txtOtherCharges.Text == "")
                //    txtOtherCharges.Text = "0";

                if (ddlBankList.SelectedIndex != 0)
                    customerAccountsVo.BankNameInExtFile = ddlBankList.SelectedValue;
                if (ddlAccountNum.SelectedIndex != 0)
                    customerAccountsVo.BankId = int.Parse(ddlAccountNum.SelectedValue);
                //customerAccountsVo.BankName = ddlBankList.SelectedValue.ToString();
                //customerAccountsVo.BankAccountNum = account;
                //if (Txt_SEBITrnfee.Text != "")
                //{
                //    customerAccountsVo.SebiTurnOverFee = double.Parse(Txt_SEBITrnfee.Text);
                //}
                //else
                //{
                //    customerAccountsVo.SebiTurnOverFee = 0.0;
                //}
                //if (Txt_Transcharges.Text != "")
                //{
                //    customerAccountsVo.TransactionCharges = double.Parse(Txt_Transcharges.Text);
                //}
                //else
                //{
                //    customerAccountsVo.TransactionCharges = 0.0;
                //}
                //if (Txt_stampcharges.Text != "")
                //{
                //    customerAccountsVo.StampCharges = double.Parse(Txt_stampcharges.Text);
                //}
                //else
                //{
                //    customerAccountsVo.StampCharges = 0.0;
                //}
                //if (Txt_STT.Text != "")
                //{
                //    customerAccountsVo.Stt = double.Parse(Txt_STT.Text);
                //}
                //else
                //{
                //    customerAccountsVo.Stt = 0.0;
                //}
                //if (Txt_ServiceTax.Text != "")
                //{
                //    customerAccountsVo.ServiceTax = double.Parse(Txt_ServiceTax.Text);
                //}
                //else
                //{
                //    customerAccountsVo.ServiceTax = 0.0;
                //}


                //if (txtBrokeragePerDelivery.Text != "" || txtBrokeragePerSpeculative.Text != "")
                //{

                //    customerAccountsVo.BrokerageDeliveryPercentage = double.Parse(txtBrokeragePerDelivery.Text);
                //    customerAccountsVo.BrokerageSpeculativePercentage = double.Parse(txtBrokeragePerSpeculative.Text);
                //    //customerAccountsVo.OtherCharges = double.Parse(txtOtherCharges.Text);
                //}
                //else
                //{
                //    customerAccountsVo.BrokerageDeliveryPercentage = 0.0;
                //    customerAccountsVo.BrokerageSpeculativePercentage = 0.0;
                //    customerAccountsVo.OtherCharges = 0.0;

                //}

                tradeAccountId = customerAccountBo.CreateCustomerEQAccount(customerAccountsVo, userVo.UserId);
                //Txt_ServiceTax.Text = string.Empty;


                //if (rbtnYes.Checked)
                //{
                //foreach (GridViewRow gvr in this.gvDPAccounts.Rows)
                //{
                //    if (((CheckBox)gvr.FindControl("chkId")).Checked == true)
                //    {

                //        dpAccountId = int.Parse(gvDPAccounts.DataKeys[gvr.RowIndex].Value.ToString());
                //        customerAccountBo.CreateEQTradeDPAssociation(tradeAccountId, dpAccountId, isDefault, userVo.UserId);
                //    }
                //}
                //}
                //DataSet TradeNum;
                //TradeNum = checkAccBo.CheckTradeNoAvailability(Id);

                //if (!CheckEQAccDetails(TradeNum))
                //{
                if (Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "MultipleEqEntry")
                {
                    string queryString = "?prevPage=TradeAccAdd";
                    // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleEqTransactionsEntry','" + queryString + "');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('RMMultipleEqTransactionsEntry','" + queryString + "');", true);

                }
                else if (Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "EquityManualSingleTransaction")
                {
                    string queryString = "?prevPage=TradeAccAdd";
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','" + queryString + "');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('EquityManualSingleTransaction','" + queryString + "');", true);
                }

                else if (Request.QueryString["Equitypopup"] == "1" && Request.QueryString["Equitypopup"] != null)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Trade Account Added Successfully');", true);

                }

                else
                {
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
                }
                //}
                //else
                //{
                //    Sample.Text = "Fail";
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
                FunctionInfo.Add("Method", "CustomerEQAccountAdd.ascx:btnSubmit_Click()");
                object[] objects = new object[6];
                objects[0] = customerAccountsVo;
                objects[1] = portfolioId;
                objects[2] = tradeAccountId;
                objects[3] = dpAccountId;
                objects[4] = isDefault;
                objects[5] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        //private bool CheckEQAccDetails(DataSet tradenum)
        //{
        //    for (int i = 0; i < tradenum.Tables[0].Rows.Count; i++)
        //    {
        //        if (tradenum.Tables[0].Rows[i][0] == txtTradeNum.Text)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}


        private void ViewEQAccountDetails()
        {


            ddlBrokerCode.SelectedValue = customerAccountsVo.BrokerCode;
            txtTradeNum.Text = customerAccountsVo.TradeNum;
            if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccountStartingDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
            else
                txtAccountStartingDate.Text = "";
            //txtBrokeragePerDelivery.Text = customerAccountsVo.BrokerageDeliveryPercentage.ToString();
            //txtBrokeragePerSpeculative.Text = customerAccountsVo.BrokerageSpeculativePercentage.ToString();
            //Txt_SEBITrnfee.Text = customerAccountsVo.SebiTurnOverFee.ToString();
            //Txt_Transcharges.Text = customerAccountsVo.TransactionCharges.ToString();
            //Txt_stampcharges.Text = customerAccountsVo.StampCharges.ToString();
            //Txt_STT.Text = customerAccountsVo.Stt.ToString();
            //Txt_ServiceTax.Text = customerAccountsVo.ServiceTax.ToString();
            //txtOtherCharges.Text = customerAccountsVo.OtherCharges.ToString();
            ddlBankList.SelectedValue = customerAccountsVo.BankNameInExtFile;
            BindAccountNum();
            ddlAccountNum.SelectedValue = customerAccountsVo.BankId.ToString();

            SetVisiblity(0);
        }

        private void EditEQAccountDetails()
        {
            if (customerAccountsVo.BrokerCode != "")
                ddlBrokerCode.SelectedValue = customerAccountsVo.BrokerCode;
            else
                ddlBrokerCode.SelectedValue = "NULL";
            txtTradeNum.Text = customerAccountsVo.TradeNum;
            if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccountStartingDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
            else
                txtAccountStartingDate.Text = "";
            //txtBrokeragePerDelivery.Text = customerAccountsVo.BrokerageDeliveryPercentage.ToString();
            //txtBrokeragePerSpeculative.Text = customerAccountsVo.BrokerageSpeculativePercentage.ToString();
            //txtOtherCharges.Text = customerAccountsVo.OtherCharges.ToString();
            //Txt_SEBITrnfee.Text = customerAccountsVo.SebiTurnOverFee.ToString();
            //Txt_Transcharges.Text = customerAccountsVo.TransactionCharges.ToString();
            //Txt_stampcharges.Text = customerAccountsVo.StampCharges.ToString();
            //Txt_STT.Text = customerAccountsVo.Stt.ToString();
            //Txt_ServiceTax.Text = customerAccountsVo.ServiceTax.ToString();
            ddlBankList.SelectedValue = customerAccountsVo.BankNameInExtFile;
            BindAccountNum();
            ddlAccountNum.SelectedValue = customerAccountsVo.BankId.ToString();
            BtnSetVisiblity(1);
            SetVisiblity(1);

        }

        private void SetVisiblity(int p)
        {
            if (p == 0)
            {
                // For View Mode
                ddlPortfolio.Enabled = false;
                ddlBrokerCode.Enabled = false;
                txtTradeNum.Enabled = false;
                txtAccountStartingDate.Enabled = false;
                //txtBrokeragePerDelivery.Enabled = false;
                //txtBrokeragePerSpeculative.Enabled = false;
                //txtOtherCharges.Enabled = false;
                ddlBankList.Enabled = false;
                ddlAccountNum.Enabled = false;
                //Txt_SEBITrnfee.Enabled = false;
                //Txt_Transcharges.Enabled = false;
                //Txt_stampcharges.Enabled = false;
                //Txt_STT.Enabled = false;
                //Txt_ServiceTax.Enabled = false;


            }
            else
            {
                //for Edit Mode
                ddlPortfolio.Enabled = true;
                ddlBrokerCode.Enabled = true;
                txtTradeNum.Enabled = true;
                txtAccountStartingDate.Enabled = true;
                //txtBrokeragePerDelivery.Enabled = true;
                //txtBrokeragePerSpeculative.Enabled = true;
                //txtOtherCharges.Enabled = true;
                ddlBankList.Enabled = true;
                ddlAccountNum.Enabled = true;
                //Txt_SEBITrnfee.Enabled = true;
                //Txt_Transcharges.Enabled = true;
                //Txt_stampcharges.Enabled = true;
                //Txt_STT.Enabled = true;
                //Txt_ServiceTax.Enabled = true;


            }
        }
        private void BtnSetVisiblity(int p)
        {
            if (p == 0)
            {   //for view selected
                //lblView.Text = "Equity Account Details";
                lblError.Visible = false;
                lnkEdit.Visible = true;
                btnSubmit.Visible = false;
                btnUpdate.Visible = false;
                lnkBack.Visible = false;

            }
            else
            {  //for Edit selected 
                //lblView.Text = "Modify Equity Account";
                lblError.Visible = true;
                lnkEdit.Visible = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
                lnkBack.Visible = true;
            }


        }
        /// <summary>
        /// getting customer banklist
        /// </summary>
        private void BindCustomerBankList()
        {

            DataSet ds = mforderBo.GetEQCustomerBank(customerVo.CustomerId);
            ddlBankList.DataSource = ds;
            ddlBankList.DataValueField = ds.Tables[0].Columns["WERPBM_BankCode"].ToString();
            ddlBankList.DataTextField = ds.Tables[0].Columns["WERPBM_BankName"].ToString();
            ddlBankList.DataBind();
            ddlBankList.Items.Insert(0, new ListItem("Select", "Select"));
        }

        /// <summary>
        /// Getting account number according to bank
        /// </summary>
        private void BindAccountNum()
        {
            DataTable dtAccountNo = new DataTable();
            DataSet dsbindAccount = new DataSet();
            int customerId = customerVo.CustomerId;
            string bankId = (ddlBankList.SelectedValue);
            account = ddlAccountNum.SelectedValue.ToString();
            if (ddlBankList.SelectedIndex != 0)
                dsbindAccount = customerAccountBo.GetEQAccountNumber(customerId, bankId);

            if (dsbindAccount.Tables.Count > 0)
            {
                dtAccountNo = dsbindAccount.Tables[0];
                ddlAccountNum.DataSource = dtAccountNo;
                ddlAccountNum.DataValueField = dtAccountNo.Columns["CB_CustBankAccId"].ToString();
                ddlAccountNum.DataTextField = dtAccountNo.Columns["CB_AccountNum"].ToString();
                ddlAccountNum.DataBind();
                // ddlAccountNum.SelectedValue = account;
            }
            ddlAccountNum.Items.Insert(0, new ListItem("Select", "0"));


        }

        /// <summary>
        /// getting the bank list and binding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //empty string initialization for bankcode
            string bankCode = string.Empty;
            //0 initialization for int customerid
            int customerId = 0;
            try
            {
                // see if the selectedindex is not zero
                if (ddlBankList.SelectedIndex != 0)
                {
                    BindAccountNum();

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
                FunctionInfo.Add("Method", "CustomerEQAccountAdd.ascx:ddlBankList_SelectedIndexChanged(object sender, EventArgs e)");
                object[] objects = new object[4];
                objects[0] = sender;
                objects[1] = e;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void ddlAccountNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
            {
                customerAccountsVo = (CustomerAccountsVo)Session["EQAccountVoRow"];
                if (Request.QueryString["action"].Trim() == "Edit")
                {
                    if (ddlAccountNum.SelectedIndex > 0)
                    {
                        if (customerAccountsVo != null)
                            BindEQLedgerMIS(customerVo.CustomerId, customerAccountsVo.AccountId, int.Parse(ddlAccountNum.SelectedValue));
                        else
                        {
                            tblMessage.Visible = true;
                            ErrorMessage.Visible = true;
                            ErrorMessage.InnerText = "Bank is not associated with Current trade  Account selected...!";
                        }
                    }
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string TradeAccNo;
            string BrokerCode;
            int PortfolioId;
            string oldaccountID;
            try
            {
                CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
                CustomerAccountAssociationVo AccountAssociationVo = new CustomerAccountAssociationVo();
                customerAccountsVo = (CustomerAccountsVo)Session["EQAccountVoRow"];
                oldaccountID = customerAccountsVo.TradeNum;
                newAccountVo.TradeNum = txtTradeNum.Text.ToString();
                if (oldaccountID == txtTradeNum.Text)
                {
                    newAccountVo.AccountId = customerAccountsVo.AccountId;

                    newAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedValue);
                    newAccountVo.BrokerCode = ddlBrokerCode.SelectedValue;

                    //newAccountVo.BrokerageDeliveryPercentage = double.Parse(txtBrokeragePerDelivery.Text);
                    //newAccountVo.BrokerageSpeculativePercentage = double.Parse(txtBrokeragePerSpeculative.Text);
                    //newAccountVo.SebiTurnOverFee = double.Parse(Txt_SEBITrnfee.Text);
                    //newAccountVo.TransactionCharges = double.Parse(Txt_Transcharges.Text);
                    //newAccountVo.StampCharges= double.Parse(Txt_stampcharges.Text);
                    //newAccountVo.Stt = double.Parse(Txt_STT.Text);
                    //newAccountVo.ServiceTax = double.Parse(Txt_ServiceTax.Text);
                    //newAccountVo.OtherCharges = double.Parse(txtOtherCharges.Text);

                    if (txtAccountStartingDate.Text != "")
                        newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountStartingDate.Text);
                    if (ddlBankList.SelectedIndex != 0)
                        newAccountVo.BankNameInExtFile = ddlBankList.SelectedValue;
                    if (ddlAccountNum.SelectedIndex != 0)
                        newAccountVo.BankId = int.Parse(ddlAccountNum.SelectedValue);
                    if (customerTransactionBo.UpdateCustomerEQAccountDetails(newAccountVo, userVo.UserId))
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
                }
                else
                {
                    TradeAccNo = txtTradeNum.Text;
                    BrokerCode = customerAccountsVo.BrokerCode.ToString();
                    PortfolioId = customerAccountsVo.PortfolioId;

                    if (ControlHost.CheckTradeNoAvailabilityForUpdate(TradeAccNo, BrokerCode, PortfolioId))
                    {
                        newAccountVo.AccountId = customerAccountsVo.AccountId;
                        newAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedValue);
                        newAccountVo.BrokerCode = ddlBrokerCode.SelectedValue;
                        //newAccountVo.BrokerageDeliveryPercentage = double.Parse(txtBrokeragePerDelivery.Text);
                        //newAccountVo.BrokerageSpeculativePercentage = double.Parse(txtBrokeragePerSpeculative.Text);
                        //newAccountVo.SebiTurnOverFee = double.Parse(Txt_SEBITrnfee.Text);
                        //newAccountVo.TransactionCharges = double.Parse(Txt_Transcharges.Text);
                        //newAccountVo.StampCharges = double.Parse(Txt_stampcharges.Text);
                        //newAccountVo.Stt = double.Parse(Txt_STT.Text);
                        //newAccountVo.ServiceTax = double.Parse(Txt_ServiceTax.Text);

                        //newAccountVo.OtherCharges = double.Parse(txtOtherCharges.Text);
                        if (txtAccountStartingDate.Text != "")
                            newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountStartingDate.Text);
                        if (ddlBankList.SelectedIndex != 0)
                            newAccountVo.BankNameInExtFile = ddlBankList.SelectedValue;
                        if (ddlAccountNum.SelectedIndex != 0)
                            newAccountVo.BankId = int.Parse(ddlAccountNum.SelectedValue);
                        if (customerTransactionBo.UpdateCustomerEQAccountDetails(newAccountVo, userVo.UserId))
                            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
                    }
                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Trade account already exists');", true);
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
                FunctionInfo.Add("Method", "CustomerEQAccountAdd.ascx:btnUpdate_Click");
                object[] objects = new object[4];
                objects[0] = customerAccountsVo;
                objects[1] = portfolioId;
                objects[2] = tradeAccountId;
                objects[3] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            customerAccountsVo = (CustomerAccountsVo)Session["EQAccountVoRow"];
            EditEQAccountDetails();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);

        }

        protected void txtTradeNum_TextChanged(object sender, EventArgs e)
        {

        }
        protected void gvEqMIS_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtEqLedgerMISDetails = new DataTable();
            dtEqLedgerMISDetails = (DataTable)Cache["gvEqMIS" + userVo.UserId];
            gvEqMIS.DataSource = dtEqLedgerMISDetails;
            gvEqMIS.Visible = true;
            imgbtnEqMIS.Visible = true;

        }
        protected void imgbtnEqMIS_Click(object sender, ImageClickEventArgs e)
        {
            gvEqMIS.ExportSettings.OpenInNewWindow = true;
            gvEqMIS.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvEqMIS.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvEqMIS.MasterTableView.ExportToExcel();
        }


    }
}
