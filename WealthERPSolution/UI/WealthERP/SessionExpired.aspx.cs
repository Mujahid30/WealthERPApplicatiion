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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
            GeneralConfigurationBo generalvonfigurationbo = new GeneralConfigurationBo();
            string xmlPath = "";
            xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
            generalconfigurationvo = generalvonfigurationbo.GetHostGeneralConfiguration(xmlPath);
            Session[SessionContents.SAC_HostGeneralDetails] = generalconfigurationvo;
            if (Session[SessionContents.SAC_HostGeneralDetails] != null)
            {
                generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];

                if (!string.IsNullOrEmpty(generalconfigurationvo.DefaultTheme))
                {
                    if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
                    {
                        Session["Theme"] = generalconfigurationvo.DefaultTheme;
                    }
                    Page.Theme = Session["Theme"].ToString();
                }
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
    }
}
