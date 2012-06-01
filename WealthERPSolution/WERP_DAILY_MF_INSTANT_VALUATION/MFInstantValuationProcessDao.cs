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


namespace WERP_MF_INSTANT_VALUATION
{
    public class MFInstantValuationProcessDao
    {

        public DataTable GetMFAccountsForInstantValuation()
        {
            Database db;
            DbCommand getMFAccountsForInstantValuationCmd;
            DataSet dsMFAccountsForInstantValuation;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFAccountsForInstantValuationCmd = db.GetStoredProcCommand("SPROC_GetMFAccountForInstantValuation");                
                dsMFAccountsForInstantValuation = db.ExecuteDataSet(getMFAccountsForInstantValuationCmd);
                
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:GetCustomerTransactionsForBalanceCreation()");


                object[] objects = new object[1];               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsMFAccountsForInstantValuation.Tables[0];

        }

    }
}
