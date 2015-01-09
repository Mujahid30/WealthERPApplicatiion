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

namespace DaoOfflineOrderManagement
{
    public class OfflineBondOrderDao
    {
        public DataSet GetOfflineAdviserIssuerList(int adviserId, int issueId, int type, int customerId, int CustomerSubType)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_OFF_GetIssuerlist");
                db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, adviserId);
                if (issueId != 0)
                    db.AddInParameter(cmdGetCommissionStructureRules, "@IssueId", DbType.Int32, issueId);
                else
                    db.AddInParameter(cmdGetCommissionStructureRules, "@IssueId", DbType.Int32, 0);

                db.AddInParameter(cmdGetCommissionStructureRules, "@type", DbType.Int32, type);
                db.AddInParameter(cmdGetCommissionStructureRules, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@CustomerSubType", DbType.Int32, CustomerSubType);
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
        public DataSet GetOfflineLiveBondTransaction(int SeriesId, int customerId, int CustomerSubType)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_OFF_GetLiveBondTransaction");

                db.AddInParameter(cmdGetCommissionStructureRules, "@IssueId", DbType.Int32, SeriesId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@customerSubType", DbType.Int32, CustomerSubType);
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
        public void CreateOfflineCustomerOrderAssociation(DataTable OrderAssociates, int userId, int orderId)
        {
            Database db;
            DbCommand cmdOfflineBondTransact;
            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = OrderAssociates.Copy();
                ds.Tables.Add(dt);
                String sb;
                sb = ds.GetXml().ToString();

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOfflineBondTransact = db.GetStoredProcCommand("SPROC_OFF_CreateCustomerOrderAssociates");
                db.AddInParameter(cmdOfflineBondTransact, "@xmlBondsOrder", DbType.Xml, sb);
                db.AddInParameter(cmdOfflineBondTransact, "@UserId", DbType.Int32, userId);
                db.AddInParameter(cmdOfflineBondTransact, "@Order_Id", DbType.Int32, orderId);
                db.ExecuteNonQuery(cmdOfflineBondTransact);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:CreateCustomerOrderAssociation(DataTable OrderAssociates, int userId,int orderId)");
                object[] objects = new object[1];
                objects[0] = OrderAssociates;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public IDictionary<string, string> CreateOfflineBondTransact(DataTable BondORder, int adviserId, int IssuerId, int agentId, string agentCode, int userId)
        {
            //List<int> orderIds = new List<int>();
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            Database db;
            DbCommand cmdOfflineBondTransact;
            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = BondORder.Copy();
                ds.Tables.Add(dt);
                String sb;
                sb = ds.GetXml().ToString();

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOfflineBondTransact = db.GetStoredProcCommand("SPROC_OFF_OfflineBondTransaction");
                db.AddInParameter(cmdOfflineBondTransact, "@xmlBondsOrder", DbType.Xml, sb);
                db.AddInParameter(cmdOfflineBondTransact, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdOfflineBondTransact, "@AIM_IssueId", DbType.Int32, IssuerId);
                db.AddInParameter(cmdOfflineBondTransact, "@AgentId", DbType.Int32, agentId);
                db.AddInParameter(cmdOfflineBondTransact, "@AgentCode", DbType.String, agentCode);
                db.AddInParameter(cmdOfflineBondTransact, "@UserId", DbType.Int32, userId);
                db.AddOutParameter(cmdOfflineBondTransact, "@Order_Id", DbType.Int32, 10);
                db.AddOutParameter(cmdOfflineBondTransact, "@application", DbType.Int32, 10);
                db.AddOutParameter(cmdOfflineBondTransact, "@aplicationNoStatus", DbType.String, 10);


                if (db.ExecuteNonQuery(cmdOfflineBondTransact) != 0)
                {

                    OrderIds.Add("Order_Id", db.GetParameterValue(cmdOfflineBondTransact, "Order_Id").ToString());
                    OrderIds.Add("application", db.GetParameterValue(cmdOfflineBondTransact, "application").ToString());
                    OrderIds.Add("aplicationNoStatus", db.GetParameterValue(cmdOfflineBondTransact, "aplicationNoStatus").ToString());

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
        public DataTable GetFDIddueList(string Category)
        {
            Database db;
            DbCommand cmdGetFDIddueList;
            DataSet ds = null;
            DataTable dtGetFDIddueList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetFDIddueList = db.GetStoredProcCommand("SPROC_GetFDIsssue");
                db.AddInParameter(cmdGetFDIddueList, "@AssetInstrumentSubCategoryCode", DbType.String, Category);
                ds = db.ExecuteDataSet(cmdGetFDIddueList);
                dtGetFDIddueList = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetFDIddueList;
        }
        public DataTable GetFD54IssueOrder(int adviserId, DateTime fromDate, DateTime toDate,string status, int issueId,string usrtype,string agentcode,string category,int AuthenticateStatus)
        {
            Database db;
            DbCommand cmdGetFD54IssueOrder;
            DataSet ds = null;
            DataTable dtGetFD54IssueOrder;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetFD54IssueOrder = db.GetStoredProcCommand("SPROC_Get54ECOrderBook");
                db.AddInParameter(cmdGetFD54IssueOrder, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(cmdGetFD54IssueOrder, "@AdviserID", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetFD54IssueOrder, "@Fromdate", DbType.DateTime, fromDate);
                db.AddInParameter(cmdGetFD54IssueOrder, "@Todate", DbType.DateTime, toDate);
                db.AddInParameter(cmdGetFD54IssueOrder, "@UserType", DbType.String, usrtype);
                db.AddInParameter(cmdGetFD54IssueOrder, "@AgentCode", DbType.String, agentcode);
                db.AddInParameter(cmdGetFD54IssueOrder, "@category", DbType.String, category);
                if (status != "0")
                    db.AddInParameter(cmdGetFD54IssueOrder, "@Status", DbType.String, status);
                else
                    db.AddInParameter(cmdGetFD54IssueOrder, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(cmdGetFD54IssueOrder, "@AuthenticateStatus", DbType.Int32, AuthenticateStatus);
                ds = db.ExecuteDataSet(cmdGetFD54IssueOrder);
                dtGetFD54IssueOrder = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetFD54IssueOrder;
        }
    }
}
