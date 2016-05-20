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

        public bool UpdateBankDetails(int customerid, string bankCode, int amcCode, string folioNo)
        {
            bool isUpdate = false;
            Database db;
            DbCommand UpdateBankDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateBankDetailsCmd = db.GetStoredProcCommand("SPROC_UpdateFolioLevelBankName");
                db.AddInParameter(UpdateBankDetailsCmd, "@C_CustomerId", DbType.Int32, customerid);
                db.AddInParameter(UpdateBankDetailsCmd, "@CB_CustBankCode", DbType.String, bankCode);
                db.AddInParameter(UpdateBankDetailsCmd, "@PA_AMCCode", DbType.String, amcCode);
                db.AddInParameter(UpdateBankDetailsCmd, "@CMFA_FolioNum", DbType.String, folioNo);

                if (db.ExecuteNonQuery(UpdateBankDetailsCmd) == -1)
                {
                    isUpdate = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdate;
        }
        public int CreateCustomerMFAccountBasic(CustomerAccountsVo customerAccountVo, int userId)
        {
             int accountId = 0;
            Database db;
            DbCommand createCustomerMFAccountCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerMFAccountCmd = db.GetStoredProcCommand("SPROC_CreateCustomerMFAccountBasic");
                db.AddInParameter(createCustomerMFAccountCmd, "@C_CustomerId", DbType.Int32, customerAccountVo.CustomerId);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_FolioNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(createCustomerMFAccountCmd, "@PA_AMCCode", DbType.Int32, customerAccountVo.AMCCode);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_AccountOpeningDate", DbType.Date, DateTime.Now);
                db.AddInParameter(createCustomerMFAccountCmd, "@UserId", DbType.Int32, userId);
               
               
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerMFAccountBasic()");
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
                db.AddInParameter(createCustomerMFAccountCmd, "@WCMV_LookupId_BankId", DbType.Int32, customerAccountVo.BankId);
                db.AddInParameter(createCustomerMFAccountCmd, "@WCMV_LookupId_AccType", DbType.Int32, customerAccountVo.BankAccTypeId);
                db.AddInParameter(createCustomerMFAccountCmd, "@PAG_AssetGroupCode", DbType.String, customerAccountVo.AssetClass);
                db.AddInParameter(createCustomerMFAccountCmd, "@PA_AMCCode", DbType.Int32, customerAccountVo.AMCCode);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_IsJointlyHeld", DbType.Int16, customerAccountVo.IsJointHolding);
          

                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_IsOnline", DbType.Int16, customerAccountVo.IsOnline);
                
                
                db.AddInParameter(createCustomerMFAccountCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerAccountVo.ModeOfHolding);
                if (customerAccountVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_AccountOpeningDate", DbType.DateTime, customerAccountVo.AccountOpeningDate);
                else
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_AccountOpeningDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createCustomerMFAccountCmd, "@CMFA_AccountId", DbType.Int32, 5000);
                db.AddOutParameter(createCustomerMFAccountCmd, "@IsFolioExist", DbType.Int32, 5000);
                if (customerAccountVo.BankId != 0)
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_CustBankAccId", DbType.Int32, customerAccountVo.BankId);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_CustBankAccId", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.Name))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_InvestorName", DbType.String, customerAccountVo.Name);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_InvestorName", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.PanNumber))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_PANNO", DbType.String, customerAccountVo.PanNumber);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_PANNO", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.BrokerCode))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_BROKERCODE", DbType.String, customerAccountVo.BrokerCode);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_BROKERCODE", DbType.String, DBNull.Value);
                }
                //if (!string.IsNullOrEmpty(customerAccountVo.TaxStaus))
                //{
                //    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_TAXSTATUS", DbType.String, customerAccountVo.TaxStaus);
                //}
                //else
                //{
                //    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_TAXSTATUS", DbType.String, DBNull.Value);
                //}
                //newly added 
                if (!string.IsNullOrEmpty(customerAccountVo.XCT_CustomerTypeCode))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@XCT_CustomerTypeCode", DbType.String, customerAccountVo.XCT_CustomerTypeCode);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@XCT_CustomerTypeCode", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(customerAccountVo.XCST_CustomerSubTypeCode))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@XCST_CustomerSubTypeCode", DbType.String, customerAccountVo.XCST_CustomerSubTypeCode);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@XCST_CustomerSubTypeCode", DbType.String, DBNull.Value);
                }



                if (!string.IsNullOrEmpty(customerAccountVo.CAddress1))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_ADDRESS1", DbType.String, customerAccountVo.CAddress1);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_ADDRESS1", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.CAddress2))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_ADDRESS2", DbType.String, customerAccountVo.CAddress2);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_ADDRESS2", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.CAddress3))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_ADDRESS3", DbType.String, customerAccountVo.CAddress3);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_ADDRESS3", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.CCity))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_CITY", DbType.String, customerAccountVo.CCity);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_CITY", DbType.String, DBNull.Value);
                }
                if (customerAccountVo.CPinCode != 0)
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_PINCODE", DbType.Int32, customerAccountVo.CPinCode);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_PINCODE", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.JointName1))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_JOINT_NAME1", DbType.String, customerAccountVo.JointName1);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_JOINT_NAME1", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.JointName2))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_JOINT_NAME2", DbType.String, customerAccountVo.JointName2);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_JOINT_NAME2", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.CPhoneOffice.ToString()))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_PHONE_OFF", DbType.Double, customerAccountVo.CPhoneOffice);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_PHONE_OFF", DbType.Double, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.CPhoneRes.ToString()))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_PHONE_RES", DbType.Double, customerAccountVo.CPhoneRes);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_PHONE_RES", DbType.Double, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.CEmail))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_EMAIL", DbType.String, customerAccountVo.CEmail);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_EMAIL", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.CMGCXP_BankCity))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_BankCity", DbType.String, customerAccountVo.CMGCXP_BankCity);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_BankCity", DbType.String, DBNull.Value);
                }
                if (customerAccountVo.CDOB != DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_DOB", DbType.DateTime, customerAccountVo.CDOB);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMGCXP_DOB", DbType.DateTime, DBNull.Value);
                }

                //added for Bank details 

                if (!string.IsNullOrEmpty(customerAccountVo.BankName))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@WERPBM_BankCode", DbType.String, customerAccountVo.BankName);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@WERPBM_BankCode", DbType.String, DBNull.Value);
                }

                if (customerAccountVo.CustomerId != 0)
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@C_CustomerId", DbType.Int32, customerAccountVo.CustomerId);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@C_CustomerId", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.AccountType))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@XBAT_BankAccountTypeCode", DbType.String, customerAccountVo.AccountType);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@XBAT_BankAccountTypeCode", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(customerAccountVo.BankAccountNum))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_AccountNum", DbType.String, customerAccountVo.BankAccountNum);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_AccountNum", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(customerAccountVo.BranchName))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchName", DbType.String, customerAccountVo.BranchName);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchName", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.BranchAdrLine1))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrLine1", DbType.String, customerAccountVo.BranchAdrLine1);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrLine1", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.BranchAdrLine2))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrLine2", DbType.String, customerAccountVo.BranchAdrLine2);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrLine2", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.BranchAdrLine3))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrLine3", DbType.String, customerAccountVo.BranchAdrLine3);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrLine3", DbType.String, DBNull.Value);
                }
                if (customerAccountVo.BranchAdrPinCode != 0)
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrPinCode", DbType.Double, customerAccountVo.BranchAdrPinCode);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrPinCode", DbType.Double, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.BranchAdrCity))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrCity", DbType.String, customerAccountVo.BranchAdrCity);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrCity", DbType.String, DBNull.Value);
                }
                if ((!string.IsNullOrEmpty(customerAccountVo.BranchAdrState)) && (customerAccountVo.BranchAdrState != "0"))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrState", DbType.String, customerAccountVo.BranchAdrState);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrState", DbType.String, DBNull.Value);
                }

                db.AddInParameter(createCustomerMFAccountCmd, "@CB_BranchAdrCountry", DbType.String, "India");


                if (customerAccountVo.Balance != 0)
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_Balance", DbType.Double, customerAccountVo.Balance);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_Balance", DbType.Double, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.MICR))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_MICR", DbType.String, customerAccountVo.MICR);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_MICR", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(customerAccountVo.IFSC))
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_IFSC", DbType.String, customerAccountVo.IFSC);
                }
                else
                {
                    db.AddInParameter(createCustomerMFAccountCmd, "@CB_IFSC", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(customerAccountVo.AssociateCode))
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_SubBrokerCode", DbType.String, customerAccountVo.AssociateCode);
                else
                    db.AddInParameter(createCustomerMFAccountCmd, "@CMFA_SubBrokerCode", DbType.String, DBNull.Value);
                if (customerAccountVo.AdviserAgentId != 0)
                    db.AddInParameter(createCustomerMFAccountCmd, "@AAC_AdviserAgentId", DbType.Int32, customerAccountVo.AdviserAgentId);
                else
                    db.AddInParameter(createCustomerMFAccountCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                db.AddInParameter(createCustomerMFAccountCmd, "@CB_CreatedBy", DbType.Int32, userId);


                db.AddInParameter(createCustomerMFAccountCmd, "@CB_ModifiedBy", DbType.Int32, userId);


                if (db.ExecuteNonQuery(createCustomerMFAccountCmd) > 0)
                {
                    accountId = int.Parse(db.GetParameterValue(createCustomerMFAccountCmd, "CMFA_AccountId").ToString());
                }
                else
                {
                    accountId = int.Parse(db.GetParameterValue(createCustomerMFAccountCmd, "IsFolioExist").ToString());
                }
               // DataSet ds = db.ExecuteDataSet(createCustomerMFAccountCmd);
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
                if (!string.IsNullOrEmpty(customerAccountVo.BankNameInExtFile))
                {
                    db.AddInParameter(createCustomerEQAccountCmd, "@WERPBM_BankCode", DbType.String, customerAccountVo.BankNameInExtFile);
                }
                else
                {
                    db.AddInParameter(createCustomerEQAccountCmd, "@WERPBM_BankCode", DbType.String, DBNull.Value);
                }
                if (customerAccountVo.BankId != 0)
                {
                    db.AddInParameter(createCustomerEQAccountCmd, "@CB_AccountNum", DbType.String, customerAccountVo.BankId);
                }
                else
                {
                    db.AddInParameter(createCustomerEQAccountCmd, "@CB_AccountNum", DbType.String, DBNull.Value);
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


        public DataSet GetISADetails(int IsaAccountId)
        {

            Database db;
            DbCommand getCustomerAssetAccountsCmd;
            DataSet dsGetISADetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssetAccountsCmd = db.GetStoredProcCommand("SPROC_GetISADetails");
                db.AddInParameter(getCustomerAssetAccountsCmd, "@CISAA_accountid", DbType.Int32, IsaAccountId);

                dsGetISADetails = db.ExecuteDataSet(getCustomerAssetAccountsCmd);
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

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetISADetails;
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


        //public bool CheckInsuranceNoAvailabilityOnAdd(string policyNumber, int adviserId)
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand chkAvailabilityCmd;
        //    int rowCount;
        //    DataSet ds;

        //    db = DatabaseFactory.CreateDatabase("wealtherp");
        //    chkAvailabilityCmd = db.GetStoredProcCommand("SPROC_CheckInsuranceNoAvailabilityOnAdd");

        //    db.AddInParameter(chkAvailabilityCmd, "@policyNumber", DbType.String, policyNumber);
        //    db.AddInParameter(chkAvailabilityCmd, "@adviserId", DbType.Int32, adviserId);

        //    ds = db.ExecuteDataSet(chkAvailabilityCmd);
        //    rowCount = Convert.ToInt32(ds.Tables[0].Rows[0]["column1"].ToString());
        //    if (rowCount > 0)
        //    {
        //        bResult = false;
        //    }
        //    else
        //    {
        //        bResult = true;
        //    }
        //    return bResult;
        //}

        public bool CheckGenInsuranceNoAvailabilityOnAdd(string policyNumber, int adviserId)
        {
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            chkAvailabilityCmd = db.GetStoredProcCommand("SPROC_CheckGenInsuranceNoAvailabilityOnAdd");

            db.AddInParameter(chkAvailabilityCmd, "@policyNumber", DbType.String, policyNumber);
            db.AddInParameter(chkAvailabilityCmd, "@adviserId", DbType.Int32, adviserId);

            ds = db.ExecuteDataSet(chkAvailabilityCmd);
            rowCount = Convert.ToInt32(ds.Tables[0].Rows[0]["column1"].ToString());
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

        public bool CheckInsuranceNoAvailabilityOnAdd(string policyNumber, int adviserId)
        {
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            chkAvailabilityCmd = db.GetStoredProcCommand("SPROC_CheckInsuranceNoAvailabilityOnAdd");

            db.AddInParameter(chkAvailabilityCmd, "@policyNumber", DbType.String, policyNumber);
            db.AddInParameter(chkAvailabilityCmd, "@adviserId", DbType.Int32, adviserId);

            ds = db.ExecuteDataSet(chkAvailabilityCmd);
            rowCount = Convert.ToInt32(ds.Tables[0].Rows[0]["column1"].ToString());
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

        //public bool CheckGenInsuranceNoAvailabilityOnAdd(string policyNumber, int adviserId)
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand chkAvailabilityCmd;
        //    int rowCount;
        //    DataSet ds;

        //    db = DatabaseFactory.CreateDatabase("wealtherp");
        //    chkAvailabilityCmd = db.GetStoredProcCommand("SPROC_CheckGenInsuranceNoAvailabilityOnAdd");

        //    db.AddInParameter(chkAvailabilityCmd, "@policyNumber", DbType.String, policyNumber);
        //    db.AddInParameter(chkAvailabilityCmd, "@adviserId", DbType.Int32, adviserId);

        //    ds = db.ExecuteDataSet(chkAvailabilityCmd);
        //    rowCount = ds.Tables[0].Rows.Count;
        //    if (rowCount > 0)
        //    {
        //        bResult = false;
        //    }
        //    else
        //    {
        //        bResult = true;
        //    }
        //    return bResult;
        //}


        public bool CheckTransactionExistanceOnHoldingAdd(int CBAccountNumber)
        {
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            chkAvailabilityCmd = db.GetStoredProcCommand("SPROC_CheckTransactionExistanceOnHoldingAdd");

            db.AddInParameter(chkAvailabilityCmd, "@CB_CustBankAccId", DbType.Int32, CBAccountNumber);

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

        public bool CheckAgentCodeAvailability(int adviserId, string agentCode)
        {
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int count = 0;;
            DataSet ds;
            try
                {
            db = DatabaseFactory.CreateDatabase("wealtherp");
            //Adding Data to the table 
            chkAvailabilityCmd = db.GetStoredProcCommand("SPROC_CodeduplicateChack");
            db.AddInParameter(chkAvailabilityCmd, "@A_AdviserId", DbType.Int32, adviserId);
            db.AddInParameter(chkAvailabilityCmd, "@agentCode", DbType.String, agentCode);
            db.AddOutParameter(chkAvailabilityCmd, "@count", DbType.Int32, 10);

            ds = db.ExecuteDataSet(chkAvailabilityCmd);
            //count = int.Parse(db.ExecuteScalar(cmdCodeduplicateCheck).ToString());
            Object objCount = db.GetParameterValue(chkAvailabilityCmd, "@count");
            if (objCount != DBNull.Value)
                count = int.Parse(db.GetParameterValue(chkAvailabilityCmd, "@count").ToString());
            else
                count = 0;
            if (count > 0)
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
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = agentCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

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

        public DataSet GetCustomerFamilyDetail(int customerId)
        {
            DataSet dsCustomerAssociates = null;
            DbCommand getCustomerAssociatesCmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssociatesCmd = db.GetStoredProcCommand("SP_GetCustomerFamilyDetail");
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerFamilyDetail()");


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
            DataSet dsCustomerAssociates = null;
            DbCommand getCustomerAssociatesCmd;
            DataTable dtCustomerAssociates;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssociatesCmd = db.GetStoredProcCommand("SP_GetCustomerAssociatesRel");
                db.AddInParameter(getCustomerAssociatesCmd, "@C_CustomerId", DbType.Int32, customerId);
                dsCustomerAssociates = db.ExecuteDataSet(getCustomerAssociatesCmd);
                if (dsCustomerAssociates.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCustomerAssociates.Tables[0].Rows)
                    {

                        //customerAccountVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        if (dr["C_DOB"].ToString() == null)
                        {
                            dr["C_DOB"] = DateTime.MinValue.ToString();
                        }
                       
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

        public DataSet GetCustomerAssociatedRelForCashAndSavings(int customerId, string strVisibility)
        {
            DataSet dsCustomerAssociates = null;
            DbCommand getCustomerAssociatesCmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssociatesCmd = db.GetStoredProcCommand("SP_GetCustomerAssociatesRelForCashAndSavings");
                db.AddInParameter(getCustomerAssociatesCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerAssociatesCmd, "@visiblity", DbType.String, strVisibility);
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
        public DataSet GetCustomerDematAccountAssociatesDetails(int customerId, string type)
        {
            DataSet dsCustomerDematAccountAssociates = null;
            DbCommand getCustomerDematAccountAssociatesCmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerDematAccountAssociatesCmd = db.GetStoredProcCommand("SPROC_GetCustomerDematAccountAssociates");
                db.AddInParameter(getCustomerDematAccountAssociatesCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerDematAccountAssociatesCmd, "@Type", DbType.String, type);
                dsCustomerDematAccountAssociates = db.ExecuteDataSet(getCustomerDematAccountAssociatesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetCustomerDematAccountAssociatesDetails()");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1]=type;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return dsCustomerDematAccountAssociates;
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

        public DataSet GetInsuranceAccountAssociation(int accountId)
        {
            DataSet ds = null;
            DbCommand getCustomerAssociatesCmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerAssociatesCmd = db.GetStoredProcCommand("SP_GetInsuranceAccountAssociation");
                db.AddInParameter(getCustomerAssociatesCmd, "@CIA_AccountId", DbType.Int32, accountId);
                ds = db.ExecuteDataSet(getCustomerAssociatesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerAccountDao.cs:GetInsuranceAccountAssociation()");

                object[] objects = new object[1];
                objects[0] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
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

        public bool CreatecustomerBankAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand CreatecustomerBankAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreatecustomerBankAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateCustomerBankAssociation");
                db.AddInParameter(CreatecustomerBankAccountAssociationCmd, "@CB_CustBankAccId", DbType.Int32, customerAccountAssociationVo.AccountId);
                db.AddInParameter(CreatecustomerBankAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(CreatecustomerBankAccountAssociationCmd, "@CCSAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(CreatecustomerBankAccountAssociationCmd, "@CCSAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(CreatecustomerBankAccountAssociationCmd, "@CCSAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(CreatecustomerBankAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreatecustomerBankAccountAssociation()");

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
            bool bResult = false;
            Database db;
            DbCommand createInsuranceAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createInsuranceAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateInsuranceAccountAssociation");
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIA_AccountId", DbType.Int32, customerAccountAssociationVo.AccountId);
                //db.AddInParameter(createInsuranceAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerAccountAssociationVo.AssociationId);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIAA_AssociationType", DbType.String, customerAccountAssociationVo.AssociationType);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@associationIds", DbType.String, associationIds);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createInsuranceAccountAssociationCmd, "@CIAA_ModifiedBy", DbType.Int32, userId);
               // if (customerAccountAssociationVo.Amount != null)
                //db.AddInParameter(createInsuranceAccountAssociationCmd, "@CINP_Amount", DbType.Decimal, customerAccountAssociationVo.Amount);
                //db.AddInParameter(createInsuranceAccountAssociationCmd, "@CINP_ModeOfPayment", DbType.String, customerAccountAssociationVo.ModeOfPayment);
                //db.AddInParameter(createInsuranceAccountAssociationCmd, "@CINP_PaymentInstrumentNumber", DbType.String, customerAccountAssociationVo.PaymentInstrumentNumber);
                //if (customerAccountAssociationVo.PaymentInstrumentDate != DateTime.MinValue)

                //db.AddInParameter(createInsuranceAccountAssociationCmd, "@CINP_PaymentInstrumentDate", DbType.DateTime, customerAccountAssociationVo.PaymentInstrumentDate);
                //db.AddInParameter(createInsuranceAccountAssociationCmd, "@CINP_BankName", DbType.Int32, customerAccountAssociationVo.BankName);
                //db.AddInParameter(createInsuranceAccountAssociationCmd, "@CINP_BankBranch", DbType.String, customerAccountAssociationVo.BankBranch);
               


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
        public DataSet GetBankName(int customerId, string accountNo)
        {
            Database db;
            DbCommand getBankNamecmd;
            DataSet dsBankName;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBankNamecmd = db.GetStoredProcCommand("SP_GetBankName");
                db.AddInParameter(getBankNamecmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(getBankNamecmd, "@accountNo", DbType.String, accountNo);
                dsBankName = db.ExecuteDataSet(getBankNamecmd);
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
            return dsBankName;
        }
        public DataSet GetAccountNumber(int customerId, string categoryType)
        {
            Database db;
            DbCommand getBankAccountNocmd;
            DataSet dsAccountNo;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBankAccountNocmd = db.GetStoredProcCommand("SP_GetAccountNumber");
                db.AddInParameter(getBankAccountNocmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(getBankAccountNocmd, "@accountType", DbType.String, categoryType);
                //db.AddInParameter(getBankAccountNocmd, "@WERPBM_BankCode", DbType.String, bankcode);
                dsAccountNo = db.ExecuteDataSet(getBankAccountNocmd);
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
                objects[1] = categoryType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAccountNo;
        }

        #endregion

        public bool CheckPANNoAvailability(string PanNumber, string BranchId, int adviserId)
        {

            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SP_CheckPanNumberAvailability");

                db.AddInParameter(chkAvailabilityCmd, "@PanNumber", DbType.String, PanNumber);
                db.AddInParameter(chkAvailabilityCmd, "@BranchId", DbType.String, BranchId);
                db.AddInParameter(chkAvailabilityCmd, "@adviserId", DbType.Int32, adviserId);

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

            return bResult;
        }

        public bool UpdateISAAccountAssociation(CustomerISAAccountsVo customerISAAccountAssociationVo, string associationIds)
        {
            bool bResult = false;
            Database db;
            DbCommand createISAAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createISAAccountAssociationCmd = db.GetStoredProcCommand("SPROC_UpdateISAAccountAssociation");
                db.AddInParameter(createISAAccountAssociationCmd, "@ISA_Accountid", DbType.Int32, customerISAAccountAssociationVo.ISAAccountId);
                db.AddInParameter(createISAAccountAssociationCmd, "@CA_AssociationId", DbType.String, associationIds);
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAAA_Associationtype", DbType.String, customerISAAccountAssociationVo.AssociationTypeCode);
                if (db.ExecuteNonQuery(createISAAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateISAAccountAssociation()");

                object[] objects = new object[2];
                objects[0] = customerISAAccountAssociationVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool CreateISAAccountAssociation(CustomerISAAccountsVo customerISAAccountAssociationVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createISAAccountAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createISAAccountAssociationCmd = db.GetStoredProcCommand("SP_CreateISAAccountAssociation");
                db.AddInParameter(createISAAccountAssociationCmd, "@ISA_Accountid", DbType.Int32, customerISAAccountAssociationVo.ISAAccountId);
                db.AddInParameter(createISAAccountAssociationCmd, "@CA_AssociationId", DbType.Int32, customerISAAccountAssociationVo.AssociationId);
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAAA_Associationtype", DbType.String, customerISAAccountAssociationVo.AssociationTypeCode);
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAAA_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createISAAccountAssociationCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateISAAccountAssociation()");

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

        public int UpdateCustomerISAAccount(CustomerISAAccountsVo customerISAAccountVo)
        {

            Database db;
            DbCommand createISAAccountAssociationCmd;
            int customerISAAccountId = 0;
            int bResult = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createISAAccountAssociationCmd = db.GetStoredProcCommand("SPROC_UpdateISADetails");
                db.AddInParameter(createISAAccountAssociationCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerISAAccountVo.ModeOfHolding);
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAA_Isjointlyheld", DbType.Int16, Convert.ToInt16(customerISAAccountVo.IsJointHolding));
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAA_IsPOAOperated", DbType.Int16, Convert.ToInt16(customerISAAccountVo.IsOperatedByPOA));
                db.AddInParameter(createISAAccountAssociationCmd, "@ISA_Accountid", DbType.Int16, Convert.ToInt16(customerISAAccountVo.ISAAccountId));

                if (db.ExecuteNonQuery(createISAAccountAssociationCmd) != 0)
                    bResult = 1;



            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerISAAccount()");

                object[] objects = new object[2];
                objects[0] = customerISAAccountVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public int CreateCustomerISAAccount(CustomerISAAccountsVo customerISAAccountVo, int customerId, int userId, int requestId)
        {

            Database db;
            DbCommand createISAAccountAssociationCmd;
            int customerISAAccountId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createISAAccountAssociationCmd = db.GetStoredProcCommand("SPROC_CreateCustomerISAAccount");
                db.AddInParameter(createISAAccountAssociationCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(createISAAccountAssociationCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerISAAccountVo.ModeOfHolding);
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAA_Isjointlyheld", DbType.Int16, Convert.ToInt16(customerISAAccountVo.IsJointHolding));
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAA_IsPOAOperated", DbType.Int16, Convert.ToInt16(customerISAAccountVo.IsOperatedByPOA));
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createISAAccountAssociationCmd, "@CISAA_ModifedBy", DbType.Int32, userId);
                db.AddOutParameter(createISAAccountAssociationCmd, "@CISAA_AccountId", DbType.Int32, 1000000);
                db.AddInParameter(createISAAccountAssociationCmd, "@RequestId", DbType.Int32, requestId);
                if (db.ExecuteNonQuery(createISAAccountAssociationCmd) != 0)
                    customerISAAccountId = int.Parse(db.GetParameterValue(createISAAccountAssociationCmd, "@CISAA_AccountId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreateCustomerISAAccount()");

                object[] objects = new object[2];
                objects[0] = customerISAAccountVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerISAAccountId;
        }
        public DataTable GetISAListForFolioMapping(int CustomerId)
        {
            DataSet dsgetISAList;
            DataTable dtgetISAList;
            Database db;
            DbCommand GetISAListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetISAListCmd = db.GetStoredProcCommand("SPROC_GetISAListForFolioMapping");
                db.AddInParameter(GetISAListCmd, "@CustomerId", DbType.Int32, CustomerId);
                dsgetISAList = db.ExecuteDataSet(GetISAListCmd);
                dtgetISAList = dsgetISAList.Tables[0];
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
            DataTable dtBindAttachedFolioGrid;
            DataSet dsBindAttachedFolioGrid;
            Database db;
            DbCommand BindAttachedFolioGridCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                BindAttachedFolioGridCmd = db.GetStoredProcCommand("SPROC_GetAttachedfolioForISA");
                db.AddInParameter(BindAttachedFolioGridCmd, "@accountId", DbType.Int32, AccountId);
                dsBindAttachedFolioGrid = db.ExecuteDataSet(BindAttachedFolioGridCmd);
                dtBindAttachedFolioGrid = dsBindAttachedFolioGrid.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtBindAttachedFolioGrid;
        }
        public DataTable GetAvailableFolioList(int CustomerId, int AccountId)
        {
            DataTable dtGetAvailableFolioList;
            DataSet dsGetAvailableFolioList;
            Database db;
            DbCommand GetAvailableFolioListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAvailableFolioListCmd = db.GetStoredProcCommand("SPROC_GetAvailablefolioForISA");
                db.AddInParameter(GetAvailableFolioListCmd, "@accountId", DbType.Int32, AccountId);
                db.AddInParameter(GetAvailableFolioListCmd, "@CustomerId", DbType.Int32, CustomerId);
                dsGetAvailableFolioList = db.ExecuteDataSet(GetAvailableFolioListCmd);
                dtGetAvailableFolioList = dsGetAvailableFolioList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAvailableFolioList;
        }
        public bool UpdateMFAccount(int cmfaAccountId, int isaAccountId, int AMCCode, int PortfolioId, string ModeOfHoldingCode, int IsJointlyHeld, out bool result)
        {
            Database db;
            DbCommand updateMFAccountCmd;
            int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFAccountCmd = db.GetStoredProcCommand("SPROC_UpdateMutualFundAccount");
                db.AddInParameter(updateMFAccountCmd, "@cmfaAccountId", DbType.Int32, cmfaAccountId);
                db.AddInParameter(updateMFAccountCmd, "@isaAccount", DbType.Int32, isaAccountId);
                db.AddInParameter(updateMFAccountCmd, "@amcCode", DbType.Int32, AMCCode);
                db.AddInParameter(updateMFAccountCmd, "@PortfolioId", DbType.Int32, PortfolioId);
                db.AddInParameter(updateMFAccountCmd, "@ModeOfHoldings", DbType.String, ModeOfHoldingCode);
                db.AddInParameter(updateMFAccountCmd, "@IsJointlyHeld", DbType.Int32, IsJointlyHeld);
                db.AddOutParameter(updateMFAccountCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(updateMFAccountCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(updateMFAccountCmd, "@IsSuccess").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            if (affectedRecords > 0)
                return result = true;
            else
                return result = false;
        }
        public DataSet GetCustomerISAAssociatedRel(int IsaAccountId)
        {
            DataSet dsGetISAAssociatedRel;
            Database db;
            DbCommand GetISAAssociatedRelCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetISAAssociatedRelCmd = db.GetStoredProcCommand("SPROC_GetCustomerISAAssociatedRel");
                db.AddInParameter(GetISAAssociatedRelCmd, "@IsaAccountId", DbType.Int32, IsaAccountId);
                dsGetISAAssociatedRel = db.ExecuteDataSet(GetISAAssociatedRelCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetISAAssociatedRel;
        }
        public int GetRequestNo(int customerId)
        {
            Database db;
            DbCommand getRequestNoCmd;
            int requestNo = 0;
            DataSet dsRequestNo;
            DataTable dtRequestNo;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRequestNoCmd = db.GetStoredProcCommand("SPROC_GetRequestNumber");
                db.AddInParameter(getRequestNoCmd, "@CustomerId", DbType.Int32, customerId);
                dsRequestNo = db.ExecuteDataSet(getRequestNoCmd);
                dtRequestNo = dsRequestNo.Tables[0];
                if (dtRequestNo.Rows.Count > 0)
                    requestNo = int.Parse(dtRequestNo.Rows[0]["AISAQ_RequestQueueid"].ToString());
                //if (db.ExecuteNonQuery(getRequestNoCmd) != 0)
                //    requestNo = int.Parse(db.GetParameterValue(getRequestNoCmd, "@CISAA_AccountId").ToString());
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
            Database db;
            DbCommand deleteISAaccountCmd;
            bool IsDelete = false; ;
            try
            {


                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteISAaccountCmd = db.GetStoredProcCommand("SPROC_DeleteCustomerISAAccount");
                db.AddInParameter(deleteISAaccountCmd, "@ISAAccount", DbType.Int32, ISAAccounts);
                if (db.ExecuteNonQuery(deleteISAaccountCmd) != 0)
                    IsDelete = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return IsDelete;
        }
        public DataSet GetAccountType()
        {
            Database db;
            DbCommand GetAccountTypeCmd;
            DataSet dsGetAccountType;

            try
            {


                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAccountTypeCmd = db.GetStoredProcCommand("SP_GetCustomerAccountTypeForBankDetails");
                dsGetAccountType = db.ExecuteDataSet(GetAccountTypeCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetAccountType;
        }
        public DataSet GetEQAccountNumber(int customerId, string bankcode)
        {
            Database db;
            DbCommand getBankAccountNocmd;
            DataSet dsAccountNo;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBankAccountNocmd = db.GetStoredProcCommand("SP_GetEQAccountNumber");
                db.AddInParameter(getBankAccountNocmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(getBankAccountNocmd, "@WERPBM_BankCode", DbType.String, bankcode);
                dsAccountNo = db.ExecuteDataSet(getBankAccountNocmd);
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
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAccountNo;
        }
        public bool CreatecustomerBankTransaction(CustomerAccountsVo customerAccountVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand CreatecustomerBanktransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreatecustomerBanktransactionCmd = db.GetStoredProcCommand("SP_CreateCustomerBankTransaction");
                db.AddInParameter(CreatecustomerBanktransactionCmd, "@CB_CustBankAccId", DbType.Int32, customerAccountVo.AccountId);
                if (customerAccountVo.ExternalTransactionId != null)
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_ExternalTransactionId", DbType.String, customerAccountVo.ExternalTransactionId);
                else
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_ExternalTransactionId", DbType.String, DBNull.Value);
                if (customerAccountVo.Transactiondate != null)
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_Transactiondate", DbType.DateTime, customerAccountVo.Transactiondate);
                else
                    customerAccountVo.Transactiondate = DateTime.MinValue;

                if (customerAccountVo.CCST_Desc != null)
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_Desc", DbType.String, customerAccountVo.CCST_Desc);
                else
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_Desc", DbType.String, DBNull.Value);
                // db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_Desc", DbType.String, customerAccountVo.CCST_Desc);
                db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_IsWithdrwal", DbType.Int32, customerAccountVo.IsWithdrwal);
                if (!string.IsNullOrEmpty("@WERP_CFCCode".ToString()))
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@WERP_CFCCode", DbType.String, customerAccountVo.CFCCategoryCode);
                else
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@WERP_CFCCode", DbType.String, DBNull.Value);
                if (customerAccountVo.ChequeNo != null)
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_ChequeNo", DbType.String, customerAccountVo.ChequeNo);
                else
                    db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_ChequeNo", DbType.String, DBNull.Value);
                db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_Amount", DbType.Double, customerAccountVo.Amount);
                db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_AvailableBalance", DbType.Double, customerAccountVo.AvailableBalance);
                db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(CreatecustomerBanktransactionCmd, "@CCST_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(CreatecustomerBanktransactionCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:CreatecustomerBankTransaction()");

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
            Database db;
            string AccountNum;
            DbCommand InsertholdingAmountCustomerBankCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                InsertholdingAmountCustomerBankCmd = db.GetStoredProcCommand("SP_InsertholdingAmountCustomerBank");
                db.AddInParameter(InsertholdingAmountCustomerBankCmd, "@C_CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(InsertholdingAmountCustomerBankCmd, "@CB_CustBankAccId", DbType.String, customerAccountVo.AccountId);
                //db.AddInParameter(InsertholdingAmountCustomerBankCmd, "@CB_AccountNum", DbType.String, customerAccountVo.AccountNum);
                db.AddInParameter(InsertholdingAmountCustomerBankCmd, "@CB_HoldingAmount", DbType.Double, customerAccountVo.Amount);
                if (db.ExecuteNonQuery(InsertholdingAmountCustomerBankCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerAccountDao.cs:InsertholdingAmountCustomerBank()");

                object[] objects = new object[2];
                objects[0] = customerAccountVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public List<CustomerAccountsVo> GetCustomerBankTransaction(int CustBankAccIds, int customerId)
        {

            List<CustomerAccountsVo> accountList = null;
            CustomerAccountsVo customerAccountsVo;
            Database db;
            DataSet getCustomerBankTransactionDs;
            DbCommand getCustomerBankTransactionCmd;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                accountList = new List<CustomerAccountsVo>();
                getCustomerBankTransactionCmd = db.GetStoredProcCommand("SP_GetCustomerBankTransaction");

                db.AddInParameter(getCustomerBankTransactionCmd, "@CB_CustBankAccId", DbType.Int32, CustBankAccIds);
                db.AddInParameter(getCustomerBankTransactionCmd, "@CustomerId", DbType.Int32, customerId);
                getCustomerBankTransactionDs = db.ExecuteDataSet(getCustomerBankTransactionCmd);
                if (getCustomerBankTransactionDs.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in getCustomerBankTransactionDs.Tables[0].Rows)
                    {
                        customerAccountsVo = new CustomerAccountsVo();
                        customerAccountsVo.BankName = dr["WERPBM_BankCode"].ToString();
                        customerAccountsVo.WERPBMBankName = dr["WERPBDTM_BankName"].ToString();
                        customerAccountsVo.BankAccountNum = dr["CB_AccountNum"].ToString();
                        if (!string.IsNullOrEmpty(dr["CCST_TransactionId"].ToString()))
                            customerAccountsVo.TransactionId = Convert.ToInt32(dr["CCST_TransactionId"].ToString());
                        else
                            customerAccountsVo.TransactionId = Convert.ToInt32(dr["CCST_TransactionId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CCST_ExternalTransactionId"].ToString()))
                            customerAccountsVo.ExternalTransactionId = dr["CCST_ExternalTransactionId"].ToString();
                        else
                            customerAccountsVo.ExternalTransactionId = null;
                        if (!string.IsNullOrEmpty(dr["CCST_Transactiondate"].ToString()))
                            customerAccountsVo.Transactiondate = DateTime.Parse(dr["CCST_Transactiondate"].ToString());
                        else
                            customerAccountsVo.Transactiondate = DateTime.MinValue;
                        if (!string.IsNullOrEmpty(dr["CCST_Desc"].ToString()))
                            customerAccountsVo.CCST_Desc = dr["CCST_Desc"].ToString();
                        else
                            customerAccountsVo.CCST_Desc = null;
                        if (!string.IsNullOrEmpty(dr["CCST_ChequeNo"].ToString()))
                            customerAccountsVo.ChequeNo = dr["CCST_ChequeNo"].ToString();
                        else
                            customerAccountsVo.ChequeNo = null;

                        if (!string.IsNullOrEmpty(dr["CCST_IsWithdrwal"].ToString()))
                            if (dr["CCST_IsWithdrwal"].ToString() == "CR")
                                customerAccountsVo.IsWithdrwal = 0;
                            else
                                customerAccountsVo.IsWithdrwal = 1;
                        else
                            customerAccountsVo.IsWithdrwal = 0;

                        if (!string.IsNullOrEmpty(dr["WERP_CFCCode"].ToString()))
                            customerAccountsVo.CFCCategoryCode = (dr["WERP_CFCCode"].ToString());
                        else
                            customerAccountsVo.CFCCategoryCode = null;
                        customerAccountsVo.CFCCategoryName = dr["WERP_CFCName"].ToString();
                        //if (!string.IsNullOrEmpty(dr["WERP_CFCName"].ToString()))
                        //    customerAccountsVo.CFCCategory = (dr["WERP_CFCName"].ToString());
                        //else
                        //    customerAccountsVo.CFCCategory = "N/A";
                        if (!string.IsNullOrEmpty(dr["CCST_Amount"].ToString()))
                            customerAccountsVo.Amount = double.Parse(dr["CCST_Amount"].ToString());
                        else
                            customerAccountsVo.Amount = 0.00;
                        accountList.Add(customerAccountsVo);

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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetCustomerBankTransaction()");


                object[] objects = new object[1];
                objects[0] = CustBankAccIds;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return accountList;

        }
        public bool UpdateCustomerBankTransaction(CustomerAccountsVo customerAccountVo, int TransactionId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateCustomerBankTransactionCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCustomerBankTransactionCmd = db.GetStoredProcCommand("SP_UpdateCustomerBankTransaction");
                db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_TransactionId", DbType.Int32, TransactionId);
                db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_ExternalTransactionId", DbType.String, customerAccountVo.ExternalTransactionId);
                if (customerAccountVo.Transactiondate != null)
                    db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_Transactiondate", DbType.DateTime, customerAccountVo.Transactiondate);
                else
                    customerAccountVo.Transactiondate = DateTime.MinValue;
                db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_Desc", DbType.String, customerAccountVo.CCST_Desc);
                db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_IsWithdrwal", DbType.Int32, customerAccountVo.IsWithdrwal);
                db.AddInParameter(updateCustomerBankTransactionCmd, "@WERP_CFCCode", DbType.String, customerAccountVo.CFCCategoryCode);
                db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_ChequeNo", DbType.String, customerAccountVo.ChequeNo);
                db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_Amount", DbType.Double, customerAccountVo.Amount);
                db.AddInParameter(updateCustomerBankTransactionCmd, "@CCST_AvailableBalance", DbType.Double, customerAccountVo.AvailableBalance);
                if (db.ExecuteNonQuery(updateCustomerBankTransactionCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:UpdateCustomerBankTransaction()");


                object[] objects = new object[2];
                objects[0] = customerAccountVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteCustomerBankTransaction(int TransactionId)
        {
            bool bResult = false;
            Database db;
            DbCommand createCustomerBankCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerBankCmd = db.GetStoredProcCommand("SP_DeleteCustomerBankTransaction");
                db.AddInParameter(createCustomerBankCmd, "@CCST_TransactionId", DbType.Int32, TransactionId);
                db.ExecuteNonQuery(createCustomerBankCmd);

                bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                bResult = false;
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:DeleteCustomerBankTransaction()");

                object[] objects = new object[1];
                objects[0] = TransactionId;

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
        public DataTable GetCashFlowCategory()
        {
            //string logoPath = "";
            Database db;
            DbCommand getCashflowCategoryCmd;
            DataSet getCashflowCategoryDs;
            // DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCashflowCategoryCmd = db.GetStoredProcCommand("SP_GetCashFlowCategory");
                getCashflowCategoryDs = db.ExecuteDataSet(getCashflowCategoryCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return getCashflowCategoryDs.Tables[0];
        }
        public DataSet GetBankAccountNumber(int customerId)
        {
            DbCommand getBankAccountNoCmd;
            DataSet ds;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBankAccountNoCmd = db.GetStoredProcCommand("SPROC_GetBankAccountNumber");
                db.AddInParameter(getBankAccountNoCmd, "@CustomerId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(getBankAccountNoCmd);
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
            Database db;
            DbCommand GetHoldingBalanceCmd;
            double Amount = 0.0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetHoldingBalanceCmd = db.GetStoredProcCommand("SP_GetHoldingAmount");
                db.AddInParameter(GetHoldingBalanceCmd, "@CB_CustBankAccId", DbType.Int32, CB_CustBankAccId);
                db.AddOutParameter(GetHoldingBalanceCmd, "@CB_HoldingAmount", DbType.Double, 100000);
                if (db.ExecuteNonQuery(GetHoldingBalanceCmd) != 0)
                    Amount = double.Parse(db.GetParameterValue(GetHoldingBalanceCmd, "CB_HoldingAmount").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return Amount;
        }

        public bool CheckPANNoAvailability(string PanNumber, int adviserId)
        {
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SPROC_CheckPanNumberAvailabilityForAssociates");
                db.AddInParameter(chkAvailabilityCmd, "@PanNumber", DbType.String, PanNumber);
                db.AddInParameter(chkAvailabilityCmd, "@adviserId", DbType.Int32, adviserId);
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

            return bResult;
        }

        public bool CheckFolioDuplicate(int adviserId, string folioNumber)
        {
            bool bResult = false;
            Database db;
            DbCommand chkFolioDuplicateCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkFolioDuplicateCmd = db.GetStoredProcCommand("SPROC_CheckFolioDuplicate");
                db.AddInParameter(chkFolioDuplicateCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(chkFolioDuplicateCmd, "@FolioNumber", DbType.String, folioNumber);
                db.AddOutParameter(chkFolioDuplicateCmd, "@IsFolioExists", DbType.Int16, 100);

                if (db.ExecuteNonQuery(chkFolioDuplicateCmd) != 0)

                    bResult = ((int.Parse(db.GetParameterValue(chkFolioDuplicateCmd, "IsFolioExists").ToString()) == 1) ? true : false);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:CheckFolioDuplicate(int customerId, string folioNumber)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = folioNumber;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
           
            return bResult;
        }
    }
}
