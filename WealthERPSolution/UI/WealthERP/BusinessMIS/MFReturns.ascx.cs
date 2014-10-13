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
    public partial class MFReturns : System.Web.UI.UserControl
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
        int customerid = 0;
        string strValuationDate;

        public enum Constants
        {
            MF = 1,
            MFDate = 3
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
            strValuationDate = genDict[Constants.MFDate.ToString()].ToShortDateString();
            lblPickDate.Text = DateTime.Parse(genDict[Constants.MFDate.ToString()].ToString()).ToShortDateString();

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();
            if (userType == "associates")
            {
                ddlTypes.Items[0].Enabled = false;
                ddlTypes.Items[2].Enabled = false; 
            }
            advisorId = advisorVo.advisorId;
            int RMId = rmVo.RMId;
            rmId = rmVo.RMId;
            bmID = rmVo.RMId;

            pnlMfReturns.Visible = false;
            pnlScheme.Visible = false;
            gvMfReturns.Visible = false;
            gvMfReturnsScheme.Visible = false;
            imgScheme.Visible = false;
            imgMFReturns.Visible = false;
            if (!IsPostBack)
            {
                if (Request.QueryString["strCustomreId"] != null )
                {
                    ddlType.SelectedValue = "SchemeWise";
                    SetParameters();
                    GetLatestValuationDate();
                    customerid = int.Parse(Request.QueryString["strCustomreId"].ToString());
                    BindMFSchemeWise();
                    gvMfReturnsScheme.Visible = true;
                    //int accountId = int.Parse(Request.QueryString["folionum"].ToString());
                    //int SchemePlanCode = int.Parse(Request.QueryString["SchemePlanCode"].ToString());
                    //PasssedFolioValue = accountId;
                    //BindLastTradeDate();
                    //string fromdate = "01-01-1990";
                    //txtFromDate.SelectedDate = DateTime.Parse(fromdate);
                    //ViewState["SchemePlanCode"] = SchemePlanCode;
                }
            }
        }
        protected void gvMfReturns_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Redirect")
            {
                GridDataItem item = (GridDataItem)e.Item;
                string folionum = item.GetDataKeyValue("accountno").ToString();
                string SchemePlanCode = item.GetDataKeyValue("schemecode").ToString();
                string name = "Select";
                Response.Redirect("ControlHost.aspx?pageid=RMMultipleTransactionView&folionum=" + folionum + "&SchemePlanCode=" + SchemePlanCode + "&name=" + name + "", false);

                

            }
        }
        private void BindMfReturnsGrid()
        {
            double totalinvestedCost = 0.0;
            double totalcurrentvalue = 0.0;
            double totalPL = 0.0;
            DataSet dsMfReturns;
            DataTable dtMfReturns;
            customerid = 0;
            dsMfReturns = adviserMFMIS.GetMFReturnsDetails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), strValuationDate, customerid,int.Parse(ddlTypes.SelectedValue));
            dtMfReturns = dsMfReturns.Tables[0];
            if (dtMfReturns == null)
            {
                gvMfReturns.DataSource = dtMfReturns;
                gvMfReturns.DataBind();
            }
            else
            {
                DataTable dtMFReturnsNew = new DataTable();
                dtMFReturnsNew.Columns.Add("CustomerId");
                dtMFReturnsNew.Columns.Add("Customer");
                dtMFReturnsNew.Columns.Add("PAN");
                dtMFReturnsNew.Columns.Add("Parent");
                dtMFReturnsNew.Columns.Add("Branch");
                dtMFReturnsNew.Columns.Add("RM");
                dtMFReturnsNew.Columns.Add("InvestedCost", typeof(Double));
                dtMFReturnsNew.Columns.Add("CurrentValue", typeof(Double));
                dtMFReturnsNew.Columns.Add("ProfitLoss", typeof(Double));
                dtMFReturnsNew.Columns.Add("Percentage", typeof(Double));
                


                DataRow drMFReturnsNew;
                foreach (DataRow dr in dtMfReturns.Rows)
                {
                    drMFReturnsNew = dtMFReturnsNew.NewRow();
                    drMFReturnsNew["CustomerId"] = dr["CustomerId"].ToString();
                    drMFReturnsNew["Customer"] = dr["Customer"].ToString();
                    drMFReturnsNew["PAN"] = dr["PAN"].ToString();
                    drMFReturnsNew["Parent"] = dr["Parent"].ToString();
                    drMFReturnsNew["Branch"] = dr["Branch"].ToString();
                    drMFReturnsNew["RM"] = dr["RM"].ToString();
                    if (!string.IsNullOrEmpty(dr["InvestedCost"].ToString().Trim()))
                        drMFReturnsNew["InvestedCost"] = Double.Parse(dr["InvestedCost"].ToString());
                    else
                        drMFReturnsNew["InvestedCost"] = 0;
                    totalinvestedCost = totalinvestedCost + Double.Parse(dr["InvestedCost"].ToString());

                    if (!string.IsNullOrEmpty(dr["CurrentValue"].ToString().Trim()))
                        drMFReturnsNew["CurrentValue"] = Double.Parse(dr["CurrentValue"].ToString());
                    else
                        drMFReturnsNew["CurrentValue"] = 0;
                    totalcurrentvalue = totalcurrentvalue + Double.Parse(dr["CurrentValue"].ToString());

                    if (!string.IsNullOrEmpty(dr["ProfitLoss"].ToString().Trim()))
                        drMFReturnsNew["ProfitLoss"] = dr["ProfitLoss"].ToString();
                    else
                        drMFReturnsNew["ProfitLoss"] = 0;

                    if (!string.IsNullOrEmpty(dr["Percentage"].ToString().Trim()))
                        drMFReturnsNew["Percentage"] = Double.Parse(dr["Percentage"].ToString());
                    else
                        drMFReturnsNew["Percentage"] = 0;
                   

                    dtMFReturnsNew.Rows.Add(drMFReturnsNew);
                }
                GridBoundColumn TotalPercentage = gvMfReturns.MasterTableView.Columns.FindByUniqueName("Percentage") as GridBoundColumn;
                totalPL = ((totalcurrentvalue - totalinvestedCost) / totalinvestedCost) * 100;
                TotalPercentage.FooterText = totalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                gvMfReturns.DataSource = dtMFReturnsNew;
                gvMfReturns.DataBind();
                //GridFooterItem fitem = (GridFooterItem)gvMfReturns.MasterTableView.GetItems(GridItemType.Footer)[0];
                //Label lbl = (Label)fitem.FindControl("lblTotalPercentage");
                //hdnTotalPL.Value = totalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                //lbl.Text = hdnTotalPL.Value;
                pnlMfReturns.Visible = true;
                gvMfReturns.Visible = true;
                imgMFReturns.Visible = true;
                if (Cache["gvMfReturns" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvMfReturns" + userVo.UserId + userType, dtMFReturnsNew);
                }
                else
                {
                    Cache.Remove("gvMfReturns" + userVo.UserId + userType);
                    Cache.Insert("gvMfReturns" + userVo.UserId + userType, dtMFReturnsNew);
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
            else if (userType == "associates")
            {
                hdnrmId.Value = rmVo.RMId.ToString(); 
                hdnAll.Value = "0";
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

        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
                adviserId = advisorVo.advisorId;
                if (portfolioBo.GetLatestValuationDate(adviserId, Constants.MF.ToString()) != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, Constants.MF.ToString()).ToString());
                }
                genDict.Add(Constants.MFDate.ToString(), MFValuationDate);
                Session[SessionContents.ValuationDate] = genDict;
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
        protected void gvMfReturns_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMfReturns = new DataTable();
            dtMfReturns = (DataTable)Cache["gvMfReturns" + userVo.UserId + userType];
            gvMfReturns.DataSource = dtMfReturns;
            gvMfReturns.Visible = true;

            pnlMfReturns.Visible = true;
            pnlScheme.Visible = false;
            gvMfReturnsScheme.Visible = false;
            imgScheme.Visible = false;
            imgMFReturns.Visible = true;
        }

        protected void imgMFReturns_Click(object sender, ImageClickEventArgs e)
        {
            gvMfReturns.ExportSettings.OpenInNewWindow = true;
            gvMfReturns.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMfReturns.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMfReturns.MasterTableView.ExportToExcel();
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            SetParameters();
            if (ddlType.SelectedValue == "CustomerWise")
                BindMfReturnsGrid();
            else if (ddlType.SelectedValue == "SchemeWise")
                BindMFSchemeWise();

        }

        private void BindMFSchemeWise()
        {
            double totalinvestedCost = 0.0;
            double totalcurrentvalue = 0.0;
            double totalPL = 0.0;
            DataSet dsMfReturnsScheme;
            DataTable dtMfReturnsScheme;
            dsMfReturnsScheme = adviserMFMIS.GetMFReturnsDetails(userType, int.Parse(hdnadviserId.Value), int.Parse(hdnrmId.Value), int.Parse(hdnbranchId.Value), int.Parse(hdnbranchHeadId.Value), int.Parse(hdnAll.Value), strValuationDate, customerid,int.Parse(ddlTypes.SelectedValue));
            dtMfReturnsScheme = dsMfReturnsScheme.Tables[1];
            if (dtMfReturnsScheme == null)
            {
                gvMfReturnsScheme.DataSource = dtMfReturnsScheme;
                gvMfReturnsScheme.DataBind();
            }
            else
            {
                DataTable dtMFReturnsSchemeNew = new DataTable();
                dtMFReturnsSchemeNew.Columns.Add("CustomerId");
                dtMFReturnsSchemeNew.Columns.Add("Customer");
                dtMFReturnsSchemeNew.Columns.Add("Folio");
                dtMFReturnsSchemeNew.Columns.Add("Parent");
                dtMFReturnsSchemeNew.Columns.Add("Branch");
                dtMFReturnsSchemeNew.Columns.Add("RM");
                dtMFReturnsSchemeNew.Columns.Add("Scheme");
                dtMFReturnsSchemeNew.Columns.Add("InvestedCost", typeof(Double));
                dtMFReturnsSchemeNew.Columns.Add("CurrentValue", typeof(Double));
                dtMFReturnsSchemeNew.Columns.Add("ProfitLoss", typeof(Double));
                dtMFReturnsSchemeNew.Columns.Add("Percentage", typeof(Double));
                dtMFReturnsSchemeNew.Columns.Add("accountno");
                dtMFReturnsSchemeNew.Columns.Add("schemecode");

                DataRow drMFReturnsSchemeNew;
                foreach (DataRow dr in dtMfReturnsScheme.Rows)
                {
                    drMFReturnsSchemeNew = dtMFReturnsSchemeNew.NewRow();
                    drMFReturnsSchemeNew["CustomerId"] = dr["CustomerId"].ToString();
                    drMFReturnsSchemeNew["Customer"] = dr["Customer"].ToString();
                    drMFReturnsSchemeNew["Folio"] = dr["CMFA_FolioNum"].ToString();
                    drMFReturnsSchemeNew["Parent"] = dr["Parent"].ToString();
                    drMFReturnsSchemeNew["Branch"] = dr["Branch"].ToString();
                    drMFReturnsSchemeNew["RM"] = dr["RM"].ToString();
                    drMFReturnsSchemeNew["Scheme"] = dr["PASP_SchemePlanName"].ToString();
                    if (!string.IsNullOrEmpty(dr["InvestedCost"].ToString().Trim()))
                        drMFReturnsSchemeNew["InvestedCost"] = Double.Parse(dr["InvestedCost"].ToString());
                    else
                        drMFReturnsSchemeNew["InvestedCost"] = 0;
                    totalinvestedCost = totalinvestedCost + Double.Parse(dr["InvestedCost"].ToString());

                    if (!string.IsNullOrEmpty(dr["CurrentValue"].ToString().Trim()))
                        drMFReturnsSchemeNew["CurrentValue"] = Double.Parse(dr["CurrentValue"].ToString());
                    else
                        drMFReturnsSchemeNew["CurrentValue"] = 0;
                    totalcurrentvalue = totalcurrentvalue + Double.Parse(dr["CurrentValue"].ToString());

                    if (!string.IsNullOrEmpty(dr["ProfitLoss"].ToString().Trim()))
                        drMFReturnsSchemeNew["ProfitLoss"] = dr["ProfitLoss"].ToString();
                    else
                        drMFReturnsSchemeNew["ProfitLoss"] = 0;

                    if (!string.IsNullOrEmpty(dr["Percentage"].ToString().Trim()))
                        drMFReturnsSchemeNew["Percentage"] = Double.Parse(dr["Percentage"].ToString());
                    else
                        drMFReturnsSchemeNew["Percentage"] = 0;
                    if (!string.IsNullOrEmpty(dr["accountno"].ToString().Trim()))
                        drMFReturnsSchemeNew["accountno"] = int.Parse(dr["accountno"].ToString());
                    else
                        drMFReturnsSchemeNew["accountno"] = 0;
                    if (!string.IsNullOrEmpty(dr["schemecode"].ToString().Trim()))
                        drMFReturnsSchemeNew["schemecode"] = int.Parse(dr["schemecode"].ToString());
                    else
                        drMFReturnsSchemeNew["schemecode"] = 0;
                    dtMFReturnsSchemeNew.Rows.Add(drMFReturnsSchemeNew);
                }
                GridBoundColumn TotalPercentage = gvMfReturnsScheme.MasterTableView.Columns.FindByUniqueName("Percentage") as GridBoundColumn;
                totalPL = ((totalcurrentvalue - totalinvestedCost) / totalinvestedCost) * 100;
                TotalPercentage.FooterText = totalPL.ToString("n2", System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN"));
                gvMfReturnsScheme.DataSource = dtMFReturnsSchemeNew;
                gvMfReturnsScheme.DataBind();
                pnlMfReturns.Visible = false;
                pnlScheme.Visible = true;
                gvMfReturnsScheme.Visible = true;
                imgScheme.Visible = true;
                imgMFReturns.Visible = false;
                if (Cache["gvMfReturnsScheme" + userVo.UserId + userType] == null)
                {
                    Cache.Insert("gvMfReturnsScheme" + userVo.UserId + userType, dtMFReturnsSchemeNew);
                }
                else
                {
                    Cache.Remove("gvMfReturnsScheme" + userVo.UserId + userType);
                    Cache.Insert("gvMfReturnsScheme" + userVo.UserId + userType, dtMFReturnsSchemeNew);
                }
            }
        }
        protected void gvMfReturnsScheme_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtMfReturnsScheme = new DataTable();
            dtMfReturnsScheme = (DataTable)Cache["gvMfReturnsScheme" + userVo.UserId + userType];
            gvMfReturnsScheme.DataSource = dtMfReturnsScheme;
            gvMfReturnsScheme.Visible = true;

            pnlMfReturns.Visible = false;
            pnlScheme.Visible = true;
            gvMfReturns.Visible = false;
            imgScheme.Visible = true;
            imgMFReturns.Visible = false;
        }

        protected void imgScheme_Click(object sender, ImageClickEventArgs e)
        {
            gvMfReturnsScheme.ExportSettings.OpenInNewWindow = true;
            gvMfReturnsScheme.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvMfReturnsScheme.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvMfReturnsScheme.MasterTableView.ExportToExcel();
        }
    }
}