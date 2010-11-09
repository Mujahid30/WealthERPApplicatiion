using System;
using System.Web.UI.WebControls;
using VoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using BoCommon;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;


namespace WealthERP.UserManagement
{
    public partial class BMDashBoard : System.Web.UI.UserControl
    {
        DataSet branchDetailsDS = new DataSet();
        DataTable branchAumDT = new DataTable();
        DataTable topFiveRMDT = new DataTable();
        //string userType;
        DataTable topFiveCustomerDT = new DataTable();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        RMVo rmVo = new RMVo();
        int rmId;
        bool GridViewCultureFlag = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            UserVo userVo = new UserVo();
            RMVo rmVo = new RMVo();
            //userType = Session["UserType"].ToString().ToLower();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            int branchId;
            userVo = (UserVo)Session["userVo"];
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            rmId = rmVo.RMId;
            
            Session["rmVo"] = rmVo;
            branchId = advisorBranchBo.GetBranchId(rmVo.RMId);
            

           
                advisorBranchVo = advisorBranchBo.GetBranch(branchId);
                Session["advisorBranchVo"] = advisorBranchVo;
           
            if (!IsPostBack)
            {
                BindBranchDropDown();
                bindGrid(0, int.Parse(ddlBMBranch.SelectedValue.ToString()), 1);
                bindChart(0, int.Parse(ddlBMBranch.SelectedValue.ToString()), 1);
            }

            
        }

        protected void BMDashBoardGrid_RowDataBound(object sender, GridViewRowEventArgs row)
        {

        }

