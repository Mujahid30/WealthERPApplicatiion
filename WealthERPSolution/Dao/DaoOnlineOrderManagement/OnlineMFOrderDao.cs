using System;
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
    public class OnlineMFOrderDao : OnlineOrderDao
    {
        public DataSet GetOrderBookMIS(int adviserId, int CustomerId, int AccountId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsOrderBookMIS;
            Database db;
            DbCommand GetOrderBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrderBookMISCmd = db.GetStoredProcCommand("SPROC_Onl_GetOrderBook");
                db.AddInParameter(GetOrderBookMISCmd, "@A_AdviserId", DbType.Int32, adviserId);
                if (AccountId != 0)
                    db.AddInParameter(GetOrderBookMISCmd, "@AccountId", DbType.Int32, AccountId);
                else
                    db.AddInParameter(GetOrderBookMISCmd, "@AccountId", DbType.Int32, DBNull.Value);
                db.AddInParameter(GetOrderBookMISCmd, "@C_CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(GetOrderBookMISCmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(GetOrderBookMISCmd, "@ToDate", DbType.DateTime, dtTo);
                //db.AddInParameter(GetOrderBookMISCmd, "@status", DbType.String, status);                
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
        public DataSet GetControlDetails(int Scheme, int folio)
        {
            DataSet dsGetControlDetails;
            Database db;
            DbCommand GetGetControlDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetControlDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_GetSchemeDetails");
                db.AddInParameter(GetGetControlDetailsCmd, "@schemecode", DbType.Int32, Scheme);
                db.AddInParameter(GetGetControlDetailsCmd, "@folio", DbType.Int32, folio);

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
                if (!string.IsNullOrEmpty(onlinemforderVo.FolioNumber))
                {
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@folioNumer", DbType.String, onlinemforderVo.FolioNumber);
                }
                else
                    db.AddInParameter(CreateCustomerOnlineMFOrderDetailsCmd, "@folioNumer", DbType.String, DBNull.Value);

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
        public DataSet GetSIPBookMIS(int adviserId, int CustomerId, int AccountId, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsSIPBookMIS;
            Database db;
            DbCommand GetSIPBookMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPBookMISCmd = db.GetStoredProcCommand("SPROC_Onl_GetSIPBook");
                db.AddInParameter(GetSIPBookMISCmd, "@A_AdviserId", DbType.Int32, adviserId);
                if (AccountId != 0)
                    db.AddInParameter(GetSIPBookMISCmd, "@AccountId", DbType.Int32, AccountId);
                else
                    db.AddInParameter(GetSIPBookMISCmd, "@AccountId", DbType.Int32, DBNull.Value);
                db.AddInParameter(GetSIPBookMISCmd, "@C_CustomerId", DbType.Int32, CustomerId);
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


        public List<int> CreateOrderMFSipDetails(OnlineMFOrderVo onlineMFOrderVo, int userId )
        {
            List<int> orderIds = new List<int>();
            int OrderId;
            Database db;
            DbCommand createMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SPROC_Onl_CreateCustomerOnlineMFSipDetails");

                db.AddInParameter(createMFOrderTrackingCmd, "@PASP_SchemePlanCode", DbType.Int32, onlineMFOrderVo.SchemePlanCode);                
              
                if (onlineMFOrderVo.AccountId != 0)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFA_accountid", DbType.Int32, onlineMFOrderVo.AccountId);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFA_accountid", DbType.Int32, 0);

                db.AddInParameter(createMFOrderTrackingCmd, "@XSTT_SystematicTypeCode", DbType.String, onlineMFOrderVo.SystematicTypeCode);
                if (onlineMFOrderVo.StartDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_StartDate", DbType.DateTime, onlineMFOrderVo.StartDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_StartDate", DbType.DateTime, DBNull.Value);
                if (onlineMFOrderVo.EndDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_EndDate", DbType.DateTime, onlineMFOrderVo.EndDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_EndDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_SystematicDate", DbType.Int32, onlineMFOrderVo.SystematicDate);

                db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_Amount", DbType.Double, onlineMFOrderVo.Amount);
                db.AddInParameter(createMFOrderTrackingCmd, "@XES_SourceCode", DbType.String, onlineMFOrderVo.SourceCode);
                 if (onlineMFOrderVo.FrequencyCode != null && onlineMFOrderVo.FrequencyCode != "")
                    db.AddInParameter(createMFOrderTrackingCmd, "@XF_FrequencyCode", DbType.String, onlineMFOrderVo.FrequencyCode);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@XF_FrequencyCode", DbType.String, DBNull.Value);
                 db.AddInParameter(createMFOrderTrackingCmd, "@UserId", DbType.Int32, userId);
                 db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_CreatedBy", DbType.Int32, userId);
                 db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_ModifiedBy", DbType.Int32, userId);
                 db.AddInParameter(createMFOrderTrackingCmd, "@CMFSS_SubBrokerCode", DbType.Int32, onlineMFOrderVo.AgentCode);
                db.AddInParameter(createMFOrderTrackingCmd, "@customerId", DbType.Int32, onlineMFOrderVo.CustomerId);
                db.AddInParameter(createMFOrderTrackingCmd, "@systamaticDates", DbType.String, onlineMFOrderVo.SystematicDates);

                db.AddOutParameter(createMFOrderTrackingCmd, "@CO_OrderId", DbType.Int32, 10);

                           

               


                if (db.ExecuteNonQuery(createMFOrderTrackingCmd) != 0)
                {
                    //OrderId = Convert.ToInt32(db.GetParameterValue(createMFOrderTrackingCmd, "CO_OrderId").ToString());

                   // orderIds.Add(OrderId);
                }
                else
                {
                    orderIds = null;
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderIds;
                      
        }


        public DataSet GetSipDetails(int schemeId, string frequency)
        {
            DataSet dsSipDetails;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_Onl_GetSipDetails");
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

    }
}



    

