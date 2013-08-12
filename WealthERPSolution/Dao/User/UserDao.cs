﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using System.Data.SqlClient;
using BoCommon;
using System.Text;
using System.Numeric;




namespace DaoUser
{
    public class UserDao
    {
        public int CreateUser(UserVo userVo)
        {

            int userId = 0;
            Database db;
            DbCommand createUserCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createUserCmd = db.GetStoredProcCommand("SP_CreateUser");
                db.AddInParameter(createUserCmd, "@U_Password", DbType.String, userVo.Password);
                db.AddInParameter(createUserCmd, "@U_FirstName", DbType.String, userVo.FirstName.ToString());
                if (userVo.MiddleName != null)
                    db.AddInParameter(createUserCmd, "@U_MiddleName", DbType.String, userVo.MiddleName.ToString());
                else
                    db.AddInParameter(createUserCmd, "@U_MiddleName", DbType.String, DBNull.Value);
                if (userVo.LastName != null)
                    db.AddInParameter(createUserCmd, "@U_LastName", DbType.String, userVo.LastName.ToString());
                else
                    db.AddInParameter(createUserCmd, "@U_LastName", DbType.String, DBNull.Value);

                db.AddInParameter(createUserCmd, "@U_Email", DbType.String, userVo.Email.ToString());
                
                db.AddInParameter(createUserCmd, "@U_UserType", DbType.String, userVo.UserType.ToString());
                db.AddInParameter(createUserCmd, "@U_LoginId", DbType.String, userVo.LoginId);
                db.AddInParameter(createUserCmd, "@U_CreatedBy", DbType.String, 100);//temp
                db.AddInParameter(createUserCmd, "@U_ModifiedBy", DbType.String, 100);//temp
                db.AddOutParameter(createUserCmd, "@U_UserId", DbType.Int32, 10);
                if (db.ExecuteNonQuery(createUserCmd) != 0)
                    userId = int.Parse(db.GetParameterValue(createUserCmd, "U_UserId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserDao.cs:CreateUser()");


                object[] objects = new object[1];
                objects[0] = userVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return userId;
        }

        public UserVo GetUser(string username)
        {

            UserVo userVo = null;
            Database db;
            DbCommand getUserCmd;
            DataSet getUserDs;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUserCmd = db.GetStoredProcCommand("SP_GetUser");
                db.AddInParameter(getUserCmd, "@U_LoginId", DbType.String, username.ToString());
                getUserDs = db.ExecuteDataSet(getUserCmd);
                if (getUserDs.Tables[0].Rows.Count > 0)
                {
                    userVo = new UserVo();
                    dr = getUserDs.Tables[0].Rows[0];

                    userVo.UserId = int.Parse(dr["U_UserId"].ToString());
                    userVo.Password = dr["U_Password"].ToString();
                    userVo.MiddleName = dr["U_MiddleName"].ToString();
                    userVo.LastName = dr["U_LastName"].ToString();
                    userVo.FirstName = dr["U_FirstName"].ToString();
                    userVo.Email = dr["U_Email"].ToString();
                    userVo.UserType = dr["U_UserType"].ToString();
                    userVo.LoginId = dr["U_LoginId"].ToString();
                    if (dr["U_IsTempPassword"].ToString() != "")
                        userVo.IsTempPassword = int.Parse(dr["U_IsTempPassword"].ToString());
                    if (dr["U_Theme"].ToString() != "")
                        userVo.theme = dr["U_Theme"].ToString();
                    
                    if(!string.IsNullOrEmpty(dr["RoleList"].ToString()))
                        userVo.RoleList = dr["RoleList"].ToString().Split(new char[] { ',' });

                    if (!string.IsNullOrEmpty(dr["PermissionList"].ToString()))
                        userVo.PermisionList = dr["PermissionList"].ToString().Split(new char[] { ',' });
                    if (!string.IsNullOrEmpty(dr["U_PwdSaltValue"].ToString()))
                        userVo.PasswordSaltValue = dr["U_PwdSaltValue"].ToString().Trim();
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

                FunctionInfo.Add("Method", "UserDao.cs:GetUser()");


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
            Database db;
            DbCommand getUserCmd;
            DataSet getUserDs;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUserCmd = db.GetStoredProcCommand("SP_GetUserDetails");
                db.AddInParameter(getUserCmd, "@U_UserId", DbType.Int32, userId);
                getUserDs = db.ExecuteDataSet(getUserCmd);
                if (getUserDs.Tables[0].Rows.Count > 0)
                {
                    userVo = new UserVo();
                    dr = getUserDs.Tables[0].Rows[0];
                    userVo.UserId = int.Parse(dr["U_UserId"].ToString());
                    userVo.Password = dr["U_Password"].ToString();
                    userVo.MiddleName = dr["U_MiddleName"].ToString();
                    userVo.LastName = dr["U_LastName"].ToString();
                    userVo.FirstName = dr["U_FirstName"].ToString();
                    userVo.Email = dr["U_Email"].ToString();
                    userVo.UserType = dr["U_UserType"].ToString();
                    userVo.LoginId = dr["U_LoginId"].ToString();
                    if (!string.IsNullOrEmpty(dr["RoleList"].ToString()))
                        userVo.RoleList = dr["RoleList"].ToString().Split(new char[] { ',' });
                    if (!string.IsNullOrEmpty(dr["PermissionList"].ToString()))
                        userVo.PermisionList = dr["PermissionList"].ToString().Split(new char[] { ',' });

                    if (dr["U_Theme"].ToString() != "")
                        userVo.theme = dr["U_Theme"].ToString();
                    if (dr["U_PwdSaltValue"] != null)
                        userVo.PasswordSaltValue = dr["U_PwdSaltValue"].ToString();
                    if(dr["U_IsTempPassword"] != null)
                        userVo.IsTempPassword = Convert.ToInt16(dr["U_IsTempPassword"].ToString());
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

                FunctionInfo.Add("Method", "UserDao.cs:GetUserDetails()");


                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return userVo;

        }

        public bool ChkAvailability(string id)
        {

            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            string query = "";
            int rowCount;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                query = "select U_LoginId from [User] where U_LoginId='" + id + "'";
                chkAvailabilityCmd = db.GetSqlStringCommand(query);
                ds = db.ExecuteDataSet(chkAvailabilityCmd);
                rowCount = ds.Tables[0].Rows.Count;
                if (rowCount > 0)
                {
                    bResult = false;
                }
                else
                {
                    bResult = true;
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

                FunctionInfo.Add("Method", "UserDao.cs:ChkAvailability()");


                object[] objects = new object[1];
                objects[0] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }
        public bool ValidateUser(string username, string password)
        {

            bool bResult = false;
            Database db;
            SqlCommand validateUserCmd = new SqlCommand();
            string query = "";
            string actualPasswod = string.Empty;



            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                query = "SELECT U_Password FROM [User] WHERE U_LoginId= @LoginID";
                //validateUserCmd.Parameters.Add(, SqlDbType.VarChar, 150);
                validateUserCmd.Parameters.AddWithValue("@LoginID", username);
                validateUserCmd.CommandText = query;

                Object userPassword = db.ExecuteScalar(validateUserCmd);
                if (userPassword != null && userPassword != DBNull.Value)
                {
                    try
                    {
                        actualPasswod = Encryption.Decrypt(userPassword.ToString());
                    }
                    catch (Exception ex)
                    {

                    }
                    if (actualPasswod != string.Empty && actualPasswod.Trim() == password.Trim())
                        bResult = true;

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

                FunctionInfo.Add("Method", "UserDao.cs:ValidateUser()");


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

        public bool UpdateUser(UserVo userVo)
        {
            bool bResult = false;

            Database db;
            DbCommand updateUserCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateUserCmd = db.GetStoredProcCommand("SP_UpdateUser");
                db.AddInParameter(updateUserCmd, "@U_UserId", DbType.Int32, userVo.UserId);
                db.AddInParameter(updateUserCmd, "@U_FirstName", DbType.String, userVo.FirstName);
                db.AddInParameter(updateUserCmd, "@U_LastName", DbType.String, userVo.LastName);
                db.AddInParameter(updateUserCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                db.AddInParameter(updateUserCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(updateUserCmd, "@U_LoginId", DbType.String, userVo.LoginId);
                db.AddInParameter(updateUserCmd, "@U_Password", DbType.String, userVo.Password);
                if (!string.IsNullOrEmpty(userVo.PasswordSaltValue))
                db.AddInParameter(updateUserCmd, "@U_PasswordSaltValue", DbType.String, userVo.PasswordSaltValue);
                db.AddInParameter(updateUserCmd, "@U_IsTempPassword", DbType.String, userVo.IsTempPassword);
                db.ExecuteNonQuery(updateUserCmd);
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

                FunctionInfo.Add("Method", "UserDao.cs:updateUser()");


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
            Database db;
            DbCommand getAdvisorLogoCmd;
            DataSet getAdvisorLogoDs;
            DataRow dr;
            string logoPath = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorLogoCmd = db.GetStoredProcCommand("SP_GetAdviserLogo");
                db.AddInParameter(getAdvisorLogoCmd, "@A_AdviserId", DbType.Int32, advisorId);
                getAdvisorLogoDs = db.ExecuteDataSet(getAdvisorLogoCmd);
                if (getAdvisorLogoDs.Tables[0].Rows.Count > 0)
                {
                    dr = getAdvisorLogoDs.Tables[0].Rows[0];
                    logoPath = dr["A_AdviserLogo"].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetAdvisorLogo()");
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
            Database db;
            DbCommand getRMLogoCmd;
            DataSet getRMLogoDs;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMLogoCmd = db.GetStoredProcCommand("SP_GetRMLogo");
                db.AddInParameter(getRMLogoCmd, "@AR_RMID", DbType.Int32, rmId);
                getRMLogoDs = db.ExecuteDataSet(getRMLogoCmd);
                if (getRMLogoDs.Tables[0].Rows.Count > 0)
                {
                    dr = getRMLogoDs.Tables[0].Rows[0];
                    logoPath = dr["A_AdviserLogo"].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetRMLogo()");
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
            Database db;
            DbCommand getRMLogoCmd;
            DataSet getRMLogoDs;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMLogoCmd = db.GetStoredProcCommand("SP_GetRMBranchLogo");
                db.AddInParameter(getRMLogoCmd, "@AR_RMID", DbType.Int32, rmId);
                getRMLogoDs = db.ExecuteDataSet(getRMLogoCmd);
                if (getRMLogoDs.Tables[0].Rows.Count > 0)
                {
                    dr = getRMLogoDs.Tables[0].Rows[0];
                    branchlogoPath = dr["AB_BranchLogo"].ToString();
                }

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetRMBranchLogo()");
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
            Database db;
            DbCommand getCustomerLogoCmd;
            DataSet getCustomerLogoDs;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerLogoCmd = db.GetStoredProcCommand("SP_GetCustomerLogo");
                db.AddInParameter(getCustomerLogoCmd, "@C_CustomerId", DbType.Int32, customerId);
                getCustomerLogoDs = db.ExecuteDataSet(getCustomerLogoCmd);
                if (getCustomerLogoDs.Tables[0].Rows.Count > 0)
                {
                    dr = getCustomerLogoDs.Tables[0].Rows[0];
                    logoPath = dr["A_AdviserLogo"].ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetCustomerLogo()");
                object[] objects = new object[1];
                objects[0] = customerId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return logoPath;
        }

        public string GetUserRole(string advisor, string rm, string bm, string path)
        {
            string role;
            DataSet ds;
            DataRow dr;
            DataRow[] row;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                row = ds.Tables["Roles"].Select(" Advisor = '" + advisor + "' and RM = '" + rm + "' and BM = '" + bm + "'");
                dr = row[0];
                role = dr["Code"].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetCustomerLogo()");
                object[] objects = new object[4];
                objects[0] = advisor;
                objects[1] = rm;
                objects[2] = bm;
                objects[3] = path;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return role;

        }


        public bool CreateUserPermisionAssociation(int userId, int PermisionId)
        {
            bool bResult = false;
            Database db;
            DbCommand createRoleAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createRoleAssociationCmd = db.GetStoredProcCommand("SP_CreateUserPermisionAssociation");
                db.AddInParameter(createRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(createRoleAssociationCmd, "@UP_PermisionId", DbType.Int32, PermisionId);
                db.AddInParameter(createRoleAssociationCmd, "@URA_CreatedBy", DbType.Int32, 100);
                db.AddInParameter(createRoleAssociationCmd, "@URA_ModifiedBy", DbType.Int32, 100);
                db.ExecuteNonQuery(createRoleAssociationCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:CreateRoleAssociation()");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = PermisionId;
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
            Database db;
            DbCommand createRoleAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createRoleAssociationCmd = db.GetStoredProcCommand("SP_CreateRoleAssociation");
                db.AddInParameter(createRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(createRoleAssociationCmd, "@UR_RoleId", DbType.Int32, roleId);
                db.AddInParameter(createRoleAssociationCmd, "@URA_CreatedBy", DbType.Int32, 100);
                db.AddInParameter(createRoleAssociationCmd, "@URA_ModifiedBy", DbType.Int32, 100);
                db.ExecuteNonQuery(createRoleAssociationCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:CreateRoleAssociation()");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = roleId;
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
            string role;
            Database db;
            DbCommand getUserRolesCmd;
            DataSet getUserRolesDs;
            string query = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                query = "select UR_RoleName from UserRole where UR_RoleId in (select UR_RoleId from UserRoleAssociation where U_UserId=" + userId.ToString() + ")";
                getUserRolesCmd = db.GetSqlStringCommand(query);
                db.AddInParameter(getUserRolesCmd, "@U_UserId", DbType.Int32, userId);
                getUserRolesDs = db.ExecuteDataSet(getUserRolesCmd);
                if (getUserRolesDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getUserRolesDs.Tables[0].Rows)
                    {
                        role = dr["UR_RoleName"].ToString();
                        roleList.Add(role);
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
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetUserRoles()");
                object[] objects = new object[1];
                objects[0] = userId;
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
            Database db;
            DbCommand changePasswordCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                changePasswordCmd = db.GetStoredProcCommand("SP_ChangePassword");
                db.AddInParameter(changePasswordCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(changePasswordCmd, "@U_Password", DbType.String, password);
                if (!string.IsNullOrEmpty(pwdSaltValue))
                 db.AddInParameter(changePasswordCmd, "@U_PwdSaltValue", DbType.String, pwdSaltValue);
                db.AddInParameter(changePasswordCmd, "@U_IsTempPassword", DbType.Int16, isTempPass);
                
                db.ExecuteNonQuery(changePasswordCmd);
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

                FunctionInfo.Add("Method", "UserDao.cs:ChangePassword()");


                object[] objects = new object[4];
                objects[0] = userId;
                objects[1] = password;
                objects[2]=pwdSaltValue;
                objects[3] = isTempPass;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public static void AddSessionTrack(string loginId, string referralUrl, string sessionId, string url, string browser, string ipaddress)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_SessionTrackAdd");
                db.AddInParameter(updateCmd, "@IPAddress", DbType.String, ipaddress);
                db.AddInParameter(updateCmd, "@LoginId", DbType.String, loginId);
                if (referralUrl != null && referralUrl != string.Empty)
                    db.AddInParameter(updateCmd, "@ReferralUrl", DbType.String, referralUrl);
                db.AddInParameter(updateCmd, "@SessionID", DbType.String, sessionId);
                if (url != null && url != string.Empty)
                    db.AddInParameter(updateCmd, "@Url", DbType.String, url);
                if (browser != null && browser != string.Empty)
                    db.AddInParameter(updateCmd, "@Browser", DbType.String, browser);

                affectedRows = db.ExecuteNonQuery(updateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserDao.cs:AddSessionTrack()");

                //object[] objects = new object[1];
                //objects[0] = value;

                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        /// <summary>
        /// Insert an entry to LoginTrack table when a user tries to login.
        /// </summary>
        public static void AddLoginTrack(string loginId, string password, bool isSuccess, string ipAddress, string browser, int createdBy)
        {

            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_LoginTrackAdd");
                db.AddInParameter(updateCmd, "@Browser", DbType.String, browser);
                db.AddInParameter(updateCmd, "@CreatedBy", DbType.Int32, createdBy);
                db.AddInParameter(updateCmd, "@IPAddress", DbType.String, ipAddress);
                db.AddInParameter(updateCmd, "@IsSuccess", DbType.Boolean, isSuccess);
                db.AddInParameter(updateCmd, "@LoginId", DbType.String, loginId);
                db.AddInParameter(updateCmd, "@Password", DbType.String, password);

                affectedRows = db.ExecuteNonQuery(updateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UserDao.cs:AddLoginTrack()");

                object[] objects = new object[6];
                objects[0] = loginId;
                objects[1] = password;
                objects[2] = isSuccess;
                objects[3] = ipAddress;
                objects[4] = browser;
                objects[5] = createdBy;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                //throw exBase;
            }
        }

        public bool UpdateUserTheme(int userId, string theme)
        {
            bool bResult = false;
            Database db;
            DbCommand changePasswordCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                changePasswordCmd = db.GetStoredProcCommand("sproc_UpdateUserTheme");
                db.AddInParameter(changePasswordCmd, "@userId", DbType.Int32, userId);
                db.AddInParameter(changePasswordCmd, "@theme", DbType.String, theme);
                db.ExecuteNonQuery(changePasswordCmd);
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

                FunctionInfo.Add("Method", "UserDao.cs:UpdateUserTheme()");


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
            Database db;
            DbCommand createRoleAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createRoleAssociationCmd = db.GetStoredProcCommand("SP_DeleteRoleAssociation");
                db.AddInParameter(createRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
                db.ExecuteNonQuery(createRoleAssociationCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:DeleteRoleAssociation()");
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
        /// Function to get the role association  of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetRoleAssociation(int userId)
        {
            Database db;
            DbCommand getRoleAssociationCmd;
            DataSet getRoleAssociationDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRoleAssociationCmd = db.GetStoredProcCommand("SP_GetRoleAssociation");
                db.AddInParameter(getRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
                getRoleAssociationDs = db.ExecuteDataSet(getRoleAssociationCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetRoleAssociation()");
                object[] objects = new object[1];
                objects[0] = userId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getRoleAssociationDs.Tables[0];
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
            Database db;
            DbCommand createRoleAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createRoleAssociationCmd = db.GetStoredProcCommand("SP_DeleteStaffRoleAssociation");
                db.AddInParameter(createRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(createRoleAssociationCmd, "@UR_RoleId", DbType.Int32, roleId);
                db.AddInParameter(createRoleAssociationCmd, "@URA_CreatedBy", DbType.Int32, 100);
                db.AddInParameter(createRoleAssociationCmd, "@URA_ModifiedBy", DbType.Int32, 100);
                db.ExecuteNonQuery(createRoleAssociationCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:CreateRoleAssociation()");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = roleId;
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
            Database db;
            DbCommand getUserThemeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUserThemeCmd = db.GetStoredProcCommand("SP_GetUserTheme");
                db.AddInParameter(getUserThemeCmd, "@UserTypeId", DbType.Int32, userTypeId);
                db.AddInParameter(getUserThemeCmd, "@UserType", DbType.String, userType);
                db.AddOutParameter(getUserThemeCmd, "@ThemeName", DbType.String, 20);
                db.ExecuteNonQuery(getUserThemeCmd);
                Object objThemeName = db.GetParameterValue(getUserThemeCmd, "@ThemeName");
                if (objThemeName != DBNull.Value)
                    theme = db.GetParameterValue(getUserThemeCmd, "@ThemeName").ToString().Trim();
                else
                    theme = string.Empty;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:GetUserTheme()");
                object[] objects = new object[2];
                objects[0] = userTypeId;
                objects[1] = userType;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public bool CheckIPAvailabilityInIPPool(int adviserID,string userCurrentIPAddress)
        {
            bool bResult;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SP_CheckIPAvailabilityInIPPool");
                db.AddInParameter(chkAvailabilityCmd, "@A_AdviserID", DbType.Int32, adviserID);
                db.AddInParameter(chkAvailabilityCmd, "@UserCurrentIP", DbType.String, userCurrentIPAddress);
                ds = db.ExecuteDataSet(chkAvailabilityCmd);
                rowCount = ds.Tables[0].Rows.Count;
                if (rowCount > 0)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
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

                FunctionInfo.Add("Method", "UserDao.cs:CheckIPAvailabilityInIPPool(int adviserID,string userCurrentIPAddress)");


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
            bool bResult = false;
            Database db;
            DbCommand chkAvailabilityCmd;
            int rowCount;
            string userDBPassWord = string.Empty;
            string actualPassWord = string.Empty;
            string strPwdSaltValue=string.Empty;
            Hashtable hashUserAuthenticationDetails = new Hashtable();
            DataSet ds;
            OneWayEncryption oneWayEncryption=new OneWayEncryption();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAvailabilityCmd = db.GetStoredProcCommand("SP_ValidateUserLoginDetails");
                db.AddInParameter(chkAvailabilityCmd, "@A_AdviserID", DbType.Int32, adviserID);
                db.AddInParameter(chkAvailabilityCmd, "@User_LoginId", DbType.String, userLoginId);
                db.AddInParameter(chkAvailabilityCmd, "@User_IPAddress", DbType.String, userCurrentIPAddress);
                
                if(isIPEnable == false)
                    db.AddInParameter(chkAvailabilityCmd, "@IPEnableFlag", DbType.Int32, 0);
                else
                    db.AddInParameter(chkAvailabilityCmd, "@IPEnableFlag", DbType.Int32, 1);

                ds = db.ExecuteDataSet(chkAvailabilityCmd);
                
                rowCount = ds.Tables[0].Rows.Count;

                if (ds.Tables[0].Rows.Count != 0)
                {
                    userDBPassWord = ds.Tables[0].Rows[0]["U_Password"].ToString().Trim();
                    if (ds.Tables[0].Rows[0]["U_PwdSaltValue"]!=null)
                     strPwdSaltValue = ds.Tables[0].Rows[0]["U_PwdSaltValue"].ToString().Trim();

                    if (!string.IsNullOrEmpty(userDBPassWord))
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(strPwdSaltValue))
                            {
                                if (oneWayEncryption.VerifyHashString(userPassword, userDBPassWord, strPwdSaltValue))
                                    actualPassWord = userPassword;
                                else
                                    actualPassWord = GetPassword();
                            }
                            else
                            {
                                actualPassWord = Encryption.Decrypt(userDBPassWord.ToString());
                            }
                            
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                if (isIPEnable == true)
                {
                    if (actualPassWord != string.Empty && actualPassWord.Trim() == userPassword.Trim())
                    {
                        hashUserAuthenticationDetails.Add("PWD", true);
                    }
                    else
                    {
                        hashUserAuthenticationDetails.Add("PWD", false);
                    }
                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        hashUserAuthenticationDetails.Add("IPAuthentication", true);
                    }
                    else
                    {
                        hashUserAuthenticationDetails.Add("IPAuthentication", false);
                    }
                }
                else
                {
                    if (actualPassWord != string.Empty && actualPassWord.Trim() == userPassword.Trim())
                    {
                        hashUserAuthenticationDetails.Add("PWD", true);
                    }
                    else
                    {
                        hashUserAuthenticationDetails.Add("PWD", false);
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

                FunctionInfo.Add("Method", "UserDao.cs:UserValidationForIPnonIPUsers(int adviserID, string userLoginId, string userCurrentIPAddress, bool isIPEnable)");

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

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch ;
            for(int i=0; i<size; i++)
            {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))) ;
            builder.Append(ch);
            }
            if(lowerCase)
            return builder.ToString().ToLower();
            return builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public bool CheckCheckerMaker(int userId, int PermisionId)
        {
            bool bResult = false;
            Database db;
            DbCommand CheckRoleAssociationCmd;
            DataSet Role;
            int rowcount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckRoleAssociationCmd = db.GetStoredProcCommand("SP_CheckCheckerMaker");
                db.AddInParameter(CheckRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(CheckRoleAssociationCmd, "@UP_PermisionId", DbType.Int32, PermisionId);
                 Role = db.ExecuteDataSet(CheckRoleAssociationCmd);
                 rowcount = Role.Tables[0].Rows.Count;
                if (rowcount>0)
                     bResult = true;
                else
                    bResult = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:CreateRoleAssociation()");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = PermisionId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;

        }
        public DataTable CheckChMk(int userId)
        {
            DataTable bResult;
            Database db;
            DbCommand CheckRoleAssociationCmd;
            DataSet Role;
            int rowcount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckRoleAssociationCmd = db.GetStoredProcCommand("SP_CheckChMk");
                db.AddInParameter(CheckRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);

                Role = db.ExecuteDataSet(CheckRoleAssociationCmd);
                bResult = Role.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                //functionInfo.Add("Method", "UserDao.cs:CreateRoleAssociation()");
                //object[] objects = new object[2];
                //objects[0] = userId;
                //objects[1] = PermisionId;
                //functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;

        }
        public bool DeleteCKMK(int userId, int PermisionId)
        {
            bool bResult = false;
            Database db;
            DbCommand CheckRoleAssociationCmd;
            DataSet Role;
            int rowcount;
            try{

            db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckRoleAssociationCmd = db.GetStoredProcCommand("SP_DeleteCHMK");
                db.AddInParameter(CheckRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(CheckRoleAssociationCmd, "@UP_PermisionId", DbType.Int32, PermisionId);
                 Role = db.ExecuteDataSet(CheckRoleAssociationCmd);
                
                 bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                functionInfo.Add("Method", "UserDao.cs:CreateRoleAssociation()");
                object[] objects = new object[2];
                objects[0] = userId;
                objects[1] = PermisionId;
                functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }
        public bool DeletefromPermision(int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand CheckRoleAssociationCmd;
            DataSet Role;
            int rowcount;
            try{

            db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckRoleAssociationCmd = db.GetStoredProcCommand("SP_DeleteFromPermission");
                db.AddInParameter(CheckRoleAssociationCmd, "@U_UserId", DbType.Int32, userId);
               
                 Role = db.ExecuteDataSet(CheckRoleAssociationCmd);
                
                 bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection functionInfo = new NameValueCollection();
                //functionInfo.Add("Method", "UserDao.cs:CreateRoleAssociation()");
                //object[] objects = new object[2];
                //objects[0] = userId;
                //objects[1] = PermisionId;
                //functionInfo = exBase.AddObject(functionInfo, objects);
                exBase.AdditionalInformation = functionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }

        public DataSet GetLoginTrack(string userType, int AdviserId, DateTime dtFrom, DateTime dtTo)
        {

            UserVo userVo = null;
            Database db;
            DbCommand getLoginCmd;
            DataSet getLoginDs;
           try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoginCmd = db.GetStoredProcCommand("SP_GetLoginTrack");
                db.AddInParameter(getLoginCmd, "@U_UserType", DbType.String, userType);
                db.AddInParameter(getLoginCmd, "@A_AdviserId", DbType.Int32, AdviserId);
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(getLoginCmd, "@dtFrom", DbType.DateTime, dtFrom);
                else
                    dtFrom = DateTime.MinValue;
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(getLoginCmd, "@dtTo", DbType.DateTime, dtTo);
                else
                    dtTo = DateTime.MinValue;

                getLoginDs = db.ExecuteDataSet(getLoginCmd);
               // if (getLoginDs.Tables[0].Rows.Count > 0)
               // {
               //     foreach (DataRow dr in getLoginDs.Tables[0].Rows)
               //     {
               //         userVo = new UserVo();
               //         userVo.LoginId = dr["LoginId"].ToString();
               //         userVo.FirstName = dr["U_FirstName"].ToString();
               //         userVo.LTDateTime = DateTime.Parse(dr["LT_CreatedOn"].ToString());

               //     }
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

                FunctionInfo.Add("Method", "UserDao.cs:GetUser()");


                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getLoginDs;

        }


    }
}
