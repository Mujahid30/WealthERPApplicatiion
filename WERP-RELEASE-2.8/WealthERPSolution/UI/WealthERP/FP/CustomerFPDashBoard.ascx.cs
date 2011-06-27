using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoAdvisorProfiling;
using VoUser;
using WealthERP.Base;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using BoCustomerRiskProfiling;
using BoFPSuperlite;

namespace WealthERP.Customer
{
    public partial class CustomerFPDashBoard : System.Web.UI.UserControl
    {
        RiskProfileBo riskprofilebo = new RiskProfileBo();
        DataSet dsFPAssetsAndLiabilitesDetails = new DataSet();
        DataSet dsFPCurrentAndRecomondedAssets = new DataSet();
        DataTable branchAumDT = new DataTable();
        bool GridViewCultureFlag = true;
        int CustomerId = 0;
        int age = 0;
        string riskCode;

        CustomerProspectBo customerprospectbo = new CustomerProspectBo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        DataRow drChartRecommendedAsset;
        DataTable dtRecommendedAllocation;




        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            CustomerId = customerVo.CustomerId;
            //dsGetRiskProfileRules = riskprofilebo.GetRiskProfileRules();
            //GetRiskCode();
            if(Session["advisorVo"]!=null)
               advisorVo = (AdvisorVo)Session["advisorVo"];

            if (!IsPostBack)
            {
                bindGrid(CustomerId);
                bindChart(CustomerId);
                ShowCurrentAssetAllocationPieChart();
                LoadRecommendedAssetAllocation();
            }
        }


        /**************************** For Customer FP Asstet Grid *************************************/

        protected void bindGrid(int CustomerId)
        {
            DataRow drAssets;
            DataRow drValues;
            dsFPAssetsAndLiabilitesDetails = customerprospectbo.GetFPDashBoardAsstesBreakUp(CustomerId);

            if ((dsFPAssetsAndLiabilitesDetails.Tables[0].Rows.Count > 0) && (!string.IsNullOrEmpty(dsFPAssetsAndLiabilitesDetails.ToString())))
            {
                hrBranchAum.Visible = true;
                ErrorMessage.Visible = false;
                lblBranchAUM.Visible = true;
                branchAumDT.Columns.Add("Asset");
                branchAumDT.Columns.Add("CurrentValue");
                DataTable dtBranchDetails = dsFPAssetsAndLiabilitesDetails.Tables[0];
                drValues = dsFPAssetsAndLiabilitesDetails.Tables[0].Rows[0];

                for (int i = 0; i < dtBranchDetails.Rows.Count; i++)
                {
                    drAssets = branchAumDT.NewRow();
                    drAssets["Asset"] = dtBranchDetails.Rows[i]["AssetType"];
                    drAssets["CurrentValue"] = dtBranchDetails.Rows[i]["Value"];
                    branchAumDT.Rows.Add(drAssets);

                    if (GridViewCultureFlag == true)
                    {
                        double tempCurrValue = 0;
                        double.TryParse(dtBranchDetails.Rows[i]["Value"].ToString(), out tempCurrValue);
                        tempCurrValue = Math.Round(tempCurrValue, 2);
                        drAssets["CurrentValue"] = tempCurrValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    }
                    else
                    {

                        double tempCurrValue = 0;
                        double.TryParse(drValues[i].ToString(), out tempCurrValue);
                        tempCurrValue = Math.Round(tempCurrValue, 2);
                        drAssets["CurrentValue"] = tempCurrValue;

                    }
                }
                gvFPDashBoard.DataSource = branchAumDT;

                gvFPDashBoard.DataBind();
                AssteLiaNetworthTable.Visible = true;
                TotalAssets.Visible = true;
                TotalLiabilities.Visible = true;
                TotalNetworth.Visible = true;

                if (dsFPAssetsAndLiabilitesDetails.Tables[2].Rows[0]["TotalSUM"].ToString() != "")
                    TotalValue.Text = dsFPAssetsAndLiabilitesDetails.Tables[2].Rows[0]["TotalSUM"].ToString();
                else
                    TotalValue.Text = "0.0";

                if (dsFPAssetsAndLiabilitesDetails.Tables[1].Rows[0]["Liabilities"].ToString() != "")
                    TotalLiabilitiesValue.Text = dsFPAssetsAndLiabilitesDetails.Tables[1].Rows[0]["Liabilities"].ToString();
                else
                    TotalLiabilitiesValue.Text = "0.0";

                NetworthValue.Text = (decimal.Parse(TotalValue.Text) - decimal.Parse(TotalLiabilitiesValue.Text)).ToString();


                if (GridViewCultureFlag == true)
                {
                    double tempTotalValue = 0;
                    double.TryParse(drValues[dsFPAssetsAndLiabilitesDetails.Tables[0].Columns.Count - 1].ToString(), out tempTotalValue);
                    tempTotalValue = Math.Round(tempTotalValue, 2);
                }
            }
            else
            {
                AssteLiaNetworthTable.Visible = false;
                TotalAssets.Visible = false;
                TotalLiabilities.Visible = false;
                TotalNetworth.Visible = false;
                hrBranchAum.Visible = false;
                ErrorMessage.Visible = true;
                gvFPDashBoard.DataSource = null;
                gvFPDashBoard.Visible = false;
                lblBranchAUM.Visible = false;
            }
        }

