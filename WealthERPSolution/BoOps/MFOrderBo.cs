using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOps;
using VoOps;
using VoCustomerPortfolio;

namespace BoOps
{
    public class MFOrderBo : OrderBo
    {
        MFOrderDao mfOrderDao = new MFOrderDao();
        public int GetOrderNumber(int orderId)
        {
            int orderNumber = 0;
            try
            {
                orderNumber = mfOrderDao.GetOrderNumber(orderId);
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
        public DataTable AplicationNODuplicates(string prefixText)
        {
            DataTable AplicationNODuplicates = new DataTable();
            try
            {
                AplicationNODuplicates = mfOrderDao.AplicationNODuplicates(prefixText);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return AplicationNODuplicates;
        }
        public List<int> CreateCustomerMFOrderDetails(OrderVo orderVo, MFOrderVo mforderVo, int userId, SystematicSetupVo SystematicSetupVo)
        {
            List<int> orderIds = new List<int>();
            try
            {
                orderIds = mfOrderDao.CreateOrderMFDetails(orderVo, mforderVo, userId, SystematicSetupVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return orderIds;
        }

        public DataSet GetCustomerMFOrderMIS(int AdviserId, DateTime FromDate, DateTime ToDate, string branchId, string rmId, string transactionType, string status, string orderType, string amcCode, string customerId, int isOnline)
        {
            DataSet dsGetMFOrderMIS;
            try
            {
                dsGetMFOrderMIS = mfOrderDao.GetCustomerMFOrderMIS(AdviserId, FromDate, ToDate, branchId, rmId, transactionType, status, orderType, amcCode, customerId, isOnline);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetMFOrderMIS;
        }

        public void UpdateCustomerMFOrderDetails(OrderVo orderVo, MFOrderVo mforderVo, int userId, SystematicSetupVo systematicSetupVo)
        {
            try
            {
                mfOrderDao.UpdateCustomerMFOrderDetails(orderVo, mforderVo, userId, systematicSetupVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        public int MarkAsReject(int orderId, string Remarks)
        {
            int IsMarked = 0;
            try
            {
                IsMarked = mfOrderDao.MarkAsReject(orderId, Remarks);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return IsMarked;
        }
        public bool ChkOnlineOrder(int OrderId)
        {
            try
            {
                return mfOrderDao.ChkOnlineOrder(OrderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        public DataSet GetCustomerMFOrderDetails(int orderId)
        {
            DataSet dsGetCustomerMFOrderDetails;
            try
            {
                dsGetCustomerMFOrderDetails = mfOrderDao.GetCustomerMFOrderDetails(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetCustomerMFOrderDetails;
        }

        public DataSet GetCustomerBank(int customerId)
        {
            DataSet dsGetCustomerBank;
            try
            {
                dsGetCustomerBank = mfOrderDao.GetCustomerBank(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetCustomerBank;
        }

        public DataSet GetSipDetails(int orderID)
        {
            DataSet dsGetSip;
            try
            {
                dsGetSip = mfOrderDao.GetSipDetails(orderID);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetSip;
        }
        public DataTable GetBankBranch(int BankId)
        {
            DataTable dtGetBankBranch;
            try
            {
                dtGetBankBranch = mfOrderDao.GetBankBranch(BankId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dtGetBankBranch;
        }

        //public DataTable GetBankBranchLookups(int lookUpId)
        //{
        //    DataTable dtGetBankBranch;
        //    try
        //    {
        //        dtGetBankBranch = mfOrderDao.GetBankBranchLookups(lookUpId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw (Ex);
        //    }
        //    return dtGetBankBranch;
        //}

        public bool DeleteMFOrder(int orderId)
        {
            bool bResult = false;
            try
            {
                bResult = mfOrderDao.DeleteMFOrder(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }

        public bool MFOrderAutoMatch(int OrderId, int SchemeCode, int AccountId, string TransType, int CustomerId, double Amount, DateTime OrderDate)
        {
            bool result = false;
            try
            {
                result = mfOrderDao.MFOrderAutoMatch(OrderId, SchemeCode, AccountId, TransType, CustomerId, Amount, OrderDate, out result);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return result;
        }
        public DataSet GetEQCustomerBank(int customerId)
        {
            DataSet dsGetCustomerBank;
            try
            {
                dsGetCustomerBank = mfOrderDao.GetEQCustomerBank(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetCustomerBank;
        }
        public DataSet GetARNNo(int adviserId)
        {
            DataSet dsARNNo;
            try
            {
                dsARNNo = mfOrderDao.GetARNNo(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsARNNo;
        }

    }
}
