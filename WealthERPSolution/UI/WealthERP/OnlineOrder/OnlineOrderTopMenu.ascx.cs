using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web;

namespace WealthERP.OnlineOrder
{
    public partial class OnlineOrderTopPanel : System.Web.UI.UserControl
    {
        Dictionary<string, string> defaultProductPageSetting;

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    Session["refreshTheme"] = true;
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["PageDefaultSetting"] != null)
                defaultProductPageSetting = (Dictionary<string, string>)Session["PageDefaultSetting"];

            if (!Page.IsPostBack)
            {
                //Session["PageRefresh"] = "true";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueTransact','login');", true);
                SetProductPageDefaultSetting(defaultProductPageSetting);
            }

        }

        protected void RTSMFOrderMenuTransact_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSMFOrderMenuTransactNewPurchase": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderPurchaseTransType','login');", true);
                    break;

                case "RTSMFOrderMenuTransactAdditionalPurchase": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderAdditionalPurchase','login');", true);
                    break;

                case "RTSMFOrderMenuTransactRedeem": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderRdemptionTransType','login');", true);
                    break;

                case "RTSMFOrderMenuTransactSIP": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType','login');", true);
                    break;
                case "RTSMFOrderMenuTransactNFO": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueList','login');", true);
                    break;

                case "RTSMFOrderMenuTransactFMP": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueList','login');", true);
                    break;

            }
        }

        protected void RTSMFOrderMenuBooks_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSMFOrderMenuBooksOrderBook": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerMFOrderBookList','login');", true);
                    break;

                case "RTSMFOrderMenuBooksTransactionBook": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerTransactionBookList','login');", true);
                    break;

                case "RTSMFOrderMenuBooksSIPBook": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerSIPBookList','login');", true);
                    break;

                case "RTSMFOrderMenuBooksDividendBook": // add a new child tab
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType','login');", true);
                    break;
            }
        }

        protected void RTSMFOrderMenuHoldings_TabClick(object sender, RadTabStripEventArgs e)
        {

        }

        protected void SetProductPageDefaultSetting(Dictionary<string, string> defaultProductPageSetting)
        {
            string ProductMenuItem;
            if (defaultProductPageSetting.ContainsKey("ProductMenu"))
                SetProductMenu(defaultProductPageSetting["ProductMenu"].ToString());

            if (defaultProductPageSetting.ContainsKey("ProductMenuItem"))
                ProductMenuItem = defaultProductPageSetting["ProductMenuItem"];

            if (defaultProductPageSetting.ContainsKey("ProductMenuItemPage"))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','login');", true);
            }


        }

        private void SetProductMenu(string tableRow)
        {
            switch (tableRow)
            {
                case "trMFOrderMenuTransactTab":
                    trMFOrderMenuTransactTab.Visible = true;
                    break;
                case "trMFOrderMenuBooksTab":
                    trMFOrderMenuBooksTab.Visible = true;
                    break;
                case "trMFOrderMenuHoldingsTab":
                    trMFOrderMenuHoldingsTab.Visible = true;
                    break;
            }

        }
    }
}