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
            DataSet dsRejectedRecords;

            try
            {
                superAdminOpsDao.DeleteDuplicateRecord(adviserId, accountId,netHolding,schemeCode,ValuationDate);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            
        }

    }
}
