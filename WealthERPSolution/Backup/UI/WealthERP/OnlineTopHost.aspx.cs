using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using VoUser;

namespace WealthERP
{
    public partial class OnlineTopHost : System.Web.UI.Page
    {
        CustomerVo customerVo = new CustomerVo();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] != null)
                Page.Theme = Session["Theme"].ToString(); ;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pageid"] != null)
            {
                Session["Session_TopPanel_PageIdKey"] = Request.QueryString["pageid"].ToString();
            }
            else
            {
                string LinksKey = string.Empty;
                string LinksValue = string.Empty;

                if (HttpContext.Current.Session["Session_TopPanel_PageIdKey"] != null)
                    LinksKey = HttpContext.Current.Session["Session_TopPanel_PageIdKey"].ToString();
                if (HttpContext.Current.Session["Session_TopPanel_PageIdValue"] != null)
                    LinksValue = HttpContext.Current.Session["Session_TopPanel_PageIdValue"].ToString();

                Session[LinksKey] = LinksValue;

            }


            loadcontrol();
            customerVo = (CustomerVo)Session["CustomerVo"];
            if (customerVo != null && !string.IsNullOrEmpty(customerVo.AccountId))
                ScriptManager.RegisterStartupScript(this, typeof(Page), "GetRMSAvailableBalanceOnTopHost", "GetRMSAvailableBalance('" + customerVo.AccountId + "');", true);

        }

        protected void loadcontrol()
        {

            string pageID;

            if (Session["Session_TopPanel_PageIdKey"] != null)
            {
                if (Session["Session_TopPanel_PageIdKey"].ToString() == null || Session["Session_TopPanel_PageIdKey"].ToString() == "")
                {
                    return;
                }
                else
                {
                    pageID = Session["Session_TopPanel_PageIdKey"].ToString();
                }
                Session["Session_TopPanel_PageIdKey"] = pageID;
            }
            else if (Request.QueryString["pageid"] != null)
            {
                pageID = Request.QueryString["pageid"].ToString();
                Session["Session_TopPanel_PageIdKey"] = pageID;
            }
            else
            {
                return;
            }
             // Control Loading Script
            if (!string.IsNullOrEmpty(pageID) && pageID != "Top_Panel_PageID")
            {
                string path = Getpagepath(pageID);
                UserControl uc1 = new UserControl();

                uc1 = (UserControl)this.Page.LoadControl(path);
                uc1.ID = "ctrl_" + pageID;

                phTop.Controls.Clear();
                phTop.Controls.Add(uc1);
            }
        }

        // Put your logic here to get the Product list    8: }


        protected string Getpagepath(string pageID)
        {
            ResourceManager resourceMessages = new ResourceManager("WealthERP.ControlMapping", typeof(ControlLeftHost).Assembly);
            return resourceMessages.GetString(pageID);
        }
    }
}
