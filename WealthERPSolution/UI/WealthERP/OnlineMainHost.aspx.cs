using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "SBIOnLine";
            Session["Theme"] = "SBIOnLine";
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            if (Page.Request.Headers["x-Account-ID"] != null && Page.Request.Headers["x-Account-ID"] != "")
            {
                userAccountId = Page.Request.Headers["x-Account-ID"].ToString();
                if (Page.Request.Headers["x-SBI-PType"] != null && Page.Request.Headers["x-SBI-PType"] != "")
                {
                    productType = Page.Request.Headers["x-SBI-PType"];
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
            //Testing User

            //if (string.IsNullOrEmpty(userAccountId))
            //    userAccountId = "ESI206315";
            //if (productType != "MF")
            //    productType = "MF";

            if (!string.IsNullOrEmpty(userAccountId))
            {
                if (string.IsNullOrEmpty(productType))
                    productType = "MF";
                if (!Page.IsPostBack)
                {
                    SetProductTypeMenu(productType.ToUpper());
                    SetDefaultPageSetting(productType.ToUpper());

                }
                lblWelcomeUser.Text = "Account: " + userAccountId;
            }
            else
            {
                //Not Authorize to see the page
                SetDefaultPageSetting("NA");
            }


        }



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["UserId"] != null)
                {
                    if (!string.IsNullOrEmpty(userAccountId))
                    {
                        //ValidateUserLogin(userAccountId);
                        //string userId = Request.QueryString["UserId"].ToString();
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loginloadcontrolfromDefault('OnlineOrderTopMenu','" + userId + "','');", true);
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(userAccountId))
                    {
                        ValidateUserLogin(userAccountId, isWerp);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscriptabcd", "LoadTopPanelDefault('OnlineOrderTopMenu');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscriptabcdnnn", "LoadTopPanelDefault('OnlineOrderDummyTopMenu');", true);
                    }

                }
            }

        }

        private void SetProductTypeMenu(string productType)
        {
            switch (productType)
            {
                case "MF":
                    divMFMenu.Visible = true;
                    lblOnlieProductType.Text = "Mutual Fund Order";
                    break;
                case "NCD":
                    divNCDMenu.Visible = true;
                    lblOnlieProductType.Text = "NCD Order";
                    break;
                case "IPO":
                    divIPOMenu.Visible = true;
                    lblOnlieProductType.Text = "IPO Order";
                    break;
            }
        }

        protected void SetDefaultPageSetting(string ProductType)
        {
            switch (ProductType.ToUpper())
            {
                case "MF":
                    defaultProductPageSetting.Add("ProductType", ProductType.ToUpper());
                    defaultProductPageSetting.Add("ProductMenu", "trMFOrderMenuTransactTab");
                    defaultProductPageSetting.Add("ProductMenuItem", "RTSMFOrderMenuTransactNewPurchase");
                    defaultProductPageSetting.Add("ProductMenuItemPage", "MFOrderPurchaseTransType");
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
                    defaultProductPageSetting.Add("ProductMenu", string.Empty);
                    defaultProductPageSetting.Add("ProductMenuItem", string.Empty);
                    defaultProductPageSetting.Add("ProductMenuItemPage", "OnlineOrderUnauthorizedUser");
                    break;

            }
            Session["PageDefaultSetting"] = defaultProductPageSetting;
        }

        private void ValidateUserLogin(string userAccountId, string isWerp)
        {
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

            userVo = userBo.GetUserAccountDetails(userAccountId);

            if (!string.IsNullOrEmpty(isWerp))
            {
                if (userVo != null)
                {
                    customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                }
                Session["CustomerVo"] = customerVo;

            }
            else if (userVo != null)
            {
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
                }

                Session["UserVo"] = userVo;
                Session["advisorVo"] = advisorVo;
                Session["rmVo"] = rmVo;
                SetAdviserPreference();

                //Session["Theme"] = advisorVo.theme;
                //Session["refreshTheme"] = true;

                Session[SessionContents.LogoPath] = advisorVo.LogoPath;
            }
            else
            {
                InvalidateUser();
            }

        }

        private void SetAdviserPreference()
        {
            AdviserPreferenceBo adviserPreferenceBo = new AdviserPreferenceBo();
            advisorPreferenceVo = adviserPreferenceBo.GetAdviserPreference(advisorVo.advisorId);
            Session["AdvisorPreferenceVo"] = advisorPreferenceVo;

        }

        private void InvalidateUser()
        {
            //lblIllegal.Text = "Username and Password does not match";
        }
        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://sspr.sbicapstestlab.com/AGLogout ");
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
                                defaultProductPageSetting.Add("ProductMenuItemPage", "CustomerMFOrderBookList");
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
                            //case "Hodings":
                            //    defaultProductPageSetting.Add("ProductType", ProductType);
                            //    defaultProductPageSetting.Add("ProductMenu", "trNCDOrderMenuHoldingsTab");
                            //    defaultProductPageSetting.Add("ProductMenuItem", "RTSNCDOrderMenuHoldingsNCDHolding");
                            //    defaultProductPageSetting.Add("ProductMenuItemPage", "NCDIssueHoldings");
                            //    break;
                        }
                        break;

                    }

            }
            Session["PageDefaultSetting"] = defaultProductPageSetting;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscriptabcd", "LoadTopPanelDefault('OnlineOrderTopMenu');", true);

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


    }
}
