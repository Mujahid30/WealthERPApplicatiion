using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoAdvisorProfiling
{
    public class AdvisorMISDao
    {

        public DataSet GetMFMIS(string userType, int Id, DateTime dtFrom, DateTime dtTo, int RMId, int branchId, int branchHeadId, int all)
        {
            Database db;
            DbCommand getMFMICmd;
            DataSet dsGetMFMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFMICmd = db.GetStoredProcCommand("SP_GetMFMIS");
                db.AddInParameter(getMFMICmd, "@userType", DbType.String, userType);
                db.AddInParameter(getMFMICmd, "@id", DbType.Int32, Id);
                db.AddInParameter(getMFMICmd, "@dtFrom", DbType.DateTime, dtFrom);
                db.AddInParameter(getMFMICmd, "@dtTo", DbType.DateTime, dtTo);
                db.AddInParameter(getMFMICmd, "@RMId", DbType.Int16, RMId);
                db.AddInParameter(getMFMICmd, "@branchId", DbType.Int16, branchId);
                db.AddInParameter(getMFMICmd, "@branchHeadId", DbType.Int16, branchHeadId);
                db.AddInParameter(getMFMICmd, "@all", DbType.Int16, all);

                dsGetMFMIS = db.ExecuteDataSet(getMFMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMFMIS()");

                object[] objects = new object[7];
                objects[0] = userType;
                objects[1] = Id;
                objects[2] = dtFrom;
                objects[3] = dtTo;
                objects[4] = branchId;
                objects[5] = branchHeadId;
                objects[6] = all;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMFMIS;
        }


        public DataSet GetEQMIS(string userType, int AdviserId, DateTime dtFrom, DateTime dtTo, int rmId, int branchId, int branchHeadId, int all)
        {
            Database db;
            DbCommand getEQMICmd;
            DataSet dsGetEQMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEQMICmd = db.GetStoredProcCommand("SP_GetEQMIS");
                db.AddInParameter(getEQMICmd, "@userType", DbType.String, userType);
                db.AddInParameter(getEQMICmd, "@id", DbType.Int32, AdviserId);
                db.AddInParameter(getEQMICmd, "@dtFrom", DbType.DateTime, dtFrom);
                db.AddInParameter(getEQMICmd, "@dtTo", DbType.DateTime, dtTo);
                if (rmId != 0)
                    db.AddInParameter(getEQMICmd, "@rmId", DbType.Int16, rmId);
                if (branchId != 0)
                    db.AddInParameter(getEQMICmd, "@branchId", DbType.Int16, branchId);
                if (branchHeadId != 0)
                    db.AddInParameter(getEQMICmd, "@branchHeadId", DbType.Int16, branchHeadId);

                db.AddInParameter(getEQMICmd, "@all", DbType.Int16, all);


                dsGetEQMIS = db.ExecuteDataSet(getEQMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetEQMIS()");

                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetEQMIS;
        }

        /// <summary>
        /// Created For Getting Company & Sector wise EQMIS for all 3 users 
        /// </summary>
        /// Created by Vinayak Patil
        /// <param name="userType"></param>
        /// <param name="valuationDate"></param>
        /// <param name="adviserId"></param>
        /// <param name="RMId"></param>
        /// <param name="BranchId"></param>
        /// <param name="branchHeadId"></param>
        /// <param name="all"></param>
        /// <param name="EQMIStype"></param>
        /// <param name="portfolioType"></param>
        /// <returns></returns>

        public DataSet GetAllUsersEQMISForComSec(string userType, DateTime valuationDate, int adviserId, int RMId, int BranchId, int branchHeadId, int all, int EQMIStype, int portfolioType)
        {
            Database db;
            DbCommand getEQMISCmd;
            DataSet dsGetAllUsersEQMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEQMISCmd = db.GetStoredProcCommand("SP_AllUserEquityMIS");

                db.AddInParameter(getEQMISCmd, "@userType", DbType.String, userType);
                db.AddInParameter(getEQMISCmd, "@valuation_Date", DbType.DateTime, valuationDate);
                db.AddInParameter(getEQMISCmd, "@EQMIStype", DbType.Int16, EQMIStype);
                db.AddInParameter(getEQMISCmd, "@portfolioType", DbType.Int16, portfolioType);


                if (adviserId != 0)
                    db.AddInParameter(getEQMISCmd, "@adviserId", DbType.Int16, adviserId);
                else
                    db.AddInParameter(getEQMISCmd, "@adviserId", DbType.Int16, DBNull.Value);

                if (RMId != 0)
                    db.AddInParameter(getEQMISCmd, "@RMId", DbType.Int16, RMId);
                else
                    db.AddInParameter(getEQMISCmd, "@RMId", DbType.Int16, DBNull.Value);

                if (BranchId != 0)
                    db.AddInParameter(getEQMISCmd, "@BranchId", DbType.Int16, BranchId);
                else
                    db.AddInParameter(getEQMISCmd, "@BranchId", DbType.Int16, DBNull.Value);

                if (branchHeadId != 0)
                    db.AddInParameter(getEQMISCmd, "@branchHeadId", DbType.Int16, branchHeadId);
                else
                    db.AddInParameter(getEQMISCmd, "@branchHeadId", DbType.Int16, DBNull.Value);

                if (all != 0)
                    db.AddInParameter(getEQMISCmd, "@all", DbType.Int16, all);


                dsGetAllUsersEQMIS = db.ExecuteDataSet(getEQMISCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAllUsersEQMISForComSec()");

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
            return dsGetAllUsersEQMIS;
        }
        //End


        public DataSet GetLoanMIS(string userType, int Id)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet dsGetLoanMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetLoanMIS");
                db.AddInParameter(getLoanMICmd, "@userType", DbType.String, userType);
                db.AddInParameter(getLoanMICmd, "@id", DbType.Int32, Id);
                dsGetLoanMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetLoanMIS()");

                object[] objects = new object[2];
                objects[0] = Id;
                objects[1] = userType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetLoanMIS;
        }

        /// <summary>
        /// For getting the AMC/Scheme wise MIS for RM for a valuation date
        /// </summary>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <returns>Dataset of the report</returns>
        public DataSet GetAMCSchemewiseMISForRM(int rmid, DateTime valuationDate, int amcCode, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CategoryFilterVal, out int Count, int AllPageExportCount)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCSchemewiseMISForRM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (amcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, amcCode);

                db.AddInParameter(getLoanMICmd, "@currentPage", DbType.Int32, CurrentPage);

                if (AMCSearchVal != "")
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, AMCSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, DBNull.Value);
                if (SchemeSearchVal != "")
                    db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, SchemeSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, DBNull.Value);
                if (CategoryFilterVal != "")
                    db.AddInParameter(getLoanMICmd, "@CategoryFilterVal", DbType.String, CategoryFilterVal);
                else
                    db.AddInParameter(getLoanMICmd, "@CategoryFilterVal", DbType.String, DBNull.Value);
                //All Page export Logic changed By "parmod"
                if (AllPageExportCount != 0)
                {
                    db.AddInParameter(getLoanMICmd, "@AllPageExportCount", DbType.Int32, AllPageExportCount);
                }

                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForRM()");

                object[] objects = new object[5];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = AMCSearchVal;
                objects[3] = SchemeSearchVal;
                objects[4] = CategoryFilterVal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            Count = Int32.Parse(AMCSchemewiseMIS.Tables[1].Rows[0]["CNT"].ToString());
            return AMCSchemewiseMIS;

        }
        /// <summary>
        ///  For getting the Customer/Folio/AMC/Scheme wise MIS for RM for a valuation date
        /// </summary>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <returns></returns>
        public DataSet GetCustomerAMCSchemewiseMISForRM(int rmid, DateTime valuationDate, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, out int Count, int AllPageExportCount)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            Count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetCustomerAMCSchemewiseMISForRM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                db.AddInParameter(getLoanMICmd, "@SchemePlanCode", DbType.Int32, schemeplanid);
                db.AddInParameter(getLoanMICmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, AMCSearchVal);
                db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, SchemeSearchVal);
                db.AddInParameter(getLoanMICmd, "@CustomerName", DbType.String, CustomerName);
                db.AddInParameter(getLoanMICmd, "@FolioNum", DbType.String, FolioNum);
                //All Page export Logic changed By "parmod"
                if (AllPageExportCount != 0)
                {
                    db.AddInParameter(getLoanMICmd, "@AllPageExportCount", DbType.Int32, AllPageExportCount);
                }
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCustomerAMCSchemewiseMISForRM()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = schemeplanid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            if (AMCSchemewiseMIS.Tables[1].Rows.Count > 0)
                Count = Int32.Parse(AMCSchemewiseMIS.Tables[1].Rows[0]["CNT"].ToString());
            return AMCSchemewiseMIS;

        }
        /// <summary>
        ///  For getting the Customer/Folio/AMC wise MIS for RM for a valuation date
        /// </summary>
        /// <param name="rmid"></param>
        /// <param name="valuationDate"></param>
        /// <returns></returns>
        public DataSet GetAMCwiseMISForRM(int rmid, DateTime valuationDate, string AMCSearchVal)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCwiseMISForRM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (AMCSearchVal != "")
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, AMCSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, DBNull.Value);

                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCwiseMISForRM()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = AMCSearchVal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;

        }

        public DataSet GetAMCSchemewiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int amcCode, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CategoryFilterVal, out int Count, int AllPageExportCount)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCSchemewiseMISForAdviser");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);

                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);

                if (amcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, amcCode);

                db.AddInParameter(getLoanMICmd, "@currentPage", DbType.Int32, CurrentPage);

                if (AMCSearchVal != "")
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, AMCSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, DBNull.Value);
                if (SchemeSearchVal != "")
                    db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, SchemeSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, DBNull.Value);
                if (CategoryFilterVal != "")
                    db.AddInParameter(getLoanMICmd, "@CategoryFilterVal", DbType.String, CategoryFilterVal);
                else
                    db.AddInParameter(getLoanMICmd, "@CategoryFilterVal", DbType.String, DBNull.Value);
                if (AllPageExportCount != 0)
                {
                    db.AddInParameter(getLoanMICmd, "@AllPageExportCount", DbType.Int32, AllPageExportCount);
                }

                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForAdviser()");

                object[] objects = new object[5];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = AMCSearchVal;
                objects[3] = SchemeSearchVal;
                objects[4] = CategoryFilterVal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            Count = Int32.Parse(AMCSchemewiseMIS.Tables[1].Rows[0]["CNT"].ToString());
            return AMCSchemewiseMIS;

        }

        public DataSet GetCustomerAMCSchemewiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, out int Count, int AllPageExportCount)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetCustomerAMCSchemewiseMISForAdviser");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                else
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, DBNull.Value);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                else
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, DBNull.Value);

                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                db.AddInParameter(getLoanMICmd, "@SchemePlanCode", DbType.Int32, schemeplanid);
                db.AddInParameter(getLoanMICmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, AMCSearchVal);
                db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, SchemeSearchVal);
                db.AddInParameter(getLoanMICmd, "@CustomerName", DbType.String, CustomerName);
                db.AddInParameter(getLoanMICmd, "@FolioNum", DbType.String, FolioNum);
                if (AllPageExportCount != 0)
                {
                    db.AddInParameter(getLoanMICmd, "@AllPageExportCount", DbType.Int32, AllPageExportCount);
                }
                getLoanMICmd.CommandTimeout = 60 * 60;
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCustomerAMCSchemewiseMISForAdviser()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = schemeplanid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            Count = Int32.Parse(AMCSchemewiseMIS.Tables[1].Rows[0]["CNT"].ToString());
            return AMCSchemewiseMIS;

        }

        public DataSet GetMFMISAdviser(int adviserid, int branchid, int rmid, DateTime dtFrom, DateTime dtTo)
        {
            Database db;
            DbCommand getMFMICmd;
            DataSet dsGetMFMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFMICmd = db.GetStoredProcCommand("SP_GetMFMISAdviser");
                db.AddInParameter(getMFMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getMFMICmd, "@AB_BranchId", DbType.Int32, branchid);
                if (rmid != 0)
                    db.AddInParameter(getMFMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getMFMICmd, "@dtFrom", DbType.DateTime, dtFrom);
                db.AddInParameter(getMFMICmd, "@dtTo", DbType.DateTime, dtTo);
                dsGetMFMIS = db.ExecuteDataSet(getMFMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMFMISAdviser()");

                object[] objects = new object[5];
                objects[0] = adviserid;
                objects[1] = branchid;
                objects[2] = rmid;
                objects[3] = dtFrom;
                objects[4] = dtTo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMFMIS;
        }

        public DataSet GetAMCwiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, string AMCSearchVal)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCwiseMISForAdviser");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                else
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, DBNull.Value);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                else
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, DBNull.Value);

                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);

                if (AMCSearchVal != "")
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, AMCSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, DBNull.Value);

                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCwiseMISForAdviser()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                objects[2] = AMCSearchVal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;

        }


        #region AUM MIS Date Range
        public DataSet GetCustomerAMCSchemewiseAUMForAdviserForDateRange(int adviserid, int branchid, int rmid, DateTime valuationDate, int SchemeCode, DateTime fromdate, DateTime todate, string AgentCode, int IsAgentBasedCode, string UserType)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SPROC_GetCustomerAMCSchemewiseAUMForAdviserForDateRange");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                else
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, DBNull.Value);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                else
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, DBNull.Value);
                if (fromdate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, fromdate);
                else
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, DBNull.Value);

                if (todate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, todate);
                else
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, DBNull.Value);



                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (SchemeCode != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);
                db.AddInParameter(getLoanMICmd, "@UserType", DbType.String, @UserType);
                getLoanMICmd.CommandTimeout = 60 * 60;
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCustomerAMCSchemewiseMISForAdviser()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;

        }
        public DataSet GetAUMForBMForDateRange(int rmid, int branchID, int branchHeadId, int XWise, int all, DateTime valuationDate, DateTime fromdate, DateTime todate)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAllAMCwiseMISforBM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@BranchId", DbType.Int32, branchID);
                db.AddInParameter(getLoanMICmd, "@BranchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getLoanMICmd, "@all", DbType.Int32, all);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                db.AddInParameter(getLoanMICmd, "@XWise", DbType.Int32, XWise);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMISForBM()");

                object[] objects = new object[8];
                objects[0] = rmid;
                objects[1] = branchID;
                objects[2] = branchHeadId;
                objects[3] = all;
                objects[4] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }
        public DataSet GetCustomerAMCSchemewiseAUMForRMForDateRange(int rmid, DateTime valuationDate, int SchemeCode, DateTime fromdate, DateTime todate)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SPROC_GetCustomerAMCSchemewiseAUMForRMForDateRange");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (SchemeCode != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, DBNull.Value);

                if (fromdate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, fromdate);
                else
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, DBNull.Value);

                if (todate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, todate);
                else
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, DBNull.Value);

                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCustomerAMCSchemewiseMISForRM()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }
        #endregion


        public DataSet GetCustomerAMCSchemewiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int SchemeCode, string AgentCode, int IsAgentBasedCode, int type)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                //, string AgentCode, int IsAgentBasedCode
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetCustomerAMCSchemewiseAUMForAdviser");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                else
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, DBNull.Value);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                else
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (SchemeCode != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);
                db.AddInParameter(getLoanMICmd, "@online", DbType.Int32, type);

                getLoanMICmd.CommandTimeout = 60 * 60;
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCustomerAMCSchemewiseMISForAdviser()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;

        }

        public DataSet GetCustomerAMCSchemewiseAUMForRM(int rmid, DateTime valuationDate, int SchemeCode)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetCustomerAMCSchemewiseAUMForRM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (SchemeCode != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, DBNull.Value);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCustomerAMCSchemewiseMISForRM()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }

        public DataSet GetAMCSchemewiseAUMForRM(int rmid, DateTime valuationDate, int AmcCode)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCSchemewiseAUMForRM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (AmcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, DBNull.Value);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForRM()");

                object[] objects = new object[5];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }

        public DataSet GetAMCSchemewiseAUMForRMForDateRange(int rmid, DateTime valuationDate, int AmcCode, DateTime fromdate, DateTime todate)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SPROC_GetAMCSchemewiseAUMForRMForDateRange");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (AmcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, DBNull.Value);
                if (fromdate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@fromDate", DbType.DateTime, fromdate);
                else
                    db.AddInParameter(getLoanMICmd, "@fromDate", DbType.DateTime, DBNull.Value);
                if (todate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@toDate", DbType.DateTime, todate);
                else
                    db.AddInParameter(getLoanMICmd, "@toDate", DbType.DateTime, DBNull.Value);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForRM()");

                object[] objects = new object[5];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }

        public DataSet GetAMCwiseAUMForRM(int rmid, DateTime valuationDate)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCwiseMISForRM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCwiseMISForRM()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }

        public DataSet GetAUMForBM(int rmid, int branchID, int branchHeadId, int XWise, int all, DateTime valuationDate)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAllAMCwiseMISforBM");
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                db.AddInParameter(getLoanMICmd, "@BranchId", DbType.Int32, branchID);
                db.AddInParameter(getLoanMICmd, "@BranchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getLoanMICmd, "@all", DbType.Int32, all);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                db.AddInParameter(getLoanMICmd, "@XWise", DbType.Int32, XWise);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMISForBM()");

                object[] objects = new object[8];
                objects[0] = rmid;
                objects[1] = branchID;
                objects[2] = branchHeadId;
                objects[3] = all;
                objects[4] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }

        public DataSet GetAUMForBM(int AdviserId, int rmId, int branchId, int branchHeadId, DateTime Valuationdate, int Type, int AmcCode, int SchemeCode, int @IsAgentBasedCode)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAllAMCwiseAUMforBM");
                db.AddInParameter(getLoanMICmd, "@AdviserID", DbType.Int32, AdviserId);
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(getLoanMICmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(getLoanMICmd, "@BranchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, Valuationdate);
                db.AddInParameter(getLoanMICmd, "@XWise", DbType.Int32, Type);
                if (AmcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, DBNull.Value);
                if (SchemeCode != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMISForBM()");
                object[] objects = new object[8];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;

        }
        #region AUM MIS

        public DataSet GetAUMForBMForDateRange(int adviserId, int rmId, int branchId, int branchHeadId, DateTime Valuationdate, int Type, int AmcCode, int SchemeCode, DateTime fromdate, DateTime todate, int IsAgentBasedCode)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SPROC_GetAUMForBMForDateRange");
                db.AddInParameter(getLoanMICmd, "@AdviserID", DbType.Int32, adviserId);
                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(getLoanMICmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(getLoanMICmd, "@BranchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, Valuationdate);
                db.AddInParameter(getLoanMICmd, "@XWise", DbType.Int32, Type);
                if (AmcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, DBNull.Value);
                if (SchemeCode != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, DBNull.Value);

                //db.AddInParameter(getLoanMICmd, "@all", DbType.Int32, all);
                if (fromdate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, fromdate);
                else
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, DBNull.Value);
                if (todate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, todate);
                else
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);

                //db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                //db.AddInParameter(getLoanMICmd, "@BranchId", DbType.Int32, branchID);
                //db.AddInParameter(getLoanMICmd, "@BranchHeadId", DbType.Int32, branchHeadId);
                //db.AddInParameter(getLoanMICmd, "@all", DbType.Int32, all);
                //db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                //db.AddInParameter(getLoanMICmd, "@XWise", DbType.Int32, XWise);


                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMISForBM()");
                object[] objects = new object[8];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;

        }


        public DataSet GetAMCwiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int type)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCwiseAUMForAdviser = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCwiseAUMForAdviser");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                //else
                //    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, 0);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                //else
                //    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, 0);

                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate.ToString("dd/MM/yyyy"));
                db.AddInParameter(getLoanMICmd, "@online", DbType.Int32, type);

                AMCwiseAUMForAdviser = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCwiseMISForAdviser()");

                object[] objects = new object[3];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCwiseAUMForAdviser;

        }

        public DataSet GetAMCSchemewiseAUMForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate, int AmcCode, string AgentCode, int IsAgentBasedCode,int type)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCSchemewiseAUMForAdviser");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                else
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, DBNull.Value);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                else
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (AmcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AmcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AmcCode", DbType.Int32, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);
                db.AddInParameter(getLoanMICmd, "@online", DbType.Int32, type);

                getLoanMICmd.CommandTimeout = 60 * 60;
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForAdviser()");

                object[] objects = new object[5];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }

        #endregion



        public DataSet validateToFromDates(DateTime dtFromHldDate, DateTime dtToHldDate)
        {
            Database db;
            DbCommand validateToFromDatesCmd;
            DataSet ds = new DataSet();
            bool isValid = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                validateToFromDatesCmd = db.GetStoredProcCommand("SPROC_validateToFromDates");
                db.AddInParameter(validateToFromDatesCmd, "@dtFromHldDate", DbType.DateTime, dtFromHldDate);
                db.AddInParameter(validateToFromDatesCmd, "@dtToHldDate", DbType.DateTime, dtToHldDate);
                ds = db.ExecuteDataSet(validateToFromDatesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForAdviser()");

                object[] objects = new object[5];
                objects[0] = dtFromHldDate;
                objects[1] = dtToHldDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetAMCSchemewiseAUMForAdviserForDateRange(int adviserid, int branchid, int rmid, DateTime valuationDate, int AmcCode, DateTime fromdate, DateTime todate, string AgentCode, int IsAgentBasedCode, string UserType)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SPROC_GetAMCSchemewiseAUMForAdviserForDateRange");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if (branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                else
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, DBNull.Value);
                if (rmid != 0)
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);
                else
                    db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, DBNull.Value);
                //db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (AmcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AmcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AmcCode", DbType.Int32, DBNull.Value);

                if (fromdate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, fromdate);
                else
                    db.AddInParameter(getLoanMICmd, "@fromdate", DbType.DateTime, DBNull.Value);

                if (todate != DateTime.MinValue)
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, todate);
                else
                    db.AddInParameter(getLoanMICmd, "@todate", DbType.DateTime, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.Int32, AgentCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);
                getLoanMICmd.CommandTimeout = 60 * 60;
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForAdviser()");

                object[] objects = new object[5];
                objects[0] = rmid;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }

        /* For BMMIS for Scheeme wise */

        public DataSet GetMISForBM(int rmid, int branchID, int branchHeadId, int XWise, int all, DateTime valuationDate, int amcCode, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, string CategoryFilterVal, out int Count, int AllPageExportCount)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAllAMCwiseMISforBM");

                db.AddInParameter(getLoanMICmd, "@RMId", DbType.Int32, rmid);



                db.AddInParameter(getLoanMICmd, "@BranchId", DbType.Int32, branchID);
                db.AddInParameter(getLoanMICmd, "@BranchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getLoanMICmd, "@all", DbType.Int32, all);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);

                if (amcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, amcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, DBNull.Value);

                if (schemeplanid != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemePlanCode", DbType.Int32, schemeplanid);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemePlanCode", DbType.Int32, DBNull.Value);

                db.AddInParameter(getLoanMICmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getLoanMICmd, "@XWise", DbType.Int32, XWise);
                if (AMCSearchVal != "")
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, AMCSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCSearchVal", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(SchemeSearchVal.Trim()))
                    db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, SchemeSearchVal);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeSearchVal", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(CustomerName.Trim()))
                    db.AddInParameter(getLoanMICmd, "@CustomerName", DbType.String, CustomerName);
                else
                    db.AddInParameter(getLoanMICmd, "@CustomerName", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(FolioNum.Trim()))
                    db.AddInParameter(getLoanMICmd, "@FolioNum", DbType.String, FolioNum);
                else
                    db.AddInParameter(getLoanMICmd, "@FolioNum", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(CategoryFilterVal.Trim()))
                    db.AddInParameter(getLoanMICmd, "@CategoryFilterVal", DbType.String, CategoryFilterVal);
                else
                    db.AddInParameter(getLoanMICmd, "@CategoryFilterVal", DbType.String, DBNull.Value);

                if (AllPageExportCount != 0)
                {
                    db.AddInParameter(getLoanMICmd, "@AllPageExportCount", DbType.Int32, AllPageExportCount);
                }

                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMISForBM()");

                object[] objects = new object[8];
                objects[0] = rmid;
                objects[1] = branchID;
                objects[2] = branchHeadId;
                objects[3] = all;
                objects[4] = valuationDate;
                objects[5] = AMCSearchVal;
                objects[6] = SchemeSearchVal;
                objects[7] = CategoryFilterVal;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            Count = Int32.Parse(AMCSchemewiseMIS.Tables[1].Rows[0]["CNT"].ToString());
            return AMCSchemewiseMIS;

        }
        /* *************** */
        /// <summary>
        /// passing the userId and MIS type  to the funtion,getting the Commission MIS structure 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="misType"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="sumToatal"></param>
        /// <returns></returns>
        public DataSet GetMFMISCommission(int adviserId, string misType, DateTime dtFrom, DateTime dtTo, out double sumToatal)
        {
            Database db;
            DbCommand getMISCommissionCmd;
            DataSet dsGetMISCommission = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMISCommissionCmd = db.GetStoredProcCommand("SP_GetMISCommission");
                db.AddInParameter(getMISCommissionCmd, "@MISType", DbType.String, misType);
                db.AddInParameter(getMISCommissionCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getMISCommissionCmd, "@FromDate", DbType.Date, dtFrom);
                db.AddInParameter(getMISCommissionCmd, "@ToDate", DbType.Date, dtTo);
                db.AddOutParameter(getMISCommissionCmd, "@SumTotal", DbType.Double, 0);
                dsGetMISCommission = db.ExecuteDataSet(getMISCommissionCmd);
                if (!string.IsNullOrEmpty(db.GetParameterValue(getMISCommissionCmd, "@SumTotal").ToString()))
                    sumToatal = double.Parse(db.GetParameterValue(getMISCommissionCmd, "@SumTotal").ToString());
                else
                    sumToatal = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMFMISCommission()");

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
            Database db;
            DbCommand getLatestValuationDateFromHistoryCmd;
            DateTime latestValuationDate = new DateTime();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLatestValuationDateFromHistoryCmd = db.GetStoredProcCommand("SP_GetLatestValuationDateFromHistory");
                db.AddInParameter(getLatestValuationDateFromHistoryCmd, "@assetType", DbType.String, assetType);
                db.AddInParameter(getLatestValuationDateFromHistoryCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddOutParameter(getLatestValuationDateFromHistoryCmd, "@ValuationDate", DbType.DateTime, 0);
                getLatestValuationDateFromHistoryCmd.CommandTimeout = 60 * 60;
                db.ExecuteDataSet(getLatestValuationDateFromHistoryCmd);
                if (!string.IsNullOrEmpty(db.GetParameterValue(getLatestValuationDateFromHistoryCmd, "@ValuationDate").ToString()))
                    latestValuationDate = DateTime.Parse(db.GetParameterValue(getLatestValuationDateFromHistoryCmd, "@ValuationDate").ToString());
                else
                    latestValuationDate = DateTime.Now;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return latestValuationDate;
        }
        public DataSet GetMFDashBoard(string userType, int adviserId, int rmId, int branchId, int branchHeadId, int All, out int i, int IsAssociates, string agentCode,int isOnline)
        {
            Database db;
            DbCommand getMFDashBoardCmd;
            DataSet dsGetMFDashBoard = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFDashBoardCmd = db.GetStoredProcCommand("SP_GetMFDashBoard");
                db.AddInParameter(getMFDashBoardCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(getMFDashBoardCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getMFDashBoardCmd, "@agentCode", DbType.String, agentCode);
                db.AddInParameter(getMFDashBoardCmd, "@IsAssociates", DbType.Int32, IsAssociates);
                db.AddInParameter(getMFDashBoardCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(getMFDashBoardCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getMFDashBoardCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(getMFDashBoardCmd, "@all", DbType.Int32, All);
                db.AddInParameter(getMFDashBoardCmd, "@IsOnline", DbType.Int32, isOnline);

                db.AddOutParameter(getMFDashBoardCmd, "@month", DbType.Int32, 0);
                getMFDashBoardCmd.CommandTimeout = 60 * 60;
                dsGetMFDashBoard = db.ExecuteDataSet(getMFDashBoardCmd);
                if (!string.IsNullOrEmpty(db.GetParameterValue(getMFDashBoardCmd, "@month").ToString()))
                    i = int.Parse(db.GetParameterValue(getMFDashBoardCmd, "@month").ToString());
                else
                    i = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetMFDashBoard()");

                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMFDashBoard;
        }
        public DataSet getTurnOverCategoryList()
        {
            Database db;
            DbCommand GetCategoryListCmd;
            DataSet dsGetCategoryList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCategoryListCmd = db.GetStoredProcCommand("SP_GetMFTurnOverSummaryCategoryList");
                dsGetCategoryList = db.ExecuteDataSet(GetCategoryListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetCategoryList;
        }
        public DataSet GetAMCTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId)
        {
            Database db;
            DbCommand AMCTransactionDeatailsCmd;
            DataSet dsAMCTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AMCTransactionDeatailsCmd = db.GetStoredProcCommand("SP_GetAMCWiseTransactionDetails");
                db.AddInParameter(AMCTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(AMCTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(AMCTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(AMCTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(AMCTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(AMCTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                db.AddInParameter(AMCTransactionDeatailsCmd, "@all", DbType.Int32, all);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(AMCTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(AMCTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                //if (!string.IsNullOrEmpty(Category))
                //    db.AddInParameter(AMCTransactionDeatailsCmd, "@Category", DbType.String, Category);
                //else
                //    db.AddInParameter(AMCTransactionDeatailsCmd, "@Category", DbType.String, DBNull.Value);
                AMCTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsAMCTransactionDeatails = db.ExecuteDataSet(AMCTransactionDeatailsCmd);
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
            Database db;
            DbCommand GetSchemeTransactionDeatailsCmd;
            DataSet dsSchemeTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeTransactionDeatailsCmd = db.GetStoredProcCommand("SP_GetSchemeWiseTransactionDetails");
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@all", DbType.Int32, all);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (AmcCode != 0)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@amcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@amcCode", DbType.Int32, DBNull.Value);
                if (AgentId != 0)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                else
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(Category))
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@Category", DbType.String, Category);
                else
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@Category", DbType.String, DBNull.Value);
                GetSchemeTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsSchemeTransactionDeatails = db.ExecuteDataSet(GetSchemeTransactionDeatailsCmd);
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
            Database db;
            DbCommand GetBranchTransactionDeatailsCmd;
            DataSet dsBranchTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetBranchTransactionDeatailsCmd = db.GetStoredProcCommand("SP_GetBranchWiseTransactionDetails");
                db.AddInParameter(GetBranchTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetBranchTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetBranchTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetBranchTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetBranchTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetBranchTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                db.AddInParameter(GetBranchTransactionDeatailsCmd, "@all", DbType.Int32, all);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetBranchTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetBranchTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                GetBranchTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsBranchTransactionDeatails = db.ExecuteDataSet(GetBranchTransactionDeatailsCmd);
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
            Database db;
            DbCommand GetFolioTransactionDeatailsCmd;
            DataSet dsFolioTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetFolioTransactionDeatailsCmd = db.GetStoredProcCommand("SP_GetFolio/CustomerTransactionDetails");
                db.AddInParameter(GetFolioTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetFolioTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetFolioTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetFolioTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetFolioTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                if (AgentId != 0)
                    db.AddInParameter(GetFolioTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                else
                    db.AddInParameter(GetFolioTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                db.AddInParameter(GetFolioTransactionDeatailsCmd, "@all", DbType.Int32, all);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetFolioTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetFolioTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (SchemeCode != 0)
                    db.AddInParameter(GetFolioTransactionDeatailsCmd, "@amcCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(GetFolioTransactionDeatailsCmd, "@amcCode", DbType.Int32, DBNull.Value);

                GetFolioTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsFolioTransactionDeatails = db.ExecuteDataSet(GetFolioTransactionDeatailsCmd);
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
            Database db;
            DbCommand GetCategoryTransactionDeatailsCmd;
            DataSet dsCategoryTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCategoryTransactionDeatailsCmd = db.GetStoredProcCommand("SP_GetCategoryWiseTransactionDetails");
                db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@all", DbType.Int32, all);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Category))
                    db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@Category", DbType.String, Category);
                else
                    db.AddInParameter(GetCategoryTransactionDeatailsCmd, "@Category", DbType.String, DBNull.Value);
                GetCategoryTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsCategoryTransactionDeatails = db.ExecuteDataSet(GetCategoryTransactionDeatailsCmd);
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
        /// <summary>
        ///  Show RM Wise Transaction details 
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="AdviserId"></param>
        /// <param name="rmId"></param>
        /// <param name="branchId"></param>
        /// <param name="branchHeadId"></param>
        /// <param name="all"></param>
        /// <param name="FromDate"></param>
        /// <param name="Todate"></param>
        /// <returns></returns>
        public DataSet GetRMTransactionDeatails(string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId)
        {
            Database db;
            DbCommand getRMTransactionDeatailsCmd;
            DataSet dsRMTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMTransactionDeatailsCmd = db.GetStoredProcCommand("SPROC_GetRMWiseTransactionDetails");
                db.AddInParameter(getRMTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(getRMTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getRMTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(getRMTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getRMTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(getRMTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                db.AddInParameter(getRMTransactionDeatailsCmd, "@all", DbType.Int32, all);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(getRMTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(getRMTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                getRMTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsRMTransactionDeatails = db.ExecuteDataSet(getRMTransactionDeatailsCmd);
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
            Database db;
            DbCommand getAllClusterTransactionDeatailsCmd;
            DataSet dsAllClusterTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAllClusterTransactionDeatailsCmd = db.GetStoredProcCommand("SPROC_GetAllClusterTransactionDeatails");
                db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@all", DbType.Int32, @all);
                db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                if (toDate != DateTime.MinValue)
                    db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@FromDate", DbType.DateTime, fromDate);
                else
                    toDate = DateTime.MinValue;

                if (fromDate != DateTime.MinValue)
                    db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@ToDate", DbType.DateTime, toDate);
                else
                    fromDate = DateTime.MinValue;

                if (!string.IsNullOrEmpty(categoryCode))
                    db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@Category", DbType.String, categoryCode);
                else
                    db.AddInParameter(getAllClusterTransactionDeatailsCmd, "@Category", DbType.String, DBNull.Value);

                getAllClusterTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsAllClusterTransactionDeatails = db.ExecuteDataSet(getAllClusterTransactionDeatailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

            return dsAllClusterTransactionDeatails;
        }


        public DataSet GetCommissionMIS(int adviserId, string misType, DateTime dtFrom, DateTime dtTo, int AMC, int SchemeCode)
        {
            Database db;
            DbCommand getMISCommissionCmd;
            DataSet dsGetMISCommission = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMISCommissionCmd = db.GetStoredProcCommand("SPROC_GetCommissionMIS");
                db.AddInParameter(getMISCommissionCmd, "@MISType", DbType.String, misType);
                db.AddInParameter(getMISCommissionCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getMISCommissionCmd, "@FromDate", DbType.Date, dtFrom);
                db.AddInParameter(getMISCommissionCmd, "@ToDate", DbType.Date, dtTo);
                if (SchemeCode != 0)
                    db.AddInParameter(getMISCommissionCmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getMISCommissionCmd, "@SchemeCode", DbType.Int32, DBNull.Value);
                if (AMC != 0)
                    db.AddInParameter(getMISCommissionCmd, "@AmcCode", DbType.Int32, AMC);
                else
                    db.AddInParameter(getMISCommissionCmd, "@AmcCode", DbType.Int32, DBNull.Value);
                getMISCommissionCmd.CommandTimeout = 60 * 60;
                dsGetMISCommission = db.ExecuteDataSet(getMISCommissionCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCommissionMIS()");

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
            Database db;
            DbCommand getMISCommissionCmd;
            DataSet dsGetMISCommission = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMISCommissionCmd = db.GetStoredProcCommand("SPROC_GetCommissionMISZoneClusterWise");
                db.AddInParameter(getMISCommissionCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getMISCommissionCmd, "@FromDate", DbType.Date, dtFrom);
                db.AddInParameter(getMISCommissionCmd, "@ToDate", DbType.Date, dtTo);
                getMISCommissionCmd.CommandTimeout = 60 * 60;
                dsGetMISCommission = db.ExecuteDataSet(getMISCommissionCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCommissionMISZoneClusterWise()");

                object[] objects = new object[5];
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

        public DataSet GetMFReturnsDetails(string userType, int adviserid, int RmId, int branchId, int branchHeadId, int All, string strValuationDate, int customerid,int online)
        {
            Database db;
            DbCommand getMFReturnsCmd;
            DataSet dsGetMFReturns = new DataSet();
            DataTable dtGetReturns;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFReturnsCmd = db.GetStoredProcCommand("SPROC_GetMFReturnsDetails");
                db.AddInParameter(getMFReturnsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(getMFReturnsCmd, "@adviserId", DbType.Int32, adviserid);
                db.AddInParameter(getMFReturnsCmd, "@RMId", DbType.Int32, RmId);
                db.AddInParameter(getMFReturnsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getMFReturnsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(getMFReturnsCmd, "@all", DbType.Int32, All);
                if (strValuationDate != "01/01/0001")
                    db.AddInParameter(getMFReturnsCmd, "@valuationDate", DbType.DateTime, DateTime.Parse(strValuationDate));
                db.AddInParameter(getMFReturnsCmd, "@customerid", DbType.Int32, customerid);
                db.AddInParameter(getMFReturnsCmd, "@online", DbType.Int32, online);
                getMFReturnsCmd.CommandTimeout = 60 * 60;
                dsGetMFReturns = db.ExecuteDataSet(getMFReturnsCmd);
                //dtGetReturns = dsGetMFReturns.Tables[0];
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
            return dsGetMFReturns;
        }

        public DataSet GetEQReturnsDetails(string userType, int adviserid, int RmId, int branchId, int branchHeadId, int All, string strValuationDate)
        {
            Database db;
            DbCommand getEQReturnsCmd;
            DataSet dsGetEQReturns = new DataSet();
            DataTable dtGetEQReturns;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEQReturnsCmd = db.GetStoredProcCommand("SPROC_GetEquityReturnsDetails");
                db.AddInParameter(getEQReturnsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(getEQReturnsCmd, "@adviserId", DbType.Int32, adviserid);
                db.AddInParameter(getEQReturnsCmd, "@RMId", DbType.Int32, RmId);
                db.AddInParameter(getEQReturnsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(getEQReturnsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(getEQReturnsCmd, "@all", DbType.Int32, All);
                if (strValuationDate != "01/01/0001")
                    db.AddInParameter(getEQReturnsCmd, "@valuationDate", DbType.DateTime, DateTime.Parse(strValuationDate));
                getEQReturnsCmd.CommandTimeout = 60 * 60;
                dsGetEQReturns = db.ExecuteDataSet(getEQReturnsCmd);
                //dtGetEQReturns = dsGetEQReturns.Tables[0];
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
            return dsGetEQReturns;
        }

        public DataTable MFNPTransactionHoldingDetails(int AdviserId, int rmId, int customerId, int branchId, int branchHeadId, int all, int isGroup, string strValuationDate)
        {
            Database db;
            DbCommand MFNPTransactionHoldingDetailsCmd;
            DataSet dsMFNPTransactionHoldingDetails = new DataSet();
            DataTable dtMFNPTransactionHoldingDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                MFNPTransactionHoldingDetailsCmd = db.GetStoredProcCommand("SPROC_MFNPTransactionHoldingDetails");
                db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@IsGroup", DbType.Int16, isGroup);
                db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@all", DbType.Int32, all);
                if (strValuationDate != "01/01/0001")
                    db.AddInParameter(MFNPTransactionHoldingDetailsCmd, "@valuationDate", DbType.DateTime, DateTime.Parse(strValuationDate));
                MFNPTransactionHoldingDetailsCmd.CommandTimeout = 60 * 60;
                dsMFNPTransactionHoldingDetails = db.ExecuteDataSet(MFNPTransactionHoldingDetailsCmd);
                dtMFNPTransactionHoldingDetails = dsMFNPTransactionHoldingDetails.Tables[0];
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


        public DataSet validateDate(DateTime fromDate, DateTime toDate)
        {
            Database db;
            DbCommand validateDateCmd;
            DataSet dsvalidateDate = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                validateDateCmd = db.GetStoredProcCommand("SPROC_validateDate");
                db.AddInParameter(validateDateCmd, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(validateDateCmd, "@toDate", DbType.DateTime, toDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:validateDate(DateTime fromDate, DateTime toDate)");

                object[] objects = new object[3];
                objects[0] = fromDate;
                objects[1] = toDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsvalidateDate;
        }
        public DataSet GetAMCwiseAUMForAssociate(int AgentId, DateTime valuationDate)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCwiseMISForAssociate");
                db.AddInParameter(getLoanMICmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCwiseMISForAssociate()");

                object[] objects = new object[3];
                objects[0] = AgentId;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }
        public DataSet GetAMCSchemewiseAUMForAssociate(int AdviserId, DateTime valuationDate, int AmcCode, string AgentCode, int IsAgentBasedCode)
        {
            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCSchemewiseAUMForAssociate");
                db.AddInParameter(getLoanMICmd, "@AdviserID", DbType.Int32, AdviserId);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (AmcCode != 0)
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AMCCode", DbType.Int32, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetAMCSchemewiseMISForAssociate()");

                object[] objects = new object[5];
                objects[0] = AgentCode;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }
        public DataSet GetCustomerAMCSchemewiseAUMForAssociate(int AdviserId, DateTime valuationDate, int SchemeCode, string AgentCode, int IsAgentBasedCode)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetCustomerAMCSchemewiseAUMForAssociate");
                db.AddInParameter(getLoanMICmd, "@AdviserID", DbType.Int32, AdviserId);
                db.AddInParameter(getLoanMICmd, "@Valuation_Date", DbType.DateTime, valuationDate);
                if (SchemeCode != 0)
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, SchemeCode);
                else
                    db.AddInParameter(getLoanMICmd, "@SchemeCode", DbType.Int32, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(getLoanMICmd, "@AgentCode", DbType.String, DBNull.Value);
                db.AddInParameter(getLoanMICmd, "@IsAgentBasedCode", DbType.Int32, IsAgentBasedCode);
                AMCSchemewiseMIS = db.ExecuteDataSet(getLoanMICmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetCustomerAMCSchemewiseMISForAssociate()");

                object[] objects = new object[3];
                objects[0] = AgentCode;
                objects[1] = valuationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AMCSchemewiseMIS;
        }
        public DataSet GetCommissionReconMis(int AdviserId, int schemeid, DateTime FromDate, DateTime Todate, string category, int Issuer)
        {
            Database db;
            DbCommand getCommissionReconMisCmd;
            DataSet dsGetCommissionReconMis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCommissionReconMisCmd = db.GetStoredProcCommand("SPROC_GetCommissionReconMis");
                db.AddInParameter(getCommissionReconMisCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getCommissionReconMisCmd, "@schemeid", DbType.Int32, schemeid);

                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(getCommissionReconMisCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@FromDate", DbType.DateTime, DateTime.MinValue);

                if (Todate != DateTime.MinValue)
                    db.AddInParameter(getCommissionReconMisCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@ToDate", DbType.DateTime, DateTime.MinValue);

                if (!string.IsNullOrEmpty(category))
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, category);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, DBNull.Value);
                if (Issuer != 0)
                    db.AddInParameter(getCommissionReconMisCmd, "@Issuer", DbType.Int32, Issuer);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Issuer", DbType.Int32, 0);

                getCommissionReconMisCmd.CommandTimeout = 60 * 60;
                dsGetCommissionReconMis = db.ExecuteDataSet(getCommissionReconMisCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
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
        public DataSet GetProductDetailFromMFOrder(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId, int isOnline)
        {
            Database db;
            DbCommand GetSchemeTransactionDeatailsCmd;
            DataSet dsSchemeTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeTransactionDeatailsCmd = db.GetStoredProcCommand("SPROC_GetProductDetailFromMFOrder");
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@agentcode", DbType.String, agentcode);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@all", DbType.Int32, all);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (AgentId != 0)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                else
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@IsOnline", DbType.Int32, isOnline);

                GetSchemeTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsSchemeTransactionDeatails = db.ExecuteDataSet(GetSchemeTransactionDeatailsCmd);
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
            return dsSchemeTransactionDeatails;
        }
        /// <summary>
        /// Display Organization wise transaction done from Oder Table
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
        public DataSet GetOrganizationDetailFromMFOrder(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId, int IsOnline)
        {
            Database db;
            DbCommand GetSchemeTransactionDeatailsCmd;
            DataSet dsSchemeTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeTransactionDeatailsCmd = db.GetStoredProcCommand("SPROC_GetOrganizationDetailFromMFOrder");
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@agentcode", DbType.String, agentcode);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@all", DbType.Int32, all);
                db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@IsOnline", DbType.Int32, IsOnline);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (AgentId != 0)
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                else
                    db.AddInParameter(GetSchemeTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                GetSchemeTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsSchemeTransactionDeatails = db.ExecuteDataSet(GetSchemeTransactionDeatailsCmd);
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

        public DataSet GetMemberDetailFromMFOrder(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, int all, DateTime FromDate, DateTime Todate, int AgentId, int IsOnline)
        {
            Database db;
            DbCommand GetMemberTransactionDeatailsCmd;
            DataSet dsMemberTransactionDeatails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetMemberTransactionDeatailsCmd = db.GetStoredProcCommand("SPROC_GetMemberDetailFromMFOrder");
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@agentcode", DbType.String, agentcode);
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@all", DbType.Int32, all);
                db.AddInParameter(GetMemberTransactionDeatailsCmd, "@IsOnline", DbType.Int32, IsOnline);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetMemberTransactionDeatailsCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetMemberTransactionDeatailsCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (AgentId != 0)
                    db.AddInParameter(GetMemberTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                else
                    db.AddInParameter(GetMemberTransactionDeatailsCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                GetMemberTransactionDeatailsCmd.CommandTimeout = 60 * 60;
                dsMemberTransactionDeatails = db.ExecuteDataSet(GetMemberTransactionDeatailsCmd);
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
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsMemberTransactionDeatails;
        }

        public DataSet GetCommissionReceivableRecon_TrailComparision(string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category, string recontype, string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, bool isAuthenticated)
        {
            Database db;
            DbCommand getCommissionReconMisCmd;
            DataSet dsGetCommissionReconMis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCommissionReconMisCmd = db.GetStoredProcCommand("SPROC_GetCommissionReceivableExceptedMIS_TrailComparision");
                db.AddInParameter(getCommissionReconMisCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getCommissionReconMisCmd, "@schemeid", DbType.Int32, schemeid);

                if (month != 0)
                    db.AddInParameter(getCommissionReconMisCmd, "@Month", DbType.Int16, month);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Month", DbType.Int16, DBNull.Value);
                if (year != 0)
                    db.AddInParameter(getCommissionReconMisCmd, "@Year", DbType.Int16, year);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Year", DbType.Int16, DBNull.Value);
                if (!string.IsNullOrEmpty(category))
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, category);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(recontype))
                    db.AddInParameter(getCommissionReconMisCmd, "@recontype", DbType.String, recontype);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@recontype", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(commtype))
                    db.AddInParameter(getCommissionReconMisCmd, "@commissiontype", DbType.String, commtype);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@commissiontype", DbType.String, DBNull.Value);
                db.AddInParameter(getCommissionReconMisCmd, "@issuer", DbType.Int32, issuer);
                db.AddInParameter(getCommissionReconMisCmd, "@productType", DbType.String, product);
                db.AddInParameter(getCommissionReconMisCmd, "@typeOfTransaction", DbType.Int16, typeOfTransaction);
                db.AddInParameter(getCommissionReconMisCmd, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(getCommissionReconMisCmd, "@commissionLookUpId", DbType.Int32, commissionLookUpId);
                db.AddInParameter(getCommissionReconMisCmd, "@orderStatus", DbType.String, orderStatus);
                db.AddInParameter(getCommissionReconMisCmd, "@AgentCode", DbType.String, agentCode);
                db.AddInParameter(getCommissionReconMisCmd, "@ProductCategory", DbType.String, productCategory);
                db.AddInParameter(getCommissionReconMisCmd, "@IsAuthenticated", DbType.Boolean, isAuthenticated);
                getCommissionReconMisCmd.CommandTimeout = 60 * 60;
                dsGetCommissionReconMis = db.ExecuteDataSet(getCommissionReconMisCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

            return dsGetCommissionReconMis;
        }
        public DataSet GetCommissionReceivableRecon(string product, int typeOfTransaction, int AdviserId, int schemeid, int month, int year, string category, string recontype, string commtype, int issuer, int issueId, int commissionLookUpId, string orderStatus, string agentCode, string productCategory, int isAuthenticated)
        {
            Database db;
            DbCommand getCommissionReconMisCmd;
            DataSet dsGetCommissionReconMis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCommissionReconMisCmd = db.GetStoredProcCommand("SPROC_GetCommissionReceivableExceptedMIS");
                db.AddInParameter(getCommissionReconMisCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getCommissionReconMisCmd, "@schemeid", DbType.Int32, schemeid);

                if (month != 0)
                    db.AddInParameter(getCommissionReconMisCmd, "@Month", DbType.Int16, month);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Month", DbType.Int16, DBNull.Value);
                if (year != 0)
                    db.AddInParameter(getCommissionReconMisCmd, "@Year", DbType.Int16, year);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Year", DbType.Int16, DBNull.Value);
                if (!string.IsNullOrEmpty(category))
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, category);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(recontype))
                    db.AddInParameter(getCommissionReconMisCmd, "@recontype", DbType.String, recontype);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@recontype", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(commtype))
                    db.AddInParameter(getCommissionReconMisCmd, "@commissiontype", DbType.String, commtype);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@commissiontype", DbType.String, DBNull.Value);
                db.AddInParameter(getCommissionReconMisCmd, "@issuer", DbType.Int32, issuer);
                db.AddInParameter(getCommissionReconMisCmd, "@productType", DbType.String, product);
                db.AddInParameter(getCommissionReconMisCmd, "@typeOfTransaction", DbType.Int16, typeOfTransaction);
                db.AddInParameter(getCommissionReconMisCmd, "@issueId", DbType.Int32, issueId);
                db.AddInParameter(getCommissionReconMisCmd, "@commissionLookUpId", DbType.Int32, commissionLookUpId);
                db.AddInParameter(getCommissionReconMisCmd, "@orderStatus", DbType.String, orderStatus);
                db.AddInParameter(getCommissionReconMisCmd, "@AgentCode", DbType.String, agentCode);
                db.AddInParameter(getCommissionReconMisCmd, "@ProductCategory", DbType.String, productCategory);
                db.AddInParameter(getCommissionReconMisCmd, "@IsAuthenticated", DbType.Int16, isAuthenticated);
                getCommissionReconMisCmd.CommandTimeout = 60 * 60;
                dsGetCommissionReconMis = db.ExecuteDataSet(getCommissionReconMisCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

            return dsGetCommissionReconMis;
        }
        public bool MarkReconStatus(int transId)
        {
            Database db;
            DataSet ds;
            DbCommand cmdMarkReconStatus;
            bool bResult = false;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdMarkReconStatus = db.GetStoredProcCommand("SPROC_MarkReconStatus");
                db.AddInParameter(cmdMarkReconStatus, "@transId", DbType.Int32, transId);
                db.AddOutParameter(cmdMarkReconStatus, "@count", DbType.Int32, 10);

                ds = db.ExecuteDataSet(cmdMarkReconStatus);
                //count = int.Parse(db.ExecuteScalar(cmdCodeduplicateCheck).ToString());
                Object objCount = db.GetParameterValue(cmdMarkReconStatus, "@count");
                if (objCount != DBNull.Value)
                    count = int.Parse(db.GetParameterValue(cmdMarkReconStatus, "@count").ToString());
                else
                    count = 0;
                if (count > 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                objects[0] = transId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool SaveReceivableReconChanges(int transId, double adjust, double expectedamount)
        {
            Database db;
            DataSet ds;
            DbCommand cmdMarkReconStatus;
            bool bResult = false;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdMarkReconStatus = db.GetStoredProcCommand("SaveReceivableReconChanges");
                db.AddInParameter(cmdMarkReconStatus, "@transId", DbType.Int32, transId);
                db.AddInParameter(cmdMarkReconStatus, "@adjust", DbType.Double, adjust);
                db.AddInParameter(cmdMarkReconStatus, "@expectedamount", DbType.Double, expectedamount);
                db.AddOutParameter(cmdMarkReconStatus, "@count", DbType.Int32, 10);

                ds = db.ExecuteDataSet(cmdMarkReconStatus);
                //count = int.Parse(db.ExecuteScalar(cmdCodeduplicateCheck).ToString());
                Object objCount = db.GetParameterValue(cmdMarkReconStatus, "@count");
                if (objCount != DBNull.Value)
                    count = int.Parse(db.GetParameterValue(cmdMarkReconStatus, "@count").ToString());
                else
                    count = 0;
                if (count > 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                objects[0] = transId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public DataTable GetProductMobilizedReport(int adviserId, int TypeMIS, int mode, Boolean isDemat, int IssueId,string productType,string productCategory, DateTime fromdate, DateTime todate)
        {
            Database db;
            DbCommand getCommissionReconMisCmd;
            DataSet dsGetCommissionReconMis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCommissionReconMisCmd = db.GetStoredProcCommand("SPROC_GetAdviserProductMoblizedMIS");
                db.AddInParameter(getCommissionReconMisCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getCommissionReconMisCmd, "@MobilizationType", DbType.String, TypeMIS);
                db.AddInParameter(getCommissionReconMisCmd, "@mode", DbType.Int32, mode);
                db.AddInParameter(getCommissionReconMisCmd, "@IsDemat", DbType.Boolean, isDemat);
                db.AddInParameter(getCommissionReconMisCmd, "@IssueId", DbType.Int32, IssueId);
                db.AddInParameter(getCommissionReconMisCmd, "@ProductType", DbType.String, productType);
                db.AddInParameter(getCommissionReconMisCmd, "@ProductCategory", DbType.String, productCategory);
                db.AddInParameter(getCommissionReconMisCmd, "@fromDate", DbType.DateTime, fromdate);
                db.AddInParameter(getCommissionReconMisCmd, "@ToDate", DbType.DateTime, todate);
                getCommissionReconMisCmd.CommandTimeout = 60 * 60;
                dsGetCommissionReconMis = db.ExecuteDataSet(getCommissionReconMisCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorMISDao.cs:GetProductMobilizedReport(int adviserId, int TypeMIS, int mode, DateTime fromdate, DataTable todate)");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = TypeMIS;
                objects[2] = mode;
                objects[1] = fromdate;
                objects[2] = todate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetCommissionReconMis.Tables[0];

        }
        public DataSet GetWERPCommissionDetails(string product, int AdviserId, int month, int year, string category, int issueId, string productCategory, int amcCode, int schemeCode, int dateFilterType, int isOnline)
        {
            Database db;
            DbCommand getCommissionReconMisCmd;
            DataSet dsGetCommissionReconMis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCommissionReconMisCmd = db.GetStoredProcCommand("SPROC_GetWERPCommissionDetails");
                db.AddInParameter(getCommissionReconMisCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getCommissionReconMisCmd, "@Product", DbType.String, product);

                if (month != 0)
                    db.AddInParameter(getCommissionReconMisCmd, "@Month", DbType.Int16, month);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Month", DbType.Int16, DBNull.Value);
                if (year != 0)
                    db.AddInParameter(getCommissionReconMisCmd, "@Year", DbType.Int16, year);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Year", DbType.Int16, DBNull.Value);
                if (!string.IsNullOrEmpty(category))
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, category);
                else
                    db.AddInParameter(getCommissionReconMisCmd, "@Category", DbType.String, DBNull.Value);

                db.AddInParameter(getCommissionReconMisCmd, "@issuer", DbType.Int32, amcCode);
                db.AddInParameter(getCommissionReconMisCmd, "@IsOnline", DbType.Int32, isOnline);
                db.AddInParameter(getCommissionReconMisCmd, "@DateFilterType", DbType.Int32, dateFilterType);
                db.AddInParameter(getCommissionReconMisCmd, "@schemeid", DbType.Int32, schemeCode);
                db.AddInParameter(getCommissionReconMisCmd, "@issueId", DbType.Int32, issueId);
                //db.AddInParameter(getCommissionReconMisCmd, "@commissionLookUpId", DbType.Int32, commissionLookUpId);

                db.AddInParameter(getCommissionReconMisCmd, "@ProductCategory", DbType.String, productCategory);
                //db.AddInParameter(getCommissionReconMisCmd, "@IsAuthenticated", DbType.Boolean, isAuthenticated);
                getCommissionReconMisCmd.CommandTimeout = 60 * 60;
                dsGetCommissionReconMis = db.ExecuteDataSet(getCommissionReconMisCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

            return dsGetCommissionReconMis;
        }

        public bool UpdateActualPayAndRec(int id, decimal ActPay, decimal ActRec, DateTime? paybleDate, DateTime? receivedDate, bool IsPayLocked, bool IsRecLocked, bool IsBulkPayble, decimal rtaAmount, bool bulkReceivedSys,bool bulkReceivedRTA,  DateTime? bulkReceivedDate, DateTime? bulkPaybleDate)
        {
            Database db;
            DataSet ds;
            DbCommand cmdUpdateActualPayAndRec;
            bool bResult = false;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdateActualPayAndRec = db.GetStoredProcCommand("SPROC_UpdateActualPayAndRec");
                db.AddInParameter(cmdUpdateActualPayAndRec, "@Id", DbType.Int32, id);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@ActPay", DbType.Decimal, ActPay);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@ActRec", DbType.Decimal, ActRec);
                if(paybleDate!=DateTime.MinValue)
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@paybleDate", DbType.DateTime, paybleDate);
                else
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@paybleDate", DbType.DateTime, DBNull.Value);
                if (receivedDate != DateTime.MinValue)
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@ReceivedDate", DbType.DateTime, receivedDate);
                else
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@ReceivedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@IsPayLocked", DbType.Boolean, IsPayLocked);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@IsRecLocked", DbType.Boolean, IsRecLocked);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@IsBulkPayble", DbType.Boolean, IsBulkPayble);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@RTAAmount", DbType.Decimal, rtaAmount);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@BulkReceivedSys", DbType.Boolean, bulkReceivedSys);
                db.AddInParameter(cmdUpdateActualPayAndRec, "@BulkReceivedRTA", DbType.Boolean, bulkReceivedRTA);
                if (bulkReceivedDate != DateTime.MinValue)
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@BulkReceivedDate", DbType.DateTime, bulkReceivedDate);
                else
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@BulkReceivedDate", DbType.DateTime, DBNull.Value);
                if (bulkPaybleDate != DateTime.MinValue)
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@BulkPaybleDate", DbType.DateTime, bulkPaybleDate);
                else
                    db.AddInParameter(cmdUpdateActualPayAndRec, "@BulkPaybleDate", DbType.DateTime, DBNull.Value);

                count = db.ExecuteNonQuery(cmdUpdateActualPayAndRec);

                if (count>0)
                    bResult=true;
               
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorMISDao.cs:UpdateActualPayAndRec()");
                object[] objects = new object[2];
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
