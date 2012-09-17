using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoCustomerProfiling;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

namespace WealthERP.SuperAdmin
{
    public partial class AddSchemeMapping : System.Web.UI.UserControl
    {
        int schemePlanCode;
        DataSet dsSchemePlanDetails;
        CustomerBo customerBo;


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void txtSchemePlanCode_ValueChanged1(object sender, EventArgs e)
        {
            dsSchemePlanDetails = new DataSet();
            customerBo = new CustomerBo();
            if (!string.IsNullOrEmpty(txtSchemePlanCode.Value.ToString().Trim()))
            {
                schemePlanCode = int.Parse(txtSchemePlanCode.Value);
                BindSchemePlanDetails(schemePlanCode);
            }
        }

        public void BindSchemePlanDetails(int schemePlanCode)
        {
            dsSchemePlanDetails = new DataSet();
            customerBo = new CustomerBo();
            schemePlanCode = int.Parse(txtSchemePlanCode.Value);
            dsSchemePlanDetails = customerBo.GetSchemeDetails(schemePlanCode);
            gvSchemeDetails.DataSource = dsSchemePlanDetails;
            gvSchemeDetails.DataBind();
            if (Cache["gvSchemeDetailsForMappinginSuperAdmin"] == null)
            {
                Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsSchemePlanDetails);
            }
            else
            {
                Cache.Remove("gvSchemeDetailsForMappinginSuperAdmin");
                Cache.Insert("gvSchemeDetailsForMappinginSuperAdmin", dsSchemePlanDetails);
            }
        }

        protected void gvSchemeDetails_ItemCommand(object source, GridCommandEventArgs e)
        {
            int strSchemePlanCode = 0;
            string strExternalCode = string.Empty;
            string strExternalType = string.Empty;
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtExtCode = (TextBox)e.Item.FindControl("txtExternalCodeForEditForm");
                DropDownList txtExtType = (DropDownList)e.Item.FindControl("ddlExternalType");
                TextBox txtSchemePlancode = (TextBox)e.Item.FindControl("txtSchemePlanCodeForEditForm");
                strSchemePlanCode = int.Parse(txtSchemePlancode.Text);
                strExternalCode = txtExtCode.Text;
                strExternalType = txtExtType.Text;
                isUpdated = customerBo.EditProductAMCSchemeMapping(strSchemePlanCode, strExternalCode, strExternalType);
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDeleted = false;
                customerBo = new CustomerBo();
                GridDataItem dataItem = (GridDataItem)e.Item;
                TableCell strSchemePlanCodeForDelete = dataItem["PASP_SchemePlanCode"];
                TableCell StrExternalCodeForDelete = dataItem["PASC_AMC_ExternalCode"];
                TableCell strExternalTypeForDelete = dataItem["PASC_AMC_ExternalType"];
                strSchemePlanCode = int.Parse(strSchemePlanCodeForDelete.Text);
                strExternalCode = StrExternalCodeForDelete.Text;
                strExternalType = strExternalTypeForDelete.Text;
                isDeleted = customerBo.DeleteMappedSchemeDetails(strSchemePlanCode, strExternalCode, strExternalType);
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                CustomerBo customerBo = new CustomerBo();
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtExtCode = (TextBox)e.Item.FindControl("txtExternalCodeForEditForm");
                DropDownList txtExtType = (DropDownList)e.Item.FindControl("ddlExternalType");
                TextBox txtSchemePlancode = (TextBox)e.Item.FindControl("txtSchemePlanCodeForEditForm");
                strSchemePlanCode = int.Parse(txtSchemePlancode.Text);
                strExternalCode = txtExtCode.Text;
                strExternalType = txtExtType.Text;
                isInserted = customerBo.InsertProductAMCSchemeMappingDetalis(strSchemePlanCode, strExternalCode, strExternalType);
            }
            BindSchemePlanDetails(schemePlanCode);
        }

        protected void gvSchemeDetails_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dtGvSchemeDetails = new DataSet();
            dtGvSchemeDetails = (DataSet)Cache["gvSchemeDetailsForMappinginSuperAdmin"];
            gvSchemeDetails.DataSource = dtGvSchemeDetails;
        }

        protected void gvSchemeDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            customerBo = new CustomerBo();
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                TextBox txtBox = (TextBox)item.FindControl("txtSchemePlanCodeForEditForm");
                txtBox.Text = txtSchemePlanCode.Value;
                txtBox.Enabled = false;
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string schemeName = dataItem["PASC_AMC_ExternalType"].Text;
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
                Label lbl = new Label();
                lbl = (Label)e.Item.FindControl("lblFiletypeId");
                if (schemeName == "AMFI" || schemeName == "ValueResearch" || schemeName == "AF")
                {
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonDelete.Attributes["onclick"] = "return alert('You cannot delete this scheme')";
                    buttonEdit.Attributes["onclick"] = "return alert('You cannot Edit this scheme')";
                }
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string strExtType = gvSchemeDetails.MasterTableView.DataKeyValues[e.Item.ItemIndex]["PASC_AMC_ExternalType"].ToString();
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlExternalType");
                dropDownList.SelectedValue = strExtType;
            }
        }        

        protected void Page_Init(object sender, EventArgs e)
        {
            gvSchemeDetails.HeaderContextMenu.ItemClick += new RadMenuEventHandler(HeaderContextMenu_ItemClick);
            GridHeaderContextMenu ghcm = new GridHeaderContextMenu(gvSchemeDetails);
            int i = ghcm.Items.Count;
            RadMenuItem rmi = new RadMenuItem();
            RadMenuEventArgs rmie = new RadMenuEventArgs(rmi);
            HeaderContextMenu_ItemClick(sender, rmie);
        }

        protected void HeaderContextMenu_ItemClick(object sender, RadMenuEventArgs e)
        {
            //GridHeaderContextMenu ghcm = new GridHeaderContextMenu(gvSchemeDetails);
            //ghcm.ClientID[1].ToString();
        }

        public void btnExportFilteredData_OnClick(object sender, ImageClickEventArgs e)
        {
            gvSchemeDetails.ExportSettings.OpenInNewWindow = true;
            gvSchemeDetails.ExportSettings.IgnorePaging = true;
            gvSchemeDetails.ExportSettings.HideStructureColumns = true;
            gvSchemeDetails.ExportSettings.ExportOnlyData = true;
            gvSchemeDetails.ExportSettings.FileName = "Scheme Mapping Details";
            gvSchemeDetails.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            gvSchemeDetails.MasterTableView.ExportToExcel();
        }
    }
}