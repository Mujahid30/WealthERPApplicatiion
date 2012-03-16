using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCommon;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoCommon
{
    public class RepositoryDao
    {
        public DataSet GetRepositoryView(int intUserId, string strRoleList)
        {
            Database db;
            DbCommand cmdGetRepositoryView;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRepositoryView = db.GetStoredProcCommand("sproc_Repository_GetRepositoryView");
                db.AddInParameter(cmdGetRepositoryView, "@userId", DbType.Int32, intUserId);
                db.AddInParameter(cmdGetRepositoryView, "@roleList", DbType.String, strRoleList);
                ds = db.ExecuteDataSet(cmdGetRepositoryView);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryDao.cs:GetRepositoryView(int intUserId, string strRoleList)");
                object[] objects = new object[1];
                objects[0] = intUserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetRepositoryCategory()
        {
            Database db;
            DbCommand cmdGetRepositoryCategory;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRepositoryCategory = db.GetStoredProcCommand("sproc_Repository_GetRepositoryCategory");
                ds = db.ExecuteDataSet(cmdGetRepositoryCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryDao.cs:GetRepositoryCategory()");
                object[] objects = new object[1];
                objects[0] = "";
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public bool AddRepositoryItem(RepositoryVo repoVo)
        {
            Database db;
            DbCommand cmdAddRepository;
            bool blResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAddRepository = db.GetStoredProcCommand("sproc_Repository_AddRepositoryItem");
                db.AddInParameter(cmdAddRepository, "@adviserId", DbType.Int32, repoVo.AdviserId);
                db.AddInParameter(cmdAddRepository, "@repoCatCode", DbType.String, repoVo.CategoryCode);
                db.AddInParameter(cmdAddRepository, "@repoHeadingText", DbType.String, repoVo.HeadingText);
                db.AddInParameter(cmdAddRepository, "@repoDescription", DbType.String, repoVo.Description);
                db.AddInParameter(cmdAddRepository, "@repoIsFile", DbType.Boolean, repoVo.IsFile);
                db.AddInParameter(cmdAddRepository, "@repoLink", DbType.String, repoVo.Link);

                db.ExecuteNonQuery(cmdAddRepository);
                blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryDao.cs:AddRepositoryItem(RepositoryVo repoVo)");
                object[] objects = new object[1];
                objects[0] = repoVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public DataSet GetAdviserRepositoryView(int intAdviserId)
        {
            Database db;
            DbCommand cmdGetAdviserRepositoryView;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAdviserRepositoryView = db.GetStoredProcCommand("sproc_Repository_GetAdviserRepositoryView");
                db.AddInParameter(cmdGetAdviserRepositoryView, "@adviserId", DbType.Int32, intAdviserId);
                ds = db.ExecuteDataSet(cmdGetAdviserRepositoryView);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryDao.cs:GetAdviserRepositoryView(int intAdviserId)");
                object[] objects = new object[1];
                objects[0] = intAdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public RepositoryVo GetRepositoryItem(int intRepositoryId)
        {
            Database db;
            DbCommand cmdGetRepositoryItem;
            DataSet ds = null;
            RepositoryVo repoVo = new RepositoryVo();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRepositoryItem = db.GetStoredProcCommand("sproc_Repository_GetRepositoryItem");
                db.AddInParameter(cmdGetRepositoryItem, "@repositoryId", DbType.Int32, intRepositoryId);
                ds = db.ExecuteDataSet(cmdGetRepositoryItem);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    repoVo.RepositoryId = intRepositoryId;
                    repoVo.AdviserId = Int32.Parse(ds.Tables[0].Rows[0]["A_AdviserId"].ToString());
                    repoVo.CategoryCode = ds.Tables[0].Rows[0]["XRC_RepositoryCategoryCode"].ToString();
                    repoVo.Description = ds.Tables[0].Rows[0]["AR_Description"].ToString();
                    repoVo.HeadingText = ds.Tables[0].Rows[0]["AR_HeadingText"].ToString();
                    repoVo.IsFile = Boolean.Parse(ds.Tables[0].Rows[0]["AR_IsFile"].ToString());
                    if (repoVo.IsFile)
                        repoVo.Link = ds.Tables[0].Rows[0]["AR_Filename"].ToString();
                    else
                        repoVo.Link = ds.Tables[0].Rows[0]["AR_Link"].ToString();
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
                FunctionInfo.Add("Method", "RepositoryDao.cs:GetRepositoryItem(int intRepositoryId)");
                object[] objects = new object[2];
                objects[0] = intRepositoryId;
                objects[1] = repoVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return repoVo;
        }

        public bool UpdateRepositoryItem(RepositoryVo repoVo)
        {
            Database db;
            DbCommand cmdUpdateRepositoryItem;
            bool blResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateRepositoryItem = db.GetStoredProcCommand("sproc_Repository_UpdateRepositoryItem");
                db.AddInParameter(cmdUpdateRepositoryItem, "@repoId", DbType.Int32, repoVo.RepositoryId);
                db.AddInParameter(cmdUpdateRepositoryItem, "@repoHeadingText", DbType.String, repoVo.HeadingText);
                db.AddInParameter(cmdUpdateRepositoryItem, "@repoDescription", DbType.String, repoVo.Description);
                if (repoVo.Link != String.Empty)
                    db.AddInParameter(cmdUpdateRepositoryItem, "@repoLink", DbType.String, repoVo.Link);
                else
                    db.AddInParameter(cmdUpdateRepositoryItem, "@repoLink", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(cmdUpdateRepositoryItem);
                blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryDao.cs:UpdateRepositoryItem(RepositoryVo repoVo)");
                object[] objects = new object[1];
                objects[0] = repoVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }
    }
}
