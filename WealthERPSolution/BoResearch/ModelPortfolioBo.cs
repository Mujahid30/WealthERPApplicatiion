using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoResearch;
using VoResearch;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoResearch
{
    public class ModelPortfolioBo
    {        
        public DataTable GetAMCList()
        {
            DataTable dtGetMutualFund = new DataTable();
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();            
            try
            {
                dtGetMutualFund = modelPortfolioDao.GetAMCList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMutualFund;
        }

        public DataTable GetModelPortfolioName(int advisorId)
        {
            DataTable dtModelPortfolioName = new DataTable();
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtModelPortfolioName = modelPortfolioDao.GetModelPortfolioName(advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtModelPortfolioName;
        }

        public DataSet BindddlMFSubCategory()
        {
            DataSet dsGetSubCategory = new DataSet();
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dsGetSubCategory = modelPortfolioDao.BindddlMFSubCategory();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetSubCategory;
        }
        public DataSet GetSchemeListSubCategory(int amcCode, string categoryCode, string subCategory)
        {
            DataSet dsSchemeListSubCategory = new DataSet();
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dsSchemeListSubCategory = modelPortfolioDao.GetSchemeListSubCategory(amcCode, categoryCode, subCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSchemeListSubCategory;
        }

        public DataTable GetBasisList()
        {
            DataTable dtGetBasisList = new DataTable();
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtGetBasisList = modelPortfolioDao.GetBasisList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetBasisList;
        }

        public DataTable GetArchiveReason()
        {
            DataTable dtGetArchiveReason = new DataTable();
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtGetArchiveReason = modelPortfolioDao.GetArchiveReason();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetArchiveReason;
        }
        public DataSet GetScoreRange(int advisorId)
        {
            DataSet dsScoreRange = new DataSet();
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dsScoreRange = modelPortfolioDao.GetScoreRange(advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsScoreRange;
        }

        public bool InsertModelPortfolioDetails(ModelPortfolioVo modelPortfolioVo, int adviserId)
        {
            bool bResult = false;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                bResult = modelPortfolioDao.InsertModelPortfolioDetails(modelPortfolioVo, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:InsertModelPortfolioDetails()");

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
            DataTable dtModelPortFolio;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtModelPortFolio = modelPortfolioDao.GetModelportfolioDetails(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:GetModelportfolioDetails()");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtModelPortFolio;
        }

        public DataTable GetAttachedSchemeDetails(int modelPortfolioCode, int adviserId)
        {
            DataTable dtAttachedScheme;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtAttachedScheme = modelPortfolioDao.GetAttachedSchemeDetails(modelPortfolioCode, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:GetAttachedSchemeDetails()");

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
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtAttachedScheme = modelPortfolioDao.getAllocationPercentageFromModelPortFolio(modelPortfolioCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:getAllocationPercentageFromModelPortFolio()");

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

        public void DeleteModelPortfolioRecords(int AMPTBValueId)
        {
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                modelPortfolioDao.DeleteModelPortfolioRecords(AMPTBValueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:DeleteModelPortfolioRecords()");

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
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                modelPortfolioDao.EditModelPortfolioRecords(modelPortfolioVo, AMPTBValueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:EditModelPortfolioRecords()");

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
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                bResult = modelPortfolioDao.AttachSchemeToPortfolio(modelPortfolioVo, adviserId, IsActiveFlag);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:AttachSchemeToPortfolio()");

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

        public void DeleteSchemeFromModelPortfolio(int modelPortfolioCode, int adviserId)
        {
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                modelPortfolioDao.DeleteSchemeFromModelPortfolio(modelPortfolioCode, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:DeleteSchemeFromModelPortfolio()");

                object[] objects = new object[1];
                objects[0] = AMFMPD_Id;
                objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataTable GetSchemeAssetAllocation(ModelPortfolioVo modelPortfolioVo, int adviserId)
        {
            DataTable dtAttachedScheme;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtAttachedScheme = modelPortfolioDao.GetSchemeAssetAllocation(modelPortfolioVo, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:GetSchemeAssetAllocation()");

                object[] objects = new object[1];
                objects[0] = modelPortfolioVo;
                objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAttachedScheme;
        }

        public DataTable SchemeAssetChartOnSubCategory(ModelPortfolioVo modelPortfolioVo, int adviserId)
        {
            DataTable dtSchemeAssetChart;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtSchemeAssetChart = modelPortfolioDao.SchemeAssetChartOnSubCategory(modelPortfolioVo, adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:SchemeAssetChartOnSubCategory()");

                object[] objects = new object[1];
                objects[0] = modelPortfolioVo;
                objects[1] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeAssetChart;
        }



        public DataTable GetArchivedSchemeDetails(int modelPortfolioCode, int adviserId)
        {
            DataTable dtAttachedScheme;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dtAttachedScheme = modelPortfolioDao.GetArchivedSchemeDetails(modelPortfolioCode, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:GetArchivedSchemeDetails()");

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


        public void ArchiveSchemeFromModelPortfolio(int AMFMPD_Id, int xarId)
        {
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                modelPortfolioDao.ArchiveSchemeFromModelPortfolio(AMFMPD_Id, xarId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:ArchiveSchemeFromModelPortfolio()");

                object[] objects = new object[1];
                objects[0] = AMFMPD_Id;
                objects[1] = xarId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        
        public DataTable GetRiskGoalClassData(int adviserId, int isRiskClass)
        {
            DataTable dt;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dt = modelPortfolioDao.GetRiskGoalClassData(adviserId, isRiskClass);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:GetRiskGoalClassData()");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable InsertUpdateRiskGoalClass(string value, string Text, int advisorId)
        {
            DataTable dt;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                dt = modelPortfolioDao.InsertUpdateRiskGoalClass(value, Text, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:EditInsertUpdateRiskClass()");

                object[] objects = new object[2];
                objects[0] = value;
                objects[1] = Text;
                objects[2] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public void DeleteRiskClass(string riskCode, int advisorId)
        {
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                modelPortfolioDao.DeleteRiskClass(riskCode, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:DeleteRiskClass()");

                object[] objects = new object[1];
                objects[0] = riskCode;
                objects[1] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public DataSet bindDdlPickRiskClass(int adviserId, int isRiskClass)
        {
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            DataSet ds = new DataSet();
            try
            {
                ds = modelPortfolioDao.bindDdlPickRiskClass(adviserId, isRiskClass);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:bindDdlPickRiskClass()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        //********************************************** Code for Asset Allocation Starts***********************************************************

        public bool CreateVariantAssetPortfolio(ModelPortfolioVo modelPortfolioVo, int adviserId, int userId)
        {
            bool bResult = false;
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();           
            try
            {
                bResult = modelPortfolioDao.CreateVariantAssetPortfolio(modelPortfolioVo, adviserId, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:CreateVariantAssetPortfolio()");

                object[] objects = new object[2];
                objects[0] = modelPortfolioVo;
                objects[1] = adviserId;
                objects[2] = userId;

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
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                bResult = modelPortfolioDao.UpdateVariantAssetPortfolio(modelPortfolioVo, adviserId, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:UpdateVariantAssetPortfolio()");

                object[] objects = new object[2];
                objects[0] = modelPortfolioVo;
                objects[1] = adviserId;
                objects[2] = userId;

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
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                ds = modelPortfolioDao.GetVariantAssetPortfolioDetails(advisorId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:GetVariantAssetPortfolioDetails()");

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
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            try
            {
                modelPortfolioDao.DeleteVariantAssetPortfolio(modelPortfolioCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ModelPortfolioBo.cs:DeleteVariantAssetPortfolio()");

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
            ModelPortfolioDao modelPortfolioDao = new ModelPortfolioDao();
            DataSet dsModelPortFolioSchemeDetails = new DataSet();
            try
            {
                dsModelPortFolioSchemeDetails = modelPortfolioDao.GetGoalModelPortFolioAttachedSchemes(customerId, adviserId, goalId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsModelPortFolioSchemeDetails;
           
        }
    
    }
}
