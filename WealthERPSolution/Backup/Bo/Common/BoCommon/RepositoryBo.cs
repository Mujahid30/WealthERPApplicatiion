﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCommon;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCommon;

namespace BoCommon
{
    public class RepositoryBo
    {
        public DataSet GetRepositoryView(int intUserId, string strRoleList)
        {
            RepositoryDao repoDao = new RepositoryDao();
            DataSet ds = new DataSet();

            try
            {
                ds = repoDao.GetRepositoryView(intUserId, strRoleList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:GetRepositoryView(int intUserId, string strRoleList)");
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
            RepositoryDao repoDao = new RepositoryDao();
            DataSet ds = new DataSet();

            try
            {
                ds = repoDao.GetRepositoryCategory(intAdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:GetRepositoryCategory(int intAdviserId)");
                object[] objects = new object[1];
                objects[0] = intAdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public int GetNcdProspectUsCat(int advisorId, int RoleId, int UserId)
        {
            RepositoryDao repoDao = new RepositoryDao();
          
             
            try
            {
                return repoDao.GetNcdProspectUsCat(advisorId, RoleId, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool AddOrderDocument(RepositoryVo repoVo, int issueId)
        {
            RepositoryDao repoDao = new RepositoryDao();
            bool blResult = false;

            try
            {
                blResult = repoDao.AddOrderDocument(repoVo, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;

        }
        public bool AddRepositoryItem(RepositoryVo repoVo,int issueId)
        {
            RepositoryDao repoDao = new RepositoryDao();
            bool blResult = false;

            try
            {
                blResult = repoDao.AddRepositoryItem(repoVo, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:AddRepositoryItem(repoVo)");
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
            RepositoryDao repoDao = new RepositoryDao();
            DataSet ds = new DataSet();

            try
            {
                ds = repoDao.GetAdviserRepositoryView(intAdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:GetRepositoryView(int intUserId, string strRoleList)");
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
            RepositoryDao repoDao = new RepositoryDao();
            RepositoryVo repoVo = new RepositoryVo();

            try
            {
                repoVo = repoDao.GetRepositoryItem(intRepositoryId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:GetRepositoryItem(int intRepositoryId)");
                object[] objects = new object[2];
                objects[0] = intRepositoryId;
                objects[1] = repoDao;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return repoVo;
        }

        public bool UpdateRepositoryItem(RepositoryVo repoVo)
        {
            RepositoryDao repoDao = new RepositoryDao();
            bool blResult = false;

            try
            {
                blResult = repoDao.UpdateRepositoryItem(repoVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:UpdateRepositoryItem(RepositoryVo repoVo)");
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
            RepositoryDao repoDao = new RepositoryDao();
            bool blResult = false;

            try
            {
                blResult = repoDao.UpdateRepositoryCategoryNames(intAdviserId, strCategoryNames, strCategoryRoles, intUserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:UpdateRepositoryCategoryNames(int intAdviserId, string strCategoryNames, int intUserId)");
                object[] objects = new object[3];
                objects[0] = intAdviserId;
                objects[1] = strCategoryNames;
                objects[2] = intUserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool DeleteRepositoryItems(string strXML, float fStorageBalance, int intAdviserId)
        {
            RepositoryDao repoDao = new RepositoryDao();
            bool blResult = false;

            try
            {
                blResult = repoDao.DeleteRepositoryItems(strXML, fStorageBalance, intAdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:DeleteRepositoryItems(string strXML, float fStorageBalance, int intAdviserId)");
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
            RepositoryDao repoDao = new RepositoryDao();
            float fResult = 0.0F;
            fMaxStorage = 0.0F;

            try
            {
                fResult = repoDao.GetAdviserStorageValues(intAdviserId, out fMaxStorage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:GetAdviserStorageValues(int intAdviserId, out float fMaxStorage)");
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
