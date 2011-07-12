using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data.Common;
using VoFPSuperlite;

namespace DaoFPSuperlite
{
    public class CustomerProspectDao
    {
        public bool AddDetailsForCustomerProspect(int customerId, int userId, CustomerProspectVo customerprospectvo)
        {
            Database db;
            DbCommand cmdAddDetailsForCustomerProspect;
            bool bTotalResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddDetailsForCustomerProspect = db.GetStoredProcCommand("SP_AddDetailsForCustomerProspect");
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@CFPS_Income", DbType.Decimal, customerprospectvo.TotalIncome);
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@CFPS_Expense", DbType.Decimal, customerprospectvo.TotalExpense);
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@CFPS_Liabilities", DbType.Decimal, customerprospectvo.TotalLiabilities);
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@CFPS_Assets", DbType.Decimal, customerprospectvo.TotalAssets);
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@CFPS_LifeInsurance", DbType.Decimal, customerprospectvo.TotalLifeInsurance);
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@CFPS_GeneralInsurance", DbType.Decimal, customerprospectvo.TotalGeneralInsurance);
                db.AddInParameter(cmdAddDetailsForCustomerProspect, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddDetailsForCustomerProspect) != 0)
                    bTotalResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:AddDetailsForCustomerProspect(int customerId, int userId, CustomerProspect customerprospect)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bTotalResult;
        }

