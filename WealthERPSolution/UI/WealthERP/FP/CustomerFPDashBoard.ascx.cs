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
        DataSet branchDetailsDS = new DataSet();
        DataTable branchAumDT = new DataTable();
        bool GridViewCultureFlag = true;
        int CustomerId = 0;

        CustomerProspectBo customerprospectbo = new CustomerProspectBo();
        CustomerVo customerVo = new CustomerVo();
        AdvisorVo advisorVo = new AdvisorVo();
        
        


        protected void Page_Load(object sender, EventArgs e)
        {
            customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            CustomerId = int.Parse(Session[SessionContents.FPS_ProspectList_CustomerId].ToString());
            //dsGetRiskProfileRules = riskprofilebo.GetRiskProfileRules();
            //GetRiskCode();
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
            branchDetailsDS = customerprospectbo.GetFPDashBoardAsstesBreakUp(CustomerId);

            if ((branchDetailsDS.Tables[0].Rows.Count > 0) && (!string.IsNullOrEmpty(branchDetailsDS.ToString())))
            {
                hrBranchAum.Visible = true;
                ErrorMessage.Visible = false;
                lblBranchAUM.Visible = true;
                branchAumDT.Columns.Add("Asset");
                branchAumDT.Columns.Add("CurrentValue");
                DataTable dtBranchDetails = branchDetailsDS.Tables[0];
                drValues = branchDetailsDS.Tables[0].Rows[0];

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
                if (GridViewCultureFlag == true)
                {
                    double tempTotalValue = 0;
                    double.TryParse(drValues[branchDetailsDS.Tables[0].Columns.Count - 1].ToString(), out tempTotalValue);
                    tempTotalValue = Math.Round(tempTotalValue, 2);
                }
            }
            else
            {
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

            if (branchDetailsDS.Tables[0].Rows.Count > 0)
            {
                lblChartBranchAUM.Visible = true;
                ErrorMessage.Visible = false;
                hrCustAsset.Visible = true;
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
                    ChartBranchAssets.Legends["BranchAssetsLegends"].BackColor = Color.FromName("#F1F9FC");
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
                ErrorMessage.Visible = true;
                ChartBranchAssets.DataSource = null;
                ChartBranchAssets.Visible = false;
                hrCustAsset.Visible = false;
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
            if (Session[SessionContents.CustomerVo] != null && Session[SessionContents.CustomerVo].ToString() != "")
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            }
            if (branchDetailsDS.Tables[2].Rows.Count > 0)
            {
                DataTable dtCurrAssetAllocation = new DataTable();
                dtCurrAssetAllocation = branchDetailsDS.Tables[2];
                dtChartCurrAsset.Columns.Add("AssetClass");
                dtChartCurrAsset.Columns.Add("CurrentAssetPercentage");
                
                foreach (DataRow dr in dtCurrAssetAllocation.Rows)
                {
                    drChartCurrAsset = dtChartCurrAsset.NewRow();
                    if (dr["Class"].ToString() == "Equity")
                    {
                        if (double.Parse(dr["CurrentPercentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["Class"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["CurrentPercentage"].ToString();
                            CurrEquity = drChartCurrAsset["CurrentAssetPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["Class"].ToString() == "Debt")
                    {
                        if (double.Parse(dr["CurrentPercentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["Class"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["CurrentPercentage"].ToString();
                            CurrDebt = drChartCurrAsset["CurrentAssetPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["Class"].ToString() == "Cash")
                    {
                        if (double.Parse(dr["CurrentPercentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["Class"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["CurrentPercentage"].ToString();
                            CurrCash = drChartCurrAsset["CurrentAssetPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                    if (dr["Class"].ToString() == "Alternate")
                    {
                        if (double.Parse(dr["CurrentPercentage"].ToString()) > 0)
                        {
                            drChartCurrAsset["AssetClass"] = dr["Class"].ToString();
                            drChartCurrAsset["CurrentAssetPercentage"] = dr["CurrentPercentage"].ToString();
                            dtChartCurrAsset.Rows.Add(drChartCurrAsset);
                        }
                    }
                }
                if ((CurrEquity != "0") || (CurrDebt != "0") || (CurrCash != "0"))
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
                    ChartCurrentAsset.Legends["ShowCurrentAssetAlllegendLegends"].BackColor = Color.FromName("#F1F9FC");
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
            DataRow drChartRecommendedAsset;
            customerVo = new CustomerVo();
            string RecommendedEquity = "0";
            string RecommendedDebt = "0";
            string RecommendedCash = "0";
            if (Session[SessionContents.CustomerVo] != null && Session[SessionContents.CustomerVo].ToString() != "")
            {
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
            }
            if (branchDetailsDS.Tables[2].Rows.Count > 0)
            {
                DataTable dtRecommendedAssetAllocation = new DataTable();
                dtRecommendedAssetAllocation = branchDetailsDS.Tables[2];
                dtChartRecommendedAsset.Columns.Add("AssetClass");
                dtChartRecommendedAsset.Columns.Add("RecommendedPercentage");
                foreach (DataRow dr in dtRecommendedAssetAllocation.Rows)
                {
                    drChartRecommendedAsset = dtChartRecommendedAsset.NewRow();
                    if (dr["Class"].ToString() == "Equity")
                    {
                        if (double.Parse(dr["RecommendedPercentage"].ToString()) > 0)
                        {
                            drChartRecommendedAsset["AssetClass"] = dr["Class"].ToString();
                            drChartRecommendedAsset["RecommendedPercentage"] = dr["RecommendedPercentage"].ToString();
                            RecommendedEquity = drChartRecommendedAsset["RecommendedPercentage"].ToString();
                            dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                        }
                    }
                    if (dr["Class"].ToString() == "Debt")
                    {
                        if (double.Parse(dr["RecommendedPercentage"].ToString()) > 0)
                        {
                            drChartRecommendedAsset["AssetClass"] = dr["Class"].ToString();
                            drChartRecommendedAsset["RecommendedPercentage"] = dr["RecommendedPercentage"].ToString();
                            RecommendedDebt = drChartRecommendedAsset["RecommendedPercentage"].ToString();
                            dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                        }
                    }
                    if (dr["Class"].ToString() == "Cash")
                    {
                        if (double.Parse(dr["RecommendedPercentage"].ToString()) > 0)
                        {
                            drChartRecommendedAsset["AssetClass"] = dr["Class"].ToString();
                            drChartRecommendedAsset["RecommendedPercentage"] = dr["RecommendedPercentage"].ToString();
                            RecommendedCash = drChartRecommendedAsset["RecommendedPercentage"].ToString();
                            dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                        }
                    }
                    if (dr["Class"].ToString() == "Alternate")
                    {
                        if (double.Parse(dr["RecommendedPercentage"].ToString()) > 0)
                        {
                            drChartRecommendedAsset["AssetClass"] = dr["Class"].ToString();
                            drChartRecommendedAsset["RecommendedPercentage"] = dr["RecommendedPercentage"].ToString();
                            dtChartRecommendedAsset.Rows.Add(drChartRecommendedAsset);
                        }
                    }
                }
                if ((RecommendedEquity != "0") || (RecommendedDebt != "0") || (RecommendedCash != "0"))
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
                    ChartRecomonedAsset.Legends["ShowRecomondedAssetAlllegendLegends"].BackColor = Color.FromName("#F1F9FC");
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
       