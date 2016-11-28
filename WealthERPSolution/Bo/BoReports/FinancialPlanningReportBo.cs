using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

using System.Collections.Specialized;
using VoReports;
using DaoReports;


namespace BoReports
{
    public class FinancialPlanningReportsBo
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public DataSet GetFinancialPlanningReport(FinancialPlanningVo report)
        {
            FinancialPlanningReportsDao financialPlanningReports = new FinancialPlanningReportsDao();
            return financialPlanningReports.GetFinancialPlanningReport(report);
        }

        public DataSet GetCustomerFPDetails(FinancialPlanningVo report, out double asset, out double liabilities, out double netWorth, out string riskClass, out int dynamicRiskClass, out double totalAnnualIncome, out int financialAssetTotal)
        {
            DataSet dsCustomerFPReportDetails;
            DataTable dtHLVAnalysis;
            DataTable dtCurrentObservation;
            DataTable dtHealthAnalysis;
            DataTable dtAssetClass;
            DataTable dtPortfolioAllocation;
            DataTable dtExpense;
            DataTable dtIncome;
            DataTable dtLoanEMI;
            DataTable dtHLVAssumption;
            double HLVbasedIncome = 0;
            double inflationPer = 0;
            double discountRate = 0;
            double yearsLeftRT = 0;
            double totalIncomeMonthly = 0;
            double totalExpenseMonthly = 0;
            double sumAssuredLI;
            DataTable dtHLVBasedIncome = new DataTable("HLVBasedIncome");
            double surplus = 0;
            double lifeProtectionTotal = 0;
            double totalEquity = 0;
            double totalMF = 0;
            double totalFixedIncome = 0;
            double totalOther = 0;
            double homeLoan = 0;
            double autoLoan = 0;
            double currEquity = 0;
            double toatlGoalAmount = 0;
            DataTable dtCustomerFPRatio = new DataTable("CustomerFPRatio");
            double loan_To_Assets_Ratio = 0;
            double savings_To_Income_Ratio = 0;
            double debt_Ratio = 0;
            double debt_To_Income_Ratio = 0;
            double solvency_Ratio = 0;
            double life_Insurance_Cover_Ratio = 0;
            double annualSalary = 0;
            double cashAndSaving = 0;
            double totalMonthlyEMI = 0;
            double totalIncomeAnnual = 0;

            Dictionary<string, decimal> dicCustomerFPRatio = new Dictionary<string, decimal>();

            string strInvestment = string.Empty;
            string strHomeLoan = string.Empty;
            string strAutoLoan = string.Empty;
            FinancialPlanningReportsDao financialPlanningReports = new FinancialPlanningReportsDao();
            dsCustomerFPReportDetails = financialPlanningReports.GetCustomerFPDetails(report, out asset, out liabilities, out netWorth, out riskClass, out sumAssuredLI, out dynamicRiskClass, out financialAssetTotal);
            dtHLVAnalysis = dsCustomerFPReportDetails.Tables["HLV"];
            dtAssetClass = dsCustomerFPReportDetails.Tables["AdvisorRiskClass"];
            dtPortfolioAllocation = dsCustomerFPReportDetails.Tables["AdvisorPortfolioAllocation"];
            dsCustomerFPReportDetails.Tables.Remove("HLV");
            dsCustomerFPReportDetails.AcceptChanges();
            dtExpense = dsCustomerFPReportDetails.Tables["Expense"];
            dtIncome = dsCustomerFPReportDetails.Tables["Income"];
            dtLoanEMI = dsCustomerFPReportDetails.Tables["LoanEMI"];
            dtHLVAssumption = dsCustomerFPReportDetails.Tables["HLVAssumption"];
            //**************GEARING RATIO*****************

            if (asset != 0)
                loan_To_Assets_Ratio = (liabilities / asset) * 100;


            double.TryParse(dtExpense.Compute("Sum(ExpenseAmount)", "").ToString(), out totalExpenseMonthly);
            foreach (DataRow dr in dtIncome.Rows)
            {
                //if (dr["IncomeCategory"].ToString().Trim() == "Salary")
                if (dr["IncomeCategory"].ToString().Trim() == "Gross Salary")
                {
                    annualSalary = Convert.ToDouble(Math.Round(double.Parse(dr["IncomeAmount"].ToString())).ToString());
                }
                totalIncomeAnnual += Convert.ToDouble(Math.Round(double.Parse(dr["IncomeAmount"].ToString())).ToString()) * 12;
            }

            if (Convert.ToString(dtLoanEMI.Rows[0][0]) != string.Empty)
            {
                totalMonthlyEMI = double.Parse(dtLoanEMI.Rows[0][0].ToString()) / 12;
            }
            //**************LIFE INSURANCE COVER*****************

            if (annualSalary != 0)
                life_Insurance_Cover_Ratio = sumAssuredLI / (annualSalary * 12);
            //Objetc sumObject = _itemCheckData.Table.Compute("Sum(reco_price)", "");
            // dtExpense.Sum("ExpenseAmount");
            //dsCustomerFPReportDetails.Tables.RemoveAt(16);
            //dsCustomerFPReportDetails.AcceptChanges();
            //dsCustomerFPReportDetails.Tables.RemoveAt(16);
            //dsCustomerFPReportDetails.AcceptChanges();
            foreach (DataRow dr in dtHLVAssumption.Rows)
            {
                if (dr["Assumption_Type"].ToString().Trim() == "Discount Rate")
                {
                    if (!string.IsNullOrEmpty(dr["Assumption_Values"].ToString()))
                        discountRate = double.Parse(dr["Assumption_Values"].ToString());
                }
            }

            foreach (DataRow dr in dtHLVAnalysis.Rows)
            {
                if (dr["HLV_Type"].ToString().Trim() == "Inflation Rate")
                {
                    if (!string.IsNullOrEmpty(dr["HLV_Values"].ToString()))
                        inflationPer = double.Parse(dr["HLV_Values"].ToString());
                }

                //if (dr["HLV_Type"].ToString().Trim() == "Discount Rate")
                //{
                //    if (!string.IsNullOrEmpty(dr["HLV_Values"].ToString()))
                //        discountRate = double.Parse(dr["HLV_Values"].ToString());
                //}

                if (dr["HLV_Type"].ToString().Trim() == "Years left till retirement")
                {
                    if (!string.IsNullOrEmpty(dr["HLV_Values"].ToString()))
                        yearsLeftRT = double.Parse(dr["HLV_Values"].ToString());
                }

                if (dr["HLV_Type"].ToString().Trim() == "Income")
                {
                    if (!string.IsNullOrEmpty(dr["HLV_Values"].ToString()))
                        totalIncomeMonthly = double.Parse(dr["HLV_Values"].ToString());
                }

            }
            //**************SAVING RATIO*****************
            if (totalIncomeMonthly != 0)
                savings_To_Income_Ratio = ((totalIncomeMonthly - totalExpenseMonthly) / totalIncomeMonthly) * 100;
            //**************SAVING RATIO*****************


            //**************Debt Service Ratio *****************
            if (totalIncomeMonthly != 0)
                debt_To_Income_Ratio = (totalMonthlyEMI / totalIncomeMonthly) * 100;
            //**************Debt Service Ratio *****************


            //**************Debt Ratio *****************
            if (netWorth != 0)
                debt_Ratio = liabilities / netWorth;
            //**************Debt Ratio *****************

            HLVbasedIncome = PV(discountRate / 100, yearsLeftRT, -(totalIncomeAnnual), 0, 1);
            totalAnnualIncome = totalIncomeMonthly * 12;
            DataRow drHLVbasedAnalysis;
            drHLVbasedAnalysis = dtHLVAnalysis.NewRow();
            drHLVbasedAnalysis["HLV_Type"] = "HLV based on income";
            drHLVbasedAnalysis["HLV_Values"] = convertUSCurrencyFormat(Math.Round(double.Parse(HLVbasedIncome.ToString()), 0));
            dtHLVAnalysis.Rows.Add(drHLVbasedAnalysis);

            drHLVbasedAnalysis = dtHLVAnalysis.NewRow();
            drHLVbasedAnalysis["HLV_Type"] = "Income(annual)";
            drHLVbasedAnalysis["HLV_Values"] = convertUSCurrencyFormat(Math.Round(double.Parse((totalIncomeAnnual).ToString()), 0));
            dtHLVAnalysis.Rows.InsertAt(drHLVbasedAnalysis, 0);
            dtHLVAnalysis.Rows.RemoveAt(1);
            dsCustomerFPReportDetails.Tables.Add(dtHLVAnalysis);



            dtHLVBasedIncome.Columns.Add("HLVIncomeType");
            dtHLVBasedIncome.Columns.Add("HLVIncomeValue");
            DataRow drHLVBasedIncome;

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Financial Net Worth";
            drHLVBasedIncome["HLVIncomeValue"] = convertUSCurrencyFormat(Math.Round(netWorth, 0));
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Insurance Cover Recommended(HLV – Financial Net Worth)";
            drHLVBasedIncome["HLVIncomeValue"] = convertUSCurrencyFormat(Math.Round(double.Parse((HLVbasedIncome - netWorth).ToString()), 0));
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Current Insurance Cover";
            drHLVBasedIncome["HLVIncomeValue"] = convertUSCurrencyFormat(Math.Round(sumAssuredLI));
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Insurance Cover Required";
            drHLVBasedIncome["HLVIncomeValue"] = convertUSCurrencyFormat(Math.Round(double.Parse(((HLVbasedIncome - netWorth) - sumAssuredLI).ToString()), 0));
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            dsCustomerFPReportDetails.Tables.Add(dtHLVBasedIncome);

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables["CashFlow"].Rows)
            {
                if (dr["CashCategory"].ToString() == "Annual Surplus")
                {
                    if (!string.IsNullOrEmpty(dr["Amount"].ToString()))
                        surplus = double.Parse(dr["Amount"].ToString());
                }

            }

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables["LifeInsurance"].Rows)
            {
                if (!string.IsNullOrEmpty(dr["InsuranceValues"].ToString()))
                {
                    lifeProtectionTotal += double.Parse(dr["InsuranceValues"].ToString());
                }
            }

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables["AssetDetails"].Rows)
            {
                if (dr["AssetGroupCode"].ToString() == "MF")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalMF = double.Parse(dr["AssetValues"].ToString());
                }
                else if (dr["AssetGroupCode"].ToString() == "DE")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalEquity = double.Parse(dr["AssetValues"].ToString());
                }
                else if (dr["AssetGroupCode"].ToString() == "FI")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalFixedIncome = double.Parse(dr["AssetValues"].ToString());
                }
                else if (dr["AssetGroupCode"].ToString() == "OT" || dr["AssetGroupCode"].ToString() == "PG" || dr["AssetGroupCode"].ToString() == "GS" || dr["AssetGroupCode"].ToString() == "CS" || dr["AssetGroupCode"].ToString() == "SP" || dr["AssetGroupCode"].ToString() == "PM" || dr["AssetGroupCode"].ToString() == "GD" || dr["AssetGroupCode"].ToString() == "CM" || dr["AssetGroupCode"].ToString() == "CL")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalOther += double.Parse(dr["AssetValues"].ToString());

                    if (dr["AssetGroupCode"].ToString() == "CS")
                    {
                        if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                            cashAndSaving = double.Parse(dr["AssetValues"].ToString());

                    }

                }

            }

            //**************Emergency Fund Ratio:*****************
            if (totalExpenseMonthly != 0)
                solvency_Ratio = cashAndSaving / totalExpenseMonthly;
            //**************Emergency Fund Ratio:*****************

            if (totalEquity > 0)
            {
                strInvestment = "Your current investments are ` " + convertUSCurrencyFormat(totalEquity) + " in Equity";
            }
            else
                strInvestment = string.Empty;

            if (totalMF > 0)
            {
                if (!string.IsNullOrEmpty(strInvestment))
                {
                    if (totalFixedIncome > 0 || totalOther > 0)
                        strInvestment += ", " + "` " + convertUSCurrencyFormat(totalMF) + " in Mutual Fund";
                    else if (totalFixedIncome == 0 && totalOther == 0)
                    {
                        strInvestment += " and " + "` " + convertUSCurrencyFormat(totalMF) + " in Mutual Fund";
                    }

                }
                else
                    strInvestment = "Your current investments are ` " + convertUSCurrencyFormat(totalMF) + " in Mutual Fund";
            }
            if (totalFixedIncome > 0)
            {
                if (!string.IsNullOrEmpty(strInvestment))
                {
                    if (totalOther > 0)
                        strInvestment += ", " + "` " + convertUSCurrencyFormat(totalFixedIncome) + " in Fixed Income ";
                    else
                        strInvestment += " and " + "` " + convertUSCurrencyFormat(totalFixedIncome) + " in Fixed Income ";
                }
                else
                    strInvestment = "Your current investments are ` " + convertUSCurrencyFormat(totalFixedIncome) + " in Fixed Income ";

            }
            if (totalOther > 0)
            {
                if (!string.IsNullOrEmpty(strInvestment))
                    strInvestment += " and " + "` " + convertUSCurrencyFormat(totalOther) + " in Others";
                else
                    strInvestment = "Your current investments is ` " + convertUSCurrencyFormat(totalOther) + " in Others";

            }

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables["LiabilitiesDetail"].Rows)
            {
                if (dr["LoanType"].ToString() == "Home Loan")
                {
                    if (dr["LoanValues"] != null)
                    {
                        strHomeLoan = "Principal Outstanding is ` " + convertUSCurrencyFormat(double.Parse(dr["LoanValues"].ToString()));
                    }

                }
                else if (dr["LoanType"].ToString() == "Home Loan")
                {
                    if (dr["LoanValues"] != null)
                    {
                        strAutoLoan = "Principal Outstanding is ` " + convertUSCurrencyFormat(double.Parse(dr["LoanValues"].ToString()));
                    }


                }
            }


            dtCurrentObservation = new DataTable("CurrentObservation");
            dtCurrentObservation.Columns.Add("ObjType");
            dtCurrentObservation.Columns.Add("ObjSummary");
            DataRow drCurrentObservation;
            if (Math.Abs(netWorth) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Net Worth";
                drCurrentObservation["ObjSummary"] = "Your current Net Worth is ` " + convertUSCurrencyFormat(netWorth).ToString();
                dtCurrentObservation.Rows.Add(drCurrentObservation);

            }
            if (Math.Abs(surplus) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Cash flow";
                drCurrentObservation["ObjSummary"] = "Your current surplus is ` " + convertUSCurrencyFormat(surplus).ToString() + " per year";
                dtCurrentObservation.Rows.Add(drCurrentObservation);
            }
            if (Math.Abs(lifeProtectionTotal) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Protection";
                drCurrentObservation["ObjSummary"] = "You have Life insurance protection of ` " + convertUSCurrencyFormat(lifeProtectionTotal).ToString();
                dtCurrentObservation.Rows.Add(drCurrentObservation);
            }
            if (Math.Abs(totalMF) > 0 || Math.Abs(totalEquity) > 0 || Math.Abs(totalFixedIncome) > 0 || Math.Abs(totalOther) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Investments";
                drCurrentObservation["ObjSummary"] = strInvestment;
                dtCurrentObservation.Rows.Add(drCurrentObservation);

            }

            if (Math.Abs(homeLoan) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Home Loan";
                drCurrentObservation["ObjSummary"] = strHomeLoan;
                dtCurrentObservation.Rows.Add(drCurrentObservation);
            }
            if (Math.Abs(autoLoan) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Car Loan";
                drCurrentObservation["ObjSummary"] = strAutoLoan;
                dtCurrentObservation.Rows.Add(drCurrentObservation);
            }
            dsCustomerFPReportDetails.Tables.Add(dtCurrentObservation);

            dtHealthAnalysis = new DataTable("HealthAnalysis");
            dtHealthAnalysis.Columns.Add("Ratio");
            dtHealthAnalysis.Columns.Add("value");
            DataRow drHealthAnalysis;

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables["RiskProfile"].Rows)
            {
                if (dr["Class"].ToString() == "Equity")
                {
                    currEquity = double.Parse(dr["CurrentPercentage"].ToString());

                }

            }
            drHealthAnalysis = dtHealthAnalysis.NewRow();
            //drHealthAnalysis["Ratio"] = "Financial Asset allocation -equity(%)";
            //drHealthAnalysis["value"] = convertUSCurrencyFormat(Math.Round(currEquity, 3)).ToString();
            drHealthAnalysis["Ratio"] = "Gearing Ratio (%)";

            drHealthAnalysis["value"] = 39;
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables["MonthlyGoalTotal"].Rows)
            {
                if (!string.IsNullOrEmpty(dr["MonthyTotal"].ToString()))
                    toatlGoalAmount = double.Parse(dr["MonthyTotal"].ToString());
            }

            drHealthAnalysis = dtHealthAnalysis.NewRow();
            //drHealthAnalysis["Ratio"] = "Savings/Income Ratio";
            drHealthAnalysis["Ratio"] = "Savings Ratio (%)";
            if (totalIncomeMonthly != 0)
                //drHealthAnalysis["value"] =Math.Round((toatlGoalAmount / totalIncome), 3).ToString();
                drHealthAnalysis["value"] = 56;
            else
                drHealthAnalysis["value"] = 56;
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            drHealthAnalysis = dtHealthAnalysis.NewRow();
            //drHealthAnalysis["Ratio"] = "Loan/Financial Assets Ratio";
            drHealthAnalysis["Ratio"] = "Emergency Fund Ratio (Months)";
            if (asset != 0)
                //drHealthAnalysis["value"] = Math.Round((liabilities / asset), 3).ToString();
                drHealthAnalysis["value"] = 50;
            else
                drHealthAnalysis["value"] = 50;
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            dsCustomerFPReportDetails.Tables.Add(dtHealthAnalysis);
            //*****************Test Scenario-1*****************
            //debt_Ratio = -2;
            //debt_To_Income_Ratio = 44;            
            //life_Insurance_Cover_Ratio = 1;
            //loan_To_Assets_Ratio = 43;
            //savings_To_Income_Ratio = 14;
            //solvency_Ratio = 2;

            ////*****************Test Scenario-2*****************
            //debt_Ratio = -1;
            //debt_To_Income_Ratio = 45;
            //life_Insurance_Cover_Ratio = 2;
            //loan_To_Assets_Ratio = 44;
            //savings_To_Income_Ratio = 15;
            //solvency_Ratio = 3;

            ////*****************Test Scenario-3*****************
            //debt_Ratio = 0;
            //debt_To_Income_Ratio = 46;
            //life_Insurance_Cover_Ratio = 3;
            //loan_To_Assets_Ratio = 45;
            //savings_To_Income_Ratio = 16;
            //solvency_Ratio = 4;

            ////*****************Test Scenario-4*****************
            //debt_Ratio = 0.5;
            //debt_To_Income_Ratio = 50;
            //life_Insurance_Cover_Ratio = 3.5;
            //loan_To_Assets_Ratio = 50;
            //savings_To_Income_Ratio = 20;
            //solvency_Ratio = 10;


            ////*****************Test Scenario-5*****************
            //debt_Ratio = 1;
            //debt_To_Income_Ratio = 65;
            //life_Insurance_Cover_Ratio = 4;
            //loan_To_Assets_Ratio = 59;
            //savings_To_Income_Ratio = 25;
            //solvency_Ratio = 12;

            ////*****************Test Scenario-6*****************
            //debt_Ratio = 2;
            //debt_To_Income_Ratio = 66;
            //life_Insurance_Cover_Ratio = 5;
            //loan_To_Assets_Ratio = 60;
            //savings_To_Income_Ratio = 26;
            //solvency_Ratio = 13;

            ////*****************Test Scenario-7*****************
            //debt_Ratio = 3;
            //debt_To_Income_Ratio = 67;
            //life_Insurance_Cover_Ratio = 6;
            //loan_To_Assets_Ratio = 61;
            //savings_To_Income_Ratio = 27;
            //solvency_Ratio = 14;

            //adding customer all ratio to dictionary.
            foreach (DataRow dr in dsCustomerFPReportDetails.Tables["FPRatio"].Rows)
            {
                switch (dr[0].ToString())
                {
                    case "1":
                        {
                            dicCustomerFPRatio.Add(dr[0].ToString(), decimal.Parse(Math.Round(savings_To_Income_Ratio, 2).ToString()));
                            break;
                        }
                    case "2":
                        {
                            dicCustomerFPRatio.Add(dr[0].ToString(), decimal.Parse(Math.Round(debt_To_Income_Ratio, 2).ToString()));
                            break;
                        }
                    case "4":
                        {
                            dicCustomerFPRatio.Add(dr[0].ToString(), decimal.Parse(Math.Round(solvency_Ratio, 2).ToString()));
                            break;
                        }
                    case "5":
                        {
                            dicCustomerFPRatio.Add(dr[0].ToString(), decimal.Parse(Math.Round(debt_Ratio, 2).ToString()));
                            break;
                        }
                    case "8":
                        {
                            dicCustomerFPRatio.Add(dr[0].ToString(), decimal.Parse(Math.Round(loan_To_Assets_Ratio, 2).ToString()));
                            break;
                        }
                    case "10":
                        {
                            dicCustomerFPRatio.Add(dr[0].ToString(), decimal.Parse(Math.Round(life_Insurance_Cover_Ratio, 2).ToString()));

                            break;
                        }

                    default:
                        break;

                }
            }
            //dicCustomerFPRatio.Add("Savings Ratio", int.Parse(Math.Round(savingRatio).ToString()));
            //dicCustomerFPRatio.Add("Debt service ratio", int.Parse(Math.Round(debtServiceRatio).ToString()));
            //dicCustomerFPRatio.Add("Emergency fund ratio", int.Parse(Math.Round(emergencyFundRatio).ToString()));
            //dicCustomerFPRatio.Add("Gearing Ratio", int.Parse(Math.Round(gearingRatio).ToString()));
            //dicCustomerFPRatio.Add("Life Insurance Cover", int.Parse(Math.Round(lifeInsuranceCoverRatio).ToString()));


            //dsCustomerFPReportDetails.Tables.Add("AssetClass",dtAssetClass);

            //dsCustomerFPReportDetails.Tables.Add(dtPortfolioAllocation);


            //dtAllGoalChart = new DataTable();
            //dtAllGoalChart.Columns.Add("GoalYear");
            //dtAllGoalChart.Columns.Add("GoalAmount");
            //DataRow drAllGoalChart;
            //if (dsCustomerFPReportDetails.Tables[5].Rows.Count > 0)
            //{
            //    foreach (DataRow drOtherGoal in dsCustomerFPReportDetails.Tables[5].Rows)
            //    {
            //        drAllGoalChart = dtAllGoalChart.NewRow();
            //        drAllGoalChart["GoalYear"] = drOtherGoal["GoalYear"];
            //        drAllGoalChart["GoalAmount"] = drOtherGoal["GoalAmount"];
            //        dtAllGoalChart.Rows.Add(drAllGoalChart);
            //    }
            //}

            //if (dsCustomerFPReportDetails.Tables[7].Rows.Count > 0)
            //{
            //    foreach (DataRow drOtherGoal in dsCustomerFPReportDetails.Tables[7].Rows)
            //    {
            //        drAllGoalChart = dtAllGoalChart.NewRow();
            //        drAllGoalChart["GoalYear"] = drOtherGoal["GoalYear"];
            //        drAllGoalChart["GoalAmount"] = drOtherGoal["FVofCostToday"];
            //        dtAllGoalChart.Rows.Add(drAllGoalChart);
            //    }
            //}
            //dsCustomerFPReportDetails.Tables.Add(dtAllGoalChart);
            dtCustomerFPRatio = createCustomerRatioTable(dsCustomerFPReportDetails.Tables["FPRatio"], dsCustomerFPReportDetails.Tables["FPRatioDetails"], dicCustomerFPRatio);
            dsCustomerFPReportDetails.Tables.Add(dtCustomerFPRatio);
            return dsCustomerFPReportDetails;
        }

        private string convertUSCurrencyFormat(double value)
        {
            string strValues = string.Empty;
            if (value != 0)
                strValues = value.ToString("#,#", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            else
                strValues = value.ToString();
            return strValues;
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
        public DataSet GetFPQuestionnaire(FPOfflineFormVo report, int adviserId)
        {
            FinancialPlanningReportsDao financialPlanningReportsDao = new FinancialPlanningReportsDao();
            DataSet dtFPQuestionnaire = new DataSet();

            try
            {
                DataSet dsFPQuestionnaire = financialPlanningReportsDao.GetFPQuestionnaire(report, adviserId);
                dtFPQuestionnaire = dsFPQuestionnaire;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtFPQuestionnaire;

        }

        public DataTable createCustomerRatioTable(DataTable dtFpRatio, DataTable dtFpRatioDetails, Dictionary<string, decimal> dicCustomerFPRatio)
        {
            DataTable dtCustomerFPRatio = new DataTable("CustomerFPRatio");
            dtCustomerFPRatio.Columns.Add("RatioId");
            dtCustomerFPRatio.Columns.Add("RatioName");
            dtCustomerFPRatio.Columns.Add("RatioPunchLine");
            dtCustomerFPRatio.Columns.Add("RatioValue");
            dtCustomerFPRatio.Columns.Add("RatioRangeOne");
            dtCustomerFPRatio.Columns.Add("RatioRangeTwo");
            dtCustomerFPRatio.Columns.Add("RatioRangeThree");
            dtCustomerFPRatio.Columns.Add("RatioColor");
            dtCustomerFPRatio.Columns.Add("RatioDescription");
            DataRow drCustomerFPRatio;
            DataRow[] drCurrentRow;
            foreach (DataRow dr in dtFpRatio.Rows)
            {
                string strRatioRangeOne = "";
                string strRatioRangeTwo = "";
                string strRatioRangeThree = "";
                string strRatioColor = "";
                string strRatioRangeDescription = "";
                decimal tempRatiovalue = 0;
                drCustomerFPRatio = dtCustomerFPRatio.NewRow();
                drCustomerFPRatio["RatioId"] = dr["WFFR_RatioId"];
                drCustomerFPRatio["RatioName"] = dr["AFFR_Ratio"];
                drCustomerFPRatio["RatioPunchLine"] = dr["AFFR_RatioDescription"];
                //drCurrentRow = dtFpRatioDetails.Select("AFFR_Ratio='" + dr["AFFR_Ratio"].ToString()+ "'");
                drCurrentRow = dtFpRatioDetails.Select("WFFR_RatioId=" + int.Parse(dr["WFFR_RatioId"].ToString()).ToString());
                foreach (DataRow drRatio in drCurrentRow)
                {
                    int tempLower = 0;
                    int tempUpper = 0;
                    if (Convert.ToString(drRatio["AFFRDR_RangeLowerLimit"]) != String.Empty)
                        tempLower = int.Parse(drRatio["AFFRDR_RangeLowerLimit"].ToString());

                    if (Convert.ToString(drRatio["AFFRDR_RangeUpperLimit"]) != String.Empty)
                        tempUpper = int.Parse(drRatio["AFFRDR_RangeUpperLimit"].ToString());


                    //strRatioRange += tempLower.ToString() + "-" + tempUpper.ToString()+"--";

                    if (string.IsNullOrEmpty(strRatioRangeOne.Trim()))
                    {
                        if (tempLower == 0 && tempUpper != 0)
                        {
                            if (Convert.ToInt16(dr["WFFR_RatioId"].ToString()) == 5)
                                strRatioRangeOne = "<=" + (tempUpper + 1).ToString();
                            else
                                strRatioRangeOne = "<" + (tempUpper + 1).ToString();

                        }
                        //else if (tempLower != 0 && tempUpper != 0)
                        //{
                        //    strRatioRangeOne = tempLower.ToString() + "-" + tempUpper.ToString();
                        //}

                    }
                    if (string.IsNullOrEmpty(strRatioRangeTwo.Trim()))
                    {
                        if (tempLower != 0 && tempUpper != 0)
                        {
                            strRatioRangeTwo = tempLower.ToString() + "-" + tempUpper.ToString();

                        }
                        else if (tempLower == 0 && int.Parse(drRatio["WFFR_RatioId"].ToString()) == 5 && Convert.ToString(drRatio["AFFRDR_RangeColour"]) == "Yellow" && tempUpper != 0)
                        {
                            strRatioRangeTwo = tempLower.ToString() + "-" + tempUpper.ToString();
                        }

                    }
                    if (string.IsNullOrEmpty(strRatioRangeThree.Trim()))
                    {
                        if (tempUpper == 0 && tempLower != 0)
                        {
                            strRatioRangeThree = ">" + (tempLower - 1).ToString();

                        }
                        //else if (tempLower != 0 && tempUpper != 0)
                        //{
                        //    strRatioRangeThree = tempLower.ToString() + "-" + tempUpper.ToString();
                        //}

                    }



                    if (dicCustomerFPRatio.ContainsKey(drRatio["WFFR_RatioId"].ToString())) // True
                    {
                        tempRatiovalue = dicCustomerFPRatio[(drRatio["WFFR_RatioId"].ToString())];

                    }
                    if (tempLower != 0 && tempUpper != 0 && tempRatiovalue >= tempLower && tempRatiovalue <= tempUpper)
                    {
                        if (string.IsNullOrEmpty(strRatioRangeDescription))
                        {
                            //strRatioColor = drRatio["AFFRDR_RangeColour"].ToString();
                            strRatioRangeDescription = drRatio["AFFRDR_RangeDescription"].ToString();
                        }
                    }
                    else if (tempUpper != 0 && tempRatiovalue <= tempUpper)
                    {
                        if (string.IsNullOrEmpty(strRatioRangeDescription))
                        {
                            //strRatioColor = drRatio["AFFRDR_RangeColour"].ToString();
                            strRatioRangeDescription = drRatio["AFFRDR_RangeDescription"].ToString();
                        }

                    }
                    else if (tempLower != 0 && tempRatiovalue >= tempLower)
                    {
                        if (string.IsNullOrEmpty(strRatioRangeDescription))
                        {
                            //strRatioColor = drRatio["AFFRDR_RangeColour"].ToString();
                            strRatioRangeDescription = drRatio["AFFRDR_RangeDescription"].ToString();
                        }

                    }
                    strRatioColor += drRatio["AFFRDR_RangeColour"].ToString() + "~";
                    tempLower = 0;
                    tempUpper = 0;

                }

                drCustomerFPRatio["RatioValue"] = tempRatiovalue;
                drCustomerFPRatio["RatioRangeOne"] = strRatioRangeOne;
                drCustomerFPRatio["RatioRangeTwo"] = strRatioRangeTwo;
                drCustomerFPRatio["RatioRangeThree"] = strRatioRangeThree;
                drCustomerFPRatio["RatioColor"] = strRatioColor;
                drCustomerFPRatio["RatioDescription"] = strRatioRangeDescription;
                tempRatiovalue = 0;
                strRatioRangeOne = string.Empty;
                strRatioRangeTwo = string.Empty;
                strRatioRangeThree = string.Empty;
                strRatioColor = string.Empty;
                strRatioRangeDescription = string.Empty;

                dtCustomerFPRatio.Rows.Add(drCustomerFPRatio);

            }


            return dtCustomerFPRatio;

        }
        public DataSet GetWelComeNoteDetails(long associateId, out string footerText, out string headerText, out string ReportfooterText, out string TableContentText, out string HeaderLogo) 
        {
            FinancialPlanningReportsDao financialPlanningReports = new FinancialPlanningReportsDao();
            DataSet dsWelComeNoteDetails;
            headerText = "";
            footerText = "";
            ReportfooterText ="";
            TableContentText = "";
            HeaderLogo = "";
            dsWelComeNoteDetails = financialPlanningReports.GetWelComeNoteDetails(associateId);
            DataTable dtReportSectionAndText = dsWelComeNoteDetails.Tables["ReportSection"];
            DataTable dtAssociateDetails = dsWelComeNoteDetails.Tables["AssociateDetails"];
            DataTable dtFieldCode = dsWelComeNoteDetails.Tables["FieldCode"];
            if (dsWelComeNoteDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dtReportSectionAndText.Rows)
                {

                    switch (dr["TextPurpose"].ToString())
                    {
                        case "WelCome_Note_Header":
                            {

                                if (string.IsNullOrEmpty(headerText.Trim()))
                                    headerText = dr["TextParagraph"].ToString();

                                if (dr["HasFieldCode"].ToString().Trim() == "Y")
                                {
                                    foreach (DataRow dr1 in dtFieldCode.Rows)
                                    {
                                        switch (dr1["FieldCode"].ToString())
                                        {
                                            case "#RMName#":
                                                {
                                                    headerText = headerText.Replace("#RMName#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["RMName"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["RMName"].ToString() : string.Empty);
                                                    break;
                                                }
                                            case "#AssociateName#":
                                                {
                                                    headerText = headerText.Replace("#AssociateName#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["Name"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["Name"].ToString() : string.Empty);
                                                    break;
                                                }
                                            case "#Address1#":
                                                {
                                                    headerText = headerText.Replace("#Address1#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["Address1"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["Address1"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#Address2#":
                                                {
                                                    headerText = headerText.Replace("#Address2#" , !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["Address2"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["Address2"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#City#":
                                                {
                                                    headerText = headerText.Replace("#City#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["City"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["City"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#State#":
                                                {
                                                    headerText = headerText.Replace("#State#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["State"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["State"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#Pin#":
                                                {
                                                    headerText = headerText.Replace("#Pin#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["PinCode"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["PinCode"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#AgentCode#":
                                                {
                                                    headerText = headerText.Replace("#AgentCode#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["AgentCode"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["AgentCode"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#BankName#":
                                                {
                                                    headerText = headerText.Replace("#BankName#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["BankName"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["BankName"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#BankBranch#":
                                                {
                                                    headerText = headerText.Replace("#BankBranch#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["BankBranch"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["BankBranch"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#AccountNo#":
                                                {
                                                    headerText = headerText.Replace("#AccountNo#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["AccountNum"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["AccountNum"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#PANNo#":
                                                {
                                                    headerText = headerText.Replace("#PANNo#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["PAN"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["PAN"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#ARNNo#":
                                                {
                                                    headerText = headerText.Replace("#ARNNo#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["ARNNo"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["ARNNo"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#EUIN#":
                                                {
                                                    headerText = headerText.Replace("#EUIN#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["EUIN"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["EUIN"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#Branch#":
                                                {
                                                    headerText = headerText.Replace("#Branch#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["BranchName"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["BranchName"].ToString() : string.Empty );
                                                    break;
                                                }
                                            case "#ProductList#":
                                                {
                                                    headerText = headerText.Replace("#ProductList#", !string.IsNullOrEmpty(dtAssociateDetails.Rows[0]["ProductList"].ToString().Trim()) ? dtAssociateDetails.Rows[0]["ProductList"].ToString() : string.Empty);
                                                    break;
                                                }
                                        }
                                    }
                                }
                                break;
                            }
                        case "WelCome_Note_Footer":
                            {
                                if (string.IsNullOrEmpty(footerText.Trim()))
                                    footerText = dr["TextParagraph"].ToString();

                                break;
                            }
                        case "WelCome_Note_PageFooter":
                                {
                                 if (string.IsNullOrEmpty(ReportfooterText.Trim()))
                                 ReportfooterText = dr["TextParagraph"].ToString();
                                break;
                                }
                        case "WelCome_Note_TableContent":
                                {
                                    if (string.IsNullOrEmpty(TableContentText.Trim()))
                                        TableContentText = dr["TextParagraph"].ToString();
                                    break;

                                }
                        case "WelCome_Note_Logo":
                                {
                                    if (string.IsNullOrEmpty(HeaderLogo.Trim()))
                                        HeaderLogo = dr["TextParagraph"].ToString();
                                    break;

                                }

                    }
                }

            }
            return dsWelComeNoteDetails;
        }
    }
}
