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

        public DataSet GetMFMIS(string userType, int Id, DateTime dtFrom, DateTime dtTo,int RMId, int branchId, int branchHeadId, int all)
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


        public DataSet GetEQMIS(string userType, int Id, DateTime dtFrom, DateTime dtTo,int rmId, int branchId, int branchHeadId, int all )
        {
            Database db;
            DbCommand getEQMICmd;
            DataSet dsGetEQMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEQMICmd = db.GetStoredProcCommand("SP_GetEQMIS");
                db.AddInParameter(getEQMICmd, "@userType", DbType.String, userType);
                db.AddInParameter(getEQMICmd, "@id", DbType.Int32, Id);
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
                objects[0] = Id;

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
                if(amcCode != 0)
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
        public DataSet GetCustomerAMCSchemewiseMISForRM(int rmid, DateTime valuationDate, int schemeplanid, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CustomerName, string FolioNum, out int Count,int AllPageExportCount)
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
            if(AMCSchemewiseMIS.Tables[1].Rows.Count > 0)
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

        public DataSet GetAMCSchemewiseMISForAdviser(int adviserid,int branchid,int rmid, DateTime valuationDate,int amcCode, int CurrentPage, string AMCSearchVal, string SchemeSearchVal, string CategoryFilterVal, out int Count,int AllPageExportCount)
        {

            Database db;
            DbCommand getLoanMICmd;
            DataSet AMCSchemewiseMIS = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLoanMICmd = db.GetStoredProcCommand("SP_GetAMCSchemewiseMISForAdviser");
                db.AddInParameter(getLoanMICmd, "@A_AdviserId", DbType.Int32, adviserid);
                if(branchid != 0)
                    db.AddInParameter(getLoanMICmd, "@AB_BranchId", DbType.Int32, branchid);
                if(rmid != 0)
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
                if (AllPageExportCount!=0)
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

        public DataSet GetAMCwiseMISForAdviser(int adviserid, int branchid, int rmid, DateTime valuationDate,string AMCSearchVal)
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

                if(schemeplanid != 0)
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

                if(!string.IsNullOrEmpty(CustomerName.Trim()))
                db.AddInParameter(getLoanMICmd, "@CustomerName", DbType.String, CustomerName);
                else
                    db.AddInParameter(getLoanMICmd, "@CustomerName", DbType.String, DBNull.Value);

                if(!string.IsNullOrEmpty(FolioNum.Trim()))
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
        public DataSet GetMFMISCommission(int userId, string misType, DateTime dtFrom, DateTime dtTo,int currentPage, out int count,out double sumToatal)
        {
            Database db;
            DbCommand getMISCommissionCmd;
            DataSet dsGetMISCommission = null;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMISCommissionCmd = db.GetStoredProcCommand("SP_GetMISCommission");
                db.AddInParameter(getMISCommissionCmd, "@MISType", DbType.String, misType);
                db.AddInParameter(getMISCommissionCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(getMISCommissionCmd, "@FromDate", DbType.Date, dtFrom);
                db.AddInParameter(getMISCommissionCmd, "@ToDate", DbType.Date,dtTo) ;
                db.AddInParameter(getMISCommissionCmd, "@currentPage", DbType.Int32, currentPage);
                db.AddOutParameter(getMISCommissionCmd, "@Count", DbType.Int32, 0);
                db.AddOutParameter(getMISCommissionCmd, "@SumTotal", DbType.Double, 0);
                dsGetMISCommission = db.ExecuteDataSet(getMISCommissionCmd);
                count = (int)db.GetParameterValue(getMISCommissionCmd, "@Count");
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
                objects[0] = userId;
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

    }
}
