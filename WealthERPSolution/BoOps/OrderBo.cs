using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOps;
using VoOps;


namespace BoOps
{
    public class OrderBo
    {

        /*************************************Code starts Here for Life Insurance****************************************************************************************/

        /// <summary>
        /// Method to get all the Bank Account details for a particular customer
        /// </summary>
        /// <param name="customerId">CustomerId of the customer whose Bank Account Details are to be retrieved.</param>
        /// <returns>Returns a List of Bank Account Objects</returns>
        /// 
        public DataTable GetBankAccountDetails(int customerId)
        {
            DataTable customerBankAccountlist = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                customerBankAccountlist = orderDao.GetBankAccountDetails(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetBankAccountDetails()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerBankAccountlist;
        }

        public DataTable GetBankBranch(int BankAccId)
        {
            DataTable dtBranch = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtBranch = orderDao.GetBankBranch(BankAccId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetBankBranch()");

                object[] objects = new object[1];
                objects[0] = BankAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtBranch;
        }

        public DataTable GetPaymentMode()
        {
            DataTable dtPaymentMode = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtPaymentMode = orderDao.GetPaymentMode();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetPaymentMode()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtPaymentMode;
        }

        public DataTable GetAssetParticular(string issuerCode)
        {
            DataTable dtAssetParticular = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtAssetParticular = orderDao.GetAssetParticular(issuerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetAssetParticular()");

                object[] objects = new object[1];
                objects[0] = issuerCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAssetParticular;
        }

        public void InsertAssetParticularScheme(string asset, string InsuranceIssuerCode, string assetCategory)
        {
            OrderDao orderDao = new OrderDao();
            try
            {
                orderDao.InsertAssetParticularScheme(asset, InsuranceIssuerCode, assetCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void InsertIntoProductGIInsuranceScheme(string asset, string InsuranceIssuerCode, string schemePlanName)
        {
            OrderDao orderDao = new OrderDao();
            try
            {
                orderDao.InsertIntoProductGIInsuranceScheme(asset, InsuranceIssuerCode, schemePlanName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool AddLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo, string nomineeAssociationIds , out int orderId)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.AddLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds, out orderId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "orderBo.cs:AddLifeInsuranceOrder()");

                object[] objects = new object[1];
                objects[0] = lifeInsuranceOrdervo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool UpdateLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo, string nomineeAssociationIds)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.UpdateLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "orderBo.cs:AddLifeInsuranceOrder()");

                object[] objects = new object[1];
                objects[0] = lifeInsuranceOrdervo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool DeleteLifeInsuranceOrder(int orderId)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.DeleteLifeInsuranceOrder(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:DeleteLifeInsuranceOrder()");
                object[] objects = new object[1];
                objects[0] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public LifeInsuranceOrderVo GetLifeInsuranceOrderDetails(int orderId)
        {
            LifeInsuranceOrderVo lifeInsuranceOrderVo = new LifeInsuranceOrderVo();
            OrderDao orderDao = new OrderDao();
            try
            {
                lifeInsuranceOrderVo = orderDao.GetLifeInsuranceOrderDetails(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetLifeInsuranceOrderDetails()");

                object[] objects = new object[1];
                objects[0] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return lifeInsuranceOrderVo;
        }

        //public bool EditLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        //{
        //    bool bResult = false;
        //    OrderDao orderDao = new OrderDao();
        //    try
        //    {
        //        bResult = orderDao.AddLifeInsuranceOrder(lifeInsuranceOrdervo);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "orderBo.cs:EditLifeInsuranceOrder()");

        //        object[] objects = new object[1];
        //        objects[0] = lifeInsuranceOrdervo;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return bResult;
        //}

        public DataTable GetCustomerOrderDetails(int customerId, DateTime orderDate, string AssetCategory, string applicationNumber)
        {
            DataTable dtOrderDetails = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrderDetails = orderDao.GetCustomerOrderDetails(customerId, orderDate, AssetCategory, applicationNumber);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetCustomerOrderDetails()");

                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = orderDate;
                objects[2] = AssetCategory;
                objects[3] = applicationNumber;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtOrderDetails;
        }

        public DataSet GetOrderStepsDetails(int orderId)
        {
            DataSet dsStepsDetails = null;
            DataTable dtStepsDetails = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dsStepsDetails = orderDao.GetOrderStepsDetails(orderId);
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
                objects[0] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsStepsDetails;
        }
        /*************************************Code starts Here for Bond Order ****************************************************************************************/

        public DataTable GetAssetParticularForBonds(int bondIssuerId)
        {
            DataTable dtAssetParticular = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtAssetParticular = orderDao.GetAssetParticularForBonds(bondIssuerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetAssetParticularForBonds()");

                object[] objects = new object[1];
                objects[0] = bondIssuerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAssetParticular;
        }

        public void InsertBondIssuerName(string bondIssuerName)
        {
            OrderDao orderDao = new OrderDao();
            try
            {
                orderDao.InsertBondIssuerName(bondIssuerName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void InsertAssetParticularForBonds(string assetParticularText, int InsuranceIssuerId)
        {
            OrderDao orderDao = new OrderDao();
            try
            {
                orderDao.InsertAssetParticularForBonds(assetParticularText, InsuranceIssuerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public DataTable GetBondsIssuerName()
        {
            DataTable dtAssetParticular = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtAssetParticular = orderDao.GetBondsIssuerName();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetBondsIssuer()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAssetParticular;
        }

        public bool AddBondOrderDetails(BondOrderVo bondOrderVo)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.AddBondOrderDetails(bondOrderVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "orderBo.cs:AddBondOrderDetails()");

                object[] objects = new object[1];
                objects[0] = bondOrderVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataTable GetCustomerDemateAccountDetails(int customerId)
        {
            DataTable dtDemateDetails = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtDemateDetails = orderDao.GetCustomerDemateAccountDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetCustomerDemateAccountDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtDemateDetails;
        }

        public DataTable GetOrderList(int systematicId, int advisorId, string rmId, string branchId, DateTime toDate, DateTime fromDate, string status, string customerId, string orderType, string usertype, int AgentId, string SubBrokerCode, string AgentCode, int orderId)
        { 
            DataTable dtOrder = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrder = orderDao.GetOrderList(systematicId,advisorId, rmId, branchId, toDate, fromDate, status, customerId, orderType, usertype, AgentId, SubBrokerCode, AgentCode, orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderList()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;
        }

        public DateTime GetServerTime()
        {
            DateTime Dt = DateTime.MinValue;
            OrderDao orderDao = new OrderDao();
            try
            {
                Dt = orderDao.GetServerTime( );
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:Get GetServerTime()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return Dt;

        }

        public DataTable GetOrderStatus(string orderStepCode, int orderId)
        {
            DataTable dtOrderStatus = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrderStatus = orderDao.GetOrderStatus(orderStepCode, orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderList()");

                object[] objects = new object[1];
                objects[0] = orderStepCode;
                objects[1] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrderStatus;
        }

        public DataTable GetOrderStatusPendingReason(string orderStatusCode)
        {
            DataTable dtOrderStatus = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrderStatus = orderDao.GetOrderStatusPendingReason(orderStatusCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetOrderStatusPendingReason()");

                object[] objects = new object[1];
                objects[0] = orderStatusCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrderStatus;
        }

        public bool UpdateOrderStep(string updatedStatus, string updatedReason, int orderId, string orderStepCode)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.UpdateOrderStep(updatedStatus, updatedReason, orderId, orderStepCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "orderBo.cs:UpdateOrderStep()");

                object[] objects = new object[1];
                objects[0] = updatedStatus;
                objects[1] = updatedReason;
                objects[2] = orderId;
                objects[3] = orderStepCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataTable GetCustomerOrderStepStatus(string orderstepCode)
        {
            DataTable dtOrderStatus = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrderStatus = orderDao.GetCustomerOrderStepStatus(orderstepCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtOrderStatus;
        }

        public DataTable GetCustomerOrderStepStatusRejectReason(string orderstepCode)
        {
            DataTable dtOrderStatus = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrderStatus = orderDao.GetCustomerOrderStepStatusRejectReason(orderstepCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtOrderStatus;
        }

        public DataTable GetCustomerOrderAssociates(int orderId)
        {
            DataTable dtOrderAccociates = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrderAccociates = orderDao.GetCustomerOrderAssociates(orderId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtOrderAccociates;
        }

        public DataTable GetSubBrokerCode(int advisorId, int rmId, int branchId, string usertype)
        {
            DataTable dtOrder = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrder = orderDao.GetSubBrokerCode(advisorId, rmId, branchId,usertype);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetSubBrokerCode()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;
        }

        public DataTable GetSubBrokerAgentCode(int adviserId, string AgentCode)
        {
            DataTable dtOrder = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrder = orderDao.GetSubBrokerAgentCode(adviserId,AgentCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetSubBrokerAgentCode()");

                object[] objects = new object[1];
                objects[0] = AgentCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;
        }
        public DataTable GetAllAgentListForOrder(int id, string UserRole)
        {

            DataTable dtOrder = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrder = orderDao.GetAllAgentListForOrder(id,UserRole);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetAllAgentListForOrder()");

                object[] objects = new object[1];
                objects[0] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrder;

        }

        public DataTable GetTradeDateListForOrder(DateTime date, int isPastDateList, int noOfDaysReq)
        {
            DataTable dtTradeDate = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtTradeDate = orderDao.GetTradeDateListForOrder(date, isPastDateList, noOfDaysReq);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetTradeDateListForOrder(DateTime date, int isPastDateList, int noOfDaysReq))");
                object[] objects = new object[3];
                objects[0] = date;
                objects[1] = isPastDateList;
                objects[2] = noOfDaysReq;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtTradeDate;
        }
        public DataTable GetTradeDateList(DateTime date)
        {
            DataTable dtTradeDate = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtTradeDate = orderDao.GetTradeDateList(date);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OrderBo.cs:GetTradeDateListForOrder(DateTime date, int isPastDateList, int noOfDaysReq))");
                object[] objects = new object[3];
                objects[0] = date;
               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtTradeDate;
        }
    }
}
