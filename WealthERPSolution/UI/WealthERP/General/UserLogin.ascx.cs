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

namespace WealthERP.General
{
    public partial class UserLogin : System.Web.UI.UserControl
    {
        Dictionary<string, DateTime> genDict = new Dictionary<string, DateTime>();
        string strUserTheme;
        string currentPageUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            if (!IsPostBack)
            {
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

                if (Request.QueryString["UserId"] != null)
                {
                    userId = int.Parse(Encryption.Decrypt(Request.QueryString["UserId"].ToString()));
                    SetUser(userId);
                }
            }
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
            AdvisorVo advisorVo = new AdvisorVo();
            CustomerBo customerBo = new CustomerBo();
            CustomerVo customerVo = new CustomerVo();
            List<string> roleList = new List<string>();
            string sourcePath = "";
            string branchLogoSourcePath = "";
            int count;
            bool isGrpHead = false;
            DataSet dspotentialHomePage;
            string potentialHomePage = "";


            if (txtLoginId.Text == "" || txtPassword.Text == "")
            {
                lblIllegal.Visible = true;
                lblIllegal.Text = "Username and Password does not match";

            }
            else
            {

                if (userBo.ValidateUser(txtLoginId.Text, txtPassword.Text))  // Validating the User Using the Username and Password
                {

                    Session["id"] = "";
                    lblIllegal.Visible = true;


                    userVo = userBo.GetUser(txtLoginId.Text);
                    Session["UserVo"] = userVo;
                    AddLoginTrack(txtLoginId.Text, txtPassword.Text, true, userVo.UserId);

                    if (userVo.theme != null)
                    {
                        Session["Theme"] = userVo.theme.ToString();
                        Session["refreshTheme"] = true;
                    }
                    else
                    {
                        Session["Theme"] = "Maroon";
                        Session["refreshTheme"] = true;
                    }

                    if (userVo.IsTempPassword == 0)
                    {
                        string UserName = userVo.FirstName + " " + userVo.LastName;



                        //if (userVo.UserType == "Branch Man")
                        //{
                        //    roleList = userBo.GetUserRoles(userVo.UserId);
                        //    count = roleList.Count;

                        //    Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                        //    rmVo = (RMVo)Session["rmVo"];
                        //    sourcePath = "Images/" + userBo.GetRMLogo(rmVo.RMId);
                        //    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        //    Session[SessionContents.LogoPath] = sourcePath;
                        //    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        //    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                        //    Session["advisorBranchVo"] = advisorBranchVo;
                        //    if (count == 2)
                        //    {
                        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMRMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);
                        //    }
                        //    if (count == 1)
                        //    {
                        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);
                        //    }
                        //}
                        if (userVo.UserType == "Advisor")
                        {
                            Session[SessionContents.CurrentUserRole] = "Admin";
                            Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                            Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                            advisorVo = (AdvisorVo)Session["advisorVo"];
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

                            if (count == 3)
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
                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMBMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                                //login user role Type
                                Session["S_CurrentUserRole"] = "Admin";
                            }
                            if (count == 2)
                            {
                                if (roleList.Contains("RM") && roleList.Contains("BM"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    //login user role Type
                                    Session["S_CurrentUserRole"] = "BM";
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


                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMRMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);
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
                                    Session["S_CurrentUserRole"] = "Admin";
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('IFAAdminMainDashboard','login');", true);
                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
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
                                    Session["S_CurrentUserRole"] = "Admin";
                                }
                            }

                            //for (int i = 0; i < 2; i++)
                            //{
                            //    if (roleList[i] == "RM")
                            //    {

                            //        rmVo = (RMVo)Session["rmVo"];
                            //        
                            //    }
                            //    if (roleList[i] == "BM")
                            //    {
                            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorBMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                            //    }

                            //}




                            if (count == 1)
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
                                    Session["S_CurrentUserRole"] = "RM";
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


                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('RMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

                                }
                                else if (roleList.Contains("BM"))
                                {
                                    advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                                    Session["advisorBranchVo"] = advisorBranchVo;
                                    branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                                    Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                                    //login user role Type Issue Reported by Ajay on July 1 2010
                                    Session["S_CurrentUserRole"] = "BM";
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

                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

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
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadlinks('AdvisorLeftPane','login');", true);
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
                            Session["S_CurrentUserRole"] = "Customer";

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
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('AdvisorRMCustGroupDashboard','login');", true);
                                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMCustGroupDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                                }
                                else
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Reg23itlpoeewerw", "loadcontrol('AdvisorRMCustIndiDashboard','login');", true);
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
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ChangeTempPassword','none');", true);
                    }
                }

