using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DaoOfflineOrderManagement;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoOnlineOrderManagemnet;

namespace BoOfflineOrderManagement
{
    public class OfflineIPOOrderBo
    {
        public DataTable GetIPOIssueList(int adviserId, int issueId, int type, int customerId)
        {
            DataTable dtIPOIssueList;
            OfflineIPOOrderDao offlineIPOOrderDao = new OfflineIPOOrderDao();

            try
            {
                dtIPOIssueList = offlineIPOOrderDao.GetIPOIssueList(adviserId, issueId, type, customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOfflineOrderManagement.cs:GetIPOIssueList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOIssueList;
        }
        public int CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList, OnlineIPOOrderVo onlineIPOOrderVo, int agentId, string agentCode)
        {
            int orderId = 0;
            OfflineIPOOrderDao OfflineIPOOrderDao = new OfflineIPOOrderDao();
            try
            {
                orderId = OfflineIPOOrderDao.CreateIPOBidOrderDetails(adviserId, userId, dtIPOBidList, onlineIPOOrderVo, agentId, agentCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderId;
        }
        public DataSet GetIPOIssueOrderDetails(int orderId)
        {
            DataSet dsGetIPOIssueOrderDetails;
            OfflineIPOOrderDao OfflineIPOOrderDao = new OfflineIPOOrderDao();

            try
            {
                dsGetIPOIssueOrderDetails = OfflineIPOOrderDao.GetIPOIssueOrderDetails(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetIPOIssueOrderDetails;
        }
        public bool UpdateIPOBidOrderDetails(DataTable dtIPOBidTransactionDettails,int orderNo,string benificialAcc)
        {
            bool bResult = false;
            OfflineIPOOrderDao OfflineIPOOrderDao = new OfflineIPOOrderDao();
            try
            {
                bResult = OfflineIPOOrderDao.UpdateIPOBidOrderDetails(dtIPOBidTransactionDettails, orderNo, benificialAcc);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool ApplicationDuplicateCheck(int issueId, int applicationNo)
        {
            bool bResult = false;
            OfflineIPOOrderDao OfflineIPOOrderDao = new OfflineIPOOrderDao();
            try
            {
                bResult = OfflineIPOOrderDao.ApplicationDuplicateCheck(issueId, applicationNo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineIPOOrderDao.cs:PANNumberDuplicateCheck()");
                object[] objects = new object[2];
                objects[0] = issueId;
                objects[1] = applicationNo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
    }
}
