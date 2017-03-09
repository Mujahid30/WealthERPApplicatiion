using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DaoWerpAdmin
{
  public  class MaintenanceDao
    {

      public DataSet GetScripMasterRecords(int CurrentPage, string Flag)
        {

            DataSet ds;
            Database db;
            DbCommand Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
              
                Cmd = db.GetStoredProcCommand("SP_GetScripMasterRecords");
                db.AddInParameter(Cmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

      public int GetScripMasterRecords(string Flag)
        {

            DataSet ds;
            Database db;
            DbCommand Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                Cmd = db.GetStoredProcCommand("SP_GetScripMasterRecords");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

      public DataSet GetSchemeMasterRecord(int CurrentPage, string Flag)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetSchemeMasterRecords");
                db.AddInParameter(Cmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }

      public int GetSchemeMasterRecord(string Flag)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetSchemeMasterRecords");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }
    }
}
