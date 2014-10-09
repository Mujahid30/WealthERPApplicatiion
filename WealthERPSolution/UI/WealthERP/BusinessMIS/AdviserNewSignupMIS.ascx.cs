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

namespace WealthERP.BusinessMIS
{
    public partial class AdviserNewSignupMIS : System.Web.UI.UserControl
    {

        DataSet dsNEWSignupMISDetails = new DataSet();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorVo adviserVo = new AdvisorVo();
        DateTime dtFromDate = new DateTime();
        DateTime dtToDate = new DateTime();

        string userType;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];

            if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "admin" || Session[SessionContents.CurrentUserRole].ToString().ToLower() == "ops")
                userType = "advisor";
            else if (Session[SessionContents.CurrentUserRole].ToString().ToLower() == "associates")
            {
                userType = "associates";
                lblTypes.Visible = false;
                ddlTypes.Visible = false;
            }
            else
                userType = Session[SessionContents.CurrentUserRole].ToString().ToLower();

            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                Cache["gvSchemeDetailsForMappinginSuperAdmin"] =ds;

                pnlFolio.Visible = false;
                pnlSIP.Visible = false;
                rdpFromDate.SelectedDate = DateTime.Now.AddMonths(-1);
                rdpToDate.SelectedDate = DateTime.Now.Date;
            
            }

        }

        protected void gvNewCustomerSignUpMIS_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsNEWSignupMISDetails = new DataSet();
            dsNEWSignupMISDetails = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            if (dsNEWSignupMISDetails.Tables.Count > 0)
                gvNewCustomerSignUpMIS.DataSource = dsNEWSignupMISDetails;

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
            dsSIP = advisorBranchBo.GetSIPSignUp(adviserVo.advisorId, dtFromDate, dtToDate, int.Parse(ddlTypes.SelectedValue));
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
            dsFolio = advisorBranchBo.GetFolioSignUp(adviserVo.advisorId, dtFromDate, dtToDate, int.Parse(ddlTypes.SelectedValue));
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

            dsNEWSignupMISDetails = advisorBranchBo.GetNEWSignupMISDetails(adviserVo.advisorId, dtFromDate, dtToDate, int.Parse(ddlTypes.SelectedValue));
            gvNewCustomerSignUpMIS.DataSource = dsNEWSignupMISDetails;
            gvNewCustomerSignUpMIS.DataBind();
            if (dsNEWSignupMISDetails.Tables[0].Rows.Count > 0)
            {
                btnExportFilteredData.Visible = true;
                btnExportFolio.Visible = false;
                btnExportSIP.Visible = false;
            }
            else
                btnExportFilteredData.Visible = false;
            if (Cache["gvSchemeDetailsForMappinginSuperAdmin"] == null)
            {
                Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsNEWSignupMISDetails);
            }
            else
            {
                Cache.Remove("gvSchemeDetailsForMappinginSuperAdmin");
                Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsNEWSignupMISDetails);
            }

        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            DataSet dtGvNewCustomerSignUpMIS = new DataSet();
            dtGvNewCustomerSignUpMIS = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            gvNewCustomerSignUpMIS.DataSource = dtGvNewCustomerSignUpMIS;

            gvNewCustomerSignUpMIS.ExportSettings.OpenInNewWindow = true;
            gvNewCustomerSignUpMIS.ExportSettings.IgnorePaging = true;
            gvNewCustomerSignUpMIS.ExportSettings.HideStructureColumns = true;
            gvNewCustomerSignUpMIS.ExportSettings.ExportOnlyData = true;
            gvNewCustomerSignUpMIS.ExportSettings.FileName = "NEW customer Signup Details";
            gvNewCustomerSignUpMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvNewCustomerSignUpMIS.MasterTableView.ExportToExcel();
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

        protected void gvNewCustomerSignUpMIS_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                if (!Convert.ToBoolean(adviserVo.MultiBranch))
                {
                    if (adviserVo.MultiBranch != 1)
                    {
                        gvNewCustomerSignUpMIS.MasterTableView.GetColumn("ZoneName").Visible = false;
                        gvNewCustomerSignUpMIS.MasterTableView.GetColumn("ClusterName").Visible = false;
                    }
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