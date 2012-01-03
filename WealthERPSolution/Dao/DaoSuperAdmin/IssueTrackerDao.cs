using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoSuperAdmin; 

namespace DaoSuperAdmin
{
    public class IssueTrackerDao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int InsertcsissueTrackerDetails(IssueTrackerVo superAdminCSIssueTrackerVo) 
        {

            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            Database db;
            DbCommand insertcsissueDetailsCmd;
            DbCommand insertIssueLevelCmd;
            DbCommand insertIssueLevelNewCmd;
           

            db = DatabaseFactory.CreateDatabase("wealtherp");
            insertcsissueDetailsCmd = db.GetStoredProcCommand("SP_CSIssueInsert");
            insertIssueLevelCmd = db.GetStoredProcCommand("SP_CsIssueLevelAssociationInsert");
            insertIssueLevelNewCmd = db.GetStoredProcCommand("SP_CsIssueLevelAssociationInsertLevel2New");

            try
            {
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Code", DbType.String, superAdminCSIssueTrackerVo.CSI_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@A_AdviserId", DbType.Int32, superAdminCSIssueTrackerVo.A_AdviserId);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ContactPerson", DbType.String, superAdminCSIssueTrackerVo.CSI_ContactPerson);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Phone", DbType.String, superAdminCSIssueTrackerVo.CSI_Phone);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Email", DbType.String, superAdminCSIssueTrackerVo.CSI_Email);
                db.AddInParameter(insertcsissueDetailsCmd, "@UR_RoleId", DbType.Int32, superAdminCSIssueTrackerVo.UR_RoleId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTN_TreeNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTN_TreeNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTSN_TreeSubNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTSN_TreeSubNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTSSN_TreeSubSubNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTSSN_TreeSubSubNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_CustomerDescription", DbType.String, superAdminCSIssueTrackerVo.CSI_CustomerDescription);                
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_issueAddedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_issueAddedDate);

                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_issueAddedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedVia", DbType.String, superAdminCSIssueTrackerVo.CSI_ReportedVia);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_CustomerSupportComments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Atuhor", DbType.String, superAdminCSIssueTrackerVo.CSI_Atuhor);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);

                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSP_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSP_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSS_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSS_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCST_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCST_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLACSP_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLACSP_Code);
                if (superAdminCSIssueTrackerVo.CSI_ResolvedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ResolvedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ResolvedDate);
                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ResolvedDate", DbType.DateTime, DBNull.Value);
                db.AddOutParameter(insertcsissueDetailsCmd, "@CSI_id", DbType.Int32, 1);
                
                j = db.ExecuteNonQuery(insertcsissueDetailsCmd);

                int csId = Convert.ToChar(db.GetParameterValue(insertcsissueDetailsCmd, "@CSI_id"));

                db.AddInParameter(insertIssueLevelCmd, "@CSI_id", DbType.Int32, csId);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);

                db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSI_Atuhor);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertIssueLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                k = db.ExecuteNonQuery(insertIssueLevelCmd);

                db.AddInParameter(insertIssueLevelNewCmd, "@CSI_id", DbType.Int32, csId);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Comments", DbType.String,DBNull.Value);

                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(insertIssueLevelNewCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                l = db.ExecuteNonQuery(insertIssueLevelNewCmd);

            }
            catch(Exception ex)
            {
                ex.StackTrace.ToString();
            }
            i = j + k+l;
            return i;            
        }

        public int InsertcsissueTrackerDetailsLevel3(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            Database db;
            DbCommand insertcsissueDetailsCmd;
            DbCommand insertIssueLevelCmd;
            DbCommand IssueLevelAssociationInsertLevel3Cmd;


            db = DatabaseFactory.CreateDatabase("wealtherp");
            insertcsissueDetailsCmd = db.GetStoredProcCommand("SP_CSIssueInsert");
            insertIssueLevelCmd = db.GetStoredProcCommand("SP_CsIssueLevelAssociationInsert");
            IssueLevelAssociationInsertLevel3Cmd = db.GetStoredProcCommand("SP_CsIssueLevelAssociationInsertLevel3");

            try
            {
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Code", DbType.String, superAdminCSIssueTrackerVo.CSI_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@A_AdviserId", DbType.Int32, superAdminCSIssueTrackerVo.A_AdviserId);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ContactPerson", DbType.String, superAdminCSIssueTrackerVo.CSI_ContactPerson);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Phone", DbType.String, superAdminCSIssueTrackerVo.CSI_Phone);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Email", DbType.String, superAdminCSIssueTrackerVo.CSI_Email);
                db.AddInParameter(insertcsissueDetailsCmd, "@UR_RoleId", DbType.Int32, superAdminCSIssueTrackerVo.UR_RoleId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTN_TreeNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTN_TreeNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTSN_TreeSubNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTSN_TreeSubNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTSSN_TreeSubSubNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTSSN_TreeSubSubNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_CustomerDescription", DbType.String, superAdminCSIssueTrackerVo.CSI_CustomerDescription);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_issueAddedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_issueAddedDate);

                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_issueAddedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedVia", DbType.String, superAdminCSIssueTrackerVo.CSI_ReportedVia);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_CustomerSupportComments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Atuhor", DbType.String, superAdminCSIssueTrackerVo.CSI_Atuhor);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);

                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSP_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSP_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSS_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSS_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCST_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCST_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLACSP_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLACSP_Code);
                if (superAdminCSIssueTrackerVo.CSI_ResolvedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ResolvedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ResolvedDate);
                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ResolvedDate", DbType.DateTime, DBNull.Value);
                db.AddOutParameter(insertcsissueDetailsCmd, "@CSI_id", DbType.Int32, 1);

                j = db.ExecuteNonQuery(insertcsissueDetailsCmd);

                int csId = Convert.ToChar(db.GetParameterValue(insertcsissueDetailsCmd, "@CSI_id"));

                db.AddInParameter(insertIssueLevelCmd, "@CSI_id", DbType.Int32, csId);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);

                db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSI_Atuhor);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertIssueLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                k = db.ExecuteNonQuery(insertIssueLevelCmd);

                db.AddInParameter(IssueLevelAssociationInsertLevel3Cmd, "@CSI_id", DbType.Int32, csId);
                db.AddInParameter(IssueLevelAssociationInsertLevel3Cmd, "@CSILA_Comments", DbType.String, DBNull.Value);

                db.AddInParameter(IssueLevelAssociationInsertLevel3Cmd, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(IssueLevelAssociationInsertLevel3Cmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(IssueLevelAssociationInsertLevel3Cmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(IssueLevelAssociationInsertLevel3Cmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(IssueLevelAssociationInsertLevel3Cmd, "@CSILA_Version", DbType.String, DBNull.Value);

                l = db.ExecuteNonQuery(IssueLevelAssociationInsertLevel3Cmd);

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            i = j + k + l;
            return i;            
        }


       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public int autoIncrementcsiSSUECode()
        {
            Database db;
            int CsCode=0;
            DbCommand autoIncrementcsissueCodecmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            autoIncrementcsissueCodecmd = db.GetStoredProcCommand("SP_autoIncrementCSIssueCode");
            try
            {
                db.AddOutParameter(autoIncrementcsissueCodecmd, "@CsCode", DbType.Int32, 1);
                if (db.ExecuteNonQuery(autoIncrementcsissueCodecmd) != 0)
                {
                    CsCode = Convert.ToChar(db.GetParameterValue(autoIncrementcsissueCodecmd, "@CsCode"));

                  
                }
                else
                {
                    CsCode = 1;
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CsCode;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DataSet GetTreeNodeList(int roleId)
        {
            Database db;
            DataSet ds;
            DbCommand getTreeNodeListcmd;
            
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getTreeNodeListcmd = db.GetStoredProcCommand("SP_GetTreeNodeList");
            try
            {                
                db.AddInParameter(getTreeNodeListcmd, "@UR_RoleId", DbType.Int32, roleId);
                ds = db.ExecuteDataSet(getTreeNodeListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="treeSubNodeId"></param>
        /// <returns></returns>
        public DataSet GetTreeSubNodeList(int roleId, int treeSubNodeId)
        {
            Database db;
            DataSet ds;
            DbCommand getTreeSubNodeListcmd;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            getTreeSubNodeListcmd = db.GetStoredProcCommand("SP_GetTreeSubNodeList");
            try
            {
                db.AddInParameter(getTreeSubNodeListcmd, "@UR_RoleId", DbType.Int32, roleId);
                db.AddInParameter(getTreeSubNodeListcmd, "@treeSubNodeId", DbType.Int32, treeSubNodeId);
                ds = db.ExecuteDataSet(getTreeSubNodeListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="treeSubNodeId"></param>
        /// <param name="treeSubSubNodeId"></param>
        /// <returns></returns>
        public DataSet GetTreeSubSubNodeList(int roleID, int treeSubNodeId, int treeSubSubNodeId)
        {
            Database db;
            DataSet ds;
            DbCommand getTreeSubSubNodeListcmd;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            getTreeSubSubNodeListcmd = db.GetStoredProcCommand("SP_GetTreeSubSubNodeList");
            try
            {
                db.AddInParameter(getTreeSubSubNodeListcmd, "@UR_RoleId", DbType.Int32, roleID);
                db.AddInParameter(getTreeSubSubNodeListcmd, "@WTN_TreeNodeId", DbType.Int32, treeSubNodeId);
                db.AddInParameter(getTreeSubSubNodeListcmd, "@WTSN_TreeSubNodeId", DbType.Int32, treeSubSubNodeId);
                ds = db.ExecuteDataSet(getTreeSubSubNodeListcmd);
                //dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetPriorityList()
        {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getPriorityListcmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getPriorityListcmd = db.GetStoredProcCommand("SP_GetPriorityList");

            try
            {
                ds= db.ExecuteDataSet(getPriorityListcmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomerPriorityList()
        {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getCustomerPriorityListCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerPriorityListCmd = db.GetStoredProcCommand("SP_GetCustomerPriorityList");

            try
            {
                ds = db.ExecuteDataSet(getCustomerPriorityListCmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }            
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetLevelList()
        {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getPriorityListcmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getPriorityListcmd = db.GetStoredProcCommand("SP_GetLevelList");

            try
            {
                ds = db.ExecuteDataSet(getPriorityListcmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTypesList()
            {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getPriorityListcmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getPriorityListcmd = db.GetStoredProcCommand("SP_GetTypesList");

            try
            {
                ds = db.ExecuteDataSet(getPriorityListcmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetStatusList()
            {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getStatusListcmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getStatusListcmd = db.GetStoredProcCommand("SP_GetStatusList");

            try
            {
                ds = db.ExecuteDataSet(getStatusListcmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdviserList()
            {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getAdviserListcmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getAdviserListcmd = db.GetStoredProcCommand("SP_GetAdviserList");

            try
            {
                ds = db.ExecuteDataSet(getAdviserListcmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
        public DataTable GetRoleList()
            {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getWerpFlavourListcmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getWerpFlavourListcmd = db.GetStoredProcCommand("SP_GetRoleList");

            try
            {
                ds = db.ExecuteDataSet(getWerpFlavourListcmd);
                dt = ds.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet getCSIssueDetails()
        {
            DataSet ds;
            Database db;
            DbCommand getCSIssueDetailsCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCSIssueDetailsCmd = db.GetStoredProcCommand("SP_GetCSIssueData");

            try
            {
                ds = db.ExecuteDataSet(getCSIssueDetailsCmd);
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csId"></param>
        /// <param name="strlevelName"></param>
        /// <returns></returns>
        public DataSet getCSIssueDataAccordingToCSId(int csId)
           {
            DataSet ds;
            Database db;
            DbCommand getCSIssueDataAccordingToCSIdCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCSIssueDataAccordingToCSIdCmd = db.GetStoredProcCommand("SP_GetCSIssueDataAccordingToCSId");

            try
            {
                db.AddInParameter(getCSIssueDataAccordingToCSIdCmd, "@CSI_id", DbType.String, csId);
                db.AddInParameter(getCSIssueDataAccordingToCSIdCmd, "@XMLCSL_Name", DbType.String, 1);
                ds = db.ExecuteDataSet(getCSIssueDataAccordingToCSIdCmd);
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stOrgName"></param>
        /// <returns></returns>
        public DataSet GetAdviserPhoneNOandEmailidAccordingToAdviserName(string stOrgName)
        {
            DataSet ds;
            Database db;
            DbCommand getAdviserPhoneNOandEmailidAccordingToAdviseerNameCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getAdviserPhoneNOandEmailidAccordingToAdviseerNameCmd = db.GetStoredProcCommand("SP_GetAdviserPhoneNOandEmailidAccordingToAdviserName");

            try
            {
                db.AddInParameter(getAdviserPhoneNOandEmailidAccordingToAdviseerNameCmd, "@A_OrgName", DbType.String, stOrgName);
                ds = db.ExecuteDataSet(getAdviserPhoneNOandEmailidAccordingToAdviseerNameCmd);
                return ds;
            }
            catch
            {
                throw;
            }
        }


        //****************START OF CODE TO GET THE DETAILS IN LEVEL 2 ON GRID LINK CLICK****************************\\
        public DataSet GetQACSIssueDataAccordingToCSId(int csId)
        {
            DataSet ds;
            Database db;
            DbCommand getCSIssueDataAccordingToCSIdCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCSIssueDataAccordingToCSIdCmd = db.GetStoredProcCommand("SP_GetQACSIssueDataAccordingToCSId");

            try
            {
                db.AddInParameter(getCSIssueDataAccordingToCSIdCmd, "@CSI_id", DbType.String, csId);
                ds = db.ExecuteDataSet(getCSIssueDataAccordingToCSIdCmd);
                return ds;
            }
            catch
            {
                throw;
            }
        }
        //****************END OF CODE TO GET THE DETAILS IN LEVEL 2 ON GRID LINK CLICK****************************\\


        //****************START OF CODE TO GET THE DETAILS IN LEVEL 3 ON GRID LINK CLICK****************************\\
        public DataSet GetDEVCSIssueDataAccordingToCSId(int csId)
           {
            DataSet ds;
            Database db;
            DbCommand getGetDEVCSIssueDataAccordingToCSIdCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getGetDEVCSIssueDataAccordingToCSIdCmd = db.GetStoredProcCommand("SP_GetDEVCSIssueDataAccordingToCSId");

            try
            {
                db.AddInParameter(getGetDEVCSIssueDataAccordingToCSIdCmd, "@CSI_id", DbType.String, csId);
                ds = db.ExecuteDataSet(getGetDEVCSIssueDataAccordingToCSIdCmd);
                return ds;
            }
            catch
            {
                throw;
            }
        }
        //****************END OF CODE TO GET THE DETAILS IN LEVEL 3 ON GRID LINK CLICK****************************\\

        public int InsertQADataLevel2(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0; 
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationQADetails;
            DbCommand cmdQACsIssueLevelAssociationInsertNew;
            

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationQADetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationQAUpdateNew");
                cmdQACsIssueLevelAssociationInsertNew = db.GetStoredProcCommand("SP_QACsIssueLevelAssociationInsertNew");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                i = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationQADetails);

                db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@CSILA_Comments", DbType.String, DBNull.Value);
                db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(cmdQACsIssueLevelAssociationInsertNew, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);

                j = db.ExecuteNonQuery(cmdQACsIssueLevelAssociationInsertNew);


            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = i + j;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int InsertQAData(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            Database db;
            DbCommand qACSIssueLevelAssociationInsertCmd;
            DbCommand QACSIssueLevelAssociationInsertNewCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            qACSIssueLevelAssociationInsertCmd = db.GetStoredProcCommand("SP_QACSIssueLevelAssociationInsert");
            QACSIssueLevelAssociationInsertNewCmd = db.GetStoredProcCommand("SP_QACsIssueLevelAssociationInsertNew");
            try
            {
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);

                i= db.ExecuteNonQuery(qACSIssueLevelAssociationInsertCmd);


                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_Comments", DbType.String, DBNull.Value);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);

                j = db.ExecuteNonQuery(QACSIssueLevelAssociationInsertNewCmd);
                
            }
            catch
            {
                throw;
            }
            i = j + k;
            return i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int InsertQADataLevel3(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            Database db;
            DbCommand qACSIssueLevelAssociationInsertCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            qACSIssueLevelAssociationInsertCmd = db.GetStoredProcCommand("SP_QACSIssueLevelAssociationInsert");
            try
            {
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(qACSIssueLevelAssociationInsertCmd, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);

                i = db.ExecuteNonQuery(qACSIssueLevelAssociationInsertCmd);
            }
            catch
            {
                throw;
            }
            return i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int InsertDEVData(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;           
            Database db;
            DbCommand dEVCSIssueLevelAssociationInsertCmd;            
            db = DatabaseFactory.CreateDatabase("wealtherp");
            dEVCSIssueLevelAssociationInsertCmd = db.GetStoredProcCommand("SP_DEVCSIssueLevelAssociationInsert");
            try
            {
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                i = db.ExecuteNonQuery(dEVCSIssueLevelAssociationInsertCmd);
            }
            catch
            {
                throw;
            }
            return i;
        }


        public int InsertDEVDataLevel2Send(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int l = 0;
            int result = 0;
            Database db;
            DbCommand dEVCSIssueLevelAssociationInsertCmd;
            DbCommand insertIssueLevelNewCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            dEVCSIssueLevelAssociationInsertCmd = db.GetStoredProcCommand("SP_DEVCSIssueLevelAssociationInsert");
            insertIssueLevelNewCmd = db.GetStoredProcCommand("SP_CsIssueLevelAssociationInsertLevel2New");
            try
            {
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(dEVCSIssueLevelAssociationInsertCmd, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                i = db.ExecuteNonQuery(dEVCSIssueLevelAssociationInsertCmd);


                db.AddInParameter(insertIssueLevelNewCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Comments", DbType.String, DBNull.Value);

                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(insertIssueLevelNewCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                l = db.ExecuteNonQuery(insertIssueLevelNewCmd);


            }
            catch
            {
                throw;
            }
            result = i + l;
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int UpdateCSIssueLevelAssociationDEVDetails(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationDEVDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationDEVDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationDEVUpdateNew");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);                
                result = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationDEVDetails);                 
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }            
            return result;
        }


        public int UpdateCSIssueLevelAssociationDEVDetailsLevel2(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationDEVDetails;
            DbCommand insertIssueLevelNewCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationDEVDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationDEVUpdateNew");
                insertIssueLevelNewCmd = db.GetStoredProcCommand("SP_CsIssueLevelAssociationInsertNew");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationDEVDetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                i = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationDEVDetails);

                db.AddInParameter(insertIssueLevelNewCmd, "@CSI_id", DbType.Int32,superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Comments", DbType.String, DBNull.Value);

                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(insertIssueLevelNewCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                j = db.ExecuteNonQuery(insertIssueLevelNewCmd);

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = i + j;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="superAdminCSIssueTrackerVo"></param>
        /// <returns></returns>
        public int UpdateCSIssueLevelAssociationCSDetails(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationCSDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationCSDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationCSUpdateNew");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                result = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationCSDetails);
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            return result;
        }


        public int UpdateCSIssueLevelAssociationDEVDetailsLevel3(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int result = 0;           
            Database db;
            DbCommand QACSIssueLevelAssociationInsertNewCmd;
            DbCommand cmdUpdateCSIssueLevelAssociationCSDetails;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            cmdUpdateCSIssueLevelAssociationCSDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationCSUpdateNew");
            QACSIssueLevelAssociationInsertNewCmd = db.GetStoredProcCommand("SP_QACsIssueLevelAssociationInsertNew");
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                j = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationCSDetails);


                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_Comments", DbType.String, DBNull.Value);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(QACSIssueLevelAssociationInsertNewCmd, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);

                i = db.ExecuteNonQuery(QACSIssueLevelAssociationInsertNewCmd);

            }
            catch
            {
                throw;
            }
            result = i + j;
            return i;
        }

        public int UpdateCSIssueLevelAssociationCSDetailsLevel2(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int j = 0;         
            Database db;
            DbCommand insertIssueLevelNewCmd;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                insertIssueLevelNewCmd = db.GetStoredProcCommand("SP_CsIssueLevelAssociationInsertNew");
                db.AddInParameter(insertIssueLevelNewCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Comments", DbType.String, DBNull.Value);

                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedBy", DbType.String, DBNull.Value);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_ActiveLevel", DbType.Int32, 1);
                db.AddInParameter(insertIssueLevelNewCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelNewCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                j = db.ExecuteNonQuery(insertIssueLevelNewCmd);
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            //result = i + j;
            return j;
        }

        
        public int UpdateCSIssueLevelAssociationQADetails(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationQADetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationQADetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationQAUpdateNew");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);

                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_RepliedDate ", DbType.DateTime, DateTime.Now);
                
                else
                    db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_RepliedDate ", DbType.DateTime, DBNull.Value);
                
                
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationQADetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                result = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationQADetails);
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            return result;
        }


        //*****************START OF CODE FOR CLOSING THE ISSUE AND SETTING THE STATUS TO CLOSE***************************\\
        public int CloseIssue(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int result = 0;
            Database db;
            DbCommand cmdCloseIssue;
            DbCommand cmdUpdateCSIssueLevelAssociationCSDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCloseIssue = db.GetStoredProcCommand("SP_CloseIssue");
                cmdUpdateCSIssueLevelAssociationCSDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationCSUpdateNew");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Version",DbType.Int32 ,DBNull.Value);
                j = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationCSDetails);

                db.AddInParameter(cmdCloseIssue, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdCloseIssue, "@CSI_ResolvedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ResolvedDate);
                db.AddInParameter(cmdCloseIssue, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code); 
                i = db.ExecuteNonQuery(cmdCloseIssue);
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = i + j;
            return result;
        }
        //*****************END OF CODE FOR CLOSING THE ISSUE AND SETTING THE STATUS TO CLOSE***************************\\



        //*****************START OF CODE FOR GETTING SEARCH DETAILS IN THE GRID****************************\\
        public DataSet GetSearchDetails(string strSearch)
        {
            IssueTrackerVo superAdminCSIssueTrackerVo = new IssueTrackerVo();
            DataSet ds;
            Database db;
            DbCommand getSearchDetailsCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            getSearchDetailsCmd = db.GetStoredProcCommand("SP_GetSearchDetails");

            try
            {
                db.AddInParameter(getSearchDetailsCmd, "@Search", DbType.String, strSearch);
                ds = db.ExecuteDataSet(getSearchDetailsCmd);
                return ds;
            }
            catch
            {
                throw;
            }
        }
        //*****************END OF CODE FOR GETTING SEARCH DETAILS IN THE GRID****************************\\

        //**********NEW CODE FOR SENDING FROM LEVEL1 TO ANY LEVEL ON SUBMIT BUTTON CLICK***********\\
        public int InsertIntoCSIssueLevel1ToLevel1(IssueTrackerVo superAdminCSIssueTrackerVo)
        {     
            int j = 0;
            int k = 0;
            int l = 0;
            int result = 0;
            Database db;
            DbCommand insertcsissueDetailsCmd;
            DbCommand insertIssueLevelCmd;
            DbCommand InsertIntoCSIssueActiveLevelCmd; 
            db = DatabaseFactory.CreateDatabase("wealtherp");
            insertcsissueDetailsCmd = db.GetStoredProcCommand("SP_CSIssueInsert");
            insertIssueLevelCmd = db.GetStoredProcCommand("SP_InsertIntoCSIssueLevelAssociation");
            InsertIntoCSIssueActiveLevelCmd = db.GetStoredProcCommand("SP_InsertIntoCSIssueActiveLevel");

            try
            {
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Code", DbType.String, superAdminCSIssueTrackerVo.CSI_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@A_AdviserId", DbType.Int32, superAdminCSIssueTrackerVo.A_AdviserId);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ContactPerson", DbType.String, superAdminCSIssueTrackerVo.CSI_ContactPerson);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Phone", DbType.String, superAdminCSIssueTrackerVo.CSI_Phone);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Email", DbType.String, superAdminCSIssueTrackerVo.CSI_Email);
                db.AddInParameter(insertcsissueDetailsCmd, "@UR_RoleId", DbType.Int32, superAdminCSIssueTrackerVo.UR_RoleId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTN_TreeNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTN_TreeNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTSN_TreeSubNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTSN_TreeSubNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@WTSSN_TreeSubSubNodeId", DbType.Int32, superAdminCSIssueTrackerVo.WTSSN_TreeSubSubNodeId);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_CustomerDescription", DbType.String, superAdminCSIssueTrackerVo.CSI_CustomerDescription);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_issueAddedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_issueAddedDate);

                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_issueAddedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedVia", DbType.String, superAdminCSIssueTrackerVo.CSI_ReportedVia);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_CustomerSupportComments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(insertcsissueDetailsCmd, "@CSI_Atuhor", DbType.String, superAdminCSIssueTrackerVo.CSI_Atuhor);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);

                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ReportedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSP_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSP_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCSS_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSS_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLCST_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCST_Code);
                db.AddInParameter(insertcsissueDetailsCmd, "@XMLACSP_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLACSP_Code);
                if (superAdminCSIssueTrackerVo.CSI_ResolvedDate != DateTime.MinValue)
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ResolvedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ResolvedDate);
                else
                    db.AddInParameter(insertcsissueDetailsCmd, "@CSI_ResolvedDate", DbType.DateTime, DBNull.Value);
                db.AddOutParameter(insertcsissueDetailsCmd, "@CSI_id", DbType.Int32, 1);

                j = db.ExecuteNonQuery(insertcsissueDetailsCmd);

                int csId = Convert.ToChar(db.GetParameterValue(insertcsissueDetailsCmd, "@CSI_id"));

                db.AddInParameter(insertIssueLevelCmd, "@CSI_id", DbType.Int32, csId);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);

                db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSI_Atuhor);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(insertIssueLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                k = db.ExecuteNonQuery(insertIssueLevelCmd);

                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@CSI_id", DbType.String, csId);
                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);

                l = db.ExecuteNonQuery(InsertIntoCSIssueActiveLevelCmd);

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result=j+k+l;
            return result;
        }
        //**********END OF NEW CODE FOR SENDING FROM LEVEL1 TO ANY LEVEL ON SUBMIT BUTTON CLICK***********\\

        //**********NEW CODE FOR SENDING FROM LEVEL2 TO ANY LEVEL ON SUBMIT BUTTON CLICK***********\\
        public int InsertIntoCSIssueLevel2ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int k = 0;
            int l = 0;
            int result = 0;
            Database db;
            DbCommand insertIssueLevelCmd;
            DbCommand InsertIntoCSIssueActiveLevelCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            insertIssueLevelCmd = db.GetStoredProcCommand("SP_InsertIntoCSIssueLevel2ToAnyLevel");
            InsertIntoCSIssueActiveLevelCmd = db.GetStoredProcCommand("SP_InsertIntoCSIssueActiveLevel2ToAnyLevel");

            try
            {
                db.AddInParameter(insertIssueLevelCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);

                db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                db.AddInParameter(insertIssueLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);

                k = db.ExecuteNonQuery(insertIssueLevelCmd);

                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@CSI_id", DbType.String, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);

                l = db.ExecuteNonQuery(InsertIntoCSIssueActiveLevelCmd);

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = k + l;
            return result;
        }
        //**********END OF NEW CODE FOR SENDING FROM LEVEL2 TO ANY LEVEL ON SUBMIT BUTTON CLICK***********\\

        //**********NEW CODE FOR SENDING FROM LEVEL3 TO ANY LEVEL ON SUBMIT BUTTON CLICK***********\\
        public int InsertIntoCSIssueLevel3ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int k = 0;
            int l = 0;
            int result = 0;
            Database db;
            DbCommand insertIssueLevelCmd;
            DbCommand InsertIntoCSIssueActiveLevelCmd;
            db = DatabaseFactory.CreateDatabase("wealtherp");
            insertIssueLevelCmd = db.GetStoredProcCommand("SP_InsertIntoCSIssueLevel3ToAnyLevel");
            InsertIntoCSIssueActiveLevelCmd = db.GetStoredProcCommand("SP_InsertIntoCSIssueActiveLevel3ToAnyLevel");

            try
            {
                db.AddInParameter(insertIssueLevelCmd, "@CSI_id", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Comments", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);

                db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                if (superAdminCSIssueTrackerVo.CSI_ReportedDate != DateTime.MinValue)
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                else
                    db.AddInParameter(insertIssueLevelCmd, "@CSILA_RepliedDate", DbType.DateTime, superAdminCSIssueTrackerVo.CSI_ReportedDate);
                db.AddInParameter(insertIssueLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(insertIssueLevelCmd, "@CSILA_Version", DbType.String, DBNull.Value);

                k = db.ExecuteNonQuery(insertIssueLevelCmd);

                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@CSI_id", DbType.String, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);

                l = db.ExecuteNonQuery(InsertIntoCSIssueActiveLevelCmd);

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = k + l;
            return result;
        }
        //**********END OF NEW CODE FOR SENDING FROM LEVEL3 TO ANY LEVEL ON SUBMIT BUTTON CLICK***********\\                

        //**********NEW CODE FOR SENDING FROM LEVEL1 TO ANY LEVEL ON UPDATE BUTTON CLICK***********\\
        public int UpdateCSIssueLevelAssociationLevel1ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationCSDetails;
            DbCommand InsertIntoCSIssueActiveLevelCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationCSDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationCSUpdateNew");
                InsertIntoCSIssueActiveLevelCmd = db.GetStoredProcCommand("SP_UpdateCSIssueActiveLevel1ToAnyLevel");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Version", DbType.Int32,DBNull.Value);
                i = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationCSDetails);

                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@CSI_id", DbType.String, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);

                j = db.ExecuteNonQuery(InsertIntoCSIssueActiveLevelCmd);


            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = i + j;
            return result;
        }
        //**********END OF NEW CODE FOR SENDING FROM LEVEL1 TO ANY LEVEL ON UPDATE BUTTON CLICK***********\\        

        //**********NEW CODE FOR SENDING FROM LEVEL2 TO ANY LEVEL ON UPDATE BUTTON CLICK***********\\
        public int UpdateCSIssueLevelAssociationLevel2ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationCSDetails;
            DbCommand InsertIntoCSIssueActiveLevelCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationCSDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationLevel2ToAnyLevelOnUpdate");
                InsertIntoCSIssueActiveLevelCmd = db.GetStoredProcCommand("SP_UpdateCSIssueActiveLevel2ToAnyLevelOnUpdate");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                i = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationCSDetails);

                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@CSI_id", DbType.String, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);

                j = db.ExecuteNonQuery(InsertIntoCSIssueActiveLevelCmd);


            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = i + j;
            return result;
        }
        
        //**********END OF NEW CODE FOR SENDING FROM LEVEL2 TO ANY LEVEL ON UPDATE BUTTON CLICK***********\\                

        //**********NEW CODE FOR SENDING FROM LEVEL3 TO ANY LEVEL ON UPDATE BUTTON CLICK***********\\
        public int UpdateCSIssueLevelAssociationLevel3ToAnyLevel(IssueTrackerVo superAdminCSIssueTrackerVo)
        {
            int i = 0;
            int j = 0;
            int result = 0;
            Database db;
            DbCommand cmdUpdateCSIssueLevelAssociationCSDetails;
            DbCommand InsertIntoCSIssueActiveLevelCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCSIssueLevelAssociationCSDetails = db.GetStoredProcCommand("SP_CsIssueLevelAssociationLevel3ToAnyLevelOnUpdate");
                InsertIntoCSIssueActiveLevelCmd = db.GetStoredProcCommand("SP_UpdateCSIssueActiveLevel3ToAnyLevelOnUpdate");

                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSI_id ", DbType.Int32, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Comments ", DbType.String, superAdminCSIssueTrackerVo.CSILA_Comments);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedBy", DbType.String, superAdminCSIssueTrackerVo.CSILA_RepliedBy);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_RepliedDate ", DbType.DateTime, superAdminCSIssueTrackerVo.CSILA_RepliedDate);
                db.AddInParameter(cmdUpdateCSIssueLevelAssociationCSDetails, "@CSILA_Version", DbType.String, superAdminCSIssueTrackerVo.CSILA_Version);
                i = db.ExecuteNonQuery(cmdUpdateCSIssueLevelAssociationCSDetails);

                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@CSI_id", DbType.String, superAdminCSIssueTrackerVo.CSI_id);
                db.AddInParameter(InsertIntoCSIssueActiveLevelCmd, "@XMLCSL_Code", DbType.Int32, superAdminCSIssueTrackerVo.XMLCSL_Code);

                j = db.ExecuteNonQuery(InsertIntoCSIssueActiveLevelCmd);


            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            result = i + j;
            return result;
        }
        //**********END OF NEW CODE FOR SENDING FROM LEVEL3 TO ANY LEVEL ON UPDATE BUTTON CLICK***********\\                
    }
}
