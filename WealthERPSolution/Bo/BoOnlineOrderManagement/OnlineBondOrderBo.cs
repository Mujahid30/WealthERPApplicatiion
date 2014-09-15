using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoOnlineOrderManagement
{
    public class OnlineBondOrderBo : OnlineOrderBo
    {
        OnlineBondOrderDao onlineBondDao = new OnlineBondOrderDao();
        DataSet dsCommissionStructureRules;


        public DataSet GetBindIssuerList()
        {
            //CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = onlineBondDao.GetBindIssuerList();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }

        public DataSet GetAdviserIssuerList(int adviserId, int issueId, int type, int custmerId, int isAdminRequest,int customerSubtype)
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetAdviserIssuerList(adviserId, issueId, type, custmerId, isAdminRequest, customerSubtype);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }
        public void GetCustomerCat(int issueId, int customerId, int adviserId, double amt, ref string catName, ref int issueDetId, ref int categoryId, ref string Description)
        {
            // bool result = false;

            try
            {
                onlineBondDao.GetCustomerCat(issueId, customerId, adviserId, amt, ref catName, ref issueDetId, ref categoryId, ref Description);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        public DataSet GetLiveBondTransaction(int SeriesId, int customerId,int customerSubType)
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetLiveBondTransaction(SeriesId, customerId, customerSubType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetLiveBondTransaction()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }
        public DataSet GetLiveBondTransactionList(int Adviserid)
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetLiveBondTransactionList(Adviserid);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetLiveBondTransactionList()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }
        public string GetCutOFFTimeForCurent(int orderId)
        {
            string cutOffTime = "";
            try
            {
                cutOffTime = onlineBondDao.GetCutOFFTimeForCurent(orderId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return cutOffTime;
        }
        public DataSet GetLookupDataForReceivableSetUP(int adviserId, string structureId)
        {
            //CommisionReceivableDao commisionReceivableDao = new CommisionReceivableDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = onlineBondDao.GetLookupDataForReceivableSetUP(adviserId, structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }
        public DataSet GetIssueDetail(int IssuerId, int CustomerId)
        {
            DataSet dsGetIssueDetail = new DataSet();
            try
            {
                dsGetIssueDetail = onlineBondDao.GetIssueDetail(IssuerId, CustomerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetIssueDetail()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetIssueDetail;
        }
        public DataSet GetAdviserCommissionStructureRules(int adviserId, string structureId)
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetAdviserCommissionStructureRules(adviserId, structureId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCommissionStructureRules;
        }
        public IDictionary<string, string> onlineBOndtransact(DataTable OnlineBondOrder, int adviserId, int IssuerId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            //bool result = false;
            //int orderIds = 0; 
            try
            {
                OrderIds = onlineBondDao.UpdateOnlineBondTransact(OnlineBondOrder, adviserId, IssuerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:onlineBOndtransact(OnlineBondOrderVo OnlineBondOrder)");
                object[] objects = new object[1];
                objects[0] = OnlineBondOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return OrderIds;
        }
        public IDictionary<string, string> OfflineBOndtransact(DataTable OnlineBondOrder, int adviserId, int IssuerId,int agentId,string agentCode,int userId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            //bool result = false;
            //int orderIds = 0; 
            try
            {
                OrderIds = onlineBondDao.CreateOfflineBondTransact(OnlineBondOrder, adviserId, IssuerId,agentId,agentCode,userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:onlineBOndtransact(OnlineBondOrderVo OnlineBondOrder)");
                object[] objects = new object[1];
                objects[0] = OnlineBondOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return OrderIds;
        }
        public IDictionary<string, string> UpdateTransactOrder(DataTable OnlineBondOrder, OnlineBondOrderVo OnlineBondOrderVo, int adviserId, int IssuerId, int OrderId, int seriesId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            bool result = false;
            try
            {
                OrderIds = onlineBondDao.UpdateTransactOrder(OnlineBondOrder, OnlineBondOrderVo, adviserId, IssuerId, OrderId, seriesId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:onlineBOndtransact(OnlineBondOrderVo OnlineBondOrder)");
                object[] objects = new object[1];
                objects[0] = OnlineBondOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return OrderIds;
        }
        public DataSet getBondsBookview(int input, string CustId)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = OnlineBondDao.GetOrderBondsBook(input, CustId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:getBondsBookview(int input)");
                object[] objects = new object[1];
                objects[0] = input;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }
        public DataSet GetOrderBondBook(int customerId,int issueId, string status, DateTime dtFrom, DateTime dtTo,int adviserId)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = OnlineBondDao.GetOrderBondBook(customerId,issueId, status, dtFrom, dtTo, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetOrderBondBook()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }
        public DataSet GetOrderBondSubBook(int customerId, int IssuerId, int orderid)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = OnlineBondDao.GetOrderBondSubBook(customerId, IssuerId, orderid);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetOrderBondBook()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }
        public DataSet GetNomineeJointHolder(int customerId)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = OnlineBondDao.GetNomineeJointHolder(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetNomineeJointHolder()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupData;
        }
        public bool cancelBondsBookOrder(int orderId, int is_Cancel, string remarks)
        {
            bool bResult = false;
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            try
            {
                bResult = OnlineBondDao.CancelBondsBookOrder(orderId, is_Cancel, remarks);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:cancelBondsBookOrder(string id)");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }


        public string GetMAXTransactNO()
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();

            string maxDB = string.Empty;
            try
            {
                maxDB = OnlineBondDao.GetMAXTransactNO();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:getBondsBookview(int input)");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return maxDB;
        }
        public int GetApplicationNumber()
        {
            int ApplicationNumber = 0;
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            try
            {
                ApplicationNumber = OnlineBondDao.GetApplicationNumber();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetApplicationNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ApplicationNumber;
        }
        public DataSet GetNCDTransactOrder(int orderId, int IssuerId)
        {
            DataSet dsNCD;

            OnlineBondOrderDao OnlineBondOrderDao = new OnlineBondOrderDao();
            try
            {
                dsNCD = OnlineBondOrderDao.GetNCDTransactOrder(orderId, IssuerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetNCDTransactOrder()");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsNCD;
        }
        public DataSet GetNCDAllTransactOrder(int orderId, int IssuerId)
        {
            DataSet dsNCD;

            OnlineBondOrderDao OnlineBondOrderDao = new OnlineBondOrderDao();
            try
            {
                dsNCD = OnlineBondOrderDao.GetNCDAllTransactOrder(orderId, IssuerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetNCDTransactOrder()");
                object[] objects = new object[1];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsNCD;
        }
        public DataTable GetNCDHoldingOrder(int customerId, int AdviserId)
        {
            DataTable dtNCDHoldingOrder;

            OnlineBondOrderDao OnlineBondOrderDao = new OnlineBondOrderDao();
            try
            {
                dtNCDHoldingOrder = OnlineBondOrderDao.GetNCDHoldingOrder(customerId, AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetNCDHoldingOrder()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDHoldingOrder;
        }
        public DataTable GetNCDHoldingSeriesOrder(int customerId, int AdviserId, int IssueId, int orderId)
        {
            DataTable dtGetNCDHoldingSeriesOrder;

            OnlineBondOrderDao OnlineBondOrderDao = new OnlineBondOrderDao();
            try
            {
                dtGetNCDHoldingSeriesOrder = OnlineBondOrderDao.GetNCDHoldingSeriesOrder(customerId, AdviserId, IssueId, orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderBo.cs:GetNCDHoldingOrder()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetNCDHoldingSeriesOrder;
        }
        public DataTable GetCustomerIssueName(int CustomerId, string Product)
        {
            DataTable dtGetCustomerIssueName;
            OnlineBondOrderDao OnlineBondOrderDao = new OnlineBondOrderDao();
            dtGetCustomerIssueName = OnlineBondOrderDao.GetCustomerIssueName(CustomerId, Product);
            return dtGetCustomerIssueName;
        }
    }
}
