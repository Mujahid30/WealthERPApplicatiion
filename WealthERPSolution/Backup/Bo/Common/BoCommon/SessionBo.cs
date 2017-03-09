using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
namespace BoCommon
{
    public class SessionBo
    {

        /// <summary>
        /// Checks if session exists and redirects to session expiry page
        /// if it doesn't exist.
        /// </summary>
        public static void CheckSession()
        {
            if (System.Web.HttpContext.Current.Session["UserVo"] == null)
            {
                //Page currentPage = (Page)HttpContext.Current.CurrentHandler;

                //currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(),
                //    "pageloadscript", "loadcontrol('SessionExpired','none');", true);
                System.Web.HttpContext.Current.Response.Redirect("SessionExpired.aspx");
            }
        }
    }
}
