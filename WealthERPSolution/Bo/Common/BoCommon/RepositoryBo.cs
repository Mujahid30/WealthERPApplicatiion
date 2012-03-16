using System;
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

        public DataSet GetRepositoryCategory()
        {
            RepositoryDao repoDao = new RepositoryDao();
            DataSet ds = new DataSet();

            try
            {
                ds = repoDao.GetRepositoryCategory();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RepositoryBo.cs:GetRepositoryCategory()");
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
            RepositoryDao repoDao = new RepositoryDao();
            bool blResult = false;

            try
            {
                blResult = repoDao.AddRepositoryItem(repoVo);
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
    }
}
