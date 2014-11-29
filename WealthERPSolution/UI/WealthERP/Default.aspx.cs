using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using VoCustomerProfiling;
using BoAdvisorProfiling;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using VoHostConfig;
using BoHostConfig;
using VoAdvisorProfiling;


namespace WealthERP
{
    public partial class _Default : System.Web.UI.Page
    {
        string logoPath = "";
        string branchLogoPath = "";
        UserVo userVo = new UserVo();
        string xmlPath = "";
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
        AdvisorVo advisorVo = new AdvisorVo();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
            GeneralConfigurationBo generalvonfigurationbo = new GeneralConfigurationBo();            
            HttpCookie UserPreference;
            string userTheme = string.Empty;

           

                if (Request.Cookies["UserPreference"] != null)
                {
                    // get the cookie
                    HttpCookie cookie = Request.Cookies["UserPreference"];
                    // get the cookie value
                    userTheme = Request.Cookies["UserPreference"].Values["UserTheme"];
                    Page.Theme = userTheme;
                    lnkBrowserIcon.Href = Request.Cookies["UserPreference"].Values["UserICOFilePath"];
                }

                if (Session["advisorVo"] != null)
                    advisorVo = (AdvisorVo)Session["advisorVo"];
                xmlPath = Server.MapPath(ConfigurationManager.AppSettings["xmllookuppath"]).ToString();
                if (advisorVo.HostId!=0)
                  generalconfigurationvo = generalvonfigurationbo.GetHostGeneralConfiguration(xmlPath,advisorVo.HostId);
                else
                    generalconfigurationvo = generalvonfigurationbo.GetHostGeneralConfiguration(xmlPath, 1000);

                Session[SessionContents.SAC_HostGeneralDetails] = generalconfigurationvo;
                if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                {
                    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];

                    if (!string.IsNullOrEmpty(generalconfigurationvo.DefaultTheme))
                    {
                        if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
                        {
                            if (string.IsNullOrEmpty(userTheme))
                                userTheme = generalconfigurationvo.DefaultTheme;
                        }
                        else if(Session["Theme"]!=null)
                        {
                            userTheme = Session["Theme"].ToString();
                        }
                        
                    }
                    if (!string.IsNullOrEmpty(generalconfigurationvo.ApplicationName))
                    {
                        Page.Title = generalconfigurationvo.ApplicationName;
                    }
                }
                //SET THE THEME FROM USER COOKIES OR DEFAULT
                Page.Theme = userTheme;
                Session["Theme"] = userTheme;
               
            
            //if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
            //{
            //    Session["Theme"] = "Maroon";
            //}

