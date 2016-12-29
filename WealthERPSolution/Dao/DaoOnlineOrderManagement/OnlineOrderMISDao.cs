using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoOnlineOrderManagement
{
    public class OnlineOrderMISDao
    {
        public DataSet GetOrderBookMIS(int adviserId, int AmcCode, string OrderStatus, DateTime dtFrom, DateTime dtTo, int orderNo, string folioNo, int Isdemat)
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
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(GetOrderBookMISCmd, "@Fromdate", DbType.DateTime, dtFrom);
                else
                    db.AddInParameter(GetOrderBookMISCmd, "@Fromdate", DbType.DateTime, DBNull.Value);
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(GetOrderBookMISCmd, "@ToDate", DbType.DateTime, dtTo);
                else
                    db.AddInParameter(GetOrderBookMISCmd, "@ToDate", DbType.DateTime, DBNull.Value);
                if (orderNo != 0)
                    db.AddInParameter(GetOrderBookMISCmd, "@orderNo", DbType.Int32, orderNo);
                else
                    db.AddInParameter(GetOrderBookMISCmd, "@orderNo", DbType.Int32, 0);
                db.AddInParameter(GetOrderBookMISCmd, "@folioNo", DbType.String, folioNo);
                db.AddInParameter(GetOrderBookMISCmd, "@Isdemat", DbType.Int32, Isdemat);

                GetOrderBookMISCmd.CommandTimeout = 60 * 60;
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
        public DataSet GetSIPBookMIS(int adviserId, int AmcCode, string OrderStatus, int systematicId, DateTime dtFrom, DateTime dtTo, int orderId, string folioNo,string Mode)
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
                if (OrderStatus != "0" && OrderStatus!="" )
                    db.AddInParameter(GetSIPBookMISCmd, "@Status", DbType.String, OrderStatus);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@Status", DbType.String, DBNull.Value);
                if (systematicId != 0)
                    db.AddInParameter(GetSIPBookMISCmd, "@systematicId", DbType.Int32, systematicId);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@systematicId", DbType.Int32, 0);
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(GetSIPBookMISCmd, "@Fromdate", DbType.DateTime, dtFrom);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@Fromdate", DbType.DateTime, DBNull.Value);
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(GetSIPBookMISCmd, "@ToDate", DbType.DateTime, dtTo);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@ToDate", DbType.DateTime, DBNull.Value);
                if (orderId != 0)
                    db.AddInParameter(GetSIPBookMISCmd, "@orderNo", DbType.Int32, orderId);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@orderNo", DbType.Int32, 0);
                db.AddInParameter(GetSIPBookMISCmd, "@folioNo", DbType.String, folioNo);
              
                    db.AddInParameter(GetSIPBookMISCmd, "@Mode", DbType.String, Mode);
                

                GetSIPBookMISCmd.CommandTimeout = 60 * 60;
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
        public DataSet GetSIPSummaryBookMIS(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo, int searchType, int statusType, string systematicType, string SIPMode,string Mode)
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
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@Type", DbType.Int32, searchType);
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@StatusType", DbType.Int32, statusType);
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@systematicType", DbType.String, systematicType);
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@SIPMode", DbType.String, SIPMode);     
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@Mode", DbType.Int32, Mode);
                
                GetSIPSummaryBookMISCmd.CommandTimeout = 60 * 60;
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
        public DataSet GetSchemeMIS(string Assettype, int Onlinetype, string Status, int IsDemat)
        {
            DataSet dsSchemeMIS;
            Database db;
            DbCommand GetSchemeMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeMISCmd = db.GetStoredProcCommand("SPROC_GetProductAMCSchemePlanDetails");

                db.AddInParameter(GetSchemeMISCmd, "@assettype", DbType.String, Assettype);
                if (Onlinetype == 2)
                {
                    db.AddInParameter(GetSchemeMISCmd, "@onlinetype", DbType.Int32, Onlinetype);

                }
                else
                {
                    db.AddInParameter(GetSchemeMISCmd, "@onlinetype", DbType.Int32, Onlinetype);

                }

                db.AddInParameter(GetSchemeMISCmd, "@status", DbType.String, Status);


                db.AddInParameter(GetSchemeMISCmd, "@IsDemat", DbType.Int32, IsDemat);

                GetSchemeMISCmd.CommandTimeout = 60 * 60;
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
        public DataTable GetAdviserCustomerTransaction(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo, int PageSize, int CurrentPage, string CustomerNamefilter, string custCode, string panNo, string folioNo, string schemeName, string type, string dividentType, string fundName, int orderNo, out int RowCount, bool Isdemat, int schemePlanCode, int customerid)
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
                if (custCode != "")
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustCode", DbType.String, custCode);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustCode", DbType.String, DBNull.Value);
                if (panNo != "")
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@Panno", DbType.String, panNo);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@Panno", DbType.String, DBNull.Value);
                if (folioNo != "")
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@FolioNo", DbType.String, folioNo);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@FolioNo", DbType.String, DBNull.Value);
                if (schemeName != "")
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
                if (CustomerNamefilter != "")
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustomerNameFilter", DbType.String, CustomerNamefilter);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@CustomerNameFilter", DbType.String, DBNull.Value);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@PageSize", DbType.Int32, PageSize);
                db.AddOutParameter(GetGetAdviserCustomerTransaction, "@RowCount", DbType.Int32, 0);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@Isdemat", DbType.Boolean, Isdemat);
                if (schemePlanCode != 0)
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@SchemeCode", DbType.Int32, schemePlanCode);
                else
                    db.AddInParameter(GetGetAdviserCustomerTransaction, "@SchemeCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(GetGetAdviserCustomerTransaction, "@Customerid", DbType.Int32, customerid);

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
        public DataTable GetAdviserCustomerTransactionsBookSIP(int AdviserID, int customerId, int SystematicId, int IsSourceAA, int AccountId, int SchemePlanCode, int requestId)
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
                if (requestId != 0)
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@RequestId", DbType.Int32, requestId);
                }
                else
                {
                    db.AddInParameter(GetAdviserCustomerTransactionsBookSIPcmd, "@RequestId", DbType.Int32, DBNull.Value);
                }
                GetAdviserCustomerTransactionsBookSIPcmd.CommandTimeout = 60 * 60;
                dsGetAdviserCustomerTransactionsBookSIP = db.ExecuteDataSet(GetAdviserCustomerTransactionsBookSIPcmd);
                dtGetAdviserCustomerTransactionsBookSIP = dsGetAdviserCustomerTransactionsBookSIP.Tables[0];
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
        public DataTable GetMFHolding()
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdGetMFHolding;
            DataTable dtGetMFHolding;
            DataSet dsGetMFHolding = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetMFHolding = db.GetStoredProcCommand("SPROC_GetReconRequestList");
                dsGetMFHolding = db.ExecuteDataSet(cmdGetMFHolding);
                dtGetMFHolding = dsGetMFHolding.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMFHolding;
        }
        public DataTable GetMFHoldingRecon(int requestNo)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdGetMFHoldingRecon;
            DataTable dtGetMFHoldingRecon;
            DataSet dsGetMFHoldingRecon = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetMFHoldingRecon = db.GetStoredProcCommand("SPROC_GetAdviserMFRecon");
                db.AddInParameter(cmdGetMFHoldingRecon, "@ReqId", DbType.Int32, requestNo);
                dsGetMFHoldingRecon = db.ExecuteDataSet(cmdGetMFHoldingRecon);
                cmdGetMFHoldingRecon.CommandTimeout = 60 * 60;
                dtGetMFHoldingRecon = dsGetMFHoldingRecon.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMFHoldingRecon;
        }
        public DataTable GetMFHoldingReconAfterSync(int requestNo, DateTime toDate, int typeFliter, int differentFliter, int AMC,bool isSync)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdGetMFHoldingRecon;
            DataTable dtGetMFHoldingRecon;
            DataSet dsGetMFHoldingRecon = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetMFHoldingRecon = db.GetStoredProcCommand("SPROC_SyncDataForMFHoldingRecon");
                db.AddInParameter(cmdGetMFHoldingRecon, "@ReqId", DbType.Int32, requestNo);
                db.AddInParameter(cmdGetMFHoldingRecon, "@ToDate", DbType.Date, toDate);
                db.AddInParameter(cmdGetMFHoldingRecon, "@TypeFliter", DbType.Int32, typeFliter);
                db.AddInParameter(cmdGetMFHoldingRecon, "@DifferentFliter", DbType.Int32, differentFliter);
                db.AddInParameter(cmdGetMFHoldingRecon, "@AMC", DbType.Int32, AMC);
                db.AddInParameter(cmdGetMFHoldingRecon, "@IsSync", DbType.Boolean, isSync);
                cmdGetMFHoldingRecon.CommandTimeout = 60 * 60;
                dsGetMFHoldingRecon = db.ExecuteDataSet(cmdGetMFHoldingRecon);
                dtGetMFHoldingRecon = dsGetMFHoldingRecon.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMFHoldingRecon;
        }
        public DataTable GetAMCList(int requestId)
        {
            DataSet dsGetAMCList;
            DataTable dtGetAMCList;
            Database db;
            DbCommand cmdGetAMCList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAMCList = db.GetStoredProcCommand("SPROC_GetAMCRequestWise");
                db.AddInParameter(cmdGetAMCList, "@requestId", DbType.Int32, requestId);
                dsGetAMCList = db.ExecuteDataSet(cmdGetAMCList);
                dtGetAMCList = dsGetAMCList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAMCList;
        }
        public bool UpdateOrderReverse(int orderid, int userID)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateOrderReverseCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateOrderReverseCmd = db.GetStoredProcCommand("SPROC_CreateOrderReverse");
                db.AddInParameter(UpdateOrderReverseCmd, "@orderId", DbType.Int32, orderid);
                db.AddInParameter(UpdateOrderReverseCmd, "@userid", DbType.Int32, userID);
                db.ExecuteNonQuery(UpdateOrderReverseCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool updateSystemMFHoldingRecon(int requestNo, DateTime toDate)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdGetMFHoldingRecon;
            bool result;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetMFHoldingRecon = db.GetStoredProcCommand("SPROC_SyncSystemDataForMFHoldingRecon");
                db.AddInParameter(cmdGetMFHoldingRecon, "@ReqId", DbType.Int32, requestNo);
                db.AddInParameter(cmdGetMFHoldingRecon, "@ToDate", DbType.Date, toDate);
                result = int.Parse(db.ExecuteNonQuery(cmdGetMFHoldingRecon).ToString()) > 1 ? true :  false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
    }
}