        /**************************** For Customer FP Asstet Chart *************************************/

        protected void bindChart(int CustomerId)
        {
            Legend Branchlegend = null;
            Branchlegend = new Legend("BranchAssetsLegends");
            Branchlegend.Enabled = true;
            string[] XValues = null;
            decimal[] YValues = null;
            DataRow drChAssets;
            DataRow drChvalues;
            Series seriesBranchAssets = null;
            seriesBranchAssets = new Series("seriesBranchAssets");
            double DAssetvalue = 0;
            int j = 0;

            if (dsFPAssetsAndLiabilitesDetails.Tables[0].Rows.Count > 0)
            {
                lblChartBranchAUM.Visible = true;
                hrCustAsset.Visible = true;
                ErrorMessage.Visible = false;
                drChvalues = branchAumDT.Rows[0];
                for (int i = 0; i < branchAumDT.Columns.Count - 1; i++)
                {
                    drChAssets = branchAumDT.NewRow();

                    branchAumDT.Rows.Add(drChAssets);
                    if (DAssetvalue == 0)
                        j = j + 1;
                }
                if (j != branchAumDT.Columns.Count)
                {
                    seriesBranchAssets.ChartType = SeriesChartType.Pie;

                    XValues = new string[10];
                    YValues = new decimal[10];
                    ChartBranchAssets.Series.Clear();
                    ChartBranchAssets.DataSource = branchAumDT;
                    ChartBranchAssets.Series.Clear();
                    ChartBranchAssets.Series.Add(seriesBranchAssets);
                    ChartBranchAssets.Series[0].XValueMember = "Asset";
                    ChartBranchAssets.Series[0].XValueType = ChartValueType.String;
                    ChartBranchAssets.Series[0].YValueMembers = "CurrentValue";
                    ChartBranchAssets.Series["seriesBranchAssets"].IsValueShownAsLabel = true;
                    ChartBranchAssets.ChartAreas[0].AxisX.Title = "Assets";
                    ChartBranchAssets.Series[0].XValueMember = "Asset";
                    ChartBranchAssets.DataManipulator.Sort(PointSortOrder.Descending, "Y", seriesBranchAssets);
                    ChartBranchAssets.Legends.Add(Branchlegend);
                    ChartBranchAssets.Legends["BranchAssetsLegends"].Title = "Assets";
                    ChartBranchAssets.Legends["BranchAssetsLegends"].TitleAlignment = StringAlignment.Center;
                    ChartBranchAssets.Legends["BranchAssetsLegends"].TitleSeparator = LegendSeparatorStyle.None;
                    ChartBranchAssets.Legends["BranchAssetsLegends"].Alignment = StringAlignment.Center;
                    ChartBranchAssets.Legends["BranchAssetsLegends"].TitleSeparator = LegendSeparatorStyle.GradientLine;
                    ChartBranchAssets.Legends["BranchAssetsLegends"].TitleSeparatorColor = Color.Black;
                    //ChartBranchAssets.Legends["BranchAssetsLegends"].AutoFitMinFontSize
                    ChartBranchAssets.Series[0]["PieLabelStyle"] = "Outside";
                    ChartBranchAssets.Series[0]["PieStartAngle"] = "10";
                    ChartArea chartArea1 = ChartBranchAssets.ChartAreas[0];
                    chartArea1.Area3DStyle.IsClustered = true;
                    chartArea1.Area3DStyle.Enable3D = true;
                    chartArea1.Area3DStyle.Perspective = 10;
                    chartArea1.Area3DStyle.PointGapDepth = 900;
                    chartArea1.Area3DStyle.IsRightAngleAxes = false;
                    chartArea1.Area3DStyle.WallWidth = 25;
                    chartArea1.Area3DStyle.Rotation = 65;
                    chartArea1.Area3DStyle.Inclination = 35;
                    chartArea1.BackColor = System.Drawing.Color.Transparent;
                    chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
                    chartArea1.Position.Auto = true;
                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartBranchAssets.Legends["BranchAssetsLegends"].CellColumns.Add(colorColumn);
                    ChartBranchAssets.Legends["BranchAssetsLegends"].BackColor = Color.FloralWhite;
                    LegendCellColumn totalColumn = new LegendCellColumn();
                    totalColumn.Alignment = ContentAlignment.MiddleLeft;

                    totalColumn.Text = "#VALX: #PERCENT";
                    totalColumn.Name = "AssetsColumn";
                    totalColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartBranchAssets.Legends["BranchAssetsLegends"].CellColumns.Add(totalColumn);
                    ChartBranchAssets.Series[0]["PieLabelStyle"] = "Disabled";
                    ChartBranchAssets.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartBranchAssets.ChartAreas[0].AxisX.Interval = 1;
                    ChartBranchAssets.ChartAreas[0].AxisY.Title = "Total Assets";
                    ChartBranchAssets.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartBranchAssets.DataBind();
                }
            }
            else
            {
                lblChartBranchAUM.Visible = false;
                hrCustAsset.Visible = false;
                ErrorMessage.Visible = true;
                ChartBranchAssets.DataSource = null;
                ChartBranchAssets.Visible = false;
            }
        }



