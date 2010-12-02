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
                db.AddInParameter(createCompleteAdvisorCmd, "@U_FirstName ", DbType.String, userVo.FirstName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_Lastname", DbType.String, userVo.LastName);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_Email", DbType.String, userVo.Email);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_UserType", DbType.String, userVo.UserType);
                db.AddInParameter(createCompleteAdvisorCmd, "@U_LoginId", DbType.String, userVo.LoginId);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_CreatedBy", DbType.Int32, 100);
                //db.AddInParameter(createCompleteAdvisorCmd, "@U_ModifiedBy", DbType.Int32, 100);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_OrgName", DbType.String, advisorVo.OrganizationName);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine1", DbType.String, advisorVo.AddressLine1);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine2", DbType.String, advisorVo.AddressLine2);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_AddressLine3", DbType.String, advisorVo.AddressLine3);
                db.AddInParameter(createCompleteAdvisorCmd, "@A_City", DbType.String, advisorVo.City);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_State", DbType.String, advisorVo.State);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_PinCode", DbType.Int32, advisorVo.PinCode);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Country", DbType.String, advisorVo.Country);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1STD", DbType.Int32, advisorVo.Phone1Std);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1ISD", DbType.Int32, advisorVo.Phone1Isd);
                //db.AddInParameter(createCompleteAdvisorCmd, "@A_Phone1Number", DbType.Int32, advisorVo.Phone1Number);
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
        public List<CustomerVo> GetAdviserCustomersForSMS(int adviserId, string namefilter)
        {
            List<CustomerVo> customerList = new List<CustomerVo>();
            CustomerVo customerVo=new CustomerVo();
            Database db;
            DbCommand getAdviserCustomersForSMSCmd;
            DataSet getCustomerDs;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAdviserCustomersForSMSCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerForSMS");
                db.AddInParameter(getAdviserCustomersForSMSCmd, "@A_AdviserId", DbType.Int32, adviserId);
                if(namefilter!="")
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


                object[] objects = new object[1];
                objects[0] = adviserId;

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

        public List<CustomerVo> GetAdviserCustomerList(int adviserId, int CurrentPage, out int Count, string SortExpression, string panFilter, string NameFilter, string AreaFilter, string PincodeFilter, string ParentFilter, string RMFilter, string Active, out Dictionary<string, string> genDictParent, out Dictionary<string, string> genDictRM, out Dictionary<string, string> genDictReassignRM)
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

                if (panFilter != "")
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, panFilter);
                else
                    db.AddInParameter(getCustomerListCmd, "@panFilter", DbType.String, DBNull.Value);

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

                    advisorVo.MultiBranch = int.Parse(dr["A_IsMultiBranch"].ToString());
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

    }
}

