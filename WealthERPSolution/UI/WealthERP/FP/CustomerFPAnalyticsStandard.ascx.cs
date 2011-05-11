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

namespace WealthERP.FP
{
    public partial class CustomerFPAnalyticsStandard : System.Web.UI.UserControl
    {
        double asset = 0;
        double liabilities = 0;
        double networth = 0;
        double totalAnnualIncome = 0;
        int dynamicRiskClass = 0;
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

        protected void Page_Load(object sender, EventArgs e)
        {

            /* **************  This Code is written by Bhoopendra Sahoo for Standerd Charts and Grids  ***************** */

            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];

            fpSectional.advisorId = advisorVo.advisorId;
            fpSectional.CustomerId = customerVo.CustomerId.ToString();

            dsGetCustomerFPAnalyticsStandard = financialplanningreportsbo.GetCustomerFPDetails(fpSectional, out asset, out liabilities, out networth, out riskClass, out dynamicRiskClass, out totalAnnualIncome);
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
                tdErrorLifeInsurance.Visible = false;
                }
                else
                {
                    tdErrorLifeInsurance.Visible=true;
                }
                
            }

            /* **************  Bhoopendra Sahoo code Ends Here in Page_load ***************** */
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
                    //tdIncomeError.Visible = false;
                }
                else
                {
                    ChartIncome.DataSource = null;
                    ChartIncome.Visible = false;
                    //tdIncomeError.Visible = true;
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
                    //tdExpenseError.Visible = false;
                }
                else
                {
                    ChartExpense.DataSource = null;
                    ChartExpense.Visible = false;
                    //tdExpenseError.Visible = true;
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

                    tdAssetErrorMsg.Visible = false;

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
                    dtFinancialHealth.Columns.Add("RatioColor");
                    dtFinancialHealth.Columns.Add("RatioRangeOne");
                    dtFinancialHealth.Columns.Add("RatioRangeTwo");
                    dtFinancialHealth.Columns.Add("RatioRangeThree");
                    dtFinancialHealth.Columns.Add("RatioDescription");

                    foreach (DataRow dr in dtCustomerFPRatio.Rows)
                    {
                        drFinancialHealth = dtFinancialHealth.NewRow();
                        drFinancialHealth["RatioName"] = dr["RatioName"];
                        drFinancialHealth["RatioPunchLine"] = dr["RatioPunchLine"];
                        drFinancialHealth["RatioValue"] = dr["RatioValue"];
                        drFinancialHealth["RatioColor"] = dr["RatioColor"];
                        drFinancialHealth["RatioRangeOne"] = dr["RatioRangeOne"];
                        drFinancialHealth["RatioRangeTwo"] = dr["RatioRangeTwo"];
                        drFinancialHealth["RatioRangeThree"] = dr["RatioRangeThree"];
                        drFinancialHealth["RatioDescription"] = dr["RatioDescription"];                   

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
                    drGEGapAnalysis["GEAssetCategory"] = "Recommende Cover";
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

                    tdGEInsuranceError.Visible = false;
                }
                else
                {
                    tdHealth.Visible = false;
                    tdOther.Visible = false;
                    tdGEInsuranceError.Visible = true;
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