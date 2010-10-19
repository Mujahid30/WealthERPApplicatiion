using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using WealthERP.Base;

namespace WealthERP.UserManagement
{
    public partial class AdvisorBMLeftpane : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            TreeNode tnRoles = new TreeNode();
            tnRoles = TreeView1.FindNode("Roles");
            tnRoles.SelectAction = TreeNodeSelectAction.None;
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
                    

                }
            }
            if (!IsPostBack)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorBMLeftpane');", true);
            }
            
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
                Session.Remove(SessionContents.LogoPath);      
                Session["refreshTheme"] = true;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loginloadcontrol('IFF')", true);
               
            }
        }
    }
}