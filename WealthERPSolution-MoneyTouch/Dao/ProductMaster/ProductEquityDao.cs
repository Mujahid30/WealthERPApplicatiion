using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using VoProductMaster;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoProductMaster
{
    public class ProductEquityDao
    {
        public List<ProductEquityVo> GetEquityScrips()
        {
            List<ProductEquityVo> equityScripsList = new List<ProductEquityVo>();

            ProductEquityVo productEquityVo;
            Database db;
            DbCommand getEquityScripsCmd;
            DataSet dsGetEquityScrips;
            DataTable dtGetEquityScrips;
 
            try
            {
                db=DatabaseFactory.CreateDatabase("wealtherp");
                getEquityScripsCmd=db.GetStoredProcCommand("getEquityScrips");
                dsGetEquityScrips=db.ExecuteDataSet(getEquityScripsCmd);
                dtGetEquityScrips=dsGetEquityScrips.Tables[0];

                foreach(DataRow dr in dtGetEquityScrips.Rows)
                {
                    productEquityVo=new ProductEquityVo();

                    productEquityVo.EquityCode=int.Parse(dr["PE_EquityCode"].ToString());
                    productEquityVo.CompanyName=dr["PE_CompanyName"].ToString();
                    productEquityVo.Exchange=dr["PE_Exchange"].ToString();
                    productEquityVo.NSECode=dr["PE_NSECode"].ToString();
                    productEquityVo.BSECode=dr["PE_BSECode"].ToString();

                    equityScripsList.Add(productEquityVo);
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

                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetEquityScrips()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return equityScripsList;
        }


        public List<ProductEquityVo> GetEquityCompanyScrips(string companyName)
        {
            List<ProductEquityVo> equityCompanyScripsList = new List<ProductEquityVo>();

            ProductEquityVo productEquityVo;
            Database db;
            DbCommand getEquityCompanyScripsCmd;
            DataSet dsGetEquityCompanyScrips;
            DataTable dtGetEquityCompanyScrips;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityCompanyScripsCmd = db.GetStoredProcCommand("getEquityCompanyScrips");
                db.AddInParameter(getEquityCompanyScripsCmd, "@PE_CompanyName", DbType.String, companyName);

                dsGetEquityCompanyScrips = db.ExecuteDataSet(getEquityCompanyScripsCmd);
                dtGetEquityCompanyScrips = dsGetEquityCompanyScrips.Tables[0];

                foreach (DataRow dr in dtGetEquityCompanyScrips.Rows)
                {
                    productEquityVo = new ProductEquityVo();

                    productEquityVo.EquityCode = int.Parse(dr["PE_EquityCode"].ToString());
                    productEquityVo.CompanyName = dr["PE_CompanyName"].ToString();
                    productEquityVo.Exchange = dr["PE_Exchange"].ToString();
                    productEquityVo.NSECode = dr["PE_NSECode"].ToString();
                    productEquityVo.BSECode = dr["PE_BSECode"].ToString();

                    equityCompanyScripsList.Add(productEquityVo);
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

                FunctionInfo.Add("Method", "ProductEquityDao.cs:GetEquityCompanyScrips()");


                object[] objects = new object[1];

                objects[0] = companyName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return equityCompanyScripsList;
        }

        public DataTable GetEquityScripPrefix(string prefixText)
        {
           
            Database db;
            DbCommand getEquityScripsCmd;
            DataSet dsGetEquityScrips;
            DataTable dtGetEquityScrip;
            string query;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                query = "Select top 50 PEM_CompanyName from ProductEquityMaster where PEM_CompanyName Like '" + prefixText + "%'";
                getEquityScripsCmd = db.GetSqlStringCommand(query);
                dsGetEquityScrips = db.ExecuteDataSet(getEquityScripsCmd);
                dtGetEquityScrip=dsGetEquityScrips.Tables[0];
              
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


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetEquityScrip;
        }

        public DataTable GetBrokerCode(int portfolioId, string tradeNumber)
        {
            Database db;
            DbCommand getBrokerCodeCmd;
            DataSet dsGetBrokerCode;
            DataTable dt;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBrokerCodeCmd = db.GetStoredProcCommand("SP_GetBrokerCode");
                db.AddInParameter(getBrokerCodeCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getBrokerCodeCmd, "@CETA_TradeAccountNum", DbType.String, tradeNumber);
                dsGetBrokerCode = db.ExecuteDataSet(getBrokerCodeCmd);
                dt = dsGetBrokerCode.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
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
        public DataSet GetScripCode(string scripName)
        {
            Database db;
            DbCommand getScripCmd;
            DataSet dsGetScrip;
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getScripCmd = db.GetStoredProcCommand("SP_GetScripCode");
                db.AddInParameter(getScripCmd, "@PEM_CompanyName", DbType.String, scripName);
                dsGetScrip = db.ExecuteDataSet(getScripCmd);
                //dr = dsGetScrip.Tables[0].Rows[0];
                //scripCode = int.Parse(dr["PEM_ScripCode"].ToString());
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

            return dsGetScrip;
        }

        public string GetScripName(int scripCode)
        {
            Database db;
            DbCommand getScripNameCmd;
            DataSet dsGetScripName;           
            string scripName;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getScripNameCmd = db.GetStoredProcCommand("SP_GetScripName");
                db.AddInParameter(getScripNameCmd, "@PEM_ScripCode", DbType.Int32, scripCode);
                dsGetScripName = db.ExecuteDataSet(getScripNameCmd);
                scripName = dsGetScripName.Tables[0].Rows[0]["PEM_CompanyName"].ToString();    
           
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
