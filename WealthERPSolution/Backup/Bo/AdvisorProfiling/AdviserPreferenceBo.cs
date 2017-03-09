using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VoAdvisorProfiling;
using DaoAdvisorProfiling;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections;

namespace BoAdvisorProfiling
{
    public class AdviserPreferenceBo
    {
        public AdvisorPreferenceVo GetAdviserPreference(int adviserId)
        {
            AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
            try
            {
                AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                advisorPreferenceVo = advisorPreferenceDao.GetAdviserPreference(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserPreferenceBo.cs:GetAdviserPreference()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return advisorPreferenceVo;

        }

        public bool AdviserPreferenceSetUp(AdvisorPreferenceVo advisorPreferenceVo, int adviserId, int UserId, string strCommand)
        {
            bool isSuccess = false;
            try
            {
                AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                isSuccess = advisorPreferenceDao.AdviserPreferenceSetUp(advisorPreferenceVo, adviserId, UserId, strCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserPreferenceBo.cs:AdviserPreferenceSetUp()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return isSuccess;

        }

        public DataSet GetTreeNodes(int userlevelId, int advisorId)
        {
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            DataSet dsGetTreeNodes = new DataSet();

            try
            {
                dsGetTreeNodes = advisorPreferenceDao.GetTreeNodes(userlevelId, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetTreeNodes()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetTreeNodes;


        }
        public DataSet GetSubTreeNodes(int parentNode, int userlevelId, int advisorId)
        {
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            DataSet dsGetSubTreeNodes = new DataSet();
            try
            {
                dsGetSubTreeNodes = advisorPreferenceDao.GetSubTreeNodes(parentNode, userlevelId, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorPreferenceBo.cs:GetSubTreeNodes()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubTreeNodes;

        }

        public DataSet GetSubSubTreeNodes(int subNode, int userlevelId, int advisorId)
        {
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            DataSet dsGetSubSubTreeNodes = new DataSet();

            try
            {
                dsGetSubSubTreeNodes = advisorPreferenceDao.GetSubSubTreeNodes(subNode, userlevelId, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorPreferenceBo.cs:GetSubSubTreeNodes()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetSubSubTreeNodes;

        }
        public DataSet GetActualRoles(int roleId, int advisorId)
        {
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            DataSet dsGetActualRoles = new DataSet();

            try
            {
                dsGetActualRoles = advisorPreferenceDao.GetActualRoles(roleId, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorPreferenceBo.cs:GetActualRoles()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetActualRoles;

        }
          public DataSet GetAdviserRoles(int advisorId)
          {
              AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
              DataSet dsGetAdviserRoles = new DataSet();

              try
              {
                  dsGetAdviserRoles = advisorPreferenceDao.GetAdviserRoles(advisorId);
              }
              catch (BaseApplicationException Ex)
              {
                  throw Ex;
              }
              catch (Exception Ex)
              {
                  BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                  NameValueCollection FunctionInfo = new NameValueCollection();
                  FunctionInfo.Add("Method", "AdvisorPreferenceBo.cs:GetAdviserRoles()");
                  object[] objects = new object[0];
                  FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                  exBase.AdditionalInformation = FunctionInfo;
                  ExceptionManager.Publish(exBase);
                  throw exBase;
              }
              return dsGetAdviserRoles;
          }

          public DataSet GetRoleLevelTreeNodes(int roleId, int levelId)
          {
              AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
              DataSet dsGetAdviserRoles = new DataSet();

              try
              {
                  dsGetAdviserRoles = advisorPreferenceDao.GetRoleLevelTreeNodes(roleId, levelId);
              }
              catch (BaseApplicationException Ex)
              {
                  throw Ex;
              }
              catch (Exception Ex)
              {
                  BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                  NameValueCollection FunctionInfo = new NameValueCollection();
                  FunctionInfo.Add("Method", "AdvisorPreferenceBo.cs:GetRoleLevelTreeNodes()");
                  object[] objects = new object[0];
                  FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                  exBase.AdditionalInformation = FunctionInfo;
                  ExceptionManager.Publish(exBase);
                  throw exBase;
              }
              return dsGetAdviserRoles;
          }
          public int CreateOrUpdateTreeNodeMapping(DataTable dtTreeNodes, string commandType, int userId,int levelId)
          {
              try
              {
                  AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                  return advisorPreferenceDao.CreateOrUpdateTreeNodeMapping(dtTreeNodes, commandType, userId, levelId);
              }
              catch (BaseApplicationException Ex)
              {
                  throw Ex;
              }
          }
          public int CreateOrUpdateTreeSubNodeMapping(DataTable dtSubTreeNodes, string commandType, int userId, int levelId)
          {
              try
              {
                  AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                  return advisorPreferenceDao.CreateOrUpdateTreeSubNodeMapping(dtSubTreeNodes, commandType, userId, levelId);
              }
              catch (BaseApplicationException Ex)
              {
                  throw Ex;
              }
          }
          public int CreateOrUpdateTreeSubSubNodeMapping(DataTable dtSubSubTreeNodes, string commandType, int userId, int levelId)
          {
              try
              {
                  AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                  return advisorPreferenceDao.CreateOrUpdateTreeSubSubNodeMapping(dtSubSubTreeNodes, commandType, userId, levelId);
              }
              catch (BaseApplicationException Ex)
              {
                  throw Ex;
              }
          }
          public int RemoveTreeNodeMapping(DataTable dtSubSubTreeNodes, DataTable dtSubTreeNodes, DataTable dtTreeNodes)
          {
              try
              {
                  AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
                  return advisorPreferenceDao.RemoveTreeNodeMapping(dtSubSubTreeNodes, dtSubTreeNodes, dtTreeNodes);
              }
              catch (BaseApplicationException Ex)
              {
                  throw Ex;
              }
          }

           public DataSet GetUserRole(int adviserid)
        {
            DataSet dsGetUserRole;
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            dsGetUserRole = advisorPreferenceDao.GetUserRole(adviserid);
            return dsGetUserRole;
        }
        public DataSet GetDepartment(int AdviserId)
        {

            DataSet dsGetUserRole;
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            dsGetUserRole = advisorPreferenceDao.GetDepartment(AdviserId);
            return dsGetUserRole;
        }
        public bool CreateUserRole(int UserRole, string RoleName, string Purpose, int AdviserId, int UserId, string StrUserLeve)
        {
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            bool bResult = false;
            try
            {
                bResult = advisorPreferenceDao.CreateUserRole(UserRole, RoleName, Purpose, AdviserId, UserId, StrUserLeve);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool UpdateUserrole(int rollid, int userrole, string rolename, string purpose, int userid, string StrUserLeve)
        {
            bool blResult = false;
           AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            try
            {
                blResult = advisorPreferenceDao.UpdateUserrole(rollid, userrole, rolename, purpose, userid,StrUserLeve);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "advisorPreferenceDao.cs:UpdateUserrole()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        public bool DeleteUserRole(int rollid)
        {
            bool blResult = false;
         AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            try
            {
                //return advisorPreferenceDao.deleteTradeBusinessDate(tradeBusinessDateVo);
                blResult = advisorPreferenceDao.DeleteUserRole(rollid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;
        }
        //public DataSet GetUserRoleDepartmentWise(int departmentid)
        //{
        //    DataSet dsGetUserRoleDepartmentWise;
        //    AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
        //    dsGetUserRoleDepartmentWise = advisorPreferenceDao.GetUserRoleDepartmentWise(departmentid);
        //    return dsGetUserRoleDepartmentWise;
        //}
        public DataTable GetUserRoleDepartmentWise(int departmentid)
        {
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
            DataTable dtGetUserRoleDepartmentWise;

            try
            {
                dtGetUserRoleDepartmentWise = advisorPreferenceDao.GetUserRoleDepartmentWise(departmentid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetUserRoleDepartmentWise(int departmentid)");
                object[] objects = new object[1];
                objects[0] = departmentid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetUserRoleDepartmentWise;
        }
        public DataSet GetAdviserRoledepartmentwise(int roleid)
        {
            AdvisorPreferenceDao advisorPreferenceDao = new AdvisorPreferenceDao();
           
            try
            {
                return advisorPreferenceDao.GetAdviserRoledepartmentwise(roleid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAdviserRoledepartmentwise()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }


    
}
