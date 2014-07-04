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

        public string GetCutOFFTimeForCurent(int orderId)
        {
            Database db;
            DbCommand dbCommand;
            string cutOffTime = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetCutOFFTimeForCurent");
                db.AddInParameter(dbCommand, "@OrderId", DbType.Int32, orderId);
                db.AddOutParameter(dbCommand, "@IssueTimeType", DbType.Int32, 0);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                //if(ds.Tables[1].

                cutOffTime = db.GetParameterValue(dbCommand, "@IssueTimeType").ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:ChekSeriesSequence()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return cutOffTime;
        }
        public void GetCustomerCat(int issueId, int customerId, int adviserId, double amt, ref string catName, ref int issueDetId, ref int categoryId, ref string Description)
        {
            Database db;
            DbCommand dbCommand;
            DataSet ds = null;
           
            try
            {
   
  

                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SP_GetCustomerCat");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@amt", DbType.Double, amt);
                db.AddOutParameter(dbCommand, "@catName", DbType.String, 500);
                db.AddOutParameter(dbCommand, "@OrdDetID", DbType.Int32, 0);
                db.AddOutParameter(dbCommand, "@categoryId", DbType.Int32, 0);
                db.AddOutParameter(dbCommand, "@description", DbType.String, 500);
                db.AddInParameter(dbCommand, "@issueDetId", DbType.Int32, issueDetId);

                ds = db.ExecuteDataSet(dbCommand);
                Description = db.GetParameterValue(dbCommand, "description").ToString();
                catName = db.GetParameterValue(dbCommand, "catName").ToString();
                issueDetId = Convert.ToInt32(db.GetParameterValue(dbCommand, "OrdDetID").ToString());
                categoryId = Convert.ToInt32(db.GetParameterValue(dbCommand, "categoryId").ToString());
                
                //catName = db.ExecuteScalar(dbCommand).ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetExtractStepCode()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            // return stepCode;
        }

        public DataSet GetAdviserIssuerList(int adviserId, int issueId, int type, int customerId, int isAdminRequest, int CustomerSubType)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetIssuerlist");
                db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, adviserId);
                if (issueId != 0)
                    db.AddInParameter(cmdGetCommissionStructureRules, "@IssueId", DbType.Int32, issueId);
                else
                    db.AddInParameter(cmdGetCommissionStructureRules, "@IssueId", DbType.Int32, 0);

                db.AddInParameter(cmdGetCommissionStructureRules, "@type", DbType.Int32, type);


             //  db.AddInParameter(cmdGetCommissionStructureRules, "@type", DbType.Int32, type);

                db.AddInParameter(cmdGetCommissionStructureRules, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@CustomerSubType", DbType.Int32, CustomerSubType);

              db.AddInParameter(cmdGetCommissionStructureRules, "@IsAdminRequest", DbType.Int16, isAdminRequest);


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
        public DataSet GetLiveBondTransaction(int SeriesId, int customerId, int CustomerSubType)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetLiveBondTransaction");

                db.AddInParameter(cmdGetCommissionStructureRules, "@IssueId", DbType.Int32, SeriesId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@customerSubType", DbType.Int32, CustomerSubType);

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

        public DataSet GetLiveBondTransactionList(int Adviserid)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_ONL_GetLiveBondTransactionList");
                db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, Adviserid);
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
        public DataSet GetIssueDetail(int IssuerId, int CustomerId)
        {
            Database db;
            DbCommand cmdGetIssueDetail;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetIssueDetail = db.GetStoredProcCommand("SPROC_ONL_GetIssueDetail");
                db.AddInParameter(cmdGetIssueDetail, "@IssuerId", DbType.Int32, IssuerId);
                db.AddInParameter(cmdGetIssueDetail, "@CustomerId", DbType.Int32, CustomerId);

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

        public IDictionary<string, string> UpdateOnlineBondTransact(DataTable BondORder, int adviserId, int IssuerId)
        {
            //List<int> orderIds = new List<int>();
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            int orderId = 0;
            int applicationNo;
            Database db;
            DbCommand cmdOnlineBondTransact;
            //bool result = false;

            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = BondORder.Copy();
                ds.Tables.Add(dt);
                String sb;
                sb = ds.GetXml().ToString();

                db = DatabaseFactory.CreateDatabase("wealtherp");
                //   cmdOnlineBondTransact = db.GetStoredProcCommand("SPROC_ONL_OnlineBondTransaction");
                cmdOnlineBondTransact = db.GetStoredProcCommand("SPROC_ONL_OnlineBondTransaction");
                db.AddInParameter(cmdOnlineBondTransact, "@xmlBondsOrder", DbType.Xml, sb);
                db.AddInParameter(cmdOnlineBondTransact, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdOnlineBondTransact, "@AIM_IssueId", DbType.Int32, IssuerId);
                db.AddOutParameter(cmdOnlineBondTransact, "@Order_Id", DbType.Int32, 1000000);
                db.AddOutParameter(cmdOnlineBondTransact, "@application", DbType.Int32, 1000000);
                db.AddOutParameter(cmdOnlineBondTransact, "@aplicationNoStatus", DbType.String, 10);


                if (db.ExecuteNonQuery(cmdOnlineBondTransact) != 0)
                {

                    OrderIds.Add("Order_Id", db.GetParameterValue(cmdOnlineBondTransact, "Order_Id").ToString());
                    OrderIds.Add("application", db.GetParameterValue(cmdOnlineBondTransact, "application").ToString());
                    OrderIds.Add("aplicationNoStatus", db.GetParameterValue(cmdOnlineBondTransact, "aplicationNoStatus").ToString());

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

         

        public IDictionary<string, string> UpdateTransactOrder(DataTable BondORder, OnlineBondOrderVo OnlineBondOrderVo, int adviserId, int IssuerId, int OrderId, int seriesId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            Database db;
            DbCommand cmdOnlineBondTransact;
            //bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOnlineBondTransact = db.GetStoredProcCommand("SPROC_ONL_UpdateTransactOrder");

                DataSet ds = new DataSet();
                ds.Tables.Add(BondORder);

                String sb;
                sb = ds.GetXml().ToString();

                db.AddInParameter(cmdOnlineBondTransact, "@xmlBondsOrder", DbType.Xml, sb);
                db.AddInParameter(cmdOnlineBondTransact, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdOnlineBondTransact, "@IssuerId", DbType.Int32, IssuerId);
                db.AddInParameter(cmdOnlineBondTransact, "@orderId", DbType.Int32, OrderId);
                db.AddInParameter(cmdOnlineBondTransact, "@seriesId", DbType.Int32, seriesId);
                db.AddInParameter(cmdOnlineBondTransact, "@Quantity", DbType.Int32, OnlineBondOrderVo.Qty);
                db.AddInParameter(cmdOnlineBondTransact, "@Amount", DbType.Int32, OnlineBondOrderVo.Amount);
                //db.AddInParameter(cmdOnlineBondTransact, "@tenure", DbType.Int32, DBNull.Value);
                //db.AddInParameter(cmdOnlineBondTransact, "@MatDate", DbType.DateTime, DBNull.Value);
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
        public DataSet GetOrderBondBook(int customerId,int issueId, string status, DateTime dtFrom, DateTime dtTo, int adviserId)
        {
            DataSet dsOrderBondsBook;
            Database db;
            DbCommand GetOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBondsBookcmd = db.GetStoredProcCommand("SPROC_ONL_GetBondOrderBook");
                db.AddInParameter(GetOrderBondsBookcmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetOrderBondsBookcmd, "@AIMissue", DbType.Int32, issueId);
                if (status != "0")
                    db.AddInParameter(GetOrderBondsBookcmd, "@Status", DbType.String, status);
                else
                    db.AddInParameter(GetOrderBondsBookcmd, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(GetOrderBondsBookcmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetOrderBondsBookcmd, "@ToDate", DbType.DateTime, dtTo);
                db.AddInParameter(GetOrderBondsBookcmd, "@AdviserId", DbType.Int32, adviserId);

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

        public DataSet GetOrderBondSubBook(int customerId, int IssuerId, int orderid)
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
        public bool CancelBondsBookOrder(int orderId, int is_Cancel, string remarks)
        {
            bool bResult = false;
            Database db;
            DbCommand CancelOrderBondsBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CancelOrderBondsBookcmd = db.GetStoredProcCommand("SPROC_NCDMarkAsCancel");
                //CancelOrderBondsBookcmd = db.GetStoredProcCommand("sproc_onl_CancelNCDOrder");
                db.AddInParameter(CancelOrderBondsBookcmd, "@OrderId", DbType.Int32, orderId);
                db.AddInParameter(CancelOrderBondsBookcmd, "@Remarks", DbType.String, remarks);
                db.AddInParameter(CancelOrderBondsBookcmd, "@Is_Canceled", DbType.Int32, is_Cancel);
                db.ExecuteDataSet(CancelOrderBondsBookcmd);
                if (db.ExecuteNonQuery(CancelOrderBondsBookcmd) != 0)
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
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:CancelBondsBookOrder(string id)");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
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

                strMaxNoDB = db.ExecuteScalar(CancelOrderBondsBookcmd).ToString();

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
        public DataSet GetNCDAllTransactOrder(int orderId, int IssuerId)
        {
            Database db;
            DataSet GetNCDTransactOrderDs;
            DbCommand GetNCDTransactOrderCmd;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNCDTransactOrderCmd = db.GetStoredProcCommand("SPROC_ONL_GetLiveBookAllBondTransaction");
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

                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetNCDAllTransactOrder()");


                object[] objects = new object[1];
                objects[0] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return GetNCDTransactOrderDs;

        }
        public DataTable GetNCDHoldingOrder(int customerId, int AdviserId)
        {
            DataSet dsNCDHoldingOrder;
            DataTable dtNCDHoldingOrder;
            Database db;
            DbCommand GetNCDHoldingOrdercmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //GetNCDHoldingOrdercmd = db.GetStoredProcCommand("SPROC_ONL_GetNCDHolding");
                GetNCDHoldingOrdercmd = db.GetStoredProcCommand("SPROC_Onl_GetIssueWiseHolding");
                db.AddInParameter(GetNCDHoldingOrdercmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetNCDHoldingOrdercmd, "@adviserId", DbType.Int32, AdviserId);
                //db.AddInParameter(GetNCDHoldingOrdercmd, "@fromdate", DbType.DateTime, dtFrom);
                //db.AddInParameter(GetNCDHoldingOrdercmd, "@todate", DbType.DateTime, dtTo);
                dsNCDHoldingOrder = db.ExecuteDataSet(GetNCDHoldingOrdercmd);
                dtNCDHoldingOrder = dsNCDHoldingOrder.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetNCDHoldingOrder()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDHoldingOrder;
        }
        public DataTable GetNCDHoldingSeriesOrder(int customerId, int AdviserId, int IssueId, int orderId)
        {
            DataSet dsGetNCDHoldingSeriesOrder;
            DataTable dtGetNCDHoldingSeriesOrder;
            Database db;
            DbCommand GetNCDHoldingSeriesOrdercmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNCDHoldingSeriesOrdercmd = db.GetStoredProcCommand("SPROC_ONL_GetIssueSeriesWiseNCDHolding");
                db.AddInParameter(GetNCDHoldingSeriesOrdercmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(GetNCDHoldingSeriesOrdercmd, "@AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetNCDHoldingSeriesOrdercmd, "@IssueId", DbType.Int32, IssueId);
                db.AddInParameter(GetNCDHoldingSeriesOrdercmd, "@orderId", DbType.Int32, orderId); 
                dsGetNCDHoldingSeriesOrder = db.ExecuteDataSet(GetNCDHoldingSeriesOrdercmd);
                dtGetNCDHoldingSeriesOrder = dsGetNCDHoldingSeriesOrder.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetNCDHoldingOrder()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetNCDHoldingSeriesOrder;
        }
        public DataTable GetCustomerIssueName(int CustomerId, string Product)
        {
            DbCommand cmdGetCustomerIssueName;
            DataTable dtGetCustomerIssueName;
            DataSet dsGetCustomerIssueName = null;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerIssueName = db.GetStoredProcCommand("SPROC_GetCustomerNCDIPOIssueName");
                db.AddInParameter(cmdGetCustomerIssueName, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(cmdGetCustomerIssueName, "@Product", DbType.String, Product);
                dsGetCustomerIssueName = db.ExecuteDataSet(cmdGetCustomerIssueName);
                dtGetCustomerIssueName = dsGetCustomerIssueName.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetCustomerIssueName;
        }
    }
}
