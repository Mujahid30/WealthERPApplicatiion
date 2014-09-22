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
         
        public int CreateCustomerMFAccountBasic(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int accountId;
            try
            {
                accountId = customerAccountDao.CreateCustomerMFAccountBasic(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerMFAccountBasic()");


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


        public DataSet GetISADetails(int IsaAccountId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsISADetails;
            try
            {
                dsISADetails = customerAccountDao.GetISADetails(IsaAccountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetISADetails()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsISADetails;
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

        public DataSet GetCustomerMFAccounts(int portfolioId, string assetGroup, int schemePlanCode)
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

        public DataSet GetCustomerFamilyDetail(int customerId)
        {
            CustomerAccountDao customerAccountsDao = new CustomerAccountDao();
            DataSet dsCustomerAssociates;
            try
            {
                dsCustomerAssociates = customerAccountsDao.GetCustomerFamilyDetail(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetCustomerFamilyDetail()");


                object[] objects = new object[1];
                objects[0] = customerId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsCustomerAssociates;

        }
        public DataSet GetCustomerGuardians(int customerId)
        {
            CustomerAccountDao customerAccountsDao = new CustomerAccountDao();
            DataSet dsCustomerAssociates;
            try
            {
                dsCustomerAssociates = customerAccountsDao.GetCustomerGuardians(customerId);
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

        public bool UpdateBankDetails(int customerid, string bankCode, int amcCode, string folioNo)
        {
            CustomerAccountDao customerAccountsDao = new CustomerAccountDao();
            bool isUpdated = false;
            try
            {
                isUpdated = customerAccountsDao.UpdateBankDetails(customerid, bankCode, amcCode, folioNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        public DataSet GetCustomerAssociatedRelForCashAndSavings(int customerId,string strVisibility)
        {
            CustomerAccountDao customerAccountsDao = new CustomerAccountDao();
            DataSet dsCustomerAssociates;
            try
            {
                dsCustomerAssociates = customerAccountsDao.GetCustomerAssociatedRelForCashAndSavings(customerId, strVisibility);
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
        public DataSet GetCustomerDematAccountAssociatesDetails(int customerId, string type)
        {
            CustomerAccountDao customerAccountsDao = new CustomerAccountDao();
            DataSet dsCustomerDematAccountAssociatesDet= new DataSet();
            try
            {
                dsCustomerDematAccountAssociatesDet = customerAccountsDao.GetCustomerDematAccountAssociatesDetails(customerId, type);
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
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1]=type;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsCustomerDematAccountAssociatesDet;

        }

        public DataSet GetCustomerAssociatedRel(int customerId)
        {
            CustomerAccountDao customerAccountsDao = new CustomerAccountDao();
            DataSet dsCustomerAssociates;
            try
            {
                dsCustomerAssociates = customerAccountsDao.GetCustomerAssociatesRel(customerId);
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

        public DataSet GetInsuranceAccountAssociation(int accountId)
        {
            CustomerAccountDao customerAccountsDao = new CustomerAccountDao();
            DataSet ds;
            try
            {
                ds = customerAccountsDao.GetInsuranceAccountAssociation(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetInsuranceAccountAssociation()");

                object[] objects = new object[1];
                objects[0] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
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

        public bool CreateInsuranceAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId, string associationIds)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreateInsuranceAccountAssociation(customerAccountAssociationVo, userId, associationIds);

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
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
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

        public List<CustomerAccountsVo> GetAdviserMFAccountList(int adviserId, int CurrentPage, string sortOrder, out int count, string NameFilter, string amcName, out Dictionary<string, string> genDictAMC)
        {
            List<CustomerAccountsVo> customerAccountsList = new List<CustomerAccountsVo>();
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountsList = customerAccountDao.GetAdviserMFAccountList(adviserId, CurrentPage, sortOrder, out count, NameFilter, amcName, out genDictAMC);
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
        public bool DeleteInsuranceAccount(int accountId)
        {
            bool Delete;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            Delete = customerAccountDao.DeleteInsuranceAccount(accountId);
            return Delete;

        }

        #endregion


        public DataSet GetBankName(int customerId, string accountNo)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsgetBankName = new DataSet();
            try
            {
                dsgetBankName = customerAccountDao.GetBankName(customerId, accountNo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetBankName()");
                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = accountNo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsgetBankName;
        }

        public DataSet GetAccountNumber(int customerId, string categoryType)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsgetAccountNo;
            try
            {
                dsgetAccountNo = customerAccountDao.GetAccountNumber(customerId, categoryType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetAccountNo()");
                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = categoryType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsgetAccountNo;
        }

        public bool CreateISAAccountAssociation(CustomerISAAccountsVo customerISAAccountAssociationVo, int userId)
        {
            bool bResult = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                bResult = customerAccountDao.CreateISAAccountAssociation(customerISAAccountAssociationVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateISAAccountAssociation()");

                object[] objects = new object[2];
                objects[0] = customerISAAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool UpdateISAAccountAssociation(CustomerISAAccountsVo customerISAAccountAssociationVo, string associationIds)
        {
            bool bResult = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                bResult = customerAccountDao.UpdateISAAccountAssociation(customerISAAccountAssociationVo, associationIds);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateISAAccountAssociation()");

                object[] objects = new object[2];
                objects[0] = customerISAAccountAssociationVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool UpdateCustomerISAAccount(CustomerISAAccountsVo customerISAAccountVo)
        {
            bool IsISAUpdated = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int customerISAAccountId = 0;
            try
            {
                IsISAUpdated = Convert.ToBoolean(customerAccountDao.UpdateCustomerISAAccount(customerISAAccountVo));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerISAAccount()");

                object[] objects = new object[3];
                objects[0] = customerISAAccountVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return IsISAUpdated;
        }

        public int CreateCustomerISAAccount(CustomerISAAccountsVo customerISAAccountVo, int customerId, int userId, int requestId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int customerISAAccountId = 0;
            try
            {
                customerISAAccountId = customerAccountDao.CreateCustomerISAAccount(customerISAAccountVo, customerId, userId, requestId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerISAAccount()");

                object[] objects = new object[3];
                objects[0] = customerISAAccountVo;
                objects[1] = userId;
                objects[2] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerISAAccountId;
        }


        public DataTable GetISAListForFolioMapping(int CustomerId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataTable dtgetISAList;
            try
            {
                dtgetISAList = customerAccountDao.GetISAListForFolioMapping(CustomerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerISAAccount()");

                object[] objects = new object[1];
                objects[0] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtgetISAList;
        }

        public DataTable GetBindAttachedFolioGrid(int AccountId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataTable dtBindAttachedFolioGrid;
            try
            {
                dtBindAttachedFolioGrid = customerAccountDao.GetBindAttachedFolioGrid(AccountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtBindAttachedFolioGrid;
        }

        public DataTable GetAvailableFolioList(int CustomerId, int AccountId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataTable dtAvailableFolioList;
            try
            {
                dtAvailableFolioList = customerAccountDao.GetAvailableFolioList(CustomerId, AccountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetAvailableFolioList()");
                object[] objects = new object[2];
                objects[0] = CustomerId;
                objects[1] = AccountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAvailableFolioList;
        }

        public bool UpdateMFAccount(int cmfaAccountId, int isaAccountId, int AMCCode, int PortfolioId, string ModeOfHoldingCode, int IsJointlyHeld)
        {
            bool Result = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                Result = customerAccountDao.UpdateMFAccount(cmfaAccountId, isaAccountId, AMCCode, PortfolioId, ModeOfHoldingCode, IsJointlyHeld, out Result);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return Result;
        }

        public DataSet GetCustomerISAAssociatedRel(int IsaAccountId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsGetISAAssociatedRel;
            try
            {
                dsGetISAAssociatedRel = customerAccountDao.GetCustomerISAAssociatedRel(IsaAccountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetISAAssociatedRel;
        }

        public int GetRequestNo(int customerId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            int requestNo = 0;
            try
            {
                requestNo = customerAccountDao.GetRequestNo(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreateCustomerISAAccount()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return requestNo;
        }

        public bool DeleteISAAccount(int ISAAccounts)
        {
            bool IsDelete;
            try
            {
                CustomerAccountDao customerAccountDao = new CustomerAccountDao();
                IsDelete = customerAccountDao.DeleteISAAccount(ISAAccounts);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return IsDelete;
        }
        public DataSet GetAccountType()
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsGetAccountType;
            try
            {
                dsGetAccountType = customerAccountDao.GetAccountType();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetAccountType;
        }
        public DataSet GetEQAccountNumber(int customerId, string bankId)
        {//
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet dsgetAccountNo;
            try
            {
                dsgetAccountNo = customerAccountDao.GetEQAccountNumber(customerId, bankId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetAccountNo()");
                object[] objects = new object[4];
                objects[0] = customerId;
                //objects[1] = categoryType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsgetAccountNo;
        }


        public bool CreatecustomerBankAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreatecustomerBankAccountAssociation(customerAccountAssociationVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreatecustomerBankAccountAssociation()");


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
        public bool CreatecustomerBankTransaction(CustomerAccountsVo customerAccountVo, int userId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool bResult = false;
            try
            {
                bResult = customerAccountDao.CreatecustomerBankTransaction(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:CreatecustomerBankAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool InsertholdingAmountCustomerBank(CustomerAccountsVo customerAccountVo, int CustomerId)
        {
            bool bResult = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();

            try
            {
                bResult = customerAccountDao.InsertholdingAmountCustomerBank(customerAccountVo,CustomerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:InsertholdingAmountCustomerBank()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public List<CustomerAccountsVo> GetCustomerBankTransaction(int CustBankAccIds,int customerId)
        {
            List<CustomerAccountsVo> customerAccountlist = null;
             CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                customerAccountlist = customerAccountDao.GetCustomerBankTransaction(CustBankAccIds, customerId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:GetCustomerBankTransaction()");


                object[] objects = new object[1];
                objects[0] = CustBankAccIds;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerAccountlist;
        }
        public bool DeleteCustomerBankTransaction(int TransactionId)
        {
            bool bResult = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();

            try
            {
                bResult = customerAccountDao.DeleteCustomerBankTransaction(TransactionId);
            }
            catch (BaseApplicationException Ex)
            {
                bResult = false;
            }

            return bResult;
        }


        public bool UpdateCustomerBankTransaction(CustomerAccountsVo customerAccountVo,int TransactionId)
        {
            bool bResult = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();

            try
            {
                bResult = customerAccountDao.UpdateCustomerBankTransaction(customerAccountVo,TransactionId);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:EditCustomerBankTransaction()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects [1]=TransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }
        public DataTable GetCashFlowCategory()
        {
            DataTable dt = new DataTable();
            //UserVo userVo = null;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                //  userVo = userDao.Getselectlist();
                dt = customerAccountDao.GetCashFlowCategory();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dt;
        }
        public bool CheckTransactionExistanceOnHoldingAdd(int CBAccountNumber)
        {
            bool bResult = false;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();

            try
            {
                bResult = customerAccountDao.CheckTransactionExistanceOnHoldingAdd(CBAccountNumber);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:CheckTransactionExistanceOnHoldingAdd()");
                object[] objects = new object[2];            
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }

        public DataSet GetBankAccountNumber(int customerId)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            DataSet ds;
            try
            {
                ds = customerAccountDao.GetBankAccountNumber(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:GetBankAccountNumber()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public double GetHoldingBalance(int CB_CustBankAccId)
        {

            double Amount;
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            try
            {
                Amount = customerAccountDao.GetHoldingBalance(CB_CustBankAccId);
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountBo.cs:GetHoldingBalance()");


                object[] objects = new object[3];
                objects[0] = CB_CustBankAccId;
               


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return Amount;

        }


        public bool CheckFolioDuplicate(int advisetId, string folioNumber)
        {
            CustomerAccountDao customerAccountDao = new CustomerAccountDao();
            bool isduplicate;
           
            try
            {
                isduplicate = customerAccountDao.CheckFolioDuplicate(advisetId, folioNumber);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:CheckFolioDuplicate(int customerId, string folioNumber)");
                object[] objects = new object[2];
                objects[0] = advisetId;
                objects[1] = folioNumber;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isduplicate;

        }
    }
}
