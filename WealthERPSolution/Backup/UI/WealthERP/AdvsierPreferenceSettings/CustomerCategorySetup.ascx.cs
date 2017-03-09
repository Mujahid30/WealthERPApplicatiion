using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using WealthERP.Base;
using BoAdvisorProfiling;
using System.Data;
using Telerik.Web.UI;

namespace WealthERP.AdvsierPreferenceSettings
{
    public partial class CustomerCategorySetup : System.Web.UI.UserControl
    {
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorBo advisorBo = new AdvisorBo();
        protected void Page_Load(object sender, EventArgs e)
        {
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            if (!IsPostBack)
            {
                BindCustomerCategory();
            }
        }

        private void BindCustomerCategory()
        {
            int AdviserId = advisorVo.advisorId;
            DataSet dsCustomerCategoryList = new DataSet();
            DataTable dtCustomerCategoryList;
            //SetControls(false);
            dsCustomerCategoryList = advisorBo.GetAdviserCustomerCategory(AdviserId);
            dtCustomerCategoryList = dsCustomerCategoryList.Tables[0];
            ViewState["CustomerCategory"] = dtCustomerCategoryList;
            if (dtCustomerCategoryList!= null)
            {
                gvCustomerCategory.DataSource = dtCustomerCategoryList;
                gvCustomerCategory.DataBind();
            }
        }

