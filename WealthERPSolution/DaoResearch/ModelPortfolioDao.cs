using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Sql;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoResearch;

namespace DaoResearch
{
    public class ModelPortfolioDao
    {
        public DataTable GetAMCList()
        {
            Database db;
            DbCommand cmdAMCList;
            DataTable dtAMCList;
            DataSet dsAMCList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdAMCList = db.GetStoredProcCommand("SP_GetMutualFundList");
                dsAMCList = db.ExecuteDataSet(cmdAMCList);
                dtAMCList = dsAMCList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtAMCList;
        }

        public DataTable GetModelPortfolioName(int advisorId)
        {
            Database db;
            DbCommand cmdModelPortfolioName;
            DataTable dtModelPortfolioName;
            DataSet dsModelPortfolioName = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdModelPortfolioName = db.GetStoredProcCommand("SP_GetModelPortFolioName");
                db.AddInParameter(cmdModelPortfolioName, "@adviser_Id", DbType.Int32, advisorId);
                //db.AddInParameter(cmdModelPortfolioName, "@amptbv_Id", DbType.Int32, AMPTBValueId);
                dsModelPortfolioName = db.ExecuteDataSet(cmdModelPortfolioName);
                dtModelPortfolioName = dsModelPortfolioName.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtModelPortfolioName;
        }

