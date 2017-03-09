using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VoProductMaster;
using DaoProductMaster;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoProductMaster
{
    public class ProductEquityBo
    {
        public List<ProductEquityVo> GetEquityScrips()
        {
            ProductEquityDao productEquityDao = new ProductEquityDao();

            List<ProductEquityVo> productEquityList = new List<ProductEquityVo>();
            try
            {
                productEquityList = productEquityDao.GetEquityScrips();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetEquityScrips()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return productEquityList;
        }

        public DataSet  GetScripCode(string scripName)
        {
            ProductEquityDao productEquityDao = new ProductEquityDao();
            DataSet dsGetScrips;
            try
            {
                dsGetScrips = productEquityDao.GetScripCode(scripName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetScripCode()");


                object[] objects = new object[1];
                objects[0] = scripName;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetScrips;
        }
        public List<ProductEquityVo> GetEquityCompanyScrips(string companyName)
        {
            ProductEquityDao productEquityDao = new ProductEquityDao();

            List<ProductEquityVo> equityCompanyList = new List<ProductEquityVo>();
            try
            {
                equityCompanyList = productEquityDao.GetEquityCompanyScrips(companyName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetEquityCompanyScrips()");


                object[] objects = new object[1];

                objects[0] = companyName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return equityCompanyList;
        }

        public DataTable GetEquityScripPrefix(string prefixText)
        {
            ProductEquityDao productEquityDao = new ProductEquityDao();

            DataTable dtproductEquityScrips = new DataTable();
            try
            {
                dtproductEquityScrips = productEquityDao.GetEquityScripPrefix(prefixText);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetEquityScripPrefix()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtproductEquityScrips;
        }

        public DataTable GetBrokerCode(int portfolioId, string tradeNumber)
        {
            DataTable dt = new DataTable();
            ProductEquityDao productEquityDao = new ProductEquityDao();
            try
            {
                dt = productEquityDao.GetBrokerCode(portfolioId, tradeNumber);
            }
            catch(BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch(Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetBrokerCode()");
                object[] objects = new object[2];
                objects[0] = portfolioId;
                objects[1] = tradeNumber;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public string GetScripName(int scripCode)
        {
            ProductEquityDao productEquityDao = new ProductEquityDao();
            string scripName = "";
            try
            {
                scripName = productEquityDao.GetScripName(scripCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetScripName()");
                object[] objects = new object[1];
                objects[0] = scripCode;                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return scripName;
        }
    }
}
