using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BoCommon;

namespace WealthERP.OnlineOrder
{
    public partial class OnlineOrderDummyTopMenu : System.Web.UI.UserControl
    {
        Dictionary<string, string> defaultProductPageSetting;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionBo.CheckSession();
            if (!Page.IsPostBack)
            {
                if (Session["PageDefaultSetting"] != null)
                    defaultProductPageSetting = (Dictionary<string, string>)Session["PageDefaultSetting"];

                if (!Page.IsPostBack)
                {
                    if (defaultProductPageSetting.ContainsKey("ProductMenuItemPage"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','login');", true);
                    }

                }
            }

        }
    }
}