        protected void bindChart(int advisorBranchId, int branchHeadId, int all)
        {
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
          
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

            if (branchAumDT.Rows.Count > 0)
            {
                lblChartBranchAUM.Visible = true;
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
                    //seriesBranchAssets1.Palette = ChartColorPalette.Chocolate;
                    //seriesBranchAssets.Palette = ChartColorPalette.Chocolate;

                    XValues = new string[10];
                    YValues = new decimal[10];
                    ChartBranchAssets.Series.Clear();
                    ChartBranchAssets.DataSource = branchAumDT;
                    //branchAumDT.DefaultView.Sort = "CurrentValue";
                    ChartBranchAssets.Series.Clear();
                    ChartBranchAssets.Series.Add(seriesBranchAssets);
                    ChartBranchAssets.Series[0].XValueMember = "Asset";
                    ChartBranchAssets.Series[0].XValueType = ChartValueType.String;
                    ChartBranchAssets.Series[0].YValueMembers = "CurrentValue";
                    ChartBranchAssets.Series["seriesBranchAssets"].IsValueShownAsLabel = true;
                    ChartBranchAssets.ChartAreas[0].AxisX.Title = "Assets";
                    ChartBranchAssets.Series[0].XValueMember = "Asset";
                    ChartBranchAssets.DataManipulator.Sort(PointSortOrder.Descending, "Y" , seriesBranchAssets);
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
            }


            /* For Chart 2 */

            if (topFiveRMDT.Rows.Count > 0)
            {
                lblTop5Rms.Visible = true;
                DataRow drRMCustomersNet;
                DataRow drRMCustomersNetResults;


                Series seriesRMCustNetworth = null;
                seriesRMCustNetworth = new Series("seriesRMCustNetworth");

                Series seriesRMCustNetworth1 = null;
                seriesRMCustNetworth1 = new Series("seriesRMCustNetworth");

                Legend RMCustNetLegend = null;
                Branchlegend = new Legend("legendsTopfiveRM");

                seriesRMCustNetworth = new Series("CustomerNetworth");
                RMCustNetLegend = new Legend("CustomerNetworthLegends");
                RMCustNetLegend.Enabled = true;
                XValues = new string[10];
                YValues = new decimal[10];
                drRMCustomersNet = topFiveRMDT.Rows[0];
                CharttopfiveRMCustNetworth.Series.Clear();
                CharttopfiveRMCustNetworth.Series.Add(seriesRMCustNetworth);
                CharttopfiveRMCustNetworth.DataSource = topFiveRMDT.DefaultView;
                topFiveRMDT.Columns.Add("RM Name");
                for (int i = 0; i < topFiveRMDT.Rows.Count; i++)
                {
                    drRMCustomersNet = topFiveRMDT.NewRow();
                    drRMCustomersNetResults = topFiveRMDT.Rows[i];

                    j = j + 1;
                }
                if (j != topFiveRMDT.Rows.Count)
                {
                    seriesRMCustNetworth.ChartType = SeriesChartType.Bar;
                    //CharttopfiveRMCustNetworth.Titles.Add("Top 5 RMs (Customer Base)");

                    
                    CharttopfiveRMCustNetworth.Series.Clear();
                    CharttopfiveRMCustNetworth.Series.Add(seriesRMCustNetworth);
                    CharttopfiveRMCustNetworth.Series[0].XValueMember = "RmName";
                    CharttopfiveRMCustNetworth.Series[0].XValueType = ChartValueType.String;
                    
                    CharttopfiveRMCustNetworth.Series[0].YValueMembers = "Customer_networth";
            

                    CharttopfiveRMCustNetworth.Series["CustomerNetworth"].IsValueShownAsLabel = true;
                    CharttopfiveRMCustNetworth.ChartAreas[0].AxisX.Title = "RM Name";
                    CharttopfiveRMCustNetworth.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                    CharttopfiveRMCustNetworth.ChartAreas[0].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90;
                    
                    CharttopfiveRMCustNetworth.ChartAreas[0].AxisX.Interval = 1;
                    CharttopfiveRMCustNetworth.ChartAreas[0].AxisY.Title = "Customer NetWorth";
                    CharttopfiveRMCustNetworth.ChartAreas[0].Area3DStyle.Enable3D = true;

                    //CharttopfiveRMCustNetworth.Legends.Add(Branchlegend);
                    //CharttopfiveRMCustNetworth.Legends["legendsTopfiveRM"].Title = "Assets";
                    //CharttopfiveRMCustNetworth.Legends["legendsTopfiveRM"].TitleAlignment = StringAlignment.Center;
                    //CharttopfiveRMCustNetworth.Legends["legendsTopfiveRM"].TitleSeparator = LegendSeparatorStyle.None;
                    //CharttopfiveRMCustNetworth.Legends["legendsTopfiveRM"].Alignment = StringAlignment.Center;
                    //CharttopfiveRMCustNetworth.Legends["legendsTopfiveRM"].TitleSeparatorColor = Color.Black;

                    ChartArea custArea = ChartBranchAssets.ChartAreas[0];
                    custArea.Area3DStyle.Perspective = 10;
                    custArea.Area3DStyle.PointGapDepth = 900;
                    custArea.Area3DStyle.IsRightAngleAxes = false;
                    custArea.Area3DStyle.WallWidth = 25;
                    custArea.Area3DStyle.Rotation = 85;
                    custArea.Area3DStyle.Inclination = 35;

                    seriesRMCustNetworth.Palette = ChartColorPalette.Chocolate;
                    //seriesRMCustNetworth1.Palette = ChartColorPalette.Fire;


                    //seriesRMCustNetworth.Palette = ChartColorPalette.Pastel;
                    //seriesRMCustNetworth1.Palette = ChartColorPalette.Fire;
                    CharttopfiveRMCustNetworth.DataBind();
                }
            }
            else
            {
                lblTop5Rms.Visible = false;
                CharttopfiveRMCustNetworth.DataSource = null;
                CharttopfiveRMCustNetworth.Visible = false;
            }
            /* *********** */


            /* For Chart 3 */

            if (topFiveCustomerDT.Rows.Count > 0)
            {
                chartCustNetworth.Visible = true;
                DataRow drNetworth;
                DataRow drNetworthResult;

                Series seriesCustNetworth = null;
                seriesCustNetworth = new Series("seriesCustNetworth");

                Series seriesCustNetworth1 = null;
                seriesCustNetworth1 = new Series("seriesCustNetworth");

                Legend CustNetLegend = null;
                Branchlegend = new Legend("legendCustomeAsset");

                seriesCustNetworth = new Series("CustomerNetworth");
                CustNetLegend = new Legend("CustomerNetworthLegends");
                CustNetLegend.Enabled = true;
                XValues = new string[10];
                YValues = new decimal[10];
                drNetworth = topFiveCustomerDT.Rows[0];
                ChartCustomerNetworth.Series.Clear();
                ChartCustomerNetworth.Series.Add(seriesCustNetworth);
                ChartCustomerNetworth.DataSource = topFiveCustomerDT.DefaultView;
                topFiveCustomerDT.Columns.Add("Customer Name");
                for (int i = 0; i < topFiveCustomerDT.Rows.Count; i++)
                {
                    drNetworth = topFiveCustomerDT.NewRow();
                    drNetworthResult = topFiveCustomerDT.Rows[i];

                    j = j + 1;
                }
                if (j != topFiveCustomerDT.Rows.Count)
                {
                    seriesCustNetworth.ChartType = SeriesChartType.Bar;

                    ChartCustomerNetworth.Series.Clear();
                    ChartCustomerNetworth.Series.Add(seriesCustNetworth);
                    ChartCustomerNetworth.Series[0].XValueMember = "Customer";
                    ChartCustomerNetworth.Series[0].XValueType = ChartValueType.String;
                    ChartCustomerNetworth.Series[0].YValueMembers = "Networth";
                    
                   

                    ChartCustomerNetworth.Series["CustomerNetworth"].IsValueShownAsLabel = true;
                    ChartCustomerNetworth.ChartAreas[0].AxisX.Title = "Customer Name";

                    ChartCustomerNetworth.ChartAreas[0].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90;
                    ChartCustomerNetworth.ChartAreas[0].AxisX.Interval = 1;
                    ChartCustomerNetworth.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
                    ChartCustomerNetworth.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 5;
                    ChartCustomerNetworth.ChartAreas[0].AxisY.Title = "Customer NetWorth";
                    ChartCustomerNetworth.ChartAreas[0].Area3DStyle.Enable3D = true;

                    ChartArea custArea = ChartBranchAssets.ChartAreas[0];
                    custArea.Area3DStyle.Perspective = 10;
                    custArea.Area3DStyle.PointGapDepth = 900;
                    custArea.Area3DStyle.IsRightAngleAxes = false;
                    custArea.Area3DStyle.WallWidth = 25;
                    custArea.Area3DStyle.Rotation = 65;
                    custArea.Area3DStyle.Inclination = 35;
                    //seriesCustNetworth.Palette = ChartColorPalette.Pastel;
                    seriesCustNetworth.Palette = ChartColorPalette.Chocolate;
                    ChartCustomerNetworth.DataBind();
                }
            }
            else
            {
                chartCustNetworth.Visible = false;
                ChartCustomerNetworth.DataSource = null;
                ChartCustomerNetworth.Visible = false;

            }
            /* *********** */

            }

