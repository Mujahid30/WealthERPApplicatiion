using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerPortfolio;
using BoFPSuperlite;
using BoReports;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using VoReports;
using System.Xml;
using InfoSoftGlobal; // Namespace added for Fusionchart Implementation...

namespace WealthERP.FP
{
    public partial class CustomerFPAnalyticsStandard : System.Web.UI.UserControl
    {
        double asset = 0;
        double liabilities = 0;
        double networth = 0;
        double totalAnnualIncome = 0;
        int dynamicRiskClass = 0;
        int financialAssetTotal = 0;
        string riskClass = string.Empty;
        FinancialPlanningVo fpSectional;
        DataTable dtIncome;
        DataTable dtExpense;
        DataTable dtAsset;
        DataTable dtCashFlow;
        DataTable dtLiabilities;
        DataTable dtLifeInsurance;
        DataTable dtGeneralInsurance;
        DataTable dtHLVAnalysis;
        DataTable dtHLVBasedIncome;
        DataTable dtCustomerFPRatio;
        //DataSet ds = new DataSet();
        //AssetBo assetBo = new AssetBo();

        AdvisorVo advisorVo;
        CustomerVo customerVo;
        DataSet dsGetCustomerFPAnalyticsStandard;
        CustomerProspectBo customerprospectbo = new CustomerProspectBo();
        FinancialPlanningReportsBo financialplanningreportsbo = new FinancialPlanningReportsBo();
        CustomerFPAnalyticsBo ObjcustomerFPAnalyticsbo = new CustomerFPAnalyticsBo();
        
        DataSet dsGetFPAnalyticsDataforFCharts = new DataSet(); // DataSet added to store data for FusionChart..
        int CustomerId;
        XmlTextWriter writer;
        XmlTextWriter writer1;
        System.Guid guid = System.Guid.NewGuid();

        string fileExtension = ".xml";

        string XMLFileName = "";
        string XMLAreaChartFileName = "";
     

        protected void Page_Load(object sender, EventArgs e)
        {

            /* **************  This Code is written by Bhoopendra Sahoo for Standerd Charts and Grids  ***************** */

            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];

            fpSectional.advisorId = advisorVo.advisorId;
            fpSectional.CustomerId = customerVo.CustomerId.ToString();

            dsGetCustomerFPAnalyticsStandard = financialplanningreportsbo.GetCustomerFPDetails(fpSectional, out asset, out liabilities, out networth, out riskClass, out dynamicRiskClass, out totalAnnualIncome, out financialAssetTotal);
            dtIncome = dsGetCustomerFPAnalyticsStandard.Tables["Income"];
            dtExpense = dsGetCustomerFPAnalyticsStandard.Tables["Expense"];
            dtCashFlow = dsGetCustomerFPAnalyticsStandard.Tables["CashFlow"];
            dtAsset = dsGetCustomerFPAnalyticsStandard.Tables["AssetDetails"];
            dtLiabilities = dsGetCustomerFPAnalyticsStandard.Tables["LiabilitiesDetail"];
            dtLifeInsurance = dsGetCustomerFPAnalyticsStandard.Tables["LifeInsurance"];
            dtGeneralInsurance = dsGetCustomerFPAnalyticsStandard.Tables["GeneralInsurance"];
            dtHLVAnalysis = dsGetCustomerFPAnalyticsStandard.Tables["HLV"];
            dtHLVBasedIncome = dsGetCustomerFPAnalyticsStandard.Tables["HLVBasedIncome"];
            dtCustomerFPRatio = dsGetCustomerFPAnalyticsStandard.Tables["CustomerFPRatio"];
            if (!IsPostBack)
            {
                BindIncomeGridChart(dsGetCustomerFPAnalyticsStandard.Tables["Income"]);
                BindExpenseGridChart(dsGetCustomerFPAnalyticsStandard.Tables["Expense"]);
                BindCashChart(dsGetCustomerFPAnalyticsStandard.Tables["CashFlow"]);
                BindAssetsGridChart(dsGetCustomerFPAnalyticsStandard.Tables["AssetDetails"]);
                BindLiabilitiesGridChart(dsGetCustomerFPAnalyticsStandard.Tables["LiabilitiesDetail"]);
                BindGridGeneralInsurance(dsGetCustomerFPAnalyticsStandard.Tables["GeneralInsurance"]);
                BindRepFinancialHealth(dsGetCustomerFPAnalyticsStandard.Tables["CustomerFPRatio"]);

                if (dtLifeInsurance.Rows.Count > 0 && dtHLVAnalysis.Rows.Count > 0 && dtHLVBasedIncome.Rows.Count > 0)
                {
                BindGridLifeInsurance(dsGetCustomerFPAnalyticsStandard.Tables["LifeInsurance"]);
                BindGridHLVAnalysis(dsGetCustomerFPAnalyticsStandard.Tables["HLV"]);
                BindGridInsuranceGapAnalysis(dsGetCustomerFPAnalyticsStandard.Tables["HLVBasedIncome"]);
                trErrorLifeInsurance.Visible = false;
                }
                else
                {
                    trErrorLifeInsurance.Visible = true;
                    trRadGridLifeInsurance.Visible = false;
                    trRadGridHLVAnalysis.Visible = false;
                    trRadGridLIGapAnalysis.Visible = false;
                }
                
            }

            /* **************  Bhoopendra Sahoo code Ends Here in Page_load ***************** */






            /* ************* Code Starts here for Fusion Chart Implementation (by Vinayak Patil) *********** */
            CustomerId = customerVo.CustomerId;


            if (!IsPostBack)
            {
                dsGetFPAnalyticsDataforFCharts = ObjcustomerFPAnalyticsbo.FutureSurplusEngine(CustomerId);
                if (dsGetFPAnalyticsDataforFCharts.Tables.Count != 0)
                {
                    if (dsGetFPAnalyticsDataforFCharts.Tables[0].Rows.Count > 0)
                    {
                        ErrorMessageLineChart.Visible = false;
                        ErrorMessageAreaChart.Visible = false;
                        tblLineChart.Visible = true;
                        tblAreaChart.Visible = true;

                        FusionLineChartXMLSetUp();
                        FusionAreaChartXMLSetUp();
                        FinalSetUpForShowingFusionChart();
                    }
                    else
                    {
                        tblLineChart.Visible = false;
                        tblAreaChart.Visible = false;
                        ErrorMessageLineChart.Visible = true;
                        ErrorMessageAreaChart.Visible = true;
                    }
                }
                else
                {
                    tblLineChart.Visible = false;
                    tblAreaChart.Visible = false;
                    ErrorMessageLineChart.Visible = true;
                    ErrorMessageAreaChart.Visible = true;
                }

            }
            /* *************        Page_Load code Ends here (Vinayak Patil)              *********** */
        }





        /* *** XML SetUp Code Starts here for Fusion Chart Implementation by (Vinayak Patil)  *** */

        private void FusionLineChartXMLSetUp()
        {
            FunctionForCreateLineChartXMLFile();

            FusionChartCommonFirstPartXMLSetUp();

            XMLSetUpForLineChart();

            FusionChartCommonLastPartXMLSetUp();
           
        }
        private void FusionAreaChartXMLSetUp()
        {
            FunctionForCreateAreaChartXMLFile();

            FusionChartAreaChartCommonFirstPartXMLSetUp();

            XMLSetUpForAreaChart();

            FusionChartAreaChartCommonLastPartXMLSetUp();
        }

        private void FunctionForCreateLineChartXMLFile()
        {
            XMLFileName = "LineChartXML" + guid + CustomerId + fileExtension;
            writer = new XmlTextWriter(Server.MapPath("~/FusionChartXMLFiles/") + XMLFileName, null);
        }

