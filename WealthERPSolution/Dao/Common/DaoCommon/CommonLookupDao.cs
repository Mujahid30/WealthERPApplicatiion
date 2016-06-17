using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using VoOnlineOrderManagemnet;

namespace DaoCommon
{
    public class CommonLookupDao
    {
        public DataTable GetMFInstrumentSubCategory(string categoryCode)
        {
            Database db;
            DbCommand cmdGetMFSubCategory;
            DataSet dsSubcategory = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetMFSubCategory = db.GetStoredProcCommand("SPROC_GetMFInstrumentSubCategory");
                db.AddInParameter(cmdGetMFSubCategory, "@InstrumentCategoryCode", DbType.String, categoryCode);
                dsSubcategory = db.ExecuteDataSet(cmdGetMFSubCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetMFInstrumentSubCategory(string categoryCode)");
                object[] objects = new object[1];
                objects[0] = categoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSubcategory.Tables[0];
        }

        /// <summary> 
        /// Gets the list of AMC</summary> 
        /// <param name="AmcCode">AMCCode for the desired AMC. '0' to return list of all AMCs</param>
        /// <returns> 
        /// DataTable containing AMC list that have only schemes</returns> 
        public DataTable GetProductAmc(int AmcCode, bool hasOnlineShcemes)
        {
            Database db;
            DbCommand cmdGetProdAmc;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProdAmc = db.GetStoredProcCommand("SP_GetProductAmc");
                if (AmcCode > 0) db.AddInParameter(cmdGetProdAmc, "@PA_AMCCode", DbType.Int32, AmcCode);
                if (hasOnlineShcemes) db.AddInParameter(cmdGetProdAmc, "@onlyOnlineSchemes", DbType.Boolean, hasOnlineShcemes);
                ds = db.ExecuteDataSet(cmdGetProdAmc);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProdAmc(int AmcCode)");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        /// <summary> 
        /// Gets the list of Products</summary> 
        /// <param name="AmcCode">ProductCode for the desired Product. EmptyString to return list of all Products</param>
        /// <returns> 
        /// DataTable containing Product list</returns>
        public DataTable GetProductList(string ProductCode)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetProductTypeCM");
                if (string.IsNullOrEmpty(ProductCode) == false) db.AddInParameter(cmd, "@PAG_AssetGroupCode", DbType.String, ProductCode);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductList(string ProductCode)");
                object[] objParams = new object[1];
                objParams[0] = ProductCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        /// <summary> 
        /// Gets the list of Products</summary> 
        /// <param name="AmcCode">ProductCode for the desired Product. EmptyString to return list of all Products</param>
        /// <returns> 
        /// DataTable containing Product list</returns>
        public DataTable GetProductCategories(string ProductCode, string CategoryCode)
        {
            Database db;
            DbCommand cmdGetCatCm;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCatCm = db.GetStoredProcCommand("SPROC_GetCategoryCM");
                if (string.IsNullOrEmpty(ProductCode) == false) db.AddInParameter(cmdGetCatCm, "@PAG_AssetGroupCode", DbType.String, ProductCode);
                if (string.IsNullOrEmpty(CategoryCode) == false) db.AddInParameter(cmdGetCatCm, "@PAIC_AssetInstrumentCategoryCode", DbType.String, CategoryCode);
                ds = db.ExecuteDataSet(cmdGetCatCm);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductCategories(string ProductCode, string CategoryCode)");
                object[] objects = new object[2];
                objects[0] = ProductCode;
                objects[1] = CategoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        /// <summary> 
        /// Gets list of categories</summary> 
        /// <param name="ProductCode">ProductCode for the desired Product. EmptyString to return list of all Products</param>
        /// <param name="CategoryCode">CategoryCode for the desired Category. EmptyString to return list of all Products</param>
        /// <param name="hasOnlineSchemes">ProductCode for the desired Product. EmptyString to return list of all Products</param>
        /// <returns> 
        /// DataTable containing Product list</returns>
        public DataTable GetProductCategories(string ProductCode, string CategoryCode, bool hasOnlineSchemes)
        {
            Database db;
            DbCommand cmdGetCatCm;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCatCm = db.GetStoredProcCommand("SPROC_GetCategoryCM");
                if (string.IsNullOrEmpty(ProductCode) == false) db.AddInParameter(cmdGetCatCm, "@PAG_AssetGroupCode", DbType.String, ProductCode);
                if (string.IsNullOrEmpty(CategoryCode) == false) db.AddInParameter(cmdGetCatCm, "@PAIC_AssetInstrumentCategoryCode", DbType.String, CategoryCode);
                ds = db.ExecuteDataSet(cmdGetCatCm);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductCategories(string ProductCode, string CategoryCode)");
                object[] objects = new object[2];
                objects[0] = ProductCode;
                objects[1] = CategoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        /// <summary> 
        /// Gets the list of Product Sub-Categories</summary> 
        /// <param name="ProductCode">ProductCode for the desired Product. EmptyString to return list of all Products</param>
        /// <param name="CategoryCode">CategoryCode for the desired Category. EmptyString to return list of all Categories</param>
        /// <param name="SubCategoryCode">SubCategoryCode for the desired SubCategory. EmptyString to return list of all SubCategories</param>
        /// <returns> 
        /// DataTable containing Product list</returns>
        public DataTable GetProductSubCategories(string ProductCode, string CategoryCode, string SubCategoryCode)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetSubCategoryCM");
                if (string.IsNullOrEmpty(ProductCode) == false) db.AddInParameter(cmd, "@PAG_AssetGroupCode", DbType.String, ProductCode);
                if (string.IsNullOrEmpty(CategoryCode) == false) db.AddInParameter(cmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, CategoryCode);
                if (string.IsNullOrEmpty(SubCategoryCode) == false) db.AddInParameter(cmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, SubCategoryCode);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductSubCategories(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objects = new object[3];
                objects[0] = ProductCode;
                objects[1] = CategoryCode;
                objects[2] = SubCategoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        public DataTable GetFolioNumberForSIP(int AmcCode, int CustomerId,int IsDemat)
        {
            Database db;
            DbCommand cmd;
            DataSet dsGetAmcSchemeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetFolioNumberForSIP");

                db.AddInParameter(cmd, "@amcCode", DbType.Int32, AmcCode);
                db.AddInParameter(cmd, "@CustomerId", DbType.Int32, CustomerId);

                db.AddInParameter(cmd, "@IsDemat", DbType.Int32, IsDemat);

                dsGetAmcSchemeList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductSubCategories(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objects = new object[3];
                objects[0] = AmcCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAmcSchemeList.Tables[0];
        }

        public DataTable Get_Offline_AllSIPDataForOrder(int schemdCode, string frequencyCode, string systematicType)
        {
            Database db;
            DbCommand cmd;
            DataSet dsGetAmcSchemeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_Offline_GetAllSIPDataForOrder");

                db.AddInParameter(cmd, "@PASP_SchemePlanCode", DbType.Int32, schemdCode);
                if (frequencyCode != "0")
                    db.AddInParameter(cmd, "@FrequencyCode", DbType.String, frequencyCode);
                db.AddInParameter(cmd, "@SystematicType", DbType.String, systematicType);

                dsGetAmcSchemeList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetAllSIPDataForOrder(int schemdCode,string frequencyCode)");
                object[] objects = new object[3];
                objects[0] = schemdCode;
                objects[1] = frequencyCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAmcSchemeList.Tables[0];
        }

        public List<OnlineMFOrderVo> GetAllSIPDataForOrderEdit(int orderIDForEdit, int customerIdForEdit)
        {
            List<OnlineMFOrderVo> SIPDataForOrderEditList = new List<OnlineMFOrderVo>();
            OnlineMFOrderVo OnlineMFOrderVo = new OnlineMFOrderVo();
            Database db;
            DbCommand getAdvisorBranchCmd;
            DataSet getAdvisorBranchDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                getAdvisorBranchCmd = db.GetStoredProcCommand("SPROC_GetAllSIPDataForOrderEdit");
                db.AddInParameter(getAdvisorBranchCmd, "@CO_OrderId", DbType.Int32, orderIDForEdit);
                db.AddInParameter(getAdvisorBranchCmd, "@C_CustomerId", DbType.Int32, customerIdForEdit);

                getAdvisorBranchDs = db.ExecuteDataSet(getAdvisorBranchCmd);

                if (getAdvisorBranchDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getAdvisorBranchDs.Tables[0].Rows)
                    {
                        OnlineMFOrderVo = new OnlineMFOrderVo();

                        OnlineMFOrderVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        OnlineMFOrderVo.AccountId = int.Parse(dr["CMFA_accountid"].ToString());
                        OnlineMFOrderVo.SystematicTypeCode = dr["XSTT_SystematicTypeCode"].ToString();
                        OnlineMFOrderVo.SystematicDate = int.Parse(dr["CMFSS_SystematicDate"].ToString());

                        OnlineMFOrderVo.Amount = Convert.ToDouble(dr["CMFOD_Amount"].ToString());
                        OnlineMFOrderVo.SourceCode = dr["XES_SourceCode"].ToString();
                        OnlineMFOrderVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                        OnlineMFOrderVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        OnlineMFOrderVo.StartDate = Convert.ToDateTime(dr["CMFOD_StartDate"].ToString());
                        OnlineMFOrderVo.EndDate = Convert.ToDateTime(dr["CMFOD_EndDate"].ToString());
                        OnlineMFOrderVo.SystematicDates = dr["CMFSS_SystematicDate"].ToString();
                        OnlineMFOrderVo.AssetGroup = dr["PA_AMCCode"].ToString();
                        OnlineMFOrderVo.Folio = dr["CMFA_FolioNum"].ToString();
                        OnlineMFOrderVo.MinDues = Convert.ToInt32(getAdvisorBranchDs.Tables[1].Rows[0]["PASPSD_MinDues"].ToString());
                        OnlineMFOrderVo.MaxDues = Convert.ToInt32(getAdvisorBranchDs.Tables[1].Rows[0]["PASPSD_MaxDues"].ToString());
                        SIPDataForOrderEditList.Add(OnlineMFOrderVo);
                    }
                }
                else
                    SIPDataForOrderEditList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorBranchDao.cs:GetAdvisorBranches()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return SIPDataForOrderEditList;
        }

        public DataTable GetFrequencyDetails()
        {
            Database db;
            DbCommand cmd;
            DataSet dsGetAmcSchemeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetFrequencyDetails");


                dsGetAmcSchemeList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductSubCategories(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objects = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAmcSchemeList.Tables[0];
        }


        public DataTable GetAllSIPDataForOrder(int schemdCode,string frequencyCode,string systematicType)
        {
            Database db;
            DbCommand cmd;
            DataSet dsGetAmcSchemeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetAllSIPDataForOrder");

                db.AddInParameter(cmd, "@PASP_SchemePlanCode", DbType.Int32, schemdCode);
                if (frequencyCode!="0")
                db.AddInParameter(cmd, "@FrequencyCode", DbType.String, frequencyCode);
                db.AddInParameter(cmd, "@SystematicType", DbType.String, systematicType);
                
                dsGetAmcSchemeList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetAllSIPDataForOrder(int schemdCode,string frequencyCode)");
                object[] objects = new object[3];
                objects[0] = schemdCode;
                objects[1] = frequencyCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAmcSchemeList.Tables[0];
        }

        //GetAmcSipSchemeList
        public DataTable GetAmcSipSchemeList(int AmcCode, string Category)
        {
            Database db;
            DbCommand cmd;
            DataSet dsGetAmcSchemeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetSipSchemes");
                db.AddInParameter(cmd, "@PA_AMCCode", DbType.Int32, AmcCode);
                if (!string.IsNullOrEmpty(Category)) db.AddInParameter(cmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, Category);
                dsGetAmcSchemeList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductSubCategories(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objects = new object[3];
                objects[0] = AmcCode;
                objects[1] = Category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAmcSchemeList.Tables[0];
        }
        public DataTable GetSwitchAmcSchemeList(int AmcCode, string Category, int customerid,char switchSchemeType, char txnType,int schemePlanCode)
        {
            Database db;
            DbCommand cmd;
            DataSet dsGetAmcSchemeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetSwitchSchemeFromOverAllCategoryListForOnlOrder");
                if (AmcCode != 0)
                    db.AddInParameter(cmd, "@amcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(cmd, "@amcCode", DbType.Int32, 0);
                if (Category != "0")
                    db.AddInParameter(cmd, "@categoryCode", DbType.String, Category);
                else
                    db.AddInParameter(cmd, "@categoryCode", DbType.String, DBNull.Value);
                if (customerid != 0)
                    db.AddInParameter(cmd, "@customerid", DbType.Int32, customerid);
                else
                    db.AddInParameter(cmd, "@customerid", DbType.Int32, DBNull.Value);

                db.AddInParameter(cmd, "@TxnType", DbType.String, txnType);
                db.AddInParameter(cmd, "@switchSchemeType", DbType.String, switchSchemeType);
                if (schemePlanCode != 0)
                    db.AddInParameter(cmd, "@PASP_SchemePlanCode", DbType.Int32, schemePlanCode);
                else
                    db.AddInParameter(cmd, "@PASP_SchemePlanCode", DbType.Int32, DBNull.Value);
                dsGetAmcSchemeList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductSubCategories(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objects = new object[3];
                objects[0] = AmcCode;
                objects[1] = Category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAmcSchemeList.Tables[0];
        }
        public DataTable GetAmcSchemeList(int AmcCode, string Category, int customerid, char txnType)
        {
            Database db;
            DbCommand cmd;
            DataSet dsGetAmcSchemeList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetSchemeFromOverAllCategoryListForOnlOrder");
                if (AmcCode != 0)
                    db.AddInParameter(cmd, "@amcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(cmd, "@amcCode", DbType.Int32, 0);
                if (Category != "0")
                    db.AddInParameter(cmd, "@categoryCode", DbType.String, Category);
                else
                    db.AddInParameter(cmd, "@categoryCode", DbType.String, DBNull.Value);
                if (customerid != 0)
                    db.AddInParameter(cmd, "@customerid", DbType.Int32, customerid);
                else
                    db.AddInParameter(cmd, "@customerid", DbType.Int32, DBNull.Value);

                db.AddInParameter(cmd, "@TxnType", DbType.String, txnType);
                dsGetAmcSchemeList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetProductSubCategories(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objects = new object[3];
                objects[0] = AmcCode;
                objects[1] = Category;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAmcSchemeList.Tables[0];
        }
        public DataSet GetAllCategoryList()
        {
            DataSet dsGetAllCategoryList = new DataSet();
            Database db;
            DbCommand CmdGetOverAllCategoryList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdGetOverAllCategoryList = db.GetStoredProcCommand("SP_GetProductAssetCategory");
                dsGetAllCategoryList = db.ExecuteDataSet(CmdGetOverAllCategoryList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAllCategoryList;
        }

        public DataSet GetLatestNav(int schemePlanCode)
        {
            DataSet dsGetAllCategoryList;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetLatestNavForScheme");
                db.AddInParameter(cmd, "@PASP_SchemePlanCode", DbType.Int32, schemePlanCode);
                dsGetAllCategoryList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAllCategoryList;
        }

        public void GetSchemeAMCCategory(int schemePlanCode, out int amcCode, out string category, out int isSipAvaliable)
        {
            Database db;
            DbCommand cmd;
            amcCode = 0;
            category = string.Empty;
            isSipAvaliable = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetSchemeAMCCategory");
                db.AddInParameter(cmd, "@SchemePlanCode", DbType.Int32, schemePlanCode);
                db.AddOutParameter(cmd, "@AMCCode", DbType.Int64, 1000000);
                db.AddOutParameter(cmd, "@CategoryCode", DbType.String, 100000);
                db.AddOutParameter(cmd, "@isSipAvaliable", DbType.Int32, 100000);

                db.ExecuteNonQuery(cmd);
                Object objAMCCode = db.GetParameterValue(cmd, "@AMCCode");
                if (objAMCCode != DBNull.Value)
                    amcCode = Convert.ToInt32(objAMCCode);

                Object objCategory = db.GetParameterValue(cmd, "@CategoryCode");
                if (objCategory != DBNull.Value)
                    category = Convert.ToString(objCategory);
                Object objisSipAvaliable = db.GetParameterValue(cmd, "@isSipAvaliable");
                if (objisSipAvaliable != DBNull.Value)
                    isSipAvaliable = Convert.ToInt32(objisSipAvaliable);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetSchemeAMCCategory(int schemePlanCode, out int amcCode, out string category)");
                object[] objects = new object[1];
                objects[0] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataTable GetSourceCodeList(string SourceCode)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetExternalSource");
                if (string.IsNullOrEmpty(SourceCode) == false) db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, SourceCode);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetSourceCodeList(string SourceCode)");
                object[] objParams = new object[1];
                objParams[0] = SourceCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// Gets the list of AMC with RTAs
        /// </summary>
        /// <returns></returns>
        public DataTable GetAmcWithRta()
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetAmcWithRta");
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetAmcWithRta()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        public DataTable GetWERPLookupMasterValueList(int codeMasterId, int lookupParentId)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetWERPLookupMasterValueList");
                db.AddInParameter(cmd, "@CodeMasterId", DbType.Int16, codeMasterId);
                if (lookupParentId != 0) db.AddInParameter(cmd, "@lookupParentId", DbType.Int16, lookupParentId);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetWERPLookupMasterValueList(int codeMasterId,int lookupParentId)");
                object[] objParams = new object[2];
                objParams[0] = codeMasterId;
                objParams[1] = lookupParentId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds.Tables[0];
        }

        public bool CheckForBusinessDate(DateTime date)
        {
            bool isBusinessDate = false;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_CheckForBusinessDate");
                db.AddInParameter(cmd, "@Date", DbType.Date, date);
                db.AddOutParameter(cmd, "@IsBusinessDate", DbType.Int16, 1000);
                db.ExecuteNonQuery(cmd);

                Object objIsValidBunisessDate = db.GetParameterValue(cmd, "@IsBusinessDate");
                if (objIsValidBunisessDate != DBNull.Value)
                    isBusinessDate = Convert.ToInt32(objIsValidBunisessDate) == 1 ? true : false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:CheckForBusinessDate(DateTime date)");
                object[] objParams = new object[1];
                objParams[0] = date;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return isBusinessDate;

        }
        public DataSet GetDepartment(int adviserId)
        {
            Database db;
            DataSet dsGetUserRole;
            DbCommand GetUserRolecmd;
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetUserRolecmd = db.GetStoredProcCommand("SPROC_GetAdviserDepartment");
                db.AddInParameter(GetUserRolecmd, "@adviserId", DbType.Int64, adviserId);
                dsGetUserRole = db.ExecuteDataSet(GetUserRolecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetFrequency()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetUserRole;
        }

        public DataTable GetOnlineMFNFOSchemeList()
        {
            Database db;
            DataTable dtNFOSchemeList;
            DbCommand getNFOSchemeListcmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNFOSchemeListcmd = db.GetStoredProcCommand("SPROC_ONL_GetMF_NFO_Schemes");
                dtNFOSchemeList = db.ExecuteDataSet(getNFOSchemeListcmd).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetOnlineMFNFOSchemeList()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNFOSchemeList;
        }

        public DataTable GetMFSchemeAMCCategory(int schemeId)
        {
            Database db;
            DataTable dtSchemeAMCCategory;
            DbCommand getSchemeAMCCategorycmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeAMCCategorycmd = db.GetStoredProcCommand("SPROC_ONL_GetMFSchemeAMCCategory");
                db.AddInParameter(getSchemeAMCCategorycmd, "@SchemePlanCode", DbType.Int64, schemeId);
                dtSchemeAMCCategory = db.ExecuteDataSet(getSchemeAMCCategorycmd).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetMFSchemeAMCCategory(int schemeId)");
                object[] objParams = new object[1];
                objParams[0] = schemeId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeAMCCategory;
        }
        public void CreateDepositoryType(string code,string description)
        {
            Database db;
            DbCommand CreateDepositoryTypecmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateDepositoryTypecmd = db.GetStoredProcCommand("SPROC_CreateDepositoryType");
                db.AddInParameter(CreateDepositoryTypecmd, "@WCMV_Code", DbType.String, code);
                db.AddInParameter(CreateDepositoryTypecmd, "@WCMV_Name", DbType.String, description);
                db.ExecuteNonQuery(CreateDepositoryTypecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:CreateDepositoryType()");
                object[] objParams = new object[2];
                objParams[0] = code;
                objParams[1] = description;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            
        }

        public Boolean CheckDuplicateDepositoryType(string code)
        {
            Database db;
            DbCommand GetDepositoryTypecmd;
            Boolean result=true;
            int count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetDepositoryTypecmd = db.GetStoredProcCommand("SPROC_CheckDuplicateDepositoryType");
                db.AddInParameter(GetDepositoryTypecmd, "@WCMV_Code", DbType.String, code);
                db.AddOutParameter(GetDepositoryTypecmd, "@Count", DbType.Int64, 10);
                db.ExecuteNonQuery(GetDepositoryTypecmd);
                Object objAMCCode = db.GetParameterValue(GetDepositoryTypecmd, "@Count");
                if (objAMCCode != DBNull.Value)
                {
                    count = Convert.ToInt32(objAMCCode);
                    if (count > 0)
                        result = false;
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
                FunctionInfo.Add("Method", "CommonLookupDao.cs:CheckDuplicateDepositoryType(string code)");
                object[] objParams = new object[1];
                objParams[0] = code;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;

        }
        
        public DataTable GetMFSchemeDividentType(int schemeId)
        {
            Database db;
            DataTable dtSchemeDividentType;
            DbCommand getSchemeDividentTypecmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeDividentTypecmd = db.GetStoredProcCommand("SPROC_ONL_GetMFSchemeDividentType");
                db.AddInParameter(getSchemeDividentTypecmd, "@SchemePlanCode", DbType.Int64, schemeId);
                dtSchemeDividentType = db.ExecuteDataSet(getSchemeDividentTypecmd).Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetMFSchemeDividentType(int schemeId)");
                object[] objParams = new object[1];
                objParams[0] = schemeId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeDividentType;
        }
        public void GetSchemeAMCSchemeCategory(int schemePlanCode, out int amcCode, out string category, out string categoryName, out string amcname, out string SchemeplanName, out int IsSIPAvaliable, out int IspurchaseAvaliable, out int IsRedeemAvaliable)
        {
            Database db;
            DbCommand cmd;
            amcCode = 0;
            category = string.Empty;
            categoryName = string.Empty;
            amcname = string.Empty;
            SchemeplanName = string.Empty;
            IsSIPAvaliable = 0;
            IspurchaseAvaliable = 0;
            IsRedeemAvaliable = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetProductWiseCtegoryAMC");
                db.AddInParameter(cmd, "@SchemePlanCode", DbType.Int32, schemePlanCode);
                db.AddOutParameter(cmd, "@AMCCode", DbType.Int64, 1000000);
                db.AddOutParameter(cmd, "@CategoryCode", DbType.String, 100000);
                  db.AddOutParameter(cmd, "@categoryName", DbType.String, 1000000);
                db.AddOutParameter(cmd, "@AMCName", DbType.String, 100000);
                  db.AddOutParameter(cmd, "@SchemeName", DbType.String, 1000000);
                  db.AddOutParameter(cmd, "@IsSIPAvaliable", DbType.Int32, 1000000);
                  db.AddOutParameter(cmd, "@IsPurchaseAvaliable", DbType.Int32, 1000000);
                  db.AddOutParameter(cmd, "@IsRedeemAvaliable", DbType.Int32, 1000000);
                db.ExecuteNonQuery(cmd);
                Object objAMCCode = db.GetParameterValue(cmd, "@AMCCode");
                if (objAMCCode != DBNull.Value)
                    amcCode = Convert.ToInt32(objAMCCode);
                Object objCategory = db.GetParameterValue(cmd, "@CategoryCode");
                if (objCategory != DBNull.Value)
                    category = Convert.ToString(objCategory);
                Object objcategoryName = db.GetParameterValue(cmd, "@categoryName");
                if (objcategoryName != DBNull.Value)
                    categoryName = Convert.ToString(objcategoryName);
                Object objamcname = db.GetParameterValue(cmd, "@AMCName");
                if (objamcname != DBNull.Value)
                    amcname = Convert.ToString(objamcname);
                Object objSchemeplanName = db.GetParameterValue(cmd, "@SchemeName");
                if (objSchemeplanName != DBNull.Value)
                    SchemeplanName = Convert.ToString(objSchemeplanName);
                Object objIsSIPAvaliable = db.GetParameterValue(cmd, "@IsSIPAvaliable");
                if (objIsSIPAvaliable != DBNull.Value)
                    IsSIPAvaliable = Convert.ToInt32(objIsSIPAvaliable);
                Object objIsPurchaseAvaliable = db.GetParameterValue(cmd, "@IsPurchaseAvaliable");
                if (objIsPurchaseAvaliable != DBNull.Value)
                    IspurchaseAvaliable = Convert.ToInt32(objIsPurchaseAvaliable);
                Object objIsRedeemAvaliable = db.GetParameterValue(cmd, "@IsRedeemAvaliable");
                if (objIsRedeemAvaliable != DBNull.Value)
                    IsRedeemAvaliable = Convert.ToInt32(objIsRedeemAvaliable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupDao.cs:GetSchemeAMCCategory(int schemePlanCode, out int amcCode, out string category)");
                object[] objects = new object[1];
                objects[0] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
    }
}
