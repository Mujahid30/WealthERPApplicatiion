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
    public partial class BMRMLeftpane : System.Web.UI.UserControl
    {
        string branchLogoSourcePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            branchLogoSourcePath = Session[SessionContents.BranchLogoPath].ToString();
            SessionBo.CheckSession();
            if (!IsPostBack)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadtopmenu('BMRMLeftpane');", true);
            }
        }
        

        protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            if (e.Item.Value == "Branch Manager")
            {
                Session[SessionContents.CurrentUserRole] = "BM";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('BMDashBoard','login');", true);
            }
            else if (e.Item.Value == "RM")
            {
                if (Session["CurrentrmVo"] != null)
                {
                    Session.Remove("CurrentrmVo");
                }
                Session[SessionContents.CurrentUserRole] = "RM";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','login');", true);
            }
        }
       
    }
}