        protected void bindGrid(int advisorBranchId, int branchHeadId, int all)
        {
            
            //DataSet ds = new DataSet();
            //DataTable BranchAssetsTab = new DataTable();
            DataRow drAssets;
            DataRow drValues;
            branchDetailsDS = advisorBranchBo.GetBranchAssets(advisorBranchId, branchHeadId, all);
            topFiveRMDT = branchDetailsDS.Tables[1];
            topFiveCustomerDT = branchDetailsDS.Tables[2];


            if (branchDetailsDS.Tables[0].Rows.Count > 0)
            {
                hrBranchAum.Visible = true;
                ErrorMessage.Visible = false;
                lblBranchAUM.Visible = true;

                branchAumDT.Columns.Add("Asset");
                branchAumDT.Columns.Add("CurrentValue");
                drValues = branchDetailsDS.Tables[0].Rows[0];
                //DataView view = branchAumDT.DefaultView;
                //view.Sort = "CurrentValue";

                for (int i = 0; i < branchDetailsDS.Tables[0].Columns.Count - 1; i++)
                {
                    drAssets = branchAumDT.NewRow();
                    drAssets["Asset"] = branchDetailsDS.Tables[0].Columns[i].ColumnName;
                    drAssets["CurrentValue"] = drValues[i].ToString();
                    branchAumDT.Rows.Add(drAssets);

                    if (GridViewCultureFlag == true)
                    {
                        double tempCurrValue = 0;
                        double.TryParse(drValues[i].ToString(), out tempCurrValue);
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
                gvBMDashBoardGrid.DataSource = branchAumDT;
                
                //branchAumDT.DefaultView.Sort = "CurrentValue";
                gvBMDashBoardGrid.DataBind();
                gvBMDashBoardGrid.GridLines = GridLines.Both;

                Label TotalText = (Label)gvBMDashBoardGrid.FooterRow.FindControl("lblTotalText");
                Label TotalValue = (Label)gvBMDashBoardGrid.FooterRow.FindControl("lblTotalValue");
                TotalText.Text = branchDetailsDS.Tables[0].Columns[branchDetailsDS.Tables[0].Columns.Count - 1].ColumnName;
                TotalValue.Text = drValues[branchDetailsDS.Tables[0].Columns.Count - 1].ToString();
                if (GridViewCultureFlag == true)
                {
                    double tempTotalValue = 0;
                    double.TryParse(drValues[branchDetailsDS.Tables[0].Columns.Count - 1].ToString(), out tempTotalValue);
                    tempTotalValue = Math.Round(tempTotalValue, 2);
                    TotalValue.Text = tempTotalValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                }
                
            }
            else
            {
                hrBranchAum.Visible = false;
                ErrorMessage.Visible = true;
                gvBMDashBoardGrid.DataSource = null;
                gvBMDashBoardGrid.Visible = false;
                lblBranchAUM.Visible = false;
            }
            /* Top 5 RM Grid */

            //DataRow drRMName;
            //DataRow drRMValue;
            //if (branchDetailsDS.Tables[0].Rows.Count > 0)
            //{
            //    hrTop5Rm.Visible = true;
            //    ErrorMsgForTop5RMs.Visible = false;
            //    lblTop5RM.Visible = true;

                //topFiveRMDT.Columns.Add("Rm Name");
                //topFiveRMDT.Columns.Add("Staff Code");
                //topFiveRMDT.Columns.Add("Customer base");
                //topFiveRMDT.Columns.Add("Customer networth");

                //drRMValue = branchDetailsDS.Tables[1].Rows[0];

                //for (int i = 0; i < branchDetailsDS.Tables[1].Columns.Count; i++)
                //{
                //    drRMName = topFiveRMDT.NewRow();
                //    drRMName["RmName"] = branchDetailsDS.Tables[1].Columns[i].ColumnName;
                //    drRMName["Staff_Code"] = branchDetailsDS.Tables[1].Columns[i].ColumnName;
                //    drRMName["Customer_base"] = branchDetailsDS.Tables[1].Columns[i].ColumnName;
                //    drRMName["Customer_networth"] = drRMValue[i].ToString();

                //    topFiveRMDT.Rows.Add(drRMName);

                //    //if (GridViewCultureFlag == true)
                //    //{
                //    //    decimal tempRMValue = System.Math.Round(decimal.Parse(drRMValue[i].ToString()), 2);
                //    //    drRMName["Customer networth"] = tempRMValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                //    //}
                //    //else
                //    //{
                //    //    drRMName["Customer networth"] = decimal.Parse(drRMValue[i].ToString());
                //    //}
                //}

            if (topFiveRMDT.Rows.Count > 0)
            {
                int i = 0;
                hrTop5Rm.Visible = true;
                ErrorMsgForTop5RMs.Visible = false;
                lblTop5RM.Visible = true;
                
                for (i = 0; i < topFiveRMDT.Rows.Count-1; i++)
                {
                    topFiveRMDT.Rows[i]["Customer_networth"] = topFiveRMDT.Rows[i]["Customer_networth"].ToString();
                }
                //if (GridViewCultureFlag == true)
                //{
                //    decimal tempRMValue = System.Math.Round(decimal.Parse(topFiveRMDT.Rows[i]["Customer_networth"].ToString()), 2);
                //    topFiveRMDT.Rows[i]["Customer_networth"] = tempRMValue.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                   
                //}
                gvRMCustNetworth.DataSource = topFiveRMDT;
                gvRMCustNetworth.DataBind();
            }
            else
            {
                hrTop5Rm.Visible = false;
                ErrorMsgForTop5RMs.Visible = true;
                lblTop5RM.Visible = false;
                gvRMCustNetworth.DataSource = null;
                gvRMCustNetworth.Visible = false;
            }

            if (topFiveCustomerDT.Rows.Count > 0)
            {
                hrTop5Cust.Visible = true;
                ErrorMsgForTop5Customer.Visible = false;
                lblTop5CustNetworth.Visible = true;
                gvCustNetWorth.DataSource = topFiveCustomerDT;
                gvCustNetWorth.DataBind();
            }
            else
            {
                hrTop5Cust.Visible = false;
                ErrorMsgForTop5Customer.Visible = true;
                lblTop5CustNetworth.Visible = false;
                gvCustNetWorth.DataSource = null;
                gvCustNetWorth.Visible = false;
            }


        }
        
        protected void ddlBMBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBMBranch.SelectedIndex == 0)
            {
                bindGrid(0, int.Parse(ddlBMBranch.SelectedValue.ToString()), 1);
                bindChart(0, int.Parse(ddlBMBranch.SelectedValue.ToString()), 1);
            }
            else
            {
                bindGrid(int.Parse(ddlBMBranch.SelectedValue.ToString()), rmId, 0);
                bindChart(int.Parse(ddlBMBranch.SelectedValue.ToString()), rmId, 0);
            }
        }

