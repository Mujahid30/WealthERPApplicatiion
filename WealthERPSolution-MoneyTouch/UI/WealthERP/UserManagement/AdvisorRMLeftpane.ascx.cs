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
       

        protected void RadPanelBar1_ItemClick(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            try
            {
                if (e.Item.Value == "Advisor")
                {
                    Session[SessionContents.CurrentUserRole] = "Admin";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('IFAAdminMainDashboard','login');", true);
                }
                else if (e.Item.Value == "RM")
                {
                    Session[SessionContents.CurrentUserRole] = "RM";
                    if (Session["CurrentrmVo"] != null)
                    {
                        Session.Remove("CurrentrmVo");
                    }
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadcontrol('RMDashBoard','login');", true);
                }

            }
            catch (Exception ex)
            {
                bool value = false;
            }

        }
    }
}