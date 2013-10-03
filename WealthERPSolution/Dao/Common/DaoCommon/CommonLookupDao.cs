using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

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
        /// DataTable containing AMC list</returns> 
        public DataTable GetProductAmc(int AmcCode)
        {
            Database db;
            DbCommand cmdGetProdAmc;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProdAmc = db.GetStoredProcCommand("SP_GetProductAmc");
                if (AmcCode > 0) db.AddInParameter(cmdGetProdAmc, "@PA_AMCCode", DbType.Int32, AmcCode);
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
    }
}
