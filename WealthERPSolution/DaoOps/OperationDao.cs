using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VoOps;
using System.Collections.Specialized;


namespace DaoOps
{
    public class OperationDao
    {
        public DataSet GetOrderStatus()
        {
            DataSet dsOrderStatus;
            Database db;
            DbCommand getOrderStatusCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getOrderStatusCmd = db.GetStoredProcCommand("SP_GetOrderStatus");
                dsOrderStatus = db.ExecuteDataSet(getOrderStatusCmd);
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetOrderStatus()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
             }
            return dsOrderStatus;
        }

        public DataSet GetMfOrderRecon(DateTime fromDate, DateTime toDate, string orderStatus, string orderType)
        {
            DataSet dsOrderRecon;
            Database db;
            DbCommand getOrderReconCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getOrderReconCmd = db.GetStoredProcCommand("SP_GetMFOrderRecon");
                db.AddInParameter(getOrderReconCmd, "@FromDate", DbType.Date, fromDate);
                db.AddInParameter(getOrderReconCmd, "@ToDate", DbType.Date, toDate);
                db.AddInParameter(getOrderReconCmd, "@OrderStatus", DbType.String, orderStatus);
                db.AddInParameter(getOrderReconCmd, "@OrderType", DbType.String, orderType);
                dsOrderRecon = db.ExecuteDataSet(getOrderReconCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderRecon()");
                object[] objects = new object[4];
                objects[0] = fromDate;
                objects[1] = toDate;
                objects[2] = orderStatus;
                objects[2] = orderType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderRecon;
        }

        public DataSet GetTransactionType()
        {
            DataSet dsTrxType;
            Database db;
            DbCommand getTrxTypeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getTrxTypeCmd = db.GetStoredProcCommand("SP_GetMFTransactionType");
                dsTrxType = db.ExecuteDataSet(getTrxTypeCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetTransactionType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsTrxType;
        }

        public DataSet GetAssetType()
        {
            DataSet dsAssetType;
            Database db;
            DbCommand getAssetTypeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetTypeCmd = db.GetStoredProcCommand("SP_GetProductAssetGroup");
                dsAssetType = db.ExecuteDataSet(getAssetTypeCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetAssetType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAssetType;
        }



        public DataSet GetOrderMIS(int adviserId, string branchId, string rmId, string transactionType, string status, string orderType,string amcCode,DateTime dtFrom,DateTime dtTo, int currentPage, out int count)
        {
            DataSet dsOrderMIS;
            Database db;
            DbCommand getOrderMISCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getOrderMISCmd = db.GetStoredProcCommand("SP_GetOrderMIS");
                db.AddInParameter(getOrderMISCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getOrderMISCmd, "@branchId", DbType.String, branchId);
                db.AddInParameter(getOrderMISCmd, "@rmId", DbType.String, rmId);
                if(!string.IsNullOrEmpty(transactionType.ToString().Trim()))
                     db.AddInParameter(getOrderMISCmd, "@trxType", DbType.String, transactionType);
                else
                    db.AddInParameter(getOrderMISCmd, "@trxType", DbType.String, DBNull.Value);
                db.AddInParameter(getOrderMISCmd, "@orderStatus", DbType.String, status);
                db.AddInParameter(getOrderMISCmd, "@ordertype", DbType.String, orderType);
                if (!string.IsNullOrEmpty(amcCode.ToString().Trim()))
                    db.AddInParameter(getOrderMISCmd, "@amcCode", DbType.String, amcCode);
                else
                    db.AddInParameter(getOrderMISCmd, "@amcCode", DbType.String, DBNull.Value);
                db.AddInParameter(getOrderMISCmd, "@fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(getOrderMISCmd, "@todate", DbType.DateTime, dtTo);
                db.AddInParameter(getOrderMISCmd, "@currentPage", DbType.Int32, currentPage);
                db.AddOutParameter(getOrderMISCmd, "@Count", DbType.Int32, 0);
                getOrderMISCmd.CommandTimeout = 60 * 60;
                dsOrderMIS = db.ExecuteDataSet(getOrderMISCmd);
                count = (int)db.GetParameterValue(getOrderMISCmd, "@Count");

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetOrderMIS()");
                object[] objects = new object[10];
                objects[0] = adviserId;
                objects[1] = branchId;
                objects[2] = rmId;
                objects[3] = transactionType;
                objects[4] = status;
                objects[5] = orderType;
                objects[6] = amcCode;
                objects[7] = dtFrom;
                objects[8] = dtTo;
                objects[9] = currentPage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderMIS;
        }

        public DataSet GetOrderMannualMatch(int scheme, int accountId, string type, double amount, DateTime orderDate, int customerId, int schemeSwitch)
        {
            DataSet dsmannualMatch;
            Database db;
            DbCommand getMannualMatchcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMannualMatchcmd = db.GetStoredProcCommand("SP_GetMannualOrderMapping");
                db.AddInParameter(getMannualMatchcmd, "@schemeCode", DbType.Int32, scheme);
                db.AddInParameter(getMannualMatchcmd, "@accountId", DbType.Int32, accountId);
                db.AddInParameter(getMannualMatchcmd, "@transactiontype", DbType.String, type);
                db.AddInParameter(getMannualMatchcmd, "@amount", DbType.Double, amount);
                db.AddInParameter(getMannualMatchcmd, "@orderDate", DbType.DateTime, orderDate);
                db.AddInParameter(getMannualMatchcmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(getMannualMatchcmd, "@schemeSwitchCode", DbType.Int32, schemeSwitch);
                dsmannualMatch = db.ExecuteDataSet(getMannualMatchcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetOrderMannualMatch()");
                object[] objects = new object[6];
                objects[0] = scheme;
                objects[1] = accountId;
                objects[2] = type;
                objects[3] = amount;
                objects[4] = orderDate;
                objects[5] = schemeSwitch;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsmannualMatch;
        }

        public DataSet GetRejectStatus(string statusCode)
        {
            DataSet dsStatusReject;
            Database db;
            DbCommand getStatusRejectcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getStatusRejectcmd = db.GetStoredProcCommand("SP_GetOrderStatusReject");
                db.AddInParameter(getStatusRejectcmd, "@statuscode", DbType.String, statusCode);
                dsStatusReject = db.ExecuteDataSet(getStatusRejectcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetRejectStatus()");
                object[] objects = new object[1];
                objects[0] = statusCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsStatusReject;
        }

        public DataSet GetAMCForOrderEntry(int flag, int customerId)
        {
            DataSet dsProductAMC;
            Database db;
            DbCommand getProductAMCcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getProductAMCcmd = db.GetStoredProcCommand("SP_GetAMCForOrderEntry");
                db.AddInParameter(getProductAMCcmd, "@flag", DbType.Int16, flag);
                db.AddInParameter(getProductAMCcmd, "@customerId", DbType.Int32, customerId);
                getProductAMCcmd.CommandTimeout = 60 * 60;
                dsProductAMC = db.ExecuteDataSet(getProductAMCcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetAMCForOrderEntry()");
                object[] objects = new object[2];
                objects[0] = flag;
                objects[1] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsProductAMC;
        }

        public DataSet GetSchemeForOrderEntry(int amcCode, string categoryCode, int Sflag, int customerId)
        {
            DataSet dsScheme;
            Database db;
            DbCommand getSchemecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemecmd = db.GetStoredProcCommand("SP_GetSchemeForOrderEntry");
                db.AddInParameter(getSchemecmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(getSchemecmd, "@categoryCode", DbType.String, categoryCode);
                db.AddInParameter(getSchemecmd, "@flag", DbType.Int16, Sflag);
                db.AddInParameter(getSchemecmd, "@customerId", DbType.Int32, customerId);
                getSchemecmd.CommandTimeout = 60 * 60;
                dsScheme = db.ExecuteDataSet(getSchemecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetSchemeForOrderEntry()");
                object[] objects = new object[4];
                objects[0] = amcCode;
                objects[1] = categoryCode;
                objects[2] = Sflag;
                objects[3] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsScheme;
        }

        public DataSet GetFolioForOrderEntry(int portfolioId, int amcCode, int all, int Fflag, int customerId)
        {
            DataSet dsfolio;
            Database db;
            DbCommand getdsfoliocmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getdsfoliocmd = db.GetStoredProcCommand("SP_GetFolioForOrderEntry");
                db.AddInParameter(getdsfoliocmd, "@portfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getdsfoliocmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(getdsfoliocmd, "@all", DbType.Int32, all);
                db.AddInParameter(getdsfoliocmd, "@flag", DbType.Int16, Fflag);
                db.AddInParameter(getdsfoliocmd, "@customerId", DbType.Int32, customerId);
                getdsfoliocmd.CommandTimeout = 60 * 60;
                dsfolio = db.ExecuteDataSet(getdsfoliocmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetFolioForOrderEntry()");
                object[] objects = new object[5];
                objects[0] = portfolioId;
                objects[1] = amcCode;
                objects[2] = all;
                objects[3] = Fflag;
                objects[4] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsfolio;
        }

        public DataSet GetAmountUnits(int schemePlanCode, int customerId)
        {
            DataSet dsAmountUnits;
            Database db;
            DbCommand getdsfoliocmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getdsfoliocmd = db.GetStoredProcCommand("SP_GetAmountUnitsForOrderEntry");
                db.AddInParameter(getdsfoliocmd, "@schemeCode", DbType.Int32, schemePlanCode);
                db.AddInParameter(getdsfoliocmd, "@customerId", DbType.Int32, customerId);
                getdsfoliocmd.CommandTimeout = 60 * 60;
                dsAmountUnits = db.ExecuteDataSet(getdsfoliocmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetAmountUnits()");
                object[] objects = new object[2];
                objects[0] = schemePlanCode;
                objects[1] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAmountUnits;
        }

        public DataSet GetSwitchScheme(int amcCode)
        {
            DataSet dsSchemeSwitch;
            Database db;
            DbCommand getSchemeSwitchcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeSwitchcmd = db.GetStoredProcCommand("SP_GetSwitchSchemeForOrderEntry");
                db.AddInParameter(getSchemeSwitchcmd, "@amcCode", DbType.Int32, amcCode);
                dsSchemeSwitch = db.ExecuteDataSet(getSchemeSwitchcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetSwitchScheme()");
                object[] objects = new object[1];
                objects[0] = amcCode;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeSwitch;
        }

        public bool CreateMFOrderTracking(VoOps.OperationVo operationVo)
        {
            bool bResult = false;
            Database db;
            DbCommand createMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SP_CreateMFOrderTracking");

                db.AddInParameter(createMFOrderTrackingCmd, "@customerId", DbType.Int32, operationVo.CustomerId);
                db.AddInParameter(createMFOrderTrackingCmd, "@schemeCode", DbType.Int32, operationVo.SchemePlanCode);
                db.AddInParameter(createMFOrderTrackingCmd, "@orderNumber", DbType.Int32, operationVo.OrderNumber);
                db.AddInParameter(createMFOrderTrackingCmd, "@amount", DbType.Double, operationVo.Amount);
                db.AddInParameter(createMFOrderTrackingCmd, "@statusCode", DbType.String, operationVo.StatusCode);
                db.AddInParameter(createMFOrderTrackingCmd, "@StatusReasonCode", DbType.String, operationVo.StatusReasonCode);
                if(operationVo.accountid !=0)
                    db.AddInParameter(createMFOrderTrackingCmd, "@accountid", DbType.Int32, operationVo.accountid);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@accountid", DbType.Int32, 0);
                db.AddInParameter(createMFOrderTrackingCmd, "@TransactionCode", DbType.String, operationVo.TransactionCode);
                db.AddInParameter(createMFOrderTrackingCmd, "@OrderDate", DbType.DateTime, operationVo.OrderDate);
                db.AddInParameter(createMFOrderTrackingCmd, "@IsImmediate", DbType.Int16, operationVo.IsImmediate);
                //db.AddInParameter(createMFOrderTrackingCmd, "@SourceCode", DbType.String, operationVo.SourceCode);
                if(!string.IsNullOrEmpty(operationVo.FutureTriggerCondition.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@FutureTriggerCondition", DbType.String, operationVo.FutureTriggerCondition);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@FutureTriggerCondition", DbType.String, DBNull.Value);
                db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationNumber", DbType.String, operationVo.ApplicationNumber);
                db.AddInParameter(createMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, operationVo.ApplicationReceivedDate);
                db.AddInParameter(createMFOrderTrackingCmd, "@portfolioId", DbType.Int32, operationVo.portfolioId);
                db.AddInParameter(createMFOrderTrackingCmd, "@PaymentMode", DbType.String, operationVo.PaymentMode);
                if (!string.IsNullOrEmpty(operationVo.ChequeNumber.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@ChequeNumber", DbType.String, operationVo.ChequeNumber);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@ChequeNumber", DbType.String, DBNull.Value);
                if (operationVo.PaymentDate!=DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, operationVo.PaymentDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, DBNull.Value);
                if (operationVo.FutureExecutionDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@FutureExecutionDate", DbType.DateTime, operationVo.FutureExecutionDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@FutureExecutionDate", DbType.DateTime, DBNull.Value);
                if(operationVo.SchemePlanSwitch !=0)
                    db.AddInParameter(createMFOrderTrackingCmd, "@SchemePlanSwitch", DbType.Int32, operationVo.SchemePlanSwitch);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@SchemePlanSwitch", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.BankName.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@BankName", DbType.String, operationVo.BankName);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@BankName", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.BranchName.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@BranchName", DbType.String, operationVo.BranchName);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@BranchName", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.AddrLine1.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@AddrLine1", DbType.String, operationVo.AddrLine1);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@AddrLine1", DbType.String, DBNull.Value);
                if (operationVo.AddrLine2!=null)
                    db.AddInParameter(createMFOrderTrackingCmd, "@AddrLine2", DbType.String, operationVo.AddrLine2);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@AddrLine2", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.AddrLine3.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@AddrLine3", DbType.String, operationVo.AddrLine3);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@AddrLine3", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.City.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@City", DbType.String, operationVo.City);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@City", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.State.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@State", DbType.String, operationVo.State);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@State", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.Country.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@Country", DbType.String, operationVo.Country);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@Country", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.Pincode.ToString().Trim()))
                    db.AddInParameter(createMFOrderTrackingCmd, "@Pincode", DbType.String, operationVo.Pincode);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@Pincode", DbType.String, DBNull.Value);
                if (operationVo.LivingSince!=DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@LivingSince", DbType.DateTime, operationVo.LivingSince);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@LivingSince", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createMFOrderTrackingCmd, "@IsExecuted ", DbType.Int16, operationVo.IsExecuted);
                if (operationVo.FrequencyCode != null && operationVo.FrequencyCode !="" )
                    db.AddInParameter(createMFOrderTrackingCmd, "@FrequencyCode", DbType.String, operationVo.FrequencyCode);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@FrequencyCode", DbType.String, DBNull.Value);
                if (operationVo.StartDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@StartDate", DbType.DateTime, operationVo.StartDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@StartDate", DbType.DateTime, DBNull.Value);
                if (operationVo.EndDate != DateTime.MinValue)
                    db.AddInParameter(createMFOrderTrackingCmd, "@EndDate", DbType.DateTime, operationVo.EndDate);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@EndDate", DbType.DateTime, DBNull.Value);
                if(operationVo.Units!=0)
                    db.AddInParameter(createMFOrderTrackingCmd, "@units", DbType.Double, operationVo.Units);
                else
                    db.AddInParameter(createMFOrderTrackingCmd, "@units", DbType.Double, DBNull.Value);
                 db.ExecuteNonQuery(createMFOrderTrackingCmd);

                bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public int GetOrderNumber()
        {
            DataSet dsOrderNumber;
            DataTable dtOrderNumber;
            int orderNumber = 0;
            Database db;
            DbCommand getSchemeSwitchcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeSwitchcmd = db.GetStoredProcCommand("SP_GetOrderNumber");
                dsOrderNumber = db.ExecuteDataSet(getSchemeSwitchcmd);
                dtOrderNumber = dsOrderNumber.Tables[0];
                if (dtOrderNumber.Rows.Count > 0)
                    orderNumber = int.Parse(dtOrderNumber.Rows[0]["CMOT_MFOrderId"].ToString());
                else
                    orderNumber = 999;
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
            return orderNumber;
        }

        public OperationVo GetCustomerOrderTrackingDetails(int orderId)
        {
            OperationVo operationVo = new OperationVo(); ;
            Database db;
            DbCommand getCustomerOrderTrackingDetailsCmd;
            DataSet dsCustomerOrderTrackingDetails;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerOrderTrackingDetailsCmd = db.GetStoredProcCommand("SP_GetCustomerOrderTrackingDetails");
                db.AddInParameter(getCustomerOrderTrackingDetailsCmd, "@orderId", DbType.Int32, orderId);
                getCustomerOrderTrackingDetailsCmd.CommandTimeout = 60 * 60;
                dsCustomerOrderTrackingDetails = db.ExecuteDataSet(getCustomerOrderTrackingDetailsCmd);
                if (dsCustomerOrderTrackingDetails.Tables[0].Rows.Count > 0)
                {

                    dr = dsCustomerOrderTrackingDetails.Tables[0].Rows[0];

                    operationVo.OrderId = int.Parse(dr["CMOT_MFOrderId"].ToString());
                    operationVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    operationVo.CustomerName = dr["Customer_Name"].ToString();
                    operationVo.RMName = dr["RM_Name"].ToString();
                    operationVo.BMName = dr["AB_BranchName"].ToString();
                    operationVo.PanNo = dr["C_PANNum"].ToString();
                    if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString().Trim()))
                        operationVo.Amccode = int.Parse(dr["PA_AMCCode"].ToString());
                    else
                        operationVo.Amccode =0;
                    if (!string.IsNullOrEmpty(dr["PAIC_AssetInstrumentCategoryCode"].ToString().Trim()))
                        operationVo.category = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanCode"].ToString().Trim()))
                        operationVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    operationVo.OrderNumber =int.Parse( dr["CMOT_OrderNumber"].ToString());
                    operationVo.Amount = double.Parse(dr["CMOT_Amount"].ToString());
                    //if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
                    //    operationVo.Units = double.Parse(dr["CMFNP_NetHoldings"].ToString());
                    //else
                    //    operationVo.Units = 0;
                    operationVo.StatusCode = dr["XS_StatusCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["XSR_StatusReasonCode"].ToString()))
                        operationVo.StatusReasonCode = dr["XSR_StatusReasonCode"].ToString();
                    else
                        operationVo.StatusReasonCode = "";
                    if (int.Parse(dr["CMFA_accountid"].ToString()) != 0)
                        operationVo.accountid = int.Parse(dr["CMFA_accountid"].ToString());
                    else
                        operationVo.accountid = 0;
                    operationVo.TransactionCode = dr["WMTT_TransactionClassificationCode"].ToString();
                    operationVo.OrderDate = DateTime.Parse(dr["CMOT_OrderDate"].ToString());
                    operationVo.IsImmediate = int.Parse(dr["CMOT_IsImmediate"].ToString());
                    operationVo.ApplicationNumber = dr["CMOT_ApplicationNumber"].ToString();
                    operationVo.ApplicationReceivedDate = DateTime.Parse(dr["CMOT_ApplicationReceivedDate"].ToString());
                    operationVo.portfolioId = int.Parse(dr["CP_portfolioId"].ToString());
                    operationVo.PaymentMode = dr["CMOT_PaymentMode"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMOT_ChequeNumber"].ToString()))
                        operationVo.ChequeNumber = dr["CMOT_ChequeNumber"].ToString();
                    else
                        operationVo.ChequeNumber = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_PaymentDate"].ToString()))
                        operationVo.PaymentDate = DateTime.Parse(dr["CMOT_PaymentDate"].ToString());
                    else
                        operationVo.PaymentDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["CMOT_FutureTriggerCondition"].ToString()))
                        operationVo.FutureTriggerCondition = dr["CMOT_FutureTriggerCondition"].ToString();
                    else
                        operationVo.FutureTriggerCondition = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_FutureExecutionDate"].ToString()))
                        operationVo.FutureExecutionDate = DateTime.Parse(dr["CMOT_FutureExecutionDate"].ToString());
                    else
                        operationVo.FutureExecutionDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["PASP_SchemePlanSwitch"].ToString()))
                        operationVo.SchemePlanSwitch = int.Parse(dr["PASP_SchemePlanSwitch"].ToString());
                    else
                        operationVo.SchemePlanSwitch = 0;
                    if (!string.IsNullOrEmpty(dr["CMOT_BankName"].ToString()))
                        operationVo.BankName = dr["CMOT_BankName"].ToString();
                    else
                        operationVo.BankName = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_BranchName"].ToString()))
                        operationVo.BranchName = dr["CMOT_BranchName"].ToString();
                    else
                        operationVo.BranchName = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_AddrLine1"].ToString()))
                        operationVo.AddrLine1 = dr["CMOT_AddrLine1"].ToString();
                    else
                        operationVo.AddrLine1 ="";
                    if (!string.IsNullOrEmpty(dr["CMOT_AddrLine2"].ToString()))
                        operationVo.AddrLine2 = dr["CMOT_AddrLine2"].ToString();
                    else
                        operationVo.AddrLine2 = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_AddrLine3"].ToString()))
                        operationVo.AddrLine3 = dr["CMOT_AddrLine3"].ToString();
                    else
                        operationVo.AddrLine3 = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_City"].ToString()))
                        operationVo.City = dr["CMOT_City"].ToString();
                    else
                        operationVo.City = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_State"].ToString()))
                        operationVo.State = dr["CMOT_State"].ToString();
                    else
                        operationVo.State = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_Country"].ToString()))
                        operationVo.Country = dr["CMOT_Country"].ToString();
                    else
                        operationVo.Country = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_Pincode"].ToString()))
                        operationVo.Pincode = dr["CMOT_Pincode"].ToString();
                    else
                        operationVo.Pincode = "";
                    if (!string.IsNullOrEmpty(dr["CMOT_LivingSince"].ToString()))
                        operationVo.LivingSince = DateTime.Parse(dr["CMOT_LivingSince"].ToString());
                    else
                        operationVo.LivingSince = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["XF_FrequencyCode"].ToString()))
                        operationVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                    else
                        operationVo.FrequencyCode ="";
                    if (!string.IsNullOrEmpty(dr["CMOT_StartDate"].ToString()))
                        operationVo.StartDate= DateTime.Parse(dr["CMOT_StartDate"].ToString());
                    else
                        operationVo.StartDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["CMOT_EndDate"].ToString()))
                        operationVo.EndDate = DateTime.Parse(dr["CMOT_EndDate"].ToString());
                    else
                        operationVo.EndDate = DateTime.MinValue;

                    if (!string.IsNullOrEmpty(dr["CMOT_Units"].ToString()))
                        operationVo.Units = double.Parse(dr["CMOT_Units"].ToString());
                    else
                        operationVo.Units = 0;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return operationVo;
        }

        public bool UpdateMFTransactionForSynch(int gvOrderId, int gvSchemeCode, int gvaccountId, string gvTrxType, int gvPortfolioId, double gvAmount, out bool status, DateTime gvOrderDate)
        {
            Database db;
            DbCommand updateMFTransactionForSynchCmd;
            int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFTransactionForSynchCmd = db.GetStoredProcCommand("SP_UpdateMFTransactionForSync");
                db.AddInParameter(updateMFTransactionForSynchCmd, "@orderId", DbType.Int32, gvOrderId);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@schemeCode", DbType.Int32, gvSchemeCode);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@accountId", DbType.Int32, gvaccountId);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@trxType", DbType.String, gvTrxType);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@portfolioId", DbType.Int32, gvPortfolioId);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@amount", DbType.Double, gvAmount);
                db.AddInParameter(updateMFTransactionForSynchCmd, "@orderDate", DbType.DateTime, gvOrderDate);
                db.AddOutParameter(updateMFTransactionForSynchCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(updateMFTransactionForSynchCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(updateMFTransactionForSynchCmd, "@IsSuccess").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:UpdateMFTransactionForSynch()");
                object[] objects = new object[7];
                objects[0] = gvOrderId;
                objects[1] = gvSchemeCode;
                objects[2] = gvaccountId;
                objects[3] = gvTrxType;
                objects[4] = gvPortfolioId;
                objects[5] = gvAmount;
                objects[6] = gvOrderDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (affectedRecords > 0)
                return status = true;
            else
                return status = false;
        }

        public bool UpdateOrderTracking(OperationVo operationVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
               updateMFOrderTrackingCmd = db.GetStoredProcCommand("SP_UpdateMFOrderTracking");
               db.AddInParameter(updateMFOrderTrackingCmd, "@orderId", DbType.Int32, operationVo.OrderId);
               db.AddInParameter(updateMFOrderTrackingCmd, "@customerId", DbType.Int32, operationVo.CustomerId);
               db.AddInParameter(updateMFOrderTrackingCmd, "@schemeCode", DbType.Int32, operationVo.SchemePlanCode);
               db.AddInParameter(updateMFOrderTrackingCmd, "@orderNumber", DbType.Int32, operationVo.OrderNumber);
               db.AddInParameter(updateMFOrderTrackingCmd, "@amount", DbType.Double, operationVo.Amount);
               db.AddInParameter(updateMFOrderTrackingCmd, "@statusCode", DbType.String, operationVo.StatusCode);
               db.AddInParameter(updateMFOrderTrackingCmd, "@StatusReasonCode", DbType.String, operationVo.StatusReasonCode);
               if(operationVo.accountid !=0)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@accountid", DbType.Int32, operationVo.accountid);
               else
                   db.AddInParameter(updateMFOrderTrackingCmd, "@accountid", DbType.Int32, 0);
               db.AddInParameter(updateMFOrderTrackingCmd, "@TransactionCode", DbType.String, operationVo.TransactionCode);
               db.AddInParameter(updateMFOrderTrackingCmd, "@OrderDate", DbType.DateTime, operationVo.OrderDate);
               db.AddInParameter(updateMFOrderTrackingCmd, "@IsImmediate", DbType.Int16, operationVo.IsImmediate);
               //db.AddInParameter(updateMFOrderTrackingCmd, "@SourceCode", DbType.String, operationVo.SourceCode);
                if (!string.IsNullOrEmpty(operationVo.FutureTriggerCondition.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@FutureTriggerCondition", DbType.String, operationVo.FutureTriggerCondition);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@FutureTriggerCondition", DbType.String, DBNull.Value);
                db.AddInParameter(updateMFOrderTrackingCmd, "@ApplicationNumber", DbType.String, operationVo.ApplicationNumber);
                db.AddInParameter(updateMFOrderTrackingCmd, "@ApplicationReceivedDate", DbType.DateTime, operationVo.ApplicationReceivedDate);
                db.AddInParameter(updateMFOrderTrackingCmd, "@portfolioId", DbType.Int32, operationVo.portfolioId);
                db.AddInParameter(updateMFOrderTrackingCmd, "@PaymentMode", DbType.String, operationVo.PaymentMode);
                if (!string.IsNullOrEmpty(operationVo.ChequeNumber.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@ChequeNumber", DbType.String, operationVo.ChequeNumber);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@ChequeNumber", DbType.String, DBNull.Value);
                if (operationVo.PaymentDate != DateTime.MinValue)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, operationVo.PaymentDate);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@PaymentDate", DbType.DateTime, DBNull.Value);
                if (operationVo.FutureExecutionDate != DateTime.MinValue)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@FutureExecutionDate", DbType.DateTime, operationVo.FutureExecutionDate);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@FutureExecutionDate", DbType.DateTime, DBNull.Value);
                if (operationVo.SchemePlanSwitch != 0)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@SchemePlanSwitch", DbType.Int32, operationVo.SchemePlanSwitch);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@SchemePlanSwitch", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.BankName.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@BankName", DbType.String, operationVo.BankName);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@BankName", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.BranchName.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@BranchName", DbType.String, operationVo.BranchName);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@BranchName", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.AddrLine1.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@AddrLine1", DbType.String, operationVo.AddrLine1);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@AddrLine1", DbType.String, DBNull.Value);
                if (operationVo.AddrLine2 != null)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@AddrLine2", DbType.String, operationVo.AddrLine2);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@AddrLine2", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.AddrLine3.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@AddrLine3", DbType.String, operationVo.AddrLine3);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@AddrLine3", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.City.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@City", DbType.String, operationVo.City);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@City", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.State.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@State", DbType.String, operationVo.State);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@State", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.Country.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@Country", DbType.String, operationVo.Country);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@Country", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(operationVo.Pincode.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@Pincode", DbType.String, operationVo.Pincode);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@Pincode", DbType.String, DBNull.Value);
                if (operationVo.LivingSince != DateTime.MinValue)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@LivingSince", DbType.DateTime, operationVo.LivingSince);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@LivingSince", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateMFOrderTrackingCmd, "@IsExecuted ", DbType.Int16, operationVo.IsExecuted);

                if (!string.IsNullOrEmpty(operationVo.FrequencyCode.ToString().Trim()))
                    db.AddInParameter(updateMFOrderTrackingCmd, "@FrequencyCode", DbType.String, operationVo.FrequencyCode);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@FrequencyCode", DbType.String, DBNull.Value);
                if (operationVo.StartDate != DateTime.MinValue)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@StartDate", DbType.DateTime, operationVo.StartDate);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@StartDate", DbType.DateTime, DBNull.Value);
                if (operationVo.EndDate != DateTime.MinValue)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@EndDate", DbType.DateTime, operationVo.EndDate);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@EndDate", DbType.DateTime, DBNull.Value);

