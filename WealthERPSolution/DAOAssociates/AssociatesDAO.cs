using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VOAssociates;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoUser;
using System.Collections.Specialized;

namespace DAOAssociates
{

    public class AssociatesDAO
    {
        AssociatesVO associatesVo =  new AssociatesVO();
        public List<int> CreateCompleteAssociates(UserVo userVo, AssociatesVO associatesVo, int userId)
        {
            int associateId;
            int UserId;
            int AdviserAgentId;
            List<int> associatesIds = new List<int>();
            Database db;
            DbCommand completeAssociatesCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                completeAssociatesCmd = db.GetStoredProcCommand("SPROC_CreateCompleteAssociates");

                db.AddInParameter(completeAssociatesCmd, "@U_Password", DbType.String, userVo.Password);
                db.AddInParameter(completeAssociatesCmd, "@U_PasswordSaltValue", DbType.String, userVo.PasswordSaltValue);
                db.AddInParameter(completeAssociatesCmd, "@U_FirstName", DbType.String, userVo.FirstName);
                //db.AddInParameter(completeAssociatesCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                //db.AddInParameter(completeAssociatesCmd, "@U_LastName", DbType.String, userVo.LastName);
                db.AddInParameter(completeAssociatesCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(completeAssociatesCmd, "@U_UserType", DbType.String, userVo.UserType);
                db.AddInParameter(completeAssociatesCmd, "@AA_ContactPersonName", DbType.String, associatesVo.ContactPersonName);
                db.AddInParameter(completeAssociatesCmd, "@AR_BranchId", DbType.Int32, associatesVo.BranchId);
                db.AddInParameter(completeAssociatesCmd, "@AR_RMId", DbType.Int32, associatesVo.RMId);
                db.AddInParameter(completeAssociatesCmd, "@UR_UserRoleId", DbType.Int32, associatesVo.UserRoleId);
                db.AddInParameter(completeAssociatesCmd, "@AA_Email", DbType.String, associatesVo.Email);
                db.AddInParameter(completeAssociatesCmd, "@U_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(completeAssociatesCmd, "@U_ModifiedBy", DbType.Int32, userId);
                if(!string.IsNullOrEmpty(associatesVo.PanNo))
                    db.AddInParameter(completeAssociatesCmd, "@AA_PAN", DbType.String, associatesVo.PanNo);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_PAN", DbType.String, DBNull.Value);
                if (associatesVo.Mobile!=0)
                    db.AddInParameter(completeAssociatesCmd, "@AA_Mobile", DbType.Int64, associatesVo.Mobile);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_Mobile", DbType.Int64, DBNull.Value);
                db.AddInParameter(completeAssociatesCmd, "@AA_RequestDate", DbType.DateTime, associatesVo.RequestDate);

                db.AddInParameter(completeAssociatesCmd, "@AAC_AgentCode", DbType.String, associatesVo.AAC_AgentCode);
                db.AddInParameter(completeAssociatesCmd, "@AAC_UserType", DbType.String, associatesVo.AAC_UserType);
                db.AddOutParameter(completeAssociatesCmd, "@AA_AdviserAssociateId", DbType.Int32, 10);
                db.AddOutParameter(completeAssociatesCmd, "@AAC_AdviserAgentId", DbType.Int32, 10);
                db.AddOutParameter(completeAssociatesCmd, "@U_UserId", DbType.Int32, 10);

                if (db.ExecuteNonQuery(completeAssociatesCmd) != 0)
                {

                    UserId = int.Parse(db.GetParameterValue(completeAssociatesCmd, "U_UserId").ToString());
                    associateId = int.Parse(db.GetParameterValue(completeAssociatesCmd, "AA_AdviserAssociateId").ToString());
                    AdviserAgentId = int.Parse(db.GetParameterValue(completeAssociatesCmd, "AAC_AdviserAgentId").ToString());


                    associatesIds.Add(UserId);
                    associatesIds.Add(associateId);
                    associatesIds.Add(AdviserAgentId);
                }
                else
                {
                    associatesIds = null;
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:CreateCompleteRM()");


                object[] objects = new object[3];
                objects[0] = associatesVo;
                objects[1] = userVo;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associatesIds;
        }

        public List<AssociatesVO> GetViewAssociates(int adviserId)
        {
            List<AssociatesVO> associatesVoList = null;
            AssociatesVO associatesVo;
            DataSet ds;
            DataTable dt;
            Database db;
            DbCommand viewAssociatesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                viewAssociatesCmd = db.GetStoredProcCommand("SPROC_GetViewAssociates");
                db.AddInParameter(viewAssociatesCmd, "@AdviserId", DbType.Int32, adviserId);
                ds=db.ExecuteDataSet(viewAssociatesCmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    associatesVoList = new List<AssociatesVO>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        associatesVo = new AssociatesVO();
                        associatesVo.AdviserAssociateId = int.Parse(dr["AA_AdviserAssociateId"].ToString());
                        if (!String.IsNullOrEmpty(dr["AA_RequestDate"].ToString()))
                            associatesVo.RequestDate = DateTime.Parse(dr["AA_RequestDate"].ToString());
                        if (!String.IsNullOrEmpty(dr["AA_StepStatus"].ToString()))
                            associatesVo.CurrentStatus = dr["AA_StepStatus"].ToString();
                        if (!String.IsNullOrEmpty(dr["AA_ContactPersonName"].ToString()))
                            associatesVo.ContactPersonName = dr["AA_ContactPersonName"].ToString();
                        if (!String.IsNullOrEmpty(dr["AAC_AgentCode"].ToString()))
                            associatesVo.AAC_AgentCode = dr["AAC_AgentCode"].ToString();
                        if (!String.IsNullOrEmpty(dr["WWFSM_StepCode"].ToString()))
                            associatesVo.StepCode = dr["WWFSM_StepCode"].ToString();
                        if (!String.IsNullOrEmpty(dr["XS_Status"].ToString()))
                            associatesVo.Status = (dr["XS_Status"].ToString());
                        if (!String.IsNullOrEmpty(dr["AB_BranchName"].ToString()))
                            associatesVo.BranchName = dr["AB_BranchName"].ToString();
                        if (!String.IsNullOrEmpty(dr["RM"].ToString()))
                            associatesVo.RMNAme = dr["RM"].ToString();
                        if (!String.IsNullOrEmpty(dr["WWFSM_StepName"].ToString()))
                            associatesVo.StepName = dr["WWFSM_StepName"].ToString();
                        if (!String.IsNullOrEmpty(dr["AWFSD_Status"].ToString()))
                            associatesVo.StatusCode = dr["AWFSD_Status"].ToString();
                        if (!String.IsNullOrEmpty(dr["AA_Email"].ToString()))
                            associatesVo.Email = dr["AA_Email"].ToString();
                        if (!String.IsNullOrEmpty(dr["AA_Mobile"].ToString()))
                            associatesVo.Mobile = long.Parse(dr["AA_Mobile"].ToString());
                        associatesVoList.Add(associatesVo);
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorStaffBo.cs:GetViewAssociates()");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associatesVoList;
        }

        public DataSet GetAdviserAssociatesDetails(int associateId)
        {
            DataSet dsAdviserAssociatesDetails;
            Database db;
            DbCommand getAdviserAssociatesDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserAssociatesDetailscmd = db.GetStoredProcCommand("SPROC_GetAdviserAssociatesDetails");
                db.AddInParameter(getAdviserAssociatesDetailscmd, "@AA_AdviserAssociateId", DbType.Int32, associateId);
                dsAdviserAssociatesDetails = db.ExecuteDataSet(getAdviserAssociatesDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetAdviserAssociatesDetails()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserAssociatesDetails;
        }

        public bool UpdateStatusStep1(string statusStep1, int associateId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateStatusStep1Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStatusStep1Cmd = db.GetStoredProcCommand("SPROC_UpdateStatusStep1");
                db.AddInParameter(updateStatusStep1Cmd, "@associateId", DbType.Int32, associateId);
                db.AddInParameter(updateStatusStep1Cmd, "@statusStep1", DbType.String, statusStep1);
                db.ExecuteNonQuery(updateStatusStep1Cmd);
                bResult = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }

        public bool UpdateAdviserAssociates(AssociatesVO associatesVo)
        {
            Database db;
            DbCommand UpdateAssociatesCmd;
            bool result;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAssociatesCmd = db.GetStoredProcCommand("SPROC_UpdateAdviserAssociateDetails");
                db.AddInParameter(UpdateAssociatesCmd, "@AA_AdviserAssociateId", DbType.Int32, associatesVo.AdviserAssociateId);
                //db.AddInParameter(UpdateAssociatesCmd, "@AA_AdviserAssociateId", DbType.Int32, associatesVo.AdviserAssociateId);
                db.AddInParameter(UpdateAssociatesCmd, "@U_UserId", DbType.Int32, associatesVo.AdviserAssociateId);
                db.AddInParameter(UpdateAssociatesCmd, "@AR_RMId", DbType.Int32, associatesVo.RMId);
                db.AddInParameter(UpdateAssociatesCmd, "@AB_BranchId", DbType.Int32, associatesVo.BranchId);
                db.AddInParameter(UpdateAssociatesCmd, "@AA_ContactPersonName", DbType.String, associatesVo.ContactPersonName);
                db.AddInParameter(UpdateAssociatesCmd, "@UR_UserRoleId", DbType.Int32, associatesVo.UserRoleId);
                db.AddInParameter(UpdateAssociatesCmd, "@AA_Email", DbType.String, associatesVo.Email);
                if (!string.IsNullOrEmpty(associatesVo.ResPhoneNo.ToString().Trim()))
                     db.AddInParameter(UpdateAssociatesCmd, "@AA_ResPhoneNo", DbType.Int64, associatesVo.ResPhoneNo);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_ResPhoneNo", DbType.Int64, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.ResISDCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_ResISDCode", DbType.Int32, associatesVo.ResISDCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_ResISDCode", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.OfcISDCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_OfcISDCode", DbType.Int32, associatesVo.OfcISDCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_OfcISDCode", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.OfficePhoneNo.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_OfficePhoneNo", DbType.Int64, associatesVo.OfficePhoneNo);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_OfficePhoneNo", DbType.Int64, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.Mobile.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_Mobile", DbType.Int64, associatesVo.Mobile);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_Mobile", DbType.Int64, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.ResFaxStd.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_ISDFax", DbType.Int32, associatesVo.ResFaxStd);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_ISDFax", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.ResFaxNumber.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_Fax", DbType.Int32, associatesVo.ResFaxNumber);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_Fax", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.CorrAdrLine1.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrLine1", DbType.String, associatesVo.CorrAdrLine1);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrLine1", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.CorrAdrLine2.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrLine2", DbType.String, associatesVo.CorrAdrLine2);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrLine2", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.CorrAdrLine3.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrLine3", DbType.String, associatesVo.CorrAdrLine3);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrLine3", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.CorrAdrPinCode.ToString().Trim()))
                     db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrPinCode", DbType.Int32, associatesVo.CorrAdrPinCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrPinCode", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.CorrAdrCity.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrCity", DbType.String, associatesVo.CorrAdrCity);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrCity", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.CorrAdrState.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrState", DbType.String, associatesVo.CorrAdrState);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrState", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.CorrAdrCountry.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrCountry", DbType.String, associatesVo.CorrAdrCountry);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_CorrAdrCountry", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.PerAdrLine1.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrLine1", DbType.String, associatesVo.PerAdrLine1);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrLine1", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.PerAdrLine2.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrLine2", DbType.String, associatesVo.PerAdrLine2);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrLine2", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.PerAdrLine3.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrLine3", DbType.String, associatesVo.PerAdrLine3);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrLine3", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.PerAdrPinCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrPinCode", DbType.Int32, associatesVo.PerAdrPinCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrPinCode", DbType.Int32, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.PerAdrCity.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrCity", DbType.String, associatesVo.PerAdrCity);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrCity", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.PerAdrState.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrState", DbType.String, associatesVo.PerAdrState);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrState", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.PerAdrCountry.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrCountry", DbType.String, associatesVo.PerAdrCountry);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_PerAdrCountry", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.MaritalStatusCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@XMS_MaritalStatusCode", DbType.String, associatesVo.MaritalStatusCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@XMS_MaritalStatusCode", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.Gender.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_Gender", DbType.String, associatesVo.Gender);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_Gender", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.QualificationCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@XQ_QualificationCode", DbType.String, associatesVo.QualificationCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@XQ_QualificationCode", DbType.String, DBNull.Value);

                //if (associatesVo.DOB != DateTime.MinValue)
                //    db.AddInParameter(UpdateAssociatesCmd, "@AA_DOB", DbType.DateTime, associatesVo.DOB);
                //else
                //    db.AddInParameter(UpdateAssociatesCmd, "@AA_DOB", DbType.DateTime, DBNull.Value);
                if (associatesVo.DOB != DateTime.MinValue)
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_DOB", DbType.DateTime, associatesVo.DOB);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_DOB", DbType.DateTime, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.BankCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@WERPBM_BankCode", DbType.String, associatesVo.BankCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@WERPBM_BankCode", DbType.String, DBNull.Value);


                if (!string.IsNullOrEmpty(associatesVo.BankAccountTypeCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@XBAT_BankAccountTypeCode", DbType.String, associatesVo.BankAccountTypeCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@XBAT_BankAccountTypeCode", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.BranchName.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchName", DbType.String, associatesVo.BranchName);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchName", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.AccountNum.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_AccountNum", DbType.String, associatesVo.AccountNum);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_AccountNum", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.MaritalStatusCode.ToString().Trim()))
                db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrLine1", DbType.String, associatesVo.BranchAdrLine1);

                if (!string.IsNullOrEmpty(associatesVo.BranchAdrLine1.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "AA_BranchAdrLine2", DbType.String, associatesVo.BranchAdrLine2);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "AA_BranchAdrLine2", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.BranchAdrLine3.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrLine3", DbType.String, associatesVo.BranchAdrLine3);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrLine3", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.MaritalStatusCode.ToString().Trim()))
                db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrCity", DbType.String, associatesVo.BranchAdrCity);

                if (!string.IsNullOrEmpty(associatesVo.BranchAdrState.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrState", DbType.String, associatesVo.BranchAdrState);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrState", DbType.String, DBNull.Value);

                //if (!string.IsNullOrEmpty(associatesVo.BranchAdrCountry.ToString().Trim()))
                //    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrCountry", DbType.String, associatesVo.BranchAdrCountry);
                //else
                //    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrCountry", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.MICR.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@CB_MICR", DbType.Int32, associatesVo.MICR);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@CB_MICR", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.IFSC.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@CB_IFSC", DbType.String, associatesVo.IFSC);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@CB_IFSC", DbType.String, DBNull.Value);
                db.AddInParameter(UpdateAssociatesCmd, "@AA_CreatedBy", DbType.Int32, associatesVo.CreatedBy);
                db.AddInParameter(UpdateAssociatesCmd, "@AA_ModifiedBy", DbType.Int32, associatesVo.ModifiedBy);

                if (!string.IsNullOrEmpty(associatesVo.NomineeName.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NomineeName", DbType.String, associatesVo.NomineeName);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NomineeName", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.NomineeAddres.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NomineeAddress", DbType.String, associatesVo.NomineeAddres);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NomineeAddress", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.NomineeTelNo.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NomineeTelNo", DbType.Int64, associatesVo.NomineeTelNo);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NomineeTelNo", DbType.Int64, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.RelationshipCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@XR_RelationshipCode", DbType.String, associatesVo.RelationshipCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@XR_RelationshipCode", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.GuardianName.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianName", DbType.String, associatesVo.GuardianName);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianName", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.GuardianAddress.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianAddress", DbType.String, associatesVo.GuardianAddress);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianAddress", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.GuardianTelNo.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianTelNo", DbType.Int64, associatesVo.GuardianTelNo);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianTelNo", DbType.Int64, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.GuardianTelNo.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianRelationship", DbType.String, associatesVo.GuardianTelNo);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_GuardianRelationship", DbType.String, DBNull.Value);

                //if (associatesVo.ExpiryDate!= DateTime.MinValue)
                //    db.AddInParameter(UpdateAssociatesCmd, "@AAAR_ExpiryDate", DbType.DateTime,associatesVo.ExpiryDate );
                //else
                //    db.AddInParameter(UpdateAssociatesCmd, "@AAAR_ExpiryDate", DbType.DateTime, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.Registrationumber.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AAAR_Registrationumber", DbType.String, associatesVo.Registrationumber);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AAAR_Registrationumber", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.assetGroupCode.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@PAG_assetGroupCode", DbType.String, associatesVo.assetGroupCode);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@PAG_assetGroupCode", DbType.String, DBNull.Value);
                if (associatesVo.StartDate != DateTime.MinValue)
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_StartDate", DbType.DateTime, associatesVo.StartDate);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_StartDate", DbType.DateTime, DBNull.Value);
                if (associatesVo.EndDate != DateTime.MinValue)
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_EndDate", DbType.DateTime, associatesVo.EndDate);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_EndDate", DbType.DateTime, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.AMFIregistrationNo.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_AMFIregistrationNo", DbType.String, associatesVo.AMFIregistrationNo);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_AMFIregistrationNo", DbType.String, DBNull.Value);
                if (associatesVo.NoOfBranches!=0)
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfBranches", DbType.Int16, associatesVo.NoOfBranches);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfBranches", DbType.Int16, DBNull.Value);
                if (associatesVo.NoOfSalesEmployees != 0)
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfSalesEmployees", DbType.Int16, associatesVo.NoOfSalesEmployees);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfSalesEmployees", DbType.Int16, DBNull.Value);
                if (associatesVo.NoOfSubBrokers != 0)
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfSubBrokers", DbType.Int16, associatesVo.NoOfSubBrokers);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfSubBrokers", DbType.Int16, DBNull.Value);
                if (associatesVo.NoOfClients != 0)
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfClients", DbType.Int16, associatesVo.NoOfClients);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_NoOfClients", DbType.Int16, DBNull.Value);


                db.ExecuteNonQuery(UpdateAssociatesCmd);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return result;
        }

        public int GetAgentId()
        {
            DataSet dsAgentId;
            DataTable dtAgentId;
            int agentId = 0;
            Database db;
            DbCommand getAgentcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAgentcmd = db.GetStoredProcCommand("SPROC_GetAgentId");
                dsAgentId = db.ExecuteDataSet(getAgentcmd);
                dtAgentId = dsAgentId.Tables[0];
                if (dtAgentId.Rows.Count > 0)
                    agentId = int.Parse(dtAgentId.Rows[0]["AAC_AdviserAgentId"].ToString());
                else
                    agentId = 999;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetAgentId()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return agentId;
        }

        public DataTable GetAssociatesList(int adviserId)
        {
            DataSet dsAssociatesList;
            DataTable dtAssociatesList;
            Database db;
            DbCommand getAssociatesListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssociatesListcmd = db.GetStoredProcCommand("SPROC_GetAssociatesList");
                db.AddInParameter(getAssociatesListcmd, "@adviserId", DbType.Int32, adviserId);
                dsAssociatesList = db.ExecuteDataSet(getAssociatesListcmd);
                dtAssociatesList = dsAssociatesList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetAssociatesList()");
                object[] objects = new object[1];
                objects[0]=adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssociatesList;
        }

        public bool CreateAdviserAgentCode(AssociatesVO associatesVo, int agentId)
        {
            bool result=false;
            Database db;
            DbCommand createAdviserAgentCodecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdviserAgentCodecmd = db.GetStoredProcCommand("SPROC_CreateAdviserAgentCodeDetails");
                db.AddInParameter(createAdviserAgentCodecmd, "@agentId", DbType.Int32, agentId);
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_AgentCode", DbType.String, associatesVo.AAC_AgentCode);
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_UserType", DbType.String, associatesVo.AAC_UserType);
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_CreatedBy", DbType.Int32, associatesVo.AAC_CreatedBy);
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_ModifiedBy", DbType.Int32, associatesVo.AAC_ModifiedBy);
                db.AddInParameter(createAdviserAgentCodecmd, "@BranchId", DbType.Int32, associatesVo.BranchId);
                db.AddInParameter(createAdviserAgentCodecmd, "@RmId", DbType.Int32, associatesVo.RMId);
                db.AddInParameter(createAdviserAgentCodecmd, "@AssociatesId", DbType.Int32, associatesVo.AdviserAssociateId);
                db.ExecuteNonQuery(createAdviserAgentCodecmd);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return result;

        }

        public DataSet GetAgentCodeAndType(int adviserId)
        {
            DataSet dsAgentCodeAndTypeList;
            Database db;
            DbCommand getAgentCodeAndTypecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAgentCodeAndTypecmd = db.GetStoredProcCommand("SPROC_GetAgentCodeAndType");
                db.AddInParameter(getAgentCodeAndTypecmd, "@adviserId", DbType.Int32, adviserId);
                dsAgentCodeAndTypeList = db.ExecuteDataSet(getAgentCodeAndTypecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetAssociatesList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAgentCodeAndTypeList;
        }

        public void UpdateAssociatesWorkFlowStatusDetails(int AssociateId, string Status, string StepCode, string StatusReason,string comments)
        {
            Database db;
            DbCommand updateWorkFlowcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateWorkFlowcmd = db.GetStoredProcCommand("UpdateAssociatesWorkFlowStatusDetails");
                db.AddInParameter(updateWorkFlowcmd, "@AA_AdviserAssociateId", DbType.Int32, AssociateId);
                db.AddInParameter(updateWorkFlowcmd, "@AWFSD_Status", DbType.String, Status);
                db.AddInParameter(updateWorkFlowcmd, "@AWFSD_StatusReason", DbType.String, StatusReason);
                db.AddInParameter(updateWorkFlowcmd, "@WWFSM_StepCode", DbType.String, StepCode);
                db.AddInParameter(updateWorkFlowcmd, "@AWFSD_Notes", DbType.String, comments);
                db.ExecuteNonQuery(updateWorkFlowcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
        }

        public DataTable GetProductAssetGroup()
        {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getProductAssetGroupcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getProductAssetGroupcmd = db.GetStoredProcCommand("SP_GetProductAssetGroup");
                ds = db.ExecuteDataSet(getProductAssetGroupcmd);
                dt = ds.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public DataSet GetAssociatesStepDetails(int requestId)
        {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getcmd = db.GetStoredProcCommand("SPROC_GetAssociatesStepDetails");
                db.AddInParameter(getcmd, "@RequestId", DbType.Int32, requestId);
                ds = db.ExecuteDataSet(getcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        public DataSet AssociateUserMangemnetList(int adviserId)
        {
            DataTable dt;
            Database db;
            DataSet ds;
            DbCommand getcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getcmd = db.GetStoredProcCommand("SPROC_GetAssociateUserMangemnetList");
                db.AddInParameter(getcmd, "@adviserId", DbType.Int32, adviserId);
                ds = db.ExecuteDataSet(getcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return ds;
        }

        public DataSet BindAssociateCodeList(int adviserId)
        {
            Database db;
            DataSet dsAssociateCodeList;
            DbCommand AssociateCodeListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AssociateCodeListcmd = db.GetStoredProcCommand("SPROC_GetBindAssociateCodeList");
                db.AddInParameter(AssociateCodeListcmd, "@adviserId", DbType.Int32, adviserId);
                dsAssociateCodeList = db.ExecuteDataSet(AssociateCodeListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAssociateCodeList;
        }

        public AssociatesVO GetAssociateUser(int UserId)
        {
            AssociatesVO associatesVo = new AssociatesVO();
            Database db;
            DbCommand getAssociateUserCmd;
            DataSet getAssociateUserDs;
            // DataTable table;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssociateUserCmd = db.GetStoredProcCommand("SPROC_GetAssociateUser");
                db.AddInParameter(getAssociateUserCmd, "@U_UserId", DbType.Int32, UserId);
                getAssociateUserDs = db.ExecuteDataSet(getAssociateUserCmd);
                if (getAssociateUserDs.Tables[0].Rows.Count > 0)
                {
                    // table = getAdvisorStaffDs.Tables["AdviserRM"];
                    dr = getAssociateUserDs.Tables[0].Rows[0];
                    associatesVo.AdviserAssociateId = int.Parse((dr["AA_AdviserAssociateId"].ToString()));
                    associatesVo.AAC_AdviserAgentId = int.Parse((dr["AAC_AdviserAgentId"].ToString()));
                    associatesVo.UserId = int.Parse((dr["U_UserId"].ToString()));
                    associatesVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                    associatesVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    associatesVo.ContactPersonName = dr["AA_ContactPersonName"].ToString();
                    if (dr["AA_Email"] != DBNull.Value)
                        associatesVo.Email = dr["AA_Email"].ToString();
                    else
                        associatesVo.Email = string.Empty;
                    if (dr["AA_CorrAdrLine1"] != DBNull.Value)
                        associatesVo.CorrAdrLine1 = dr["AA_CorrAdrLine1"].ToString();
                    else
                        associatesVo.CorrAdrLine1 = string.Empty;
                    if (dr["AA_CorrAdrLine2"] != DBNull.Value)
                        associatesVo.CorrAdrLine2= dr["AA_CorrAdrLine2"].ToString();
                    else
                        associatesVo.CorrAdrLine2 = string.Empty;
                    if (dr["AA_CorrAdrLine3"] != DBNull.Value)
                        associatesVo.CorrAdrLine3 = dr["AA_CorrAdrLine3"].ToString();
                    else
                        associatesVo.CorrAdrLine3 = string.Empty;
                    if (dr["AA_CorrAdrPinCode"] != DBNull.Value)
                        associatesVo.CorrAdrPinCode =int.Parse(dr["AA_CorrAdrPinCode"].ToString());
                    else
                        associatesVo.CorrAdrPinCode = 0;
                    if (dr["AA_CorrAdrCity"] != DBNull.Value)
                        associatesVo.CorrAdrCity= dr["AA_CorrAdrCity"].ToString();
                    else
                        associatesVo.CorrAdrCity = string.Empty;
                    if (dr["AA_CorrAdrState"] != DBNull.Value)
                        associatesVo.CorrAdrState = dr["AA_CorrAdrState"].ToString();
                    else
                        associatesVo.CorrAdrState = string.Empty;
                    if (dr["AA_CorrAdrCountry"] != DBNull.Value)
                        associatesVo.CorrAdrCountry = dr["AA_CorrAdrCountry"].ToString();
                    else
                        associatesVo.CorrAdrCountry = string.Empty;
                    if (dr["AA_PerAdrLine1"] != DBNull.Value)
                        associatesVo.PerAdrLine1 = dr["AA_PerAdrLine1"].ToString();
                    else
                        associatesVo.PerAdrLine1 = string.Empty;
                    if (dr["AA_PerAdrLine2"] != DBNull.Value)
                        associatesVo.PerAdrLine2 = dr["AA_PerAdrLine2"].ToString();
                    else
                        associatesVo.PerAdrLine2 = string.Empty;
                    if (dr["AA_PerAdrLine3"] != DBNull.Value)
                        associatesVo.PerAdrLine3= dr["AA_PerAdrLine3"].ToString();
                    else
                        associatesVo.PerAdrLine3 = string.Empty;
                    if (dr["AA_PerAdrPinCode"] != DBNull.Value)
                        associatesVo.PerAdrPinCode = int.Parse(dr["AA_PerAdrPinCode"].ToString());
                    else
                        associatesVo.PerAdrPinCode = 0;
                    if (dr["AA_PerAdrCity"] != DBNull.Value)
                        associatesVo.PerAdrCity = dr["AA_PerAdrCity"].ToString();
                    else
                        associatesVo.PerAdrCity = string.Empty;
                    if (dr["AA_PerAdrState"] != DBNull.Value)
                        associatesVo.PerAdrState = dr["AA_PerAdrState"].ToString();
                    else
                        associatesVo.PerAdrState = string.Empty;
                    if (dr["AA_PerAdrCountry"] != DBNull.Value)
                        associatesVo.PerAdrCountry = dr["AA_PerAdrCountry"].ToString();
                    else
                        associatesVo.PerAdrCountry = string.Empty;
                    if (dr["AA_OfficePhoneNo"] != DBNull.Value)
                        associatesVo.OfficePhoneNo = int.Parse(dr["AA_OfficePhoneNo"].ToString());

                    if (dr["AA_Fax"] != DBNull.Value)
                        associatesVo.ResFaxNumber = int.Parse(dr["AA_Fax"].ToString());
                    if (dr["XMS_MaritalStatusCode"] != DBNull.Value)
                        associatesVo.MaritalStatusCode = dr["XMS_MaritalStatusCode"].ToString();
                    if (dr["AA_Gender"] != DBNull.Value)
                        associatesVo.Gender = dr["AA_Gender"].ToString();
                    if (dr["XQ_QualificationCode"] != DBNull.Value)
                        associatesVo.QualificationCode =dr["XQ_QualificationCode"].ToString();
                    if (dr["AA_ResISDCode"] != DBNull.Value)
                        associatesVo.ResISDCode = int.Parse(dr["AA_ResISDCode"].ToString());
                    if (dr["AA_OfcISDCode"] != DBNull.Value)
                        associatesVo.ResSTDCode = int.Parse(dr["AA_OfcISDCode"].ToString());
                    if (dr["AA_ResPhoneNo"] != DBNull.Value)
                        associatesVo.ResPhoneNo = int.Parse(dr["AA_ResPhoneNo"].ToString());
                    if (dr["AA_DOB"] != DBNull.Value)
                        associatesVo.DOB = DateTime.Parse(dr["AA_DOB"].ToString());
                    //if (!string.IsNullOrEmpty(dr["WERPBM_BankCode]"].ToString()))
                    //    associatesVo.BankCode =dr["WERPBM_BankCode]"].ToString();
                    if (dr["XBAT_BankAccountTypeCode"] != DBNull.Value)
                        associatesVo.BankAccountTypeCode = dr["XBAT_BankAccountTypeCode"].ToString();
                    if (dr["AA_Mobile"] != DBNull.Value)
                        associatesVo.Mobile = Convert.ToInt64(dr["AA_Mobile"].ToString());
                    if (dr["AA_BranchName"] != DBNull.Value)
                        associatesVo.BranchName = dr["AA_BranchName"].ToString();
                    if (dr["UR_UserRoleId"] != DBNull.Value)
                        associatesVo.UserRoleId = int.Parse(dr["UR_UserRoleId"].ToString());
                    if (!string.IsNullOrEmpty(dr["AA_AccountNum"].ToString().Trim()))
                        associatesVo.AccountNum = dr["AA_AccountNum"].ToString();
                    if (dr["AA_BranchAdrLine1"] != DBNull.Value)
                        associatesVo.CorrAdrLine1 = dr["AA_BranchAdrLine1"].ToString();
                    else
                        associatesVo.PerAdrLine1 = string.Empty;
                    if (dr["AA_BranchAdrLine2"] != DBNull.Value)
                        associatesVo.BranchAdrLine2 = dr["AA_BranchAdrLine2"].ToString();
                    else
                        associatesVo.BranchAdrLine2 = string.Empty;
                    if (dr["AA_BranchAdrLine3"] != DBNull.Value)
                        associatesVo.BranchAdrLine3 = dr["AA_BranchAdrLine3"].ToString();
                    else
                        associatesVo.BranchAdrLine3 = string.Empty;
                    if (dr["AA_BranchAdrCity"] != DBNull.Value)
                        associatesVo.BranchAdrCity = dr["AA_BranchAdrCity"].ToString();
                    else
                        associatesVo.BranchAdrCity = string.Empty;
                    if (dr["AA_BranchAdrState"] != DBNull.Value)
                        associatesVo.BranchAdrState = dr["AA_BranchAdrState"].ToString();
                    else
                        associatesVo.BranchAdrState = string.Empty;
                    if (dr["AA_BranchAdrCountry"] != DBNull.Value)
                        associatesVo.BranchAdrCountry = dr["AA_BranchAdrCountry"].ToString();
                    else
                        associatesVo.BranchAdrCountry = string.Empty;
                    if (dr["CB_MICR"] != DBNull.Value && dr["CB_MICR"].ToString() != "")
                        associatesVo.MICR = Int16.Parse(dr["CB_MICR"].ToString());
                    else
                        associatesVo.MICR = 0;

                    if (dr["CB_IFSC"] != DBNull.Value)
                        associatesVo.IFSC = dr["CB_IFSC"].ToString();
                    else
                        associatesVo.IFSC = string.Empty;

                    if (dr["AA_StepStatus"] != DBNull.Value)
                        associatesVo.StepCode = dr["AA_StepStatus"].ToString();
                    if (dr["XISAS_Code"] != DBNull.Value)
                        associatesVo.StatusCode = dr["XISAS_Code"].ToString();
                    if (dr["AA_PAN"] != DBNull.Value)
                        associatesVo.PanNo = dr["AA_PAN"].ToString();
                    if (dr["AA_RequestDate"] != DBNull.Value)
                        associatesVo.RequestDate =DateTime.Parse( dr["AA_RequestDate"].ToString());
                    if (dr["AA_NomineeName"] != DBNull.Value)
                        associatesVo.NomineeName = dr["AA_NomineeName"].ToString();
                    if (dr["XR_RelationshipCode"] != DBNull.Value)
                        associatesVo.RelationshipCode = dr["XR_RelationshipCode"].ToString();
                    if (dr["AA_NomineeAddress"] != DBNull.Value)
                        associatesVo.NomineeAddres = dr["AA_NomineeAddress"].ToString();
                    if (dr["AA_NomineeTelNo"] != DBNull.Value)
                        associatesVo.NomineeTelNo = int.Parse(dr["AA_NomineeTelNo"].ToString());
                    if (dr["AA_GuardianName"] != DBNull.Value)
                        associatesVo.GuardianName = dr["AA_GuardianName"].ToString();
                    if (dr["AA_GuardianRelationship"] != DBNull.Value)
                        associatesVo.GuardianRelationship = dr["AA_GuardianRelationship"].ToString();
                    if (dr["AA_GuardianAddress"] != DBNull.Value)
                        associatesVo.GuardianAddress = dr["AA_GuardianAddress"].ToString();
                    if (dr["AA_GuardianTelNo"] != DBNull.Value)
                        associatesVo.GuardianTelNo = int.Parse(dr["AA_GuardianTelNo"].ToString());
                    if (dr["AA_AMFIregistrationNo"] != DBNull.Value)
                        associatesVo.AMFIregistrationNo = dr["AA_AMFIregistrationNo"].ToString();
                    if (dr["AA_StartDate"] != DBNull.Value)
                        associatesVo.StartDate = DateTime.Parse(dr["AA_StartDate"].ToString());
                    if (dr["AA_EndDate"] != DBNull.Value)
                        associatesVo.EndDate = DateTime.Parse(dr["AA_EndDate"].ToString());
                    if (dr["AA_NoOfBranches"] != DBNull.Value)
                        associatesVo.NoOfBranches = int.Parse(dr["AA_NoOfBranches"].ToString());
                    if (dr["AA_NoOfSalesEmployees"] != DBNull.Value)
                        associatesVo.NoOfSalesEmployees = int.Parse(dr["AA_NoOfSalesEmployees"].ToString());
                    if (dr["AA_NoOfSubBrokers"] != DBNull.Value)
                        associatesVo.NoOfSubBrokers = int.Parse(dr["AA_NoOfSubBrokers"].ToString());
                    if (dr["AA_NoOfClients"] != DBNull.Value)
                        associatesVo.NoOfClients = int.Parse(dr["AA_NoOfClients"].ToString());
                    if (dr["AAC_AgentCode"] != DBNull.Value)
                        associatesVo.AAC_AgentCode= dr["AAC_AgentCode"].ToString();
                    if (dr["AAC_UserType"] != DBNull.Value)
                        associatesVo.AAC_UserType = dr["AAC_UserType"].ToString();

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdvisorStaff()");

                object[] objects = new object[1];
                objects[0] = UserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associatesVo;
        }
    }
}
