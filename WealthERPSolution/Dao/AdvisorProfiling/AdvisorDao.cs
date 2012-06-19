using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using VoUser;
using BoUser;
using VoEmailSMS;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections;
using VoSuperAdmin;



namespace DaoAdvisorProfiling
{
    public class AdvisorDao
    {

        /// <summary>
        /// New Adviser Registration Funtion
        /// </summary>
        ///<remarks>REWRITE THIS FUNCTION.</remarks>
        /// <param name="advisorVo"></param>
        /// <param name="rmVo"></param>
        /// <returns></returns>
        /// 
        public List<int> RegisterAdviser(UserVo userVo, AdvisorVo advisorVo, RMVo rmVo)
        {
            Database db;
            DbCommand createCompleteAdvisorCmd;
            int userId;
            int rmId;
            int adviserId;
            List<int> Ids = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCompleteAdvisorCmd = db.GetStoredProcCommand("SP_CreateCompleteAdviser");
                db.AddInParameter(createCompleteAdvisorCmd, "@U_Password", DbType.String, userVo.Password);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_PasswordSaltValue", DbType.String, userVo.PasswordSaltValue);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_FirstName ", DbType.String, userVo.FirstName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_Lastname", DbType.String, userVo.LastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_UserType", DbType.String, userVo.UserType);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_LoginId", DbType.String, userVo.LoginId);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_CreatedBy", DbType.Int32, 100);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_ModifiedBy", DbType.Int32, 100);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_OrgName", DbType.String, advisorVo.OrganizationName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine1", DbType.String, advisorVo.AddressLine1);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine2", DbType.String, advisorVo.AddressLine2);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine3", DbType.String, advisorVo.AddressLine3);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_City", DbType.String, advisorVo.City);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_State", DbType.String, advisorVo.State);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_PinCode", DbType.Int32, advisorVo.PinCode);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Country", DbType.String, advisorVo.Country);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1STD", DbType.Int32, advisorVo.Phone1Std);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1ISD", DbType.Int32, advisorVo.Phone1Isd);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1Number", DbType.Int32, advisorVo.Phone1Number);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2STD", DbType.Int32, advisorVo.Phone2Std);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2ISD", DbType.Int32, advisorVo.Phone2Isd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2Number", DbType.Int32, advisorVo.Phone2Number);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Email", DbType.String, advisorVo.Email);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_FAXISD", DbType.Int32, advisorVo.FaxIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_FAXSTD", DbType.Int32, advisorVo.FaxStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_FAX", DbType.Int32, advisorVo.Fax);
                //db.AddInParameter(createCompleteAdvisorCmd, "@XABT_BusinessTypeCode", DbType.String, advisorVo.BusinessCode);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonFirstName", DbType.String, advisorVo.ContactPersonFirstName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonMiddleName", DbType.String, advisorVo.ContactPersonMiddleName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonLastName", DbType.String, advisorVo.ContactPersonLastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonMobile", DbType.Int64, advisorVo.MobileNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsMultiBranch", DbType.Int32, advisorVo.MultiBranch);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsActive", DbType.Int16, advisorVo.IsActive);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ActivationDate", DbType.DateTime, advisorVo.ActivationDate);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_DeactivateDate", DbType.DateTime, advisorVo.DeactivationDate);
                db.AddInParameter(createCompleteAdvisorCmd, "@XAC_AdviserCategoryCode", DbType.String, advisorVo.Category);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsAssociateModel", DbType.Int32, advisorVo.Associates);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_ModifiedBy", DbType.Int32, 100);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_AdviserLogo", DbType.String, advisorVo.LogoPath);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_FirstName", DbType.String, rmVo.FirstName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_MiddleName", DbType.String, rmVo.MiddleName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_LastName", DbType.String, rmVo.LastName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirectISD", DbType.Int32, rmVo.OfficePhoneDirectIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirectSTD", DbType.Int32, rmVo.OfficePhoneDirectStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirect", DbType.Int32, rmVo.OfficePhoneDirectNumber);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExtISD", DbType.Int32, rmVo.OfficePhoneExtIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExtSTD", DbType.Int32, rmVo.OfficePhoneExtStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExt", DbType.Int32, rmVo.OfficePhoneExtNumber);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhoneISD", DbType.Int32, rmVo.ResPhoneIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhoneSTD", DbType.Int32, rmVo.ResPhoneStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhone", DbType.Int32, rmVo.ResPhoneNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_Mobile", DbType.Int64, rmVo.Mobile);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_FaxISD", DbType.Int32, rmVo.FaxIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_FaxSTD", DbType.Int32, rmVo.FaxStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_Fax", DbType.Int32, rmVo.Fax);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_Email", DbType.String, rmVo.Email);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_JobFunction", DbType.String, rmVo.RMRole);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_IsExternalStaff", DbType.Int16, rmVo.IsExternal);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_CTC", DbType.Double, 0);

                db.AddOutParameter(createCompleteAdvisorCmd, "@U_UserId", DbType.Int32, 5000);
                db.AddOutParameter(createCompleteAdvisorCmd, "@AR_RMId", DbType.Int32, 5000);
                db.AddOutParameter(createCompleteAdvisorCmd, "@A_AdviserId", DbType.Int32, 5000);
                if (db.ExecuteNonQuery(createCompleteAdvisorCmd) != 0)
                {
                    Ids = new List<int>();


                    userId = int.Parse(db.GetParameterValue(createCompleteAdvisorCmd, "U_UserId").ToString());
                    adviserId = int.Parse(db.GetParameterValue(createCompleteAdvisorCmd, "A_AdviserId").ToString());
                    rmId = int.Parse(db.GetParameterValue(createCompleteAdvisorCmd, "AR_RMId").ToString());

                    Ids.Add(userId);
                    Ids.Add(adviserId);
                    Ids.Add(rmId);
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:CreateCompleteAdviser()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = rmVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Ids;
        }

        public List<int> CreateCompleteAdviser(UserVo userVo, AdvisorVo advisorVo, RMVo rmVo)
        {
            Database db;
            DbCommand createCompleteAdvisorCmd;
            int userId;
            int rmId;
            int adviserId;
            List<int> Ids = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCompleteAdvisorCmd = db.GetStoredProcCommand("SP_CreateCompleteAdviser");
                db.AddInParameter(createCompleteAdvisorCmd, "@U_Password", DbType.String, userVo.Password);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_FirstName ", DbType.String, userVo.FirstName);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_Lastname", DbType.String, userVo.LastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_UserType", DbType.String, userVo.UserType);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_LoginId", DbType.String, userVo.LoginId);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_CreatedBy", DbType.Int32, 100);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_ModifiedBy", DbType.Int32, 100);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_OrgName", DbType.String, advisorVo.OrganizationName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine1", DbType.String, advisorVo.AddressLine1);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine2", DbType.String, advisorVo.AddressLine2);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine3", DbType.String, advisorVo.AddressLine3);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_City", DbType.String, advisorVo.City);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_State", DbType.String, advisorVo.State);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_PinCode", DbType.Int32, advisorVo.PinCode);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Country", DbType.String, advisorVo.Country);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1STD", DbType.Int32, advisorVo.Phone1Std);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1ISD", DbType.Int32, advisorVo.Phone1Isd);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1Number", DbType.Int32, advisorVo.Phone1Number);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2STD", DbType.Int32, advisorVo.Phone2Std);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2ISD", DbType.Int32, advisorVo.Phone2Isd);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2Number", DbType.Int32, advisorVo.Phone2Number);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Email", DbType.String, advisorVo.Email);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Website", DbType.String, advisorVo.Website);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_FAXISD", DbType.Int32, advisorVo.FaxIsd);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_FAXSTD", DbType.Int32, advisorVo.FaxStd);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_FAX", DbType.Int32, advisorVo.Fax);
                db.AddInParameter(createCompleteAdvisorCmd, "@XABT_BusinessTypeCode", DbType.String, advisorVo.BusinessCode);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonFirstName", DbType.String, advisorVo.ContactPersonFirstName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonMiddleName", DbType.String, advisorVo.ContactPersonMiddleName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonLastName", DbType.String, advisorVo.ContactPersonLastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonMobile", DbType.Int64, advisorVo.MobileNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsMultiBranch", DbType.Int32, advisorVo.MultiBranch);

                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsAssociateModel", DbType.Int32, advisorVo.Associates);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_ModifiedBy", DbType.Int32, 100);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AdviserLogo", DbType.String, advisorVo.LogoPath);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_FirstName", DbType.String, rmVo.FirstName);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_MiddleName", DbType.String, rmVo.MiddleName);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_LastName", DbType.String, rmVo.LastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirectISD", DbType.Int32, rmVo.OfficePhoneDirectIsd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirectSTD", DbType.Int32, rmVo.OfficePhoneDirectStd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirect", DbType.Int32, rmVo.OfficePhoneDirectNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExtISD", DbType.Int32, rmVo.OfficePhoneExtIsd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExtSTD", DbType.Int32, rmVo.OfficePhoneExtStd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExt", DbType.Int32, rmVo.OfficePhoneExtNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhoneISD", DbType.Int32, rmVo.ResPhoneIsd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhoneSTD", DbType.Int32, rmVo.ResPhoneStd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhone", DbType.Int32, rmVo.ResPhoneNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_Mobile", DbType.Int64, rmVo.Mobile);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_FaxISD", DbType.Int32, rmVo.FaxIsd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_FaxSTD", DbType.Int32, rmVo.FaxStd);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_Fax", DbType.Int32, rmVo.Fax);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_Email", DbType.String, rmVo.Email);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_JobFunction", DbType.String, rmVo.RMRole);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_IsExternalStaff", DbType.Int16, rmVo.IsExternal);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_CTC", DbType.Double, 0);

                db.AddOutParameter(createCompleteAdvisorCmd, "@U_UserId", DbType.Int32, 5000);
                db.AddOutParameter(createCompleteAdvisorCmd, "@AR_RMId", DbType.Int32, 5000);
                db.AddOutParameter(createCompleteAdvisorCmd, "@A_AdviserId", DbType.Int32, 5000);
                if (db.ExecuteNonQuery(createCompleteAdvisorCmd) != 0)
                {
                    Ids = new List<int>();


                    userId = int.Parse(db.GetParameterValue(createCompleteAdvisorCmd, "U_UserId").ToString());
                    adviserId = int.Parse(db.GetParameterValue(createCompleteAdvisorCmd, "A_AdviserId").ToString());
                    rmId = int.Parse(db.GetParameterValue(createCompleteAdvisorCmd, "AR_RMId").ToString());

                    Ids.Add(userId);
                    Ids.Add(adviserId);
                    Ids.Add(rmId);
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:CreateCompleteAdviser()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = rmVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Ids;
        }

        public List<CustomerVo> GetAdviserCustomersForSMS(int adviserId, int rmId, string namefilter)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo = new CustomerVo();
            Database db;
            DbCommand getAdviserCustomersForSMSCmd;
            DataSet getCustomerDs;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserCustomersForSMSCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerForSMS");
                if (adviserId != 0)
                    db.AddInParameter(getAdviserCustomersForSMSCmd, "@A_AdviserId", DbType.Int32, adviserId);
                else
                    db.AddInParameter(getAdviserCustomersForSMSCmd, "@A_AdviserId", DbType.Int32, DBNull.Value);

                if (rmId != 0)
                    db.AddInParameter(getAdviserCustomersForSMSCmd, "@rmID", DbType.Int32, rmId);
                else
                    db.AddInParameter(getAdviserCustomersForSMSCmd, "@rmID", DbType.Int32, DBNull.Value);

                if (namefilter != "")
                    db.AddInParameter(getAdviserCustomersForSMSCmd, "@namefilter", DbType.String, namefilter);
                else
                    db.AddInParameter(getAdviserCustomersForSMSCmd, "@namefilter", DbType.String, DBNull.Value);

                getCustomerDs = db.ExecuteDataSet(getAdviserCustomersForSMSCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<CustomerVo>();
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();

                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["NAME"].ToString();
                        customerVo.Mobile1 = Int64.Parse(dr["Mobile"].ToString());

                        customerList.Add(customerVo);
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

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomersForSMS(int adviserId, string namefilter)");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerList;
        }

        public int CreateAdvisor(AdvisorVo advisorVo)
        {
            int advisorId = 0;

            Database db;
            DbCommand createAdvisorCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdvisorCmd = db.GetStoredProcCommand("SP_CreateAdviser");
                db.AddInParameter(createAdvisorCmd, "@U_UserId", DbType.Int32, advisorVo.UserId);
                db.AddInParameter(createAdvisorCmd, "@A_OrgName", DbType.String, advisorVo.OrganizationName);
                db.AddInParameter(createAdvisorCmd, "@A_AddressLine1", DbType.String, advisorVo.AddressLine1);
                db.AddInParameter(createAdvisorCmd, "@A_AddressLine2", DbType.String, advisorVo.AddressLine2);
                db.AddInParameter(createAdvisorCmd, "@A_AddressLine3", DbType.String, advisorVo.AddressLine3);
                db.AddInParameter(createAdvisorCmd, "@A_City", DbType.String, advisorVo.City);
                db.AddInParameter(createAdvisorCmd, "@A_State", DbType.String, advisorVo.State);
                db.AddInParameter(createAdvisorCmd, "@A_PinCode", DbType.Int32, advisorVo.PinCode);
                db.AddInParameter(createAdvisorCmd, "@A_Country", DbType.String, advisorVo.Country);
                db.AddInParameter(createAdvisorCmd, "@A_Phone1ISD", DbType.Int32, advisorVo.Phone1Isd);
                db.AddInParameter(createAdvisorCmd, "@A_Phone1STD", DbType.Int32, advisorVo.Phone1Std);
                db.AddInParameter(createAdvisorCmd, "@A_Phone1Number", DbType.Int32, advisorVo.Phone1Number);
                db.AddInParameter(createAdvisorCmd, "@A_Phone2ISD", DbType.Int32, advisorVo.Phone2Isd);
                db.AddInParameter(createAdvisorCmd, "@A_Phone2STD", DbType.Int32, advisorVo.Phone2Std);
                db.AddInParameter(createAdvisorCmd, "@A_Phone2Number", DbType.Int32, advisorVo.Phone2Number);
                db.AddInParameter(createAdvisorCmd, "@A_FAXISD", DbType.Int32, advisorVo.FaxIsd);
                db.AddInParameter(createAdvisorCmd, "@A_FAXSTD", DbType.Int32, advisorVo.FaxStd);
                db.AddInParameter(createAdvisorCmd, "@A_FAX", DbType.Int32, advisorVo.Fax);
                db.AddInParameter(createAdvisorCmd, "@A_ContactPersonMobile", DbType.Int64, advisorVo.MobileNumber);
                db.AddInParameter(createAdvisorCmd, "@A_Email", DbType.String, advisorVo.Email);
                db.AddInParameter(createAdvisorCmd, "@A_Website", DbType.String, advisorVo.Website);
                db.AddInParameter(createAdvisorCmd, "@A_ContactPersonFirstName", DbType.String, advisorVo.ContactPersonFirstName);
                db.AddInParameter(createAdvisorCmd, "@A_ContactPersonMiddleName", DbType.String, advisorVo.ContactPersonMiddleName);
                db.AddInParameter(createAdvisorCmd, "@A_ContactPersonLastName", DbType.String, advisorVo.ContactPersonLastName);
                db.AddInParameter(createAdvisorCmd, "@A_IsMultiBranch", DbType.Int32, advisorVo.MultiBranch);
                db.AddInParameter(createAdvisorCmd, "@A_IsAssociateModel", DbType.Int32, advisorVo.Associates);
                db.AddInParameter(createAdvisorCmd, "@XABT_BusinessTypeCode", DbType.String, advisorVo.BusinessCode);
                db.AddInParameter(createAdvisorCmd, "@A_CreatedBy", DbType.Int32, 100);
                db.AddInParameter(createAdvisorCmd, "@A_ModifiedBy", DbType.Int32, 100);
                db.AddInParameter(createAdvisorCmd, "@A_AdviserLogo", DbType.String, advisorVo.LogoPath);
                db.AddOutParameter(createAdvisorCmd, "@AdviserId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createAdvisorCmd) != 0)

                    advisorId = int.Parse(db.GetParameterValue(createAdvisorCmd, "AdviserId").ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:CreateAdvisor()");


                object[] objects = new object[1];
                objects[0] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorId;
        }

        public AdvisorVo GetAdvisor(int advisorId)
        {
            AdvisorVo advisorVo = new AdvisorVo();
            Database db;
            DbCommand getAdvisorCmd;
            DataSet getAdvisorDs;
            DataTable table;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorCmd = db.GetStoredProcCommand("SP_GetAdviser");
                db.AddInParameter(getAdvisorCmd, "@A_AdviserId", DbType.Int32, advisorId);
                getAdvisorDs = db.ExecuteDataSet(getAdvisorCmd);
                if (getAdvisorDs.Tables[0].Rows.Count > 0)
                {
                    table = getAdvisorDs.Tables["Adviser"];
                    dr = getAdvisorDs.Tables[0].Rows[0];
                    advisorVo.advisorId = int.Parse(dr["A_AdviserId"].ToString());
                    advisorVo.BusinessCode = dr["XABT_BusinessTypeCode"].ToString();
                    advisorVo.OrganizationName = dr["A_OrgName"].ToString();
                    advisorVo.AddressLine1 = dr["A_AddressLine1"].ToString();
                    advisorVo.AddressLine2 = dr["A_AddressLine2"].ToString();
                    advisorVo.AddressLine3 = dr["A_AddressLine3"].ToString();
                    //advisorVo.BusinessCode = dr["BT_BusinessCode"].ToString();
                    advisorVo.City = dr["A_City"].ToString();
                    advisorVo.ContactPersonFirstName = dr["A_ContactPersonFirstName"].ToString();
                    advisorVo.ContactPersonLastName = dr["A_ContactPersonLastName"].ToString();
                    advisorVo.ContactPersonMiddleName = dr["A_ContactPersonMiddleName"].ToString();
                    advisorVo.Country = dr["A_Country"].ToString();
                    advisorVo.Email = dr["A_Email"].ToString();
                    advisorVo.Website = dr["A_Website"].ToString();
                    if (dr["A_Fax"] != null && dr["A_Fax"].ToString() != "")
                        advisorVo.Fax = int.Parse(dr["A_Fax"].ToString());
                    if (dr["A_FaxISD"] != null && dr["A_FaxISD"].ToString() != "")
                        advisorVo.FaxIsd = int.Parse(dr["A_FaxISD"].ToString());
                    if (dr["A_FaxSTD"] != null && dr["A_FaxSTD"].ToString() != "")
                        advisorVo.FaxStd = int.Parse(dr["A_FaxSTD"].ToString());
                    if (dr["A_ContactPersonMobile"] != null && dr["A_ContactPersonMobile"].ToString() != "")
                        advisorVo.MobileNumber = Convert.ToInt64(dr["A_ContactPersonMobile"].ToString());
                    if(dr["A_IsMultiBranch"].ToString()!="" && dr["A_IsMultiBranch"].ToString()!=null)
                        advisorVo.MultiBranch = int.Parse(dr["A_IsMultiBranch"].ToString());
                    if (dr["A_IsAssociateModel"].ToString() != "" && dr["A_IsAssociateModel"].ToString() != null)
                        advisorVo.Associates = int.Parse(dr["A_IsAssociateModel"].ToString());
                    if (dr["A_Phone1STD"] != null && dr["A_Phone1STD"].ToString() != "")
                        advisorVo.Phone1Std = int.Parse(dr["A_Phone1STD"].ToString());
                    if (dr["A_Phone2STD"] != null && dr["A_Phone2STD"].ToString() != "")
                    advisorVo.Phone2Std = int.Parse(dr["A_Phone2STD"].ToString());

                    if (dr["A_VaultSize(in MB)"] != null && dr["A_VaultSize(in MB)"].ToString() != "")
                        advisorVo.VaultSize = float.Parse(dr["A_VaultSize(in MB)"].ToString());

                    if (dr["A_Phone1ISD"] != null && dr["A_Phone1ISD"].ToString() != "")
                    advisorVo.Phone1Isd = int.Parse(dr["A_Phone1ISD"].ToString());
                    if (dr["A_Phone2ISD"] != null && dr["A_Phone2ISD"].ToString() != "")
                    advisorVo.Phone2Isd = int.Parse(dr["A_Phone2ISD"].ToString());
                    if (dr["A_Phone1Number"] != null && dr["A_Phone1Number"].ToString() != "")
                    advisorVo.Phone1Number = int.Parse(dr["A_Phone1Number"].ToString());
                    if (dr["A_Phone2Number"] != null && dr["A_Phone2Number"].ToString() != "")
                    advisorVo.Phone2Number = int.Parse(dr["A_Phone2Number"].ToString());
                    if (dr["A_PinCode"] != null && dr["A_PinCode"].ToString() != "")
                    advisorVo.PinCode = int.Parse(dr["A_PinCode"].ToString());
                    if (dr["A_AdviserLogo"] != DBNull.Value)
                        advisorVo.LogoPath = dr["A_AdviserLogo"].ToString();
                    if (!string.IsNullOrEmpty(dr["A_IsActive"].ToString().Trim()))
                        advisorVo.IsActive = Convert.ToInt16(dr["A_IsActive"].ToString());
                    if (!string.IsNullOrEmpty(dr["U_Theme"].ToString().Trim()))
                        advisorVo.theme = Convert.ToString(dr["U_Theme"]).Trim();

                    advisorVo.SubscriptionVo.AdviserId = int.Parse(dr["A_AdviserId"].ToString());

                    if (dr["AS_AdviserSubscriptionId"] != DBNull.Value && dr["AS_AdviserSubscriptionId"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.SubscriptionId = int.Parse(dr["AS_AdviserSubscriptionId"].ToString());

                    if (dr["AS_TrialStartDate"] != DBNull.Value && dr["AS_TrialStartDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.TrialStartDate = DateTime.Parse(dr["AS_TrialStartDate"].ToString());

                    if (dr["AS_TrialEndDate"] != DBNull.Value && dr["AS_TrialEndDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.TrialEndDate = DateTime.Parse(dr["AS_TrialEndDate"].ToString());

                    if (dr["AS_SubscriptionStartDate"] != DBNull.Value && dr["AS_SubscriptionStartDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.StartDate = DateTime.Parse(dr["AS_SubscriptionStartDate"].ToString());

                    if (dr["AS_SubscriptionEndDate"] != DBNull.Value && dr["AS_SubscriptionEndDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.EndDate = DateTime.Parse(dr["AS_SubscriptionEndDate"].ToString());

                    if (dr["AS_SMSLicences"] != DBNull.Value && dr["AS_SMSLicences"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.SmsBought = int.Parse(dr["AS_SMSLicences"].ToString());

                    if (dr["AS_StorageSize"] != DBNull.Value && dr["AS_StorageSize"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.StorageSize = float.Parse(dr["AS_StorageSize"].ToString());

                    if (dr["AS_StorageBalance"] != DBNull.Value && dr["AS_StorageBalance"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.StorageBalance = float.Parse(dr["AS_StorageBalance"].ToString());

                    if (dr["AS_NoOfStaffWebLogins"] != DBNull.Value && dr["AS_NoOfStaffWebLogins"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.NoOfStaffLogins = int.Parse(dr["AS_NoOfStaffWebLogins"].ToString());

                    if (dr["AS_NoOfBranches"] != DBNull.Value && dr["AS_NoOfBranches"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.NoOfBranches = int.Parse(dr["AS_NoOfBranches"].ToString());

                    if (dr["AS_NoOfCustomerWebLogins"] != DBNull.Value && dr["AS_NoOfCustomerWebLogins"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.NoOfCustomerLogins = int.Parse(dr["AS_NoOfCustomerWebLogins"].ToString());

                    if (dr["AS_IsDeactivated"] != DBNull.Value && dr["AS_IsDeactivated"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.IsDeActivated = int.Parse(dr["AS_IsDeactivated"].ToString());

                    if (dr["AS_DeactivationDate"] != DBNull.Value && dr["AS_DeactivationDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.DeActivationDate = DateTime.Parse(dr["AS_DeactivationDate"].ToString());

                    if (dr["AS_Comments"] != DBNull.Value && dr["AS_Comments"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.Comments = dr["AS_Comments"].ToString();

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
                objects[0] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return advisorVo;
        }

        public DataSet GetAdviserSubscriptionDetails(int adviserId)
        {
            Database db;
            DbCommand GetAdviserSubscriptionDetailsCmd;
            DataSet dsAdviserSubscriptionDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserSubscriptionDetailsCmd = db.GetStoredProcCommand("SP_GetAdviserSubscriptionDetails");
                db.AddInParameter(GetAdviserSubscriptionDetailsCmd, "@A_AdviserId", DbType.Int32, adviserId);

                dsAdviserSubscriptionDetails = db.ExecuteDataSet(GetAdviserSubscriptionDetailsCmd);            


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserSibscriptionDetails(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserSubscriptionDetails;
        
        }

        public bool AddToAdviserSMSLog(SMSVo smsVo, int adviserId, string smsType)
        {
            bool bResult = false;
            Database db;
            DbCommand AddToAdviserSMSLogCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AddToAdviserSMSLogCmd = db.GetStoredProcCommand("SP_AddToAdviserSMSLog");
                db.AddInParameter(AddToAdviserSMSLogCmd, "@ASML_SMSQueueId", DbType.Int32, smsVo.SMSId);
                if (smsVo.AlertId != 0)
                {
                    db.AddInParameter(AddToAdviserSMSLogCmd, "@AEN_EventQueueID", DbType.Int32, smsVo.AlertId);
                }
                else
                {
                    db.AddInParameter(AddToAdviserSMSLogCmd, "@AEN_EventQueueID", DbType.Int32, 0);
                }
                db.AddInParameter(AddToAdviserSMSLogCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(AddToAdviserSMSLogCmd, "@ASML_SMSType", DbType.String, smsType);
                db.AddInParameter(AddToAdviserSMSLogCmd, "@C_CustomerId", DbType.Int32, smsVo.CustomerId);
                db.AddInParameter(AddToAdviserSMSLogCmd, "@ASML_Status", DbType.Int32, 1);

                db.ExecuteNonQuery(AddToAdviserSMSLogCmd);
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

                FunctionInfo.Add("Method", "AdvisorDao.cs:AddToAdviserSMSLog(SMSVo smsVo, int adviserId, string smsType)");


                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = smsVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool UpdateAdviserSMSLicence(int adviserId, int smsLincence)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateAdviserSMSLicenceCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAdviserSMSLicenceCmd = db.GetStoredProcCommand("SP_UpdateAdviserSMSLicence");
                db.AddInParameter(UpdateAdviserSMSLicenceCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(UpdateAdviserSMSLicenceCmd, "@AS_SMSLicenece", DbType.Int32, smsLincence);

                db.ExecuteNonQuery(UpdateAdviserSMSLicenceCmd);
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

                FunctionInfo.Add("Method", "AdvisorDao.cs:UpdateAdviserSMSLicence(int adviserId, int smsLincence)");


                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = smsLincence;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public int GetRMAdviserId(int rmId)
        {
            int adviserId;
            Database db;
            DbCommand GetRMAdviserIdCmd;
            DataSet ds;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetRMAdviserIdCmd = db.GetStoredProcCommand("SP_GetRMAdviserId");
                db.AddInParameter(GetRMAdviserIdCmd, "@AR_RMId", DbType.Int32, rmId);

                ds = db.ExecuteDataSet(GetRMAdviserIdCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    adviserId = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                    adviserId = 0;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetRMAdviserId()");
                object[] objects = new object[1];
                objects[0] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return adviserId;
        }

        public List<CustomerVo> FindCustomer(string CustomerName, int advisorId, int CurrentPage, out int Count, string SortExpression, string NameFilter, string AreaFilter, string PincodeFilter, string ParentFilter, string RMFilter, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM)
        {
            List<CustomerVo> customerList;
            Database db;
            DbCommand FindCustomerCmd;
            DataSet ds;
            CustomerVo customerVo;

            genDictParent = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();
            genDictReassignRM = new Dictionary<string, string>();

            Count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                FindCustomerCmd = db.GetStoredProcCommand("SP_FindAdviserCustomer");
                db.AddInParameter(FindCustomerCmd, "@C_FirstName", DbType.String, CustomerName);
                db.AddInParameter(FindCustomerCmd, "@A_AdviserId", DbType.Int32, advisorId);
                db.AddInParameter(FindCustomerCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(FindCustomerCmd, "@SortOrder", DbType.String, SortExpression);

                if (NameFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@nameFilter", DbType.String, NameFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (AreaFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@areaFilter", DbType.String, AreaFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@areaFilter", DbType.String, DBNull.Value);
                if (PincodeFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@pincodeFilter", DbType.String, PincodeFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@pincodeFilter", DbType.String, DBNull.Value);
                if (ParentFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@parentFilter", DbType.String, ParentFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@parentFilter", DbType.String, DBNull.Value);
                if (RMFilter != "")
                    db.AddInParameter(FindCustomerCmd, "@rmFilter", DbType.String, RMFilter);
                else
                    db.AddInParameter(FindCustomerCmd, "@rmFilter", DbType.String, DBNull.Value);

                ds = db.ExecuteDataSet(FindCustomerCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<CustomerVo>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();

                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        if (dr["RMName"].ToString() != "")
                            customerVo.AssignedRM = dr["RMName"].ToString();
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;

                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        genDictParent.Add(dr["Parent"].ToString(), dr["Parent"].ToString());
                    }
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        genDictRM.Add(dr["RMName"].ToString(), dr["RMName"].ToString());
                    }
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        genDictReassignRM.Add(dr["RMName"].ToString(), dr["RMId"].ToString());
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:FindCustomer()");
                object[] objects = new object[2];
                objects[0] = CustomerName;
                objects[1] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (ds.Tables[1].Rows.Count > 0)
                Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());

            return customerList;
        }

        public int GetAdviserCustomerList(int adviserId, string Flag)
        {
            Database db;
            DbCommand getAdviserCustomerListCmd;
            DataSet getAdviserCustomerDs = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserCustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerList");
                db.AddInParameter(getAdviserCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(getAdviserCustomerListCmd, "@Flag", DbType.String, Flag);
                getAdviserCustomerDs = db.ExecuteDataSet(getAdviserCustomerListCmd);
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorStaffDao.cs:GetAdviserCustomerList()");


                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = Flag;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Convert.ToInt32(getAdviserCustomerDs.Tables[0].Rows[0][0].ToString());
        }

        public DataSet GetAdviserCustomerListDataSet(int adviserId)
        {
            Database db;
            DbCommand CustomerListCmd;
            DataSet dsCustomerList = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerListDataSet");
                db.AddInParameter(CustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);
                dsCustomerList = db.ExecuteDataSet(CustomerListCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerListDataSet()");

                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsCustomerList;

        }

        public List<CustomerVo> GetAdviserAllCustomerList(int adviserId)
        {
            List<CustomerVo> customerList = null;
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserAllCustomerList");
                db.AddInParameter(getCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);

                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<CustomerVo>();
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();

                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        //  customerVo.CompanyName = dr["C_CompanyName"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        if (dr["RMName"] != "")
                            customerVo.AssignedRM = dr["RMName"].ToString();
                        customerList.Add(customerVo);

                    }
                }
                else
                    customerList = null;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserAllCustomer()");


                object[] objects = new object[4];
                objects[0] = adviserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerList;
        }

        //public List<CustomerVo> GetAdviserCustomerList(int adviserId, int CurrentPage, out int Count, string SortExpression, string NameFilter, string AreaFilter, string PincodeFilter, string ParentFilter, string RMFilter, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM)

        public List<CustomerVo> GetAdviserCustomerList(int adviserId, int CurrentPage, out int Count, string SortExpression, string panFilter, string NameFilter, string AreaFilter, string PincodeFilter, string ParentFilter, string RMFilter, string Active, string isProspect, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM)
        {
            List<CustomerVo> customerList = null;
            CustomerVo customerVo;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;

            genDictParent = new Dictionary<string, string>();
            genDictRM = new Dictionary<string, string>();
            genDictReassignRM = new Dictionary<string, string>();
            Count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerList");
                db.AddInParameter(getCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(getCustomerListCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                if (SortExpression != "")
                    db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, SortExpression);
                else
                    db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, DBNull.Value);

                if (NameFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, NameFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (AreaFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, AreaFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@areaFilter", DbType.String, DBNull.Value);
                if (PincodeFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@pincodeFilter", DbType.String, PincodeFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@pincodeFilter", DbType.String, DBNull.Value);
                if (ParentFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@parentFilter", DbType.String, ParentFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@parentFilter", DbType.String, DBNull.Value);
                if (RMFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, RMFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@rmFilter", DbType.String, DBNull.Value);

                if (Active != "")
                    db.AddInParameter(getCustomerListCmd, "@active", DbType.String, Active);
                else
                    db.AddInParameter(getCustomerListCmd, "@active", DbType.String, "D");

                if (isProspect != "")
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, isProspect);
                else
                    db.AddInParameter(getCustomerListCmd, "@IsProspect", DbType.String, "2");

                if (panFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, panFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, DBNull.Value);
                getCustomerListCmd.CommandTimeout = 60 * 60;
                getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);

                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<CustomerVo>();
                    foreach (DataRow dr in getCustomerDs.Tables[0].Rows)
                    {
                        customerVo = new CustomerVo();

                        customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                        customerVo.LastName = dr["C_LastName"].ToString();
                        customerVo.CustCode = dr["C_CustCode"].ToString();
                        if (dr["C_PANNum"].ToString() != string.Empty)
                            customerVo.PANNum = dr["C_PANNum"].ToString();
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                        customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                        customerVo.Email = dr["C_Email"].ToString();
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                        if (!string.IsNullOrEmpty(dr["C_IsActive"].ToString().Trim()))
                         customerVo.IsActive = int.Parse(dr["C_IsActive"].ToString());
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerVo.IsFPClient = int.Parse(dr["C_IsFPClient"].ToString());
                        customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                        if (dr["Parent"].ToString() != "")
                            customerVo.ParentCustomer = dr["Parent"].ToString();
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                        if (dr["C_Mobile1"].ToString() != "")
                            customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                        if (dr["RMName"].ToString() != "")
                            customerVo.AssignedRM = dr["RMName"].ToString();
                        if (dr["C_IsProspect"].ToString() != "")
                            customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                        customerList.Add(customerVo);
                    }
                }
                else
                    customerList = null;

                if (getCustomerDs.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[2].Rows)
                    {
                        genDictParent.Add(dr["CustomerId"].ToString(), dr["Parent"].ToString());
                    }
                }

                if (getCustomerDs.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[3].Rows)
                    {
                        if (dr["RMName"].ToString().Trim() != "")
                        {
                            genDictRM.Add(dr["RMId"].ToString(), dr["RMName"].ToString());
                        }
                    }
                }
                if (getCustomerDs.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in getCustomerDs.Tables[4].Rows)
                    {
                        genDictReassignRM.Add(dr["RMId"].ToString(), dr["RMName"].ToString());
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

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerList()");

                object[] objects = new object[11];
                objects[0] = adviserId;
                objects[1] = CurrentPage;
                objects[2] = Count;
                objects[3] = SortExpression;
                objects[4] = NameFilter;
                objects[5] = AreaFilter;
                objects[6] = PincodeFilter;
                objects[7] = ParentFilter;
                objects[8] = RMFilter;
                objects[9] = genDictParent;
                objects[10] = genDictRM;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            if (getCustomerDs.Tables[1].Rows.Count > 0)
                Count = Int32.Parse(getCustomerDs.Tables[1].Rows[0]["CNT"].ToString());

            return customerList;
        }

        public List<int> GetAdviserCustomer(int adviserId, int currentPage, string sortOrder)
        {
            List<int> customerList = null;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet ds;

            try
            {
                customerList = new List<int>();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerList");
                db.AddInParameter(getCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(getCustomerListCmd, "@CurrentPage", DbType.Int32, currentPage);
                db.AddInParameter(getCustomerListCmd, "@SortOrder", DbType.String, sortOrder);

                ds = db.ExecuteDataSet(getCustomerListCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int customerId = int.Parse(dr["C_CustomerId"].ToString());

                        customerList.Add(customerId);
                    }
                }
                else
                    customerList = null;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomer()");
                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = currentPage;
                objects[2] = sortOrder;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }

        public bool CreateAdvisorUser(AdvisorVo advisorVo, int userId, string password)
        {
            bool bResult = false;
            UserVo userVo = new UserVo();
            UserBo userBo = new UserBo();


            try
            {
                userVo.Email = advisorVo.Email.ToString();
                userVo.FirstName = advisorVo.ContactPersonFirstName.ToString();
                userVo.LastName = advisorVo.ContactPersonLastName.ToString();
                userVo.MiddleName = advisorVo.ContactPersonMiddleName.ToString();
                userVo.Password = "123";
                userVo.UserType = "Advisor";
                userBo.CreateUser(userVo);


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

                FunctionInfo.Add("Method", "AdvisorDao.cs:CreateAdvisorUser()");


                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = userId;
                objects[2] = password;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public string GetAdvisorUsername(int advisorId)
        {
            string userName = "";
            Database db;
            DbCommand getUserNameCmd;

            string query = "";
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                query = "select A_Email from Adviser where A_AdviserId=" + advisorId.ToString() + "";

                getUserNameCmd = db.GetSqlStringCommand(query);
                userName = db.ExecuteScalar(getUserNameCmd).ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "AdvisorDao.cs:getAdvisorUsername()");


                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return userName;
        }

        public UserVo GetAdvisorUserInfo(int advisorId)
        {
            UserVo userVo = null;
            Database db;
            DbCommand getAdvisorUserInfoCmd;
            DataSet getAdvisorUserInfoDs;
            DataTable table;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorUserInfoCmd = db.GetStoredProcCommand("SP_GetAdviserUserInfo");
                getAdvisorUserInfoDs = db.ExecuteDataSet(getAdvisorUserInfoCmd);

                if (getAdvisorUserInfoDs.Tables[0].Rows.Count > 0)
                {
                    userVo = new UserVo();
                    table = getAdvisorUserInfoDs.Tables["User"];
                    dr = table.NewRow();
                    userVo.UserId = int.Parse(dr["U_UserId"].ToString());
                    userVo.FirstName = dr["U_FirstName"].ToString();
                    userVo.MiddleName = dr["U_MiddleName"].ToString();
                    userVo.LastName = dr["U_LastName"].ToString();
                    userVo.Password = dr["U_Password"].ToString();
                    userVo.Email = dr["U_Email"].ToString();
                    userVo.UserType = dr["U_UserType"].ToString();
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

                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdvisorUserInfo()");


                object[] objects = new object[1];
                objects[0] = advisorId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return userVo;
        }

        public AdvisorVo GetAdvisorUser(int userId)
        {

            AdvisorVo advisorVo = null;

            Database db;
            DbCommand getAdvisorUserCmd;
            DataSet getAdvisorUserDs;
            DataTable table;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdvisorUserCmd = db.GetStoredProcCommand("SP_GetAdviserUser");
                db.AddInParameter(getAdvisorUserCmd, "@U_UserId", DbType.Int32, userId);
                getAdvisorUserDs = db.ExecuteDataSet(getAdvisorUserCmd);
                if (getAdvisorUserDs.Tables[0].Rows.Count > 0)
                {
                    advisorVo = new AdvisorVo();
                    table = getAdvisorUserDs.Tables["Adviser"];
                    dr = getAdvisorUserDs.Tables[0].Rows[0];
                    advisorVo.advisorId = int.Parse(dr["A_AdviserId"].ToString());
                    if (dr["XABT_BusinessTypeCode"] != DBNull.Value)
                        advisorVo.BusinessCode = dr["XABT_BusinessTypeCode"].ToString();

                    advisorVo.OrganizationName = dr["A_OrgName"].ToString();
                    if (dr["A_AddressLine1"] != DBNull.Value)
                        advisorVo.AddressLine1 = dr["A_AddressLine1"].ToString();
                    if (dr["A_AddressLine2"] != DBNull.Value)
                        advisorVo.AddressLine2 = dr["A_AddressLine2"].ToString();
                    if (dr["A_AddressLine3"] != DBNull.Value)
                        advisorVo.AddressLine3 = dr["A_AddressLine3"].ToString();

                    advisorVo.City = dr["A_City"].ToString();
                    if (dr["A_ContactPersonFirstName"] != DBNull.Value)
                        advisorVo.ContactPersonFirstName = dr["A_ContactPersonFirstName"].ToString();
                    if (dr["A_ContactPersonLastName"] != DBNull.Value)
                        advisorVo.ContactPersonLastName = dr["A_ContactPersonLastName"].ToString();
                    if (dr["A_ContactPersonMiddleName"] != DBNull.Value)
                        advisorVo.ContactPersonMiddleName = dr["A_ContactPersonMiddleName"].ToString();
                    if (dr["A_Country"] != DBNull.Value)
                        advisorVo.Country = dr["A_Country"].ToString();
                    advisorVo.Email = dr["A_Email"].ToString();
                    if (dr["A_Fax"] != DBNull.Value)
                        advisorVo.Fax = int.Parse(dr["A_Fax"].ToString());
                    if (dr["A_Website"] != DBNull.Value)
                        advisorVo.Website = dr["A_Website"].ToString();
                    if (dr["A_FaxISD"] != DBNull.Value)
                        advisorVo.FaxIsd = int.Parse(dr["A_FaxISD"].ToString());
                    if (dr["A_FaxSTD"] != DBNull.Value)
                        advisorVo.FaxStd = int.Parse(dr["A_FaxSTD"].ToString());
                    if (dr["A_ContactPersonMobile"] != DBNull.Value)
                        advisorVo.MobileNumber = Convert.ToInt64(dr["A_ContactPersonMobile"].ToString());
                    if (dr["A_IsMultiBranch"] != DBNull.Value)
                        advisorVo.MultiBranch = int.Parse(dr["A_IsMultiBranch"].ToString());
                    if (dr["A_IsAssociateModel"] != DBNull.Value)
                        advisorVo.Associates = int.Parse(dr["A_IsAssociateModel"].ToString());

                    if (dr["A_Phone1STD"] != DBNull.Value && dr["A_Phone1STD"].ToString() != string.Empty)
                        advisorVo.Phone1Std = int.Parse(dr["A_Phone1STD"].ToString());
                    if (dr["A_Phone2STD"] != DBNull.Value && dr["A_Phone2STD"].ToString() != string.Empty)
                        advisorVo.Phone2Std = int.Parse(dr["A_Phone2STD"].ToString());
                    if (dr["A_Phone1ISD"] != DBNull.Value && dr["A_Phone1ISD"].ToString() != string.Empty)
                        advisorVo.Phone1Isd = int.Parse(dr["A_Phone1ISD"].ToString());
                    if (dr["A_Phone2ISD"] != DBNull.Value && dr["A_Phone2ISD"].ToString() != string.Empty)
                        advisorVo.Phone2Isd = int.Parse(dr["A_Phone2ISD"].ToString());
                    if (dr["A_Phone1Number"] != DBNull.Value && dr["A_Phone1Number"].ToString() != string.Empty)
                        advisorVo.Phone1Number = int.Parse(dr["A_Phone1Number"].ToString());
                    if (dr["A_Phone2Number"] != DBNull.Value && dr["A_Phone2Number"].ToString() != string.Empty)
                        advisorVo.Phone2Number = int.Parse(dr["A_Phone2Number"].ToString());
                    if (dr["A_PinCode"] != DBNull.Value)
                    advisorVo.PinCode = int.Parse(dr["A_PinCode"].ToString());
                    if (dr["A_State"] != DBNull.Value)
                    advisorVo.State = dr["A_State"].ToString();
                    if (dr["A_AdviserLogo"] != DBNull.Value)
                        advisorVo.LogoPath = dr["A_AdviserLogo"].ToString();
                    if (dr["A_Designation"] != DBNull.Value)
                        advisorVo.Designation = dr["A_Designation"].ToString();
                    if (dr["XAC_AdviserCategory"] != null && dr["XAC_AdviserCategory"].ToString() != "")
                        advisorVo.Category = dr["XAC_AdviserCategory"].ToString();
                    if (dr["AL_IsDependent"] != null && dr["AL_IsDependent"].ToString() != "")
                        advisorVo.IsDependent = Int16.Parse(dr["AL_IsDependent"].ToString());
                    advisorVo.LoginId = dr["U_LoginId"].ToString();
                    advisorVo.Password = dr["U_Password"].ToString();
                    advisorVo.Email = dr["A_Email"].ToString();
                    if (!string.IsNullOrEmpty(dr["A_IsActive"].ToString()))
                        advisorVo.IsActive = Int16.Parse(dr["A_IsActive"].ToString());
                    if (dr["A_ActivationDate"] != null && dr["A_ActivationDate"].ToString() != "")
                        advisorVo.ActivationDate = DateTime.Parse(dr["A_ActivationDate"].ToString());
                    if (dr["A_DeactivateDate"] != null && dr["A_DeactivateDate"].ToString() != "")
                        advisorVo.DeactivationDate = DateTime.Parse(dr["A_DeactivateDate"].ToString());

                    if (dr["A_VaultSize(in MB)"] != null && dr["A_VaultSize(in MB)"].ToString() != "")
                        advisorVo.VaultSize = float.Parse(dr["A_VaultSize(in MB)"].ToString());

                    if (!string.IsNullOrEmpty(dr["A_DomainName"].ToString()))
                        advisorVo.DomainName = dr["A_DomainName"].ToString();
                    else
                        advisorVo.DomainName = string.Empty;
                    advisorVo.IsIPEnable = Convert.ToInt16(dr["A_isIPEnable"].ToString());
                    advisorVo.IsOpsEnable = Convert.ToInt16(dr["A_IsOpsEnable"].ToString());
                    if (!string.IsNullOrEmpty(dr["U_Theme"].ToString().Trim()))
                        advisorVo.theme = Convert.ToString(dr["U_Theme"]).Trim();

                    advisorVo.SubscriptionVo.AdviserId = int.Parse(dr["A_AdviserId"].ToString());

                    if (dr["AS_AdviserSubscriptionId"] != DBNull.Value && dr["AS_AdviserSubscriptionId"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.SubscriptionId = int.Parse(dr["AS_AdviserSubscriptionId"].ToString());

                    if (dr["AS_TrialStartDate"] != DBNull.Value && dr["AS_TrialStartDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.TrialStartDate = DateTime.Parse(dr["AS_TrialStartDate"].ToString());

                    if (dr["AS_TrialEndDate"] != DBNull.Value && dr["AS_TrialEndDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.TrialEndDate = DateTime.Parse(dr["AS_TrialEndDate"].ToString());

                    if (dr["AS_SubscriptionStartDate"] != DBNull.Value && dr["AS_SubscriptionStartDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.StartDate = DateTime.Parse(dr["AS_SubscriptionStartDate"].ToString());

                    if (dr["AS_SubscriptionEndDate"] != DBNull.Value && dr["AS_SubscriptionEndDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.EndDate = DateTime.Parse(dr["AS_SubscriptionEndDate"].ToString());

                    if (dr["AS_SMSLicences"] != DBNull.Value && dr["AS_SMSLicences"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.SmsBought = int.Parse(dr["AS_SMSLicences"].ToString());

                    if (dr["AS_StorageSize"] != DBNull.Value && dr["AS_StorageSize"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.StorageSize = float.Parse(dr["AS_StorageSize"].ToString());

                    if (dr["AS_StorageBalance"] != DBNull.Value && dr["AS_StorageBalance"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.StorageBalance = float.Parse(dr["AS_StorageBalance"].ToString());

                    if (dr["AS_NoOfStaffWebLogins"] != DBNull.Value && dr["AS_NoOfStaffWebLogins"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.NoOfStaffLogins = int.Parse(dr["AS_NoOfStaffWebLogins"].ToString());

                    if (dr["AS_NoOfBranches"] != DBNull.Value && dr["AS_NoOfBranches"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.NoOfBranches = int.Parse(dr["AS_NoOfBranches"].ToString());

                    if (dr["AS_NoOfCustomerWebLogins"] != DBNull.Value && dr["AS_NoOfCustomerWebLogins"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.NoOfCustomerLogins = int.Parse(dr["AS_NoOfCustomerWebLogins"].ToString());

                    if (dr["AS_IsDeactivated"] != DBNull.Value && dr["AS_IsDeactivated"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.IsDeActivated = int.Parse(dr["AS_IsDeactivated"].ToString());

                    if (dr["AS_DeactivationDate"] != DBNull.Value && dr["AS_DeactivationDate"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.DeActivationDate = DateTime.Parse(dr["AS_DeactivationDate"].ToString());

                    if (dr["AS_Comments"] != DBNull.Value && dr["AS_Comments"].ToString() != string.Empty)
                        advisorVo.SubscriptionVo.Comments = dr["AS_Comments"].ToString();

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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdvisorUser()");

                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return advisorVo;
        }

        public string getId()
        {
            Guid id;
            id = Guid.NewGuid();
            return id.ToString();
        }

        public bool UpdateAdvisorUser(AdvisorVo advisorVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateAdvisorUserCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdvisorUserCmd = db.GetStoredProcCommand("SP_UpdateAdviserUser");
                db.AddInParameter(updateAdvisorUserCmd, "@A_AdviserId", DbType.Int32, advisorVo.advisorId);
                db.AddInParameter(updateAdvisorUserCmd, "@U_UserId", DbType.Int32, advisorVo.UserId);
                db.AddInParameter(updateAdvisorUserCmd, "@A_OrgName", DbType.String, advisorVo.OrganizationName);
                db.AddInParameter(updateAdvisorUserCmd, "@A_AddressLine1", DbType.String, advisorVo.AddressLine1);
                db.AddInParameter(updateAdvisorUserCmd, "@A_AddressLine2", DbType.String, advisorVo.AddressLine2);
                db.AddInParameter(updateAdvisorUserCmd, "@A_AddressLine3", DbType.String, advisorVo.AddressLine3);
                db.AddInParameter(updateAdvisorUserCmd, "@A_City", DbType.String, advisorVo.City);
                db.AddInParameter(updateAdvisorUserCmd, "@A_State", DbType.String, advisorVo.State);
                db.AddInParameter(updateAdvisorUserCmd, "@A_PinCode", DbType.Int32, advisorVo.PinCode);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Country", DbType.String, advisorVo.Country);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Phone1ISD", DbType.Int32, advisorVo.Phone1Isd);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Phone1STD", DbType.Int32, advisorVo.Phone1Std);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Phone1Number", DbType.Int32, advisorVo.Phone1Number);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Phone2ISD", DbType.Int32, advisorVo.Phone2Isd);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Phone2STD", DbType.Int32, advisorVo.Phone2Std);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Phone2Number", DbType.Int32, advisorVo.Phone2Number);
                db.AddInParameter(updateAdvisorUserCmd, "@A_FAXISD", DbType.Int32, advisorVo.FaxIsd);
                db.AddInParameter(updateAdvisorUserCmd, "@A_FAXSTD", DbType.Int32, advisorVo.FaxStd);
                db.AddInParameter(updateAdvisorUserCmd, "@A_FAX", DbType.Int32, advisorVo.Fax);
                db.AddInParameter(updateAdvisorUserCmd, "@A_ContactPersonMobile", DbType.String, advisorVo.MobileNumber);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Email", DbType.String, advisorVo.Email);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Website", DbType.String, advisorVo.Website);
                db.AddInParameter(updateAdvisorUserCmd, "@A_ContactPersonFirstName", DbType.String, advisorVo.ContactPersonFirstName);
                db.AddInParameter(updateAdvisorUserCmd, "@A_ContactPersonMiddleName", DbType.String, advisorVo.ContactPersonMiddleName);
                db.AddInParameter(updateAdvisorUserCmd, "@A_ContactPersonLastName", DbType.String, advisorVo.ContactPersonLastName);
                db.AddInParameter(updateAdvisorUserCmd, "@A_IsMultiBranch", DbType.Int32, advisorVo.MultiBranch);
                db.AddInParameter(updateAdvisorUserCmd, "@A_IsAssociateModel", DbType.Int32, advisorVo.Associates);
                db.AddInParameter(updateAdvisorUserCmd, "@XABT_BusinessTypeCode", DbType.String, advisorVo.BusinessCode);
                db.AddInParameter(updateAdvisorUserCmd, "@A_AdviserLogo", DbType.String, advisorVo.LogoPath);
                db.AddInParameter(updateAdvisorUserCmd, "@A_Designation", DbType.String, advisorVo.Designation);
                db.AddInParameter(updateAdvisorUserCmd, "@A_IsIPEnable", DbType.Int32, advisorVo.IsIPEnable);
                db.AddInParameter(updateAdvisorUserCmd, "@A_IsOpsEnable", DbType.Int32, advisorVo.IsOpsEnable);

                if (db.ExecuteNonQuery(updateAdvisorUserCmd) != 0)
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

                FunctionInfo.Add("Method", "AdvisorDao.cs:UpdateAdvisorUser()");


                object[] objects = new object[1];
                objects[0] = advisorVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        /// <summary>
        /// Get all Classification List of the advisor
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetAdviserClassification(int adviserId)
        {
            Database db;
            DbCommand GetAdviserClassificationCmd;
            DataSet dsAdviserClassification;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserClassificationCmd = db.GetStoredProcCommand("SP_GetAllClassification");
                db.AddInParameter(GetAdviserClassificationCmd, "@AdvisorId", DbType.Int32, adviserId);

                dsAdviserClassification = db.ExecuteDataSet(GetAdviserClassificationCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserClassification(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserClassification;

        }

        /// <summary>
        /// Function to retrieve the tree nodes based on the user role
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public DataSet GetTreeNodesBasedOnUserRoles(string userRole,string treeType,int adviserId)
        {
            Database db;
            DbCommand GetAdviserTreeNodes;
            DataSet dsAdviserTreeNodes;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserTreeNodes = db.GetStoredProcCommand("SP_GetTreeNodesBasedOnUserRoles");
                db.AddInParameter(GetAdviserTreeNodes, "@userrole", DbType.String, userRole);
                db.AddInParameter(GetAdviserTreeNodes, "@treetype", DbType.String, treeType);
                db.AddInParameter(GetAdviserTreeNodes, "@adviserId", DbType.Int32, adviserId);

                dsAdviserTreeNodes = db.ExecuteDataSet(GetAdviserTreeNodes);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetTreeNodesBasedOnUserRoles(string userRole)");
                object[] objects = new object[1];
                objects[0] = userRole;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTreeNodes;
        }

        /// <summary>
        /// Function to retrieve the tree nodes based on the plans subscribed
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetTreeNodesBasedOnPlans(int adviserId,string userRole,string treeType)
        {
            Database db;
            DbCommand GetAdviserTreeNodes;
            DataSet dsAdviserTreeNodes;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserTreeNodes = db.GetStoredProcCommand("SP_GetTreeNodesForAdvisersBasedOnPlan");
                db.AddInParameter(GetAdviserTreeNodes, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetAdviserTreeNodes, "@userrole", DbType.String, userRole);
                db.AddInParameter(GetAdviserTreeNodes, "@treetype", DbType.String, treeType);

                dsAdviserTreeNodes = db.ExecuteDataSet(GetAdviserTreeNodes);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetTreeNodesBasedOnPlans()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = userRole;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserTreeNodes;
        }

        /// <summary>
        /// Function to retrieve the potential home page fot a user
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataSet GetUserPotentialHomepages(int adviserId, string userRole)
        {
            Database db;
            DbCommand GetUserPotentialHomepage;
            DataSet dsUserPotentialHomepage;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetUserPotentialHomepage = db.GetStoredProcCommand("SP_GetUserPotentialHomePage");
                db.AddInParameter(GetUserPotentialHomepage, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetUserPotentialHomepage, "@UserRole", DbType.String, userRole);

                dsUserPotentialHomepage = db.ExecuteDataSet(GetUserPotentialHomepage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetUserPotentialHomepages()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = userRole;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsUserPotentialHomepage;
        }


        public DataSet GetXMLAdvisorCategory()
        {
            Database db;
            DbCommand GetXMLAdvisorCategorycmd;
            DataSet dsGetXMLAdvisorCategory;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetXMLAdvisorCategorycmd = db.GetStoredProcCommand("SP_GetXMLAdvisorCategory");
                dsGetXMLAdvisorCategory = db.ExecuteDataSet(GetXMLAdvisorCategorycmd);




            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserSibscriptionDetails(int adviserId)");
                object[] objects = new object[1];
                objects[0] = "GetXMLAdvisorCategory";

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetXMLAdvisorCategory;

        }

        /// <summary>
        /// Updates Complete Advisor which includes AdviserRM,Adviser and Adviser Table
        /// Caution:Please use this only if it is needed.
        /// </summary>
        /// <param name="userVo"></param>
        /// <param name="adviserVo"></param>
        /// <param name="rmVo"></param>
        public void UpdateCompleteAdviser(UserVo userVo, AdvisorVo advisorVo, RMVo rmVo)
        {
            Database db;
            DbCommand createCompleteAdvisorCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCompleteAdvisorCmd = db.GetStoredProcCommand("SP_UpdateCompleteAdviser");
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_Password", DbType.String, userVo.Password);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_FirstName ", DbType.String, userVo.FirstName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_Lastname", DbType.String, userVo.LastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_UserType", DbType.String, userVo.UserType);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_LoginId", DbType.String, userVo.LoginId);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_CreatedBy", DbType.Int32, 100);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_ModifiedBy", DbType.Int32, 100);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_OrgName", DbType.String, advisorVo.OrganizationName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine1", DbType.String, advisorVo.AddressLine1);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine2", DbType.String, advisorVo.AddressLine2);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine3", DbType.String, advisorVo.AddressLine3);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_City", DbType.String, advisorVo.City);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_State", DbType.String, advisorVo.State);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_PinCode", DbType.Int32, advisorVo.PinCode);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Country", DbType.String, advisorVo.Country);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1STD", DbType.Int32, advisorVo.Phone1Std);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1ISD", DbType.Int32, advisorVo.Phone1Isd);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1Number", DbType.Int32, advisorVo.Phone1Number);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2STD", DbType.Int32, advisorVo.Phone2Std);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2ISD", DbType.Int32, advisorVo.Phone2Isd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone2Number", DbType.Int32, advisorVo.Phone2Number);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_Email", DbType.String, advisorVo.Email);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_FAXISD", DbType.Int32, advisorVo.FaxIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_FAXSTD", DbType.Int32, advisorVo.FaxStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_FAX", DbType.Int32, advisorVo.Fax);
                //db.AddInParameter(createCompleteAdvisorCmd, "@XABT_BusinessTypeCode", DbType.String, advisorVo.BusinessCode);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonFirstName", DbType.String, advisorVo.ContactPersonFirstName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonMiddleName", DbType.String, advisorVo.ContactPersonMiddleName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonLastName", DbType.String, advisorVo.ContactPersonLastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_ContactPersonMobile", DbType.Int64, advisorVo.MobileNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsMultiBranch", DbType.Int32, advisorVo.MultiBranch);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsActive", DbType.String, advisorVo.IsActive);
                if (advisorVo.ActivationDate != DateTime.MinValue)
                    db.AddInParameter(createCompleteAdvisorCmd, "@A_ActivationDate", DbType.DateTime,DBNull.Value);
                else
                {
                    advisorVo.ActivationDate = DateTime.Now;
                    db.AddInParameter(createCompleteAdvisorCmd, "@A_ActivationDate", DbType.DateTime, advisorVo.ActivationDate);
                }
                db.AddInParameter(createCompleteAdvisorCmd, "@A_DeactivateDate", DbType.DateTime, advisorVo.DeactivationDate);
                db.AddInParameter(createCompleteAdvisorCmd, "@XAC_AdviserCategoryCode", DbType.String, advisorVo.Category);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_IsAssociateModel", DbType.Int32, advisorVo.Associates);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_ModifiedBy", DbType.Int32, 100);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_AdviserLogo", DbType.String, advisorVo.LogoPath);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_FirstName", DbType.String, rmVo.FirstName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_MiddleName", DbType.String, rmVo.MiddleName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_LastName", DbType.String, rmVo.LastName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirectISD", DbType.Int32, rmVo.OfficePhoneDirectIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirectSTD", DbType.Int32, rmVo.OfficePhoneDirectStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneDirect", DbType.Int32, rmVo.OfficePhoneDirectNumber);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExtISD", DbType.Int32, rmVo.OfficePhoneExtIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExtSTD", DbType.Int32, rmVo.OfficePhoneExtStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_OfficePhoneExt", DbType.Int32, rmVo.OfficePhoneExtNumber);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhoneISD", DbType.Int32, rmVo.ResPhoneIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhoneSTD", DbType.Int32, rmVo.ResPhoneStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_ResPhone", DbType.Int32, rmVo.ResPhoneNumber);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_Mobile", DbType.Int64, rmVo.Mobile);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_FaxISD", DbType.Int32, rmVo.FaxIsd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_FaxSTD", DbType.Int32, rmVo.FaxStd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_Fax", DbType.Int32, rmVo.Fax);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_Email", DbType.String, rmVo.Email);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_JobFunction", DbType.String, rmVo.RMRole);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_IsExternalStaff", DbType.Int16, rmVo.IsExternal);
                //db.AddInParameter(createCompleteAdvisorCmd, "@AR_CTC", DbType.Double, 0);                             

                db.AddInParameter(createCompleteAdvisorCmd, "@U_UserId", DbType.Int32, userVo.UserId);
                db.AddInParameter(createCompleteAdvisorCmd, "@AR_RMId", DbType.Int32, rmVo.RMId);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_AdviserId", DbType.Int32, advisorVo.advisorId);
                db.ExecuteNonQuery(createCompleteAdvisorCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:CreateCompleteAdviser()");
                object[] objects = new object[3];
                objects[0] = advisorVo;
                objects[1] = rmVo;
                objects[2] = userVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }
  
        /// <summary>
        ///Getting domain name for login widget 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public string GetAdviserDomainName(int adviserId)
        {
            Database db;
            DbCommand GetAdviserDomainName;
            DataSet dsAdviserDomainName;
            string domain = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserDomainName = db.GetStoredProcCommand("SP_GetAdviserDomain");
                db.AddInParameter(GetAdviserDomainName, "@AdviserId", DbType.Int32, adviserId);
                dsAdviserDomainName = db.ExecuteDataSet(GetAdviserDomainName);
                if (dsAdviserDomainName != null && dsAdviserDomainName.Tables[0].Rows.Count > 0)
                {
                    domain = dsAdviserDomainName.Tables[0].Rows[0]["Domain"].ToString();
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserDomainName(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return domain;

        }
        /// <summary>
        /// To Create AdviserIPsPool Information for Adviser 
        /// Added by Vinayak Patil
        /// </summary>
        /// <param name="adviserIPvo"></param>
        /// <param name="createdBy"></param>
        /// <returns>adviserIPPoolstatus</returns>
        
        public bool CreateAdviserIPPools(AdviserIPVo adviserIPvo, int createdBy)
        {
            bool bStatus = false;
            Database db;
            DbCommand createAdviserIPPools;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createAdviserIPPools = db.GetStoredProcCommand("SP_AddAdviserIPPool");
                db.AddInParameter(createAdviserIPPools, "@A_AdviserId", DbType.Int32, adviserIPvo.advisorId);
                db.AddInParameter(createAdviserIPPools, "@AIPP_IP", DbType.String, adviserIPvo.AdviserIPs);
                db.AddInParameter(createAdviserIPPools, "@AIPP_Comments", DbType.String, adviserIPvo.AdviserIPComments);
                db.AddInParameter(createAdviserIPPools, "@AIPP_CreatedBy", DbType.String, createdBy);
                db.AddInParameter(createAdviserIPPools, "@AIPP_ModifiedBy", DbType.Int32, createdBy);
                db.AddOutParameter(createAdviserIPPools, "@AIPP_poolId", DbType.Int32, 10);

                if (db.ExecuteNonQuery(createAdviserIPPools) != 0)
                    bStatus = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateCustomerGIPortfolio()");


                object[] objects = new object[2];
                objects[0] = createdBy;
                objects[1] = adviserIPvo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bStatus;
        }

        /// <summary>
        /// To Get all AdviserIPPool Informations
        /// Added by Vinayak Patil
        /// </summary>
        /// <param name="AdviserId"></param>
        /// <returns></returns>
        
        public DataSet GetAdviserIPPoolsInformation(int AdviserId)
        {
            AdviserIPVo adviserIPPoolsVo = new AdviserIPVo();
            Database db;
            DbCommand getAdviserIPPoolsInformationCmd;
            DataSet getAdviserIPPoolsInformationDs;
            
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserIPPoolsInformationCmd = db.GetStoredProcCommand("SP_GetAdviserIPPoolInformation");
                db.AddInParameter(getAdviserIPPoolsInformationCmd, "@A_AdviserID", DbType.Int32, AdviserId);
                getAdviserIPPoolsInformationDs = db.ExecuteDataSet(getAdviserIPPoolsInformationCmd);

                if (getAdviserIPPoolsInformationDs.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in getAdviserIPPoolsInformationDs.Tables[0].Rows)
                    {
                        adviserIPPoolsVo = new AdviserIPVo();
                        adviserIPPoolsVo.advisorId = AdviserId;
                        adviserIPPoolsVo.advisorIPPoolId = Int32.Parse(dr["AIPP_poolId"].ToString());
                        adviserIPPoolsVo.AdviserIPs = dr["AIPP_IP"].ToString();
                        adviserIPPoolsVo.AdviserIPComments = dr["AIPP_Comments"].ToString();
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

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetAdviserIPPoolsInformation()");


                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getAdviserIPPoolsInformationDs;
        }


        /// <summary>
        /// To Delete last IP Pool from Adviser IP Pool 
        /// Vinayak Patil
        /// </summary>
        /// <param name="adviserIPPoolId"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>

        public bool DeleteAdviserIPPools(int adviserIPPoolId, int adviserId, bool isSingleIP, string Flag)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteCustomerBankCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCustomerBankCmd = db.GetStoredProcCommand("SP_UpdateAdviserIPPools");
                db.AddInParameter(deleteCustomerBankCmd, "@AIPP_poolId", DbType.Int32, adviserIPPoolId);
                db.AddInParameter(deleteCustomerBankCmd, "@A_AdviserID", DbType.Int32, adviserId);
                db.AddInParameter(deleteCustomerBankCmd, "@Flag", DbType.String, Flag);
                if(isSingleIP == true)
                    db.AddInParameter(deleteCustomerBankCmd, "@isSingleIP", DbType.Int32, 1);
                else
                    db.AddInParameter(deleteCustomerBankCmd, "@isSingleIP", DbType.Int32, 0);

                if (db.ExecuteNonQuery(deleteCustomerBankCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerDao.cs:DeleteAdviserIPPools(int adviserIPPoolId, string Flag)");

                object[] objects = new object[2];
                objects[0] = adviserIPPoolId;
                //objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataSet GetAdvisersAlreadyLoggedIPs(int AdviserId)
        {
            AdviserIPVo adviserIPPoolsVo = new AdviserIPVo();
            Database db;
            DbCommand getAdviserIPPoolsInformationCmd;
            DataSet getAdviserIPPoolsInformationDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserIPPoolsInformationCmd = db.GetStoredProcCommand("SP_GetAdviserLoggedIPs");
                db.AddInParameter(getAdviserIPPoolsInformationCmd, "@AdvisorId", DbType.Int32, AdviserId);
                getAdviserIPPoolsInformationDs = db.ExecuteDataSet(getAdviserIPPoolsInformationCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerFamilyDao.cs:GetAdviserIPPoolsInformation()");


                object[] objects = new object[1];
                objects[0] = AdviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getAdviserIPPoolsInformationDs;
        }

        /// <summary>
        /// Updating the AdviserIPPool Informations..
        /// Added by Vinayak Patil..
        /// </summary>
        /// <param name="adviserIPvo"></param>
        /// <param name="createdBy"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        
        public bool UpdateAdviserIPPools(AdviserIPVo adviserIPvo, int createdBy, string Flag)
        {
            bool bResult = false;
            Database db;
            DbCommand updateAdviserIPPools;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdviserIPPools = db.GetStoredProcCommand("SP_UpdateAdviserIPPools");
                db.AddInParameter(updateAdviserIPPools, "@AIPP_poolId", DbType.Int32, adviserIPvo.advisorIPPoolId);
                db.AddInParameter(updateAdviserIPPools, "@AIPP_IP", DbType.String, adviserIPvo.AdviserIPs);
                db.AddInParameter(updateAdviserIPPools, "@AIPP_Comments", DbType.String, adviserIPvo.AdviserIPComments);
                db.AddInParameter(updateAdviserIPPools, "@AIPP_ModifiedBy", DbType.Int32, createdBy);
                db.AddInParameter(updateAdviserIPPools, "@Flag", DbType.String, Flag);

                if (db.ExecuteNonQuery(updateAdviserIPPools) != 0)
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:CreateCustomerGIPortfolio()");


                object[] objects = new object[2];
                objects[0] = createdBy;
                objects[1] = adviserIPvo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Get All the Ops Staffs for a perticular advisor...
        /// Added by Vinayak Patil on 20th Sep 2011
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="UserRole"></param>
        /// <returns></returns>
        public DataSet GetAllOpsStaffsForAdviser(int adviserId, string UserRole)
        {
            Database db;
            DbCommand GetAllOpsStaffsForAdviserCmd;
            DataSet dsAllOpsStaffsForAdviser;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAllOpsStaffsForAdviserCmd = db.GetStoredProcCommand("SP_GetAllOpsStaffsForAdviser");
                db.AddInParameter(GetAllOpsStaffsForAdviserCmd, "@AR_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetAllOpsStaffsForAdviserCmd, "@UR_UserRoleName", DbType.String, UserRole);

                dsAllOpsStaffsForAdviser = db.ExecuteDataSet(GetAllOpsStaffsForAdviserCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserSibscriptionDetails(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAllOpsStaffsForAdviser;
        }

        /// <summary>
        /// To Update the perticular RM Status..
        /// Added by Vinayak Patil on 20th Sep 2011
        /// </summary>
        /// <param name="RMId"></param>
        /// <param name="RMLoginStatus"></param>
        /// <returns></returns>
        
        public bool UpdateOpsStaffLoginStatus(int RMId, int RMLoginStatus)
        {
            bool bResult = false;
            Database db;
            DbCommand updateOpsStaffLoginStatusCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateOpsStaffLoginStatusCmd = db.GetStoredProcCommand("SP_UpdateOpsStaffsLoginActiveStatus");
                db.AddInParameter(updateOpsStaffLoginStatusCmd, "@AR_RMId", DbType.Int32, RMId);
                db.AddInParameter(updateOpsStaffLoginStatusCmd, "@AR_IsActiveStatus", DbType.Int32, RMLoginStatus);


                if (db.ExecuteNonQuery(updateOpsStaffLoginStatusCmd) != 0)
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

                FunctionInfo.Add("Method", "InsuranceDao.cs:UpdateOpsStaffLoginStatus()");


                object[] objects = new object[3];
                objects[0] = RMId;
                objects[1] = RMLoginStatus;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// Get all the Advisers Online Transaction AMC Links..
        /// </summary>
        /// <returns></returns>
        public List<AdviserOnlineTransactionAMCLinksVo> GetAdviserOnlineTransactionAMCLinks(AdviserOnlineTransactionAMCLinksVo aotalVo)
        {
            Database db;
            DbCommand GetAdviserOnlineTransactionAMCLinksCmd;
            DataSet dsGetAdviserOnlineTransactionAMCLinks = new DataSet();
            List<AdviserOnlineTransactionAMCLinksVo> adviserOTALink = null;
            AdviserOnlineTransactionAMCLinksVo aoTALVo = new AdviserOnlineTransactionAMCLinksVo();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserOnlineTransactionAMCLinksCmd = db.GetStoredProcCommand("SP_GetAdviserOnlineTransactionAMCLinks");
                db.AddInParameter(GetAdviserOnlineTransactionAMCLinksCmd, "@A_AdviserID", DbType.Int32, aotalVo.advisorId);
                db.AddInParameter(GetAdviserOnlineTransactionAMCLinksCmd, "@XLU_LinkUserCode", DbType.Int32, aotalVo.AMCLinkUserCode);
                db.AddInParameter(GetAdviserOnlineTransactionAMCLinksCmd, "@XLTY_LinkTypeCode", DbType.Int32, aotalVo.AMCLinkTypeCode);

                dsGetAdviserOnlineTransactionAMCLinks = db.ExecuteDataSet(GetAdviserOnlineTransactionAMCLinksCmd);

                if (dsGetAdviserOnlineTransactionAMCLinks.Tables.Count > 0)
                {
                    if (dsGetAdviserOnlineTransactionAMCLinks.Tables[0].Rows.Count > 0)
                    {
                        adviserOTALink = new List<AdviserOnlineTransactionAMCLinksVo>();

                        foreach (DataRow dr in dsGetAdviserOnlineTransactionAMCLinks.Tables[0].Rows)
                        {
                            aoTALVo = new AdviserOnlineTransactionAMCLinksVo();
                            aoTALVo.AMCLinkId = int.Parse(dr["AL_LinkId"].ToString());
                            aoTALVo.advisorId = int.Parse(dr["A_AdviserId"].ToString());
                            aoTALVo.AMCLinkUserCode = int.Parse(dr["XLU_LinkUserCode"].ToString());
                            aoTALVo.AMCLinkTypeCode = int.Parse(dr["XLTY_LinkTypeCode"].ToString());
                            aoTALVo.AMCLinks = dr["AL_Link"].ToString();
                            aoTALVo.AMCImagePath = dr["AL_LinkImagePath"].ToString();
                            if(!string.IsNullOrEmpty(dr["WELM_LinkCode"].ToString()))
                            {
                                aoTALVo.ExternalLinkCode=dr["WELM_LinkCode"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dr["AL_AltLinkName"].ToString()))
                            {
                                aoTALVo.AltLinkName = dr["AL_AltLinkName"].ToString();
                            }
                            adviserOTALink.Add(aoTALVo);
                        }
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserOnlineTransactionAMCLinks(AdviserOnlineTransactionAMCLinksVo aotalVo)");
                object[] objects = new object[1];
                objects[0] = aotalVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return adviserOTALink;
        }

        /// <summary>
        ///Getting Adviser Theme for login widget 
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAdviserLogInWidgetDetails(int adviserId)
        {
            Database db;
            DbCommand GetAdviserDetails;
            DataSet dsGetAdviserDetails;
            Dictionary<string, string> advisorLoginWidgetDetails = new Dictionary<string, string>();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAdviserDetails = db.GetStoredProcCommand("SP_GetAdviserLoginWidgetDeatils");
                db.AddInParameter(GetAdviserDetails, "@AdviserId", DbType.Int32, adviserId);
                dsGetAdviserDetails = db.ExecuteDataSet(GetAdviserDetails);
                if (dsGetAdviserDetails != null && dsGetAdviserDetails.Tables[0].Rows.Count > 0)
                {
                    advisorLoginWidgetDetails.Add("DomainName", dsGetAdviserDetails.Tables[0].Rows[0]["A_DomainName"].ToString().Trim());
                    advisorLoginWidgetDetails.Add("IsActive", dsGetAdviserDetails.Tables[0].Rows[0]["A_IsActive"].ToString().Trim());
                    advisorLoginWidgetDetails.Add("Theme", dsGetAdviserDetails.Tables[0].Rows[0]["U_Theme"].ToString().Trim());
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserLogInWidgetDetails(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return advisorLoginWidgetDetails;

        }

        public bool UpdateAdviserFPBatch(string customerIds,int adviserId)
        {
            Database db;
            DbCommand dbCustomerSync;
           
            bool result = true;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCustomerSync = db.GetStoredProcCommand("SP_UpdateAdviserFPBatch");
                db.AddInParameter(dbCustomerSync, "@CustomerIds", DbType.String, customerIds);

                db.AddInParameter(dbCustomerSync, "@AdviserId", DbType.Int32, adviserId);
                db.ExecuteNonQuery(dbCustomerSync);                     
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:UpdateAdviserFPBatch(string customerIds)");
                object[] objects = new object[1];
                objects[0] = customerIds;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool UpdateAdviserStorageBalance(int intAdviserId, float fStorageBal)
        {
            Database db;
            DbCommand updateAdviserStorageBalanceCmd;

            bool result = true;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdviserStorageBalanceCmd = db.GetStoredProcCommand("sproc_Adviser_UpdateAdviserStorageBalance");
                db.AddInParameter(updateAdviserStorageBalanceCmd, "@adviserId", DbType.String, intAdviserId);
                db.AddInParameter(updateAdviserStorageBalanceCmd, "@storageBalance", DbType.Double, fStorageBal);
                db.ExecuteNonQuery(updateAdviserStorageBalanceCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AdvisorDao.cs:UpdateAdviserStorageBalance(int intAdviserId, float fStorageBal)");
                object[] objects = new object[2];
                objects[0] = intAdviserId;
                objects[1] = fStorageBal;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
    }
}

