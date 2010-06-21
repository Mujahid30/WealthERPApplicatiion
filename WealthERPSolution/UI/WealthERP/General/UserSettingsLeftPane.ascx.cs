using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoAdvisorProfiling;
using VoUser;
using BoUser;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.General
{
    public partial class UserSettingsLeftPane : System.Web.UI.UserControl
    {
        List<string> roleList = new List<string>();
        UserBo userBo = new UserBo();
        UserVo userVo;
        int count;
        string UserName = "";
        string sourcepath = "";
        string branchLogoSourcePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            userVo = (UserVo)Session["userVo"];
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strNodeValue = null;
            try
            {
                if (TreeView1.SelectedNode.Value.ToString() == "Home")
                {
                    roleList = userBo.GetUserRoles(userVo.UserId);
                    count = roleList.Count;
                    if (count == 3)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorRMBMDashBoard','none');", true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('IFAAdminDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                    }
                    else if (count == 2)
                    {
                        if (roleList.Contains("RM") && roleList.Contains("BM"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMRMDashBoard','login','" + UserName + "','" + sourcepath + "','" + branchLogoSourcePath + "');", true);
                        }
                        else if (roleList.Contains("RM") && roleList.Contains("Admin"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMDashBoard','login','" + UserName + "','" + sourcepath + "');", true);
                        }

                    }
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('CustomerIndividualDashboard','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Change Password")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ChangePassword','none');", true);
                }
                else if (TreeView1.SelectedNode.Value.ToString() == "Change Login Id")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ChangeLoginId','none');", true);
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

                FunctionInfo.Add("Method", "RMLeftPane.ascx.cs:TreeView1_SelectedNodeChanged()");

                object[] objects = new object[1];
                objects[0] = strNodeValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }
}