        private void FunctionForCreateAreaChartXMLFile()
        {
            XMLAreaChartFileName = "AreaChartXML" + guid + CustomerId + fileExtension;
            writer1 = new XmlTextWriter(Server.MapPath("~/FusionChartXMLFiles/") + XMLAreaChartFileName, null);
        }


        /*  XML SetUp code Ends here */


        /* *** XML SetUp for Line Chart Code Starts here for Fusion Chart Implementation by (Vinayak Patil)  *** */

        private void XMLSetUpForLineChart()
        {
            // For Income
            writer.WriteStartElement("dataset");
            writer.WriteAttributeString("seriesName", "" + dsGetFPAnalyticsDataforFCharts.Tables[0].Columns["Income"].ToString() + "");
            writer.WriteAttributeString("renderAs", "Line");
            writer.WriteAttributeString("color", "006600");
            writer.WriteWhitespace("\n");
            foreach (DataRow dr in dsGetFPAnalyticsDataforFCharts.Tables[0].Rows)
            {
                writer.WriteStartElement("set");
                writer.WriteAttributeString("value", "" + dr["Income"].ToString() + "");
                writer.WriteEndElement();
                writer.WriteWhitespace("\n");
            }
            writer.WriteEndElement();
            writer.WriteWhitespace("\n");

            // For Expense
            writer.WriteStartElement("dataset");
            writer.WriteAttributeString("seriesName", "" + dsGetFPAnalyticsDataforFCharts.Tables[0].Columns["Expense"].ToString() + "");
            writer.WriteAttributeString("color", "#FF0000");
            writer.WriteAttributeString("renderAs", "Line");
            writer.WriteWhitespace("\n");
            foreach (DataRow dr in dsGetFPAnalyticsDataforFCharts.Tables[0].Rows)
            {
                writer.WriteStartElement("set");
                writer.WriteAttributeString("value", "" + dr["Expense"].ToString() + "");
                writer.WriteEndElement();
                writer.WriteWhitespace("\n");
            }
            writer.WriteEndElement();
            writer.WriteWhitespace("\n");

            // For Available Surplus
            writer.WriteStartElement("dataset");
            writer.WriteAttributeString("seriesName", "" + dsGetFPAnalyticsDataforFCharts.Tables[0].Columns["AvailableSurplus"].ToString() + "");
            writer.WriteAttributeString("parentYAxis", "S");
            writer.WriteAttributeString("color", "3300CC");
            writer.WriteAttributeString("renderAs", "Area");
            writer.WriteWhitespace("\n");
            foreach (DataRow dr in dsGetFPAnalyticsDataforFCharts.Tables[0].Rows)
            {
                decimal total;
                total = decimal.Parse(dr["Income"].ToString()) - decimal.Parse(dr["Expense"].ToString());
                writer.WriteStartElement("set");
                writer.WriteAttributeString("value", "" + total + "");
                writer.WriteEndElement();
                writer.WriteWhitespace("\n");
            }
            writer.WriteEndElement();
            writer.WriteWhitespace("\n");
        }

        /* XML SetUp for LineChart code Ends here */




        /* *** XML SetUp for Area Chart Code Starts here for Fusion Chart Implementation by (Vinayak Patil)  *** */

        private void XMLSetUpForAreaChart()
        {
            decimal CashCorpus = 0;
            decimal DebtCorpus = 0;
            decimal EquityCorpus = 0;
            decimal AlternateCorpus = 0;
            decimal TotalCorpus = 0;

            decimal CashGoal = 0;
            decimal DebtGoal = 0;
            decimal EquityGoal = 0;
            decimal AlternateGoal = 0;
            decimal TotalGoal = 0;


            // For  Total Corpus
            writer1.WriteStartElement("dataset");
            writer1.WriteAttributeString("seriesName", "Total Corpus");
            writer1.WriteAttributeString("renderAs", "Area");
            writer1.WriteAttributeString("color", "330033");
            writer1.WriteWhitespace("\n");
            int TotalCorpusTempYear = int.Parse(DateTime.Now.Year.ToString());
            for (int i = 0; i <= dsGetFPAnalyticsDataforFCharts.Tables[0].Rows.Count; i++)
            {
                DataRow[] drTodayToEol = dsGetFPAnalyticsDataforFCharts.Tables[1].Select("Year=" + TotalCorpusTempYear);

                foreach (DataRow drEOL in drTodayToEol)
                {
                    // Total Corpus Calculation..
                    if (drEOL["AssetClass"].ToString() == "Cash")
                        CashCorpus = Decimal.Parse(drEOL["BalanceMoney"].ToString());
                    if (drEOL["AssetClass"].ToString() == "Debt")
                        DebtCorpus = Decimal.Parse(drEOL["BalanceMoney"].ToString());
                    if (drEOL["AssetClass"].ToString() == "Equity")
                        EquityCorpus = Decimal.Parse(drEOL["BalanceMoney"].ToString());
                    if (drEOL["AssetClass"].ToString() == "Alternate")
                        AlternateCorpus = Decimal.Parse(drEOL["BalanceMoney"].ToString());

                    TotalCorpus = (CashCorpus + DebtCorpus + EquityCorpus + AlternateCorpus);
                }

                writer1.WriteStartElement("set");
                writer1.WriteAttributeString("value", "" + TotalCorpus + "");
                writer1.WriteEndElement();
                writer1.WriteWhitespace("\n");
                TotalCorpusTempYear++;
            }
            writer1.WriteEndElement();
            writer1.WriteWhitespace("\n");

            // For Goal Withdrawls

            writer1.WriteStartElement("dataset");
            writer1.WriteAttributeString("seriesName", "Goal Withdrawls");
            writer1.WriteAttributeString("renderAs", "Area");
            writer1.WriteWhitespace("\n");

            int GoalWithdrawnTempYear = int.Parse(DateTime.Now.Year.ToString());

            for (int i = 0; i <= dsGetFPAnalyticsDataforFCharts.Tables[0].Rows.Count; i++)
            {
                DataRow[] drTodayToEolForGoal = dsGetFPAnalyticsDataforFCharts.Tables[1].Select("Year=" + GoalWithdrawnTempYear);

                foreach (DataRow drEOL in drTodayToEolForGoal)
                {
                    // Total Corpus Calculation..
                    if (drEOL["AssetClass"].ToString() == "Cash")
                        CashGoal = Decimal.Parse(drEOL["GoalMoneyWithdrawn"].ToString());
                    if (drEOL["AssetClass"].ToString() == "Debt")
                        DebtGoal = Decimal.Parse(drEOL["GoalMoneyWithdrawn"].ToString());
                    if (drEOL["AssetClass"].ToString() == "Equity")
                        EquityGoal = Decimal.Parse(drEOL["GoalMoneyWithdrawn"].ToString());
                    if (drEOL["AssetClass"].ToString() == "Alternate")
                        AlternateGoal = Decimal.Parse(drEOL["GoalMoneyWithdrawn"].ToString());

                    TotalGoal = (CashGoal + DebtGoal + EquityGoal + AlternateGoal);
                }
                writer1.WriteStartElement("set");
                writer1.WriteAttributeString("value", "" + TotalGoal + "");
                writer1.WriteEndElement();
                writer1.WriteWhitespace("\n");
                GoalWithdrawnTempYear++;
            }
        
            writer1.WriteEndElement();
            writer1.WriteWhitespace("\n");

            // For Cash Corpus
            writer1.WriteStartElement("dataset");
            writer1.WriteAttributeString("seriesName", "Cash Corpus");
            writer1.WriteAttributeString("renderAs", "Area");
            writer1.WriteAttributeString("color", "996633");
            
            
            writer1.WriteWhitespace("\n");

            int CashCopusTempYear = int.Parse(DateTime.Now.Year.ToString());

            for (int i = 0; i <= dsGetFPAnalyticsDataforFCharts.Tables[0].Rows.Count; i++)
            {
                DataRow[] drTodayToEolForCashCorpus = dsGetFPAnalyticsDataforFCharts.Tables[1].Select("Year=" + CashCopusTempYear);

                foreach (DataRow drEOL in drTodayToEolForCashCorpus)
                {
                    
                    if (drEOL["AssetClass"].ToString() == "Cash")
                    {
                        writer1.WriteStartElement("set");
                        writer1.WriteAttributeString("value", "" + drEOL["BalanceMoney"].ToString() + "");
                        writer1.WriteEndElement();
                        writer1.WriteWhitespace("\n");
                    }
                        
                }
                CashCopusTempYear++;
            }
            writer1.WriteEndElement();
            writer1.WriteWhitespace("\n");

            // For Debt Corpus
            writer1.WriteStartElement("dataset");
            writer1.WriteAttributeString("seriesName", "Debt Corpus");
            writer1.WriteAttributeString("renderAs", "Area");
            writer1.WriteWhitespace("\n");
            
            int DebtCopusTempYear = int.Parse(DateTime.Now.Year.ToString());

            for (int i = 0; i <= dsGetFPAnalyticsDataforFCharts.Tables[0].Rows.Count; i++)
            {
                DataRow[] drTodayToEolForDebtCorpus = dsGetFPAnalyticsDataforFCharts.Tables[1].Select("Year=" + DebtCopusTempYear);

                foreach (DataRow dr in drTodayToEolForDebtCorpus)
                {
                    
                    if (dr["AssetClass"].ToString() == "Debt")
                    {
                        writer1.WriteStartElement("set");
                        writer1.WriteAttributeString("value", "" + dr["BalanceMoney"].ToString() + "");
                        writer1.WriteEndElement();
                        writer1.WriteWhitespace("\n");
                    }
                       
                }
                DebtCopusTempYear++;
            }
            writer1.WriteEndElement();
            writer1.WriteWhitespace("\n");

            // For Equity Corpus
            writer1.WriteStartElement("dataset");
            writer1.WriteAttributeString("seriesName", "Equity Corpus");
            writer1.WriteAttributeString("renderAs", "Area");
            writer1.WriteWhitespace("\n");

            int EquityCopusTempYear = int.Parse(DateTime.Now.Year.ToString());

            for (int i = 0; i <= dsGetFPAnalyticsDataforFCharts.Tables[0].Rows.Count; i++)
            {
                DataRow[] drTodayToEolForEquityCorpus = dsGetFPAnalyticsDataforFCharts.Tables[1].Select("Year=" + EquityCopusTempYear);
                foreach (DataRow dr in drTodayToEolForEquityCorpus)
                {
                    
                    if (dr["AssetClass"].ToString() == "Equity")
                    {
                        writer1.WriteStartElement("set");
                        writer1.WriteAttributeString("value", "" + dr["BalanceMoney"].ToString() + "");
                        writer1.WriteEndElement();
                        writer1.WriteWhitespace("\n");
                    }
                        
                }
                EquityCopusTempYear++;
            }
            writer1.WriteEndElement();
            writer1.WriteWhitespace("\n");
        }

