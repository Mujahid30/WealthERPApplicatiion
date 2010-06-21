    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DaoAdvisorProfiling;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace BoAdvisorProfiling
{
    public class AdvisorMISBo
    {
        public DataSet GetMFMIS(string userType, int Id, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsAdvisorMIS;
            AdvisorMISDao advisorMISDao = new AdvisorMISDao();
            try
            {
                dsAdvisorMIS = advisorMISDao.GetMFMIS(userType, Id, dtFrom, dtTo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetMFMIS()");

                object[] objects = new object[1];
                objects[0] = Id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAdvisorMIS;
        }

        public DataSet GetEQMIS(string userType, int Id, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetEQMIS(userType, Id, dtFrom, dtTo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetEQMIS()");

                object[] objects = new object[4];
                objects[0] = userType;
                objects[1] = Id;
                objects[2] = dtFrom;
                objects[3] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetLoanMIS(string userType, int Id)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetLoanMIS(userType, Id);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetLoanMIS()");

                object[] objects = new object[2];
                objects[0] = userType;
                objects[1] = Id;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        /// <summary>
        /// For getting the AMC/Scheme wise MIS for RM for a valuation date
        /// </summary>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <returns>Dataset of the report</returns>
        /// //Added one parameter(AllPageExportCount) for all page export.....
        public DataSet GetAMCSchemewiseMISForRM(int rmid, DateTime valuationDate,int amcCode, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CategoryFilterVal, out int Count, int AllPageExportCount)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseMISForRM(rmid, valuationDate, amcCode, CurrentPage, AMCSearchVal, SchemeSearchVal, CategoryFilterVal, out Count, AllPageExportCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetAMCSchemewiseMISForRM()");

                object[] objects = new object[2];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        /// <summary>
        ///  For getting the Customer/Folio/AMC/Scheme wise MIS for RM for a valuation date
        /// </summary>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <returns>Dataset of the generated report</returns>
        public DataSet GetCustomerAMCSchemewiseMISForRM(int rmid, DateTime valuationDate, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, out int Count,int AllPageExportCount)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseMISForRM(rmid, valuationDate, schemeplanid, CurrentPage, AMCSearchVal, SchemeSearchVal, CustomerName, FolioNum, out Count, AllPageExportCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetCustomerAMCSchemewiseMISForRM()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = schemeplanid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetAMCwiseMISForRM(int rmid, DateTime valuationDate, string AMCSearchVal)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCwiseMISForRM(rmid, valuationDate,AMCSearchVal);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetAMCwiseMISForRM()");

                object[] objects = new object[2];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetAMCSchemewiseMISForAdviser(int adviserid,int branchid,int rmid, DateTime valuationDate,int amcCode, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CategoryFilterVal, out int Count,int AllPageExportCount)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseMISForAdviser(adviserid,branchid,rmid, valuationDate,amcCode, CurrentPage, AMCSearchVal, SchemeSearchVal, CategoryFilterVal, out Count, AllPageExportCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetAMCSchemewiseMISForAdviser()");

                object[] objects = new object[2];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetCustomerAMCSchemewiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, out int Count,int AllPageExportCount)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseMISForAdviser(adviserid, branchid, rmid, valuationDate, schemeplanid, CurrentPage, AMCSearchVal, SchemeSearchVal, CustomerName, FolioNum, out Count, AllPageExportCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetCustomerAMCSchemewiseMISForAdviser()");

                object[] objects = new object[2];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = schemeplanid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetMFMISAdviser(int adviserid, int branchid, int rmid, DateTime dtFrom, DateTime dtTo)
        {
            DataSet dsAdvisorMIS;
            AdvisorMISDao advisorMISDao = new AdvisorMISDao();
            try
            {
                dsAdvisorMIS = advisorMISDao.GetMFMISAdviser(adviserid,branchid,rmid, dtFrom, dtTo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetMFMISAdviser()");

                object[] objects = new object[1];
                objects[0] = adviserid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAdvisorMIS;
        }

        public DataSet GetAMCwiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, string AMCSearchVal)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCwiseMISForAdviser(adviserid, branchid, rmid, valuationDate, AMCSearchVal);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetAMCwiseMISForAdviser()");

                object[] objects = new object[2];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }
        
    }
}
