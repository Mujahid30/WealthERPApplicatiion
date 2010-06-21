using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;
using System.IO.Compression;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode;
using System.Threading;
using BoMarketDataPull.App_Code;
using DaoMarketDataPull;
using BoWerpAdmin;
using VoWerpAdmin;
using System.Collections;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoMarketDataPull
{
    public class GetDownloadData
    {
        #region Download BSE Equity data
        public DataTable downloadBSEEquityData(int UserId, DateTime Startdate, DateTime EndDate)
        {

            string pathval = System.AppDomain.CurrentDomain.BaseDirectory;
            if (pathval.EndsWith("\\bin\\Debug\\"))
            {
                pathval = pathval.Replace("\\bin\\Debug", "");

            }

            string pathNse = Path.Combine(pathval, @"MarketDataDownloadFiles\BseEquities.zip");
            string pathFilter = Path.Combine(pathval, @"MarketDataDownloadFiles\BseEquitiesFiltered.txt");
            //string pathXml = Path.Combine(pathval, @"MarketDataDownloadFiles\BseEquities.xml");
            string pathextract = Path.Combine(pathval, @"MarketDataDownloadFiles");
            string pathEquityFile = Path.Combine(pathval, @"MarketDataDownloadFiles\BseEquities.CSV");
            string Downloadtype = "BSEEquities";

            MarketDataPullDao marketdatapullDao = new MarketDataPullDao();
            DataTable dtResult = new DataTable("BseDownloadResult");
            dtResult.Columns.Add("Date", typeof(string));
            dtResult.Columns.Add("Result", typeof(string));
            dtResult.Columns.Add("NumofRecords", typeof(int));
            DataRow dr;
            DateTime date = Startdate;



            do
            {
                bool result = true;
                string ProcessStatus_BSEEqt = "";
                int num_of_rec_BSEEqt = -1;
                //date = CEodFetch.ValidateDay(date);

                string fetchData = CEodFetch.GetDownloads(date);
                string[] splitstring = fetchData.Split(new Char[] { '_' });

                ProductPriceDownloadLogBo productPriceDownloadLogBo = new ProductPriceDownloadLogBo();
                AdminDownloadProcessLogVo processLogVo = new AdminDownloadProcessLogVo();
                processLogVo.CreatedBy = UserId;
                processLogVo.AssetClass = "Equity";
                processLogVo.SourceName = "BSE";
                processLogVo.StartTime = DateTime.Now;
                processLogVo.EndTime = DateTime.Now;
                processLogVo.ModifiedOn = DateTime.Now;
                processLogVo.ModifiedBy = UserId;
                processLogVo.ForDate = date;
                //Create a process for the download
                processLogVo.ProcessID = productPriceDownloadLogBo.CreateProcessLog(processLogVo);

                try
                {

                    //// Create a new WebClient instance.
                    WebClient myWebClient = new WebClient();

                    // Concatenate the domain with the Web resource filename.
                    string myStringWebResource = fetchData + pathNse;

                    myWebClient.DownloadFile(fetchData, pathNse);
                    processLogVo.IsConnectionToSiteEstablished = 1;


                    //Zip Extracting Code
                    FastZip fz = new FastZip();
                    fz.ExtractZip(pathNse, pathextract, "");
                    processLogVo.IsFileDownloaded = 1;
                    result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                    //filename convert

                    string pathBSE = Path.Combine(pathval, @"MarketDataDownloadFiles\") + CEodFetch.GetDownloadFile(date, Downloadtype);

                    FileInfo renamefile = new FileInfo(pathBSE);
                    if (renamefile.Exists)
                    {
                        File.Delete(pathEquityFile);
                        File.Move(pathBSE, pathEquityFile);
                    }
                    if (!String.IsNullOrEmpty(fetchData))
                    {

                        if (result)
                        {
                            Console.WriteLine("Writing into a text file........");
                            //Create a stream reader for the text file
                            StreamReader sr = new StreamReader(pathEquityFile);
                            StringBuilder strNewData = new StringBuilder();
                            String line;

                            //Omit lines which are there in the text files 
                            //for information purpose
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Contains(","))
                                {
                                    strNewData.AppendLine(line);
                                }

                            }
                            sr.Close();

                            //Create Filtered Data text file
                            result = CEodFetch.WriteToFile(pathFilter, strNewData.ToString());

                            if (result)
                            {
                                //Create Data Set from the filtered data
                                DataSet ds = TextToDataSet.GetBSEEquitiesDataSet(pathFilter, date);


                                if (null != ds)
                                {
                                    processLogVo.NoOfRecordsDownloaded = ds.Tables[0].Rows.Count;
                                    //Create an XML file from the Created Data set
                                    ds.WriteXml(Path.Combine(pathval, @"MarketDataDownloadFiles\" + processLogVo.ProcessID + ".xml"), XmlWriteMode.WriteSchema); ;
                                    processLogVo.IsConversiontoXMLComplete = 1;
                                    processLogVo.XMLFileName = processLogVo.ProcessID + ".xml";
                                    result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);

                                    //insert into bse temp table

                                    result = marketdatapullDao.InsertDataToTemp(ds, "BSE");


                                    if (result)
                                    {
                                        num_of_rec_BSEEqt = marketdatapullDao.InsertDataintoMarketPriceTable(date, "BSE");
                                        if (num_of_rec_BSEEqt == 0)
                                        {
                                            ProcessStatus_BSEEqt = "Latest data already exists in the database for the date : " + date + "";
                                            processLogVo.NoOfRecordsInserted = num_of_rec_BSEEqt;
                                            processLogVo.Description = ProcessStatus_BSEEqt;
                                        }
                                        else
                                        {
                                            ProcessStatus_BSEEqt = "Data Updated successfully into the table";
                                            processLogVo.NoOfRecordsInserted = num_of_rec_BSEEqt;
                                            processLogVo.IsInsertiontoDBdone = 1;
                                            processLogVo.Description = ProcessStatus_BSEEqt;
                                        }

                                    }



                                }
                                else
                                {

                                    ProcessStatus_BSEEqt = "Could not create data set from the filtered data!";
                                    num_of_rec_BSEEqt = 0;
                                    processLogVo.NoOfRecordsDownloaded = num_of_rec_BSEEqt;
                                    processLogVo.Description = ProcessStatus_BSEEqt;
                                }
                            }
                            else
                            {

                                ProcessStatus_BSEEqt = "Could not write the filtered data to the disk!";
                                num_of_rec_BSEEqt = 0;
                                processLogVo.NoOfRecordsDownloaded = num_of_rec_BSEEqt;
                                processLogVo.Description = ProcessStatus_BSEEqt;
                            }
                        }

                        else
                        {

                            ProcessStatus_BSEEqt = "Could not write the downloaded data to the disk!";
                            num_of_rec_BSEEqt = 0;
                            processLogVo.NoOfRecordsDownloaded = num_of_rec_BSEEqt;
                            processLogVo.Description = ProcessStatus_BSEEqt;
                        }
                    }
                }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (WebException Ex)
                {
                    ProcessStatus_BSEEqt = "Data not available on the site!";
                    num_of_rec_BSEEqt = 0;
                    processLogVo.NoOfRecordsDownloaded = num_of_rec_BSEEqt;
                    processLogVo.Description = ProcessStatus_BSEEqt;
                }
                catch (Exception Ex)
                {
                    result = false;
                    ProcessStatus_BSEEqt = "Stacktrace --- :" + Ex.StackTrace + "---Inner Ex:" + Ex.InnerException;

                    processLogVo.Description = ProcessStatus_BSEEqt;

                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();

                    FunctionInfo.Add("downloadBSEEquityData", "MarketDataPullBo.cs:GetProcessLog()");

                    object[] objects = new object[3];
                    objects[0] = UserId;
                    objects[1] = Startdate;
                    objects[2] = EndDate;

                    FunctionInfo = exBase.AddObject(FunctionInfo, null);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);


                }
                
                processLogVo.EndTime = DateTime.Now;
                result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                dr = dtResult.NewRow();
                dr["Date"] = date.ToShortDateString();
                dr["Result"] = ProcessStatus_BSEEqt;
                dr["NumofRecords"] = num_of_rec_BSEEqt;
                dtResult.Rows.Add(dr);
                date = date.AddDays(1);
            } while (date <= EndDate);

            DataTable dtresulttable = dtResult;
            return dtResult;
        }
        #endregion

        #region Download NSE Corporate Actions Equity data
        //public DataTable downloadNSECorpActsData(DateTime Startdate, DateTime EndDate)
        //{


        //    string pathval = System.AppDomain.CurrentDomain.BaseDirectory;
        //    if (pathval.EndsWith("\\bin\\Debug\\"))
        //    {
        //        pathval = pathval.Replace("\\bin\\Debug", "");

        //    }

        //    string pathNse = Path.Combine(pathval, @"App_Data\BseEquities.zip");
        //    string pathFilter = Path.Combine(pathval, @"App_Data\NSECorpActsFiltered.txt");
        //    string pathXml = Path.Combine(pathval, @"App_Data\NSECorpActs.xml");
        //    string NSECorpActsFile = Path.Combine(pathval, @"App_Data\NSECorpActs.CSV");
        //    string Downloadtype = "NSECorpActs";

        //    DataTable dtResult = new DataTable("NSECorpActsDownloadResult");
        //    dtResult.Columns.Add("Date", typeof(string));
        //    dtResult.Columns.Add("Result", typeof(string));
        //    dtResult.Columns.Add("NumofRecords", typeof(int));
        //    DataRow dr;

        //    DateTime date = Startdate;
        //    do
        //    {
        //        string ProcessStatus_NSECrpActs = "";
        //        int num_of_rec_NSECrpActs = -1;
        //        bool result = true;
        //        string fetchData = CEodFetch.GetDownloadsNSECrpacts(Startdate, EndDate);
        //        string[] splitstring = fetchData.Split(new Char[] { '_' });

        //        string pathNSECorpActs = Path.Combine(pathval, @"App_Data\") + CEodFetch.GetDownloadFile(date, Downloadtype);

        //        try
        //        {
        //            bool filedownloadsatus = false;
        //            do
        //            {

        //                try
        //                {
        //                    WebClient myWebClient = new WebClient();
        //                    // Concatenate the domain with the Web resource filename.
        //                    string myStringWebResource = fetchData + pathNse;
        //                    Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", pathNSECorpActs, fetchData);
        //                    // Download the Web resource and save it into the current filesystem folder.
        //                    myWebClient.DownloadFile(fetchData, pathNSECorpActs);
        //                    filedownloadsatus = true;
        //                    Console.WriteLine("Could not find file... Dowloading again in 2 seconds");
        //                }
        //                catch (WebException exdownload)
        //                {
        //                    ProcessStatus_NSECrpActs = "Downloading file error: " + exdownload.Message;
        //                    num_of_rec_NSECrpActs = 0;
        //                    filedownloadsatus = false;
        //                    Thread.Sleep(2000);

        //                }
        //            } while (filedownloadsatus == false);

        //            FileInfo renamefile = new FileInfo(pathNSECorpActs);
        //            if (renamefile.Exists)
        //            {
        //                File.Delete(NSECorpActsFile);
        //                File.Move(pathNSECorpActs, NSECorpActsFile);
        //            }
        //            if (!String.IsNullOrEmpty(fetchData))
        //            {

        //                if (result)
        //                {
        //                    Console.WriteLine("Writing into a text file........");
        //                    //Create a stream reader for the text file
        //                    StreamReader sr = new StreamReader(NSECorpActsFile);
        //                    StringBuilder strNewData = new StringBuilder();
        //                    String line;

        //                    //Omit lines which are there in the text files 
        //                    //for information purpose
        //                    while ((line = sr.ReadLine()) != null)
        //                    {
        //                        if (line.Contains(","))
        //                        {
        //                            strNewData.AppendLine(line);
        //                        }

        //                    }
        //                    sr.Close();
        //                    if (num_of_rec_NSECrpActs == 0)
        //                    {
        //                        result = false;
        //                        ProcessStatus_NSECrpActs = "No records for the date";
        //                        num_of_rec_NSECrpActs = 0;


        //                    }
        //                    if (result)
        //                    {
        //                        result = CEodFetch.WriteToFile(pathFilter, strNewData.ToString());
        //                        DataSet ds = TextToDataSet.GetNSECrpActsDataSet(pathFilter, date);
        //                        if (null != ds)
        //                        {
        //                            //Create an XML file from the Created Data set
        //                            ds.WriteXml(pathXml);

        //                            Console.WriteLine("Writing into database......");
        //                            //Store the data set into NSE EOD Market Data Table
        //                            string errorMessage;
        //                            result = DBHelper.UpdateTable(ds.Tables["NSE_Corporate_Actions"], out errorMessage);

        //                            if (result)
        //                            {

        //                                ProcessStatus_NSECrpActs = "Data Successfully inserted into NSE_Corporate_Actions Table";
        //                                num_of_rec_NSECrpActs = ds.Tables["NSE_Corporate_Actions"].Rows.Count;
        //                            }
        //                            else
        //                            {

        //                                ProcessStatus_NSECrpActs = "Failed to insert the data into NSE_Corporate_Actions: " + errorMessage;
        //                                num_of_rec_NSECrpActs = 0;

        //                            }
        //                        }
        //                    }
        //                    else
        //                    {

        //                        ProcessStatus_NSECrpActs = "No data for the current date!";
        //                        num_of_rec_NSECrpActs = 0;
        //                    }
        //                }

        //                else
        //                {

        //                    ProcessStatus_NSECrpActs = "Could not write the downloaded data to the disk!";
        //                    num_of_rec_NSECrpActs = 0;
        //                }
        //            }

        //        }


        //        catch (Exception ex)
        //        {
        //            ProcessStatus_NSECrpActs = "Error Ocured: " + ex.Message;
        //            num_of_rec_NSECrpActs = 0;
        //        }

        //        dr = dtResult.NewRow();
        //        dr["Date"] = date.ToShortDateString();
        //        dr["Result"] = ProcessStatus_NSECrpActs;
        //        dr["NumofRecords"] = num_of_rec_NSECrpActs;
        //        dtResult.Rows.Add(dr);
        //        date = date.AddDays(1);
        //    } while (date <= EndDate);

        //    DataTable dtresulttable = dtResult;
        //    return dtResult;
        //} 
        #endregion

        #region Download AMFI Historical data
        public DataTable downloadAmfiHistoricalData(int UserId, DateTime Startdate, DateTime EndDate)
        {


            string pathval = System.AppDomain.CurrentDomain.BaseDirectory;
            //if (pathval.EndsWith("\\bin\\Debug\\"))
            //{
            //    pathval = pathval.Replace("\\bin\\Debug", "");

            //}
            MarketDataPullDao marketdatapullDao = new MarketDataPullDao();

            string pathAmfi = Path.Combine(pathval, @"MarketDataDownloadFiles\AMFIData.txt");
            string pathFilter = Path.Combine(pathval, @"MarketDataDownloadFiles\AMFIDataFiltered.txt");
            string pathXml = Path.Combine(pathval, @"MarketDataDownloadFiles\AMFIData.xml");
            DataTable dtResult = new DataTable("AMFIDownloadResult");
            dtResult.Columns.Add("Date", typeof(string));
            dtResult.Columns.Add("Result", typeof(string));
            dtResult.Columns.Add("NumofRecords", typeof(int));
            DataRow dr;

            
            DateTime date = Startdate;
            do
            {
                string ProcessStatus_AmfiData = "";
                int num_of_rec_AmfiData = -1;
                bool result = true;
                ProductPriceDownloadLogBo productPriceDownloadLogBo = new ProductPriceDownloadLogBo();
                AdminDownloadProcessLogVo processLogVo = new AdminDownloadProcessLogVo();
                processLogVo.CreatedBy = UserId;
                processLogVo.AssetClass = "MF";
                processLogVo.SourceName = "AMFI";
                processLogVo.StartTime = DateTime.Now;
                processLogVo.EndTime = DateTime.Now;
                processLogVo.ModifiedOn = DateTime.Now;
                processLogVo.ModifiedBy = UserId;

                //Create a process for the download
                processLogVo.ProcessID = productPriceDownloadLogBo.CreateProcessLog(processLogVo);
                try
                {
                    string datevalue = date.ToString("dd-MMM-yyyy");
                    processLogVo.ForDate = date;
                    string orginalData = CEodFetch.GetAmfiHistoricalData(datevalue);
                    if (orginalData != null)
                    {
                        result = CEodFetch.WriteToFile(pathAmfi, orginalData);

                        if (result)
                        {
                            processLogVo.IsConnectionToSiteEstablished = 1;
                            processLogVo.IsFileDownloaded = 1;
                            result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);

                            StreamReader sr = new StreamReader(pathAmfi);
                            StringBuilder strNewData = new StringBuilder();
                            String line;

                            //Omit lines which are there in the text files for information purpose
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Contains(";"))
                                {
                                    strNewData.AppendLine(line);
                                }
                                num_of_rec_AmfiData++;
                            }
                            sr.Close();

                            //Create Filtered Data text file
                            result = CEodFetch.WriteToFile(pathFilter, strNewData.ToString());
                            
                            if (result)
                            {
                                DataSet ds = TextToDataSet.GetAmfiDataSet(pathFilter, date);

                                if (null != ds)
                                {
                                    processLogVo.NoOfRecordsDownloaded = ds.Tables[0].Rows.Count;

                                    ds.WriteXml(Path.Combine(pathval, @"MarketDataDownloadFiles\" + processLogVo.ProcessID + ".xml"), XmlWriteMode.WriteSchema);
                                    processLogVo.IsConversiontoXMLComplete = 1;
                                    processLogVo.XMLFileName = processLogVo.ProcessID + ".xml";
                                    result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                                    // result = DBHelper.UpdateTable(ds.Tables["AMFI"], out Message);
                                    result = marketdatapullDao.InsertDataToTemp(ds, "AMFI");

                                    if (result)
                                    {
                                        num_of_rec_AmfiData = marketdatapullDao.InsertDataintoMarketPriceTable(date, "AMFI");
                                        if (num_of_rec_AmfiData == 0)
                                        {
                                            ProcessStatus_AmfiData = "Latest data already exists in the database for the date : " + date + "";
                                            processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                            processLogVo.Description = ProcessStatus_AmfiData;
                                        }
                                        else
                                        {
                                            ProcessStatus_AmfiData = "Data Updated successfully into the table";
                                            processLogVo.IsInsertiontoDBdone = 1;
                                            processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                            processLogVo.Description = ProcessStatus_AmfiData;
                                        }

                                    }
                                    else
                                    {
                                        ProcessStatus_AmfiData = "Failed to insert data into Amfi table";
                                        num_of_rec_AmfiData = 0;
                                        processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                        processLogVo.Description = ProcessStatus_AmfiData;
                                    }
                                }
                                else
                                {
                                    ProcessStatus_AmfiData = "Could not create data set from the filtered data!";
                                    num_of_rec_AmfiData = 0;
                                    processLogVo.NoOfRecordsDownloaded = num_of_rec_AmfiData;
                                    processLogVo.Description = ProcessStatus_AmfiData;
                                }


                            }
                            else
                            {
                                ProcessStatus_AmfiData = "Failed to insert data into Amfi table";
                                num_of_rec_AmfiData = 0;
                                processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                processLogVo.Description = ProcessStatus_AmfiData;
                            }

                        }
                        else
                        {
                            ProcessStatus_AmfiData = "Could not write the downloaded data to the disk!";
                            num_of_rec_AmfiData = 0;
                            processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                            processLogVo.Description = ProcessStatus_AmfiData;
                        }
                    }
                    else
                    {
                        ProcessStatus_AmfiData = "Data not available on the site!";
                        num_of_rec_AmfiData = 0;
                        processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                        processLogVo.Description = ProcessStatus_AmfiData;
                    }
                }
                catch (Exception ex)
                {
                    ProcessStatus_AmfiData = "Exception in Amfi Data Fectch, " + ex.Message;
                    num_of_rec_AmfiData = 0;
                    processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                    processLogVo.Description = ProcessStatus_AmfiData;
                }
                processLogVo.EndTime = DateTime.Now;
                result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                dr = dtResult.NewRow();
                dr["Date"] = date.ToShortDateString();
                dr["Result"] = ProcessStatus_AmfiData;
                dr["NumofRecords"] = num_of_rec_AmfiData;
                dtResult.Rows.Add(dr);
                date = date.AddDays(1);
            } while (date <= EndDate);
            DataTable dtresulttable = dtResult;
            return dtResult;
        }
        #endregion

        #region Download AMFI Current Data
        public DataTable downloadAmfiCurrentData(int UserId)
        {


            string pathval = System.AppDomain.CurrentDomain.BaseDirectory;
            //if (pathval.EndsWith("\\bin\\Debug\\"))
            //{
            //    pathval = pathval.Replace("\\bin\\Debug", "");

            //}
            MarketDataPullDao marketdatapullDao = new MarketDataPullDao();

            string pathAmfi = Path.Combine(pathval, @"MarketDataDownloadFiles\AMFIData.txt");
            string pathFilter = Path.Combine(pathval, @"MarketDataDownloadFiles\AMFIDataFiltered.txt");
            string pathXml = Path.Combine(pathval, @"MarketDataDownloadFiles\AMFIData.xml");
            DataTable dtResult = new DataTable("AMFIDownloadResult");
            dtResult.Columns.Add("Date", typeof(string));
            dtResult.Columns.Add("Result", typeof(string));
            dtResult.Columns.Add("NumofRecords", typeof(int));
            DataRow dr;


            DateTime date = DateTime.Now.Date;
               string ProcessStatus_AmfiData = "";
                int num_of_rec_AmfiData = -1;
                bool result = true;
                ProductPriceDownloadLogBo productPriceDownloadLogBo = new ProductPriceDownloadLogBo();
                AdminDownloadProcessLogVo processLogVo = new AdminDownloadProcessLogVo();
                processLogVo.CreatedBy = UserId;
                processLogVo.AssetClass = "MF";
                processLogVo.SourceName = "AMFI";
                processLogVo.StartTime = DateTime.Now;
                processLogVo.EndTime = DateTime.Now;
                processLogVo.ModifiedOn = DateTime.Now;
                processLogVo.ModifiedBy = UserId;

                //Create a process for the download
                processLogVo.ProcessID = productPriceDownloadLogBo.CreateProcessLog(processLogVo);
                try
                {
                    
                    processLogVo.ForDate = date;
                    string orginalData = CEodFetch.GetAmfiCurrentData();
                    if (orginalData != null)
                    {
                        result = CEodFetch.WriteToFile(pathAmfi, orginalData);

                        if (result)
                        {
                            processLogVo.IsConnectionToSiteEstablished = 1;
                            processLogVo.IsFileDownloaded = 1;
                            result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);

                            StreamReader sr = new StreamReader(pathAmfi);
                            StringBuilder strNewData = new StringBuilder();
                            String line;

                            //Omit lines which are there in the text files for information purpose
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Contains(";"))
                                {
                                    strNewData.AppendLine(line);
                                }
                                num_of_rec_AmfiData++;
                            }
                            sr.Close();

                            //Create Filtered Data text file
                            result = CEodFetch.WriteToFile(pathFilter, strNewData.ToString());

                            if (result)
                            {
                                DataSet ds = TextToDataSet.GetAmfiDataSet(pathFilter, date);

                                if (null != ds)
                                {
                                    processLogVo.NoOfRecordsDownloaded = ds.Tables[0].Rows.Count;

                                    ds.WriteXml(Path.Combine(pathval, @"MarketDataDownloadFiles\" + processLogVo.ProcessID + ".xml"), XmlWriteMode.WriteSchema);
                                    processLogVo.IsConversiontoXMLComplete = 1;
                                    processLogVo.XMLFileName = processLogVo.ProcessID + ".xml";
                                    result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                                    // result = DBHelper.UpdateTable(ds.Tables["AMFI"], out Message);
                                    result = marketdatapullDao.InsertDataToTemp(ds, "AMFI");

                                    if (result)
                                    {
                                        num_of_rec_AmfiData = marketdatapullDao.InsertDataintoMarketPriceTable(date, "AMFI");
                                        if (num_of_rec_AmfiData == 0)
                                        {
                                            ProcessStatus_AmfiData = "Latest data already exists in the database for the date : " + date + "";
                                            processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                            processLogVo.Description = ProcessStatus_AmfiData;
                                        }
                                        else
                                        {
                                            ProcessStatus_AmfiData = "Data Updated successfully into the table";
                                            processLogVo.IsInsertiontoDBdone = 1;
                                            processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                            processLogVo.Description = ProcessStatus_AmfiData;
                                        }

                                    }
                                    else
                                    {
                                        ProcessStatus_AmfiData = "Failed to insert data into Amfi table";
                                        num_of_rec_AmfiData = 0;
                                        processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                        processLogVo.Description = ProcessStatus_AmfiData;
                                    }
                                }
                                else
                                {
                                    ProcessStatus_AmfiData = "Could not create data set from the filtered data!";
                                    num_of_rec_AmfiData = 0;
                                    processLogVo.NoOfRecordsDownloaded = num_of_rec_AmfiData;
                                    processLogVo.Description = ProcessStatus_AmfiData;
                                }


                            }
                            else
                            {
                                ProcessStatus_AmfiData = "Failed to insert data into Amfi table";
                                num_of_rec_AmfiData = 0;
                                processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                                processLogVo.Description = ProcessStatus_AmfiData;
                            }

                        }
                        else
                        {
                            ProcessStatus_AmfiData = "Could not write the downloaded data to the disk!";
                            num_of_rec_AmfiData = 0;
                            processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                            processLogVo.Description = ProcessStatus_AmfiData;
                        }
                    }
                    else
                    {
                        ProcessStatus_AmfiData = "Data not available on the site!";
                        num_of_rec_AmfiData = 0;
                        processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                        processLogVo.Description = ProcessStatus_AmfiData;
                    }
                }
                catch (Exception ex)
                {
                    ProcessStatus_AmfiData = "Exception in Amfi Data Fectch, " + ex.Message;
                    num_of_rec_AmfiData = 0;
                    processLogVo.NoOfRecordsInserted = num_of_rec_AmfiData;
                    processLogVo.Description = ProcessStatus_AmfiData;
                }
                processLogVo.EndTime = DateTime.Now;
                result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                dr = dtResult.NewRow();
                dr["Date"] = date.ToShortDateString();
                dr["Result"] = ProcessStatus_AmfiData;
                dr["NumofRecords"] = num_of_rec_AmfiData;
                dtResult.Rows.Add(dr);
                date = date.AddDays(1);
           
            DataTable dtresulttable = dtResult;
            return dtResult;
        }

        #endregion

        #region Download NSE Equity data
        public DataTable downloadNSEEquityData(int UserId, DateTime Startdate, DateTime Enddate)
        {


            string pathval = System.AppDomain.CurrentDomain.BaseDirectory;
            if (pathval.EndsWith("\\bin\\Debug\\"))
            {
                pathval = pathval.Replace("\\bin\\Debug", "");

            }
            string pathNse = Path.Combine(pathval, @"MarketDataDownloadFiles\NseEquities.txt");
            string pathFilter = Path.Combine(pathval, @"MarketDataDownloadFiles\NseEquitiesFiltered.txt");
            string pathNseZip = Path.Combine(pathval, @"MarketDataDownloadFiles\NSEEquities.zip");
            string pathNSEEquityFile = Path.Combine(pathval, @"MarketDataDownloadFiles\NSEEquities.CSV");
            string pathextract = Path.Combine(pathval, @"MarketDataDownloadFiles");
            
            string Downloadtype = "NSEEquities";
            MarketDataPullDao marketdatapullDao = new MarketDataPullDao();
            DateTime date = Startdate;
            DataTable dtResult = new DataTable("NSECorpActsDownloadResult");
            dtResult.Columns.Add("Date", typeof(string));
            dtResult.Columns.Add("Result", typeof(string));
            dtResult.Columns.Add("NumofRecords", typeof(int));
            DataRow dr;

            //date = CEodFetch.ValidateDay(date);
            do
            {
                bool result = true;
                
                string ProcessStatus_NseData = "";
                int num_of_rec_NseData = -1;
                ProductPriceDownloadLogBo productPriceDownloadLogBo  = new ProductPriceDownloadLogBo(); 
                AdminDownloadProcessLogVo processLogVo = new AdminDownloadProcessLogVo();
                processLogVo.CreatedBy = UserId;
                processLogVo.AssetClass = "Equity";
                processLogVo.SourceName = "NSE";
                processLogVo.StartTime = DateTime.Now;
                processLogVo.EndTime = DateTime.Now;
                processLogVo.ModifiedOn = DateTime.Now;
                processLogVo.ModifiedBy = UserId;
                processLogVo.ForDate = date;
                //Create a process for the download
                processLogVo.ProcessID = productPriceDownloadLogBo.CreateProcessLog(processLogVo);
                try
                {
                    string fetchData = CEodFetch.GetNseEquities(date);
                    
                    //Download data from NSE Site
                    WebClient myWebClient = new WebClient();
                    myWebClient.DownloadFile(fetchData, pathNseZip);
                    processLogVo.IsConnectionToSiteEstablished = 1;
                    processLogVo.IsFileDownloaded = 1;
                    result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                    if (result)
                    {
                    
                        //Zip Extracting Code
                        FastZip fz = new FastZip();
                        fz.ExtractZip(pathNseZip, pathextract, "");
                        
                        //filename convert
                        string pathNSEDownloadFile = Path.Combine(pathval, @"MarketDataDownloadFiles\") + CEodFetch.GetDownloadFile(date, Downloadtype);

                        FileInfo renamefile = new FileInfo(pathNSEDownloadFile);
                        if (renamefile.Exists)
                        {
                            File.Delete(pathNSEEquityFile);
                            File.Move(pathNSEDownloadFile, pathNSEEquityFile);
                        }

// ********************************************************************************
                    
                    
                        
                        
                        //Store the retrieved data in a text file

                        //result = CEodFetch.WriteToFile(pathNse, pathNSEEquityFile);

                        if (result)
                        {
                            //Create a stream reader for the text file
                            StreamReader sr = new StreamReader(pathNSEEquityFile);
                            StringBuilder strNewData = new StringBuilder();
                            String line;

                            //Omit lines which are there in the text files 
                            //for information purpose
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Contains(","))
                                {
                                    strNewData.AppendLine(line);
                                }
                            }
                            sr.Close();

                            //Create Filtered Data text file
                            result = CEodFetch.WriteToFile(pathFilter, strNewData.ToString());

                            if (result)
                            {
                                //Create Data Set from the filtered data
                                DataSet ds = TextToDataSet.GetNseEquitiesDataSet(pathFilter);
                                

                                if (null != ds)
                                {
                                    processLogVo.NoOfRecordsDownloaded = ds.Tables[0].Rows.Count;
                                    
                                    ds.WriteXml(Path.Combine(pathval, @"MarketDataDownloadFiles\" +processLogVo.ProcessID + ".xml"),XmlWriteMode.WriteSchema);
                                    processLogVo.IsConversiontoXMLComplete = 1;
                                    processLogVo.XMLFileName = processLogVo.ProcessID + ".xml";
                                    result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                                    //Store the data set into NSE EOD Market Data Table
                                    string Message ="";
                                    //result = DBHelper.UpdateTable(ds.Tables["NSE"], out Message);
                                    result = marketdatapullDao.InsertDataToTemp(ds, "NSE");


                                    if (result)
                                    {
                                        num_of_rec_NseData = marketdatapullDao.InsertDataintoMarketPriceTable(date, "NSE");
                                        
                                        if (num_of_rec_NseData == 0)
                                        {
                                            ProcessStatus_NseData = "Latest data already exists in the database for the date : " + date + "";
                                            processLogVo.NoOfRecordsInserted = num_of_rec_NseData;
                                            processLogVo.Description = ProcessStatus_NseData;
                                                                                        
                                        }
                                        else
                                        {
                                            ProcessStatus_NseData = "Data Updated successfully into the table";
                                            processLogVo.IsInsertiontoDBdone = 1;
                                            processLogVo.NoOfRecordsInserted = num_of_rec_NseData;
                                            processLogVo.Description = ProcessStatus_NseData;
                                        }
                                        

                                    }
                                    else
                                    {
                                        ProcessStatus_NseData = "Failed to insert the data into NSEEquities: " + Message;
                                        num_of_rec_NseData = 0;
                                        processLogVo.NoOfRecordsInserted = num_of_rec_NseData;
                                        processLogVo.Description = ProcessStatus_NseData;
                                    }
                                }
                                else
                                {
                                    ProcessStatus_NseData = "Could not create data set from the filtered data!";
                                    num_of_rec_NseData = 0;
                                    processLogVo.NoOfRecordsInserted = num_of_rec_NseData;
                                    processLogVo.Description = ProcessStatus_NseData;

                                }
                            }
                            else
                            {
                                ProcessStatus_NseData = "Could not write the filtered data to the disk!";
                                num_of_rec_NseData = 0;
                                processLogVo.NoOfRecordsInserted = num_of_rec_NseData;
                                processLogVo.Description = ProcessStatus_NseData;
                            }
                        }
                        else
                        {
                            ProcessStatus_NseData = "Could not write the downloaded data to the disk!";
                            num_of_rec_NseData = 0;
                            processLogVo.NoOfRecordsDownloaded = num_of_rec_NseData;
                            processLogVo.Description = ProcessStatus_NseData;
                        }
                    }
                    else
                    {
                        ProcessStatus_NseData = "Data not available on the site!";
                        num_of_rec_NseData = 0;
                        processLogVo.NoOfRecordsDownloaded = num_of_rec_NseData;
                        processLogVo.Description = ProcessStatus_NseData;
                    }
                }
                catch (Exception exp)
                {
                    ProcessStatus_NseData = "Exception in NSE Equities EOD Fetch: " + exp.Message;
                    num_of_rec_NseData = 0;
                    processLogVo.NoOfRecordsDownloaded = num_of_rec_NseData;
                    processLogVo.Description = ProcessStatus_NseData;
                }
                processLogVo.EndTime = DateTime.Now;
                result = productPriceDownloadLogBo.UpdateProcessLog(processLogVo);
                dr = dtResult.NewRow();
                dr["Date"] = date.ToShortDateString();
                dr["Result"] = ProcessStatus_NseData;
                dr["NumofRecords"] = num_of_rec_NseData;
                dtResult.Rows.Add(dr);
                date = date.AddDays(1);
            } while (date <= Enddate);
            DataTable dtresulttable = dtResult;
            return dtResult;
        }
        #endregion
    }
}