        /* For Binding the Branch Dropdowns */

        private void BindBranchDropDown()
        {

            try
            {

                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, rmId, 0);
                if (ds != null)
                {
                    ddlBMBranch.DataSource = ds.Tables[1]; ;
                    ddlBMBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBMBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBMBranch.DataBind();
                }
                ddlBMBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", rmId.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "BMDashBoard.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        /* End For Binding the Branch Dropdowns */

       
    }
}





 ////seriesAssets = new Series("CustNetworth");
                //legend = new Legend("CustNetworthLegends");
                //legend.Enabled = true;
                //XValues = new string[10];
                //YValues = new decimal[10];
                //ds = advisorBranchBo.GetBranchAssets(advisorBranchId, branchHeadId, all);
                //seriesAssets.ChartType = SeriesChartType.Pie;
                //seriesAssets.Points.DataBindXY(XValues, YValues);
                //ChartBranchAssets.Series.Clear();
                //ChartBranchAssets.Series.Add(seriesAssets);
                //ChartBranchAssets.DataSource = ds.Tables[0].DefaultView;
                //ChartCustNetworth.DataSource = ds.Tables[1].DefaultView;

                //ChartCustNetworth.Series[0]["CollectedLegendText"] = null;
                //ChartCustNetworth.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
                //ChartCustNetworth.Series[0].Label = null;
                //ChartCustNetworth.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                //ChartCustNetworth.DataSource = ds;
                //ChartCustNetworth.Series[0].XValueMember = "Asset";
                //ChartCustNetworth.Series[0].YValueMembers = "Value";
                //ChartCustNetworth.Legends.Add(legend);
                //ChartCustNetworth.Legends["CustNetworthLegends"].Title = "Assets";
                //ChartCustNetworth.Legends["CustNetworthLegends"].TitleAlignment = StringAlignment.Center;
                //ChartCustNetworth.Legends["CustNetworthLegends"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                //ChartCustNetworth.Legends["CustNetworthLegends"].TitleSeparatorColor = Color.Black;
                //ChartCustNetworth.Series[0]["PieLabelStyle"] = "Outside";
                //ChartCustNetworth.Series[0]["PieStartAngle"] = "10";
                //ChartArea chartArea2 = ChartCustNetworth.ChartAreas[0];
                //chartArea2.Area3DStyle.IsClustered = true;
                //chartArea2.Area3DStyle.Enable3D = true;
                //chartArea2.Area3DStyle.Perspective = 10;
                //chartArea2.Area3DStyle.PointGapDepth = 900;
                //chartArea2.Area3DStyle.IsRightAngleAxes = false;
                //chartArea2.Area3DStyle.WallWidth = 25;
                //chartArea2.Area3DStyle.Rotation = 65;
                //chartArea2.Area3DStyle.Inclination = 35;
                //chartArea2.BackColor = System.Drawing.Color.Transparent;
                //chartArea2.BackSecondaryColor = System.Drawing.Color.Transparent;
                //chartArea2.Position.Auto = true;
                //LegendCellColumn colorColumn1 = new LegendCellColumn();
                //colorColumn1.ColumnType = LegendCellColumnType.SeriesSymbol;
                //colorColumn1.HeaderBackColor = Color.WhiteSmoke;
                //ChartCustNetworth.Legends["CustNetworthLegends"].CellColumns.Add(colorColumn1);
                //LegendCellColumn totalColumn1 = new LegendCellColumn();
                //totalColumn1.Alignment = ContentAlignment.MiddleLeft;
                //totalColumn1.Text = "#VALX: #PERCENT";
                //totalColumn1.Name = "AssetsColumn1";
                //totalColumn1.HeaderBackColor = Color.WhiteSmoke;
                //ChartCustNetworth.Legends["CustNetworthLegends"].CellColumns.Add(totalColumn1);
                //ChartCustNetworth.Series[0]["PieLabelStyle"] = "Disabled";
                //ChartCustNetworth.Series[0].ToolTip = "#VALX: #PERCENT";
                //ChartCustNetworth.DataBind();


