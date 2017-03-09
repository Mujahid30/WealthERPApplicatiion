using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoUser;
using BoUser;
using System.Web.UI;
using PCGMailLib;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Configuration;
using BoCommon;
using VoAdvisorProfiling;
using BoAdvisorProfiling;
using System.IO;

namespace WealthERP.General
{
    public partial class ForgotPassword : System.Web.UI.UserControl
    {
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        Random r = new Random();
        EmailMessage email = new EmailMessage();
        Emailer emailer = new Emailer();
        AdvisorVo advisorVo = new AdvisorVo();
        AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
        AdvisorBo advisorBo = new AdvisorBo();

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionBo.CheckSession();
            //lblError.Visible = false;
            //lblMailSent.Visible = false;
            advisorVo = (AdvisorVo)Session["AdvisorVo"];
           // ValidateLoginAsStaffOrAssociate();

        }

        //private bool ValidateLoginAsStaffOrAssociate()
        //{
           // string userType=string.Empty;
           // int userId=0;
           // bool result = true;
           // if (!string.IsNullOrEmpty(txtLoginId.Text))
           // {
           //      if (!string.IsNullOrEmpty(txtEmail.Text))
           //   {
           //         userBo.ValidateLoginAsStaffOrAssociate(txtLoginId.Text, txtEmail.Text, 0,out userType,out  userId);

           //}
           // }
           // return result;
        //}

        protected void txtEmail_OnTextChanged(object sender, EventArgs e)
        {
            string userType = string.Empty;
            int userId = 0;
            if (!string.IsNullOrEmpty(txtLoginId.Text))
            {
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                 userBo.ValidateLoginAsStaffOrAssociate(txtLoginId.Text, txtEmail.Text, 0, out userType, out  userId);

                }
            }

            if (userId >= 0)
            {
                if (userType.ToUpper() == "Associates".ToUpper())
                {
                    trPan.Visible = true;
                }
                else
                {
                    trPan.Visible = false;

                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            advisorVo = (AdvisorVo)Session["advisorVo"];
            if (Session["AdvisorPreferenceVo"] != null)
                advisorPreferenceVo = (AdvisorPreferenceVo)Session["AdvisorPreferenceVo"];
            // userVo = userBo.GetUser(txtLoginId.Text);
            userVo = userBo.GetUserReset(txtLoginId.Text, txtEmail.Text, TxtPan.Text);
            if (userVo != null)
            {
                advisorVo = advisorBo.GetAssociateAdviserUser(userVo.UserId);
            }
            Random r = new Random();
            OneWayEncryption encryption;
            encryption = new OneWayEncryption();
            bool isSuccess = false;
            string logoPath = string.Empty;
            string statusMessage = string.Empty;
            ScriptManager script = new ScriptManager();


            if (userVo != null)  // && userVo.Email == txtEmail.Text )
            {

                string hassedPassword = string.Empty;
                string saltValue = string.Empty;
                string password = r.Next(20000, 100000).ToString();

                //userVo = userBo.GetUserDetails(userId);
                //  string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                encryption.GetHashAndSaltString(password, out hassedPassword, out saltValue);
                userVo.Password = hassedPassword;
                userVo.PasswordSaltValue = saltValue;
                userVo.OriginalPassword = password;
                userVo.IsTempPassword = 1;
                //  isSuccess = userBo.UpdateUser(userVo);
                isSuccess = userBo.UpdateUserReset(userVo);

                string userName = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
                email.GetResetPasswordMail(userVo.LoginId, password, userName);
                email.Subject = email.Subject.Replace("WealthERP", advisorVo.OrganizationName);
                email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);
                //email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
                email.Body = email.Body.Replace("[CUSTOMER_NAME]", userVo.FirstName);
                if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                {
                    email.Body = email.Body.Replace("[WEBSITE]", advisorPreferenceVo.WebSiteDomainName);
                }
                else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Citrus")
                {
                    email.Body = email.Body.Replace("[WEBSITE]", advisorPreferenceVo.WebSiteDomainName);
                }

                email.Body = email.Body.Replace("[CONTACTPERSON]", advisorVo.ContactPersonFirstName + " " + advisorVo.ContactPersonMiddleName + " " + advisorVo.ContactPersonLastName);
                if (!string.IsNullOrEmpty(advisorVo.Designation))
                    email.Body = email.Body.Replace("[DESIGNATION]", advisorVo.Designation.Trim());
                else
                    email.Body = email.Body.Replace("[DESIGNATION]", string.Empty);
                if (!string.IsNullOrEmpty(advisorVo.Phone1Number.ToString()))
                    email.Body = email.Body.Replace("[PHONE]", advisorVo.Phone1Std.ToString().Trim() + "-" + advisorVo.Phone1Number.ToString().Trim());
                else
                    email.Body = email.Body.Replace("[PHONE]", string.Empty);

                if (!string.IsNullOrEmpty(advisorVo.Email))
                    email.Body = email.Body.Replace("[EMAIL]", advisorVo.Email.Trim());
                else
                    email.Body = email.Body.Replace("[EMAIL]", string.Empty);

                email.Body = email.Body.Replace("[LOGO]", "<img src='cid:HDIImage' alt='Logo'>");

                System.Net.Mail.AlternateView htmlView;
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body " + "style='font-family:Tahoma, Arial; font-size: 10pt;'><p>" + email.Body + "</p></body></html>", null, "text/html");
                //Add image to HTML version
                if (advisorVo != null)
                    logoPath = "~/Images/" + advisorVo.LogoPath;
                if (!File.Exists(Server.MapPath(logoPath)))
                    logoPath = "~/Images/spacer.png";
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath(logoPath), "image/jpeg");
                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                email.AlternateViews.Add(plainTextView);
                email.AlternateViews.Add(htmlView);

                email.To.Add(userVo.Email);

                AdviserStaffSMTPBo adviserStaffSMTPBo = new AdviserStaffSMTPBo();
                int rmId = userVo.rmid;
                AdviserStaffSMTPVo adviserStaffSMTPVo = adviserStaffSMTPBo.GetSMTPCredentials(rmId);
                if (adviserStaffSMTPVo.HostServer != null && adviserStaffSMTPVo.HostServer != string.Empty)
                {
                    emailer.isDefaultCredentials = !Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired);

                    if (!String.IsNullOrEmpty(adviserStaffSMTPVo.Password))
                        emailer.smtpPassword = Encryption.Decrypt(adviserStaffSMTPVo.Password);
                    emailer.smtpPort = int.Parse(adviserStaffSMTPVo.Port);
                    emailer.smtpServer = adviserStaffSMTPVo.HostServer;
                    emailer.smtpUserName = adviserStaffSMTPVo.Email;

                    if (Convert.ToBoolean(adviserStaffSMTPVo.IsAuthenticationRequired))
                    {
                        if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                        {
                            email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                        }
                        else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                        {
                            email.From = new MailAddress(emailer.smtpUserName, advisorVo.OrganizationName);
                        }

                    }
                }
                bool isMailSent = false;

