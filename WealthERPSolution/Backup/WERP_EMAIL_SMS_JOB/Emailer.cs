using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.Net.Mime;
using System.Net.Mail;
using System.Collections;
using System.IO;
using BoCommon;
using System.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections;
using System.Collections.Specialized;
using VoUser;
using BoAdvisorProfiling;
using BoCustomerProfiling;
using VoAdvisorProfiling;

namespace WERP_EMAIL_SMS_JOB
{
    public class Emailer
    {
        private static string _SMTPServer = ConfigurationSettings.AppSettings["SMTPServer"];
        private static int _SMTPPort = int.Parse(ConfigurationSettings.AppSettings["SMTPPort"]);
        private static string _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameOne"]);
        private static string _SMTPPassword = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPPassword"]);
        private static string _SMTPFromDisplay = ConfigurationSettings.AppSettings["SMTPFromDisplay"];
        private static string _SMTPFrom = ConfigurationSettings.AppSettings["SMTPFrom"];
        private static bool _SMTPDefaultCredentials = false;
        private static string _AdviserLogoDirectory = ConfigurationSettings.AppSettings["ADVISER_LOGO_PATH"];

        public static void SendMail(string To, string Cc, string Bcc, string Subject, string Body, ArrayList Attachments, string from,string emailTypeCode, DataTable dtAdviserSMTP, out string fromSMTPEmail,DataSet dsEmailTemplateDetails, out string statusMessage)
        {
                   statusMessage = "";
                   fromSMTPEmail = "";          

                string templateId = string.Empty;
                string reportType = string.Empty;
                AdvisorPreferenceVo advisorPreferenceVo = null;
                AdvisorVo advisorVo = null;
                CustomerVo customerVo = null;
                RMVo rmVo = null;
                AdvisorBo advisorBo = new AdvisorBo();
                CustomerBo customerBo = new CustomerBo();
                AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
                AdviserPreferenceBo adviserPreferenceBo = new AdviserPreferenceBo();
                DataTable dtEmailOutgoingParameterValues = dsEmailTemplateDetails.Tables[0];
                DataTable dtEmailTemplateParameters = dsEmailTemplateDetails.Tables[1];
                DataTable dtAdviserEmailTemplate = dsEmailTemplateDetails.Tables[2];
                DataTable dtAdviserTemplateParametersPre = dsEmailTemplateDetails.Tables[3];
               
                _SMTPFrom = from;
                SetAdviserSMTP(dtAdviserSMTP, _SMTPFrom);

                SmtpClient smtpClient = new SmtpClient();
                EmailMessage email = new EmailMessage();
                //MailMessage email = new MailMessage(_SMTPFromDisplay, To);
                _SMTPUsername = GetSMTPUserFromPool();
                fromSMTPEmail = _SMTPUsername;

                email.To.Add(To);
                //string name = userVo.FirstName + " " + userVo.MiddleName + " " + userVo.LastName;

                email.ReadSendMailTemplate(emailTypeCode);

                foreach (DataRow dr in dtEmailOutgoingParameterValues.Rows)
                {
                    switch (dr["WP_ParameterCode"].ToString())
                    {
                        case "AID":
                            advisorVo = advisorBo.GetAdvisor(Convert.ToInt32(dr["WRD_InputParameterValue"].ToString()));
                            advisorPreferenceVo = adviserPreferenceBo.GetAdviserPreference(advisorVo.advisorId);
                            break;
                        case "CID":
                            customerVo = customerBo.GetCustomer(Convert.ToInt32(dr["WRD_InputParameterValue"].ToString()));
                            rmVo = advisorStaffBo.GetAdvisorStaffDetails(customerVo.RmId);
                            break;
                        case "RT":
                            reportType = Convert.ToString(dr["WRD_InputParameterValue"]);
                            break;
                    }
                }

                foreach (DataRow dr in dtAdviserEmailTemplate.Rows)
                {
                    if (dr["WERPTTM_TypeCode"].ToString() == emailTypeCode)
                    {
                        email.Body = email.Body.Replace("[EMAIL_BODY]", dr["AHTMLT_TemplateBody"].ToString());
                        templateId = dr["AHTMLT_Id"].ToString();
                        break;
                    }
                }

                foreach (DataRow dr in dtEmailTemplateParameters.Rows)
                {
                    string templateCode = dr["WERPTPM_TemplateParameterCode"].ToString();
                    switch (templateCode)
                    {
                           
                        case "[ADVISER_NAME]":                           
                            email.Body = email.Body.Replace(templateCode, advisorVo.FirstName + " " + advisorVo.MiddleName + " " + advisorVo.LastName);
                            break;
                        case "[ADVISER_PHONE]":
                            email.Body = email.Body.Replace(templateCode, advisorVo.Phone1Std + "-" + advisorVo.Phone1Number);
                            break;
                        case "[ADVISER_MOBILE]":
                            email.Body = email.Body.Replace(templateCode, advisorVo.MobileNumber.ToString());
                            break;
                        case "[ADVISER_EMAIL]":
                            email.Body = email.Body.Replace(templateCode, advisorVo.Email.ToString());
                            break;
                        case "[A_WEB_SITE]":
                            email.Body = email.Body.Replace(templateCode, advisorPreferenceVo.WebSiteDomainName.ToString());
                            break;
                        case "[RM_NAME]":
                            email.Body = email.Body.Replace(templateCode, rmVo.FirstName + " " + rmVo.MiddleName + " " + rmVo.LastName);
                            break;
                        case "[RM_MOBILE]":
                            email.Body = email.Body.Replace(templateCode, rmVo.Mobile.ToString());
                            break;
                        case "[RM_EMAIL]":
                            email.Body = email.Body.Replace(templateCode, rmVo.Email.ToString());
                            break;
                        case "[CUSTOMER_FIRST_NAME]":
                            email.Body = email.Body.Replace(templateCode, customerVo.FirstName);
                            break;
                        case "[CUSTOMER_MIDDLE_NAME]":
                            email.Body = email.Body.Replace(templateCode, customerVo.MiddleName);
                            break;
                        case "[CUSTOMER_LAST_NAME]":
                            email.Body = email.Body.Replace(templateCode, customerVo.LastName);
                            break;
                        case "[START_LINE]":
                            email.Body = email.Body.Replace(templateCode, "<font face=" + "\"" + "[TEXT_FONT_NAME]" + "\"" + " size=" + "\"" + "[TEXT_FONT_SIZE]" + "\"" + " color=" + "\"" + "[TEXT_COLOR]" + "\"" + ">");
                            break;
                        case "[END_LINE]":
                            email.Body = email.Body.Replace(templateCode, "</font>");
                            break;
                        case "[LINE_BREAK]":
                            email.Body = email.Body.Replace(templateCode, "<br />");
                            break;
                        case "[ONE_EMPTY_SPACE]":
                            email.Body = email.Body.Replace(templateCode, "&nbsp;");
                            break;
                        case "[HYPERLINK_START]":
                            email.Body = email.Body.Replace(templateCode, "<a href=" + "\"" + "[A_WEB_SITE]" + "\"" + "target=" + "\"" + "_blank" + "\"" + ">");
                            break;
                        case "[HYPERLINK_END]":
                            email.Body = email.Body.Replace(templateCode, "</a>");
                            break;
                        case "[FONT_BOLD_START]":
                            email.Body = email.Body.Replace(templateCode, "<b>");
                            break;
                        case "[FONT_BOLD_END]":
                            email.Body = email.Body.Replace(templateCode, "</b>");
                            break;
                        case "[TEXT_FONT_NAME]":
                            email.Body = email.Body.Replace(templateCode, GetTemplateParamerValue(dtAdviserTemplateParametersPre, templateId, dr["WERPTPM_ParameterCode" ].ToString()));
                            break;
                        case "[TEXT_FONT_SIZE]":
                            email.Body = email.Body.Replace(templateCode, GetTemplateParamerValue(dtAdviserTemplateParametersPre, templateId, dr["WERPTPM_ParameterCode"].ToString()));
                            break;
                        case "[TEXT_COLOR]":
                            email.Body = email.Body.Replace(templateCode, GetTemplateParamerValue(dtAdviserTemplateParametersPre, templateId, dr["WERPTPM_ParameterCode"].ToString()));
                            break;
                        case "[REPORT_TYPE]":
                            //string reportType = GetTemplateParamerValue(dtAdviserTemplateParametersPre, reportTypeCode, dr["WERPTPM_ParameterCode"].ToString());
                            email.Body = email.Body.Replace(templateCode,reportType);
                            email.Subject = email.Subject.Replace(templateCode, reportType);
                            break;


                    }
                    

                    //email.Subject = email.Subject.Replace("MoneyTouch", advisorVo.OrganizationName);


                    //email.Body = email.Body.Replace("[ORGANIZATION]", advisorVo.OrganizationName);
                    //email.Body = email.Body.Replace("[CUSTOMER_NAME]", userVo.FirstName);
                    //if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Wealtherp")
                    //{
                    //    email.Body = email.Body.Replace("[WEBSITE]", advisorPreferenceVo.WebSiteDomainName);
                    //}
                    //else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "Citrus")
                    //{
                    //    email.Body = email.Body.Replace("[WEBSITE]", advisorPreferenceVo.WebSiteDomainName);
                    //}
                    //email.Body = email.Body.Replace("[CONTACTPERSON]", advisorVo.ContactPersonFirstName + " " + advisorVo.ContactPersonMiddleName + " " + advisorVo.ContactPersonLastName);
                    //if (!string.IsNullOrEmpty(advisorVo.Designation))
                    //    email.Body = email.Body.Replace("[DESIGNATION]", advisorVo.Designation);
                    //else
                    //    email.Body = email.Body.Replace("[DESIGNATION]", string.Empty);
                    //if (!string.IsNullOrEmpty(advisorVo.Phone1Number.ToString()))
                    //    email.Body = email.Body.Replace("[PHONE]", advisorVo.Phone1Std.ToString() + "-" + advisorVo.Phone1Number.ToString());
                    //else
                    //    email.Body = email.Body.Replace("[PHONE]", string.Empty);

                    //if (!string.IsNullOrEmpty(advisorVo.Email))
                    //    email.Body = email.Body.Replace("[EMAIL]", advisorVo.Email);
                    //else
                    //    email.Body = email.Body.Replace("[EMAIL]", string.Empty);


                    //if (_SMTPDefaultCredentials == true)
                    //{
                    //    NetworkCredential basicCredential = new NetworkCredential(_SMTPUsername, _SMTPPassword);
                    //    smtpClient.UseDefaultCredentials = false;
                    //    smtpClient.Credentials = basicCredential;
                    //}
                    //else
                    //{
                    //    smtpClient.UseDefaultCredentials = true;
                    //}

                    //smtpClient.Host = _SMTPServer;
                    //if (_SMTPPort > 0)
                    //    smtpClient.Port = _SMTPPort;

                    ////Hardcoding SSL settings for gmail SMTP
                    //if (_SMTPServer.Contains("smtp.gmail.com") || _SMTPServer.Contains("smtp.live.com"))
                    //{
                    //    smtpClient.EnableSsl = true;

                    //}

                    //if (Cc != null && Cc.Trim().Length > 0)
                    //    mail.CC.Add(Cc);
                    //if (Bcc != null && Bcc.Trim().Length > 0)
                    //    mail.Bcc.Add(Bcc);

                    //if (!string.IsNullOrEmpty(_SMTPFromDisplay.Trim()))
                    //{
                    //    if (_SMTPDefaultCredentials == true)
                    //    {
                    //        MailAddress md1 = new MailAddress(_SMTPUsername, _SMTPFromDisplay);
                    //        mail.From = md1;
                    //    }

                    //    if (!string.IsNullOrEmpty(from.Trim()))
                    //    {
                    //        MailAddress md3 = new MailAddress(_SMTPFrom, _SMTPFromDisplay);
                    //        mail.ReplyTo = md3;
                    //    }
                    //}
                    
                   

                    //mail.Subject = Subject;
                    //mail.IsBodyHtml = true;
                    //mail.Body = Body;

                    //if (mail.AlternateViews.Count != 0)
                    //{
                    //    foreach (AlternateView altrView in mail.AlternateViews)
                    //    {
                    //        mail.AlternateViews.Add(altrView);
                    //    }
                    //}

                    //smtpClient.Send(mail);
                }

                foreach (object obj in Attachments)
                {
                    Attachment attachment = attachment = new Attachment(obj.ToString());
                    email.Attachments.Add(attachment);
                }

                //SendMail(To, Cc, Bcc, email.Subject.ToString(), email.Body.ToString(), Attachments, from, dtAdviserSMTP,out fromSMTPEmail);

                email.Body = email.Body.Replace("[A_WEB_SITE]", advisorPreferenceVo.WebSiteDomainName.ToString());
               

                email.Body = email.Body.Replace("[A_LOGO]", "<img src='cid:HDIImage' alt='Logo'>");
                string logoPath = string.Empty;
                System.Net.Mail.AlternateView htmlView;
                System.Net.Mail.AlternateView plainTextView = System.Net.Mail.AlternateView.CreateAlternateViewFromString("Text view", null, "text/plain");
                //System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(hidBody.Value.Trim() + "<image src=cid:HDIImage>", null, "text/html");
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(email.Body, null, "text/html");
                //Add image to HTML version
                if (advisorVo != null)
                    logoPath = _AdviserLogoDirectory + "\\" + advisorVo.LogoPath;
                if (!File.Exists(logoPath))
                    logoPath = _AdviserLogoDirectory  + "\\spacer.png";
                //System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(Server.MapPath("~/Images/") + @"\3DSYRW_4009.JPG", "image/jpeg");
                System.Net.Mail.LinkedResource imageResource = new System.Net.Mail.LinkedResource(logoPath, "image/jpeg");
                imageResource.ContentId = "HDIImage";
                htmlView.LinkedResources.Add(imageResource);
                //Add two views to message.
                email.AlternateViews.Add(plainTextView);
                email.AlternateViews.Add(htmlView);

                //SendMail(To, Cc, Bcc, email.Subject.ToString(), email.Body.ToString(), Attachments, from, dtAdviserSMTP, out fromSMTPEmail);

                SendMail(email, out statusMessage);
            
            
        }

