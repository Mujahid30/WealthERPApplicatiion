using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Data;
using VoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;


namespace DaoWerpAdmin
{
    public class ProductPriceUploadLogDao
    {
        /// <summary>
        /// Creates a new entry in  WerpAdminUploadProcessLog table
        /// </summary>
        /// <returns>returns process Id</returns>
        public int CreateProcessLog(AdminUploadProcessLogVo processLog)
        {
            Database db;
            DbCommand updateCmd;
            int processId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_InsertWerpAdminUploadProcessLog");
                db.AddInParameter(updateCmd, "@WAUPL_AssetClass", DbType.String, processLog.AssetClass);
                db.AddInParameter(updateCmd, "@WAUPL_CreatedBy", DbType.Int32, processLog.CreatedBy);
                db.AddOutParameter(updateCmd, "@ProcessId", DbType.Int32, processLog.ProcessId);

                db.ExecuteNonQuery(updateCmd);
                processId = Convert.ToInt32(db.GetParameterValue(updateCmd, "@ProcessId"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("CreateProcessLog()", "ProductPriceUploadLogDao.cs:CreateProcessLog()");

                object[] objects = new object[3];
                objects[0] = processLog.AssetClass;
                objects[1] = processLog.CreatedBy;
                objects[2] = processLog.ProcessId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return processId;
        }
        /// <summary>
        /// Update the status of a particular row in WerpAdminUploadProcessLog
        /// </summary>
        /// <returns>true/false to denote if the update was sucess</returns>
        public  bool UpdateProcessLog(AdminUploadProcessLogVo processLog)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_UpdateWerpAdminUploadProcessLog");
                db.AddInParameter(updateCmd, "@WAUPL_ProcessId", DbType.Int32, processLog.ProcessId);
                if (processLog.EndTime != DateTime.MinValue)
                db.AddInParameter(updateCmd, "@WAUPL_EndTime", DbType.DateTime, processLog.EndTime);
                if (processLog.IsXMLCreated)
                    db.AddInParameter(updateCmd, "@WAUPL_IsXmlCreated", DbType.Single, 1);
                if (processLog.IsInsertedToSnapshot)
                    db.AddInParameter(updateCmd, "@WAUPL_IsSnapshotUpdated", DbType.Single, 1);
                if (processLog.IsInsertedToHistory)
                    db.AddInParameter(updateCmd, "@WAUPL_IsHistoryUpdated", DbType.Single, 1);
                db.AddInParameter(updateCmd, "@WAUPL_ModifiedBy", DbType.Int32, processLog.ModifiedBy);

                if (processLog.NoOfRecordsRejected > 0)
                    db.AddInParameter(updateCmd, "@WAUPL_NoOfRecordsRejected", DbType.Int32, processLog.NoOfRecordsRejected);
                if (processLog.NoOfSnapshotsUpdated >0)
                db.AddInParameter(updateCmd, "@WAUPL_NoOfSnapshotsUpdated", DbType.Int32, processLog.NoOfSnapshotsUpdated);
                if (processLog.NoOfRecordsToHistory > 0)
                db.AddInParameter(updateCmd, "@WAUPL_NoOfRecordsToHistory", DbType.Int32, processLog.NoOfRecordsToHistory);


                affectedRows = db.ExecuteNonQuery(updateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("UpdateProcessLog()", "ProcessPriceUploadLogDao.cs:UpdateProcessLog()");

                object[] objects = new object[1];
                //objects[0] = value;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            if (affectedRows > 0)
                return true;
            else
                return false;



        }

        public DataSet GetProcessLog(int currentPage, out int count)
        {
            Database db;
            DataSet ds=null;
            DbCommand cmd;
            count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetPMTUploadProcessLog");
                db.AddInParameter(cmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddOutParameter(cmd, "@Count", DbType.Int32, 10);

                ds = db.ExecuteDataSet(cmd);

                Object objCount = db.GetParameterValue(cmd, "@Count");
                if (objCount != DBNull.Value)
                    count = Convert.ToInt32(objCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("GetProcessLog()", "ProductPriceUploadLogDao.cs:GetProcessLog()");

                object[] objects = new object[1];
                //objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;


        }
    }
}
