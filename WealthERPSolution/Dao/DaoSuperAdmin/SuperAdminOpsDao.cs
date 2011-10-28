﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoSuperAdmin; 

namespace DaoSuperAdmin
{
   public class SuperAdminOpsDao
    {
        /// <summary>
        /// To get all adviser  duplicate check.
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>
       public DataSet GetAllAdviserDuplicateRecords(DateTime FromDate, DateTime ToDate, int currentPage, out int count,string adviserId,string OrgName,string folioNo,string schemeName)
       {
           DataSet dsGetDuplicateRecord;
           Database db;
           DbCommand getDuplicateCheckCmd;
           try
           {
               db = DatabaseFactory.CreateDatabase("wealtherp");
               getDuplicateCheckCmd = db.GetStoredProcCommand("SP_GetDuplicateRecordsFromValuation");
               db.AddInParameter(getDuplicateCheckCmd, "@fromDate", DbType.Date, FromDate);
               db.AddInParameter(getDuplicateCheckCmd, "@toDate", DbType.Date, ToDate);
               db.AddInParameter(getDuplicateCheckCmd, "@currentPage", DbType.Int32, currentPage);
               db.AddOutParameter(getDuplicateCheckCmd, "@Count", DbType.Int32, 0);
               if (adviserId != "")
               {
                   db.AddInParameter(getDuplicateCheckCmd, "@AdviserId", DbType.String, adviserId);
               }
               else
                   db.AddInParameter(getDuplicateCheckCmd, "@AdviserId", DbType.String, DBNull.Value);
               if (OrgName != "")
               {
                   db.AddInParameter(getDuplicateCheckCmd, "@orgName", DbType.String, OrgName);
               }
               else
                   db.AddInParameter(getDuplicateCheckCmd, "@orgName", DbType.String, DBNull.Value);
               if (folioNo != "")
               {

                   db.AddInParameter(getDuplicateCheckCmd, "@folioNo", DbType.String, folioNo);
               }
               else
                   db.AddInParameter(getDuplicateCheckCmd, "@folioNo", DbType.String, DBNull.Value);

               if (schemeName != "")
               {

                   db.AddInParameter(getDuplicateCheckCmd, "@schemeName", DbType.String, schemeName);
               }
               else
                   db.AddInParameter(getDuplicateCheckCmd, "@schemeName", DbType.String, DBNull.Value);
             
               getDuplicateCheckCmd.CommandTimeout = 60 * 60;
               dsGetDuplicateRecord = db.ExecuteDataSet(getDuplicateCheckCmd);
               count = (int)db.GetParameterValue(getDuplicateCheckCmd, "@Count");
               if ((dsGetDuplicateRecord == null) && (dsGetDuplicateRecord.Tables.Count == 0))
                   dsGetDuplicateRecord = null;
           }

           catch (BaseApplicationException Ex)
           {
               throw Ex;
           }
           catch (Exception Ex)
           {
               BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
               NameValueCollection FunctionInfo = new NameValueCollection();
               FunctionInfo.Add("Method", "PortfolioBo.cs:GetAllAdviserDuplicateRecords()");
               object[] objects = new object[7];
               objects[0] = FromDate;
               objects[1] = ToDate;
               objects[2] = currentPage;
               objects[3] = adviserId;
               objects[4] = OrgName;
               objects[5] = folioNo;
               objects[6] = schemeName;
               FunctionInfo = exBase.AddObject(FunctionInfo, objects);
               exBase.AdditionalInformation = FunctionInfo;
               ExceptionManager.Publish(exBase);
               throw exBase;
           }
           return dsGetDuplicateRecord;
       }
       /// <summary>
       /// To Get all adviser's AUM
       /// </summary>
       /// <param name="fromdate"></param>
       /// <param name="todate"></param>
       /// <returns></returns>
       public DataSet GetAllAdviserAUM(DateTime fromdate, DateTime todate, int currentPage, out int count, string orgName)
       {
           DataSet dsGetAumValue;
           Database db;
           DbCommand getAumValueCmd;
           try
           {
               db = DatabaseFactory.CreateDatabase("wealtherp");
               getAumValueCmd = db.GetStoredProcCommand("SP_GetAllAdviser'sTotalAUM");
               db.AddInParameter(getAumValueCmd, "@fromDate", DbType.Date, fromdate);
               db.AddInParameter(getAumValueCmd, "@toDate", DbType.Date, todate);
               db.AddInParameter(getAumValueCmd, "@currentPage", DbType.Int32, currentPage);
               db.AddInParameter(getAumValueCmd, "@orgName", DbType.String, orgName);
               db.AddOutParameter(getAumValueCmd, "@Count", DbType.Int32, 0);
               getAumValueCmd.CommandTimeout = 60 * 60;
               dsGetAumValue = db.ExecuteDataSet(getAumValueCmd);
               count = (int)db.GetParameterValue(getAumValueCmd, "@Count");
               if ((dsGetAumValue == null) && (dsGetAumValue.Tables.Count == 0))
                   dsGetAumValue = null;
           }
           catch (BaseApplicationException Ex)
           {
               throw (Ex);
           }
           catch (Exception Ex)
           {
               BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
               NameValueCollection FunctionInfo = new NameValueCollection();
               FunctionInfo.Add("Method", "SuperAdminOpsBo.cs:GetAllAdviserAUM()");
               object[] objects = new object[4];
               objects[0] = fromdate;
               objects[1] = todate;
               objects[2] = currentPage;
               objects[3] = orgName;
               FunctionInfo = exBase.AddObject(FunctionInfo, objects);
               exBase.AdditionalInformation = FunctionInfo;
               ExceptionManager.Publish(exBase);
               throw exBase;
           }
           return dsGetAumValue;
       }
       public DataSet GetMfrejectedDetails(DateTime fromdate, DateTime todate, int currentPage, out int count, string rejectReasoncode, string adviserId, string processId)
       {
           DataSet dsRejectedRecords = new DataSet();
           Database db;
           DbCommand getMfrejectedDetailsCmd;

           try
           {
               db = DatabaseFactory.CreateDatabase("wealtherp");
               getMfrejectedDetailsCmd = db.GetStoredProcCommand("SP_GetMFTransactionRejectedRecords");
               db.AddInParameter(getMfrejectedDetailsCmd, "@fromDate", DbType.Date, fromdate);
               db.AddInParameter(getMfrejectedDetailsCmd, "@toDate", DbType.Date, todate);
               db.AddInParameter(getMfrejectedDetailsCmd, "@currentPage", DbType.Int32, currentPage);
               db.AddOutParameter(getMfrejectedDetailsCmd, "@Count", DbType.Int32, 0);
               if (rejectReasoncode != "")
               {
                   db.AddInParameter(getMfrejectedDetailsCmd, "@RejectCode", DbType.String, rejectReasoncode);
               }
               else
               {
                   db.AddInParameter(getMfrejectedDetailsCmd, "@RejectCode", DbType.String, DBNull.Value);
               }
               if (adviserId != "")
               {
                   db.AddInParameter(getMfrejectedDetailsCmd, "@AdviserId", DbType.String, adviserId);
               }
               else
               {
                   db.AddInParameter(getMfrejectedDetailsCmd, "@AdviserId", DbType.String, DBNull.Value);
               }
               if (processId != "")
               {
                   db.AddInParameter(getMfrejectedDetailsCmd, "@ProcessId", DbType.String, processId);
               }
               else
               {
                   db.AddInParameter(getMfrejectedDetailsCmd, "@ProcessId", DbType.String, DBNull.Value);
               }
               
               dsRejectedRecords = db.ExecuteDataSet(getMfrejectedDetailsCmd);
               count = (int)db.GetParameterValue(getMfrejectedDetailsCmd, "@Count");
           }
           catch (BaseApplicationException Ex)
           {
               throw Ex;
           }
           return dsRejectedRecords;
       }


