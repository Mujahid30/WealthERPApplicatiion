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
        public DataSet GetLookupDataForReceivableSetUP(int adviserId, int structureId)
        {
            Database db;
            DbCommand cmdGetLookupDataForReceivable;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp_SBI");
                cmdGetLookupDataForReceivable = db.GetStoredProcCommand("SPROC_OnlineBondManagement");
                db.AddInParameter(cmdGetLookupDataForReceivable, "@ReportType", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetLookupDataForReceivable, "@SeriesId", DbType.Int32, structureId);
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
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetAdviserCommissionStructureRules(int adviserId, int structureId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_OnlineBondManagement");
                db.AddInParameter(cmdGetCommissionStructureRules, "@ReportType", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@SeriesId", DbType.Int32, structureId);
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

        public DataSet GetOrderBondsBook(int input)
        {
            DataSet dsOrderBondsBook;
            Database db;
            DbCommand GetOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBondsBookcmd = db.GetStoredProcCommand("SPROC_OnlineBondManagement");
                db.AddInParameter(GetOrderBondsBookcmd, "@ReportType", DbType.Int32, input);
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
    }
}
