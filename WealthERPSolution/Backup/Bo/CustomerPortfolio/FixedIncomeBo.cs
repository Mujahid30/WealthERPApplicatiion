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
    public class FixedIncomeBo
    {
        public bool CreateFixedIncome(FixedIncomeVo fixedincomeVo, int userId)
        {
            bool bResult = false;
            FixedIncomeDao fixedincomeDao = new FixedIncomeDao();

            try
            {
                bResult = fixedincomeDao.CreateFixedIncomePortfolio(fixedincomeVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FixedIncomeBo.cs:CreateFixedIncome()");
                object[] objects = new object[2];
                objects[0] = fixedincomeVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool UpdateFixedIncomePortfolio(FixedIncomeVo fixedIncomeVo, int userId)
        {
            bool bResult = false;
            FixedIncomeDao fixedIncomeDao = new FixedIncomeDao();
            try
            {
                bResult = fixedIncomeDao.UpdateFixedIncomePortfolio(fixedIncomeVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FixedIncomeBo.cs:UpdateFixedIncomePortfolio()");
                object[] objects = new object[2];
                objects[0] = fixedIncomeVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteFixedIncomePortfolio(int personalId, int accountId)
        {
            bool bResult = false;
            FixedIncomeDao fixedIncomeDao = new FixedIncomeDao();

            try
            {
                bResult = fixedIncomeDao.DeleteFixedIncomePortfolio(personalId, accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FixedIncomeBo.cs:DeleteFixedIncomePortfolio()");
                object[] objects = new object[2];
                objects[0] = personalId;
                objects[1] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

        public bool UpdateFixedIncomeAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            bool bResult = false;
            FixedIncomeDao fixedIncomeDao = new FixedIncomeDao();
            try
            {
                bResult = fixedIncomeDao.UpdateFixedIncomeAccount(customerAccountVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {

                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FixedIncomeBo.cs:UpdateFixedIncomeAccount()");
                object[] objects = new object[2];
                objects[0] = customerAccountVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public FixedIncomeVo GetFixedIncomePortfolio(int portfolioId)
        {
            FixedIncomeVo fixedIncomeVo = new FixedIncomeVo();
            FixedIncomeDao fixedIncomeDao = new FixedIncomeDao();
            try
            {
                fixedIncomeVo = fixedIncomeDao.GetFixedIncomePortfolio(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {

                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FixedIncomeBo.cs:GetFixedIncomePortfolio()");
                object[] objects = new object[1];
                objects[0] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return fixedIncomeVo;  
        }

        public List<FixedIncomeVo> GetFixedIncomePortfolioList(int customerId, string SortOrder)
        {
            List<FixedIncomeVo> fixedincomeList = new List<FixedIncomeVo>();
            FixedIncomeDao fixedincomeDao = new FixedIncomeDao();
            try
            {
                fixedincomeList = fixedincomeDao.GetFixedIncomePortfolioList(customerId, SortOrder);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "FixedIncomeBo.cs:GetFixedIncomePortfolioList()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return fixedincomeList;
        }
    }
}