                if (userBo.UpdateUser(userVo))
                {
                    isMailSent = emailer.SendMail(email);
                }

                if (isMailSent)
                {
                    statusMessage = "Credentials have been reset and sent to your mail";
                    tblMessage.Visible = true;
                    ErrorMessage.Visible = false;
                    SuccessMsg.InnerText = statusMessage;
                    SuccessMsg.Visible = true;
                    txtLoginId.Text = "";
                    txtEmail.Text = "";
                    TxtPan.Text = "";
                }
            }
            else
            {
                statusMessage = "Password has been not reset sucessfully";
                tblMessage.Visible = true;
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = statusMessage;
                SuccessMsg.Visible = false;
                txtLoginId.Text = "";
                txtEmail.Text = "";
                TxtPan.Text = "";
            }
            //if (isSuccess)
            //{
            //    tblMessage.Visible = true;
            //    SuccessMsg.Visible = true;
            //    ErrorMessage.Visible = false;
            //    SuccessMsg.InnerText = "Password has been reset successfully...";
            //    txtLoginId.Text = "";
            //    txtEmail.Text = "";
            //    TxtPan.Text = "";
            //}
            //else
            //{
            //    tblMessage.Visible = true;
            //    SuccessMsg.Visible = false;
            //    ErrorMessage.Visible = true;
            //    ErrorMessage.InnerText = "An error occurred while reseting password.";

            //}

        }
        //{
        //    userVo.Password = r.Next(20000, 100000).ToString();
        //    userVo.IsTempPassword = 1;
        //    userVo.Password = Encryption.Encrypt(userVo.Password);
        //    userBo.UpdateUser(userVo);


        //    //Send email
        //    //
        //    //string EmailPath = Server.MapPath(ConfigurationManager.AppSettings["EmailPath"].ToString());
        //    //email.SendForgotPasswordMail("admin@wealtherp.net", userVo.Email, userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName, userVo.LoginId, userVo.Password, EmailPath);
        //    bool isMailSent = SendMail(userVo);
        //    //
        //    if (!isMailSent)
        //    {
        //        lblMailSent.Text = "An error occurred while sending mail to your Email ID";

        //    lblMailSent.Visible = true;
        //    lblError.Visible = false;
        //    trEmail.Visible = false;
        //    trLogin.Visible = false;
        //    btnSubmit.Visible = false;
        //    lnkSignIn.Visible = true;

        //}
        //else if (userVo == null || userVo.Email != txtEmail.Text)
        //{
        //    lblError.Text = "Login ID and Email ID do not match";
        //    lblError.Visible = true;
        //}
        //else
        //{
        //    lblError.Visible = true;
        //    lblMailSent.Visible = false;
        //}
        protected void lnkBackbutton_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('UserLogin','none');", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "UserLogin", "loadcontrol('UserLogin');", true);


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "UserLogin", "loadcontrol('UserLogin');", true);
        }




        private bool SendMail(UserVo userVo)
        {
            Emailer emailer = new Emailer();
            EmailMessage email = new EmailMessage();
            email.To.Add(userVo.Email);
            string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;
            // string password = Encryption.Decrypt(userVo.Password);
            //  email.GetForgotPasswordMail(userVo.LoginId,userVo.Password, name);
            bool isMailSent = emailer.SendMail(email);
            return isMailSent;
        }

        protected void lnkSignIn_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "pageloadscript", "loadcontrol('Userlogin','none');", true);
        }
    }
}