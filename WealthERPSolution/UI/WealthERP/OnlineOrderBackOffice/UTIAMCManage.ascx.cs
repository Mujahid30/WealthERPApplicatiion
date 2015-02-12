using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using VoUser;
using BoOnlineOrderManagement;
using WealthERP.Base;
using BoCommon;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class UTIAMCManage : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        DateTime fromDate;
        DateTime toDate;
        OnlineOrderBackOfficeBo OnlineOrderBackOfficeBo = new OnlineOrderBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (!IsPostBack)
            {
                fromDate = DateTime.Now.AddMonths(-1);
                txtOrderFrom.SelectedDate = fromDate.Date;
                txtOrderTo.SelectedDate = DateTime.Now;
            }
        }
        protected void BindUTIAMC()
        {
            try
            {
                DataTable dtGetUTIAMCDetails = OnlineOrderBackOfficeBo.GetUTIAMCDetails(advisorVo.advisorId, Convert.ToDateTime(txtOrderFrom.SelectedDate), Convert.ToDateTime(txtOrderTo.SelectedDate));

                if (Cache["UTIAMCList" + advisorVo.advisorId] != null)
                {
                    Cache.Remove("UTIAMCList" + advisorVo.advisorId);
                }

                Cache.Insert("UTIAMCList" + advisorVo.advisorId, dtGetUTIAMCDetails);
                gvUATAMCList.DataSource = dtGetUTIAMCDetails;
                gvUATAMCList.DataBind();
                pnlSeries.Visible = true;
                imgexportButton.Visible = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeMIS.ascx.cs:btnExportData_OnClick()");
                object[] objects = new object[4];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        protected void gvAdviserList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtGetUTIAMCDetails = (DataTable)Cache["UTIAMCList" + advisorVo.advisorId];
            if (dtGetUTIAMCDetails != null)
            {
                gvUATAMCList.DataSource = dtGetUTIAMCDetails;
            }
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            BindUTIAMC();
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {

            gvUATAMCList.ExportSettings.OpenInNewWindow = true;
            gvUATAMCList.ExportSettings.IgnorePaging = true;
            gvUATAMCList.ExportSettings.HideStructureColumns = true;
            gvUATAMCList.ExportSettings.ExportOnlyData = true;
            gvUATAMCList.ExportSettings.FileName = "UTI AMC List";
            gvUATAMCList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvUATAMCList.MasterTableView.ExportToExcel();
        }
    }
}