using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BoCustomerProfiling;
using VoCustomerProfiling;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using BoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.General;
using Telerik.Web.UI;

namespace WealthERP.Customer
{
    public partial class AddBankDetails : System.Web.UI.UserControl
    {
        DataSet dsCustomerBankAccountDetails = new DataSet();
        UserVo userVo = null;
        RMVo rmVo = null;
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
        CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
        CustomerAccountsVo customeraccountVo = new CustomerAccountsVo();
        CustomerAccountAssociationVo customerAccountAssociationVo = new CustomerAccountAssociationVo();
        List<CustomerAccountsVo> TransactionList = new List<CustomerAccountsVo>();
        CommonLookupBo commonLookupBo = new CommonLookupBo();
        int custBankAccId;
        string path;
        string DisplayType;
        string Pagetype;
        int bankId = 0;
        string accountNum;
        double amount;
        string bankname;
        double holdingAmount=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());
            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["CustomerVo"];
            if (Request.QueryString["name"] != null)
            {
                Pagetype = Request.QueryString["name"].ToString();
            }
          if (Request.QueryString["BankId"] != null)
            {
                bankId = int.Parse(Request.QueryString["BankId"].ToString());
            }
            if (Request.QueryString["accountNum"] != null)
            {
                accountNum = Request.QueryString["accountNum"].ToString();
            }
            if (Request.QueryString["amount"] != null)
            {
                amount = double.Parse(Request.QueryString["amount"].ToString());
            }
            if (Request.QueryString["bankname"] != null)
            {
                bankname = Request.QueryString["bankname"].ToString();
            }



