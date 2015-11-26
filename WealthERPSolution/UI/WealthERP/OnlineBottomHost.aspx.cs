using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;

namespace WealthERP
{
    public partial class OnlineBottomHost : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] != null)
                Page.Theme = Session["Theme"].ToString(); ;
        }
        string path = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["refreshTheme"] != null && Convert.ToBoolean(Session["refreshTheme"]) == true)
            //{
            //    Session["refreshTheme"] = null;
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "window.parent.location.href = window.parent.location.href;", true);
            //}

            if (Request.QueryString["pageid"] != null)
            {
                Session["Session_BottomPanel_PageIdKey"] = Request.QueryString["pageid"].ToString();
            }
            else
            {
                string key = string.Empty;
                string value = string.Empty;

                if (HttpContext.Current.Session["Session_BottomPanel_PageIdKey"] != null)
                    key = HttpContext.Current.Session["Session_BottomPanel_PageIdKey"].ToString();
                if (HttpContext.Current.Session["Session_BottomPanel_PageIdValue"] != null)
                    value = HttpContext.Current.Session["Session_BottomPanel_PageIdValue"].ToString();
                Session[key] = value;
            }

            loadcontrol();
            if (!this.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "SetBottomFrameHeight"))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SetBottomFrameHeight", "calcIFrameHeight('bottomframe');", true);
            }
        }

        protected void loadcontrol()
        {

            string pageID = "";

            if (Session["Session_BottomPanel_PageIdKey"] != null)
            {
                if (Session["Session_BottomPanel_PageIdKey"].ToString() == null || Session["Session_BottomPanel_PageIdKey"].ToString() == "")
                {
                    return;
                }
                else
                {
                    pageID = Session["Session_BottomPanel_PageIdKey"].ToString();
                }
            }
            else if (Request.QueryString["pageid"] != null)
            {   // If Session is null, get the pageid from the querystring
                pageID = Request.QueryString["pageid"].ToString();
            }
            else
            {
                return;
            }

            //BreadCrumbs Loading Scripts
            string bcID = "";
            if (Request.QueryString["action"] != null)
            {
                bcID = pageID + "_" + Request.QueryString["action"].ToString();
            }
            else
            {
                bcID = pageID;
            }

            // Control Loading Script
            if (!string.IsNullOrEmpty(pageID) && pageID != "Bottom_Panel_PageID")
            {
                path = Getpagepath(pageID);
                UserControl uc1 = new UserControl();

                uc1 = (UserControl)this.Page.LoadControl(path);
                uc1.ID = "ctrl_" + pageID;



                phBottom.Controls.Clear();
                phBottom.Controls.Add(uc1);
            }

        }

        protected string Getpagepath(string pageID)
        {
            ResourceManager resourceMessages = new ResourceManager("WealthERP.ControlMapping", typeof(ControlHost).Assembly);
            return resourceMessages.GetString(pageID);
        }
    }
}
