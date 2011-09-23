using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data.SqlClient;

namespace DaoUploads
{
    public class RejectedTransactionsDao
    {
        public DataSet GetRejectedRecords(int adviserId)
        {
            Database db;
            DbCommand getRejectedTransactionsCmd;
            DataSet getRejectedTransactionsDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRejectedTransactionsCmd = db.GetStoredProcCommand("SP_GetRejectedTransactions", adviserId);
                getRejectedTransactionsDs = db.ExecuteDataSet(getRejectedTransactionsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:GetRejectedRecords()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getRejectedTransactionsDs;
        }

        public bool MapFolioToCustomer(int MFTransactionStagingId, int customerId, int userId)
        {
            Database db;
            DbCommand cmd;
            int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_MapFolioToCustomer");
                db.AddInParameter(cmd, "@MFTransactionStagingId", DbType.Int32, MFTransactionStagingId);
                db.AddInParameter(cmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmd, "@CurrentUser", DbType.Int32, userId);
                affectedRecords = db.ExecuteNonQuery(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedTransactionsDao.cs:GetRejectedRecord()");

                object[] objects = new object[3];
                objects[0] = MFTransactionStagingId;
                objects[1] = customerId;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (affectedRecords > 0)
                return true;
            else
                return false;
        }

        public bool MapRejectedFoliosToCustomer(int mfFolioStagingId, int customerId, int userId)
        {
            Database db;
            DbCommand cmd;
            int affectedMFRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_MapRejectedMFFolioToCustomer");
                db.AddInParameter(cmd, "@MFFolioStagingId", DbType.Int32, mfFolioStagingId);
                db.AddInParameter(cmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmd, "@CurrentUser", DbType.Int32, userId);
                affectedMFRecords = db.ExecuteNonQuery(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedTransactionsDao.cs:GetRejectedRecord()");

                object[] objects = new object[3];
                objects[0] = mfFolioStagingId;
                objects[1] = customerId;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (affectedMFRecords > 0)
                return true;
            else
                return false;
        }

        public bool MapEquityToCustomer(int transactionStagingId, int customerId, int userId)
        {
            Database db;
            DbCommand cmd;
            int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_MapEquityToCustomer");
                db.AddInParameter(cmd, "@TransactionStagingId", DbType.Int32, transactionStagingId);
                db.AddInParameter(cmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmd, "@CurrentUser", DbType.Int32, userId);
                affectedRecords = db.ExecuteNonQuery(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedTransactionsDao.cs:GetRejectedRecord()");

                object[] objects = new object[3];
                objects[0] = transactionStagingId;
                objects[1] = customerId;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (affectedRecords > 0)
                return true;
            else
                return false;
        }
    }
}
