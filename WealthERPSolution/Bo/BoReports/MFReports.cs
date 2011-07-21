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

        public DataTable GetReturnSummaryReport(MFReportVo reports, int adviserId)
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
                if (dsCustomerMFReturns != null)
                {
                    if (dsCustomerMFReturns.Tables[4].Rows.Count != 0)
                    {
                       DataRow drTotal=dsCustomerMFReturns.Tables[4].Rows[0];
                       if (!string.IsNullOrEmpty(drTotal["Amount"].ToString()))
                       totalValue = double.Parse(drTotal["Amount"].ToString());
                    }
                    foreach (DataRow drHoldings in dsCustomerMFReturns.Tables[2].Rows)
                    {
                        holdingstotal = holdingstotal + double.Parse(drHoldings["Amount"].ToString());
                    }
                    foreach (DataRow drSector in dsCustomerMFReturns.Tables[5].Rows)
                    {
                        sectorTotal = sectorTotal + double.Parse(drSector["Amount"].ToString());
                    }
                    otherSectorTotal = totalValue - sectorTotal;
                    if(dsCustomerMFReturns.Tables[3].Rows.Count!=0 && dsCustomerMFReturns.Tables[3].Rows[0]["Amount"].ToString()!="")
                        otherholdingsTotal = totalValue - double.Parse(dsCustomerMFReturns.Tables[3].Rows[0]["Amount"].ToString()) - holdingstotal;
                    if (dsCustomerMFReturns.Tables[2].Rows.Count != 0)
                    {
                        DataRow dr = dsCustomerMFReturns.Tables[2].NewRow();
                        dr["Instrument"] = "Other Holdings";
                        dr["InsType"] = "Other";
                        dr["Amount"] = otherholdingsTotal.ToString();
                        dsCustomerMFReturns.Tables[2].Rows.Add(dr);
                        dr = dsCustomerMFReturns.Tables[2].NewRow();
                        dr["Instrument"] = dsCustomerMFReturns.Tables[3].Rows[0]["Instrument"].ToString();
                        dr["InsType"] = dsCustomerMFReturns.Tables[3].Rows[0]["InsType"].ToString();
                        dr["Amount"] = double.Parse(dsCustomerMFReturns.Tables[3].Rows[0]["Amount"].ToString());
                        dsCustomerMFReturns.Tables[2].Rows.Add(dr);
                       // dr = dsCustomerMFReturns.Tables[2].NewRow();
                        //dr["Instrument"] = dsCustomerMFReturns.Tables[4].Rows[0]["Instrument"].ToString();
                        //dr["InsType"] = "";
                        //dr["Amount"] = dsCustomerMFReturns.Tables[4].Rows[0]["Amount"].ToString();
                        //dsCustomerMFReturns.Tables[2].Rows.Add(dr);
                    }
                    dsCustomerMFReturns.Tables[2].Columns.Add("Percentage");
                    if(totalValue!=0)
                    {
                        for (int i = 0; i < dsCustomerMFReturns.Tables[2].Rows.Count; i++)
                        {
                            dsCustomerMFReturns.Tables[2].Rows[i]["Percentage"] = Math.Round(double.Parse((((double.Parse(dsCustomerMFReturns.Tables[2].Rows[i]["Amount"].ToString())) / totalValue) * 100).ToString()),2);
                        }
                    }
                    if (dsCustomerMFReturns.Tables[5].Rows.Count != 0)
                    {
                        DataRow drSectorHoldings = dsCustomerMFReturns.Tables[5].NewRow();
                        drSectorHoldings["SectorCode"] = "0";
                        drSectorHoldings["Sector"] = "Others";
                        drSectorHoldings["Amount"] = otherSectorTotal.ToString();
                        dsCustomerMFReturns.Tables[5].Rows.Add(drSectorHoldings);
                    }
                    dsCustomerMFReturns.Tables[5].Columns.Add("Percentage");
                    if (totalValue != 0)
                    {
                        for (int i = 0; i < dsCustomerMFReturns.Tables[5].Rows.Count; i++)
                        {
                            dsCustomerMFReturns.Tables[5].Rows[i]["Percentage"] = Math.Round(double.Parse((((double.Parse(dsCustomerMFReturns.Tables[5].Rows[i]["Amount"].ToString())) / totalValue) * 100).ToString()),2);
                        }
                    }
                }

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
    }
}
