using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoAdvisorProfiling;


namespace DaoAdvisorProfiling
{
    public class AdvisorPreferenceDao
    {

        public AdvisorPreferenceVo GetAdviserPreference(int adviserId)
        {
            Database db;
            DbCommand cmdAdviserPreference;
            DataSet dsAdviserPreference = null;
            DataTable dtAdviserPreference = new DataTable();
            AdvisorPreferenceVo advisorPreferenceVo = new AdvisorPreferenceVo();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAdviserPreference = db.GetStoredProcCommand("SPROC_GetAdviserPreference");
                db.AddInParameter(cmdAdviserPreference, "@AdviserId", DbType.Int32, adviserId);
                dsAdviserPreference = db.ExecuteDataSet(cmdAdviserPreference);
                dtAdviserPreference = dsAdviserPreference.Tables[0];
                if (dtAdviserPreference.Rows.Count > 0)
                {
                    advisorPreferenceVo.ValtPath = dtAdviserPreference.Rows[0]["AP_ValtPath"].ToString();
                    advisorPreferenceVo.IsLoginWidgetEnable = Convert.ToBoolean(dtAdviserPreference.Rows[0]["AP_IsLoginWidgetActive"]);
                    if (!string.IsNullOrEmpty(dtAdviserPreference.Rows[0]["AP_LoginWidgetLogOutPageURL"].ToString()))
                        advisorPreferenceVo.LoginWidgetLogOutPageURL = dtAdviserPreference.Rows[0]["AP_LoginWidgetLogOutPageURL"].ToString();
                    advisorPreferenceVo.BrowserTitleBarName = dtAdviserPreference.Rows[0]["AP_BrowserTitleBarName"].ToString();
                    advisorPreferenceVo.BrowserTitleBarIconImageName = dtAdviserPreference.Rows[0]["AP_BrowserTitleBarIconImageName"].ToString();
                    advisorPreferenceVo.WebSiteDomainName = dtAdviserPreference.Rows[0]["AP_WebSiteDomainName"].ToString();
                    advisorPreferenceVo.GridPageSize = int.Parse(dtAdviserPreference.Rows[0]["AP_GridPageSize"].ToString());
                    if (dtAdviserPreference.Rows[0]["AP_IsBannerEnable"].ToString() == "1")
                        advisorPreferenceVo.IsBannerEnabled = true;
                    advisorPreferenceVo.BannerImageName = dtAdviserPreference.Rows[0]["AP_BannerImageName"].ToString();

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
                FunctionInfo.Add("Method", "AdvisorPreferenceDao.cs:GetAdviserPreference()");
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
            Database db;
            DbCommand cmdAdviserPreferenceSetUp;
            DataTable dtAdviserPreference = new DataTable();
            // string LoginWidgetLogOutPageURL=null;
            bool isSuccess = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAdviserPreferenceSetUp = db.GetStoredProcCommand("SPROC_UpdateAdviserPreference");
                if (!string.IsNullOrEmpty(strCommand))
                    db.AddInParameter(cmdAdviserPreferenceSetUp, "@strCommand", DbType.String, strCommand);
                else
                    db.AddInParameter(cmdAdviserPreferenceSetUp, "@strCommand", DbType.String, DBNull.Value);

                db.AddInParameter(cmdAdviserPreferenceSetUp, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@IsLoginWidgetEnable", DbType.Int16, advisorPreferenceVo.IsLoginWidgetEnable);
                if (!string.IsNullOrEmpty(advisorPreferenceVo.LoginWidgetLogOutPageURL))
                    db.AddInParameter(cmdAdviserPreferenceSetUp, "@LoginWidgetLogOutPageURL", DbType.String, advisorPreferenceVo.LoginWidgetLogOutPageURL);
                if (!string.IsNullOrEmpty(advisorPreferenceVo.BrowserTitleBarName))
                    db.AddInParameter(cmdAdviserPreferenceSetUp, "@BrowserTitleBarName", DbType.String, advisorPreferenceVo.BrowserTitleBarName);
                if (!string.IsNullOrEmpty(advisorPreferenceVo.WebSiteDomainName))
                    db.AddInParameter(cmdAdviserPreferenceSetUp, "@WebSiteDomainName", DbType.String, advisorPreferenceVo.WebSiteDomainName);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@GridPageSize", DbType.Int32, advisorPreferenceVo.GridPageSize);
                db.AddInParameter(cmdAdviserPreferenceSetUp, "@UserId", DbType.String, UserId);
                if (db.ExecuteNonQuery(cmdAdviserPreferenceSetUp) != 0)
                    isSuccess = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorPreferenceDao.cs:AdviserPreferenceSetUp()");
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
            DataSet dsTreeNodes;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetParentTreeNodes");
                db.AddInParameter(dbCommand, "@userlevelId", DbType.Int32, userlevelId);
                db.AddInParameter(dbCommand, "@advisorId", DbType.String, advisorId);
                dsTreeNodes = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsTreeNodes;
        }

        public DataSet GetSubTreeNodes(int parentNode, int userlevelId, int advisorId)
        {
            DataSet dsSubTreeNodes;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSubTreeNodes");
                db.AddInParameter(dbCommand, "@SubNodeId", DbType.Int32, parentNode);
                db.AddInParameter(dbCommand, "@userlevelId", DbType.Int32, userlevelId);
                db.AddInParameter(dbCommand, "@advisorId", DbType.String, advisorId);
                dsSubTreeNodes = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsSubTreeNodes;
        }
        public DataSet GetSubSubTreeNodes(int subNode, int userlevelId, int advisorId)
        {
            DataSet dsSubSubTreeNodes;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetSubSubTreeNodes");
                db.AddInParameter(dbCommand, "@SubNodeId", DbType.Int32, subNode);
                db.AddInParameter(dbCommand, "@userlevelId", DbType.Int32, userlevelId);
                db.AddInParameter(dbCommand, "@advisorId", DbType.String, advisorId);
                dsSubSubTreeNodes = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsSubSubTreeNodes;
        }

        public DataSet GetActualRoles(int roleId, int advisorId)
        {

            DataSet dsGetActualRoles;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetActualRoles");
                db.AddInParameter(dbCommand, "@userRoleId", DbType.Int32, roleId);
                db.AddInParameter(dbCommand, "@advisorId", DbType.String, advisorId);
                dsGetActualRoles = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsGetActualRoles;
        }
        public DataSet GetAdviserRoles(int advisorId)
        {
            DataSet dsGetAdviserRoles;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetAdviserRoles");
                db.AddInParameter(dbCommand, "@advisorId", DbType.Int32, advisorId);
                dsGetAdviserRoles = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsGetAdviserRoles;
        }

        public DataSet GetRoleLevelTreeNodes(int roleId,int levelId)
        {
            DataSet dsGetNodes;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_GetRoleLevelTreeNodes");
                db.AddInParameter(dbCommand, "@RoleId", DbType.Int32, roleId);
                db.AddInParameter(dbCommand, "@LevelId", DbType.Int32, levelId);

                dsGetNodes = db.ExecuteDataSet(dbCommand);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsGetNodes;
        }

        public int CreateOrUpdateTreeNodeMapping(DataTable dtTreeNodes, string commandType, int userId,int levelId)
        {
            int i = 0;
            try
            {

                Database db;
                DbCommand cmdtreeNode;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dtTreeNodes.Copy();
                ds.Tables.Add(dt);
                String sb;
                sb = ds.GetXml().ToString();

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdtreeNode = db.GetStoredProcCommand("SPROC_CreateAdviserRoleToTreeNodeMaping");
                db.AddInParameter(cmdtreeNode, "@xmlTreeNode", DbType.Xml, sb);
                db.AddInParameter(cmdtreeNode, "@CommandType", DbType.String, commandType);
                db.AddInParameter(cmdtreeNode, "@userId", DbType.Int32, userId);
                db.AddInParameter(cmdtreeNode, "@levelId", DbType.Int32, levelId);                
                i = db.ExecuteNonQuery(cmdtreeNode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return i;
        }

        public int CreateOrUpdateTreeSubNodeMapping(DataTable dtSubTreeNodes, string commandType, int userId, int levelId)
        {
            int i = 0;
            try
            {

                Database db;
                DbCommand cmdtreeNode;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dtSubTreeNodes.Copy();
                ds.Tables.Add(dt);
                String sb;
                sb = ds.GetXml().ToString();

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdtreeNode = db.GetStoredProcCommand("SPROC_CreateAdviserRoleToSubTreeNodeMaping");
                db.AddInParameter(cmdtreeNode, "@xmlSubTreeNode", DbType.Xml, sb);
                db.AddInParameter(cmdtreeNode, "@CommandType", DbType.String, commandType);
                db.AddInParameter(cmdtreeNode, "@userId", DbType.Int32, userId);
                db.AddInParameter(cmdtreeNode, "@levelId", DbType.Int32, levelId);                

                i = db.ExecuteNonQuery(cmdtreeNode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return i;
        }

        public int CreateOrUpdateTreeSubSubNodeMapping(DataTable dtSubSubTreeNodes, string commandType, int userId, int levelId)
        {
            int i = 0;
            try
            {

                Database db;
                DbCommand cmdtreeNode;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = dtSubSubTreeNodes.Copy();
                ds.Tables.Add(dt);
                String sb;
                sb = ds.GetXml().ToString();

                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdtreeNode = db.GetStoredProcCommand("SPROC_CreateAdviserRoleToSubSubTreeNodeMaping");
                db.AddInParameter(cmdtreeNode, "@xmlSubSubTreeNode", DbType.Xml, sb);
                db.AddInParameter(cmdtreeNode, "@CommandType", DbType.String, commandType);
                db.AddInParameter(cmdtreeNode, "@userId", DbType.Int32, userId);
                db.AddInParameter(cmdtreeNode, "@levelId", DbType.Int32, levelId);                

                i = db.ExecuteNonQuery(cmdtreeNode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return i;
        }

        public int RemoveTreeNodeMapping(DataTable dtSubSubTreeNodes, DataTable dtSubTreeNodes, DataTable dtTreeNodes)
        {
            int i = 0;
            try
            {

                Database db;
                DbCommand cmdtreeNode;

                DataSet dsSubSubTN = new DataSet();
                DataTable dtSubSubTN = new DataTable();

                DataSet dsSubTN = new DataSet();
                DataTable dtSubTN = new DataTable();

                DataSet dsTN = new DataSet();
                DataTable dtTN = new DataTable();

                dtSubSubTN = dtSubSubTreeNodes.Copy();
                dsSubSubTN.Tables.Add(dtSubSubTN);
                String sbSubSubTreeNodes;
                sbSubSubTreeNodes = dsSubSubTN.GetXml().ToString();


                dtSubTN = dtSubTreeNodes.Copy();
                dsSubTN.Tables.Add(dtSubTN);
                String sbSubTreeNodes;
                sbSubTreeNodes = dsSubTN.GetXml().ToString();

                dtTN = dtTreeNodes.Copy();
                dsTN.Tables.Add(dtTN);
                String sbTreeNodes;
                sbTreeNodes = dsTN.GetXml().ToString();


                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdtreeNode = db.GetStoredProcCommand("SPROC_RemoveAdviserRoleToTreeNodeMapings");
                db.AddInParameter(cmdtreeNode, "@xmlSubSubTreeNode", DbType.Xml, sbSubSubTreeNodes);
                db.AddInParameter(cmdtreeNode, "@xmlSubTreeNode", DbType.Xml, sbSubTreeNodes);
                db.AddInParameter(cmdtreeNode, "@xmlTreeNode", DbType.Xml, sbTreeNodes);               
                i = db.ExecuteNonQuery(cmdtreeNode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return i;
        }

        public DataSet GetUserRole(int adviserid)
        {
            Database db;
            DataSet dsGetUserRole;
            DbCommand GetUserRolecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetUserRolecmd = db.GetStoredProcCommand("SPROC_GetUserRole");
                db.AddInParameter(GetUserRolecmd, "@AdviserId", DbType.Int32, adviserid);
                dsGetUserRole = db.ExecuteDataSet(GetUserRolecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetUserRole()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetUserRole;
        }
        public DataSet GetDepartment(int AdviserId)
        {
            Database db;
            DataSet dsGetUserRole;
            DbCommand GetUserRolecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetUserRolecmd = db.GetStoredProcCommand("SPROC_GetAdviserDepartment");
                db.AddInParameter(GetUserRolecmd, "@adviserId", DbType.Int32, AdviserId);
                dsGetUserRole = db.ExecuteDataSet(GetUserRolecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetFrequency()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetUserRole;
        }
        public bool CreateUserRole(int UserRole, string RoleName, string Purpose, int AdviserId, int UserId, string StrUserLeve)
        {
            bool bResult = false;
            Database db;
            DbCommand CreateUserRoleCmd;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateUserRoleCmd = db.GetStoredProcCommand("SPROC_CreateAdviserUserRole");
                db.AddInParameter(CreateUserRoleCmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(CreateUserRoleCmd, "@AD_DepartmentId", DbType.Int32, UserRole);
                db.AddInParameter(CreateUserRoleCmd, "@AR_Role", DbType.String, RoleName);
                db.AddInParameter(CreateUserRoleCmd, "@AR_RolePurpose", DbType.String, Purpose);
                db.AddInParameter(CreateUserRoleCmd, "@CreatedBy", DbType.Int32, UserId);
                db.AddInParameter(CreateUserRoleCmd, "@ModifiedBy", DbType.Int32, UserId);
                db.AddInParameter(CreateUserRoleCmd, "@UserlevelId", DbType.String, StrUserLeve);
                if (db.ExecuteNonQuery(CreateUserRoleCmd) != 0)
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

                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:CreateUserRole()");

                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool UpdateUserrole(int rollid, int userrole, string rolename, string purpose, int userid, string StrUserLeve)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateUserroleCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateUserroleCmd = db.GetStoredProcCommand("SPROC_UpdateAdviserUserRole");
                db.AddInParameter(UpdateUserroleCmd, "@Rolid", DbType.Int32, rollid);
                db.AddInParameter(UpdateUserroleCmd, "@DepartmentId", DbType.Int32, userrole);
                db.AddInParameter(UpdateUserroleCmd, "@Role", DbType.String, rolename);
                db.AddInParameter(UpdateUserroleCmd, "@purpose", DbType.String, purpose);
                db.AddInParameter(UpdateUserroleCmd, "@ModifiedBy", DbType.Int32, userid);
                db.AddInParameter(UpdateUserroleCmd, "@UserlevelId", DbType.String, StrUserLeve);
                if (db.ExecuteNonQuery(UpdateUserroleCmd) != 0)
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
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateUserrole()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool DeleteUserRole(int rollid)
        {
            bool bResult = false;
            Database db;
            DbCommand createtradeBusinessDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createtradeBusinessDateCmd = db.GetStoredProcCommand("SPROC_ToDeleteUser");
                db.AddInParameter(createtradeBusinessDateCmd, "@RollId", DbType.Int32, rollid);

                if (db.ExecuteNonQuery(createtradeBusinessDateCmd) != 0)
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
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:deleteTradeBusinessDate()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }
        //public DataSet GetUserRoleDepartmentWise(int departmentid)
        //{
        //    Database db;
        //    DataSet dsGetUserRoleDepartmentWise;
        //    DbCommand GetUserRoleDepartmentWisecmd;
        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        GetUserRoleDepartmentWisecmd = db.GetStoredProcCommand("SPROC_ToGetAllUserRole");
        //        db.AddInParameter(GetUserRoleDepartmentWisecmd, "@Departmentid", DbType.Int32, departmentid);
        //        dsGetUserRoleDepartmentWise = db.ExecuteDataSet(GetUserRoleDepartmentWisecmd);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetFrequency()");
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return dsGetUserRoleDepartmentWise;
        //}
        public DataTable GetUserRoleDepartmentWise(int departmentid)
        {
            Database db;
            DbCommand cmdGetUserRoleDepartmentWise;
            DataSet dsGetUserRoleDepartmentWise = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetUserRoleDepartmentWise = db.GetStoredProcCommand("SPROC_GetDepartmentLevels");
                db.AddInParameter(cmdGetUserRoleDepartmentWise, "@DepartmentId", DbType.Int32, departmentid);
                dsGetUserRoleDepartmentWise = db.ExecuteDataSet(cmdGetUserRoleDepartmentWise);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetUserRoleDepartmentWise(int departmentid)");
                object[] objects = new object[1];
                objects[0] = departmentid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetUserRoleDepartmentWise.Tables[0];
        }
        public DataSet GetAdviserRoledepartmentwise(int roleid)
        {
            DataSet dsGetAdviserRoledepartmentwise;
            Database db;
            DbCommand GetAdviserRoledepartmentwisecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserRoledepartmentwisecmd = db.GetStoredProcCommand("SPROC_GetdepartmentRole");
                //db.AddInParameter(GetAdviserRoledepartmentwisecmd, "@DepartmentId", DbType.String, departmentid);
                db.AddInParameter(GetAdviserRoledepartmentwisecmd, "@AR_Roleid", DbType.String, roleid);
                dsGetAdviserRoledepartmentwise = db.ExecuteDataSet(GetAdviserRoledepartmentwisecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetSeriesInvestorTypeRule()");
                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAdviserRoledepartmentwise;
        }
    }
}