            BindDDLBankDetails();
            if (Pagetype == "Add")
            {
                lblheader.Text = "Add Bank Transaction/Balance";
               
                if (!IsPostBack)
                {
                    BindDDLBankDetails();
                    gvCashSavingTransaction.Visible = false;
                    DivTransaction.Visible = false;                    
                    BindDDLCategory();
                }
            }
            else if (Pagetype == "viewTransaction")
            {
                lblheader.Text = "View Bank Transaction";
                trAccount.Visible = false;
                trHoldingAndTrnx.Visible = false;
                trAddTransaction.Visible = false;
                imgBtnrgHoldings.Visible = true;
                if (!IsPostBack)
                {

                    BindTransactionGrid(bankId);
                    gvCashSavingTransaction.Visible = true;
                    DivTransaction.Visible = true;
                }
            }
            else if (Pagetype == "Editbalance")
            {

                lblheader.Text = "Add/Update Balance";
                lblAccId.Text = bankname.ToString() + "/" + accountNum.ToString();
                //lblBankName.Text = bankname.ToString();
                txtholdingAmt.Text = amount.ToString();
                //lblAccId.Text = customeraccountVo.AccountNum.ToString();
                ddlAccountDetails.Visible = false;
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
                trholdingamount.Visible = true;
            }
        }

        /// <summary>
        /// This function use to Getting the Transaction Grid
        /// </summary>
        private void BindTransactionGrid(int bankId)
        {
            DataTable dtBankAccId = new DataTable();
            //if (bankId == 0)
            //{
                //int i;
                //dtBankAccId = (DataTable)Session["BankAccId"];
                //if (dtBankAccId.Rows.Count > 0)
                //{
                //    for (i = 0; i < dtBankAccId.Rows.Count; i++)
                //    {
                //       custBankAccId = Convert.ToInt32(dtBankAccId.Rows[i]["CB_CustBankAccId"].ToString());
                //    }
                //}
                //(Session["BankAccId"]);
                // custBankAccId = Convert.ToInt32(ViewState["BankId"]);
            //    TransactionList = customerAccountBo.GetCustomerBankTransaction(custBankAccId);
            //}
            //else
           // {
                TransactionList = customerAccountBo.GetCustomerBankTransaction(bankId,customerVo.CustomerId);
           // }
            DataTable dtTransaction = new DataTable();
            dtTransaction.Columns.Add("WERPBM_BankCode");
            dtTransaction.Columns.Add("WERPBDTM_BankName");
            dtTransaction.Columns.Add("CB_AccountNum");
            dtTransaction.Columns.Add("CCST_TransactionId");
            dtTransaction.Columns.Add("CCST_ExternalTransactionId");
            dtTransaction.Columns.Add("CCST_Transactiondate",typeof(DateTime));
            dtTransaction.Columns.Add("CCST_Desc");
            dtTransaction.Columns.Add("CCST_ChequeNo");
            dtTransaction.Columns.Add("CCST_IsWithdrwal");
            dtTransaction.Columns.Add("WERP_CFCCode");
            dtTransaction.Columns.Add("WERP_CFCName");
            dtTransaction.Columns.Add("CCST_Amount");
            dtTransaction.Columns.Add("CB_HoldingAmount");
            DataRow drTransaction;
            for (int i = 0; i < TransactionList.Count; i++)
            {
                drTransaction = dtTransaction.NewRow();
                customeraccountVo = new CustomerAccountsVo();
                customeraccountVo = TransactionList[i];
                drTransaction["CCST_TransactionId"] = customeraccountVo.TransactionId.ToString();
                if (!string.IsNullOrEmpty(customeraccountVo.BankName))
                    drTransaction["WERPBM_BankCode"] = customeraccountVo.BankName.ToString();
                if (!string.IsNullOrEmpty(customeraccountVo.WERPBMBankName))
                    drTransaction["WERPBDTM_BankName"] = customeraccountVo.WERPBMBankName.ToString();
                if (!string.IsNullOrEmpty(customeraccountVo.BankName))
                    drTransaction["CB_AccountNum"] = customeraccountVo.BankAccountNum.ToString();

                if (customeraccountVo.ExternalTransactionId == null)
                {
                    drTransaction["CCST_ExternalTransactionId"] = "N/A";
                }
                else
                {
                    drTransaction["CCST_ExternalTransactionId"] = customeraccountVo.ExternalTransactionId.ToString();
                }
                drTransaction["CCST_Transactiondate"] = customeraccountVo.Transactiondate.ToString();
                if (customeraccountVo.CCST_Desc == null)
                {
                    drTransaction["CCST_Desc"] = "N/A";
                }
                else
                {
                    drTransaction["CCST_Desc"] = customeraccountVo.CCST_Desc.ToString().Trim();
                }
                if (customeraccountVo.ChequeNo == null)
                {
                    drTransaction["CCST_ChequeNo"] = "N/A";
                }
                else
                {
                    drTransaction["CCST_ChequeNo"] = customeraccountVo.ChequeNo.ToString().Trim();
                }
                if (customeraccountVo.IsWithdrwal == 0)
                {
                    drTransaction["CCST_IsWithdrwal"] = "CR";
                }
                else
                {
                    drTransaction["CCST_IsWithdrwal"] = "DR";
                }
                if (customeraccountVo.CFCCategoryCode == null)
                {
                    drTransaction["WERP_CFCCode"] = "N/A";
                }
                else
                drTransaction["WERP_CFCCode"] = customeraccountVo.CFCCategoryCode.ToString();
                drTransaction["WERP_CFCName"] = customeraccountVo.CFCCategoryName.ToString();
                drTransaction["CCST_Amount"] = double.Parse(customeraccountVo.Amount.ToString());
                dtTransaction.Rows.Add(drTransaction);
            }
            if (TransactionList.Count > 0)
            {
                if (Cache["gvCashSavingTransaction" + customerVo.CustomerId] == null)
                {
                    Cache.Insert("gvCashSavingTransaction" + customerVo.CustomerId, dtTransaction);
                }
                else
                {
                    Cache.Remove("gvCashSavingTransaction" + customerVo.CustomerId);
                    Cache.Insert("gvCashSavingTransaction" + customerVo.CustomerId, dtTransaction);
                }
                gvCashSavingTransaction.DataSource = dtTransaction;
                gvCashSavingTransaction.DataBind(); 
                gvCashSavingTransaction.Visible = true;
                imgBtnrgHoldings.Visible = true;
                // BindDDLBankDetails();
            }
            else
            {
                gvCashSavingTransaction.DataSource = dtTransaction;
                gvCashSavingTransaction.DataBind();
                gvCashSavingTransaction.Visible = true;
                imgBtnrgHoldings.Visible = false;

            }
        }

        /// <summary>
        /// This function use for Bind Account detail in DropDownlist
        /// </summary>
        public void BindDDLBankDetails()
        {
            int customerIdForGettingBankDetails = 0;
            customerVo = (CustomerVo)Session["CustomerVo"];
            customerIdForGettingBankDetails = customerVo.CustomerId;
            dsCustomerBankAccountDetails = customerBankAccountBo.GetCustomerIndividualBankDetails(customerIdForGettingBankDetails);
            ddlAccountDetails.DataSource = dsCustomerBankAccountDetails.Tables[1];
            ddlAccountDetails.DataTextField = "details";
            ddlAccountDetails.DataValueField = "CB_CustBankAccId";
            ddlAccountDetails.DataBind();
            // ViewState["BankId"] = dsCustomerBankAccountDetails.Tables[1];
            Session["BankAccId"] = dsCustomerBankAccountDetails.Tables[1];
           // ddlAccountDetails.Items.Insert(0, "Select");

        }

        public void ddlAccountDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ddlAccountDetails.SelectedIndex != 0)
            {
               // txtholdingAmt.Text = amount.ToString();
                trHoldingAndTrnx.Visible = true;
                trAddTransaction.Visible = false;
                DivTransaction.Visible = false;
            }
            else
            {
                trHoldingAndTrnx.Visible = false;
               
            }
            if (ddlAccountDetails.SelectedValue == "0")
            {
                trHoldingAndTrnx.Visible = false;
                trholdingamount.Visible = false;
                btnSubmit.Visible = false;
                trAddTransaction.Visible = false;
                DivTransaction.Visible = false;
            }
            if (ViewState["BankId"] != null)
                ViewState.Remove("BankId");
            bankId = int.Parse(ddlAccountDetails.SelectedValue);
            if (bankId != 0)
            {  
               
                holdingAmount =customerAccountBo.GetHoldingBalance(bankId);
                txtholdingAmt.Text = holdingAmount.ToString();

            }
            else {
                txtholdingAmt.Text = "0";
                }
            //{
            //    txtholdingAmt.Text = customeraccountVo.Amount.ToString(); 
            //}
            //else
            //{
            //    txtholdingAmt.Text="0";
            //}
 
            //  Session["BankAccId"] = ddlAccountDetails.SelectedValue.ToString();
        }

        public void ddlAccountSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayType = ddlAccountSelect.SelectedValue;
            if (DisplayType == "TB")
            {
                gvCashSavingTransaction.Visible = true;              
                btnSubmit.Visible = true;
                trAddTransaction.Visible = false;
                trholdingamount.Visible = true;
                DivTransaction.Visible = false;
            }
            if (DisplayType == "IT")
            {
                trAddTransaction.Visible = true;
                DivTransaction.Visible = false;
                btnSubmit.Visible = false;              
                gvCashSavingTransaction.Visible = false;
                trholdingamount.Visible = false;
                imgBtnrgHoldings.Visible = false;
            }
            if (DisplayType == "Select")
            {
                trAddTransaction.Visible = false;
                trholdingamount.Visible = false;
                btnSubmit.Visible = false;
            }

        }
        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ViewBankDetails", "loadcontrol('ViewBankDetails','none');", true);
           // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('PortfolioCashSavingsView', 'none')", true);
        }
        public void BindDDLCategory()
        {
            DataTable dtCFCCategory = new DataTable();
            dtCFCCategory = customerAccountBo.GetCashFlowCategory();
            ddlCFCCategory.DataSource = dtCFCCategory;
            ddlCFCCategory.DataValueField = dtCFCCategory.Columns["WERP_CFCCode"].ToString();
            ddlCFCCategory.DataTextField = dtCFCCategory.Columns["WERP_CFCName"].ToString();
            ddlCFCCategory.DataBind();
            ddlCFCCategory.Items.Insert(0, new ListItem("Select", "Select"));

        }
        protected void gvCashSavingTransaction_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                DataTable dtCFCCategory = new DataTable();
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlCFCCategory = (DropDownList)gefi.FindControl("ddlCFCCategory");
                dtCFCCategory = customerAccountBo.GetCashFlowCategory();
                ddlCFCCategory.DataSource = dtCFCCategory;
                ddlCFCCategory.DataValueField = dtCFCCategory.Columns["WERP_CFCCode"].ToString();
                ddlCFCCategory.DataTextField = dtCFCCategory.Columns["WERP_CFCName"].ToString();
                ddlCFCCategory.DataBind();
                ddlCFCCategory.Items.Insert(0, new ListItem("Select", "Select"));

            }
            string strCasflowcategory;

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                int accountId = int.Parse(gvCashSavingTransaction.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CCST_TransactionId"].ToString());
                strCasflowcategory = gvCashSavingTransaction.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WERP_CFCCode"].ToString();
                string rdbType = gvCashSavingTransaction.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CCST_IsWithdrwal"].ToString();
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DataTable dtCFCCategory = new DataTable();
                DropDownList ddlCFCCategory = (DropDownList)editedItem.FindControl("ddlCFCCategory");
                RadioButton rbtnY = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rbtnN = (RadioButton)e.Item.FindControl("rbtnNo");
                dtCFCCategory = customerAccountBo.GetCashFlowCategory();
                ddlCFCCategory.DataSource = dtCFCCategory;
                ddlCFCCategory.DataValueField = dtCFCCategory.Columns["WERP_CFCCode"].ToString();
                ddlCFCCategory.DataTextField = dtCFCCategory.Columns["WERP_CFCName"].ToString();
                ddlCFCCategory.DataBind();
                ddlCFCCategory.SelectedValue = strCasflowcategory;

                if (rdbType == "CR")
                {
                    rbtnY.Checked = false;
                    rbtnN.Checked = true;
                }
                else
                {

                    rbtnY.Checked = true;
                    rbtnN.Checked = false;
                }

                rbtnN.Enabled = false;
                rbtnY.Enabled = false;
                RadDatePicker dpTransactionDate = (RadDatePicker)e.Item.FindControl("dpTransactionDate");
                dpTransactionDate.SelectedDate = customeraccountVo.Transactiondate;

            }
            else
            {
                rbtnN.Enabled = true;
                rbtnY.Enabled = true;
            }
        }
        protected void gvCashSavingTransaction_ItemCommand(object source, GridCommandEventArgs e)
        {
            int accountId = 0;
            int customerId = 0;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                RadDatePicker dpTransactionDate = (RadDatePicker)e.Item.FindControl("dpTransactionDate");
                TextBox txtDescripton = (TextBox)e.Item.FindControl("txtDescripton");
                TextBox txtChequeNo = (TextBox)e.Item.FindControl("txtChequeNo");
                DateTime date = Convert.ToDateTime(dpTransactionDate.SelectedDate);
                DropDownList ddlCFCCategory = (DropDownList)e.Item.FindControl("ddlCFCCategory");
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                TextBox txtExternalTransactionId = (TextBox)e.Item.FindControl("txtExternalTransactionId");
                RadioButton rbtnY = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rbtnN = (RadioButton)e.Item.FindControl("rbtnNo");
                customeraccountVo = new CustomerAccountsVo();
                customeraccountVo.ExternalTransactionId = txtExternalTransactionId.Text.ToString();
                customeraccountVo.Transactiondate = date;
                customeraccountVo.CCST_Desc = txtDescripton.Text.ToString();
                customeraccountVo.ChequeNo = txtChequeNo.Text.ToString();
                customeraccountVo.CFCCategoryCode = ddlCFCCategory.SelectedItem.Value.ToString();
                customeraccountVo.Amount = double.Parse(txtAmount.Text.ToString());               
                customeraccountVo.AccountId = bankId;
                if (rbtnN.Checked)
                {
                    customeraccountVo.IsWithdrwal = 0;
                }
                if (rbtnY.Checked)
                {
                    customeraccountVo.IsWithdrwal = 1;
                }
                accountId = int.Parse(gvCashSavingTransaction.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CCST_TransactionId"].ToString());
                customerAccountBo.UpdateCustomerBankTransaction(customeraccountVo, accountId);

            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                DataTable dtBankAccId = new DataTable();
                dtBankAccId = (DataTable)Session["BankAccId"];//string strBankAccountID = ViewState["BankId"].ToString();
                RadDatePicker dpTransactionDate = (RadDatePicker)e.Item.FindControl("dpTransactionDate");
                TextBox txtDescripton = (TextBox)e.Item.FindControl("txtDescripton");
                TextBox txtChequeNo = (TextBox)e.Item.FindControl("txtChequeNo");
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                TextBox txtExternalTransactionId = (TextBox)e.Item.FindControl("txtExternalTransactionId");
                DateTime date = Convert.ToDateTime(dpTransactionDate.SelectedDate);
                DropDownList ddlCFCCategory = (DropDownList)e.Item.FindControl("ddlCFCCategory");
                RadioButton rbtnY = (RadioButton)e.Item.FindControl("rbtnYes");
                RadioButton rbtnN = (RadioButton)e.Item.FindControl("rbtnNo");
                RMVo rmVo = new RMVo();
                int userId;
                rmVo = (RMVo)Session["RmVo"];
                userId = rmVo.UserId;
                customerId = customerVo.CustomerId;
                customeraccountVo = new CustomerAccountsVo();
                customeraccountVo.ExternalTransactionId = txtExternalTransactionId.Text.ToString();
                customeraccountVo.Transactiondate = date;
                customeraccountVo.CCST_Desc = txtDescripton.Text.ToString();
                customeraccountVo.ChequeNo = txtChequeNo.Text.ToString();
                customeraccountVo.CFCCategoryCode = ddlCFCCategory.SelectedItem.Value.ToString();
                customeraccountVo.Amount = double.Parse(txtAmount.Text.ToString());
                //int i;
                //dtBankAccId = (DataTable)Session["BankAccId"];
                //if (dtBankAccId.Rows.Count > 0)
                //{
                //    for (i = 0; i < dtBankAccId.Rows.Count; i++)
                //    {
                //        custBankAccId = Convert.ToInt32(dtBankAccId.Rows[i]["CB_CustBankAccId"].ToString());
                //    }
                //}
                customeraccountVo.AccountId = bankId;
                if (rbtnN.Checked)
                {
                    customeraccountVo.IsWithdrwal = 0;
                }
                if (rbtnY.Checked)
                {
                    customeraccountVo.IsWithdrwal = 1;
                }

                customerAccountBo.CreatecustomerBankTransaction(customeraccountVo, userId);

            }

            if (e.CommandName == "Delete")
            {
                bool isdeleted = false;
                accountId = int.Parse(gvCashSavingTransaction.MasterTableView.DataKeyValues[e.Item.ItemIndex]["CCST_TransactionId"].ToString());
                isdeleted = customerAccountBo.DeleteCustomerBankTransaction(accountId);
                //if (isdeleted == false)
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('Cannot delete the bank is associate with a folio');", true);
            }
            BindTransactionGrid(bankId);
        }
        /// <summary>
        /// This button use to insert Holding Amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // int i;
             //DataTable dtBankAccId = new DataTable();
             //dtBankAccId = (DataTable)Session["BankAccId"];
             //if (dtBankAccId.Rows.Count > 0)
             //{
             //    for (i = 0; i < dtBankAccId.Rows.Count; i++)
             //    {
             //        custBankAccId = Convert.ToInt32(dtBankAccId.Rows[i]["CB_CustBankAccId"].ToString());
             //    }
             //}
            
            customeraccountVo = new CustomerAccountsVo();
            int customerId = 0;
            customerVo = (CustomerVo)Session["customerVo"];
            customerId = customerVo.CustomerId;
            customeraccountVo.AccountId = int.Parse(ddlAccountDetails.SelectedValue);
            customeraccountVo.Amount = double.Parse(txtholdingAmt.Text.ToString());
            customerAccountBo.InsertholdingAmountCustomerBank(customeraccountVo, customerId);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBankDetails','none');", true);
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //int i;
            bool accountId;
            //DataTable dtBankAccId = new DataTable();
            //dtBankAccId = (DataTable)Session["BankAccId"];
            //if (dtBankAccId.Rows.Count > 0)
            //{
            //    for (i = 0; i < dtBankAccId.Rows.Count; i++)
            //    {
            //        custBankAccId = Convert.ToInt32(dtBankAccId.Rows[i]["CB_CustBankAccId"].ToString());
            //    }
            //}
            customeraccountVo = new CustomerAccountsVo();
            int customerId = 0;
            customerVo = (CustomerVo)Session["customerVo"];
            customerId = customerVo.CustomerId;
            //accountId = customerAccountBo.CheckTransactionExistanceOnHoldingAdd(bankId);
            //if (accountId == true)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Transaction all ready exist first delete Transactions !!');", true);
            //}
            //else
            customeraccountVo.AccountId = bankId;
            customeraccountVo.Amount = double.Parse(txtholdingAmt.Text.ToString());
            customerAccountBo.InsertholdingAmountCustomerBank(customeraccountVo, customerId);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewBankDetails','none');", true);
        }
        /// <summary>
        /// This Button use to Add Transaction Detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitTransaction_Click(object sender, EventArgs e)
        {

            int customerId = 0;
            DataTable dtBankAccId = new DataTable();
            dtBankAccId = (DataTable)Session["BankAccId"];
            //  string strBankAccountID = ViewState["BankId"].ToString();
            RMVo rmVo = new RMVo();
            int userId;
            rmVo = (RMVo)Session["RmVo"];
            userId = rmVo.UserId;
            customerId = customerVo.CustomerId;
            customeraccountVo = new CustomerAccountsVo();
            customeraccountVo.ExternalTransactionId = txtExternalTransactionId.Text.ToString();
            DateTime date = Convert.ToDateTime(dpTransactionDate.SelectedDate);
            customeraccountVo.Transactiondate = date;
            customeraccountVo.CCST_Desc = txtDescripton.Text.ToString();
            customeraccountVo.ChequeNo = txtChequeNo.Text.ToString();
            customeraccountVo.CFCCategoryCode = ddlCFCCategory.SelectedItem.Value.ToString();
            if (txtAmount.Text.ToString() != "")
            {
                customeraccountVo.Amount = double.Parse(txtAmount.Text.ToString());
            }
            else
            {
                customeraccountVo.Amount = 0;
            }
            //int i;
            //dtBankAccId = (DataTable)Session["BankAccId"];
            //if (dtBankAccId.Rows.Count > 0)
            //{
            //    for (i = 0; i < dtBankAccId.Rows.Count; i++)
            //    {
            //        custBankAccId = Convert.ToInt32(dtBankAccId.Rows[i]["CB_CustBankAccId"].ToString());
            //    }
            //}

            customeraccountVo.AccountId = int.Parse(ddlAccountDetails.SelectedValue);
            bankId = customeraccountVo.AccountId;
            if (rbtnN.Checked)
            {
                customeraccountVo.IsWithdrwal = 0;
            }
            if (rbtnY.Checked)
            {
                customeraccountVo.IsWithdrwal = 1;
            }
            customerAccountBo.CreatecustomerBankTransaction(customeraccountVo, userId);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AddBankDetails','?name=" + "viewTransaction" + "');", true);
        }
        //protected void Holding_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbtnholding.Checked == true)
        //    {
        //        gvCashSavingTransaction.Visible = true;
        //        btnSubmit.Visible = true;
        //        BindTransactionGrid();
        //        trAddTransaction.Visible = false;
        //        trholdingamount.Visible = true;
        //        DivTransaction.Visible = false;
        //    }
        //    if (rbtntransaction.Checked == true)
        //    {
        //        trAddTransaction.Visible = true;
        //        DivTransaction.Visible = true;
        //        btnSubmit.Visible = false;
        //        BindTransactionGrid();
        //        gvCashSavingTransaction.Visible = true;
        //        trholdingamount.Visible = false;
        //    }
        //}
        protected void gvCashSavingTransaction_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvTransaction = new DataTable();
            if (Cache["gvCashSavingTransaction" + customerVo.CustomerId] != null)
            {
                dtgvTransaction = (DataTable)Cache["gvCashSavingTransaction" + customerVo.CustomerId];              
                gvCashSavingTransaction.DataSource = dtgvTransaction;
                gvCashSavingTransaction.Visible = true;
               
            }
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvCashSavingTransaction.ExportSettings.OpenInNewWindow = true;
            gvCashSavingTransaction.ExportSettings.IgnorePaging = true;
            gvCashSavingTransaction.ExportSettings.HideStructureColumns = true;
            gvCashSavingTransaction.ExportSettings.ExportOnlyData = true;
            gvCashSavingTransaction.ExportSettings.FileName = "Transaction Detail";
            gvCashSavingTransaction.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCashSavingTransaction.MasterTableView.ExportToExcel();
        }

    }
}




























































