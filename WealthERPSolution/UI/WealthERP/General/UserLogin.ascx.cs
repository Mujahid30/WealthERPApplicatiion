using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using VoAdvisorProfiling;
using VoUser;
using BoCustomerPortfolio;
using WealthERP.Advisor;
using WealthERP.Customer;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using WealthERP.Base;
using BoCommon;
using System.Data;
using BoHostConfig;
using VoHostConfig;
using System.Configuration;
using VoCustomerPortfolio;
using BOAssociates;
using VOAssociates;

namespace WealthERP.General
{
    public partial class UserLogin : System.Web.UI.UserControl
    {
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
        AdviserPreferenceBo adviserPreferenceBo = new AdviserPreferenceBo();
        AssociatesBo associatesBo = new AssociatesBo();
        AssociatesVO associatesVo = new AssociatesVO();
        AssociatesUserHeirarchyVo associatesUserHeirarchyVo = new AssociatesUserHeirarchyVo();
        AdvisorVo advisorVo = new AdvisorVo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorBo advisorBo = new AdvisorBo();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
        AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
        RMVo rmVo = new RMVo();

        CustomerBo customerBo = new CustomerBo();
        CustomerVo customerVo = new CustomerVo();

        string strUserTheme;
        string currentPageUrl;
        int userId = 0;
        protected void Page_Init(object sender, EventArgs e)
        {



            if (Request.QueryString["UserId"] != null)
            {
                userId = int.Parse(Encryption.Decrypt(Request.QueryString["UserId"].ToString()));
                userVo = userBo.GetUserDetails(userId);
                Session["UserVo"] = userVo;
                AddLoginTrack(txtLoginId.Text, txtPassword.Text, true, userVo.UserId);
                if (userVo != null)
                {

                    if (userVo.UserType == "Associates")
                        advisorVo = advisorBo.GetAssociateAdviserUser(userVo.UserId);

                    else if (userVo.UserType == "Customer")
                    {
                        customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                        advisorVo = advisorBo.GetAdvisor(advisorBranchBo.GetBranch(customerVo.BranchId).AdviserId);

                    }
                    else
                        advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                }

                Session["advisorVo"] = advisorVo;
                SetAdviserPreference();

                if (!string.IsNullOrEmpty(advisorVo.theme))
                {
                    //Page.Theme = advisorVo.theme.ToString();
                    Session["Theme"] = advisorVo.theme.ToString();
                    Session["refreshTheme"] = true;
                }
                else
                {
                    Page.Theme = "SBIOnLine";
                    Session["Theme"] = "SBIOnLine";
                    Session["refreshTheme"] = true;

                }

                SetUser(userId, userVo, advisorVo, customerVo);
            }
            //SBI Single Signon POC
            else if (Page.Request.Headers["From_NAM"] != null || Page.Request.Headers["x-username"] != null || Page.Request.Headers["x-guid"] != null)
            {
                string headerValue1 = string.Empty, headerValue2 = string.Empty, headerValue3 = string.Empty;
                string[] headerinfo = new string[10];
                headerinfo = Page.Request.Headers.GetValues("From_NAM");
                if (Page.Request.Headers["From_NAM"] != null)
                    headerValue1 = Page.Request.Headers["Name"];
                else
                    headerValue1 = "null";
                if (Page.Request.Headers["x-username"] != null)
                    headerValue2 = Page.Request.Headers["x-username"];
                else
                    headerValue2 = "null";
                if (Page.Request.Headers["x-guid"] != null)
                    headerValue3 = Page.Request.Headers["x-guid"];
                else
                    headerValue3 = "null";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + "From_NAM=" + headerValue1 + "x-username=" + headerValue2 + "x-guid=" + headerValue3 + "x-username=" + headerinfo[0] + "');", true);

                foreach (string str in headerinfo)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "alert('" + str + "');", true);
                }
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {

            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
            if (!IsPostBack)
            {
                if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                {
                    MT_LoginContent.Visible = true;
                    dynamicLoginContent.Visible = false;
                }
                else
                {
                    dynamicLoginContent.Visible = true;
                    MT_LoginContent.Visible = false;
                }
                if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                {
                    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];
                    if (!string.IsNullOrEmpty(generalconfigurationvo.LoginPageContent))
                    {
                        //lblUserLoginContent.Text = generalconfigurationvo.LoginPageContent;
                    }
                    if (!string.IsNullOrEmpty(generalconfigurationvo.ApplicationName))
                    {
                        lblCompanyName.Text = "Partner Login";
                    }
                }
                if (Request.ServerVariables["HTTPS"].ToString() == "")
                {
                    currentPageUrl = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 4).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString() + Request.ServerVariables["SCRIPT_NAME"].ToString();
                }
                else
                {
                    currentPageUrl = Request.ServerVariables["SERVER_PROTOCOL"].ToString().ToLower().Substring(0, 5).ToString() + "://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"].ToString() + Request.ServerVariables["SCRIPT_NAME"].ToString();
                }
                if (currentPageUrl.Contains("scb"))
                {
                    trWealthERP.Visible = false;
                    trAdvisorLogo.Visible = true;
                }
                else
                {
                    trWealthERP.Visible = true;
                    trAdvisorLogo.Visible = false;
                }


            }
            //btnSignIn.Attributes.Add("OnClick", "loadImage()");
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            UserVo userVo = new UserVo();
            UserBo userBo = new UserBo();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
            RMVo rmVo = new RMVo();
            AdvisorBo advisorBo = new AdvisorBo();
            //AdvisorVo advisorVo = new AdvisorVo();
            CustomerBo customerBo = new CustomerBo();
            CustomerVo customerVo = new CustomerVo();

            List<string> roleList = new List<string>();
            string sourcePath = "";
            string branchLogoSourcePath = "";
            int count;
            bool isGrpHead = false;
            DataSet dspotentialHomePage;
            string potentialHomePage = "";
            GeneralConfigurationBo generalvonfigurationbo = new GeneralConfigurationBo();
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
            Hashtable hashUserAuthenticationDetails = new Hashtable();
            string currentUserIP = string.Empty;
            bool isIPAuthenticated = false;
            bool isPassWordMathed = false;
            bool isUserAlreadyLogedIn = false;

            //if (txtLoginId.Text == "ESI64786")
            //{
            //    ValidateUserLogin(txtLoginId.Text.Trim());
            //}
            //else 
            try
            {
                if (!CheckSuperAdmin())
                {
                    if (txtLoginId.Text == "" || txtPassword.Text == "")
                    {
                        lblIllegal.Visible = true;
                        lblIllegal.Text = "Username and Password does not match";

                    }
                    else
                    {
                        userVo = userBo.GetUser(txtLoginId.Text);
                        if (userVo != null)
                        {

                            if (userVo.UserType == "Associates")
                                advisorVo = advisorBo.GetAssociateAdviserUser(userVo.UserId);
                            else if (userVo.UserType != "Customer")
                                advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                            else
                            {
                                customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                                advisorVo = advisorBo.GetAdvisor(advisorBranchBo.GetBranch(customerVo.BranchId).AdviserId);
                                if (customerVo.IsProspect == 0)
                                {
                                    PortfolioBo portfolioBo = new PortfolioBo();
                                    CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
                                    customerPortfolioVo = portfolioBo.GetCustomerDefaultPortfolio(customerVo.CustomerId);
                                    Session[SessionContents.PortfolioId] = customerPortfolioVo.PortfolioId;
                                }

                            }


                            SetAdviserPreference();

                            currentUserIP = HttpContext.Current.Request.UserHostAddress.Trim();
                            if ((advisorVo != null) && (userVo.UserType != "Customer"))
                            {
                                if (advisorVo.IsIPEnable == 1)
                                {
                                    hashUserAuthenticationDetails = userBo.UserValidationForIPnonIPUsers(advisorVo.advisorId, txtLoginId.Text, txtPassword.Text, currentUserIP, true);

                                    if (hashUserAuthenticationDetails["PWD"].ToString() == "True")
                                        isPassWordMathed = true;
                                    if (hashUserAuthenticationDetails["IPAuthentication"].ToString() == "True")
                                        isIPAuthenticated = true;
                                }
                                else
                                {
                                    hashUserAuthenticationDetails = userBo.UserValidationForIPnonIPUsers(advisorVo.advisorId, txtLoginId.Text, txtPassword.Text, currentUserIP, false);

                                    if (hashUserAuthenticationDetails["PWD"].ToString() == "True")
                                        isPassWordMathed = true;

                                }
                            }
                            else if (userVo.UserType == "Customer")
                            {
                                hashUserAuthenticationDetails = userBo.UserValidationForIPnonIPUsers(advisorVo.advisorId, txtLoginId.Text, txtPassword.Text, currentUserIP, false);

                                if (hashUserAuthenticationDetails["PWD"].ToString() == "True")
                                    isPassWordMathed = true;
                            }

                            if (isPassWordMathed)
                                isUserAlreadyLogedIn = ValidateSingleSessionPerUser(userVo.UserId.ToString());

                        }



                        if ((isPassWordMathed && isIPAuthenticated && isUserAlreadyLogedIn) || (isPassWordMathed && advisorVo.IsIPEnable == 0 && isUserAlreadyLogedIn))  // Validating the User Using the Username and Password
                        {


                            Session["id"] = "";
                            lblIllegal.Visible = true;

                            Session["advisorVo"] = advisorVo;
                            Session["UserVo"] = userVo;

                            AddLoginTrack(txtLoginId.Text, txtPassword.Text, true, userVo.UserId);
                            if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                            {
                                generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];
                                if (userVo.theme != null)
                                {
                                    Session["Theme"] = userVo.theme.ToString();
                                    Session["refreshTheme"] = true;
                                }
                                else
                                {
                                    Session["Theme"] = generalconfigurationvo.DefaultTheme;
                                    Session["refreshTheme"] = true;
                                }
                            }

                            //Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                            //advisorVo = (AdvisorVo)Session["advisorVo"];

                            if (advisorVo.IsActive == 1)
                            {

                                if (userVo.IsTempPassword == 0)
                                {
                                    string UserName = userVo.FirstName + " " + userVo.LastName;

                                    if (userVo.UserType == "Advisor")
                                    {
                                        bool breakLoopIfIPFailed = false;
                                        Session[SessionContents.CurrentUserRole] = "Admin";
                                        //Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                                        Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                                        //advisorVo = (AdvisorVo)Session["advisorVo"];
                                        rmVo = (RMVo)Session["rmVo"];
                                        Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                        if (advisorVo.LogoPath == null)
                                        {
                                            advisorVo.LogoPath = "";
                                        }
                                        sourcePath = "Images/" + advisorVo.LogoPath.ToString();

                                        Session[SessionContents.LogoPath] = sourcePath;

                                        roleList = userBo.GetUserRoles(userVo.UserId);
                                        count = roleList.Count;
                                        //Check For IP Authentication enable for Advisor 
                                        if (advisorVo.IsIPEnable == 1)
                                        {
                                            breakLoopIfIPFailed = CheckIPAuthentication(roleList, advisorVo);
                                            if (breakLoopIfIPFailed == false)
                                                return;
                                        }

                                        if ((advisorVo.IsOpsEnable == 0) && (roleList.Contains("Ops")))
                                        {
                                            lblIllegal.Text = "Login Failed..! <br> Ops Role is Disabled...!!";
                                            return;
                                        }

                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);

                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adviserpaneleftttt", "loadlinks('AdvisorLeftPane','login');", true);
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);

                                        if (count == 4)
                                        {
                                            advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                            Session["advisorBranchVo"] = advisorBranchVo;
                                            branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                            Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                            dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                            if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                            if (potentialHomePage == "Admin Home")
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                            else if (potentialHomePage == "Admin Small IFA Home")
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                            else
                                            {
                                                Session["Customer"] = "Customer";
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                            }
                                            Session[SessionContents.CurrentUserRole] = "Admin";
                                            Session[SessionContents.UserTopRole] = "Admin";
                                        }
                                        else if (count == 3)
                                        {
                                            if (roleList.Contains("Admin") && ((roleList.Contains("RM") && roleList.Contains("BM")) || (roleList.Contains("Research") && roleList.Contains("BM")) || (roleList.Contains("Research") && roleList.Contains("RM"))))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "Admin Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else if (potentialHomePage == "Admin Small IFA Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                                }
                                                Session[SessionContents.CurrentUserRole] = "Admin";
                                                Session[SessionContents.UserTopRole] = "Admin";
                                            }
                                            if (roleList.Contains("RM") && roleList.Contains("BM") && roleList.Contains("Research"))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                //login user role Type
                                                Session[SessionContents.CurrentUserRole] = "BM";
                                                Session[SessionContents.UserTopRole] = "BM";
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                //RM Theme Will be same as Advisor Theme
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "BM Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                                }

                                            }
                                            //if (roleList.Contains("Admin") && (roleList.Contains("RM") && roleList.Contains("Research")))
                                            //{

                                            //}
                                            //if (roleList.Contains("Admin") && (roleList.Contains("BM") && roleList.Contains("Research")))
                                            //{

                                            //}
                                        }
                                        else if (count == 2)
                                        {
                                            if (roleList.Contains("RM") && roleList.Contains("BM"))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                //login user role Type
                                                Session[SessionContents.CurrentUserRole] = "BM";
                                                Session[SessionContents.UserTopRole] = "BM";
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                //RM Theme Will be same as Advisor Theme
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "BM Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                                }
                                            }
                                            else if (roleList.Contains("RM") && roleList.Contains("Admin"))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                //login user role Type
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "Admin Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else if (potentialHomePage == "Admin Small IFA Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                                }
                                                Session[SessionContents.CurrentUserRole] = "Admin";
                                                Session[SessionContents.UserTopRole] = "Admin";
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;

                                            }
                                            else if (roleList.Contains("BM") && roleList.Contains("Admin"))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "Admin Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else if (potentialHomePage == "Admin Small IFA Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                                }
                                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                //login user role Type
                                                Session[SessionContents.CurrentUserRole] = "Admin";
                                                Session[SessionContents.UserTopRole] = "Admin";
                                            }
                                            else if (roleList.Contains("Admin") && roleList.Contains("Research"))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                //login user role Type
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "Admin Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else if (potentialHomePage == "Admin Small IFA Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                                }
                                                Session[SessionContents.CurrentUserRole] = "Admin";
                                                Session[SessionContents.UserTopRole] = "Admin";
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                            }
                                            else if (roleList.Contains("RM") && roleList.Contains("Research"))
                                            {
                                                Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                                //Session["advisorVo"]=advisorBo.GetAdvisor(
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                                                Session[SessionContents.LogoPath] = sourcePath;
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                //login user role Type Issue Reported by Ajay on July 1 2010
                                                Session[SessionContents.CurrentUserRole] = "RM";
                                                Session[SessionContents.UserTopRole] = "RM";
                                                //RM Theme Will be same as Advisor Theme
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "RM");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "RM Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMDashBoard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMCustomer','login');", true);
                                                }
                                            }
                                            else if (roleList.Contains("BM") && roleList.Contains("Research"))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                //login user role Type Issue Reported by Ajay on July 1 2010
                                                Session[SessionContents.CurrentUserRole] = "BM";
                                                Session[SessionContents.UserTopRole] = "BM";
                                                //RM Theme Will be same as Advisor Theme
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "BM Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                                }
                                            }
                                        }
                                        else if (count == 1)
                                        {
                                            if (roleList.Contains("RM"))
                                            {
                                                Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                                //Session["advisorVo"]=advisorBo.GetAdvisor(
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                                                Session[SessionContents.LogoPath] = sourcePath;
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                //login user role Type Issue Reported by Ajay on July 1 2010
                                                Session[SessionContents.CurrentUserRole] = "RM";
                                                Session[SessionContents.UserTopRole] = "RM";
                                                //RM Theme Will be same as Advisor Theme
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "RM");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "RM Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMDashBoard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMCustomer','login');", true);
                                                }

                                            }
                                            else if (roleList.Contains("BM"))
                                            {
                                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                                Session["advisorBranchVo"] = advisorBranchVo;
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                //login user role Type Issue Reported by Ajay on July 1 2010
                                                Session[SessionContents.CurrentUserRole] = "BM";
                                                Session[SessionContents.UserTopRole] = "BM";
                                                //RM Theme Will be same as Advisor Theme
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "BM Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                                }

                                            }
                                            else if (roleList.Contains("Research"))
                                            {
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;

                                                Session[SessionContents.CurrentUserRole] = "Research";
                                                Session[SessionContents.UserTopRole] = "Research";
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('ModelPortfolioSetup','login');", true);

                                            }
                                            else if (roleList.Contains("Ops"))
                                            {
                                                //RM Theme Will be same as Advisor Theme
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;

                                                Session[SessionContents.CurrentUserRole] = "Ops";
                                                Session[SessionContents.UserTopRole] = "Ops";
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('AdviserCustomer','login');", true);

                                            }
                                            else
                                            {
                                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                                if (potentialHomePage == "Admin Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else if (potentialHomePage == "Admin Small IFA Home")
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                                else
                                                {
                                                    Session["Customer"] = "Customer";
                                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                                }
                                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                                            }
                                        }

                                        GetLatestValuationDate();
                                    }
                                    //-------------------------------------------------Checking Associate User Login Details----------------
                                    else if (userVo.UserType.Trim() == "Associates")
                                    {
                                        bool breakLoopIfIPFailed = false;
                                        Session[SessionContents.CurrentUserRole] = "Associates";
                                        associatesVo = associatesBo.GetAssociateUser(userVo.UserId);
                                        if (associatesVo.IsActive == 1 || associatesVo.AAC_AgentCode == null)
                                        {
                                            associatesUserHeirarchyVo = associatesBo.GetAssociateUserHeirarchy(userVo.UserId, advisorVo.advisorId);
                                            Session["associatesVo"] = associatesVo;
                                            Session["associatesUserHeirarchyVo"] = associatesUserHeirarchyVo;
                                            Session["rmVo"] = advisorStaffBo.GetAdvisorStaffDetails(associatesVo.RMId);
                                            //advisorVo = (AdvisorVo)Session["advisorVo"];
                                            rmVo = (RMVo)Session["rmVo"];
                                            Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                            if (advisorVo.LogoPath == null)
                                            {
                                                advisorVo.LogoPath = "";
                                            }
                                            sourcePath = "Images/" + advisorVo.LogoPath.ToString();

                                            Session[SessionContents.LogoPath] = sourcePath;

                                            roleList = userBo.GetUserRoles(userVo.UserId);
                                            count = roleList.Count;
                                            //Check For IP Authentication enable for Advisor 
                                            if (advisorVo.IsIPEnable == 1)
                                            {
                                                breakLoopIfIPFailed = CheckIPAuthentication(roleList, advisorVo);
                                                if (breakLoopIfIPFailed == false)
                                                    return;
                                            }

                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);


                                            //if (count == 1)
                                            //{
                                            if (roleList.Contains("Associates"))
                                            {
                                                Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                                //Session["advisorVo"]=advisorBo.GetAdvisor(
                                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                                sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                                                Session[SessionContents.LogoPath] = sourcePath;
                                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                                userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                                Session["Theme"] = strUserTheme;
                                                Session["refreshTheme"] = true;
                                                Session[SessionContents.CurrentUserRole] = "Associates";
                                                Session[SessionContents.UserTopRole] = "Associates";
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adviserpaneleftttt", "loadlinks('AdvisorLeftPane','login');", true);
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SalesDashBoard','login');", true);

                                            }
                                            //}

                                            GetLatestValuationDate();
                                        }
                                        else
                                        {
                                            Session["Theme"] = "SBIOnLine";
                                            Session["refreshTheme"] = true;
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);
                                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderwjhh", "loadcontrol('AccountDeactive','login');", true);
                                        }
                                    }
                                    //else if (userVo.UserType == "RM")
                                    //{
                                    //    Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                                    //    rmVo = (RMVo)Session["rmVo"];
                                    //    Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                    //    //Session["advisorVo"]=advisorBo.GetAdvisor(
                                    //    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    //    sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                                    //    Session[SessionContents.LogoPath] = sourcePath;
                                    //    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('RMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

                                    //}
                                    else if (userVo.UserType == "Customer")
                                    {
                                        customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                                        //Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                                        Session["CustomerVo"] = customerVo;
                                        customerVo = (CustomerVo)Session["CustomerVo"];

                                        advisorVo = advisorBo.GetAdvisor(advisorBranchBo.GetBranch(customerVo.BranchId).AdviserId);
                                        rmVo = advisorStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                                        Session["rmVo"] = rmVo;
                                        Session["advisorVo"] = advisorVo;

                                        //if(customerVo!=null){

                                        sourcePath = "Images/" + userBo.GetCustomerLogo(customerVo.CustomerId);
                                        Session[SessionContents.LogoPath] = sourcePath;
                                        Session[SessionContents.CurrentUserRole] = "Customer";
                                        Session[SessionContents.UserTopRole] = "Customer";

                                        //RM Theme Will be same as Advisor Theme
                                        userBo.GetUserTheme(customerVo.CustomerId, "CUSTOMER", out strUserTheme);
                                        Session["Theme"] = strUserTheme;
                                        Session["refreshTheme"] = true;

                                        GetLatestValuationDate();

                                        dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Customer");
                                        if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                            potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                        //string urlPath = "<script type='text/javascript'>window.parent.location.href = '../OnlineMainHost.aspx?WERP=Customer&x-SBI-PType=MF&x-Account-ID="+customerVo.CustCode+"'; </script>";
                                        //Page.ClientScript.RegisterStartupScript(GetType(), "Load", urlPath);

                                        if (potentialHomePage == "Group Dashboard" || potentialHomePage == "Customer Dashboard")
                                        {
                                            Session["IsDashboard"] = "true";
                                            isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                                            if (isGrpHead == true)
                                            {
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerDashBoardShortcut','login');", true);
                                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMCustGroupDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                                            }
                                            else
                                            {
                                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerDashBoardShortcut','login');", true);
                                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMCustIndiDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                                            }
                                        }
                                        else
                                        {
                                            Session["IsDashboard"] = "FP";
                                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerFPDashBoard','login');", true);
                                        }

                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpanaaaae", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                                    }

                                    else if (userVo.UserType == "Admin")
                                    {
                                        Session["refreshTheme"] = false;
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminUpload','login','" + UserName + "','');", true);
                                    }
                                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "anytextcanbewritten", "parent.loadCB();", true);
                                   
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ChangeTempPassword','none');", true);
                                }
                            }
                            else
                            {
                                Session["Theme"] = "SBIOnLine";
                                Session["refreshTheme"] = true;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderwjhh", "loadcontrol('AccountDeactive','login');", true);
                            }

                        }

                        else
                        {
                            lblIllegal.Visible = true;
                            AddLoginTrack(txtLoginId.Text, txtPassword.Text, false, 0);

                            if (advisorVo != null)
                            {
                                if (((advisorVo.IsIPEnable == 0) && (!isPassWordMathed)) || ((advisorVo.IsIPEnable == 1) && ((!isPassWordMathed))))
                                {
                                    lblIllegal.Text = "Username and Password does not match..!!";
                                }
                                else if ((advisorVo.IsIPEnable == 1) && (!isIPAuthenticated))
                                {
                                    lblIllegal.Text = "IP Authentication is failed..!!";
                                }
                                else if (!isUserAlreadyLogedIn)
                                {
                                    lblIllegal.Text = "User already logged in..!!";

                                }

                            }
                            else
                            {
                                lblIllegal.Visible = true;
                                AddLoginTrack(txtLoginId.Text, txtPassword.Text, false, 0);
                                lblIllegal.Text = "Username and Password does not match";
                            }
                        }
                    }
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
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:SetUser()");
                object[] objects = new object[3];
                objects[0] = userId;
                objects[1] = userVo;
                objects[2] = advisorVo;
                objects[3] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        protected void lnkForgetPassword_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ForgotPassword", "loadcontrol('ForgotPassword');", true);
        }

        private bool ValidateSingleSessionPerUser(string userId)
        {
            bool isUserLogedIn = false;
            Hashtable currentLoginUserList = new Hashtable();

            if (Application["LoginUserList"] != null)
            {
                currentLoginUserList = (Hashtable)Application["LoginUserList"];
            }

            isUserLogedIn = currentLoginUserList.ContainsValue(userId);
            if (!isUserLogedIn)
            {
                currentLoginUserList.Add(Session.SessionID.ToString(), userId);
                Application.Lock();
                Application["LoginUserList"] = currentLoginUserList;
                Application.UnLock();
                isUserLogedIn = true;
            }
            else
            {
                isUserLogedIn = false;
            }

            return isUserLogedIn;

        }

        private bool CheckIPAuthentication(List<string> roleList, AdvisorVo advisorVo)
        {
            UserBo userBo = new UserBo();
            string currentUserIP = string.Empty;
            bool bCheckIPAvailability = false;
            currentUserIP = HttpContext.Current.Request.UserHostAddress.Trim().ToString();
            bCheckIPAvailability = userBo.CheckIPAvailabilityInIPPool(advisorVo.advisorId, currentUserIP);
            if (roleList.Count > 1)
            {
                if (bCheckIPAvailability == false)
                {
                    lblIllegal.Visible = true;
                    lblIllegal.Text = "IP Authentication failed..!!";
                    AddLoginTrack(txtLoginId.Text, txtPassword.Text, false, 0);

                }
            }
            else if (roleList.Count == 1)
            {
                if (!roleList.Contains("Customer"))
                {
                    if (bCheckIPAvailability == false)
                    {
                        lblIllegal.Visible = true;
                        lblIllegal.Text = "IP Authentication failed..!!";
                        AddLoginTrack(txtLoginId.Text, txtPassword.Text, false, 0);
                    }
                }

            }
            return bCheckIPAvailability;
        }

        private bool CheckSuperAdmin()
        {
            string UserName = "";
            UserVo userVo = new UserVo();
            UserBo userBo = new UserBo();
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorVo advisorVo = new AdvisorVo();

            if (userBo.ValidateUser(txtLoginId.Text, txtPassword.Text))
            {
                userVo = userBo.GetUser(txtLoginId.Text);
               
                Session[SessionContents.LogoPath] = "";
                Session[SessionContents.BranchLogoPath] = "";
                Session["UserId"] = userVo.UserId;


                if (userVo != null && userVo.UserType == "SuperAdmin")
                {
                    AddLoginTrack(txtLoginId.Text, txtPassword.Text, true, userVo.UserId);
                    advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);
                    Session["advisorVo"] = advisorVo;
                    Session["UserId"] = userVo.UserId;
                    Session["role"] = "SUPER_ADMIN";
                    Session["UserVo"] = userVo;
                    Session["SuperAdminRetain"] = userVo;
                    UserName = userVo.FirstName + " " + userVo.LastName;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('IFF','login','" + UserName + "','');", true);
                    //GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
                    //if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                    //{
                    //    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];
                    //    if (!string.IsNullOrEmpty(generalconfigurationvo.DefaultTheme))
                    //    {
                    //        if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
                    //        {
                    //            Session["Theme"] = generalconfigurationvo.DefaultTheme;
                    //        }
                    //        Page.Theme = Session["Theme"].ToString();
                    //    }
                    //}
                    //if (userVo.theme != null)
                    //{
                    //    Session["Theme"] = "Purple";
                    //    Session["refreshTheme"] = true;
                    //}
                    //else
                    //{
                    //    Session["Theme"] = "Purple";
                    //    Session["refreshTheme"] = true;
                    //}
                    return true;
                }
                else
                    return false;
            }
            else
            {
                lblIllegal.Visible = true;
                lblIllegal.Text = "Username and Password does not match";
                AddLoginTrack(txtLoginId.Text, txtPassword.Text, false, 0);
                return false;
            }
        }

        public void SetUser(int userId, UserVo userVo, AdvisorVo advisorVo, CustomerVo customerVo)
        {
            //UserVo userVo = new UserVo();
            UserBo userBo = new UserBo();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
            RMVo rmVo = new RMVo();
            AdvisorBo advisorBo = new AdvisorBo();
            //AdvisorVo advisorVo = new AdvisorVo();
            CustomerBo customerBo = new CustomerBo();
            //CustomerVo customerVo = new CustomerVo();
            List<string> roleList = new List<string>();
            GeneralConfigurationBo generalvonfigurationbo = new GeneralConfigurationBo();
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
            DataSet dspotentialHomePage;
            string potentialHomePage = "";
            string sourcePath = "";
            string branchLogoSourcePath = "";
            int count;
            bool isGrpHead = false;
            try
            {
                //userId = 52877;
                //userVo = userBo.GetUserDetails(userId);
                //Session["UserVo"] = userVo;
                //AddLoginTrack(txtLoginId.Text, txtPassword.Text, true, userVo.UserId);

                //if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                //{
                //    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];
                //    if (userVo.theme != null)
                //    {
                //        Session["Theme"] = userVo.theme.ToString();
                //        Session["refreshTheme"] = true;
                //    }
                //    else
                //    {
                //        Session["Theme"] = generalconfigurationvo.DefaultTheme;
                //        Session["refreshTheme"] = true;
                //    }
                //}
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);
                //if (userVo.theme != null)
                //{
                //    Session["Theme"] = userVo.theme.ToString();
                //    Session["refreshTheme"] = true;
                //}
                //else
                //{
                //    Session["Theme"] = "Maroon";
                //    Session["refreshTheme"] = true;
                //}

                //if (userVo != null)
                //{
                //    if (userVo.UserType != "Customer")
                //        advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);

                //    else
                //    {
                //        customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                //        advisorVo = advisorBo.GetAdvisor(advisorBranchBo.GetBranch(customerVo.BranchId).AdviserId);

                //    }
                //}

                //Session["advisorVo"] = advisorVo;
                //if (!string.IsNullOrEmpty(advisorVo.theme))
                //{
                //    Session["Theme"] = advisorVo.theme.ToString();
                //    Session["refreshTheme"] = true;
                //}
                //else
                //{
                //    Session["Theme"] = "Maroon";
                //    Session["refreshTheme"] = true;

                //}

                //Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                //advisorVo = (AdvisorVo)Session["advisorVo"];

                if (advisorVo.IsActive == 1)
                {

                    if (userVo.IsTempPassword == 0)
                    {
                        string UserName = userVo.FirstName + " " + userVo.LastName;

                        if (userVo.UserType == "Advisor")
                        {
                            bool breakLoopIfIPFailed = false;
                            //Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                            Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                            //advisorVo = (AdvisorVo)Session["advisorVo"];
                            rmVo = (RMVo)Session["rmVo"];
                            Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                            if (advisorVo.LogoPath == null)
                            {
                                advisorVo.LogoPath = "";
                            }
                            sourcePath = "Images/" + advisorVo.LogoPath.ToString();

                            Session[SessionContents.LogoPath] = sourcePath;

                            roleList = userBo.GetUserRoles(userVo.UserId);
                            count = roleList.Count;
                            //Check For IP Authentication enable for Advisor 
                            if (advisorVo.IsIPEnable == 1)
                            {
                                breakLoopIfIPFailed = CheckIPAuthentication(roleList, advisorVo);
                                if (breakLoopIfIPFailed == false)
                                    return;
                            }

                            if ((advisorVo.IsOpsEnable == 0) && (roleList.Contains("Ops")))
                            {
                                lblIllegal.Text = "Login Failed..! <br> Ops Role is Disabled...!!";
                                return;
                            }

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);

                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adviserpaneleftttt", "loadlinks('AdvisorLeftPane','login');", true);

                            if (count == 4)
                            {
                                advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                Session["advisorBranchVo"] = advisorBranchVo;
                                branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                    potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                if (potentialHomePage == "Admin Home")
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                else if (potentialHomePage == "Admin Small IFA Home")
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                else
                                {
                                    Session["Customer"] = "Customer";
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                }
                                Session[SessionContents.CurrentUserRole] = "Admin";
                            }
                            else if (count == 3)
                            {
                                if (roleList.Contains("Admin") && ((roleList.Contains("RM") && roleList.Contains("BM")) || (roleList.Contains("Research") && roleList.Contains("BM")) || (roleList.Contains("Research") && roleList.Contains("RM"))))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "Admin Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else if (potentialHomePage == "Admin Small IFA Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                    }
                                    Session[SessionContents.CurrentUserRole] = "Admin";
                                }
                                if (roleList.Contains("RM") && roleList.Contains("BM") && roleList.Contains("Research"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    //login user role Type
                                    Session[SessionContents.CurrentUserRole] = "BM";
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //RM Theme Will be same as Advisor Theme
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "BM Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                    }

                                }
                                //if (roleList.Contains("Admin") && (roleList.Contains("RM") && roleList.Contains("Research")))
                                //{

                                //}
                                //if (roleList.Contains("Admin") && (roleList.Contains("BM") && roleList.Contains("Research")))
                                //{

                                //}
                            }
                            else if (count == 2)
                            {
                                if (roleList.Contains("RM") && roleList.Contains("BM"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    //login user role Type
                                    Session[SessionContents.CurrentUserRole] = "BM";
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //RM Theme Will be same as Advisor Theme
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "BM Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                    }
                                }
                                else if (roleList.Contains("RM") && roleList.Contains("Admin"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    //login user role Type
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "Admin Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else if (potentialHomePage == "Admin Small IFA Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                    }
                                    Session[SessionContents.CurrentUserRole] = "Admin";
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;

                                }
                                else if (roleList.Contains("BM") && roleList.Contains("Admin"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "Admin Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else if (potentialHomePage == "Admin Small IFA Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                    }
                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    //login user role Type
                                    Session[SessionContents.CurrentUserRole] = "Admin";
                                }
                                else if (roleList.Contains("Admin") && roleList.Contains("Research"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    //login user role Type
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "Admin Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else if (potentialHomePage == "Admin Small IFA Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                    }
                                    Session[SessionContents.CurrentUserRole] = "Admin";
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                }
                                else if (roleList.Contains("RM") && roleList.Contains("Research"))
                                {
                                    Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                    //Session["advisorVo"]=advisorBo.GetAdvisor(
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                                    Session[SessionContents.LogoPath] = sourcePath;
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //login user role Type Issue Reported by Ajay on July 1 2010
                                    Session[SessionContents.CurrentUserRole] = "RM";
                                    //RM Theme Will be same as Advisor Theme
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "RM");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "RM Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMDashBoard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMCustomer','login');", true);
                                    }
                                }
                                else if (roleList.Contains("BM") && roleList.Contains("Research"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //login user role Type Issue Reported by Ajay on July 1 2010
                                    Session[SessionContents.CurrentUserRole] = "BM";
                                    //RM Theme Will be same as Advisor Theme
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "BM Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                    }
                                }
                            }
                            else if (count == 1)
                            {
                                if (roleList.Contains("RM"))
                                {
                                    Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                                    //Session["advisorVo"]=advisorBo.GetAdvisor(
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                                    Session[SessionContents.LogoPath] = sourcePath;
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //login user role Type Issue Reported by Ajay on July 1 2010
                                    Session[SessionContents.CurrentUserRole] = "RM";
                                    //RM Theme Will be same as Advisor Theme
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "RM");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "RM Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMDashBoard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('RMCustomer','login');", true);
                                    }

                                }
                                else if (roleList.Contains("BM"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //login user role Type Issue Reported by Ajay on July 1 2010
                                    Session[SessionContents.CurrentUserRole] = "BM";
                                    //RM Theme Will be same as Advisor Theme
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "BM");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "BM Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeejuywerw", "loadcontrol('BMDashBoard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('BMCustomer','login');", true);
                                    }

                                }
                                else if (roleList.Contains("Research"))
                                {
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;

                                    Session[SessionContents.CurrentUserRole] = "Research";
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('ModelPortfolioSetup','login');", true);

                                }
                                else if (roleList.Contains("Ops"))
                                {
                                    //RM Theme Will be same as Advisor Theme
                                    userBo.GetUserTheme(rmVo.RMId, "RM", out strUserTheme);
                                    Session["Theme"] = strUserTheme;
                                    Session["refreshTheme"] = true;

                                    Session[SessionContents.CurrentUserRole] = "Admin";
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeemkwerw", "loadcontrol('AdviserCustomer','login');", true);

                                }
                                else
                                {
                                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Admin");
                                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();
                                    if (potentialHomePage == "Admin Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else if (potentialHomePage == "Admin Small IFA Home")
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    else
                                    {
                                        Session["Customer"] = "Customer";
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsdserw", "loadcontrol('AdviserCustomer','login');", true);
                                    }
                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdvisorDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                                }
                            }

                            GetLatestValuationDate();
                        }
                        //else if (userVo.UserType == "RM")
                        //{
                        //    Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                        //    rmVo = (RMVo)Session["rmVo"];
                        //    Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                        //    //Session["advisorVo"]=advisorBo.GetAdvisor(
                        //    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        //    sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                        //    Session[SessionContents.LogoPath] = sourcePath;
                        //    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('RMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

                        //}
                        else if (userVo.UserType == "Customer")
                        {
                            customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                            //Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                            Session["CustomerVo"] = customerVo;
                            customerVo = (CustomerVo)Session["CustomerVo"];

                            advisorVo = advisorBo.GetAdvisor(advisorBranchBo.GetBranch(customerVo.BranchId).AdviserId);
                            rmVo = advisorStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                            Session["rmVo"] = rmVo;
                            Session["advisorVo"] = advisorVo;

                            //if(customerVo!=null){

                            sourcePath = "Images/" + userBo.GetCustomerLogo(customerVo.CustomerId);
                            Session[SessionContents.LogoPath] = sourcePath;
                            Session[SessionContents.CurrentUserRole] = "Customer";

                            //RM Theme Will be same as Advisor Theme
                            userBo.GetUserTheme(customerVo.CustomerId, "CUSTOMER", out strUserTheme);
                            Session["Theme"] = strUserTheme;
                            Session["refreshTheme"] = true;

                            GetLatestValuationDate();

                            dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Customer");
                            if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                                potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();

                            if (potentialHomePage == "Group Dashboard" || potentialHomePage == "Customer Dashboard")
                            {
                                Session["IsDashboard"] = "true";
                                isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                                if (isGrpHead == true)
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerDashBoardShortcut','login');", true);
                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMCustGroupDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerDashBoardShortcut','login');", true);
                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMCustIndiDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                                }
                            }
                            else
                            {
                                Session["IsDashboard"] = "FP";
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerFPDashBoard','login');", true);
                            }

                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);
                        }

                        else if (userVo.UserType == "Admin")
                        {
                            Session["refreshTheme"] = false;
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('AdminUpload','login','" + UserName + "','');", true);
                        }
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "anytextcanbewritten", "parent.loadCB();", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ChangeTempPassword','none');", true);
                    }
                }
                else
                {
                    Session["Theme"] = "SBIOnLine";
                    Session["refreshTheme"] = true;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewsderwjhh", "loadcontrol('CustomerFPAnalyticsDynamic','login');", true);
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
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:SetUser()");
                object[] objects = new object[3];
                objects[0] = userId;
                objects[1] = userVo;
                objects[2] = advisorVo;
                objects[3] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        private void GetLatestValuationDate()
        {
            DateTime EQValuationDate = new DateTime();
            DateTime MFValuationDate = new DateTime();
            PortfolioBo portfolioBo = null;
            genDict = new Dictionary<string, DateTime>();
            AdvisorVo advisorVo = new AdvisorVo();
            int adviserId = 0;
            try
            {
                portfolioBo = new PortfolioBo();
                advisorVo = (AdvisorVo)Session["advisorVo"];
                adviserId = advisorVo.advisorId;


                if (portfolioBo.GetLatestValuationDate(adviserId, "EQ") != null)
                {
                    EQValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "EQ").ToString());
                }
                if (portfolioBo.GetLatestValuationDate(adviserId, "MF") != null)
                {
                    MFValuationDate = DateTime.Parse(portfolioBo.GetLatestValuationDate(adviserId, "MF").ToString());
                }
                genDict.Add("EQDate", EQValuationDate);
                genDict.Add("MFDate", MFValuationDate);
                Session["ValuationDate"] = genDict;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioDashboard.ascx.cs:GetLatestValuationDate()");
                object[] objects = new object[3];
                objects[0] = EQValuationDate;
                objects[1] = adviserId;
                objects[2] = MFValuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        protected void setControl()
        {
            AdvisorHomeLinks HomeLinks = (AdvisorHomeLinks)this.Page.LoadControl("Advisor//AdvisorHomeLinks.ascx");
            //HomeLinks = (AdvisorHomeLinks)this.Page.LoadControl("Advisor//AdvisorHomeLinks.ascx");
            Panel pleft = new Panel();
            pleft = (Panel)this.Parent.FindControl("LeftPanel");
            pleft.Controls.Clear();
            pleft.Controls.Add(HomeLinks);
        }

        protected void NewUser_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('RegistrationType','none','none');", true);
        }

        protected void ForgotPassword_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ForgotPassword','none','none');", true);
        }

        /// <summary>
        /// Save the Login Track information.
        /// </summary>
        private void AddLoginTrack(string loginId, string password, bool isSuccess, int createdBy)
        {
            string IPAddress = string.Empty;
            string browser = string.Empty;
            string securedPassword = string.Empty;

            if (HttpContext.Current.Request.UserAgent != null)
                browser = HttpContext.Current.Request.UserAgent;

            IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (txtPassword.Text != null)
                securedPassword = Encryption.Encrypt(txtPassword.Text);

            UserBo.AddLoginTrack(txtLoginId.Text, securedPassword, isSuccess, IPAddress, browser, createdBy);

        }

        private void SetAdviserPreference()
        {
            advisorPreferenceVo = adviserPreferenceBo.GetAdviserPreference(advisorVo.advisorId);
            Session["AdvisorPreferenceVo"] = advisorPreferenceVo;

        }

        private void ValidateUserLogin(string userAccountId)
        {

            userVo = userBo.GetUserAccountDetails(userAccountId, 0);

            if (userVo != null)
            {
                List<string> roleList = new List<string>();
                string branchLogoSourcePath;
                string sourcePath;
                bool isGrpHead;
                DataSet dspotentialHomePage;
                string potentialHomePage = string.Empty;

                roleList = userBo.GetUserRoles(userVo.UserId);

                if (userVo.UserType == "Associates")
                {
                    advisorVo = advisorBo.GetAssociateAdviserUser(userVo.UserId);
                    associatesVo = associatesBo.GetAssociateUser(userVo.UserId);
                    associatesUserHeirarchyVo = associatesBo.GetAssociateUserHeirarchy(userVo.UserId, advisorVo.advisorId);
                    Session["rmVo"] = advisorStaffBo.GetAdvisorStaffDetails(associatesVo.RMId);

                    Session["adviserId"] = advisorVo.advisorId;
                    Session[SessionContents.CurrentUserRole] = "Associates";
                    Session[SessionContents.UserTopRole] = "Associates";

                    Session["associatesVo"] = associatesVo;
                    Session["associatesUserHeirarchyVo"] = associatesUserHeirarchyVo;


                }
                else if (userVo.UserType == "Advisor")
                {
                    advisorVo = advisorBo.GetAdvisorUser(userVo.UserId);

                }
                else if (userVo.UserType == "Customer")
                {
                    customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                    advisorVo = advisorBo.GetAdvisor(advisorBranchBo.GetBranch(customerVo.BranchId).AdviserId);
                    if (customerVo.IsProspect == 0)
                    {
                        PortfolioBo portfolioBo = new PortfolioBo();
                        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
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

                    dspotentialHomePage = advisorBo.GetUserPotentialHomepages(advisorVo.advisorId, "Customer");
                    if (dspotentialHomePage.Tables[0].Rows.Count > 0)
                        potentialHomePage = dspotentialHomePage.Tables[0].Rows[0][0].ToString();

                    Session["CustomerVo"] = customerVo;
                }

                Session["UserVo"] = userVo;
                Session["advisorVo"] = advisorVo;
                Session["rmVo"] = rmVo;
                SetAdviserPreference();

                Session["Theme"] = advisorVo.theme;
                Session["refreshTheme"] = true;

                Session[SessionContents.LogoPath] = advisorVo.LogoPath;

                if (userVo.UserType == "Associates")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "loadingatthelogin", "parent.loadCB();", true);

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adviserpaneleftttt", "loadlinks('AdvisorLeftPane','login');", true);
                    if (roleList.Count == 1)
                    {
                        if (roleList.Contains("Associates"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('SalesDashBoard','login');", true);

                        }
                    }


                }

                else if (userVo.UserType == "Customer")
                {
                    if (potentialHomePage == "Group Dashboard" || potentialHomePage == "Customer Dashboard")
                    {
                        Session["IsDashboard"] = "true";
                        isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                        if (isGrpHead == true)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerDashBoardShortcut','login');", true);

                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerDashBoardShortcut','login');", true);

                        }
                    }
                    else
                    {
                        Session["IsDashboard"] = "FP";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('CustomerFPDashBoard','login');", true);
                    }

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "leftpane", "loadlinks('RMCustomerIndividualLeftPane','login');", true);

                }

                GetLatestValuationDate();

            }
            else
            {
                InvalidateUser();
            }

        }

        private void InvalidateUser()
        {
            lblIllegal.Text = "Username and Password does not match";
        }


    }
}