        protected void gvCustomerCategory_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            if (ViewState["CustomerCategory"] != null)
            {
                dt = (DataTable)ViewState["CustomerCategory"];
                gvCustomerCategory.DataSource = dt;
            }

        }
        protected void gvCustomerCategory_ItemCommand(object source, GridCommandEventArgs e)
        {
            int CategoryCode = 0;
            string strExternalCode = string.Empty;
            string CategoryName = string.Empty;
            DateTime createdDate = new DateTime();
            DateTime editedDate = new DateTime();
            DateTime deletedDate = new DateTime();
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                //strExternalCodeToBeEdited = Session["extCodeTobeEdited"].ToString();
                AdvisorBo advisorBo = new AdvisorBo();
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtCategorycode = (TextBox)e.Item.FindControl("txtCategoryCode");
                TextBox txtCusCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                txtCategorycode.Enabled = false;
                CategoryCode = int.Parse(txtCategorycode.Text);
                CategoryName = txtCusCategoryName.Text;
                isUpdated = advisorBo.EditAdviserCustomerCategory(CategoryCode, CategoryName, advisorVo.advisorId);

            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                bool isDeleted = false;
                advisorBo = new AdvisorBo();
                GridDataItem dataItem = (GridDataItem)e.Item;
                TableCell strCategoryCodeForDelete = dataItem["ACC_CustomerCategoryCode"];
                CategoryCode = int.Parse(strCategoryCodeForDelete.Text);
                deletedDate = DateTime.Now;
                isDeleted = advisorBo.DeleteAdviserCustomerCategory(CategoryCode);
                if (!isDeleted)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Already Assigned to Customer. Please remove association first');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pageloadscript", "alert('Record has been deleted successfully !!');", true);
                }
            }
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                //TextBox txtCategorycode = (TextBox)e.Item.FindControl("txtCategoryCode");
                TextBox txtCusCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                //CategoryCode = int.Parse(txtCategorycode.Text);
                CategoryName = txtCusCategoryName.Text;
                isInserted = advisorBo.InsertAdviserCustomerCategory(CategoryName, advisorVo.advisorId);
            }
            BindCustomerCategory();
        }

        protected void gvCustomerCategory_ItemDataBound(object sender, GridItemEventArgs e)
        {
            advisorBo = new AdvisorBo();
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {

                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                //TextBox txtBox = (TextBox)item.FindControl("txtCategoryCode");
                TextBox txtBoxScName = (TextBox)item.FindControl("txtCategoryName");
                //txtBox.Text = txtSchemePlanCode.Value;
                //txtBoxScName.Text = txtSchemeName.Text;
                //txtBox.Enabled = false;
                //txtBoxScName.Enabled = false;
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                //string schemeName = dataItem["PASC_AMC_ExternalType"].Text;
                LinkButton buttonEdit = dataItem["editColumn"].Controls[0] as LinkButton;
                LinkButton buttonDelete = dataItem["deleteColumn"].Controls[0] as LinkButton;
                //Label lbl = new Label();
                //lbl = (Label)e.Item.FindControl("lblFiletypeId");
                //if (schemeName == "AMFI" || schemeName == "ValueResearch" || schemeName == "AF")
                //{
                //    buttonEdit.Enabled = false;
                //    buttonDelete.Enabled = false;
                //    buttonDelete.Attributes["onclick"] = "return alert('You cannot delete this scheme')";
                //    buttonEdit.Attributes["onclick"] = "return alert('You cannot Edit this scheme')";
                //}
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                string strExtType = gvCustomerCategory.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACC_CustomerCategoryCode"].ToString();
                //strExternalCodeToBeEdited = gvCustomerCategory.MasterTableView.DataKeyValues[e.Item.ItemIndex]["gvCustomerCategory"].ToString();

                //if (Session["extCodeTobeEdited"] != null)
                //    Session["extCodeTobeEdited"] = null;
                //Session["extCodeTobeEdited"] = strExternalCodeToBeEdited;
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                //DropDownList dropDownList = (DropDownList)editedItem.FindControl("ddlExternalType");
                //dropDownList.SelectedValue = strExtType;
            }
        }
        //protected void gvCustomerCategory_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        int CategoryCode = Convert.ToInt32(gvCustomerCategory.MasterTableView.DataKeyValues[e.Item.ItemIndex]["XAMP_ModelPortfolioCode"].ToString());
        //        advisorBo.DeleteAdviserCustomerCategory(CategoryCode);
        //        BindCustomerCategory();
        //    }
        //    catch (Exception ex)
        //    {
        //        gvCustomerCategory.Controls.Add(new LiteralControl("Unable to Delete the Record. Reason: " + ex.Message));
        //        e.Canceled = true;
        //    }
        //}
        //protected void gvCustomerCategory_DataBound(object sender, EventArgs e)
        //{
        //}
        //protected void gvCustomerCategory_ItemDataBound(object sender, GridItemEventArgs e)
        //{
 
        //    if (e.Item is GridCommandItem)
        //    {
        //        GridCommandItem cmditm = (GridCommandItem)e.Item;
        //        cmditm.FindControl("RefreshButton").Visible = false;//hide the text
        //        cmditm.FindControl("RebindGridButton").Visible = false;//hide the image
        //    }
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem dataItem = e.Item as GridDataItem;
        //        int CategoryCode = int.Parse(dataItem["ACC_CustomerCategoryCode"].ToString());

        //        LinkButton button = dataItem["DeleteColumn"].Controls[0] as LinkButton;
        //        button.Attributes["onclick"] = "return confirm('Are you sure you want to delete " +
        //        CategoryCode + "?')";
        //    }

        //    if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
        //    {
        //        int adviserId = advisorVo.advisorId;
        //        GridEditFormItem editform = (GridEditFormItem)e.Item;
        //        TextBox txtCategoryName = (TextBox)editform.FindControl("txtCategoryName");

        //    }
        //}
        //protected void gvCustomerCategory_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
        //    {
        //        hdnButtonText.Value = "Insert";
        //    }
        //    else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
        //    {
        //        e.Canceled = true;
        //    }
        //    else if (e.CommandName == RadGrid.UpdateCommandName)
        //    {
        //    }
        //    else if (e.CommandName == "Edit")
        //    {
        //        hdnButtonText.Value = "Edit";
        //    }
        //    else
        //    {
        //        GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
        //        if (!editColumn.Visible)
        //            editColumn.Visible = true;
        //    }
        //}
    }
}