                if (operationVo.Units != 0)
                    db.AddInParameter(updateMFOrderTrackingCmd, "@units", DbType.Double, operationVo.Units);
                else
                    db.AddInParameter(updateMFOrderTrackingCmd, "@units", DbType.Double, DBNull.Value);

                db.ExecuteNonQuery(updateMFOrderTrackingCmd);

                bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public bool OrderMannualMatch(int OrderId, int transId, int SchemeCode, double amount, out bool status, string TrxType)
        {
            Database db;
            int affectedRecords = 0;
            DbCommand getOrderMannualMatchCmd;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getOrderMannualMatchCmd = db.GetStoredProcCommand("SP_GetOrderMannualMatch");
                db.AddInParameter(getOrderMannualMatchCmd, "@orderId", DbType.Int32, OrderId);
                db.AddInParameter(getOrderMannualMatchCmd, "@transId", DbType.Int32, transId);
                db.AddInParameter(getOrderMannualMatchCmd, "@schemeCode", DbType.Int32, SchemeCode);
                //db.AddInParameter(getOrderMannualMatchCmd, "@portfolioId", DbType.Int32, PortfolioId);
                db.AddInParameter(getOrderMannualMatchCmd, "@amount", DbType.Double, amount);
                db.AddInParameter(getOrderMannualMatchCmd, "@type", DbType.String, TrxType);
                db.AddOutParameter(getOrderMannualMatchCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(getOrderMannualMatchCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(getOrderMannualMatchCmd, "@IsSuccess").ToString());
  
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:OrderMannualMatch()");
                object[] objects = new object[5];
                objects[0] = OrderId;
                objects[1] = transId;
                objects[2] = SchemeCode;
                objects[3] = amount;
                objects[4] = TrxType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (affectedRecords > 0)
                return status=true;
            else
                return status=false;
        }

        /// <summary>
        /// Added to check PDF Form Availabilty
        /// </summary>
        /// <param name="transactionType"></param>
        /// <param name="SchemeName"></param>
        /// <returns></returns>
        public DataTable CheckPDFFormAvailabilty(string transactionType, int schemeCode)
        {
            Database db;
            DataSet dsPdfForms = new DataSet();
            DataTable dtPdfForms = new DataTable();
            DbCommand CheckPDFFormAvailabiltyCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckPDFFormAvailabiltyCmd = db.GetStoredProcCommand("SP_CheckFillablePDFFormAvailability");
                db.AddInParameter(CheckPDFFormAvailabiltyCmd, "@PDF_TransactionType", DbType.String, transactionType);
                db.AddInParameter(CheckPDFFormAvailabiltyCmd, "@PASP_SchemePlanCode", DbType.Int32, schemeCode);
                CheckPDFFormAvailabiltyCmd.CommandTimeout = 60 * 60;
                dsPdfForms = db.ExecuteDataSet(CheckPDFFormAvailabiltyCmd);
                if (dsPdfForms.Tables.Count > 0)
                    dtPdfForms = dsPdfForms.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:CheckPDFFormAvailabilty(string transactionType, string SchemeName)");
                object[] objects = new object[3];
                objects[0] = transactionType;
                objects[1] = schemeCode;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dtPdfForms;
            
        }
    }
}
