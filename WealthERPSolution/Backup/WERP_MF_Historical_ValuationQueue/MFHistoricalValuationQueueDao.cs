using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using VoValuation;

namespace WERP_MF_Historical_ValuationQueue
{
   public class MFHistoricalValuationQueueDao
    {

       public DataTable GetMFHistoricalValuationQueueDetails()
       {
           Database db;
           DbCommand getHistoricalValuationQueueDetailsCmd;
           DataSet dsHistoricalValuationQueueDetails;


               db = DatabaseFactory.CreateDatabase("wealtherp");
               getHistoricalValuationQueueDetailsCmd = db.GetStoredProcCommand("SPROC_GetMFHistoricalValuationQueueDetails");
               dsHistoricalValuationQueueDetails = db.ExecuteDataSet(getHistoricalValuationQueueDetailsCmd);
               
               return dsHistoricalValuationQueueDetails.Tables[0];

       }

       public void UpdateHistoricalValuationQueueFlag(int adviserId, DateTime valuationDate, int flag)
        {
            Database db;
            DbCommand updateHistoricalValuationFlagCmd;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateHistoricalValuationFlagCmd = db.GetStoredProcCommand("SPROC_UpdateHistoricalValuationQueueFlag");
                db.AddInParameter(updateHistoricalValuationFlagCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(updateHistoricalValuationFlagCmd, "@valuationDate", DbType.DateTime, valuationDate);
                db.AddInParameter(updateHistoricalValuationFlagCmd, "@Flag", DbType.Int16, flag);
                db.ExecuteNonQuery(updateHistoricalValuationFlagCmd);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }           
        }    
    }
}
