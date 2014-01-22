using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP
{
    public partial class OnlineUserValidation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Loaded"] != null && Convert.ToBoolean(Session["Loaded"]) == true)
            {
                Session["Loaded"] = false;
                Response.Redirect("OnlineMainHost.aspx");
            }
            else
            {
                Session["Loaded"] = true;
                //Register a javascript to set the parent

                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "pageloadscript", "window.parent.location.href = 'OnlineUserValidation.aspx'", true);
            }
        }
    }
}