#region Previous Code of Bank detail


//CustomerBankAccountBo customerBankAccountBo = new CustomerBankAccountBo();
//CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
//CustomerVo customerVo = new CustomerVo();
//CustomerBo customerBo = new CustomerBo();

//RMVo rmVo = new RMVo();
//DataTable dtAccountType = new DataTable();
//DataTable dtModeofOperation = new DataTable();
//DataTable dtStates = new DataTable();
//int customerId;
//int userId;
//string chk;
//string path;

//protected void Page_Load(object sender, EventArgs e)
//{
//    try
//    {
//        SessionBo.CheckSession();
//        path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

//        if (!IsPostBack)
//        {
//            BindDropDowns(path);
//        }
//    }
//    catch (BaseApplicationException Ex)
//    {
//        throw Ex;
//    }
//    catch (Exception Ex)
//    {
//        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//        NameValueCollection FunctionInfo = new NameValueCollection();
//        FunctionInfo.Add("Method", "AddBankDetails.ascx:Page_Load()");
//        object[] objects = new object[1];
//        objects[0] = path;
//        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//        exBase.AdditionalInformation = FunctionInfo;
//        ExceptionManager.Publish(exBase);
//        throw exBase;

//    }

//}

//private void BindDropDowns(string path)
//{
//    try
//    {
//        dtAccountType = XMLBo.GetBankAccountTypes(path);
//        ddlAccountType.DataSource = dtAccountType;
//        ddlAccountType.DataTextField = "BankAccountType";
//        ddlAccountType.DataValueField = "BankAccountTypeCode";
//        ddlAccountType.DataBind();
//        ddlAccountType.Items.Insert(0, new ListItem("Select", "Select"));

