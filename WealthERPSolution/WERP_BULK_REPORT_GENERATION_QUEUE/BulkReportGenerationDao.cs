using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


namespace WERP_BULK_REPORT_GENERATION_QUEUE
{
    public class BulkReportGenerationDao
    {
        /// <summary>
        /// Get The list of TOP 5 bulk report Request 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTheBulkReportRequestList(string daemonCode)
        {
            DataSet DS=null;
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@TaskId", 1);
                Params[0].DbType = DbType.Int32;
                Params[1] = new SqlParameter("@DaemonCode", daemonCode);
                Params[1].DbType = DbType.String;
                DS = Utils.ExecuteDataSet("SPROC_GetWERPRequestList", Params);
                //DS = Utils.ExecuteDataSet("SPROC_GetWERPRequestList_Test", Params);

            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkReportGenerationDao:GetTheBulkReportRequestList()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return DS.Tables[0];

        }

        public DataSet GetTheSubBulkReportRequestList(int parentRequestId, string daemonCode)
        {
            DataSet subRequestDS = null;
           
            try
            {
                
                Database DB = DatabaseFactory.CreateDatabase("wealtherp");
                DbCommand CMD = DB.GetStoredProcCommand("SPROC_GetWERPSubRequestWithParameterValues");
                //DbCommand CMD = DB.GetStoredProcCommand("SPROC_GetWERPSubRequestWithParameterValues_Test");
                DB.AddInParameter(CMD, "@TaskRequestId", DbType.Int32, parentRequestId);
                DB.AddInParameter(CMD, "@DaemonCode", DbType.String, daemonCode);
                //DB.AddOutParameter(CMD, "@RequestLogId", DbType.Int32, 1000000);
                subRequestDS = DB.ExecuteDataSet(CMD);                    
                //logId = int.Parse(DB.GetParameterValue(CMD, "RequestLogId").ToString());



            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkReportGenerationDao:GetTheSubBulkReportRequestList()");
                object[] objects = new object[2];
                objects[0] = parentRequestId;
                objects[1] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return subRequestDS;

        }

        public bool CreateTaskRequestLOG(int taskRequestId, out int logId)
        {
            logId = 0;
            bool isSuccess = false;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase("wealtherp");
                DbCommand CMD = DB.GetStoredProcCommand("SPROC_CreateTaskRequestLog");
                DB.AddInParameter(CMD, "@TaskRequestId", DbType.Int32, taskRequestId);
                DB.AddOutParameter(CMD, "@RequestLogId", DbType.Int32, 1000000);
                if (DB.ExecuteNonQuery(CMD) != 0)
                    isSuccess = true;
                logId = int.Parse(DB.GetParameterValue(CMD, "RequestLogId").ToString());

            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkReportGenerationDao:GetTheSubBulkReportRequestList()");
                object[] objects = new object[2];
                objects[0] = taskRequestId;
                objects[1] = logId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return isSuccess;

        }


        public void UpdateTaskRequestAndLOG(int taskRequestId, int logId, string message)
        {
          
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@RequestLogId", logId);
                Params[0].DbType = DbType.Int32;
                Params[1] = new SqlParameter("@RequestTaskId", taskRequestId);
                Params[1].DbType = DbType.Int32;
                Params[2] = new SqlParameter("@Message", message);
                Params[2].DbType = DbType.String;
                Utils.ExecuteNonQuery("SPROC_UpdateRequestLog", Params);
             

            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkReportGenerationDao:GetTheSubBulkReportRequestList()");
                object[] objects = new object[3];
                objects[0] = taskRequestId;
                objects[1] = logId;
                objects[2] = message;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

         

        }

        public void UpdateTaskRequestLOG(int logId, string message)
        {
            
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@RequestLogId", logId);
                Params[0].DbType = DbType.Int32;             
                Params[1] = new SqlParameter("@Message", message);
                Params[1].DbType = DbType.String;
                Utils.ExecuteNonQuery("SPROC_UpdateTaskRequestLog", Params);


            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkReportGenerationDao:GetTheSubBulkReportRequestList()");
                object[] objects = new object[2];
                objects[0] = logId;
                objects[1] = message;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }

        public void UpdateTaskRequestStatus(int requestId,int taskStatus)
        {

            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@RequestId", requestId);
                Params[0].DbType = DbType.Int32;
                Params[1] = new SqlParameter("@TaskStatus", taskStatus);
                Params[1].DbType = DbType.Int16;               
                Utils.ExecuteNonQuery("SPROC_UpdateTaskRequestStatus", Params);


            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkReportGenerationDao:UpdateTaskRequestStatus()");
                object[] objects = new object[2];
                objects[0] = requestId;
                objects[1] = taskStatus;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }

        public void InsertRequestOutputData(int requestId, string fileName)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@RequestId", requestId);
                Params[0].DbType = DbType.Int32;
                Params[1] = new SqlParameter("@FileName", fileName);
                Params[1].DbType = DbType.String;
                Utils.ExecuteNonQuery("SPROC_AddRequestOutPutData", Params);


            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkReportGenerationDao:InsertRequestOutputData()");
                object[] objects = new object[2];
                objects[0] = requestId;
                objects[1] = fileName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }
    }
}
