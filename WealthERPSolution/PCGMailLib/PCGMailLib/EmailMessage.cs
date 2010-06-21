using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Configuration;

namespace PCGMailLib
{

    public enum EmailTypes
    {
        AdviserRegistration = 1,
        AdviserRMAccount = 2,
        AdviserRegistrationNotification = 3,
        ForgotPassword = 4,
        CustomerCredentials = 5,
        ResetPassword = 6,   
        ReportMails =7
    }

    public class EmailMessage : MailMessage
    {
        const string EmailTemplatesFolder = "\\Emails\\Templates\\";


        /// <summary>
        /// Assign DisplayName,From Email Id and Subject based on the email type.
        /// </summary>
        /// <param name="emailTypes"></param>
        /// <param name="body"></param>
        public void AssignMailSettings(EmailTypes emailTypes, string body)
        {

            switch (emailTypes)
            {
                case EmailTypes.AdviserRegistration:
                    this.From = new MailAddress("admin@werp.com", "WealthERP Adviser Registration");
                    this.Subject = "WERP Adviser Registration";
                    break;
                case EmailTypes.AdviserRMAccount:
                    this.From = new MailAddress("admin@werp.com", "WealthERP");
                    this.Subject = "WealthERP Account Details";
                    break;
                case EmailTypes.ForgotPassword:
                    this.From = new MailAddress("admin@werp.com", "WealthERP");
                    this.Subject = "WealthERP Forgot Password";
                    break;
                case EmailTypes.CustomerCredentials:
                    this.From = new MailAddress("admin@werp.com", "WealthERP");
                    this.Subject = "WealthERP Customer Account Credentials";
                    break;
                case EmailTypes.ResetPassword:
                    this.From = new MailAddress("admin@werp.com", "WealthERP");
                    this.Subject = "WealthERP Reset Password";
                    break;
                case EmailTypes.AdviserRegistrationNotification:
                    this.To.Clear();
                    this.CC.Clear();
                    string toMailIdString = ConfigurationManager.AppSettings["AdminNotificationTo"].ToString();
                    string[] toMailIds = toMailIdString.Split(',');
                    foreach (string mailId in toMailIds)
                    {
                        this.To.Add(mailId);
                    }

                    string CCMailIdString = ConfigurationManager.AppSettings["AdminNotificationCC"].ToString();
                    string[] CCMailIds = CCMailIdString.Split(',');
                    foreach (string mailId in CCMailIds)
                    {
                        this.CC.Add(mailId);
                    }

                    this.From = new MailAddress("admin@werp.com", "WealthERP");
                    this.Subject = "WealthERP Adviser Registration";
                    break;
            }
            this.Body = body;

        }



        /// <summary>
        /// Read the content of the email template file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string ReadTemplate(string fileName)
        {

            fileName = HttpContext.Current.Server.MapPath(EmailTemplatesFolder + fileName);
            return File.ReadAllText(fileName);

        }

        #region MailContents
        /// <summary>
        /// Get the content of the Adviser registration mail
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        public void GetAdviserRegistrationMail(string loginId, string password, string name)
        {
            string emailContent = string.Empty;

            emailContent = ReadTemplate("AdviserRegistration.html");
            emailContent = emailContent.Replace("[LOGIN-ID]", loginId);
            emailContent = emailContent.Replace("[PASSWORD]", password);
            emailContent = emailContent.Replace("[Name]", name);

            AssignMailSettings(EmailTypes.AdviserRegistration, emailContent);


        }

        /// <summary>
        /// Get the content of Adviser Registration Mail notification.
        /// This mail is for notifying the administrator(s) when an adviser is registered.
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="emailId"></param>
        /// <param name="name"></param>
        public void GetAdviserRegistrationMailNotification(string organizationname,string city,long mobileno,string loginId, string emailId, string name)
        {
            string emailContent = string.Empty;

            emailContent = ReadTemplate("AdviserRegistrationNotification.html");
            emailContent = emailContent.Replace("[ORGANIZATION-NAME]", organizationname);
            emailContent = emailContent.Replace("[CITY]", city);
            emailContent = emailContent.Replace("[MOBILE NO]", mobileno.ToString());            
            emailContent = emailContent.Replace("[LOGIN-ID]", loginId);
            emailContent = emailContent.Replace("[EMAIL-ID]", emailId);
            emailContent = emailContent.Replace("[NAME]", name);


            AssignMailSettings(EmailTypes.AdviserRegistrationNotification, emailContent);


        }

        /// <summary>
        /// Get the content of the Forgot password mail
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        public void GetForgotPasswordMail(string loginId, string password, string name)
        {
            string emailContent = string.Empty;

            emailContent = ReadTemplate("ForgotPassword.html");
            emailContent = emailContent.Replace("[LOGIN-ID]", loginId);
            emailContent = emailContent.Replace("[PASSWORD]", password);
            emailContent = emailContent.Replace("[Name]", name);

            AssignMailSettings(EmailTypes.ForgotPassword, emailContent);
        }

        /// <summary>
        /// Get the content of the Adviser RM Account mail.
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        public void GetAdviserRMAccountMail(string loginId, string password, string name)
        {
            string emailContent = string.Empty;

            emailContent = ReadTemplate("AdviserRMAccounts.html");
            emailContent = emailContent.Replace("[LOGIN-ID]", loginId);
            emailContent = emailContent.Replace("[PASSWORD]", password);
            emailContent = emailContent.Replace("[Name]", name);

            AssignMailSettings(EmailTypes.AdviserRMAccount, emailContent);


        }

        /// <summary>
        /// Get the content of the Adviser RM Account mail.
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        public void GetCustomerAccountMail(string loginId, string password, string name)
        {
            string emailContent = string.Empty;

            emailContent = ReadTemplate("CustomerCredentials.html");
            emailContent = emailContent.Replace("[LOGIN-ID]", loginId);
            emailContent = emailContent.Replace("[PASSWORD]", password);
            emailContent = emailContent.Replace("[Name]", name);

            AssignMailSettings(EmailTypes.CustomerCredentials, emailContent);


        }

        /// <summary>
        /// Get the content of the Reset Password mail.
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        public void GetResetPasswordMail(string loginId, string password, string name)
        {
            string emailContent = string.Empty;

            emailContent = ReadTemplate("ResetPassword.html");
            emailContent = emailContent.Replace("[LOGIN-ID]", loginId);
            emailContent = emailContent.Replace("[PASSWORD]", password);
            emailContent = emailContent.Replace("[Name]", name);

            AssignMailSettings(EmailTypes.ResetPassword, emailContent);

        }

        #endregion

    }

}


