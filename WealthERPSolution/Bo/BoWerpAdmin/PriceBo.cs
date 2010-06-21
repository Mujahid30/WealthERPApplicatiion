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



        public DataSet GetAMFIRecord(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFIRecord(Flag, StartDate, EndDate, Search, CurrentPage);

        }


        public int GetAMFICount(string Flag, DateTime StartDate, DateTime EndDate, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFICount(Flag, StartDate, EndDate, Search, CurrentPage);

        }



        public DataSet GetAMFISnapshot(string Flag, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFISnapshot(Flag, Search, CurrentPage);

        }

        public int GetAMFICountSnapshot(string Flag, String Search, int CurrentPage)
        {
            PriceDao PriceObj = new PriceDao();
            return PriceObj.GetAMFICountSnapshot(Flag, Search,CurrentPage);

        }


     }
}