        /* XML SetUp for LineChart code Ends here */


        /* Common XMLSetUp for both the charts */

        private void FusionChartCommonFirstPartXMLSetUp()
        {
            // Common XML SetUp //
            // Use automatic indentation for readability.
            writer.Formatting = Formatting.Indented;

            //Write the root element
            writer.WriteStartElement("chart");
            writer.WriteAttributeString("palette", "2");
            writer.WriteAttributeString("caption", "FP Engine Income, Expense and Surplus Chart");
            writer.WriteAttributeString("xAxisName", "Years");
            writer.WriteAttributeString("yAxisName", "Values");
            writer.WriteAttributeString("palette", "1");
            writer.WriteAttributeString("useRoundEdges", "1");
            writer.WriteAttributeString("showLabels", "1");
            writer.WriteAttributeString("showValues", "0");
            writer.WriteAttributeString("valuePosition", "auto");
            writer.WriteAttributeString("allowSelection", "1");
            writer.WriteAttributeString("chartOrder", "area,line");
            
            
            writer.WriteAttributeString("exportEnabled", "1");
            writer.WriteAttributeString("exportShowMenuItem", "1");
            
            


            writer.WriteAttributeString("labelDisplay", "Rotate");
            writer.WriteAttributeString("height", "500");
            writer.WriteAttributeString("animate3D", "1");
            writer.WriteAttributeString("slantLabels", "1");
            writer.WriteAttributeString("rotateValues", "1");
            writer.WriteAttributeString("numDivLines", "10");
            writer.WriteAttributeString("plotFillAlpha", "95");
            writer.WriteAttributeString("formatNumberScale", "0");
            writer.WriteAttributeString("divLineEffect", "emboss");
            writer.WriteAttributeString("clustered", "0");
            writer.WriteAttributeString("exeTime", "1.5");
            writer.WriteAttributeString("showPlotBorder", "0");
            writer.WriteAttributeString("startAngX", "10");
            writer.WriteAttributeString("endAngX", "18");
            writer.WriteAttributeString("startAngY", "-10");
            writer.WriteAttributeString("endAngY", "-40");
            writer.WriteAttributeString("rotateNames", "0");

            writer.WriteAttributeString("showDivLineSecondaryValue", "0");
            writer.WriteAttributeString("showSecondaryLimits", "0");


            writer.WriteWhitespace("\n");

            //Start an Category element
            writer.WriteStartElement("categories");
            writer.WriteWhitespace("\n");

            foreach (DataRow dr in dsGetFPAnalyticsDataforFCharts.Tables[0].Rows)
            {
                //Start an category element
                writer.WriteStartElement("category");

                writer.WriteAttributeString("label", "" + dr["Year"].ToString() + "");
                writer.WriteEndElement();
                //End Of category
                writer.WriteWhitespace("\n");
            }

            writer.WriteEndElement();
            //End Of Category
            writer.WriteWhitespace("\n");
        }
        /* Common FirstPart XMLSetUp ends here */

        /* Common FirstPart AreaChartXMLSetUp starts here */

        private void FusionChartAreaChartCommonFirstPartXMLSetUp()
        {
            // Common XML SetUp //
            // Use automatic indentation for readability.
            writer1.Formatting = Formatting.Indented;

            //Write the root element
            writer1.WriteStartElement("chart");
            writer1.WriteAttributeString("palette", "2");
            writer1.WriteAttributeString("caption", "FP Engine Corpus Chart");
            writer1.WriteAttributeString("xAxisName", "Years");
            writer1.WriteAttributeString("yAxisName", "Values");
            writer1.WriteAttributeString("palette", "1");
            writer1.WriteAttributeString("useRoundEdges", "1");
            writer1.WriteAttributeString("showLabels", "1");
            writer1.WriteAttributeString("showValues", "0");
            writer1.WriteAttributeString("valuePosition", "auto");
            writer1.WriteAttributeString("labelDisplay", "Rotate");
            writer1.WriteAttributeString("height", "500");
            writer1.WriteAttributeString("animate3D", "1");
            writer1.WriteAttributeString("slantLabels", "1");
            writer1.WriteAttributeString("rotateValues", "1");
            writer1.WriteAttributeString("numDivLines", "10");
            writer1.WriteAttributeString("plotFillAlpha", "95");
            writer1.WriteAttributeString("formatNumberScale", "0");
            writer1.WriteAttributeString("divLineEffect", "emboss");
            writer1.WriteAttributeString("clustered", "0");
            writer1.WriteAttributeString("exeTime", "1.5");
            writer1.WriteAttributeString("showPlotBorder", "0");
            writer1.WriteAttributeString("startAngX", "10");
            writer1.WriteAttributeString("endAngX", "18");
            writer1.WriteAttributeString("startAngY", "-10");
            writer1.WriteAttributeString("endAngY", "-40");
            writer1.WriteAttributeString("rotateNames", "1");
            writer1.WriteAttributeString("showplotborder", "0");
            writer1.WriteAttributeString("plotGradientColor", "FFFFFF");
           
            
            
            //writer1.WriteAttributeString("_yScale", "10");
            //writer1.WriteAttributeString("paletteColors", "FF0000,0372AB,FF5904");
            
            
            

            // For New Implementaion
            writer1.WriteAttributeString("showPlotBorder", "0");
            writer1.WriteAttributeString("startAngX", "26");
            writer1.WriteAttributeString("startAngY", "-56");
            writer1.WriteAttributeString("endAngX", "26");
            writer1.WriteAttributeString("endAngY", "-56");
            writer1.WriteAttributeString("zDepth", "50");
            writer1.WriteAttributeString("zPlotGap", "30");
            
            writer1.WriteWhitespace("\n");

            //Start an Category element
            writer1.WriteStartElement("categories");
            writer1.WriteWhitespace("\n");

            foreach (DataRow dr in dsGetFPAnalyticsDataforFCharts.Tables[0].Rows)
            {
                //Start an category element
                writer1.WriteStartElement("category");

                writer1.WriteAttributeString("label", "" + dr["Year"].ToString() + "");
                writer1.WriteEndElement();
                //End Of category
                writer1.WriteWhitespace("\n");
            }

            writer1.WriteEndElement();
            //End Of Category
            writer1.WriteWhitespace("\n");
        }
        /* Common FirstPart AreaChartXMLSetUp ends here */


