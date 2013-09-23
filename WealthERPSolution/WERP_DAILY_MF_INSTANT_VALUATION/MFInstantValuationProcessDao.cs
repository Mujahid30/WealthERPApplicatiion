﻿using System;
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


namespace WERP_DAILY_MF_INSTANT_VALUATION
{
    public class MFInstantValuationProcessDao
    {
        public DateTime GetMFLatestValuationDate(string assetType,int adviserId)
        {
            Database db;
            DbCommand getMFLatestValuationDateCmd;
            DateTime valuationDate=DateTime.Now;
            DataSet dsValuationDate=new DataSet();
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFLatestValuationDateCmd = db.GetStoredProcCommand("SP_GetAdviserLatestValuationDateForInstantValuation");
                db.AddInParameter(getMFLatestValuationDateCmd, "@AssetType", DbType.String, assetType);
                db.AddInParameter(getMFLatestValuationDateCmd, "@AdviserId", DbType.String, adviserId);               
                //db.ExecuteNonQuery(getMFLatestValuationDateCmd);
                dsValuationDate = db.ExecuteDataSet(getMFLatestValuationDateCmd);
                valuationDate = Convert.ToDateTime(dsValuationDate.Tables[0].Rows[0][0].ToString());
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

            return valuationDate;

        }

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

        public void UpdateInstantValuationFlag(int instantValuationId,int accountId,int schemeplanCode ,int flag)
        {
            Database db;
            DbCommand updateInstantValuationFlagCmd;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateInstantValuationFlagCmd = db.GetStoredProcCommand("SPROC_UpdateInstantValuationFlag");
                db.AddInParameter(updateInstantValuationFlagCmd, "@CMFA_AccountId", DbType.Int32, accountId);
                db.AddInParameter(updateInstantValuationFlagCmd, "@PASP_SchemePlanCode", DbType.Int32, schemeplanCode);
                db.AddInParameter(updateInstantValuationFlagCmd, "@Flag", DbType.Int16, flag);
                db.AddInParameter(updateInstantValuationFlagCmd, "@MFAIV_Id", DbType.Int32, instantValuationId);
                db.ExecuteNonQuery(updateInstantValuationFlagCmd);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "MFEngineDao.cs:UpdateInstantValuationFlag()");


                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
    }
}
