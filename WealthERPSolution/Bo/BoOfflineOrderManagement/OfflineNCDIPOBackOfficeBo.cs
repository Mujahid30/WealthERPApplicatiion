﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.IO.Compression;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;
using DaoOfflineOrderManagement;

namespace BoOfflineOrderManagement
{
    public class OfflineNCDIPOBackOfficeBo
    {
        OfflineNCDIPOBackOfficeDao offlineNCDBackOfficeDao;
        public DataTable GetOfflineCustomerNCDOrderBook(int adviserId, int issueNo, string status, DateTime dtFrom, DateTime dtTo, string userType, string agentCode, int orderNo,int authenticateType)
        {
            DataTable dtNCDOrder;
            offlineNCDBackOfficeDao = new OfflineNCDIPOBackOfficeDao();
            try
            {
                dtNCDOrder = offlineNCDBackOfficeDao.GetOfflineCustomerNCDOrderBook(adviserId, issueNo, status, dtFrom, dtTo, userType, agentCode, orderNo, authenticateType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineNCDBackOfficeBo.cs:GetAdviserNCDOrderBook()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrder;
        }
        public DataTable GetAdviserNCDOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataTable dtNCDOrderBook;
            offlineNCDBackOfficeDao = new OfflineNCDIPOBackOfficeDao();
            try
            {
                dtNCDOrderBook = offlineNCDBackOfficeDao.GetOfflineCustomerNCDOrderSubBook(adviserId, IssuerId, orderid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineNCDBackOfficeBo.cs:GetAdviserNCDOrderSubBook()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrderBook;
        }
        public DataSet GetNCDIssueOrderDetails(int orderId)
        {
            DataSet dsGetNCDIssueOrderDetails;
            offlineNCDBackOfficeDao = new OfflineNCDIPOBackOfficeDao();
            try
            {
                dsGetNCDIssueOrderDetails = offlineNCDBackOfficeDao.GetNCDIssueOrderDetails(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetNCDIssueOrderDetails;
        }
        public bool UpdateNCDDetails(int orderid, int userid, DataTable dtOrderDetails,string brokerCode)
        {
            bool result = false;
            offlineNCDBackOfficeDao = new OfflineNCDIPOBackOfficeDao();
            try
            {
                result = offlineNCDBackOfficeDao.UpdateNCDDetails(orderid, userid, dtOrderDetails, brokerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
    }
}
