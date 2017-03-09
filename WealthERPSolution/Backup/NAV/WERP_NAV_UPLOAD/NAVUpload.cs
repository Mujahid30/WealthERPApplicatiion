using System;
using System.Data;
using System.Data.SqlClient;
using NLog;
using WERP_COMMON;
using System.Configuration;

namespace WERP_NAV_UPLOAD
{
    public class NAVUpload
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public bool CopyDataToWERPTempTable()
        {

            bool isSuccess = false;
            logger.Debug("Copying data from AF_ProductMaster to WealthERP temp table.");

            DataTable sourceData = new DataTable();

            // get the source data
            DataTable dtNAV = new DataTable();
            using (SqlConnection sourceConnection = DBAccess.GetAFOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("usp_DAILY_NAV_GetAll", sourceConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                SqlDataReader sdr = cmd.ExecuteReader();
                dtNAV.Load(sdr);
                sdr.Close();
            }

            using (SqlConnection destinationConnection = DBAccess.GetWERPOpenConnection())
            {
                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(destinationConnection))
                {
                    bulkcopy.DestinationTableName = "ProductAMCAFDownloadTemp";
                    try
                    {
                        //The previous data in DownloadTemp is not important. So no transaction.
                        SqlCommand cmdTruncate = new SqlCommand("TRUNCATE TABLE ProductAMCAFDownloadTemp", destinationConnection);
                        cmdTruncate.ExecuteNonQuery();
                        
                        bulkcopy.WriteToServer(dtNAV);
                        isSuccess = true;
                        logger.Debug("Data copied.");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //We can remove the finally below since we use using()
                    finally
                    {
                        if (destinationConnection.State == ConnectionState.Open)
                            destinationConnection.Close();
                    }
                }
            }
            return isSuccess;


        }

        public void UpdateWERPSchemeCode()
        {

            using (SqlConnection sqlConnection = DBAccess.GetWERPOpenConnection())
            {
                try
                {
                    //The previous data in DownloadTemp is not important. So no transaction.
                    SqlCommand cmd = new SqlCommand("SP_UpdateTempAFTableWithWERPCode", sqlConnection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logger.Fatal("Error occurred while updating WERP Scheme code : " + ex.ToString());
                    Lib.ExitApplication();
                }

            }

        }

        public int MoveRejectedRecords(int logId)
        {
            int totalRejectedRecords = 0;
            using (
                SqlConnection sqlConnection = DBAccess.GetWERPOpenConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_NAVUploadRejectMove", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@logId", logId));


                    SqlParameter prmTotalRejects = new SqlParameter("@TotalRejects", SqlDbType.Int);
                    prmTotalRejects.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(prmTotalRejects);


                    cmd.ExecuteNonQuery();
                    totalRejectedRecords = (int)prmTotalRejects.Value;

                    //*** Notify the administrator about new SCHEME  **// 
                    if (totalRejectedRecords > 0)
                    {

                        if (ConfigurationSettings.AppSettings["NotifyOnRejection"] != null && ConfigurationSettings.AppSettings["NotifyOnRejection"] == "1")
                            logger.Fatal(totalRejectedRecords + " Scheme(s) rejected. Please check the Rejected records table.");
                        else
                            logger.Error(totalRejectedRecords + " Scheme(s) rejected. Please check the Rejected records table.");
                    }
                }
                catch (Exception ex)
                {
                    logger.Fatal("Error occurred while moving rejected records : " + ex.ToString());
                    Lib.ExitApplication();
                }

            }
            return totalRejectedRecords;

        }

        public int UpdateSnapshot()
        {
            int updatedRecords = 0;
            using (SqlConnection sqlConnection = DBAccess.GetWERPOpenConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_UpdateAMCAFSchemePlanSnapshot", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter prmTotalUpdates = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    prmTotalUpdates.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(prmTotalUpdates);

                    cmd.ExecuteNonQuery();
                    updatedRecords = (int)prmTotalUpdates.Value;
                    logger.Debug("Snapshot updated successfully. Total updated schemes : " + updatedRecords);
                    
                }
                catch (Exception ex)
                {
                    logger.Fatal("An error occurred while updating snapshot" + ex.ToString());
                }
            }
            return updatedRecords;
        }
        
        //public bool UpdateHistoryTable()
        //{
        //    //   int updatedRecords = 0;
        //    using (SqlConnection sqlConnection = DBAccess.GetWERPOpenConnection())
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("SP_UpdateAMCHistory", sqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //            logger.Debug("Histroy table updated successfully.");
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Fatal("An error occurred while updating snapshot" + ex.ToString());
        //        }
        //    }
        //    return false;
        //    // return updatedRecords;
        //}

    }
}
