using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace DaoWerpAdmin
{
    public class PriceDao
    {

        public DataSet GetEquityRecord(string Flag, DateTime StartDate, DateTime EndDate, String Search)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistory");
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                //db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetEquityCount(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;
            
            {


                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistory");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }


        public DataSet GetEquitySnapshot(string Flag, String Search)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                //db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetEquityCountSnapshot(string Flag, String Search, int CurrentPage)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }


        public DataSet GetAMFISnapshot(string Flag, String Search, int amfiCode, int schemeCode, int selectAllCode)
        {

            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                //db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(Cmd, "@amfiCode", DbType.Int32, amfiCode);
                db.AddInParameter(Cmd, "@schemeCode", DbType.Int32, schemeCode);
                db.AddInParameter(Cmd, "@selectAllCode", DbType.Int32, selectAllCode);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetAMFICountSnapshot(string Flag, String Search, int CurrentPage, int amfiCode, int schemeCode, int selectAllCode)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistorySnapshot");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(Cmd, "@amfiCode", DbType.Int32, amfiCode);
                db.AddInParameter(Cmd, "@schemeCode", DbType.Int32, schemeCode);
                db.AddInParameter(Cmd, "@selectAllCode", DbType.Int32, selectAllCode);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        public DataSet GetAMFIRecord(string Flag, DateTime StartDate, DateTime EndDate, String Search, int amfiCode, int schemeCode, int selectAllCode)
        {

            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistory");
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                //db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(Cmd, "@amfiCode", DbType.Int32, amfiCode);
                db.AddInParameter(Cmd, "@schemeCode", DbType.Int32, schemeCode);
                db.AddInParameter(Cmd, "@selectAllCode", DbType.Int32, selectAllCode);

                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
        }

        public int GetAMFICount(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage, int amfiCode, int schemeCode,int selectAllCode)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMFIPriceHistory");
                db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(Cmd, "@Search", DbType.String, Search);
                db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(Cmd, "@amfiCode", DbType.Int32, amfiCode);
                db.AddInParameter(Cmd, "@schemeCode", DbType.Int32, schemeCode);
                db.AddInParameter(Cmd, "@selectAllCode", DbType.Int32, selectAllCode);
                ds = db.ExecuteDataSet(Cmd);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
        }

        public DataTable GetMutualFundList()
        {
            Database db;
            DbCommand cmdGetMutualFundList;
            DataTable dtGetMutualFundList;
            DataSet dsGetMutualFundList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetMutualFundList = db.GetStoredProcCommand("SP_GetMutualFundList");
                dsGetMutualFundList = db.ExecuteDataSet(cmdGetMutualFundList);
                dtGetMutualFundList = dsGetMutualFundList.Tables[0];
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMutualFundList;
        }

        public DataTable GetAllScehmeList(int amcCode)
        {
            Database db;
            DbCommand cmdGetAllScehmeList;
            DataTable dtGetAllScehmeList;
            DataSet dsGetAllScehmeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetAllScehmeList = db.GetStoredProcCommand("SP_GetAllScehmeList");
                db.AddInParameter(cmdGetAllScehmeList, "@AMC_Code", DbType.String, amcCode);
                dsGetAllScehmeList = db.ExecuteDataSet(cmdGetAllScehmeList);

                dtGetAllScehmeList = dsGetAllScehmeList.Tables[0];
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAllScehmeList;
        }


        public DataSet BindddlMFSubCategory()
        {
            Database db;
            DbCommand cmdGetAllSubCategory; 
            DataSet dsGetAllSubCategory = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAllSubCategory = db.GetStoredProcCommand("SP_BindMFSubCategoryddl");
                dsGetAllSubCategory = db.ExecuteDataSet(cmdGetAllSubCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAllSubCategory;
        }

        /***********************************************Bhoopendra's code for factsheet***************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Flag"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Search"></param>
        /// <param name="CurrentPage"></param>
        /// <returns></returns>
        /// 

        public DataTable GetMFFundPerformance(int amcCode, string subCategory)
        {
            DataSet dsMFFundPerformance;
            DataTable dtMFFundPerformance;
            Database db;
            DbCommand CmdMFFundPerformance;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdMFFundPerformance = db.GetStoredProcCommand("SP_GetMFFundPerformance");
                db.AddInParameter(CmdMFFundPerformance, "@amcCode", DbType.Int32, amcCode);
                //db.AddInParameter(CmdMFFundPerformance, "@selectSchemeCode", DbType.Int32, selectSchemeCode);
                db.AddInParameter(CmdMFFundPerformance, "@subCategory", DbType.String, subCategory);
                //db.AddInParameter(CmdMFFundPerformance, "@returnPeriod", DbType.Int32, returnPeriod);
                //db.AddInParameter(CmdMFFundPerformance, "@conditionType", DbType.Int32, conditionType);
                CmdMFFundPerformance.CommandTimeout = 60 * 60;
                dsMFFundPerformance = db.ExecuteDataSet(CmdMFFundPerformance);
                dtMFFundPerformance = dsMFFundPerformance.Tables[0];               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtMFFundPerformance;

        }

        //public DataTable GetMFFundPerformance(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        //{
        //    DataSet ds;
        //    Database db;
        //    DbCommand Cmd;

        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        Cmd = db.GetStoredProcCommand("SP_GetProductEquityPriceHistory");
        //        db.AddInParameter(Cmd, "@StartDate", DbType.DateTime, StartDate);
        //        db.AddInParameter(Cmd, "@EndDate", DbType.DateTime, EndDate);
        //        db.AddInParameter(Cmd, "@Search", DbType.String, Search);
        //        db.AddInParameter(Cmd, "@Flag", DbType.String, Flag);
        //        db.AddInParameter(Cmd, "CurrentPage", DbType.Int32, CurrentPage);
        //        ds = db.ExecuteDataSet(Cmd);
        //        return ds;
        //    }
        //}

        public DataSet GetFactSheetSchemeDetails(int schemePlanId, int month, int year)
        {
            DataSet dsFactsheetschemeDetails = new DataSet();
            Database dbFactsheetschemeDetails;
            DbCommand CmdFactsheetschemeDetails;

            try
            {
                dbFactsheetschemeDetails = DatabaseFactory.CreateDatabase("wealtherp");
                CmdFactsheetschemeDetails = dbFactsheetschemeDetails.GetStoredProcCommand("SP_GetFactSheetSchemeDetails");
                dbFactsheetschemeDetails.AddInParameter(CmdFactsheetschemeDetails, "@month", DbType.Int16, month);
                dbFactsheetschemeDetails.AddInParameter(CmdFactsheetschemeDetails, "@year", DbType.Int16, year);
                dbFactsheetschemeDetails.AddInParameter(CmdFactsheetschemeDetails, "@SchemePlanCode", DbType.Int16, schemePlanId);
                CmdFactsheetschemeDetails.CommandTimeout = 60 * 60;
                dsFactsheetschemeDetails = dbFactsheetschemeDetails.ExecuteDataSet(CmdFactsheetschemeDetails);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsFactsheetschemeDetails;

        }
        public DataSet GetSchemeListCategorySubCategory(int amcCode, string categoryCode, string subCategory)
        {
            
            DataSet dsSchemeListCategorySubCategory = new DataSet();
            Database dbFactsheetschemeDetails;
            DbCommand CmdSchemeListCategorySubCategory;
            try
            {
                dbFactsheetschemeDetails = DatabaseFactory.CreateDatabase("wealtherp");
                CmdSchemeListCategorySubCategory = dbFactsheetschemeDetails.GetStoredProcCommand("SP_GetSchemeNameOnCategorySubCategory");
                dbFactsheetschemeDetails.AddInParameter(CmdSchemeListCategorySubCategory, "@AmcCode", DbType.Int32, amcCode);
                dbFactsheetschemeDetails.AddInParameter(CmdSchemeListCategorySubCategory, "@CategoryCode", DbType.String, categoryCode);
                dbFactsheetschemeDetails.AddInParameter(CmdSchemeListCategorySubCategory, "@SubCategory", DbType.String, subCategory);
                CmdSchemeListCategorySubCategory.CommandTimeout = 60 * 60;
                dsSchemeListCategorySubCategory = dbFactsheetschemeDetails.ExecuteDataSet(CmdSchemeListCategorySubCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSchemeListCategorySubCategory;
        }
        public DataSet GetNavOverAllCategoryList()
        {
            DataSet dsGetOverAllCategoryList = new DataSet();
            Database db;
            DbCommand CmdGetOverAllCategoryList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdGetOverAllCategoryList = db.GetStoredProcCommand("SP_GetNavOverAllCategoryList");
                dsGetOverAllCategoryList = db.ExecuteDataSet(CmdGetOverAllCategoryList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetOverAllCategoryList;
        }
        public DataSet GetSchemeFromOverAllCategoryList(int amcCode, string categoryCode)
        {
            DataSet dsSchemeFromOverAllCategoryList = new DataSet();
            Database db;
            DbCommand CmdSchemeFromOverAllCategoryList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdSchemeFromOverAllCategoryList = db.GetStoredProcCommand("SP_GetSchemeFromOverAllCategoryList");
                db.AddInParameter(CmdSchemeFromOverAllCategoryList, "@AmcCode", DbType.Int32, amcCode);
                db.AddInParameter(CmdSchemeFromOverAllCategoryList, "@CategoryCode", DbType.String, categoryCode);
                dsSchemeFromOverAllCategoryList = db.ExecuteDataSet(CmdSchemeFromOverAllCategoryList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSchemeFromOverAllCategoryList;
        }
    }
}

