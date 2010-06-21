using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using VoCustomerProfiling;
using VoUser;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoUser;
using VoAlerts;
using BoAlerts;
using System.Data;
using System.Collections.Specialized;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCommon;

namespace WealthERP.Advisor
{
    public partial class AdvisorRMCustIndiDashboard : System.Web.UI.UserControl
    {
        CustomerFamilyBo customerFamilyBo = new CustomerFamilyBo();
        CustomerFamilyVo customerFamilyVo;
        List<CustomerFamilyVo> customerFamilyList = null;
        RMVo rmVo = new RMVo();
        CustomerVo customerVo = null;
        CustomerVo customerMemberVo = null;
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        AlertsBo alertsBo = new AlertsBo();
        CustomerBo customerBo = new CustomerBo();
        UserVo userVo = new UserVo();
        AssetBo assetBo = new AssetBo();
        PortfolioBo portfolioBo = new PortfolioBo();
        DataSet dsCustomerAssetMaturityDates = new DataSet();
        DataSet dsAssetAggrCurrentValues = new DataSet();
        DataSet dsCustomerAlerts = new DataSet();
        DataRow drMaturityDates;
        DataRow drCurrentValues;
        DataRow drCustomerAlerts;
        int customerId;
        int portfolioId;
        int memberCustomerId;
        int userId;
        string metatablePrimaryKey;
        double sum=0;
        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                SessionBo.CheckSession();
                userVo = (UserVo)Session[SessionContents.UserVo];
                customerVo = (CustomerVo)Session[SessionContents.CustomerVo];
                rmVo = (RMVo)Session[SessionContents.RmVo];
                userId = userVo.UserId;
                customerId = customerVo.CustomerId;
                customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerId);
                portfolioId = customerPortfolioVo.PortfolioId;
                Session[SessionContents.PortfolioId] = portfolioId;
                lblMessage.Visible = false;
                //trlblerrormsg.Visible = false;
                lblMaturityMsg.Visible = false;
                BindCustomerFamilyGrid();
                BindCustomerAssetMaturityDates();
                BindAssetInvestments();
                BindAssetCurrentValChart();
                BindCustomerAlerts();
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:Page_Load()");