        /* Common LastPart XMLSetUp */ 

        private void FusionChartCommonLastPartXMLSetUp()
        {
            //Start trendlines element
            writer.WriteStartElement("trendlines");
            writer.WriteWhitespace("\n");

            //Start line element
            writer.WriteStartElement("line");
            writer.WriteAttributeString("startValue", "26000");
            writer.WriteAttributeString("color", "91C728");
            writer.WriteAttributeString("displayValue", "Target");
            writer.WriteAttributeString("showOnTop", "1");
            writer.WriteEndElement();
            //End of line
            writer.WriteWhitespace("\n");

            writer.WriteEndElement();
            //End of trendlines
            writer.WriteWhitespace("\n");

            //Start styles element
            writer.WriteStartElement("styles");
            writer.WriteWhitespace("\n");

            //Start definition element
            writer.WriteStartElement("definition");
            writer.WriteWhitespace("\n");

            //Start style element
            writer.WriteStartElement("style");
            writer.WriteAttributeString("name", "CanvasAnim");
            writer.WriteAttributeString("type", "animation");
            writer.WriteAttributeString("param", "_xScale");
            writer.WriteAttributeString("start", "0");
            writer.WriteAttributeString("duration", "1");
            writer.WriteEndElement();
            //End of style
            writer.WriteWhitespace("\n");

            writer.WriteEndElement();
            //End of definition
            writer.WriteWhitespace("\n");


            //Start application element
            writer.WriteStartElement("application");
            writer.WriteWhitespace("\n");

            //Start style element
            writer.WriteStartElement("apply");
            writer.WriteAttributeString("toObject", "Canvas");
            writer.WriteAttributeString("styles", "CanvasAnim");
            writer.WriteEndElement();
            //End of style
            writer.WriteWhitespace("\n");

            writer.WriteEndElement();
            //End of application
            writer.WriteWhitespace("\n");

            writer.WriteEndElement();
            //End of styles
            writer.WriteWhitespace("\n");

            writer.WriteFullEndElement();
            //End of Chart
            writer.Close();
            //End of XML File
        }

        /* Common LastPart XMLSetUp Ends here */

        /* Common LastPart XMLSetUp For AreaChart */

        private void FusionChartAreaChartCommonLastPartXMLSetUp()
        {
            //Start trendlines element
            writer1.WriteStartElement("trendlines");
            writer1.WriteWhitespace("\n");

            //Start line element
            writer1.WriteStartElement("line");
            writer1.WriteAttributeString("startValue", "26000");
            writer1.WriteAttributeString("color", "91C728");
            writer1.WriteAttributeString("displayValue", "Target");
            writer1.WriteAttributeString("showOnTop", "1");
            writer1.WriteEndElement();
            //End of line
            writer1.WriteWhitespace("\n");

            writer1.WriteEndElement();
            //End of trendlines
            writer1.WriteWhitespace("\n");

            //Start styles element
            writer1.WriteStartElement("styles");
            writer1.WriteWhitespace("\n");

            //Start definition element
            writer1.WriteStartElement("definition");
            writer1.WriteWhitespace("\n");

            //Start style element
            writer1.WriteStartElement("style");
            writer1.WriteAttributeString("name", "CanvasAnim");
            writer1.WriteAttributeString("type", "animation");
            writer1.WriteAttributeString("param", "_xScale");
            writer1.WriteAttributeString("start", "0");
            writer1.WriteAttributeString("duration", "1");
            writer1.WriteEndElement();
            //End of style
            writer1.WriteWhitespace("\n");

            writer1.WriteEndElement();
            //End of definition
            writer1.WriteWhitespace("\n");


            //Start application element
            writer1.WriteStartElement("application");
            writer1.WriteWhitespace("\n");

            //Start style element
            writer1.WriteStartElement("apply");
            writer1.WriteAttributeString("toObject", "Canvas");
            writer1.WriteAttributeString("styles", "CanvasAnim");
            writer1.WriteEndElement();
            //End of style
            writer1.WriteWhitespace("\n");

            writer1.WriteEndElement();
            //End of application
            writer1.WriteWhitespace("\n");

            writer1.WriteEndElement();
            //End of styles
            writer1.WriteWhitespace("\n");

            writer1.WriteFullEndElement();
            //End of Chart
            writer1.Close();
            //End of XML File
        }

        /* Common LastPart XMLSetUp Ends here */


        /* Final XMLSetUp For Fusion Charts Starts here (By Vinayak Patil) */

        private void FinalSetUpForShowingFusionChart()
        {
            ShowFusionLineChart();
            ShowFusionAreaChart();
        }

        /* Final XMLSetUp Ends here */

        /* Show Line Chart */
        private void ShowFusionLineChart()
        {
            literalLineChart.Text = literalLineChart.Text = FusionCharts.RenderChart("FusionCharts/MSCombi2D.swf", "FusionChartXMLFiles/" + XMLFileName + "", "", "myFirst", "90%", "500", false, true, true);
        }

        /* Show Area Chart */
        private void ShowFusionAreaChart()
        {
            literalAreaChart.Text = literalAreaChart.Text = FusionCharts.RenderChart("FusionCharts/MSCombi3D.swf", "FusionChartXMLFiles/" + XMLAreaChartFileName + "", "", "myFirstArea", "90%", "500", false, false, true);
        }
        






        /* **************  This Code is written by Bhoopendra Sahoo for Standerd Charts and Grids  ***************** */