//protected void bindChart(int advisorBranchId, int branchHeadId, int all)
//{
//    AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
//    DataSet ds = new DataSet();
//    DataTable BranchAssetTab = new DataTable();
//    string[] XValues = null;
//    decimal[] YValues = null;
//    Series seriesAssets = null;
//    Series seriesCustNetworth = null;
//    Legend NetLegend = null;
//    Legend legend = null;

//    /* Cust Networth Chart */
//    double DNetworth = 0;
//    DataTable CustNetworthTab = new DataTable();
//    DataRow drNetworth;
//    DataRow drNetworthResult;
//    Series seriesCustN = new Series("CustNetworth");
//    int j = 0;
//    int Count = 0;
//    try
//    {
//        seriesAssets = new Series("BranchAssets");
//        legend = new Legend("BranchAssetsLegends");
//        legend.Enabled = true;
//        XValues = new string[10];
//        YValues = new decimal[10];
//        ds = advisorBranchBo.GetBranchAssets(advisorBranchId, branchHeadId, all);
//        seriesAssets.ChartType = SeriesChartType.Pie;
//        seriesAssets.Points.DataBindXY(XValues, YValues);
//        ChartBranchAssets.Series.Clear();
//        ChartBranchAssets.Series.Add(seriesAssets);
//        ChartBranchAssets.DataSource = ds.Tables[0].DefaultView;
//        //ChartCustNetworth.DataSource = ds.Tables[1].DefaultView;

