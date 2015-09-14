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
        /// 
        public void CreateTaskRequest(int taskId, int userId, out int taskRequestId, string filePath, int adviserId, int rmId)
        {
             WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                requestManagementDao.CreateTaskRequest(taskId, userId, out taskRequestId, filePath, adviserId, rmId);
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
        public void CreateTaskRequest(int taskId, int userId, out int taskRequestId, string filePath, int adviserId, int rmId, int branchId, string uploadType, int xmlFileTypeId,int isOnline)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();

            try
            {
                requestManagementDao.CreateTaskRequest(taskId, userId, out taskRequestId, filePath, adviserId, rmId, branchId, uploadType, xmlFileTypeId, isOnline);

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
        public void CreateTaskRequestForRecon(int taskId, int userId, out int taskRequestId, string filePath, int adviserId, string uploadType, string remarks)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();

            try
            {
                requestManagementDao.CreateTaskRequestForRecon(taskId, userId, out taskRequestId, filePath, adviserId, uploadType, remarks);

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementBo.cs:CreateTaskRequestForRecon()");
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
            DataSet dsRm = new DataSet();
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                dsRm = requestManagementDao.GetAdviserWiseRM(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsRm;
        }

        public DataSet GetAdviserWiseBranch(int adviserId)
        {
            DataSet dsBranch = new DataSet();
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                dsBranch = requestManagementDao.GetAdviserWiseBranch(adviserId);
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

        public void CreateTaskRequestForBulk(int taskId, int UserId, out int ReqId, int advisorId, string OrderBookType, string IssueNO)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();

            try
            {
                requestManagementDao.CreateTaskRequestForBulk(taskId, UserId, out ReqId, advisorId, OrderBookType, IssueNO);

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementBo.cs:CreateTaskRequestForBulk()");
                object[] objects = new object[2];
                objects[0] = taskId;
                objects[1] = UserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataSet GetBulkOrderStatus(int reqId,string OrderBookType,DateTime Fromdate,DateTime Todate)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                return requestManagementDao.GetBulkOrderStatus(reqId,OrderBookType, Fromdate, Todate);

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
        }
        public DataTable GetBrokerageCalculationDetails(int reqId, out string productType, out String productCategory, out string commissionType)
        { 
         WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
         productType = string.Empty;
         productCategory = string.Empty;
         commissionType = string.Empty;
         try
         {
             
             return requestManagementDao.GetBrokerageCalculationDetails(reqId, out productType, out productCategory, out commissionType);
         }
         catch (BaseApplicationException ex)
         {
             throw (ex);
         }
         catch (Exception Ex)
         {
             BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
             NameValueCollection FunctionInfo = new NameValueCollection();
             FunctionInfo.Add("Method", "WERPTaskRequestManagementBo:GetBrokerageCalculationDetails(int reqId, out string productType, out String productCategory, out string commissionType)");
             object[] objects = new object[12];
             objects[0] = reqId;
             objects[1] = productType;
             objects[2] = productCategory;
             FunctionInfo = exBase.AddObject(FunctionInfo, objects);
             exBase.AdditionalInformation = FunctionInfo;
             ExceptionManager.Publish(exBase);
             throw exBase;
         }
        }
        public void CreateTaskRequestForBrokerageCalculation(int taskId, int UserId, out int reqId, string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category, string recontype, string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, int isAuthenticated, string requestParameterHash)
        {
            reqId = 0;
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                requestManagementDao.CreateTaskRequestForBrokerageCalculation(taskId, UserId, out  reqId, product, typeOfTransaction, AdviserId, schemeid, month, year, category, recontype, commtype, issuer, issueId, commissionLookUpId, orderStatus, agentCode, productCategory, isAuthenticated, requestParameterHash);
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
        public DataTable GetBrokerageCalculationStatus( string product,string productCategory,int amc,int issueId,string commissionType,int month,int year)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
          return  requestManagementDao.GetBrokerageCalculationStatus(product, productCategory, amc, issueId, commissionType, month, year);

        }
        public DataTable GetBrokerageCalculationStatus(int reqId, DateTime FromDate, DateTime ToDate)
        {
            WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
            try
            {
                return requestManagementDao.GetBrokerageCalculationStatus(reqId, FromDate, ToDate);

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "WERPTaskRequestManagementBo:GetBrokerageCalculationStatus(int reqId,DateTime FromDate,DateTime ToDate)");
                object[] objects = new object[12];
                objects[0] = reqId;
                objects[1] = FromDate;
                objects[2] = ToDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }
        public bool UpdateBrokereageCalculationRequest(string sRequestIds, int userId, char CommandType)
        {
            try
            {
                WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
                return requestManagementDao.UpdateBrokereageCalculationRequest(sRequestIds, userId, CommandType);
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                object[] objects = new object[3];
                objects[0] = sRequestIds;
                objects[1] = userId;
                objects[2] = CommandType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        
        }
        public bool CheckCalculationRequestExists(string RequestHash, int adviserId)
        {
            try
            {
                WERPTaskRequestManagementDao requestManagementDao = new WERPTaskRequestManagementDao();
                return requestManagementDao.CheckCalculationRequestExists(RequestHash, adviserId);

            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", " CheckCalculationRequestExists(string RequestHash, int adviserId)");
                object[] objects = new object[3];
                objects[0] = RequestHash;
                objects[1] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public string RequestCalculationHash(int taskId, string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category,string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, int isAuthenticated)
        {
            StringBuilder sbRequestParameter = new StringBuilder();
            Encryption er = new Encryption();
            if(taskId!=null)
                sbRequestParameter.Append(taskId);
            if(product!=null)
                sbRequestParameter.Append(product);
            if (typeOfTransaction != null)
                sbRequestParameter.Append(typeOfTransaction);
            if (AdviserId != null)
                sbRequestParameter.Append(AdviserId);
            if (schemeid != null)
                sbRequestParameter.Append(schemeid);
            if (month != null)
                sbRequestParameter.Append(month);
            if (year != null)
                sbRequestParameter.Append(year);
            if (category != null)
                sbRequestParameter.Append(category);
            if (commtype != null)
                sbRequestParameter.Append(commtype);
            if (issuer != null)
                sbRequestParameter.Append(issuer);
            if (issueId != null)
                sbRequestParameter.Append(issueId);
            if (commissionLookUpId != null)
                sbRequestParameter.Append(commissionLookUpId);
            if (orderStatus != null)
                sbRequestParameter.Append(orderStatus);
            if (agentCode != null)
                sbRequestParameter.Append(agentCode);
            if (productCategory != null)
                sbRequestParameter.Append(productCategory);
            if (isAuthenticated != null)
                sbRequestParameter.Append(isAuthenticated);
            char[] chData = sbRequestParameter.ToString().ToCharArray();
            byte[] byData = new byte[chData.Length];
            return er.HashString(chData, byData);
        }
    }
}
