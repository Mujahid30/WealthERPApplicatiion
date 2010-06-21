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
    public class PortfolioReportsBo
    {
        public DataSet GetPortfolioSummary(PortfolioReportVo report, int adviserId)
        {
            PortfolioReportsDao portfolioReports = new PortfolioReportsDao();
            DataSet dsPortfolio = new DataSet();
            DataTable dtAssetPrevious = new DataTable();
            DataTable dtAssetCurrent = new DataTable();
            DataTable dtAssets = new DataTable();
            //DataTable dtLiabilities = new DataTable();
            dtAssets.Columns.Add("PortfolioId");
            dtAssets.Columns.Add("AssetType");

            dtAssets.Columns.Add("PreviousValue", Type.GetType("System.Double"));
            dtAssets.Columns.Add("CurrentValue", Type.GetType("System.Double"));
            dtAssets.Columns.Add("CustomerId", Type.GetType("System.Int64"));
            dtAssets.Columns.Add("CustomerName");
            dtAssets.Columns.Add("PortfolioName");



            DataSet dsPortfolioSummary = portfolioReports.GetPortfolioSummary(report, adviserId);

            dtAssetCurrent = dsPortfolioSummary.Tables[0];
            dtAssetPrevious = dsPortfolioSummary.Tables[1];

            //dtAssetCurrent.Merge(dtAssetPrevious);
            try
            {

                foreach (DataRow drCurrent in dtAssetCurrent.Rows)
                {
                    foreach (DataRow drPrevious in dtAssetPrevious.Rows)
                    {
                        if (drCurrent["PortfolioId"].ToString() == drPrevious["PortfolioId"].ToString() && drCurrent["AssetType"].ToString() == drPrevious["AssetType"].ToString())
                        {
                            drCurrent["PreviousValue"] = Convert.ToInt64(drPrevious["PreviousValue"]);
                            break;
                        }

                    }
                    dtAssets.ImportRow(drCurrent);
                }


                foreach (DataRow drPrevious in dtAssetPrevious.Rows)
                {
                    string expression = string.Empty;
                    expression = "PortfolioId = " + drPrevious["PortfolioId"] + " and AssetType = '" + drPrevious["AssetType"] + "'";
                    if (dtAssets.Select(expression) == null)
                    {
                        dtAssets.ImportRow(drPrevious);
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            dsPortfolio.Tables.Add(dtAssets);
            dsPortfolioSummary.Tables[2].TableName = "dtLiabilities";
            dsPortfolio.Tables.Add(dsPortfolioSummary.Tables[2].Copy());
            DataTable dtNetWorth =  CalculateNetWorth(dsPortfolio);
            dsPortfolio.Tables.Add(dtNetWorth);


            return dsPortfolio;
        }

        /// <summary>
        /// Calculating the Networth for each Customer
        /// </summary>
        /// <param name="dsPortfolio"></param>
        /// <returns></returns>
        private DataTable CalculateNetWorth(DataSet dsPortfolio)
        {
            DataTable dtNetWorth = new DataTable();


            DataTable dtPortfolioSummary = (DataTable)dsPortfolio.Tables[0];
            
            DataTable dtLiabilities =(DataTable) dsPortfolio.Tables[1];

            dtNetWorth.Columns.Add("CustomerId", Type.GetType("System.Int64"));
            dtNetWorth.Columns.Add("Type");
            dtNetWorth.Columns.Add("PreviousValue", Type.GetType("System.Double"));
            dtNetWorth.Columns.Add("CurrentValue", Type.GetType("System.Double"));

            try
            {
                double  customerId = -1;
                double totalPreviousAsset = 0;
                double totalCurrentAsset = 0;
                decimal totalLoanAmt = 0;

                DataRow drAsset = null;
                DataRow drLiab = null;
                DataRow drNet = null;
                
                //Calculate the portfolio summary total (sum of Current Asset and Previous Asset)
                foreach (DataRow drPortfolioSummary in dtPortfolioSummary.Rows)
                {
                    if (customerId == -1)
                    {
                        customerId = (Int64)drPortfolioSummary["CustomerId"];
                        totalPreviousAsset = (double)drPortfolioSummary["PreviousValue"];
                        totalCurrentAsset = (double)drPortfolioSummary["CurrentValue"];
                    }
                    else if (customerId == (Int64)drPortfolioSummary["CustomerId"])
                    {
                        customerId = (Int64)drPortfolioSummary["CustomerId"];
                        totalPreviousAsset += (double)drPortfolioSummary["PreviousValue"];
                        totalCurrentAsset += (double)drPortfolioSummary["CurrentValue"];
                    }
                    else
                    {
                        drAsset = dtNetWorth.NewRow();
                        drAsset["CustomerId"] = customerId;
                        drAsset["Type"] = "Investment";
                        drAsset["PreviousValue"] = totalPreviousAsset;
                        drAsset["CurrentValue"] = totalCurrentAsset;
                        dtNetWorth.Rows.Add(drAsset);

                        customerId = (Int64)drPortfolioSummary["CustomerId"];
                        totalPreviousAsset = (double)drPortfolioSummary["PreviousValue"];
                        totalCurrentAsset = (double)drPortfolioSummary["CurrentValue"]; 
                    }
                }
                //Storing the Asset sum of the last customer
                drAsset = dtNetWorth.NewRow();
                drAsset["CustomerId"] = customerId;
                drAsset["Type"] = "Investment";
                drAsset["PreviousValue"] = totalPreviousAsset;
                drAsset["CurrentValue"] = totalCurrentAsset;
                dtNetWorth.Rows.Add(drAsset);

                //Calculate the liabilities total (sum of Current Liability and Previous Liability)
                customerId = -1;
                foreach (DataRow drLiabilities in dtLiabilities.Rows)
                {
                    if (customerId == -1)
                    {
                        customerId = (int)drLiabilities["CustomerId"];
                        totalLoanAmt = (decimal)drLiabilities["LoanAmount"];
                    }
                    else if (customerId == (int)drLiabilities["CustomerId"])
                    {
                        customerId = (int)drLiabilities["CustomerId"];
                        totalLoanAmt += (decimal)drLiabilities["LoanAmount"];
                    }
                    else
                    {
                        drLiab = dtNetWorth.NewRow();
                        drLiab["CustomerId"] = customerId;
                        drLiab["Type"] = "Liabilities";
                        drLiab["PreviousValue"] = totalLoanAmt;
                        drLiab["CurrentValue"] = totalLoanAmt;
                        dtNetWorth.Rows.Add(drLiab);

                        customerId = (int)drLiabilities["CustomerId"];
                        totalLoanAmt += (decimal)drLiabilities["LoanAmount"];
                    }
                }

                //Storing the Liabilities sum of the last customer
                drLiab = dtNetWorth.NewRow();
                drLiab["CustomerId"] = customerId;
                drLiab["Type"] = "Liabilities";
                drLiab["PreviousValue"] = totalLoanAmt;
                drLiab["CurrentValue"] = totalLoanAmt;
                dtNetWorth.Rows.Add(drLiab);

                //Creating temporary table with liabilities and investment entries with default values(0) for customers who dont have an entry of the same
                DataRow drTemp;
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("CustomerId", Type.GetType("System.Int64"));
                dtTemp.Columns.Add("Type");
                dtTemp.Columns.Add("PreviousValue", Type.GetType("System.Double"));
                dtTemp.Columns.Add("CurrentValue", Type.GetType("System.Double"));

                foreach (DataRow drNetWorth in dtNetWorth.Rows)
                {
                    customerId = (Int64)drNetWorth["CustomerId"];
                    if (drNetWorth["Type"] == "Investment")
                    {
                        DataRow[] dr = dtNetWorth.Select("CustomerId = '" + customerId + "' AND Type = 'Liabilities'");
                        if (dr.Length == 0)
                        {
                            drTemp = dtTemp.NewRow();
                            drTemp["CustomerId"] = customerId;
                            drTemp["Type"] = "Liabilities";
                            drTemp["PreviousValue"] = 0;
                            drTemp["CurrentValue"] = 0;
                            dtTemp.Rows.Add(drTemp);
                        }
                    }
                    else if (drNetWorth["Type"] == "Liabilities")
                    {
                        DataRow[] dr = dtNetWorth.Select("CustomerId = '" + customerId + "' AND Type = 'Investment'");
                        if (dr.Length == 0)
                        {
                            drTemp = dtTemp.NewRow();
                            drTemp["CustomerId"] = customerId;
                            drTemp["Type"] = "Investment";
                            drTemp["PreviousValue"] = 0;
                            drTemp["CurrentValue"] = 0;
                            dtTemp.Rows.Add(drTemp);
                        }
                    }
                }
                //Adding the temporary table with the Networth table
                dtNetWorth.Merge(dtTemp);

                //Calculating the Net worth of each customer and add it to the Networth table
                dtTemp.Clear(); // Clearing the temp table
                foreach (DataRow drNetWorth in dtNetWorth.Rows)
                {
                    customerId = (Int64)drNetWorth["CustomerId"];
                    if (drNetWorth["Type"] == "Investment")
                    {
                        DataRow[] dr = dtNetWorth.Select("CustomerId = '" + customerId + "' AND Type = 'Liabilities'");
                        if (dr.Length > 0)
                        {
                            drTemp = dtTemp.NewRow();
                            drTemp["CustomerId"] = customerId;
                            drTemp["Type"] = "NetWorth";
                            drTemp["PreviousValue"] = (double)drNetWorth["PreviousValue"] - (double)dr[0]["PreviousValue"];
                            drTemp["CurrentValue"] = (double)drNetWorth["CurrentValue"] - (double)dr[0]["CurrentValue"];
                            dtTemp.Rows.Add(drTemp);
                        }
                    }
                }
                //Merging the temporary table with the Networth table
                dtNetWorth.Merge(dtTemp);
            }
            catch (Exception ex)
            {
                
            }
            return dtNetWorth;
        }
    }
}
