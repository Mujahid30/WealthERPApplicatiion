using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using VoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace DaoWerpAdmin
{
    public class UploadRejectsDao
    {
        public int MoveRejectedRecordsFromTemp(int processId, UploadType uploadType, AssetGroupType assetGroupType, int createdBy)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_ProcessPMTUploadReject");

                db.AddInParameter(updateCmd, "@ProcessId", DbType.Int32, processId);
                db.AddInParameter(updateCmd, "@UploadType", DbType.String, uploadType.ToString());
                db.AddInParameter(updateCmd, "@AssetGroup", DbType.String, assetGroupType.ToString());
                db.AddInParameter(updateCmd, "@CreatedBy", DbType.Int32, createdBy);
                db.AddOutParameter(updateCmd, "@TotalRejects", DbType.Int32, 10);

                db.ExecuteNonQuery(updateCmd);
                affectedRows = Convert.ToInt32(db.GetParameterValue(updateCmd, "@TotalRejects"));

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("MoveRejectedRecordsFromTemp", "UploadRejectsDao.cs:MoveRejectedRecordsFromTemp()");

                object[] objects = new object[4];
                objects[0] = processId;
                objects[1] = uploadType;
                objects[2] = assetGroupType;
                objects[3] = createdBy;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return affectedRows;
        }

        public DataSet GetRejectedRecords(int processId, int page, out int count, UploadType uploadType, AssetGroupType assetGroupType)
        {
            Database db;
            DbCommand cmd;
            DataSet dsRejectedRecords = null;
            count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetPMTPriceRejects");

                db.AddInParameter(cmd, "@ProcessId", DbType.Int32, processId);
                db.AddInParameter(cmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(cmd, "@AssetGroup", DbType.String, assetGroupType);

                db.AddInParameter(cmd, "@CurrentPage", DbType.Int32, page);
                db.AddOutParameter(cmd, "@Count", DbType.Int32, 10);

                dsRejectedRecords = db.ExecuteDataSet(cmd);
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

                FunctionInfo.Add("GetRejectedRecords()", "UploadRejectsDao.cs:GetRejectedRecords()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = processId;
                objects[2] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsRejectedRecords;


        }

        public bool Reprocess(int processId, UploadType uploadType, AssetGroupType assetGroupType, int currentUser, out int updatedSnapshots, out int updatedHistory)
        {

            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            updatedSnapshots = 0;
            updatedHistory = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_ReProcessPMTUploadRejects");
                db.AddInParameter(updateCmd, "@ProcessId", DbType.Int32, processId);
                db.AddInParameter(updateCmd, "@UploadType", DbType.String, uploadType);
                db.AddInParameter(updateCmd, "@AssetGroup", DbType.String, assetGroupType);
                db.AddInParameter(updateCmd, "@CurrentUser", DbType.Int32, currentUser);

                db.AddOutParameter(updateCmd, "@TotalSnapshotsUpdated", DbType.Int32, 10);
                db.AddOutParameter(updateCmd, "@TotalHistoryUpdated", DbType.Int32, 10);

                affectedRows = db.ExecuteNonQuery(updateCmd);
                Object objUpdatedSnapshots = db.GetParameterValue(updateCmd, "@TotalSnapshotsUpdated");
                if (objUpdatedSnapshots != DBNull.Value)
                    updatedSnapshots = Convert.ToInt32(objUpdatedSnapshots);
                
                Object  objUpdatedHistory = db.GetParameterValue(updateCmd, "@TotalHistoryUpdated");
                if (objUpdatedHistory != DBNull.Value)
                    updatedHistory = Convert.ToInt32(objUpdatedHistory);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Reprocess()", "UploadRejectsDao.cs:Reprocess()");

                object[] objects = new object[4];
                objects[0] = processId;
                objects[1] = uploadType;
                objects[2] = assetGroupType;
                objects[3] = currentUser;

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
    }
}
