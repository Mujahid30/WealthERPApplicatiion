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
    public class PropertyBo
    {
        public int  CreatePropertyPortfolio(PropertyVo propertyVo, int userId)
        {
            PropertyDao propertyDao = new PropertyDao();
            int PropertyId = 0;
            try
            {
                PropertyId = propertyDao.CreatePropertyPortfolio(propertyVo, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PropertyBo.cs:CreatePropertyPortfolio()");


                object[] objects = new object[2];
                objects[0] = propertyVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return PropertyId;
        }

        public List<PropertyVo> GetPropertyPortfolio(int portfolioId, int CurrentPage, string SortOrder, out int Count)
        {
            PropertyDao propertyDao = new PropertyDao();

            List<PropertyVo> propertyList = new List<PropertyVo>();
            try
            {
                propertyList = propertyDao.GetPropertyPortfolio(portfolioId,CurrentPage,SortOrder,out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PropertyBo.cs:GetPropertyPortfolio()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return propertyList;
        }

        public PropertyVo GetPropertyAsset(int propertyId)
        {
            PropertyDao propertyDao = new PropertyDao();

            PropertyVo propertyVo = new PropertyVo();
            try
            {
                propertyVo = propertyDao.GetPropertyAsset(propertyId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PropertyBo.cs:GetPropertyAsset()");


                object[] objects = new object[1];
                objects[0] = propertyId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return propertyVo;
        }

        public bool UpdatePropertyPortfolio(PropertyVo propertyVo, int userId)
        {
            PropertyDao propertyDao = new PropertyDao();
            bool bResult = false;
            try
            {
                bResult = propertyDao.UpdatePropertyPortfolio(propertyVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "PropertyBo.cs:UpdatePropertyPortfolio()");


                object[] objects = new object[2];
                objects[0] = propertyVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeletePropertyPortfolio(int propertyId, int accountId)
        {
            bool bResult = false;
            PropertyDao propertyDao = new PropertyDao();

            try
            {
                bResult = propertyDao.DeletePropertyPortfolio(propertyId, accountId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:DeletePropertyPortfolio()");
                object[] objects = new object[2];
                objects[0] = propertyId;
                objects[1] = accountId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
        public DataTable GetLoanAgainstProperty(int customerId)
        {
            DataTable dt = null;
            PropertyDao propertyDao = new PropertyDao();

            try
            {
                dt = propertyDao.GetLoanAgainstProperty(customerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "InsuranceBo.cs:DeletePropertyPortfolio()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dt;
        }

        public Dictionary<int, string> GetPropertyDropDown(string customerId, List<int> customerIds)
        {
            PropertyDao propertyDao = new PropertyDao();
            Dictionary<int, string> propertyList = new Dictionary<int, string>();
            try
            {
                propertyList = propertyDao.GetPropertyDropDown(customerId, customerIds);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "PropertyBo.cs:GetPropertyDropDown()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return propertyList;
        }

        
    }
}
