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
using BoUploads;
using BOAssociates;
using VOAssociates;
using System.Configuration;

namespace WealthERP.BusinessMIS
{
    public partial class MFDashBoard : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AssociatesVO associatesVo = new AssociatesVO();
        string path = string.Empty;
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        int advisorId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        int all = 0;
        int branchId = 0;
        int branchHeadId = 0;
        int AgentId = 0;
        int IsAssociates;
        string agentCode;
        int isOnline;


        protected void Page_Load(object sender, EventArgs e)
        {
            associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            associatesVo = (AssociatesVO)Session["associatesVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            path = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"].ToString());

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
                userType = "associates";

            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;

            //if (userType == "associates")
            //{
            //    SetParameters();
            //    BindMfDashBoard();
            //}

            if (!IsPostBack)
            {
                if (userType == "advisor" || userType == "rm")
                {
                    BindBranchDropDown();
                    BindRMDropDown();
                    if (userType == "rm")
                    {
                        ddlType.Visible = false;
                        lblType.Visible = false;
                        ddlBranch.Enabled = false;
                        ddlRM.SelectedValue = rmVo.RMId.ToString();
                        ddlRM.Enabled = false;

                    }
                    if (Session["NodeType"] != null)
                    {
                        if (Session["NodeType"] == "MFDashBoard")
                        {
                            SetParameters();
                            BindMfDashBoard();
                            UpnlMFDashBoard.Visible = true;
                        }
                    }
                }
                else if (userType == "rm")
                {

                    //BindBranchDropDown();
                    //BindRMDropDown();
                    //if (userType == "rm")
                    //{
                    //    ddlBranch.Enabled = false;
                    //    ddlRM.SelectedValue = rmVo.RMId.ToString();
                    //    ddlRM.Enabled = false;
                    //}
                }
                if (userType == "bm")
                {
                    //trBranchRM.Visible = true;
                    BindBranchForBMDropDown();
                    BindRMforBranchDropdown(0, bmID);
                    //BindMfDashBoard();
                }
                else if (userType == "associates")
                {
                    SetParameters();
                    BindMfDashBoard();
                    BindBranchDropDown();
                    BindRMDropDown();
                    gvBranch.Visible = true;
                    Label1.Visible = true;
                    lnkBranchNavi.Visible = true;
                    UpnlMFDashBoard.Visible = true;
                    trBranchRM.Visible = false;
                    btnGo.Visible = false;
                    ddlType.Visible=false;
                    lblType.Visible = false;
                }
            }

