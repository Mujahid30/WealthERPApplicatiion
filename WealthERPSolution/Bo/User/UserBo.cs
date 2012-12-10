﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoUser;
using VoUser;
using System.Data;
using System.Collections;

namespace BoUser
{
    public class UserBo
    {
        public int CreateUser(UserVo userVo)
        {
            int userId;
            UserDao userDao = new UserDao();
            try
            {
                userId = userDao.CreateUser(userVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:CreateUser()");


                object[] objects = new object[1];
                objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return userId;
        }
        public bool ChkAvailability(string id)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.ChkAvailability(id);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:chkAvailability()");


                object[] objects = new object[1];
                objects[0] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }
        public UserVo GetUser(string username)
        {
            UserVo userVo = null;
            UserDao userDao = new UserDao();

            try
            {
                userVo = userDao.GetUser(username);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:GetUser()");


                object[] objects = new object[1];
                objects[0] = username;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return userVo;
        }


        public UserVo GetUserDetails(int userId)
        {
            UserVo userVo = null;
            UserDao userDao = new UserDao();

            try
            {
                userVo = userDao.GetUserDetails(userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:GetUserDetails()");


                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return userVo;
        }


        public bool ValidateUser(string username, string password)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {

                bResult=userDao.ValidateUser(username, password);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:ValidateUser()");


                object[] objects = new object[2];
                objects[0] = username;
                objects[1] = password;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

      //  public bool updateUser(AdvisorVo advisorVo)
        public bool UpdateUser(UserVo userVo)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                userDao.UpdateUser(userVo);
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

                FunctionInfo.Add("Method", "UserBo.cs:updateUser()");


                object[] objects = new object[1];
                objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }


        public string GetAdvisorLogo(int advisorId)
        {
            string logoPath = "";
            UserDao userDao = new UserDao();

            try
            {
                logoPath = userDao.GetAdvisorLogo(advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:GetAdvisorLogo()");
                object[] objects = new object[1];
                objects[0] = advisorId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return logoPath;
        }

        public string GetRMLogo(int rmId)
        {
            string logoPath = "";
            UserDao userDao = new UserDao();

            try
            {
                logoPath = userDao.GetRMLogo(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:GetRMLogo()");
                object[] objects = new object[1];
                objects[0] = rmId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return logoPath;
        }

        public string GetRMBranchLogo(int rmId)
        {
            string branchlogoPath = "";
            UserDao userDao = new UserDao();

            try
            {
                branchlogoPath = userDao.GetRMBranchLogo(rmId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:GetRMBranchLogo()");
                object[] objects = new object[1];
                objects[0] = rmId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return branchlogoPath;
        }

        public string GetCustomerLogo(int customerId)
        {
            string logoPath = "";
            UserDao userDao = new UserDao();

            try
            {
                logoPath = userDao.GetCustomerLogo(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:GetCustomerLogo()");
                object[] objects = new object[1];
                objects[0] = customerId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return logoPath;
        }

        public bool CreateUserPermisionAssociation(int userId, int PermisionId)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.CreateUserPermisionAssociation(userId, PermisionId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:CreateRoleAssociation()");
                object[] objects = new object[4];
                objects[0] = userId;
                objects[1] = PermisionId;
                objects[2] = bResult;
                objects[3] = userDao;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        public bool CreateRoleAssociation(int userId, int roleId)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.CreateRoleAssociation(userId, roleId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:CreateRoleAssociation()");
                object[] objects = new object[4];
                objects[0] = userId;
                objects[1] = roleId;
                objects[2] = bResult;
                objects[3] = userDao;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        public List<string> GetUserRoles(int userId)
        {
            List<string> roleList = new List<string>();
            UserDao userDao = new UserDao();
            try
            {
                roleList = userDao.GetUserRoles(userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:GetUserRoles()");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = roleList;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return roleList;
        }

        public bool ChangePassword(int userId, string password,string pwdSaltValue, int isTempPass)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                userDao.ChangePassword(userId, password, pwdSaltValue, isTempPass);
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

                FunctionInfo.Add("Method", "UserBo.cs:ChangePassword()");


                object[] objects = new object[3];
                objects[0] = userId;
                objects[1] = password;
                objects[2] = isTempPass;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public static void AddSessionTrack(string loginId,string referralUrl,string sessionId,string url,string browser, string ipaddress)
        {
            UserDao.AddSessionTrack(loginId, referralUrl, sessionId, url, browser, ipaddress);
        }

        /// <summary>
        /// Insert an entry to LoginTrack table when a user tries to login.
        /// </summary>
        public static void AddLoginTrack(string loginId, string password, bool isSuccess, string ipAddress, string browser, int createdBy)
        {
            UserDao.AddLoginTrack(loginId, password, isSuccess, ipAddress, browser, createdBy);
        }

        public bool UpdateUserTheme(int userId, string theme)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.UpdateUserTheme(userId, theme);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:ChangePassword()");


                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = theme;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Function to delete the role association of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteRoleAssociation(int userId)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.DeleteRoleAssociation(userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:DeleteRoleAssociation()");
                object[] objects = new object[1];
                objects[0] = userId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Function to get the role Association of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetRoleAssociation(int userId)
        {
            UserDao userDao = new UserDao();
            DataTable getRoleAssociationDt;
            try
            {
                getRoleAssociationDt = userDao.GetRoleAssociation(userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:GetRoleAssociation()");
                object[] objects = new object[1];
                objects[0] = userId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getRoleAssociationDt;
        }
        /// <summary>
        /// Function to delete the role association of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteRoleAssociation(int userId, int roleId)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.DeleteRoleAssociation(userId, roleId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:CreateRoleAssociation()");
                object[] objects = new object[4];
                objects[0] = userId;
                objects[1] = roleId;
                objects[2] = bResult;
                objects[3] = userDao;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        /// <summary>
        /// Get RM,BM and Customer Theme Based On Role Type And Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public void GetUserTheme(int userTypeId, string userType, out string theme)
        {
            UserDao userDao = new UserDao();
            
            try
            {
                userDao.GetUserTheme(userTypeId, userType, out theme);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:GetUserTheme()");
                object[] objects = new object[2];
                objects[0] = userTypeId;
                objects[1] = userType;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        public bool CheckIPAvailabilityInIPPool(int adviserID, string userCurrentIPAddress)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.CheckIPAvailabilityInIPPool(adviserID, userCurrentIPAddress);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:CheckIPAvailabilityInIPPool(int adviserID, string userCurrentIPAddress)");


                object[] objects = new object[3];
                objects[0] = adviserID;
                objects[1] = userCurrentIPAddress;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }
        
        /// <summary>
        /// Login details validation for a user..
        /// Added by Vinayak Patil
        /// </summary>
        /// <param name="adviserID"></param>
        /// <param name="userLoginId"></param>
        /// <param name="userCurrentIPAddress"></param>
        /// <param name="isIPEnable"></param>
        /// <returns></returns>

        public Hashtable UserValidationForIPnonIPUsers(int adviserID, string userLoginId, string userPassword, string userCurrentIPAddress, bool isIPEnable)
        {
            Hashtable hashUserAuthenticationDetails = new Hashtable();
            UserDao userDao = new UserDao();
            try
            {
                hashUserAuthenticationDetails = userDao.UserValidationForIPnonIPUsers(adviserID, userLoginId, userPassword, userCurrentIPAddress, isIPEnable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserBo.cs:UserValidationForIPnonIPUsers(int adviserID, string userLoginId, string userCurrentIPAddress, bool isIPEnable)");


                object[] objects = new object[6];
                objects[0] = adviserID;
                objects[1] = userLoginId;
                objects[2] = userCurrentIPAddress;
                objects[1] = isIPEnable;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return hashUserAuthenticationDetails;
        }
        public bool CheckCheckerMaker(int userId, int PermisionId)
        {
            bool bResult = false;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.CheckCheckerMaker(userId, PermisionId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserBo.cs:CreateRoleAssociation()");
                object[] objects = new object[4];
                objects[0] = userId;
                objects[1] = PermisionId;
                objects[2] = bResult;
                objects[3] = userDao;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        public DataTable CheckChMk(int userId)
        {
            DataTable bResult ;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.CheckChMk(userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                //functionInfo.Add("Method", "UserBo.cs:CreateRoleAssociation()");
                //object[] objects = new object[4];
                //objects[0] = userId;
                //objects[1] = PermisionId;
                //objects[2] = bResult;
                //objects[3] = userDao;
                //functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool DeleteCKMK(int userId, int permisionid)
        {
            bool bResult ;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.DeleteCKMK(userId, permisionid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                //functionInfo.Add("Method", "UserBo.cs:CreateRoleAssociation()");
                //object[] objects = new object[4];
                //objects[0] = userId;
                //objects[1] = PermisionId;
                //objects[2] = bResult;
                //objects[3] = userDao;
                //functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        public bool DeletefromPermision(int userId)
        {
            bool bResult ;
            UserDao userDao = new UserDao();
            try
            {
                bResult = userDao.DeletefromPermision(userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                //functionInfo.Add("Method", "UserBo.cs:CreateRoleAssociation()");
                //object[] objects = new object[4];
                //objects[0] = userId;
                //objects[1] = PermisionId;
                //objects[2] = bResult;
                //objects[3] = userDao;
                //functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
    }
}
