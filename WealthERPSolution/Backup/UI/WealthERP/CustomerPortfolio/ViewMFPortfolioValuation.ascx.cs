using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VoCustomerPortfolio;
using VoUser;
using BoCustomerPortfolio;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections;
using WealthERP.Base;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class ViewMFPortfolioValuation : System.Web.UI.UserControl
    {
        int index;
        MFPortfolioVo mfPortfolioVo;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<DividendTaggingPortfolioVo> dividendTaggingPortfolioVoList;
        CustomerVo customerVo;
        UserVo userVo;
        DividendTaggingPortfolioVo dividendTaggingPortfolioVo;

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        // DateTime tradeDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        DateTime tradeDate = new DateTime();
       
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
        static int portfolioId;
        RMVo rmVo = new RMVo();
        public double totalUnRealizedValue = 0;
        decimal temp = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["customerVo"];
                userVo = (UserVo)Session["userVo"];
                rmVo = (RMVo)Session["rmVo"];
                if (Session["ValuationDate"] == null)
                    GetLatestValuationDate();
                genDict = (Dictionary<string, DateTime>)Session["ValuationDate"];
                lblMFDate.Text = "Valuation as on  " + DateTime.Parse(genDict["MFDate"].ToString()).ToLongDateString();

                if (Session["folioNum"] != null)
                {
                    hdnFolioFilter.Value = Session["folioNum"].ToString();
                }

                //  dividendTaggingPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                if (!IsPostBack)
                {
                    portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    LoadMFPortfolio();
                    //



                    //  decimal.Parse(drResult["InsuranceAggr"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    //lblCostOfAcqusition.Text = decimal.Parse(CostOfAcquisition.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    //lblCurrentHolding.Text = String.Format("{0:n4}", decimal.Parse(currentHoldings.ToString()));
                    //lblCurrentValue.Text = decimal.Parse(currenValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
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
                FunctionInfo.Add("Method", "ViewMutualFundPortfolio.ascx:Page_Load()");
                object[] objects = new object[3];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = dividendTaggingPortfolioVo;
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
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            ddlPortfolio.SelectedValue = portfolioId.ToString();

        }
        public void LoadMFPortfolio()
        {
            //hdnSelectedCategory.Value = "";
            //hdnRealizedSelectedCategory.Value="";
            //hdnAllSelectedCategory.Value = "";
            bool containsBrockerChange = false;
            string categorySearch = "";
            int count = 0;
            lblMessageForCategoryNotional.Visible = false;
            lnkGetBackNotionalLink.Visible = false;
            lblMessageRealized.Visible = false;
            lnkGoBackRealized.Visible = false;
            gvMFPortfolio.DataSource = null;
            gvMFPortfolio.DataBind();
            gvMFPortfolioNotional.DataSource = null;
            gvMFPortfolioNotional.DataBind();
            gvMFPortfolioRealized.DataSource = null;
            gvMFPortfolioRealized.DataBind();
            try
            {
                genDict = (Dictionary<string, DateTime>)Session["ValuationDate"];
                tradeDate = DateTime.Parse(genDict["MFDate"].ToString());
                if (tradeDate == DateTime.MinValue)
                {
                    count = 0;
                }
                else
                {
                    if (hdnSelectedCategory.Value == "All")
                        categorySearch = "";
                    else
                        categorySearch = hdnSelectedCategory.Value;
                    dividendTaggingPortfolioVoList = customerPortfolioBo.GetCustomerMFPortfolioDivTagging(customerVo.CustomerId, portfolioId, tradeDate, hdnSchemeFilter.Value.Trim(), hdnFolioFilter.Value.Trim(), categorySearch);
                    //BindCategoryDropDown(dividendTaggingPortfolioVoList);
                    Session["dividendTaggingPortfolioVoList"] = dividendTaggingPortfolioVoList;
                    if (dividendTaggingPortfolioVoList != null)
                    {
                        count = dividendTaggingPortfolioVoList.Count;
                    }
                }
                if (count == 0)
                {
                    lblMessageAll.Visible = true;
                    lblMessageNotional.Visible = true;
                    lblMessageRealized.Visible = true;
                    gvMFPortfolio.DataSource = null;
                    gvMFPortfolio.DataBind();



                    gvMFPortfolioRealized.DataSource = null;
                    gvMFPortfolioRealized.DataBind();
                    gvMFPortfolioNotional.DataSource = null;
                    gvMFPortfolioNotional.DataBind();

                   
                    hdnFolioFilter.Value = "";
                    hdnSchemeFilter.Value = "";
                    hdnSelectedCategory.Value = "All";
                    btnPortfolioSearch.Focus();
                }
                else
                {
                    lblMessageAll.Visible = false;
                    lblMessageNotional.Visible = false;
                    lblMessageRealized.Visible = false;

                  
                    DataTable dtMFPortfolio = new DataTable();
                    DataTable dtMFPortfolioRealized = new DataTable();
                    DataTable dtMFPortfolioNotional = new DataTable();

                    //Portfolio All Datatable
                    AllDataTableCreation(dtMFPortfolio);

                    //Portfolio realized Datatable
                    RealizedDataTableCreation(dtMFPortfolioRealized);

                    //Portfolio Notional Datatable
                    NotionalDataTableCreation(dtMFPortfolioNotional);

                    DataRow drMFPortfolio;
                    DataRow drMFPortfolioRealized;
                    DataRow drMFPortfolioNotional;
                    double unrealizedPLSum = 0;
                   
                  
                    for (int i = 0; i < dividendTaggingPortfolioVoList.Count; i++)
                    {
                        dividendTaggingPortfolioVo = new DividendTaggingPortfolioVo();
                        dividendTaggingPortfolioVo = dividendTaggingPortfolioVoList[i];
                        //Portfolio All
                        # region Portfolio All
                        if (dividendTaggingPortfolioVo != null)
                        {

                            drMFPortfolio = dtMFPortfolio.NewRow();
                            drMFPortfolio = PortfolioDetails(drMFPortfolio, i);
                            if (drMFPortfolio["CurrentHoldings"].ToString() == "0.00")
                            {
                                dtMFPortfolio.Rows.InsertAt(drMFPortfolio, dtMFPortfolio.Rows.Count);

                            }
                            else
                            {
                                dtMFPortfolio.Rows.InsertAt(drMFPortfolio, 0);
                            }

                        }
                        # endregion
                        //Portfolio Realized
                        # region Portfolio Realized
                        if (dividendTaggingPortfolioVo.TotalUnitsSold != 0.0)
                        {
                            drMFPortfolioRealized = dtMFPortfolioRealized.NewRow();
                            drMFPortfolioRealized = RealizedDetails(drMFPortfolioRealized, dtMFPortfolioRealized, i);
                            dtMFPortfolioRealized.Rows.Add(drMFPortfolioRealized);

                        }
                        # endregion
                        //Portfolio Notional
                        # region Notional
                        if (dividendTaggingPortfolioVo.OutstandingUnits != 0)
                        {
                            drMFPortfolioNotional = dtMFPortfolioNotional.NewRow();
                            drMFPortfolioNotional = NotionalDetails(dividendTaggingPortfolioVo, drMFPortfolioNotional, dtMFPortfolioNotional, i);
                            dtMFPortfolioNotional.Rows.Add(drMFPortfolioNotional);
                        }
                        # endregion
                    }
                    // Bind Portfolio Grid Details
                    # region Bind Portfolio Grid Details
                    //I have did some mess up, because i dont find any other way rather than this.If any one
                    // find any thing that we can reduce this  then please do that,but what i did here is ,
                    //According to Business people specs is when Current holdings value is 0.0 then it needs
                    //to be shown at last. So i created two loops that checks a logic.


                    if (dtMFPortfolio.Rows.Count > 0)
                    {
                        gvMFPortfolio.Visible = true;
                        gvMFPortfolio.DataSource = dtMFPortfolio;
                        gvMFPortfolio.DataBind();

                    }
                    else
                    {
                        gvMFPortfolio.DataSource = null;
                        gvMFPortfolio.DataBind();
                        lblMessageAll.Visible = true;
                    }


                    TextBox txtSchemeName = GetPortfolioSchemeNameTextBox();
                    TextBox txtFolio = GetPortfolioFolioTextBox();

                    if (txtSchemeName != null && txtFolio != null)
                    {
                        if (hdnSchemeFilter.Value != "")
                        {
                            txtSchemeName.Text = hdnSchemeFilter.Value.ToString().Trim();
                        }
                        if (hdnFolioFilter.Value != "")
                        {
                            txtFolio.Text = hdnFolioFilter.Value.ToString().Trim();
                        }
                    }
                    # endregion
                    // Bind Realized Portfolio Details
                    # region Bind realized Portfolio Details
                    string SearchExpression = string.Empty;

                    if (hdnRealizedSchemeFilter.Value.Trim() != "" && hdnRealizedFolioFilter.Value.Trim() != "")
                    {
                        SearchExpression = "FundDescription like '%" + hdnRealizedSchemeFilter.Value.Trim() + "%' AND FolioNum like '%" + hdnRealizedFolioFilter.Value.Trim() + "%'";
                        dtMFPortfolioRealized.DefaultView.RowFilter = SearchExpression;
                    }
                    else if (hdnRealizedSchemeFilter.Value.Trim() != "" && hdnRealizedFolioFilter.Value.Trim() == "")
                    {
                        SearchExpression = "FundDescription like '%" + hdnRealizedSchemeFilter.Value.Trim() + "%'";
                        dtMFPortfolioRealized.DefaultView.RowFilter = SearchExpression;
                    }
                    else if (hdnRealizedSchemeFilter.Value.Trim() == "" && hdnRealizedFolioFilter.Value.Trim() != "")
                    {
                        SearchExpression = "FolioNum like '%" + hdnRealizedFolioFilter.Value.Trim() + "%'";
                        dtMFPortfolioRealized.DefaultView.RowFilter = SearchExpression;
                    }


                    if (dtMFPortfolioRealized.Rows.Count > 0)
                    {
                        gvMFPortfolioRealized.Visible = true;
                        gvMFPortfolioRealized.DataSource = dtMFPortfolioRealized.DefaultView;
                        gvMFPortfolioRealized.DataBind();

                    }
                    else
                    {
                        lblMessageRealized.Visible = true;
                        gvMFPortfolioRealized.DataSource = null;
                        gvMFPortfolioRealized.DataBind();

                    }

                    TextBox txtRealizedSchemeName = GetRealizedSchemeNameTextBox();
                    TextBox txtRealizedFolio = GetRealizedFolioTextBox();

                    if (txtRealizedSchemeName != null && txtRealizedFolio != null)
                    {
                        if (hdnRealizedSchemeFilter.Value != "")
                        {
                            txtRealizedSchemeName.Text = hdnRealizedSchemeFilter.Value.ToString().Trim();
                        }
                        if (hdnRealizedFolioFilter.Value != "")
                        {
                            txtRealizedFolio.Text = hdnRealizedFolioFilter.Value.ToString().Trim();
                        }
                    }
                    # endregion
                    // Bind Notional Portfolio Details
                    # region Bind Notional Portfolio Details
                    if (dtMFPortfolioNotional.Rows.Count > 0)
                    {
                        lblMessageNotional.Visible = false;

                        string NotionalSearchExpression = string.Empty;

                        if (hdnNotionalSchemeFilter.Value.Trim() != "" && hdnNotionalFolioFilter.Value.Trim() != "")
                        {
                            NotionalSearchExpression = "FundDescription like '%" + hdnNotionalSchemeFilter.Value.Trim() + "%' AND FolioNum like '" + hdnNotionalFolioFilter.Value.Trim() + "%'";
                        }
                        else if (hdnNotionalSchemeFilter.Value.Trim() != "" && hdnNotionalFolioFilter.Value.Trim() == "")
                        {
                            NotionalSearchExpression = "FundDescription like '%" + hdnNotionalSchemeFilter.Value.Trim() + "%'";
                            dtMFPortfolioNotional.DefaultView.RowFilter = NotionalSearchExpression;
                        }
                        else if (hdnNotionalSchemeFilter.Value.Trim() == "" && hdnNotionalFolioFilter.Value.Trim() != "")
                        {
                            NotionalSearchExpression = "FolioNum like '" + hdnNotionalFolioFilter.Value.Trim() + "%'";
                            dtMFPortfolioNotional.DefaultView.RowFilter = NotionalSearchExpression;
                        }

                        if (dtMFPortfolioNotional.Rows.Count > 0)
                        {
                            gvMFPortfolioNotional.Visible = true;
                            gvMFPortfolioNotional.DataSource = dtMFPortfolioNotional.DefaultView;
                            gvMFPortfolioNotional.DataBind();
                        }
                        else
                        {
                            gvMFPortfolioNotional.Visible = true;
                            gvMFPortfolioNotional.DataSource = null;
                            gvMFPortfolioNotional.DataBind();
                            lblMessageNotional.Visible = true;
                        }

                        TextBox txtNotionalSchemeName = GetNotionalSchemeNameTextBox();
                        TextBox txtNotionalFolio = GetNotionalFolioTextBox();

                        if (txtNotionalSchemeName != null && txtFolio != null)
                        {
                            if (hdnNotionalSchemeFilter.Value != "")
                            {
                                txtNotionalSchemeName.Text = hdnNotionalSchemeFilter.Value.ToString().Trim();
                            }
                            if (hdnNotionalFolioFilter.Value != "")
                            {
                                txtNotionalFolio.Text = hdnNotionalFolioFilter.Value.ToString().Trim();
                            }
                        }
                    }

                    else
                        lblMessageNotional.Visible = true;
                    # endregion
                }
                if (containsBrockerChange)
                    lblPortfolioMsg.Visible = true;
                else
                    lblPortfolioMsg.Visible = false;
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:LoadMFPortfolio()");
                object[] objects = new object[6];
                objects[0] = customerVo;
                objects[1] = dividendTaggingPortfolioVo;
                objects[2] = genDict;
                objects[3] = tradeDate;
                objects[4] = dividendTaggingPortfolioVoList;
                objects[5] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
        /// <summary>
        /// This below three function (AllDataTableCreation,RealizedDataTableCreation,RealizedDataTableCreation) is created because  
        /// we need to call this function in two places one is at the time of Loading the page and another is at the category selection.
        /// </summary>
        /// <param name="dtMFPortfolio"></param>
        public void AllDataTableCreation(DataTable dtMFPortfolio)
        {
            dtMFPortfolio.Columns.Add("SI.No");//0
            dtMFPortfolio.Columns.Add("PortfolioCategoryName");//1
            dtMFPortfolio.Columns.Add("FundDescription");//2
            dtMFPortfolio.Columns.Add("FolioNum");//3
            dtMFPortfolio.Columns.Add("CurrentHoldings");//4
            dtMFPortfolio.Columns.Add("AveragePrice");//5
            dtMFPortfolio.Columns.Add("AcqCost");//6
            dtMFPortfolio.Columns.Add("NetAcqCost");//7
            dtMFPortfolio.Columns.Add("CurrentNAV");//8
            dtMFPortfolio.Columns.Add("CurrentValue");//9
            dtMFPortfolio.Columns.Add("TotalUnitsSold");//10
            dtMFPortfolio.Columns.Add("CostOfSales");//11
            dtMFPortfolio.Columns.Add("SalesProceeds");//12
            dtMFPortfolio.Columns.Add("DividendPayout");//13
            dtMFPortfolio.Columns.Add("DividendReinvested");//14
            dtMFPortfolio.Columns.Add("DividendTotal");//15
            dtMFPortfolio.Columns.Add("UnRealizedPL");//16
            dtMFPortfolio.Columns.Add("TotalPL");//17
            dtMFPortfolio.Columns.Add("AbsReturn");//18
            dtMFPortfolio.Columns.Add("XIRR");//19
            

        }
        public void RealizedDataTableCreation(DataTable dtMFPortfolioRealized)
        {
            dtMFPortfolioRealized.Columns.Add("SI.No");//0
            dtMFPortfolioRealized.Columns.Add("RealizedCategoryName");//1
            dtMFPortfolioRealized.Columns.Add("FundDescription");//2
            dtMFPortfolioRealized.Columns.Add("FolioNum");//3
            dtMFPortfolioRealized.Columns.Add("OriginalUnitsSold");//4
            dtMFPortfolioRealized.Columns.Add("DivUnitsSold");//5
            dtMFPortfolioRealized.Columns.Add("TotalUnitsSold");//6
            dtMFPortfolioRealized.Columns.Add("CostOfSales");//7
            dtMFPortfolioRealized.Columns.Add("SalesProceeds");//8
            dtMFPortfolioRealized.Columns.Add("DividendPayout");//9
            dtMFPortfolioRealized.Columns.Add("DividendReinvested");//10
            dtMFPortfolioRealized.Columns.Add("DividendTotal");//11
            dtMFPortfolioRealized.Columns.Add("RealizedPL");//12
            dtMFPortfolioRealized.Columns.Add("TotalRealizedPL");//13
            dtMFPortfolioRealized.Columns.Add("Absolute Return");//14
            dtMFPortfolioRealized.Columns.Add("XIRR");//15
        }
        public void NotionalDataTableCreation(DataTable dtMFPortfolioNotional)
        {

            dtMFPortfolioNotional.Columns.Add("SI.No");//0
            dtMFPortfolioNotional.Columns.Add("AssetInstrumentCategoryName");//1
            dtMFPortfolioNotional.Columns.Add("FundDescription");//2
            dtMFPortfolioNotional.Columns.Add("FolioNum");//3
            dtMFPortfolioNotional.Columns.Add("CurrentHoldings");//4
            dtMFPortfolioNotional.Columns.Add("OriginalUnits");//5
            dtMFPortfolioNotional.Columns.Add("DividendUnits");//6
            dtMFPortfolioNotional.Columns.Add("AverageCost");//7
            dtMFPortfolioNotional.Columns.Add("AcqCost");//8
            dtMFPortfolioNotional.Columns.Add("NetAcqCost");//9
            dtMFPortfolioNotional.Columns.Add("CurrentNAV");//10
            dtMFPortfolioNotional.Columns.Add("CurrentValue");//11
            dtMFPortfolioNotional.Columns.Add("DividendPayout");//12
            dtMFPortfolioNotional.Columns.Add("DividendReinvested");//13
            dtMFPortfolioNotional.Columns.Add("DividendTotal");//14
            dtMFPortfolioNotional.Columns.Add("UnRealizedPL");//15
            dtMFPortfolioNotional.Columns.Add("TotalPL");//16
            dtMFPortfolioNotional.Columns.Add("AbsoluteReturn");//17
            dtMFPortfolioNotional.Columns.Add("XIRR");//18
        }
        /// <summary>
        /// Below three functions are used to add details for the datatable.... schema which define on above three
        /// </summary>
        /// <param name="drMFPortfolio"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public DataRow PortfolioDetails(DataRow drMFPortfolio, int i)
        {
            
            drMFPortfolio[0] = (i + 1).ToString();
            drMFPortfolio["SI.No"]=(i + 1).ToString();//0
            drMFPortfolio["PortfolioCategoryName"]=dividendTaggingPortfolioVo.Category.ToString(); //1
            drMFPortfolio["FundDescription"]=dividendTaggingPortfolioVo.SchemePlan.ToString();//2
            drMFPortfolio["FolioNum"]=dividendTaggingPortfolioVo.Folio.ToString();//3
            drMFPortfolio["CurrentHoldings"] = dividendTaggingPortfolioVo.OutstandingUnits.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//4
            drMFPortfolio["AveragePrice"] = dividendTaggingPortfolioVo.AvgCostPerUnit.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//5
            drMFPortfolio["AcqCost"] = dividendTaggingPortfolioVo.AcqCost.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//6
            drMFPortfolio["NetAcqCost"] = dividendTaggingPortfolioVo.NetAcqCost.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//7
            drMFPortfolio["CurrentNAV"] = dividendTaggingPortfolioVo.CurrentNAV.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//8
            drMFPortfolio["CurrentValue"] = dividendTaggingPortfolioVo.CurrentValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//9
            drMFPortfolio["TotalUnitsSold"] = dividendTaggingPortfolioVo.TotalUnitsSold.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//10
            drMFPortfolio["CostOfSales"] = dividendTaggingPortfolioVo.CostOfSales.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//11
            drMFPortfolio["SalesProceeds"] = dividendTaggingPortfolioVo.SaleProceeds.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//12
            drMFPortfolio["DividendPayout"] = dividendTaggingPortfolioVo.AllDividendPayout.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//13
            drMFPortfolio["DividendReinvested"] = dividendTaggingPortfolioVo.AllDividendReinvested.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//14
            drMFPortfolio["DividendTotal"] = dividendTaggingPortfolioVo.AllDividendTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//15
            drMFPortfolio["UnRealizedPL"] = dividendTaggingPortfolioVo.AllUnRealizedPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//16
            drMFPortfolio["TotalPL"] = dividendTaggingPortfolioVo.AllTotalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//17
            drMFPortfolio["AbsReturn"] = dividendTaggingPortfolioVo.AllAbsReturn.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"))+"%";//18
            drMFPortfolio["XIRR"] = dividendTaggingPortfolioVo.AllXIRR.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) + "%";//19
            
            return drMFPortfolio;
        }

        public DataRow RealizedDetails(DataRow drMFPortfolioRealized, DataTable dtMFPortfolioRealized, int i)
        {

            drMFPortfolioRealized["SI.No"]=(i+1).ToString();//0
            drMFPortfolioRealized["RealizedCategoryName"]=dividendTaggingPortfolioVo.Category.ToString();//1
            drMFPortfolioRealized["FundDescription"]=dividendTaggingPortfolioVo.SchemePlan.ToString();//2
            drMFPortfolioRealized["FolioNum"]=dividendTaggingPortfolioVo.Folio.ToString();//3
            drMFPortfolioRealized["OriginalUnitsSold"] = dividendTaggingPortfolioVo.OriginalUnitsSold.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//4
            drMFPortfolioRealized["DivUnitsSold"] = dividendTaggingPortfolioVo.ReinvestedUnitsSold.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//5
            drMFPortfolioRealized["TotalUnitsSold"] = dividendTaggingPortfolioVo.TotalUnitsSold.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//6
            drMFPortfolioRealized["CostOfSales"] = dividendTaggingPortfolioVo.CostOfSales.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//7
            drMFPortfolioRealized["SalesProceeds"] = dividendTaggingPortfolioVo.SaleProceeds.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//8
            drMFPortfolioRealized["DividendPayout"] = dividendTaggingPortfolioVo.RealizedDividendPayout.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//9
            drMFPortfolioRealized["DividendReinvested"] = dividendTaggingPortfolioVo.RealizedDividendReinvested.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//10
            drMFPortfolioRealized["DividendTotal"] = dividendTaggingPortfolioVo.RealizedDividendTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//11
            drMFPortfolioRealized["RealizedPL"] = dividendTaggingPortfolioVo.RealizedPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//12
            drMFPortfolioRealized["TotalRealizedPL"] = dividendTaggingPortfolioVo.TotalRealizedPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//13
            drMFPortfolioRealized["Absolute Return"] = dividendTaggingPortfolioVo.RealizedAbsReturn.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) + "%";//14
            drMFPortfolioRealized["XIRR"] = dividendTaggingPortfolioVo.RealizedXIRR.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) + "%";//15
           
            return drMFPortfolioRealized;
        }

        public DataRow NotionalDetails(DividendTaggingPortfolioVo dividendTaggingPortfolioVo, DataRow drMFPortfolioNotional, DataTable dtMFPortfolioNotional, int i)
        {

            
            drMFPortfolioNotional["SI.No"]=(i + 1).ToString();//0
            drMFPortfolioNotional["AssetInstrumentCategoryName"]=dividendTaggingPortfolioVo.Category.ToString();//1
            drMFPortfolioNotional["FundDescription"]=dividendTaggingPortfolioVo.SchemePlan.ToString();//2
            drMFPortfolioNotional["FolioNum"]=dividendTaggingPortfolioVo.Folio.ToString();//3
            drMFPortfolioNotional["CurrentHoldings"] = dividendTaggingPortfolioVo.OutstandingUnits.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//4
            drMFPortfolioNotional["OriginalUnits"] = dividendTaggingPortfolioVo.OutstandingOriginalUnits.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//5
            drMFPortfolioNotional["DividendUnits"] = dividendTaggingPortfolioVo.OutstandingDividendUnits.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//6
            drMFPortfolioNotional["AverageCost"] = dividendTaggingPortfolioVo.AvgCostPerUnit.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//7
            drMFPortfolioNotional["AcqCost"] = dividendTaggingPortfolioVo.AcqCost.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//8
            drMFPortfolioNotional["NetAcqCost"] = dividendTaggingPortfolioVo.NetAcqCost.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//9
            drMFPortfolioNotional["CurrentNAV"] = dividendTaggingPortfolioVo.CurrentNAV.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//10
            drMFPortfolioNotional["CurrentValue"] = dividendTaggingPortfolioVo.CurrentValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//11
            drMFPortfolioNotional["DividendPayout"] = dividendTaggingPortfolioVo.OutStandingDividendPayout.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//12
            drMFPortfolioNotional["DividendReinvested"] = dividendTaggingPortfolioVo.OutStandingDividendReinvested.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//13
            drMFPortfolioNotional["DividendTotal"] = dividendTaggingPortfolioVo.OutStandingDividendTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//14
            drMFPortfolioNotional["UnRealizedPL"] = dividendTaggingPortfolioVo.UnRealizedPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//15
            drMFPortfolioNotional["TotalPL"] = dividendTaggingPortfolioVo.OutStandingTotalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));//16
            drMFPortfolioNotional["AbsoluteReturn"] = dividendTaggingPortfolioVo.OutStandingAbsReturn.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) + "%";//17
            drMFPortfolioNotional["XIRR"] = dividendTaggingPortfolioVo.OutStandingXIRR.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) + "%";//18
           
            return drMFPortfolioNotional;
        }
        #region Portfolio All Grid View Methods
        protected void gvMFPortfolio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadMFPortfolio();
                // lblCostOfAcqusition.Text = String.Format("{0:n2}", decimal.Parse(CostOfAcquisition.ToString()));
                //lblCurrentHolding.Text = String.Format("{0:n4}", decimal.Parse(currentHoldings.ToString()));
                //lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currenValue.ToString()));

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int slNo = int.Parse(gvMFPortfolio.DataKeys[index].Value.ToString());
                    //Session["MFPortfolioTransactionList"] = dividendTaggingPortfolioVoList[slNo - 1].MFPortfolioTransactionVoList;
                    //Session["MFPortfolioVo"] = dividendTaggingPortfolioVoList[slNo - 1];
                    Session["Folio"] = dividendTaggingPortfolioVoList[slNo - 1].Folio.ToString();
                    Session["Scheme"] = dividendTaggingPortfolioVoList[slNo - 1].SchemePlan.ToString();
                    Hashtable ht = new Hashtable();
                    ht["From"] = dividendTaggingPortfolioVoList[slNo - 1].DividendTaggingTransactionVoList[0].TransactionDate.ToShortDateString();
                    ht["To"] = DateTime.Today.ToShortDateString();
                    Session["tranDates"] = ht;
                    if (e.CommandName == "Select")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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


                object[] objects = new object[2];
                objects[0] = index;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        

        

        protected void gvMFPortfolio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMFPortfolio.PageIndex = e.NewPageIndex;
            gvMFPortfolio.DataBind();
        }

        #endregion Portfolio All Grid View Methods

        #region Portfolio Realized Grid View Methods
        protected void gvMFPortfolioRealized_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadMFPortfolio();
                // lblCostOfAcqusition.Text = String.Format("{0:n2}", decimal.Parse(CostOfAcquisition.ToString()));
                //lblCurrentHolding.Text = String.Format("{0:n4}", decimal.Parse(currentHoldings.ToString()));
                //lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currenValue.ToString()));

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int slNo = int.Parse(gvMFPortfolioRealized.DataKeys[index].Value.ToString());
                    Session["Folio"] = dividendTaggingPortfolioVoList[slNo - 1].Folio.ToString();
                    Session["Scheme"] = dividendTaggingPortfolioVoList[slNo - 1].SchemePlan.ToString();
                    Hashtable ht = new Hashtable();
                    ht["From"] = dividendTaggingPortfolioVoList[slNo - 1].DividendTaggingTransactionVoList[0].TransactionDate.ToShortDateString();
                    ht["To"] = DateTime.Today.ToShortDateString();
                    Session["tranDates"] = ht;
                    if (e.CommandName == "Select")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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

                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvMFPortfolioRealized_RowCommand()");


                object[] objects = new object[2];
                objects[0] = index;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        
        protected void gvMFPortfolioRealized_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMFPortfolioRealized.PageIndex = e.NewPageIndex;
            gvMFPortfolioRealized.DataBind();
        }
        #endregion Portfolio Realized Grid View Methods

        #region Portfolio Notional Grid View Methods
        protected void gvMFPortfolioNotional_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadMFPortfolio();
                //lblCostOfAcqusition.Text = String.Format("{0:n2}", decimal.Parse(CostOfAcquisition.ToString()));
                //lblCurrentHolding.Text = String.Format("{0:n4}", decimal.Parse(currentHoldings.ToString()));
                //lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currenValue.ToString()));

                if (e.CommandName.ToString() != "Sort")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    int slNo = int.Parse(gvMFPortfolioNotional.DataKeys[index].Value.ToString());
                    Session["Folio"] = dividendTaggingPortfolioVoList[slNo - 1].Folio.ToString();
                    Session["Scheme"] = dividendTaggingPortfolioVoList[slNo - 1].SchemePlan.ToString();
                    Hashtable ht = new Hashtable();
                    ht["From"] = dividendTaggingPortfolioVoList[slNo - 1].DividendTaggingTransactionVoList[0].TransactionDate.ToShortDateString();
                    ht["To"] = DateTime.Today.ToShortDateString();
                    Session["tranDates"] = ht;
                    if (e.CommandName == "Select")
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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

                FunctionInfo.Add("Method", "ViewEquityPortfolios.ascx:gvMFPortfolioNotional_RowCommand()");


                object[] objects = new object[2];
                objects[0] = index;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        
        protected void gvMFPortfolioNotional_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMFPortfolioNotional.PageIndex = e.NewPageIndex;
            gvMFPortfolioNotional.DataBind();
        }
        #endregion Portfolio Notional Grid View Methods

       

        protected void gvMFPortfolio_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvMFPortfolioRealized_DataBound(object sender, EventArgs e)
        {
            if (gvMFPortfolioRealized.FooterRow != null)
            {
                if (gvMFPortfolioRealized.FooterRow.DataItem != null)
                {
                    gvMFPortfolioRealized.FooterRow.Cells[1].Text = "Total Records: " + dividendTaggingPortfolioVoList.Count.ToString();

                    gvMFPortfolioRealized.FooterRow.Cells[1].ColumnSpan = gvMFPortfolioRealized.FooterRow.Cells.Count - 1;
                    for (int i = 2; i < gvMFPortfolioRealized.FooterRow.Cells.Count; i++)
                    {
                        gvMFPortfolioRealized.FooterRow.Cells[i].Visible = false;
                    }
                }
            }
        }

        protected void gvMFPortfolioNotional_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvMFPortfolio_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total ";

                
            }
            DataSet dsGetProductAssteInstrumentCategory = customerPortfolioBo.GetProductAssetInstrumentCategory();
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DropDownList ddlCategoryAll;
                ddlCategoryAll = (DropDownList)e.Row.FindControl("ddlCategoryAll");
                Session["ddlCategoryAll"] = ddlCategoryAll;
                if (ddlCategoryAll != null)
                {
                    ddlCategoryAll.DataSource = dsGetProductAssteInstrumentCategory;
                    ddlCategoryAll.DataTextField = dsGetProductAssteInstrumentCategory.Tables[0].Columns["AssetInstrumentCategoryName"].ToString();
                    ddlCategoryAll.DataValueField = dsGetProductAssteInstrumentCategory.Tables[0].Columns["AssetGroupCode"].ToString();
                    ddlCategoryAll.DataBind();
                    //ddlCategoryAll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    ddlCategoryAll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
                    if (hdnSelectedCategory != null)
                    {
                        ddlCategoryAll.SelectedValue = hdnSelectedCategory.Value;
                    }
                }

            }





        }
        protected void gvMFPortfolioRealized_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total:";
               

            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DataSet dsGetProductAssteInstrumentCategory = customerPortfolioBo.GetProductAssetInstrumentCategory();
                DropDownList ddlCategoryRealized;
                ddlCategoryRealized = (DropDownList)e.Row.FindControl("ddlCategoryRealized");
                Session["ddlCategoryRealized"] = ddlCategoryRealized;
                if (ddlCategoryRealized != null)
                {
                    ddlCategoryRealized.DataSource = dsGetProductAssteInstrumentCategory;
                    ddlCategoryRealized.DataTextField = dsGetProductAssteInstrumentCategory.Tables[0].Columns["AssetInstrumentCategoryName"].ToString();
                    ddlCategoryRealized.DataValueField = dsGetProductAssteInstrumentCategory.Tables[0].Columns["AssetGroupCode"].ToString();
                    ddlCategoryRealized.DataBind();
                    //ddlCategoryRealized.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    ddlCategoryRealized.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
                    if (hdnRealizedSelectedCategory != null)
                    {
                        ddlCategoryRealized.SelectedValue = hdnSelectedCategory.Value;
                    }
                }

            }
        }
        protected void gvMFPortfolioNotional_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            DataSet dsGetProductAssteInstrumentCategory = customerPortfolioBo.GetProductAssetInstrumentCategory();
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total ";

               


            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DropDownList ddlCategory;
                ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                Session["ddlCategory"] = ddlCategory;
                if (ddlCategory != null)
                {
                    ddlCategory.DataSource = dsGetProductAssteInstrumentCategory;
                    ddlCategory.DataTextField = dsGetProductAssteInstrumentCategory.Tables[0].Columns["AssetInstrumentCategoryName"].ToString();
                    ddlCategory.DataValueField = dsGetProductAssteInstrumentCategory.Tables[0].Columns["AssetGroupCode"].ToString();
                    ddlCategory.DataBind();
                    // ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "Select"));
                    ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "All"));
                    if (hdnSelectedCategory != null)
                    {
                        ddlCategory.SelectedValue = hdnSelectedCategory.Value;
                    }
                }

            }
        }

        protected void btnPortfolioSearch_Click(object sender, EventArgs e)
        {
            hdnRealizedFolioFilter.Value = "";
            hdnRealizedSchemeFilter.Value = "";
            hdnNotionalFolioFilter.Value = "";
            hdnNotionalSchemeFilter.Value = "";

            string TabberScript = "<script language='javascript'>document.getElementById('dvMFPortfolio').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

            TextBox txtSchemeName = GetPortfolioSchemeNameTextBox();
            TextBox txtFolio = GetPortfolioFolioTextBox();

            if (txtSchemeName != null && txtFolio != null)
            {
                hdnSchemeFilter.Value = txtSchemeName.Text.Trim();
                hdnFolioFilter.Value = txtFolio.Text.Trim();


            }
            LoadMFPortfolio();
        }

        private TextBox GetPortfolioFolioTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFPortfolio.HeaderRow != null)
            {
                if ((TextBox)gvMFPortfolio.HeaderRow.FindControl("txtFolioSearch") != null)
                {
                    txt = (TextBox)gvMFPortfolio.HeaderRow.FindControl("txtFolioSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetPortfolioSchemeNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFPortfolio.HeaderRow != null)
            {
                if ((TextBox)gvMFPortfolio.HeaderRow.FindControl("txtSchemeNameSearch") != null)
                {
                    txt = (TextBox)gvMFPortfolio.HeaderRow.FindControl("txtSchemeNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetRealizedFolioTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFPortfolioRealized.HeaderRow != null)
            {
                if ((TextBox)gvMFPortfolioRealized.HeaderRow.FindControl("txtRealizedFolioSearch") != null)
                {
                    txt = (TextBox)gvMFPortfolioRealized.HeaderRow.FindControl("txtRealizedFolioSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetRealizedSchemeNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFPortfolioRealized.HeaderRow != null)
            {
                if ((TextBox)gvMFPortfolioRealized.HeaderRow.FindControl("txtRealizedSchemeNameSearch") != null)
                {
                    txt = (TextBox)gvMFPortfolioRealized.HeaderRow.FindControl("txtRealizedSchemeNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetNotionalFolioTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFPortfolioNotional.HeaderRow != null)
            {
                if ((TextBox)gvMFPortfolioNotional.HeaderRow.FindControl("txtNotionalFolioSearch") != null)
                {
                    txt = (TextBox)gvMFPortfolioNotional.HeaderRow.FindControl("txtNotionalFolioSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        private TextBox GetNotionalSchemeNameTextBox()
        {
            TextBox txt = new TextBox();
            if (gvMFPortfolioNotional.HeaderRow != null)
            {
                if ((TextBox)gvMFPortfolioNotional.HeaderRow.FindControl("txtNotionalSchemeNameSearch") != null)
                {
                    txt = (TextBox)gvMFPortfolioNotional.HeaderRow.FindControl("txtNotionalSchemeNameSearch");
                }
            }
            else
                txt = null;

            return txt;
        }

        protected void btnPortfolioRealizedSearch_Click(object sender, EventArgs e)
        {
            hdnNotionalFolioFilter.Value = "";
            hdnNotionalSchemeFilter.Value = "";

            string TabberScript = "<script language='javascript'>document.getElementById('dvRealizedPortfolio').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

            TextBox txtSchemeName = GetRealizedSchemeNameTextBox();
            TextBox txtFolio = GetRealizedFolioTextBox();

            if (txtSchemeName != null && txtFolio != null)
            {
                hdnRealizedSchemeFilter.Value = txtSchemeName.Text.Trim();
                hdnRealizedFolioFilter.Value = txtFolio.Text.Trim();

                LoadMFPortfolio();
            }
        }
        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
            LoadMFPortfolio();
            
        }
        private DropDownList GetCategoryDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvMFPortfolioNotional.HeaderRow != null)
            {
                if ((DropDownList)gvMFPortfolioNotional.HeaderRow.FindControl("ddlCategory") != null)
                {
                    ddl = (DropDownList)gvMFPortfolioNotional.HeaderRow.FindControl("ddlCategory");
                }
            }
            else
                ddl = null;

            return ddl;
        }
        private DropDownList GetRealizedCategoryDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvMFPortfolioRealized.HeaderRow != null)
            {
                if ((DropDownList)gvMFPortfolioRealized.HeaderRow.FindControl("ddlCategoryRealized") != null)
                {
                    ddl = (DropDownList)gvMFPortfolioRealized.HeaderRow.FindControl("ddlCategoryRealized");
                }
            }
            else
                ddl = null;

            return ddl;
        }
        private DropDownList GetAllCategoryDDL()
        {
            DropDownList ddl = new DropDownList();
            if (gvMFPortfolio.HeaderRow != null)
            {
                if ((DropDownList)gvMFPortfolio.HeaderRow.FindControl("ddlCategoryAll") != null)
                {
                    ddl = (DropDownList)gvMFPortfolio.HeaderRow.FindControl("ddlCategoryAll");
                }
            }
            else
                ddl = null;

            return ddl;
        }

        protected void ddlCategoryAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnSelectedCategory.Value = ((DropDownList)gvMFPortfolio.HeaderRow.FindControl("ddlCategoryAll")).SelectedValue;
            LoadMFPortfolio();
            
        }
        protected void ddlCategoryRealized_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnSelectedCategory.Value = ((DropDownList)gvMFPortfolioRealized.HeaderRow.FindControl("ddlCategoryRealized")).SelectedValue;
            LoadMFPortfolio();
            
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnSelectedCategory.Value = ((DropDownList)gvMFPortfolioNotional.HeaderRow.FindControl("ddlCategory")).SelectedValue;
            LoadMFPortfolio();
            
        }
        protected void btnPortfolioNotionalSearch_Click(object sender, EventArgs e)
        {
            hdnRealizedFolioFilter.Value = "";
            hdnRealizedSchemeFilter.Value = "";

            string TabberScript = "<script language='javascript'>document.getElementById('dvNotionalPortfolio').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

            TextBox txtSchemeName = GetNotionalSchemeNameTextBox();
            TextBox txtFolio = GetNotionalFolioTextBox();

            if (txtSchemeName != null && txtFolio != null)
            {
                hdnNotionalSchemeFilter.Value = txtSchemeName.Text.Trim();
                hdnNotionalFolioFilter.Value = txtFolio.Text.Trim();

                LoadMFPortfolio();
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
                    gv.Controls.AddAt(i, l);

                }

                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {

                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);


                }

                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {

                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);

                }
                else if (gv.Controls[i].GetType() == typeof(TextBox))
                {

                    l.Text = (gv.Controls[i] as TextBox).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);

                }

                if (gv.Controls[i].HasControls())
                {

                    PrepareGridViewForExport(gv.Controls[i]);
                }

            }

        }

        private void ShowPdf(string strS)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader
            ("Content-Disposition", "attachment; filename=" + strS);
            Response.TransmitFile(strS);
            Response.End();
            Response.Flush();
            Response.Clear();

        }
        

        private void GridView_Print(string title)
        {

            if (title == "MFPortfolio")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewMutualFundPortfolio_dvMFPortfolio','ctrl_ViewMutualFundPortfolio_btnPrintGrid');", true);
            }
            else if (title == "MFPortfolioRealized")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewMutualFundPortfolio_dvRealizedPortfolio','ctrl_ViewMutualFundPortfolio_btnPrintGrid');", true);
            }
            else if (title == "MFPortfolioNotional")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewMutualFundPortfolio_dvNotionalPortfolio','ctrl_ViewMutualFundPortfolio_btnPrintGrid');", true);
            }


        }

        private void ExportGridView(string FileType, string title, GridView gv)
        {
            HtmlForm frm = new HtmlForm();
            frm.Controls.Clear();
            frm.Attributes["runat"] = "server";
            if (FileType.ToLower() == "print")
            {
                GridView_Print(title);
            }
            else if (FileType.ToLower() == "excel")
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

                // Create a form to contain the grid

                gv.Parent.Controls.Add(frm);
                frm.Controls.Add(gv);
                frm.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();


            }
            else if (FileType.ToLower() == "word")
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
                // Create a form to contain the grid
                gv.Parent.Controls.Add(frm);
                frm.Controls.Add(gv);
                frm.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else if (FileType.ToLower() == "pdf")
            {
                string temp = customerVo.FirstName + customerVo.LastName + title;
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gv.Columns.Count - 1);
                table.HeaderRows = 4;
                iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
                Phrase phApplicationName = new Phrase(" ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
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
                            cellText = "Folio Number";
                        }
                        else if (j == 3)
                        {
                            cellText = "Scheme Name";
                        }
                        else if (j == 4)
                        {
                            if (title == "MFPortfolio")
                            {
                                cellText = "Current Units";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "Number of Units sold";
                            }
                            else if (title == "MFPortfolioNotional")
                            {
                                cellText = "Current Units";
                            }


                        }
                        else if (j == 5)
                        {
                            if (title == "MFPortfolio")
                            {
                                cellText = "Average Price (Rs)";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "Realized sale Proceeds (Rs)";
                            }
                            else if (title == "MFPortfolioNotional")
                            {
                                cellText = "Cost of Acquisition (Rs)";
                            }


                        }
                        else if (j == 6)
                        {
                            if (title == "MFPortfolio")
                            {
                                cellText = "Cost of Acquisition (Rs)";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "Cost of Sales (Rs)";
                            }
                            else if (title == "MFPortfolioNotional")
                            {
                                cellText = "Current NAV (Rs)";
                            }


                        }
                        else if (j == 7)
                        {
                            if (title == "MFPortfolio")
                            {
                                cellText = "Current NAV (Rs) ";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "Divident Income (Rs)";
                            }
                            else if (title == "MFPortfolioNotional")
                            {
                                cellText = "Current Value (Rs)";
                            }


                        }
                        else if (j == 8)
                        {

                            if (title == "MFPortfolio")
                            {
                                cellText = "Current Value (Rs)";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "Realized P/L (Rs)";
                            }
                            else if (title == "MFPortfolioNotional")
                            {
                                cellText = "UnRealized P/L (Rs)";
                            }

                        }
                        else if (j == 9)
                        {

                            if (title == "MFPortfolio")
                            {
                                cellText = "Dividend Income (Rs)";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "Absolute Return";
                            }
                            else if (title == "MFPortfolioNotional")
                            {
                                cellText = "UnRealized Gain/Loss %";
                            }
                        }
                        else if (j == 10)
                        {

                            if (title == "MFPortfolio")
                            {
                                cellText = "UnRealized P/L (Rs)";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "Annual Return";
                            }
                            else if (title == "MFPortfolioNotional")
                            {
                                cellText = "XIRR";
                            }
                        }
                        else if (j == 11)
                        {

                            if (title == "MFPortfolio")
                            {
                                cellText = "Realized P/L (Rs)";
                            }
                            else if (title == "MFPortfolioRealized")
                            {
                                cellText = "XIRR";
                            }
                        }
                        else if (j == 12)
                        {

                            if (title == "MFPortfolio")
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
                                if (title == "MFPortfolio")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblFolioHeader")).Text;
                                }
                                else if (title == "MFPortfolioRealized")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblRealizedFolioHeader")).Text;
                                }
                                else if (title == "MFPortfolioNotional")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblNotionalFolioHeader")).Text;
                                }


                            }
                            else if (j == 3)
                            {
                                if (title == "MFPortfolio")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblNameHeader")).Text;
                                }
                                else if (title == "MFPortfolioRealized")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblRealizedNameHeader")).Text;
                                }
                                else if (title == "MFPortfolioNotional")
                                {
                                    cellText = ((Label)gv.Rows[i].FindControl("lblNotionalNameHeader")).Text;
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

        

       


        protected void btnPrintGrid_Click(object sender, EventArgs e)
        {
            LoadMFPortfolio();

        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            hdnDownloadFormat.Value = "excel";
            if (hdnSelectedTab.Value.ToString() == "1")
            {
                gvMFPortfolio.Columns[0].Visible = false;
                for (int i = 0; i < gvMFPortfolio.Rows.Count; i++)
                {
                    if (gvMFPortfolio.Rows[i].RowType != DataControlRowType.Header)
                    {
                        gvMFPortfolio.Rows[i].Cells[7].Text = gvMFPortfolio.Rows[i].Cells[7].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[8].Text = gvMFPortfolio.Rows[i].Cells[8].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[10].Text = gvMFPortfolio.Rows[i].Cells[10].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[12].Text = gvMFPortfolio.Rows[i].Cells[12].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[13].Text = gvMFPortfolio.Rows[i].Cells[13].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[14].Text = gvMFPortfolio.Rows[i].Cells[14].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[15].Text = gvMFPortfolio.Rows[i].Cells[15].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[16].Text = gvMFPortfolio.Rows[i].Cells[16].Text.Replace(",", "");

                    }
                }
                PrepareGridViewForExport(gvMFPortfolio);

                ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolio", gvMFPortfolio);
            }
            else if (hdnSelectedTab.Value.ToString() == "2")
            {
                gvMFPortfolioRealized.Columns[0].Visible = false;
                for (int i = 0; i < gvMFPortfolioRealized.Rows.Count; i++)
                {
                    if (gvMFPortfolioRealized.Rows[i].RowType != DataControlRowType.Header)
                    {
                        gvMFPortfolioRealized.Rows[i].Cells[12].Text = gvMFPortfolioRealized.Rows[i].Cells[12].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[7].Text = gvMFPortfolioRealized.Rows[i].Cells[7].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[8].Text = gvMFPortfolioRealized.Rows[i].Cells[8].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[13].Text = gvMFPortfolioRealized.Rows[i].Cells[13].Text.Replace(",", "");

                        gvMFPortfolioRealized.Rows[i].Cells[10].Text = gvMFPortfolioRealized.Rows[i].Cells[10].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[11].Text = gvMFPortfolioRealized.Rows[i].Cells[11].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[14].Text = gvMFPortfolioRealized.Rows[i].Cells[14].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[15].Text = gvMFPortfolioRealized.Rows[i].Cells[15].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[18].Text = gvMFPortfolioRealized.Rows[i].Cells[18].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[19].Text = gvMFPortfolioRealized.Rows[i].Cells[19].Text.Replace(",", "");

                    }
                }
                PrepareGridViewForExport(gvMFPortfolioRealized);
                ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioRealized", gvMFPortfolioRealized);
            }
            else if (hdnSelectedTab.Value.ToString() == "0")
            {
                gvMFPortfolioNotional.Columns[0].Visible = false;
                for (int i = 0; i < gvMFPortfolioNotional.Rows.Count; i++)
                {
                    if (gvMFPortfolioNotional.Rows[i].RowType != DataControlRowType.Header)
                    {
                        gvMFPortfolioNotional.Rows[i].Cells[6].Text = gvMFPortfolioNotional.Rows[i].Cells[6].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[7].Text = gvMFPortfolioNotional.Rows[i].Cells[7].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[8].Text = gvMFPortfolioNotional.Rows[i].Cells[8].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[9].Text = gvMFPortfolioNotional.Rows[i].Cells[9].Text.Replace(",", "");

                        gvMFPortfolioNotional.Rows[i].Cells[10].Text = gvMFPortfolioNotional.Rows[i].Cells[10].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[11].Text = gvMFPortfolioNotional.Rows[i].Cells[11].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[14].Text = gvMFPortfolioNotional.Rows[i].Cells[14].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[15].Text = gvMFPortfolioNotional.Rows[i].Cells[15].Text.Replace(",", "");

                    }
                }
                PrepareGridViewForExport(gvMFPortfolioNotional);
                ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioNotional", gvMFPortfolioNotional);
            }





        }

        protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            hdnDownloadFormat.Value = "excel";
            Export();

        }
        private void Export()
        {
            if (hdnSelectedTab.Value.ToString() == "0")
            {
                gvMFPortfolioNotional.Columns[0].Visible = false;
                for (int i = 0; i < gvMFPortfolioNotional.Rows.Count; i++)
                {
                    if (gvMFPortfolioNotional.Rows[i].RowType != DataControlRowType.Header)
                    {
                        gvMFPortfolioNotional.Rows[i].Cells[6].Text = gvMFPortfolioNotional.Rows[i].Cells[6].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[7].Text = gvMFPortfolioNotional.Rows[i].Cells[7].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[8].Text = gvMFPortfolioNotional.Rows[i].Cells[8].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[9].Text = gvMFPortfolioNotional.Rows[i].Cells[9].Text.Replace(",", "");

                        gvMFPortfolioNotional.Rows[i].Cells[10].Text = gvMFPortfolioNotional.Rows[i].Cells[10].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[11].Text = gvMFPortfolioNotional.Rows[i].Cells[11].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[12].Text = gvMFPortfolioNotional.Rows[i].Cells[12].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[13].Text = gvMFPortfolioNotional.Rows[i].Cells[13].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[14].Text = gvMFPortfolioNotional.Rows[i].Cells[14].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[15].Text = gvMFPortfolioNotional.Rows[i].Cells[15].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[16].Text = gvMFPortfolioNotional.Rows[i].Cells[16].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[17].Text = gvMFPortfolioNotional.Rows[i].Cells[17].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[18].Text = gvMFPortfolioNotional.Rows[i].Cells[18].Text.Replace(",", "");
                        gvMFPortfolioNotional.Rows[i].Cells[19].Text = gvMFPortfolioNotional.Rows[i].Cells[19].Text.Replace(",", "");

                    }
                }
                PrepareGridViewForExport(gvMFPortfolioNotional);
                ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioNotional", gvMFPortfolioNotional);


            }
            else if (hdnSelectedTab.Value.ToString() == "1")
            {
                gvMFPortfolio.Columns[0].Visible = false;
                for (int i = 0; i < gvMFPortfolio.Rows.Count; i++)
                {
                    if (gvMFPortfolio.Rows[i].RowType != DataControlRowType.Header)
                    {
                        gvMFPortfolio.Rows[i].Cells[5].Text = gvMFPortfolio.Rows[i].Cells[5].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[6].Text = gvMFPortfolio.Rows[i].Cells[6].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[7].Text = gvMFPortfolio.Rows[i].Cells[7].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[8].Text = gvMFPortfolio.Rows[i].Cells[8].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[9].Text = gvMFPortfolio.Rows[i].Cells[10].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[10].Text = gvMFPortfolio.Rows[i].Cells[10].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[11].Text = gvMFPortfolio.Rows[i].Cells[11].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[12].Text = gvMFPortfolio.Rows[i].Cells[12].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[13].Text = gvMFPortfolio.Rows[i].Cells[13].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[14].Text = gvMFPortfolio.Rows[i].Cells[14].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[15].Text = gvMFPortfolio.Rows[i].Cells[15].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[16].Text = gvMFPortfolio.Rows[i].Cells[16].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[17].Text = gvMFPortfolio.Rows[i].Cells[17].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[18].Text = gvMFPortfolio.Rows[i].Cells[18].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[19].Text = gvMFPortfolio.Rows[i].Cells[19].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[20].Text = gvMFPortfolio.Rows[i].Cells[20].Text.Replace(",", "");
                        gvMFPortfolio.Rows[i].Cells[21].Text = gvMFPortfolio.Rows[i].Cells[21].Text.Replace(",", "");



                    }
                }
                PrepareGridViewForExport(gvMFPortfolio);
                ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolio", gvMFPortfolio);
            }
            else if (hdnSelectedTab.Value.ToString() == "2")
            {
                gvMFPortfolioRealized.Columns[0].Visible = false;
                for (int i = 0; i < gvMFPortfolioRealized.Rows.Count; i++)
                {
                    if (gvMFPortfolioRealized.Rows[i].RowType != DataControlRowType.Header)
                    {
                        gvMFPortfolioRealized.Rows[i].Cells[6].Text = gvMFPortfolioRealized.Rows[i].Cells[6].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[7].Text = gvMFPortfolioRealized.Rows[i].Cells[7].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[8].Text = gvMFPortfolioRealized.Rows[i].Cells[8].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[9].Text = gvMFPortfolioRealized.Rows[i].Cells[9].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[10].Text = gvMFPortfolioRealized.Rows[i].Cells[10].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[11].Text = gvMFPortfolioRealized.Rows[i].Cells[11].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[12].Text = gvMFPortfolioRealized.Rows[i].Cells[12].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[13].Text = gvMFPortfolioRealized.Rows[i].Cells[13].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[14].Text = gvMFPortfolioRealized.Rows[i].Cells[14].Text.Replace(",", "");
                        gvMFPortfolioRealized.Rows[i].Cells[15].Text = gvMFPortfolioRealized.Rows[i].Cells[15].Text.Replace(",", "");


                    }
                }
                PrepareGridViewForExport(gvMFPortfolioRealized);
                ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioRealized", gvMFPortfolioRealized);

            }
        }

        protected void imgBtnWord_Click(object sender, ImageClickEventArgs e)
        {
            hdnDownloadFormat.Value = "word";
            Export();
        }

        protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        {
            hdnDownloadFormat.Value = "pdf";
            Export();
        }

        protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            hdnDownloadFormat.Value = "print";
            Export();
        }

        protected void GetBackNotionalLink_Click(object sender, EventArgs e)
        {
            LoadMFPortfolio();
        }


        protected void GoBackRealizedLink_Click(object sender, EventArgs e)
        {
            LoadMFPortfolio();
        }
        protected void GoBackAllLink_Click(object sender, EventArgs e)
        {
            LoadMFPortfolio();
        }
    }
}