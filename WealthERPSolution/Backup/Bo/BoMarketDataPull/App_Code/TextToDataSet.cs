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
using System.Text.RegularExpressions;
using System.IO;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Diagnostics;


namespace BoMarketDataPull.App_Code
{
    /// <summary>
    /// Helper class to construct data sets from delimited text files
    /// </summary>
    public class TextToDataSet
    {
      

        #region Construction

        /// <summary>
        /// Private Constructor to avoid class instantiation 
        /// </summary>
        private TextToDataSet()
        {
        }

        #endregion Construction

        #region Public Methods

        /// <summary>
        /// Constructs a AMFI Mutual Funds data set from the supplied Text File
        /// </summary>
        /// <param name="textFile">Text file path</param>
        /// <returns>Data set constructed from the text file</returns>
        public static DataSet GetAmfiDataSet(string textFile, DateTime date)
        {
            if (!System.IO.File.Exists(textFile))
            {
                return null;//fail file not found
            }
            StreamReader textStream = new StreamReader(textFile, true);

            //Create a new data set
            DataSet dsFile = new DataSet("File");

            //Create a new data table
            DataTable dtTable = new DataTable("AMFI");
            try
            {
                //Create a new stream for the input file
                

                //Add columns
                //A simple way to add columns.
                //A better and more re-usable way would be to add a function to 
                //Either pass in column names (and / or types) or
                //optionally parse the first line of the input file to generate the field / column names

                //Remember accessing by column name is case sensitive
                //
                dtTable.Columns.Add("Scheme_Code", typeof(string));
                dtTable.Columns.Add("Scheme_Name", typeof(string));
                dtTable.Columns.Add("Net_Asset_Value", typeof(decimal));
                dtTable.Columns.Add("Repurchase_Price", typeof(decimal));
                dtTable.Columns.Add("Sale_Price", typeof(decimal));
                dtTable.Columns.Add("Post_Date", typeof(DateTime));
                dtTable.Columns.Add("Date", typeof(DateTime));

                //Populate the data table from the text stream
                dtTable = PopulateDataTable(textStream, ";", true, dtTable);
                foreach (DataRow dr in dtTable.Rows)
                {
                    dr["Date"] = date.Date;
                }
                dsFile.Tables.Add(dtTable);
            }
            catch (Exception exp)
            {
                Trace.WriteLine("Exception in CEodFetch::WriteToFile: " + exp.Message);
            }
            finally
            {
                textStream.Close();
            }
            //Return the DataSet 
            return dsFile;
        }


        /// <summary>
        /// Constructs a NSE Equities data set from the supplied Text File
        /// </summary>
        /// <param name="textFile">Text file path</param>
        /// <returns>Data set constructed from the text file</returns>
        public static DataSet GetNseEquitiesDataSet(string textFile)
        {
            if (!System.IO.File.Exists(textFile))
            {
                return null;//fail file not found
            }

            //Create a new stream for the input file
            StreamReader textStream = new StreamReader(textFile, true);

            //Create a new data set
            DataSet dsFile = new DataSet("File");

            //Create a new data table
            DataTable dtTable = new DataTable("NSE");
            try
            {

                //Add columns
                //
                dtTable.Columns.Add("Symbol", typeof(string));
                dtTable.Columns.Add("Series", typeof(string));
                dtTable.Columns.Add("Open_Price", typeof(decimal));
                dtTable.Columns.Add("High_Price", typeof(decimal));
                dtTable.Columns.Add("Low_Price", typeof(decimal));
                dtTable.Columns.Add("Close_Price", typeof(decimal));
                dtTable.Columns.Add("Last_Price", typeof(decimal));
                dtTable.Columns.Add("Previous_Close", typeof(decimal));
                dtTable.Columns.Add("Total_Trade_Qty", typeof(decimal));
                dtTable.Columns.Add("Total_Trade_Value", typeof(decimal));
                dtTable.Columns.Add("Date", typeof(DateTime));

                //Populate the data table from the text stream
                dtTable = PopulateDataTable(textStream, ",", false, dtTable);

                dsFile.Tables.Add(dtTable);
                
            }
            catch (Exception exp)
            {
                Trace.WriteLine("Exception in CEodFetch::WriteToFile: " + exp.Message);
            }
            finally
            {
                textStream.Close();
            }
            //Return the DataSet 
            return dsFile;
        }

