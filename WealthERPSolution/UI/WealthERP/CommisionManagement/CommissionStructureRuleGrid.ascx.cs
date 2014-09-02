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
using BoCommisionManagement;
using VoCommisionManagement;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace WealthERP.CommisionManagement
{
    public partial class CommissionStructureRuleGrid : System.Web.UI.UserControl
    {
        UserVo userVo;
        AdvisorVo advisorVo;
        RMVo rmVo;
        CommisionReceivableBo commisionReceivableBo = new CommisionReceivableBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userVo = (UserVo)Session[SessionContents.UserVo];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            rmVo = (RMVo)Session["rmVo"];

            ibtExportSummary.Visible = false;

            if (!IsPostBack) {
                pnlGrid.Visible = false;

                BindProductDropdown();
                ddProduct.SelectedValue = "MF";
                ddProduct.Enabled = false;

                BindCategoryDropdown(ddProduct.SelectedValue);
                BindIssuerDropdown();
            }            
        }
        
        private void bindDropdown(DropDownList ddList, DataSet dsListItems) {

        }

        private void BindProductDropdown()
        {
            DataSet dsLookupData = commisionReceivableBo.GetProductType();

            //Populating the product dropdown
            DataRow drProduct = dsLookupData.Tables[0].NewRow();
            drProduct["PAG_AssetGroupCode"] = "All";
            drProduct["PAG_AssetGroupName"] = "All";
            dsLookupData.Tables[0].Rows.InsertAt(drProduct, 0);
            ddProduct.DataSource = dsLookupData.Tables[0];
            ddProduct.DataValueField = dsLookupData.Tables[0].Columns["PAG_AssetGroupCode"].ToString();
            ddProduct.DataTextField = dsLookupData.Tables[0].Columns["PAG_AssetGroupName"].ToString();
            ddProduct.DataBind();
        }

        private void BindCategoryDropdown(string prodType)
        {
            DataSet dsLookupData = commisionReceivableBo.GetCategories(prodType);

            //Populating the categories dropdown
            DataRow drProduct = dsLookupData.Tables[0].NewRow();
            drProduct["PAIC_AssetInstrumentCategoryCode"] = "All";
            drProduct["PAIC_AssetInstrumentCategoryName"] = "All";
            dsLookupData.Tables[0].Rows.InsertAt(drProduct, 0);
            ddCategory.DataSource = dsLookupData.Tables[0];
            ddCategory.DataValueField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryCode"].ToString();
            ddCategory.DataTextField = dsLookupData.Tables[0].Columns["PAIC_AssetInstrumentCategoryName"].ToString();
            ddCategory.DataBind();
        }

        private void BindSubCategoryDropdown(string cat)
        {
            DataSet dsLookupData = commisionReceivableBo.GetSubCategories(cat);

            //Populating the categories dropdown
            DataRow drProduct = dsLookupData.Tables[0].NewRow();
            drProduct["PAISC_AssetInstrumentSubCategoryCode"] = "All";
            drProduct["PAISC_AssetInstrumentSubCategoryName"] = "All";
            dsLookupData.Tables[0].Rows.InsertAt(drProduct, 0);
            ddSubCategory.DataSource = dsLookupData.Tables[0];
            ddSubCategory.DataValueField = dsLookupData.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryCode"].ToString();
            ddSubCategory.DataTextField = dsLookupData.Tables[0].Columns["PAISC_AssetInstrumentSubCategoryName"].ToString();
            ddSubCategory.DataBind();
        }

        private void BindIssuerDropdown() 
        {
            DataSet dsLookupData = commisionReceivableBo.GetProdAmc();

            //Populating the categories dropdown
            DataRow drProduct = dsLookupData.Tables[0].NewRow();
            drProduct["PA_AMCCode"] = 0;
            drProduct["PA_AMCName"] = "All";
            dsLookupData.Tables[0].Rows.InsertAt(drProduct, 0);
            ddIssuer.DataSource = dsLookupData.Tables[0];
            ddIssuer.DataValueField = dsLookupData.Tables[0].Columns["PA_AMCCode"].ToString();
            ddIssuer.DataTextField = dsLookupData.Tables[0].Columns["PA_AMCName"].ToString();
            ddIssuer.DataBind();
        }

        protected void ddProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategoryDropdown(ddProduct.SelectedValue);
        }

        protected void ddCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCategoryDropdown(ddCategory.SelectedValue);
            ddSubCategory.Enabled = true;
        }

        protected void BindStructureRuleGrid()
        {
            DataSet dsStructureRules = commisionReceivableBo.GetAdviserCommissionStructureRules(advisorVo.advisorId, ddProduct.SelectedValue.ToLower(), ddCategory.SelectedValue.ToLower(), ddSubCategory.SelectedValue.ToLower(), int.Parse(ddIssuer.SelectedValue), ddStatus.SelectedValue.ToLower());

            if (dsStructureRules.Tables[0].Rows.Count > 0)
                ibtExportSummary.Visible = true;
            else
                ibtExportSummary.Visible = false;

            gvCommMgmt.DataSource = dsStructureRules.Tables[0];
            gvCommMgmt.DataBind();
            Cache.Insert(userVo.UserId.ToString() + "CommissionStructureRule", dsStructureRules.Tables[0]);
        }

        protected void gvCommMgmt_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dsCommissionStructureRule = new DataTable();
            if (Cache[userVo.UserId.ToString() + "CommissionStructureRule"] != null)
            {
                dsCommissionStructureRule = (DataTable)Cache[userVo.UserId.ToString() + "CommissionStructureRule"];
                gvCommMgmt.DataSource = dsCommissionStructureRule;
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            BindStructureRuleGrid();
            //gvCommMgmt.Visible = true;
            pnlGrid.Visible = true;
            ibtExportSummary.Visible = true;
        }

        protected void ibtExportSummary_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dtCommMgmt = new DataTable();
            dtCommMgmt = (DataTable)Cache[userVo.UserId.ToString() + "CommissionStructureRule"];
            if (dtCommMgmt == null)
                return;
            else if (dtCommMgmt.Rows.Count < 1)
                return;
            gvCommMgmt.DataSource = dtCommMgmt;
            gvCommMgmt.ExportSettings.OpenInNewWindow = true;
            gvCommMgmt.ExportSettings.IgnorePaging = true;
            gvCommMgmt.ExportSettings.HideStructureColumns = true;
            gvCommMgmt.ExportSettings.ExportOnlyData = true;
            gvCommMgmt.ExportSettings.FileName = "ViewReceivableStructures";
            gvCommMgmt.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvCommMgmt.MasterTableView.ExportToExcel();
            //BindStructureRuleGrid();
        }

        protected void ddAction_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //string sActionName = ((DropDownList)sender).SelectedItem.Text;
            //string sStructId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlAction = (DropDownList)sender;
            GridDataItem item = (GridDataItem)ddlAction.NamingContainer;
            int structureId = int.Parse(gvCommMgmt.MasterTableView.DataKeyValues[item.ItemIndex]["StructureId"].ToString());
            string prodType = this.ddProduct.SelectedValue;

            switch (ddlAction.SelectedValue)
            {
                case "ViewSTDetails":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('ReceivableSetup','StructureId=" + structureId + "');", true);
                    break;
                case "ManageSchemeMapping":
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "TestPage", "loadcontrol('CommissionStructureToSchemeMapping','ID=" + structureId + "&p=" + prodType + "');", true);
                    break;
                default:
                    return;
            }
        }

        protected void gvCommMgmt_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            gvCommMgmt.CurrentPageIndex = e.NewPageIndex;
            BindStructureRuleGrid();
        }

        protected void gvCommMgmt_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = e.Item as GridDataItem;
            //    DropDownList myDD = item.FindControl("ddAction") as DropDownList;
            //    string itemVal = ((DataRowView)e.Item.DataItem).Row["StructureId"].ToString();
            //    myDD.Items.Insert(0, "Action");
            //    myDD.Items.Insert(1, new ListItem("View Details", itemVal));
            //    myDD.Items.Insert(2, new ListItem("View Mapped Schemes", itemVal));
            //    myDD.SelectedIndexChanged += new EventHandler(ddAction_OnSelectedIndexChanged);
            //}
        }
    }
}