using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Configuration;

namespace WERP_EMAIL_SMS_JOB
{

    public enum EmailTypes
    {
        AdviserRegistration = 1,
        AdviserRMAccount = 2,
        AdviserRegistrationNotification = 3,
        ForgotPassword = 4,
        CustomerCredentials = 5,
        ResetPassword = 6,
        ReportMails = 7,
        AdviserLoginWidget = 8
    }

    public class EmailMessage : MailMessage
    {      
        private static string EmailTemplatesFolder = ConfigurationSettings.AppSettings["EMailTemplatePath"];
        
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
                    case EmailTypes.AdviserLoginWidget:
                        this.From = new MailAddress("admin@werp.com", "WealthERP Login Widget");
                        this.Subject = "WealthERP Login Widget";
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
                    case EmailTypes.ReportMails:
                        this.From = new MailAddress("admin@werp.com", "WealthERP");
                        this.Subject = "[REPORT_TYPE]";
                        break;
                        //case EmailTypes.AdviserRegistrationNotification:
                        //    this.To.Clear();
                        //    this.CC.Clear();
                        //    string toMailIdString = ConfigurationManager.AppSettings["AdminNotificationTo"].ToString();
                        //    string[] toMailIds = toMailIdString.Split(',');
                        //    foreach (string mailId in toMailIds)
                        //    {
                        //        this.To.Add(mailId);
                        //    }

                        //    string CCMailIdString = ConfigurationManager.AppSettings["AdminNotificationCC"].ToString();
                        //    string[] CCMailIds = CCMailIdString.Split(',');
                        //    foreach (string mailId in CCMailIds)
                        //    {
                        //        this.CC.Add(mailId);
                        //    }

                        //this.From = new MailAddress("admin@werp.com", "WealthERP");
                        //this.Subject = "WealthERP Adviser Registration";
                        //break;
                }
            

            this.Body = body;

        }





        #region SendMailTemplate

        /// <summary>
        /// Get the content of the Report
        /// </summary>
        /// <param name="encryptedAdviserId"></param>
        /// <param name="name"></param>
        public void ReadSendMailTemplate(string emailTypeCode)
        {
            string emailContent = string.Empty;
            emailContent = ReadTemplate("SendMail.html");
            //emailContent = emailContent.Replace("[ORGANIZATION]", "Wealtherp");  
            if (emailTypeCode == "RT")
            AssignMailSettings(EmailTypes.ReportMails, emailContent);

        }

        /// <summary>
        /// Read the content of the email template file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string ReadTemplate(string fileName)
        {
            //fileName = HttpContext.Current.Server.MapPath(EmailTemplatesFolder + fileName);
            return File.ReadAllText(EmailTemplatesFolder + fileName);

        }

        #endregion

    }
}