        public void BindIncomeGridChart(DataTable dtIncome)
        {        
            Series Income = new Series("Income");

            try
            {
                Legend legend = new Legend("ChartIncomeLegend");
                legend.Enabled = true;

                if (dtIncome.Rows.Count > 0)
                {
                                   
                    // LoadChart 
                    Income.ChartType = SeriesChartType.Pie;
                    ChartIncome.DataSource = dtIncome.DefaultView;
                    ChartIncome.Series.Clear();
                    ChartIncome.Series.Add(Income);
                    ChartIncome.Series[0].XValueMember = "IncomeCategory";
                    ChartIncome.Series[0].XValueType = ChartValueType.String;
                    ChartIncome.Series[0].YValueMembers = "IncomeAmount";

                    ChartIncome.Palette = ChartColorPalette.Pastel;
                    ChartIncome.PaletteCustomColors = new Color[]{Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};

                    ChartIncome.Legends.Add(legend);
                    ChartIncome.Legends["ChartIncomeLegend"].Title = "Income Performance";
                    ChartIncome.Legends["ChartIncomeLegend"].TitleAlignment = StringAlignment.Center;
                    ChartIncome.Legends["ChartIncomeLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    ChartIncome.Legends["ChartIncomeLegend"].BackColor = Color.FloralWhite;
                    ChartIncome.Legends["ChartIncomeLegend"].TitleSeparatorColor = Color.Black;

                    ChartArea chartArea1 = ChartIncome.ChartAreas[0];

                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderText = "Color";
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartIncome.Legends["ChartIncomeLegend"].CellColumns.Add(colorColumn);
                    chartArea1.BackColor = System.Drawing.Color.Transparent;
                    chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;

                    LegendCellColumn TypeColumn = new LegendCellColumn();
                    TypeColumn.Alignment = ContentAlignment.TopLeft;
                    TypeColumn.Text = "#VALX";
                    TypeColumn.HeaderText = "Type";
                    TypeColumn.Name = "TypeColumn";
                    TypeColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartIncome.Legends["ChartIncomeLegend"].CellColumns.Add(TypeColumn);

                    LegendCellColumn percentColumn = new LegendCellColumn();
                    percentColumn.Alignment = ContentAlignment.MiddleLeft;
                    percentColumn.HeaderText = "%";
                    percentColumn.Text = "#PERCENT";
                    percentColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartIncome.Legends["ChartIncomeLegend"].CellColumns.Add(percentColumn);

                    ChartIncome.Series[0]["PieLabelStyle"] = "Disabled";
                    ChartIncome.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartIncome.ChartAreas[0].Area3DStyle.Perspective = 50;
                    ChartIncome.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartIncome.DataBind();

                    DataRow drIncomeForGrid;
                    DataTable dtIncomeForGrid = new DataTable();
                    dtIncomeForGrid.Columns.Add("IncomeCategory");
                    dtIncomeForGrid.Columns.Add("IncomeAmount",System.Type.GetType("System.Decimal"));

                    dtIncomeForGrid.Columns.Add("Percent",System.Type.GetType("System.Decimal"));
                    double sum = 0;
                    foreach (DataRow dr in dtIncome.Rows)
                    {
                        sum = sum + double.Parse(dr["IncomeAmount"].ToString());
                    }

                    foreach (DataRow dr in dtIncome.Rows)
                    {
                        drIncomeForGrid = dtIncomeForGrid.NewRow();
                        drIncomeForGrid["IncomeCategory"] = dr["IncomeCategory"];
                        drIncomeForGrid["IncomeAmount"] = dr["IncomeAmount"];
                        drIncomeForGrid["Percent"] = Math.Round(((double.Parse(dr["IncomeAmount"].ToString()) / sum) * 100), 2);
                        
                        dtIncomeForGrid.Rows.Add(drIncomeForGrid);
                    }

                    gvrIncome.DataSource = dtIncomeForGrid;
                    gvrIncome.DataBind();
                    tdIncomeError.Visible = true;
                    lblIncomeError.Visible = false;
                }
                else
                {
                    ChartIncome.DataSource = null;
                    ChartIncome.Visible = false;
                    tdIncomeError.Visible = true;
                    tdgvrIncome.Visible = false;
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void BindExpenseGridChart(DataTable dtExpense)
        {
            Series Expense = new Series("Expense");           
           
            try
            {
                Legend legend = new Legend("ChartExpenseLegend");
                legend.Enabled = true;

                if (dtExpense.Rows.Count > 0)
                {
                    /* ***************EXPENSE CHART BINDING START**************** */
                    Expense.ChartType = SeriesChartType.Pie;
                    ChartExpense.DataSource = dtExpense.DefaultView;
                    ChartExpense.Series.Clear();
                    ChartExpense.Series.Add(Expense);
                    ChartExpense.Series[0].XValueMember = "ExpenseCategory";
                    ChartExpense.Series[0].XValueType = ChartValueType.String;
                    ChartExpense.Series[0].YValueMembers = "ExpenseAmount";

                    ChartExpense.Palette = ChartColorPalette.Pastel;
                    ChartExpense.PaletteCustomColors = new Color[]{ Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};

                    ChartExpense.Legends.Add(legend);
                    ChartExpense.Legends["ChartExpenseLegend"].Title = "Expense Performance";
                    ChartExpense.Legends["ChartExpenseLegend"].TitleAlignment = StringAlignment.Center;
                    ChartExpense.Legends["ChartExpenseLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    ChartExpense.Legends["ChartExpenseLegend"].BackColor = Color.FloralWhite;
                    ChartExpense.Legends["ChartExpenseLegend"].TitleSeparatorColor = Color.Black;


                    ChartArea chartArea1 = ChartExpense.ChartAreas[0];
                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderText = "Color";
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartExpense.Legends["ChartExpenseLegend"].CellColumns.Add(colorColumn);
                    chartArea1.BackColor = System.Drawing.Color.Transparent;
                    chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;  
                    LegendCellColumn TypeColumn = new LegendCellColumn();
                    TypeColumn.Alignment = ContentAlignment.TopLeft;
                    TypeColumn.Text = "#VALX";
                    TypeColumn.HeaderText = "Type";
                    TypeColumn.Name = "TypeColumn";
                    TypeColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartExpense.Legends["ChartExpenseLegend"].CellColumns.Add(TypeColumn);

                    LegendCellColumn percentColumn = new LegendCellColumn();
                    percentColumn.Alignment = ContentAlignment.MiddleLeft;
                    percentColumn.HeaderText = "%";
                    percentColumn.Text = "#PERCENT";
                    percentColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartExpense.Legends["ChartExpenseLegend"].CellColumns.Add(percentColumn);
                    
                    ChartExpense.Series[0]["PieLabelStyle"] = "Disabled";                    
                    ChartExpense.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartExpense.ChartAreas[0].Area3DStyle.Perspective = 50;
                    ChartExpense.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartExpense.DataBind();

                    /* ***************EXPENSE CHART BINDING END**************** */

                    /* ***************EXPENSE GRID BINDING START**************** */

                    DataRow drExpenseForGrid;
                    DataTable dtExpenseForGrid = new DataTable();
                    dtExpenseForGrid.Columns.Add("ExpenseCategory");
                    dtExpenseForGrid.Columns.Add("ExpenseAmount", System.Type.GetType("System.Decimal"));
                    dtExpenseForGrid.Columns.Add("Percentage", System.Type.GetType("System.Decimal"));
                    double total = 0;
                    foreach (DataRow dr in dtExpense.Rows)
                    {
                        total = total + double.Parse(dr["ExpenseAmount"].ToString());
                    }

                    foreach (DataRow dr in dtExpense.Rows)
                    {
                        drExpenseForGrid = dtExpenseForGrid.NewRow();
                        drExpenseForGrid["ExpenseCategory"] = dr["ExpenseCategory"];
                        drExpenseForGrid["ExpenseAmount"] = dr["ExpenseAmount"];
                        drExpenseForGrid["Percentage"] = (Math.Round(double.Parse(dr["ExpenseAmount"].ToString()) / total * 100, 2));

                        dtExpenseForGrid.Rows.Add(drExpenseForGrid);
                    }

                    RedGridExpense.DataSource = dtExpenseForGrid;
                    RedGridExpense.DataBind();
                   
                   /* ***************EXPENSE GRID BINDING END**************** */
                    tdExpenseError.Visible = false;
                }
                else
                {
                    ChartExpense.DataSource = null;
                    ChartExpense.Visible = false;
                    tdExpenseError.Visible = true;
                    tdRedGridExpense.Visible = false;
                }               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void BindAssetsGridChart(DataTable dtAsset)
        {
            Series Asset = new Series("Asset");

            try
            {
                Legend legend = new Legend("ChartAssetLegend");
                legend.Enabled = true;

                if (dtAsset.Rows.Count > 0)
                {
                    /* ***************ASSET CHART BINDING START**************** */
                    
                    Asset.ChartType = SeriesChartType.Pie;
                    ChartAsset.DataSource = dtAsset.DefaultView;
                    ChartAsset.Series.Clear();
                    ChartAsset.Series.Add(Asset);
                    ChartAsset.Series[0].XValueMember = "AssetGroupName";
                    ChartAsset.Series[0].XValueType = ChartValueType.String;
                    ChartAsset.Series[0].YValueMembers = "AssetValues";

                    ChartAsset.Palette = ChartColorPalette.Pastel;
                    ChartAsset.PaletteCustomColors = new Color[]{ Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};

                    ChartAsset.Legends.Add(legend);
                    ChartAsset.Legends["ChartAssetLegend"].Title = "Asset Performance";
                    ChartAsset.Legends["ChartAssetLegend"].TitleAlignment = StringAlignment.Center;
                    ChartAsset.Legends["ChartAssetLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    ChartAsset.Legends["ChartAssetLegend"].BackColor = Color.FloralWhite;
                    ChartAsset.Legends["ChartAssetLegend"].TitleSeparatorColor = Color.Black;

                    ChartArea chartArea1 = ChartAsset.ChartAreas[0];

                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderText = "Color";
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartAsset.Legends["ChartAssetLegend"].CellColumns.Add(colorColumn);
                    chartArea1.BackColor = System.Drawing.Color.Transparent;
                    chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;

                    LegendCellColumn TypeColumn = new LegendCellColumn();
                    TypeColumn.Alignment = ContentAlignment.TopLeft;
                    TypeColumn.Text = "#VALX";
                    TypeColumn.HeaderText = "Type";
                    TypeColumn.Name = "TypeColumn";
                    TypeColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartAsset.Legends["ChartAssetLegend"].CellColumns.Add(TypeColumn);

                    LegendCellColumn percentColumn = new LegendCellColumn();
                    percentColumn.Alignment = ContentAlignment.MiddleLeft;
                    percentColumn.HeaderText = "%";
                    percentColumn.Text = "#PERCENT";
                    percentColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartAsset.Legends["ChartAssetLegend"].CellColumns.Add(percentColumn);

                    ChartAsset.Series[0]["PieLabelStyle"] = "Disabled";
                    ChartAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                    ChartAsset.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartAsset.DataBind();

                    tdAssetErrorMsg.Visible = true;
                    lblAssetErrorMsg.Visible = false;

                    /* ***************ASSET CHART BINDING END**************** */

                    /* ***************ASSET GRID BINDING START**************** */

                    DataRow drAssetForGrid;
                    DataTable dtAssetForGrid = new DataTable();
                    dtAssetForGrid.Columns.Add("AssetGroupName");
                    dtAssetForGrid.Columns.Add("AssetValues", System.Type.GetType("System.Decimal"));
                    dtAssetForGrid.Columns.Add("Pctg", System.Type.GetType("System.Decimal"));
                    double sum = 0;
                    foreach (DataRow dr in dtAsset.Rows)
                    {
                        sum = sum + double.Parse(dr["AssetValues"].ToString());
                    }

                    foreach (DataRow dr in dtAsset.Rows)
                    {
                        drAssetForGrid = dtAssetForGrid.NewRow();
                        drAssetForGrid["AssetGroupName"] = dr["AssetGroupName"];
                        drAssetForGrid["AssetValues"] = dr["AssetValues"];
                        drAssetForGrid["Pctg"] = Math.Round(((double.Parse(dr["AssetValues"].ToString()) / sum) * 100), 2);

                        dtAssetForGrid.Rows.Add(drAssetForGrid);
                    }

                    RadGridAsset.DataSource = dtAssetForGrid;
                    RadGridAsset.DataBind();
                }
                else
                {
                    ChartAsset.DataSource = null;
                    ChartAsset.Visible = false;
                    tdAssetErrorMsg.Visible = true;
                    tdRadGridAsset.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            
        }

        public void BindLiabilitiesGridChart(DataTable dtLiabilities)
        {
            Series Liabilities = new Series("Liabilities ");

            try
            {
                Legend legend = new Legend("ChartLiabilitiesLegend");
                legend.Enabled = true;

                if (dtLiabilities.Rows.Count > 0)
                {
                    /* ***************Liabilities CHART BINDING START**************** */

                    Liabilities.ChartType = SeriesChartType.Pie;
                    ChartLiabilities.DataSource = dtLiabilities.DefaultView;
                    ChartLiabilities.Series.Clear();
                    ChartLiabilities.Series.Add(Liabilities);
                    ChartLiabilities.Series[0].XValueMember = "LoanType";
                    ChartLiabilities.Series[0].XValueType = ChartValueType.String;
                    ChartLiabilities.Series[0].YValueMembers = "LoanValues";

                    ChartLiabilities.Palette = ChartColorPalette.Pastel;
                    ChartLiabilities.PaletteCustomColors = new Color[]{ Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                                                                          Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};

                    ChartLiabilities.Legends.Add(legend);
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].Title = "Liabilities Performance";
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].TitleAlignment = StringAlignment.Center;
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].BackColor = Color.FloralWhite;
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].TitleSeparatorColor = Color.Black;

                    ChartArea chartArea1 = ChartLiabilities.ChartAreas[0];

                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderText = "Color";
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].CellColumns.Add(colorColumn);
                    chartArea1.BackColor = System.Drawing.Color.Transparent;
                    chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;

                    LegendCellColumn TypeColumn = new LegendCellColumn();
                    TypeColumn.Alignment = ContentAlignment.TopLeft;
                    TypeColumn.Text = "#VALX";
                    TypeColumn.HeaderText = "Type";
                    TypeColumn.Name = "TypeColumn";
                    TypeColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].CellColumns.Add(TypeColumn);

                    LegendCellColumn percentColumn = new LegendCellColumn();
                    percentColumn.Alignment = ContentAlignment.MiddleLeft;
                    percentColumn.HeaderText = "%";
                    percentColumn.Text = "#PERCENT";
                    percentColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartLiabilities.Legends["ChartLiabilitiesLegend"].CellColumns.Add(percentColumn);

                    ChartLiabilities.Series[0]["PieLabelStyle"] = "Disabled";                    
                    ChartLiabilities.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartLiabilities.ChartAreas[0].Area3DStyle.Perspective = 50;
                    ChartLiabilities.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartLiabilities.DataBind();

                    tdErrorLiabilities.Visible = false;
                    /* ***************Liabilities CHART BINDING END**************** */


                    /* ***************Liabilities GRID BINDING START**************** */
                    DataRow drLiabilitiesForGrid;
                    DataTable dtLiabilitiesForGrid = new DataTable();
                    dtLiabilitiesForGrid.Columns.Add("LoanType");
                    dtLiabilitiesForGrid.Columns.Add("LoanValues", System.Type.GetType("System.Decimal"));
                    dtLiabilitiesForGrid.Columns.Add("Prcentage", System.Type.GetType("System.Decimal"));
                    double sum = 0;
                    foreach (DataRow dr in dtLiabilities.Rows)
                    {
                        sum = sum + double.Parse(dr["LoanValues"].ToString());

                    }

                    foreach (DataRow dr in dtLiabilities.Rows)
                    {
                        drLiabilitiesForGrid = dtLiabilitiesForGrid.NewRow();
                        drLiabilitiesForGrid["LoanType"] = dr["LoanType"];
                        drLiabilitiesForGrid["LoanValues"] = dr["LoanValues"];
                        drLiabilitiesForGrid["Prcentage"] = Math.Round(((double.Parse(dr["LoanValues"].ToString()) / sum) * 100), 2);


                        dtLiabilitiesForGrid.Rows.Add(drLiabilitiesForGrid);
                    }

                    RadGridLiabilities.DataSource = dtLiabilitiesForGrid;
                    RadGridLiabilities.DataBind();
               
                    /* ***************Liabilities GRID BINDING END**************** */
                }
                else
                {
                    ChartLiabilities.DataSource = null;
                    ChartLiabilities.Visible = false;
                    tdErrorLiabilities.Visible = true;
                    tdRadGridLiabilities.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void BindCashChart(DataTable dtCashFlow)
        {
            Series CashFlow = new Series("CashFlow");

            try
            {
                if (dtCashFlow.Rows.Count > 0)
                {
                    // LoadChart 
                    CashFlow.ChartType = SeriesChartType.Column;

                    ChartCashFlow.DataSource = dtCashFlow.DefaultView;
                    ChartCashFlow.Series.Clear();
                    ChartCashFlow.Series.Add(CashFlow);
                    ChartCashFlow.Series[0].XValueMember = "CashCategory";
                    ChartCashFlow.Series[0].XValueType = ChartValueType.String;
                    ChartCashFlow.Series[0].YValueMembers = "Amount";

                    //ChartCashFlow.Palette = ChartColorPalette.Pastel;
                    //ChartCashFlow.PaletteCustomColors = new Color[]{ Color.LimeGreen, Color.Yellow, Color.LightBlue, Color.Purple, Color.Goldenrod, Color.Blue, Color.BurlyWood,
                    //                                                 Color.Chocolate, Color.DeepPink, Color.Plum, Color.Violet, Color.Gainsboro, Color.Tomato, 
                    //                                                 Color.Teal, Color.BlanchedAlmond, Color.Cornsilk};

                    ChartArea chartArea1 = ChartCashFlow.ChartAreas[0];

                    ChartCashFlow.Series["CashFlow"].IsValueShownAsLabel = true;
                    ChartCashFlow.ChartAreas[0].AxisX.Title = "Cash Category";
                    ChartCashFlow.ChartAreas[0].IsSameFontSizeForAllAxes = true;                    

                    ChartCashFlow.ChartAreas[0].AxisX.Interval = 1;
                    ChartCashFlow.ChartAreas[0].AxisY.Title = "Amount";                    
                    ChartCashFlow.ChartAreas[0].Area3DStyle.Enable3D = true;                    
                    CashFlow.Palette = ChartColorPalette.Chocolate;

                    ChartCashFlow.Series[0]["BarLabelStyle"] = "Disabled";
                    ChartCashFlow.Series[0].ToolTip = "#VALX: #VALY";                    
                    ChartCashFlow.DataBind();

                    lblCashFlowError.Visible = false;
                }
                else
                {
                    ChartCashFlow.DataSource = null;
                    ChartCashFlow.Visible = false;
                    lblCashFlowError.Visible = true;
                    //tdgvrIncome.Visible = false;
                    //tdRedGridExpense.Visible = false;
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }


        public void BindGridLifeInsurance(DataTable dtLifeInsurance)
        {
            try
            {
                if (dtLifeInsurance.Rows.Count > 0)
                {
                    DataRow drLifeInsurance;
                    DataTable dtLifeInsuranceForGrid = new DataTable();
                    dtLifeInsuranceForGrid.Columns.Add("InsuranceCategoryName");
                    dtLifeInsuranceForGrid.Columns.Add("InsuranceValues", System.Type.GetType("System.Decimal"));
                    
                    double sum = 0;
                    foreach (DataRow dr in dtLifeInsurance.Rows)
                    {
                        sum = sum + double.Parse(dr["InsuranceValues"].ToString());
                    }

                    foreach (DataRow dr in dtLifeInsurance.Rows)
                    {
                        drLifeInsurance = dtLifeInsuranceForGrid.NewRow();
                        drLifeInsurance["InsuranceCategoryName"] = dr["InsuranceCategoryName"];
                        drLifeInsurance["InsuranceValues"] = dr["InsuranceValues"];

                        dtLifeInsuranceForGrid.Rows.Add(drLifeInsurance);
                    }

                    RadGridLifeInsurance.DataSource = dtLifeInsuranceForGrid;
                    RadGridLifeInsurance.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void BindGridHLVAnalysis(DataTable dtHLVAnalysis)
        {
            try
            {
                if (dtHLVAnalysis.Rows.Count > 0)
                {
                    DataRow drHLVAnalysis;
                    DataTable dtHLVAnalysisForGrid = new DataTable();
                    dtHLVAnalysisForGrid.Columns.Add("HLV_Type");
                    dtHLVAnalysisForGrid.Columns.Add("HLV_Values", System.Type.GetType("System.Decimal"));

                    foreach (DataRow dr in dtHLVAnalysis.Rows)
                    {
                        drHLVAnalysis = dtHLVAnalysisForGrid.NewRow();
                        drHLVAnalysis["HLV_Type"] = dr["HLV_Type"];
                        drHLVAnalysis["HLV_Values"] = dr["HLV_Values"];

                        dtHLVAnalysisForGrid.Rows.Add(drHLVAnalysis);
                    }

                    RadGridHLVAnalysis.DataSource = dtHLVAnalysisForGrid;
                    RadGridHLVAnalysis.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void BindGridInsuranceGapAnalysis(DataTable dtHLVBasedIncome)
        {
            try
            {
                if (dtHLVBasedIncome.Rows.Count > 0)
                {
                    
                    DataTable dtGapAnalysisForGrid = new DataTable();
                    DataRow drGapAnalysis;
                    dtGapAnalysisForGrid.Columns.Add("HLVIncomeType");
                    dtGapAnalysisForGrid.Columns.Add("HLVIncomeValue", System.Type.GetType("System.Decimal"));

                    foreach (DataRow dr in dtHLVBasedIncome.Rows)
                    {
                        drGapAnalysis = dtGapAnalysisForGrid.NewRow();
                        drGapAnalysis["HLVIncomeType"] = dr["HLVIncomeType"];
                        drGapAnalysis["HLVIncomeValue"] = dr["HLVIncomeValue"];

                        dtGapAnalysisForGrid.Rows.Add(drGapAnalysis);
                    }

                    RadGridLIGapAnalysis.DataSource = dtGapAnalysisForGrid;
                    RadGridLIGapAnalysis.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void BindRepFinancialHealth(DataTable dtCustomerFPRatio)
        {

            try
            {
                if (dtCustomerFPRatio.Rows.Count>0)
                {
                    DataTable dtFinancialHealth = new DataTable();
                    DataRow drFinancialHealth;
                    dtFinancialHealth.Columns.Add("RatioName");
                    dtFinancialHealth.Columns.Add("RatioPunchLine");
                    dtFinancialHealth.Columns.Add("RatioValue");
                    dtFinancialHealth.Columns.Add("RatioColorOne");
                    dtFinancialHealth.Columns.Add("RatioRangeOne");
                    dtFinancialHealth.Columns.Add("RatioRangeTwo");
                    dtFinancialHealth.Columns.Add("RatioColorTwo");
                    dtFinancialHealth.Columns.Add("RatioRangeThree");
                    dtFinancialHealth.Columns.Add("RatioColorThree");
                    dtFinancialHealth.Columns.Add("RatioDescription");
                    dtFinancialHealth.Columns.Add("Indicator");

                    string rangeone;
                    string rangeTwo; 
                    string rangeThree;
                    decimal ratioValue;
                    int indicator;
                    int ratioId;
                    foreach (DataRow dr in dtCustomerFPRatio.Rows)
                    {
                        
                        drFinancialHealth = dtFinancialHealth.NewRow();
                        rangeone = dr["RatioRangeOne"].ToString();
                        rangeTwo = dr["RatioRangeTwo"].ToString();
                        rangeThree = dr["RatioRangeThree"].ToString();
                        ratioValue=decimal.Parse(dr["RatioValue"].ToString());
                        ratioId = int.Parse(dr["RatioId"].ToString());
                        indicator = ColorforRepFinancialHealth(rangeone, rangeTwo, rangeThree, ratioValue, ratioId);

                        drFinancialHealth["RatioName"] = dr["RatioName"];
                        drFinancialHealth["RatioPunchLine"] = dr["RatioPunchLine"];
                        drFinancialHealth["RatioValue"] = dr["RatioValue"];
                        string[] ratioAllColor = dr["RatioColor"].ToString().Split('~');
                        drFinancialHealth["RatioColorOne"] =ratioAllColor[0];
                        drFinancialHealth["RatioRangeOne"] = dr["RatioRangeOne"];
                        drFinancialHealth["RatioRangeTwo"] = dr["RatioRangeTwo"];
                        drFinancialHealth["RatioColorTwo"] = ratioAllColor[1];
                        drFinancialHealth["RatioRangeThree"] = dr["RatioRangeThree"];
                        drFinancialHealth["RatioColorThree"] = ratioAllColor[2];
                        drFinancialHealth["RatioDescription"] = dr["RatioDescription"];
                        drFinancialHealth["Indicator"] = indicator.ToString();

                        dtFinancialHealth.Rows.Add(drFinancialHealth);
                    }                 

                    repFinancialHealth.DataSource = dtFinancialHealth;
                    repFinancialHealth.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public int ColorforRepFinancialHealth(string rangeone, string rangeTwo, string rangeThree, decimal value, int ratioId)
        {
            int rangeoneUpper=0;
            int rangeTwoLower=0;
            int rangeTwoUpper=0;
            int rangeThreeLower=0;

            //string strData = rangeone;

            if (ratioId != 5)
            {
                rangeoneUpper = int.Parse(rangeone.Substring(1));
            }
            else
            {
                rangeoneUpper = int.Parse(rangeone.Substring(2));
            }
                string[] ratioMiddleRange = rangeTwo.Split('-');
            rangeTwoLower = int.Parse(ratioMiddleRange[0]);
            rangeTwoUpper = int.Parse(ratioMiddleRange[1]);
            rangeThreeLower = int.Parse(rangeThree.Substring(1));

            if (value < rangeoneUpper)
            {
                return 0;                
            }

            else if (rangeTwoLower <= value && rangeTwoUpper >= value)
            {
                return 1;
            }
            else
            {
                return 2;
            }            
        }

        public void repFinancialHealth_RowDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int indicatorValue;
                System.Web.UI.WebControls.Image imgRedStatus1 = e.Item.FindControl("imgRedStatus1") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image imgRedStatus2 = e.Item.FindControl("imgRedStatus2") as System.Web.UI.WebControls.Image;
                System.Web.UI.WebControls.Image imgRedStatus3 = e.Item.FindControl("imgRedStatus3") as System.Web.UI.WebControls.Image;

                Label lblindicator = e.Item.FindControl("lblIndicator") as Label;

                indicatorValue = int.Parse(lblindicator.Text);

                if (indicatorValue == 0)
                {
                    imgRedStatus1.Visible = true;
                    imgRedStatus2.Visible = false;
                    imgRedStatus3.Visible = false;               
                }
                else if (indicatorValue == 1)
                {
                    imgRedStatus1.Visible = false;
                    imgRedStatus2.Visible = true;
                    imgRedStatus3.Visible = false;                    
                }
                else if (indicatorValue == 2)
                {
                    imgRedStatus1.Visible = false;
                    imgRedStatus2.Visible = false;
                    imgRedStatus3.Visible = true;
                }

            }

        }

        public void BindGridGeneralInsurance(DataTable dtGeneralInsurance)
        {
            try
            {
                if (dtGeneralInsurance.Rows.Count > 0)
                {
                    double existingCover = 0;
                    double recomndCover = 0;
                    double sum = 0;

                    DataTable dtGEHealth = new DataTable();
                    DataRow drGEHealth;
                    dtGEHealth.Columns.Add("GEAssetCategory");
                    dtGEHealth.Columns.Add("GEAssetValues");

                    DataTable dtGEGapAnalysis = new DataTable();
                    DataRow drGEGapAnalysis;
                    dtGEGapAnalysis.Columns.Add("GEAssetCategory");
                    dtGEGapAnalysis.Columns.Add("GEAssetValues");

                    DataTable dtGEOther = new DataTable();
                    DataRow drGEOther;
                    dtGEOther.Columns.Add("GEAssetCategory");
                    dtGEOther.Columns.Add("GEAssetValues", System.Type.GetType("System.Decimal"));

                    drGEGapAnalysis = dtGEGapAnalysis.NewRow();
                    drGEGapAnalysis["GEAssetCategory"] = "Recommended Cover";
                    if (totalAnnualIncome > 50000)
                    {
                        drGEGapAnalysis["GEAssetValues"] = 50000;
                        recomndCover = 50000;
                    }
                    else
                    {
                        drGEGapAnalysis["GEAssetValues"] = totalAnnualIncome;
                        recomndCover = totalAnnualIncome;
                    }
                    dtGEGapAnalysis.Rows.Add(drGEGapAnalysis);

                    foreach (DataRow dr in dtGeneralInsurance.Rows)
                    {
                        if (dr["GEAssetCategory"].ToString().Trim() == "Health/Medical")
                        {
                            drGEHealth = dtGEHealth.NewRow();
                            drGEHealth["GEAssetCategory"] = dr["GEAssetCategory"];
                            drGEHealth["GEAssetValues"] = dr["GEAssetValues"];
                            dtGEHealth.Rows.Add(drGEHealth);
                            existingCover = Math.Round(double.Parse(dr["GEAssetValues"].ToString()), 0);

                            RadGridGEHealth.DataSource = dtGEHealth;
                            RadGridGEHealth.DataBind();
                        }

                        if (dr["GEAssetCategory"].ToString().Trim() != "Health/Medical")
                        {
                            drGEOther = dtGEOther.NewRow();
                            drGEOther["GEAssetCategory"] = dr["GEAssetCategory"];
                            drGEOther["GEAssetValues"] = dr["GEAssetValues"];
                            dtGEOther.Rows.Add(drGEOther);
                            //sum = sum + double.Parse(dr["GEAssetValues"].ToString());
                            RadGridGEOther.DataSource = dtGEOther;
                            RadGridGEOther.DataBind();
                        }
                    }

                    if (existingCover != 0)
                    {
                        drGEGapAnalysis = dtGEGapAnalysis.NewRow();
                        drGEGapAnalysis["GEAssetCategory"] = "Existing Cover";
                        drGEGapAnalysis["GEAssetValues"] = existingCover;
                        dtGEGapAnalysis.Rows.Add(drGEGapAnalysis);
                    }

                    drGEGapAnalysis = dtGEGapAnalysis.NewRow();
                    drGEGapAnalysis["GEAssetCategory"] = "Gap";
                    drGEGapAnalysis["GEAssetValues"] = recomndCover - existingCover;
                    dtGEGapAnalysis.Rows.Add(drGEGapAnalysis);

                    RadGridGEGapAnalysis.DataSource = dtGEGapAnalysis;
                    RadGridGEGapAnalysis.DataBind();

                    trGEInsuranceError.Visible = false;
                }
                else
                {
                    trHealth.Visible = false;
                    trOther.Visible = false;                   
                    trGEInsuranceError.Visible = true;
                    trRadGridGEHealth.Visible = false;
                    trRadGridLIGapAnalysis.Visible = false;
                    trRadGridGEOther.Visible = false;
                    RadGridGEOther.Visible = false;
                    trRadGridGEGapAnalysis.Visible = false;
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        /* **************  Bhoopendra Sahoo code Ends Here ***************** */
    }
}