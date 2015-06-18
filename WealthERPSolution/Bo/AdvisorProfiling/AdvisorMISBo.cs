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
                dsAdvisorMIS = advisorMISDao.GetMFMIS(userType, Id, dtFrom, dtTo, RMId, branchId, branchHeadId, all);
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
        public DataSet GetEQMIS(string userType, int AdviserId, DateTime dtFrom, DateTime dtTo, int rmId, int branchId, int branchHeadId, int all)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetEQMIS(userType, AdviserId, dtFrom, dtTo, rmId, branchId, branchHeadId, all);
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
                objects[1] = AdviserId;
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
        public DataSet GetAMCSchemewiseMISForRM(int rmid, DateTime valuationDate, int amcCode, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CategoryFilterVal, out int Count, int AllPageExportCount)
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
        public DataSet GetCustomerAMCSchemewiseMISForRM(int rmid, DateTime valuationDate, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, out int Count, int AllPageExportCount)
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
                dsMIS = MISDao.GetAMCwiseMISForRM(rmid, valuationDate, AMCSearchVal);
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
        public DataSet GetAMCSchemewiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int amcCode, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CategoryFilterVal, out int Count, int AllPageExportCount)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseMISForAdviser(adviserid, branchid, rmid, valuationDate, amcCode, CurrentPage, AMCSearchVal, SchemeSearchVal, CategoryFilterVal, out Count, AllPageExportCount);
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
        public DataSet GetCustomerAMCSchemewiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, out int Count, int AllPageExportCount)
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
                dsAdvisorMIS = advisorMISDao.GetMFMISAdviser(adviserid, branchid, rmid, dtFrom, dtTo);
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

        public DataSet GetCustomerAMCSchemewiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int SchemeCode, string AgentCode, int IsAgentBasedCode, int type)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseAUMForAdviser(adviserid, branchid, rmid, valuationDate, SchemeCode, AgentCode, IsAgentBasedCode, type);
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


        public DataSet GetCustomerAMCSchemewiseAUMForAdviserForDateRange(int adviserid, int branchid, int rmid, DateTime valuationDate, int SchemeCode, DateTime fromdate, DateTime todate, string AgentCode, int IsAgentBasedCode, string UserType)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseAUMForAdviserForDateRange(adviserid, branchid, rmid, valuationDate, SchemeCode, fromdate, todate, AgentCode, IsAgentBasedCode, UserType);
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


        #region AUM MIS ForDateRange
        public DataSet GetCustomerAMCSchemewiseAUMForRMForDateRange(int rmid, DateTime valuationDate, int SchemeCode, DateTime fromdate, DateTime todate)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseAUMForRMForDateRange(rmid, valuationDate, SchemeCode, fromdate, todate);
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



        #endregion


        public DataSet GetCustomerAMCSchemewiseAUMForRM(int rmid, DateTime valuationDate, int SchemeCode)
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

        public DataSet GetAMCSchemewiseAUMForRM(int rmid, DateTime valuationDate, int AmcCode)
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


        public DataSet GetAMCSchemewiseAUMForRMForDateRange(int rmid, DateTime valuationDate, int AmcCode, DateTime fromdate, DateTime todate)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseAUMForRMForDateRange(rmid, valuationDate, AmcCode, fromdate, todate);
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

        public DataSet GetAUMForBM(int AdviserId, int rmId, int branchId, int branchHeadId, DateTime Valuationdate, int type, int AmcCode, int SchemeCode, int @IsAgentBasedCode)
        {
            DataSet dsBMMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsBMMIS = MISDao.GetAUMForBM(AdviserId, rmId, branchId, branchHeadId, Valuationdate, type, AmcCode, SchemeCode, @IsAgentBasedCode);
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

        public DataSet GetAUMForBMForDateRange(int adviserId, int rmId, int branchId, int branchHeadId, DateTime Valuationdate, int type, int AmcCode, int SchemeCode, DateTime fromdate, DateTime todate, int IsAgentBasedCode)
        {
            DataSet dsBMMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsBMMIS = MISDao.GetAUMForBMForDateRange(adviserId, rmId, branchId, branchHeadId, Valuationdate, type, AmcCode, SchemeCode, fromdate, todate, IsAgentBasedCode);
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

        public DataSet GetAMCwiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate,int type)
        {
            DataSet dsAmsWiseAUM;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsAmsWiseAUM = MISDao.GetAMCwiseAUMForAdviser(adviserid, branchid, rmid, valuationDate,type);
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

        public DataSet GetAMCSchemewiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int AmcCode, string AgentCode, int IsAgentBasedCode,int type)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseAUMForAdviser(adviserid, branchid, rmid, valuationDate, AmcCode, AgentCode, IsAgentBasedCode,type);
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

        public DataSet validateToFromDates(DateTime dtFromHldDate, DateTime dtToHldDate)
        {
            DataSet ds = new DataSet();
            AdvisorMISDao MISDao = new AdvisorMISDao();
            ds = MISDao.validateToFromDates(dtFromHldDate, dtToHldDate);
            return ds;
        }


        public DataSet GetAMCSchemewiseAUMForAdviserForDateRange(int adviserid, int branchid, int rmid, DateTime valuationDate, int AmcCode, DateTime fromdate, DateTime todate, string AgentCode, int IsAgentBasedCode, string UserType)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseAUMForAdviserForDateRange(adviserid, branchid, rmid, valuationDate, AmcCode, fromdate, todate, AgentCode, IsAgentBasedCode, UserType);
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
        /* For BM Scheeme wise MIS */

        public DataSet GetMISForBM(int rmid, int branchID, int branchHeadId, int XWise, int all, DateTime valuationDate, int amcCode, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, string CategoryFilterVal, out int Count, int AllPageExportCount)
        {
            DataSet dsBMMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsBMMIS = MISDao.GetMISForBM(rmid, branchID, branchHeadId, XWise, all, valuationDate, amcCode, schemeplanid, CurrentPage, AMCSearchVal, SchemeSearchVal, CustomerName, FolioNum, CategoryFilterVal, out Count, AllPageExportCount);
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
        public DataSet GetMFMISCommission(int adviserId, string misType, DateTime dtFrom, DateTime dtTo, out double sumTotal)
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
        public DateTime GetLatestValuationDateFromHistory(int adviserId, string assetType)
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


        public DataSet GetMFDashBoard(string userType, int adviserId, int rmId, int branchId, int branchHeadId, int All, out int i, int IsAssociates, string agentCode, int isOnline)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsMfDashBoard;
            try
            {
                dsMfDashBoard = MISDao.GetMFDashBoard(userType, adviserId, rmId, branchId, branchHeadId, All, out i, IsAssociates, agentCode,isOnline);
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

        public DataSet GetAMCTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsAMCTransactionDeatails;
            try
            {
                dsAMCTransactionDeatails = MISDao.GetAMCTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, AgentId);
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

        public DataSet GetSchemeTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AmcCode, string Category, int AgentId)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsSchemeTransactionDeatails;
            try
            {
                dsSchemeTransactionDeatails = MISDao.GetSchemeTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, AmcCode, Category, AgentId);
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

        public DataSet GetBranchTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsBranchTransactionDeatails;
            try
            {
                dsBranchTransactionDeatails = MISDao.GetBranchTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, AgentId);
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

        public DataSet GetFolioTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int SchemeCode, int AgentId)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsFolioTransactionDeatails;
            try
            {
                dsFolioTransactionDeatails = MISDao.GetFolioTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, SchemeCode, AgentId);
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

        public DataSet GetCategoryTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, string Category, int AgentId)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsCategoryTransactionDeatails;
            try
            {
                dsCategoryTransactionDeatails = MISDao.GetCategoryTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, Category, AgentId);
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

        public DataSet GetRMTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsRMTransactionDeatails;
            try
            {
                dsRMTransactionDeatails = MISDao.GetRMTransactionDeatails(userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, AgentId);
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

        public DataSet GetAllClusterTransactionDeatails(int adviserId, int rmId, int branchId, int branchHeadId, int all, DateTime fromDate, DateTime toDate, string categoryCode)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsAllClusterTransactionDeatails;
            try
            {
                dsAllClusterTransactionDeatails = MISDao.GetAllClusterTransactionDeatails(adviserId, rmId, branchId, branchHeadId, all, fromDate, toDate, categoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsAllClusterTransactionDeatails;
        }

        public DataSet GetCommissionMIS(int adviserId, string misType, DateTime dtFrom, DateTime dtTo, int AMC, int Schemecode)
        {

            DataSet dsGetMISCommission = null;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsGetMISCommission = MISDao.GetCommissionMIS(adviserId, misType, dtFrom, dtTo, AMC, Schemecode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFBo.cs:GetCommissionMIS()");

                object[] objects = new object[4];
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

        public DataSet GetCommissionMISZoneClusterWise(int adviserId, DateTime dtFrom, DateTime dtTo)
        {

            DataSet dsGetMISCommission = null;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsGetMISCommission = MISDao.GetCommissionMISZoneClusterWise(adviserId, dtFrom, dtTo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFBo.cs:GetCommissionMISZoneClusterWise()");

                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = dtFrom;
                objects[2] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMISCommission;
        }

        public DataSet GetMFReturnsDetails(string userType, int adviserid, int RmId, int branchId, int branchHeadId, int All, string strValuationDate,int customerid,int online)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetReturns;
            try
            {
                dsGetReturns = MISDao.GetMFReturnsDetails(userType, adviserid, RmId, branchId, branchHeadId, All, strValuationDate, customerid,online);
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
                objects[0] = adviserid;
                objects[1] = RmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetReturns;
        }

        public DataSet GetEQReturnsDetails(string userType, int adviserId, int rmId, int branchId, int branchHeadId, int all, string strValuationDate)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetEQReturns;
            try
            {
                dsGetEQReturns = MISDao.GetEQReturnsDetails(userType, adviserId, rmId, branchId, branchHeadId, all, strValuationDate);
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
                objects[0] = adviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetEQReturns;
        }

        public DataTable MFNPTransactionHoldingDetails(int AdviserId, int rmId, int customerId, int branchId, int branchHeadId, int all, int isGroup, string strValuationDate)
        {
            AdvisorMISDao advisorMISDao = new AdvisorMISDao();
            DataTable dtMFNPTransactionHoldingDetails;
            try
            {
                dtMFNPTransactionHoldingDetails = advisorMISDao.MFNPTransactionHoldingDetails(AdviserId, rmId, customerId, branchId, branchHeadId, all, isGroup, strValuationDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:MFNPTransactionHoldingDetails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtMFNPTransactionHoldingDetails;
        }

        public DateTime getValidAUMDate(DateTime dtFrom, DateTime dtTo)
        {
            DateTime dt = new DateTime();
            return dt;
        }

        public DataSet validateDate(DateTime fromDate, DateTime toDate)
        {
            AdvisorMISDao advisorMISDao = new AdvisorMISDao();
            DataSet dsValidateDate = new DataSet();
            dsValidateDate = advisorMISDao.validateDate(fromDate, toDate);
            return dsValidateDate;
        }
        public DataSet GetAMCwiseAUMForAssociate(int AgentId, DateTime valuationDate)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCwiseAUMForAssociate(AgentId, valuationDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetAMCwiseMISForAssociate()");

                object[] objects = new object[2];
                objects[0] = AgentId;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }

        public DataSet GetAMCSchemewiseAUMForAssociate(int AdviserId, DateTime valuationDate, int AmcCode, string AgentCode, int IsAgentBasedCode)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetAMCSchemewiseAUMForAssociate(AdviserId, valuationDate, AmcCode, AgentCode, IsAgentBasedCode);
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
                objects[0] = AgentCode;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }
        public DataSet GetCustomerAMCSchemewiseAUMForAssociate(int AdviserId, DateTime valuationDate, int SchemeCode, string AgentCode, int IsAgentBasedCode)
        {
            DataSet dsMIS;
            AdvisorMISDao MISDao = new AdvisorMISDao();
            try
            {
                dsMIS = MISDao.GetCustomerAMCSchemewiseAUMForAssociate(AdviserId, valuationDate, SchemeCode, AgentCode, IsAgentBasedCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:GetCustomerAMCSchemewiseMISForAssociate)");

                object[] objects = new object[3];
                objects[0] = AgentCode;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsMIS;
        }
        public DataSet GetCommissionReconMis(int AdviserId, int schemeid, DateTime FromDate, DateTime Todate, string category, int Issuer)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetCommissionReconMis;
            DataTable dtstructure = new DataTable();
            DataTable dtTrailSet = new DataTable();
            int Age = 0;
            try
            {
                dsGetCommissionReconMis = MISDao.GetCommissionReconMis(AdviserId, schemeid, FromDate, Todate, category, Issuer);
                //for trail
                if (dsGetCommissionReconMis.Tables.Count >= 1 && dsGetCommissionReconMis.Tables[1].Rows.Count >= 1 && dsGetCommissionReconMis.Tables[2].Rows.Count >= 1)
                {

                    dsGetCommissionReconMis.Tables[0].Columns.Add("AverageAum");
                    dsGetCommissionReconMis.Tables[0].Columns.Add("TotalDays");
                    dsGetCommissionReconMis.Tables[0].Columns.Add("AumPerDay");
                    dsGetCommissionReconMis.Tables[0].Columns.Add("TrailPerDay");
                    dsGetCommissionReconMis.Tables[0].Columns.Add("TrailForPeriod");
                    dtstructure = dsGetCommissionReconMis.Tables[1];
                    dtTrailSet = dsGetCommissionReconMis.Tables[2];
                    dtTrailSet.Columns.Add("AverageAum");
                    dtTrailSet.Columns.Add("TotalDays");
                    dtTrailSet.Columns.Add("AumPerDay");
                    dtTrailSet.Columns.Add("TrailPerDay");
                    dtTrailSet.Columns.Add("TrailForPeriod");
                    //foreach (DataRow drfinal in dsGetCommissionReconMis.Tables[0].Rows)
                    //{
                    foreach (DataRow drstructure in dtstructure.Rows)
                    {
                        foreach (DataRow drtrail in dtTrailSet.Rows)
                        {

                            if (drstructure["schemecode"].ToString() == drtrail["schemecode"].ToString())
                            {
                                if (!string.IsNullOrEmpty(drstructure["ACSR_MinInvestmentAge"].ToString()))
                                {
                                    if (!string.IsNullOrEmpty(drstructure["ACSR_MaxInvestmentAge"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(drstructure["AgeUnit"].ToString()))
                                        {
                                            if (drstructure["AgeUnit"].ToString() == "Months")
                                            {
                                                Age = int.Parse(drtrail["CMFTB_Age"].ToString()) / 12;
                                            }
                                            else if (drstructure["AgeUnit"].ToString() == "Days")
                                            {
                                                Age = int.Parse(drtrail["CMFTB_Age"].ToString()) ;
                                            }
                                            else if(drstructure["AgeUnit"].ToString() == "Years")
                                            {
                                                Age = int.Parse(drtrail["CMFTB_Age"].ToString()) / 365;
                                            }
                                        }
                                        if (Age >= int.Parse(drstructure["ACSR_MinInvestmentAge"].ToString())
                                            && Age <= int.Parse(drstructure["ACSR_MaxInvestmentAge"].ToString()))
                                        {
                                            if (!string.IsNullOrEmpty(drtrail["UNITS"].ToString()) && (!string.IsNullOrEmpty((drtrail["cumNav"].ToString()))))
                                            {
                                                drtrail["AverageAum"] = float.Parse(drtrail["UNITS"].ToString()) * float.Parse(drtrail["cumNav"].ToString());
                                            }
                                            else
                                            {
                                                drtrail["AverageAum"] = 0;
                                            }
                                            drtrail["TotalDays"] = Math.Abs(Todate.Subtract(FromDate).Days);
                                            drtrail["AumPerDay"] = float.Parse(drtrail["AverageAum"].ToString()) / int.Parse(drtrail["TotalDays"].ToString());

                                            drtrail["TrailPerDay"] = float.Parse(drstructure["ACSR_BrokerageValue"].ToString()) / 365;
                                            //float.Parse(drtrail["TrailFee"].ToString()) / 365;
                                            drtrail["TrailForPeriod"] = float.Parse(drtrail["TrailPerDay"].ToString()) * int.Parse(drtrail["TotalDays"].ToString());
                                        }
                                        else if (int.Parse(drstructure["ACSR_MaxInvestmentAge"].ToString()) == 0)
                                        {
                                            if (!string.IsNullOrEmpty(drtrail["UNITS"].ToString()) && (!string.IsNullOrEmpty((drtrail["cumNav"].ToString()))))
                                            {
                                                drtrail["AverageAum"] = float.Parse(drtrail["UNITS"].ToString()) * float.Parse(drtrail["cumNav"].ToString());
                                            }
                                            else
                                            {
                                                drtrail["AverageAum"] = 0;
                                            }
                                            drtrail["TotalDays"] = Math.Abs(DateTime.Parse(drtrail["TransactionDate"].ToString()).Subtract(FromDate).Days);
                                            drtrail["AumPerDay"] = float.Parse(drtrail["AverageAum"].ToString()) / int.Parse(drtrail["TotalDays"].ToString());
                                            drtrail["TrailPerDay"] = float.Parse(drstructure["ACSR_BrokerageValue"].ToString()) / 365;
                                            drtrail["TrailForPeriod"] = float.Parse(drtrail["TrailPerDay"].ToString()) * int.Parse(drtrail["TotalDays"].ToString());
                                        }

                                    }
                                    else if (Age >= int.Parse(drstructure["ACSR_MinInvestmentAge"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(drtrail["UNITS"].ToString()) && (!string.IsNullOrEmpty((drtrail["cumNav"].ToString()))))
                                        {
                                            drtrail["AverageAum"] = float.Parse(drtrail["UNITS"].ToString()) * float.Parse(drtrail["cumNav"].ToString());
                                        }
                                        else
                                        {
                                            drtrail["AverageAum"] = 0;
                                        }
                                        drtrail["TotalDays"] = Math.Abs(Todate.Subtract(FromDate).Days);
                                        drtrail["AumPerDay"] = float.Parse(drtrail["AverageAum"].ToString()) / int.Parse(drtrail["TotalDays"].ToString());
                                        drtrail["TrailPerDay"] = float.Parse(drstructure["ACSR_BrokerageValue"].ToString()) / 365;
                                        drtrail["TrailForPeriod"] = float.Parse(drtrail["TrailPerDay"].ToString()) * int.Parse(drtrail["TotalDays"].ToString());
                                    }


                                }
                                //else
                                //{
                                //    if (!string.IsNullOrEmpty(drtrail["UNITS"].ToString()) && (!string.IsNullOrEmpty((drtrail["cumNav"].ToString()))))
                                //    {
                                //        drtrail["AverageAum"] = float.Parse(drtrail["UNITS"].ToString()) * float.Parse(drtrail["cumNav"].ToString());
                                //    }
                                //    else
                                //    {
                                //        drtrail["AverageAum"] = 0;
                                //    }
                                //    drtrail["TotalDays"] = Math.Abs(Todate.Subtract(FromDate).Days);
                                //    drtrail["AumPerDay"] = float.Parse(drtrail["AverageAum"].ToString()) / int.Parse(drtrail["TotalDays"].ToString());
                                //    drtrail["TrailPerDay"] = float.Parse(drstructure["ACSR_BrokerageValue"].ToString()) / 365;
                                //    drtrail["TrailForPeriod"] = float.Parse(drtrail["TrailPerDay"].ToString()) * int.Parse(drtrail["TotalDays"].ToString());
                                //}

                            }

                            else
                            {
                                drtrail["AverageAum"] = 0;
                                drtrail["TotalDays"] = 0;
                                drtrail["AumPerDay"] = 0;
                                drtrail["TrailPerDay"] = 0;
                                drtrail["TrailForPeriod"] = 0;


                            }
                            dsGetCommissionReconMis.Tables[0].ImportRow(drtrail);
                        }

                    }
                }
                //}
                return dsGetCommissionReconMis;
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

                object[] objects = new object[2];
                objects[0] = AdviserId;
                objects[1] = schemeid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCommissionReconMis;
        }
        /// <summary>
        /// Display Product wise transaction done from Oder Table
        /// </summary>
        /// <param name="agentcode"></param>
        /// <param name="userType"></param>
        /// <param name="AdviserId"></param>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="branchHeadId"></param>
        /// <param name="all"></param>
        /// <param name="FromDate"></param>
        /// <param name="Todate"></param>
        /// <param name="AgentId"></param>
        /// <returns></returns>
        public DataSet GetProductDetailFromMFOrder(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId,int isOnline)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetProductDetailFromMFOrder;
            try
            {
                dsGetProductDetailFromMFOrder = MISDao.GetProductDetailFromMFOrder(agentcode, userType, AdviserId, rmId, branchId, branchHeadId, all, FromDate, Todate, AgentId, isOnline);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetProductDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetProductDetailFromMFOrder;
        }
        /// <summary>
        /// Display Organization wise transaction done from Oder Table
        /// </summary>
        /// <param name="Agentcode"></param>
        /// <param name="userType"></param>
        /// <param name="adviserId"></param>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="branchHeadId"></param>
        /// <param name="all"></param>
        /// <param name="fromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public DataSet GetOrganizationDetailFromMFOrder(string Agentcode, string userType, int adviserId, int rmId, int branchId, int branchHeadId, int all, DateTime fromDate, DateTime ToDate, int agentId,int IsOnline)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetOrgDetailFromMFOrder;
            try
            {
                dsGetOrgDetailFromMFOrder = MISDao.GetOrganizationDetailFromMFOrder(Agentcode, userType, adviserId, rmId, branchId, branchHeadId, all, fromDate, ToDate, agentId,IsOnline);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetOrganizationDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetOrgDetailFromMFOrder;
        }

        public DataSet GetMemberDetailFromMFOrder(string Agentcode, string userType, int adviserId, int rmId, int branchId, int branchHeadId, int all, DateTime fromDate, DateTime ToDate, int agentId,int IsOnline)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetMemberDetailFromMFOrder;
            try
            {
                dsGetMemberDetailFromMFOrder = MISDao.GetMemberDetailFromMFOrder(Agentcode, userType, adviserId, rmId, branchId, branchHeadId, all, fromDate, ToDate, agentId,IsOnline);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMemberDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMemberDetailFromMFOrder;
        }

        public DataSet GetCommissionReceivableRecon_TrailComparision(string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category, string recontype, string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, bool isAuthenticated)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetCommissionReconMis = new DataSet();
            DataTable dtstructure = new DataTable();
            DataTable dtTrailSet = new DataTable();

            try
            {
                dsGetCommissionReconMis = MISDao.GetCommissionReceivableRecon_TrailComparision(product, typeOfTransaction, AdviserId, schemeid, month, year, category, recontype, commtype, issuer, issueId, commissionLookUpId, orderStatus, agentCode, productCategory, isAuthenticated);

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMemberDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCommissionReconMis;
        }

        public DataSet GetCommissionReceivableRecon(string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category, string recontype, string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, int isAuthenticated)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetCommissionReconMis = new DataSet();
            DataTable dtstructure = new DataTable();
            DataTable dtTrailSet = new DataTable();

            try
            {
                dsGetCommissionReconMis = MISDao.GetCommissionReceivableRecon(product, typeOfTransaction, AdviserId, schemeid, month, year, category, recontype, commtype, issuer, issueId, commissionLookUpId, orderStatus, agentCode, productCategory, isAuthenticated);

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMemberDetailFromMFOrder()");

                object[] objects = new object[3];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCommissionReconMis;
        }
        public bool MarkReconStatus(int transid)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            bool bResult = false;
            try
            {
                bResult = MISDao.MarkReconStatus(transid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool SaveReceivableReconChanges(int transid, double adjust, double expectedamount)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            bool bResult = false;
            try
            {
                bResult = MISDao.SaveReceivableReconChanges(transid, adjust, expectedamount);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public DataSet GetWERPCommissionDetails(string product,  int AdviserId, int month, int year, string category,  int issueId,  string productCategory)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            DataSet dsGetCommissionReconMis = new DataSet();
            DataTable dtstructure = new DataTable();
            DataTable dtTrailSet = new DataTable();

            try
            {
                dsGetCommissionReconMis = MISDao.GetWERPCommissionDetails (  product,AdviserId,  month,  year,  category,  issueId, productCategory);

            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetWERPCommissionDetails()");

                object[] objects = new object[3];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetCommissionReconMis;
        }
        public bool UpdateActualPayAndRec(int id, decimal ActPay, decimal ActRec, DateTime paybleDate, bool IsPayLocked, bool IsRecLocked, bool IsBulkPayble, bool isBulkReceived)
        {
            AdvisorMISDao MISDao = new AdvisorMISDao();
            bool bResult = false;
            try
            {
                bResult = MISDao.UpdateActualPayAndRec(id, ActPay, ActRec, paybleDate, IsPayLocked, IsRecLocked, IsBulkPayble, isBulkReceived);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMISBo.cs:UpdateActualPayAndRec()");

                object[] objects = new object[3];
                objects[0] = id;
                objects[1] = ActPay;
                objects[2] = ActRec;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
    }
}
