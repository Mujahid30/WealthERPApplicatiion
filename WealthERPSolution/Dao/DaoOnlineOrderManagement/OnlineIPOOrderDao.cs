using System;
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


namespace DaoOnlineOrderManagement
{
    public class OnlineIPOOrderDao : OnlineOrderDao
    {
        public DataTable GetIPOIssueList(int adviserId, int issueId, int type, int customerId)
        {
            DataTable dtIPOIssueList;
            Database db;
            DbCommand GetIPOIssueListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOIssueListCmd = db.GetStoredProcCommand("SPROC_ONL_GetIPOIssueList");
                db.AddInParameter(GetIPOIssueListCmd, "@AdviserId", DbType.Int32, adviserId);
                if (issueId != 0)
                    db.AddInParameter(GetIPOIssueListCmd, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(GetIPOIssueListCmd, "@type", DbType.Int32, type);
                db.AddInParameter(GetIPOIssueListCmd, "@customerId", DbType.Int32, customerId);

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
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs:GetIPOIssueList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOIssueList;
        }

        public int CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList, OnlineIPOOrderVo onlineIPOOrderVo,ref string applicationNo,ref string apllicationNoStatus)
        {
            int orderId=0;
            Database db;
            DbCommand CreateIPOBidOrderCmd;            
            DataSet dsIssueBidList = new DataSet();  

            try
            {               
                dsIssueBidList.Tables.Add(dtIPOBidList.Copy());
                dsIssueBidList.DataSetName = "IssueBidsDS";
                dsIssueBidList.Tables[0].TableName = "IssueBidsDT";                
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateIPOBidOrderCmd = db.GetStoredProcCommand("SPROC_ONL_CreateIPOBidOrderDetails");
                db.AddInParameter(CreateIPOBidOrderCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@C_CustomerId", DbType.Int32, onlineIPOOrderVo.CustomerId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@OrderDate", DbType.Date, onlineIPOOrderVo.OrderDate);
                db.AddInParameter(CreateIPOBidOrderCmd, "@AIM_IssueId", DbType.Int32, onlineIPOOrderVo.IssueId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@PAG_AssetGroupCode", DbType.String, onlineIPOOrderVo.AssetGroup);
                db.AddInParameter(CreateIPOBidOrderCmd, "@CO_IsDeclarationAccepted", DbType.Int16,  onlineIPOOrderVo.IsDeclarationAccepted?1:0);

                db.AddInParameter(CreateIPOBidOrderCmd, "@XMLIPOBids", DbType.Xml,dsIssueBidList.GetXml().ToString());
                db.AddOutParameter(CreateIPOBidOrderCmd, "@OrderId", DbType.Int32, 1000000);
                db.AddOutParameter(CreateIPOBidOrderCmd, "@ApplicationNo", DbType.String, 1000000);
                db.AddOutParameter(CreateIPOBidOrderCmd, "@AplicationNoStatus", DbType.String, 1000000);

             
                if (db.ExecuteNonQuery(CreateIPOBidOrderCmd) != 0)
                {
                     orderId = Convert.ToInt32(db.GetParameterValue(CreateIPOBidOrderCmd, "OrderId").ToString());
                    applicationNo= db.GetParameterValue(CreateIPOBidOrderCmd, "@ApplicationNo").ToString() ;
                    apllicationNoStatus=  db.GetParameterValue(CreateIPOBidOrderCmd, "@AplicationNoStatus").ToString();                   

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

        public DataTable GetCustomerIPOIssueBook(int customerId, int issueId, string status, DateTime fromdate, DateTime todate, int orderId, out string orderStep)
        {
            DataTable dtCustomerIPOIssueBook;
            Database db;
            DbCommand GetCustomerIPOIssueListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCustomerIPOIssueListCmd = db.GetStoredProcCommand("SPROC_ONL_GetCustomerIPOIssueOrderBooks");
                db.AddInParameter(GetCustomerIPOIssueListCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(GetCustomerIPOIssueListCmd, "@AIMissue", DbType.Int32, issueId);
                db.AddInParameter(GetCustomerIPOIssueListCmd, "@Status", DbType.String, status);
                db.AddInParameter(GetCustomerIPOIssueListCmd, "@Fromdate", DbType.DateTime, fromdate);
                db.AddInParameter(GetCustomerIPOIssueListCmd, "@ToDate", DbType.DateTime, todate);
                  db.AddOutParameter(GetCustomerIPOIssueListCmd, "@orderStep", DbType.String, 10000);
                if (orderId != 0)
                {
                    db.AddInParameter(GetCustomerIPOIssueListCmd, "@OrderId", DbType.Int32, orderId);
                }
                dtCustomerIPOIssueBook = db.ExecuteDataSet(GetCustomerIPOIssueListCmd).Tables[0];
                orderStep = db.GetParameterValue(GetCustomerIPOIssueListCmd, "@orderStep").ToString();
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs: GetCustomerIPOIssueBook(int customerId)");
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
            Database db;
            DbCommand GetIPOHoldingCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOHoldingCmd = db.GetStoredProcCommand("SPROC_ONL_GetIPOHolding");
                db.AddInParameter(GetIPOHoldingCmd, "@customerId", DbType.Int32, customerId);
                dtIPOHolding = db.ExecuteDataSet(GetIPOHoldingCmd).Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs:GetIPOHolding()");
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
            DataTable dtCustomerIPOIssueSubdBook;
            Database db;
            DbCommand GetCustomerIPOIssueSubListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCustomerIPOIssueSubListCmd = db.GetStoredProcCommand("SPROC_ONL_GetCustomerIPOIssueSubOrderBooks");
                db.AddInParameter(GetCustomerIPOIssueSubListCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(GetCustomerIPOIssueSubListCmd, "@IssueId", DbType.Int32, strIssuerId);
                db.AddInParameter(GetCustomerIPOIssueSubListCmd, "@orderId", DbType.Int32, orderId);
                dtCustomerIPOIssueSubdBook = db.ExecuteDataSet(GetCustomerIPOIssueSubListCmd).Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs: GetCustomerIPOIssueBook(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtCustomerIPOIssueSubdBook;
        }
        public DataTable GetIPOIOrderList(int orderId, out bool isRMSDebited)
        {
            DataTable dtGetIPOIOrderList;
            Database db;
            DbCommand GetGetIPOIOrderList;

             isRMSDebited = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetIPOIOrderList = db.GetStoredProcCommand("SPROC_ONL_ViewIPOOrder");
                db.AddInParameter(GetGetIPOIOrderList, "@orderId", DbType.Int32, orderId);
                db.AddOutParameter(GetGetIPOIOrderList, "@iseligibleRMSDebit", DbType.Boolean, 10000);
                dtGetIPOIOrderList = db.ExecuteDataSet(GetGetIPOIOrderList).Tables[0];
                    isRMSDebited =Convert.ToBoolean(db.GetParameterValue(GetGetIPOIOrderList, "@iseligibleRMSDebit").ToString());
              

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs:GetIPOIOrderList(int orderId)");
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
            Database db;
            DbCommand CreateIPOBidOrderCmd;
            DataSet dsIssueBidList = new DataSet();
            int result = 0;
            try
            {
                dsIssueBidList.Tables.Add(dtIPOBidList.Copy());
                dsIssueBidList.DataSetName = "IssueBidsDS";
                dsIssueBidList.Tables[0].TableName = "IssueBidsDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateIPOBidOrderCmd = db.GetStoredProcCommand("SPROC_ONL_UpdateIPOBidOrder");
                db.AddInParameter(CreateIPOBidOrderCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@XMLIPOBids", DbType.Xml, dsIssueBidList.GetXml().ToString());
                db.AddInParameter(CreateIPOBidOrderCmd, "@OrderId", DbType.Int32, orderId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@diffrentcetAmout", DbType.Double, differentialAmt);
                if (db.ExecuteNonQuery(CreateIPOBidOrderCmd) != 0)
                    result = 1;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs:UpdateIPOBidOrderDetails( int userId, DataTable dtIPOBidList,int orderId)");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = dtIPOBidList;
                objects[2] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;

        }

    }

}
