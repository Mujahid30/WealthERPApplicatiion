using System;
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
            int result ;

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

    }

}
