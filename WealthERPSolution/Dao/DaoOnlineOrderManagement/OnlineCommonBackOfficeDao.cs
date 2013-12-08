using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.Practices.EnterpriseLibrary.Data; 
using System.Collections.Specialized;

namespace DaoOnlineOrderManagement
{
    public class OnlineCommonBackOfficeDao
    {
        public DataSet GetSourceCode()
        {
            Database db;
            DbCommand cmdGetSourceCode;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSourceCode = db.GetStoredProcCommand("SPROC_GetExternalSourceCode");
                ds = db.ExecuteDataSet(cmdGetSourceCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetSourceCode()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public DataSet GetInternalHeaderMapping(string type)
        {
            Database db;
            DataSet dsGetInternalHeaderMapping;
            DbCommand cmdGetInternalHeaderMapping;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetInternalHeaderMapping = db.GetStoredProcCommand("SPROC_GetInternalHeaderMapping");
                db.AddInParameter(cmdGetInternalHeaderMapping, "@type", DbType.String, type);
                dsGetInternalHeaderMapping = db.ExecuteDataSet(cmdGetInternalHeaderMapping);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetInternalHeaderMapping()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetInternalHeaderMapping;
        }
    }
}
