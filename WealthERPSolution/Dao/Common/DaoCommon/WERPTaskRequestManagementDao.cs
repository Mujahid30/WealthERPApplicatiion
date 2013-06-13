using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoReports;



namespace DaoCommon
{
    public class WERPTaskRequestManagementDao
    {
        /// <summary>
        /// Create Task Request returns RequestId
        /// </summary>
        /// <param name="portfolioIDs"></param>
        /// <param name="subreportype"></param>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        public void CreateTaskRequest(int taskId, int userId, out int taskRequestId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateTaskRequest;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateTaskRequest = db.GetStoredProcCommand("SPROC_CreateTaskRequest");
                db.AddInParameter(cmdCreateTaskRequest, "@TaskId", DbType.Int32, taskId);
                db.AddInParameter(cmdCreateTaskRequest, "@UserId", DbType.Int32, userId);
                db.AddOutParameter(cmdCreateTaskRequest, "@OutRequestId", DbType.Int32, 1000000);
                db.ExecuteNonQuery(cmdCreateTaskRequest);

                Object objRequestId = db.GetParameterValue(cmdCreateTaskRequest, "@OutRequestId");
                if (objRequestId != DBNull.Value)
                    taskRequestId = int.Parse(db.GetParameterValue(cmdCreateTaskRequest, "@OutRequestId").ToString());
                else
                    taskRequestId = 0;

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFReports.cs:GetCalculateFromDate()");
                object[] objects = new object[2];
                objects[0] = taskId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        /// <summary>
        /// Create Bulk Report request to Queue
        /// </summary>
        /// <param name="portfolioIDs"></param>
        /// <param name="subreportype"></param>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        public void CreateBulkReportRequest(List<MFReportVo> mfReportVoList, MFReportEmailVo mfReportEmailVo, int parentRequestId, int taskId, int userId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateReportRequest;

            try
            {
                foreach (MFReportVo mfReportVo in mfReportVoList)
                {
                    try
                    {
                        db = DatabaseFactory.CreateDatabase("wealtherp");
                        cmdCreateReportRequest = db.GetStoredProcCommand("SPROC_CreateBulkReportRequest");
                        db.AddInParameter(cmdCreateReportRequest, "@TaskId", DbType.Int32, taskId);
                        db.AddInParameter(cmdCreateReportRequest, "@UserId", DbType.Int32, userId);
                        db.AddInParameter(cmdCreateReportRequest, "@ParentRequestId", DbType.Int32, parentRequestId);
                        db.AddInParameter(cmdCreateReportRequest, "@ReportName", DbType.String, mfReportVo.ReportName);
                        db.AddInParameter(cmdCreateReportRequest, "@FromDate", DbType.Date, mfReportVo.FromDate);
                        db.AddInParameter(cmdCreateReportRequest, "@Todate", DbType.Date, mfReportVo.ToDate);
                        db.AddInParameter(cmdCreateReportRequest, "@ReportType", DbType.String, mfReportVo.SubType);
                        db.AddInParameter(cmdCreateReportRequest, "@AdviserId", DbType.Int32, mfReportVo.AdviserId);
                        db.AddInParameter(cmdCreateReportRequest, "@CustomerId", DbType.Int32, mfReportVo.CustomerId);
                        db.AddInParameter(cmdCreateReportRequest, "@PortfolioIds", DbType.String, mfReportVo.PortfolioIds);
                        db.ExecuteNonQuery(cmdCreateReportRequest);
                    }
                    catch (BaseApplicationException ex)
                    {
                        throw (ex);
                    }
                    catch (Exception Ex)
                    {
                        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                        NameValueCollection FunctionInfo = new NameValueCollection();
                        FunctionInfo.Add("Method", "WERPTaskRequestManagementDao.cs:CreateBulkReportRequest()");
                        object[] objects = new object[10];
                        objects[0] = taskId;
                        objects[1] = userId;
                        objects[2] = parentRequestId;
                        objects[3] = mfReportVo.ReportName;
                        objects[4] = mfReportVo.FromDate;
                        objects[5] = mfReportVo.ToDate;
                        objects[6] = mfReportVo.SubType;
                        objects[7] = mfReportVo.AdviserId;
                        objects[8] = mfReportVo.CustomerId;
                        objects[9] = mfReportVo.PortfolioIds;
                        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                        exBase.AdditionalInformation = FunctionInfo;
                        ExceptionManager.Publish(exBase);
                        throw exBase;
                    }
                }

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateReportRequest = db.GetStoredProcCommand("SPROC_CreateBulkMailReportRequest");
                db.AddInParameter(cmdCreateReportRequest, "@UserId", DbType.Int32, userId);
                db.AddInParameter(cmdCreateReportRequest, "@DependentRequestId", DbType.Int32, parentRequestId);
                db.AddInParameter(cmdCreateReportRequest, "@ReportTypeName", DbType.String, mfReportEmailVo.ReportTypeName);
                db.AddInParameter(cmdCreateReportRequest, "@AdviserId", DbType.Int32, mfReportEmailVo.AdviserId);
                db.AddInParameter(cmdCreateReportRequest, "@CustomerId", DbType.Int32, mfReportEmailVo.CustomerId);
                db.AddInParameter(cmdCreateReportRequest, "@CustomerEmail", DbType.String, mfReportEmailVo.CustomerEmail);
                db.AddInParameter(cmdCreateReportRequest, "@RMEMail", DbType.String, mfReportEmailVo.RMEmail);
                db.ExecuteNonQuery(cmdCreateReportRequest);

                
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementDao.cs:CreateBulkReportRequest()");
                object[] objects = new object[3];
                objects[0] = taskId;
                objects[1] = userId;
                objects[2] = parentRequestId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        /// <summary>
        /// Get all the request list and status for a particular date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="requestDate"></param>
        public DataSet GetRequestStatusList(int adviserId,DateTime requestDate)
        {
            Database db;
            DbCommand cmdGetRequestList;
            DataSet dsRequestList = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRequestList = db.GetStoredProcCommand("SPROC_GetTaskRequestStatusList");
                db.AddInParameter(cmdGetRequestList, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetRequestList, "@RequestDate", DbType.Date, requestDate);
                dsRequestList = db.ExecuteDataSet(cmdGetRequestList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementDao.cs:GetRequestStatusList(int userId,DateTime requestDate)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsRequestList;

 
        }


    }
}
