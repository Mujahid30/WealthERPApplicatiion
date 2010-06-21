using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoCustomerPortfolio;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoUser;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class MFManualMultipleTran : System.Web.UI.UserControl
    {
        MFTransactionVo mfTransactionVo = new MFTransactionVo();
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            txtSearchName.Text = "";
            txtDividentRate.Text = "";
            txtNAV.Text = "";
            txtPrice.Text = "";
            txtAmount.Text = ""; 
            txtSTT.Text = "";
            txtUnits.Text = "";
         

            if (!Page.IsPostBack)
            {

                dt = new DataTable();
                dt = createTable();
                Session["MFTable"] = dt;
                this.gvMFTransactions.DataSource = ((DataTable)Session["MFTable"]);
                this.gvMFTransactions.DataBind();
                divTransaction.Visible = false;
            }


            
        }

        protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransactionType.SelectedItem.Value == "Sell")
            {
                txtUnits.Enabled = true;
                txtSTT.Enabled = true;
                txtDividentRate.Enabled = false;
            }
            if (ddlTransactionType.SelectedItem.Value == "Buy")
            {
                txtUnits.Enabled = true;
                txtSTT.Enabled = false;
                txtDividentRate.Enabled = false;
            }
            if (ddlTransactionType.SelectedItem.Value == "Divident Reinvestment")
            {
                txtUnits.Enabled = true;
                txtSTT.Enabled = false;
                txtDividentRate.Enabled = true;
            }
            if (ddlTransactionType.SelectedItem.Value == "Divident Payout")
            {
                txtUnits.Enabled = false;
                txtSTT.Enabled = false;
                txtDividentRate.Enabled = true;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session["CustomerVo"];
            //mfTransactionVo.TransactionId = customerBo.GenerateId();
            mfTransactionVo.CustomerId = customerVo.CustomerId;
            //mfTransactionVo.AccountId = "acc1";
            mfTransactionVo.MFCode = int.Parse(txtSearchName.Text.ToString() );
            //mfTransactionVo.FinancialFlag = "F";
            mfTransactionVo.TransactionDate = DateTime.Parse(ddlTransactionDateDay.SelectedItem.Value + "/" + ddlTransactionDateMonth.SelectedItem.Value + "/" + ddlTransactionDateYear.SelectedItem.Value);
            mfTransactionVo.Source = "MP";
            mfTransactionVo.IsSourceManual = 1;
            mfTransactionVo.NAV = float.Parse(txtNAV.Text.ToString());
            mfTransactionVo.Price = float.Parse(txtPrice.Text.ToString());
            mfTransactionVo.Amount = float.Parse(txtAmount.Text.ToString());

            if (ddlTransactionType.SelectedItem.Value == "Buy")
            {
                mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                mfTransactionVo.TransactionClassificationCode = "BUY";
                //mfTransactionVo.SwitchSourceTrxId = "";
                mfTransactionVo.BuySell = "B";

            }

            if (ddlTransactionType.SelectedItem.Value == "Sell")
            {
                mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                mfTransactionVo.STT = float.Parse(txtSTT.Text.ToString());
                mfTransactionVo.TransactionClassificationCode = "SEL";
                //mfTransactionVo.SwitchSourceTrxId = "";
                mfTransactionVo.BuySell = "S";
            }
             
            if (ddlTransactionType.SelectedItem.Value == "Divident Reinvestment")
            {
                mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                mfTransactionVo.Units = float.Parse(txtUnits.Text.ToString());
                mfTransactionVo.TransactionClassificationCode = "DVR";
                //mfTransactionVo.SwitchSourceTrxId = "";
                mfTransactionVo.BuySell = "B";
            }

            if (ddlTransactionType.SelectedItem.Value == "Divident Payout")
            {
                mfTransactionVo.DividendRate = float.Parse(txtDividentRate.Text.ToString());
                mfTransactionVo.TransactionType = "Divident Payout";
                mfTransactionVo.TransactionClassificationCode = "DVP";
                //mfTransactionVo.SwitchSourceTrxId = "";
                mfTransactionVo.BuySell = "S";

            }
            customerTransactionBo.AddMFTransaction(mfTransactionVo, customerVo.UserId);

            DataTable tempDt = (DataTable)Session["MFTable"];

            dr = tempDt.NewRow();
            dr["Scheme Name"] = txtSearchName.Text.ToString();
            dr["Transaction Type"] = ddlTransactionType.SelectedItem.Value.ToString();
            dr["Divident Rate"] = txtDividentRate.Text.ToString();
            dr["Transaction Date"] = ddlTransactionDateDay.SelectedItem.Value + "/" + ddlTransactionDateMonth.SelectedItem.Value + "/" + ddlTransactionDateYear.SelectedItem.Value;
            dr["NAV"]=txtNAV.Text.ToString();
            dr["Price"]=txtPrice.Text.ToString();
            dr["Amount"]=txtAmount.Text.ToString();
            dr["Units"]=txtUnits.Text.ToString();
            dr["STT"] = txtSTT.Text.ToString();
            tempDt.Rows.Add(dr);

            gvMFTransactions.DataSource = tempDt;
            gvMFTransactions.DataBind();
            gvMFTransactions.Visible = true;

            divTransaction.Visible = false;
        }

        protected void btnAddTransaction_Click(object sender, EventArgs e)
        {
            divTransaction.Visible = true;
        }

        public DataTable createTable()
        {
            DataTable table = new DataTable();
            DataColumn dc;
            dc = new DataColumn();
            dc.ColumnName = "Scheme Name";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Transaction Type";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Divident Rate";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Transaction Date";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "NAV";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Price";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Amount";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Units";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "STT";
            table.Columns.Add(dc);

            return table;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','login');", true);

        }

    }
}