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
    public partial class AdvisorRMLeftpane : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (!IsPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('AdvisorRMLeftpane');", true);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode.Value == "Advisor")
            {
                Session[SessionContents.CurrentUserRole] = "Admin";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IFAAdminMainDashboard','login');", true);
            }
            else if (TreeView1.SelectedNode.Value == "RM")
            {
                Session[SessionContents.CurrentUserRole] = "RM";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','login');", true);
            }
        }
    }
}