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


namespace WealthERP.CustomerPortfolio
{
    public partial class CustomerMutualFundPortfolioNPView : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
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
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                if (Session[SessionContents.ValuationDate] == null)
                    GetLatestValuationDate();
                genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
                string strValuationDate = genDict[Constants.MFDate.ToString()].ToString();
                lblPickDate.Text = DateTime.Parse(genDict[Constants.MFDate.ToString()].ToString()).ToShortDateString();

                if (!IsPostBack)
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();
                    CalculatePortfolioXIRR(portfolioId);
                    GetMFPortfolioList(lblPickDate.Text);
                    SetPanelVisibility(false, false);
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
        }

        private void GetMFPortfolioList(string strValuationDate)
        {
            intPortfolioListCount = 0;

            try
            {
                genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];

                mfPortfolioNetPositionList = customerPortfolioBo.GetCustomerMFNetPositions(customerVo.CustomerId, portfolioId, strValuationDate);
                Session["mfPortfolioList"] = mfPortfolioNetPositionList;
                if (mfPortfolioNetPositionList != null)
                {
                    intPortfolioListCount = mfPortfolioNetPositionList.Count;
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
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
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

        private void SetPanelVisibility(bool blIsReturnsVisibile, bool blIsTaxVisible)
        {
            pnlReturns.Visible = blIsReturnsVisibile;
            pnlTax.Visible = blIsTaxVisible;
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
                        lblPortfolioXIRRValue.Text = double.Parse(dr["XIRR"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                }
            }
        }

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            Session[SessionContents.PortfolioId] = portfolioId;
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
            SetPanelVisibility(true, false);
            BindReturnsGrid();
            BindPerformaceChart();
        }

        public void lnkBtnTax_Click(object sender, EventArgs e)
        {
            SetPanelVisibility(false, true);
            BindTaxGrid();
        }

        private void BindReturnsGrid()
        {
            SetTaxGridsNull();
            if (intPortfolioListCount == 0)
            {
                ReturnsLabelVisibility(true);
                SetReturnsGridsNull();
            }
            else
            {
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

                rgHoldings.DataSource = dtReturnsHoldings;
                rgHoldings.DataBind();
                ViewState["HoldingReturns"] = dtReturnsHoldings;

                rgAll.DataSource = dtReturnsAll;
                rgAll.DataBind();
                ViewState["AllReturns"] = dtReturnsAll;

                rgRealized.DataSource = dtReturnsRealized;
                rgRealized.DataBind();
                ViewState["RealizedReturns"] = dtReturnsRealized;
            }
        }

        private void BindTaxGrid()
        {
            SetReturnsGridsNull();
            if (intPortfolioListCount == 0)
            {
                TaxLabelVisibility(true);
                SetTaxGridsNull();
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

                rgTaxHoldings.DataSource = dtTaxHoldings;
                rgTaxHoldings.DataBind();
                ViewState["TaxHoldings"] = dtTaxHoldings;

                rgTaxRealized.DataSource = dtTaxRealized;
                rgTaxRealized.DataBind();
                ViewState["TaxRealized"] = dtTaxRealized;
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
                drMFPortfolioRealized[5] = mfVo.ReturnsRealizedInvestedCost.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[5] = "0.00";

            if (mfVo.SalesQuantity != 0)
                drMFPortfolioRealized[6] = mfVo.SalesQuantity.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[6] = "0.00";

            if (mfVo.RedeemedAmount != 0)
                drMFPortfolioRealized[7] = mfVo.RedeemedAmount.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[7] = "0.00";

            if (mfVo.ReturnsRealizedDVPAmt != 0)
                drMFPortfolioRealized[8] = mfVo.ReturnsRealizedDVPAmt.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[8] = "0.00";

            if (mfVo.ReturnsRealizedTotalDividends != 0)
                drMFPortfolioRealized[9] = mfVo.ReturnsRealizedTotalDividends.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[9] = "0.00";

            if (mfVo.ReturnsRealizedTotalPL != 0)
                drMFPortfolioRealized[10] = mfVo.ReturnsRealizedTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[10] = "0.00";

            if (mfVo.ReturnsRealizedAbsReturn != 0)
                drMFPortfolioRealized[11] = mfVo.ReturnsRealizedAbsReturn.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[11] = "0.00";

            if (mfVo.ReturnsRealizedXIRR != 0)
                drMFPortfolioRealized[12] = mfVo.ReturnsRealizedXIRR.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioRealized[12] = "0.00";

            drMFPortfolioRealized[13] = mfVo.AMCCode;
            drMFPortfolioRealized[14] = mfVo.SchemePlanCode;
            drMFPortfolioRealized[15] = mfVo.AssetInstrumentSubCategoryName;

        }

        private static void PopulateReturnsAllDataTable(DataRow drMFPortfolioAll, MFPortfolioNetPositionVo mfVo)
        {
            drMFPortfolioAll[0] = mfVo.MFPortfolioId;
            drMFPortfolioAll[1] = mfVo.AccountId;
            drMFPortfolioAll[2] = mfVo.AssetInstrumentCategoryName;
            drMFPortfolioAll[3] = mfVo.SchemePlan;
            drMFPortfolioAll[4] = mfVo.FolioNumber;

            if (mfVo.ReturnsAllPrice != 0)
                drMFPortfolioAll[5] = mfVo.ReturnsAllPrice.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[5] = "0.00";

            if (mfVo.InvestedCost != 0)
                drMFPortfolioAll[6] = mfVo.InvestedCost.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[6] = "0.00";

            if (mfVo.NetHoldings != 0)
                drMFPortfolioAll[7] = mfVo.NetHoldings.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[7] = "0.00";

            if (mfVo.MarketPrice != 0)
                drMFPortfolioAll[8] = mfVo.MarketPrice.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[8] = "0.00";

            if (mfVo.CurrentValue != 0)
                drMFPortfolioAll[9] = mfVo.CurrentValue.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[9] = "0.00";

            if (mfVo.SalesQuantity != 0)
                drMFPortfolioAll[10] = mfVo.SalesQuantity.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[10] = "0.00";

            if (mfVo.RedeemedAmount != 0)
                drMFPortfolioAll[11] = mfVo.RedeemedAmount.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[11] = "0.00";

            if (mfVo.ReturnsAllDVPAmt != 0)
                drMFPortfolioAll[12] = mfVo.ReturnsAllDVPAmt.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[12] = "0.00";

            if (mfVo.ReturnsAllTotalPL != 0)
                drMFPortfolioAll[13] = mfVo.ReturnsAllTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[13] = "0.00";

            if (mfVo.ReturnsAllAbsReturn != 0)
                drMFPortfolioAll[14] = mfVo.ReturnsAllAbsReturn.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[14] = "0.00";

            if (mfVo.ReturnsAllDVRAmt != 0)
                drMFPortfolioAll[15] = mfVo.ReturnsAllDVRAmt.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[15] = "0.00";

            if (mfVo.ReturnsAllTotalXIRR != 0)
                drMFPortfolioAll[16] = mfVo.ReturnsAllTotalXIRR.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[16] = "0.00";

            if (mfVo.ReturnsAllTotalDividends != 0)
                drMFPortfolioAll[17] = mfVo.ReturnsAllTotalDividends.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioAll[17] = "0.00";

            drMFPortfolioAll[18] = mfVo.AMCCode;
            drMFPortfolioAll[19] = mfVo.SchemePlanCode;
            drMFPortfolioAll[20] = mfVo.AssetInstrumentSubCategoryName;
        }

        private static void PopulateReturnsHoldDataTable(MFPortfolioNetPositionVo mfVo, DataRow drMFPortfolioHoldings)
        {
            drMFPortfolioHoldings[0] = mfVo.MFPortfolioId;
            drMFPortfolioHoldings[1] = mfVo.AccountId;
            if (mfVo.AssetInstrumentCategoryName != null)
                drMFPortfolioHoldings[2] = mfVo.AssetInstrumentCategoryName;
            drMFPortfolioHoldings[3] = mfVo.SchemePlan;
            drMFPortfolioHoldings[4] = mfVo.FolioNumber;

            if (mfVo.ReturnsHoldPurchaseUnit != 0)
                drMFPortfolioHoldings[5] = mfVo.ReturnsHoldPurchaseUnit.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[5] = "0.00";

            if (mfVo.ReturnsHoldDVRUnits != 0)
                drMFPortfolioHoldings[6] = mfVo.ReturnsHoldDVRUnits.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[6] = "0.00";

            if (mfVo.NetHoldings != 0)
                drMFPortfolioHoldings[7] = mfVo.NetHoldings.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[7] = "0.00";

            if (mfVo.InvestedCost != 0)
                drMFPortfolioHoldings[8] = mfVo.ReturnsHoldAcqCost.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[8] = "0.00";

            if (mfVo.MarketPrice != 0)
                drMFPortfolioHoldings[9] = mfVo.MarketPrice.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[9] = "0.00";

            if (mfVo.CurrentValue != 0)
                drMFPortfolioHoldings[10] = mfVo.CurrentValue.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[10] = "0.00";

            if (mfVo.ReturnsHoldDVPAmt != 0)
                drMFPortfolioHoldings[11] = mfVo.ReturnsHoldDVPAmt.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[11] = "0.00";

            if (mfVo.ReturnsHoldTotalPL != 0)
                drMFPortfolioHoldings[12] = mfVo.ReturnsHoldTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[12] = "0.00";

            if (mfVo.ReturnsHoldAbsReturn != 0)
                drMFPortfolioHoldings[13] = mfVo.ReturnsHoldAbsReturn.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[13] = "0.00";

            if (mfVo.ReturnsHoldXIRR != 0)
                drMFPortfolioHoldings[14] = mfVo.ReturnsHoldXIRR.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drMFPortfolioHoldings[14] = "0.00";

            drMFPortfolioHoldings[15] = mfVo.AMCCode;
            drMFPortfolioHoldings[16] = mfVo.SchemePlanCode;
            drMFPortfolioHoldings[17] = mfVo.AssetInstrumentSubCategoryName;
        }

        private static void PopulateTaxHoldDataTable(DataRow drTaxHoldings, MFPortfolioNetPositionVo mfVo)
        {
            drTaxHoldings[0] = mfVo.MFPortfolioId;
            drTaxHoldings[1] = mfVo.AccountId;
            drTaxHoldings[2] = mfVo.AssetInstrumentCategoryName;
            drTaxHoldings[3] = mfVo.SchemePlan;
            drTaxHoldings[4] = mfVo.FolioNumber;

            if (mfVo.NetHoldings != 0)
                drTaxHoldings[5] = mfVo.NetHoldings.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxHoldings[5] = "0.00";

            if (mfVo.TaxHoldBalanceAmt != 0)
                drTaxHoldings[6] = mfVo.TaxHoldBalanceAmt.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxHoldings[6] = "0.00";

            if (mfVo.MarketPrice != 0)
                drTaxHoldings[7] = mfVo.MarketPrice.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxHoldings[7] = "0.00";

            if (mfVo.CurrentValue != 0)
                drTaxHoldings[8] = mfVo.CurrentValue.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxHoldings[8] = "0.00";

            if (mfVo.TaxHoldTotalPL != 0)
                drTaxHoldings[9] = mfVo.TaxHoldTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxHoldings[9] = "0.00";

            if (mfVo.TaxHoldEligibleSTCG != 0)
                drTaxHoldings[10] = mfVo.TaxHoldEligibleSTCG.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxHoldings[10] = "0.00";

            if (mfVo.TaxHoldEligibleLTCG != 0)
                drTaxHoldings[11] = mfVo.TaxHoldEligibleLTCG.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxHoldings[11] = "0.00";

            drTaxHoldings[12] = mfVo.AMCCode;
            drTaxHoldings[13] = mfVo.SchemePlanCode;
            drTaxHoldings[14] = mfVo.AssetInstrumentSubCategoryName;

        }

        private static void PopulateTaxRealizedDataTable(DataRow drTaxRealized, MFPortfolioNetPositionVo mfVo)
        {
            drTaxRealized[0] = mfVo.MFPortfolioId;
            drTaxRealized[1] = mfVo.AccountId;
            drTaxRealized[2] = mfVo.AssetInstrumentCategoryName;
            drTaxRealized[3] = mfVo.SchemePlan;
            drTaxRealized[4] = mfVo.FolioNumber;

            if (mfVo.TaxRealizedAcqCost != 0)
                drTaxRealized[5] = mfVo.TaxRealizedAcqCost.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[5] = "0.00";

            if (mfVo.RedeemedAmount != 0)
                drTaxRealized[6] = mfVo.RedeemedAmount.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[6] = "0.00";

            if (mfVo.TaxRealizedTotalPL != 0)
                drTaxRealized[7] = mfVo.TaxRealizedTotalPL.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[7] = "0.00";

            if (mfVo.TaxRealizedSTCG != 0)
                drTaxRealized[8] = mfVo.TaxRealizedSTCG.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[8] = "0.00";

            if (mfVo.TaxRealizedLTCG != 0)
                drTaxRealized[9] = mfVo.TaxRealizedLTCG.ToString("n2", CultureInfo.CreateSpecificCulture("hi-IN"));
            else
                drTaxRealized[9] = "0.00";

            drTaxRealized[10] = mfVo.AMCCode;
            drTaxRealized[11] = mfVo.SchemePlanCode;
            drTaxRealized[12] = mfVo.AssetInstrumentSubCategoryName;
        }

        private void ReturnsLabelVisibility(bool blVisibility)
        {
            lblMessageHoldings.Visible = blVisibility;
            lblMessageAll.Visible = blVisibility;
            lblMessageRealized.Visible = blVisibility;
        }

        private void TaxLabelVisibility(bool blVisibility)
        {
            lblMessageTaxHoldings.Visible = blVisibility;
            lblTaxRealized.Visible = blVisibility;
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
            rgTaxHoldings.DataSource = null;
            rgTaxHoldings.DataBind();
            rgTaxRealized.DataSource = null;
            rgTaxRealized.DataBind();
        }

        private void ReturnsHoldingsDataTableCreation(DataTable dtReturnsHoldings)
        {
            dtReturnsHoldings.Columns.Add("MFNPId");
            dtReturnsHoldings.Columns.Add("AccountId");
            dtReturnsHoldings.Columns.Add("Category");
            dtReturnsHoldings.Columns.Add("Scheme");
            dtReturnsHoldings.Columns.Add("FolioNum");
            dtReturnsHoldings.Columns.Add("PurchasedUnits");
            dtReturnsHoldings.Columns.Add("DVRUnits");
            dtReturnsHoldings.Columns.Add("OpenUnits");
            dtReturnsHoldings.Columns.Add("InvestedCost", typeof(double));
            dtReturnsHoldings.Columns.Add("NAV");
            dtReturnsHoldings.Columns.Add("MarketValue");
            dtReturnsHoldings.Columns.Add("DVP");
            dtReturnsHoldings.Columns.Add("TotalPL", typeof(double));
            dtReturnsHoldings.Columns.Add("AbsoluteReturn");
            dtReturnsHoldings.Columns.Add("XIRR");
            dtReturnsHoldings.Columns.Add("AMCCode");
            dtReturnsHoldings.Columns.Add("SchemeCode");
            dtReturnsHoldings.Columns.Add("SubCategoryName");
        }

        private void ReturnsAllDataTableCreation(DataTable dtReturnsAll)
        {
            dtReturnsAll.Columns.Add("MFNPId");
            dtReturnsAll.Columns.Add("AccountId");
            dtReturnsAll.Columns.Add("Category");
            dtReturnsAll.Columns.Add("Scheme");
            dtReturnsAll.Columns.Add("FolioNum");
            dtReturnsAll.Columns.Add("Price");
            dtReturnsAll.Columns.Add("InvestedCost");
            dtReturnsAll.Columns.Add("OpenUnits");
            dtReturnsAll.Columns.Add("NAV");
            dtReturnsAll.Columns.Add("CurrentValue");
            dtReturnsAll.Columns.Add("UnitsSold");
            dtReturnsAll.Columns.Add("RedeemedAmount");
            dtReturnsAll.Columns.Add("DVP");
            dtReturnsAll.Columns.Add("TotalPL");
            dtReturnsAll.Columns.Add("AbsoluteReturn");
            dtReturnsAll.Columns.Add("DVR");
            dtReturnsAll.Columns.Add("XIRR");
            dtReturnsAll.Columns.Add("TotalDividends");
            dtReturnsAll.Columns.Add("AMCCode");
            dtReturnsAll.Columns.Add("SchemeCode");
            dtReturnsAll.Columns.Add("SubCategoryName");
        }

        private void ReturnsRealizedDataTableCreation(DataTable dtReturnsRealized)
        {
            dtReturnsRealized.Columns.Add("MFNPId");
            dtReturnsRealized.Columns.Add("AccountId");
            dtReturnsRealized.Columns.Add("Category");
            dtReturnsRealized.Columns.Add("Scheme");
            dtReturnsRealized.Columns.Add("FolioNum");
            dtReturnsRealized.Columns.Add("InvestedCost");
            dtReturnsRealized.Columns.Add("UnitsSold");
            dtReturnsRealized.Columns.Add("RedeemedAmount");
            dtReturnsRealized.Columns.Add("DVP");
            dtReturnsRealized.Columns.Add("TotalDividends");
            dtReturnsRealized.Columns.Add("TotalPL");
            dtReturnsRealized.Columns.Add("AbsoluteReturn");
            dtReturnsRealized.Columns.Add("XIRR");
            dtReturnsRealized.Columns.Add("AMCCode");
            dtReturnsRealized.Columns.Add("SchemeCode");
            dtReturnsRealized.Columns.Add("SubCategoryName");
        }

        private void TaxHoldingsDataTableCreation(DataTable dtTaxHoldings)
        {
            dtTaxHoldings.Columns.Add("MFNPId");
            dtTaxHoldings.Columns.Add("AccountId");
            dtTaxHoldings.Columns.Add("Category");
            dtTaxHoldings.Columns.Add("Scheme");
            dtTaxHoldings.Columns.Add("FolioNum");
            dtTaxHoldings.Columns.Add("OpenUnits");
            dtTaxHoldings.Columns.Add("BalanceAmount");
            dtTaxHoldings.Columns.Add("NAV");
            dtTaxHoldings.Columns.Add("MarketValue");
            dtTaxHoldings.Columns.Add("UnrealizedPL");
            dtTaxHoldings.Columns.Add("EligibleSTCG");
            dtTaxHoldings.Columns.Add("EligibleLTCG");
            dtTaxHoldings.Columns.Add("AMCCode");
            dtTaxHoldings.Columns.Add("SchemeCode");
            dtTaxHoldings.Columns.Add("SubCategoryName");
        }

        private void TaxRealizedDataTableCreation(DataTable dtTaxRealized)
        {
            dtTaxRealized.Columns.Add("MFNPId");
            dtTaxRealized.Columns.Add("AccountId");
            dtTaxRealized.Columns.Add("Category");
            dtTaxRealized.Columns.Add("Scheme");
            dtTaxRealized.Columns.Add("FolioNum");
            dtTaxRealized.Columns.Add("AcquisitionCost");
            dtTaxRealized.Columns.Add("RedeemedAmount");
            dtTaxRealized.Columns.Add("TotalPL");
            dtTaxRealized.Columns.Add("STCG");
            dtTaxRealized.Columns.Add("LTCG");
            dtTaxRealized.Columns.Add("AMCCode");
            dtTaxRealized.Columns.Add("SchemeCode");
            dtTaxRealized.Columns.Add("SubCategoryName");
        }

        private void BindPerformaceChart()
        {
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

        #region Returns Tab Events

        protected void lnlGoBackHoldings_Click(object sender, EventArgs e)
        {

        }
        protected void rgHoldings_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select" || e.CommandName == "NavigateToMarketData")
            {
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

                    Session["Folio"] = dataItem["FolioNum"].Text;
                    Session["Scheme"] = dataItem["Scheme"].Text;
                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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

                    Session["Folio"] = dataItem["FolioNum"].Text;
                    Session["Scheme"] = dataItem["Scheme"].Text;
                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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

                    Session["Folio"] = dataItem["FolioNum"].Text;
                    Session["Scheme"] = dataItem["Scheme"].Text;
                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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

                    Session["Folio"] = dataItem["FolioNum"].Text;
                    Session["Scheme"] = dataItem["Scheme"].Text;
                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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

                    Session["Folio"] = dataItem["FolioNum"].Text;
                    Session["Scheme"] = dataItem["Scheme"].Text;
                    Hashtable ht = new Hashtable();
                    ht["From"] = strFromDate;
                    ht["To"] = strToDate;
                    Session["tranDates"] = ht;

                    #endregion

                    //Response.Redirect("ControlHost.aspx?pageid=TransactionsView&Folio=" + strFolio + "&Scheme=" + strScheme + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "", false);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('TransactionsView','none');", true);
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
                SetPanelVisibility(false, false);
            }
            else if (ddlDisplayType.SelectedIndex == 1)
            {
                SetPanelVisibility(true, false);
                BindReturnsGrid();
                BindPerformaceChart();
            }
            else if (ddlDisplayType.SelectedIndex == 2)
            {
                SetPanelVisibility(false, true);
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
        }

        protected void rgAll_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["AllReturns"] != null)
            {
                dt = (DataTable)ViewState["AllReturns"];
                rgAll.DataSource = dt;
            }
        }

        protected void rgRealized_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["RealizedReturns"] != null)
            {
                dt = (DataTable)ViewState["RealizedReturns"];
                rgRealized.DataSource = dt;
            }
        }

        protected void rgTaxHoldings_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["TaxHoldings"] != null)
            {
                dt = (DataTable)ViewState["TaxHoldings"];
                rgTaxHoldings.DataSource = dt;
            }
        }

        protected void rgTaxRealized_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["TaxRealized"] != null)
            {
                dt = (DataTable)ViewState["TaxRealized"];
                rgTaxRealized.DataSource = dt;
            }
        }
    }
}
