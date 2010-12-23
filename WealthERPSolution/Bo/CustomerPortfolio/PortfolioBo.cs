using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoCustomerPortfolio;
using VoUser;
using DaoCustomerPortfolio;
using BoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;

namespace BoCustomerPortfolio
{
    public class PortfolioBo
    {

        public DateTime? GetLatestValuationDate(int adviserID, string assetGroup)
        {
            DateTime? valuationDate = new DateTime?();
            
            PortfolioDao portfolioDao = new PortfolioDao();
            try
            {
                valuationDate = portfolioDao.GetLatestValuationDate(adviserID, assetGroup);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:GetLatestValuationDate()");


                object[] objects = new object[2];
                objects[0] = adviserID;
                objects[1] = assetGroup;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return valuationDate;
        }
        public bool CreateCustomerPortfolio(CustomerPortfolioVo customerPortfolioVo, int userId)
        {
            bool bResult = false;
            
            PortfolioDao portfolioDao = new PortfolioDao();
            try
            {
                bResult = portfolioDao.CreateCustomerPortfolio(customerPortfolioVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:CreateCustomerPortfolio()");


                object[] objects = new object[2];
                objects[0] = customerPortfolioVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }
        public bool UpdateCustomerPortfolio(CustomerPortfolioVo customerPortfolioVo, int userId)
        {

            bool bResult = false;
            PortfolioDao portfolioDao = new PortfolioDao();
            try
            {
                bResult = portfolioDao.UpdateCustomerPortfolio(customerPortfolioVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PortfolioBo.cs:UpdateCustomerPortfolio()");
                object[] objects = new object[2];
                objects[0] = customerPortfolioVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
            
        }
        public DataSet GetCustomerPortfolio(int customerId)
        {
            DataSet dsGetCustomerPortfolio = new DataSet();
            PortfolioDao portfolioDao = new PortfolioDao();

            try
            {
                dsGetCustomerPortfolio = portfolioDao.GetCustomerPortfolio(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:GetCustomerPortfolio()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerPortfolio;
            
        }
        public List<CustomerPortfolioVo> GetCustomerPortfolios(int customerId)
        {
            List<CustomerPortfolioVo> customerPortfolioVoList = new List<CustomerPortfolioVo>();
            PortfolioDao portfolioDao = new PortfolioDao();

            try
            {
                customerPortfolioVoList = portfolioDao.GetCustomerPortfolios(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:GetCustomerPortfolios()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerPortfolioVoList;
        }
        public CustomerPortfolioVo GetCustomerDefaultPortfolio(int customerId)
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            PortfolioDao portfolioDao = new PortfolioDao();
            try
            {
                customerPortfolioVo = portfolioDao.GetCustomerDefaultPortfolio(customerId);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:GetCustomerDefaultPortfolio()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerPortfolioVo;

        }


        public CustomerPortfolioVo GetCustomerDefaultPortfolio1(int customerId, string portfolio)
        {
            CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
            PortfolioDao portfolioDao = new PortfolioDao();
            try
            {
                customerPortfolioVo = portfolioDao.GetCustomerDefaultPortfolio1(customerId, portfolio);
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:GetCustomerDefaultPortfolio()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerPortfolioVo;

        }
        public DataSet GetCustomerPortfolioDetails(int portfolioId)
        {
            DataSet dsGetCustomerPortfolio = new DataSet();
            PortfolioDao portfolioDao = new PortfolioDao();

            try
            {
                dsGetCustomerPortfolio = portfolioDao.GetCustomerPortfolioDetails(portfolioId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:GetCustomerPortfolioDetails()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCustomerPortfolio;

        }
        public DataTable GetRMCustomerPortfolios(int rmId,int currentPage,out int count,string nameSrchValue)
        {
            DataTable dtGetCustomerPortfolios = new DataTable();
            PortfolioDao portfolioDao = new PortfolioDao();

            try
            {
                dtGetCustomerPortfolios = portfolioDao.GetRMCustomerPortfolios(rmId,currentPage,out count,nameSrchValue);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PortfolioBo.cs:GetRMCustomerPortfolios()");


                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerPortfolios;

        }

        public static bool TransferFolio(int MFAccountId, int newPortfolioId)
        {
            return PortfolioDao.TransferFolio(MFAccountId, newPortfolioId);
        }

        public int CustomerPortfolioCheck(string association, string Flag)
        {

            PortfolioDao portfolioDao = new PortfolioDao();

            return portfolioDao.CustomerPortfolioCheck(association,Flag);


        }
        public int CustomerPortfolioMultiple(string association, string Flag)
        {

            PortfolioDao portfolioDao = new PortfolioDao();

            return portfolioDao.CustomerPortfolioMultiple(association,Flag);


        }
        public DataSet CustomerPortfolioNumber(string association, string Flag)
        {

            PortfolioDao portfolioDao = new PortfolioDao();

            return portfolioDao.CustomerPortfolioNumber(association, Flag);

        }

        public int PortfolioDissociate(string association, string toPortfolio, string Flag)
        {

            PortfolioDao portfolioDao = new PortfolioDao();

            return portfolioDao.PortfolioDissociate(association, toPortfolio, Flag);


        }

        public int PortfolioDissociateUnmanaged(string association, string Flag)
        {

            PortfolioDao portfolioDao = new PortfolioDao();

            return portfolioDao.PortfolioDissociateUnmanaged(association, Flag);


        }

        public int CustomerPortfolioDefault(string association, string Flag)
        {

            PortfolioDao portfolioDao = new PortfolioDao();

            return portfolioDao.CustomerPortfolioDefault(association, Flag);


        }
    }
}
