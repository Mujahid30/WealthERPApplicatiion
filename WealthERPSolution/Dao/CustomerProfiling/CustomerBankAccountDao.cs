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
        public bool CreateCustomerBankAccount(CustomerBankAccountVo customerBankAccountVo, int customerId, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createCustomerBankCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerBankCmd = db.GetStoredProcCommand("SP_CreateCustomerBankAccount");

                db.AddInParameter(createCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(createCustomerBankCmd, "@CB_BankName", DbType.String, customerBankAccountVo.BankName);
                db.AddInParameter(createCustomerBankCmd, "@XBAT_BankAccountTypeCode", DbType.String, customerBankAccountVo.AccountType);
                db.AddInParameter(createCustomerBankCmd, "@CB_AccountNum", DbType.String, customerBankAccountVo.AccountNum);
                db.AddInParameter(createCustomerBankCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerBankAccountVo.ModeOfOperation);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchName", DbType.String, customerBankAccountVo.BranchName);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrLine1", DbType.String, customerBankAccountVo.BranchAdrLine1);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrLine2", DbType.String, customerBankAccountVo.BranchAdrLine2);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrLine3", DbType.String, customerBankAccountVo.BranchAdrLine3);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrPinCode", DbType.Int32, customerBankAccountVo.BranchAdrPinCode);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrCity", DbType.String, customerBankAccountVo.BranchAdrCity);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrState", DbType.String, customerBankAccountVo.BranchAdrState);
                db.AddInParameter(createCustomerBankCmd, "@CB_BranchAdrCountry", DbType.String, customerBankAccountVo.BranchAdrCountry);
                db.AddInParameter(createCustomerBankCmd, "@CB_Balance", DbType.Decimal, customerBankAccountVo.Balance);
                db.AddInParameter(createCustomerBankCmd, "@CB_MICR", DbType.Int64, customerBankAccountVo.MICR);
                db.AddInParameter(createCustomerBankCmd, "@CB_IFSC", DbType.String, customerBankAccountVo.IFSC);
                db.AddInParameter(createCustomerBankCmd, "@CB_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerBankCmd, "@CB_ModifiedBy", DbType.Int32, userId);

                 
                db.ExecuteNonQuery(createCustomerBankCmd);

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

            return bResult;
        }

        public List<CustomerBankAccountVo> GetCustomerBankAccounts(int customerId)
        {

            List<CustomerBankAccountVo> accountList=null;
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
                getCustomerBankDs = db.ExecuteDataSet(getCustomerBankCmd);
                if (getCustomerBankDs.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in getCustomerBankDs.Tables[0].Rows)
                    {
                        customerBankAccountVo = new CustomerBankAccountVo();
                        customerBankAccountVo.CustBankAccId = int.Parse(dr["CB_CustBankAccId"].ToString());
                        customerBankAccountVo.BankName = dr["CB_BankName"].ToString();
                        customerBankAccountVo.AccountType = dr["XBAT_BankAccountTye"].ToString();
                        customerBankAccountVo.AccountNum = dr["CB_AccountNum"].ToString();
                        customerBankAccountVo.ModeOfOperation = dr["XMOH_ModeOfHolding"].ToString();
                        customerBankAccountVo.BranchName = dr["CB_BranchName"].ToString();
                        ////customerBankAccountVo.BranchAdrLine1 = dr["CB_BranchAdrLine1"].ToString();
                        ////customerBankAccountVo.BranchAdrLine2 = dr["CB_BranchAdrLine2"].ToString();
                        ////customerBankAccountVo.BranchAdrLine3 = dr["CB_BranchAdrLine3"].ToString();
                        ////customerBankAccountVo.BranchAdrPinCode = int.Parse(dr["CB_BranchAdrPinCode"].ToString());
                        ////customerBankAccountVo.BranchAdrCity = dr["CB_BranchAdrCity"].ToString();
                        ////customerBankAccountVo.BranchAdrState = dr["CB_BranchAdrState"].ToString();
                        ////customerBankAccountVo.BranchAdrCountry = dr["CB_BranchAdrCountry"].ToString();
                        ////customerBankAccountVo.Balance = float.Parse(dr["CB_Balance"].ToString());
                        ////customerBankAccountVo.MICR = long.Parse(dr["CB_MICR"].ToString());
                        ////customerBankAccountVo.IFSC = dr["CB_IFSC"].ToString();
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
            string query = "select * from CustomerBank where C_CustomerId=" + customerId + "and CB_CustBankAccId=" + customerBankAccId;
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
                    customerBankAccountVo.BankName = dr["CB_BankName"].ToString();
                    if (dr["XBAT_BankAccountTypeCode"].ToString() == "SB")
                    {

                        customerBankAccountVo.AccountType = "SV";
                    }
                    else
                    {
                        customerBankAccountVo.AccountType = dr["XBAT_BankAccountTypeCode"].ToString();
                    }
                    customerBankAccountVo.AccountNum = dr["CB_AccountNum"].ToString();
                    customerBankAccountVo.ModeOfOperation = dr["XMOH_ModeOfHoldingCode"].ToString();
                    customerBankAccountVo.BranchName = dr["CB_BranchName"].ToString();
                    customerBankAccountVo.BranchAdrLine1 = dr["CB_BranchAdrLine1"].ToString();
                    customerBankAccountVo.BranchAdrLine2 = dr["CB_BranchAdrLine2"].ToString();
                    customerBankAccountVo.BranchAdrLine3 = dr["CB_BranchAdrLine3"].ToString();
                    if (dr["CB_BranchAdrPinCode"].ToString() != "")
                    customerBankAccountVo.BranchAdrPinCode = int.Parse(dr["CB_BranchAdrPinCode"].ToString());
                    customerBankAccountVo.BranchAdrCity = dr["CB_BranchAdrCity"].ToString();
                    customerBankAccountVo.BranchAdrState = dr["CB_BranchAdrState"].ToString();
                    customerBankAccountVo.BranchAdrCountry = dr["CB_BranchAdrCountry"].ToString();
                    if (dr["CB_Balance"].ToString() != "")
                    customerBankAccountVo.Balance = float.Parse(dr["CB_Balance"].ToString());
                    if (dr["CB_MICR"].ToString() != "")
                    customerBankAccountVo.MICR = long.Parse(dr["CB_MICR"].ToString());
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
                db.AddInParameter(updateCustomerBankCmd, "@CB_BankName", DbType.String, customerBankAccountVo.BankName);
                db.AddInParameter(updateCustomerBankCmd, "@XBAT_BankAccountTypeCode", DbType.String, customerBankAccountVo.AccountType);
                db.AddInParameter(updateCustomerBankCmd, "@CB_AccountNum", DbType.String, customerBankAccountVo.AccountNum);
                db.AddInParameter(updateCustomerBankCmd, "@XMOH_ModeOfHoldingCode", DbType.String, customerBankAccountVo.ModeOfOperation);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchName", DbType.String, customerBankAccountVo.BranchName);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrLine1", DbType.String, customerBankAccountVo.BranchAdrLine1);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrLine2", DbType.String, customerBankAccountVo.BranchAdrLine2);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrLine3", DbType.String, customerBankAccountVo.BranchAdrLine3);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrPinCode", DbType.String, customerBankAccountVo.BranchAdrPinCode);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrCity", DbType.String, customerBankAccountVo.BranchAdrCity);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrState", DbType.String, customerBankAccountVo.BranchAdrState);
                db.AddInParameter(updateCustomerBankCmd, "@CB_BranchAdrCountry", DbType.String, customerBankAccountVo.BranchAdrCountry);
                db.AddInParameter(updateCustomerBankCmd, "@CB_Balance", DbType.Decimal, customerBankAccountVo.Balance);
                db.AddInParameter(updateCustomerBankCmd, "@CB_MICR", DbType.Int64, customerBankAccountVo.MICR);
                db.AddInParameter(updateCustomerBankCmd, "@CB_IFSC", DbType.String, customerBankAccountVo.IFSC);
                db.AddInParameter(updateCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
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
    }
}
