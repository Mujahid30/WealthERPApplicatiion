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
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement.CustomPublisher;
using System.Data.Common;

namespace AmpsysJobDaemon
{
    class Utils
    {
        private static string _AccordDBConnectionString = ConfigurationManager.ConnectionStrings["AccordDBConnectionString"].ToString();
        private static string _TraceFilePath = Application.StartupPath + @"\Logs\JobDaemon_" + String.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) + ".txt";
        private static StreamWriter _TraceFile = new StreamWriter(_TraceFilePath);
        private static string _DataStorePath = ConfigurationSettings.AppSettings["DataStore"];

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

        public static DataSet ExecuteDataSetAccord(string CommandText, SqlParameter[] Params)
        {
            Database D = DatabaseFactory.CreateDatabase("AccordDBConnectionString");
            DbCommand DC = D.GetStoredProcCommand(CommandText);

            foreach (SqlParameter Param in Params)
            {
                D.AddInParameter(DC, Param.ParameterName, Param.DbType, Param.Value);
            }
            DataSet DS = D.ExecuteDataSet(DC);

            return DS;
        }

        public static void ExecuteNonQueryAccord(string CommandText, SqlParameter[] Params)
        {
            Database D = DatabaseFactory.CreateDatabase("AccordDBConnectionString");
            DbCommand DC = D.GetStoredProcCommand(CommandText);

            foreach (SqlParameter Param in Params)
            {
                D.AddInParameter(DC, Param.ParameterName, Param.DbType, Param.Value);
            }
            D.ExecuteNonQuery(DC);
        }

        public static void ExecuteNonQueryAccord(string CommandText)
        {
            Database D = DatabaseFactory.CreateDatabase("AccordDBConnectionString");
            DbCommand DC = D.GetSqlStringCommand(CommandText);

            D.ExecuteNonQuery(DC);
        }

        public static string GetAccordConnectionString()
        {
            return _AccordDBConnectionString;
        }

        public static void LogError(string Msg)
        {
            Console.WriteLine(DateTime.Now.ToString() + ": " + Msg);
            _TraceFile.WriteLine(DateTime.Now.ToString() + ": " + Msg);
            EmailExceptionPublisher emailExPublisher = new EmailExceptionPublisher();
            emailExPublisher.SendMail("admin@wealtherp.com", "ssujith@ampsys.in", "schaluvadi@ampsys.in", "", "Exception at Job Processor", Msg);

        }

        public static void Trace(string Msg)
        {
            Console.WriteLine(DateTime.Now.ToString() + ": " + Msg);
            _TraceFile.WriteLine(DateTime.Now.ToString() + ": " + Msg);
        }

        public static string GetDataStore()
        {
            return _DataStorePath;
        }
    }
}

