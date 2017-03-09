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
using BoAdvisorProfiling;
using VoUser;
using BoUploads;
using BoCustomerGoalProfiling;
using Telerik.Web.UI;
using BoCommon;
using System.Configuration;
using BoCustomerPortfolio;

namespace WealthERP.BusinessMIS
{
    public partial class EquityReturns : System.Web.UI.UserControl
    {
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorMISBo adviserMFMIS = new AdvisorMISBo();
        RMVo rmVo = new RMVo();
        UserVo userVo = new UserVo();
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();

        int advisorId = 0;
        String userType;
        int rmId = 0;
        int bmID = 0;
        int all = 0;
        int branchId = 0;
        int branchHeadId = 0;
        string strValuationDate;

        public enum Constants
        {
            EQ = 0,    
            EQDate = 2
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session[SessionContents.RmVo];
            userVo = (UserVo)Session["userVo"];
            AdvisorMISBo adviserMISBo = new AdvisorMISBo();
            if (Session[SessionContents.ValuationDate] == null)
                GetLatestValuationDate();
            genDict = (Dictionary<string, DateTime>)Session[SessionContents.ValuationDate];
            strValuationDate = genDict[Constants.EQDate.ToString()].ToShortDateString();
            lblPickDate.Text = DateTime.Parse(genDict[Constants.EQDate.ToString()].ToString()).ToShortDateString();

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;
            //if (!IsPostBack)
            //{
            //    SetParameters();
            //    BindEQReturnsGrid();
            //}
            pnlEqHoldings.Visible = false;
            pnlEQReturns.Visible = false;
            gvEqHldings.Visible = false;
            gvEQReturns.Visible = false;
            imgCompany.Visible = false;
            imgEQReturns.Visible = false;
        }
        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
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
                genDict.Add("EQDate", EQValuationDate);
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
                object[] objects = new object[2];
                objects[0] = EQValuationDate;
                objects[1] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void BindEQReturnsGrid()
        {
            double totalinvestedCost = 0.0;
            double totalcurrentvalue = 0.0;
            double totalPL=0.0;
            DataSet dsEQReturns;
            DataTable dtEQReturns;
            dsEQReturns = adviserMFMIS.GetEQReturnsDetails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), strValuationDate);
            dtEQReturns = dsEQReturns.Tables[1];
            if(dtEQReturns == null)
            {
                gvEQReturns.DataSource = dtEQReturns;
                gvEQReturns.DataBind();
                pnlEQReturns.Visible = true;
                gvEQReturns.Visible = true;
            }
            else
            {
                DataTable dtEQReturnsNew = new DataTable();
                dtEQReturnsNew.Columns.Add("CustomerId");
                dtEQReturnsNew.Columns.Add("Customer");
                dtEQReturnsNew.Columns.Add("PAN");
                dtEQReturnsNew.Columns.Add("Parent");
                dtEQReturnsNew.Columns.Add("Branch");
                dtEQReturnsNew.Columns.Add("RM");
                dtEQReturnsNew.Columns.Add("CompanyName");
                dtEQReturnsNew.Columns.Add("Sector");
                dtEQReturnsNew.Columns.Add("Price", typeof(Double));
                dtEQReturnsNew.Columns.Add("NoOfShares", typeof(Double));
                dtEQReturnsNew.Columns.Add("InvestedCost", typeof(Double));
                dtEQReturnsNew.Columns.Add("CurrentValue", typeof(Double));
                dtEQReturnsNew.Columns.Add("ProfitLoss", typeof(Double));
                dtEQReturnsNew.Columns.Add("Percentage", typeof(Double));

                DataRow drEQReturnsNew;
                foreach (DataRow dr in dtEQReturns.Rows)
                {
                    drEQReturnsNew = dtEQReturnsNew.NewRow();
                    drEQReturnsNew["CustomerId"] = dr["CustomerId"].ToString();
                    drEQReturnsNew["Customer"] = dr["Customer"].ToString();
                    drEQReturnsNew["PAN"] = dr["PAN"].ToString();
                    drEQReturnsNew["Parent"] = dr["Parent"].ToString();
                    drEQReturnsNew["Branch"] = dr["Branch"].ToString();
                    drEQReturnsNew["RM"] = dr["RM"].ToString();
                    drEQReturnsNew["CompanyName"] = dr["CompanyName"].ToString();
                    drEQReturnsNew["Sector"] = dr["PGSC_SectorCategoryName"].ToString();
                    if (!string.IsNullOrEmpty(dr["NoOfShares"].ToString().Trim()))
                        drEQReturnsNew["NoOfShares"] = dr["NoOfShares"].ToString();
                    else
                        drEQReturnsNew["NoOfShares"] = 0;

                    if (!string.IsNullOrEmpty(dr["Price"].ToString().Trim()))
                        drEQReturnsNew["Price"] = dr["Price"].ToString();
                    else
                        drEQReturnsNew["Price"] = 0;
                    
                    if (!string.IsNullOrEmpty(dr["InvestedCost"].ToString().Trim()))
                        drEQReturnsNew["InvestedCost"] = Double.Parse(dr["InvestedCost"].ToString());
                    else
                        drEQReturnsNew["InvestedCost"] = 0;
                    totalinvestedCost = totalinvestedCost + Double.Parse(dr["InvestedCost"].ToString());

                    if (!string.IsNullOrEmpty(dr["MarketValue"].ToString().Trim()))
                        drEQReturnsNew["CurrentValue"] = Double.Parse(dr["MarketValue"].ToString());
                    else
                        drEQReturnsNew["CurrentValue"] = 0;
                    totalcurrentvalue = totalcurrentvalue + Double.Parse(dr["MarketValue"].ToString());

                    if (!string.IsNullOrEmpty(dr["ProfitLoss"].ToString().Trim()))
                        drEQReturnsNew["ProfitLoss"] = dr["ProfitLoss"].ToString();
                    else
                        drEQReturnsNew["ProfitLoss"] = 0;

                    if (!string.IsNullOrEmpty(dr["Percentage"].ToString().Trim()))
                        drEQReturnsNew["Percentage"] = Double.Parse(dr["Percentage"].ToString());
                    else
                        drEQReturnsNew["Percentage"] = 0;

                    dtEQReturnsNew.Rows.Add(drEQReturnsNew);
                }
                GridBoundColumn TotalPercentage = gvEQReturns.MasterTableView.Columns.FindByUniqueName("Percentage") as GridBoundColumn;
                totalPL = ((totalcurrentvalue - totalinvestedCost) / totalinvestedCost) * 100;
                TotalPercentage.FooterText = totalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                gvEQReturns.DataSource = dtEQReturnsNew;
                gvEQReturns.DataBind();

                //GridFooterItem fitem = (GridFooterItem)gvEQReturns.MasterTableView.GetItems(GridItemType.Footer)[0];
                //Label lbl = (Label)fitem.FindControl("lblTotalPercentage"); 
                //Label lblTotalText = (Label)gvEQReturns.FindControl("lblTotalPercentage");
                //lbl.Text = totalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                pnlEQReturns.Visible = true;
                gvEQReturns.Visible = true;
                imgEQReturns.Visible = true;
                if (Cache["gvEQReturns" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvEQReturns" + userVo.UserId + userType, dtEQReturnsNew);
                }
                else
                {
                    Cache.Remove("gvEQReturns" + userVo.UserId + userType);
                    Cache.Insert("gvEQReturns" + userVo.UserId + userType, dtEQReturnsNew);
                }
            }
        }
        private void SetParameters()
        {
            if (userType == "advisor")
            {
                hdnadviserId.Value = advisorVo.advisorId.ToString();
                hdnAll.Value = "0";
                hdnbranchId.Value = "0";
                hdnrmId.Value = "0";

            }
            else if (userType == "rm")
            {
                hdnrmId.Value = rmVo.RMId.ToString();
                hdnAll.Value = "0";

            }
            else if (userType == "bm")
            {
                hdnbranchHeadId.Value = bmID.ToString();
                hdnAll.Value = "0";
                hdnrmId.Value = "0";
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
        protected void gvEQReturns_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMfReturns = new DataTable();
            dtMfReturns = (DataTable)Cache["gvEQReturns" + userVo.UserId + userType];
            gvEQReturns.DataSource = dtMfReturns;
            gvEQReturns.Visible = true;

            pnlEqHoldings.Visible = false;
            pnlEQReturns.Visible = true;
            gvEqHldings.Visible = false;
            imgCompany.Visible = false;
            imgEQReturns.Visible = true;
        }

        protected void imgEQReturns_Click(object sender, ImageClickEventArgs e)
        {
            gvEQReturns.ExportSettings.OpenInNewWindow = true;
            gvEQReturns.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvEQReturns.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvEQReturns.MasterTableView.ExportToExcel();
        }

        protected void imgCompany_Click(object sender, ImageClickEventArgs e)
        {
            gvEqHldings.ExportSettings.OpenInNewWindow = true;
            gvEqHldings.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvEQReturns.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvEqHldings.MasterTableView.ExportToExcel();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            SetParameters();
            if (ddlType.SelectedValue == "CustomerWise")
                BindEQHoldings();
            else if (ddlType.SelectedValue == "CompanyWise")
                BindEQReturnsGrid();
        }

        private void BindEQHoldings()
        {
            double totalinvestedCost = 0.0;
            double totalcurrentvalue = 0.0;
            double totalPL = 0.0;
            DataSet dsEqReturns;
            DataTable dtEQHoldings;
            dsEqReturns = adviserMFMIS.GetEQReturnsDetails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), strValuationDate);
            dtEQHoldings = dsEqReturns.Tables[0];
            if (dtEQHoldings == null)
            {
                gvEqHldings.DataSource = dtEQHoldings;
                gvEqHldings.DataBind();
                pnlEqHoldings.Visible = true;
                gvEqHldings.Visible = true;
            }
            else
            {
                DataTable dtEQHoldingsNew = new DataTable();
                dtEQHoldingsNew.Columns.Add("CustomerId");
                dtEQHoldingsNew.Columns.Add("Customer");
                dtEQHoldingsNew.Columns.Add("PAN");
                dtEQHoldingsNew.Columns.Add("Parent");
                dtEQHoldingsNew.Columns.Add("Branch");
                dtEQHoldingsNew.Columns.Add("RM");
                dtEQHoldingsNew.Columns.Add("Price", typeof(Double));
                dtEQHoldingsNew.Columns.Add("NoOfShares", typeof(Double));
                dtEQHoldingsNew.Columns.Add("InvestedCost", typeof(Double));
                dtEQHoldingsNew.Columns.Add("CurrentValue", typeof(Double));
                dtEQHoldingsNew.Columns.Add("ProfitLoss", typeof(Double));
                dtEQHoldingsNew.Columns.Add("Percentage", typeof(Double));

                DataRow drEQHoldingsNew;
                foreach (DataRow dr in dtEQHoldings.Rows)
                {
                    drEQHoldingsNew = dtEQHoldingsNew.NewRow();
                    drEQHoldingsNew["CustomerId"] = dr["CustomerId"].ToString();
                    drEQHoldingsNew["Customer"] = dr["Customer"].ToString();
                    drEQHoldingsNew["PAN"] = dr["PAN"].ToString();
                    drEQHoldingsNew["Parent"] = dr["Parent"].ToString();
                    drEQHoldingsNew["Branch"] = dr["Branch"].ToString();
                    drEQHoldingsNew["RM"] = dr["RM"].ToString();
                    if (!string.IsNullOrEmpty(dr["NoOfShares"].ToString().Trim()))
                        drEQHoldingsNew["NoOfShares"] = dr["NoOfShares"].ToString();
                    else
                        drEQHoldingsNew["NoOfShares"] = 0;

                    if (!string.IsNullOrEmpty(dr["Price"].ToString().Trim()))
                        drEQHoldingsNew["Price"] = dr["Price"].ToString();
                    else
                        drEQHoldingsNew["Price"] = 0;

                    if (!string.IsNullOrEmpty(dr["InvestedCost"].ToString().Trim()))
                        drEQHoldingsNew["InvestedCost"] = Double.Parse(dr["InvestedCost"].ToString());
                    else
                        drEQHoldingsNew["InvestedCost"] = 0;
                    totalinvestedCost = totalinvestedCost + Double.Parse(dr["InvestedCost"].ToString());

                    if (!string.IsNullOrEmpty(dr["MarketValue"].ToString().Trim()))
                        drEQHoldingsNew["CurrentValue"] = Double.Parse(dr["MarketValue"].ToString());
                    else
                        drEQHoldingsNew["CurrentValue"] = 0;
                    totalcurrentvalue = totalcurrentvalue + Double.Parse(dr["MarketValue"].ToString());

                    if (!string.IsNullOrEmpty(dr["ProfitLoss"].ToString().Trim()))
                        drEQHoldingsNew["ProfitLoss"] = dr["ProfitLoss"].ToString();
                    else
                        drEQHoldingsNew["ProfitLoss"] = 0;

                    if (!string.IsNullOrEmpty(dr["Percentage"].ToString().Trim()))
                        drEQHoldingsNew["Percentage"] = Double.Parse(dr["Percentage"].ToString());
                    else
                        drEQHoldingsNew["Percentage"] = 0;

                    dtEQHoldingsNew.Rows.Add(drEQHoldingsNew);
                }
                GridBoundColumn TotalPercentage = gvEqHldings.MasterTableView.Columns.FindByUniqueName("Percentage") as GridBoundColumn;
                totalPL = ((totalcurrentvalue - totalinvestedCost) / totalinvestedCost) * 100;
                TotalPercentage.FooterText = totalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                gvEqHldings.DataSource = dtEQHoldingsNew;
                gvEqHldings.DataBind();
                pnlEqHoldings.Visible = true;
                gvEqHldings.Visible = true;
                imgCompany.Visible = true;
                if (Cache["gvEqHldings" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvEqHldings" + userVo.UserId + userType, dtEQHoldingsNew);
                }
                else
                {
                    Cache.Remove("gvEqHldings" + userVo.UserId + userType);
                    Cache.Insert("gvEqHldings" + userVo.UserId + userType, dtEQHoldingsNew);
                }
            }
        }

        protected void gvEqHldings_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dtEQHoldings = new DataTable();
            dtEQHoldings = (DataTable)Cache["gvEqHldings" + userVo.UserId + userType];
            gvEqHldings.DataSource = dtEQHoldings;
            gvEqHldings.Visible = true;

            pnlEqHoldings.Visible = true;
            pnlEQReturns.Visible = false;
            gvEQReturns.Visible = false;
            imgCompany.Visible = true;
            imgEQReturns.Visible = false;
        }
    }
}