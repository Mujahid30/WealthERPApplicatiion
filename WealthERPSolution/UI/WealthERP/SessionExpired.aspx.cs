using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
            {
                Session["Theme"] = "Maroon";
            }
            Page.Theme = Session["Theme"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Loaded"] != null && Convert.ToBoolean(Session["Loaded"]) == true)
            {
                Session["Loaded"] = false;
            }
            else
            {
                Session["Loaded"] = true;
                //Register a javascript to set the parent

                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "pageloadscript", "window.parent.location.href = 'SessionExpired.aspx'", true);
            }
        }
    }
}