//        dtModeofOperation = XMLBo.GetModeOfHolding(path);
//        ddlModeOfOperation.DataSource = dtModeofOperation;
//        ddlModeOfOperation.DataTextField = "ModeOfHolding";
//        ddlModeOfOperation.DataValueField = "ModeOfHoldingCode";
//        ddlModeOfOperation.DataBind();
//        ddlModeOfOperation.Items.Insert(0, new ListItem("Select", "Select"));

//        dtStates = XMLBo.GetStates(path);
//        ddlBankAdrState.DataSource = dtStates;
//        ddlBankAdrState.DataTextField = "StateName";
//        ddlBankAdrState.DataValueField = "StateCode";
//        ddlBankAdrState.DataBind();
//        ddlBankAdrState.Items.Insert(0, new ListItem("Select", "Select"));
//    }

//    catch (BaseApplicationException Ex)
//    {
//        throw Ex;
//    }
//    catch (Exception Ex)
//    {
//        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//        NameValueCollection FunctionInfo = new NameValueCollection();
//        FunctionInfo.Add("Method", "AddBankDetails.ascx:BindDropDowns()");
//        object[] objects = new object[4];
//        objects[0] = path;
//        objects[1] = dtAccountType;
//        objects[2] = dtModeofOperation;
//        objects[3] = dtStates;
//        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//        exBase.AdditionalInformation = FunctionInfo;
//        ExceptionManager.Publish(exBase);
//        throw exBase;

