﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerProfiling;
using DaoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace BoCustomerProfiling
{
    public class CustomerFamilyBo
    {

        public bool CreateCustomerFamily(CustomerFamilyVo customerFamilyVo, int customerId, int userId)
        {
            bool bResult = false;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            try
            {
                bResult = customerFamilyDao.CreateCustomerFamily(customerFamilyVo, customerId, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:CreateCustomerFamily()");


                object[] objects = new object[3];
                objects[0] = customerFamilyVo;
                objects[1] = customerId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool UpdateCustomerAssociate(CustomerFamilyVo customerFamilyVo, int customerId, int userId)
        {
            bool bResult = false;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            try
            {
                bResult = customerFamilyDao.UpdateCustomerAssociate(customerFamilyVo, customerId, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:UpdateCustomerAssociate()");


                object[] objects = new object[3];
                objects[0] = customerFamilyVo;
                objects[1] = customerId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public CustomerFamilyVo GetCustomerFamilyAssociateDetails(int AssociationId)
        {
            CustomerFamilyVo customerFamilyVo = new CustomerFamilyVo();
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();
            try
            {
                customerFamilyVo = customerFamilyDao.GetCustomerFamilyAssociateDetails(AssociationId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:GetCustomerFamilyAssociateDetails()");


                object[] objects = new object[1];
                objects[0] = AssociationId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerFamilyVo;
        }

        public List<CustomerFamilyVo> GetCustomerFamily(int customerId)
        {
            List<CustomerFamilyVo> familyList = null;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();
            try
            {
                familyList = customerFamilyDao.GetCustomerFamily(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:GetCustomerFamily()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return familyList;

        }

        /// <summary>
        /// Modified by Vinayak Patil.. Customer Association functionality for adviser..
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="rmId"></param>
        /// <param name="nameSrchValue"></param>
        /// <returns></returns>
        public DataTable GetCustomerAssociations(int adviserId, int rmId, string nameSrchValue)
        {
            DataTable dt = new DataTable();
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();
            try
            {
                dt = customerFamilyDao.GetCustomerAssociations(adviserId, rmId, nameSrchValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:GetCustomerFamily()");


                object[] objects = new object[2];
                objects[0] = rmId;
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public bool DeleteCustomerFamily(int CustomerAssociationID)
        {
            bool bResult = false;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            try
            {
                bResult = customerFamilyDao.DeleteCustomerFamily(CustomerAssociationID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:DeleteCustomerFamily()");

                object[] objects = new object[1];
                objects[0] = CustomerAssociationID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataTable GetCustomerAssociates(int customerId)
        {

            DataTable dtCustomerFamily = null;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            try
            {
                dtCustomerFamily = customerFamilyDao.GetCustomerAssociates(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerFamily;
        }

        public DataSet GetCustomerAssociateDetails(int associationId)
        {

            DataSet dsAssociateDetails = null;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            try
            {
                dsAssociateDetails = customerFamilyDao.GetCustomerAssociateDetails(associationId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = associationId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsAssociateDetails;
        }

        public int GetCustomersAssociationId(int customerId)
        {
            int AssociationId = 0;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            try
            {
                AssociationId = customerFamilyDao.GetCustomersAssociationId(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:GetCustomerAssociates()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AssociationId;
        }

        public bool CustomerAssociateUpdate(int customerId, int associateId, string relCode, int userId)
        {
            bool bResult = false;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            try
            {
                bResult = customerFamilyDao.CustomerAssociateUpdate(customerId, associateId, relCode, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:CustomerAssociateUpdate()");


                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = associateId;
                objects[2] = relCode;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public DataTable GetAllCustomerAssociates(int customerId)
        {


            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();


            return customerFamilyDao.GetAllCustomerAssociates(customerId);
        }

        public int CustomerFamilyDissociation(string association)
        {

            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            return customerFamilyDao.CustomerFamilyDissociation(association);


        }

        public int CustomerDissociate(string association,int UserID)
        {

            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();

            return customerFamilyDao.CustomerDissociate(association, UserID);


        }

        public bool Deleteassociation(int associateCustomerId)
        {
            bool bResult = false;
            CustomerFamilyDao customerFamilyDao = new CustomerFamilyDao();
            try
            {
                bResult = customerFamilyDao.Deleteassociation(associateCustomerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerFamilyBo.cs:Deleteassociation()");

                object[] objects = new object[1];
                objects[0] = associateCustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }
    }
}
