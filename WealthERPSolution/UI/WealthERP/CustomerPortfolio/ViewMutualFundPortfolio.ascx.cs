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
    public partial class ViewMutualFundPortfolio : System.Web.UI.UserControl
    {        
        int index;
        MFPortfolioVo mfPortfolioVo;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<MFPortfolioVo> mfPortfolioList;
        CustomerVo customerVo;
        UserVo userVo;
        CustomerPortfolioVo customerPortfolioVo;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        // DateTime tradeDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        DateTime tradeDate = new DateTime();
        private double currenValue = 0;
        private double currentHoldings = 0;
        private double CostOfAcquisition = 0;
        private double acqCostExclDivReinvst = 0;
        double currenValue_Notional = 0;
        double currentHoldings_Notional = 0;
        double CostOfAcquisition_Notional = 0;
        private double acqCostExclDivReinvst_Notional = 0;
        private double divPayoutTotal_Notional = 0;
        private double divReinvstTotal_Notional = 0;
        private double divIncTotal_Notional = 0;
        private double divPayoutTotal_Realized = 0;
        private double divReinvstdTotal_Realized = 0;
        private double divReinvestedTotal_All = 0;
        private double divPayoutTotal_All = 0;
        private double salesProceedsTotal = 0;
        private double costOfSalesTotal = 0;
        private double realized_all = 0;
        private double unrealised_all = 0;
        private double realisedPNL = 0;
        private double unrealisedPNL = 0;
        private double divIncomeTotal = 0;
        private double stcg_notional_total = 0.0;
        private double ltcg_notional_total = 0.0;
        private double stcg_realized_total = 0.0;
        private double ltcg_realized_total = 0.0;
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
        static int portfolioId;
        RMVo rmVo = new RMVo();
        public double totalUnRealizedValue = 0;
        decimal temp = 0;
        DataSet dsCheckAndGetValuationDateForThePickedDate = new DataSet();
      
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
                txtPickDate.Text = DateTime.Parse(genDict["MFDate"].ToString()).ToShortDateString();
                cmpAsOnDate.ValueToCompare = DateTime.Today.ToShortDateString();
                txtPickDate.Enabled = false;
                //txtPickDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
                
                if (Session["folioNum"] != null)
                {
                    hdnFolioFilter.Value = Session["folioNum"].ToString();
                }

                //  customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                if (!IsPostBack)
                {
                    portfolioId = Int32.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    LoadMFPortfolio();
                    //BindPerformaceChart();
                    CalculatePortfolioXIRR(portfolioId);
                 
                  
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

                if(tradeDate == DateTime.MinValue)
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

                    mfPortfolioList = customerPortfolioBo.GetCustomerMFPortfolio(customerVo.CustomerId, portfolioId, tradeDate, hdnSchemeFilter.Value.Trim(), hdnFolioFilter.Value.Trim(),categorySearch);
                    //BindCategoryDropDown(mfPortfolioList);
                    Session["mfPortfolioList"] = mfPortfolioList;
                    if (mfPortfolioList != null)
                    {
                        count = mfPortfolioList.Count;
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

                    btnUpdateNP.Visible = false;
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

                    if (userVo.UserType.ToString() == "RM" || userVo.UserType.ToString() == "Advisor" || userVo.UserType.ToString() == "Adviser")
                        btnUpdateNP.Visible = true;
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
                    double unrealizedPLSum=0;
                    currenValue = 0;
                    currentHoldings = 0;
                    CostOfAcquisition = 0;
                    acqCostExclDivReinvst = 0;
                    currenValue_Notional = 0;
                    currentHoldings_Notional = 0;
                    CostOfAcquisition_Notional = 0;
                    acqCostExclDivReinvst_Notional = 0;
                    divPayoutTotal_Realized = 0;
                    divReinvstdTotal_Realized = 0;
                    salesProceedsTotal = 0;
                    costOfSalesTotal = 0;
                    realized_all = 0;
                    unrealised_all = 0;
                    realisedPNL = 0;
                    unrealisedPNL = 0;
                    divIncomeTotal = 0;
                    for (int j = 0; j < mfPortfolioList.Count; j++)
                    {
                        unrealizedPLSum = unrealizedPLSum + mfPortfolioList[j].UnRealizedPNL;
                        divIncomeTotal = divIncomeTotal + mfPortfolioList[j].DividendIncome;
                        costOfSalesTotal = costOfSalesTotal + mfPortfolioList[j].CostOfSales;
                        salesProceedsTotal = salesProceedsTotal + mfPortfolioList[j].RealizedSalesProceed;
                        if (mfPortfolioList[j].ContainsFolioTransfer)
                            containsBrockerChange = true;
                    }
                    for (int i = 0; i < mfPortfolioList.Count; i++)
                    {
                        mfPortfolioVo = new MFPortfolioVo();
                        mfPortfolioVo = mfPortfolioList[i];
                        //Portfolio All
                        # region Portfolio All
                        if (mfPortfolioVo != null)
                        {
                            
                                drMFPortfolio = dtMFPortfolio.NewRow();
                                drMFPortfolio = PortfolioDetails(drMFPortfolio, i);
                                //if (drMFPortfolio["CurrentHoldings"].ToString() == "0.00")
                                //{
                                //    dtMFPortfolio.Rows.InsertAt(drMFPortfolio, dtMFPortfolio.Rows.Count);

                                //}
                                //else
                                //{
                                //    dtMFPortfolio.Rows.InsertAt(drMFPortfolio, 0);
                                //}
                                dtMFPortfolio.Rows.Add(drMFPortfolio);
                           
                        }
                        # endregion
                        //Portfolio Realized
                        # region Portfolio Realized
                        if (mfPortfolioVo.SalesQuantity != 0.0)
                        {                           
                                drMFPortfolioRealized = dtMFPortfolioRealized.NewRow();
                                drMFPortfolioRealized = RealizedDetails(drMFPortfolioRealized, dtMFPortfolioRealized, i);
                                dtMFPortfolioRealized.Rows.Add(drMFPortfolioRealized);
                            
                        }
                        # endregion
                        //Portfolio Notional
                        # region Notional
                        if (mfPortfolioVo.Quantity != 0)
                        {
                            drMFPortfolioNotional = dtMFPortfolioNotional.NewRow();
                            drMFPortfolioNotional=NotionalDetails(mfPortfolioVo, drMFPortfolioNotional, dtMFPortfolioNotional, i);
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
                        hdnNoOfRecords.Value = dtMFPortfolio.Rows.Count.ToString();
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
                        hdnNoOfRecords.Value = dtMFPortfolioRealized.Rows.Count.ToString();
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
                            hdnNoOfRecords.Value = dtMFPortfolioNotional.DefaultView.Count.ToString();
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
                BindPerformaceChart();
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
                objects[1] = mfPortfolioVo;
                objects[2] = genDict;
                objects[3] = tradeDate;
                objects[4] = mfPortfolioList;
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
            dtMFPortfolio.Columns.Add("FolioNum");//1
            dtMFPortfolio.Columns.Add("FundDescription");//2
            dtMFPortfolio.Columns.Add("CurrentHoldings");//3
            dtMFPortfolio.Columns.Add("AveragePrice");//4
            dtMFPortfolio.Columns.Add("CostOfAcquisition");//5
            dtMFPortfolio.Columns.Add("CurrentNAV");//6
            dtMFPortfolio.Columns.Add("CurrentValue");//7
            dtMFPortfolio.Columns.Add("DividendIncome");//8
            dtMFPortfolio.Columns.Add("UnRealizedPL");//9
            dtMFPortfolio.Columns.Add("RealizedPL");//10
            dtMFPortfolio.Columns.Add("XIRR");//11
            dtMFPortfolio.Columns.Add("DividendPayout");//12
            dtMFPortfolio.Columns.Add("DividendReinvested");//13
            dtMFPortfolio.Columns.Add("AbsReturn");//14
            dtMFPortfolio.Columns.Add("TotalPL");//15
            dtMFPortfolio.Columns.Add("AcqCostExclDivReinvst");//16
            dtMFPortfolio.Columns.Add("PortfolioCategoryName");//17
            dtMFPortfolio.Columns.Add("unitssold");//18
            dtMFPortfolio.Columns.Add("costofsales");//19
            dtMFPortfolio.Columns.Add("salesproceeds");//20

        }
        public void RealizedDataTableCreation(DataTable dtMFPortfolioRealized)
        {
            dtMFPortfolioRealized.Columns.Add("SI.No");
            dtMFPortfolioRealized.Columns.Add("FolioNum");
            dtMFPortfolioRealized.Columns.Add("FundDescription");
            dtMFPortfolioRealized.Columns.Add("SalesQty");
            dtMFPortfolioRealized.Columns.Add("RealizedSalesProceeds");
            dtMFPortfolioRealized.Columns.Add("CostOfSales");
            dtMFPortfolioRealized.Columns.Add("DividendIncome");
            dtMFPortfolioRealized.Columns.Add("RealizedPL");
            dtMFPortfolioRealized.Columns.Add("Absolute Return");            
            dtMFPortfolioRealized.Columns.Add("XIRR");
            dtMFPortfolioRealized.Columns.Add("DividendPayout");
            dtMFPortfolioRealized.Columns.Add("DividendReinvested");
            dtMFPortfolioRealized.Columns.Add("RealizedCategoryName");
            dtMFPortfolioRealized.Columns.Add("STCG");
            dtMFPortfolioRealized.Columns.Add("LTCG");
        }
        public void NotionalDataTableCreation(DataTable dtMFPortfolioNotional)
        {
            
            dtMFPortfolioNotional.Columns.Add("SI.No");
            dtMFPortfolioNotional.Columns.Add("FolioNum");
            dtMFPortfolioNotional.Columns.Add("FundDescription");
            dtMFPortfolioNotional.Columns.Add("CurrentHoldings");
            dtMFPortfolioNotional.Columns.Add("CostOfAcquisition");
            dtMFPortfolioNotional.Columns.Add("CurrentNAV");
            dtMFPortfolioNotional.Columns.Add("CurrentValue");
            dtMFPortfolioNotional.Columns.Add("UnRealizedPL");
            dtMFPortfolioNotional.Columns.Add("XIRR");
            dtMFPortfolioNotional.Columns.Add("AcqCostExclDivReinvst");
            dtMFPortfolioNotional.Columns.Add("DividendPayout");
            dtMFPortfolioNotional.Columns.Add("DividendReinvested");
            dtMFPortfolioNotional.Columns.Add("DividendTotal");
            dtMFPortfolioNotional.Columns.Add("TransactionDate");
            dtMFPortfolioNotional.Columns.Add("AssetInstrumentCategoryName");
            dtMFPortfolioNotional.Columns.Add("AbsoluteReturn");
            dtMFPortfolioNotional.Columns.Add("STCG");
            dtMFPortfolioNotional.Columns.Add("LTCG");
            dtMFPortfolioNotional.Columns.Add("TotalPL");
        }
        /// <summary>
        /// Below three functions are used to add details for the datatable.... schema which define on above three
        /// </summary>
        /// <param name="drMFPortfolio"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public DataRow PortfolioDetails(DataRow drMFPortfolio,int i)
        {
            string absolutereturn = "0.0";
            drMFPortfolio[0] = (i + 1).ToString();

            drMFPortfolio[1] = mfPortfolioVo.Folio.ToString();
            if (mfPortfolioVo.ContainsFolioTransfer)
            {
                drMFPortfolio[2] = "** " + mfPortfolioVo.SchemePlan.ToString();
            }
            else
            {
                drMFPortfolio[2] = mfPortfolioVo.SchemePlan.ToString();
            }
            if (mfPortfolioVo.Quantity != 0)
            {
                drMFPortfolio[3] = double.Parse(mfPortfolioVo.Quantity.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[3] = "0.00";
            }
            if (mfPortfolioVo.AveragePrice != 0)
            {
                drMFPortfolio[4] = double.Parse(mfPortfolioVo.AveragePrice.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[4] = "0.00";
            }
            if (mfPortfolioVo.CostOfPurchase != 0)
            {
                drMFPortfolio[5] = double.Parse(mfPortfolioVo.CostOfPurchase.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[5] = "0.00";
            }
            if (mfPortfolioVo.CurrentNAV != 0)
            {
                if (mfPortfolioVo.Quantity != 0)
                {
                    drMFPortfolio[6] = double.Parse(mfPortfolioVo.CurrentNAV.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                }
                else
                {
                    drMFPortfolio[6] = "-";
                }
            }
            else
            {
                drMFPortfolio[6] = "0.00";
            }
            if (mfPortfolioVo.CurrentValue != 0)
            {
                if (mfPortfolioVo.Quantity != 0)
                {
                    drMFPortfolio[7] = double.Parse(mfPortfolioVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                }
                else
                {
                    drMFPortfolio[7] = "-";
                }
            }
            else
            {
                drMFPortfolio[7] = "0.00";
            }

            currenValue = currenValue + mfPortfolioVo.CurrentValue;
            CostOfAcquisition = CostOfAcquisition + mfPortfolioVo.CostOfPurchase;
            acqCostExclDivReinvst = acqCostExclDivReinvst + mfPortfolioVo.AcqCostExclDivReinvst;
            currentHoldings = currentHoldings + mfPortfolioVo.Quantity;
            if (mfPortfolioVo.DividendIncome != 0)
            {
                drMFPortfolio[8] = double.Parse(mfPortfolioVo.DividendIncome.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[8] = "0.00";
            }
            if (mfPortfolioVo.UnRealizedPNL != 0)
            {
                unrealised_all = unrealised_all + mfPortfolioVo.UnRealizedPNL;
                drMFPortfolio[9] = double.Parse(mfPortfolioVo.UnRealizedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[9] = "0.00";
            }
            if (mfPortfolioVo.RealizedPNL != 0)
            {
                realized_all = realized_all + mfPortfolioVo.RealizedPNL;
                drMFPortfolio[10] = double.Parse(mfPortfolioVo.RealizedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[10] = "0.00";
            }
            if (mfPortfolioVo.DividendPayout != 0)
            {
                divPayoutTotal_All = divPayoutTotal_All + mfPortfolioVo.DividendPayout;
                drMFPortfolio[12] = double.Parse(mfPortfolioVo.DividendPayout.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[12] = "0.00";
            }
            if (mfPortfolioVo.DividendReinvested != 0)
            {
                divReinvestedTotal_All = divReinvestedTotal_All + mfPortfolioVo.DividendReinvested;
                drMFPortfolio[13] = double.Parse(mfPortfolioVo.DividendReinvested.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[13] = "0.00";
            }
            if (mfPortfolioVo.CostOfSales+mfPortfolioVo.AcqCostExclDivReinvst != 0)
            {

                absolutereturn = (double.Parse(((mfPortfolioVo.TotalPNL / (mfPortfolioVo.CostOfSales + mfPortfolioVo.AcqCostExclDivReinvst)) * 100).ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                drMFPortfolio[14] = absolutereturn;
            }
            else
            {
                drMFPortfolio[14] = "0.00";
            }
            drMFPortfolio[11] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.XIRR.ToString("f2")));
            drMFPortfolio[15] = double.Parse(mfPortfolioVo.TotalPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            drMFPortfolio[16] = double.Parse(mfPortfolioVo.AcqCostExclDivReinvst.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            if (mfPortfolioVo.Category != null && mfPortfolioVo.CategoryCode != null)
            {
                drMFPortfolio[17] = mfPortfolioVo.Category.ToString();
            }
            if (mfPortfolioVo.SalesQuantity.ToString() != "0.0")
            {
                drMFPortfolio[18] = double.Parse(mfPortfolioVo.SalesQuantity.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[18] = "0.0";
            }
            if (mfPortfolioVo.CostOfSales.ToString() != "0.0")
            {
                drMFPortfolio[19] = double.Parse(mfPortfolioVo.CostOfSales.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[19] = "0.0";
            }
            if (mfPortfolioVo.RealizedSalesProceed.ToString() != "0.0")
            {
                drMFPortfolio[20] = double.Parse(mfPortfolioVo.RealizedSalesProceed.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolio[20] = "0.0";
            }
            return drMFPortfolio;
        }

        public DataRow RealizedDetails(DataRow drMFPortfolioRealized,DataTable dtMFPortfolioRealized,int i)
        {
                            string absreturn = "0.0";
                            drMFPortfolioRealized[0] = (i + 1).ToString();
                            drMFPortfolioRealized[1] = mfPortfolioVo.Folio.ToString();
                            if (mfPortfolioVo.ContainsFolioTransfer)
                            {
                               drMFPortfolioRealized[2] = "** " + mfPortfolioVo.SchemePlan.ToString();
                            }
                            else
                            {
                                drMFPortfolioRealized[2] = mfPortfolioVo.SchemePlan.ToString();
                            }
                             
                            drMFPortfolioRealized[3] = mfPortfolioVo.SalesQuantity.ToString("f2");
                            if (mfPortfolioVo.RealizedSalesProceed != 0)
                            {
                                drMFPortfolioRealized[4] = double.Parse(mfPortfolioVo.RealizedSalesProceed.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                               
                            }
                            else
                            {
                                drMFPortfolioRealized[4] = "0.00";
                            }
                            if (mfPortfolioVo.CostOfSales != 0)
                            {
                                drMFPortfolioRealized[5] = double.Parse(mfPortfolioVo.CostOfSales.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                               
                            }
                            else
                            {
                                drMFPortfolioRealized[5] = "0.00";
                            }
                            if (mfPortfolioVo.DividendIncome != 0)
                            {
                                drMFPortfolioRealized[6] = double.Parse(mfPortfolioVo.DividendIncome.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            }
                            else
                            {
                                drMFPortfolioRealized[6] = "0.00";
                            }
                            if (mfPortfolioVo.RealizedPNL != 0)
                            {
                                realisedPNL = realisedPNL + mfPortfolioVo.RealizedPNL;
                                drMFPortfolioRealized[7] = double.Parse(mfPortfolioVo.RealizedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                            }
                            else
                            {
                                drMFPortfolioRealized[7] = "0.00";
                            }
                            if ( mfPortfolioVo.CostOfSales != 0)
                            {
                                absreturn = (double.Parse((((mfPortfolioVo.RealizedPNL + mfPortfolioVo.DividendIncome) / mfPortfolioVo.CostOfSales)*100).ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                                drMFPortfolioRealized[8] = absreturn;
                            }
                            else
                            {
                                drMFPortfolioRealized[8] = "0.0";
                            }     
                            drMFPortfolioRealized[9] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.RealizedXIRR.ToString("f2")));
                            if (mfPortfolioVo.DividendPayout != 0)
                            {

                                drMFPortfolioRealized[10] = double.Parse(mfPortfolioVo.DividendPayout.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                                divPayoutTotal_Realized = divPayoutTotal_Realized + mfPortfolioVo.DividendPayout;
                            }
                            else
                            {
                                drMFPortfolioRealized[10] = "0.00";
                            }
                            if (mfPortfolioVo.DividendReinvested != 0)
                            {

                                drMFPortfolioRealized[11] = double.Parse(mfPortfolioVo.DividendReinvested.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                                divReinvstdTotal_Realized = divReinvstdTotal_Realized + mfPortfolioVo.DividendReinvested;
                                
                            }
                            else
                            {
                                drMFPortfolioRealized[11] = "0.00";
                            }
                            if (mfPortfolioVo.Category != null && mfPortfolioVo.CategoryCode != null)
                            {
                                drMFPortfolioRealized[12] = mfPortfolioVo.Category.ToString();
                            }
                            //======================================================================================================
                            //This is for STCG and LTCG
                            //======================================================================================================
                            
                                drMFPortfolioRealized[13] = (mfPortfolioVo.STCG-mfPortfolioVo.STCGEligible).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                                drMFPortfolioRealized[14] = (mfPortfolioVo.LTCG - mfPortfolioVo.LTCGEligible).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));



                                ltcg_realized_total = ltcg_realized_total + (mfPortfolioVo.LTCG - mfPortfolioVo.LTCGEligible);
                                stcg_realized_total = stcg_realized_total + (mfPortfolioVo.STCG - mfPortfolioVo.STCGEligible);
                            return drMFPortfolioRealized;                   
        }
       
        public DataRow NotionalDetails(MFPortfolioVo mfPortfolioVo, DataRow drMFPortfolioNotional, DataTable dtMFPortfolioNotional,int i)
        {            

            drMFPortfolioNotional[0] = (i + 1).ToString();

            drMFPortfolioNotional[1] = mfPortfolioVo.Folio.ToString();
            if (mfPortfolioVo.ContainsFolioTransfer)
            {
                drMFPortfolioNotional[2] = "** "+ mfPortfolioVo.SchemePlan.ToString();
            }
            else
            {
                drMFPortfolioNotional[2] = mfPortfolioVo.SchemePlan.ToString();
            }
            drMFPortfolioNotional[3] = mfPortfolioVo.Quantity.ToString("f2");
            if (mfPortfolioVo.CostOfPurchase != 0)
            {
                drMFPortfolioNotional[4] = double.Parse(mfPortfolioVo.CostOfPurchase.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolioNotional[4] = "0.00";
            }
            if (mfPortfolioVo.CurrentNAV != 0)
            {
                drMFPortfolioNotional[5] = double.Parse(mfPortfolioVo.CurrentNAV.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolioNotional[5] = "0.00";
            }
            if (mfPortfolioVo.CurrentValue != 0)
            {
                drMFPortfolioNotional[6] = double.Parse(mfPortfolioVo.CurrentValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolioNotional[6] = "0.00";
            }
            if (mfPortfolioVo.UnRealizedPNL != 0)
            {
                unrealisedPNL = unrealisedPNL + mfPortfolioVo.UnRealizedPNL;
                drMFPortfolioNotional[7] = double.Parse(mfPortfolioVo.UnRealizedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolioNotional[7] = "0.00";
            }


            if (mfPortfolioVo.DividendPayout != 0)
            {

                drMFPortfolioNotional[10] = double.Parse(mfPortfolioVo.DividendPayout.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                divPayoutTotal_Notional = divPayoutTotal_Notional + mfPortfolioVo.DividendPayout;
            }
            else
            {
                drMFPortfolioNotional[10] = "0.00";
            }
            if (mfPortfolioVo.DividendReinvested != 0)
            {

                drMFPortfolioNotional[11] = double.Parse(mfPortfolioVo.DividendReinvested.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                divReinvstTotal_Notional = divReinvstTotal_Notional + mfPortfolioVo.DividendReinvested;
            }
            else
            {
                drMFPortfolioNotional[11] = "0.00";
            }
            if (mfPortfolioVo.DividendIncome != 0)
            {

                drMFPortfolioNotional[12] = double.Parse(mfPortfolioVo.DividendIncome.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                divIncTotal_Notional = divIncTotal_Notional + mfPortfolioVo.DividendIncome;
            }
            else
            {
                drMFPortfolioNotional[12] = "0.00";
            }
            // FolioDate
            //======================================================================================================

            drMFPortfolioNotional[13] = mfPortfolioVo.DateOfAcq.ToShortDateString();
            //======================================================================================================
            // To add Category Data in Dropdown
            //======================================================================================================
            drMFPortfolioNotional[14] = mfPortfolioVo.Category.ToString();
            //======================================================================================================
            //This is for Absolute Returns
            //======================================================================================================
            if (mfPortfolioVo.AcqCostExclDivReinvst!=0)
            {
                drMFPortfolioNotional[15] = (double.Parse((((mfPortfolioVo.UnRealizedPNL + mfPortfolioVo.DividendIncome) / mfPortfolioVo.AcqCostExclDivReinvst) * 100).ToString())).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            }
            else
            {
                drMFPortfolioNotional[15] = "0.0";
            }
            //======================================================================================================
            //This is for STCG and LTCG
            //======================================================================================================
            
            
            //mfPortfolioList[slNo - 1].MFPortfolioTransactionVoList

            drMFPortfolioNotional[17] = mfPortfolioVo.LTCGEligible.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            drMFPortfolioNotional[16] = mfPortfolioVo.STCGEligible.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));


            stcg_notional_total = stcg_notional_total + mfPortfolioVo.STCGEligible;
            ltcg_notional_total = ltcg_notional_total + mfPortfolioVo.LTCGEligible;
            //======================================================================================================
            //Total P/L=(dividend Income + Unrealized P/L)
            //======================================================================================================
            drMFPortfolioNotional[18] = (mfPortfolioVo.DividendIncome + mfPortfolioVo.UnRealizedPNL).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            //======================================================================================================
            totalUnRealizedValue = totalUnRealizedValue + mfPortfolioVo.UnRealizedPNL;
            drMFPortfolioNotional[8] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.XIRR.ToString("f2")));
            drMFPortfolioNotional[9] = double.Parse(mfPortfolioVo.AcqCostExclDivReinvst.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
            currenValue_Notional = currenValue_Notional + mfPortfolioVo.CurrentValue;
            CostOfAcquisition_Notional = CostOfAcquisition_Notional + mfPortfolioVo.CostOfPurchase;
            acqCostExclDivReinvst_Notional = acqCostExclDivReinvst_Notional + mfPortfolioVo.AcqCostExclDivReinvst;
            currentHoldings_Notional = currentHoldings_Notional + mfPortfolioVo.Quantity;
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
                    //Session["MFPortfolioTransactionList"] = mfPortfolioList[slNo - 1].MFPortfolioTransactionVoList;
                    //Session["MFPortfolioVo"] = mfPortfolioList[slNo - 1];
                    Session["Folio"] = mfPortfolioList[slNo - 1].Folio.ToString();
                    Session["Scheme"] = mfPortfolioList[slNo - 1].SchemePlan.ToString();
                    Hashtable ht = new Hashtable();
                    ht["From"] = mfPortfolioList[slNo - 1].MFPortfolioTransactionVoList[0].BuyDate.ToShortDateString();
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

        protected void gvMFPortfolio_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                sortGridViewMFPortfolio(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                sortGridViewMFPortfolio(sortExpression, ASCENDING);
            }
        }

        private void sortGridViewMFPortfolio(string sortExpression, string direction)
        {
            LoadMFPortfolio();
            //lblCostOfAcqusition.Text = String.Format("{0:n2}", decimal.Parse(CostOfAcquisition.ToString()));
            //   lblCurrentHolding.Text = String.Format("{0:n4}", decimal.Parse(currentHoldings.ToString()));
            //lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currenValue.ToString()));

            DataTable dtMFPortfolio = new DataTable();

            //Portfolio All Datatable
            dtMFPortfolio.Columns.Add("SI.No");
            dtMFPortfolio.Columns.Add("FolioNum");
            dtMFPortfolio.Columns.Add("FundDescription");
            dtMFPortfolio.Columns.Add("CurrentHoldings");
            dtMFPortfolio.Columns.Add("AveragePrice");
            dtMFPortfolio.Columns.Add("CostOfAcquisition");
            dtMFPortfolio.Columns.Add("CurrentNAV");
            dtMFPortfolio.Columns.Add("CurrentValue");
            dtMFPortfolio.Columns.Add("DividendIncome");
            dtMFPortfolio.Columns.Add("UnRealizedPL");
            dtMFPortfolio.Columns.Add("RealizedPL");

            DataRow drMFPortfolio;

            for (int i = 0; i < mfPortfolioList.Count; i++)
            {
                mfPortfolioVo = new MFPortfolioVo();
                mfPortfolioVo = mfPortfolioList[i];
                //Portfolio All
                drMFPortfolio = dtMFPortfolio.NewRow();

                drMFPortfolio[0] = (i + 1).ToString();

                drMFPortfolio[1] = mfPortfolioVo.Folio.ToString();
                drMFPortfolio[2] = mfPortfolioVo.SchemePlan.ToString();
                drMFPortfolio[3] = mfPortfolioVo.Quantity.ToString("f4");
                drMFPortfolio[4] = String.Format("{0:n4}", decimal.Parse(mfPortfolioVo.AveragePrice.ToString("f4")));
                drMFPortfolio[5] = String.Format("{0:n2}", decimal.Parse(mfPortfolioVo.CostOfPurchase.ToString("f2")));
                drMFPortfolio[6] = String.Format("{0:n4}", decimal.Parse(mfPortfolioVo.CurrentNAV.ToString("f4")));
                drMFPortfolio[7] = String.Format("{0:n4}", decimal.Parse(mfPortfolioVo.CurrentValue.ToString("f2")));
                drMFPortfolio[8] = String.Format("{0:n4}", decimal.Parse(mfPortfolioVo.DividendIncome.ToString("f2")));
                drMFPortfolio[9] = String.Format("{0:n4}", decimal.Parse(mfPortfolioVo.UnRealizedPNL.ToString("f2")));
                drMFPortfolio[10] = String.Format("{0:n4}", decimal.Parse(mfPortfolioVo.RealizedPNL.ToString("f2")));

                dtMFPortfolio.Rows.Add(drMFPortfolio);
            }

            DataView dv = new DataView(dtMFPortfolio);
            dv.Sort = hdnSort.Value;
            gvMFPortfolio.DataSource = dv;
            gvMFPortfolio.DataBind();
            gvMFPortfolio.Visible = true;
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
                    Session["Folio"] = mfPortfolioList[slNo - 1].Folio.ToString();
                    Session["Scheme"] = mfPortfolioList[slNo - 1].SchemePlan.ToString();
                    Hashtable ht = new Hashtable();
                    ht["From"] = mfPortfolioList[slNo - 1].MFPortfolioTransactionVoList[0].BuyDate.ToShortDateString();
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

        protected void gvMFPortfolioRealized_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                sortGridViewMFPortfolioRealized(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                sortGridViewMFPortfolioRealized(sortExpression, ASCENDING);
            }

            string TabberScript = "<script language='javascript'>document.getElementById('dvRealizedPortfolio').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

        }

        private void sortGridViewMFPortfolioRealized(string sortExpression, string direction)
        {
            LoadMFPortfolio();
            //lblCostOfAcqusition.Text = String.Format("{0:n2}", decimal.Parse(CostOfAcquisition.ToString()));
            //lblCurrentHolding.Text = String.Format("{0:n4}", decimal.Parse(currentHoldings.ToString()));
            //  lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currenValue.ToString()));

            DataTable dtMFPortfolioRealized = new DataTable();

            //Portfolio All Datatable
            dtMFPortfolioRealized.Columns.Add("SI.No");
            dtMFPortfolioRealized.Columns.Add("FolioNum");
            dtMFPortfolioRealized.Columns.Add("FundDescription");
            dtMFPortfolioRealized.Columns.Add("SalesQty");
            dtMFPortfolioRealized.Columns.Add("RealizedSalesProceeds");
            dtMFPortfolioRealized.Columns.Add("CostOfSales");
            dtMFPortfolioRealized.Columns.Add("DividendIncome");
            dtMFPortfolioRealized.Columns.Add("RealizedPL");


            DataRow drMFPortfolioRealized;

            for (int i = 0; i < mfPortfolioList.Count; i++)
            {
                mfPortfolioVo = new MFPortfolioVo();
                mfPortfolioVo = mfPortfolioList[i];
                //Portfolio All
                drMFPortfolioRealized = dtMFPortfolioRealized.NewRow();

                drMFPortfolioRealized[0] = (i + 1).ToString();
                drMFPortfolioRealized[1] = mfPortfolioVo.Folio.ToString();
                drMFPortfolioRealized[2] = mfPortfolioVo.SchemePlan.ToString();
                drMFPortfolioRealized[3] = mfPortfolioVo.SalesQuantity.ToString("f4");
                drMFPortfolioRealized[4] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.RealizedSalesProceed.ToString("f2")));
                drMFPortfolioRealized[5] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.CostOfSales.ToString("f2")));
                drMFPortfolioRealized[6] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.DividendIncome.ToString("f2")));
                drMFPortfolioRealized[7] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.RealizedPNL.ToString("f2")));

                dtMFPortfolioRealized.Rows.Add(drMFPortfolioRealized);



            }


            DataView dv = new DataView(dtMFPortfolioRealized);
            dv.Sort = hdnSort.Value;
            //sortExpression + direction;
            gvMFPortfolioRealized.DataSource = dv;
            gvMFPortfolioRealized.DataBind();
            gvMFPortfolioRealized.Visible = true;
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
                    Session["Folio"] = mfPortfolioList[slNo - 1].Folio.ToString();
                    Session["Scheme"] = mfPortfolioList[slNo - 1].SchemePlan.ToString();
                    Hashtable ht = new Hashtable();
                    ht["From"] = mfPortfolioList[slNo - 1].MFPortfolioTransactionVoList[0].BuyDate.ToShortDateString();
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

        protected void gvMFPortfolioNotional_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["sortExpression"] = sortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                hdnSort.Value = sortExpression + " DESC";
                sortGridViewMFPortfolioNotional(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                hdnSort.Value = sortExpression + " ASC";
                sortGridViewMFPortfolioNotional(sortExpression, ASCENDING);
            }

            string TabberScript = "<script language='javascript'>document.getElementById('dvNotionalPortfolio').className = 'tabbertab tabbertabdefault';</script>";//
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Tabber", TabberScript);

        }

        private void sortGridViewMFPortfolioNotional(string sortExpression, string direction)
        {
            LoadMFPortfolio();
            //lblCostOfAcqusition.Text = String.Format("{0:n2}", decimal.Parse(CostOfAcquisition.ToString()));
            //lblCurrentHolding.Text = String.Format("{0:n4}", decimal.Parse(currentHoldings.ToString()));
            // lblCurrentValue.Text = String.Format("{0:n2}", decimal.Parse(currenValue.ToString()));

            DataTable dtMFPortfolioNotional = new DataTable();

            //Portfolio All Datatable
            dtMFPortfolioNotional.Columns.Add("SI.No");
            dtMFPortfolioNotional.Columns.Add("FolioNum");
            dtMFPortfolioNotional.Columns.Add("FundDescription");
            dtMFPortfolioNotional.Columns.Add("CurrentHoldings");
            dtMFPortfolioNotional.Columns.Add("CostOfAcquisition");
            dtMFPortfolioNotional.Columns.Add("CurrentNAV");
            dtMFPortfolioNotional.Columns.Add("CurrentValue");
            dtMFPortfolioNotional.Columns.Add("UnRealizedPL");


            DataRow drMFPortfolioNotional;

            for (int i = 0; i < mfPortfolioList.Count; i++)
            {
                mfPortfolioVo = new MFPortfolioVo();
                mfPortfolioVo = mfPortfolioList[i];
                //Portfolio All
                if (mfPortfolioVo.Quantity != 0)
                {
                    drMFPortfolioNotional = dtMFPortfolioNotional.NewRow();

                    drMFPortfolioNotional[0] = (i + 1).ToString();

                    drMFPortfolioNotional[1] = mfPortfolioVo.Folio.ToString();
                    drMFPortfolioNotional[2] = mfPortfolioVo.SchemePlan.ToString();
                    drMFPortfolioNotional[3] = mfPortfolioVo.Quantity.ToString("f4");
                    drMFPortfolioNotional[4] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.CostOfPurchase.ToString("f2")));
                    drMFPortfolioNotional[5] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.CurrentNAV.ToString("f4")));
                    drMFPortfolioNotional[6] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.CurrentValue.ToString("f2")));
                    drMFPortfolioNotional[7] = String.Format("{0:n2}", double.Parse(mfPortfolioVo.UnRealizedPNL.ToString("f2")));


                    dtMFPortfolioNotional.Rows.Add(drMFPortfolioNotional);
                }



            }


            DataView dv = new DataView(dtMFPortfolioNotional);
            dv.Sort = hdnSort.Value;
            //sortExpression + direction;
            gvMFPortfolioNotional.DataSource = dv;
            gvMFPortfolioNotional.DataBind();
            gvMFPortfolioNotional.Visible = true;
        }

        protected void gvMFPortfolioNotional_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMFPortfolioNotional.PageIndex = e.NewPageIndex;
            gvMFPortfolioNotional.DataBind();
        }
        #endregion Portfolio Notional Grid View Methods

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

        protected void btnUpdateNP_Click(object sender, EventArgs e)
        {
            try
            {
                if (mfPortfolioList.Count != 0)
                {
                    customerPortfolioBo.DeleteMutualFundNetPosition(customerVo.CustomerId, tradeDate);
                    customerPortfolioBo.AddMutualFundNetPosition(mfPortfolioList, userVo.UserId);
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


                object[] objects = new object[2];
                objects[0] = mfPortfolioList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvMFPortfolio_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvMFPortfolioRealized_DataBound(object sender, EventArgs e)
        {
            if (gvMFPortfolioRealized.FooterRow != null)
            {
                if (gvMFPortfolioRealized.FooterRow.DataItem != null)
                {
                    gvMFPortfolioRealized.FooterRow.Cells[1].Text = "Total Records: " + mfPortfolioList.Count.ToString();

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
                if (hdnNoOfRecords.Value != "")
                    e.Row.Cells[3].Text = "Total Records : " + hdnNoOfRecords.Value + "";
                e.Row.Cells[7].Text = double.Parse(acqCostExclDivReinvst.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[7].Attributes.Add("align", "Right");
                e.Row.Cells[8].Text = double.Parse(CostOfAcquisition.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[8].Attributes.Add("align", "Right");

                e.Row.Cells[10].Text = double.Parse(currenValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[10].Attributes.Add("align", "Right");
                e.Row.Cells[12].Text = double.Parse(costOfSalesTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[12].Attributes.Add("align", "Right");
                e.Row.Cells[13].Text = double.Parse(salesProceedsTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[13].Attributes.Add("align", "Right");
                e.Row.Cells[14].Text = double.Parse((divPayoutTotal_All).ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[14].Attributes.Add("align", "Right");
                e.Row.Cells[15].Text = double.Parse((divReinvestedTotal_All).ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[15].Attributes.Add("align", "Right");
                e.Row.Cells[16].Text = double.Parse(divIncomeTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[16].Attributes.Add("align", "Right");

                e.Row.Cells[17].Text = double.Parse(unrealised_all.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[17].Attributes.Add("align", "Right");
                e.Row.Cells[18].Text = double.Parse(realized_all.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[18].Attributes.Add("align", "Right");
                e.Row.Cells[19].Text = double.Parse((realized_all + unrealised_all + divIncomeTotal).ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[19].Attributes.Add("align", "Right");
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
                e.Row.Cells[1].Attributes.Add("align", "Left");
                if (hdnNoOfRecords.Value != "")
                    e.Row.Cells[3].Text = "Total Records : " + hdnNoOfRecords.Value + "";
                e.Row.Cells[7].Text = double.Parse(salesProceedsTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[7].Attributes.Add("align", "Right");
                e.Row.Cells[6].Text = double.Parse(costOfSalesTotal.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[6].Attributes.Add("align", "Right");
                e.Row.Cells[8].Text = double.Parse(divPayoutTotal_Realized.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[8].Attributes.Add("align", "Right");
                e.Row.Cells[9].Text = double.Parse(divReinvstdTotal_Realized.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[9].Attributes.Add("align", "Right");
                e.Row.Cells[10].Text = double.Parse((divPayoutTotal_Realized + divReinvstdTotal_Realized).ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[10].Attributes.Add("align", "Right");
                e.Row.Cells[11].Text = double.Parse(realisedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[11].Attributes.Add("align", "Right");
                e.Row.Cells[14].Text = double.Parse(stcg_realized_total.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[14].Attributes.Add("align", "Right");
                e.Row.Cells[15].Text = double.Parse(ltcg_realized_total.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[15].Attributes.Add("align", "Right");


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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //currenValue_Notional = currenValue_Notional + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CurrentValue"));
            //    //CostOfAcquisition_Notional = CostOfAcquisition_Notional + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CostOfAcquisition"));
            //    //currentHoldings_Notional = currentHoldings_Notional + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CurrentHoldings"));
            //    if (double.Parse(e.Row.Cells[8].Text.ToString()) == 0 && totalUnRealizedValue == 0)
            //    {
            //        e.Row.Cells[9].Text = "0.00";
            //    }
            //    else
            //    {
            //        e.Row.Cells[9].Text = string.Format("{0:n2}", decimal.Parse(((double.Parse(e.Row.Cells[8].Text.ToString()) / totalUnRealizedValue) * 100).ToString()));
            //    }
            //}
            //else 
            DataSet dsGetProductAssteInstrumentCategory=customerPortfolioBo.GetProductAssetInstrumentCategory();
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total ";
                if (hdnNoOfRecords.Value != "")
                    e.Row.Cells[3].Text = "Total Records : " + hdnNoOfRecords.Value + "";
                e.Row.Cells[7].Text = double.Parse(acqCostExclDivReinvst_Notional.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[7].Attributes.Add("align", "Right");
                e.Row.Cells[8].Text = double.Parse(CostOfAcquisition_Notional.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[8].Attributes.Add("align", "Right");

                e.Row.Cells[10].Text = double.Parse(currenValue_Notional.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[10].Attributes.Add("align", "Right");
                e.Row.Cells[11].Text = double.Parse(divPayoutTotal_Notional.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[11].Attributes.Add("align", "Right");
                e.Row.Cells[12].Text = double.Parse(divReinvstTotal_Notional.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[12].Attributes.Add("align", "Right");
                e.Row.Cells[13].Text = double.Parse(divIncTotal_Notional.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[13].Attributes.Add("align", "Right");
                e.Row.Cells[14].Text = double.Parse(unrealisedPNL.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[14].Attributes.Add("align", "Right");
                e.Row.Cells[15].Text = double.Parse((unrealisedPNL + divIncTotal_Notional).ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[15].Attributes.Add("align", "Right");
                e.Row.Cells[18].Text = double.Parse(stcg_notional_total.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[18].Attributes.Add("align", "Right");
                e.Row.Cells[19].Text = double.Parse(ltcg_notional_total.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[19].Attributes.Add("align", "Right");


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
            BindPerformaceChart();
            CalculatePortfolioXIRR(portfolioId);
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
            BindPerformaceChart();
        }
        protected void ddlCategoryRealized_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnSelectedCategory.Value = ((DropDownList)gvMFPortfolioRealized.HeaderRow.FindControl("ddlCategoryRealized")).SelectedValue;
            LoadMFPortfolio();
            BindPerformaceChart();
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)            
        {
            hdnSelectedCategory.Value = ((DropDownList)gvMFPortfolioNotional.HeaderRow.FindControl("ddlCategory")).SelectedValue;
            LoadMFPortfolio();
            BindPerformaceChart();
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
        //protected void btnExportNotional_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolioNotional.Columns[0].Visible = false;
        //    PrepareGridViewForExport(gvMFPortfolioNotional);
        //    if (rbtnNotionalExcel.Checked)
        //    {
        //        ExportGridView("Excel", "MFPortfolioNotional", gvMFPortfolioNotional);
        //    }
        //    else if (rbtnNotionalPdf.Checked)
        //    {

        //        ExportGridView("PDF", "MFPortfolioNotional", gvMFPortfolioNotional);
        //    }
        //    else if (rbtnNotionalWord.Checked)
        //    {
        //        ExportGridView("Word", "MFPortfolioNotional", gvMFPortfolioNotional);
        //    }

        //    gvMFPortfolioNotional.Columns[0].Visible = true;
        //}

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
            //else if (FileType.ToLower() == "word")
            //{

            //    string temp = customerVo.FirstName + customerVo.LastName + title + ".doc";
            //    string attachment = "attachment; filename=" + temp;
            //    Response.ClearContent();
            //    Response.AddHeader("content-disposition", attachment);
            //    Response.ContentType = "application/msword";
            //    StringWriter sw = new StringWriter();
            //    HtmlTextWriter htw = new HtmlTextWriter(sw);

            //    Response.Output.Write("<table border=\"0\"><tbody><caption><FONT FACE=\"ARIAL\"  SIZE=\"4\">");
            //    Response.Output.Write(title + "</FONT></caption><tr><td>");
            //    Response.Output.Write("Advisor Name : ");
            //    Response.Output.Write("</td>");
            //    Response.Output.Write("<td>");
            //    Response.Output.Write(userVo.FirstName + userVo.LastName);
            //    Response.Output.Write("</td></tr>");
            //    Response.Output.Write("<tr><td>");
            //    Response.Output.Write("Customer Name  : ");
            //    Response.Output.Write("</td>");
            //    Response.Output.Write("<td>");
            //    Response.Output.Write(customerVo.FirstName + customerVo.MiddleName + customerVo.LastName);
            //    Response.Output.Write("</td></tr>");
            //    Response.Output.Write("<tr><td>");
            //    Response.Output.Write("Contact Person  : ");
            //    Response.Output.Write("</td>");
            //    Response.Output.Write("<td>");
            //    Response.Output.Write(rmVo.FirstName + rmVo.MiddleName + rmVo.LastName);
            //    Response.Output.Write("</td></tr><tr><td>");
            //    Response.Output.Write("Date : ");
            //    Response.Output.Write("</td><td>");
            //    System.DateTime tDate1 = System.DateTime.Now;
            //    Response.Output.Write(tDate1);
            //    Response.Output.Write("</td></tr>");
            //    Response.Output.Write("</tbody></table>");
            //    // Create a form to contain the grid
            //    gv.Parent.Controls.Add(frm);
            //    frm.Controls.Add(gv);
            //    frm.RenderControl(htw);
            //    Response.Write(sw.ToString());
            //    Response.End();

            //}
            //else if (FileType.ToLower() == "pdf")
            //{
            //    string temp = customerVo.FirstName + customerVo.LastName + title;
            //    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gv.Columns.Count - 1);
            //    table.HeaderRows = 4;
            //    iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);
            //    Phrase phApplicationName = new Phrase(" ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
            //    PdfPCell clApplicationName = new PdfPCell(phApplicationName);
            //    clApplicationName.Border = PdfPCell.NO_BORDER;
            //    clApplicationName.HorizontalAlignment = Element.ALIGN_LEFT;


            //    Phrase phDate = new Phrase(DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL));
            //    PdfPCell clDate = new PdfPCell(phDate);
            //    clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
            //    clDate.Border = PdfPCell.NO_BORDER;


            //    headerTable.AddCell(clApplicationName);
            //    headerTable.AddCell(clDate);
            //    headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

            //    PdfPCell cellHeader = new PdfPCell(headerTable);
            //    cellHeader.Border = PdfPCell.NO_BORDER;
            //    cellHeader.Colspan = gv.Columns.Count - 1;
            //    table.AddCell(cellHeader);

            //    Phrase phHeader = new Phrase(temp, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
            //    PdfPCell clHeader = new PdfPCell(phHeader);
            //    clHeader.Colspan = gv.Columns.Count - 1;
            //    clHeader.Border = PdfPCell.NO_BORDER;
            //    clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            //    table.AddCell(clHeader);


            //    Phrase phSpace = new Phrase("\n");
            //    PdfPCell clSpace = new PdfPCell(phSpace);
            //    clSpace.Border = PdfPCell.NO_BORDER;
            //    clSpace.Colspan = gv.Columns.Count - 1;
            //    table.AddCell(clSpace);

            //    GridViewRow HeaderRow = gv.HeaderRow;
            //    if (HeaderRow != null)
            //    {
            //        string cellText = "";
            //        for (int j = 1; j < gv.Columns.Count; j++)
            //        {

            //            if (j == 2)
            //            {
            //                cellText = "Folio Number";
            //            }
            //            else if (j == 3)
            //            {
            //                cellText = "Scheme Name";
            //            }
            //            else if (j == 4)
            //            {
            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "Current Units";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "Number of Units sold";
            //                }
            //                else if (title == "MFPortfolioNotional")
            //                {
            //                    cellText = "Current Units";
            //                }


            //            }
            //            else if (j == 5)
            //            {
            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "Average Price (Rs)";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "Realized sale Proceeds (Rs)";
            //                }
            //                else if (title == "MFPortfolioNotional")
            //                {
            //                    cellText = "Cost of Acquisition (Rs)";
            //                }


            //            }
            //            else if (j == 6)
            //            {
            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "Cost of Acquisition (Rs)";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "Cost of Sales (Rs)";
            //                }
            //                else if (title == "MFPortfolioNotional")
            //                {
            //                    cellText = "Current NAV (Rs)";
            //                }


            //            }
            //            else if (j == 7)
            //            {
            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "Current NAV (Rs) ";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "Divident Income (Rs)";
            //                }
            //                else if (title == "MFPortfolioNotional")
            //                {
            //                    cellText = "Current Value (Rs)";
            //                }


            //            }
            //            else if (j == 8)
            //            {

            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "Current Value (Rs)";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "Realized P/L (Rs)";
            //                }
            //                else if (title == "MFPortfolioNotional")
            //                {
            //                    cellText = "UnRealized P/L (Rs)";
            //                }

            //            }
            //            else if (j == 9)
            //            {

            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "Dividend Income (Rs)";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "Absolute Return";
            //                }
            //                else if (title == "MFPortfolioNotional")
            //                {
            //                    cellText = "UnRealized Gain/Loss %";
            //                }
            //            }
            //            else if (j == 10)
            //            {

            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "UnRealized P/L (Rs)";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "Annual Return";
            //                }
            //                else if (title == "MFPortfolioNotional")
            //                {
            //                    cellText = "XIRR";
            //                }
            //            }
            //            else if (j == 11)
            //            {

            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "Realized P/L (Rs)";
            //                }
            //                else if (title == "MFPortfolioRealized")
            //                {
            //                    cellText = "XIRR";
            //                }
            //            }
            //            else if (j == 12)
            //            {

            //                if (title == "MFPortfolio")
            //                {
            //                    cellText = "XIRR";
            //                }
            //            }
            //            else
            //            {
            //                cellText = Server.HtmlDecode(gv.HeaderRow.Cells[j].Text);
            //            }
            //            Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD));
            //            table.AddCell(ph);
            //        }

            //    }

            //    for (int i = 0; i < gv.Rows.Count; i++)
            //    {
            //        string cellText = "";
            //        if (gv.Rows[i].RowType == DataControlRowType.DataRow)
            //        {
            //            for (int j = 1; j < gv.Columns.Count; j++)
            //            {
            //                if (j == 1)
            //                {
            //                    cellText = (i + 1).ToString();
            //                }
            //                else if (j == 2)
            //                {
            //                    if (title == "MFPortfolio")
            //                    {
            //                        cellText = ((Label)gv.Rows[i].FindControl("lblFolioHeader")).Text;
            //                    }
            //                    else if (title == "MFPortfolioRealized")
            //                    {
            //                        cellText = ((Label)gv.Rows[i].FindControl("lblRealizedFolioHeader")).Text;
            //                    }
            //                    else if (title == "MFPortfolioNotional")
            //                    {
            //                        cellText = ((Label)gv.Rows[i].FindControl("lblNotionalFolioHeader")).Text;
            //                    }


            //                }
            //                else if (j == 3)
            //                {
            //                    if (title == "MFPortfolio")
            //                    {
            //                        cellText = ((Label)gv.Rows[i].FindControl("lblNameHeader")).Text;
            //                    }
            //                    else if (title == "MFPortfolioRealized")
            //                    {
            //                        cellText = ((Label)gv.Rows[i].FindControl("lblRealizedNameHeader")).Text;
            //                    }
            //                    else if (title == "MFPortfolioNotional")
            //                    {
            //                        cellText = ((Label)gv.Rows[i].FindControl("lblNotionalNameHeader")).Text;
            //                    }
            //                }
            //                else
            //                {
            //                    cellText = Server.HtmlDecode(gv.Rows[i].Cells[j].Text);
            //                }

            //                Phrase ph = new Phrase(cellText, FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL));
            //                iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
            //                table.AddCell(ph);

            //            }

            //        }

            //    }



            //    //Create the PDF Document

            //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //    pdfDoc.Open();
            //    pdfDoc.NewPage();
            //    pdfDoc.Add(table);
            //    pdfDoc.Close();
            //    Response.ContentType = "application/pdf";
            //    temp = "filename=" + temp + ".pdf";
            //    //    Response.AddHeader("content-disposition", "attachment;" + "filename=GridViewExport.pdf");
            //    Response.AddHeader("content-disposition", "attachment;" + temp);
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.Write(pdfDoc);
            //    Response.End();



            //}
        }

        //protected void btnExportPortfolio_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolio.Columns[0].Visible = false;
        //    PrepareGridViewForExport(gvMFPortfolio);
        //    if (rbtnPortfolioExcel.Checked)
        //    {
        //        ExportGridView("Excel", "MFPortfolio", gvMFPortfolio);
        //    }
        //    else if (rbtnPortfolioPdf.Checked)
        //    {
        //        ExportGridView("PDF", "MFPortfolio", gvMFPortfolio);
        //    }
        //    else if (rbtnPortfolioWord.Checked)
        //    {
        //        ExportGridView("Word", "MFPortfolio", gvMFPortfolio);
        //    }
        //    gvMFPortfolio.Columns[0].Visible = true;
        //}

        //protected void btnExportRealized_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolioRealized.Columns[0].Visible = false;
        //    PrepareGridViewForExport(gvMFPortfolioRealized);
        //    if (rbtnRealizedExcel.Checked)
        //    {
        //        ExportGridView("Excel", "MFPortfolioRealized", gvMFPortfolioRealized);
        //    }
        //    else if (rbtnRealizedPdf.Checked)
        //    {
        //        ExportGridView("PDF", "MFPortfolioRealized", gvMFPortfolioRealized);
        //    }
        //    else if (rbtnRealizedWord.Checked)
        //    {
        //        ExportGridView("Word", "MFPortfolioRealized", gvMFPortfolioRealized);
        //    }
        //    gvMFPortfolioRealized.Columns[0].Visible = true;
        //}

        //protected void btnPrintPortfolio_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolio.Columns[0].Visible = false;
        //    PrepareGridViewForExport(gvMFPortfolio);
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewMutualFundPortfolio_dvMFPortfolio','ctrl_ViewMutualFundPortfolio_btnPrintPortfolioGrid');", true);

        //}

        //protected void btnPrintPortfolioGrid_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolio.Columns[0].Visible = true;
        //}

        //protected void btnPrintRealized_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolioRealized.Columns[0].Visible = false;
        //    PrepareGridViewForExport(gvMFPortfolioRealized);
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewMutualFundPortfolio_dvRealizedPortfolio','ctrl_ViewMutualFundPortfolio_btnPrintRealizedGrid');", true);

        //}

        //protected void btnPrintRealizedGrid_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolioRealized.Columns[0].Visible = true;
        //}

        //protected void btnPrintNotional_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolioNotional.Columns[0].Visible = false;
        //    PrepareGridViewForExport(gvMFPortfolioNotional);
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", "Print_Click('ctrl_ViewMutualFundPortfolio_dvNotionalPortfolio','ctrl_ViewMutualFundPortfolio_btnPrintNotionalGrid');", true);

        //}

        //protected void btnPrintNotionalGrid_Click(object sender, EventArgs e)
        //{
        //    gvMFPortfolioNotional.Columns[0].Visible = true;
        //}

        private void BindPerformaceChart()
        {
            AssetBo assetBo = new AssetBo();
            try
            {
                AdvisorVo adviserVo = (AdvisorVo)Session["advisorVo"];

                DataSet dsMFInv = assetBo.GetMFInvAggrCurrentValues(portfolioId, adviserVo.advisorId);
                if (dsMFInv.Tables[0].Rows.Count > 0)
                {
                    if (dsMFInv.Tables[1].Rows.Count > 0)
                    {
                        // Total Assets Chart
                        Series seriesAssets = new Series("seriesMFC");
                        Legend legend = new Legend("AssetsLegend");
                        legend.Enabled = true;
                        string[] XValues = new string[dsMFInv.Tables[1].Rows.Count];
                        double[] YValues = new double[dsMFInv.Tables[1].Rows.Count];
                        int i = 0;
                        seriesAssets.ChartType = SeriesChartType.Pie;

                        foreach (DataRow dr in dsMFInv.Tables[1].Rows)
                        {
                            XValues[i] = dr["MFType"].ToString();
                            YValues[i] = double.Parse(dr["AggrCurrentValue"].ToString());
                            i++;
                        }
                        seriesAssets.Points.DataBindXY(XValues, YValues);
                        //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

                        chrtMFClassification.Series.Clear();
                        chrtMFClassification.Series.Add(seriesAssets);

                        //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
                        //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
                        chrtMFClassification.Legends.Clear();
                        chrtMFClassification.Legends.Add(legend);
                        chrtMFClassification.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
                        chrtMFClassification.Legends["AssetsLegend"].Title = "Assets";
                        chrtMFClassification.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
                        chrtMFClassification.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                        chrtMFClassification.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
                        chrtMFClassification.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

                        chrtMFClassification.ChartAreas[0].Area3DStyle.Enable3D = true;
                        chrtMFClassification.ChartAreas[0].Area3DStyle.Perspective = 50;
                        //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
                        chrtMFClassification.Width = 500;
                        chrtMFClassification.BackColor = System.Drawing.Color.Transparent;
                        chrtMFClassification.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
                        chrtMFClassification.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

                        LegendCellColumn colors = new LegendCellColumn();
                        colors.HeaderText = "Color";
                        colors.ColumnType = LegendCellColumnType.SeriesSymbol;
                        colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
                        chrtMFClassification.Legends["AssetsLegend"].CellColumns.Add(colors);

                        LegendCellColumn asset = new LegendCellColumn();
                        asset.Alignment = ContentAlignment.MiddleLeft;
                        asset.HeaderText = "Asset";
                        asset.Text = "#VALX";
                        chrtMFClassification.Legends["AssetsLegend"].CellColumns.Add(asset);

                        LegendCellColumn assetPercent = new LegendCellColumn();
                        assetPercent.Alignment = ContentAlignment.MiddleLeft;
                        assetPercent.HeaderText = "Asset Percentage";
                        assetPercent.Text = "#PERCENT";
                        chrtMFClassification.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

                        foreach (DataPoint point in chrtMFClassification.Series["seriesMFC"].Points)
                        {
                            point["Exploded"] = "true";
                        }

                        chrtMFClassification.DataBind();
                        //chrtTotalAssets.Series["Assets"]. 
                    }

                }
                else
                {
                    trMFCode.Visible = false;
                    trChart.Visible = false;
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
                FunctionInfo.Add("Method", "ViewMutualFundPortfolio.ascx:BindPerformaceChart()");
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


        //protected void btnPrintGrid_Click(object sender, EventArgs e)
        //{
        //    LoadMFPortfolio();

        //}
        //protected void btnExportExcel_Click(object sender, EventArgs e)
        //{

        //    hdnDownloadFormat.Value = "excel";
        //    if (hdnSelectedTab.Value.ToString() == "1")
        //    {
        //        gvMFPortfolio.Columns[0].Visible = false;
        //        for(int i=0;i<gvMFPortfolio.Rows.Count;i++)
        //        {
        //            if (gvMFPortfolio.Rows[i].RowType!=DataControlRowType.Header)
        //            {
        //                gvMFPortfolio.Rows[i].Cells[7].Text=gvMFPortfolio.Rows[i].Cells[7].Text.Replace(",","");
        //                gvMFPortfolio.Rows[i].Cells[8].Text = gvMFPortfolio.Rows[i].Cells[8].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[10].Text = gvMFPortfolio.Rows[i].Cells[10].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[12].Text = gvMFPortfolio.Rows[i].Cells[12].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[13].Text = gvMFPortfolio.Rows[i].Cells[13].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[14].Text = gvMFPortfolio.Rows[i].Cells[14].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[15].Text = gvMFPortfolio.Rows[i].Cells[15].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[16].Text = gvMFPortfolio.Rows[i].Cells[16].Text.Replace(",", "");

        //            }
        //        }
        //        PrepareGridViewForExport(gvMFPortfolio);
                
        //        ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolio", gvMFPortfolio);
        //    }
        //    else if (hdnSelectedTab.Value.ToString() == "2")
        //    {
        //        gvMFPortfolioRealized.Columns[0].Visible = false;
        //        for (int i = 0; i < gvMFPortfolioRealized.Rows.Count; i++)
        //        {
        //            if (gvMFPortfolioRealized.Rows[i].RowType != DataControlRowType.Header)
        //            {
        //                gvMFPortfolioRealized.Rows[i].Cells[12].Text = gvMFPortfolioRealized.Rows[i].Cells[12].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[7].Text = gvMFPortfolioRealized.Rows[i].Cells[7].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[8].Text = gvMFPortfolioRealized.Rows[i].Cells[8].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[13].Text = gvMFPortfolioRealized.Rows[i].Cells[13].Text.Replace(",", "");

        //                gvMFPortfolioRealized.Rows[i].Cells[10].Text = gvMFPortfolioRealized.Rows[i].Cells[10].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[11].Text = gvMFPortfolioRealized.Rows[i].Cells[11].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[14].Text = gvMFPortfolioRealized.Rows[i].Cells[14].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[15].Text = gvMFPortfolioRealized.Rows[i].Cells[15].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[18].Text = gvMFPortfolioRealized.Rows[i].Cells[18].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[19].Text = gvMFPortfolioRealized.Rows[i].Cells[19].Text.Replace(",", "");

        //            }
        //        }
        //        PrepareGridViewForExport(gvMFPortfolioRealized);
        //        ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioRealized", gvMFPortfolioRealized);
        //    }
        //    else if (hdnSelectedTab.Value.ToString() == "0")
        //    {
        //        gvMFPortfolioNotional.Columns[0].Visible = false;
        //        for (int i = 0; i < gvMFPortfolioNotional.Rows.Count; i++)
        //        {
        //            if (gvMFPortfolioNotional.Rows[i].RowType != DataControlRowType.Header)
        //            {
        //                gvMFPortfolioNotional.Rows[i].Cells[6].Text = gvMFPortfolioNotional.Rows[i].Cells[6].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[7].Text = gvMFPortfolioNotional.Rows[i].Cells[7].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[8].Text = gvMFPortfolioNotional.Rows[i].Cells[8].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[9].Text = gvMFPortfolioNotional.Rows[i].Cells[9].Text.Replace(",", "");

        //                gvMFPortfolioNotional.Rows[i].Cells[10].Text = gvMFPortfolioNotional.Rows[i].Cells[10].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[11].Text = gvMFPortfolioNotional.Rows[i].Cells[11].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[14].Text = gvMFPortfolioNotional.Rows[i].Cells[14].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[15].Text = gvMFPortfolioNotional.Rows[i].Cells[15].Text.Replace(",", "");

        //            }
        //        }
        //        PrepareGridViewForExport(gvMFPortfolioNotional);
        //        ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioNotional", gvMFPortfolioNotional);
        //    }





        //}

        //protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
        //{
        //    hdnDownloadFormat.Value = "excel";
        //    Export();
            
        //}
        //private void Export()
        //{
        //    if (hdnSelectedTab.Value.ToString() == "0")
        //    {
        //        gvMFPortfolioNotional.Columns[0].Visible = false;
        //        for (int i = 0; i < gvMFPortfolioNotional.Rows.Count; i++)
        //        {
        //            if (gvMFPortfolioNotional.Rows[i].RowType != DataControlRowType.Header)
        //            {
        //                gvMFPortfolioNotional.Rows[i].Cells[6].Text = gvMFPortfolioNotional.Rows[i].Cells[6].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[7].Text = gvMFPortfolioNotional.Rows[i].Cells[7].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[8].Text = gvMFPortfolioNotional.Rows[i].Cells[8].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[9].Text = gvMFPortfolioNotional.Rows[i].Cells[9].Text.Replace(",", "");

        //                gvMFPortfolioNotional.Rows[i].Cells[10].Text = gvMFPortfolioNotional.Rows[i].Cells[10].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[11].Text = gvMFPortfolioNotional.Rows[i].Cells[11].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[12].Text = gvMFPortfolioNotional.Rows[i].Cells[12].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[13].Text = gvMFPortfolioNotional.Rows[i].Cells[13].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[14].Text = gvMFPortfolioNotional.Rows[i].Cells[14].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[15].Text = gvMFPortfolioNotional.Rows[i].Cells[15].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[16].Text = gvMFPortfolioNotional.Rows[i].Cells[16].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[17].Text = gvMFPortfolioNotional.Rows[i].Cells[17].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[18].Text = gvMFPortfolioNotional.Rows[i].Cells[18].Text.Replace(",", "");
        //                gvMFPortfolioNotional.Rows[i].Cells[19].Text = gvMFPortfolioNotional.Rows[i].Cells[19].Text.Replace(",", "");

        //            }
        //        }
        //        PrepareGridViewForExport(gvMFPortfolioNotional);
        //        ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioNotional", gvMFPortfolioNotional);

                
        //    }
        //    else if (hdnSelectedTab.Value.ToString() == "1")
        //    {
        //        gvMFPortfolio.Columns[0].Visible = false;
        //        for (int i = 0; i < gvMFPortfolio.Rows.Count; i++)
        //        {
        //            if (gvMFPortfolio.Rows[i].RowType != DataControlRowType.Header)
        //            {
        //                gvMFPortfolio.Rows[i].Cells[5].Text = gvMFPortfolio.Rows[i].Cells[5].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[6].Text = gvMFPortfolio.Rows[i].Cells[6].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[7].Text = gvMFPortfolio.Rows[i].Cells[7].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[8].Text = gvMFPortfolio.Rows[i].Cells[8].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[9].Text = gvMFPortfolio.Rows[i].Cells[10].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[10].Text = gvMFPortfolio.Rows[i].Cells[10].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[11].Text = gvMFPortfolio.Rows[i].Cells[11].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[12].Text = gvMFPortfolio.Rows[i].Cells[12].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[13].Text = gvMFPortfolio.Rows[i].Cells[13].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[14].Text = gvMFPortfolio.Rows[i].Cells[14].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[15].Text = gvMFPortfolio.Rows[i].Cells[15].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[16].Text = gvMFPortfolio.Rows[i].Cells[16].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[17].Text = gvMFPortfolio.Rows[i].Cells[17].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[18].Text = gvMFPortfolio.Rows[i].Cells[18].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[19].Text = gvMFPortfolio.Rows[i].Cells[19].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[20].Text = gvMFPortfolio.Rows[i].Cells[20].Text.Replace(",", "");
        //                gvMFPortfolio.Rows[i].Cells[21].Text = gvMFPortfolio.Rows[i].Cells[21].Text.Replace(",", "");



        //            }
        //        }
        //        PrepareGridViewForExport(gvMFPortfolio);
        //        ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolio", gvMFPortfolio);
        //    }
        //    else if (hdnSelectedTab.Value.ToString() == "2")
        //    {
        //        gvMFPortfolioRealized.Columns[0].Visible = false;
        //        for (int i = 0; i < gvMFPortfolioRealized.Rows.Count; i++)
        //        {
        //            if (gvMFPortfolioRealized.Rows[i].RowType != DataControlRowType.Header)
        //            {
        //                gvMFPortfolioRealized.Rows[i].Cells[6].Text = gvMFPortfolioRealized.Rows[i].Cells[6].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[7].Text = gvMFPortfolioRealized.Rows[i].Cells[7].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[8].Text = gvMFPortfolioRealized.Rows[i].Cells[8].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[9].Text = gvMFPortfolioRealized.Rows[i].Cells[9].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[10].Text = gvMFPortfolioRealized.Rows[i].Cells[10].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[11].Text = gvMFPortfolioRealized.Rows[i].Cells[11].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[12].Text = gvMFPortfolioRealized.Rows[i].Cells[12].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[13].Text = gvMFPortfolioRealized.Rows[i].Cells[13].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[14].Text = gvMFPortfolioRealized.Rows[i].Cells[14].Text.Replace(",", "");
        //                gvMFPortfolioRealized.Rows[i].Cells[15].Text = gvMFPortfolioRealized.Rows[i].Cells[15].Text.Replace(",", "");


        //            }
        //        }
        //        PrepareGridViewForExport(gvMFPortfolioRealized);
        //        ExportGridView(hdnDownloadFormat.Value.ToString(), "MFPortfolioRealized", gvMFPortfolioRealized);

        //    }
        //}

        //protected void imgBtnWord_Click(object sender, ImageClickEventArgs e)
        //{
        //    hdnDownloadFormat.Value = "word";
        //    Export();
        //}

        //protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
        //{
        //    hdnDownloadFormat.Value = "pdf";
        //    Export();
        //}

        //protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)
        //{
        //    hdnDownloadFormat.Value = "print";
        //    Export();
        //}

        private void gvMFPortfolioNotionalExport()
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

        private void gvMFPortfolioExport()
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
                    gvMFPortfolio.Rows[i].Cells[9].Text = gvMFPortfolio.Rows[i].Cells[9].Text.Replace(",", "");
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

        private void gvMFPortfolioRealizedExport()
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

        protected void CalculatePortfolioXIRR(int portfolioId)
        {
            DataTable dtPortfolioXIRR;
            dtPortfolioXIRR = customerPortfolioBo.GetCustomerPortfolioLabelXIRR(portfolioId.ToString()+",");
            if (dtPortfolioXIRR.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPortfolioXIRR.Rows)
                {
                    if (int.Parse(dr["PortfolioId"].ToString()) == portfolioId)
                    {
                        lblPortfolioXIRRValue.Text = double.Parse(dr["XIRR"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                    }

                }
            }
        }

        protected void imgBtnExport1_Click(object sender, ImageClickEventArgs e)
        {
            hdnDownloadFormat.Value = "excel";
            gvMFPortfolioNotionalExport();
        }

        protected void imgBtnExport2_Click(object sender, ImageClickEventArgs e)
        {
            hdnDownloadFormat.Value = "excel";
            gvMFPortfolioExport();
        }

        protected void imgBtnExport3_Click(object sender, ImageClickEventArgs e)
        {
            hdnDownloadFormat.Value = "excel";
            gvMFPortfolioRealizedExport();
        }

        //protected void btnGo_Click(object sender, EventArgs e)
        //{
        //    AdvisorVo adviserVo = (AdvisorVo)Session["advisorVo"];
        //    bool bCheckValuationForDate = false;
        //    tradeDate = DateTime.Parse(txtPickDate.Text);
        //    bCheckValuationForDate = customerPortfolioBo.CheckValuationDoneOrNotForThePickedDate(adviserVo.advisorId, "MF", tradeDate);

        //    if (bCheckValuationForDate == true)
        //    {
        //        msgRecordStatus.Visible = false;
        //        ddlMFClassificationCode.Visible = true;
        //        LoadMFPortfolio();
        //    }
        //    else
        //    {
        //        msgRecordStatus.Visible = true;

        //        lblMessageAll.Visible = true;
        //        lblMessageNotional.Visible = true;
        //        lblMessageRealized.Visible = true;
        //        gvMFPortfolio.DataSource = null;
        //        gvMFPortfolio.DataBind();

        //        chrtMFClassification.DataSource = null;
        //        chrtMFClassification.DataBind();
        //        chrtMFClassification.Visible = false;
        //        ddlMFClassificationCode.Visible = false;



        //        gvMFPortfolioRealized.DataSource = null;
        //        gvMFPortfolioRealized.DataBind();
        //        gvMFPortfolioNotional.DataSource = null;
        //        gvMFPortfolioNotional.DataBind();

        //        btnUpdateNP.Visible = false;
        //        hdnFolioFilter.Value = "";
        //        hdnSchemeFilter.Value = "";
        //        hdnSelectedCategory.Value = "All";
        //        btnPortfolioSearch.Focus();
        //    }
        //}
        
    }
}
