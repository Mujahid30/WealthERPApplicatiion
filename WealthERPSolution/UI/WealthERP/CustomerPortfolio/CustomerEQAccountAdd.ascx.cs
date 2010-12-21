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
using System.Web.Services;
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
        CustomerAccountDao checkAccDao = new CustomerAccountDao();
       
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
                    if (Session[SessionContents.PortfolioId] != null)
                    {
                        portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                        BindPortfolioDropDown();



                        dtBroker = XMLBo.GetBroker(path);
                        ddlBrokerCode.DataSource = dtBroker;
                        ddlBrokerCode.DataTextField = "Broker";
                        ddlBrokerCode.DataValueField = "BrokerCode";
                        ddlBrokerCode.DataBind();

                        if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                        {
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
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
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
                if (txtBrokeragePerDelivery.Text == "")
                    txtBrokeragePerDelivery.Text = "0";
                if (txtBrokeragePerSpeculative.Text == "")
                    txtBrokeragePerSpeculative.Text = "0";
                if (txtOtherCharges.Text == "")
                    txtOtherCharges.Text = "0";
                if (txtBrokeragePerDelivery.Text != "" || txtBrokeragePerSpeculative.Text != "" || txtOtherCharges.Text != "")
                {

                    customerAccountsVo.BrokerageDeliveryPercentage = double.Parse(txtBrokeragePerDelivery.Text);
                    customerAccountsVo.BrokerageSpeculativePercentage = double.Parse(txtBrokeragePerSpeculative.Text);
                    customerAccountsVo.OtherCharges = double.Parse(txtOtherCharges.Text);
                }
                else
                {
                    customerAccountsVo.BrokerageDeliveryPercentage = 0.0;
                    customerAccountsVo.BrokerageSpeculativePercentage = 0.0;
                    customerAccountsVo.OtherCharges = 0.0;

                }
                tradeAccountId = customerAccountBo.CreateCustomerEQAccount(customerAccountsVo, userVo.UserId);

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
            customerAccountsVo = (CustomerAccountsVo)Session["EQAccountVoRow"];

            ddlBrokerCode.SelectedValue = customerAccountsVo.BrokerCode;
            txtTradeNum.Text = customerAccountsVo.TradeNum;
            if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccountStartingDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
            else
                txtAccountStartingDate.Text = "";
            txtBrokeragePerDelivery.Text = customerAccountsVo.BrokerageDeliveryPercentage.ToString();
            txtBrokeragePerSpeculative.Text = customerAccountsVo.BrokerageSpeculativePercentage.ToString();
            txtOtherCharges.Text = customerAccountsVo.OtherCharges.ToString();
            SetVisiblity(0);
        }

        private void EditEQAccountDetails()
        {
            customerAccountsVo = (CustomerAccountsVo)Session["EQAccountVoRow"];

            ddlBrokerCode.SelectedValue = customerAccountsVo.BrokerCode;
            txtTradeNum.Text = customerAccountsVo.TradeNum;
            if (customerAccountsVo.AccountOpeningDate != DateTime.MinValue)
                txtAccountStartingDate.Text = customerAccountsVo.AccountOpeningDate.ToShortDateString();
            else
                txtAccountStartingDate.Text = "";
            txtBrokeragePerDelivery.Text = customerAccountsVo.BrokerageDeliveryPercentage.ToString();
            txtBrokeragePerSpeculative.Text = customerAccountsVo.BrokerageSpeculativePercentage.ToString();
            txtOtherCharges.Text = customerAccountsVo.OtherCharges.ToString();
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
                txtBrokeragePerDelivery.Enabled = false;
                txtBrokeragePerSpeculative.Enabled = false;
                txtOtherCharges.Enabled = false;



            }
            else
            {
                //for Edit Mode
                ddlPortfolio.Enabled = true;
                ddlBrokerCode.Enabled = true;
                txtTradeNum.Enabled = true;
                txtAccountStartingDate.Enabled = true;
                txtBrokeragePerDelivery.Enabled = true;
                txtBrokeragePerSpeculative.Enabled = true;
                txtOtherCharges.Enabled = true;



            }
        }
        private void BtnSetVisiblity(int p)
        {
            if (p == 0)
            {   //for view selected
                lblView.Text = "Equity Account Details";
                lblError.Visible = false;
                lnkEdit.Visible = true;
                btnSubmit.Visible = false;
                btnUpdate.Visible = false;
                lnkBack.Visible = false;

            }
            else
            {  //for Edit selected 
                lblView.Text = "Modify Equity Account";
                lblError.Visible = true;
                lnkEdit.Visible = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
                lnkBack.Visible = true;
            }


        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerAccountsVo newAccountVo = new CustomerAccountsVo();
                CustomerAccountAssociationVo AccountAssociationVo = new CustomerAccountAssociationVo();
                customerAccountsVo = (CustomerAccountsVo)Session["EQAccountVoRow"];

                newAccountVo.AccountId = customerAccountsVo.AccountId;
                newAccountVo.PortfolioId = int.Parse(ddlPortfolio.SelectedValue);
                newAccountVo.BrokerCode = ddlBrokerCode.SelectedValue;
                newAccountVo.TradeNum = txtTradeNum.Text.ToString();
                newAccountVo.BrokerageDeliveryPercentage = double.Parse(txtBrokeragePerDelivery.Text);
                newAccountVo.BrokerageSpeculativePercentage = double.Parse(txtBrokeragePerSpeculative.Text);
                newAccountVo.OtherCharges = double.Parse(txtOtherCharges.Text);
                newAccountVo.AccountOpeningDate = DateTime.Parse(txtAccountStartingDate.Text);

                if (customerTransactionBo.UpdateCustomerEQAccountDetails(newAccountVo, userVo.UserId))
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountView','none');", true);

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
    }
}