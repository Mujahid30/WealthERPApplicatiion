using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoMarketDataPull
{
    public class MarketDataPullDao
    {
        //This method retrevies data from the MarketDB database for a date and asset type
        public DataSet GetExistingValues(DateTime DateValue, string AssetType)
        {
            DataSet dsExistingValues = new DataSet();

            Database db;
            DbCommand getExistingdata;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getExistingdata = db.GetStoredProcCommand("SP_GetExistingPriceValuesForDate");
                db.AddInParameter(getExistingdata, "@DateValue", DbType.DateTime, DateValue);
                db.AddInParameter(getExistingdata, "@AssetType", DbType.String, AssetType);
                
                dsExistingValues = db.ExecuteDataSet(getExistingdata);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MarketDataPullDao.cs:GetExistingValues()");


                object[] objects = new object[2];
                objects[0] = DateValue;
                objects[1] = AssetType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return dsExistingValues;

        }
        //This method inserts data into the MaerketDb Database Temp tables for a particular date and assettype
        public bool InsertDataToTemp(DataSet ds, string AssetType)
        {

            bool IsSuccess = false;
            Database db;
            //DbCommand cmdinsertdata;
            //DbCommand cmdUpdatedata;
            //DbCommand cmdDeletedata;
            db = DatabaseFactory.CreateDatabase("marketdb");
            SqlConnection connection = new SqlConnection(db.ConnectionString);
            SqlBulkCopy bulkcopy = new SqlBulkCopy(connection);
            connection.Open();
            
            try
            {
                
                //if (AssetType == "NSE")
                //{
                //    //cmdinsertdata = db.GetStoredProcCommand("SP_InsertNSEMarketPriceToMarketDB");
                //    //cmdUpdatedata = db.GetStoredProcCommand("SP_UpdateNSEMarketPriceToMarketDB");
                //    //cmdDeletedata = db.GetStoredProcCommand("SP_DeleteNSEMarketPriceToMarketDB");

                //    //db.AddInParameter(cmdinsertdata, "@Symbol", DbType.String,"Symbol", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Series", DbType.String,"Series", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Open_Price", DbType.Double, "Open_Price", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@High_Price", DbType.Double, "High_Price", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Low_Price", DbType.Double, "Low_Price", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Close_Price", DbType.Double, "Close_Price", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Last_Price", DbType.Double, "Last_Price", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Previous_Close", DbType.Double, "Previous_Close", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Total_Trade_Qty", DbType.Double, "Total_Trade_Qty", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Total_Trade_Value", DbType.Double, "Total_Trade_Value", DataRowVersion.Current);
                //    //db.AddInParameter(cmdinsertdata, "@Date", DbType.DateTime,"Date", DataRowVersion.Current);

                //    //db.AddInParameter(cmdUpdatedata, "@Symbol", DbType.String, "Symbol", DataRowVersion.Current);
                //    //db.AddInParameter(cmdDeletedata, "@Symbol", DbType.String, "Symbol", DataRowVersion.Current);




                //    //rowsaffected = db.UpdateDataSet(ds, "NSE", cmdinsertdata, cmdUpdatedata, cmdDeletedata, UpdateBehavior.Standard);
                //}

                if (AssetType == "NSE")
                {
                    bulkcopy.DestinationTableName = "dbo.TempNSEEquitiesMarketData";
                }

                else if (AssetType == "BSE")
                {
                    bulkcopy.DestinationTableName = "dbo.TempBSEEquitiesMarketData";
                }

                else if (AssetType == "AMFI")
                {
                    bulkcopy.DestinationTableName = "dbo.TempAMFIMarketData";

                }
                bulkcopy.WriteToServer(ds.Tables[0]);
                IsSuccess = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MarketDataPullDao.cs:InsertDataToTemp()");


                object[] objects = new object[2];
                objects[0] = ds;
                objects[1] = AssetType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                connection.Close();
            }

            return IsSuccess;
            
        }

        //This method will compare data if existing and will also insert or Update data accordingly

        public int InsertDataintoMarketPriceTable(DateTime date, string AssetType)
        {
            int num_of_records = 0;

            try
            {
                Database db;
                DbCommand cmdinsertdata = null;

                db = DatabaseFactory.CreateDatabase("marketdb");
                if (AssetType == "AMFI")
                {
                    cmdinsertdata = db.GetStoredProcCommand("SP_InsertAMFIDataPrice");
                }

                else if (AssetType == "NSE")
                {
                    cmdinsertdata = db.GetStoredProcCommand("SP_InsertNSEDataPrice");
                }

                else if (AssetType == "BSE")
                {
                    cmdinsertdata = db.GetStoredProcCommand("SP_InsertBSEDataPrice");
                }
                db.AddInParameter(cmdinsertdata, "@Date", DbType.DateTime, date);

                num_of_records = db.ExecuteNonQuery(cmdinsertdata);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MarketDataPullDao.cs:InsertDataintoMarketPriceTable()");


                object[] objects = new object[2];
                objects[0] = date;
                objects[1] = AssetType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return num_of_records;
        }
        
    }
}
