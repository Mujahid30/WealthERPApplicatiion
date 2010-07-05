using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WealthERP.General
{
    public partial class UserLogout : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Theme"].ToString() != "PCG")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "window.parent.location.href = window.parent.location.href;", true);
            }
            Session.Clear();
            Session.Abandon();



            lblMessage.Text = "You have been successfully logged off!";
        }
    }
}