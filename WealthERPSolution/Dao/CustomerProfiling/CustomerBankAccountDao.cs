using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data.Common;
using VoCustomerProfiling;
using System.Collections.Specialized;
namespace DaoCustomerProfiling
{
    public class CustomerBankAccountDao
    {
        public int CreateCustomerBankAccount(CustomerBankAccountVo customerBankAccountVo, int customerId, int userId)
        {
            int accountId = 0;
          //  bool bResult = false;
            Database db;
            DbCommand createCustomerBankCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerBankCmd = db.GetStoredProcCommand("SP_CreateCustomerBankAccount");
                db.AddInParameter(createCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(createCustomerBankCmd, "@WERPBM_BankCode", DbType.String, customerBankAccountVo.BankName);
                db.AddInParameter(createCustomerBankCmd, "@CP_PortfolioId", DbType.Int32, customerBankAccountVo.PortfolioId);
                if (!string.IsNullOrEmpty("PAIC_AssetInstrumentCategoryCode".ToString()))
                    db.AddInParameter(createCustomerBankCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerBankAccountVo.AccountType);
                else
                    db.AddInParameter(createCustomerBankCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, DBNull.Value);
                db.AddInParameter(createCustomerBankCmd, "@CB_AccountNum", DbType.String, customerBankAccountVo.BankAccountNum);
                db.AddInParameter(createCustomerBankCmd, "@CB_IsHeldJointly", DbType.Int32, customerBankAccountVo.IsJointHolding);
                db.AddInParameter(createCustomerBankCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerBankAccountVo.ModeOfOperation);
                db.AddInParameter(createCustomerBankCmd, "@CB_BankCity", DbType.String, customerBankAccountVo.BankCity);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchName", DbType.String, customerBankAccountVo.BranchName);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrLine1", DbType.String, customerBankAccountVo.BranchAdrLine1);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrLine2", DbType.String, customerBankAccountVo.BranchAdrLine2);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrLine3", DbType.String, customerBankAccountVo.BranchAdrLine3);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrPinCode", DbType.Int32, customerBankAccountVo.BranchAdrPinCode);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrCity", DbType.String, customerBankAccountVo.BranchAdrCity);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrState", DbType.String, customerBankAccountVo.BranchAdrState);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrCountry", DbType.String, customerBankAccountVo.BranchAdrCountry);
                db.AddInParameter(createCustomerBankCmd, "@CB_Balance", DbType.Decimal, customerBankAccountVo.Balance);
                 if(!string.IsNullOrEmpty(customerBankAccountVo.MICR.ToString()))                
                db.AddInParameter(createCustomerBankCmd, "@CB_MICR", DbType.Int64, customerBankAccountVo.MICR);
                 else
                     db.AddInParameter(createCustomerBankCmd, "@CB_MICR", DbType.Int64, 0);
                 if (!string.IsNullOrEmpty(customerBankAccountVo.RTGSCode.ToString()))
                     db.AddInParameter(createCustomerBankCmd, "@CB_RTGS", DbType.String, customerBankAccountVo.RTGSCode);
                 else
                     db.AddInParameter(createCustomerBankCmd, "@CB_RTGS", DbType.String, DBNull.Value);
                 if (!string.IsNullOrEmpty(customerBankAccountVo.NeftCode.ToString()))
                     db.AddInParameter(createCustomerBankCmd, "@CB_NEFT", DbType.String, customerBankAccountVo.NeftCode);
                 else
                     db.AddInParameter(createCustomerBankCmd, "@CB_NEFT", DbType.String, 0);
                 if (!string.IsNullOrEmpty(customerBankAccountVo.IFSC.ToString()))
                     db.AddInParameter(createCustomerBankCmd, "@CB_IFSC", DbType.String, customerBankAccountVo.IFSC);
                 else
                     db.AddInParameter(createCustomerBankCmd, "@CB_IFSC", DbType.String, 0);
                db.AddInParameter(createCustomerBankCmd, "@CB_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerBankCmd, "@CB_ModifiedBy", DbType.Int32, userId);

                db.AddInParameter(createCustomerBankCmd, "@WCMV_LookupId_BankId", DbType.Int32, customerBankAccountVo.BankId);
                db.AddInParameter(createCustomerBankCmd, "@WCMV_LookupId_AccType", DbType.Int32, customerBankAccountVo.BankAccTypeId);

                if (customerBankAccountVo.BranchAddCityId != 0)
                    db.AddInParameter(createCustomerBankCmd, "@WCMV_Lookup_BranchAddCityId", DbType.Int32, customerBankAccountVo.BranchAddCityId);

                if (customerBankAccountVo.BranchAddStateId != 0)
                    db.AddInParameter(createCustomerBankCmd, "@WCMV_Lookup_BranchAddStateId", DbType.Int32, customerBankAccountVo.BranchAddStateId);

                if (customerBankAccountVo.BranchAddCountryId != 0)
                    db.AddInParameter(createCustomerBankCmd, "@WCMV_Lookup_BranchAddCountryId", DbType.Int32, customerBankAccountVo.BranchAddCountryId);

                db.AddOutParameter(createCustomerBankCmd, "@CB_CustBankAccId", DbType.Int32, 10000);
                db.AddInParameter(createCustomerBankCmd, "@CB_IsCurrent", DbType.Int16, customerBankAccountVo.IsCurrent);



                // db.ExecuteNonQuery(createCustomerBankCmd);

                if (db.ExecuteNonQuery(createCustomerBankCmd) != 0)
                    accountId = int.Parse(db.GetParameterValue(createCustomerBankCmd, "CB_CustBankAccId").ToString());
                // bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:CreateCustomerBankAccount()");


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

        public DataSet GetCustomerIndividualBankDetails(int customerId)
        {
            Database db;
            DataSet getCustomerBankDs;
            DbCommand getCustomerBankCmd;
            //string query = "select * from CustomerBankAccount where C_CustomerId=" + customerId.ToString() + "and CB_CustBankAccId=" + customerBankAccId.ToString();
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");

                getCustomerBankCmd = db.GetStoredProcCommand("SP_GetCustomerBankAccounts");
                db.AddInParameter(getCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
                getCustomerBankDs = db.ExecuteDataSet(getCustomerBankCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetCustomerBankAccounts()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getCustomerBankDs;
        }

        public List<CustomerBankAccountVo> GetCustomerBankAccounts(int customerId)
        {

            List<CustomerBankAccountVo> accountList = null;
            CustomerBankAccountVo customerBankAccountVo;
            Database db;
            DataSet getCustomerBankDs;
            DbCommand getCustomerBankCmd;
            //string query = "select * from CustomerBankAccount where C_CustomerId=" + customerId.ToString() + "and CB_CustBankAccId=" + customerBankAccId.ToString();
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                accountList = new List<CustomerBankAccountVo>();
                getCustomerBankCmd = db.GetStoredProcCommand("SP_GetCustomerBankAccounts");
                db.AddInParameter(getCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
                //db.AddInParameter(getCustomerBankCmd, "@CB_CustBankAccId", DbType.Int32, customerBankAccId);
                getCustomerBankDs = db.ExecuteDataSet(getCustomerBankCmd);
                if (getCustomerBankDs.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in getCustomerBankDs.Tables[0].Rows)
                    {
                        customerBankAccountVo = new CustomerBankAccountVo();
                        customerBankAccountVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                        customerBankAccountVo.WERPBMBankName = dr["WCMV_BankName"].ToString();
                        customerBankAccountVo.AccountType = dr["WCMV_BankAccountType"].ToString();
                        customerBankAccountVo.BankAccountNum = dr["CB_AccountNum"].ToString();
                        customerBankAccountVo.ModeOfOperation = dr["XMOH_ModeOfHolding"].ToString().Trim();
                        customerBankAccountVo.BranchName = dr["CB_BranchName"].ToString();
                        customerBankAccountVo.BranchAdrLine1 = dr["CB_BranchAdrLine1"].ToString();
                        customerBankAccountVo.BranchAdrLine2 = dr["CB_BranchAdrLine2"].ToString();
                        customerBankAccountVo.BranchAdrLine3 = dr["CB_BranchAdrLine3"].ToString();
                        customerBankAccountVo.RTGSCode = dr["CB_RTGS"].ToString();
                        customerBankAccountVo.NeftCode = dr["CB_NEFT"].ToString();
                        customerBankAccountVo.MICR = dr["CB_MICR"].ToString();
                        if (!string.IsNullOrEmpty(dr["CB_BranchAdrPinCode"].ToString()))
                            customerBankAccountVo.BranchAdrPinCode = int.Parse(dr["CB_BranchAdrPinCode"].ToString());
                        //customerBankAccountVo.BranchAdrCity = dr["CB_BranchAdrCity"].ToString();
                        //customerBankAccountVo.BranchAdrState = dr["CB_BranchAdrState"].ToString();
                        //customerBankAccountVo.BranchAdrCountry = dr["CB_BranchAdrCountry"].ToString();
                        customerBankAccountVo.ModeOfOperationCode = dr["XMOH_ModeOfHoldingCode"].ToString().Trim();
                        //customerBankAccountVo.AccountTypeCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        //customerBankAccountVo.BankName = dr["WERPBM_BankCode"].ToString();

                        if (dr["CB_Balance"].ToString() != "")
                            if (!string.IsNullOrEmpty(dr["CB_Balance"].ToString()))
                                customerBankAccountVo.Balance = float.Parse(dr["CB_Balance"].ToString());
                        //if (!string.IsNullOrEmpty(dr["CB_MICR"].ToString()))
                        //    customerBankAccountVo.MICR = long.Parse(dr["CB_MICR"].ToString());
                        customerBankAccountVo.MICR = dr["CB_MICR"].ToString();
                        customerBankAccountVo.IFSC = dr["CB_IFSC"].ToString();

                        if (!string.IsNullOrEmpty(dr["WCMV_LookupId_AccType"].ToString()))
                        customerBankAccountVo.BankAccTypeId = int.Parse(dr["WCMV_LookupId_AccType"].ToString());
                        if (!string.IsNullOrEmpty(dr["WCMV_LookupId_BankId"].ToString()))
                        customerBankAccountVo.BankId = int.Parse(dr["WCMV_LookupId_BankId"].ToString());

                        if (!string.IsNullOrEmpty(dr["WCMV_Lookup_BranchAddCityId"].ToString()))
                            customerBankAccountVo.BranchAddCityId = int.Parse(dr["WCMV_Lookup_BranchAddCityId"].ToString());
                        if (!string.IsNullOrEmpty(dr["WCMV_Lookup_BranchAddStateId"].ToString()))
                            customerBankAccountVo.BranchAddStateId = int.Parse(dr["WCMV_Lookup_BranchAddStateId"].ToString());
                        if (!string.IsNullOrEmpty(dr["WCMV_Lookup_BranchAddCountryId"].ToString()))
                            customerBankAccountVo.BranchAddCountryId = int.Parse(dr["WCMV_Lookup_BranchAddCountryId"].ToString());

                        accountList.Add(customerBankAccountVo);

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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetCustomerBankAccounts()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return accountList;

        }

        public CustomerBankAccountVo GetCusomerBankAccount(int customerId, int customerBankAccId)
        {
            CustomerBankAccountVo customerBankAccountVo = null;
            Database db;
            DataSet getCustomerBankDs;
            DbCommand getCustomerBankAccCmd;
            DataRow dr;
            string query = "select * from CustomerBank a INNER JOIN WERPBankDataTransalationMapping b ON a.WERPBM_BankCode = b.WERPBM_BankCode where C_CustomerId=" + customerId + "and CB_CustBankAccId=" + customerBankAccId;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerBankAccCmd = db.GetSqlStringCommand(query);
                db.AddInParameter(getCustomerBankAccCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerBankAccCmd, "@CB_CustBankAccId", DbType.Int32, customerBankAccId);
                getCustomerBankDs = db.ExecuteDataSet(getCustomerBankAccCmd);

                if (getCustomerBankDs.Tables[0].Rows.Count > 0)
                {

                    customerBankAccountVo = new CustomerBankAccountVo();
                    dr = getCustomerBankDs.Tables[0].Rows[0];

                    if (dr["CB_CustBankAccId"].ToString() != "")
                        customerBankAccountVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                    customerBankAccountVo.BankName = dr["WERPBM_BankCode"].ToString();
                    if (dr["XBAT_BankAccountTypeCode"].ToString() == "SB")
                    {

                        customerBankAccountVo.AccountType = "SV";
                    }
                    else
                    {
                        customerBankAccountVo.AccountType = dr["XBAT_BankAccountTye"].ToString();
                    }
                    customerBankAccountVo.BankAccountNum = dr["CB_AccountNum"].ToString();
                    customerBankAccountVo.ModeOfOperation = dr["XMOH_ModeOfHolding"].ToString();
                    customerBankAccountVo.BranchName = dr["CB_BranchName"].ToString();
                    customerBankAccountVo.BranchAdrLine1 = dr["CB_BranchAdrLine1"].ToString();
                    customerBankAccountVo.BranchAdrLine2 = dr["CB_BranchAdrLine2"].ToString();
                    customerBankAccountVo.BranchAdrLine3 = dr["CB_BranchAdrLine3"].ToString();
                    if (dr["CB_BranchAdrPinCode"].ToString() != "")
                        customerBankAccountVo.BranchAdrPinCode = int.Parse(dr["CB_BranchAdrPinCode"].ToString());
                    customerBankAccountVo.RTGSCode = dr["CB_RTGS"].ToString();
                    customerBankAccountVo.NeftCode = dr["CB_NEFT"].ToString();
                    customerBankAccountVo.BranchAdrCity = dr["CB_BranchAdrCity"].ToString();
                    customerBankAccountVo.BranchAdrState = dr["CB_BranchAdrState"].ToString();
                    customerBankAccountVo.BranchAdrCountry = dr["CB_BranchAdrCountry"].ToString();
                    if (dr["CB_Balance"].ToString() != "")
                        customerBankAccountVo.Balance = float.Parse(dr["CB_Balance"].ToString());
                    //if (dr["CB_MICR"].ToString() != "")
                    //    customerBankAccountVo.MICR = long.Parse(dr["CB_MICR"].ToString());
                    customerBankAccountVo.MICR = dr["CB_MICR"].ToString();
                    customerBankAccountVo.IFSC = dr["CB_IFSC"].ToString();
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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetCusomerBankAccount()");


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

        public bool UpdateCustomerBankAccount(CustomerBankAccountVo customerBankAccountVo, int customerId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateCustomerBankCmd;
            //string query = "update CustomerBankAccount set CB_CustBankAccId='" + customerBankAccountVo.CustBankAccId + "',CB_BankName='" + customerBankAccountVo.BankName + "',CB_AccountType='" + customerBankAccountVo.AccountType + "',CB_AccountNum='" + customerBankAccountVo.AccountNum + "',CB_ModeOfOperation='" + customerBankAccountVo.ModeOfOperation + "',CB_BranchName='" + customerBankAccountVo.BankName + "',CB_BranchAdrLine1='" + customerBankAccountVo.BranchAdrLine1 + "',CB_BranchAdrLine2='" + customerBankAccountVo.BranchAdrLine2 + "',CB_BranchAdrLine3='" + customerBankAccountVo.BranchAdrLine3 + "',CB_BranchAdrPinCode='" + customerBankAccountVo.BranchAdrPinCode + "',CB_BranchAdrCity='" + customerBankAccountVo.BranchAdrCity + "',CB_BranchAdrState='" + customerBankAccountVo.BranchAdrState + "',CB_BranchAdrCountry='" + customerBankAccountVo.BranchAdrCountry + "',CB_MICR='" + customerBankAccountVo.MICR + "',CB_IFSC='" + customerBankAccountVo.IFSC + "'where C_CustomerId='" + customerId + "'";

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCustomerBankCmd = db.GetStoredProcCommand("SP_UpdateCustomerBankAccount");
                db.AddInParameter(updateCustomerBankCmd, "@CB_CustBankAccId", DbType.Int32, customerBankAccountVo.CustBankAccId);
                //db.AddInParameter(updateCustomerBankCmd, "@WERPBM_BankCode", DbType.String, customerBankAccountVo.BankName);
                //db.AddInParameter(updateCustomerBankCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerBankAccountVo.AccountType);
                db.AddInParameter(updateCustomerBankCmd, "@CB_AccountNum", DbType.String, customerBankAccountVo.BankAccountNum);
                db.AddInParameter(updateCustomerBankCmd, "@CB_IsHeldJointly", DbType.Int32, customerBankAccountVo.IsJointHolding);
                db.AddInParameter(updateCustomerBankCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerBankAccountVo.ModeOfOperation);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BankCity", DbType.String, customerBankAccountVo.BankCity);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchName", DbType.String, customerBankAccountVo.BranchName);
                //if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine1))
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrLine1", DbType.String, customerBankAccountVo.BranchAdrLine1);
                // if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine2))
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrLine2", DbType.String, customerBankAccountVo.BranchAdrLine2);
                // if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrLine3))
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrLine3", DbType.String, customerBankAccountVo.BranchAdrLine3);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrPinCode", DbType.String, customerBankAccountVo.BranchAdrPinCode);
                //if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCity))
                //db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrCity", DbType.String, customerBankAccountVo.BranchAdrCity);
                // if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrState))
                //db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrState", DbType.String, customerBankAccountVo.BranchAdrState);
                //if (!string.IsNullOrEmpty(customerBankAccountVo.BranchAdrCountry))
                //db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrCountry", DbType.String, customerBankAccountVo.BranchAdrCountry);
                db.AddInParameter(updateCustomerBankCmd, "@CB_Balance", DbType.Decimal, customerBankAccountVo.Balance);
                db.AddInParameter(updateCustomerBankCmd, "@CB_MICR", DbType.Int64, customerBankAccountVo.MICR);
                // if (!string.IsNullOrEmpty(customerBankAccountVo.IFSC))
                db.AddInParameter(updateCustomerBankCmd, "@CB_RTGS", DbType.String, customerBankAccountVo.RTGSCode);
                db.AddInParameter(updateCustomerBankCmd, "@CB_NEFT", DbType.String, customerBankAccountVo.NeftCode);

                db.AddInParameter(updateCustomerBankCmd, "@CB_IFSC", DbType.String, customerBankAccountVo.IFSC);
                db.AddInParameter(updateCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);

                db.AddInParameter(updateCustomerBankCmd, "@WCMV_LookupId_BankId", DbType.Int32, customerBankAccountVo.BankId);
                db.AddInParameter(updateCustomerBankCmd, "@WCMV_LookupId_AccType", DbType.Int32, customerBankAccountVo.BankAccTypeId);

                if (customerBankAccountVo.BranchAddCityId != 0)
                    db.AddInParameter(updateCustomerBankCmd, "@WCMV_Lookup_BranchAddCityId", DbType.Int32, customerBankAccountVo.BranchAddCityId);

                if (customerBankAccountVo.BranchAddStateId != 0)
                    db.AddInParameter(updateCustomerBankCmd, "@WCMV_Lookup_BranchAddStateId", DbType.Int32, customerBankAccountVo.BranchAddStateId);

                if (customerBankAccountVo.BranchAddCountryId != 0)
                    db.AddInParameter(updateCustomerBankCmd, "@WCMV_Lookup_BranchAddCountryId", DbType.Int32, customerBankAccountVo.BranchAddCountryId);
                db.AddInParameter(updateCustomerBankCmd, "@CB_IsCurrent", DbType.Int16, customerBankAccountVo.IsCurrent);
                db.ExecuteNonQuery(updateCustomerBankCmd);

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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:UpdateCustomerBankAccount()");


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
            Database db;
            DbCommand createCustomerBankCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerBankCmd = db.GetStoredProcCommand("SP_DeleteCustomerBankDetails");
                db.AddInParameter(createCustomerBankCmd, "@CB_CustBankAccId", DbType.Int32, customerAccountId);
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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:DeleteCustomerBankAccount()");

                object[] objects = new object[1];
                objects[0] = customerAccountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public DataTable GetALLBankName()
        {
            //string logoPath = "";
            Database db;
            DbCommand getBankNameCmd;
            DataSet getBankNameDs;
            // DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBankNameCmd = db.GetStoredProcCommand("SPRCO_GetAllBankName ");
                getBankNameDs = db.ExecuteDataSet(getBankNameCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return getBankNameDs.Tables[0];
        }


        public DataTable XMLBankaccountType()
        {
            //string logoPath = "";
            Database db;
            DbCommand getBankAccountCmd;
            DataSet getBankAccountDs;
            // DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBankAccountCmd = db.GetStoredProcCommand("SP_XMLBankAccountType");
                getBankAccountDs = db.ExecuteDataSet(getBankAccountCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return getBankAccountDs.Tables[0];
        }
        public DataTable XMLModeOfholding()
        {
            //string logoPath = "";
            Database db;
            DbCommand getModeHoldingCmd;
            DataSet getModeHoldingDs;
            // DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getModeHoldingCmd = db.GetStoredProcCommand("SP_XMLModeOfHolding ");
                getModeHoldingDs = db.ExecuteDataSet(getModeHoldingCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return getModeHoldingDs.Tables[0];
        }

        public DataTable AssetBankaccountType()
        {
            //string logoPath = "";
            Database db;
            DbCommand getBankAccountCmd;
            DataSet getBankAccountDs;
            // DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBankAccountCmd = db.GetStoredProcCommand("SP_BankAssetAccountType");
                getBankAccountDs = db.ExecuteDataSet(getBankAccountCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return getBankAccountDs.Tables[0];
        }

        public CustomerBankAccountVo GetCusomerIndBankAccount(int customerBankAccId)
        {
            CustomerBankAccountVo customerBankAccountVo = null;
            Database db;
            DataSet getCustomerBankDs;
            DbCommand getCustomerBankAccCmd;
            DataRow dr;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerBankAccCmd = db.GetStoredProcCommand("SP_GetCustomerIndividualBankAccounts");
                db.AddInParameter(getCustomerBankAccCmd, "@CB_CustBankAccId", DbType.Int32, customerBankAccId);
                getCustomerBankDs = db.ExecuteDataSet(getCustomerBankAccCmd);

                if (getCustomerBankDs.Tables[0].Rows.Count > 0)
                {
                    customerBankAccountVo = new CustomerBankAccountVo();
                    dr = getCustomerBankDs.Tables[0].Rows[0];
                    if (dr["CB_CustBankAccId"].ToString() != "")
                        customerBankAccountVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                    if (dr["CP_PortfolioId"].ToString() != "")
                        customerBankAccountVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    //customerBankAccountVo.BankName = dr["WERPBM_BankCode"].ToString();
                    //if (dr["XBAT_BankAccountTypeCode"].ToString() == "SB")
                    //{

                    //    customerBankAccountVo.AccountType = "SV";
                    //}
                    //else
                    //customerBankAccountVo.AccountType = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    //customerBankAccountVo.AccountTypeCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    customerBankAccountVo.BankAccountNum = dr["CB_AccountNum"].ToString();
                    customerBankAccountVo.ModeOfOperation = dr["XMOH_ModeOfHoldingCode"].ToString();
                    customerBankAccountVo.ModeOfOperationCode = dr["XMOH_ModeOfHolding"].ToString();
                    customerBankAccountVo.BranchName = dr["CB_BranchName"].ToString();
                    customerBankAccountVo.BankCity = dr["CB_BankCity"].ToString();
                    if (dr["CB_IsHeldJointly"].ToString() != "")
                        customerBankAccountVo.IsJointHolding = int.Parse(dr["CB_IsHeldJointly"].ToString());
                    customerBankAccountVo.BranchAdrLine1 = dr["CB_BranchAdrLine1"].ToString();
                    customerBankAccountVo.BranchAdrLine2 = dr["CB_BranchAdrLine2"].ToString();
                    customerBankAccountVo.BranchAdrLine3 = dr["CB_BranchAdrLine3"].ToString();
                    if (dr["CB_BranchAdrPinCode"].ToString() != "")
                        customerBankAccountVo.BranchAdrPinCode = int.Parse(dr["CB_BranchAdrPinCode"].ToString());
                    customerBankAccountVo.RTGSCode = dr["CB_RTGS"].ToString();
                    customerBankAccountVo.NeftCode = dr["CB_NEFT"].ToString();
                    //customerBankAccountVo.BranchAdrCity = dr["CB_BranchAdrCity"].ToString();
                    //customerBankAccountVo.BranchAdrState = dr["CB_BranchAdrState"].ToString();
                    //customerBankAccountVo.BranchAdrCountry = dr["CB_BranchAdrCountry"].ToString();

                    if (dr["CB_Balance"].ToString() != "")
                        customerBankAccountVo.Balance = float.Parse(dr["CB_Balance"].ToString());
                    //if (dr["CB_MICR"].ToString() != "")
                        customerBankAccountVo.MICR = dr["CB_MICR"].ToString();
                    customerBankAccountVo.IFSC = dr["CB_IFSC"].ToString();

                    if (!string.IsNullOrEmpty(dr["WCMV_BankName"].ToString()))
                        customerBankAccountVo.BankName = dr["WCMV_BankName"].ToString();
                    if (!string.IsNullOrEmpty(dr["WCMV_LookupId_BankId"].ToString()))
                        customerBankAccountVo.BankId = int.Parse(dr["WCMV_LookupId_BankId"].ToString());

                    if (!string.IsNullOrEmpty(dr["WCMV_LookupId_AccType"].ToString()))
                        customerBankAccountVo.BankAccTypeId = int.Parse(dr["WCMV_LookupId_AccType"].ToString());
                    

                    if (!string.IsNullOrEmpty(dr["WCMV_Lookup_BranchAddCityId"].ToString()))
                        customerBankAccountVo.BranchAddCityId = int.Parse(dr["WCMV_Lookup_BranchAddCityId"].ToString());
                    if (!string.IsNullOrEmpty(dr["WCMV_Lookup_BranchAddStateId"].ToString()))
                        customerBankAccountVo.BranchAddStateId = int.Parse(dr["WCMV_Lookup_BranchAddStateId"].ToString());
                    if (!string.IsNullOrEmpty(dr["WCMV_Lookup_BranchAddCountryId"].ToString()))
                        customerBankAccountVo.BranchAddCountryId = int.Parse(dr["WCMV_Lookup_BranchAddCountryId"].ToString());

                    customerBankAccountVo.IsCurrent = Convert.ToBoolean(dr["CB_IsCurrent"]);

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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetCusomerBankAccount()");
                object[] objects = new object[2];
                objects[0] = customerBankAccId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerBankAccountVo;

        }


        //public List<CustomerAccountsVo> GetCustomerBankTransaction(int CustBankAccId)
        //{

        //    List<CustomerBankAccountVo> accountList = null;
        //    CustomerBankAccountVo CustomerBankAccountVo;
        //    Database db;
        //    DataSet getCustomerBankTransactionDs;
        //    DbCommand getCustomerBankTransactionCmd;

        //    try
        //    {

        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        accountList = new List<CustomerBankAccountVo>();
        //        getCustomerBankTransactionCmd = db.GetStoredProcCommand("SP_GetCustomerBankAccounts");
        //        db.AddInParameter(getCustomerBankTransactionCmd, "@CB_CustBankAccId", DbType.Int32, CustBankAccId);
        //        getCustomerBankTransactionDs = db.ExecuteDataSet(getCustomerBankTransactionCmd);
        //        if (getCustomerBankTransactionDs.Tables[0].Rows.Count > 0)
        //        {

        //            foreach (DataRow dr in getCustomerBankTransactionDs.Tables[0].Rows)
        //            {
        //                CustomerBankAccountVo = new CustomerBankAccountVo();
        //                CustomerBankAccountVo.TransactionId = Convert.ToInt32(dr["CCST_TransactionId"].ToString());
        //                CustomerBankAccountVo.ExternalTransactionId = dr["CCST_ExternalTransactionId"].ToString();
        //                CustomerBankAccountVo.Transactiondate = DateTime.Parse(dr["CCST_Transactiondate"].ToString());
        //                CustomerBankAccountVo.CCST_Desc = dr["CCST_Desc"].ToString();
        //                CustomerBankAccountVo.ChequeNo = dr["CCST_ChequeNo"].ToString();
        //                CustomerBankAccountVo.IsWithdrwal = int.Parse(dr["CCST_IsWithdrwal"].ToString());
        //                CustomerBankAccountVo.Amount = double.Parse(dr["CB_HoldingAmount"].ToString());
        //                accountList.Add(CustomerBankAccountVo);

        //            }
        //        }

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetCustomerBankTransaction()");


        //        object[] objects = new object[1];
        //        objects[0] = CustBankAccId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return accountList;

        //}

        public bool DeleteCustomerBankAccountAssociates(int CB_CustBankAccId)
        {
            bool blResult = false;
            Database db;
            DbCommand deletecustomerbankCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deletecustomerbankCmd = db.GetStoredProcCommand("SP_DeleteCustomerBankAccountAssociates");
                db.AddInParameter(deletecustomerbankCmd, "@CB_CustBankAccId", DbType.Int32, CB_CustBankAccId);

                if (db.ExecuteNonQuery(deletecustomerbankCmd) != 0)
                    blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:UpdateCustomerBankAccount()");
                object[] objects = new object[1];
                objects[0] = CB_CustBankAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        public string Getfolioname(int folioid)
        {
            string portfolioname="";
            Database db;
            DbCommand GetfolionameCmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetfolionameCmd = db.GetStoredProcCommand("SP_PortFoliname");
                db.AddInParameter(GetfolionameCmd, "@AcntId", DbType.Int16, folioid);
                db.AddOutParameter(GetfolionameCmd, "@PortFoilo", DbType.String,50);
                if (db.ExecuteNonQuery(GetfolionameCmd) != 0)
                    portfolioname = db.GetParameterValue(GetfolionameCmd, "PortFoilo").ToString();

                return portfolioname;

            }
        }
    }
}
 