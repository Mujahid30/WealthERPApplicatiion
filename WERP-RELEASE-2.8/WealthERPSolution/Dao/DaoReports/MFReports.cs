﻿using System;
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
using BoCustomerPortfolio;
using VoCustomerPortfolio;
using BoCommon;

namespace DaoReports
{
    public class MFReportsDao
    {
        /// <summary>
        /// Get MF Open Position data for MF Summary Report(Date for main report).
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public DataSet GetMFFundSummaryReport(MFReportVo report,int adviserId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand getCustomerNPListCmd;
            DataSet dsMFCategorySummary;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerNPListCmd = db.GetStoredProcCommand("SP_RPT_MF_FundSummaryOpenPosition");

                db.AddInParameter(getCustomerNPListCmd, "@PortfolioIds", DbType.String, report.PortfolioIds);
                db.AddInParameter(getCustomerNPListCmd, "@StartDate", DbType.DateTime, DateBo.GetPreviousMonthLastDate(report.FromDate));
                db.AddInParameter(getCustomerNPListCmd, "@EndDate", DbType.DateTime,report.ToDate);
                db.AddInParameter(getCustomerNPListCmd, "@AdviserId", DbType.Int32, adviserId);
                getCustomerNPListCmd.CommandTimeout = 60 * 60;
                dsMFCategorySummary = db.ExecuteDataSet(getCustomerNPListCmd);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsMFCategorySummary;
        }

        /// <summary>
        /// Get MF All Position data for MF Summary Report(Data for subreports).
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public DataSet GetMFFundSummaryReportAllPosition(MFReportVo report, int adviserId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand getCustomerNPListCmd;
            DataSet dsMFCategorySummary;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerNPListCmd = db.GetStoredProcCommand("SP_RPT_MF_FundSummaryAllPosition");

                db.AddInParameter(getCustomerNPListCmd, "@PortfolioIds", DbType.String, report.PortfolioIds);
                db.AddInParameter(getCustomerNPListCmd, "@StartDate", DbType.DateTime, DateBo.GetPreviousMonthLastDate(report.FromDate));
                db.AddInParameter(getCustomerNPListCmd, "@EndDate", DbType.DateTime, report.ToDate);
                db.AddInParameter(getCustomerNPListCmd, "@AdviserId", DbType.Int32, adviserId);
                getCustomerNPListCmd.CommandTimeout = 60 * 60;
                dsMFCategorySummary = db.ExecuteDataSet(getCustomerNPListCmd);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsMFCategorySummary;
        }

        /// <summary>
        /// Get Transaction Report
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        public DataTable GetTransactionReport(MFReportVo reports)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand getCustomerNPListCmd;
            DataSet getCustomerNPDs;

            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerNPListCmd = db.GetStoredProcCommand("SP_RPT_GetCustomerMFTransactions");
                //reports.PortfolioIds = "13708,15175";
                db.AddInParameter(getCustomerNPListCmd, "@PortfolioIds", DbType.String, reports.PortfolioIds); //35437
                db.AddInParameter(getCustomerNPListCmd, "@FromDate", DbType.DateTime, reports.FromDate);
                db.AddInParameter(getCustomerNPListCmd, "@Todate", DbType.DateTime, reports.ToDate);
                if (!string.IsNullOrEmpty(reports.FilterBy))
                    db.AddInParameter(getCustomerNPListCmd, "@TransType", DbType.String, reports.FilterBy);
                else
                    db.AddInParameter(getCustomerNPListCmd, "@TransType", DbType.String, "ALL");

