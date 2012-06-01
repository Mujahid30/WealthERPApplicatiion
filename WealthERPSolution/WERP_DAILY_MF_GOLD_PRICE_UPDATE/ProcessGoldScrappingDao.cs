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


namespace WERP_DAILY_MF_GOLD_PRICE_UPDATE
{
    public class ProcessGoldScrappingDao
    {
        public void UpdateGoldPrice(decimal goldPrice, DateTime goldPriceDate)
        {
            Database db;
            DbCommand goldPriceUpdateCmd;
            

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                goldPriceUpdateCmd = db.GetStoredProcCommand("PG_goldPriceInsert");
                db.AddInParameter(goldPriceUpdateCmd, "@pg_Date", DbType.DateTime, goldPriceDate);
                db.AddInParameter(goldPriceUpdateCmd, "@pg_Price", DbType.Decimal, goldPrice);
                db.ExecuteNonQuery(goldPriceUpdateCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProcessGoldScrappingDao.cs:UpdateGoldPrice()");


                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

           
        }
    }
}
