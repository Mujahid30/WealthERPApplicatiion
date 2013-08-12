﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using VoAdvisorProfiling;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace DaoAdvisorProfiling
{
    public class AdvisorStaffDao
    {
        public int CreateAdvisorStaff(RMVo rmVo, int advisorId, int userId)
        {
            int rmId = 0;
            Database db;
            DbCommand createAdvisorStaffCmd;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdvisorStaffCmd = db.GetStoredProcCommand("SP_CreateAdviserStaff");

                db.AddInParameter(createAdvisorStaffCmd, "@A_AdviserId", DbType.Int32, advisorId);
                db.AddInParameter(createAdvisorStaffCmd, "@U_UserId", DbType.Int32, rmVo.UserId);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_FirstName", DbType.String, rmVo.FirstName);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_MiddleName", DbType.String, rmVo.MiddleName);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_LastName", DbType.String, rmVo.LastName);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_OfficePhoneDirectISD", DbType.Int32, rmVo.OfficePhoneDirectIsd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_OfficePhoneDirectSTD", DbType.Int32, rmVo.OfficePhoneDirectStd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_OfficePhoneDirect", DbType.Int32, rmVo.OfficePhoneDirectNumber);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_OfficePhoneExtISD", DbType.Int32, rmVo.OfficePhoneExtIsd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_OfficePhoneExtSTD", DbType.Int32, rmVo.OfficePhoneExtStd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_OfficePhoneExt", DbType.Int32, rmVo.OfficePhoneExtNumber);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_ResPhoneISD", DbType.Int32, rmVo.ResPhoneIsd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_ResPhoneSTD", DbType.Int32, rmVo.ResPhoneStd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_ResPhone", DbType.Int32, rmVo.ResPhoneNumber);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_Mobile", DbType.Int64, rmVo.Mobile);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_Fax", DbType.Int32, rmVo.Fax);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_FaxISD", DbType.Int32, rmVo.FaxIsd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_FaxSTD", DbType.Int32, rmVo.FaxStd);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_Email", DbType.String, rmVo.Email);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_JobFunction", DbType.String, rmVo.RMRole);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createAdvisorStaffCmd, "@AR_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createAdvisorStaffCmd, "@AR_RMId", DbType.Int32, 5000);
                if (db.ExecuteNonQuery(createAdvisorStaffCmd) != 0)

                    rmId = int.Parse(db.GetParameterValue(createAdvisorStaffCmd, "AR_RMId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CreateAdvisorStaff()");


                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = advisorId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmId;
        }

        public bool DeleteRM(int rmId, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteRMCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteRMCmd = db.GetStoredProcCommand("SP_DeleteRM");
                db.AddInParameter(deleteRMCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(deleteRMCmd, "@U_UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(deleteRMCmd) != 0)
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
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:DeleteRM()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool CreateRMUser(RMVo rmVo, int userId, string password)
        {

            bool bResult = false;
            Database db;
            DbCommand createRMUserCmd;
            string type = "RM";

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createRMUserCmd = db.GetStoredProcCommand("SP_CreateRMUser");
                db.AddInParameter(createRMUserCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(createRMUserCmd, "@U_Password", DbType.String, password.ToString());
                db.AddInParameter(createRMUserCmd, "@U_FirstName", DbType.String, rmVo.FirstName);
                db.AddInParameter(createRMUserCmd, "@U_MiddleName", DbType.String, rmVo.MiddleName);
                db.AddInParameter(createRMUserCmd, "@U_LastName", DbType.String, rmVo.LastName);
                db.AddInParameter(createRMUserCmd, "@U_Email", DbType.String, rmVo.Email);
                db.AddInParameter(createRMUserCmd, "@U_UserType", DbType.String, type.ToString());
                if (db.ExecuteNonQuery(createRMUserCmd) != 0)
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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CreateRMUser()");


                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = password;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public List<int> CreateCompleteRM(UserVo userVo, RMVo rmVo, int userId, bool isOpsIsChecked, bool isPurelyResearchLogin)
        {
            int rmId;
            int rmUserId;
            List<int> rmIds = new List<int>();
            Database db;
            DbCommand createRMCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createRMCmd = db.GetStoredProcCommand("SP_CreateCompleteRM");

                db.AddInParameter(createRMCmd, "@U_Password", DbType.String, userVo.Password);
                db.AddInParameter(createRMCmd, "@U_PasswordSaltValue", DbType.String, userVo.PasswordSaltValue);
                db.AddInParameter(createRMCmd, "@U_FirstName", DbType.String, userVo.FirstName);
                db.AddInParameter(createRMCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                db.AddInParameter(createRMCmd, "@U_LastName", DbType.String, userVo.LastName);
                db.AddInParameter(createRMCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(createRMCmd, "@U_UserType", DbType.String, userVo.UserType);
                // db.AddInParameter(createRMCmd, "@U_LoginId", DbType.String, userVo.LoginId);
                db.AddInParameter(createRMCmd, "@A_AdviserId", DbType.Int32, rmVo.AdviserId);
                db.AddInParameter(createRMCmd, "@AR_FirstName", DbType.String, rmVo.FirstName);
                db.AddInParameter(createRMCmd, "@AR_MiddleName", DbType.String, rmVo.MiddleName);
                db.AddInParameter(createRMCmd, "@AR_LastName", DbType.String, rmVo.LastName);
                db.AddInParameter(createRMCmd, "@AR_StaffCode", DbType.String, rmVo.StaffCode);
                db.AddInParameter(createRMCmd, "@AR_OfficePhoneDirectISD", DbType.Int32, rmVo.OfficePhoneDirectIsd);
                db.AddInParameter(createRMCmd, "@AR_OfficePhoneDirectSTD", DbType.Int32, rmVo.OfficePhoneDirectStd);
                db.AddInParameter(createRMCmd, "@AR_OfficePhoneDirect", DbType.Int32, rmVo.OfficePhoneDirectNumber);
                db.AddInParameter(createRMCmd, "@AR_OfficePhoneExtISD", DbType.Int32, rmVo.OfficePhoneExtIsd);
                db.AddInParameter(createRMCmd, "@AR_OfficePhoneExtSTD", DbType.Int32, rmVo.OfficePhoneExtStd);
                db.AddInParameter(createRMCmd, "@AR_OfficePhoneExt", DbType.Int32, rmVo.OfficePhoneExtNumber);
                db.AddInParameter(createRMCmd, "@AR_ResPhoneISD", DbType.Int32, rmVo.ResPhoneIsd);
                db.AddInParameter(createRMCmd, "@AR_ResPhoneSTD", DbType.Int32, rmVo.ResPhoneStd);
                db.AddInParameter(createRMCmd, "@AR_ResPhone", DbType.Int32, rmVo.ResPhoneNumber);
                db.AddInParameter(createRMCmd, "@AR_Mobile", DbType.Int64, rmVo.Mobile);
                db.AddInParameter(createRMCmd, "@AR_FaxISD", DbType.Int32, rmVo.FaxIsd);
                db.AddInParameter(createRMCmd, "@AR_FaxSTD", DbType.Int32, rmVo.FaxStd);
                db.AddInParameter(createRMCmd, "@AR_Fax", DbType.Int32, rmVo.Fax);
                db.AddInParameter(createRMCmd, "@AR_Email", DbType.String, rmVo.Email);
                db.AddInParameter(createRMCmd, "@AR_JobFunction", DbType.String, rmVo.RMRole);
                db.AddInParameter(createRMCmd, "@AR_IsExternalStaff", DbType.Int16, rmVo.IsExternal);
                db.AddInParameter(createRMCmd, "@AR_CTC", DbType.Double, rmVo.CTC);
                db.AddInParameter(createRMCmd, "@U_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createRMCmd, "@U_ModifiedBy", DbType.Int32, userId);
                
                if (isOpsIsChecked == true)
                    db.AddInParameter(createRMCmd, "@IsOpsOrRMStaff", DbType.Int16, 1);
                else
                    db.AddInParameter(createRMCmd, "@IsOpsOrRMStaff", DbType.Int16, 0);

                if (isPurelyResearchLogin == true)
                    db.AddInParameter(createRMCmd, "@IsPurelyResearchStaff", DbType.Int16, 1);
                else
                    db.AddInParameter(createRMCmd, "@IsPurelyResearchStaff", DbType.Int16, 0);

                if (rmVo.BranchId != 0)
                    db.AddInParameter(createRMCmd, "@BranchId", DbType.Int32, rmVo.BranchId);
                if (rmVo.HierarchyRoleId != 0)
                    db.AddInParameter(createRMCmd, "@HierarchyRoleId", DbType.Int32, rmVo.HierarchyRoleId);
                if (rmVo.ReportingManagerId != 0)
                    db.AddInParameter(createRMCmd, "@ReportingManagerId", DbType.Int32, rmVo.ReportingManagerId);
                if (rmVo.IsAssociateUser)
                    db.AddInParameter(createRMCmd, "@IsAssociateUser", DbType.Int16, 1);

                db.AddOutParameter(createRMCmd, "@AR_RMId", DbType.Int32, 1000000);
                db.AddOutParameter(createRMCmd, "@U_UserId", DbType.Int32, 1000000);
              

                if (db.ExecuteNonQuery(createRMCmd) != 0)
                {

                    rmUserId = int.Parse(db.GetParameterValue(createRMCmd, "U_UserId").ToString());
                    rmId = int.Parse(db.GetParameterValue(createRMCmd, "AR_RMId").ToString());


                    rmIds.Add(rmUserId);
                    rmIds.Add(rmId);

                }
                else
                {
                    rmIds = null;
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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CreateCompleteRM()");


                object[] objects = new object[3];
                objects[0] = rmVo;
                objects[1] = userVo;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmIds;
        }

        public bool CreateRMBranch(int rmId, int branchId, int userId)
        {
            bool result = false;
            Database db;
            DbCommand createRMBranchCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createRMBranchCmd = db.GetStoredProcCommand("SP_CreateRMBranch");

                db.AddInParameter(createRMBranchCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(createRMBranchCmd, "@AB_BranchId", DbType.Int32, branchId);
                db.AddInParameter(createRMBranchCmd, "@RMB_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createRMBranchCmd, "@RMB_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createRMBranchCmd) != 0)
                    result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CreateRMBranch()");


                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = branchId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public DataSet FindRM(string rmName, int advisorId, string sortorder, out int count)
        {
            //List<int> rmList = new List<int>();
            Database db;
            DbCommand findRMCmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                findRMCmd = db.GetStoredProcCommand("SP_FindRM");
                db.AddInParameter(findRMCmd, "@AR_FirstName", DbType.String, rmName);
                db.AddInParameter(findRMCmd, "@A_AdviserId", DbType.Int16, advisorId);
              
                //db.AddInParameter(findRMCmd, "@CurrentPage", DbType.Int16, currentpage);
               // db.AddInParameter(findRMCmd, "@SortOrder", DbType.String, sortorder);
                ds = db.ExecuteDataSet(findRMCmd);
              

                count = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:FindRM()");
                object[] objects = new object[2];
                objects[0] = rmName;
                objects[1] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public List<RMVo> GetBMRMList(int branchId, out int Count)
        {
            List<RMVo> rmList = new List<RMVo>();
            int rmId;
            RMVo rmVo;
            Database db;
            DbCommand getRMListCmd;
            DataSet getRMListDs;
            Count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMListCmd = db.GetStoredProcCommand("SP_GetRMList");
                db.AddInParameter(getRMListCmd, "@AB_BranchId", DbType.String, branchId);
                //db.AddInParameter(getRMListCmd, "@CurrentPage", DbType.Int32, currentPage);

                getRMListDs = db.ExecuteDataSet(getRMListCmd);
                if (getRMListDs.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in getRMListDs.Tables[0].Rows)
                    {
                        rmVo = new RMVo();
                        rmVo.UserId = int.Parse(dr["U_UserId"].ToString());
                        rmVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                        rmVo.FirstName = dr["AR_FirstName"].ToString();
                        rmVo.MiddleName = dr["AR_MiddleName"].ToString();
                        rmVo.LastName = dr["AR_LastName"].ToString();
                        rmVo.StaffCode = dr["AR_StaffCode"].ToString();
                        rmVo.OfficePhoneDirectNumber = int.Parse(dr["AR_OfficePhoneDirect"].ToString());
                        rmVo.OfficePhoneDirectIsd = int.Parse(dr["AR_OfficePhoneDirectISD"].ToString());
                        rmVo.OfficePhoneDirectStd = int.Parse(dr["AR_OfficePhoneDirectSTD"].ToString());
                        rmVo.OfficePhoneExtNumber = int.Parse(dr["AR_OfficePhoneExt"].ToString());
                        rmVo.OfficePhoneExtIsd = int.Parse(dr["AR_OfficePhoneExtISD"].ToString());
                        rmVo.OfficePhoneExtStd = int.Parse(dr["AR_OfficePhoneExtSTD"].ToString());
                        rmVo.ResPhoneIsd = int.Parse(dr["AR_ResPhoneISD"].ToString());
                        rmVo.ResPhoneStd = int.Parse(dr["AR_ResPhoneSTD"].ToString());
                        rmVo.ResPhoneNumber = int.Parse(dr["AR_ResPhone"].ToString());
                        rmVo.Fax = int.Parse(dr["AR_Fax"].ToString());
                        rmVo.FaxIsd = int.Parse(dr["AR_FaxISD"].ToString());
                        rmVo.FaxStd = int.Parse(dr["AR_FaxSTD"].ToString());
                        rmVo.Mobile = Convert.ToInt64(dr["AR_Mobile"].ToString());
                        rmVo.Email = dr["AR_Email"].ToString();
                        rmVo.RMRole = dr["AR_JobFunction"].ToString();
                        //rmVo.MainBranch = dr["MainBranch"].ToString();
                        //if (dr["AR_CTC"].ToString() != string.Empty) 
                        // rmVo.CTC = Double.Parse(dr["AR_CTC"].ToString());
                        rmVo.IsExternal = Int16.Parse(dr["AR_IsExternalStaff"].ToString());
                        //rmVo.MainBranch = dr["MainBranch"].ToString();
                        rmList.Add(rmVo);
                    }
                }
                else
                    rmList = null;
                if (getRMListDs.Tables[1].Rows.Count > 0)
                {
                    Count = int.Parse(getRMListDs.Tables[1].Rows[0][0].ToString());
                }
            }
            catch (Exception e)
            {
                string msg = e.Message.ToString();
            }
            return rmList;
        }

        public List<RMVo> GetRMList(int advisorId)
        {
            List<RMVo> rmList = new List<RMVo>();
            RMVo rmVo;
            Database db;
            DbCommand getAdvisorCmd;
            DataSet getAdvisorDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorCmd = db.GetStoredProcCommand("SP_GetAdviserStaff");
                db.AddInParameter(getAdvisorCmd, "@A_AdviserId", DbType.Int32, advisorId);
                getAdvisorDs = db.ExecuteDataSet(getAdvisorCmd);
                if (getAdvisorDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getAdvisorDs.Tables[0].Rows)
                    {
                        rmVo = new RMVo();
                        if (dr["U_UserId"].ToString()!="")
                        rmVo.UserId = int.Parse(dr["U_UserId"].ToString());
                        rmVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                        rmVo.FirstName = dr["AR_FirstName"].ToString();
                        rmVo.MiddleName = dr["AR_MiddleName"].ToString();
                        rmVo.LastName = dr["AR_LastName"].ToString();
                        rmVo.StaffCode = dr["AR_StaffCode"].ToString();
                        if (dr["AR_OfficePhoneDirect"].ToString() != "")
                            rmVo.OfficePhoneDirectNumber = int.Parse(dr["AR_OfficePhoneDirect"].ToString());
                        if (dr["AR_OfficePhoneDirectISD"].ToString() != "")
                            rmVo.OfficePhoneDirectIsd = int.Parse(dr["AR_OfficePhoneDirectISD"].ToString());
                        if (dr["AR_OfficePhoneDirectSTD"].ToString() != "")
                            rmVo.OfficePhoneDirectStd = int.Parse(dr["AR_OfficePhoneDirectSTD"].ToString());
                        if (dr["AR_OfficePhoneExt"].ToString() != "")
                            rmVo.OfficePhoneExtNumber = int.Parse(dr["AR_OfficePhoneExt"].ToString());
                        if (dr["AR_OfficePhoneExtISD"].ToString() != "")
                            rmVo.OfficePhoneExtIsd = int.Parse(dr["AR_OfficePhoneExtISD"].ToString());
                        if (dr["AR_OfficePhoneExtSTD"].ToString() != "")
                            rmVo.OfficePhoneExtStd = int.Parse(dr["AR_OfficePhoneExtSTD"].ToString());
                        if (dr["AR_ResPhoneISD"].ToString() != "")
                            rmVo.ResPhoneIsd = int.Parse(dr["AR_ResPhoneISD"].ToString());
                        if (dr["AR_ResPhoneSTD"].ToString() != "")
                            rmVo.ResPhoneStd = int.Parse(dr["AR_ResPhoneSTD"].ToString());
                        if (dr["AR_ResPhone"].ToString() != "")
                            rmVo.ResPhoneNumber = int.Parse(dr["AR_ResPhone"].ToString());
                        if (dr["AR_Fax"].ToString() != "")
                            rmVo.Fax = int.Parse(dr["AR_Fax"].ToString());
                        if (dr["AR_FaxISD"].ToString() != "")
                            rmVo.FaxIsd = int.Parse(dr["AR_FaxISD"].ToString());
                        if (dr["AR_FaxSTD"].ToString() != "")
                            rmVo.FaxStd = int.Parse(dr["AR_FaxSTD"].ToString());
                        if (dr["AR_Mobile"].ToString() != "")
                            rmVo.Mobile = Convert.ToInt64(dr["AR_Mobile"].ToString());

                        rmVo.Email = dr["AR_Email"].ToString();
                        rmVo.RMRole = dr["AR_JobFunction"].ToString();
                        //    rmVo.MainBranch = dr["MainBranch"].ToString();
                        rmList.Add(rmVo);
                    }
                }
                else
                    rmList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetRMList()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmList;
        }

        public List<RMVo> GetRMList(int advisorId, string sortOrder, string nameSrch)
        {
            List<RMVo> rmList = new List<RMVo>();
            RMVo rmVo;
            Database db;
            DbCommand getAdvisorCmd;
            DataSet getAdvisorDs;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorCmd = db.GetStoredProcCommand("SP_GetAdviserRMList");
                db.AddInParameter(getAdvisorCmd, "@A_AdviserId", DbType.Int32, advisorId);
                //db.AddInParameter(getAdvisorCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getAdvisorCmd, "@SortOrder", DbType.String, sortOrder);
                db.AddInParameter(getAdvisorCmd, "@nameSrch", DbType.String, nameSrch);
                getAdvisorDs = db.ExecuteDataSet(getAdvisorCmd);
               
                //Count = Int32.Parse(getAdvisorDs.Tables[1].Rows[0][0].ToString());
                if (getAdvisorDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getAdvisorDs.Tables[0].Rows)
                    {
                        rmVo = new RMVo();
                        rmVo.UserId = int.Parse(dr["U_UserId"].ToString());
                        rmVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                        rmVo.FirstName = dr["AR_FirstName"].ToString();
                        rmVo.MiddleName = dr["AR_MiddleName"].ToString();
                        rmVo.LastName = dr["AR_LastName"].ToString();
                        if (!string.IsNullOrEmpty(dr["AR_StaffCode"].ToString()))
                            rmVo.StaffCode = dr["AR_StaffCode"].ToString();
                        if (dr["AR_OfficePhoneDirect"].ToString() != "" && dr["AR_OfficePhoneDirect"] != null)
                            rmVo.OfficePhoneDirectNumber = int.Parse(dr["AR_OfficePhoneDirect"].ToString());
                        if (dr["AR_OfficePhoneDirectISD"].ToString() != "" && dr["AR_OfficePhoneDirectISD"] != null)
                            rmVo.OfficePhoneDirectIsd = int.Parse(dr["AR_OfficePhoneDirectISD"].ToString());
                        if (dr["AR_OfficePhoneDirectSTD"].ToString() != "" && dr["AR_OfficePhoneDirectSTD"] != null)
                            rmVo.OfficePhoneDirectStd = int.Parse(dr["AR_OfficePhoneDirectSTD"].ToString());
                        if (dr["AR_OfficePhoneExt"].ToString() != "" && dr["AR_OfficePhoneExt"] != null)
                            rmVo.OfficePhoneExtNumber = int.Parse(dr["AR_OfficePhoneExt"].ToString());
                        if (dr["AR_OfficePhoneExtISD"].ToString() != "" && dr["AR_OfficePhoneExtISD"] != null)
                            rmVo.OfficePhoneExtIsd = int.Parse(dr["AR_OfficePhoneExtISD"].ToString());
                        if (dr["AR_OfficePhoneExtSTD"].ToString() != "" && dr["AR_OfficePhoneExtSTD"] != null)
                            rmVo.OfficePhoneExtStd = int.Parse(dr["AR_OfficePhoneExtSTD"].ToString());
                        if (dr["AR_ResPhoneISD"].ToString() != "" && dr["AR_ResPhoneISD"] != null)
                            rmVo.ResPhoneIsd = int.Parse(dr["AR_ResPhoneISD"].ToString());
                        if (dr["AR_ResPhoneSTD"].ToString() != "" && dr["AR_ResPhoneSTD"] != null)
                            rmVo.ResPhoneStd = int.Parse(dr["AR_ResPhoneSTD"].ToString());
                        if (dr["AR_ResPhone"].ToString() != "" && dr["AR_ResPhone"] != null)
                            rmVo.ResPhoneNumber = int.Parse(dr["AR_ResPhone"].ToString());
                        if (dr["AR_Fax"].ToString() != "" && dr["AR_Fax"] != null)
                            rmVo.Fax = int.Parse(dr["AR_Fax"].ToString());
                        if (dr["AR_FaxISD"].ToString() != "" && dr["AR_FaxISD"] != null)
                            rmVo.FaxIsd = int.Parse(dr["AR_FaxISD"].ToString());
                        if (dr["AR_FaxSTD"].ToString() != "" && dr["AR_FaxSTD"] != null)
                            rmVo.FaxStd = int.Parse(dr["AR_FaxSTD"].ToString());
                        if (dr["AR_Mobile"].ToString() != "" && dr["AR_Mobile"] != null)
                            rmVo.Mobile = Convert.ToInt64(dr["AR_Mobile"].ToString());
                        rmVo.Email = dr["AR_Email"].ToString();
                        rmVo.RMRole = dr["AR_JobFunction"].ToString();
                        //rmVo.MainBranch = dr["MainBranch"].ToString();
                        //if (dr["AR_CTC"].ToString() != string.Empty) 
                        // rmVo.CTC = Double.Parse(dr["AR_CTC"].ToString());
                        rmVo.IsExternal = Int16.Parse(dr["AR_IsExternalStaff"].ToString());
                        if (!string.IsNullOrEmpty(dr["BranchList"].ToString().Trim()))
                            rmVo.BranchList = dr["BranchList"].ToString();
                        else
                            rmVo.BranchList = string.Empty;
                        rmList.Add(rmVo);
                    }
                }
                else
                    rmList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetRMList()");

                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmList;
        }

        public int GetUserId(int rmId)
        {
            int userId = 0;
            Database db;
            DbCommand getUserIdCmd;
            DataSet getUserIdDs;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUserIdCmd = db.GetSqlStringCommand("select U_UserId from AdviserRM where AR_RMId=" + rmId);
                getUserIdDs = db.ExecuteDataSet(getUserIdCmd);
                if (getUserIdDs.Tables[0].Rows.Count > 0)
                {
                    dr = getUserIdDs.Tables[0].Rows[0];
                    userId = int.Parse(dr["U_UserId"].ToString());
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
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetUserId()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return userId;
        }

        public RMVo GetAdvisorStaff(int userId)
        {
            RMVo rmVo = new RMVo();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            Database db;
            DbCommand getAdvisorStaffCmd;
            DataSet getAdvisorStaffDs;
            // DataTable table;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorStaffCmd = db.GetStoredProcCommand("SP_GetAdviserStaffDetails");
                db.AddInParameter(getAdvisorStaffCmd, "@U_UserId", DbType.Int32, userId);
                getAdvisorStaffDs = db.ExecuteDataSet(getAdvisorStaffCmd);
                if (getAdvisorStaffDs.Tables[0].Rows.Count > 0)
                {
                    // table = getAdvisorStaffDs.Tables["AdviserRM"];
                    dr = getAdvisorStaffDs.Tables[0].Rows[0];
                    rmVo.UserId = int.Parse((dr["U_UserId"].ToString()));
                    rmVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                    rmVo.FirstName = dr["AR_FirstName"].ToString();
                    if (dr["AR_MiddleName"] != DBNull.Value)
                        rmVo.MiddleName = dr["AR_MiddleName"].ToString();
                    else
                        rmVo.MiddleName = string.Empty;
                    if (dr["AR_LastName"] != DBNull.Value)
                        rmVo.LastName = dr["AR_LastName"].ToString();
                    else
                        rmVo.LastName = string.Empty;
                    if (dr["AR_StaffCode"] != DBNull.Value)
                        rmVo.StaffCode = dr["AR_StaffCode"].ToString(); //dr["AAC_AgentCode"].ToString();    //dr["AR_StaffCode"].ToString();
                    else
                        rmVo.StaffCode = string.Empty;
                    if (dr["AR_OfficePhoneDirect"] != DBNull.Value)
                        rmVo.OfficePhoneDirectNumber = int.Parse(dr["AR_OfficePhoneDirect"].ToString());
                    if (dr["AR_OfficePhoneDirectISD"] != DBNull.Value)
                        rmVo.OfficePhoneDirectIsd = int.Parse(dr["AR_OfficePhoneDirectISD"].ToString());
                    if (dr["AR_OfficePhoneDirectSTD"] != DBNull.Value)
                        rmVo.OfficePhoneDirectStd = int.Parse(dr["AR_OfficePhoneDirectSTD"].ToString());
                    if (dr["AR_OfficePhoneExt"] != DBNull.Value)
                        rmVo.OfficePhoneExtNumber = int.Parse(dr["AR_OfficePhoneExt"].ToString());
                    if (dr["AR_OfficePhoneExtISD"] != DBNull.Value)
                        rmVo.OfficePhoneExtIsd = int.Parse(dr["AR_OfficePhoneExtISD"].ToString());
                    if (dr["AR_OfficePhoneExtSTD"] != DBNull.Value)
                        rmVo.OfficePhoneExtStd = int.Parse(dr["AR_OfficePhoneExtSTD"].ToString());
                    if (dr["AR_ResPhoneISD"] != DBNull.Value)
                        rmVo.ResPhoneIsd = int.Parse(dr["AR_ResPhoneISD"].ToString());
                    if (dr["AR_ResPhoneSTD"] != DBNull.Value)
                        rmVo.ResPhoneStd = int.Parse(dr["AR_ResPhoneSTD"].ToString());
                    if (dr["AR_ResPhone"] != DBNull.Value)
                        rmVo.ResPhoneNumber = int.Parse(dr["AR_ResPhone"].ToString());
                    if (dr["AR_Fax"] != DBNull.Value)
                        rmVo.Fax = int.Parse(dr["AR_Fax"].ToString());
                    if (dr["AR_FaxISD"] != DBNull.Value)
                        rmVo.FaxIsd = int.Parse(dr["AR_FaxISD"].ToString());
                    if (dr["AR_FaxSTD"] != DBNull.Value)
                        rmVo.FaxStd = int.Parse(dr["AR_FaxSTD"].ToString());
                    if (dr["AR_Mobile"] != DBNull.Value)
                        rmVo.Mobile = Convert.ToInt64(dr["AR_Mobile"].ToString());
                    if (dr["AR_Email"] != DBNull.Value)
                        rmVo.Email = dr["AR_Email"].ToString();
                    else
                        rmVo.Email = string.Empty;
                    if (dr["AR_JobFunction"] != DBNull.Value)
                        rmVo.RMRole = dr["AR_JobFunction"].ToString();
                    else
                        rmVo.RMRole = string.Empty;

                    if (dr["RoleList"] != DBNull.Value)
                        rmVo.RMRoleList = dr["RoleList"].ToString();
                    else
                        rmVo.RMRoleList = string.Empty;

                    if (!string.IsNullOrEmpty(dr["BranchList"].ToString().Trim()))
                        rmVo.BranchList = dr["BranchList"].ToString();
                    else
                        rmVo.BranchList = string.Empty;

                    if (dr["AR_IsExternalStaff"] != DBNull.Value && dr["AR_IsExternalStaff"].ToString() != "")
                        rmVo.IsExternal = Int16.Parse(dr["AR_IsExternalStaff"].ToString());
                    else
                        rmVo.IsExternal = 1;

                    if (dr["AR_CTC"].ToString() != "")
                        rmVo.CTC = Double.Parse(dr["AR_CTC"].ToString());
                    else
                        rmVo.CTC = 0;

                    if (dr["A_AdviserId"] != DBNull.Value)
                        rmVo.AdviserId = int.Parse(dr["A_AdviserId"].ToString());
                    else
                        rmVo.AdviserId = 0;

                    if (dr["AAC_AgentCode"] != DBNull.Value)
                        rmVo.AAC_AgentCode =  dr["AAC_AgentCode"].ToString() ;
                    else
                        rmVo.AAC_AgentCode ="";
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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdvisorStaff()");

                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmVo;
        }

        public RMVo GetAdvisorStaffDetails(int rmId)
        {
            RMVo rmVo = new RMVo();
            AdvisorStaffDao advisorStaffDao = new AdvisorStaffDao();
            Database db;
            DbCommand getAdvisorStaffCmd;
            DataSet getAdvisorStaffDs;
            // DataTable table;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorStaffCmd = db.GetStoredProcCommand("SP_GetAdviserStaffDetailsByRMId");
                db.AddInParameter(getAdvisorStaffCmd, "@AR_RMId", DbType.Int32, rmId);
                getAdvisorStaffDs = db.ExecuteDataSet(getAdvisorStaffCmd);

                if (getAdvisorStaffDs.Tables[0].Rows.Count > 0)
                {
                    // table = getAdvisorStaffDs.Tables["AdviserRM"];
                    dr = getAdvisorStaffDs.Tables[0].Rows[0];
                    rmVo.UserId = int.Parse((dr["U_UserId"].ToString()));
                    rmVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                    rmVo.FirstName = dr["AR_FirstName"].ToString();
                    if (dr["AR_MiddleName"] != DBNull.Value)
                        rmVo.MiddleName = dr["AR_MiddleName"].ToString();
                    else
                        rmVo.MiddleName = string.Empty;
                    if (dr["AR_LastName"] != DBNull.Value)
                        rmVo.LastName = dr["AR_LastName"].ToString();
                    else
                        rmVo.LastName = string.Empty;
                    if (dr["AR_StaffCode"] != DBNull.Value)
                        rmVo.StaffCode = dr["AR_StaffCode"].ToString();
                    else
                        rmVo.StaffCode = string.Empty;
                    if (dr["AR_OfficePhoneDirect"] != DBNull.Value)
                        rmVo.OfficePhoneDirectNumber = int.Parse(dr["AR_OfficePhoneDirect"].ToString());
                    if (dr["AR_OfficePhoneDirectISD"] != DBNull.Value)
                        rmVo.OfficePhoneDirectIsd = int.Parse(dr["AR_OfficePhoneDirectISD"].ToString());
                    if (dr["AR_OfficePhoneDirectSTD"] != DBNull.Value)
                        rmVo.OfficePhoneDirectStd = int.Parse(dr["AR_OfficePhoneDirectSTD"].ToString());
                    if (dr["AR_OfficePhoneExt"] != DBNull.Value)
                        rmVo.OfficePhoneExtNumber = int.Parse(dr["AR_OfficePhoneExt"].ToString());
                    if (dr["AR_OfficePhoneExtISD"] != DBNull.Value)
                        rmVo.OfficePhoneExtIsd = int.Parse(dr["AR_OfficePhoneExtISD"].ToString());
                    if (dr["AR_OfficePhoneExtSTD"] != DBNull.Value)
                        rmVo.OfficePhoneExtStd = int.Parse(dr["AR_OfficePhoneExtSTD"].ToString());
                    if (dr["AR_ResPhoneISD"] != DBNull.Value)
                        rmVo.ResPhoneIsd = int.Parse(dr["AR_ResPhoneISD"].ToString());
                    if (dr["AR_ResPhoneSTD"] != DBNull.Value)
                        rmVo.ResPhoneStd = int.Parse(dr["AR_ResPhoneSTD"].ToString());
                    if (dr["AR_ResPhone"] != DBNull.Value)
                        rmVo.ResPhoneNumber = int.Parse(dr["AR_ResPhone"].ToString());
                    if (dr["AR_Fax"] != DBNull.Value)
                        rmVo.Fax = int.Parse(dr["AR_Fax"].ToString());
                    if (dr["AR_FaxISD"] != DBNull.Value)
                        rmVo.FaxIsd = int.Parse(dr["AR_FaxISD"].ToString());
                    if (dr["AR_FaxSTD"] != DBNull.Value)
                        rmVo.FaxStd = int.Parse(dr["AR_FaxSTD"].ToString());
                    if (dr["AR_Mobile"] != DBNull.Value)
                        rmVo.Mobile = Convert.ToInt64(dr["AR_Mobile"].ToString());
                    if (dr["AR_Email"] != DBNull.Value)
                        rmVo.Email = dr["AR_Email"].ToString();
                    else
                        rmVo.Email = string.Empty;
                    if (dr["AR_JobFunction"] != DBNull.Value)
                        rmVo.RMRole = dr["AR_JobFunction"].ToString();
                    else
                        rmVo.RMRole = string.Empty;

                    if (dr["RoleList"] != DBNull.Value)
                        rmVo.RMRoleList = dr["RoleList"].ToString();
                    else
                        rmVo.RMRoleList = string.Empty;

                    if (!string.IsNullOrEmpty(dr["BranchList"].ToString().Trim()))
                        rmVo.BranchList = dr["BranchList"].ToString();
                    else
                        rmVo.BranchList = string.Empty;

                    if (dr["AR_IsExternalStaff"] != DBNull.Value && dr["AR_IsExternalStaff"].ToString() != "")
                        rmVo.IsExternal = Int16.Parse(dr["AR_IsExternalStaff"].ToString());
                    else
                        rmVo.IsExternal = 1;

                    if (dr["AR_CTC"].ToString() != "")
                        rmVo.CTC = Double.Parse(dr["AR_CTC"].ToString());
                    else
                        rmVo.CTC = 0;

                    if (dr["A_AdviserId"] != DBNull.Value)
                        rmVo.AdviserId = int.Parse(dr["A_AdviserId"].ToString());
                    else
                        rmVo.AdviserId = 0;
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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdvisorStaffDetails()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return rmVo;
        }

        public bool UpdateStaff(RMVo rmVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateAdvisorStaffCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdvisorStaffCmd = db.GetStoredProcCommand("SP_UpdateAdviserStaff");
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_RMId", DbType.Int32, rmVo.RMId);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_FirstName", DbType.String, rmVo.FirstName);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_MiddleName", DbType.String, rmVo.MiddleName);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_LastName", DbType.String, rmVo.LastName);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_StaffCode", DbType.String, rmVo.StaffCode);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_OfficePhoneDirectISD", DbType.Int32, rmVo.OfficePhoneDirectIsd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_OfficePhoneDirectSTD", DbType.Int32, rmVo.OfficePhoneDirectStd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_OfficePhoneDirect", DbType.Int32, rmVo.OfficePhoneDirectNumber);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_OfficePhoneExtISD", DbType.Int32, rmVo.OfficePhoneExtIsd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_OfficePhoneExtSTD", DbType.Int32, rmVo.OfficePhoneExtStd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_OfficePhoneExt", DbType.Int32, rmVo.OfficePhoneExtNumber);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_ResPhoneISD", DbType.Int32, rmVo.ResPhoneIsd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_ResPhoneSTD", DbType.Int32, rmVo.ResPhoneStd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_ResPhone", DbType.Int32, rmVo.ResPhoneNumber);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_Mobile", DbType.Int64, rmVo.Mobile);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_Fax", DbType.String, rmVo.Fax);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_FaxISD", DbType.String, rmVo.FaxIsd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_FaxSTD", DbType.String, rmVo.FaxStd);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_Email", DbType.String, rmVo.Email);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_CTC", DbType.String, rmVo.CTC);
                db.AddInParameter(updateAdvisorStaffCmd, "@AR_IsExternalStaff", DbType.String, rmVo.IsExternal);
                db.AddInParameter(updateAdvisorStaffCmd, "@AAC_AdviserAgentId", DbType.String, "");
                db.AddInParameter(updateAdvisorStaffCmd, "@AAC_UserType", DbType.String, rmVo.RMRole);
                db.AddInParameter(updateAdvisorStaffCmd, "@AAC_AgentCode", DbType.String, rmVo.AAC_AgentCode);

                if (rmVo.BranchId != 0)
                    db.AddInParameter(updateAdvisorStaffCmd, "@AB_StaffBranchId", DbType.Int32, rmVo.BranchId);
                if (rmVo.HierarchyRoleId != 0)
                    db.AddInParameter(updateAdvisorStaffCmd, "@AH_HierarchyId", DbType.Int32, rmVo.HierarchyRoleId);
                if (rmVo.ReportingManagerId != 0)
                    db.AddInParameter(updateAdvisorStaffCmd, "@AR_ReportingManagerId", DbType.Int32, rmVo.ReportingManagerId);

                if (db.ExecuteNonQuery(updateAdvisorStaffCmd) != 0)

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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:UpdateStaff()");

                object[] objects = new object[3];
                objects[0] = rmVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public List<CustomerVo> FindCustomer(string CustomerName,
                                            int rmId,
                                            int currentPage,
                                            out int count,
                                            string sortExpression,
                                            string nameFilter,
                                            string areaFilter,
                                            string pincodeFilter,
                                            string parentFilter,
                                            string cityFilter,
                                            out Dictionary<string, string> genDictParent,
                                            out Dictionary<string, string> genDictCity
                                            )
        {
            List<CustomerVo> customerList;
            Database db;
            DbCommand FindCustomerCmd;
            DataSet ds;
            CustomerVo customerVo;

            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();

            count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                FindCustomerCmd = db.GetStoredProcCommand("SP_FindRMCustomer");
                db.AddInParameter(FindCustomerCmd, "@C_FirstName", DbType.String, CustomerName);
                db.AddInParameter(FindCustomerCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(FindCustomerCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(FindCustomerCmd, "@SortOrder", DbType.String, sortExpression);
                if (nameFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@nameFilter", DbType.String, nameFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (areaFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@areaFilter", DbType.String, areaFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@areaFilter", DbType.String, DBNull.Value);
                if (pincodeFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@pincodeFilter", DbType.String, pincodeFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@pincodeFilter", DbType.String, DBNull.Value);
                if (parentFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@parentFilter", DbType.String, parentFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@parentFilter", DbType.String, DBNull.Value);
                if (cityFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@cityFilter", DbType.String, cityFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@cityFilter", DbType.String, DBNull.Value);

                ds = db.ExecuteDataSet(FindCustomerCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<CustomerVo>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());

                        customerVo.Email = dr["C_Email"].ToString();
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerVo.IsFPClient = int.Parse(dr["C_IsFPClient"].ToString());
                        if (dr["CA_AssociationId"].ToString() != string.Empty)
                            customerVo.AssociationId = int.Parse(dr["CA_AssociationId"].ToString());
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;

                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        genDictParent.Add(dr["Parent"].ToString(), dr["Parent"].ToString());
                    }
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        genDictCity.Add(dr["C_Adr1City"].ToString(), dr["C_Adr1City"].ToString());
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
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:FindCustomer()");
                object[] objects = new object[2];
                objects[0] = CustomerName;
                objects[1] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (ds.Tables[1].Rows.Count > 0)
                count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());

            return customerList;
        }

        public List<CustomerVo> GetCustomerForAssociation(int customerId, int rmId, int currentPage, string sortOrder, out int Count)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetCustomerForAssociation");
                db.AddInParameter(getCustomerListCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getCustomerListCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, sortOrder);
                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                Count = Int32.Parse(getCustomerDs.Tables[1].Rows[0][0].ToString());
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.AssociationId = int.Parse(dr["CA_AssociationId"].ToString());
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        customerVo.CompanyName = dr["C_CompanyName"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetCustomerForAssociation()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }

        public List<CustomerVo> GetCustomerList(int rmId, int currentPage, out int count, string sortExpression,string panFilter, string nameFilter, string areaFilter, string pincodeFilter, string parentFilter, string cityFilter, string Active, string IsProspect, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictCity)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();

            count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetRMCustomerList");
                db.AddInParameter(getCustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getCustomerListCmd, "@CurrentPage", DbType.Int32, currentPage);


                if (sortExpression != "")
                    db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, sortExpression);
                else
                    db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, DBNull.Value);

                if (nameFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, nameFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (areaFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, areaFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, DBNull.Value);
                db.AddInParameter(getCustomerListCmd, "@pincodeFilter", DbType.String, pincodeFilter);
                db.AddInParameter(getCustomerListCmd, "@parentFilter", DbType.String, parentFilter);
                db.AddInParameter(getCustomerListCmd, "@cityFilter", DbType.String, cityFilter);
                if (Active != "")
                    db.AddInParameter(getCustomerListCmd, "@active", DbType.String, Active);
                else
                    db.AddInParameter(getCustomerListCmd, "@active", DbType.String, "2");

                if (IsProspect != "")
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, IsProspect);
                else
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, "2");

                if (panFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, panFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, DBNull.Value);


                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        if (dr["ADUL_ProcessId"].ToString() == null || dr["ADUL_ProcessId"].ToString() == "")
                        {
                            customerVo.ProcessId = 0;

                        }
                        else
                        {
                            customerVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        }
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.IsActive = int.Parse(dr["C_IsActive"].ToString());
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerVo.IsFPClient = int.Parse(dr["C_IsFPClient"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerVo.IsFPClient = int.Parse(dr["C_IsFPClient"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());

                        if (dr["C_IsProspect"].ToString() != "")
                            customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;

                if (getCustomerDs.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[2].Rows)
                    {
                        genDictParent.Add(dr["Parent"].ToString(), dr["Parent"].ToString());
                    }
                }

                if (getCustomerDs.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[3].Rows)
                    {
                        genDictCity.Add(dr["C_Adr1City"].ToString(), dr["C_Adr1City"].ToString());
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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (getCustomerDs.Tables[1].Rows.Count > 0)
                count = Int32.Parse(getCustomerDs.Tables[1].Rows[0]["CNT"].ToString());

            return customerList;
        }

        public List<CustomerVo> GetBMCustomerList(int rmId, int currentPage, out int count, string sortExpression,string panFilter, string nameFilter, string areaFilter, string pincodeFilter, string parentFilter, string cityFilter, string RMFilter, string isProspect, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictCity, out Dictionary<string, string> genDictRM)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();

            count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetBMCustomerList");
                db.AddInParameter(getCustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getCustomerListCmd, "@CurrentPage", DbType.Int32, currentPage);
                if (!string.IsNullOrEmpty(sortExpression.Trim()))
                  db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, sortExpression);
                if (nameFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, nameFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (areaFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, areaFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, DBNull.Value);
                db.AddInParameter(getCustomerListCmd, "@pincodeFilter", DbType.String, pincodeFilter);
                db.AddInParameter(getCustomerListCmd, "@parentFilter", DbType.String, parentFilter);
                db.AddInParameter(getCustomerListCmd, "@cityFilter", DbType.String, cityFilter);
                if (RMFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, RMFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, DBNull.Value);

                if (isProspect != "")
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, isProspect);
                else
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, "2");

                if (panFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, panFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, DBNull.Value);

                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        if (dr["ADUL_ProcessId"].ToString() == null || dr["ADUL_ProcessId"].ToString() == "" )
                        {
                          customerVo.ProcessId = 0;
                            
                        }
                        else
                        {
                            customerVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        }
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["RMName"] != "")
                            customerVo.AssignedRM = dr["RMName"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        if (dr["C_IsProspect"].ToString() != "")
                            customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;

                if (getCustomerDs.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[2].Rows)
                    {
                        genDictParent.Add(dr["Parent"].ToString(), dr["Parent"].ToString());
                    }
                }

                if (getCustomerDs.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[3].Rows)
                    {
                        genDictCity.Add(dr["C_Adr1City"].ToString(), dr["C_Adr1City"].ToString());
                    }
                }

                if (getCustomerDs.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[4].Rows)
                    {
                        if (dr["RMName"].ToString().Trim() != "")
                        {
                            genDictRM.Add(dr["RMId"].ToString(), dr["RMName"].ToString());
                        }
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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetBMCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (getCustomerDs.Tables[1].Rows.Count > 0)
                count = Int32.Parse(getCustomerDs.Tables[1].Rows[0]["CNT"].ToString());

            return customerList;
        }

        public List<CustomerVo> GetCustomerList(int rmId)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetRMAllCustomerList");
                db.AddInParameter(getCustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        if (dr["ADUL_ProcessId"].ToString() == null || dr["ADUL_ProcessId"].ToString() == "")
                        {
                            customerVo.ProcessId = 0;

                        }
                        else
                        {
                            customerVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        }
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerVo.IsFPClient = int.Parse(dr["C_IsFPClient"].ToString());
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerVo.IsFPClient = int.Parse(dr["C_IsFPClient"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        customerList.Add(customerVo);

                    }
                }
                else
                    customerList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetCustomerList()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerList;
        }

        public List<CustomerVo> GetBMCustomerList(int rmId)
        {
            List<CustomerVo> bmCustomerList = new List<CustomerVo>();
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetBMAllCustomerList");
                db.AddInParameter(getCustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        if (dr["ADUL_ProcessId"].ToString() == null || dr["ADUL_ProcessId"].ToString() == "")
                        {
                            customerVo.ProcessId = 0;

                        }
                        else
                        {
                            customerVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        }
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["RMName"] != "")
                            customerVo.AssignedRM = dr["RMName"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        bmCustomerList.Add(customerVo);
                    }
                }
                else
                    bmCustomerList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetCustomerList()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bmCustomerList;
        }

        public List<CustomerVo> GetCustomerList(string customerName, int rmId)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetCustomerList");
                db.AddInParameter(getCustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getCustomerListCmd, "@C_FirstName", DbType.String, customerName);

                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        customerList.Add(customerVo);

                    }
                }
                else
                    customerList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetCustomerList()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerList;
        }

        public int GetCustomerList(int rmId, string Flag)
        {
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetRMCustomerList");
                db.AddInParameter(getCustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getCustomerListCmd, "@Flag", DbType.String, Flag);
                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetCustomerList()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Convert.ToInt32(getCustomerDs.Tables[0].Rows[0][0].ToString());
        }

        public DataSet GetRMCustomerListDataSet(int rmId)
        {
            Database db;
            DbCommand CustomerListCmd;
            DataSet dsCustomerList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CustomerListCmd = db.GetStoredProcCommand("SP_GetRMCustomerListDataSet");
                db.AddInParameter(CustomerListCmd, "@AR_RMId", DbType.Int32, rmId);
                dsCustomerList = db.ExecuteDataSet(CustomerListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetRMCustomerListDataSet()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsCustomerList;

        }

        public DataSet GetBMStaff(int rmId, int currentPage, string sortOrder, out int Count)
        {
            Database db;
            DbCommand getStaffListCmd;
            DataSet getStaffListDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getStaffListCmd = db.GetStoredProcCommand("SP_BMGetStaff");
                db.AddInParameter(getStaffListCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getStaffListCmd, "@currentPage", DbType.Int32, currentPage);
                db.AddInParameter(getStaffListCmd, "@sortOrder", DbType.String, sortOrder);

                getStaffListDs = db.ExecuteDataSet(getStaffListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetBMStaff()");
                object[] objects = new object[1];
                objects[0] = rmId;
                objects[1] = currentPage;
                objects[2] = sortOrder;
                objects[3] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Convert.ToInt32(getStaffListDs.Tables[0].Rows[0]["CNT"].ToString());
            return getStaffListDs;
        }

        public DataTable GetAdvisorAssociatesDropDown(int advisorId)
        {
            Database db;
            DbCommand getStaffListCmd;
            DataSet getStaffListDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getStaffListCmd = db.GetStoredProcCommand("SP_GetAdvisorAssociatesDDL");
                db.AddInParameter(getStaffListCmd, "@A_AdviserId", DbType.Int32, advisorId);

                getStaffListDs = db.ExecuteDataSet(getStaffListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdvisorAssociatesDropDown()");
                object[] objects = new object[1];
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return getStaffListDs.Tables[0];
        }

        public DataTable GetExternalRMList(int adviserId, int flag)
        {
            Database db;
            DbCommand cmdExternalRM;
            DataSet ds = null;
            DataTable dt;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdExternalRM = db.GetStoredProcCommand("SP_GetAdviserExternalStaffRMList");
                db.AddInParameter(cmdExternalRM, "@A_AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(cmdExternalRM, "@Flag", DbType.Int16, flag);
                ds = db.ExecuteDataSet(cmdExternalRM);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:FindRM()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public int CheckRMMainBranch(int rmId)
        {
            Database db;
            DbCommand cmdRMMainBranch;
            int cnt;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdRMMainBranch = db.GetStoredProcCommand("SP_CheckRMMainBranch");
                db.AddInParameter(cmdRMMainBranch, "@AR_RMId", DbType.Int32, rmId);
                cnt = (Int32)db.ExecuteScalar(cmdRMMainBranch);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CheckRMMainBranch()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return cnt;
        }

        public int GetRMBranchId(int rmId)
        {
            Database db;
            DbCommand cmdGetRMBranchId;
            int branchId = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRMBranchId = db.GetStoredProcCommand("SP_RMGetMainBranch");
                db.AddInParameter(cmdGetRMBranchId, "@AR_RMId", DbType.Int32, rmId);
                if (db.ExecuteScalar(cmdGetRMBranchId) != null)
                    Int32.TryParse(db.ExecuteScalar(cmdGetRMBranchId).ToString(), out branchId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetRMBranchId()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return branchId;
        }
        public int GetAdviserAgentID(string AgentCode,string UserType)
        {
            Database db;
            DbCommand cmdGetAdviserAgentID;
            int AdviserAgentID = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAdviserAgentID = db.GetStoredProcCommand("SP_GetAdviserAgentID");
                db.AddInParameter(cmdGetAdviserAgentID, "@AAC_AgentCode", DbType.String, AgentCode);
                db.AddInParameter(cmdGetAdviserAgentID, "@AAC_UserType", DbType.String, UserType);

                if (db.ExecuteScalar(cmdGetAdviserAgentID) != null)
                    Int32.TryParse(db.ExecuteScalar(cmdGetAdviserAgentID).ToString(), out AdviserAgentID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdviserAgentID()");
                object[] objects = new object[1];
                objects[0] = AdviserAgentID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AdviserAgentID;
        }
      
        public DataTable GetBranchRMList(int branchId)
        {
            Database db;
            DbCommand getBranchRMListCmd;
            DataSet getBranchRMListDs;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchRMListCmd = db.GetStoredProcCommand("SP_GetBranchRMList");
                db.AddInParameter(getBranchRMListCmd, "@AB_BranchId", DbType.Int32, branchId);

                getBranchRMListDs = db.ExecuteDataSet(getBranchRMListCmd);
                dt = getBranchRMListDs.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetBranchRMList()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dt;
        }

        public DataTable GetAdviserRM(int adviserId)
        {
            Database db;
            DbCommand getAdviserRMListCmd;
            DataSet getAdviserRMListDs;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserRMListCmd = db.GetStoredProcCommand("SP_GetAdviserRM");
                db.AddInParameter(getAdviserRMListCmd, "@A_AdviserId", DbType.Int32, adviserId);

                getAdviserRMListDs = db.ExecuteDataSet(getAdviserRMListCmd);
                dt = getAdviserRMListDs.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdviserRM()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dt;
        }

        /// <summary>
        /// Checks the RM BM Role Have4 any dependency...
        /// </summary>
        /// <param name="RMID"></param>
        /// <returns></returns>
        public Hashtable CheckRMDependency(int RMID)
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            Database db;
            DbCommand CheckRMCustomerCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckRMCustomerCmd = db.GetStoredProcCommand("SP_CheckRMDependency");
                db.AddInParameter(CheckRMCustomerCmd, "@RMId", DbType.Int32, RMID);
                db.AddOutParameter(CheckRMCustomerCmd, "@CustomerCount", DbType.Int32, 50000);
                db.AddOutParameter(CheckRMCustomerCmd, "@BranchHead", DbType.Int32, 1000);
                if (db.ExecuteNonQuery(CheckRMCustomerCmd) != 0)
                {
                    ht.Add("RMCustomerCount", db.GetParameterValue(CheckRMCustomerCmd, "CustomerCount").ToString());
                    ht.Add("BMBranchHead", db.GetParameterValue(CheckRMCustomerCmd, "BranchHead").ToString());
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
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CheckRMDependency()");
                object[] objects = new object[1];
                objects[0] = RMID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ht;
        }


        /* Function For Branch Manager */

        public List<RMVo> GetBMRMList(int branchId, int branchHeadId, int all)
        {
            List<RMVo> rmList = new List<RMVo>();
            int rmId;
            RMVo rmVo;
            Database db;
            DbCommand getRMListCmd;
            DataSet getRMListDs;
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMListCmd = db.GetStoredProcCommand("SP_GetAllBMStaffs");
                db.AddInParameter(getRMListCmd, "@AB_BranchId", DbType.String, branchId);
                db.AddInParameter(getRMListCmd, "@branchHeadId", DbType.String, branchHeadId);
                db.AddInParameter(getRMListCmd, "@all", DbType.String, all);
                //db.AddInParameter(getRMListCmd, "@CurrentPage", DbType.Int32, currentPage);

                getRMListDs = db.ExecuteDataSet(getRMListCmd);
                if (getRMListDs.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in getRMListDs.Tables[0].Rows)
                    {
                        rmVo = new RMVo();
                        rmVo.UserId = int.Parse(dr["U_UserId"].ToString());
                        rmVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                        rmVo.FirstName = dr["AR_FirstName"].ToString();
                        rmVo.MiddleName = dr["AR_MiddleName"].ToString();
                        rmVo.LastName = dr["AR_LastName"].ToString();
                        rmVo.StaffCode = dr["AR_StaffCode"].ToString();
                        rmVo.Mobile = Convert.ToInt64(dr["AR_Mobile"].ToString());
                        rmVo.Email = dr["AR_Email"].ToString();
                        rmVo.IsExternal = Int16.Parse(dr["AR_IsExternalStaff"].ToString());
                        rmVo.BranchList = dr["BranchList"].ToString();
                        rmVo.RMRole = dr["AR_JobFunction"].ToString();
                        rmList.Add(rmVo);
                    }
                }
                else
                    rmList = null;
               
            }
            catch (Exception e)
            {
                string msg = e.Message.ToString();
            }
            return rmList;
        }

        /* End */


        /* BM Customer List */

        public List<CustomerVo> GetAllBMCustomerList(int branchId, int branchHeadId, int all, int rmId, int currentPage, out int count, string sortExpression, string panFilter, string nameFilter, string areaFilter, string pincodeFilter, string parentFilter, string cityFilter, string RMFilter, string IsProspect, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictCity, out Dictionary<string, string> genDictRM)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            genDictParent = new Dictionary<string, string>();
            genDictCity = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();

            count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetALLCustomersForBM");
                db.AddInParameter(getCustomerListCmd, "@AB_BranchId", DbType.Int32, branchId);
                db.AddInParameter(getCustomerListCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getCustomerListCmd, "@all", DbType.Int32, all);

                db.AddInParameter(getCustomerListCmd, "@CurrentPage", DbType.Int32, currentPage);
                if (!string.IsNullOrEmpty(sortExpression.Trim()))
                    db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, sortExpression);
                if (nameFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, nameFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (areaFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, areaFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, DBNull.Value);
                if (pincodeFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@pincodeFilter", DbType.String, pincodeFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@pincodeFilter", DbType.String, DBNull.Value);

                if (parentFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@parentFilter", DbType.String, parentFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@parentFilter", DbType.String, DBNull.Value);
                if (cityFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@cityFilter", DbType.String, cityFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@cityFilter", DbType.String, DBNull.Value);
                if (RMFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, RMFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, DBNull.Value);

                if (IsProspect != "")
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, IsProspect);
                else
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, "2");

                if (panFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, panFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, DBNull.Value);

                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();
                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["RMName"] != "")
                            customerVo.AssignedRM = dr["RMName"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());

                        if (dr["C_IsProspect"].ToString() != "")
                            customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;

                if (getCustomerDs.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[2].Rows)
                    {
                        genDictParent.Add(dr["CustomerId"].ToString(), dr["Parent"].ToString());
                    }
                }

                if (getCustomerDs.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[3].Rows)
                    {
                        genDictCity.Add(dr["C_Adr1City"].ToString(), dr["C_Adr1City"].ToString());
                    }
                }

                if (getCustomerDs.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[4].Rows)
                    {
                        if (dr["RMName"].ToString().Trim() != "")
                        {
                            genDictRM.Add(dr["RMId"].ToString(), dr["RMName"].ToString());
                        }
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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetBMCustomerList()");

                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (getCustomerDs.Tables[1].Rows.Count > 0)
                count = Int32.Parse(getCustomerDs.Tables[1].Rows[0]["CNT"].ToString());

            return customerList;
        }

        /*  End For BM Customers */

        /* BM Branch Dropdown */

        public DataTable GetRMListForBranchDP(int branchId, int branchHeadId, int all)
        {
            Database db;
            DbCommand getBranchRMListCmd;
            DataSet getBranchRMListDs;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchRMListCmd = db.GetStoredProcCommand("SP_GetAllBMRMList");
                db.AddInParameter(getBranchRMListCmd, "@AB_BranchId", DbType.Int32, branchId);
                db.AddInParameter(getBranchRMListCmd, "@AB_BranchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getBranchRMListCmd, "@all", DbType.Int32, all);
                getBranchRMListDs = db.ExecuteDataSet(getBranchRMListCmd);
                dt = getBranchRMListDs.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetRMListForBranchDP()");
                object[] objects = new object[2];
                objects[0] = branchId;
                objects[1] = branchHeadId;
                objects[2] = all;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dt;
        }

        /* ****************** */



        /// <summary>
        /// TO GET ALL THE STAFFS WHO IS HAVING ONLY ADMIN AND RM ROLES UNDER THE PERTICULAR ADVISER
        /// </summary>
        /// Created by Vinayak Patil
        /// <param name="adviserId"></param>
        /// <returns></returns>

        public DataSet GetAllAdviserRMsHavingOnlyAdminRMRole(int adviserId, int rmId)
        {
            Database db;
            DbCommand getAdviserRMListCmd;
            DataSet getAdviserRMListDs;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserRMListCmd = db.GetStoredProcCommand("SP_GetStaffsHavingOnlyRMRole");
                if (adviserId != 0)
                    db.AddInParameter(getAdviserRMListCmd, "@A_AdviserId", DbType.Int32, adviserId);
                else
                    db.AddInParameter(getAdviserRMListCmd, "@A_AdviserId", DbType.Int32, DBNull.Value);

                if (rmId != 0)
                    db.AddInParameter(getAdviserRMListCmd, "@AR_RMId", DbType.Int32, rmId);
                else
                    db.AddInParameter(getAdviserRMListCmd, "@AR_RMId", DbType.Int32, DBNull.Value);

                getAdviserRMListDs = db.ExecuteDataSet(getAdviserRMListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdviserRM()");
                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return getAdviserRMListDs;
        }
        /// <summary>
        ///  To check Ops Planwise ops enable or not
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetPlanOpsStaffAddStatus(int adviserId)
        {
            Database db;
            DbCommand PlanOpsStaffAddStatusCmd;
            DataSet dsPlanOpsStaffAddStatus;
            DataTable dt = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                PlanOpsStaffAddStatusCmd = db.GetStoredProcCommand("SPROC_GetPlanOpsStaffAddStatus");
                if (adviserId != 0)
                    db.AddInParameter(PlanOpsStaffAddStatusCmd, "@AdviserId", DbType.Int32, adviserId);
                else
                    db.AddInParameter(PlanOpsStaffAddStatusCmd, "@AdviserId", DbType.Int32, DBNull.Value);
                dsPlanOpsStaffAddStatus = db.ExecuteDataSet(PlanOpsStaffAddStatusCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetPlanOpsStaffAddStatus()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPlanOpsStaffAddStatus;
        }
        public DataSet BindStaffGridWithTeamChanelDetails(int id, bool isAdviser, bool isBranchHead, bool isBranchId, string currentUserRole,int AdviserId)
        {
            DataSet dsViewStaff;
            Database db;
            DbCommand ViewStaffCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                ViewStaffCmd = db.GetStoredProcCommand("SPROC_BindStaffGridWithTeamChanelDetails");
                db.AddInParameter(ViewStaffCmd, "@id", DbType.Int32, Convert.ToInt32(id));
                db.AddInParameter(ViewStaffCmd, "@AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(ViewStaffCmd, "@isAdviser", DbType.Int16, Convert.ToInt16(isAdviser));
                db.AddInParameter(ViewStaffCmd, "@isBranchHead", DbType.Int16, Convert.ToInt16(isBranchHead));
                db.AddInParameter(ViewStaffCmd, "@isBranchId", DbType.Int16, Convert.ToInt16(isBranchId));
                db.AddInParameter(ViewStaffCmd, "@currentUserRole", DbType.String, currentUserRole);
                dsViewStaff = db.ExecuteDataSet(ViewStaffCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetViewAssociates()");
                object[] objects = new object[1];
                objects[0] = id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsViewStaff;
        }

        public DataTable GetAdviserTeamList()
        {
            Database db;
            DbCommand adviserTeamListCmd;
            DataSet dsAdviserTeamList;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                adviserTeamListCmd = db.GetStoredProcCommand("SPROC_GetAdviserTeamList");
                dsAdviserTeamList = db.ExecuteDataSet(adviserTeamListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdviserTeamList()");
                object[] objects = new object[1];              
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTeamList.Tables[0];
 
        }

        public DataTable GetAdviserTeamTitleList(int teamId, int adviserId)
        {
            Database db;
            DbCommand adviserTeamTitleListCmd;
            DataSet dsAdviserTeamTitleList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                adviserTeamTitleListCmd = db.GetStoredProcCommand("SPROC_GetAdviserTeamTitleList");
                db.AddInParameter(adviserTeamTitleListCmd, "@TeamId", DbType.Int32, teamId);
                db.AddInParameter(adviserTeamTitleListCmd, "@AdviserId", DbType.Int32, adviserId);
                dsAdviserTeamTitleList = db.ExecuteDataSet(adviserTeamTitleListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdviserTeamTitleList()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTeamTitleList.Tables[0];

        }

        public DataSet GetAdviserTitleReportingLevel(int titleId, int adviserId)
        {
            Database db;
            DbCommand adviserTitleReportingLevelCmd;
            DataSet dsAdviserTitleReportingLevel;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                adviserTitleReportingLevelCmd = db.GetStoredProcCommand("SPROC_GetAdviserTitleReportingLevel");
                db.AddInParameter(adviserTitleReportingLevelCmd, "@TitleId", DbType.Int32, titleId);
                db.AddInParameter(adviserTitleReportingLevelCmd, "@AdviserId", DbType.Int32, adviserId);
                dsAdviserTitleReportingLevel = db.ExecuteDataSet(adviserTitleReportingLevelCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdviserTitleReportingLevel()");
                object[] objects = new object[1];
                objects[0] = titleId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTitleReportingLevel;

        }

        public DataSet GetAdviserReportingManagerList(int roleId, int adviserId)
        {
            Database db;
            DbCommand reportingManagerListCmd;
            DataSet dsReportingManagerList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                reportingManagerListCmd = db.GetStoredProcCommand("SPROC_GetReportingManagerList");
                db.AddInParameter(reportingManagerListCmd, "@RoleId", DbType.Int32, roleId);
                db.AddInParameter(reportingManagerListCmd, "@AdviserId", DbType.Int32, adviserId);
                dsReportingManagerList = db.ExecuteDataSet(reportingManagerListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetAdviserReportingManagerList()");
                object[] objects = new object[1];
                objects[0] = roleId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsReportingManagerList;

        }

        public DataSet GetAdviserBranchList(int adviserId, string userType)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetAdviserBranchList");
                db.AddInParameter(cmd, "@Id", DbType.Int32, adviserId);
                db.AddInParameter(cmd, "@userType", DbType.String, userType);

                ds = db.ExecuteDataSet(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetAdviserBranchList()");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

       


    }
}
