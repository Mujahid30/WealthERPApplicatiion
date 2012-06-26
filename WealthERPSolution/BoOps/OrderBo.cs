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

        public void InsertAssetParticularScheme(string asset, string InsuranceIssuerCode)
        {
            OrderDao orderDao = new OrderDao();
            try
            {
                orderDao.InsertAssetParticularScheme(asset, InsuranceIssuerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void InsertIntoProductGIInsuranceScheme(string asset, string InsuranceIssuerCode,string schemePlanName)
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

        public bool AddLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo, string nomineeAssociationIds, string jointHoldingAssociationIds)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.AddLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds, jointHoldingAssociationIds);

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

        public bool UpdateLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo, string nomineeAssociationIds, string jointHoldingAssociationIds)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.UpdateLifeInsuranceOrder(lifeInsuranceOrdervo, nomineeAssociationIds, jointHoldingAssociationIds);

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

        public DataTable GetOrderList(int advisorId)
        {
            DataTable dtOrder = null;
            OrderDao orderDao = new OrderDao();
            try
            {
                dtOrder = orderDao.GetOrderList(advisorId);
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
    }
}
