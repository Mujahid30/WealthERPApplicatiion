using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace PCGMailLib
{
    /// <summary>
    /// Utility class for sending email
    /// </summary>
    public class Emailer : System.Web.UI.Page
    {
        public string smtpfromEmailId;
        public string smtpServer;
        public int smtpPort = 0;
        public string smtpUserName;
        public string smtpPassword;
        public bool? isDefaultCredentials;

        const string EmailSentFolder = "\\Emails\\SentFolder\\";

        /// <summary>
        /// Get the  SMTP settings from web.config file.
        /// </summary>
        /// <remarks>An if condition is added to allow overriding the default settings
        /// by directly assigning values.If a previous value is set, then the default settings will
        /// not be assigned.</remarks>
        private void GetSMTPSettings()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            if (smtpServer == null)
                smtpServer = settings.Smtp.Network.Host;
            if (smtpPort == 0)
                smtpPort = settings.Smtp.Network.Port;
            if (smtpUserName == null)
                smtpUserName = settings.Smtp.Network.UserName;
            if (smtpPassword == null)
                smtpPassword = settings.Smtp.Network.Password;
            //For some mail servers like yahoo, the from mail Id should be the user name.
            if (smtpfromEmailId == null && isDefaultCredentials.HasValue && isDefaultCredentials.Value == false)
                smtpfromEmailId = smtpUserName;
            else if (smtpfromEmailId == null || smtpfromEmailId != string.Empty)
                smtpfromEmailId = settings.Smtp.From;
            if (!isDefaultCredentials.HasValue)
                isDefaultCredentials = settings.Smtp.Network.DefaultCredentials;
        }

        /// <summary>
        /// This function send mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool SendMail(EmailMessage email)
        {
            string statusMessage = string.Empty;
            return SendMail(email, out statusMessage);

        }
        public bool SendMail(EmailMessage email, out string statusMessage)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mail = new MailMessage();
            statusMessage = string.Empty;
            try
            {
                GetSMTPSettings();
                if (isDefaultCredentials == false)
                {
                    NetworkCredential basicCredential = new NetworkCredential(this.smtpUserName, this.smtpPassword);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;
                }

                smtpClient.Host = smtpServer;
                if (smtpPort > 0)
                    smtpClient.Port = smtpPort;

                //Hardcoding SSL settings for gmail SMTP
                if (smtpServer.Contains("smtp.gmail.com"))
                    smtpClient.EnableSsl = true;

                mail.From = email.From;

                foreach (MailAddress toEmail in email.To)
                {
                    mail.To.Add(toEmail);
                }
                foreach (MailAddress CCEmail in email.CC)
                {
                    mail.CC.Add(CCEmail);
                }
                foreach (MailAddress BccEmail in email.Bcc)
                {
                    mail.Bcc.Add(BccEmail);
                }
                foreach (Attachment attachment in email.Attachments)
                {
                    mail.Attachments.Add(attachment);
                }

                mail.Subject = email.Subject;
                mail.IsBodyHtml = true;                
                mail.Body = email.Body;
                
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("<html><body><table align=\"center\" width=\"60%\"><tr><td><img src=cid:HDIImage alt=\"MoneyTouch360&deg\" height=\"60px\">"+mail.Body, null, "text/html");
                //Add image to HTML version
                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("\\Images\\Money_Touch_360_logo1.gif"), "image/gif");
                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                //mail.AlternateViews.Add(plainTextView);
                mail.AlternateViews.Add(htmlView);
                //Send message
               
                SaveMail(mail);

                smtpClient.Send(mail);

                return true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Emailer.cs:SendMail()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                statusMessage = Ex.Message;
                return false;
                //throw exBase;
            }
        }

        /// <summary>
        /// Save mail as an HTML file.
        /// </summary>
        /// <param name="email"></param>
        private void SaveMail(MailMessage email)
        {
            try
            {
                string folder = HttpContext.Current.Server.MapPath(EmailSentFolder);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = folder + email.To + DateTime.Now.Ticks.ToString() + ".html";
                File.AppendAllText(fileName, email.Body);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Emailer.cs:SendMail()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

    }
}
