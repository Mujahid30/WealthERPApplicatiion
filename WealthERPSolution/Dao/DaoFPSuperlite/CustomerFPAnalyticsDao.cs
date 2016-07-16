using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

using System.Data;
using System.Data.Sql;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoFPSuperlite
{
    public class CustomerFPAnalyticsDao
    {
        public DataSet GetCustomerProjectedAssetAllocation(int CustomerID)
        {
            Database db;
            DbCommand projectedAssetAllocationCmd;
            DataSet projectedAssetAllocationDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                projectedAssetAllocationCmd = db.GetStoredProcCommand("SP_GetCustomerProjectedAssetAllocation");
                db.AddInParameter(projectedAssetAllocationCmd, "@C_CustomerId", DbType.Int32, CustomerID);
                projectedAssetAllocationDs = db.ExecuteDataSet(projectedAssetAllocationCmd);
                projectedAssetAllocationDs.Tables[0].TableName = "CustomerProjectedAssetAllocation";
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFPAnalyticsDao:GetCustomerProjectedAssetAllocation()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return projectedAssetAllocationDs;
        }


        public DataSet GetCustomerDataForFutureSurplusEngine(int CustomerID,out decimal incomeTotal,out decimal expenseTotal,out int age)
        {
            Database db;
            DbCommand customerDataForFutureSurplusCmd;
            DataSet customerDataForFutureSurplusDs;
            incomeTotal = 0;
            expenseTotal = 0;
            age = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                customerDataForFutureSurplusCmd = db.GetStoredProcCommand("SP_GetCustomerDataForFutureSurplusEngine");
                db.AddInParameter(customerDataForFutureSurplusCmd, "@CustomerId", DbType.Int32, CustomerID);
                db.AddOutParameter(customerDataForFutureSurplusCmd, "@IncomeTotal", DbType.Decimal, 18);
                db.AddOutParameter(customerDataForFutureSurplusCmd, "@ExpenseTotal", DbType.Decimal, 18);
                db.AddOutParameter(customerDataForFutureSurplusCmd, "@CustomerAge", DbType.Int16, 100);

                customerDataForFutureSurplusDs = db.ExecuteDataSet(customerDataForFutureSurplusCmd);

                customerDataForFutureSurplusDs.Tables[0].TableName = "CustomerStaticAssumption";
                customerDataForFutureSurplusDs.Tables[1].TableName = "CustomerProjectedAssumption";
                customerDataForFutureSurplusDs.Tables[2].TableName = "CustomerFPIncomeDetails";
                customerDataForFutureSurplusDs.Tables[3].TableName = "CustomerAssetAllocation";
                customerDataForFutureSurplusDs.Tables[4].TableName = "CustomerCurrentAssetAllocation";
                customerDataForFutureSurplusDs.Tables[5].TableName = "CustomerGoalFunding";
                customerDataForFutureSurplusDs.Tables[6].TableName = "CustomerFutureSavings";

                Object objIncomeTotal = db.GetParameterValue(customerDataForFutureSurplusCmd, "@IncomeTotal");
                if (objIncomeTotal != DBNull.Value)
                    incomeTotal = decimal.Parse(db.GetParameterValue(customerDataForFutureSurplusCmd, "@IncomeTotal").ToString());
                

                Object objExpenseTotal = db.GetParameterValue(customerDataForFutureSurplusCmd, "@ExpenseTotal");
                if (objExpenseTotal != DBNull.Value)
                    expenseTotal = decimal.Parse(db.GetParameterValue(customerDataForFutureSurplusCmd, "@ExpenseTotal").ToString());
                

                Object objAge = db.GetParameterValue(customerDataForFutureSurplusCmd, "@CustomerAge");
                if (objAge != DBNull.Value)
                    age = int.Parse(db.GetParameterValue(customerDataForFutureSurplusCmd, "@CustomerAge").ToString());
                

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFPAnalyticsDao:GetCustomerDataForFutureSurplusEngine()");


                object[] objects = new object[4];
                objects[0] = CustomerID;
                objects[1] = incomeTotal;
                objects[2] = expenseTotal;
                objects[3] = age;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerDataForFutureSurplusDs;
        }


        public void UpdateFPProjectionAssetAllocation(int customerId,int rangeFromYear,int rangeToYear,int tempYear, decimal equityAgreedAssetAllocation, decimal debtAgreedAssetAllocation, decimal cashAgreedAssetAllocation, decimal alternateAgreedAssetAllocation)
        {
            Database db;
            DbCommand updateAssetAllocationCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAssetAllocationCmd = db.GetStoredProcCommand("SP_UpdateFPProjectionAssetAllocation");
                db.AddInParameter(updateAssetAllocationCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(updateAssetAllocationCmd, "@rangeFromYear", DbType.Int32, rangeFromYear);
                db.AddInParameter(updateAssetAllocationCmd, "@rangeToYear", DbType.Int32, rangeToYear);
                db.AddInParameter(updateAssetAllocationCmd, "@year", DbType.Int32, tempYear);
                db.AddInParameter(updateAssetAllocationCmd, "@euiityAgreed", DbType.Decimal, equityAgreedAssetAllocation);
                db.AddInParameter(updateAssetAllocationCmd, "@debtAgreed", DbType.Decimal, debtAgreedAssetAllocation);
                db.AddInParameter(updateAssetAllocationCmd, "@cashAgreed", DbType.Decimal, cashAgreedAssetAllocation);
                db.AddInParameter(updateAssetAllocationCmd, "@alternateAgreed", DbType.Decimal, alternateAgreedAssetAllocation);
              

                db.ExecuteNonQuery(updateAssetAllocationCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }


        public void UpdateFutureSavingProjection(int customerId, int advisorId, decimal equityFutureAllocation, decimal debtFutureAllocation, decimal cashFutureAllocation, decimal alternateFutureAllocation, int tempYear,int rangeFromYear,int rangeToYear)
        {
            Database db;
            DbCommand updateFutureSavingCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateFutureSavingCmd = db.GetStoredProcCommand("SP_UpdateFutureSavingProjection");
                db.AddInParameter(updateFutureSavingCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(updateFutureSavingCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(updateFutureSavingCmd, "@rangeFromYear", DbType.Int32, rangeFromYear);
                db.AddInParameter(updateFutureSavingCmd, "@rangeToYear", DbType.Int32, rangeToYear);
                db.AddInParameter(updateFutureSavingCmd, "@year", DbType.Int32, tempYear);
                db.AddInParameter(updateFutureSavingCmd, "@equityPercent", DbType.Decimal, equityFutureAllocation);
                db.AddInParameter(updateFutureSavingCmd, "@debtPercent", DbType.Decimal, debtFutureAllocation);
                db.AddInParameter(updateFutureSavingCmd, "@cashPercent", DbType.Decimal, cashFutureAllocation);
                db.AddInParameter(updateFutureSavingCmd, "@alternatePercent", DbType.Decimal, alternateFutureAllocation);


                db.ExecuteNonQuery(updateFutureSavingCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        
        }
        public DataTable BindDropdownsRebalancing(int adviserId)
        {
            Database db;
            DbCommand BindDropdownsRebalancingCmd;
            DataTable dtBindDropdownsRebalancing=new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                BindDropdownsRebalancingCmd = db.GetStoredProcCommand("SP_BindDropdownsRebalancing");

                db.AddInParameter(BindDropdownsRebalancingCmd, "@adviserId", DbType.Int32, adviserId);
                DataSet ds = db.ExecuteDataSet(BindDropdownsRebalancingCmd);
                if (ds != null)
                {
                    dtBindDropdownsRebalancing = ds.Tables[0];
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dtBindDropdownsRebalancing;
        }



        public DataSet GetProductTypes()
        {
            Database db;
            DbCommand getProductTypesCmd;
            DataSet dsGetProductTypes;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getProductTypesCmd = db.GetStoredProcCommand("SPROC_GetProductTypes");
                dsGetProductTypes = db.ExecuteDataSet(getProductTypesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
           

            return dsGetProductTypes;
        }







        public int CreateCashFlowRecomendation(int CustomerId, int userId,int CRPL_ID, int CCRLSourceId, String CCRL_BuyType, decimal CCRLAmount, DateTime startDate, DateTime endDate, decimal SumAssured, string Remarks, String CCRL_FrequencyMode)
        {
            int customercashrecomendationid = 0;
            Database db;
            DbCommand createCashFlowRecomendationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCashFlowRecomendationCmd = db.GetStoredProcCommand("SP_CreateRecomendationInvestment");
                db.AddInParameter(createCashFlowRecomendationCmd, "@C_CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_SourceId", DbType.Int32, CCRLSourceId);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_Amount", DbType.Decimal, CCRLAmount);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_StartDate", DbType.DateTime, startDate);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_EndDate", DbType.DateTime, endDate);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_SumAssured", DbType.Decimal, SumAssured);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_Remarks", DbType.String, Remarks);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_CreatedBy", DbType.String, userId);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_FrequencyMode", DbType.String, CCRL_FrequencyMode);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CCRL_BuyType", DbType.String, CCRL_BuyType);
                db.AddInParameter(createCashFlowRecomendationCmd, "@CRPL_ID", DbType.String, CRPL_ID);

                db.ExecuteNonQuery(createCashFlowRecomendationCmd);

                  //if (db.ExecuteNonQuery(createCashFlowRecomendationCmd) != 0)

                  //  customercashrecomendationid = int.Parse(db.GetParameterValue(createCashFlowRecomendationCmd, "CCRL_ID").ToString());

            }

            
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return customercashrecomendationid;
        }


        public DataTable GetCustomerCashFlow(int customerId, bool isIncludeSpouse)
        {
            Database db;
            DbCommand Cmd;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetCustomerBeforeRetCashflowDetails");

                db.AddInParameter(Cmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(Cmd, "@IncludeSpouse", DbType.Boolean, isIncludeSpouse);
                DataSet ds = db.ExecuteDataSet(Cmd);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dt;
        }
        public DataTable GetCustomerCashFlowAfterRetirement(int customerId)
        {
            Database db;
            DbCommand Cmd;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetCustomerAfterRetCasflowDetails");

                db.AddInParameter(Cmd, "@CustomerId", DbType.Int32, customerId);
        
                DataSet ds = db.ExecuteDataSet(Cmd);
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dt;
        }



        public DataSet BindCustomerCashFlowDetails(int CustomerId)
        {
            DataSet dsCustomerCashFlowDetails;
            Database db;
            DbCommand cmdCustomerCashFlowDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerCashFlowDetails = db.GetStoredProcCommand("SP_GETCustomerCashFlowRecommendedList");
                db.AddInParameter(cmdCustomerCashFlowDetails, "@CustomerId", DbType.Int32, CustomerId);
                dsCustomerCashFlowDetails = db.ExecuteDataSet(cmdCustomerCashFlowDetails);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsCustomerCashFlowDetails;
        }



        public DataSet GetCustomerCashFlowDropDownList(int ProductListId)
        {
            Database db;
            DbCommand getDropDownCmd;
            DataSet dsGetDropDownList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getDropDownCmd = db.GetStoredProcCommand("SP_CashFlow_GetRecomendedProductDetails");
                db.AddInParameter(getDropDownCmd, "@ProductListId", DbType.Int32, ProductListId);

                dsGetDropDownList = db.ExecuteDataSet(getDropDownCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }


            return dsGetDropDownList;
        }



         
    }
}
