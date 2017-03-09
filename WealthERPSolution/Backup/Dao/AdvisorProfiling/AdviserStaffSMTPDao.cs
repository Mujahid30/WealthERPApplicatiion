using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoAdvisorProfiling;
using System.Data.SqlClient;


namespace DaoAdvisorProfiling
{
    public class AdviserStaffSMTPDao
    {
        public bool InsertAdviserStaffSMTP(AdviserStaffSMTPVo adviserStaffSMTPvo)
        {
            bool result = false;

            Database db;
            DbCommand cmdInsert;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsert = db.GetStoredProcCommand("SP_InsertAdviserStaffSMTP");

                db.AddInParameter(cmdInsert, "@AR_RMId", DbType.Int32, adviserStaffSMTPvo.RMId);
                db.AddInParameter(cmdInsert, "@ASS_Email", DbType.String, adviserStaffSMTPvo.Email);
                db.AddInParameter(cmdInsert, "@ASS_HostServer", DbType.String, adviserStaffSMTPvo.HostServer);
                db.AddInParameter(cmdInsert, "@ASS_IsAuthenticationRequired", DbType.Int16, adviserStaffSMTPvo.IsAuthenticationRequired);
                if (adviserStaffSMTPvo.ModifiedBy > 0)
                    db.AddInParameter(cmdInsert, "@CurrentUser", DbType.Int32, adviserStaffSMTPvo.ModifiedBy);
                else if (adviserStaffSMTPvo.CreatedBy > 0)
                    db.AddInParameter(cmdInsert, "@CurrentUser", DbType.Int32, adviserStaffSMTPvo.CreatedBy);
                //db.AddInParameter(cmdInsert, "@ASS_ModifiedOn", DbType.DateTime, adviserStaffSMTPvo.ModifiedOn);
                db.AddInParameter(cmdInsert, "@ASS_Password", DbType.String, adviserStaffSMTPvo.Password);
                db.AddInParameter(cmdInsert, "@ASS_Port", DbType.String, adviserStaffSMTPvo.Port);
                db.AddInParameter(cmdInsert, "@ASS_SenderEmailAlias", DbType.String, adviserStaffSMTPvo.SenderEmailAlias);
                db.ExecuteNonQuery(cmdInsert);

                result = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdviserStaffSMTPDao.cs:InsertAdviserStaffSMTP()");
                object[] objects = new object[1];
                objects[0] = adviserStaffSMTPvo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return result;
        }

