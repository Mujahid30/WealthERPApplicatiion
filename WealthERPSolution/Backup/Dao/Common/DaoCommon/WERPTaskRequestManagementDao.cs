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
        public void CreateTaskRequest(int taskId, int userId, out int taskRequestId, string filePath, int adviserId, int rmId, int branchId, string uploadType, int xmlFileTypeId, int isOnline)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateTaskRequest;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateTaskRequest = db.GetStoredProcCommand("SPROC_CreateTaskRequest");
                db.AddInParameter(cmdCreateTaskRequest, "@TaskId", DbType.Int32, taskId);
                db.AddInParameter(cmdCreateTaskRequest, "@UserId", DbType.Int32, userId);
                db.AddInParameter(cmdCreateTaskRequest, "@DependentRequestId", DbType.Int32, 0);
                db.AddInParameter(cmdCreateTaskRequest, "@ParentRequestId", DbType.Int32, 0);
                db.AddOutParameter(cmdCreateTaskRequest, "@OutRequestId", DbType.Int32, 1000000);
                db.AddInParameter(cmdCreateTaskRequest, "@FilePath", DbType.String, filePath);
                db.AddInParameter(cmdCreateTaskRequest, "@AdvisorId", DbType.Int32, adviserId);
                db.AddInParameter(cmdCreateTaskRequest, "@IsOnline", DbType.Int32, isOnline);
                if (rmId == 0)
                    db.AddInParameter(cmdCreateTaskRequest, "@RmId", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@RmId", DbType.Int32, rmId);
                if (branchId == 0)
                    db.AddInParameter(cmdCreateTaskRequest, "@Branchid", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@Branchid", DbType.Int32, branchId);

                db.AddInParameter(cmdCreateTaskRequest, "@UploadType", DbType.String, uploadType);

                db.AddInParameter(cmdCreateTaskRequest, "@XMLFileTypeId", DbType.Int32, xmlFileTypeId);

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
        public void CreateTaskRequest(int taskId, int userId, out int taskRequestId, string filePath, int adviserId, int rmId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateTaskRequest;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateTaskRequest = db.GetStoredProcCommand("SPROC_CreateTaskRequestForKYC");
                db.AddInParameter(cmdCreateTaskRequest, "@TaskId", DbType.Int32, taskId);
                db.AddInParameter(cmdCreateTaskRequest, "@UserId", DbType.Int32, userId);
                db.AddOutParameter(cmdCreateTaskRequest, "@OutRequestId", DbType.Int32, 1000000);
                db.AddInParameter(cmdCreateTaskRequest, "@FilePath", DbType.String, filePath);
                db.AddInParameter(cmdCreateTaskRequest, "@AdvisorId", DbType.Int32, adviserId);
                if (rmId == 0)
                    db.AddInParameter(cmdCreateTaskRequest, "@RmId", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@RmId", DbType.Int32, rmId);
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
        public void CreateTaskRequestForRecon(int taskId, int userId, out int taskRequestId, string filePath, int adviserId, string uploadType, string remarks)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateTaskRequest;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateTaskRequest = db.GetStoredProcCommand("SPROC_CreateTaskRequestForRecon");
                db.AddInParameter(cmdCreateTaskRequest, "@TaskId", DbType.Int32, taskId);
                db.AddInParameter(cmdCreateTaskRequest, "@UserId", DbType.Int32, userId);
                db.AddInParameter(cmdCreateTaskRequest, "@DependentRequestId", DbType.Int32, 0);
                db.AddInParameter(cmdCreateTaskRequest, "@ParentRequestId", DbType.Int32, 0);
                db.AddOutParameter(cmdCreateTaskRequest, "@OutRequestId", DbType.Int32, 1000000);
                db.AddInParameter(cmdCreateTaskRequest, "@FilePath", DbType.String, filePath);
                db.AddInParameter(cmdCreateTaskRequest, "@AdvisorId", DbType.Int32, adviserId);

                db.AddInParameter(cmdCreateTaskRequest, "@UploadType", DbType.String, uploadType);

                db.AddInParameter(cmdCreateTaskRequest, "@Remarks", DbType.String, remarks);
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

        public DataSet GetAdviserWiseRM(int adviserId)
        {
            Database db;
            DbCommand cmdGetRequestList;
            DataSet dsRM = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRequestList = db.GetStoredProcCommand("SPROC_GetRm");
                db.AddInParameter(cmdGetRequestList, "@AdviserId", DbType.Int32, adviserId);
                dsRM = db.ExecuteDataSet(cmdGetRequestList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsRM;
        }

        public DataSet GetAdviserWiseBranch(int adviserId)
        {
            Database db;
            DbCommand cmdGetRequestList;
            DataSet dsBranch = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRequestList = db.GetStoredProcCommand("SPROC_GetBranch");
                db.AddInParameter(cmdGetRequestList, "@AdviserId", DbType.Int32, adviserId);
                dsBranch = db.ExecuteDataSet(cmdGetRequestList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsBranch;
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
        public DataSet GetRequestStatusList(int adviserId, DateTime requestDate)
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


        /// <summary>
        /// Create Task Request returns RequestId
        /// </summary>
        /// <param name="portfolioIDs"></param>
        /// <param name="subreportype"></param>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        public void CreateBulkMailRequestRecord(Dictionary<string, object> bulkMailRequest)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateTaskRequest;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateTaskRequest = db.GetStoredProcCommand("SPROC_CreateBulkMailRequestRecord");
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_TaskId", DbType.Int32, Convert.ToInt32(bulkMailRequest["TASK_ID"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_CustomerIds", DbType.String, Convert.ToString(bulkMailRequest["CUST_IDS"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_AdviserId", DbType.Int32, Convert.ToInt32(bulkMailRequest["ADVISER_ID"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_ReportTypeAsON", DbType.String, Convert.ToString(bulkMailRequest["ASON_REPORT"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_ReportTypeRange", DbType.String, Convert.ToString(bulkMailRequest["RANGE_REPORT"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_StartDate", DbType.Date, Convert.ToString(bulkMailRequest["START_DATE"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_EndDate", DbType.Date, Convert.ToString(bulkMailRequest["END_DATE"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_AsOnReportDate", DbType.Date, Convert.ToString(bulkMailRequest["ASON_DATE"]));
                db.AddInParameter(cmdCreateTaskRequest, "@WRR_IsGroupHead", DbType.Int16, Convert.ToString(bulkMailRequest["IS_GROUP_HEAD_REPORT"]));
                db.AddInParameter(cmdCreateTaskRequest, "@UserId", DbType.Int32, Convert.ToInt32(bulkMailRequest["USER_ID"]));
                db.ExecuteNonQuery(cmdCreateTaskRequest);
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFReports.cs:CreateBulkMailRequestRecord(Dictionary<string, object> bulkMailRequest)");
                object[] objects = new object[2];
                objects[0] = bulkMailRequest["ADVISER_ID"];
                objects[1] = bulkMailRequest["USER_ID"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void CreateTaskRequestForBulk(int taskId, int UserId, out int ReqId, int advisorId, string OrderBookType, string IssueNO)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateTaskRequest;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateTaskRequest = db.GetStoredProcCommand("SPROC_CreateTaskRequestForBulkOrderBook");
                db.AddInParameter(cmdCreateTaskRequest, "@TaskId", DbType.Int32, taskId);
                db.AddInParameter(cmdCreateTaskRequest, "@UserId", DbType.Int32, UserId);
                db.AddOutParameter(cmdCreateTaskRequest, "@OutRequestId", DbType.Int32, 1000000);
                db.AddInParameter(cmdCreateTaskRequest, "@AdvisorId", DbType.Int32, advisorId);
                db.AddInParameter(cmdCreateTaskRequest, "@OrderBookType", DbType.String, OrderBookType);
                db.AddInParameter(cmdCreateTaskRequest, "@IssueNO", DbType.String, IssueNO);
                db.ExecuteNonQuery(cmdCreateTaskRequest);
                Object objRequestId = db.GetParameterValue(cmdCreateTaskRequest, "@OutRequestId");
                if (objRequestId != DBNull.Value)
                    ReqId = int.Parse(db.GetParameterValue(cmdCreateTaskRequest, "@OutRequestId").ToString());
                else
                    ReqId = 0;

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementDao.cs:CreateTaskRequestForBulk()");
                object[] objects = new object[2];
                objects[0] = taskId;
                objects[1] = UserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataTable GetBrokerageCalculationDetails(int reqId, out string productType, out String productCategory, out string commissionType)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            DataTable dtBrokerageDetails;
            productType = string.Empty;
            productCategory = string.Empty;
            commissionType = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetTaskRequestBrokerageDetails");
                if (reqId != 0)
                    db.AddInParameter(dbCommand,"@ReqId", DbType.Int32, reqId);
                db.AddOutParameter(dbCommand, "@productType", DbType.String, 100000);
                db.AddOutParameter(dbCommand, "@productCategory", DbType.String, 1000000);
                db.AddOutParameter(dbCommand, "@CommissionType", DbType.String, 1000000);
                

                dtBrokerageDetails = db.ExecuteDataSet(dbCommand).Tables[0];
                if (!string.IsNullOrEmpty(db.GetParameterValue(dbCommand, "@productType").ToString()))
                {
                    productType = db.GetParameterValue(dbCommand, "@productType").ToString();
                }
                if (!string.IsNullOrEmpty(db.GetParameterValue(dbCommand, "@productCategory").ToString()))
                {
                    productCategory = db.GetParameterValue(dbCommand, "@productCategory").ToString();
                }
                if (!string.IsNullOrEmpty(db.GetParameterValue(dbCommand, "@CommissionType").ToString()))
                {
                    commissionType = db.GetParameterValue(dbCommand, "@CommissionType").ToString();
                }
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetBrokerageCalculationStatus(int reqId,DateTime FromDate,DateTime ToDate)");
                object[] objects = new object[4];
                objects[0] = reqId;
                objects[1] = productType;
                objects[2] = productCategory;
                objects[3] = commissionType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dtBrokerageDetails;

        }
        public DataTable GetBrokerageCalculationStatus(string product, string productCategory, int amc, int issueId, string commissionType, int month, int year)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            DataTable dtStatus;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetBrokerageStatus_Test");
                if ( !string.IsNullOrEmpty(product))
                    db.AddInParameter(dbCommand, "@Product", DbType.String, product);
                if (!string.IsNullOrEmpty(productCategory))
                    db.AddInParameter(dbCommand, "@ProductCategory", DbType.String, productCategory);
                if(amc!=0)
                  db.AddInParameter(dbCommand, "@ProductAmc", DbType.Int32, amc);
                if(issueId!=0)
                    db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
                if (!string.IsNullOrEmpty(commissionType))
                    db.AddInParameter(dbCommand, "@CommissionType", DbType.String, commissionType);
                if(month!=0)
                    db.AddInParameter(dbCommand, "@month", DbType.Int32, month);
                if(year!=0)
                    db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                dtStatus = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetBrokerageCalculationStatus(int reqId,DateTime FromDate,DateTime ToDate)");
                object[] objects = new object[7];
                objects[0] = product;
                objects[1] = productCategory;
                objects[2] = amc;
                objects[3] = issueId;
                objects[4] = commissionType;
                objects[5] = month;
                objects[6] = year;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtStatus;
                
        }
        public DataTable GetBrokerageCalculationStatus(int reqId,DateTime FromDate,DateTime ToDate)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            DataTable dtStatus;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetBrokerageStatus");
                if(reqId!=0)
                    db.AddInParameter(dbCommand, "@ReqId", DbType.Int32, reqId);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "@FromDate", DbType.Date, FromDate);
                if (ToDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "@ToDate", DbType.Date, ToDate);
                dtStatus = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetBrokerageCalculationStatus(int reqId,DateTime FromDate,DateTime ToDate)");
                object[] objects = new object[12];
                objects[0] = reqId;
                objects[1] = FromDate;
                objects[2] = ToDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dtStatus;

        }
        public DataSet GetBulkOrderStatus(int reqId, string OrderBookType, DateTime Fromdate, DateTime Todate)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            DataSet dsbos;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetBulkOrderStatus");
                db.AddInParameter(dbCommand, "@ReqId", DbType.Int32, reqId);
                if (OrderBookType != "0")
                    db.AddInParameter(dbCommand, "@OrderBookType", DbType.String, OrderBookType);
                else
                    db.AddInParameter(dbCommand, "@OrderBookType", DbType.String, DBNull.Value);
                if (Fromdate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "@FromDate", DbType.Date, Fromdate);
                else
                    db.AddInParameter(dbCommand, "@FromDate", DbType.Date, null);
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "@ToDate", DbType.Date, Todate);
                else
                    db.AddInParameter(dbCommand, "@ToDate", DbType.Date, null);
                dsbos = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetBulkOrderStatus(int reqId, string OrderBookType, DateTime Fromdate, DateTime Todate)");
                object[] objects = new object[12];
                objects[0] = reqId;
                objects[1] = Fromdate;
                objects[2] = Todate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsbos;
        }


        /// <summary>
        /// Create Task Request For Calculated Brokerage
        /// </summary>
        /// <param name="portfolioIDs"></param>
        /// <param name="subreportype"></param>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        public bool CheckCalculationRequestExists(string RequestHash, int adviserId)
        {
            bool isDuplicate = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_CheckDuplicateCalculationRequest");
                if (!string.IsNullOrEmpty(RequestHash))
                    db.AddInParameter(dbCommand, "@RequestParameterHash", DbType.String, RequestHash);
                db.AddInParameter(dbCommand, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddOutParameter(dbCommand, "@Count", DbType.Int32, 1000000);
                object count = db.ExecuteScalar(dbCommand);
                if (count != null && int.Parse(count.ToString()) > 0)
                {
                    isDuplicate = true;
                }

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", " UpdateBrokereageCalculationRequest(string sRequestIds,int userId)");
                object[] objects = new object[3];
                objects[0] = RequestHash;
                objects[1] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isDuplicate;
        }


        public bool UpdateBrokereageCalculationRequest(string sRequestIds, int userId, char CommandType)
        {
            bool isUpdated = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_ReprocessBrokerageRequest");
                if (!string.IsNullOrEmpty(sRequestIds))
                    db.AddInParameter(dbCommand, "@RequestIds", DbType.String, sRequestIds);
                if (userId != 0)
                    db.AddInParameter(dbCommand, "@userId", DbType.Int32, userId);
                db.AddInParameter(dbCommand, "@CommandType", DbType.String, CommandType);

                if (db.ExecuteNonQuery(dbCommand) != 0)
                    isUpdated = true;

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", " UpdateBrokereageCalculationRequest(string sRequestIds,int userId)");
                object[] objects = new object[3];
                objects[0] = sRequestIds;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return isUpdated;

        }
    
        public void CreateTaskRequestForBrokerageCalculation(int taskId, int UserId, out int reqId, string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category, string recontype, string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, int isAuthenticated, string requestParameterHash)
        {
            reqId = 0;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCreateTaskRequest;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateTaskRequest = db.GetStoredProcCommand("SPROC_CreateTaskRequestForBrokerageCalculation");
                db.AddInParameter(cmdCreateTaskRequest, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(cmdCreateTaskRequest, "@schemeid", DbType.Int32, schemeid);
                if (month != 0)
                    db.AddInParameter(cmdCreateTaskRequest, "@Month", DbType.Int16, month);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@Month", DbType.Int16, DBNull.Value);
                if (year != 0)
                    db.AddInParameter(cmdCreateTaskRequest, "@Year", DbType.Int16, year);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@Year", DbType.Int16, DBNull.Value);
                if (!string.IsNullOrEmpty(category))
                    db.AddInParameter(cmdCreateTaskRequest, "@Category", DbType.String, category);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@Category", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(recontype))
                    db.AddInParameter(cmdCreateTaskRequest, "@recontype", DbType.String, recontype);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@recontype", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(commtype))
                    db.AddInParameter(cmdCreateTaskRequest, "@commissiontype", DbType.String, commtype);
                else
                    db.AddInParameter(cmdCreateTaskRequest, "@commissiontype", DbType.String, DBNull.Value);
                db.AddInParameter(cmdCreateTaskRequest, "@issuer", DbType.Int32, issuer);
                db.AddInParameter(cmdCreateTaskRequest, "@productType", DbType.String, product);
                db.AddInParameter(cmdCreateTaskRequest, "@typeOfTransaction", DbType.Int16, typeOfTransaction);
                db.AddInParameter(cmdCreateTaskRequest, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(cmdCreateTaskRequest, "@commissionLookUpId", DbType.Int32, commissionLookUpId);
                db.AddInParameter(cmdCreateTaskRequest, "@orderStatus", DbType.String, orderStatus);
                db.AddInParameter(cmdCreateTaskRequest, "@AgentCode", DbType.String, agentCode);
                db.AddInParameter(cmdCreateTaskRequest, "@ProductCategory", DbType.String, productCategory);
                db.AddInParameter(cmdCreateTaskRequest, "@IsAuthenticated", DbType.Int16, isAuthenticated);
                db.AddOutParameter(cmdCreateTaskRequest, "@OutRequestId", DbType.Int32, 1000000);
                db.AddInParameter(cmdCreateTaskRequest, "@taskId", DbType.Int32, taskId);
                db.AddInParameter(cmdCreateTaskRequest, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(cmdCreateTaskRequest, "@RequestParameterHash", DbType.String, requestParameterHash);
                cmdCreateTaskRequest.CommandTimeout = 60 * 60;
                db.ExecuteNonQuery(cmdCreateTaskRequest);
                Object objRequestId = db.GetParameterValue(cmdCreateTaskRequest, "@OutRequestId");
                if (objRequestId != DBNull.Value)
                    reqId = int.Parse(db.GetParameterValue(cmdCreateTaskRequest, "@OutRequestId").ToString());
                else
                    reqId = 0;

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CreateTaskRequestForBrokerageCalculation(int taskId, int UserId, out int reqId, string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category, string recontype, string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, int isAuthenticated)");
                object[] objects = new object[12];
                objects[0] = AdviserId;
                objects[1] = schemeid;
                objects[2] = month;
                objects[3] = year;
                objects[4] = category;
                objects[5] = recontype;
                objects[6] = commtype;
                objects[7] = issuer;
                objects[8] = product;
                objects[9] = typeOfTransaction;
                objects[10] = issueId;
                objects[11] = commissionLookUpId;
                objects[12] = orderStatus;
                objects[13] = agentCode;
                objects[14] = productCategory;
                objects[15] = isAuthenticated;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

    }
}
