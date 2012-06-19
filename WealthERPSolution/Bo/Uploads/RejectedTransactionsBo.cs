using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using DaoUploads;
using BoUser;
using VoUser;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.SqlServer;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoUploads
{
    public class RejectedTransactionsBo
    {
        public DataSet GetRejectedRecords(int adviserId)
        {
            RejectedTransactionsDao RejectedTransactionsDao = new RejectedTransactionsDao();
            DataSet getRejectedTransactionsDs;
            try
            {
                getRejectedTransactionsDs = RejectedTransactionsDao.GetRejectedRecords(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:GetRejectedRecords()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getRejectedTransactionsDs;
        }

        public DataSet GETAllEquityInputRejectTransactions(int processId)
        {
            RejectedTransactionsDao RejectedTransactionsDao = new RejectedTransactionsDao();
            DataSet getRejectedTransactionsDs;
            try
            {
                getRejectedTransactionsDs = RejectedTransactionsDao.GETAllEquityInputRejectTransactions(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:GETAllEquityInputRejectTransactions()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getRejectedTransactionsDs;
        }

        
        public bool MapFolioToCustomer(int MFTransactionStagingId, int customerId, int userId)
        {
            RejectedTransactionsDao rejectedTransactionsDao = new RejectedTransactionsDao();
            return rejectedTransactionsDao.MapFolioToCustomer(MFTransactionStagingId, customerId, userId);
        }

        public bool MapRejectedFoliosToCustomer(int mfFolioStagingId, int customerId, int userId)
        {
            RejectedTransactionsDao rejectedTransactionsDao = new RejectedTransactionsDao();
            bool rejectedTransactions;
            try
            {
                rejectedTransactions = rejectedTransactionsDao.MapRejectedFoliosToCustomer(mfFolioStagingId, customerId, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:GetRejectedRecords()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return rejectedTransactions;
        }

        public bool MapEquityToCustomer(int transactionStagingId, int customerId, int userId)
        {
            RejectedTransactionsDao rejectedTransactionsDao = new RejectedTransactionsDao();
            return rejectedTransactionsDao.MapEquityToCustomer(transactionStagingId, customerId, userId);
        }
    }
}
