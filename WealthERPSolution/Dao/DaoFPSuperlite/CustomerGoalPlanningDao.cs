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
using VoFPSuperlite;

namespace DaoFPSuperlite
{
    public class CustomerGoalPlanningDao
    {
        public DataSet GetGoalObjectiveTypes()
        {
            Database db;
            DbCommand getGoalObjTypeCmd;
            DataSet getGoalObjTypeDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGoalObjTypeCmd = db.GetStoredProcCommand("SP_GetGoalName");
                getGoalObjTypeDs = db.ExecuteDataSet(getGoalObjTypeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalPlanningDao:GetGoalObjectiveTypes()");


                object[] objects = new object[1];
                objects[0] = "SP_GetGoalName";

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getGoalObjTypeDs;
        }

        public DataSet GetAllGoalDropDownDetails(int CustomerID)
        {
            Database db;
            DbCommand allGoalDropDownDetailsCmd;
            DataSet allGoalDropDownDetailsDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                allGoalDropDownDetailsCmd = db.GetStoredProcCommand("SP_GetAllGoalDropDownDetails");
                db.AddInParameter(allGoalDropDownDetailsCmd, "@Customer_ID", DbType.Int32, CustomerID);
                allGoalDropDownDetailsDs = db.ExecuteDataSet(allGoalDropDownDetailsCmd);
                allGoalDropDownDetailsDs.Tables[0].TableName = "GoalObjective";
                allGoalDropDownDetailsDs.Tables[1].TableName = "ChildDetails";
                allGoalDropDownDetailsDs.Tables[2].TableName = "GoalFrequency";
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetCustomerAssociationDetails()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return allGoalDropDownDetailsDs;
        }


        public void CreateCustomerGoalPlanning(CustomerGoalPlanningVo GoalPlanningVo, int UserId)
        {
            Database db;
            DbCommand createCustomerGoalProfileCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerGoalProfileCmd = db.GetStoredProcCommand("SP_CreateCustomerGoalPlanning");
                db.AddInParameter(createCustomerGoalProfileCmd, "@CustomerId", DbType.Int32, GoalPlanningVo.CustomerId);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalCode", DbType.String, GoalPlanningVo.Goalcode);
                db.AddInParameter(createCustomerGoalProfileCmd, "@CostToday", DbType.Double, GoalPlanningVo.CostOfGoalToday);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalYear", DbType.Int32, GoalPlanningVo.GoalYear);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalProfileDate", DbType.DateTime, GoalPlanningVo.GoalDate);
                db.AddInParameter(createCustomerGoalProfileCmd, "@FutureValueOfCostToday", DbType.Double, GoalPlanningVo.FutureValueOfCostToday);
                db.AddInParameter(createCustomerGoalProfileCmd, "@MonthlySavingsRequired", DbType.Double, GoalPlanningVo.MonthlySavingsReq);
                if (GoalPlanningVo.AssociateId != 0)
                {
                    db.AddInParameter(createCustomerGoalProfileCmd, "@AssociateId", DbType.Int32, GoalPlanningVo.AssociateId);
                }
                db.AddInParameter(createCustomerGoalProfileCmd, "@ROIEarned", DbType.Double, GoalPlanningVo.ROIEarned);
                db.AddInParameter(createCustomerGoalProfileCmd, "@CurrentInvestment", DbType.Double, GoalPlanningVo.CurrInvestementForGoal);
                db.AddInParameter(createCustomerGoalProfileCmd, "@ExpectedROI", DbType.Double, GoalPlanningVo.ExpectedROI);
                db.AddInParameter(createCustomerGoalProfileCmd, "@IsActive", DbType.Int16, GoalPlanningVo.IsActice);
                db.AddInParameter(createCustomerGoalProfileCmd, "@InflationPer", DbType.Double, GoalPlanningVo.InflationPercent);
                if (GoalPlanningVo.CustomerApprovedOn != DateTime.Parse("01/01/0001 00:00:00"))
                {
                    db.AddInParameter(createCustomerGoalProfileCmd, "@CustomerApprovedOn", DbType.DateTime, GoalPlanningVo.CustomerApprovedOn);
                }
                db.AddInParameter(createCustomerGoalProfileCmd, "@Comments", DbType.String, GoalPlanningVo.Comments);
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalDescription", DbType.String, GoalPlanningVo.GoalDescription);
                db.AddInParameter(createCustomerGoalProfileCmd, "@ROIOnFuture", DbType.Double, GoalPlanningVo.RateofInterestOnFture);
                db.AddInParameter(createCustomerGoalProfileCmd, "@CreatedBy", DbType.Int32, GoalPlanningVo.CreatedBy);
                db.AddInParameter(createCustomerGoalProfileCmd, "@LumpsumInvestmentRequired", DbType.Double, GoalPlanningVo.LumpsumInvestRequired);
                db.AddInParameter(createCustomerGoalProfileCmd, "@FutureValueOnCurrentInvest", DbType.Double, GoalPlanningVo.FutureValueOnCurrentInvest);
              

                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalPriority", DbType.String, GoalPlanningVo.Priority);
                if (GoalPlanningVo.IsOnetimeOccurence==true)
                db.AddInParameter(createCustomerGoalProfileCmd, "@IsOneTimeOccurrence", DbType.Int16, 1);
                else
                db.AddInParameter(createCustomerGoalProfileCmd, "@IsOneTimeOccurrence", DbType.Int16, 0);
                if (!string.IsNullOrEmpty(GoalPlanningVo.Frequency))
                db.AddInParameter(createCustomerGoalProfileCmd, "@GoalFrequency", DbType.String, GoalPlanningVo.Frequency);
                
                
                
                db.ExecuteNonQuery(createCustomerGoalProfileCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalPlanningDao.cs:CreateCustomerGoalProfile()");


                object[] objects = new object[1];
                objects[0] = GoalPlanningVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }
    }
}
