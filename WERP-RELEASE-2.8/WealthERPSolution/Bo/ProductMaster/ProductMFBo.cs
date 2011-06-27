using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoProductMaster;
using DaoProductMaster;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoProductMaster
{
    public class ProductMFBo
    {
        public List<ProductMFSchemePlanVo> GetSchemePlans()
        {
            ProductMFDao productMFDao = new ProductMFDao();

            List<ProductMFSchemePlanVo> mfSchemePlanList = new List<ProductMFSchemePlanVo>();
            try
            {
                mfSchemePlanList = productMFDao.GetSchemePlans();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetSchemePlans()");


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
            ProductMFDao productMFDao = new ProductMFDao();

            List<ProductMFSchemePlanVo> amcSchemePlanList = new List<ProductMFSchemePlanVo>();
            try
            {
                amcSchemePlanList = productMFDao.GetAMCSchemePlans(amcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetAMCSchemePlans()");


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
            ProductMFDao productMFDao = new ProductMFDao();

            List<ProductMFSchemePlanVo> codeSchemePlanList = new List<ProductMFSchemePlanVo>();
            try
            {
                codeSchemePlanList = productMFDao.GetAMCSchemePlans(schemeCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetCodeSchemePlans()");


                object[] objects = new object[0];
                objects[0] = schemeCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return codeSchemePlanList;
        }

        public DataSet GetProductAmc()
        {
            ProductMFDao productMFDao = new ProductMFDao();
            DataSet dsProductAmc;
            try
            {
                dsProductAmc = productMFDao.GetProductAmc();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetProductAmc()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsProductAmc;
        }
        public DataSet GetBrokerCodeForLOB()
        {
            ProductMFDao productMFDao = new ProductMFDao();
            DataSet dsBrokerCode;
            try
            {
                dsBrokerCode = productMFDao.GetBrokerCodeForLOB();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetBrokerCodeForLOB()");


                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsBrokerCode;
        }

        public int GetScheme(string schemePlanName)
        {
            ProductMFDao productMFDao = new ProductMFDao();
            int schemePlanCode;
            try
            {
                schemePlanCode = productMFDao.GetScheme(schemePlanName);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetScheme()");


                object[] objects = new object[1];
                objects[0] = schemePlanName;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return schemePlanCode;
        }

        public DataTable GetSchemePlanPrefix(string prefixText)
        {
            DataTable dtSchemePlans;
            ProductMFDao productMFDao = new ProductMFDao();

            try
            {
                dtSchemePlans = productMFDao.GetSchemePlanPrefix(prefixText);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetSchemePlanPrefix()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtSchemePlans;
     
        }
        public DataTable GetSwitchSchemePlanPrefix(string prefixText,int SchemePlanCode)
        {
            DataTable dtSchemePlans;
            ProductMFDao productMFDao = new ProductMFDao();

            try
            {
                dtSchemePlans = productMFDao.GetSwitchSchemePlanPrefix(prefixText, SchemePlanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "ProductMFBo.cs:GetSchemePlanPrefix()");


                object[] objects = new object[0];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtSchemePlans;
        }
    }
}
