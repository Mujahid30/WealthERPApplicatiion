﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerProfiling;
using BoUser;
using BoCustomerProfiling;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using WealthERP.Base;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Drawing;
using System.Collections.Specialized;
using BoCommon;

namespace WealthERP.CustomerPortfolio
{
    public partial class PortfolioDashboard : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        PortfolioBo portfolioBo = new PortfolioBo();
        AdvisorVo adviserVo = null;
        int adviserId = 0;
        static int portfolioId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                customerVo = (CustomerVo)Session["CustomerVo"];
                rmVo = (RMVo)Session["RmVo"];
                userVo = (UserVo)Session["userVo"];
                if (userVo.UserType == "Advisor")
                {
                    adviserVo = (AdvisorVo)Session["advisorVo"];
                    adviserId = adviserVo.advisorId;
                }
                else if (userVo.UserType == "RM")
                {
                    adviserId = int.Parse(Session["adviserId"].ToString());
                }
                adviserId=((AdvisorVo)Session["advisorVo"]).advisorId;

                GetLatestValuationDate();
                if (!IsPostBack)
                {
                    if (Session[SessionContents.PortfolioId] != null)
                    {
                        portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    }
                    else
                    {
                        customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                        portfolioId = customerPortfolioVo.PortfolioId;
                    }
                    
                    BindPortfolioDropDown();
                    LoadChartsAndGrids();
                }
                else
                {
                    portfolioId = int.Parse(Session[SessionContents.PortfolioId].ToString());
                    BindPortfolioDropDown();

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

                FunctionInfo.Add("Method", "PortfolioDashboard.ascx:Page_Load()");
                object[] objects = new object[6];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = rmVo;
                objects[3] = adviserVo;
                objects[4] = customerPortfolioVo;
                objects[5]=adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        private void LoadChartsAndGrids()
        {
            DataSet dsAssetChart = new DataSet();
            DataSet dsMFInv = new DataSet();
            DataSet dsEquity = new DataSet();
            DataSet dsFI = new DataSet();
            DataSet dsOtherAssets = new DataSet();
            DataSet dsMFInvestments = new DataSet();
            DataSet dsEquityDirect = new DataSet();
            DataSet dsNetIncomeSummary = new DataSet();
            AssetBo assetsBo = new AssetBo();
            double TotalAssetValue = 0.0F;
            double mfRealizedPL = 0;
            double mfDivIncome = 0;
            double mfSubTotal = 0;
            double eqSpecPL = 0;
            double eqDelivPL = 0;
            double eqDivIncome = 0;
            double eqSubTotal = 0;
            double assetTotal = 0;
            double divIncomeTotal = 0;
            // Bind Total Asset DataSet
            //dsAssetChart = assetsBo.GetPortfolioAssetAggregateCurrentValues(customerPortfolioVo.PortfolioId);
            dsAssetChart = assetsBo.GetPortfolioAssetAggregateCurrentValues(portfolioId);

            // Bind MF Investments DataSet
            dsMFInv = assetsBo.GetMFInvAggrCurrentValues(portfolioId, adviserId);

            // Bind Equity DataSet
            dsEquity = assetsBo.GetEQAggrCurrentValues(portfolioId, adviserId);

            // Bind FI Dashboard DataSet
            dsFI = assetsBo.GetFIGovtInsDashboardCurrentValues(portfolioId);

            // Bind Other Asset Dashboard DataSet
            dsOtherAssets = assetsBo.GetOtherAssetsDashboardCurrentValues(portfolioId);

            // Bind Net Income Summary
            dsNetIncomeSummary = assetsBo.GetNetIncomeSummary(portfolioId);

            BindMFChart(dsMFInv.Tables[1]);
            BindMFGrid(dsMFInv.Tables[0]);
            BindEQGrid(dsEquity.Tables[0]);
            //BindEQChart();
            BindFIDashGrid(dsFI.Tables[0]);
            BindOtherAssetGrid(dsOtherAssets.Tables[0]);

            BindAssetChart(dsAssetChart);

            // Bind Total Asset Labels
            if (dsAssetChart.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsAssetChart.Tables[0].Rows.Count; i++)
                {
                    TotalAssetValue += double.Parse(dsAssetChart.Tables[0].Rows[i]["AggrCurrentValue"].ToString());
                }
                lblTotalAssetsValue.Text = String.Format("{0:n2}", decimal.Parse(TotalAssetValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            }
            else
            {
                lblTotalAssetsValue.Text = String.Format("{0:n2}", decimal.Parse(TotalAssetValue.ToString()));
            }

            lblNetWorthValue.Text = String.Format("{0:n2}", decimal.Parse((TotalAssetValue - 0).ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

            if (dsNetIncomeSummary.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsNetIncomeSummary.Tables[0].Rows)
                {
                    if (dr["NetIncome"].ToString() == "MFRealizedPL")
                    {
                        mfRealizedPL = double.Parse(dr["AggrValue"].ToString());
                    }
                    else if (dr["NetIncome"].ToString() == "Realised G/L - Deliv")
                    {
                        eqDelivPL = double.Parse(dr["AggrValue"].ToString());
                    }
                    else if (dr["NetIncome"].ToString() == "Realised G/L - Spec")
                    {
                        eqSpecPL = double.Parse(dr["AggrValue"].ToString());
                    }
                    else if (dr["NetIncome"].ToString() == "Dividend Income")
                    {
                        mfDivIncome = double.Parse(dr["AggrValue"].ToString());
                    }
                }
                mfSubTotal = mfRealizedPL;
                eqSubTotal = eqSpecPL + eqDelivPL;
                assetTotal = mfSubTotal + eqSubTotal;
                
                eqDivIncome = 0;
                divIncomeTotal = mfDivIncome + eqDivIncome;

                lblDividend.Text = String.Format("{0:n2}", mfDivIncome.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                lblMFRPLValue.Text = String.Format("{0:n2}", mfRealizedPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblMFSubTotalValue.Text = String.Format("{0:n2}", mfSubTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblRealisedDeliv.Text = String.Format("{0:n2}", eqDelivPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblRealisedSpec.Text = String.Format("{0:n2}", eqSpecPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblEQSubTotalValue.Text = String.Format("{0:n2}", eqSubTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblTotalValue.Text = String.Format("{0:n2}", assetTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblEQDidvidend.Text = String.Format("{0:n2}", eqDivIncome.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                lblDivIncomeTotal.Text = String.Format("{0:n2}", divIncomeTotal.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
            }
        }
        private void BindPortfolioDropDown()
        {
            DataSet ds = portfolioBo.GetCustomerPortfolio(customerVo.CustomerId);
            ddlPortfolio.DataSource = ds;
            ddlPortfolio.DataValueField = ds.Tables[0].Columns["CP_PortfolioId"].ToString();
            ddlPortfolio.DataTextField = ds.Tables[0].Columns["CP_PortfolioName"].ToString();
            ddlPortfolio.DataBind();
            //ddlPortfolio.Items.Insert(0, "Select the Portfolio");

            ddlPortfolio.SelectedValue = portfolioId.ToString();
           
        }

        private void BindMFChart(DataTable dtMFInvChart)
        {

            if (dtMFInvChart.Rows.Count > 0)
            {
                trMFData.Visible = true;
                trMFNoRecords.Visible = false;
                trMFDate.Visible = true;

                // Total Assets Chart
                Series seriesAssets = new Series("MFInv");
                seriesAssets.ChartType = SeriesChartType.Column;
                
                chrtMFInv.DataSource = dtMFInvChart.DefaultView;
                chrtMFInv.Series.Clear();
                chrtMFInv.Series.Add(seriesAssets);
                chrtMFInv.Series[0].XValueMember = "MFType";
                chrtMFInv.Series[0].YValueMembers = "AggrCurrentValue";
                // Enable X axis margin
                chrtMFInv.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
                chrtMFInv.BackColor = Color.Transparent;
                chrtMFInv.ChartAreas[0].BackColor = Color.Transparent;

                chrtMFInv.DataBind();
            }
            else
            {
                trMFData.Visible = false;
                trMFDate.Visible = false;
                trMFNoRecords.Visible = true;
            }
        }

        private void BindMFGrid(DataTable dtMFInvGrid)
        {
            Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
            DateTime tradeDate = new DateTime();
            if (dtMFInvGrid.Rows.Count > 0)
            {
                genDict = (Dictionary<string,DateTime>)Session["ValuationDate"];
                tradeDate = DateTime.Parse(genDict["MFDate"].ToString());
                trMFDate.Visible = true;
                trMFData.Visible = true;
                trMFNoRecords.Visible = false;




                DataTable dtCurrentValusForMF = new DataTable();
                //dtCurrentValusForMF.Columns.Add("CustomerId");
                dtCurrentValusForMF.Columns.Add("SchemeType");
                dtCurrentValusForMF.Columns.Add("Scheme");
                dtCurrentValusForMF.Columns.Add("AmortisedCost");
                dtCurrentValusForMF.Columns.Add("CurrentValue");
                DataRow drCurrentValuesForMF;

                for (int i = 0; i < dtMFInvGrid.Rows.Count; i++)
                {
                    drCurrentValuesForMF = dtCurrentValusForMF.NewRow();
                    //drCurrentValuesForMF[0] = dtMFInvGrid.Rows[i]["CustomerId"].ToString();
                    drCurrentValuesForMF[0] = dtMFInvGrid.Rows[i]["SchemeType"].ToString();
                    drCurrentValuesForMF[1] = dtMFInvGrid.Rows[i]["Scheme"].ToString();
                    drCurrentValuesForMF[2] = String.Format("{0:n2}", decimal.Parse(dtMFInvGrid.Rows[i]["AmortisedCost"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    drCurrentValuesForMF[3] = String.Format("{0:n2}", decimal.Parse(dtMFInvGrid.Rows[i]["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    dtCurrentValusForMF.Rows.Add(drCurrentValuesForMF);
                }



                dtMFInvGrid = dtCurrentValusForMF;
                gvMFInv.DataSource = dtMFInvGrid;
                gvMFInv.DataBind();
                lblMFDate.Text = "Latest Valuation on  " + tradeDate.ToLongDateString().ToString();
            }
            else
            {
                trMFData.Visible = false;
                trMFDate.Visible = false;
                trMFNoRecords.Visible = true;
                gvMFInv.DataSource = null;
                gvMFInv.DataBind();
            }
        }

        private void BindEQGrid(DataTable dtEquityGrid)
        {
            Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
            DateTime tradeDate=new DateTime();
            if (dtEquityGrid.Rows.Count > 0)
            {
                genDict = (Dictionary<string, DateTime>)Session["ValuationDate"];
                tradeDate = DateTime.Parse(genDict["EQDate"].ToString());
                trEQData.Visible = true;
                trEQNoRecords.Visible = false;
                trEQDate.Visible = true;



                DataTable dtCurrentValusForEQ = new DataTable();
                //dtCurrentValusForMF.Columns.Add("CustomerId");
                dtCurrentValusForEQ.Columns.Add("Script");
                dtCurrentValusForEQ.Columns.Add("NetHoldings");
                dtCurrentValusForEQ.Columns.Add("AmortisedCost");
                dtCurrentValusForEQ.Columns.Add("CurrentValue");
                DataRow drCurrentValuesForEQ;

                for (int i = 0; i < dtEquityGrid.Rows.Count; i++)
                {
                    drCurrentValuesForEQ = dtCurrentValusForEQ.NewRow();
                    //drCurrentValuesForMF[0] = dtMFInvGrid.Rows[i]["CustomerId"].ToString();
                    drCurrentValuesForEQ[0] = dtEquityGrid.Rows[i]["Script"].ToString();
                    drCurrentValuesForEQ[1] = String.Format("{0:n2}", decimal.Parse(dtEquityGrid.Rows[i]["NetHoldings"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    drCurrentValuesForEQ[2] = String.Format("{0:n2}", decimal.Parse(dtEquityGrid.Rows[i]["AmortisedCost"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    drCurrentValuesForEQ[3] = String.Format("{0:n2}", decimal.Parse(dtEquityGrid.Rows[i]["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    dtCurrentValusForEQ.Rows.Add(drCurrentValuesForEQ);
                }



                dtEquityGrid = dtCurrentValusForEQ;



                gvEquity.DataSource = dtEquityGrid;
                gvEquity.DataBind();
                lblEQDate.Text ="Latest Valuation on  "+ tradeDate.ToLongDateString().ToString();
            }
            else
            {
                trEQData.Visible = false;
                trEQNoRecords.Visible = true;
                trEQDate.Visible = false;
                gvEquity.DataSource = null;
                gvEquity.DataBind();
            }
        }

        private void BindFIDashGrid(DataTable dtFIDashgrid)
        {
            if (dtFIDashgrid.Rows.Count > 0)
            {
                trGovData.Visible = true;
                trGovNoRecords.Visible = false;







                DataTable dtCurrentValusForFI = new DataTable();
                //dtCurrentValusForMF.Columns.Add("CustomerId");
                dtCurrentValusForFI.Columns.Add("AssetType");
                dtCurrentValusForFI.Columns.Add("AssetParticulars");
                dtCurrentValusForFI.Columns.Add("PurchaseCost");
                dtCurrentValusForFI.Columns.Add("CurrentValue");
                DataRow drCurrentValuesForFI;

                for (int i = 0; i < dtFIDashgrid.Rows.Count; i++)
                {
                    drCurrentValuesForFI = dtCurrentValusForFI.NewRow();
                    //drCurrentValuesForMF[0] = dtMFInvGrid.Rows[i]["CustomerId"].ToString();
                    drCurrentValuesForFI[0] = dtFIDashgrid.Rows[i]["AssetType"].ToString();
                    drCurrentValuesForFI[1] = dtFIDashgrid.Rows[i]["AssetParticulars"].ToString();
                    drCurrentValuesForFI[2] = String.Format("{0:n2}", decimal.Parse(dtFIDashgrid.Rows[i]["PurchaseCost"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    drCurrentValuesForFI[3] = String.Format("{0:n2}", decimal.Parse(dtFIDashgrid.Rows[i]["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    dtCurrentValusForFI.Rows.Add(drCurrentValuesForFI);
                }



                dtFIDashgrid = dtCurrentValusForFI;



                gvFIGovIns.DataSource = dtFIDashgrid;
                gvFIGovIns.DataBind();
            }
            else
            {
                trGovData.Visible = false;
                trGovNoRecords.Visible = true;
                gvFIGovIns.DataSource = null;
                gvFIGovIns.DataBind();
            }
        }

        private void BindOtherAssetGrid(DataTable dtOtherAssetGrid)
        {
            if (dtOtherAssetGrid.Rows.Count > 0)
            {
                trOtherData.Visible = true;
                trOtherNoRecords.Visible = false;




                DataTable dtCurrentValusForOT = new DataTable();
                //dtCurrentValusForMF.Columns.Add("CustomerId");
                dtCurrentValusForOT.Columns.Add("AssetType");
                dtCurrentValusForOT.Columns.Add("AssetParticulars");
                dtCurrentValusForOT.Columns.Add("PurchaseCost");
                dtCurrentValusForOT.Columns.Add("CurrentValue");
                DataRow drCurrentValuesForOT;

                for (int i = 0; i < dtOtherAssetGrid.Rows.Count; i++)
                {
                    drCurrentValuesForOT = dtCurrentValusForOT.NewRow();
                    //drCurrentValuesForMF[0] = dtMFInvGrid.Rows[i]["CustomerId"].ToString();
                    drCurrentValuesForOT[0] = dtOtherAssetGrid.Rows[i]["AssetType"].ToString();
                    drCurrentValuesForOT[1] = dtOtherAssetGrid.Rows[i]["AssetParticulars"].ToString();
                    drCurrentValuesForOT[2] = String.Format("{0:n2}", decimal.Parse(dtOtherAssetGrid.Rows[i]["PurchaseCost"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    drCurrentValuesForOT[3] = String.Format("{0:n2}", decimal.Parse(dtOtherAssetGrid.Rows[i]["CurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                    dtCurrentValusForOT.Rows.Add(drCurrentValuesForOT);
                }



                dtOtherAssetGrid = dtCurrentValusForOT;



                gvOtherAssets.DataSource = dtOtherAssetGrid;
                gvOtherAssets.DataBind();
            }
            else
            {
                trOtherData.Visible = false;
                trOtherNoRecords.Visible = true;
                gvOtherAssets.DataSource = null;
                gvOtherAssets.DataBind();
            }
        }

        private void BindAssetChart(DataSet dsAssetChart)
        {
            // Total Assets Chart
            Series seriesAssets = new Series("Assets");
            Legend legend = new Legend("AssetsLegend");
            legend.Enabled = true;
            string[] XValues = new string[dsAssetChart.Tables[0].Rows.Count];
            double[] YValues = new double[dsAssetChart.Tables[0].Rows.Count];
            int i = 0;
            seriesAssets.ChartType = SeriesChartType.Pie;

            foreach (DataRow dr in dsAssetChart.Tables[0].Rows)
            {
                XValues[i] = dr["AssetType"].ToString();
                YValues[i] = double.Parse(dr["AggrCurrentValue"].ToString());
                i++;
            }
            seriesAssets.Points.DataBindXY(XValues, YValues);
            //chrtTotalAssets.DataSource = dsAssetChart.Tables[0].DefaultView;

            chrtTotalAssets.Series.Clear();
            chrtTotalAssets.Series.Add(seriesAssets);
            
            //chrtTotalAssets.Series["Assets"].XValueMember = "AssetType";
            //chrtTotalAssets.Series["Assets"].YValueMembers = "AggrCurrentValue";
            chrtTotalAssets.Legends.Add(legend);
            chrtTotalAssets.Series["Assets"]["CollectedSliceExploded"] = "true";
            chrtTotalAssets.Legends["AssetsLegend"].Title = "Assets";
            chrtTotalAssets.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
            chrtTotalAssets.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
            chrtTotalAssets.Legends["AssetsLegend"].TitleSeparatorColor = Color.Black;
            chrtTotalAssets.Series["Assets"]["PieLabelStyle"] = "Disabled";

            chrtTotalAssets.ChartAreas[0].Area3DStyle.Enable3D = true;
            chrtTotalAssets.ChartAreas[0].Area3DStyle.Perspective = 50;
            //chrtTotalAssets.ChartAreas[0].InnerPlotPosition.Width = 100;
            chrtTotalAssets.Width = 500;
            chrtTotalAssets.BackColor = Color.Transparent;
            chrtTotalAssets.ChartAreas[0].BackColor = Color.Transparent;
            chrtTotalAssets.Series["Assets"].ToolTip = "#VALX: #PERCENT";

            LegendCellColumn colors = new LegendCellColumn();
            colors.HeaderText = "Color";
            colors.ColumnType = LegendCellColumnType.SeriesSymbol;
            colors.HeaderBackColor = Color.WhiteSmoke;
            chrtTotalAssets.Legends["AssetsLegend"].CellColumns.Add(colors);

            LegendCellColumn asset = new LegendCellColumn();
            asset.Alignment = ContentAlignment.MiddleLeft;
            asset.HeaderText = "Asset";
            asset.Text = "#VALX";
            chrtTotalAssets.Legends["AssetsLegend"].CellColumns.Add(asset);

            LegendCellColumn assetPercent = new LegendCellColumn();
            assetPercent.Alignment = ContentAlignment.MiddleLeft;
            assetPercent.HeaderText = "Asset Percentage";
            assetPercent.Text = "#PERCENT";
            chrtTotalAssets.Legends["AssetsLegend"].CellColumns.Add(assetPercent);

            foreach (DataPoint point in chrtTotalAssets.Series["Assets"].Points)
            {
                point["Exploded"] = "true";
            }

            chrtTotalAssets.DataBind();
            //chrtTotalAssets.Series["Assets"]. 
        }

        protected void gvMFInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMFInv_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvEquity_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEquity_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvFIGovIns_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvFIGovIns_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvOtherAssets_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvOtherAssets_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();                   
                advisorVo = (AdvisorVo)Session["advisorVo"];
                adviserId = advisorVo.advisorId;               
                

                if (portfolioBo.GetLatestValuationDate(adviserId, "EQ") != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "EQ").ToString());
                }
                if (portfolioBo.GetLatestValuationDate(adviserId, "MF") != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "MF").ToString());
                }
                genDict.Add("EQDate", EQValuationDate);
                genDict.Add("MFDate", MFValuationDate);
                Session["ValuationDate"] = genDict;
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

        protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session[SessionContents.PortfolioId] = ddlPortfolio.SelectedItem.Value.ToString();
            portfolioId = int.Parse(ddlPortfolio.SelectedItem.Value.ToString());
            LoadChartsAndGrids();
        }

    }
}
