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

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewEquityPortfolios : System.Web.UI.UserControl
    {
        int index;
        EQPortfolioVo eqPortfolioVo;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<EQPortfolioVo> eqPortfolioList = new List<EQPortfolioVo>();
        CustomerVo customerVo;
        UserVo userVo;
        CustomerPortfolioVo customerPortfolioVo;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        //  DateTime tradeDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        DateTime tradeDate = new DateTime();
        decimal currentPrice_All = 0;
        decimal currentPrice_UnRe = 0;
        private double realised_all = 0;
        private double realised_delivery = 0;
        private double realised_spec = 0;
        private double unrealised_all = 0;
        private double unrealisedPNL = 0;
        private double costofpurchase = 0;
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
        private decimal currentValue = 0;
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
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                GetLatestValuationDate();
                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    LoadEquityPortfolio();
                    lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
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
        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                adviserId = advisorVo.advisorId;


                if (portfolioBo.GetLatestValuationDate(adviserId, "EQ") != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "EQ").ToString());
                }
                if (portfolioBo.GetLatestValuationDate(adviserId, "MF") != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "MF").ToString());
                }
                genDict.Add("EQDate", EQValuationDate);
                genDict.Add("MFDate", MFValuationDate);
                Session["ValuationDate"] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[3];
                objects[0] = EQValuationDate;
                objects[1] = adviserId;
                objects[2] = MFValuationDate;
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
            try
            {
                genDict = (Dictionary<string, DateTime>)Session["ValuationDate"];
                tradeDate = DateTime.Parse(genDict["EQDate"].ToString());
                if (tradeDate == DateTime.MinValue)
                {
                    count = 0;
                }
                else
                {
                    eqPortfolioList = customerPortfolioBo.GetCustomerEquityPortfolio(customerVo.CustomerId, portfolioId, tradeDate, hdnScipNameFilter.Value.Trim(),string.Empty);
                    if (eqPortfolioList != null)
                    {
                        count = eqPortfolioList.Count;
                    }
                }

                if (count == 0)
                {

                    lblMessage.Visible = true;
                    lblMessageD.Visible = true;
                    lblMessageSpeculative.Visible = true;
                    lblMessageUnrealized.Visible = true;
                    gvEquityPortfolio.DataSource = null;
                    gvEquityPortfolio.DataBind();
                    tblDelivery.Visible = false;
                    tblPortfolio.Visible = false;
                    tblSpec.Visible = false;
                    tblUnrealized.Visible = false;
                }
                else
                {
                    lblMessage.Visible = false;
                    lblMessageD.Visible = false;
                    lblMessageSpeculative.Visible = false;
                    lblMessageUnrealized.Visible = false;
                    tblDelivery.Visible = true;
                    tblPortfolio.Visible = true;
                    tblSpec.Visible = true;
                    tblUnrealized.Visible = true;
                    DataTable dtEqPortfolio = new DataTable();
                    DataTable dtEqPortfolioDelivery = new DataTable();
                    DataTable dtEqPortfolioSpeculative = new DataTable();
                    DataTable dtEqPortfolioUnrealized = new DataTable();

                    // Consolidated View Datatable
                    dtEqPortfolio.Columns.Add("Sl.No.");
                    dtEqPortfolio.Columns.Add("CompanyName");
                    dtEqPortfolio.Columns.Add("Quantity");
                    dtEqPortfolio.Columns.Add("AveragePrice");
                    dtEqPortfolio.Columns.Add("CostOfPurchase");
                    dtEqPortfolio.Columns.Add("MarketPrice");
                    dtEqPortfolio.Columns.Add("CurrentValue");
                    dtEqPortfolio.Columns.Add("UnRealizedPL");
                    dtEqPortfolio.Columns.Add("RealizedPL");
                    dtEqPortfolio.Columns.Add("XIRR");

                    //Delivery Based View Datatable
                    dtEqPortfolioDelivery.Columns.Add("Sl.No.");
                    dtEqPortfolioDelivery.Columns.Add("CompanyName");
                    dtEqPortfolioDelivery.Columns.Add("SaleQty");
                    dtEqPortfolioDelivery.Columns.Add("CostOfSales");
                    dtEqPortfolioDelivery.Columns.Add("RealizedSalesProceeds");
                    dtEqPortfolioDelivery.Columns.Add("RealizedPL");
                    dtEqPortfolioDelivery.Columns.Add("XIRR");

                    //Speculative Based View Datatable
                    dtEqPortfolioSpeculative.Columns.Add("Sl.No.");
                    dtEqPortfolioSpeculative.Columns.Add("CompanyName");
                    dtEqPortfolioSpeculative.Columns.Add("SaleQty");
                    dtEqPortfolioSpeculative.Columns.Add("RealizedSalesProceeds");
                    dtEqPortfolioSpeculative.Columns.Add("CostOfSales");                   
                    dtEqPortfolioSpeculative.Columns.Add("RealizedPL");                    
                    dtEqPortfolioSpeculative.Columns.Add("XIRR");

                    //Unrealized View Datatable
                    dtEqPortfolioUnrealized.Columns.Add("Sl.No.");
                    dtEqPortfolioUnrealized.Columns.Add("CompanyName");
                    dtEqPortfolioUnrealized.Columns.Add("Quantity");
                    dtEqPortfolioUnrealized.Columns.Add("AveragePrice");
                    dtEqPortfolioUnrealized.Columns.Add("CostOfPurchase");
                    dtEqPortfolioUnrealized.Columns.Add("MarketPrice");
                    dtEqPortfolioUnrealized.Columns.Add("CurrentValue");
                    dtEqPortfolioUnrealized.Columns.Add("UnRealizedPL");
                    dtEqPortfolioUnrealized.Columns.Add("XIRR");

                    DataRow drEqPortfolio;
                    DataRow drEqPortfolioDelivery;
                    DataRow drEqPortfolioSpeculative;
                    DataRow drEqPortfolioUnrealized;

                    for (int i = 0; i < eqPortfolioList.Count; i++)
                    {
                        drEqPortfolio = dtEqPortfolio.NewRow();
                        drEqPortfolioDelivery = dtEqPortfolioDelivery.NewRow();
                        drEqPortfolioSpeculative = dtEqPortfolioSpeculative.NewRow();

                        eqPortfolioVo = new EQPortfolioVo();
                        eqPortfolioVo = eqPortfolioList[i];
                        //Consolidated View
                        drEqPortfolio[0] = eqPortfolioVo.EqPortfolioId.ToString();
                        drEqPortfolio[1] = eqPortfolioVo.EQCompanyName.ToString();
                        drEqPortfolio[2] = eqPortfolioVo.Quantity.ToString("f0");
                        drEqPortfolio[3] = double.Parse(eqPortfolioVo.AveragePrice.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        if (eqPortfolioVo.CostOfPurchase.ToString() != string.Empty)
                        {
                            costofpurchase = costofpurchase + eqPortfolioVo.CostOfPurchase;
                            drEqPortfolio[4] = double.Parse(eqPortfolioVo.CostOfPurchase.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        }
                        drEqPortfolio[5] = double.Parse(eqPortfolioVo.MarketPrice.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        drEqPortfolio[6] = double.Parse(eqPortfolioVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        if (eqPortfolioVo.UnRealizedPNL.ToString() != string.Empty)
                        {
                            unrealised_all = unrealised_all + eqPortfolioVo.UnRealizedPNL;
                            drEqPortfolio[7] = double.Parse(eqPortfolioVo.UnRealizedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        }
                   
                       
                        if (eqPortfolioVo.RealizedPNL.ToString() != string.Empty)
                        {
                            realised_all = realised_all + eqPortfolioVo.RealizedPNL;
                            drEqPortfolio[8] = double.Parse(eqPortfolioVo.RealizedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                        }
                        drEqPortfolio[9] = double.Parse(eqPortfolioVo.XIRR.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                       
                        dtEqPortfolio.Rows.Add(drEqPortfolio);

                        if (eqPortfolioVo.DeliverySalesQuantity != 0)
                        {
                            //Delivery Based View
                            drEqPortfolioDelivery[0] = eqPortfolioVo.EqPortfolioId.ToString();
                            drEqPortfolioDelivery[1] = eqPortfolioVo.EQCompanyName.ToString();
                            drEqPortfolioDelivery[2] = eqPortfolioVo.DeliverySalesQuantity.ToString("f0");
                            drEqPortfolioDelivery[3] = double.Parse(eqPortfolioVo.DeliveryRealizedSalesProceeds.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            drEqPortfolioDelivery[4] = double.Parse(eqPortfolioVo.DeliveryCostOfSales.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            if (eqPortfolioVo.DeliveryRealizedProfitLoss.ToString() != string.Empty)
                            {
                                realised_delivery = realised_delivery + eqPortfolioVo.DeliveryRealizedProfitLoss;
                                drEqPortfolioDelivery[5] = double.Parse(eqPortfolioVo.DeliveryRealizedProfitLoss.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            }
                            drEqPortfolioDelivery[6] = double.Parse(eqPortfolioVo.XIRR.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));


                            dtEqPortfolioDelivery.Rows.Add(drEqPortfolioDelivery);
                        }
                        if (eqPortfolioVo.SpeculativeSalesQuantity != 0)
                        {
                            //Speculative Based View
                            drEqPortfolioSpeculative[0] = eqPortfolioVo.EqPortfolioId.ToString();
                            drEqPortfolioSpeculative[1] = eqPortfolioVo.EQCompanyName.ToString();
                            drEqPortfolioSpeculative[2] = eqPortfolioVo.SpeculativeSalesQuantity.ToString("f0");
                            drEqPortfolioSpeculative[3] = double.Parse(eqPortfolioVo.SpeculativeRealizedSalesProceeds.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            drEqPortfolioSpeculative[4] = double.Parse(eqPortfolioVo.SpeculativeCostOfSales.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            if (eqPortfolioVo.SpeculativeRealizedProfitLoss.ToString() != string.Empty)
                            {
                                realised_spec = realised_spec + eqPortfolioVo.SpeculativeRealizedProfitLoss;
                                drEqPortfolioSpeculative[5] = double.Parse(eqPortfolioVo.SpeculativeRealizedProfitLoss.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            }
                            drEqPortfolioSpeculative[6] = double.Parse(eqPortfolioVo.XIRR.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            
                            dtEqPortfolioSpeculative.Rows.Add(drEqPortfolioSpeculative);
                        }

                        if (eqPortfolioVo.Quantity != 0)
                        {
                            drEqPortfolioUnrealized = dtEqPortfolioUnrealized.NewRow();
                            drEqPortfolioUnrealized[0] = eqPortfolioVo.EqPortfolioId.ToString();
                            drEqPortfolioUnrealized[1] = eqPortfolioVo.EQCompanyName.ToString();
                            drEqPortfolioUnrealized[2] = eqPortfolioVo.Quantity.ToString("f0");
                            drEqPortfolioUnrealized[3] = double.Parse(eqPortfolioVo.AveragePrice.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            if (eqPortfolioVo.CostOfPurchase.ToString() != string.Empty)
                            {
                                costofpurchase = costofpurchase + eqPortfolioVo.CostOfPurchase;
                                drEqPortfolio[4] = double.Parse(eqPortfolioVo.CostOfPurchase.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            }
                            drEqPortfolioUnrealized[5] = double.Parse(eqPortfolioVo.MarketPrice.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            drEqPortfolioUnrealized[6] = double.Parse(eqPortfolioVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            if (eqPortfolioVo.UnRealizedPNL.ToString() != string.Empty)
                            {
                                unrealisedPNL = unrealisedPNL + eqPortfolioVo.UnRealizedPNL;
                                drEqPortfolioUnrealized[7] = double.Parse(eqPortfolioVo.UnRealizedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            }
                            drEqPortfolioUnrealized[8] = double.Parse(eqPortfolioVo.XIRR.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                   
                            dtEqPortfolioUnrealized.Rows.Add(drEqPortfolioUnrealized);
                        }
                    }

                    // Bind EQ Net Position Details
                    if (dtEqPortfolio.Rows.Count > 0)
                    {
                        gvEquityPortfolio.DataSource = dtEqPortfolio;
                        gvEquityPortfolio.DataBind();
                        gvEquityPortfolio.Visible = true;
                    }
                    else
                    {
                        gvEquityPortfolio.DataSource = null;
                        gvEquityPortfolio.DataBind();
                        lblMessage.Visible = true;
                    }

                    TextBox txtScripName = GetEQPortfolioScripNameTextBox();
                    if (txtScripName != null)
                    {
                        if (hdnScipNameFilter.Value != "")
                        {
                            txtScripName.Text = hdnScipNameFilter.Value.Trim();
                        }
                    }

                    // Bind EQ Delivery Details
                    string SearchExpression = string.Empty;

                    if (hdnRealizedScipFilter.Value.Trim() != "")
                    {
                        SearchExpression = "CompanyName like '%" + hdnRealizedScipFilter.Value.Trim() + "%'";
                        dtEqPortfolioDelivery.DefaultView.RowFilter = SearchExpression;
                    }

                    if (dtEqPortfolioDelivery.Rows.Count > 0)
                    {
                        gvEquityPortfolioDelivery.DataSource = dtEqPortfolioDelivery;
                        gvEquityPortfolioDelivery.DataBind();
                        gvEquityPortfolioDelivery.Visible = true;
                    }
                    else
                    {
                        gvEquityPortfolioDelivery.DataSource = null;
                        gvEquityPortfolioDelivery.DataBind();
                        lblMessageD.Visible = true;
                    }

                    TextBox txtScripDName = GetEQRealizedScripNameTextBox();
                    if (txtScripDName != null)
                    {
                        if (hdnRealizedScipFilter.Value != "")
                        {
                            txtScripDName.Text = hdnRealizedScipFilter.Value.Trim();
                        }
                    }

                    // Bind EQ Speculative Details
                    if (hdnRealizedSpecScipFilter.Value.Trim() != "")
                    {
                        SearchExpression = "CompanyName like '%" + hdnRealizedSpecScipFilter.Value.Trim() + "%'";
                        dtEqPortfolioSpeculative.DefaultView.RowFilter = SearchExpression;
                    }

                    if (dtEqPortfolioSpeculative.Rows.Count > 0)
                    {

                        gvEquityPortfolioSpeculative.DataSource = dtEqPortfolioSpeculative;
                        gvEquityPortfolioSpeculative.DataBind();
                        gvEquityPortfolioSpeculative.Visible = true;
                    }
                    else
                    {
                        gvEquityPortfolioSpeculative.DataSource = null;
                        gvEquityPortfolioSpeculative.DataBind();
                        lblMessageSpeculative.Visible = true;
                    }

                    TextBox txtScripSName = GetEQRealizedSpecScripNameTextBox();
                    if (txtScripSName != null)
                    {
                        if (hdnRealizedSpecScipFilter.Value != "")
                        {
                            txtScripSName.Text = hdnRealizedSpecScipFilter.Value.Trim();
                        }
                    }

                    // Bind EQ Unrealized Details
                    if (hdnUnRealizedScipFilter.Value.Trim() != "")
                    {
                        SearchExpression = "CompanyName like '%" + hdnUnRealizedScipFilter.Value.Trim() + "%'";
                        dtEqPortfolioUnrealized.DefaultView.RowFilter = SearchExpression;
                    }

                    if (dtEqPortfolioUnrealized.Rows.Count > 0)
                    {
                        gvEquityPortfolioUnrealized.DataSource = dtEqPortfolioUnrealized;
                        gvEquityPortfolioUnrealized.DataBind();
                        gvEquityPortfolioUnrealized.Visible = true;
                    }
                    else
                    {
                        gvEquityPortfolioUnrealized.DataSource = null;
                        gvEquityPortfolioUnrealized.DataBind();
                        lblMessageUnrealized.Visible = true;
                    }

                    TextBox txtScripUnrealizedName = GetEQUnRealizedScripNameTextBox();
                    if (txtScripUnrealizedName != null)
                    {
                        if (hdnUnRealizedScipFilter.Value != "")
                        {
                            txtScripUnrealizedName.Text = hdnUnRealizedScipFilter.Value.Trim();
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
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            LoadEquityPortfolio();
            lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
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

        #region Equity Portfolio Consolidated Grid View Methods
        protected void gvEquityPortfolio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int slNo = int.Parse(gvEquityPortfolio.DataKeys[index].Value.ToString());
                    Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                    Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                    if (e.CommandName == "Select")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolio_RowCommand()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
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

        protected void gvEquityPortfolio_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridVIew(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridVIew(sortExpression, ASCENDING);
            }
        }

        private void SortGridVIew(string sortExpression, string direction)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                DataTable dtEqPortfolio = new DataTable();

                dtEqPortfolio.Columns.Add("Sl.No.");
                dtEqPortfolio.Columns.Add("CompanyName");
                dtEqPortfolio.Columns.Add("Quantity");
                dtEqPortfolio.Columns.Add("AveragePrice");
                dtEqPortfolio.Columns.Add("CostOfPurchase");
                dtEqPortfolio.Columns.Add("MarketPrice");
                dtEqPortfolio.Columns.Add("CurrentValue");
                dtEqPortfolio.Columns.Add("UnRealizedPL");
                dtEqPortfolio.Columns.Add("RealizedPL");
                dtEqPortfolio.Columns.Add("XIRR");

                DataRow drEqPortfolio;
                for (int i = 0; i < eqPortfolioList.Count; i++)
                {
                    drEqPortfolio = dtEqPortfolio.NewRow();
                    eqPortfolioVo = new EQPortfolioVo();
                    eqPortfolioVo = eqPortfolioList[i];
                    drEqPortfolio[0] = eqPortfolioVo.EqPortfolioId.ToString();
                    drEqPortfolio[1] = eqPortfolioVo.EQCompanyName.ToString();
                    drEqPortfolio[2] = eqPortfolioVo.Quantity.ToString("f0");
                    drEqPortfolio[3] = eqPortfolioVo.AveragePrice.ToString("f2");
                    drEqPortfolio[4] = eqPortfolioVo.CostOfPurchase.ToString("f2");
                    drEqPortfolio[5] = eqPortfolioVo.MarketPrice.ToString("f2");
                    drEqPortfolio[6] = eqPortfolioVo.CurrentValue.ToString("f2");
                    drEqPortfolio[7] = eqPortfolioVo.UnRealizedPNL.ToString("f2");
                    drEqPortfolio[8] = eqPortfolioVo.RealizedPNL.ToString("f2");
                    drEqPortfolio[9] = eqPortfolioVo.XIRR.ToString("f2");
                    dtEqPortfolio.Rows.Add(drEqPortfolio);
                }
                DataView dv = new DataView(dtEqPortfolio);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:SortGridVIew()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        #endregion Equity Portfolio Consolidated Grid View Methods

        #region Equity Portfolio Delivery Based Grid View Methods
        protected void gvEquityPortfolioDelivery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int slNo = int.Parse(gvEquityPortfolioDelivery.DataKeys[index].Value.ToString());
                    Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                    Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                    if (e.CommandName == "Select")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolio_RowCommand()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvEquityPortfolioDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquityPortfolioDelivery.PageIndex = e.NewPageIndex;
            gvEquityPortfolioDelivery.DataBind();
        }

        protected void gvEquityPortfolioDelivery_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortDeliveryGridVIew(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortDeliveryGridVIew(sortExpression, ASCENDING);
            }

            string TabberScript = "<script language='javascript'>document.getElementById('dvEquityPortfolioDelivery').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

        }

        private void SortDeliveryGridVIew(string sortExpression, string direction)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                DataTable dtEqPortfolio = new DataTable();

                dtEqPortfolio.Columns.Add("Sl.No.");
                dtEqPortfolio.Columns.Add("CompanyName");
                dtEqPortfolio.Columns.Add("SaleQty");
                dtEqPortfolio.Columns.Add("CostOfSales");
                dtEqPortfolio.Columns.Add("RealizedSalesProceeds");
                dtEqPortfolio.Columns.Add("RealizedPL");

                DataRow drEqPortfolio;
                for (int i = 0; i < eqPortfolioList.Count; i++)
                {
                    drEqPortfolio = dtEqPortfolio.NewRow();
                    eqPortfolioVo = new EQPortfolioVo();
                    eqPortfolioVo = eqPortfolioList[i];
                    drEqPortfolio[0] = eqPortfolioVo.EqPortfolioId.ToString();
                    drEqPortfolio[1] = eqPortfolioVo.EQCompanyName.ToString();
                    drEqPortfolio[2] = eqPortfolioVo.DeliverySalesQuantity.ToString("f0");
                    drEqPortfolio[3] = eqPortfolioVo.DeliveryCostOfSales.ToString("f2");
                    drEqPortfolio[4] = eqPortfolioVo.DeliveryRealizedSalesProceeds.ToString("f2");
                    drEqPortfolio[5] = eqPortfolioVo.DeliveryRealizedProfitLoss.ToString("f2");
                    dtEqPortfolio.Rows.Add(drEqPortfolio);

                }
                DataView dv = new DataView(dtEqPortfolio);
                dv.Sort = sortExpression + direction;
                gvEquityPortfolioDelivery.DataSource = dv;
                gvEquityPortfolioDelivery.DataBind();
                gvEquityPortfolioDelivery.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:SortDeliveryGridVIew()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        #endregion Equity Portfolio Delivery Based Grid View Methods

        #region Equity Portfolio Speculative Based Grid View Methods
        protected void gvEquityPortfolioSpeculative_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currentValue.ToString()));

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int slNo = int.Parse(gvEquityPortfolioSpeculative.DataKeys[index].Value.ToString());
                    Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                    Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                    if (e.CommandName == "Select")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioSpeculative_RowCommand()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvEquityPortfolioSpeculative_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquityPortfolioSpeculative.PageIndex = e.NewPageIndex;
            gvEquityPortfolioSpeculative.DataBind();
        }

        protected void gvEquityPortfolioSpeculative_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortSpeculativeGridVIew(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortSpeculativeGridVIew(sortExpression, ASCENDING);
            }

            string TabberScript = "<script language='javascript'>document.getElementById('dvEquityPortfolioSpeculative').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

        }

        private void SortSpeculativeGridVIew(string sortExpression, string direction)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currentValue.ToString()));

                DataTable dtEqPortfolio = new DataTable();

                dtEqPortfolio.Columns.Add("Sl.No.");
                dtEqPortfolio.Columns.Add("CompanyName");
                dtEqPortfolio.Columns.Add("SaleQty");
                dtEqPortfolio.Columns.Add("CostOfSales");
                dtEqPortfolio.Columns.Add("RealizedSalesProceeds");
                dtEqPortfolio.Columns.Add("RealizedPL");
                dtEqPortfolio.Columns.Add("AveragePrice");

                DataRow drEqPortfolio;
                for (int i = 0; i < eqPortfolioList.Count; i++)
                {
                    drEqPortfolio = dtEqPortfolio.NewRow();
                    eqPortfolioVo = new EQPortfolioVo();
                    eqPortfolioVo = eqPortfolioList[i];
                    drEqPortfolio[0] = eqPortfolioVo.EqPortfolioId.ToString();
                    drEqPortfolio[1] = eqPortfolioVo.EQCompanyName.ToString();
                    drEqPortfolio[2] = eqPortfolioVo.DeliverySalesQuantity.ToString("f0");
                    drEqPortfolio[3] = eqPortfolioVo.DeliveryCostOfSales.ToString("f2");
                    drEqPortfolio[4] = eqPortfolioVo.DeliveryRealizedSalesProceeds.ToString("f2");
                    drEqPortfolio[5] = eqPortfolioVo.DeliveryRealizedProfitLoss.ToString("f2");
                    drEqPortfolio[5] = eqPortfolioVo.AveragePrice.ToString("f2");
                    dtEqPortfolio.Rows.Add(drEqPortfolio);

                }
                DataView dv = new DataView(dtEqPortfolio);
                dv.Sort = sortExpression + direction;
                gvEquityPortfolioSpeculative.DataSource = dv;
                gvEquityPortfolioSpeculative.DataBind();
                gvEquityPortfolioSpeculative.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:SortSpeculativeGridVIew()");
                object[] objects = new object[1];
                objects[0] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        #endregion Equity Portfolio Speculative Based Grid View Methods

        #region Equity Portfolio Unrealized Grid View Methods
        protected void gvEquityPortfolioUnrealized_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currentValue.ToString()));

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int slNo = int.Parse(gvEquityPortfolioUnrealized.DataKeys[index].Value.ToString());
                    Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                    Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                    if (e.CommandName == "Select")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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

        protected void gvEquityPortfolioUnrealized_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquityPortfolioUnrealized.PageIndex = e.NewPageIndex;
            gvEquityPortfolioUnrealized.DataBind();
        }

        protected void gvEquityPortfolioUnrealized_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortUnrealizedGridVIew(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortUnrealizedGridVIew(sortExpression, ASCENDING);
            }

            string TabberScript = "<script language='javascript'>document.getElementById('dvEquityPortfolioUnrealized').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

        }

        private void SortUnrealizedGridVIew(string sortExpression, string direction)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currentValue.ToString()));

                DataTable dtEqPortfolioUnrealized = new DataTable();

                dtEqPortfolioUnrealized.Columns.Add("Sl.No.");
                dtEqPortfolioUnrealized.Columns.Add("CompanyName");
                dtEqPortfolioUnrealized.Columns.Add("Quantity");
                dtEqPortfolioUnrealized.Columns.Add("AveragePrice");
                dtEqPortfolioUnrealized.Columns.Add("CostOfPurchase");
                dtEqPortfolioUnrealized.Columns.Add("MarketPrice");
                dtEqPortfolioUnrealized.Columns.Add("CurrentValue");
                dtEqPortfolioUnrealized.Columns.Add("UnRealizedPL");

                DataRow drEqPortfolioUnrealized;
                for (int i = 0; i < eqPortfolioList.Count; i++)
                {

                    eqPortfolioVo = new EQPortfolioVo();
                    eqPortfolioVo = eqPortfolioList[i];
                    if (eqPortfolioVo.Quantity != 0)
                    {
                        drEqPortfolioUnrealized = dtEqPortfolioUnrealized.NewRow();
                        drEqPortfolioUnrealized[0] = eqPortfolioVo.EqPortfolioId.ToString();
                        drEqPortfolioUnrealized[1] = eqPortfolioVo.EQCompanyName.ToString();
                        drEqPortfolioUnrealized[2] = eqPortfolioVo.Quantity.ToString("f0");
                        drEqPortfolioUnrealized[3] = eqPortfolioVo.AveragePrice.ToString("f2");
                        drEqPortfolioUnrealized[4] = eqPortfolioVo.CostOfPurchase.ToString("f2");
                        drEqPortfolioUnrealized[5] = eqPortfolioVo.MarketPrice.ToString("f2");
                        drEqPortfolioUnrealized[6] = eqPortfolioVo.CurrentValue.ToString("f2");
                        drEqPortfolioUnrealized[7] = eqPortfolioVo.UnRealizedPNL.ToString("f2");
                        //drEqPortfolioUnrealized[8] = eqPortfolioVo.RealizedPNL.ToString("f2");

                        dtEqPortfolioUnrealized.Rows.Add(drEqPortfolioUnrealized);
                    }
                }

                DataView dv = new DataView(dtEqPortfolioUnrealized);
                dv.Sort = sortExpression + direction;
                gvEquityPortfolioUnrealized.DataSource = dv;
                gvEquityPortfolioUnrealized.DataBind();
                gvEquityPortfolioUnrealized.Visible = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:SortUnrealizedGridVIew()");
                object[] objects = new object[2];
                objects[0] = eqPortfolioVo;
                objects[1] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        #endregion Equity Portfolio Unrealized Grid View Methods

        protected void btnUpdateNP_Click(object sender, EventArgs e)
        {
            try
            {
                if (eqPortfolioList != null)
                {
                    customerPortfolioBo.DeleteEquityNetPosition(customerVo.CustomerId, tradeDate);
                    customerPortfolioBo.AddEquityNetPosition(eqPortfolioList, userVo.UserId);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:btnUpdateNP_Click()");
                object[] objects = new object[3];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = eqPortfolioList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvEquityPortfolio_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvEquityPortfolioDelivery_DataBound(object sender, EventArgs e)
        {
          
        }

        protected void gvEquityPortfolioUnrealized_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvEquityPortfolioSpeculative_DataBound(object sender, EventArgs e)
        {
           
        }

        protected void gvEquityPortfolio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                currentPrice_All = currentPrice_All + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CurrentValue"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total ";
                e.Row.Cells[5].Text = costofpurchase.ToString("f4");
                e.Row.Cells[5].Attributes.Add("align", "Right");
                e.Row.Cells[7].Text = currentPrice_All.ToString("f4");
                e.Row.Cells[7].Attributes.Add("align", "Right");
                e.Row.Cells[8].Text = unrealised_all.ToString("f4");
                e.Row.Cells[8].Attributes.Add("align", "Right");
                e.Row.Cells[9].Text = realised_all.ToString("f4");
                e.Row.Cells[9].Attributes.Add("align", "Right");


            }
        }
        protected void gvEquityPortfolioSpeculative_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {


                e.Row.Cells[6].Text = realised_spec.ToString("f4");
                e.Row.Cells[6].Attributes.Add("align", "Right");
            }
        }
        protected void gvEquityPortfolioDelivery_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[6].Text = realised_delivery.ToString("f4");
                e.Row.Cells[6].Attributes.Add("align", "Right");
            }
        }
        protected void gvEquityPortfolioUnrealized_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                currentPrice_UnRe = currentPrice_UnRe + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CurrentValue"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total ";
                e.Row.Cells[5].Text = costofpurchase.ToString("f4");
                e.Row.Cells[5].Attributes.Add("align", "Right");
                e.Row.Cells[7].Text = String.Format("{0:n2}", decimal.Parse(currentPrice_UnRe.ToString("f2")));
                currentValue = decimal.Parse(currentPrice_UnRe.ToString("f2"));
                e.Row.Cells[7].Attributes.Add("align", "Right");
                e.Row.Cells[8].Text = unrealisedPNL.ToString("f4");
                e.Row.Cells[8].Attributes.Add("align", "Right");
            }
        }

        protected void btnEQNPSearch_Click(object sender, EventArgs e)
        {
            hdnRealizedScipFilter.Value = "";
            hdnRealizedSpecScipFilter.Value = "";
            hdnUnRealizedScipFilter.Value = "";

            string TabberScript = "<script language='javascript'>document.getElementById('dvEquityPortfolio').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

            TextBox txtScripName = GetEQPortfolioScripNameTextBox();

            if (txtScripName != null)
            {
                hdnScipNameFilter.Value = txtScripName.Text.Trim();

                LoadEquityPortfolio();
            }
        }

        protected void btnEQRealizedSearch_Click(object sender, EventArgs e)
        {
            hdnRealizedScipFilter.Value = "";

            string TabberScript = "<script language='javascript'>document.getElementById('dvEquityPortfolioDelivery').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

            TextBox txtScripName = GetEQRealizedScripNameTextBox();

            if (txtScripName != null)
            {
                hdnRealizedScipFilter.Value = txtScripName.Text.Trim();
                LoadEquityPortfolio();
            }
        }

        protected void btnEQRealizedSpecSearch_Click(object sender, EventArgs e)
        {
            hdnRealizedSpecScipFilter.Value = "";

            string TabberScript = "<script language='javascript'>document.getElementById('dvEquityPortfolioSpeculative').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

            TextBox txtScripName = GetEQRealizedSpecScripNameTextBox();

            if (txtScripName != null)
            {
                hdnRealizedSpecScipFilter.Value = txtScripName.Text.Trim();
                LoadEquityPortfolio();
            }
        }

        protected void btnEQUnRealizedSearch_Click(object sender, EventArgs e)
        {
            hdnUnRealizedScipFilter.Value = "";

            string TabberScript = "<script language='javascript'>document.getElementById('dvEquityPortfolioUnrealized').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

            TextBox txtScripName = GetEQUnRealizedScripNameTextBox();

            if (txtScripName != null)
            {
                hdnUnRealizedScipFilter.Value = txtScripName.Text.Trim();
                LoadEquityPortfolio();
            }
        }

        private TextBox GetEQPortfolioScripNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvEquityPortfolio.HeaderRow != null)
            {
                if ((TextBox)gvEquityPortfolio.HeaderRow.FindControl("txtScripNameSearch") != null)
                {
                    txt = (TextBox)gvEquityPortfolio.HeaderRow.FindControl("txtScripNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetEQRealizedScripNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvEquityPortfolioDelivery.HeaderRow != null)
            {
                if ((TextBox)gvEquityPortfolioDelivery.HeaderRow.FindControl("txtScripNameRealizedSearch") != null)
                {
                    txt = (TextBox)gvEquityPortfolioDelivery.HeaderRow.FindControl("txtScripNameRealizedSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetEQRealizedSpecScripNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvEquityPortfolioSpeculative.HeaderRow != null)
            {
                if ((TextBox)gvEquityPortfolioSpeculative.HeaderRow.FindControl("txtScripNameRealizedSpecSearch") != null)
                {
                    txt = (TextBox)gvEquityPortfolioSpeculative.HeaderRow.FindControl("txtScripNameRealizedSpecSearch");
                }
            }
            else
            {
                txt = null;
            }
            return txt;
        }

        private TextBox GetEQUnRealizedScripNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvEquityPortfolioUnrealized.HeaderRow != null)
            {
                if ((TextBox)gvEquityPortfolioUnrealized.HeaderRow.FindControl("txtScripNameUnRealizedSearch") != null)
                {
                    txt = (TextBox)gvEquityPortfolioUnrealized.HeaderRow.FindControl("txtScripNameUnRealizedSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {



            gvEquityPortfolio.Columns[0].Visible = false;

            PrepareGridViewForExport(gvEquityPortfolio);
            if (rbtnTry.Checked)
            {
                if (hdnSelectedTab.Value.ToString() == "0")
                {
                    ExportGridView("Excel", "Equity Portfolio", gvEquityPortfolio);
                }
                else if(hdnSelectedTab.Value.ToString() == "1")
                {
                    ExportGridView("Excel", "Equity Portfolio", gvEquityPortfolioDelivery);
                }
                else if (hdnSelectedTab.Value.ToString() == "2")
                {
                    ExportGridView("Excel", "Equity Portfolio", gvEquityPortfolioSpeculative);
                }
                else if (hdnSelectedTab.Value.ToString() == "3")
                {
                    ExportGridView("Excel", "Equity Portfolio", gvEquityPortfolioUnrealized);
                }
            }
            else if (rbtnPDF.Checked)
            {

                ExportGridView("PDF", "EquityPortfolio", gvEquityPortfolio);
            }
            else if (rbtnWord.Checked)
            {
                ExportGridView("Word", "EquityPortfolio", gvEquityPortfolio);
            }

            gvEquityPortfolio.Columns[0].Visible = true;
        }

        private void ExportGridView(string FileType, string title, GridView gv)
        {
            HtmlForm frm = new HtmlForm();
            frm.Controls.Clear();
            frm.Attributes["runat"] = "server";
            if (FileType == "Excel")
            {
                // gvCustomer.Columns.Remove(this.gvCustomer.Columns[0]);

                string temp = customerVo.FirstName + customerVo.LastName + title + ".xls";
                string attachment = "attachment; filename=" + temp;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                Response.Output.Write("<table border=\"0\"><tbody><caption><FONT FACE=\"ARIAL\"  SIZE=\"4\">");
                Response.Output.Write(title + "</FONT></caption><tr><td>");
                Response.Output.Write("Advisor Name : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td>");
                Response.Output.Write(userVo.FirstName + userVo.LastName);
                Response.Output.Write("</td></tr>");
                Response.Output.Write("<tr><td>");
                Response.Output.Write("Customer Name  : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td>");
                Response.Output.Write(customerVo.FirstName + customerVo.MiddleName + customerVo.LastName);
                Response.Output.Write("</td></tr>");
                Response.Output.Write("<tr><td>");
                Response.Output.Write("Contact Person  : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td>");
                Response.Output.Write(rmVo.FirstName + rmVo.MiddleName + rmVo.LastName);
                Response.Output.Write("</td></tr><tr><td>");
                Response.Output.Write("Date : ");
                Response.Output.Write("</td><td>");
                System.DateTime tDate1 = System.DateTime.Now;
                Response.Output.Write(tDate1);
                Response.Output.Write("</td></tr>");
                Response.Output.Write("</tbody></table>");

                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                }
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                }
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                }

                gv.Parent.Controls.Add(frm);
                frm.Controls.Add(gv);
                frm.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();


            }
            else if (FileType == "Word")
            {

                string temp = customerVo.FirstName + customerVo.LastName + title + ".doc";
                string attachment = "attachment; filename=" + temp;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/msword";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                Response.Output.Write("<table border=\"0\"><tbody><caption><FONT FACE=\"ARIAL\"  SIZE=\"4\">");
                Response.Output.Write(title + "</FONT></caption><tr><td>");
                Response.Output.Write("Advisor Name : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td>");
                Response.Output.Write(userVo.FirstName + userVo.LastName);
                Response.Output.Write("</td></tr>");
                Response.Output.Write("<tr><td>");
                Response.Output.Write("Customer Name  : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td>");
                Response.Output.Write(customerVo.FirstName + customerVo.MiddleName + customerVo.LastName);
                Response.Output.Write("</td></tr>");
                Response.Output.Write("<tr><td>");
                Response.Output.Write("Contact Person  : ");
                Response.Output.Write("</td>");
                Response.Output.Write("<td>");
                Response.Output.Write(rmVo.FirstName + rmVo.MiddleName + rmVo.LastName);
                Response.Output.Write("</td></tr><tr><td>");
                Response.Output.Write("Date : ");
                Response.Output.Write("</td><td>");
                System.DateTime tDate1 = System.DateTime.Now;
                Response.Output.Write(tDate1);
                Response.Output.Write("</td></tr>");
                Response.Output.Write("</tbody></table>");
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                }
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                }
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                }

                gv.Parent.Controls.Add(frm);
                frm.Controls.Add(gv);
                frm.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else if (FileType == "PDF")
            {
                string temp = customerVo.FirstName + customerVo.LastName + title;
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gv.Columns.Count - 1);
                table.HeaderRows = 4;
                iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
                Phrase phApplicationName = new Phrase("WWW.PrincipalConsulting.net", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                PdfPCell clApplicationName = new PdfPCell(phApplicationName);
                clApplicationName.Border = PdfPCell.NO_BORDER;
                clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;


                Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
                PdfPCell clDate = new PdfPCell(phDate);
                clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
                clDate.Border = PdfPCell.NO_BORDER;


                headerTable.AddCell(clApplicationName);
                headerTable.AddCell(clDate);
                headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

                PdfPCell cellHeader = new PdfPCell(headerTable);
                cellHeader.Border = PdfPCell.NO_BORDER;
                cellHeader.Colspan = gv.Columns.Count - 1;
                table.AddCell(cellHeader);

                Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                PdfPCell clHeader = new PdfPCell(phHeader);
                clHeader.Colspan = gv.Columns.Count - 1;
                clHeader.Border = PdfPCell.NO_BORDER;
                clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(clHeader);


                Phrase phSpace = new Phrase("\n");
                PdfPCell clSpace = new PdfPCell(phSpace);
                clSpace.Border = PdfPCell.NO_BORDER;
                clSpace.Colspan = gv.Columns.Count - 1;
                table.AddCell(clSpace);

                GridViewRow HeaderRow = gv.HeaderRow;
                if (HeaderRow != null)
                {
                    string cellText = "";
                    for (int j = 1; j < gv.Columns.Count; j++)
                    {

                        if (j == 2)
                        {
                            cellText = "Scrip Name";
                        }
                        else if (j == 3)
                        {
                            if (title == "EquityPortfolioUnRealized")
                            {
                                cellText = "Number of Shares";
                            }
                            else if (title == "EquityPortfolio")
                            {
                                cellText = "Number of Shares";
                            }
                            else if (title == "EquityPortfolioRealized")
                            {
                                cellText = "Number of shares sold";
                            }
                            else if (title == "EquityPortfolioRealizedSpeculative")
                            {
                                cellText = "Number of shares sold";
                            }

                        }
                        else if (j == 4)
                        {
                            if (title == "EquityPortfolioUnRealized")
                            {
                                cellText = "Average Price";
                            }
                            else if (title == "EquityPortfolio")
                            {
                                cellText = "Average Price";
                            }
                            else if (title == "EquityPortfolioRealized")
                            {
                                cellText = "Sale Proceeds (Rs)";
                            }
                            else if (title == "EquityPortfolioRealizedSpeculative")
                            {
                                cellText = "Sale Proceeds (Rs)";
                            }
                        }
                        else if (j == 5)
                        {
                            if (title == "EquityPortfolioUnRealized")
                            {
                                cellText = "Cost of Purchase (Rs)";
                            }
                            else if (title == "EquityPortfolio")
                            {
                                cellText = "Cost of Purchase (Rs)";
                            }
                            else if (title == "EquityPortfolioRealized")
                            {
                                cellText = "Cost of Sales (Rs)";
                            }
                            else if (title == "EquityPortfolioRealizedSpeculative")
                            {
                                cellText = "Cost of Sales (Rs)";
                            }
                        }
                        else if (j == 6)
                        {
                            if (title == "EquityPortfolioUnRealized")
                            {
                                cellText = "Current Price (Rs)";
                            }
                            else if (title == "EquityPortfolio")
                            {
                                cellText = "Current Price (Rs)";
                            }
                            else if (title == "EquityPortfolioRealized")
                            {
                                cellText = "Realized P/L (Rs)";
                            }
                            else if (title == "EquityPortfolioRealizedSpeculative")
                            {
                                cellText = "Realized P/L (Rs)";
                            }
                        }
                        else if (j == 7)
                        {
                            if (title == "EquityPortfolioUnRealized")
                            {
                                cellText = "Current Value (Rs)";
                            }
                            else if (title == "EquityPortfolio")
                            {
                                cellText = "Current Value (Rs)";
                            }
                            else if (title == "EquityPortfolioRealized")
                            {
                                cellText = "XIRR";
                            }
                            else if (title == "EquityPortfolioRealizedSpeculative")
                            {
                                cellText = "XIRR";
                            }
                        }
                        else if (j == 8)
                        {
                            if (title == "EquityPortfolioUnRealized")
                            {
                                cellText = "UnRealized P/L (Rs)";
                            }
                            else if (title == "EquityPortfolio")
                            {
                                cellText = "UnRealized P/L (Rs)";
                            }
                        }
                        else if (j == 9)
                        {
                            if (title == "EquityPortfolio")
                            {
                                cellText = "Realized P/L (Rs)";
                            }
                            else if (title == "EquityPortfolioUnRealized")
                            {
                                cellText = "XIRR";
                            }
                        }
                        else if (j == 10)
                        {
                            if (title == "EquityPortfolio")
                            {
                                cellText = "XIRR";
                            }
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(gv.HeaderRow.Cells[j].Text);
                        }
                        Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
                        table.AddCell(ph);
                    }

                }

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    string cellText = "";
                    if (gv.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        for (int j = 1; j < gv.Columns.Count; j++)
                        {
                            if (j == 1)
                            {
                                cellText = (i + 1).ToString();
                            }
                            else if (j == 2)
                            {
                                if (title == "EquityPortfolioUnRealized")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblScripNameUnRealizedHeader")).Text;
                                }
                                else if (title == "EquityPortfolio")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblScripNameHeader")).Text;
                                }
                                else if (title == "EquityPortfolioRealized")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblScripNameRealizedHeader")).Text;
                                }
                                else if (title == "EquityPortfolioRealizedSpeculative")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblScripNameRealizedSpecHeader")).Text;
                                }

                            }
                            else
                            {
                                cellText = Server.HtmlDecode(gv.Rows[i].Cells[j].Text);
                            }

                            Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
                            iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                            table.AddCell(ph);

                        }

                    }

                }

                //Create the PDF Document

                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                pdfDoc.NewPage();
                pdfDoc.Add(table);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                temp = "filename=" + temp + ".pdf";
                //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
                Response.AddHeader("content-disposition", "attachment;" + temp);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();



            }
        }
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedValue.ToString()));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }
                else if (current is TextBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as TextBox).Text));
                }
                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }



        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(TextBox))
                {
                    l.Text = (gv.Controls[i] as TextBox).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                }

                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {

                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                }
                if (gv.Controls[i].HasControls())
                {
                    PrepareGridViewForExport(gv.Controls[i]);
                }

            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            gvEquityPortfolio.Columns[0].Visible = false;
            if (gvEquityPortfolio.HeaderRow != null)
            {
                PrepareControlForExport(gvEquityPortfolio.HeaderRow);
            }
            foreach (GridViewRow row in gvEquityPortfolio.Rows)
            {
                PrepareControlForExport(row);
            }
            if (gvEquityPortfolio.FooterRow != null)
            {
                PrepareControlForExport(gvEquityPortfolio.FooterRow);
            }

            PrepareGridViewForExport(gvEquityPortfolio);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewEquityPortfolios_dvEquityPortfolio','ctrl_ViewEquityPortfolios_btnPrintGrid');", true);
        }

        protected void btnPrintGrid_Click(object sender, EventArgs e)
        {

            gvEquityPortfolio.Columns[0].Visible = true;

        }

        protected void btnExportUnrealized_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioUnrealized.Columns[0].Visible = false;
            PrepareGridViewForExport(gvEquityPortfolioUnrealized);
            if (rbtnUnrealExcel.Checked)
            {
                ExportGridView("Excel", "EquityPortfolioUnRealized", gvEquityPortfolioUnrealized);
            }
            else if (rbtnUnrealPDF.Checked)
            {

                ExportGridView("PDF", "EquityPortfolioUnRealized", gvEquityPortfolioUnrealized);
            }
            else if (rbtnUnrealWord.Checked)
            {
                ExportGridView("Word", "EquityPortfolioUnRealized", gvEquityPortfolioUnrealized);
            }

            gvEquityPortfolioUnrealized.Columns[0].Visible = true;
        }

        protected void btnExportRealizedSpeculative_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioSpeculative.Columns[0].Visible = false;
            PrepareGridViewForExport(gvEquityPortfolioSpeculative);
            if (rbtnSpecExcel.Checked)
            {
                ExportGridView("Excel", "EquityPortfolioRealizedSpeculative", gvEquityPortfolioSpeculative);
            }
            else if (rbtnSpecPdf.Checked)
            {

                ExportGridView("PDF", "EquityPortfolioRealizedSpeculative", gvEquityPortfolioSpeculative);
            }
            else if (rbtnSpecWord.Checked)
            {
                ExportGridView("Word", "EquityPortfolioRealizedSpeculative", gvEquityPortfolioSpeculative);
            }

            gvEquityPortfolioSpeculative.Columns[0].Visible = true;
        }

        protected void btnExportRealizedDelivery_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioDelivery.Columns[0].Visible = false;
            PrepareGridViewForExport(gvEquityPortfolioDelivery);
            if (rbtnDeliveryExcel.Checked)
            {
                ExportGridView("Excel", "EquityPortfolioRealized", gvEquityPortfolioDelivery);
            }
            else if (rbtnDeliveryPdf.Checked)
            {

                ExportGridView("PDF", "EquityPortfolioRealized", gvEquityPortfolioDelivery);
            }
            else if (rbtnDeliveryWord.Checked)
            {
                ExportGridView("Word", "EquityPortfolioRealized", gvEquityPortfolioDelivery);
            }

            gvEquityPortfolioDelivery.Columns[0].Visible = true;
        }

        protected void btnPrintUnrealized_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioUnrealized.Columns[0].Visible = false;
            if (gvEquityPortfolioUnrealized.HeaderRow != null)
            {
                PrepareControlForExport(gvEquityPortfolioUnrealized.HeaderRow);
            }
            foreach (GridViewRow row in gvEquityPortfolioUnrealized.Rows)
            {
                PrepareControlForExport(row);
            }
            if (gvEquityPortfolioUnrealized.FooterRow != null)
            {
                PrepareControlForExport(gvEquityPortfolioUnrealized.FooterRow);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewEquityPortfolios_dvEquityPortfolioUnrealized','ctrl_ViewEquityPortfolios_btnPrintUnrealizedGrid');", true);

        }

        protected void btnPrintRealizedDelivery_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioDelivery.Columns[0].Visible = false;
            if (gvEquityPortfolioDelivery.HeaderRow != null)
            {
                PrepareControlForExport(gvEquityPortfolioDelivery.HeaderRow);
            }
            foreach (GridViewRow row in gvEquityPortfolioDelivery.Rows)
            {
                PrepareControlForExport(row);
            }
            if (gvEquityPortfolioDelivery.FooterRow != null)
            {
                PrepareControlForExport(gvEquityPortfolioDelivery.FooterRow);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewEquityPortfolios_dvEquityPortfolioDelivery','ctrl_ViewEquityPortfolios_btnPrintRealizedDeliveryGrid');", true);
        }

        protected void btnPrintRealizedSpeculative_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioSpeculative.Columns[0].Visible = false;
            if (gvEquityPortfolioSpeculative.HeaderRow != null)
            {
                PrepareControlForExport(gvEquityPortfolioSpeculative.HeaderRow);
            }
            foreach (GridViewRow row in gvEquityPortfolioSpeculative.Rows)
            {
                PrepareControlForExport(row);
            }
            if (gvEquityPortfolioSpeculative.FooterRow != null)
            {
                PrepareControlForExport(gvEquityPortfolioSpeculative.FooterRow);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewEquityPortfolios_dvEquityPortfolioSpeculative','ctrl_ViewEquityPortfolios_btnPrintRealizedSpeculativeGrid');", true);
        }

        protected void btnPrintRealizedDeliveryGrid_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioDelivery.Columns[0].Visible = true;
        }

        protected void btnPrintRealizedSpeculativeGrid_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioSpeculative.Columns[0].Visible = true;
        }

        protected void btnPrintUnrealizedGrid_Click(object sender, EventArgs e)
        {
            gvEquityPortfolioUnrealized.Columns[0].Visible = true;
        }

    }
}
