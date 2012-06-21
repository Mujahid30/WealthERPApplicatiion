using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DaoValuation;
using System.Data;
using System.Web.Caching;
using System.Configuration;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WealthERP.Base
{
    public class Caching
    {
        public static DataSet GetDataFromDB(string tableName, string cacheName)
        {
            CalculatorDao calDAO = new CalculatorDao();
            DataSet dsDbData = null;
            try
            {
                if (tableName == "InstrumentTypeMaster")
                    dsDbData = calDAO.GetInstrumentType();
                else if (tableName == "OutputMaster")
                    dsDbData = calDAO.GetOutputType();
                else if (tableName == "InputMaster")
                    dsDbData = calDAO.GetInputForSelOutput();
                else if (tableName == "InterestFrequency")
                    dsDbData = calDAO.GetInterestFrequency();
                else if (tableName == "FormulaMaster")
                    dsDbData = calDAO.GetFormula();

                SqlCacheDependency sqlDep = new SqlCacheDependency(ConfigurationManager.AppSettings["DBName"], tableName);
                HttpContext.Current.Cache.Insert(cacheName, dsDbData, sqlDep);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Caching.cs:GetDataFromDB()");

                FunctionInfo = exBase.AddObject(FunctionInfo, null);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsDbData;
        }
    }
}
