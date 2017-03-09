using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using DaoSuperAdmin;
using VoSuperAdmin;
using System.Web;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Collections;


namespace BoSuperAdmin
{
    public class SuperAdminOpsBo
    {
        /// <summary>
        /// To get all adviser  duplicate check.
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <returns></returns>


        public DataSet GetAllAdviserDuplicateRecords(DateTime FromDate, DateTime ToDate)
        {
            DataSet dsGetDuplicateRecords;
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                dsGetDuplicateRecords = superAdminOpsDao.GetAllAdviserDuplicateRecords(FromDate, ToDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {

                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo.cs:GetAllAdviserDuplicateRecords()");
                object[] objects = new object[7];
                objects[0] = FromDate;
                objects[1] = ToDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsGetDuplicateRecords;
        }
        /// <summary>
        ///  Get All Adviser's AUM
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public DataSet GetAllAdviserAUM(DateTime fromdate, DateTime todate, string asset)
        {
            DataSet dsGetAum;
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                dsGetAum = superAdminOpsDao.GetAllAdviserAUM(fromdate, todate, asset);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo.cs:GetAllAdviserAUM()");
                object[] objects = new object[3];
                objects[0] = fromdate;
                objects[1] = todate;
                objects[2] = asset;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAum;
        }
        public DataSet GetMfrejectedDetails(DateTime fromdate, DateTime todate)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            DataSet dsRejectedRecords;

            try
            {
                dsRejectedRecords = superAdminOpsDao.GetMfrejectedDetails(fromdate, todate);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsRejectedRecords;
        }

        public void DeleteDuplicateRecord(int adviserId, int accountId, double netHolding, int schemeCode, DateTime ValuationDate)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();

            try
            {
                superAdminOpsDao.DeleteDuplicateRecord(adviserId, accountId, netHolding, schemeCode, ValuationDate);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }


        }

        SuperAdminOpsDao ProductGoldPriceDao = new SuperAdminOpsDao();

        public int InsertAndUpdateGoldPrice(SuperAdminOpsVo productGoldPriceVO)
        {

            try
            {
                return ProductGoldPriceDao.InsertAndUpdateGoldPrice(productGoldPriceVO);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetDataBetweenDatesForGoldPrice(SuperAdminOpsVo productGoldPriceVO, int productGoldPriceId, int CurrentPage, out int Count)
        //public DataSet GetDataBetweenDatesForGoldPrice(SuperAdminOpsVo productGoldPriceVO)
        {
            DataSet ds = new DataSet();
            try
            {

                ds = ProductGoldPriceDao.GetDataBetweenDatesForGoldPrice(productGoldPriceVO, productGoldPriceId, CurrentPage, out Count);
            }
            catch
            {
                throw;
            }
            return ds;
        }

        public DataSet GetGoldPriceAccordingToDate(DateTime txtDateSearch)
        {
            try
            {
                return ProductGoldPriceDao.GetGoldPriceAccordingToDate(txtDateSearch);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetAllGoldPriceDetails()
        {
            SuperAdminOpsDao ProductGoldPriceDao = new SuperAdminOpsDao();
            try
            {
                return ProductGoldPriceDao.GetAllGoldPriceDetails();
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetGoldPriceAccordingToID(int productGoldPriceID)
        {
            try
            {
                return ProductGoldPriceDao.GetGoldPriceAccordingToID(productGoldPriceID);
            }
            catch
            {
                throw;
            }
        }

        //public List<SuperAdminOpsVo> GetDateList(int pgpId, int currentPage, string sortOrder, out int Count, string dateSrch)
        //{
        //    List<SuperAdminOpsVo> rmList = null;
        //    try
        //    {

        //        rmList = advisorStaffDao.GetRMList(advisorId, currentPage, sortOrder, out Count, nameSrch);

        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetRMList()");


        //        object[] objects = new object[1];
        //        objects[0] = advisorId;


        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return rmList;
        //}

        public int deleteGoldPriceDetails(int productGoldPriceID)
        {

            try
            {
                int i;
                i = ProductGoldPriceDao.deleteGoldPriceDetails(productGoldPriceID);
                return i;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteDuplicateTransactionDetailsORFolioDetails(int adviserId, string CommandName, int deleted, int cmfaAccountId,string folioNo)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao(); 
            bool completed = false;
            try
            {
                completed = superAdminOpsDao.DeleteDuplicateTransactionDetailsORFolioDetails(adviserId, CommandName, deleted, cmfaAccountId,folioNo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:ZoneClusterDetailsAddEditDelete(int adviserId,int rmId,int ZoneId, string Description, string name, string type, int createdBy,int modifiedBy,DateTime createdDate, string CommandName)");
                object[] objects = new object[10];
                objects[0] = deleted;
                objects[1] = adviserId;
                objects[2] = CommandName;
                objects[3] = cmfaAccountId;
                objects[4] = folioNo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return completed;
        }

        /// <summary>
        /// This function will get the duplicate foliodetails or the transaction details 
        /// </summary>
        /// <param name="adviserId">filter according to the adviserid or All</param>
        /// <param name="toDate">to transaction date</param>
        /// <param name="fromDate">from transaction date</param>
        /// <param name="isDuplicatesOnly">if only duplicate records are expected</param>
        /// <returns>dataset for duplicate folios or transactions</returns>
        public DataSet GetDuplicateTransactionDetailsORFolioDetails(string strMonitorFor,string strTypeOfMonitor,int adviserId, DateTime todate, DateTime fromDate,int isDuplicateOnly)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            DataSet dsDuplicateTransactions;
            try
            {
                dsDuplicateTransactions = superAdminOpsDao.GetDuplicateTransactionDetailsOrFolioDetails(strMonitorFor,strTypeOfMonitor,adviserId, todate, fromDate, isDuplicateOnly);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo.cs:GetDuplicateTransactionDetailsORFolioDetails(string strMonitorFor,string strTypeOfMonitor,int adviserId, DateTime todate, DateTime fromDate,int isDuplicateOnly)");
                object[] objects = new object[10];
                objects[0] = strMonitorFor;
                objects[1] = strTypeOfMonitor;
                objects[2] = adviserId;
                objects[3] = todate;
                objects[4] = fromDate;
                objects[5] = isDuplicateOnly;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsDuplicateTransactions;
        }

      

        public void DeleteAllDuplicates()
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                superAdminOpsDao.DeleteAllDuplicatesForASuperAdmin();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }
        public bool SyncSIPtoGoal(int adviserId)
        {

            DataSet dsGetAum;
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            bool affectedRecords;
            try
            {
                affectedRecords = superAdminOpsDao.SyncSIPtoGoal(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return affectedRecords;
        }

        public DataTable GetAdviserValuationStatus(string assetType, DateTime valuationDate)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                DataTable dtAdviserList = superAdminOpsDao.GetAdviserValuationStatus(assetType, valuationDate);
                return dtAdviserList;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo:GetAdviserValuationStatus()");

                object[] objects = new object[2];
                objects[0] = assetType;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }

        public DataTable GetAdviserListHavingSIPGoalFunding()
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                DataTable dtAdviserList = superAdminOpsDao.GetAdviserListHavingSIPGoalFunding();
                return dtAdviserList;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public DataSet GetNAVPercentage(DateTime navDate, double NavPer)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            DataSet dsGetNAVPer;
            try
            {
                dsGetNAVPer = superAdminOpsDao.GetNAVPercentage(navDate, NavPer);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetNAVPer;
        }
        public DataTable BindAdviserForUpload()
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                DataTable dtAdviserList = superAdminOpsDao.BindAdviserForUpload();
                return dtAdviserList;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public DataSet GetAdviserRmDetails(int adviserId)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            DataSet dsAdviserRMDetails = new DataSet();
            try
            {
                dsAdviserRMDetails = superAdminOpsDao.GetAdviserRmDetails(adviserId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserRMDetails;
        }
        public bool FolioStartDate(int adviserId)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            bool isComplete = false;
            try
            {

                superAdminOpsDao.FolioStartDate(adviserId);
                isComplete = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isComplete;
        }

        public DataSet CheckForBusinessDateAndIsCurrent(DateTime dtTradeDate, out bool isValidDate)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            isValidDate = false;
            DataSet ds = new DataSet();
            try
            {

               ds= superAdminOpsDao.CheckForBusinessDateAndIsCurrent(dtTradeDate,out isValidDate);
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }


        /// <summary>
        /// Get Folio and Transaction details of Uploaded data
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="nullable"></param>
        /// <param name="nullable_3"></param>
        /// <returns></returns>
        public DataSet UploadFolioTransactionReconcilation(int adviserId, DateTime fromDate, DateTime toDate)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            DataSet ds = new DataSet();
            try
            {

                ds = superAdminOpsDao.UploadFolioTransactionReconcilation(adviserId, fromDate, toDate);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SuperAdminOpsBo:UploadFolioTransactionReconcilation()");

                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
    }
}
