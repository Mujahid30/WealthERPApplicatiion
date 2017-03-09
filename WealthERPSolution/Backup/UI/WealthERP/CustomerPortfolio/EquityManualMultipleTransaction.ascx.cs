using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class EquityManualMultipleTransaction : System.Web.UI.UserControl
    {
        EQTransactionVo eqTransactionVo = null;
        CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
        CustomerVo customerVo;
        DataSet ds=new DataSet();
        DataTable dt = new DataTable();
        static DataTable tempDt;
        DataRow dr;
        float totalProrate;
        static  float totalBrokerage;
       

        
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Session["flag"].ToString() == "2")
            {
                totalProrate = (float)Session["total"];
            }
            if (!Page.IsPostBack)
            {
                
                dt = new DataTable();
                dt = createTable();
                Session["Table"] = dt;
                this.GridView1.DataSource = ((DataTable)Session["Table"]);
                this.GridView1.DataBind();
            
           

            }
            divEquityManualMultiple.Visible = false;
            ClearTextbox();
           
        }
        public void ClearTextbox()
        {
            txtBroker.Text = "";
            txtBrokerage.Text = "";
            txtNoOfShares.Text = "";
            txtRate.Text = "";
            txtScrip.Text = "";
            txtScripParticular.Text = "";
            txtTicker.Text = "";
            txtTradeDate.Text = "";
            
        }
        public DataTable createTable()
        {
            DataTable table = new DataTable();
            DataColumn dc ;
            dc= new DataColumn();
            dc.ColumnName = "Transaction Mode";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Transaction Type";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Exchange Type";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Scrip";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Ticker";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Trade Date";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "No Of Shares";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Rate";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Broker";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Brokerage";
            table.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Other Charges";
            table.Columns.Add(dc);

            return table;
        }

       
        protected void btnAdd_Click(object sender, EventArgs e)
        {
                  
            divEquityManualMultiple.Visible = true;
            System.DateTime dt = System.DateTime.Now;     
            txtTradeDate.Text = dt.Date.ToString();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
          //  DataTable tempDt = (DataTable)Session["Table"];
            tempDt = (DataTable)Session["Table"]; 

            dr = tempDt.NewRow();          
            dr["Transaction Mode"] = ddlTransactionMode.SelectedItem.Value.ToString();
            dr["Transaction Type"] = ddlTransactionType.SelectedItem.Value.ToString();
            dr["Exchange Type"] = ddlExchange.SelectedItem.Value.ToString();
            dr["Scrip"] = txtScripParticular.Text;
            dr["Ticker"] = txtTicker.Text;
            dr["Trade Date"] = txtTradeDate.Text;
            dr["No Of Shares"] = txtNoOfShares.Text;
            dr["Rate"] = txtRate.Text;
            dr["Broker"] = txtBroker.Text;
            dr["Brokerage"] = txtBrokerage.Text;
            dr["Other Charges"] = " ";
           
            tempDt.Rows.Add(dr);

            GridView1.DataSource = (DataTable)Session["Table"];
       
            GridView1.DataBind();
            GridView1.Visible = true;
            
           
          
           
        }

        protected void btnAddProrate_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopupScript", "showProrate();", true);
           
        }

        protected void btnSubmit_1_Click(object sender, EventArgs e)
        {

            customerVo = (CustomerVo)Session["CustomerVo"];          
            
            decimal temp;
            float res;
            float serviceTax = float.Parse(Session["serviceTax"].ToString());
            float stt = float.Parse(Session["STT"].ToString());
            ds.Tables.Add(tempDt);
          

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                float brokerage = float.Parse(ds.Tables[0].Rows[i ]["Brokerage"].ToString());
                res = ((totalProrate) * (brokerage)) / totalBrokerage;
            
                ds.Tables[0].Rows[i]["Other Charges"] = res.ToString();

            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                eqTransactionVo = new EQTransactionVo();
                eqTransactionVo.AccountId = 1001;
              //  eqTransactionVo.Brokerage = decimal.Round((decimal)(ds.Tables[0].Rows[i]["Brokerage"].ToString()), 4);
                
                eqTransactionVo.Brokerage = float.Parse(ds.Tables[0].Rows[i]["Brokerage"].ToString());
                eqTransactionVo.Brokerage = (float)decimal.Round(Convert.ToDecimal(eqTransactionVo.Brokerage), 4);
                eqTransactionVo.BrokerCode = ds.Tables[0].Rows[i]["Broker"].ToString();
                if (ds.Tables[0].Rows[i]["Transaction Type"].ToString() == "Buy")
                {
                    eqTransactionVo.BuySell = "B";
                }
                else
                {
                    eqTransactionVo.BuySell = "S";
                }
                eqTransactionVo.CustomerId = int.Parse(customerVo.CustomerId.ToString());
                if (ds.Tables[0].Rows[i]["Transaction Mode"].ToString() == "Delivery Trade")
                {
                    eqTransactionVo.TradeType = "D";
                }
                else
                {
                    eqTransactionVo.TradeType = "S";
                }
                eqTransactionVo.ServiceTax = serviceTax;

                eqTransactionVo.EducationCess =(float) ((3 / 100) * serviceTax);
                eqTransactionVo.EducationCess = (float)decimal.Round(Convert.ToDecimal(eqTransactionVo.EducationCess), 4);
                eqTransactionVo.ScripCode =100000;
                eqTransactionVo.Exchange = ds.Tables[0].Rows[i]["Exchange Type"].ToString();
                eqTransactionVo.OtherCharges = float.Parse(ds.Tables[0].Rows[i]["Other Charges"].ToString());
                eqTransactionVo.OtherCharges = (float)decimal.Round(Convert.ToDecimal(eqTransactionVo.OtherCharges), 4);
                eqTransactionVo.Quantity =float.Parse(ds.Tables[0].Rows[i]["No Of Shares"].ToString());
                eqTransactionVo.Rate = float.Parse(ds.Tables[0].Rows[i]["Rate"].ToString());
                eqTransactionVo.Rate = (float)decimal.Round(Convert.ToDecimal(eqTransactionVo.Rate), 4);
                eqTransactionVo.STT = stt;
                if (eqTransactionVo.BuySell == "B")
                {
                    eqTransactionVo.RateInclBrokerage = eqTransactionVo.Rate + eqTransactionVo.Brokerage + eqTransactionVo.ServiceTax + eqTransactionVo.EducationCess + eqTransactionVo.STT + eqTransactionVo.OtherCharges;
                    eqTransactionVo.RateInclBrokerage = (float)decimal.Round(Convert.ToDecimal(eqTransactionVo.RateInclBrokerage), 4);
                    eqTransactionVo.TransactionCode = 1;
                }
                else
                {
                    eqTransactionVo.RateInclBrokerage = eqTransactionVo.Rate - eqTransactionVo.Brokerage - (eqTransactionVo.ServiceTax + eqTransactionVo.EducationCess) - eqTransactionVo.STT - eqTransactionVo.OtherCharges;
                    eqTransactionVo.RateInclBrokerage = (float)decimal.Round(Convert.ToDecimal(eqTransactionVo.RateInclBrokerage), 4);
                    eqTransactionVo.TransactionCode = 2;

                }
                //eqTransactionVo.Source = "M";
                //eqTransactionVo.SourceDetail = "WERP";
                eqTransactionVo.TradeDate = DateTime.Parse(ds.Tables[0].Rows[i]["Trade Date"].ToString());
                eqTransactionVo.TradeTotal = eqTransactionVo.RateInclBrokerage * eqTransactionVo.Quantity;
                eqTransactionVo.TradeTotal = (float)decimal.Round(Convert.ToDecimal(eqTransactionVo.TradeTotal), 4);
                eqTransactionVo.IsCorpAction = 0;
                
               // eqTransactionVo.TransactionId = customerTransactionBo.getId();
                customerTransactionBo.AddEquityTransaction(eqTransactionVo, customerVo.UserId);
            }
            //foreach (GridViewRow gvr in GridView1.Rows)
            //{
            //    //TextBox txtBrokerage1 = (TextBox)gvr.FindControl("Brokerage");
            //    //float brokerage = float.Parse(txtBrokerage1.Text.ToString());
            //    // float brokerage = float.Parse(gvr.FindControl("Brokerage").ToString());
            //    string txt = ((TextBox)gvr.FindControl("Brokerage")).Text.ToString();
            //    float brokerage = float.Parse(txt);
            //    res = ((totalProrate) * (brokerage)) / totalBrokerage;
            //    TextBox other = (TextBox)gvr.FindControl("Other Charges");
            //    other.Text = res.ToString();
            //}
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            GridView1.Visible = true;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
       
            DataRowView tableData=e.Row.DataItem as DataRowView;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                totalBrokerage = 0;

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //totalBrokerage += (float)tableData["Brokerage"];
                totalBrokerage += float.Parse(tableData["Brokerage"].ToString());
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtTotalBrokerage = (TextBox)e.Row.FindControl("txtTotalBrokerage") as TextBox;
                txtTotalBrokerage.Text = totalBrokerage.ToString();
            }

        }
    }
}