            // BindMfDashBoard();
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex == 0)
            {
                BindRMforBranchDropdown(0, bmID);
            }
            else
            {
                BindRMforBranchDropdown(int.Parse(ddlBranch.SelectedValue.ToString()), 0);
            }

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            SetParameters();
            BindMfDashBoard();
            UpnlMFDashBoard.Visible = true;
            if (userType == "advisor")
            {
                gvBranch.Visible = true;
                Label1.Visible = true;
                lnkBranchNavi.Visible = true;
            }
            else if (userType == "rm")
            {
                gvBranch.Visible = false;
                Label1.Visible = false;
                lnkBranchNavi.Visible = false;
            }
            else if (userType == "bm")
            {
                gvBranch.Visible = true;
                Label1.Visible = true;
                lnkBranchNavi.Visible = true;
            }
        }

        private void SetParameters()
        {
            if (userType == "advisor")
            {
                IsAssociates = 0;
                isOnline = Convert.ToInt32(ddlType.SelectedValue);
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnAll.Value = "0";
                    hdnbranchId.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnadviserId.Value = advisorVo.advisorId.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }
            else if (userType == "rm")
            {
                IsAssociates = 0;
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";
                isOnline = 0;
            }
            else if (userType == "bm")
            {
                IsAssociates = 0;
                isOnline = 0;
                if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex == 0)
                {

                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnAll.Value = "0";
                    hdnrmId.Value = "0";
                }
                else if ((ddlBranch.SelectedIndex != 0) && (ddlRM.SelectedIndex == 0))
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnAll.Value = "1";
                    hdnrmId.Value = "0";
                }
                else if (ddlBranch.SelectedIndex == 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = "0";
                    hdnAll.Value = "2";
                    hdnrmId.Value = ddlRM.SelectedValue; ;
                }
                else if (ddlBranch.SelectedIndex != 0 && ddlRM.SelectedIndex != 0)
                {
                    hdnbranchHeadId.Value = bmID.ToString();
                    hdnbranchId.Value = ddlBranch.SelectedValue;
                    hdnrmId.Value = ddlRM.SelectedValue;
                    hdnAll.Value = "3";
                }
            }
            else if (userType == "associates")
             {
                 agentCode = associateuserheirarchyVo.AgentCode;
                IsAssociates = 1;
                hdnadviserId.Value = advisorVo.advisorId.ToString() ;
                hdnbranchId.Value ="0";
                hdnrmId.Value ="0";
                hdnAll.Value = "0";
                isOnline = 0;
            }
            if (hdnbranchHeadId.Value == "")
                hdnbranchHeadId.Value = "0";

            if (hdnbranchId.Value == "")
                hdnbranchId.Value = "0";

            if (hdnadviserId.Value == "")
                hdnadviserId.Value = "0";

            if (hdnrmId.Value == "")
                hdnrmId.Value = "0";
        }

        private void BindBranchForBMDropDown()
        {
            try
            {
                DataSet ds = advisorBranchBo.GetBranchsRMForBMDp(0, bmID, 0);
                if (ds != null)
                {
                    ddlBranch.DataSource = ds.Tables[1]; ;
                    ddlBranch.DataValueField = ds.Tables[1].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[1].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserRMMFSystematicMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        private void BindRMforBranchDropdown(int branchId, int branchHeadId)
        {

            try
            {

                DataSet ds = advisorBranchBo.GetAllRMsWithOutBMRole(branchId, branchHeadId);
                if (ds != null)
                {
                    ddlRM.DataSource = ds.Tables[0]; ;
                    ddlRM.DataValueField = ds.Tables[0].Columns["RmID"].ToString();
                    ddlRM.DataTextField = ds.Tables[0].Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserEQMIS.ascx:BindRMforBranchDropdown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindMfDashBoard()
        {

            string preMonth = string.Empty;
            string currMonth = string.Empty;
            int i = 0, j = 0;
            DataSet dsMFDashBoard = new DataSet();

            dsMFDashBoard = adviserMFMIS.GetMFDashBoard(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), out i,IsAssociates,agentCode,isOnline);


            //i = DateTime.Now.Month;
            if (i == 1)
                j = 12;
            else
                j = i - 1;
            /* Bind mf dashboard count grid*/
            if (dsMFDashBoard.Tables[0].Rows.Count > 0)
            {
                gvMFDashboardCount.DataSource = dsMFDashBoard.Tables[0];
                GridBoundColumn gvItemPrev = gvMFDashboardCount.MasterTableView.Columns.FindByUniqueName("PreviousCount") as GridBoundColumn;
                GridBoundColumn gvItemCurr = gvMFDashboardCount.MasterTableView.Columns.FindByUniqueName("CurrentCount") as GridBoundColumn;
                gvItemPrev.HeaderText = preMonth = GetMonthName(j);
                gvItemCurr.HeaderText = currMonth = GetMonthName(i);
                gvMFDashboardCount.DataBind();
                gvMFDashboardCount.Visible = true;
            }
            /* Bind mf dashboard Amount grid*/
            if (dsMFDashBoard.Tables[1].Rows.Count > 0)
            {
                gvMFDashboardAmount.DataSource = dsMFDashBoard.Tables[1];
                GridBoundColumn gvItemPrev = gvMFDashboardAmount.MasterTableView.Columns.FindByUniqueName("CostPrevious") as GridBoundColumn;
                GridBoundColumn gvItemCurr = gvMFDashboardAmount.MasterTableView.Columns.FindByUniqueName("CostCurrent") as GridBoundColumn;
                gvItemPrev.HeaderText = preMonth = GetMonthName(j);
                gvItemCurr.HeaderText = currMonth = GetMonthName(i);
                gvMFDashboardAmount.DataBind();
                gvMFDashboardAmount.Visible = true;
            }
            if (dsMFDashBoard.Tables[6] != null)
            {
                gvAUM.DataSource = dsMFDashBoard.Tables[6];
                GridBoundColumn gvItemPrev = gvAUM.MasterTableView.Columns.FindByUniqueName("CostPrevious") as GridBoundColumn;
                GridBoundColumn gvItemCurr = gvAUM.MasterTableView.Columns.FindByUniqueName("CostCurrent") as GridBoundColumn;
                gvItemPrev.HeaderText = preMonth = GetMonthName(j);
                gvItemCurr.HeaderText = currMonth = GetMonthName(i);
                gvAUM.DataBind();
                gvAUM.Visible = true;
            }

            /* End*/
            /*  Bind Top 5 Scheme*/
            #region
            if (dsMFDashBoard.Tables[4] != null)
            {
                gvScheme.DataSource = dsMFDashBoard.Tables[4];
                gvScheme.DataBind();
                gvScheme.Visible = true;
            }
            //if (dsMFDashBoard.Tables[4].Rows.Count > 0)
            //{
            //        // Total Assets Chart
            //        Series seriesAssets = new Series("seriesMFC");
            //        Legend legend = new Legend("AssetsLegend");
            //        legend.Enabled = true;
            //        string[] XValues = new string[dsMFDashBoard.Tables[4].Rows.Count];
            //        double[] YValues = new double[dsMFDashBoard.Tables[4].Rows.Count];
            //        int i = 0;
            //        seriesAssets.ChartType = SeriesChartType.Pie;

            //        foreach (DataRow dr in dsMFDashBoard.Tables[4].Rows)
            //        {
            //            XValues[i] = dr["Scheme"].ToString();
            //            YValues[i] = double.Parse(dr["AUM"].ToString());
            //            i++;
            //        }
            //        seriesAssets.Points.DataBindXY(XValues, YValues);
            //        //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

            //        chrtScheme.Series.Clear();
            //        chrtScheme.Series.Add(seriesAssets);

            //        //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
            //        //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
            //        chrtScheme.Legends.Clear();
            //        chrtScheme.Legends.Add(legend);
            //        chrtScheme.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
            //        chrtScheme.Legends["AssetsLegend"].Title = "Top 5 Scheme";
            //        chrtScheme.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
            //        chrtScheme.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
            //        chrtScheme.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
            //        chrtScheme.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

            //        chrtScheme.ChartAreas[0].Area3DStyle.Enable3D = true;
            //        chrtScheme.ChartAreas[0].Area3DStyle.Perspective = 50;
            //        //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
            //        chrtScheme.Width = 500;
            //        chrtScheme.BackColor = System.Drawing.Color.Transparent;
            //        chrtScheme.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
            //        chrtScheme.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

            //        LegendCellColumn colors = new LegendCellColumn();
            //        colors.HeaderText = "Color";
            //        colors.ColumnType = LegendCellColumnType.SeriesSymbol;
            //        colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
            //        chrtScheme.Legends["AssetsLegend"].CellColumns.Add(colors);

            //        LegendCellColumn asset = new LegendCellColumn();
            //        asset.Alignment = ContentAlignment.MiddleLeft;
            //        asset.HeaderText = "Scheme";
            //        asset.Text = "#VALX";
            //        chrtScheme.Legends["AssetsLegend"].CellColumns.Add(asset);

            //        LegendCellColumn assetPercent = new LegendCellColumn();
            //        assetPercent.Alignment = ContentAlignment.MiddleLeft;
            //        assetPercent.HeaderText = "AUM";
            //        assetPercent.Text = "#PERCENT";
            //        chrtScheme.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

            //        foreach (DataPoint point in chrtScheme.Series["seriesMFC"].Points)
            //        {
            //            point["Exploded"] = "true";
            //        }

            //        chrtScheme.DataBind();


            //}
            //else
            //{
            //    chrtScheme.Visible = false;
            //}
            #endregion
            /* Bind Top 5 Branches*/
            if (dsMFDashBoard.Tables[2] != null)
            {
                gvBranch.DataSource = dsMFDashBoard.Tables[2];
                gvBranch.DataBind();
                gvBranch.Visible = true;
            }
            //    if (dsMFDashBoard.Tables[2].Rows.Count > 0)
            //    {
            //        // Total Assets Chart
            //        Series seriesAssets = new Series("seriesMFC");
            //        Legend legend = new Legend("AssetsLegend");
            //        legend.Enabled = true;
            //        string[] XValues = new string[dsMFDashBoard.Tables[2].Rows.Count];
            //        double[] YValues = new double[dsMFDashBoard.Tables[2].Rows.Count];
            //        int i = 0;
            //        seriesAssets.ChartType = SeriesChartType.Pie;

            //        foreach (DataRow dr in dsMFDashBoard.Tables[2].Rows)
            //        {
            //            XValues[i] = dr["BranchName"].ToString();
            //            YValues[i] = double.Parse(dr["AUM"].ToString());
            //            i++;
            //        }
            //        seriesAssets.Points.DataBindXY(XValues, YValues);
            //        //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

            //        chrtBranch.Series.Clear();
            //        chrtBranch.Series.Add(seriesAssets);

            //        //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
            //        //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
            //        chrtBranch.Legends.Clear();
            //        chrtBranch.Legends.Add(legend);
            //        chrtBranch.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
            //        chrtBranch.Legends["AssetsLegend"].Title = "Top 5 Branches";
            //        chrtBranch.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
            //        chrtBranch.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
            //        chrtBranch.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
            //        chrtBranch.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

            //        chrtBranch.ChartAreas[0].Area3DStyle.Enable3D = true;
            //        chrtBranch.ChartAreas[0].Area3DStyle.Perspective = 50;
            //        //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
            //        chrtBranch.Width = 500;
            //        chrtBranch.BackColor = System.Drawing.Color.Transparent;
            //        chrtBranch.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
            //        chrtBranch.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

            //        LegendCellColumn colors = new LegendCellColumn();
            //        colors.HeaderText = "Color";
            //        colors.ColumnType = LegendCellColumnType.SeriesSymbol;
            //        colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
            //        chrtBranch.Legends["AssetsLegend"].CellColumns.Add(colors);

            //        LegendCellColumn asset = new LegendCellColumn();
            //        asset.Alignment = ContentAlignment.MiddleLeft;
            //        asset.HeaderText = "Branch";
            //        asset.Text = "#VALX";
            //        chrtBranch.Legends["AssetsLegend"].CellColumns.Add(asset);

            //        LegendCellColumn assetPercent = new LegendCellColumn();
            //        assetPercent.Alignment = ContentAlignment.MiddleLeft;
            //        assetPercent.HeaderText = "AUM";
            //        assetPercent.Text = "#PERCENT";
            //        chrtBranch.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

            //        foreach (DataPoint point in chrtBranch.Series["seriesMFC"].Points)
            //        {
            //            point["Exploded"] = "true";
            //        }

            //        chrtBranch.DataBind();
            //        //chrtTotalAssets.Series["Assets"]. 

            //}
            //else
            //{
            //    chrtBranch.Visible = false;
            //}
            /* End*/
            /* Bind Top 5 Customers*/

            if (dsMFDashBoard.Tables[3] != null)
            {
                gvCustomer.DataSource = dsMFDashBoard.Tables[3];
                gvCustomer.DataBind();
                gvCustomer.Visible = true;
            }

            //    if (dsMFDashBoard.Tables[3].Rows.Count > 0)
            //    {
            //        // Total Assets Chart
            //        Series seriesAssets = new Series("seriesMFC");
            //        Legend legend = new Legend("AssetsLegend");
            //        legend.Enabled = true;
            //        string[] XValues = new string[dsMFDashBoard.Tables[3].Rows.Count];
            //        double[] YValues = new double[dsMFDashBoard.Tables[3].Rows.Count];
            //        int i = 0;
            //        seriesAssets.ChartType = SeriesChartType.Pie;

            //        foreach (DataRow dr in dsMFDashBoard.Tables[3].Rows)
            //        {
            //            XValues[i] = dr["CustomerName"].ToString();
            //            YValues[i] = double.Parse(dr["AUM"].ToString());
            //            i++;
            //        }
            //        seriesAssets.Points.DataBindXY(XValues, YValues);
            //        //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

            //        chrtCustomer.Series.Clear();
            //        chrtCustomer.Series.Add(seriesAssets);

            //        //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
            //        //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
            //        chrtCustomer.Legends.Clear();
            //        chrtCustomer.Legends.Add(legend);
            //        chrtCustomer.Series["seriesMFC"]["CollectedSliceExploded"] = "true";
            //        chrtCustomer.Legends["AssetsLegend"].Title = "Top 5 Customers";
            //        chrtCustomer.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
            //        chrtCustomer.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
            //        chrtCustomer.Legends["AssetsLegend"].TitleSeparatorColor = System.Drawing.Color.Black;
            //        chrtCustomer.Series["seriesMFC"]["PieLabelStyle"] = "Disabled";

            //        chrtCustomer.ChartAreas[0].Area3DStyle.Enable3D = true;
            //        chrtCustomer.ChartAreas[0].Area3DStyle.Perspective = 50;
            //        //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
            //        chrtCustomer.Width = 500;
            //        chrtCustomer.BackColor = System.Drawing.Color.Transparent;
            //        chrtCustomer.ChartAreas[0].BackColor = System.Drawing.Color.Transparent;
            //        chrtCustomer.Series["seriesMFC"].ToolTip = "#VALX: #PERCENT";

            //        LegendCellColumn colors = new LegendCellColumn();
            //        colors.HeaderText = "Color";
            //        colors.ColumnType = LegendCellColumnType.SeriesSymbol;
            //        colors.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
            //        chrtCustomer.Legends["AssetsLegend"].CellColumns.Add(colors);

            //        LegendCellColumn asset = new LegendCellColumn();
            //        asset.Alignment = ContentAlignment.MiddleLeft;
            //        asset.HeaderText = "Name";
            //        asset.Text = "#VALX";
            //        chrtCustomer.Legends["AssetsLegend"].CellColumns.Add(asset);

            //        LegendCellColumn assetPercent = new LegendCellColumn();
            //        assetPercent.Alignment = ContentAlignment.MiddleLeft;
            //        assetPercent.HeaderText = "AUM";
            //        assetPercent.Text = "#PERCENT";
            //        chrtCustomer.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

            //        foreach (DataPoint point in chrtCustomer.Series["seriesMFC"].Points)
            //        {
            //            point["Exploded"] = "true";
            //        }

            //        chrtCustomer.DataBind();

            //}
            //else
            //{
            //    chrtCustomer.Visible = false;
            //}
            /* END*/
            /* Bind Subcategory Grid*/
            if (dsMFDashBoard.Tables[5].Rows.Count > 0)
            {
                gvSubcategory.DataSource = dsMFDashBoard.Tables[5];
                gvSubcategory.DataBind();
            }
            /* END*/

        }

        private string GetMonthName(int i)
        {
            string monthName = string.Empty;
            switch (i)
            {
                case 1:
                    monthName = "Jan";
                    break;
                case 2:
                    monthName = "Feb";
                    break;
                case 3:
                    monthName = "Mar";
                    break;
                case 4:
                    monthName = "Apr";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "Jun";
                    break;
                case 7:
                    monthName = "Jul";
                    break;
                case 8:
                    monthName = "Aug";
                    break;
                case 9:
                    monthName = "Sep";
                    break;
                case 10:
                    monthName = "Oct";
                    break;
                case 11:
                    monthName = "Nov";
                    break;
                case 12:
                    monthName = "Dec";
                    break;

            }
            return monthName;
        }

        protected void lnkSchemeNavi_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MutualFundMIS", "loadcontrol('MutualFundMIS','action=SchemeWise');", true);
        }

        protected void lnkBranchNavi_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "IFAAdminMainDashboardOld", "loadcontrol('IFAAdminMainDashboardOld');", true);
        }

        protected void lnkFolioNavi_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MutualFundMIS", "loadcontrol('MutualFundMIS','action=FolioWise');", true);
        }

        private void BindBranchDropDown()
        {

            RMVo rmVo = new RMVo();
            rmVo = (RMVo)Session[SessionContents.RmVo];
            int bmID = rmVo.RMId;
            try
            {
                UploadCommonBo uploadsCommonDao = new UploadCommonBo();
                DataSet ds = uploadsCommonDao.GetAdviserBranchList(advisorVo.advisorId, "adviser");
                if (ds != null)
                {
                    ddlBranch.DataSource = ds;
                    ddlBranch.DataValueField = ds.Tables[0].Columns["AB_BranchId"].ToString();
                    ddlBranch.DataTextField = ds.Tables[0].Columns["AB_BranchName"].ToString();
                    ddlBranch.DataBind();
                }
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", bmID.ToString()));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindBranchDropDown()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private void BindRMDropDown()
        {
            try
            {
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                DataTable dt = advisorStaffBo.GetAdviserRM(advisorVo.advisorId);
                if (dt.Rows.Count > 0)
                {
                    ddlRM.DataSource = dt;
                    ddlRM.DataValueField = dt.Columns["AR_RMId"].ToString();
                    ddlRM.DataTextField = dt.Columns["RMName"].ToString();
                    ddlRM.DataBind();
                }
                ddlRM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "2"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RMAMCSchemewiseMIS.ascx:BindRMDropDown()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}