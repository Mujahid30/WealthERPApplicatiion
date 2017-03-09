using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using DaoOfflineOrderManagement;

namespace BoOfflineOrderManagement
{
    public class OfflineMFOrderBackOfficeBo
    {
        OfflineMFOrderBackOfficeDao OfflineMFOrderBackOfficeDao = new OfflineMFOrderBackOfficeDao();
         public DataTable GetStatusCode()
        {
            DataTable dtGetStatusCode;
            try
            {
                dtGetStatusCode = OfflineMFOrderBackOfficeDao.GetStatusCode();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetStatusCode;
         }
         public DataSet GetOfflineSIPSummaryBookMIS(int AmcCode, DateTime dtFrom, DateTime dtTo,int adviserId)
         {
             DataSet dsGetOfflineSIPSummaryBookMIS = null;
             try
             {
                 dsGetOfflineSIPSummaryBookMIS = OfflineMFOrderBackOfficeDao.GetOfflineSIPSummaryBookMIS(AmcCode, dtFrom, dtTo, adviserId);
             }
             catch (BaseApplicationException Ex)
             {
                 throw Ex;
             }
             return dsGetOfflineSIPSummaryBookMIS;
         }
    }
}
