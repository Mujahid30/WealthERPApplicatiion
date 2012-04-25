using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using VoUser;
using BoCustomerPortfolio;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Globalization;
using BoCommon;
using BoWerpAdmin;
using WealthERP.Base;



namespace WealthERP.Advisor
{
    public partial class IFAAdminMainDashboard : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        UserVo userVo;
        AssetBo assetBo = new AssetBo();
        decimal eqTotal = 0;
        decimal mfTotal = 0;
        decimal insuranceTotal = 0;
        int customerTotal = 0;
        double total = 0;
        DataSet ds = new DataSet();
        AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
        MessageBo msgBo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                userVo = (UserVo)Session[SessionContents.UserVo];

                if (!IsPostBack)
                {
                    LoadAdminDashBoardDataInCaching();
                    LoadAdminBranchPerformance();
                    LoadBranchPerfomanceChart();
                    LoadRMPerformanceChart();
                    ShowMessageBroadcast();
                    ShowUnreadMessageAlert();
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
                FunctionInfo.Add("Method", "IFAAdminMainDashboard.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        private void ShowUnreadMessageAlert()
        {
            msgBo = new MessageBo();

            // Get unread messages from the DB
            int intCount = 0;
            intCount = msgBo.GetUnreadMessageCount(userVo.UserId);
            
            // Store the messages in a label control
            if (intCount > 0)
            {
                lnkBtnNewMessages.Visible = true;
                lnkBtnNewMessages.Text = "<u>You have " + intCount + " unread messages</u>";
            }
        }

        protected void lnkBtnNewMessages_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('MessageInbox','login');", true);
        }

        /// <summary>
        /// Modified this function to add a new column(No. of customers) to the Branch AUM grid 
        /// </summary>
        /// 

        private void LoadAdminBranchPerformance()
        {
            List<AdvisorBranchVo> branchList = new List<AdvisorBranchVo>();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();

            DataTable dt = new DataTable();
            DataRow drResult = null;
            DataRow dr = null;

            dt = CreateBranchPerformanceTable();
            try
            {
                //Cache.Insert(advisorVo.advisorId.ToString(),

                // ds = assetBo.GetAdviserBranchMF_EQ_In_AggregateCurrentValues(advisorVo.advisorId);
                //Cache.Insert(advisorVo.advisorId.ToString(),
                //lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();

                if (dt.Rows.Count > 0)
                {


                    gvrAdminBranchPerform.DataSource = dt;
                    gvrAdminBranchPerform.DataBind();
                    gvrAdminBranchPerform.Visible = true;
                    //Label lblEquityTotal = (Label)gvrAdminBranchPerform.FooterRow.FindControl("lblEquityTotal");
                    //Label lblMFTotal = (Label)gvrAdminBranchPerform.FooterRow.FindControl("lblMFTotal");
                    //Label lblInsuranceTotal = (Label)gvrAdminBranchPerform.FooterRow.FindControl("lblInsuranceTotal");
                    //Label lblNoOfCustomersTotal = (Label)gvrAdminBranchPerform.FooterRow.FindControl("lblNoOfCustomersTotal");
                    //lblEquityTotal.Text = eqTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", eqTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    //lblMFTotal.Text = mfTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", mfTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    //lblInsuranceTotal.Text = insuranceTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", insuranceTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";
                    //lblNoOfCustomersTotal.Text = customerTotal != 0 ? Math.Round(double.Parse(String.Format("{0:n2}", customerTotal.ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")) : "0";


                    lblGT.Text = Math.Round(double.Parse(String.Format("{0:n2}", ds.Tables[1].Rows[0]["Total"].ToString())), 0).ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                    //GetPageCount();
                }
                else
                {
                    lblGT.Visible = false;
                    lblGTT.Visible = false;

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
                FunctionInfo.Add("Method", "IFAAdminMainDashboard.ascx:LoadAdminBranchPerformance()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = branchList;
                objects[2] = ds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        //protected override void OnInit(EventArgs e)
        //{
        //    try
        //    {

        //        ((Pager)mypager).ItemClicked += new Pager.ItemClickEventHandler(this.HandlePagerEvent);
        //        mypager.EnableViewState = true;
        //        base.OnInit(e);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "IFAAdminMainDashboard.ascx.cs:OnInit()");
        //        object[] objects = new object[0];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        //public void HandlePagerEvent(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        GetPageCount();
        //        this.LoadAdminBranchPerformance();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "IFAAdminMainDashboard.ascx.cs:HandlePagerEvent()");
        //        object[] objects = new object[1];
        //        objects[0] = mypager.CurrentPage;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        //private void GetPageCount()
        //{
        //    string upperlimit = null;
        //    int rowCount = 0;
        //    int ratio = 0;
        //    string lowerlimit = null;
        //    string PageRecords = null;
        //    try
        //    {
        //        rowCount = Convert.ToInt32(hdnRecordCount.Value);
        //        ratio = rowCount / 10;
        //        mypager.PageCount = rowCount % 10 == 0 ? ratio : ratio + 1;
        //        mypager.Set_Page(mypager.CurrentPage, mypager.PageCount);
        //        lowerlimit = (((mypager.CurrentPage - 1) * 10)+1).ToString();
        //        upperlimit = (mypager.CurrentPage * 10).ToString();
        //        if (mypager.CurrentPage == mypager.PageCount)
        //            upperlimit = hdnRecordCount.Value;
        //        PageRecords = String.Format("{0}- {1} of ", lowerlimit, upperlimit);
        //        lblCurrentPage.Text = PageRecords;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "IFAAdminMainDashboard.ascx.cs:GetPageCount()");

        //        object[] objects = new object[5];
        //        objects[0] = upperlimit;
        //        objects[0] = rowCount;
        //        objects[0] = ratio;
        //        objects[0] = lowerlimit;
        //        objects[0] = PageRecords;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }

        //}

        private void LoadBranchPerfomanceChart()
        {
            double tempEq = 0, tempMf = 0, tempIns = 0;
            int j = 0;
            DataSet dsAssetChart = new DataSet();
            Series seriesAssets = new Series("BranchPerformance");
            AssetBo assetsBo = new AssetBo();
            //DataSet ds = null;
            int Count = 0;
            try
            {
                //ds = assetBo.GetAdviserBranchMF_EQ_In_AggregateCurrentValues(advisorVo.advisorId, out Count, 0,out total);
                //lblTotalRows.Text = hdnRecordCount.Value = Count.ToString();


                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataSet branchPerformanceDs = new DataSet();
                    DataRow drResult;
                    DataRow dr;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Branch Name");
                    dt.Columns.Add("Branch Code");
                    dt.Columns.Add("Aggr");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        drResult = ds.Tables[0].Rows[i];
                        if (!string.IsNullOrEmpty(drResult["AB_BranchName"].ToString().Trim()))
                            dr[0] = drResult["AB_BranchName"].ToString();
                        else
                            dr[0] = string.Empty;

                        if (!string.IsNullOrEmpty(drResult["AB_BranchCode"].ToString().Trim()))
                            dr[1] = drResult["AB_BranchCode"].ToString();
                        else
                            dr[1] = string.Empty;
                        tempEq = Math.Round(Convert.ToDouble(drResult["EquityAggr"].ToString()), 2);
                        tempIns = Math.Round(Convert.ToDouble(drResult["MFAggr"].ToString()), 2);
                        tempMf = Math.Round(Convert.ToDouble(drResult["InsuranceAggr"].ToString()), 2);
                        dr[2] = Math.Round((tempEq + tempIns + tempMf), 2).ToString();
                        if (tempEq == 0 && tempIns == 0 && tempMf == 0)
                        {
                            j = j + 1;
                        }
                        if (tempEq + tempIns + tempMf != 0)
                        {
                            dt.Rows.Add(dr);
                        }

                    }
                    branchPerformanceDs.Tables.Add(dt);

                    if (j != ds.Tables[0].Rows.Count)
                    {
                        // LoadChart       


                        seriesAssets.ChartType = SeriesChartType.Bar;

                        ChartBranchPerformance.DataSource = branchPerformanceDs.Tables[0].DefaultView;
                        ChartBranchPerformance.Series.Clear();
                        ChartBranchPerformance.Series.Add(seriesAssets);
                        ChartBranchPerformance.Series[0].XValueMember = "Branch Code";
                        ChartBranchPerformance.Series[0].XValueType = ChartValueType.String;
                        ChartBranchPerformance.Series[0].YValueMembers = "Aggr";

                        ChartBranchPerformance.Series["BranchPerformance"].IsValueShownAsLabel = true;
                        ChartBranchPerformance.ChartAreas[0].AxisX.Title = "BranchCode";

                        ChartBranchPerformance.ChartAreas[0].AxisX.Interval = 1;
                        ChartBranchPerformance.ChartAreas[0].AxisY.Title = "Aggregate Value";
                        //ChartBranchPerformance.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Rotated90;
                        ChartBranchPerformance.ChartAreas[0].Area3DStyle.Enable3D = true;
                        seriesAssets.Palette = ChartColorPalette.Chocolate;
                        ChartBranchPerformance.DataBind();
                    }
                    else
                    {
                        ChartBranchPerformance.DataSource = null;
                        ChartBranchPerformance.Visible = false;
                        lblBranchPerformChart.Visible = false;
                        //GetPageCount();
                    }
                }
                else
                {
                    ChartBranchPerformance.Visible = false;
                    lblBranchPerformChart.Visible = false;
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
                FunctionInfo.Add("Method", "IFAAdminMainDashboard.ascx:LoadBranchPerfomanceChart()");
                object[] objects = new object[5];
                objects[0] = advisorVo;
                objects[1] = ds;
                objects[2] = tempEq;
                objects[3] = tempIns;
                objects[4] = tempMf;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void LoadRMPerformanceChart()
        {
            double tempAggr = 0;
            AssetBo assetsBo = new AssetBo();
            DataSet dsAssetChart = new DataSet();
            //DataSet ds = null;
            try
            {

                Series seriesAssets = new Series("RMPerformance");
                Legend legend = new Legend("RMPerformanceLegend");
                legend.Enabled = true;

                //ds = assetBo.GetAdvisorRM_All_AssetAgr(advisorVo.advisorId);
                if (Cache[advisorVo.advisorId.ToString()] == null)
                {
                    LoadAdminDashBoardDataInCaching();
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    string[] XValues = new string[ds.Tables[2].Rows.Count];
                    double[] YValues = new double[ds.Tables[2].Rows.Count];
                    DataSet RMPerformanceDs = new DataSet();
                    int j = 0;
                    DataRow drResult;
                    DataRow dr;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("RMName");
                    dt.Columns.Add("AggregateValue");
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        drResult = ds.Tables[2].Rows[i];
                        dr[0] = drResult["AR_FirstName"].ToString() + drResult["AR_LastName"].ToString();
                        tempAggr = Math.Round(Convert.ToDouble(drResult["result"].ToString()), 2);
                        if (tempAggr == 0)
                            j = j + 1;
                        dr[1] = tempAggr.ToString();
                        dt.Rows.Add(dr);

                    }
                    RMPerformanceDs.Tables.Add(dt);

                    if (j != ds.Tables[2].Rows.Count)
                    {

                        // LoadChart            

                        seriesAssets.ChartType = SeriesChartType.Pie;
                        ChartRMPerformance.DataSource = RMPerformanceDs.Tables[0].DefaultView;


                        Series series1 = ChartRMPerformance.Series[0];
                        ChartRMPerformance.Series.Clear();
                        ChartRMPerformance.Series.Add(seriesAssets);
                        //ChartRMPerformance.Series[0]["CollectedThreshold"] = "5";
                        ChartRMPerformance.Series[0]["CollectedLegendText"] = "Other";
                        ChartRMPerformance.Series[0].XValueMember = "RMName";
                        ChartRMPerformance.Series[0].YValueMembers = "AggregateValue";
                        ChartRMPerformance.Legends.Add(legend);
                        ChartRMPerformance.Legends["RMPerformanceLegend"].Title = "RM Performance";
                        ChartRMPerformance.Legends["RMPerformanceLegend"].TitleAlignment = StringAlignment.Center;
                        ChartRMPerformance.Legends["RMPerformanceLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                        ChartRMPerformance.Legends["RMPerformanceLegend"].BackColor = Color.FloralWhite;
                        ChartRMPerformance.Legends["RMPerformanceLegend"].TitleSeparatorColor = Color.Black;


                        ChartArea chartArea1 = ChartRMPerformance.ChartAreas[0];
                        LegendCellColumn colorColumn = new LegendCellColumn();
                        colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                        colorColumn.HeaderText = "Color";
                        colorColumn.HeaderBackColor = Color.WhiteSmoke;
                        ChartRMPerformance.Legends["RMPerformanceLegend"].CellColumns.Add(colorColumn);
                        chartArea1.BackColor = System.Drawing.Color.Transparent;
                        chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
                        //ChartRMPerformance.BackSecondaryColor = System.Drawing.Color.Transparent;

                        //LegendCellColumn RMNameColumn = new LegendCellColumn();
                        //RMNameColumn.ColumnType = LegendCellColumnType.Text;
                        //RMNameColumn.HeaderText = "Name";
                        //RMNameColumn.Text = "#RMName";
                        //RMNameColumn.HeaderBackColor = Color.WhiteSmoke;
                        //ChartRMPerformance.Legends["RMPerformanceLegend"].CellColumns.Add(RMNameColumn);


                        LegendCellColumn totalColumn = new LegendCellColumn();
                        totalColumn.Alignment = ContentAlignment.MiddleLeft;
                        totalColumn.Text = "#VALX: #PERCENT";
                        totalColumn.HeaderText = "Performance";
                        totalColumn.Name = "PerformanceColumn";
                        totalColumn.HeaderBackColor = Color.WhiteSmoke;
                        ChartRMPerformance.Legends["RMPerformanceLegend"].CellColumns.Add(totalColumn);



                        ChartRMPerformance.Series[0]["PieLabelStyle"] = "Disabled";


                        ChartRMPerformance.Series[0].ToolTip = "#VALX: #VALY";
                        ChartRMPerformance.ChartAreas[0].Area3DStyle.Enable3D = true;
                        ChartRMPerformance.DataBind();
                    }
                    else
                    {
                        ChartRMPerformance.DataSource = null;
                        ChartRMPerformance.Visible = false;
                        lblRMPerformChart.Visible = false;

                    }
                }
                else
                {
                    ChartRMPerformance.DataSource = null;
                    ChartRMPerformance.Visible = false;
                    lblRMPerformChart.Visible = false;
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
                FunctionInfo.Add("Method", "IFAAdminMainDashboard.ascx:LoadRMPerformanceChart()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = ds;
                objects[2] = tempAggr;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void gvrAdminBranchPerform_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    eqTotal = eqTotal + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Equity"));
            //    mfTotal = mfTotal + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MF"));
            //    insuranceTotal = insuranceTotal + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Insurance"));
            //}
            //else 
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total ";
                e.Row.Cells[4].Text = insuranceTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[4].Attributes.Add("align", "Right");
                e.Row.Cells[3].Text = mfTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[3].Attributes.Add("align", "Right");
                e.Row.Cells[2].Text = eqTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                e.Row.Cells[2].Attributes.Add("align", "Right");
                e.Row.Cells[5].Text = customerTotal.ToString();
                e.Row.Cells[5].Attributes.Add("align", "Right");

            }
            lblGT.Text = total.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
        }

        protected void LoadAdminDashBoardDataInCaching()
        {
            double totalEquity = 0;
            double totalMF = 0;
            //Cache.Remove(advisorVo.advisorId.ToString());
            if (Cache[advisorVo.advisorId.ToString()] == null)
            {
                ds = assetBo.GetAdviserBranchMF_EQ_In_AggregateCurrentValues(advisorVo.advisorId);

                totalEquity = double.Parse(ds.Tables[0].Compute("SUM(EquityAggr)", String.Empty).ToString());

                totalMF = double.Parse(ds.Tables[0].Compute("SUM(MFAggr)", String.Empty).ToString());
                if ((totalEquity != 0 && totalMF == 0) || (totalEquity == 0 && totalMF != 0) || (totalEquity != 0 && totalMF != 0))
                    Cache.Insert(advisorVo.advisorId.ToString(), ds, null, DateTime.Now.AddMinutes(10 * 60), TimeSpan.Zero);
            }
            else
            {
                ds = (DataSet)Cache[advisorVo.advisorId.ToString()];

            }


        }

        public DataTable CreateBranchPerformanceTable()
        {
            DataTable dtBMPerformance = new DataTable();
            DataRow drResult = null;
            DataRow dr = null;


            dtBMPerformance.Columns.Add("Branch Id");
            dtBMPerformance.Columns.Add("Branch Name");
            dtBMPerformance.Columns.Add("Branch Code");
            //dt.Columns.Add("Equity");
            dtBMPerformance.Columns.Add("Equity", typeof(Decimal));
            dtBMPerformance.Columns.Add("MF", typeof(Decimal));
            dtBMPerformance.Columns.Add("Insurance", typeof(Decimal));
            dtBMPerformance.Columns.Add("NoOfCustomers", typeof(Int32));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGT.Visible = true;
                lblGTT.Visible = true;

                //dt.Columns.Add(new DataColumn("Equity", typeof(decimal)));
                //dt.Columns.Add(new DataColumn("MF", typeof(decimal)));
                //dt.Columns.Add(new DataColumn("Insurance", typeof(decimal)));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dr = dtBMPerformance.NewRow();
                    drResult = ds.Tables[0].Rows[i];
                    dr[0] = drResult["A_AdviserId"].ToString();
                    dr[1] = drResult["AB_BranchName"].ToString();
                    dr[2] = drResult["AB_BranchCode"].ToString();
                    if (Convert.ToDecimal(drResult["EquityAggr"].ToString()) == 0)
                        dr[3] = "0";
                    else
                    {
                        dr[3] = decimal.Parse(drResult["EquityAggr"].ToString());

                    }
                    eqTotal = eqTotal + Convert.ToDecimal(drResult["EquityAggr"].ToString());

                    if (Convert.ToDecimal(drResult["MFAggr"].ToString()) == 0)
                        dr[4] = "0";
                    else
                        dr[4] = decimal.Parse(drResult["MFAggr"].ToString());
                    mfTotal = mfTotal + Convert.ToDecimal(drResult["MFAggr"].ToString());

                    if (Convert.ToDecimal(drResult["InsuranceAggr"].ToString()) == 0)
                        dr[5] = "0";
                    else
                        dr[5] = decimal.Parse(drResult["InsuranceAggr"].ToString());
                    insuranceTotal = insuranceTotal + Convert.ToDecimal(drResult["InsuranceAggr"].ToString());


                    dr[6] = drResult["NoOfCustomers"].ToString();
                    customerTotal = customerTotal + Int32.Parse(drResult["NoOfCustomers"].ToString());

                    dtBMPerformance.Rows.Add(dr);
                }
            }

            return dtBMPerformance;

        }

        protected void ShowMessageBroadcast()
        {
            DataSet dsMessage = advisermaintanencebo.GetMessageBroadcast(advisorVo.advisorId);
            if (dsMessage != null && dsMessage.Tables[0].Rows.Count > 0)
            {

                //MessageReceived.Visible = true;
                if (dsMessage.Tables[0].Rows[0]["ABM_IsActive"].ToString() == "1" && dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessage"].ToString() != "")
                {
                    DateTime dtMessageDate = DateTime.Parse(dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessageDate"].ToString());
                    lblSuperAdmnMessage.Text = "Message from SuperAdmin:" + dsMessage.Tables[0].Rows[0]["ABM_BroadCastMessage"].ToString() + Environment.NewLine + " Sent on:" + dtMessageDate.ToString();
                    //lblSuperAdmnMessage.Text+="\n Sent on:"+
                }
            }
            else
            {
                lblSuperAdmnMessage.Text = "No more message from Super Admin.";
            }

        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            if (Cache[advisorVo.advisorId.ToString()] != null)
                Cache.Remove(advisorVo.advisorId.ToString());

            LoadAdminDashBoardDataInCaching();
            LoadAdminBranchPerformance();
            LoadBranchPerfomanceChart();
            LoadRMPerformanceChart();
            ShowMessageBroadcast();

        }

        protected void gvrAdminBranchPerform_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBranchPerformance = new DataTable();
            LoadAdminDashBoardDataInCaching();

            dtBranchPerformance = CreateBranchPerformanceTable();
            gvrAdminBranchPerform.DataSource = dtBranchPerformance;

            LoadBranchPerfomanceChart();
            LoadRMPerformanceChart();
            ShowMessageBroadcast();


        }


    }
}