       public void DeleteDuplicateRecord(int adviserId, int accountId, double netHolding, int schemeCode, DateTime ValuationDate)
       {
          
           Database db;
           DbCommand getDeleteDuplicateRecordCmd;

           try
           {
               db = DatabaseFactory.CreateDatabase("wealtherp");
               getDeleteDuplicateRecordCmd = db.GetStoredProcCommand("SP_DeleteDuplicateNetPosition");
               db.AddInParameter(getDeleteDuplicateRecordCmd, "@adviserId", DbType.Int32, adviserId);
               db.AddInParameter(getDeleteDuplicateRecordCmd, "@accountId", DbType.Int32, accountId);
               db.AddInParameter(getDeleteDuplicateRecordCmd, "@netHolding", DbType.Double, netHolding);
               db.AddInParameter(getDeleteDuplicateRecordCmd, "@schemeCode", DbType.Int32, schemeCode);
               db.AddInParameter(getDeleteDuplicateRecordCmd, "@ValuationDate", DbType.Date, ValuationDate);
               db.ExecuteDataSet(getDeleteDuplicateRecordCmd);
           }
           catch (BaseApplicationException Ex)
           {
               throw Ex;
           }
           
       }

       

       public int InsertAndUpdateGoldPrice(SuperAdminOpsVo productGoldPriceVO)
       {
           Database db;
           DbCommand createUserCmd;
           db = DatabaseFactory.CreateDatabase("wealtherp");
           createUserCmd = db.GetStoredProcCommand("PG_goldPriceInsert");
           try
           {
               db.AddInParameter(createUserCmd, "@pg_Date", DbType.DateTime, productGoldPriceVO.PG_Date);
               db.AddInParameter(createUserCmd, "@pg_Price", DbType.String, productGoldPriceVO.PG_Price);
               return db.ExecuteNonQuery(createUserCmd);
           }
           catch
           {
               throw;
           }
       }

