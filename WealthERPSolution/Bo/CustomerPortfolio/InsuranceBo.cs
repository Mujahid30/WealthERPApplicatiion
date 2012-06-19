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
    public class InsuranceBo
    {
        public int CreateInsurancePortfolio(InsuranceVo insuranceVo, int userId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            //bool bResult = false;
            int insuranceId = 0;
            try
            {
            //    bResult = insuranceDao.CreateInsurancePortfolio(insuranceVo, userId);
                insuranceId = insuranceDao.CreateInsurancePortfolio(insuranceVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateInsurancePortfolio()");


                object[] objects = new object[2];
                objects[0] = insuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            //return bResult;
            return insuranceId;
        }

        public bool UpdateInsurancePortfolio(InsuranceVo insuranceVo, int userId)
        {
            bool bResult = false;
            InsuranceDao insuranceDao = new InsuranceDao();

            try
            {
                bResult = insuranceDao.UpdateInsurancePortfolio(insuranceVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:UpdateInsurancePortfolio()");
                object[] objects = new object[2];
                objects[0] = insuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteInsurancePortfolio(int InsuranceId, int AccountId)
        {
            bool bResult = false;
            InsuranceDao insuranceDao = new InsuranceDao();

            try
            {
                bResult = insuranceDao.DeleteInsurancePortfolio(InsuranceId, AccountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:DeleteInsurancePortfolio()");
                object[] objects = new object[2];
                objects[0] = InsuranceId;
                objects[1] = AccountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        
        public List<InsuranceVo> GetInsurancePortfolio(int portfolioId, string sortExpression)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            List<InsuranceVo> insuranceList = new List<InsuranceVo>();

            try
            {
                insuranceList = insuranceDao.GetInsurancePortfolio(portfolioId, sortExpression);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetInsurancePortfolio()");

                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return insuranceList;
        }

        public InsuranceVo GetInsuranceAsset(int insurancePortfolioId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            InsuranceVo insuranceVo = new InsuranceVo();

            try
            {
                insuranceVo = insuranceDao.GetInsuranceAsset(insurancePortfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetInsuranceAsset()");


                object[] objects = new object[1];
                objects[0] = insurancePortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return insuranceVo;
        }

        public int ChkAccountAvail(int customerId)
        {
            int count = 0;
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                count = insuranceDao.ChkAccountAvail(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:ChkAccountAvail()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
        }

        public int CreateInsuranceAccount(InsuranceVo insuranceVo, int userId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();       
            int id = 0;
            try
            {
               id = insuranceDao.CreateInsuranceAccount(insuranceVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateInsuranceAccount()");


                object[] objects = new object[2];
                objects[0] = insuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return id;
        }

        public DataSet GetCustomerInsuranceAccounts(int customerId, string assetGroup)
        {
            DataSet dsInsuranceAccounts;
            InsuranceDao insuranceDao=new InsuranceDao();
            try
            {
                dsInsuranceAccounts = insuranceDao.GetCustomerInsuranceAccounts(customerId, assetGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetCustomerInsuranceAccounts()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsInsuranceAccounts;
        }

        public bool CreateMoneyBackEpisode(MoneyBackEpisodeVo moneyBackEpisodeVo)
        {
            bool bResult = false;
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                bResult = insuranceDao.CreateMoneyBackEpisode(moneyBackEpisodeVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateMoneyBackEpisode()");
                object[] objects = new object[1];
                objects[0] = moneyBackEpisodeVo;              
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool UpdateMoneyBackEpisode(MoneyBackEpisodeVo moneyBackEpisodeVo)
        {
            bool bResult = false;

            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                bResult = insuranceDao.UpdateMoneyBackEpisode(moneyBackEpisodeVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:UpdateMoneyBackEpisode()");
                object[] objects = new object[1];
                objects[0] = moneyBackEpisodeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool CreateInsuranceULIPPlan(InsuranceULIPVo insuranceUlipVo)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            bool bResult = false;
            try
            {
                bResult = insuranceDao.CreateInsuranceULIPPlan(insuranceUlipVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateInsuranceULIPPlan()");
                object[] objects = new object[1];
                objects[0] = insuranceUlipVo;           
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public bool UpdateInsuranceULIPPlan(InsuranceULIPVo insuranceUlipVo)
        {
            bool bResult = false;

            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                bResult = insuranceDao.UpdateInsuranceULIPPlan(insuranceUlipVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:UpdateMoneyBackEpisode()");
                object[] objects = new object[1];
                objects[0] = insuranceUlipVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteInsuranceUlipPlans(int InsuranceID)
        {
            bool bResult = false;

            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                bResult = insuranceDao.DeleteInsuranceUlipPlans(InsuranceID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:DeleteInsuranceUlipPlans()");
                object[] objects = new object[1];
                objects[0] = InsuranceID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }
        
        public InsuranceULIPVo GetInsuranceULIPDetails(int insuranceUlipId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            InsuranceULIPVo insuranceUlipVo = new InsuranceULIPVo();
            try
            {
                insuranceUlipVo = insuranceDao.GetInsuranceULIPDetails(insuranceUlipId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:GetInsuranceULIPDetails()");
                object[] objects = new object[1];
                objects[0] = insuranceUlipId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return insuranceUlipVo;

        }

        public List<InsuranceULIPVo> GetInsuranceULIPList(int insuranceId)
        {
            List<InsuranceULIPVo> insuranceUlipList = new List<InsuranceULIPVo>();
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                insuranceUlipList = insuranceDao.GetInsuranceULIPList(insuranceId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:GetInsuranceULIPList()");
                object[] objects = new object[1];
                objects[0] = insuranceId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return insuranceUlipList;
        }

        public List<MoneyBackEpisodeVo> GetMoneyBackEpisodeList(int insuranceId)
        {
            List<MoneyBackEpisodeVo> moneyBackEpisodeList = new List<MoneyBackEpisodeVo>();
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                moneyBackEpisodeList = insuranceDao.GetMoneyBackEpisodeList(insuranceId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:GetMoneyBackEpisodeList()");
                object[] objects = new object[1];
                objects[0] = insuranceId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return moneyBackEpisodeList;
        }

        public bool DeleteMoneyBackEpisode(MoneyBackEpisodeVo moneyBackEpisodeVo)
        {
            bool blResult = false;

            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                blResult = insuranceDao.DeleteMoneyBackEpisode(moneyBackEpisodeVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:DeleteMoneyBackEpisode()");
                object[] objects = new object[1];
                objects[0] = moneyBackEpisodeVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public DataSet GetUlipPlanCode(int CustInsInvId)
        {
            DataSet dsGetUlipPlanCodeFromInsuranceId;
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                dsGetUlipPlanCodeFromInsuranceId = insuranceDao.GetUlipPlanCode(CustInsInvId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetUlipPlanCode()");

                object[] objects = new object[1];
                objects[0] = CustInsInvId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetUlipPlanCodeFromInsuranceId;
        }

        public DataTable ChkGenInsurancePortfolioExist(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.ChkGenInsurancePortfolioExist(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:ChkGenInsurancePortfolioExist()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable ChkLifeInsurancePortfolioExist(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.ChkLifeInsurancePortfolioExist(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:ChkLifeInsurancePortfolioExist()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public int CreateCustomerGIPortfolio(int customerId, int userId)
        {
            int customerGIPortfolioId = 0;

            InsuranceDao insuaranceDao = new InsuranceDao();
            try
            {
                customerGIPortfolioId = insuaranceDao.CreateCustomerGIPortfolio(customerId, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateCustomerGIPortfolio()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerGIPortfolioId;
        }

        public int CreateCustomerLIPortfolio(int customerId, int userId)
        {
            int customerLIPortfolioId = 0;

            InsuranceDao insuaranceDao = new InsuranceDao();
            try
            {
                customerLIPortfolioId = insuaranceDao.CreateCustomerLIPortfolio(customerId, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateCustomerLIPortfolio()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerLIPortfolioId;
        }

        public int CreateCustomerGIAccount(GeneralInsuranceVo generalInsuranceVo, int userId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            int accountId = 0;
            try
            {
                accountId = insuranceDao.CreateCustomerGIAccount(generalInsuranceVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateCustomerGIAccount()");


                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return accountId;
        }

        public DataSet GetInsurancePolicyIssuerList()
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataSet ds = new DataSet();
            try
            {
                ds = insuranceDao.GetInsurancePolicyIssuerList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetInsurancePolicyIssuerList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataTable GetCustomerPropertyList(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetCustomerPropertyList(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetCustomerPropertyList()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetCustomerCollectiblesList(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetCustomerCollectiblesList(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetCustomerCollectiblesList()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetCustomerGoldList(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetCustomerGoldList(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetCustomerGoldList()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetCustomerPersonalItemsList(int customerId, string AssCatCode)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetCustomerPersonalItemsList(customerId, AssCatCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetCustomerPersonalItemsList()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetGIPolicyType()
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetGIPolicyType();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetGIPolicyType()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public int CreateGINetPosition(GeneralInsuranceVo generalInsuranceVo, int userId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            //bool bResult = false;
            int insuranceId = 0;
            try
            {
                //    bResult = insuranceDao.CreateInsurancePortfolio(insuranceVo, userId);
                insuranceId = insuranceDao.CreateGINetPosition(generalInsuranceVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateGINetPosition()");


                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            //return bResult;
            return insuranceId;
        }

        public bool UpdateGINetPosition(GeneralInsuranceVo generalInsuranceVo, int userId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            bool bResult = false;
            try
            {
                bResult = insuranceDao.UpdateGINetPosition(generalInsuranceVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:UpdateGINetPosition()");


                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool CreateGIAssetAssociation(GeneralInsuranceVo generalInsuranceVo, int userId)
        {
            bool bResult = false;

            InsuranceDao insuaranceDao = new InsuranceDao();
            try
            {
                bResult = insuaranceDao.CreateGIAssetAssociation(generalInsuranceVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateGIAssetAssociation()");


                object[] objects = new object[2];
                objects[0] = generalInsuranceVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public DataTable GetCustomerGIDetails(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetCustomerGIDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetCustomerGIDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public GeneralInsuranceVo GetGINetPositionDetails(int insuranceId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            GeneralInsuranceVo generalInsuranceVo = new GeneralInsuranceVo();

            try
            {
                generalInsuranceVo = insuranceDao.GetGINetPositionDetails(insuranceId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetGINetPositionDetails()");


                object[] objects = new object[1];
                objects[0] = insuranceId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return generalInsuranceVo;
        }

        public bool CreateGIAccountAssociation(CustomerAccountAssociationVo customerAccountAssociationVo, int userId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            bool bResult = false;
            try
            {
                bResult = insuranceDao.CreateGIAccountAssociation(customerAccountAssociationVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:CreateGIAccountAssociation()");


                object[] objects = new object[2];
                objects[0] = customerAccountAssociationVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;

        }

        public DataTable GetGIAccountAssociation(int accountId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetGIAccountAssociation(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetGIAccountAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public bool DeleteGIAccountAssociation(int accountId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            bool bresult = false;
            try
            {
                bresult = insuranceDao.DeleteGIAccountAssociation(accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:DeleteGIAccountAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bresult;
        }

        public DataTable GetGIAssetAssociation(int insuranceId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetGIAssetAssociation(insuranceId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetGIAssetAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public bool DeleteGIAssetAssociation(int insuranceId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            bool bresult = false;
            try
            {
                bresult = insuranceDao.DeleteGIAssetAssociation(insuranceId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:DeleteGIAssetAssociation()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bresult;
        }

        public DataSet GetGIIssuerList()
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataSet ds = new DataSet();
            try
            {
                ds = insuranceDao.GetGIIssuerList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetGIIssuerList()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        /// <summary>
        /// Function to retrieve all the Life Insurance and General Insurance of a Group for Group Dashboard
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetGrpInsuranceDetails(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataSet ds = new DataSet();
            try
            {
                ds = insuranceDao.GetGrpInsuranceDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetGrpInsuranceDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        /// <summary>
        /// Function to retrieve all the Life Insurance and General Insurance of a Customer for Customer Dashboard
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetCustomerDashboardInsuranceDetails(int customerId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataSet ds = new DataSet();
            try
            {
                ds = insuranceDao.GetCustomerDashboardInsuranceDetails(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetCustomerDashboardInsuranceDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }


        public DataSet GetAllProductMIS(int advisorId, int branchId, int rmId, int branchHeadId, int customerId, int isGroup)
        {
            InsuranceDao insuaranceDao = new InsuranceDao();
            DataSet dsAllProductMIS;
            try
            {
                dsAllProductMIS = insuaranceDao.GetAllProductMIS(advisorId, branchId, rmId, branchHeadId, customerId, isGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetAllProductMIS()");

                object[] objects = new object[6];
                objects[0] = advisorId;
                objects[1] = branchId;
                objects[2] = rmId;
                objects[3] = branchHeadId;
                objects[4] = customerId;
                objects[5] = isGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAllProductMIS;
        }

        public DataSet GetFixedincomeMISDetails(int advisorId, int branchId, int rmId, int branchHeadId, int customerId, int isGroup)
        {
            InsuranceDao insuaranceDao = new InsuranceDao();
            DataSet dsFixedIncome;
            try
            {
                dsFixedIncome = insuaranceDao.GetFixedincomeMISDetails(advisorId, branchId, rmId, branchHeadId, customerId, isGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetFixedincomeMISDetails()");

                object[] objects = new object[6];
                objects[0] = advisorId;
                objects[1] = branchId;
                objects[2] = rmId;
                objects[3] = branchHeadId;
                objects[4] = customerId;
                objects[5] = isGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFixedIncome;
        }

        public DataSet GetMultiProductMISInsuranceDetails(int advisorId, int branchId, int branchHeadId, int rmId, int customerId, string asset, int isGroup)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataSet ds = new DataSet();
            try
            {
                ds = insuranceDao.GetMultiProductMISInsuranceDetails(advisorId, branchId, branchHeadId, rmId, customerId, asset, isGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetMultiProductMISInsuranceDetails()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataTable GetInsuranceULIPPlans(int insuranceId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            DataTable dt = new DataTable();
            try
            {
                dt = insuranceDao.GetInsuranceULIPPlans(insuranceId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceBo.cs:GetInsuranceULIPPlans()");

                object[] objects = new object[1];
                objects[0] = insuranceId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public void InsertNewULIPPlan(string PlanName, string InsuranceIssuerCode)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                insuranceDao.InsertNewULIPPlan(PlanName, InsuranceIssuerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void InsertULIPInsuranceSchemeFund(string ulipSchemeFundName, int insuranceSchemeId, string issuerCode, int userId)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                insuranceDao.InsertULIPInsuranceSchemeFund(ulipSchemeFundName, insuranceSchemeId, issuerCode, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void InsertSchemeName(string schemeName, string InsuranceIssuerCode)
        {
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                insuranceDao.InsertSchemeName(schemeName, InsuranceIssuerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool UpdateULIPInsuranceSchemeFund(InsuranceULIPVo insuranceULIPvo)
        {
            bool bResult = false;
            InsuranceDao insuranceDao = new InsuranceDao();
            try
            {
                bResult = insuranceDao.UpdateULIPInsuranceSchemeFund(insuranceULIPvo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:UpdateULIPInsuranceSchemeFund()");
                object[] objects = new object[1];
                objects[0] = insuranceULIPvo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
    }
}
