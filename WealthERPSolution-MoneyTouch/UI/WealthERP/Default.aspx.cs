﻿using System;
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

namespace WealthERP
{
    public partial class _Default : System.Web.UI.Page
    {
        string logoPath = "";
        string branchLogoPath = "";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Theme"] == null || Session["Theme"].ToString() == string.Empty)
            {
                Session["Theme"] = "Purple";
            }

            Page.Theme = Session["Theme"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserVo userVo = new UserVo();
            RMVo rmVo = new RMVo();
            AdvisorBranchBo advisorBranchBo = new AdvisorBranchBo();
            try
            {
                lblDate.Text = DateTime.Now.ToLongDateString();

                AdvisorLogo.Style.Add("display", "block");
                BranchLogo.Style.Add("display", "block");
                
                if (Session["userVo"] != null)
                {
                    userVo = (UserVo)(Session["userVo"]);
                    rmVo = (RMVo)(Session[SessionContents.RmVo]);

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
                        if(rmVo != null)
                            branchLogoPath = "Images/" + (advisorBranchBo.GetBranch(advisorBranchBo.GetBranchId(rmVo.RMId))).LogoPath;
                    }

                    if (logoPath != "Images/")
                    {
                        AdvisorLogo.Src = logoPath;
                    }
                    if (branchLogoPath != "Images/")
                    {
                        BranchLogo.Src = branchLogoPath;

                    }
                    //if (Session["advisorVo"] != null)
                    //{
                    //    //GeneralMenu.Visible = false;
                    //    //AdvisorHeader.Visible = true;
                    //    GeneralMenu.Style.Add("display", "none");
                    //    AdvisorHeader.Style.Add("display", "block");
                    //    RMHeader.Style.Add("display", "none");
                    //    CustomerIndividualHeader.Style.Add("display", "none");
                    //    CustomerNonIndividualHeader.Style.Add("display", "none");
                    //    RMCLientHeaderIndividual.Style.Add("display", "none");
                    //    RMCLientHeaderNonIndividual.Style.Add("display", "none");
                    //    BMHeader.Style.Add("display", "none");

                    //}
                    //if (Session["CustomerVo"] != null && Session["rmVo"] == null)
                    //{
                    //    //GeneralMenu.Visible = false;
                    //    //AdvisorHeader.Visible = true;
                    //    GeneralMenu.Style.Add("display", "none");
                    //    AdvisorHeader.Style.Add("display", "none");
                    //    RMHeader.Style.Add("display", "none");

                        CustomerVo customerVo = new CustomerVo();
                        customerVo = (CustomerVo)(Session["CustomerVo"]);

                    //    if (customerVo.Type == "IND")
                    //    {
                    //        CustomerIndividualHeader.Style.Add("display", "block");
                    //        CustomerNonIndividualHeader.Style.Add("display", "none");
                    //    }
                    //    else if (customerVo.Type == "NIND")
                    //    {
                    //        CustomerIndividualHeader.Style.Add("display", "none");
                    //        CustomerNonIndividualHeader.Style.Add("display", "block");
                    //    }

                    //    RMCLientHeaderIndividual.Style.Add("display", "none");
                    //    RMCLientHeaderNonIndividual.Style.Add("display", "none");
                    //    BMHeader.Style.Add("display", "none");
                    //}
                    //if (Session["rmVo"] != null)
                    //{
                    //    if (Session["CustomerVo"] != null)
                    //    {
                    //        if (userVo.UserType != "Customer")
                    //        {
                    //            GeneralMenu.Style.Add("display", "none");
                    //            AdvisorHeader.Style.Add("display", "none");
                    //            RMHeader.Style.Add("display", "none");
                    //            CustomerIndividualHeader.Style.Add("display", "none");
                    //            CustomerNonIndividualHeader.Style.Add("display", "none");
                    //            BMHeader.Style.Add("display", "none");

                    //            CustomerVo customerVo = new CustomerVo();
                    //            customerVo = (CustomerVo)(Session["CustomerVo"]);

                    //            if (customerVo.Type == "IND")
                    //            {
                    //                RMCLientHeaderIndividual.Style.Add("display", "block");
                    //                RMCLientHeaderNonIndividual.Style.Add("display", "none");
                    //            }
                    //            else if (customerVo.Type == "NIND")
                    //            {
                    //                RMCLientHeaderIndividual.Style.Add("display", "none");
                    //                RMCLientHeaderNonIndividual.Style.Add("display", "block");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            GeneralMenu.Style.Add("display", "none");
                    //            AdvisorHeader.Style.Add("display", "none");
                    //            RMHeader.Style.Add("display", "none");
                    //            RMCLientHeaderIndividual.Style.Add("display", "none");
                    //            RMCLientHeaderNonIndividual.Style.Add("display", "none");
                                
                    //            BMHeader.Style.Add("display", "none");

                    //            CustomerVo customerVo = new CustomerVo();
                    //            customerVo = (CustomerVo)(Session["CustomerVo"]);

                    //            if (customerVo.Type == "IND")
                    //            {
                    //                CustomerIndividualHeader.Style.Add("display", "block");
                    //                CustomerNonIndividualHeader.Style.Add("display", "none"); 
                    //            }
                    //            else if (customerVo.Type == "NIND")
                    //            {
                    //                CustomerIndividualHeader.Style.Add("display", "none");
                    //                CustomerNonIndividualHeader.Style.Add("display", "block");
                    //            }
                    //        }
                        //}
                        //else
                        //{
                        //    if (rmVo.RMRole == "RM")
                        //    {
                        //        GeneralMenu.Style.Add("display", "none");
                        //        AdvisorHeader.Style.Add("display", "none");
                        //        RMHeader.Style.Add("display", "block");
                        //        CustomerIndividualHeader.Style.Add("display", "none");
                        //        CustomerNonIndividualHeader.Style.Add("display", "none");
                        //        RMCLientHeaderIndividual.Style.Add("display", "none");
                        //        RMCLientHeaderNonIndividual.Style.Add("display", "none");
                        //        BMHeader.Style.Add("display", "none");
                        //    }
                        //    else if (rmVo.RMRole == "Branch Man")
                        //    {
                        //        GeneralMenu.Style.Add("display", "none");
                        //        AdvisorHeader.Style.Add("display", "none");
                        //        RMHeader.Style.Add("display", "none");
                        //        CustomerIndividualHeader.Style.Add("display", "none");
                        //        CustomerNonIndividualHeader.Style.Add("display", "none");
                        //        RMCLientHeaderIndividual.Style.Add("display", "none");
                        //        RMCLientHeaderNonIndividual.Style.Add("display", "none");
                        //        BMHeader.Style.Add("display", "block");
                        //    }
                        //}
                    //}
                }
                else
                {
                    // If User Sessions are empty, load the login control 
                    //GeneralMenu.Style.Add("display", "block");
                    //AdvisorHeader.Style.Add("display", "none");
                    //RMHeader.Style.Add("display", "none");
                    //CustomerIndividualHeader.Style.Add("display", "none");
                    //CustomerNonIndividualHeader.Style.Add("display", "none");
                    //RMCLientHeaderIndividual.Style.Add("display", "none");
                    //RMCLientHeaderNonIndividual.Style.Add("display", "none");
                    //BMHeader.Style.Add("display", "none");
                    AdvisorLogo.Style.Add("display", "none");
                    BranchLogo.Style.Add("display", "none");

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

                if (AdvisorLogo.Src == "")
                {
                    AdvisorLogo.Style.Add("display", "none");
                }

                if (BranchLogo.Src == "")
                {
                    BranchLogo.Style.Add("display", "none");
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
