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
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSeriesCategories()");
                object[] objects = new object[0];               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSeriesCategories;
        }
        public int CreateIssue(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            int issueId;
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateIssueMaster");

                db.AddInParameter(createCmd, "@PAG_AssetGroupCode", DbType.String, onlineNCDBackOfficeVo.AssetGroupCode );
                db.AddInParameter(createCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, "FICG");
                db.AddInParameter(createCmd, "@AIM_IssueName", DbType.String, onlineNCDBackOfficeVo.IssueName);
                db.AddInParameter(createCmd, "@PFIIM_IssuerId", DbType.String, onlineNCDBackOfficeVo.IssuerId);
                db.AddInParameter(createCmd, "@AIM_InitialChequeNo", DbType.String, onlineNCDBackOfficeVo.InitialChequeNo);
                db.AddInParameter(createCmd, "@AIM_FaceValue", DbType.Decimal, onlineNCDBackOfficeVo.FaceValue);
                db.AddInParameter(createCmd, "@FloorPrice", DbType.Decimal, onlineNCDBackOfficeVo.FloorPrice);
                db.AddInParameter(createCmd, "@AIM_ModeOfIssue", DbType.String, onlineNCDBackOfficeVo.ModeOfIssue);
                db.AddInParameter(createCmd, "@AIM_ModeOfTrading", DbType.String , onlineNCDBackOfficeVo.ModeOfTrading);
                db.AddInParameter(createCmd, "@AIM_OpenDate", DbType.Date, onlineNCDBackOfficeVo.OpenDate);
                db.AddInParameter(createCmd, "@AIM_CloseDate", DbType.Date, onlineNCDBackOfficeVo.CloseDate);
                db.AddInParameter(createCmd, "@AIM_OpenTime", DbType.String , onlineNCDBackOfficeVo.OpenTime); //onlineNCDBackOfficeVo.OpenTime.Hours);
                db.AddInParameter(createCmd, "@AIM_CloseTime", DbType.String, onlineNCDBackOfficeVo.CloseTime);// onlineNCDBackOfficeVo.CloseTime.Hours);
                //db.AddInParameter(createCmd, "@IssueRevis", DbType.Date, onlineNCDBackOfficeVo.IssueRevis);
                db.AddInParameter(createCmd, "@TradingLot", DbType.Int32, onlineNCDBackOfficeVo.TradingLot );
                db.AddInParameter(createCmd, "@BiddingLot", DbType.Int32, onlineNCDBackOfficeVo.BiddingLot);
                db.AddInParameter(createCmd, "@AIM_MinApplicationSize", DbType.Int32, onlineNCDBackOfficeVo.MinApplicationSize);
                db.AddInParameter(createCmd, "@IsPrefix", DbType.String, onlineNCDBackOfficeVo.IsPrefix);
                db.AddInParameter(createCmd, "@AIM_TradingInMultipleOf", DbType.Int32, onlineNCDBackOfficeVo.TradingInMultipleOf);
                db.AddInParameter(createCmd, "@AIM_ListedInExchange", DbType.String, onlineNCDBackOfficeVo.ListedInExchange);
                db.AddInParameter(createCmd, "@AIM_BankName", DbType.String, onlineNCDBackOfficeVo.BankName);              
                db.AddInParameter(createCmd, "@AIM_BankBranch", DbType.String , onlineNCDBackOfficeVo.BankBranch);
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

        public int CreateSeries(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
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
        public bool CreateSeriesCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            bool bResult = false;
            //int affectedRecords = 0;
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateSeriesCategories");
                db.AddInParameter(createCmd, "@SeriesId", DbType.Int32, onlineNCDBackOfficeVo.SeriesId);
                //db.AddInParameter(createCmd, "@SeriesName", DbType.String, onlineNCDBackOfficeVo.SeriesName);
                //db.AddInParameter(createCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);

                //db.AddInParameter(createCmd, "@IsBuyBackAvailable", DbType.Int32, onlineNCDBackOfficeVo.IsBuyBackAvailable);
                //db.AddInParameter(createCmd, "@Tenure", DbType.Int32, onlineNCDBackOfficeVo.Tenure);
                //db.AddInParameter(createCmd, "@InterestFrequency", DbType.String, onlineNCDBackOfficeVo.InterestFrequency);
                //db.AddInParameter(createCmd, "@InterestType", DbType.String, onlineNCDBackOfficeVo.InterestType );
                db.AddInParameter(createCmd, "@InvestorCatgeoryId", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                db.AddInParameter(createCmd, "@DefaultInterestRate", DbType.Double, onlineNCDBackOfficeVo.DefaultInterestRate);
                db.AddInParameter(createCmd, "@AnnualizedYieldUpto", DbType.Double, onlineNCDBackOfficeVo.AnnualizedYieldUpto);
                //db.AddInParameter(createCmd, "@RenewCouponRate", DbType.Double, onlineNCDBackOfficeVo.RenewCouponRate);
                //db.AddInParameter(createCmd, "@LockinPeriod", DbType.String, onlineNCDBackOfficeVo.LockinPeriod);
                //db.AddInParameter(createCmd, "@DiscountPrice", DbType.String, onlineNCDBackOfficeVo.DiscountPrice);
                //db.AddInParameter(createCmd, "@IsDiscountAllowed", DbType.Int32, onlineNCDBackOfficeVo.IsDiscountAllowed);       

                if (db.ExecuteNonQuery(createCmd) != 0)
                    //    affectedRecords = int.Parse(db.GetParameterValue(createCmd, "@IsSuccess").ToString());
                    //if (affectedRecords == 1)
                    //    bResult = true;
                    //else
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

        // Invesor types/SubCategories 
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

        public int CreateCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
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
                //db.AddInParameter(createCmd, "@InvestorId", DbType.String, onlineNCDBackOfficeVo.SubCatgeoryId);
                //db.AddInParameter(createCmd, "@InvestorSubTypeCode", DbType.Int32, onlineNCDBackOfficeVo.SubCatgeoryTypeCode);
                //db.AddInParameter(createCmd, "@MinInvestmentAmount", DbType.Double, onlineNCDBackOfficeVo.MinInvestmentAmount);
                //db.AddInParameter(createCmd, "@MaxInvestmentAmount", DbType.Double, onlineNCDBackOfficeVo.MaxInvestmentAmount);
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
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSubCategory()");
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

        public bool CreateSubTypePerCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            bool bResult = false;
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateInvestorsInvestorSubTypePerCategory");
                db.AddInParameter(createCmd, "@InvestorCatgeoryId ", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryId);
                //db.AddInParameter(createCmd, "@IssueId", DbType.Int32, onlineNCDBackOfficeVo.IssueId);
                //db.AddInParameter(createCmd, "@InvestorCatgeoryName", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryName);
                //db.AddInParameter(createCmd, "@InvestorCatgeoryDescription", DbType.Int32, onlineNCDBackOfficeVo.CatgeoryDescription);
                //db.AddInParameter(createCmd, "@ChequePayableTo", DbType.Int32, onlineNCDBackOfficeVo.ChequePayableTo);
                //db.AddInParameter(createCmd, "@MInBidAmount", DbType.Int32, onlineNCDBackOfficeVo.MInBidAmount);
                //db.AddInParameter(createCmd, "@MaxBidAmount", DbType.String, onlineNCDBackOfficeVo.MaxBidAmount);
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

    }
}
