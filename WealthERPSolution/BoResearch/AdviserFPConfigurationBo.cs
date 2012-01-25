using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoResearch;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCustomerRiskProfiling;

namespace BoResearch
{
    public class AdviserFPConfigurationBo
    {
        //***********Assumption START********************
        public DataSet GetAdviserAssumptions(int adviserId)
        {
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            DataSet dsAdviserAssumptions;
            try
            {
                dsAdviserAssumptions = adviserFPConfigurationDao.GetAdviserAssumptions(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserAssumptions;
        }

        public void UpdateAdviserAssumptions(int adviserId, decimal value, string assumptionId)
        {
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                adviserFPConfigurationDao.UpdateAdviserAssumptions(adviserId, value, assumptionId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        //***********Assumption END*******************


        //******************AssetMapping START*****************************************************
        public DataSet GetAssetClassificationMapping()
        {
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            DataSet dsAdviserAssumptions;
            try
            {
                dsAdviserAssumptions = adviserFPConfigurationDao.GetAssetClassificationMapping();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserAssumptions;
        }
        //******************AssetMapping END*******************************************************

        //******************RiskScore START********************************************************

        public DataTable GetAdviserRiskScore(int advisorId)
        {
            DataTable dt;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                dt = adviserFPConfigurationDao.GetAdviserRiskScore(advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public void UpdateRiskClassScore(string classCode, decimal lowerText, decimal upperText, int advisorId, int userId)
        {
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                adviserFPConfigurationDao.UpdateRiskClassScore(classCode, lowerText, upperText, advisorId, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "adviserFPConfigurationDao.cs:EditInsertUpdateRiskClass()");

                object[] objects = new object[1];
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public void DeleteRiskClassScore(string classCode, int advisorId)
        {
            DataTable dt;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                adviserFPConfigurationDao.DeleteRiskClassScore(classCode, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "adviserFPConfigurationDao.cs:DeleteRiskClassScore()");

                object[] objects = new object[1];
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        //******************RiskScore END************************************************************

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
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                qustionID = adviserFPConfigurationDao.CreateAdvisorDynamicRiskQuestions(adviserDynamicRiskQuestionsVo);
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
        /// Create Advisor Dynamic RiskQuestions Options 
        /// Added by Vinayak Patil...
        /// </summary>
        /// <param name="adviserDynamicRiskQuestionsVo"></param>
        /// <param name="AdviserMaintainableOrNot"></param>
        /// <param name="InsertAdviserQuestionOROptions"></param>
        /// <returns></returns>
        
        public int CreateAdvisorDynamicRiskQuestionsOptions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)
        {
            int qustionID = 0;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                qustionID = adviserFPConfigurationDao.CreateAdvisorDynamicRiskQuestionsOptions(adviserDynamicRiskQuestionsVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CreateAdvisorDynamicRiskQuestionsOptions()");


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
        /// Update Advisor Dynamic RiskQuestions
        /// Added by Vinayak Patil...
        /// </summary>
        /// <param name="adviserDynamicRiskQuestionsVo"></param>
        /// <param name="AdviserMaintainableOrNot"></param>
        /// <param name="InsertAdviserQuestionOROptions"></param>
        /// <returns></returns>
        public bool UpdateAdvisorDynamicRiskQuestions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)
        {
            bool bResult = false;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                bResult = adviserFPConfigurationDao.UpdateAdvisorDynamicRiskQuestions(adviserDynamicRiskQuestionsVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:UpdateAdvisorDynamicRiskQuestions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)");
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
        /// Update Advisor Dynamic RiskQuestions Options
        /// Added by Vinayak Patil...
        /// </summary>
        /// <param name="adviserDynamicRiskQuestionsVo"></param>
        /// <param name="AdviserMaintainableOrNot"></param>
        /// <param name="InsertAdviserQuestionOROptions"></param>
        /// <returns></returns>
        
        public bool UpdateAdvisorDynamicRiskQuestionsOptions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)
        {
            bool bResult = false;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                bResult = adviserFPConfigurationDao.UpdateAdvisorDynamicRiskQuestionsOptions(adviserDynamicRiskQuestionsVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBo.cs:UpdateAdvisorDynamicRiskQuestions(AdviserDynamicRiskQuestionsVo adviserDynamicRiskQuestionsVo)");
                object[] objects = new object[3];
                objects[0] = adviserDynamicRiskQuestionsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }



        public DataSet GetAdviserMaintainedRiskProfileQuestionAndOptions(int advisorId)
        {
            DataSet dsGetAdviserMaintainedRiskProfileQuestionAndOptions = null;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                dsGetAdviserMaintainedRiskProfileQuestionAndOptions = adviserFPConfigurationDao.GetAdviserMaintainedRiskProfileQuestionAndOptions(advisorId);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException baseEx = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "RiskProfileBo.cs:GetAdviserMaintainedRiskProfileQuestionAndOptions(int advisorId,QuestionId)");
                throw baseEx;
            }
            return dsGetAdviserMaintainedRiskProfileQuestionAndOptions;
        }

        /// <summary>
        /// Delete Question Options..
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="QuestionId"></param>
        /// <param name="OptionId"></param>
        /// <param name="QuestionOrOptionFlag"></param>
        /// <returns></returns>
        public bool DeleteAdviserQuestionOptions(int adviserId, int QuestionId, int OptionId, int QuestionOrOptionFlag)
        {
            bool bResult = false;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                bResult = adviserFPConfigurationDao.DeleteAdviserQuestionOptions(adviserId, QuestionId, OptionId, QuestionOrOptionFlag);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteAdviserQuestionOptions(int adviserId, int QuestionId, int OptionId, int QuestionOrOptionFlag)");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }


        public bool CheckAdviserRiskProfileDependency(int AdviserID)
        {
            bool bResult = false;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                bResult = adviserFPConfigurationDao.CheckAdviserRiskProfileDependency(AdviserID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserFPConfigurationBo.cs:CheckAdviserRiskProfileDependency()");
                object[] objects = new object[2];
                objects[0] = AdviserID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }


        public bool DeleteAdviserRiskProfile(int adviserId, int CustomerID)
        {
            bool bResult = false;
            AdviserFPConfigurationDao adviserFPConfigurationDao = new AdviserFPConfigurationDao();
            try
            {
                bResult = adviserFPConfigurationDao.DeleteAdviserRiskProfile(adviserId, CustomerID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBo.cs:DeleteAdviserRiskProfile(int adviserId, int QuestionId, int OptionId, int QuestionOrOptionFlag)");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
    }
}

