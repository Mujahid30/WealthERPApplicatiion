using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VoUser;
using BoOfflineOrderManagement;
using BoCommon;
using WealthERP.Base;
using System.Data;
namespace WealthERP.OffLineOrderManagement
{
    public partial class CustomerOfflineBondOrderView : System.Web.UI.UserControl
    {

        AdvisorVo advisorVo;
        CustomerVo customerVO = new CustomerVo();
        UserVo userVo;
        OfflineBondOrderBo OfflineBondOrderBo = new OfflineBondOrderBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session["advisorVo"];
            customerVO = (CustomerVo)Session["customerVo"];
            userVo = (UserVo)Session[SessionContents.UserVo];
            if (!IsPostBack)
            {
                BindFixedIncomeList(); 
            }
        }
        protected void BindFixedIncomeList()
        {
            DataSet ds = OfflineBondOrderBo.GetCustomerAllotedData(customerVO.CustomerId);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //Cache.Remove("BondOrderBookList" + userVo.UserId.ToString());
            //Cache.Insert("BondOrderBookList" + userVo.UserId.ToString(), ds.Tables[0]);
                pnlGrid.Visible = true;
                imgexportButton.Visible = true;
                gvBondOrderList.DataSource = ds.Tables[0];
                gvBondOrderList.DataBind();
            //}
        }
        protected void gvBondOrderList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtOrder;
            dtOrder = (DataTable)Cache["BondOrderBookList" + userVo.UserId.ToString()];
            if (dtOrder != null)
            {
                gvBondOrderList.DataSource = dtOrder;
            }

        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            Int32 COAD_Id = Convert.ToInt32(gvBondOrderList.MasterTableView.DataKeyValues[gvr.ItemIndex]["COAD_Id"].ToString());
            string category = gvBondOrderList.MasterTableView.DataKeyValues[gvr.ItemIndex]["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncome54ECOrderEntry", "loadcontrol( 'CustomerOfflineBondOrder','action=" + ddlAction.SelectedItem.Value.ToString() + "&COADID=" + COAD_Id + "&Category=" + category + "');", true);
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvBondOrderList.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvBondOrderList.ExportSettings.OpenInNewWindow = true;
            gvBondOrderList.ExportSettings.IgnorePaging = true;
            gvBondOrderList.ExportSettings.HideStructureColumns = true;
            gvBondOrderList.ExportSettings.ExportOnlyData = true;
            gvBondOrderList.ExportSettings.FileName = "Bond";
            gvBondOrderList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvBondOrderList.MasterTableView.ExportToExcel();

        }
    }
}