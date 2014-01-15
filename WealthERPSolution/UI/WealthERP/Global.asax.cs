using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using System.Collections;
using System.Net.Cache;


namespace WealthERP
{

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();
           // Response.Headers.Add("X-UA-Compatible", "IE=Edge");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Hashtable currentLoginUserList = new Hashtable();
            if (Application["LoginUserList"] != null)
            {
                Application.Lock();
                currentLoginUserList = (Hashtable)Application["LoginUserList"];
                currentLoginUserList.Remove(Session.SessionID.ToString());
                Application["LoginUserList"] = currentLoginUserList;
                Application.UnLock();
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
