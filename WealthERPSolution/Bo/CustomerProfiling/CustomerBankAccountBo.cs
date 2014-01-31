using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using DaoCustomerProfiling;
using VoCustomerProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoCustomerProfiling
{
    public class CustomerBankAccountBo
    {
        /// <summary>
        /// Method for Creating a Bank Account for the Customer.
        /// </summary>
        /// <param name="customerBankAccountVo">Object holding all the details of the Bank Account to be created.</param>
        /// <param name="customerId">CustomerId of the customer whose Bank Account the method Creates</param>
        /// <param name="userId">UserId of the user who is creating the Bank Account for the Customer</param>
        /// <returns>Returns a boolean Variable stating if the Method is successful</returns>
        public int CreateCustomerBankAccount(CustomerBankAccountVo customerBankAccountVo, int customerId, int userId)
        {

           int accountId;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                accountId = customerBankAccountDao.CreateCustomerBankAccount(customerBankAccountVo, customerId, userId);
               // bResult=true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:CreateCustomerBankAccount()");


                object[] objects = new object[3];
                objects[0] = customerBankAccountVo;
                objects[1] = customerId;
                objects[2] = userId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }

        /// <summary>
        /// Method to get all the Bank Account details for a particular customer
        /// </summary>
        /// <param name="customerId">CustomerId of the customer whose Bank Account Details are to be retrieved.</param>
        /// <returns>Returns a List of Bank Account Objects</returns>
        public List<CustomerBankAccountVo> GetCustomerBankAccounts(int customerId)
        {
            List<CustomerBankAccountVo> customerBankAccountlist = null;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();
            try
            {
                customerBankAccountlist = customerBankAccountDao.GetCustomerBankAccounts(customerId);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:GetCustomerBankAccounts()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerBankAccountlist;
        }

        /// <summary>
        /// Gets All the Details of a Bank Account beloging to a Customer
        /// </summary>
        /// <param name="customerId">CustomerId of the customer who holds the Bank Account</param>
        /// <param name="customerBankAccId">Bank Account Id of the Bank Account to be retrieved</param>
        /// <returns>Returns Object holding Bank Details</returns>
        public CustomerBankAccountVo GetCustomerBankAccount(int customerId, int customerBankAccId)
        {
            CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();
            try
            {
                customerBankAccountVo = customerBankAccountDao.GetCusomerBankAccount(customerId, customerBankAccId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:GetCusomerBankAccount()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = customerBankAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerBankAccountVo;
        }

        /// <summary>
        /// Method to Edit the Bank Account Details
        /// </summary>
        /// <param name="customerBankAccountVo">Object holding all the details of the Bank Account to be created.</param>
        /// <param name="customerId">CustomerId of the customer whose Bank Account the method Edits</param>
        /// <returns>Returns a boolean Variable stating if the Method is successful</returns>
        public bool UpdateCustomerBankAccount(CustomerBankAccountVo customerBankAccountVo, int customerId)
        {
            bool bResult = false;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                bResult = customerBankAccountDao.UpdateCustomerBankAccount(customerBankAccountVo, customerId);
                bResult=true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:EditCustomerBankAccount()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = customerBankAccountVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }

        public bool DeleteCustomerBankAccount(int customerAccountId)
        {
            bool bResult = false;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                bResult = customerBankAccountDao.DeleteCustomerBankAccount(customerAccountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;               
            }          

            return bResult;
        }

        public DataTable GetALLBankName()
        {
            DataTable dt = new DataTable();
            //UserVo userVo = null;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                //  userVo = userDao.Getselectlist();
                dt = customerBankAccountDao.GetALLBankName();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dt;
        }


        public DataTable XMLBankAccountType()
        {
            DataTable dt = new DataTable();
            //UserVo userVo = null;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                //  userVo = userDao.Getselectlist();
                dt = customerBankAccountDao.XMLBankaccountType();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dt;
        }
        public DataTable XMLModeOfHolding()
        {
            DataTable dt = new DataTable();
            //UserVo userVo = null;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                //  userVo = userDao.Getselectlist();
                dt = customerBankAccountDao.XMLModeOfholding();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dt;
        }

        public DataSet GetCustomerIndividualBankDetails(int customerId)
        {
            DataSet ds;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                //  userVo = userDao.Getselectlist();
                ds = customerBankAccountDao.GetCustomerIndividualBankDetails(customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return ds;
        }

        public DataTable AssetBankaccountType()
        {
            DataTable dt = new DataTable();
            //UserVo userVo = null;
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();

            try
            {
                //  userVo = userDao.Getselectlist();
                dt = customerBankAccountDao.AssetBankaccountType();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dt;
        }
         public CustomerBankAccountVo GetCusomerIndBankAccount( int customerBankAccId)
         {      
            CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();
            CustomerBankAccountVo customerBankAccountVo = new CustomerBankAccountVo();
            try
            {
                //  userVo = userDao.Getselectlist();
                customerBankAccountVo = customerBankAccountDao.GetCusomerIndBankAccount(customerBankAccId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return customerBankAccountVo;
        }
         public bool DeleteCustomerBankAccountAssociates(int CB_CustBankAccId)
         {
             bool blResult = false;
             CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();
             try
             {

                 blResult = customerBankAccountDao.DeleteCustomerBankAccountAssociates(CB_CustBankAccId);

             }
             catch (BaseApplicationException Ex)
             {
                 throw Ex;
             }
             catch (Exception Ex)
             {
                 BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                 NameValueCollection FunctionInfo = new NameValueCollection();
                 FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:DeleteCustomerBankAccountAssociates()");
                 object[] objects = new object[1];
                 objects[0] = CB_CustBankAccId;

                 FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                 exBase.AdditionalInformation = FunctionInfo;
                 ExceptionManager.Publish(exBase);
                 throw exBase;

             }
             return blResult;
             }
         public string  Getfolioname(int folioid)
         {
             CustomerBankAccountDao customerBankAccountDao = new CustomerBankAccountDao();
             try
             {
                return  customerBankAccountDao.Getfolioname(folioid);
             }
             catch (BaseApplicationException Ex)
             {
                 throw Ex;
             }
         }
   }
}