                if (!string.IsNullOrEmpty(reports.OrderBy))
                     db.AddInParameter(getCustomerNPListCmd, "@OrderBy", DbType.String, reports.OrderBy);
                else
                    db.AddInParameter(getCustomerNPListCmd, "@OrderBy", DbType.String, "Date");
                //getCustomerNPListCmd = db.GetSqlStringCommand("select top 20 CMFA_AccountID,CMFT_TransactionDate,PASP_SchemePlanName from CustomerMutualfundtransaction  CustomerMutualfundtransaction inner join ProductAMCSchemePlan AS ProductAMCSchemePlan ON CustomerMutualfundtransaction.PASP_SchemePlanCode = ProductAMCSchemePlan.PASP_SchemePlanCode;");  //db.GetStoredProcCommand("SP_GetAdviser");
                //db.AddInParameter(getCustomerNPListCmd, "@A_AdviserId", DbType.Int32, 1049);
                //db.AddInParameter(getCustomerNPListCmd, "@ValuationDate", DbType.DateTime, "2009-10-08");

                getCustomerNPListCmd.CommandTimeout = 60 * 60;
                getCustomerNPDs = db.ExecuteDataSet(getCustomerNPListCmd);
                ds = getCustomerNPDs;

                return getCustomerNPDs.Tables[0];


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Returns Dividend Report
        /// </summary>
        /// <param name="reports"></param>
        /// <returns></returns>
        public DataTable GetDivdendReport(MFReportVo reports)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand getCustomerNPListCmd;
            DataSet getCustomerNPDs;

            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerNPListCmd = db.GetStoredProcCommand("SP_RPT_GetCustomerMFDividend");
                //reports.PortfolioIds = "13708,15175";
                db.AddInParameter(getCustomerNPListCmd, "@PortfolioIds", DbType.String, reports.PortfolioIds); //35437
                db.AddInParameter(getCustomerNPListCmd, "@FromDate", DbType.DateTime, reports.FromDate);
                db.AddInParameter(getCustomerNPListCmd, "@Todate", DbType.DateTime, reports.ToDate);

                //getCustomerNPListCmd = db.GetSqlStringCommand("select top 20 CMFA_AccountID,CMFT_TransactionDate,PASP_SchemePlanName from CustomerMutualfundtransaction  CustomerMutualfundtransaction inner join ProductAMCSchemePlan AS ProductAMCSchemePlan ON CustomerMutualfundtransaction.PASP_SchemePlanCode = ProductAMCSchemePlan.PASP_SchemePlanCode;");  //db.GetStoredProcCommand("SP_GetAdviser");
                //db.AddInParameter(getCustomerNPListCmd, "@A_AdviserId", DbType.Int32, 1049);
                //db.AddInParameter(getCustomerNPListCmd, "@ValuationDate", DbType.DateTime, "2009-10-08");

                getCustomerNPListCmd.CommandTimeout = 60 * 60;
                getCustomerNPDs = db.ExecuteDataSet(getCustomerNPListCmd);
                ds = getCustomerNPDs;

                return getCustomerNPDs.Tables[0];


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable GetReturnSummaryReport(MFReportVo reports,int adviserId)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCustomerMFReturns;
            DataSet dsCustomerMFReturns;

            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerMFReturns = db.GetStoredProcCommand("SP_RPT_GetCustomerMFReturnSummary");
                db.AddInParameter(cmdCustomerMFReturns, "@PortfolioIds", DbType.String, reports.PortfolioIds); //35437
                db.AddInParameter(cmdCustomerMFReturns, "@FromDate", DbType.DateTime, reports.FromDate);
                //db.AddInParameter(cmdCustomerMFReturns, "@AdviserId", DbType.Int32, adviserId);

                cmdCustomerMFReturns.CommandTimeout = 60 * 60;
                dsCustomerMFReturns = db.ExecuteDataSet(cmdCustomerMFReturns);
                //ds = dsCustomerMFReturns;
                return dsCustomerMFReturns.Tables[0];

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Get Customer Portfolio Analytics Report Data
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetPortfolioAnalyticsReport(MFReportVo reports, int adviserId)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCustomerMFReturns;
            DataSet dsCustomerMFReturns;

            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerMFReturns = db.GetStoredProcCommand("SP_RPT_GetCustomerPortfolioAnalytics");
                db.AddInParameter(cmdCustomerMFReturns, "@PortfolioIds", DbType.String, reports.PortfolioIds); //35437
                db.AddInParameter(cmdCustomerMFReturns, "@FromDate", DbType.DateTime, reports.FromDate);
                //db.AddInParameter(cmdCustomerMFReturns, "@AdviserId", DbType.Int32, adviserId);

