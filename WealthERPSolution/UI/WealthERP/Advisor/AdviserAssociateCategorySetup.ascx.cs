using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCustomerPortfolio;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using VoUser;
using Telerik.Web.UI;
using BoCommon;
using BoAdvisorProfiling;
namespace WealthERP.Advisor
{
    public partial class AdviserAssociateCategorySetup : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo;
        protected void Page_Load(object sender, EventArgs e)
        {

            userVo = (UserVo)Session["userVo"];
            advisorVo = (AdvisorVo)Session["advisorVo"];
            SessionBo.CheckSession();
            if (!IsPostBack)
            {
                DataSet dsBindCategoryDropdown = AdvisorCategoryBind(advisorVo.advisorId);
                ddlCategory.DataSource = dsBindCategoryDropdown.Tables[0];
                ddlCategory.DataValueField = dsBindCategoryDropdown.Tables[0].Columns["ACM_Id"].ToString();
                ddlCategory.DataTextField = dsBindCategoryDropdown.Tables[0].Columns["ACM_Name"].ToString();
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "Select"));
                gvAdvisorCategory.Visible = false;

            }


        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            gvAdvisorCategory.Visible = true;
            bindgrid();
        }
        public void bindgrid()
        {
            int ACM_ID = Convert.ToInt32(ddlCategory.SelectedValue);
            DataSet ds = gvAdvisorCategoryBind(advisorVo.advisorId, ACM_ID);
            DataTable dtCategory = ds.Tables[0];
            gvAdvisorCategory.DataSource = dtCategory;
            gvAdvisorCategory.DataBind();
            if (Cache["gvAdvisorCategory" + userVo.UserId.ToString()] == null)
            {
                Cache.Insert("gvAdvisorCategory" + userVo.UserId.ToString(), dtCategory);
            }
            else
            {
                Cache.Remove("gvAdvisorCategory" + userVo.UserId.ToString());
                Cache.Insert("gvAdvisorCategory" + userVo.UserId.ToString(), dtCategory);
            }
        }

        private DataSet AdvisorCategoryBind(int advisorId)
        {
            AdvisorBo advisorbo = new AdvisorBo();
            try
            {
                DataSet categorylist = advisorbo.AdvisorCategoryBind(advisorId);
                return categorylist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet gvAdvisorCategoryBind(int advisorId, int ACM_ID)
        {
            AdvisorBo advisorbo = new AdvisorBo();
            try
            {
                DataSet categorylist = advisorbo.gvAdvisorCategoryBind(advisorId, ACM_ID);
                return categorylist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            gvAdvisorCategory.Visible = false;
        }
        protected void gvAdvisorCategory_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtCategory = (DataTable)Cache["gvAdvisorCategory" + userVo.UserId.ToString()];
            gvAdvisorCategory.DataSource = dtCategory;
        }
        protected void gvAdvisorCategory_ItemCommand(object source, GridCommandEventArgs e)
        {

            AdvisorBo advisorbo = new AdvisorBo();
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                string categoryname;
                int acm_id;
                gvAdvisorCategory.Visible = true;
                bool isInserted = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                categoryname = txtCategoryName.Text;
                acm_id = Convert.ToInt32(ddlCategory.SelectedValue);
                int AC_CategoryId = 0;
                isInserted = advisorbo.InsertEditDeleteCategory(categoryname, advisorVo.advisorId, acm_id, AC_CategoryId, "Insert");
                if (isInserted == false)
                    Response.Write(@"<script language='javascript'>alert('Cannot be Inserted');</script>");
                else
                    Response.Write(@"<script language='javascript'>alert('Inserted successfully.');</script>");
            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                string categoryname;
                int acm_id;
                gvAdvisorCategory.Visible = true;
                bool isUpdated = false;
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");
                acm_id = Convert.ToInt32(ddlCategory.SelectedValue);
                categoryname = txtCategoryName.Text;
                int AC_CategoryId = Convert.ToInt32(gvAdvisorCategory.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AC_CategoryId"].ToString());
                isUpdated = advisorbo.InsertEditDeleteCategory(categoryname, advisorVo.advisorId, acm_id, AC_CategoryId, "Update");
                if (isUpdated == false)
                    Response.Write(@"<script language='javascript'>alert('Cannot be Updated');</script>");
                else
                    Response.Write(@"<script language='javascript'>alert('Updated successfully.');</script>");
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                int ACM_ID;
                bool isDeleted;
                string categoryname;
                ACM_ID = Convert.ToInt32(gvAdvisorCategory.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ACM_ID"]);
                categoryname = gvAdvisorCategory.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AC_CategoryName"].ToString();
                int AC_CategoryId = Convert.ToInt32(gvAdvisorCategory.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AC_CategoryId"].ToString());
                //check if deleted then show message
                isDeleted = advisorbo.InsertEditDeleteCategory(categoryname, advisorVo.advisorId, ACM_ID, AC_CategoryId, "Delete");
                if (isDeleted == false)
                    Response.Write(@"<script language='javascript'>alert('Cannot be Deleted');</script>");
                else
                    Response.Write(@"<script language='javascript'>alert('Deleted successfully.');</script>");
            }

            bindgrid();

        }
        protected void gvAdvisorCategory_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {

                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                TextBox txtCategoryName = (TextBox)e.Item.FindControl("ddlCategoryName");
                //int A_AdviserId;
                gvAdvisorCategory.Visible = true;
                //txtCategoryName.Text = gvAdvisorCategory.MasterTableView.DataKeyValues[0]["AC_CategoryName"].ToString();


            }
        }


    }
}