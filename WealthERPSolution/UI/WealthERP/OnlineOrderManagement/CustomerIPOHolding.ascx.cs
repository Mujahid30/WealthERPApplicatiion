using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoUser;
using VoUser;
using WealthERP.Base;
using BoCommon;
using BoOnlineOrderManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Data;


namespace WealthERP.OnlineOrderManagement
{
    public partial class CustomerIPOHolding : System.Web.UI.UserControl
    {
        UserVo userVo;
        CustomerVo customerVo;
        OnlineIPOOrderBo OnlineIPOOrderBo = new OnlineIPOOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session[SessionContents.UserVo];
            customerVo = (CustomerVo)Session["customerVo"];
            BindIPOHolding();
        }
        /// <summary>
        /// Get IPO Holding
        /// </summary>
        protected void BindIPOHolding()
        {
            DataTable dtIPOHolding;
            dtIPOHolding = OnlineIPOOrderBo.GetIPOHolding(customerVo.CustomerId);
            if (dtIPOHolding.Rows.Count >= 0)
            {
                if (Cache["IPOHoldingList" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("IPOHoldingList" + userVo.UserId.ToString(), dtIPOHolding);
                }
                else
                {
                    Cache.Remove("IPOHoldingList" + userVo.UserId.ToString());
                    Cache.Insert("IPOHoldingList" + userVo.UserId.ToString(), dtIPOHolding);
                }
                gvIPOHolding.DataSource = dtIPOHolding;
                gvIPOHolding.DataBind();
                ibtExportSummary.Visible = true;
                pnlGrid.Visible = true;
            }
            else
            {
                ibtExportSummary.Visible = false;
                gvIPOHolding.DataSource = dtIPOHolding;
                gvIPOHolding.DataBind();
                pnlGrid.Visible = true;
            }
        }
        protected void gvIPOHolding_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }
        public void gvIPOHolding_OnItemDataBound(object sender, GridItemEventArgs e)
        {
        }
        protected void gvIPOHolding_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtIPOHoldingOrder;
            dtIPOHoldingOrder = (DataTable)Cache["IPOHoldingList" + userVo.UserId.ToString()];
            if (dtIPOHoldingOrder != null)
            {
                gvIPOHolding.DataSource = dtIPOHoldingOrder;
            }
        }
        public void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            gvIPOHolding.ExportSettings.OpenInNewWindow = true;
            gvIPOHolding.ExportSettings.IgnorePaging = true;
            gvIPOHolding.ExportSettings.HideStructureColumns = true;
            gvIPOHolding.ExportSettings.ExportOnlyData = true;
            gvIPOHolding.ExportSettings.FileName = "IPO Holding";
            gvIPOHolding.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvIPOHolding.MasterTableView.ExportToExcel();

        }
    }
}