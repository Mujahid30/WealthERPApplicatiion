using System;
using System.Data;
using System.Data.SqlClient;
using NLog;
using WERP_COMMON;
namespace WERP_NAVLIB
{
    public class DownloadLogVo
    {
        public int ID { set; get; }
        public DateTime Start_Date { set; get; }
        public DateTime End_Date { set; get; }

        public bool Is_Downloaded { set; get; }
        public bool Is_AF_Updated { set; get; }
        public bool Is_WERP_Snapshot_Updated { set; get; }

        public string Log_File { set; get; }
        public string Downloaded_Schemes { set; get; }
        public string Description { set; get; }

        public int UpdatedSchemesCount { set; get; }
        public int RejectedSchemesCount { set; get; }
        public string RejectedSchemes { set; get; }

    }

    public class DownloadLog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static int Add()
        {


            SqlConnection con = DBAccess.GetWERPOpenConnection();
            SqlCommand cmd = null;
            string logFile = "NAV_LOG--" + DateTime.Now.ToString("") + ".txt";
            int insertedLogId = 0;

            try
            {
                logger.Debug("Adding DownloadLog");
                cmd = new SqlCommand("usp_NAV_DOWNLOAD_LOGInsert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Start_Time", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@Log_File", SqlDbType.VarChar).Value = "NAV_LOG--" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                SqlParameter prmInsertId = new SqlParameter("@InsertedId", SqlDbType.Int);
                prmInsertId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(prmInsertId);
                cmd.ExecuteNonQuery();
                insertedLogId = Convert.ToInt32(prmInsertId.Value);
                logger.Debug("Inserted Download Log Id " + insertedLogId);
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while adding download log" + ex.ToString());
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            return insertedLogId;

        }

        public static bool Update(DownloadLogVo downloadLogVo)
        {

            SqlConnection con = DBAccess.GetWERPOpenConnection();
            SqlCommand cmd = null;

            string logFile = "NAV_LOG--" + DateTime.Now.ToString("") + ".txt";

            try
            {
                cmd = new SqlCommand("usp_NAV_DOWNLOAD_LOGUpdate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //cmd.Parameters.Add("@Start_Time", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add(new SqlParameter("@ID", downloadLogVo.ID));

                cmd.Parameters.Add(new SqlParameter("@End_Time", DateTime.Now));

                if (downloadLogVo.Is_Downloaded == true)
                    cmd.Parameters.Add(new SqlParameter("@Is_Downloaded", downloadLogVo.Is_Downloaded));

                if (downloadLogVo.Is_AF_Updated == true)
                    cmd.Parameters.Add(new SqlParameter("@Is_AF_Updated", downloadLogVo.Is_AF_Updated));

                if (downloadLogVo.Is_AF_Updated == true)
                    cmd.Parameters.Add(new SqlParameter("Is_WERP_Snapshot_Updated", downloadLogVo.Is_WERP_Snapshot_Updated));

                //if (downloadLogVo.Downloaded_Schemes != null)
                //    cmd.Parameters.Add(new SqlParameter("@Downloaded_Schemes", downloadLogVo.Downloaded_Schemes));

                if (downloadLogVo.Description != null)
                    cmd.Parameters.Add(new SqlParameter("@Description", downloadLogVo.Description));

               // cmd.Parameters.Add(new SqlParameter("@Rejected_Schemes", downloadLogVo.RejectedSchemes));

                cmd.Parameters.Add(new SqlParameter("@Updated_Schemes_Count", downloadLogVo.UpdatedSchemesCount));

                cmd.Parameters.Add(new SqlParameter("@Rejected_Schemes_Count", downloadLogVo.RejectedSchemesCount));


                int retValue = cmd.ExecuteNonQuery();
                if (retValue == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while updating download log." + ex.ToString());
            }
            return false;


        }

    }
}
