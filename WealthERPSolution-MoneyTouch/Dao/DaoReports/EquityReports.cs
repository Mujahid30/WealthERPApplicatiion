using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoReports;
using BoCommon;

namespace DaoReports
{
    public class EquityReportsDao
    {
        /// <summary>
        /// Get Transaction Report
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        public DataSet GetEquityScripwiseSummary(EquityReportVo reports,int adviserId)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand getCustomerNPListCmd;
            DataSet dsEquitySectorwise;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerNPListCmd = db.GetStoredProcCommand("SP_RPT_GetCustomerSectorwiseEqTransactions");
                //reports.PortfolioIds = "13708,14675";
                db.AddInParameter(getCustomerNPListCmd, "@PortfolioIds", DbType.String, reports.PortfolioIds); //35437
               // reports.FromDate = Convert.ToDateTime("01/01/2008");
                db.AddInParameter(getCustomerNPListCmd, "@FromDate", DbType.DateTime,DateBo.GetPreviousMonthLastDate(reports.FromDate));
                //reports.ToDate = Convert.ToDateTime("01/01/2012");
                db.AddInParameter(getCustomerNPListCmd, "@Todate", DbType.DateTime, reports.ToDate);
                db.AddInParameter(getCustomerNPListCmd, "@AdviserId", DbType.Int32, adviserId);


                dsEquitySectorwise = db.ExecuteDataSet(getCustomerNPListCmd);


                return dsEquitySectorwise;


            }
            catch (Exception ex)
            {
                throw (ex);
            }
           // return null;

            //DataTable dtMFSummary = new DataTable();
            //dtMFSummary.Columns.Add("CustomerName");
            //dtMFSummary.Columns.Add("CustomerId");
            //dtMFSummary.Columns.Add("PortfolioName");
            //dtMFSummary.Columns.Add("PortfolioId");
            //dtMFSummary.Columns.Add("Category");
            //dtMFSummary.Columns.Add("PreviousValue", System.Type.GetType("System.Int32"));
            //dtMFSummary.Columns.Add("CurrentValue", System.Type.GetType("System.Int32"));

            //DataRow dtRow = dtMFSummary.NewRow();

            //// Mahesh

            //dtRow = dtMFSummary.NewRow();
            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH1";
            //dtRow["PortfolioId"] = 2;

            //dtRow["Category"] = "Equity";
            //dtRow["PreviousValue"] = 700.555;
            //dtRow["CurrentValue"] = 900;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();

            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH1";
            //dtRow["PortfolioId"] = 2;

            //dtRow["Category"] = "Hybrid";
            //dtRow["PreviousValue"] = 200;
            //dtRow["CurrentValue"] = 200;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();
            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH1";
            //dtRow["PortfolioId"] = 2;

            //dtRow["Category"] = "Debt";
            //dtRow["PreviousValue"] = 400;
            //dtRow["CurrentValue"] = 200;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();

            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH1";
            //dtRow["PortfolioId"] = 2;

            //dtRow["Category"] = "Others";
            //dtRow["PreviousValue"] = 600;
            //dtRow["CurrentValue"] = 700;
            //dtMFSummary.Rows.Add(dtRow);


            ////MAHESH 2


            //dtRow = dtMFSummary.NewRow();
            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH2";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Equity";
            //dtRow["PreviousValue"] = 700;
            //dtRow["CurrentValue"] = 9;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();

            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH2";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Hybrid";
            //dtRow["PreviousValue"] = 200;
            //dtRow["CurrentValue"] = 200;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();
            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH2";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Debt";
            //dtRow["PreviousValue"] = 400;
            //dtRow["CurrentValue"] = 200;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();

            //dtRow["CustomerName"] = "Mahesh";
            //dtRow["CustomerId"] = "200";
            //dtRow["PortfolioName"] = "FOLIOMH2";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Others";
            //dtRow["PreviousValue"] = 600;
            //dtRow["CurrentValue"] = 700;
            //dtMFSummary.Rows.Add(dtRow);




            ////Robin
            //dtRow = dtMFSummary.NewRow();
            //dtRow["CustomerName"] = "Robin";
            //dtRow["CustomerId"] = "100";
            //dtRow["PortfolioName"] = "FOLIORT";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Equity";
            //dtRow["PreviousValue"] = 700;
            //dtRow["CurrentValue"] = 900;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();

            //dtRow["CustomerName"] = "Robin";
            //dtRow["CustomerId"] = "100";
            //dtRow["PortfolioName"] = "FOLIORT";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Hybrid";
            //dtRow["PreviousValue"] = 200;
            //dtRow["CurrentValue"] = 200;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();
            //dtRow["CustomerName"] = "Robin";
            //dtRow["CustomerId"] = "100";
            //dtRow["PortfolioName"] = "FOLIORT";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Debt";
            //dtRow["PreviousValue"] = 400;
            //dtRow["CurrentValue"] = 200;
            //dtMFSummary.Rows.Add(dtRow);


            //dtRow = dtMFSummary.NewRow();

            //dtRow["CustomerName"] = "Robin";
            //dtRow["CustomerId"] = "100";
            //dtRow["PortfolioName"] = "FOLIORT";
            //dtRow["PortfolioId"] = 20;

            //dtRow["Category"] = "Others";
            //dtRow["PreviousValue"] = 600;
            //dtRow["CurrentValue"] = 700;
            //dtMFSummary.Rows.Add(dtRow);





           // return dtMFSummary;
        }


        public DataTable GetEquityTransactionAll(EquityReportVo reports)
        {
            Database db;
            DbCommand getEquityTransactionsCmd;
            DataSet dsGetEquityTransactions;
            DataTable dtGetEquityTransactions = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionsCmd = db.GetStoredProcCommand("SP_RPT_GetCustomerEquityTransactions");
                db.AddInParameter(getEquityTransactionsCmd, "@PortfolioIds", DbType.String, reports.PortfolioIds);
                db.AddInParameter(getEquityTransactionsCmd, "@FromDate", DbType.DateTime, reports.FromDate);
                db.AddInParameter(getEquityTransactionsCmd, "@Todate", DbType.DateTime, reports.ToDate);

                dsGetEquityTransactions = db.ExecuteDataSet(getEquityTransactionsCmd);
                if (dsGetEquityTransactions.Tables.Count > 0 && dsGetEquityTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransactions = dsGetEquityTransactions.Tables[0];
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dtGetEquityTransactions;
        }

        public DataTable GetEquityTransactionDerivate(EquityReportVo reports)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public DataTable GetEquityTransactionSpeculative(EquityReportVo reports)
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public DataTable GetCustomerPortfolioEquityTransactions(EquityReportVo reports,int adviserId)
        {
            Database db;
            DbCommand getEquityTransactionsCmd;
            DataSet dsGetEquityTransactions;
            DataTable dtGetEquityTransactions = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionsCmd = db.GetStoredProcCommand("SP_RPT_GetCustomerPortfolioEquityTransactions");
                db.AddInParameter(getEquityTransactionsCmd, "@PortfolioIds", DbType.String, reports.PortfolioIds);
                db.AddInParameter(getEquityTransactionsCmd, "@AsOnDate", DbType.DateTime, reports.FromDate);
                db.AddInParameter(getEquityTransactionsCmd, "@AdviserId", DbType.Int64, adviserId);

                dsGetEquityTransactions = db.ExecuteDataSet(getEquityTransactionsCmd);
                if (dsGetEquityTransactions.Tables.Count > 0 && dsGetEquityTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransactions = dsGetEquityTransactions.Tables[0];
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dtGetEquityTransactions;
        }


    }
}
