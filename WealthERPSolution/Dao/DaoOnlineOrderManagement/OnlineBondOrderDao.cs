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

        public DataSet GetAdviserIssuerList(int adviserId, string IssuerId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetIssuerlist");
                db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@IssuerId", DbType.String, IssuerId);
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
        public DataSet GetLiveBondTransaction(string SeriesId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetLiveBondTransaction");
                db.AddInParameter(cmdGetCommissionStructureRules, "@SeriesId", DbType.String, SeriesId);
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
        public DataSet GetIssueDetail(string IssuerId)
        {
            Database db;
            DbCommand cmdGetIssueDetail;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetIssueDetail = db.GetStoredProcCommand("SPROC_ONL_GetIssueDetail");
                db.AddInParameter(cmdGetIssueDetail, "@IssuerId", DbType.String, IssuerId);
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

        public bool UpdateOnlineBondTransact(DataTable BondORder)
        {
            Database db;
            DbCommand cmdOnlineBondTransact;
            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOnlineBondTransact = db.GetStoredProcCommand("SPROC_ONL_OnlineBondTransaction");

                DataSet ds = new DataSet();
                ds.Tables.Add(BondORder);

                String sb;
                sb = ds.GetXml().ToString();

                db.AddInParameter(cmdOnlineBondTransact, "@xmlBondsOrder", DbType.Xml, sb);

                //db.AddInParameter(cmdOnlineBondTransact, "@CustomerId", DbType.String, BondORder.CustomerId);
                //db.AddInParameter(cmdOnlineBondTransact, "@PFISM_SchemeId", DbType.Int32, BondORder.PFISM_SchemeId);
                //db.AddInParameter(cmdOnlineBondTransact, "@PFISD_SeriesId", DbType.Int32, BondORder.PFISD_SeriesId);
                //db.AddInParameter(cmdOnlineBondTransact, "@PFIIM_IssuerId", DbType.String, BondORder.PFIIM_IssuerId);
                //db.AddInParameter(cmdOnlineBondTransact, "@Qty", DbType.Int32, BondORder.Qty);
                //db.AddInParameter(cmdOnlineBondTransact, "@Amount", DbType.Double, BondORder.Amount);
                //db.AddInParameter(cmdOnlineBondTransact, "@BankAccid", DbType.Double, BondORder.BankAccid);
                db.ExecuteNonQuery(cmdOnlineBondTransact);
                result = true;
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
            return result;
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
        public DataSet GetOrderBondBook(int customerId)
        {
            DataSet dsOrderBondsBook;
            Database db;
            DbCommand GetOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBondsBookcmd = db.GetStoredProcCommand("SPROC_ONL_GetBondOrderBook");
                db.AddInParameter(GetOrderBondsBookcmd, "@customerId", DbType.Int32, customerId);
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

        

    }
}
