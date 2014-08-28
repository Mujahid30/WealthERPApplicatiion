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


        public DataSet GetOrderBookMIS(int CustomerId, int AmcCode, string OrderStatus, DateTime dtFrom, DateTime dtTo)
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

        public DataSet GetFolioAccount(int CustomerId)
        {
            DataSet dsFolioAccount;
            Database db;
            DbCommand GetFolioAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetFolioAccountCmd = db.GetStoredProcCommand("SPROC_Onl_BindFolioAccount");
                db.AddInParameter(GetFolioAccountCmd, "@C_CustomerId", DbType.Int32, CustomerId);
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
        public DataSet GetControlDetails(int Scheme, string folio)
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
                if (onlinemforderVo.IsAllUnits==true)
                {
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@IsAllUnits", DbType.Boolean, bool.Parse(onlinemforderVo.IsAllUnits.ToString()));
                }
                else
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@IsAllUnits", DbType.Boolean, 0);

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
                db.AddInParameter(CreateOnlineMFSwitchOrderDetailsCmd, "@xmlOrderDetails", DbType.Xml,sb);
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
        public DataSet GetSIPBookMIS(int CustomerId, int AmcCode, string OrderStatus,int systematicId,DateTime dtFrom, DateTime dtTo)
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
                if (!string.IsNullOrEmpty(onlineMFOrderVo.SWPRedeemValueType.Trim()))
                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_SWPRedeemValueType", DbType.String, onlineMFOrderVo.SWPRedeemValueType);
                
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


        public DataSet GetSipDetails(int schemeId, string frequency)
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
        public DataSet GetRedeemAmcDetails(int customerId)
        {
            DataSet dsGetRedeemSchemeDetails;
            Database db;
            DbCommand GetRedeemSchemeDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetRedeemSchemeDetailscmd = db.GetStoredProcCommand("SPROC_ONL_GetRedeemAmcDetails");
                db.AddInParameter(GetRedeemSchemeDetailscmd, "@customerId", DbType.Int32, customerId);
                dsGetRedeemSchemeDetails = db.ExecuteDataSet(GetRedeemSchemeDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetRedeemSchemeDetails;
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
        public DataSet GetSIPSummaryBookMIS(int CustomerId, int AmcCode,DateTime dtFrom, DateTime dtTo)
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
                //if (OrderStatus != "0")
                //    db.AddInParameter(GetSIPSummaryBookMISCmd, "@Status", DbType.String, OrderStatus);
                //else
                //    db.AddInParameter(GetSIPSummaryBookMISCmd, "@Status", DbType.String, DBNull.Value);
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
        public void TriggerAutoOrderFromSIP()
        {
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_CreateAutoSipOrder");
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
        public DataSet GetSIPAmcDetails(int customerId)
        {
            DataSet dsGetSIPAmcDetails;
            Database db;
            DbCommand GetSIPAmcDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPAmcDetailscmd = db.GetStoredProcCommand("sproc_onl_BindAmcSIP");
                db.AddInParameter(GetSIPAmcDetailscmd, "@customerId", DbType.Int32, customerId);
                dsGetSIPAmcDetails = db.ExecuteDataSet(GetSIPAmcDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetSIPAmcDetails;
        }
        public DataSet GetOrderAmcDetails(int customerId)
        {
            DataSet dsGetOrderAmcDetails;
            Database db;
            DbCommand GetOrderAmcDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderAmcDetailscmd = db.GetStoredProcCommand("sproc_onl_BindAmcOrder");
                db.AddInParameter(GetOrderAmcDetailscmd, "@customerId", DbType.Int32, customerId);
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

        public bool UpdateCnacleRegisterSIP(int systematicId,int is_Cancel,string remark, int cancelBy)
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
        public DataTable GetMFSchemeDetailsForLanding(int Schemeplancode,string category)
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
                if (category!= "0")
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

        public DataSet GetCustomerSchemeFolioHoldings(int customerId, int schemeId, out string schemeDividendOption)
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


    }
}
