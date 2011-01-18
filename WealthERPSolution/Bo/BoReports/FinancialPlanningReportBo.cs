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

        public DataSet GetCustomerFPDetails(FinancialPlanningVo report, out double asset, out double liabilities, out double netWorth, out string riskClass)
        {
            DataSet dsCustomerFPReportDetails;
            DataTable dtHLVAnalysis;
            DataTable dtCurrentObservation;
            DataTable dtHealthAnalysis;
            DataTable dtAssetClass;
            DataTable dtPortfolioAllocation;            
            double HLVbasedIncome = 0;
            double inflationPer = 0;
            double yearsLeftEOL = 0;
            double totalIncome = 0;
            double sumAssuredLI;
            DataTable dtHLVBasedIncome = new DataTable();
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
            string strInvestment = string.Empty;
            string strHomeLoan = string.Empty;
            string strAutoLoan = string.Empty;
            FinancialPlanningReportsDao financialPlanningReports = new FinancialPlanningReportsDao();
            dsCustomerFPReportDetails = financialPlanningReports.GetCustomerFPDetails(report, out asset, out liabilities, out netWorth, out riskClass, out sumAssuredLI);
            dtHLVAnalysis = dsCustomerFPReportDetails.Tables[16];
            dtAssetClass = dsCustomerFPReportDetails.Tables[17];
            dtPortfolioAllocation = dsCustomerFPReportDetails.Tables[18];
            dsCustomerFPReportDetails.Tables.RemoveAt(16);
            dsCustomerFPReportDetails.AcceptChanges();
            //dsCustomerFPReportDetails.Tables.RemoveAt(16);
            //dsCustomerFPReportDetails.AcceptChanges();
            //dsCustomerFPReportDetails.Tables.RemoveAt(16);
            //dsCustomerFPReportDetails.AcceptChanges();
            
            foreach (DataRow dr in dtHLVAnalysis.Rows)
            {
                if (dr["HLV_Type"].ToString().Trim() == "Inflation Rate")
                {
                    if (!string.IsNullOrEmpty(dr["HLV_Values"].ToString()))
                        inflationPer = double.Parse(dr["HLV_Values"].ToString());
                }


                if (dr["HLV_Type"].ToString().Trim() == "Years left till EOL")
                {
                    if (!string.IsNullOrEmpty(dr["HLV_Values"].ToString()))
                        yearsLeftEOL = double.Parse(dr["HLV_Values"].ToString());
                }

                if (dr["HLV_Type"].ToString().Trim() == "Income")
                {
                    if (!string.IsNullOrEmpty(dr["HLV_Values"].ToString()))
                        totalIncome = double.Parse(dr["HLV_Values"].ToString());
                }

            }
            HLVbasedIncome = PV(inflationPer, yearsLeftEOL, totalIncome, 0, 0);

            DataRow drHLVbasedAnalysis;
            drHLVbasedAnalysis = dtHLVAnalysis.NewRow();
            drHLVbasedAnalysis["HLV_Type"] = "Life Insurance Gap analysis";
            drHLVbasedAnalysis["HLV_Values"] = convertUSCurrencyFormat(Math.Round(double.Parse(HLVbasedIncome.ToString()), 2));
            dtHLVAnalysis.Rows.Add(drHLVbasedAnalysis);            
            dsCustomerFPReportDetails.Tables.Add(dtHLVAnalysis);


            dtHLVBasedIncome.Columns.Add("HLVIncomeType");
            dtHLVBasedIncome.Columns.Add("HLVIncomeValue");
            DataRow drHLVBasedIncome;

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Financial Net Worth";
            drHLVBasedIncome["HLVIncomeValue"] = convertUSCurrencyFormat(Math.Round(netWorth,2));
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Insurance Cover Recommended";
            drHLVBasedIncome["HLVIncomeValue"] =convertUSCurrencyFormat(Math.Round(double.Parse((HLVbasedIncome - netWorth).ToString()), 2)); 
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Current Insurance Cover";
            drHLVBasedIncome["HLVIncomeValue"] =convertUSCurrencyFormat(Math.Round(sumAssuredLI));
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Insurance Cover Required";
            drHLVBasedIncome["HLVIncomeValue"] = convertUSCurrencyFormat(Math.Round(double.Parse(((HLVbasedIncome - netWorth) - sumAssuredLI).ToString()), 2));
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            dsCustomerFPReportDetails.Tables.Add(dtHLVBasedIncome);

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[10].Rows)
            {
                if (dr["CashCategory"].ToString() == "Annual Surplus")
                {
                    if (!string.IsNullOrEmpty(dr["Amount"].ToString()))
                        surplus = double.Parse(dr["Amount"].ToString());
                }

            }

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[14].Rows)
            {
                if (!string.IsNullOrEmpty(dr["InsuranceValues"].ToString()))
                {
                    lifeProtectionTotal += double.Parse(dr["InsuranceValues"].ToString());
                }
            }

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[11].Rows)
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
                else if (dr["AssetGroupCode"].ToString() == "OT")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalOther = double.Parse(dr["AssetValues"].ToString());

                }
            }

            if (totalEquity > 0)
            {
                strInvestment = "Your current investments are Rs " + convertUSCurrencyFormat(totalEquity) + " in equity";
            }
            else
                strInvestment = string.Empty;

            if (totalMF > 0)
            {
                if (!string.IsNullOrEmpty(strInvestment))
                {
                   if (totalFixedIncome>0 || totalFixedIncome>0)
                     strInvestment += ", " + "Rs " + convertUSCurrencyFormat(totalMF) + " in Mutual Fund";
                    else if (totalFixedIncome == 0 && totalOther == 0)
                    {
                        strInvestment += " and " + "Rs " + convertUSCurrencyFormat(totalMF) + " in Mutual Fund";
                    }

                }
                else
                    strInvestment = "Your current investments are Rs " + convertUSCurrencyFormat(totalMF) + " in Mutual Fund";
            }
            if (totalFixedIncome > 0)
            {
                if (!string.IsNullOrEmpty(strInvestment))
                {
                    if (totalOther>0)
                        strInvestment += ", " + "Rs " + convertUSCurrencyFormat(totalFixedIncome) + " in FixedIncome ";
                    else
                        strInvestment += " and " + "Rs " + convertUSCurrencyFormat(totalFixedIncome) + " in FixedIncome ";
                }
                else
                    strInvestment = "Your current investments are Rs " + convertUSCurrencyFormat(totalFixedIncome) + " in FixedIncome ";

            }
            if (totalOther > 0)
            {
                if (!string.IsNullOrEmpty(strInvestment))
                    strInvestment +="and " + "Rs " + convertUSCurrencyFormat(totalOther) + " in others";
                else
                    strInvestment = "Your current investments is Rs " + convertUSCurrencyFormat(totalOther) + " in others";

            }

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[12].Rows)
            {
                if (dr["LoanType"].ToString() == "Home Loan")
                {
                    if (dr["LoanValues"] != null)
                    {
                        strHomeLoan = "Principal Outstanding is Rs " + convertUSCurrencyFormat(double.Parse(dr["LoanValues"].ToString()));
                    }

                }
                else if (dr["LoanType"].ToString() == "Home Loan")
                {
                    if (dr["LoanValues"] != null)
                    {
                        strAutoLoan = "Principal Outstanding is Rs " + convertUSCurrencyFormat(double.Parse(dr["LoanValues"].ToString()));
                    }


                }
            }


            dtCurrentObservation = new DataTable();
            dtCurrentObservation.Columns.Add("ObjType");
            dtCurrentObservation.Columns.Add("ObjSummary");
            DataRow drCurrentObservation;
            if (Math.Abs(netWorth) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Net Worth";
                drCurrentObservation["ObjSummary"] = "Your current Net Worth is Rs " + convertUSCurrencyFormat(netWorth).ToString();
                dtCurrentObservation.Rows.Add(drCurrentObservation);

            }
            if (Math.Abs(surplus) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Cash flow";
                drCurrentObservation["ObjSummary"] = "Your current surplus is Rs " + convertUSCurrencyFormat(surplus).ToString() + " per year";
                dtCurrentObservation.Rows.Add(drCurrentObservation);
            }
            if (Math.Abs(lifeProtectionTotal) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Protection";
                drCurrentObservation["ObjSummary"] = "You have Life insurance protection of Rs " + convertUSCurrencyFormat(lifeProtectionTotal).ToString();
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

            dtHealthAnalysis = new DataTable();
            dtHealthAnalysis.Columns.Add("Ratio");
            dtHealthAnalysis.Columns.Add("value");
            DataRow drHealthAnalysis;

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[13].Rows)
            {
                if (dr["Class"].ToString() == "Equity")
                {
                    currEquity = double.Parse(dr["CurrentPercentage"].ToString());

                }

            }
            drHealthAnalysis = dtHealthAnalysis.NewRow();
            drHealthAnalysis["Ratio"] = "Financial Asset allocation -equity(%)";
            drHealthAnalysis["value"] = convertUSCurrencyFormat(Math.Round(currEquity, 3)).ToString();
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[6].Rows)
            {
                if (!string.IsNullOrEmpty(dr["MonthyTotal"].ToString()))
                    toatlGoalAmount = double.Parse(dr["MonthyTotal"].ToString());
            }

            drHealthAnalysis = dtHealthAnalysis.NewRow();
            drHealthAnalysis["Ratio"] = "Savings/Income Ratio";
            if (totalIncome != 0)
                drHealthAnalysis["value"] =Math.Round((toatlGoalAmount / totalIncome), 3).ToString();
                    
            else
                drHealthAnalysis["value"] = 0;
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

           
            drHealthAnalysis = dtHealthAnalysis.NewRow();
            drHealthAnalysis["Ratio"] = "Loan/Financial Assets Ratio";
            if (asset != 0)
                drHealthAnalysis["value"] = Math.Round((liabilities / asset), 3).ToString();
            else
                drHealthAnalysis["value"] = 0;
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            dsCustomerFPReportDetails.Tables.Add(dtHealthAnalysis);

            //dsCustomerFPReportDetails.Tables.Add("AssetClass",dtAssetClass);

            //dsCustomerFPReportDetails.Tables.Add(dtPortfolioAllocation);


            return dsCustomerFPReportDetails;
        }

        private string convertUSCurrencyFormat(double value)
        {
            string strValues = string.Empty;
            if (value > 999)
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

    }
}
