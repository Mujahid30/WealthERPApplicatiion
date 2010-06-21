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
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using DaoMarketDataPull;
using BoCommon;


namespace BoMarketDataPull.App_Code
{
    /// <summary>
    /// Helper class for Database Operations
    /// </summary>
    public class DBHelper
    {
        #region Fields

        //Needs to change depends on the server
        private static string s_ConnString = "Data Source=pcg1server;Initial Catalog=MarketData_db;Persist Security Info=True;User ID=sa;Password=pcg123#";
        
        private static SqlConnection s_Connection;

        #endregion Fields


        #region Construction

        /// <summary>
        /// Private constructor to avoid class instantiation
        /// </summary>
        private DBHelper()
        {
        }

        #endregion Construction

        #region Public Methods


        /// <summary>
        /// Insert values from the supplied data table to the database
        /// </summary>
        /// <param name="marketDataTable">Data table contains values to be updated in the database</param>
        /// <param name="error">[Out] If return value is False, contains the reason for the error</param>
        /// <returns>Boolean Status, True if succeed</returns>
        public static bool UpdateTable( DataTable marketDataTable, out string error)
        {
            //Set the database retrieval query for the supplied data table
            
            string assetType = marketDataTable.TableName;

            
            
            bool result = true;
           

            
                try
                {
                    MarketDataPullDao marketdatapulldao = new MarketDataPullDao();
                    DataSet dsExistingValues = marketdatapulldao.GetExistingValues((DateTime)marketDataTable.Rows[0]["Date"], assetType);

                    //Check whether the records for the inserting date is already exist in the database table 
                    if (dsExistingValues.Tables[0].Rows.Count == 0)
                    {
                        DataSet ds1 = new DataSet();
                        ds1.Tables.Add(marketDataTable.Copy());
                        int rowsaffected = marketdatapulldao.InsertData(ds1, assetType);
                        error = rowsaffected.ToString();
                        
                    }
                    else
                    {
                        GetDifferenceInTables getdifference = new GetDifferenceInTables();
                        DataTable difference = getdifference.Difference(marketDataTable, dsExistingValues.Tables[0]);

                        if (difference.Rows.Count == 0)
                        {
                            result = false;
                            error = "The Market Data for the date: " +
                                    marketDataTable.Rows[0]["Date"].ToString()
                                    + " already exists in the database";
                        }
                        else
                        {
                            error = "";
                        }
                    }

                }
                catch (ArgumentNullException exp)
                {
                    result = false;
                    error = "Exception occurred while Market Data Insertion into the Database; Table Name: "
                            + marketDataTable.TableName + " ;Message: " + exp.Message;
                }
                catch (InvalidOperationException exp)
                {
                    result = false;
                    error = "Exception occurred while Market Data Insertion into the Database; Table Name: "
                            + marketDataTable.TableName + " ;Message: " + exp.Message;
                }
                catch (DBConcurrencyException exp)
                {
                    result = false;
                    error = "Exception occurred while Market Data Insertion into the Database; Table Name: "
                            + marketDataTable.TableName + " ;Message: " + exp.Message;
                }
                
            
            

            return result;
        }

        #endregion Public Methods


        #region Private Implementation

        /// <summary>
        /// Checks whether a date already exists in the databse for a supplied table
        /// </summary>
        /// <param name="tableName">Name of the table in database</param>
        /// <param name="date">Date to be checked</param>
        /// <returns>Result of the check operation, an exception also returns true</returns>
        private static bool CheckDateExists(string tableName, DateTime date)
        {
            bool result = false;

            //Set up a new SQL server connection
            s_Connection = new SqlConnection(s_ConnString);

            if (s_Connection != null)
            {
                try
                {
                    //Create a sql command to get distinct dates from the database
                    //
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select distinct Date from " + tableName;
                    cmd.Connection = s_Connection;

                    //Open SQL Server database connection
                    s_Connection.Open();

                    //Execute Command and retrieve the result in a Data Reader
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (null != dataReader)
                    {
                        //If date already exists in the result set, 
                        //set the return value true
                        while (dataReader.Read())
                        {
                            if ((DateTime)dataReader["Date"] == date)
                            {
                                result = true;
                                break;
                            }
                        }
                    }

                }
                catch (SqlException exp)
                {
                    Trace.WriteLine("Error in DBHelper::CheckDateExists(): " + exp.Message);
                    result = true;
                }
                catch (ArgumentException exp)
                {
                    Trace.WriteLine("Error in DBHelper::CheckDateExists(): " + exp.Message);
                    result = true;
                }
                catch (InvalidOperationException exp)
                {
                    Trace.WriteLine("Error in DBHelper::CheckDateExists(): " + exp.Message);
                    result = true;
                }
                finally
                {
                    //Close the SQL server connection
                    s_Connection.Close();
                }
            }

            return result;
        }
        public static DateTime getLastDate(string tableName)
        {
            DateTime lastdate = new DateTime();

            s_Connection = new SqlConnection(s_ConnString);

            if (s_Connection != null)
            {
                try
                {
                    //Create a sql command to get distinct dates from the database
                    //
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select max([Date]) from " + tableName;
                    cmd.Connection = s_Connection;

                    //Open SQL Server database connection
                    s_Connection.Open();

                    //Execute Command and retrieve the result in a Data Reader
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (null != dataReader)
                    {
                        //If date already exists in the result set, 
                        //set the return value true
                        while (dataReader.Read())
                        {
                            lastdate = Convert.ToDateTime(dataReader[0]);
                        }
                        
                    }

                }
                catch (SqlException exp)
                {
                    Trace.WriteLine("Error in DBHelper::CheckDateExists(): " + exp.Message);
                    
                }
                catch (ArgumentException exp)
                {
                    Trace.WriteLine("Error in DBHelper::CheckDateExists(): " + exp.Message);
                    
                }
                catch (InvalidOperationException exp)
                {
                    Trace.WriteLine("Error in DBHelper::CheckDateExists(): " + exp.Message);
                    
                }
                finally
                {
                    //Close the SQL server connection
                    s_Connection.Close();
                }
            }
            return lastdate;

        
        }

        #endregion Private Implementation
    }
}
