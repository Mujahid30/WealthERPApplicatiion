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
            dsCustomerFPReportDetails.Tables.RemoveAt(16);
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
            drHLVbasedAnalysis["HLV_Type"] = "HLV based on income";
            drHLVbasedAnalysis["HLV_Values"] = HLVbasedIncome.ToString();
            dtHLVAnalysis.Rows.Add(drHLVbasedAnalysis);
            dsCustomerFPReportDetails.Tables.Add(dtHLVAnalysis);


            dtHLVBasedIncome.Columns.Add("HLVIncomeType");
            dtHLVBasedIncome.Columns.Add("HLVIncomeValue");
            DataRow drHLVBasedIncome;

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Financial Net Worth";
            drHLVBasedIncome["HLVIncomeValue"] = netWorth.ToString();
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Insurance Cover Recommended";
            drHLVBasedIncome["HLVIncomeValue"] = (HLVbasedIncome - netWorth).ToString();
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Current Insurance Cover";
            drHLVBasedIncome["HLVIncomeValue"] = sumAssuredLI.ToString();
            dtHLVBasedIncome.Rows.Add(drHLVBasedIncome);

            drHLVBasedIncome = dtHLVBasedIncome.NewRow();
            drHLVBasedIncome["HLVIncomeType"] = "Insurance Cover Required";
            drHLVBasedIncome["HLVIncomeValue"] = ((HLVbasedIncome - netWorth) - sumAssuredLI).ToString();
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
                if (dr["AssetGroupName"].ToString() == "Mutual Fund")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalMF = double.Parse(dr["AssetValues"].ToString());
                }
                else if (dr["AssetGroupName"].ToString() == "Direct Equity")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalEquity = double.Parse(dr["AssetValues"].ToString());
                }
                else if (dr["AssetGroupName"].ToString() == "Fixed Income")
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalFixedIncome = double.Parse(dr["AssetValues"].ToString());
                }
                else
                {
                    if (!string.IsNullOrEmpty(dr["AssetValues"].ToString()))
                        totalOther = double.Parse(dr["AssetValues"].ToString());

                }
            }

            if (totalEquity > 0)
            {
                strInvestment = "Your current investments are Rs" + totalEquity.ToString() + " in equity ,";
            }
            if (totalMF > 0)
            {
                strInvestment += "Rs" + totalMF.ToString() + " in Mutual Fund, ";
            }
            if (totalFixedIncome > 0)
            {
                strInvestment += "Rs" + totalFixedIncome.ToString() + " in FixedIncome";

            }
            if (totalOther > 0)
            {
                strInvestment += "and Rs " + totalOther.ToString() + " in others";

            }

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[12].Rows)
            {
                if (dr["LoanType"].ToString() == "Home Loan")
                {
                    if (dr["LoanValues"] != null)
                    {
                        strHomeLoan = "Principal Outstanding is Rs " + dr["LoanValues"].ToString();
                    }

                }
                else if (dr["LoanType"].ToString() == "Home Loan")
                {
                    if (dr["LoanValues"] != null)
                    {
                        strAutoLoan = "Principal Outstanding is Rs " + dr["LoanValues"].ToString();
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
                drCurrentObservation["ObjSummary"] = "Your current Net Worth is Rs " + netWorth.ToString();
                dtCurrentObservation.Rows.Add(drCurrentObservation);

            }
            if (Math.Abs(surplus) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Cash flow";
                drCurrentObservation["ObjSummary"] = "Your current surplus is Rs" + surplus.ToString() + "per year";
                dtCurrentObservation.Rows.Add(drCurrentObservation);
            }
            if (Math.Abs(lifeProtectionTotal) > 0)
            {
                drCurrentObservation = dtCurrentObservation.NewRow();
                drCurrentObservation["ObjType"] = "Protection";
                drCurrentObservation["ObjSummary"] = "You have Life insurance protection of Rs" + lifeProtectionTotal.ToString();
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
            drHealthAnalysis["Ratio"] = "Financial Asset allocation -equity";
            drHealthAnalysis["value"] = Math.Round(currEquity, 3).ToString();
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            drHealthAnalysis = dtHealthAnalysis.NewRow();
            drHealthAnalysis["Ratio"] = "Savings/Income";
            if (asset != 0)
                drHealthAnalysis["value"] = Math.Round((liabilities / asset), 3).ToString();
            else
                drHealthAnalysis["value"] = 0;
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            foreach (DataRow dr in dsCustomerFPReportDetails.Tables[6].Rows)
            {
                if (!string.IsNullOrEmpty(dr["MonthyTotal"].ToString()))
                    toatlGoalAmount = double.Parse(dr["MonthyTotal"].ToString());
            }
            drHealthAnalysis = dtHealthAnalysis.NewRow();
            drHealthAnalysis["Ratio"] = "Loan/Financial Assets";
            if (totalIncome != 0)
                drHealthAnalysis["value"] = Math.Round((toatlGoalAmount / totalIncome), 3).ToString();
            else
                drHealthAnalysis["value"] = 0;
            dtHealthAnalysis.Rows.Add(drHealthAnalysis);

            dsCustomerFPReportDetails.Tables.Add(dtHealthAnalysis);


            return dsCustomerFPReportDetails;
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
