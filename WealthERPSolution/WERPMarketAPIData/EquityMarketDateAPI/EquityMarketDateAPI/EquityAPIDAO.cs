using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Configuration;
namespace EquityMarketDateAPI
{
   public class EquityAPIDAO
    {
       public int CreateUpdateEquityMarketData(DataTable dt)
       {
           int result = 0;
           {
               string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
               SqlConnection sqlCon = new SqlConnection(connectionString);
               sqlCon.Open();
               SqlCommand cmdProc = new SqlCommand("SPROC_EquityMarketData", sqlCon);
               cmdProc.CommandType = CommandType.StoredProcedure;
               cmdProc.Parameters.AddWithValue("@EquityMarketData", dt);
               result = cmdProc.ExecuteNonQuery();
           }
           return result;
       }
       public int CreateUpdateEquityMasterData(DataTable dt)
       {
           int result = 0;
           {
               string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
               SqlConnection sqlCon = new SqlConnection(connectionString);
               sqlCon.Open();
               SqlCommand cmdProc = new SqlCommand("SPROC_CreateUpdateEquityMaster", sqlCon);
               cmdProc.CommandType = CommandType.StoredProcedure;
               cmdProc.Parameters.AddWithValue("@EquityMaster", dt);
               result = cmdProc.ExecuteNonQuery();
           }
           return result;
       }
    }
}
