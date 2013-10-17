using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VoUser;
using WealthERP.Base;
using System.Data;

namespace WealthERP.OnlineOrderManagement
{
    public partial class NCDIssueHoldings : System.Web.UI.UserControl
    {
        UserVo userVo;
        BoOnlineOrderManagement.OnlineBondOrderBo BoOnlineBondOrder = new BoOnlineOrderManagement.OnlineBondOrderBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CusstId"] = "ESI123456";
            userVo = (UserVo)Session[SessionContents.UserVo];
            string CustId = Session["CusstId"].ToString();
            BindBHGV(4, CustId);
        }
        protected void lbBuySell_Click(object sender, EventArgs e)
        {
            string CustId = Session["CustId"].ToString();
            int rowindex1 = ((GridDataItem)((LinkButton)sender).NamingContainer).RowIndex;
            int rowindex = (rowindex1 / 2) - 1;
            LinkButton lbButton = (LinkButton)sender;
            GridDataItem item = (GridDataItem)lbButton.NamingContainer;
            string IssuerId = gvBHList.MasterTableView.DataKeyValues[rowindex]["BHScrip"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TransactionPage", "loadcontrol('NCDIssueTransact','CustId=" + CustId + "'&'IssuerId=" + IssuerId + "');", true);
        }
        protected void BindBHGV(int Type, string CustId)
        {
            DataSet dsbondsHolding = BoOnlineBondOrder.getBondsBookview(Type, CustId);

            if (dsbondsHolding.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;

            gvBHList.DataSource = dsbondsHolding;
            gvBHList.DataBind();

            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsbondsHolding.Tables[0]);
        }
        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dtCommMgmt = new DataTable();
            dtCommMgmt = (DataTable)Cache[userVo.UserId.ToString() + "CommissionStructureRule"];
            if (dtCommMgmt == null)
                return;
            else if (dtCommMgmt.Rows.Count < 1)
                return;
            gvBHList.DataSource = dtCommMgmt;
            gvBHList.ExportSettings.OpenInNewWindow = true;
            gvBHList.ExportSettings.IgnorePaging = true;
            gvBHList.ExportSettings.HideStructureColumns = true;
            gvBHList.ExportSettings.ExportOnlyData = true;
            gvBHList.ExportSettings.FileName = "Details";
            gvBHList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBHList.MasterTableView.ExportToExcel();
            //BindStructureRuleGrid();
        }

    }
}