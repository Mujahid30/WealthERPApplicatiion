using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using DaoOps;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoOps;
using System.Collections.Specialized;


namespace BoOps
{
    public class OperationBo
    {
        OperationDao operationDao = new OperationDao();
        public DataSet GetOrderStatus()
        {
            DataSet dsOrderStatus;
            try
            {
                dsOrderStatus = operationDao.GetOrderStatus();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetOrderStatus()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderStatus;
        }

        public DataSet GetMFOrderRecon(DateTime fromDate, DateTime toDate, string orderStatus, string orderType)
        {
            DataSet dsOrderRecon;
            try
            {
                dsOrderRecon = operationDao.GetMfOrderRecon(fromDate, toDate, orderStatus, orderType);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetMFOrderRecon()");
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
            DataSet dsTransactionType;
            try
            {
                dsTransactionType = operationDao.GetTransactionType();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetTransactionType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsTransactionType;
        }

        public DataSet GetAssetType()
        {
            DataSet dsAssetType;
            try
            {
                dsAssetType = operationDao.GetAssetType();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetAssetType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAssetType;
        }



        public DataSet GetOrderMIS(int adviserId, string branchId, string rmId, string transactionType, string status, string orderType, string amcCode, DateTime FromDate, DateTime ToDate, int CurrentPage, out int count)
        {
            DataSet dsOrderMIS;
            try
            {
                dsOrderMIS = operationDao.GetOrderMIS(adviserId, branchId, rmId, transactionType, status, orderType, amcCode, FromDate, ToDate, CurrentPage, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetOrderMIS()");
                object[] objects = new object[10];
                objects[0] = adviserId;
                objects[1] = branchId;
                objects[2] = rmId;
                objects[3] = transactionType;
                objects[4] = status;
                objects[5] = orderType;
                objects[6] = amcCode;
                objects[7] = FromDate;
                objects[8] = ToDate;
                objects[9] = CurrentPage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrderMIS;
        }

        public DataSet GetOrderMannualMatch(int scheme, int accountId, string type, double amount, DateTime orderDate, int customerId, int schemeSwitch)
        {
            DataSet dsMannualMatch;
            try
            {
                dsMannualMatch = operationDao.GetOrderMannualMatch(scheme, accountId, type, amount, orderDate, customerId,schemeSwitch);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetOrderMannualMatch()");
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
            return dsMannualMatch;
        }

        public DataSet GetRejectStatus(string statusCode)
        {
            DataSet dsStatusReject;
            try
            {
                dsStatusReject = operationDao.GetRejectStatus(statusCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetRejectStatus()");
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
            try
            {
                dsProductAMC = operationDao.GetAMCForOrderEntry(flag,customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetAMCForOrderEntry()");
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
            try
            {
                dsScheme = operationDao.GetSchemeForOrderEntry(amcCode, categoryCode, Sflag, customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetSchemeForOrderEntry()");
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
            try
            {
                dsfolio = operationDao.GetFolioForOrderEntry(portfolioId, amcCode, all, Fflag, customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetFolioForOrderEntry()");
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
            try
            {
                dsAmountUnits = operationDao.GetAmountUnits(schemePlanCode, customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetAmountUnits()");
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
            try
            {
                dsSchemeSwitch = operationDao.GetSwitchScheme(amcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetSwitchScheme()");
                object[] objects = new object[1];
                objects[0] = amcCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeSwitch;
        }

        public bool CreateMFOrderTracking(OperationVo operationVo)
        {
            bool bResult = false;
            try
            {
                bResult = operationDao.CreateMFOrderTracking(operationVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public int GetOrderNumber()
        {
            int orderNumber = 0;
            try
            {
                orderNumber = operationDao.GetOrderNumber();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetOrderNumber()");
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
            OperationVo operationVo = new OperationVo();
            try
            {
                operationVo = operationDao.GetCustomerOrderTrackingDetails(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return operationVo;
        }

        public bool UpdateMFTransaction(int gvOrderId, int gvSchemeCode, int gvaccountId, string gvTrxType, int gvPortfolioId, double gvAmount, DateTime gvOrderDate)
        {
            bool Result = false;
            try
            {
                Result = operationDao.UpdateMFTransactionForSynch(gvOrderId, gvSchemeCode, gvaccountId, gvTrxType, gvPortfolioId, gvAmount, out Result, gvOrderDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:UpdateMFTransaction()");
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
            return Result;
        }

        public bool UpdateOrderTracking(OperationVo operationVo)
        {
            bool bResult = false;
            try
            {
                bResult = operationDao.UpdateOrderTracking(operationVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public bool OrderMannualMatch(int OrderId, int transId, int SchemeCode, double amount, string TrxType)
        {
            bool Result = false;
            try
            {
                Result = operationDao.OrderMannualMatch(OrderId,transId, SchemeCode, amount, out Result, TrxType);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:OrderMannualMatch()");
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
            return Result;
        }

        public DataTable CheckPDFFormAvailabilty(string transactionType, int schemeCode)
        {
            DataTable dtPdfForms = new DataTable();

            try
            {
                dtPdfForms = operationDao.CheckPDFFormAvailabilty(transactionType, schemeCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:CheckPDFFormAvailabilty(string transactionType, int schemeCode)");
                object[] objects = new object[5];
                objects[0] = transactionType;
                objects[1] = schemeCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dtPdfForms;
        }

        public DataSet GetCustomerApprovalList(int customerId,int status)
        {
            DataSet dsCustomerApprovallist = new DataSet();
            try
            {
                dsCustomerApprovallist = operationDao.GetCustomerApprovalList(customerId, status);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsCustomerApprovallist;
        }

        public bool UpdateCustomerApprovalList(int gvOrderId)
        {
            bool Result = false;
            try
            {
                Result = operationDao.UpdateCustomerApprovalList(gvOrderId,out Result);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

            return Result;
        }
    }
}
