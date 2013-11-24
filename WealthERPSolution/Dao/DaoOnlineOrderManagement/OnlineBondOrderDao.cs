using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoOnlineOrderManagemnet;

namespace DaoOnlineOrderManagement
{
    public class OnlineBondOrderDao : OnlineOrderDao
    {
        public DataSet GetBindIssuerList()
        {
            Database db;
            DbCommand cmdGetLookupDataForReceivable;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLookupDataForReceivable = db.GetStoredProcCommand("SPROC_ONL_BindIssuerList");
                //db.AddInParameter(cmdGetLookupDataForReceivable, "@ReportType", DbType.Int32, adviserId);
                //db.AddInParameter(cmdGetLookupDataForReceivable, "@SeriesId", DbType.String, structureId);
                ds = db.ExecuteDataSet(cmdGetLookupDataForReceivable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetBindIssuerList()");
                object[] objects = new object[1];               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetAdviserIssuerList(int adviserId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetIssuerlist");
                db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, adviserId);
                //db.AddInParameter(cmdGetCommissionStructureRules, "@IssuerId", DbType.String, IssuerId);
                //db.AddInParameter(cmdGetCommissionStructureRules, "@SeriesId", DbType.String, structureId);
                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetAdviserIssuerList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public DataSet GetLiveBondTransaction(int SeriesId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetLiveBondTransaction");
                db.AddInParameter(cmdGetCommissionStructureRules, "@SeriesId", DbType.Int32, SeriesId);
                //if (orderId!=0)
                //    db.AddInParameter(cmdGetCommissionStructureRules, "@orderId", DbType.Int32, orderId);
                //else
                //    db.AddInParameter(cmdGetCommissionStructureRules, "@orderId", DbType.Int32, 0);
                //db.AddInParameter(cmdGetCommissionStructureRules, "@ReportType", DbType.Int32, adviserId);
                //db.AddInParameter(cmdGetCommissionStructureRules, "@SeriesId", DbType.String, structureId);
                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetLiveBondTransaction()");
                object[] objects = new object[1];               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetLiveBondTransactionList()
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetLiveBondTransactionList");               
                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetLiveBondTransactionList(int adviserId)");
                object[] objects = new object[1];                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public DataSet GetLookupDataForReceivableSetUP(int adviserId, string structureId)
        {
            Database db;
            DbCommand cmdGetLookupDataForReceivable;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLookupDataForReceivable = db.GetStoredProcCommand("SPROC_OnlineBondManagement");
                db.AddInParameter(cmdGetLookupDataForReceivable, "@ReportType", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetLookupDataForReceivable, "@SeriesId", DbType.String, structureId);
                ds = db.ExecuteDataSet(cmdGetLookupDataForReceivable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetAdviserCommissionStructureRules(int adviserId, string structureId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_OnlineBondManagement");
                //db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@ReportType", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@SeriesId", DbType.String, structureId);
                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public DataSet GetIssueDetail(int IssuerId)
        {
            Database db;
            DbCommand cmdGetIssueDetail;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetIssueDetail = db.GetStoredProcCommand("SPROC_ONL_GetIssueDetail");
                db.AddInParameter(cmdGetIssueDetail, "@IssuerId", DbType.Int32, IssuerId);
                ds = db.ExecuteDataSet(cmdGetIssueDetail);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetIssueDetail()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public IDictionary<string, string> UpdateOnlineBondTransact(DataTable BondORder, int adviserId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            Database db;
            DbCommand cmdOnlineBondTransact;
            //bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOnlineBondTransact = db.GetStoredProcCommand("SPROC_ONL_OnlineBondTransaction");

                DataSet ds = new DataSet();
                ds.Tables.Add(BondORder);

                String sb;
                sb = ds.GetXml().ToString();

                db.AddInParameter(cmdOnlineBondTransact, "@xmlBondsOrder", DbType.Xml, sb);
                db.AddInParameter(cmdOnlineBondTransact, "@AdviserId", DbType.Int32,adviserId);
                db.AddOutParameter(cmdOnlineBondTransact, "@Order_Id", DbType.Int32, 10000);
                //db.AddInParameter(cmdOnlineBondTransact, "@CustomerId", DbType.String, BondORder.CustomerId);
                //db.AddInParameter(cmdOnlineBondTransact, "@PFISM_SchemeId", DbType.Int32, BondORder.PFISM_SchemeId);
                //db.AddInParameter(cmdOnlineBondTransact, "@PFISD_SeriesId", DbType.Int32, BondORder.PFISD_SeriesId);
                //db.AddInParameter(cmdOnlineBondTransact, "@PFIIM_IssuerId", DbType.String, BondORder.PFIIM_IssuerId);
                //db.AddInParameter(cmdOnlineBondTransact, "@Qty", DbType.Int32, BondORder.Qty);
                //db.AddInParameter(cmdOnlineBondTransact, "@Amount", DbType.Double, BondORder.Amount);
                //db.AddInParameter(cmdOnlineBondTransact, "@BankAccid", DbType.Double, BondORder.BankAccid);
                //db.ExecuteNonQuery(cmdOnlineBondTransact);
                //result = true;
                
                if (db.ExecuteNonQuery(cmdOnlineBondTransact) != 0)
                {
                    OrderIds.Add("OrderId", db.GetParameterValue(cmdOnlineBondTransact, "Order_Id").ToString());
                
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
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:UpdateOnlineBondTransact(VoOnlineOrderManagemnet.OnlineBondOrderVo BondORder)");
                object[] objects = new object[1];
                objects[0] = BondORder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return OrderIds;
        }

        public DataSet GetOrderBondsBook(int input, string CustId)
        {
            DataSet dsOrderBondsBook;
            Database db;
            DbCommand GetOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBondsBookcmd = db.GetStoredProcCommand("SPROC_OnlineBondManagement");
                db.AddInParameter(GetOrderBondsBookcmd, "@ReportType", DbType.Int32, input);
                db.AddInParameter(GetOrderBondsBookcmd, "@SeriesId", DbType.String, CustId);
                dsOrderBondsBook = db.ExecuteDataSet(GetOrderBondsBookcmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetOrderBondsBook(int input)");
                object[] objects = new object[1];
                objects[0] = input;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderBondsBook;
        }
        public DataSet GetOrderBondBook(int customerId, string status, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsOrderBondsBook;
            Database db;
            DbCommand GetOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBondsBookcmd = db.GetStoredProcCommand("SPROC_ONL_GetBondOrderBook");
                db.AddInParameter(GetOrderBondsBookcmd, "@customerId", DbType.Int32, customerId);
                if (status != "0")
                    db.AddInParameter(GetOrderBondsBookcmd, "@Status", DbType.String, status);
                else
                db.AddInParameter(GetOrderBondsBookcmd, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(GetOrderBondsBookcmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetOrderBondsBookcmd, "@ToDate", DbType.DateTime, dtTo);
                dsOrderBondsBook = db.ExecuteDataSet(GetOrderBondsBookcmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetOrderBondBook()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderBondsBook;
        }

        public DataSet GetOrderBondSubBook(int customerId,int IssuerId,int orderid)
        {
            DataSet dsOrderBondssubBook;
            Database db;
            DbCommand GetOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBondsBookcmd = db.GetStoredProcCommand("SPROC_ONL_GetBondOrdersubBook");
                db.AddInParameter(GetOrderBondsBookcmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetOrderBondsBookcmd, "@IssuerId", DbType.Int32, IssuerId);
                db.AddInParameter(GetOrderBondsBookcmd, "@orderId", DbType.Int32, orderid);
                dsOrderBondssubBook = db.ExecuteDataSet(GetOrderBondsBookcmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetOrderBondSubBook()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderBondssubBook;
        }

        public DataSet GetNomineeJointHolder(int customerId)
        {
            DataSet dsGetNomineeJointHolder;
            Database db;
            DbCommand GetNomineeJointHoldercmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNomineeJointHoldercmd = db.GetStoredProcCommand("SPROC_ONL_GetNomineeJointHolder");
                db.AddInParameter(GetNomineeJointHoldercmd, "@customerId", DbType.Int32, customerId);
                dsGetNomineeJointHolder = db.ExecuteDataSet(GetNomineeJointHoldercmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetNomineeJointHolder()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetNomineeJointHolder;
        }
        public void CancelBondsBookOrder(string id)
        {
            Database db;
            DbCommand CancelOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CancelOrderBondsBookcmd = db.GetStoredProcCommand("");
                db.AddInParameter(CancelOrderBondsBookcmd, "", DbType.String, id);
                db.ExecuteDataSet(CancelOrderBondsBookcmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:CancelBondsBookOrder(string id)");
                object[] objects = new object[1];
                objects[0] = id;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public string GetMAXTransactNO()
        {
            Database db;
            string strMaxNoDB = string.Empty;
            DbCommand CancelOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CancelOrderBondsBookcmd = db.GetStoredProcCommand("SP");

                strMaxNoDB=db.ExecuteScalar(CancelOrderBondsBookcmd).ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:CancelBondsBookOrder(string id)");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return strMaxNoDB;
        }

       public int GetApplicationNumber()
        {
            DataSet dsApplicationNumber;
            DataTable dtApplicationNumber;
            int ApplicationNumber = 0;
            Database db;
            DbCommand getSchemeSwitchcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeSwitchcmd = db.GetStoredProcCommand("SP_GetApplicationNumber");
                dsApplicationNumber = db.ExecuteDataSet(getSchemeSwitchcmd);
                dtApplicationNumber = dsApplicationNumber.Tables[0];
                if (dtApplicationNumber.Rows.Count > 0)
                    ApplicationNumber = int.Parse(dtApplicationNumber.Rows[0]["CO_ApplicationNo"].ToString());
                else
                    ApplicationNumber = 999;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetOrderNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ApplicationNumber;
        }
       public DataSet GetNCDTransactOrder(int orderId, int IssuerId)
       {
           Database db;
           DataSet GetNCDTransactOrderDs;
           DbCommand GetNCDTransactOrderCmd;
           try
           {

               db = DatabaseFactory.CreateDatabase("wealtherp");
               GetNCDTransactOrderCmd = db.GetStoredProcCommand("SPROC_ONL_GetLiveBookBondTransaction");
               db.AddInParameter(GetNCDTransactOrderCmd, "@orderId", DbType.Int32, orderId);
               db.AddInParameter(GetNCDTransactOrderCmd, "@IssuerId", DbType.Int32, IssuerId);
               GetNCDTransactOrderDs = db.ExecuteDataSet(GetNCDTransactOrderCmd);
              
           }
           catch (BaseApplicationException Ex)
           {
               throw Ex;
           }
           catch (Exception Ex)
           {
               BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
               NameValueCollection FunctionInfo = new NameValueCollection();

               FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetNCDTransactOrder()");


               object[] objects = new object[1];
               objects[0] = orderId;

               FunctionInfo = exBase.AddObject(FunctionInfo, objects);
               exBase.AdditionalInformation = FunctionInfo;
               ExceptionManager.Publish(exBase);
               throw exBase;

           }
           return GetNCDTransactOrderDs;

       }
    }
}
