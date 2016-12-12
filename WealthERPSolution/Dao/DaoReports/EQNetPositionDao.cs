using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace DaoReports
{
   public class EQNetPositionDao
    {
   
      public DataSet GetCustomerEQTransactions(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand cmd = db.GetStoredProcCommand("SPROC_CustomerEQNetTransactionToProcess");
            db.AddInParameter(cmd, "@C_CustomerId", DbType.Int32, C_CustomerId);
            db.AddInParameter(cmd, "@CP_PortfolioIds", DbType.String, PortfolioIds);
            if (FromDate != DateTime.MinValue)
                db.AddInParameter(cmd, "@FromDate", DbType.Date, FromDate);
            else
                db.AddInParameter(cmd, "@FromDate", DbType.Date, DBNull.Value);
            db.AddInParameter(cmd, "@ToDate", DbType.Date, ToDate);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }
        public DataSet GetCustomerReportHeaders(int A_AdviserrId, int C_CutomerId)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand cmd = db.GetStoredProcCommand("SPROC_GETReportParameter");
            db.AddInParameter(cmd, "@A_AdviserId", DbType.Int32, A_AdviserrId);
            db.AddInParameter(cmd, "@C_C_CustomerId", DbType.Int32, C_CutomerId);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }
        public DataSet GetCustomerEQTraxnReport(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand cmd = db.GetStoredProcCommand("SPROC_RPT_CustomerEQTransaction");
            db.AddInParameter(cmd, "@C_CustomerId", DbType.Int32, C_CustomerId);
            db.AddInParameter(cmd, "@CP_PortfolioIds", DbType.String, PortfolioIds);
            if (FromDate != DateTime.MinValue)
                db.AddInParameter(cmd, "@FromDate", DbType.Date, FromDate);
            else
                db.AddInParameter(cmd, "@FromDate", DbType.Date, DBNull.Value);
            db.AddInParameter(cmd, "@ToDate", DbType.Date, ToDate);
            ds = db.ExecuteDataSet(cmd);
            return ds;
        }
}
}
