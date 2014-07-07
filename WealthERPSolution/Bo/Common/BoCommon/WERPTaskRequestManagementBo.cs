using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCommon;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCommon;
using VoReports;


namespace BoCommon
{
    public class WERPTaskRequestManagementBo
    {
        /// <summary>
        /// Create Task Request returns RequestId
        /// </summary>
        /// <param name="portfolioIDs"></param>
        /// <param name="subreportype"></param>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        public void CreateTaskRequest(int taskId, int userId, out int taskRequestId,string filePath)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();

            try
            {
                requestManagementDao.CreateTaskRequest(taskId, userId, out taskRequestId, filePath); 

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementBo.cs:CreateTaskRequest()");
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
        public void CreateBulkReportRequest(List<MFReportVo> mfReportVoList,MFReportEmailVo mfReportEmailVo, int parentRequestId, int taskId, int userId)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                requestManagementDao.CreateBulkReportRequest(mfReportVoList,mfReportEmailVo, parentRequestId, taskId, userId);

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementBo.cs:CreateBulkReportRequest()");
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


        public DataSet GetRequestStatusList(int adviserId, DateTime requestDate)
        {
            DataSet dsRequestList = null;
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                dsRequestList = requestManagementDao.GetRequestStatusList(adviserId, requestDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementBo.cs:GetRequestStatusList(int userId,DateTime requestDate)");
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
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();

            try
            {
               requestManagementDao.CreateBulkMailRequestRecord(bulkMailRequest);

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementBo.cs:CreateTaskRequest()");
                object[] objects = new object[2];
                objects[0] = bulkMailRequest["ADVISER_ID"];
                objects[1] = bulkMailRequest["USER_ID"];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
    }
}