        public DataSet BindddlMFSubCategory()
        {
            Database db;
            DbCommand cmdGetAllSubCategory;
            DataSet dsGetAllSubCategory = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAllSubCategory = db.GetStoredProcCommand("SP_BindMFSubCategoryddl");
                dsGetAllSubCategory = db.ExecuteDataSet(cmdGetAllSubCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAllSubCategory;
        }

        public DataSet GetSchemeListSubCategory(int amcCode, string categoryCode, string subCategory)
        {

            DataSet dsSchemeListCategorySubCategory = new DataSet();
            Database db;
            DbCommand CmdSchemeList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdSchemeList = db.GetStoredProcCommand("SP_GetSchemeNameOnCategorySubCategory");
                db.AddInParameter(CmdSchemeList, "@AmcCode", DbType.Int32, amcCode);
                db.AddInParameter(CmdSchemeList, "@CategoryCode", DbType.String, categoryCode);
                db.AddInParameter(CmdSchemeList, "@SubCategory", DbType.String, subCategory);
                CmdSchemeList.CommandTimeout = 60 * 60;
                dsSchemeListCategorySubCategory = db.ExecuteDataSet(CmdSchemeList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSchemeListCategorySubCategory;
        }

        public DataTable GetBasisList()
        {
            Database db;
            DbCommand cmdBasisList;
            DataTable dtBasisList;
            DataSet dsBasisList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdBasisList = db.GetStoredProcCommand("SP_GetBasisList");
                dsBasisList = db.ExecuteDataSet(cmdBasisList);
                dtBasisList = dsBasisList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtBasisList;
        }

        public DataTable GetArchiveReason()
        {
            Database db;
            DbCommand cmdArchiveReason;
            DataTable dtArchiveReason;
            DataSet dsArchiveReason = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdArchiveReason = db.GetStoredProcCommand("SP_GetArchiveReason");
                dsArchiveReason = db.ExecuteDataSet(cmdArchiveReason);
                dtArchiveReason = dsArchiveReason.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtArchiveReason;
        }

        public DataSet GetScoreRange(int advisorId)
        {
            Database db;
            DbCommand cmdScoreRange;
            DataSet dsScoreRange = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdScoreRange = db.GetStoredProcCommand("SP_GetScoreRange");
                db.AddInParameter(cmdScoreRange, "@advisorId", DbType.Int32, advisorId);
                dsScoreRange = db.ExecuteDataSet(cmdScoreRange);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:GetScoreRange()");
                object[] objects = new object[1];
                objects[0] = advisorId;                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsScoreRange;
        }

        public bool InsertModelPortfolioDetails(ModelPortfolioVo modelPortfolioVo, int adviserId)
        {
            bool bResult = false;
            Database db;
            DbCommand modelPortfolioCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                modelPortfolioCmd = db.GetStoredProcCommand("SP_InsertModelPortfolioDetails");
                db.AddInParameter(modelPortfolioCmd, "@portfolioName", DbType.String, modelPortfolioVo.PortfolioName);
                db.AddInParameter(modelPortfolioCmd, "@description", DbType.String, modelPortfolioVo.Description);
                db.AddInParameter(modelPortfolioCmd, "@lowerScore", DbType.Int32, modelPortfolioVo.LowerScore);
                db.AddInParameter(modelPortfolioCmd, "@upperScore", DbType.Int32, modelPortfolioVo.UpperScore);
                db.AddInParameter(modelPortfolioCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(modelPortfolioCmd, "@basisId", DbType.String, modelPortfolioVo.BasisId);
                db.AddOutParameter(modelPortfolioCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioVo.ModelPortfolioCode);
                db.AddOutParameter(modelPortfolioCmd, "@AMPBU_Id", DbType.Int32, modelPortfolioVo.AMPBU_Id);
                if (modelPortfolioVo.RiskClassCode != null)
                    db.AddInParameter(modelPortfolioCmd, "@riskClassCode", DbType.String, modelPortfolioVo.RiskClassCode);
                else
                    db.AddInParameter(modelPortfolioCmd, "@riskClassCode", DbType.String, DBNull.Value);
                if (db.ExecuteNonQuery(modelPortfolioCmd) != 0)
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

                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:InsertModelPortfolioDetails()");
                object[] objects = new object[1];
                objects[0] = modelPortfolioVo;
                objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataTable GetModelportfolioDetails(int adviserId)
        {
            DataTable dtModelPortfolio;
            DataSet dsModelPortFolio;
            Database db;
            DbCommand modelPortfolioCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                modelPortfolioCmd = db.GetStoredProcCommand("SP_GetModelPortfolioDetails");
                db.AddInParameter(modelPortfolioCmd, "@adviserId", DbType.Int32, adviserId);
                dsModelPortFolio = db.ExecuteDataSet(modelPortfolioCmd);
                dtModelPortfolio = dsModelPortFolio.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:GetModelportfolioDetails()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtModelPortfolio;
        }

        public DataTable GetDefaultAdviserRiskClasses()
        {
            DataTable dtModelPortfolio;
            DataSet dsModelPortFolio;
            Database db;
            DbCommand modelPortfolioCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                modelPortfolioCmd = db.GetStoredProcCommand("SP_GetDefaultAdviserRiskClass");
                //db.AddInParameter(modelPortfolioCmd, "@adviserId", DbType.Int32, adviserId);
                dsModelPortFolio = db.ExecuteDataSet(modelPortfolioCmd);
                dtModelPortfolio = dsModelPortFolio.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtModelPortfolio;
        }

        public void DeleteSchemeFromModelPortfolio(int modelPortfolioCode, int adviserId)
        {
            Database db;
            DbCommand deleteRecordsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteRecordsCmd = db.GetStoredProcCommand("SP_DeleteSchemeToModelPortfolio");
                db.AddInParameter(deleteRecordsCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioCode);
                db.AddInParameter(deleteRecordsCmd, "@adviserId", DbType.Int32, adviserId);
                db.ExecuteDataSet(deleteRecordsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:DeleteSchemeFromModelPortfolio()");

                object[] objects = new object[1];
                objects[0] = modelPortfolioCode;
                objects[1] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void DeleteModelPortfolioRecords(int AMPTBValueId)
        {
            Database db;
            DbCommand deleteRecordsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteRecordsCmd = db.GetStoredProcCommand("SP_DeleteModelPortfolio");
                db.AddInParameter(deleteRecordsCmd, "@amptbv_Id", DbType.Int32, AMPTBValueId);
                db.ExecuteDataSet(deleteRecordsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:DeleteModelPortfolioRecords()");

                object[] objects = new object[1];
                objects[0] = AMPTBValueId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public void EditModelPortfolioRecords(ModelPortfolioVo modelPortfolioVo, int AMPTBValueId)
        {
            Database db;
            DbCommand EditRecordsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                EditRecordsCmd = db.GetStoredProcCommand("SP_EditModelPortfolioDetails");
                db.AddInParameter(EditRecordsCmd, "@amptbv_Id", DbType.Int32, AMPTBValueId);
                db.AddInParameter(EditRecordsCmd, "@portfolioName", DbType.String, modelPortfolioVo.PortfolioName);
                db.AddInParameter(EditRecordsCmd, "@description", DbType.String, modelPortfolioVo.Description);
                db.AddInParameter(EditRecordsCmd, "@lowerScore", DbType.Int32, modelPortfolioVo.LowerScore);
                db.AddInParameter(EditRecordsCmd, "@upperScore", DbType.Int32, modelPortfolioVo.UpperScore);                
                db.AddOutParameter(EditRecordsCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioVo.ModelPortfolioCode);                
                if (modelPortfolioVo.RiskClassCode != null)
                    db.AddInParameter(EditRecordsCmd, "@riskClassCode", DbType.String, modelPortfolioVo.RiskClassCode);
                else
                    db.AddInParameter(EditRecordsCmd, "@riskClassCode", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(EditRecordsCmd);                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:EditModelPortfolioRecords()");

                object[] objects = new object[1];
                objects[0] = modelPortfolioVo;
                objects[1] = AMPTBValueId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public bool AttachSchemeToPortfolio(ModelPortfolioVo modelPortfolioVo, int adviserId, int IsActiveFlag)
        {
            bool bResult = false;
            Database db;
            DbCommand modelPortfolioCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                modelPortfolioCmd = db.GetStoredProcCommand("SP_InsertSchemeToModelPortfolio");
                db.AddInParameter(modelPortfolioCmd, "@schemeCode", DbType.Int32, modelPortfolioVo.SchemeCode);
                db.AddInParameter(modelPortfolioCmd, "@weightage", DbType.Int32, modelPortfolioVo.Weightage);
                db.AddInParameter(modelPortfolioCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioVo.ModelPortfolioCode);
                db.AddInParameter(modelPortfolioCmd, "@IsActiveflag", DbType.Int32, IsActiveFlag);
                db.AddInParameter(modelPortfolioCmd, "@schemeDescription", DbType.String, modelPortfolioVo.SchemeDescription);
                db.AddInParameter(modelPortfolioCmd, "@adviserId", DbType.Int32, adviserId);
                if (modelPortfolioVo.ArchiveReason != 0)
                    db.AddInParameter(modelPortfolioCmd, "@archiveReason", DbType.Int32, modelPortfolioVo.ArchiveReason);
                else
                    db.AddInParameter(modelPortfolioCmd, "@archiveReason", DbType.Int32, 0);
                if (db.ExecuteNonQuery(modelPortfolioCmd) != 0)
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
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:AttachSchemeToPortfolio()");

                object[] objects = new object[2];
                objects[0] = modelPortfolioVo;
                objects[1] = adviserId;
                objects[2] = IsActiveFlag;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public DataTable GetAttachedSchemeDetails(int modelPortfolioCode, int adviserId)
        {
            DataTable dtAttachedScheme;
            DataSet dsAttachedScheme;
            Database db;
            DbCommand AttachedSchemeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AttachedSchemeCmd = db.GetStoredProcCommand("SP_GetAttachedSchemeDetails");
                db.AddInParameter(AttachedSchemeCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(AttachedSchemeCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioCode);
                dsAttachedScheme = db.ExecuteDataSet(AttachedSchemeCmd);
                dtAttachedScheme = dsAttachedScheme.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:GetAttachedSchemeDetails()");
                object[] objects = new object[1];
                objects[0] = modelPortfolioCode;
                objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAttachedScheme;
        }

        public DataTable getAllocationPercentageFromModelPortFolio(int modelPortfolioCode)
        {
            DataTable dtAttachedScheme;
            DataSet dsAttachedScheme;
            Database db;
            DbCommand AttachedSchemeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AttachedSchemeCmd = db.GetStoredProcCommand("SP_GetAllocatedPercentageOfModelPortfolio");
                //db.AddInParameter(AttachedSchemeCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(AttachedSchemeCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioCode);
                dsAttachedScheme = db.ExecuteDataSet(AttachedSchemeCmd);
                dtAttachedScheme = dsAttachedScheme.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:getAllocationPercentageFromModelPortFolio()");
                object[] objects = new object[1];
                objects[0] = modelPortfolioCode;
                //objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAttachedScheme;
        }

        public void ArchiveSchemeFromModelPortfolio(int AMFMPD_Id, int xarId)
        {
            Database db;
            DbCommand ArchiveRecordsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                ArchiveRecordsCmd = db.GetStoredProcCommand("SP_ArchiveAttachedScheme");
                db.AddInParameter(ArchiveRecordsCmd, "@amfmpd_Id", DbType.Int32, AMFMPD_Id);
                db.AddInParameter(ArchiveRecordsCmd, "@xarId", DbType.Int32, xarId);
                db.ExecuteDataSet(ArchiveRecordsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:ArchiveSchemeFromModelPortfolio()");

                object[] objects = new object[1];
                objects[0] = AMFMPD_Id;
                objects[1] = xarId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataTable GetArchivedSchemeDetails(int modelPortfolioCode, int adviserId)
        {
            DataTable dtAttachedScheme;
            DataSet dsAttachedScheme;
            Database db;
            DbCommand AttachedSchemeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AttachedSchemeCmd = db.GetStoredProcCommand("SP_GetArchivedSchemeDetails");
                db.AddInParameter(AttachedSchemeCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(AttachedSchemeCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioCode);
                dsAttachedScheme = db.ExecuteDataSet(AttachedSchemeCmd);
                dtAttachedScheme = dsAttachedScheme.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:GetArchivedSchemeDetails()");
                object[] objects = new object[1];
                objects[0] = modelPortfolioCode;
                objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAttachedScheme;
        }

        public DataTable GetSchemeAssetAllocation(string schemePlanCode)
        {
            DataTable dtSchemeAsset;
            DataSet dsSchemeAsset;
            Database db;
            DbCommand SchemeAssetCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SchemeAssetCmd = db.GetStoredProcCommand("SP_AttachedSchemeAssetAllocation");
                //db.AddInParameter(SchemeAssetCmd, "@adviserId", DbType.Int32, adviserId);
                //db.AddInParameter(SchemeAssetCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioVo.ModelPortfolioCode);
                db.AddInParameter(SchemeAssetCmd, "@schemePlanCode", DbType.String, schemePlanCode);
                dsSchemeAsset = db.ExecuteDataSet(SchemeAssetCmd);
                dtSchemeAsset = dsSchemeAsset.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:GetSchemeAssetAllocation()");
                object[] objects = new object[1];
                objects[0] = schemePlanCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeAsset;
        }

        public DataTable SchemeAssetChartOnSubCategory(ModelPortfolioVo modelPortfolioVo, int adviserId)
        {
            DataTable dtSchemeAsset;
            DataSet dsSchemeAsset;
            Database db;
            DbCommand SchemeAssetCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SchemeAssetCmd = db.GetStoredProcCommand("SP_AttachedSchemeAssetOnSubCategory");
                db.AddInParameter(SchemeAssetCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(SchemeAssetCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioVo.ModelPortfolioCode);
                dsSchemeAsset = db.ExecuteDataSet(SchemeAssetCmd);
                dtSchemeAsset = dsSchemeAsset.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:SchemeAssetChartOnSubCategory()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeAsset;
        }

        public DataTable GetRiskGoalClassData(int adviserId, int isRiskClass)
        {
            DataTable dtRiskClass;
            DataSet dsRiskClass;
            Database db;
            DbCommand RiskClassCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                RiskClassCmd = db.GetStoredProcCommand("SP_GetRiskGoalClassData");
                db.AddInParameter(RiskClassCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(RiskClassCmd, "@isRiskClass", DbType.Int32, isRiskClass);
                //db.AddInParameter(RiskClassCmd, "@classCode", DbType.String, modelPortfolioVo.RiskClassCode);
                //if(var != "")
                //db.AddInParameter(RiskClassCmd, "@var", DbType.Int16, var);
                //else
                //    db.AddInParameter(RiskClassCmd, "@var", DbType.Int16, DBNull.Value);
                dsRiskClass = db.ExecuteDataSet(RiskClassCmd);
                dtRiskClass = dsRiskClass.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:GetRiskGoalClassData()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtRiskClass;
        }

        public DataTable InsertUpdateRiskGoalClass(string value, string Text, int advisorId)
        {
            DataTable dtRiskClass;
            DataSet dsRiskClass;
            Database db;
            DbCommand RiskClassCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                RiskClassCmd = db.GetStoredProcCommand("SP_InsertUpdateRiskGoalClass");
                db.AddInParameter(RiskClassCmd, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(RiskClassCmd, "@description", DbType.String, Text);
                db.AddInParameter(RiskClassCmd, "@classCode", DbType.String, value);
                
                dsRiskClass = db.ExecuteDataSet(RiskClassCmd);
                dtRiskClass = dsRiskClass.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:InsertUpdateRiskGoalClass()");
                object[] objects = new object[2];
                objects[0] = value;
                objects[1] = Text;
                objects[2] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtRiskClass;
        }

        public void DeleteRiskClass(string riskCode, int advisorId)
        {
            Database db;
            DbCommand deleteRecordsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteRecordsCmd = db.GetStoredProcCommand("SP_DeleteRiskGoalClass");
                db.AddInParameter(deleteRecordsCmd, "@classCode", DbType.String, riskCode);
                db.AddInParameter(deleteRecordsCmd, "@adviserId", DbType.Int32, advisorId); 
                db.ExecuteDataSet(deleteRecordsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:DeleteRiskClass()");

                object[] objects = new object[1];
                objects[0] = riskCode;
                objects[0] = advisorId;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataSet bindDdlPickRiskClass(int adviserId, int isRiskClass)
        {
            Database db;
            DbCommand getAdviserRiskClassesCmd;
            DataSet dsAdviserRiskClasses = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserRiskClassesCmd = db.GetStoredProcCommand("SP_GetAdviserRiskClass");
                db.AddInParameter(getAdviserRiskClassesCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getAdviserRiskClassesCmd, "@isRiskClass", DbType.Int32, isRiskClass);
                dsAdviserRiskClasses = db.ExecuteDataSet(getAdviserRiskClassesCmd);                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:bindDdlPickRiskClass()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserRiskClasses;
        }
//********************************************** Code for Asset Allocation Starts***********************************************************


        public bool CreateVariantAssetPortfolio(ModelPortfolioVo modelPortfolioVo, int adviserId, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand AttachedVariantCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AttachedVariantCmd = db.GetStoredProcCommand("SP_InsertVariantAssetToModelPortfolio");
                db.AddInParameter(AttachedVariantCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(AttachedVariantCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioVo.ModelPortfolioCode);
                db.AddInParameter(AttachedVariantCmd, "@PortfolioName", DbType.String, modelPortfolioVo.PortfolioName);
                db.AddInParameter(AttachedVariantCmd, "@MinAge", DbType.Int32, modelPortfolioVo.MinAge);
                db.AddInParameter(AttachedVariantCmd, "@MaxAge", DbType.Int32, modelPortfolioVo.MaxAge);
                db.AddInParameter(AttachedVariantCmd, "@MinTimeHorizon", DbType.Int32, modelPortfolioVo.MinTimeHorizon);
                db.AddInParameter(AttachedVariantCmd, "@MaxTimeHorizon", DbType.Int32, modelPortfolioVo.MaxTimeHorizon);
                db.AddInParameter(AttachedVariantCmd, "@MinAUM", DbType.Double, modelPortfolioVo.MinAUM);                
                db.AddInParameter(AttachedVariantCmd, "@MaxAUM", DbType.Double, modelPortfolioVo.MaxAUM);                
                db.AddInParameter(AttachedVariantCmd, "@VariantDescription", DbType.String, modelPortfolioVo.VariantDescription);
                db.AddInParameter(AttachedVariantCmd, "@RiskClassCode", DbType.String, modelPortfolioVo.RiskClassCode);


                db.AddInParameter(AttachedVariantCmd, "@CashAllocation", DbType.Decimal, modelPortfolioVo.CashAllocation);
                db.AddInParameter(AttachedVariantCmd, "@DebtAllocation", DbType.Decimal, modelPortfolioVo.DebtAllocation);
                db.AddInParameter(AttachedVariantCmd, "@EquityAllocation", DbType.Decimal, modelPortfolioVo.EquityAllocation);
                db.AddInParameter(AttachedVariantCmd, "@AlternateAllocation", DbType.Decimal, modelPortfolioVo.AlternateAllocation);

                db.AddInParameter(AttachedVariantCmd, "@ROR", DbType.Decimal, modelPortfolioVo.ROR);
                db.AddInParameter(AttachedVariantCmd, "@RiskPercentage", DbType.Decimal, modelPortfolioVo.RiskPercentage);

                db.AddInParameter(AttachedVariantCmd, "@userId", DbType.Int32, userId);                
                if (db.ExecuteNonQuery(AttachedVariantCmd) != 0)
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
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:CreateVariantAssetPortfolio()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool UpdateVariantAssetPortfolio(ModelPortfolioVo modelPortfolioVo, int adviserId, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand AttachedVariantCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AttachedVariantCmd = db.GetStoredProcCommand("SP_UpdateVariantAssetToModelPortfolio");
                db.AddInParameter(AttachedVariantCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(AttachedVariantCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioVo.ModelPortfolioCode);
                db.AddInParameter(AttachedVariantCmd, "@PortfolioName", DbType.String, modelPortfolioVo.PortfolioName);
                db.AddInParameter(AttachedVariantCmd, "@MinAge", DbType.Int32, modelPortfolioVo.MinAge);
                db.AddInParameter(AttachedVariantCmd, "@MaxAge", DbType.Int32, modelPortfolioVo.MaxAge);
                db.AddInParameter(AttachedVariantCmd, "@MinTimeHorizon", DbType.Int32, modelPortfolioVo.MinTimeHorizon);
                db.AddInParameter(AttachedVariantCmd, "@MaxTimeHorizon", DbType.Int32, modelPortfolioVo.MaxTimeHorizon);
                db.AddInParameter(AttachedVariantCmd, "@MinAUM", DbType.Double, modelPortfolioVo.MinAUM);
                db.AddInParameter(AttachedVariantCmd, "@MaxAUM", DbType.Double, modelPortfolioVo.MaxAUM);
                db.AddInParameter(AttachedVariantCmd, "@VariantDescription", DbType.String, modelPortfolioVo.VariantDescription);
                db.AddInParameter(AttachedVariantCmd, "@RiskClassCode", DbType.String, modelPortfolioVo.RiskClassCode);

                db.AddInParameter(AttachedVariantCmd, "@CashAllocation", DbType.Decimal, modelPortfolioVo.CashAllocation);
                db.AddInParameter(AttachedVariantCmd, "@DebtAllocation", DbType.Decimal, modelPortfolioVo.DebtAllocation);
                db.AddInParameter(AttachedVariantCmd, "@EquityAllocation", DbType.Decimal, modelPortfolioVo.EquityAllocation);
                db.AddInParameter(AttachedVariantCmd, "@AlternateAllocation", DbType.Decimal, modelPortfolioVo.AlternateAllocation);

                db.AddInParameter(AttachedVariantCmd, "@ROR", DbType.Decimal, modelPortfolioVo.ROR);
                db.AddInParameter(AttachedVariantCmd, "@RiskPercentage", DbType.Decimal, modelPortfolioVo.RiskPercentage);

                db.AddInParameter(AttachedVariantCmd, "@userId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(AttachedVariantCmd) != 0)
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
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:UpdateVariantAssetPortfolio()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataSet GetVariantAssetPortfolioDetails(int advisorId)
        {            
            DataSet ds;
            Database db;
            DbCommand AttachedVariantCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AttachedVariantCmd = db.GetStoredProcCommand("SP_GetVariantAssetPortfolioDetails");
                //db.AddInParameter(AttachedVariantCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(AttachedVariantCmd, "@adviserId", DbType.Int32, advisorId);
                ds = db.ExecuteDataSet(AttachedVariantCmd);                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:GetVariantAssetPortfolioDetails()");
                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
       
        public void DeleteVariantAssetPortfolio(int modelPortfolioCode)
        {
            Database db;
            DbCommand deleteRecordsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteRecordsCmd = db.GetStoredProcCommand("SP_DeleteVariantAssetPortfolio");
                db.AddInParameter(deleteRecordsCmd, "@modelPortfolioCode", DbType.Int32, modelPortfolioCode);                
                db.ExecuteDataSet(deleteRecordsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioDao.cs:DeleteVariantAssetPortfolio()");

                object[] objects = new object[1];
                objects[0] = modelPortfolioCode;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

//********************************************** Code for Asset Allocation Ends***********************************************************

        public DataSet GetGoalModelPortFolioAttachedSchemes(int customerId,int adviserId,int goalId)
        {
            Database db;
            DbCommand getGoalModelPortFolioAttachedSchemesCmd;
            DataTable dtModelPortFolioSchemeDetails = new DataTable();
            DataSet dsModelPortFolioSchemeDetails = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGoalModelPortFolioAttachedSchemesCmd = db.GetStoredProcCommand("SP_GetGoalModelPortFolioAttachedSchemes");
                db.AddInParameter(getGoalModelPortFolioAttachedSchemesCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(getGoalModelPortFolioAttachedSchemesCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getGoalModelPortFolioAttachedSchemesCmd, "@goalId", DbType.Int32, goalId);
                dsModelPortFolioSchemeDetails = db.ExecuteDataSet(getGoalModelPortFolioAttachedSchemesCmd);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsModelPortFolioSchemeDetails;
        }
    }
}
