using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using BoWerpAdmin;
using Telerik.Web.UI;


namespace WealthERP.CommisionManagement
{
    public partial class ViewSchemeStructureAssociation : System.Web.UI.UserControl
    {
        PriceBo priceBo = new PriceBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = priceBo.GetStructureoverScheme();
            if (dt.Rows.Count > 0)
            {
                ibtExportSummary.Visible = true;
                RadGrid1.DataSource = dt;
                RadGrid1.DataBind();
            }
            else
            {
                ibtExportSummary.Visible = false;
                RadGrid1.DataSource = null;
            }
        }
        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.HideStructureColumns = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.FileName = "ViewSchemeStructureAssociation";
            RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            RadGrid1.MasterTableView.ExportToExcel();
            //BindStructureRuleGrid();
        }
    }
}