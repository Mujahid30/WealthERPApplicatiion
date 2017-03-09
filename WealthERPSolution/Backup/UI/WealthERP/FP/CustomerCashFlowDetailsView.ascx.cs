using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BoFPSuperlite;
using VoUser;
using System.Data;
using Telerik.Web.UI;

namespace WealthERP.FP
{
    public partial class CustomerCashFlowDetailsView : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        CustomerVo customerVo;
        UserVo userVo = new UserVo();
        CustomerFPAnalyticsBo ObjcustomerFPAnalyticsbo = new CustomerFPAnalyticsBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVo = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session["userVo"];
            if (!IsPostBack)
            {
              
                BindCashFlowAfterRetirement();
            }
        }

        private void BindCashFlowAfterRetirement()
        {
            DataTable dt = ObjcustomerFPAnalyticsbo.GetCustomerCashFlowAfterRetirement(customerVo.CustomerId);
            gvRetCashFlowList.Visible = true;
            gvRetCashFlowList.DataSource = dt;
            gvRetCashFlowList.DataBind();
            if (Cache["gvRetCashFlowList" + userVo.UserId.ToString()] == null)
            {
                Cache.Insert("gvRetCashFlowList" + userVo.UserId.ToString(), dt);
            }
            else
            {
                Cache.Remove("gvRetCashFlowList" + userVo.UserId.ToString());
                Cache.Insert("gvRetCashFlowList" + userVo.UserId.ToString(), dt);
            }
        }
        protected void gvRetCashFlowList_OnNeedDataSource(Object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvRetCashFlowList" + userVo.UserId.ToString()];
            gvRetCashFlowList.DataSource = dt;
        }

        private void BindCashFlow()
        {
            DataTable dt = ObjcustomerFPAnalyticsbo.GetCustomerCashFlow(customerVo.CustomerId, chkincludeSpouse.Checked);
            gvCashFlowList.Visible = true;
            gvCashFlowList.DataSource = dt;
            gvCashFlowList.DataBind();
            if (Cache["gvCashFlowList" + userVo.UserId.ToString()] == null)
            {
                Cache.Insert("gvCashFlowList" + userVo.UserId.ToString(), dt);
            }
            else
            {
                Cache.Remove("gvCashFlowList" + userVo.UserId.ToString());
                Cache.Insert("gvCashFlowList" + userVo.UserId.ToString(), dt);
            }
        }
        protected void gvCashFlowList_OnNeedDataSource(Object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Cache["gvCashFlowList" + userVo.UserId.ToString()];
            gvCashFlowList.DataSource = dt;
        }
        public void btnExportgvCashFlowList_OnClick(object sender, ImageClickEventArgs e)
        {
            gvCashFlowList.ExportSettings.OpenInNewWindow = true;
            gvCashFlowList.ExportSettings.IgnorePaging = true;
            gvCashFlowList.ExportSettings.HideStructureColumns = true;
            gvCashFlowList.ExportSettings.ExportOnlyData = true;
            gvCashFlowList.ExportSettings.FileName = "CashFlow" + customerVo.FirstName;
            gvCashFlowList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCashFlowList.MasterTableView.ExportToExcel();
        }
        public void btnExportgvRetCashFlowList_OnClick(object sender, ImageClickEventArgs e)
        {
            gvRetCashFlowList.ExportSettings.OpenInNewWindow = true;
            gvRetCashFlowList.ExportSettings.IgnorePaging = true;
            gvRetCashFlowList.ExportSettings.HideStructureColumns = true;
            gvRetCashFlowList.ExportSettings.ExportOnlyData = true;
            gvRetCashFlowList.ExportSettings.FileName = "CashFlowAfterRetirement" + customerVo.FirstName;
            gvRetCashFlowList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvRetCashFlowList.MasterTableView.ExportToExcel();
        }
        /* *** XML SetUp Code Starts here for Fusion Chart Implementation by (Vinayak Patil)  *** */

        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            BindCashFlow();
        }
    }
}