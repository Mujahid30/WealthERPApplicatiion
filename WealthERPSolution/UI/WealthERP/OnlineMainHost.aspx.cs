using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using System.Data;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;


using WealthERP.Base;


using VoUser;
using BoUser;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using VOAssociates;
using BOAssociates;
using BoCommon;
using BoCustomerProfiling;
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoOnlineOrderManagement;




namespace WealthERP
{
    public partial class OnlineMainHost : System.Web.UI.Page
    {
        UserVo userVo = new UserVo();
        AdvisorVo advisorVo = new AdvisorVo();
        CustomerVo customerVo = new CustomerVo();
        RMVo rmVo = new RMVo();
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
        string userAccountId;
        string productType;
        string isWerp;
        Dictionary<string, string> defaultProductPageSetting = new Dictionary<string, string>();
        HttpCookie UserPreference;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "SBIOnLine";
            Session["Theme"] = "SBIOnLine";
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                OnlineUserSessionBo.CheckSession();
            }
            else
            {

                if (Page.Request.Headers["x-Account-ID"] != null && Page.Request.Headers["x-Account-ID"] != "")
                {
                    userAccountId = Page.Request.Headers["x-Account-ID"].ToString();
                    if (Page.Request.Headers["x-SBI-PType"] != null && Page.Request.Headers["x-SBI-PType"] != "")
                    {
                        productType = Page.Request.Headers["x-SBI-PType"];
                        lblTest.Text = productType;
                    }
                }
                else if (Request.QueryString["x-Account-ID"] != null && Request.QueryString["x-Account-ID"] != "")
                {
                    userAccountId = Request.QueryString["x-Account-ID"].ToString();

                    if (Request.QueryString["x-SBI-PType"] != null && Request.QueryString["x-SBI-PType"] != "")
                    {
                        productType = Request.QueryString["x-SBI-PType"];
                        lblTest.Text = productType;
                    }
                }
                if (Request.QueryString["WERP"] != null)
                    isWerp = Request.QueryString["WERP"];
                

                if (!string.IsNullOrEmpty(userAccountId))
                {
                    if (string.IsNullOrEmpty(productType))
                        productType = "MF";
                    lblWelcomeUser.Text = "Account: " + userAccountId;
                }
                else
                {
                    productType = "NP";
                    //Not Authorize to see the page
                    SetDefaultPageSetting("NA");
                }

            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            bool isValidUser = false;
            if (!IsPostBack)
            {
               

                if (!string.IsNullOrEmpty(userAccountId))
                {
                    isValidUser = ValidateUserLogin(userAccountId, isWerp);
                }

                if (isValidUser)
                {
                    if(productType.ToUpper()=="MF")
                    {
                        mainmenuMF.Visible = true;
                        mainmenuNCD.Visible = false;
                        mainmenuIPO.Visible = false;
                        scroller.Visible = true;
                        SetScroller(productType.ToUpper());
                        //BindTransactionType("Online");
                        BindNewsHeading();
                    }
                    else if (productType.ToUpper() == "NCD")
                    {
                        mainmenuMF.Visible = false;
                        mainmenuNCD.Visible = true;
                        mainmenuIPO.Visible = false;
                        scroller.Visible = false;

                    }
                    else if (productType.ToUpper() == "IPO")
                    {
                        mainmenuMF.Visible = false;
                        mainmenuNCD.Visible = false;
                        mainmenuIPO.Visible = true;
                        scroller.Visible = false;
                    }
                    SetDemoLink(productType.ToUpper());
                    SetFAQLink(productType.ToUpper());
                    SetProductTypeMenu(productType.ToUpper());
                    SetDefaultPageSetting(productType.ToUpper());
                    Session["PageDefaultSetting"] = defaultProductPageSetting;
                    if (defaultProductPageSetting.ContainsKey("ProductMenuItemPage"))
                    {

                        if (defaultProductPageSetting.ContainsValue("NCD"))
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscriptabcd", @"LoadBottomPanelControl('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','?BondType=" + "FISDSD" + "');", true);
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscriptabcd", @"LoadBottomPanelControl('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','login');", true);
                        if (productType.ToUpper() == "MF")
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "PageLoadTransactTab", @"LoadTransactPanel('MFOrderPurchaseTransType','login');", true);
                    }
                }
                else
                {

                    mainmenuMF.Visible = false;
                    mainmenuNCD.Visible = false;
                    mainmenuIPO.Visible = false;
                    scroller.Visible = false;
                    SetDefaultPageSetting("NA");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscriptabcdnnn", "LoadTopPanelDefault('OnlineOrderDummyTopMenu');", true);
                }


            }

        }

        private void BindExchangeDropDown(string exchange)
        {

            string[] exchangeTypes = exchange.Split(',');
            int i = 0;
            ddlchannel.Items.Clear();
            foreach (string s in exchangeTypes)
            {

                if (!String.IsNullOrEmpty(s))
                {
                    ddlchannel.Items.Insert(i,new ListItem(s, s));
                    i++;
                }
            }
            if (exchange.Contains("Online"))
            {
                BindTransactionType("Online");
               
            }
            else
            {
                BindTransactionType("Demat");
              

            }

        }

        private void BindTransactionType(string exchangeType)
        {
            OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
            Dictionary<string, string> SchemetransactType;
            SchemetransactType = (Dictionary<string, string>)Session["SchemeExchangeee"];

            Dictionary<string, string> TransactionTypes = onlineOrderBo.GetTransactionTypeForExchange(exchangeType, SchemetransactType[exchangeType].ToString());
            DropDownList1.Items.Clear();
            DropDownList1.DataSource = TransactionTypes;
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataTextField = "Value";
            DropDownList1.DataBind();
        }
        protected void CreateExchangeDetailsSession(int schemeCode)
        {
            OnlineOrderBo OnlineOrderBo = new OnlineOrderBo();
            Dictionary<string, string> SchemetransactType;
            Session["MFSchemePlan"] = schemeCode;
            SchemetransactType = OnlineOrderBo.GetschemedetailonlineorDemate(schemeCode);
            Session["SchemeExchangeee"] = SchemetransactType;
            BindExchangeDropDown(SchemetransactType["exchange"].ToString());
        }
        protected void ddlchannel_onSelectedChanged(object sender, EventArgs e)
        {
            BindTransactionType(ddlchannel.SelectedValue);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscripRajiv", @"LoadTransactPanel('MFOrderPurchaseTransType" + ddlchannel.SelectedValue + "');", true);
        }
        protected void TextBox1_OnTextChanged(object sender, EventArgs e)
        {
            if (schemeCode.Value != "")
            {


                string exchangeType = string.Empty;
                CreateExchangeDetailsSession(Int32.Parse(schemeCode.Value));
                Dictionary<string, string> SchemetransactType;
                SchemetransactType = (Dictionary<string, string>)Session["SchemeExchangeee"];
                if (SchemetransactType["exchange"].ToString().Contains("Online"))
                {
                    exchangeType = "&exchangeType=Online";

                }
                else
                {
                    exchangeType = "&exchangeType=Demat";

                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "LoadTransactPanelFromSchemeSearch", "LoadTransactPanel('MFOrderPurchaseTransType" + exchangeType + "');", true);
                //if (!this.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "LoadTransactPanelFromSchemeSearch"))
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "LoadTransactPanelFromSchemeSearch", @"LoadTransactPanel('MFOrderPurchaseTransType" + exchangeType + "');", true);
                //}
            }
        }
        protected void SchemeSearch_OnTextChanged(object sender, EventArgs e)
        {
            if (schemeCode.Value != "")
            {


                string exchangeType = string.Empty;
                CreateExchangeDetailsSession(Int32.Parse(schemeCode.Value));
                Dictionary<string, string> SchemetransactType;
                SchemetransactType = (Dictionary<string, string>)Session["SchemeExchangeee"];
                if (SchemetransactType["exchange"].ToString().Contains("Online"))
                {
                    exchangeType = "&exchangeType=Online";

                }
                else
                {
                    exchangeType = "&exchangeType=Demat";

                }
                if (!this.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "pageloadscripRajiv"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscripRajiv", @"LoadTransactPanel('MFOrderPurchaseTransType" + exchangeType + "');", true);
                }
            }
        }
        private void SetProductTypeMenu(string productType)
        {
            switch (productType)
            {
                case "MF":
                    //divMFMenu.Visible = true;
                    lblOnlieProductType.Text = "Mutual Fund";
                    break;
                case "NCD":
                    //divNCDMenu.Visible = true;
                    lblOnlieProductType.Text = "NCD Order";
                    break;
                case "IPO":
                    //divIPOMenu.Visible = true;
                    lblOnlieProductType.Text = "IPO/FPO Order";
                    break;
            }
        }

        protected void SetDefaultPageSetting(string ProductType)
        {
            string innerHtml = String.Empty;
            defaultProductPageSetting.Clear();
            switch (ProductType.ToUpper())
            {

                case "MF":
                    defaultProductPageSetting.Add("ProductType", ProductType.ToUpper());
                    defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuMarketTab");
                    defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuHomeMarket");
                    defaultProductPageSetting.Add("ProductMenuItemPage", "MFSchemeRelateInformation");
                    break;
                case "NCD":
                    defaultProductPageSetting.Add("ProductType", ProductType.ToUpper());
                    defaultProductPageSetting.Add("ProductMenu", "trNCDOrderMenuTransactTab");
                    defaultProductPageSetting.Add("ProductMenuItem", "RTSNCDOrderMenuTransactNCDIssueList");
                    defaultProductPageSetting.Add("ProductMenuItemPage", "NCDIssueList");
                    break;
                case "IPO":
                    defaultProductPageSetting.Add("ProductType", ProductType.ToUpper());
                    defaultProductPageSetting.Add("ProductMenu", "trIPOOrderMenuTransactTab");
                    defaultProductPageSetting.Add("ProductMenuItem", "RTSIPOOrderMenuTransactIPOIssueList");
                    defaultProductPageSetting.Add("ProductMenuItemPage", "IPOIssueList");
                    break;
                case "NA":
                    defaultProductPageSetting.Add("ProductType", ProductType.ToUpper());
                    defaultProductPageSetting.Add("ProductMenu", "DUMMY");
                    defaultProductPageSetting.Add("ProductMenuItem", string.Empty);
                    defaultProductPageSetting.Add("ProductMenuItemPage", "OnlineOrderUnauthorizedUser");
                    break;

            }
            //Session["PageDefaultSetting"] = defaultProductPageSetting;
            //if (defaultProductPageSetting.ContainsKey("ProductMenuItemPage"))
            //{
               
            //    if (defaultProductPageSetting.ContainsValue("NCD"))
            //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrolInvestorMainPage('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','?BondType=" + "FISDSD" + "');", true);
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetDemo", @"loadcontrolInvestorMainPage('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','?BondType=" + "FISDSD" + "');", true);
            //    else
            //        ClientScript.RegisterStartupScript(this.GetType(), "hiya1", "alert('hi!')", true);
            //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscriptvvvv", "loadcontrolInvestorMainPage('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','login');", true);
            //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "SetDemo", @"loadcontrolInvestorMainPage('" + defaultProductPageSetting["ProductMenuItemPage"].ToString() + "','login');", true);
            //}
        }

        private bool ValidateUserLogin(string userAccountId, string isWerp)
        {

            string strOnlineAdviser = "0";
            bool isValidUser = false;
            UserBo userBo = new UserBo();
            AssociatesVO associatesVo = new AssociatesVO();
            AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
            AssociatesUserHeirarchyVo associatesUserHeirarchyVo = new AssociatesUserHeirarchyVo();
            AdvisorBo advisorBo = new AdvisorBo();

            AssociatesBo associatesBo = new AssociatesBo();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            CustomerBo customerBo = new CustomerBo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            strOnlineAdviser = ConfigurationSettings.AppSettings["ONLINE_ADVISER"].ToString();
            if (string.IsNullOrEmpty(isWerp))
                userVo = userBo.GetUserAccountDetails(userAccountId, Convert.ToInt32(strOnlineAdviser));
            else
            {
                advisorVo = (AdvisorVo)Session["advisorVo"];
                userVo = userBo.GetUserAccountDetails(userAccountId, advisorVo.advisorId);
            }

            if (!string.IsNullOrEmpty(isWerp))
            {
                if (userVo != null)
                {
                    customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                    isValidUser = true;
                }
                Session["CustomerVo"] = customerVo;

            }
            else if (userVo != null)
            {
                isValidUser = true;
                List<string> roleList = new List<string>();
                string branchLogoSourcePath;
                string sourcePath;
                string potentialHomePage = string.Empty;

                roleList = userBo.GetUserRoles(userVo.UserId);

                if (userVo.UserType == "Customer")
                {
                    customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                    advisorVo = advisorBo.GetAdvisor(advisorBranchBo.GetBranch(customerVo.BranchId).AdviserId);
                    if (customerVo.IsProspect == 0)
                    {
                        customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                        Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                    }
                    rmVo = advisorStaffBo.GetAdvisorStaffDetails(customerVo.RmId);

                    Session[SessionContents.LogoPath] = advisorVo.LogoPath;
                    Session[SessionContents.CurrentUserRole] = "Customer";
                    Session[SessionContents.UserTopRole] = "Customer";

                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                    sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                    Session[SessionContents.LogoPath] = sourcePath;
                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                    Session["CustomerVo"] = customerVo;
                    UserBo.AddLoginTrack(userVo.LoginId, string.Empty, true, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], HttpContext.Current.Request.UserAgent, userVo.UserId);
                }

                Session["UserVo"] = userVo;
                Session["advisorVo"] = advisorVo;
                Session["rmVo"] = rmVo;
                SetAdviserPreference();

                //Session["Theme"] = advisorVo.theme;
                //Session["refreshTheme"] = true;

                Session[SessionContents.LogoPath] = advisorVo.LogoPath;
            }
            return isValidUser;

        }

        private void SetAdviserPreference()
        {
            AdviserPreferenceBo adviserPreferenceBo = new AdviserPreferenceBo();
            string logoutPageURL = ConfigurationSettings.AppSettings["SSO_USER_LOGOUT_URL"];
            string loginPageURL = ConfigurationSettings.AppSettings["SSO_USER_LOGIN_URL"];
            advisorPreferenceVo = adviserPreferenceBo.GetAdviserPreference(advisorVo.advisorId);
            if (advisorPreferenceVo != null)
            {
                UserPreference = new HttpCookie("UserPreference");
                UserPreference.Values["UserTheme"] = "SBIOnLine";
                UserPreference.Values["OnlineUser"] = "Yes";
                hidUserLogOutPageUrl.Value = logoutPageURL;
                hidUserLogInPageUrl.Value = loginPageURL;

                if (!string.IsNullOrEmpty(loginPageURL))
                    UserPreference.Values["UserLoginPageURL"] = loginPageURL;

                if (!string.IsNullOrEmpty(logoutPageURL))
                    UserPreference.Values["UserLogOutPageURL"] = logoutPageURL;
                else if (!string.IsNullOrEmpty(advisorPreferenceVo.LoginWidgetLogOutPageURL))
                    UserPreference.Values["UserLogOutPageURL"] = advisorPreferenceVo.LoginWidgetLogOutPageURL;

                UserPreference.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(UserPreference);
                Session["AdvisorPreferenceVo"] = advisorPreferenceVo;
            }

        }

        private void InvalidateUser()
        {
            //lblIllegal.Text = "Username and Password does not match";
        }
        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            string currentURL = string.Empty;
            string logoutPageURL = ConfigurationSettings.AppSettings["SSO_USER_LOGOUT_URL"];
            string loginPageURL = ConfigurationSettings.AppSettings["SSO_USER_LOGIN_URL"];
            Session.Abandon();

            if (Request.ServerVariables["HTTPS"].ToString() == "")
            {
                currentURL = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 4).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString() + Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
            if (currentURL.Contains("localhost"))
            {
                currentURL = currentURL.Replace("OnlineMainHost", "Default");
                Response.Redirect(currentURL);
            }
            else
            {
                if (!string.IsNullOrEmpty(loginPageURL))
                {
                    Response.Redirect(logoutPageURL);
                    Response.Redirect(loginPageURL);
                }
                else if (!string.IsNullOrEmpty(hidUserLogInPageUrl.Value))
                {
                    Response.Redirect(hidUserLogOutPageUrl.Value);
                    Response.Redirect(hidUserLogInPageUrl.Value);
                }
                else
                {
                    currentURL = currentURL.Replace("OnlineMainHost", "Default");
                    Response.Redirect(currentURL);

                }
            }

            //Response.Redirect("http://sspr.sbicapstestlab.com/AGLogout");
        }

        protected void lnkMFOrderMenuHome_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("MF", "Market");
        } 

        protected void lnkMFOrderMenuTransact_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("MF", "Transact");
        }

        protected void lnkMFOrderMenuBooks_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("MF", "OrderBook");
        }

        protected void lnkMFOrderMenuHoldings_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("MF", "Hodings");
        }


        protected void lnkNCDOrderMenuTransact_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("NCD", "Transact");
        }
        protected void lnkNCDOrderMenuBooks_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("NCD", "OrderBook");
        }
        protected void lnkNCDOrderMenuHoldings_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("NCD", "Hodings");
        }

        protected void lnkIPOOrderMenuTransact_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("IPO", "Transact");
        }
        protected void lnkIPOOrderMenuBooks_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("IPO", "OrderBook");
        }
        protected void lnkIPOOrderMenuHoldings_Click(object sender, EventArgs e)
        {
            ProductMenuItemChange("IPO", "Hodings");
        }


        protected void ProductMenuItemChange(string ProductType, string menuType)
        {
           
            switch (ProductType)
            {
                case "MF":
                    {
                        switch (menuType)
                        {
                            case "Market":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuMarketTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuHomeMarket");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "MFSchemeRelateInformation");
                                break;

                            case "Transact":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuTransactTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuTransactNewPurchase");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "MFOrderPurchaseTransType");
                                break;
                            case "OrderBook":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuBooksTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuBooks");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "OnlineCustomerOrderandTransactionBook");
                                break;
                            case "Hodings":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuHoldingsTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuHoldings");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "CustomerMFUnitHoldingList");
                                break;
                        }
                         
                    }
                    break;
                case "NCD":
                    {
                        switch (menuType)
                        {
                            case "Transact":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trNCDOrderMenuTransactTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSNCDOrderMenuTransactNCDIssueList");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "NCDIssueList");
                                break;
                            case "OrderBook":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trNCDOrderMenuBooksTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSNCDOrderMenuBooksNCDBook");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "NCDIssueBooks");
                                break;
                            case "Hodings":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trNCDOrderMenuHoldingsTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSNCDOrderMenuHoldingsNCDHolding");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "NCDIssueHoldings");
                                break;
                        }
                        break;

                    }

                case "IPO":
                    {
                        switch (menuType)
                        {
                            case "Transact":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trIPOOrderMenuTransactTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSIPOOrderMenuTransactIPOIssueList");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "IPOIssueList");
                                break;
                            case "OrderBook":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trIPOOrderMenuBooksTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSIPOOrderMenuBooksIPOBook");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "CustomerIPOOrderBook");
                                break;
                            case "Hodings":
                                defaultProductPageSetting.Add("ProductType", ProductType);
                                defaultProductPageSetting.Add("ProductMenu", "trIPOOrderMenuHoldingsTab");
                                defaultProductPageSetting.Add("ProductMenuItem", "RTSIPOOrderMenuHoldingsIPOHolding");
                                defaultProductPageSetting.Add("ProductMenuItemPage", "CustomerIPOHolding");
                                break;
                        }
                        break;

                    }

            }
            
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void AjaxSetTopPanelSession(string key, string value)
        {
            try
            {
                HttpContext.Current.Session["Session_TopPanel_PageIdKey"] = key;
                HttpContext.Current.Session["Session_TopPanel_PageIdValue"] = value;
            }
            catch (Exception ex)
            {
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void AjaxSetBottomPanelSession(string key, string value)
        {
            try
            {
                HttpContext.Current.Session["Session_BottomPanel_PageIdKey"] = key;
                HttpContext.Current.Session["Session_BottomPanel_PageIdValue"] = value;
            }
            catch (Exception ex)
            {
            }
        }
        protected void BindNewsHeading()
        {
            OnlineMFSchemeDetailsBo OnlineMFSchemeDetailsBo = new OnlineMFSchemeDetailsBo();
            try
            {

                DataSet theDataSet = OnlineMFSchemeDetailsBo.GetAPIData(ConfigurationSettings.AppSettings["NEWS_HEADING"] + ConfigurationSettings.AppSettings["NEWS_COUNT"]);
                dlNews.DataSource = theDataSet.Tables[1];
                dlNews.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }
        protected void SetScroller(string productType)
        {
            OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
            string assetCategory = String.Empty;
            
            switch (productType)
            {
                case "MF":
                    assetCategory = "MF";
                    break;
                case "NCD":
                    assetCategory = "FI";
                    break;
                case "IPO":
                    assetCategory = "IP";
                    break;
            }
           
            string innerHtml = String.Empty;
            DataTable dt = new DataTable();

            dt = onlineOrderBo.GetAdvertisementData(assetCategory, "Scroll");
            dlScroller.DataSource = dt;
            dlScroller.DataBind();
            //innerHtml = @"<marquee style=""font-family: Arial;font-size: 14px;"">";
            //foreach (DataRow dr in dt.Rows)
            //{
            //    innerHtml += string.Format(@"{0} &nbsp; <b>|</b> &nbsp; ", dr["PUHD_HelpDetails"].ToString());
            //}
            //innerHtml += "</marquee>";
            //Label1.Text = innerHtml;
        
        }
        protected void SetDemoLink(string productType)
        {
            OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
            string assetCategory = String.Empty;
            string innerHtml = String.Empty;
            DataTable dt = new DataTable();
            switch (productType)
            {
                case "MF":
                    assetCategory="MF";
                    break;
                case "NCD":
                    assetCategory = "FI";
                    break;
                case "IPO":
                    assetCategory = "IP";
                    break;
            }
           
            dt = onlineOrderBo.GetAdvertisementData(assetCategory, "Demo");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "SetDemo", @"window.open('ReferenceFiles/HelpVideo.htm?Link=" + dt.Rows[0][0].ToString() + "', 'newwindow', 'width=655, height=520'); ", true);
            if (dt.Rows.Count > 0)
                lnkDemo.OnClientClick = @"window.open('ReferenceFiles/HelpVideo.htm?Link=" + dt.Rows[0][0].ToString() + "', 'newwindow', 'width=655, height=520'); ";
            
        }
        protected void SetFAQLink(string productType)
        {
           // if (Page.Request.Headers["x-SBI-PType"] != null && Page.Request.Headers["x-SBI-PType"] != "")
           // {
           //     productType = Page.Request.Headers["x-SBI-PType"];
           // }
           //else if (Request.QueryString["x-SBI-PType"] != null && Request.QueryString["x-SBI-PType"] != "")
           // {
           //     productType = Request.QueryString["x-SBI-PType"];
           // }

            OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
            string assetCategory = String.Empty;
          
            DataTable dt = new DataTable();
            switch (productType)
            {
                case "MF":
                    assetCategory = "MF";
                    break;
                case "NCD":
                    assetCategory = "FI";
                    break;
                case "IPO":
                    assetCategory = "IP";
                    break;
            }
            dt = onlineOrderBo.GetAdvertisementData(assetCategory, "FAQ");
            string path = ConfigurationManager.AppSettings["BANNER_IMAGE_PATH"].ToString();
            //Response.Redirect(path + dt.Rows[0][0].ToString());
            if(dt.Rows.Count>0)
                lnkFAQ.OnClientClick = @"window.open('" + path.Replace("~", "..") + dt.Rows[0][0].ToString() + "','_blank'); ";
        } 

    }
}
