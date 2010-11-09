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

        GoalProfileSetupVo goalProfileSetupVo = new GoalProfileSetupVo();
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
            double lumpsumInvestRequired = 0;
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
                
                lumpsumInvestRequired = Math.Abs(PV(newRate, yearsLeft, 0, -(retirementCorpus - futureValue), 0));
                GoalProfileVo.FutureValueOnCurrentInvest = futureValue;
                GoalProfileVo.RetirementCorpus = retirementCorpus;
                GoalProfileVo.MonthlySavingsReq = monthlySavings;
                GoalProfileVo.InflationPercent = InflationPercent;
                GoalProfileVo.LumpsumInvestRequired = lumpsumInvestRequired;
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
        /// <summary>
        /// It retuns Customer Retirement Goal description paragrapgh 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string RTGoalDescriptionText(int CustomerID)
        {
            
          
            string GoalDescription="";
            DataSet dsGetCustomerRTDetails = new DataSet();
            CustomerGoalSetupDao RTDetails = new CustomerGoalSetupDao();

            try
            {
                string futureValueOncurrentInvestment = null;
                string futureValueOncurrentInvestment2 = null;
                string yearlySavingRequired = null;
                string gapvalues = null;
                dsGetCustomerRTDetails = RTDetails.GetCustomerRTDetails(CustomerID);
                if (dsGetCustomerRTDetails.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_FutureValueOnCurrentInvest"].ToString()))
                    {
                        futureValueOncurrentInvestment = "0";

                    }
                    else
                        futureValueOncurrentInvestment = Math.Round(double.Parse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_FutureValueOnCurrentInvest"].ToString()), 2).ToString();

                    if (string.IsNullOrEmpty(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_LumpsumInvestmentRequired"].ToString()))
                    {
                        futureValueOncurrentInvestment2 = "0";

                    }
                    else
                        futureValueOncurrentInvestment2 = Math.Round(double.Parse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_LumpsumInvestmentRequired"].ToString()), 2).ToString();


                    if (string.IsNullOrEmpty(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_YearlySavingsRequired"].ToString()))
                    {
                        yearlySavingRequired = "0";

                    }
                    else
                        yearlySavingRequired = Math.Round(double.Parse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_YearlySavingsRequired"].ToString()), 2).ToString();


                    if (string.IsNullOrEmpty(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_GapValues"].ToString()))
                    {
                        gapvalues = "0";

                    }
                    else
                        gapvalues = Math.Round(double.Parse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_GapValues"].ToString()), 2).ToString();


                    if (dsGetCustomerRTDetails.Tables[0].Rows.Count > 0)
                    {
                        double fvCostOfToday = 0;
                        double currentInvestment = 0;
                        double roiEarned = 0;
                        double monthlySavingsRequired = 0;
                        double.TryParse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_FVofCostToday"].ToString(), out fvCostOfToday);
                        double.TryParse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_CurrentInvestment"].ToString(), out currentInvestment);
                        double.TryParse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_ROIEarned"].ToString(), out roiEarned);
                        double.TryParse(dsGetCustomerRTDetails.Tables[0].Rows[0]["CG_MonthlySavingsRequired"].ToString(), out monthlySavingsRequired);

                        string StrRT1 = "We have done an extensive analysis of your" +
                        " retirement goal needs and savings required per month to meet those " +
                        "needs.Based on the inputs provided by you we have calculated that at " +
                        "the time of your retirement you'll need a corpus of Rs." + Math.Round(fvCostOfToday, 2).ToString() +
                        " to lead a financialy stable retired life.";

                        string StrRT2 = "To meet the retirement goal you have already invested Rs." + Math.Round(currentInvestment, 2).ToString() +
                        " which will grow at " + Math.Round(roiEarned, 2).ToString() + " % " +
                        " and it's value at the time of your retirement will be Rs." + futureValueOncurrentInvestment + ".";

                        string StrRT3 = "You have no investmensts attached to your retirement.";

                        string StrRT4 = "For the gap of Rs." + gapvalues +
                        " for your retirement goal, you need to start planning soon." +
                        "You can opt for any of the following saving options to meet your retirement goal." +
                        " Monthly savings required to meet the goal is Rs." + Math.Round(monthlySavingsRequired, 2).ToString() +
                        ".Yearly savings required to meet the goal is Rs." + yearlySavingRequired + ".Lumpsum investment required is Rs." + futureValueOncurrentInvestment2 +
                        ".We recommend you to start saving as per the retirement plan provided by us.";



                        if (currentInvestment == 0)
                        {
                            GoalDescription = StrRT1 + StrRT3 + StrRT4;

                        }
                        else

                            GoalDescription = StrRT1 + StrRT2 + StrRT4;
                    }
                    else
                        GoalDescription = "";
                }
                else
                {
                    GoalDescription = "";
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

                FunctionInfo.Add("Method", "CustomerGoalSetupBo.cs:GetCustomerRTDetails()");

                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            
            

            return GoalDescription;
        }
        /// <summary>
        /// It retuns Customer Other Goals description paragrapgh 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public string OtherGoalDescriptionText(int CustomerId)
        {
            string GoalDescription = "";
            List<GoalProfileSetupVo> GoalProfileList = new List<GoalProfileSetupVo>();
            CustomerGoalSetupDao customerGoalProfileDao = new CustomerGoalSetupDao();
            try
            {
                
                double totalChildEducation=0;
                double totalChildMarriage=0;
                double totalHome=0;
                double totalOther = 0;

                string strMain = "";
                string strChildEducation = "";
                string strChildMarriage = "";
                string strHome = "";
                string strOther = "";
                string strTotal = "";

                GoalProfileList = customerGoalProfileDao.GetCustomerGoalProfile(CustomerId, 1);
                if (GoalProfileList != null)
                {
                    for (int i = 0; i < GoalProfileList.Count; i++)
                    {
                        goalProfileSetupVo = new GoalProfileSetupVo();
                        goalProfileSetupVo = GoalProfileList[i];
                        if (goalProfileSetupVo.Goalcode == "BH")
                        {
                            totalHome = totalHome + goalProfileSetupVo.MonthlySavingsReq;
                        }
                        if (goalProfileSetupVo.Goalcode == "ED")
                        {
                            totalChildEducation = totalChildEducation + goalProfileSetupVo.MonthlySavingsReq;

                        }
                        if (goalProfileSetupVo.Goalcode == "MR")
                        {
                            totalChildMarriage = totalChildMarriage + goalProfileSetupVo.MonthlySavingsReq;

                        }
                        if (goalProfileSetupVo.Goalcode == "OT")
                        {
                            totalOther = totalOther + goalProfileSetupVo.MonthlySavingsReq;

                        }

                    }
                    double TotalCost = totalChildEducation + totalChildMarriage + totalHome + totalOther;

                    strMain = "Based on your inputs we have done an analysis of your life's major financial goals and savings required to achieve them.";
                    if (totalChildEducation != 0)
                        strChildEducation = "You need to save Rs." + Math.Round(totalChildEducation,2).ToString() + " to meet your children's higher education expenses.";               
                    if (totalChildMarriage != 0)
                        strChildMarriage = "Your monthly saving should be Rs." + Math.Round(totalChildMarriage, 2).ToString() + " to meet your children's marriage expeneses."; 
                    if (totalHome != 0)
                        strHome = "For buying house you need to save Rs." + Math.Round(totalHome,2).ToString() + " every month.";
                    if (totalOther != 0)
                        strOther = "For other Goals you need to save Rs." + Math.Round(totalOther,2).ToString() + " every month.";

                    strTotal = "To meet all your major life goals other than retirement you need to save Rs." + Math.Round(TotalCost,2).ToString() + " every month.";

                    GoalDescription = strMain + strChildEducation + strChildMarriage + strHome + strOther + strTotal;
                }
                else
                GoalDescription = "";
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
            return GoalDescription;
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
