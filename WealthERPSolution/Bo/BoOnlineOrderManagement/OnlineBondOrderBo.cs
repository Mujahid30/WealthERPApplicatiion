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

        public DataSet GetAdviserIssuerList(int adviserId)
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetAdviserIssuerList(adviserId);

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
        public DataSet GetLiveBondTransaction(int SeriesId)
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetLiveBondTransaction(SeriesId);

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
        public DataSet GetLiveBondTransactionList()
        {

            try
            {
                dsCommissionStructureRules = onlineBondDao.GetLiveBondTransactionList();

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
        public DataSet GetIssueDetail(int IssuerId)
        {
            DataSet dsGetIssueDetail = new DataSet();
            try
            {
                dsGetIssueDetail = onlineBondDao.GetIssueDetail(IssuerId);

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
                dsCommissionStructureRules = onlineBondDao.GetAdviserCommissionStructureRules(adviserId,structureId);

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
        public int onlineBOndtransact(DataTable OnlineBondOrder, int adviserId)
        {
            //List<int> orderIds = new List<int>(); 
            //bool result = false;
            int orderIds = 0; 
            try
            {
                orderIds = onlineBondDao.UpdateOnlineBondTransact(OnlineBondOrder, adviserId);

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
            return orderIds;
        }
        public IDictionary<string, string> UpdateTransactOrder(DataTable OnlineBondOrder,OnlineBondOrderVo OnlineBondOrderVo, int adviserId, int IssuerId, int OrderId, int seriesId)
        {
            IDictionary<string, string> OrderIds = new Dictionary<string, string>();
            bool result = false;
            try
            {
                OrderIds = onlineBondDao.UpdateTransactOrder(OnlineBondOrder,OnlineBondOrderVo, adviserId, IssuerId, OrderId,seriesId);

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
        public DataSet GetOrderBondBook(int customerId, string status, DateTime dtFrom, DateTime dtTo)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            DataSet dsLookupData;
            try
            {
                dsLookupData = OnlineBondDao.GetOrderBondBook(customerId, status, dtFrom, dtTo);

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
                dsLookupData = OnlineBondDao.GetOrderBondSubBook(customerId,IssuerId,orderid);

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
        public void cancelBondsBookOrder(string id)
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
            try
            {
                OnlineBondDao.CancelBondsBookOrder(id);

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
                objects[0] = id;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        public string GetMAXTransactNO()
        {
            OnlineBondOrderDao OnlineBondDao = new OnlineBondOrderDao();
           
            string maxDB=string.Empty;
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
    }
}
