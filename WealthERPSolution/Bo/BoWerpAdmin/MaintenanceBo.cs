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
  public  class MaintenanceBo
    {
        public DataSet GetScripMasterRecords( int CurrentPage, string Flag)
        {
            MaintenanceDao obj = new MaintenanceDao();
            return obj.GetScripMasterRecords( CurrentPage,Flag);

        }

        public int GetScripMasterRecords(string Flag)
        {
            MaintenanceDao obj = new MaintenanceDao();
            return obj.GetScripMasterRecords( Flag);
        }

        public DataSet GetSchemeMasterRecord(int CurrentPage, string Flag)
        {
            MaintenanceDao obj = new MaintenanceDao();
            return obj.GetSchemeMasterRecord(CurrentPage,Flag);
        }

        public int GetSchemeMasterRecord(string Flag)
        {
            MaintenanceDao obj = new MaintenanceDao();
            return obj.GetSchemeMasterRecord(Flag);
        }
    }
}