            //Page.Theme = Session["Theme"].ToString();
            if (Session["AdvisorPreferenceVo"] != null)
            {
                advisorPreferenceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
                Page.Title = advisorPreferenceVo.BrowserTitleBarName;
                lnkBrowserIcon.Href = "~//Images//favicon//" + advisorPreferenceVo.BrowserTitleBarIconImageName;
                hidUserLogOutPageUrl.Value = advisorPreferenceVo.LoginWidgetLogOutPageURL;

                UserPreference = new HttpCookie("UserPreference");
                UserPreference.Values["UserLoginPageURL"] = advisorPreferenceVo.WebSiteDomainName;
                if (!string.IsNullOrEmpty(Page.Theme))
                UserPreference.Values["UserTheme"] = Page.Theme.ToString();
                UserPreference.Values["UserICOFilePath"] = lnkBrowserIcon.Href.ToString();
                UserPreference.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(UserPreference);
            }


        }



        protected void Page_Load(object sender, EventArgs e)
        {
            RMVo rmVo = new RMVo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();         

            //tdTermsCondition.Visible = false;
            try
            {
                lblDate.Text = DateTime.Now.ToLongDateString();
                //imgLeftPlaceHolder.Style.Add("display", "none");
                imgCenterPlaceholder.Style.Add("display", "none");
                imgRightPlaceholder.Style.Add("display", "none");
                imgIFABanner.Style.Add("display", "none");                
                if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                {
                    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];


                    if (!IsPostBack)
                    {
                        imgLeftPlaceHolder.Src = "~/Images/LogoSBICAPSEC.jpg";
                        if (!string.IsNullOrEmpty(generalconfigurationvo.HostLogoPlacement))
                        {
                            if (generalconfigurationvo.HostLogoPlacement == "TopLeftCorner")
                            {
                                imgLeftPlaceHolder.Style.Add("display", "block");
                                imgLeftPlaceHolder.Src = "Images/" + generalconfigurationvo.HostLogo;
                            }
                            else if (generalconfigurationvo.HostLogoPlacement == "TopRightCorner")
                            {
                                imgRightPlaceholder.Style.Add("display", "block");
                                imgRightPlaceholder.Src = "Images/" + generalconfigurationvo.HostLogo;
                            }
                            else if (generalconfigurationvo.HostLogoPlacement == "TopCenter")
                            {
                                imgCenterPlaceholder.Style.Add("display", "block");
                                imgCenterPlaceholder.Src = "Images/" + generalconfigurationvo.HostLogo;
                            }
                        }
                    }
                    if (Session["userVo"] != null)
                    {
                        userVo = (UserVo)(Session["userVo"]);
                        rmVo = (RMVo)(Session[SessionContents.RmVo]);
                        //tdTermsCondition.Visible = true;
                        if (userVo.UserType == "Advisor")
                        {

                          
                            //tdGodaddySeal.Visible = true;
                        }
                        else
                        {
                            //tdTermsCondition.Visible = false;
                            
                        }

                        if ((!IsPostBack) && (userVo.UserType == "Customer"))
                        {
                            //tdTermsCondition.Visible = false;
                            tdDemo.Visible = false;
                            tdHelp.Visible = false;
                            lnkDemo.Visible = false;
                            lnkHelp.Visible = false;
                        }

                        lblUserName.Text = "Welcome " + " " + userVo.FirstName + " " + userVo.LastName;

                        if (userVo.PermisionList!=null && userVo.RoleList.Contains("Ops") && userVo.PermisionList.Count()>0)
                        {   
                            lblPermissionList.Visible = true;
                            foreach(string PName in userVo.PermisionList)
                            {
                                lblPermissionList.Text+= PName + ",";
                                
                            }

                            lblPermissionList.Text = lblPermissionList.Text.TrimEnd(',');
                        }
                        
                        lblSignOut.Text = "SignOut";
                        LinkButtonSignIn.Text = "";
                        if (Session[SessionContents.LogoPath] != null)
                            logoPath = (Session[SessionContents.LogoPath].ToString());
                        else
                        {
                            if (Session["advisorVo"] != null)
                                logoPath = "Images/" + ((AdvisorVo)Session["advisorVo"]).LogoPath;
                        }
                        if (Session[SessionContents.BranchLogoPath] != null)
                            branchLogoPath = (Session[SessionContents.BranchLogoPath].ToString());
                        else
                        {
                            if (rmVo != null)
                                branchLogoPath = "Images/" + (advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId))).LogoPath;
                        }

                        if (!IsPostBack)
                        {
                            if (!string.IsNullOrEmpty(generalconfigurationvo.AdviserLogoPlacement))
                            {
                                if (logoPath != "Images/" && logoPath != "")
                                {
                                    if (generalconfigurationvo.AdviserLogoPlacement == "TopLeftCorner")
                                    {
                                        imgLeftPlaceHolder.Style.Add("display", "block");
                                        imgLeftPlaceHolder.Src = logoPath;
                                    }
                                    else if (generalconfigurationvo.AdviserLogoPlacement == "TopRightCorner")
                                    {
                                        imgRightPlaceholder.Style.Add("display", "block");
                                        imgRightPlaceholder.Src = logoPath;
                                    }
                                    else if (generalconfigurationvo.AdviserLogoPlacement == "TopCenter")
                                    {
                                        imgCenterPlaceholder.Style.Add("display", "block");
                                        imgCenterPlaceholder.Src = logoPath;
                                    }
                                }
                            }




                            if (imgLeftPlaceHolder.Src != "" && imgRightPlaceholder.Src != "")
                            {
                                if (branchLogoPath != "Images/" && branchLogoPath != "")
                                {
                                    imgCenterPlaceholder.Style.Add("display", "block");
                                    imgCenterPlaceholder.Src = branchLogoPath;
                                }
                            }
                            else if (imgCenterPlaceholder.Src != "" && imgRightPlaceholder.Src != "")
                            {
                                if (branchLogoPath != "Images/" && branchLogoPath != "")
                                {
                                    imgLeftPlaceHolder.Style.Add("display", "block");
                                    imgLeftPlaceHolder.Src = branchLogoPath;
                                }
                            }
                            else if (imgLeftPlaceHolder.Src != "" && imgCenterPlaceholder.Src != "")
                            {
                                if (branchLogoPath != "Images/" && branchLogoPath != "")
                                {
                                    imgRightPlaceholder.Style.Add("display", "block");
                                    imgRightPlaceholder.Src = branchLogoPath;
                                }
                            }
                        }
                        if (advisorPreferenceVo.IsBannerEnabled)
                        {
                            imgPlaceholders.Visible = false;
                            tblIFALongBanner.Visible = true;
                            if (!string.IsNullOrEmpty(advisorPreferenceVo.BannerImageName))
                            {
                                imgIFABanner.Style.Add("display", "block");
                                imgIFABanner.Src = "Images/" + advisorPreferenceVo.BannerImageName;                               
                            }
                            else
                                imgIFABanner.Style.Add("display", "none");

                        }
                        else
                        {
                            tblIFALongBanner.Visible = false;
                        }
                        //if (logoPath != "Images/")
                        //{
                        //    AdvisorLogo.Src = logoPath;
                        //}
                        //if (branchLogoPath != "Images/")
                        //{
                        //    BranchLogo.Src = branchLogoPath;
                        //}

                        CustomerVo customerVo = new CustomerVo();
                        customerVo = (CustomerVo)(Session["CustomerVo"]);
                    }
                    else
                    {



                        if (!IsPostBack)
                        {
                            if (Request.QueryString["UserId"] != null)
                            {
                                string userId = Request.QueryString["UserId"].ToString();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loginloadcontrolfromDefault('Userlogin','" + userId + "','');", true);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "pageloadscript", "loginloadcontrolfromDefault('Userlogin','','');", true);
                            }
                        }
                    }
                }

                //if (AdvisorLogo.Src == "")
                //{
                //    AdvisorLogo.Style.Add("display", "none");
                //}

                //if (BranchLogo.Src == "")
                //{
                //    BranchLogo.Style.Add("display", "none");
                //}

                if (userVo.UserType != "Advisor") {
                    MenuItemCollection items = AdvisorMenu.Items;
                    foreach (MenuItem item in items) {
                        if (item.Value == @"Transact/Business online") { item.Text = ""; item.SeparatorImageUrl = null; }
                        if (item.Value == @"Repository") { item.Text = ""; item.SeparatorImageUrl = null; }
                        if (item.Value == @"Info links") { item.Text = ""; item.SeparatorImageUrl = null; }
                        if (item.Value == @"Price List") { item.Text = ""; item.SeparatorImageUrl = null; }
                        if (item.Value == @"Calculators") { item.Text = ""; item.SeparatorImageUrl = null; }
                    }
                    tdDemo.Visible = false;
                    tdHelp.Visible = false;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Default.aspx:PageLoad()");

                object[] objects = new object[1];
                objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void AdvisorMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Value == "Home")
            {
                if (userVo.RoleList.Contains("Admin"))
                {
                    e.Item.NavigateUrl = "javascript:loadfrommenu('IFAAdminMainDashboard','login')";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadlinks('AdvisorLeftPane','login');", true);
                }
                else if (userVo.RoleList.Contains("BM"))
                {
                    e.Item.NavigateUrl = "javascript:loadfrommenu('BMDashBoard','login')";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadlinks('AdvisorLeftPane','login');", true);
                }
                else if (userVo.RoleList.Contains("RM"))
                {
                    e.Item.NavigateUrl = "javascript:loadfrommenu('RMDashBoard','login')";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadlinks('AdvisorLeftPane','login');", true);
                }
                //else if (userVo.RoleList.Contains("Research"))
                //{
                //    e.Item.NavigateUrl = "javascript:loadfrommenu('ResearchDashboard','login')";
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadlinks('AdvisorLeftPane','login');", true);
                //}

                else
                {
                    e.Item.NavigateUrl = "javascript:loadfrommenu('AdvisorRMCustIndiDashboard','login')";
                }
            }
        }
                
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void AjaxSetSession(string key, string value)
        {
            try
            {
                HttpContext.Current.Session["Sessionkey"] = key;
                HttpContext.Current.Session["Sessionvalue"] = value;
            }
            catch (Exception ex)
            {
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void AjaxSetLinksSession(string key, string value)
        
        {
            try
            {
                HttpContext.Current.Session["SessionLinksKey"] = key;
                HttpContext.Current.Session["SessionLinksValue"] = value;
            }
            catch (Exception ex)
            {
            }
        }

        [System.Web.Services.WebMethod()]
        public static string AjaxGetCustomerType()
        {
            string CustType = string.Empty;

            CustomerVo customerVo = new CustomerVo();
            customerVo = (CustomerVo)(HttpContext.Current.Session["CustomerVo"]);
            CustType = customerVo.Type;

            return CustType;
        }

        protected void lnkIECompatibilityView_Click(object sender,EventArgs e)
        {
            Response.Redirect("~/Images/IECompatibility.jpg");
        }

        protected void lblSignOut_Click(object sender, EventArgs e)
        {
            string currentURL=string.Empty;
            if (Request.ServerVariables["HTTPS"].ToString() == "")
            {
                currentURL = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 4).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString() + Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
            if (currentURL.Contains("localhost"))
            {
                Session.Abandon();
                Response.Redirect(currentURL);
            }
            else
            {
                if (!string.IsNullOrEmpty(hidUserLogOutPageUrl.Value))
                {
                    Session.Abandon();
                    Response.Redirect(hidUserLogOutPageUrl.Value);
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
