using System;
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
        public DataSet GetAllAdviserDuplicateRecords(DateTime FromDate, DateTime ToDate)
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
                //db.AddInParameter(getDuplicateCheckCmd, "@currentPage", DbType.Int32, currentPage);
                //db.AddOutParameter(getDuplicateCheckCmd, "@Count", DbType.Int32, 0);


                getDuplicateCheckCmd.CommandTimeout = 60 * 60;
                dsGetDuplicateRecord = db.ExecuteDataSet(getDuplicateCheckCmd);
                //count = (int)db.GetParameterValue(getDuplicateCheckCmd, "@Count");
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
                //objects[2] = currentPage;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetDuplicateRecord;
        }

        /// <summary>
        /// Delete the duplicate transactions as per the filters 
        /// </summary>
        /// <param name="adviserId">adviser filter</param>
        /// <param name="CommandName">if the command is delete</param>
        /// <param name="deleted">if the operation is success</param>
        /// <param name="cmfaAccountId">the account for which the duplicate is deleted</param>
        /// <returns>true or false</returns>

        public bool DeleteDuplicateTransactionDetailsORFolioDetails(int adviserId, string CommandName, int deleted, int cmfaAccountId, string folioNo)
        {
            bool completed = false;
            Database db;
            DbCommand InsertZoneClusterDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                InsertZoneClusterDetailsCmd = db.GetStoredProcCommand("SPROC_DeleteDuplicateTransactionDetailsORFolioDetails");

                if (!string.IsNullOrEmpty(CommandName))
                    db.AddInParameter(InsertZoneClusterDetailsCmd, "@CommandName", DbType.String, CommandName);
                else
                    db.AddInParameter(InsertZoneClusterDetailsCmd, "@CommandName", DbType.String, DBNull.Value);


                if (!string.IsNullOrEmpty(folioNo))
                    db.AddInParameter(InsertZoneClusterDetailsCmd, "@folioNo", DbType.String, folioNo);
                else
                    db.AddInParameter(InsertZoneClusterDetailsCmd, "@folioNo", DbType.String, DBNull.Value);

                if (cmfaAccountId != 0)
                    db.AddInParameter(InsertZoneClusterDetailsCmd, "@CMFA_AccountId", DbType.Int32, cmfaAccountId);
                else
                    db.AddInParameter(InsertZoneClusterDetailsCmd, "@CMFA_AccountId", DbType.Int32, DBNull.Value);


                db.AddOutParameter(InsertZoneClusterDetailsCmd, "@deleted", DbType.Int32, 5000);


                db.ExecuteNonQuery(InsertZoneClusterDetailsCmd);
                completed = true;


                deleted = Convert.ToInt32(db.GetParameterValue(InsertZoneClusterDetailsCmd, "deleted").ToString());
                if (deleted == 0)
                    completed = false;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:ZoneClusterDetailsAddEditDelete(int adviserId,int rmId,int ZoneId, string Description, string name, string type, int userId, DateTime createdDate, string CommandName)");
                object[] objects = new object[10];
                objects[0] = adviserId;
                objects[1] = CommandName;
                objects[2] = deleted;
                objects[3] = cmfaAccountId;
                objects[4] = folioNo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return completed;
        }


        /// <summary>
        /// This function will get the duplicate foliodetails or the transaction details 
        /// </summary>
        /// <param name="adviserId">filter according to the adviserid or All</param>
        /// <param name="toDate">to transaction date</param>
        /// <param name="fromDate">from transaction date</param>
        /// <param name="isDuplicatesOnly">if only duplicate records are expected</param>
        /// <returns>dataset for duplicate folios or transactions</returns>
        public DataSet GetDuplicateTransactionDetailsOrFolioDetails(string strMonitorFor, string strTypeOfMonitor, int adviserId, DateTime toDate, DateTime fromDate, int isDuplicatesOnly)
        {
            //declare the dataset to hold the data for the duplicate details
            DataSet dsDuplicateTransactionDetailsOrFolioDetails;
            //declare the database
            Database db;
            //declare the command for the performing the operations
            DbCommand getDuplicateTransactionDetailsOrFolioDetailsCmd;
            //check exception 
            try
            {
                //initialize the database
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //initialize the dataset
                getDuplicateTransactionDetailsOrFolioDetailsCmd = db.GetStoredProcCommand("SPROC_GetDuplicateTransactionDetailsOrFolioDetails");
                //add parameter for the adviserid filter or the all filter
                if (adviserId != 0)
                    db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@adviserId", DbType.Int32, adviserId);
                else
                    db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@adviserId", DbType.Int32, DBNull.Value);

                //add parameter for the type of monitor for
                db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@strMonitorFor", DbType.String, strMonitorFor);
                //add parameter for the type of monitor
                db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@strTypeOfMonitor", DbType.String, strTypeOfMonitor);
                //check if the date contain min value which need to discard and null value to be sent
                if (toDate != DateTime.MinValue)
                    //add parameter for the to transaction date               
                    db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@toDate", DbType.Date, toDate);
                else
                    db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@toDate", DbType.Date, DBNull.Value);
                //check if the date contain min value which need to discard and null value to be sent
                if (fromDate != DateTime.MinValue)
                    //add parameter for the from transaction date
                    db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@fromDate", DbType.Date, fromDate);
                else
                    db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@fromDate", DbType.Date, DBNull.Value);

                //add parameter for the records to filter if only duplicate records are to the retrived
                db.AddInParameter(getDuplicateTransactionDetailsOrFolioDetailsCmd, "@isDuplicatesOnly", DbType.Int32, isDuplicatesOnly);
                //check the timeout of the command at runtime
                getDuplicateTransactionDetailsOrFolioDetailsCmd.CommandTimeout = 60 * 60;
                //execution of the command and getting the data from the database
                dsDuplicateTransactionDetailsOrFolioDetails = db.ExecuteDataSet(getDuplicateTransactionDetailsOrFolioDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioBo.cs:GetDuplicateTransactionDetailsOrFolioDetails(int adviserId,DateTime toDate,DateTime fromDate,int isDuplicatesOnly)");
                object[] objects = new object[7];
                objects[0] = strMonitorFor;
                objects[1] = strTypeOfMonitor;
                objects[2] = adviserId;
                objects[3] = toDate;
                objects[4] = fromDate;
                objects[5] = isDuplicatesOnly;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //return the duplicatedetails for folio or transactions
            return dsDuplicateTransactionDetailsOrFolioDetails;
        }

        /// <summary>
        /// To Get all adviser's AUM
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public DataSet GetAllAdviserAUM(DateTime fromdate, DateTime todate, string asset)
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
                //db.AddInParameter(getAumValueCmd, "@currentPage", DbType.Int32, currentPage);
                //db.AddInParameter(getAumValueCmd, "@orgName", DbType.String, orgName);
                db.AddInParameter(getAumValueCmd, "@Assettype", DbType.String, asset);
                //db.AddOutParameter(getAumValueCmd, "@Count", DbType.Int32, 0);
                getAumValueCmd.CommandTimeout = 60 * 60;
                dsGetAumValue = db.ExecuteDataSet(getAumValueCmd);
                //count = (int)db.GetParameterValue(getAumValueCmd, "@Count");
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
                object[] objects = new object[3];
                objects[0] = fromdate;
                objects[1] = todate;
                //objects[2] = currentPage;
                objects[2] = asset;
                //objects[3] = asset;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAumValue;
        }

        public DataSet GetMfrejectedDetails(DateTime fromdate, DateTime todate)
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

                dsRejectedRecords = db.ExecuteDataSet(getMfrejectedDetailsCmd);
                //count = (int)db.GetParameterValue(getMfrejectedDetailsCmd, "@Count");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo.cs:GetMfrejectedDetails()");
                object[] objects = new object[6];
                objects[0] = fromdate;
                objects[1] = todate;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsRejectedRecords;
        }

        /// <summary>
        /// Delete (selected) duplicate records from net position Table
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="accountId"></param>
        /// <param name="netHolding"></param>
        /// <param name="schemeCode"></param>
        /// <param name="ValuationDate"></param>
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
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo.cs:DeleteDuplicateRecord()");
                object[] objects = new object[5];
                objects[0] = adviserId;
                objects[1] = accountId;
                objects[2] = netHolding;
                objects[3] = schemeCode;
                objects[4] = ValuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
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
        /// <summary>
        /// Delete all duplicate records since inception.
        /// </summary>
        public void DeleteAllDuplicatesForASuperAdmin()
        {
            Database db;
            DbCommand deleteAllDuplicatescmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAllDuplicatescmd = db.GetStoredProcCommand("SP_DeleteAllDuplicatesForASuperAdmin");
                deleteAllDuplicatescmd.CommandTimeout = 60 * 60;
                db.ExecuteDataSet(deleteAllDuplicatescmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioBo.cs:DeleteAllDuplicatesForASuperAdmin()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public bool SyncSIPtoGoal(int adviserId)
        {
            int affectedRecords = 0;
            Database db;
            DbCommand syncSIPtoGoalCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                syncSIPtoGoalCmd = db.GetStoredProcCommand("SP_SyncSIPToGoalAllocation");
                db.AddInParameter(syncSIPtoGoalCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddOutParameter(syncSIPtoGoalCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(syncSIPtoGoalCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(syncSIPtoGoalCmd, "@IsSuccess").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            if (affectedRecords > 0)
                return true;
            else
                return false;
        }

        public DataTable GetAdviserValuationStatus(string assetType, DateTime valuationDate)
        {
            DataSet dsAdviserList;
            Database db;
            DbCommand getAdviserList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserList = db.GetStoredProcCommand("SP_GetAdviserValuationStatus");
                db.AddInParameter(getAdviserList, "@AssetType", DbType.String, assetType);
                db.AddInParameter(getAdviserList, "@ValuationDate", DbType.Date, valuationDate);
                dsAdviserList = db.ExecuteDataSet(getAdviserList);

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
                object[] objects = new object[3];
                objects[0] = assetType;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAdviserList.Tables[0];
        }
        public DataSet GetNAVPercentage(DateTime navDate, double NavPer)
        {
            DataSet dsGetNAVPer;
            Database db;
            DbCommand getNAVPercmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNAVPercmd = db.GetStoredProcCommand("SP_GetNAVChangePercentage");
                db.AddInParameter(getNAVPercmd, "@navDateToday", DbType.DateTime, navDate);
                //db.AddInParameter(getNAVPercmd, "@currentPage", DbType.Int32, currentPage);
                db.AddInParameter(getNAVPercmd, "@navPercent", DbType.Double, NavPer);
                //db.AddOutParameter(getNAVPercmd, "@Count", DbType.Int32, 0);
                getNAVPercmd.CommandTimeout = 60 * 60;
                dsGetNAVPer = db.ExecuteDataSet(getNAVPercmd);
                //count = (int)db.GetParameterValue(getNAVPercmd, "@Count");
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetNAVPer;
        }
        public DataTable GetAdviserListHavingSIPGoalFunding()
        {
            DataSet dsAdviserListHavingSIPGoalFunding = new DataSet();
            Database db;
            DbCommand AdviserListHavingSIPGoalFundingcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AdviserListHavingSIPGoalFundingcmd = db.GetStoredProcCommand("SP_GetAdviserListHavingSIPGoalFunding");
                dsAdviserListHavingSIPGoalFunding = db.ExecuteDataSet(AdviserListHavingSIPGoalFundingcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserListHavingSIPGoalFunding.Tables[0]; ;
        }

        public DataTable BindAdviserForUpload()
        {
            DataSet dsBindAdviserForUpload = new DataSet();
            Database db;
            DbCommand AdviserListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AdviserListcmd = db.GetStoredProcCommand("SP_GetAllAdvisers");
                db.AddInParameter(AdviserListcmd, "@IsforOldVluation", DbType.Int32, 0);
                dsBindAdviserForUpload = db.ExecuteDataSet(AdviserListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsBindAdviserForUpload.Tables[0];
        }
        public DataSet GetAdviserRmDetails(int adviserId)
        {
            DataSet dsAdviserRmDetails = new DataSet();
            Database db;
            DbCommand AdviserRmDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AdviserRmDetailscmd = db.GetStoredProcCommand("SP_GetAdviserRmDetails");
                db.AddInParameter(AdviserRmDetailscmd, "@AdviserId", DbType.Int32, adviserId);
                dsAdviserRmDetails = db.ExecuteDataSet(AdviserRmDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserRmDetails;
        }
        public bool FolioStartDate(int adviserId)
        {
            DataSet dsFolioStartDate = new DataSet();
            Database db;
            DbCommand FolioStartDatecmd;
            bool isComplete = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                FolioStartDatecmd = db.GetStoredProcCommand("SP_FolioStartDate");
                db.AddInParameter(FolioStartDatecmd, "@AdviserId", DbType.Int32, adviserId);
                dsFolioStartDate = db.ExecuteDataSet(FolioStartDatecmd);
                isComplete = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isComplete;
        }
        public DataSet CheckForBusinessDateAndIsCurrent(DateTime dtTradeDate, out bool isValidDate)
        {
            DataSet ds = new DataSet();
            Database db;
            DbCommand cmd;
            isValidDate = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_CheckForBusinessDateAndIsCurrent");
                db.AddInParameter(cmd, "@dtTradeDate", DbType.DateTime, dtTradeDate);
                ds = db.ExecuteDataSet(cmd);

                if (ds.Tables[0].Rows.Count > 0)
                    isValidDate = true;
                else
                    isValidDate = false;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;

        }
        public DataSet UploadFolioTransactionReconcilation(int adviserId, DateTime fromDate, DateTime toDate)
        {
            DataSet ds = new DataSet();
            Database db;
            DbCommand cmd;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_UploadFolioTransactionReconcilation");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmd, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(cmd, "@ToDate", DbType.DateTime, toDate);

                cmd.CommandTimeout = 60 * 60;
                
                ds = db.ExecuteDataSet(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo:UploadFolioTransactionReconcilation()");

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
