using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WealthERP.General
{
    public partial class SessionExpired : System.Web.UI.UserControl
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Cookies["UserPreference"] != null)
            {
                // get the cookie
                HttpCookie cookie = Request.Cookies["UserPreference"];
                // get the cookie value
                string userTheme = Request.Cookies["UserPreference"].Values["UserTheme"];
                Page.Theme = userTheme;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lblSignOut_Click(object sender, EventArgs e)
        {
            string userLoginPageURL = ConfigurationSettings.AppSettings["SSO_USER_LOGIN_URL"];           
            if (!string.IsNullOrEmpty(userLoginPageURL))
            {
                Session.Abandon();
                Response.Redirect(userLoginPageURL);
            }
            
        }
    }
}