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

        public void GenerateOnlineNcdExtract(int AdviserId, int UserId, string ExternalSource, string ProductAsset) {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            onlineNCDBackOfficeDao.GenereateNcdExtract(AdviserId, UserId, ExternalSource, ProductAsset);
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

            KeyValuePair<string, string>[] kvpHeaders = new KeyValuePair<string, string>[dtHeaderMap.Rows.Count];
            for (int i = 0; i < nRows; i++)
            {
                string Key = dtHeaderMap.Rows[i]["COLUMN_NAME"].ToString();
                string Value = dtHeaderMap.Rows[i]["FILE_HEADER"].ToString();
                kvpHeaders[i] = new KeyValuePair<string, string>(Key, Value);
            }

            return kvpHeaders;
        }

        public DataTable GetOnlineNcdExtractPreview(DateTime extractDate, int adviserId, int fileTypeId, string extSource)
        {
            KeyValuePair<string, string>[] headers = GetHeaderMapping(fileTypeId, extSource);

            if (onlineNCDBackOfficeDao == null) onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();

            DataTable dtExtract = onlineNCDBackOfficeDao.GetOnlineNcdExtractPreview(extractDate, adviserId, fileTypeId).Tables[0];

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

        public bool UpdateNcdOrderMannualMatch(int orderId, int allotmentId)
        {
            bool result = false;
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                result = onlineNCDBackOfficeDao.UpdateNcdOrderMannualMatch(orderId, allotmentId);
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
            return result;
        }
        public bool UpdateNcdAutoMatch(int orderId, int applictionNo, string dpId)
        {
            bool result = false;
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                result = onlineNCDBackOfficeDao.UpdateNcdAutoMatch(orderId, applictionNo, dpId);
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
            return result;

        }

        public DataSet GetUnmatchedAllotments(int adviserId, int issuerId)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetUnmatchedAllotments(adviserId, issuerId);
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
        public DataSet GetAdviserOrders(int IssueId, string Product, string Status, DateTime FromDate, DateTime ToDate)
        {
            onlineNCDBackOfficeDao = new OnlineNCDBackOfficeDao();
            try
            {
                return onlineNCDBackOfficeDao.GetAdviserOrders(IssueId,Product,Status,FromDate,ToDate);
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
    }
}
