﻿    using System;
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

        public DataSet GetCustomerAMCSchemewiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate,int SchemeCode)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseAUMForAdviser(adviserid, branchid, rmid, valuationDate, SchemeCode);
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

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetCustomerAMCSchemewiseAUMForRM(int rmid, DateTime valuationDate,int SchemeCode)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseAUMForRM(rmid, valuationDate, SchemeCode);
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

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetAMCSchemewiseAUMForRM(int rmid, DateTime valuationDate,int AmcCode)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseAUMForRM(rmid, valuationDate, AmcCode);
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

        public DataSet GetAMCwiseAUMForRM(int rmid, DateTime valuationDate)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCwiseAUMForRM(rmid, valuationDate);
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

        public DataSet GetAUMForBM(int rmid, int branchID, int branchHeadId, int XWise, int all, DateTime valuationDate)
        {
            DataSet dsBMMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsBMMIS = MISDao.GetAUMForBM(rmid, branchID, branchHeadId, XWise, all, valuationDate);
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

                object[] objects = new object[15];
                objects[0] = rmid;
                objects[1] = branchID;
                objects[2] = branchHeadId;
                objects[3] = XWise;
                objects[4] = all;
                objects[5] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsBMMIS;
        }

        public DataSet GetAUMForBM(int rmId, int branchId, int branchHeadId, DateTime Valuationdate, int type, int AmcCode, int SchemeCode)
        {
            DataSet dsBMMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsBMMIS = MISDao.GetAUMForBM(rmId, branchId, branchHeadId, Valuationdate, type, AmcCode, SchemeCode);
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
                object[] objects = new object[15];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsBMMIS;
        }
        #region AUM MIS
        
        public DataSet GetAMCwiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate)
        {
            DataSet dsAmsWiseAUM;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsAmsWiseAUM = MISDao.GetAMCwiseAUMForAdviser(adviserid, branchid, rmid, valuationDate);
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

            return dsAmsWiseAUM;
        }

        public DataSet GetAMCSchemewiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate,int AmcCode)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseAUMForAdviser(adviserid, branchid, rmid, valuationDate, AmcCode);
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
        #endregion

        /* For BM Scheeme wise MIS */

        public DataSet GetMISForBM(int rmid, int branchID, int branchHeadId, int XWise, int all, DateTime valuationDate, int amcCode, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, string CategoryFilterVal, out int Count, int AllPageExportCount)
        {
            DataSet dsBMMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsBMMIS = MISDao.GetMISForBM(rmid, branchID, branchHeadId, XWise, all, valuationDate, amcCode, schemeplanid,CurrentPage, AMCSearchVal,SchemeSearchVal,CustomerName, FolioNum,CategoryFilterVal, out Count, AllPageExportCount);
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

                object[] objects = new object[15];
                objects[0] = rmid;
                objects[1] = branchID;
                objects[2] = branchHeadId;
                objects[3] = XWise;
                objects[4] = all;
                objects[5] = valuationDate;
                objects[6] = amcCode;
                objects[7] = schemeplanid;
                objects[8] = CurrentPage;
                objects[9] = AMCSearchVal;
                objects[10] = SchemeSearchVal;
                objects[11] = CategoryFilterVal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsBMMIS;
        }

        /* End For BM MIS */



        public DataSet GetAllUsersEQMISForComSec(string userType, DateTime valuationDate, int adviserId, int RMId, int BranchId, int branchHeadId, int all, int EQMIStype, int portfolioType)
        {
            DataSet dsEQMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsEQMIS = MISDao.GetAllUsersEQMISForComSec(userType, valuationDate, adviserId, RMId, BranchId, branchHeadId, all, EQMIStype, portfolioType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetAllUsersEQMISForComSec()");

                object[] objects = new object[11];
                objects[0] = userType;
                objects[1] = valuationDate;
                objects[2] = EQMIStype;
                objects[3] = portfolioType;
                objects[4] = adviserId;
                objects[5] = RMId;
                objects[6] = BranchId;
                objects[7] = branchHeadId;
                objects[8] = all;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsEQMIS;
        }
        

   /// <summary>
   /// For getting the MIS wise transaction grid with total brokerage amount
   /// </summary>
   /// <param name="userId"></param>
   /// <param name="misType"></param>
   /// <param name="dtFrom"></param>
   /// <param name="dtTo"></param>
   /// <param name="currentPage"></param>
   /// <param name="count"></param>
   /// <param name="sumTotal"></param>
   /// <returns></returns>
        public DataSet GetMFMISCommission(int adviserId, string misType, DateTime dtFrom, DateTime dtTo,out double sumTotal)
        {
           
            DataSet dsGetMISCommission = null;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsGetMISCommission = MISDao.GetMFMISCommission(adviserId, misType, dtFrom, dtTo, out sumTotal);               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFBo.cs:GetMFMISCommission()");

                object[] objects = new object[5];
                objects[0] = adviserId;
                objects[1] = misType;
                objects[2] = dtFrom;
                objects[3] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMISCommission;
        }
        public DateTime GetLatestValuationDateFromHistory(int adviserId,string assetType)
        {

            DateTime latestValuationDate = new DateTime();
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                latestValuationDate = MISDao.GetLatestValuationDateFromHistory(adviserId, assetType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            
            return latestValuationDate;
        }


        public DataSet GetMFDashBoard(int adviserId,out int i)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsMfDashBoard;
            try
            {
                dsMfDashBoard = MISDao.GetMFDashBoard(adviserId,out i);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsMfDashBoard;
        }

        public DataSet getTurnOverCategoryList()
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsCategoryList;
            try
            {
                dsCategoryList = MISDao.getTurnOverCategoryList();
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsCategoryList;
        }

        public DataSet GetAMCTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsAMCTransactionDeatails;
            try
            {
                dsAMCTransactionDeatails = MISDao.GetAMCTransactionDeatails(userType,AdviserId,rmId,branchId,branchHeadId,all,FromDate,Todate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCTransactionDeatails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAMCTransactionDeatails;
        }

        public DataSet GetSchemeTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AmcCode, string Category)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsSchemeTransactionDeatails;
            try
            {
                dsSchemeTransactionDeatails = MISDao.GetSchemeTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, AmcCode, Category);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetSchemeTransactionDeatails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSchemeTransactionDeatails;
        }

        public DataSet GetBranchTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsBranchTransactionDeatails;
            try
            {
                dsBranchTransactionDeatails = MISDao.GetBranchTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetBranchTransactionDeatails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsBranchTransactionDeatails;
        }

        public DataSet GetFolioTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate,int SchemeCode)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsFolioTransactionDeatails;
            try
            {
                dsFolioTransactionDeatails = MISDao.GetFolioTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, SchemeCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetFolioTransactionDeatails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFolioTransactionDeatails;
        }

        public DataSet GetCategoryTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate,string Category)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsCategoryTransactionDeatails;
            try
            {
                dsCategoryTransactionDeatails = MISDao.GetCategoryTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate,Category);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCategoryTransactionDeatails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsCategoryTransactionDeatails;
        }

        public DataSet GetRMTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsRMTransactionDeatails;
            try
            {
                dsRMTransactionDeatails = MISDao.GetRMTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetRMTransactionDeatails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsRMTransactionDeatails;
        }

        public DataSet GetAllClusterTransactionDeatails(int adviserId,int rmId,int branchId,int branchHeadId,int all, DateTime fromDate, DateTime toDate,string categoryCode)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsAllClusterTransactionDeatails;
            try
            {
                dsAllClusterTransactionDeatails = MISDao.GetAllClusterTransactionDeatails(adviserId,rmId,branchId,branchHeadId,all, fromDate, toDate,categoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsAllClusterTransactionDeatails;
        }                
    }
}
