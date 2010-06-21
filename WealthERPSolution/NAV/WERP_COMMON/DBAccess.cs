using System;
using System.Configuration;
using System.Data.SqlClient;
using NLog;

namespace WERP_COMMON
{
   public class DBAccess
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static SqlConnection GetAFOpenConnection()
        {
            string connectionString = ConfigurationSettings.AppSettings["AFDBconnStr"];
            SqlConnection con = null;
            //string connectionString = "Server = 122.166.49.39; Database = AF_ProductMaster; User ID = sa; Password = pcg123#; Trusted_Connection = False;";
            try
            {
                // logger.Debug("Opening database connection.");
                con = new SqlConnection(connectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                logger.Fatal("Error occurred while connecting to AF_ProductMasterDB : " + ex.ToString());
                Lib.ExitApplication();
            }
            return con;
        }

        public static SqlConnection GetWERPOpenConnection()
        {
            string connectionString = ConfigurationSettings.AppSettings["WERPDBconnStr"];
            SqlConnection con = null;
            //string connectionString = "Server = 122.166.49.39; Database = AF_ProductMaster; User ID = sa; Password = pcg123#; Trusted_Connection = False;";
            try
            {
                // logger.Debug("Opening database connection.");
                con = new SqlConnection(connectionString);
                con.Open();
            }
            catch (Exception ex)
            {
                logger.Fatal("Error occurred while connecting to WealthERPDB : " + ex.ToString());
                Lib.ExitApplication();

            }
            return con;
        }
    }
}
