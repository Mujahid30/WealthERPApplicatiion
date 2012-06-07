using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using VoValuation;
using BoValuation;

namespace WERP_DAILY_MF_INSTANT_VALUATION
{
    public class MFInstantValuationProcessBo
    {
        MFInstantValuationProcessDao mfInstantValuationProcessDao = new MFInstantValuationProcessDao();
        MFInstantValuationBo mfInstantValuationBo = new MFInstantValuationBo();

        public void ProcessMFAccountInstantValuation()
        {
            DataTable dtAccountList = new DataTable();
            int flag;
            dtAccountList = mfInstantValuationProcessDao.GetMFAccountsForInstantValuation();
            foreach (DataRow dr in dtAccountList.Rows)
            {
                try
                {
                    mfInstantValuationBo.ProcessMFAccountScheme(int.Parse(dr["CMFA_AccountId"].ToString()), int.Parse(dr["PASP_SchemePlanCode"].ToString()),DateTime.Today.Date.AddDays(-1));
                    flag = 2;
                    mfInstantValuationProcessDao.UpdateInstantValuationFlag(int.Parse(dr["CMFA_AccountId"].ToString()), int.Parse(dr["PASP_SchemePlanCode"].ToString()),flag);
                    //Update the Account details As Processed and Ideal
                }
                catch (Exception Ex)
                {
                    flag = 0;
                    mfInstantValuationProcessDao.UpdateInstantValuationFlag(int.Parse(dr["CMFA_AccountId"].ToString()), int.Parse(dr["PASP_SchemePlanCode"].ToString()), flag);
                     
                }

            }

        }

    }
}
