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
        AssociatesVO associatesVo = new AssociatesVO();
        public List<int> CreateCompleteAssociates(UserVo userVo, AssociatesVO associatesVo, int userId)
        {
            int associateId;
            int UserId;
            int AdviserAgentId = 0;
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
                db.AddInParameter(completeAssociatesCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                db.AddInParameter(completeAssociatesCmd, "@U_LastName", DbType.String, userVo.LastName);
                db.AddInParameter(completeAssociatesCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(completeAssociatesCmd, "@U_UserType", DbType.String, userVo.UserType);
                db.AddInParameter(completeAssociatesCmd, "@U_LoginId", DbType.String, userVo.LoginId);

                db.AddInParameter(completeAssociatesCmd, "@AA_ContactPersonName", DbType.String, associatesVo.ContactPersonName);
                db.AddInParameter(completeAssociatesCmd, "@AR_BranchId", DbType.Int32, associatesVo.BranchId);
                db.AddInParameter(completeAssociatesCmd, "@AR_RMId", DbType.Int32, associatesVo.RMId);
                db.AddInParameter(completeAssociatesCmd, "@UR_UserRoleId", DbType.Int32, associatesVo.UserRoleId);
                db.AddInParameter(completeAssociatesCmd, "@AA_Email", DbType.String, associatesVo.Email);
                db.AddInParameter(completeAssociatesCmd, "@U_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(completeAssociatesCmd, "@U_ModifiedBy", DbType.Int32, userId);
                if (!string.IsNullOrEmpty(associatesVo.PanNo))
                    db.AddInParameter(completeAssociatesCmd, "@AA_PAN", DbType.String, associatesVo.PanNo);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_PAN", DbType.String, DBNull.Value);
                if (associatesVo.Mobile != 0)
                    db.AddInParameter(completeAssociatesCmd, "@AA_Mobile", DbType.Int64, associatesVo.Mobile);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_Mobile", DbType.Int64, DBNull.Value);
                db.AddInParameter(completeAssociatesCmd, "@AA_RequestDate", DbType.DateTime, associatesVo.RequestDate);

                db.AddInParameter(completeAssociatesCmd, "@AAC_AgentCode", DbType.String, associatesVo.AAC_AgentCode);
                db.AddInParameter(completeAssociatesCmd, "@AAC_UserType", DbType.String, associatesVo.AAC_UserType);
                db.AddOutParameter(completeAssociatesCmd, "@AA_AdviserAssociateId", DbType.Int32, 10);
                db.AddOutParameter(completeAssociatesCmd, "@AAC_AdviserAgentId", DbType.Int32, 10);
                db.AddOutParameter(completeAssociatesCmd, "@U_UserId", DbType.Int32, 10);
                db.AddInParameter(completeAssociatesCmd, "@roleIds", DbType.String, associatesVo.Roleid);
                if(!string.IsNullOrEmpty(associatesVo.AMFIregistrationNo))
                db.AddInParameter(completeAssociatesCmd, "@AA_AMFIregistrationNo", DbType.Int32, associatesVo.AMFIregistrationNo);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_AMFIregistrationNo", DbType.Int32, 0);
                if(associatesVo.AssociationExpairyDate!=DateTime.MinValue)
                db.AddInParameter(completeAssociatesCmd, "@AA_ExpiryDate", DbType.DateTime, associatesVo.AssociationExpairyDate);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_ExpiryDate", DbType.DateTime, DBNull.Value);
                if (associatesVo.ARNDate != DateTime.MinValue)
                    db.AddInParameter(completeAssociatesCmd, "@ARNDate", DbType.DateTime, associatesVo.ARNDate);
                else
                    db.AddInParameter(completeAssociatesCmd, "@ARNDate", DbType.DateTime, DBNull.Value);

                if(associatesVo.StartDate!=DateTime.MinValue)
                db.AddInParameter(completeAssociatesCmd, "@AA_StartDate", DbType.DateTime, associatesVo.StartDate);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_StartDate", DbType.DateTime, DBNull.Value);
                if(associatesVo.EndDate!=DateTime.MinValue)
                db.AddInParameter(completeAssociatesCmd, "@AA_EndDate", DbType.DateTime, associatesVo.EndDate);
                else
                    db.AddInParameter(completeAssociatesCmd, "@AA_EndDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(completeAssociatesCmd, "@AA_EUIN", DbType.String, associatesVo.EUIN);
                db.AddInParameter(completeAssociatesCmd, "@XCT_CustomerTypeCode", DbType.String, associatesVo.AssociateType);
                db.AddInParameter(completeAssociatesCmd, "@XCST_CustomerSubTypeCode", DbType.String, associatesVo.AssociateSubType);
                db.AddInParameter(completeAssociatesCmd, "@AA_IsActive", DbType.Int32, associatesVo.IsActive);
                db.AddInParameter(completeAssociatesCmd, "@AA_IsDummyAssociate", DbType.Int32, associatesVo.IsDummy);
                db.AddInParameter(completeAssociatesCmd, "@KYDStatus", DbType.Boolean, associatesVo.KYDStatus);
                db.AddInParameter(completeAssociatesCmd, "@FormBRecvd", DbType.Boolean, associatesVo.FormBRecvd);
               
                if (db.ExecuteNonQuery(completeAssociatesCmd) != 0)
                {

                    UserId = int.Parse(db.GetParameterValue(completeAssociatesCmd, "U_UserId").ToString());
                    associateId = int.Parse(db.GetParameterValue(completeAssociatesCmd, "AA_AdviserAssociateId").ToString());
                    if (!string.IsNullOrEmpty(db.GetParameterValue(completeAssociatesCmd, "AAC_AdviserAgentId").ToString()))
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

        public List<AssociatesVO> GetViewAssociates(int id, bool isAdviser, bool isBranchHead, bool isBranchId, string currentUserRole)
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
                //db.AddInParameter(viewAssociatesCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(viewAssociatesCmd, "@id", DbType.Int16, Convert.ToInt32(id));
                db.AddInParameter(viewAssociatesCmd, "@isAdviser", DbType.Int16, Convert.ToInt16(isAdviser));
                db.AddInParameter(viewAssociatesCmd, "@isBranchHead", DbType.Int16, Convert.ToInt16(isBranchHead));
                db.AddInParameter(viewAssociatesCmd, "@isBranchId", DbType.Int16, Convert.ToInt16(isBranchId));
                db.AddInParameter(viewAssociatesCmd, "@currentUserRole", DbType.String, currentUserRole);
                ds = db.ExecuteDataSet(viewAssociatesCmd);

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
                objects[0] = id;

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
        public bool CheckPanNumberDuplicatesForAssociates(string Pan, int AdviserAssociateId, int adviserId)
        {
            Database db;
            DbCommand cmdPanDuplicateCheck;
            bool bResult = false;
            int res = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdPanDuplicateCheck = db.GetStoredProcCommand("SPROC_CheckPanNumberDuplicatesForAssociates");
                db.AddInParameter(cmdPanDuplicateCheck, "@PanNumber", DbType.String, Pan);
                db.AddInParameter(cmdPanDuplicateCheck, "@AdviserAssociateId", DbType.Int32, AdviserAssociateId);

                res = int.Parse(db.ExecuteScalar(cmdPanDuplicateCheck).ToString());
                if (res > 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:CheckPanNumberDuplicatesForAssociates()");
                object[] objects = new object[3];
                objects[0] = Pan;
                objects[1] = AdviserAssociateId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool UpdateAdviserAssociates(AssociatesVO associatesVo, int associateId, int userId, string command)
        {
            Database db;
            DbCommand UpdateAssociatesCmd;
            bool result;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAssociatesCmd = db.GetStoredProcCommand("SPROC_UpdateAdviserAssociateDetails");
                db.AddInParameter(UpdateAssociatesCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(UpdateAssociatesCmd, "@AA_AdviserAssociateId", DbType.Int32, associateId);
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
                if (!string.IsNullOrEmpty(associatesVo.CorrAdrLine1.ToString()))
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

                if (!string.IsNullOrEmpty(associatesVo.CorrAdrCity))
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

                if (!string.IsNullOrEmpty(associatesVo.PerAdrCity))
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

                if (!string.IsNullOrEmpty(associatesVo.BranchAdrState))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrState", DbType.String, associatesVo.BranchAdrState);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrState", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(associatesVo.BranchAdrCountry))
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrCountry", DbType.String, associatesVo.BranchAdrCountry);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@AA_BranchAdrCountry", DbType.String, DBNull.Value);
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
                if (associatesVo.NoOfBranches != 0)
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
                db.AddInParameter(UpdateAssociatesCmd, "@command", DbType.String, command);
                db.AddInParameter(UpdateAssociatesCmd, "@categoryId", DbType.Int16, associatesVo.categoryId);

                if (associatesVo.NomineeDOB != DateTime.MinValue)
                    db.AddInParameter(UpdateAssociatesCmd, "@NomineeDOB", DbType.DateTime, associatesVo.NomineeDOB);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@NomineeDOB", DbType.DateTime, DBNull.Value);
                if (associatesVo.BankUpdatedDate != DateTime.MinValue)
                    db.AddInParameter(UpdateAssociatesCmd, "@BankUpdatedDate", DbType.DateTime, associatesVo.BankUpdatedDate);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@BankUpdatedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(UpdateAssociatesCmd, "@BankMobile", DbType.Int64, associatesVo.BankMobile);
             
                if (!string.IsNullOrEmpty(associatesVo.Remarks.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@Remarks", DbType.String, associatesVo.Remarks);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@Remarks", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(associatesVo.BankEmail.ToString().Trim()))
                    db.AddInParameter(UpdateAssociatesCmd, "@BankEmail", DbType.String, associatesVo.BankEmail);
                else
                    db.AddInParameter(UpdateAssociatesCmd, "@BankEmail", DbType.String, DBNull.Value);

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
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssociatesList;
        }

        public bool CreateAdviserAgentCode(AssociatesVO associatesVo, int agentId, int adviserId)
        {
            bool result = false;
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
                db.AddInParameter(createAdviserAgentCodecmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(createAdviserAgentCodecmd, "@AssociatesId", DbType.Int32, associatesVo.AdviserAssociateId);
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_AdviserAgentId", DbType.Int32, associatesVo.AAC_AdviserAgentId);
                db.ExecuteNonQuery(createAdviserAgentCodecmd);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return result;

        }

        public DataSet GetAgentCodeAndType(int adviserId, string Usertype, string agentcode)
        {
            DataSet dsAgentCodeAndTypeList;
            Database db;
            DbCommand getAgentCodeAndTypecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAgentCodeAndTypecmd = db.GetStoredProcCommand("SPROC_GetAgentCodeAndType");
                db.AddInParameter(getAgentCodeAndTypecmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(getAgentCodeAndTypecmd, "@usertype", DbType.String, Usertype);
                db.AddInParameter(getAgentCodeAndTypecmd, "@agentcode", DbType.String, agentcode);
                getAgentCodeAndTypecmd.CommandTimeout = 60 * 60;
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

        public void UpdateAssociatesWorkFlowStatusDetails(int AssociateId, string Status, string StepCode, string StatusReason, string comments)
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
                    if (dr["RMName"] != DBNull.Value)
                        associatesVo.RMNAme = dr["RMName"].ToString();
                    else
                        associatesVo.RMNAme = string.Empty;
                    associatesVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    if (dr["AB_BranchName"] != DBNull.Value)
                        associatesVo.BMName = dr["AB_BranchName"].ToString();
                    else
                        associatesVo.BMName = string.Empty;

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
                        associatesVo.CorrAdrLine2 = dr["AA_CorrAdrLine2"].ToString();
                    else
                        associatesVo.CorrAdrLine2 = string.Empty;
                    if (dr["AA_CorrAdrLine3"] != DBNull.Value)
                        associatesVo.CorrAdrLine3 = dr["AA_CorrAdrLine3"].ToString();
                    else
                        associatesVo.CorrAdrLine3 = string.Empty;
                    if (dr["AA_CorrAdrPinCode"] != DBNull.Value)
                        associatesVo.CorrAdrPinCode = int.Parse(dr["AA_CorrAdrPinCode"].ToString());
                    else
                        associatesVo.CorrAdrPinCode = 0;
                    if (dr["AA_CorrAdrCity"] != DBNull.Value)
                        associatesVo.CorrAdrCity = dr["AA_CorrAdrCity"].ToString();
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
                        associatesVo.PerAdrLine3 = dr["AA_PerAdrLine3"].ToString();
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
                        associatesVo.QualificationCode = dr["XQ_QualificationCode"].ToString();
                    if (dr["AA_ResISDCode"] != DBNull.Value)
                        associatesVo.ResISDCode = int.Parse(dr["AA_ResISDCode"].ToString());
                    if (dr["AA_OfcISDCode"] != DBNull.Value)
                        associatesVo.ResSTDCode = int.Parse(dr["AA_OfcISDCode"].ToString());
                    if (dr["AA_ResPhoneNo"] != DBNull.Value)
                        associatesVo.ResPhoneNo = int.Parse(dr["AA_ResPhoneNo"].ToString());
                    if (dr["AA_DOB"] != DBNull.Value)
                        associatesVo.DOB = DateTime.Parse(dr["AA_DOB"].ToString());
                    if (!string.IsNullOrEmpty(dr["WERPBM_BankCode"].ToString()))
                        associatesVo.BankCode = dr["WERPBM_BankCode"].ToString();
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
                    if (dr["CB_MICR"] != DBNull.Value)
                        associatesVo.MICR = (dr["CB_MICR"].ToString());
                    else
                        associatesVo.MICR = string.Empty;

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
                        associatesVo.RequestDate = DateTime.Parse(dr["AA_RequestDate"].ToString());
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
                        associatesVo.AAC_AgentCode = dr["AAC_AgentCode"].ToString();
                    if (dr["AAC_UserType"] != DBNull.Value)
                        associatesVo.AAC_UserType = dr["AAC_UserType"].ToString();
                    if (bool.Parse(dr["AA_IsActive"].ToString().ToUpper()) != false)
                        associatesVo.IsActive = 1;
                    if (bool.Parse(dr["AA_IsDummyAssociate"].ToString().ToUpper()) != false)
                        associatesVo.IsDummy = 1;

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

        public AssociatesUserHeirarchyVo GetAssociateUserHeirarchy(int UserId, int adviserId)
        {
            AssociatesUserHeirarchyVo associatesUserHeirarchyVo = new AssociatesUserHeirarchyVo();
            Database db;
            DbCommand getAssociateUserCmd;
            DataSet getAssociateUserDs;
            // DataTable table;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssociateUserCmd = db.GetStoredProcCommand("SPROC_GetAssociateUserHeirarchy");
                db.AddInParameter(getAssociateUserCmd, "@U_UserId", DbType.Int32, UserId);
                db.AddInParameter(getAssociateUserCmd, "@adviserId", DbType.Int32, adviserId);

                getAssociateUserDs = db.ExecuteDataSet(getAssociateUserCmd);
                if (getAssociateUserDs.Tables[0].Rows.Count > 0)
                {
                    // table = getAdvisorStaffDs.Tables["AdviserRM"];
                    dr = getAssociateUserDs.Tables[0].Rows[0];
                    associatesUserHeirarchyVo.UserTitle = dr["UserTitle"].ToString();
                    associatesUserHeirarchyVo.AdviserAgentId = int.Parse((dr["AAC_AdviserAgentId"].ToString()));
                    associatesUserHeirarchyVo.UserId = int.Parse((dr["U_UserId"].ToString()));
                    associatesUserHeirarchyVo.UserTitleId = int.Parse(dr["UserTitleId"].ToString());
                    associatesUserHeirarchyVo.AgentCode = dr["AAC_AgentCode"].ToString();
                    associatesUserHeirarchyVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                    associatesUserHeirarchyVo.IsBranchOps = short.Parse(dr["IsBranchOps"].ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {

            }
            return associatesUserHeirarchyVo;
        }
        public int SynchronizeCustomerAssociation(int AdviserId, string Type, int UId)
        {
            Database db;
            DbCommand getSynchcmd;
            int Result;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSynchcmd = db.GetStoredProcCommand("SPROC_SynchronizeCustomerSubbrokerAssociation");
                db.AddInParameter(getSynchcmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(getSynchcmd, "@Type", DbType.String, Type);
                db.AddInParameter(getSynchcmd, "@userId", DbType.Int32, UId);
                Result = db.ExecuteNonQuery(getSynchcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return Result;

        }
        public DataSet GetAdviserAssociateList(int Id, string Usertype, string agentcode)
        {
            Database db;
            DataSet dsGetAdviserAssociateList;
            DbCommand getAdviserAssociateListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserAssociateListcmd = db.GetStoredProcCommand("SPROC_GetAdviserAssociateList");
                db.AddInParameter(getAdviserAssociateListcmd, "@adviserId", DbType.Int32, Id);
                db.AddInParameter(getAdviserAssociateListcmd, "@Usertype", DbType.String, Usertype);
                db.AddInParameter(getAdviserAssociateListcmd, "@agentcode", DbType.String, agentcode);
                dsGetAdviserAssociateList = db.ExecuteDataSet(getAdviserAssociateListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAdviserAssociateList;
        }

        public AssociatesVO GetAssociateVoList(int assiciateId)
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
                getAssociateUserCmd = db.GetStoredProcCommand("SPROC_GetAssociateVoList");
                db.AddInParameter(getAssociateUserCmd, "@assiciateId", DbType.Int32, assiciateId);
                getAssociateUserDs = db.ExecuteDataSet(getAssociateUserCmd);
                if (getAssociateUserDs.Tables[0].Rows.Count > 0)
                {
                    dr = getAssociateUserDs.Tables[0].Rows[0];
                    associatesVo.AdviserAssociateId = int.Parse((dr["AA_AdviserAssociateId"].ToString()));
                    associatesVo.AAC_AdviserAgentId = int.Parse((dr["AAC_AdviserAgentId"].ToString()));
                    if (dr["U_UserId"].ToString() != "")
                        associatesVo.UserId = int.Parse((dr["U_UserId"].ToString()));
                    associatesVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                    if (dr["RMName"] != DBNull.Value)
                        associatesVo.RMNAme = dr["RMName"].ToString();
                    else
                        associatesVo.RMNAme = string.Empty;
                    if (dr["AA_WelcomeNotePath"] != DBNull.Value)
                        associatesVo.WelcomeNotePath = dr["AA_WelcomeNotePath"].ToString();
                    else
                        associatesVo.WelcomeNotePath = string.Empty;
                    associatesVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    if (dr["AB_BranchName"] != DBNull.Value)
                        associatesVo.BMName = dr["AB_BranchName"].ToString();
                    else
                        associatesVo.BMName = string.Empty;

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
                        associatesVo.CorrAdrLine2 = dr["AA_CorrAdrLine2"].ToString();
                    else
                        associatesVo.CorrAdrLine2 = string.Empty;
                    if (dr["AA_CorrAdrLine3"] != DBNull.Value)
                        associatesVo.CorrAdrLine3 = dr["AA_CorrAdrLine3"].ToString();
                    else
                        associatesVo.CorrAdrLine3 = string.Empty;
                    if (dr["AA_CorrAdrPinCode"] != DBNull.Value)
                        associatesVo.CorrAdrPinCode = int.Parse(dr["AA_CorrAdrPinCode"].ToString());
                    else
                        associatesVo.CorrAdrPinCode = 0;
                    if (dr["AA_CorrAdrCity"] != DBNull.Value)
                        associatesVo.CorrAdrCity = dr["AA_CorrAdrCity"].ToString();
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
                        associatesVo.PerAdrLine3 = dr["AA_PerAdrLine3"].ToString();
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
                        associatesVo.OfficePhoneNo = Convert.ToInt64(dr["AA_OfficePhoneNo"].ToString());

                    if (dr["AA_Fax"] != DBNull.Value)
                        associatesVo.ResFaxNumber = int.Parse(dr["AA_Fax"].ToString());
                    if (dr["XMS_MaritalStatusCode"] != DBNull.Value)
                        associatesVo.MaritalStatusCode = dr["XMS_MaritalStatusCode"].ToString();
                    if (dr["AA_Gender"] != DBNull.Value)
                        associatesVo.Gender = dr["AA_Gender"].ToString();
                    if (dr["XQ_QualificationCode"] != DBNull.Value)
                        associatesVo.QualificationCode = dr["XQ_QualificationCode"].ToString();
                    if (dr["AA_ResISDCode"] != DBNull.Value)
                        associatesVo.ResISDCode = int.Parse(dr["AA_ResISDCode"].ToString());
                    if (dr["AA_OfcISDCode"] != DBNull.Value)
                        associatesVo.ResSTDCode = int.Parse(dr["AA_OfcISDCode"].ToString());
                    if (dr["AA_ResPhoneNo"] != DBNull.Value)
                        associatesVo.ResPhoneNo = Convert.ToInt64(dr["AA_ResPhoneNo"].ToString());
                    if (dr["AA_DOB"] != DBNull.Value)
                        associatesVo.DOB = DateTime.Parse(dr["AA_DOB"].ToString());
                    if (dr["WERPBM_BankCode"] != DBNull.Value)
                        associatesVo.BankCode = dr["WERPBM_BankCode"].ToString();
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
                        associatesVo.BranchAdrLine1 = dr["AA_BranchAdrLine1"].ToString();
                    else
                        associatesVo.BranchAdrLine1 = string.Empty;
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
                    if (dr["CB_MICR"] != DBNull.Value)
                        associatesVo.MICR = dr["CB_MICR"].ToString();
                    else
                        associatesVo.MICR = string.Empty;

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
                        associatesVo.RequestDate = DateTime.Parse(dr["AA_RequestDate"].ToString());
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
                        associatesVo.AAC_AgentCode = dr["AAC_AgentCode"].ToString();
                    if (dr["AAC_UserType"] != DBNull.Value)
                        associatesVo.AAC_UserType = dr["AAC_UserType"].ToString();
                    if (dr["AA_EUIN"] != DBNull.Value)
                        associatesVo.EUIN = dr["AA_EUIN"].ToString();
                    if (dr["XCT_CustomerTypeCode"] != DBNull.Value)
                        associatesVo.AssociateType = dr["XCT_CustomerTypeCode"].ToString();
                    if (dr["XCST_CustomerSubTypeCode"] != DBNull.Value)
                        associatesVo.AssociateSubType = dr["XCST_CustomerSubTypeCode"].ToString();
                    if (dr["AssetCodes"] != DBNull.Value)
                        associatesVo.assetGroupCode = dr["AssetCodes"].ToString();
                    if (dr["AH_HierarchyId"] != DBNull.Value)
                        associatesVo.adviserhirerchi = int.Parse(dr["AH_HierarchyId"].ToString());
                    if (dr["DepartmentId"] != DBNull.Value)
                        associatesVo.Departmrntid = int.Parse(dr["DepartmentId"].ToString());
                    if (dr["DepartentRoles"] != DBNull.Value)
                        associatesVo.Roleid = dr["DepartentRoles"].ToString();
                    if (dr["AA_ExpiryDate"] != DBNull.Value)
                        associatesVo.AssociationExpairyDate = DateTime.Parse(dr["AA_ExpiryDate"].ToString());
                    if (bool.Parse(dr["AA_IsActive"].ToString().ToUpper()) != false)
                        associatesVo.IsActive = 1;
                    if (bool.Parse(dr["AA_IsDummyAssociate"].ToString().ToUpper()) != false)
                        associatesVo.IsDummy = 1;

                    if (bool.Parse(dr["AA_FormRecvd"].ToString()) != false)
                        associatesVo.FormBRecvd = true;
                    if (bool.Parse(dr["AA_KYDStatus"].ToString().ToUpper()) != false)
                        associatesVo.KYDStatus = true;
                    if (dr["AA_ARNDATE"] != DBNull.Value)
                        associatesVo.ARNDate = DateTime.Parse(dr["AA_ARNDATE"].ToString());

                    if (dr["AA_BankUpdatedDate"] != DBNull.Value)
                        associatesVo.BankUpdatedDate = DateTime.Parse(dr["AA_BankUpdatedDate"].ToString());

                    if (dr["AA_NomineeDOB"] != DBNull.Value)
                        associatesVo.NomineeDOB = DateTime.Parse(dr["AA_NomineeDOB"].ToString());
                    if (dr["AA_Remarks"] != DBNull.Value)
                        associatesVo.Remarks = dr["AA_Remarks"].ToString();

                    if (dr["AA_BankMobile"] != DBNull.Value)
                        associatesVo.BankMobile = Convert.ToInt64( dr["AA_BankMobile"].ToString());
                    if (dr["AA_BankEmail"] != DBNull.Value)
                        associatesVo.BankEmail = dr["AA_BankEmail"].ToString();
                    if (!string.IsNullOrEmpty(dr["AC_CategoryId"].ToString()))
                        associatesVo.AdviserCategory = dr["AC_CategoryId"].ToString();

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
                objects[0] = assiciateId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associatesVo;
        }

        public DataTable GetRMAssociatesList(int rmId)
        {
            DataSet dsAssociatesList;
            DataTable dtAssociatesList;
            Database db;
            DbCommand getAssociatesListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssociatesListcmd = db.GetStoredProcCommand("SPROC_GetRMAssociatesList");
                db.AddInParameter(getAssociatesListcmd, "@RM_Id", DbType.Int32, rmId);
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
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetRMAssociatesList()");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssociatesList;
        }

        public bool CodeduplicateCheck(int adviserId, string agentCode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdCodeduplicateCheck;
            bool bResult = false;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdCodeduplicateCheck = db.GetStoredProcCommand("SPROC_CodeduplicateChack");
                db.AddInParameter(cmdCodeduplicateCheck, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdCodeduplicateCheck, "@agentCode", DbType.String, agentCode);
                db.AddOutParameter(cmdCodeduplicateCheck, "@count", DbType.Int32, 10);

                ds = db.ExecuteDataSet(cmdCodeduplicateCheck);
                //count = int.Parse(db.ExecuteScalar(cmdCodeduplicateCheck).ToString());
                Object objCount = db.GetParameterValue(cmdCodeduplicateCheck, "@count");
                if (objCount != DBNull.Value)
                    count = int.Parse(db.GetParameterValue(cmdCodeduplicateCheck, "@count").ToString());
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
                objects[0] = adviserId;
                objects[1] = agentCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public DataTable GetAgentCodeFromAgentPaaingAssociateId(int assiciateId, string type)
        {
            DataSet ds;
            Database db;
            DataTable dt = new DataTable();
            DbCommand getcmd;
            //string code = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getcmd = db.GetStoredProcCommand("SPROC_GetAgentCodeFromAgentPaaingAssociateId");
                db.AddInParameter(getcmd, "@Id", DbType.Int32, assiciateId);
                db.AddInParameter(getcmd, "@type", DbType.String, type);
                ds = db.ExecuteDataSet(getcmd);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
                //if (dt.Rows.Count > 0)
                //    code = dt.Rows[0]["AAC_AgentCode"].ToString();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateBo.cs:GetAgentCodeFromAgentPaaingAssociateId(assiciateId)");
                object[] objects = new object[1];
                objects[0] = assiciateId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }

        public bool AddAgentChildCode(AssociatesVO associatesVo, string multiCode)
        {
            bool result = false;
            Database db;
            DbCommand createAdviserAgentCodecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdviserAgentCodecmd = db.GetStoredProcCommand("SPROC_CreateAdviserAgentCode");
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_AgentCode", DbType.String, associatesVo.AAC_AgentCode);
                db.AddInParameter(createAdviserAgentCodecmd, "@multiCode", DbType.String, multiCode);
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_UserType", DbType.String, "associates");
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_CreatedBy", DbType.Int32, associatesVo.AAC_CreatedBy);
                db.AddInParameter(createAdviserAgentCodecmd, "@AAC_ModifiedBy", DbType.Int32, associatesVo.AAC_ModifiedBy);
                db.AddOutParameter(createAdviserAgentCodecmd, "@AAC_AdviserAgentId", DbType.Int32, 1000);
                db.ExecuteNonQuery(createAdviserAgentCodecmd);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return result;
        }

        public DataTable GetAgentChildCodeList(int PagentId)
        {
            DataSet dsChildCodeList;
            DataTable dtChildCodeList = new DataTable();
            Database db;
            DbCommand getAgentChildCodeListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAgentChildCodeListcmd = db.GetStoredProcCommand("SPROC_GetAgentChildCodeList");
                db.AddInParameter(getAgentChildCodeListcmd, "@PagentId", DbType.String, PagentId);
                dsChildCodeList = db.ExecuteDataSet(getAgentChildCodeListcmd);
                if (dsChildCodeList.Tables.Count > 0)
                    dtChildCodeList = dsChildCodeList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateBo.cs:GetAgentChildCodeList(PagentId)");
                object[] objects = new object[1];
                objects[0] = PagentId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtChildCodeList;
        }
        public bool EditAddChildAgentCodeList(AssociatesVO associatesVo, string ChildCode, int PagentId, char flag,string childName,string childEmailId,int userId,string roleIds)
        {
            bool result = false;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_EditAddChildAgentCodeList");
                db.AddInParameter(cmd, "@flag", DbType.String, flag);
                db.AddInParameter(cmd, "@AAC_AgentCode", DbType.String, ChildCode);
                db.AddInParameter(cmd, "@AAC_UserType", DbType.String, associatesVo.AAC_UserType);
                db.AddInParameter(cmd, "@AAC_CreatedBy", DbType.Int32, associatesVo.AAC_CreatedBy);
                db.AddInParameter(cmd, "@AAC_ModifiedBy", DbType.Int32, associatesVo.AAC_ModifiedBy);
                db.AddInParameter(cmd, "@PagentId", DbType.Int32, PagentId);
                db.AddInParameter(cmd, "@AdviserAgentId", DbType.Int32, associatesVo.AAC_AdviserAgentId);
                db.AddInParameter(cmd, "@ChildName", DbType.String, childName);
                db.AddInParameter(cmd, "@ChildEmailId", DbType.String, childEmailId);
                db.AddInParameter(cmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(cmd, "@RoleIds", DbType.String, roleIds);
                db.ExecuteNonQuery(cmd);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return result;
        }

        public bool DeleteChildAgentCode(int childAgentId)
        {
            bool result = false;
            Database db;
            DbCommand cmdDeleteChildAgentCode;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeleteChildAgentCode = db.GetStoredProcCommand("SPROC_DeleteChildAgentCode");
                db.AddInParameter(cmdDeleteChildAgentCode, "@AdviserAgentId", DbType.Int32, childAgentId);
                db.ExecuteNonQuery(cmdDeleteChildAgentCode);
                result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return result;
        }

        public DataSet GetAdviserHierarchyStaffList(int hierarchyRoleId)
        {
            Database db;
            DataSet dsAdviserHierarchyStaffList;
            DbCommand getAdviserHierarchyStaffListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserHierarchyStaffListcmd = db.GetStoredProcCommand("SPROC_GetAdviserHierarchyStaffList");
                db.AddInParameter(getAdviserHierarchyStaffListcmd, "@HierarchyRoleId", DbType.String, hierarchyRoleId);
                dsAdviserHierarchyStaffList = db.ExecuteDataSet(getAdviserHierarchyStaffListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAdviserHierarchyStaffList;
        }

        public DataTable GetSalesListToAddCode(int AdviserId)
        {
            DataSet dsSalesListToAddCode;
            DataTable dtSalesListToAddCode = new DataTable();
            Database db;
            DbCommand getSalesListToAddCodecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSalesListToAddCodecmd = db.GetStoredProcCommand("SPROC_GetSalesListToAddCode");
                db.AddInParameter(getSalesListToAddCodecmd, "@AdviserId", DbType.String, AdviserId);
                dsSalesListToAddCode = db.ExecuteDataSet(getSalesListToAddCodecmd);
                if (dsSalesListToAddCode.Tables.Count > 0)
                    dtSalesListToAddCode = dsSalesListToAddCode.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateBo.cs:GetSalesListToAddCode(AdviserId)");
                object[] objects = new object[1];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSalesListToAddCode;
        }

        public DataTable GetAssociatesSubBrokerCodeList(int adviserId)
        {
            DataSet dsAssociatesSubBrokerCodeList;
            DataTable dtAssociatesSubBrokerCodeList;
            Database db;
            DbCommand getAssociatesSubBrokerCodeListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssociatesSubBrokerCodeListcmd = db.GetStoredProcCommand("SPROC_GetAssociatesSubBrokerCodeList");
                db.AddInParameter(getAssociatesSubBrokerCodeListcmd, "@adviserId", DbType.Int32, adviserId);
                dsAssociatesSubBrokerCodeList = db.ExecuteDataSet(getAssociatesSubBrokerCodeListcmd);
                dtAssociatesSubBrokerCodeList = dsAssociatesSubBrokerCodeList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetRMAssociatesList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtAssociatesSubBrokerCodeList;
        }
        public DataTable GetSalesSubBrokerCodeList(int adviserId)
        {
            DataSet dsSalesSubBrokerCodeList;
            DataTable dtSalesSubBrokerCodeList;
            Database db;
            DbCommand getSalesSubBrokerCodeListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSalesSubBrokerCodeListcmd = db.GetStoredProcCommand("SPROC_GetSalesSubBrokerCodeList");
                db.AddInParameter(getSalesSubBrokerCodeListcmd, "@adviserId", DbType.Int32, adviserId);
                dsSalesSubBrokerCodeList = db.ExecuteDataSet(getSalesSubBrokerCodeListcmd);
                dtSalesSubBrokerCodeList = dsSalesSubBrokerCodeList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetSalesSubBrokerCodeList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSalesSubBrokerCodeList;
        }
        public DataTable GetBranchSubBrokerCodeList(int adviserId)
        {
            DataSet dsBranchSubBrokerCodeList;
            DataTable dtbranchSubBrokerCodeList;
            Database db;
            DbCommand getBranchSubBrokerCodeListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchSubBrokerCodeListcmd = db.GetStoredProcCommand("SPROC_GetBranchSubBrokerCodeList");
                db.AddInParameter(getBranchSubBrokerCodeListcmd, "@adviserId", DbType.Int32, adviserId);
                dsBranchSubBrokerCodeList = db.ExecuteDataSet(getBranchSubBrokerCodeListcmd);
                dtbranchSubBrokerCodeList = dsBranchSubBrokerCodeList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetBranchSubBrokerCodeList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtbranchSubBrokerCodeList;
        }
        /// <summary>
        ///  Transaction Product details
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
        public DataSet GetProductDetailsFromMFTransaction(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, DateTime FromDate, DateTime Todate, int All, int IsOnline)
        {
            Database db;
            DbCommand GetProductDetailFromMFOrderCmd;
            DataSet dsProductDetailFromMFOrder;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetProductDetailFromMFOrderCmd = db.GetStoredProcCommand("SPROC_GetProductDetailsFromMFTransaction");
                db.AddInParameter(GetProductDetailFromMFOrderCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetProductDetailFromMFOrderCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetProductDetailFromMFOrderCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetProductDetailFromMFOrderCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetProductDetailFromMFOrderCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetProductDetailFromMFOrderCmd, "@all", DbType.Int32, All);
                db.AddInParameter(GetProductDetailFromMFOrderCmd, "@IsOnline", DbType.Int32, IsOnline);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetProductDetailFromMFOrderCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetProductDetailFromMFOrderCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(agentcode))
                    db.AddInParameter(GetProductDetailFromMFOrderCmd, "@agentcode", DbType.String, agentcode);
                else
                    db.AddInParameter(GetProductDetailFromMFOrderCmd, "@agentcode", DbType.String, DBNull.Value);
                dsProductDetailFromMFOrder = db.ExecuteDataSet(GetProductDetailFromMFOrderCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetProductDetailsFromMFTransaction()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsProductDetailFromMFOrder;
        }

        public DataSet GetOrganizationDetailFromTransaction(string agentcode, string userType, int AdviserId, int rmId, int branchId, int branchHeadId, DateTime FromDate, DateTime Todate, int All)
        {
            Database db;
            DbCommand GetOrganizationDetailFromMFOrderCmd;
            DataSet dsOrganizationDetailFromMFOrder;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOrganizationDetailFromMFOrderCmd = db.GetStoredProcCommand("SPROC_GetOrganizationDetailsFromMFTransaction");
                db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@all", DbType.Int32, All);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(agentcode))
                    db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@agentcode", DbType.String, agentcode);
                else
                    db.AddInParameter(GetOrganizationDetailFromMFOrderCmd, "@agentcode", DbType.String, DBNull.Value);
                dsOrganizationDetailFromMFOrder = db.ExecuteDataSet(GetOrganizationDetailFromMFOrderCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetOrganizationDetailFromTransaction()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                objects[1] = rmId;
                objects[2] = branchId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsOrganizationDetailFromMFOrder;
        }

        public DataSet GetMemberDetailFromTransaction(string agentcode, string userType, int AdviserId, int branchHeadId, DateTime FromDate, DateTime Todate, int IsOnline)
        {
            Database db;
            DbCommand GetMemberDetailFromTrnxCmd;
            DataSet dsGetMemberDetailFromTranx;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetMemberDetailFromTrnxCmd = db.GetStoredProcCommand("SPROC_GetMemberDetailsFromMFTransaction");
                db.AddInParameter(GetMemberDetailFromTrnxCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetMemberDetailFromTrnxCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetMemberDetailFromTrnxCmd, "@branchHeadId", DbType.Int32, branchHeadId);
                db.AddInParameter(GetMemberDetailFromTrnxCmd, "@IsOnline", DbType.Int32, IsOnline);
                if (FromDate != DateTime.MinValue)
                    db.AddInParameter(GetMemberDetailFromTrnxCmd, "@FromDate", DbType.DateTime, FromDate);
                else
                    FromDate = DateTime.MinValue;
                if (Todate != DateTime.MinValue)
                    db.AddInParameter(GetMemberDetailFromTrnxCmd, "@ToDate", DbType.DateTime, Todate);
                else
                    Todate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(agentcode))
                    db.AddInParameter(GetMemberDetailFromTrnxCmd, "@agentcode", DbType.String, agentcode);
                else
                    db.AddInParameter(GetMemberDetailFromTrnxCmd, "@agentcode", DbType.String, DBNull.Value);
                GetMemberDetailFromTrnxCmd.CommandTimeout = 60 * 60;
                dsGetMemberDetailFromTranx = db.ExecuteDataSet(GetMemberDetailFromTrnxCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorMFDao.cs:GetOrganizationDetailFromTransaction()");

                object[] objects = new object[3];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMemberDetailFromTranx;
        }

        public DataSet BindChannelList(int AdviserId)
        {
            Database db;
            DbCommand ChannelListCmd;
            DataSet dsChannelList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                ChannelListCmd = db.GetStoredProcCommand("SPROC_BindChannelList");
                db.AddInParameter(ChannelListCmd, "@adviserId", DbType.Int32, AdviserId);

                dsChannelList = db.ExecuteDataSet(ChannelListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsChannelList;
        }

        public DataSet BindTitleList(int channelId, int AdviserId)
        {
            Database db;
            DbCommand TitleListCmd;
            DataSet dsTitleList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                TitleListCmd = db.GetStoredProcCommand("SPROC_BindTitleList");
                db.AddInParameter(TitleListCmd, "@channelId", DbType.Int32, channelId);
                db.AddInParameter(TitleListCmd, "@adviserId", DbType.Int32, AdviserId);

                dsTitleList = db.ExecuteDataSet(TitleListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsTitleList;
        }
        public DataSet BindSubBrokerList(int searchId, int AdviserId, string searchType)
        {
            Database db;
            DbCommand SubBrokerListCmd;
            DataSet dsSubBrokerList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SubBrokerListCmd = db.GetStoredProcCommand("SPROC_BindSubBrokerList");
                db.AddInParameter(SubBrokerListCmd, "@searchId", DbType.Int32, searchId);
                db.AddInParameter(SubBrokerListCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(SubBrokerListCmd, "@searchType", DbType.String, searchType);
                dsSubBrokerList = db.ExecuteDataSet(SubBrokerListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsSubBrokerList;
        }

        public DataTable GetStateList()
        {
            Database db;
            DbCommand StateListCmd;
            DataSet dsStateList;
            DataTable dtStateList = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                StateListCmd = db.GetStoredProcCommand("SPROC_GetStateList");
                dsStateList = db.ExecuteDataSet(StateListCmd);
                if (dsStateList.Tables.Count > 0)
                    dtStateList = dsStateList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetStateList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtStateList;
        }

        public DataTable GetCityList(string stateId, int flag)
        {
            Database db;
            DbCommand CityListCmd;
            DataSet dsCityList;
            DataTable dtCityList = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CityListCmd = db.GetStoredProcCommand("SPROC_GetCityList");
                if (!string.IsNullOrEmpty(stateId))
                    db.AddInParameter(CityListCmd, "@stateId", DbType.String, stateId);
                else
                    db.AddInParameter(CityListCmd, "@stateId", DbType.String, DBNull.Value);
                db.AddInParameter(CityListCmd, "@flag", DbType.Int32, flag);
                dsCityList = db.ExecuteDataSet(CityListCmd);
                if (dsCityList.Tables.Count > 0)
                    dtCityList = dsCityList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationBo.cs:GetCityList()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtCityList;
        }

        public DataTable GetAdviserHierarchyTitleList(int adviserId)
        {
            Database db;
            DbCommand adviserHierarchyTitleListCmd;
            DataSet dsAdviserHierarchyTitleList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                adviserHierarchyTitleListCmd = db.GetStoredProcCommand("SPROC_GetAdviserHierarchyTitleList");
                db.AddInParameter(adviserHierarchyTitleListCmd, "@AdviserId", DbType.Int32, adviserId);
                dsAdviserHierarchyTitleList = db.ExecuteDataSet(adviserHierarchyTitleListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetAdviserHierarchyTitleList()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserHierarchyTitleList.Tables[0];

        }

        public DataTable GetAdviserStaffBranchList(int staffId)
        {
            Database db;
            DbCommand adviserStaffBranchListCmd;
            DataSet dsAdviserStaffBranchList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                adviserStaffBranchListCmd = db.GetStoredProcCommand("SPROC_GetAdviserStaffBranchList");
                db.AddInParameter(adviserStaffBranchListCmd, "@staffId", DbType.Int32, staffId);
                dsAdviserStaffBranchList = db.ExecuteDataSet(adviserStaffBranchListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociatesDAO.cs:GetAdviserStaffBranchList()");
                object[] objects = new object[1];
                objects[0] = staffId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserStaffBranchList.Tables[0];

        }
        public bool UpdateUserrole(int DepartmentId, string rollid)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateUserroleCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateUserroleCmd = db.GetStoredProcCommand("SPROC_UpdateAdviserRoleAssociation");
                db.AddInParameter(UpdateUserroleCmd, "@userIds", DbType.Int32, DepartmentId);
                db.AddInParameter(UpdateUserroleCmd, "@roleIds", DbType.String, rollid);
                if (db.ExecuteNonQuery(UpdateUserroleCmd) != 0)
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
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateUserrole()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public DataSet GetDepartment(int adviserId)
        {
            Database db;
            DataSet dsGetUserRole;
            DbCommand GetUserRolecmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetUserRolecmd = db.GetStoredProcCommand("SPROC_GetDepartmentNameAssociate");
                db.AddInParameter(GetUserRolecmd, "@adviserId", DbType.Int64, adviserId);
                dsGetUserRole = db.ExecuteDataSet(GetUserRolecmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetFrequency()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetUserRole;
        }
        public string GetAgentCode(int agentId, int adviserId)
        {
            Database db;
            DataSet ds;
            DbCommand cmdGetAgentCode;
            string agentCode = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetAgentCode = db.GetStoredProcCommand("SPROC_CheckAdviserAgentCode");
                db.AddInParameter(cmdGetAgentCode, "@adviserAgentId", DbType.Int32, agentId);
                db.AddInParameter(cmdGetAgentCode, "@adviserId", DbType.Int32, adviserId);
                db.AddOutParameter(cmdGetAgentCode, "@agentCode", DbType.String, 20);
                ds = db.ExecuteDataSet(cmdGetAgentCode);
                if (db.ExecuteNonQuery(cmdGetAgentCode) != 0)
                {
                    agentCode = db.GetParameterValue(cmdGetAgentCode, "agentCode").ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return agentCode;
        }
        public string GetPANNo(int agentId)
        {
            Database db;
            DataSet ds;
            DbCommand cmdGetPANNo;
            string PANNo = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetPANNo = db.GetStoredProcCommand("SPROC_CheckDuplicatePANNo");
                db.AddInParameter(cmdGetPANNo, "@adviserAssociateId", DbType.Int32, agentId);
                db.AddOutParameter(cmdGetPANNo, "@PANN0", DbType.String, 20);
                ds = db.ExecuteDataSet(cmdGetPANNo);
                if (db.ExecuteNonQuery(cmdGetPANNo) != 0)
                {
                    PANNo = db.GetParameterValue(cmdGetPANNo, "PANN0").ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return PANNo;
        }
        public bool UpdateAssociate(AssociatesVO associatesVo, int userId, int associateId, int agentId)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateAssociateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAssociateCmd = db.GetStoredProcCommand("SPROC_UpdateAssociateDetails");
                db.AddInParameter(UpdateAssociateCmd, "@AA_ContactPersonName ", DbType.String, associatesVo.ContactPersonName);
                db.AddInParameter(UpdateAssociateCmd, "@AR_RMid ", DbType.Int32, associatesVo.RMId);
                db.AddInParameter(UpdateAssociateCmd, "@AB_BranchId", DbType.Int32, associatesVo.BranchId);
                db.AddInParameter(UpdateAssociateCmd, "@AA_PAN", DbType.String, associatesVo.PanNo);
                // db.AddInParameter(UpdateAssociateCmd, "@U_LoginId", DbType.String, associatesVo.AAC_AgentCode);
                db.AddInParameter(UpdateAssociateCmd, "@AAC_AgentCode", DbType.String, associatesVo.AAC_AgentCode);
                db.AddInParameter(UpdateAssociateCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(UpdateAssociateCmd, "@AAC_AdviserAgentId", DbType.Int32, agentId);
                db.AddInParameter(UpdateAssociateCmd, "@AA_AdviserAssociateId", DbType.Int32, associateId);

                if (db.ExecuteNonQuery(UpdateAssociateCmd) != 0)
                    bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool UpdateAssociateWelcomeNotePath(int userId, long associateId, string welcomeNotePath)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateAssociateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAssociateCmd = db.GetStoredProcCommand("SPROC_UpdateAssociateWelcomeNotePath");
                db.AddInParameter(UpdateAssociateCmd, "@AA_WelcomeNotePath", DbType.String, welcomeNotePath);
                db.AddInParameter(UpdateAssociateCmd, "@AA_AdviserAssociateId", DbType.Int64, associateId);
                db.AddInParameter(UpdateAssociateCmd, "@UserId", DbType.Int32, userId);
                if (db.ExecuteNonQuery(UpdateAssociateCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool UpdateAssociateDetails(AssociatesVO associatesVo, int userId, int associateid, int agentcode)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateAssociateDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAssociateDetailsCmd = db.GetStoredProcCommand("SPROC_AssociateUpdate");
                db.AddInParameter(UpdateAssociateDetailsCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_ContactPersonName", DbType.String, associatesVo.ContactPersonName);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_AdviserAssociateId", DbType.Int32, associateid);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_AMFIregistrationNo", DbType.String, associatesVo.AMFIregistrationNo);
                if(associatesVo.StartDate!=DateTime.MinValue)
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_StartDate", DbType.DateTime, associatesVo.StartDate);
                else
                    db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_StartDate", DbType.DateTime,DBNull.Value);
                if(associatesVo.EndDate!=DateTime.MinValue)
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_EndDate", DbType.DateTime, associatesVo.EndDate);
                else
                    db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_EndDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_EUIN", DbType.String, associatesVo.EUIN);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@XCT_CustomerTypeCode", DbType.String, associatesVo.AssociateType);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@XCST_CustomerSubTypeCode", DbType.String, associatesVo.AssociateSubType);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_PAN", DbType.String, associatesVo.PanNo);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AAC_AgentCode", DbType.String, associatesVo.AAC_AgentCode);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AgentId", DbType.Int32, agentcode);
                if(associatesVo.AssociationExpairyDate!=DateTime.MinValue)
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_ExpiryDate", DbType.DateTime, associatesVo.AssociationExpairyDate);
                else
                    db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_ExpiryDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_IsActive", DbType.Int32, associatesVo.IsActive);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AB_BranchId", DbType.Int32, associatesVo.BranchId);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@AA_IsDummyAssociate", DbType.Int32, associatesVo.IsDummy);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@KYDStatus", DbType.Boolean, associatesVo.KYDStatus);
                db.AddInParameter(UpdateAssociateDetailsCmd, "@FormBRecvd", DbType.Boolean, associatesVo.FormBRecvd);
                if (associatesVo.ARNDate != DateTime.MinValue)
                    db.AddInParameter(UpdateAssociateDetailsCmd, "@ARNDate", DbType.DateTime, associatesVo.ARNDate);
                else
                    db.AddInParameter(UpdateAssociateDetailsCmd, "@ARNDate", DbType.DateTime, DBNull.Value);


                if (db.ExecuteNonQuery(UpdateAssociateDetailsCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }
        public DataTable AssetsGroup()
        {
            DataTable dtAssetsGroup;
            DataSet dsAssetsGroup;
            DbCommand cmdAssetsGroup;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdAssetsGroup = db.GetStoredProcCommand("SPROC_AssetsGroup");
                dsAssetsGroup = db.ExecuteDataSet(cmdAssetsGroup);
                dtAssetsGroup = dsAssetsGroup.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dtAssetsGroup;
        }
        public DataTable GetAssetsRegistration(int associateId)
        {
            DataTable dtGetAssetsRegistration;
            DataSet dsGetAssetsRegistration;
            DbCommand cmdGetAssetsRegistration;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAssetsRegistration = db.GetStoredProcCommand("SPROC_GetAssociateRegistration");
                db.AddInParameter(cmdGetAssetsRegistration, "@associateId", DbType.Int32, associateId);
                dsGetAssetsRegistration = db.ExecuteDataSet(cmdGetAssetsRegistration);
                dtGetAssetsRegistration = dsGetAssetsRegistration.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dtGetAssetsRegistration;
        }
        public bool AssociateRegistration(int associateId,DateTime registrationExp,int RegistrationNo,string assetsGroup)
        {
            bool bResult = false;
            Database db;
            DbCommand AssociateRegistrationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AssociateRegistrationCmd = db.GetStoredProcCommand("SPROC_CreateAdviserAssociateRegistration");
                db.AddInParameter(AssociateRegistrationCmd, "@associatId", DbType.Int32, associateId);
                db.AddInParameter(AssociateRegistrationCmd, "@assetsGroup", DbType.String, assetsGroup);
                db.AddInParameter(AssociateRegistrationCmd, "@registrationNo", DbType.Int32, RegistrationNo);
                db.AddInParameter(AssociateRegistrationCmd, "@expiryDate", DbType.DateTime, registrationExp);
                if (db.ExecuteNonQuery(AssociateRegistrationCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }
        public bool AssociateFieldValidation(string text, string Type, int adviserId, int AdviserAssociateId)
        {
            bool bResult = false;
            Database db;
            DbCommand AssociateFieldValidationCmd;
            int res;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AssociateFieldValidationCmd = db.GetStoredProcCommand("SPROC_AssociateFieldValidation");
                db.AddInParameter(AssociateFieldValidationCmd, "@AdviserAssociateId", DbType.Int32, AdviserAssociateId);
                db.AddInParameter(AssociateFieldValidationCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(AssociateFieldValidationCmd, "@Type", DbType.String, Type);
                db.AddInParameter(AssociateFieldValidationCmd, "@Text", DbType.String, text);
                res = int.Parse(db.ExecuteScalar(AssociateFieldValidationCmd).ToString());
                if (res > 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return bResult;
        }
    }
}
