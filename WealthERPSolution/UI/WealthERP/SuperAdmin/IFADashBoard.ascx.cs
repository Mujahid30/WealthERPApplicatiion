using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP.SuperAdmin
{
    public partial class IFADashBoard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkbtnIFAAddLink_OnClick(object sender, EventArgs e)
        {
            Session["IFFAdd"] = "Add";
            Session.Remove("advisorVo");
            Session.Remove("IDs");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadIFFAdd", "loadcontrol('IFFAdd','login');", true);
        }

        protected void imgIFAAddClick_OnClick(object sender, ImageClickEventArgs e)
        {
            Session["IFFAdd"] = "Add";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadIFFAdd", "loadcontrol('IFFAdd','login');", true);
        }

        protected void imgIFAList_OnClick(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadIFFGrid", "loadcontrol('IFADashBoard','login');", true);
        }

        protected void lnkbtnIFAList_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadIFFGrid", "loadcontrol('IFADashBoard','login');", true);
        }

        protected void imgUserManagement_OnClick(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadIFFUserManagement", "loadcontrol('IFFUserManagement','login');", true);
        }

        protected void lnkbtnUserManagement_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadIFFUserManagement", "loadcontrol('IFFUserManagement','login');", true);
        }

        protected void imgManualValuation_OnClick(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadManualValuation", "loadcontrol('ManualValuation','login');", true);
        }

        protected void lnkbtnManualValuation_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadManualValuation", "loadcontrol('ManualValuation','login');", true);
        }

        protected void imgValuationMonitor_OnClick(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadDailyValuationMonitor", "loadcontrol('DailyValuationMonitor','login');", true);
        }

        protected void lnkbtnValuationMonitor_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadDailyValuationMonitor", "loadcontrol('DailyValuationMonitor','login');", true);
        }

        protected void imgbtnMsgBroadcast_OnClick(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadSuperAdminMessageBroadcast", "loadcontrol('SuperAdminMessageBroadcast','login');", true);
        }

        protected void lnkbtnMsgBroadcast_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadSuperAdminMessageBroadcast", "loadcontrol('SuperAdminMessageBroadcast','login');", true);
        }
    }
}