        /// <summary>
        /// Getting income details for a particular Customer used in Customer Prospect
        ///  </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetDetailsForCustomerProspect(int customerId)
        {
            Database db;
            DbCommand cmdGetDetailsForCustomerProspect;
            DataSet dsGetDetailsForCustomerProspect = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetDetailsForCustomerProspect = db.GetStoredProcCommand("SP_GetDetailsForCustomerProspect");
                db.AddInParameter(cmdGetDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
                dsGetDetailsForCustomerProspect = db.ExecuteDataSet(cmdGetDetailsForCustomerProspect);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetDetailsForCustomerProspect(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetDetailsForCustomerProspect;


        }

        /// <summary>
        /// Used to Add Income Details for Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectIncomeDetailsVo"></param>
        /// <returns></returns>
        public bool AddCustomerFPIncomeDetails(int customerId, int userId, CustomerProspectIncomeDetailsVo customerProspectIncomeDetailsVo)
        {
            Database db;
            DbCommand cmdAddCustomerFPIncomeDetails;
            bool bIncomeResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddCustomerFPIncomeDetails = db.GetStoredProcCommand("SP_AddCustomerFPIncomeDetails");
                db.AddInParameter(cmdAddCustomerFPIncomeDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectIncomeDetailsVo.IncomeCategoryCode != 0)
                    db.AddInParameter(cmdAddCustomerFPIncomeDetails, "@XIC_IncomeCategoryCode", DbType.Int32, customerProspectIncomeDetailsVo.IncomeCategoryCode);
                else
                    db.AddInParameter(cmdAddCustomerFPIncomeDetails, "@XIC_IncomeCategoryCode", DbType.Int32, 0);
                if (customerProspectIncomeDetailsVo.IncomeValue != 0)
                    db.AddInParameter(cmdAddCustomerFPIncomeDetails, "@CFPID_Value", DbType.Decimal, customerProspectIncomeDetailsVo.IncomeValue);
                else
                    db.AddInParameter(cmdAddCustomerFPIncomeDetails, "@CFPID_Value", DbType.Decimal, 0.0);

                db.AddInParameter(cmdAddCustomerFPIncomeDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddCustomerFPIncomeDetails) != 0)
                    bIncomeResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:AddCustomerFPIncomeDetails(int customerId,int userId,CustomerProspectIncomeDetailsVo customerProspectIncomeDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bIncomeResult;
        }

        /// <summary>
        /// Getting income details for a particular Customer used in Customer Prospect
        ///  </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetIncomeDetailsForCustomerProspect(int customerId)
        {
            Database db;
            DbCommand cmdGetIncomeDetailsForCustomerProspect;
            DataSet dsGetIncomeDetailsForCustomerProspect = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetIncomeDetailsForCustomerProspect = db.GetStoredProcCommand("SP_GetIncomeDetailsForCustomerProspect");
                db.AddInParameter(cmdGetIncomeDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
                dsGetIncomeDetailsForCustomerProspect = db.ExecuteDataSet(cmdGetIncomeDetailsForCustomerProspect);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetIncomeDetailsForCustomerProspect(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetIncomeDetailsForCustomerProspect;


        }

        /// <summary>
        /// Used to Update Customer Income Details for Particular Customers
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerProspectIncomeDetailsVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerIncomeDetailsForCustomerProspect(int userId, int customerId, CustomerProspectIncomeDetailsVo customerProspectIncomeDetailsVo)
        {
            Database db;
            DbCommand cmdUpdIncomeDetails;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdIncomeDetails = db.GetStoredProcCommand("SP_UpdateIncomeDetailsForCustomerProspect");
                db.AddInParameter(cmdUpdIncomeDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectIncomeDetailsVo.IncomeCategoryCode != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XIC_IncomeCategoryCode", DbType.Int32, customerProspectIncomeDetailsVo.IncomeCategoryCode);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XIC_IncomeCategoryCode", DbType.Int32, 0);
                if (customerProspectIncomeDetailsVo.IncomeValue != 0.0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CFPID_Value", DbType.Decimal, customerProspectIncomeDetailsVo.IncomeValue);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CFPID_Value", DbType.Decimal, 0.0);
                db.AddInParameter(cmdUpdIncomeDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdUpdIncomeDetails) != 0)
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
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:UpdateCustomerIncomeDetailsForCustomerProspect(int userId, int customerId, CustomerProspectIncomeDetailsVo customerProspectIncomeDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Used to Add Customer Expense Details in Customer Prospect
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectExpenseeDetailsVo"></param>
        /// <returns></returns>
        public bool AddCustomerFPExpenseDetails(int customerId, int userId, CustomerProspectExpenseDetailsVo customerProspectExpenseeDetailsVo)
        {
            Database db;
            DbCommand cmdAddCustomerFPExpenseDetails;
            bool bExpenseResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddCustomerFPExpenseDetails = db.GetStoredProcCommand("SP_AddCustomerFPExpenseDetails");
                db.AddInParameter(cmdAddCustomerFPExpenseDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectExpenseeDetailsVo.ExpenseCategoryCode != 0)
                    db.AddInParameter(cmdAddCustomerFPExpenseDetails, "@XEC_ExpenseCategoryCode", DbType.Int32, customerProspectExpenseeDetailsVo.ExpenseCategoryCode);
                else
                    db.AddInParameter(cmdAddCustomerFPExpenseDetails, "@XEC_ExpenseCategoryCode", DbType.Int32, 0);
                if (customerProspectExpenseeDetailsVo.ExpenseValue != 0)
                    db.AddInParameter(cmdAddCustomerFPExpenseDetails, "@CFPED_Value", DbType.Decimal, customerProspectExpenseeDetailsVo.ExpenseValue);
                else
                    db.AddInParameter(cmdAddCustomerFPExpenseDetails, "@CFPED_Value", DbType.Decimal, 0.0);

                db.AddInParameter(cmdAddCustomerFPExpenseDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddCustomerFPExpenseDetails) != 0)
                    bExpenseResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:AddCustomerFPExpenseDetails(int customerId, int userId, CustomerProspectIncomeDetailsVo customerProspectExpenseeDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bExpenseResult;
        }

        /// <summary>
        /// Used to Get Details about Expense in Customer Prospect
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetExpenseDetailsForCustomerProspect(int customerId)
        {
            Database db;
            DbCommand cmdGetExpenseDetailsForCustomerProspect;
            DataSet dsGetExpenseDetailsForCustomerProspect = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetExpenseDetailsForCustomerProspect = db.GetStoredProcCommand("SP_GetExpenseDetailsForCustomerProspect");
                db.AddInParameter(cmdGetExpenseDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
                dsGetExpenseDetailsForCustomerProspect = db.ExecuteDataSet(cmdGetExpenseDetailsForCustomerProspect);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetExpenseDetailsForCustomerProspect(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetExpenseDetailsForCustomerProspect;


        }

        /// <summary>
        /// Used to Update Expense Details for Particular Customer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerProspectExpenseDetailsVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerExpenseDetailsForCustomerProspect(int userId, int customerId, CustomerProspectExpenseDetailsVo customerProspectExpenseDetailsVo)
        {
            Database db;
            DbCommand cmdUpdExpenseDetails;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdExpenseDetails = db.GetStoredProcCommand("SP_UpdateExpenseDetailsForCustomerProspect");
                db.AddInParameter(cmdUpdExpenseDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectExpenseDetailsVo.ExpenseCategoryCode != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XEC_ExpenseCategoryCode", DbType.Int32, customerProspectExpenseDetailsVo.ExpenseCategoryCode);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XEC_ExpenseCategoryCode", DbType.Int32, 0);
                if (customerProspectExpenseDetailsVo.ExpenseValue != 0.0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CFPED_Value", DbType.Decimal, customerProspectExpenseDetailsVo.ExpenseValue);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CFPED_Value", DbType.Decimal, 0.0);
                db.AddInParameter(cmdUpdExpenseDetails, "@U_USerId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdUpdExpenseDetails) != 0)
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
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:UpdateCustomerIncomeDetailsForCustomerProspect(int userId, int customerId, CustomerProspectExpenseDetailsVo customerProspectExpenseDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Used to Add Liability Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectLiabilitiesDetailsVo"></param>
        /// <returns></returns>
        public bool AddLiabilitiesDetailsForCustomerProspect(int customerId, int userId, CustomerProspectLiabilitiesDetailsVo customerProspectLiabilitiesDetailsVo)
        {
            Database db;
            DbCommand cmdAddLiabilitiesDetailsForCustomerProspect;
            bool bLiabilitiesResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddLiabilitiesDetailsForCustomerProspect = db.GetStoredProcCommand("SP_AddLiabilitiesDetailsForCustomerProspect");
                db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectLiabilitiesDetailsVo.LoanTypeCode != 0)
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@XLT_LoanTypeCode", DbType.Int32, customerProspectLiabilitiesDetailsVo.LoanTypeCode);
                else
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@XLT_LoanTypeCode", DbType.Int32, 0);
                if (customerProspectLiabilitiesDetailsVo.AdjustedLoan != 0.0)
                {
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_AdjustedLoanOutstanding", DbType.Decimal, customerProspectLiabilitiesDetailsVo.AdjustedLoan);
                }
                else
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_AdjustedLoanOutstanding", DbType.Decimal, 0.0);

                if (customerProspectLiabilitiesDetailsVo.LoanOutstanding != 0.0)
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_LoanOutstanding", DbType.Decimal, customerProspectLiabilitiesDetailsVo.LoanOutstanding);
                else
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_LoanOutstanding", DbType.Decimal, 0.0);
                if (customerProspectLiabilitiesDetailsVo.Tenure != 0)
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_Tenure", DbType.Int32, customerProspectLiabilitiesDetailsVo.Tenure);
                else
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_Tenure", DbType.Int32, 0);
                if (customerProspectLiabilitiesDetailsVo.EMIAmount != 0)
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_EMIAmount", DbType.Decimal, customerProspectLiabilitiesDetailsVo.EMIAmount);
                else
                    db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@CFPLD_EMIAmount", DbType.Int32, 0.0);
                db.AddInParameter(cmdAddLiabilitiesDetailsForCustomerProspect, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddLiabilitiesDetailsForCustomerProspect) != 0)
                    bLiabilitiesResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:AddLiabilitiesDetailsForCustomerProspect(int customerId, int userId, CustomerProspectLiabilitiesDetailsVo customerProspectLiabilitiesDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bLiabilitiesResult;
        }

        /// <summary>
        /// Used to Get Liabilities DEtails of a Particular Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetLiabilitiesDetailsForCustomerProspect(int customerId)
        {
            Database db;
            DbCommand cmdGetLiabilitiesDetailsForCustomerProspect;
            DataSet dsGetLiabilitiesDetailsForCustomerProspect = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLiabilitiesDetailsForCustomerProspect = db.GetStoredProcCommand("SP_GetLiabilitiesDetailsForCustomerProspect");
                db.AddInParameter(cmdGetLiabilitiesDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
                dsGetLiabilitiesDetailsForCustomerProspect = db.ExecuteDataSet(cmdGetLiabilitiesDetailsForCustomerProspect);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetLiabilitiesDetailsForCustomerProspect(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetLiabilitiesDetailsForCustomerProspect;


        }

        /// <summary>
        /// Used to Update Liabilities Details of a Particular Customer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerProspectLiabilitiesDetailsVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerLiabilitiesDetailsForCustomerProspect(int userId, int customerId, CustomerProspectLiabilitiesDetailsVo customerProspectLiabilitiesDetailsVo)
        {
            Database db;
            DbCommand cmdUpdateLiabilitiesDetailsForCustomerProspect;
            bool bLiabilitiesResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdateLiabilitiesDetailsForCustomerProspect = db.GetStoredProcCommand("SP_UpdateLiabilitiesDetailsForCustomerProspect");
                db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectLiabilitiesDetailsVo.LoanTypeCode != 0)
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@XLT_LoanTypeCode", DbType.Int32, customerProspectLiabilitiesDetailsVo.LoanTypeCode);
                else
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@XLT_LoanTypeCode", DbType.Int32, 0);
                if (customerProspectLiabilitiesDetailsVo.LoanOutstanding != 0.0)
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@CFPLD_LoanOutstanding", DbType.Decimal, customerProspectLiabilitiesDetailsVo.LoanOutstanding);
                else
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@CFPLD_LoanOutstanding", DbType.Decimal, 0.0);
                if (customerProspectLiabilitiesDetailsVo.Tenure != 0)
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@CFPLD_Tenure", DbType.Int32, customerProspectLiabilitiesDetailsVo.Tenure);
                else
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@CFPLD_Tenure", DbType.Int32, 0);
                if (customerProspectLiabilitiesDetailsVo.EMIAmount != 0)
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@CFPLD_EMIAmount", DbType.Int32, customerProspectLiabilitiesDetailsVo.EMIAmount);
                else
                    db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@CFPLD_EMIAmount", DbType.Int32, 0.0);
                db.AddInParameter(cmdUpdateLiabilitiesDetailsForCustomerProspect, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdUpdateLiabilitiesDetailsForCustomerProspect) != 0)
                    bLiabilitiesResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:UpdateCustomerLiabilitiesDetailsForCustomerProspect(int userId, int customerId, CustomerProspectLiabilitiesDetailsVo customerProspectLiabilitiesDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bLiabilitiesResult;
        }


        /// <summary>
        /// Used to Add Customer FP AssetnSub Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetSubDetailsVo"></param>
        /// <returns></returns>
        public bool AddCustomerFPAssetSubInstrumentDetails(int customerId, int userId, CustomerProspectAssetSubDetailsVo customerProspectAssetSubDetailsVo)
        {
            Database db;
            DbCommand cmdAddCustomerFPAssetSubInstrumentDetails;
            bool bAssetSubResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddCustomerFPAssetSubInstrumentDetails = db.GetStoredProcCommand("SP_AddCustomerFPAssetSubInstrumentDetails");
                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@C_CustomerId", DbType.Int32, customerId);

                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@PAG_AssetGroupCode", DbType.String, customerProspectAssetSubDetailsVo.AssetGroupCode);


                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerProspectAssetSubDetailsVo.AssetInstrumentCategoryCode);


                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, customerProspectAssetSubDetailsVo.AssetInstrumentSubCategoryCode);

                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@CFPASID_AdjustedValue", DbType.Decimal, customerProspectAssetSubDetailsVo.AdjustedValue);


                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@CFPASID_Value", DbType.Decimal, customerProspectAssetSubDetailsVo.Value);

                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@CFPASID_MaturityDate", DbType.DateTime, customerProspectAssetSubDetailsVo.MaturityDate);
                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@CFPASID_Premium", DbType.Decimal, customerProspectAssetSubDetailsVo.Premium);

                db.AddInParameter(cmdAddCustomerFPAssetSubInstrumentDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddCustomerFPAssetSubInstrumentDetails) != 0)
                    bAssetSubResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:AddCustomerFPAssetSubInstrumentDetails(int customerId, int userId, CustomerProspectAssetSubDetailsVo customerProspectAssetSubDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bAssetSubResult;

        }

        /// <summary>
        /// Used to Get Customer FP Asset Sub Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerFPAssetSubInstrumentDetails(int customerId)
        {
            Database db;
            DbCommand cmdGetCustomerFPAssetSubInstrumentDetails;
            DataSet dsGetCustomerFPAssetSubInstrumentDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerFPAssetSubInstrumentDetails = db.GetStoredProcCommand("SP_GetCustomerFPAssetSubInstrumentDetails");
                db.AddInParameter(cmdGetCustomerFPAssetSubInstrumentDetails, "@C_CustomerId", DbType.Int32, customerId);
                dsGetCustomerFPAssetSubInstrumentDetails = db.ExecuteDataSet(cmdGetCustomerFPAssetSubInstrumentDetails);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerFPAssetSubInstrumentDetails(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerFPAssetSubInstrumentDetails;


        }


        /// <summary>
        /// Used to Update Customer FP Asset Sub Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetSubDetailsVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerFPAssetSubInstrumentDetails(int customerId, int userId, CustomerProspectAssetSubDetailsVo customerProspectAssetSubDetailsVo)
        {
            Database db;
            DbCommand cmdUpdateCustomerFPAssetSubInstrumentDetails;
            bool bAssetSubResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdateCustomerFPAssetSubInstrumentDetails = db.GetStoredProcCommand("SP_UpdateCustomerFPAssetSubInstrumentDetails");
                db.AddInParameter(cmdUpdateCustomerFPAssetSubInstrumentDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectAssetSubDetailsVo.AssetGroupCode != null)
                    db.AddInParameter(cmdUpdateCustomerFPAssetSubInstrumentDetails, "@PAG_AssetGroupCode", DbType.String, customerProspectAssetSubDetailsVo.AssetGroupCode);

                if (customerProspectAssetSubDetailsVo.AssetInstrumentCategoryCode != null)
                    db.AddInParameter(cmdUpdateCustomerFPAssetSubInstrumentDetails, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerProspectAssetSubDetailsVo.AssetInstrumentCategoryCode);

                if (customerProspectAssetSubDetailsVo.AssetInstrumentSubCategoryCode != null)
                    db.AddInParameter(cmdUpdateCustomerFPAssetSubInstrumentDetails, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, customerProspectAssetSubDetailsVo.AssetInstrumentSubCategoryCode);

                if (customerProspectAssetSubDetailsVo.Value != 0)
                    db.AddInParameter(cmdUpdateCustomerFPAssetSubInstrumentDetails, "@CFPASID_Value", DbType.Decimal, customerProspectAssetSubDetailsVo.Value);
                else
                    db.AddInParameter(cmdUpdateCustomerFPAssetSubInstrumentDetails, "@CFPASID_Value", DbType.Decimal, 0.0);


                db.AddInParameter(cmdUpdateCustomerFPAssetSubInstrumentDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdUpdateCustomerFPAssetSubInstrumentDetails) != 0)
                    bAssetSubResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:UpdateCustomerFPAssetSubInstrumentDetails(int customerId, int userId, CustomerProspectAssetSubDetailsVo customerProspectAssetSubDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bAssetSubResult;
        }

        /// <summary>
        /// Used to Add Customer FP Asset Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetDetailsVo"></param>
        /// <returns></returns>
        public bool AddCustomerFPAssetInstrumentDetails(int customerId, int userId, CustomerProspectAssetDetailsVo customerProspectAssetDetailsVo)
        {
            Database db;
            DbCommand cmdAddCustomerFPAssetInstrumentDetails;
            bool bAssetResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddCustomerFPAssetInstrumentDetails = db.GetStoredProcCommand("SP_AddCustomerFPAssetInstrumentDetails");
                db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectAssetDetailsVo.AssetGroupCode != null)
                    db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@PAG_AssetGroupCode", DbType.String, customerProspectAssetDetailsVo.AssetGroupCode);


                db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerProspectAssetDetailsVo.AssetInstrumentCategoryCode);


                if (customerProspectAssetDetailsVo.AdjustedValue != 0.0)
                    db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_AdjustedValue", DbType.Decimal, customerProspectAssetDetailsVo.AdjustedValue);
                else
                    db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_AdjustedValue", DbType.Decimal, 0.0);

                if (customerProspectAssetDetailsVo.Value != 0.0)
                    db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_Value", DbType.Decimal, customerProspectAssetDetailsVo.Value);
                else
                    db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_Value", DbType.Decimal, 0.0);

                if (customerProspectAssetDetailsVo.SurrMktVal != 0.0)
                    db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_SurrenderMarketValue", DbType.Decimal, customerProspectAssetDetailsVo.SurrMktVal);
                else
                    db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_SurrenderMarketValue", DbType.Decimal, 0.0);


                db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_MaturityDate", DbType.DateTime, customerProspectAssetDetailsVo.MaturityDate);


                db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_Premium", DbType.Decimal, customerProspectAssetDetailsVo.Premium);

                db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_AdjustedPremium", DbType.Decimal, customerProspectAssetDetailsVo.AdjustedPremium);

                db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@CFPAID_TotalPremiumValue", DbType.Decimal, customerProspectAssetDetailsVo.TotalPremiumValue);


                db.AddInParameter(cmdAddCustomerFPAssetInstrumentDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddCustomerFPAssetInstrumentDetails) != 0)
                    bAssetResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:AddCustomerFPAssetInstrumentDetails(int customerId, int userId, CustomerProspectAssetDetailsVo customerProspectAssetDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bAssetResult;
        }

        /// <summary>
        /// Used to Add FP Asset Instrument details of the Particular Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerFPAssetInstrumentDetails(int customerId)
        {
            Database db;
            DbCommand cmdGetCustomerFPAssetInstrumentDetails;
            DataSet dsGetCustomerFPAssetInstrumentDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerFPAssetInstrumentDetails = db.GetStoredProcCommand("SP_GetCustomerFPAssetInstrumentDetails");
                db.AddInParameter(cmdGetCustomerFPAssetInstrumentDetails, "@C_CustomerId", DbType.Int32, customerId);
                dsGetCustomerFPAssetInstrumentDetails = db.ExecuteDataSet(cmdGetCustomerFPAssetInstrumentDetails);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerFPAssetInstrumentDetails(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerFPAssetInstrumentDetails;


        }

        /// <summary>
        /// Used to Update Customer FP Asset Instrument Details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetDetailsVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerFPAssetInstrumentDetails(int customerId, int userId, CustomerProspectAssetDetailsVo customerProspectAssetDetailsVo)
        {
            Database db;
            DbCommand cmdUpdateCustomerFPAssetInstrumentDetails;
            bool bAssetResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdateCustomerFPAssetInstrumentDetails = db.GetStoredProcCommand("SP_AddCustomerFPAssetInstrumentDetails");
                db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectAssetDetailsVo.AssetGroupCode != null)
                    db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@PAG_AssetGroupCode", DbType.String, customerProspectAssetDetailsVo.AssetGroupCode);

                if (customerProspectAssetDetailsVo.AssetInstrumentCategoryCode != null)
                    db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@PAIC_AssetInstrumentCategoryCode", DbType.String, customerProspectAssetDetailsVo.AssetInstrumentCategoryCode);

                if (customerProspectAssetDetailsVo.InstrumentDetailsId != 0)
                    db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@CFPAID_FPInstrumentDetailsId", DbType.Int32, customerProspectAssetDetailsVo.InstrumentDetailsId);

                if (customerProspectAssetDetailsVo.Value != 0.0)
                    db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@CFPAID_Value", DbType.Decimal, customerProspectAssetDetailsVo.Value);
                else
                    db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@CFPAID_Value", DbType.Decimal, 0.0);


                db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@CFPAID_MaturityDate", DbType.DateTime, customerProspectAssetDetailsVo.MaturityDate);


                db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@CFPAID_Premium", DbType.Decimal, customerProspectAssetDetailsVo.Premium);

                db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@CFPAID_AdjustedPremium", DbType.Decimal, customerProspectAssetDetailsVo.AdjustedPremium);

                db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@CFPAID_TotalPremiumValue", DbType.Decimal, customerProspectAssetDetailsVo.TotalPremiumValue);

                db.AddInParameter(cmdUpdateCustomerFPAssetInstrumentDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdUpdateCustomerFPAssetInstrumentDetails) != 0)
                    bAssetResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:UpdateCustomerFPAssetInstrumentDetails(int customerId, int userId, CustomerProspectAssetDetailsVo customerProspectAssetDetailsVo)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bAssetResult;
        }
        //public void DeleteDetailsForCustomerProspect(int customerId)
        //{
        //    Database db;
        //    DbCommand cmdDeleteDetailsForCustomerProspect;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        cmdDeleteDetailsForCustomerProspect = db.GetStoredProcCommand("SP_DeleteDetailsForCustomerProspect");
        //        db.AddInParameter(cmdDeleteDetailsForCustomerProspect, "@C_CustomerId", DbType.Int32, customerId);
        //        db.ExecuteNonQuery(cmdDeleteDetailsForCustomerProspect);

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerFPAssetInstrumentDetails(int customerId)");
        //        object[] objects = new object[1];
        //        objects[0] = customerId;
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }


        //}


        /// <summary>
        /// Used to add CustomerFPAssetGroup Details. First Level of Category
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="customerProspectAssetGroupDetails"></param>
        /// <returns></returns>
        public bool AddCustomerFPAssetGroupDetails(int customerId, int userId, CustomerProspectAssetGroupDetails customerProspectAssetGroupDetails)
        {
            Database db;
            DbCommand cmdAddCustomerFPAssetGroupDetails;
            bool bAssetGroupResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddCustomerFPAssetGroupDetails = db.GetStoredProcCommand("SP_AddCustomerFPAssetGroupDetails");
                db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerProspectAssetGroupDetails.AssetGroupCode != null)
                    db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@PAG_AssetGroupCode", DbType.String, customerProspectAssetGroupDetails.AssetGroupCode);
                if (customerProspectAssetGroupDetails.AdjustedValue != 0.0)
                    db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@CFPAGD_AdjustedValue", DbType.Decimal, customerProspectAssetGroupDetails.AdjustedValue);
                else
                    db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@CFPAGD_AdjustedValue", DbType.Decimal, 0.0);

                if (customerProspectAssetGroupDetails.Value != 0.0)
                    db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@CFPAGD_Value", DbType.Decimal, customerProspectAssetGroupDetails.Value);
                else
                    db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@CFPAGD_Value", DbType.Decimal, 0.0);

                if (customerProspectAssetGroupDetails.TotalPremiumValue != 0.0)
                    db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@CFPAGD_TotalPremiumValue", DbType.Decimal, customerProspectAssetGroupDetails.TotalPremiumValue);
                else
                    db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@CFPAGD_TotalPremiumValue", DbType.Decimal, 0.0);

                db.AddInParameter(cmdAddCustomerFPAssetGroupDetails, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(cmdAddCustomerFPAssetGroupDetails) != 0)
                    bAssetGroupResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:AddCustomerFPAssetGroupDetails(int customerId, int userId, CustomerProspectAssetGroupDetails customerProspectAssetGroupDetails)");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bAssetGroupResult;
        }

        /// <summary>
        /// Used to Add FP Asset Group details of the Particular Customer. first Level of Category
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerFPAssetGroupDetails(int customerId)
        {
            Database db;
            DbCommand cmdGetCustomerFPAssetGroupDetails;
            DataSet dsGetCustomerFPAssetGroupDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerFPAssetGroupDetails = db.GetStoredProcCommand("SP_GetCustomerFPAssetGroupDetails");
                db.AddInParameter(cmdGetCustomerFPAssetGroupDetails, "@C_CustomerId", DbType.Int32, customerId);
                dsGetCustomerFPAssetGroupDetails = db.ExecuteDataSet(cmdGetCustomerFPAssetGroupDetails);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerFPAssetGroupDetails(int customerId)");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCustomerFPAssetGroupDetails;


        }


        /// <summary>
        /// Used to Show FP Assets and liabilitis.
        /// </summary>
        /// Added by: Vinayak Patil.
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        /// 
        public DataSet GetFPDashBoardAsstesBreakUp(int CustomerId)
        {
            Database db;
            DbCommand getFPDashBoardAsstesBreakUpCmd;
            DataSet dsFPDashBoard = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFPDashBoardAsstesBreakUpCmd = db.GetStoredProcCommand("SP_FPAssetsAndLiabilitieDetails");
                if (CustomerId != 0)
                    db.AddInParameter(getFPDashBoardAsstesBreakUpCmd, "@CustomerID", DbType.Int32, CustomerId);

                dsFPDashBoard = db.ExecuteDataSet(getFPDashBoardAsstesBreakUpCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:GetFPDashBoardAsstesBreakUp()");
                object[] objects = new object[3];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFPDashBoard;
        }


        /// <summary>
        /// Used to Show Current and Recomonded Asset allocation.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetFPCurrentAndRecomondedAssets(int CustomerId)
        {
            Database db;
            DbCommand getFPCurrentAndRecomondedAssetsCmd;
            DataSet dsFPCurrentAndRecomondedAssets = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFPCurrentAndRecomondedAssetsCmd = db.GetStoredProcCommand("SP_GetCustomerAssets");
                if (CustomerId != 0)
                    db.AddInParameter(getFPCurrentAndRecomondedAssetsCmd, "@CustomerID", DbType.Int32, CustomerId);

                dsFPCurrentAndRecomondedAssets = db.ExecuteDataSet(getFPCurrentAndRecomondedAssetsCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:GetFPCurrentAndRecomondedAssets()");
                object[] objects = new object[3];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFPCurrentAndRecomondedAssets;
        }


        /// <summary>
        /// Used to get unmanaged and Managed Details for FP Propect screen
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetUnmanagedManagedDetailsForFP(int CustomerId,int AdvisorId,int Switch)
        {
            Database db;
            DbCommand getUnmanagedManagedDetailsForFPCmd;
            DataSet dsgetUnmanagedManagedDetailsForFP = null;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUnmanagedManagedDetailsForFPCmd = db.GetStoredProcCommand("SP_GetManagedUnmanagedDetailsForFP");
                if (CustomerId != 0)
                    db.AddInParameter(getUnmanagedManagedDetailsForFPCmd, "@CustomerID", DbType.Int32, CustomerId);
                if (AdvisorId != 0)
                    db.AddInParameter(getUnmanagedManagedDetailsForFPCmd, "@AdvisorId", DbType.Int32, AdvisorId);

                db.AddInParameter(getUnmanagedManagedDetailsForFPCmd, "@Switch", DbType.Int32, Switch);

                dsgetUnmanagedManagedDetailsForFP = db.ExecuteDataSet(getUnmanagedManagedDetailsForFPCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:GetUnmanagedManagedDetailsForFP(int CustomerId)");
                object[] objects = new object[1];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsgetUnmanagedManagedDetailsForFP;
        }

        /// <summary>
        /// Used to Show Current and Recomonded Asset allocation.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DataSet GetAllDetailsForCustomerProspect(int CustomerId)
        {
            Database db;
            DbCommand getAllDetailsForCustomerProspectCmd;
            DataSet dsGetAllDetailsForCustomerProspect = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAllDetailsForCustomerProspectCmd = db.GetStoredProcCommand("SP_GetAllDetailsForCustomerProspect");
                if (CustomerId != 0)
                    db.AddInParameter(getAllDetailsForCustomerProspectCmd, "@C_CustomerId", DbType.Int32, CustomerId);

                dsGetAllDetailsForCustomerProspect = db.ExecuteDataSet(getAllDetailsForCustomerProspectCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:GetAllDetailsForCustomerProspect(int CustomerId)GetAllDetailsForCustomerProspect(int CustomerId)");
                object[] objects = new object[3];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAllDetailsForCustomerProspect;
        }

        /// <summary>
        /// To get All the Prospect custoomers for perticular RMId.
        /// </summary>
        /// Vinayak Patil
        /// <param name="RmId"></param>
        /// <returns></returns>
        public DataSet GetAllProspectCustomersForRM(int RmId)
        {
            Database db;
            DbCommand getAllProspectCustomersForRMCmd;
            DataSet dsGetAllProspectCustomersForRM = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAllProspectCustomersForRMCmd = db.GetStoredProcCommand("SP_GetAllProspectCustomersForRM");
                if (RmId != 0)
                    db.AddInParameter(getAllProspectCustomersForRMCmd, "@AR_RMId", DbType.Int32, RmId);

                dsGetAllProspectCustomersForRM = db.ExecuteDataSet(getAllProspectCustomersForRMCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerProspectDao.cs:GetAllProspectCustomersForRM(int CustomerId)GetAllDetailsForCustomerProspect(int CustomerId)");
                object[] objects = new object[3];
                objects[0] = RmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAllProspectCustomersForRM;
        }


        /// <summary>
        /// To get the FP analytic Standard data .. 
        /// </summary>
        /// Created by   ** Bhoopendra Sahoo **
        /// <param name="customerId"></param>
        /// <returns></returns>
        
        public DataSet GetCustomerFPAnalyticsStandard(int customerId)
        {
            Database db;
            DbCommand getCustomerFPAnalyticsStandardCmd;
            DataSet dsGetCustomerFPAnalyticsStandard = null;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerFPAnalyticsStandardCmd = db.GetStoredProcCommand("SP_FPAnalyticsStandard");
                db.AddInParameter(getCustomerFPAnalyticsStandardCmd, "@CustomerId", DbType.Int32,customerId);
                dsGetCustomerFPAnalyticsStandard = db.ExecuteDataSet(getCustomerFPAnalyticsStandardCmd);
            }

             catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetCustomerFPAnalyticsStandard;
        }

        // ***** End ****
        
    }
}