       public static string GetTemplateParamerValue(DataTable dtAdviserParameterPreference,string sourceId,string parameterCode)
        {
            string parameterValue=string.Empty;
            foreach(DataRow dr in dtAdviserParameterPreference.Rows)
            {
                if (dr["ATPP_SourceTypeCode"].ToString() == sourceId && dr["WERPTPM_ParameterCode"].ToString() == parameterCode)
                {
                    parameterValue = dr["ATPP_ParameterValue"].ToString();
                    break;
                }

            }

            return parameterValue;
        }

        public static void SetAdviserSMTP(DataTable dtAdviserSMTP, string from)
        {

            if (dtAdviserSMTP.Rows.Count > 0)
            {
                //_SMTPServer = dtAdviserSMTP.Rows[0]["ASS_HostServer"].ToString();
                //_SMTPPort = Convert.ToInt32(dtAdviserSMTP.Rows[0]["ASS_Port"].ToString());
                //_SMTPUsername = dtAdviserSMTP.Rows[0]["ASS_Email"].ToString();
                //_SMTPPassword = Encryption.Decrypt(dtAdviserSMTP.Rows[0]["ASS_Password"].ToString());

                _SMTPFrom = dtAdviserSMTP.Rows[0]["ASS_Email"].ToString();
                _SMTPFromDisplay = dtAdviserSMTP.Rows[0]["ASS_SenderEmailAlias"].ToString();
                if (string.IsNullOrEmpty(_SMTPFromDisplay.Trim()))
                {
                    _SMTPFromDisplay = dtAdviserSMTP.Rows[0]["A_OrgName"].ToString();
                }

                if (_SMTPFrom.Contains("WealthERP") || string.IsNullOrEmpty(_SMTPFrom))
                {
                    _SMTPFrom = dtAdviserSMTP.Rows[0]["A_Email"].ToString();
                }

                if (!string.IsNullOrEmpty(_SMTPFromDisplay) && !string.IsNullOrEmpty(_SMTPFrom))
                {
                    _SMTPFromDisplay = _SMTPFromDisplay + " <" + _SMTPFrom + ">";
                    _SMTPFrom = _SMTPFromDisplay;
                }
                _SMTPDefaultCredentials = Convert.ToBoolean(Convert.ToInt16(dtAdviserSMTP.Rows[0]["ASS_IsAuthenticationRequired"].ToString()));
            }


        }

