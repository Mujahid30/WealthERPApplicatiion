using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using VoAdvisorProfiling;
using VoUser;
using BoUser;

namespace DaoAdvisorProfiling
{
    public class AdvisorBranchDao
    {
        public int CreateAdvisorBranch(AdvisorBranchVo advisorBranchVo, int advisorId, int userId)
        {

            int branchId = 0; ;
            Database db;
            DbCommand createAdvisorBranchCmd;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdvisorBranchCmd = db.GetStoredProcCommand("SP_CreateAdviserBranch");
                db.AddInParameter(createAdvisorBranchCmd, "@A_AdviserId", DbType.Int32, advisorId);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_AddressLine1", DbType.String, advisorBranchVo.AddressLine1);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_AddressLine2", DbType.String, advisorBranchVo.AddressLine2);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_AddressLine3", DbType.String, advisorBranchVo.AddressLine3);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_BranchCode", DbType.String, advisorBranchVo.BranchCode);
                //if (advisorBranchVo.BranchHeadId == 100)
                //{
                //    db.AddInParameter(createAdvisorBranchCmd, "@AB_BranchHeadId", DbType.Int32, DBNull.Value);
                //}
                //else
                //{
                    db.AddInParameter(createAdvisorBranchCmd, "@AB_BranchHeadId", DbType.Int32, advisorBranchVo.BranchHeadId);
                //}
                //db.AddInParameter(createAdvisorBranchCmd, "@AB_BranchHeadMobile", DbType.Double, advisorBranchVo.MobileNumber);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_BranchName", DbType.String, advisorBranchVo.BranchName);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_City", DbType.String, advisorBranchVo.City);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Country", DbType.String, advisorBranchVo.Country);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Email", DbType.String, advisorBranchVo.Email);

                db.AddInParameter(createAdvisorBranchCmd, "@XABRT_BranchTypeCode", DbType.Int32, advisorBranchVo.BranchTypeCode);
                if (advisorBranchVo.AssociateCategoryId != 0)
                    db.AddInParameter(createAdvisorBranchCmd, "@AAC_AssociateCategoryId", DbType.Int32, advisorBranchVo.AssociateCategoryId);
                else
                    db.AddInParameter(createAdvisorBranchCmd, "@AAC_AssociateCategoryId", DbType.Int32, DBNull.Value);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_BranchLogo", DbType.String, advisorBranchVo.LogoPath);

