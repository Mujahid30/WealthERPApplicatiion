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
    public partial class CustomerEQAccountRateAdd : System.Web.UI.UserControl
    {
        CustomerVo customerVo;
        CustomerPortfolioVo customerPortfolioVo;
        UserVo userVo;
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerAccountsVo customeraccountvo = new CustomerAccountsVo();
        int portfolioId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            customerVo = (CustomerVo)Session["CustomerVo"];
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {

                portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                LoadEquityTradeNumbers();


                if (Request.QueryString["action"] != "" && Request.QueryString["action"] != null)
                {
                    customeraccountvo = (CustomerAccountsVo)Session["EQAccountVoRow"];
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
            }

        }
          public void LoadEquityTradeNumbers()
        {
            DataSet dsEqutiyTradeNumbers;
           
                dsEqutiyTradeNumbers = customerAccountBo.GetCustomerEQAccounts(portfolioId, "DE");
                   DataTable dtCustomerAccounts = dsEqutiyTradeNumbers.Tables[0];
                    ddlAccountNo.DataSource = dtCustomerAccounts;
                    ddlAccountNo.DataTextField = "CETA_TradeAccountNum";
                    ddlAccountNo.DataValueField = "CETA_AccountId";
                    ddlAccountNo.DataBind();
                    ddlAccountNo.Items.Insert(0, new ListItem("Select the Trade Number", "Select the Trade Number"));
         }
            
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AddRateForEquityAccount();
        }
         public void AddRateForEquityAccount()
        {
            
            customeraccountvo.AccountId = Convert.ToInt16(ddlAccountNo.SelectedValue);
            if (ddlTransactionMode.SelectedItem.Text == "Speculative")
            {
                customeraccountvo.TransactionMode = 1;
            }
            else if (ddlTransactionMode.SelectedItem.Text == "Delivery")
            {
                customeraccountvo.TransactionMode = 0;
            }
            if (Ddl_Type.SelectedItem.Text == "Buy")
            {
                customeraccountvo.Type = "B";
            }
            else if (Ddl_Type.SelectedItem.Text == "Sell")
            {
                customeraccountvo.Type = "S";
            }
            else
            {
                customeraccountvo.Type = null;
            }
            if (TxtRate.Text != "")
            {
                customeraccountvo.Rate = double.Parse(TxtRate.Text);
            }
            if (Txt_SEBITrnfee.Text != "")
            {
                customeraccountvo.SebiTurnOverFee = double.Parse(Txt_SEBITrnfee.Text);
            }
            else
            {
                customeraccountvo.SebiTurnOverFee = 0.0;
            }
            if (Txt_Transcharges.Text != "")
            {
                customeraccountvo.TransactionCharges = double.Parse(Txt_Transcharges.Text);
            }
            else
            {
                customeraccountvo.TransactionCharges = 0.0;
            }
            if (Txt_stampcharges.Text != "")
            {
                customeraccountvo.StampCharges = double.Parse(Txt_stampcharges.Text);
            }
            else
            {
                customeraccountvo.StampCharges = 0.0;
            }
            if (Txt_STT.Text != "")
            {
                customeraccountvo.Stt = double.Parse(Txt_STT.Text);
            }
            else
            {
                customeraccountvo.Stt = 0.0;
            }
            if (Txt_ServiceTax.Text != "")
            {
                customeraccountvo.ServiceTax = double.Parse(Txt_ServiceTax.Text);
            }
            else
            {
                customeraccountvo.ServiceTax = 0.0;
            }
            if (chksebi.Checked == true)
            {
                customeraccountvo.IsSebiApplicableToStax = 1;
            }
            else
            {
                 customeraccountvo.IsSebiApplicableToStax = 0;
            }
            if (ChkTrxn.Checked == true)
            {
                customeraccountvo.IsTrxnApplicableToStax = 1;
            }
            else
            {
                customeraccountvo.IsTrxnApplicableToStax = 0;
            }
            if (Chkstamp.Checked == true)
            {
                customeraccountvo.IsStampApplicableToStax = 1;
            }
            else
            {
                customeraccountvo.IsStampApplicableToStax = 0;
            }
            if (Chkbrk.Checked == true)
            {
                customeraccountvo.IsBrApplicableToStax = 1;
            }
            else
            {
                customeraccountvo.IsBrApplicableToStax = 0;
            }
             
             
            customeraccountvo.StartDate = Convert.ToDateTime(txtstartdate.SelectedDate);
            if (txtendDate.SelectedDate!=DateTime.MinValue)
            customeraccountvo.EndDate = Convert.ToDateTime(txtendDate.SelectedDate);

           int flag= customerAccountBo.AddEquityRates(customeraccountvo);
           if (flag == 1)
           {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Rates Already Added');", true);
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Rates Added Successfully');", true);
           }
           resetcontrol();

        }

         private void resetcontrol()
         {
          ddlAccountNo.SelectedIndex=0;
          ddlTransactionMode.SelectedIndex=0;
          Ddl_Type.SelectedIndex=0;
          TxtRate.Text=string.Empty;
          Txt_SEBITrnfee.Text=string.Empty;
          Txt_Transcharges.Text = string.Empty;
          Txt_stampcharges.Text = string.Empty;
          Txt_STT.Text = string.Empty;
          Txt_ServiceTax.Text = string.Empty;
          txtstartdate.SelectedDate = null;
          txtendDate.SelectedDate = null;
          chksebi.Checked = false;
          ChkTrxn.Checked = false;
          Chkstamp.Checked = false;
          Chkbrk.Checked = false;
         

         }

         private void SetVisiblity(int p)
         {
             if (p == 0)
             {
                 // For View Mode
                 ddlAccountNo.Enabled = false;
                 ddlTransactionMode.Enabled = false;
                 Ddl_Type.Enabled = false;
                 TxtRate.Enabled = false;
               
                 Txt_SEBITrnfee.Enabled = false;
                 Txt_Transcharges.Enabled = false;
                 Txt_stampcharges.Enabled = false;
                 Txt_STT.Enabled = false;
                 Txt_ServiceTax.Enabled = false;
                 txtstartdate.Enabled = false;
                 txtendDate.Enabled = false;
                 chksebi.Enabled = false;
                 ChkTrxn.Enabled = false;
                 Chkstamp.Enabled = false;
                 Chkbrk.Enabled = false;

             }
             else
             {
               
                 ddlAccountNo.Enabled = true;
                 ddlTransactionMode.Enabled = true;
                 Ddl_Type.Enabled = true;
                 TxtRate.Enabled = true;
                 Txt_SEBITrnfee.Enabled = true;
                 Txt_Transcharges.Enabled = true;
                 Txt_stampcharges.Enabled = true;
                 Txt_STT.Enabled = true;
                 Txt_ServiceTax.Enabled = true;
                 txtstartdate.Enabled = true;
                 txtendDate.Enabled = true;
                 chksebi.Enabled = true;
                 ChkTrxn.Enabled = true;
                 Chkstamp.Enabled = true;
                 Chkbrk.Enabled = true;

             }
         }

         private void BtnSetVisiblity(int p)
         {
             if (p == 0)
             {  
                 lnkEdit.Visible = true;
                 btnSubmit.Visible = false;
                 btnUpdate.Visible = false;
                 lnkBack.Visible = false;

             }
             else
             {  

                 lnkEdit.Visible = false;
                 btnSubmit.Visible = false;
                 btnUpdate.Visible = true;
                 lnkBack.Visible = true;
             }
         }
         protected void lnkEdit_Click(object sender, EventArgs e)
         {
             customeraccountvo = (CustomerAccountsVo)Session["EQAccountVoRow"];
             EditEQAccountDetails();

         }
         private void EditEQAccountDetails()
         {
             if (customeraccountvo.AccountId != 0)
             {
                 ddlAccountNo.SelectedValue = Convert.ToInt16(customeraccountvo.AccountId).ToString();
             }
             if (customeraccountvo.TransactionMode !=null)
             {
                 ddlTransactionMode.SelectedValue =Convert.ToInt16(customeraccountvo.TransactionMode).ToString();
             }
             if (customeraccountvo.Type != "")
             {
                 Ddl_Type.SelectedValue = customeraccountvo.Type;
             }
             TxtRate.Text = customeraccountvo.Rate.ToString();
             Txt_SEBITrnfee.Text = customeraccountvo.SebiTurnOverFee.ToString();
             Txt_Transcharges.Text = customeraccountvo.TransactionCharges.ToString();
             Txt_stampcharges.Text = customeraccountvo.StampCharges.ToString();
             Txt_STT.Text = customeraccountvo.Stt.ToString();
             Txt_ServiceTax.Text = customeraccountvo.ServiceTax.ToString();
             if (customeraccountvo.IsSebiApplicableToStax == 1)
             {
                 chksebi.Checked = true;
             }
             if (customeraccountvo.IsTrxnApplicableToStax == 1)
             {
                 ChkTrxn.Checked = true;
             }
             if (customeraccountvo.IsStampApplicableToStax == 1)
             {
                 Chkstamp.Checked = true;
             }
             if (customeraccountvo.IsBrApplicableToStax == 1)
             {
                 Chkbrk.Checked = true;
             }
             if (customeraccountvo.StartDate != DateTime.MinValue)
             {
                 txtstartdate.SelectedDate = customeraccountvo.StartDate;
             }
             else
             {
                 txtstartdate.SelectedDate = null;
             }
             if (customeraccountvo.EndDate != DateTime.MinValue)
             {
                 txtendDate.SelectedDate = customeraccountvo.EndDate;
             }
             else
             {
                 txtendDate.SelectedDate = null;
             }
             BtnSetVisiblity(1);
             SetVisiblity(1);


         }
         private void ViewEQAccountDetails()
         {

             if (customeraccountvo.AccountId!=0)
             {
                 ddlAccountNo.SelectedValue =Convert.ToInt16(customeraccountvo.AccountId).ToString();
             }
             if (customeraccountvo.TransactionMode != null)
             {
                 ddlTransactionMode.SelectedValue = Convert.ToInt16(customeraccountvo.TransactionMode).ToString();
             }
             if (customeraccountvo.Type != "")
             {
                 Ddl_Type.SelectedValue = customeraccountvo.Type;
             }
             TxtRate.Text = customeraccountvo.Rate.ToString();
             Txt_SEBITrnfee.Text = customeraccountvo.SebiTurnOverFee.ToString();
             Txt_Transcharges.Text = customeraccountvo.TransactionCharges.ToString();
             Txt_stampcharges.Text = customeraccountvo.StampCharges.ToString();
             Txt_STT.Text = customeraccountvo.Stt.ToString();
             Txt_ServiceTax.Text = customeraccountvo.ServiceTax.ToString();
             if (customeraccountvo.IsSebiApplicableToStax == 1)
             {
                 chksebi.Checked = true;
             }
             if (customeraccountvo.IsTrxnApplicableToStax == 1)
             {
                 ChkTrxn.Checked = true;
             }
             if (customeraccountvo.IsStampApplicableToStax == 1)
             {
                 Chkstamp.Checked = true;
             }
             if (customeraccountvo.IsBrApplicableToStax == 1)
             {
                 Chkbrk.Checked = true;
             }
             if (customeraccountvo.StartDate != DateTime.MinValue)
             {
                 txtstartdate.SelectedDate = customeraccountvo.StartDate;
             }
             else
             {
                 txtstartdate.SelectedDate = null;
             }
             if (customeraccountvo.EndDate != DateTime.MinValue)
             {
                 txtendDate.SelectedDate = customeraccountvo.EndDate;
             }
             else
             {
                 txtendDate.SelectedDate = null;
             }
             SetVisiblity(0);
         }

         protected void btnCancel_Click(object sender, EventArgs e)
         {
             Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountRateView','none');", true);

         }

         protected void lnkBack_Click(object sender, EventArgs e)
         {
             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountRateView','none');", true);

         }

        

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            customeraccountvo.CebId = Convert.ToInt16(Session["CebId"].ToString());
            customeraccountvo.AccountId = Convert.ToInt16(ddlAccountNo.SelectedValue);
            if (ddlTransactionMode.SelectedItem.Text == "Speculative")
            {
                customeraccountvo.TransactionMode = 1;
            }
            else if (ddlTransactionMode.SelectedItem.Text == "Delivery")
            {
                customeraccountvo.TransactionMode = 0;
            }
            if (Ddl_Type.SelectedItem.Text == "Buy")
            {
                customeraccountvo.Type = "B";
            }
            else if (Ddl_Type.SelectedItem.Text == "Sell")
            {
                customeraccountvo.Type = "S";
            }
            else
            {
                customeraccountvo.Type = null;
            }
            if (TxtRate.Text != "")
            {
                customeraccountvo.Rate = double.Parse(TxtRate.Text);
            }
            if (Txt_SEBITrnfee.Text != "")
            {
                customeraccountvo.SebiTurnOverFee = double.Parse(Txt_SEBITrnfee.Text);
            }
            else
            {
                customeraccountvo.SebiTurnOverFee = 0.0;
            }
            if (Txt_Transcharges.Text != "")
            {
                customeraccountvo.TransactionCharges = double.Parse(Txt_Transcharges.Text);
            }
            else
            {
                customeraccountvo.TransactionCharges = 0.0;
            }
            if (Txt_stampcharges.Text != "")
            {
                customeraccountvo.StampCharges = double.Parse(Txt_stampcharges.Text);
            }
            else
            {
                customeraccountvo.StampCharges = 0.0;
            }
            if (Txt_STT.Text != "")
            {
                customeraccountvo.Stt = double.Parse(Txt_STT.Text);
            }
            else
            {
                customeraccountvo.Stt = 0.0;
            }
            if (Txt_ServiceTax.Text != "")
            {
                customeraccountvo.ServiceTax = double.Parse(Txt_ServiceTax.Text);
            }
            else
            {
                customeraccountvo.ServiceTax = 0.0;
            }

            if (chksebi.Checked == true)
            {
                customeraccountvo.IsSebiApplicableToStax = 1;
            }
            else
            {
                customeraccountvo.IsSebiApplicableToStax = 0;
            }
            if (ChkTrxn.Checked == true)
            {
                customeraccountvo.IsTrxnApplicableToStax = 1;
            }
            else
            {
                customeraccountvo.IsTrxnApplicableToStax = 0;
            }
            if (Chkstamp.Checked == true)
            {
                customeraccountvo.IsStampApplicableToStax = 1;
            }
            else
            {
                customeraccountvo.IsStampApplicableToStax = 0;
            }
            if (Chkbrk.Checked == true)
            {
                customeraccountvo.IsBrApplicableToStax = 1;
            }
            else
            {
                customeraccountvo.IsBrApplicableToStax = 0;
            }

            if (txtstartdate.SelectedDate!=DateTime.MinValue)
            {
                customeraccountvo.StartDate = Convert.ToDateTime(txtstartdate.SelectedDate);
            }
            if (txtendDate.SelectedDate != DateTime.MinValue)
            {
                customeraccountvo.EndDate = Convert.ToDateTime(txtendDate.SelectedDate);
            }
            int flag = customerAccountBo.UpdateEquityRates(customeraccountvo);
            //if (flag == 1)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Rates Already Added');", true);
            //}
            //else
            //{
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "leftpane", "loadcontrol('CustomerEQAccountRateView','none');", true);
            
        }
        }
    }
