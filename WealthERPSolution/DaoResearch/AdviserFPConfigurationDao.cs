using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using VoCustomerRiskProfiling;
using System.Collections.Specialized;

namespace DaoResearch
{
    public class AdviserFPConfigurationDao
    {

        //***********Assumption START****************************************************************
        public DataSet GetAdviserAssumptions(int adviserid)
        {
            Database db;
            DbCommand cmdAdviserAssumptions;
            DataSet dsAdviserAssumptions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdAdviserAssumptions = db.GetStoredProcCommand("SP_GetAdviserAssumptions");
                db.AddInParameter(cmdAdviserAssumptions, "@adviserId", DbType.Int32, adviserid);
                dsAdviserAssumptions = db.ExecuteDataSet(cmdAdviserAssumptions);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserAssumptions;
        }

        public void UpdateAdviserAssumptions(int adviserId, decimal value, string assumptionId)
        {
            Database db;
            DbCommand cmdAdviserAssumptions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdAdviserAssumptions = db.GetStoredProcCommand("SP_UpdateAdviserAssumptions");
                db.AddInParameter(cmdAdviserAssumptions, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdAdviserAssumptions, "@assumptionId", DbType.String, assumptionId);
                db.AddInParameter(cmdAdviserAssumptions, "@value", DbType.Decimal, value);
                db.ExecuteDataSet(cmdAdviserAssumptions);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        //***********Assumption END****************************************************************

        //******************AssetMapping START*****************************************************
        public DataSet GetAssetClassificationMapping()
        {
            Database db;
            DbCommand cmdAdviserAssumptions;
            DataSet dsAdviserAssumptions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdAdviserAssumptions = db.GetStoredProcCommand("SP_AssetClassificationMapping");
                dsAdviserAssumptions = db.ExecuteDataSet(cmdAdviserAssumptions);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserAssumptions;
        }

        //******************AssetMapping END*****************************************************

        //******************RiskScore START********************************************************
        public DataTable GetAdviserRiskScore(int advisorId)
        {
            Database db;
            DataTable dt;
            DbCommand cmdScoreRange;
            DataSet dsScoreRange = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdScoreRange = db.GetStoredProcCommand("SP_GetAdviserRiskScore");
                db.AddInParameter(cmdScoreRange, "@adviserId", DbType.Int32, advisorId);
                dsScoreRange = db.ExecuteDataSet(cmdScoreRange);
                dt = dsScoreRange.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public DataTable InsertUpdateRiskClassScore(string classCode, decimal lowerText, decimal upperText, int advisorId, int userId)
        {
            Database db;
            DataTable dt;
            DbCommand cmdScoreRange;
            DataSet dsScoreRange = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdScoreRange = db.GetStoredProcCommand("SP_InsertUpdateRiskClassScore");
                db.AddInParameter(cmdScoreRange, "@riskCode", DbType.String, classCode);
                db.AddInParameter(cmdScoreRange, "@lowerLimit", DbType.Int32, lowerText);
                db.AddInParameter(cmdScoreRange, "@upperLimit", DbType.Int32, upperText);
                db.AddInParameter(cmdScoreRange, "@adviserId", DbType.Int32, advisorId);
                db.AddInParameter(cmdScoreRange, "@userId", DbType.Int32, userId);
                dsScoreRange = db.ExecuteDataSet(cmdScoreRange);
                dt = dsScoreRange.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public void DeleteRiskClassScore(string classCode, int advisorId)
        {
            Database db;
            //DataTable dt;
            DbCommand cmdScoreRange;
            //DataSet dsScoreRange = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdScoreRange = db.GetStoredProcCommand("SP_DeleteRiskClassScore");
                db.AddInParameter(cmdScoreRange, "@riskCode", DbType.String, classCode);
                db.AddInParameter(cmdScoreRange, "@adviserId", DbType.Int32, advisorId);
                db.ExecuteDataSet(cmdScoreRange);
                //dsScoreRange = 
                //dt = dsScoreRange.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            //return dt;
        }
        //******************RiskScore END********************************************************




        /// <summary>
        /// Create Advisor Dynamic RiskQuestions
        /// Added by Vinayak Patil...
        /// </summary>
        /// <param name="adviserDynamicRiskQuestionsVo"></param>
        /// <param name="AdviserMaintainableOrNot"></param>
        /// <param name="InsertAdviserQuestionOROptions"></param>
        /// <returns></returns>

        public int CreateAdvisorDynamicRiskQuestions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)
        {
            int qustionID = 0;
            Database db;
            DbCommand createAdvisorStaffCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdvisorStaffCmd = db.GetStoredProcCommand("SP_CreateAdvisorDynamicRiskProfileQuestions");

                db.AddInParameter(createAdvisorStaffCmd, "@AdvisorId", DbType.Int32, adviserDynamicRiskQuestionsVo.AdviserId);
                db.AddInParameter(createAdvisorStaffCmd, "@QM_Question", DbType.String, adviserDynamicRiskQuestionsVo.Question);
                db.AddInParameter(createAdvisorStaffCmd, "@QM_Purpose", DbType.String, adviserDynamicRiskQuestionsVo.Purpose);
                db.AddInParameter(createAdvisorStaffCmd, "@QM_IsImageAttached", DbType.Int32, 0);
                db.AddInParameter(createAdvisorStaffCmd, "@QM_ImageLocation", DbType.String, "");
                db.AddInParameter(createAdvisorStaffCmd, "@QM_IsActive", DbType.Int32, 0);

                db.AddOutParameter(createAdvisorStaffCmd, "@QM_QustionID", DbType.Int32, 0);

                if (db.ExecuteNonQuery(createAdvisorStaffCmd) != 0)

                    qustionID = int.Parse(db.GetParameterValue(createAdvisorStaffCmd, "@QM_QustionID").ToString());

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
                objects[0] = adviserDynamicRiskQuestionsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return qustionID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserDynamicRiskQuestionsVo"></param>
        /// <returns></returns>
        
        public int CreateAdvisorDynamicRiskQuestionsOptions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)
        {
            int qustionID = 0;
            Database db;
            DbCommand createAdvisorStaffCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdvisorStaffCmd = db.GetStoredProcCommand("SP_CreateAdvisorDynamicRiskProfileQuestionsOptions");

                db.AddInParameter(createAdvisorStaffCmd, "@AdvisorId", DbType.Int32, adviserDynamicRiskQuestionsVo.AdviserId);
                db.AddInParameter(createAdvisorStaffCmd, "@QM_QustionID", DbType.Int32, adviserDynamicRiskQuestionsVo.QuestionId);

                db.AddInParameter(createAdvisorStaffCmd, "@QOM_Option", DbType.String, adviserDynamicRiskQuestionsVo.Option);
                db.AddInParameter(createAdvisorStaffCmd, "@QOM_Weightage", DbType.Int32, adviserDynamicRiskQuestionsVo.Weightage);

                db.AddOutParameter(createAdvisorStaffCmd, "@QOM_OptionId", DbType.Int32, 1000);

                if (db.ExecuteNonQuery(createAdvisorStaffCmd) != 0)

                    qustionID = int.Parse(db.GetParameterValue(createAdvisorStaffCmd, "@QOM_OptionId").ToString());

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
                objects[0] = adviserDynamicRiskQuestionsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return qustionID;
        }


        /// <summary>
        /// Update Questions for an Adviser
        /// </summary>
        /// <param name="adviserDynamicRiskQuestionsVo"></param>
        /// <returns></returns>
        
        public bool UpdateAdvisorDynamicRiskQuestions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateAdvisorStaffCmd;

            try
            {
 
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdvisorStaffCmd = db.GetStoredProcCommand("SP_UpdateAdvisorDynamicRiskProfileQuestions");

                db.AddInParameter(updateAdvisorStaffCmd, "@AdvisorId", DbType.Int32, adviserDynamicRiskQuestionsVo.AdviserId);
                db.AddInParameter(updateAdvisorStaffCmd, "@QM_QustionID", DbType.Int32, adviserDynamicRiskQuestionsVo.QuestionId);
                db.AddInParameter(updateAdvisorStaffCmd, "@QM_Question", DbType.String, adviserDynamicRiskQuestionsVo.Question);
                db.AddInParameter(updateAdvisorStaffCmd, "@QM_Purpose", DbType.String, adviserDynamicRiskQuestionsVo.Purpose);
                db.AddInParameter(updateAdvisorStaffCmd, "@QM_IsImageAttached", DbType.Int32, 0);
                db.AddInParameter(updateAdvisorStaffCmd, "@QM_ImageLocation", DbType.String, DBNull.Value);
                db.AddInParameter(updateAdvisorStaffCmd, "@QM_IsActive", DbType.Int32, 0);

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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:UpdateAdvisorDynamicRiskQuestions()");


                object[] objects = new object[3];
                objects[0] = adviserDynamicRiskQuestionsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }


        public bool UpdateAdvisorDynamicRiskQuestionsOptions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateAdvisorStaffCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdvisorStaffCmd = db.GetStoredProcCommand("SP_UpdateAdvisorDynamicRiskProfileQuestionsOptions");

                db.AddInParameter(updateAdvisorStaffCmd, "@AdvisorId", DbType.Int32, adviserDynamicRiskQuestionsVo.AdviserId);
                db.AddInParameter(updateAdvisorStaffCmd, "@QM_QustionID", DbType.Int32, adviserDynamicRiskQuestionsVo.QuestionId);
                db.AddInParameter(updateAdvisorStaffCmd, "@QOM_OptionId", DbType.Int32, adviserDynamicRiskQuestionsVo.OptionId);

                db.AddInParameter(updateAdvisorStaffCmd, "@QOM_Option", DbType.String, adviserDynamicRiskQuestionsVo.Option);
                db.AddInParameter(updateAdvisorStaffCmd, "@QOM_Weightage", DbType.Int32, adviserDynamicRiskQuestionsVo.Weightage);


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

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:UpdateAdvisorDynamicRiskQuestionsOptions()");


                object[] objects = new object[3];
                objects[0] = adviserDynamicRiskQuestionsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }



        /// <summary>
        /// Get Adviser Maintained RiskProfile Question And Options
        /// </summary>
        /// <param name="advisorId"></param>
        /// <returns></returns>

        public DataSet GetAdviserMaintainedRiskProfileQuestionAndOptions(int advisorId)
        {
            Database db;
            DbCommand dbGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd = null;
            DataSet dsGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd = db.GetStoredProcCommand("SP_GetAdviserMaintainedRiskProfileQuestionAndOptions");
                db.AddInParameter(dbGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd, "@A_AdviserId", DbType.Int32, advisorId);
                //db.AddInParameter(dbGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd, "@QM_QuestionId", DbType.Int32, QuestionId);
                //db.AddInParameter(dbGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd, "@GetQuestionsOrOptions", DbType.Int32, GetQuestionsOrOptions);
                dsGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd = db.ExecuteDataSet(dbGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException baseEx = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RiskProfileDao.cs:GetRiskProfileQuestion()");
                throw baseEx;
            }
            return dsGetAdviserMaintainedRiskProfileQuestionAndOptionsCmd;
        }

    }
}

