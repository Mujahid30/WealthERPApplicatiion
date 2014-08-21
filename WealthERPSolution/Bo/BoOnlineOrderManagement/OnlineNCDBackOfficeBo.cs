using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.IO.Compression;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer;


namespace BoOnlineOrderManagement
{
    public class OnlineNCDBackOfficeBo
    {
        OnlineNCDBackOfficeDao onlineNCDBackOfficeDao;

        public DataSet GetIssueDetails(int issueId, int adviserId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetIssueDetails(issueId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetIssueDetails()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetExtSource(string product, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetExtSource(product, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetIssueDetails()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetAdviserIssueList(DateTime date, int type, string product, int adviserId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetAdviserIssueList(date, type, product, adviserId);
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

        public int UpdateIssue(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.UpdateIssue(onlineNCDBackOfficeVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool UpdateOnlineEnablement(int issueId)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.UpdateOnlineEnablement(issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public int GetSeriesSequence(int issueId, int adviserId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSeriesSequence(issueId, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSeriesSequence()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public int ChekSeriesSequence(int seqNo, int issueId, int adviserId, int seriesId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.ChekSeriesSequence(seqNo, issueId, adviserId, seriesId);
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
        public int CreateUpdateDeleteIssuer(int issuerId, string issuerCode, string issuerName, string commandType)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.CreateUpdateDeleteIssuer(issuerId, issuerCode, issuerName, commandType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:CreateUpdateDeleteIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }



        public int GetValidateFrom(int fromRange, int adviserId, int issueId, int formRangeId, ref string status)
        {

            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetValidateFrom(fromRange, adviserId, issueId, formRangeId, ref  status);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:CreateUpdateDeleteIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        public int CreateUpdateDeleteAplicationNos(int fromRange, int toRange, int adviserId, int issueId, int formRangeId, string commandType, ref string status)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.CreateUpdateDeleteAplicationNos(fromRange, toRange, adviserId, issueId, formRangeId, commandType, ref  status);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:CreateUpdateDeleteIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public int CreateUpdateDeleteSyndicateMaster(int syndicateId, string syndicateCode, string syndicateName, string commandType)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.CreateUpdateDeleteSyndicateMaster(syndicateId, syndicateCode, syndicateName, commandType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:CreateUpdateDeleteIssuer()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataSet BindNcdCategory(string type, string catCode)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.BindNcdCategory(type, catCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:BindNcdCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet BindRta()
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.BindRta();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:BindRta()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetActiveRange(int adviserId, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetActiveRange(adviserId, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetActiveRange()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }


        public DataSet GetSeriesCategories(int issuerId, int issueId, int seriesId)
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

        public DataSet GetSeries(int issuerId, int issueId)
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

        //public DataSet GetIssuer()
        //{
        //    onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
        //    try
        //    {
        //        return onlineNCDBackOfficeDao.GetIssuer();
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();
        //        FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetIssuer()");
        //        object[] objects = new object[0];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //}

        public DataSet GetAplRanges()
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetAplRanges();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAplRanges()");
                object[] objects = new object[0];
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
        public DataTable GetFrequency()
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetFrequency();
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

        public int CreateIssue(VoOnlineOrderManagemnet.OnlineNCDBackOfficeVo onlineNCDBackOfficeVo, int adviserId)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.CreateIssue(onlineNCDBackOfficeVo, adviserId);
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

        public DataSet GetAllInvestorTypes(int issuerId, int issueId, int categoryId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetAllInvestorTypes(issuerId, issueId, categoryId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAllInvestorTypes()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetSubCategory(int issuerId, int issueId, int size)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSubCategory(issuerId, issueId, size);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSubCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetSubCategory1(int issuerId, int issueId, int size)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetSubCategory(issuerId, issueId, size);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSubCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetCategory(int issuerId, int issueId)
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
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetCategory()");
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

        public string GetEligibleIssueDelete(int catSubTypeId, int catId, int seriesId, int IssueId)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.GetEligibleIssueDelete(catSubTypeId, catId, seriesId, IssueId);
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

        public DataSet GetEligibleInvestorsCategory(int issuerId, int issueId)
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
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetSubTypePerCategoryDetails()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        public DataSet GetIssuerIssue(int advisorId, string product)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetIssuerIssue(advisorId, product);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetIssuerIssue()");
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

        public int IsValidBidRange(int issueId, double minBidAmt, double MaxBidAmt)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.IsValidBidRange(issueId, minBidAmt, MaxBidAmt);
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
        public void GetOrdersEligblity(int issueId, ref int isPurchaseAvailable)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                onlineNCDBackOfficeDao.GetOrdersEligblity(issueId, ref isPurchaseAvailable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public void AttchingSameSubtypeCattoSeries(int issueId)
        {
            try
            {
                onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                onlineNCDBackOfficeDao.AttchingSameSubtypeCattoSeries(issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }





        public void GenerateOnlineNcdExtract(int AdviserId, int UserId, string ExternalSource, string ProductAsset, int issueId, ref int isExtracted)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            onlineNCDBackOfficeDao.GenereateNcdExtract(AdviserId, UserId, ExternalSource, ProductAsset, issueId, ref isExtracted);
        }

        public DataTable GetAdviserNCDOrderBook(int adviserId, int issueNo, string status, DateTime dtFrom, DateTime dtTo)
        {
            DataTable dtNCDOrder;
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                dtNCDOrder = onlineNCDBackOfficeDao.GetAdviserNCDOrderBook(adviserId, issueNo, status, dtFrom, dtTo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAdviserNCDOrderBook()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrder;
        }
        public DataTable GetAdviserNCDOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataTable dtNCDOrderBook;
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                dtNCDOrderBook = onlineNCDBackOfficeDao.GetAdviserNCDOrderSubBook(adviserId, IssuerId, orderid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAdviserNCDOrderSubBook()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrderBook;
        }

        public DataTable GetFileTypeList(int FileTypeId, string ExternalSource, char FileSubType, string ProductCode)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            return onlineNCDBackOfficeDao.GetFileTypeList(FileTypeId, ExternalSource, FileSubType, ProductCode);
        }


        private KeyValuePair<string, string>[] GetHeaderMapping(int fileTypeId, string extSource)
        {
            if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            DataTable dtHeaderMap = onlineNCDBackOfficeDao.GetHeaderMapping(fileTypeId, extSource);
            int nRows = dtHeaderMap.Rows.Count;
            if (nRows <= 0) return null;

            List<OnlineIssueHeader> fileHeaderList = new List<OnlineIssueHeader>();
            for (int i = 0; i < nRows; i++)
            {
                OnlineIssueHeader header = new OnlineIssueHeader();
                header.HeaderSequence = int.Parse(dtHeaderMap.Rows[i]["WEEHM_HeaderSequence"].ToString());
                header.ColumnAlias = dtHeaderMap.Rows[i]["WEIH_ColumnAlias"].ToString();
                header.HeaderName = dtHeaderMap.Rows[i]["WEEHM_HeaderName"].ToString();
                fileHeaderList.Add(header);
            }

            KeyValuePair<string, string>[] kvpHeaders = new KeyValuePair<string, string>[nRows];

            List<OnlineIssueHeader> sortedBySeq = fileHeaderList.OrderBy(o => o.HeaderSequence).ToList();

            int j = 0;
            foreach (OnlineIssueHeader header in sortedBySeq)
            {
                kvpHeaders[j] = new KeyValuePair<string, string>(header.ColumnAlias, header.HeaderName);
                j++;
            }
            return kvpHeaders;
        }
        public DataTable GetOnlineAllotment(int fileTypeId, string extSource, DataTable dtUpload)
        {
            KeyValuePair<string, string>[] headers = GetHeaderMapping(fileTypeId, extSource);
            if (dtUpload.Rows.Count != 0)
            {

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        if (dtUpload.Columns.Contains(header.Key))
                        {
                            if (dtUpload.Columns[header.Key].ToString() == header.Key)
                            {
                                dtUpload.Columns[header.Key].ColumnName = header.Value;
                            }
                        }
                    }
                    dtUpload.AcceptChanges();
                }

                if (dtUpload.Columns.Contains("Status"))
                {
                    dtUpload.Columns.Remove(dtUpload.Columns["Status"]);

                }
                dtUpload.AcceptChanges();
            }
            return dtUpload;
        }


        public DataTable GetOnlineNcdExtractPreview(DateTime extractDate, int adviserId, int fileTypeId, string extSource, int issueId)
        {

            KeyValuePair<string, string>[] headers = GetHeaderMapping(fileTypeId, extSource);

            int AID_SeriesCount = 0;
            if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            DataTable dtExtract = onlineNCDBackOfficeDao.GetOnlineNcdExtractPreview(extractDate, adviserId, fileTypeId, issueId, extSource, out AID_SeriesCount).Tables[0];

            if (fileTypeId == 5)
            {

                int desiredSize = (AID_SeriesCount * 2) + 15;

                while ((dtExtract.Columns.Count - 19) >= desiredSize)
                {
                    dtExtract.Columns.RemoveAt(desiredSize);
                }
                dtExtract.AcceptChanges();
            }
            if (fileTypeId == 11)
            {

                int seriesDelete = AID_SeriesCount + 1;
                int buyDelete = AID_SeriesCount + 1;
                List<string> columnsToDelete = new List<string>();
                foreach (DataColumn column in dtExtract.Columns)
                {

                    if (column.ColumnName.Contains("SeriesQuantity" + seriesDelete.ToString("D2")))
                    {
                        columnsToDelete.Add("AIOE_BSE_BOND_SeriesQuantity" + seriesDelete.ToString("D2"));
                        columnsToDelete.Add("AIOE_BSE_BOND_SeriesAmountPayable" + seriesDelete.ToString("D2"));
                        seriesDelete = seriesDelete + 1;

                    }
                    if (column.ColumnName.Contains("BuyBackFacity" + buyDelete.ToString("D2")))
                    {
                        columnsToDelete.Add("AIOE_BSE_BOND_SeriesSequence" + buyDelete.ToString("D2"));
                        columnsToDelete.Add("AIOE_BSE_BOND_SeriesBuyBackFacity" + buyDelete.ToString("D2"));
                        buyDelete = buyDelete + 1;
                    }

                }
                foreach (string col in columnsToDelete)
                {
                    dtExtract.Columns.Remove(col);
                    dtExtract.AcceptChanges();

                }



            }
            if (dtExtract != null)
            {
                if (dtExtract.Rows.Count != 0)
                {

                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            if (dtExtract.Columns.Contains(header.Key))
                            {
                                if (dtExtract.Columns[header.Key].ToString() == header.Key)
                                {
                                    dtExtract.Columns[header.Key].ColumnName = header.Value;
                                }
                            }
                        }
                        dtExtract.AcceptChanges();
                    }

                    if (extSource == "BSE" && fileTypeId == 1)
                    {
                        DataTable dtExtractClone = dtExtract.Copy();
                        DataRow newBlankRow;
                        newBlankRow = dtExtract.NewRow();
                        newBlankRow = dtExtractClone.Rows[0];

                        dtExtract.Rows.Add(newBlankRow.ItemArray);

                        int maxIndex = dtExtract.Rows.Count;

                        DataRow selectedRow = dtExtract.Rows[maxIndex - 1];
                        DataRow newRow = dtExtract.NewRow();
                        newRow.ItemArray = selectedRow.ItemArray; // copy data
                        dtExtract.Rows.Remove(selectedRow);
                        dtExtract.Rows.InsertAt(newRow, 0);


                    }
                }
            }

            dtExtract.AcceptChanges();
            return dtExtract;
        }




        public string GetExtractStepCode(int fileTypeId)
        {
            try
            {
                if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                return onlineNCDBackOfficeDao.GetExtractStepCode(fileTypeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        public void IsIssueAlloted(int issueId, ref string result)
        {
            try
            {
                if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                onlineNCDBackOfficeDao.IsIssueAlloted(issueId, ref result);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

        public void IsSameSubTypeCatAttchedtoSeries(string cat, int issueId, ref string result)
        {
            try
            {
                if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
                onlineNCDBackOfficeDao.IsSameSubTypeCatAttchedtoSeries(cat, issueId, ref result);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }


        public void GetFileName(string extSource, int fileTypeId, ref string filename, ref string delimeter, ref string format, string issueName)
        {

            string dt = DateTime.Now.ToString("ddMMyy");
            string strIssue = string.Empty;
            if (issueName.Length > 15)
                strIssue = issueName.Substring(0, 4);
            else
                strIssue = issueName;


            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            string extractStepCode = GetExtractStepCode(fileTypeId);
            if (extractStepCode == "EI")
            {
                filename = extSource + "OrderBookExtr_" + strIssue + "_" + dt;
                delimeter = "";
                format = ".xls";

            }
            if (extractStepCode == "EC")
            {
                filename = extSource + "ChqExtr_" + strIssue + "_" + dt;
                delimeter = ",";
                format = ".csv";
            }
            else if (extractStepCode == "EB")
            {
                filename = extSource + "BidExtr_" + strIssue + "_" + dt;
                delimeter = "|";
                format = ".txt";
            }
            else if (extractStepCode == "EP")
            {
                filename = extSource + "CheqPrintFile_" + strIssue + "_" + dt;
                delimeter = "";
                format = ".xls";


            }
            else if (extractStepCode == "EAP")
            {

                GetAcntingExtractFileName(ref filename, ref delimeter, ref format, strIssue, fileTypeId);
            }
            else if (extractStepCode == "EAJV")
            {

                GetAcntingExtractFileName(ref filename, ref delimeter, ref format, strIssue, fileTypeId);
            }
            else if (extractStepCode == "EODJV")
            {
                GetAcntingExtractFileName(ref filename, ref delimeter, ref format, strIssue, fileTypeId);
            }

        }

        //GetAcntingExtractFileName

        public void GetAcntingExtractFileName(ref string filename, ref string delimeter, ref string format, string strIssue, int fileTypeId)
        {
            string strExtractDate = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            string[] strSplitExtractDate = strExtractDate.Split('/');

            string DD = strSplitExtractDate[0].ToString();
            string MM = strSplitExtractDate[1].ToString();
            string YYYY = strSplitExtractDate[2].ToString();

            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            if (fileTypeId == 32)
            {
                filename = "NCD PAYFILE FOR" + ' ' + strIssue + ' ' + "BOND" + "_" + DD + '.' + MM;
                delimeter = "\t";
                format = ".txt";
            }
            else if (fileTypeId == 33)
            {
                filename = "eNCD" + DD + MM + YYYY + '-' + strIssue;
                delimeter = "|";
                format = ".txt";
            }
            if (fileTypeId == 34)
            {
                filename = "IPO PAYFILE FOR" + ' ' + strIssue + ' ' + "BOND" + "_" + DD + '.' + MM;
                delimeter = "\t";
                format = ".txt";
            }
            else if (fileTypeId == 35)
            {
                filename = "eIPO" + DD + MM + YYYY + '-' + strIssue;
                delimeter = "|";
                format = ".txt";
            }
            else if (fileTypeId == 36)
            {
                filename = "eNCD" + DD + MM + YYYY + '-' + strIssue + '-' + "EOD";
                delimeter = "|";
                format = ".txt";
            }
            else if (fileTypeId == 37)
            {
                filename = "eIPO" + DD + MM + YYYY + '-' + strIssue + '-' + "EOD";
                delimeter = "|";
                format = ".txt";
            }

        }



        public DataTable ReadCsvFile(string FilePath, int fileTypeId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            string extractStepCode = GetExtractStepCode(fileTypeId);
            char ch = ' ';
            if (extractStepCode == "UC")
            {
                ch = ',';
            }
            else if (extractStepCode == "UB")
            {

                ch = '|';
            }
            else if (extractStepCode == "UA")
            {
                ch = ',';
            }
            string[] allLines = File.ReadAllLines(FilePath);

            string[] headers = allLines[0].Split(ch);

            DataTable dtUploadFile = new DataTable("Upload");
            string columnName = "";


            if (fileTypeId == 17 || fileTypeId == 18 || fileTypeId == 19 || fileTypeId == 25)
            {
                int i = 1;
                foreach (string header in headers)
                {
                    columnName = "column" + i;
                    if (dtUploadFile.Columns.Contains(columnName))
                    {
                        dtUploadFile.Columns.Add(columnName + dtUploadFile.Columns.Count);
                    }
                    else
                    {
                        dtUploadFile.Columns.Add(columnName);
                    }

                    i++;
                }
                for (i = 0; i < allLines.Length; i++)
                {
                    string[] row = allLines[i].Split(ch);
                    dtUploadFile.Rows.Add(row);
                }
            }
            else
            {
                foreach (string header in headers)
                {
                    if (dtUploadFile.Columns.Contains(header))
                    {
                        dtUploadFile.Columns.Add(header + dtUploadFile.Columns.Count);
                    }
                    else
                    {
                        dtUploadFile.Columns.Add(header);
                    }
                }

                for (int i = 1; i < allLines.Length; i++)
                {
                    string[] row = allLines[i].Split(ch);
                    dtUploadFile.Rows.Add(row);
                }

            }

            return dtUploadFile;
        }

        public void UpdateNcdOrderMannualMatch(int orderId, int allotmentId, ref int isAllotmented, ref int isUpdated)
        {
            //bool result = false;
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                onlineNCDBackOfficeDao.UpdateNcdOrderMannualMatch(orderId, allotmentId, ref isAllotmented, ref isUpdated);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:OrderMannualMatch()");
                object[] objects = new object[2];
                objects[0] = orderId;
                objects[1] = allotmentId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //return result;
        }
        public void UpdateNcdAutoMatch(int orderId, int applictionNo, string dpId, ref int isAllotmented, ref int isUpdated)
        {
            // bool result = false;
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                onlineNCDBackOfficeDao.UpdateNcdAutoMatch(orderId, applictionNo, dpId, ref isAllotmented, ref isUpdated);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:OrderMannualMatch()");
                object[] objects = new object[1];
                objects[0] = orderId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            //   return result;

        }

        public DataSet GetUnmatchedAllotments(int adviserId, int issueId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetUnmatchedAllotments(adviserId, issueId);
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



        public DataSet GetNcdIpoAcntingExtract(DateTime Today, int adviserId, int issueId, string Product, string extractType)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetNcdIpoAcntingExtract(Today, adviserId, issueId, Product, extractType);
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








        public DataSet GetUploadIssue(string product, int adviserId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetUploadIssue(product, adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetUploadIssue()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataSet GetAdviserOrders(int IssueId, string Product, string Status, DateTime FromDate, DateTime ToDate, int adviserid)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetAdviserOrders(IssueId, Product, Status, FromDate, ToDate, adviserid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAdviserOrders()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet GetAdviserissueallotmentList(int adviserId, int issureid, string type, DateTime fromdate, DateTime todate)
        {
            OnlineNCDBackOfficeDao OnlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            DataSet dsGetAdviserissueallotmentList;
            dsGetAdviserissueallotmentList = OnlineNCDBackOfficeDao.GetAdviserissueallotmentList(adviserId, issureid, type, fromdate, todate);
            return dsGetAdviserissueallotmentList;

        }
        public DataTable GetIssuerid(int adviserid)
        {
            DataTable dtGetIssuerid;
            OnlineNCDBackOfficeDao OnlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                dtGetIssuerid = OnlineNCDBackOfficeDao.GetIssuerid(adviserid);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:OnlinebindRandT()");
                object[] objects = new object[1];
                objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetIssuerid;
        }

        public List<OnlineIssueHeader> GetHeaderDetails(int fileTypeId, string extSource)
        {
            if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            DataTable dtHeaderMap = onlineNCDBackOfficeDao.GetHeaderMapping(fileTypeId, extSource);
            if (dtHeaderMap.Rows.Count <= 0) return null;

            List<OnlineIssueHeader> fileHeaderList = new List<OnlineIssueHeader>();
            foreach (DataRow row in dtHeaderMap.Rows)
            {
                OnlineIssueHeader header = new OnlineIssueHeader();
                header.HeaderSequence = int.Parse(row["WEEHM_HeaderSequence"].ToString());
                header.ColumnAlias = row["WEIH_ColumnAlias"].ToString();
                header.HeaderName = row["WEEHM_HeaderName"].ToString();
                header.RegularExpression = row["WEEHM_RegularExpression"].ToString();
                header.ColumnName = row["WEIH_ColumnName"].ToString();
                header.IsUploadRelated = bool.Parse(row["WEIH_IsUploadRelated"].ToString());
                header.ColumnExists = false;
                fileHeaderList.Add(header);
            }
            return fileHeaderList.OrderBy(o => o.HeaderSequence).ToList();
        }

        private string HtmError(string csvError)
        {
            string[] strErrList = csvError.Split('|');
            StringBuilder sbError = new StringBuilder();

            sbError.AppendLine("<a href=\"#\" class=\"popper\" data-popbox=\"divCutOffCheck\">KBAJPAI</a>");
            sbError.AppendLine("<div id=\"divCutOffCheck\" class=\"popbox\">");
            sbError.AppendLine("<h2>Error Details:</h2>");
            //sbError.AppendLine("<ol>");       
            for (int i = 0; i < strErrList.Length; i++)
            {
                sbError.Append("<p>" + strErrList[i] + "</p>");
            }
            //sbError.AppendLine("</ol>");
            sbError.AppendLine("</div>");

            return sbError.ToString();
        }


        public string GetErrorsForRow(List<OnlineIssueHeader> Headers, DataRow row, int rowNum)
        {
            StringBuilder strErr = new StringBuilder();
            List<string> ErrorList = new List<string>();

            foreach (OnlineIssueHeader Header in Headers)
            {

                for (int j = 0; j < row.Table.Columns.Count - 1; j++)
                {
                    if (row.Table.Columns[j].ToString().Trim().ToLower() == Header.HeaderName.Trim().ToLower())
                    {
                        // int colInx = row.Table.Columns[Header.HeaderName].Ordinal;
                        //int colInx = Header.HeaderSequence;
                        Header.ColumnExists = true;
                        string colRegex = Header.RegularExpression;
                        string colVal = row[j].ToString();
                        string colNam = Header.HeaderName;
                        if (colRegex != "NULL")
                        {
                            Regex regex = new Regex(colRegex);
                            if (!regex.IsMatch(colVal))
                            {
                                ErrorList.Add("Error at: " + colNam + "(" + rowNum + ", " + (j + 3) + ")");

                            }
                        }
                    }
                }
            }

            int i = 0;
            foreach (string err in ErrorList)
            {
                strErr.Append(err);
                if (i < ErrorList.Count - 1) strErr.Append("|");
                i++;
            }

            string htmError = HtmError(strErr.ToString());

            //return htmError;
            return strErr.ToString();
        }

        private static bool FindHeaders(OnlineIssueHeader Header)
        {
            if (string.IsNullOrEmpty(Header.RegularExpression) == true) return false;
            return true;
        }

        public DataTable ValidateUploadData(DataTable dtRawData, int fileTypeId, string extSource, ref StringBuilder ColumnNameErrors)
        {
            DataColumn serialNo = new DataColumn("SN", System.Type.GetType("System.Int32"));
            DataColumn errorCol = new DataColumn("Remarks", System.Type.GetType("System.String"), "");
            if (fileTypeId == 27 || fileTypeId == 28 || fileTypeId == 29 || fileTypeId == 30)
            {
                DataColumn Status = new DataColumn("Status", System.Type.GetType("System.String"), "");
                DataColumn processId = new DataColumn("ProcessId", System.Type.GetType("System.Int32"));
                dtRawData.Columns.Add(Status);
                dtRawData.Columns.Add(processId);
            }
            dtRawData.Columns.Add(serialNo);
            dtRawData.Columns.Add(errorCol);
            dtRawData.AcceptChanges();
            dtRawData.Columns.ToString().Trim();

            List<OnlineIssueHeader> AllHeaders = GetHeaderDetails(fileTypeId, extSource);

            if (AllHeaders == null) return null;
            List<OnlineIssueHeader> Headers = AllHeaders.FindAll(FindHeaders);

            int i = 1;
            foreach (DataRow row in dtRawData.Rows)
            {
                row["SN"] = i.ToString();
                row["Remarks"] = GetErrorsForRow(Headers, row, i);

                i++;
            }


            dtRawData.Columns["SN"].SetOrdinal(0);
            dtRawData.Columns["Remarks"].SetOrdinal(1);
            dtRawData.AcceptChanges();

            ColumnNameErrors.Append("Column Name MisMatch");
            ColumnNameErrors.AppendLine();

            foreach (OnlineIssueHeader header in Headers)
            {
                if (header.ColumnExists == false)
                {
                    ColumnNameErrors.Append("Actual Name:  " + header.HeaderName);
                    ColumnNameErrors.Append("\n");
                }
            }



            return dtRawData;
        }
        //    private DataTable AddColumnToDataTable(DataTable dtRaw, string  dataColumn)
        //{
        //    if (!dtRaw.Columns.Contains(dataColumn))
        //    {
        //        DataColumn newColumn = new DataColumn(dataColumn, dataColumn.DataType);
        //        dataTable.Columns.Add(newColumn);

        //        for (int i = 0; i < dataColumn.Table.Rows.Count; i++)
        //        {
        //            while (dataTable.Rows.Count <= i)
        //            {
        //                dataTable.Rows.Add(dataTable.NewRow());
        //            }
        //            dataTable.Rows[i][newColumn.ColumnName] = dataColumn.Table.Rows[i][dataColumn.ColumnName];
        //        }
        //    }
        //    return dataTable;

        //    //else
        //    //{
        //    //    throw new Exception(string.Format("Data Table already contains a column named '{0}", dataColumn.ColumnName));
        //    //}

        //}
        //private bool Find(OnlineIssueHeader header)
        //{
        //    if(
        //}
        public int Getissueid(int orderid)
        {
            int result = 0;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                result = daoOnlNcdBackOff.Getissueid(orderid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public int GetScriptId(string scriptid, int adviserid, string product)
        {
            int result = 0;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                result = daoOnlNcdBackOff.GetScriptId(scriptid, adviserid, product);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public DataTable UploadAllotmentFile(DataTable dtCheckOrder, int fileTypeId, int issueId, ref string isEligbleIssue, int adviserid, string source, ref string result, string product, string filePath, int userId)
        {
            DataTable dtUploadAllotment = new DataTable();
            try
            {
                OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
                isEligbleIssue = "";
                string extractStepCode = daoOnlNcdBackOff.GetExtractStepCode(fileTypeId);

                daoOnlNcdBackOff.IsIssueAlloted(issueId, ref   result);
                if (result != string.Empty && result != "0")
                    dtUploadAllotment = daoOnlNcdBackOff.UploadAllotmentIssueDataDynamic(dtCheckOrder, issueId, ref  result, product, filePath, userId);
                else
                {
                    result = "Pls Fill Allotment Date";
                }



            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:UploadAllotmentFile()");
                object[] objects = new object[1];
                objects[0] = dtCheckOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtUploadAllotment;

        }
        public int UploadCheckOrderFile(DataTable dtCheckOrder, int fileTypeId, int issueId, ref string isEligbleIssue, int adviserid, string source, ref string result, string product, string filePath, int userId)
        {

            int nRows = 0;

            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            isEligbleIssue = "";

            try
            {
                string extractStepCode = daoOnlNcdBackOff.GetExtractStepCode(fileTypeId);
                //if (extractStepCode == "UA")
                //{
                //    daoOnlNcdBackOff.IsIssueAlloted(issueId, ref   result);
                //    if (result != string.Empty && result != "0")
                //        nRows = daoOnlNcdBackOff.UploadAllotmentIssueDataDynamic(dtCheckOrder, issueId, ref   result, product,filePath,userId);
                //    else
                //    {
                //        result = "Pls Fill Allotment Date";
                //    }

                //}
                //else
                if (extractStepCode == "UC")
                {
                    int orderId = int.Parse(dtCheckOrder.Rows[0][0].ToString());
                    int orderIssueId = daoOnlNcdBackOff.Getissueid(orderId);
                    if (orderIssueId == issueId)
                    {
                        if (product == "FI")
                        {
                            nRows = daoOnlNcdBackOff.UploadChequeIssueData(dtCheckOrder, issueId);

                        }
                        else if (product == "IP")
                        {
                            nRows = daoOnlNcdBackOff.UploadIPOChequeIssueData(dtCheckOrder, issueId);
                        }

                    }
                    else
                    {
                        isEligbleIssue = "NotEligble";
                    }
                }
                else if (extractStepCode == "UB")
                {

                    string scriptId = dtCheckOrder.Rows[0][0].ToString();
                    int scriptissueid = daoOnlNcdBackOff.GetScriptId(scriptId, adviserid, product);
                    if (scriptissueid == issueId)
                    {


                        if (dtCheckOrder.Columns.Contains("Error Text"))
                            dtCheckOrder.Columns.RemoveAt(3);
                        if (product == "FI")
                        {
                            nRows = daoOnlNcdBackOff.UploadBidSuccessData(BSEExchangeDataChange(dtCheckOrder, source), issueId);
                        }
                        else if (product == "IP")
                        {
                            nRows = daoOnlNcdBackOff.IPOUploadBidSuccessData(dtCheckOrder, issueId);
                        }

                    }
                    else
                    {
                        isEligbleIssue = "NotEligble";
                    }
                }

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:OnlinebindRandT()");
                object[] objects = new object[1];
                objects[0] = dtCheckOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return nRows;
        }

        private DataTable BSEExchangeDataChange(DataTable dt, string source)
        {
            DataTable dtCopy = new DataTable();
            if (source == "BSE")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["AIOE_ScriptId"] = dr["AIOE_ScriptId"].ToString() + '-' + dr["COID_SeriesQuantity"].ToString();
                }

                if (dt.Columns.Contains("COID_SeriesQuantity"))
                    dt.Columns.RemoveAt(2);
                dt.AcceptChanges();
            }
            return dt;
        }

        public int CheckIssueSeriesName(string Issuename, int issueid)
        {
            int result = 0;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                result = daoOnlNcdBackOff.CheckIssueSeriesName(Issuename, issueid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public DataTable BankBranchName(int bankid)
        {
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            DataTable dtBankBranchName;
            {
                dtBankBranchName = daoOnlNcdBackOff.BankBranchName(bankid);

            }
            return dtBankBranchName;
        }
        public void NSEandBSEcodeCheck(int issueid, int adviserid, string nsecode, string bsecode, ref int isBseExist, ref int isNseExist)
        {

            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                daoOnlNcdBackOff.NSEandBSEcodeCheck(issueid, adviserid, nsecode, bsecode, ref isBseExist, ref isNseExist);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public bool Deleteinvestmentcategory(int investorid)
        {
            bool blResult = false;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                //return OnlineOrderBackOfficeDao.deleteTradeBusinessDate(tradeBusinessDateVo);
                blResult = daoOnlNcdBackOff.Deleteinvestmentcategory(investorid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;
        }
        public bool DeleteIssueinvestor(int investorcategoryid)
        {
            bool blResult = false;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                //return OnlineOrderBackOfficeDao.deleteTradeBusinessDate(tradeBusinessDateVo);
                blResult = daoOnlNcdBackOff.DeleteIssueinvestor(investorcategoryid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;
        }
        public bool DeleteSubcategory(int issuesubtyperuleid)
        {
            bool blResult = false;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                //return OnlineOrderBackOfficeDao.deleteTradeBusinessDate(tradeBusinessDateVo);
                blResult = daoOnlNcdBackOff.DeleteSubcategory(issuesubtyperuleid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;
        }
        public void DeleteAvaliable(int adviserid, int InvestorCatgeoryId, int AIICST_Id, int AIDCSR_Id, int IssueDetailId, int issueId)
        {
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                daoOnlNcdBackOff.DeleteAvaliable(adviserid, InvestorCatgeoryId, AIICST_Id, AIDCSR_Id, IssueDetailId, issueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public int CheckAccountisActive(int adviserid, int customerid)
        {
            int result = 0;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                result = daoOnlNcdBackOff.CheckAccountisActive(adviserid, customerid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public DataSet GetNCDIPOAccountingExtractType(string Product)
        {
            DataSet dsExtractType;
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                dsExtractType = onlineNCDBackOfficeDao.GetNCDIPOAccountingExtractType(Product);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }
        public DataSet GetNCDIPOExtractTypeDataForFileCreation(DateTime orderDate, int AdviserId, int extractType, DateTime fromDate, DateTime toDate, int IssuerID, string Product)
        {
            DataSet dsExtractType;
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            try
            {
                dsExtractType = onlineNCDBackOfficeDao.GetNCDIPOExtractTypeDataForFileCreation(orderDate, AdviserId, extractType, fromDate, toDate, IssuerID, Product);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }

        public DataSet GetProductIssuerList(int Isactive, string Product)
        {
            DataSet dsProductIssuer;
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            try
            {
                dsProductIssuer = onlineNCDBackOfficeDao.GetProductIssuerList(Isactive, Product);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsProductIssuer;
        }

        public string GetNCDIPOProductIssuer(int IssueId)
        {
            string dsProductIssuer;
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            try
            {
                dsProductIssuer = onlineNCDBackOfficeDao.GetNCDIPOProductIssuer(IssueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsProductIssuer;
        }
        public DataTable GetIssueName(int Adviserid, string product)
        {
            DataTable dtGetIssueNamee = new DataTable();
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                dtGetIssueNamee = onlineNCDBackOfficeDao.GetIssueName(Adviserid, product);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetIssueNamee;
        }
        public DataTable GetNCDHoldings(int AdviserId, int AIMIssueId, int PageSize, int CurrentPage, string CustomerNamefilter, out int RowCount)
        {
            DataTable dtGetNCDHoldings;
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            dtGetNCDHoldings = onlineNCDBackOfficeDao.GetNCDHoldings(AdviserId, AIMIssueId, PageSize, CurrentPage, CustomerNamefilter, out RowCount);
            return dtGetNCDHoldings;
        }
        public DataSet GetNCDSubHoldings(int AdviserId, int IssueId)
        {
            DataSet dsGetNCDSubHoldings;
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            dsGetNCDSubHoldings = onlineNCDBackOfficeDao.GetNCDSubHoldings(AdviserId, IssueId);
            return dsGetNCDSubHoldings;
        }
        public int CheckBankisActive(int CustomerId)
        {
            int result = 0;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                result = daoOnlNcdBackOff.CheckBankisActive(CustomerId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public DataTable GetNCDAllotmentFileType(string fileType, string productType)
        {
            DataTable dtGetNCDAllotmentFile;

            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                dtGetNCDAllotmentFile = onlineNCDBackOfficeDao.GetNCDAllotmentFileType(fileType, productType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetNCDAllotmentFile;
        }
        public void UploadData(DataTable dtUploadFile)
        {
            OnlineNCDBackOfficeDao onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                onlineNCDBackOfficeDao.UploadData(dtUploadFile);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public int CustomerMultipleOrder(int CustomerId, int AIMissueId)
        {
            int result = 0;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            try
            {
                result = daoOnlNcdBackOff.CustomerMultipleOrder(CustomerId, AIMissueId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public bool CreateRegister(string register,int userid)
        {
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            bool bResult = false;
            try
            {
                bResult = daoOnlNcdBackOff.CreateRegister(register,userid);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable BindSyndiacte()
        {
            DataTable dt;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            dt = daoOnlNcdBackOff.BindSyndiacte();
            return dt;
        }
        public bool CreateSyndiacte(string Syndicatename, int userid)
        {
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            bool bResult = false;
            try
            {
                bResult = daoOnlNcdBackOff.CreateSyndiacte(Syndicatename, userid);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
    }
}
