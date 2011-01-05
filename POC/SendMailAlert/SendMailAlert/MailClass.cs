using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SendMailAlert
{
    class MailClass
    {
        public bool SendMail(MailVo mailVo)
        {
            bool result = false;
            string filepath = ConfigurationSettings.AppSettings["logPath"].ToString();
            string filepath_url = "";
            string attachment_url = ConfigurationSettings.AppSettings["Attachment"].ToString();
            string year = "", day = "", month = "";
            if (attachment_url.Contains("#Date#"))
            {
                year = DateTime.Now.Year.ToString();
                if (DateTime.Now.Month < 10)
                {
                    month = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    month = DateTime.Now.Month.ToString();
                }
                if (DateTime.Now.Day < 10)
                {
                    day = "0" + DateTime.Now.Day.ToString();
                }
                else
                {
                    day = DateTime.Now.Day.ToString();
                }
                filepath_url = attachment_url.Replace("#Date#", year + month + day);
            }
            else
            {
                filepath_url = attachment_url;
            }
            try
            {
                
                SmtpClient smtpclient = new SmtpClient(mailVo.Host, mailVo.Port);
                NetworkCredential networkCredential = new NetworkCredential(mailVo.UserName, mailVo.Password);
                smtpclient.Credentials = networkCredential;
                MailMessage mailMessage = new MailMessage();
                MailAddress mailFrom = new MailAddress(mailVo.From);
                Attachment attachment=new Attachment(filepath_url);
                mailMessage.From = mailFrom;
                foreach (string strTo in mailVo.To)
                {
                    if(strTo!="")
                        mailMessage.To.Add(strTo);
                }
                foreach (string strCc in mailVo.Cc)
                {
                    if (strCc != "")
                        mailMessage.CC.Add(strCc);
                }
                foreach (string strBcc in mailVo.Bcc)
                {
                    if (strBcc != "")
                        mailMessage.Bcc.Add(strBcc);
                }
                mailMessage.Attachments.Add(attachment);
                mailMessage.Subject = mailVo.Subject;
                mailMessage.Body = mailVo.Message;
                smtpclient.Send(mailMessage);
                result = true;
            }
            catch (Exception ex)
            {
                String exMsg = ex.Message.ToString();
                WriteToFile(filepath, ex.ToString());
                
            }

            return result;
        }
        public bool SendBulkMail(MailVo mailVo)
        {
            bool result = false;
            string filepath = ConfigurationSettings.AppSettings["logPath"].ToString();
            string filepath_url = "";
            string attachment_url = ConfigurationSettings.AppSettings["Attachment"].ToString();
            string year = "", day = "", month = "";
            if (attachment_url.Contains("#Date#"))
            {
                year = DateTime.Now.Year.ToString();
                if (DateTime.Now.Month < 10)
                {
                    month = "0" + DateTime.Now.Month.ToString();
                }
                else
                {
                    month = DateTime.Now.Month.ToString();
                }
                if (DateTime.Now.Day < 10)
                {
                    day = "0" + DateTime.Now.Day.ToString();
                }
                else
                {
                    day = DateTime.Now.Day.ToString();
                }
                filepath_url = attachment_url.Replace("#Date#", year + month + day);
            }
            else
            {
                filepath_url = attachment_url;
            }
            
            
            try
            {

                SmtpClient smtpclient = new SmtpClient(mailVo.Host, mailVo.Port);
                NetworkCredential networkCredential = new NetworkCredential(mailVo.UserName, mailVo.Password);
                smtpclient.Credentials = networkCredential;
                MailMessage mailMessage = new MailMessage();
                MailAddress mailFrom = new MailAddress(mailVo.From);
                Attachment attachment = new Attachment(filepath_url);
                mailMessage.From = mailFrom;
                mailMessage.Attachments.Add(attachment);
                foreach (string strCc in mailVo.Cc)
                {
                    if (strCc != "")
                        mailMessage.CC.Add(strCc);
                }
                foreach (string strBcc in mailVo.Bcc)
                {
                    if (strBcc != "")
                        mailMessage.Bcc.Add(strBcc);
                }
                foreach (string strTo in mailVo.To)
                {
                    if (strTo != "")
                    {
                       
                        mailMessage.To.Add(strTo);
                        mailMessage.Subject = mailVo.Subject;
                        mailMessage.Body = mailVo.Message;
                        smtpclient.Send(mailMessage);
                        mailMessage.To.Clear();
                    }

                }
                
                
                result = true;
            }
            catch (Exception ex)
            {
                String exMsg = ex.Message.ToString();
                WriteToFile(filepath, ex.ToString());

            }

            return result;
        }
        private void WriteToFile(string m_LogName, string Message)
        {
            try
            {
                using (FileStream fs = File.Open(m_LogName,
                            FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {

                        sw.WriteLine(DateTime.Now.ToLongDateString()+"  "+ DateTime.Now.ToLongTimeString()+ " "  + Message.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