//    }

//}

//protected void btnYes_Click(object sender, EventArgs e)
//{
//    //List<int> customerIds = new List<int>();
//    //int customerId = 0;
//    try
//    {
//        if (Validation())
//        {
//            rmVo = (RMVo)Session["RmVo"];
//            //  customerVo = (CustomerVo)Session["CustomerVo"];
//            userId = rmVo.UserId;
//            // customerId = customerVo.CustomerId;
//            if (Session["Check"] != null)
//            {
//                chk = Session["Check"].ToString();
//            }
//            //if (Session["CustomerIds"] != null)
//            //{
//            //    customerIds = (List<int>)Session["CustomerIds"];
//            //    customerId = customerIds[1];
//            //}
//            //else
//            //{
//            //    customerVo = (CustomerVo)Session["customerVo"];
//            //    customerId = customerVo.CustomerId;
//            //}
//            customerVo = (CustomerVo)Session["customerVo"];
//            customerId = customerVo.CustomerId;

//            //if (ddlModeOfOperation.SelectedValue.ToString() != "Select a Mode of Holding")
//            //    customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedValue.ToString();
//            //customerBankAccountVo.BankName = txtBankName.Text.ToString();
//            //customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
//            //customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
//            //customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
//            //customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
//            //if (txtBankAdrPinCode.Text.ToString() != "")
//            //    customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
//            //customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
//            //if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
//            //    customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
//            //customerBankAccountVo.BranchAdrCountry = ddlBankAdrCountry.SelectedValue.ToString();
//            //if (txtMicr.Text.ToString() != "")
//            //    customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
//            //customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
//            //customerBankAccountVo.Balance = 0;

