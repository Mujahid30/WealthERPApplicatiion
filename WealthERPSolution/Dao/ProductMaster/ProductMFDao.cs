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
    public class ProductMFDao
    {
        public List<ProductMFSchemePlanVo> GetSchemePlans()
        {
            List<ProductMFSchemePlanVo> mfSchemePlanList = new List<ProductMFSchemePlanVo>();

            ProductMFSchemePlanVo productMFSchemePlanVo;
            Database db;
            DbCommand getMFSchemesCmd;
            DataSet dsGetMFSchemes;
            DataTable dtGetMFScheme;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFSchemesCmd = db.GetStoredProcCommand("getMFSchemes");
                dsGetMFSchemes = db.ExecuteDataSet(getMFSchemesCmd);
                dtGetMFScheme = dsGetMFSchemes.Tables[0];

                foreach (DataRow dr in dtGetMFScheme.Rows)
                {
                    productMFSchemePlanVo = new ProductMFSchemePlanVo();

                    productMFSchemePlanVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    productMFSchemePlanVo.SchemePlan = dr["PASP_SchemePlan"].ToString();
                    productMFSchemePlanVo.SchemeCode = int.Parse(dr["PAS_SchemeCode"].ToString());
                    productMFSchemePlanVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());


                    mfSchemePlanList.Add(productMFSchemePlanVo);
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

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetSchemePlans()");


                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return mfSchemePlanList;
        }


        public List<ProductMFSchemePlanVo> GetAMCSchemePlans(int amcCode)
        {
            List<ProductMFSchemePlanVo> amcSchemePlanList = new List<ProductMFSchemePlanVo>();

            ProductMFSchemePlanVo productMFSchemePlanVo;
            Database db;
            DbCommand getAMCSchemesCmd;
            DataSet dsGetAMCSchemes;
            DataTable dtGetAMCScheme;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAMCSchemesCmd = db.GetStoredProcCommand("getAMCSchemes");
                db.AddInParameter(getAMCSchemesCmd, "@PA_AMCCode", DbType.String, amcCode);

                dsGetAMCSchemes = db.ExecuteDataSet(getAMCSchemesCmd);
                dtGetAMCScheme = dsGetAMCSchemes.Tables[0];

                foreach (DataRow dr in dtGetAMCScheme.Rows)
                {
                    productMFSchemePlanVo = new ProductMFSchemePlanVo();

                    productMFSchemePlanVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    productMFSchemePlanVo.SchemePlan = dr["PASP_SchemePlan"].ToString();
                    productMFSchemePlanVo.SchemeCode = int.Parse(dr["PAS_SchemeCode"].ToString());
                    productMFSchemePlanVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());

                    amcSchemePlanList.Add(productMFSchemePlanVo);
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

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetAMCSchemePlans()");


                object[] objects = new object[0];
                objects[0] = amcCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }




            return amcSchemePlanList;
        }

        public List<ProductMFSchemePlanVo> GetCodeSchemePlans(int schemeCode)
        {
            List<ProductMFSchemePlanVo> codeSchemePlanList = new List<ProductMFSchemePlanVo>();

            ProductMFSchemePlanVo productMFSchemePlanVo;
            Database db;
            DbCommand getCodeSchemesCmd;
            DataSet dsGetCodeSchemes;
            DataTable dtGetCodeScheme;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCodeSchemesCmd = db.GetStoredProcCommand("getCodeSchemes");
                db.AddInParameter(getCodeSchemesCmd, "@PAS_SchemeCode", DbType.String, schemeCode);


                dsGetCodeSchemes = db.ExecuteDataSet(getCodeSchemesCmd);
                dtGetCodeScheme = dsGetCodeSchemes.Tables[0];

                foreach (DataRow dr in dtGetCodeScheme.Rows)
                {
                    productMFSchemePlanVo = new ProductMFSchemePlanVo();

                    productMFSchemePlanVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    productMFSchemePlanVo.SchemePlan = dr["PASP_SchemePlan"].ToString();
                    productMFSchemePlanVo.SchemeCode = int.Parse(dr["PAS_SchemeCode"].ToString());
                    productMFSchemePlanVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());

                    codeSchemePlanList.Add(productMFSchemePlanVo);
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

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetCodeSchemePlans()");


                object[] objects = new object[0];
                objects[0] = schemeCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return codeSchemePlanList;
        }

        public int GetScheme(string schemePlanName)
        {
            Database db;
            DbCommand getSchemeCmd;
            DataSet dsGetScheme;
            DataRow dr;
            int schemePlanCode;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeCmd = db.GetStoredProcCommand("SP_GetSchemeCode");
                db.AddInParameter(getSchemeCmd, "@PASP_SchemePlan", DbType.String, schemePlanName);
                dsGetScheme = db.ExecuteDataSet(getSchemeCmd);
                dr = dsGetScheme.Tables[0].Rows[0];
                schemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetScheme()");


                object[] objects = new object[1];
                objects[0] = schemePlanName;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return schemePlanCode;
        }

        public DataSet GetSchemeDetails(string schemePlan)
        {
            Database db;
            DbCommand getSchemeDetailsCmd;
            DataSet dsGetSchemeDetails;
            
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeDetailsCmd = db.GetStoredProcCommand("SP_GetSchemeDetails");
                db.AddInParameter(getSchemeDetailsCmd, "@PASP_SchemePlanCode", DbType.String, schemePlan);
                dsGetSchemeDetails = db.ExecuteDataSet(getSchemeDetailsCmd);               
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetSchemeDetails()");


                object[] objects = new object[1];
                objects[0] = schemePlan;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetSchemeDetails;
        }

        public DataSet GetBrokerCodeForLOB()
        {
            Database db;
            DbCommand getBrokerCode;
            DataSet dsGetBrokerCode;
            try
            {
                db=DatabaseFactory.CreateDatabase("wealtherp");
                getBrokerCode = db.GetStoredProcCommand("SP_GetBrokerCodeForLOB");
                dsGetBrokerCode = db.ExecuteDataSet(getBrokerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetBrokerCodeForLOB()");


                object[] objects = new object[0];
               

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetBrokerCode;
        }

        public DataSet GetProductAmc()
        {
            Database db;
            DbCommand getProductAmcCmd;
            DataSet dsGetProductAmc;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getProductAmcCmd = db.GetStoredProcCommand("SP_GetProductAmc");
                dsGetProductAmc = db.ExecuteDataSet(getProductAmcCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetProductAmc()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetProductAmc;
        }

        public DataTable GetSchemePlanPrefix(string prefixText)
        {

            Database db;
            DbCommand getMFSchemesCmd;
            DataSet dsGetMFSchemes;
            DataTable dtGetMFScheme;
            

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFSchemesCmd = db.GetStoredProcCommand("SP_GetMutualFundSchemes");
                db.AddInParameter(getMFSchemesCmd, "@PrefixText", DbType.String, prefixText);
                dsGetMFSchemes = db.ExecuteDataSet(getMFSchemesCmd);
                dtGetMFScheme = dsGetMFSchemes.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetSchemePlanPrefix()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }



            return dtGetMFScheme;
        }
        public DataTable GetSwitchSchemePlanPrefix(string prefixText,int SchemePlanCode)
        {

            Database db;
            DbCommand getMFSchemesCmd;
            DataSet dsGetMFSchemes;
            DataTable dtGetMFScheme;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFSchemesCmd = db.GetStoredProcCommand("SP_GetMutualFundSwitchSchemes");
                db.AddInParameter(getMFSchemesCmd, "@PrefixText", DbType.String, prefixText);
                db.AddInParameter(getMFSchemesCmd, "@SchemePlanCode", DbType.Int32, SchemePlanCode);
                dsGetMFSchemes = db.ExecuteDataSet(getMFSchemesCmd);
                dtGetMFScheme = dsGetMFSchemes.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetSwitchSchemePlanPrefix(string prefixText,int SchemePlanCode)");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }



            return dtGetMFScheme;
        }
        public DataSet GetProductAssetCategory()
        {
            Database db;
            DbCommand getAssetCategoryCmd;
            DataSet dsAssetCategory;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetCategoryCmd = db.GetStoredProcCommand("SP_GetProductAssetCategory");
                dsAssetCategory = db.ExecuteDataSet(getAssetCategoryCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetProductAssetCategory()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAssetCategory;
        }

        public DataSet GetSchemeName(int amcCode, string categoryCode, int all, int status)
        {
            Database db;
            DbCommand getSchemeNameCmd;
            DataSet dsSchemeName;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeNameCmd = db.GetStoredProcCommand("SP_GetSchemeName");
                db.AddInParameter(getSchemeNameCmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(getSchemeNameCmd, "@categoryCode", DbType.String, categoryCode);
                db.AddInParameter(getSchemeNameCmd, "@All", DbType.Int16, all);
                db.AddInParameter(getSchemeNameCmd, "@staus", DbType.Int16, status);
                dsSchemeName = db.ExecuteDataSet(getSchemeNameCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetProductAssetCategory()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeName;
        }

        public DataSet GetFolioNumber(int portfolioId, int amcCode, int all)
        {
            Database db;
            DbCommand getfolionumberCmd;
            DataSet dsFolioNumber;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getfolionumberCmd = db.GetStoredProcCommand("SP_GetFolioNumber");
                db.AddInParameter(getfolionumberCmd, "@portfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getfolionumberCmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(getfolionumberCmd, "@all", DbType.Int16, all);
                dsFolioNumber = db.ExecuteDataSet(getfolionumberCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFDao.cs:GetProductAssetCategory()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFolioNumber;
        }
        public int GetAMCfromFolioNo(int accountId)
        {
            Database db;
            DbCommand getamcfromfolioNoCmd;
            DataSet dsGetAMC;
            DataRow dr;
            int amcCode;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getamcfromfolioNoCmd = db.GetStoredProcCommand("SP_GetAMCFromFolioNo");
                db.AddInParameter(getamcfromfolioNoCmd, "@accountId", DbType.Int32, accountId);
                dsGetAMC = db.ExecuteDataSet(getamcfromfolioNoCmd);
                dr = dsGetAMC.Tables[0].Rows[0];
                amcCode = int.Parse(dr["PA_AMCCode"].ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetAMCfromFolioNo()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return amcCode;
        }
        public string GetCategoryNameFromSChemeCode(int schemePlanCode)
        {
            Database db;
            DbCommand getcategoryCmd;
            DataSet dsGetCategory;
            DataRow dr;
            string category;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getcategoryCmd = db.GetStoredProcCommand("SP_GetCategoryNameFromSchemeCade");
                db.AddInParameter(getcategoryCmd, "@schemeplanCode", DbType.Int32, schemePlanCode);
                dsGetCategory = db.ExecuteDataSet(getcategoryCmd);
                dr = dsGetCategory.Tables[0].Rows[0];
                category = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetAMCfromFolioNo()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return category;
        }

    }
}
