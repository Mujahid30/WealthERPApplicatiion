using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using BoCustomerPortfolio;
using WealthERP.Base;
using VoUser;
using Telerik.Web.UI;

namespace WealthERP.OfflineOrderBackOffice
{
    public partial class InsuranceIssueList : System.Web.UI.UserControl
    {
        AssetBo assetBo = new AssetBo(); UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
        }
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory(ddlType.SelectedValue);

        }
        public void LoadCategory(string productType)
        {
            try
            {
                DataSet ds = assetBo.GetAssetInstrumentCategory(productType);
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataTextField = "PAIC_AssetInstrumentCategoryName";
                ddlCategory.DataValueField = "PAIC_AssetInstrumentCategoryCode";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            try
            {
                InsuranceBo InsuranceBo = new InsuranceBo();
                DataTable dt = InsuranceBo.GetIssueList(ddlType.SelectedValue, ddlCategory.SelectedValue, int.Parse(ddlStatus.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    Cache.Remove("InsuranceList" + userVo.UserId.ToString());
                    Cache.Insert("InsuranceList" + userVo.UserId.ToString(), dt);
                    gvInsuranceList.DataSource = dt;
                    gvInsuranceList.DataBind();
                    pnlGrid.Visible = true;
                    imgexportButton.Visible = true;
                }
                else
                {
                    gvInsuranceList.DataSource = dt;
                    gvInsuranceList.DataBind();
                    pnlGrid.Visible = true;
                    imgexportButton.Visible = false;

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        protected void gvInsuranceList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable InsuranceList;
            InsuranceList = (DataTable)Cache["InsuranceList" + userVo.UserId.ToString()];
            if (InsuranceList != null)
            {
                gvInsuranceList.DataSource = InsuranceList;
            }

        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvInsuranceList.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvInsuranceList.ExportSettings.OpenInNewWindow = true;
            gvInsuranceList.ExportSettings.IgnorePaging = true;
            gvInsuranceList.ExportSettings.HideStructureColumns = true;
            gvInsuranceList.ExportSettings.ExportOnlyData = true;
            gvInsuranceList.ExportSettings.FileName = "Insurance Issue List";
            gvInsuranceList.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvInsuranceList.MasterTableView.ExportToExcel();

        }
        protected void ddlAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem gvr = (GridDataItem)ddlAction.NamingContainer;
            Int32 IssueID = Convert.ToInt32(gvInsuranceList.MasterTableView.DataKeyValues[gvr.ItemIndex]["IS_SchemeId"].ToString());
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "FixedIncome54ECOrderEntry", "loadcontrol( 'InsuranceIssueSetup','action=" + ddlAction.SelectedItem.Value.ToString() + "&IssueID=" + IssueID + "');", true);
        }
    }
}