        /**************************** For Current Asset Allocation Chart *************************************/

        protected void ShowCurrentAssetAllocationPieChart()
        {
            DataTable dtChartCurrAsset = new DataTable();
            DataRow drChartCurrAsset;
            customerVo = new CustomerVo();
            string CurrEquity = "0";
            string CurrDebt = "0";
            string CurrCash = "0";
            string CurrAlternates = "0";
            if (Session[SessionContents.CustomerVo] != null && Session[SessionContents.CustomerVo].ToString() != "")
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            }

            dsFPCurrentAndRecomondedAssets = customerprospectbo.GetFPCurrentAndRecomondedAssets(CustomerId);
            if (dsFPCurrentAndRecomondedAssets.Tables[0].Rows.Count > 0)
            {
                DataTable dtCurrAssetAllocation = new DataTable();
                dtCurrAssetAllocation = dsFPCurrentAndRecomondedAssets.Tables[0];
                dtChartCurrAsset.Columns.Add("AssetClass");
                dtChartCurrAsset.Columns.Add("CurrentAssetPercentage");

                foreach (DataRow dr in dtCurrAssetAllocation.Rows)
                {
                    drChartCurrAsset = dtChartCurrAsset.NewRow();
                    if (dr["AssetType"].ToString() == "Equity")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["AssetType"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["Percentage"].ToString();
                            CurrEquity = drChartCurrAsset["CurrentAssetPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["AssetType"].ToString() == "Debt")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["AssetType"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["Percentage"].ToString();
                            CurrDebt = drChartCurrAsset["CurrentAssetPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["AssetType"].ToString() == "Cash")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["AssetType"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["Percentage"].ToString();
                            CurrCash = drChartCurrAsset["CurrentAssetPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["AssetType"].ToString() == "Alternates")
                    {
                        if (double.Parse(dr["Percentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["AssetType"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["Percentage"].ToString();
                            CurrAlternates = drChartCurrAsset["CurrentAssetPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                }
                if ((CurrEquity != "0") || (CurrDebt != "0") || (CurrCash != "0") || (CurrAlternates != "0"))
                {
                    /****** For Chart binding *********/
                    Legend ShowCurrentAssetAlllegend = null;
                    ShowCurrentAssetAlllegend = new Legend("ShowCurrentAssetAlllegendLegends");
                    ShowCurrentAssetAlllegend.Enabled = true;

                    Series seriesAssets = new Series("CurrentAsset");
                    seriesAssets.ChartType = SeriesChartType.Pie;
                    ChartCurrentAsset.Visible = true;
                    ChartCurrentAsset.Series.Clear();
                    ChartCurrentAsset.Series.Add(seriesAssets);
                    ChartCurrentAsset.DataSource = dtChartCurrAsset;
                    ChartCurrentAsset.Series[0].XValueMember = "AssetClass";
                    ChartCurrentAsset.Series[0].YValueMembers = "CurrentAssetPercentage";
                    ChartCurrentAsset.Series[0].ToolTip = "#VALX: #PERCENT";

                    ChartCurrentAsset.Legends.Add(ShowCurrentAssetAlllegend);
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].Title = "Assets";
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleAlignment = StringAlignment.Center;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.None;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].Alignment = StringAlignment.Center;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.GradientLine;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].TitleSeparatorColor = Color.Black;

                    /******** Enable X axis margin *********/
                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].CellColumns.Add(colorColumn);
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].BackColor = Color.FloralWhite;
                    LegendCellColumn totalColumn = new LegendCellColumn();
                    totalColumn.Alignment = ContentAlignment.MiddleLeft;

                    totalColumn.Text = "#VALX: #PERCENT";
                    totalColumn.Name = "AssetsColumn";
                    totalColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].CellColumns.Add(totalColumn);
                    ChartCurrentAsset.Series[0]["PieLabelStyle"] = "Disabled";

                    ChartCurrentAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
                    ChartCurrentAsset.BackColor = Color.Transparent;
                    ChartCurrentAsset.ChartAreas[0].BackColor = Color.Transparent;
                    ChartCurrentAsset.ChartAreas[0].BackSecondaryColor = System.Drawing.Color.Transparent;
                    ChartCurrentAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartCurrentAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                    tdCurrentAssetAllocation.Visible = true;
                    ChartCurrentAsset.DataBind();
                }
                else
                {
                    lblCurrChartErrorDisplay.Visible = true;
                    ChartCurrentAsset.Visible = false;
                }
            }
            else
            {
                lblCurrChartErrorDisplay.Visible = true;
                ChartCurrentAsset.Visible = false;
            }
        }


        /**************************** For Recommended Asset Allocation Chart *************************************/

        public void LoadRecommendedAssetAllocation()
        {
            DataTable dtChartRecommendedAsset = new DataTable();
            DataSet dsGetCustomerDOBById = new DataSet();

            customerVo = new CustomerVo();
            string RecommendedEquity = "0";
            string RecommendedDebt = "0";
            string RecommendedCash = "0";
            string RecommendedAlternates = "0";
            dsGetCustomerDOBById = riskprofilebo.GetCustomerDOBById(CustomerId);
            if (Session[SessionContents.CustomerVo] != null && Session[SessionContents.CustomerVo].ToString() != "")
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            }
            if (dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString() != "" && dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString() != null)
            {
                DateTime bday = DateTime.Parse(dsGetCustomerDOBById.Tables[0].Rows[0]["C_DOB"].ToString());
                DateTime now = DateTime.Today;
                age = now.Year - bday.Year;
                if (now < bday.AddYears(age))
                {
                    age--;
                }
                if (dsFPCurrentAndRecomondedAssets.Tables[1].Rows.Count > 0)
                {
                    DataTable dtRecommendedAssetAllocation = new DataTable();
                    dtRecommendedAssetAllocation = dsFPCurrentAndRecomondedAssets.Tables[1];
                    dtChartRecommendedAsset.Columns.Add("AssetClass");
                    dtChartRecommendedAsset.Columns.Add("RecommendedPercentage");
                    foreach (DataRow dr in dtRecommendedAssetAllocation.Rows)
                    {
                        drChartRecommendedAsset = dtChartRecommendedAsset.NewRow();
                        if (dr["AssetType"].ToString() == "Equity")
                        {
                            if (double.Parse(dr["Percentage"].ToString()) > 0)
                            {
                                drChartRecommendedAsset["AssetClass"] = dr["AssetType"].ToString();
                                drChartRecommendedAsset["RecommendedPercentage"] = dr["Percentage"].ToString();
                                RecommendedEquity = drChartRecommendedAsset["RecommendedPercentage"].ToString();
                                dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                            }
                        }
                        if (dr["AssetType"].ToString() == "Debt")
                        {
                            if (double.Parse(dr["Percentage"].ToString()) > 0)
                            {
                                drChartRecommendedAsset["AssetClass"] = dr["AssetType"].ToString();
                                drChartRecommendedAsset["RecommendedPercentage"] = dr["Percentage"].ToString();
                                RecommendedDebt = drChartRecommendedAsset["RecommendedPercentage"].ToString();
                                dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                            }
                        }
                        if (dr["AssetType"].ToString() == "Cash")
                        {
                            if (double.Parse(dr["Percentage"].ToString()) > 0)
                            {
                                drChartRecommendedAsset["AssetClass"] = dr["AssetType"].ToString();
                                drChartRecommendedAsset["RecommendedPercentage"] = dr["Percentage"].ToString();
                                RecommendedCash = drChartRecommendedAsset["RecommendedPercentage"].ToString();
                                dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                            }
                        }
                        if (dr["AssetType"].ToString() == "Alternates")
                        {
                            if (double.Parse(dr["Percentage"].ToString()) > 0)
                            {
                                drChartRecommendedAsset["AssetClass"] = dr["AssetType"].ToString();
                                drChartRecommendedAsset["RecommendedPercentage"] = dr["Percentage"].ToString();
                                RecommendedAlternates = drChartRecommendedAsset["RecommendedPercentage"].ToString();
                                dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                            }
                        }
                    }
                    if ((RecommendedEquity != "0") || (RecommendedDebt != "0") || (RecommendedCash != "0") || (RecommendedAlternates != "0"))
                    {
                        lblCurrChartErrorDisplay.Visible = false;
                        /* For Chart binding */
                        Legend ShowRecomondedAssetAlllegend = null;
                        ShowRecomondedAssetAlllegend = new Legend("ShowRecomondedAssetAlllegendLegends");
                        ShowRecomondedAssetAlllegend.Enabled = true;
                        Series seriesAssets = new Series("RecomondedAsset");
                        seriesAssets.ChartType = SeriesChartType.Pie;
                        ChartRecomonedAsset.Visible = true;
                        ChartRecomonedAsset.Series.Clear();
                        ChartRecomonedAsset.Series.Add(seriesAssets);
                        ChartRecomonedAsset.DataSource = dtChartRecommendedAsset;
                        ChartRecomonedAsset.Series[0].XValueMember = "AssetClass";
                        ChartRecomonedAsset.Series[0].YValueMembers = "RecommendedPercentage";
                        ChartRecomonedAsset.Series[0].ToolTip = "#VALX: #PERCENT";

                        ChartRecomonedAsset.Legends.Add(ShowRecomondedAssetAlllegend);
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Title = "Assets";
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleAlignment = StringAlignment.Center;
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.None;
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Alignment = StringAlignment.Center;
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.GradientLine;
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparatorColor = Color.Black;

                        // Enable X axis margin
                        LegendCellColumn colorColumn = new LegendCellColumn();
                        colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                        colorColumn.HeaderBackColor = Color.WhiteSmoke;
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(colorColumn);
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].BackColor = Color.FloralWhite;
                        LegendCellColumn totalColumn = new LegendCellColumn();
                        totalColumn.Alignment = ContentAlignment.MiddleLeft;

                        totalColumn.Text = "#VALX: #PERCENT";
                        totalColumn.Name = "AssetsColumn";
                        totalColumn.HeaderBackColor = Color.WhiteSmoke;
                        ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(totalColumn);
                        ChartRecomonedAsset.Series[0]["PieLabelStyle"] = "Disabled";

                        ChartRecomonedAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
                        ChartRecomonedAsset.BackColor = Color.Transparent;
                        ChartRecomonedAsset.ChartAreas[0].BackColor = Color.Transparent;
                        ChartRecomonedAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                        ChartRecomonedAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                        tdCurrentAssetAllocation.Visible = true;
                        ChartRecomonedAsset.DataBind();
                    }
                    else
                    {
                        lblChartErrorDisplay.Visible = true;
                        ChartRecomonedAsset.Visible = false;
                    }
                }
                else
                {
                    GetAssetAllocation();
                }
            }
            else
            {
                lblChartErrorDisplay.Visible = true;
                lblChartErrorDisplay.Text = "No Age to display chart. Please Fill Date of Birth in profile!";
                ChartRecomonedAsset.Visible = false;
            }

        }
        public void GetAssetAllocation()
        {
            DataSet dsGetCustomerRiskProfile = riskprofilebo.GetCustomerRiskProfile(CustomerId, advisorVo.advisorId);
            if (dsGetCustomerRiskProfile.Tables[0].Rows.Count > 0)
            {
                riskCode = dsGetCustomerRiskProfile.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();
                DataSet dsGetAssetAllocationRules = riskprofilebo.GetAssetAllocationRules(riskCode, advisorVo.advisorId);
                DataTable dtAsset = new DataTable();
                dtAsset.Columns.Add("AssetType");
                dtAsset.Columns.Add("Percentage");
                dtAsset.Columns.Add("AssetTypeCode");
                DataRow drAsset;
                double equityAdjustment = 0;
                double equitycalc = 0.0;
                double equityPercentage = 0;
                double debtpercentage = 0;
                double cashPercentage = 0;
                if (dsGetAssetAllocationRules != null && dsGetAssetAllocationRules.Tables[0].Rows[0]["A_AdviserId"].ToString() != advisorVo.advisorId.ToString())
                {
                    foreach (DataRow dr in dsGetAssetAllocationRules.Tables[0].Rows)
                    {
                        if (dr["WAC_AssetClassification"].ToString() == "Cash")
                        {
                            cashPercentage = double.Parse(dr["WAAR_AssetAllocationPercenatge"].ToString());
                        }
                        else if (dr["WAC_AssetClassification"].ToString() == "Equity")
                        {
                            equityAdjustment = double.Parse(dr["WAAR_Adjustment"].ToString());
                        }
                    }
                    equitycalc = double.Parse(((100 - double.Parse(age.ToString())) / 100).ToString());
                    equityPercentage = (((100 - cashPercentage) * equitycalc + (equityAdjustment)));
                    debtpercentage = (100 - equityPercentage - cashPercentage);
                    drAsset = dtAsset.NewRow();
                    drAsset[0] = "Equity";
                    drAsset[1] = equityPercentage.ToString();
                    drAsset[2] = 1;
                    dtAsset.Rows.Add(drAsset);
                    drAsset = dtAsset.NewRow();
                    drAsset[0] = "Debt";
                    drAsset[1] = debtpercentage.ToString();
                    drAsset[2] = 2;
                    dtAsset.Rows.Add(drAsset);
                    drAsset = dtAsset.NewRow();
                    drAsset[0] = "Cash";
                    drAsset[1] = cashPercentage.ToString();
                    drAsset[2] = 3;
                    dtAsset.Rows.Add(drAsset);
                }
                else
                {
                    foreach (DataRow dr in dsGetAssetAllocationRules.Tables[0].Rows)
                    {
                        drAsset = dtAsset.NewRow();
                        drAsset[0] = dr["WAC_AssetClassification"].ToString();
                        if (dr["WAAR_AssetAllocationPercenatge"] != null)
                            drAsset[1] = dr["WAAR_AssetAllocationPercenatge"].ToString();
                        else
                            drAsset[1] = 0;
                        drAsset[2] = dr["WAC_AssetClassificationCode"].ToString();
                        dtAsset.Rows.Add(drAsset);
                    }
                }
                dtRecommendedAllocation = dtAsset;

                if ((dtAsset.Rows.Count > 0) && (dtAsset.ToString() != null))
                {
                    lblCurrChartErrorDisplay.Visible = false;
                    /* For Chart binding */
                    Legend ShowRecomondedAssetAlllegend = null;
                    ShowRecomondedAssetAlllegend = new Legend("ShowRecomondedAssetAlllegendLegends");
                    ShowRecomondedAssetAlllegend.Enabled = true;
                    Series seriesAssets = new Series("RecomondedAsset");
                    seriesAssets.ChartType = SeriesChartType.Pie;
                    ChartRecomonedAsset.Visible = true;
                    ChartRecomonedAsset.Series.Clear();
                    ChartRecomonedAsset.Series.Add(seriesAssets);
                    ChartRecomonedAsset.DataSource = dtAsset;
                    ChartRecomonedAsset.Series[0].XValueMember = "AssetType";
                    ChartRecomonedAsset.Series[0].YValueMembers = "Percentage";
                    ChartRecomonedAsset.Series[0].ToolTip = "#VALX: #PERCENT";
                    ChartRecomonedAsset.Legends.Add(ShowRecomondedAssetAlllegend);
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Title = "Assets";
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleAlignment = StringAlignment.Center;
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.None;
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].Alignment = StringAlignment.Center;
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparator = LegendSeparatorStyle.GradientLine;
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].TitleSeparatorColor = Color.Black;

                    // Enable X axis margin
                    LegendCellColumn colorColumn = new LegendCellColumn();
                    colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colorColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(colorColumn);
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].BackColor = Color.FloralWhite;
                    LegendCellColumn totalColumn = new LegendCellColumn();
                    totalColumn.Alignment = ContentAlignment.MiddleLeft;
                    totalColumn.Text = "#VALX: #PERCENT";
                    totalColumn.Name = "AssetsColumn";
                    totalColumn.HeaderBackColor = Color.WhiteSmoke;
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].CellColumns.Add(totalColumn);
                    ChartRecomonedAsset.Series[0]["PieLabelStyle"] = "Disabled";
                    ChartRecomonedAsset.ChartAreas["caActualAsset"].AxisX.IsMarginVisible = true;
                    ChartRecomonedAsset.BackColor = Color.Transparent;
                    ChartRecomonedAsset.ChartAreas[0].BackColor = Color.Transparent;
                    ChartRecomonedAsset.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartRecomonedAsset.ChartAreas[0].Area3DStyle.Perspective = 50;
                    tdCurrentAssetAllocation.Visible = true;
                    ChartRecomonedAsset.DataBind();
                }
                else
                {
                    lblChartErrorDisplay.Visible = true;
                    ChartRecomonedAsset.Visible = false;
                }
            }
            else
            {
                lblChartErrorDisplay.Visible = true;
                ChartRecomonedAsset.Visible = false;
            }
        }
    }
}
       