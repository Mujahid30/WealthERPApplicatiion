using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;


namespace DaoOnlineOrderManagement
{
    public class OnlineMFOrderDao : OnlineOrderDao
    {
        public DataSet GetMfOrderExtract(DateTime dtFrom, int adviserId, string orderType)
        {
            DataSet dsGetMfOrderExtract;
            Database db;
            DbCommand GetGetMfOrderExtractCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetMfOrderExtractCmd = db.GetStoredProcCommand("Sp_CreateAdviserMFOrderExtract");
                db.AddInParameter(GetGetMfOrderExtractCmd, "@Fromdate", DbType.DateTime, dtFrom);

                db.AddInParameter(GetGetMfOrderExtractCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@OrderType", DbType.Int32, adviserId);

                dsGetMfOrderExtract = db.ExecuteDataSet(GetGetMfOrderExtractCmd);

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
            return dsGetMfOrderExtract;
        }


        public DataSet GetOrderBookMIS(int CustomerId, int AmcCode, string OrderStatus, DateTime dtFrom, DateTime dtTo, string orderType)
        {
            DataSet dsOrderBookMIS;
            Database db;
            DbCommand GetOrderBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBookMISCmd = db.GetStoredProcCommand("SPROC_Onl_GetOrderBook");
                db.AddInParameter(GetOrderBookMISCmd, "@C_CustomerId", DbType.Int32, CustomerId);
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
                db.AddInParameter(GetOrderBookMISCmd, "@StatusType", DbType.String, orderType);
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

        public DataSet GetFolioAccount(int CustomerId, int exchangeType)
        {
            DataSet dsFolioAccount;
            Database db;
            DbCommand GetFolioAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetFolioAccountCmd = db.GetStoredProcCommand("SPROC_Onl_BindFolioAccount");
                db.AddInParameter(GetFolioAccountCmd, "@C_CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(GetFolioAccountCmd, "@exchangeType", DbType.Int32, exchangeType);
                dsFolioAccount = db.ExecuteDataSet(GetFolioAccountCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetFolioAccount()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFolioAccount;
        }
        public DataSet GetOrderStatus()
        {
            DataSet dsOrderStatus;
            Database db;
            DbCommand GetOrderStatusCmd;
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderStatusCmd = db.GetStoredProcCommand("Sproc_onl_GetOrderStatus");
                dsOrderStatus = db.ExecuteDataSet(GetOrderStatusCmd);

            }

            return dsOrderStatus;
        }
        public DataSet GetOrderIssueStatus()
        {
            DataSet dsOrderStatus;
            Database db;
            DbCommand GetOrderStatusCmd;
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderStatusCmd = db.GetStoredProcCommand("Sproc_onl_GetOrderIssueStatus");
                dsOrderStatus = db.ExecuteDataSet(GetOrderStatusCmd);

            }

            return dsOrderStatus;
        }
        public DataSet GetControlDetails(int Scheme, string folio, int demat)
        {
            DataSet dsGetControlDetails;
            Database db;
            DbCommand GetGetControlDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetControlDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_GetSchemeDetails");
                db.AddInParameter(GetGetControlDetailsCmd, "@schemecode", DbType.Int32, Scheme);
                if (!string.IsNullOrEmpty(folio))
                    db.AddInParameter(GetGetControlDetailsCmd, "@accountid", DbType.Int32, int.Parse(folio));
                else
                    db.AddInParameter(GetGetControlDetailsCmd, "@accountid", DbType.Int32, 0);
                db.AddInParameter(GetGetControlDetailsCmd, "@demate", DbType.Int32, demat);
                dsGetControlDetails = db.ExecuteDataSet(GetGetControlDetailsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetFolioAccount()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetControlDetails;
        }
        public List<int> CreateCustomerOnlineMFOrderDetails(OnlineMFOrderVo onlinemforderVo, int UserId, int CustomerId)
        {
            List<int> orderIds = new List<int>();
            int orderId;
            Database db;
            DbCommand CreateCustomerOnlineMFOrderDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateCustomerOnlineMFOrderDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_CreateCustomerOnlineMFOrderDetails");
                db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@schemecode", DbType.Int32, onlinemforderVo.SchemePlanCode);
                db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@amount", DbType.Double, onlinemforderVo.Amount);
                db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@userId", DbType.Int32, UserId);
                db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@customerId", DbType.Int32, CustomerId);
                db.AddOutParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@CO_OrderId", DbType.Int32, 10);
                db.AddOutParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@CMFOD_OrderDetailsId", DbType.Int32, 10);
                db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@TransactionType", DbType.String, onlinemforderVo.TransactionType);
                db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@DividendType", DbType.String, onlinemforderVo.DividendType);

                if (!string.IsNullOrEmpty(onlinemforderVo.Redeemunits.ToString()))
                {
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@Redeemunits", DbType.Double, onlinemforderVo.Redeemunits);
                }
                else
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@Redeemunits", DbType.Double, 0);
                if (!string.IsNullOrEmpty(onlinemforderVo.FolioNumber))
                {
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@accountid", DbType.Int32, int.Parse(onlinemforderVo.FolioNumber));
                }
                else
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@accountid", DbType.Int32, 0);
                if (onlinemforderVo.IsAllUnits == true)
                {
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@IsAllUnits", DbType.Boolean, bool.Parse(onlinemforderVo.IsAllUnits.ToString()));
                }
                else
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@IsAllUnits", DbType.Boolean, 0);
                db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@IsDemate", DbType.Int32, onlinemforderVo.OrderType);

                if (db.ExecuteNonQuery(CreateCustomerOnlineMFOrderDetailsCmd) != 0)
                {
                    orderId = Convert.ToInt32(db.GetParameterValue(CreateCustomerOnlineMFOrderDetailsCmd, "CO_OrderId").ToString());
                    orderIds.Add(orderId);
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
                FunctionInfo.Add("Method", "OperationDao.cs:GetFolioAccount()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderIds;
        }
        public List<int> CreateOnlineMFSwitchOrderDetails(DataTable dtSwitchOrder, int userId, int customerId)
        {
            List<int> orderIds = new List<int>();
            int sICO_OrderId, sOCO_OrderId;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt = dtSwitchOrder.Copy();
            ds.Tables.Add(dt);
            String sb;
            sb = ds.GetXml().ToString();
            Database db;
            DbCommand CreateOnlineMFSwitchOrderDetailsCmd;
            try
            {


                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateOnlineMFSwitchOrderDetailsCmd = db.GetStoredProcCommand("SPROC_ONL_CreateCustomerOnlineMFOrderSwitchDetails");
                db.AddInParameter(CreateOnlineMFSwitchOrderDetailsCmd, "@xmlOrderDetails", DbType.Xml, sb);
                db.AddInParameter(CreateOnlineMFSwitchOrderDetailsCmd, "@userId", DbType.Int32, userId);
                db.AddInParameter(CreateOnlineMFSwitchOrderDetailsCmd, "@customerId", DbType.Int32, customerId);
                db.AddOutParameter(CreateOnlineMFSwitchOrderDetailsCmd, "@SICO_OrderId", DbType.Int32, 10);
                db.AddOutParameter(CreateOnlineMFSwitchOrderDetailsCmd, "@SOCO_OrderId", DbType.Int32, 10);
                if (db.ExecuteNonQuery(CreateOnlineMFSwitchOrderDetailsCmd) != 0)
                {
                    sICO_OrderId = Convert.ToInt32(db.GetParameterValue(CreateOnlineMFSwitchOrderDetailsCmd, "SICO_OrderId").ToString());
                    sOCO_OrderId = Convert.ToInt32(db.GetParameterValue(CreateOnlineMFSwitchOrderDetailsCmd, "SOCO_OrderId").ToString());
                    orderIds.Add(sICO_OrderId);
                    orderIds.Add(sOCO_OrderId);
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
                FunctionInfo.Add("Method", "OperationDao.cs:GetFolioAccount()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderIds;

        }
        public DataSet GetSIPBookMIS(int CustomerId, int AmcCode, string OrderStatus, int systematicId)
        {
            DataSet dsSIPBookMIS;
            Database db;
            DbCommand GetSIPBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPBookMISCmd = db.GetStoredProcCommand("SPROC_Onl_GetSIPBook");
                db.AddInParameter(GetSIPBookMISCmd, "@C_CustomerId", DbType.Int32, CustomerId);
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
                //db.AddInParameter(GetSIPBookMISCmd, "@Fromdate", DbType.DateTime, dtFrom);
                //db.AddInParameter(GetSIPBookMISCmd, "@ToDate", DbType.DateTime, dtTo);
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


        public IDictionary<string, string> CreateOrderMFSipDetails(OnlineMFOrderVo onlineMFOrderVo, int userId)
        {
            IDictionary<string, string> sipOrderIds = new Dictionary<string, string>();
            //int OrderId;
            Database db;
            DbCommand createMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SPROC_Onl_CreateCustomerOnlineMFSipDetails");

                db.AddInParameter(createMFOrderTrackingCmd, "@PASP_SchemePlanCode", DbType.Int32, onlineMFOrderVo.SchemePlanCode);

                if (onlineMFOrderVo.AccountId != 0)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFA_accountid", DbType.Int32, onlineMFOrderVo.AccountId);

                if (!string.IsNullOrEmpty(onlineMFOrderVo.SystematicTypeCode))
                    db.AddInParameter(createMFOrderTrackingCmd, "@XSTT_SystematicTypeCode", DbType.String, onlineMFOrderVo.SystematicTypeCode);

                if (onlineMFOrderVo.StartDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_StartDate", DbType.DateTime, onlineMFOrderVo.StartDate);

                if (onlineMFOrderVo.EndDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_EndDate", DbType.DateTime, onlineMFOrderVo.EndDate);

                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_TotalInstallment", DbType.Int32, onlineMFOrderVo.TotalInstallments);
                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_SystematicDate", DbType.Int32, onlineMFOrderVo.SystematicDate);
                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_Amount", DbType.Double, onlineMFOrderVo.Amount);
                db.AddInParameter(createMFOrderTrackingCmd, "@XES_SourceCode", DbType.String, onlineMFOrderVo.SourceCode);

                if (!string.IsNullOrEmpty(onlineMFOrderVo.FrequencyCode))
                    db.AddInParameter(createMFOrderTrackingCmd, "@XF_FrequencyCode", DbType.String, onlineMFOrderVo.FrequencyCode);

                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_SubBrokerCode", DbType.Int32, onlineMFOrderVo.AgentCode);
                db.AddInParameter(createMFOrderTrackingCmd, "@customerId", DbType.Int32, onlineMFOrderVo.CustomerId);
                db.AddInParameter(createMFOrderTrackingCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(createMFOrderTrackingCmd, "@systamaticDates", DbType.String, onlineMFOrderVo.SystematicDate.ToString());
                db.AddOutParameter(createMFOrderTrackingCmd, "@CO_OrderId", DbType.Int32, 10);
                db.AddInParameter(createMFOrderTrackingCmd, "@CP_PortfolioId", DbType.Int32, onlineMFOrderVo.PortfolioId);
                db.AddInParameter(createMFOrderTrackingCmd, "@CMFOD_DividendOption", DbType.String, onlineMFOrderVo.DivOption);
                if (!string.IsNullOrEmpty(onlineMFOrderVo.SWPRedeemValueType))
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_SWPRedeemValueType", DbType.String, onlineMFOrderVo.SWPRedeemValueType);
                if (!string.IsNullOrEmpty(onlineMFOrderVo.ModeTypeCode))
                 db.AddInParameter(createMFOrderTrackingCmd, "@ModeTypeCode", DbType.String, onlineMFOrderVo.ModeTypeCode);
                

                db.AddOutParameter(createMFOrderTrackingCmd, "@SIPRegisterId", DbType.Int32, 10000);


                if (db.ExecuteNonQuery(createMFOrderTrackingCmd) != 0)
                {
                    sipOrderIds.Add("OrderId", db.GetParameterValue(createMFOrderTrackingCmd, "CO_OrderId").ToString());
                    sipOrderIds.Add("SIPId", db.GetParameterValue(createMFOrderTrackingCmd, "SIPRegisterId").ToString());

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return sipOrderIds;
        }


        public DataSet GetSipDetails(int schemeId, string frequency,bool IsDemat)
        {
            DataSet dsSipDetails;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_Onl_SipDetails");
                db.AddInParameter(cmd, "@PASP_SchemePlanCode", DbType.Int32, schemeId);
                if (frequency != null) db.AddInParameter(cmd, "@XF_SystematicFrequencyCode", DbType.String, frequency);
                db.AddInParameter(cmd, "@IsDemat", DbType.Boolean, IsDemat);
                dsSipDetails = db.ExecuteDataSet(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetFolioAccount()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSipDetails;
        }
        public DataSet GetCustomerHoldingAMCList(int customerId, char type)
        {
            DataSet dsCustomerHoldingAMCList;
            Database db;
            DbCommand GetCustomerHoldingAMCListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCustomerHoldingAMCListcmd = db.GetStoredProcCommand("SPROC_ONL_GetCustomerHoldingAMCList");
                db.AddInParameter(GetCustomerHoldingAMCListcmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetCustomerHoldingAMCListcmd, "@Type", DbType.String, type);
                dsCustomerHoldingAMCList = db.ExecuteDataSet(GetCustomerHoldingAMCListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsCustomerHoldingAMCList;
        }
        public OnlineMFOrderVo GetOrderDetails(int Id)
        {
            DataSet dsGetOrderDetails;
            OnlineMFOrderVo onlinemforderVo = new OnlineMFOrderVo();
            Database db;
            DbCommand GetOrderDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderDetailscmd = db.GetStoredProcCommand("SPROC_ONL_GetOrderDetails");
                db.AddInParameter(GetOrderDetailscmd, "@Id", DbType.Int32, Id);
                dsGetOrderDetails = db.ExecuteDataSet(GetOrderDetailscmd);
                foreach (DataRow dr in dsGetOrderDetails.Tables[0].Rows)
                {
                    onlinemforderVo.SchemePlanCode = Convert.ToInt32(dr["scheme"].ToString());
                    onlinemforderVo.Amount = Convert.ToDouble(dr["amount"].ToString());
                    onlinemforderVo.DividendType = dr["DivOption"].ToString();
                    onlinemforderVo.TransactionType = dr["transactiontype"].ToString();

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return onlinemforderVo;
        }
        public DataSet GetSIPSummaryBookMIS(int CustomerId, int AmcCode, string systematicType,string SIPMode)
        {
            DataSet dsSIPSummaryBookMIS;
            Database db;
            DbCommand GetSIPSummaryBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPSummaryBookMISCmd = db.GetStoredProcCommand("SPROC_ONL_GetSIPSummaryBookDet");
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@C_CustomerId", DbType.Int32, CustomerId);
                if (AmcCode != 0)
                    db.AddInParameter(GetSIPSummaryBookMISCmd, "@AMC", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(GetSIPSummaryBookMISCmd, "@AMC", DbType.Int32, 0);
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@systematicType", DbType.String, systematicType);
                db.AddInParameter(GetSIPSummaryBookMISCmd, "@SIPMode", DbType.String, SIPMode);
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
        public void TriggerAutoOrderFromSIP()
        {
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_CreateAutoSystematicOrder");
                db.ExecuteNonQuery(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:TriggerAutoOrderFromSIP()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataSet GetSIPAmcDetails(int customerId, string systematicType)
        {
            DataSet dsGetSIPAmcDetails;
            Database db;
            DbCommand GetSIPAmcDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPAmcDetailscmd = db.GetStoredProcCommand("sproc_onl_BindAmcSIP");
                db.AddInParameter(GetSIPAmcDetailscmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetSIPAmcDetailscmd, "@systematicType", DbType.String, systematicType);
                dsGetSIPAmcDetails = db.ExecuteDataSet(GetSIPAmcDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetSIPAmcDetails;
        }
        public DataSet GetOrderAmcDetails(int customerId, Boolean IsRTA)
        {
            DataSet dsGetOrderAmcDetails;
            Database db;
            DbCommand GetOrderAmcDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderAmcDetailscmd = db.GetStoredProcCommand("sproc_onl_BindAmcOrder");
                db.AddInParameter(GetOrderAmcDetailscmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetOrderAmcDetailscmd, "@IsRTA", DbType.Boolean, IsRTA);
                dsGetOrderAmcDetails = db.ExecuteDataSet(GetOrderAmcDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetOrderAmcDetails;
        }
        public DataSet GetTransAllAmcDetails(int customerId)
        {
            DataSet dsGetTransAllAmcDetails;
            Database db;
            DbCommand GetTransAllAmcDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetTransAllAmcDetailscmd = db.GetStoredProcCommand("sproc_onl_BindAmcAllTran");
                db.AddInParameter(GetTransAllAmcDetailscmd, "@customerId", DbType.Int32, customerId);
                dsGetTransAllAmcDetails = db.ExecuteDataSet(GetTransAllAmcDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetTransAllAmcDetails;
        }

        public bool UpdateCnacleRegisterSIP(int systematicId, int is_Cancel, string remark, int cancelBy)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateCnacleRegisterSIP;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateCnacleRegisterSIP = db.GetStoredProcCommand("sproc_onl_CancelSIPRegister");
                db.AddInParameter(UpdateCnacleRegisterSIP, "@systematicId", DbType.Int32, systematicId);
                db.AddInParameter(UpdateCnacleRegisterSIP, "@Is_Canceled", DbType.Int32, is_Cancel);
                db.AddInParameter(UpdateCnacleRegisterSIP, "@Remark", DbType.String, remark);
                db.AddInParameter(UpdateCnacleRegisterSIP, "@CancelBy", DbType.Int32, cancelBy);
                if (db.ExecuteNonQuery(UpdateCnacleRegisterSIP) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }
        public DataTable GetMFSchemeDetailsForLanding(int Schemeplancode, string category)
        {
            DataSet dsGetMFSchemeDetailsForLanding;
            DataTable dtGetMFSchemeDetailsForLanding;
            Database db;
            DbCommand GetMFSchemeDetailsForLandingCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetMFSchemeDetailsForLandingCmd = db.GetStoredProcCommand("SPROC_Onl_GetMFSchemeDetailsForLanding");
                if (Schemeplancode != 0)
                    db.AddInParameter(GetMFSchemeDetailsForLandingCmd, "@Schemeplancode", DbType.Int32, Schemeplancode);
                else
                    db.AddInParameter(GetMFSchemeDetailsForLandingCmd, "@Schemeplancode", DbType.Int32, 0);
                if (category != "0")
                    db.AddInParameter(GetMFSchemeDetailsForLandingCmd, "@category", DbType.String, category);
                else
                    db.AddInParameter(GetMFSchemeDetailsForLandingCmd, "@category", DbType.String, DBNull.Value);
                dsGetMFSchemeDetailsForLanding = db.ExecuteDataSet(GetMFSchemeDetailsForLandingCmd);
                dtGetMFSchemeDetailsForLanding = dsGetMFSchemeDetailsForLanding.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineMFOrderDao.cs:GetMFSchemeDetailsForLanding()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetMFSchemeDetailsForLanding;
        }

        public DataSet GetCustomerSchemeFolioHoldings(int customerId, int schemeId, out string schemeDividendOption, int Demate, int accountId)
        {
            DataSet dsCustomerSchemeFolioHoldings;
            Database db;
            DbCommand GetCustomerSchemeFolioHoldingsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCustomerSchemeFolioHoldingsCmd = db.GetStoredProcCommand("SPROC_ONL_GetCustomerSchemeFolioHoldings");
                db.AddInParameter(GetCustomerSchemeFolioHoldingsCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(GetCustomerSchemeFolioHoldingsCmd, "@SchemeId", DbType.Int32, schemeId);
                db.AddInParameter(GetCustomerSchemeFolioHoldingsCmd, "@Demate", DbType.Int32, Demate);
                db.AddInParameter(GetCustomerSchemeFolioHoldingsCmd, "@accountid", DbType.Int32, accountId);
                db.AddOutParameter(GetCustomerSchemeFolioHoldingsCmd, "@DividendType", DbType.String, 1000);
                dsCustomerSchemeFolioHoldings = db.ExecuteDataSet(GetCustomerSchemeFolioHoldingsCmd);

                if (db.GetParameterValue(GetCustomerSchemeFolioHoldingsCmd, "@DividendType").ToString() != string.Empty)
                {
                    schemeDividendOption = db.GetParameterValue(GetCustomerSchemeFolioHoldingsCmd, "@DividendType").ToString();
                }
                else
                    schemeDividendOption = string.Empty;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetCustomerSchemeFolioHoldings(int customerId, int schemeId)");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCustomerSchemeFolioHoldings;
        }

        public DataSet GetCustomerOrderBookTransaction(int customerId, int amcCode, int schemeCode, string orderType, int exchangeType)
        {
            DataSet dsOGetCustomerOrderBookTransaction;
            Database db;
            DbCommand GetCustomerOrderBookTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCustomerOrderBookTransactionCmd = db.GetStoredProcCommand("SPROC_Onl_GetCustomerOrDerBookAndTransaction");
                db.AddInParameter(GetCustomerOrderBookTransactionCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetCustomerOrderBookTransactionCmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(GetCustomerOrderBookTransactionCmd, "@schemeCode", DbType.Int32, schemeCode);
                db.AddInParameter(GetCustomerOrderBookTransactionCmd, "@transactionType", DbType.String, orderType);
                db.AddInParameter(GetCustomerOrderBookTransactionCmd, "@exchangeType", DbType.Int32, exchangeType);
                dsOGetCustomerOrderBookTransaction = db.ExecuteDataSet(GetCustomerOrderBookTransactionCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsOGetCustomerOrderBookTransaction;
        }
        public DataTable GetCustomerFolioSchemeWise(int customerId, int schemeCode, int IsDemat)
        {
            DataSet dsGetCustomerFolioSchemeWise;
            Database db;
            DbCommand GetGetCustomerFolioSchemeWiseCmd;
            DataTable dtGetCustomerFolioSchemeWise;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetCustomerFolioSchemeWiseCmd = db.GetStoredProcCommand("SPROC_ONL_GetCustomerFolioAsPerScheme");
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@SchemeId", DbType.Int32, schemeCode);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@IsDemat", DbType.Int32, IsDemat);
                dsGetCustomerFolioSchemeWise = db.ExecuteDataSet(GetGetCustomerFolioSchemeWiseCmd);
                dtGetCustomerFolioSchemeWise = dsGetCustomerFolioSchemeWise.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetCustomerFolioSchemeWise;
        }
        public int BSEorderEntryParam(string TransCode, int UserID, string ClientCode, string SchemeCd, string BuySell, string BuySellType,
string DPTxn, string OrderVal, string Qty, string AllRedeem, string Remarks, string KYCStatus, string RefNo, string SubBrCode, string EUIN,
string EUINVal, string MinRedeem, string DPC, string IPAdd, int rmsId)
        {
            Database db;
            DbCommand GetGetCustomerFolioSchemeWiseCmd;

            int result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetCustomerFolioSchemeWiseCmd = db.GetStoredProcCommand("SPROC_ONL_CreateBSEMFOrderEntryRequestDetails");
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@Transactioncode", DbType.String, TransCode);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@UserID", DbType.Int32, UserID);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@ClientCode", DbType.String, ClientCode);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@SchemeCode", DbType.String, SchemeCd);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@BuySell", DbType.String, BuySell);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@BuySellType", DbType.String, BuySellType);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@DPTxn", DbType.String, DPTxn);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@Amount", DbType.String, OrderVal);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@Qty", DbType.String, Qty);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@AllRedeem", DbType.String, AllRedeem);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@KYCStatus", DbType.String, KYCStatus);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@RefNo", DbType.String, RefNo);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@SubBrCode", DbType.String, SubBrCode);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@EUIN", DbType.String, EUIN);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@EUINVal", DbType.String, EUINVal);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@MinRedeem", DbType.String, MinRedeem);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@DPC", DbType.String, DPC);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@IPAdd", DbType.String, IPAdd);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@rmsId", DbType.Int32, rmsId);
                db.AddOutParameter(GetGetCustomerFolioSchemeWiseCmd, "@OutOrderId", DbType.Int32, 10);
                if (db.ExecuteNonQuery(GetGetCustomerFolioSchemeWiseCmd) != 0)
                {
                    result = Convert.ToInt32(db.GetParameterValue(GetGetCustomerFolioSchemeWiseCmd, "OutOrderId").ToString());

                }


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;

        }
        public void BSEorderResponseParam(int RequestId, int userId, double BSEOrderId, string ClientCode, string BSERemarks, string Successflag, int rmsId, string uniqueRefNo)
        {
            Database db;
            DbCommand GetGetCustomerFolioSchemeWiseCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetCustomerFolioSchemeWiseCmd = db.GetStoredProcCommand("SPROC_ONL_CreateBSEMFOrderEntryResponseDetails");
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@UserID", DbType.Int32, userId);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@ClientCode", DbType.String, ClientCode);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@BSEOrderId", DbType.Int64, BSEOrderId);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@Remarks", DbType.String, BSERemarks);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@Successflag", DbType.Int32, Successflag);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@ReqId", DbType.Int64, RequestId);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@UniqueRefNo", DbType.String, uniqueRefNo);
                db.AddInParameter(GetGetCustomerFolioSchemeWiseCmd, "@rmsId", DbType.Int32, rmsId);
                db.ExecuteNonQuery(GetGetCustomerFolioSchemeWiseCmd);
            }
            catch (Exception ex)
            {
            }
        }
        public bool BSERequestOrderIdUpdate(int orderId, long bseReqId, int rmsId)
        {
            Database db;
            DbCommand BSERequestOrderIdUpdateCmd;
            bool result = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                BSERequestOrderIdUpdateCmd = db.GetStoredProcCommand("SPROC_BSE_OrderIdUpdate");
                db.AddInParameter(BSERequestOrderIdUpdateCmd, "@orderId", DbType.Int32, orderId);
                db.AddInParameter(BSERequestOrderIdUpdateCmd, "@RMSId", DbType.Int32, rmsId);
                db.AddInParameter(BSERequestOrderIdUpdateCmd, "@BSEReqId", DbType.Int64, bseReqId);
                if (db.ExecuteNonQuery(BSERequestOrderIdUpdateCmd) != 0)
                    result = true;

            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public bool UpdateSystematicStep(int systematicId,string StepCode,int userId)
        {
            Database db;
            DbCommand cmd;
            bool result = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_CreateSystematicRequestStep");
                db.AddInParameter(cmd, "@CMFSS_SystematicSetupId", DbType.Int32, systematicId);
                db.AddInParameter(cmd, "@WOS_StepCode", DbType.String, StepCode);
                db.AddInParameter(cmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(cmd, "@CMFSS_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(cmd, "@IsUpdateStep", DbType.Boolean, true);
                db.AddOutParameter(cmd,"@CMFSS_StepId",DbType.Int32, 1000);
                if (db.ExecuteNonQuery(cmd) != 0)
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
                FunctionInfo.Add("Method", "DAO:UpdateSystematicStep(int systematicId)");

                object[] objects = new object[1];
                objects[0] = systematicId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        public bool BSESIPRequestUpdate(int systematicId, long bseReqId)
        {
            Database db;
            DbCommand cmd;
            bool result = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_BSE_SIPOrderIdUpdate");

                db.AddInParameter(cmd, "@SystematicId", DbType.Int64, systematicId);
                db.AddInParameter(cmd, "@BSEReqId", DbType.Int64, bseReqId);
                if (db.ExecuteNonQuery(cmd) != 0)
                    result = true;

            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public string BSESchemeCode(int schemecode, string divdentType)
        {
            Database db;
            DbCommand BSESchemeCodeCmd;
            string BSECode = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                BSESchemeCodeCmd = db.GetStoredProcCommand("SPROC_BSE_BSESchemeCode");
                db.AddInParameter(BSESchemeCodeCmd, "@schemecode", DbType.Int32, schemecode);
                db.AddInParameter(BSESchemeCodeCmd, "@divdentType", DbType.String, divdentType);
                db.AddOutParameter(BSESchemeCodeCmd, "@BSECode", DbType.String, 100);
                if (db.ExecuteNonQuery(BSESchemeCodeCmd) != 0)
                    BSECode = db.GetParameterValue(BSESchemeCodeCmd, "BSECode").ToString();
            }
            catch (Exception ex)
            {
            }
            return BSECode;
        }
        public DataSet GetAPICredentials(string APIName, int AdviserId)
        {
            Database db;
            DbCommand Cmd;
            DataSet ds = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SPROC_GetAPICredentials");
                db.AddInParameter(Cmd, "@APIName", DbType.String, APIName);
                db.AddInParameter(Cmd, "@AdviserId", DbType.Int32, AdviserId);
                ds = db.ExecuteDataSet(Cmd);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public int CreateMandateOrder(int customerId, double Amount, string BankName, string BankBranch, int UserId,int mandateId)
        {
            Database db;
            DbCommand cmd;
            int result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_CreateCustomerBSEMandateOrder");
                db.AddInParameter(cmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmd, "@Amount", DbType.Double, Amount);
                db.AddInParameter(cmd, "@BankName", DbType.String, BankName);
                db.AddInParameter(cmd, "@BankBranch", DbType.String, BankBranch);
                db.AddInParameter(cmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(cmd, "@MandateId", DbType.Int32, mandateId);
                db.AddOutParameter(cmd, "co_OrderId", DbType.Int32, 10);
                if (db.ExecuteNonQuery(cmd) != 0)
                {
                    result = Convert.ToInt32(db.GetParameterValue(cmd, "co_OrderId").ToString());
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
                FunctionInfo.Add("Method", "DAO:CreateMandateOrder(int customerId, double Amount, string BankName, string BankBranch, int UserId)");

                object[] objects = new object[5];
                objects[0] = customerId;
                objects[1] = Amount;
                objects[2] = BankName;
                objects[3] = BankBranch;
                objects[4] = UserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        public int BSEMFSIPorderResponseParam(BSEMFSIPOdererVo bseMFSIPOdererVo, int rmId, int userId)
        {
            Database db;
            DbCommand cmd;
            int result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_CreateBSEMFSIPOrderRequest");
                db.AddInParameter(cmd, "@Transactioncode", DbType.String, bseMFSIPOdererVo.Transactioncode);
                db.AddInParameter(cmd, "@BSEOrderId", DbType.String, bseMFSIPOdererVo.BSEOrderId);
                db.AddInParameter(cmd, "@UniqueReferanceNumber", DbType.String, bseMFSIPOdererVo.UniqueReferanceNumber);
                db.AddInParameter(cmd, "@SchemeCode", DbType.String, bseMFSIPOdererVo.SchemeCode);
                db.AddInParameter(cmd, "@MemberId", DbType.String, bseMFSIPOdererVo.MemberId);
                db.AddInParameter(cmd, "@ClientCode", DbType.String, bseMFSIPOdererVo.ClientCode);
                db.AddInParameter(cmd, "@BSEUserId", DbType.String, bseMFSIPOdererVo.BSEUserId);
                db.AddInParameter(cmd, "@InternalReferenceNo", DbType.String, bseMFSIPOdererVo.InternalReferenceNo);
                db.AddInParameter(cmd, "@TransMode", DbType.String, bseMFSIPOdererVo.Transactioncode);
                db.AddInParameter(cmd, "@DPTransactionMode", DbType.String, bseMFSIPOdererVo.DPTransactionMode);
                db.AddInParameter(cmd, "@StartDate", DbType.String, bseMFSIPOdererVo.StartDate);
                db.AddInParameter(cmd, "@FrequenceType", DbType.String, bseMFSIPOdererVo.FrequenceType);
                db.AddInParameter(cmd, "@FrequenceAllowed", DbType.String, bseMFSIPOdererVo.FrequenceAllowed);
                db.AddInParameter(cmd, "@InstallmentType", DbType.String, bseMFSIPOdererVo.InstallmentType);
                db.AddInParameter(cmd, "@NoOfInstallments", DbType.String, bseMFSIPOdererVo.NoOfInstallments);
                db.AddInParameter(cmd, "@Remarks", DbType.String, bseMFSIPOdererVo.Remarks);
                db.AddInParameter(cmd, "@FolioNo", DbType.String, bseMFSIPOdererVo.FolioNo);
                db.AddInParameter(cmd, "@FirstOrderFlag", DbType.String, bseMFSIPOdererVo.FirstOrderFlag);
                db.AddInParameter(cmd, "@SubBRCode", DbType.String, bseMFSIPOdererVo.SubBRCode);
                db.AddInParameter(cmd, "@EUIN", DbType.String, bseMFSIPOdererVo.EUIN);
                db.AddInParameter(cmd, "@EUINDeclarationFlag", DbType.String, bseMFSIPOdererVo.EUINDeclarationFlag);
                db.AddInParameter(cmd, "@DPC", DbType.String, bseMFSIPOdererVo.DPC);
                db.AddInParameter(cmd, "@REGID", DbType.String, bseMFSIPOdererVo.REGID);
                db.AddInParameter(cmd, "@Password", DbType.String, bseMFSIPOdererVo.Password);
                db.AddInParameter(cmd, "@PassKey", DbType.String, bseMFSIPOdererVo.PassKey);
                db.AddInParameter(cmd, "@Param1", DbType.String, bseMFSIPOdererVo.Param1);
                db.AddInParameter(cmd, "@Param2", DbType.String, bseMFSIPOdererVo.Param2);
                db.AddInParameter(cmd, "@Param3", DbType.String, bseMFSIPOdererVo.Param3);
               
                db.AddInParameter(cmd, "@IPAddress", DbType.String, bseMFSIPOdererVo.IPAddress);
                db.AddInParameter(cmd, "@UserId", DbType.Int32, userId);

                db.AddOutParameter(cmd, "@OutSIPOrderId", DbType.Int32, 10);
                if (db.ExecuteNonQuery(cmd) != 0)
                {
                    result = Convert.ToInt32(db.GetParameterValue(cmd, "OutSIPOrderId").ToString());
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public void BSEMFSIPorderResponseParam(int RequestId, int userId, double BSEOrderId, string memberId, string ClientCode, string BSERemarks, string Successflag, int rmsId, string uniqueRefNo)
        {
            Database db;
            DbCommand BSEMFSIPorderResponseParamCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                BSEMFSIPorderResponseParamCmd = db.GetStoredProcCommand("SPROC_ONL_CreateBSEMFSIPOrderResponse");
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@UserID", DbType.Int32, userId);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@MemberId", DbType.String, memberId);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@ClientCode", DbType.String, ClientCode);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@BSESIPOrderId", DbType.Int64, BSEOrderId);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@Remarks", DbType.String, BSERemarks);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@Successflag", DbType.Int32, Successflag);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@ReqId", DbType.Int64, RequestId);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@rmsId", DbType.Int32, rmsId);
                db.AddInParameter(BSEMFSIPorderResponseParamCmd, "@UniqueRefNo", DbType.String, uniqueRefNo);
                db.ExecuteNonQuery(BSEMFSIPorderResponseParamCmd);
            }
            catch (Exception ex)
            {
            }
        }

        public DataSet BindMandateddetailsDetails(int AdviserId)
        {
            DataSet ds = new DataSet();
            Database db;
            DbCommand ViewMandatedetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                ViewMandatedetailsCmd = db.GetStoredProcCommand("SPROC_ONL_GetAdviserCustomerMandate");
                db.AddInParameter(ViewMandatedetailsCmd, "@AdviserId", DbType.Int32, AdviserId);
                ViewMandatedetailsCmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(ViewMandatedetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {

            }
            return ds;
        }

    
    }
}
