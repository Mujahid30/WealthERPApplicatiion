using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCustomerPortfolio;
using VoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoCustomerPortfolio
{
    public class GovtSavingsBo
    {
        public bool CreateGovtSavingsNP(GovtSavingsVo govtSavingsVo, int userId)
        {
            bool bResult = false;
            GovtSavingsDao GovtSavingsDao = new GovtSavingsDao();
            try
            {
                bResult = GovtSavingsDao.CreateGovtSavingNetPosition(govtSavingsVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GovtSavingsBoBo.cs:CreateGovtSavingsNP");
                object[] objects = new object[2];
                objects[0] = govtSavingsVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public List<GovtSavingsVo> GetGovtSavingsNPList(int portfolioId, int CurrentPage, string sortOrder, out int count)
        {
            List<GovtSavingsVo> govtSavingsList = new List<GovtSavingsVo>();
            GovtSavingsDao govtSavingsDao = new GovtSavingsDao();
            try
            {
                govtSavingsList = govtSavingsDao.GetGovtSavingsNPList(portfolioId,CurrentPage,sortOrder,out count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GovtSavingsBo.cs:GetGovtSavingsNPList()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return govtSavingsList;
        }

        public GovtSavingsVo GetGovtSavingsDetails(int govtSavingsNPId)
        {
            GovtSavingsDao govtSavingsDao = new GovtSavingsDao();
            GovtSavingsVo govtSavingsVo;
            try
            {
                govtSavingsVo = govtSavingsDao.GetGovtSavingsDetails(govtSavingsNPId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GovtSavingsBo.cs:GetGovtSavingsDetails()");
                object[] objects = new object[1];
                objects[0] = govtSavingsNPId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return govtSavingsVo;
        }

        public bool UpdateGovtSavingsNP(GovtSavingsVo govtSavingsVo, int userId)
        {
            bool bResult = false;
            GovtSavingsDao govtSavingsDao = new GovtSavingsDao();
            try
            {
                bResult = govtSavingsDao.UpdateGovtSavingsNP(govtSavingsVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GovtSavingsBo.cs:UpdateGovtSavingsNP()");
                object[] objects = new object[2];
                objects[0] = govtSavingsVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool DeleteGovtSavingsPortfolio(int personalId)
        {
            bool bResult = false;
            GovtSavingsDao govtDao = new GovtSavingsDao();

            try
            {
                bResult = govtDao.DeleteGovtSavingsPortfolio(personalId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GovtSavingsBo.cs:DeleteGovtSavingsPortfolio()");
                object[] objects = new object[1];
                objects[0] = personalId;
                //objects[1] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        
        public bool UpdateGovtSavingsAccount(CustomerAccountsVo govtSavingsAccVo, int userId)
        {
            bool bResult = false;
            GovtSavingsDao govtSavingsDao = new GovtSavingsDao();
            try
            {
                bResult = govtSavingsDao.UpdateGovtSavingsAccount(govtSavingsAccVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GovtSavingsBo.cs:UpdateGovtSavingsAccount()");
                object[] objects = new object[2];
                objects[0] = govtSavingsAccVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
    }
}
