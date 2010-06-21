using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

using System.Data;

using Microsoft.ApplicationBlocks.ExceptionManagement;

using VoUser;
using VoCustomerGoalProfiling;
using DaoCustomerGoalProfiling;


namespace BoCustomerGoalProfiling
{
    public class CustomerGoalSetupBo
    {

        public DataSet GetCustomerGoalProfiling()
        {
            DataSet dsGetCustomerGoalProfiling = new DataSet();
            CustomerGoalSetupDao GaolProfiling = new CustomerGoalSetupDao();

            try
            {
                dsGetCustomerGoalProfiling = GaolProfiling.GetCustomerGoalProfiling();
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
            return dsGetCustomerGoalProfiling;

        }
        public DataSet GetCustomerAssociationDetails(int CustomerID)
        {
            DataSet dsGetCustomerAssociationDetails = new DataSet();
            CustomerGoalSetupDao AssociationDetails = new CustomerGoalSetupDao();

            try
            {
                dsGetCustomerAssociationDetails = AssociationDetails.GetCustomerAssociationDetails(CustomerID);
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
        public GoalProfileSetupVo CalculateGoalProfile(GoalProfileSetupVo GoalProfileVo) 
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
            CustomerGoalSetupDao CustomerGoalDao = new CustomerGoalSetupDao();
            decimal InflationPercent = CustomerGoalDao.GetInflationPercent();

            try
            {
                costToday = GoalProfileVo.CostOfGoalToday;
                // requiredAfter = GoalProfileVo.GoalYear;
                requiredAfter = GoalProfileVo.GoalYear - DateTime.Today.Year;
                currentValue = GoalProfileVo.CurrInvestementForGoal;
                rateEarned = GoalProfileVo.ROIEarned / 100;
                rateOfReturn = GoalProfileVo.ExpectedROI / 100;
                inflationValues = (Double)InflationPercent / 100;

                futureCost = Math.Abs(FutureValue(inflationValues, requiredAfter, 0, costToday, 0));
                if (currentValue == 0 && rateEarned == 0)
                {
                    futureInvValue = 0;

                }
                else
                    futureInvValue = Math.Abs(FutureValue(rateEarned, requiredAfter, 0, currentValue, 0));

                requiredSavings = Math.Abs(PMT((rateOfReturn / 12), (requiredAfter * 12), 0, (futureCost - futureInvValue), 0));
                GoalProfileVo.MonthlySavingsReq = requiredSavings;
                GoalProfileVo.InflationPercent = (Double)InflationPercent;

                return GoalProfileVo;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:CalculateGoalProfile()");


                object[] objects = new object[1];
                objects[0] = GoalProfileVo;
              


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
          
        }
        public GoalProfileSetupVo CalculateGoalProfileRT(GoalProfileSetupVo GoalProfileVo)
        {
            CustomerGoalSetupDao CustomerGoalDao = new CustomerGoalSetupDao();
            int yearsLeft = 0;
            int targetRetirementYear = 0;
            double amountRequired = 0;
            double futureValue = 0;
            double retirementCorpus = 0;
            double monthlySavings = 0;
            double InflationPercent = (Double)CustomerGoalDao.GetInflationPercent();
            double InflationValue = InflationPercent / 100;
            
            try
            {
                double annualRequirement = GoalProfileVo.CostOfGoalToday;
                double currentValue = GoalProfileVo.CurrInvestementForGoal;
                double exRate = GoalProfileVo.ROIEarned / 100;
                double newRate = GoalProfileVo.ExpectedROI / 100;
                double retRetirementCorpus = GoalProfileVo.RateofInterestOnFture / 100;

                if (GoalProfileVo.GoalYear != 0)
                {
                    targetRetirementYear = GoalProfileVo.GoalYear;
                    yearsLeft = targetRetirementYear - DateTime.Now.Year;
                }
                amountRequired = annualRequirement / retRetirementCorpus;
                if (currentValue == 0 && exRate == 0)
                {
                    futureValue = 0;

                }
                else
                  futureValue = Math.Abs(FutureValue(exRate, yearsLeft, 0, currentValue, 0));

                retirementCorpus = Math.Abs(FutureValue(InflationValue, yearsLeft, 0, amountRequired, 0));
                monthlySavings = Math.Abs(PMT((newRate / 12), (yearsLeft * 12), 0, (retirementCorpus - futureValue), 0));

                GoalProfileVo.RetirementCorpus = retirementCorpus;
                GoalProfileVo.MonthlySavingsReq = monthlySavings;
                GoalProfileVo.InflationPercent = InflationPercent;

                return GoalProfileVo;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:CreateCustomerGoalProfileForRetirement()");


                object[] objects = new object[1];
                objects[0] = GoalProfileVo;
               


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
 
        }
       

        public void CreateCustomerGoalProfile(GoalProfileSetupVo GoalProfileVo, int UserId, int Flag)
        {
            try 
            {
                GoalProfileSetupVo CreateGoalProfileVo = new GoalProfileSetupVo();
                CreateGoalProfileVo = CalculateGoalProfile(GoalProfileVo);
                CustomerGoalSetupDao customerGoalSetupDao = new CustomerGoalSetupDao();
                if (Flag == 0)
                    customerGoalSetupDao.CreateCustomerGoalProfile(CreateGoalProfileVo, UserId);
                else
                    // when the flag value 1 at this time userId IS GoalId
                    customerGoalSetupDao.UpdateCustomerGoalProfile(CreateGoalProfileVo, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:CreateCustomerGoalProfile()");


                object[] objects = new object[2];
                objects[0] = GoalProfileVo;
                objects[1] = UserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

           
        }
        public void CreateCustomerGoalProfileForRetirement(GoalProfileSetupVo GoalProfileVo, int UserId, int Flag)
        {
                       
            try
            {
                GoalProfileSetupVo CreateGoalProfileVo = new GoalProfileSetupVo();
                CreateGoalProfileVo = CalculateGoalProfileRT(GoalProfileVo);
                CustomerGoalSetupDao customerGoalSetupDao = new CustomerGoalSetupDao();
                if (Flag == 0)
                    customerGoalSetupDao.CreateCustomerGoalProfile(CreateGoalProfileVo, UserId);
                else
                    // when the flag value 1 at this time userId IS GoalId
                    customerGoalSetupDao.UpdateCustomerGoalProfile(CreateGoalProfileVo, UserId);
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:CreateCustomerGoalProfileForRetirement()");


                object[] objects = new object[2];
                objects[0] = GoalProfileVo;
                objects[1] = UserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            

        }
        public Decimal GetExpectedROI(int CustomerID)
        {
            Decimal ExpROI = 0;
            CustomerGoalSetupDao ExpectedROI = new CustomerGoalSetupDao();

            try
            {
                ExpROI = ExpectedROI.GetExpectedROI(CustomerID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:GetExpectedROI()");

                object[] objects = new object[1];
                objects[0] = CustomerID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ExpROI;


        }
        public List<GoalProfileSetupVo> GetCustomerGoalProfile(int CustomerId, int ActiveFlag)
        {
            List<GoalProfileSetupVo> GoalProfileList = new List<GoalProfileSetupVo>();
            CustomerGoalSetupDao customerGoalProfileDao = new CustomerGoalSetupDao();
            try
            {

                GoalProfileList = customerGoalProfileDao.GetCustomerGoalProfile(CustomerId, ActiveFlag);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:GetCustomerGoalProfile()");
                object[] objects = new object[1];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return GoalProfileList;
        }

        public double SumSavingReq(int CustomerId, int ActiveFlag)
        {
            double SumSave = 0;
            List<GoalProfileSetupVo> GoalProfileList = new List<GoalProfileSetupVo>();
            CustomerGoalSetupDao customerGoalProfileDao = new CustomerGoalSetupDao();
            GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
            GoalProfileList = customerGoalProfileDao.GetCustomerGoalProfile(CustomerId, ActiveFlag);
            
            try
            {
                for (int i = 0; i < GoalProfileList.Count; i++)
                {
                    
                   SumSave +=goalProfileSetupVo.MonthlySavingsReq;
                     
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
                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:SumSavingReq()");
                object[] objects = new object[1];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return SumSave;
        }

        public DataSet GetCustomerRTDetails(int CustomerID)
        {
            DataSet dsGetCustomerRTDetails = new DataSet();
            CustomerGoalSetupDao RTDetails = new CustomerGoalSetupDao();

            try
            {
                dsGetCustomerRTDetails = RTDetails.GetCustomerRTDetails(CustomerID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:GetCustomerRTDetails()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerRTDetails;


        }

        public void SetCustomerGoalIsActive(String GoalIDs, int UserId)
        {
            
            CustomerGoalSetupDao SetGoalIsActive = new CustomerGoalSetupDao();

            try
            {
                SetGoalIsActive.SetCustomerlGoalIsActive(GoalIDs, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:SetCustomerGoalIsActive()");

                object[] objects = new object[2];
                objects[0] = GoalIDs;
                objects[1] = UserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
                        
        }


        public void SetCustomerGoalDeActive(String GoalIDs, int UserId)
        {

            CustomerGoalSetupDao SetGoalDeActive = new CustomerGoalSetupDao();

            try
            {
                SetGoalDeActive.SetCustomerlGoalDeActive(GoalIDs, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:SetCustomerGoalDeActive()");

                object[] objects = new object[2];
                objects[0] = GoalIDs;
                objects[1] = UserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }


        public void DeleteCustomerGoal(String GoalIDs, int UserId)
        {

            CustomerGoalSetupDao DeleteGoalProfile = new CustomerGoalSetupDao();

            try
            {
                DeleteGoalProfile.DeleteCustomerlGoal(GoalIDs, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:DeleteCustomerGoal()");

                object[] objects = new object[2];
                objects[0] = GoalIDs;
                objects[1] = UserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }



        public GoalProfileSetupVo GetCustomerGoal(int CustomerId, int GoalId)
        {
            GoalProfileSetupVo GoalProfileVo = new GoalProfileSetupVo();
            CustomerGoalSetupDao GaolProfiling = new CustomerGoalSetupDao();

            try
            {
                GoalProfileVo = GaolProfiling.GetCustomerGoal(CustomerId, GoalId);

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

                object[] objects = new object[2];
                objects[0] = CustomerId;
                objects[1] = GoalId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return GoalProfileVo;


        }


        public int CheckGoalProfile(int UserId)
        {
            CustomerGoalSetupDao GaolProfiling = new CustomerGoalSetupDao();
            int Count = 0;
            return Count = GaolProfiling.CheckGoalProfile(UserId);
        }
        public void SetCustomerAllGoalDeActive(int UserId)
        {
            CustomerGoalSetupDao GaolProfiling = new CustomerGoalSetupDao();
            GaolProfiling.SetCustomerAllGoalDeActive(UserId);
            
        }


    }
}
