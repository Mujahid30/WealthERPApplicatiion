using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VoCustomerPortfolio;
using VoUser;
using DaoCustomerPortfolio;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoCustomerPortfolio
{
    public class AssetBo
    {
        public DataSet GetAssetGroups()
        {
            AssetDao assetDao = new AssetDao();
            DataSet assetGroupsDs;
            try
            {
                assetGroupsDs = assetDao.GetAssetGroups();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetAssetInstrumentCategory()");


                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return assetGroupsDs;
        }
        public double GetCustomerPortfolioLiability(int portfolioId)
        {
            double liabilityValue = 0;
            AssetDao assetDao = new AssetDao();
            
            try
            {
                liabilityValue = assetDao.GetCustomerPortfolioLiability(portfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetCustomerPortfolioLiability(int portfolioId)");

                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return liabilityValue;
        }
        public DataSet GetPortfolioAssetAggregateCurrentValues(int PortfolioId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet aggrCurrentValues;
            try
            {
                aggrCurrentValues = assetDao.GetPortfolioAssetAggregateCurrentValues(PortfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetPortfolioAssetAggregateCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return aggrCurrentValues;
        }

        public DataSet GetMFInvAggrCurrentValues(int PortfolioId,int adviserId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet aggrCurrentValues;
            try
            {
                aggrCurrentValues = assetDao.GetMFInvAggrCurrentValues(PortfolioId,adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetMFInvAggrCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return aggrCurrentValues;
        }

        public DataSet GetEQAggrCurrentValues(int PortfolioId,int adviserId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet aggrCurrentValues;
            try
            {
                aggrCurrentValues = assetDao.GetEQAggrCurrentValues(PortfolioId,adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetEQAggrCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return aggrCurrentValues;
        }

        public DataSet GetFIGovtInsDashboardCurrentValues(int PortfolioId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet aggrCurrentValues;
            try
            {
                aggrCurrentValues = assetDao.GetFIGovtInsDashboardCurrentValues(PortfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetFIGovtInsDashboardCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return aggrCurrentValues;
        }

        public DataSet GetOtherAssetsDashboardCurrentValues(int PortfolioId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet aggrCurrentValues;
            try
            {
                aggrCurrentValues = assetDao.GetOtherAssetsDashboardCurrentValues(PortfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetOtherAssetsDashboardCurrentValues()");

                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return aggrCurrentValues;
        }

        public DataSet GetNetIncomeSummary(int PortfolioId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet dsNetIncomeSummary;
            try
            {
                dsNetIncomeSummary = assetDao.GetNetIncomeSummary(PortfolioId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetNetIncomeSummary()");

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
            AssetDao assetDao = new AssetDao();
            DataSet aggrCurrentValues;
            try
            {
                aggrCurrentValues = assetDao.GetRMAssetAggregateCurrentValues(RMId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetRMAssetAggregateCurrentValues()");


                object[] objects = new object[1];
                objects[0] = RMId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return aggrCurrentValues;
        }

        //TO get Customer MF and EQ values for each RM in RM Dashboard
        public DataSet GetRMCustomersAssetAggregateCurrentValues(int RMId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet aggrCurrentValues;
            try
            {
                aggrCurrentValues = assetDao.GetRMCustomersAssetAggregateCurrentValues(RMId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetRMCustomersAssetAggregateCurrentValues()");


                object[] objects = new object[1];
                objects[0] = RMId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return aggrCurrentValues;
        }


        public DataSet GetAssetInstrumentCategory(string groupCode)
        {
            AssetDao assetDao = new AssetDao();
            DataSet assetCategories;
            try
            {
                assetCategories = assetDao.GetAssetInstrumentCategory(groupCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetAssetInstrumentCategory()");


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
            AssetDao assetDao = new AssetDao();
            DataSet assetSubCategories;
            try
            {
                assetSubCategories = assetDao.GetAssetInstrumentSubCategory(groupCode, instrumentCategory);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AssetBo.cs:GetAssetInstrumentSubCategory()");


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

        public DataSet GetAdviserBranchMF_EQ_In_AggregateCurrentValues(int advisorId,out int Count,int currentPage)
        {
            AssetDao assetDao = new AssetDao();
            DataSet ds = new DataSet();
            try
            {
                ds = assetDao.GetAdviserBranchMF_EQ_In_AggregateCurrentValues(advisorId,out Count,currentPage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetAdviserBranchMF_EQ_In_AggregateCurrentValues()");
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
            AssetDao assetDao = new AssetDao();
            DataSet ds = new DataSet();
            try
            {
                ds = assetDao.GetAdvisorRM_All_AssetAgr(advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetAdvisorRM_All_AssetAgr()");
                object[] objects = new object[1];
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;

        }

        public DataSet GetAssetInstrumentSubSubCategory(string groupCode, string instrumentCategory, string instrumentSubCategory)
        {
            AssetDao assetDao = new AssetDao();
            DataSet assetSubSubCategories;
            try
            {
                assetSubSubCategories = assetDao.GetAssetInstrumentSubSubCategory(groupCode, instrumentCategory, instrumentSubCategory);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
               FunctionInfo.Add("Method", "AssetBo.cs:GetAssetInstrumentSubSubCategory()");
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
        public DataSet GetULIPPlans(string issuerCode)
        {
            AssetDao assetDao = new AssetDao();
            DataSet getUlipPlanDs;
            try
            {
                getUlipPlanDs = assetDao.GetULIPPlans(issuerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetULIPPlans()");
                object[] objects = new object[1];
                objects[0] = issuerCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getUlipPlanDs;
        }
        public DataSet GetULIPSubPlans(int ulipPlanId)
        {
            AssetDao assetDao = new AssetDao();
            DataSet getUlipSubPlanDs;
            try
            {
                getUlipSubPlanDs = assetDao.GetULIPSubPlans(ulipPlanId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetULIPSubPlans()");
                object[] objects = new object[1];
                objects[0] = ulipPlanId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getUlipSubPlanDs;
        }
        public DataTable GetInsuranceIssuerCode(string path)
        {
            AssetDao assetDao = new AssetDao();
            DataTable dt;
            try
            {
                dt = assetDao.GetInsuranceIssuerCode(path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetInsuranceIssuerCode()");
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
            AssetDao assetDao = new AssetDao();
            DataTable dt;
            try
            {
                dt = assetDao.GetModeOfHolding(path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetModeOfHolding()");
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
            AssetDao assetDao = new AssetDao();
            DataTable dt;
            try
            {
                dt = assetDao.GetDebtIssuerCode(path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetDebtIssuerCode()");
                object[] objects = new object[1];
                objects[0] = path;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dt;

        }
        public DataTable GetFiscalYearCode(string path)
        {
            AssetDao assetDao = new AssetDao();
            DataTable dt;
            try
            {
                dt = assetDao.GetFiscalYearCode(path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetFiscalYearCode()");
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
            AssetDao assetDao = new AssetDao();
            DataTable dt;
            try
            {
                dt = assetDao.GetMeasureCode(path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetMeasureCode()");
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
            AssetDao assetDao = new AssetDao();
            DataTable dt;
            try
            {
                dt = assetDao.GetInterestBasis(path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetInterestBasis()");
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
            AssetDao assetDao = new AssetDao();
            DataTable dt;
            try
            {
                dt = assetDao.GetFrequencyCode(path);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetFrequencyCode()");
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
            AssetDao assetDao = new AssetDao();
            DataSet getPrevUlipSubPlanDs;
            try
            {
                getPrevUlipSubPlanDs = assetDao.GetPrevULIPSubPlans(InsuranceId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetPrevULIPSubPlans()");
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
            AssetDao assetDao = new AssetDao();
            DataSet getPrevUlipPlanDs;
            try
            {
                getPrevUlipPlanDs = assetDao.GetPrevUlipPlanCode(UlipSubPlanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetPrevUlipPlanCode()");
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
            AssetDao assetDao = new AssetDao();
            DataSet getAssetMaturityDatesDs;
            try
            {
                getAssetMaturityDatesDs = assetDao.GetAssetMaturityDates(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetAssetMaturityDates()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getAssetMaturityDatesDs;
        }

        public DataSet GetAssetOwnerShip(int AssetId, string AssetGroupCode, int customerId, int associateId, Int16 IsMainCustomer)
        {
            AssetDao assetDao = new AssetDao();
            DataSet getAssetOwnerShipDs;
            try
            {
                getAssetOwnerShipDs = assetDao.GetAssetOwnerShip(AssetId, AssetGroupCode, customerId, associateId, IsMainCustomer);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssetBo.cs:GetAssetOwnerShip()");
                object[] objects = new object[4];
                objects[0] = AssetId;
                objects[1] = AssetGroupCode;
                objects[2] = customerId;
                objects[3] = associateId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getAssetOwnerShipDs;
        }
    }
}
