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
using BoOfflineOrderManagement;
using VOAssociates;
using BoUser;

namespace WealthERP.OnlineOrderBackOffice
{
    public partial class ExternalHeaderMapping : System.Web.UI.UserControl
    {
        UserBo userBo;
        UserVo userVo;
        AdvisorVo advisorVo;
        OfflineIPOBackOfficeBo OfflineIPOBackOfficeBo = new OfflineIPOBackOfficeBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
        }
        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            BindHeaderMapping();
        }
        protected void BindHeaderMapping()
        {
            DataSet dsHeaderMapping;
            DataTable dtHeaderMapping;
            dsHeaderMapping = OfflineIPOBackOfficeBo.GetHeaderMapping(Convert.ToInt32(ddlMappingType.SelectedValue), ddlRTA.SelectedValue);
            dtHeaderMapping = dsHeaderMapping.Tables[0];
            if (dtHeaderMapping.Rows.Count > 0)
            {
                if (Cache["HeaderMapping" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("HeaderMapping" + userVo.UserId.ToString(), dtHeaderMapping);
                }
                else
                {
                    Cache.Remove("HeaderMapping" + userVo.UserId.ToString());
                    Cache.Insert("HeaderMapping" + userVo.UserId.ToString(), dtHeaderMapping);
                }
                if (Cache["HeaderMapping1" + userVo.UserId.ToString()] == null)
                {
                    Cache.Insert("HeaderMapping1" + userVo.UserId.ToString(), dtHeaderMapping);
                }
                else
                {
                    Cache.Remove("HeaderMapping1" + userVo.UserId.ToString());
                    Cache.Insert("HeaderMapping1" + userVo.UserId.ToString(), dtHeaderMapping);
                }
                gvHeaderMapping.DataSource = dsHeaderMapping;
                gvHeaderMapping.DataBind();
                pnlGrid.Visible = true;
                gvHeaderMapping.Visible = true;
            }
            else
            {
                gvHeaderMapping.DataSource = dsHeaderMapping;
                gvHeaderMapping.DataBind();
                pnlGrid.Visible = true;
                gvHeaderMapping.Visible = true;
            }
        }
        //protected void btnExpand_Click(object sender, EventArgs e)
        //{
        //    LinkButton button1 = (LinkButton)sender;

        //    if (button1.Text == "+")
        //    {
        //        foreach (GridDataItem gvr in this.gvHeaderMapping.Items)
        //        {

        //            DataTable dtIssueDetail;
        //            int XMLHeaderFileId = 0;
        //            int XMLHeaderId = 0;
        //            LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
        //            RadGrid gvHeaderMappingDetails = (RadGrid)gvr.FindControl("gvHeaderMappingDetails");
        //            Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
        //            //XMLHeaderFileId = int.Parse(gvHeaderMapping.MasterTableView.DataKeyValues[gvr.ItemIndex]["WUXFT_XMLFileTypeId"].ToString());
        //            XMLHeaderId = int.Parse(gvHeaderMapping.MasterTableView.DataKeyValues[gvr.ItemIndex]["WUXHM_XMLHeaderId"].ToString());
        //            DataTable dtgvHeaderMappingDetails = OfflineIPOBackOfficeBo.GetRTASubHeaderType(XMLHeaderId,ddlRTA.SelectedValue);
        //            dtIssueDetail = dtgvHeaderMappingDetails;
        //            gvHeaderMappingDetails.DataSource = dtIssueDetail;
        //            gvHeaderMappingDetails.DataBind();
        //            if (PnlChild.Visible == false)
        //            {
        //                PnlChild.Visible = true;
        //                button.Text = "-";
        //            }

        //        }
        //        button1.Text = "-";
        //    }
        //    else
        //    {
        //        foreach (GridDataItem gvr in this.gvHeaderMapping.Items)
        //        {
        //            LinkButton button = (LinkButton)gvr.FindControl("lbDetails");
        //            Panel PnlChild = (Panel)gvr.FindControl("pnlchild");
        //            if (PnlChild.Visible == true)
        //                PnlChild.Visible = false;
        //            button.Text = "+";
        //        }
        //        button1.Text = "+";
        //    }

        //}
        //protected void btnExpandAll_Click(object sender, EventArgs e)
        //{
        //    int XMLHeaderFileId = 0;
        //    LinkButton buttonlink = (LinkButton)sender;
        //    GridDataItem gdi;
        //    gdi = (GridDataItem)buttonlink.NamingContainer;
        //    //XMLHeaderFileId = int.Parse(gvHeaderMapping.MasterTableView.DataKeyValues[gdi.ItemIndex]["WUXFT_XMLFileTypeId"].ToString());
        //    int XMLHeaderId = int.Parse(gvHeaderMapping.MasterTableView.DataKeyValues[gdi.ItemIndex]["WUXHM_XMLHeaderId"].ToString());

        //    RadGrid gvHeaderMappingDetails = (RadGrid)gdi.FindControl("gvHeaderMappingDetails");
        //    Panel PnlChild = (Panel)gdi.FindControl("pnlchild");
        //    if (PnlChild.Visible == false)
        //    {
        //        PnlChild.Visible = true;
        //        buttonlink.Text = "-";
        //    }
        //    else if (PnlChild.Visible == true)
        //    {
        //        PnlChild.Visible = false;
        //        buttonlink.Text = "+";
        //    }
        //    DataTable dtHeaderMapp = OfflineIPOBackOfficeBo.GetRTAHeaderType(XMLHeaderFileId, XMLHeaderId, ddlRTA.SelectedValue);
        //    gvHeaderMappingDetails.DataSource = dtHeaderMapp;
        //    gvHeaderMappingDetails.DataBind();
        //}
        protected void gvHeaderMapping_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtHeaderMapping;
            dtHeaderMapping = (DataTable)Cache["HeaderMapping" + userVo.UserId.ToString()];
            if (dtHeaderMapping != null)
            {
                gvHeaderMapping.DataSource = dtHeaderMapping;
            }

        }
        //protected void Page_PreRenderComplete(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        GridNestedViewItem firstLevelNestedViewItem = (GridNestedViewItem)gvHeaderMapping.MasterTableView.GetItems(GridItemType.NestedView)[0];

        //        GridNestedViewItem secondLevelNestedViewItem = (GridNestedViewItem)firstLevelNestedViewItem.NestedTableViews[0].GetItems(GridItemType.NestedView)[0];

        //        GridItem itemToExpand = secondLevelNestedViewItem.NestedTableViews[0].GetItems(GridItemType.Item)[0];

        //        itemToExpand.ExpandHierarchyToTop();
        //    }
        //}
        protected void gvHeaderMapping_ItemCommand(object source, GridCommandEventArgs e)
        {
            string ecommand = null;
            if (e.CommandName == "PerformInsert")
            {
                ecommand = "IN";
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                RadGrid gvHeaderMapping = (RadGrid)e.Item.FindControl("gvHeaderMapping");
                DropDownList ddlXMLHeaderName = (DropDownList)e.Item.FindControl("ddlXMLHeaderName");
                int XMLHeaderId = Convert.ToInt32(ddlXMLHeaderName.SelectedValue);
                TextBox txtExHeader = (TextBox)e.Item.FindControl("txtExHeader");
                OfflineIPOBackOfficeBo.CreateUpdateExternalHeader(txtExHeader.Text.TrimEnd(), XMLHeaderId, ddlRTA.SelectedValue, ecommand, "");
                Response.Write(@"<script language='javascript'>alert('External Header " + txtExHeader.Text + " Added  successfully');</script>");

            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                ecommand = "UP";
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                RadGrid gvHeaderMapping = (RadGrid)e.Item.FindControl("gvHeaderMapping");
                DropDownList ddlXMLHeaderName = (DropDownList)e.Item.FindControl("ddlXMLHeaderName");
                int XMLHeaderId = Convert.ToInt32(ddlXMLHeaderName.SelectedValue);
                TextBox txtExHeader = (TextBox)e.Item.FindControl("txtExHeader");
                OfflineIPOBackOfficeBo.CreateUpdateExternalHeader(txtExHeader.Text.TrimEnd(), XMLHeaderId, ddlRTA.SelectedValue, ecommand, hdnPrevEHName.Value);
                Response.Write(@"<script language='javascript'>alert('External Header " + txtExHeader.Text + "Updated successfully');</script>");

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                ecommand = "DLT";
                GridDataItem dataItem = (GridDataItem)e.Item;
                TableCell headerId = dataItem["WUXHM_XMLHeaderId"];
                TableCell externalHeader = dataItem["WEHXHM_ExternalHeaderName"];
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                RadGrid gvHeaderMapping = (RadGrid)e.Item.FindControl("gvHeaderMapping");
                int XMLHeaderId = int.Parse(headerId.Text);
              string   txtExHeader= externalHeader.Text;
              OfflineIPOBackOfficeBo.CreateUpdateExternalHeader(txtExHeader.ToString().TrimEnd(),XMLHeaderId, ddlRTA.SelectedValue, ecommand, hdnPrevEHName.Value);
                Response.Write(@"<script language='javascript'>alert('External Header " + txtExHeader  + "Deleted successfully');</script>");
            }
            BindHeaderMapping();
        }
        protected void gvHeaderMapping_ItemDataBound(object sender, GridItemEventArgs e)
        {
            DropDownList ddlXMLHeaderName = (DropDownList)e.Item.FindControl("ddlXMLHeaderName");
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
               DataTable dtHeaderMapping1 = (DataTable)Cache["HeaderMapping1" + userVo.UserId.ToString()];
               ddlXMLHeaderName.DataSource = dtHeaderMapping1.DefaultView.ToTable(true, new string[] { "WUXHM_XMLHeaderId", "WUXHM_XMLHeaderName" });
                ddlXMLHeaderName.DataValueField = "WUXHM_XMLHeaderId";
                ddlXMLHeaderName.DataTextField = "WUXHM_XMLHeaderName";
                ddlXMLHeaderName.DataBind();
                ddlXMLHeaderName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton; 
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                DataTable dtHeaderMapping1 = (DataTable)Cache["HeaderMapping1" + userVo.UserId.ToString()];
                ddlXMLHeaderName.DataSource = dtHeaderMapping1.DefaultView.ToTable(true, new string[] { "WUXHM_XMLHeaderId", "WUXHM_XMLHeaderName" });
                ddlXMLHeaderName.DataValueField = "WUXHM_XMLHeaderId";
                ddlXMLHeaderName.DataTextField = "WUXHM_XMLHeaderName";
                ddlXMLHeaderName.DataBind();
                ddlXMLHeaderName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                string strExtType = gvHeaderMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WUXHM_XMLHeaderId"].ToString();
                string strExtType1 = gvHeaderMapping.MasterTableView.DataKeyValues[e.Item.ItemIndex]["WEHXHM_ExternalHeaderName"].ToString();
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                TextBox txtExHeader = (TextBox)editedItem.FindControl("txtExHeader");
                ddlXMLHeaderName.SelectedValue = strExtType;
                txtExHeader.Text = strExtType1;
                hdnPrevEHName.Value = strExtType1;
            }
        }
        //protected void gvHeaderMapping_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        //{
        //    GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        //      DataTable dtHeaderMapping;
        //    switch (e.DetailTableView.Name)
        //    {

        //        case "gvTableview":
        //            {
        //                 dtHeaderMapping = (DataTable)Cache["HeaderMapping1" + userVo.UserId.ToString()];
        //                e.DetailTableView.DataSource = dtHeaderMapping;
        //                break;
        //            }


        //    }
        //}
        protected void ddlMappingType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMappingType.SelectedValue == "50")
            {

                lblRTA.Visible = false;
                ddlRTA.Visible = false;
                ddlRTA_RequiredFieldValidator.Visible = false;
                ddlRTA.SelectedValue = null;
                pnlGrid.Visible = false;
                gvHeaderMapping.Visible = false;
            }
            else
            {

                lblRTA.Visible = true;
                ddlRTA.Visible = true;
                ddlRTA_RequiredFieldValidator.Visible = true;
                pnlGrid.Visible = false;
                gvHeaderMapping.Visible = false;
            }
        }
        public void btnExportData_OnClick(object sender, ImageClickEventArgs e)
        {
            //  gvIPOOrderBook.MasterTableView.DetailTables[0].HierarchyDefaultExpanded = true;
            gvHeaderMapping.MasterTableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
            gvHeaderMapping.ExportSettings.OpenInNewWindow = true;
            gvHeaderMapping.ExportSettings.IgnorePaging = true;
            gvHeaderMapping.ExportSettings.HideStructureColumns = true;
            gvHeaderMapping.ExportSettings.ExportOnlyData = true;
            gvHeaderMapping.ExportSettings.FileName = "External Header Mapping Details";
            gvHeaderMapping.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvHeaderMapping.MasterTableView.ExportToExcel();

        }
    }
}
