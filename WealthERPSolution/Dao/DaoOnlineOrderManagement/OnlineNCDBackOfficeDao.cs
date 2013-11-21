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


namespace DaoOnlineOrderManagement
{
    public class OnlineNCDBackOfficeDao
    {
        public DataSet GetSeriesCategories(string issuerId, int issueId, int seriesId)
        {
            DataSet dsGetSeriesCategories;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSeriesCategories");
                db.AddInParameter(dbCommand, "@issuerId", DbType.String, issuerId);
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
        public int CreateIssue(OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            int issueId;
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateIssueMaster");

                db.AddInParameter(createCmd, "@PAG_AssetGroupCode", DbType.String, onlineNCDBackOfficeVo.AssetGroupCode);
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, "FICG");
                db.AddInParameter(createCmd, "@AIM_IssueName", DbType.String, onlineNCDBackOfficeVo.IssueName);
                db.AddInParameter(createCmd, "@PFIIM_IssuerId", DbType.String, onlineNCDBackOfficeVo.IssuerId);
                db.AddInParameter(createCmd, "@AIM_InitialChequeNo", DbType.String, onlineNCDBackOfficeVo.InitialChequeNo);
                db.AddInParameter(createCmd, "@AIM_FaceValue", DbType.Decimal, onlineNCDBackOfficeVo.FaceValue);
                db.AddInParameter(createCmd, "@FloorPrice", DbType.Decimal, onlineNCDBackOfficeVo.FloorPrice);
                db.AddInParameter(createCmd, "@AIM_ModeOfIssue", DbType.String, onlineNCDBackOfficeVo.ModeOfIssue);
                db.AddInParameter(createCmd, "@AIM_ModeOfTrading", DbType.String, onlineNCDBackOfficeVo.ModeOfTrading);
                db.AddInParameter(createCmd, "@AIM_OpenDate", DbType.Date, onlineNCDBackOfficeVo.OpenDate);
                db.AddInParameter(createCmd, "@AIM_CloseDate", DbType.Date, onlineNCDBackOfficeVo.CloseDate);
                db.AddInParameter(createCmd, "@AIM_OpenTime", DbType.String, onlineNCDBackOfficeVo.OpenTime);
                db.AddInParameter(createCmd, "@AIM_CloseTime", DbType.String, onlineNCDBackOfficeVo.CloseTime);
                //db.AddInParameter(createCmd, "@IssueRevis", DbType.Date, onlineNCDBackOfficeVo.IssueRevis);
                db.AddInParameter(createCmd, "@TradingLot", DbType.Int32, onlineNCDBackOfficeVo.TradingLot);
                db.AddInParameter(createCmd, "@BiddingLot", DbType.Int32, onlineNCDBackOfficeVo.BiddingLot);
                db.AddInParameter(createCmd, "@AIM_MinApplicationSize", DbType.Int32, onlineNCDBackOfficeVo.MinApplicationSize);
                db.AddInParameter(createCmd, "@IsPrefix", DbType.String, onlineNCDBackOfficeVo.IsPrefix);
                db.AddInParameter(createCmd, "@AIM_TradingInMultipleOf", DbType.Int32, onlineNCDBackOfficeVo.TradingInMultipleOf);
                db.AddInParameter(createCmd, "@AIM_ListedInExchange", DbType.String, onlineNCDBackOfficeVo.ListedInExchange);
                db.AddInParameter(createCmd, "@AIM_BankName", DbType.String, onlineNCDBackOfficeVo.BankName);
                db.AddInParameter(createCmd, "@AIM_BankBranch", DbType.String, onlineNCDBackOfficeVo.BankBranch);
                db.AddInParameter(createCmd, "@AIM_PutCallOption", DbType.String, onlineNCDBackOfficeVo.PutCallOption);
                db.AddOutParameter(createCmd, "@AIM_IssueId", DbType.Int32, 0);
                db.AddInParameter(createCmd, "@FromRange", DbType.Int32, onlineNCDBackOfficeVo.FromRange);
                db.AddInParameter(createCmd, "@ToRange", DbType.Int32, onlineNCDBackOfficeVo.ToRange);
                db.AddInParameter(createCmd, "@IsActive", DbType.Int32, onlineNCDBackOfficeVo.IsActive);
                db.AddInParameter(createCmd, "@IsNominationRequired", DbType.Int32, onlineNCDBackOfficeVo.IsNominationRequired);


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
                db.AddInParameter(createCmd, "@SeriesName", DbType.String, onlineNCDBackOfficeVo.SeriesName);
                db.AddInParameter(createCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                db.AddInParameter(createCmd, "@IsBuyBackAvailable", DbType.Int32, onlineNCDBackOfficeVo.IsBuyBackAvailable);
                db.AddInParameter(createCmd, "@Tenure", DbType.String, onlineNCDBackOfficeVo.Tenure);
                db.AddInParameter(createCmd, "@InterestFrequency", DbType.Int32, onlineNCDBackOfficeVo.InterestFrequency);
                db.AddInParameter(createCmd, "@InterestType", DbType.String, onlineNCDBackOfficeVo.InterestType);


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
        public DataSet GetSeries(string issuerId, int issueId)
        {
            DataSet dsGetSeriesCategories;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSeries");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@issuerId", DbType.String, issuerId);
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
        public DataSet GetAllInvestorTypes(string issuerId, int issueId)
        {
            DataSet dsGetSubCategory;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAllInvestorTypes");
                db.AddInParameter(dbCommand, "@issuerId", DbType.String, issuerId);
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

        public DataSet GetSubCategory(string issuerId, int issueId)
        {
            DataSet dsGetSubCategory;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetInvestorTypes");
                db.AddInParameter(dbCommand, "@issuerId", DbType.String, issuerId);
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
        public DataSet GetEligibleInvestorsCategory(string issuerId, int issueId)
        {
            DataSet dsGetEligibleInvestorsCategory;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetEligibleInvestorsCategory");
                db.AddInParameter(dbCommand, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(dbCommand, "@issuerId", DbType.String, issuerId);

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
        public DataSet GetCategory(string issuerId, int issueId)
        {
            DataSet dsGetSubCategory;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetCategories");
                db.AddInParameter(dbCommand, "@issuerId", DbType.String, issuerId);
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
                db.AddInParameter(updateCmd, "@SeriesName", DbType.String, onlineNCDBackOfficeVo.SeriesName);
                db.AddInParameter(updateCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                db.AddInParameter(updateCmd, "@IsBuyBackAvailable", DbType.Int32, onlineNCDBackOfficeVo.IsBuyBackAvailable);
                db.AddInParameter(updateCmd, "@Tenure", DbType.String, onlineNCDBackOfficeVo.Tenure);
                db.AddInParameter(updateCmd, "@InterestFrequency", DbType.Int32, onlineNCDBackOfficeVo.InterestFrequency);
                db.AddInParameter(updateCmd, "@InterestType", DbType.String, onlineNCDBackOfficeVo.InterestType);

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
        public DataSet GetIssuerIssue(string issuerID)
        {
            DataSet dsGetIssuerIssue;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetIssuerIssue");
                db.AddInParameter(dbCommand, "@issuerID", DbType.String, issuerID);
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
                objects[1] = issuerID;
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
    }
}
