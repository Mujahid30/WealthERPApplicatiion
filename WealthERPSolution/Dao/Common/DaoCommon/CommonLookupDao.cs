using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace DaoCommon
{
    public class CommonLookupDao
    {
        public DataTable GetMFInstrumentSubCategory(string categoryCode)
        {
            Database db;
            DbCommand cmdGetMFSubCategory;
            DataSet dsSubcategory = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetMFSubCategory = db.GetStoredProcCommand("SPROC_GetMFInstrumentSubCategory");
                db.AddInParameter(cmdGetMFSubCategory, "@InstrumentCategoryCode", DbType.String, categoryCode);
                dsSubcategory = db.ExecuteDataSet(cmdGetMFSubCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetMFInstrumentSubCategory(string categoryCode)");
                object[] objects = new object[1];
                objects[0] = categoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSubcategory.Tables[0];
        }

    }
}
