using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DaoOnlineOrderManagement;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoOnlineOrderManagemnet;

namespace BoOnlineOrderManagement
{
    public class OnlineIPOOrderBo : OnlineOrderBo
    {
        public DataTable GetIPOIssueList(int adviserId, int issueId, int type, int customerId)
        {
            DataTable dtIPOIssueList;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();

            try
            {
                dtIPOIssueList = onlineIPOOrderDao.GetIPOIssueList(adviserId, issueId, type, customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:GetIPOIssueList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOIssueList;
        }

        public int CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList, OnlineIPOOrderVo onlineIPOOrderVo, ref string applicationNo, ref string apllicationNoStatus)
        {
            int orderId = 0;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();
            try
            {
                orderId = onlineIPOOrderDao.CreateIPOBidOrderDetails(adviserId, userId, dtIPOBidList, onlineIPOOrderVo, ref applicationNo, ref apllicationNoStatus);

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

        public DataTable GetCustomerIPOIssueBook(int customerId, int issueId, string status, DateTime fromdate, DateTime todate, int orderId, out string orderStep)
        {

            DataTable dtCustomerIPOIssueBook;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();

            try
            {
                dtCustomerIPOIssueBook = onlineIPOOrderDao.GetCustomerIPOIssueBook(customerId, issueId, status, fromdate, todate, orderId, out  orderStep);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:GetCustomerIPOIssueBook(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtCustomerIPOIssueBook;
        }
        public DataTable GetIPOHolding(int customerId)
        {
            DataTable dtIPOHolding;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();

            try
            {
                dtIPOHolding = onlineIPOOrderDao.GetIPOHolding(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:GetIPOHolding()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOHolding;
        }
        public DataTable GetCustomerIPOIssueSubBook(int customerId, int strIssuerId, int orderId)
        {

            DataTable dtCustomerIPOIssueChildBook;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();

            try
            {
                dtCustomerIPOIssueChildBook = onlineIPOOrderDao.GetCustomerIPOIssueSubBook(customerId, strIssuerId, orderId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:GetCustomerIPOIssueBook(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtCustomerIPOIssueChildBook;
        }
        public DataTable GetIPOIOrderList(int orderId, out bool isRMSDebited, out bool orderIscanclled, out string OderExtractStep)
        {
            DataTable dtGetIPOIOrderList;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();

            try
            {
                dtGetIPOIOrderList = onlineIPOOrderDao.GetIPOIOrderList(orderId, out isRMSDebited, out orderIscanclled, out OderExtractStep);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:GetIPOIOrderList(int orderId)");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetIPOIOrderList;
        }
        public int UpdateIPOBidOrderDetails(int userId, DataTable dtIPOBidList, int orderId, double differentialAmt)
        {
            int result = 0;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();
            try
            {
                result = onlineIPOOrderDao.UpdateIPOBidOrderDetails(userId, dtIPOBidList, orderId, differentialAmt);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:GetIPOIOrderList(int orderId)");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
        public bool CustomerCancelledOrder(int CustomerId, int AIMissueId)
        {
            bool result = false;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();
            try
            {
                result = onlineIPOOrderDao.CustomerCancelledOrder(CustomerId, AIMissueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:CustomerMultipleOrder(int CustomerId, int AIMissueId)");
                object[] objects = new object[1];
                objects[0] = CustomerId;
                objects[1] = AIMissueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public string IPOOrderExtractStep(int orderId)
        {
            string orderStep = string.Empty;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();
            try
            {
                orderStep = onlineIPOOrderDao.IPOOrderExtractStep(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:IPOOrderExtractStep(int orderId)");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderStep;
        }
        public bool CustomerIPOOrderCancelle(int orderId, string orderstatus)
        {
            bool result = false;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();
            try
            {
                result = onlineIPOOrderDao.CustomerIPOOrderCancelle(orderId, orderstatus);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "BoOnlineOrderManagement.cs:CustomerMultipleOrder(int CustomerId, int AIMissueId)");
                object[] objects = new object[1];
                objects[0] = orderId;
                objects[1] = orderstatus;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
    }
}
