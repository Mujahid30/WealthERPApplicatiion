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

            DataTable dtNetWorth = CalculateCustomersNetWorth(dsPortfolio);
            //DataTable dtNetWorth =  CalculateNetWorth(dsPortfolio);
            dsPortfolio.Tables.Add(dtNetWorth);


            return dsPortfolio;
        }


        /// <summary>
        /// Calculating NetWorth For MultiAsset Report
        /// </summary>
        /// <param name="dsPortfolio"></param>
        /// <returns></returns>
        private DataTable CalculateCustomersNetWorth(DataSet dsPortfolio)
        {

            DataTable dtNetWorth = new DataTable();

            DataTable dtPortfolioSummary = (DataTable)dsPortfolio.Tables[0];

            DataTable dtLiabilities = (DataTable)dsPortfolio.Tables[1];

           
            DataTable dtAssets = new DataTable();
            dtAssets.Columns.Add("CustomerId", Type.GetType("System.Int64"));
            dtAssets.Columns.Add("CustomerName");
            dtAssets.Columns.Add("Type");
            dtAssets.Columns.Add("PreviousValue", Type.GetType("System.Double"));
            dtAssets.Columns.Add("CurrentValue", Type.GetType("System.Double"));
            DataRow drAsset;

            DataTable dtLiabilitie = new DataTable();
            dtLiabilitie.Columns.Add("CustomerId", Type.GetType("System.Int64"));
            dtLiabilitie.Columns.Add("CustomerName");
            dtLiabilitie.Columns.Add("Type");
            dtLiabilitie.Columns.Add("LoanAmount", Type.GetType("System.Double"));
            DataRow drLiabilitie;

            try
            {
                double CustomerId = 0;
                Double PreviousValues = 0;
                Double PresentValues = 0;
                Double TempCustomerID = 0;
                if(dtPortfolioSummary.Rows.Count>0)
                    TempCustomerID = Double.Parse(dtPortfolioSummary.Rows[0]["CustomerId"].ToString());
                string CustomerName = "";
                double LoanAmount = 0;
                foreach (DataRow drAssets in dtPortfolioSummary.Rows)
                {
                    CustomerId = (Int64)drAssets["CustomerId"];

                    if (!string.IsNullOrEmpty(drAssets["CustomerId"].ToString()))
                    {
                        if (CustomerId != TempCustomerID)
                        {
                            drAsset = dtAssets.NewRow();
                            drAsset["CustomerId"] = TempCustomerID;
                            drAsset["CustomerName"] = CustomerName;
                            drAsset["Type"] = "Asset";
                            drAsset["PreviousValue"] = PreviousValues;
                            drAsset["CurrentValue"] = PresentValues;
                            dtAssets.Rows.Add(drAsset);
                            PreviousValues = 0;
                            PresentValues = 0;

                        }

                        CustomerName = drAssets["CustomerName"].ToString();
                        PreviousValues = PreviousValues + double.Parse(drAssets["PreviousValue"].ToString());
                        PresentValues = PresentValues +  double.Parse(drAssets["CurrentValue"].ToString());
                        TempCustomerID = CustomerId;
                        
                    }

                    
                }
           
                   drAsset = dtAssets.NewRow();
                   drAsset["CustomerId"] = TempCustomerID;
                   drAsset["CustomerName"] = CustomerName;
                   drAsset["Type"] = "Asset";
                   drAsset["PreviousValue"] = PreviousValues;
                   drAsset["CurrentValue"] = PresentValues;
                   dtAssets.Rows.Add(drAsset);
                   PreviousValues = 0;
                   PresentValues = 0;
             


                /* ******************************************************** */
                    if(dtLiabilities.Rows.Count>0)
                        TempCustomerID = Double.Parse(dtLiabilities.Rows[0]["CustomerId"].ToString());
                   foreach (DataRow drliabilities in dtLiabilities.Rows)
                   {
                        CustomerId = Int32.Parse(drliabilities["CustomerId"].ToString());

                     if (!string.IsNullOrEmpty(drliabilities["CustomerId"].ToString()))
                        {
                         if (CustomerId != TempCustomerID)
                            {
                                drLiabilitie = dtLiabilitie.NewRow();
                                drLiabilitie["CustomerId"] = TempCustomerID;
                                drLiabilitie["CustomerName"] = CustomerName;
                                drLiabilitie["Type"] = "Liabilities";
                                drLiabilitie["LoanAmount"] = LoanAmount;

                                dtLiabilitie.Rows.Add(drLiabilitie);
                                LoanAmount = 0;
                                

                        }

                        CustomerName = drliabilities["CustomerName"].ToString();
                        LoanAmount = LoanAmount + double.Parse(drliabilities["LoanAmount"].ToString());
                        TempCustomerID = CustomerId;
                        
                    }

                    
                }

                   drLiabilitie = dtLiabilitie.NewRow();
                   drLiabilitie["CustomerId"] = TempCustomerID;
                   drLiabilitie["CustomerName"] = CustomerName;
                   drLiabilitie["Type"] = "Liabilities";
                   drLiabilitie["LoanAmount"] = LoanAmount;

                   dtLiabilitie.Rows.Add(drLiabilitie);
                   LoanAmount = 0;

                 /******************** NET WORTH CALCULATION *******************/

                  

                   dtNetWorth.Columns.Add("CustomerId", Type.GetType("System.Int64"));
                   dtNetWorth.Columns.Add("CustomerName");
                   dtNetWorth.Columns.Add("Type");
                   //dtNetWorth.Columns.Add("Asset", Type.GetType("System.Double"));
                   dtNetWorth.Columns.Add("PreviousValue", Type.GetType("System.Double"));
                   dtNetWorth.Columns.Add("CurrentValue", Type.GetType("System.Double"));
                   DataRow drNetWorth;

                   double CurrentNetWorth = 0;
                   double PreviousNetWorth = 0;
                   TempCustomerID = Double.Parse(dtAssets.Rows[0]["CustomerId"].ToString());
                   foreach (DataRow DrAsset in dtAssets.Rows)
                   {
                       CustomerId = Int32.Parse(DrAsset["CustomerId"].ToString());
                       if (dtLiabilitie.Rows.Count > 0)
                       {
                           foreach (DataRow DrLiabilities in dtLiabilitie.Rows)
                           {
                               double LcustomerId = Int32.Parse(DrLiabilities["CustomerId"].ToString());
                               if (CustomerId == LcustomerId)
                               {
                                   PreviousNetWorth = Double.Parse(DrAsset["PreviousValue"].ToString()) - Double.Parse(dtLiabilitie.Rows[0]["LoanAmount"].ToString());
                                   CurrentNetWorth = Double.Parse(DrAsset["CurrentValue"].ToString()) - Double.Parse(dtLiabilitie.Rows[0]["LoanAmount"].ToString());
                                   drNetWorth = dtNetWorth.NewRow();
                                   drNetWorth["CustomerId"] = CustomerId;
                                   drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                                   drNetWorth["Type"] = "Asset";
                                   drNetWorth["PreviousValue"] = DrAsset["PreviousValue"];
                                   drNetWorth["CurrentValue"] = DrAsset["CurrentValue"];
                                   dtNetWorth.Rows.Add(drNetWorth);

                                   drNetWorth = dtNetWorth.NewRow();
                                   drNetWorth["CustomerId"] = CustomerId;
                                   drNetWorth["CustomerName"] = DrLiabilities["CustomerName"].ToString().Trim();
                                   drNetWorth["Type"] = "Liabilities";
                                   drNetWorth["PreviousValue"] = DrLiabilities["LoanAmount"];
                                   drNetWorth["CurrentValue"] = DrLiabilities["LoanAmount"];
                                   dtNetWorth.Rows.Add(drNetWorth);

                                   drNetWorth = dtNetWorth.NewRow();
                                   drNetWorth["CustomerId"] = CustomerId;
                                   drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                                   drNetWorth["Type"] = "NetWorth";
                                   drNetWorth["PreviousValue"] = PreviousNetWorth;
                                   drNetWorth["CurrentValue"] = CurrentNetWorth;
                                   dtNetWorth.Rows.Add(drNetWorth);

                               }
                               else
                               {
                                   drNetWorth = dtNetWorth.NewRow();
                                   drNetWorth["CustomerId"] = CustomerId;
                                   drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                                   drNetWorth["Type"] = "Asset";
                                   drNetWorth["PreviousValue"] = DrAsset["PreviousValue"];
                                   drNetWorth["CurrentValue"] = DrAsset["CurrentValue"];
                                   dtNetWorth.Rows.Add(drNetWorth);

                                   drNetWorth = dtNetWorth.NewRow();
                                   drNetWorth["CustomerId"] = CustomerId;
                                   drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                                   drNetWorth["Type"] = "Liabilities";
                                   drNetWorth["PreviousValue"] = 0;
                                   drNetWorth["CurrentValue"] = 0;
                                   dtNetWorth.Rows.Add(drNetWorth);

                                   drNetWorth = dtNetWorth.NewRow();
                                   drNetWorth["CustomerId"] = CustomerId;
                                   drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                                   drNetWorth["Type"] = "NetWorth";
                                   drNetWorth["PreviousValue"] = DrAsset["PreviousValue"];
                                   drNetWorth["CurrentValue"] = DrAsset["CurrentValue"];
                                   dtNetWorth.Rows.Add(drNetWorth);


                               }

                           }



                       }
                       else
                       {
                           drNetWorth = dtNetWorth.NewRow();
                           drNetWorth["CustomerId"] = CustomerId;
                           drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                           drNetWorth["AssetType"] = "Asset";
                           drNetWorth["PreviousValue"] = DrAsset["PreviousValue"];
                           drNetWorth["CurrentValue"] = DrAsset["CurrentValue"];
                           dtNetWorth.Rows.Add(drNetWorth);

                           drNetWorth = dtNetWorth.NewRow();
                           drNetWorth["CustomerId"] = CustomerId;
                           drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                           drNetWorth["AssetType"] = "Liabilities";
                           drNetWorth["PreviousValue"] = 0;
                           drNetWorth["CurrentValue"] = 0;
                           dtNetWorth.Rows.Add(drNetWorth);

                           drNetWorth = dtNetWorth.NewRow();
                           drNetWorth["CustomerId"] = CustomerId;
                           drNetWorth["CustomerName"] = DrAsset["CustomerName"].ToString().Trim();
                           drNetWorth["AssetType"] = "NetWorth";
                           drNetWorth["PreviousValue"] = DrAsset["PreviousValue"];
                           drNetWorth["CurrentValue"] = DrAsset["CurrentValue"];
                           dtNetWorth.Rows.Add(drNetWorth);
 
                       }



 
                   }



            }




            catch (Exception ex)
            {
                throw (ex);
            }


            return dtNetWorth;

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
            //dtNetWorth.Columns.Add("CustomerName", Type.GetType("System.String"));

            try
            {
                double  customerId = -1;
                string CustomerName = "";
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
                        //CustomerName = drPortfolioSummary["CustomerName"].ToString();
                        totalPreviousAsset = (double)drPortfolioSummary["PreviousValue"];
                        totalCurrentAsset = (double)drPortfolioSummary["CurrentValue"];
                    }
                    else if (customerId == (Int64)drPortfolioSummary["CustomerId"])
                    {
                        customerId = (Int64)drPortfolioSummary["CustomerId"];
                        //CustomerName = drPortfolioSummary["CustomerName"].ToString();
                        totalPreviousAsset += (double)drPortfolioSummary["PreviousValue"];
                        totalCurrentAsset += (double)drPortfolioSummary["CurrentValue"];
                    }
                    else
                    {
                        drAsset = dtNetWorth.NewRow();
                        drAsset["CustomerId"] = customerId;
                        //CustomerName = drPortfolioSummary["CustomerName"].ToString();
                        drAsset["Type"] = "Investment";
                        drAsset["PreviousValue"] = totalPreviousAsset;
                        drAsset["CurrentValue"] = totalCurrentAsset;
                        //drAsset["CustomerName"] = CustomerName;
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
                //drAsset["CustomerName"] = CustomerName;
                dtNetWorth.Rows.Add(drAsset);

                //Calculate the liabilities total (sum of Current Liability and Previous Liability)
                customerId = -1;
                foreach (DataRow drLiabilities in dtLiabilities.Rows)
                {
                    if (customerId == -1)
                    {
                        customerId = (int)drLiabilities["CustomerId"];
                        //CustomerName = drLiabilities["CustomerName"].ToString();
                        totalLoanAmt = (decimal)drLiabilities["LoanAmount"];
                    }
                    else if (customerId == (int)drLiabilities["CustomerId"])
                    {
                        customerId = (int)drLiabilities["CustomerId"];
                        //CustomerName = drLiabilities["CustomerName"].ToString();
                        totalLoanAmt += (decimal)drLiabilities["LoanAmount"];
                    }
                    else
                    {
                        drLiab = dtNetWorth.NewRow();
                        drLiab["CustomerId"] = customerId;
                        drLiab["Type"] = "Liabilities";
                        drLiab["PreviousValue"] = totalLoanAmt;
                        drLiab["CurrentValue"] = totalLoanAmt;
                        //drLiab["CustomerName"] = CustomerName;
                        dtNetWorth.Rows.Add(drLiab);

                        customerId = (int)drLiabilities["CustomerId"];
                        //CustomerName = drLiabilities["CustomerName"].ToString();
                        totalLoanAmt += (decimal)drLiabilities["LoanAmount"];
                    }
                }

                //Storing the Liabilities sum of the last customer
                drLiab = dtNetWorth.NewRow();
                drLiab["CustomerId"] = customerId;
                drLiab["Type"] = "Liabilities";
                drLiab["PreviousValue"] = totalLoanAmt;
                drLiab["CurrentValue"] = totalLoanAmt;
                //drLiab["CustomerName"] = CustomerName;

                dtNetWorth.Rows.Add(drLiab);

                //Creating temporary table with liabilities and investment entries with default values(0) for customers who dont have an entry of the same
                
                DataTable dtTemp = new DataTable();
                DataRow drTemp;
                dtTemp.Columns.Add("CustomerId", Type.GetType("System.Int64"));
                dtTemp.Columns.Add("Type");
                dtTemp.Columns.Add("PreviousValue", Type.GetType("System.Double"));
                dtTemp.Columns.Add("CurrentValue", Type.GetType("System.Double"));
                //dtTemp.Columns.Add("CustomerName", Type.GetType("System.String"));

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
                            //drTemp["CustomerName"] = CustomerName;
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
                            //drTemp["CustomerName"] = CustomerName;
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
                            //drTemp["CustomerName"] = CustomerName;
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

        public DataSet GetCustomerAssetAllocationDetails(PortfolioReportVo report, int adviserId, string reportType)
        {

            PortfolioReportsDao portfolioReport = new PortfolioReportsDao();
            DataSet dsAssetAllocation = null;
            try
            {
                dsAssetAllocation = portfolioReport.GetCustomerAssetAllocationDetails(report, adviserId, reportType);
            }
            catch (Exception ex)
            {

            }
            return dsAssetAllocation;
        }
    }
}
