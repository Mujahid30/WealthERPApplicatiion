using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoPriceAlerts
{
    public class PriceAlertsDao
    {
        public DataSet GetDailyPriceDetails(string dbName)
        {
            DataSet dsPriceDetails = new DataSet();
            Database db;
            DbCommand getDailyPriceDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase(dbName);
                getDailyPriceDetails = db.GetStoredProcCommand("SP_GetPriceDetails");

                getDailyPriceDetails.CommandTimeout = 60 * 60;
                dsPriceDetails = db.ExecuteDataSet(getDailyPriceDetails);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PriceAlertsDao.cs:GetDailyPriceDetails(string dbName)");


                object[] objects = new object[1];
                objects[0] = dbName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsPriceDetails;
        }
    }
}
