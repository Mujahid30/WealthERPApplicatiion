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

using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using WealthERP.Base;
using System.IO;
using VoHostConfig;
using BoHostConfig;

namespace WealthERP
{
    public partial class _Default : System.Web.UI.Page
    {
        string logoPath = "";
        string branchLogoPath = "";
        UserVo userVo = new UserVo();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();
            GeneralConfigurationBo generalvonfigurationbo = new GeneralConfigurationBo();
            if (!IsPostBack)
            {
                generalconfigurationvo = generalvonfigurationbo.GetHostGeneralConfiguration(0);
                Session[SessionContents.SAC_HostGeneralDetails] = generalconfigurationvo;
                if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                {
                    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];

                    if (!string.IsNullOrEmpty(generalconfigurationvo.DefaultTheme))
                    {
                        if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
                        {
                            Session["Theme"] = generalconfigurationvo.DefaultTheme;
                        }
                        Page.Theme = Session["Theme"].ToString();

                    }
                    if (!string.IsNullOrEmpty(generalconfigurationvo.ApplicationName))
                    {
                        Page.Title = generalconfigurationvo.ApplicationName;
                    }
                }
            }
            //if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
            //{
            //    Session["Theme"] = "Maroon";
            //}

            //Page.Theme = Session["Theme"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RMVo rmVo = new RMVo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            GeneralConfigurationVo generalconfigurationvo = new GeneralConfigurationVo();

            tdTermsCondition.Visible = false;
            try
            {
                lblDate.Text = DateTime.Now.ToLongDateString();





                imgLeftPlaceHolder.Style.Add("display", "none");
                imgCenterPlaceholder.Style.Add("display", "none");
                imgRightPlaceholder.Style.Add("display", "none");
                if (Session[SessionContents.SAC_HostGeneralDetails] != null)
                {
                    generalconfigurationvo = (GeneralConfigurationVo)Session[SessionContents.SAC_HostGeneralDetails];


                    if (!IsPostBack)
                    {
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
                        if (userVo.UserType == "Advisor")
                        {

                            tdTermsCondition.Visible = true;
                        }
                        else
                        {
                            tdTermsCondition.Visible = false;
                        }

                        if ((!IsPostBack) && (userVo.UserType == "Customer"))
                        {
                            tdTermsCondition.Visible = false;
                            tdDemo.Visible = false;
                            tdHelp.Visible = false;
                            lnkDemo.Visible = false;
                            lnkHelp.Visible = false;
                        }

                        lblUserName.Text = "Welcome " + " " + userVo.FirstName + " " + userVo.LastName;
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

    }
}
