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
using DaoOnlineOrderManagement;


namespace BoOnlineOrderManagement
{
    public class OnlineOrderMISBo
    {
        public DataSet GetOrderBookMIS(int adviserId, int AmcCode, string OrderStatus, DateTime dtFrom, DateTime dtTo, int orderNo, string folioNo, int Isdemat)
        {
            DataSet dsOrderBookMIS = null;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            try
            {
                dsOrderBookMIS = OnlineOrderMISDao.GetOrderBookMIS(adviserId, AmcCode, OrderStatus, dtFrom, dtTo, orderNo, folioNo, Isdemat);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderStepsDetails()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsOrderBookMIS;
        }

        public DataSet GetSIPBookMIS(int adviserId, int AmcCode, string OrderStatus, int systematicId, DateTime dtFrom, DateTime dtTo, int orderId, string folioNo, string Mode)
        {
            DataSet dsSIPBookMIS = null;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            try
            {
                dsSIPBookMIS = OnlineOrderMISDao.GetSIPBookMIS(adviserId, AmcCode, OrderStatus, systematicId, dtFrom, dtTo, orderId, folioNo, Mode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderStepsDetails()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSIPBookMIS;
        }
        public DataSet GetSIPSummaryBookMIS(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo, int searchType, int statusType, string systematicType, string SIPMode, string Mode)
        {
            DataSet dsSIPSummaryBookMIS = null;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            try
            {
                dsSIPSummaryBookMIS = OnlineOrderMISDao.GetSIPSummaryBookMIS(adviserId, AmcCode, dtFrom, dtTo, searchType, statusType, systematicType, SIPMode, Mode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetSIPSummaryBookMIS()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSIPSummaryBookMIS;
        }
        public DataSet GetSchemeMIS(string Assettype, int Onlinetype, string Status, int IsDemat)
        {
            DataSet dsSchemeMIS;
            OnlineOrderMISDao onlineOrderMISDao = new OnlineOrderMISDao();

            try
            {

                dsSchemeMIS = onlineOrderMISDao.GetSchemeMIS(Assettype, Onlinetype, Status, IsDemat);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderMISBo.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeMIS;
        }
        public DataTable GetAdviserCustomerTransaction(int adviserId, int AmcCode, DateTime dtFrom, DateTime dtTo, int PageSize, int CurrentPage, string CustomerNamefilter, string custCode, string panNo, string folioNo, string schemeName, string type, string dividentType, string fundName, int orderNo, out int RowCount, bool Isdemat, int schemePlanCode, int customerid)
        {
            DataTable dtGetAdviserCustomerTransaction;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            dtGetAdviserCustomerTransaction = OnlineOrderMISDao.GetAdviserCustomerTransaction(adviserId, AmcCode, dtFrom, dtTo, PageSize, CurrentPage, CustomerNamefilter, custCode, panNo, folioNo, schemeName, type, dividentType, fundName, orderNo, out RowCount, Isdemat, schemePlanCode, customerid);
            return dtGetAdviserCustomerTransaction;
        }
        public DataTable GetAdviserCustomerTransactionsBookSIP(int AdviserID, int customerId, int SystematicId, int IsSourceAA, int AccountId, int SchemePlanCode, int requestId)
        {
            DataTable dtGetAdviserCustomerTransactionsBookSIP;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            dtGetAdviserCustomerTransactionsBookSIP = OnlineOrderMISDao.GetAdviserCustomerTransactionsBookSIP(AdviserID, customerId, SystematicId, IsSourceAA, AccountId, SchemePlanCode, requestId);
            return dtGetAdviserCustomerTransactionsBookSIP;
        }
        public DataTable GetMFHolding()
        {
            DataTable dtGetMFHolding;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            dtGetMFHolding = OnlineOrderMISDao.GetMFHolding();
            return dtGetMFHolding;
        }
        public DataTable GetMFHoldingRecon(int requestNo)
        {
            DataTable dtGetMFHoldingRecon;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            dtGetMFHoldingRecon = OnlineOrderMISDao.GetMFHoldingRecon(requestNo);
            return dtGetMFHoldingRecon;
        }
        public DataTable GetMFHoldingReconAfterSync(int requestNo, DateTime toDate, int typeFliter, int differentFliter, int AMC, bool isSync)
        {
            DataTable dtGetMFHoldingRecon;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            dtGetMFHoldingRecon = OnlineOrderMISDao.GetMFHoldingReconAfterSync(requestNo, toDate, typeFliter, differentFliter, AMC, isSync);
            return dtGetMFHoldingRecon;
        }
        public DataTable GetAMCList(int requestId)
        {
            DataTable dtGetAMCList;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            try
            {
                dtGetAMCList = OnlineOrderMISDao.GetAMCList(requestId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAMCList;
        }
        public bool UpdateOrderReverse(int orderid, int userID)
        {
            bool bResult = false;
            OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
            bResult = OnlineOrderMISDao.UpdateOrderReverse(orderid, userID);
            return bResult;
        }
        public bool updateSystemMFHoldingRecon(int requestNo, DateTime toDate)
        {
            bool result = false;
            try
            {
                OnlineOrderMISDao OnlineOrderMISDao = new OnlineOrderMISDao();
                result = OnlineOrderMISDao.updateSystemMFHoldingRecon(requestNo, toDate);
            }
            catch (Exception ex)
            {
            }
            return result;

        }
    }
}