//            //customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);

//            customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
//            customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();

//            if (ddlModeOfOperation.SelectedValue.ToString() != "Select a Mode of Holding")
//                customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedValue.ToString();
//            customerBankAccountVo.BankName = txtBankName.Text.ToString();
//            customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
//            customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
//            customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
//            customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
//            if (txtBankAdrPinCode.Text.ToString() != "")
//                customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
//            customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
//            if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
//                customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
//            customerBankAccountVo.BranchAdrCountry = ddlBankAdrCountry.SelectedValue.ToString();
//            if (txtMicr.Text.ToString() != "")
//                customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
//            customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
//            customerBankAccountVo.Balance = 0;
//            //customerBankAccountVo.Balance = long.Parse(txtBalance.Text.ToString());

//            customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);


//            txtAccountNumber.Text = "";
//            txtBankAdrLine1.Text = "";
//            txtBankAdrLine2.Text = "";
//            txtBankAdrLine3.Text = "";
//            txtBankAdrPinCode.Text = "";
//            txtBankAdrCity.Text = "";
//            txtBankName.Text = "";
//            txtBranchName.Text = "";
//            txtIfsc.Text = "";
//            txtMicr.Text = "";
//            ddlAccountType.SelectedIndex = 0;
//            ddlModeOfOperation.SelectedIndex = 0;

