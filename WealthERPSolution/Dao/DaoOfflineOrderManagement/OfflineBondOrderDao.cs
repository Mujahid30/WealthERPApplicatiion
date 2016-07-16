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
        public DataSet GetOfflineAdviserIssuerList(int adviserId, int issueId, int type, int category)
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
                //db.AddInParameter(cmdGetCommissionStructureRules, "@CustomerSubType", DbType.Int32, CustomerSubType);
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
        public DataSet GetOfflineLiveBondTransaction(int SeriesId, int orderId, int category)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                if (orderId != 0)
                {
                    cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_OFF_GetLiveBondTransactionTViewOrderdBid");
                    db.AddInParameter(cmdGetCommissionStructureRules, "@orderId", DbType.Int32, orderId);
                }
                else
                    cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_OFF_GetLiveBondTransaction");
                db.AddInParameter(cmdGetCommissionStructureRules, "@IssueId", DbType.Int32, SeriesId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@customerSubType", DbType.Int32, category);
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
        public IDictionary<string, string> CreateOfflineBondTransact(DataTable BondORder, int adviserId, OnlineBondOrderVo OnlineBondVo, int agentId, string agentCode, int userId)
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
                db.AddInParameter(cmdOfflineBondTransact, "@AgentId", DbType.Int32, agentId);
                db.AddInParameter(cmdOfflineBondTransact, "@AgentCode", DbType.String, agentCode);
                db.AddInParameter(cmdOfflineBondTransact, "@UserId", DbType.Int32, userId);
                db.AddOutParameter(cmdOfflineBondTransact, "@Order_Id", DbType.Int32, 10);
                db.AddOutParameter(cmdOfflineBondTransact, "@application", DbType.Int32, 10);
                db.AddOutParameter(cmdOfflineBondTransact, "@aplicationNoStatus", DbType.String, 10);
                db.AddInParameter(cmdOfflineBondTransact, "@CustomerName", DbType.String, OnlineBondVo.CustomerName);
                db.AddInParameter(cmdOfflineBondTransact, "@CustomerPAN", DbType.String, OnlineBondVo.PanNo);
                db.AddInParameter(cmdOfflineBondTransact, "@CustomerType", DbType.String, OnlineBondVo.CustomerType);
                db.AddInParameter(cmdOfflineBondTransact, "@CustomerSubTypeId", DbType.Int32, OnlineBondVo.CustomerSubTypeId);
                db.AddInParameter(cmdOfflineBondTransact, "@DematBeneficiaryAccountNum", DbType.String, OnlineBondVo.DematBeneficiaryAccountNum);
                db.AddInParameter(cmdOfflineBondTransact, "@DematDepositoryName", DbType.String, OnlineBondVo.DematDepositoryName);
                db.AddInParameter(cmdOfflineBondTransact, "@DematDPId", DbType.String, OnlineBondVo.DematDPId);
                db.AddInParameter(cmdOfflineBondTransact, "@customerBankAccountID", DbType.Double, OnlineBondVo.BankAccountNo);

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
        public DataTable GetFD54IssueOrder(int adviserId, DateTime fromDate, DateTime toDate, string status, int issueId, string usrtype, string agentcode, string category, int AuthenticateStatus, int orderNo, int userId)
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
                if (fromDate != DateTime.MinValue)
                    db.AddInParameter(cmdGetFD54IssueOrder, "@Fromdate", DbType.DateTime, fromDate);
                else
                    db.AddInParameter(cmdGetFD54IssueOrder, "@Fromdate", DbType.DateTime, "2014-9-1");
                db.AddInParameter(cmdGetFD54IssueOrder, "@Todate", DbType.DateTime, toDate);
                db.AddInParameter(cmdGetFD54IssueOrder, "@UserType", DbType.String, usrtype);
                db.AddInParameter(cmdGetFD54IssueOrder, "@AgentCode", DbType.String, agentcode);
                db.AddInParameter(cmdGetFD54IssueOrder, "@category", DbType.String, category);
                if (status != "0")
                    db.AddInParameter(cmdGetFD54IssueOrder, "@Status", DbType.String, status);
                else
                    db.AddInParameter(cmdGetFD54IssueOrder, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(cmdGetFD54IssueOrder, "@AuthenticateStatus", DbType.Int32, AuthenticateStatus);
                if (orderNo != 0)
                    db.AddInParameter(cmdGetFD54IssueOrder, "@orderNo", DbType.Int32, orderNo);
                db.AddInParameter(cmdGetFD54IssueOrder, "@userId", DbType.Int32, userId);
                ds = db.ExecuteDataSet(cmdGetFD54IssueOrder);
                dtGetFD54IssueOrder = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetFD54IssueOrder;
        }
        public bool CancelBondsFDBookOrder(int orderId, string remarks, int userId, bool IsAuthenticated)
        {
            bool bResult = false;
            Database db;
            DbCommand CancelBondsFDBookOrdercmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CancelBondsFDBookOrdercmd = db.GetStoredProcCommand("SPROC_54ECMarkAsReject");
                db.AddInParameter(CancelBondsFDBookOrdercmd, "@OrderId", DbType.Int32, orderId);
                db.AddInParameter(CancelBondsFDBookOrdercmd, "@Remarks", DbType.String, remarks);
                db.AddInParameter(CancelBondsFDBookOrdercmd, "@userId", DbType.Int32, userId);
                db.AddInParameter(CancelBondsFDBookOrdercmd, "@IsAuthenticated", DbType.Boolean, IsAuthenticated);

                //db.ExecuteDataSet(CancelBondsFDBookOrdercmd);
                if (db.ExecuteNonQuery(CancelBondsFDBookOrdercmd) != 0)
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
        public void GetCustomerCat(int issueId, int adviserId, int customerSubType, double amt, ref string catName, ref int issueDetId, ref int categoryId, ref string Description)
        {
            Database db;
            DbCommand dbCommand;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_Off_GetCustomerCat");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@amt", DbType.Double, amt);
                db.AddOutParameter(dbCommand, "@catName", DbType.String, 500);
                db.AddOutParameter(dbCommand, "@OrdDetID", DbType.Int32, 0);
                db.AddOutParameter(dbCommand, "@categoryId", DbType.Int32, 0);
                db.AddOutParameter(dbCommand, "@description", DbType.String, 500);
                db.AddInParameter(dbCommand, "@issueDetId", DbType.Int32, issueDetId);
                db.AddInParameter(dbCommand, "@customerSubType", DbType.Int32, customerSubType);
                ds = db.ExecuteDataSet(dbCommand);
                Description = db.GetParameterValue(dbCommand, "description").ToString();
                catName = db.GetParameterValue(dbCommand, "catName").ToString();
                issueDetId = Convert.ToInt32(db.GetParameterValue(dbCommand, "OrdDetID").ToString());
                categoryId = Convert.ToInt32(db.GetParameterValue(dbCommand, "categoryId").ToString());
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

        }
        public bool CreateAllotmentDetails(int userid, DataTable dtOrderAllotmentDetails)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand CreateAllotmentDetailscmd;
            bool bResult = false;
            DataSet dsdtOrderAllotmentDetails = new DataSet();
            try
            {
                dsdtOrderAllotmentDetails.Tables.Add(dtOrderAllotmentDetails.Copy());
                dsdtOrderAllotmentDetails.DataSetName = "dtOrderAllotmentDetailsDS";
                dsdtOrderAllotmentDetails.Tables[0].TableName = "dtOrderAllotmentDetailsDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateAllotmentDetailscmd = db.GetStoredProcCommand("SPROC_CreateAllotmentDetails");
                db.AddInParameter(CreateAllotmentDetailscmd, "@xmlBondsAllotmentDetails", DbType.Xml, dsdtOrderAllotmentDetails.GetXml().ToString());
                db.AddInParameter(CreateAllotmentDetailscmd, "@UserId", DbType.Int32, userid);
                db.ExecuteNonQuery(CreateAllotmentDetailscmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public int GetAdviserIssueDetailsId(int IssueCategory)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdGetAdviserIssueDetailsId;
            int IssueCategoryid = 0;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAdviserIssueDetailsId = db.GetStoredProcCommand("SPROC_GetCategoryRuleDetailsId");
                db.AddInParameter(cmdGetAdviserIssueDetailsId, "@SeriesId", DbType.Int32, IssueCategory);
                db.AddOutParameter(cmdGetAdviserIssueDetailsId, "@IssueCategoryid", DbType.Int32, 0);
                ds = db.ExecuteDataSet(cmdGetAdviserIssueDetailsId);
                if (db.ExecuteNonQuery(cmdGetAdviserIssueDetailsId) != 0)
                {
                    IssueCategoryid = Convert.ToInt32(db.GetParameterValue(cmdGetAdviserIssueDetailsId, "IssueCategoryid").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return IssueCategoryid;
        }
        public DataSet GetIntrestFrequency(int detailsId)
        {
            Database db;
            DbCommand cmdGetFDIddueList;
            DataSet ds = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetFDIddueList = db.GetStoredProcCommand("SPROC_GetFrequencyAndIntrestRate");
                db.AddInParameter(cmdGetFDIddueList, "@IssueDetailId", DbType.Int32, detailsId);
                ds = db.ExecuteDataSet(cmdGetFDIddueList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }



        public DataSet GetCustomerAllotedData(int customerId)
        {
            Database db;
            DbCommand cmdGetCustomerAllotedData;
            DataSet ds = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerAllotedData = db.GetStoredProcCommand("SPROC_GetBondsAllotmentDetails");
                db.AddInParameter(cmdGetCustomerAllotedData, "@CustomerId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(cmdGetCustomerAllotedData);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }
        public DataSet GetCustomerAllotedDetailData(int customerId)
        {
            Database db;
            DbCommand cmdGetCustomerAllotedDetailData;
            DataSet ds = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerAllotedDetailData = db.GetStoredProcCommand("SPROC_GetCustomerBondAllotedData");
                db.AddInParameter(cmdGetCustomerAllotedDetailData, "@coadId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(cmdGetCustomerAllotedDetailData);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        public bool UpdateAllotmentDetails(int userid, DataTable dtOrderAllotmentDetails,int coadId)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand CreateAllotmentDetailscmd;
            bool bResult = false;
            DataSet dsdtOrderAllotmentDetails = new DataSet();
            try
            {
                dsdtOrderAllotmentDetails.Tables.Add(dtOrderAllotmentDetails.Copy());
                dsdtOrderAllotmentDetails.DataSetName = "dtOrderAllotmentDetailsDS";
                dsdtOrderAllotmentDetails.Tables[0].TableName = "dtOrderAllotmentDetailsDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateAllotmentDetailscmd = db.GetStoredProcCommand("SPROC_UpdateAllotmentOrderDetails");
                db.AddInParameter(CreateAllotmentDetailscmd, "@xmlBondsAllotmentDetails", DbType.Xml, dsdtOrderAllotmentDetails.GetXml().ToString());
                db.AddInParameter(CreateAllotmentDetailscmd, "@UserId", DbType.Int32, userid);
                db.AddInParameter(CreateAllotmentDetailscmd, "@coadId", DbType.Int32, coadId);
                db.ExecuteNonQuery(CreateAllotmentDetailscmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
    }
}
