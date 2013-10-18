using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Telerik.Web.UI;
using System.Data;
using VoUser;



namespace WealthERP.OnlineOrderManagement
{
    public partial class OnlineOrderExtract : System.Web.UI.UserControl
    {
        OnlineMFOrderBo boOnlineOrder = new OnlineMFOrderBo();
        OnlineMFOrderVo onlineMFOrderVo = new OnlineMFOrderVo();
        AdvisorVo advisorVo;
        DataTable   dtOrderMIS=new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["advisorVo"].ToString()))
                advisorVo = (AdvisorVo)Session["advisorVo"];
        }
        protected void btnGenerateFile_Click(object sender, EventArgs e)
        {

        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            gvExtractMIS.ExportSettings.OpenInNewWindow = true;
            gvExtractMIS.ExportSettings.OpenInNewWindow = true;
            gvExtractMIS.ExportSettings.IgnorePaging = true;
            gvExtractMIS.ExportSettings.HideStructureColumns = true;
            gvExtractMIS.ExportSettings.ExportOnlyData = true;
            gvExtractMIS.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvExtractMIS.MasterTableView.ExportToExcel();
        }
        protected void btnExtract_Click(object sender, EventArgs e)
        {
            BindMISGridView();
        }
        protected void gvExtractMIS_ItemDataBound(object sender, GridItemEventArgs e)
        {
           
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;

            //    Label lblordertype = dataItem.FindControl("lblOrderType") as Label;
            //    string ordertype = null;
            //    ordertype = lblordertype.Text;
            //    if (ordertype == "1")
            //        lblordertype.Text = "Immediate";
            //    else
            //        lblordertype.Text = "Future";
            //}

        }
        protected void gvExtractMIS_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //DataTable dtOrderMIS = new DataTable();
            //dtOrderMIS = (DataTable)Cache["OrderMIS" + userVo.UserId];

            DataTable dtOrderMIS = new DataTable();
            dtOrderMIS = (DataTable)Cache["OrderMIS"];            
            gvExtractMIS.DataSource = dtOrderMIS;
            gvExtractMIS.Visible = true;
           
        }
        private void BindMISGridView()
        {
            DataSet dsOrderMIS;

            dsOrderMIS = boOnlineOrder.GetMfOrderExtract(txtFrom.SelectedDate.Value, advisorVo.advisorId, ddlExtractType.SelectedValue);
            dtOrderMIS = dsOrderMIS.Tables[0];
            if (dtOrderMIS.Rows.Count > 0)
            {
                //lblTotalRows.Text = hdnRecordCount.Value = count.ToString();
                gvExtractMIS.DataSource = dtOrderMIS;
                gvExtractMIS.DataBind();
                gvExtractMIS.Visible = true;
            }
            Cache.Remove("OrderMIS");
            if (Cache["OrderMIS"] == null)
            {
                Cache.Insert("OrderMIS", dtOrderMIS);
            }
        }

        protected void btnExportFilteredData_OnClick(object sender, EventArgs e)
        {
           
        }
    }
}