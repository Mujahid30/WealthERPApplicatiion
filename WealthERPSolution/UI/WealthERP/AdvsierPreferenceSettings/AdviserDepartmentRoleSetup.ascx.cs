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
                DataSet dsddlLevel = advisorPreferenceBo.GetDepartment(adviserVo.advisorId);
                DataTable dtddlLevel;
                dtddlLevel = dsddlLevel.Tables[0];
                ddlLevel.DataSource = dtddlLevel;
                ddlLevel.DataValueField = dtddlLevel.Columns["AD_DepartmentId"].ToString();

                ddlLevel.DataTextField = dtddlLevel.Columns["AD_DepartmentName"].ToString();
                ddlLevel.DataBind();
                ddlLevel.Items.Insert(0, new ListItem("Select", "Select"));
            }
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode && e.Item.ItemIndex != -1)
            {
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                GridEditFormItem gefi = (GridEditFormItem)e.Item;
                DropDownList ddlLevel = (DropDownList)gefi.FindControl("ddlLevel");
                DataSet dsddlLevel = advisorPreferenceBo.GetDepartment(adviserVo.advisorId);
                DataTable dtddlLevel;
                dtddlLevel = dsddlLevel.Tables[0];
                ddlLevel.DataSource = dtddlLevel;
                ddlLevel.DataValueField = dtddlLevel.Columns["AD_DepartmentId"].ToString();

                ddlLevel.DataTextField = dtddlLevel.Columns["AD_DepartmentName"].ToString();
                ddlLevel.DataBind();

                if (ddlLevel.Items.Count > 0)
                    ddlLevel.SelectedValue = gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AD_DepartmentId"].ToString();

            }
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
                CheckBoxList rlbUserlist = (CheckBoxList)e.Item.FindControl("rlbUserlist");

                int i = 0;



                if (rlbUserlist.Items[i].Selected)
 	{
        StrUserLeve = rlbUserlist.Items[i].Value;
         
 	}


               //for (int idx1 = 0; idx1 < rlbUserlist.Items.Count; idx1++)

               //    if(rlbUserlist.CheckedItems.Count>1)
            

              //  RadListBox rlbUserlist =ById<RadListBox>("rlbUserlist");
           //     RadListBox RadListBox1 = (RadListBox)e.Item.FindControl("rlbUserlist");

                 
           //     StringBuilder sb = new StringBuilder();
           //     IList<RadListBoxItem> collection = RadListBox1.CheckedItems;
           // foreach (RadListBoxItem item in collection)
           // {
           //     sb.Append(item.Value + "<br />");
           // }
 
           //string a = sb.ToString();







                //if (rlbUserlist.CheckBoxes)
                //{
                //    foreach (RadListBoxItem item1 in rlbUserlist.Items)
                //    {
                //        //if (((RadListBox)gridEditableItem.FindControl("rlbUserlist")).CheckedItems.Count > 0)
                //        //if (item1.Checked==true)
                //        //if (((RadListBox)gridEditableItem.FindControl("rlbUserlist")).CheckBoxes == true)
                //        //{
                //        if (item1.Checked == true)

                //            //for (int i = 0; i < rlbUserlist.CheckedItems.Count; i++)
                //            //{
                //            StrUserLeve += item1.Value + ",";

                        //}
                        // StrUserLeve = StrUserLeve.Remove(StrUserLeve.Trim().Length - 1);
                        //}

                    //}
                
                //foreach (RadListBoxItem item1 in rlbUserlist.Items)
                //{+ rlbUserlist.CheckedItems[i].Text
                //    if (((RadListBox)gridEditableItem.FindControl("rlbUserlist")).CheckBoxes == true)
                //    {

                //        strrlbUserlist.Append(item1.Value);
                //        strrlbUserlist.Append(",");

                //    }
                //}

                advisorPreferenceBo.CreateUserRole(int.Parse(ddlLevel.SelectedValue), txtRoleName.Text, txtNote.Text, adviserVo.advisorId, userVo.UserId, StrUserLeve.TrimEnd(','));
            }
            if (e.CommandName == RadGrid.UpdateCommandName)
            {
                GridEditableItem gridEditableItem = (GridEditableItem)e.Item;
                DropDownList ddlLevel = (DropDownList)e.Item.FindControl("ddlLevel");
                TextBox txtRoleName = (TextBox)e.Item.FindControl("txtRoleName");
                TextBox txtNote = (TextBox)e.Item.FindControl("txtNote");
                int rollid = int.Parse(gvAdviserList.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AR_RoleId"].ToString());
                advisorPreferenceBo.UpdateUserrole(rollid, int.Parse(ddlLevel.SelectedValue), txtRoleName.Text, txtNote.Text, userVo.UserId);
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

            BindUserRole();
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
            DropDownList ddlLevel = (DropDownList)sender;

            GridEditFormInsertItem gdi = (GridEditFormInsertItem)(ddlLevel).NamingContainer;
            CheckBoxList rlbUserlist = (CheckBoxList)gdi.FindControl("rlbUserlist");
            BindList(int.Parse(ddlLevel.SelectedItem.Value), rlbUserlist);
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
    }
}
