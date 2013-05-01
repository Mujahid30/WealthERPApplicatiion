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
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


namespace WERP_EMAIL_SMS_JOB
{
    public class Utils
    {
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

       

   

        public static DataTable GetSendEmailSMTPCount()
        {
            DataTable dtSendEmailSMTPCount = new DataTable();
            SqlParameter[] Params = new SqlParameter[0];
            DataSet dsSMTPDetails = ExecuteDataSet("SPOC_GetSMTPSendMailCount", Params);
            dtSendEmailSMTPCount = dsSMTPDetails.Tables[0];
            return dtSendEmailSMTPCount;
        }

      




        public static string SendSMS(Dictionary<string, string> SMSDetails)
        {
            string message = SMSDetails["MESSAGE"].ToString();
            string number = SMSDetails["NUMBER"].ToString();
            _SMSURL = SMSDetails["SMSAPI"].ToString();
            _SMSUsername = SMSDetails["USERNAME"].ToString();
            _SMSPassword = SMSDetails["PASSWORD"].ToString();

            if (message.Length > 150)
                message = message.Substring(0, 150);

            string URL = _SMSURL.Replace("#NUMBER#", number);
            // URL = URL.Replace("#MESSAGE#", HttpUtility.UrlEncode(Message));
            URL = URL.Replace("#MESSAGE#", message);
            URL = URL.Replace("#USERNAME#", _SMSUsername);
            URL = URL.Replace("#PASSWORD#", _SMSPassword);
            string Response = GetURL(URL);

            if (Response.IndexOf("Successfully") == -1)
            {
                throw new Exception("SMS-Send Failed: " + Response);

            }
            else
                return "SMS-Send Successfully:" + Response;
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
