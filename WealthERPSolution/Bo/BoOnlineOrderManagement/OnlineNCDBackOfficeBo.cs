using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using DaoOnlineOrderManagement;

namespace BoOnlineOrderManagement
{
    public class OnlineNCDBackOfficeBo
    {
        OnlineNCDBackOfficeDao onlineNCDBackOfficeDao;
        public DataSet GetSeriesCategories(string issuerId, int issueId, int seriesId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSeriesCategories(issuerId, issueId, seriesId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeriesCategories()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = issuerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetSeries(string issuerId, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSeries(issuerId, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeries()");
                object[] objects = new object[2];
                objects[1] = issueId;
                objects[2] = issuerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetIssuer()
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetIssuer();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public int CreateSeries(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.CreateSeries(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public int UpdateSeries(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.UpdateSeries(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public int CreateIssue(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.CreateIssue(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool CreateSeriesCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.CreateSeriesCategory(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool UpdateSeriesCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.UpdateSeriesCategory(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

         public DataSet GetAllInvestorTypes(string issuerId, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetAllInvestorTypes(issuerId, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeries()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetSubCategory(string issuerId, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSubCategory(issuerId, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeries()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetCategory(string issuerId, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetCategory(issuerId, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeries()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetSeriesInvestorTypeRule(int seriesId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSeriesInvestorTypeRule(seriesId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeriesInvestorTypeRule()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public int CreateCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.CreateCategory(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public bool CreateSubTypePerCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.CreateSubTypePerCategory(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public DataSet GetEligibleInvestorsCategory(string issuerId, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetEligibleInvestorsCategory(issuerId, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeries()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetSubTypePerCategoryDetails(int investorCatgeoryId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSubTypePerCategoryDetails(investorCatgeoryId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeries()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }
        public DataSet GetIssuerIssue(string  issuerID)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetIssuerIssue(issuerID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeries()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
          public DataSet GetCategoryDetails(int CatgeoryId)
        {
         
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetCategoryDetails(CatgeoryId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetCategoryDetails()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public int UpdateCategory(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.UpdateCategory(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public bool UpdateCategoryDetails(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int userID)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.UpdateCategoryDetails(onlineNCDBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

    }
}