//            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bank details added successfully');", true);


//        }
//        else
//        {

//        }
//    }
//    catch (BaseApplicationException Ex)
//    {
//        throw Ex;
//    }
//    catch (Exception Ex)
//    {
//        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//        NameValueCollection FunctionInfo = new NameValueCollection();
//        FunctionInfo.Add("Method", "AddBankDetails.ascx:btnYes_Click()");
//        object[] objects = new object[3];
//        objects[0] = customerBankAccountVo;
//        objects[1] = customerVo;
//        objects[2] = rmVo;
//        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//        exBase.AdditionalInformation = FunctionInfo;
//        ExceptionManager.Publish(exBase);
//        throw exBase;

//    }

//}

//protected void btnNo_Click(object sender, EventArgs e)
//{
//    //List<int> customerIds = new List<int>();
//    //int customerId = 0;
//    try
//    {
//        if (Validation())
//        {
//            rmVo = (RMVo)Session["RmVo"];

//            if (Session["Check"] != null)
//            {
//                chk = Session["Check"].ToString();
//            }
//            userId = rmVo.UserId;

//            //if (Session["CustomerIds"] != null)
//            //{
//            //    customerIds = (List<int>)Session["CustomerIds"];
//            //    customerId = customerIds[1];
//            //}
//            //else
//            //{
//            //    customerVo = (CustomerVo)Session["customerVo"];
//            //    customerId = customerVo.CustomerId;
//            //}

//            customerVo = (CustomerVo)Session["customerVo"];
//            customerId = customerVo.CustomerId;

//            customerBankAccountVo.AccountType = ddlAccountType.SelectedValue.ToString();
//            customerBankAccountVo.BankAccountNum = txtAccountNumber.Text.ToString();

//            if (ddlModeOfOperation.SelectedValue.ToString() != "Select a Mode of Holding")
//                customerBankAccountVo.ModeOfOperation = ddlModeOfOperation.SelectedValue.ToString();
//            customerBankAccountVo.BankName = txtBankName.Text.ToString();
//            customerBankAccountVo.BranchName = txtBranchName.Text.ToString();
//            customerBankAccountVo.BranchAdrLine1 = txtBankAdrLine1.Text.ToString();
//            customerBankAccountVo.BranchAdrLine2 = txtBankAdrLine2.Text.ToString();
//            customerBankAccountVo.BranchAdrLine3 = txtBankAdrLine3.Text.ToString();
//            if (txtBankAdrPinCode.Text.ToString() != "")
//                customerBankAccountVo.BranchAdrPinCode = int.Parse(txtBankAdrPinCode.Text.ToString());
//            customerBankAccountVo.BranchAdrCity = txtBankAdrCity.Text.ToString();
//            if (ddlBankAdrState.SelectedValue.ToString() != "Select a State")
//                customerBankAccountVo.BranchAdrState = ddlBankAdrState.SelectedValue.ToString();
//            customerBankAccountVo.BranchAdrCountry = ddlBankAdrCountry.SelectedValue.ToString();
//            if (txtMicr.Text.ToString() != "")
//                customerBankAccountVo.MICR = long.Parse(txtMicr.Text.ToString());
//            customerBankAccountVo.IFSC = txtIfsc.Text.ToString();
//            customerBankAccountVo.Balance = 0;
//            //customerBankAccountVo.Balance = long.Parse(txtBalance.Text.ToString());

