
#region CopyRight
/**********************************************************************
* * (c) Principal Consulting Group 2008. *
* * *
* * The computer program listings and specification herein are the *
* * property of Principal Consulting Group and are not to be *
* * reproduced or copied in whole or in part for any reason without *
* * written permission of Principal Consulting Group. *
* * *
* * Copyright (c) 2008 Principal Consulting Group. *
* * All rights reserved *
* * *
* * File Name : $RCSfile: $ *
* * Last updated by : $Author: $ *
* * Date Updated : $Date: $ *
* *********************************************************************
* *
* * Revision History
* * $Log: $
* *
* *
***********************************************************************/
#endregion


using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Diagnostics;

///<summary>
namespace BoMarketDataPull.App_Code
{
    /// <summary>
    /// Helper class to download data from any website and write it to the disk
    /// </summary>
    public class CEodFetch
    {

        #region Construction

        /// <summary>
        /// Private constructor to avoid class instantiation
        /// </summary>
        private CEodFetch()
        {
        }

        #endregion Construction


        #region Public Methods
        /// <summary>
        /// Downloads NSE Corporate Actions data for a specified date from www.nse-india.com 
        /// and convert it to a large string.
        /// </summary>
        /// <param name="date">Date from which the data to be downloaded</param>
        /// <returns>Downloaded NSE Corporate Actions data data as a string</returns>
        
        /// <summary>
        /// Downloads data for a specified date from appropriate website 
        /// and convert it to a large string.
        /// </summary>
        /// <param name="date">Date from which the data to be downloaded</param>
        /// <returns>Downloaded BSE Equities data as a string</returns>
        public static string GetDownloads(DateTime date)
        {
            string url = "";
            

            if (null != date)
            {
                int year = date.Year;
                int day = date.Day;
                string strMonth = date.Month.ToString();
                string strDay = day.ToString();

                //For day of the month with only one digit prepend 0. 
                //For eg. day= "1" becomes "01"
                if (strDay.Length == 1)
                {
                    strDay = "0" + strDay;
                }

                //For month with only one digit prepend 0. 
                //For eg. month= "1" becomes "01"
                if (strMonth.Length == 1)
                {
                    strMonth = "0" + strMonth;
                }

                //Construct the URL for download
                //The actual url format for eg. 20-MAR-2008
                //http://www.bseindia.com/bhavcopy/eq200309_csv.zip

                
                    url = @"http://www.bseindia.com/bhavcopy/";
                    url = url + @"eq" + strDay +
                            strMonth + year.ToString().Substring(2) + @"_csv.zip";
               
                
               

            }

            //Download from the URL and return the result
            return url;//ProcessUrl(url);

        }

        public static string GetDownloadsNSECrpacts(DateTime startdate, DateTime enddate)
        {
            string url = "";
            

            
                int year = startdate.Year;
                int day = startdate.Day;
                string strstartMonth = startdate.Month.ToString();
                string strstartDay = day.ToString();

                int endyear = enddate.Year;
                int endday = enddate.Day;
                string strendMonth = enddate.Month.ToString();
                string strendDay = endday.ToString();

                //For day of the month with only one digit prepend 0. 
                //For eg. day= "1" becomes "01"
                if (strstartDay.Length == 1)
                {
                    strstartDay = "0" + strstartDay;
                }

                //For month with only one digit prepend 0. 
                //For eg. month= "1" becomes "01"
                if (strstartMonth.Length == 1)
                {
                    strstartMonth = "0" + strstartMonth;
                }

                if (strendDay.Length == 1)
                {
                    strendDay = "0" + strendDay;
                }

                //For month with only one digit prepend 0. 
                //For eg. month= "1" becomes "01"
                if (strendMonth.Length == 1)
                {
                    strendMonth = "0" + strendMonth;
                }
      
               
                
                    url = @"http://www.nse-india.com/content/corporate/datafiles/";
                    //string strlastdate = getLastDate("NSE_Corporate_Actions");
                    url = url + strstartDay + strstartMonth + endyear.ToString() + "_" + strendDay + strendMonth + endyear.ToString() + @"_1.csv";
                
               

            

            //Download from the URL and return the result
            return url;//ProcessUrl(url);

        }