                db.AddInParameter(createAdvisorBranchCmd, "@AB_Fax", DbType.Int32, advisorBranchVo.Fax);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_FaxISD", DbType.Int32, advisorBranchVo.FaxIsd);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_FaxSTD", DbType.Int32, advisorBranchVo.FaxStd);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Phone1ISD", DbType.Int32, advisorBranchVo.Phone1Isd);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Phone2ISD", DbType.Int32, advisorBranchVo.Phone2Isd);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Phone1STD", DbType.Int32, advisorBranchVo.Phone1Std);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Phone2STD", DbType.Int32, advisorBranchVo.Phone2Std);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Phone1", DbType.Int32, advisorBranchVo.Phone1Number);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_Phone2", DbType.Int32, advisorBranchVo.Phone2Number);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_PinCode", DbType.Int32, advisorBranchVo.PinCode);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_State", DbType.String, advisorBranchVo.State);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createAdvisorBranchCmd, "@AB_ModifiedBy", DbType.Int32, userId);
                if (advisorBranchVo.IsHeadBranch != null)
                    db.AddInParameter(createAdvisorBranchCmd, "@AB_IsHeadBranch", DbType.Int32, advisorBranchVo.IsHeadBranch);
                else
                    db.AddInParameter(createAdvisorBranchCmd, "@AB_IsHeadBranch", DbType.Int32, DBNull.Value);
                db.AddOutParameter(createAdvisorBranchCmd, "BranchId", DbType.Int32, 5000);
                if (db.ExecuteNonQuery(createAdvisorBranchCmd) != 0)
                    branchId = int.Parse(db.GetParameterValue(createAdvisorBranchCmd, "BranchId").ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CreateAdviserBranch()");
                object[] objects = new object[3];
                objects[0] = advisorBranchVo;
                objects[1] = advisorId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return branchId;

        }

        public bool DeleteBranch(int branchId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteBranchCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteBranchCmd = db.GetStoredProcCommand("SP_DeleteBranchDetails");
                db.AddInParameter(deleteBranchCmd, "@AB_BranchId", DbType.Int16, branchId);
                if (db.ExecuteNonQuery(deleteBranchCmd) != 0)
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:DeleteBranch()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public DataSet GetRMBranchAssociation(int rmId, int adviserId, string Flag)
        {
            DataSet ds = null;
            Database db;
            DbCommand getBranchAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchAssociationCmd = db.GetStoredProcCommand("SP_GetRMBranchAssociation");
                db.AddInParameter(getBranchAssociationCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(getBranchAssociationCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(getBranchAssociationCmd, "@Flag", DbType.String, Flag);

                if (db.ExecuteDataSet(getBranchAssociationCmd).Tables[0].Rows.Count > 0)
                {

                    ds = db.ExecuteDataSet(getBranchAssociationCmd);
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetRMBranchAssociation()");
                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = adviserId;
                objects[2] = Flag;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        
        public DataSet GetBranchAssociation(int advisorId, int currentPage, out int Count, string BranchFilter, string RMFilter, string SortExpression, out Dictionary<string, string> genDictBranch, out Dictionary<string, string> genDictRM)
        {
            DataSet ds = null;
            Database db;
            DbCommand getBranchAssociationCmd;

            genDictRM = new Dictionary<string, string>();
            genDictBranch = new Dictionary<string, string>();
            Count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchAssociationCmd = db.GetStoredProcCommand("SP_GetBranchAssociation");
                db.AddInParameter(getBranchAssociationCmd, "@AdvisorId", DbType.Int32, advisorId);
                db.AddInParameter(getBranchAssociationCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getBranchAssociationCmd, "@SortOrder", DbType.String, SortExpression);

                if (BranchFilter != "")
                    db.AddInParameter(getBranchAssociationCmd, "@branchFilter", DbType.String, BranchFilter);
                else
                    db.AddInParameter(getBranchAssociationCmd, "@branchFilter", DbType.String, DBNull.Value);
                if (RMFilter != "")
                    db.AddInParameter(getBranchAssociationCmd, "@rmFilter", DbType.String, RMFilter);
                else
                    db.AddInParameter(getBranchAssociationCmd, "@rmFilter", DbType.String, DBNull.Value);

                if (db.ExecuteDataSet(getBranchAssociationCmd).Tables[0].Rows.Count > 0)
                    ds = db.ExecuteDataSet(getBranchAssociationCmd);

                if (ds.Tables[1].Rows.Count > 0)
                    Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());

                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        genDictBranch.Add(dr["branchId"].ToString(), dr["BranchName"].ToString());
                    }
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranchAssociation()");
                object[] objects = new object[5];
                objects[0] = advisorId;
                objects[1] = currentPage;
                objects[2] = RMFilter;
                objects[3] = BranchFilter;
                objects[4] = SortExpression;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public bool AddBranchTerminal(int branchId, float terminalId, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand addBranchTerminalCmd;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                addBranchTerminalCmd = db.GetStoredProcCommand("SP_CreateBranchTerminal");
                db.AddInParameter(addBranchTerminalCmd, "@AT_TerminalId", DbType.Double, terminalId);
                db.AddInParameter(addBranchTerminalCmd, "@AB_BranchId", DbType.Int32, branchId);
                db.AddInParameter(addBranchTerminalCmd, "@AT_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(addBranchTerminalCmd, "@AT_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(addBranchTerminalCmd) != 0)
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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:AddBranchTerminal()");


                object[] objects = new object[3];
                objects[0] = branchId;
                objects[1] = terminalId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public int GetBranchId(int rmId)
        {
            int branchId;
            Database db;
            DbCommand getBranchIdCmd;
            DataSet getBranchIdDs;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchIdCmd = db.GetSqlStringCommand("select AB_BranchId from AdviserRMBranch where AR_RMId=" + rmId.ToString());
                getBranchIdDs = db.ExecuteDataSet(getBranchIdCmd);
                if (getBranchIdDs.Tables[0].Rows.Count == 0)
                {
                    branchId = 0;
                }
                else
                {
                    dr = getBranchIdDs.Tables[0].Rows[0];
                    branchId = int.Parse(dr["AB_BranchId"].ToString());
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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranchId()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return branchId;

        }

        public bool CheckRMBranchAssociation(int rmId, int branchId)
        {
            bool bResult = false;
            Database db;
            DbCommand chkRMBranchAssociationCmd;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkRMBranchAssociationCmd = db.GetStoredProcCommand("SP_CheckRMBranchAssociation");
                db.AddInParameter(chkRMBranchAssociationCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(chkRMBranchAssociationCmd, "@AB_BranchId", DbType.Int32, branchId);
                count = int.Parse(db.ExecuteScalar(chkRMBranchAssociationCmd).ToString());
                if (count == 0)
                {
                    bResult = true;
                }
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
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckRMBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        
        public bool CheckBranchMgrRole(int rmId)
        {
            bool bResult = false;
            Database db;
            DbCommand checkBranchMgrRoleCmd;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkBranchMgrRoleCmd = db.GetStoredProcCommand("SP_CheckBranchManagerRole");
                db.AddInParameter(checkBranchMgrRoleCmd, "@AR_RMId", DbType.Int32, rmId);

                count = int.Parse(db.ExecuteScalar(checkBranchMgrRoleCmd).ToString());
                if (count == 0)
                {
                    bResult = true;
                }
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
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckBranchMgrRole()");
                object[] objects = new object[2];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool AssociateBranch(int rmId, int branchId, int IsMainBranch, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand associateBranchCmd;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                associateBranchCmd = db.GetStoredProcCommand("SP_CreateRMBranch");

                if (advisorBranchDao.CheckRMBranchAssociation(rmId, branchId))
                {
                    db.AddInParameter(associateBranchCmd, "@AR_RMId", DbType.Int32, rmId);
                    db.AddInParameter(associateBranchCmd, "@AB_BranchId", DbType.Int32, branchId);
                    db.AddInParameter(associateBranchCmd, "@ARB_IsMainBranch", DbType.Int32, IsMainBranch);
                    db.AddInParameter(associateBranchCmd, "@ARB_CreatedBy", DbType.Int32, userId);
                    db.AddInParameter(associateBranchCmd, "@ARB_ModifiedBy", DbType.Int32, userId);
                    if (db.ExecuteNonQuery(associateBranchCmd) != 0)
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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:AssociateBranch()");


                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = branchId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool UpdateAssociateBranch(int rmId, int branchId, int IsMainBranch, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand associateBranchCmd;
            AdvisorBranchDao advisorBranchDao = new AdvisorBranchDao();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                associateBranchCmd = db.GetStoredProcCommand("SP_UpdateRMBranch");


                db.AddInParameter(associateBranchCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(associateBranchCmd, "@AB_BranchId", DbType.Int32, branchId);
                db.AddInParameter(associateBranchCmd, "@ARB_IsMainBranch", DbType.Int32, IsMainBranch);
                db.AddInParameter(associateBranchCmd, "@ARB_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(associateBranchCmd) != 0)
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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:AssociateBranch()");


                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = branchId;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public DataSet GetBranchTerminals(int branchId)
        {
            Database db;
            DbCommand getBranchTerminalsCmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchTerminalsCmd = db.GetStoredProcCommand("SP_GetBranchTerminals");
                db.AddInParameter(getBranchTerminalsCmd, "@AB_BranchId", DbType.Int32, branchId);
                if (db.ExecuteDataSet(getBranchTerminalsCmd).Tables[0].Rows.Count > 0)
                    ds = db.ExecuteDataSet(getBranchTerminalsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranchTerminals()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;

        }

        public bool UpdateRMBranchAssociation(int rmId, int branchId,int userid,Int16 IsMainBranch)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAssociationCmd = db.GetStoredProcCommand("SP_UpdateRMBranchAssociation");
                db.AddInParameter(deleteAssociationCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(deleteAssociationCmd, "@AB_BranchId", DbType.Int32, branchId);
                db.AddInParameter(deleteAssociationCmd, "@ARB_CreatedBy", DbType.Int32, userid);
                db.AddInParameter(deleteAssociationCmd, "@ARB_ModifiedBy", DbType.Int32, userid);
                db.AddInParameter(deleteAssociationCmd, "@ARB_IsMainBranch", DbType.Int32, IsMainBranch);
                if (db.ExecuteNonQuery(deleteAssociationCmd) != 0)
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:DeleteRMBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool DeleteRMBranchAssociation(int rmId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAssociationCmd = db.GetStoredProcCommand("SP_DeleteRMBranchAssociation1");
                db.AddInParameter(deleteAssociationCmd, "@AR_RMId", DbType.Int32, rmId);
                if (db.ExecuteNonQuery(deleteAssociationCmd) != 0)
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:DeleteRMBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;
               
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public List<int> FindBranch(string branchName, int advisorId, int CurrentPage, string sortOrder, out int count)
        {
            List<int> branchList = new List<int>();
            Database db;
            DbCommand findBranchCmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                findBranchCmd = db.GetStoredProcCommand("SP_FindBranch");
                db.AddInParameter(findBranchCmd, "@AB_BranchName", DbType.String, branchName);
                db.AddInParameter(findBranchCmd, "@A_AdviserId", DbType.Int16, advisorId);
                db.AddInParameter(findBranchCmd, "@CurrentPage", DbType.Int16, CurrentPage);
                db.AddInParameter(findBranchCmd, "@SortOrder", DbType.String, sortOrder);
                ds = db.ExecuteDataSet(findBranchCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int branchId = int.Parse(dr["AB_BranchId"].ToString());
                        branchList.Add(branchId);
                    }
                }
                else
                {
                    branchList = null;
                }
                count = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:FindBranch()");
                object[] objects = new object[2];
                objects[0] = branchName;
                objects[1] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return branchList;
        }

        public List<AdvisorBranchVo> GetAdvisorBranches(int advisorId, string sortOrder, int CurrentPage, out int Count)
        {
            List<AdvisorBranchVo> branchList = new List<AdvisorBranchVo>();
            AdvisorBranchVo advisorBranchVo;
            Database db;
            DbCommand getAdvisorBranchCmd;
            DataSet getAdvisorBranchDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                getAdvisorBranchCmd = db.GetStoredProcCommand("SP_GetAdviserBranches");
                db.AddInParameter(getAdvisorBranchCmd, "@A_AdviserId", DbType.Int32, advisorId);
                if (sortOrder != String.Empty)
                    db.AddInParameter(getAdvisorBranchCmd, "@SortOrder", DbType.String, sortOrder);
                db.AddInParameter(getAdvisorBranchCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                getAdvisorBranchDs = db.ExecuteDataSet(getAdvisorBranchCmd);

                if (getAdvisorBranchDs.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in getAdvisorBranchDs.Tables[0].Rows)
                    {
                        advisorBranchVo = new AdvisorBranchVo();
                        advisorBranchVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                        advisorBranchVo.AddressLine1 = dr["AB_AddressLine1"].ToString();
                        advisorBranchVo.AddressLine2 = dr["AB_AddressLine2"].ToString();
                        advisorBranchVo.AddressLine3 = dr["AB_AddressLine3"].ToString();
                        advisorBranchVo.BranchCode = dr["AB_BranchCode"].ToString();
                        advisorBranchVo.BranchName = dr["AB_BranchName"].ToString();
                        advisorBranchVo.City = dr["AB_City"].ToString();
                        advisorBranchVo.Country = dr["AB_Country"].ToString();
                        advisorBranchVo.Email = dr["AB_Email"].ToString();
                        advisorBranchVo.Fax = int.Parse(dr["AB_Fax"].ToString());
                        advisorBranchVo.FaxStd = int.Parse(dr["AB_FaxSTD"].ToString());
                        advisorBranchVo.FaxIsd = int.Parse(dr["AB_FaxISD"].ToString());

                        advisorBranchVo.Phone1Isd = int.Parse(dr["AB_Phone1ISD"].ToString());
                        advisorBranchVo.Phone2Isd = int.Parse(dr["AB_Phone2ISD"].ToString());
                        advisorBranchVo.Phone1Std = int.Parse(dr["AB_Phone1STD"].ToString());
                        advisorBranchVo.Phone2Std = int.Parse(dr["AB_Phone2STD"].ToString());
                        advisorBranchVo.Phone1Number = int.Parse(dr["AB_Phone1"].ToString());
                        advisorBranchVo.Phone2Number = int.Parse(dr["AB_Phone2"].ToString());
                        advisorBranchVo.PinCode = int.Parse(dr["AB_PinCode"].ToString());
                        advisorBranchVo.State = dr["AB_State"].ToString();
                        advisorBranchVo.BranchType = dr["XABRT_BranchType"].ToString();
                        advisorBranchVo.BranchHead = dr["BranchHead"].ToString();

                        branchList.Add(advisorBranchVo);
                    }

                }
                else
                    branchList = null;
                Count = Int32.Parse(getAdvisorBranchDs.Tables[1].Rows[0][0].ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetAdvisorBranches()");


                object[] objects = new object[3];
                objects[0] = advisorId;
                objects[1] = sortOrder;
                objects[2] = CurrentPage;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return branchList;
        }

        public List<AdvisorBranchVo> GetAdvisorBranches(int advisorId, string IsExternal)
        {
            List<AdvisorBranchVo> branchList = new List<AdvisorBranchVo>();
            AdvisorBranchVo advisorBranchVo;
            Database db;
            DbCommand getAdvisorBranchCmd;
            DataSet getAdvisorBranchDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                getAdvisorBranchCmd = db.GetStoredProcCommand("SP_GetAdviserBranches");
                db.AddInParameter(getAdvisorBranchCmd, "@A_AdviserId", DbType.Int32, advisorId);
                if (IsExternal != "")
                    db.AddInParameter(getAdvisorBranchCmd, "@IsExternal", DbType.String, IsExternal);
                else
                    db.AddInParameter(getAdvisorBranchCmd, "@IsExternal", DbType.String, DBNull.Value);
                getAdvisorBranchDs = db.ExecuteDataSet(getAdvisorBranchCmd);
                if (getAdvisorBranchDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getAdvisorBranchDs.Tables[0].Rows)
                    {
                        advisorBranchVo = new AdvisorBranchVo();
                        advisorBranchVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                        advisorBranchVo.AddressLine1 = dr["AB_AddressLine1"].ToString();
                        advisorBranchVo.AddressLine2 = dr["AB_AddressLine2"].ToString();
                        advisorBranchVo.AddressLine3 = dr["AB_AddressLine3"].ToString();
                        advisorBranchVo.BranchCode = dr["AB_BranchCode"].ToString();
                        advisorBranchVo.BranchName = dr["AB_BranchName"].ToString();
                        advisorBranchVo.City = dr["AB_City"].ToString();
                        advisorBranchVo.Country = dr["AB_Country"].ToString();
                        advisorBranchVo.Email = dr["AB_Email"].ToString();
                        if (!string.IsNullOrEmpty(dr["AB_Fax"].ToString().Trim()))
                        advisorBranchVo.Fax = int.Parse(dr["AB_Fax"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_FaxSTD"].ToString().Trim()))
                        advisorBranchVo.FaxStd = int.Parse(dr["AB_FaxSTD"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_FaxISD"].ToString().Trim()))
                        advisorBranchVo.FaxIsd = int.Parse(dr["AB_FaxISD"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_Phone1ISD"].ToString().Trim()))
                        advisorBranchVo.Phone1Isd = int.Parse(dr["AB_Phone1ISD"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_Phone2ISD"].ToString().Trim()))
                        advisorBranchVo.Phone2Isd = int.Parse(dr["AB_Phone2ISD"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_Phone1STD"].ToString().Trim()))
                        advisorBranchVo.Phone1Std = int.Parse(dr["AB_Phone1STD"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_Phone2STD"].ToString().Trim()))
                        advisorBranchVo.Phone2Std = int.Parse(dr["AB_Phone2STD"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_Phone1"].ToString().Trim()))
                        advisorBranchVo.Phone1Number = int.Parse(dr["AB_Phone1"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_Phone2"].ToString().Trim()))
                        advisorBranchVo.Phone2Number = int.Parse(dr["AB_Phone2"].ToString());
                        if (!string.IsNullOrEmpty(dr["AB_PinCode"].ToString().Trim()))
                        advisorBranchVo.PinCode = int.Parse(dr["AB_PinCode"].ToString());
                        advisorBranchVo.State = dr["AB_State"].ToString();
                        branchList.Add(advisorBranchVo);
                    }
                }
                else
                    branchList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetAdvisorBranches()");


                object[] objects = new object[3];
                objects[0] = advisorId;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return branchList;
        }

        public bool ChkBranchManagerAvail(int branchId)
        {
            Database db;
            DbCommand chkBranchManagerAvailCmd;
            DataSet chkBranchMangerAvailDs;
            string query;
            bool res = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                query = "SELECT UR_RoleName FROM UserRole WHERE UR_RoleId IN (SELECT UR_RoleId FROM RoleAssociation WHERE U_UserId IN ( SELECT U_UserId FROM dbo.AdviserRM WHERE AR_RMId IN(SELECT AR_RMId FROM dbo.AdviserRMBranch WHERE AB_BranchId=" + branchId.ToString() + ")))";

                chkBranchManagerAvailCmd = db.GetSqlStringCommand(query);
                chkBranchMangerAvailDs = db.ExecuteDataSet(chkBranchManagerAvailCmd);
                if (chkBranchMangerAvailDs.Tables[0].Rows.Count == 0)
                {
                    res = true;

                }
                else
                {
                    foreach (DataRow dr in chkBranchMangerAvailDs.Tables[0].Rows)
                    {
                        if (dr["UR_RoleName"].ToString() == "Advisor")
                        {
                            res = true;
                            continue;
                        }
                        if (dr["UR_RoleName"].ToString() == "BM")
                        {
                            res = false;
                            break;
                        }

                        res = true;
                    }
                }

            }
            catch (BaseApplicationException e)
            {
                throw e;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:ChkBranchManagerAvail()");
                object[] objects = new object[0];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return res;
        }

        public ArrayList GetBranchName(int id)
        {
            Database db = DatabaseFactory.CreateDatabase("wealtherp");
            ArrayList branchList = new ArrayList();
            DbCommand getBranchCmd;
            DataSet getBranchDs;
            try
            {
                string query = "select AB_BranchName from AdviserBranch where A_AdviserId=" + id.ToString();
                getBranchCmd = db.GetSqlStringCommand(query);
                getBranchDs = db.ExecuteDataSet(getBranchCmd);
                if (getBranchDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getBranchDs.Tables[0].Rows)
                    {
                        branchList.Add(dr["AB_BranchName"].ToString());
                    }
                }
                else
                    branchList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranchName()");
                object[] objects = new object[1];
                objects[0] = id;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return branchList;
        }

        public DataTable GetAsscCommissionDetails(int adviserId, int asscCategoryId)
        {
            Database db = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand cmdAsscCommDetails;
            DataTable dtAsscCommDetails;
            DataSet dsAsscCommDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdAsscCommDetails = db.GetStoredProcCommand("SP_AdviserAssociateDefaultCommissionStructureGetDetails");
                db.AddInParameter(cmdAsscCommDetails, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdAsscCommDetails, "@AAC_AssociateCategoryID", DbType.Int32, asscCategoryId);
                dsAsscCommDetails = db.ExecuteDataSet(cmdAsscCommDetails);
                dtAsscCommDetails = dsAsscCommDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranchName()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[0] = asscCategoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAsscCommDetails;
        }

        public bool UpdateAdvisorBranch(AdvisorBranchVo advisorBranchVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateAdvisorBranchCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdvisorBranchCmd = db.GetStoredProcCommand("SP_UpdateAdviserBranch");
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_BranchId", DbType.Int32, advisorBranchVo.BranchId);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_AddressLine1", DbType.String, advisorBranchVo.AddressLine1);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_AddressLine2", DbType.String, advisorBranchVo.AddressLine2);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_AddressLine3", DbType.String, advisorBranchVo.AddressLine3);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_BranchCode", DbType.String, advisorBranchVo.BranchCode);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_BranchHeadMobile", DbType.Decimal, advisorBranchVo.MobileNumber);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_BranchHeadId", DbType.String, advisorBranchVo.BranchHeadId);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_BranchName", DbType.String, advisorBranchVo.BranchName);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_City", DbType.String, advisorBranchVo.City);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Country", DbType.String, advisorBranchVo.Country);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Email", DbType.String, advisorBranchVo.Email);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Fax", DbType.Decimal, advisorBranchVo.Fax);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_FaxISD", DbType.Decimal, advisorBranchVo.FaxIsd);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_FaxSTD", DbType.Decimal, advisorBranchVo.FaxStd);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Phone1ISD", DbType.Decimal, advisorBranchVo.Phone1Isd);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Phone2ISD", DbType.Decimal, advisorBranchVo.Phone2Isd);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Phone1STD", DbType.Decimal, advisorBranchVo.Phone1Std);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Phone2STD", DbType.Decimal, advisorBranchVo.Phone2Std);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Phone1", DbType.Decimal, advisorBranchVo.Phone1Number);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_Phone2", DbType.Decimal, advisorBranchVo.Phone2Number);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_PinCode", DbType.Decimal, advisorBranchVo.PinCode);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_State", DbType.String, advisorBranchVo.State);
                db.AddInParameter(updateAdvisorBranchCmd, "@AB_BranchLogo", DbType.String, advisorBranchVo.LogoPath);
                db.AddInParameter(updateAdvisorBranchCmd, "@XABRT_BranchTypeCode", DbType.Int32, advisorBranchVo.BranchTypeCode);
                if (advisorBranchVo.AssociateCategoryId != 0)
                    db.AddInParameter(updateAdvisorBranchCmd, "@AAC_AssociateCategoryId", DbType.Int32, advisorBranchVo.AssociateCategoryId);
                else
                    db.AddInParameter(updateAdvisorBranchCmd, "@AAC_AssociateCategoryId", DbType.Int32, DBNull.Value);
                


                if (db.ExecuteNonQuery(updateAdvisorBranchCmd) != 0)

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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:UpdateAdvisorBranch()");


                object[] objects = new object[1];
                objects[0] = advisorBranchVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public AdvisorBranchVo GetBranch(int branchId)
        {
            AdvisorBranchVo advisorBranchVo = new AdvisorBranchVo();
            Database db;
            DbCommand getBranchCmd;
            DataSet getBranchDs;
            DataTable table;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchCmd = db.GetStoredProcCommand("SP_GetBranch");
                db.AddInParameter(getBranchCmd, "AB_BranchId", DbType.Int32, branchId);

                getBranchDs = db.ExecuteDataSet(getBranchCmd);
                if (getBranchDs.Tables[0].Rows.Count > 0)
                {
                    table = getBranchDs.Tables["AdviserBranch"];
                    dr = getBranchDs.Tables[0].Rows[0];
                    advisorBranchVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    if (dr["AB_BranchHeadId"].ToString() != "")
                    {
                        advisorBranchVo.BranchHeadId = int.Parse(dr["AB_BranchHeadId"].ToString());
                    }
                    advisorBranchVo.BranchHead = dr["BranchHead"].ToString();
                    if (dr["AB_BranchHeadMobile"].ToString() != "")
                        advisorBranchVo.MobileNumber = long.Parse(dr["AB_BranchHeadMobile"].ToString());
                    if (dr["AAC_AssociateCategoryId"].ToString() != string.Empty)
                    {
                        advisorBranchVo.AssociateCategory = dr["AAC_AssociateCategoryName"].ToString();
                        advisorBranchVo.AssociateCategoryId = Int32.Parse(dr["AAC_AssociateCategoryId"].ToString());
                    }
                    if (dr["XABRT_BranchTypeCode"].ToString() != string.Empty)
                        advisorBranchVo.BranchTypeCode = Int32.Parse(dr["XABRT_BranchTypeCode"].ToString());
                    advisorBranchVo.BranchType = dr["XABRT_BranchType"].ToString();
                    advisorBranchVo.AdviserId = int.Parse(dr["A_AdviserId"].ToString());
                    advisorBranchVo.AddressLine1 = dr["AB_AddressLine1"].ToString();
                    advisorBranchVo.AddressLine2 = dr["AB_AddressLine2"].ToString();
                    advisorBranchVo.AddressLine3 = dr["AB_AddressLine3"].ToString();
                    advisorBranchVo.BranchCode = dr["AB_BranchCode"].ToString();
                    advisorBranchVo.BranchName = dr["AB_BranchName"].ToString();
                    advisorBranchVo.City = dr["AB_City"].ToString();
                    advisorBranchVo.Country = dr["AB_Country"].ToString();
                    advisorBranchVo.Email = dr["AB_Email"].ToString();
                    advisorBranchVo.Fax = int.Parse(dr["AB_Fax"].ToString());
                    advisorBranchVo.FaxIsd = int.Parse(dr["AB_FaxISD"].ToString());
                    advisorBranchVo.FaxStd = int.Parse(dr["AB_FaxSTD"].ToString());
                    advisorBranchVo.Phone1Isd = int.Parse(dr["AB_Phone1ISD"].ToString());
                    advisorBranchVo.Phone1Number = int.Parse(dr["AB_Phone1"].ToString());
                    advisorBranchVo.Phone1Std = int.Parse(dr["AB_Phone1STD"].ToString());
                    advisorBranchVo.Phone2Isd = int.Parse(dr["AB_Phone2ISD"].ToString());
                    advisorBranchVo.Phone2Number = int.Parse(dr["AB_Phone2"].ToString());
                    advisorBranchVo.Phone2Std = int.Parse(dr["AB_Phone2STD"].ToString());
                    advisorBranchVo.PinCode = int.Parse(dr["AB_PinCode"].ToString());
                    advisorBranchVo.State = dr["AB_State"].ToString();

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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranch()");


                object[] objects = new object[1];
                objects[0] = branchId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorBranchVo;
        }

        public bool DeleteBranchTerminal(int terminalId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteTerminalCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteTerminalCmd = db.GetStoredProcCommand("SP_DeleteBranchTerminal");
                db.AddInParameter(deleteTerminalCmd, "@AT_Id", DbType.Int32, terminalId);
                if (db.ExecuteNonQuery(deleteTerminalCmd) != 0)

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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:DeleteBranchTerminal()");
                object[] objects = new object[1];
                objects[1] = terminalId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool CheckInternalBranchAssociations(int rmId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateAdvisorBranchCmd;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdvisorBranchCmd = db.GetStoredProcCommand("SP_CheckInternalBranchAssociations");
                db.AddInParameter(updateAdvisorBranchCmd, "@AR_RMId", DbType.Int32, rmId);

                ds = db.ExecuteDataSet(updateAdvisorBranchCmd);

                if (ds.Tables[0].Rows[0]["CNT"].ToString() != "0")
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:UpdateAdvisorBranch()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool CheckExternalBranchAssociations(int rmId)
        {
            bool blResult = false;

            Database db;
            DbCommand updateAdvisorBranchCmd;
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdvisorBranchCmd = db.GetStoredProcCommand("SP_CheckExternalBranchAssociations");
                db.AddInParameter(updateAdvisorBranchCmd, "@AR_RMId", DbType.Int32, rmId);

                ds = db.ExecuteDataSet(updateAdvisorBranchCmd);

                if (ds.Tables[0].Rows[0]["CNT"].ToString() != "0")
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:UpdateAdvisorBranch()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public DataTable GetAdviserAssetGroups(int adviserId)
        {
            Database db = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand cmdAssetGroup;
            DataTable dtAssetGroup;
            DataSet dsAssetGroup = null;
            try
            {
                //To retreive data from the table 
                cmdAssetGroup = db.GetStoredProcCommand("SP_GetAdviserAssetGroups");
                db.AddInParameter(cmdAssetGroup, "@A_AdviserId", DbType.Int32, adviserId);
                dsAssetGroup = db.ExecuteDataSet(cmdAssetGroup);
                dtAssetGroup = dsAssetGroup.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetAdviserAssetGroups()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAssetGroup;
        }

        public bool AddAssociateCommission(int userid, AdvisorAssociateCommissionVo advisorAssociateCommissionVo)
        {
            Database db;
            DbCommand addCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                addCmd = db.GetStoredProcCommand("SP_CreateAdviserAssociateCommissionStructure");
                db.AddInParameter(addCmd, "@AB_BranchId", DbType.Int32, advisorAssociateCommissionVo.BranchId);
                db.AddInParameter(addCmd, "@AACS_CommissionFee", DbType.Double, advisorAssociateCommissionVo.CommissionFee);
                db.AddInParameter(addCmd, "@AACS_RevenueUpperlimit", DbType.Double, advisorAssociateCommissionVo.RevenueUpperlimit);
                db.AddInParameter(addCmd, "@AACS_RevenueLowerlimit", DbType.Double, advisorAssociateCommissionVo.RevenueLowerlimit);
                db.AddInParameter(addCmd, "@XALAG_LOBAssetGroupsCode", DbType.String, advisorAssociateCommissionVo.LOBAssetGroupsCode);
                if (advisorAssociateCommissionVo.StartDate != DateTime.MinValue)
                    db.AddInParameter(addCmd, "@AACS_StartDate", DbType.DateTime, advisorAssociateCommissionVo.StartDate);
                else
                    db.AddInParameter(addCmd, "@AACS_StartDate", DbType.DateTime, DBNull.Value);
                if (advisorAssociateCommissionVo.EndDate != DateTime.MinValue)
                    db.AddInParameter(addCmd, "@AACS_EndDate", DbType.DateTime, advisorAssociateCommissionVo.EndDate);
                else
                    db.AddInParameter(addCmd, "@AACS_EndDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(addCmd, "@AACS_CreatedBy", DbType.Int32, userid);
                db.AddInParameter(addCmd, "@AACS_ModifiedBy", DbType.Int32, userid);

                affectedRows = db.ExecuteNonQuery(addCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:AddAssociateCommission()");

                object[] objects = new object[1];
                objects[0] = userid;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            if (affectedRows > 0)
                return true;
            else
                return false;
        }

        public bool UpdateAssociateCommission(int userid, AdvisorAssociateCommissionVo advisorAssociateCommissionVo)
        {
            Database db;
            DbCommand updateCmd;
            int affectedRows = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCmd = db.GetStoredProcCommand("SP_UpdateAdviserAssociateCommissionStructure");
                db.AddInParameter(updateCmd, "@AACS_Id", DbType.Int32, advisorAssociateCommissionVo.Id);
                db.AddInParameter(updateCmd, "@AACS_CommissionFee", DbType.Double, advisorAssociateCommissionVo.CommissionFee);
                db.AddInParameter(updateCmd, "@AACS_RevenueUpperlimit", DbType.Double, advisorAssociateCommissionVo.RevenueUpperlimit);
                db.AddInParameter(updateCmd, "@AACS_RevenueLowerlimit", DbType.Double, advisorAssociateCommissionVo.RevenueLowerlimit);
                db.AddInParameter(updateCmd, "@XALAG_LOBAssetGroupsCode", DbType.String, advisorAssociateCommissionVo.LOBAssetGroupsCode);
                if (advisorAssociateCommissionVo.StartDate != DateTime.MinValue)
                    db.AddInParameter(updateCmd, "@AACS_StartDate", DbType.DateTime, advisorAssociateCommissionVo.StartDate);
                else
                    db.AddInParameter(updateCmd, "@AACS_StartDate", DbType.DateTime, DBNull.Value);
                if (advisorAssociateCommissionVo.EndDate != DateTime.MinValue)
                    db.AddInParameter(updateCmd, "@AACS_EndDate", DbType.DateTime, advisorAssociateCommissionVo.EndDate);
                else
                    db.AddInParameter(updateCmd, "@AACS_EndDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateCmd, "@userId", DbType.Int32, userid);

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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:UpdateAssociateCommission()");

                object[] objects = new object[1];
                objects[0] = userid;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            if (affectedRows > 0)
                return true;
            else
                return false;
        }

        public DataTable GetBranchAssociateCommission(int branchId)
        {
            Database db = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand cmdAsscCommn;
            DataTable dtAsscCommn;
            DataSet dsAsscCommn = null;
            try
            {
                //To retreive data from the table 
                cmdAsscCommn = db.GetStoredProcCommand("SP_AssociateCommnStrGetDetails");
                db.AddInParameter(cmdAsscCommn, "@AB_BranchId", DbType.Int32, branchId);
                dsAsscCommn = db.ExecuteDataSet(cmdAsscCommn);
                dtAsscCommn = dsAsscCommn.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetAdviserAssetGroups()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAsscCommn;
        }

        /// <summary>
        /// Function to check whether an RM is the branch head (for removing the branch Association) 
        /// </summary>
        /// <param name="rmId">Id of the RM </param>
        /// <param name="branchId">Id of the Branch</param>
        /// <returns></returns>
        public int CheckBranchHead(int rmId, int branchId)
        {
            Database db;
            DbCommand chkBranchHead;
            int count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkBranchHead = db.GetStoredProcCommand("SP_ChkBranchHead");
                db.AddInParameter(chkBranchHead, "@AR_RMId", DbType.Int32, rmId);
                if (branchId!=0)
                db.AddInParameter(chkBranchHead, "@AB_BranchId", DbType.Int32, branchId);
                else
                db.AddInParameter(chkBranchHead, "@AB_BranchId", DbType.Int32, DBNull.Value);

                count = int.Parse(db.ExecuteScalar(chkBranchHead).ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckBranchHead()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }

        /// <summary>
        /// Function to delete the RM-Branch association
       /// </summary>
       /// <param name="rmId"></param>
       /// <param name="branchID"></param>
       /// <returns></returns>
        public bool DeleteBranchAssociation(int rmId,int branchID)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAssociationCmd = db.GetStoredProcCommand("SP_DeleteRMBranchAssociation");
                db.AddInParameter(deleteAssociationCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(deleteAssociationCmd, "@AB_BranchId", DbType.Int32, branchID);
                if (db.ExecuteNonQuery(deleteAssociationCmd) != 0)
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:DeleteBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Function to check whether an Associate Category is linked to any branch(for deleting the Associate Category) 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int CheckAssociateBranchCategory(int categoryId)
        {
            Database db;
            DbCommand chkAssociateBranchCategory;
            int count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkAssociateBranchCategory = db.GetStoredProcCommand("SP_ChkAssociateBranchCategory");
                db.AddInParameter(chkAssociateBranchCategory, "@AAC_AssociateCategoryId", DbType.Int32, categoryId);

                count = int.Parse(db.ExecuteScalar(chkAssociateBranchCategory).ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckAssociateBranchCategory()");
                object[] objects = new object[1];
                objects[0] = categoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }

        /// <summary>
        /// Creating RM Branch Association......
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CreateRMBranchAssociation(int rmId, int branchId, int createdBy, int modifiedBy)
        {
            bool bResult = false;
            Database db;
            DbCommand createAssociationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAssociationCmd = db.GetStoredProcCommand("SP_CreateRMBranchAssociation");
                db.AddInParameter(createAssociationCmd, "@AR_RMId", DbType.Int32, rmId);
                db.AddInParameter(createAssociationCmd, "@AB_BranchId", DbType.Int32, branchId);
                db.AddInParameter(createAssociationCmd, "@ARB_CreatedBy", DbType.Int32, createdBy);
                db.AddInParameter(createAssociationCmd, "@ARB_ModifiedBy", DbType.Int32, modifiedBy);
                if (db.ExecuteNonQuery(createAssociationCmd) != 0)
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CreateRMBranchAssociation()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }


        /// <summary>
        /// Checking RM Branch Association......
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckRMBranchDependency(int rmId, int branchId)
        {
            bool isBranchDependency = false;
            char isDependency = 'N';
            Database db;
            DbCommand checkBranchDependencyCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkBranchDependencyCmd = db.GetStoredProcCommand("SP_BranchDependency");
                db.AddInParameter(checkBranchDependencyCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(checkBranchDependencyCmd, "@BranchId", DbType.Int32, branchId);
                db.AddOutParameter(checkBranchDependencyCmd, "@IsDependency", DbType.String, 1);
                if (db.ExecuteNonQuery(checkBranchDependencyCmd) != 0)
                    isDependency = Convert.ToChar(db.GetParameterValue(checkBranchDependencyCmd, "@IsDependency"));
                if (isDependency == 'Y')
                {
                    isBranchDependency = true;
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckRMBranchDependency()");
                object[] objects = new object[2];
                objects[0] = rmId;
                objects[1] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isBranchDependency;
        }


        /// <summary>
        ///Converting One External Branch to Internal and Vice-versa Checking any RM is associated to the branch
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckBranchDependency(int branchId)
        {
            bool isBranchRMDependency = false;
            int countRM = 0;
            Database db;
            DbCommand checkBranchRMDependencyCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkBranchRMDependencyCmd = db.GetStoredProcCommand("SP_BranchRMDependency");
                db.AddInParameter(checkBranchRMDependencyCmd, "@BranchId", DbType.Int32, branchId);
                db.AddOutParameter(checkBranchRMDependencyCmd, "@Count", DbType.Int32, sizeof(Int32));
                if (db.ExecuteNonQuery(checkBranchRMDependencyCmd) != 0)
                    countRM = int.Parse(db.GetParameterValue(checkBranchRMDependencyCmd, "Count").ToString());
                if (countRM > 0)
                {
                    isBranchRMDependency = true;
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckBranchDependency()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isBranchRMDependency;
        }


        /// <summary>
        ///Before deletion of any branch check the branch depedency with customer
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckBranchCustomerDependency(int branchId)
        {
            bool isBranchCustomerDependency = false;
            int countRM = 0;
            Database db;
            DbCommand checkBranchCustomerDependencyCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkBranchCustomerDependencyCmd = db.GetStoredProcCommand("SP_BranchCustomerDependency");
                db.AddInParameter(checkBranchCustomerDependencyCmd, "@BranchId", DbType.Int32, branchId);
                db.AddOutParameter(checkBranchCustomerDependencyCmd, "@Count", DbType.Int32, sizeof(Int32));
                if (db.ExecuteNonQuery(checkBranchCustomerDependencyCmd) != 0)
                    countRM = int.Parse(db.GetParameterValue(checkBranchCustomerDependencyCmd, "Count").ToString());
                if (countRM > 0)
                {
                    isBranchCustomerDependency = true;
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckBranchCustomerDependency()");
                object[] objects = new object[1];
                objects[0] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isBranchCustomerDependency;
        }


        /// <summary>
        ///BBefore deleting a associate category checking dependency of category in AdvisorBranch
        /// </summary>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="userid"></param>
        /// <param name="IsMainBranch"></param>
        /// <returns></returns>
        public bool CheckAssociateCategoryDependency(int categoryId)
        {
            bool isAssociateCategoryDependent = false;
            int countDependent = 0;
            Database db;
            DbCommand checkAssociateCategoryDependentCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkAssociateCategoryDependentCmd = db.GetStoredProcCommand("SP_CheckAssociateCategoryDependent");
                db.AddInParameter(checkAssociateCategoryDependentCmd, "@CategoryId", DbType.Int32, categoryId);
                db.AddOutParameter(checkAssociateCategoryDependentCmd, "@Count", DbType.Int32, sizeof(Int32));
                if (db.ExecuteNonQuery(checkAssociateCategoryDependentCmd) != 0)
                    countDependent = int.Parse(db.GetParameterValue(checkAssociateCategoryDependentCmd, "Count").ToString());
                if (countDependent > 0)
                {
                    isAssociateCategoryDependent = true;
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckAssociateCategoryDependency()");
                object[] objects = new object[1];
                objects[0] = categoryId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isAssociateCategoryDependent;
        }

        public List<CustomerVo> GetAdviserCustomerListForAssociation(int branchIdFilter, int adviserId, int currentPage, out int count, string sortExpression, string custNameFilter, string branchNameFilter, string rmNameFilter, string areaFilter, string cityFilter, out Dictionary<string, string> advisorBranchList)
        {
            List<CustomerVo> customerList = null;
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;

            advisorBranchList = new Dictionary<string, string>();
            //genDictRM = new Dictionary<string, string>();
            //genDictReassignRM = new Dictionary<string, string>();
            count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerListForAssociation");
                db.AddInParameter(getCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(getCustomerListCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, sortExpression);

                if (branchIdFilter != 0)
                    db.AddInParameter(getCustomerListCmd, "@branchIdFilter", DbType.Int32, branchIdFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@branchIdFilter", DbType.Int32, DBNull.Value);

                if (branchNameFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@BranchFilter", DbType.String, branchNameFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@BranchFilter", DbType.String, DBNull.Value);

                if (custNameFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, custNameFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, DBNull.Value);

                if (areaFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, areaFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, DBNull.Value);

                if (cityFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@CityFilter", DbType.String, cityFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@CityFilter", DbType.String, DBNull.Value);



                db.AddInParameter(getCustomerListCmd, "@parentFilter", DbType.String, DBNull.Value);

                if (rmNameFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, rmNameFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, DBNull.Value);

                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);

                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<CustomerVo>();
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();

                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.BranchName = dr["AB_BranchName"].ToString();
                        customerVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                        //customerVo.CustCode = dr["C_CustCode"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        //customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        //customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        //customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        //customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        //if (dr["Parent"].ToString() != "")
                        //    customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        //if (dr["C_Mobile1"].ToString() != "")
                        //    customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        if (dr["RMName"].ToString() != "")
                            customerVo.AssignedRM = dr["RMName"].ToString();
                        customerVo.UserType = dr["CustomerType"].ToString();
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;


                if (getCustomerDs.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[2].Rows)
                    {
                        advisorBranchList.Add(dr["AB_BranchId"].ToString(), dr["AB_BranchName"].ToString());
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

                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetAdviserCustomerListForAssociation()");

                object[] objects = new object[11];
                objects[0] = adviserId;
                objects[1] = currentPage;
                objects[2] = count;
                objects[3] = sortExpression;
                objects[4] = custNameFilter;
                objects[5] = areaFilter;
                objects[6] = branchNameFilter;
                objects[7] = rmNameFilter;
                objects[8] = branchIdFilter;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (getCustomerDs.Tables[1].Rows.Count > 0)
                count = Int32.Parse(getCustomerDs.Tables[1].Rows[0]["CNT"].ToString());

            return customerList;
        }
        /// <summary>
        /// Checking any of one customer is GroupHead(Customer are associated to him) 
        /// </summary>
        /// <param name="customerIds"></param>
        /// <returns></returns>
        public bool CheckCustomerGroupHead(string customerIds)
        {
            Database db;
            DbCommand chkCustomerIdCmd;
            int count = 0;
            bool flag = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                chkCustomerIdCmd = db.GetStoredProcCommand("SP_CheckCustomerGroupHead");
                db.AddInParameter(chkCustomerIdCmd, "@CustomerId", DbType.String, customerIds);
                db.AddOutParameter(chkCustomerIdCmd, "@CustomerCount", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(chkCustomerIdCmd) != 0)
                    count = int.Parse(db.GetParameterValue(chkCustomerIdCmd, "CustomerCount").ToString());

                if (count > 1)
                {
                    flag = true;
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:CheckCustomerGroupHead()");
                object[] objects = new object[2];
                objects[0] = customerIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerIds"></param>
        /// <param name="branchId"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public bool ReassignCustomersBranchRM(string customerIds, int branchId, int rmId)
        {
            Database db;
            DbCommand reassignCustomersBranchRMCmd;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                reassignCustomersBranchRMCmd = db.GetStoredProcCommand("SP_ReassignCustomersBranchAndRM");
                db.AddInParameter(reassignCustomersBranchRMCmd, "@CustomerIds", DbType.String, customerIds);
                db.AddInParameter(reassignCustomersBranchRMCmd, "@BranchId", DbType.Int32, branchId);
                if (rmId != 0)
                    db.AddInParameter(reassignCustomersBranchRMCmd, "@RMId", DbType.Int32, rmId);
                else
                    db.AddInParameter(reassignCustomersBranchRMCmd, "@RMId", DbType.Int32, DBNull.Value);
                if (db.ExecuteNonQuery(reassignCustomersBranchRMCmd) != 0)
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
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:ReassignCustomersBranchRM()");
                object[] objects = new object[3];
                objects[0] = customerIds;
                objects[1] = branchId;
                objects[2] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

         /* For Branch Assets */

        public DataSet GetBranchAssets(int advisorBranchId, int branchHeadId, int all)
        {
            Database db;
            DbCommand getBranchAssetValueCmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchAssetValueCmd = db.GetStoredProcCommand("SP_BMDashboard");
                if (advisorBranchId != 0)
                    db.AddInParameter(getBranchAssetValueCmd, "@advisorBranchId", DbType.Int16, advisorBranchId);
                if (branchHeadId != 0)
                    db.AddInParameter(getBranchAssetValueCmd, "@branchHeadId", DbType.Int16, branchHeadId);
                if (all == 1)
                    db.AddInParameter(getBranchAssetValueCmd, "@all", DbType.Int16, all);

                ds = db.ExecuteDataSet(getBranchAssetValueCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranchAssets()");
                object[] objects = new object[3];
                objects[0] = advisorBranchId;
                objects[1] = branchHeadId;
                objects[2] = all;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
     }

        /* End For Branch Assets */


        /* For Branch RM dropdowns */


     public DataSet GetBranchsRMForBMDp(int branchId, int branchHeadId, int all)
        {
            Database db;
            DbCommand getBranchsRMCmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchsRMCmd = db.GetStoredProcCommand("Get_BindBranchRMdropDowns");
                    db.AddInParameter(getBranchsRMCmd, "@branchId", DbType.Int16, branchId);
                    db.AddInParameter(getBranchsRMCmd, "@branchHeadId", DbType.Int16, branchHeadId);
                    db.AddInParameter(getBranchsRMCmd, "@all", DbType.Int16, all);

                   ds = db.ExecuteDataSet(getBranchsRMCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetBranchsRMForBMDp()");
                object[] objects = new object[3];
                objects[0] = branchId;
                objects[1] = branchHeadId;
                objects[2] = all;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }


     public DataSet GetAdviserCustomerFolioMerge(int adviserId, int currentPage, string custNameFilter,out int count)
     {
         
         Database db;
         DbCommand getCustomerListCmd;
         DataSet getCustomerDs;
                 
             db = DatabaseFactory.CreateDatabase("wealtherp");
             getCustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerFolioMerge");
             db.AddInParameter(getCustomerListCmd, "@AdviserID", DbType.Int32, adviserId);
             db.AddInParameter(getCustomerListCmd, "@currentPage", DbType.Int32, currentPage);
             
             if (custNameFilter != "")
                 db.AddInParameter(getCustomerListCmd, "@Search", DbType.String, custNameFilter);
             else
                 db.AddInParameter(getCustomerListCmd, "@Search", DbType.String, DBNull.Value);

             db.AddOutParameter(getCustomerListCmd, "@count", DbType.Int64, 1);
             getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
             count =int.Parse(db.GetParameterValue(getCustomerListCmd, "@count").ToString());            
             return getCustomerDs;

                  
     }


     public DataSet GetCustomerFolioMergeList(int customerId, int amcCode,string fnumber)
     {

         Database db;
         DbCommand getCustomerListCmd;
         DataSet getCustomerDs;

         db = DatabaseFactory.CreateDatabase("wealtherp");
         getCustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerFolioMergeList");
         db.AddInParameter(getCustomerListCmd, "@CustomerID", DbType.Int32, customerId);
         db.AddInParameter(getCustomerListCmd, "@AMCCode", DbType.Int32, amcCode);
         db.AddInParameter(getCustomerListCmd, "@Folio", DbType.String, fnumber);
         getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
         return getCustomerDs;


     }


     public bool CustomerFolioMerged(string ffromfolio, string fnumber, int customerId)
     {

         Database db;
         DbCommand getCustomerListCmd;
         DataSet getCustomerDs;
         bool result = true;

         db = DatabaseFactory.CreateDatabase("wealtherp");
         getCustomerListCmd = db.GetStoredProcCommand("SP_MergerCustomerFolio");
         db.AddInParameter(getCustomerListCmd, "@ToFolio", DbType.String, ffromfolio);
         db.AddInParameter(getCustomerListCmd, "@FromFolio", DbType.String, fnumber);
         db.AddInParameter(getCustomerListCmd, "@CustomerID", DbType.Int32, customerId);
         getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
         return result ;


     }

        /* End For Branch RM dropdowns */


    /// <summary>
     /// Getting RM's who are all not having BM role. Added by <<Kirteeshree>>
    /// </summary>
    /// <param name="branchId"></param>
    /// <param name="branchHeadId"></param>
    /// <returns></returns>

     public DataSet GetAllRMsWithOutBMRole(int branchId, int branchHeadId)
     {
         Database db;
         DbCommand getBranchsRMCmd;
         DataSet ds = null;

         try
         {
             db = DatabaseFactory.CreateDatabase("wealtherp");
             getBranchsRMCmd = db.GetStoredProcCommand("SP_GetAllRMsWithOutBMRole");
             db.AddInParameter(getBranchsRMCmd, "@branchId", DbType.Int16, branchId);
             db.AddInParameter(getBranchsRMCmd, "@branchHeadId", DbType.Int16, branchHeadId);

             ds = db.ExecuteDataSet(getBranchsRMCmd);


         }
         catch (BaseApplicationException Ex)
         {
             throw Ex;
         }
         catch (Exception Ex)
         {
             BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
             NameValueCollection FunctionInfo = new NameValueCollection();
             FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetAllRMsWithOutBMRole()");
             object[] objects = new object[3];
             objects[0] = branchId;
             objects[1] = branchHeadId;
             FunctionInfo = exBase.AddObject(FunctionInfo, objects);
             exBase.AdditionalInformation = FunctionInfo;
             ExceptionManager.Publish(exBase);
             throw exBase;
         }
         return ds;
     }
    }

    
}
