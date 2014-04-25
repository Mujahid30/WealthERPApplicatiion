﻿using System;
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
        public DataTable GetIPOIssueList(int adviserId, int issueId, int type)
        {
            DataTable dtIPOIssueList;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();

            try
            {
                dtIPOIssueList = onlineIPOOrderDao.GetIPOIssueList(adviserId, issueId,type);

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
                orderId = onlineIPOOrderDao.CreateIPOBidOrderDetails(adviserId, userId, dtIPOBidList, onlineIPOOrderVo,   ref applicationNo, ref apllicationNoStatus);

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

        public DataTable GetCustomerIPOIssueBook(int customerId)
        {

            DataTable dtCustomerIPOIssueBook;
            OnlineIPOOrderDao onlineIPOOrderDao = new OnlineIPOOrderDao();

            try
            {
                dtCustomerIPOIssueBook = onlineIPOOrderDao.GetCustomerIPOIssueBook(customerId);

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
    }
}
