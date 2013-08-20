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
using VoCustomerPortfolio;
using DaoCustomerProfiling;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoCustomerProfiling
{
    public class CustomerBo
    {

        /// <summary>
        /// Used to Create Customer
        /// </summary>
        /// <param name="customerVo"></param>
        /// <param name="rmId"></param>
        /// <param name="userId"></param>
        /// <param name="ADULProcessId"></param>
        /// <returns></returns>
        public int CreateCustomer(CustomerVo customerVo, int rmId, int userId, int ADULProcessId)
        {
            int customerId;
            CustomerDao customerDao = new CustomerDao();

            try
            {
                customerId = customerDao.CreateCustomer(customerVo, rmId, userId, ADULProcessId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:CreateCustomer()");


                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerId;
        }


        /// <summary>
        /// Used to Get Customer Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// 
        public Boolean GetFixedMapped(string explist)
        {
            bool Isfixed = false;
            CustomerDao customerDao = new CustomerDao();
            try {
                Isfixed = customerDao.GetFixedMapped(explist);
            }
             catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetSchemeDetails()");
                //object[] objects = new object[1];
                //objects[0] = customerId;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Isfixed;
        }
        public DataTable GetISaList(int customerId)
        {
            DataTable dtGetISaList = new DataTable();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dtGetISaList = customerDao.GetISaList(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetSchemeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetISaList;
        }

        public DataSet GetSchemeDetails(int schemePlanCode)
        {
            DataSet dsGetSchemeDetails = new DataSet();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dsGetSchemeDetails = customerDao.GetSchemeDetails(schemePlanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetSchemeDetails()");
                object[] objects = new object[1];
                objects[0] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetSchemeDetails;
        }


        public DataSet GetAMCExternalType()
        {
            DataSet dsGetAMCExternalType = new DataSet();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dsGetAMCExternalType = customerDao.GetAMCExternalType();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetAMCExternalType()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetAMCExternalType;
        }

        public CustomerVo GetCustomer(int customerId)
        {

            CustomerVo customerVo = new CustomerVo();
            CustomerDao customerDao = new CustomerDao();

            try
            {
                customerVo = customerDao.GetCustomer(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomer()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerVo;

        }


        /// <summary>
        /// Used to Create Customer User
        /// </summary>
        /// <param name="customerVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CreateCustomerUser(CustomerVo customerVo, int userId)
        {

            int custUserId;

            CustomerDao customerDao = new CustomerDao();
            try
            {
                custUserId = customerDao.CreateCustomerUser(customerVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:CreateCustomerUser()");


                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return custUserId;
        }
        public bool ChckBussinessDate(DateTime chckdate)
        {
            CustomerDao customerDao = new CustomerDao();
            bool isCorrect=false;
            try
            {
              isCorrect=customerDao.ChckBussinessDate(chckdate);
                

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isCorrect;
        }

        /// <summary>
        /// Used to Get Customer User Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public UserVo GetCustomerUser(int customerId)
        {
            UserVo userVo = null;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                userVo = customerDao.GetCustomerUser(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerUser()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return userVo;

        }

        /// <summary>
        /// Used to Search mParticular Customer
        /// </summary>
        /// <param name="advisorId"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public DataSet SearchCustomers(int advisorId, string firstName)
        {
            CustomerDao customerDao = new CustomerDao();
            return customerDao.SearchCustomers(advisorId, firstName);
        }

        /// <summary>
        /// Used to Get details about that Particular Customer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CustomerVo GetCustomerInfo(int userId)
        {


            CustomerVo customerVo = new CustomerVo();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                customerVo = customerDao.GetCustomerInfo(userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerInfo()");


                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerVo;
        }

        /// <summary>
        /// Used to Update Customer Details
        /// </summary>
        /// <param name="customerVo"></param>
        /// <returns></returns>
        public bool UpdateCustomer(CustomerVo customerVo)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();

            try
            {
                bResult = customerDao.UpdateCustomer(customerVo);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:EditCustomer()");


                object[] objects = new object[1];
                objects[0] = customerVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return bResult;
        }


        /// <summary>
        /// Used to Get Customer Proof List
        /// </summary>
        /// <param name="customerType"></param>
        /// <param name="KYCFlag"></param>
        /// <param name="assetInterest"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetProofList(string customerType, int KYCFlag, string assetInterest, string path)
        {
            List<string> prooflist;
            CustomerVo customerVo;
            CustomerDao customerDao;
            string assetCode;
            string filterCategory;
            try
            {
                customerVo = new CustomerVo();
                assetCode = assetInterest;
                customerDao = new CustomerDao();
                filterCategory = customerDao.GetAssestCode(assetCode, customerType, KYCFlag);
                prooflist = customerDao.GetProofList(filterCategory, path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAssetCode()");


                object[] objects = new object[4];
                objects[0] = assetInterest;
                objects[1] = customerType;
                objects[2] = KYCFlag;
                objects[3] = path;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return prooflist;
        }

        /// <summary>
        /// Used to Save Customer Proofs 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerProof"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        public bool SaveCustomerProofs(int customerId, int customerProof, int userId)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            CustomerBo customerBo = new CustomerBo();
            string proofId;

            try
            {
                //proofId=customerBo.GenerateId();
                bResult = customerDao.SaveCustomerProofs(customerId, customerProof, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:SaveCustomerProofs()");


                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = customerProof;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }


        /// <summary>
        /// Used to get Customer Proofs Code
        /// </summary>
        /// <param name="proof"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetCustomerProofCode(string proof, string path)
        {
            CustomerDao customerDao = new CustomerDao();
            int proofCode;

            try
            {
                proofCode = customerDao.GetCustomerProofCode(proof, path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerProofCode()");


                object[] objects = new object[2];
                objects[0] = proof;
                objects[2] = path;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return proofCode;
        }

        /// <summary>
        /// Used to Get Customer Proofs
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="currentPage"></param>
        /// <param name="Count"></param>
        /// <returns></returns>

        public DataSet GetCustomerProofs(int customerId, int currentPage, out int Count)
        {
            DataSet ds = null;
            CustomerDao customerDao = new CustomerDao();

            try
            {
                ds = customerDao.GetCustomerProofs(customerId, currentPage, out Count);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerProofs()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return ds;
        }

        public string GenerateId()
        {
            Guid id = new Guid();
            id = Guid.NewGuid();
            return id.ToString();
        }


        /// <summary>
        /// Used to Delete Particular Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int customerId, string Flag)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.DeleteCustomer(customerId, Flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteCustomer()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public int GetAssociationCount(string Flag, int CustomerId)
        {
            CustomerDao customerDao = new CustomerDao();
            return customerDao.CustomerAssociation(Flag, CustomerId);

        }


        /// <summary>
        /// Used to Create Complete Customer Details
        /// </summary>
        /// <param name="customerVo"></param>
        /// <param name="userVo"></param>
        /// <param name="customerPortfolioVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> CreateCompleteCustomer(CustomerVo customerVo, UserVo userVo, CustomerPortfolioVo customerPortfolioVo, int userId)
        {
            List<int> customerIds = new List<int>();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                customerIds = customerDao.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:CreateCompleteCustomer()");


                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerPortfolioVo;
                objects[3] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerIds;
        }


        /// <summary>
        /// Used to Get Customer Proof List
        /// </summary>
        /// <param name="customerType"></param>
        /// <param name="proofCategory"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetProofList(int customerType, int proofCategory, int customerId)
        {
            DataTable dt = new DataTable();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dt = customerDao.GetProofList(customerType, proofCategory, customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetProofList()");
                object[] objects = new object[3];
                objects[0] = customerType;
                objects[1] = proofCategory;
                objects[2] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dt;
        }


        /// <summary>
        /// Used to Delete Customer Proofs
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="proofCode"></param>
        /// <returns></returns>
        public bool DeleteCustomerProof(int customerId, int proofCode)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.DeleteCustomerProof(customerId, proofCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteCustomerProof()");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = proofCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool UpdateCustomerAssignedRM(int[] customerIds, int[] rmIds)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.UpdateCustomerAssignedRM(customerIds, rmIds);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:UpdateCustomerAssignedRM()");
                object[] objects = new object[2];
                objects[0] = customerIds;
                objects[1] = rmIds;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }


        /// <summary>
        /// Used to Get Customer Income Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerIncomeDetails(int customerId)
        {
            DataTable dt = new DataTable();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dt = customerDao.GetCustomerIncomeDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        /// <summary>
        /// Used to add Customer Income Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerIncomeVo"></param>
        /// <returns></returns>

        public bool AddCustomerIncomeDetails(int rmUserId, int customerId, CustomerIncomeVo customerIncomeVo)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.AddCustomerIncomeDetails(rmUserId, customerId, customerIncomeVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }


        /// <summary>
        /// Used to Update Customer Income Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerIncomeVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerIncomeDetails(int rmUserId, int customerId, CustomerIncomeVo customerIncomeVo)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.UpdateCustomerIncomeDetails(rmUserId, customerId, customerIncomeVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:UpdateCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Used to Get Customer Property Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerPropertyDetails(int customerId)
        {
            DataTable dt = new DataTable();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dt = customerDao.GetCustomerPropertyDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }


        /// <summary>
        /// Used to Get Customer Expense Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerExpenseDetails(int customerId)
        {
            DataTable dt = new DataTable();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dt = customerDao.GetCustomerExpenseDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerExpenseDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }


        /// <summary>
        /// Used to Add Customer Expense Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerExpenseVo"></param>
        /// <returns></returns>
        public bool AddCustomerExpenseDetails(int rmUserId, int customerId, CustomerExpenseVo customerExpenseVo)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.AddCustomerExpenseDetails(rmUserId, customerId, customerExpenseVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerExpenseDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Update Customer Expense Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerExpenseVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerExpenseDetails(int rmUserId, int customerId, CustomerExpenseVo customerExpenseVo)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.UpdateCustomerExpenseDetails(rmUserId, customerId, customerExpenseVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:UpdateCustomerExpenseDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Uesd to do Pan Number Duplication Check
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="panNumber"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public bool PANNumberDuplicateCheck(int adviserId, string panNumber, int CustomerId)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.PANNumberDuplicateCheck(adviserId, panNumber, CustomerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:PANNumberDuplicateCheck()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = panNumber;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Used to Get Customer PaN Details and Address Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerPanAddress(int customerId)
        {
            DataTable dt = new DataTable();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dt = customerDao.GetCustomerPanAddress(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerPanAddress(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        /// <summary>
        /// Get RM Group Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetParentCustomerName(string prefixText, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetParentCustomerName(prefixText, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetParentCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        // <summary>
        /// Get RM Group Customer Names for Grouping
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetParentCustomers(string prefixText, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetParentCustomers(prefixText, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetParentCustomers()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// Get RM Individual Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetMemberCustomerName(string prefixText, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetMemberCustomerName(prefixText, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetMemberCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// NO Use
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>

        public DataTable GetCustomerName(string prefixText, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetCustomerName(prefixText, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// Get Advisor Individual Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataTable GetAdviserCustomerPan(string prefixText, int adviserId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAdviserCustomerPan(prefixText, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAdviserCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetAdviserCustomerName(string prefixText, int adviserId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAdviserCustomerName(prefixText, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAdviserCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// Get Advisor Group Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataTable GetAdviserGroupCustomerName(string prefixText, int adviserId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAdviserGroupCustomerName(prefixText, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAdviserCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// Function to check whether a customer is group head or not
        /// </summary>
        /// <param name="advisorId"></param>
        /// <param name="classificationCode"></param>
        /// <returns></returns>
        public bool CheckCustomerGroupHead(int customerId)
        {
            bool result = false;

            CustomerDao customerDao = new CustomerDao();
            try
            {

                result = customerDao.CheckCustomerGroupHead(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorLOBBo.cs:CheckCustomerGroupHead()");


                object[] objects = new object[1];
                objects[0] = customerId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }
        public int GetCustomerGroupHead(int customerId)
        {
            int result;

            CustomerDao customerDao = new CustomerDao();

            {

                result = customerDao.GetCustomerGroupHead(customerId);

            }
            return result;

        }
        //FP SuperLite Related Functions
        //===================================================================================================================================

        /// <summary>
        /// Used to get Customer Relation
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomerRelation()
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtGetCustomerRelation = new DataTable();
            try
            {
                dtGetCustomerRelation = customerDao.GetCustomerRelation();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerRelation()");
                object[] objects = new object[0];
                objects[0] = "BoRelationshipProblem";
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerRelation;
        }


        /// <summary>
        /// Used ot Get Customer Details for Prospect List
        /// </summary>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetCustomerDetailsForProspectList(int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtGGetCustomerDetails = new DataTable();
            try
            {
                dtGGetCustomerDetails = customerDao.GetCustomerDetailsForProspectList(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerDetailsForProspectList(int rmId)");
                object[] objects = new object[0];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGGetCustomerDetails;
        }

        /// <summary>
        /// Get RM Individual Customer Names for Grouping
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetMemberCustomerNamesForGrouping(string prefixText, int selectedParentId, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetMemberCustomerNamesForGrouping(prefixText, selectedParentId, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetMemberCustomerNamesForGrouping()");
                object[] objects = new object[3];
                objects[0] = prefixText;
                objects[1] = rmId;
                objects[2] = selectedParentId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }


        public DataSet GetCustomerPortfolioList(int customerId)
        {

            CustomerDao customerDao = new CustomerDao();

            return customerDao.GetCustomerPortfolioList(customerId);


        }
        public DataTable GetAllCustomerName(string prefixText, int adviserId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAllCustomerName(prefixText, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAllCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public DataTable GetAllRMMemberCustomerName(string prefixText, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAllRMMemberCustomerName(prefixText, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAllRMMemberCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }


        public DataTable BindDropDownassumption(string flag)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtBindDropDownassumption = new DataTable();
            try
            {
                dtBindDropDownassumption = customerDao.BindDropDownassumption(flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtBindDropDownassumption;
        }
        public void InsertCustomerStaticDetalis(int userId, int customerId, decimal assumptionValue, string assumptionType)
        {
            CustomerDao customerDao = new CustomerDao();
            try
            {
                customerDao.InsertCustomerStaticDetalis(userId, customerId, assumptionValue, assumptionType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool InsertProductAMCSchemeMappingDetalis(int schemePlanCode, string externalCode, string externalType,DateTime createdDate ,DateTime editedDate,DateTime deletedDate)
        {
            bool isInserted = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
              isInserted=customerDao.InsertProductAMCSchemeMappingDetalis(schemePlanCode, externalCode, externalType, createdDate , editedDate, deletedDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isInserted;
        }

        public void UpdateCustomerProjectedDetalis(int userId, int customerId, decimal assumptionValue, string assumptionType)
        {
            CustomerDao customerDao = new CustomerDao();
            customerDao.UpdateCustomerProjectedDetalis(userId, customerId, assumptionValue, assumptionType);
        }
        public int ExpiryAgeOfAdviser(int adviserId, int customerId)
        {
            CustomerDao customerDao = new CustomerDao();
            int expiryAge;
            try
            {
                expiryAge = customerDao.ExpiryAgeOfAdviser(adviserId, customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return expiryAge;
        }
        public DataSet GetAllCustomersAssumptions(int customerId,int adviserId)
        {
            CustomerDao customerDao = new CustomerDao();
            DataSet dsGetAllCustomersAssumptions;
            try
            {
                dsGetAllCustomersAssumptions = customerDao.GetAllCustomersAssumptions(customerId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsGetAllCustomersAssumptions;
        }

        public void InsertPlanPreferences(int customerId, int calculationBasisId, int calculationId)
        {
            CustomerDao customerDao = new CustomerDao();
            try
            {
                customerDao.InsertPlanPreferences(customerId, calculationBasisId, calculationId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public DataSet SetDefaultPlanRetirementValueForCustomer(int customerId)
        {
            CustomerDao customerDao = new CustomerDao();
            DataSet dsSetDefaultPlanRetirementValueForCustomer;
            try
            {
                dsSetDefaultPlanRetirementValueForCustomer = customerDao.SetDefaultPlanRetirementValueForCustomer(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSetDefaultPlanRetirementValueForCustomer;
        }
        
        
        public DataSet GetCustomerTaxSlab(int CustomerID,int age, string Gender)
        {
            CustomerDao customerDao = new CustomerDao();
            DataSet dsGetTaxSlab = new DataSet(); 
            try
            {
                dsGetTaxSlab = customerDao.GetCustomerTaxSlab(CustomerID,age, Gender);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAllRMMemberCustomerName()");


                object[] objects = new object[4];
                objects[0] = age;
                objects[1] = Gender;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetTaxSlab;

        }


        public DataTable GetBMParentCustomerNames(string prefixText, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetBMParentCustomerNames(prefixText, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetBMParentCustomerNames()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetBMIndividualCustomerNames(string prefixText, int rmId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetBMIndividualCustomerNames(prefixText, rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetBMIndividualCustomerNames()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        // Added for FP Sectional Report.. Added on 13th June 2011
        public DataSet DefaultFPReportsAssumtion(int customerId)
        {
            CustomerDao customerDao = new CustomerDao();
            DataSet dsDefaultFPReportsAssumtion = new DataSet();
            try
            {
                dsDefaultFPReportsAssumtion = customerDao.DefaultFPReportsAssumtion(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsDefaultFPReportsAssumtion;
        }

        public void CustomerFPReportsAssumption(int customerId, decimal assumptionInflation, decimal assumptionInvestment, decimal assumptionDr)
        {
            CustomerDao customerDao = new CustomerDao();
            try
            {
                customerDao.CustomerFPReportsAssumption(customerId, assumptionInflation, assumptionInvestment, assumptionDr);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }


        }

        public void AddRMRecommendationForCustomer(int customerId, string strRMRecHTML)
        {
            CustomerDao customerDao = new CustomerDao();


            try
            {
                customerDao.AddRMRecommendationForCustomer(customerId, strRMRecHTML);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }


        public string GetRMRecommendationForCustomer(int customerId)
        {
            CustomerDao customerDao = new CustomerDao();
            string strRMRecommendationHTML;

            try
            {
                strRMRecommendationHTML = customerDao.GetRMRecommendationForCustomer(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return strRMRecommendationHTML;
        }

        // To Check and Delete the Child Customers.. 
        // Added by Vinayak Patil..

        public int CheckAndDeleteTheChildCustomers(string Flag, int CustomerId)
        {
            CustomerDao customerDao = new CustomerDao();
            int associationStatus = 0;

            try
            {
                associationStatus = customerDao.CheckAndDeleteTheChildCustomers(Flag, CustomerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:CheckAndDeleteTheChildCustomers()");


                object[] objects = new object[3];
                objects[0] = Flag;
                objects[1] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associationStatus;
        }


        // To delete the child customer <<Added by Vinayak Patil>>

        public bool DeleteChildCustomer(int customerId, string Flag)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.DeleteChildCustomer(customerId, Flag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteCustomer()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
        public void CheckSpouseRelationship(int customerId, out bool spRelationExist,out bool spDobExist,out bool spAssumptionExist)
        {
            bool spouseRelationExist = false;
            bool spouseDobExist = false;
            bool spouseAssumptionExist = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
               customerDao.CheckSpouseRelationship(customerId, out spouseRelationExist, out spouseDobExist, out spouseAssumptionExist);
               if (!spouseRelationExist)
                   spRelationExist = false;
               else
                   spRelationExist = true;
               if (!spouseDobExist)
                   spDobExist = false;
               else
                   spDobExist = true;
               if (!spouseAssumptionExist)
                   spAssumptionExist = false;
               else
                   spAssumptionExist = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:CheckSpouseRelationship()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            

        }

        public DataTable GetRMBranchIndividualCustomerNames(string contextKey, string prefixText)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetRMBranchIndividualCustomerNames(contextKey, prefixText);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetBMIndividualCustomerNames()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetRMBranchGroupCustomerNames(string contextKey, string prefixText)
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetRMBranchGroupCustomerNames(contextKey,prefixText);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetBMIndividualCustomerNames()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }


        public DataTable GetPerticularBranchsAllIndividualCustomers(string contextKey, string prefixText)
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetPerticularBranchsAllIndividualCustomers(contextKey, prefixText);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetBMIndividualCustomerNames()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetPerticularBranchsAllGroupCustomers(string contextKey, string prefixText)
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetPerticularBranchsAllGroupCustomers(contextKey, prefixText);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetPerticularBranchsAllGroupCustomers()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public bool PANNumberDuplicateChild(int adviserId, string Pan)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.PANNumberDuplicateChild(adviserId, Pan);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:PANNumberDuplicateChild()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = Pan;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }


        // added by Vinayak Patil

        public DataTable GetCustomerProofTypes()
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtCustomerProofs = new DataTable();
            try
            {
                dtCustomerProofs = customerDao.GetCustomerProofTypes();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetPerticularBranchsAllGroupCustomers()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtCustomerProofs;
        }

        public DataTable GetCustomerProofsForTypes(int proofTypeCode)
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtCustomerProofs = new DataTable();
            try
            {
                dtCustomerProofs = customerDao.GetCustomerProofsForTypes(proofTypeCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetPerticularBranchsAllGroupCustomers()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerProofs;
        }

        public DataTable GetCustomerProofPurpose()
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtCustomerProofPurpose = new DataTable();
            try
            {
                dtCustomerProofPurpose = customerDao.GetCustomerProofPurpose();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetPerticularBranchsAllGroupCustomers()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtCustomerProofPurpose;
        }

        public DataTable GetCustomerProofCopy()
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtCustomerProofCopy = new DataTable();
            try
            {
                dtCustomerProofCopy = customerDao.GetCustomerProofCopy();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetPerticularBranchsAllGroupCustomers()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dtCustomerProofCopy;
        }


        public bool CreateCustomersProofUploads(CustomerProofUploadsVO CPUVo, int ProofUploadId, string createOrUpdate)
        {
            bool bStatus = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bStatus = customerDao.CreateCustomersProofUploads(CPUVo, ProofUploadId, createOrUpdate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofTypes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bStatus;
        }


        public DataTable GetCustomerUploadedProofs(int customerId, int proofId)
        {
            DataTable dtGetCustomerUploadedProofs = new DataTable();
            CustomerDao customerDao = new CustomerDao();

            try
            {
                dtGetCustomerUploadedProofs = customerDao.GetCustomerUploadedProofs(customerId, proofId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerUploadedProofs(int customerId)");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerUploadedProofs;
        }

        public bool DeleteCustomerUploadedProofs(int customerId, int proofUploadID, float fBalanceStorage, int adviserId)
        {
            bool bResult = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bResult = customerDao.DeleteCustomerUploadedProofs(customerId, proofUploadID, fBalanceStorage, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteCustomerUploadedProofs()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool CreateCustomersProofPurposes(int ProofUploadedID, string PurposeCode)
        {
            bool bStatus = false;
            CustomerDao customerDao = new CustomerDao();
            try
            {
                bStatus = customerDao.CreateCustomersProofPurposes(ProofUploadedID, PurposeCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:CreateCustomersProofPurposes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bStatus;
        }

        public DataTable GetCustomerUploadedProofPurposes(int proofId)
        {
            DataTable dtGetCustomerUploadedProofPurposes = new DataTable();
            CustomerDao customerDao = new CustomerDao();

            try
            {
                dtGetCustomerUploadedProofPurposes = customerDao.GetCustomerUploadedProofPurposes(proofId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerUploadedProofPurposes(int customerId)");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerUploadedProofPurposes;
        }

        public bool DeleteMappedSchemeDetails(int schemePlanCode, string strExtCode, string strExtName, DateTime createdDate, DateTime editedDate, DateTime deletedDate)
        {
            CustomerDao customerDao = new CustomerDao();
            bool isDeleted = false;
            try
            {
                isDeleted = customerDao.DeleteMappedSchemeDetails(schemePlanCode, strExtCode, strExtName, createdDate , editedDate, deletedDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isDeleted;            
        }

        public bool EditProductAMCSchemeMapping(int schemePlanCode,string strExternalCodeToBeEdited, string strExtCode, string strExtName, DateTime createdDate, DateTime editedDate, DateTime deletedDate)
        {
            CustomerDao customerDao = new CustomerDao();
            bool isEdited = false;
            try
            {
                isEdited = customerDao.EditProductAMCSchemeMapping(schemePlanCode, strExternalCodeToBeEdited, strExtCode, strExtName, createdDate, editedDate, deletedDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isEdited;            
        }

        public DataTable GetMemberRelationShip()
        {
            CustomerDao customerDao = new CustomerDao();
            DataTable dtRelationship;
            try
            {
                dtRelationship = customerDao.GetMemberRelationShip();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetMemberRelationShip()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtRelationship;
        }

        public List<int> CreateISACustomerRequest(CustomerVo customerVo, int custCreateFlag, string priority)
        {
            List<int> customerIds = new List<int>();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                customerIds = customerDao.CreateISACustomerRequest(customerVo, custCreateFlag, priority);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return customerIds;
        }
        public void UpdateCustomerISAStageDetails(int requestNumber, string stageStatusCode, string priorityCode, string stepCode, string reasonCode, string comments, string stageToMarkReprocess)
        {
            CustomerDao customerDao = new CustomerDao();
            try
            {
                customerDao.UpdateCustomerISAStageDetails(requestNumber, stageStatusCode, priorityCode, stepCode, reasonCode, comments, stageToMarkReprocess);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }          
        }

        public DataSet GetReasonAndStatus(string purpose)
        {
            DataSet dsReasonandStatus = new DataSet();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dsReasonandStatus = customerDao.GetReasonAndStatus(purpose);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsReasonandStatus;
        }

        public DataSet GetISARequestDetails(int requestId)
        {
            DataSet dsGetISARequestDetails = new DataSet();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dsGetISARequestDetails = customerDao.GetISARequestDetails(requestId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetISARequestDetails;
        }

        public int CreateCustomerBasicProfileDetails(CustomerVo customerVo, int cretaedBy, string PortfolioTypeCode, string PortfolioName)
        {
            CustomerDao customerDao = new CustomerDao();

            int customerId = 0;
            try
            {
                customerId = customerDao.CreateCustomerBasicProfileDetails(customerVo, cretaedBy, PortfolioTypeCode, PortfolioName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:CreateCustomerBasicProfileDetails()");
                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = cretaedBy;
                objects[2] = PortfolioTypeCode;
                objects[3] = PortfolioName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerId;

        }

        public DataTable GetCustomerISAAccounts(int customerId)
        {
            DataTable dtCustomerISAAccountList = new DataTable();
            CustomerDao customerDao = new CustomerDao();

            try
            {
                dtCustomerISAAccountList = customerDao.GetCustomerISAAccounts(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetCustomerISAAccounts(int customerId)");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerISAAccountList;
        }


        public bool UpdateMemberRelation(int AssociationId, string relationCode)
        {
            CustomerDao customerDao = new CustomerDao();
            bool isEdited = false;
            try
            {
                isEdited = customerDao.UpdateMemberRelation(AssociationId, relationCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isEdited;   
        }
        public DataSet GetExceptionList()
        {
            DataSet dsGetExceptionList = new DataSet();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dsGetExceptionList = customerDao.GetExceptionList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetSchemeDetails()");
                //object[] objects = new object[1];
                //objects[0] = schemePlanCode;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionList;
        }
        public DataSet GetExceptionType(bool isISA)
        {
            DataSet dsGetExceptionType = new DataSet();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dsGetExceptionType = customerDao.GetExceptionType(isISA);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetSchemeDetails()");
                //object[] objects = new object[1];
                //objects[0] = schemePlanCode;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionType;
        }
        public DataSet GetExceptionReportMismatchDetails(string userType, int adviserId, int rmId, int CustomerId, int branchheadId, int branchId, int All, int isIndividualOrGroup, string Explist, string Exptype, int Mismatch)
        {
            DataSet dsGetExceptionReportMismatchDetails = new DataSet();
            CustomerDao customerDao=new CustomerDao();
            try
            {
                dsGetExceptionReportMismatchDetails = customerDao.GetExceptionReportMismatchDetails(userType, adviserId, rmId, CustomerId, branchheadId, branchId, All, isIndividualOrGroup, Explist, Exptype, Mismatch);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetAllSystematicMISData()");
                //object[] objects = new object[16];
                //objects[0] = UserType;
                //objects[1] = AdviserId;
                //objects[2] = RmId;
                //objects[3] = CustomerId;
                //objects[4] = BranchHeadId;
                //objects[5] = BranchId;
                //objects[6] = All;
                //objects[7] = Category;
                //objects[8] = SysType;
                //objects[9] = AmcCode;
                //objects[10] = SchemePlanCode;
                //objects[11] = StartDate;
                //objects[12] = EndDate;
                //objects[13] = dtFrom;
                //objects[14] = dtTo;

                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionReportMismatchDetails;
        }
        public DataSet GetExceptionReportDetails(string userType, int adviserId, int rmId, int CustomerId, int branchheadId, int branchId, int All, int isIndividualOrGroup, string Explist, string Exptype,int Mismatch)
        {
            DataSet dsGetExceptionReportDetails = new DataSet();
            CustomerDao customerDao=new CustomerDao();
            try
            {
                dsGetExceptionReportDetails = customerDao.GetExceptionReportDetails(userType, adviserId, rmId, CustomerId, branchheadId, branchId, All, isIndividualOrGroup, Explist, Exptype, Mismatch);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupBo.cs:GetAllSystematicMISData()");
                //object[] objects = new object[16];
                //objects[0] = UserType;
                //objects[1] = AdviserId;
                //objects[2] = RmId;
                //objects[3] = CustomerId;
                //objects[4] = BranchHeadId;
                //objects[5] = BranchId;
                //objects[6] = All;
                //objects[7] = Category;
                //objects[8] = SysType;
                //objects[9] = AmcCode;
                //objects[10] = SchemePlanCode;
                //objects[11] = StartDate;
                //objects[12] = EndDate;
                //objects[13] = dtFrom;
                //objects[14] = dtTo;

                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionReportDetails;
        }
        public bool EditData(string ProData, string FolioData, string FolioNumber, int CustomerId, string Explist)
        {
            CustomerDao customerDao = new CustomerDao();
            bool isUpdated = false;
            try
            {
                isUpdated = customerDao.EditData(ProData, FolioData, FolioNumber, CustomerId, Explist);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isUpdated;
        }

        public DataTable GetISAHoldings(int accountId)
        {
            DataTable dt = new DataTable();
            CustomerDao customerDao = new CustomerDao();
            try
            {
                dt = customerDao.GetISAHoldings(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetISAHoldings(int accountId)");
                object[] objects = new object[1];
                objects[0] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        public DataTable GetholdersName(int ISANumber)
        {
            DataTable dtGetholdersName = new DataTable();
             CustomerDao customerDao = new CustomerDao();
             try
             {
                 dtGetholdersName = customerDao.GetholdersName(ISANumber);
             }
             catch (BaseApplicationException Ex)
             {
                 throw Ex;
             }
             catch (Exception Ex)
             {
                 BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                 NameValueCollection FunctionInfo = new NameValueCollection();
                 FunctionInfo.Add("Method", "CustomerBo.cs:GetISAHoldings(int accountId)");
                 //object[] objects = new object[1];
                 //objects[0] = accountId;
                 //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                 exBase.AdditionalInformation = FunctionInfo;
                 ExceptionManager.Publish(exBase);
                 throw exBase;
             }
             return dtGetholdersName;
        }

        public bool CheckIfISAAccountGenerated(string requestNumber)
        {
            bool result = false;           
            CustomerDao customerDao = new CustomerDao();
            try
            {
                result = customerDao.CheckIfISAAccountGenerated(int.Parse(requestNumber));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }

        public DataTable GetBMParentCustomers(string prefixText, int bmId, int parentId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetBMParentCustomers(prefixText, bmId,parentId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
           
            return dtCustomerNames;
        }
        public DataTable GetAdviserAllCustomerForAssociations(string prefixText, int adviserId,int parentId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAdviserAllCustomerForAssociations(prefixText, adviserId,parentId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
           
            return dtCustomerNames;
        }
        public DataTable GetAssociateCustomerName(string prefixText, int AgentId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAssociateCustomerName(prefixText, AgentId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAssociateCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public DataTable GetAssociateGroupCustomerName(string prefixText, int AgentId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAssociateGroupCustomerName(prefixText, AgentId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAssociateGroupCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public DataTable GetAgentCodeAssociateDetails(string prefixText, int adviserId)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAgentCodeAssociateDetails(prefixText, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAdviserCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public DataTable GetAgentId(int adviserid, int agentid)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAgentId(adviserid, agentid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAdviserCustomerName()");


                object[] objects = new object[0];
                objects[0] = adviserid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public DataTable GetAssociateName(int adviserId, string agentcode)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAssociateName(adviserId, agentcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAdviserCustomerName()");


                object[] objects = new object[0];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public DataTable GetAgentCodeAssociateDetailsForAssociates(string prefixText, string agentcode)
        {
            CustomerDao customerDao = new CustomerDao();

            DataTable dtCustomerNames = new DataTable();
            try
            {
                dtCustomerNames = customerDao.GetAgentCodeAssociateDetailsForAssociates(prefixText, agentcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:GetAdviserCustomerName()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

    }
}
