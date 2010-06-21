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
using System.Web;
using BoCommon;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace AmpsysDaemon
{
    class Utils
    {
        private static string _SMTPServer = ConfigurationSettings.AppSettings["SMTPServer"];
        private static int _SMTPPort = int.Parse(ConfigurationSettings.AppSettings["SMTPPort"]);
        private static string _SMTPUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPUsername"]);
        private static string _SMTPPassword = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMTPPassword"]);
        private static string _SMTPFromDisplay = ConfigurationSettings.AppSettings["SMTPFromDisplay"];
        private static string _SMTPFrom = ConfigurationSettings.AppSettings["SMTPFrom"];
        private static string _SMSURL = ConfigurationSettings.AppSettings["SMSURL"];
        private static string _SMSUsername = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMSUsername"]);
        private static string _SMSPassword = Encryption.Decrypt(ConfigurationSettings.AppSettings["SMSPassword"]);

        public static DataSet ExecuteDataSet(string CommandText, SqlParameter[] Params)
        {
            Database D = DatabaseFactory.CreateDatabase("DBConnectionString");
            DbCommand DC = D.GetStoredProcCommand(CommandText);

            foreach (SqlParameter Param in Params)
            {
                D.AddInParameter(DC, Param.ParameterName, Param.DbType, Param.Value);
            }
            DataSet DS = D.ExecuteDataSet(DC);

            return DS;
        }

        public static void ExecuteNonQuery(string CommandText, SqlParameter[] Params)
        {
            Database D = DatabaseFactory.CreateDatabase("DBConnectionString");
            DbCommand DC = D.GetStoredProcCommand(CommandText);

            foreach (SqlParameter Param in Params)
            {
                D.AddInParameter(DC, Param.ParameterName, Param.DbType, Param.Value);
            }
            D.ExecuteNonQuery(DC);
        }

        public static void LogError(string Msg)
        {
            Console.WriteLine(DateTime.Now.ToString() + ": " + Msg);
        }

        public static void Trace(string Msg)
        {
            Console.WriteLine(DateTime.Now.ToString() + ": " + Msg);
        }

        public static void SendMail(string To, string Cc, string Bcc, string Subject, string Body, ArrayList Attachments)
        {
            MailMessage MM = new MailMessage(_SMTPFromDisplay, To);
            MM.Sender = new MailAddress(_SMTPFrom);
            MM.ReplyTo = new MailAddress(_SMTPFrom);
            if (Cc != null && Cc.Trim().Length > 0)
                MM.CC.Add(Cc);
            if (Bcc != null && Bcc.Trim().Length > 0)
                MM.Bcc.Add(Bcc);
            MM.Subject = Subject;
            MM.Body = Body;
            MM.IsBodyHtml = (Body.IndexOf("<html>") == -1)? false: true;

            foreach (string Attachment in Attachments)
            {
                Attachment MA = new Attachment(Attachment);
                MM.Attachments.Add(MA);
            }

            SmtpClient SC = new SmtpClient(_SMTPServer, _SMTPPort);
            NetworkCredential NC = new NetworkCredential();
            NC.UserName = _SMTPUsername;
            NC.Password = _SMTPPassword;
            //SC.EnableSsl = true;
            SC.UseDefaultCredentials = false;
            SC.Credentials = NC;

            SC.Timeout = 100000 * 10;

            SC.Send(MM);
        }

        public static void SendSMS(string Number, string Message)
        {
            if (Message.Length > 150)
                Message = Message.Substring(0, 150);

            string URL = _SMSURL.Replace("#NUMBER#", Number);
            URL = URL.Replace("#MESSAGE#", HttpUtility.UrlEncode(Message));
            URL = URL.Replace("#USERNAME#", _SMSUsername);
            URL = URL.Replace("#PASSWORD#", _SMSPassword);
            string Response = GetURL(URL);

            if (Response.IndexOf("success") == -1)
                throw new Exception("SendSMS failed: " + Response);
        }

        public static string GetURL(string URL)
        {
            string Response = "";
            WebRequest WR = (WebRequest)HttpWebRequest.Create(URL);
            Response = new StreamReader(WR.GetResponse().GetResponseStream()).ReadToEnd();

            return Response;
        }
    }
}

