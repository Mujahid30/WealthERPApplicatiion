using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using VoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoWerpAdmin
{
    public class ProductPriceDownloadLogDao
    {
        /// <summary>
        /// Creates a new entry in  WerpAdminUploadProcessLog table
        /// </summary>
        /// <returns>returns process Id</returns>
            public int CreateProcessLog(AdminDownloadProcessLogVo processLog)
            {
                Database db;
                DbCommand updateCmd;
                int processId =0;	
                try
                {
                    db = DatabaseFactory.CreateDatabase("wealtherp");
                    updateCmd = db.GetStoredProcCommand("SP_InsertWerpAdminDownloadProcessLog");
                    db.AddInParameter(updateCmd, "@WADPL_AssetClass", DbType.String, processLog.AssetClass);
                    db.AddInParameter(updateCmd, "@WADPL_CreatedBy", DbType.Int32, processLog.CreatedBy);
                    db.AddInParameter(updateCmd, "@WADPL_StartTime", DbType.DateTime, processLog.StartTime);
                    db.AddOutParameter(updateCmd, "@ProcessId", DbType.Int32, 10);
                    

                    db.ExecuteNonQuery(updateCmd);
                    processId = int.Parse(db.GetParameterValue(updateCmd, "@ProcessId").ToString());
                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (Exception Ex)
                {
                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();

                    FunctionInfo.Add("CreateProcessLog", "ProductPriceDownloadLogDao.cs:CreateProcessLog()");

                    object[] objects = new object[1];
                    objects[0] = processLog;

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
            public bool UpdateProcessLog(AdminDownloadProcessLogVo processLog)
            {
                Database db;
                DbCommand updateCmd;
                int affectedRows =0;	
                try
                {
                    db = DatabaseFactory.CreateDatabase("wealtherp");
                    updateCmd = db.GetStoredProcCommand("SP_UpdateWerpAdminDownloadProcessLog");
                    db.AddInParameter(updateCmd, "@WADPL_ProcessID", DbType.Int32, processLog.ProcessID);
                    db.AddInParameter(updateCmd, "@WADPL_AssetClass", DbType.String, processLog.AssetClass);
                    db.AddInParameter(updateCmd, "@WADPL_SourceName", DbType.String, processLog.SourceName);
                    db.AddInParameter(updateCmd, "@WADPL_StartTime", DbType.DateTime, processLog.StartTime);
                    db.AddInParameter(updateCmd, "@WADPL_EndTime", DbType.DateTime, processLog.EndTime);
                    db.AddInParameter(updateCmd, "@WADPL_ForDate", DbType.DateTime, processLog.ForDate);
                    db.AddInParameter(updateCmd, "@WADPL_NoOfRecordsDownloaded", DbType.Int32, processLog.NoOfRecordsDownloaded);
                    db.AddInParameter(updateCmd, "@WADPL_IsConnectionToSiteEstablished", DbType.Int16, processLog.IsConnectionToSiteEstablished);
                    db.AddInParameter(updateCmd, "@WADPL_IsFileDownloaded", DbType.Int16, processLog.IsFileDownloaded);
                    db.AddInParameter(updateCmd, "@WADPL_IsConversiontoXMLComplete", DbType.Int16, processLog.IsConversiontoXMLComplete);
                    db.AddInParameter(updateCmd, "@WADPL_IsInsertiontoDBdone", DbType.Int16, processLog.IsInsertiontoDBdone);
                    db.AddInParameter(updateCmd, "@WADPL_NoOfRecordsInserted", DbType.Int32, processLog.NoOfRecordsInserted);
                    db.AddInParameter(updateCmd, "@WADPL_XMLFileName", DbType.String, processLog.XMLFileName);
                    db.AddInParameter(updateCmd, "@WADPL_Description", DbType.String, processLog.Description);
                    db.AddInParameter(updateCmd, "@WADPL_ModifiedBy", DbType.Int32, processLog.ModifiedBy);
                    db.AddInParameter(updateCmd, "@WADPL_ModifiedOn", DbType.DateTime, processLog.ModifiedOn);

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

                    FunctionInfo.Add("UpdateProcessLog", "ProductPriceDownloadLogDao.cs:UpdateProcessLog()");

                    object[] objects = new object[1];
                    objects[0] = processLog;

                    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);
                    throw exBase;
                }


            if(affectedRows > 0)
            return true;
            else 
            return false;





            }

            public DataSet GetProcessLog(int CurrentPage, out int Count)
            {
                Database db;
                DbCommand cmd;
                DataSet ds = null;
                try
                {
                    db = DatabaseFactory.CreateDatabase("wealtherp");
                    cmd = db.GetStoredProcCommand("SP_GetWerpAdminDownloadProcessLog");
                    db.AddInParameter(cmd, "@currentPage", DbType.Int32, CurrentPage);
                    ds = db.ExecuteDataSet(cmd);

                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (Exception Ex)
                {
                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();

                    FunctionInfo.Add("GetProcessLog", "ProductPriceDownloadLogDao.cs:GetProcessLog()");

                    //object[] objects = new object[1];
                    //objects[0] = value;

                    FunctionInfo = exBase.AddObject(FunctionInfo, null);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);
                    throw exBase;
                }
                Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());
                return ds;


            }
    }
}
