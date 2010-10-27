using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Configuration;
using System.Data;
using WealthERP.Base;
using VoUser;
using BoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoProductMaster;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using System.Collections;

namespace WealthERP.CustomerPortfolio
{
    public partial class RMMultipleEqTransactionsEntry : System.Web.UI.UserControl
    {
        string path;

        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        DataTable dtRelationship = new DataTable();
        UserVo userVo = new UserVo();
        ProductEquityBo productEqutiyBo = new ProductEquityBo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();


        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            rmVo = (RMVo)Session[SessionContents.RmVo];



            txtParentCustomer_autoCompleteExtender.ContextKey = rmVo.RMId.ToString();
            cvTransactionDate.ValueToCompare = DateTime.Now.ToShortDateString();

            if (!IsPostBack && Session["EqTransactionHT"] == null) //Session["EqTransactionHT"] -> returning from trade account add.
            {
                BindLastTradeDate();
            }
            RestorePreviousState();
        }

        protected void txtParentCustomerId_ValueChanged(object sender, EventArgs e)
        {
            if (txtParentCustomerId.Value != string.Empty)
            {
                DataTable dt = customerBo.GetCustomerPanAddress(int.Parse(txtParentCustomerId.Value));
                DataRow dr = dt.Rows[0];
                txtPanParent.Text = dr["C_PANNum"].ToString();

                BindCustomerTradeAccount();
            }

        }

