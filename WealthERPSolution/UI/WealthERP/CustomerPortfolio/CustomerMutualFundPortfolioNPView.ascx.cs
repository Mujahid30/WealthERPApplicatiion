using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoCommon;
using WealthERP.Base;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Globalization;
using System.Collections;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using Telerik.Web.UI.GridExcelBuilder;
using BoReports;


namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMutualFundPortfolioNPView : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        MFNetPositionBo mFNetPositionBO = new MFNetPositionBo();
        static int portfolioId;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
        MFPortfolioVo mfPortfolioVo;
        List<MFPortfolioNetPositionVo> mfPortfolioNetPositionList;
        const string strEQConstant = "EQ", strMFConstant = "MF";
        DateTime tradeDate = new DateTime();
        static int intPortfolioListCount;
        string strValuationDate;
        DateTime FromDate;
        DateTime ToDate;
        int C_CustomerId;
        string PortfolioIds;
        DataSet dsSchemeHoldingSector;

        public enum Constants
        {
            EQ = 0,     // explicitly specifying the enum constant values will improve performance
            MF = 1,
            EQDate = 2,
            MFDate = 3
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session[SessionContents.UserVo];
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                if (Session[SessionContents.ValuationDate] == null)
                    GetLatestValuationDate();
                genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
                strValuationDate = genDict[Constants.MFDate.ToString()].ToString();
                lblPickDate.Text = DateTime.Parse(genDict[Constants.MFDate.ToString()].ToString()).ToShortDateString();
                ErrorMessage.Visible = false;
                if (!IsPostBack)
                {
                    //NewBindReturnsGrid();
                    //RealizedBindReturnsGrid();
                    //AllBindReturnsGrid();

                    BindPortfolioDropDown();
                    //SetPanelVisibility(false, false);
                    trNoRecords.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerMutualFundPortfolioNPView.ascx.cs:Page_Load()");
                object[] objects = new object[3];
                objects[0] = userVo;
                objects[1] = customerVo;
                objects[2] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            rgRealized_Init(sender, e);
            rgAll_Init(sender, e);
            rgHoldings_Init(sender, e);
            rgTaxHoldings_Init(sender, e);
            rgTaxRealized_Init(sender, e);
        }

        private void GetMFPortfolioList()
        {
            intPortfolioListCount = 0;

            try
            {
                genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
            
                    mfPortfolioNetPositionList = customerPortfolioBo.GetCustomerMFNetPositions(customerVo.CustomerId, portfolioId);
                    Session["mfPortfolioList"] = mfPortfolioNetPositionList;
              
                if (mfPortfolioNetPositionList != null)
                {
                    intPortfolioListCount = mfPortfolioNetPositionList.Count;
                }
                else
                {
                  
                    //SetPanelVisibility(false, false);
                    trNoRecords.Visible = false;
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
                FunctionInfo.Add("Method", "CustomerMutualFundPortfolioNPView.ascx:GetMFPortfolioList()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = portfolioId;
                objects[2] = genDict;
                objects[3] = mfPortfolioNetPositionList;
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
            //AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                //advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                adviserId = advisorVo.advisorId;


                if (portfolioBo.GetLatestValuationDate(adviserId, Constants.EQ.ToString()) != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, Constants.EQ.ToString()).ToString());
                }
                if (portfolioBo.GetLatestValuationDate(adviserId, Constants.MF.ToString()) != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, Constants.MF.ToString()).ToString());
                }
                genDict.Add(Constants.EQDate.ToString(), EQValuationDate);
                genDict.Add(Constants.MFDate.ToString(), MFValuationDate);
                Session[SessionContents.ValuationDate] = genDict;
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

      

        protected void CalculatePortfolioXIRR(int portfolioId)
        {
            DataTable dtPortfolioXIRR;
            dtPortfolioXIRR = customerPortfolioBo.GetCustomerPortfolioLabelXIRR(portfolioId.ToString());
            if (dtPortfolioXIRR.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPortfolioXIRR.Rows)
                {
                    if (int.Parse(dr["PortfolioId"].ToString()) == portfolioId)
                    {
                        //lblPortfolioXIRRValue.Text = double.Parse(dr["XIRR"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                }
            }
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            //Session[SessionContents.PortfolioId] = portfolioId;
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

        public void lnkBtnReturns_Click(object sender, EventArgs e)
        {
            //SetPanelVisibility(true, false);
            BindReturnsGrid();
            BindPerformaceChart();
        }
        public void lnkBtnTax_Click(object sender, EventArgs e)
        {
            //SetPanelVisibility(false, true);
            BindTaxGrid();
        }
        private void NewBindReturnsGrid()
        {
            
          DataSet dsHoldingReturnDetails = new DataSet();
          dsHoldingReturnDetails = mFNetPositionBO.CreateCustomerMFReturnsHolding(customerVo.CustomerId, ddlPortfolio.SelectedValue.ToString(), null, DateTime.Now);
          rgHoldings.DataSource = dsHoldingReturnDetails;
          rgHoldings.DataBind();
        }
        private void AllBindReturnsGrid()
        {

            DataSet dsAllReturnDetails = new DataSet();
            dsAllReturnDetails = mFNetPositionBO.CreateCustomerMFComprehensive(customerVo.CustomerId, ddlPortfolio.SelectedValue.ToString(), null, DateTime.Now);
            rgAll.DataSource = dsAllReturnDetails;
            rgAll.DataBind();
        }
        private void RealizedBindReturnsGrid()
        {

            DataSet dsRealizedReturnDetails = new DataSet();
            dsRealizedReturnDetails = mFNetPositionBO.CreateCustomerMFReturnsRealized(customerVo.CustomerId, ddlPortfolio.SelectedValue.ToString(), null, DateTime.Now);
            rgRealized.DataSource = dsRealizedReturnDetails;
            rgRealized.DataBind();
        }
        private void BindReturnsGrid()
        {
            string expressonHoldings = "";
            string expressonRealized = "";
            string expressonAll = "";
            object sumObject;
            double totalALLPL = 0;
            double totalALLAbsoluteReturn = 0;
            double totalHoldingPL = 0;
            double totalHoldingAbsoluteReturn = 0;
            double totalRealizedPl = 0;
            double totalRealizedAbsReturn = 0;
            double totalALLInvestedCost = 0;
            double totalRealizedInvestedCost = 0;
            double totalHoldingInvestedCost = 0;
            double totalHoldingMarketValue = 0;
            double TotalPL = 0;
            double TotalInvestcost = 0;

            if (ddlPortfolio.Items.Count == 0 || Session["mfPortfolioList"] == null)
            {
                ReturnsLabelVisibility(true);
            }
            else
            {
                //if (rgHoldings.Items != null)
                //{
                //    categoryType = hdnDdlCategorySelectedValue.Value;

                //    if (categoryType == "Select")
                //        categoryType = "";
                //}

                ReturnsLabelVisibility(false);
                mfPortfolioNetPositionList = new List<MFPortfolioNetPositionVo>();
                mfPortfolioNetPositionList = (List<MFPortfolioNetPositionVo>)Session["mfPortfolioList"];

                DataTable dtReturnsHoldings = new DataTable();
                DataTable dtReturnsAll = new DataTable();
                DataTable dtReturnsRealized = new DataTable();

                //Create MF Portfolio Returns Holdings Datatable
                ReturnsHoldingsDataTableCreation(dtReturnsHoldings);

                //Create MF Portfolio Returns All Datatable
                ReturnsAllDataTableCreation(dtReturnsAll);

                //Create MF Portfolio Returns realized Datatable
                ReturnsRealizedDataTableCreation(dtReturnsRealized);

                DataRow drMFPortfolioHoldings;
                DataRow drMFPortfolioAll;
                DataRow drMFPortfolioRealized;

                foreach (MFPortfolioNetPositionVo mfVo in mfPortfolioNetPositionList)
                {
                    // Populate the Returns Holdings DataTable
                    drMFPortfolioHoldings = dtReturnsHoldings.NewRow();
                    PopulateReturnsHoldDataTable(mfVo, drMFPortfolioHoldings);
                    dtReturnsHoldings.Rows.Add(drMFPortfolioHoldings);

                    // Populate the Returns All DataTable
                    drMFPortfolioAll = dtReturnsAll.NewRow();
                    PopulateReturnsAllDataTable(drMFPortfolioAll, mfVo);
                    dtReturnsAll.Rows.Add(drMFPortfolioAll);

                    // Populate the Returns Realized DataTable
                    drMFPortfolioRealized = dtReturnsRealized.NewRow();
                    PopulateReturnsRealizedDataTable(drMFPortfolioRealized, mfVo);
                    dtReturnsRealized.Rows.Add(drMFPortfolioRealized);
                }

                // Filter DAtatable
                // Get DataRow Array
                // Convert the array into another datatable
                // Bind the datatabke to grid
                //DataRow[] drCategory;
                //drCategory = dtReturnsHoldings.Select(expresson);
                if (hdnReturnsHoldingsCategory.Value == "")
                    expressonHoldings = "OpenUnits <> '0.000'";
                else
                    expressonHoldings = "OpenUnits <> '0.000' AND Category LIKE '%" + hdnReturnsHoldingsCategory.Value + "%'";

                DataView dvReturnsHoldings = new DataView(dtReturnsHoldings, expressonHoldings, "", DataViewRowState.CurrentRows);

                if (hdnReturnsRealizedCategory.Value == "")
                    expressonRealized = "UnitsSold > '0.00'";
                else
                    expressonRealized = "UnitsSold > '0.00' AND Category LIKE '%" + hdnReturnsRealizedCategory.Value + "%'";
                DataView dvReturnsRealized = new DataView(dtReturnsRealized, expressonRealized, "", DataViewRowState.CurrentRows);

                if (hdnReturnsAllCategory.Value != "")
                    expressonAll = "Category LIKE '%" + hdnReturnsAllCategory.Value + "%'";
                DataView dvReturnsAll = new DataView(dtReturnsAll, expressonAll, "", DataViewRowState.CurrentRows);
                //dvCategory.Table = dtReturnsHoldings;
                //dvCategory.RowFilter = "Category = categoryType";
                //DataTable dtRet = new DataTable();
                //dtRet = drCategory.CopyToDataTable();
                //foreach (DataRow row in drCategory)
                //{
                //    dtReturnsHoldings.ImportRow(row);
                //}

                //DataRow[] drTaxRealized = 

                DataTable dtMFReturnsholding = new DataTable();
                dtMFReturnsholding = dvReturnsHoldings.ToTable();
               

                sumObject = dtMFReturnsholding.Compute("Sum(TotalPL)", string.Empty);
                double.TryParse(Convert.ToString(sumObject), out totalHoldingPL);

                sumObject = dtMFReturnsholding.Compute("Sum(InvestedCost)", string.Empty);
                double.TryParse(Convert.ToString(sumObject), out totalHoldingInvestedCost);

                if (totalHoldingInvestedCost != 0)
                    totalHoldingAbsoluteReturn = (totalHoldingPL / totalHoldingInvestedCost) * 100;

                //lblHoldingAbsoluteReturnValue.Text = Math.Round(totalHoldingAbsoluteReturn, 2).ToString();
                //lblHoldingTotalPLValue.Text = Math.Round(totalHoldingPL, 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));


                rgHoldings.DataSource = dtMFReturnsholding;
                rgHoldings.DataBind();
                ViewState["HoldingReturns"] = dtMFReturnsholding;

                if (dtMFReturnsholding.Rows.Count != 0)
                {
                    imgBtnrgHoldings.Visible = true;
                    trNote.Visible = true;
                }
                else
                {
                    imgBtnrgHoldings.Visible = false;
                    trNote.Visible = false;
                }
                DataTable dtMFReturnsAll = new DataTable();
                dtMFReturnsAll = dvReturnsAll.ToTable();
              

                sumObject = dtMFReturnsAll.Compute("Sum(TotalPL)", string.Empty);
                double.TryParse(Convert.ToString(sumObject), out totalALLPL);

                sumObject = dtMFReturnsAll.Compute("Sum(InvestedCost)", string.Empty);
                double.TryParse(Convert.ToString(sumObject), out totalALLInvestedCost);

              
                if (totalALLInvestedCost != 0)
                    totalALLAbsoluteReturn = (totalALLPL / totalALLInvestedCost) * 100;

                //lblALLAbsoluteReturnsValue.Text = Math.Round(totalALLAbsoluteReturn, 2).ToString();
                //lblALLTotalPLValue.Text = Math.Round(totalALLPL, 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                rgAll.DataSource = dtMFReturnsAll;
                rgAll.DataBind();
                ViewState["AllReturns"] = dtMFReturnsAll;

                if (dtMFReturnsAll.Rows.Count != 0)
                {
                    imgBtnrgAll.Visible = true;
                    trNote.Visible = true;
                }
                else
                {
                    imgBtnrgAll.Visible = false;
                    trNote.Visible = true;
                }
               
                DataTable dtMFReturnsRealized = new DataTable();
                dtMFReturnsRealized = dvReturnsRealized.ToTable();

                //foreach (DataRow dr in dtMFReturnsRealized.Rows)
                //{
                //    if (dr["TotalPL"].ToString() != "N/A")
                //    {
                //        totalRealizedPl = totalRealizedPl + double.Parse(dr["TotalPL"].ToString());
                //    }
                //    if (dr["InvestedCost"].ToString() != "N/A")
                //    {
                //        totalRealizedInvestedCost = totalRealizedInvestedCost + double.Parse(dr["InvestedCost"].ToString());
                //    }
                //}

                sumObject = dtMFReturnsRealized.Compute("Sum(TotalPL)", string.Empty);
                double.TryParse(Convert.ToString(sumObject), out totalRealizedPl);

                sumObject = dtMFReturnsRealized.Compute("Sum(InvestedCost)", string.Empty);
                double.TryParse(Convert.ToString(sumObject), out totalRealizedInvestedCost);

                if (totalRealizedInvestedCost != 0)
                    totalRealizedAbsReturn = (totalRealizedPl / totalRealizedInvestedCost) * 100;

                //lblRealizedAbsoluteReturnValue.Text = Math.Round(totalRealizedAbsReturn, 2).ToString();
                //lblRealizedTotalPLValue.Text = Math.Round(totalRealizedPl, 2).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));

                rgRealized.DataSource = dtMFReturnsRealized;
                if (dtMFReturnsRealized.Rows.Count != 0)
                {
                    imgBtnrgRealized.Visible = true;
                   // trNote.Visible = false;
                }
                else
                {
                    imgBtnrgRealized.Visible = false;
                    //trNote.Visible = false;
                }
                rgRealized.DataBind();
                ViewState["RealizedReturns"] = dtMFReturnsRealized;


            }
        }

        private void BindTaxGrid()
        {
            string expressonTaxHoldings = "";
            string expressonTaxRealized = "";


            if (ddlPortfolio.Items.Count == 0 || Session["mfPortfolioList"] == null)
            {
                TaxLabelVisibility(true);
            }
            else
            {
                TaxLabelVisibility(false);
                mfPortfolioNetPositionList = new List<MFPortfolioNetPositionVo>();
                mfPortfolioNetPositionList = (List<MFPortfolioNetPositionVo>)Session["mfPortfolioList"];

                DataTable dtTaxHoldings = new DataTable();
                DataTable dtTaxRealized = new DataTable();

                //Create MF Portfolio Tax Holdings Datatable
                TaxHoldingsDataTableCreation(dtTaxHoldings);

                //Create MF Portfolio Tax realized Datatable
                TaxRealizedDataTableCreation(dtTaxRealized);

                DataRow drTaxHoldings;
                DataRow drTaxRealized;

                foreach (MFPortfolioNetPositionVo mfVo in mfPortfolioNetPositionList)
                {
                    // Populate the Tax Holdings DataTable
                    drTaxHoldings = dtTaxHoldings.NewRow();
                    PopulateTaxHoldDataTable(drTaxHoldings, mfVo);
                    dtTaxHoldings.Rows.Add(drTaxHoldings);

                    // Populate the Tax Realized DataTable
                    drTaxRealized = dtTaxRealized.NewRow();
                    PopulateTaxRealizedDataTable(drTaxRealized, mfVo);
                    dtTaxRealized.Rows.Add(drTaxRealized);
                }

                if (hdnTaxRealizedCategory.Value == "")
                    expressonTaxRealized = "RedeemedAmount > '0.00'";
                else
                    expressonTaxRealized = "RedeemedAmount > '0.00' AND Category LIKE '%" + hdnTaxRealizedCategory.Value + "%'";

                DataView dvTaxRealized = new DataView(dtTaxRealized, expressonTaxRealized, "", DataViewRowState.CurrentRows);

                if (hdnTaxHoldingsCategory.Value == "")
                    expressonTaxHoldings = "OpenUnits <> '0.000'";
                else
                    expressonTaxHoldings = "OpenUnits <> '0.000' AND Category LIKE '%" + hdnTaxHoldingsCategory.Value + "%'";

                DataView dvTaxHoldings = new DataView(dtTaxHoldings, expressonTaxHoldings, "", DataViewRowState.CurrentRows);

                ViewState["TaxHoldings"] = dvTaxHoldings.ToTable();
                //rgTaxHoldings.DataSource = dvTaxHoldings.ToTable();
                //rgTaxHoldings.DataBind();

                if (dtTaxHoldings.Rows.Count != 0)
                {
                    //imgBtnrgTaxHoldings.Visible = true;
                    trNote.Visible = true;
                }
                else
                {
                    //imgBtnrgTaxHoldings.Visible = false;
                    trNote.Visible = false;
                }
                ViewState["TaxRealized"] = dvTaxRealized.ToTable();
                //rgTaxRealized.DataSource = dvTaxRealized.ToTable();
                //rgTaxRealized.DataBind();

                if (dtTaxRealized.Rows.Count != 0)
                {
                    //imgBtnrgTaxRealized.Visible = true;
                    trNote.Visible = false;
                }
                else
                {
                    //imgBtnrgTaxRealized.Visible = false;
                    trNote.Visible = false;
                }
            }
        }

        private static void PopulateReturnsRealizedDataTable(DataRow drMFPortfolioRealized, MFPortfolioNetPositionVo mfVo)
        {
            drMFPortfolioRealized[0] = mfVo.MFPortfolioId;
            drMFPortfolioRealized[1] = mfVo.AccountId;
            drMFPortfolioRealized[2] = mfVo.AssetInstrumentCategoryName;
            drMFPortfolioRealized[3] = mfVo.SchemePlan;
            drMFPortfolioRealized[4] = mfVo.FolioNumber;
            if (mfVo.ReturnsRealizedInvestedCost != 0)
                drMFPortfolioRealized[5] = mfVo.ReturnsRealizedInvestedCost.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[5] = "0.00";

            if (mfVo.SalesQuantity != 0)
                drMFPortfolioRealized[6] = mfVo.SalesQuantity.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[6] = "0.00";

            if (mfVo.RedeemedAmount != 0)
                drMFPortfolioRealized[7] = mfVo.RedeemedAmount.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[7] = "0.00";

            if (mfVo.ReturnsRealizedDVPAmt != 0)
                drMFPortfolioRealized[8] = mfVo.ReturnsRealizedDVPAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[8] = "0.00";

            if (mfVo.ReturnsRealizedTotalDividends != 0)
                drMFPortfolioRealized[9] = mfVo.ReturnsRealizedTotalDividends.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[9] = "0.00";

            if (mfVo.ReturnsRealizedTotalPL != 0)
                drMFPortfolioRealized[10] = mfVo.ReturnsRealizedTotalPL.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[10] = "0.00";

            if (mfVo.ReturnsRealizedAbsReturn != 0)
                drMFPortfolioRealized[11] = mfVo.ReturnsRealizedAbsReturn.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[11] = "0.00";

            // Showing RetunAll XIRR instead of realized XIRR(as per MJ discussion)
            if (mfVo.ReturnsAllTotalXIRR != 0)
                drMFPortfolioRealized[12] = mfVo.ReturnsAllTotalXIRR.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[12] = "0.00";

            drMFPortfolioRealized[13] = mfVo.AMCCode;
            drMFPortfolioRealized[14] = mfVo.SchemePlanCode;
            drMFPortfolioRealized[15] = mfVo.AssetInstrumentSubCategoryName;
            if (mfVo.FolioStartDate == DateTime.MinValue)
                drMFPortfolioRealized[16] = "N/A";
            else
                //drMFPortfolioRealized[16] = mfVo.FolioStartDate.ToString("D");
                drMFPortfolioRealized[16] = mfVo.FolioStartDate.ToShortDateString();

            if (mfVo.InvestmentStartDate == DateTime.MinValue)
                drMFPortfolioRealized[17] = "N/A";
            else
                //drMFPortfolioRealized[16] = mfVo.FolioStartDate.ToString("D");
                drMFPortfolioRealized[17] = mfVo.InvestmentStartDate.ToShortDateString();
            drMFPortfolioRealized["CMFNP_ValuationDate"] = mfVo.ValuationDate.ToShortDateString();


        }

        private static void PopulateReturnsAllDataTable(DataRow drMFPortfolioAll, MFPortfolioNetPositionVo mfVo)
        {
            drMFPortfolioAll[0] = mfVo.MFPortfolioId;
            drMFPortfolioAll[1] = mfVo.AccountId;
            drMFPortfolioAll[2] = mfVo.AssetInstrumentCategoryName;
            drMFPortfolioAll[3] = mfVo.SchemePlan;
            drMFPortfolioAll[4] = mfVo.FolioNumber;

            if (mfVo.NetHoldings > 0 || mfVo.NetHoldings == 0)
            {
                if (mfVo.ReturnsHoldPurchaseUnit != 0)
                    drMFPortfolioAll[5] = mfVo.ReturnsHoldPurchaseUnit.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[5] = "0.00";

                if (mfVo.ReturnsHoldDVRUnits != 0)
                    drMFPortfolioAll[6] = mfVo.ReturnsHoldDVRUnits.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[6] = "0.00";

                if (mfVo.NetHoldings != 0)
                    drMFPortfolioAll[7] = mfVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[7] = "0.00";

                if (mfVo.ReturnsAllPrice != 0)
                    drMFPortfolioAll[8] = mfVo.ReturnsAllPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[8] = "N/A";

                if (mfVo.InvestedCost != 0)
                    drMFPortfolioAll[9] = mfVo.InvestedCost.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[9] = "0.00";

                if (mfVo.MarketPrice != 0)
                    drMFPortfolioAll[10] = mfVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[10] = "0.00";

                if (mfVo.CurrentValue != 0)
                    drMFPortfolioAll[11] = mfVo.CurrentValue.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[11] = "0.00";

                if (mfVo.SalesQuantity != 0)
                    drMFPortfolioAll[12] = mfVo.SalesQuantity.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[12] = "0.00";

                if (mfVo.RedeemedAmount != 0)
                    drMFPortfolioAll[13] = mfVo.RedeemedAmount.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[13] = "0.00";

                if (mfVo.ReturnsAllDVPAmt != 0)
                    drMFPortfolioAll[14] = mfVo.ReturnsAllDVPAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[14] = "0.00";

                if (mfVo.ReturnsAllTotalPL != 0)
                    drMFPortfolioAll[15] = mfVo.ReturnsAllTotalPL.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[15] = "0.00";

                if (mfVo.ReturnsAllAbsReturn != 0)
                    drMFPortfolioAll[16] = mfVo.ReturnsAllAbsReturn.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[16] = "0.00";

                if (mfVo.ReturnsAllDVRAmt != 0)
                    drMFPortfolioAll[17] = mfVo.ReturnsAllDVRAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[17] = "0";

                if (mfVo.ReturnsAllTotalXIRR != 0)
                    drMFPortfolioAll[18] = mfVo.ReturnsAllTotalXIRR.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[18] = "0.00";

                if (mfVo.ReturnsAllTotalDividends != 0)
                    drMFPortfolioAll[19] = mfVo.ReturnsAllTotalDividends.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioAll[19] = "0.00";

                drMFPortfolioAll[20] = mfVo.AMCCode;
                drMFPortfolioAll[21] = mfVo.SchemePlanCode;
                drMFPortfolioAll[22] = mfVo.AssetInstrumentSubCategoryName;
                if (mfVo.FolioStartDate == DateTime.MinValue)
                    drMFPortfolioAll[23] = "N/A";
                else
                    //drMFPortfolioAll[23] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioAll[23] = mfVo.FolioStartDate.ToShortDateString();

                if (mfVo.InvestmentStartDate == DateTime.MinValue)
                    drMFPortfolioAll[24] = "N/A";
                else
                    //drMFPortfolioAll[23] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioAll[24] = mfVo.InvestmentStartDate.ToShortDateString();
                if (mfVo.NavDate == DateTime.MinValue)
                    drMFPortfolioAll[25] = "N/A";
                else
                    drMFPortfolioAll[25] = mfVo.NavDate.ToShortDateString();
                drMFPortfolioAll["CMFNP_ValuationDate"] = mfVo.ValuationDate.ToShortDateString();


            }
            else
            {
                drMFPortfolioAll[5] = "0";
                drMFPortfolioAll[6] = "0";
                drMFPortfolioAll[7] = mfVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                drMFPortfolioAll[8] = "0";
                drMFPortfolioAll[9] = "0";
                drMFPortfolioAll[10] = mfVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                drMFPortfolioAll[11] = "0";
                drMFPortfolioAll[12] = "0";
                drMFPortfolioAll[13] = "0";
                drMFPortfolioAll[14] = "0";
                drMFPortfolioAll[15] = "0";
                drMFPortfolioAll[16] = "0";
                drMFPortfolioAll[17] = "0";
                drMFPortfolioAll[18] = "0";
                drMFPortfolioAll[19] = "0";
                drMFPortfolioAll[20] = mfVo.AMCCode;
                drMFPortfolioAll[21] = mfVo.SchemePlanCode;
                drMFPortfolioAll[22] = mfVo.AssetInstrumentSubCategoryName;
                if (mfVo.FolioStartDate == DateTime.MinValue)
                    drMFPortfolioAll[23] = "0";
                else
                    //drMFPortfolioAll[23] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioAll[23] = mfVo.FolioStartDate.ToShortDateString();

                if (mfVo.InvestmentStartDate == DateTime.MinValue)
                    drMFPortfolioAll[24] = " ";
                else
                    //drMFPortfolioAll[23] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioAll[24] = mfVo.InvestmentStartDate.ToShortDateString();
                if (mfVo.NavDate == DateTime.MinValue)
                    drMFPortfolioAll[25] = "0";
                else
                    drMFPortfolioAll[25] = mfVo.NavDate.ToShortDateString();
                drMFPortfolioAll["CMFNP_ValuationDate"] = mfVo.ValuationDate.ToShortDateString();

            }
        }

        private static void PopulateReturnsHoldDataTable(MFPortfolioNetPositionVo mfVo, DataRow drMFPortfolioHoldings)
        {
            drMFPortfolioHoldings[0] = mfVo.MFPortfolioId;
            drMFPortfolioHoldings[1] = mfVo.AccountId;
            if (mfVo.AssetInstrumentCategoryName != null)
                drMFPortfolioHoldings[2] = mfVo.AssetInstrumentCategoryName;
            drMFPortfolioHoldings[3] = mfVo.SchemePlan;
            drMFPortfolioHoldings[4] = mfVo.FolioNumber;

            if (mfVo.NetHoldings > 0)
            {
                if (mfVo.ReturnsHoldPurchaseUnit != 0)
                    drMFPortfolioHoldings[5] = mfVo.ReturnsHoldPurchaseUnit.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[5] = "0.00";

                if (mfVo.ReturnsHoldDVRUnits != 0)
                    drMFPortfolioHoldings[6] = mfVo.ReturnsHoldDVRUnits.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[6] = "0.00";

                if (mfVo.NetHoldings != 0)
                    drMFPortfolioHoldings[7] = mfVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[7] = "0.00";

                if (mfVo.InvestedCost != 0)
                    drMFPortfolioHoldings[8] = mfVo.ReturnsHoldAcqCost.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[8] = "0.00";

                if (mfVo.MarketPrice != 0)
                    drMFPortfolioHoldings[9] = mfVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[9] = "0.00";

                if (mfVo.CurrentValue != 0)
                    drMFPortfolioHoldings[10] = mfVo.CurrentValue.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[10] = "0.00";

                if (mfVo.ReturnsHoldDVPAmt != 0)
                    drMFPortfolioHoldings[11] = mfVo.ReturnsHoldDVPAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[11] = "0.00";

                if (mfVo.ReturnsHoldTotalPL != 0)
                    drMFPortfolioHoldings[12] = mfVo.ReturnsHoldTotalPL.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[12] = "0";

                if (mfVo.ReturnsHoldAbsReturn != 0)
                    drMFPortfolioHoldings[13] = mfVo.ReturnsHoldAbsReturn.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[13] = "0.00";

                // Showing RetunAll XIRR instead of Holding XIRR(as per MJ discussion)
                if (mfVo.ReturnsAllTotalXIRR != 0)
                    drMFPortfolioHoldings[14] = mfVo.ReturnsAllTotalXIRR.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[14] = "0.00";

                drMFPortfolioHoldings[15] = mfVo.AMCCode;
                drMFPortfolioHoldings[16] = mfVo.SchemePlanCode;
                drMFPortfolioHoldings[17] = mfVo.AssetInstrumentSubCategoryName;
                if (mfVo.FolioStartDate == DateTime.MinValue)
                    drMFPortfolioHoldings[18] = "N/A";
                else
                    //drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToShortDateString();


                if (mfVo.InvestmentStartDate == DateTime.MinValue)
                    drMFPortfolioHoldings[19] = "N/A";
                else
                    //drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioHoldings[19] = mfVo.InvestmentStartDate.ToShortDateString();
                if (mfVo.ReturnHoldDVRAmounts != 0)
                    drMFPortfolioHoldings["CMFNP_RET_Hold_DVRAmounts"] = mfVo.ReturnHoldDVRAmounts.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings["CMFNP_RET_Hold_DVRAmounts"] = "0.00";

                if (mfVo.NavDate == DateTime.MinValue)
                    drMFPortfolioHoldings[21] = "N/A";
                else
                    drMFPortfolioHoldings[21] = mfVo.NavDate.ToShortDateString();

                if (mfVo.AnnualisedReturns != 0)
                    drMFPortfolioHoldings[22] = mfVo.AnnualisedReturns.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[22] = "0";
                if (mfVo.WeightageNAV != 0)
                    drMFPortfolioHoldings[23] = mfVo.WeightageNAV.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drMFPortfolioHoldings[23] = "0.00";
                if (mfVo.WeightageDays != 0)
                    drMFPortfolioHoldings[24] = mfVo.WeightageDays;
                else
                    drMFPortfolioHoldings[24] = "0";
                drMFPortfolioHoldings["CMFNP_ValuationDate"] = mfVo.ValuationDate.ToShortDateString();
                
            }
            else
            {
                drMFPortfolioHoldings[5] = "0";
                drMFPortfolioHoldings[6] = "0";
                drMFPortfolioHoldings[7] = mfVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                drMFPortfolioHoldings[8] = "0";
                drMFPortfolioHoldings[9] = mfVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                drMFPortfolioHoldings[10] = "0";
                drMFPortfolioHoldings[11] = "0";
                drMFPortfolioHoldings[12] = "0";
                drMFPortfolioHoldings[13] = "0";
                drMFPortfolioHoldings[14] = "0";
                drMFPortfolioHoldings[15] = mfVo.AMCCode;
                drMFPortfolioHoldings[16] = mfVo.SchemePlanCode;
                drMFPortfolioHoldings[17] = mfVo.AssetInstrumentSubCategoryName;
                if (mfVo.FolioStartDate == DateTime.MinValue)
                    drMFPortfolioHoldings[18] = "0";
                else
                    //drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToShortDateString();


                if (mfVo.InvestmentStartDate == DateTime.MinValue)
                    drMFPortfolioHoldings[19] = " ";
                else
                    //drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToString("D");
                    drMFPortfolioHoldings[19] = mfVo.InvestmentStartDate.ToShortDateString();
                if (mfVo.NavDate == DateTime.MinValue)
                    drMFPortfolioHoldings[21] = "0";
                else
                    drMFPortfolioHoldings[21] = mfVo.NavDate.ToShortDateString();

                drMFPortfolioHoldings["CMFNP_RET_Hold_DVRAmounts"] = "0";
                drMFPortfolioHoldings[22] = "0";
                drMFPortfolioHoldings[23] = "0";
                drMFPortfolioHoldings[24] = "0";



                //drMFPortfolioHoldings[5] = "N/A";
                //drMFPortfolioHoldings[6] = "N/A";
                //drMFPortfolioHoldings[7] = mfVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                //drMFPortfolioHoldings[8] = "N/A";
                //drMFPortfolioHoldings[9] = mfVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                //drMFPortfolioHoldings[10] = "N/A";
                //drMFPortfolioHoldings[11] = "N/A";
                //drMFPortfolioHoldings[12] = "N/A";
                //drMFPortfolioHoldings[13] = "N/A";
                //drMFPortfolioHoldings[14] = "N/A";
                //drMFPortfolioHoldings[15] = mfVo.AMCCode;
                //drMFPortfolioHoldings[16] = mfVo.SchemePlanCode;
                //drMFPortfolioHoldings[17] = mfVo.AssetInstrumentSubCategoryName;
                //if (mfVo.FolioStartDate == DateTime.MinValue)
                //    drMFPortfolioHoldings[18] = "N/A";
                //else
                //    //drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToString("D");
                //    drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToShortDateString();


                //if (mfVo.InvestmentStartDate == DateTime.MinValue)
                //    drMFPortfolioHoldings[19] = "N/A";
                //else
                //    //drMFPortfolioHoldings[18] = mfVo.FolioStartDate.ToString("D");
                //    drMFPortfolioHoldings[19] = mfVo.InvestmentStartDate.ToShortDateString();
                //if (mfVo.NavDate == DateTime.MinValue)
                //    drMFPortfolioHoldings[21] = "N/A";
                //else
                //    drMFPortfolioHoldings[21] = mfVo.NavDate.ToShortDateString();

                //drMFPortfolioHoldings["CMFNP_RET_Hold_DVRAmounts"] = "N/A";
                //drMFPortfolioHoldings[22] = "N/A";
                //drMFPortfolioHoldings[23] = "N/A";
                //drMFPortfolioHoldings[24] = "N/A";
                //drMFPortfolioHoldings["CMFNP_RET_Hold_DVRAmounts"] = "N/A";
                //drMFPortfolioHoldings[22] = "N/A";
                //drMFPortfolioHoldings[23] = "N/A";
                //drMFPortfolioHoldings[24] = "N/A";
                drMFPortfolioHoldings["CMFNP_ValuationDate"] = mfVo.ValuationDate.ToShortDateString();

            }
        }

        private static void PopulateTaxHoldDataTable(DataRow drTaxHoldings, MFPortfolioNetPositionVo mfVo)
        {
            drTaxHoldings[0] = mfVo.MFPortfolioId;
            drTaxHoldings[1] = mfVo.AccountId;
            drTaxHoldings[2] = mfVo.AssetInstrumentCategoryName;
            drTaxHoldings[3] = mfVo.SchemePlan;
            drTaxHoldings[4] = mfVo.FolioNumber;

            if (mfVo.NetHoldings > 0)
            {
                if (mfVo.NetHoldings != 0)
                    drTaxHoldings[5] = mfVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drTaxHoldings[5] = "0.00";

                if (mfVo.TaxHoldBalanceAmt != 0)
                    drTaxHoldings[6] = mfVo.TaxHoldBalanceAmt.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drTaxHoldings[6] = "0.00";

                if (mfVo.MarketPrice != 0)
                    drTaxHoldings[7] = mfVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drTaxHoldings[7] = "0.00";

                if (mfVo.CurrentValue != 0)
                    drTaxHoldings[8] = mfVo.CurrentValue.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drTaxHoldings[8] = "0.00";

                if (mfVo.TaxHoldTotalPL != 0)
                    drTaxHoldings[9] = mfVo.TaxHoldTotalPL.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drTaxHoldings[9] = "0.00";

                if (mfVo.TaxHoldEligibleSTCG != 0)
                    drTaxHoldings[10] = mfVo.TaxHoldEligibleSTCG.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drTaxHoldings[10] = "0.00";

                if (mfVo.TaxHoldEligibleLTCG != 0)
                    drTaxHoldings[11] = mfVo.TaxHoldEligibleLTCG.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
                else
                    drTaxHoldings[11] = "0.00";

                drTaxHoldings[12] = mfVo.AMCCode;
                drTaxHoldings[13] = mfVo.SchemePlanCode;
                drTaxHoldings[14] = mfVo.AssetInstrumentSubCategoryName;
                if (mfVo.FolioStartDate == DateTime.MinValue)
                    drTaxHoldings[15] = "0.00";
                else
                    //drTaxHoldings[15] = mfVo.FolioStartDate.ToString("D");
                    drTaxHoldings[15] = mfVo.FolioStartDate.ToShortDateString();
                if (mfVo.InvestmentStartDate == DateTime.MinValue)
                    drTaxHoldings[16] = "N/A";
                else
                    //drTaxHoldings[15] = mfVo.FolioStartDate.ToString("D");
                    drTaxHoldings[16] = mfVo.InvestmentStartDate.ToShortDateString();
                if (mfVo.NavDate == DateTime.MinValue)
                    drTaxHoldings[17] = "N/A";
                else
                    drTaxHoldings[17] = mfVo.NavDate.ToShortDateString();
                drTaxHoldings["CMFNP_ValuationDate"] = mfVo.ValuationDate.ToShortDateString();

            }

            else
            {
                drTaxHoldings[5] = mfVo.NetHoldings.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
                drTaxHoldings[6] = "0";
                drTaxHoldings[7] = mfVo.MarketPrice.ToString("n4", CultureInfo.CreateSpecificCulture("hi-IN"));
                drTaxHoldings[8] = "0";
                drTaxHoldings[9] = "0";
                drTaxHoldings[10] = "0";
                drTaxHoldings[11] = "0";
                drTaxHoldings[12] = mfVo.AMCCode;
                drTaxHoldings[13] = mfVo.SchemePlanCode;
                drTaxHoldings[14] = mfVo.AssetInstrumentSubCategoryName;
                if (mfVo.FolioStartDate == DateTime.MinValue)
                    drTaxHoldings[15] = "0.00";
                else
                    //drTaxHoldings[15] = mfVo.FolioStartDate.ToString("D");
                    drTaxHoldings[15] = mfVo.FolioStartDate.ToShortDateString();
                if (mfVo.InvestmentStartDate == DateTime.MinValue)
                    drTaxHoldings[16] = " ";
                else
                    //drTaxHoldings[15] = mfVo.FolioStartDate.ToString("D");
                    drTaxHoldings[16] = mfVo.InvestmentStartDate.ToShortDateString();
                if (mfVo.NavDate == DateTime.MinValue)
                    drTaxHoldings[17] = "0";
                else
                    drTaxHoldings[17] = mfVo.NavDate.ToShortDateString();
            }
        }

        private static void PopulateTaxRealizedDataTable(DataRow drTaxRealized, MFPortfolioNetPositionVo mfVo)
        {
            drTaxRealized[0] = mfVo.MFPortfolioId;
            drTaxRealized[1] = mfVo.AccountId;
            drTaxRealized[2] = mfVo.AssetInstrumentCategoryName;
            drTaxRealized[3] = mfVo.SchemePlan;
            drTaxRealized[4] = mfVo.FolioNumber;

            if (mfVo.TaxRealizedAcqCost != 0)
                drTaxRealized[5] = mfVo.TaxRealizedAcqCost.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[5] = "0.00";

            if (mfVo.RedeemedAmount != 0)
                drTaxRealized[6] = mfVo.RedeemedAmount.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[6] = "0.00";

            if (mfVo.TaxRealizedTotalPL != 0)
                drTaxRealized[7] = mfVo.TaxRealizedTotalPL.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[7] = "0.00";

            if (mfVo.TaxRealizedSTCG != 0)
                drTaxRealized[8] = mfVo.TaxRealizedSTCG.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[8] = "0.00";

            if (mfVo.TaxRealizedLTCG != 0)
                drTaxRealized[9] = mfVo.TaxRealizedLTCG.ToString("n0", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[9] = "0.00";

            drTaxRealized[10] = mfVo.AMCCode;
            drTaxRealized[11] = mfVo.SchemePlanCode;
            drTaxRealized[12] = mfVo.AssetInstrumentSubCategoryName;

            if (mfVo.SalesQuantity != 0)
                drTaxRealized[13] = mfVo.SalesQuantity.ToString("n3", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[13] = "0.000";
            if (mfVo.FolioStartDate == DateTime.MinValue)
                drTaxRealized[14] = "N/A";
            else
                //drTaxRealized[14] = mfVo.FolioStartDate.ToString("D");
                drTaxRealized[14] = mfVo.FolioStartDate.ToShortDateString();

            if (mfVo.InvestmentStartDate == DateTime.MinValue)
                drTaxRealized[15] = "N/A";
            else
                //drTaxRealized[14] = mfVo.FolioStartDate.ToString("D");
                drTaxRealized[15] = mfVo.InvestmentStartDate.ToShortDateString();
            drTaxRealized["CMFNP_ValuationDate"] = mfVo.ValuationDate.ToShortDateString();

        }

        private void ReturnsLabelVisibility(bool blVisibility)
        {
            lblMessageHoldings.Visible = blVisibility;
            lblMessageAll.Visible = blVisibility;
            lblMessageRealized.Visible = blVisibility;
        }

        private void TaxLabelVisibility(bool blVisibility)
        {
            //lblMessageTaxHoldings.Visible = blVisibility;
            //lblTaxRealized.Visible = blVisibility;
        }

        private void SetReturnsGridsNull()
        {
            rgHoldings.DataSource = null;
            rgHoldings.DataBind();
            rgAll.DataSource = null;
            rgAll.DataBind();
            rgRealized.DataSource = null;
            rgRealized.DataBind();
        }

        private void SetTaxGridsNull()
        {
            //rgTaxHoldings.DataSource = null;
            //rgTaxHoldings.DataBind();
            //rgTaxRealized.DataSource = null;
            //rgTaxRealized.DataBind();
        }

        private void ReturnsHoldingsDataTableCreation(DataTable dtReturnsHoldings)
        {
            dtReturnsHoldings.Columns.Add("MFNPId");
            dtReturnsHoldings.Columns.Add("AccountId");
            dtReturnsHoldings.Columns.Add("Category");
            dtReturnsHoldings.Columns.Add("Scheme", typeof(string));
            dtReturnsHoldings.Columns.Add("FolioNum");
            dtReturnsHoldings.Columns.Add("PurchasedUnits", typeof(double));
            dtReturnsHoldings.Columns.Add("DVRUnits", typeof(double));
            dtReturnsHoldings.Columns.Add("OpenUnits", typeof(double));
            dtReturnsHoldings.Columns.Add("InvestedCost", typeof(double));
            dtReturnsHoldings.Columns.Add("NAV", typeof(double));
            dtReturnsHoldings.Columns.Add("MarketValue", typeof(double));
            dtReturnsHoldings.Columns.Add("DVP", typeof(double));
            dtReturnsHoldings.Columns.Add("TotalPL", typeof(double));
            dtReturnsHoldings.Columns.Add("AbsoluteReturn", typeof(double));
            dtReturnsHoldings.Columns.Add("XIRR", typeof(double));
            dtReturnsHoldings.Columns.Add("AMCCode");
            dtReturnsHoldings.Columns.Add("SchemeCode");
            dtReturnsHoldings.Columns.Add("SubCategoryName");
            dtReturnsHoldings.Columns.Add("FolioStartDate");
            dtReturnsHoldings.Columns.Add("InvestmentStartDate");
            dtReturnsHoldings.Columns.Add("CMFNP_RET_Hold_DVRAmounts", typeof(double));
            dtReturnsHoldings.Columns.Add("CMFNP_NAVDate");
            dtReturnsHoldings.Columns.Add("weightage returns", typeof(double));
            dtReturnsHoldings.Columns.Add("Weighted NAV", typeof(double));
            dtReturnsHoldings.Columns.Add("Weighted Days");
            dtReturnsHoldings.Columns.Add("CMFNP_ValuationDate");
         
        }

        private void ReturnsAllDataTableCreation(DataTable dtReturnsAll)
        {
            dtReturnsAll.Columns.Add("MFNPId");
            dtReturnsAll.Columns.Add("AccountId");
            dtReturnsAll.Columns.Add("Category");
            dtReturnsAll.Columns.Add("Scheme");
            dtReturnsAll.Columns.Add("FolioNum");
            dtReturnsAll.Columns.Add("PurchasedUnits", typeof(double));
            dtReturnsAll.Columns.Add("DVRUnits", typeof(double));
            dtReturnsAll.Columns.Add("OpenUnits", typeof(double));
            dtReturnsAll.Columns.Add("Price");
            dtReturnsAll.Columns.Add("InvestedCost", typeof(double));
            dtReturnsAll.Columns.Add("NAV", typeof(double));
            dtReturnsAll.Columns.Add("CurrentValue", typeof(double));
            dtReturnsAll.Columns.Add("UnitsSold", typeof(double));
            dtReturnsAll.Columns.Add("RedeemedAmount", typeof(double));
            dtReturnsAll.Columns.Add("DVP", typeof(double));
            dtReturnsAll.Columns.Add("TotalPL", typeof(double));
            dtReturnsAll.Columns.Add("AbsoluteReturn", typeof(double));
            dtReturnsAll.Columns.Add("DVR", typeof(double));
            dtReturnsAll.Columns.Add("XIRR", typeof(double));
            dtReturnsAll.Columns.Add("TotalDividends", typeof(double));
            dtReturnsAll.Columns.Add("AMCCode");
            dtReturnsAll.Columns.Add("SchemeCode");
            dtReturnsAll.Columns.Add("SubCategoryName");
            dtReturnsAll.Columns.Add("FolioStartDate");
            dtReturnsAll.Columns.Add("InvestmentStartDate");
            dtReturnsAll.Columns.Add("CMFNP_NAVDate");
            dtReturnsAll.Columns.Add("CMFNP_ValuationDate");
        }

        private void ReturnsRealizedDataTableCreation(DataTable dtReturnsRealized)
        {
            dtReturnsRealized.Columns.Add("MFNPId");
            dtReturnsRealized.Columns.Add("AccountId");
            dtReturnsRealized.Columns.Add("Category");
            dtReturnsRealized.Columns.Add("Scheme");
            dtReturnsRealized.Columns.Add("FolioNum");
            dtReturnsRealized.Columns.Add("InvestedCost", typeof(double));
            dtReturnsRealized.Columns.Add("UnitsSold", typeof(double));
            dtReturnsRealized.Columns.Add("RedeemedAmount", typeof(double));
            dtReturnsRealized.Columns.Add("DVP", typeof(double));
            dtReturnsRealized.Columns.Add("TotalDividends", typeof(double));
            dtReturnsRealized.Columns.Add("TotalPL", typeof(double));
            dtReturnsRealized.Columns.Add("AbsoluteReturn", typeof(double));
            dtReturnsRealized.Columns.Add("XIRR", typeof(double));
            dtReturnsRealized.Columns.Add("AMCCode");
            dtReturnsRealized.Columns.Add("SchemeCode");
            dtReturnsRealized.Columns.Add("SubCategoryName");
            dtReturnsRealized.Columns.Add("FolioStartDate");
            dtReturnsRealized.Columns.Add("InvestmentStartDate");
            dtReturnsRealized.Columns.Add("CMFNP_ValuationDate");
        }

        private void TaxHoldingsDataTableCreation(DataTable dtTaxHoldings)
        {
            dtTaxHoldings.Columns.Add("MFNPId");
            dtTaxHoldings.Columns.Add("AccountId");
            dtTaxHoldings.Columns.Add("Category");
            dtTaxHoldings.Columns.Add("Scheme");
            dtTaxHoldings.Columns.Add("FolioNum");
            dtTaxHoldings.Columns.Add("OpenUnits", typeof(double));
            dtTaxHoldings.Columns.Add("BalanceAmount", typeof(double));
            dtTaxHoldings.Columns.Add("NAV", typeof(double));
            dtTaxHoldings.Columns.Add("MarketValue", typeof(double));
            dtTaxHoldings.Columns.Add("UnrealizedPL", typeof(double));
            dtTaxHoldings.Columns.Add("EligibleSTCG", typeof(double));
            dtTaxHoldings.Columns.Add("EligibleLTCG", typeof(double));
            dtTaxHoldings.Columns.Add("AMCCode");
            dtTaxHoldings.Columns.Add("SchemeCode");
            dtTaxHoldings.Columns.Add("SubCategoryName");
            dtTaxHoldings.Columns.Add("FolioStartDate");
            dtTaxHoldings.Columns.Add("InvestmentStartDate");
            dtTaxHoldings.Columns.Add("CMFNP_NAVDate");
            dtTaxHoldings.Columns.Add("CMFNP_ValuationDate");
        }

        private void TaxRealizedDataTableCreation(DataTable dtTaxRealized)
        {
            dtTaxRealized.Columns.Add("MFNPId");
            dtTaxRealized.Columns.Add("AccountId");
            dtTaxRealized.Columns.Add("Category");
            dtTaxRealized.Columns.Add("Scheme");
            dtTaxRealized.Columns.Add("FolioNum");
            dtTaxRealized.Columns.Add("AcquisitionCost", typeof(double));
            dtTaxRealized.Columns.Add("RedeemedAmount", typeof(double));
            dtTaxRealized.Columns.Add("TotalPL", typeof(double));
            dtTaxRealized.Columns.Add("STCG", typeof(double));
            dtTaxRealized.Columns.Add("LTCG", typeof(double));
            dtTaxRealized.Columns.Add("AMCCode");
            dtTaxRealized.Columns.Add("SchemeCode");
            dtTaxRealized.Columns.Add("SubCategoryName");
            dtTaxRealized.Columns.Add("UnitsSold", typeof(double));
            dtTaxRealized.Columns.Add("FolioStartDate");
            dtTaxRealized.Columns.Add("InvestmentStartDate");
            dtTaxRealized.Columns.Add("CMFNP_ValuationDate");
        }

        private void BindPerformaceChart()
        {
            //BindSubCategoryPieChart();
            AssetBo assetBo = new AssetBo();
            try
            {
                AdvisorVo adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
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

                        //chrtMFClassification.Series.Clear();
                        //chrtMFClassification.Series.Add(seriesAssets);

                        //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
                        //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
                        //chrtMFClassification.Legends.Clear();
                        //chrtMFClassification.Legends.Add(legend);
                        //chrtMFClassification.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
                        //chrtMFClassification.Legends["AssetsLegend"].Title = "Assets";
                        //chrtMFClassification.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
                        //chrtMFClassification.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                        //chrtMFClassification.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
                        //chrtMFClassification.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

                        //chrtMFClassification.ChartAreas[0].Area3DStyle.Enable3D = true;
                        //chrtMFClassification.ChartAreas[0].Area3DStyle.Perspective = 50;
                        //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
                        //chrtMFClassification.Width = 500;
                        //chrtMFClassification.BackColor = System.Drawing.Color.Transparent;
                        //chrtMFClassification.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
                        //chrtMFClassification.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

                        //LegendCellColumn colors = new LegendCellColumn();
                        //colors.HeaderText = "Color";
                        //colors.ColumnType = LegendCellColumnType.SeriesSymbol;
                        //colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
                        //chrtMFClassification.Legends["AssetsLegend"].CellColumns.Add(colors);

                        //LegendCellColumn asset = new LegendCellColumn();
                        //asset.Alignment = ContentAlignment.MiddleLeft;
                        //asset.HeaderText = "Asset";
                        //asset.Text = "#VALX";
                        //chrtMFClassification.Legends["AssetsLegend"].CellColumns.Add(asset);

                        LegendCellColumn assetPercent = new LegendCellColumn();
                        assetPercent.Alignment = ContentAlignment.MiddleLeft;
                        assetPercent.HeaderText = "Asset Percentage";
                        assetPercent.Text = "#PERCENT";
                        //chrtMFClassification.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

                        //foreach (DataPoint point in chrtMFClassification.Series["seriesMFC"].Points)
                        //{
                        //    point["Exploded"] = "true";
                        //}

                        //chrtMFClassification.DataBind();
                        //chrtTotalAssets.Series["Assets"]. 
                    }

                }
                else
                {
                    //trChart.Visible = false;
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

        //private void BindSubCategoryPieChart()
        //{
        //    AssetBo assetBo = new AssetBo();
        //    AdvisorVo adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
        //    DataSet dsSubcategoryValue = assetBo.GetSubCategoryPieChartValue(portfolioId, adviserVo.advisorId);
        //    DataTable dtSubCategoryPie;
        //    try
        //    {
        //        dtSubCategoryPie = dsSubcategoryValue.Tables[0];
        //        if (dtSubCategoryPie.Rows.Count > 0)
        //        {
        //            // Total Assets Chart
        //            Series seriesAssets = new Series("seriesMFC");
        //            Legend legend = new Legend("AssetsLegend");
        //            legend.Enabled = true;
        //            string[] XValues = new string[dtSubCategoryPie.Rows.Count];
        //            double[] YValues = new double[dtSubCategoryPie.Rows.Count];
        //            int i = 0;
        //            seriesAssets.ChartType = SeriesChartType.Pie;

        //            foreach (DataRow dr in dtSubCategoryPie.Rows)
        //            {
        //                XValues[i] = dr["SubCategory"].ToString();
        //                YValues[i] = double.Parse(dr["AggrCurrentValue"].ToString());
        //                i++;
        //            }
        //            seriesAssets.Points.DataBindXY(XValues, YValues);


        //            chrtSubCategory.Series.Clear();
        //            chrtSubCategory.Series.Add(seriesAssets);

        //            chrtSubCategory.Legends.Clear();
        //            chrtSubCategory.Legends.Add(legend);
        //            chrtSubCategory.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
        //            chrtSubCategory.Legends["AssetsLegend"].Title = "Assets";
        //            chrtSubCategory.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
        //            chrtSubCategory.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
        //            chrtSubCategory.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
        //            chrtSubCategory.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

        //            chrtSubCategory.ChartAreas[0].Area3DStyle.Enable3D = true;
        //            chrtSubCategory.ChartAreas[0].Area3DStyle.Perspective = 50;
        //            chrtSubCategory.Width = 500;
        //            chrtSubCategory.BackColor = System.Drawing.Color.Transparent;
        //            chrtSubCategory.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
        //            chrtSubCategory.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

        //            LegendCellColumn colors = new LegendCellColumn();
        //            colors.HeaderText = "Color";
        //            colors.ColumnType = LegendCellColumnType.SeriesSymbol;
        //            colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
        //            chrtSubCategory.Legends["AssetsLegend"].CellColumns.Add(colors);

        //            LegendCellColumn asset = new LegendCellColumn();
        //            asset.Alignment = ContentAlignment.MiddleLeft;
        //            asset.HeaderText = "SubCategory";
        //            asset.Text = "#VALX";
        //            chrtSubCategory.Legends["AssetsLegend"].CellColumns.Add(asset);

        //            LegendCellColumn assetPercent = new LegendCellColumn();
        //            assetPercent.Alignment = ContentAlignment.MiddleLeft;
        //            assetPercent.HeaderText = "AssetPercentage";
        //            assetPercent.Text = "#PERCENT";
        //            chrtSubCategory.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

        //            foreach (DataPoint point in chrtSubCategory.Series["seriesMFC"].Points)
        //            {
        //                point["Exploded"] = "true";
        //            }

        //            chrtSubCategory.DataBind();
        //            //chrtTotalAssets.Series["Assets"]. 
        //            chrtSubCategory.Visible = true;
        //            trSubCategoryWise.Visible = true;
        //        }

        //        else
        //        {
        //            trChart.Visible = false;
        //            trSubCategoryWise.Visible = false;
        //            chrtSubCategory.Visible = false;
        //        }

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }

        //}

        #region Returns Tab Events

        protected void lnlGoBackHoldings_Click(object sender, EventArgs e)
        {

        }
        protected void rgHoldings_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select" || e.CommandName == "NavigateToMarketData")
            {
                string portfolio = string.Empty;
                //if(ddlPortfolio.SelectedIndex!=0)
                portfolio = ddlPortfolio.SelectedItem.ToString();
                GridDataItem dataItem = e.Item as GridDataItem;
                string strMFNPId = dataItem.GetDataKeyValue("MFNPId").ToString();
                //string strMFNPId = rgHoldings.MasterTableView.DataKeyValues[e.Item.ItemIndex]["MFNPId"].ToString();
                string strFolio = dataItem["FolioNum"].Text;
                LinkButton lnk = (LinkButton)dataItem.FindControl("lnkScheme");
                string strScheme = lnk.Text;
                int intSchemeCode = Int32.Parse(dataItem.GetDataKeyValue("SchemeCode").ToString());

                if (e.CommandName == "Select")
                {
                    int intAccId = Int32.Parse(dataItem.GetDataKeyValue("AccountId").ToString());

                    // Function to get the minimum date for that account id and scheme code
                    DateTime dtFrom = customerPortfolioBo.GetSchemeTransactionInitialBuyDate(intAccId, intSchemeCode);
                    string strFromDate = dtFrom.ToShortDateString();
                    string strToDate = DateTime.Today.ToShortDateString();

                    #region Reusing Old Code

                    //Session["Folio"] = dataItem["FolioNum"].Text;
                    //Session["Scheme"] = dataItem["Scheme"].Text;
                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    ht["Folio"] = strFolio;
                    ht["SchemePlanCode"] = intSchemeCode;
                    ht["Account"] = intAccId;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleTransactionView','none');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMMultipleTransactionView", "loadcontrol('RMMultipleTransactionView','strPortfolio=" + portfolio + "');", true);

                }
                else if (e.CommandName == "NavigateToMarketData")
                {
                    string strAMCCode = dataItem.GetDataKeyValue("AMCCode").ToString();
                    int month = 0;
                    int year = 0;

                    if (DateTime.Now.Month != 1)
                    {
                        month = DateTime.Now.Month - 1;
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        month = 12;
                        year = DateTime.Now.Year - 1;
                    }

                    Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + intSchemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + strScheme + "&AMCCode=" + strAMCCode, false);
                }
            }

        }

        protected void lnkGoBackAll_Click(object sender, EventArgs e)
        {

        }
        protected void rgAll_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string portfolio = string.Empty;
            //if(ddlPortfolio.SelectedIndex!=0)
            portfolio = ddlPortfolio.SelectedItem.ToString();
            if (e.CommandName == "Select" || e.CommandName == "NavigateToMarketData")
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string strMFNPId = dataItem.GetDataKeyValue("MFNPId").ToString();
                string strFolio = dataItem["FolioNum"].Text;
                LinkButton lnk = (LinkButton)dataItem.FindControl("lnkScheme");
                string strScheme = lnk.Text;
                int intSchemeCode = Int32.Parse(dataItem.GetDataKeyValue("SchemeCode").ToString());

                if (e.CommandName == "Select")
                {
                    int intAccId = Int32.Parse(dataItem.GetDataKeyValue("AccountId").ToString());

                    // Function to get the minimum date for that account id and scheme code
                    DateTime dtFrom = customerPortfolioBo.GetSchemeTransactionInitialBuyDate(intAccId, intSchemeCode);
                    string strFromDate = dtFrom.ToShortDateString();
                    string strToDate = DateTime.Today.ToShortDateString();

                    #region Reusing Old Code

                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    ht["Folio"] = strFolio;
                    ht["SchemePlanCode"] = intSchemeCode;
                    ht["Account"] = intAccId;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleTransactionView','none');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMMultipleTransactionView", "loadcontrol('RMMultipleTransactionView','none');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMMultipleTransactionView", "loadcontrol('RMMultipleTransactionView','strPortfolio=" + portfolio + "');", true);

                }
                else if (e.CommandName == "NavigateToMarketData")
                {
                    string strAMCCode = dataItem.GetDataKeyValue("AMCCode").ToString();
                    int month = 0;
                    int year = 0;

                    if (DateTime.Now.Month != 1)
                    {
                        month = DateTime.Now.Month - 1;
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        month = 12;
                        year = DateTime.Now.Year - 1;
                    }

                    Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + intSchemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + strScheme + "&AMCCode=" + strAMCCode, false);
                }
            }
        }

        protected void lnkGoBackRealized_Click(object sender, EventArgs e)
        {

        }
        protected void rgRealized_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string portfolio = string.Empty;
            //if(ddlPortfolio.SelectedIndex!=0)
            portfolio = ddlPortfolio.SelectedItem.ToString();
            if (e.CommandName == "Select" || e.CommandName == "NavigateToMarketData")
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string strMFNPId = dataItem.GetDataKeyValue("MFNPId").ToString();
                string strFolio = dataItem["FolioNum"].Text;
                LinkButton lnk = (LinkButton)dataItem.FindControl("lnkScheme");
                string strScheme = lnk.Text;
                int intSchemeCode = Int32.Parse(dataItem.GetDataKeyValue("SchemeCode").ToString());

                if (e.CommandName == "Select")
                {
                    int intAccId = Int32.Parse(dataItem.GetDataKeyValue("AccountId").ToString());

                    // Function to get the minimum date for that account id and scheme code
                    DateTime dtFrom = customerPortfolioBo.GetSchemeTransactionInitialBuyDate(intAccId, intSchemeCode);
                    string strFromDate = dtFrom.ToShortDateString();
                    string strToDate = DateTime.Today.ToShortDateString();

                    #region Reusing Old Code

                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    ht["Folio"] = strFolio;
                    ht["SchemePlanCode"] = intSchemeCode;
                    ht["Account"] = intAccId;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleTransactionView','none');", true);

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMMultipleTransactionView", "loadcontrol('RMMultipleTransactionView','strPortfolio=" + portfolio + "');", true);

                }
                else if (e.CommandName == "NavigateToMarketData")
                {
                    string strAMCCode = dataItem.GetDataKeyValue("AMCCode").ToString();
                    int month = 0;
                    int year = 0;

                    if (DateTime.Now.Month != 1)
                    {
                        month = DateTime.Now.Month - 1;
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        month = 12;
                        year = DateTime.Now.Year - 1;
                    }

                    Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + intSchemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + strScheme + "&AMCCode=" + strAMCCode, false);
                }
            }
        }

        #endregion

        #region Tax Tab Events

        protected void lnlGoBackTaxHoldings_Click(object sender, EventArgs e)
        {

        }
        protected void rgTaxHoldings_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select" || e.CommandName == "NavigateToMarketData")
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string strMFNPId = dataItem.GetDataKeyValue("MFNPId").ToString();
                string strFolio = dataItem["FolioNum"].Text;
                LinkButton lnk = (LinkButton)dataItem.FindControl("lnkScheme");
                string strScheme = lnk.Text;
                int intSchemeCode = Int32.Parse(dataItem.GetDataKeyValue("SchemeCode").ToString());

                if (e.CommandName == "Select")
                {
                    int intAccId = Int32.Parse(dataItem.GetDataKeyValue("AccountId").ToString());

                    // Function to get the minimum date for that account id and scheme code
                    DateTime dtFrom = customerPortfolioBo.GetSchemeTransactionInitialBuyDate(intAccId, intSchemeCode);
                    string strFromDate = dtFrom.ToShortDateString();
                    string strToDate = DateTime.Today.ToShortDateString();

                    #region Reusing Old Code

                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    ht["Folio"] = strFolio;
                    ht["SchemePlanCode"] = intSchemeCode;
                    ht["Account"] = intAccId;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleTransactionView','none');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMMultipleTransactionView", "loadcontrol('RMMultipleTransactionView','none');", true);
                }
                else if (e.CommandName == "NavigateToMarketData")
                {
                    string strAMCCode = dataItem.GetDataKeyValue("AMCCode").ToString();
                    int month = 0;
                    int year = 0;

                    if (DateTime.Now.Month != 1)
                    {
                        month = DateTime.Now.Month - 1;
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        month = 12;
                        year = DateTime.Now.Year - 1;
                    }

                    Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + intSchemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + strScheme + "&AMCCode=" + strAMCCode, false);
                }
            }
        }

        protected void lnlGoBackTaxRealized_Click(object sender, EventArgs e)
        {

        }
        protected void rgTaxRealized_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select" || e.CommandName == "NavigateToMarketData")
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string strMFNPId = dataItem.GetDataKeyValue("MFNPId").ToString();
                string strFolio = dataItem["FolioNum"].Text;
                LinkButton lnk = (LinkButton)dataItem.FindControl("lnkScheme");
                string strScheme = lnk.Text;
                int intSchemeCode = Int32.Parse(dataItem.GetDataKeyValue("SchemeCode").ToString());

                if (e.CommandName == "Select")
                {
                    int intAccId = Int32.Parse(dataItem.GetDataKeyValue("AccountId").ToString());

                    // Function to get the minimum date for that account id and scheme code
                    DateTime dtFrom = customerPortfolioBo.GetSchemeTransactionInitialBuyDate(intAccId, intSchemeCode);
                    string strFromDate = dtFrom.ToShortDateString();
                    string strToDate = DateTime.Today.ToShortDateString();

                    #region Reusing Old Code

                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    ht["Folio"] = strFolio;
                    ht["SchemePlanCode"] = intSchemeCode;
                    ht["Account"] = intAccId;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMMultipleTransactionView','none');", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "RMMultipleTransactionView", "loadcontrol('RMMultipleTransactionView','none');", true);
                }
                else if (e.CommandName == "NavigateToMarketData")
                {
                    string strAMCCode = dataItem.GetDataKeyValue("AMCCode").ToString();
                    int month = 0;
                    int year = 0;

                    if (DateTime.Now.Month != 1)
                    {
                        month = DateTime.Now.Month - 1;
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        month = 12;
                        year = DateTime.Now.Year - 1;
                    }

                    Response.Redirect("ControlHost.aspx?pageid=AdminPriceList&SchemeCode=" + intSchemeCode + "&Year=" + year + "&Month=" + month + "&SchemeName=" + strScheme + "&AMCCode=" + strAMCCode, false);
                }
            }
        }

        #endregion

        protected void ddlDisplayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDisplayType.SelectedIndex == 0)
            {
            }
            else if (ddlDisplayType.SelectedIndex == 1)
            {
               
            }
            else if (ddlDisplayType.SelectedIndex == 2)
            {
                BindTaxGrid();
            }
        }

        protected void rgHoldings_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["HoldingReturns"] != null)
            {
                dt = (DataTable)ViewState["HoldingReturns"];
                rgHoldings.DataSource = dt;

            }
            BindPerformaceChart();
        }

        protected void rgAll_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["AllReturns"] != null)
            {
                dt = (DataTable)ViewState["AllReturns"];
                rgAll.DataSource = dt;
            }
            BindPerformaceChart();
        }

        protected void rgRealized_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["RealizedReturns"] != null)
            {
                dt = (DataTable)ViewState["RealizedReturns"];
                rgRealized.DataSource = dt;
            }
            BindPerformaceChart();
        }

        protected void rgTaxHoldings_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
           
        }

        protected void rgTaxRealized_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
           
        }


        protected void rgHoldings_Init(object sender, System.EventArgs e)
        {
            GridFilterMenu menu = rgHoldings.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Text == "NoFilter" || menu.Items[i].Text == "Contains" || menu.Items[i].Text == "EqualTo" || menu.Items[i].Text == "StartsWith" || menu.Items[i].Text == "EndsWith")
                {
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }

        protected void rgAll_Init(object sender, System.EventArgs e)
        {
            GridFilterMenu menu = rgAll.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Text == "NoFilter" || menu.Items[i].Text == "Contains" || menu.Items[i].Text == "EqualTo" || menu.Items[i].Text == "StartsWith" || menu.Items[i].Text == "EndsWith")
                {
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }

        protected void rgRealized_Init(object sender, System.EventArgs e)
        {
            GridFilterMenu menu = rgRealized.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                if (menu.Items[i].Text == "NoFilter" || menu.Items[i].Text == "Contains" || menu.Items[i].Text == "EqualTo" || menu.Items[i].Text == "StartsWith" || menu.Items[i].Text == "EndsWith")
                {
                    i++;
                }
                else
                {
                    menu.Items.RemoveAt(i);
                }
            }
        }

        protected void rgTaxHoldings_Init(object sender, System.EventArgs e)
        {
           
        }

        protected void rgTaxRealized_Init(object sender, System.EventArgs e)
        {
        }

     

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridHeaderItem headerItem = rgHoldings.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            DropDownList ddl = headerItem.FindControl("ddlCategory") as DropDownList;
            if (ddl != null)
            {
                hdnReturnsHoldingsCategory.Value = ddl.SelectedItem.ToString();
            }
            BindReturnsGrid();
        }

        protected void ddlRealizedCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridHeaderItem headerItem = rgRealized.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            DropDownList ddl = headerItem.FindControl("ddlRealizedCategory") as DropDownList;
            if (ddl != null)
            {
                hdnReturnsRealizedCategory.Value = ddl.SelectedItem.ToString();
            }
            BindReturnsGrid();
        }

        protected void ddlAllCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridHeaderItem headerItem = rgAll.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            DropDownList ddl = headerItem.FindControl("ddlAllCategory") as DropDownList;
            if (ddl != null)
            {
                hdnReturnsAllCategory.Value = ddl.SelectedItem.ToString();
            }
            BindReturnsGrid();
        }

        protected void ddlTaxHoldingsCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void ddlTaxRealizedCategory_SelectedIndexChanged(object sender, ImageClickEventArgs e)
        {
           
        }

        public void btnExportrgTaxHoldingsFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
           
        }

        public void btnExportrgAllFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            rgAll.ExportSettings.OpenInNewWindow = true;
            rgAll.ExportSettings.IgnorePaging = true;
            rgAll.ExportSettings.HideStructureColumns = true;
            rgAll.ExportSettings.ExportOnlyData = true;
            rgAll.ExportSettings.FileName = "Return ALL Detail";
            rgAll.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgAll.MasterTableView.ExportToExcel();
        }

        public void btnExportrgRealizedFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            rgRealized.ExportSettings.OpenInNewWindow = true;
            rgRealized.ExportSettings.IgnorePaging = true;
            rgRealized.ExportSettings.HideStructureColumns = true;
            rgRealized.ExportSettings.ExportOnlyData = true;
            rgRealized.ExportSettings.FileName = "Return Realized Detail";
            rgRealized.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            rgRealized.MasterTableView.ExportToExcel();
        }


        public void btnExportrgHoldingsFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            rgHoldings.ExportSettings.OpenInNewWindow = true;
            rgHoldings.ExportSettings.IgnorePaging = true;
            rgHoldings.ExportSettings.HideStructureColumns = true;
            rgHoldings.ExportSettings.ExportOnlyData = true;
            rgHoldings.ExportSettings.FileName = "Returns Holdings Detail";
            rgHoldings.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;

            rgHoldings.MasterTableView.ExportToExcel();
        }

        public void btnExportrgTaxRealizedFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
           
        }

        protected void ddlMFClassificationCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime valDate = DateTime.Parse(lblPickDate.Text);
            dsSchemeHoldingSector = customerPortfolioBo.GetCustomerSchemeHoldingSectors(portfolioId, valDate);
            Session["DsSchemeHoldingSector"] = dsSchemeHoldingSector;
           
           
          
          
        }

     

      


        private void BindHoldingChart()
        {
            DataTable dtHoldingsPie;
            try
            {
                if (Session["DsSchemeHoldingSector"] != "")
                {
                    dsSchemeHoldingSector = (DataSet)Session["DsSchemeHoldingSector"];
                    dtHoldingsPie = dsSchemeHoldingSector.Tables[1];
                    if (dsSchemeHoldingSector.Tables[1].Rows.Count > 0)
                    {
                        // Total Assets Chart
                        Series seriesAssets = new Series("seriesMFC");
                        Legend legend = new Legend("AssetsLegend");
                        legend.Enabled = true;
                        string[] XValues = new string[dtHoldingsPie.Rows.Count];
                        double[] YValues = new double[dtHoldingsPie.Rows.Count];
                        int i = 0;
                        seriesAssets.ChartType = SeriesChartType.Pie;

                        foreach (DataRow dr in dtHoldingsPie.Rows)
                        {
                            XValues[i] = dr["Instrument"].ToString();
                            YValues[i] = double.Parse(dr["Amount"].ToString());
                            i++;
                        }
                        seriesAssets.Points.DataBindXY(XValues, YValues);


                    


                        LegendCellColumn colors = new LegendCellColumn();
                        colors.HeaderText = "Color";
                        colors.ColumnType = LegendCellColumnType.SeriesSymbol;
                        colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;

                        LegendCellColumn asset = new LegendCellColumn();
                        asset.Alignment = ContentAlignment.MiddleLeft;
                        asset.HeaderText = "Instrument";
                        asset.Text = "#VALX";

                        LegendCellColumn assetPercent = new LegendCellColumn();
                        assetPercent.Alignment = ContentAlignment.MiddleLeft;
                        assetPercent.HeaderText = "Amount";
                        assetPercent.Text = "#PERCENT";

                    }

                }
                else
                {
                   
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        private void BindHoldingGrid()
        {
            DataTable dtHoldings;
            if (Session["DsSchemeHoldingSector"] != "")
            {
                dsSchemeHoldingSector = (DataSet)Session["DsSchemeHoldingSector"];
                dtHoldings = dsSchemeHoldingSector.Tables[1];
                if (dsSchemeHoldingSector.Tables[2].Rows.Count > 0)
                {
                    //gvTopTenHoldings.DataSource = dtHoldings;
                    //gvTopTenHoldings.DataBind();
                    ErrorMessage.Visible = false;
                    trNote.Visible = false;
                }
                else
                {
                    //gvTopTenHoldings.Visible = false;
                    ErrorMessage.Visible = true;
                    trNote.Visible = false;


                }
            }
            else
            {
                ErrorMessage.Visible = true;
            }
        }

        private void BindSchemePerformanceGrid()
        {
            DataTable dtSchemePerformance;
            if (Session["DsSchemeHoldingSector"] != "")
            {
                dsSchemeHoldingSector = (DataSet)Session["DsSchemeHoldingSector"];
                dtSchemePerformance = dsSchemeHoldingSector.Tables[0];
                if (dsSchemeHoldingSector.Tables[0].Rows.Count > 0)
                {
                    //gvSchemePerformance.DataSource = dtSchemePerformance;
                    //gvSchemePerformance.DataBind();
                    ErrorMessage.Visible = false;
                    //trSchemePerformance.Visible = true;
                    //gvSchemePerformance.Visible = true;
                    if (Cache["SchemePerformance" + userVo.UserId] == null)
                    {
                        Cache.Insert("SchemePerformance" + userVo.UserId, dtSchemePerformance);
                    }
                    else
                    {
                        Cache.Remove("SchemePerformance" + userVo.UserId);
                        Cache.Insert("SchemePerformance" + userVo.UserId, dtSchemePerformance);
                    }
                }
                else
                {
                    //gvSchemePerformance.Visible = false;
                    trNote.Visible = false;
                    ErrorMessage.Visible = true;

                }
            }
            else
            {
                trNote.Visible = false;
                ErrorMessage.Visible = true;
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            portfolioId = Convert.ToInt32(ddlPortfolio.SelectedValue);
           
           
            CalculatePortfolioXIRR(portfolioId);
            GetMFPortfolioList();
            SetTaxGridsNull();
            SetReturnsGridsNull();

            if (ddlDisplayType.SelectedIndex == 0)
            {

                if (ddlType.SelectedValue == "0")
                {
                    NewBindReturnsGrid();
                }
                else if (ddlType.SelectedValue == "1")
                {
                    AllBindReturnsGrid();

                }
                else if (ddlType.SelectedValue == "2")
                {
                    RealizedBindReturnsGrid();
                }

            }
            else if (ddlDisplayType.SelectedIndex == 1)
            {
                BindTaxGrid();
            }
        }
        protected void gvSchemePerformance_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtSchemePerformance = new DataTable();
            dtSchemePerformance = (DataTable)Cache["SchemePerformance" + userVo.UserId];
          
        }
        protected void rgHoldings_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
        }

        protected void rgAll_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        { }
        protected void rgRealized_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           
        }

        protected void rgTaxHoldings_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (double.Parse(item["OpenUnits"].Text) < 0)
                {
                    item["BalanceAmount"].BackColor = Color.Red;
                    item["BalanceAmount"].Font.Bold = true;
                    item["MarketValue"].BackColor = Color.Red;
                    item["MarketValue"].Font.Bold = true;
                    item["UnrealizedPL"].BackColor = Color.Red;
                    item["UnrealizedPL"].Font.Bold = true;
                    item["EligibleSTCG"].BackColor = Color.Red;
                    item["EligibleSTCG"].Font.Bold = true;
                    item["EligibleLTCG"].BackColor = Color.Red;
                    item["EligibleLTCG"].Font.Bold = true;
                    item["NAV"].BackColor = Color.Red;
                    item["NAV"].Font.Bold = true;
                    item["Category"].BackColor = Color.Red;
                    item["Category"].Font.Bold = true;
                    item["OpenUnits"].BackColor = Color.Red;
                    item["OpenUnits"].Font.Bold = true;
                    item["Scheme"].BackColor = Color.Red;
                    item["Scheme"].Font.Bold = true;
                    item["FolioNum"].BackColor = Color.Red;
                    item["FolioNum"].Font.Bold = true;
                    item["SubCategoryName"].BackColor = Color.Red;
                    item["SubCategoryName"].Font.Bold = true;
                    item["FolioStartDate"].BackColor = Color.Red;
                    item["FolioStartDate"].Font.Bold = true;
                    item["InvestmentStartDate"].BackColor = Color.Red;
                    item["InvestmentStartDate"].Font.Bold = true;
                    item["CMFNP_NAVDate"].BackColor = Color.Red;
                    item["CMFNP_NAVDate"].Font.Bold = true;

                }
            }
        }

        protected void rgTaxRealized_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
          
        }
        protected void rgHoldings_OnExcelMLExportStylesCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
        {
            BorderStylesCollection borders = new BorderStylesCollection();
            BorderStyles borderStyle = null;
            for (int i = 1; i <= 3; i++)
            {
                borderStyle = new BorderStyles();
                borderStyle.PositionType = (PositionType)i;
                borderStyle.Color = System.Drawing.Color.Black;
                borderStyle.LineStyle = LineStyle.Continuous;
                borderStyle.Weight = 1.0;
                borders.Add(borderStyle);
            }
            foreach (Telerik.Web.UI.GridExcelBuilder.StyleElement style in e.Styles)
            {
                foreach (BorderStyles border in borders)
                {
                    style.Borders.Add(border);
                }
                if (style.Id == "headerStyle")
                {
                    style.FontStyle.Bold = true;
                    style.FontStyle.Color = System.Drawing.Color.White;
                    style.InteriorStyle.Color = System.Drawing.Color.Black;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "itemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;                    
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.NumberFormat.FormatType = NumberFormatType.GeneralNumber;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "dateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingDateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
            }
            Telerik.Web.UI.GridExcelBuilder.StyleElement myStyle = new Telerik.Web.UI.GridExcelBuilder.StyleElement("MyCustomStyle");
            myStyle.FontStyle.Bold = true;
            myStyle.FontStyle.Italic = true;
            myStyle.InteriorStyle.Color = System.Drawing.Color.Gray;
            myStyle.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            e.Styles.Add(myStyle);
        }
        protected void rgHoldings_OnExcelMLExportRowCreated(object source, GridExportExcelMLRowCreatedArgs e)
        {

            if (e.RowType == GridExportExcelMLRowType.HeaderRow)
            {
                int rowIndex = 0;
                RowElement row = new RowElement();
                CellElement cell = new CellElement();
                cell.MergeAcross = e.Row.Cells.Count - 1;
                cell.Data.DataItem = "Adviser" + ": " + advisorVo.OrganizationName;
                row.Cells.Add(cell);
                e.Worksheet.Table.Rows.Insert(rowIndex, row);
                rowIndex++;
                if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    RowElement row1 = new RowElement();
                    CellElement cell1 = new CellElement();
                    cell1.MergeAcross = e.Row.Cells.Count - 1;
                    cell1.Data.DataItem = "Customer" + ": " + customerVo.FirstName + "  " + customerVo.MiddleName + "  " + customerVo.LastName;
                    row1.Cells.Add(cell1);
                    e.Worksheet.Table.Rows.Insert(rowIndex, row1);
                    rowIndex++;
                }
                RowElement DateRow = new RowElement();
                CellElement Datecell = new CellElement();
                Datecell.MergeAcross = e.Row.Cells.Count - 1;
                Datecell.Data.DataItem = "As on Date" + ": " + genDict[Constants.MFDate.ToString()].ToString("dd/MM/yyyy");
                DateRow.Cells.Add(Datecell);
                e.Worksheet.Table.Rows.Insert(rowIndex, DateRow);
                rowIndex++;
                RowElement BlankRow = new RowElement();
                CellElement Blankcell = new CellElement();
                Blankcell.MergeAcross = e.Row.Cells.Count - 1;
                Blankcell.Data.DataItem = "";
                BlankRow.Cells.Add(Blankcell);
                e.Worksheet.Table.Rows.Insert(rowIndex, BlankRow);

            }
        }
        protected void rgAll_OnExcelMLExportStylesCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
        {
            BorderStylesCollection borders = new BorderStylesCollection();
            BorderStyles borderStyle = null;
            for (int i = 1; i <= 3; i++)
            {
                borderStyle = new BorderStyles();
                borderStyle.PositionType = (PositionType)i;
                borderStyle.Color = System.Drawing.Color.Black;
                borderStyle.LineStyle = LineStyle.Continuous;
                borderStyle.Weight = 1.0;
                borders.Add(borderStyle);
            }
            foreach (Telerik.Web.UI.GridExcelBuilder.StyleElement style in e.Styles)
            {
                foreach (BorderStyles border in borders)
                {
                    style.Borders.Add(border);
                }
                if (style.Id == "headerStyle")
                {
                    style.FontStyle.Bold = true;
                    style.FontStyle.Color = System.Drawing.Color.White;
                    style.InteriorStyle.Color = System.Drawing.Color.Black;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "itemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.NumberFormat.FormatType = NumberFormatType.GeneralNumber;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "dateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingDateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
            }
            Telerik.Web.UI.GridExcelBuilder.StyleElement myStyle = new Telerik.Web.UI.GridExcelBuilder.StyleElement("MyCustomStyle");
            myStyle.FontStyle.Bold = true;
            myStyle.FontStyle.Italic = true;
            myStyle.InteriorStyle.Color = System.Drawing.Color.Gray;
            myStyle.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            e.Styles.Add(myStyle);
        }
        protected void rgAll_OnExcelMLExportRowCreated(object source, GridExportExcelMLRowCreatedArgs e)
        {

            if (e.RowType == GridExportExcelMLRowType.HeaderRow)
            {
                int rowIndex = 0;
                RowElement row = new RowElement();
                CellElement cell = new CellElement();
                cell.MergeAcross = e.Row.Cells.Count - 1;
                cell.Data.DataItem = "Adviser" + ": " + advisorVo.OrganizationName;
                row.Cells.Add(cell);
                e.Worksheet.Table.Rows.Insert(rowIndex, row);
                rowIndex++;
                if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    RowElement row1 = new RowElement();
                    CellElement cell1 = new CellElement();
                    cell1.MergeAcross = e.Row.Cells.Count - 1;
                    cell1.Data.DataItem = "Customer" + ": " + customerVo.FirstName + "  " + customerVo.MiddleName + "  " + customerVo.LastName;
                    row1.Cells.Add(cell1);
                    e.Worksheet.Table.Rows.Insert(rowIndex, row1);
                    rowIndex++;
                }
                RowElement DateRow = new RowElement();
                CellElement Datecell = new CellElement();
                Datecell.MergeAcross = e.Row.Cells.Count - 1;
                Datecell.Data.DataItem = "As on Date" + ": " + genDict[Constants.MFDate.ToString()].ToString("dd/MM/yyyy");
                DateRow.Cells.Add(Datecell);
                e.Worksheet.Table.Rows.Insert(rowIndex, DateRow);
                rowIndex++;
                RowElement BlankRow = new RowElement();
                CellElement Blankcell = new CellElement();
                Blankcell.MergeAcross = e.Row.Cells.Count - 1;
                Blankcell.Data.DataItem = "";
                BlankRow.Cells.Add(Blankcell);
                e.Worksheet.Table.Rows.Insert(rowIndex, BlankRow);
            }
        }
        protected void rgTaxRealized_OnExcelMLExportStylesCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
        {
            BorderStylesCollection borders = new BorderStylesCollection();
            BorderStyles borderStyle = null;
            for (int i = 1; i <= 3; i++)
            {
                borderStyle = new BorderStyles();
                borderStyle.PositionType = (PositionType)i;
                borderStyle.Color = System.Drawing.Color.Black;
                borderStyle.LineStyle = LineStyle.Continuous;
                borderStyle.Weight = 1.0;
                borders.Add(borderStyle);
            }
            foreach (Telerik.Web.UI.GridExcelBuilder.StyleElement style in e.Styles)
            {
                foreach (BorderStyles border in borders)
                {
                    style.Borders.Add(border);
                }
                if (style.Id == "headerStyle")
                {
                    style.FontStyle.Bold = true;
                    style.FontStyle.Color = System.Drawing.Color.White;
                    style.InteriorStyle.Color = System.Drawing.Color.Black;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "itemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.NumberFormat.FormatType = NumberFormatType.GeneralNumber;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "dateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingDateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
            }
            Telerik.Web.UI.GridExcelBuilder.StyleElement myStyle = new Telerik.Web.UI.GridExcelBuilder.StyleElement("MyCustomStyle");
            myStyle.FontStyle.Bold = true;
            myStyle.FontStyle.Italic = true;
            myStyle.InteriorStyle.Color = System.Drawing.Color.Gray;
            myStyle.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            e.Styles.Add(myStyle);
        }
        protected void rgTaxRealized_OnExcelMLExportRowCreated(object source, GridExportExcelMLRowCreatedArgs e)
        {

            if (e.RowType == GridExportExcelMLRowType.HeaderRow)
            {
                int rowIndex = 0;
                RowElement row = new RowElement();
                CellElement cell = new CellElement();
                cell.MergeAcross = e.Row.Cells.Count - 1;
                cell.Data.DataItem = "Adviser" + ": " + advisorVo.OrganizationName;
                row.Cells.Add(cell);
                e.Worksheet.Table.Rows.Insert(rowIndex, row);
                rowIndex++;
                if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    RowElement row1 = new RowElement();
                    CellElement cell1 = new CellElement();
                    cell1.MergeAcross = e.Row.Cells.Count - 1;
                    cell1.Data.DataItem = "Customer" + ": " + customerVo.FirstName + "  " + customerVo.MiddleName + "  " + customerVo.LastName;
                    row1.Cells.Add(cell1);
                    e.Worksheet.Table.Rows.Insert(rowIndex, row1);
                    rowIndex++;
                }
                RowElement DateRow = new RowElement();
                CellElement Datecell = new CellElement();
                Datecell.MergeAcross = e.Row.Cells.Count - 1;
                Datecell.Data.DataItem = "As on Date" + ": " + genDict[Constants.MFDate.ToString()].ToString("dd/MM/yyyy");
                DateRow.Cells.Add(Datecell);
                e.Worksheet.Table.Rows.Insert(rowIndex, DateRow);
                rowIndex++;
                RowElement BlankRow = new RowElement();
                CellElement Blankcell = new CellElement();
                Blankcell.MergeAcross = e.Row.Cells.Count - 1;
                Blankcell.Data.DataItem = "";
                BlankRow.Cells.Add(Blankcell);
                e.Worksheet.Table.Rows.Insert(rowIndex, BlankRow);
            }
        }
        protected void rgTaxHoldings_OnExcelMLExportStylesCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
        {
            BorderStylesCollection borders = new BorderStylesCollection();
            BorderStyles borderStyle = null;
            for (int i = 1; i <= 3; i++)
            {
                borderStyle = new BorderStyles();
                borderStyle.PositionType = (PositionType)i;
                borderStyle.Color = System.Drawing.Color.Black;
                borderStyle.LineStyle = LineStyle.Continuous;
                borderStyle.Weight = 1.0;
                borders.Add(borderStyle);
            }
            foreach (Telerik.Web.UI.GridExcelBuilder.StyleElement style in e.Styles)
            {
                foreach (BorderStyles border in borders)
                {
                    style.Borders.Add(border);
                }
                if (style.Id == "headerStyle")
                {
                    style.FontStyle.Bold = true;
                    style.FontStyle.Color = System.Drawing.Color.White;
                    style.InteriorStyle.Color = System.Drawing.Color.Black;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "itemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.NumberFormat.FormatType = NumberFormatType.GeneralNumber;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "dateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingDateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
            }
            Telerik.Web.UI.GridExcelBuilder.StyleElement myStyle = new Telerik.Web.UI.GridExcelBuilder.StyleElement("MyCustomStyle");
            myStyle.FontStyle.Bold = true;
            myStyle.FontStyle.Italic = true;
            myStyle.InteriorStyle.Color = System.Drawing.Color.Gray;
            myStyle.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            e.Styles.Add(myStyle);
        }
        protected void rgTaxHoldings_OnExcelMLExportRowCreated(object source, GridExportExcelMLRowCreatedArgs e)
        {

            if (e.RowType == GridExportExcelMLRowType.HeaderRow)
            {
                int rowIndex = 0;
                RowElement row = new RowElement();
                CellElement cell = new CellElement();
                cell.MergeAcross = e.Row.Cells.Count - 1;
                cell.Data.DataItem = "Adviser" + ": " + advisorVo.OrganizationName;
                row.Cells.Add(cell);
                e.Worksheet.Table.Rows.Insert(rowIndex, row);
                rowIndex++;
                if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    RowElement row1 = new RowElement();
                    CellElement cell1 = new CellElement();
                    cell1.MergeAcross = e.Row.Cells.Count - 1;
                    cell1.Data.DataItem = "Customer" + ": " + customerVo.FirstName + "  " + customerVo.MiddleName + "  " + customerVo.LastName;
                    row1.Cells.Add(cell1);
                    e.Worksheet.Table.Rows.Insert(rowIndex, row1);
                    rowIndex++;
                }
                RowElement DateRow = new RowElement();
                CellElement Datecell = new CellElement();
                Datecell.MergeAcross = e.Row.Cells.Count - 1;
                Datecell.Data.DataItem = "As on Date" + ": " + genDict[Constants.MFDate.ToString()].ToString("dd/MM/yyyy");
                DateRow.Cells.Add(Datecell);
                e.Worksheet.Table.Rows.Insert(rowIndex, DateRow);
                rowIndex++;
                RowElement BlankRow = new RowElement();
                CellElement Blankcell = new CellElement();
                Blankcell.MergeAcross = e.Row.Cells.Count - 1;
                Blankcell.Data.DataItem = "";
                BlankRow.Cells.Add(Blankcell);
                e.Worksheet.Table.Rows.Insert(rowIndex, BlankRow);
            }
        }
        protected void rgRealized_OnExcelMLExportStylesCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
        {
            BorderStylesCollection borders = new BorderStylesCollection();
            BorderStyles borderStyle = null;
            for (int i = 1; i <= 3; i++)
            {
                borderStyle = new BorderStyles();
                borderStyle.PositionType = (PositionType)i;
                borderStyle.Color = System.Drawing.Color.Black;
                borderStyle.LineStyle = LineStyle.Continuous;
                borderStyle.Weight = 1.0;
                borders.Add(borderStyle);
            }
            foreach (Telerik.Web.UI.GridExcelBuilder.StyleElement style in e.Styles)
            {
                foreach (BorderStyles border in borders)
                {
                    style.Borders.Add(border);
                }
                if (style.Id == "headerStyle")
                {
                    style.FontStyle.Bold = true;
                    style.FontStyle.Color = System.Drawing.Color.White;
                    style.InteriorStyle.Color = System.Drawing.Color.Black;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "itemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.NumberFormat.FormatType = NumberFormatType.GeneralNumber;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "dateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
                else if (style.Id == "alternatingDateItemStyle")
                {
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                    style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
                }
            }
            Telerik.Web.UI.GridExcelBuilder.StyleElement myStyle = new Telerik.Web.UI.GridExcelBuilder.StyleElement("MyCustomStyle");
            myStyle.FontStyle.Bold = true;
            myStyle.FontStyle.Italic = true;
            myStyle.InteriorStyle.Color = System.Drawing.Color.Gray;
            myStyle.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            e.Styles.Add(myStyle);
        }
        protected void rgRealized_OnExcelMLExportRowCreated(object source, GridExportExcelMLRowCreatedArgs e)
        {

            if (e.RowType == GridExportExcelMLRowType.HeaderRow)
            {
                int rowIndex = 0;
                RowElement row = new RowElement();
                CellElement cell = new CellElement();
                cell.MergeAcross = e.Row.Cells.Count - 1;
                cell.Data.DataItem = "Adviser" + ": " + advisorVo.OrganizationName;
                row.Cells.Add(cell);
                e.Worksheet.Table.Rows.Insert(rowIndex, row);
                rowIndex++;
                if (Session["IsCustomerDrillDown"] == "Yes")
                {
                    RowElement row1 = new RowElement();
                    CellElement cell1 = new CellElement();
                    cell1.MergeAcross = e.Row.Cells.Count - 1;
                    cell1.Data.DataItem = "Customer" + ": " + customerVo.FirstName + "  " + customerVo.MiddleName + "  " + customerVo.LastName;
                    row1.Cells.Add(cell1);
                    e.Worksheet.Table.Rows.Insert(rowIndex, row1);
                    rowIndex++;
                }
                RowElement DateRow = new RowElement();
                CellElement Datecell = new CellElement();
                Datecell.MergeAcross = e.Row.Cells.Count - 1;
                Datecell.Data.DataItem = "As on Date" + ": " + genDict[Constants.MFDate.ToString()].ToString("dd/MM/yyyy");
                DateRow.Cells.Add(Datecell);
                e.Worksheet.Table.Rows.Insert(rowIndex, DateRow);
                rowIndex++;
                RowElement BlankRow = new RowElement();
                CellElement Blankcell = new CellElement();
                Blankcell.MergeAcross = e.Row.Cells.Count - 1;
                Blankcell.Data.DataItem = "";
                BlankRow.Cells.Add(Blankcell);
                e.Worksheet.Table.Rows.Insert(rowIndex, BlankRow);
            }
        }
    }
}
