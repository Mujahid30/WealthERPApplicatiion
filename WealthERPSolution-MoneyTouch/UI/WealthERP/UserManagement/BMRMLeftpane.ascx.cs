using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;

namespace WealthERP.UserManagement
{
    public partial class BMRMLeftpane : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (Session["role"] != null)
            {
                if (Session["role"].ToString() == "SUPER_ADMIN")
                {
                    TreeNode parentNode = new TreeNode();
                    parentNode = TreeView1.FindNode("Roles");
                    TreeNode tnSuperAdmin = new TreeNode("SuperAdmin", "SuperAdmin");
                    parentNode.ChildNodes.AddAt(0, tnSuperAdmin);

                }
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode.Value == "Branch Manager")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('BMDashBoard','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "RM")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','login');", true);
            }
        }
    }
}