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

        public bool AddLifeInsuranceOrder(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        {
            bool bResult = false;
            OrderDao orderDao = new OrderDao();
            try
            {
                bResult = orderDao.AddLifeInsuranceOrder(lifeInsuranceOrdervo);

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

        //public DataSet GetLifeInsuranceOrderDetails(LifeInsuranceOrderVo lifeInsuranceOrdervo)
        //{
        //    DataSet dsLIOrder = null;
        //    DataTable dtLIOrder = null;
        //    OrderDao orderDao = new OrderDao();
        //    try
        //    {
        //        dsLIOrder = orderDao.GetLifeInsuranceOrderDetails();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "OrderBo.cs:GetLifeInsuranceOrderDetails()");

        //        object[] objects = new object[1];
        //        objects[0] = issuerCode;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return dsLIOrder;
        //}

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
    }
}
