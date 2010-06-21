using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using DaoCustomerPortfolio;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoCustomerPortfolio
{
    public class CashAndSavingsBo
    {
        public List<CashAndSavingsVo> GetCustomerCashSavings(int portfolioId, int CurrentPage, string sortOrder, out int Count)
        {
            List<CashAndSavingsVo> customerCashSavingsList = new List<CashAndSavingsVo>();
            CashAndSavingsDao customerCashSavingsDao = new CashAndSavingsDao();

            try
            {
                customerCashSavingsList = customerCashSavingsDao.GetCustomerCashSavings(portfolioId,CurrentPage,sortOrder, out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerCashSavingsPortfolioBo.cs:GetCustomerCashSavings()");

                object[] objects = new object[1];
                objects[0] = portfolioId;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerCashSavingsList;

        }

        public bool AddCustomerCashSavingsDetails(CashAndSavingsVo custCashSavingsVo, int UserID)
        {
            bool blResult = false;
            CashAndSavingsDao customerCashSavingsPortfolioDao = new CashAndSavingsDao();

            try
            {
                blResult = customerCashSavingsPortfolioDao.AddCustomerCashSavingsDetails(custCashSavingsVo, UserID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerCashSavingsPorfolioBo.cs:AddCustomerCashSavingsDetails()");

                object[] objects = new object[1];
                objects[0] = custCashSavingsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public CashAndSavingsVo GetSpecificCashSavings(int CashSavingsPortfolioID, int CustomerID)
        {
            CashAndSavingsVo CashSavingsVo = new CashAndSavingsVo();
            CashAndSavingsDao CashSavingsDao = new CashAndSavingsDao();

            try
            {
                CashSavingsVo = CashSavingsDao.GetSpecificCashSavings(CashSavingsPortfolioID, CustomerID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerCashSavingsPorfolioBo.cs:GetSpecificCashSavings()");


                object[] objects = new object[1];
                objects[0] = CashSavingsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return CashSavingsVo;
        }

        public bool UpdateCashSavingsAccount(CustomerAccountsVo customerAccountVo, int userId)
        {
            bool bResult = false;
            CashAndSavingsDao cashSavingsDao = new CashAndSavingsDao();
            try
            {
                bResult = cashSavingsDao.UpdateCashSavingsAccount(customerAccountVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerCashSavingsPorfolioBo.cs:UpdateCashSavingsAccount()");

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

        public bool UpdateCashSavingsDetails(CashAndSavingsVo CashSavingsVo, int UserID)
        {
            bool blResult = false;
            CashAndSavingsDao CashSavingsDao = new CashAndSavingsDao();

            try
            {
                blResult = CashSavingsDao.UpdateCashSavingsDetails(CashSavingsVo, UserID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerCashSavingsPorfolioBo.cs:UpdateCashSavingsDetails()");
                object[] objects = new object[1];
                objects[0] = CashSavingsVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return blResult;
        }

        public bool DeleteCashSavingsPortfolio(int cashSavingsId, int accountId)
        {
            bool bResult = false;
            CashAndSavingsDao cashSavingsDao = new CashAndSavingsDao();

            try
            {
                bResult = cashSavingsDao.DeleteCashSavingsPortfolio(cashSavingsId, accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CashAndSavingsBo.cs:DeleteCashSavingsPortfolio()");
                object[] objects = new object[2];
                objects[0] = cashSavingsId;
                objects[1] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
    }
}
