using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoFPSuperlite;

namespace BoFPSuperlite
{
    public class CustomerFPAnalyticsBo
    {  

        public DataSet GetCustomerProjectedAssetAllocation(int CustomerID)
        {
            CustomerFPAnalyticsDao customerFPAnalyticsDao = new CustomerFPAnalyticsDao();
            DataSet projectedAssetAllocationDs;

            try
            {
                projectedAssetAllocationDs = customerFPAnalyticsDao.GetCustomerProjectedAssetAllocation(CustomerID);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFPAnalyticsBo:GetCustomerProjectedAssetAllocation()");
                
                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return projectedAssetAllocationDs;
        }


        public DataSet FutureSurplusEngine(int CustomerID)
        {
            CustomerFPAnalyticsDao customerFPAnalyticsDao = new CustomerFPAnalyticsDao();
            DataSet customerDataForFutureSurplusEngineDs;
            decimal totalIncome;
            decimal  totalExpense;
            int age;
            DataSet dsFPAnalyticsEngine = new DataSet();
            try
            {
                customerDataForFutureSurplusEngineDs = customerFPAnalyticsDao.GetCustomerDataForFutureSurplusEngine(CustomerID, out totalIncome, out totalExpense, out age);

                if (age != 0 && customerDataForFutureSurplusEngineDs.Tables[0].Rows.Count > 0 && customerDataForFutureSurplusEngineDs.Tables[1].Rows.Count > 0)
                {
                    dsFPAnalyticsEngine = CreateFutureSurplusEngine(customerDataForFutureSurplusEngineDs, totalIncome, totalExpense, age);
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

                FunctionInfo.Add("Method", "CustomerFPAnalyticsBo:FutureSurplusEngine()");

                object[] objects = new object[1];
                objects[0] = CustomerID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsFPAnalyticsEngine;
        }

        public DataSet CreateFutureSurplusEngine(DataSet dsCustomerData,decimal totalIncome, decimal totalExpense,int age)
        {
            int lifeRest; 
            int lifeExpentancy=0;
            int retirementAge=0;
            int tempYear;
            decimal incomeGrowthRate=0;
            decimal expenseGrowthRate=0;

            decimal equityGrowthRate = 0;
            decimal debtGrowthRate = 0;
            decimal cashGrowthRate = 0;
            decimal alternateGrowthRate = 0;

            decimal equityAllocationPercent = 0;
            decimal debtAllocationPercent = 0;
            decimal cashAllocationPercent = 0;
            decimal alternateAllocationPercent = 0;

            decimal equityAggAdjustment = 0;
            decimal debtAggAdjustment = 0;
            decimal cashAggAdjustment = 0;
            decimal alternateAggAdjustment = 0;

            decimal equityAgreedPercent = 0;
            decimal debtAgreedPercent = 0;
            decimal cashAgreedPercent = 0;
            decimal alternateAgreedPercent = 0;

            decimal equityAmount = 0;
            decimal debtAmount = 0;
            decimal cashAmount = 0;
            decimal alternateAmount = 0;

            decimal equityAmountFV = 0;
            decimal debtAmountFV = 0;
            decimal cashAmountFV = 0;
            decimal alternateAmountFV = 0;
            DataSet dsFPAnalyticsEngine = new DataSet();
            

            int retYear;
            decimal salaryIncome=0;
            decimal surplusForTheTempYear;
            DataTable dtCustomerStaticAssumption;
            DataTable dtCustomerProjectedAssumption;
            DataTable dtIncomeDetails;
            DataTable dtCustomerAssetAllocation;
            DataTable dtCustomerCurrentAssetAllocation;
            DataTable dtCustomerGoalFunding;
            dtCustomerStaticAssumption = dsCustomerData.Tables["CustomerStaticAssumption"];
            dtCustomerProjectedAssumption = dsCustomerData.Tables["CustomerProjectedAssumption"];
            dtIncomeDetails = dsCustomerData.Tables["CustomerFPIncomeDetails"];
            dtCustomerAssetAllocation = dsCustomerData.Tables["CustomerAssetAllocation"];

            dtCustomerCurrentAssetAllocation = dsCustomerData.Tables["CustomerCurrentAssetAllocation"];
            dtCustomerGoalFunding = dsCustomerData.Tables["CustomerGoalFunding"];



            DataTable dtFutureSurplusEngine = new DataTable();
            dtFutureSurplusEngine.Columns.Add("Year");
            dtFutureSurplusEngine.Columns.Add("Income");
            dtFutureSurplusEngine.Columns.Add("IncomeGrowth");
            dtFutureSurplusEngine.Columns.Add("Expense");
            dtFutureSurplusEngine.Columns.Add("ExpenseGrowth");
            dtFutureSurplusEngine.Columns.Add("AvailableSurplus");

            dtFutureSurplusEngine.Columns.Add("AgrEquityPercent");
            dtFutureSurplusEngine.Columns.Add("EquityAmount");
            dtFutureSurplusEngine.Columns.Add("EquityFutureValue");

            dtFutureSurplusEngine.Columns.Add("AgrDebtPercent");
            dtFutureSurplusEngine.Columns.Add("DebtAmount");
            dtFutureSurplusEngine.Columns.Add("DebtFutureValue");

            dtFutureSurplusEngine.Columns.Add("AgrCashPercent");
            dtFutureSurplusEngine.Columns.Add("CashAmount");
            dtFutureSurplusEngine.Columns.Add("CashFutureValue");

            dtFutureSurplusEngine.Columns.Add("AgrAlternatePercent");
            dtFutureSurplusEngine.Columns.Add("AlternateAmount");
            dtFutureSurplusEngine.Columns.Add("AlternateFutureValue");

            dtFutureSurplusEngine.Columns.Add("TotalAssetValues");
            dtFutureSurplusEngine.Columns.Add("TotalAssetFutureValues");

            //********************************************************************************************

            DataTable dtRebalancingEngine = new DataTable();
            dtRebalancingEngine.Columns.Add("Year");
            dtRebalancingEngine.Columns.Add("AssetClass");
            dtRebalancingEngine.Columns.Add("PreviousYearClosingBalance");
            dtRebalancingEngine.Columns.Add("AgrredPercent");            
            dtRebalancingEngine.Columns.Add("CurrentAssetAllocationPercent");
            dtRebalancingEngine.Columns.Add("GapFrpmAgrredPercent");

            dtRebalancingEngine.Columns.Add("AmountBeforeReturns");
            dtRebalancingEngine.Columns.Add("ReturnRate");
            dtRebalancingEngine.Columns.Add("AmountAfterReturns");
            dtRebalancingEngine.Columns.Add("GoalMoneyWithdrawn");
            dtRebalancingEngine.Columns.Add("MoneyToBeRebalanced");
            dtRebalancingEngine.Columns.Add("MoneyAvailableAfterRW");
            dtRebalancingEngine.Columns.Add("MoneyAvailableAfterRWReturn");
            dtRebalancingEngine.Columns.Add("BalanceMoney");


            DataRow drEquityRebalancingEngine;
            DataRow drDebtRebalancingEngine;
            DataRow drCashRebalancingEngine;
            DataRow drAlternateRebalancingEngine;




            //********************************************************************
            decimal equityCurrentPercent = 0;
            decimal debtCurrentPercent = 0;
            decimal cashCurrentPercent = 0;
            decimal alternateCurrentPercent = 0;

            decimal equityClosingBalance = 0;
            decimal debtClosingBalance = 0;
            decimal cashClosingBalance = 0;
            decimal alternateClosingBalance = 0;

            decimal equityGapFromAgreedAllocation = 0;
            decimal debtGapFromAgreedAllocation = 0;
            decimal cashGapFromAgreedAllocation = 0;
            decimal alternateGapFromAgreedAllocation = 0;

            decimal equityAmountToBeRebalanced = 0;
            decimal debtAmountToBeRebalanced = 0;
            decimal cashAmountToBeRebalanced = 0;
            decimal alternateAmountToBeRebalanced = 0;

            decimal equityAmountAvailableAfterRebalancingAndWithdrawls = 0;
            decimal debtAmountAvailableAfterRebalancingAndWithdrawls = 0;
            decimal cashAmountAvailableAfterRebalancingAndWithdrawls = 0;
            decimal alternateAmountAvailableAfterRebalancingAndWithdrawls = 0;

            decimal equityAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = 0;
            decimal debtAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = 0;
            decimal cashAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = 0;
            decimal alternateAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = 0;

            decimal equityBalanceAmount = 0;
            decimal debtBalanceAmount = 0;
            decimal cashBalanceAmount = 0;
            decimal alternateBalanceAmount = 0;

            decimal equityGoalFundedAmount = 0;
            decimal debtGoalFundedAmount = 0;
            decimal cashGoalFundedAmount = 0;
            decimal alternateGoalFundedAmount = 0;


            //***********************************************************************************************

            DataRow[] drGoalFundingAssetWise = dtCustomerGoalFunding.Select("CG_GoalYear=" + DateTime.Now.Year.ToString());
            foreach (DataRow dr in drGoalFundingAssetWise)
            {
                if (Convert.ToString(dr["AssetClass"]) == "Equity")
                {
                    equityGoalFundedAmount = decimal.Parse(Convert.ToString(dr["Amount"]));
                }
                else if (Convert.ToString(dr["AssetClass"]) == "Debt")
                {
                    debtGoalFundedAmount = decimal.Parse(Convert.ToString(dr["Amount"]));
                }
                else if (Convert.ToString(dr["AssetClass"]) == "Cash")
                {
                    cashGoalFundedAmount = decimal.Parse(Convert.ToString(dr["Amount"]));
                }
                else if (Convert.ToString(dr["AssetClass"]) == "Alternate")
                {
                    alternateGoalFundedAmount = decimal.Parse(Convert.ToString(dr["Amount"]));
                }
            }

            //**********************************************************************************************************************

            foreach (DataRow dr in dtCustomerCurrentAssetAllocation.Rows)
            {
                if (Convert.ToString(dr["AssetClass"]) == "Equity")
                {
                    equityCurrentPercent = decimal.Parse(Convert.ToString(dr["CurrentPercentage"]));
                    equityClosingBalance=decimal.Parse(Convert.ToString(dr["CurrentAssetValue"]));
                }
                else if (Convert.ToString(dr["AssetClass"]) == "Debt")
                {
                    debtCurrentPercent = decimal.Parse(Convert.ToString(dr["CurrentPercentage"]));
                    debtClosingBalance=decimal.Parse(Convert.ToString(dr["CurrentAssetValue"]));
                }
                else if (Convert.ToString(dr["AssetClass"]) == "Cash")
                {
                    cashCurrentPercent = decimal.Parse(Convert.ToString(dr["CurrentPercentage"]));
                    cashClosingBalance=decimal.Parse(Convert.ToString(dr["CurrentAssetValue"]));
                }
                else if (Convert.ToString(dr["AssetClass"]) == "Alternates")
                {
                    alternateCurrentPercent = decimal.Parse(Convert.ToString(dr["CurrentPercentage"]));
                    alternateClosingBalance=decimal.Parse(Convert.ToString(dr["CurrentAssetValue"]));
                }

            }


            //***********************************************************************************************************
            foreach (DataRow dr in dtIncomeDetails.Rows)
            {
                if (dr["IncomeCategory"].ToString() == "Salary")
                {
                    salaryIncome =decimal.Parse(dr["IncomeAmount"].ToString());
                }
 
            }
            foreach (DataRow dr in dtCustomerStaticAssumption.Rows)
            {
                if (dr["WA_AssumptionId"].ToString() == "LE")
                {
                    lifeExpentancy =(int)Math.Round(double.Parse(dr["CSA_Value"].ToString()),0);
                }
                else if (dr["WA_AssumptionId"].ToString() == "RA")
                {
                    retirementAge = (int)Math.Round(double.Parse(dr["CSA_Value"].ToString()), 0);
                }

            }

            DataRow[] drCustomerAssetAllocation = dtCustomerAssetAllocation.Select("CAA_year=" + DateTime.Now.Year.ToString());
            foreach (DataRow dr in drCustomerAssetAllocation)
            {
                if (Convert.ToString(dr["WAC_AssetClassification"]) == "Equity")
                {
                    equityAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                    equityAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));

                }
                else if (Convert.ToString(dr["WAC_AssetClassification"]) == "Debt")
                {
                    debtAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                    debtAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));
 
                }
                else if (Convert.ToString(dr["WAC_AssetClassification"]) == "Cash")
                {
                    cashAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                    cashAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));

                }
                else if (Convert.ToString(dr["WAC_AssetClassification"]) == "Alternate")
                {
                    alternateAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                    alternateAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));

                }
             
            }

            tempYear = DateTime.Now.Year;
            lifeRest = lifeExpentancy - age;
            retYear = (retirementAge - age) + tempYear;
           
            
            

           // for First Record Processing
            DataRow drFutureSurplusEngine;
            drFutureSurplusEngine = dtFutureSurplusEngine.NewRow();
            DataRow[] drGrowthRateYearWise = dtCustomerProjectedAssumption.Select("CPA_Year=" + tempYear.ToString());
            drCustomerAssetAllocation = dtCustomerAssetAllocation.Select("CAA_year=" + tempYear.ToString());
            foreach (DataRow dr in drGrowthRateYearWise)
            {
                if (dr["WA_AssumptionId"].ToString() == "IR")
                {
                    incomeGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                }
                else if (dr["WA_AssumptionId"].ToString() == "IG")
                {
                    expenseGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                }
                else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "ER")
                {
                    equityGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                }
                else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "DR")
                {
                    debtGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                }
                else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "CR")
                {
                    cashGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                }
                else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "AR")
                {
                    alternateGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                }
            }
            drFutureSurplusEngine["Year"] = tempYear;
            drFutureSurplusEngine["Income"] = totalIncome;
            drFutureSurplusEngine["IncomeGrowth"] = incomeGrowthRate;
            drFutureSurplusEngine["Expense"] = Math.Round(totalExpense,2);
            drFutureSurplusEngine["ExpenseGrowth"] = expenseGrowthRate;
            surplusForTheTempYear = Math.Round((totalIncome - totalExpense),2);
            drFutureSurplusEngine["AvailableSurplus"] =surplusForTheTempYear;

            //**************************Equity******************************
            equityAgreedPercent =Math.Round(equityAllocationPercent + equityAggAdjustment);
            drFutureSurplusEngine["AgrEquityPercent"] = equityAgreedPercent;
            equityAmount =Math.Round((surplusForTheTempYear / 100 * equityAgreedPercent),2);
            drFutureSurplusEngine["EquityAmount"] = equityAmount;
            if (equityAmount != 0)
            equityAmountFV=Math.Round(decimal.Parse(FutureValue(double.Parse((equityGrowthRate/100).ToString()),1,0,-double.Parse(equityAmount.ToString()),0).ToString()),2);
            drFutureSurplusEngine["EquityFutureValue"] = equityAmountFV;

            //**************************Debt******************************
            debtAgreedPercent =Math.Round(debtAllocationPercent + debtAggAdjustment);
            drFutureSurplusEngine["AgrDebtPercent"] = debtAgreedPercent;
            debtAmount =Math.Round((surplusForTheTempYear / 100 * debtAgreedPercent),2);
            drFutureSurplusEngine["DebtAmount"] = debtAmount;
            if (debtAmount != 0)
            debtAmountFV =Math.Round(decimal.Parse(FutureValue(double.Parse((debtGrowthRate / 100).ToString()), 1, 0, -double.Parse(debtAmount.ToString()), 0).ToString()),2);
            drFutureSurplusEngine["DebtFutureValue"] = debtAmountFV;

            //**************************Cash******************************
            cashAgreedPercent =Math.Round(cashAllocationPercent + cashAggAdjustment);
            drFutureSurplusEngine["AgrCashPercent"] = cashAgreedPercent;
            cashAmount =Math.Round(surplusForTheTempYear / 100 * cashAgreedPercent);
            drFutureSurplusEngine["CashAmount"] = cashAmount;
            if (cashAmount != 0)
                cashAmountFV =Math.Round(decimal.Parse(FutureValue(double.Parse((cashGrowthRate / 100).ToString()), 1, 0, -double.Parse(cashAmount.ToString()), 0).ToString()));
            drFutureSurplusEngine["CashFutureValue"] = cashAmountFV;

            //**************************Alternate******************************
            alternateAgreedPercent =Math.Round(alternateAllocationPercent + alternateAggAdjustment);
            drFutureSurplusEngine["AgrAlternatePercent"] = alternateAgreedPercent;
            alternateAmount =Math.Round(surplusForTheTempYear / 100 * alternateAgreedPercent);
            drFutureSurplusEngine["alternateAmount"] = alternateAmount;
            if (alternateAmount != 0)
            alternateAmountFV = decimal.Parse(FutureValue(double.Parse((alternateGrowthRate / 100).ToString()), 1, 0, -double.Parse(alternateAmount.ToString()), 0).ToString());
            drFutureSurplusEngine["alternateFutureValue"] = alternateAmountFV;

            drFutureSurplusEngine["TotalAssetValues"] =Math.Round(equityAmount + debtAmount + cashAmount + alternateAmount);
            drFutureSurplusEngine["TotalAssetFutureValues"] =Math.Round(equityAmountFV + debtAmountFV + cashAmountFV + alternateAmountFV);

            dtFutureSurplusEngine.Rows.Add(drFutureSurplusEngine);


            //*************************************1st Record Processing for Rebalancing*****************************************
            

            equityGapFromAgreedAllocation = equityAgreedPercent - equityCurrentPercent;
            debtGapFromAgreedAllocation = debtAgreedPercent - debtCurrentPercent;
            cashGapFromAgreedAllocation = cashAgreedPercent - cashCurrentPercent;
            alternateGapFromAgreedAllocation = alternateAgreedPercent - alternateCurrentPercent;


            equityAmountToBeRebalanced = equityGapFromAgreedAllocation / 100 * equityClosingBalance + equityAmount;
            debtAmountToBeRebalanced = debtGapFromAgreedAllocation / 100 * debtClosingBalance + debtAmount;
            cashAmountToBeRebalanced = cashGapFromAgreedAllocation / 100 * cashClosingBalance + cashAmount;
            alternateAmountToBeRebalanced = alternateGapFromAgreedAllocation / 100 * alternateClosingBalance + alternateAmount;

            equityAmountAvailableAfterRebalancingAndWithdrawls = equityClosingBalance - equityGoalFundedAmount + equityAmountToBeRebalanced;
            debtAmountAvailableAfterRebalancingAndWithdrawls = debtClosingBalance - debtGoalFundedAmount + debtAmountToBeRebalanced;
            cashAmountAvailableAfterRebalancingAndWithdrawls = cashClosingBalance - cashGoalFundedAmount + cashAmountToBeRebalanced;
            alternateAmountAvailableAfterRebalancingAndWithdrawls = alternateClosingBalance - alternateGoalFundedAmount + alternateAmountToBeRebalanced;

            if (equityAmountAvailableAfterRebalancingAndWithdrawls != 0)
            equityAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(equityGrowthRate.ToString()), 1, 0, -double.Parse(equityAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());
            if (debtAmountAvailableAfterRebalancingAndWithdrawls != 0)
            debtAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(debtGrowthRate.ToString()), 1, 0, -double.Parse(debtAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());
            if (cashAmountAvailableAfterRebalancingAndWithdrawls != 0)
            cashAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(cashGrowthRate.ToString()), 1, 0, -double.Parse(cashAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());
            if (alternateAmountAvailableAfterRebalancingAndWithdrawls != 0)
            alternateAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(alternateGrowthRate.ToString()), 1, 0, -double.Parse(alternateAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());

            equityBalanceAmount = equityAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + equityAmountFV;
            debtBalanceAmount = debtAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + debtAmountFV;
            cashBalanceAmount = cashAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + cashAmountFV;
            alternateBalanceAmount = alternateAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + alternateAmountFV;

            drEquityRebalancingEngine = dtRebalancingEngine.NewRow();
            drDebtRebalancingEngine = dtRebalancingEngine.NewRow();
            drCashRebalancingEngine = dtRebalancingEngine.NewRow();
            drAlternateRebalancingEngine = dtRebalancingEngine.NewRow();

            drEquityRebalancingEngine["Year"] = tempYear;
            drEquityRebalancingEngine["AssetClass"] = "Equity";
            drEquityRebalancingEngine["PreviousYearClosingBalance"] = equityClosingBalance;
            drEquityRebalancingEngine["AgrredPercent"] = equityAgreedPercent;
            drEquityRebalancingEngine["CurrentAssetAllocationPercent"] = equityCurrentPercent;
            drEquityRebalancingEngine["GapFrpmAgrredPercent"] = equityGapFromAgreedAllocation;
            drEquityRebalancingEngine["AmountBeforeReturns"] = equityAmount;
            drEquityRebalancingEngine["ReturnRate"] = equityGrowthRate;
            drEquityRebalancingEngine["AmountAfterReturns"] = equityAmountFV;
            drEquityRebalancingEngine["GoalMoneyWithdrawn"] = equityGoalFundedAmount;
            drEquityRebalancingEngine["MoneyToBeRebalanced"] = equityAmountToBeRebalanced;
            drEquityRebalancingEngine["MoneyAvailableAfterRW"] = equityAmountAvailableAfterRebalancingAndWithdrawls;
            drEquityRebalancingEngine["MoneyAvailableAfterRWReturn"] = equityAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
            drEquityRebalancingEngine["BalanceMoney"] = equityBalanceAmount;
            dtRebalancingEngine.Rows.Add(drEquityRebalancingEngine);

            drDebtRebalancingEngine["Year"] = tempYear;
            drDebtRebalancingEngine["AssetClass"] = "Debt";
            drDebtRebalancingEngine["PreviousYearClosingBalance"] = debtClosingBalance;
            drDebtRebalancingEngine["AgrredPercent"] = debtAgreedPercent;
            drDebtRebalancingEngine["CurrentAssetAllocationPercent"] = debtCurrentPercent;
            drDebtRebalancingEngine["GapFrpmAgrredPercent"] = debtGapFromAgreedAllocation;
            drDebtRebalancingEngine["AmountBeforeReturns"] = debtAmount;
            drDebtRebalancingEngine["ReturnRate"] = debtGrowthRate;
            drDebtRebalancingEngine["AmountAfterReturns"] = debtAmountFV;
            drDebtRebalancingEngine["GoalMoneyWithdrawn"] = debtGoalFundedAmount;
            drDebtRebalancingEngine["MoneyToBeRebalanced"] = debtAmountToBeRebalanced;
            drDebtRebalancingEngine["MoneyAvailableAfterRW"] = debtAmountAvailableAfterRebalancingAndWithdrawls;
            drDebtRebalancingEngine["MoneyAvailableAfterRWReturn"] = debtAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
            drDebtRebalancingEngine["BalanceMoney"] = debtBalanceAmount;
            dtRebalancingEngine.Rows.Add(drDebtRebalancingEngine);

            drCashRebalancingEngine["Year"] = tempYear;
            drCashRebalancingEngine["AssetClass"] = "Cash";
            drCashRebalancingEngine["PreviousYearClosingBalance"] = cashClosingBalance;
            drCashRebalancingEngine["AgrredPercent"] = cashAgreedPercent;
            drCashRebalancingEngine["CurrentAssetAllocationPercent"] = cashCurrentPercent;
            drCashRebalancingEngine["GapFrpmAgrredPercent"] = cashGapFromAgreedAllocation;
            drCashRebalancingEngine["AmountBeforeReturns"] = cashAmount;
            drCashRebalancingEngine["ReturnRate"] = cashGrowthRate;
            drCashRebalancingEngine["AmountAfterReturns"] = cashAmountFV;
            drCashRebalancingEngine["GoalMoneyWithdrawn"] = cashGoalFundedAmount;
            drCashRebalancingEngine["MoneyToBeRebalanced"] = cashAmountToBeRebalanced;
            drCashRebalancingEngine["MoneyAvailableAfterRW"] = cashAmountAvailableAfterRebalancingAndWithdrawls;
            drCashRebalancingEngine["MoneyAvailableAfterRWReturn"] = cashAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
            drCashRebalancingEngine["BalanceMoney"] = cashBalanceAmount;
            dtRebalancingEngine.Rows.Add(drCashRebalancingEngine);

            drAlternateRebalancingEngine["Year"] = tempYear;
            drAlternateRebalancingEngine["AssetClass"] = "Alternate";
            drAlternateRebalancingEngine["PreviousYearClosingBalance"] = alternateClosingBalance;
            drAlternateRebalancingEngine["AgrredPercent"] = alternateAgreedPercent;
            drAlternateRebalancingEngine["CurrentAssetAllocationPercent"] = alternateCurrentPercent;
            drAlternateRebalancingEngine["GapFrpmAgrredPercent"] = alternateGapFromAgreedAllocation;
            drAlternateRebalancingEngine["AmountBeforeReturns"] = alternateAmount;
            drAlternateRebalancingEngine["ReturnRate"] = alternateGrowthRate;
            drAlternateRebalancingEngine["AmountAfterReturns"] = alternateAmountFV;
            drAlternateRebalancingEngine["GoalMoneyWithdrawn"] = alternateGoalFundedAmount;
            drAlternateRebalancingEngine["MoneyToBeRebalanced"] = alternateAmountToBeRebalanced;
            drAlternateRebalancingEngine["MoneyAvailableAfterRW"] = alternateAmountAvailableAfterRebalancingAndWithdrawls;
            drAlternateRebalancingEngine["MoneyAvailableAfterRWReturn"] = alternateAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
            drAlternateRebalancingEngine["BalanceMoney"] = alternateBalanceAmount;
            dtRebalancingEngine.Rows.Add(drAlternateRebalancingEngine);
           
           

            //***************************************************************************************************************************************

            for (; lifeRest >= 0; lifeRest--)
            {
                equityCurrentPercent = equityAgreedPercent;
                debtCurrentPercent = debtAgreedPercent;
                cashCurrentPercent = cashAgreedPercent;
                alternateCurrentPercent = alternateAgreedPercent;

                drFutureSurplusEngine = dtFutureSurplusEngine.NewRow();               
                drGrowthRateYearWise = dtCustomerProjectedAssumption.Select("CPA_Year=" + tempYear.ToString());
                foreach (DataRow dr in drGrowthRateYearWise)
                {
                    if (Convert.ToString(dr["WA_AssumptionId"]).Trim()== "IR")
                    {
                        incomeGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                    }
                    else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "IG")
                    {
                        expenseGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                    }
                    else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "ER")
                    {
                        equityGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                    }
                    else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "DR")
                    {
                        debtGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                    }
                    else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "CR")
                    {
                        cashGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                    }
                    else if (Convert.ToString(dr["WA_AssumptionId"]).Trim() == "AR")
                    {
                        alternateGrowthRate = decimal.Parse(dr["CPA_Value"].ToString());
                    }
                    
                }
                if (tempYear == retYear)
                    totalIncome = totalIncome - salaryIncome;
                else
                salaryIncome = Math.Round(salaryIncome + (salaryIncome * incomeGrowthRate / 100));

                totalIncome = Math.Round(totalIncome + (totalIncome * incomeGrowthRate / 100));
                totalExpense =Math.Round(totalExpense + (totalExpense * expenseGrowthRate / 100));
               
               

                //*
                drCustomerAssetAllocation = dtCustomerAssetAllocation.Select("CAA_year=" + tempYear.ToString());

                foreach (DataRow dr in drCustomerAssetAllocation)
                {
                    if (Convert.ToString(dr["WAC_AssetClassification"]).Trim() == "Equity")
                    {
                        equityAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                        equityAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));

                    }
                    else if (Convert.ToString(dr["WAC_AssetClassification"]).Trim() == "Debt")
                    {
                        debtAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                        debtAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));

                    }
                    else if (Convert.ToString(dr["WAC_AssetClassification"]).Trim() == "Cash")
                    {
                        cashAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                        cashAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));

                    }
                    else if (Convert.ToString(dr["WAC_AssetClassification"]).Trim() == "Alternate")
                    {
                        alternateAllocationPercent = decimal.Parse(Convert.ToString(dr["CAA_RecommendedPercentage"]));
                        alternateAggAdjustment = decimal.Parse(Convert.ToString(dr["CAA_AgreedAdjustment"]));

                    }

                }

                drFutureSurplusEngine["Year"] = tempYear;
                drFutureSurplusEngine["Income"] = totalIncome;
                drFutureSurplusEngine["IncomeGrowth"] = incomeGrowthRate;
                drFutureSurplusEngine["Expense"] = totalExpense;
                drFutureSurplusEngine["ExpenseGrowth"] = expenseGrowthRate;
                surplusForTheTempYear =Math.Round(totalIncome - totalExpense);
                drFutureSurplusEngine["AvailableSurplus"] = surplusForTheTempYear;

                //**************************Equity******************************
                equityAgreedPercent = Math.Round(equityAllocationPercent + equityAggAdjustment);
                drFutureSurplusEngine["AgrEquityPercent"] = equityAgreedPercent;
                equityAmount = Math.Round((surplusForTheTempYear / 100 * equityAgreedPercent), 2);
                drFutureSurplusEngine["EquityAmount"] = equityAmount;
                if (equityAmount != 0)
                    equityAmountFV = Math.Round(decimal.Parse(FutureValue(double.Parse((equityGrowthRate / 100).ToString()), 1, 0, -double.Parse(equityAmount.ToString()), 0).ToString()), 2);
                drFutureSurplusEngine["EquityFutureValue"] = equityAmountFV;

                //**************************Debt******************************
                debtAgreedPercent = Math.Round(debtAllocationPercent + debtAggAdjustment);
                drFutureSurplusEngine["AgrDebtPercent"] = debtAgreedPercent;
                debtAmount = Math.Round((surplusForTheTempYear / 100 * debtAgreedPercent), 2);
                drFutureSurplusEngine["DebtAmount"] = debtAmount;
                if (debtAmount != 0)
                    debtAmountFV = Math.Round(decimal.Parse(FutureValue(double.Parse((debtGrowthRate / 100).ToString()), 1, 0, -double.Parse(debtAmount.ToString()), 0).ToString()), 2);
                drFutureSurplusEngine["DebtFutureValue"] = debtAmountFV;

                //**************************Cash******************************
                cashAgreedPercent = Math.Round(cashAllocationPercent + cashAggAdjustment);
                drFutureSurplusEngine["AgrCashPercent"] = cashAgreedPercent;
                cashAmount = Math.Round(surplusForTheTempYear / 100 * cashAgreedPercent);
                drFutureSurplusEngine["CashAmount"] = cashAmount;
                if (cashAmount != 0)
                    cashAmountFV = Math.Round(decimal.Parse(FutureValue(double.Parse((cashGrowthRate / 100).ToString()), 1, 0, -double.Parse(cashAmount.ToString()), 0).ToString()));
                drFutureSurplusEngine["CashFutureValue"] = cashAmountFV;

                //**************************Alternate******************************
                alternateAgreedPercent = Math.Round(alternateAllocationPercent + alternateAggAdjustment);
                drFutureSurplusEngine["AgrAlternatePercent"] = alternateAgreedPercent;
                alternateAmount = Math.Round(surplusForTheTempYear / 100 * alternateAgreedPercent);
                drFutureSurplusEngine["alternateAmount"] = alternateAmount;
                if (alternateAmount != 0)
                    alternateAmountFV = decimal.Parse(FutureValue(double.Parse((alternateGrowthRate / 100).ToString()), 1, 0, -double.Parse(alternateAmount.ToString()), 0).ToString());
                drFutureSurplusEngine["alternateFutureValue"] = alternateAmountFV;

                drFutureSurplusEngine["TotalAssetValues"] = Math.Round(equityAmount + debtAmount + cashAmount + alternateAmount);
                drFutureSurplusEngine["TotalAssetFutureValues"] = Math.Round(equityAmountFV + debtAmountFV + cashAmountFV + alternateAmountFV);

                dtFutureSurplusEngine.Rows.Add(drFutureSurplusEngine);

                //***************************************Second Record onwards Record Processing for Rebalancing*************************


                equityGapFromAgreedAllocation = equityAgreedPercent - equityCurrentPercent;
                debtGapFromAgreedAllocation = debtAgreedPercent - debtCurrentPercent;
                cashGapFromAgreedAllocation = cashAgreedPercent - cashCurrentPercent;
                alternateGapFromAgreedAllocation = alternateAgreedPercent - alternateCurrentPercent;


                equityAmountToBeRebalanced = equityGapFromAgreedAllocation / 100 * equityClosingBalance + equityAmount;
                debtAmountToBeRebalanced = debtGapFromAgreedAllocation / 100 * debtClosingBalance + debtAmount;
                cashAmountToBeRebalanced = cashGapFromAgreedAllocation / 100 * cashClosingBalance + cashAmount;
                alternateAmountToBeRebalanced = alternateGapFromAgreedAllocation / 100 * alternateClosingBalance + alternateAmount;

                equityAmountAvailableAfterRebalancingAndWithdrawls = equityClosingBalance - equityGoalFundedAmount + equityAmountToBeRebalanced;
                debtAmountAvailableAfterRebalancingAndWithdrawls = debtClosingBalance - debtGoalFundedAmount + debtAmountToBeRebalanced;
                cashAmountAvailableAfterRebalancingAndWithdrawls = cashClosingBalance - cashGoalFundedAmount + cashAmountToBeRebalanced;
                alternateAmountAvailableAfterRebalancingAndWithdrawls = alternateClosingBalance - alternateGoalFundedAmount + alternateAmountToBeRebalanced;

                if (equityAmountAvailableAfterRebalancingAndWithdrawls != 0)
                    equityAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(equityGrowthRate.ToString()), 1, 0, -double.Parse(equityAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());
                if (debtAmountAvailableAfterRebalancingAndWithdrawls != 0)
                    debtAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(debtGrowthRate.ToString()), 1, 0, -double.Parse(debtAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());
                if (cashAmountAvailableAfterRebalancingAndWithdrawls != 0)
                    cashAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(cashGrowthRate.ToString()), 1, 0, -double.Parse(cashAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());
                if (alternateAmountAvailableAfterRebalancingAndWithdrawls != 0)
                    alternateAmountAvailableAfterRebalancingAndWithdrawlsWithReturns = decimal.Parse(FutureValue(double.Parse(alternateGrowthRate.ToString()), 1, 0, -double.Parse(alternateAmountAvailableAfterRebalancingAndWithdrawls.ToString()), 0).ToString());

                equityBalanceAmount = equityAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + equityAmountFV;
                debtBalanceAmount = debtAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + debtAmountFV;
                cashBalanceAmount = cashAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + cashAmountFV;
                alternateBalanceAmount = alternateAmountAvailableAfterRebalancingAndWithdrawlsWithReturns + alternateAmountFV;

                drEquityRebalancingEngine = dtRebalancingEngine.NewRow();
                drDebtRebalancingEngine = dtRebalancingEngine.NewRow();
                drCashRebalancingEngine = dtRebalancingEngine.NewRow();
                drAlternateRebalancingEngine = dtRebalancingEngine.NewRow();

                drEquityRebalancingEngine["Year"] = tempYear;
                drEquityRebalancingEngine["AssetClass"] = "Equity";
                drEquityRebalancingEngine["PreviousYearClosingBalance"] = equityClosingBalance;
                drEquityRebalancingEngine["AgrredPercent"] = equityAgreedPercent;
                drEquityRebalancingEngine["CurrentAssetAllocationPercent"] = equityCurrentPercent;
                drEquityRebalancingEngine["GapFrpmAgrredPercent"] = equityGapFromAgreedAllocation;
                drEquityRebalancingEngine["AmountBeforeReturns"] = equityAmount;
                drEquityRebalancingEngine["ReturnRate"] = equityGrowthRate;
                drEquityRebalancingEngine["AmountAfterReturns"] = equityAmountFV;
                drEquityRebalancingEngine["GoalMoneyWithdrawn"] = equityGoalFundedAmount;
                drEquityRebalancingEngine["MoneyToBeRebalanced"] = equityAmountToBeRebalanced;
                drEquityRebalancingEngine["MoneyAvailableAfterRW"] = equityAmountAvailableAfterRebalancingAndWithdrawls;
                drEquityRebalancingEngine["MoneyAvailableAfterRWReturn"] = equityAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
                drEquityRebalancingEngine["BalanceMoney"] = equityBalanceAmount;
                dtRebalancingEngine.Rows.Add(drEquityRebalancingEngine);

                drDebtRebalancingEngine["Year"] = tempYear;
                drDebtRebalancingEngine["AssetClass"] = "Debt";
                drDebtRebalancingEngine["PreviousYearClosingBalance"] = debtClosingBalance;
                drDebtRebalancingEngine["AgrredPercent"] = debtAgreedPercent;
                drDebtRebalancingEngine["CurrentAssetAllocationPercent"] = debtCurrentPercent;
                drDebtRebalancingEngine["GapFrpmAgrredPercent"] = debtGapFromAgreedAllocation;
                drDebtRebalancingEngine["AmountBeforeReturns"] = debtAmount;
                drDebtRebalancingEngine["ReturnRate"] = debtGrowthRate;
                drDebtRebalancingEngine["AmountAfterReturns"] = debtAmountFV;
                drDebtRebalancingEngine["GoalMoneyWithdrawn"] = debtGoalFundedAmount;
                drDebtRebalancingEngine["MoneyToBeRebalanced"] = debtAmountToBeRebalanced;
                drDebtRebalancingEngine["MoneyAvailableAfterRW"] = debtAmountAvailableAfterRebalancingAndWithdrawls;
                drDebtRebalancingEngine["MoneyAvailableAfterRWReturn"] = debtAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
                drDebtRebalancingEngine["BalanceMoney"] = debtBalanceAmount;
                dtRebalancingEngine.Rows.Add(drDebtRebalancingEngine);

                drCashRebalancingEngine["Year"] = tempYear;
                drCashRebalancingEngine["AssetClass"] = "Cash";
                drCashRebalancingEngine["PreviousYearClosingBalance"] = cashClosingBalance;
                drCashRebalancingEngine["AgrredPercent"] = cashAgreedPercent;
                drCashRebalancingEngine["CurrentAssetAllocationPercent"] = cashCurrentPercent;
                drCashRebalancingEngine["GapFrpmAgrredPercent"] = cashGapFromAgreedAllocation;
                drCashRebalancingEngine["AmountBeforeReturns"] = cashAmount;
                drCashRebalancingEngine["ReturnRate"] = cashGrowthRate;
                drCashRebalancingEngine["AmountAfterReturns"] = cashAmountFV;
                drCashRebalancingEngine["GoalMoneyWithdrawn"] = cashGoalFundedAmount;
                drCashRebalancingEngine["MoneyToBeRebalanced"] = cashAmountToBeRebalanced;
                drCashRebalancingEngine["MoneyAvailableAfterRW"] = cashAmountAvailableAfterRebalancingAndWithdrawls;
                drCashRebalancingEngine["MoneyAvailableAfterRWReturn"] = cashAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
                drCashRebalancingEngine["BalanceMoney"] = cashBalanceAmount;
                dtRebalancingEngine.Rows.Add(drCashRebalancingEngine);

                drAlternateRebalancingEngine["Year"] = tempYear;
                drAlternateRebalancingEngine["AssetClass"] = "Alternate";
                drAlternateRebalancingEngine["PreviousYearClosingBalance"] = alternateClosingBalance;
                drAlternateRebalancingEngine["AgrredPercent"] = alternateAgreedPercent;
                drAlternateRebalancingEngine["CurrentAssetAllocationPercent"] = alternateCurrentPercent;
                drAlternateRebalancingEngine["GapFrpmAgrredPercent"] = alternateGapFromAgreedAllocation;
                drAlternateRebalancingEngine["AmountBeforeReturns"] = alternateAmount;
                drAlternateRebalancingEngine["ReturnRate"] = alternateGrowthRate;
                drAlternateRebalancingEngine["AmountAfterReturns"] = alternateAmountFV;
                drAlternateRebalancingEngine["GoalMoneyWithdrawn"] = alternateGoalFundedAmount;
                drAlternateRebalancingEngine["MoneyToBeRebalanced"] = alternateAmountToBeRebalanced;
                drAlternateRebalancingEngine["MoneyAvailableAfterRW"] = alternateAmountAvailableAfterRebalancingAndWithdrawls;
                drAlternateRebalancingEngine["MoneyAvailableAfterRWReturn"] = alternateAmountAvailableAfterRebalancingAndWithdrawlsWithReturns;
                drAlternateRebalancingEngine["BalanceMoney"] = alternateBalanceAmount;
                dtRebalancingEngine.Rows.Add(drAlternateRebalancingEngine);
           

                tempYear++;
            }

            dsFPAnalyticsEngine.Tables.Add(dtFutureSurplusEngine);
            dsFPAnalyticsEngine.Tables.Add(dtRebalancingEngine);


            return dsFPAnalyticsEngine;
 
        }

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

    }
}
