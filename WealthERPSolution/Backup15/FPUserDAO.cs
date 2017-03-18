using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using VOFPUtilityUser;

namespace DaoFPUtility
{
    public class FPUserDAO
    {
        public FPUserVo CreateAndGetFPUtilityUserDetails(FPUserVo userVo, string clientCode, bool userType)
        {
            FPUserVo fpUserVo = new FPUserVo();
            DataSet ds;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("CreateFPUtilityUser");
                db.AddInParameter(dbCommand, "@Name", DbType.String, userVo.UserName);
                db.AddInParameter(dbCommand, "@EMail", DbType.String, userVo.EMail);
                db.AddInParameter(dbCommand, "@PAN", DbType.String, userVo.Pan);
                db.AddInParameter(dbCommand, "@MobileNo", DbType.Int64, userVo.MobileNo);
                db.AddInParameter(dbCommand, "@ClientCode", DbType.String, clientCode);
                db.AddInParameter(dbCommand, "@UserType", DbType.Boolean, userType);
                if (userVo.DOB == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "@DOB", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "@DOB", DbType.DateTime, userVo.DOB);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    fpUserVo.EMail = ds.Tables[0].Rows[0]["FPUUD_EMail"].ToString();
                    fpUserVo.UserName = ds.Tables[0].Rows[0]["FPUUD_Name"].ToString();
                    fpUserVo.Pan = ds.Tables[0].Rows[0]["FPUUD_PAN"].ToString();
                    fpUserVo.MobileNo = Convert.ToInt64(ds.Tables[0].Rows[0]["FPUUD_MobileNo"].ToString());
                    fpUserVo.C_CustomerId = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["C_CustomerId"].ToString()) ? Convert.ToInt32(ds.Tables[0].Rows[0]["C_CustomerId"].ToString()) : 0;
                    fpUserVo.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["FPUUD_UserId"].ToString());
                    fpUserVo.CreatedOn = Convert.ToDateTime(ds.Tables[0].Rows[0]["FPUUD_CreatedOn"].ToString());
                    fpUserVo.ModifiedOn = Convert.ToDateTime(ds.Tables[0].Rows[0]["FPUUD_ModifiedOn"].ToString());
                    fpUserVo.RiskClassCode = ds.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();
                    fpUserVo.IsProspectmarked = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["FPUUD_IsProspectmarked"].ToString()) ? Convert.ToBoolean(ds.Tables[0].Rows[0]["FPUUD_IsProspectmarked"]) : false;
                    fpUserVo.DOB = Convert.ToDateTime(ds.Tables[0].Rows[0]["FPUUD_DOB"].ToString());
                    bool i;
                    fpUserVo.IsClientExists = bool.TryParse(ds.Tables[0].Rows[0]["FPUUD_IsClientExists"].ToString(), out i) ? (bool?)i : null;
                }
            }

            catch (Exception Ex)
            {

            }
            return fpUserVo;
        }
        public bool SetAnswerToQuestion(int userId, int questionId, int answerId)
        {
            bool result = false;
            DataSet ds;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_FPUtility_SetAnswerToQuestion");
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, userId);
                db.AddInParameter(dbCommand, "@QuestionId", DbType.Int32, questionId);
                db.AddInParameter(dbCommand, "@AnswerId", DbType.Int32, answerId);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public DataSet GetQuestionAndOptions(int userId)
        {
            DataSet ds = new DataSet();
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_FPUtility_GetQuestionAndOptions");
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, userId);
                ds = db.ExecuteDataSet(dbCommand);
            }

            catch (Exception Ex)
            {

            }
            return ds;
        }
        public DataSet GetRiskClass(int userId, int adviserId)
        {
            DataSet ds = new DataSet();
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_FPUtility_GetRiskClass");
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, userId);
                db.AddInParameter(dbCommand, "@AdviserId", DbType.Int32, adviserId);
                ds = db.ExecuteDataSet(dbCommand);
            }

            catch (Exception Ex)
            {

            }
            return ds;
        }
        //Getting List of Question
        public DataSet GetRiskProfileQuestion(int advisorId)
        {
            Database db;
            DbCommand dbGetCustomerList = null;
            DataSet dsGetCustomerList = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbGetCustomerList = db.GetStoredProcCommand("SP_GetRiskProfileQuestion");
                db.AddInParameter(dbGetCustomerList, "@A_AdviserId", DbType.Int32, advisorId);
                dsGetCustomerList = db.ExecuteDataSet(dbGetCustomerList);
            }

            catch (Exception ex)
            {

            }
            return dsGetCustomerList;
        }
        //Getting Option for Each Question
        public DataSet GetQuestionOption(int questionId, int advisorId)
        {
            Database db;
            DataSet dsGetQuestionOption = null;
            DbCommand dbGetQuestionOption;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbGetQuestionOption = db.GetStoredProcCommand("SP_GetQuestionOption");
                db.AddInParameter(dbGetQuestionOption, "@QuestionId", DbType.Int32, questionId);
                db.AddInParameter(dbGetQuestionOption, "@advisorId", DbType.Int32, advisorId);
                dsGetQuestionOption = db.ExecuteDataSet(dbGetQuestionOption);

            }

            catch (Exception ex)
            {

            }
            return dsGetQuestionOption;
        }
        public bool CheckInvestorExists(int adviserId, string panNo, string clientCode)
        {
            bool result = false;
            DataSet ds;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_FPUtility_CheckInvestorExists");
                db.AddInParameter(dbCommand, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(dbCommand, "@PanNo", DbType.String, panNo);
                db.AddInParameter(dbCommand, "@ClientCode", DbType.String, clientCode);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public bool UpdateCustomerProspect(int customerId, int fpUserId)
        {
            bool result = false;
            DataSet ds;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_FPUtility_UpdateCustomerProspect");
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(dbCommand, "@fpUserId", DbType.Int32, fpUserId);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public FPUserVo GetFPUser(int fpUserId)
        {
            FPUserVo fpUserVo = new FPUserVo();
            DataSet ds;
            Database db;
            DbCommand dbCommand;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SPROC_FPUtility_GETFPUtilityUser");
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, fpUserId);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    fpUserVo.EMail = ds.Tables[0].Rows[0]["FPUUD_EMail"].ToString();
                    fpUserVo.UserName = ds.Tables[0].Rows[0]["FPUUD_Name"].ToString();
                    fpUserVo.Pan = ds.Tables[0].Rows[0]["FPUUD_PAN"].ToString();
                    fpUserVo.MobileNo = Convert.ToInt64(ds.Tables[0].Rows[0]["FPUUD_MobileNo"].ToString());
                    fpUserVo.C_CustomerId = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["C_CustomerId"].ToString()) ? Convert.ToInt32(ds.Tables[0].Rows[0]["C_CustomerId"].ToString()) : 0;
                    fpUserVo.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["FPUUD_UserId"].ToString());
                    fpUserVo.CreatedOn = Convert.ToDateTime(ds.Tables[0].Rows[0]["FPUUD_CreatedOn"].ToString());
                    fpUserVo.ModifiedOn = Convert.ToDateTime(ds.Tables[0].Rows[0]["FPUUD_ModifiedOn"].ToString());
                    fpUserVo.RiskClassCode = ds.Tables[0].Rows[0]["XRC_RiskClassCode"].ToString();
                    fpUserVo.IsProspectmarked = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["FPUUD_IsProspectmarked"].ToString()) ? Convert.ToBoolean(ds.Tables[0].Rows[0]["FPUUD_IsProspectmarked"]) : false;
                    bool i;
                    fpUserVo.IsClientExists = bool.TryParse(ds.Tables[0].Rows[0]["FPUUD_IsClientExists"].ToString(), out i) ? (bool?)i : null;
                }
            }

            catch (Exception Ex)
            {

            }
            return fpUserVo;
        }
    }
}
