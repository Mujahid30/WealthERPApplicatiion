using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BoOnlineOrderManagement;
using BoCommon;
using VoUser;
using System.Data;
using WealthERP.Base;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineAdviserCustomerRMSLog : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo;
        UserVo userVo;
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                txtRMSLogFrom.SelectedDate = DateTime.Now;
                txtRMSLogTo.SelectedDate = DateTime.Now;
            }
        }
        protected void ddlProduct_Selectedindexchanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue == "MF")
            {
                lblOrderType.Visible = true;
                ddlOrderType.Visible = true;
            }
            else
            {
                lblOrderType.Visible = false;
                ddlOrderType.Visible = false;
            }
        }
        protected void btnViewRMSLog_click(object sender, EventArgs e)
        {
            BindRMSLog();
        }
        protected void BindRMSLog()
        {
            DataTable dtBindRMSLog = OnlineOrderBackOfficeBo.CustomerGetRMSLog(Convert.ToDateTime(txtRMSLogFrom.SelectedDate), Convert.ToDateTime(txtRMSLogTo.SelectedDate), advisorVo.advisorId,ddlProduct.SelectedValue,(ddlOrderType.SelectedValue=="MF")?ddlOrderType.SelectedValue:null);
            if (Cache["RMSLog" + userVo.UserId.ToString()] != null)
            {
                Cache.Remove("RMSLog" + userVo.UserId.ToString());
            }
            Cache.Insert("RMSLog" + userVo.UserId.ToString(), dtBindRMSLog);

            gvRMSLog.DataSource = dtBindRMSLog;
            gvRMSLog.DataBind();
            pnlRMSLog.Visible = true;
            imgexportButton.Visible = true;

        }
        protected void gvCommMgmt_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindRMSLog;
            dtBindRMSLog = (DataTable)Cache["RMSLog" + userVo.UserId.ToString()];
            if (dtBindRMSLog != null)
            {
                gvRMSLog.DataSource = dtBindRMSLog;
            }

        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvRMSLog.ExportSettings.OpenInNewWindow = true;
            gvRMSLog.ExportSettings.IgnorePaging = true;
            //gvOrderBookMIS.MasterTableView.GetColumn("C_CustCode").Display = true;
            gvRMSLog.ExportSettings.ExportOnlyData = true;
            gvRMSLog.ExportSettings.FileName = "RMS Details";
            gvRMSLog.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvRMSLog.MasterTableView.ExportToExcel();
        } 

    }
}