       public DataSet GetDataBetweenDatesForGoldPrice(SuperAdminOpsVo productGoldPriceVO, int productGoldPriceId, int CurrentPage, out int Count)
       //public DataSet GetDataBetweenDatesForGoldPrice(SuperAdminOpsVo productGoldPriceVO)
       {
           DataSet ds;
           Database db;
           DbCommand getPGPDetails;
           db = DatabaseFactory.CreateDatabase("wealtherp");
           getPGPDetails = db.GetStoredProcCommand("SP_ProductGoldPriceBteweenDates");

           try
           {
               db.AddInParameter(getPGPDetails, "@currentPage", DbType.Int32, CurrentPage);
               db.AddInParameter(getPGPDetails, "@pg_id", DbType.String, productGoldPriceVO.Pg_id);
               if (productGoldPriceVO.Pg_fromDate != DateTime.MinValue)
               {
                   db.AddInParameter(getPGPDetails, "@PG_Date_From", DbType.DateTime, productGoldPriceVO.Pg_fromDate);


               }
               else
               {
                   db.AddInParameter(getPGPDetails, "@PG_Date_From", DbType.DateTime, DBNull.Value);

               }
               if (productGoldPriceVO.Pg_toDate != DateTime.MinValue)
               {
                   db.AddInParameter(getPGPDetails, "@PG_Date_To", DbType.DateTime, productGoldPriceVO.Pg_toDate);
               }
               else
               {
                   db.AddInParameter(getPGPDetails, "@PG_Date_To", DbType.DateTime, DBNull.Value);
               }
               db.AddOutParameter(getPGPDetails, "@Count", DbType.Int32, 0);
               ds = db.ExecuteDataSet(getPGPDetails);
               Count = (int)db.GetParameterValue(getPGPDetails, "@Count");

               return ds;
           }
           catch
           {
               throw;
           }
       }

       public DataSet GetGoldPriceAccordingToDate(DateTime txtDateSearch)
       {
           DataSet ds;
           Database db;
           DbCommand getGoldPriceDetailsAccordingToDate;
           db = DatabaseFactory.CreateDatabase("wealtherp");
           getGoldPriceDetailsAccordingToDate = db.GetStoredProcCommand("SP_GetGoldPriceAccordingToDate");

           try
           {
               db.AddInParameter(getGoldPriceDetailsAccordingToDate, "@Pg_date", DbType.DateTime, txtDateSearch);
               ds = db.ExecuteDataSet(getGoldPriceDetailsAccordingToDate);
               return ds;
           }
           catch
           {
               throw;
           }
       }

       public DataSet GetGoldPriceAccordingToID(int productGoldPriceID)
       {
           DataSet ds;
           Database db;
           DbCommand getGoldPriceDetailsAccordingToDate;
           db = DatabaseFactory.CreateDatabase("wealtherp");
           getGoldPriceDetailsAccordingToDate = db.GetStoredProcCommand("SP_GetGoldPriceDetailsAccordingToPGID");

           try
           {
               db.AddInParameter(getGoldPriceDetailsAccordingToDate, "@pg_id", DbType.String, productGoldPriceID);
               ds = db.ExecuteDataSet(getGoldPriceDetailsAccordingToDate);
               return ds;
           }
           catch
           {
               throw;
           }
       }

       public DataSet GetAllGoldPriceDetails()
       {
           DataSet ds;
           Database db;
           DbCommand getAllGoldPriceDetails;
           db = DatabaseFactory.CreateDatabase("wealtherp");
           getAllGoldPriceDetails = db.GetStoredProcCommand("SP_GetAllGoldPriceDetails");

           try
           {
               ds = db.ExecuteDataSet(getAllGoldPriceDetails);
               return ds;
           }
           catch
           {
               throw;
           }
       }



       public int deleteGoldPriceDetails(int productGoldPriceID)
       {
           int i = 0;
           Database db;
           DbCommand deleteGoldPriceDetails;
           db = DatabaseFactory.CreateDatabase("wealtherp");
           deleteGoldPriceDetails = db.GetStoredProcCommand("SP_DeleteGoldPrice");
           try
           {
               db.AddInParameter(deleteGoldPriceDetails, "@Pg_Id", DbType.String, productGoldPriceID);
               i = int.Parse(db.ExecuteNonQuery(deleteGoldPriceDetails).ToString());
               return i;
           }
           catch (Exception ex)
           {
               ex.StackTrace.ToString();
           }
           return i;
       }
       
    }
}
