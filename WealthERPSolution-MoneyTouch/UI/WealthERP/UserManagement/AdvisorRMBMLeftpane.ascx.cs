using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using VoAdvisorProfiling;
using BoCommon;
using WealthERP.Base;


namespace WealthERP.UserManagement
{
    public partial class AdvisorRMBMLeftpane : System.Web.UI.UserControl
    {
        UserVo userVo;
        RMVo rmVo = new RMVo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        int bmBranchId;
        protected void Page_Load(object sender, EventArgs e)
        {
            TreeNode tnRoles = new TreeNode();
            tnRoles = TreeView1.FindNode("Roles");
            tnRoles.SelectAction = TreeNodeSelectAction.None;
            SessionBo.CheckSession();
            userVo = (UserVo)Session["UserVo"];
            rmVo = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
            Session["rmVo"] = rmVo;
            bmBranchId = advisorBranchBo.GetBranchId(rmVo.RMId);
            if (bmBranchId ==0 )
            {

            }
            else
            {
                advisorBranchVo = advisorBranchBo.GetBranch(bmBranchId);
                Session["advisorBranchVo"] = advisorBranchVo;
            }           
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "SUPER_ADMIN")
                {
                    TreeNode parentNode = new TreeNode();
                    parentNode = TreeView1.FindNode("Roles");
                    TreeNode tnSuperAdmin = new TreeNode("SuperAdmin", "SuperAdmin");
                    if (parentNode.ChildNodes[0].Value.ToString() != "SuperAdmin")
                    {
                        parentNode.ChildNodes.AddAt(0, tnSuperAdmin);
                    }                    
                    Session["SuperAdmin_Status_Check"] = "1";
                }
            }
            if (!IsPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorRMBMLeftpane');", true);
            }
            //if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
            //{
            //    SetNode();
            //}
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Page.Request.Params.Get("__EVENTTARGET") != null && (Page.Request.Params.Get("__EVENTTARGET")).Contains("TreeView1"))
            {
                SetNode();
            }
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

           
        }
        public void SetNode()
        {
            if (TreeView1.SelectedNode.Value == "Advisor")
            {
                
                Session["refreshTheme"] = true;
                Session["FromUserLogin"] = "false";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IFAAdminMainDashboard','login');", true);
                
            }
            else if (TreeView1.SelectedNode.Value == "RM")
            {
               
                Session["refreshTheme"] = true;
                Session["FromUserLogin"] = "false";
                if (Session["CurrentrmVo"] != null)
                {
                   Session.Remove("CurrentrmVo");
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','login');", true);
                
            }
            else if (TreeView1.SelectedNode.Value == "Branch Manager")
            {
                
                Session["refreshTheme"] = true;
                Session["FromUserLogin"] = "false";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('BMDashBoard','login');", true);
                
            }
            else if (TreeView1.SelectedNode.Value == "SuperAdmin")
            {
                Session["userVo"] = Session["SuperAdminRetain"];
                Session.Remove("advisorVo");
                Session.Remove("rmVo");
                Session.Remove("customerVo");
                Session["refreshTheme"] = true;
                Session.Remove(SessionContents.LogoPath);      
                
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loginloadcontrol('IFF')", true);
            }
        }
    }
}