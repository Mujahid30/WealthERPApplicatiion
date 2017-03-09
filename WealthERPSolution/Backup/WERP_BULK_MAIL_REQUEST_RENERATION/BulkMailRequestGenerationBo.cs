using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VoEmailSMS;

namespace WERP_BULK_MAIL_REQUEST_RENERATION
{
    public class BulkMailRequestGenerationBo
    {
        string daemonCode;
        string reportRepositoryFileLocation;

        public BulkMailRequestGenerationBo(string reportRepositoryLocation)
        {
            daemonCode = Environment.MachineName + "BULKMAILREPORT";
            Trace("Starting BULK MAIL REPORT REQUEST processor...");
            reportRepositoryFileLocation = reportRepositoryLocation;

        }

        public void BulkMailRequestProcessor()
        {
            DataTable dtBulkReportMailRequestList = GetTheBulkMailReportRequestList(daemonCode);
            foreach (DataRow dr in dtBulkReportMailRequestList.Rows)
            {
                int requestId = Convert.ToInt32(dr["WR_RequestId"].ToString());
                int dependentRequestId = Convert.ToInt32(dr["WR_DependentOn"].ToString());
                bool flag;
                int logId = 0;
                try
                {
                    //bool requestStatus = CheckDependentRequestStatus(dependentRequestId);
                    //if (requestStatus == true)
                    //{
                        CreateTaskRequestLOG(requestId, out logId);
                        flag=ProcessBulkMailRequest(requestId, dependentRequestId);
                        if(flag)
                        UpdateTaskRequestLOG(logId, "MAIL REQUEST SENT");
                        //UpdateTaskRequestStatus(requestId, 1);
                    //}
                    //else
                    //{
                        
                    //}
                }
                catch (Exception ex)
                {
                    //bulkReportGenerationDao.UpdateTaskRequestLOG(logId, ex.Message);
                }
                finally
                {


                }


            }

        }