//        ChartBranchAssets.Series[0]["CollectedLegendText"] = null;
//        ChartBranchAssets.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
//        ChartBranchAssets.Series[0].Label = null;
//        ChartBranchAssets.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
//        ChartBranchAssets.DataSource = ds;
//        ChartBranchAssets.Series[0].XValueMember = "Asset";
//        ChartBranchAssets.Series[0].YValueMembers = "Value";
//        ChartBranchAssets.Legends.Add(legend);
//        ChartBranchAssets.Legends["BranchAssetsLegends"].Title = "Assets";
//        ChartBranchAssets.Legends["BranchAssetsLegends"].TitleAlignment = StringAlignment.Center;
//        ChartBranchAssets.Legends["BranchAssetsLegends"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
//        ChartBranchAssets.Legends["BranchAssetsLegends"].TitleSeparatorColor = Color.Black;
//        ChartBranchAssets.Series[0]["PieLabelStyle"] = "Outside";
//        ChartBranchAssets.Series[0]["PieStartAngle"] = "10";
//        ChartArea chartArea1 = ChartBranchAssets.ChartAreas[0];
//        chartArea1.Area3DStyle.IsClustered = true;
//        chartArea1.Area3DStyle.Enable3D = true;
//        chartArea1.Area3DStyle.Perspective = 10;
//        chartArea1.Area3DStyle.PointGapDepth = 900;
//        chartArea1.Area3DStyle.IsRightAngleAxes = false;
//        chartArea1.Area3DStyle.WallWidth = 25;
//        chartArea1.Area3DStyle.Rotation = 65;
//        chartArea1.Area3DStyle.Inclination = 35;
//        chartArea1.BackColor = System.Drawing.Color.Transparent;
//        chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
//        chartArea1.Position.Auto = true;
//        LegendCellColumn colorColumn = new LegendCellColumn();
//        colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
//        colorColumn.HeaderBackColor = Color.WhiteSmoke;
//        ChartBranchAssets.Legends["BranchAssetsLegends"].CellColumns.Add(colorColumn);
//        LegendCellColumn totalColumn = new LegendCellColumn();
//        totalColumn.Alignment = ContentAlignment.MiddleLeft;
//        totalColumn.Text = "#VALX: #PERCENT";
//        totalColumn.Name = "AssetsColumn";
//        totalColumn.HeaderBackColor = Color.WhiteSmoke;
//        ChartBranchAssets.Legends["BranchAssetsLegends"].CellColumns.Add(totalColumn);
//        ChartBranchAssets.Series[0]["PieLabelStyle"] = "Disabled";
//        ChartBranchAssets.Series[0].ToolTip = "#VALX: #PERCENT";
//        ChartBranchAssets.DataBind();


