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
        public DataSet GetEquityScripwiseSummary(EquityReportVo reports, int adviserId)
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
                db.AddInParameter(getCustomerNPListCmd, "@FromDate", DbType.DateTime, DateBo.GetPreviousMonthLastDate(reports.FromDate));
                //reports.ToDate = Convert.ToDateTime("01/01/2012");
                db.AddInParameter(getCustomerNPListCmd, "@Todate", DbType.DateTime, reports.ToDate);
                db.AddInParameter(getCustomerNPListCmd, "@AdviserId", DbType.Int32, adviserId);

                getCustomerNPListCmd.CommandTimeout = 60 * 60;
                dsEquitySectorwise = db.ExecuteDataSet(getCustomerNPListCmd);


                return dsEquitySectorwise;


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
          public DataSet GetEquityTransaction(EquityReportVo reports,int adviserId)
            {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand getCustomerNPListCmd;
            DataSet dsEquityTransactionwise;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerNPListCmd = db.GetStoredProcCommand("SP_RPT_GetCustomerEquityTransactions");
                //reports.PortfolioIds = "13708,14675";
                db.AddInParameter(getCustomerNPListCmd, "@PortfolioIds", DbType.String, reports.PortfolioIds); //35437
               // reports.FromDate = Convert.ToDateTime("01/01/2008");
                db.AddInParameter(getCustomerNPListCmd, "@FromDate", DbType.DateTime,DateBo.GetPreviousMonthLastDate(reports.FromDate));
                //reports.ToDate = Convert.ToDateTime("01/01/2012");
                db.AddInParameter(getCustomerNPListCmd, "@Todate", DbType.DateTime, reports.ToDate);

                getCustomerNPListCmd.CommandTimeout = 60 * 60;
                dsEquityTransactionwise = db.ExecuteDataSet(getCustomerNPListCmd);


                return dsEquityTransactionwise;


            }
            catch (Exception ex)
            {
                throw (ex);
            }
         
        }
       public DataSet GetEquityHolding(EquityReportVo reports, int adviserId)
         {

              //DataTable dtEquityHolding = new DataTable();
              //DataRow drEquityholding;
              //DataColumn dcEquityHolding;

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.String");
              //dcEquityHolding.ColumnName = "CompanyName";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "NetHoldings";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "AveragePrice";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "NetCost";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "MarketPrice";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "CurrentValue";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "UnrealizedPL";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "RealizedPL";
              //dtEquityHolding.Columns.Add(dcEquityHolding);


              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "TotalPL";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              //dcEquityHolding = new DataColumn();
              //dcEquityHolding.DataType = Type.GetType("System.Double");
              //dcEquityHolding.ColumnName = "XIRR";
              //dtEquityHolding.Columns.Add(dcEquityHolding);

              Microsoft.Practices.EnterpriseLibrary.Data.Database db;
              DbCommand getCustomerNPListCmd;
              DataSet dsEquityHoldingwise;

              try
              {
                  db = DatabaseFactory.CreateDatabase("wealtherp");
                  getCustomerNPListCmd = db.GetStoredProcCommand("SP_RPT_GetCustomerEquityHoldings");
                  db.AddInParameter(getCustomerNPListCmd, "@PortfolioIds", DbType.String, reports.PortfolioIds);
                  db.AddInParameter(getCustomerNPListCmd, "@AsOnDate", DbType.DateTime, reports.ToDate);
                  db.AddInParameter(getCustomerNPListCmd, "@AdviserId", DbType.Int32, adviserId);

                  getCustomerNPListCmd.CommandTimeout = 60 * 60;
                  dsEquityHoldingwise = db.ExecuteDataSet(getCustomerNPListCmd);

                  //foreach (DataRow dr in dsEquityHoldingwise.Tables[0].Rows)
                  //{
                  //     drEquityholding =dtEquityHolding.NewRow();
                  //     drEquityholding[0] = dr[0];
                  //     drEquityholding[1] = dr[1];
                  //     drEquityholding[2] = dr[2];
                  //     drEquityholding[3] = dr[3];
                  //     drEquityholding[4] = dr[4];
                  //     drEquityholding[5] = dr[5];
                  //     drEquityholding[6] = dr[6];
                  //     drEquityholding[7] = dr[7];
                  //     drEquityholding[8] = dr[8];
                  //     drEquityholding[9] = dr[9];
                  //     dtEquityHolding.Rows.Add(drEquityholding); 
                  //}
                  //dsEquityHoldingwise.Tables.Add(dtEquityHolding);
                  return dsEquityHoldingwise;


              }
              catch (Exception ex)
              {
                  throw (ex);
              }

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
                getEquityTransactionsCmd.CommandTimeout = 60 * 60;
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