        private bool ProcessBulkMailRequest(int requestId, int dependentRequestId)
        {
            EmailVo emailVo = new EmailVo();
            DataSet dsReportInputOutPutData = GetBulkMailReportInputOutData(requestId, dependentRequestId);
            DataTable dtRequestInputData = dsReportInputOutPutData.Tables[0];
            DataTable dtRequestOutPutData = dsReportInputOutPutData.Tables[1];
            bool flag;
            try
            {
                emailVo = FillBulkMailParamerValues(dtRequestInputData, dtRequestOutPutData);
                emailVo.Body = "Dear Customer Please find attached report";
                emailVo.Subject = "MF Report";
                emailVo.SourceId = requestId;
                flag=CreateBulkMailRequest(emailVo);
                return flag;
            }
            catch (BaseApplicationException Ex)
            {
                UpdateRequestDetails(requestId);
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkMailRequestGenerationBo:ProcessBulkMailRequest()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private EmailVo FillBulkMailParamerValues(DataTable dtRequestInputData, DataTable dtRequestOutPutData)
        {
            EmailVo emailVo = new EmailVo();

            try
            {
                foreach (DataRow dr in dtRequestInputData.Rows)
                {

                    switch (dr["WP_ParameterCode"].ToString())
                    {
                        case "AID":
                            emailVo.AdviserId = Int32.Parse(dr["WRD_InputParameterValue"].ToString());
                            break;
                        case "CID":
                            emailVo.CustomerId = Int32.Parse(dr["WRD_InputParameterValue"].ToString());
                            break;
                        case "CMAIL":
                            emailVo.To = dr["WRD_InputParameterValue"].ToString();
                            break;
                        case "RMAIL":
                            emailVo.EmailFrom = dr["WRD_InputParameterValue"].ToString();
                            break;
                        case "ONAME":
                            emailVo.OrgName = dr["WRD_InputParameterValue"].ToString();
                            break;

                    }
                }

                foreach (DataRow dr in dtRequestOutPutData.Rows)
                {
                    emailVo.FileName += reportRepositoryFileLocation + @"\" + dr["WRD_OutPutParameterValue"].ToString() + "~";

                }

                if (emailVo.FileName.EndsWith("~"))
                    emailVo.FileName = emailVo.FileName.Substring(0, emailVo.FileName.Length - 1);
            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkMailRequestGenerationBo:FillBulkMailParamerValues()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return emailVo;
        }

        private DataSet GetBulkMailReportInputOutData(int requestId, int dependentRequestId)
        {
            DataSet DS = null;
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@RequestId", requestId);
                Params[0].DbType = DbType.Int32;
                Params[1] = new SqlParameter("@DependentRequestId", dependentRequestId);
                Params[1].DbType = DbType.Int32;
                DS = Utils.ExecuteDataSet("SPROC_GetBulkMailInputOutput", Params);

            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkMailRequestGenerationBo:GetBulkMailReportInputOutData()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return DS;
        }

        public void UpdateTaskRequestStatus(int requestId, int taskStatus)
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
                FunctionInfo.Add("Method", "BulkReportGenerationDao:UpdateTaskRequestLOG()");
                object[] objects = new object[2];
                objects[0] = logId;
                objects[1] = message;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }



        }

        public void UpdateRequestDetails(int requestId)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@RequestId", requestId);
                Params[0].DbType = DbType.Int32;
                Utils.ExecuteNonQuery("SPROC_UpdateRequestDetails", Params);

            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkMailRequestGenerationBo:UpdateRequestDetails()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

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

        public bool CheckDependentRequestStatus(int requestId)
        {
            DataSet DS = null;
            bool requestStatus = false;
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@RequestId", requestId);
                Params[0].DbType = DbType.Int32;
                DS = Utils.ExecuteDataSet("SPROC_RequestStatus", Params);
                if (DS.Tables[0].Rows.Count >= 1)
                {
                    requestStatus = true;
                }
            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkMailRequestGenerationBo:CheckDependentRequestStatus()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return requestStatus;

        }

        public DataTable GetTheBulkMailReportRequestList(string daemonCode)
        {
            DataSet DS = null;
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@TaskId", 2);
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
                FunctionInfo.Add("Method", "BulkMailRequestGenerationBo:GetTheBulkReportRequestList()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return DS.Tables[0];

        }

        private bool CreateBulkMailRequest(EmailVo emailVo)
        {
            try
            {
                bool flag=false;
                SqlParameter[] Params = new SqlParameter[9];
                Params[0] = new SqlParameter("@TO", emailVo.To);
                Params[0].DbType = DbType.String;

                Params[1] = new SqlParameter("@CC", emailVo.Cc);
                Params[1].DbType = DbType.String;

                Params[2] = new SqlParameter("@BCC", emailVo.Bcc);
                Params[2].DbType = DbType.String;

                Params[3] = new SqlParameter("@Subject", emailVo.Subject);
                Params[3].DbType = DbType.String;

                Params[4] = new SqlParameter("@Body", emailVo.Body);
                Params[4].DbType = DbType.String;

                Params[5] = new SqlParameter("@AdviserId", emailVo.AdviserId);
                Params[5].DbType = DbType.Int32;

                Params[6] = new SqlParameter("@FromEmailId", emailVo.EmailFrom);
                Params[6].DbType = DbType.String;

                Params[7] = new SqlParameter("@SourceId", emailVo.SourceId);
                Params[7].DbType = DbType.Int32;

                Params[8] = new SqlParameter("@FilePathList", emailVo.FileName);
                Params[8].DbType = DbType.String;

                Utils.ExecuteNonQuery("SPROC_CreateReportBulkMailRequest", Params);
                flag=true;
                return flag;
            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BulkMailRequestGenerationBo:CheckDependentRequestStatus()");
                object[] objects = new object[1];
                objects[0] = daemonCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        private void Trace(string Msg)
        {
            Utils.Trace(daemonCode + ": " + Msg);
        }

    }
}
