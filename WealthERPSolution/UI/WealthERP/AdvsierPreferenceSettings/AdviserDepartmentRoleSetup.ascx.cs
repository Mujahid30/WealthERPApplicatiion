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
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoOnlineOrderManagemnet;
using System.Configuration;
using System.Data;
using BoOnlineOrderManagement;
using Telerik.Web.UI;
using System.Text;
using VoUser;
using BoCommon;

using BoAdvisorProfiling;
namespace WealthERP.AdvsierPreferenceSettings
{
    public partial class AdviserDepartmentRoleSetup : System.Web.UI.UserControl
    {
        AdviserPreferenceBo advisorPreferenceBo = new AdviserPreferenceBo();
        UserBo userBo;
        UserVo userVo;
        AdvisorVo adviserVo = new AdvisorVo();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            userBo = new UserBo();
            userVo = (UserVo)Session[SessionContents.UserVo];
            adviserVo = (AdvisorVo)Session["advisorVo"];
            BindUserRole();
        }
        protected void BindUserRole()
        {
            try
            {
                DataSet dsBindUserRole = new DataSet();
                DataTable dtBindUserRole = new DataTable();
                dtBindUserRole = advisorPreferenceBo.GetUserRole(adviserVo.advisorId).Tables[0];
                if (dtBindUserRole.Rows.Count > 0)
                {
                    if (Cache["UserList" + adviserVo.advisorId] == null)
                    {
                        Cache.Insert("UserList" + adviserVo.advisorId, dtBindUserRole);
                    }
                    else
                    {
                        Cache.Remove("UserList" + adviserVo.advisorId);
                        Cache.Insert("UserList" + adviserVo.advisorId, dtBindUserRole);
                    }
                    gvAdviserList.DataSource = dtBindUserRole;
                    gvAdviserList.DataBind();
                }
                else
                {
                    gvAdviserList.DataSource = dtBindUserRole;
                    gvAdviserList.DataBind();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineClientAccess.ascx.cs:BindAdviserClientKYCStatusList()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void gvAdviserList_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)
            {
                GridEditFormInsertItem item = (GridEditFormInsertItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlLevel = (DropDownList)gefi.FindControl("ddlLevel");
                BindDepartmentddl(ddlLevel);
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                int roleId = Convert.ToInt32(gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RoleId"].ToString());
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                DropDownList ddlLevel = (DropDownList)editedItem.FindControl("ddlLevel");
                TextBox txtRoleName=(TextBox) editedItem.FindControl("txtRoleName");
                 TextBox txtNote=(TextBox) editedItem.FindControl("txtNote");
                RadGrid rgRoles=(RadGrid)editedItem.FindControl("rgRoles");
                BindPopUpcontrols(roleId,   ddlLevel,  txtRoleName,txtNote, rgRoles);
                FillAdviserrole(roleId,ddlLevel, txtRoleName, txtNote, rgRoles);

                //TextBox txtIssueName = (TextBox)e.Item.FindControl("txtIssueName");
                //txtIssueName.Text = txtName.Text;
                //TextBox txtCategoryName = (TextBox)e.Item.FindControl("txtCategoryName");



                //GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                //GridEditFormItem gefi = (GridEditFormItem)e.Item;
                //DropDownList ddlLevel = (DropDownList)gefi.FindControl("ddlLevel");
                //DataSet dsddlLevel = advisorPreferenceBo.GetDepartment(adviserVo.advisorId);
                //DataTable dtddlLevel;
                //dtddlLevel = dsddlLevel.Tables[0];
                //ddlLevel.DataSource = dtddlLevel;
                //ddlLevel.DataValueField = dtddlLevel.Columns["AD_DepartmentId"].ToString();

                //ddlLevel.DataTextField = dtddlLevel.Columns["AD_DepartmentName"].ToString();
                //ddlLevel.DataBind();

                //if (ddlLevel.Items.Count > 0)
                //    ddlLevel.SelectedValue = gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AD_DepartmentId"].ToString();

            }
        }


        private void BindDepartmentddl(DropDownList ddlDepartMent)
        {
            DataSet dsddlDepartMent = advisorPreferenceBo.GetDepartment(adviserVo.advisorId);
            DataTable dtddlDepartMent;
            dtddlDepartMent = dsddlDepartMent.Tables[0];
            ddlDepartMent.DataSource = dtddlDepartMent;
            ddlDepartMent.DataValueField = dtddlDepartMent.Columns["AD_DepartmentId"].ToString();
            ddlDepartMent.DataTextField = dtddlDepartMent.Columns["AD_DepartmentName"].ToString();
            ddlDepartMent.DataBind();
            ddlDepartMent.Items.Insert(0, new ListItem("Select", "Select"));
        }

        protected void gvAdviserList_OnItemCommand(object source, GridCommandEventArgs e)
        {
            // StringBuilder strrlbUserlist = new StringBuilder();
            string StrUserLeve = "";
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlLevel = (DropDownList)e.Item.FindControl("ddlLevel");
                TextBox txtRoleName = (TextBox)e.Item.FindControl("txtRoleName");
                TextBox txtNote = (TextBox)e.Item.FindControl("txtNote");
              //  CheckBoxList rlbUserlist = (CheckBoxList)e.Item.FindControl("rlbUserlist");
                RadGrid rgLevels = (RadGrid)gridEditableItem.FindControl("rgRoles");
                foreach (GridDataItem gdi in rgLevels.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbRoles")).Checked == true)
                    {
                        StrUserLeve += gdi["UR_RoleId"].Text+',';   

                    }
                }
                advisorPreferenceBo.CreateUserRole(int.Parse(ddlLevel.SelectedValue), txtRoleName.Text, txtNote.Text, adviserVo.advisorId, userVo.UserId, StrUserLeve.TrimEnd(','));
            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlLevel = (DropDownList)e.Item.FindControl("ddlLevel");
                TextBox txtRoleName = (TextBox)e.Item.FindControl("txtRoleName");
                TextBox txtNote = (TextBox)e.Item.FindControl("txtNote");
                int rollid = int.Parse(gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RoleId"].ToString());
                RadGrid rgLevels = (RadGrid)gridEditableItem.FindControl("rgRoles");
                foreach (GridDataItem gdi in rgLevels.Items)
                {
                    if (((CheckBox)gdi.FindControl("cbRoles")).Checked == true)
                    {
                        StrUserLeve += gdi["UR_RoleId"].Text + ',';

                    }
                }
                //advisorPreferenceBo.UpdateUserrole(rollid, int.Parse(ddlLevel.SelectedValue), txtRoleName.Text, txtNote.Text, userVo.UserId, StrUserLeve.TrimEnd(','));
            }
            if (e.CommandName == RadGrid.DeleteCommandName)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                int rollid = int.Parse(gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RoleId"].ToString());
                advisorPreferenceBo.DeleteUserRole(rollid);
            }
            if (e.CommandName == RadGrid.RebindGridCommandName)
            {
                gvAdviserList.Rebind();
            }
            if (e.CommandName == RadGrid.SelectCommandName)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                int roleId = int.Parse(gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RoleId"].ToString());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvvvvvv", "loadcontrol('AdviserRoleToTreeNodeMapping','?RoleId=" + roleId + "');", true);

            }
            BindUserRole();
        }


        private void BindPopUpcontrols(int roleId, DropDownList ddlLevel, TextBox textRoleName,TextBox txtNote,RadGrid rgRoles)
        {
            //DataTable dtuserassociate = new DataTable();
            



        }
        private void FillAdviserrole(int roleId,  DropDownList ddlLevel,TextBox textRoleName, TextBox txtNote, RadGrid rgRoles)
        {
            int levelid=0;
             DataTable dtuserlist = new DataTable();
             DataTable dtBindUserRole = new DataTable();
             

                dtuserlist = advisorPreferenceBo.GetAdviserRoledepartmentwise(roleId).Tables[0];
                BindDepartmentddl(ddlLevel);
                if (dtuserlist.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtuserlist.Rows)
                    {
                        ddlLevel.SelectedValue = dr["AD_DepartmentName"].ToString();
                        textRoleName.Text = dr["AR_Role"].ToString();
                        txtNote.Text = dr["AR_RolePurpose"].ToString();

                        if (rgRoles.Items.Count == 0)
                        {
                            dtBindUserRole = advisorPreferenceBo.GetUserRoleDepartmentWise(Convert.ToInt32(ddlLevel.SelectedValue));
                            rgRoles.DataSource = dtBindUserRole;
                            rgRoles.DataBind();
                        }

                        foreach (GridDataItem gdi in rgRoles.Items)
                        {
                            int levelid2 = Convert.ToInt32(gdi["UR_RoleId"].Text);
                            if (levelid2 == Convert.ToInt32(dr["UR_RoleId"].ToString()))
                            {
                                CheckBox cbRoles = (CheckBox)gdi.FindControl("cbRoles");
                                cbRoles.Checked = true;

                            }


                        }
                    }
                }
             
        }

        protected void CheckBoxList1_SelectedIndexChnaged(object sender, System.EventArgs e)
        {
            
            foreach (ListItem li in rlbUserlist.Items)
            {
                if (li.Selected == true)
                {
                    Response.Write(li.Text);

                }
            }
             
        }  

        protected void ddlLevel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int cnt = 0;
            DropDownList ddlLevel = (DropDownList)sender;

            GridEditFormInsertItem gdi = (GridEditFormInsertItem)(ddlLevel).NamingContainer;
            RadGrid rgRoles = (RadGrid)gdi.FindControl("rgRoles");


            DataTable dtUserList = new DataTable();
            dtUserList = advisorPreferenceBo.GetUserRoleDepartmentWise(Convert.ToInt32(ddlLevel.SelectedValue));

            foreach (GridDataItem gdii in rgRoles.Items)
               {
                   if (((CheckBox)gdii.FindControl("cbRoles")).Checked == true)
                   {

                       cnt = cnt + 1;
                   }
               }
             if (cnt == 0)
             {
                 rgRoles.DataSource = dtUserList;
                 rgRoles.DataBind();

                 if (Cache[userVo.UserId.ToString() + "RgRoles"] != null)
                 {
                     Cache.Remove(userVo.UserId.ToString() + "RgRoles");
                     Cache.Insert(userVo.UserId.ToString() + "RgRoles", dtUserList);
                 }
                 else
                 {
                     Cache.Insert(userVo.UserId.ToString() + "RgRoles", dtUserList);

                 }
             }

           
           // BindList(int.Parse(ddlLevel.SelectedItem.Value), rlbUserlist);
        }
        private void BindList(int DepartmentId, CheckBoxList rlbUserlist)
        {

            DataTable dtUserList = new DataTable();
            dtUserList = advisorPreferenceBo.GetUserRoleDepartmentWise(DepartmentId);
            rlbUserlist.DataSource = dtUserList;
            rlbUserlist.DataValueField = dtUserList.Columns["UR_RoleId"].ToString();
            rlbUserlist.DataTextField = dtUserList.Columns["UR_RoleName"].ToString();
            rlbUserlist.DataBind();
        }
        protected void gvAdviserList_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindUserRole = new DataTable();
            dtBindUserRole = (DataTable)Cache["UserList" + adviserVo.advisorId];
            if (dtBindUserRole != null)
            {
                gvAdviserList.DataSource = dtBindUserRole;
            }
        }


        protected void rgRoles_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtBindUserRole = new DataTable();
            RadGrid rgRoles = (RadGrid)(sender);
           

            dtBindUserRole = (DataTable)Cache[userVo.UserId.ToString() + "RgRoles"];
            if (dtBindUserRole != null)
            {
                rgRoles.DataSource = dtBindUserRole;
            }
        }
        //protected void lnkRole_OnClick(object sender, EventArgs e)
        //{
        //    GridDataItem dataItem = (GridDataItem)e.Item;
        //    int rollid = int.Parse(gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RoleId"].ToString());
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvvvvvv", "loadcontrol('AdviserRoleToTreeNodeMapping');", true);

        //}
       }
}
