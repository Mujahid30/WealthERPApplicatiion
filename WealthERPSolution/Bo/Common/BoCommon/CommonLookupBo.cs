using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;


namespace BoCommon
{
    public class CommonLookupBo
    {
        CommonLookupDao daoCommonLookup;

        public CommonLookupBo()
        {
            daoCommonLookup = new CommonLookupDao();
        }

        public DataTable GetMFInstrumentSubCategory(string categoryCode)
        {
            CommonLookupDao commonLookupDao = new CommonLookupDao();
            DataTable dtSubCategory;

            try
            {
                dtSubCategory = commonLookupDao.GetMFInstrumentSubCategory(categoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetMFInstrumentSubCategory(string categoryCode)");
                object[] objects = new object[1];
                objects[0] = categoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSubCategory;
        }

        /// <summary> 
        /// Gets the list of AMC</summary> 
        /// <returns> 
        /// DataTable containing AMC list</returns>
        public DataTable GetProdAmc()
        {
            DataTable dtAmc;
            try
            {
                dtAmc = daoCommonLookup.GetProductAmc(0);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetProdAmc()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAmc;
        }

        /// <summary> 
        /// Gets the list of AMC</summary> 
        /// <param name="AmcCode">AMCCode for the desired AMC. '0' to return list of all AMCs</param>
        /// <returns> 
        /// DataTable containing AMC list</returns>
        public DataTable GetProdAmc(int AmcCode)
        {
            DataTable dtAmc;
            try
            {
                dtAmc = daoCommonLookup.GetProductAmc(AmcCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetProdAmc(int AmcCode)");
                object[] objParams = new object[1];
                objParams[0] = AmcCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAmc;
        }

        /// <summary> 
        /// Gets the list of Products</summary> 
        /// <returns> 
        /// DataTable containing Product list</returns>
        public DataTable GetProductList()
        {
            DataTable dt;
            try
            {
                dt = daoCommonLookup.GetProductList(null);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetProductList()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        /// <summary> 
        /// Gets the list of AMC</summary> 
        /// <param name="AmcCode">AMCCode for the desired AMC. '0' to return list of all AMCs</param>
        /// <returns> 
        /// DataTable containing AMC list</returns>
        public DataTable GetProductList(string ProductCode)
        {
            DataTable dt;
            try
            {
                dt = daoCommonLookup.GetProductList(ProductCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetProdAmc(string ProductCode)");
                object[] objParams = new object[1];
                objParams[0] = ProductCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        /// <summary> 
        /// Gets list of Product Categories</summary> 
        /// <param name="ProductCode">ProductCode for the desired AMC. null to return list of all Products</param>
        /// <param name="CategoryCode">CategoryCode for the desired Category. null to return list for all Categories</param>
        /// <returns> 
        /// DataTable containing AMC list</returns>
        public DataTable GetCategoryList(string ProductCode, string CategoryCode)
        {
            DataTable dt;
            try
            {
                ProductCode = string.IsNullOrEmpty(ProductCode) ? null : ProductCode.Trim();
                CategoryCode = string.IsNullOrEmpty(CategoryCode) ? null : CategoryCode.Trim();
                dt = daoCommonLookup.GetProductCategories(ProductCode, CategoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetProdAmc(string ProductCode)");
                object[] objParams = new object[2];
                objParams[0] = ProductCode;
                objParams[1] = CategoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
    }
}
