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
            userVo = (UserVo)Session[SessionContents.UserVo];
            BindBHGV();
        }

        protected void BindBHGV()
        {
            DataSet dsbondsHolding = BoOnlineBondOrder.getBondsBookview(4);

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

        protected void ddlMenu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //string sActionName = ((DropDownList)sender).SelectedItem.Text;
            //string sStructId = ((DropDownList)sender).SelectedValue;

            RadComboBox ddlAction = (RadComboBox)sender;
            //GridDataItem item = (GridDataItem)ddlAction.NamingContainer;
            //int structureId = int.Parse(gvBHList.MasterTableView.DataKeyValues[item.ItemIndex]["StructureId"].ToString());
            //string prodType = this.ddProduct.SelectedValue;

            switch (ddlAction.SelectedValue)
            {
                case "Edit":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=1');", true);
                    break;
                case "Cancel":
                   // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=1');", true);
                    break;
                default:
                    return;
            }
        }

        protected void gvBHList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvBHList.CurrentPageIndex = e.NewPageIndex;
            BindBHGV();
        }
    }
}