        public AdviserStaffSMTPVo GetSMTPCredentials(int RMId)
        {
            Database db;
            DbCommand cmd;
            DataSet dsSMTPCredentials;
            AdviserStaffSMTPVo adviserStaffSMTPVo = new AdviserStaffSMTPVo();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_AdviserStaffGetSMTPSettings");
                db.AddInParameter(cmd, "@RMId", DbType.Int32, RMId);
                dsSMTPCredentials = db.ExecuteDataSet(cmd);
                if (dsSMTPCredentials.Tables.Count > 0)
                {
                    if (dsSMTPCredentials.Tables[0].Rows.Count > 0)
                    {
                        adviserStaffSMTPVo.HostServer = dsSMTPCredentials.Tables[0].Rows[0]["ASS_HostServer"].ToString();
                        adviserStaffSMTPVo.Email = dsSMTPCredentials.Tables[0].Rows[0]["ASS_Email"].ToString();
                        adviserStaffSMTPVo.Password = dsSMTPCredentials.Tables[0].Rows[0]["ASS_Password"].ToString();
                        adviserStaffSMTPVo.IsAuthenticationRequired = Convert.ToInt16(dsSMTPCredentials.Tables[0].Rows[0]["ASS_IsAuthenticationRequired"]);
                        adviserStaffSMTPVo.Port = dsSMTPCredentials.Tables[0].Rows[0]["ASS_Port"].ToString();
                        adviserStaffSMTPVo.SenderEmailAlias = dsSMTPCredentials.Tables[0].Rows[0]["ASS_SenderEmailAlias"].ToString();
                    }
                    if (dsSMTPCredentials.Tables[1].Rows.Count > 0)
                    {
                        adviserStaffSMTPVo.SmsProviderId = Convert.ToInt32(dsSMTPCredentials.Tables[1].Rows[0]["WERPSMSPM_Id"].ToString());
                        adviserStaffSMTPVo.SmsUserName = dsSMTPCredentials.Tables[1].Rows[0]["ASMSP_Username"].ToString();
                        adviserStaffSMTPVo.Smspassword = dsSMTPCredentials.Tables[1].Rows[0]["ASMSP_Password"].ToString();
                        adviserStaffSMTPVo.SmsInitialcredit = Convert.ToInt32(dsSMTPCredentials.Tables[1].Rows[0]["ASMSP_InitialCredit"].ToString());
                        adviserStaffSMTPVo.SmsCreditLeft = Convert.ToInt32(dsSMTPCredentials.Tables[1].Rows[0]["ASMSP_CreditLeft"].ToString());
                        adviserStaffSMTPVo.SmsSenderId = dsSMTPCredentials.Tables[1].Rows[0]["ASMSP_SenderId"].ToString();
                    }
                    if (dsSMTPCredentials.Tables[2].Rows.Count > 0)
                    {
                        adviserStaffSMTPVo.ApiProviderId = Convert.ToInt32(dsSMTPCredentials.Tables[2].Rows[0]["WEAM_ID"].ToString());
                        adviserStaffSMTPVo.ApiUserName = dsSMTPCredentials.Tables[2].Rows[0]["AEAC_Username"].ToString();
                        adviserStaffSMTPVo.Apipassword = dsSMTPCredentials.Tables[2].Rows[0]["AEAC_Password"].ToString();
                        adviserStaffSMTPVo.ApiMemberId = dsSMTPCredentials.Tables[2].Rows[0]["AEAC_MemberId"].ToString();
                    }
                }

                //SqlDataReader sdr = (SqlDataReader)db.ExecuteReader(cmd);
                //while (sdr.Read())
                //{
                //    adviserStaffSMTPVo.HostServer = sdr["ASS_HostServer"].ToString();
                //    adviserStaffSMTPVo.Email = sdr["ASS_Email"].ToString();
                //    adviserStaffSMTPVo.Password = sdr["ASS_Password"].ToString();
                //    adviserStaffSMTPVo.IsAuthenticationRequired = Convert.ToInt16(sdr["ASS_IsAuthenticationRequired"]);
                //    adviserStaffSMTPVo.Port = sdr["ASS_Port"].ToString();
                //    adviserStaffSMTPVo.SenderEmailAlias = sdr["ASS_SenderEmailAlias"].ToString();
                    
                //}

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdviserStaffSMTPDao.cs:GetSMTPCredentials()");

                object[] objects = new object[1];
                objects[0] = RMId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return adviserStaffSMTPVo;


        }
        public DataSet GetSMSProvider()
        {
            DataSet dsSMSProvider;
            Database db;
            DbCommand getSMSProviderCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSMSProviderCmd = db.GetStoredProcCommand("SP_GetSMSProviderMaster");
                dsSMSProvider = db.ExecuteDataSet(getSMSProviderCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSMSProvider;
        }

        public bool CreateSMSProviderDetails(AdviserStaffSMTPVo adviserStaffSMTPvo)
        {
            bool bResult = false;
            Database db;
            DbCommand createSMSProviderCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createSMSProviderCmd = db.GetStoredProcCommand("SP_CreateSMSProviderDetails");

                db.AddInParameter(createSMSProviderCmd, "@WERPSMSPM_Id", DbType.Int32, adviserStaffSMTPvo.SmsProviderId);
                db.AddInParameter(createSMSProviderCmd, "@A_AdviserId", DbType.Int32, adviserStaffSMTPvo.AdvisorId);
                db.AddInParameter(createSMSProviderCmd, "@ASMSP_Username", DbType.String, adviserStaffSMTPvo.SmsUserName);
                db.AddInParameter(createSMSProviderCmd, "@ASMSP_Password", DbType.String, adviserStaffSMTPvo.Smspassword);
                db.AddInParameter(createSMSProviderCmd, "@ASMSP_InitialCredit", DbType.Int32, adviserStaffSMTPvo.SmsInitialcredit);
                db.AddInParameter(createSMSProviderCmd, "@ASMSP_CreditLeft", DbType.Int32, adviserStaffSMTPvo.SmsCreditLeft);
                db.AddInParameter(createSMSProviderCmd, "@ASMSP_CreatedBy", DbType.Int32, adviserStaffSMTPvo.SmsCreatedBy);
                db.AddInParameter(createSMSProviderCmd, "@ASMSP_ModifiedBy", DbType.Int32, adviserStaffSMTPvo.SmsModifiedBy);
                db.AddInParameter(createSMSProviderCmd, "@SenderId", DbType.String, adviserStaffSMTPvo.SmsSenderId);
                db.ExecuteNonQuery(createSMSProviderCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }


        public DataSet GetAPIProvider()
        {
            DataSet dsAPIProvider;
            Database db;
            DbCommand getAPIProviderCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAPIProviderCmd = db.GetStoredProcCommand("SP_GetAPIProviderMaster");
                dsAPIProvider = db.ExecuteDataSet(getAPIProviderCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsAPIProvider;
        }

        public bool CreateAPIProviderDetails(AdviserStaffSMTPVo adviserStaffSMTPvo)
        {
            bool bResult = false;
            Database db;
            DbCommand createAPIProviderCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAPIProviderCmd = db.GetStoredProcCommand("SP_CreateAPIProviderDetails");

                db.AddInParameter(createAPIProviderCmd, "@WEAM_ID", DbType.Int32, adviserStaffSMTPvo.ApiProviderId);
                db.AddInParameter(createAPIProviderCmd, "@A_AdviserId", DbType.Int32, adviserStaffSMTPvo.AdvisorId);
                db.AddInParameter(createAPIProviderCmd, "@AEAC_Username", DbType.String, adviserStaffSMTPvo.ApiUserName);
                db.AddInParameter(createAPIProviderCmd, "@AEAC_Password", DbType.String, adviserStaffSMTPvo.NewPassword);
                db.AddInParameter(createAPIProviderCmd, "@AEAC_CreatedBy", DbType.Int32, adviserStaffSMTPvo.ApiCreatedBy);
                db.AddInParameter(createAPIProviderCmd, "@AEAC_MemberId", DbType.String, adviserStaffSMTPvo.ApiMemberId);
                db.ExecuteNonQuery(createAPIProviderCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

    }
}
