using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoOnlineOrderManagement;
using VoOnlineOrderManagemnet;
using System.Text.RegularExpressions;
using System.Web;

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
        public int ChekSeriesSequence(int seqNo, int issueId, int adviserId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.ChekSeriesSequence(seqNo, issueId, adviserId);
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
        public DataSet BindNcdCategory()
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.BindNcdCategory();
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

        public DataSet GetAllInvestorTypes(int issuerId, int issueId)
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
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeBo.cs:GetAllInvestorTypes()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public DataSet GetSubCategory(int issuerId, int issueId)
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

        public DataSet GetIssuerIssue(int issuerId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetIssuerIssue(issuerId);
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
        public void GenerateOnlineNcdExtract(int AdviserId, int UserId, string ExternalSource, string ProductAsset,int issueId, ref int isExtracted)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            onlineNCDBackOfficeDao.GenereateNcdExtract(AdviserId, UserId, ExternalSource, ProductAsset, issueId, ref isExtracted);
        }

        public DataTable GetAdviserNCDOrderBook(int adviserId, string status, DateTime dtFrom, DateTime dtTo)
        {
            DataTable dtNCDOrder;
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                dtNCDOrder = onlineNCDBackOfficeDao.GetAdviserNCDOrderBook(adviserId, status, dtFrom, dtTo);
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

        public DataTable GetOnlineNcdExtractPreview(DateTime extractDate, int adviserId, int fileTypeId, string extSource,int issueId)
        {
            KeyValuePair<string, string>[] headers = GetHeaderMapping(fileTypeId, extSource);

            if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            DataTable dtExtract = onlineNCDBackOfficeDao.GetOnlineNcdExtractPreview(extractDate, adviserId, fileTypeId, issueId).Tables[0];
            //No maping Has to do
            if (dtExtract == null) return null;

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    dtExtract.Columns[header.Key].ColumnName = header.Value;
                }
                dtExtract.AcceptChanges();
            }
            return dtExtract;
        }

        public void UpdateNcdOrderMannualMatch(int orderId, int allotmentId,ref int isAllotmented,ref int isUpdated)
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
                  onlineNCDBackOfficeDao.UpdateNcdAutoMatch(orderId, applictionNo, dpId,ref isAllotmented, ref isUpdated);
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
                    if (row.Table.Columns[j].ToString().Trim() == Header.HeaderName.Trim())
                    {
                        // int colInx = row.Table.Columns[Header.HeaderName].Ordinal;
                        //int colInx = Header.HeaderSequence;
                        string colRegex = Header.RegularExpression;
                        string colVal = row[j].ToString();
                        string colNam = Header.HeaderName;
                        if (colRegex != "NULL" )
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

        public DataTable ValidateUploadData(DataTable dtRawData, int fileTypeId, string extSource)
        {
            DataColumn serialNo = new DataColumn("SN", System.Type.GetType("System.Int32"));
            DataColumn errorCol = new DataColumn("Remarks", System.Type.GetType("System.String"), "");

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

        public int UploadCheckOrderFile(DataTable dtCheckOrder,  int fileTypeId,int issueId)
        {

            int nRows=0;
            OnlineNCDBackOfficeDao daoOnlNcdBackOff = new OnlineNCDBackOfficeDao();
            

            try
            {
                string extractStepCode = daoOnlNcdBackOff.GetExtractStepCode(fileTypeId);
                if (extractStepCode == "UA")
                {

                    nRows = daoOnlNcdBackOff.UploadAllotmentIssueData(dtCheckOrder, issueId);
                }
                else if (extractStepCode == "UC")
                {
                    nRows = daoOnlNcdBackOff.UploadChequeIssueData(dtCheckOrder, issueId);                  
                }
                else if (extractStepCode == "UB")
                {
                   if( dtCheckOrder.Columns.Contains("Error Text"))
                  dtCheckOrder.Columns.RemoveAt(3);
                    nRows = daoOnlNcdBackOff.UploadBidSuccessData(dtCheckOrder, issueId);

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

         
    }
}
