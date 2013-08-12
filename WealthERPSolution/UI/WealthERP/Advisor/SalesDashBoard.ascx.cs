using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoCommon;
using VoUser;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoWerpAdmin;
using System.Data;


namespace WealthERP.Advisor
{
    public partial class SalesDashBoard : System.Web.UI.UserControl
    {

        AdvisorVo advisorVo;
        AdviserMaintenanceBo advisermaintanencebo = new AdviserMaintenanceBo();
        UserVo userVo;
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionBo.CheckSession();
            advisorVo = (AdvisorVo)Session[SessionContents.AdvisorVo];
            userVo = (UserVo)Session[SessionContents.UserVo];

        }


        public void imgClientsClick_OnClick(object sender, EventArgs e)
        {
            Session["Customer"] = "Customer";
            Session["NodeType"] = "AdviserCustomer";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            if (advisorVo.IsOpsEnable == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
        }
        public void lnkbtnClientLink_OnClick(object sender, EventArgs e)
        {
            Session["Customer"] = "Customer";
            Session["NodeType"] = "AdviserCustomer";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            if (advisorVo.IsOpsEnable == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadCustomerGrid", "loadcontrol('AdviserCustomer','login');", true);
            }
        }
      
        public void imgOrderentry_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('MFOrderEntry','login');", true);

        }
        public void lnkbtnOrderEntry_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "MFOrderEntry";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('MFOrderEntry','login');", true);

        }
        public void imgbtnFPClients_OnClick(object sender, EventArgs e)
        {
            Session["NodeType"] = "AddProspectList";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('AddProspectList','login');", true);

        }
        public void lnkbtnFPClients_OnClick(object sender, EventArgs e)
        {

            Session["NodeType"] = "AddProspectList";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('AdvisorLeftPane','login');", true);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoadOrderentry", "loadcontrol('AddProspectList','login');", true);

        }
    }
}