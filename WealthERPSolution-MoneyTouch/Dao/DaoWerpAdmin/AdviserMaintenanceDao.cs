using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCustomerProfiling;
using VoAdvisorProfiling;

namespace DaoWerpAdmin
{
    public class AdviserMaintenanceDao
    { 
        /// <summary>
        /// This function is used in two places. One is IFF Superadmin Grid and Two is UserManagement
        /// </summary>
        /// <returns></returns>

        public List<AdvisorVo> GetAdviserListWithPager(int CurrentPage, out int Count, string SortExpression, string filterExpression)
        {
            List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
            //List<AdvisorLOBVo> advisorLOBVoList=new List<AdvisorLOBVo>();
            AdvisorVo adviserVo = new AdvisorVo();
            DataSet getAdvisorDs;
            DataTable dtAdvisers;
            Database db;
            DbCommand Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetAllAdvisersWithPaging");
                db.AddInParameter(Cmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(Cmd, "@SortOrder", DbType.String, SortExpression);
                db.AddInParameter(Cmd, "@filterexpression", DbType.String, filterExpression);
                getAdvisorDs = db.ExecuteDataSet(Cmd);
                dtAdvisers = getAdvisorDs.Tables[0];
                foreach (DataRow dr in dtAdvisers.Rows)
                {
                    adviserVo = new AdvisorVo();

                    adviserVo.advisorId = int.Parse(dr["A_AdviserId"].ToString());
                    adviserVo.UserId = int.Parse(dr["U_UserId"].ToString());
                    adviserVo.BusinessCode = dr["XABT_BusinessTypeCode"].ToString();
                    adviserVo.OrganizationName = dr["A_OrgName"].ToString();
                    adviserVo.AddressLine1 = dr["A_AddressLine1"].ToString();
                    adviserVo.AddressLine2 = dr["A_AddressLine2"].ToString();
                    adviserVo.AddressLine3 = dr["A_AddressLine3"].ToString();
                    //advisorVo.BusinessCode = dr["BT_BusinessCode"].ToString();
                    adviserVo.City = dr["A_City"].ToString();
                    adviserVo.ContactPersonFirstName = dr["A_ContactPersonFirstName"].ToString();
                    adviserVo.ContactPersonLastName = dr["A_ContactPersonLastName"].ToString();
                    adviserVo.ContactPersonMiddleName = dr["A_ContactPersonMiddleName"].ToString();
                    adviserVo.Country = dr["A_Country"].ToString();
                    adviserVo.Email1 = dr["A_Email"].ToString();
                    adviserVo.Website = dr["A_Website"].ToString();
                    if (dr["A_Fax"] != null && dr["A_Fax"].ToString() != "")
                        adviserVo.Fax = int.Parse(dr["A_Fax"].ToString());
                    if (dr["A_FaxISD"] != null && dr["A_FaxISD"].ToString() != "")
                        adviserVo.FaxIsd = int.Parse(dr["A_FaxISD"].ToString());
                    if (dr["A_FaxSTD"] != null && dr["A_FaxSTD"].ToString() != "")
                        adviserVo.FaxStd = int.Parse(dr["A_FaxSTD"].ToString());
                    if (dr["A_ContactPersonMobile"] != null && dr["A_ContactPersonMobile"].ToString() != "")
                        adviserVo.MobileNumber = Convert.ToInt64(dr["A_ContactPersonMobile"].ToString());
                    if (dr["A_IsMultiBranch"].ToString() != "" && dr["A_IsMultiBranch"].ToString() != null)
                        adviserVo.MultiBranch = int.Parse(dr["A_IsMultiBranch"].ToString());
                    if (dr["A_IsAssociateModel"].ToString() != "" && dr["A_IsAssociateModel"].ToString() != null)
                        adviserVo.Associates = int.Parse(dr["A_IsAssociateModel"].ToString());
                    if (dr["A_Phone1STD"] != null && dr["A_Phone1STD"].ToString() != "")
                        adviserVo.Phone1Std = int.Parse(dr["A_Phone1STD"].ToString());
                    if (dr["A_Phone2STD"] != null && dr["A_Phone2STD"].ToString() != "")
                        adviserVo.Phone2Std = int.Parse(dr["A_Phone2STD"].ToString());
                    if (dr["A_Phone1ISD"] != null && dr["A_Phone1ISD"].ToString() != "")
                        adviserVo.Phone1Isd = int.Parse(dr["A_Phone1ISD"].ToString());
                    if (dr["A_Phone2ISD"] != null && dr["A_Phone2ISD"].ToString() != "")
                        adviserVo.Phone2Isd = int.Parse(dr["A_Phone2ISD"].ToString());
                    if (dr["A_Phone1Number"] != null && dr["A_Phone1Number"].ToString() != "")
                        adviserVo.Phone1Number = int.Parse(dr["A_Phone1Number"].ToString());
                    if (dr["A_Phone2Number"] != null && dr["A_Phone2Number"].ToString() != "")
                        adviserVo.Phone2Number = int.Parse(dr["A_Phone2Number"].ToString());
                    if (dr["A_PinCode"] != null && dr["A_PinCode"].ToString() != "")
                        adviserVo.PinCode = int.Parse(dr["A_PinCode"].ToString());
                    if (dr["A_IsActive"] != null && dr["A_IsActive"].ToString() != "")
                        adviserVo.IsActive = Int16.Parse(dr["A_IsActive"].ToString());
                    if (dr["A_ActivationDate"] != null && dr["A_ActivationDate"].ToString() != "")
                        adviserVo.ActivationDate = DateTime.Parse(dr["A_ActivationDate"].ToString());
                    if (dr["A_DeactivateDate"] != null && dr["A_DeactivateDate"].ToString() != "")
                        adviserVo.DeactivationDate = DateTime.Parse(dr["A_DeactivateDate"].ToString());
                    if (dr["XAC_AdviserCategory"] != null && dr["XAC_AdviserCategory"].ToString() != "")
                        adviserVo.Category = dr["XAC_AdviserCategory"].ToString();
                    adviserVo.LoginId = dr["U_LoginId"].ToString();
                    adviserVo.Password = dr["U_Password"].ToString();
                    adviserVo.AdvisorLOBVoList = GetAdvisorLOBs(adviserVo.advisorId);
                    adviserVoList.Add(adviserVo);
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserListWithPager()");
                object[] objects = new object[1];
                objects[0] = adviserVoList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            Count = int.Parse(getAdvisorDs.Tables[1].Rows[0]["CNT"].ToString());
            return adviserVoList;
        }
        public List<AdvisorVo> GetAdviserList()
        {
            List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
            //List<AdvisorLOBVo> advisorLOBVoList=new List<AdvisorLOBVo>();
            AdvisorVo adviserVo=new AdvisorVo();
            DataSet getAdvisorDs;
            DataTable dtAdvisers;
            Database db;
            DbCommand Cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");                
                Cmd = db.GetStoredProcCommand("SP_GetAllAdvisers");
                
                 getAdvisorDs = db.ExecuteDataSet(Cmd);
                 dtAdvisers=getAdvisorDs.Tables[0];
                 foreach (DataRow dr in dtAdvisers.Rows)
                 {
                     adviserVo = new AdvisorVo();

                     adviserVo.advisorId = int.Parse(dr["A_AdviserId"].ToString());
                     adviserVo.UserId = int.Parse(dr["U_UserId"].ToString());
                     adviserVo.BusinessCode = dr["XABT_BusinessTypeCode"].ToString();
                     adviserVo.OrganizationName = dr["A_OrgName"].ToString();
                     adviserVo.AddressLine1 = dr["A_AddressLine1"].ToString();
                     adviserVo.AddressLine2 = dr["A_AddressLine2"].ToString();
                     adviserVo.AddressLine3 = dr["A_AddressLine3"].ToString();
                     //advisorVo.BusinessCode = dr["BT_BusinessCode"].ToString();
                     adviserVo.City = dr["A_City"].ToString();
                     adviserVo.ContactPersonFirstName = dr["A_ContactPersonFirstName"].ToString();
                     adviserVo.ContactPersonLastName = dr["A_ContactPersonLastName"].ToString();
                     adviserVo.ContactPersonMiddleName = dr["A_ContactPersonMiddleName"].ToString();
                     adviserVo.Country = dr["A_Country"].ToString();
                     adviserVo.Email1 = dr["A_Email"].ToString();
                     adviserVo.Website = dr["A_Website"].ToString();
                     if (dr["A_Fax"] != null && dr["A_Fax"].ToString() != "")
                         adviserVo.Fax = int.Parse(dr["A_Fax"].ToString());
                     if (dr["A_FaxISD"] != null && dr["A_FaxISD"].ToString() != "")
                         adviserVo.FaxIsd = int.Parse(dr["A_FaxISD"].ToString());
                     if (dr["A_FaxSTD"] != null && dr["A_FaxSTD"].ToString() != "")
                         adviserVo.FaxStd = int.Parse(dr["A_FaxSTD"].ToString());
                     if (dr["A_ContactPersonMobile"] != null && dr["A_ContactPersonMobile"].ToString() != "")
                         adviserVo.MobileNumber = Convert.ToInt64(dr["A_ContactPersonMobile"].ToString());
                     if (dr["A_IsMultiBranch"].ToString() != "" && dr["A_IsMultiBranch"].ToString() != null)
                         adviserVo.MultiBranch = int.Parse(dr["A_IsMultiBranch"].ToString());
                     if (dr["A_IsAssociateModel"].ToString() != "" && dr["A_IsAssociateModel"].ToString() != null)
                         adviserVo.Associates = int.Parse(dr["A_IsAssociateModel"].ToString());
                     if (dr["A_Phone1STD"] != null && dr["A_Phone1STD"].ToString() != "")
                         adviserVo.Phone1Std = int.Parse(dr["A_Phone1STD"].ToString());
                     if (dr["A_Phone2STD"] != null && dr["A_Phone2STD"].ToString() != "")
                         adviserVo.Phone2Std = int.Parse(dr["A_Phone2STD"].ToString());
                     if (dr["A_Phone1ISD"] != null && dr["A_Phone1ISD"].ToString() != "")
                         adviserVo.Phone1Isd = int.Parse(dr["A_Phone1ISD"].ToString());
                     if (dr["A_Phone2ISD"] != null && dr["A_Phone2ISD"].ToString() != "")
                         adviserVo.Phone2Isd = int.Parse(dr["A_Phone2ISD"].ToString());
                     if (dr["A_Phone1Number"] != null && dr["A_Phone1Number"].ToString() != "")
                         adviserVo.Phone1Number = int.Parse(dr["A_Phone1Number"].ToString());
                     if (dr["A_Phone2Number"] != null && dr["A_Phone2Number"].ToString() != "")
                         adviserVo.Phone2Number = int.Parse(dr["A_Phone2Number"].ToString());
                     if (dr["A_PinCode"] != null && dr["A_PinCode"].ToString() != "")
                         adviserVo.PinCode = int.Parse(dr["A_PinCode"].ToString());
                     if (dr["A_IsActive"] != null && dr["A_IsActive"].ToString() != "")
                         adviserVo.IsActive = Int16.Parse(dr["A_IsActive"].ToString());
                     if (dr["A_ActivationDate"] != null && dr["A_ActivationDate"].ToString() != "")
                         adviserVo.ActivationDate = DateTime.Parse(dr["A_ActivationDate"].ToString());
                     if (dr["A_DeactivateDate"] != null && dr["A_DeactivateDate"].ToString() != "")
                         adviserVo.DeactivationDate = DateTime.Parse(dr["A_DeactivateDate"].ToString());
                     if (dr["XAC_AdviserCategory"] != null && dr["XAC_AdviserCategory"].ToString() != "")
                         adviserVo.Category = dr["XAC_AdviserCategory"].ToString();                     
                     adviserVo.LoginId = dr["U_LoginId"].ToString();
                     adviserVo.Password = dr["U_Password"].ToString();
                     adviserVo.AdvisorLOBVoList = GetAdvisorLOBs(adviserVo.advisorId);                     
                         adviserVoList.Add(adviserVo);
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdvisor()");
                object[] objects = new object[1];
                objects[0] = adviserVoList;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
           
            return adviserVoList;
        }
        public List<AdvisorLOBVo> GetAdvisorLOBs(int advisorId)
        {
            List<AdvisorLOBVo> advisorLOBList = new List<AdvisorLOBVo>();
            AdvisorLOBVo advisorLOBVo;
            Database db;
            DbCommand getAdvisorLOBCmd;
            DataSet getAdvisorLOBDs;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorLOBCmd = db.GetStoredProcCommand("SP_GetAdviserLOB");
                db.AddInParameter(getAdvisorLOBCmd, "@A_AdviserId", DbType.Int32, advisorId);

                getAdvisorLOBDs = db.ExecuteDataSet(getAdvisorLOBCmd);

                foreach (DataRow dr in getAdvisorLOBDs.Tables[0].Rows)
                {
                    advisorLOBVo = new AdvisorLOBVo();
                    advisorLOBVo.LOBId = int.Parse(dr["AL_LOBId"].ToString());
                    advisorLOBVo.OrganizationName = dr["AL_OrgName"].ToString();
                    advisorLOBVo.LicenseNumber = dr["AL_LicenseNo"].ToString();
                    advisorLOBVo.AdviserId = advisorId;//Int32.Parse(dr["A_AdviserId"].ToString());
                    if (dr["AL_Validity"].ToString() != string.Empty)
                        advisorLOBVo.ValidityDate = DateTime.Parse(dr["AL_Validity"].ToString());
                    advisorLOBVo.LOBClassificationCode = dr["XALC_LOBClassificationCode"].ToString();
                    advisorLOBVo.IdentifierTypeCode = dr["XALIT_IdentifierTypeCode"].ToString();
                    advisorLOBVo.Identifier = dr["AL_Identifier"].ToString();
                    advisorLOBVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                    advisorLOBVo.AgentType = dr["XALAT_AgentTypeCode"].ToString();
                    advisorLOBVo.AgentNum = dr["AL_AgentNo"].ToString();
                    if (dr["AL_TargetAccounts"].ToString() != string.Empty)
                        advisorLOBVo.TargetAccount = float.Parse(dr["AL_TargetAccounts"].ToString());
                    if (dr["AL_TargetAmount"].ToString() != string.Empty)
                        advisorLOBVo.TargetAmount = double.Parse(dr["AL_TargetAmount"].ToString());
                    if (dr["AL_TargetPremiumAmount"].ToString() != string.Empty)
                        advisorLOBVo.TargetPremiumAmount = double.Parse(dr["AL_TargetPremiumAmount"].ToString());
                    if (dr["AL_IsDependent"].ToString() != string.Empty)
                        advisorLOBVo.IsDependent = Int16.Parse(dr["AL_IsDependent"].ToString());
                    if (dr["XALAG_LOBAssetGroup"].ToString() != string.Empty)
                        advisorLOBVo.LOBClassificationType = dr["XALAG_LOBAssetGroup"].ToString();

                    advisorLOBList.Add(advisorLOBVo);

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

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:GetAdvisorLOBs()");


                object[] objects = new object[1];
                objects[0] = advisorId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorLOBList;

        }
        public void MessageBroadcastSendMessage(string BroadcastMessage,DateTime Broadcasttime)
        {
            Database dbMessageBroadcast;
            DbCommand CmdMessageBroadcast;
            try
            {
                dbMessageBroadcast = DatabaseFactory.CreateDatabase("wealtherp");

                CmdMessageBroadcast = dbMessageBroadcast.GetStoredProcCommand("SP_MessageBroadcastSendMessage");
                dbMessageBroadcast.AddInParameter(CmdMessageBroadcast, "@BroadcastMessage", DbType.String, BroadcastMessage);
                dbMessageBroadcast.AddInParameter(CmdMessageBroadcast, "@BroadcastDate", DbType.DateTime, Broadcasttime);
                dbMessageBroadcast.AddInParameter(CmdMessageBroadcast, "@IsActive", DbType.Int16, 1);
                dbMessageBroadcast.AddInParameter(CmdMessageBroadcast, "@Createdby", DbType.Int32, 10000);
                dbMessageBroadcast.AddInParameter(CmdMessageBroadcast, "@Modifiedby", DbType.Int32, 10000);
                dbMessageBroadcast.ExecuteNonQuery(CmdMessageBroadcast);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:MessageBroadcastSendMessage()");
                object[] objects = new object[1];
                objects[0] = BroadcastMessage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            
        }
        public void UpdateMessageBroadcast(int BroadcastMessageId, Int16 IsActive)
        {
            Database dbMessageBroadcast;
            DbCommand CmdMessageBroadcast;
            try
            {
                dbMessageBroadcast = DatabaseFactory.CreateDatabase("wealtherp");

                CmdMessageBroadcast = dbMessageBroadcast.GetStoredProcCommand("SP_UpdateMessageBroadcast");
                dbMessageBroadcast.AddInParameter(CmdMessageBroadcast, "@BroadcastMessage", DbType.Int32, BroadcastMessageId);
                dbMessageBroadcast.AddInParameter(CmdMessageBroadcast, "@BroadcastDate", DbType.Int16, IsActive);                
                CmdMessageBroadcast.ExecuteNonQuery();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:UpdateMessageBroadcast()");
                object[] objects = new object[1];
                objects[0] = BroadcastMessageId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        public DataSet GetMessageBroadcast()
        {
            DataSet dsMessageBroadcast;
            Database dbMessageBroadcast;
            DbCommand CmdMessageBroadcast;
            try
            {
                dbMessageBroadcast = DatabaseFactory.CreateDatabase("wealtherp");

                CmdMessageBroadcast = dbMessageBroadcast.GetStoredProcCommand("SP_GetMessageBroadcastMessage");
                dsMessageBroadcast=dbMessageBroadcast.ExecuteDataSet(CmdMessageBroadcast);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetMessageBroadcast()");
                object[] objects = new object[1];
                objects[0] = "Max of something";
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsMessageBroadcast;

        }
    }
}