                cmdCustomerMFReturns.CommandTimeout = 60 * 60;
                dsCustomerMFReturns = db.ExecuteDataSet(cmdCustomerMFReturns);
                //ds = dsCustomerMFReturns;
                return dsCustomerMFReturns;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Created Dtaa Table For "Portfolio Returns Realized" Report --Author:Pramod
        /// </summary>
        /// <param name="reports">"reports" is a object of "MFReportVo" Contails report parameters</param>
        /// <param name="adviserId">Get the data of all the customer belong to This Id</param>
        /// <returns>DataTable</returns>
        public DataTable GetMFReturnRESummaryReport(MFReportVo reports, int adviserId)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdCustomerMFReturns;
            DataSet dsCustomerMFReturns;
                       
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerMFReturns = db.GetStoredProcCommand("SP_RPT_GetCustomerMFReturnsRESummary");
                db.AddInParameter(cmdCustomerMFReturns, "@PortfolioIds", DbType.String, reports.PortfolioIds); //35437
                db.AddInParameter(cmdCustomerMFReturns, "@FromDate", DbType.DateTime, reports.FromDate);
                //db.AddInParameter(cmdCustomerMFReturns, "@AdviserId", DbType.Int32, adviserId);

                cmdCustomerMFReturns.CommandTimeout = 60 * 60;
                dsCustomerMFReturns = db.ExecuteDataSet(cmdCustomerMFReturns);
                //ds = dsCustomerMFReturns;
                return dsCustomerMFReturns.Tables[0];

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public DataTable GetReturnTransactionSummaryReport(MFReportVo reports)
        {
            
            DataTable dtReturnTransaction = new DataTable();
            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            DataSet dsReturnsTransactions = new DataSet();
            List<MFPortfolioVo> mfPortfolioVoList = new List<MFPortfolioVo>();
            dtReturnTransaction.Columns.Add("TransactionType");
            dtReturnTransaction.Columns.Add("Units", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("PurchaseDate", System.Type.GetType("System.DateTime"));
            dtReturnTransaction.Columns.Add("PurchasePrice", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("PurchaseCost", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("SellDate", System.Type.GetType("System.DateTime"));
            dtReturnTransaction.Columns.Add("SellPrice", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("SaleProceed", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("AsonDateNAV", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("AsOnDateValue", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("AgeOfInvestment", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("ActualPL", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("NotionalPL", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("TotalPL", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("AbsoluteReturn", System.Type.GetType("System.Double"));
            dtReturnTransaction.Columns.Add("AnnualReturn", System.Type.GetType("System.Double"));


            dtReturnTransaction.Columns.Add("SchemePlanCode");
            dtReturnTransaction.Columns.Add("SchemePlanName");
            dtReturnTransaction.Columns.Add("FolioNum");
            dtReturnTransaction.Columns.Add("CustomerName");
            dtReturnTransaction.Columns.Add("CustomerId");
            dtReturnTransaction.Columns.Add("PortfolioName");
            dtReturnTransaction.Columns.Add("PortfolioId");

            try
            {

                String[] portfolioIds = reports.PortfolioIds.Split(',');
                foreach (string strPortfoliioId in portfolioIds)
                {
                    mfPortfolioVoList = new List<MFPortfolioVo>();
                    Int32 portfoliioId  = 0;
                    portfoliioId = Convert.ToInt32(strPortfoliioId);
                    DataSet dsPortfolioCustomer = portfolioBo.GetCustomerPortfolioDetails(portfoliioId);
                    DataRow drPortfolioCustomer = dsPortfolioCustomer.Tables[0].Rows[0];
                    mfPortfolioVoList = customerPortfolioBo.GetCustomerMFPortfolio(int.Parse(drPortfolioCustomer["C_CustomerId"].ToString()), portfoliioId, reports.FromDate, "", "","");
                    if (mfPortfolioVoList != null && mfPortfolioVoList.Count > 0)
                    {
                        foreach (MFPortfolioVo mFPortfolioVo in mfPortfolioVoList)
                        {
                            foreach (MFPortfolioTransactionVo mFPortfolioTransaction in mFPortfolioVo.MFPortfolioTransactionVoList)
                            {
                                DataRow drReturnTransaction = dtReturnTransaction.NewRow();
                                drReturnTransaction["TransactionType"] = mFPortfolioTransaction.TransactionType;
                                drReturnTransaction["Units"] = mFPortfolioTransaction.BuyQuantity; //
                                drReturnTransaction["PurchaseDate"] = mFPortfolioTransaction.BuyDate;
                                drReturnTransaction["PurchasePrice"] = mFPortfolioTransaction.BuyPrice;
                                drReturnTransaction["PurchaseCost"] = mFPortfolioTransaction.CostOfAcquisition; //
                                drReturnTransaction["SellDate"] = mFPortfolioTransaction.SellDate;
                                drReturnTransaction["SellPrice"] = mFPortfolioTransaction.SellPrice;
                                drReturnTransaction["SaleProceed"] = mFPortfolioTransaction.RealizedSalesValue;
                                drReturnTransaction["AsonDateNAV"] = mFPortfolioTransaction.CurrentNAV;//
                                drReturnTransaction["AsOnDateValue"] = mFPortfolioTransaction.CurrentValue;
                                drReturnTransaction["AgeOfInvestment"] = mFPortfolioTransaction.AgeOfInvestment;
                                drReturnTransaction["ActualPL"] = mFPortfolioTransaction.RealizedProfitLoss;
                                drReturnTransaction["NotionalPL"] = mFPortfolioTransaction.NotionalProfitLoss;
                                drReturnTransaction["TotalPL"] = mFPortfolioTransaction.TotalProfitLoss;
                                drReturnTransaction["AbsoluteReturn"] = mFPortfolioTransaction.AbsoluteReturns;
                                drReturnTransaction["AnnualReturn"] = mFPortfolioTransaction.AnnualReturns;


                                drReturnTransaction["SchemePlanCode"] = mFPortfolioVo.MFCode;
                                drReturnTransaction["SchemePlanName"] = mFPortfolioVo.SchemePlan;
                                drReturnTransaction["FolioNum"] = mFPortfolioVo.Folio;
                                drReturnTransaction["CustomerName"] = drPortfolioCustomer["C_FirstName"].ToString();
                                drReturnTransaction["CustomerId"] = mFPortfolioVo.CustomerId;
                                if (drPortfolioCustomer["CP_PortfolioName"] != null)
                                    drReturnTransaction["PortfolioName"] = drPortfolioCustomer["CP_PortfolioName"].ToString();
                                drReturnTransaction["PortfolioId"] = portfoliioId;
                                dtReturnTransaction.Rows.Add(drReturnTransaction);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtReturnTransaction;
        }


        public DataTable GetCapitalGainSummaryReport(MFReportVo reports)
        {
            DataTable dtCapitalGainSummary = new DataTable();

            dtCapitalGainSummary.Columns.Add("CustomerName");
            dtCapitalGainSummary.Columns.Add("CustomerId");
            dtCapitalGainSummary.Columns.Add("PortfolioName");
            dtCapitalGainSummary.Columns.Add("PortfolioId");
            dtCapitalGainSummary.Columns.Add("GainOrLoss", System.Type.GetType("System.Double"));

            dtCapitalGainSummary.Columns.Add("FolioNum");
            dtCapitalGainSummary.Columns.Add("PASP_SchemePlanCode");
            dtCapitalGainSummary.Columns.Add("PASP_SchemePlanName");


            dtCapitalGainSummary.Columns.Add("STCGAmount", System.Type.GetType("System.Double"));
            dtCapitalGainSummary.Columns.Add("LTCGAmount", System.Type.GetType("System.Double"));
            dtCapitalGainSummary.Columns.Add("Category");


            DataRow dtRow = dtCapitalGainSummary.NewRow();

            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            DataSet dsReturnsTransactions = new DataSet();
            List<MFPortfolioVo> mfPortfolioVoList = new List<MFPortfolioVo>();


            try
            {

                String[] portfolioIds = reports.PortfolioIds.Split(',');
                if (portfolioIds.Count() > 0)
                {
                    foreach (string strPortfoliioId in portfolioIds)
                    {
                        mfPortfolioVoList = new List<MFPortfolioVo>();
                        Int32 portfoliioId = Convert.ToInt32(strPortfoliioId);
                        DataSet dsPortfolioCustomer = portfolioBo.GetCustomerPortfolioDetails(portfoliioId);
                        DataRow drPortfolioCustomer = dsPortfolioCustomer.Tables[0].Rows[0];
                        mfPortfolioVoList = customerPortfolioBo.GetCustomerMFPortfolio(int.Parse(drPortfolioCustomer["C_CustomerId"].ToString()), portfoliioId, reports.ToDate, "", "", "");
                        if (mfPortfolioVoList != null && mfPortfolioVoList.Count > 0)
                        {
                            foreach (MFPortfolioVo mFPortfolioVo in mfPortfolioVoList)
                            {
                                foreach (MFPortfolioTransactionVo mFPortfolioTransaction in mFPortfolioVo.MFPortfolioTransactionVoList)
                                {
                                    if (mFPortfolioTransaction.Closed == true && mFPortfolioTransaction.SellDate > reports.FromDate && mFPortfolioTransaction.SellDate < reports.ToDate)
                                    {
                                        DataRow drCapitalGainDetails = dtCapitalGainSummary.NewRow();

                                        drCapitalGainDetails["CustomerName"] = drPortfolioCustomer["C_FirstName"].ToString();
                                        drCapitalGainDetails["CustomerId"] = mFPortfolioVo.CustomerId;
                                        if (drPortfolioCustomer["CP_PortfolioName"] != null)
                                            drCapitalGainDetails["PortfolioName"] = drPortfolioCustomer["CP_PortfolioName"].ToString();
                                        drCapitalGainDetails["PortfolioId"] = portfoliioId;

                                        //drCapitalGainDetails["GainOrLoss"] = mFPortfolioTransaction.RealizedProfitLoss;
                                        drCapitalGainDetails["GainOrLoss"] = mFPortfolioTransaction.STCGTax + mFPortfolioTransaction.LTCGTax;
                                        drCapitalGainDetails["FolioNum"] = mFPortfolioVo.Folio;
                                        drCapitalGainDetails["PASP_SchemePlanCode"] = mFPortfolioVo.MFCode;
                                        drCapitalGainDetails["PASP_SchemePlanName"] = mFPortfolioVo.SchemePlan;

                                        drCapitalGainDetails["STCGAmount"] = mFPortfolioTransaction.STCGTax;
                                        drCapitalGainDetails["LTCGAmount"] = mFPortfolioTransaction.LTCGTax;
                                        drCapitalGainDetails["Category"] = mFPortfolioVo.Category;

                                        dtCapitalGainSummary.Rows.Add(drCapitalGainDetails);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtCapitalGainSummary;


            
        }


        public DataTable GetCapitalGainDetailsReport(MFReportVo reports)
        {
            DataTable dtCapitalGainDetails = new DataTable();

            dtCapitalGainDetails.Columns.Add("CustomerName");
            dtCapitalGainDetails.Columns.Add("CustomerId");
            dtCapitalGainDetails.Columns.Add("PortfolioName");
            dtCapitalGainDetails.Columns.Add("PortfolioId");
            dtCapitalGainDetails.Columns.Add("GainOrLoss", System.Type.GetType("System.Double"));

            dtCapitalGainDetails.Columns.Add("Units", System.Type.GetType("System.Double"));
            dtCapitalGainDetails.Columns.Add("RedDate");
            dtCapitalGainDetails.Columns.Add("RedAmount", System.Type.GetType("System.Double"));
            dtCapitalGainDetails.Columns.Add("DaysInvestedFor");
            dtCapitalGainDetails.Columns.Add("PurchaseDate");
            dtCapitalGainDetails.Columns.Add("PurchaseAmount", System.Type.GetType("System.Double"));
           
            dtCapitalGainDetails.Columns.Add("FolioNum");
            dtCapitalGainDetails.Columns.Add("SchemePlanCode");
            dtCapitalGainDetails.Columns.Add("SchemePlanName");

            dtCapitalGainDetails.Columns.Add("STCGTax", System.Type.GetType("System.Double"));
            dtCapitalGainDetails.Columns.Add("LTCGTax", System.Type.GetType("System.Double"));
            dtCapitalGainDetails.Columns.Add("Category");


            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            DataSet dsReturnsTransactions = new DataSet();
            List<MFPortfolioVo> mfPortfolioVoList = new List<MFPortfolioVo>();


            try
            {

                String[] portfolioIds = reports.PortfolioIds.Split(',');
                if (portfolioIds.Count() >0)
                {
                    foreach (string strPortfoliioId in portfolioIds)
                    {
                        mfPortfolioVoList = new List<MFPortfolioVo>();
                        Int32 portfoliioId = Convert.ToInt32(strPortfoliioId);
                        DataSet dsPortfolioCustomer = portfolioBo.GetCustomerPortfolioDetails(portfoliioId);
                        DataRow drPortfolioCustomer = dsPortfolioCustomer.Tables[0].Rows[0];
                        mfPortfolioVoList = customerPortfolioBo.GetCustomerMFPortfolio(int.Parse(drPortfolioCustomer["C_CustomerId"].ToString()), portfoliioId, reports.ToDate, "", "", "");
                        if (mfPortfolioVoList != null && mfPortfolioVoList.Count > 0)
                        {
                            foreach (MFPortfolioVo mFPortfolioVo in mfPortfolioVoList)
                            {
                                foreach (MFPortfolioTransactionVo mFPortfolioTransaction in mFPortfolioVo.MFPortfolioTransactionVoList)
                                {
                                    if (mFPortfolioTransaction.Closed == true && mFPortfolioTransaction.SellDate > reports.FromDate && mFPortfolioTransaction.SellDate < reports.ToDate)
                                    {
                                        DataRow drCapitalGainDetails = dtCapitalGainDetails.NewRow();

                                        drCapitalGainDetails["CustomerName"] = drPortfolioCustomer["C_FirstName"].ToString();
                                        drCapitalGainDetails["CustomerId"] = mFPortfolioVo.CustomerId;
                                        if (drPortfolioCustomer["CP_PortfolioName"] != null)
                                            drCapitalGainDetails["PortfolioName"] = drPortfolioCustomer["CP_PortfolioName"].ToString();
                                        drCapitalGainDetails["PortfolioId"] = portfoliioId;

                                        drCapitalGainDetails["GainOrLoss"] = mFPortfolioTransaction.RealizedProfitLoss;

                                        drCapitalGainDetails["Units"] = mFPortfolioTransaction.BuyQuantity;
                                        drCapitalGainDetails["RedDate"] = mFPortfolioTransaction.SellDate.ToShortDateString();
                                        drCapitalGainDetails["RedAmount"] = mFPortfolioTransaction.NetSalesProceed;

                                        drCapitalGainDetails["DaysInvestedFor"] = mFPortfolioTransaction.AgeOfInvestment;

                                        drCapitalGainDetails["PurchaseDate"] = mFPortfolioTransaction.BuyDate.ToShortDateString();
                                        drCapitalGainDetails["PurchaseAmount"] = mFPortfolioTransaction.CostOfAcquisition;

                                        drCapitalGainDetails["FolioNum"] = mFPortfolioVo.Folio;
                                        drCapitalGainDetails["SchemePlanCode"] = mFPortfolioVo.MFCode;
                                        drCapitalGainDetails["SchemePlanName"] = mFPortfolioVo.SchemePlan;

                                        drCapitalGainDetails["STCGTax"] = mFPortfolioTransaction.STCGTax;
                                        drCapitalGainDetails["LTCGTax"] = mFPortfolioTransaction.LTCGTax;
                                        drCapitalGainDetails["Category"] = mFPortfolioVo.Category;

                                        dtCapitalGainDetails.Rows.Add(drCapitalGainDetails);
                                    }
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtCapitalGainDetails;




        }


        /// <summary>
        /// Creating Data Table For "ELIGIBLE CAPITAL GAIN DETAILS & SUMMARY" Report : "Author:Pramod"
        /// </summary>
        /// <param name="reports">"reports" is a object of "MFReportVo" Contails report parameters </param>
        /// <returns> DataTable</returns>
        public DataTable GetEligibleCapitalGainDetailsReport(MFReportVo reports)
        {
            DataTable dtEligibleCapitalGainDetails = new DataTable();

            dtEligibleCapitalGainDetails.Columns.Add("CustomerName");
            dtEligibleCapitalGainDetails.Columns.Add("CustomerId");
            dtEligibleCapitalGainDetails.Columns.Add("PortfolioName");
            dtEligibleCapitalGainDetails.Columns.Add("PortfolioId");
            dtEligibleCapitalGainDetails.Columns.Add("GainOrLoss", System.Type.GetType("System.Double"));

            dtEligibleCapitalGainDetails.Columns.Add("Units", System.Type.GetType("System.Double"));
            dtEligibleCapitalGainDetails.Columns.Add("RedDate");
            dtEligibleCapitalGainDetails.Columns.Add("RedAmount", System.Type.GetType("System.Double"));
            dtEligibleCapitalGainDetails.Columns.Add("DaysInvestedFor");
            dtEligibleCapitalGainDetails.Columns.Add("PurchaseDate");
            dtEligibleCapitalGainDetails.Columns.Add("PurchaseAmount", System.Type.GetType("System.Double"));

            dtEligibleCapitalGainDetails.Columns.Add("FolioNum");
            dtEligibleCapitalGainDetails.Columns.Add("SchemePlanCode");
            dtEligibleCapitalGainDetails.Columns.Add("SchemePlanName");

            dtEligibleCapitalGainDetails.Columns.Add("STCGTax", System.Type.GetType("System.Double"));
            dtEligibleCapitalGainDetails.Columns.Add("LTCGTax", System.Type.GetType("System.Double"));
            dtEligibleCapitalGainDetails.Columns.Add("Category");
            dtEligibleCapitalGainDetails.Columns.Add("CurrNAV", System.Type.GetType("System.Double"));
            dtEligibleCapitalGainDetails.Columns.Add("CurrVALUE", System.Type.GetType("System.Double"));

            PortfolioBo portfolioBo = new PortfolioBo();
            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            DataSet dsReturnsTransactions = new DataSet();
            List<MFPortfolioVo> mfPortfolioVoList = new List<MFPortfolioVo>();


            try
            {

             String[] portfolioIds = reports.PortfolioIds.Split(',');
             if (portfolioIds.Count()>0)
              {
                foreach (string strPortfoliioId in portfolioIds)
                {
                    mfPortfolioVoList = new List<MFPortfolioVo>();
                    Int32 portfoliioId = Convert.ToInt32(strPortfoliioId);
                    DataSet dsPortfolioCustomer = portfolioBo.GetCustomerPortfolioDetails(portfoliioId);
                    DataRow drPortfolioCustomer = dsPortfolioCustomer.Tables[0].Rows[0];
                    mfPortfolioVoList = customerPortfolioBo.GetCustomerMFPortfolio(int.Parse(drPortfolioCustomer["C_CustomerId"].ToString()), portfoliioId, reports.ToDate, "", "", "");
                    if (mfPortfolioVoList != null && mfPortfolioVoList.Count > 0)
                    {
                        foreach (MFPortfolioVo mFPortfolioVo in mfPortfolioVoList)
                        {
                            foreach (MFPortfolioTransactionVo mFPortfolioTransaction in mFPortfolioVo.MFPortfolioTransactionVoList)
                            {
                                if (mFPortfolioTransaction.Closed == false &&  mFPortfolioTransaction.BuyDate < reports.ToDate)
                                {
                                    DataRow drEligibleCapitalGainDetails = dtEligibleCapitalGainDetails.NewRow();

                                    drEligibleCapitalGainDetails["CustomerName"] = drPortfolioCustomer["C_FirstName"].ToString();
                                    drEligibleCapitalGainDetails["CustomerId"] = mFPortfolioVo.CustomerId;
                                    if (drPortfolioCustomer["CP_PortfolioName"] != null)
                                        drEligibleCapitalGainDetails["PortfolioName"] = drPortfolioCustomer["CP_PortfolioName"].ToString();
                                    drEligibleCapitalGainDetails["PortfolioId"] = portfoliioId;

                                    //drEligibleCapitalGainDetails["GainOrLoss"] = mFPortfolioTransaction.RealizedProfitLoss;
                                    drEligibleCapitalGainDetails["GainOrLoss"] = mFPortfolioTransaction.STCGTax + mFPortfolioTransaction.LTCGTax;
                                    drEligibleCapitalGainDetails["Units"] = mFPortfolioTransaction.BuyQuantity;
                                    drEligibleCapitalGainDetails["RedDate"] = mFPortfolioTransaction.SellDate.ToShortDateString();
                                    drEligibleCapitalGainDetails["RedAmount"] = mFPortfolioTransaction.NetSalesProceed;

                                    drEligibleCapitalGainDetails["DaysInvestedFor"] = mFPortfolioTransaction.AgeOfInvestment;

                                    drEligibleCapitalGainDetails["PurchaseDate"] = mFPortfolioTransaction.BuyDate.ToShortDateString();
                                    drEligibleCapitalGainDetails["PurchaseAmount"] = mFPortfolioTransaction.CostOfAcquisition;

                                    drEligibleCapitalGainDetails["FolioNum"] = mFPortfolioVo.Folio;
                                    drEligibleCapitalGainDetails["SchemePlanCode"] = mFPortfolioVo.MFCode;
                                    drEligibleCapitalGainDetails["SchemePlanName"] = mFPortfolioVo.SchemePlan;

                                    drEligibleCapitalGainDetails["STCGTax"] = mFPortfolioTransaction.STCGTax;
                                    drEligibleCapitalGainDetails["LTCGTax"] = mFPortfolioTransaction.LTCGTax;
                                    drEligibleCapitalGainDetails["Category"] = mFPortfolioVo.Category;

                                    drEligibleCapitalGainDetails["CurrNAV"] = mFPortfolioTransaction.CurrentNAV;
                                    drEligibleCapitalGainDetails["CurrVALUE"] = mFPortfolioTransaction.CurrentValue;


                                    dtEligibleCapitalGainDetails.Rows.Add(drEligibleCapitalGainDetails);
                                }
                            }
                        }
                    }

                }
              }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtEligibleCapitalGainDetails;




        }

        
        public DataSet GetMFTransactionType()
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand MFTransactionTypeCmd;
            DataSet dsMFTransactionType;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                MFTransactionTypeCmd = db.GetStoredProcCommand("RPT_SP_GetTransactionType");
                dsMFTransactionType = db.ExecuteDataSet(MFTransactionTypeCmd);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsMFTransactionType;
        }

    }
}
