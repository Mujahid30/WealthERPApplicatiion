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
        public DataTable GetMFSchemeRanking(int adviserId)
        {
            Database db;
            DataTable dtSchemeRankList;
            DbCommand cmdSchemeRankList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdSchemeRankList = db.GetStoredProcCommand("SPROC_GetAdviserMFSchemeRankList");
                db.AddInParameter(cmdSchemeRankList, "@AdviserId", DbType.Int32, adviserId);
                dtSchemeRankList = db.ExecuteDataSet(cmdSchemeRankList).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineCommonBackOfficeDao.cs:DataTable GetMFSchemeRanking(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeRankList;

        }
        public DataTable GetSchemeForRank(int adviserId, int amcCode, string Category,Boolean IsEdit)
        {
            Database db;
            DataTable dtGetSchemeForRank;
            DbCommand cmdGetSchemeForRank;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSchemeForRank = db.GetStoredProcCommand("SPROC_ONL_GetSchemesAvailableForRank");
                db.AddInParameter(cmdGetSchemeForRank, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetSchemeForRank, "@AmcCode", DbType.Int32, amcCode);
                db.AddInParameter(cmdGetSchemeForRank, "@Category", DbType.String, Category);
                db.AddInParameter(cmdGetSchemeForRank, "@IsEdit", DbType.Boolean, IsEdit);
                dtGetSchemeForRank = db.ExecuteDataSet(cmdGetSchemeForRank).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineCommonBackOfficeDao.cs:GetSchemeForRank");
                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = amcCode;
                objects[2] = Category;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetSchemeForRank;
        }
        public DataTable GetCategorySchemeRank(string category,int adviserId)
        {
            Database db;
            DataTable dtCategorySchemeRank;
            DbCommand cmdCategorySchemeRank;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCategorySchemeRank = db.GetStoredProcCommand("SPROC_ONL_GetAvailableCategorySchemeRankList");
                db.AddInParameter(cmdCategorySchemeRank, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdCategorySchemeRank, "@Category", DbType.String, category);
                dtCategorySchemeRank = db.ExecuteDataSet(cmdCategorySchemeRank).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineCommonBackOfficeDao.cs:GetCategorySchemeRank(string category,int adviserId)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = category;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtCategorySchemeRank;
        }
        public int CUDSchemeRanking(int adviserId, int amcCode, int schemePlanCode, string Category, int schemeRank, int opType, int RankId)
        {
            Database db;
            DbCommand CmdCUDSchemeRanking;
            DataSet dsIssueBidList = new DataSet();
            int result = 0;
            try
            {
               
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdCUDSchemeRanking = db.GetStoredProcCommand("SPROC_ONL_CUDMFSchemeRanking");
                db.AddInParameter(CmdCUDSchemeRanking, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(CmdCUDSchemeRanking, "@AmcCode", DbType.Int32, amcCode);
                db.AddInParameter(CmdCUDSchemeRanking, "@SchemePlanCode", DbType.Int32, schemePlanCode);
                db.AddInParameter(CmdCUDSchemeRanking, "@Category", DbType.String, Category);
                db.AddInParameter(CmdCUDSchemeRanking, "@SchemeRank", DbType.Int32, schemeRank);
                db.AddInParameter(CmdCUDSchemeRanking, "@OpType", DbType.Int16, opType);
                db.AddInParameter(CmdCUDSchemeRanking, "@RankId", DbType.Int32, RankId);
                if (db.ExecuteNonQuery(CmdCUDSchemeRanking) != 0)
                    result = 1;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs:CUDSchemeRanking(int adviserId, int amcCode, int schemePlanCode, string Category, int schemeRank,int opType)");
                object[] objects = new object[6];
                objects[0] = adviserId;
                objects[1] = amcCode;
                objects[2] = schemePlanCode;
                objects[3] = Category;
                objects[4] = schemeRank;
                objects[5] = opType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;

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