                else
                {
                    lblIllegal.Visible = true;
                    lblIllegal.Text = "Username and Password does not match";
                    AddLoginTrack(txtLoginId.Text, txtPassword.Text, false, 0);
                }

            }

        }
        public void SetUser(int userId)
        {
            UserVo userVo = new UserVo();
            UserBo userBo = new UserBo();
            AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
            RMVo rmVo = new RMVo();
            AdvisorBo advisorBo = new AdvisorBo();
            AdvisorVo advisorVo = new AdvisorVo();
            CustomerBo customerBo = new CustomerBo();
            CustomerVo customerVo = new CustomerVo();
            List<string> roleList = new List<string>();
            string sourcePath = "";
            string branchLogoSourcePath = "";
            int count;
            bool isGrpHead = false;
            userVo = userBo.GetUserDetails(userId);
            Session["UserVo"] = userVo;
            AddLoginTrack(txtLoginId.Text, txtPassword.Text, true, userVo.UserId);

            if (userVo.theme != null)
            {
                Session["Theme"] = userVo.theme.ToString();
                Session["refreshTheme"] = true;
            }
            else
            {
                Session["Theme"] = "Purple";
                Session["refreshTheme"] = true;
            }

            if (userVo.IsTempPassword == 0)
            {
                string UserName = userVo.FirstName + " " + userVo.LastName;


                if (userVo.UserType == "Advisor")
                {
                    Session[SessionContents.CurrentUserRole] = "Admin";
                    Session["advisorVo"] = advisorBo.GetAdvisorUser(userVo.UserId);
                    Session["rmVo"] = advisorStaffBo.GetAdvisorStaff(userVo.UserId);
                    advisorVo = (AdvisorVo)Session["advisorVo"];
                    rmVo = (RMVo)Session["rmVo"];
                    Session["adviserId"] = advisorBo.GetRMAdviserId(rmVo.RMId);
                    if (advisorVo.LogoPath == null || advisorVo.LogoPath == "")
                    {
                        advisorVo.LogoPath = "spacer.png";
                    }
                    else
                    {
                        sourcePath = "Images/" + advisorVo.LogoPath.ToString();
                        if (!System.IO.File.Exists(Server.MapPath(sourcePath)))
                            sourcePath = "";
                    }

                    Session[SessionContents.LogoPath] = sourcePath;

                    roleList = userBo.GetUserRoles(userVo.UserId);
                    count = roleList.Count;

                    if (count == 3)
                    {
                        advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                        Session["advisorBranchVo"] = advisorBranchVo;
                        branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                        Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMBMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                        //login user role Type
                        Session["S_CurrentUserRole"] = "Admin";
                    }
                    if (count == 2)
                    {
                        if (roleList.Contains("RM") && roleList.Contains("BM"))
                        {
                            advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                            Session["advisorBranchVo"] = advisorBranchVo;
                            //login user role Type
                            Session["S_CurrentUserRole"] = "RM";
                            branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                            Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMRMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);
                        }
                        else if (roleList.Contains("RM") && roleList.Contains("Admin"))
                        {
                            advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                            Session["advisorBranchVo"] = advisorBranchVo;
                            //login user role Type
                            Session["S_CurrentUserRole"] = "Admin";
                            branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                            Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                        }
                        else if (roleList.Contains("BM") && roleList.Contains("Admin"))
                        {
                            advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                            Session["advisorBranchVo"] = advisorBranchVo;
                            branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                            Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                            //login user role Type
                            Session["S_CurrentUserRole"] = "Admin";
                        }
                    }


                    if (count == 1)
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
                            Session["S_CurrentUserRole"] = "RM";
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('RMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

                        }
                        else if (roleList.Contains("BM"))
                        {
                            advisorBranchVo = advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId));
                            Session["advisorBranchVo"] = advisorBranchVo;
                            branchLogoSourcePath = "Images/" + userBo.GetRMBranchLogo(rmVo.RMId);
                            Session[SessionContents.BranchLogoPath] = branchLogoSourcePath;
                            //login user role Type Issue Reported by Ajay on July 1 2010
                            Session["S_CurrentUserRole"] = "BM";
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('BMDashBoard','login','" + UserName + "','" + sourcePath + "','" + branchLogoSourcePath + "');", true);

                        }
                        else
                        {

                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorDashBoard','login','" + UserName + "','" + sourcePath + "');", true);
                        }
                    }
                    GetLatestValuationDate();
                }

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
                    Session["S_CurrentUserRole"] = "Customer";
                    GetLatestValuationDate();

                    Session["IsDashboard"] = "true";
                    isGrpHead = customerBo.CheckCustomerGroupHead(customerVo.CustomerId);
                    if (isGrpHead == true)
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMCustGroupDashboard','login','" + UserName + "','" + sourcePath + "');", true);
                    else
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdvisorRMCustIndiDashboard','login','" + UserName + "','" + sourcePath + "');", true);

                }

                else if (userVo.UserType == "Admin")
                {
                    Session["refreshTheme"] = false;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loginloadcontrol('AdminUpload','login','" + UserName + "','');", true);


                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('ChangeTempPassword','none');", true);
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

            if (HttpContext.Current.Request.UserAgent != null)
                browser = HttpContext.Current.Request.UserAgent;

            IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            UserBo.AddLoginTrack(txtLoginId.Text, txtPassword.Text, isSuccess, IPAddress, browser, createdBy);

        }

    }
}
