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
        DateTime dtFromDate=new DateTime();
        DateTime dtToDate = new DateTime();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            adviserVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                Cache["gvSchemeDetailsForMappinginSuperAdmin"] =ds;
            

                rdpFromDate.SelectedDate = DateTime.Now.AddMonths(-1);
                rdpToDate.SelectedDate = DateTime.Now.Date;
            
            }

        }

        protected void gvNewCustomerSignUpMIS_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsNEWSignupMISDetails = new DataSet();
            dsNEWSignupMISDetails = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            if(dsNEWSignupMISDetails.Tables.Count>0)
            gvNewCustomerSignUpMIS.DataSource = dsNEWSignupMISDetails;
            
        }


        protected void btnViewMIS_Click(object sender, EventArgs e)
        {
            BindNewCustomerSignUpMIS();
        }


        public void BindNewCustomerSignUpMIS()
        {
            dtFromDate=Convert.ToDateTime(rdpFromDate.SelectedDate);
            dtToDate = Convert.ToDateTime(rdpToDate.SelectedDate);
            dsNEWSignupMISDetails = new DataSet();
            
            dsNEWSignupMISDetails = advisorBranchBo.GetNEWSignupMISDetails(adviserVo.advisorId,dtFromDate,dtToDate);
            gvNewCustomerSignUpMIS.DataSource = dsNEWSignupMISDetails;
            gvNewCustomerSignUpMIS.DataBind();
            if (dsNEWSignupMISDetails != null)
                btnExportFilteredData.Visible = true;
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
    }
}