using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoPriceAlerts;

namespace BoPriceAlerts
{
    public class PriceAlertsBo
    {
        public DataSet GetDailyPriceDetails(string dbName)
        {
            DataSet dsPriceDetails = new DataSet();
            PriceAlertsDao priceAlertsDao = new PriceAlertsDao();
            try
            {
                dsPriceDetails = priceAlertsDao.GetDailyPriceDetails(dbName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PriceAlertsBo.cs:GetDailyPriceDetails(string dbName)");


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
