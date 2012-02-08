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


        public DataSet GetAllAdviserDuplicateRecords(DateTime FromDate, DateTime ToDate, int currentPage, out int count,string adviserId,string OrgName,string FolioNo,string schemename)
        {
            DataSet dsGetDuplicateRecords;
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                dsGetDuplicateRecords = superAdminOpsDao.GetAllAdviserDuplicateRecords(FromDate, ToDate,currentPage,out count,adviserId,OrgName,FolioNo,schemename);
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
                objects[2] = currentPage;
                objects[3] = adviserId;
                objects[4] = OrgName;
                objects[5] = FolioNo;
                objects[6] = schemename;
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
        public DataSet GetAllAdviserAUM(DateTime fromdate, DateTime todate, int currentPage, out int count,string orgName)
        {
            DataSet dsGetAum;
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                dsGetAum = superAdminOpsDao.GetAllAdviserAUM(fromdate, todate, currentPage, out count, orgName);
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
                object[] objects = new object[2];
                objects[0] = fromdate;
                objects[1] = todate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetAum;
        }
        public DataSet GetMfrejectedDetails(DateTime fromdate,DateTime todate,int currentPage, out int count, string rejectReasoncode, string adviserId, string processId)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            DataSet dsRejectedRecords;

            try
            {
                dsRejectedRecords = superAdminOpsDao.GetMfrejectedDetails(fromdate, todate,currentPage, out count,rejectReasoncode, adviserId, processId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dsRejectedRecords;
        }

        public void DeleteDuplicateRecord(int adviserId,int accountId,double netHolding,int schemeCode,DateTime ValuationDate)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();

            try
            {
                superAdminOpsDao.DeleteDuplicateRecord(adviserId, accountId,netHolding,schemeCode,ValuationDate);

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
                
                 ds=ProductGoldPriceDao.GetDataBetweenDatesForGoldPrice(productGoldPriceVO,productGoldPriceId,CurrentPage,out Count);
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
                 i=ProductGoldPriceDao.deleteGoldPriceDetails(productGoldPriceID);
                 return i;
            }
            catch
            {
                throw;
            }
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
        public void SyncSIPtoGoal()
        {

            DataSet dsGetAum;
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            try
            {
                superAdminOpsDao.SyncSIPtoGoal();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

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


        public DataSet GetNAVPercentage(DateTime navDate, int currentPage, out int count)
        {
            SuperAdminOpsDao superAdminOpsDao = new SuperAdminOpsDao();
            DataSet dsGetNAVPer;
            try
            {
                dsGetNAVPer = superAdminOpsDao.GetNAVPercentage(navDate, currentPage, out count);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetNAVPer;
        }
    }
}
