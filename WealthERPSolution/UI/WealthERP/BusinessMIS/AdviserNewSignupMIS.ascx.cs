using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BoCommon;
using System.Data;
using BoAdvisorProfiling;
using VoUser;
using WealthERP.Base;
using VOAssociates;

namespace WealthERP.BusinessMIS
{
    public partial class AdviserNewSignupMIS : System.Web.UI.UserControl
    {

        DataSet dsNEWSignupMISDetails = new DataSet();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo adviserVo = new AdvisorVo();
        DateTime dtFromDate = new DateTime();
        DateTime dtToDate = new DateTime();
        AssociatesUserHeirarchyVo associateuserheirarchyVo = new AssociatesUserHeirarchyVo();
        string userType;
        RMVo rmVo = new RMVo();
        string AgentCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            GetUserType();
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";

                ddlTypes.Items[0].Enabled = false;
            }
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                Cache["gvProductWiseCustomersCount"] = ds;

                pnlFolio.Visible = false;
                pnlSIP.Visible = false;
                rdpFromDate.SelectedDate = DateTime.Now.AddMonths(-1);
                rdpToDate.SelectedDate = DateTime.Now.Date;
            }
        }

        public void GetUserType()
        {

            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!string.IsNullOrEmpty(Session[SessionContents.RmVo].ToString()))
                rmVo = (RMVo)Session[SessionContents.RmVo];
            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "bm")
                userType = "bm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "rm")
                userType = "rm";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                associateuserheirarchyVo = (AssociatesUserHeirarchyVo)Session[SessionContents.AssociatesLogin_AssociatesHierarchy];
                if (associateuserheirarchyVo.AgentCode != null)
                {
                    AgentCode = associateuserheirarchyVo.AgentCode.ToString();

                }
                else
                    AgentCode = "0";
            }
        }

        protected void gvProductWiseCustomersCount_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
           
            DataSet dsNEWSignupMISDetails = new DataSet();
            dsNEWSignupMISDetails = (DataSet)Cache["gvProductWiseCustomersCount"];
            if (dsNEWSignupMISDetails.Tables.Count > 0)
                gvProductWiseCustomersCount.DataSource = dsNEWSignupMISDetails;

        }


        protected void btnViewMIS_Click(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "Customer")
            {
                BindNewCustomerSignUpMIS();
                pnlFolio.Visible = false;
                PnlCustomerWise.Visible = true;
                pnlSIP.Visible = false;
            }
            else if (ddlType.SelectedValue == "folio")
            {
                pnlFolio.Visible = true;
                pnlSIP.Visible = false;
                PnlCustomerWise.Visible = false;
                BindFolioSignUp();
            }
            else if (ddlType.SelectedValue == "SIP")
            {
                pnlFolio.Visible = false;
                pnlSIP.Visible = true;

                PnlCustomerWise.Visible = false;
                BindSIPSignUp();
            }
        }

        private void BindSIPSignUp()
        {
            dtFromDate = Convert.ToDateTime(rdpFromDate.SelectedDate);
            dtToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
            DataSet dsSIP;
            dsSIP = advisorBranchBo.GetSIPSignUp(adviserVo.advisorId, dtFromDate, dtToDate, int.Parse(ddlTypes.SelectedValue), userType, AgentCode);
            DataTable dtSIP = dsSIP.Tables[0];
            if (dtSIP == null)
            {
                gvSIP.DataSource = dtSIP;
                gvSIP.DataBind();
            }
            else
            {
                gvSIP.DataSource = dtSIP;
                gvSIP.DataBind();
                btnExportFilteredData.Visible = false;
                btnExportFolio.Visible = false;
                btnExportSIP.Visible = true;
                if (Cache["gvSIP" + adviserVo.advisorId] == null)
                {
                    Cache.Insert("gvSIP" + adviserVo.advisorId, dtSIP);
                }
                else
                {
                    Cache.Remove("gvSIP" + adviserVo.advisorId);
                    Cache.Insert("gvSIP" + adviserVo.advisorId, dtSIP);
                }
            }
        }

        private void BindFolioSignUp()
        {
            dtFromDate = Convert.ToDateTime(rdpFromDate.SelectedDate);
            dtToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
            DataSet dsFolio;
            dsFolio = advisorBranchBo.GetFolioSignUp(adviserVo.advisorId, dtFromDate, dtToDate, int.Parse(ddlTypes.SelectedValue), userType, AgentCode);
            DataTable dtFolio = dsFolio.Tables[0];
            if (dtFolio == null)
            {
                gvFolio.DataSource = dtFolio;
                gvFolio.DataBind();
            }
            else
            {
                gvFolio.DataSource = dtFolio;
                gvFolio.DataBind();
                btnExportFilteredData.Visible = false;
                btnExportFolio.Visible = true;
                btnExportSIP.Visible = false;
                if (Cache["gvFolio" + adviserVo.advisorId] == null)
                {
                    Cache.Insert("gvFolio" + adviserVo.advisorId, dtFolio);
                }
                else
                {
                    Cache.Remove("gvFolio" + adviserVo.advisorId);
                    Cache.Insert("gvFolio" + adviserVo.advisorId, dtFolio);
                }
            }
        }


        public void BindNewCustomerSignUpMIS()
        {
            dtFromDate = Convert.ToDateTime(rdpFromDate.SelectedDate);
            dtToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
            dsNEWSignupMISDetails = new DataSet();

            dsNEWSignupMISDetails = advisorBranchBo.GetNEWSignupMISDetails(adviserVo.advisorId, dtFromDate, dtToDate, int.Parse(ddlTypes.SelectedValue), userType, AgentCode);
            gvProductWiseCustomersCount.DataSource = dsNEWSignupMISDetails;
            gvProductWiseCustomersCount.DataBind();

            if (dsNEWSignupMISDetails.Tables[0].Rows.Count > 0)
            {
                btnExportFilteredData.Visible = true;
                btnExportFolio.Visible = false;
                btnExportSIP.Visible = false;
            }
            else
                btnExportFilteredData.Visible = false;

            if (Cache["gvProductWiseCustomersCount" + adviserVo.advisorId] == null)
            {
                Cache.Insert("gvProductWiseCustomersCount" + adviserVo.advisorId, dsNEWSignupMISDetails.Tables[0]);
            }
            else
            {
                Cache.Remove("gvProductWiseCustomersCount" + adviserVo.advisorId);
                Cache.Insert("gvProductWiseCustomersCount" + adviserVo.advisorId, dsNEWSignupMISDetails.Tables[0]);
            }
            
            
            
            
            //if (Cache["gvProductWiseCustomersCount"] == null)
            //{
            //    Cache.Insert("gvProductWiseCustomersCount", dsNEWSignupMISDetails);
            //}
            //else
            //{
            //    Cache.Remove("gvProductWiseCustomersCount");
            //    Cache.Insert("gvProductWiseCustomersCount", dsNEWSignupMISDetails);
            //}

        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtgvProductWiseCustomersCount = new DataSet();
            dtgvProductWiseCustomersCount = (DataSet)Cache["gvProductWiseCustomersCount"];
            gvProductWiseCustomersCount.DataSource = dtgvProductWiseCustomersCount;

            gvProductWiseCustomersCount.ExportSettings.OpenInNewWindow = true;
            gvProductWiseCustomersCount.ExportSettings.IgnorePaging = true;
            gvProductWiseCustomersCount.ExportSettings.HideStructureColumns = true;
            gvProductWiseCustomersCount.ExportSettings.ExportOnlyData = true;
            gvProductWiseCustomersCount.ExportSettings.FileName = "NEW customer Signup Details";
            gvProductWiseCustomersCount.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvProductWiseCustomersCount.MasterTableView.ExportToExcel();
        }
        protected void gvSIP_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtSIP = new DataTable();
            dtSIP = (DataTable)Cache["gvSIP" + adviserVo.advisorId];
            gvSIP.Visible = true;
            this.gvSIP.DataSource = dtSIP;
        }
        protected void gvFolio_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            DataTable dtFolio = new DataTable();
            dtFolio = (DataTable)Cache["gvFolio" + adviserVo.advisorId];
            gvFolio.Visible = true;
            this.gvFolio.DataSource = dtFolio;

        }

        protected void gvProductWiseCustomersCount_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (ddlTypes.SelectedValue == "1")
                {
                    gvProductWiseCustomersCount.MasterTableView.GetColumn("AAC_AgentCode").Visible = false;
                    gvProductWiseCustomersCount.MasterTableView.GetColumn("PAISC_AssetInstrumentSubCategoryName").Visible = false;

                }
                else
                {
                    //PnlCustomerWise.Width = "70%";
                    gvProductWiseCustomersCount.MasterTableView.GetColumn("AAC_AgentCode").Visible = true;
                    gvProductWiseCustomersCount.MasterTableView.GetColumn("PAISC_AssetInstrumentSubCategoryName").Visible = true;

                }
            }
        }

        protected void btnExportFolio_Click(object sender, ImageClickEventArgs e)
        {
            gvFolio.ExportSettings.OpenInNewWindow = true;
            gvFolio.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvFolio.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvFolio.MasterTableView.ExportToExcel();
        }

        protected void btnExportSIP_Click(object sender, ImageClickEventArgs e)
        {
            gvSIP.ExportSettings.OpenInNewWindow = true;
            gvSIP.ExportSettings.IgnorePaging = true;
            foreach (GridFilteringItem filter in gvSIP.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                filter.Visible = false;
            }
            gvSIP.MasterTableView.ExportToExcel();
        }
    }
}