        public static DataSet GetBSEEquitiesDataSet(string textFile, DateTime date)
        {
            if (!System.IO.File.Exists(textFile))
            {
                return null;//fail file not found
            }

            //Create a new stream for the input file
            StreamReader textStream = new StreamReader(textFile, true);

            //Create a new data set
            DataSet dsFile = new DataSet("File");

            //Create a new data table
            DataTable dtTable = new DataTable("BSE");
            try
            {
                //Add columns
                //
                dtTable.Columns.Add("SC_CODE", typeof(int));
                dtTable.Columns.Add("SC_NAME", typeof(string));
                dtTable.Columns.Add("SC_GROUP", typeof(string));
                dtTable.Columns.Add("SC_TYPE", typeof(string));
                dtTable.Columns.Add("OPEN_VALUE", typeof(decimal));
                dtTable.Columns.Add("HIGH_VALUE", typeof(decimal));
                dtTable.Columns.Add("LOW_VALUE", typeof(decimal));
                dtTable.Columns.Add("CLOSE_VALUE", typeof(decimal));
                dtTable.Columns.Add("LAST_VALUE", typeof(decimal));
                dtTable.Columns.Add("PREVCLOSE_VALUE", typeof(decimal));
                dtTable.Columns.Add("NO_TRADES_VALUE", typeof(decimal));
                dtTable.Columns.Add("NO_OF_SHRS_VALUE", typeof(decimal));
                dtTable.Columns.Add("NET_TURNOV_VALUE", typeof(decimal));
                dtTable.Columns.Add("TDCLOINDI_VALUE", typeof(decimal));
                dtTable.Columns.Add("Date", typeof(DateTime));

                //Populate the data table from the text stream
                dtTable = PopulateDataTable(textStream, ",", false, dtTable);
                foreach (DataRow dr in dtTable.Rows)
                {
                    dr["Date"] = date.Date;
                }
                dsFile.Tables.Add(dtTable);
            }
            catch (Exception exp)
            {
                Trace.WriteLine("Exception in CEodFetch::WriteToFile: " + exp.Message);
            }
            finally
            {
                textStream.Close();
            }
           
            
            //Return the DataSet 
            return dsFile;
        }

        public static DataSet GetNSECrpActsDataSet(string textFile, DateTime date)
        {
            if (!System.IO.File.Exists(textFile))
            {
                return null;//fail file not found
            }

            //Create a new stream for the input file
            StreamReader textStream = new StreamReader(textFile, true);

            //Create a new data set
            DataSet dsFile = new DataSet("File");

            //Create a new data table
            DataTable dtTable = new DataTable("NSE_Corporate_Actions");

            //Add columns
            //
            try
            {
                dtTable.Columns.Add("Symbol", typeof(string));
                dtTable.Columns.Add("Series", typeof(string));
                dtTable.Columns.Add("Record_Date", typeof(DateTime));
                dtTable.Columns.Add("Bc_Start_Date", typeof(DateTime));
                dtTable.Columns.Add("Bc_End_Date", typeof(DateTime));
                dtTable.Columns.Add("Ex_Date", typeof(DateTime));
                dtTable.Columns.Add("No_Delivery_Start_Date", typeof(DateTime));
                dtTable.Columns.Add("No_Delivery_End_Date", typeof(DateTime));
                dtTable.Columns.Add("Purpose", typeof(string));
                dtTable.Columns.Add("Date", typeof(DateTime));

                //Populate the data table from the text stream
                dtTable = PopulateDataTable(textStream, ",", false, dtTable);
                foreach (DataRow dr in dtTable.Rows)
                {
                    dr["Date"] = date.Date;
                }
                dsFile.Tables.Add(dtTable);
            }
            catch (Exception exp)
            {
                Trace.WriteLine("Exception in CEodFetch::WriteToFile: " + exp.Message);
            }
            finally
            {
                textStream.Close();
            }
            //Return the DataSet 
            return dsFile;
        }


        /// <summary>
        /// Constructs a NSE Derivatives data set from the supplied Text File
        /// </summary>
        /// <param name="textFile">Text file path</param>
        /// <returns>Data set constructed from the text file</returns>
        public static DataSet GetNseDerivativesDataSet(string textFile)
        {
            if (!System.IO.File.Exists(textFile))
            {
                return null;//fail file not found
            }

            //Create a new stream for the input file
            StreamReader textStream = new StreamReader(textFile, true);

            //Create a new data set
            DataSet dsFile = new DataSet("File");

            //Create a new data table
            DataTable dtTable = new DataTable("NSEDerivatives");

            //Add columns
            //
            dtTable.Columns.Add("Instrument", typeof(string));
            dtTable.Columns.Add("Symbol", typeof(string));
            dtTable.Columns.Add("Expiry_Date", typeof(DateTime));
            dtTable.Columns.Add("Strike_Price", typeof(decimal));
            dtTable.Columns.Add("Option_Type", typeof(string));
            dtTable.Columns.Add("Open_Price", typeof(decimal));
            dtTable.Columns.Add("High_Price", typeof(decimal));
            dtTable.Columns.Add("Low_Price", typeof(decimal));
            dtTable.Columns.Add("Close_Price", typeof(decimal));
            dtTable.Columns.Add("Settle_Price", typeof(decimal));
            dtTable.Columns.Add("Contracts", typeof(decimal));
            dtTable.Columns.Add("Value_In_Lakhs", typeof(decimal));
            dtTable.Columns.Add("Open_Int", typeof(decimal));
            dtTable.Columns.Add("Change_In_OI", typeof(decimal));
            dtTable.Columns.Add("Date", typeof(DateTime));

            //Populate data table with values from text stream
            dtTable = PopulateDataTable(textStream, ",", false, dtTable);
            
            dsFile.Tables.Add(dtTable);

            //Return the DataSet 
            return dsFile;
        }

