﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.IO;

namespace DaoOfflineOrderManagement
{
    public class OfflineIPOOrderDao
    {
        public DataTable GetIPOIssueList(int adviserId, int issueId, int type, int customerSubTypeId)
        {
            DataTable dtIPOIssueList;
            Database db;
            DbCommand GetIPOIssueListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOIssueListCmd = db.GetStoredProcCommand("SPROC_OFF_GetIPOIssueList");
                db.AddInParameter(GetIPOIssueListCmd, "@AdviserId", DbType.Int32, adviserId);
                if (issueId != 0)
                    db.AddInParameter(GetIPOIssueListCmd, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(GetIPOIssueListCmd, "@type", DbType.Int32, type);
                //db.AddInParameter(GetIPOIssueListCmd, "@CustomerSubTypeId", DbType.Int32, customerSubTypeId);

                dtIPOIssueList = db.ExecuteDataSet(GetIPOIssueListCmd).Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOfflineOrderManagement.cs:GetIPOIssueList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOIssueList;
        }
        public int CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList, OnlineIPOOrderVo onlineIPOOrderVo, int agentId, string agentCode,int EmpId)
        {
            int orderId = 0;
            Database db;
            DbCommand CreateIPOBidOrderCmd;
            DataSet dsIssueBidList = new DataSet();

            try
            {
                dsIssueBidList.Tables.Add(dtIPOBidList.Copy());
                dsIssueBidList.DataSetName = "IssueBidsDS";
                dsIssueBidList.Tables[0].TableName = "IssueBidsDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateIPOBidOrderCmd = db.GetStoredProcCommand("SPROC_OFF_CreateIPOBidOrderDetails");
                db.AddInParameter(CreateIPOBidOrderCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@UserId", DbType.Int32, userId);
                //db.AddInParameter(CreateIPOBidOrderCmd, "@C_CustomerId", DbType.Int32, onlineIPOOrderVo.CustomerId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@OrderDate", DbType.Date, onlineIPOOrderVo.OrderDate);
                db.AddInParameter(CreateIPOBidOrderCmd, "@AIM_IssueId", DbType.Int32, onlineIPOOrderVo.IssueId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@ApplicationNo", DbType.Int32, onlineIPOOrderVo.ApplicationNumber);
                db.AddInParameter(CreateIPOBidOrderCmd, "@PAG_AssetGroupCode", DbType.String, onlineIPOOrderVo.AssetGroup);
                db.AddInParameter(CreateIPOBidOrderCmd, "@CO_IsDeclarationAccepted", DbType.Int16, onlineIPOOrderVo.IsDeclarationAccepted ? 1 : 0);
                db.AddInParameter(CreateIPOBidOrderCmd, "@XMLIPOBids", DbType.Xml, dsIssueBidList.GetXml().ToString());
                db.AddInParameter(CreateIPOBidOrderCmd, "@AgentId", DbType.Int32, agentId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@AgentCode", DbType.String, agentCode);
                db.AddInParameter(CreateIPOBidOrderCmd, "@EmpId", DbType.Int32, EmpId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@CustomerName", DbType.String, onlineIPOOrderVo.CustomerName);
                db.AddInParameter(CreateIPOBidOrderCmd, "@CustomerPAN", DbType.String, onlineIPOOrderVo.CustomerPAN);
                db.AddInParameter(CreateIPOBidOrderCmd, "@CustomerType", DbType.String, onlineIPOOrderVo.CustomerType);
                db.AddInParameter(CreateIPOBidOrderCmd, "@CustomerSubTypeId", DbType.Int16, onlineIPOOrderVo.CustomerSubTypeId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@DematBeneficiaryAccountNum", DbType.String, onlineIPOOrderVo.DematBeneficiaryAccountNum);
                db.AddInParameter(CreateIPOBidOrderCmd, "@DematDepositoryName", DbType.String, onlineIPOOrderVo.DematDepositoryName);
                if (!string.IsNullOrEmpty(onlineIPOOrderVo.DematDPId))
                    db.AddInParameter(CreateIPOBidOrderCmd, "@DematDPId", DbType.String, onlineIPOOrderVo.DematDPId);



                db.AddOutParameter(CreateIPOBidOrderCmd, "@OrderId", DbType.Int32, 1000000);
                if (db.ExecuteNonQuery(CreateIPOBidOrderCmd) != 0)
                {
                    orderId = Convert.ToInt32(db.GetParameterValue(CreateIPOBidOrderCmd, "OrderId").ToString());

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
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs:CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderId;

        }
        public DataSet GetIPOIssueOrderDetails(int orderId)
        {
            Database db;
            DbCommand GetGetIPOIssueOrderDetails;
            DataSet dsGetIPOIssueOrderDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetIPOIssueOrderDetails = db.GetStoredProcCommand("SPROC_ViewIPOOfflineOrderDetails");
                db.AddInParameter(GetGetIPOIssueOrderDetails, "@orderId", DbType.Int32, orderId);
                dsGetIPOIssueOrderDetails = db.ExecuteDataSet(GetGetIPOIssueOrderDetails);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetIPOIssueOrderDetails;
        }
        public bool UpdateIPOBidOrderDetails(DataTable dtIPOBidTransactionDettails, int orderNo, string benificialAcc, string brokerCode, int userId, OnlineIPOOrderVo onlineIPOOrderVo,int EmpId)
        {
            Database db;
            DataSet dsIssueBidList = new DataSet(); ;
            DbCommand cmdUpdateIPOBidOrderDetails;
            bool bResult = false;
            int count = 0;
            try
            {
                dsIssueBidList.Tables.Add(dtIPOBidTransactionDettails.Copy());
                dsIssueBidList.DataSetName = "IssueBidsDS";
                dsIssueBidList.Tables[0].TableName = "IssueBidsDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateIPOBidOrderDetails = db.GetStoredProcCommand("SPROC_OFF_UpdateIPOBidOrderDetails");
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@XMLIPOBids", DbType.Xml, dsIssueBidList.GetXml().ToString());
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@orderId", DbType.Int32, orderNo);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@benificialAcc", DbType.String, benificialAcc);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@brokerCode", DbType.String, brokerCode);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@UserId", DbType.Int32, userId);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@AgentCode", DbType.String, onlineIPOOrderVo.AgentNo);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@AgentId", DbType.Int32, onlineIPOOrderVo.AgentId);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@CustomerName", DbType.String, onlineIPOOrderVo.CustomerName);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@CustomerPAN", DbType.String, onlineIPOOrderVo.CustomerPAN);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@CustomerType", DbType.String, onlineIPOOrderVo.CustomerType);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@CustomerSubTypeId", DbType.Int32, onlineIPOOrderVo.CustomerSubTypeId);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@DematBeneficiaryAccountNum", DbType.String, onlineIPOOrderVo.DematBeneficiaryAccountNum);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@DematDepositoryName", DbType.String, onlineIPOOrderVo.DematDepositoryName);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@DematDPId", DbType.String, onlineIPOOrderVo.DematDPId);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@customerbankAccId", DbType.Int64, onlineIPOOrderVo.BankAccountNo);
                db.AddInParameter(cmdUpdateIPOBidOrderDetails, "@EmpId", DbType.Int64, EmpId);

                if (db.ExecuteNonQuery(cmdUpdateIPOBidOrderDetails) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return bResult;
        }
        public bool ApplicationDuplicateCheck(int issueId, int applicationNo)
        {
            Database db;
            DbCommand cmdApplicationDuplicateCheck;
            bool bResult = false;
            int count = 0;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdApplicationDuplicateCheck = db.GetStoredProcCommand("SP_ApplicationDuplicateCheck");
                db.AddInParameter(cmdApplicationDuplicateCheck, "@Application", DbType.Int32, applicationNo);
                db.AddInParameter(cmdApplicationDuplicateCheck, "@AIM_IssueId", DbType.Int32, issueId);
                db.AddOutParameter(cmdApplicationDuplicateCheck, "@Count", DbType.Int32, 100);
                db.ExecuteNonQuery(cmdApplicationDuplicateCheck);
                int objCount = Convert.ToInt32(db.GetParameterValue(cmdApplicationDuplicateCheck, "Count").ToString());
                if (objCount != 0)
                    count = int.Parse(db.GetParameterValue(cmdApplicationDuplicateCheck, "@count").ToString());
                else
                    count = 0;
                if (count > 0)
                    bResult = true;
                //res = Convert.ToInt32(db.ExecuteScalar(cmdApplicationDuplicateCheck).ToString());
                //if (res > 0)
                //    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineIPOOrderDao.cs:ApplicationDuplicateCheck()");
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
        public bool OrderedDuplicateCheck(int orderId)
        {
            Database db;
            DbCommand cmdOrderedDuplicateCheck;
            bool bResult = false;
            int count = 0;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdOrderedDuplicateCheck = db.GetStoredProcCommand("SP_OrderedDuplicateCheck");
                db.AddInParameter(cmdOrderedDuplicateCheck, "@OrderId", DbType.Int32, orderId);
                db.AddOutParameter(cmdOrderedDuplicateCheck, "@Count", DbType.Int32, 100);
                db.ExecuteNonQuery(cmdOrderedDuplicateCheck);
                int objCount = Convert.ToInt32(db.GetParameterValue(cmdOrderedDuplicateCheck, "Count").ToString());
                if (objCount != 0)
                    count = int.Parse(db.GetParameterValue(cmdOrderedDuplicateCheck, "@count").ToString());
                else
                    count = 0;
                if (count > 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineIPOOrderDao.cs:ApplicationDuplicateCheck()");
                object[] objects = new object[2];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public DataTable GetAddedCustomer(int customerId)
        {
            DataTable dtGetAddedCustomer;
            Database db;
            DbCommand GetAddedCustomertCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAddedCustomertCmd = db.GetStoredProcCommand("SPROC_GetNewAddedCustomer");
                db.AddInParameter(GetAddedCustomertCmd, "@customerId", DbType.Int32, customerId);
                dtGetAddedCustomer = db.ExecuteDataSet(GetAddedCustomertCmd).Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOfflineOrderManagement.cs:GetIPOIssueList(int adviserId)");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetAddedCustomer;
        }
        public DataTable GetIssueCategory(int issueId)
        {

            DataTable dtIssueCategory;
            Database db;
            DbCommand IssueCategoryCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                IssueCategoryCmd = db.GetStoredProcCommand("SPROC_Off_GetInvestorCategory");
                db.AddInParameter(IssueCategoryCmd, "@IssueID", DbType.Int32, issueId);
                dtIssueCategory = db.ExecuteDataSet(IssueCategoryCmd).Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtIssueCategory;
        }
    }
}
