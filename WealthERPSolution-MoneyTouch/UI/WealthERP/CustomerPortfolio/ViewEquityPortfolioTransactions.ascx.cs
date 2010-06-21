using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using VoCustomerPortfolio;
using VoUser;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewEquityTransactions : System.Web.UI.UserControl
    {
        List<EQPortfolioTransactionVo> eqPortfolioTransactionVoList;
        EQPortfolioTransactionVo eqPortfolioTransactionVo;
        EQPortfolioVo eqPortfolioVo;
        CustomerVo customerVo;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                eqPortfolioTransactionVoList = (List<EQPortfolioTransactionVo>)Session["EquityPortfolioTransactionList"];
                eqPortfolioVo = (EQPortfolioVo)Session["EquityPortfolio"];
                customerVo = (CustomerVo)Session["customerVo"];
                LoadEquityPortfolioTransactions();
                lnkBack.Visible = true;
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolioTransactions.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = eqPortfolioTransactionVoList;
                objects[1] = eqPortfolioVo;
                objects[2] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void LoadEquityPortfolioTransactions()
        {
            try
            {

                if (eqPortfolioTransactionVoList==null)
                {
                }
                else
                {
                    lblCustomer.Text = customerVo.FirstName.ToString() + " " + customerVo.LastName.ToString();
                    lblScrip.Text = eqPortfolioVo.EQCompanyName.ToString();
                    lblAccount.Text = eqPortfolioVo.AccountId.ToString();
                    DataTable dtEqPortfolioTransaction = new DataTable();
                    dtEqPortfolioTransaction.Columns.Add("Date");
                    dtEqPortfolioTransaction.Columns.Add("Trade Type");
                    dtEqPortfolioTransaction.Columns.Add("Buy/Sell");
                    dtEqPortfolioTransaction.Columns.Add("Buy Quantity");
                    dtEqPortfolioTransaction.Columns.Add("Buy Price");
                    dtEqPortfolioTransaction.Columns.Add("Sell Quantity");
                    dtEqPortfolioTransaction.Columns.Add("Sell Price");
                    dtEqPortfolioTransaction.Columns.Add("Cost Of Acquisition");
                    dtEqPortfolioTransaction.Columns.Add("Realized Sales Value");
                    dtEqPortfolioTransaction.Columns.Add("Cost Of Sales");
                    dtEqPortfolioTransaction.Columns.Add("Net Cost");
                    dtEqPortfolioTransaction.Columns.Add("Net Holdings");
                    dtEqPortfolioTransaction.Columns.Add("Average Price");
                    dtEqPortfolioTransaction.Columns.Add("Profit/Loss");


                    DataRow drEqPortfolioTransaction;
                    for (int i = 0; i < eqPortfolioTransactionVoList.Count; i++)
                    {
                        drEqPortfolioTransaction = dtEqPortfolioTransaction.NewRow();
                        eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
                        eqPortfolioTransactionVo = eqPortfolioTransactionVoList[i];
                       
                        drEqPortfolioTransaction[0] = eqPortfolioTransactionVo.TradeDate.ToShortDateString();
                        if(eqPortfolioTransactionVo.TradeType.ToString()=="S")
                            drEqPortfolioTransaction[1] = "Speculative";
                        else
                            drEqPortfolioTransaction[1] = "Delivery";

                        drEqPortfolioTransaction[2] = eqPortfolioTransactionVo.TradeSide.ToString();
                        drEqPortfolioTransaction[3] = eqPortfolioTransactionVo.BuyQuantity.ToString("f4");
                        drEqPortfolioTransaction[4] = String.Format("{0:n4}", decimal.Parse(eqPortfolioTransactionVo.BuyPrice.ToString("f4")));
                        drEqPortfolioTransaction[5] = eqPortfolioTransactionVo.SellQuantity.ToString("f0");
                        drEqPortfolioTransaction[6] = String.Format("{0:n4}", decimal.Parse(eqPortfolioTransactionVo.SellPrice.ToString("f4")));
                        drEqPortfolioTransaction[7] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.CostOfAcquisition.ToString("f4")));
                        drEqPortfolioTransaction[8] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.RealizedSalesValue.ToString("f4")));
                        drEqPortfolioTransaction[9] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.CostOfSales.ToString("f4")));
                        drEqPortfolioTransaction[10] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.NetCost.ToString("f4")));
                        drEqPortfolioTransaction[11] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.NetHoldings.ToString("f4")));
                        drEqPortfolioTransaction[12] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.AveragePrice.ToString("f4")));
                        drEqPortfolioTransaction[13] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.RealizedProfitLoss.ToString("f4")));

                        dtEqPortfolioTransaction.Rows.Add(drEqPortfolioTransaction);

                    }
                    gvEquityPortfolio.DataSource = dtEqPortfolioTransaction;
                    gvEquityPortfolio.DataBind();
                    gvEquityPortfolio.Visible = true;

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
                FunctionInfo.Add("Method", "ViewEquityPortfolioTransactions.ascx:LoadEquityPortfolioTransactions()");
                object[] objects = new object[3];
                objects[0] = eqPortfolioTransactionVoList;
                objects[1] = eqPortfolioTransactionVo;
                objects[2] = eqPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvEquityPortfolio_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;         
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                sortGridViewBranches(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                sortGridViewBranches(sortExpression, ASCENDING);
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

        private void sortGridViewBranches(string sortExpression, string direction)
        {
            try
            {
                eqPortfolioTransactionVoList = (List<EQPortfolioTransactionVo>)Session["EquityPortfolioTransactionList"];
                DataTable dtEqPortfolioTransaction = new DataTable();

                dtEqPortfolioTransaction.Columns.Add("Date");
                dtEqPortfolioTransaction.Columns.Add("Trade Type");
                dtEqPortfolioTransaction.Columns.Add("Buy/Sell");
                dtEqPortfolioTransaction.Columns.Add("Buy Quantity");
                dtEqPortfolioTransaction.Columns.Add("Buy Price");
                dtEqPortfolioTransaction.Columns.Add("Sell Quantity");
                dtEqPortfolioTransaction.Columns.Add("Sell Price");
                dtEqPortfolioTransaction.Columns.Add("Cost Of Acquisition");
                dtEqPortfolioTransaction.Columns.Add("Realized Sales Value");
                dtEqPortfolioTransaction.Columns.Add("Cost Of Sales");
                dtEqPortfolioTransaction.Columns.Add("Net Cost");
                dtEqPortfolioTransaction.Columns.Add("Net Holdings");
                dtEqPortfolioTransaction.Columns.Add("Average Price");
                dtEqPortfolioTransaction.Columns.Add("Profit/Loss");



                DataRow drEqPortfolioTransaction;
                for (int i = 0; i < eqPortfolioTransactionVoList.Count; i++)
                {
                    drEqPortfolioTransaction = dtEqPortfolioTransaction.NewRow();
                    eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
                    eqPortfolioTransactionVo = eqPortfolioTransactionVoList[i];

                    drEqPortfolioTransaction[0] = eqPortfolioTransactionVo.TradeDate.ToShortDateString();
                    if (eqPortfolioTransactionVo.TradeType.ToString() == "S")
                        drEqPortfolioTransaction[1] = "Speculative";
                    else
                        drEqPortfolioTransaction[1] = "Delivery";

                    drEqPortfolioTransaction[2] = eqPortfolioTransactionVo.TradeSide.ToString();
                    drEqPortfolioTransaction[3] = eqPortfolioTransactionVo.BuyQuantity.ToString("f4");
                    drEqPortfolioTransaction[4] = String.Format("{0:n4}", decimal.Parse(eqPortfolioTransactionVo.BuyPrice.ToString("f4")));
                    drEqPortfolioTransaction[5] = eqPortfolioTransactionVo.SellQuantity.ToString("f0");
                    drEqPortfolioTransaction[6] = String.Format("{0:n4}", decimal.Parse(eqPortfolioTransactionVo.SellPrice.ToString("f4")));
                    drEqPortfolioTransaction[7] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.CostOfAcquisition.ToString("f4")));
                    drEqPortfolioTransaction[8] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.RealizedSalesValue.ToString("f4")));
                    drEqPortfolioTransaction[9] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.CostOfSales.ToString("f4")));
                    drEqPortfolioTransaction[10] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.NetCost.ToString("f4")));
                    drEqPortfolioTransaction[11] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.NetHoldings.ToString("f4")));
                    drEqPortfolioTransaction[12] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.AveragePrice.ToString("f4")));
                    drEqPortfolioTransaction[13] = String.Format("{0:n2}", decimal.Parse(eqPortfolioTransactionVo.RealizedProfitLoss.ToString("f4")));

                    dtEqPortfolioTransaction.Rows.Add(drEqPortfolioTransaction);

                }

                DataView dv = new DataView(dtEqPortfolioTransaction);
                dv.Sort = sortExpression + direction;
                gvEquityPortfolio.DataSource = dv;
                gvEquityPortfolio.DataBind();
                gvEquityPortfolio.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolioTransactions.ascx:sortGridViewBranches()");
                object[] objects = new object[3];
                objects[0] = eqPortfolioTransactionVoList;
                objects[1] = eqPortfolioTransactionVo;
                objects[2] = eqPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvEquityPortfolio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquityPortfolio.PageIndex = e.NewPageIndex;
            gvEquityPortfolio.DataBind();

        }

        protected void gvEquityPortfolio_DataBound(object sender, EventArgs e)
        {

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ViewEquityPortfolios','none');", true);
        }

        

    }
}
