using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoOnlineOrderManagemnet;
using System.Configuration;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer;




namespace DaoOnlineOrderManagement
{
    public class OnlineNCDBackOfficeDao
    {
        string allotmentDataTable;

        public DataSet GetExtSource(string product, int issueId)
        {
            DataSet dsExtSource;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_BindExtSource");
                db.AddInParameter(dbCommand, "@product", DbType.String, product);
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                dsExtSource = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetExtSource()");
                object[] objects = new object[2];
                objects[1] = product;
                objects[2] = issueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtSource;
        }

        public DataSet GetIssueDetails(int issueId, int adviserId)
        {
            DataSet dsIssueDetails;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetIssueDetails");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, adviserId);
                dsIssueDetails = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetIssueDetails()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsIssueDetails;
        }

        public string GetExtractStepCode(int fileId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            string stepCode;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetExtractStepCode");
                db.AddInParameter(dbCommand, "@fileTypeId", DbType.Int32, fileId);
                stepCode = db.ExecuteScalar(dbCommand).ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetExtractStepCode()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return stepCode;
        }

        public void IsIssueAlloted(int issueId, ref string result)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_CheckIssueAllomentDate");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@tableName", DbType.String, allotmentDataTable);
                db.AddOutParameter(dbCommand, "@IsAlloted", DbType.Int16, 0);
                db.ExecuteNonQuery(dbCommand);
                if (db.GetParameterValue(dbCommand, "@IsAlloted").ToString() != string.Empty)
                {
                    result = db.GetParameterValue(dbCommand, "@IsAlloted").ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:IsIssueAlloted()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void IsSameSubTypeCatAttchedtoSeries(string cat, int issueId, ref string result)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_IsSameSubTypeCatAttchedtoSeries");
                db.AddInParameter(dbCommand, "@cat", DbType.String, cat);
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddOutParameter(dbCommand, "@result", DbType.String, 100);

                if (db.ExecuteNonQuery(dbCommand) != 0)
                {
                    result = db.GetParameterValue(dbCommand, "@result").ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:IsSameSubTypeCatAttchedtoSeries()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        public void AttchingSameSubtypeCattoSeries(int issueId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_AttchingSameSubtypeCattoSeries");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);

                db.ExecuteNonQuery(dbCommand);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }


        }




        public int IsValidBidRange(int issueId, double minBidAmt, double MaxBidAmt)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            int isExist = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_IsValidBidRange");
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@MinBidAmt", DbType.Decimal, minBidAmt);
                db.AddInParameter(dbCommand, "@MaxBidAmt", DbType.Decimal, MaxBidAmt);
                db.AddOutParameter(dbCommand, "@isExist", DbType.Int32, 10);
                if (db.ExecuteScalar(dbCommand) == null)
                    isExist = 0;
                else if (Convert.ToInt32(db.ExecuteScalar(dbCommand).ToString()) == 0)
                {
                    isExist = 0;
                }
                else
                    isExist = 1;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetExtractStepCode()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isExist;
        }



        public string GetEligibleIssueDelete(int catSubTypeId, int catId, int seriesId, int IssueId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            string isEligible = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetEligibleIssueDelete");
                db.AddInParameter(dbCommand, "@CatSubTypeId", DbType.Int32, catSubTypeId);
                db.AddInParameter(dbCommand, "@CatId", DbType.Int32, catId);
                db.AddInParameter(dbCommand, "@SeriesId", DbType.Int32, seriesId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, IssueId);
                db.AddOutParameter(dbCommand, "@IsAvailble", DbType.String, 100);
                if (db.ExecuteScalar(dbCommand) == null)
                    isEligible = string.Empty;
                else
                    isEligible = db.ExecuteScalar(dbCommand).ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetExtractStepCode()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isEligible;
        }

        public int GetSeriesSequence(int issueId, int adviserId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            int DupseqNo = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSeriesSequence");
                db.AddInParameter(dbCommand, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);

                DupseqNo = Convert.ToInt32(db.ExecuteScalar(dbCommand).ToString());
                //if (db.ExecuteNonQuery(dbCommand) != 0)
                //{
                //    seqNo = Convert.ToInt32(db.GetParameterValue(dbCommand, "CO_OrderId").ToString());
                //}

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:ChekSeriesSequence()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return DupseqNo;
        }

