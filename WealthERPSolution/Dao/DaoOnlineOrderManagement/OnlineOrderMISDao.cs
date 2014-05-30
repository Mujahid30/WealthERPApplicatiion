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

namespace DaoOnlineOrderManagement
{
    public class OnlineOrderMISDao
    {
        public DataSet GetOrderBookMIS(int adviserId, int AmcCode, string OrderStatus, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsOrderBookMIS;
            Database db;
            DbCommand GetOrderBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBookMISCmd = db.GetStoredProcCommand("SPROC_Onl_GetAdviserOrderBook");
                db.AddInParameter(GetOrderBookMISCmd, "@A_AdviserId", DbType.Int32, adviserId);
                if (AmcCode != 0)
                    db.AddInParameter(GetOrderBookMISCmd, "@AMC", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(GetOrderBookMISCmd, "@AMC", DbType.Int32, 0);
                if (OrderStatus != "0")
                    db.AddInParameter(GetOrderBookMISCmd, "@Status", DbType.String, OrderStatus);
                else
                    db.AddInParameter(GetOrderBookMISCmd, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(GetOrderBookMISCmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetOrderBookMISCmd, "@ToDate", DbType.DateTime, dtTo);
                dsOrderBookMIS = db.ExecuteDataSet(GetOrderBookMISCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetOrderBookMIS()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderBookMIS;
        }
        public DataSet GetSIPBookMIS(int adviserId, int AmcCode, string OrderStatus, int systematicId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPBookMIS;
            Database db;
            DbCommand GetSIPBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPBookMISCmd = db.GetStoredProcCommand("SPROC_Onl_GetAdviserSIPBook");
                db.AddInParameter(GetSIPBookMISCmd, "@A_AdviserId", DbType.Int32, adviserId);
                if (AmcCode != 0)
                    db.AddInParameter(GetSIPBookMISCmd, "@AMC", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@AMC", DbType.Int32, 0);
                if (OrderStatus != "0")
                    db.AddInParameter(GetSIPBookMISCmd, "@Status", DbType.String, OrderStatus);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@Status", DbType.String, DBNull.Value);
                if (systematicId != 0)
                    db.AddInParameter(GetSIPBookMISCmd, "@systematicId", DbType.Int32, systematicId);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@systematicId", DbType.Int32, 0);
                db.AddInParameter(GetSIPBookMISCmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetSIPBookMISCmd, "@ToDate", DbType.DateTime, dtTo);
                dsSIPBookMIS = db.ExecuteDataSet(GetSIPBookMISCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetSIPBookMIS()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSIPBookMIS;
        }
        public DataSet GetSIPSummaryBookMIS(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPSummaryBookMIS;
            Database db;
            DbCommand GetSIPSummaryBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPSummaryBookMISCmd = db.GetStoredProcCommand("SPROC_ONL_GetAdviserSIPSummaryBookDet");
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@A_AdviserId", DbType.Int32, adviserId);
                if (AmcCode != 0)
                    db.AddInParameter(GetSIPSummaryBookMISCmd, "@AMC", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(GetSIPSummaryBookMISCmd, "@AMC", DbType.Int32, 0);
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@ToDate", DbType.DateTime, dtTo);
                dsSIPSummaryBookMIS = db.ExecuteDataSet(GetSIPSummaryBookMISCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetSIPSummaryBookMIS()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSIPSummaryBookMIS;
        }
        public DataSet GetSchemeMIS(string Assettype, int Onlinetype,string Status)
        {
            DataSet dsSchemeMIS;
            Database db;
            DbCommand GetSchemeMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeMISCmd = db.GetStoredProcCommand("SPROC_GetProductAMCSchemePlanDetails");
                //if (Assettype == "All")
                //{
                //    db.AddInParameter(GetSchemeMISCmd, "@assettype", DbType.String, null);
                //}
                //else
                //{
                //    db.AddInParameter(GetSchemeMISCmd, "@assettype", DbType.String, Assettype);
                //}
                db.AddInParameter(GetSchemeMISCmd, "@assettype", DbType.String, Assettype);
                if (Onlinetype ==2)
                {
                    db.AddInParameter(GetSchemeMISCmd, "@onlinetype", DbType.Int32, Onlinetype);

                }
                else
                {
                    db.AddInParameter(GetSchemeMISCmd, "@onlinetype", DbType.Int32, Onlinetype);

                }
                //if (Status == "All")
                //{
                    db.AddInParameter(GetSchemeMISCmd, "@status", DbType.String, Status);

                //}
                //else
                //{
                //    db.AddInParameter(GetSchemeMISCmd, "@status", DbType.String, Status);

                //}
                dsSchemeMIS = db.ExecuteDataSet(GetSchemeMISCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeMIS;
        }
        public DataTable GetAdviserCustomerTransaction(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo, int PageSize, int CurrentPage, string CustomerNamefilter,string custCode,string panNo,string folioNo,string schemeName,string type,string dividentType,string fundName,int orderNo, out int RowCount)
        {
            DataTable dtGetAdviserCustomerTransaction;
            Database db;
            DataSet dsGetAdviserCustomerTransaction;
            DbCommand GetGetAdviserCustomerTransaction;
            RowCount = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetAdviserCustomerTransaction = db.GetStoredProcCommand("SPROC_GetAdviserTransactionList");
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@A_AdviserId", DbType.Int32, adviserId);
                if (AmcCode != 0)
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@AMC", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@AMC", DbType.Int32, 0);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@ToDate", DbType.DateTime, dtTo);
                if(custCode !="")
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustCode", DbType.String, custCode);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustCode", DbType.String, DBNull.Value);
                if(panNo !="")
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@Panno", DbType.String, panNo);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@Panno", DbType.String, DBNull.Value);
                if(folioNo !="")
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@FolioNo", DbType.String, folioNo);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@FolioNo", DbType.String, DBNull.Value);
                if(schemeName !="")
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@SchemeName", DbType.String, schemeName);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@SchemeName", DbType.String, DBNull.Value);
                if (type != "")
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@Type", DbType.String, type);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@Type", DbType.String, DBNull.Value);
                if (dividentType != "")
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@DividentType", DbType.String, dividentType);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@DividentType", DbType.String, DBNull.Value);
                if (fundName != "")
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@FundName", DbType.String, fundName);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@FundName", DbType.String, DBNull.Value);
                //if (orderNo != 0)
                //    db.AddInParameter(GetGetAdviserCustomerTransaction, "@OrderNo", DbType.Int32, orderNo);
                //else
                //    db.AddInParameter(GetGetAdviserCustomerTransaction, "@OrderNo", DbType.Int32, DBNull.Value);
                if(CustomerNamefilter !="")
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustomerNameFilter", DbType.String, CustomerNamefilter);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustomerNameFilter", DbType.String, DBNull.Value);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@PageSize", DbType.Int32, PageSize);
                db.AddOutParameter(GetGetAdviserCustomerTransaction, "@RowCount", DbType.Int32, 0);
                GetGetAdviserCustomerTransaction.CommandTimeout = 60 * 60;
                dsGetAdviserCustomerTransaction = db.ExecuteDataSet(GetGetAdviserCustomerTransaction);
                dtGetAdviserCustomerTransaction = dsGetAdviserCustomerTransaction.Tables[0];
                if (db.ExecuteNonQuery(GetGetAdviserCustomerTransaction) != 0)
                {
                    if (db.GetParameterValue(GetGetAdviserCustomerTransaction, "RowCount").ToString() != "")
                    {
                        RowCount = Convert.ToInt32(db.GetParameterValue(GetGetAdviserCustomerTransaction, "RowCount").ToString());
                    }
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
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:Getproductcode()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetAdviserCustomerTransaction;
        }
        public DataTable GetAdviserCustomerTransactionsBookSIP(int AdviserID, int customerId, int SystematicId, int IsSourceAA, int AccountId, int SchemePlanCode)
        {
            DataTable dtGetAdviserCustomerTransactionsBookSIP;
            Database db;
            DataSet dsGetAdviserCustomerTransactionsBookSIP;
            DbCommand GetAdviserCustomerTransactionsBookSIPcmd;
             try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserCustomerTransactionsBookSIPcmd = db.GetStoredProcCommand("SPROC_ONL_GetAdviserCustomerMFSIPTransactionsBook");
                if (AdviserID != 0)
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@AdviserID", DbType.Int32, AdviserID);
                }
                else
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@AdviserID", DbType.Int32, DBNull.Value);
                }
                if (customerId != 0)
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@CustomerId", DbType.Int32, customerId);
                }
                else
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@CustomerId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@AccountId", DbType.Int32, @AccountId);
                db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@SchemePlanCode", DbType.Int32, @SchemePlanCode);
                db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@IsSourceAA", DbType.Int32, IsSourceAA);

                if (SchemePlanCode != 0)
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@SystemeticId", DbType.Int32, SystematicId);
                }
                else
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@SystemeticId", DbType.Int32, DBNull.Value);
                }
                GetAdviserCustomerTransactionsBookSIPcmd.CommandTimeout = 60 * 60;
                dsGetAdviserCustomerTransactionsBookSIP = db.ExecuteDataSet(GetAdviserCustomerTransactionsBookSIPcmd);
                 dtGetAdviserCustomerTransactionsBookSIP=dsGetAdviserCustomerTransactionsBookSIP.Tables[0];
        }
             catch (BaseApplicationException Ex)
             {
                 throw Ex;
             }
             catch (Exception Ex)
             {
                 BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                 NameValueCollection FunctionInfo = new NameValueCollection();
                 FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:Getproductcode()");
                 exBase.AdditionalInformation = FunctionInfo;
                 ExceptionManager.Publish(exBase);
                 throw exBase;
             }
             return dtGetAdviserCustomerTransactionsBookSIP;
        }
    }
}
