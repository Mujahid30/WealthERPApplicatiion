using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BoUser;
using System.Collections;
using System.Text;
using BoCommon;
using VoUser;
using System.Configuration;

namespace WealthERP.General
{
    public partial class UserSessionManager : System.Web.UI.UserControl
    {

        UserBo userBo = new UserBo();
        UserVo userVo = new UserVo();
        protected void Page_Load(object sender, EventArgs e)
        {          
            userVo = (UserVo)Session["userVo"];
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userSessionPwd = ConfigurationSettings.AppSettings["USER_SESSION_REMOVE_PWD"];
            if (txtPassword.Text.Trim() == userSessionPwd)
            {
                tblPasswordSection.Visible = false;
                tblUserSessionList.Visible = true;
                BindUserSessionGrid(GetAllActiveUserIds());
            }
            else
            {
                trPassword.Visible = true;
            }

        }

        private void BindUserSessionGrid(string strUserIds)
        {
            DataTable dtUserDetails = new DataTable();
            dtUserDetails = userBo.GetUserDetails(strUserIds);

            if (dtUserDetails.Rows.Count > 0)
            {
                if (Cache["UserSession"] != null)
                    Cache.Remove("UserSession");

                Cache.Insert("UserSession", dtUserDetails);
            }

            RadGridSessionManager.DataSource = dtUserDetails;
            RadGridSessionManager.DataBind();

        }

        private string GetAllActiveUserIds()
        {
            Hashtable currentLoginUserList = new Hashtable();
            currentLoginUserList = (Hashtable)Application["LoginUserList"];
            StringBuilder strActiveUserList = new StringBuilder();
            if (currentLoginUserList != null)
            {
                foreach (DictionaryEntry item in currentLoginUserList)
                {
                    strActiveUserList.Append(item.Value);
                    strActiveUserList.Append("~");
                }
            }

            return strActiveUserList.ToString();

        }

        protected void RadGridSessionManager_OnNeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtUserSessions = new DataTable();
            dtUserSessions = (DataTable)Cache["UserSession"];
            if (dtUserSessions != null)
            {
                RadGridSessionManager.DataSource = dtUserSessions;
                RadGridSessionManager.Visible = true;

            }

        }

        protected void RadGridSessionManager_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string userId = RadGridSessionManager.MasterTableView.DataKeyValues[e.Item.ItemIndex]["U_UserId"].ToString();
                Hashtable currentLoginUserList = new Hashtable();
                if (Application["LoginUserList"] != null)
                {

                    currentLoginUserList = (Hashtable)Application["LoginUserList"];
                    foreach (DictionaryEntry item in currentLoginUserList)
                    {
                        if (item.Value.ToString() == userId)
                        {
                            Session.Contents.Remove(item.Key.ToString());
                            currentLoginUserList.Remove(item.Key);
                            Application["LoginUserList"] = currentLoginUserList;
                            if (userVo != null)
                            {
                                if (userId == userVo.UserId.ToString())
                                {
                                    Session.Abandon();
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                }
                                else
                                {
                                    BindUserSessionGrid(GetAllActiveUserIds());
                                }
                            }
                            else
                                BindUserSessionGrid(GetAllActiveUserIds());
                            
                            return;
                        }
                    }



                }

            }

        }

    }
}