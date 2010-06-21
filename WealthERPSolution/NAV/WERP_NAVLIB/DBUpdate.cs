using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NLog;
using WERP_COMMON;

namespace WERP_NAVLIB
{
    class DBUpdate
    {

        //TODO: This method need to change to work as batch processing to improve performance
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public int DBupload(List<WerpMutualFund> mflist, DownloadLogVo downloadLogVo)
        {

            SqlCommand cmd = null;

            SqlConnection con = DBAccess.GetAFOpenConnection();
            string updatedSchemes = string.Empty;
            logger.Debug("Updating/Inserting data to DB");
            int totalUpdatedSchemes = 0;

            foreach (WerpMutualFund m in mflist)
            {
                try
                {

                    cmd = new SqlCommand("usp_DAILY_NAV_Upload", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@SCHEMECODE", SqlDbType.Float).Value = m.SCHEMECODE;

                    if (!String.IsNullOrEmpty(m.NAV_DATE))
                        cmd.Parameters.Add("@NAV_DATE", SqlDbType.DateTime).Value = DateTime.Parse(m.NAV_DATE);
                    if (!String.IsNullOrEmpty(m.NAVRS))
                        cmd.Parameters.Add("@NAVRS", SqlDbType.Float).Value = m.NAVRS; ;
                    if (!String.IsNullOrEmpty(m.REPURPRICE))
                        cmd.Parameters.Add("@REPURPRICE", SqlDbType.Float).Value = m.REPURPRICE; ;
                    if (!String.IsNullOrEmpty(m.SALEPRICE))
                        cmd.Parameters.Add("@SALEPRICE", SqlDbType.Float).Value = m.SALEPRICE; ;
                    if (m.CLDATE != string.Empty)
                        cmd.Parameters.Add("@CLDATE", SqlDbType.DateTime).Value = DateTime.Parse(m.CLDATE);//, "MM/dd/yyyy hh:mm:ss", culture, DateTimeStyles.NoCurrentDateDefault);// DateTime.Parse(m.CLDATE);
                    if (!String.IsNullOrEmpty(m.CHANGE))
                        cmd.Parameters.Add("@CHANGE", SqlDbType.Float).Value = m.CHANGE;

                    if (!String.IsNullOrEmpty(m.NETCHANGE))
                        cmd.Parameters.Add("@NETCHANGE", SqlDbType.Float).Value = m.NETCHANGE;

                    if (!String.IsNullOrEmpty(m.PREVNAV))
                        cmd.Parameters.Add("@PREVNAV", SqlDbType.Float).Value = m.PREVNAV;

                    if (m.PRENAVDATE != string.Empty)
                        cmd.Parameters.Add("@PRENAVDATE", SqlDbType.DateTime).Value = DateTime.Parse(m.PRENAVDATE);//, "MM/dd/yyyy hh:mm:ss", culture, DateTimeStyles.NoCurrentDateDefault);// DateTime.Parse(m.PRENAVDATE);

                    cmd.Parameters.Add("@UPD_FLAG", SqlDbType.VarChar).Value = m.UPD_FLAG; ;
                    cmd.ExecuteNonQuery();
                    updatedSchemes += m.SCHEMECODE + ",";
                    totalUpdatedSchemes++;

                }
                catch (Exception ex)
                {
                   // downloadLogVo.Downloaded_Schemes += m.SCHEMECODE;
                    logger.Error("An error occurred while updating the scheme details for scheme : " + m.SCHEMECODE + ": " + ex.ToString());

                }

            }
            // logger.Debug("Updated Schemes :" + updatedSchemes);

            if (!String.IsNullOrEmpty(downloadLogVo.RejectedSchemes))
                logger.Debug("Rejected Schemes : " + downloadLogVo.RejectedSchemes);
            downloadLogVo.Is_AF_Updated = true;

            logger.Debug("The following schemes Inserted/Updated in AF table: " +updatedSchemes.ToString());
           // downloadLogVo.Downloaded_Schemes = updatedSchemes;
            
            DownloadLog.Update(downloadLogVo);
            
            if (con != null && con.State == ConnectionState.Open)
            {
                try
                {
                   // logger.Debug("Closing DB connection");
                    con.Close();
                }
                catch (Exception ex)
                {
                    logger.Fatal("Error occurred while closing connection");
                }
            }
            return totalUpdatedSchemes;

        }

    }
}
