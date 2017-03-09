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
        string mFLnik = ConfigurationSettings.AppSettings["MF_LINK"];

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
            if (!Page.IsPostBack)
            {
                Response.Redirect(mFLnik);
            }
            else
            {
                Server.Transfer(mFLnik);
            }

        }

        protected void lblSignOut_Click(object sender, EventArgs e)
        {
            string loginPageURL = ConfigurationSettings.AppSettings["SSO_USER_LOGIN_URL"];
            string logoutPageURL = ConfigurationSettings.AppSettings["SSO_USER_LOGOUT_URL"];
            string mFLnik = ConfigurationSettings.AppSettings["MF_LINK"];
            HttpCookie cookie;
            //Session.Abandon();
                        
            //if (Request.Cookies["UserPreference"] != null)
            //{
            //    cookie = Request.Cookies["UserPreference"];
            //    if (!string.IsNullOrEmpty(Request.Cookies["UserPreference"].Values["OnlineUser"].ToString()))
            //        Response.Redirect(mFLnik);
            //    else
            //    {
            //        Response.Redirect(Request.Cookies["UserPreference"].Values["UserLogOutPageURL"].ToString());
            //        Response.Redirect(Request.Cookies["UserPreference"].Values["UserLoginPageURL"].ToString());
            //    }

            //}
            //else if (!string.IsNullOrEmpty(loginPageURL))
            //{
            //    Response.Redirect(logoutPageURL);
            //    Response.Redirect(loginPageURL);
            //}

        }
    }
}