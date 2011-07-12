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


        public void CreateCustomerGoalPlanning(CustomerGoalPlanningVo GoalPlanningVo)
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
                if (GoalPlanningVo.GoalDate != DateTime.MinValue)
                    db.AddInParameter(createCustomerGoalProfileCmd, "@GoalProfileDate", DbType.DateTime, GoalPlanningVo.GoalDate);
                else
                    GoalPlanningVo.GoalDate = DateTime.Now;
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
                if (GoalPlanningVo.CustomerApprovedOn != DateTime.MinValue)
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

                if (GoalPlanningVo.Goalcode=="RT")
                {
                    db.AddInParameter(createCustomerGoalProfileCmd, "@CorpsLeftBehind", DbType.Double, GoalPlanningVo.CorpusLeftBehind);
                }                
                
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


        public void UpdateCustomerGoalProfile(CustomerGoalPlanningVo GoalPlanningVo)
        {
            Database db;
            DbCommand updateCustomerGoalProfileCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCustomerGoalProfileCmd = db.GetStoredProcCommand("SP_UpdateCustomerGoalProfile");
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalId", DbType.Int32, GoalPlanningVo.GoalId);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CustomerId", DbType.Int32, GoalPlanningVo.CustomerId);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalCode", DbType.String, GoalPlanningVo.Goalcode);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CostToday", DbType.Double, GoalPlanningVo.CostOfGoalToday);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@FutureValueOfCostToday", DbType.Double, GoalPlanningVo.FutureValueOfCostToday);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalYear", DbType.Int32, GoalPlanningVo.GoalYear);
                if (GoalPlanningVo.GoalDate != DateTime.MinValue)
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalProfileDate", DbType.DateTime, GoalPlanningVo.GoalDate);
                else
                    GoalPlanningVo.GoalDate = DateTime.Now;
                db.AddInParameter(updateCustomerGoalProfileCmd, "@MonthlySavingsRequired", DbType.Double, GoalPlanningVo.MonthlySavingsReq);
                if (GoalPlanningVo.AssociateId != 0)
                {
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@AssociateId", DbType.Int32, GoalPlanningVo.AssociateId);
                }
                db.AddInParameter(updateCustomerGoalProfileCmd, "@ROIEarned", DbType.Double, GoalPlanningVo.ROIEarned);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CurrentInvestment", DbType.Double, GoalPlanningVo.CurrInvestementForGoal);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@ExpectedROI", DbType.Double, GoalPlanningVo.ExpectedROI);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@IsActive", DbType.Int16, GoalPlanningVo.IsActice);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@InflationPer", DbType.Double, GoalPlanningVo.InflationPercent);
                if (GoalPlanningVo.CustomerApprovedOn != DateTime.MinValue)
                {
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@CustomerApprovedOn", DbType.DateTime, GoalPlanningVo.CustomerApprovedOn);
                }
                db.AddInParameter(updateCustomerGoalProfileCmd, "@Comments", DbType.String, GoalPlanningVo.Comments);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@GoalDescription", DbType.String, GoalPlanningVo.GoalDescription);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@ROIOnFuture", DbType.Double, GoalPlanningVo.RateofInterestOnFture);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@CreatedBy", DbType.Int32, GoalPlanningVo.CreatedBy);
                if (GoalPlanningVo.IsOnetimeOccurence==true)
                db.AddInParameter(updateCustomerGoalProfileCmd, "@IsOneTimeOccurance", DbType.Int16, 1);
                else
                db.AddInParameter(updateCustomerGoalProfileCmd, "@IsOneTimeOccurance", DbType.Int16, 0);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@Priority", DbType.String, GoalPlanningVo.Priority);
                if(!string.IsNullOrEmpty(GoalPlanningVo.Frequency))
                db.AddInParameter(updateCustomerGoalProfileCmd, "@FrequencyId", DbType.Int16, int.Parse(GoalPlanningVo.Frequency));

                db.ExecuteNonQuery(updateCustomerGoalProfileCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoalProfileSetupDao.cs:UpdateCustomerGoalProfile()");


                object[] objects = new object[2];
                objects[0] = GoalPlanningVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }

        public CustomerAssumptionVo GetAllCustomerAssumption(int CustomerID,int goalYear,out bool isHavingAssumption)
        {
            Database db;
            DbCommand allCustomerAssumptionCmd;
            DataSet allCustomerAssumptionDs;
            CustomerAssumptionVo customerAssumptionVo = new CustomerAssumptionVo(); 
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                allCustomerAssumptionCmd = db.GetStoredProcCommand("SP_GetCustomerAssumptionForGoalSetup");
                db.AddInParameter(allCustomerAssumptionCmd, "@CustomerId", DbType.Int32, CustomerID);
                db.AddInParameter(allCustomerAssumptionCmd, "@GoalYear", DbType.Int32, goalYear);
                db.AddOutParameter(allCustomerAssumptionCmd, "@RTGaolYear", DbType.Int32, 1000);
                allCustomerAssumptionDs = db.ExecuteDataSet(allCustomerAssumptionCmd);

                customerAssumptionVo.RTGoalYear = (int)db.GetParameterValue(allCustomerAssumptionCmd, "@RTGaolYear");

                if ((allCustomerAssumptionDs.Tables[0].Rows.Count) > 0 && (allCustomerAssumptionDs.Tables[0].Rows.Count) > 0)
                    isHavingAssumption = true;
                else
                    isHavingAssumption = false;               

                DataTable dtCustomerStaticAssumption = allCustomerAssumptionDs.Tables[0];
                DataTable dtCustomerProjectedAssumption = allCustomerAssumptionDs.Tables[1];
                foreach (DataRow dr in dtCustomerStaticAssumption.Rows)
                {
                    if(Convert.ToString(dr["WA_AssumptionId"])=="LE")
                    {
                        customerAssumptionVo.CustomerEOL = int.Parse(Math.Round(double.Parse(dr["CSA_Value"].ToString()), 0).ToString());

                    }
                    else if (Convert.ToString(dr["WA_AssumptionId"]) == "RA")
                    {
                        customerAssumptionVo.RetirementAge = int.Parse(Math.Round(double.Parse(dr["CSA_Value"].ToString()), 0).ToString());

                    }
 
                }
                foreach (DataRow dr in dtCustomerProjectedAssumption.Rows)
                {
                    switch(Convert.ToString(dr["WA_AssumptionId"]))
                    {
                        case "IR":
                            {
                               customerAssumptionVo.InflationPercent=double.Parse(Convert.ToString(dr["CPA_Value"]));
                               break;
                            }                        
                        case "PRT":
                            {
                                customerAssumptionVo.PostRetirementReturn = double.Parse(Convert.ToString(dr["CPA_Value"]));
                                break;
                            }
                        case "RNI":
                            {
                                customerAssumptionVo.ReturnOnNewInvestment = double.Parse(Convert.ToString(dr["CPA_Value"]));
                                break;
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

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetAllCustomerAssumption()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerAssumptionVo;
            
        }

        public DataSet GetCustomerGoalDetails(int CustomerID)
        {
            Database db;
            DbCommand customerGoalDetailsCmd;
            DataSet customerGoalDetailsDS;
          
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                customerGoalDetailsCmd = db.GetStoredProcCommand("SP_GetCustomersAllGoalDetails");
                db.AddInParameter(customerGoalDetailsCmd, "@CustomerId", DbType.Int32, CustomerID);
                customerGoalDetailsDS = db.ExecuteDataSet(customerGoalDetailsCmd);                

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalPlanningDao:GetCustomerGoalDetails()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerGoalDetailsDS;

        }

        public DataSet GetCustomerGoalList(int CustomerID)
        {
            Database db;
            DbCommand customerGoalListCmd;
            DataSet customerGoalListDS;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                customerGoalListCmd = db.GetStoredProcCommand("SP_GetCustomerGoalList");
                db.AddInParameter(customerGoalListCmd, "@CustomerId", DbType.Int32, CustomerID);
                customerGoalListDS = db.ExecuteDataSet(customerGoalListCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalPlanningDao:GetCustomerGoalList()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerGoalListDS;

        }

        public CustomerGoalPlanningVo GetGoalDetails(int goalId)
        {
            Database db;
            DbCommand goalDetailsCmd;
            DataSet goalDetailsDS;
            DataTable dtGoalDetails;
            CustomerGoalPlanningVo GoalPlanningVo = new CustomerGoalPlanningVo();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                goalDetailsCmd = db.GetStoredProcCommand("SP_GetGoalDetails");
                db.AddInParameter(goalDetailsCmd, "@GoalId", DbType.Int32, goalId);
                goalDetailsDS = db.ExecuteDataSet(goalDetailsCmd);
                dtGoalDetails=goalDetailsDS.Tables[0];
                if (dtGoalDetails.Rows.Count > 0)
                {
                    GoalPlanningVo.Goalcode = dtGoalDetails.Rows[0]["XG_GoalCode"].ToString();
                    GoalPlanningVo.CostOfGoalToday =double.Parse(dtGoalDetails.Rows[0]["CG_CostToday"].ToString());
                    GoalPlanningVo.GoalYear = int.Parse(dtGoalDetails.Rows[0]["CG_GoalYear"].ToString());
                    GoalPlanningVo.GoalProfileDate =DateTime.Parse(dtGoalDetails.Rows[0]["CG_GoalProfileDate"].ToString());
                    GoalPlanningVo.MonthlySavingsReq =double.Parse(dtGoalDetails.Rows[0]["CG_MonthlySavingsRequired"].ToString());
                    if(!string.IsNullOrEmpty(dtGoalDetails.Rows[0]["CA_AssociateId"].ToString()))
                    GoalPlanningVo.AssociateId =int.Parse(dtGoalDetails.Rows[0]["CA_AssociateId"].ToString());
                    GoalPlanningVo.ExpectedROI =double.Parse(dtGoalDetails.Rows[0]["CG_ExpectedROI"].ToString());
                    GoalPlanningVo.Comments = dtGoalDetails.Rows[0]["CG_Comments"].ToString();
                    GoalPlanningVo.GoalDescription = dtGoalDetails.Rows[0]["CG_GoalDescription"].ToString();
                    if (int.Parse(dtGoalDetails.Rows[0]["CG_IsOnetimeOccurence"].ToString()) == 1)
                    {
                        GoalPlanningVo.IsOnetimeOccurence = true;

                    }
                    else
                    {
                        GoalPlanningVo.IsOnetimeOccurence = false;
                        GoalPlanningVo.Frequency = dtGoalDetails.Rows[0]["XGF_GoalFrequencyId"].ToString();
                    }

                    GoalPlanningVo.Priority = dtGoalDetails.Rows[0]["CG_Priority"].ToString();                    

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

                FunctionInfo.Add("Method", "CustomerGoalPlanningDao:GetCustomerGoalList()");


                object[] objects = new object[1];
                objects[0] = goalId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return GoalPlanningVo;

        }

        public DataTable GetCustomerFPCalculationBasis(int customerId)
        {
            Database db;
            DbCommand fpCalculationBasisCmd;            
            DataTable dtFPCalculationBasis;
            DataSet dsFPCalculationBasis;
             try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                fpCalculationBasisCmd = db.GetStoredProcCommand("SP_CustomerFPCalculationBasis");
                db.AddInParameter(fpCalculationBasisCmd, "@CustomerId", DbType.Int32, customerId);
                dsFPCalculationBasis = db.ExecuteDataSet(fpCalculationBasisCmd);
                dtFPCalculationBasis = dsFPCalculationBasis.Tables[0];
            }
             catch (BaseApplicationException Ex)
             {
                 throw Ex;
             }
             catch (Exception Ex)
             {
                 BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                 NameValueCollection FunctionInfo = new NameValueCollection();

                 FunctionInfo.Add("Method", "CustomerGoalPlanningDao:GetCustomerFPCalculationBasis()");


                 object[] objects = new object[1];
                 objects[0] = customerId;

                 FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                 exBase.AdditionalInformation = FunctionInfo;
                 ExceptionManager.Publish(exBase);
                 throw exBase;

             }
             return dtFPCalculationBasis;
 
        }

        public void CreateCustomerGoalFunding(int goalId, decimal equityAllocatedAmount, decimal debtAllocatedAmount, decimal cashAllocatedAmount, decimal alternateAllocatedAmount, int isloanFunded, decimal loanAmount, DateTime loanStartDate)
        {
            Database db;
            DbCommand CreateCustomerGoalFundingCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateCustomerGoalFundingCmd = db.GetStoredProcCommand("SP_GetCustomerGoalFunding");
                db.AddInParameter(CreateCustomerGoalFundingCmd, "@goalId", DbType.Int32, goalId);
                db.AddInParameter(CreateCustomerGoalFundingCmd, "@equityAllocatedAmount", DbType.Decimal, equityAllocatedAmount);
                db.AddInParameter(CreateCustomerGoalFundingCmd, "@debtAllocatedAmount", DbType.Decimal, debtAllocatedAmount);
                db.AddInParameter(CreateCustomerGoalFundingCmd, "@cashAllocatedAmount", DbType.Decimal, cashAllocatedAmount);
                db.AddInParameter(CreateCustomerGoalFundingCmd, "@alternateAllocatedAmount", DbType.Decimal, alternateAllocatedAmount);
                db.AddInParameter(CreateCustomerGoalFundingCmd, "@isloanFunded", DbType.Int16, isloanFunded);
                db.AddInParameter(CreateCustomerGoalFundingCmd, "@loanAmount", DbType.Decimal, loanAmount);
                if (loanStartDate != DateTime.MinValue)
                    db.AddInParameter(CreateCustomerGoalFundingCmd, "@loanStartDate", DbType.DateTime, loanStartDate);
                else
                    db.AddInParameter(CreateCustomerGoalFundingCmd, "@loanStartDate", DbType.DateTime, DBNull.Value);
                db.ExecuteNonQuery(CreateCustomerGoalFundingCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void DeleteCustomerGoalFunding(int goalId, int customerId)
        {
            Database db;
            DbCommand DeleteCustomerGoalFundingCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteCustomerGoalFundingCmd = db.GetStoredProcCommand("SP_DeleteGoalFunding");
                db.AddInParameter(DeleteCustomerGoalFundingCmd, "@GoalId", DbType.Int32, goalId);
                db.AddInParameter(DeleteCustomerGoalFundingCmd, "@CustomerId", DbType.Int32, customerId);
                db.ExecuteNonQuery(DeleteCustomerGoalFundingCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool CheckCustomerAssumptionExists(int CustomerID)
        {
            Database db;
            DbCommand checkCustomerAssumptionCmd;
            CustomerAssumptionVo customerAssumptionVo = new CustomerAssumptionVo();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkCustomerAssumptionCmd = db.GetStoredProcCommand("SP_CheckCustomerAssumptionExists");
                db.AddInParameter(checkCustomerAssumptionCmd, "@CustomerId", DbType.Int32, CustomerID);
                db.ExecuteNonQuery(checkCustomerAssumptionCmd);

                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupDao:GetAllCustomerAssumption()");


                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return true;
        }
    }
}