        protected void txtScrip_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = productEqutiyBo.GetScripCode(txtScrip.Text);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["PEM_ScripCode"] != null)
                hidScrip.Value = ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString();
            //txtTicker.Text = ds.Tables[0].Rows[0]["PEM_Ticker"].ToString();
            //scripCode = int.Parse(ds.Tables[0].Rows[0]["PEM_ScripCode"].ToString());
        }

        protected void btnSaveAndAdd_Click1(object sender, EventArgs e)
        {
            SaveEquityTransaction();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleEqTransactionsEntry','none');", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveEquityTransaction();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMEQMultipleTransactionsView','none');", true);
        }

        protected void btnAddTradeAccount_Click(object sender, EventArgs e)
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerVo customerVo = customerBo.GetCustomer(Convert.ToInt32(txtParentCustomerId.Value));

            customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
            Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
            Session["CustomerVo"] = customerVo;

            SaveCurrentPageState();


            string queryString = "?prevPage=MultipleEqEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('CustomerEQAccountAdd','" + queryString + "');", true);



            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('CustomerEQAccountAdd','none')", true);

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountAdd','none');", true);
        }


        private bool SaveEquityTransaction()
        {
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();

            try
            {
                // dt = productEquityBo.GetBrokerCode(portfolioId, ddlTradeAcc.SelectedItem.Text.ToString());

                eqTransactionVo.IsSourceManual = 1;
                eqTransactionVo.Brokerage = float.Parse(txtBrokerage.Text);
                //eqTransactionVo.BrokerCode = dt.Rows[0]["XB_BrokerCode"].ToString();

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
                if (txtParentCustomerId.Value != string.Empty)
                    eqTransactionVo.CustomerId = Convert.ToInt32(txtParentCustomerId.Value); //customerVo.CustomerId;
                eqTransactionVo.IsCorpAction = 0;
                if (rdoDelivery.Checked)
                {
                    eqTransactionVo.IsSpeculative = 0;
                }
                else if (rdoSpeculative.Checked)
                {
                    eqTransactionVo.IsSpeculative = 1;
                }
                //eqTransactionVo.EducationCess = (float)tempEducation;
                eqTransactionVo.ScripCode = Convert.ToInt32(hidScrip.Value);
                //eqTransactionVo.Exchange = ddlExchange.SelectedItem.Value.ToString();
                if (txtOtherCharges.Text != string.Empty)
                    eqTransactionVo.OtherCharges = float.Parse(txtOtherCharges.Text);
                if (txtQuantity.Text != string.Empty)
                    eqTransactionVo.Quantity = float.Parse(txtQuantity.Text);
                //eqTransactionVo.IsSpeculative = 0;
                eqTransactionVo.TradeType = "D";



                // eqTransactionVo.AccountId = ;
                if (txtRate.Text != string.Empty)
                    eqTransactionVo.Rate = float.Parse(txtRate.Text);
                //eqTransactionVo.RateInclBrokerage = float.Parse(txtRateIncBrokerage.Text);
                //temp = decimal.Round(Convert.ToDecimal(tempService), 3);
                //eqTransactionVo.ServiceTax = (float)temp;
                if (txtSTT.Text != string.Empty)
                    eqTransactionVo.STT = float.Parse(txtSTT.Text);

                eqTransactionVo.TradeDate = Convert.ToDateTime(txtTransactionDate.Text.Trim());// DateTime.Parse(txtTradeDate.Text);//ddlDay.SelectedItem.Text.ToString() + "/" + ddlMonth.SelectedItem.Value.ToString() + "/" + ddlYear.SelectedItem.Value.ToString()
                //eqTransactionVo.TradeTotal = float.Parse(txtTotal.Text);
                if (ddlTradeAccountNos.SelectedValue != "-1")
                    eqTransactionVo.AccountId = int.Parse(ddlTradeAccountNos.SelectedValue);
                //long.Parse(ddlTradeAcc.SelectedItem.Text.ToString());

                if (customerTransactionBo.AddEquityTransaction(eqTransactionVo, eqTransactionVo.CustomerId))
                {
                    CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
                    AdvisorVo advisorVo = new AdvisorVo();
                    advisorVo = (AdvisorVo)Session["advisorVo"];

                    customerPortfolioBo.UpdateAdviserDailyEODLogRevaluateForTransaction(advisorVo.advisorId, "EQ", eqTransactionVo.TradeDate);
                }
                //btnSubmit.Enabled = false;


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
                //objects[0] = portfolioId; ;
                //objects[1] = eqTransactionVo;
                //objects[2] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return false;
        }

        private void SaveCurrentPageState()
        {

            Hashtable hashtable = new Hashtable();
            hashtable.Add("Scrip", txtScrip.Text);
            hashtable.Add("ScripId", hidScrip.Value);
            hashtable.Add("TranType", ddlTransactionType.SelectedItem.Value.ToString());

            hashtable.Add("ParentCustomerId", txtParentCustomerId.Value);
            hashtable.Add("ParentCustomer", txtParentCustomer.Text);

            hashtable.Add("Quantity", txtQuantity.Text);

            hashtable.Add("Brokerage", txtBrokerage.Text);
            hashtable.Add("STT", txtSTT.Text);
            hashtable.Add("Rate", txtRate.Text);
            hashtable.Add("OtherCharges", txtOtherCharges.Text);
            hashtable.Add("GrossPrice", txtGrossPrice.Text);

            hashtable.Add("Mode", rdoDelivery.Checked);

            hashtable.Add("TransactionDate", txtTransactionDate.Text);
            hashtable.Add("PanParent", txtPanParent.Text);
            Session["EqTransactionHT"] = hashtable;
        }

        /// <summary>
        /// Maintain values after adding trade account.
        /// </summary>
        private void RestorePreviousState()
        {


            if (Session["EqTransactionHT"] != null && Request.QueryString["prevPage"] != null && Request.QueryString["prevPage"] == "TradeAccAdd")
            {
                Hashtable hashtable = new Hashtable();
                hashtable = (Hashtable)Session["EqTransactionHT"];
                txtScrip.Text = hashtable["Scrip"].ToString();

                hidScrip.Value = hashtable["ScripId"].ToString();
                ddlTransactionType.SelectedValue = hashtable["TranType"].ToString();
                txtParentCustomerId.Value = hashtable["ParentCustomerId"].ToString();
                txtParentCustomer.Text = hashtable["ParentCustomer"].ToString();
                txtQuantity.Text = hashtable["Quantity"].ToString();
                txtBrokerage.Text = hashtable["Brokerage"].ToString();
                txtSTT.Text = hashtable["STT"].ToString();
                txtRate.Text = hashtable["Rate"].ToString();
                txtOtherCharges.Text = hashtable["OtherCharges"].ToString();
                txtGrossPrice.Text = hashtable["GrossPrice"].ToString();
                if (Convert.ToBoolean(hashtable["Mode"]))
                    rdoDelivery.Checked = true;
                else
                    rdoSpeculative.Checked = true;
                txtTransactionDate.Text = hashtable["TransactionDate"].ToString();
                txtPanParent.Text = hashtable["PanParent"].ToString();
                BindCustomerTradeAccount();
            }
            Session["EqTransactionHT"] = null;
        }

        private void BindCustomerTradeAccount()
        {
            if (txtParentCustomerId.Value != string.Empty)
            {
                CustomerAccountBo customerAccountBo = new CustomerAccountBo();

                DataTable dtTradeAccountNumbers = customerAccountBo.GetTradeAccountNumbersByCustomer(int.Parse(txtParentCustomerId.Value));
                ddlTradeAccountNos.DataTextField = dtTradeAccountNumbers.Columns[1].ToString();
                ddlTradeAccountNos.DataValueField = dtTradeAccountNumbers.Columns[0].ToString();
                ddlTradeAccountNos.DataSource = dtTradeAccountNumbers;
                ddlTradeAccountNos.DataBind();

                ddlTradeAccountNos.Items.Insert(0, new ListItem("select", "-1"));
            }

        }

        private void BindLastTradeDate()
        {

            try
            {
                DataSet ds = customerTransactionBo.GetLastTradeDate();
                txtTransactionDate.Text = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString()).ToShortDateString();
            }
            catch (Exception ex)
            {
            }
            //txtToDate.Text = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString()).ToShortDateString();

        }

        protected void ddlTradeAccountNos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTradeAccountNos.SelectedValue != "-1")
            {
                CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
                CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
                CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
                PortfolioBo portfolioBo = new PortfolioBo();
                CustomerVo customerVo = customerBo.GetCustomer(Convert.ToInt32(txtParentCustomerId.Value));

                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                customerAccountsVo = customerTransactionBo.GetCustomerEQAccountDetails(int.Parse(ddlTradeAccountNos.SelectedValue.ToString()), customerPortfolioVo.PortfolioId);
                if (rdoDelivery.Checked)
                    hidBrokerRate.Value = customerAccountsVo.BrokerageDeliveryPercentage.ToString();
                else
                    hidBrokerRate.Value = customerAccountsVo.BrokerageSpeculativePercentage.ToString();
                hidOtherCharges.Value = customerAccountsVo.OtherCharges.ToString();

            }
            else
            {
                hidBrokerRate.Value = "0";
                hidOtherCharges.Value = "0";
            }
        }

    }
}

