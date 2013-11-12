using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoCommon;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using VoOnlineOrderManagemnet;


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

        public List<OnlineMFOrderVo> GetAllSIPDataForOrderEdit(int orderIDForEdit, int customerIdForEdit)
        {
            List<OnlineMFOrderVo> SIPDataForOrderEditList = new List<OnlineMFOrderVo>();
            SIPDataForOrderEditList = daoCommonLookup.GetAllSIPDataForOrderEdit(orderIDForEdit, customerIdForEdit);

            return SIPDataForOrderEditList;
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
                dtAmc = daoCommonLookup.GetProductAmc(0, false);
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
                dtAmc = daoCommonLookup.GetProductAmc(AmcCode, false);

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
        /// Gets the list of AMC</summary> 
        /// <param name="AmcCode">AMCCode for the desired AMC. '0' to return list of all AMCs</param>
        /// <param name="hasOnlineShcemes">true if AMC with only online schemes, otherwise false</param>
        /// <returns> 
        /// DataTable containing AMC list</returns>
        public DataTable GetProdAmc(int AmcCode, bool hasOnlineShcemes)
        {
            DataTable dtAmc;
            try
            {
                dtAmc = daoCommonLookup.GetProductAmc(AmcCode, hasOnlineShcemes);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetProdAmc(int AmcCode, bool hasOnlineShcemes)");
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

        /// <summary> 
        /// Gets list of Product Categories</summary> 
        /// <param name="ProductCode">ProductCode for the desired AMC. null to return list of all Products</param>
        /// <param name="CategoryCode">CategoryCode for the desired Category. null to return list for all Categories</param>
        /// <param name="SubCategoryCode">SubCategoryCode for the desired SubCategory. null to return list for all SubCategories</param>
        /// <returns> 
        /// DataTable containing AMC list</returns>
        public DataTable GetProductSubCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)
        {
            DataTable dt;
            try
            {
                ProductCode = string.IsNullOrEmpty(ProductCode) ? null : ProductCode.Trim();
                CategoryCode = string.IsNullOrEmpty(CategoryCode) ? null : CategoryCode.Trim();
                SubCategoryCode = string.IsNullOrEmpty(SubCategoryCode) ? null : SubCategoryCode.Trim();
                dt = daoCommonLookup.GetProductSubCategories(ProductCode, CategoryCode, SubCategoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objParams = new object[3];
                objParams[0] = ProductCode;
                objParams[1] = CategoryCode;
                objParams[2] = SubCategoryCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public DataTable GetFrequencyDetails()
        {
            DataTable dtAllSIPDataForOrder = new DataTable();
            try
            {
                dtAllSIPDataForOrder = daoCommonLookup.GetFrequencyDetails();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objParams = new object[3];

                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAllSIPDataForOrder;
        }


        public DataTable GetAllSIPDataForOrder(int schemeCode)
        {
            DataTable dtAllSIPDataForOrder = new DataTable();
            try
            {
                dtAllSIPDataForOrder = daoCommonLookup.GetAllSIPDataForOrder(schemeCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objParams = new object[3];
                objParams[0] = schemeCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAllSIPDataForOrder;
        }

        public DataTable GetFolioNumberForSIP(int Amccode, int CustomerId)
        {
            DataTable dtSchemeList = new DataTable();
            try
            {
                dtSchemeList = daoCommonLookup.GetFolioNumberForSIP(Amccode, CustomerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objParams = new object[3];
                objParams[0] = Amccode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeList;
        }

        public DataTable GetAmcSipSchemeList(int Amccode, string Category)
        {
            DataTable dtSchemeList = new DataTable();
            try
            {
                string CategoryCode = string.IsNullOrEmpty(Category) ? null : Category.Trim();
                dtSchemeList = daoCommonLookup.GetAmcSipSchemeList(Amccode, CategoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objParams = new object[3];
                objParams[0] = Amccode;
                objParams[1] = Category;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeList;
        }

        public DataTable GetAmcSchemeList(int Amccode, string Category, int Customerid)
        {
            DataTable dtSchemeList = new DataTable();
            try
            {
                string CategoryCode = string.IsNullOrEmpty(Category) ? null : Category.Trim();
                dtSchemeList = daoCommonLookup.GetAmcSchemeList(Amccode, CategoryCode, Customerid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objParams = new object[3];
                objParams[0] = Amccode;
                objParams[1] = Category;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSchemeList;
        }
        public DataSet GetAllCategoryList()
        {
            DataSet dsGetAllCategoryList = new DataSet();

            try
            {
                dsGetAllCategoryList = daoCommonLookup.GetAllCategoryList();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsGetAllCategoryList;
        }

        public DataSet GetLatestNav(int schemePlanCode)
        {
            DataSet dtLatNav = new DataSet();
            try
            {
                dtLatNav = daoCommonLookup.GetLatestNav(schemePlanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetCategoryList(string ProductCode, string CategoryCode, string SubCategoryCode)");
                object[] objParams = new object[1];
                objParams[0] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtLatNav;
        }

        //Returns 0 for correct & 1 if cut-off error, -1 in other error
        //If no max val send amt as max
        public int IsRuleCorrect(float amt, float min, float max, float multiple, DateTime cutOff)
        {
            if (amt < min || amt > max) return -2;
            if (amt % multiple != 0) return -1;
            if (DateTime.Now.TimeOfDay > cutOff.TimeOfDay && cutOff.TimeOfDay < System.TimeSpan.Parse("23:59:59")) return 1;
            return 0;
        }


        public void GetSchemeAMCCategory(int schemePlanCode, out int amcCode, out string category)
        {
            try
            {
                daoCommonLookup.GetSchemeAMCCategory(schemePlanCode, out amcCode, out category);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceCode"></param>
        /// <returns></returns>
        public DataTable GetExternalSource(string SourceCode)
        {
            DataTable dt;
            try
            {
                dt = daoCommonLookup.GetSourceCodeList(SourceCode);
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
        /// Gets the list of AMC with RTAs
        /// </summary>
        /// <returns></returns>
        public DataTable GetAmcWithRta() {
            DataTable dt;

            try {
                dt = daoCommonLookup.GetAmcWithRta();
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

        public DataTable GetWERPLookupMasterValueList(int codeMasterId, int lookupParentId)
        {
            DataTable dt;
            try
            {
                dt = daoCommonLookup.GetWERPLookupMasterValueList(codeMasterId, lookupParentId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetWERPLookupMasterValueList(int codeMasterId,int lookupParentId)");
                object[] objParams = new object[2];
                objParams[0] = codeMasterId;
                objParams[1] = lookupParentId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objParams);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }



    }
}
