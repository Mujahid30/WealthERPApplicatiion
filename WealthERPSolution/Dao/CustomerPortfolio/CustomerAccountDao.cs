using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerPortfolio;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerPortfolio
{
    public class CustomerAccountDao
    {
        public int CreateCustomerCashSavingsAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            int accountId = 0;
            Database db;
            DbCommand createCustomerCashSavingsAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerCashSavingsAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerCashSavingsAccount");
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerAccountVo.AssetCategory);
                if (customerAccountVo.BankName != null)
                    db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_BankName", DbType.String, customerAccountVo.BankName);
                else
                    db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_BankName", DbType.String, DBNull.Value);
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_IsHeldJointly", DbType.Int16, customerAccountVo.IsJointHolding);
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                if (customerAccountVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_AccountOpeningDate", DbType.Date, customerAccountVo.AccountOpeningDate);
                else
                    db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_AccountOpeningDate", DbType.Date, DBNull.Value);
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerCashSavingsAccountCmd, "@CCSA_ModifiedBy", DbType.String, userId);
                db.AddOutParameter(createCustomerCashSavingsAccountCmd, "@CCSA_AccountId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerCashSavingsAccountCmd) != 0)

                    accountId = int.Parse(db.GetParameterValue(createCustomerCashSavingsAccountCmd, "CCSA_AccountId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerCashSavingsAccount()");


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

        public int CreateCustomerInsuranceAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            int accountId = 0;
            Database db;
            DbCommand createCustomerInsuranceAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerInsuranceAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerInsuranceAccount");
                db.AddInParameter(createCustomerInsuranceAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerInsuranceAccountCmd, "@CIA_PolicyNum", DbType.String, customerAccountVo.PolicyNum);
                db.AddInParameter(createCustomerInsuranceAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerInsuranceAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerAccountVo.AssetCategory);
                db.AddInParameter(createCustomerInsuranceAccountCmd, "@CIA_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(createCustomerInsuranceAccountCmd, "@CIA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerInsuranceAccountCmd, "@CIA_ModifiedBy", DbType.String, userId);
                db.AddOutParameter(createCustomerInsuranceAccountCmd, "@CIA_AccountId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerInsuranceAccountCmd) != 0)

                    accountId = int.Parse(db.GetParameterValue(createCustomerInsuranceAccountCmd, "CIA_AccountId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerInsuranceAccount()");


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
            int accountId = 0;
            Database db;
            DbCommand createCustomerFixedIncomeAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerFixedIncomeAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerFixedIncomeAccount");
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@CFIA_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@CFIA_AccountSource", DbType.String, customerAccountVo.AccountSource);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerAccountVo.AssetCategory);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@CFIA_IsHeldJointly", DbType.Int16, customerAccountVo.IsJointHolding);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@CFIA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerFixedIncomeAccountCmd, "@CFIA_ModifiedBy", DbType.String, userId);
                db.AddOutParameter(createCustomerFixedIncomeAccountCmd, "@CFIA_AccountId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerFixedIncomeAccountCmd) != 0)

                    accountId = int.Parse(db.GetParameterValue(createCustomerFixedIncomeAccountCmd, "CFIA_AccountId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerFixedIncomeAccount()");


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
            int accountId = 0;
            Database db;
            DbCommand createCustomerGovtSavingAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerGovtSavingAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerGovtSavingAccount");
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@CGSA_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@CGSA_AccountSource", DbType.String, customerAccountVo.AccountSource);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerAccountVo.AssetCategory);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@CGSA_IsHeldJointly", DbType.Int16, customerAccountVo.IsJointHolding);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@CGSA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerGovtSavingAccountCmd, "@CGSA_ModifiedBy", DbType.String, userId);
                db.AddOutParameter(createCustomerGovtSavingAccountCmd, "@CGSA_AccountId", DbType.Int32, 5000);
                if (customerAccountVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(createCustomerGovtSavingAccountCmd, "@CGSA_AccountOpeningDate", DbType.DateTime, customerAccountVo.AccountOpeningDate);

                if (db.ExecuteNonQuery(createCustomerGovtSavingAccountCmd) != 0)

                    accountId = int.Parse(db.GetParameterValue(createCustomerGovtSavingAccountCmd, "CGSA_AccountId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerGovtSavingAccount()");


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
            int accountId = 0;
            Database db;
            DbCommand createCustomerPensionGratuitiesAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerPensionGratuitiesAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerPensionGratuitiesAccount");
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@CPGA_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@CPGA_AccountSource", DbType.String, customerAccountVo.AccountSource);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerAccountVo.AssetCategory);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@CPGA_IsHeldJointly", DbType.Int16, customerAccountVo.IsJointHolding);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                if (customerAccountVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@CPGA_AccountOpeningDate", DbType.Date, customerAccountVo.AccountOpeningDate);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@CPGA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerPensionGratuitiesAccountCmd, "@CPGA_ModifiedBy", DbType.String, userId);
                db.AddOutParameter(createCustomerPensionGratuitiesAccountCmd, "@CPGA_AccountId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerPensionGratuitiesAccountCmd) != 0)

                    accountId = int.Parse(db.GetParameterValue(createCustomerPensionGratuitiesAccountCmd, "CPGA_AccountId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerPensionGratuitiesAccount()");


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

        public int CreateCustomerPropertyAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            int accountId = 0;
            Database db;
            DbCommand createCustomerPropertyAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerPropertyAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerPropertyAccount");
                db.AddInParameter(createCustomerPropertyAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerPropertyAccountCmd, "@CPA_AccountNum", DbType.String, customerAccountVo.AccountNum);

                db.AddInParameter(createCustomerPropertyAccountCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, customerAccountVo.AssetSubCategory);
                db.AddInParameter(createCustomerPropertyAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerPropertyAccountCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerAccountVo.AssetCategory);
                db.AddInParameter(createCustomerPropertyAccountCmd, "@CPA_IsHeldJointly", DbType.Int16, customerAccountVo.IsJointHolding);
                db.AddInParameter(createCustomerPropertyAccountCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                db.AddInParameter(createCustomerPropertyAccountCmd, "@CPA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerPropertyAccountCmd, "@CPA_ModifiedBy", DbType.String, userId);
                db.AddOutParameter(createCustomerPropertyAccountCmd, "@CPA_AccountId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerPropertyAccountCmd) != 0)

                    accountId = int.Parse(db.GetParameterValue(createCustomerPropertyAccountCmd, "CPA_AccountId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerPropertyAccount()");


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
            int accountId = 0;
            Database db;
            DbCommand createCustomerMFAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerMFAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerMFAccount");
                db.AddInParameter(createCustomerMFAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_FolioNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(createCustomerMFAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerMFAccountCmd, "@PA_AMCCode", DbType.Int32, customerAccountVo.AMCCode);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_IsJointlyHeld", DbType.Int16, customerAccountVo.IsJointHolding);
                db.AddInParameter(createCustomerMFAccountCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                if (customerAccountVo.AccountOpeningDate == DateTime.MinValue)
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_AccountOpeningDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_AccountOpeningDate", DbType.DateTime, customerAccountVo.AccountOpeningDate);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_ModifiedBy", DbType.String, userId);
                db.AddOutParameter(createCustomerMFAccountCmd, "@CMFA_AccountId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerMFAccountCmd) != 0)

                    accountId = int.Parse(db.GetParameterValue(createCustomerMFAccountCmd, "CMFA_AccountId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerMFAccount()");


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
            int accountId = 0;
            Database db;
            DbCommand createCustomerEQAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerEQAccountCmd = db.GetStoredProcCommand("SP_CreateCustomerEQAccount");
                db.AddInParameter(createCustomerEQAccountCmd, "@CP_PortfolioId", DbType.Int32, customerAccountVo.PortfolioId);
                db.AddInParameter(createCustomerEQAccountCmd, "@CETA_TradeAccountNum", DbType.String, customerAccountVo.TradeNum);
                db.AddInParameter(createCustomerEQAccountCmd, "@PAG_AssetGroupCode", DbType.String, "DE");
                db.AddInParameter(createCustomerEQAccountCmd, "@XB_BrokerCode", DbType.String, customerAccountVo.BrokerCode);
                if (customerAccountVo.AccountOpeningDate != DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerEQAccountCmd, "@CETA_AccountOpeningDate", DbType.DateTime, customerAccountVo.AccountOpeningDate);
                }
                else
                {
                    db.AddInParameter(createCustomerEQAccountCmd, "@CETA_AccountOpeningDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(createCustomerEQAccountCmd, "@CETA_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCustomerEQAccountCmd, "@CETA_ModifiedBy", DbType.String, userId);                
                db.AddInParameter(createCustomerEQAccountCmd, "@CETA_BrokerDeliveryPercentage", DbType.Double, customerAccountVo.BrokerageDeliveryPercentage);
                db.AddInParameter(createCustomerEQAccountCmd, "@CETA_BrokerSpeculativePercentage", DbType.Double, customerAccountVo.BrokerageSpeculativePercentage);
                db.AddInParameter(createCustomerEQAccountCmd, "@CETA_OtherCharges", DbType.Double, customerAccountVo.OtherCharges);
                db.AddOutParameter(createCustomerEQAccountCmd, "@CETA_AccountId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerEQAccountCmd) != 0)
                    accountId = int.Parse(db.GetParameterValue(createCustomerEQAccountCmd, "CETA_AccountId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerEQAccount()");


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
            List<CustomerAccountsVo> customerAccountsList = null;

            CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
            Database db;
            DbCommand getCustomerAccountsCmd;
            DataSet dsGetCustomerAccounts;
            DataTable dtGetCustomerAccounts;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAccountsCmd = db.GetStoredProcCommand("GetCustomerAccounts");
                db.AddInParameter(getCustomerAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);

                dsGetCustomerAccounts = db.ExecuteDataSet(getCustomerAccountsCmd);
                if (dsGetCustomerAccounts.Tables[0].Rows.Count > 0)
                {
                    customerAccountsList = new List<CustomerAccountsVo>();
                    dtGetCustomerAccounts = dsGetCustomerAccounts.Tables[0];
                    customerAccountsList = new List<CustomerAccountsVo>();
                    foreach (DataRow dr in dtGetCustomerAccounts.Rows)
                    {
                        customerAccountsVo = new CustomerAccountsVo();
                        customerAccountsVo.AccountId = int.Parse(dr["CA_AccountId"].ToString());
                        customerAccountsVo.AccountNum = dr["CA_AccountNum"].ToString();
                        customerAccountsVo.AssetClass = dr["CA_AssetClass"].ToString();
                        customerAccountsList.Add(customerAccountsVo);
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerAccounts()");
                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerAccountsList;
        }

        public DataSet GetCustomerAssetAccounts(int portfolioId, string assetGroup)
        {

            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerAssetAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CA_AssetClass", DbType.String, assetGroup);
                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerAccount()");


                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssetAccounts;
        }

        //For Submit Button in Add EQ Account
        public bool CheckTradeNoAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {

            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SP_CheckTradeAccAvailability");

                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_No", DbType.String, TradeAccNo);
                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_BrokerCode", DbType.String, BrokerCode);
                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_PortfolioId", DbType.Int32, PortfolioId);

                ds = db.ExecuteDataSet(chkAvailabilityCmd);
                rowCount = ds.Tables[0].Rows.Count;
                if (rowCount > 0)
                {
                    bResult = false;
                }
                else
                {
                    bResult = true;
                }
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
        //For Update Button in Add EQ Account
        public bool CheckTradeNoAvailabilityForUpdate(string TradeAccNo, string BrokerCode, int PortfolioId)
        {

            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SP_CheckTradeAccAvailabilityForUpdate");

                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_No", DbType.String, TradeAccNo);
                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_BrokerCode", DbType.String, BrokerCode);
                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_PortfolioId", DbType.Int32, PortfolioId);

                ds = db.ExecuteDataSet(chkAvailabilityCmd);
                rowCount = ds.Tables[0].Rows.Count;
                if (rowCount > 0)
                {
                    bResult = false;
                }
                else
                {
                    bResult = true;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CheckTradeNoAvailabilityForUpdate()");


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

        public bool CheckTradeNoMFAvailability(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;

            db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SP_CheckTradeMFAccAvailability");

                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_No", DbType.String, TradeAccNo);
                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_BrokerCode", DbType.String, BrokerCode);
                db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_PortfolioId", DbType.Int32, PortfolioId);

                ds = db.ExecuteDataSet(chkAvailabilityCmd);
                rowCount = ds.Tables[0].Rows.Count;
                if (rowCount > 0)
                {
                    bResult = false;
                }
                else
                {
                    bResult = true;
                }
             return bResult;
        }

        public bool CheckTradeNoAvailabilityAccount(string TradeAccNo, string BrokerCode, int PortfolioId)
        {
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            chkAvailabilityCmd = db.GetStoredProcCommand("SP_CheckTradeMFAccAvailabilityAccount");

            db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_No", DbType.String, TradeAccNo);
            db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_BrokerCode", DbType.String, BrokerCode);
            db.AddInParameter(chkAvailabilityCmd, "@TradeAcc_PortfolioId", DbType.Int32, PortfolioId);

            ds = db.ExecuteDataSet(chkAvailabilityCmd);
            rowCount = ds.Tables[0].Rows.Count;
            if (rowCount > 0)
            {
                bResult = false;
            }
            else
            {
                bResult = true;
            }
            return bResult;
        }

        public DataSet GetCustomerPropertyAccounts(int portfolioId, string assetGroup, string assetCategory, string assetSubCategory)
        {

            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerPropertyAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, assetCategory);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, assetSubCategory);
                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerPropertyAccounts()");


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
            return dsGetCustomerAssetAccounts;
        }

        public CustomerAccountsVo GetCustomerPropertyAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = null;

            Database db;
            DbCommand getCustomerPropAccCmd;
            DataSet dsGetCustomerPropAccount;
            DataTable dtGetCustomerPropAccount;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPropAccCmd = db.GetStoredProcCommand("SP_GetCustomerPropertyAccount");
                db.AddInParameter(getCustomerPropAccCmd, "@CPA_AccountId", DbType.Int32, accountId);
                dsGetCustomerPropAccount = db.ExecuteDataSet(getCustomerPropAccCmd);
                if (dsGetCustomerPropAccount.Tables[0].Rows.Count > 0)
                {
                    customerAccountVo = new CustomerAccountsVo();
                    dtGetCustomerPropAccount = dsGetCustomerPropAccount.Tables[0];
                    dr = dtGetCustomerPropAccount.Rows[0];

                    //customerAccountVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    customerAccountVo.AccountNum = (dr["CPA_AccountNum"].ToString());
                    customerAccountVo.AssetSubCategory = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                    customerAccountVo.AssetCategory = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    customerAccountVo.AssetClass = dr["PAG_AssetGroupCode"].ToString();
                    customerAccountVo.IsJointHolding = int.Parse(dr["CPA_IsHeldJointly"].ToString());
                    customerAccountVo.ModeOfHolding = dr["XMOH_ModeOfHoldingCode"].ToString();
                    customerAccountVo.AccountId = int.Parse(dr["CPA_AccountId"].ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs: ()");
                object[] objects = new object[1];
                objects[0] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerAccountVo;
        }

        public DataSet GetCustomerInsuranceAccounts(int portfolioId, string assetGroup)
        {
            Database db;
            DbCommand getCustomerInsuranceAccountsCmd;
            DataSet dsGetCustomerInsuranceAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerInsuranceAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerInsuranceAccounts");
                db.AddInParameter(getCustomerInsuranceAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerInsuranceAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);


                dsGetCustomerInsuranceAccounts = db.ExecuteDataSet(getCustomerInsuranceAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerInsuranceAccounts()");


                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerInsuranceAccounts;
        }

        public DataSet GetCustomerPensionGratuitiesAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerPensionGratuitiesAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, assetCategory);

                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerPensionGratuitiesAccounts()");


                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssetAccounts;
        }

        public DataSet GetCustomerGovtSavingsAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerGovtSavingsAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, assetCategory);

                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerGovtSavingsAccounts()");


                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssetAccounts;
        }

        public DataSet GetCustomerFixedIncomeAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerFixedIncomeAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, assetCategory);

                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerFixedIncomeAccounts()");


                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssetAccounts;
        }

        public DataSet GetCustomerCashSavingsAccounts(int portfolioId, string assetGroup, string assetCategory)
        {
            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerCashSavingsAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, assetCategory);

                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerCashSavingsAccounts()");


                object[] objects = new object[4];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = assetCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssetAccounts;
        }

        public DataSet GetCustomerMFAccounts(int portfolioId, string assetGroup, int schemePlanCode)
        {
            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerMFAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);
                if (schemePlanCode != 0)
                    db.AddInParameter(getCustomerAssetAccountsCmd, "@schemePlanCode", DbType.String, schemePlanCode);
                else
                    db.AddInParameter(getCustomerAssetAccountsCmd, "@schemePlanCode", DbType.String, DBNull.Value);

                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerMFAccounts()");


                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = assetGroup;
                objects[2] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssetAccounts;
        }

        public DataSet GetCustomerEQAccounts(int portfolioId, string assetGroup)
        {
            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetCustomerAssetAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerEQAccounts");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAssetAccountsCmd, "@PAG_AssetGroupCode", DbType.String, assetGroup);

                dsGetCustomerAssetAccounts = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerEQAccounts()");


                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = assetGroup;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssetAccounts;
        }

        public DataSet GetCustomerEQDPAccounts(int portfolioId)
        {
            Database db;
            DbCommand getCustomerDPAccountsCmd;
            DataSet dsGetCustomerDPAccounts = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerDPAccountsCmd = db.GetStoredProcCommand("SP_GetCustomerEQDPAccounts");
                db.AddInParameter(getCustomerDPAccountsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);


                dsGetCustomerDPAccounts = db.ExecuteDataSet(getCustomerDPAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerEQDPAccounts()");


                object[] objects = new object[2];
                objects[0] = portfolioId;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerDPAccounts;
        }

        public DataSet GetCustomerAssociatesRel(int customerId)
        {
            DataSet dsCustomerAssociates = null;
            DbCommand getCustomerAssociatesCmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssociatesCmd = db.GetStoredProcCommand("SP_GetCustomerAssociatesRel");
                db.AddInParameter(getCustomerAssociatesCmd, "@C_CustomerId", DbType.Int32, customerId);
                dsCustomerAssociates = db.ExecuteDataSet(getCustomerAssociatesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerAssociatesRel()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return dsCustomerAssociates;
        }

        public CustomerAccountsVo GetFixedIncomeAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = null;

            Database db;
            DbCommand getCustomerFixedIncomeAccCmd;
            DataSet dsGetCustomerFixedIncomeAccount;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFixedIncomeAccCmd = db.GetStoredProcCommand("SP_GetCustomerFixedIncomeAccount");
                db.AddInParameter(getCustomerFixedIncomeAccCmd, "@CFIA_AccountId", DbType.Int32, accountId);
                dsGetCustomerFixedIncomeAccount = db.ExecuteDataSet(getCustomerFixedIncomeAccCmd);
                if (dsGetCustomerFixedIncomeAccount.Tables[0].Rows.Count > 0)
                {
                    customerAccountVo = new CustomerAccountsVo();
                    dr = dsGetCustomerFixedIncomeAccount.Tables[0].Rows[0];

                    customerAccountVo.AccountId = int.Parse(dr["CFIA_AccountId"].ToString());
                    customerAccountVo.AccountNum = dr["CFIA_AccountNum"].ToString();
                    customerAccountVo.AccountSource = dr["CFIA_AccountSource"].ToString();
                    customerAccountVo.AssetCategory = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    customerAccountVo.AssetClass = dr["PAG_AssetGroupCode"].ToString();
                    customerAccountVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    customerAccountVo.IsJointHolding = int.Parse(dr["CFIA_IsHeldJointly"].ToString());
                    customerAccountVo.ModeOfHolding = dr["XMOH_ModeOfHoldingCode"].ToString();
                }
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetFixedIncomeAccount ()");
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

            CustomerAccountsVo customerAccountVo = null;
            Database db;
            DbCommand getCustomerCSAccCmd;
            DataSet dsGetCustomerCSAccount;
            DataTable dtGetCustomerCSAccount;
            DataRow dr;
            string temp;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerCSAccCmd = db.GetStoredProcCommand("SP_GetCustomerCashAndSavingsAccount");
                db.AddInParameter(getCustomerCSAccCmd, "@CCSA_AccountId", DbType.Int32, accountId);
                dsGetCustomerCSAccount = db.ExecuteDataSet(getCustomerCSAccCmd);
                if (dsGetCustomerCSAccount.Tables[0].Rows.Count > 0)
                {
                    customerAccountVo = new CustomerAccountsVo();
                    dtGetCustomerCSAccount = dsGetCustomerCSAccount.Tables[0];
                    dr = dtGetCustomerCSAccount.Rows[0];

                    customerAccountVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    customerAccountVo.AccountNum = (dr["CCSA_AccountNum"].ToString());

                    temp = dr["CCSA_AccountOpeningDate"].ToString();
                    if (temp == "")
                    {

                    }
                    else
                    {
                        customerAccountVo.AccountOpeningDate = DateTime.Parse(dr["CCSA_AccountOpeningDate"].ToString());
                    }
                    customerAccountVo.BankName = dr["CCSA_BankName"].ToString();
                    customerAccountVo.AssetCategory = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    customerAccountVo.AssetClass = dr["PAG_AssetGroupCode"].ToString();
                    customerAccountVo.IsJointHolding = int.Parse(dr["CCSA_IsHeldJointly"].ToString());
                    customerAccountVo.ModeOfHolding = dr["XMOH_ModeOfHoldingCode"].ToString();
                    customerAccountVo.AccountId = int.Parse(dr["CCSA_AccountId"].ToString());
                    customerAccountVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCashAndSavingsAccount ()");
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
            CustomerAccountsVo customerAccountVo = null;

            Database db;
            DbCommand getCustomerPGAccCmd;
            DataSet dsGetCustomerPGAccount;
            DataTable dtGetCustomerPGAccount;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPGAccCmd = db.GetStoredProcCommand("SP_GetCustomerPensionAndGratuitiesAccount");
                db.AddInParameter(getCustomerPGAccCmd, "@CPGA_AccountId", DbType.Int32, accountId);
                dsGetCustomerPGAccount = db.ExecuteDataSet(getCustomerPGAccCmd);
                if (dsGetCustomerPGAccount.Tables[0].Rows.Count > 0)
                {
                    customerAccountVo = new CustomerAccountsVo();
                    dtGetCustomerPGAccount = dsGetCustomerPGAccount.Tables[0];
                    dr = dtGetCustomerPGAccount.Rows[0];
                    customerAccountVo.PortfolioId = Int32.Parse(dr["CP_PortfolioId"].ToString());
                    customerAccountVo.AccountNum = dr["CPGA_AccountNum"].ToString();
                    if (dr["CPGA_AccountOpeningDate"] != DBNull.Value)
                        customerAccountVo.AccountOpeningDate = DateTime.Parse(dr["CPGA_AccountOpeningDate"].ToString());
                    customerAccountVo.AccountSource = dr["CPGA_AccountSource"].ToString();
                    customerAccountVo.AssetCategory = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    customerAccountVo.AssetClass = dr["PAG_AssetGroupCode"].ToString();
                    customerAccountVo.IsJointHolding = int.Parse(dr["CPGA_IsHeldJointly"].ToString());
                    customerAccountVo.ModeOfHolding = dr["XMOH_ModeOfHoldingCode"].ToString();
                    customerAccountVo.AccountId = int.Parse(dr["CPGA_AccountId"].ToString());
                    customerAccountVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerPensionAndGratuitiesAccount()");
                object[] objects = new object[1];
                objects[0] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerAccountVo;
        }

        public CustomerAccountsVo GetCustomerGovtSavingsAccount(int accountId)
        {
            CustomerAccountsVo customerAccountVo = null;
            Database db;
            DbCommand getGovtSavingsAccCmd;
            DataSet dsGetGovtSavingsAcc;
            DataTable dtGetGovtSavingsAcc;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGovtSavingsAccCmd = db.GetStoredProcCommand("SP_GetCustomerGovtSavingsAccount");
                db.AddInParameter(getGovtSavingsAccCmd, "CGSA_AccountId", DbType.Int32, accountId);
                dsGetGovtSavingsAcc = db.ExecuteDataSet(getGovtSavingsAccCmd);
                if (dsGetGovtSavingsAcc.Tables[0].Rows.Count > 0)
                {
                    customerAccountVo = new CustomerAccountsVo();
                    dtGetGovtSavingsAcc = dsGetGovtSavingsAcc.Tables[0];
                    dr = dtGetGovtSavingsAcc.Rows[0];
                    customerAccountVo.AccountId = int.Parse(dr["CGSA_AccountId"].ToString());
                    customerAccountVo.AccountNum = dr["CGSA_AccountNum"].ToString();
                    customerAccountVo.AssetCategory = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    customerAccountVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                    customerAccountVo.AssetClass = dr["PAG_AssetGroupCode"].ToString();
                    customerAccountVo.AccountSource = dr["CGSA_AccountSource"].ToString();
                    if (dr["CGSA_AccountOpeningDate"] != DBNull.Value && dr["CGSA_AccountOpeningDate"].ToString() != "")
                    {
                        customerAccountVo.AccountOpeningDate = DateTime.Parse(dr["CGSA_AccountOpeningDate"].ToString());
                    }



                    customerAccountVo.ModeOfHolding = dr["XMOH_ModeOfHoldingCode"].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerGovtSavingsAccount()");
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
            CustomerAccountsVo customerAccountVo = null;

            Database db;
            DbCommand getCustomerInsuranceAccCmd;
            DataSet dsGetInsuranceAcc;
            DataTable dtGetInsuranceAcc;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerInsuranceAccCmd = db.GetStoredProcCommand("SP_GetCustomerInsuranceAccount");
                db.AddInParameter(getCustomerInsuranceAccCmd, "@CIA_AccountId", DbType.Int32, accountId);
                dsGetInsuranceAcc = db.ExecuteDataSet(getCustomerInsuranceAccCmd);
                if (dsGetInsuranceAcc.Tables[0].Rows.Count > 0)
                {
                    customerAccountVo = new CustomerAccountsVo();
                    dtGetInsuranceAcc = dsGetInsuranceAcc.Tables[0];
                    dr = dtGetInsuranceAcc.Rows[0];
                    customerAccountVo.AccountId = int.Parse(dr["CIA_AccountId"].ToString());
                    customerAccountVo.AccountNum = dr["CIA_AccountNum"].ToString();
                    customerAccountVo.AssetCategory = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    customerAccountVo.AssetClass = dr["PAG_AssetGroupCode"].ToString();
                    customerAccountVo.PolicyNum = dr["CIA_PolicyNum"].ToString();
                    customerAccountVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerInsuranceAccount()");
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

            CustomerAccountsVo customerAccountsVo = null;

            Database db;
            DbCommand getCustomerAccountCmd;
            DataSet dsGetCustomerAccount;
            DataTable dtGetCustomerAccount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAccountCmd = db.GetStoredProcCommand("GetCustomerAccount");
                db.AddInParameter(getCustomerAccountCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getCustomerAccountCmd, "@CA_AccountId", DbType.Int32, accountId);
                dsGetCustomerAccount = db.ExecuteDataSet(getCustomerAccountCmd);
                if (dsGetCustomerAccount.Tables[0].Rows.Count > 0)
                {
                    customerAccountsVo = new CustomerAccountsVo();
                    dtGetCustomerAccount = dsGetCustomerAccount.Tables[0];
                    foreach (DataRow dr in dtGetCustomerAccount.Rows)
                    {
                        customerAccountsVo.AccountNum = dr["CA_AccountNum"].ToString();
                        customerAccountsVo.AssetClass = dr["CA_AssetClass"].ToString();
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerAccount()");


                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return customerAccountsVo;
        }

        public bool CreatePropertyAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createPropertyAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createPropertyAccountAssociationCmd = db.GetStoredProcCommand("SP_CreatePropertyAccountAssociation");
                db.AddInParameter(createPropertyAccountAssociationCmd, "@CPA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createPropertyAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createPropertyAccountAssociationCmd, "@CPAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createPropertyAccountAssociationCmd, "@CPAA_JointholderShare", DbType.Int32, customerAccountAssociationVo.NomineeShare);
                db.AddInParameter(createPropertyAccountAssociationCmd, "@CPAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createPropertyAccountAssociationCmd, "@CPAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createPropertyAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreatePropertyAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createPensionGratuitiesAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createPensionGratuitiesAccountAssociationCmd = db.GetStoredProcCommand("SP_CreatePensionGratuitiesAccountAssociation");
                db.AddInParameter(createPensionGratuitiesAccountAssociationCmd, "@CPGA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createPensionGratuitiesAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createPensionGratuitiesAccountAssociationCmd, "@CPGAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createPensionGratuitiesAccountAssociationCmd, "@CPGAA_NomineeShare", DbType.Int32, customerAccountAssociationVo.NomineeShare);
                db.AddInParameter(createPensionGratuitiesAccountAssociationCmd, "@CPGAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createPensionGratuitiesAccountAssociationCmd, "@CPGAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createPensionGratuitiesAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreatePensionGratuitiesAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createGovtSavingsAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGovtSavingsAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateGovtSavingsAccountAssociation");
                db.AddInParameter(createGovtSavingsAccountAssociationCmd, "@CGSA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createGovtSavingsAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createGovtSavingsAccountAssociationCmd, "@CGSAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createGovtSavingsAccountAssociationCmd, "@CGSAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGovtSavingsAccountAssociationCmd, "@CGSAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createGovtSavingsAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateGovtSavingsAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createFixedIncomeAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createFixedIncomeAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateFixedIncomeAccountAssociation");
                db.AddInParameter(createFixedIncomeAccountAssociationCmd, "@CFIA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createFixedIncomeAccountAssociationCmd, "@CA_AssociateId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createFixedIncomeAccountAssociationCmd, "@CFIAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createFixedIncomeAccountAssociationCmd, "@CFIAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createFixedIncomeAccountAssociationCmd, "@CFIAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createFixedIncomeAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateFixedIncomeAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createCashSavingsAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCashSavingsAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateCashSavingsAccountAssociation");
                db.AddInParameter(createCashSavingsAccountAssociationCmd, "@CCSA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createCashSavingsAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createCashSavingsAccountAssociationCmd, "@CCSAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createCashSavingsAccountAssociationCmd, "@CCSAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCashSavingsAccountAssociationCmd, "@CCSAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createCashSavingsAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCashSavingsAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createInsuranceAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createInsuranceAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateInsuranceAccountAssociation");
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createInsuranceAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateInsuranceAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createMFAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateMFAccountAssociation");
                db.AddInParameter(createMFAccountAssociationCmd, "@CMFA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createMFAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createMFAccountAssociationCmd, "@CMFAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createMFAccountAssociationCmd, "@CMFAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createMFAccountAssociationCmd, "@CMFAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createMFAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateMFAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAccountAssociationCmd = db.GetStoredProcCommand("CreateCustomerAccount");
                db.AddInParameter(createAccountAssociationCmd, "@CA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(createAccountAssociationCmd, "@CAA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createAccountAssociationCmd, "@CAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createAccountAssociationCmd, "@CAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createAccountAssociationCmd, "@CAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createDPTradeAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createDPTradeAssociationCmd = db.GetStoredProcCommand("SP_CreateEQTradeDPAssociation");
                db.AddInParameter(createDPTradeAssociationCmd, "@CEDA_DematAccountId", DbType.Int32, dpId);
                db.AddInParameter(createDPTradeAssociationCmd, "@CETA_AccountId", DbType.Int32, tradeId);
                db.AddInParameter(createDPTradeAssociationCmd, "@CETDAA_IsDefault", DbType.Int32, isDefault);
                db.AddInParameter(createDPTradeAssociationCmd, "@CETDAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createDPTradeAssociationCmd, "@CETDAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createDPTradeAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateEQTradeDPAssociation()");

                object[] objects = new object[3];
                objects[0] = dpId;
                objects[1] = tradeId;
                objects[2] = isDefault;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }


        #region AdviserAccountsList
        //These Functions are used for retrieving the Account details of all the customers for a particular Adviser

        /// <summary>
        /// Function to get all the MF Folio's for a particular Adviser
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="CurrentPage">Gives the current page in the grid (Parameters for paging)</param>
        /// <param name="sortOrder">Gives the sort order ASC or DSC (Parameters for paging)</param>
        /// <param name="count">Number of records per page (Parameters for paging)</param>
        /// <returns></returns>
        public List<CustomerAccountsVo> GetAdviserMFAccountList(int adviserId, int CurrentPage, string sortOrder, out int count, string NameFilter, string amcFilter, out Dictionary<string, string> genDictAMC)
        {
            CustomerAccountsVo customerAccountsVo;
            List<CustomerAccountsVo> customerAccountsList = new List<CustomerAccountsVo>();
            Database db;
            DbCommand getCustomerAccountsListCmd;
            DataSet dsCustomerAccounts;
            genDictAMC = new Dictionary<string, string>();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAccountsListCmd = db.GetStoredProcCommand("SP_GetAdviserMFAccounts");
                db.AddInParameter(getCustomerAccountsListCmd, "@A_AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(getCustomerAccountsListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                db.AddInParameter(getCustomerAccountsListCmd, "@SortOrder", DbType.String, sortOrder);
                if (NameFilter != "")
                    db.AddInParameter(getCustomerAccountsListCmd, "@nameFilter", DbType.String, NameFilter);
                else
                    db.AddInParameter(getCustomerAccountsListCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (amcFilter != "")
                    db.AddInParameter(getCustomerAccountsListCmd, "@amcFilter", DbType.String, amcFilter);
                else
                    db.AddInParameter(getCustomerAccountsListCmd, "@amcFilter", DbType.String, DBNull.Value);

                dsCustomerAccounts = db.ExecuteDataSet(getCustomerAccountsListCmd);
                if (dsCustomerAccounts.Tables[1] != null && dsCustomerAccounts.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsCustomerAccounts.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                if (dsCustomerAccounts.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCustomerAccounts.Tables[0].Rows)
                    {
                        customerAccountsVo = new CustomerAccountsVo();
                        customerAccountsVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        customerAccountsVo.AccountNum = dr["CMFA_FolioNum"].ToString();
                        customerAccountsVo.AMCName = dr["PA_AMCName"].ToString();
                        customerAccountsVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerAccountsVo.PortfolioId = int.Parse(dr["Cp_PortfolioId"].ToString());
                        customerAccountsVo.CustomerName = dr["C_FirstName"].ToString() + " " + dr["C_MiddleName"].ToString() + " " + dr["C_LastName"].ToString();


                        customerAccountsList.Add(customerAccountsVo);
                    }
                }
                else
                {
                    customerAccountsList = null;
                }
                if (dsCustomerAccounts.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCustomerAccounts.Tables[2].Rows)
                    {
                        genDictAMC.Add(dr["AMCName"].ToString(), dr["AMCName"].ToString());
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetAdviserMFAccountList()");


                object[] objects = new object[5];
                objects[0] = adviserId;
                objects[1] = CurrentPage;
                objects[2] = sortOrder;
                objects[3] = NameFilter;
                objects[4] = amcFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerAccountsList;
        }

        /// <summary>
        /// Function to get all the Equity Trade Accounts for a particular Adviser
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="CurrentPage">Gives the current page in the grid (Parameters for paging)</param>
        /// <param name="sortOrder">Gives the sort order ASC or DSC (Parameters for paging)</param>
        /// <param name="count">Number of records per page (Parameters for paging)</param>
        /// <returns></returns>
        public List<CustomerAccountsVo> GetAdviserEquityAccountList(int adviserId, int CurrentPage, string sortOrder, out int count)
        {
            CustomerAccountsVo customerAccountsVo;
            List<CustomerAccountsVo> customerAccountsList = new List<CustomerAccountsVo>();
            Database db;
            DbCommand getCustomerAccountsListCmd;
            DataSet dsCustomerAccounts;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAccountsListCmd = db.GetStoredProcCommand("SP_GetAdviserEQAccounts");
                db.AddInParameter(getCustomerAccountsListCmd, "@A_AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(getCustomerAccountsListCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                db.AddInParameter(getCustomerAccountsListCmd, "@SortOrder", DbType.String, sortOrder);

                dsCustomerAccounts = db.ExecuteDataSet(getCustomerAccountsListCmd);
                if (dsCustomerAccounts.Tables[1] != null && dsCustomerAccounts.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsCustomerAccounts.Tables[1].Rows[0][0].ToString());
                else
                    count = 0;
                foreach (DataRow dr in dsCustomerAccounts.Tables[0].Rows)
                {
                    customerAccountsVo = new CustomerAccountsVo();
                    customerAccountsVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                    customerAccountsVo.AccountNum = dr["CMFA_FolioNum"].ToString();
                    customerAccountsVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                    customerAccountsVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    customerAccountsVo.CustomerName = dr["C_FirstName"].ToString() + " " + dr["C_MiddleName"].ToString() + " " + dr["C_LastName"].ToString();


                    customerAccountsList.Add(customerAccountsVo);
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetAdviserEQAccountList()");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[0] = CurrentPage;
                objects[0] = sortOrder;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerAccountsList;
        }

        public DataTable GetTradeAccountNumbersByCustomer(int customerId)
        {

            Database db;
            DbCommand cmd;

            DataTable dt = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetTradeAccountNumbersByCustomer");
                db.AddInParameter(cmd, "@CustomerId", DbType.Int32, customerId);

                DataSet ds = db.ExecuteDataSet(cmd);
                dt = ds.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetTradeAccountNumbersByCustomer()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;



        }

        public bool DeleteInsuranceAccount(int accountId)
        {

            
               Database db;
               DbCommand chkAvailabilityCmd;
               bool bResult = false;

                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SP_DeleteLifeInsurance");

                db.AddInParameter(chkAvailabilityCmd, "@InsuranceAccount", DbType.String, accountId);
                
                db.ExecuteDataSet(chkAvailabilityCmd);
                bResult = true;
                return bResult;

               
                
            }

        #endregion

    }
}