        public static string GetDownloadFile(DateTime date, string strDownloadtype)
        {
            string url = "";

            if (null != date)
            {
                int year = date.Year;
                int day = date.Day;
                
                string strDay = day.ToString();
                if (strDay.Length == 1)
                {
                    strDay = "0" + strDay;
                }

                if (strDownloadtype == "BSEEquities")
                {

                    //For day of the month with only one digit prepend 0. 
                    //For eg. day= "1" becomes "01"
                    

                    //For month with only one digit prepend 0. 
                    //For eg. month= "1" becomes "01"
                    string strMonth = date.Month.ToString();
                    if (strMonth.Length == 1)
                    {
                        strMonth = "0" + strMonth;
                    }

                    //Construct the URL for download
                    //The actual url format for eg. 20-MAR-2008
                    //http://www.bseindia.com/bhavcopy/eq200309_csv.zip
                    
                        url = url + @"EQ" + strDay +
                                    strMonth + year.ToString().Substring(2) + @".CSV";
                }
                else if (strDownloadtype == "NSECorpActs")
                {
                   // string strlastdate = getLastDate("NSE_Corporate_Actions");

                    //url = url + strlastdate + "_" + strDay + strMonth + year.ToString() + @"_1.csv";
                }
                else if (strDownloadtype == "NSEEquities")
                {
                    string month = ProcessMonth(date.Month);

                    url = @"cm" + strDay +
                            month + year.ToString() + @"bhav.csv";
                }

            }

            //Download from the URL and return the result
            return url;//ProcessUrl(url);

        }
        //Get the last date entered for a table in the database
       


        /// <summary>
        /// Downloads NSE equities data for a specified date from www.nse-india.com 
        /// and convert it to a large string.
        /// </summary>
        /// <param name="date">Date from which the data to be downloaded</param>
        /// <returns>Downloaded NSE Equities data as a string</returns>
        public static string GetNseEquities(DateTime date)
        {
            string url = @"http://www.nse-india.com/content/historical/EQUITIES/";

            if (null != date)
            {
                int year = date.Year;
                int day = date.Day;
                string month = ProcessMonth(date.Month);
                string strDay = day.ToString();

                //For day of the month with only one digit prepend 0. 
                //For eg. day= "1" becomes "01"
                if (strDay.Length == 1)
                {
                    strDay = "0" + strDay;
                }

                //Construct the URL for download
                //The actual url format for eg. 26-May-2008
                //http://www.nse-india.com/content/historical/EQUITIES/2008/MAY/cm26MAY2008bhav.csv
                url = url + year.ToString() + @"/" + month + @"/" + @"cm" + strDay +
                            month + year.ToString() + @"bhav.csv.zip";
                
            }

            //Download from the URL and return the result
            //return ProcessUrl(url);
            return url;

        }

        /// <summary>
        /// Downloads NSE Derivatives data for a specified date from www.nse-india.com 
        /// and convert it to a large string.
        /// </summary>
        /// <param name="date">Date from which the data to be downloaded</param>
        /// <returns>Downloaded NSE Derivatives data as a string</returns>
        public static string GetNseDerivatives(DateTime date)
        {
            string url = @"http://www.nse-india.com/content/historical/DERIVATIVES/";

            if (null != date)
            {
                int year = date.Year;
                int day = date.Day;
                string month = ProcessMonth(date.Month);
                string strDay = day.ToString();

                //For day of the month with only one digit prepend 0. 
                //For eg. day= "1" becomes "01"
                if (strDay.Length == 1)
                {
                    strDay = "0" + strDay;
                }

                //Construct the URL for download
                //The actual url format for eg. 26-May-2008
                //http://www.nse-india.com/content/historical/EQUITIES/2008/MAY/fo26MAY2008bhav.csv
                url = url + year.ToString() + @"/" + month + @"/" + @"fo" + strDay +
                            month + year.ToString() + @"bhav.csv";

            }

            //Download from the URL and return the result
            return ProcessUrl(url);
        }


        /// <summary>
        /// Downloads BSE Market data for the latest available date from www.bseindia.com 
        /// and convert it to a large string.
        /// </summary>
        /// <returns>Downloaded BSE Market data as a string</returns>
        public static string GetBseCurrentData()
        {
            string url = @"http://www.bseindia.com/mktlive/bhavcopy.asp";

            //Download from the URL and return the result
            return ProcessUrl(url);
        }


        /// <summary>
        /// Downloads AMFI Market data for the historical price for given dates from amfiindia 
        /// and convert it to a large string.
        /// </summary>
        /// <returns>Downloaded AMFI Market data as a string</returns>
        public static string GetAmfiHistoricalData( string datevalue)
        {
            string url = @"http://www.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?frmdt="+datevalue;

            //Download from the URL and return the result
            return ProcessUrl(url);
        }

