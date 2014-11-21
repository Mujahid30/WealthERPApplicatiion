using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoOfflineOrderManagement
{
    public class OfflineMFOrderBackOfficeDao
    {
        public DataTable GetStatusCode()
        {
            DataSet dsGetStatusCode;
            DataTable dtGetStatusCode;
            Database db;
            DbCommand GetStatusCodeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetStatusCodeCmd = db.GetStoredProcCommand("SPROC_OfflineGetOrderStatus");
                dsGetStatusCode = db.ExecuteDataSet(GetStatusCodeCmd);
                dtGetStatusCode = dsGetStatusCode.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetStatusCode;
        }
        public DataSet GetOfflineSIPSummaryBookMIS(int AmcCode, DateTime dtFrom, DateTime dtTo, int adviserId)
        {
            DataSet dsGetOfflineSIPSummaryBookMIS;
            Database db;
            DbCommand GetGetOfflineSIPSummaryBookMIS;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetOfflineSIPSummaryBookMIS = db.GetStoredProcCommand("SPROC_OfflineSIPOrderBook");
                if (AmcCode != 0)
                    db.AddInParameter(GetGetOfflineSIPSummaryBookMIS, "@AMC", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(GetGetOfflineSIPSummaryBookMIS, "@AMC", DbType.Int32, 0);

                db.AddInParameter(GetGetOfflineSIPSummaryBookMIS, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetGetOfflineSIPSummaryBookMIS, "@ToDate", DbType.DateTime, dtTo);
                db.AddInParameter(GetGetOfflineSIPSummaryBookMIS, "@adviseId", DbType.Int32, adviserId);

                GetGetOfflineSIPSummaryBookMIS.CommandTimeout = 60 * 60;
                dsGetOfflineSIPSummaryBookMIS = db.ExecuteDataSet(GetGetOfflineSIPSummaryBookMIS);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetOfflineSIPSummaryBookMIS;
        }
    }
}