        #endregion Public Methods


        #region Private Implementation
                

        /// <summary>
        /// Scans through the text input stream one line at a time, and extracts tokens 
        /// based on the delimiter using regular expressions. Extracted tokens will be 
        /// added to a data table as each line representing a data row.
        /// </summary>
        /// <param name="textStream">Input text data stream to process</param>
        /// <param name="delimiter">Token delimiter of the text stream</param>
        /// <param name="processLastValue">Boolean value indicates whether last token process is required</param>
        /// <param name="dataTable">Data Table to add rows into it</param>
        /// <returns>Data table populated with data rows constructed from the text stream</returns>
        private static DataTable PopulateDataTable(StreamReader textStream, string delimiter, bool processLastValue, DataTable dataTable)
        {
            //Set Regular Expression
            //If you were parsing a standard semi-colon delimited text file
            //i.e. "a";55;"some text";"d"
            //regExp returns": "a" and 55 and "some text" and "d"
            //	
            Regex regExp = new Regex(delimiter + "(?=([^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            String currentLine;
            DateTime date = DateTime.Today;

            if((textStream !=null) && (dataTable != null))
            {
                // Get the first line of data from the input text file.
                currentLine = NextLine(textStream); //Ignore the first row with field names
                currentLine = NextLine(textStream);
                           
                int start;//used as a placeholder for current location
                int count;//used to determine which field currently extracting

                //used to temporarily hold the field pulled out by RegEx in order to trim 
                //the quotation marks from the ends of the field (if any)
                string temp;

                //Data row object used to set values
                DataRow drRow;

                //Strip out each field and insert in DataTable
                //
                while (currentLine != "")//currentLine is empty when the input stream has been read to the end
                {
                    start = 0;
                    count = 0;

                    //create a new row
                    drRow = dataTable.NewRow();

                    //Iterate through all the matches (match=field in current row)
                    foreach (Match m in regExp.Matches(currentLine))
                    {
                        //Retrieve the field based on the results of the match
                        temp = currentLine.Substring(start, m.Index - start);

                        //Get rid of quotes around certain fields if any
                        temp = temp.Trim('"');

                        //Change the field value N.A. to 0.0000 if any
                        if (temp.Equals("N.A."))
                        {
                            temp = "0.0000";
                        }
                        else if (temp.Equals("-"))
                        {
                            temp = "";
                        }
                        
                        //fill the field by index

                        if (temp == "")
                            drRow[count] = DBNull.Value;
                        else
                        {
                            if (dataTable.Columns[count].DataType.ToString() == "System.DateTime")
                            {
                                //to format date in mm/dd/yyyy order
                                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                                DateTime dt = DateTime.Parse(temp, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                                
                                drRow[count] = dt;
                            }
                            else
                                drRow[count] = temp;
                        }
                        //keep one step ahead 
                        start = m.Index + 1;

                        //keep track of which field is next
                        count++;
                    }

                    //Process the last value if required from the file
                    //
                    if (processLastValue)
                    {
                        //squeeze out the last field which isn't extracted in the loop above
                        temp = currentLine.Substring(start, currentLine.Length - start);

                        //Get rid of quotes around the field
                        temp = temp.Trim('"');
                        drRow[count] = temp;
                    }

                    //Now add the populated row to the data table
                    dataTable.Rows.Add(drRow);
                    

                    //Get the next line from the input file
                    currentLine = NextLine(textStream);

                    //And back to the top....
                }
            }

            //Return the Data Table
            return dataTable;

        }


        /// <summary>
        /// Extracts one line of text data at a time from an input stream.
        /// Assumption: each line ends in a single \n character.
        /// </summary>
        /// <param name="stream">Input data stream from which the line to be extracted</param>
        /// <returns>A line from the input stream</returns>
        private static String NextLine(StreamReader stream)
        {
            int temp = stream.Read();
            String returnVal = "";

            while (temp != -1 && temp != '\n')
            {
                returnVal += (char)temp;
                temp = stream.Read();
            }
            return returnVal;
        }

        #endregion Private Implementation
    }
}
