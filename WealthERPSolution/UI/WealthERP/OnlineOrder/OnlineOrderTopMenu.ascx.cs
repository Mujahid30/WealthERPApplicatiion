using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web;
using BoCommon;

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
        {   OnlineUserSessionBo.CheckSession();
            //txtSchemeName_AutoCompleteExtender.ServiceMethod = "GetSchemeList";
            if (Session["PageDefaultSetting"] != null)
                defaultProductPageSetting = (Dictionary<string, string>)Session["PageDefaultSetting"];
            if (!Page.IsPostBack)
            {
                //Session["PageRefresh"] = "true";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueTransact','login');", true);
                SetProductPageDefaultSetting(defaultProductPageSetting);
            }

        }
        protected void RTSMFOrderMenuHome_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSMFOrderMenuHomeMarket": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFSchemeRelateInformation','login');", true);
                    break;

                case "RTSMFOrderMenuHomeSchemeResearch": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFSchemeDetails','login');", true);
                    break;

                case "RTSMFOrderMenuHomeSchemeCompare": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('OnlineMFSchemeCompare','login');", true);
                    break;
                case "RTSMFOrderMenuHomeNews": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('ProductOnlineFundNews','login');", true);
                    break;

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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderNFOTransType','login');", true);
                    break;
                case "RTSMFOrderMenuTransactSwitch": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSwitchTransType','login');", true);
                    break;
                case "RTSMFOrderMenuTransactSWP": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSWPTransType','login');", true);
                    break;
                case "RTSMFOrderMenuTransactSTP": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSTPTransType','login');", true);
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
                case "RTSMFOrderMenuBooksOrderBook": // add a new root tab--CustomerMFOrderBookList
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('OnlineCustomerOrderandTransactionBook','login');", true);
                    break;

                case "RTSMFOrderMenuBooksTransactionBook": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerTransactionBookList','login');", true);
                    break;

                case "RTSMFOrderMenuBooksSIPBook": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('SIPBookSummmaryList','?systematicType=" + "SIP" + "');", true);
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('SIPBookSummmaryList','login');", true);
                    break;
                case "RTSMFOrderMenuBooksSWPBook": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('SIPBookSummmaryList','?systematicType=" + "SWP" + "');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "SIPBookSummmaryList", "loadcontrol('SIPBookSummmaryList','?systematicType=" + "SWP" + "');", true);

                    break;
                case "RTSMFOrderMenuBooksDividendBook": // add a new child tab
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFOrderSIPTransType','login');", true);
                    break;
            }
        }

        protected void RTSMFOrderMenuHoldings_TabClick(object sender, RadTabStripEventArgs e)
        {

        }
        protected void RTSNCDOrderMenuTransact_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSNCDOrderMenuTransactNCDIssueList": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueList','login');", true);
                    break;

                case "RTSNCDOrderMenuTransactIssueTransact": // add a new child tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueTransact','login');", true);
                    break;

            }

        }
        protected void RTSNCDOrderMenuBooks_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSNCDOrderMenuBooksNCDBook": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueBooks','login');", true);
                    break;
            }


        }

        protected void RTSNCDOrderMenuHoldings_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSNCDOrderMenuHoldingsNCDHolding": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('NCDIssueHoldings','login');", true);
                    break;
            }


        }


        protected void RTSIPOOrderMenuTransact_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSIPOOrderMenuTransactIPOIssueList": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('IPOIssueList','login');", true);
                    break;

            }

        }
        protected void RTSIPPOOrderMenuBooks_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSIPOOrderMenuBooksIPOBook": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerIPOOrderBook','login');", true);
                    break;

            }

        }
        protected void RTSIPOOrderMenuHoldingsIPO_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "RTSIPOOrderMenuHoldingsIPO": // add a new root tab
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('CustomerIPOHolding','login');", true);
                    break;

            }

        }



        protected void SetProductPageDefaultSetting(Dictionary<string, string> defaultProductPageSetting)
        {
            string ProductMenuItem;

            if (defaultProductPageSetting.ContainsKey("ProductType") && defaultProductPageSetting.ContainsKey("ProductMenu"))
                SetProductMenu(defaultProductPageSetting["ProductType"].ToString(), defaultProductPageSetting["ProductMenu"].ToString());

            if (defaultProductPageSetting.ContainsKey("ProductMenuItem"))
                ProductMenuItem = defaultProductPageSetting["ProductMenuItem"];

            if (defaultProductPageSetting.ContainsKey("ProductMenuItemPage"))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','login');", true);
            }


        }


        private void SetProductMenu(string productType, string productMenu)
        {
            switch (productType.ToUpper())
            {
                case "MF":
                    {
                        tblMF.Visible = true;
                        trMFOrderMenuMarketTab.Visible = false;
                        trMFOrderMenuTransactTab.Visible = false;
                        trMFOrderMenuBooksTab.Visible = false;
                        trMFOrderMenuHoldingsTab.Visible = false;
                        switch (productMenu)
                        {
                            case "trMFOrderMenuMarketTab":
                                trMFOrderMenuMarketTab.Visible = true;
                                break;
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
                        break;
                    }

                case "NCD":
                    {
                        tblNCD.Visible = true;
                        switch (productMenu)
                        {
                            case "trNCDOrderMenuTransactTab":
                                trNCDOrderMenuTransactTab.Visible = true;
                                break;
                            case "trNCDOrderMenuBooksTab":
                                trNCDOrderMenuBooksTab.Visible = true;
                                break;
                            case "trNCDOrderMenuHoldingsTab":
                                trNCDOrderMenuHoldingsTab.Visible = true;
                                break;
                        }
                        break;
                    }

                case "IPO":
                    {
                        tblIPO.Visible = true;
                        switch (productMenu)
                        {
                            case "trIPOOrderMenuTransactTab":
                                trIPOOrderMenuTransactTab.Visible = true;
                                break;
                            case "trIPOOrderMenuBooksTab":
                                trIPOOrderMenuBooksTab.Visible = true;
                                break;
                            case "trIPOOrderMenuHoldingsTab":
                                trIPOOrderMenuHoldingsTab.Visible = true;
                                break;
                        }
                        break;
                    }

            }

        }
        protected void SchemeSearch_OnTextChanged(object sender, EventArgs e)
        {
            if (schemeCode.Value != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "LoadBottomPanelControl('MFSchemeDetails','&schemeCode=" + schemeCode.Value + "')", true);
            }
        }
    }
}