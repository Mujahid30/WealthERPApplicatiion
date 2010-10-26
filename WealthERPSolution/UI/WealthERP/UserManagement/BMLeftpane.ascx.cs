using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using BoUser;
using WealthERP.Base;
using BoCommon;

namespace WealthERP.UserManagement
{
    public partial class BMLeftpane : System.Web.UI.UserControl
    {
        List<string> roleList = new List<string>();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        UserBo userBo = new UserBo();
        UserVo userVo;
        int count;
        RMVo rmVo = new RMVo();
        string UserName = "";
        string sourcepath = "";
        string branchLogoSourcePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            RMVo rmVo = new RMVo();
            userVo = (UserVo)Session["userVo"];
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            Session["rmVo"] = rmVo;
            UserName = userVo.FirstName + userVo.LastName;
            sourcepath = Session[SessionContents.LogoPath].ToString();
            branchLogoSourcePath = Session[SessionContents.BranchLogoPath].ToString();
            if (!IsPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('BMLeftpane');", true);
            }
        }

        /* For BM Left Treeview */

        protected void BMLeftTree_SelectedNodeChanged(object sender, EventArgs e)
        { 
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("BMLeftTree"))
            {
                SetNode();
            }
        }
        public void SetNode()
        {
            if (BMLeftTree.SelectedNode.Value == "Switch Roles")
            {
                roleList = userBo.GetUserRoles(userVo.UserId);
                count = roleList.Count;
                if (count == 3)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMBMDashBoard','login');", true);
                }
                if (count == 2)
                {
                    if (roleList.Contains("RM") && roleList.Contains("BM"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMRMDashBoard','login','" + UserName + "','" + sourcepath + "','" + branchLogoSourcePath + "');", true);
                    }
                    else if (roleList.Contains("BM") && roleList.Contains("Admin"))
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorBMDashBoard','login','" + UserName + "','" + sourcepath + "');", true);

                    }
                }
                if (count == 1 && userVo.UserType == "Branch Man")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMDashBoard','login');", true);
                }
            }
            else if (BMLeftTree.SelectedNode.Value == "Dashboard")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMDashBoard','login');", true);
            }
            //else if (BMLeftTree.SelectedNode.Value == "View Branch Details")
            //{
            //    Session["FromAdvisorView"] = "FromBMView";
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewBranchDetails','login');", true);
            //}
            else if (BMLeftTree.SelectedNode.Value == "Staff")
            {
                Session[SessionContents.CurrentUserRole] = "BM";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('ViewRM','login');", true);
            }
            else if (BMLeftTree.SelectedNode.Value == "Customer")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('BMCustomer','login');", true);
            }
            else if (BMLeftTree.SelectedNode.Value == "MFMIS")
            {
                Session["UserType"] = "bm";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RMAMCwiseMIS','login');", true);
            }
            else if (BMLeftTree.SelectedNode.Value == "EQMIS")
            {
                Session["UserType"] = "bm";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdviserEQMIS','login');", true);
            }
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            
        }

        /* End For BM Left Treeview */ 
    }
}