        /// <summary>
        /// Downloads AMFI Market data for current from amfiindia 
        /// and convert it to a large string.
        /// </summary>
        /// <returns>Downloaded AMFI Market data as a string</returns>
        public static string GetAmfiCurrentData()
        {
            string url = @"http://www.amfiindia.com/spages/NAV0.txt";

            //Download from the URL and return the result
            return ProcessUrl(url);
        }

        
        /// <summary>
        /// Download text data from any website and convert it to a string
        /// </summary>
        /// <param name="url">URL of text data to be downloaded</param>
        /// <returns>Downloaded text data as a string</returns>
        public static string ProcessUrl(string url)
        {
            string result;
            HttpWebRequest webRequest;
            HttpWebResponse webResponse;
            StreamReader streamReader;

            try
            {
                //Create a web request for the URL
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

                //Get response from the web request
                webResponse = (HttpWebResponse)webRequest.GetResponse();

                //TO BE DONE String.IsNullOrEmpty(webResponse.ToString())
               
                //Obtain response stream from the web response
                streamReader = new StreamReader(webResponse.GetResponseStream());
                
                //Construct string from the text stream
                result = streamReader.ReadToEnd().Trim();

                streamReader.Close();
            }
            catch (Exception exp)
            {
               result = "";
               Trace.WriteLine("Exception in CEodFetch::ProcessUrl: "+exp.Message);
            }

            if(result.Contains("No data found on the basis of selected parameters for this report"))
            result = null;

            return result;
        }


        /// <summary>
        /// Write a string data to a file in the disk.
        /// </summary>
        /// <param name="fileName">Path of the file to be written</param>
        /// <param name="data">Data to write into the file</param>
        /// <returns>Status of write operation, True for success</returns>
        public static bool WriteToFile(string fileName, string data)
        {
            bool result = true;
            StreamWriter streamWriter = null;
            try
            {
                //Create a stream writer for the file
               
                streamWriter = new StreamWriter(fileName);
                //Write the file to the disk
                streamWriter.Write(data);

                //Close the stream writer
                //streamWriter.Close();

            }
            catch (Exception exp)
            {
                result = false;
                Trace.WriteLine("Exception in CEodFetch::WriteToFile: " + exp.Message);
            }
            finally
            {
                if(streamWriter != null)
                streamWriter.Close();
            }
            return result;

        }


        /// <summary>
        /// Validates supplied date's day of the week to a day in set
        /// {Mon,Tue,Wed,Thu,Fri}, if not set to current date.
        /// </summary>
        /// <param name="date">Date to validate</param>
        /// <returns>Validated or re-constructed date</returns>
        public static DateTime ValidateDay(DateTime date)
        {
            //Allows only upto present date and present year
            if ((date >= DateTime.Now) || (date.Year != DateTime.Now.Year))
            {
                date = DateTime.Now;
            }

            //Need to verify later
            if (date.Hour < 18 && date.Hour != 0)
            {
                date = date.AddDays(-1);
            }

            //Switch Day of the week to determine the validity
            //
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Monday:
                    {
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        date = date.AddDays(-1);
                        Console.WriteLine("Invalid day: Saturday");
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        date = date.AddDays(-2);
                        Console.WriteLine("Invalid day: Sunday");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return date;
        }

        #endregion Public Methods


        #region Private Implementation

        /// <summary>
        /// Constructs 3 letter month name
        /// </summary>
        /// <param name="month">Integer representation of the month</param>
        /// <returns>3 Leter month name</returns>
        private static string ProcessMonth(int month)
        {
            string processedMonth;

            switch (month)
            {
                case 1:
                    {
                        processedMonth = "JAN";
                        break;
                    }
                case 2:
                    {
                        processedMonth = "FEB";
                        break;
                    }
                case 3:
                    {
                        processedMonth = "MAR";
                        break;
                    }
                case 4:
                    {
                        processedMonth = "APR";
                        break;
                    }
                case 5:
                    {
                        processedMonth = "MAY";
                        break;
                    }
                case 6:
                    {
                        processedMonth = "JUN";
                        break;
                    }
                case 7:
                    {
                        processedMonth = "JUL";
                        break;
                    }
                case 8:
                    {
                        processedMonth = "AUG";
                        break;
                    }
                case 9:
                    {
                        processedMonth = "SEP";
                        break;
                    }
                case 10:
                    {
                        processedMonth = "OCT";
                        break;
                    }
                case 11:
                    {
                        processedMonth = "NOV";
                        break;
                    }
                case 12:
                    {
                        processedMonth = "DEC";
                        break;
                    }
                default:
                    {
                        processedMonth = "";
                        break;
                    }

            }
            return processedMonth;
        }





        #endregion Private Implementation


    }
}