                object[] objects = new object[10];

                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerMemberVo;
                objects[3] = rmVo;
                objects[4] = customerFamilyVo;
                objects[5] = customerFamilyList;
                objects[6] = customerPortfolioVo;
                objects[7] = userId;
                objects[8] = customerId;
                objects[9] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        //Function to populate the Customer Family Member details in the Grid
        public void BindCustomerFamilyGrid()
        {
            DataTable dtCustomerFamily = new DataTable();
            DataRow drCustomerFamily;
            try
            {
                customerFamilyList = customerFamilyBo.GetCustomerFamily(customerId);
                if (customerFamilyList == null)
                {
                    lblMessage.Visible = true;
                    //trlblerrormsg.Visible = true;
                    lblFamilyMembersNum.Text = "0";
                }
                else
                {
                    lblMessage.Visible = false;
                    //trlblerrormsg.Visible = false;
                   
                    dtCustomerFamily.Columns.Add("Member Name");
                    dtCustomerFamily.Columns.Add("Relationship");
                    lblFamilyMembersNum.Text = customerFamilyList.Count.ToString();

                    
                    for (int i = 0; i < customerFamilyList.Count; i++)
                    {
                        drCustomerFamily = dtCustomerFamily.NewRow();

                        customerFamilyVo = customerFamilyList[i];
                        memberCustomerId = customerFamilyVo.AssociateCustomerId;
                        customerMemberVo = customerBo.GetCustomer(memberCustomerId);
                        drCustomerFamily[0] = customerMemberVo.FirstName.ToString() + " " + customerMemberVo.MiddleName.ToString() + " " + customerMemberVo.LastName.ToString();
                        drCustomerFamily[1] = customerFamilyVo.Relationship.ToString();

                        dtCustomerFamily.Rows.Add(drCustomerFamily);

                    }

                    gvCustomerFamily.DataSource = dtCustomerFamily;
                    gvCustomerFamily.DataBind();
                    gvCustomerFamily.Visible = true; 
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindCustomerFamilyGrid()");


                object[] objects = new object[4];

                objects[0] = customerVo;                
                objects[2] = customerMemberVo;                
                objects[4] = customerFamilyVo;
                objects[5] = customerFamilyList;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        //function to populate the maturity dates in the grid
        public void BindCustomerAssetMaturityDates()
        {
            DataTable dtMaturityDates = new DataTable();
            try
            {
                dsCustomerAssetMaturityDates = assetBo.GetAssetMaturityDates(portfolioId);
                if (dsCustomerAssetMaturityDates.Tables[0].Rows.Count == 0)
                {
                    lblMaturityMsg.Visible = true;
                }
                else
                {
                    
                    dtMaturityDates.Columns.Add("Asset Group");
                    dtMaturityDates.Columns.Add("Asset Particulars");
                    dtMaturityDates.Columns.Add("Maturity Date");

                    foreach (DataRow dr in dsCustomerAssetMaturityDates.Tables[0].Rows)
                    {
                        drMaturityDates = dtMaturityDates.NewRow();

                        drMaturityDates[0] = dr["AssetGroup"].ToString();
                        if (dr["AssetParticulars"].ToString() != "")
                            drMaturityDates[1] = dr["AssetParticulars"].ToString();
                        else
                            drMaturityDates[1] = "N/A";
                        if (dr["MaturityDate"].ToString() != "")
                            drMaturityDates[2] = (DateTime.Parse(dr["MaturityDate"].ToString())).ToShortDateString();
                        else
                            drMaturityDates[2] = "N/A";

                        dtMaturityDates.Rows.Add(drMaturityDates);
                    }
                    gvMaturitySchedule.DataSource = dtMaturityDates;
                    gvMaturitySchedule.DataBind();
                    gvMaturitySchedule.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindCustomerAssetMaturityDates()");


                object[] objects = new object[2];

                objects[0] = portfolioId;
                objects[1] = dsCustomerAssetMaturityDates;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        //function to populate the Asset Class wise Investments in the grid
        public void BindAssetInvestments()
        {
            DataTable dtAssetAggrCurrentValues = new DataTable();
            double liabilityValue = 0;
            double networth = 0;
            try
            {
                dsAssetAggrCurrentValues = assetBo.GetPortfolioAssetAggregateCurrentValues(portfolioId);
                liabilityValue = assetBo.GetCustomerPortfolioLiability(portfolioId);
                if (dsAssetAggrCurrentValues.Tables[0].Rows.Count == 0)
                {
                    lblAssetDetailsMsg.Visible = true;
                }
                else
                {
                    lblAssetDetailsMsg.Visible = false;
                    
                    dtAssetAggrCurrentValues.Columns.Add("Asset Class");
                    dtAssetAggrCurrentValues.Columns.Add("Current Value");


                    foreach (DataRow dr in dsAssetAggrCurrentValues.Tables[0].Rows)
                    {
                        drCurrentValues = dtAssetAggrCurrentValues.NewRow();

                        drCurrentValues[0] = dr["AssetType"].ToString();
                        drCurrentValues[1] = String.Format("{0:n2}", double.Parse(dr["AggrCurrentValue"].ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));

                        dtAssetAggrCurrentValues.Rows.Add(drCurrentValues);

                        sum=sum+double.Parse(dr["AggrCurrentValue"].ToString());
                    }
                    gvAssetAggrCurrentValue.DataSource = dtAssetAggrCurrentValues;
                    gvAssetAggrCurrentValue.DataBind();
                    gvAssetAggrCurrentValue.Visible = true;
                    networth = sum - liabilityValue;
                    lblAssets.Text = String.Format("{0:n2}", double.Parse(sum.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    lblLiabilityValue.Text = String.Format("{0:n2}", double.Parse(liabilityValue.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
                    lblNetWorth.Text = String.Format("{0:n2}", double.Parse(networth.ToString()).ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN")));
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
                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindAssetInvestments()");
                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = dsAssetAggrCurrentValues;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public void BindAssetCurrentValChart()
        {
              Series seriesAssets=null;
              Legend legend = null;
              int i = 0;
              string[] XValues = null;
              double[] YValues = null;
            try
            {

                // Total Assets Chart
                seriesAssets = new Series("Assets");
                legend = new Legend("AssetsLegend");
                legend.Enabled = true;
                XValues = new string[dsAssetAggrCurrentValues.Tables[0].Rows.Count];
                YValues = new double[dsAssetAggrCurrentValues.Tables[0].Rows.Count];
               
                seriesAssets.ChartType = SeriesChartType.Pie;


                foreach (DataRow dr in dsAssetAggrCurrentValues.Tables[0].Rows)
                {
                    XValues[i] = dr["AssetType"].ToString();
                    YValues[i] = double.Parse(dr["AggrCurrentValue"].ToString());
                    i++;

                }
                seriesAssets.Points.DataBindXY(XValues, YValues);
                //Chart1.DataSource = dsAssetAggrCurrentValues.Tables[0].DefaultView;
                Chart1.Series.Clear();
                Chart1.Series.Add(seriesAssets);

                //Chart1.Series[0].XValueMember = "AssetType";
                //Chart1.Series[0].YValueMembers = "AggrCurrentValue";
                Chart1.Legends.Add(legend);

                Chart1.Series[0]["CollectedSliceExploded"] = "true";
                Chart1.Legends["AssetsLegend"].Title = "Assets";
                Chart1.Legends["AssetsLegend"].TitleAlignment = StringAlignment.Center;
                Chart1.Legends["AssetsLegend"].TitleSeparator = LegendSeparatorStyle.DoubleLine;
                Chart1.Legends["AssetsLegend"].TitleSeparatorColor = Color.Black;
                //Chart1.Legends["AssetsLegend"].BackColor = Color.Transparent;
                Chart1.Series[0].IsValueShownAsLabel = false;
                Chart1.Series[0]["PieLabelStyle"] = "Disabled";
                Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                Chart1.ChartAreas[0].Area3DStyle.Perspective = 50;
                Chart1.Series[0].ToolTip = "#VALX: #PERCENT";
                //Chart1.Series[0].Label = "#PERCENT";
                //Chart1.Series[0]["CollectedLegendText"] = "Other";
                Chart1.BackColor = Color.Transparent;
                Chart1.ChartAreas[0].BackColor = Color.Transparent;

                LegendCellColumn colorColumn = new LegendCellColumn();
                colorColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                colorColumn.HeaderText = "Color";
                colorColumn.HeaderBackColor = Color.WhiteSmoke;
                Chart1.Legends["AssetsLegend"].CellColumns.Add(colorColumn);

                LegendCellColumn asset = new LegendCellColumn();
                asset.ColumnType = LegendCellColumnType.Text;
                asset.HeaderText = "Asset";
                asset.Alignment = ContentAlignment.TopLeft;
                asset.Text = "#VALX";
                asset.HeaderBackColor = Color.WhiteSmoke;
                Chart1.Legends["AssetsLegend"].CellColumns.Add(asset);


                LegendCellColumn assetPercent = new LegendCellColumn();
                assetPercent.Alignment = ContentAlignment.MiddleLeft;
                assetPercent.Text = "#PERCENT";
                assetPercent.HeaderText = "% Of Assets";
                assetPercent.Name = "PerformanceColumn";
                assetPercent.HeaderBackColor = Color.WhiteSmoke;
                Chart1.Legends["AssetsLegend"].CellColumns.Add(assetPercent);


                foreach (DataPoint point in Chart1.Series[0].Points)
                {
                    point["Exploded"] = "true";
                }

                Chart1.DataBind();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindAssetCurrentValChart()");
                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = dsAssetAggrCurrentValues;
                objects[2] = XValues;
                objects[3] = YValues;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public void BindCustomerAlerts()
        {
            try
            {
                dsCustomerAlerts = alertsBo.GetCustomerDashboardAlerts(customerId);
                if (dsCustomerAlerts.Tables[0].Rows.Count == 0)
                {
                    lblAlertsMessage.Visible = true;
                }
                else
                {
                    lblAlertsMessage.Visible = false;
                    DataTable dtCustomerAlerts = new DataTable();
                    dtCustomerAlerts.Columns.Add("Details");
                    dtCustomerAlerts.Columns.Add("EventMessage");


                    foreach (DataRow dr in dsCustomerAlerts.Tables[0].Rows)
                    {
                        drCustomerAlerts = dtCustomerAlerts.NewRow();

                        drCustomerAlerts[0] = dr["EventCode"].ToString() +" : "+ dr["Name"].ToString();
                        drCustomerAlerts[1] = dr["EventMessage"].ToString();

                        dtCustomerAlerts.Rows.Add(drCustomerAlerts);

                    }
                    gvCustomerAlerts.DataSource = dtCustomerAlerts;
                    gvCustomerAlerts.DataBind();
                    gvCustomerAlerts.Visible = true;
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

                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:BindCustomerAlerts()");


                object[] objects = new object[2];

                objects[0] = customerId;
                objects[1] = dsCustomerAlerts;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected string GetSchemeName(string alertType, int SchemeID)
        {
            string schemeName="";

            DataSet dsmetatableDetails=null;
            DataSet dsSchemeName=null;
            string tableName="";
            string description="";


            try
            {
                if (alertType == "Property")
                {
                    metatablePrimaryKey = "CPNP_PropertyNPId";
                }
                else if (alertType == "SIP" || alertType == "SWP" || alertType == "STP")
                {
                    metatablePrimaryKey = "PASP_SchemePlanCode";
                }
                else if (alertType == "Personal")
                {
                    metatablePrimaryKey = "CPNP_PersonalNPId";
                }
                dsmetatableDetails = alertsBo.GetMetatableDetails(metatablePrimaryKey);
                tableName = dsmetatableDetails.Tables[0].Rows[0][2].ToString();
                description = dsmetatableDetails.Tables[0].Rows[0][1].ToString();

                dsSchemeName = alertsBo.GetSchemeDescription(description, tableName, metatablePrimaryKey, SchemeID);

                if (dsSchemeName.Tables[0].Rows.Count > 0)
                {
                    schemeName = dsSchemeName.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    schemeName = "N/A";
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
                FunctionInfo.Add("Method", "AdvisorRMCustIndiDashboard.ascx:GetSchemeName()");
                object[] objects = new object[9];
                objects[0] = customerId;
                objects[1] = dsCustomerAlerts;
                objects[2] = alertType;
                objects[3] = SchemeID;
                objects[4] = dsmetatableDetails;
                objects[5] = dsSchemeName;
                objects[6] = tableName;
                objects[7] = description;
                objects[8] = schemeName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return schemeName;
        }

        //public void ShowNetWorthSummary()
        //{
        //    foreach (DataRow dr in dsAssetAggrCurrentValues)
        //    {

        //    }
        //}
    }
}
