using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using BoOnlineOrderManagement;
using VoUser;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Collections.Specialized;
using Telerik.Web.UI;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class OnlineNCDExtract : System.Web.UI.UserControl
    {
        OnlineNCDBackOfficeBo boNcdBackOff = new OnlineNCDBackOfficeBo();

        UserVo userVo = new UserVo();
        AdvisorVo adviserVo = new AdvisorVo();
        DateTime fromdate;
        DateTime todate;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session["userVo"];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                SetDownloadDate();
            }
        }

        //private void Set

        protected void BindNcdExtract()
        {
            DataSet dsGetOnlineNCDExtractPreview;
            DataTable dtGetOnlineNCDExtractPreview;
            
            if (rdpDownloadDate.SelectedDate != null)
                fromdate = DateTime.Parse(rdpDownloadDate.SelectedDate.ToString());
            
            dsGetOnlineNCDExtractPreview = boNcdBackOff.GetOnlineNcdExtractPreview(fromdate, adviserVo.advisorId);
            dtGetOnlineNCDExtractPreview = dsGetOnlineNCDExtractPreview.Tables[0];

            if (Cache["NCDExtract" + userVo.UserId] != null) Cache.Remove("NCDExtract" + userVo.UserId);

            if (dtGetOnlineNCDExtractPreview.Rows.Count > 0) Cache.Insert("NCDExtract" + userVo.UserId, dtGetOnlineNCDExtractPreview);

            gvOnlneNCDExtract.DataSource = dtGetOnlineNCDExtractPreview;
            gvOnlneNCDExtract.DataBind();
        }


        protected void gvOnlneNCDExtract_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsGetOnlineNCDExtractPreview = new DataSet();
            DataTable dtGetOnlineNCDExtractPreview = new DataTable();
            dtGetOnlineNCDExtractPreview = (DataTable)Cache["NCDExtract" + userVo.UserId];

            if (dtGetOnlineNCDExtractPreview != null)
            {
                gvOnlneNCDExtract.DataSource = dtGetOnlineNCDExtractPreview;
            }
        }

        protected void btnNcdExtract_Click(object sender, EventArgs e)
        {
            Page.Validate("NcdExtract");
            if (!Page.IsValid) {
                ShowMessage("Please check all required fields");
                return;
            }
            boNcdBackOff.GenerateOnlineNcdExtract(adviserVo.advisorId, userVo.UserId);

            ShowMessage("Extraction Done");
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Page.Validate("NcdExtract");
            if (!Page.IsValid)
            {
                ShowMessage("Please check all required fields");
                return;
            }
            BindNcdExtract();
        }

        private void SetDownloadDate()
        {
            rdpDownloadDate.SelectedDate = DateTime.Now;
        }

        private void ShowMessage(string msg)
        {
            tblMessage.Visible = true;
            msgRecordStatus.InnerText = msg;
        }

        protected void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvOnlneNCDExtract.ExportSettings.OpenInNewWindow = true;
            gvOnlneNCDExtract.ExportSettings.ExportOnlyData = true;
            gvOnlneNCDExtract.ExportSettings.IgnorePaging = true;
            gvOnlneNCDExtract.ExportSettings.FileName = "NCDExtract";
            gvOnlneNCDExtract.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvOnlneNCDExtract.MasterTableView.ExportToExcel();
        }
    }
}
