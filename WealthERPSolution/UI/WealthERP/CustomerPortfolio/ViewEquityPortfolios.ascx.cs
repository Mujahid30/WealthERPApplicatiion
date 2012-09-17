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
        double costofpurchase_all = 0;
        double costofpurchase_Unrealized = 0;
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
                    eqPortfolioList = customerPortfolioBo.GetCustomerEquityPortfolio(customerVo.CustomerId, portfolioId, tradeDate, hdnScipNameFilter.Value.Trim(), string.Empty);
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
                    //imgBtnExport1.Visible = true;
                    gvEquityPortfolio.DataBind();
                    //tblDelivery.Visible = false;
                    //tblPortfolio.Visible = false;
                    //tblSpec.Visible = false;
                    //tblUnrealized.Visible = false;
                }
                else
                {
                    lblMessage.Visible = false;
                    lblMessageD.Visible = false;
                    lblMessageSpeculative.Visible = false;
                    lblMessageUnrealized.Visible = false;
                    //tblDelivery.Visible = true;
                    //tblPortfolio.Visible = true;
                    //tblSpec.Visible = true;
                    //tblUnrealized.Visible = true;
                    DataTable dtEqPortfolio = new DataTable();
                    DataTable dtEqPortfolioDelivery = new DataTable();
                    DataTable dtEqPortfolioSpeculative = new DataTable();
                    DataTable dtEqPortfolioUnrealized = new DataTable();

                    // Consolidated View Datatable
                    dtEqPortfolio.Columns.Add("Sl.No.");
                    dtEqPortfolio.Columns.Add("CompanyName");
                    dtEqPortfolio.Columns.Add("Quantity",typeof(double));
                    dtEqPortfolio.Columns.Add("AveragePrice", typeof(double));
                    dtEqPortfolio.Columns.Add("CostOfPurchase", typeof(double));
                    dtEqPortfolio.Columns.Add("MarketPrice", typeof(double));
                    dtEqPortfolio.Columns.Add("CurrentValue", typeof(double));
                    dtEqPortfolio.Columns.Add("UnRealizedPL", typeof(double));
                    dtEqPortfolio.Columns.Add("RealizedPL", typeof(double));
                    dtEqPortfolio.Columns.Add("XIRR", typeof(double));

                    //Delivery Based View Datatable
                    dtEqPortfolioDelivery.Columns.Add("Sl.No.");
                    dtEqPortfolioDelivery.Columns.Add("CompanyName");
                    dtEqPortfolioDelivery.Columns.Add("SaleQty", typeof(double));
                    dtEqPortfolioDelivery.Columns.Add("CostOfSales", typeof(double));
                    dtEqPortfolioDelivery.Columns.Add("RealizedSalesProceeds", typeof(double));
                    dtEqPortfolioDelivery.Columns.Add("RealizedPL", typeof(double));
                    dtEqPortfolioDelivery.Columns.Add("XIRR", typeof(double));

                    //Speculative Based View Datatable
                    dtEqPortfolioSpeculative.Columns.Add("Sl.No.");
                    dtEqPortfolioSpeculative.Columns.Add("CompanyName");
                    dtEqPortfolioSpeculative.Columns.Add("SaleQty", typeof(double));
                    dtEqPortfolioSpeculative.Columns.Add("RealizedSalesProceeds", typeof(double));
                    dtEqPortfolioSpeculative.Columns.Add("CostOfSales", typeof(double));
                    dtEqPortfolioSpeculative.Columns.Add("RealizedPL", typeof(double));
                    dtEqPortfolioSpeculative.Columns.Add("XIRR", typeof(double));

                    //Unrealized View Datatable
                    dtEqPortfolioUnrealized.Columns.Add("Sl.No.");
                    dtEqPortfolioUnrealized.Columns.Add("CompanyName");
                    dtEqPortfolioUnrealized.Columns.Add("Quantity", typeof(double));
                    dtEqPortfolioUnrealized.Columns.Add("AveragePrice", typeof(double));
                    dtEqPortfolioUnrealized.Columns.Add("CostOfPurchase", typeof(double));
                    dtEqPortfolioUnrealized.Columns.Add("MarketPrice", typeof(double));
                    dtEqPortfolioUnrealized.Columns.Add("CurrentValue", typeof(double));
                    dtEqPortfolioUnrealized.Columns.Add("UnRealizedPL", typeof(double));
                    dtEqPortfolioUnrealized.Columns.Add("XIRR", typeof(double));

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
                            costofpurchase_all = costofpurchase_all + eqPortfolioVo.CostOfPurchase;
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
                                costofpurchase_Unrealized = costofpurchase_Unrealized + eqPortfolioVo.CostOfPurchase;
                                drEqPortfolioUnrealized[4] = double.Parse(eqPortfolioVo.CostOfPurchase.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
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
                        hdnNoOfRecords.Value = dtEqPortfolio.Rows.Count.ToString();
                        gvEquityPortfolio.DataSource = dtEqPortfolio;
                        gvEquityPortfolio.DataBind();
                        gvEquityPortfolio.Visible = true;
                        imgBtnExport3.Visible = true;

                        if (Cache["gvEquityPortfolioDetails" + customerPortfolioVo.CustomerId.ToString()] == null)
                        {
                            Cache.Insert("gvEquityPortfolioDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolio);
                        }
                        else
                        {
                            Cache.Remove("gvEquityPortfolioDetails" + customerPortfolioVo.CustomerId.ToString());
                            Cache.Insert("gvEquityPortfolioDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolio);
                        }
                    }
                    else
                    {
                        gvEquityPortfolio.DataSource = null;
                        gvEquityPortfolio.DataBind();
                        lblMessage.Visible = true;
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
                        hdnNoOfRecords.Value = dtEqPortfolioDelivery.Rows.Count.ToString();
                        gvEquityPortfolioDelivery.DataSource = dtEqPortfolioDelivery;
                        gvEquityPortfolioDelivery.DataBind();
                        gvEquityPortfolioDelivery.Visible = true;
                        imgBtnExport1.Visible = true;
                        if (Cache["gvEquityPortfolioDeliveryDetails" + customerPortfolioVo.CustomerId.ToString()] == null)
                        {
                            Cache.Insert("gvEquityPortfolioDeliveryDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolioDelivery);
                        }
                        else
                        {
                            Cache.Remove("gvEquityPortfolioDeliveryDetails" + customerPortfolioVo.CustomerId.ToString());
                            Cache.Insert("gvEquityPortfolioDeliveryDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolioDelivery);
                        }
                    }
                    else
                    {
                        gvEquityPortfolioDelivery.DataSource = null;
                        gvEquityPortfolioDelivery.DataBind();
                        lblMessageD.Visible = true;
                    }
                   
                    // Bind EQ Speculative Details
                    if (hdnRealizedSpecScipFilter.Value.Trim() != "")
                    {
                        SearchExpression = "CompanyName like '%" + hdnRealizedSpecScipFilter.Value.Trim() + "%'";
                        dtEqPortfolioSpeculative.DefaultView.RowFilter = SearchExpression;
                    }

                    if (dtEqPortfolioSpeculative.Rows.Count > 0)
                    {
                        hdnNoOfRecords.Value = dtEqPortfolioSpeculative.Rows.Count.ToString();
                        gvEquityPortfolioSpeculative.DataSource = dtEqPortfolioSpeculative;
                        gvEquityPortfolioSpeculative.DataBind();
                        gvEquityPortfolioSpeculative.Visible = true;
                        imgBtnExport2.Visible = true;

                        if (Cache["gvEquityPortfolioSpeculativeDetails" + customerPortfolioVo.CustomerId.ToString()] == null)
                        {
                            Cache.Insert("gvEquityPortfolioSpeculativeDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolioSpeculative);
                        }
                        else
                        {
                            Cache.Remove("gvEquityPortfolioSpeculativeDetails" + customerPortfolioVo.CustomerId.ToString());
                            Cache.Insert("gvEquityPortfolioSpeculativeDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolioSpeculative);
                        }
                    }
                    else
                    {
                        gvEquityPortfolioSpeculative.DataSource = null;
                        gvEquityPortfolioSpeculative.DataBind();
                        lblMessageSpeculative.Visible = true;
                    }

                    // Bind EQ Unrealized Details
                    if (hdnUnRealizedScipFilter.Value.Trim() != "")
                    {
                        SearchExpression = "CompanyName like '%" + hdnUnRealizedScipFilter.Value.Trim() + "%'";
                        dtEqPortfolioUnrealized.DefaultView.RowFilter = SearchExpression;
                    }

                    if (dtEqPortfolioUnrealized.Rows.Count > 0)
                    {
                        hdnNoOfRecords.Value = dtEqPortfolioUnrealized.Rows.Count.ToString();
                        gvEquityPortfolioUnrealized.DataSource = dtEqPortfolioUnrealized;
                        gvEquityPortfolioUnrealized.DataBind();
                        gvEquityPortfolioUnrealized.Visible = true;
                        imgBtnExport.Visible = true;

                        if (Cache["gvEquityPortfolioUnrealizedDetails" + customerPortfolioVo.CustomerId.ToString()] == null)
                        {
                            Cache.Insert("gvEquityPortfolioUnrealizedDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolioUnrealized);
                        }
                        else
                        {
                            Cache.Remove("gvEquityPortfolioUnrealizedDetails" + customerPortfolioVo.CustomerId.ToString());
                            Cache.Insert("gvEquityPortfolioUnrealizedDetails" + customerPortfolioVo.CustomerId.ToString(), dtEqPortfolioUnrealized);
                        }
                    }
                    else
                    {
                        gvEquityPortfolioUnrealized.DataSource = null;
                        gvEquityPortfolioUnrealized.DataBind();
                        lblMessageUnrealized.Visible = true;
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
        #region Equity Portfolio Consolidated Grid View Methods     

        protected void gvEquityPortfolio_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                if (e.CommandName.ToString() != "Filter")
                {
                    if ((GridDataItem)e.Item.DataItem != null)
                    {
                        GridDataItem gvr = (GridDataItem)e.Item;
                        int selectedRow = gvr.ItemIndex + 1;
                        int slNo = Convert.ToInt32(gvr.GetDataKeyValue("Sl.No."));

                        //int slNo = int.Parse(gvEquityPortfolio.DataKeys[index].Value.ToString());
                        Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                        Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];

                        if (e.CommandName == "Select")
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolio_RowCommand()");
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
        protected void gvEquityPortfolioDelivery_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = decimal.Parse(currentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                if (e.CommandName.ToString() != "Filter")
                {
                    if ((GridDataItem)e.Item.DataItem != null)
                    {
                        GridDataItem gvr = (GridDataItem)e.Item;
                        int selectedRow = gvr.ItemIndex + 1;
                        int slNo = Convert.ToInt32(gvr.GetDataKeyValue("Sl.No."));

                        //int slNo = int.Parse(gvEquityPortfolioDelivery.DataKeys[index].Value.ToString());
                        Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                        Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                        if (e.CommandName == "Select")
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolio_RowCommand()");
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
        protected void gvEquityPortfolioSpeculative_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currentValue.ToString()));

                if (e.CommandName.ToString() != "Filter")
                {
                    if ((GridDataItem)e.Item.DataItem != null)
                    {
                        GridDataItem gvr = (GridDataItem)e.Item;
                        int selectedRow = gvr.ItemIndex + 1;
                        int slNo = Convert.ToInt32(gvr.GetDataKeyValue("Sl.No."));


                        //int slNo = int.Parse(gvEquityPortfolioSpeculative.DataKeys[index].Value.ToString());
                        Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                        Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                        if (e.CommandName == "Select")
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvEquityPortfolioSpeculative_RowCommand()");
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
        protected void gvEquityPortfolioUnrealized_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                LoadEquityPortfolio();
                lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currentValue.ToString()));

                if (e.CommandName.ToString() != "Filter")
                {
                    if ((GridDataItem)e.Item.DataItem != null)
                    {
                        GridDataItem gvr = (GridDataItem)e.Item;
                        int selectedRow = gvr.ItemIndex + 1;
                        int slNo = Convert.ToInt32(gvr.GetDataKeyValue("Sl.No."));

                        //int slNo = int.Parse(gvEquityPortfolioUnrealized.DataKeys[index].Value.ToString());
                        Session["EquityPortfolioTransactionList"] = eqPortfolioList[slNo - 1].EQPortfolioTransactionVo;
                        Session["EquityPortfolio"] = eqPortfolioList[slNo - 1];


                        if (e.CommandName == "Select")
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewEquityPortfolioTransactions','none');", true);
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
        #region export option
        public void btngvEquityPortfolioUnrealizedExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvEquityPortfolioUnrealized.ExportSettings.OpenInNewWindow = true;
            gvEquityPortfolioUnrealized.ExportSettings.IgnorePaging = true;
            gvEquityPortfolioUnrealized.ExportSettings.HideStructureColumns = true;
            gvEquityPortfolioUnrealized.ExportSettings.ExportOnlyData = true;
            gvEquityPortfolioUnrealized.ExportSettings.FileName = "Equity Portfolio Unrealized Details";
            gvEquityPortfolioUnrealized.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvEquityPortfolioUnrealized.MasterTableView.ExportToExcel();
        }
        public void btngvEquityPortfolioDeliveryExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvEquityPortfolioDelivery.ExportSettings.OpenInNewWindow = true;
            gvEquityPortfolioDelivery.ExportSettings.IgnorePaging = true;
            gvEquityPortfolioDelivery.ExportSettings.HideStructureColumns = true;
            gvEquityPortfolioDelivery.ExportSettings.ExportOnlyData = true;
            gvEquityPortfolioDelivery.ExportSettings.FileName = "Equity Portfolio Delivery Details";
            gvEquityPortfolioDelivery.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvEquityPortfolioDelivery.MasterTableView.ExportToExcel();
        }
        public void btngvEquityPortfolioSpeculativeExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvEquityPortfolioSpeculative.ExportSettings.OpenInNewWindow = true;
            gvEquityPortfolioSpeculative.ExportSettings.IgnorePaging = true;
            gvEquityPortfolioSpeculative.ExportSettings.HideStructureColumns = true;
            gvEquityPortfolioSpeculative.ExportSettings.ExportOnlyData = true;
            gvEquityPortfolioSpeculative.ExportSettings.FileName = "Equity Portfolio Speculative Details";
            gvEquityPortfolioSpeculative.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvEquityPortfolioSpeculative.MasterTableView.ExportToExcel();
        }
        public void btngvEquityPortfolioExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvEquityPortfolio.ExportSettings.OpenInNewWindow = true;
            gvEquityPortfolio.ExportSettings.IgnorePaging = true;
            gvEquityPortfolio.ExportSettings.HideStructureColumns = true;
            gvEquityPortfolio.ExportSettings.ExportOnlyData = true;
            gvEquityPortfolio.ExportSettings.FileName = "Equity Portfolio Details";
            gvEquityPortfolio.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvEquityPortfolio.MasterTableView.ExportToExcel();
        }
        #endregion

        #region needdatasource
        protected void gvEquityPortfolio_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEquityPortfolioDetails = new DataTable();
            dtgvEquityPortfolioDetails = (DataTable)Cache["gvEquityPortfolioDetails" + customerVo.CustomerId.ToString()];
            gvEquityPortfolio.DataSource = dtgvEquityPortfolioDetails;
        }
        protected void gvEquityPortfolioSpeculative_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEquityPortfolioSpeculativeDetails = new DataTable();
            dtgvEquityPortfolioSpeculativeDetails = (DataTable)Cache["gvEquityPortfolioSpeculativeDetails" + customerVo.CustomerId.ToString()];
            gvEquityPortfolioSpeculative.DataSource = dtgvEquityPortfolioSpeculativeDetails;
        }
        protected void gvEquityPortfolioDelivery_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEquityPortfolioDeliveryDetails = new DataTable();
            dtgvEquityPortfolioDeliveryDetails = (DataTable)Cache["gvEquityPortfolioDeliveryDetails" + customerVo.CustomerId.ToString()];
            gvEquityPortfolioDelivery.DataSource = dtgvEquityPortfolioDeliveryDetails;
        }
        protected void gvEquityPortfolioUnrealized_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtgvEquityPortfolioUnrealizedDetails = new DataTable();
            dtgvEquityPortfolioUnrealizedDetails = (DataTable)Cache["gvEquityPortfolioUnrealizedDetails" + customerVo.CustomerId.ToString()];
            gvEquityPortfolioUnrealized.DataSource = dtgvEquityPortfolioUnrealizedDetails;
        }
        #endregion

    }
}
