using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DaoWerpAdmin;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoWerpAdmin
{
    public class PriceBo
    {

        public DataSet GetEquityRecord(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetEquityRecord(Flag, StartDate, EndDate, Search, CurrentPage);

        }

        public int GetEquityCount(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetEquityCount(Flag, StartDate, EndDate, Search, CurrentPage);

        }


        public DataSet GetEquitySnapshot(string Flag, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetEquitySnapshot(Flag, Search, CurrentPage);

        }

        public int GetEquityCountSnapshot(string Flag, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetEquityCountSnapshot(Flag, Search, CurrentPage);

        }



        public DataSet GetAMFIRecord(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage, int amfiCode, int schemeCode, int selectAllCode)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFIRecord(Flag, StartDate, EndDate, Search, CurrentPage, amfiCode, schemeCode,selectAllCode);

        }


        public int GetAMFICount(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage, int amfiCode, int schemeCode, int selectAllCode)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFICount(Flag, StartDate, EndDate, Search, CurrentPage,amfiCode,schemeCode,selectAllCode);

        }



        public DataSet GetAMFISnapshot(string Flag, String Search, int CurrentPage, int amfiCode, int schemeCode, int selectAllCode)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFISnapshot(Flag, Search, CurrentPage,amfiCode,schemeCode,selectAllCode);

        }

        public int GetAMFICountSnapshot(string Flag, String Search, int CurrentPage, int amfiCode, int schemeCode,int selectAllCode)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFICountSnapshot(Flag, Search, CurrentPage, amfiCode, schemeCode, selectAllCode);

        }

        public DataTable GetMutualFundList()
        {
            DataTable dtGetMutualFund = new DataTable();
            PriceDao priceDao=new PriceDao();
            try
            {
                dtGetMutualFund = priceDao.GetMutualFundList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMutualFund;
        }
        public DataTable GetAllScehmeList(int amcCode)
        {
            DataTable dtGetAllScehmeList = new DataTable();
            PriceDao priceDao = new PriceDao();
            try
            {
                dtGetAllScehmeList = priceDao.GetAllScehmeList(amcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAllScehmeList;
        }

        public DataSet BindddlMFSubCategory()
        {
            DataSet dsGetSubCategory = new DataSet();
            PriceDao priceDao = new PriceDao();
            try
            {
                dsGetSubCategory = priceDao.BindddlMFSubCategory();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetSubCategory;

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
            DataTable dtMFFund = new DataTable();
            PriceDao PriceObj = new PriceDao();
            try
            {
                dtMFFund = PriceObj.GetMFFundPerformance(amcCode, subCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtMFFund;

        }

        public DataSet GetFactSheetSchemeDetails(int schemePlanId, int month, int year)
        {
            DataSet dsFactsheetschemeDetails = new DataSet();
            PriceDao PriceObj = new PriceDao();
            try
            {
                dsFactsheetschemeDetails = PriceObj.GetFactSheetSchemeDetails(schemePlanId, month, year);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsFactsheetschemeDetails;

        }
     }
}
