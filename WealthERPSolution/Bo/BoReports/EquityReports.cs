using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.FSharp;
using System.Numeric;
using System.Collections.Specialized;
using System.Data;
using DaoReports;
using VoReports;


namespace BoReports
{
   public  class EquityReportsBo
    {
        /// <summary>
        /// Get Transaction Report
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
       public DataTable GetEquityScripwiseSummary(EquityReportVo report, int adviserId)
        {
            //EquityReportsDao eqReports = new EquityReportsDao();
            //return eqReports.GetEquityScripwiseSummary(reports, adviserId);


            EquityReportsDao equityReportsDao = new EquityReportsDao();

            DataSet dsPortfolio = new DataSet();
            DataTable dtCurrent = new DataTable();
            DataTable dtPrevious = new DataTable();

            DataTable dtMFSummary = new DataTable();
            dtMFSummary.Columns.Add("CustomerName");
            dtMFSummary.Columns.Add("CustomerId");
            dtMFSummary.Columns.Add("PortfolioName");
            dtMFSummary.Columns.Add("PortfolioId");
            dtMFSummary.Columns.Add("Category");
            dtMFSummary.Columns.Add("PreviousValue", System.Type.GetType("System.Double"));
            dtMFSummary.Columns.Add("CurrentValue", System.Type.GetType("System.Double"));



            DataSet dsPortfolioSummary = equityReportsDao.GetEquityScripwiseSummary(report, adviserId);

            dtCurrent = dsPortfolioSummary.Tables[0];
            dtPrevious = dsPortfolioSummary.Tables[1];

            //dtCurrent.Merge(dtPrevious);
            try
            {

                foreach (DataRow drCurrent in dtCurrent.Rows)
                {
                    foreach (DataRow drPrevious in dtPrevious.Rows)
                    {
                        if (drCurrent["PortfolioId"].ToString() == drPrevious["PortfolioId"].ToString() && drCurrent["Category"].ToString() == drPrevious["Category"].ToString())
                        {
                            drCurrent["PreviousValue"] = drPrevious["PreviousValue"];
                            break;
                        }

                    }
                    dtMFSummary.ImportRow(drCurrent);
                }


                foreach (DataRow drPrevious in dtPrevious.Rows)
                {
                    string expression = string.Empty;
                    expression = "PortfolioId = " + drPrevious["PortfolioId"] + " and Category = '" + drPrevious["Category"] + "'";
                    if (dtMFSummary.Select(expression) == null)
                    {
                        dtMFSummary.ImportRow(drPrevious);
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtMFSummary;

        }

       public DataTable GetEquityTransaction(EquityReportVo report, int adviserId)
       {
           EquityReportsDao equityReportsDao = new EquityReportsDao();
           DataTable dtCurrent = new DataTable();           
          
           try
           {
               DataSet dsPortfolioSummary = equityReportsDao.GetEquityTransaction(report, adviserId);
               dtCurrent = dsPortfolioSummary.Tables[0];
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dtCurrent;

       }
       //public double CalculateXIRR(System.Collections.Generic.IEnumerable<double> values, System.Collections.Generic.IEnumerable<DateTime> date)
       //{

       //    double result = 0;
       //    try
       //    {
       //        result = System.Numeric.Financial.XIrr(values, date);
       //        //This 'if' loop is a temporary fix for the error where calculation is done for XIRR instead of average
       //        if (Convert.ToInt64(result).ToString().Length > 3 || result.ToString().Contains("E") || result.ToString().Contains("e"))
       //        {
       //            result = 0;
       //        }
       //        return result;
       //    }
       //    catch (Exception ex)
       //    {
       //        string e = ex.ToString();
       //        return result;
       //    }

       //}
       public DataTable GetEquityCustomerPortfolioLabelXIRR(string portfolioIds)
       {
           EquityReportsDao equityReportsDao = new EquityReportsDao();
           DataSet dsCustomerTransaction;
             string tempPortfoliId;
            DataTable dtCustomerPortfolio;
            DataTable dtCustomerTransaction;
            DataTable dtCustomerPortfolioNetHolding;
            DataTable dtCustomerPortfolioXIRR;
            double tempPortfolioXIRR;

            dsCustomerTransaction = equityReportsDao.GetEquityCustomerTransactionsDetailsForPortfolioXIRR(portfolioIds);
            dtCustomerPortfolio = dsCustomerTransaction.Tables[0];
            dtCustomerTransaction = dsCustomerTransaction.Tables[1];
            dtCustomerPortfolioNetHolding = dsCustomerTransaction.Tables[2];
            DataRow[] drTransactionDateAmount;

            dtCustomerPortfolioXIRR = new DataTable();
            dtCustomerPortfolioXIRR.Columns.Add("CustomerId", typeof(Int32));
            dtCustomerPortfolioXIRR.Columns.Add("CustomerName", typeof(string));
            dtCustomerPortfolioXIRR.Columns.Add("PortfolioId", typeof(Int32));
            dtCustomerPortfolioXIRR.Columns.Add("PortfolioName", typeof(string));
            dtCustomerPortfolioXIRR.Columns.Add("XIRR", typeof(decimal));
            DataRow drXIRR;
            foreach (DataRow dr in dtCustomerPortfolio.Rows)
            {
                drXIRR=dtCustomerPortfolioXIRR.NewRow();
                tempPortfoliId = dr["CP_PortfolioId"].ToString();
                drTransactionDateAmount = dtCustomerTransaction.Select("CP_PortfolioId=" + tempPortfoliId);

                double[] transactionAmount = new double[drTransactionDateAmount.Count()+1];
                DateTime[] transactionDate = new DateTime[drTransactionDateAmount.Count()+1];
                int tempCount=0;
                foreach (DataRow drAmountDate in drTransactionDateAmount)
                {                    
                        transactionAmount[tempCount] = double.Parse(drAmountDate["Calculated_Amount"].ToString());
                        transactionDate[tempCount] = DateTime.Parse(drAmountDate["CET_TradeDate"].ToString());
                        tempCount++;
                   
                }
                foreach (DataRow drNetHolding in dtCustomerPortfolioNetHolding.Rows)
                {
                    if (drNetHolding["CP_PortfolioId"].ToString() == tempPortfoliId)
                    {
                        transactionAmount[tempCount] = double.Parse(drNetHolding["Holding_Amount"].ToString());
                        transactionDate[tempCount] = DateTime.Parse(drNetHolding["Holding_AsOn"].ToString());
                    }

                }
                tempPortfolioXIRR = CalculatePortfolioXIRR(transactionAmount, transactionDate);
                drXIRR["CustomerId"] = dr["C_CustomerId"];
                drXIRR["CustomerName"] = dr["C_CustomerName"];
                drXIRR["PortfolioId"] = tempPortfoliId;
                drXIRR["PortfolioName"] = dr["CP_PortfolioName"];
                drXIRR["XIRR"] = (Math.Round(tempPortfolioXIRR,5))*100;
                dtCustomerPortfolioXIRR.Rows.Add(drXIRR);
            }

            return dtCustomerPortfolioXIRR;
        }
       public double CalculatePortfolioXIRR(System.Collections.Generic.IEnumerable<double> values, System.Collections.Generic.IEnumerable<DateTime> date)
       {

           double result = 0;
           try
           {
               result = System.Numeric.Financial.XIrr(values, date);
               //This 'if' loop is a temporary fix for the error where calculation is done for XIRR instead of average
               if (result.ToString().Contains("E") || result.ToString().Contains("e"))
               {
                   result = 0;
               }
               return result;
           }
           catch (Exception ex)
           {
               string e = ex.ToString();
               return result;
           }

       }
       
       public DataSet GetEquityHolding(EquityReportVo report, int adviserId)
       {
           EquityReportsDao equityReportsDao = new EquityReportsDao();    
            DataSet dsPortfolioSummary=new DataSet();

           try
           {
               dsPortfolioSummary = equityReportsDao.GetEquityHolding(report, adviserId);
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dsPortfolioSummary;
       }



        public DataTable GetEquityTransactionAll(EquityReportVo reports)
        {
            EquityReportsDao equityReportsDao = new EquityReportsDao();
            return equityReportsDao.GetEquityTransactionAll(reports);
        }

        public DataTable GetEquityTransactionDerivate(EquityReportVo reports)
        {
            EquityReportsDao equityReportsDao = new EquityReportsDao();
            return equityReportsDao.GetEquityTransactionDerivate(reports);
        }

        public DataTable GetEquityTransactionSpeculative(EquityReportVo reports)
        {
            EquityReportsDao equityReportsDao = new EquityReportsDao();
            return equityReportsDao.GetEquityTransactionSpeculative(reports);
        }

        public DataTable GetCustomerPortfolioEquityTransactions(EquityReportVo reports, int adviserId)
        {
            EquityReportsDao equityReportsDao = new EquityReportsDao();
            return equityReportsDao.GetCustomerPortfolioEquityTransactions(reports, adviserId);
        }
    }
}
