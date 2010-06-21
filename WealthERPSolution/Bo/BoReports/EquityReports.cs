using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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


           EquityReportsDao eqReports = new EquityReportsDao();

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



            DataSet dsPortfolioSummary = eqReports.GetEquityScripwiseSummary(report, adviserId);

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


        public DataTable GetEquityTransactionAll(EquityReportVo reports)
        {
            EquityReportsDao eqReports = new EquityReportsDao();
            return eqReports.GetEquityTransactionAll(reports);
        }

        public DataTable GetEquityTransactionDerivate(EquityReportVo reports)
        {
            EquityReportsDao eqReports = new EquityReportsDao();
            return eqReports.GetEquityTransactionDerivate(reports);
        }

        public DataTable GetEquityTransactionSpeculative(EquityReportVo reports)
        {
            EquityReportsDao eqReports = new EquityReportsDao();
            return eqReports.GetEquityTransactionSpeculative(reports);
        }

        public DataTable GetCustomerPortfolioEquityTransactions(EquityReportVo reports, int adviserId)
        {
            EquityReportsDao eqReports = new EquityReportsDao();
            return eqReports.GetCustomerPortfolioEquityTransactions(reports,adviserId);
        }
    }
}
