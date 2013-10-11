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
        public DataTable GetControlDetails(int Scheme)
        {
            DataSet dsGetControlDetails;
            Database db;
            DbCommand GetGetControlDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetControlDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_GetSchemeDetails");
                db.AddInParameter(GetGetControlDetailsCmd, "@schemecode", DbType.Int32, Scheme);
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
            return dsGetControlDetails.Tables[0];
        }
        public int CreateCustomerOnlineMFOrderDetails(OnlineMFOrderVo onlinemforderVo, int UserId, int CustomerId)
        {
            int OrderId;
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

                if (db.ExecuteNonQuery(CreateCustomerOnlineMFOrderDetailsCmd) != 0)
                {
                    OrderId = Convert.ToInt32(db.GetParameterValue(CreateCustomerOnlineMFOrderDetailsCmd, "CO_OrderId").ToString());

                }
                OrderId = 0;

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
            return OrderId;
        }

    }
}
