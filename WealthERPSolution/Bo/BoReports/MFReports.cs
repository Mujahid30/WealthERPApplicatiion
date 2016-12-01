using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using System.Data;
using DaoReports;
using VoReports;

namespace BoReports
{
    public class MFReportsBo
    {
        public DataSet GetMFFundSummaryReport(MFReportVo report, int adviserId)
        {
            MFReportsDao mfReports = new MFReportsDao();
            DataSet dsMFSummary = new DataSet();

            try
            {
                // Code to combine two datatables (current and previous values data for main report) into one.
                DataSet dsPortfolio = new DataSet();
                DataTable dtCurrent = new DataTable();
                DataTable dtPrevious = new DataTable();

                DataTable MFSummaryOpenPosition = new DataTable();
                MFSummaryOpenPosition.Columns.Add("CustomerName");
                MFSummaryOpenPosition.Columns.Add("CustomerId");
                MFSummaryOpenPosition.Columns.Add("PortfolioName");
                MFSummaryOpenPosition.Columns.Add("PortfolioId");
                MFSummaryOpenPosition.Columns.Add("Category");
                MFSummaryOpenPosition.Columns.Add("PreviousValue", System.Type.GetType("System.Decimal"));
                MFSummaryOpenPosition.Columns.Add("CurrentValue", System.Type.GetType("System.Decimal"));
                MFSummaryOpenPosition.Columns.Add("PreviousNetCost", System.Type.GetType("System.Decimal"));
                MFSummaryOpenPosition.Columns.Add("CurrentNetCost", System.Type.GetType("System.Decimal"));
                

                MFSummaryOpenPosition.TableName = "MFSummaryOpenPosition";

                DataSet dsPortfolioSummary = mfReports.GetMFFundSummaryReport(report, adviserId);

                dtCurrent = dsPortfolioSummary.Tables[0];
                dtPrevious = dsPortfolioSummary.Tables[1];

                //dtCurrent.Merge(dtPrevious);


                foreach (DataRow drCurrent in dtCurrent.Rows)
                {
                    foreach (DataRow drPrevious in dtPrevious.Rows)
                    {
                        if (drCurrent["PortfolioId"].ToString() == drPrevious["PortfolioId"].ToString() && drCurrent["Category"].ToString() == drPrevious["Category"].ToString())
                        {
                            drCurrent["PreviousValue"] = drPrevious["PreviousValue"];
                            drCurrent["PreviousNetCost"] = drPrevious["PreviousNetCost"];
                            break;
                        }
                    }
                    if (drCurrent["PreviousNetCost"].ToString() == string.Empty)
                        drCurrent["PreviousNetCost"] = 0.00;
                    if (drCurrent["PreviousValue"].ToString() == string.Empty)
                        drCurrent["PreviousValue"] = 0.00;
                    if (drCurrent["CurrentValue"].ToString() == string.Empty)
                        drCurrent["CurrentValue"] = 0.00;
                    if (drCurrent["CurrentNetCost"].ToString() == string.Empty)
                        drCurrent["CurrentNetCost"] = 0.00;
                    MFSummaryOpenPosition.ImportRow(drCurrent);
                }


                foreach (DataRow drPrevious in dtPrevious.Rows)
                {
                    string expression = string.Empty;
                    expression = "PortfolioId = " + drPrevious["PortfolioId"] + " and Category = '" + drPrevious["Category"] + "'";
                    if (MFSummaryOpenPosition.Select(expression) == null)
                    {
                        MFSummaryOpenPosition.ImportRow(drPrevious);
                        break;
                    }

                }
                dsMFSummary.Tables.Add(MFSummaryOpenPosition);

                // Code to combine two datatables (current and previous values data for sub reports) into one.
                DataTable dtCurrentSR = new DataTable();
                DataTable dtPreviousSR = new DataTable();

                DataTable MFSummaryAllPosition = new DataTable();
                MFSummaryAllPosition.Columns.Add("CustomerName");
                MFSummaryAllPosition.Columns.Add("CustomerId");
                MFSummaryAllPosition.Columns.Add("PortfolioName");
                MFSummaryAllPosition.Columns.Add("PortfolioId");
                MFSummaryAllPosition.Columns.Add("Category");
                MFSummaryAllPosition.Columns.Add("PreviousDVP", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentDVP", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("PreviousDVR", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentDVR", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("PreviousDVI", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentDVI", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("PreviousNPL", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentNPL", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("PreviousRPL", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentRPL", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("PreviousTPL", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentTPL", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("PreviousSTCG", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentSTCG", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("PreviousLTCG", System.Type.GetType("System.Decimal"));
                MFSummaryAllPosition.Columns.Add("CurrentLTCG", System.Type.GetType("System.Decimal"));

                MFSummaryAllPosition.TableName = "MFSummaryAllPosition";

                DataSet dsPortfolioSummarySR = mfReports.GetMFFundSummaryReportAllPosition(report, adviserId);

                dtCurrentSR = dsPortfolioSummarySR.Tables[0];
                dtPreviousSR = dsPortfolioSummarySR.Tables[1];

                //dtCurrent.Merge(dtPrevious);


                foreach (DataRow drCurrent in dtCurrentSR.Rows)
                {
                    foreach (DataRow drPrevious in dtPreviousSR.Rows)
                    {
                        if (drCurrent["PortfolioId"].ToString() == drPrevious["PortfolioId"].ToString() && drCurrent["Category"].ToString() == drPrevious["Category"].ToString())
                        {
                            drCurrent["PreviousDVP"] = drPrevious["PreviousDVP"];
                            drCurrent["PreviousDVR"] = drPrevious["PreviousDVR"];
                            drCurrent["PreviousDVI"] = drPrevious["PreviousDVI"];
                            drCurrent["PreviousNPL"] = drPrevious["PreviousNPL"];
                            drCurrent["PreviousRPL"] = drPrevious["PreviousRPL"];
                            drCurrent["PreviousTPL"] = drPrevious["PreviousTPL"];
                            drCurrent["PreviousSTCG"] = drPrevious["PreviousSTCG"];
                            drCurrent["PreviousLTCG"] = drPrevious["PreviousLTCG"];

                            break;
                        }
                    }
                    if (drCurrent["PreviousDVP"].ToString() == string.Empty)
                        drCurrent["PreviousDVP"] = 0.00;
                    if (drCurrent["PreviousDVR"].ToString() == string.Empty)
                        drCurrent["PreviousDVR"] = 0.00;
                    if (drCurrent["PreviousDVI"].ToString() == string.Empty)
                        drCurrent["PreviousDVI"] = 0.00;
                    if (drCurrent["PreviousNPL"].ToString() == string.Empty)
                        drCurrent["PreviousNPL"] = 0.00;
                    if (drCurrent["PreviousRPL"].ToString() == string.Empty)
                        drCurrent["PreviousRPL"] = 0.00;
                    if (drCurrent["PreviousTPL"].ToString() == string.Empty)
                        drCurrent["PreviousTPL"] = 0.00;
                    if (drCurrent["PreviousSTCG"].ToString() == string.Empty)
                        drCurrent["PreviousSTCG"] = 0.00;
                    if (drCurrent["PreviousLTCG"].ToString() == string.Empty )
                        drCurrent["PreviousLTCG"] = 0.00;
                    if (drCurrent["CurrentDVP"].ToString() == string.Empty)
                        drCurrent["CurrentDVP"] = 0.00;
                    if (drCurrent["CurrentDVR"].ToString() == string.Empty)
                        drCurrent["CurrentDVR"] = 0.00;
                    if (drCurrent["CurrentDVI"].ToString() == string.Empty)
                        drCurrent["CurrentDVI"] = 0.00;
                    if (drCurrent["CurrentNPL"].ToString() == string.Empty)
                        drCurrent["CurrentNPL"] = 0.00;
                    if (drCurrent["CurrentRPL"].ToString() == string.Empty)
                        drCurrent["CurrentRPL"] = 0.00;
                    if (drCurrent["CurrentTPL"].ToString() == string.Empty)
                        drCurrent["CurrentTPL"] = 0.00;
                    if (drCurrent["CurrentSTCG"].ToString() == string.Empty)
                        drCurrent["CurrentSTCG"] = 0.00;
                    if (drCurrent["CurrentLTCG"].ToString() == string.Empty)
                        drCurrent["CurrentLTCG"] = 0.00;


                    MFSummaryAllPosition.ImportRow(drCurrent);
                }


                foreach (DataRow drPrevious in dtPreviousSR.Rows)
                {
                    string expression = string.Empty;
                    expression = "PortfolioId = " + drPrevious["PortfolioId"] + " and Category = '" + drPrevious["Category"] + "'";
                    if (MFSummaryAllPosition.Select(expression) == null)
                    {
                        MFSummaryAllPosition.ImportRow(drPrevious);
                        break;
                    }
                }
                dsMFSummary.Tables.Add(MFSummaryAllPosition);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsMFSummary;
        }

        public DataTable GetTransactionReport(MFReportVo reports)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetTransactionReport(reports);
        }

        public DataTable GetDivdendReport(MFReportVo reports)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetDivdendReport(reports);
        }

        public DataSet GetReturnSummaryReport(MFReportVo reports, int adviserId)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetReturnSummaryReport(reports, adviserId);
        }
        /// <summary>
        /// Get Customer Portfolio Analytics Report Data
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetPortfolioAnalyticsReport(MFReportVo reports, int adviserId)
        {

            MFReportsDao mfReports = new MFReportsDao();
            DataSet dsCustomerMFReturns;
            double totalValue = 0;
            double holdingstotal = 0;
            double otherholdingsTotal = 0;
            double sectorTotal = 0;
            double otherSectorTotal = 0;
            try
            {
                dsCustomerMFReturns = mfReports.GetPortfolioAnalyticsReport(reports, adviserId);
                //------Old code for generating Comperehensive report(Holding wise,sector wise and scheme performance)
                //if (dsCustomerMFReturns != null)
                //{
                //    if (dsCustomerMFReturns.Tables[4].Rows.Count != 0)
                //    {
                //       DataRow drTotal=dsCustomerMFReturns.Tables[4].Rows[0];
                //       if (!string.IsNullOrEmpty(drTotal["Amount"].ToString()))
                //       totalValue = double.Parse(drTotal["Amount"].ToString());
                //    }
                //    foreach (DataRow drHoldings in dsCustomerMFReturns.Tables[2].Rows)
                //    {
                //        holdingstotal = holdingstotal + double.Parse(drHoldings["Amount"].ToString());
                //    }
                //    foreach (DataRow drSector in dsCustomerMFReturns.Tables[5].Rows)
                //    {
                //        sectorTotal = sectorTotal + double.Parse(drSector["Amount"].ToString());
                //    }
                //    otherSectorTotal = totalValue - sectorTotal;
                //    if(dsCustomerMFReturns.Tables[3].Rows.Count!=0 && dsCustomerMFReturns.Tables[3].Rows[0]["Amount"].ToString()!="")
                //        otherholdingsTotal = totalValue - double.Parse(dsCustomerMFReturns.Tables[3].Rows[0]["Amount"].ToString()) - holdingstotal;
                //    if (dsCustomerMFReturns.Tables[2].Rows.Count != 0)
                //    {
                //        DataRow dr = dsCustomerMFReturns.Tables[2].NewRow();
                //        dr["Instrument"] = "Other Holdings";
                //        dr["InsType"] = "Other";
                //        dr["Amount"] = otherholdingsTotal.ToString();
                //        dsCustomerMFReturns.Tables[2].Rows.Add(dr);
                //        dr = dsCustomerMFReturns.Tables[2].NewRow();
                //        dr["Instrument"] = dsCustomerMFReturns.Tables[3].Rows[0]["Instrument"].ToString();
                //        dr["InsType"] = dsCustomerMFReturns.Tables[3].Rows[0]["InsType"].ToString();
                //        dr["Amount"] = double.Parse(dsCustomerMFReturns.Tables[3].Rows[0]["Amount"].ToString());
                //        dsCustomerMFReturns.Tables[2].Rows.Add(dr);
                //       // dr = dsCustomerMFReturns.Tables[2].NewRow();
                //        //dr["Instrument"] = dsCustomerMFReturns.Tables[4].Rows[0]["Instrument"].ToString();
                //        //dr["InsType"] = "";
                //        //dr["Amount"] = dsCustomerMFReturns.Tables[4].Rows[0]["Amount"].ToString();
                //        //dsCustomerMFReturns.Tables[2].Rows.Add(dr);
                //    }
                //    dsCustomerMFReturns.Tables[2].Columns.Add("Percentage");
                //    if(totalValue!=0)
                //    {
                //        for (int i = 0; i < dsCustomerMFReturns.Tables[2].Rows.Count; i++)
                //        {
                //            dsCustomerMFReturns.Tables[2].Rows[i]["Percentage"] = Math.Round(double.Parse((((double.Parse(dsCustomerMFReturns.Tables[2].Rows[i]["Amount"].ToString())) / totalValue) * 100).ToString()),2);
                //        }
                //    }
                //    if (dsCustomerMFReturns.Tables[5].Rows.Count != 0)
                //    {
                //        DataRow drSectorHoldings = dsCustomerMFReturns.Tables[5].NewRow();
                //        drSectorHoldings["SectorCode"] = "0";
                //        drSectorHoldings["Sector"] = "Others";
                //        drSectorHoldings["Amount"] = otherSectorTotal.ToString();
                //        dsCustomerMFReturns.Tables[5].Rows.Add(drSectorHoldings);
                //    }
                //    dsCustomerMFReturns.Tables[5].Columns.Add("Percentage");
                //    if (totalValue != 0)
                //    {
                //        for (int i = 0; i < dsCustomerMFReturns.Tables[5].Rows.Count; i++)
                //        {
                //            dsCustomerMFReturns.Tables[5].Rows[i]["Percentage"] = Math.Round(double.Parse((((double.Parse(dsCustomerMFReturns.Tables[5].Rows[i]["Amount"].ToString())) / totalValue) * 100).ToString()),2);
                //        }
                  // }
                //}

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCustomerMFReturns;
        }
        /// <summary>
        /// Returns DataTable For "Portfolio Returns Realized" Report --Author:Pramod
        /// </summary>
        /// <param name="reports"> "reports" is a object of "MFReportVo" Contails report parameters</param>
        /// <param name="adviserId">Get the data of all the customer belong to This Id</param>
        /// <returns>DataTable </returns>
        public DataTable GetMFReturnRESummaryReport(MFReportVo reports, int adviserId)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetMFReturnRESummaryReport(reports, adviserId);
        }

        public DataTable GetReturnTransactionSummaryReport(MFReportVo reports)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetReturnTransactionSummaryReport(reports);
        }


        public DataTable GetCapitalGainSummaryReport(MFReportVo reports)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetCapitalGainSummaryReport(reports);
        }

