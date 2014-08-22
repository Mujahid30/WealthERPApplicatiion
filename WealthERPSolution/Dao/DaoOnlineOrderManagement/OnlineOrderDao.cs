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

        public void UpdateOrderRMSAccountDebitDetails(int orderId,int isDebited, string rmsTransactionId, string rmsResponseMessage)
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
    }
}
