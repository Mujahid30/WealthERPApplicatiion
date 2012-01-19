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


        public void CreateCustomerGoalPlanning(CustomerGoalPlanningVo GoalPlanningVo,out int goalId)
        {
            Database db;
            DbCommand createCustomerGoalProfileCmd;
            goalId = 0;
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
                if (GoalPlanningVo.IsFundFromAsset==true)
                    db.AddInParameter(createCustomerGoalProfileCmd, "@IsGoalFundFromAsset", DbType.Int16, 1);
                else
                    db.AddInParameter(createCustomerGoalProfileCmd, "@IsGoalFundFromAsset", DbType.Int16,0);

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
                if (GoalPlanningVo.IsOnetimeOccurence == true)
                    db.AddInParameter(createCustomerGoalProfileCmd, "@IsOneTimeOccurrence", DbType.Int16, 1);
                else
                    db.AddInParameter(createCustomerGoalProfileCmd, "@IsOneTimeOccurrence", DbType.Int16, 0);
                if (!string.IsNullOrEmpty(GoalPlanningVo.Frequency))
                    db.AddInParameter(createCustomerGoalProfileCmd, "@GoalFrequency", DbType.String, GoalPlanningVo.Frequency);

                if (GoalPlanningVo.Goalcode == "RT")
                {
                    db.AddInParameter(createCustomerGoalProfileCmd, "@CorpsLeftBehind", DbType.Double, GoalPlanningVo.CorpusLeftBehind);
                }

                db.AddOutParameter(createCustomerGoalProfileCmd, "@GoalId", DbType.Int32, 10000);
                                
                db.ExecuteNonQuery(createCustomerGoalProfileCmd);

                Object objGoalId = db.GetParameterValue(createCustomerGoalProfileCmd, "@GoalId");
                if (objGoalId != DBNull.Value)
                    goalId = (int)db.GetParameterValue(createCustomerGoalProfileCmd, "@GoalId");


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
                if (GoalPlanningVo.IsOnetimeOccurence == true)
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@IsOneTimeOccurance", DbType.Int16, 1);
                else
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@IsOneTimeOccurance", DbType.Int16, 0);
                db.AddInParameter(updateCustomerGoalProfileCmd, "@Priority", DbType.String, GoalPlanningVo.Priority);
                if (!string.IsNullOrEmpty(GoalPlanningVo.Frequency))
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@FrequencyId", DbType.Int16, int.Parse(GoalPlanningVo.Frequency));
                if (GoalPlanningVo.IsFundFromAsset == true)
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@IsGoalFundFromAsset", DbType.Int16, 1);
                else
                    db.AddInParameter(updateCustomerGoalProfileCmd, "@IsGoalFundFromAsset", DbType.Int16, 0);

                db.AddInParameter(updateCustomerGoalProfileCmd, "@CorpsToBeLeftBehind", DbType.Double, GoalPlanningVo.CorpusLeftBehind);

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

        public CustomerAssumptionVo GetCustomerAssumptions(int CustomerID, int adviserId, out bool isHavingAssumption)
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
                db.AddInParameter(allCustomerAssumptionCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddOutParameter(allCustomerAssumptionCmd, "@RTGaolYear", DbType.Int32, 1000);
                db.AddOutParameter(allCustomerAssumptionCmd, "@CustomerAge", DbType.Int32, 1000);
                db.AddOutParameter(allCustomerAssumptionCmd, "@SpouseAge", DbType.Int32, 1000);
                db.AddOutParameter(allCustomerAssumptionCmd, "@SpouseEOL", DbType.Int32, 1000);
                db.AddOutParameter(allCustomerAssumptionCmd, "@isRiskProfileComplete", DbType.Int32, 1000);


                allCustomerAssumptionDs = db.ExecuteDataSet(allCustomerAssumptionCmd);

                Object objRTGoalYear = db.GetParameterValue(allCustomerAssumptionCmd, "@RTGaolYear");
                if (objRTGoalYear != DBNull.Value)
                    customerAssumptionVo.RTGoalYear = (int)db.GetParameterValue(allCustomerAssumptionCmd, "@RTGaolYear");

                Object objCustomerAge = db.GetParameterValue(allCustomerAssumptionCmd, "@CustomerAge");
                if (objCustomerAge != DBNull.Value)
                    customerAssumptionVo.CustomerAge = (int)db.GetParameterValue(allCustomerAssumptionCmd, "@CustomerAge");

                Object objSpouseAge = db.GetParameterValue(allCustomerAssumptionCmd, "@SpouseAge");
                if (objSpouseAge != DBNull.Value)
                    customerAssumptionVo.SpouseAge = (int)db.GetParameterValue(allCustomerAssumptionCmd, "@SpouseAge");

                Object objSpouseEOL = db.GetParameterValue(allCustomerAssumptionCmd, "@SpouseEOL");
                if (objSpouseEOL != DBNull.Value)
                    customerAssumptionVo.SpouseEOL = (int)db.GetParameterValue(allCustomerAssumptionCmd, "@SpouseEOL");

                Object objRiskProfileComplete = db.GetParameterValue(allCustomerAssumptionCmd, "@isRiskProfileComplete");
                if (objRiskProfileComplete != DBNull.Value)
                {
                    int isRiskComplete;
                    isRiskComplete = (int)db.GetParameterValue(allCustomerAssumptionCmd, "@isRiskProfileComplete");
                    if (isRiskComplete == 1)
                        customerAssumptionVo.IsRiskProfileComplete = true;
                    else
                        customerAssumptionVo.IsRiskProfileComplete = false;
                }

                //customerAssumptionVo.RTGoalYear = (int)db.GetParameterValue(allCustomerAssumptionCmd, "@RTGaolYear");

                if ((allCustomerAssumptionDs.Tables[0].Rows.Count) > 0 && (allCustomerAssumptionDs.Tables[1].Rows.Count) > 0)
                    isHavingAssumption = true;
                else
                    isHavingAssumption = false;

                DataTable dtCustomerStaticAssumption = allCustomerAssumptionDs.Tables[0];
                DataTable dtCustomerProjectedAssumption = allCustomerAssumptionDs.Tables[1];
                DataTable dtCustomerFPConfiguration = allCustomerAssumptionDs.Tables[2];
                DataTable dtCustomerAssetAllocation = allCustomerAssumptionDs.Tables[3];

                foreach (DataRow dr in dtCustomerStaticAssumption.Rows)
                {
                    if (Convert.ToString(dr["WA_AssumptionId"]) == "LE")
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
                    switch (Convert.ToString(dr["WA_AssumptionId"]))
                    {
                        case "IR":
                            {
                                customerAssumptionVo.InflationPercent = double.Parse(Convert.ToString(dr["CPA_Value"]));
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
                        case "DR":
                            {
                                customerAssumptionVo.ReturnOnDebt = double.Parse(Convert.ToString(dr["CPA_Value"]));
                                break;
                            }
                        case "ER":
                            {
                                customerAssumptionVo.ReturnOnEquity = double.Parse(Convert.ToString(dr["CPA_Value"]));
                                break;
                            }
                        case "CR":
                            {
                                customerAssumptionVo.ReturnOnCash = double.Parse(Convert.ToString(dr["CPA_Value"]));
                                break;
                            }
                        case "AR":
                            {
                                customerAssumptionVo.ReturnOnAlternate = double.Parse(Convert.ToString(dr["CPA_Value"]));
                                break;
                            }
                    }
                }

                foreach (DataRow dr in dtCustomerFPConfiguration.Rows)
                {
                    switch (Convert.ToInt32(dr["WFPC_CalculationId"].ToString()))
                    {
                        case 1:
                            {
                                if (Convert.ToInt16(dr["WFPCB_CalculationBasisId"].ToString()) == 1)
                                {
                                    customerAssumptionVo.IsCorpusToBeLeftBehind = false;
                                }
                                else
                                {
                                    customerAssumptionVo.IsCorpusToBeLeftBehind = true;
                                }
                                break;
                            }
                        case 3:
                            {
                                if (Convert.ToInt16(dr["WFPCB_CalculationBasisId"].ToString()) == 5)
                                {
                                    customerAssumptionVo.IsGoalFundingFromInvestMapping = true;
                                }
                                else
                                {
                                    customerAssumptionVo.IsGoalFundingFromInvestMapping = false;
                                }
                                break;
                            }
                        case 4:
                            {
                                if (Convert.ToInt16(dr["WFPCB_CalculationBasisId"].ToString()) == 7)
                                {
                                    customerAssumptionVo.IsModelPortfolio = true;
                                }
                                else
                                {
                                    customerAssumptionVo.IsModelPortfolio = false;
                                }
                                break;
                            }

                    }

                }

                if (dtCustomerAssetAllocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomerAssetAllocation.Rows)
                    {
                        if (Convert.ToString(dr["WAC_AssetClassificationCode"]).Trim() == "1")
                        {
                            customerAssumptionVo.WeightedReturn = customerAssumptionVo.WeightedReturn + (Convert.ToDouble(dr["CAA_RecommendedPercentage"]) / 100 * customerAssumptionVo.ReturnOnEquity);
                        }
                        else if (Convert.ToString(dr["WAC_AssetClassificationCode"]).Trim() == "2")
                        {
                            customerAssumptionVo.WeightedReturn = customerAssumptionVo.WeightedReturn + (Convert.ToDouble(dr["CAA_RecommendedPercentage"]) / 100 * customerAssumptionVo.ReturnOnDebt);
                        }
                        else if (Convert.ToString(dr["WAC_AssetClassificationCode"]).Trim() == "3")
                        {
                            customerAssumptionVo.WeightedReturn = customerAssumptionVo.WeightedReturn + (Convert.ToDouble(dr["CAA_RecommendedPercentage"]) / 100 * customerAssumptionVo.ReturnOnCash);

                        }
                        else if (Convert.ToString(dr["WAC_AssetClassificationCode"]).Trim() == "4")
                        {
                            customerAssumptionVo.WeightedReturn = customerAssumptionVo.WeightedReturn + (Convert.ToDouble(dr["CAA_RecommendedPercentage"]) / 100 * customerAssumptionVo.ReturnOnAlternate);

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
                dtGoalDetails = goalDetailsDS.Tables[0];
                if (dtGoalDetails.Rows.Count > 0)
                {
                    GoalPlanningVo.GoalName = dtGoalDetails.Rows[0]["XG_GoalName"].ToString();
                    GoalPlanningVo.Goalcode = dtGoalDetails.Rows[0]["XG_GoalCode"].ToString();
                    if (!string.IsNullOrEmpty(dtGoalDetails.Rows[0]["CG_CostToday"].ToString().Trim()))
                        GoalPlanningVo.CostOfGoalToday = double.Parse(dtGoalDetails.Rows[0]["CG_CostToday"].ToString());
                    if (!string.IsNullOrEmpty(dtGoalDetails.Rows[0]["CG_GoalYear"].ToString().Trim()))
                        GoalPlanningVo.GoalYear = int.Parse(dtGoalDetails.Rows[0]["CG_GoalYear"].ToString());
                    if (dtGoalDetails.Rows[0]["CG_GoalProfileDate"] != null && dtGoalDetails.Rows[0]["CG_GoalProfileDate"].ToString().Trim() != "")
                        GoalPlanningVo.GoalProfileDate = DateTime.Parse(dtGoalDetails.Rows[0]["CG_GoalProfileDate"].ToString());
                    GoalPlanningVo.MonthlySavingsReq = double.Parse(dtGoalDetails.Rows[0]["CG_MonthlySavingsRequired"].ToString());
                    if (!string.IsNullOrEmpty(dtGoalDetails.Rows[0]["CA_AssociateId"].ToString().Trim()))
                        GoalPlanningVo.AssociateId = int.Parse(dtGoalDetails.Rows[0]["CA_AssociateId"].ToString());
                    if (dtGoalDetails.Rows[0]["CG_ExpectedROI"] != null && dtGoalDetails.Rows[0]["CG_ExpectedROI"].ToString().Trim() != "")
                        GoalPlanningVo.ExpectedROI = double.Parse(dtGoalDetails.Rows[0]["CG_ExpectedROI"].ToString());
                    if (dtGoalDetails.Rows[0]["CG_Comments"] != null && dtGoalDetails.Rows[0]["CG_Comments"].ToString().Trim() != "")
                        GoalPlanningVo.Comments = dtGoalDetails.Rows[0]["CG_Comments"].ToString();
                    if (dtGoalDetails.Rows[0]["CG_GoalDescription"] != null && dtGoalDetails.Rows[0]["CG_GoalDescription"].ToString().Trim() != "")
                        GoalPlanningVo.GoalDescription = dtGoalDetails.Rows[0]["CG_GoalDescription"].ToString();
                    if (dtGoalDetails.Rows[0]["CG_CorpusLeftBehind"] != null && dtGoalDetails.Rows[0]["CG_CorpusLeftBehind"].ToString().Trim() != "")
                        GoalPlanningVo.CorpusLeftBehind = double.Parse(dtGoalDetails.Rows[0]["CG_CorpusLeftBehind"].ToString());
                    if (dtGoalDetails.Rows[0]["CG_FVofCostToday"] != null && dtGoalDetails.Rows[0]["CG_FVofCostToday"].ToString().Trim() != "")
                        GoalPlanningVo.FutureValueOfCostToday = double.Parse(dtGoalDetails.Rows[0]["CG_FVofCostToday"].ToString());
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

        public DataSet GetExistingInvestmentDetails(int CustomerID, int goalId)
        {
            Database db;
            DataSet dsGetExistingInvestmentDetails = new DataSet();
            DbCommand getExistingInvestmentDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getExistingInvestmentDetailsCmd = db.GetStoredProcCommand("SP_GetExistingInvestmentDetails");
                db.AddInParameter(getExistingInvestmentDetailsCmd, "@CustomerId", DbType.Int32, CustomerID);
                db.AddInParameter(getExistingInvestmentDetailsCmd, "@goalId", DbType.Int32, goalId);
                getExistingInvestmentDetailsCmd.CommandTimeout = 60 * 60;
                dsGetExistingInvestmentDetails = db.ExecuteDataSet(getExistingInvestmentDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetExistingInvestmentDetails;
        }

        public DataSet GetSIPInvestmentDetails(int CustomerID, int goalId)
        {
            Database db;
            DataSet dsGetSIPInvestmentDetails = new DataSet();
            DbCommand getSIPInvestmentDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSIPInvestmentDetailsCmd = db.GetStoredProcCommand("SP_GetSIPInvestmentDetails");
                db.AddInParameter(getSIPInvestmentDetailsCmd, "@CustomerId", DbType.Int32, CustomerID);
                db.AddInParameter(getSIPInvestmentDetailsCmd, "@goalId", DbType.Int32, goalId);
                dsGetSIPInvestmentDetails = db.ExecuteDataSet(getSIPInvestmentDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetSIPInvestmentDetails;
        }

        public void UpdateGoalAllocationPercentage(decimal allocationPercentage, int schemeId, int goalId, decimal investedAmount)
        {
            Database db;
            DbCommand goalAllocationPercentageCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                goalAllocationPercentageCmd = db.GetStoredProcCommand("SP_UpdateGoalAllocationPercentage");
                db.AddInParameter(goalAllocationPercentageCmd, "@allocationPercentage", DbType.Decimal, allocationPercentage);
                db.AddInParameter(goalAllocationPercentageCmd, "@goalId", DbType.Int32, goalId);
                db.AddInParameter(goalAllocationPercentageCmd, "@schemeId", DbType.Int32, schemeId);
                db.AddInParameter(goalAllocationPercentageCmd, "@investedAmount", DbType.Decimal, investedAmount);
                db.ExecuteNonQuery(goalAllocationPercentageCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        public void UpdateSIPGoalAllocationAmount(decimal allocationAmount, int sipId, int goalId)
        {
            Database db;
            DbCommand SIPGoalAllocationCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SIPGoalAllocationCmd = db.GetStoredProcCommand("SP_UpdateSIPGoalAllocationAmount");
                db.AddInParameter(SIPGoalAllocationCmd, "@allocationAmount", DbType.Decimal, allocationAmount);
                db.AddInParameter(SIPGoalAllocationCmd, "@goalId", DbType.Int32, goalId);
                db.AddInParameter(SIPGoalAllocationCmd, "@sipId", DbType.Int32, sipId);
                db.ExecuteNonQuery(SIPGoalAllocationCmd);



            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        public DataSet BindDDLSchemeAllocated(int customerId, int goalId)
        {
            Database db;
            DbCommand bindDDLSchemeAllocatedCmd;
            DataSet dsBindDDLSchemeAllocated = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                bindDDLSchemeAllocatedCmd = db.GetStoredProcCommand("SP_BindDDLSchemeGoalAllocation");
                db.AddInParameter(bindDDLSchemeAllocatedCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(bindDDLSchemeAllocatedCmd, "@GoalId", DbType.Int32, goalId);
                bindDDLSchemeAllocatedCmd.CommandTimeout = 60 * 60;
                dsBindDDLSchemeAllocated = db.ExecuteDataSet(bindDDLSchemeAllocatedCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsBindDDLSchemeAllocated;
        }

        public DataSet BindDDLSIPSchemeAllocated(int customerId, int goalId)
        {
            Database db;
            DbCommand bindDDLSIPSchemeAllocatedCmd;
            DataSet dsBindDDLSIPSchemeAllocated = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                bindDDLSIPSchemeAllocatedCmd = db.GetStoredProcCommand("SP_BindSIPDDLSchemeGoalAllocation");
                db.AddInParameter(bindDDLSIPSchemeAllocatedCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(bindDDLSIPSchemeAllocatedCmd, "@GoalId", DbType.Int32, goalId);
                dsBindDDLSIPSchemeAllocated = db.ExecuteDataSet(bindDDLSIPSchemeAllocatedCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsBindDDLSIPSchemeAllocated;
        }

        public void DeleteFundedScheme(int schemeId, int goalId)
        {
            Database db;
            DbCommand deleteFundedSchemeCmd;
        
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteFundedSchemeCmd = db.GetStoredProcCommand("SP_DeleteFundedScheme");
                db.AddInParameter(deleteFundedSchemeCmd, "@schemeId", DbType.Int32, schemeId);
                db.AddInParameter(deleteFundedSchemeCmd, "@GoalId", DbType.Int32, goalId);
                db.ExecuteNonQuery(deleteFundedSchemeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
           
        }

        public void DeleteSIPFundedScheme(int sipId, int goalId)
        {
            Database db;
            DbCommand deleteSIPFundedSchemeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteSIPFundedSchemeCmd = db.GetStoredProcCommand("SP_DeleteSIPFundedScheme");
                db.AddInParameter(deleteSIPFundedSchemeCmd, "@sipId", DbType.Int32, sipId);
                db.AddInParameter(deleteSIPFundedSchemeCmd, "@GoalId", DbType.Int32, goalId);
                db.ExecuteNonQuery(deleteSIPFundedSchemeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
    }
}
