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



namespace DaoOnlineOrderManagement
{
    public class OnlineNCDBackOfficeDao
    {

        public DataSet GetIssueDetails(int issueId, int adviserId)
        {
            DataSet dsIssueDetails;
            Database db;
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
            Database db;
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

        public int GetSeriesSequence(int issueId, int adviserId)
        {
            Database db;
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
        public int ChekSeriesSequence(int seqNo,int issueId, int adviserId)
        {           
            Database db;
            DbCommand dbCommand;
            int DupseqNo=0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_ChekSeriesSequence");
                db.AddInParameter(dbCommand, "@SequenceNo", DbType.Int32, seqNo);
                db.AddInParameter(dbCommand, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, adviserId);

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
        public DataSet GetAdviserIssueList(DateTime date, int type, string product,int adviserId)
        {
            DataSet dsIssueDetails;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAdviserIssueList");
                db.AddInParameter(dbCommand, "@date", DbType.Date, date);
                db.AddInParameter(dbCommand, "@type", DbType.Int32, type);
                db.AddInParameter(dbCommand, "@product", DbType.String, product);
                db.AddInParameter(dbCommand, "@adviserId", DbType.String, adviserId);                
               
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

        public int UpdateIssue(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo)
        {
            int issueId;
            Database db;
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
                db.AddInParameter(createCmd, "@IsPrefix", DbType.String, onlineNCDBackOfficeVo.IsPrefix);
                db.AddInParameter(createCmd, "@AIM_TradingInMultipleOf", DbType.Int32, onlineNCDBackOfficeVo.TradingInMultipleOf);
                //db.AddInParameter(createCmd, "@AIM_ListedInExchange", DbType.String, onlineNCDBackOfficeVo.ListedInExchange);
                db.AddInParameter(createCmd, "@AIM_BankName", DbType.String, onlineNCDBackOfficeVo.BankName);
                db.AddInParameter(createCmd, "@AIM_BankBranch", DbType.String, onlineNCDBackOfficeVo.BankBranch);
                db.AddInParameter(createCmd, "@AIM_PutCallOption", DbType.String, onlineNCDBackOfficeVo.PutCallOption);
                db.AddOutParameter(createCmd, "@AIM_IssueId", DbType.Int32, 0);
                db.AddInParameter(createCmd, "@FromRange", DbType.Int32, onlineNCDBackOfficeVo.FromRange);
                db.AddInParameter(createCmd, "@ToRange", DbType.Int32, onlineNCDBackOfficeVo.ToRange);
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
                db.AddInParameter(createCmd, "@RtaSourceCode", DbType.String, onlineNCDBackOfficeVo.RtaSourceCode);
                db.AddInParameter(createCmd, "@MaxQty", DbType.Int32, onlineNCDBackOfficeVo.MaxQty);
                db.AddInParameter(createCmd, "@IssueSizeQty", DbType.Int32, onlineNCDBackOfficeVo.IssueSizeQty);
                db.AddInParameter(createCmd, "@IssueSizeAmt", DbType.Decimal, onlineNCDBackOfficeVo.IssueSizeAmt);
                db.AddInParameter(createCmd, "@issueID", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                //db.AddInParameter(createCmd, "@IsListedinBSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinBSE); 
                //db.AddInParameter(createCmd, "@IsListedinNSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinNSE); 
                //db.AddInParameter(createCmd, "@NSECode", DbType.String, onlineNCDBackOfficeVo.NSECode);
                //db.AddInParameter(createCmd, "@BSECode", DbType.String, onlineNCDBackOfficeVo.BSECode);
                //db.AddInParameter(createCmd, "@adviserId", DbType.Int32, adviserId);  

                //db.AddInParameter(createCmd, "@issueID", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                //db.AddInParameter(createCmd, "@AIM_IssueName", DbType.String, onlineNCDBackOfficeVo.IssueName);
                //db.AddInParameter(createCmd, "@PI_IssuerId", DbType.String, onlineNCDBackOfficeVo.IssuerId );
                //db.AddInParameter(createCmd, "@AIM_InitialChequeNo", DbType.String, onlineNCDBackOfficeVo.InitialChequeNo);
                //db.AddInParameter(createCmd, "@AIM_FaceValue", DbType.Decimal, onlineNCDBackOfficeVo.FaceValue);
                //db.AddInParameter(createCmd, "@FloorPrice", DbType.Decimal, onlineNCDBackOfficeVo.FloorPrice);
                //db.AddInParameter(createCmd, "@FixedPrice", DbType.Decimal, onlineNCDBackOfficeVo.FixedPrice);
                //db.AddInParameter(createCmd, "@AIM_ModeOfIssue", DbType.String, onlineNCDBackOfficeVo.ModeOfIssue);
                //db.AddInParameter(createCmd, "@AIM_ModeOfTrading", DbType.String, onlineNCDBackOfficeVo.ModeOfTrading);
                //db.AddInParameter(createCmd, "@AIM_OpenDate", DbType.Date, onlineNCDBackOfficeVo.OpenDate);
                //db.AddInParameter(createCmd, "@AIM_CloseDate", DbType.Date, onlineNCDBackOfficeVo.CloseDate);
                //db.AddInParameter(createCmd, "@AIM_OpenTime", DbType.Time, onlineNCDBackOfficeVo.OpenTime);
                //db.AddInParameter(createCmd, "@AIM_CloseTime", DbType.Time, onlineNCDBackOfficeVo.CloseTime);
                //db.AddInParameter(createCmd, "@TradingLot", DbType.Int32, onlineNCDBackOfficeVo.TradingLot);
                //db.AddInParameter(createCmd, "@BiddingLot", DbType.Int32, onlineNCDBackOfficeVo.BiddingLot);
                //db.AddInParameter(createCmd, "@AIM_MinApplicationSize", DbType.Int32, onlineNCDBackOfficeVo.MinApplicationSize);
                //db.AddInParameter(createCmd, "@IsPrefix", DbType.String, onlineNCDBackOfficeVo.IsPrefix);
                //db.AddInParameter(createCmd, "@AIM_TradingInMultipleOf", DbType.Int32, onlineNCDBackOfficeVo.TradingInMultipleOf);
                ////db.AddInParameter(createCmd, "@AIM_ListedInExchange", DbType.String, onlineNCDBackOfficeVo.ListedInExchange);
                //db.AddInParameter(createCmd, "@AIM_BankName", DbType.String, onlineNCDBackOfficeVo.BankName);
                //db.AddInParameter(createCmd, "@AIM_BankBranch", DbType.String, onlineNCDBackOfficeVo.BankBranch);
                //db.AddInParameter(createCmd, "@AIM_PutCallOption", DbType.String, onlineNCDBackOfficeVo.PutCallOption);
                //db.AddOutParameter(createCmd, "@AIM_IssueId", DbType.Int32, 0);
                //db.AddInParameter(createCmd, "@FromRange", DbType.Int32, onlineNCDBackOfficeVo.FromRange);
                //db.AddInParameter(createCmd, "@ToRange", DbType.Int32, onlineNCDBackOfficeVo.ToRange);
                //db.AddInParameter(createCmd, "@IsActive", DbType.Int32, onlineNCDBackOfficeVo.IsActive);
                //db.AddInParameter(createCmd, "@IsNominationRequired", DbType.Int32, onlineNCDBackOfficeVo.IsNominationRequired);

                //db.AddInParameter(createCmd, "@IsListedinBSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinBSE);
                //db.AddInParameter(createCmd, "@IsListedinNSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinNSE);
                //db.AddInParameter(createCmd, "@BSECode", DbType.String, onlineNCDBackOfficeVo.BSECode);
                //db.AddInParameter(createCmd, "@NSECode", DbType.String, onlineNCDBackOfficeVo.NSECode);  

                issueId = db.ExecuteNonQuery(createCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return issueId;
        }

        public DataSet GetSeriesCategories(int  issuerId, int issueId, int seriesId)
        {
            DataSet dsGetSeriesCategories;
            Database db;
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

        public DataSet GetIssuer()
        {
            DataSet dsGetSeriesCategories;
            Database db;
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
        public DataSet GetSyndicateMaster()
        {
            DataSet dsGetSeriesCategories;
            Database db;
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
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateUpdateDeletetIssuer");
                db.AddInParameter(createCmd, "@CommandType", DbType.String, commandType);
                db.AddInParameter(createCmd, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(createCmd, "@IssuerName", DbType.String,issuerName);
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
        public int CreateUpdateDeleteSyndicateMaster(int syndicateId, string syndicateCode, string syndicateName, string commandType)
        {
            int issueId;
            Database db;
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

        public int CreateIssue(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int adviserId)
        {
            int issueId;
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateIssueMaster");
                //db.AddInParameter(createCmd, "@PAG_AssetGroupCode", DbType.String, onlineNCDBackOfficeVo.AssetGroupCode);
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, onlineNCDBackOfficeVo.AssetInstrumentCategoryCode);
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentSubCategoryCode", DbType.String, onlineNCDBackOfficeVo.AssetInstrumentSubCategoryCode);
                db.AddInParameter(createCmd, "@AIM_IssueName", DbType.String, onlineNCDBackOfficeVo.IssueName);
                db.AddInParameter(createCmd, "@PFIIM_IssuerId", DbType.String, onlineNCDBackOfficeVo.IssuerId );
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
                db.AddInParameter(createCmd, "@IsPrefix", DbType.String, onlineNCDBackOfficeVo.IsPrefix);
                db.AddInParameter(createCmd, "@AIM_TradingInMultipleOf", DbType.Int32, onlineNCDBackOfficeVo.TradingInMultipleOf);
                //db.AddInParameter(createCmd, "@AIM_ListedInExchange", DbType.String, onlineNCDBackOfficeVo.ListedInExchange);
                db.AddInParameter(createCmd, "@AIM_BankName", DbType.String, onlineNCDBackOfficeVo.BankName);
                db.AddInParameter(createCmd, "@AIM_BankBranch", DbType.String, onlineNCDBackOfficeVo.BankBranch);
                db.AddInParameter(createCmd, "@AIM_PutCallOption", DbType.String, onlineNCDBackOfficeVo.PutCallOption);
                db.AddOutParameter(createCmd, "@AIM_IssueId", DbType.Int32, 0);
                db.AddInParameter(createCmd, "@FromRange", DbType.Int32, onlineNCDBackOfficeVo.FromRange);
                db.AddInParameter(createCmd, "@ToRange", DbType.Int32, onlineNCDBackOfficeVo.ToRange);
                db.AddInParameter(createCmd, "@IsActive", DbType.Int32, onlineNCDBackOfficeVo.IsActive);
                db.AddInParameter(createCmd, "@IsNominationRequired", DbType.Int32, onlineNCDBackOfficeVo.IsNominationRequired);

                db.AddInParameter(createCmd, "@IsListedinBSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinBSE);
                db.AddInParameter(createCmd, "@IsListedinNSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinNSE);
                db.AddInParameter(createCmd, "@BSECode", DbType.String, onlineNCDBackOfficeVo.BSECode);                
                db.AddInParameter(createCmd, "@NSECode", DbType.String, onlineNCDBackOfficeVo.NSECode);
                db.AddInParameter(createCmd, "@Rating", DbType.String, onlineNCDBackOfficeVo.Rating);


                db.AddInParameter(createCmd, "@IsBookBuilding", DbType.Int32, onlineNCDBackOfficeVo.IsBookBuilding );
                db.AddInParameter(createCmd, "@BookBuildingPercentage", DbType.Double, onlineNCDBackOfficeVo.BookBuildingPercentage);
                db.AddInParameter(createCmd, "@CapPrice", DbType.Double, onlineNCDBackOfficeVo.CapPrice);
                db.AddInParameter(createCmd, "@NoOfBidAllowed", DbType.Int32, onlineNCDBackOfficeVo.NoOfBidAllowed);
                db.AddInParameter(createCmd, "@RtaSourceCode", DbType.String, onlineNCDBackOfficeVo.RtaSourceCode);
                db.AddInParameter(createCmd, "@MaxQty", DbType.Int32, onlineNCDBackOfficeVo.MaxQty);
                db.AddInParameter(createCmd, "@IssueSizeQty", DbType.Int32, onlineNCDBackOfficeVo.IssueSizeQty);
                db.AddInParameter(createCmd, "@IssueSizeAmt", DbType.Decimal, onlineNCDBackOfficeVo.IssueSizeAmt); 
                //db.AddInParameter(createCmd, "@IsListedinBSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinBSE); 
                //db.AddInParameter(createCmd, "@IsListedinNSE", DbType.Int32, onlineNCDBackOfficeVo.IsListedinNSE); 
                //db.AddInParameter(createCmd, "@NSECode", DbType.String, onlineNCDBackOfficeVo.NSECode);
                //db.AddInParameter(createCmd, "@BSECode", DbType.String, onlineNCDBackOfficeVo.BSECode);
                db.AddInParameter(createCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(createCmd, "@Tradableexchane", DbType.Int32, onlineNCDBackOfficeVo.TradableExchange);

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
            Database db;
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
            Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateSeriesCategories");
                db.AddInParameter(createCmd, "@SeriesId", DbType.Int32, onlineNCDBackOfficeVo.SeriesId);
                db.AddInParameter(createCmd, "@InvestorCatgeoryId", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@DefaultInterestRate", DbType.Double, onlineNCDBackOfficeVo.DefaultInterestRate);
                db.AddInParameter(createCmd, "@AnnualizedYieldUpto", DbType.Double, onlineNCDBackOfficeVo.AnnualizedYieldUpto);
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

        public DataSet GetSeries(int  issuerId, int issueId)
        {
            DataSet dsGetSeriesCategories;
            Database db;
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
        public DataSet BindRta()
        {
            DataSet dsGetSeriesCategories;
            Database db;
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
        public DataSet GetAllInvestorTypes(int  issuerId, int issueId)
        {
            DataSet dsGetSubCategory;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAllInvestorTypes");
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
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

        public DataSet GetSubCategory(int  issuerId, int issueId)
        {
            DataSet dsGetSubCategory;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetInvestorTypes");
                db.AddInParameter(dbCommand, "@issuerId", DbType.Int32, issuerId);
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, issueId);
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

        public DataSet GetEligibleInvestorsCategory(int  issuerId, int issueId)
        {
            DataSet dsGetEligibleInvestorsCategory;
            Database db;
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
            Database db;
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
                db.AddInParameter(createCmd, "@MInBidAmount", DbType.Int32, onlineNCDBackOfficeVo.MInBidAmount);
                db.AddInParameter(createCmd, "@MaxBidAmount", DbType.Int32, onlineNCDBackOfficeVo.MaxBidAmount);
                db.AddInParameter(createCmd, "@PriceDiscountType", DbType.String, onlineNCDBackOfficeVo.DiscuountType);
                db.AddInParameter(createCmd, "@PriceDiscountValue", DbType.Decimal, onlineNCDBackOfficeVo.DiscountValue);

                
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

        public DataSet GetCategory(int  issuerId, int issueId)
        {
            DataSet dsGetSubCategory;
            Database db;
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
            Database db;
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
                db.AddInParameter(createCmd, "@MInBidAmount", DbType.Int32, onlineNCDBackOfficeVo.MInBidAmount);
                db.AddInParameter(createCmd, "@MaxBidAmount", DbType.Int32, onlineNCDBackOfficeVo.MaxBidAmount);
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
            Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateInvestorsInvestorSubTypePerCategory");
                db.AddInParameter(createCmd, "@InvestorCatgeoryId ", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@InvestorId", DbType.Int32, onlineNCDBackOfficeVo.LookUpId);
                db.AddInParameter(createCmd, "@InvestorSubTypeCode", DbType.String, onlineNCDBackOfficeVo.SubCatgeoryTypeCode);
                db.AddInParameter(createCmd, "@MinInvestmentAmount", DbType.Int32, onlineNCDBackOfficeVo.MinInvestmentAmount);
                db.AddInParameter(createCmd, "@MaxInvestmentAmount", DbType.Int32, onlineNCDBackOfficeVo.MaxInvestmentAmount);

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
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("[SPROC_UpdateCategoryDetails]");
                db.AddInParameter(createCmd, "@InvestorCatgeoryId ", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@InvestorId", DbType.Int32, onlineNCDBackOfficeVo.LookUpId);
                db.AddInParameter(createCmd, "@InvestorSubTypeCode", DbType.String, onlineNCDBackOfficeVo.SubCatgeoryTypeCode);
                db.AddInParameter(createCmd, "@MinInvestmentAmount", DbType.Int32, onlineNCDBackOfficeVo.MinInvestmentAmount);
                db.AddInParameter(createCmd, "@MaxInvestmentAmount", DbType.Int32, onlineNCDBackOfficeVo.MaxInvestmentAmount);

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
            Database db;
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
            Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("[SPROC_UpdateSeriesCategories]");
                db.AddInParameter(createCmd, "@SeriesId", DbType.Int32, onlineNCDBackOfficeVo.SeriesId);
                db.AddInParameter(createCmd, "@InvestorCatgeoryId", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@DefaultInterestRate", DbType.Double, onlineNCDBackOfficeVo.DefaultInterestRate);
                db.AddInParameter(createCmd, "@AnnualizedYieldUpto", DbType.Double, onlineNCDBackOfficeVo.AnnualizedYieldUpto);

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
            Database db;
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
            Database db;
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

        public DataSet GetIssuerIssue(int  issuerId)
        {
            DataSet dsGetIssuerIssue;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetIssuerIssues");
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, issuerId);
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
                objects[1] = issuerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetIssuerIssue;
        }
        public DataSet GetUploadIssue(string product,int adviserId)
        {
            DataSet dsGetIssuerIssue;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetUploadIssue");
                db.AddInParameter(dbCommand, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@product", DbType.String, product);

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
                objects[1] = product;
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
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetCategoryDetails");
                db.AddInParameter(dbCommand, "@AIIC_InvestorCatgeoryId", DbType.Int32, investorCatgeoryId);
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

        public void GenereateNcdExtract(int AdviserId, int UserId, string SourceCode, string ProductAsset)
        {
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_NCDOrderExtract");
                db.AddInParameter(cmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(cmd, "@U_UserId", DbType.Int32, UserId);
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, SourceCode);
                db.AddInParameter(cmd, "@PAG_AssetGroupCode", DbType.String, ProductAsset);
                db.ExecuteDataSet(cmd);
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

        public DataSet GetOnlineNcdExtractPreview(DateTime Today, int AdviserId, int FileType)
        {
            Database db;
            DataSet dsGetOnlineNCDExtractPreview;
            DbCommand GetOnlineNCDExtractPreviewcmd;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOnlineNCDExtractPreviewcmd = db.GetStoredProcCommand("SPROC_ONL_IssueExtractPreview");
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@Today", DbType.DateTime, Today);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@WIFT_Id", DbType.Int32, FileType);
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
        public DataTable GetAdviserNCDOrderBook(int adviserId,string status, DateTime dtFrom, DateTime dtTo)
        {
            Database db;
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
                db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(cmd, "@ToDate", DbType.DateTime, dtTo);
                dsNCDOrder=db.ExecuteDataSet(cmd);
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
            Database db;
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
            Database db;
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
            Database db;
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
        public void UpdateNcdOrderMannualMatch(int orderId, int allotmentId,ref int isAllotmented,ref int isUpdated)
        {
            Database db;
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

        public void  UpdateNcdAutoMatch(int orderId,int applictionNo,string dpId,ref int isAllotmented,ref int isUpdated)
        {
            Database db;
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
            Database db;
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
        public DataSet GetAdviserOrders(int IssueId, string Product, string Status, DateTime FromDate, DateTime ToDate,int adviserid)
        {
            DataSet dsOrders;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAdviserWiseOrders");
                db.AddInParameter(dbCommand, "@IssueId", DbType.Int32, IssueId);
                db.AddInParameter(dbCommand, "@Product", DbType.String, Product);
                db.AddInParameter(dbCommand, "@Status", DbType.String, Status);
                db.AddInParameter(dbCommand, "@FromDate", DbType.Date, FromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.Date, ToDate);
                db.AddInParameter(dbCommand, "@AdviserId", DbType.Int32, adviserid);

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
        public DataSet GetAdviserissueallotmentList(int adviserId,int issureid,string type,DateTime fromdate,DateTime todate)
        {

            Database db;
            DataSet dsGetAdviserissueallotmentList;
            DbCommand GetAdviserissueallotmentListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserissueallotmentListcmd = db.GetStoredProcCommand("SPROC_GetAdviserIssueAllotmentDetail");
                db.AddInParameter(GetAdviserissueallotmentListcmd, "@AdviserId", DbType.Int32, adviserId);
                if(issureid!=0)
                db.AddInParameter(GetAdviserissueallotmentListcmd, "@Issuerid", DbType.Int32,issureid);
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
            Database db;
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
        public DataTable GetFrequency()
        {
            DataSet dsGetIssuerid;
            DataTable dt;
            Database db;
            DbCommand GetIssueridCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIssueridCmd = db.GetStoredProcCommand("SPROC_GetFrequency");               
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

        public int UploadAllotmentIssueData(DataTable dtData, int issueId)
        {
             int result;
            try
            {

                string conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand cmdProc = new SqlCommand("SPROC_UploadAllotmentIssueData", sqlCon);
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
                for(int i = 0; i < Params.Length; i++){
                    SqlDbType sqlType = GetSqlDbType(ParamsType[i]);
                    SqlParameter param = cmdUpdate.Parameters.Add("@" + Params[i], sqlType, 50, Params[i]);
                    param.SourceVersion = DataRowVersion.Original;
                }
               // }

                //DataSet ds = new DataSet("AdviserIssueOrderExtract");
                //ds.Tables.Add(dtData);

                foreach (DataRow row in dtData.Rows) {
                    if (row.RowState == DataRowState.Unchanged) {
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
    }
}
