using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoWerpAdmin
{
    public class PriceDao
    {

        public DataSet GetEquityRecord(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistory");
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetEquityCount(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;
            
            {


                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistory");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }


        public DataSet GetEquitySnapshot(string Flag, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetEquityCountSnapshot(string Flag, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }


        public DataSet GetAMFISnapshot(string Flag, String Search, int CurrentPage)
        {

            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetAMFICountSnapshot(string Flag, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        public DataSet GetAMFIRecord(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {

            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistory");
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetAMFICount(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistory");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }

        
    }
}

