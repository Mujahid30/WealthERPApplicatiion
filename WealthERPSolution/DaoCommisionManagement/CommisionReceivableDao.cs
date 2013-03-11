using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCommisionManagement;

namespace DaoCommisionManagement
{
    public class CommisionReceivableDao
    {
        /// <summary>
        /// Get all the lookup data for Receivable SetUP UI
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>

        public DataSet GetLookupDataForReceivableSetUP(int adviserId)
        {
            Database db;
            DbCommand cmdGetLookupDataForReceivable;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLookupDataForReceivable = db.GetStoredProcCommand("SPROC_GetLookupDataForReceivableSetUP");
                db.AddInParameter(cmdGetLookupDataForReceivable, "@A_AdviserId", DbType.Int32, adviserId);
                ds = db.ExecuteDataSet(cmdGetLookupDataForReceivable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
    }
}