        private static string GetSMTPUserFromPool()
        {
            DataTable dtSMTPSendMailCount = new DataTable();
            string _SMTPUsername = string.Empty;
            dtSMTPSendMailCount = Utils.GetSendEmailSMTPCount();
            if (dtSMTPSendMailCount.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSMTPSendMailCount.Rows)
                {
                    if (dr["FromSMTPEmailId"].ToString() == "admin1@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameOne"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameTwo"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin2@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameTwo"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameThree"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin3@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameThree"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameFour"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin4@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameFour"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameFive"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin5@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameFive"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameSeven"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin7@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameSeven"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameEight"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin8@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameEight"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameNine"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin9@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameNine"]);
                        else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameTen"]);
                    }
                    else if (dr["FromSMTPEmailId"].ToString() == "admin10@wealtherp.com")
                    {
                        if (Convert.ToInt16(dr["SendMailCount"].ToString()) <= 240)
                            _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameTen"]);
                        //else if (Convert.ToInt16(dr["SendMailCount"].ToString()) >= 240)
                        //    _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameFive"]);
                    }

                }

            }
            else
            {
                _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsernameOne"]);
            }


            return _SMTPUsername;
        }

        public static bool SendMail(EmailMessage email)
        {
            string statusMessage = string.Empty;
            return SendMail(email, out statusMessage);

        }

        public static bool SendMail(EmailMessage email, out string statusMessage)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mail = new MailMessage(_SMTPFromDisplay, email.To.First().ToString());
            statusMessage = string.Empty;
               //GetSMTPSettings();
            if (_SMTPDefaultCredentials == true)
            {
                NetworkCredential basicCredential = new NetworkCredential(_SMTPUsername, _SMTPPassword);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;
            }
            else
            {
                smtpClient.UseDefaultCredentials = true;
            }

                smtpClient.Host = _SMTPServer;
                if (_SMTPPort > 0)
                    smtpClient.Port = _SMTPPort;

                //Hardcoding SSL settings for gmail SMTP
                if (_SMTPServer.Contains("smtp.gmail.com") || _SMTPServer.Contains("smtp.live.com"))
                {
                    smtpClient.EnableSsl = true;

                }

                //mail.From = email.From;
            
                email.To.Remove(email.To.First());

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


                if (!string.IsNullOrEmpty(_SMTPFromDisplay.Trim()))
                {
                    if (_SMTPDefaultCredentials == true)
                    {
                        MailAddress md1 = new MailAddress(_SMTPUsername, _SMTPFromDisplay);
                        mail.From = md1;
                    }

                    if (!string.IsNullOrEmpty(_SMTPFrom.Trim()))
                    {
                        MailAddress md3 = new MailAddress(_SMTPFrom, _SMTPFromDisplay);
                        mail.ReplyTo = md3;
                    }
                }


                foreach (Attachment attachment in email.Attachments)
                {
                    mail.Attachments.Add(attachment);
                }


                mail.Subject = email.Subject;
                mail.IsBodyHtml = true;
                mail.Body = email.Body;
                if (email.AlternateViews.Count != 0)
                {
                    foreach (AlternateView altrView in email.AlternateViews)
                    {
                        mail.AlternateViews.Add(altrView);
                    }
                }
                //else if (ConfigurationSettings.AppSettings["HostName"].ToString() == "MoneyTouch")
                //{
                //    ////Send message
                //    ProcessMailForInlineAttachments(ref mail, mail.Body);
                //}

                //SaveMail(mail);

                smtpClient.Send(mail);

                return true;

           
        }

        public static void SendMail(string To, string Cc, string Bcc, string Subject, string Body, ArrayList Attachments, string from, DataTable dtAdviserSMTP, out string fromSMTPEmail)
        {

            SetAdviserSMTP(dtAdviserSMTP, from);

            SmtpClient smtpClient = new SmtpClient();
            MailMessage mail = new MailMessage(_SMTPFromDisplay, To);
            _SMTPUsername = GetSMTPUserFromPool();
            fromSMTPEmail = _SMTPUsername;

            if (_SMTPDefaultCredentials == true)
            {
                NetworkCredential basicCredential = new NetworkCredential(_SMTPUsername, _SMTPPassword);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;
            }
            else
            {
                smtpClient.UseDefaultCredentials = true;
            }

            smtpClient.Host = _SMTPServer;
            if (_SMTPPort > 0)
                smtpClient.Port = _SMTPPort;

            //Hardcoding SSL settings for gmail SMTP
            if (_SMTPServer.Contains("smtp.gmail.com") || _SMTPServer.Contains("smtp.live.com"))
            {
                smtpClient.EnableSsl = true;

            }

            if (Cc != null && Cc.Trim().Length > 0)
                mail.CC.Add(Cc);
            if (Bcc != null && Bcc.Trim().Length > 0)
                mail.Bcc.Add(Bcc);

            if (!string.IsNullOrEmpty(_SMTPFromDisplay.Trim()))
            {
                if (_SMTPDefaultCredentials == true)
                {
                    MailAddress md1 = new MailAddress(_SMTPUsername, _SMTPFromDisplay);
                    mail.From = md1;
                }

                if (!string.IsNullOrEmpty(from.Trim()))
                {
                    MailAddress md3 = new MailAddress(from, _SMTPFromDisplay);
                    mail.ReplyTo = md3;
                }
            }


            foreach (object obj in Attachments)
            {
                Attachment attachment = attachment = new Attachment(obj.ToString());
                mail.Attachments.Add(attachment);
            }

            mail.Subject = Subject;
            mail.IsBodyHtml = true;
            mail.Body = Body;

            if (mail.AlternateViews.Count != 0)
            {
                foreach (AlternateView altrView in mail.AlternateViews)
                {
                    mail.AlternateViews.Add(altrView);
                }
            }

            smtpClient.Send(mail);


        }
    }
}
