using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using VoUser;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewMutualFundPortfolioTransactions : System.Web.UI.UserControl
    {
        List<MFPortfolioTransactionVo> mfPortfolioTransactionVoList;
        MFPortfolioTransactionVo mfPortfolioTransactionVo;
        MFPortfolioVo mfPortfolioVo;
        CustomerVo customerVo;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        static int portfolioId;
        private double unrealizedPL = 0;
        private double realizedPL = 0;
        private double totalPL = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["customerVo"];
                mfPortfolioVo = (MFPortfolioVo)Session["MFPortfolioVo"];
                mfPortfolioTransactionVoList = (List<MFPortfolioTransactionVo>)Session["MFPortfolioTransactionList"];
               
                
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                }
               
                LoadEquityPortfolioTransactions();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewMutualFundPortfolioTransactions.cs:Page_Load()");
                object[] objects = new object[3];
                objects[0] = mfPortfolioTransactionVoList;
                objects[1] = mfPortfolioVo;
                objects[2] = customerVo;
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
        public void LoadEquityPortfolioTransactions()
        {
            try
            {
                if (mfPortfolioTransactionVoList == null)
                {


                }
                else
                {
                    lblCustomer.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                    lblScrip.Text = mfPortfolioVo.SchemePlan.ToString();
                    lblAccount.Text = mfPortfolioVo.Folio.ToString();
                    realizedPL = 0;
                    unrealizedPL = 0;
                    totalPL = 0;
                    DataTable dtMFPortfolioTransaction = new DataTable();
                    
                    dtMFPortfolioTransaction.Columns.Add("Transaction Type");
                    dtMFPortfolioTransaction.Columns.Add("Buy Date");                    
                    dtMFPortfolioTransaction.Columns.Add("Buy Quantity");
                    dtMFPortfolioTransaction.Columns.Add("Buy Price");
                    dtMFPortfolioTransaction.Columns.Add("Cost Of Acquisition");
                    dtMFPortfolioTransaction.Columns.Add("Sell Date");                    
                    dtMFPortfolioTransaction.Columns.Add("Sell Quantity");
                    dtMFPortfolioTransaction.Columns.Add("Sell Price");                    
                    dtMFPortfolioTransaction.Columns.Add("Realized Sales Value");
                    dtMFPortfolioTransaction.Columns.Add("Current NAV");
                    dtMFPortfolioTransaction.Columns.Add("Current Value");
                    dtMFPortfolioTransaction.Columns.Add("AgeOfInvestment");
                    dtMFPortfolioTransaction.Columns.Add("ActualPL");
                    dtMFPortfolioTransaction.Columns.Add("NotionalPL");
                    dtMFPortfolioTransaction.Columns.Add("TotalPL");
                    dtMFPortfolioTransaction.Columns.Add("AbsReturn");
                    dtMFPortfolioTransaction.Columns.Add("AnnReturn");
                    dtMFPortfolioTransaction.Columns.Add("STT");
                    dtMFPortfolioTransaction.Columns.Add("NetSalesProceed");
                    dtMFPortfolioTransaction.Columns.Add("STCG");
                    dtMFPortfolioTransaction.Columns.Add("LTCG"); 

                    DataRow drMFPortfolioTransaction;
                    for (int i = 0; i < mfPortfolioTransactionVoList.Count; i++)
                    {
                        
                        drMFPortfolioTransaction = dtMFPortfolioTransaction.NewRow();
                        mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
                        mfPortfolioTransactionVo = mfPortfolioTransactionVoList[i];
                        unrealizedPL = unrealizedPL + mfPortfolioTransactionVo.NotionalProfitLoss;
                        realizedPL = realizedPL + mfPortfolioTransactionVo.RealizedProfitLoss;
                        totalPL = totalPL + mfPortfolioTransactionVo.TotalProfitLoss;
                        drMFPortfolioTransaction[0] = mfPortfolioTransactionVo.TransactionType.ToString();

                        if (mfPortfolioTransactionVo.BuyDate != DateTime.MinValue)
                            drMFPortfolioTransaction[1] = mfPortfolioTransactionVo.BuyDate.ToShortDateString();
                        else
                            drMFPortfolioTransaction[1] = "";
                        drMFPortfolioTransaction[2] = mfPortfolioTransactionVo.BuyQuantity.ToString("f4");                    
                        drMFPortfolioTransaction[3] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.BuyPrice.ToString("f4")));
                        drMFPortfolioTransaction[4] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.CostOfAcquisition.ToString("f4")));
                        
                        if (mfPortfolioTransactionVo.SellDate != DateTime.MinValue)
                            drMFPortfolioTransaction[5] = mfPortfolioTransactionVo.SellDate.ToShortDateString();
                        else
                            drMFPortfolioTransaction[5] = "-";
                        drMFPortfolioTransaction[6] = mfPortfolioTransactionVo.SellQuantity.ToString("f4");
                        drMFPortfolioTransaction[7] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.SellPrice.ToString("f4")));
                        drMFPortfolioTransaction[8] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.RealizedSalesValue.ToString("f4")));

                        drMFPortfolioTransaction[9] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.CurrentNAV.ToString("f4")));
                        drMFPortfolioTransaction[10] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.CurrentValue.ToString("f4")));
                        drMFPortfolioTransaction[11] = String.Format("{0:n0}", decimal.Parse(mfPortfolioTransactionVo.AgeOfInvestment.ToString("f0")));
                        drMFPortfolioTransaction[12] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.RealizedProfitLoss.ToString("f4")));
                        drMFPortfolioTransaction[13] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.NotionalProfitLoss.ToString("f4")));
                        drMFPortfolioTransaction[14] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.TotalProfitLoss.ToString("f4")));
                        drMFPortfolioTransaction[15] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.AbsoluteReturns.ToString("f4")));
                        drMFPortfolioTransaction[16] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.AnnualReturns.ToString("f4")));
                        drMFPortfolioTransaction[17] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.STT.ToString("f4")));
                        drMFPortfolioTransaction[18] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.NetSalesProceed.ToString("f4")));
                        drMFPortfolioTransaction[19] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.STCGTax.ToString("f4")));
                        drMFPortfolioTransaction[20] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.LTCGTax.ToString("f4")));
                        dtMFPortfolioTransaction.Rows.Add(drMFPortfolioTransaction);

                    }
                    gvMFPortfolio.DataSource = dtMFPortfolioTransaction;
                    gvMFPortfolio.DataBind();
                    gvMFPortfolio.Visible = true;

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
                FunctionInfo.Add("Method", "ViewMutualFundPortfolioTransactions.ascx:LoadEquityPortfolioTransactions()");
                object[] objects = new object[1];
                objects[0] = mfPortfolioTransactionVoList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvMFPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvMFPortfolio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMFPortfolio.PageIndex = e.NewPageIndex;
            gvMFPortfolio.DataBind();
        }

        protected void gvMFPortfolio_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                sortGridViewMFPortfolio(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                sortGridViewMFPortfolio(sortExpression, ASCENDING);
            }
        }
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        private void sortGridViewMFPortfolio(string sortExpression, string direction)
        {

            DataTable dtMFPortfolioTransaction = new DataTable();

            dtMFPortfolioTransaction.Columns.Add("Date");
            dtMFPortfolioTransaction.Columns.Add("Transaction Type");

            dtMFPortfolioTransaction.Columns.Add("Buy/Sell");
            dtMFPortfolioTransaction.Columns.Add("Buy Quantity");
            dtMFPortfolioTransaction.Columns.Add("Buy Price");
            dtMFPortfolioTransaction.Columns.Add("Sell Quantity");
            dtMFPortfolioTransaction.Columns.Add("Sell Price");
            dtMFPortfolioTransaction.Columns.Add("Cost Of Acquisition");
            dtMFPortfolioTransaction.Columns.Add("Realized Sales Value");
            dtMFPortfolioTransaction.Columns.Add("Cost Of Sales");
            dtMFPortfolioTransaction.Columns.Add("Net Cost");
            dtMFPortfolioTransaction.Columns.Add("Net Holdings");
            dtMFPortfolioTransaction.Columns.Add("Average Price");
            dtMFPortfolioTransaction.Columns.Add("Profit/Loss");



            DataRow drMFPortfolioTransaction;
            for (int i = 0; i < mfPortfolioTransactionVoList.Count; i++)
            {
                drMFPortfolioTransaction = dtMFPortfolioTransaction.NewRow();
                mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
                mfPortfolioTransactionVo = mfPortfolioTransactionVoList[i];

                drMFPortfolioTransaction[0] = mfPortfolioTransactionVo.TransactionDate.ToShortDateString();
                drMFPortfolioTransaction[1] = mfPortfolioTransactionVo.TransactionType.ToString();

                drMFPortfolioTransaction[2] = mfPortfolioTransactionVo.BuySell.ToString();
                drMFPortfolioTransaction[3] = mfPortfolioTransactionVo.BuyQuantity.ToString("f4");
                drMFPortfolioTransaction[4] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.BuyPrice.ToString("f4")));
                drMFPortfolioTransaction[5] = mfPortfolioTransactionVo.SellQuantity.ToString("f4");
                drMFPortfolioTransaction[6] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.SellPrice.ToString("f4")));
                drMFPortfolioTransaction[7] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.CostOfAcquisition.ToString("f4")));
                drMFPortfolioTransaction[8] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.RealizedSalesValue.ToString("f4")));
                drMFPortfolioTransaction[9] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.CostOfSales.ToString("f4")));
                drMFPortfolioTransaction[10] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.NetCost.ToString("f4")));
                drMFPortfolioTransaction[11] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.NetHoldings.ToString("f4")));
                drMFPortfolioTransaction[12] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.AveragePrice.ToString("f4")));
                drMFPortfolioTransaction[13] = String.Format("{0:n4}", decimal.Parse(mfPortfolioTransactionVo.RealizedProfitLoss.ToString("f4")));

                dtMFPortfolioTransaction.Rows.Add(drMFPortfolioTransaction);

            }

            DataView dv = new DataView(dtMFPortfolioTransaction);
            dv.Sort = sortExpression + direction;
            gvMFPortfolio.DataSource = dv;
            gvMFPortfolio.DataBind();
            gvMFPortfolio.Visible = true;
        }

        protected void gvMFPortfolio_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (mfPortfolioTransactionVoList != null)
                {
                    gvMFPortfolio.FooterRow.Cells[0].Text = "Total Records : " + mfPortfolioTransactionVoList.Count.ToString();
                    //gvMFPortfolio.FooterRow.Cells[0].ColumnSpan = 3;
                    //for (int i = 1; i < gvMFPortfolio.FooterRow.Cells.Count; i++)
                    //{
                    //    gvMFPortfolio.FooterRow.Cells[i].Visible = false;
                    //}
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
                FunctionInfo.Add("Method", "ViewMutualFundPortfolioTransactions.ascx:gvMFPortfolio_DataBound()");
                object[] objects = new object[1];
                objects[0] = mfPortfolioTransactionVoList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void gvMFPortfolio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[12].Text = double.Parse(realizedPL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[12].Attributes.Add("align", "Right");

                e.Row.Cells[13].Text = double.Parse((Math.Round(unrealizedPL,5)).ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[13].Attributes.Add("align", "Right");

                e.Row.Cells[14].Text = double.Parse(totalPL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[14].Attributes.Add("align", "Right");

            }
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewMutualFundPortfolio','none');", true);
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
        }
       
      
    }
}