//        /* Chart for Customer Networth */

//        seriesCustNetworth = new Series("CustomerNetworth");
//        NetLegend = new Legend("CustomerNetworthLegends");
//        NetLegend.Enabled = true;
//        XValues = new string[10];
//        YValues = new decimal[10];
//        ds = advisorBranchBo.GetBranchAssets(advisorBranchId, branchHeadId, all);
//       // seriesAssets.ChartType = SeriesChartType.Bar;
//       // seriesAssets.Points.DataBindXY(XValues, YValues);
//        ChartCustomerNetworth.Series.Clear();
//        ChartCustomerNetworth.Series.Add(seriesCustNetworth);
//        ChartCustomerNetworth.DataSource = ds.Tables[1].DefaultView;

//        CustNetworthTab.Columns.Add("Customer Name");
//        CustNetworthTab.Columns.Add("Networth");

//        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
//        {
//            drNetworth = CustNetworthTab.NewRow();
//            drNetworthResult = ds.Tables[1].Rows[i];
//            drNetworth[0] = drNetworthResult["Customer_Name"].ToString();
//            DNetworth = Math.Round(Convert.ToDouble(drNetworthResult["NetWorth"].ToString()), 2);

//            if (DNetworth == 0)
//                j = j + 1;
//            CustNetworthTab.Rows.Add(drNetworth);

//        }
//        ds.Tables.Add(CustNetworthTab);

//        if (j != ds.Tables[1].Rows.Count)
//        {
//            seriesCustNetworth.ChartType = SeriesChartType.Bar;

//            ChartCustomerNetworth.DataSource = ds.Tables[1].DefaultView;
//            ChartCustomerNetworth.Series.Clear();
//            ChartCustomerNetworth.Series.Add(seriesCustNetworth);
//            ChartCustomerNetworth.Series[0].XValueMember = "Customer_Name";
//            ChartCustomerNetworth.Series[0].XValueType = ChartValueType.String;
//            ChartCustomerNetworth.Series[0].YValueMembers = "NetWorth";

//            ChartCustomerNetworth.Series["CustomerNetworth"].IsValueShownAsLabel = true;
//            ChartCustomerNetworth.ChartAreas[0].AxisX.Title = "Customer Name";

//            ChartCustomerNetworth.ChartAreas[0].AxisX.Interval = 1;
//            ChartCustomerNetworth.ChartAreas[0].AxisY.Title = "Customer NetWorth";
//            ChartCustomerNetworth.ChartAreas[0].Area3DStyle.Enable3D = true;

//            ChartArea custArea = ChartBranchAssets.ChartAreas[0];
//            custArea.Area3DStyle.Perspective = 10;
//            custArea.Area3DStyle.PointGapDepth = 900;
//            custArea.Area3DStyle.IsRightAngleAxes = false;
//            custArea.Area3DStyle.WallWidth = 25;
//            custArea.Area3DStyle.Rotation = 65;
//            custArea.Area3DStyle.Inclination = 35;
//            seriesCustNetworth.Palette = ChartColorPalette.Pastel;
//            seriesCustN.Palette = ChartColorPalette.Fire;
//            ChartCustomerNetworth.DataBind();
//        }
//        else
//        {
//            ChartCustomerNetworth.DataSource = null;
//            ChartCustomerNetworth.Visible = false;
//            //lblBranchPerformChart.Visible = false;
//            //GetPageCount();
//        }
//    }
//    catch (BaseApplicationException Ex)
//    {
//        throw Ex;
//    }
//    catch (Exception Ex)
//    {
//        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//        NameValueCollection FunctionInfo = new NameValueCollection();
//        FunctionInfo.Add("Method", "BMDashBoard.ascx:bindChart()");
//        object[] objects = new object[5];
//        objects[0] = advisorBranchId;
//        objects[1] = ds;
//        objects[2] = branchHeadId;
//        objects[3] = all;

