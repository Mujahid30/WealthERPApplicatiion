using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VoUser;
using BoAdvisorProfiling;
using BoUser;
using PCGMailLib;
using System.Net.Mail;
using System.Collections.Specialized;
using WealthERP.Base;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Configuration;
using BoCommon;
namespace WealthERP.Advisor
{
    public partial class AdvisorCustomerGeneratePassword : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        UserVo advisorUserVo = new UserVo();
        UserBo userBo = new UserBo();
        AdvisorVo advisorVo = new AdvisorVo();
        Random r = new Random();
        PcgMailMessage email = new PcgMailMessage();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionBo.CheckSession();
                int userId;
                //userVo = (UserVo)Session["CustomerUser"];
                advisorUserVo = (UserVo)Session[SessionContents.UserVo];

                if (Request.QueryString["GenLoginPassword_UserId"] != null)
                {
                    userId = int.Parse(Request.QueryString["GenLoginPassword_UserId"].ToString());
                    userVo = userBo.GetUserDetails(userId);
                }

                if (userVo.Password != null)
                    try
                    {
                        lblPassword.Text = Encryption.Decrypt(userVo.Password);
                    }
                    catch (Exception ex)
                    {
                    }
                if (userVo.LoginId != null && userVo.LoginId != "")
                {
                    btnGenerate.Text = "Reset Password";
                    lblLoginId.Text = userVo.LoginId;
                }
                else
                    btnGenerate.Text = "Generate Login and Password";
                btnGenerate.Enabled = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GenerateLoginPassword.ascx:Page_Load()");
                object[] objects = new object[1];
                objects[0] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "str", "<script>alert('Hai')</script>");

                string password = r.Next(20000, 100000).ToString();
                if (userVo.LoginId == null || userVo.LoginId == "")
                    userVo.LoginId = r.Next(10000000, 99999999).ToString();
                userVo.Password = Encryption.Encrypt(password);
                userVo.IsTempPassword = 1;

                userBo.UpdateUser(userVo);

                //Sending email to Customer
                if (userVo.Email != null)
                {
                    if(btnGenerate.Text=="Reset Password")
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "str", "<script>alert('Password reset successfully !')</script>");
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "str", "<script>alert('Login Credentials created successfully !')</script>");
                    SendMail(userVo);
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "str", "<script>alert('Login Credentials created successfully. Not able to send mail as E-mail Id is not specified')</script>");
                lblLoginId.Text = userVo.LoginId;
                lblPassword.Text = Encryption.Decrypt(userVo.Password);
                btnGenerate.Enabled = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GenerateLoginPassword.ascx:btnGenerate_Click()");
                object[] objects = new object[1];
                objects[0] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        protected void lnkBtnBack_Click(object sender, EventArgs e)
        {
            if (Session["Current_Link"].ToString() == "RMLeftPane")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('RMCustomer','none');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "pageloadscript", "loadcontrol('AdviserCustomer','none');", true);
            }
        }

        private void SendMail(UserVo userVo)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            
            try
            {
                email.To.Add(userVo.Email);
                string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                if(btnGenerate.Text=="Reset Password")
                    email.GetResetPasswordMail(userVo.LoginId, Encryption.Decrypt(userVo.Password), name);
                else
                    email.GetCustomerAccountMail(userVo.LoginId, Encryption.Decrypt(userVo.Password), name);
                emailer.SendMail(email);
            }
            catch (Exception ex)
            {

            }
        }
    }
}