        public int ChekSeriesSequence(int seqNo, int issueId, int adviserId, int seriesId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            int IsseqNoExist = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_ChekSeriesSequence");
                db.AddInParameter(dbCommand, "@SequenceNo", DbType.Int32, seqNo);
                db.AddInParameter(dbCommand, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@seriesId", DbType.Int32, seriesId);

                if (db.ExecuteScalar(dbCommand) != null)
                    IsseqNoExist = Convert.ToInt32(db.ExecuteScalar(dbCommand).ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:ChekSeriesSequence()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsseqNoExist;
        }

        public DataSet GetAdviserIssueList(DateTime date, int type, string product, int adviserId)
        {
            DataSet dsIssueDetails;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAdviserIssueList");
                db.AddInParameter(dbCommand, "@date", DbType.Date, date);
                db.AddInParameter(dbCommand, "@type", DbType.Int32, type);
                db.AddInParameter(dbCommand, "@product", DbType.String, product);
                db.AddInParameter(dbCommand, "@adviserId", DbType.String, adviserId);
                dbCommand.CommandTimeout = 60 * 60;
                dsIssueDetails = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetAdviserIssueList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsIssueDetails;
        }
        public DataSet GetAdviserIssueListClosed(string product, int adviserId)
        {
            DataSet dsIssueDetails;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAdviserClosedIssueList");
                db.AddInParameter(dbCommand, "@product", DbType.String, product);
                db.AddInParameter(dbCommand, "@adviserId", DbType.String, adviserId);
                dbCommand.CommandTimeout = 60 * 60;
                dsIssueDetails = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetAdviserIssueList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsIssueDetails;
        }

        public bool UpdateOnlineEnablement(int issueId)
        {
            bool result = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_UpdateOnlineEnablement");
                db.AddInParameter(createCmd, "@issueId", DbType.Int32, issueId);
                db.ExecuteNonQuery(createCmd);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                result = false;
                throw Ex;
            }
            return result;
        }

        public int UpdateIssue(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            int issueId;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_UpdateIssueMaster");
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, onlineNCDBackOfficeVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentSubCategoryCode", DbType.String, onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode);
                db.AddInParameter(createCmd, "@AIM_IssueName", DbType.String, onlineNCDBackOfficeVo.IssueName);
                db.AddInParameter(createCmd, "@PFIIM_IssuerId", DbType.String, onlineNCDBackOfficeVo.IssuerId);
                db.AddInParameter(createCmd, "@AIM_InitialChequeNo", DbType.String, onlineNCDBackOfficeVo.InitialChequeNo);
                db.AddInParameter(createCmd, "@AIM_FaceValue", DbType.Decimal, onlineNCDBackOfficeVo.FaceValue);
                db.AddInParameter(createCmd, "@FloorPrice", DbType.Decimal, onlineNCDBackOfficeVo.FloorPrice);
                db.AddInParameter(createCmd, "@FixedPrice", DbType.Decimal, onlineNCDBackOfficeVo.FixedPrice);
                db.AddInParameter(createCmd, "@AIM_ModeOfIssue", DbType.String, onlineNCDBackOfficeVo.ModeOfIssue);
                db.AddInParameter(createCmd, "@AIM_ModeOfTrading", DbType.String, onlineNCDBackOfficeVo.ModeOfTrading);
                db.AddInParameter(createCmd, "@AIM_OpenDate", DbType.Date, onlineNCDBackOfficeVo.OpenDate);
                db.AddInParameter(createCmd, "@AIM_CloseDate", DbType.Date, onlineNCDBackOfficeVo.CloseDate);
                db.AddInParameter(createCmd, "@AIM_OpenTime", DbType.Time, onlineNCDBackOfficeVo.OpenTime);
                db.AddInParameter(createCmd, "@AIM_CloseTime", DbType.Time, onlineNCDBackOfficeVo.CloseTime);
                //db.AddInParameter(createCmd, "@IssueRevis", DbType.Date, onlineNCDBackOfficeVo.IssueRevis);
                db.AddInParameter(createCmd, "@TradingLot", DbType.Int32, onlineNCDBackOfficeVo.TradingLot);
                db.AddInParameter(createCmd, "@BiddingLot", DbType.Int32, onlineNCDBackOfficeVo.BiddingLot);
                db.AddInParameter(createCmd, "@AIM_MinApplicationSize", DbType.Int32, onlineNCDBackOfficeVo.MinApplicationSize);
                db.AddInParameter(createCmd, "@IsPrefix", DbType.Int32, onlineNCDBackOfficeVo.IsPrefix);
                db.AddInParameter(createCmd, "@AIM_TradingInMultipleOf", DbType.Int32, onlineNCDBackOfficeVo.TradingInMultipleOf);
                //db.AddInParameter(createCmd, "@AIM_ListedInExchange", DbType.String, onlineNCDBackOfficeVo.ListedInExchange);
                db.AddInParameter(createCmd, "@AIM_BankName", DbType.String, onlineNCDBackOfficeVo.BankName);
                db.AddInParameter(createCmd, "@AIM_BankBranch", DbType.String, onlineNCDBackOfficeVo.BankBranch);
                db.AddInParameter(createCmd, "@AIM_PutCallOption", DbType.String, onlineNCDBackOfficeVo.PutCallOption);
                db.AddOutParameter(createCmd, "@AIM_IssueId", DbType.Int32, 0);
                db.AddInParameter(createCmd, "@FromRange", DbType.Int64, onlineNCDBackOfficeVo.FromRange);
                db.AddInParameter(createCmd, "@ToRange", DbType.Int64, onlineNCDBackOfficeVo.ToRange);
                db.AddInParameter(createCmd, "@IsActive", DbType.Int32, onlineNCDBackOfficeVo.IsActive);
                db.AddInParameter(createCmd, "@IsNominationRequired", DbType.Int32, onlineNCDBackOfficeVo.IsNominationRequired);

                db.AddInParameter(createCmd, "@IsListedinBSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinBSE);
                db.AddInParameter(createCmd, "@IsListedinNSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinNSE);
                db.AddInParameter(createCmd, "@BSECode", DbType.String, onlineNCDBackOfficeVo.BSECode);
                db.AddInParameter(createCmd, "@NSECode", DbType.String, onlineNCDBackOfficeVo.NSECode);
                db.AddInParameter(createCmd, "@Rating", DbType.String, onlineNCDBackOfficeVo.Rating);


                db.AddInParameter(createCmd, "@IsBookBuilding", DbType.Int32, onlineNCDBackOfficeVo.IsBookBuilding);
                db.AddInParameter(createCmd, "@BookBuildingPercentage", DbType.Double, onlineNCDBackOfficeVo.BookBuildingPercentage);
                db.AddInParameter(createCmd, "@CapPrice", DbType.Double, onlineNCDBackOfficeVo.CapPrice);
                db.AddInParameter(createCmd, "@NoOfBidAllowed", DbType.Int32, onlineNCDBackOfficeVo.NoOfBidAllowed);
                db.AddInParameter(createCmd, "@RtaSourceCode", DbType.Int32, onlineNCDBackOfficeVo.RtaSourceCode);
                db.AddInParameter(createCmd, "@MaxQty", DbType.Int32, onlineNCDBackOfficeVo.MaxQty);
                db.AddInParameter(createCmd, "@IssueSizeQty", DbType.Int32, onlineNCDBackOfficeVo.IssueSizeQty);
                db.AddInParameter(createCmd, "@IssueSizeAmt", DbType.Decimal, onlineNCDBackOfficeVo.IssueSizeAmt);
                db.AddInParameter(createCmd, "@issueID", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                db.AddInParameter(createCmd, "@Tradableexchange", DbType.Int32, onlineNCDBackOfficeVo.TradableExchange);
                db.AddInParameter(createCmd, "@Subbrokercode", DbType.String, onlineNCDBackOfficeVo.Subbrokercode);
                db.AddInParameter(createCmd, "@RegistrarAddress", DbType.String, onlineNCDBackOfficeVo.RegistrarAddress);
                db.AddInParameter(createCmd, "@RegistrarTelNo", DbType.String, onlineNCDBackOfficeVo.RegistrarTelNo);
                db.AddInParameter(createCmd, "@RegistrarFaxNo", DbType.String, onlineNCDBackOfficeVo.RegistrarFaxNo);
                db.AddInParameter(createCmd, "@RegistrarGrievenceEmail", DbType.String, onlineNCDBackOfficeVo.RegistrarGrievenceEmail);
                db.AddInParameter(createCmd, "@RegistrarWebsite", DbType.String, onlineNCDBackOfficeVo.RegistrarWebsite);
                db.AddInParameter(createCmd, "@ISINNumber", DbType.String, onlineNCDBackOfficeVo.ISINNumber);
                db.AddInParameter(createCmd, "@RegistrarContactPerson", DbType.String, onlineNCDBackOfficeVo.RegistrarContactPerson);

                if (onlineNCDBackOfficeVo.AllotmentDate != DateTime.MinValue)
                {
                    db.AddInParameter(createCmd, "@AllotmentDate", DbType.Date, onlineNCDBackOfficeVo.AllotmentDate);
                }
                else
                {
                    db.AddInParameter(createCmd, "@AllotmentDate", DbType.Date, DBNull.Value);
                }
                if (onlineNCDBackOfficeVo.IssueRevis != DateTime.MinValue)
                {
                    db.AddInParameter(createCmd, "@RevisionDate", DbType.Date, onlineNCDBackOfficeVo.IssueRevis);
                }
                else
                {
                    db.AddInParameter(createCmd, "@RevisionDate", DbType.Date, DBNull.Value);
                }
                if (onlineNCDBackOfficeVo.CutOffTime != DateTime.MinValue)
                    db.AddInParameter(createCmd, "@CutOffTime", DbType.Time, onlineNCDBackOfficeVo.CutOffTime);
                else
                    db.AddInParameter(createCmd, "@CutOffTime", DbType.Time, DBNull.Value);

                db.AddInParameter(createCmd, "@MultipleApplicationAllowed", DbType.Int32, onlineNCDBackOfficeVo.MultipleApplicationAllowed);
                db.AddInParameter(createCmd, "@IsCancelAllowed", DbType.Int32, onlineNCDBackOfficeVo.IsCancelAllowed);
                db.AddInParameter(createCmd, "@Syndicateid", DbType.Int32, onlineNCDBackOfficeVo.syndicateId);
                db.AddInParameter(createCmd, "@Broker", DbType.Int32, onlineNCDBackOfficeVo.broker);
                db.AddInParameter(createCmd, "@BusinessId", DbType.Int32, onlineNCDBackOfficeVo.BusinessChannelId);
                if (onlineNCDBackOfficeVo.OfflineCutOffTime != DateTime.MinValue)
                    db.AddInParameter(createCmd, "@OfflineCutOffTime", DbType.Time, onlineNCDBackOfficeVo.OfflineCutOffTime);
                else
                    db.AddInParameter(createCmd, "@OfflineCutOffTime", DbType.Time, DBNull.Value);
                 
                db.AddInParameter(createCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@applicationBank", DbType.String, onlineNCDBackOfficeVo.applicationBank);
                issueId = db.ExecuteNonQuery(createCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return issueId;
        }

        public DataSet GetSeriesCategories(int issuerId, int issueId, int seriesId)
        {
            DataSet dsGetSeriesCategories;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSeriesCategories");
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@seriesId", DbType.Int32, seriesId);
                dsGetSeriesCategories = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSeriesCategories()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = issuerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSeriesCategories;
        }

        public DataSet GetAplRanges()
        {
            DataSet dsGetAplRanges;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAplRanges");
                dsGetAplRanges = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAplRanges;
        }

        public DataSet GetIssuer(string category)
        {
            DataSet dsGetIssuer;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetIssuer");
                db.AddInParameter(dbCommand, "@category", DbType.String, category);
                dsGetIssuer = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetIssuer;
        }

        public DataSet GetSyndicateMaster()
        {
            DataSet dsGetSeriesCategories;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetIssuer");
                dsGetSeriesCategories = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSeriesCategories;
        }

        public int CreateUpdateDeleteIssuer(int issuerId, string issuerCode, string issuerName, string commandType)
        {
            int issueId;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateUpdateDeletetIssuer");
                db.AddInParameter(createCmd, "@CommandType", DbType.String, commandType);
                db.AddInParameter(createCmd, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(createCmd, "@IssuerName", DbType.String, issuerName);
                db.AddInParameter(createCmd, "@IssuerCode", DbType.String, issuerCode);

                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;

                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public int GetValidateFrom(int fromRange, int adviserId, int issueId, int formRangeId, ref string status)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_GetValidateFrom");
                db.AddInParameter(createCmd, "@FromRange", DbType.String, fromRange);
                db.AddInParameter(createCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(createCmd, "@issueId", DbType.String, issueId);
                db.AddInParameter(createCmd, "@formRangeId", DbType.String, formRangeId);
                db.AddOutParameter(createCmd, "@status", DbType.String, 500);

                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    status = db.GetParameterValue(createCmd, "status").ToString();
                    return 1;
                }
                else
                {
                    return 0;

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public int CreateUpdateDeleteAplicationNos(int fromRange, int toRange, int adviserId, int issueId, int formRangeId, string commandType, ref string status)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateUpdateDeletetAplicationNos");
                db.AddInParameter(createCmd, "@FromRange", DbType.String, fromRange);
                db.AddInParameter(createCmd, "@ToRange", DbType.String, toRange);
                db.AddInParameter(createCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(createCmd, "@issueId", DbType.String, issueId);
                db.AddInParameter(createCmd, "@formRangeId", DbType.String, formRangeId);
                db.AddInParameter(createCmd, "@commandType", DbType.String, commandType);
                db.AddOutParameter(createCmd, "@status", DbType.String, 500);


                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    status = db.GetParameterValue(createCmd, "status").ToString();

                    return 1;
                }
                else
                {
                    return 0;

                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public int CreateUpdateDeleteSyndicateMaster(int syndicateId, string syndicateCode, string syndicateName, string commandType)
        {
            int issueId;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateUpdateDeleteViewSyndicateMaster");
                db.AddInParameter(createCmd, "@CommandType", DbType.String, commandType);
                db.AddInParameter(createCmd, "@SyndicateId", DbType.Int32, syndicateId);
                db.AddInParameter(createCmd, "@SyndicateCode", DbType.String, syndicateCode);
                db.AddInParameter(createCmd, "@SyndicateName", DbType.String, syndicateName);

                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;

                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public int CreateIssue(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int adviserId, int userID)
        {
            int issueId;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateIssueMaster");
                //db.AddInParameter(createCmd, "@PAG_AssetGroupCode", DbType.String, onlineNCDBackOfficeVo.AssetGroupCode);
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, onlineNCDBackOfficeVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentSubCategoryCode", DbType.String, onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode);
                db.AddInParameter(createCmd, "@AIM_IssueName", DbType.String, onlineNCDBackOfficeVo.IssueName);
                db.AddInParameter(createCmd, "@PFIIM_IssuerId", DbType.String, onlineNCDBackOfficeVo.IssuerId);
                db.AddInParameter(createCmd, "@AIM_InitialChequeNo", DbType.String, onlineNCDBackOfficeVo.InitialChequeNo);
                db.AddInParameter(createCmd, "@AIM_FaceValue", DbType.Decimal, onlineNCDBackOfficeVo.FaceValue);
                db.AddInParameter(createCmd, "@FloorPrice", DbType.Decimal, onlineNCDBackOfficeVo.FloorPrice);
                db.AddInParameter(createCmd, "@FixedPrice", DbType.Decimal, onlineNCDBackOfficeVo.FixedPrice);
                db.AddInParameter(createCmd, "@AIM_ModeOfIssue", DbType.String, onlineNCDBackOfficeVo.ModeOfIssue);
                db.AddInParameter(createCmd, "@AIM_ModeOfTrading", DbType.String, onlineNCDBackOfficeVo.ModeOfTrading);
                db.AddInParameter(createCmd, "@AIM_OpenDate", DbType.Date, onlineNCDBackOfficeVo.OpenDate);
                db.AddInParameter(createCmd, "@AIM_CloseDate", DbType.Date, onlineNCDBackOfficeVo.CloseDate);
                if (onlineNCDBackOfficeVo.IssueRevis != DateTime.MinValue)
                {
                    db.AddInParameter(createCmd, "@issuerevisiondate", DbType.Date, onlineNCDBackOfficeVo.IssueRevis);
                }
                else
                {
                    db.AddInParameter(createCmd, "@issuerevisiondate", DbType.Date, DBNull.Value);
                }
                if (onlineNCDBackOfficeVo.AllotmentDate != DateTime.MinValue)
                {
                    db.AddInParameter(createCmd, "@AllotmentDate", DbType.Date, onlineNCDBackOfficeVo.AllotmentDate);
                }
                else
                {
                    db.AddInParameter(createCmd, "@AllotmentDate", DbType.Date, DBNull.Value);
                }
                db.AddInParameter(createCmd, "@AIM_OpenTime", DbType.Time, onlineNCDBackOfficeVo.OpenTime);
                db.AddInParameter(createCmd, "@AIM_CloseTime", DbType.Time, onlineNCDBackOfficeVo.CloseTime);
                //db.AddInParameter(createCmd, "@IssueRevis", DbType.Date, onlineNCDBackOfficeVo.IssueRevis);
                db.AddInParameter(createCmd, "@TradingLot", DbType.Int32, onlineNCDBackOfficeVo.TradingLot);
                db.AddInParameter(createCmd, "@BiddingLot", DbType.Int32, onlineNCDBackOfficeVo.BiddingLot);
                db.AddInParameter(createCmd, "@AIM_MinApplicationSize", DbType.Int32, onlineNCDBackOfficeVo.MinApplicationSize);
                db.AddInParameter(createCmd, "@IsPrefix", DbType.Int32, onlineNCDBackOfficeVo.IsPrefix);
                db.AddInParameter(createCmd, "@AIM_TradingInMultipleOf", DbType.Int32, onlineNCDBackOfficeVo.TradingInMultipleOf);
                //db.AddInParameter(createCmd, "@AIM_ListedInExchange", DbType.String, onlineNCDBackOfficeVo.ListedInExchange);
                db.AddInParameter(createCmd, "@AIM_BankName", DbType.String, onlineNCDBackOfficeVo.BankName);
                db.AddInParameter(createCmd, "@AIM_BankBranch", DbType.String, onlineNCDBackOfficeVo.BankBranch);
                db.AddInParameter(createCmd, "@AIM_PutCallOption", DbType.String, onlineNCDBackOfficeVo.PutCallOption);
                db.AddOutParameter(createCmd, "@AIM_IssueId", DbType.Int32, 0);
                db.AddInParameter(createCmd, "@FromRange", DbType.Int64, onlineNCDBackOfficeVo.FromRange);
                db.AddInParameter(createCmd, "@ToRange", DbType.Int64, onlineNCDBackOfficeVo.ToRange);
                db.AddInParameter(createCmd, "@IsActive", DbType.Int32, onlineNCDBackOfficeVo.IsActive);
                db.AddInParameter(createCmd, "@IsNominationRequired", DbType.Int32, onlineNCDBackOfficeVo.IsNominationRequired);

                db.AddInParameter(createCmd, "@IsListedinBSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinBSE);
                db.AddInParameter(createCmd, "@IsListedinNSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinNSE);
                db.AddInParameter(createCmd, "@BSECode", DbType.String, onlineNCDBackOfficeVo.BSECode);
                db.AddInParameter(createCmd, "@NSECode", DbType.String, onlineNCDBackOfficeVo.NSECode);
                db.AddInParameter(createCmd, "@Rating", DbType.String, onlineNCDBackOfficeVo.Rating);


                db.AddInParameter(createCmd, "@IsBookBuilding", DbType.Int32, onlineNCDBackOfficeVo.IsBookBuilding);
                db.AddInParameter(createCmd, "@BookBuildingPercentage", DbType.Double, onlineNCDBackOfficeVo.BookBuildingPercentage);
                db.AddInParameter(createCmd, "@CapPrice", DbType.Double, onlineNCDBackOfficeVo.CapPrice);
                db.AddInParameter(createCmd, "@NoOfBidAllowed", DbType.Int32, onlineNCDBackOfficeVo.NoOfBidAllowed);
                db.AddInParameter(createCmd, "@RtaSourceCode", DbType.Int32, onlineNCDBackOfficeVo.RtaSourceCode);
                db.AddInParameter(createCmd, "@MaxQty", DbType.Int32, onlineNCDBackOfficeVo.MaxQty);
                db.AddInParameter(createCmd, "@IssueSizeQty", DbType.Int32, onlineNCDBackOfficeVo.IssueSizeQty);
                db.AddInParameter(createCmd, "@IssueSizeAmt", DbType.Decimal, onlineNCDBackOfficeVo.IssueSizeAmt);
                //db.AddInParameter(createCmd, "@IsListedinBSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinBSE); 
                //db.AddInParameter(createCmd, "@IsListedinNSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinNSE); 
                //db.AddInParameter(createCmd, "@NSECode", DbType.String, onlineNCDBackOfficeVo.NSECode);
                //db.AddInParameter(createCmd, "@BSECode", DbType.String, onlineNCDBackOfficeVo.BSECode);
                db.AddInParameter(createCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(createCmd, "@Tradableexchane", DbType.Int32, onlineNCDBackOfficeVo.TradableExchange);
                if (onlineNCDBackOfficeVo.CutOffTime==DateTime.MinValue)
                    db.AddInParameter(createCmd, "@CutOffTime", DbType.Time, DBNull.Value);
                else
                    db.AddInParameter(createCmd, "@CutOffTime", DbType.Time, onlineNCDBackOfficeVo.CutOffTime);
                db.AddInParameter(createCmd, "@Subbrokercode", DbType.String, onlineNCDBackOfficeVo.Subbrokercode);

                db.AddInParameter(createCmd, "@RegistrarAddress", DbType.String, onlineNCDBackOfficeVo.RegistrarAddress);
                db.AddInParameter(createCmd, "@RegistrarTelNo", DbType.String, onlineNCDBackOfficeVo.RegistrarTelNo);
                db.AddInParameter(createCmd, "@RegistrarFaxNo", DbType.String, onlineNCDBackOfficeVo.RegistrarFaxNo);
                db.AddInParameter(createCmd, "@RegistrarGrievenceEmail", DbType.String, onlineNCDBackOfficeVo.RegistrarGrievenceEmail);
                db.AddInParameter(createCmd, "@RegistrarWebsite", DbType.String, onlineNCDBackOfficeVo.RegistrarWebsite);
                db.AddInParameter(createCmd, "@ISINNumber", DbType.String, onlineNCDBackOfficeVo.ISINNumber);
                db.AddInParameter(createCmd, "@RegistrarContactPerson", DbType.String, onlineNCDBackOfficeVo.RegistrarContactPerson);
                db.AddInParameter(createCmd, "@SBIRegistationNo", DbType.String, onlineNCDBackOfficeVo.SBIRegistationNo);
                db.AddInParameter(createCmd, "@MultipleApplicationAllowed", DbType.Int32, onlineNCDBackOfficeVo.MultipleApplicationAllowed);
                db.AddInParameter(createCmd, "@IsCancelAllowed", DbType.Int32, onlineNCDBackOfficeVo.IsCancelAllowed);
                db.AddInParameter(createCmd, "@Syndicateid", DbType.Int32, onlineNCDBackOfficeVo.syndicateId);
                db.AddInParameter(createCmd, "@Broker", DbType.Int32, onlineNCDBackOfficeVo.broker);
                db.AddInParameter(createCmd, "@BusinessId", DbType.Int32, onlineNCDBackOfficeVo.BusinessChannelId);
                if (onlineNCDBackOfficeVo.OfflineCutOffTime==DateTime.MinValue)
                    db.AddInParameter(createCmd, "@OfflineCutOffTime", DbType.Time, DBNull.Value);
                else
                    db.AddInParameter(createCmd, "@OfflineCutOffTime", DbType.Time, onlineNCDBackOfficeVo.OfflineCutOffTime);
                db.AddInParameter(createCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@applicationBank", DbType.String, onlineNCDBackOfficeVo.applicationBank);
                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    issueId = Convert.ToInt32(db.GetParameterValue(createCmd, "AIM_IssueId").ToString());
                }
                else
                {
                    issueId = 0;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return issueId;
        }

        public int CreateSeries(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            int seriesId;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateSeries");
                db.AddOutParameter(createCmd, "@SeriesId", DbType.Int32, onlineNCDBackOfficeVo.SeriesId);
                db.AddInParameter(createCmd, "@SeriesSequence", DbType.Int32, onlineNCDBackOfficeVo.SeriesSequence);
                db.AddInParameter(createCmd, "@SeriesName", DbType.String, onlineNCDBackOfficeVo.SeriesName);
                db.AddInParameter(createCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                db.AddInParameter(createCmd, "@IsBuyBackAvailable", DbType.Int32, onlineNCDBackOfficeVo.IsBuyBackAvailable);
                db.AddInParameter(createCmd, "@Tenure", DbType.Int32, onlineNCDBackOfficeVo.Tenure);
                db.AddInParameter(createCmd, "@InterestFrequency", DbType.String, onlineNCDBackOfficeVo.InterestFrequency);
                db.AddInParameter(createCmd, "@InterestType", DbType.String, onlineNCDBackOfficeVo.InterestType);
                db.AddInParameter(createCmd, "@ModeOfTenure", DbType.String, onlineNCDBackOfficeVo.ModeOfTenure);
                db.AddInParameter(createCmd, "@RedemptionApplicable", DbType.Int32, onlineNCDBackOfficeVo.RedemptionApplicable);
                db.AddInParameter(createCmd, "@LockinApplicable", DbType.Int32, onlineNCDBackOfficeVo.LockInApplicable);
                db.AddInParameter(createCmd, "@TenureUnits", DbType.String, onlineNCDBackOfficeVo.TenureUnits);
                db.AddInParameter(createCmd, "@seriesFaceValue", DbType.Double, onlineNCDBackOfficeVo.SeriesFaceValue);
                db.AddInParameter(createCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, userID);
                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    seriesId = Convert.ToInt32(db.GetParameterValue(createCmd, "SeriesId").ToString());
                }
                else
                {
                    seriesId = 0;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return seriesId;
        }

        public bool CreateSeriesCategory(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateSeriesCategories");
                db.AddInParameter(createCmd, "@SeriesId", DbType.Int32, onlineNCDBackOfficeVo.SeriesId);
                db.AddInParameter(createCmd, "@InvestorCatgeoryId", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@DefaultInterestRate", DbType.Double, onlineNCDBackOfficeVo.DefaultInterestRate);
                db.AddInParameter(createCmd, "@AnnualizedYieldUpto", DbType.Double, onlineNCDBackOfficeVo.AnnualizedYieldUpto);
                db.AddInParameter(createCmd, "@RenCpnRate", DbType.Double, onlineNCDBackOfficeVo.RenCpnRate);
                db.AddInParameter(createCmd, "@YieldAtCall", DbType.Double, onlineNCDBackOfficeVo.YieldAtCall);
                db.AddInParameter(createCmd, "@YieldAtBuyBack", DbType.Double, onlineNCDBackOfficeVo.YieldAtBuyBack);
                db.AddInParameter(createCmd, "@RedemptionDate", DbType.String, onlineNCDBackOfficeVo.RedemptionDate);
                db.AddInParameter(createCmd, "@redemptionAmount", DbType.Double, onlineNCDBackOfficeVo.RedemptionAmount);
                db.AddInParameter(createCmd, "@lockinperiod", DbType.Int32, onlineNCDBackOfficeVo.LockInPeriodapplicable);
                db.AddInParameter(createCmd, "@issueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);

                if (db.ExecuteNonQuery(createCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                bResult = false;
                throw Ex;
            }
            return bResult;
        }

        public DataSet GetSeries(int issuerId, int issueId)
        {
            DataSet dsGetSeriesCategories;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSeries");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                dsGetSeriesCategories = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSeries()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = issuerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSeriesCategories;
        }

        public DataSet BindNcdCategory(string type, string catCode)
        {
            DataSet dsGetSeriesCategories;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetNcdCategory");
                db.AddInParameter(dbCommand, "@Type", DbType.String, type);
                db.AddInParameter(dbCommand, "@catCode", DbType.String, catCode);

                dsGetSeriesCategories = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:BindNcdCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSeriesCategories;
        }
        //public DataSet BindNcdSubCategory(string type)
        //{
        //    DataSet dsGetSubCategory;
        //    Microsoft.Practices.EnterpriseLibrary.Data.Database db;
        //    DbCommand dbCommand;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        dbCommand = db.GetStoredProcCommand("SPROC_GetNcdSubCategory");
        //        db.AddInParameter(dbCommand, "@Type", DbType.String, type);
        //        dsGetSubCategory = db.ExecuteDataSet(dbCommand);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:BindNcdSubCategory()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return dsGetSubCategory;
        //}

        public DataSet BindRta()
        {
            DataSet dsGetSeriesCategories;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_BindRta");
                dsGetSeriesCategories = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:BindRta()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSeriesCategories;
        }

        public DataSet GetActiveRange(int adviserId, int issueId)
        {
            DataSet dsgetActiveRange;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetActiveRange");
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                dsgetActiveRange = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetActiveRange()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsgetActiveRange;
        }

        public DataSet GetAllInvestorTypes(int issuerId, int issueId, int categoryId)
        {
            DataSet dsGetSubCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAllInvestorTypes");
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@CatId", DbType.Int32, categoryId);

                dsGetSubCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetAllInvestorTypes()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubCategory;
        }

        public DataSet GetSubCategory(int issuerId, int issueId, int size)
        {
            DataSet dsGetSubCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetInvestorTypes");
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@CatId", DbType.Int32, size);

                //db.AddInParameter(dbCommand, "@size", DbType.Int32, size);

                dsGetSubCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSubCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubCategory;
        }

        public DataSet GetSubCategory1(int issuerId, int issueId, int size)
        {
            DataSet dsGetSubCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetInvestorTypes1");
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@CatId", DbType.Int32, size);

                //db.AddInParameter(dbCommand, "@size", DbType.Int32, size);

                dsGetSubCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSubCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubCategory;
        }
        public DataSet GetEligibleInvestorsCategory(int issuerId, int issueId)
        {
            DataSet dsGetEligibleInvestorsCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetEligibleInvestorsCategory");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);

                dsGetEligibleInvestorsCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetEligibleInvestorsCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetEligibleInvestorsCategory;
        }

        public int CreateCategory(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            int categoryId;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateInvestorsCategory");
                db.AddOutParameter(createCmd, "@InvestorCatgeoryId", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                db.AddInParameter(createCmd, "@InvestorCatgeoryName", DbType.String, onlineNCDBackOfficeVo.CatgeoryName);
                db.AddInParameter(createCmd, "@InvestorCatgeoryDescription", DbType.String, onlineNCDBackOfficeVo.CatgeoryDescription);
                db.AddInParameter(createCmd, "@ChequePayableTo", DbType.String, onlineNCDBackOfficeVo.ChequePayableTo);
                db.AddInParameter(createCmd, "@MInBidAmount", DbType.Double, onlineNCDBackOfficeVo.MInBidAmount);
                db.AddInParameter(createCmd, "@MaxBidAmount", DbType.Double, onlineNCDBackOfficeVo.MaxBidAmount);
                db.AddInParameter(createCmd, "@PriceDiscountType", DbType.String, onlineNCDBackOfficeVo.DiscuountType);
                db.AddInParameter(createCmd, "@PriceDiscountValue", DbType.Decimal, onlineNCDBackOfficeVo.DiscountValue);
                db.AddInParameter(createCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, userID);

                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    categoryId = Convert.ToInt32(db.GetParameterValue(createCmd, "InvestorCatgeoryId").ToString());
                }
                else
                {
                    categoryId = 0;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return categoryId;
        }

        public DataSet GetCategory(int issuerId, int issueId)
        {
            DataSet dsGetSubCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetCategories");
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                dsGetSubCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetCategory()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = issuerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubCategory;
        }

        public int UpdateCategory(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            int categoryId = 0;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_UpdateInvestorsCategory");
                db.AddInParameter(createCmd, "@InvestorCatgeoryId", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                db.AddInParameter(createCmd, "@InvestorCatgeoryName", DbType.String, onlineNCDBackOfficeVo.CatgeoryName);
                db.AddInParameter(createCmd, "@InvestorCatgeoryDescription", DbType.String, onlineNCDBackOfficeVo.CatgeoryDescription);
                db.AddInParameter(createCmd, "@ChequePayableTo", DbType.String, onlineNCDBackOfficeVo.ChequePayableTo);
                db.AddInParameter(createCmd, "@MInBidAmount", DbType.Double, onlineNCDBackOfficeVo.MInBidAmount);
                db.AddInParameter(createCmd, "@MaxBidAmount", DbType.Double, onlineNCDBackOfficeVo.MaxBidAmount);
                db.AddInParameter(createCmd, "@PriceDiscountValue", DbType.Double, onlineNCDBackOfficeVo.DiscountValue);
                db.AddInParameter(createCmd, "@DiscuountType", DbType.String, onlineNCDBackOfficeVo.DiscuountType);
                db.AddInParameter(createCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, userID);
                categoryId = db.ExecuteNonQuery(createCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return categoryId;
        }

        public bool CreateSubTypePerCategory(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateInvestorsSubTypePerCategory");
                db.AddInParameter(createCmd, "@InvestorCatgeoryId ", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@InvestorId", DbType.Int32, onlineNCDBackOfficeVo.LookUpId);
                db.AddInParameter(createCmd, "@InvestorSubTypeCode", DbType.String, onlineNCDBackOfficeVo.SubCatgeoryTypeCode);
                db.AddInParameter(createCmd, "@MinInvestmentAmount", DbType.Double, onlineNCDBackOfficeVo.MinInvestmentAmount);
                db.AddInParameter(createCmd, "@MaxInvestmentAmount", DbType.Double, onlineNCDBackOfficeVo.MaxInvestmentAmount);
                db.AddInParameter(createCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, userID);
                if (db.ExecuteNonQuery(createCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                bResult = false;
                throw Ex;
            }
            return bResult;
        }

        public bool UpdateCategoryDetails(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_UpdateCategoryDetails");
                db.AddInParameter(createCmd, "@InvestorCatgeoryId ", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@InvestorId", DbType.Int32, onlineNCDBackOfficeVo.LookUpId);
                db.AddInParameter(createCmd, "@InvestorSubTypeCode", DbType.String, onlineNCDBackOfficeVo.SubCatgeoryTypeCode);
                db.AddInParameter(createCmd, "@MinInvestmentAmount", DbType.Double, onlineNCDBackOfficeVo.MinInvestmentAmount);
                db.AddInParameter(createCmd, "@MaxInvestmentAmount", DbType.Double, onlineNCDBackOfficeVo.MaxInvestmentAmount);
                db.AddInParameter(createCmd, "@SubCategoryId", DbType.Double, onlineNCDBackOfficeVo.SubCatgeoryId);
                db.AddInParameter(createCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(createCmd, "@CreatedBy", DbType.Int32, userID);

                if (db.ExecuteNonQuery(createCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                bResult = false;
                throw Ex;
            }
            return bResult;
        }

        //public bool CreateSubTypePerCategory(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        //{
        //    bool bResult = false;
        //    Database db;
        //    DbCommand createCmd;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        createCmd = db.GetStoredProcCommand("SPROC_CreateInvestorsInvestorSubTypePerCategory");
        //        db.AddInParameter(createCmd, "@InvestorCatgeoryId ", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
        //        db.AddInParameter(createCmd, "@InvestorId", DbType.Int32, onlineNCDBackOfficeVo.LookUpId);
        //        db.AddInParameter(createCmd, "@InvestorSubTypeCode", DbType.String, onlineNCDBackOfficeVo.SubCatgeoryTypeCode);
        //        db.AddInParameter(createCmd, "@MinInvestmentAmount", DbType.Int32, onlineNCDBackOfficeVo.MinInvestmentAmount);
        //        db.AddInParameter(createCmd, "@MaxInvestmentAmount", DbType.Int32, onlineNCDBackOfficeVo.MaxInvestmentAmount);

        //        if (db.ExecuteNonQuery(createCmd) != 0)
        //            bResult = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        bResult = false;
        //        throw Ex;
        //    }
        //    return bResult;
        //}

        public int UpdateSeries(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            int seriesId;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand updateCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SPROC_UpdateSeries");
                db.AddInParameter(updateCmd, "@SeriesId", DbType.Int32, onlineNCDBackOfficeVo.SeriesId);
                db.AddInParameter(updateCmd, "@SeriesSequence", DbType.Int32, onlineNCDBackOfficeVo.SeriesSequence);
                db.AddInParameter(updateCmd, "@SeriesName", DbType.String, onlineNCDBackOfficeVo.SeriesName);
                db.AddInParameter(updateCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                db.AddInParameter(updateCmd, "@IsBuyBackAvailable", DbType.Int32, onlineNCDBackOfficeVo.IsBuyBackAvailable);
                db.AddInParameter(updateCmd, "@Tenure", DbType.Int32, onlineNCDBackOfficeVo.Tenure);
                db.AddInParameter(updateCmd, "@InterestFrequency", DbType.String, onlineNCDBackOfficeVo.InterestFrequency);
                db.AddInParameter(updateCmd, "@InterestType", DbType.String, onlineNCDBackOfficeVo.InterestType);
                db.AddInParameter(updateCmd, "@ModeOfTenure", DbType.String, onlineNCDBackOfficeVo.ModeOfTenure);
                db.AddInParameter(updateCmd, "@RedemptionApplicable", DbType.Int32, onlineNCDBackOfficeVo.RedemptionApplicable);
                db.AddInParameter(updateCmd, "@LockinApplicable", DbType.Int32, onlineNCDBackOfficeVo.LockInApplicable);
                db.AddInParameter(updateCmd, "@TenureUnits", DbType.String, onlineNCDBackOfficeVo.TenureUnits);
                db.AddInParameter(updateCmd, "@seriesFaceValue", DbType.Double, onlineNCDBackOfficeVo.SeriesFaceValue);
                db.AddInParameter(updateCmd, "@ModifiedBy", DbType.Int32, userID);
                db.AddInParameter(updateCmd, "@CreatedBy", DbType.Int32, userID);
                seriesId = db.ExecuteNonQuery(updateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return seriesId;
        }

        public bool UpdateSeriesCategory(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_UpdateSeriesCategories");
                db.AddInParameter(createCmd, "@SeriesId", DbType.Int32, onlineNCDBackOfficeVo.SeriesId);
                db.AddInParameter(createCmd, "@InvestorCatgeoryId", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@DefaultInterestRate", DbType.Double, onlineNCDBackOfficeVo.DefaultInterestRate);
                db.AddInParameter(createCmd, "@AnnualizedYieldUpto", DbType.Double, onlineNCDBackOfficeVo.AnnualizedYieldUpto);
                db.AddInParameter(createCmd, "@RenCpnRate", DbType.Double, onlineNCDBackOfficeVo.RenCpnRate);
                db.AddInParameter(createCmd, "@YieldAtCall", DbType.Double, onlineNCDBackOfficeVo.YieldAtCall);
                db.AddInParameter(createCmd, "@YieldAtBuyBack", DbType.Double, onlineNCDBackOfficeVo.YieldAtBuyBack);
                db.AddInParameter(createCmd, "@RedemptionDate", DbType.String, onlineNCDBackOfficeVo.RedemptionDate);
                db.AddInParameter(createCmd, "@redemptionAmount", DbType.Double, onlineNCDBackOfficeVo.RedemptionAmount);
                db.AddInParameter(createCmd, "@lockinperiod", DbType.Int32, onlineNCDBackOfficeVo.LockInPeriodapplicable);
                db.AddInParameter(createCmd, "@issueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);

                if (db.ExecuteNonQuery(createCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                bResult = false;
                throw Ex;
            }
            return bResult;
        }

        public DataSet GetSeriesInvestorTypeRule(int seriesId)
        {
            DataSet dsGetSubCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSeriesInvestorTypeRule");
                db.AddInParameter(dbCommand, "@seriesId", DbType.String, seriesId);
                dsGetSubCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSeriesInvestorTypeRule()");
                object[] objects = new object[1];
                objects[1] = seriesId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubCategory;
        }

        public DataSet GetCategoryDetails(int CatgeoryId)
        {
            DataSet dsGetSubCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetCategoryDetails");
                db.AddInParameter(dbCommand, "@categoryId", DbType.Int32, CatgeoryId);
                dsGetSubCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetCategoryDetails()");
                object[] objects = new object[1];
                objects[1] = CatgeoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubCategory;
        }

        public DataSet GetIssuerIssue(int advisorId, string product, int businessChannel, string orderStatus)
        {
            DataSet dsGetIssuerIssue;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetIssuerIssues");
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(dbCommand, "@product", DbType.String, product);
                db.AddInParameter(dbCommand, "@businessChannel", DbType.Int32, businessChannel);
              //  db.AddInParameter(dbCommand, "@orderStatus", DbType.String, orderStatus);

                dsGetIssuerIssue = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetIssuerIssue()");
                object[] objects = new object[1];
                objects[1] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetIssuerIssue;
        }

        public DataSet GetUploadIssue(string productCategory, int adviserId, string type, int isOnline)
        {
            DataSet dsGetIssuerIssue;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetUploadIssue");
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, productCategory);
                db.AddInParameter(dbCommand, "Type", DbType.String, type);
                db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, isOnline);
                //@product
                dsGetIssuerIssue = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetUploadIssue()");
                object[] objects = new object[1];
                objects[1] = productCategory;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetIssuerIssue;
        }

        public DataSet GetSubTypePerCategoryDetails(int investorCatgeoryId)
        {
            DataSet dsGetSubCategory;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetCategoryDetails");
                db.AddInParameter(dbCommand, "@categoryId", DbType.Int32, investorCatgeoryId);
                dsGetSubCategory = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSubTypePerCategoryDetails()");
                object[] objects = new object[1];
                objects[1] = investorCatgeoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubCategory;
        }

        public void GenereateNcdExtract(int AdviserId, int UserId, string SourceCode, string ProductAsset, int issueId, ref int isExtracted, int isOnline)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmd;
            DataSet dt = new DataSet();
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_AdviserIssueOrderExtract");
                db.AddInParameter(cmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(cmd, "@U_UserId", DbType.Int32, UserId);
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, SourceCode);
                db.AddInParameter(cmd, "@PAG_AssetGroupCode", DbType.String, ProductAsset);
                db.AddInParameter(cmd, "@AIM_IssueId", DbType.Int32, issueId);
                db.AddInParameter(cmd, "@isExtracted", DbType.Int32, issueId);
                db.AddInParameter(cmd, "@IsOnline", DbType.Int32, isOnline);

                dt = db.ExecuteDataSet(cmd);
                isExtracted = int.Parse(db.GetParameterValue(cmd, "@isExtracted").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GenereateNcdExtract(int adviserId, int userId)");
                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = UserId;
                objects[2] = SourceCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataSet GetOnlineNcdExtractPreview(DateTime Today, int AdviserId, int FileType, int issueId, string extSource, string modificationType, int isOnline, out int AID_SeriesCount)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsGetOnlineNCDExtractPreview;
            DbCommand GetOnlineNCDExtractPreviewcmd;
            AID_SeriesCount = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOnlineNCDExtractPreviewcmd = db.GetStoredProcCommand("SPROC_ONL_IssueExtractPreview");
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@Today", DbType.DateTime, Today);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@WIFT_Id", DbType.Int32, FileType);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@IssueId", DbType.Int32, issueId);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@ExtSource", DbType.String, extSource);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@ModificationType", DbType.String, modificationType);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@IsOnline", DbType.Int16, isOnline);
                db.AddOutParameter(GetOnlineNCDExtractPreviewcmd, "@AID_SeriesCount", DbType.Int32, 1000);
                GetOnlineNCDExtractPreviewcmd.CommandTimeout = 60 * 60;
                if (db.ExecuteNonQuery(GetOnlineNCDExtractPreviewcmd) != 0)
                {
                    if (db.GetParameterValue(GetOnlineNCDExtractPreviewcmd, "AID_SeriesCount").ToString() != string.Empty)
                    {
                        AID_SeriesCount = Int32.Parse(db.GetParameterValue(GetOnlineNCDExtractPreviewcmd, "AID_SeriesCount").ToString());

                    }

                }
                dsGetOnlineNCDExtractPreview = db.ExecuteDataSet(GetOnlineNCDExtractPreviewcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetOnlineNcdExtractPreview(DateTime Today, int AdviserId, int FileType)");
                object[] objects = new object[3];
                objects[2] = FileType;
                objects[1] = AdviserId;
                objects[0] = Today;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetOnlineNCDExtractPreview;
        }




        public DataSet GetNcdIpoAcntingExtract(DateTime Today, int adviserId, int issueId, string Product, string extractType)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsGetOnlineNCDExtractPreview;
            DbCommand GetOnlineNCDExtractPreviewcmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOnlineNCDExtractPreviewcmd = db.GetStoredProcCommand("SPROC_GetNcdIpoAccountingExtractFiles");
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@Today", DbType.DateTime, Today);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@AdviserIssueId", DbType.Int32, issueId);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@Product", DbType.String, Product);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@extractType", DbType.String, extractType);


                dsGetOnlineNCDExtractPreview = db.ExecuteDataSet(GetOnlineNCDExtractPreviewcmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsGetOnlineNCDExtractPreview;
        }




        public DataTable GetAdviserNCDOrderBook(int adviserId, int issueNo, string status, DateTime dtFrom, DateTime dtTo, int orderNo)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsNCDOrder;
            DataTable dtNCDOrder;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetAdviserNCDOrderBook");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, adviserId);
                if (status != "0")
                    db.AddInParameter(cmd, "@Status", DbType.String, status);
                else
                    db.AddInParameter(cmd, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@AIMissue", DbType.Int32, issueNo);
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, dtFrom);
                else
                    db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, DBNull.Value);
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(cmd, "@ToDate", DbType.DateTime, dtTo);
                else
                    db.AddInParameter(cmd, "@ToDate", DbType.DateTime, DBNull.Value);
                if (orderNo != 0)
                    db.AddInParameter(cmd, "@orderNo", DbType.Int32, orderNo);
                else
                    db.AddInParameter(cmd, "@orderNo", DbType.Int32, 0);
                dsNCDOrder = db.ExecuteDataSet(cmd);
                dtNCDOrder = dsNCDOrder.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetAdviserNCDOrderBook()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrder;
        }

        public DataTable GetAdviserNCDOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataSet dsNCDOrderBook;
            DataTable dtNCDOrderBook;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetNCDOrderBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNCDOrderBookcmd = db.GetStoredProcCommand("SPROC_ONL_GetAdviserBondOrdersubBook");
                db.AddInParameter(GetNCDOrderBookcmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetNCDOrderBookcmd, "@IssuerId", DbType.Int32, IssuerId);
                db.AddInParameter(GetNCDOrderBookcmd, "@orderId", DbType.Int32, orderid);
                dsNCDOrderBook = db.ExecuteDataSet(GetNCDOrderBookcmd);
                dtNCDOrderBook = dsNCDOrderBook.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetAdviserNCDOrderSubBook()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrderBook;
        }

        public DataTable GetFileTypeList(int FileTypeId, string ExternalSource, char FileSubType, string ProductCode)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmd;
            DataTable dtFileType;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetFileType");
                if (FileTypeId > 0) db.AddInParameter(cmd, "@WEFT_Id", DbType.Int32, FileTypeId);
                if (!string.IsNullOrEmpty(ExternalSource)) db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, ExternalSource);
                db.AddInParameter(cmd, "@WEFT_FileSubType", DbType.String, FileSubType);
                if (!string.IsNullOrEmpty(ProductCode)) db.AddInParameter(cmd, "@PAG_AssetGroupCode", DbType.String, ProductCode);
                DataSet ds = db.ExecuteDataSet(cmd);
                dtFileType = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GenereateNcdExtract(int adviserId, int userId)");
                object[] objects = new object[3];
                objects[0] = FileTypeId;
                objects[1] = ExternalSource;
                objects[2] = FileSubType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtFileType;
        }

        public DataTable GetHeaderMapping(int fileTypeId, string ExternalSource)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmd;
            DataTable dtHeaders;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_IssueExtractHeaders");
                db.AddInParameter(cmd, "@WEFT_Id", DbType.Int32, fileTypeId);
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, ExternalSource);
                DataSet ds = db.ExecuteDataSet(cmd);
                dtHeaders = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetHeaderMapping(int fileTypeId, string ExternalSource)");
                object[] objects = new object[2];
                objects[0] = fileTypeId;
                objects[1] = ExternalSource;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtHeaders;
        }

        public void GetOrdersEligblity(int issueId, ref int isPurchaseAvailable)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;

            //bool status = false;
            //int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_PurchaseAvailablity");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddOutParameter(dbCommand, "@IsPurchaseAvailable", DbType.Int32, isPurchaseAvailable);
                db.ExecuteDataSet(dbCommand);
                isPurchaseAvailable = int.Parse(db.GetParameterValue(dbCommand, "@IsPurchaseAvailable").ToString());

                // isPurchaseAvailable = Convert.ToInt32(db.ExecuteScalar(dbCommand).ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:UpdateNcdOrderMannualMatch()");
                object[] objects = new object[7];
                objects[0] = issueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //if (affectedRecords > 0)
            //    return status = true;
            //else
            //    return status = false;
        }

        public void UpdateNcdOrderMannualMatch(int orderId, int allotmentId, ref int isAllotmented, ref int isUpdated)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand updateCmd;
            //bool status = false;
            //int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SPROC_Onl_UpdateNcdOrderMannualMatch");
                db.AddInParameter(updateCmd, "@OrderId", DbType.Int32, orderId);
                db.AddInParameter(updateCmd, "@AllotmentId", DbType.Int32, allotmentId);
                db.AddOutParameter(updateCmd, "@IsSuccess", DbType.Int16, 0);
                db.AddOutParameter(updateCmd, "@IsAlloted", DbType.Int16, 0);

                if (db.ExecuteNonQuery(updateCmd) != 0)
                {
                    isUpdated = int.Parse(db.GetParameterValue(updateCmd, "@IsSuccess").ToString());
                    isAllotmented = int.Parse(db.GetParameterValue(updateCmd, "@IsAlloted").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:UpdateNcdOrderMannualMatch()");
                object[] objects = new object[7];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //if (affectedRecords > 0)
            //    return status = true;
            //else
            //    return status = false;
        }

        public void UpdateNcdAutoMatch(int orderId, int applictionNo, string dpId, ref int isAllotmented, ref int isUpdated)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand updateCmd;
            //  bool status = false;
            //int affectedRecords = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SPROC_Onl_UpdateNcdAutoMatch");
                db.AddInParameter(updateCmd, "@OrderId", DbType.Int32, orderId);
                db.AddInParameter(updateCmd, "@AppNo", DbType.Int32, applictionNo);
                db.AddInParameter(updateCmd, "@DpId", DbType.String, dpId);
                db.AddOutParameter(updateCmd, "@IsSuccess", DbType.Int16, 0);
                db.AddOutParameter(updateCmd, "@IsAlloted", DbType.Int16, 0);


                if (db.ExecuteNonQuery(updateCmd) != 0)
                {
                    isUpdated = int.Parse(db.GetParameterValue(updateCmd, "@IsSuccess").ToString());
                    isAllotmented = int.Parse(db.GetParameterValue(updateCmd, "@IsAlloted").ToString());
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:UpdateNcdAutoMatch()");
                object[] objects = new object[7];
                objects[0] = orderId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //if (affectedRecords > 0)
            //    return status = true;
            //else
            //    return status = false;
        }

        public DataSet GetUnmatchedAllotments(int adviserId, int issueId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand GetCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCmd = db.GetStoredProcCommand("SPROC_GetUnmatchedAllotments");
                db.AddInParameter(GetCmd, "@advisorId", DbType.Int32, adviserId);
                db.AddInParameter(GetCmd, "@isseueId", DbType.Int32, issueId);
                ds = db.ExecuteDataSet(GetCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetOnlineNcdExtractPreview(DateTime Today, int AdviserId, int FileType)");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetAdviserOrders(int IssueId, string Product, int adviserid, int BusinessChannel, string userType,string AgentCode)
        {
            DataSet dsOrders;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAdviserWiseOrders");
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, IssueId);
                db.AddInParameter(dbCommand, "@Product", DbType.String, Product);
                db.AddInParameter(dbCommand, "@AdviserId", DbType.Int32, adviserid);
                db.AddInParameter(dbCommand, "@BusinessChannel", DbType.Int32, BusinessChannel);
                db.AddInParameter(dbCommand, "@userType", DbType.String, userType);
                db.AddInParameter(dbCommand, "@AgentCode", DbType.String, AgentCode);
                dsOrders = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetAdviserOrders()");
                object[] objects = new object[1];
                objects[1] = IssueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrders;
        }

        public DataSet GetAdviserissueallotmentList(int adviserId, int issureid, string type, DateTime fromdate, DateTime todate)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsGetAdviserissueallotmentList;
            DbCommand GetAdviserissueallotmentListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserissueallotmentListcmd = db.GetStoredProcCommand("SPROC_GetAdviserIssueAllotmentDetail");
                db.AddInParameter(GetAdviserissueallotmentListcmd, "@AdviserId", DbType.Int32, adviserId);
                if (issureid != 0)
                    db.AddInParameter(GetAdviserissueallotmentListcmd, "@Issuerid", DbType.Int32, issureid);
                else
                    db.AddInParameter(GetAdviserissueallotmentListcmd, "@Issuerid", DbType.Int32, 0);
                if (type != "0")
                    db.AddInParameter(GetAdviserissueallotmentListcmd, "@type", DbType.String, type);
                else
                    db.AddInParameter(GetAdviserissueallotmentListcmd, "@type", DbType.String, DBNull.Value);
                db.AddInParameter(GetAdviserissueallotmentListcmd, "@fromdate", DbType.Date, fromdate);
                db.AddInParameter(GetAdviserissueallotmentListcmd, "@todate", DbType.Date, todate);
                dsGetAdviserissueallotmentList = db.ExecuteDataSet(GetAdviserissueallotmentListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetAdviserClientKYCStatusList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAdviserissueallotmentList;
        }

        public DataTable GetIssuerid(int adviserid)
        {
            DataSet dsGetIssuerid;
            DataTable dt;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetIssueridCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIssueridCmd = db.GetStoredProcCommand("SPROC_GetIssuerId");
                db.AddInParameter(GetIssueridCmd, "@AdviserId", DbType.Int32, adviserid);
                dsGetIssuerid = db.ExecuteDataSet(GetIssueridCmd);
                dt = dsGetIssuerid.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetIssuerid()");
                object[] objects = new object[1];
                objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        public DataTable GetBusinessChannel()
        {
            DataSet dsGetChannel;
            DataTable dt;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetIssueridCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIssueridCmd = db.GetStoredProcCommand("SPROC_GetBusinessChannel");
                dsGetChannel = db.ExecuteDataSet(GetIssueridCmd);
                dt = dsGetChannel.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetIssuerid()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        public DataTable GetFrequency()
        {
            DataSet dsGetIssuerid;
            DataTable dt;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetIssueridCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIssueridCmd = db.GetStoredProcCommand("SPROC_GetProductWiseFrequency");
                dsGetIssuerid = db.ExecuteDataSet(GetIssueridCmd);
                dt = dsGetIssuerid.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetIssuerid()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public int UploadAllotmentIssueData(DataTable dtData, int issueId, ref string isValidated, string product)
        {
            int result = 0;
            DataTable dt;
            try
            {

                string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_ValidateUploadIssueAllotmentDetails", sqlCon);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@Details", dtData);
                cmdProc.Parameters.AddWithValue("@issueId", issueId);
                cmdProc.Parameters.AddWithValue("@result", string.Empty);

                isValidated = cmdProc.ExecuteScalar().ToString();
                if (isValidated == string.Empty)
                {
                    SqlCommand cmdProcAllot = new SqlCommand("SPROC_UploadIssueAllotmentDetails", sqlCon);
                    cmdProcAllot.CommandType = CommandType.StoredProcedure;
                    cmdProcAllot.Parameters.AddWithValue("@Details", dtData);
                    cmdProcAllot.Parameters.AddWithValue("@issueId", issueId);
                    cmdProcAllot.Parameters.AddWithValue("@product", product);

                    //cmdProcAllot.Parameters.AddWithValue("@result", string.Empty);

                    result = cmdProcAllot.ExecuteNonQuery();

                }
                else
                {
                    result = 0;
                }

                //dt = dsGetIssuerid.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UploadAllotmentIssueData()");
                object[] objects = new object[1];
                //objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
        public DataTable UploadAllotmentIssueDataDynamic(DataTable dtData, int issueId, ref string isValidated, string product, string filePath, int userId, int isOnline,string subCategoryCode)
        {
            int result = 0;
            UploadData(dtData);
            DataTable dtAllotmentUploadData = new DataTable();
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdAllotmentUpload;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAllotmentUpload = db.GetStoredProcCommand("SPROC_ONL_AdviserIssueAllotmentUpload");
                db.AddInParameter(cmdAllotmentUpload, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(cmdAllotmentUpload, "@tableName", DbType.String, allotmentDataTable);
                db.AddInParameter(cmdAllotmentUpload, "@filePath", DbType.String, filePath);
                db.AddInParameter(cmdAllotmentUpload, "@createdBy", DbType.Int32, userId);
                db.AddInParameter(cmdAllotmentUpload, "@product", DbType.String, product);
                db.AddInParameter(cmdAllotmentUpload, "@IsOnline", DbType.String, isOnline);
                db.AddInParameter(cmdAllotmentUpload, "@subCategoryCode", DbType.String, subCategoryCode);
                cmdAllotmentUpload.CommandTimeout = 60 * 60;
                dtAllotmentUploadData = db.ExecuteDataSet(cmdAllotmentUpload).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UploadAllotmentIssueData()");
                object[] objects = new object[1];
                //objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAllotmentUploadData;

        }

        public int UploadBidSuccessData(DataTable dtData, int issueId)
        {
            int result;
            try
            {

                string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_UploadBidSuccessData", sqlCon);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@Details", dtData);
                cmdProc.Parameters.AddWithValue("@issueId", issueId);

                result = cmdProc.ExecuteNonQuery();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UploadAllotmentIssueData()");
                object[] objects = new object[1];
                //objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public int IPOUploadBidSuccessData(DataTable dtData, int issueId,int isOnline)
        {
            int result;
            try
            {

                string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_IPO_UploadBidSuccessData", sqlCon);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@Details", dtData);
                cmdProc.Parameters.AddWithValue("@issueId", issueId);
              
                cmdProc.CommandTimeout = 60 * 60;
                result = cmdProc.ExecuteNonQuery();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UploadAllotmentIssueData()");
                object[] objects = new object[1];
                //objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }


        public int UploadChequeIssueData(DataTable dtData, int issueId)
        {
            int result;
            try
            {

                string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_UploadChequeIssueData", sqlCon);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@Details", dtData);
                cmdProc.Parameters.AddWithValue("@issueId", issueId);

                result = cmdProc.ExecuteNonQuery();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UploadAllotmentIssueData()");
                object[] objects = new object[1];
                //objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public int UploadIPOChequeIssueData(DataTable dtData, int issueId)
        {
            int result;
            try
            {

                string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_IPO_UploadChequeIssueData", sqlCon);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@IPoDetails", dtData);
                cmdProc.Parameters.AddWithValue("@issueId", issueId);

                result = cmdProc.ExecuteNonQuery();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UploadAllotmentIssueData()");
                object[] objects = new object[1];
                //objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public void UploadIssueData(string sqlUpdate, string sqlSel, string csvParams, string csvDataType, DataTable dtData)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(conString);

                SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, sqlCon);


                string[] Params = csvParams.Split(',');
                string[] ParamsType = csvDataType.Split(',');
                //foreach (DataRow row in dtData.Rows) {
                for (int i = 0; i < Params.Length; i++)
                {
                    SqlDbType sqlType = GetSqlDbType(ParamsType[i]);
                    SqlParameter param = cmdUpdate.Parameters.Add("@" + Params[i], sqlType, 50, Params[i]);
                    param.SourceVersion = DataRowVersion.Original;
                }
                // }

                //DataSet ds = new DataSet("AdviserIssueOrderExtract");
                //ds.Tables.Add(dtData);

                foreach (DataRow row in dtData.Rows)
                {
                    if (row.RowState == DataRowState.Unchanged)
                    {
                        row.SetModified();
                    }
                }

                //dtData.
                SqlDataAdapter da = new SqlDataAdapter(sqlSel, sqlCon);

                da.AcceptChangesDuringUpdate = true;
                da.UpdateCommand = cmdUpdate;
                sqlCon.Open();
                int nRows = da.Update(dtData);
                //int nRows = da.Update(ds, "AdviserIssueOrderExtract");
                sqlCon.Close();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetIssuerid()");
                object[] objects = new object[1];
                //objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        private SqlDbType GetSqlDbType(string sDataType)
        {
            switch (sDataType)
            {
                case "System.Int64":
                    return SqlDbType.BigInt;
                case "System.Boolean":
                    return SqlDbType.TinyInt;
                case "System.DateTime":
                    return SqlDbType.Date;
                case "System.Decimal":
                    return SqlDbType.Decimal;
                case "System.Int32":
                    return SqlDbType.Int;
                case "System.String":
                    return SqlDbType.VarChar;
                case "System.Int16":
                    return SqlDbType.SmallInt;
                case "System.TimeSpan":
                    return SqlDbType.Time;
                default:
                    return SqlDbType.VarChar;
            }
        }

        public int CheckIssueSeriesName(string externalcode, int issueid)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdCheckIssueName;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdCheckIssueName = db.GetStoredProcCommand("SPROC_TocheckingIssueDetailName");
                db.AddInParameter(cmdCheckIssueName, "@IssueDetailName", DbType.String, externalcode);
                db.AddInParameter(cmdCheckIssueName, "@Issueid", DbType.Int32, issueid);
                db.AddOutParameter(cmdCheckIssueName, "@count", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdCheckIssueName);
                if (db.ExecuteNonQuery(cmdCheckIssueName) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdCheckIssueName, "count").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CheckIssueName()");
                object[] objects = new object[2];
                objects[0] = externalcode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
        }

        public DataTable BankBranchName(int bankid)
        {
            DataSet dsBankBranchName;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataTable dt;
            DbCommand CmdBankBranchName;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdBankBranchName = db.GetStoredProcCommand("SPROC_GetBankBranch");
                db.AddInParameter(CmdBankBranchName, "@bankId", DbType.Int32, bankid);
                dsBankBranchName = db.ExecuteDataSet(CmdBankBranchName);
                dt = dsBankBranchName.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public void NSEandBSEcodeCheck(int issueid, int adviserid, string nsecode, string bsecode, ref int isBseExist, ref int isNseExist)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdNSEandBSEcodeCheck;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                cmdNSEandBSEcodeCheck = db.GetStoredProcCommand("SPROC_CheckingNSEandBSEcode");
                db.AddInParameter(cmdNSEandBSEcodeCheck, "@AdviserId", DbType.String, adviserid);
                db.AddInParameter(cmdNSEandBSEcodeCheck, "@IssueId", DbType.String, issueid);
                db.AddInParameter(cmdNSEandBSEcodeCheck, "@NseCode", DbType.String, nsecode);
                db.AddInParameter(cmdNSEandBSEcodeCheck, "@BseCode", DbType.String, bsecode);
                db.AddOutParameter(cmdNSEandBSEcodeCheck, "@isBseExist", DbType.Int32, 10);
                db.AddOutParameter(cmdNSEandBSEcodeCheck, "@isNseExist", DbType.Int32, 10);

                ds = db.ExecuteDataSet(cmdNSEandBSEcodeCheck);

                isBseExist = Convert.ToInt32(db.GetParameterValue(cmdNSEandBSEcodeCheck, "isBseExist").ToString());
                isNseExist = Convert.ToInt32(db.GetParameterValue(cmdNSEandBSEcodeCheck, "isNseExist").ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public bool Deleteinvestmentcategory(int investorid)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand DeleteinvestmentcategoryCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteinvestmentcategoryCmd = db.GetStoredProcCommand("SPROC_DeleteInvestorCategory");
                db.AddInParameter(DeleteinvestmentcategoryCmd, "@Adviserinvetmentsubtypeid", DbType.Int32, investorid);

                if (db.ExecuteNonQuery(DeleteinvestmentcategoryCmd) != 0)
                    bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:deleteTradeBusinessDate()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;

        }

        public bool DeleteIssueinvestor(int investorcategoryid)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand DeleteIssueinvestorCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteIssueinvestorCmd = db.GetStoredProcCommand("SPROC_Deleteadviseinvestor");
                db.AddInParameter(DeleteIssueinvestorCmd, "@AIICcode", DbType.Int32, investorcategoryid);

                if (db.ExecuteNonQuery(DeleteIssueinvestorCmd) != 0)
                    bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:DeleteIssueinvestor(int investorcategoryid)");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }

        public int Getissueid(int orderid)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdGetissueid;
            int issueid = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetissueid = db.GetStoredProcCommand("SPROC_CheckUploadData");
                db.AddInParameter(cmdGetissueid, "@OrderId", DbType.Int32, orderid);
                db.AddOutParameter(cmdGetissueid, "@issueid", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdGetissueid);
                if (db.ExecuteNonQuery(cmdGetissueid) != 0)
                {
                    issueid = Convert.ToInt32(db.GetParameterValue(cmdGetissueid, "issueid").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return issueid;
        }

        public int GetScriptId(string scriptid, int adviserid, string product)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdGetScriptId;
            int issueid = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                cmdGetScriptId = db.GetStoredProcCommand("SPROC_TocheckBidUpload");
                db.AddInParameter(cmdGetScriptId, "@Scriptid", DbType.String, scriptid);
                db.AddInParameter(cmdGetScriptId, "@adviserid", DbType.Int32, adviserid);
                db.AddOutParameter(cmdGetScriptId, "@issueid", DbType.Int32, 0);
                db.AddInParameter(cmdGetScriptId, "@product", DbType.String, product);

                if (db.ExecuteScalar(cmdGetScriptId) != null)
                    issueid = Convert.ToInt32(db.ExecuteScalar(cmdGetScriptId).ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return issueid;
        }

        public bool DeleteSubcategory(int issuesubtyperuleid)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand DeleteSubcategoryCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteSubcategoryCmd = db.GetStoredProcCommand("SPROC_DeleteSubcategory");
                db.AddInParameter(DeleteSubcategoryCmd, "@AIDCSR_Id", DbType.Int32, issuesubtyperuleid);

                if (db.ExecuteNonQuery(DeleteSubcategoryCmd) != 0)
                    bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:deleteTradeBusinessDate()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;

        }

        public void DeleteAvaliable(int adviserid, int InvestorCatgeoryId, int AIICST_Id, int AIDCSR_Id, int IssueDetailId, int issueId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdDeleteAvaliable;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                cmdDeleteAvaliable = db.GetStoredProcCommand("SPROC_IsIssuedeleteAvailable");
                db.AddInParameter(cmdDeleteAvaliable, "@AIICST_Id", DbType.Int32, AIICST_Id);
                db.AddInParameter(cmdDeleteAvaliable, "@InvestorCatgeoryId", DbType.Int32, InvestorCatgeoryId);
                db.AddInParameter(cmdDeleteAvaliable, "@AIDCSR_Id", DbType.Int32, AIDCSR_Id);
                db.AddInParameter(cmdDeleteAvaliable, "@IssueDetailId", DbType.Int32, IssueDetailId);
                db.AddOutParameter(cmdDeleteAvaliable, "@issueId", DbType.Int32, issueId);
                db.AddOutParameter(cmdDeleteAvaliable, "@advisorId", DbType.Int32, adviserid);
                ds = db.ExecuteDataSet(cmdDeleteAvaliable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public int CheckAccountisActive(int adviserid, int customerid)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdCheckAccountisActive;
            int isExist = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckAccountisActive = db.GetStoredProcCommand("SPROC_IsCustomerDematAcount");
                db.AddInParameter(cmdCheckAccountisActive, "@advisorId", DbType.Int32, adviserid);
                db.AddInParameter(cmdCheckAccountisActive, "@customerId", DbType.Int32, customerid);
                db.AddOutParameter(cmdCheckAccountisActive, "@isExist", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdCheckAccountisActive);
                if (db.ExecuteNonQuery(cmdCheckAccountisActive) != 0)
                {
                    isExist = Convert.ToInt32(db.GetParameterValue(cmdCheckAccountisActive, "isExist").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return isExist;
        }

        public DataSet GetNCDIPOAccountingExtractType(string Product)
        {
            DataSet dsExtractType;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetGetMfOrderExtractCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetMfOrderExtractCmd = db.GetStoredProcCommand("SPROC_ONL_GetNCDIPOAccountingExtractType");
                db.AddInParameter(GetGetMfOrderExtractCmd, "@Product", DbType.String, Product);
                dsExtractType = db.ExecuteDataSet(GetGetMfOrderExtractCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }

        public DataSet GetNCDIPOExtractTypeDataForFileCreation(DateTime orderDate, int AdviserId, int extractType, DateTime fromDate, DateTime toDate, int IssuerID, string Product)
        {
            DataSet dsExtractType;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetGetMfOrderExtractCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetMfOrderExtractCmd = db.GetStoredProcCommand("SPROC_GetNCDIPOExtractTypeDataForFileCreation");
                //if (orderDate != DateTime.MinValue)
                //db.AddInParameter(GetGetMfOrderExtractCmd, "@orderDate", DbType.DateTime, orderDate);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@extractType", DbType.Int32, extractType);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@ProductIssuerID", DbType.Int32, IssuerID);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@Product", DbType.String, Product);

                dsExtractType = db.ExecuteDataSet(GetGetMfOrderExtractCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }

        public DataSet GetProductIssuerList(int Isactive, string Product)
        {
            DataSet dsProductIssuer;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetProductIssuerListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetProductIssuerListCmd = db.GetStoredProcCommand("SPROC_GetProductIssuerList");
                db.AddInParameter(GetProductIssuerListCmd, "@Isactive", DbType.Int32, Isactive);
                db.AddInParameter(GetProductIssuerListCmd, "@Product", DbType.String, Product);
                dsProductIssuer = db.ExecuteDataSet(GetProductIssuerListCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsProductIssuer;
        }

        public string GetNCDIPOProductIssuer(int IssueId)
        {
            DataSet dsProductIssuer;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetProductIssuerListCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetProductIssuerListCmd = db.GetStoredProcCommand("SPROC_GetNCDIPOProductIssuer");
                db.AddInParameter(GetProductIssuerListCmd, "@IssueId", DbType.Int32, IssueId);
                dsProductIssuer = db.ExecuteDataSet(GetProductIssuerListCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsProductIssuer.Tables[0].Rows[0][0].ToString();
        }
        public DataTable GetIssueName(int Adviserid, string product)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand cmdGetIssueName;
            DataTable dtGetIssueName;
            DataSet dsGetIssueName = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetIssueName = db.GetStoredProcCommand("SPROC_GetADviserNCDIssueName");
                db.AddInParameter(cmdGetIssueName, "@AdviserId", DbType.Int32, Adviserid);
                db.AddInParameter(cmdGetIssueName, "@product", DbType.String, product);
                dsGetIssueName = db.ExecuteDataSet(cmdGetIssueName);
                dtGetIssueName = dsGetIssueName.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetIssueName;
        }
        public DataTable GetNCDHoldings(int AdviserId, int AIMIssueId, int PageSize, int CurrentPage, string CustomerNamefilter, out int RowCount)
        {
            DataTable dtGetNCDHoldings;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsGetNCDHoldings;
            DbCommand GetNCDHoldingscmd;
            RowCount = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNCDHoldingscmd = db.GetStoredProcCommand("SPROC_GetAdviserIssueHoldings");
                db.AddInParameter(GetNCDHoldingscmd, "@AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetNCDHoldingscmd, "@AIMissue", DbType.Int32, AIMIssueId);
                db.AddInParameter(GetNCDHoldingscmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(GetNCDHoldingscmd, "@CustomerNameFilter", DbType.String, CustomerNamefilter);
                db.AddInParameter(GetNCDHoldingscmd, "@PageSize", DbType.Int32, PageSize);
                db.AddOutParameter(GetNCDHoldingscmd, "@RowCount", DbType.Int32, 0);

                //dsGetNCDHoldings = db.ExecuteDataSet(GetNCDHoldingscmd);
                //dtGetNCDHoldings = dsGetNCDHoldings.Tables[0];
                dsGetNCDHoldings = db.ExecuteDataSet(GetNCDHoldingscmd);
                dtGetNCDHoldings = dsGetNCDHoldings.Tables[0];
                if (db.ExecuteNonQuery(GetNCDHoldingscmd) != 0)
                {
                    if (db.GetParameterValue(GetNCDHoldingscmd, "RowCount").ToString() != "")
                    {
                        RowCount = Convert.ToInt32(db.GetParameterValue(GetNCDHoldingscmd, "RowCount").ToString());
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:Getproductcode()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetNCDHoldings;
        }
        public DataSet GetNCDSubHoldings(int AdviserId, int IssueId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsGetNCDSubHoldings;
            DbCommand GetNCDSubHoldingscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNCDSubHoldingscmd = db.GetStoredProcCommand("SPROC_GetAdviserIssueSeriesWiseNCDHolding");
                db.AddInParameter(GetNCDSubHoldingscmd, "@AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetNCDSubHoldingscmd, "@IssueId", DbType.Int32, IssueId);
                dsGetNCDSubHoldings = db.ExecuteDataSet(GetNCDSubHoldingscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:Getproductcode()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetNCDSubHoldings;
        }
        public int CheckBankisActive(int CustomerId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdCheckBankisActive;
            int isExist = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckBankisActive = db.GetStoredProcCommand("SPROC_BankISAvailable");
                db.AddInParameter(cmdCheckBankisActive, "@customerId", DbType.Int32, CustomerId);
                db.AddOutParameter(cmdCheckBankisActive, "@isExist", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdCheckBankisActive);
                if (db.ExecuteNonQuery(cmdCheckBankisActive) != 0)
                {
                    isExist = Convert.ToInt32(db.GetParameterValue(cmdCheckBankisActive, "isExist").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return isExist;
        }
        public DataTable GetNCDAllotmentFileType(string fileType, string productType)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsGetNCDAllotment;
            DbCommand cmdGetNCDAllotment;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetNCDAllotment = db.GetStoredProcCommand("SPROC_ONL_GetNCDAllotmentFileType");
                db.AddInParameter(cmdGetNCDAllotment, "@typeofRegister", DbType.String, fileType);
                db.AddInParameter(cmdGetNCDAllotment, "@productType", DbType.String, productType);
                dsGetNCDAllotment = db.ExecuteDataSet(cmdGetNCDAllotment);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetNCDAllotment.Tables[0];
        }

        public void UploadData(DataTable uploadData)
        {
            allotmentDataTable = GenerateTableName();
            string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            string[] DdName = conString.Split(';');
            DdName[1] = DdName[1].Substring(DdName[1].IndexOf('W'), DdName[1].Length - DdName[1].IndexOf('W'));
            SqlConnection Connection = new SqlConnection(conString);
            //SMO Server object setup with SQLConnection.
            Server server = new Server(new ServerConnection(Connection));
            //Create a new SMO Database giving server object and database name
            //Microsoft.SqlServer.Management.Smo.Database db = new Microsoft.SqlServer.Management.Smo.Database("WealthERP_SBI_STG");
            Microsoft.SqlServer.Management.Smo.Database db = server.Databases[DdName[1]];
            //db.Create();
            //Set Database to the newly created database
            db = server.Databases[DdName[1]];
            //Create a new SMO table
            Table UploadLogTable = new Table(db, allotmentDataTable);
            Column tempC = new Column();
            //Add the column names and types from the datatable into the new table
            //Using the columns name and type property
            foreach (DataColumn dc in uploadData.Columns)
            {
                //Create columns from datatable column schema
                tempC = new Column(UploadLogTable, dc.ColumnName);
                tempC.DataType = GetDataType(dc.DataType.ToString());

                UploadLogTable.Columns.Add(tempC);
            }
            //Create the Destination Table
            UploadLogTable.Create();
            createtableDatabse(uploadData);
        }
        public DataType GetDataType(string dataType)
        {
            DataType DTTemp = null;

            switch (dataType)
            {
                case ("System.Decimal"):
                    DTTemp = DataType.Decimal(2, 18);
                    break;
                case ("System.String"):
                    DTTemp = DataType.VarChar(50);
                    break;
                case ("System.Int32"):
                    DTTemp = DataType.Int;
                    break;
            }
            return DTTemp;
        }
        public string GenerateTableName()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            Random rnd = new Random();
            for (int i = 0; i <= 7; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            return strPwd;
        }
        public void createtableDatabse(DataTable uploaddata)
        {

            string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;

            //Open a connection with destination database;
            using (SqlConnection connection =
                   new SqlConnection(conString))
            {
                connection.Open();

                //Open bulkcopy connection.
                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                {
                    //Set destination table name
                    //to table previously created.
                    bulkcopy.BulkCopyTimeout = 60 * 60;
                    bulkcopy.DestinationTableName = allotmentDataTable;

                    try
                    {
                        bulkcopy.WriteToServer(uploaddata);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    connection.Close();
                }
            }
        }
        public int CustomerMultipleOrder(int CustomerId, int AIMissueId)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet ds;
            DbCommand cmdCheckBankisActive;
            int Count = 0;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckBankisActive = db.GetStoredProcCommand("SPROC_GetIssueIsMultipalApplicable");
                db.AddInParameter(cmdCheckBankisActive, "@customerId", DbType.Int32, CustomerId);
                db.AddInParameter(cmdCheckBankisActive, "@AIMissueId", DbType.Int32, AIMissueId);

                db.AddOutParameter(cmdCheckBankisActive, "@Count", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdCheckBankisActive);
                if (db.ExecuteNonQuery(cmdCheckBankisActive) != 0)
                {
                    Count = Convert.ToInt32(db.GetParameterValue(cmdCheckBankisActive, "Count").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Count;
        }
        public bool CreateRegister(string register, int userid)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand CreateRegisterCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateRegisterCmd = db.GetStoredProcCommand("SPROC_XMLRegister");
                db.AddInParameter(CreateRegisterCmd, "@register", DbType.String, register);
                db.AddInParameter(CreateRegisterCmd, "@userid", DbType.Int32, userid);

                if (db.ExecuteNonQuery(CreateRegisterCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable GetIssueList(int adviserId, int type, int customerId, string productAssetGroup)
        {
            DataTable dtIssueList;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand getIssueListCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getIssueListCmd = db.GetStoredProcCommand("SPROC_OFF_GetIssueList");
                db.AddInParameter(getIssueListCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getIssueListCmd, "@type", DbType.Int32, type);
                db.AddInParameter(getIssueListCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(getIssueListCmd, "@productAssetGroup", DbType.String, productAssetGroup);
                dtIssueList = db.ExecuteDataSet(getIssueListCmd).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetIssueList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIssueList;
        }

        public DataSet BindSyndiacteAndBusinessChannel()
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbCommand;
            DataSet dsBindSyndiacteChannel;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_BindSyndicate");
                dsBindSyndiacteChannel = db.ExecuteDataSet(dbCommand);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:BindSyndiacteAndBusinessChannel()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsBindSyndiacteChannel;
        }
        public bool CreateSyndiacte(string Syndicatename, int userid)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand CreateSyndiacteCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateSyndiacteCmd = db.GetStoredProcCommand("SPROC_CreateSyndiacte");
                db.AddInParameter(CreateSyndiacteCmd, "@syndicateName", DbType.String, Syndicatename);
                db.AddInParameter(CreateSyndiacteCmd, "@userId", DbType.Int32, userid);
                if (db.ExecuteNonQuery(CreateSyndiacteCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable BindBroker()
        {
            DataTable dtBindBroker;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbBindBroker;
            DataSet dsBindBroker;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbBindBroker = db.GetStoredProcCommand("SPROC_GetBrokerName");
                dsBindBroker = db.ExecuteDataSet(dbBindBroker);
                dtBindBroker = dsBindBroker.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:BindRta()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtBindBroker;
        }
        public bool CreateBroker(string BrokerName, int userid)
        {
            bool bResult = false;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand CreateBrokerCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateBrokerCmd = db.GetStoredProcCommand("SPROC_CreateBroker");
                db.AddInParameter(CreateBrokerCmd, "@brokerShortName", DbType.String, BrokerName);
                db.AddInParameter(CreateBrokerCmd, "@userId", DbType.Int32, userid);
                if (db.ExecuteNonQuery(CreateBrokerCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable GetIssuercategorywise(string category)
        {
            DataTable dtGetIssuercategorywise;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand dbGetIssuercategorywise;
            DataSet dsGetIssuercategorywise;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbGetIssuercategorywise = db.GetStoredProcCommand("SPROC_GetIssuercategorywise");
                db.AddInParameter(dbGetIssuercategorywise, "@subCategoryCode", DbType.String, category);
                dsGetIssuercategorywise = db.ExecuteDataSet(dbGetIssuercategorywise);
                dtGetIssuercategorywise = dsGetIssuercategorywise.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetIssuercategorywise;
        }
    }
}


