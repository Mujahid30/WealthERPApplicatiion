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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="Id"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataSet GetMFMIS(string userType, int Id, DateTime dtFrom, DateTime dtTo, int RMId, int branchId, int branchHeadId, int all)
        {
            DataSet dsAdvisorMIS;
            AdvisorMISDao advisorMISDao = new AdvisorMISDao();
            try
            {
                dsAdvisorMIS = advisorMISDao.GetMFMIS(userType, Id, dtFrom, dtTo,RMId, branchId, branchHeadId, all);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="Id"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataSet GetEQMIS(string userType, int Id, DateTime dtFrom, DateTime dtTo,int rmId, int branchId, int branchHeadId, int all)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetEQMIS(userType, Id, dtFrom, dtTo, rmId, branchId, branchHeadId, all);
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

                object[] objects = new object[8];
                objects[0] = userType;
                objects[1] = Id;
                objects[2] = dtFrom;
                objects[3] = dtTo;
                objects[4] = rmId;
                objects[5] = branchId;
                objects[6] = branchHeadId;
                objects[7] = all;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <param name="AMCSearchVal"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserid"></param>
        /// <param name="branchid"></param>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <param name="amcCode"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="AMCSearchVal"></param>
        /// <param name="SchemeSearchVal"></param>
        /// <param name="CategoryFilterVal"></param>
        /// <param name="Count"></param>
        /// <param name="AllPageExportCount"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserid"></param>
        /// <param name="branchid"></param>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <param name="schemeplanid"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="AMCSearchVal"></param>
        /// <param name="SchemeSearchVal"></param>
        /// <param name="CustomerName"></param>
        /// <param name="FolioNum"></param>
        /// <param name="Count"></param>
        /// <param name="AllPageExportCount"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserid"></param>
        /// <param name="branchid"></param>
        /// <param name="rmid"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adviserid"></param>
        /// <param name="branchid"></param>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <param name="AMCSearchVal"></param>
        /// <returns></returns>
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

                object[] objects = new object[6];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }


        /* For BM Scheeme wise MIS */

        public DataSet GetMISForBM(int rmid, int branchID, int branchHeadId, int XWise, int all, DateTime valuationDate,int CurrentPage, string AMCSearchVal, out int Count, int AllPageExportCount)
        {
            DataSet dsBMMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsBMMIS = MISDao.GetMISForBM(rmid, branchID, branchHeadId, XWise, all, valuationDate,CurrentPage, AMCSearchVal, out Count, AllPageExportCount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetMISForBM()");

                object[] objects = new object[6];
                objects[0] = rmid;
                objects[1] = branchID;
                objects[2] = branchHeadId;
                objects[3] = XWise;
                objects[4] = all;
                objects[5] = valuationDate;
                objects[6] = CurrentPage;
                objects[7] = AMCSearchVal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsBMMIS;
        }

        /* End For BM MIS */
        
    }
}
