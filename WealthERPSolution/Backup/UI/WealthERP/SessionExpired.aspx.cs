using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WealthERP.Base;
using VoHostConfig;
using BoHostConfig;
using System.Configuration;

namespace WealthERP
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    //GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
        //    //GeneralConfigurationBo generalvonfigurationbo = new GeneralConfigurationBo();
        //    //string xmlPath = "";
        //    //xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
        //    //generalconfigurationvo = generalvonfigurationbo.GetHostGeneralConfiguration(xmlPath,1000);
        //    //Session[SessionContents.SAC_HostGeneralDetails] = generalconfigurationvo;
        //    //if (Session[SessionContents.SAC_HostGeneralDetails] != null)
        //    //{
        //    //    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];

        //    //    if (!string.IsNullOrEmpty(generalconfigurationvo.DefaultTheme))
        //    //    {
        //    //        if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
        //    //        {
        //    //            Session["Theme"] = generalconfigurationvo.DefaultTheme;
        //    //        }
        //    //        Page.Theme = Session["Theme"].ToString();
        //    //    }
        //    //}
        //}

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

        protected void lblSignOut_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["UserPreference"] != null)
            {
                // get the cookie
                HttpCookie cookie = Request.Cookies["UserPreference"];
                // get the cookie value
                string userLoginPageURL = Request.Cookies["UserPreference"].Values["UserLoginPageURL"];
                string currentURL=string.Empty;
                if (Request.ServerVariables["HTTPS"].ToString() == "")
                {
                    currentURL = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 4).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString() + Request.ServerVariables["SCRIPT_NAME"].ToString();
                }
                if (currentURL.Contains("localhost"))
                {
                    currentURL = currentURL.Replace("SessionExpired", "Default");
                    Session.Abandon();
                    Response.Redirect(currentURL);
                }
                else
                {

                    if (!string.IsNullOrEmpty(userLoginPageURL))
                    {
                        Session.Abandon();
                        Response.Redirect(userLoginPageURL);
                    }
                    else
                    {
                        Session.Abandon();
                        Response.Redirect("https://app.wealtherp.com/");
                    }
                }
            }
        }
    }
}