        public DataTable GetCapitalGainDetailsReport(MFReportVo reports)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetCapitalGainDetailsReport(reports);
        }


        /// <summary>
        /// Returns DataTable For "Eligible Capital Gain Details & Summary" Report --Author:Pramod
        /// </summary>
        /// <param name="reports">"reports" is a object of "MFReportVo" Contails report parameters</param>
        /// <returns>DataTable</returns>
        public DataTable GetEligibleCapitalGainDetailsReport(MFReportVo reports)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetEligibleCapitalGainDetailsReport(reports);
        }

        public DataSet GetMFTransactionType()
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetMFTransactionType();
        }

        /// <summary>
        /// Calculate FromDate for Since Inception Option for Period Selection
        /// </summary>
        /// <param name="portfolioIDs"></param>
        /// <param name="subreportype"></param>
        /// <returns></returns>
        public DateTime GetCalculateFromDate(string portfolioIDs, string subreportype)
        {
            MFReportsDao mfReports = new MFReportsDao();
            DateTime fromDate = DateTime.MinValue;
            
           
            try
            {
                fromDate = mfReports.GetCalculateFromDate(portfolioIDs, subreportype, out fromDate);
             
            }
            catch (BaseApplicationException ex)
            {
                throw (ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFReports.cs:GetCalculateFromDate()");
                object[] objects = new object[2];
                objects[0] = portfolioIDs;
                objects[1] = subreportype;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return fromDate;
        }

        public DataTable GetOpeningClosingTransactionReport(MFReportVo reports)
        {
            MFReportsDao mfReportsDao = new MFReportsDao();
            DataSet dsTransactionList;
            dsTransactionList = mfReportsDao.GetOpeningClosingTransactionReport(reports);
            DataTable dtTransactionList;
            DataTable dtSchemeOpeningBalance;
            DataTable dtSchemeClosingBalance;            
            dtTransactionList = dsTransactionList.Tables[0];
            dtSchemeOpeningBalance = dsTransactionList.Tables[1];
            dtSchemeClosingBalance = dsTransactionList.Tables[2];
            DataRow drOpeningClosingSchemeWise;

            DataTable dtOpeningClosingSchemeWise = new DataTable();
            dtOpeningClosingSchemeWise.Columns.Add("CustomerName");
            dtOpeningClosingSchemeWise.Columns.Add("CP_PortfolioName");
            dtOpeningClosingSchemeWise.Columns.Add("PASP_SchemePlanName");
            dtOpeningClosingSchemeWise.Columns.Add("WMTT_TransactionType");
            dtOpeningClosingSchemeWise.Columns.Add("CMFT_TransactionDate");
            dtOpeningClosingSchemeWise.Columns.Add("CMFT_Price", System.Type.GetType("System.Decimal"));
            dtOpeningClosingSchemeWise.Columns.Add("CMFT_Units");
            dtOpeningClosingSchemeWise.Columns.Add("CMFT_Amount", System.Type.GetType("System.Decimal"));
            dtOpeningClosingSchemeWise.Columns.Add("CMFT_OpeningBalance", System.Type.GetType("System.Decimal"));
            dtOpeningClosingSchemeWise.Columns.Add("CMFT_ClosingBalance", System.Type.GetType("System.Decimal"));
            DataRow[] drOpening=new DataRow[3];
            DataRow[] drClosing = new DataRow[3];
            string tempSchemePlanCode=string.Empty;

            if (dtTransactionList.Rows.Count > 0 && dtSchemeOpeningBalance.Rows.Count > 0 && dtSchemeClosingBalance.Rows.Count > 0)            
            {
                foreach (DataRow dr in dtTransactionList.Rows)
                {
                    drOpeningClosingSchemeWise = dtOpeningClosingSchemeWise.NewRow();

                    drOpeningClosingSchemeWise["CustomerName"] = dr["CustomerName"];
                    drOpeningClosingSchemeWise["CP_PortfolioName"] = dr["CP_PortfolioName"];
                    if (dr["PASP_SchemePlanCode"].ToString() != tempSchemePlanCode)
                    {
                        tempSchemePlanCode = dr["PASP_SchemePlanCode"].ToString().Trim();
                        drOpening = dtSchemeOpeningBalance.Select("PASP_SchemePlanCode='" + tempSchemePlanCode + "'");
                        drClosing = dtSchemeClosingBalance.Select("PASP_SchemePlanCode='" + tempSchemePlanCode + "'");
                    }
                    
                    drOpeningClosingSchemeWise["PASP_SchemePlanName"] = dr["PASP_SchemePlanName"];
                    drOpeningClosingSchemeWise["WMTT_TransactionType"] = dr["WMTT_TransactionType"];
                    drOpeningClosingSchemeWise["CMFT_TransactionDate"] = dr["CMFT_TransactionDate"];
                    drOpeningClosingSchemeWise["CMFT_Price"] = dr["CMFT_Price"];
                    drOpeningClosingSchemeWise["CMFT_Units"] = dr["CMFT_Units"];
                    drOpeningClosingSchemeWise["CMFT_Amount"] = dr["CMFT_Amount"];
                    if (drOpening.Count() > 0)
                    {
                        drOpeningClosingSchemeWise["CMFT_OpeningBalance"] = drOpening[0][2].ToString();
                    }
                    else
                        drOpeningClosingSchemeWise["CMFT_OpeningBalance"] = 0.0;

                    if (drClosing.Count() > 0)
                    {
                        drOpeningClosingSchemeWise["CMFT_ClosingBalance"] = drClosing[0][2].ToString();
                    }
                    else
                        drOpeningClosingSchemeWise["CMFT_ClosingBalance"] = 0.0;
                    

                    //drOpeningClosingSchemeWise["CMFT_OpeningBalance"] = 

                    dtOpeningClosingSchemeWise.Rows.Add(drOpeningClosingSchemeWise);

                }

            }

            return dtOpeningClosingSchemeWise;
        }


        public DataSet GetOrderTransactionForm(OrderTransactionSlipVo report)
        {
            MFReportsDao mfReportsDao = new MFReportsDao();
            DataSet dsTransactionSlip = new DataSet();
            try
            {
                dsTransactionSlip = mfReportsDao.GetOrderTransactionForm(report);
            }
            catch(BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFReports.cs:GetOrderTransactionForm()");
                object[] objects = new object[3];
                objects[0] = report.CustomerId;
                objects[1] = report.SchemeCode;
                objects[2] = report.Type;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsTransactionSlip;
        }

        public DataTable GetMFRealizedReport(MFReportVo report, int adviserId)
        {
            MFReportsDao mfReports = new MFReportsDao();
            return mfReports.GetMFRealizedReport(report, adviserId);
        }

        public void LogCustomerMFReportEmailStatus(Dictionary<string, string> MFReportEmailStatus)
        {
            MFReportsDao mfReports = new MFReportsDao();
            mfReports.LogCustomerMFReportEmailStatus(MFReportEmailStatus);
        }


        public DataSet GetOrderTransactionBlankForm(OrderTransactionSlipVo report)
        {
            MFReportsDao mfReportsDao = new MFReportsDao();
            DataSet dsTransactionSlipBlankForm = new DataSet();
            try
            {
                dsTransactionSlipBlankForm = mfReportsDao.GetOrderTransactionBlankForm(report);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsTransactionSlipBlankForm;
        }
        public DataSet GetPortfolioCompositionReport(MFReportVo reports, int adviserId)
        {

            MFReportsDao mfReports = new MFReportsDao();
            DataSet dsCustomerPortfolioComposition;
          
            try
            {
                dsCustomerPortfolioComposition = mfReports.GetPortfolioCompositionReport(reports, adviserId);
              
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsCustomerPortfolioComposition;
        }

        public DataSet GetARNNoAndJointHoldings(int OCustomerId, int OportfolioId,string oFolio)
        {
            MFReportsDao mfReports = new MFReportsDao();
            DataSet dsGetARNNoAndJointHoldings;

            try
            {
                dsGetARNNoAndJointHoldings = mfReports.GetARNNoAndJointHoldings(OCustomerId, OportfolioId, oFolio);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dsGetARNNoAndJointHoldings;
        }




        //-------------------------------------------------------------------------New Calculation---------------------------
        //----------Pairing Starts
        public DataSet CreateInvetmentsPairingAccountStockWise(DataTable dtTransactions)
        {
            string FolioNum = "";
            int ScripCode = 0;
            DataTable dtAccountList = new DataTable();
            DataTable dtBuyTransactions = new DataTable();
            DataTable dtSellTransactions = new DataTable();
            DataSet dsTransactoinsAfterPairing = new DataSet();
            DataSet ds = new DataSet();
            DataTable dtCustomerTransactionToProcess = new DataTable();
            DataTable dtAcountWiseBuyTransaction = new DataTable();
            DataTable dtAcountWiseSellTransaction = new DataTable();
            string[] columnNames = new string[] { "ScripCode", "FolioNum" };
            dtAccountList = new DataView(dtTransactions).ToTable(true, columnNames);
            dtCustomerTransactionToProcess = dtTransactions.Copy();
            foreach (DataRow drAccounts in dtAccountList.Rows)
            {

                FolioNum = drAccounts["FolioNum"].ToString();
                ScripCode = int.Parse(drAccounts["ScripCode"].ToString());

                try
                {
                    if (dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell<>'S' ").Count() > 0)
                    {
                        dtAcountWiseBuyTransaction = dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell<>'S' ", "TransactionDate ASC").CopyToDataTable();
                        if (dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell='S' ").Count() > 0)
                        {
                            dtAcountWiseSellTransaction = dtCustomerTransactionToProcess.Select("FolioNum='" + FolioNum + "' AND ScripCode='" + ScripCode + "' AND BuySell='S' ", "TransactionDate ASC").CopyToDataTable();
                        }
                        else
                            dtAcountWiseSellTransaction = dtCustomerTransactionToProcess.Clone();
                        ds = CreateMFNetPositionPairing(dtAcountWiseBuyTransaction, dtAcountWiseSellTransaction);
                        dtBuyTransactions.Merge(ds.Tables[0]);
                        dtSellTransactions.Merge(ds.Tables[1]);
                        ds.Clear();
                    }

                    ScripCode = 0;
                    FolioNum = "";

                }
                catch (Exception ex)
                {
                }
            }
            dsTransactoinsAfterPairing.Tables.Add(dtBuyTransactions);
            dsTransactoinsAfterPairing.Tables.Add(dtSellTransactions);
            return dsTransactoinsAfterPairing;
        }
        public DataSet CreateMFNetPositionPairing(DataTable tempBuyTransaction, DataTable tempSellTransaction)
        {
            DataSet ds = new DataSet();
            decimal sellQuantity = 0;
            decimal buyQuantity = 0;
            int rowCountBuy = 0;
            foreach (DataRow drSell in tempSellTransaction.Rows)
            {
                sellQuantity = decimal.Parse(drSell["Units"].ToString());
                while (sellQuantity > 0)
                {
                    DataRow drBuy;
                    if (rowCountBuy < (tempBuyTransaction.Rows.Count - 1))
                        drBuy = tempBuyTransaction.Rows[rowCountBuy];
                    else
                        if (tempBuyTransaction.Rows.Count > 0)
                            drBuy = tempBuyTransaction.Rows[tempBuyTransaction.Rows.Count - 1];//tempBuyTransaction.Select("CET_IsSpeculative='" + tranxType + "'  ").Last();//
                        else
                            drBuy = null;

                    if (drBuy != null)
                    {
                        buyQuantity = decimal.Parse(drBuy["Units"].ToString());
                        if (buyQuantity >= sellQuantity)
                        {
                            buyQuantity = buyQuantity - sellQuantity;
                            sellQuantity = 0;
                            drBuy["Units"] = buyQuantity.ToString();
                            drSell["Units"] = 0;
                            if (buyQuantity == 0)
                                rowCountBuy = rowCountBuy + 1;
                        }
                        else
                        {
                            if (rowCountBuy >= (tempBuyTransaction.Rows.Count - 1))
                            {
                                buyQuantity = buyQuantity - sellQuantity;
                                drBuy["Units"] = buyQuantity.ToString();
                                drSell["Units"] = 0;
                                sellQuantity = 0;
                            }
                            else
                            {
                                sellQuantity = sellQuantity - buyQuantity;
                                drBuy["Units"] = 0;
                                drSell["Units"] = sellQuantity.ToString();
                                rowCountBuy = rowCountBuy + 1;
                            }

                        }
                    }
                    else
                    {
                        //Exception code goes here

                        drSell["Units"] = (-sellQuantity).ToString();
                        sellQuantity = -sellQuantity;
                    }

                }

            }
            ds.Tables.Add(tempBuyTransaction);
            ds.Tables.Add(tempSellTransaction);
            return ds;
        }
        //----------Pairing Ends






        public DataSet CreateCustomerMFReturnsHolding(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {

            MFReportsDao mfReports = new MFReportsDao();
            DataTable dtTranxn = mfReports.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            DataTable dtMFReturnsHolding = new DataTable();
            //dtMFReturnsHolding.Columns.Add("C_CustomerId");
            dtMFReturnsHolding.Columns.Add("CustomerName");
            //dtMFReturnsHolding.Columns.Add("PASP_SchemePlanCode");

            //dtMFReturnsHolding.Columns.Add("Scheme");
            dtMFReturnsHolding.Columns.Add("PASP_SchemePlanShortName");
            dtMFReturnsHolding.Columns.Add("FolioNo");
            dtMFReturnsHolding.Columns.Add("PortfolioId");
            //dtMFReturnsHolding.Columns.Add("portfolioName");
            dtMFReturnsHolding.Columns.Add("InvestmentStartDate");

            dtMFReturnsHolding.Columns.Add("InvestedCost", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("All_Current", typeof(decimal));

            dtMFReturnsHolding.Columns.Add("UnitsAll", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("Avg_Nav", typeof(decimal));

            dtMFReturnsHolding.Columns.Add("NAV", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("CMFNP_NAVDate");
            dtMFReturnsHolding.Columns.Add("Divident_DVR", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("Divident_DVP", typeof(decimal));

            dtMFReturnsHolding.Columns.Add("Divident_Total", typeof(decimal));

            dtMFReturnsHolding.Columns.Add("PL", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("AbsoluteReturn");
            dtMFReturnsHolding.Columns.Add("XIRR");

            dtMFReturnsHolding.Columns.Add("Category");


            DataTable dtMFCustomerTotalXIRR = new DataTable();
            dtMFCustomerTotalXIRR.Columns.Add("CustomerName");
            dtMFCustomerTotalXIRR.Columns.Add("PortfolioName");
            dtMFCustomerTotalXIRR.Columns.Add("XIRR");
            dtMFCustomerTotalXIRR.Columns.Add("AbsoluteReturn");
            DataSet dsMFHoldingReturns = new DataSet();
            DataTable dtAllXirr = new DataTable();
            if (dtTranxn.Rows.Count > 0)
            {
                DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());

                DataTable dtBuy = ds.Tables[0];
                dtBuy.Columns.Add("AmountNew", typeof(decimal), "Units*Price");
                dtBuy.Columns.Add("CurrentAmount", typeof(decimal), "Units*MktPrice");
                dtBuy.Columns.Add("CostOfSales", typeof(decimal), "(UnitsBefore-Units)*Price");
                dtTranxn.Columns.Add("AmountNew", typeof(decimal), "Amount");
                DataTable dtSchemeList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "MktPriceDate", "PAIC_AssetInstrumentCategoryName" });
                DataTable dtCustomerList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId" });

                foreach (DataRow dr in dtSchemeList.Rows)
                {

                    decimal BalanceUnits = 0;
                    BalanceUnits = decimal.Parse(dtBuy.Compute("Sum(Units)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    if (BalanceUnits > 1)
                    {
                        DataRow drNew = dtMFReturnsHolding.NewRow();
                        drNew["CustomerName"] = dr["CustomerName"].ToString();
                        drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                        drNew["PASP_SchemePlanShortName"] = dr["ScripName"].ToString();
                        drNew["FolioNo"] = dr["FolioNum"].ToString();
                        drNew["Category"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        drNew["InvestmentStartDate"] = DateTime.Parse(dtBuy.Compute("MIN(TransactionDate)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units<>0").ToString()).ToString("dd/MM/yyyy");
                        drNew["NAV"] = decimal.Parse(dr["MktPrice"].ToString());


                        if (dr["MktPriceDate"].ToString() != "")
                            drNew["CMFNP_NAVDate"] = DateTime.Parse(dr["MktPriceDate"].ToString()).ToString("dd/MM/yyyy");

                        drNew["UnitsAll"] = BalanceUnits.ToString();
                        if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR'").Count() > 0)
                            drNew["InvestedCost"] = decimal.Parse(dtBuy.Compute("Sum(AmountNew)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR'").ToString());
                        else
                            drNew["InvestedCost"] = 0;
                        drNew["Avg_Nav"] = decimal.Parse(drNew["InvestedCost"].ToString()) / BalanceUnits;
                        drNew["All_Current"] = decimal.Parse(dr["MktPrice"].ToString()) * BalanceUnits;
                        drNew["PL"] = decimal.Parse(drNew["All_Current"].ToString()) - decimal.Parse(drNew["InvestedCost"].ToString());
                        if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode='DVR'").Count() > 0)
                            drNew["Divident_DVR"] = decimal.Parse(dtBuy.Compute("Sum(AmountNew)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode='DVR'").ToString());
                        else
                            drNew["Divident_DVR"] = 0;




                        if (dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvestmentStartDate"].ToString()).ToString("MM/dd/yyyy") + "'").Count() > 0)
                            drNew["Divident_DVP"] = decimal.Parse(dtTranxn.Compute("Sum(Amount)", "TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvestmentStartDate"].ToString()).ToString("MM/dd/yyyy") + "'").ToString());
                        else
                            drNew["Divident_DVP"] = 0;

                        drNew["Divident_Total"] = decimal.Parse(drNew["Divident_DVP"].ToString()) + decimal.Parse(drNew["Divident_DVR"].ToString());

                        if (decimal.Parse(drNew["InvestedCost"].ToString()) != 0)
                            drNew["AbsoluteReturn"] = Math.Round((decimal.Parse(drNew["PL"].ToString()) / (decimal.Parse(drNew["InvestedCost"].ToString()))) * 100, 2);
                        if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units<>0 AND AmountNew<>0 AND TranxnTypeCode<>'DVR'").Count() > 0)
                        {
                            DataTable dtXirr = new DataView(dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND Units<>0 AND AmountNew<>0 AND TranxnTypeCode<>'DVR'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" });
                            dtAllXirr.Merge(dtXirr);
                            if (dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvestmentStartDate"].ToString()).ToString("MM/dd/yyyy") + "'").Count() > 0)
                            {
                                dtAllXirr.Merge(new DataView(dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvestmentStartDate"].ToString()).ToString("MM/dd/yyyy") + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" }));
                                dtXirr.Merge(new DataView(dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TransactionDate >='" + DateTime.Parse(drNew["InvestmentStartDate"].ToString()).ToString("MM/dd/yyyy") + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" }));
                            }
                            dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                            dtXirr.DefaultView.Sort = "TransactionDate ASC";
                            DataTable dt = dtXirr.DefaultView.ToTable();
                            double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                            DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                            int tempCount = 0;
                            foreach (DataRow drRow in dt.Rows)
                            {
                                if (drRow["BuySell"].ToString() != "S")
                                    transactionAmount[tempCount] = -(double.Parse(drRow["AmountNew"].ToString()));
                                else
                                    transactionAmount[tempCount] = (double.Parse(drRow["AmountNew"].ToString()));
                                transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                                tempCount++;
                            }

                            transactionAmount[tempCount] = double.Parse(drNew["All_Current"].ToString());
                            transactionDate[tempCount] = ToDate;

                            double xirr = CalculateXIRR(transactionAmount, transactionDate);
                            if (xirr < 10000)
                                drNew["XIRR"] = decimal.Parse(xirr.ToString());
                            else
                                drNew["XIRR"] = 0;
                        }
                        else
                            drNew["XIRR"] = 0;

                        dtMFReturnsHolding.Rows.Add(drNew);
                    }
                }
                dsMFHoldingReturns.Tables.Add(dtMFReturnsHolding);
                if (dtAllXirr.Rows.Count > 0 && dtMFReturnsHolding.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomerList.Rows)
                    {
                        if (dtAllXirr.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "'").Count() > 0)
                        {
                            DataRow drPortfolioXirr = dtMFCustomerTotalXIRR.NewRow();
                            drPortfolioXirr["CustomerName"] = dr["CustomerName"].ToString();
                            drPortfolioXirr["PortfolioName"] = dr["PortfolioName"].ToString();

                            DataTable dtXirr = new DataView(dtAllXirr.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "'", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "AmountNew", "BuySell", "PortfolioId" });
                            dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                            dtXirr.DefaultView.Sort = "TransactionDate ASC";
                            DataTable dt = dtXirr.DefaultView.ToTable();
                            double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                            DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                            int tempCount = 0;
                            foreach (DataRow drRow in dt.Rows)
                            {
                                if (drRow["BuySell"].ToString() != "S")
                                    transactionAmount[tempCount] = -(double.Parse(drRow["AmountNew"].ToString()));
                                else
                                    transactionAmount[tempCount] = (double.Parse(drRow["AmountNew"].ToString()));
                                transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                                tempCount++;
                            }

                            transactionAmount[tempCount] = double.Parse(dtMFReturnsHolding.Compute("Sum(All_Current)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString());
                            transactionDate[tempCount] = DateTime.Now;

                            double xirr = CalculateXIRR(transactionAmount, transactionDate);
                            if (xirr < 10000)
                                drPortfolioXirr["XIRR"] = decimal.Parse(xirr.ToString());
                            else
                                drPortfolioXirr["XIRR"] = 0;

                            drPortfolioXirr["AbsoluteReturn"] = Math.Round(double.Parse(dtMFReturnsHolding.Compute("Sum(PL)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()) * 100 / double.Parse(dtMFReturnsHolding.Compute("Sum(InvestedCost)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()), 2);
                            dtMFCustomerTotalXIRR.Rows.Add(drPortfolioXirr);
                        }
                    }

                }
                dsMFHoldingReturns.Tables.Add(dtMFCustomerTotalXIRR);
            }
            else
            {
                dsMFHoldingReturns.Tables.Add(dtMFReturnsHolding);
                dsMFHoldingReturns.Tables.Add(dtMFCustomerTotalXIRR);
            }
            return dsMFHoldingReturns;

        }
        public DataSet CreateCustomerMFComprehensive(int C_CustomerId, string PortfolioIds, DateTime? FromDate, DateTime ToDate)
        {
            MFReportsDao mfReports = new MFReportsDao();
            DataTable dtTranxn = mfReports.GetCustomerMFTransactions(C_CustomerId, PortfolioIds, FromDate, ToDate).Tables[0];
            DataSet dsMFComprehensive = new DataSet();
            DataTable dtMFReturnsHolding = new DataTable();
            dtMFReturnsHolding.Columns.Add("CustomerName");
            // dtMFReturnsHolding.Columns.Add("CustomerId");
            dtMFReturnsHolding.Columns.Add("PortfolioName");
            dtMFReturnsHolding.Columns.Add("PortfolioId");
            dtMFReturnsHolding.Columns.Add("PASP_SchemePlanShortName");
            dtMFReturnsHolding.Columns.Add("Folio");
            dtMFReturnsHolding.Columns.Add("CMFNP_FolioSchemeStartDate");
            dtMFReturnsHolding.Columns.Add("Units", typeof(decimal));
            //dtMFReturnsHolding.Columns.Add("AvgPrice", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("InvestedCost", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("NAV", typeof(decimal));
            //dtMFReturnsHolding.Columns.Add("CurrentNAVDate");
            dtMFReturnsHolding.Columns.Add("CurrentValue", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("RedeemedAmount", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("DVR", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("DVP", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("PL", typeof(decimal));
            dtMFReturnsHolding.Columns.Add("AbsReturn");
            dtMFReturnsHolding.Columns.Add("TotalXIRR");
            dtMFReturnsHolding.Columns.Add("Category");
            DataTable dtMFCustomerTotalXIRR = new DataTable();
            dtMFCustomerTotalXIRR.Columns.Add("CustomerName");
            dtMFCustomerTotalXIRR.Columns.Add("PortfolioName");
            dtMFCustomerTotalXIRR.Columns.Add("XIRR");
            dtMFCustomerTotalXIRR.Columns.Add("AbsoluteReturn");
            if (dtTranxn.Rows.Count > 0)
            {
                DataSet ds = CreateInvetmentsPairingAccountStockWise(dtTranxn.Select("TranxnTypeCode <>'DVP'", "CustomerName ASC,ScripName ASC,TransactionDate ASC").CopyToDataTable());
                DataTable dtBuy = ds.Tables[0];
                DataTable dtSell = ds.Tables[1];
                dtBuy.Columns.Add("AmountNew", typeof(decimal), "Units*Price");
                dtBuy.Columns.Add("CurrentAmount", typeof(decimal), "Units*MktPrice");
                dtBuy.Columns.Add("CostOfSales", typeof(decimal), "(UnitsBefore-Units)*Price");
                DataTable dtSchemeList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId", "FolioNum", "ScripCode", "ScripName", "MktPrice", "MktPriceDate", "PAIC_AssetInstrumentCategoryName" });
                DataTable dtCustomerList = new DataView(dtBuy).ToTable(true, new[] { "CustomerName", "CustomerId", "PortfolioName", "PortfolioId" });

                foreach (DataRow dr in dtSchemeList.Rows)
                {

                    decimal BalanceUnits = 0;
                    BalanceUnits = decimal.Parse(dtBuy.Compute("Sum(Units)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());

                    DataRow drNew = dtMFReturnsHolding.NewRow();
                    drNew["CustomerName"] = dr["CustomerName"].ToString();
                    //drNew["CustomerId"] = dr["CustomerId"].ToString();
                    drNew["PortfolioName"] = dr["PortfolioName"].ToString();
                    drNew["PortfolioId"] = dr["PortfolioId"].ToString();
                    drNew["PASP_SchemePlanShortName"] = dr["ScripName"].ToString();
                    drNew["Folio"] = dr["FolioNum"].ToString();
                    drNew["Category"] = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                    drNew["CMFNP_FolioSchemeStartDate"] = DateTime.Parse(dtBuy.Compute("MIN(TransactionDate)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString()).ToString("dd/MM/yyyy");
                    drNew["NAV"] = decimal.Parse(dr["MktPrice"].ToString());
                    //if (dr["MktPriceDate"].ToString() != "")
                    //    drNew["CurrentNAVDate"] = DateTime.Parse(dr["MktPriceDate"].ToString()).ToString("dd-MMM-yy");
                    //else
                    //    drNew["CurrentNAVDate"] = "";
                    if (dtBuy.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR'").Count() > 0)
                        drNew["InvestedCost"] = decimal.Parse(dtBuy.Compute("Sum(Amount)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR'").ToString());
                    else
                        drNew["InvestedCost"] = 0;
                    if (BalanceUnits > 0)
                    {
                        drNew["Units"] = BalanceUnits.ToString();
                        // drNew["AvgPrice"] = decimal.Parse(dtBuy.Compute("Sum(AmountNew)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString()) / BalanceUnits;
                        drNew["CurrentValue"] = decimal.Parse(dr["MktPrice"].ToString()) * BalanceUnits;
                    }
                    else
                    {
                        drNew["CurrentValue"] = drNew["Units"] = 0;
                    }


                    if (dtTranxn.Select("TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                        drNew["DVP"] = decimal.Parse(dtTranxn.Compute("Sum(Amount)", "TranxnTypeCode ='DVP' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    else
                        drNew["DVP"] = 0;

                    if (dtSell.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                        drNew["RedeemedAmount"] = decimal.Parse(dtSell.Compute("Sum(Amount)", "FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    else
                        drNew["RedeemedAmount"] = 0;

                    if (dtBuy.Select("TranxnTypeCode ='DVR' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").Count() > 0)
                        drNew["DVR"] = decimal.Parse(dtBuy.Compute("Sum(Amount)", "TranxnTypeCode ='DVR' AND FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "'").ToString());
                    else
                        drNew["DVR"] = 0;

                    drNew["PL"] = decimal.Parse(drNew["RedeemedAmount"].ToString()) + decimal.Parse(drNew["CurrentValue"].ToString()) - decimal.Parse(drNew["InvestedCost"].ToString()) + decimal.Parse(drNew["DVP"].ToString());

                    if (decimal.Parse(drNew["InvestedCost"].ToString()) != 0)
                        drNew["AbsReturn"] = Math.Round((decimal.Parse(drNew["PL"].ToString()) / (decimal.Parse(drNew["InvestedCost"].ToString()))) * 100, 2);
                    if (dtTranxn.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR' ").Count() > 0)
                    {
                        DataTable dtXirr = new DataView(dtTranxn.Select("FolioNum='" + dr["FolioNum"].ToString() + "' AND ScripCode='" + dr["ScripCode"].ToString() + "' AND TranxnTypeCode <>'DVR' ", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "Units", "Amount", "MktPrice", "BuySell", "PAIC_AssetInstrumentCategoryCode" });
                        dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                        dtXirr.DefaultView.Sort = "TransactionDate ASC";
                        DataTable dt = dtXirr.DefaultView.ToTable();
                        double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                        DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                        int tempCount = 0;
                        foreach (DataRow drRow in dt.Rows)
                        {
                            if (drRow["BuySell"].ToString() != "S")
                                transactionAmount[tempCount] = -(double.Parse(drRow["Amount"].ToString()));
                            else
                                transactionAmount[tempCount] = (double.Parse(drRow["Amount"].ToString()));
                            transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                            tempCount++;
                        }

                        transactionAmount[tempCount] = double.Parse(drNew["CurrentValue"].ToString());
                        transactionDate[tempCount] = ToDate;

                        double xirr = CalculateXIRR(transactionAmount, transactionDate);
                        if (xirr < 10000)
                            drNew["TotalXIRR"] = decimal.Parse(xirr.ToString());
                        else
                            drNew["TotalXIRR"] = 0;
                    }
                    else
                        drNew["TotalXIRR"] = 0;

                    dtMFReturnsHolding.Rows.Add(drNew);
                }

                dsMFComprehensive.Tables.Add(dtMFReturnsHolding);
                foreach (DataRow dr in dtCustomerList.Rows)
                {

                    DataRow drPortfolioXirr = dtMFCustomerTotalXIRR.NewRow();
                    drPortfolioXirr["CustomerName"] = dr["CustomerName"].ToString();
                    drPortfolioXirr["PortfolioName"] = dr["PortfolioName"].ToString();
                    if (dtTranxn.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "'  AND TranxnTypeCode <>'DVR' AND Amount<>0").Count() > 0)
                    {
                        DataTable dtXirr = new DataView(dtTranxn.Select("PortfolioId='" + dr["PortfolioId"].ToString() + "' AND TranxnTypeCode <>'DVR' AND Amount<>0 ", string.Empty).CopyToDataTable()).ToTable(false, new string[] { "TransactionDate", "Units", "Amount", "MktPrice", "BuySell" });
                        dtXirr.Columns["TransactionDate"].DataType = Type.GetType("System.DateTime");
                        dtXirr.DefaultView.Sort = "TransactionDate ASC";
                        DataTable dt = dtXirr.DefaultView.ToTable();
                        double[] transactionAmount = new double[dtXirr.Rows.Count + 1];
                        DateTime[] transactionDate = new DateTime[dtXirr.Rows.Count + 1];
                        int tempCount = 0;
                        foreach (DataRow drRow in dt.Rows)
                        {
                            if (drRow["BuySell"].ToString() != "S")
                                transactionAmount[tempCount] = -(double.Parse(drRow["Amount"].ToString()));
                            else
                                transactionAmount[tempCount] = (double.Parse(drRow["Amount"].ToString()));
                            transactionDate[tempCount] = DateTime.Parse(drRow["TransactionDate"].ToString());
                            tempCount++;
                        }

                        transactionAmount[tempCount] = double.Parse(dtMFReturnsHolding.Compute("Sum(CurrentValue)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString());
                        transactionDate[tempCount] = DateTime.Now;

                        double xirr = CalculateXIRR(transactionAmount, transactionDate);
                        if (xirr < 10000)
                            drPortfolioXirr["XIRR"] = decimal.Parse(xirr.ToString());
                        else
                            drPortfolioXirr["XIRR"] = 0;
                    }
                    else
                        drPortfolioXirr["XIRR"] = 0;
                    drPortfolioXirr["AbsoluteReturn"] = Math.Round(double.Parse(dtMFReturnsHolding.Compute("Sum(PL)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()) * 100 / double.Parse(dtMFReturnsHolding.Compute("Sum(InvestedCost)", "PortfolioId='" + dr["PortfolioId"].ToString() + "'").ToString()), 2);
                    dtMFCustomerTotalXIRR.Rows.Add(drPortfolioXirr);
                }

                dsMFComprehensive.Tables.Add(dtMFCustomerTotalXIRR);
            }
            else
            {
                dsMFComprehensive.Tables.Add(dtMFReturnsHolding);
                dsMFComprehensive.Tables.Add(dtMFCustomerTotalXIRR);
            }
            return dsMFComprehensive;
        }


        public static double CalculateXIRR(System.Collections.Generic.IEnumerable<double> values, System.Collections.Generic.IEnumerable<DateTime> date)
        {

            double result = 0;
            float guess = 0.1f;

            try
            {
                if (values.Sum() < 0)
                    guess = -guess;
                result = System.Numeric.Financial.XIrr(values, date);
                //This 'if' loop is a temporary fix for the error where calculation is done for XIRR instead of average
                if (result.ToString().Contains("E") || result.ToString().Contains("e"))
                {
                    result = 0;
                }
                return Math.Round(result * 100, 2);
            }
            catch (Exception ex)
            {
                try
                {
                    result = System.Numeric.Financial.XIrr(values, date, guess * 5);
                    if (result.ToString().Contains("E") || result.ToString().Contains("e"))
                    {
                        result = 0;
                    }
                    return Math.Round(result * 100, 2); ;
                }
                catch (Exception e)
                {
                    return Math.Round(result * 100, 2); ;
                }

            }

        }



    }
}