//        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//        exBase.AdditionalInformation = FunctionInfo;
//        ExceptionManager.Publish(exBase);
//        throw exBase;
//    }
//}



//    gvBMDashBoardGrid.DataSource = ChBranchAssetTab;
//    gvBMDashBoardGrid.DataBind();


//    ChBranchAssetTab.Columns.Add("Customer Name");
//    ChBranchAssetTab.Columns.Add("Networth");

//            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
//            {
//                drNetworth = CustNetworthTab.NewRow();
//                drNetworthResult = ds.Tables[1].Rows[i];
//                drNetworth[0] = drNetworthResult["Customer_Name"].ToString();
//                DNetworth = Math.Round(Convert.ToDouble(drNetworthResult["NetWorth"].ToString()), 2);

//                if (DNetworth == 0)
//                    j = j + 1;
//                CustNetworthTab.Rows.Add(drNetworth);

//            }
//            ds.Tables.Add(CustNetworthTab);

//            if (j != ds.Tables[1].Rows.Count)
//            {
//                seriesCustNetworth.ChartType = SeriesChartType.Bar;

//                ChartCustomerNetworth.DataSource = ds.Tables[1].DefaultView;
//                ChartCustomerNetworth.Series.Clear();
//                ChartCustomerNetworth.Series.Add(seriesCustNetworth);
//                ChartCustomerNetworth.Series[0].XValueMember = "Customer_Name";
//                ChartCustomerNetworth.Series[0].XValueType = ChartValueType.String;
//                ChartCustomerNetworth.Series[0].YValueMembers = "NetWorth";

//                ChartCustomerNetworth.Series["CustomerNetworth"].IsValueShownAsLabel = true;
//                ChartCustomerNetworth.ChartAreas[0].AxisX.Title = "Customer Name";

//                ChartCustomerNetworth.ChartAreas[0].AxisX.Interval = 1;
//                ChartCustomerNetworth.ChartAreas[0].AxisY.Title = "Customer NetWorth";
//                ChartCustomerNetworth.ChartAreas[0].Area3DStyle.Enable3D = true;

//                ChartArea custArea = ChartBranchAssets.ChartAreas[0];
//                custArea.Area3DStyle.Perspective = 10;
//                custArea.Area3DStyle.PointGapDepth = 900;
//                custArea.Area3DStyle.IsRightAngleAxes = false;
//                custArea.Area3DStyle.WallWidth = 25;
//                custArea.Area3DStyle.Rotation = 65;
//                custArea.Area3DStyle.Inclination = 35;
//                seriesCustNetworth.Palette = ChartColorPalette.Pastel;
//                seriesCustN.Palette = ChartColorPalette.Fire;
//                ChartCustomerNetworth.DataBind();
//            }
//            else
//            {
//                ChartCustomerNetworth.DataSource = null;
//                ChartCustomerNetworth.Visible = false;
//                //lblBranchPerformChart.Visible = false;
//                //GetPageCount();
//            }
//        }

//    try
//    {

//    }
//    catch (BaseApplicationException Ex)
//    {
//        throw Ex;
//    }
//    catch (Exception Ex)
//    {
//        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
//        NameValueCollection FunctionInfo = new NameValueCollection();
//        FunctionInfo.Add("Method", "BMDashBoard.ascx:bindChart()");
//        object[] objects = new object[5];
//        objects[0] = advisorBranchId;
//        objects[1] = ds;
//        objects[2] = branchHeadId;
//        objects[3] = all;

//        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
//        exBase.AdditionalInformation = FunctionInfo;
//        ExceptionManager.Publish(exBase);
//        throw exBase;
//    }
//}
