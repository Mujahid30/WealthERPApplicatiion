using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using VoCustomerPortfolio;
using VoUser;
using BoCustomerPortfolio;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Globalization;
using System.Collections;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BoCommon;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar.Utils;
using BoReports;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewEquityPortfolios : System.Web.UI.UserControl
    {
        int index;
        EQPortfolioVo eqPortfolioVo;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        EQNetPositionBo eQNetPositionBo = new EQNetPositionBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<EQPortfolioVo> eqPortfolioList = new List<EQPortfolioVo>();
        CustomerVo customerVo;
        UserVo userVo;
        CustomerPortfolioVo customerPortfolioVo;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        AdvisorVo advisorVo = new AdvisorVo();

        DateTime tradeDate = new DateTime();

        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();

        static int portfolioId;
        RMVo rmVo = new RMVo();




        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["customerVo"];
                rmVo = (RMVo)Session["rmVo"];
                userVo = (UserVo)Session["userVo"];
                advisorVo = (AdvisorVo)Session["advisorVo"];
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                GetLatestDate();

                genDict = (Dictionary<string, DateTime>)Session[SessionContents.MaxPriceDate];
                string strValuationDate = genDict[Constants.EQDate.ToString()].ToString();

                radEqTranxDetails.VisibleOnPageLoad = false;
                radEqSellPair.VisibleOnPageLoad = false;
                radEqAvgPriceDetails.VisibleOnPageLoad = false;


                if (!IsPostBack)
                {

                    //NewBindReturnsGrid();
                    //BindReturnsRealizedGrid();
                    //BindReturnsAllGrid();

                    BindPortfolioDropDown();

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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerPortfolioVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public enum Constants
        {
            EQ = 0,     // explicitly specifying the enum constant values will improve performance
            MF = 1,
            EQDate = 2,
            MFDate = 3
        };
        private void GetLatestDate()
        {
            DateTime EQLatestDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            try
            {
                portfolioBo = new PortfolioBo();
                EQLatestDate = DateTime.Parse(portfolioBo.GetLatestDate().ToString());
                genDict.Add("EQDate", EQLatestDate);
                Session["MaxPriceDate"] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestDate()");
                object[] objects = new object[3];

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
            ddlPortfolio.SelectedValue = portfolioId.ToString();
        }


        public void LoadEquityPortfolio()
        {
            int count = 0;
            DataSet dsCustomeNP = new DataSet();
            DataTable dtUnRealized = new DataTable();
            DataTable dtRealizedDelivery = new DataTable();
            DataTable dtRealizedSpeculative = new DataTable();
            DataTable dtAll = new DataTable();

            try
            {
                tradeDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());

                RadAjaxPanel4.Visible = false;
                RadAjaxPanel1.Visible = true;


                dsCustomeNP = customerPortfolioBo.GetCustomerEquityNP(advisorVo.advisorId, int.Parse(ddlPortfolio.SelectedValue.ToString()), tradeDate, ddl_type.SelectedItem.Value.ToString());



                dtUnRealized = dsCustomeNP.Tables[1];
                dtRealizedDelivery = dsCustomeNP.Tables[0];
                dtAll = dsCustomeNP.Tables[2];
                dtRealizedSpeculative = dsCustomeNP.Tables[3];

                if (dtUnRealized.Rows.Count > 0)
                {
                    gvEquityPortfolio.DataSource = dtUnRealized;
                    gvEquityPortfolio.DataBind();
                    if (Cache["gvEquityPortfolio" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvEquityPortfolio" + customerVo.CustomerId.ToString(), dtUnRealized);
                    }
                    else
                    {
                        Cache.Remove("gvEquityPortfolio" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvEquityPortfolio" + customerVo.CustomerId.ToString(), dtUnRealized);
                    }
                }
                if (dtUnRealized.Rows.Count == 0)
                {
                    gvEquityPortfolio.DataBind();
                }

                if (dtRealizedDelivery.Rows.Count > 0)
                {

                    gvEquityPortfolioRealizedDelivery.DataSource = dtRealizedDelivery;
                    gvEquityPortfolioRealizedDelivery.DataBind();
                    if (Cache["gvEquityPortfolioRealizedDeliveryeDetails" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvEquityPortfolioRealizedDeliveryeDetails" + customerVo.CustomerId.ToString(), dtRealizedDelivery);
                    }
                    else
                    {
                        Cache.Remove("gvEquityPortfolioRealizedDeliveryeDetails" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvEquityPortfolioRealizedDeliveryeDetails" + customerVo.CustomerId.ToString(), dtRealizedDelivery);
                    }

                }
                if (dtRealizedDelivery.Rows.Count == 0)
                {
                    gvEquityPortfolioRealizedDelivery.DataBind();
                }


                if (dtAll.Rows.Count > 0)
                {

                    gvrEQAll.DataSource = dtAll;
                    gvrEQAll.DataBind();
                    if (Cache["gvrEQAll" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvrEQAll" + customerVo.CustomerId.ToString(), dtAll);
                    }
                    else
                    {
                        Cache.Remove("gvrEQAll" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvrEQAll" + customerVo.CustomerId.ToString(), dtAll);
                    }
                }
                if (dtAll.Rows.Count == 0)
                {
                    gvrEQAll.DataBind();
                }
                if (dtRealizedSpeculative.Rows.Count > 0)
                {

                    gvEquityPortfolioRealizedSpeculative.DataSource = dtRealizedSpeculative;
                    gvEquityPortfolioRealizedSpeculative.DataBind();
                    if (Cache["gvEquityPortfolioRealizedSpeculative" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvEquityPortfolioRealizedSpeculative" + customerVo.CustomerId.ToString(), dtRealizedSpeculative);
                    }
                    else
                    {
                        Cache.Remove("gvEquityPortfolioRealizedSpeculative" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvEquityPortfolioRealizedSpeculative" + customerVo.CustomerId.ToString(), dtRealizedSpeculative);
                    }
                }
                if (dtRealizedSpeculative.Rows.Count == 0)
                {
                    gvEquityPortfolioRealizedSpeculative.DataBind();
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
                FunctionInfo.Add("Method", "CustomerEquityNP.aspx:LoadEquityPortfolio()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = eqPortfolioVo;
                objects[2] = count;
                objects[3] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void NewBindReturnsGrid()
        {
            DataTable dtUnRealized = new DataTable();
            DataSet dsHoldingReturnDetails = new DataSet();
            tradeDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());
            dtUnRealized = eQNetPositionBo.CreateCustomerEQReturnsHolding(customerVo.CustomerId, ddlPortfolio.SelectedValue.ToString(), null, tradeDate).Tables[0];
            if (dtUnRealized.Rows.Count > 0)
            {
                RadTabStrip1.Visible = true;
                RadAjaxPanel1.Visible = true;
                gvEquityPortfolio.DataSource = dtUnRealized;
                gvEquityPortfolio.DataBind();
             
            }
            if (dtUnRealized.Rows.Count == 0)
            {
                RadTabStrip1.Visible = false;
                RadAjaxPanel1.Visible = false;
                gvEquityPortfolio.DataBind();
            }
        }
        private void BindReturnsRealizedGrid()
        {
            DataTable dtRealized = new DataTable();
            DataSet dsHoldingReturnDetails = new DataSet();
            tradeDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());
            dtRealized = eQNetPositionBo.CreateCustomerEQReturnsRealized(customerVo.CustomerId, ddlPortfolio.SelectedValue.ToString(), null, tradeDate).Tables[0];
            if (dtRealized.Rows.Count > 0)
            {
                RadTabStrip1.Visible = true;
                RadAjaxPanel1.Visible = true;
                gvEquityPortfolioRealizedDelivery.DataSource = dtRealized;
                gvEquityPortfolioRealizedDelivery.DataBind();
              
            }
            if (dtRealized.Rows.Count == 0)
            {
                RadTabStrip1.Visible = false;
                RadAjaxPanel1.Visible = false;
                gvEquityPortfolioRealizedDelivery.DataBind();
            }
        }
        private void BindReturnsAllGrid()
        {

            DataTable dtAll = new DataTable();
            DataSet dsHoldingReturnDetails = new DataSet();
            tradeDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());
            dtAll = eQNetPositionBo.CreateCustomerEQReturnsAll(customerVo.CustomerId, ddlPortfolio.SelectedValue.ToString(), null, tradeDate).Tables[0];
            if (dtAll.Rows.Count > 0)
            {
                RadTabStrip1.Visible = true;
                RadAjaxPanel1.Visible = true;
                gvrEQAll.DataSource = dtAll;
                gvrEQAll.DataBind();
               
            }
            if (dtAll.Rows.Count == 0)
            {
                RadTabStrip1.Visible = false;
                RadAjaxPanel1.Visible = false;
                gvrEQAll.DataBind();
            }
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            LoadEquityPortfolio();
            
        }


        protected void gvEquityPortfolio_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Select")
                {
                    GridDataItem gvr = (GridDataItem)e.Item;
                    int ScripCode = Convert.ToInt32(gvr.GetDataKeyValue("ScripCode"));
                    DateTime From = customerPortfolioBo.GetMinimumInvestmentDate(int.Parse(ddlPortfolio.SelectedValue.ToString()), ScripCode);
                    string To = txtPickDate.SelectedDate.ToString();
                    string Currency = ddl_type.SelectedValue.ToString();
                    string PortfolioId = ddlPortfolio.SelectedValue.ToString();
                    string CustomerId = customerVo.CustomerId.ToString();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadcontrol", "loadcontrol('EquityTransactionsView','?ScripCode=" + ScripCode + "&From=" + From.ToString() + "&To=" + To + "&Currency=" + Currency + "&PortfolioId=" + PortfolioId + "&CustomerId=" + CustomerId + "');", true);

                }
                if (e.CommandName == "AvgPriceCalculation")
                {
                    GridDataItem gvr = (GridDataItem)e.Item;
                    int ScripCode = Convert.ToInt32(gvr.GetDataKeyValue("ScripCode"));
                    radEqAvgPriceDetails.VisibleOnPageLoad = true;
                    LoadAvgPriceCalculationDetails(ScripCode);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolio_RowCommand()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void gvEquityPortfolioRealizedDelivery_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Select")
                {
                    GridDataItem gvr = (GridDataItem)e.Item;
                    int ScripCode = Convert.ToInt32(gvr.GetDataKeyValue("ScripCode"));
                    LoadEquityPortfolioSellPairDetails(ScripCode);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioRealizedDelivery_RowCommand()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void gvEquityPortfolioUnrealized_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();

                if (e.CommandName.ToString() != "Filter")
                {
                    if (e.CommandName.ToString() != "Sort")
                    {
                        if (e.CommandName.ToString() != "Page")
                        {
                            if (e.CommandName.ToString() != "ChangePageSize")
                            {
                                GridDataItem gvr = (GridDataItem)e.Item;
                                int selectedRow = gvr.ItemIndex + 1;
                                int slNo = Convert.ToInt32(gvr.GetDataKeyValue("Sl.No."));

                                Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                                Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                                if (e.CommandName == "Select")
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
                                }
                            }
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioUnrealized_RowCommand()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void gvEquityPortfolioRealizedSpeculative_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {


                if (e.CommandName == "Select")
                {
                    GridDataItem gvr = (GridDataItem)e.Item;
                    int ScripCode = Convert.ToInt32(gvr.GetDataKeyValue("ScripCode"));
                    DateTime From = customerPortfolioBo.GetMinimumInvestmentDate(int.Parse(ddlPortfolio.SelectedValue.ToString()), ScripCode);
                    string To = txtPickDate.SelectedDate.ToString();
                    string Currency = ddl_type.SelectedValue.ToString();
                    string PortfolioId = ddlPortfolio.SelectedValue.ToString();
                    string CustomerId = customerVo.CustomerId.ToString();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "loadcontrol", "loadcontrol('EquityTransactionsView','?ScripCode=" + ScripCode + "&From=" + From.ToString() + "&To=" + To + "&Currency=" + Currency + "&PortfolioId=" + PortfolioId + "&CustomerId=" + CustomerId + "');", true);

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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolio_RowCommand()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        protected void gvEquityPortfolio_DataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {


            //DataTable dt = new DataTable();
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    if ((Convert.ToDouble(dataItem["UnRealizedPL"].Text) < 0))
            //        dataItem["UnRealizedPL"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["PnlPercent"].Text) < 0)
            //        dataItem["PnlPercent"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["XIRRValue"].Text) < 0)
            //        dataItem["XIRRValue"].ForeColor = System.Drawing.Color.Red;
            //}

            //if (ddl_type.SelectedValue.ToString() == "INR")
            //{
            //    gvEquityPortfolio.MasterTableView.GetColumn("ForExCurrentpPrice").Display = false;
            //    gvEquityPortfolio.MasterTableView.GetColumn("ForExCurrentPriceDate").Display = false;


            //}
            //else
            //{
            //    gvEquityPortfolio.MasterTableView.GetColumn("ForExCurrentpPrice").Display = true;
            //    gvEquityPortfolio.MasterTableView.GetColumn("ForExCurrentPriceDate").Display = true;
            //}

        }
        protected void gvEquityPortfolioRealizedDelivery_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    if (Convert.ToDouble(dataItem["RealizedPL"].Text) < 0)
            //        dataItem["RealizedPL"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["PnlPercent"].Text) < 0)
            //        dataItem["PnlPercent"].ForeColor = System.Drawing.Color.Red;


            //}

        }
        protected void gvrEQAll_DataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    if (Convert.ToDouble(dataItem["RealizedPL"].Text) < 0)
            //        dataItem["RealizedPL"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["UnRealizedPL"].Text) < 0)
            //        dataItem["UnRealizedPL"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["TotalXIRRValue"].Text) < 0)
            //        dataItem["TotalXIRRValue"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["TotalPL"].Text) < 0)
            //        dataItem["TotalPL"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["Multiple"].Text) < 1)
            //        dataItem["Multiple"].ForeColor = System.Drawing.Color.Red;
            //    if (Convert.ToDouble(dataItem["PnlPercent"].Text) < 0)
            //        dataItem["PnlPercent"].ForeColor = System.Drawing.Color.Red;
            //    if (ddl_type.SelectedValue.ToString() == "INR")
            //    {
            //        gvrEQAll.MasterTableView.GetColumn("ForExCurrentpPrice").Display = false;
            //        gvrEQAll.MasterTableView.GetColumn("ForExCurrentPriceDate").Display = false;


            //    }
            //    else
            //    {
            //        gvrEQAll.MasterTableView.GetColumn("ForExCurrentpPrice").Display = true;
            //        gvrEQAll.MasterTableView.GetColumn("ForExCurrentPriceDate").Display = true;
            //    }



            //}


        }
        protected void gvEquityPortfolioRealizedSpeculative_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void gvEquityPortfolioRealizedDelivery_OnDataBound(object sender, EventArgs e)
        {
            RadGrid grid = sender as RadGrid;
            int gridItems = grid.MasterTableView.Items.Count;
            if (gridItems < 11)
            {
                grid.ClientSettings.Scrolling.ScrollHeight = Unit.Pixel((gridItems * 24) + 24);
            }
            else
            {
                grid.ClientSettings.Scrolling.ScrollHeight = Unit.Pixel(11 * 24);
            }
        }





        protected void gvEquityPortfolio_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEquityPortfolioDetails = new DataTable();
            dtgvEquityPortfolioDetails = (DataTable)Cache["gvEquityPortfolio" + customerVo.CustomerId.ToString()];
            gvEquityPortfolio.DataSource = dtgvEquityPortfolioDetails;
        }
        protected void gvEquityPortfolioRealizedDelivery_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEquityPortfolioRealizedDeliveryDetails = new DataTable();
            dtgvEquityPortfolioRealizedDeliveryDetails = (DataTable)Cache["gvEquityPortfolioRealizedDeliveryeDetails" + customerVo.CustomerId.ToString()];
            gvEquityPortfolioRealizedDelivery.DataSource = dtgvEquityPortfolioRealizedDeliveryDetails;
        }
        protected void gvEquityPortfolioRealizedSpeculative_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            DataTable dtgvEquityPortfolioRealizedSpeculative = new DataTable();
            dtgvEquityPortfolioRealizedSpeculative = (DataTable)Cache["gvEquityPortfolioRealizedSpeculative" + customerVo.CustomerId.ToString()];
            gvEquityPortfolioRealizedSpeculative.DataSource = dtgvEquityPortfolioRealizedSpeculative;

        }
        protected void gvrEQAll_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEquityPortfolioDetails = new DataTable();
            dtgvEquityPortfolioDetails = (DataTable)Cache["gvrEQAll" + customerVo.CustomerId.ToString()];
            gvrEQAll.DataSource = dtgvEquityPortfolioDetails;

        }



        protected void btnGo_Click(object sender, EventArgs e)
        {

            NewBindReturnsGrid();
            BindReturnsRealizedGrid();
            BindReturnsAllGrid();

        }



        public void LoadEquityPortfolioTransactionDetails(int ScripCode, int Is_Specuative)
        {
            int count = 0;
            DataSet dsCustomeNP = new DataSet();
            DataTable dtEqTranx = new DataTable();
            DataTable dtEqAvg = new DataTable();
            try
            {
                tradeDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());


                dsCustomeNP = customerPortfolioBo.GetCustomerEquityNPTransactionDetails(int.Parse(ddlPortfolio.SelectedValue.ToString()), tradeDate, ScripCode, Is_Specuative, ddl_type.SelectedValue.ToString());
                radEqTranxDetails.VisibleOnPageLoad = true;
                dtEqTranx = dsCustomeNP.Tables[0];
                if (dtEqTranx.Rows.Count > 0)
                {

                    gvEqTranxDetails.DataSource = dtEqTranx;
                    ViewState["dtEqTranx"] = dtEqTranx;
                    gvEqTranxDetails.DataBind();

                    if (Cache["gvEqTranxDetails" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvEqTranxDetails" + customerVo.CustomerId.ToString(), dtEqTranx);
                    }
                    else
                    {
                        Cache.Remove("gvEqTranxDetails" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvEqTranxDetails" + customerVo.CustomerId.ToString(), dtEqTranx);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:LoadEquityPortfolio()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = eqPortfolioVo;
                objects[2] = count;
                objects[3] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void LoadEquityPortfolioSellPairDetails(int ScripCode)
        {
            DataSet dsCustomeNP = new DataSet();
            DataTable dtEqSellpair = new DataTable();
            try
            {
                tradeDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());


                dsCustomeNP = customerPortfolioBo.GetCustomerEquityNPSellPair(int.Parse(ddlPortfolio.SelectedValue.ToString()), tradeDate, ScripCode, ddl_type.SelectedValue.ToString());
                radEqSellPair.VisibleOnPageLoad = true;
                dtEqSellpair = dsCustomeNP.Tables[0];



                if (dtEqSellpair.Rows.Count > 0)
                {

                    gvEqSellPairDetails.DataSource = dtEqSellpair;
                    gvEqSellPairDetails.DataBind();
                    if (Cache["gvEqSellPairDetails" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvEqSellPairDetails" + customerVo.CustomerId.ToString(), dtEqSellpair);
                    }
                    else
                    {
                        Cache.Remove("gvEqSellPairDetails" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvEqSellPairDetails" + customerVo.CustomerId.ToString(), dtEqSellpair);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:LoadEquityPortfolio()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = eqPortfolioVo;
                objects[3] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        public void LoadAvgPriceCalculationDetails(int ScripCode)
        {
            DataSet dsCustomeNP = new DataSet();
            DataTable dtEqAvgPrice = new DataTable();
            try
            {
                tradeDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());


                dsCustomeNP = customerPortfolioBo.GetCustomerEquityAvgPriceCalculation(int.Parse(ddlPortfolio.SelectedValue.ToString()), tradeDate, ScripCode, ddl_type.SelectedValue.ToString());


                dtEqAvgPrice = dsCustomeNP.Tables[0];



                if (dtEqAvgPrice.Rows.Count > 0)
                {

                    gvAvgPriceCalcDetails.DataSource = dtEqAvgPrice;
                    ViewState["dtEqAvg"] = dtEqAvgPrice;
                    gvAvgPriceCalcDetails.DataBind();

                    if (Cache["gvEqAvgPriceDetails" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvEqAvgPriceDetails" + customerVo.CustomerId.ToString(), dtEqAvgPrice);
                    }
                    else
                    {
                        Cache.Remove("gvEqAvgPriceDetails" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvEqAvgPriceDetails" + customerVo.CustomerId.ToString(), dtEqAvgPrice);
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
                FunctionInfo.Add("Method", "CustomerEquityNP.ascx:LoadAvgPriceCalculationDetails()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = eqPortfolioVo;
                objects[3] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvEqSellPairDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEqSellPairDetails = new DataTable();
            dtgvEqSellPairDetails = (DataTable)Cache["gvEqSellPairDetails" + customerVo.CustomerId.ToString()];
            gvEqSellPairDetails.DataSource = dtgvEqSellPairDetails;
        }
        protected void gvEqTranxDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEqTranxDetails = new DataTable();
            dtgvEqTranxDetails = (DataTable)Cache["gvEqTranxDetails" + customerVo.CustomerId.ToString()];
            gvEqTranxDetails.DataSource = dtgvEqTranxDetails;

        }
        protected void gvAvgPriceCalcDetails_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEqAvgPriceDetails = new DataTable();
            dtgvEqAvgPriceDetails = (DataTable)Cache["gvEqAvgPriceDetails" + customerVo.CustomerId.ToString()];
            gvAvgPriceCalcDetails.DataSource = dtgvEqAvgPriceDetails;

        }

        protected void gvEqSellPairDetails_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            int index = gvEqSellPairDetails.CurrentPageIndex;
            radEqSellPair.VisibleOnPageLoad = true;

        }
        protected void gvEqTranxDetails_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            int index = gvEqTranxDetails.CurrentPageIndex;
            radEqTranxDetails.VisibleOnPageLoad = true;
        }
        protected void gvAvgPriceCalcDetails_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            int index = gvAvgPriceCalcDetails.CurrentPageIndex;
            radEqAvgPriceDetails.VisibleOnPageLoad = true;
        }


        protected void gvEqTranxDetails_DataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            DataTable dtgvAvgPriceCalcDetails = new DataTable();

            dtgvAvgPriceCalcDetails = (DataTable)ViewState["dtEqAvg"];
            if (dtgvAvgPriceCalcDetails != null)
            {
                object sumQuantity;
                sumQuantity = dtgvAvgPriceCalcDetails.Compute("Sum(CET_Quantity)", "");

                object sumTradeTotal;
                sumTradeTotal = dtgvAvgPriceCalcDetails.Compute("Sum(InvestedAmount)", "");

                Double avgValue = double.Parse(sumTradeTotal.ToString()) / double.Parse(sumQuantity.ToString());
                if (e.Item is GridFooterItem)
                {
                    GridFooterItem footer = (GridFooterItem)e.Item;
                    footer["CET_Rate"].Text = "Avg Price:" + " " + String.Format("{0:0.0000}", avgValue);

                }


            }

        }
        protected void gvEqSellPairDetails_DataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            int Count = 0;
            int row = 0;
            foreach (GridDataItem item in gvEqSellPairDetails.Items)
            {
                if (Count % 2 == 0)
                {
                    item["ScripName"].BackColor = System.Drawing.Color.RoyalBlue;
                    item["TradeDate"].BackColor = System.Drawing.Color.RoyalBlue;
                    item["CET_BuySell"].BackColor = System.Drawing.Color.RoyalBlue;
                    item["Quantity"].BackColor = System.Drawing.Color.RoyalBlue;
                    item["CET_Rate"].BackColor = System.Drawing.Color.RoyalBlue;
                    item["Amount"].BackColor = System.Drawing.Color.RoyalBlue;
                    item["ProfitOrLoss"].BackColor = System.Drawing.Color.RoyalBlue;

                    row++;
                    if (row == 2)
                    {
                        Count++;
                        row = 0;
                    }
                }

                else
                {
                    item["ScripName"].BackColor = System.Drawing.Color.IndianRed;
                    item["TradeDate"].BackColor = System.Drawing.Color.IndianRed;
                    item["CET_BuySell"].BackColor = System.Drawing.Color.IndianRed;
                    item["Quantity"].BackColor = System.Drawing.Color.IndianRed;
                    item["CET_Rate"].BackColor = System.Drawing.Color.IndianRed;
                    item["Amount"].BackColor = System.Drawing.Color.IndianRed;
                    item["ProfitOrLoss"].BackColor = System.Drawing.Color.IndianRed;
                    row++;
                    if (row == 2)
                    {
                        Count++;
                        row = 0;
                    }
                }
            }
        }


        protected void Button2_Click(object sender, System.EventArgs e)
        {
            ImageButton clickedButton = sender as ImageButton;

            if (RadTabStrip1.SelectedIndex == 0)
            {

                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton1")
                {
                    ConfigureExport(1, 0);

                }
                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton2")
                {
                    ConfigureExport(1, 1);

                }
            }
            else if (RadTabStrip1.SelectedIndex == 1)
            {
                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton1")
                {
                    ConfigureExport(2, 0);

                }
                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton2")
                {
                    ConfigureExport(2, 1);

                }

            }
            else if (RadTabStrip1.SelectedIndex == 2)
            {
                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton1")
                {
                    ConfigureExport(3, 0);

                }
                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton2")
                {
                    ConfigureExport(3, 1);

                }
            }
            else
            {
                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton1")
                {
                    ConfigureExport(4, 0);

                }
                if (clickedButton.ClientID == "ctrl_ViewEquityPortfolios_ImageButton2")
                {
                    ConfigureExport(4, 1);

                }
            }


        }
        protected void ButtonAvg_Click(object sender, System.EventArgs e)
        {
            ConfigureExport(5);

        }
        protected void ButtonTranx_Click(object sender, System.EventArgs e)
        {
            ConfigureExport(3);

        }
        protected void ButtonSellPair_Click(object sender, System.EventArgs e)
        {

            ConfigureExport(4);
        }
        public void ConfigureExport(int x)
        {

            if (x == 3)
            {
                if (gvEqTranxDetails.Items.Count != 0)
                {
                    gvEqTranxDetails.ExportSettings.ExportOnlyData = true;
                    gvEqTranxDetails.ExportSettings.IgnorePaging = true;
                    gvEqTranxDetails.ExportSettings.OpenInNewWindow = true;
                    gvEqTranxDetails.MasterTableView.ExportToCSV();
                }
            }
            if (x == 4)
            {
                if (gvEqSellPairDetails.Items.Count != 0)
                {
                    gvEqSellPairDetails.ExportSettings.ExportOnlyData = true;
                    gvEqSellPairDetails.ExportSettings.IgnorePaging = true;
                    gvEqSellPairDetails.ExportSettings.OpenInNewWindow = true;
                    gvEqSellPairDetails.MasterTableView.ExportToCSV();
                }
            }
            if (x == 5)
            {
                if (gvAvgPriceCalcDetails.Items.Count != 0)
                {
                    gvAvgPriceCalcDetails.ExportSettings.ExportOnlyData = true;
                    gvAvgPriceCalcDetails.ExportSettings.IgnorePaging = true;
                    gvAvgPriceCalcDetails.ExportSettings.OpenInNewWindow = true;
                    gvAvgPriceCalcDetails.MasterTableView.ExportToCSV();
                }
            }
        }
        public void ConfigureExport(int x, int y)
        {
            if (x == 1)
            {
                if (gvEquityPortfolio.Items.Count != 0)
                {
                    gvEquityPortfolio.ExportSettings.ExportOnlyData = true;
                    gvEquityPortfolio.ExportSettings.IgnorePaging = true;
                    gvEquityPortfolio.ExportSettings.OpenInNewWindow = true;
                    gvEquityPortfolio.MasterTableView.GetColumn("action").Display = false;

                    gvEquityPortfolio.MasterTableView.GetColumn("AvgPriceDup").Visible = true;
                    gvEquityPortfolio.MasterTableView.GetColumn("AvgPrice").Visible = false;
                    if (y == 0)
                        gvEquityPortfolio.MasterTableView.ExportToExcel();
                    else if (y == 1)
                        gvEquityPortfolio.MasterTableView.ExportToCSV();
                }
            }
            if (x == 2)
            {
                if (gvEquityPortfolioRealizedDelivery.Items.Count != 0)
                {
                    gvEquityPortfolioRealizedDelivery.ExportSettings.ExportOnlyData = true;
                    gvEquityPortfolioRealizedDelivery.ExportSettings.IgnorePaging = true;
                    gvEquityPortfolioRealizedDelivery.ExportSettings.OpenInNewWindow = true;
                    gvEquityPortfolioRealizedDelivery.MasterTableView.GetColumn("action").Display = false;
                    if (y == 0)
                        gvEquityPortfolioRealizedDelivery.MasterTableView.ExportToExcel();
                    else if (y == 1)
                        gvEquityPortfolioRealizedDelivery.MasterTableView.ExportToCSV();
                }
            }
            if (x == 3)
            {
                if (gvrEQAll.Items.Count != 0)
                {
                    gvrEQAll.ExportSettings.ExportOnlyData = true;
                    gvrEQAll.ExportSettings.IgnorePaging = true;
                    gvrEQAll.ExportSettings.OpenInNewWindow = true;
                    gvrEQAll.MasterTableView.GetColumn("action").Display = false;
                    if (y == 0)
                        gvrEQAll.MasterTableView.ExportToExcel();
                    else if (y == 1)
                        gvrEQAll.MasterTableView.ExportToCSV();
                }
            }
            if (x == 4)
            {
                if (gvEquityPortfolioRealizedSpeculative.Items.Count != 0)
                {
                    gvEquityPortfolioRealizedSpeculative.ExportSettings.ExportOnlyData = true;
                    gvEquityPortfolioRealizedSpeculative.ExportSettings.IgnorePaging = true;
                    gvEquityPortfolioRealizedSpeculative.ExportSettings.OpenInNewWindow = true;
                    gvEquityPortfolioRealizedSpeculative.MasterTableView.GetColumn("action").Display = false;
                    if (y == 0)
                        gvEquityPortfolioRealizedSpeculative.MasterTableView.ExportToExcel();
                    else if (y == 1)
                        gvEquityPortfolioRealizedSpeculative.MasterTableView.ExportToCSV();
                }
            }
        }

        protected void ddlDateRange_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ddlDateRange.SelectedValue == "DateRange")
            {
                if ((DateTime.Now.Month <= 3))
                    txtPickDate.SelectedDate = DateTime.Parse("1/04/" + ((DateTime.Now.Year) - 1));
                else
                    txtPickDate.SelectedDate = DateTime.Parse("1/04/" + (DateTime.Now.Year));

                radToDate.SelectedDate = DateTime.Now;
                tdlblDate.Visible = true;
                tdToDate.Visible = true;
                lblDate.Text = "From:";
                RadAjaxPanel4.Visible = false;
                RadAjaxPanel1.Visible = false;
            }
            else
            {
                txtPickDate.SelectedDate = DateTime.Parse(genDict[Constants.EQDate.ToString()].ToString());
                tdlblDate.Visible = false;
                tdToDate.Visible = false;
                lblDate.Text = "As On:";
                RadAjaxPanel4.Visible = false;
                RadAjaxPanel1.Visible = false;
            }
        }
        public void LoadEquityPortfolioNPInDateRange()
        {
            RadAjaxPanel4.Visible = true;
            DataSet dsCustomeNP = new DataSet();
            DataTable dtUnRealized = new DataTable();
            DataTable dtRealizedDelivery = new DataTable();
            DateTime dtStartDate;
            DateTime dtToDate;


            try
            {
                dtStartDate = DateTime.Parse(txtPickDate.SelectedDate.ToString());
                dtToDate = DateTime.Parse(radToDate.SelectedDate.ToString());
                RadAjaxPanel4.Visible = true;
                RadAjaxPanel1.Visible = false;


                dsCustomeNP = customerPortfolioBo.GetCustomerEquityNPInDateRange(int.Parse(ddlPortfolio.SelectedValue.ToString()), dtStartDate, dtToDate, ddl_type.SelectedItem.Value.ToString());


                dtUnRealized = dsCustomeNP.Tables[0];

                if (dtUnRealized.Rows.Count > 0)
                {
                    gvUnRealizedHolding.DataSource = dtUnRealized;
                    gvUnRealizedHolding.DataBind();
                    if (Cache["gvUnRealizedHolding" + customerVo.CustomerId.ToString()] == null)
                    {
                        Cache.Insert("gvUnRealizedHolding" + customerVo.CustomerId.ToString(), dtUnRealized);
                    }
                    else
                    {
                        Cache.Remove("gvUnRealizedHolding" + customerVo.CustomerId.ToString());
                        Cache.Insert("gvUnRealizedHolding" + customerVo.CustomerId.ToString(), dtUnRealized);
                    }
                }
                else
                    gvUnRealizedHolding.DataBind();


            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:LoadEquityPortfolio()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = eqPortfolioVo;
                objects[3] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        protected void gvUnRealizedHolding_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvUnRealizedHolding = new DataTable();
            dtgvUnRealizedHolding = (DataTable)Cache["gvUnRealizedHolding" + customerVo.CustomerId.ToString()];
            gvUnRealizedHolding.DataSource = dtgvUnRealizedHolding;
        }
        protected void gvUnRealizedHolding_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                if (Convert.ToDouble(dataItem["RealisedProfitOrLoss"].Text) < 0)
                    dataItem["RealisedProfitOrLoss"].ForeColor = System.Drawing.Color.Red;
                if (Convert.ToDouble(dataItem["UnrealizedProfit"].Text) < 0)
                    dataItem["UnrealizedProfit"].ForeColor = System.Drawing.Color.Red;
                if (Convert.ToDouble(dataItem["TotalProfitOrLoss"].Text) < 0)
                    dataItem["TotalProfitOrLoss"].ForeColor = System.Drawing.Color.Red;
                if (Convert.ToDouble(dataItem["XIRRValue"].Text) < 0)
                    dataItem["XIRRValue"].ForeColor = System.Drawing.Color.Red;

            }

        }
        protected void Button3_Click(object sender, System.EventArgs e)
        {
            ImageButton clickedButton = sender as ImageButton;


            if (clickedButton.ClientID == "ctrl_CustomerEquityNP_ImageButton5")
            {
                gvUnRealizedHolding.ExportSettings.ExportOnlyData = true;
                gvUnRealizedHolding.ExportSettings.IgnorePaging = true;
                gvUnRealizedHolding.ExportSettings.OpenInNewWindow = true;
                gvUnRealizedHolding.MasterTableView.ExportToExcel();

            }
            if (clickedButton.ClientID == "ctrl_CustomerEquityNP_ImageButton6")
            {
                gvUnRealizedHolding.ExportSettings.ExportOnlyData = true;
                gvUnRealizedHolding.ExportSettings.IgnorePaging = true;
                gvUnRealizedHolding.ExportSettings.OpenInNewWindow = true;
                gvUnRealizedHolding.MasterTableView.ExportToCSV();
            }


        }


    }
}