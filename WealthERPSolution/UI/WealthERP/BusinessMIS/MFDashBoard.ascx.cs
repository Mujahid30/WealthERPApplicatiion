using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using VoUser;
using Telerik.Web.UI;
using BoAdvisorProfiling;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace WealthERP.BusinessMIS
{
    public partial class MFDashBoard : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            BindMfDashBoard();
        }

        private void BindMfDashBoard()
        {
            DataSet dsMFDashBoard = new DataSet();
            dsMFDashBoard=adviserMFMIS.GetMFDashBoard(advisorVo.advisorId);
            /* Bind mf dashboard count grid*/
            if (dsMFDashBoard.Tables[0].Rows.Count > 0)
            {
                gvMFDashboardCount.DataSource = dsMFDashBoard.Tables[0];
                gvMFDashboardCount.DataBind();
                gvMFDashboardCount.Visible = true;
            }
            /* Bind mf dashboard Amount grid*/
            if (dsMFDashBoard.Tables[1].Rows.Count > 0)
            {
                gvMFDashboardAmount.DataSource = dsMFDashBoard.Tables[1];
                gvMFDashboardAmount.DataBind();
                gvMFDashboardAmount.Visible = true;
            }

            /* End*/
            /*  Bind Top 5 Scheme*/
            #region
            if (dsMFDashBoard.Tables[4].Rows.Count > 0)
            {
                    // Total Assets Chart
                    Series seriesAssets = new Series("seriesMFC");
                    Legend legend = new Legend("AssetsLegend");
                    legend.Enabled = true;
                    string[] XValues = new string[dsMFDashBoard.Tables[4].Rows.Count];
                    double[] YValues = new double[dsMFDashBoard.Tables[4].Rows.Count];
                    int i = 0;
                    seriesAssets.ChartType = SeriesChartType.Pie;

                    foreach (DataRow dr in dsMFDashBoard.Tables[4].Rows)
                    {
                        XValues[i] = dr["Scheme"].ToString();
                        YValues[i] = double.Parse(dr["AUM"].ToString());
                        i++;
                    }
                    seriesAssets.Points.DataBindXY(XValues, YValues);
                    //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

                    chrtScheme.Series.Clear();
                    chrtScheme.Series.Add(seriesAssets);

                    //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
                    //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
                    chrtScheme.Legends.Clear();
                    chrtScheme.Legends.Add(legend);
                    chrtScheme.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
                    //chrtScheme.Legends["AssetsLegend"].Title = "Top 5 Scheme";
                    chrtScheme.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
                    chrtScheme.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    chrtScheme.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
                    chrtScheme.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

                    chrtScheme.ChartAreas[0].Area3DStyle.Enable3D = true;
                    chrtScheme.ChartAreas[0].Area3DStyle.Perspective = 50;
                    //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
                    chrtScheme.Width = 500;
                    chrtScheme.BackColor = System.Drawing.Color.Transparent;
                    chrtScheme.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
                    chrtScheme.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

                    LegendCellColumn colors = new LegendCellColumn();
                    colors.HeaderText = "Color";
                    colors.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
                    chrtScheme.Legends["AssetsLegend"].CellColumns.Add(colors);

                    LegendCellColumn asset = new LegendCellColumn();
                    asset.Alignment = ContentAlignment.MiddleLeft;
                    asset.HeaderText = "Scheme";
                    asset.Text = "#VALX";
                    chrtScheme.Legends["AssetsLegend"].CellColumns.Add(asset);

                    LegendCellColumn assetPercent = new LegendCellColumn();
                    assetPercent.Alignment = ContentAlignment.MiddleLeft;
                    assetPercent.HeaderText = "AUM";
                    assetPercent.Text = "#PERCENT";
                    chrtScheme.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

                    foreach (DataPoint point in chrtScheme.Series["seriesMFC"].Points)
                    {
                        point["Exploded"] = "true";
                    }

                    chrtScheme.DataBind();


            }
            else
            {
                chrtScheme.Visible = false;
            }
            #endregion
            /* Bind Top 5 Branches*/
                if (dsMFDashBoard.Tables[2].Rows.Count > 0)
                {
                    // Total Assets Chart
                    Series seriesAssets = new Series("seriesMFC");
                    Legend legend = new Legend("AssetsLegend");
                    legend.Enabled = true;
                    string[] XValues = new string[dsMFDashBoard.Tables[2].Rows.Count];
                    double[] YValues = new double[dsMFDashBoard.Tables[2].Rows.Count];
                    int i = 0;
                    seriesAssets.ChartType = SeriesChartType.Pie;

                    foreach (DataRow dr in dsMFDashBoard.Tables[2].Rows)
                    {
                        XValues[i] = dr["BranchName"].ToString();
                        YValues[i] = double.Parse(dr["AUM"].ToString());
                        i++;
                    }
                    seriesAssets.Points.DataBindXY(XValues, YValues);
                    //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

                    chrtBranch.Series.Clear();
                    chrtBranch.Series.Add(seriesAssets);

                    //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
                    //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
                    chrtBranch.Legends.Clear();
                    chrtBranch.Legends.Add(legend);
                    chrtBranch.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
                    //chrtBranch.Legends["AssetsLegend"].Title = "Top 5 Branches";
                    chrtBranch.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
                    chrtBranch.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    chrtBranch.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
                    chrtBranch.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

                    chrtBranch.ChartAreas[0].Area3DStyle.Enable3D = true;
                    chrtBranch.ChartAreas[0].Area3DStyle.Perspective = 50;
                    //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
                    chrtBranch.Width = 500;
                    chrtBranch.BackColor = System.Drawing.Color.Transparent;
                    chrtBranch.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
                    chrtBranch.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

                    LegendCellColumn colors = new LegendCellColumn();
                    colors.HeaderText = "Color";
                    colors.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
                    chrtBranch.Legends["AssetsLegend"].CellColumns.Add(colors);

                    LegendCellColumn asset = new LegendCellColumn();
                    asset.Alignment = ContentAlignment.MiddleLeft;
                    asset.HeaderText = "Branch";
                    asset.Text = "#VALX";
                    chrtBranch.Legends["AssetsLegend"].CellColumns.Add(asset);

                    LegendCellColumn assetPercent = new LegendCellColumn();
                    assetPercent.Alignment = ContentAlignment.MiddleLeft;
                    assetPercent.HeaderText = "AUM";
                    assetPercent.Text = "#PERCENT";
                    chrtBranch.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

                    foreach (DataPoint point in chrtBranch.Series["seriesMFC"].Points)
                    {
                        point["Exploded"] = "true";
                    }

                    chrtBranch.DataBind();
                    //chrtTotalAssets.Series["Assets"]. 

            }
            else
            {
                chrtBranch.Visible = false;
            }
            /* End*/
            /* Bind Top 5 Customers*/
           
                if (dsMFDashBoard.Tables[3].Rows.Count > 0)
                {
                    // Total Assets Chart
                    Series seriesAssets = new Series("seriesMFC");
                    Legend legend = new Legend("AssetsLegend");
                    legend.Enabled = true;
                    string[] XValues = new string[dsMFDashBoard.Tables[3].Rows.Count];
                    double[] YValues = new double[dsMFDashBoard.Tables[3].Rows.Count];
                    int i = 0;
                    seriesAssets.ChartType = SeriesChartType.Pie;

                    foreach (DataRow dr in dsMFDashBoard.Tables[3].Rows)
                    {
                        XValues[i] = dr["CustomerName"].ToString();
                        YValues[i] = double.Parse(dr["AUM"].ToString());
                        i++;
                    }
                    seriesAssets.Points.DataBindXY(XValues, YValues);
                    //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

                    chrtCustomer.Series.Clear();
                    chrtCustomer.Series.Add(seriesAssets);

                    //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
                    //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
                    chrtCustomer.Legends.Clear();
                    chrtCustomer.Legends.Add(legend);
                    chrtCustomer.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
                    chrtCustomer.Legends["AssetsLegend"].Title = "Top 5 Customers";
                    chrtCustomer.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
                    chrtCustomer.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                    chrtCustomer.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
                    chrtCustomer.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

                    chrtCustomer.ChartAreas[0].Area3DStyle.Enable3D = true;
                    chrtCustomer.ChartAreas[0].Area3DStyle.Perspective = 50;
                    //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
                    chrtCustomer.Width = 500;
                    chrtCustomer.BackColor = System.Drawing.Color.Transparent;
                    chrtCustomer.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
                    chrtCustomer.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

                    LegendCellColumn colors = new LegendCellColumn();
                    colors.HeaderText = "Color";
                    colors.ColumnType = LegendCellColumnType.SeriesSymbol;
                    colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
                    chrtCustomer.Legends["AssetsLegend"].CellColumns.Add(colors);

                    LegendCellColumn asset = new LegendCellColumn();
                    asset.Alignment = ContentAlignment.MiddleLeft;
                    asset.HeaderText = "Name";
                    asset.Text = "#VALX";
                    chrtCustomer.Legends["AssetsLegend"].CellColumns.Add(asset);

                    LegendCellColumn assetPercent = new LegendCellColumn();
                    assetPercent.Alignment = ContentAlignment.MiddleLeft;
                    assetPercent.HeaderText = "AUM";
                    assetPercent.Text = "#PERCENT";
                    chrtCustomer.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

                    foreach (DataPoint point in chrtCustomer.Series["seriesMFC"].Points)
                    {
                        point["Exploded"] = "true";
                    }

                    chrtCustomer.DataBind();

            }
            else
            {
                chrtCustomer.Visible = false;
            }
            /* END*/
            /* Bind Subcategory Grid*/
                if (dsMFDashBoard.Tables[5].Rows.Count > 0)
                {
                    gvSubcategory.DataSource = dsMFDashBoard.Tables[5];
                    gvSubcategory.DataBind();
                }
            /* END*/
        }

    }
}