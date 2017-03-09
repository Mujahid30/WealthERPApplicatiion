using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoSuperAdmin
{
    public class SuperAdminOpsVo
    {
        private DateTime pg_date;
        private string pg_price;
        private DateTime pg_fromDate;
        private int pg_id;
        private int pg_currentPage;

        public int Pg_currentPage
        {
            get { return pg_currentPage; }
            set { pg_currentPage = value; }
        }

        public int Pg_id
        {
            get { return pg_id; }
            set { pg_id = value; }
        }

       
        public DateTime Pg_fromDate
        {
            get { return pg_fromDate; }
            set { pg_fromDate = value; }
        }
        private DateTime pg_toDate;

        public DateTime Pg_toDate
        {
            get { return pg_toDate; }
            set { pg_toDate = value; }
        }

        public DateTime PG_Date
        {
            get { return pg_date; }
            set { pg_date = value; }
        }

        public string PG_Price
        {
            get { return pg_price; }
            set { pg_price = value; }
        }
    }
}
