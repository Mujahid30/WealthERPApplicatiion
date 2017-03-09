using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BoUser;
using VoUser;
using BoCommon;
using BoAdvisorProfiling;
using BoCustomerProfiling;

namespace WealthERP_Login_Widget
{
    public partial class _Default : System.Web.UI.Page
    {
        static int adviserId;
        static bool isMainDomain = false;
        string adviserDomain = "";
        string host = "";
        
        Uri alias;
        UserVo userVo = new UserVo();
        string refLink = ConfigurationManager.AppSettings["RefLink"].ToString();
        string appName = ConfigurationManager.AppSettings["AppName"].ToString();
        string MainDomain = ConfigurationManager.AppSettings["MainDomain"].ToString();
        string imageURL = ConfigurationManager.AppSettings["ImageURL"].ToString();
        protected void Page_PreInit(object sender, EventArgs e)
        {
           

            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string url="";
            AdvisorBo adviserBo=new AdvisorBo();           
           
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    alias = Request.UrlReferrer;
                    host = alias.Host.ToString();
                }

                if (Request.QueryString["AdviserId"] != null)
                {

                    url = Request.QueryString["AdviserId"].ToString();
                    adviserId = int.Parse(Encryption.Decrypt(url).ToString());
                }
                if (adviserId != 0)
                {
                    if (host.Contains(MainDomain.ToString()))
                        isMainDomain = true;
                    else
                        isMainDomain = false;
                    if (!isMainDomain)
                    {
                        adviserDomain = adviserBo.GetAdviserDomainName(adviserId);
                        tblLogoBlock.Visible = false;
                    }
                    else
                        tblLogoBlock.Visible = true;
                    if (isMainDomain || host.Contains(adviserDomain))
                    {
                        if (Session["CurrentUserVo"] == null)
                        {
                            lblLoginMessage.Visible = false;                           
                            tblLoginBlock.Visible = true;
                            lnklogout.Visible = false;
                        }
                        else
                        {
                            lblLoginMessage.Visible = true;
                            userVo = (UserVo)Session["CurrentUserVo"];
                            lblLoginMessage.Text = "Logged In to " + appName.ToString() + " as " + userVo.FirstName + ' ' + userVo.MiddleName + ' ' + userVo.LastName;
                            lblLoginMessage.CssClass = "FieldName";
                            tblLoginBlock.Visible = false;
                            lnklogout.Visible = true;
                        }
                    }
                    else
                    {
                        tblLoginBlock.Visible = false;
                        lnklogout.Visible = false;
                        lblLoginMessage.Visible = true;
                        lblLoginMessage.Text = "Your Domain is not regsitered At " + appName.ToString() + ". Please contact " + appName.ToString() + " Admin";
                        lblLoginMessage.CssClass = "FieldError";
                    }
                }
                else
                {
                    tblLoginBlock.Visible = false;
                    lnklogout.Visible = false;
                    lblLoginMessage.Visible = true;
                    lblLoginMessage.Text = "Your Domain is not regsitered At " + appName.ToString() + ". Please contact " + appName.ToString() + " Admin";
                    lblLoginMessage.CssClass = "FieldError";
                    
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserBo userBo = new UserBo();
           
            AdvisorStaffBo adviserStaffBo = new AdvisorStaffBo();
            RMVo rmVo = new RMVo();
            CustomerVo customerVo = new CustomerVo();
            CustomerBo customerBo=new CustomerBo();
           
           
            if(userBo.ValidateUser(txtUserName.Text.ToString(),txtPassword.Text.ToString()))
            {
                
                userVo = userBo.GetUser(txtUserName.Text.ToString());
                if (userVo.UserType == "Advisor")
                {
                    rmVo = adviserStaffBo.GetAdvisorStaff(userVo.UserId);
                    if (rmVo.AdviserId == adviserId || isMainDomain == true)
                    {
                        lblLoginMessage.Visible = true;
                        lblLoginMessage.Text = "Logged In to "+appName.ToString()+" as " + userVo.FirstName + ' ' + userVo.MiddleName + ' ' + userVo.LastName;
                        lblLoginMessage.CssClass = "FieldName";
                        tblLoginBlock.Visible = false;

                        Session["CurrentUserVo"] = userVo;
                        lnklogout.Visible = true;
                        Response.Write("<script>window.open('"+refLink+"?UserId="+Encryption.Encrypt(userVo.UserId.ToString())+"','_blank');</script>");
                    }
                    else
                    {
                        lblLoginMessage.Visible = true;
                        tblLoginBlock.Visible = true;
                        lblLoginMessage.Text = "Authentication Failed";
                        lblLoginMessage.CssClass = "FieldError";
                    }
                }
                else if (userVo.UserType == "Customer")
                {
                    customerVo = customerBo.GetCustomerInfo(userVo.UserId);
                    rmVo = adviserStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                    if (rmVo.AdviserId == adviserId || isMainDomain == true)
                    {
                        lblLoginMessage.Visible = true;
                        lblLoginMessage.Text = "Logged In to " + appName.ToString() + " as " + userVo.FirstName + ' ' + userVo.MiddleName + ' ' + userVo.LastName;
                        lblLoginMessage.CssClass = "FieldName";
                        Session["CurrentUserVo"] = userVo;
                        tblLoginBlock.Visible = false;
                        lnklogout.Visible = true;
                        Response.Write("<script>window.open('" + refLink + "?UserId=" + Encryption.Encrypt(userVo.UserId.ToString()) + "','_blank');</script>");
                    }
                    else
                    {
                        lblLoginMessage.Visible = true;
                        tblLoginBlock.Visible = false;
                        lblLoginMessage.Text = "Authentication Failed";
                        lblLoginMessage.CssClass = "FieldError";
                    }
                }
            }
            else
            {
                lblLoginMessage.Text = "Invalid LoginId or Password";
                lblLoginMessage.Visible = true;
                lblLoginMessage.CssClass = "FieldError";
            }
        }

        protected void lnklogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUserVo");
            tblLoginBlock.Visible = true;
            lblLoginMessage.Visible = false;
            lnklogout.Visible = false;
        }
    }
}
