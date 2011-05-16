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


        public void UpdateFPProjectionAssetAllocation(int customerId, int tempYear, decimal equityAgreedAssetAllocation, decimal debtAgreedAssetAllocation, decimal cashAgreedAssetAllocation, decimal alternateAgreedAssetAllocation)
        {
            Database db;
            DbCommand updateAssetAllocationCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAssetAllocationCmd = db.GetStoredProcCommand("SP_UpdateFPProjectionAssetAllocation");
                db.AddInParameter(updateAssetAllocationCmd, "@customerId", DbType.Int32, customerId);
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


        public void UpdateFutureSavingProjection(int customerId, int advisorId, decimal equityFutureAllocation, decimal debtFutureAllocation, decimal cashFutureAllocation, decimal alternateFutureAllocation, int tempYear)
        {
            Database db;
            DbCommand updateFutureSavingCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateFutureSavingCmd = db.GetStoredProcCommand("SP_UpdateFutureSavingProjection");
                db.AddInParameter(updateFutureSavingCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(updateFutureSavingCmd, "@adviserId", DbType.Int32, advisorId);
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
         
    }
}
