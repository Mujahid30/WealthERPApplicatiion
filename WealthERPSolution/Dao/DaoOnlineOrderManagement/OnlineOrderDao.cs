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
    public class OnlineOrderDao
    {
        public void UpdateOrderRMSAccountDebitRequestTime(int orderId, decimal amount)
        {

            Database db;
            DbCommand updateRMSDebitRequestTimeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateRMSDebitRequestTimeCmd = db.GetStoredProcCommand("SPROC_ONL_UpdateOrderRMSDebitRequestTime");
                db.AddInParameter(updateRMSDebitRequestTimeCmd, "@OrderId", DbType.Int32, orderId);
                db.AddInParameter(updateRMSDebitRequestTimeCmd, "@Amount", DbType.Decimal, amount);
                db.ExecuteDataSet(updateRMSDebitRequestTimeCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderDao.cs:GUpdateOrderRMSAccountDebitRequestTime(int orderId)");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public int CreateOrUpdateRMSLog(int userId, int rmsId, int isSuccess, string RMSType, double RequestAmount, DateTime RequestTime, DateTime ResponseTime, string rmsTransactionId, string rmsResponseMessage, string rmsReferenceNumber)
        {
            Database db;
            DbCommand createOrUpdateRMSLog;
            int result = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createOrUpdateRMSLog = db.GetStoredProcCommand("SPROC_ONL_CreateOrUpdateRMSLog");
                if (rmsId != 0)
                    db.AddInParameter(createOrUpdateRMSLog, "@RMSId", DbType.Int32, rmsId);
                db.AddInParameter(createOrUpdateRMSLog, "@ResponseTime", DbType.DateTime, ResponseTime);
                db.AddInParameter(createOrUpdateRMSLog, "@RequestTime", DbType.DateTime, RequestTime);
                db.AddInParameter(createOrUpdateRMSLog, "@RequestAmount", DbType.Decimal, RequestAmount);
                db.AddInParameter(createOrUpdateRMSLog, "@RMSType", DbType.String, RMSType);
                db.AddOutParameter(createOrUpdateRMSLog, "@OutRMSId", DbType.Int32, 10);
                db.AddInParameter(createOrUpdateRMSLog, "@UserId", DbType.Int32, userId);
                db.AddInParameter(createOrUpdateRMSLog, "@IsSuccess", DbType.Int16, isSuccess);
                if (!string.IsNullOrEmpty(rmsTransactionId))
                    db.AddInParameter(createOrUpdateRMSLog, "@RMSTransactionId", DbType.String, rmsTransactionId);
                if (!string.IsNullOrEmpty(rmsResponseMessage))
                    db.AddInParameter(createOrUpdateRMSLog, "@RMSResponseMessage", DbType.String, rmsResponseMessage);
                db.AddInParameter(createOrUpdateRMSLog, "@ReferenceNumber", DbType.String, rmsReferenceNumber);
                if (db.ExecuteNonQuery(createOrUpdateRMSLog) != 0)
                {
                    result = Convert.ToInt32(db.GetParameterValue(createOrUpdateRMSLog, "OutRMSId").ToString());
                    
                }
                return result;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderDao.cs:CreateOrUpdateRMSLog(int userId, int rmsId, int isSuccess, string RMSType, decimal RequestAmount, DateTime RequestTime, DateTime ResponseTime, string rmsTransactionId, string rmsResponseMessage, string rmsReferenceNumber)");
                object[] objects = new object[9];
                objects[0] = userId;
                objects[1] = rmsId;
                objects[2] = isSuccess;
                objects[3] = RMSType;
                objects[4] = RequestAmount;
                objects[5] = RequestTime;
                objects[6] = rmsTransactionId;
                objects[7] = rmsResponseMessage;
                objects[8] = rmsReferenceNumber;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public void UpdateOrderRMSAccountDebitDetails(int orderId, int isDebited, string rmsTransactionId, string rmsResponseMessage, string rmsReferenceNumber)
        {

            Database db;
            DbCommand updateRMSAccountDebitedCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateRMSAccountDebitedCmd = db.GetStoredProcCommand("SPROC_ONL_UpdateOrderRMSAccountDebitStatus");
                db.AddInParameter(updateRMSAccountDebitedCmd, "@OrderId", DbType.Int32, orderId);
                db.AddInParameter(updateRMSAccountDebitedCmd, "@IsDebited", DbType.Int16, isDebited);
                if (!string.IsNullOrEmpty(rmsTransactionId))
                    db.AddInParameter(updateRMSAccountDebitedCmd, "@RMSTransactionId", DbType.String, rmsTransactionId);
                if (!string.IsNullOrEmpty(rmsResponseMessage))
                    db.AddInParameter(updateRMSAccountDebitedCmd, "@RMSResponseMessage", DbType.String, rmsResponseMessage);
                db.AddInParameter(updateRMSAccountDebitedCmd, "@ReferenceNumber", DbType.String, rmsReferenceNumber);
                db.ExecuteDataSet(updateRMSAccountDebitedCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderDao.cs:UpdateOrderRMSAccountDebitDetails(int orderId,string rmsTransactionId)");
                object[] objects = new object[2];
                objects[0] = orderId;
                objects[1] = rmsTransactionId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataTable GetClientKYCStatus(int customerId)
        {

            DataTable dtClientKYC = new DataTable();
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetCustomerFamilyKYCStatus");
                db.AddInParameter(cmd, "@CustomerId", DbType.Int32, customerId);
                dtClientKYC = db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderDao.cs:GetClientKYCStatus(int customerId)");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dtClientKYC;

        }
        public DataTable GetImageListForBanner(string assetGroupCode)
        {
            DataSet dsGetImageListForBanner;
            DataTable dtGetImageListForBanner;
            Database db;
            DbCommand cmdGetImageListForBanner;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetImageListForBanner = db.GetStoredProcCommand("SPROC_GetImageListForBanner");
                db.AddInParameter(cmdGetImageListForBanner, "@AssetGroupCode", DbType.String, assetGroupCode);

                dsGetImageListForBanner = db.ExecuteDataSet(cmdGetImageListForBanner);
                dtGetImageListForBanner = dsGetImageListForBanner.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetImageListForBanner;
        }


        public DataTable GetAdvertisementData(string assetGroupCode, string type)
        {
            DataSet ds;
            DataTable dt;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetAdvertisementData");
                db.AddInParameter(cmd, "@AssetGroupCode", DbType.String, assetGroupCode);
                db.AddInParameter(cmd, "@Type", DbType.String, type);

                ds = db.ExecuteDataSet(cmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
    }
}
