using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerPortfolio;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerPortfolio
{
    public class AssetDao
    {

        public double GetCustomerPortfolioLiability(int portfolioId)
        {
            double liabilityValue = 0;
            Database db;
            DbCommand getCustomerPortfolioLiabilityCmd;
            DataSet CustomerPortfolioLiabilityDs = null;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerPortfolioLiabilityCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioLiability");
                db.AddInParameter(getCustomerPortfolioLiabilityCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                CustomerPortfolioLiabilityDs = db.ExecuteDataSet(getCustomerPortfolioLiabilityCmd);
                if (CustomerPortfolioLiabilityDs != null && CustomerPortfolioLiabilityDs.Tables[0].Rows.Count != 0)
                {
                    dr=CustomerPortfolioLiabilityDs.Tables[0].Rows[0];
                    if (dr["LiabilityValue"] != null && dr["LiabilityValue"].ToString() != string.Empty)
                        liabilityValue = double.Parse(dr["LiabilityValue"].ToString());
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

                FunctionInfo.Add("Method", "AssetDao.cs:GetCustomerPortfolioLiability(int portfolioId)");

                object objects = new object();

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return liabilityValue;
        }
        public DataSet GetAssetGroups()
        {
            Database db;
            DbCommand getAssetGroupCmd;
            DataSet assetGroupsDs=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetGroupCmd = db.GetStoredProcCommand("SP_GetAssetGroups");
                assetGroupsDs = db.ExecuteDataSet(getAssetGroupCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetDao.cs:GetAssetGroups()");

                object objects = new object();

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return assetGroupsDs;
        }



        public DataSet GetAdviserBranchMF_EQ_In_AggregateCurrentValues(int advisorId, out int Count,int currentPage)
        {
            Database db;
            DbCommand getAdvisorBranchAggregateValueCmd;
            DataSet ds=null;
            Count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorBranchAggregateValueCmd = db.GetStoredProcCommand("SP_GetAdvisorBranchEQ_MF_IN_CurrentAggr");
                db.AddInParameter(getAdvisorBranchAggregateValueCmd, "@A_AdviserId", DbType.Int16, advisorId);
                db.AddInParameter(getAdvisorBranchAggregateValueCmd, "@CurrentPage", DbType.Int32, currentPage);
                ds = db.ExecuteDataSet(getAdvisorBranchAggregateValueCmd);
                if (ds.Tables[1].Rows.Count > 0)
                {
                    Count = int.Parse(ds.Tables[1].Rows[0][0].ToString());
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
                FunctionInfo.Add("Method", "AssetDao.cs:GetAdviserBranchMF_EQ_In_AggregateCurrentValues()");
                object[] objects = new object[1];
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;

        }

        public DataSet GetAdvisorRM_All_AssetAgr(int advisorId)
        {
            Database db;
            DbCommand getAdvisorRMAggregateValueCmd;
            DataSet ds=null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorRMAggregateValueCmd = db.GetStoredProcCommand("SP_GetAdvisorRM_All_AssetAgr");
                db.AddInParameter(getAdvisorRMAggregateValueCmd, "@A_AdviserId", DbType.Int16, advisorId);
                ds = db.ExecuteDataSet(getAdvisorRMAggregateValueCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetAdvisorRM_All_AssetAgr()");
                object[] objects = new object[1];
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;

        }


        public DataSet GetPortfolioAssetAggregateCurrentValues(int PortfolioId)
        {
            Database db;
            DbCommand getAssetAggregateCurrentValuesCmd;
            DataSet assetAggrCurrValues=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetAssetCurrentValues");
                db.AddInParameter(getAssetAggregateCurrentValuesCmd, "CP_PortfolioId", DbType.Int32, PortfolioId);
                assetAggrCurrValues = db.ExecuteDataSet(getAssetAggregateCurrentValuesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetAssetInstrumentCategory()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return assetAggrCurrValues;
        }

        public DataSet GetMFInvAggrCurrentValues(int PortfolioId,int adviserId)
        {
            Database db;
            DbCommand getMFAggregateCurrentValuesCmd;
            DataSet mfAggrCurrValues=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioMFInvstDashboard");
                db.AddInParameter(getMFAggregateCurrentValuesCmd, "@PortfolioId", DbType.Int32, PortfolioId);
                db.AddInParameter(getMFAggregateCurrentValuesCmd, "@A_AdviserId", DbType.Int32, adviserId);
                mfAggrCurrValues = db.ExecuteDataSet(getMFAggregateCurrentValuesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetMFInvAggrCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return mfAggrCurrValues;
        }

        public DataSet GetEQAggrCurrentValues(int PortfolioId,int adviserId)
        {
            Database db;
            DbCommand getEQAggregateCurrentValuesCmd;
            DataSet mfAggrCurrValues=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEQAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioEQInvstDashboard");
              //  getEQAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetEQAggregate");
                //getEQAggregateCurrentValuesCmd = db.GetStoredProcCommand("testing");
                db.AddInParameter(getEQAggregateCurrentValuesCmd, "@PortfolioId", DbType.Int32, PortfolioId);
                db.AddInParameter(getEQAggregateCurrentValuesCmd, "@A_AdviserId", DbType.Int32, adviserId);
                mfAggrCurrValues = db.ExecuteDataSet(getEQAggregateCurrentValuesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetEQAggrCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return mfAggrCurrValues;
        }

        public DataSet GetFIGovtInsDashboardCurrentValues(int PortfolioId)
        {
            Database db;
            DbCommand getFIDashAggregateCurrentValuesCmd;
            DataSet fiDashAggrCurrValues=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getFIDashAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioFIGovtInsuranceDashboard");
                db.AddInParameter(getFIDashAggregateCurrentValuesCmd, "PortfolioId", DbType.Int32, PortfolioId);
                fiDashAggrCurrValues = db.ExecuteDataSet(getFIDashAggregateCurrentValuesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetFIGovtInsDashboardCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return fiDashAggrCurrValues;
        }

        public DataSet GetOtherAssetsDashboardCurrentValues(int PortfolioId)
        {
            Database db;
            DbCommand getOtherAggregateCurrentValuesCmd;
            DataSet otherAggrCurrValues=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getOtherAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioOtherAssetsDashboard");
                db.AddInParameter(getOtherAggregateCurrentValuesCmd, "PortfolioId", DbType.Int32, PortfolioId);
                otherAggrCurrValues = db.ExecuteDataSet(getOtherAggregateCurrentValuesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetOtherAssetsDashboardCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return otherAggrCurrValues;
        }

        public DataSet GetNetIncomeSummary(int PortfolioId)
        {
            Database db;
            DbCommand getNetIncomeSummaryCmd;
            DataSet dsNetIncomeSummary=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNetIncomeSummaryCmd = db.GetStoredProcCommand("SP_GetEQDeliverySpecAggr");
                db.AddInParameter(getNetIncomeSummaryCmd, "CP_PortfolioId", DbType.Int32, PortfolioId);
                dsNetIncomeSummary = db.ExecuteDataSet(getNetIncomeSummaryCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetNetIncomeSummary()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsNetIncomeSummary;
        }

        //TO get Asset values for each RM in RM Dashboard
        public DataSet GetRMAssetAggregateCurrentValues(int RMId)
        {
            Database db;
            DbCommand getAssetAggregateCurrentValuesCmd;
            DataSet assetAggrCurrValues=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetAssetCurrentValuesRM");
                db.AddInParameter(getAssetAggregateCurrentValuesCmd, "RMID", DbType.Int32, RMId);
                assetAggrCurrValues = db.ExecuteDataSet(getAssetAggregateCurrentValuesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetRMAssetAggregateCurrentValues()");

                object[] objects = new object[1];
                objects[0] = RMId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return assetAggrCurrValues;
        }
        //TO get Customer MF and EQ values for each RM in RM Dashboard
        public DataSet GetRMCustomersAssetAggregateCurrentValues(int RMId)
        {
            Database db;
            DbCommand getAssetAggregateCurrentValuesCmd;
            DataSet assetAggrCurrValues=null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetAggregateCurrentValuesCmd = db.GetStoredProcCommand("SP_GetRMCustAssetAgrVal");
                db.AddInParameter(getAssetAggregateCurrentValuesCmd, "AR_RmId", DbType.Int32, RMId);
                assetAggrCurrValues = db.ExecuteDataSet(getAssetAggregateCurrentValuesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetCustomerAssetAggregateCurrentValues()");

                object[] objects = new object[1];
                objects[0] = RMId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return assetAggrCurrValues;
        }

        public DataSet GetAssetInstrumentCategory(string groupCode)
        {
            Database db;
            DbCommand getAssetInstrumentCategoryCmd;
            DataSet assetCategories=null;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetInstrumentCategoryCmd = db.GetStoredProcCommand("SP_GetAssetInstrumentCat");
                db.AddInParameter(getAssetInstrumentCategoryCmd, "PAG_AssetGroupCode", DbType.String, groupCode);
                assetCategories = db.ExecuteDataSet(getAssetInstrumentCategoryCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetDao.cs:GetPortfolioAssetAggregateCurrentValues()");


                object[] objects = new object[1];
                objects[0] = groupCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return assetCategories;
        }

        public DataSet GetAssetInstrumentSubCategory(string groupCode, string instrumentCategory)
        {
            Database db;
            DbCommand getAssetInstrumentSubCategoryCmd;
            DataSet assetSubCategories=null;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetInstrumentSubCategoryCmd = db.GetStoredProcCommand("SP_GetAssetInstrumentSubCat");
                db.AddInParameter(getAssetInstrumentSubCategoryCmd, "PAG_AssetGroupCode", DbType.String, groupCode);
                db.AddInParameter(getAssetInstrumentSubCategoryCmd, "PAIC_AssetInstrumentCategoryCode", DbType.String, instrumentCategory);
                assetSubCategories = db.ExecuteDataSet(getAssetInstrumentSubCategoryCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Asset.cs:GetAssetInstrumentSubCategory()");


                object[] objects = new object[2];
                objects[0] = groupCode;
                objects[1] = instrumentCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return assetSubCategories;

        }

        public DataSet GetAssetInstrumentSubSubCategory(string groupCode, string instrumentCategory, string instrumentSubCategory)
        {
            Database db;
            DbCommand getAssetInstrumentSubSubCategoryCmd;
            DataSet assetSubSubCategories=null;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetInstrumentSubSubCategoryCmd = db.GetStoredProcCommand("SP_GetAssetInstrumentSubSubCat");
                db.AddInParameter(getAssetInstrumentSubSubCategoryCmd, "PAG_AssetGroupCode", DbType.String, groupCode);
                db.AddInParameter(getAssetInstrumentSubSubCategoryCmd, "PAIC_AssetInstrumentCategoryCode", DbType.String, instrumentCategory);
                db.AddInParameter(getAssetInstrumentSubSubCategoryCmd, "PAISC_AssetInstrumentSubCategoryCode", DbType.String, instrumentSubCategory);
                assetSubSubCategories = db.ExecuteDataSet(getAssetInstrumentSubSubCategoryCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "Asset.cs:GetAssetInstrumentSubSubCategory()");


                object[] objects = new object[3];
                objects[0] = groupCode;
                objects[1] = instrumentCategory;
                objects[2] = instrumentSubCategory;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return assetSubSubCategories;

        }
        public DataSet GetULIPSubPlans(int ulipPlanId)
        {
            DataSet getUlipSubPalnDs=null;
            Database db;
            DbCommand getUlipSubPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUlipSubPlanCmd = db.GetStoredProcCommand("SP_GetULIPSubPlans");
                db.AddInParameter(getUlipSubPlanCmd, "@WUP_ULIPPlanCode", DbType.String, ulipPlanId);
                getUlipSubPalnDs = db.ExecuteDataSet(getUlipSubPlanCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Asset.cs:GetULIPSubPlans()");
                object[] objects = new object[1];
                objects[0] = ulipPlanId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getUlipSubPalnDs;
        }
        public DataSet GetULIPPlans(string issuerCode)
        {
            DataSet getUlipPlanDs=null;
            Database db;
            DbCommand getUlipPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUlipPlanCmd = db.GetStoredProcCommand("SP_GetULIPPlans");
                db.AddInParameter(getUlipPlanCmd, "@XII_InsuranceIssuerCode", DbType.String, issuerCode);
                getUlipPlanDs = db.ExecuteDataSet(getUlipPlanCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Asset.cs:GetULIPPlans()");
                object[] objects = new object[1];
                objects[0] = issuerCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getUlipPlanDs;

        }
        public DataTable GetFiscalYearCode(string path)
        {
            DataSet ds;
            DataTable dt=null;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["FiscalYear"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetFiscalYearCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public DataTable GetMeasureCode(string path)
        {
            DataSet ds;
            DataTable dt=null;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["MeasureCode"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetMeasureCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public DataTable GetDebtIssuerCode(string path)
        {
            DataSet ds;
            DataTable dt=null;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["DebtIssuerCode"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetDebtIssuerCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;
        }

        public DataTable GetInterestBasis(string path)
        {
            DataSet ds;
            DataTable dt=null;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["InterestBasis"];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetInterestBasis()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }
        public DataTable GetModeOfHolding(string path)
        {
            DataSet ds;
            DataTable dt=null;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["ModeOfHolding"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetModeOfHolding()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        public DataTable GetInsuranceIssuerCode(string path)
        {
            DataSet ds;
            DataTable dt=null;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["InsuranceIssuerCode"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetInsuranceIssuerCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        public DataTable GetFrequencyCode(string path)
        {
            DataSet ds;
            DataTable dt=null;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                dt = ds.Tables["Frequency"];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetFrequencyCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }

        public DataSet GetPrevULIPSubPlans(int InsuranceId)
        {
            DataSet getPrevUlipSubPlanDs=null;
            Database db;
            DbCommand getPrevUlipSubPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPrevUlipSubPlanCmd = db.GetStoredProcCommand("SP_GetUlipSubPlanCodeFromInvInsID");
                db.AddInParameter(getPrevUlipSubPlanCmd, "@CINP_InsuranceNPId", DbType.Int32, InsuranceId);
                getPrevUlipSubPlanDs = db.ExecuteDataSet(getPrevUlipSubPlanCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Asset.cs:GetULIPPlans()");
                object[] objects = new object[1];
                objects[0] = InsuranceId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getPrevUlipSubPlanDs;
        }

        public DataSet GetPrevUlipPlanCode(int UlipSubPlanCode)
        {
            DataSet getPrevUlipPlanDs=null;
            Database db;
            DbCommand getPrevUlipPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getPrevUlipPlanCmd = db.GetStoredProcCommand("SP_GetULipPlanCodeFromSubPlanCode");
                db.AddInParameter(getPrevUlipPlanCmd, "@WUSP_ULIPSubPlanCode", DbType.Int32, UlipSubPlanCode);
                getPrevUlipPlanDs = db.ExecuteDataSet(getPrevUlipPlanCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Asset.cs:GetPrevUlipPlanCode()");
                object[] objects = new object[1];
                objects[0] = UlipSubPlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getPrevUlipPlanDs;
        }

        public DataSet GetAssetMaturityDates(int portfolioId)
        {
            DataSet getAssetMaturityDatesDs=null;
            Database db;
            DbCommand getAssetMaturityDatesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetMaturityDatesCmd = db.GetStoredProcCommand("SP_GetAssetMaturityDates");
                db.AddInParameter(getAssetMaturityDatesCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                getAssetMaturityDatesDs = db.ExecuteDataSet(getAssetMaturityDatesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Asset.cs:GetAssetMaturityDates()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getAssetMaturityDatesDs;
        }

        /// <summary>
        /// Function to get the Top 5 Asset Maturity Dates of a Group (including the Group Head and all its member customers)
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetGrpAssetMaturityDates(int customerId)
        {
            DataSet getGrpAssetMaturityDatesDs = null;
            Database db;
            DbCommand getGrpAssetMaturityDatesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGrpAssetMaturityDatesCmd = db.GetStoredProcCommand("SP_GetGrpAssetMaturityDates");
                db.AddInParameter(getGrpAssetMaturityDatesCmd, "@C_CustomerId", DbType.Int32, customerId);
                getGrpAssetMaturityDatesDs = db.ExecuteDataSet(getGrpAssetMaturityDatesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Asset.cs:GetAssetMaturityDates()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getGrpAssetMaturityDatesDs;
        }

        public string GetAssetCategoryName(string Code)
        {
            Database db;
            DataSet getAssetCategoryNameDs=null;
            DbCommand getAssetCategoryNameCmd;
            string categoryName="";
            try
            {         
               
                    db = DatabaseFactory.CreateDatabase("wealtherp");
                    getAssetCategoryNameCmd = db.GetStoredProcCommand("SP_GetAssetCategoryName");
                    db.AddInParameter(getAssetCategoryNameCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, Code);
                    getAssetCategoryNameDs = db.ExecuteDataSet(getAssetCategoryNameCmd);
                categoryName=getAssetCategoryNameDs.Tables[0].Rows[0][0].ToString();

            }
                catch (BaseApplicationException Ex)
                {
                    throw Ex;
                }
                catch (Exception Ex)
                {
                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                    NameValueCollection FunctionInfo = new NameValueCollection();
                    FunctionInfo.Add("Method", "Asset.cs:GetAssetCategoryName()");
                    object[] objects = new object[1];
                    objects[0] = Code;
                    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                    exBase.AdditionalInformation = FunctionInfo;
                    ExceptionManager.Publish(exBase);
                    throw exBase;
                }
            return categoryName;
        }

        public DataSet GetAssetOwnerShip(int AssetId, string AssetGroupCode, int customerId, int associateId, Int16 IsMainCustomer)
        {
            DataSet getAssetOwnerShipDs = null;
            Database db;
            DbCommand getAssetOwnerShipCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetOwnerShipCmd = db.GetStoredProcCommand("SP_GetAssetOwnerShip");
                db.AddInParameter(getAssetOwnerShipCmd, "@AssetId", DbType.Int32, AssetId);
                db.AddInParameter(getAssetOwnerShipCmd, "@AssetGroupCode", DbType.String, AssetGroupCode);
                db.AddInParameter(getAssetOwnerShipCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(getAssetOwnerShipCmd, "@associateId", DbType.Int32, associateId);
                db.AddInParameter(getAssetOwnerShipCmd, "@IsMainCustomer", DbType.Int16, IsMainCustomer);
                getAssetOwnerShipDs = db.ExecuteDataSet(getAssetOwnerShipCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "Asset.cs:GetAssetOwnerShip()");
                object[] objects = new object[5];
                objects[0] = AssetId;
                objects[1] = AssetGroupCode;
                objects[2] = customerId;
                objects[3] = associateId;
                objects[4] = IsMainCustomer;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getAssetOwnerShipDs;
        }

        /// <summary>
        /// Retrieve all the member customers Net Holdings for Group Dashboard
        /// </summary>
        /// <param name="CustomerId">Customer ID of the group head</param>
        /// <returns></returns>
        public DataSet GetGrpAssetNetHoldings(int CustomerId)
        {
            Database db;
            DbCommand getGrpAssetNetHoldingsCmd;
            DataSet assetGrpNetHoldings = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGrpAssetNetHoldingsCmd = db.GetStoredProcCommand("SP_GrpDashBoard_GetGroupHoldings");
                db.AddInParameter(getGrpAssetNetHoldingsCmd, "CustomerId", DbType.Int32, CustomerId);
                assetGrpNetHoldings = db.ExecuteDataSet(getGrpAssetNetHoldingsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetDao.cs:GetGrpAssetNetHoldings()");

                object[] objects = new object[1];
                objects[0] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return assetGrpNetHoldings;
        }

    }

}
