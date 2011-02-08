using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;

namespace WealthERP
{
    public partial class ControlLeftHost : System.Web.UI.Page
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
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(new DateTime(1990, 1, 1));
            if (Request.QueryString["pageid"] != null)
            {
                Session["Current_LinkID"] = Request.QueryString["pageid"].ToString();
            }
            else
            {
                string LinksKey = string.Empty;
                string LinksValue = string.Empty;

                if (HttpContext.Current.Session["SessionLinksKey"] != null)
                    LinksKey = HttpContext.Current.Session["SessionLinksKey"].ToString();
                if (HttpContext.Current.Session["SessionLinksValue"] != null)
                    LinksValue = HttpContext.Current.Session["SessionLinksValue"].ToString();

                Session[LinksKey] = LinksValue;
                //Session[_Default.SessionLinksKey] = _Default.SessionLinksValue;
            }


            loadcontrol();
        }
        protected void loadcontrol()
        {

            string pageID;

            if (Session["SessionLinksKey"] != null)
            {
                if (Session[Session["SessionLinksKey"].ToString()] == null || Session[Session["SessionLinksKey"].ToString()].ToString() == "")
                {
                    return;
                }
                else
                {
                    pageID = Session[Session["SessionLinksKey"].ToString()].ToString();
                }
                Session["Current_Link"] = pageID;
            }
            else if (Request.QueryString["pageid"] != null)
            {
                pageID = Request.QueryString["pageid"].ToString();
                Session["Current_Link"] = pageID;
            }
            else
            {
                return;
            }

            string path = Getpagepath(pageID);
            UserControl uc1 = new UserControl();

            uc1 = (UserControl)this.Page.LoadControl(path);
            uc1.ID = "ctrl_" + pageID;

            phLeft.Controls.Clear();
            phLeft.Controls.Add(uc1);
        }

        // Put your logic here to get the Product list    8: }


        protected string Getpagepath(string pageID)
        {
            ResourceManager resourceMessages = new ResourceManager("WealthERP.ControlMapping", typeof(ControlLeftHost).Assembly);
            return resourceMessages.GetString(pageID);
        }
    }
}