//            customerBankAccountBo.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);

//            if (chk == "CustomerAdd")
//            {
//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerProofsAdd','none');", true);
//            }
//            else if (chk == "Family")
//            {

//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewCustomerFamily','none');", true);
//            }
//            else if (chk == "Dashboard" || Session["Check"] == null)
//            {
//                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBankDetails','none');", true);
//            }
//            txtAccountNumber.Text = "";
//            txtBankAdrLine1.Text = "";
//            txtBankAdrLine2.Text = "";
//            txtBankAdrLine3.Text = "";
//            txtBankAdrPinCode.Text = "";
//            txtBankAdrCity.Text = "";
//            txtBankName.Text = "";
//            txtBranchName.Text = "";
//            txtIfsc.Text = "";
//            txtMicr.Text = "";
//            ddlAccountType.SelectedIndex = 0;
//            ddlModeOfOperation.SelectedIndex = 0;

//            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Bank details added successfully');", true);

//        }
//        else
//        {
//        }



//    }
//    catch (BaseApplicationException Ex)
//    {
//        throw Ex;
//    }
//    catch (Exception Ex)
//    {
//        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//        NameValueCollection FunctionInfo = new NameValueCollection();
//        FunctionInfo.Add("Method", "AddBankDetails.ascx:btnNo_Click()");
//        object[] objects = new object[3];
//        objects[0] = customerBankAccountVo;
//        objects[1] = customerVo;
//        objects[2] = rmVo;

//        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//        exBase.AdditionalInformation = FunctionInfo;
//        ExceptionManager.Publish(exBase);
//        throw exBase;
//    }
//}
//public bool Validation()
//{
//    bool result = true;
//    try
//    {
//        if (ddlAccountType.SelectedItem.Value == "")
//        {
//            lblAccountType.CssClass = "Error";
//            result = false;
//        }
//        else
//        {
//            lblAccountType.CssClass = "FieldName";
//            result = true;
//        }
//        if (txtAccountNumber.Text.ToString() == "")
//        {
//            lblAccountNumber.CssClass = "Error";
//            result = false;
//        }
//        else
//        {
//            lblAccountNumber.CssClass = "FieldName";
//            result = true;
//        }
//        if (ddlModeOfOperation.SelectedItem.Value == "")
//        {
//            lblModeOfOperation.CssClass = "Error";
//            result = false;
//        }
//        else
//        {
//            lblModeOfOperation.CssClass = "FieldName";
//            result = true;
//        }
//        if (txtBankName.Text.ToString() == "")
//        {
//            lblBankName.CssClass = "Error";
//            result = false;
//        }
//        else
//        {
//            lblBankName.CssClass = "FieldName";
//            result = true;
//        }
//        if (txtBranchName.Text.ToString() == "")
//        {
//            lblBranchName.CssClass = "Error";
//            result = false;
//        }
//        else
//        {
//            lblBranchName.CssClass = "FieldName";
//            result = true;
//        }
//        //if (txtBankAdrLine1.Text.ToString() == "")
//        //{
//        //    lblAdrLine1.CssClass = "Error";
//        //    result = false;
//        //}
//        //else
//        //{
//        //    lblAdrLine1.CssClass = "FieldName";
//        //    result = true;
//        //}
//        //if (txtBankAdrPinCode.Text.ToString() == "")
//        //{
//        //    lblPinCode.CssClass = "Error";
//        //    result = false;
//        //}
//        //else
//        //{
//        //    lblPinCode.CssClass = "FieldName";
//        //    result = true;
//        //}
//        //if (txtBankAdrCity.Text.ToString() == "")
//        //{
//        //    lblCity.CssClass = "Error";
//        //    result = false;
//        //}
//        //else
//        //{
//        //    lblCity.CssClass = "FieldName";
//        //    result = true;
//        //}
//        //if (txtMicr.Text.ToString() == "")
//        //{
//        //    lblMicr.CssClass = "Error";
//        //    result = false;
//        //}
//        //else
//        //{
//        //    lblMicr.CssClass = "FieldName";
//        //    result = true;
//        //}
//        //if (txtIfsc.Text.ToString() == "")
//        //{
//        //    lblIfsc.CssClass = "Error";
//        //    result = false;
//        //}
//        //else
//        //{
//        //    lblIfsc.CssClass = "FieldName";
//        //    result = true;
//        //}
//    }

//    catch (BaseApplicationException Ex)
//    {
//        throw Ex;
//    }
//    catch (Exception Ex)
//    {
//        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//        NameValueCollection FunctionInfo = new NameValueCollection();
//        FunctionInfo.Add("Method", "AddBankDetails.ascx:Validation()");
//        object[] objects = new object[1];
//        objects[0] = result;
//        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//        exBase.AdditionalInformation = FunctionInfo;
//        ExceptionManager.Publish(exBase);
//        throw exBase;
//    }
//    return result;
//}
//  }
//}
#endregion