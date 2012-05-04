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

        public DataSet GetRepositoryCategory(int intAdviserId)
        {
            Database db;
            DbCommand cmdGetRepositoryCategory;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRepositoryCategory = db.GetStoredProcCommand("sproc_Repository_GetRepositoryCategory");
                db.AddInParameter(cmdGetRepositoryCategory, "@adviserId", DbType.Int32, intAdviserId);
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
                FunctionInfo.Add("Method", "RepositoryDao.cs:GetRepositoryCategory(int intAdviserId)");
                object[] objects = new object[1];
                objects[0] = intAdviserId;
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
                    repoVo.CategoryCode = ds.Tables[0].Rows[0]["ARC_RepositoryCategoryCode"].ToString();
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

                if (repoVo.IsFile)
                {
                    // File Update
                    if (repoVo.Link != String.Empty)
                        db.AddInParameter(cmdUpdateRepositoryItem, "@repoFile", DbType.String, repoVo.Link);
                    else
                        db.AddInParameter(cmdUpdateRepositoryItem, "@repoFile", DbType.String, DBNull.Value);

                    // Set Link parameter as NULL
                    db.AddInParameter(cmdUpdateRepositoryItem, "@repoLink", DbType.String, DBNull.Value);
                }
                else
                {
                    // Link Update
                    if (repoVo.Link != String.Empty)
                        db.AddInParameter(cmdUpdateRepositoryItem, "@repoLink", DbType.String, repoVo.Link);
                    else
                        db.AddInParameter(cmdUpdateRepositoryItem, "@repoLink", DbType.String, DBNull.Value);

                    // Set File parameter as NULL
                    db.AddInParameter(cmdUpdateRepositoryItem, "@repoFile", DbType.String, DBNull.Value);
                }
                
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

        public bool UpdateRepositoryCategoryNames(int intAdviserId, string strCategoryNames, string strCategoryRoles, int intUserId)
        {
            Database db;
            DbCommand cmdUpdateRepositoryCategoryNames;
            bool blResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateRepositoryCategoryNames = db.GetStoredProcCommand("sproc_Repository_UpdateRepositoryCategoryNames");
                db.AddInParameter(cmdUpdateRepositoryCategoryNames, "@adviserId", DbType.Int32, intAdviserId);
                db.AddInParameter(cmdUpdateRepositoryCategoryNames, "@strCategoryNames", DbType.String, strCategoryNames);
                db.AddInParameter(cmdUpdateRepositoryCategoryNames, "@strCategoryRoles", DbType.String, strCategoryRoles);
                db.AddInParameter(cmdUpdateRepositoryCategoryNames, "@userId", DbType.Int32, intUserId);
                db.ExecuteNonQuery(cmdUpdateRepositoryCategoryNames);
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
                FunctionInfo.Add("Method", "RepositoryDao.cs:UpdateRepositoryCategoryNames(int intAdviserId, string strCategoryNames, int intUserId)");
                object[] objects = new object[3];
                objects[0] = intAdviserId;
                objects[1] = strCategoryNames;
                objects[3] = intUserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool DeleteRepositoryItems(string strXML, float fStorageBalance, int intAdviserId)
        {
            Database db;
            DbCommand cmdDeleteRepositoryItems;
            bool blResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeleteRepositoryItems = db.GetStoredProcCommand("sproc_Repository_DeleteRepositoryItems");
                db.AddInParameter(cmdDeleteRepositoryItems, "@deletedRepositoryXML", DbType.String, strXML);
                db.AddInParameter(cmdDeleteRepositoryItems, "@storageBalance", DbType.Decimal, fStorageBalance);
                db.AddInParameter(cmdDeleteRepositoryItems, "@adviserId", DbType.Int32, intAdviserId);
                db.ExecuteNonQuery(cmdDeleteRepositoryItems);
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
                FunctionInfo.Add("Method", "RepositoryDao.cs:DeleteRepositoryItems(string strXML, float fStorageBalance, int intAdviserId)");
                object[] objects = new object[3];
                objects[0] = strXML;
                objects[1] = fStorageBalance;
                objects[2] = intAdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public float GetAdviserStorageValues(int intAdviserId, out float fMaxStorage)
        {
            Database db;
            DbCommand cmdGetAdviserStorageValues;
            DataSet ds = new DataSet();
            float fResult = 0.0F;
            fMaxStorage = 0.0F;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAdviserStorageValues = db.GetStoredProcCommand("sproc_Repository_GetAdviserStorageValues");
                db.AddInParameter(cmdGetAdviserStorageValues, "@adviserId", DbType.Int32, intAdviserId);
                ds = db.ExecuteDataSet(cmdGetAdviserStorageValues);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["AS_StorageBalance"] != null)
                            fResult = float.Parse(ds.Tables[0].Rows[0]["AS_StorageBalance"].ToString());
                        if (ds.Tables[0].Rows[0]["AS_StorageSize"] != null)
                            fMaxStorage = float.Parse(ds.Tables[0].Rows[0]["AS_StorageSize"].ToString());
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
                FunctionInfo.Add("Method", "RepositoryDao.cs:GetAdviserStorageValues(int intAdviserId, out float fMaxStorage)");
                object[] objects = new object[1];
                objects[0] = intAdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return fResult;
        }

    }
}
