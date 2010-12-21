using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using VoUser;
using DaoCustomerPortfolio;
using BoUser;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoCustomerPortfolio
{
    public class CustomerAccountBo
    {
        public int CreateCustomerCashSavingsAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerCashSavingsAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerCashSavingsAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }
        public CustomerAccountsVo GetGovtSavingsAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountVo = customerAccountDao.GetCustomerGovtSavingsAccount(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetGovtSavingsAccount()");
                object[] objects = new object[1];
                objects[0] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerAccountVo;
        }
        public CustomerAccountsVo GetCashAndSavingsAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountVo = customerAccountDao.GetCashAndSavingsAccount(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCashAndSavingsAccount()");


                object[] objects = new object[1];
                objects[0] = accountId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerAccountVo;
        }
        public CustomerAccountsVo GetCustomerInsuranceAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountVo = customerAccountDao.GetCustomerInsuranceAccount(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerInsuranceAccount()");
                object[] objects = new object[1];
                objects[0] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerAccountVo;

        }
        public CustomerAccountsVo GetCustomerPensionAndGratuitiesAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountVo = customerAccountDao.GetCustomerPensionAndGratuitiesAccount(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerPensionAndGratuitiesAccount()");
                object[] objects = new object[1];
                objects[0] = accountId;              
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerAccountVo;
        }
        public CustomerAccountsVo GetCustomerPropertyAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountVo = customerAccountDao.GetCustomerPropertyAccount(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerPropertyAccount()");
                object[] objects = new object[1];
                objects[0] = accountId;              
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerAccountVo;
        }


        public int CreateCustomerInsuranceAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerInsuranceAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerInsuranceAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }
        public int CreateCustomerFixedIncomeAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerFixedIncomeAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerFixedIncomeAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }


        public int CreateCustomerGovtSavingAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerGovtSavingAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerGovtSavingAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }

        public int CreateCustomerPensionGratuitiesAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerPensionGratuitiesAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerPensionGratuitiesAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }

        public int CreateCustomerMFAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerMFAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerMFsAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }

        public int CreateCustomerEQAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerEQAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerEQAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }

     

        public bool CheckTradeNoAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            bool bResult = false;
            CustomerAccountDao customerAccDao = new CustomerAccountDao();
            try
            {
                bResult = customerAccDao.CheckTradeNoAvailability(TradeAccNo, BrokerCode, PortfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CheckTradeNoAvailability()");


                object[] objects = new object[5];
                objects[0] = TradeAccNo;
                objects[1] = BrokerCode;
                objects[2] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public int CreateCustomerPropertyAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerPropertyAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerPropertyAccount()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;

        }

        public List<CustomerAccountsVo> GetCustomerAccounts(int portfolioId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            List<CustomerAccountsVo> customerAccountsList = new List<CustomerAccountsVo>();
            try
            {
                customerAccountsList = customerAccountDao.GetCustomerAccounts(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerAccounts()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerAccountsList;
        }

        public CustomerAccountsVo GetCustomerFixedIncomeAccount(int accountId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            CustomerAccountsVo customerAccountVo;
            try
            {
                customerAccountVo = customerAccountDao.GetFixedIncomeAccount(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerFixedIncomeAccount()");
                object[] objects = new object[1];                
                objects[0] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerAccountVo;
        }
        public CustomerAccountsVo GetCustomerAccount(int portfolioId, int accountId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            CustomerAccountsVo customerAccountVo = new CustomerAccountsVo();
            try
            {
                customerAccountVo = customerAccountDao.GetCustomerAccount(portfolioId, accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerAccount()");


                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerAccountVo;
        }
        public DataSet GetCustomerAssetAccounts(int portfolioId, string assetGroup)
         {
             CustomerAccountDao customerAccountDao = new CustomerAccountDao();
             DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerAssetAccounts(portfolioId, assetGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }
        public DataSet GetCustomerInsuranceAccounts(int portfolioId, string assetGroup)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsInsuranceAccounts;
            try
            {
                dsInsuranceAccounts = customerAccountDao.GetCustomerInsuranceAccounts(portfolioId, assetGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerInsuranceAccounts()");


                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsInsuranceAccounts;
        }

        public DataSet GetCustomerPropertyAccounts(int portfolioId, string assetGroup, string assetCategory, string assetSubCategory)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerPropertyAccounts(portfolioId, assetGroup, assetCategory, assetSubCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerPropertyAccounts()");


                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;
                objects[3] = assetSubCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }



        public DataSet GetCustomerPensionGratuitiesAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerPensionGratuitiesAccounts(portfolioId, assetGroup, assetCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerPensionGratuitiesAccounts()");


                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }

        public DataSet GetCustomerGovtSavingsAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerGovtSavingsAccounts(portfolioId, assetGroup, assetCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerGovtSavingsAccounts()");


                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }

        public DataSet GetCustomerFixedIncomeAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerFixedIncomeAccounts(portfolioId, assetGroup, assetCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerFixedIncomeAccounts()");


                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }

        public DataSet GetCustomerCashSavingsAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerCashSavingsAccounts(portfolioId, assetGroup, assetCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerCashSavingsAccounts()");


                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;
               

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }

        public DataSet GetCustomerMFAccounts(int portfolioId, string assetGroup,int schemePlanCode)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerMFAccounts(portfolioId, assetGroup, schemePlanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerMFAccounts()");


                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }

        public DataSet GetCustomerEQAccounts(int portfolioId, string assetGroup)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsAssetAccounts;
            try
            {
                dsAssetAccounts = customerAccountDao.GetCustomerEQAccounts(portfolioId, assetGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerEQAccounts()");


                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsAssetAccounts;
        }

        public DataSet GetCustomerEQDPAccounts(int portfolioId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsDPAccounts;
            try
            {
                dsDPAccounts = customerAccountDao.GetCustomerEQDPAccounts(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerEQAccounts()");


                object[] objects = new object[1];
                objects[0] = portfolioId;
                



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsDPAccounts;
        }


        public DataSet GetCustomerAssociatedRel(int customerId)
        {
            CustomerAccountDao customerAccountsDao=new CustomerAccountDao();
            DataSet dsCustomerAssociates;
            try
            {
                dsCustomerAssociates=customerAccountsDao.GetCustomerAssociatesRel(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerAssociatesRel()");


                object[] objects = new object[1];
                objects[0] = customerId;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsCustomerAssociates;

        }


        public bool CreatePropertyAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreatePropertyAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreatePropertyAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreatePensionGratuitiesAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreatePensionGratuitiesAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreatePensionGratuitiesAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreateGovtSavingsAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateGovtSavingsAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateGovtSavingsAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreateFixedIncomeAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateFixedIncomeAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateFixedIncomeAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreateCashSavingsAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateCashSavingsAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCashSavingsAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreateInsuranceAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateInsuranceAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateInsuranceAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreateMFAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateMFAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateMFAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreateAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool CreateEQTradeDPAssociation(int tradeId, int dpId, int isDefault, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateEQTradeDPAssociation(tradeId, dpId, isDefault, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateEQTradeDPAssociation()");


                object[] objects = new object[4];
                objects[0] = tradeId;
                objects[1] = dpId;
                objects[2] = isDefault;
                objects[3] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public DataTable GetTradeAccountNumbersByCustomer(int customerId)
        {
            CustomerAccountDao customerAccountDao  = new CustomerAccountDao();
            return customerAccountDao.GetTradeAccountNumbersByCustomer(customerId);
        }


        //public int ChkAccountAvail(int customerId, string tableName)
        //{
        //    int Count = 0;
        //    CustomerAccountDao customerAccountDao = new CustomerAccountDao();
        //    try
        //    {
        //        Count = customerAccountDao.ChkAccountAvail(customerId, tableName);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerAccountBo.cs:ChkAccountAvail()");


        //        object[] objects = new object[2];
        //        objects[0] = customerId;
        //        objects[1] = tableName;


        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return Count;
        //}

        #region AdviserAccountsList

        public List<CustomerAccountsVo> GetAdviserMFAccountList(int adviserId, int CurrentPage, string sortOrder, out int count,string NameFilter, string amcName ,out Dictionary<string, string> genDictAMC)
        {
            List<CustomerAccountsVo> customerAccountsList = new List<CustomerAccountsVo>();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountsList = customerAccountDao.GetAdviserMFAccountList(adviserId, CurrentPage, sortOrder, out count, NameFilter, amcName ,out genDictAMC);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetAdviserMFAccountList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerAccountsList;
        }

        public List<CustomerAccountsVo> GetAdviserEquityAccountList(int adviserId, int CurrentPage, string sortOrder, out int count)
        {
            List<CustomerAccountsVo> customerAccountList = new List<CustomerAccountsVo>();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountList = customerAccountDao.GetAdviserEquityAccountList(adviserId, CurrentPage, sortOrder, out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetAdviserEquityAccountList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerAccountList;
        }

        #endregion
    }
}
