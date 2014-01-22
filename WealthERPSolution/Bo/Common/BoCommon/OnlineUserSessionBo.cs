using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace BoCommon
{
    public class OnlineUserSessionBo
    {
        public static void CheckSession()
        {
            if (System.Web.HttpContext.Current.Session["UserVo"] == null)
            {
                //Page currentPage = (Page)HttpContext.Current.CurrentHandler;

                //currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(),
                //    "pageloadscript", "loadcontrol('SessionExpired','none');", true);
                System.Web.HttpContext.Current.Response.Redirect("OnlineUserValidation.aspx");
            }
        }

    }
}
