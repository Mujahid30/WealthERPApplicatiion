using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using DaoFPSuperlite;
using VoFPSuperlite;


namespace BoFPSuperlite
{
    public class CustomerGoalPlanningBo
    {
        public DataSet GetGoalObjectiveTypes()
        {
            DataSet dsGoalObjectiveTypes;
            CustomerGoalPlanningDao daoGoalPlanning = new CustomerGoalPlanningDao();

            try
            {
                dsGoalObjectiveTypes = daoGoalPlanning.GetGoalObjectiveTypes();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:GetCustomerGoalProfiling()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGoalObjectiveTypes;

        }

        public DataSet GetAllGoalDropDownDetails(int CustomerID)
        {
            DataSet dsGetCustomerAssociationDetails = new DataSet();
            CustomerGoalPlanningDao AssociationDetails = new CustomerGoalPlanningDao();

            try
            {
                dsGetCustomerAssociationDetails = AssociationDetails.GetAllGoalDropDownDetails(CustomerID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:GetCustomerGoalProfiling()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerAssociationDetails;


        }


        public void CreateCustomerGoalPlanning(CustomerGoalPlanningVo goalPlanningVo, int UserId, int Flag)
        {
            try
            {
                CustomerGoalPlanningVo customerGoalPlanningVo = new CustomerGoalPlanningVo();
               
                CustomerGoalPlanningDao customerGoalPlanningDao = new CustomerGoalPlanningDao();
                CustomerAssumptionVo customerAssumptionVo = new CustomerAssumptionVo();
                customerAssumptionVo = customerGoalPlanningDao.GetAllCustomerAssumption(goalPlanningVo.CustomerId, goalPlanningVo.GoalYear);
                if (goalPlanningVo.Goalcode!="RT")
                customerGoalPlanningVo = CalculateGoalProfile(goalPlanningVo, customerAssumptionVo);
                else
                customerGoalPlanningVo = CalculateGoalProfileRT(goalPlanningVo, customerAssumptionVo);

                if (Flag == 0)
                    customerGoalPlanningDao.CreateCustomerGoalPlanning(customerGoalPlanningVo, UserId);
                //else
                //    // when the flag value 1 at this time userId IS GoalId
                //    customerGoalSetupDao.UpdateCustomerGoalProfile(CreateGoalProfileVo, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerGoalPlanningBo.cs:CreateCustomerGoalPlanning()");
                object[] objects = new object[2];
                objects[0] = goalPlanningVo;
                objects[1] = UserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


        }


        public CustomerGoalPlanningVo CalculateGoalProfile(CustomerGoalPlanningVo goalPlanningVo, CustomerAssumptionVo customerAssumptionVo)
        {
            double futureInvValue = 0;
            double futureCost = 0;
            double requiredSavings = 0;
            double costToday = 0;
            double requiredAfter = 0;
            double currentValue = 0;
            double rateEarned = 0;
            double rateOfReturn = 0;
            double inflationValues = 0;
            string goal = string.Empty;
            double InflationPercent = 8;
            
            try
            {
                costToday = goalPlanningVo.CostOfGoalToday;
                // requiredAfter = GoalProfileVo.GoalYear;
                requiredAfter = goalPlanningVo.GoalYear - DateTime.Today.Year;
                //currentValue = goalPlanningVo.CurrInvestementForGoal;
                //rateEarned = goalPlanningVo.ROIEarned / 100;
                //rateOfReturn = goalPlanningVo.ExpectedROI / 100;
                inflationValues = (Double)customerAssumptionVo.InflationPercent / 100;

                
                futureCost = Math.Abs(FutureValue(inflationValues, requiredAfter, 0, costToday, 0));
                if (currentValue == 0 && rateEarned == 0)
                {
                    futureInvValue = 0;

                }
                else
                    futureInvValue = Math.Abs(FutureValue(rateEarned, requiredAfter, 0, currentValue, 0));

                requiredSavings = Math.Abs(PMT((rateOfReturn / 12), (requiredAfter * 12), 0, (futureCost - futureInvValue), 0));
                goalPlanningVo.MonthlySavingsReq =Math.Round(requiredSavings,2);
                goalPlanningVo.InflationPercent = (Double)InflationPercent;
                goalPlanningVo.FutureValueOfCostToday =Math.Round((Double)futureCost,2);

                return goalPlanningVo;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerGoalPlanningBo.cs:CalculateGoalProfile()");
                object[] objects = new object[1];
                objects[0] = goalPlanningVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }


        public CustomerGoalPlanningVo CalculateGoalProfileRT(CustomerGoalPlanningVo goalPlanningVo, CustomerAssumptionVo customerAssumptionVo)
        {
           
            double yearsLeftForRetirement = customerAssumptionVo.RetirementAge - customerAssumptionVo.CustomerAge;
            double amountAfterFirstMonthRetirement;
            double spouseLifeAfterCustomerRet;
            double adjustedInfluation;
            double corpusReqAtTimeOfRetirement;
            double amountToBeSavedPerMonth;

            try
            {
                amountAfterFirstMonthRetirement = FutureValue(customerAssumptionVo.InflationPercent / 100, yearsLeftForRetirement, 0, -goalPlanningVo.CostOfGoalToday, 1);
                spouseLifeAfterCustomerRet = customerAssumptionVo.SpouseEOL - customerAssumptionVo.RetirementAge + (customerAssumptionVo.CustomerAge - customerAssumptionVo.RetirementAge);
                adjustedInfluation = ((1 + customerAssumptionVo.PostRetirementReturn / 100) / (1 + customerAssumptionVo.InflationPercent / 100) - 1) * 100;
                if (customerAssumptionVo.CorpusToBeLeftBehind != 0)
                {
                    corpusReqAtTimeOfRetirement = PV(adjustedInfluation / 1200, spouseLifeAfterCustomerRet * 12, -amountAfterFirstMonthRetirement, 0, 1);
                }
                else
                {
                    corpusReqAtTimeOfRetirement = PV(adjustedInfluation / 1200, spouseLifeAfterCustomerRet * 12, -amountAfterFirstMonthRetirement, -customerAssumptionVo.CorpusToBeLeftBehind, 1);
                }
                amountToBeSavedPerMonth = PMT(customerAssumptionVo.ReturnOnNewInvestment / 1200, yearsLeftForRetirement * 12, 0, -corpusReqAtTimeOfRetirement, 1);
                goalPlanningVo.MonthlySavingsReq = amountToBeSavedPerMonth;
                goalPlanningVo.CorpusLeftBehind = customerAssumptionVo.CorpusToBeLeftBehind;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalPlanningBo.cs:CalculateGoalProfileRT()");


                object[] objects = new object[2];
                objects[0] = goalPlanningVo;
                objects[1] = customerAssumptionVo;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return goalPlanningVo;

        }

        
        /// <summary>/// Calculates the present value of a loan based upon constant payments and a constant interest rate.
        /// </summary>
        /// <param name="rate">The interest rate.</param>
        /// <param name="nper">Total number of payments.</param>
        /// <param name="pmt">payment made each period</param>
        /// <param name="fv">Future value.</param>
        /// <param name="type">Indicates when payments are due. 0 = end of period, 1 = beginning of period.</param>
        /// <returns>The Present Value</returns>
        public static double PV(double rate, double nper, double pmt, double fv, double type)
        {
            if (rate == 0.0)
                return (-fv - (pmt * nper));
            else
                return (pmt * (1.0 + rate * type) * (1.0 - Math.Pow((1.0 + rate), nper)) / rate - fv) / Math.Pow((1.0 + rate), nper);
        }

        public double FutureValue(double rate, double nper, double pmt, double pv, int type)
        {
            double result = 0;
            result = System.Numeric.Financial.Fv(rate, nper, pmt, pv, 0);
            return result;
        }

        public double PMT(double rate, double nper, double pv, double fv, int type)
        {
            double result = 0;
            result = System.Numeric.Financial.Pmt(rate, nper, pv, fv, 0);
            return result;
        }


        public DataSet GetCustomerGoalDetails(int CustomerID)
        {
            CustomerGoalPlanningDao customerGoalPlanningDao = new CustomerGoalPlanningDao();
            DataSet customerGoalDetailsDS;

            try
            {
                customerGoalDetailsDS = customerGoalPlanningDao.GetCustomerGoalDetails(CustomerID);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalPlanningBo:GetCustomerGoalDetails()");

                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerGoalDetailsDS;

        }

    }
}
