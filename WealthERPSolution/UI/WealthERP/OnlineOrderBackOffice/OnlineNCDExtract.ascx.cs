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
        OnlineOrderBackOfficeBo onlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
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
                //DateTime fromDate = DateTime.Now.AddMonths(-1);
                //txtNFOStartDate.SelectedDate = fromDate.Date;
                //txtNFOStartDate.SelectedDate = DateTime.Now;
            }
        }

        protected void BindNCDExtract()
        {
            DataSet dsGetOnlineNCDExtractPreview = new DataSet();
            DataTable dtGetOnlineNCDExtractPreview = new DataTable();
            if (txtNFOStartDate.SelectedDate != null)
                fromdate = DateTime.Parse(txtNFOStartDate.SelectedDate.ToString());
            dsGetOnlineNCDExtractPreview = onlineOrderBackOfficeBo.GetOnlineNCDExtractPreview(fromdate);
            dtGetOnlineNCDExtractPreview = dsGetOnlineNCDExtractPreview.Tables[0];
            if (dtGetOnlineNCDExtractPreview.Rows.Count > 0)
            {
                if (Cache["NCDExtract" + adviserVo.advisorId] == null)
                {
                    Cache.Insert("NCDExtract" + adviserVo.advisorId, dtGetOnlineNCDExtractPreview);
                }
                else
                {
                    Cache.Remove("NCDExtract" + adviserVo.advisorId);
                    Cache.Insert("NCDExtract" + adviserVo.advisorId, dtGetOnlineNCDExtractPreview);
                }
                gvOnlneNCDExtract.DataSource = dtGetOnlineNCDExtractPreview;
                gvOnlneNCDExtract.DataBind();
            }
            else
            {
                gvOnlneNCDExtract.DataSource = dtGetOnlineNCDExtractPreview;
                gvOnlneNCDExtract.DataBind();
            }
        }
        protected void btngo_Click(object sender, EventArgs e)
        {
            BindNCDExtract();
        }

        protected void gvOnlneNCDExtract_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet dsGetOnlineNCDExtractPreview = new DataSet();
            DataTable dtGetOnlineNCDExtractPreview = new DataTable();
            dtGetOnlineNCDExtractPreview = (DataTable)Cache["NCDExtract" + adviserVo.advisorId];

            if (dtGetOnlineNCDExtractPreview != null)
            {
                gvOnlneNCDExtract.DataSource = dtGetOnlineNCDExtractPreview;
            }
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
