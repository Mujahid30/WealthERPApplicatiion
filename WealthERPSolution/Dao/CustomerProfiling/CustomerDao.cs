using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoUser;
using VoCustomerProfiling;
using VoCustomerPortfolio;
using DaoUser;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerProfiling
{
    public class CustomerDao
    {
        /// <summary>
        /// It Creates Customer
        /// </summary>
        /// <param name="customerVo"></param>
        /// <param name="rmId"></param>
        /// <param name="userId"></param>
        /// <param name="ADULProcessId"></param>
        /// <returns></returns>
        public int CreateCustomer(CustomerVo customerVo, int rmId, int userId, int ADULProcessId)
        {

            int customerId = 0;
            Database db;
            DbCommand createCustomerCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerCmd = db.GetStoredProcCommand("SP_CreateCustomer");

                db.AddInParameter(createCustomerCmd, "@ADUL_ProcessId", DbType.Int32, ADULProcessId);
                db.AddInParameter(createCustomerCmd, "@C_CustCode", DbType.String, customerVo.CustCode);
                db.AddInParameter(createCustomerCmd, "@AR_RMId", DbType.Int32, customerVo.RmId);
                db.AddInParameter(createCustomerCmd, "@AB_BranchId", DbType.Int32, customerVo.BranchId);
                db.AddInParameter(createCustomerCmd, "@U_UMId", DbType.Int32, customerVo.UserId);
                db.AddInParameter(createCustomerCmd, "@C_ProfilingDate", DbType.DateTime, customerVo.ProfilingDate);
                db.AddInParameter(createCustomerCmd, "@C_FirstName", DbType.String, customerVo.FirstName);
                db.AddInParameter(createCustomerCmd, "@C_MiddleName", DbType.String, customerVo.MiddleName);
                db.AddInParameter(createCustomerCmd, "@C_LastName", DbType.String, customerVo.LastName);
                db.AddInParameter(createCustomerCmd, "@C_Gender", DbType.String, customerVo.Gender);

                if (customerVo.Dob == DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_DOB", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_DOB", DbType.DateTime, customerVo.Dob);
                }
                db.AddInParameter(createCustomerCmd, "@XCT_CustomerTypeCode", DbType.String, customerVo.Type);
                db.AddInParameter(createCustomerCmd, "@XCST_CustomerSubTypeCode", DbType.String, customerVo.SubType);
                db.AddInParameter(createCustomerCmd, "@C_Salutation", DbType.String, customerVo.Salutation);
                db.AddInParameter(createCustomerCmd, "@C_PANNum", DbType.String, customerVo.PANNum);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Line1", DbType.String, customerVo.Adr1Line1);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Line2", DbType.String, customerVo.Adr1Line2);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Line3", DbType.String, customerVo.Adr1Line3);
                db.AddInParameter(createCustomerCmd, "@C_Adr1PinCode", DbType.Int32, customerVo.Adr1PinCode);
                db.AddInParameter(createCustomerCmd, "@C_Adr1City", DbType.String, customerVo.Adr1City);
                db.AddInParameter(createCustomerCmd, "@C_Adr1State", DbType.String, customerVo.Adr1State);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Country", DbType.String, customerVo.Adr1Country);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line1", DbType.String, customerVo.Adr2Line1);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line2", DbType.String, customerVo.Adr2Line2);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line3", DbType.String, customerVo.Adr2Line3);
                db.AddInParameter(createCustomerCmd, "@C_Adr2PinCode", DbType.Int32, customerVo.Adr2PinCode);
                db.AddInParameter(createCustomerCmd, "@C_Adr2City", DbType.String, customerVo.Adr2City);
                db.AddInParameter(createCustomerCmd, "@C_Adr2State", DbType.String, customerVo.Adr2State);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Country", DbType.String, customerVo.Adr2Country);
                if (customerVo.ResidenceLivingDate == DateTime.MinValue || customerVo.ResidenceLivingDate == null)
                {
                    db.AddInParameter(createCustomerCmd, "@C_ResidenceLivingDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_ResidenceLivingDate", DbType.DateTime, customerVo.ResidenceLivingDate);
                }
                db.AddInParameter(createCustomerCmd, "@C_ResISDCode", DbType.Int32, customerVo.ResISDCode);
                db.AddInParameter(createCustomerCmd, "@C_ResSTDCode", DbType.Int32, customerVo.ResSTDCode);
                db.AddInParameter(createCustomerCmd, "@C_ResPhoneNum", DbType.Int32, customerVo.ResPhoneNum);
                db.AddInParameter(createCustomerCmd, "@C_OfcISDCode", DbType.Int32, customerVo.OfcISDCode);
                db.AddInParameter(createCustomerCmd, "@C_OfcSTDCode", DbType.Int32, customerVo.OfcSTDCode);
                db.AddInParameter(createCustomerCmd, "@C_OfcPhoneNum", DbType.Int32, customerVo.OfcPhoneNum);
                db.AddInParameter(createCustomerCmd, "@C_Email", DbType.String, customerVo.Email);
                db.AddInParameter(createCustomerCmd, "@C_AltEmail", DbType.String, customerVo.AltEmail);
                db.AddInParameter(createCustomerCmd, "@C_Mobile1", DbType.Int64, customerVo.Mobile1);
                db.AddInParameter(createCustomerCmd, "@C_Mobile2", DbType.Int64, customerVo.Mobile2);
                db.AddInParameter(createCustomerCmd, "@C_ISDFax", DbType.Int32, customerVo.ISDFax);
                db.AddInParameter(createCustomerCmd, "@C_STDFax", DbType.Int32, customerVo.STDFax);
                db.AddInParameter(createCustomerCmd, "@C_Fax", DbType.Int32, customerVo.Fax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxISD", DbType.Int32, customerVo.OfcISDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxSTD", DbType.Int32, customerVo.OfcSTDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFax", DbType.Int32, customerVo.OfcFax);
                db.AddInParameter(createCustomerCmd, "@XO_OccupationCode", DbType.String, customerVo.Occupation);
                db.AddInParameter(createCustomerCmd, "@XQ_QualificationCode", DbType.String, customerVo.Qualification);
                db.AddInParameter(createCustomerCmd, "@C_MarriageDate", DbType.DateTime, customerVo.MarriageDate);
                db.AddInParameter(createCustomerCmd, "@XMS_MaritalStatusCode", DbType.String, customerVo.MaritalStatus);
                db.AddInParameter(createCustomerCmd, "@XN_NationalityCode", DbType.String, customerVo.Nationality);
                db.AddInParameter(createCustomerCmd, "@C_RBIRefNum", DbType.String, customerVo.RBIRefNum);
                if (customerVo.RBIApprovalDate != DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_RBIApprovalDate", DbType.DateTime, customerVo.RBIApprovalDate);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_RBIApprovalDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(createCustomerCmd, "@C_CompanyName", DbType.String, customerVo.CompanyName);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrLine1", DbType.String, customerVo.OfcAdrLine1);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrLine2", DbType.String, customerVo.OfcAdrLine2);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrLine3", DbType.String, customerVo.OfcAdrLine3);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrPinCode", DbType.Int32, customerVo.OfcAdrPinCode);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrCity", DbType.String, customerVo.OfcAdrCity);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrState", DbType.String, customerVo.OfcAdrState);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrCountry", DbType.String, customerVo.OfcAdrCountry);
                if (customerVo.JobStartDate == DateTime.MinValue || customerVo.JobStartDate == null)
                {
                    db.AddInParameter(createCustomerCmd, "@C_JobStartDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_JobStartDate", DbType.DateTime, customerVo.JobStartDate);
                }
                if (customerVo.RegistrationDate != DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_RegistrationDate", DbType.DateTime, customerVo.RegistrationDate);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_RegistrationDate", DbType.DateTime, DBNull.Value);
                }
                if (customerVo.CommencementDate != DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_CommencementDate", DbType.DateTime, customerVo.CommencementDate);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_CommencementDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(createCustomerCmd, "@C_RegistrationPlace", DbType.String, customerVo.RegistrationPlace);
                db.AddInParameter(createCustomerCmd, "@C_RegistrationNum", DbType.String, customerVo.RegistrationNum);
                db.AddInParameter(createCustomerCmd, "@C_CompanyWebsite", DbType.String, customerVo.CompanyWebsite);
                db.AddInParameter(createCustomerCmd, "@C_MothersMaidenName", DbType.String, customerVo.MothersMaidenName);
                db.AddInParameter(createCustomerCmd, "@C_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerCmd, "@C_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createCustomerCmd, "@CustomerId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createCustomerCmd) != 0)

                    customerId = int.Parse(db.GetParameterValue(createCustomerCmd, "CustomerId").ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:CreateCustomer()");


                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerId;
        }

        /// <summary>
        /// It gets completes Customer Details and Assigned it to CustomerVo
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerVo GetCustomer(int customerId)
        {
            CustomerVo customerVo = null;
            Database db;
            DbCommand getCustomerCmd;
            DataSet getCustomerDs;
            DataRow dr;

            try
            {
                customerVo = new CustomerVo();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerCmd = db.GetStoredProcCommand("SP_GetCustomer");
                db.AddInParameter(getCustomerCmd, "@C_CustomerId", DbType.String, customerId.ToString());
                getCustomerDs = db.ExecuteDataSet(getCustomerCmd);
                if (getCustomerDs.Tables[0].Rows.Count > 0)
                {
                    dr = getCustomerDs.Tables[0].Rows[0];
                    if (dr["C_ProfilingDate"].ToString() == DateTime.MinValue.ToString() || dr["C_ProfilingDate"].ToString() == "")
                    {
                        customerVo.ProfilingDate = DateTime.MinValue;
                    }
                    else
                        customerVo.ProfilingDate = DateTime.Parse(dr["C_ProfilingDate"].ToString());
                    customerVo.CustCode = dr["C_CustCode"].ToString();
                    customerVo.CustomerId = customerId;
                    customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                    if (dr["AB_BranchId"].ToString() != "")
                        customerVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    else
                        customerVo.BranchId = 0;
                    customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                    customerVo.FirstName = dr["C_FirstName"].ToString();
                    customerVo.MiddleName = dr["C_MiddleName"].ToString();
                    if (dr["C_IsDummyPAN"] != null && dr["C_IsDummyPAN"] != "")
                    {
                        customerVo.DummyPAN = int.Parse(dr["C_IsDummyPAN"].ToString());
                    }
                    else
                    {
                        customerVo.DummyPAN = 0;
                    }
                    if (dr["C_IsActive"] != null && dr["C_IsActive"] != "")
                    {

                        customerVo.IsActive = int.Parse(dr["C_IsActive"].ToString());

                    }
                    else
                    {
                        customerVo.IsActive = 0;
                    }
                    if (dr["C_AlertViaSMS"] == null)
                    {

                        customerVo.ViaSMS = 0;

                    }
                    else
                    {
                        customerVo.ViaSMS = int.Parse(dr["C_AlertViaSMS"].ToString());


                    }
                    if (dr["C_AlertViaEmail"] == null)
                    {
                        customerVo.AlertViaEmail = 0;


                    }
                    else
                    {
                        customerVo.AlertViaEmail = int.Parse(dr["C_AlertViaEmail"].ToString());

                    }
                    if (dr["C_IsProspect"] == null)
                    {
                        customerVo.IsProspect = 0;


                    }
                    else
                    {
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());

                    }
                    customerVo.AdviseNote = dr["C_Comments"].ToString();
                    if (!string.IsNullOrEmpty(dr["ACC_CustomerClassificationId"].ToString()))
                    {

                        customerVo.CustomerClassificationID = int.Parse(dr["ACC_CustomerClassificationId"].ToString());

                    }
                    else
                    {
                        customerVo.CustomerClassificationID = '0';
                    }

                    customerVo.LastName = dr["C_LastName"].ToString();
                    customerVo.Gender = dr["C_Gender"].ToString();
                    customerVo.BranchName = dr["AB_BranchName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_DOB"].ToString()))
                        customerVo.Dob = Convert.ToDateTime(dr["C_DOB"].ToString());
                    //customerVo.Dob = DateTime.Parse(dr["C_DOB"].ToString());

                    customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                    customerVo.SubType = dr["XCST_CustomerSubTypeCode"].ToString();
                    customerVo.Salutation = dr["C_Salutation"].ToString();
                    customerVo.PANNum = dr["C_PANNum"].ToString();
                    customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                    customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                    customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                    customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                    customerVo.Adr1City = dr["C_Adr1City"].ToString();
                    customerVo.Adr1State = dr["C_Adr1State"].ToString();
                    customerVo.Adr1Country = dr["C_Adr1Country"].ToString();
                    customerVo.Adr2Line1 = dr["C_Adr2Line1"].ToString();
                    customerVo.Adr2Line2 = dr["C_Adr2Line2"].ToString();
                    customerVo.Adr2Line3 = dr["C_Adr2Line3"].ToString();
                    customerVo.Adr2PinCode = int.Parse(dr["C_Adr2PinCode"].ToString());
                    customerVo.Adr2City = dr["C_Adr2City"].ToString();
                    customerVo.Adr2State = dr["C_Adr2State"].ToString();
                    customerVo.Adr2Country = dr["C_Adr2Country"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_ResidenceLivingDate"].ToString()))
                        customerVo.ResidenceLivingDate = Convert.ToDateTime(dr["C_ResidenceLivingDate"].ToString());
                    customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                    customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                    customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                    customerVo.OfcISDCode = int.Parse(dr["C_OfcISDCode"].ToString());
                    customerVo.OfcSTDCode = int.Parse(dr["C_OfcSTDCode"].ToString());
                    customerVo.OfcPhoneNum = int.Parse(dr["C_OfcPhoneNum"].ToString());
                    customerVo.Email = dr["C_Email"].ToString();
                    customerVo.AltEmail = dr["C_AltEmail"].ToString();
                    customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                    customerVo.Mobile2 = long.Parse(dr["C_Mobile2"].ToString());
                    customerVo.ISDFax = int.Parse(dr["C_ISDFax"].ToString());
                    customerVo.STDFax = int.Parse(dr["C_STDFax"].ToString());
                    customerVo.Fax = int.Parse(dr["C_Fax"].ToString());
                    customerVo.OfcISDFax = int.Parse(dr["C_OfcFaxISD"].ToString());
                    customerVo.OfcSTDFax = int.Parse(dr["C_OfcFaxSTD"].ToString());
                    customerVo.OfcFax = int.Parse(dr["C_OfcFax"].ToString());
                    customerVo.Occupation = dr["XO_OccupationCode"].ToString();
                    customerVo.Qualification = dr["XQ_QualificationCode"].ToString();
                    customerVo.MaritalStatus = dr["XMS_MaritalStatusCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_MarriageDate"].ToString()))
                        customerVo.MarriageDate = Convert.ToDateTime(dr["C_MarriageDate"].ToString());
                    customerVo.Nationality = dr["XN_NationalityCode"].ToString();
                    customerVo.RBIRefNum = dr["C_RBIRefNum"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_RBIApprovalDate"].ToString()))
                        customerVo.RBIApprovalDate = Convert.ToDateTime(dr["C_RBIApprovalDate"].ToString());
                    customerVo.CompanyName = dr["C_CompanyName"].ToString();
                    customerVo.OfcAdrLine1 = dr["C_OfcAdrLine1"].ToString();
                    customerVo.OfcAdrLine2 = dr["C_OfcAdrLine2"].ToString();
                    customerVo.OfcAdrLine3 = dr["C_OfcAdrLine3"].ToString();
                    customerVo.OfcAdrPinCode = int.Parse(dr["C_OfcAdrPinCode"].ToString());
                    customerVo.OfcAdrCity = dr["C_OfcAdrCity"].ToString();
                    customerVo.OfcAdrState = dr["C_OfcAdrState"].ToString();
                    customerVo.OfcAdrCountry = dr["C_OfcAdrCountry"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_JobStartDate"].ToString()))
                        customerVo.JobStartDate = Convert.ToDateTime(dr["C_JobStartDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_RegistrationDate"].ToString()))
                        customerVo.RegistrationDate = Convert.ToDateTime(dr["C_RegistrationDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_CommencementDate"].ToString()))
                        customerVo.CommencementDate = Convert.ToDateTime(dr["C_CommencementDate"].ToString());
                    customerVo.RegistrationPlace = dr["C_RegistrationPlace"].ToString();
                    customerVo.RegistrationNum = dr["C_RegistrationNum"].ToString();
                    customerVo.CompanyWebsite = dr["C_CompanyWebsite"].ToString();
                    customerVo.ContactMiddleName = dr["C_ContactGuardianMiddleName"].ToString();
                    customerVo.ContactFirstName = dr["C_ContactGuardianFirstName"].ToString();
                    customerVo.ContactLastName = dr["C_ContactGuardianLastName"].ToString();
                    customerVo.MothersMaidenName = dr["C_MothersMaidenName"].ToString();
                    if (dr["CA_AssociationId"].ToString() != string.Empty)
                        customerVo.AssociationId = int.Parse(dr["CA_AssociationId"].ToString());
                    if (dr["XR_RelationshipCode"].ToString() != string.Empty)
                        customerVo.RelationShip = dr["XR_RelationshipCode"].ToString();
                    customerVo.ParentCustomer = dr["ParentCustomer"].ToString();
                    customerVo.RMName = dr["AR_RM_FullName"].ToString();
                    if (!string.IsNullOrEmpty(dr["AR_RM_Email"].ToString()))
                        customerVo.RMEmail = dr["AR_RM_Email"].ToString();
                    else
                        customerVo.RMEmail = "";
                    if (!string.IsNullOrEmpty(dr["AR_RM_OffiePhone"].ToString()))
                        customerVo.RMOfficePhone = dr["AR_RM_OffiePhone"].ToString();
                    else
                        customerVo.RMOfficePhone = string.Empty;

                    if (!string.IsNullOrEmpty(dr["AR_RM_Moble"].ToString()))
                        customerVo.RMMobile = long.Parse(dr["AR_RM_Moble"].ToString());
                    else
                        customerVo.RMMobile = 0;
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomer()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerVo;
        }


        public int CreateCustomerUser(CustomerVo customerVo, int userId)
        {

            int custUserId = 0;
            UserVo userVo = new UserVo();
            UserDao userDao = new UserDao();

            try
            {
                //userVo.LoginId = customerVo.Email.ToString();
                userVo.UserId = userId;
                userVo.Email = customerVo.Email.ToString();
                if (customerVo.Type == "IND")
                {
                    userVo.FirstName = customerVo.FirstName.ToString();
                    userVo.LastName = customerVo.LastName.ToString();
                    userVo.MiddleName = customerVo.MiddleName.ToString();
                }
                else if (customerVo.Type == "NIND")
                {
                    userVo.FirstName = customerVo.CompanyName.ToString();
                }
                //userVo.Password = id.Next(10000, 99999).ToString();
                userVo.UserType = "Customer";
                custUserId = userDao.CreateUser(userVo);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:CreateCustomerUser()");


                object[] objects = new object[2];
                objects[0] = customerVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return custUserId;
        }

        /// <summary>
        /// Gets CustomerInfo if you pass UserId as its Parameter
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CustomerVo GetCustomerInfo(int userId)
        {

            CustomerVo customerVo = null;
            Database db;
            DbCommand getCustomerUserCmd;
            DataSet getCustomerUserDs;
            DataRow dr;

            try
            {
                customerVo = new CustomerVo();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerUserCmd = db.GetStoredProcCommand("SP_GetCustomerInfo");
                db.AddInParameter(getCustomerUserCmd, "@U_UserId", DbType.Int32, userId);
                getCustomerUserDs = db.ExecuteDataSet(getCustomerUserCmd);
                if (getCustomerUserDs.Tables[0].Rows.Count > 0)
                {
                    dr = getCustomerUserDs.Tables[0].Rows[0];

                    if (dr["C_ProfilingDate"].ToString() == DateTime.MinValue.ToString() || dr["C_ProfilingDate"].ToString() == "")
                    {
                        customerVo.ProfilingDate = DateTime.MinValue;
                    }
                    else
                        customerVo.ProfilingDate = DateTime.Parse(dr["C_ProfilingDate"].ToString());
                    customerVo.CustCode = dr["C_CustCode"].ToString();
                    customerVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    if (dr["AR_RMId"].ToString() != "")
                    {
                        customerVo.RmId = int.Parse(dr["AR_RMId"].ToString());
                    }
                    customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                    customerVo.FirstName = dr["C_FirstName"].ToString();
                    customerVo.MiddleName = dr["C_MiddleName"].ToString();
                    customerVo.LastName = dr["C_LastName"].ToString();
                    customerVo.Gender = dr["C_Gender"].ToString();
                    if (dr["C_DOB"].ToString() == DateTime.MinValue.ToString() || dr["C_DOB"].ToString() == "")
                    {
                        customerVo.Dob = DateTime.MinValue;
                    }
                    else
                        customerVo.Dob = DateTime.Parse(dr["C_DOB"].ToString());

                    customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                    customerVo.SubType = dr["XCST_CustomerSubTypeCode"].ToString();
                    customerVo.Salutation = dr["C_Salutation"].ToString();
                    customerVo.PANNum = dr["C_PANNum"].ToString();
                    customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                    customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                    customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                    customerVo.Adr1PinCode = int.Parse(dr["C_Adr1PinCode"].ToString());
                    customerVo.Adr1City = dr["C_Adr1City"].ToString();
                    customerVo.Adr1State = dr["C_Adr1State"].ToString();
                    customerVo.Adr1Country = dr["C_Adr1Country"].ToString();
                    customerVo.Adr2Line1 = dr["C_Adr2Line1"].ToString();
                    customerVo.Adr2Line2 = dr["C_Adr2Line2"].ToString();
                    customerVo.Adr2Line3 = dr["C_Adr2Line3"].ToString();
                    customerVo.Adr2PinCode = int.Parse(dr["C_Adr2PinCode"].ToString());
                    customerVo.Adr2City = dr["C_Adr2City"].ToString();
                    customerVo.Adr2State = dr["C_Adr2State"].ToString();
                    customerVo.Adr2Country = dr["C_Adr2Country"].ToString();
                    customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                    customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                    customerVo.ResPhoneNum = int.Parse(dr["C_ResPhoneNum"].ToString());
                    customerVo.OfcISDCode = int.Parse(dr["C_OfcISDCode"].ToString());
                    customerVo.OfcSTDCode = int.Parse(dr["C_OfcSTDCode"].ToString());
                    customerVo.OfcPhoneNum = int.Parse(dr["C_OfcPhoneNum"].ToString());
                    customerVo.Email = dr["C_Email"].ToString();
                    customerVo.AltEmail = dr["C_AltEmail"].ToString();
                    customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                    customerVo.Mobile2 = long.Parse(dr["C_Mobile2"].ToString());
                    customerVo.ISDFax = int.Parse(dr["C_ISDFax"].ToString());
                    customerVo.STDFax = int.Parse(dr["C_STDFax"].ToString());
                    customerVo.Fax = int.Parse(dr["C_Fax"].ToString());
                    customerVo.OfcISDFax = int.Parse(dr["C_OfcFaxISD"].ToString());
                    customerVo.OfcSTDFax = int.Parse(dr["C_OfcFaxSTD"].ToString());
                    customerVo.OfcFax = int.Parse(dr["C_OfcFax"].ToString());
                    customerVo.Occupation = dr["XO_OccupationCode"].ToString();
                    customerVo.Qualification = dr["XQ_QualificationCode"].ToString();
                    customerVo.MaritalStatus = dr["XMS_MaritalStatusCode"].ToString();
                    customerVo.Nationality = dr["XN_NationalityCode"].ToString();
                    customerVo.RBIRefNum = dr["C_RBIRefNum"].ToString();
                    if (dr["C_RBIApprovalDate"].ToString() != "")
                        customerVo.RBIApprovalDate = Convert.ToDateTime(dr["C_RBIApprovalDate"].ToString());
                    customerVo.CompanyName = dr["C_CompanyName"].ToString();
                    customerVo.OfcAdrLine1 = dr["C_OfcAdrLine1"].ToString();
                    customerVo.OfcAdrLine2 = dr["C_OfcAdrLine2"].ToString();
                    customerVo.OfcAdrLine3 = dr["C_OfcAdrLine3"].ToString();
                    customerVo.OfcAdrPinCode = int.Parse(dr["C_OfcAdrPinCode"].ToString());
                    customerVo.OfcAdrCity = dr["C_OfcAdrCity"].ToString();
                    customerVo.OfcAdrState = dr["C_OfcAdrState"].ToString();
                    customerVo.OfcAdrCountry = dr["C_OfcAdrCountry"].ToString();
                    if (dr["C_RegistrationDate"].ToString() != "")
                        customerVo.RegistrationDate = Convert.ToDateTime(dr["C_RegistrationDate"].ToString());
                    if (dr["C_CommencementDate"].ToString() != "")
                        customerVo.CommencementDate = Convert.ToDateTime(dr["C_CommencementDate"].ToString());
                    customerVo.RegistrationPlace = dr["C_RegistrationPlace"].ToString();
                    customerVo.RegistrationNum = dr["C_RegistrationNum"].ToString();
                    customerVo.CompanyWebsite = dr["C_CompanyWebsite"].ToString();
                    customerVo.ContactMiddleName = dr["C_ContactGuardianMiddleName"].ToString();
                    customerVo.ContactFirstName = dr["C_ContactGuardianFirstName"].ToString();
                    customerVo.ContactLastName = dr["C_ContactGuardianLastName"].ToString();
                    customerVo.MothersMaidenName = dr["C_MothersMaidenName"].ToString();
                    if (dr["AB_BranchId"].ToString() != string.Empty)
                        customerVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    if (dr["XR_RelationshipCode"].ToString() != string.Empty)
                        customerVo.RelationShip = dr["XR_RelationshipCode"].ToString();
                    customerVo.ParentCustomer = dr["ParentCustomer"].ToString();
                    if (dr["AB_BranchName"] != null)
                        customerVo.BranchName = dr["AB_BranchName"].ToString();
                    if (!string.IsNullOrEmpty(dr["CA_AssociationId"].ToString()))
                        customerVo.AssociationId = int.Parse(dr["CA_AssociationId"].ToString());
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerInfo()");


                object[] objects = new object[1];
                objects[0] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return customerVo;

        }


        /// <summary>
        /// Gets only Customer Name
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string GetCustomerUsername(int customerId)
        {
            string userName = "";
            Database db;
            DbCommand getUserNameCmd;

            string query = "";
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                query = "select C_Email from CustomerMaster where C_CustomerId=" + customerId;

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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerUsername()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return userName;
        }

        /// <summary>
        /// It will get you Complete information of the Customer if you pass Firstname of the Customer as its Parameter
        /// </summary>
        /// <param name="advisorId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataSet SearchCustomers(int advisorId, string name)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_CustomerSearch");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, advisorId);
                db.AddInParameter(cmd, "@FirstName", DbType.String, name);
                //db.AddInParameter(cmd, "@LastName", DbType.String, lastName);

                ds = db.ExecuteDataSet(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomers()");

                object[] objects = new object[2];
                objects[0] = advisorId;
                objects[1] = name;
                //objects[2] = lastName;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;


        }


        /// <summary>
        /// Gets you Firstname middlename lastname emailid password detals for a particular Customer from Usertable if you pass Customer Id as its Parameter
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public UserVo GetCustomerUser(int customerId)
        {
            UserVo userVo = null;
            Database db;
            DbCommand getCustomerUserInfoCmd;
            DataSet getCustomerUserInfoDs;

            DataRow dr;
            try
            {
                userVo = new UserVo();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerUserInfoCmd = db.GetStoredProcCommand("SP_GetCustomerUser");
                getCustomerUserInfoDs = db.ExecuteDataSet(getCustomerUserInfoCmd);
                if (getCustomerUserInfoDs.Tables[0].Rows.Count > 0)
                {


                    dr = getCustomerUserInfoDs.Tables[0].Rows[0];
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerUser()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return userVo;
        }


        /// <summary>
        /// It Updates Complete customer Details if you pass complete CustomerVo as a Parameter
        /// </summary>
        /// <param name="customerVo"></param>
        /// <returns></returns>
        public bool UpdateCustomer(CustomerVo customerVo)
        {
            bool bResult = false;
            Database db;
            DbCommand editCustomerCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                editCustomerCmd = db.GetStoredProcCommand("SP_UpdateCustomer");

                db.AddInParameter(editCustomerCmd, "@C_CustomerId", DbType.Int32, customerVo.CustomerId);
                db.AddInParameter(editCustomerCmd, "@AB_BranchId", DbType.Int32, customerVo.BranchId);
                db.AddInParameter(editCustomerCmd, "@C_FirstName", DbType.String, customerVo.FirstName);
                db.AddInParameter(editCustomerCmd, "@C_MiddleName", DbType.String, customerVo.MiddleName);
                db.AddInParameter(editCustomerCmd, "@C_LastName", DbType.String, customerVo.LastName);
                db.AddInParameter(editCustomerCmd, "@C_Gender", DbType.String, customerVo.Gender);
                db.AddInParameter(editCustomerCmd, "@XCT_CustomerTypeCode", DbType.String, customerVo.Type);
                if (customerVo.SubType != "")
                    db.AddInParameter(editCustomerCmd, "@XCST_CustomerSubTypeCode", DbType.String, customerVo.SubType);
                else
                    db.AddInParameter(editCustomerCmd, "@XCST_CustomerSubTypeCode", DbType.String, DBNull.Value);


                db.AddInParameter(editCustomerCmd, "@C_DummyPAN", DbType.String, customerVo.DummyPAN);
                db.AddInParameter(editCustomerCmd, "@C_IsProspect", DbType.String, customerVo.IsProspect);
                db.AddInParameter(editCustomerCmd, "@C_mail", DbType.String, customerVo.AlertViaEmail);
                db.AddInParameter(editCustomerCmd, "@C_sms", DbType.String, customerVo.ViaSMS);

                db.AddInParameter(editCustomerCmd, "@C_Salutation", DbType.String, customerVo.Salutation);
                db.AddInParameter(editCustomerCmd, "@C_PANNum", DbType.String, customerVo.PANNum);
                if (customerVo.ProfilingDate != DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_ProfilingDate", DbType.DateTime, customerVo.ProfilingDate);
                else
                    db.AddInParameter(editCustomerCmd, "@C_ProfilingDate", DbType.DateTime, DBNull.Value);
                if (customerVo.Dob != DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_DOB", DbType.DateTime, customerVo.Dob);
                else
                    db.AddInParameter(editCustomerCmd, "@C_DOB", DbType.DateTime, DBNull.Value);
                db.AddInParameter(editCustomerCmd, "@C_CustCode", DbType.String, customerVo.CustCode);
                db.AddInParameter(editCustomerCmd, "@C_Adr1Line1", DbType.String, customerVo.Adr1Line1);
                db.AddInParameter(editCustomerCmd, "@C_Adr1Line2", DbType.String, customerVo.Adr1Line2);
                db.AddInParameter(editCustomerCmd, "@C_Adr1Line3", DbType.String, customerVo.Adr1Line3);
                db.AddInParameter(editCustomerCmd, "@C_Adr1PinCode", DbType.Int32, customerVo.Adr1PinCode);
                db.AddInParameter(editCustomerCmd, "@C_Adr1City", DbType.String, customerVo.Adr1City);
                if (customerVo.Adr1State != "Select a State")
                    db.AddInParameter(editCustomerCmd, "@C_Adr1State", DbType.String, customerVo.Adr1State);
                else
                    db.AddInParameter(editCustomerCmd, "@C_Adr1State", DbType.String, "");
                db.AddInParameter(editCustomerCmd, "@C_Adr1Country", DbType.String, customerVo.Adr1Country);
                db.AddInParameter(editCustomerCmd, "@C_Adr2Line1", DbType.String, customerVo.Adr2Line1);
                db.AddInParameter(editCustomerCmd, "@C_Adr2Line2", DbType.String, customerVo.Adr2Line2);
                db.AddInParameter(editCustomerCmd, "@C_Adr2Line3", DbType.String, customerVo.Adr2Line3);
                db.AddInParameter(editCustomerCmd, "@C_Adr2PinCode", DbType.Int32, customerVo.Adr2PinCode);
                db.AddInParameter(editCustomerCmd, "@C_Adr2City", DbType.String, customerVo.Adr2City);
                if (customerVo.Adr2State != "Select a State")
                    db.AddInParameter(editCustomerCmd, "@C_Adr2State", DbType.String, customerVo.Adr2State);
                else
                    db.AddInParameter(editCustomerCmd, "@C_Adr2State", DbType.String, "");
                db.AddInParameter(editCustomerCmd, "@C_Adr2Country", DbType.String, customerVo.Adr2Country);
                if (customerVo.ResidenceLivingDate != DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_ResidenceLivingDate", DbType.DateTime, customerVo.ResidenceLivingDate);
                else
                    db.AddInParameter(editCustomerCmd, "@C_ResidenceLivingDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(editCustomerCmd, "@C_ResISDCode", DbType.Int32, customerVo.ResISDCode);
                db.AddInParameter(editCustomerCmd, "@C_ResSTDCode", DbType.Int32, customerVo.ResSTDCode);
                db.AddInParameter(editCustomerCmd, "@C_ResPhoneNum", DbType.Int32, customerVo.ResPhoneNum);
                db.AddInParameter(editCustomerCmd, "@C_OfcISDCode", DbType.Int32, customerVo.OfcISDCode);
                db.AddInParameter(editCustomerCmd, "@C_OfcSTDCode", DbType.Int32, customerVo.OfcSTDCode);
                db.AddInParameter(editCustomerCmd, "@C_OfcPhoneNum", DbType.Int32, customerVo.OfcPhoneNum);
                db.AddInParameter(editCustomerCmd, "@C_Email", DbType.String, customerVo.Email);
                db.AddInParameter(editCustomerCmd, "@C_AltEmail", DbType.String, customerVo.AltEmail);
                db.AddInParameter(editCustomerCmd, "@C_Mobile1", DbType.Int64, customerVo.Mobile1);
                db.AddInParameter(editCustomerCmd, "@C_Mobile2", DbType.Int64, customerVo.Mobile2);
                db.AddInParameter(editCustomerCmd, "@C_ISDFax", DbType.Int32, customerVo.ISDFax);
                db.AddInParameter(editCustomerCmd, "@C_STDFax", DbType.Int32, customerVo.STDFax);
                db.AddInParameter(editCustomerCmd, "@C_Fax", DbType.Int32, customerVo.Fax);
                db.AddInParameter(editCustomerCmd, "@C_OfcFaxISD", DbType.Int32, customerVo.OfcISDFax);
                db.AddInParameter(editCustomerCmd, "@C_OfcFaxSTD", DbType.Int32, customerVo.OfcSTDFax);
                db.AddInParameter(editCustomerCmd, "@C_OfcFax", DbType.Int32, customerVo.OfcFax);
                db.AddInParameter(editCustomerCmd, "@C_ContactGuardianFirstName", DbType.String, customerVo.ContactFirstName);
                db.AddInParameter(editCustomerCmd, "@C_ContactGuardianMiddleName", DbType.String, customerVo.ContactMiddleName);
                db.AddInParameter(editCustomerCmd, "@C_ContactGuardianLastName", DbType.String, customerVo.ContactLastName);



                if (customerVo.Occupation != "Select a Occupation" && customerVo.Occupation != "")
                    db.AddInParameter(editCustomerCmd, "@XO_OccupationCode", DbType.String, customerVo.Occupation);
                else
                    db.AddInParameter(editCustomerCmd, "@XO_OccupationCode", DbType.String, DBNull.Value);

                if (customerVo.Qualification != "Select a Qualification" && customerVo.Qualification != "")
                    db.AddInParameter(editCustomerCmd, "@XQ_QualificationCode", DbType.String, customerVo.Qualification);
                else
                    db.AddInParameter(editCustomerCmd, "@XQ_QualificationCode", DbType.String, DBNull.Value);

                if (customerVo.MaritalStatus != "Select a Status" && customerVo.MaritalStatus != "")
                    db.AddInParameter(editCustomerCmd, "@XMS_MaritalStatusCode", DbType.String, customerVo.MaritalStatus);
                else
                    db.AddInParameter(editCustomerCmd, "@XMS_MaritalStatusCode", DbType.String, DBNull.Value);
                if (customerVo.MarriageDate != DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_MarriageDate", DbType.DateTime, customerVo.MarriageDate);
                else
                    db.AddInParameter(editCustomerCmd, "@C_MarriageDate", DbType.DateTime, DBNull.Value);

                if (customerVo.Nationality != "Select a Nationality" && customerVo.Nationality != "")
                    db.AddInParameter(editCustomerCmd, "@XN_NationalityCode", DbType.String, customerVo.Nationality);
                else
                    db.AddInParameter(editCustomerCmd, "@XN_NationalityCode", DbType.String, DBNull.Value);

                db.AddInParameter(editCustomerCmd, "@C_RBIRefNum", DbType.String, customerVo.RBIRefNum);
                //  if (customerVo.RBIApprovalDate != null)
                if (customerVo.RBIApprovalDate != DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_RBIApprovalDate", DbType.DateTime, customerVo.RBIApprovalDate);
                else
                    db.AddInParameter(editCustomerCmd, "@C_RBIApprovalDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(editCustomerCmd, "@C_CompanyName", DbType.String, customerVo.CompanyName);
                db.AddInParameter(editCustomerCmd, "@C_OfcAdrLine1", DbType.String, customerVo.OfcAdrLine1);
                db.AddInParameter(editCustomerCmd, "@C_OfcAdrLine2", DbType.String, customerVo.OfcAdrLine2);
                db.AddInParameter(editCustomerCmd, "@C_OfcAdrLine3", DbType.String, customerVo.OfcAdrLine3);
                db.AddInParameter(editCustomerCmd, "@C_OfcAdrPinCode", DbType.Int32, customerVo.OfcAdrPinCode);
                db.AddInParameter(editCustomerCmd, "@C_OfcAdrCity", DbType.String, customerVo.OfcAdrCity);
                if (customerVo.OfcAdrState != "Select a State")
                    db.AddInParameter(editCustomerCmd, "@C_OfcAdrState", DbType.String, customerVo.OfcAdrState);
                else
                    db.AddInParameter(editCustomerCmd, "@C_OfcAdrState", DbType.String, "");

                if (customerVo.OfcAdrCountry != "Select a Country")
                    db.AddInParameter(editCustomerCmd, "@C_OfcAdrCountry", DbType.String, customerVo.OfcAdrCountry);
                else
                    db.AddInParameter(editCustomerCmd, "@C_OfcAdrCountry", DbType.String, "");


                if (customerVo.JobStartDate != DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_JobStartDate", DbType.DateTime, customerVo.JobStartDate);
                else
                    db.AddInParameter(editCustomerCmd, "@C_JobStartDate", DbType.DateTime, DBNull.Value);

                if (customerVo.RegistrationDate == DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_RegistrationDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(editCustomerCmd, "@C_RegistrationDate", DbType.DateTime, customerVo.RegistrationDate);
                if (customerVo.CommencementDate == DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_CommencementDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(editCustomerCmd, "@C_CommencementDate", DbType.DateTime, customerVo.CommencementDate);
                db.AddInParameter(editCustomerCmd, "@C_RegistrationPlace", DbType.String, customerVo.RegistrationPlace);
                db.AddInParameter(editCustomerCmd, "@C_RegistrationNum", DbType.String, customerVo.RegistrationNum);
                db.AddInParameter(editCustomerCmd, "@C_CompanyWebsite", DbType.String, customerVo.CompanyWebsite);
                db.AddInParameter(editCustomerCmd, "@C_MothersMaidenName", DbType.String, customerVo.MothersMaidenName);
                db.AddInParameter(editCustomerCmd, "@C_AdvNote", DbType.String, customerVo.AdviseNote);
                db.AddInParameter(editCustomerCmd, "@C_IsAct", DbType.Int32, customerVo.IsActive);
                db.AddInParameter(editCustomerCmd, "@C_ClassCode", DbType.Int32, customerVo.CustomerClassificationID);

                if (db.ExecuteNonQuery(editCustomerCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerDao.cs:UpdateCustomer()");

                object[] objects = new object[1];
                objects[0] = customerVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }


        /// <summary>
        /// It Gets you Asset Code about Asset Class
        /// </summary>
        /// <param name="AssetType"></param>
        /// <param name="CustomerType"></param>
        /// <param name="KYCFlag"></param>
        /// <returns></returns>
        public string GetAssestCode(string AssetType, string CustomerType, int KYCFlag)
        {
            string filtercategory = "";
            Database db;
            DataSet dsAssetCode;
            DbCommand getAssetCode;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetCode = db.GetStoredProcCommand("SP_GetProfileFilterCategory");

                db.AddInParameter(getAssetCode, "@WPFC_AssetClass", DbType.String, AssetType);
                db.AddInParameter(getAssetCode, "@XCT_CustomerTypeCode", DbType.String, CustomerType);
                db.AddInParameter(getAssetCode, "@WPFC_KYFCompliantFlag", DbType.Int16, KYCFlag);

                dsAssetCode = db.ExecuteDataSet(getAssetCode);
                if (dsAssetCode.Tables[0].Rows.Count > 0)
                {
                    filtercategory = dsAssetCode.Tables[0].Rows[0][0].ToString();
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAssestCode()");


                object[] objects = new object[3];
                objects[0] = AssetType;
                objects[1] = CustomerType;
                objects[2] = KYCFlag;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return filtercategory;
        }


        /// <summary>
        /// It Gets you Proof List Details if you pass FilterCategory as its Parameter
        /// </summary>
        /// <param name="filterCategory"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetProofList(string filterCategory, string path)
        {
            Database db;
            string proofidvalue = "";
            DbCommand cmdGetIdCode;
            DataSet dsIdCode;
            DataSet ds;
            DataTable dtIdCode;
            List<string> lookups = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                //query = "Select WIPL_MandatoryIdentifiercode from WerpInvestmentProofLookup where WIPL_FilterCategory='" + filterCategory + "'";

                cmdGetIdCode = db.GetStoredProcCommand("SP_GetProofCodes");
                db.AddInParameter(cmdGetIdCode, "WPFC_FilterCategoryCode", DbType.String, filterCategory);
                dsIdCode = db.ExecuteDataSet(cmdGetIdCode);

                if (dsIdCode.Tables[0].Rows.Count > 0)
                {
                    //To Read data from the xml file 
                    ds = new DataSet();
                    //to replace with the xml lookup file for MF and Equity 
                    ds.ReadXml(path);

                    if (ds.Tables.Count > 0)
                    {
                        dtIdCode = dsIdCode.Tables[0];
                        lookups = new List<string>();
                        foreach (DataRow drc in dtIdCode.Rows)
                        {
                            DataRow[] rows = ds.Tables["Proof"].Select(" ProofCode = '" + drc["XP_ProofCode"] + "'"); //replace investment with the name of the table in lookup 
                            foreach (DataRow dr in rows)
                            {
                                proofidvalue = dr["ProofName"].ToString(); // to replace the Name for the new xml file 
                                lookups.Add(proofidvalue);
                            }
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetProofList()");


                object[] objects = new object[2];
                objects[0] = filterCategory;
                objects[1] = path;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return lookups;
        }


        /// <summary>
        /// It inserts Customer Proofs in to Database
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerProof"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SaveCustomerProofs(int customerId, int customerProof, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand saveCustomerProofsCmd;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                saveCustomerProofsCmd = db.GetStoredProcCommand("SP_SaveCustomerProofs");
                db.AddInParameter(saveCustomerProofsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(saveCustomerProofsCmd, "@XP_ProofCode", DbType.Int32, customerProof);
                db.AddInParameter(saveCustomerProofsCmd, "@CP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(saveCustomerProofsCmd, "@CP_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(saveCustomerProofsCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerDao.cs:SaveCustomerProofs()");


                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = customerProof;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        /// <summary>
        /// It Gets Customer Proof Code if you Pass Proofs and its location as its Paramaeter
        /// </summary>
        /// <param name="proof"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetCustomerProofCode(string proof, string path)
        {
            int proofCode = 0;
            DataSet ds = new DataSet();
            DataRow[] dr;
            try
            {
                ds = new DataSet();
                ds.ReadXml(path);
                if (ds.Tables.Count != 0)
                {
                    dr = ds.Tables["Proof"].Select("ProofName ='" + proof + "'");
                    proofCode = int.Parse(dr[0][0].ToString());
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofCode()");


                object[] objects = new object[2];
                objects[0] = proof;
                objects[1] = path;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return proofCode;
        }


        /// <summary>
        /// It Gets Complete Customer Proofs for the Particular Customer if you Pass Customer Id as its Parameter
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="currentPage"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public DataSet GetCustomerProofs(int customerId, int currentPage, out int Count)
        {


            Database db;
            DbCommand getCustomerProofsCmd;
            DataSet getCustomerProofsDs = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerProofsCmd = db.GetStoredProcCommand("SP_GetCustomerProof");
                db.AddInParameter(getCustomerProofsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getCustomerProofsCmd, "@CurrentPage", DbType.Int32, currentPage);

                getCustomerProofsDs = db.ExecuteDataSet(getCustomerProofsCmd);
                Count = int.Parse(getCustomerProofsDs.Tables[1].Rows[0][0].ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofs()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return getCustomerProofsDs;
        }



        /// <summary>
        /// It Deletes Complete Customer details for the Particular Customer if you Pass CustomerId as its Parameter
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int customerId, string Flag)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteCustomerBankCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCustomerBankCmd = db.GetStoredProcCommand("SP_DeleteCustomer");
                db.AddInParameter(deleteCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
                //db.AddInParameter(deleteCustomerBankCmd, "@U_UserId", DbType.Int32, userId);
                db.AddInParameter(deleteCustomerBankCmd, "@Flag", DbType.String, Flag);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:DeleteCustomer()");

                object[] objects = new object[2];
                objects[0] = customerId;
                //objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public int CustomerAssociation(string Flag, int CustomerId)
        {
            int associationcount = 0;
            // bool bResultAssociation = false;
            Database db;
            DbCommand deleteCustomerBankCmd;


            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCustomerBankCmd = db.GetStoredProcCommand("SP_DeleteCustomer");
                db.AddInParameter(deleteCustomerBankCmd, "@Flag", DbType.String, Flag);
                db.AddInParameter(deleteCustomerBankCmd, "@C_CustomerId", DbType.Int32, CustomerId);
                db.AddOutParameter(deleteCustomerBankCmd, "@CountFlag", DbType.Int32, 0);
                associationcount = db.ExecuteNonQuery(deleteCustomerBankCmd);
                associationcount = (int)db.GetParameterValue(deleteCustomerBankCmd, "@CountFlag");
                if (associationcount != 0)
                    return 1;
                else return 0;





            }

        }



        /// <summary>
        /// It Creates Complete Customer Profile in both Customer Table and also User Table if you pass CustomerVo,UserVo,CustomerPortfolioVo as its Parameter
        /// </summary>
        /// <param name="customerVo"></param>
        /// <param name="userVo"></param>
        /// <param name="customerPortfolioVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> CreateCompleteCustomer(CustomerVo customerVo, UserVo userVo, CustomerPortfolioVo customerPortfolioVo, int userId)
        {
            //bool bReturn = false;
            int customerId;
            int customerUserId;
            int customerPortfolioId;
            List<int> customerIds = new List<int>();
            Database db;
            DbCommand createCustomerCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerCmd = db.GetStoredProcCommand("SP_CreateCompleteCustomer");

                db.AddInParameter(createCustomerCmd, "@ADUL_ProcessId", DbType.Int32, customerVo.ProcessId);
                db.AddInParameter(createCustomerCmd, "@C_CustCode", DbType.String, customerVo.CustCode);
                if (customerVo.RmId != 100)
                {
                    db.AddInParameter(createCustomerCmd, "@AR_RMId", DbType.Int32, customerVo.RmId);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@AR_RMId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(createCustomerCmd, "@AB_BranchId", DbType.Int32, customerVo.BranchId);
                if (customerVo.ProfilingDate == DateTime.MinValue || customerVo.ProfilingDate == null)
                {
                    db.AddInParameter(createCustomerCmd, "@C_ProfilingDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_ProfilingDate", DbType.DateTime, customerVo.ProfilingDate);
                }

                db.AddInParameter(createCustomerCmd, "@C_FirstName", DbType.String, customerVo.FirstName);
                db.AddInParameter(createCustomerCmd, "@C_MiddleName", DbType.String, customerVo.MiddleName);
                db.AddInParameter(createCustomerCmd, "@C_LastName", DbType.String, customerVo.LastName);
                db.AddInParameter(createCustomerCmd, "@C_Gender", DbType.String, customerVo.Gender);
                if (customerVo.Dob == null || customerVo.Dob == DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_DOB", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_DOB", DbType.DateTime, customerVo.Dob);
                }
                db.AddInParameter(createCustomerCmd, "@XCT_CustomerTypeCode", DbType.String, customerVo.Type);
                db.AddInParameter(createCustomerCmd, "@XCST_CustomerSubTypeCode", DbType.String, customerVo.SubType);
                db.AddInParameter(createCustomerCmd, "@C_Salutation", DbType.String, customerVo.Salutation);
                db.AddInParameter(createCustomerCmd, "@C_PANNum", DbType.String, customerVo.PANNum);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Line1", DbType.String, customerVo.Adr1Line1);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Line2", DbType.String, customerVo.Adr1Line2);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Line3", DbType.String, customerVo.Adr1Line3);
                db.AddInParameter(createCustomerCmd, "@C_Adr1PinCode", DbType.Int32, customerVo.Adr1PinCode);
                db.AddInParameter(createCustomerCmd, "@C_Adr1City", DbType.String, customerVo.Adr1City);
                if (customerVo.Adr1State != "Select a State")
                    db.AddInParameter(createCustomerCmd, "@C_Adr1State", DbType.String, customerVo.Adr1State);
                else
                    db.AddInParameter(createCustomerCmd, "@C_Adr1State", DbType.String, "");
                db.AddInParameter(createCustomerCmd, "@C_Adr1Country", DbType.String, customerVo.Adr1Country);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line1", DbType.String, customerVo.Adr2Line1);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line2", DbType.String, customerVo.Adr2Line2);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line3", DbType.String, customerVo.Adr2Line3);
                db.AddInParameter(createCustomerCmd, "@C_Adr2PinCode", DbType.Int32, customerVo.Adr2PinCode);
                db.AddInParameter(createCustomerCmd, "@C_Adr2City", DbType.String, customerVo.Adr2City);
                if (customerVo.Adr2State != "Select a State")
                    db.AddInParameter(createCustomerCmd, "@C_Adr2State", DbType.String, customerVo.Adr2State);
                else
                    db.AddInParameter(createCustomerCmd, "@C_Adr2State", DbType.String, "");
                db.AddInParameter(createCustomerCmd, "@C_Adr2Country", DbType.String, customerVo.Adr2Country);
                if (customerVo.ResidenceLivingDate == DateTime.MinValue || customerVo.ResidenceLivingDate == null)
                {
                    db.AddInParameter(createCustomerCmd, "@C_ResidenceLivingDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_ResidenceLivingDate", DbType.DateTime, customerVo.ResidenceLivingDate);
                }
                db.AddInParameter(createCustomerCmd, "@C_ResISDCode", DbType.Int32, customerVo.ResISDCode);
                db.AddInParameter(createCustomerCmd, "@C_ResSTDCode", DbType.Int32, customerVo.ResSTDCode);
                db.AddInParameter(createCustomerCmd, "@C_ResPhoneNum", DbType.Int32, customerVo.ResPhoneNum);
                db.AddInParameter(createCustomerCmd, "@C_OfcISDCode", DbType.Int32, customerVo.OfcISDCode);
                db.AddInParameter(createCustomerCmd, "@C_OfcSTDCode", DbType.Int32, customerVo.OfcSTDCode);
                db.AddInParameter(createCustomerCmd, "@C_OfcPhoneNum", DbType.Int32, customerVo.OfcPhoneNum);
                db.AddInParameter(createCustomerCmd, "@C_Email", DbType.String, customerVo.Email);
                db.AddInParameter(createCustomerCmd, "@C_AltEmail", DbType.String, customerVo.AltEmail);
                db.AddInParameter(createCustomerCmd, "@C_Mobile1", DbType.Int64, customerVo.Mobile1);
                db.AddInParameter(createCustomerCmd, "@C_Mobile2", DbType.Int64, customerVo.Mobile2);
                db.AddInParameter(createCustomerCmd, "@C_ISDFax", DbType.Int32, customerVo.ISDFax);
                db.AddInParameter(createCustomerCmd, "@C_STDFax", DbType.Int32, customerVo.STDFax);
                db.AddInParameter(createCustomerCmd, "@C_Fax", DbType.Int32, customerVo.Fax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxISD", DbType.Int32, customerVo.OfcISDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxSTD", DbType.Int32, customerVo.OfcSTDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFax", DbType.Int32, customerVo.OfcFax);
                db.AddInParameter(createCustomerCmd, "@XO_OccupationCode", DbType.String, customerVo.Occupation);
                db.AddInParameter(createCustomerCmd, "@XQ_QualificationCode", DbType.String, customerVo.Qualification);
                db.AddInParameter(createCustomerCmd, "@XMS_MaritalStatusCode", DbType.String, customerVo.MaritalStatus);
                db.AddInParameter(createCustomerCmd, "@XN_NationalityCode", DbType.String, customerVo.Nationality);
                db.AddInParameter(createCustomerCmd, "@C_IsProspect", DbType.Int32, customerVo.IsProspect);
                db.AddInParameter(createCustomerCmd, "@C_IsFPClient", DbType.Int32, customerVo.IsFPClient);
                //Customer Marriage Date

                db.AddInParameter(createCustomerCmd, "@C_MarriageDate", DbType.DateTime, DBNull.Value);


                db.AddInParameter(createCustomerCmd, "@C_RBIRefNum", DbType.String, customerVo.RBIRefNum);
                if (customerVo.RBIApprovalDate == null || customerVo.RBIApprovalDate == DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_RBIApprovalDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_RBIApprovalDate", DbType.DateTime, customerVo.RBIApprovalDate);
                }
                db.AddInParameter(createCustomerCmd, "@C_CompanyName", DbType.String, customerVo.CompanyName);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrLine1", DbType.String, customerVo.OfcAdrLine1);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrLine2", DbType.String, customerVo.OfcAdrLine2);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrLine3", DbType.String, customerVo.OfcAdrLine3);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrPinCode", DbType.Int32, customerVo.OfcAdrPinCode);
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrCity", DbType.String, customerVo.OfcAdrCity);
                if (customerVo.OfcAdrState != "Select a State")
                    db.AddInParameter(createCustomerCmd, "@C_OfcAdrState", DbType.String, customerVo.OfcAdrState);
                else
                    db.AddInParameter(createCustomerCmd, "@C_OfcAdrState", DbType.String, "");
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrCountry", DbType.String, customerVo.OfcAdrCountry);
                if (customerVo.JobStartDate == DateTime.MinValue || customerVo.JobStartDate == null)
                {
                    db.AddInParameter(createCustomerCmd, "@C_JobStartDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_JobStartDate", DbType.DateTime, customerVo.JobStartDate);
                }
                if (customerVo.RegistrationDate == null || customerVo.RegistrationDate == DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_RegistrationDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_RegistrationDate", DbType.DateTime, customerVo.RegistrationDate);
                }
                if (customerVo.CommencementDate == null || customerVo.CommencementDate == DateTime.MinValue)
                {
                    db.AddInParameter(createCustomerCmd, "@C_CommencementDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@C_CommencementDate", DbType.DateTime, customerVo.CommencementDate);
                }
                db.AddInParameter(createCustomerCmd, "@C_RegistrationPlace", DbType.String, customerVo.RegistrationPlace);
                db.AddInParameter(createCustomerCmd, "@C_RegistrationNum", DbType.String, customerVo.RegistrationNum);
                db.AddInParameter(createCustomerCmd, "@C_CompanyWebsite", DbType.String, customerVo.CompanyWebsite);
                db.AddInParameter(createCustomerCmd, "@C_MothersMaidenName", DbType.String, customerVo.MothersMaidenName);
                db.AddInParameter(createCustomerCmd, "@U_Password", DbType.String, userVo.Password);
                if (userVo.FirstName != null)
                    db.AddInParameter(createCustomerCmd, "@U_FirstName", DbType.String, userVo.FirstName);
                else
                    db.AddInParameter(createCustomerCmd, "@U_FirstName", DbType.String, DBNull.Value);
                if (userVo.MiddleName != null)
                    db.AddInParameter(createCustomerCmd, "@U_MiddleName", DbType.String, userVo.MiddleName);
                else
                    db.AddInParameter(createCustomerCmd, "@U_MiddleName", DbType.String, DBNull.Value);
                if (userVo.LastName != null)
                    db.AddInParameter(createCustomerCmd, "@U_LastName", DbType.String, userVo.LastName);
                else
                    db.AddInParameter(createCustomerCmd, "@U_LastName", DbType.String, DBNull.Value);
                db.AddInParameter(createCustomerCmd, "@U_Email", DbType.String, userVo.Email.ToString());
                db.AddInParameter(createCustomerCmd, "@U_UserType", DbType.String, "Customer");
                db.AddInParameter(createCustomerCmd, "@U_LoginId", DbType.String, userVo.LoginId);

                db.AddInParameter(createCustomerCmd, "@CP_IsMainPortfolio", DbType.Int16, customerPortfolioVo.IsMainPortfolio);
                db.AddInParameter(createCustomerCmd, "@XPT_PortfolioTypeCode", DbType.String, customerPortfolioVo.PortfolioTypeCode);
                db.AddInParameter(createCustomerCmd, "@CP_PMSIdentifier", DbType.String, customerPortfolioVo.PMSIdentifier);
                db.AddInParameter(createCustomerCmd, "@CP_PortfolioName", DbType.String, customerPortfolioVo.PortfolioName);
                db.AddInParameter(createCustomerCmd, "@C_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerCmd, "@C_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerCmd, "@C_DummyPAN", DbType.Int32, customerVo.DummyPAN);

                db.AddOutParameter(createCustomerCmd, "@C_CustomerId", DbType.Int32, 10);
                db.AddOutParameter(createCustomerCmd, "@U_UserId", DbType.Int32, 10);
                db.AddOutParameter(createCustomerCmd, "@CP_PortfolioId", DbType.Int32, 10);

                if (db.ExecuteNonQuery(createCustomerCmd) != 0)
                {

                    customerUserId = int.Parse(db.GetParameterValue(createCustomerCmd, "U_UserId").ToString());
                    customerId = int.Parse(db.GetParameterValue(createCustomerCmd, "C_CustomerId").ToString());
                    customerPortfolioId = int.Parse(db.GetParameterValue(createCustomerCmd, "CP_PortfolioId").ToString());

                    customerIds.Add(customerUserId);
                    customerIds.Add(customerId);
                    customerIds.Add(customerPortfolioId);
                }
                else
                {
                    customerIds = null;
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

                FunctionInfo.Add("Method", "CustomerDao.cs:CreateCompleteCustomer()");


                object[] objects = new object[4];
                objects[0] = customerVo;
                objects[1] = userVo;
                objects[2] = customerPortfolioVo;
                objects[3] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerIds;
        }


        /// <summary>
        /// Get ProofList based on CustomerType and also based on Proof Category
        /// </summary>
        /// <param name="customerType"></param>
        /// <param name="proofCategory"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetProofList(int customerType, int proofCategory, int customerId)
        {
            Database db;
            DbCommand cmdGetIdCode;

            DataSet dsIdCode;
            DataTable dtResult = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetIdCode = db.GetStoredProcCommand("SP_GetCustomerProofs");
                db.AddInParameter(cmdGetIdCode, "@CustomerTypeCode", DbType.Int32, customerType);
                db.AddInParameter(cmdGetIdCode, "@ProofCategory", DbType.Int32, proofCategory);
                db.AddInParameter(cmdGetIdCode, "@CustomerId", DbType.Int32, customerId);
                dsIdCode = db.ExecuteDataSet(cmdGetIdCode);
                dtResult = dsIdCode.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetProofList()");
                object[] objects = new object[3];
                objects[0] = customerType;
                objects[1] = proofCategory;
                objects[2] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtResult;
        }

        /// <summary>
        /// Deletes Customer Proof if you pass CustomerId as its Parameter
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="proofCode"></param>
        /// <returns></returns>
        public bool DeleteCustomerProof(int customerId, int proofCode)
        {
            Database db;
            DbCommand deleteCustomerProofCmd;
            bool bResult = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCustomerProofCmd = db.GetStoredProcCommand("SP_DeleteCustomerProof");
                db.AddInParameter(deleteCustomerProofCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(deleteCustomerProofCmd, "@ProofCode", DbType.Int32, proofCode);
                if (db.ExecuteNonQuery(deleteCustomerProofCmd) != 0)
                {
                    bResult = true;
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
                FunctionInfo.Add("Method", "CustomerDao.cs:DeleteCustomerProof()");
                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = proofCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// It Reassigns RM if you pass Arrays of Customer Ids and RmIds
        /// </summary>
        /// <param name="customerIds"></param>
        /// <param name="rmIds"></param>
        /// <returns></returns>
        public bool UpdateCustomerAssignedRM(int[] customerIds, int[] rmIds)
        {
            bool bResult = false;
            Database db;
            DbCommand updateCustomerReassignRM;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                for (int i = 0; i < customerIds.Count(); i++)
                {
                    updateCustomerReassignRM = db.GetStoredProcCommand("SP_UpdateCustomerAssignedRM");
                    db.AddInParameter(updateCustomerReassignRM, "@CustomerId", DbType.Int32, customerIds[i]);
                    db.AddInParameter(updateCustomerReassignRM, "@RMId", DbType.Int32, rmIds[i]);
                    if (db.ExecuteNonQuery(updateCustomerReassignRM) != 0)
                        bResult = true;
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
                FunctionInfo.Add("Method", "CustomerDao.cs:UpdateCustomerAssignedRM()");
                object[] objects = new object[2];
                objects[0] = customerIds;
                objects[1] = rmIds;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }

        /// <summary>
        /// Getting the details of income for a particular customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerIncomeDetails(int customerId)
        {
            Database db;
            DbCommand cmdGetIncomeDetails;
            DataTable dtIncomeDetails;
            DataSet dsIncomeDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetIncomeDetails = db.GetStoredProcCommand("SP_CustomerIncomeGetDetails");
                db.AddInParameter(cmdGetIncomeDetails, "@C_CustomerId", DbType.Int32, customerId);
                dsIncomeDetails = db.ExecuteDataSet(cmdGetIncomeDetails);
                dtIncomeDetails = dsIncomeDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtIncomeDetails;
        }

        /// <summary>
        /// Adding Customer Income Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerIncomeVo"></param>
        /// <returns></returns>

        public bool AddCustomerIncomeDetails(int rmUserId, int customerId, CustomerIncomeVo customerIncomeVo)
        {
            Database db;
            DbCommand cmdAddIncomeDetails;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddIncomeDetails = db.GetStoredProcCommand("SP_CustomerIncomeAddDetails");
                db.AddInParameter(cmdAddIncomeDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerIncomeVo.grossSalary != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_GrossSalary", DbType.Double, customerIncomeVo.grossSalary);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_GrossSalary", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeGrossSalary != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeGrossSalary", DbType.Int16, customerIncomeVo.currencyCodeGrossSalary);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeGrossSalary", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.takeHomeSalary != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_TakeHomeSalary", DbType.Double, customerIncomeVo.takeHomeSalary);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_TakeHomeSalary", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeTakeHomeSalary != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeTakeHomeSalary", DbType.Int16, customerIncomeVo.currencyCodeTakeHomeSalary);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeTakeHomeSalary", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.rentalIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_RentalIncome", DbType.Double, customerIncomeVo.rentalIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_RentalIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeRentalIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeRentalIncome", DbType.Int16, customerIncomeVo.currencyCodeRentalIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeRentalIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.pensionIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_PensionIncome", DbType.Double, customerIncomeVo.pensionIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_PensionIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodePensionIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodePensionIncome", DbType.Int16, customerIncomeVo.currencyCodePensionIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodePensionIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.AgriculturalIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_AgriculturalIncome", DbType.Double, customerIncomeVo.AgriculturalIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_AgriculturalIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeAgriIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeAgriculturalIncome", DbType.Int16, customerIncomeVo.currencyCodeAgriIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeAgriculturalIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.businessIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_BusinessIncome", DbType.Double, customerIncomeVo.businessIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_BusinessIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeBusinessIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeBusinessIncome", DbType.Int16, customerIncomeVo.currencyCodeBusinessIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeBusinessIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.otherSourceIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_OtherSourceIncome", DbType.Double, customerIncomeVo.otherSourceIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_OtherSourceIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeOtherIncome != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeOtherSourceIncome", DbType.Int16, customerIncomeVo.currencyCodeOtherIncome);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@XC_CurrencyCodeOtherSourceIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.rentalPropAccountId != 0)
                    db.AddInParameter(cmdAddIncomeDetails, "@CPA_AccountId", DbType.Int16, customerIncomeVo.rentalPropAccountId);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CPA_AccountId", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.dateOfEntry != DateTime.MinValue)
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_DateOfEntry", DbType.DateTime, customerIncomeVo.dateOfEntry);
                else
                    db.AddInParameter(cmdAddIncomeDetails, "@CI_DateOfEntry", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmdAddIncomeDetails, "@rmUserId", DbType.Int32, rmUserId);
                if (db.ExecuteNonQuery(cmdAddIncomeDetails) != 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Update Customer Income Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerIncomeVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerIncomeDetails(int rmUserId, int customerId, CustomerIncomeVo customerIncomeVo)
        {
            Database db;
            DbCommand cmdUpdIncomeDetails;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdIncomeDetails = db.GetStoredProcCommand("SP_CustomerIncomeUpdateDetails");
                db.AddInParameter(cmdUpdIncomeDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerIncomeVo.grossSalary != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_GrossSalary", DbType.Double, customerIncomeVo.grossSalary);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_GrossSalary", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeGrossSalary != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeGrossSalary", DbType.Int16, customerIncomeVo.currencyCodeGrossSalary);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeGrossSalary", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.takeHomeSalary != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_TakeHomeSalary", DbType.Double, customerIncomeVo.takeHomeSalary);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_TakeHomeSalary", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeTakeHomeSalary != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeTakeHomeSalary", DbType.Int16, customerIncomeVo.currencyCodeTakeHomeSalary);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeTakeHomeSalary", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.rentalIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_RentalIncome", DbType.Double, customerIncomeVo.rentalIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_RentalIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeRentalIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeRentalIncome", DbType.Int16, customerIncomeVo.currencyCodeRentalIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeRentalIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.pensionIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_PensionIncome", DbType.Double, customerIncomeVo.pensionIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_PensionIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodePensionIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodePensionIncome", DbType.Int16, customerIncomeVo.currencyCodePensionIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodePensionIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.AgriculturalIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_AgriculturalIncome", DbType.Double, customerIncomeVo.AgriculturalIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_AgriculturalIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeAgriIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeAgriculturalIncome", DbType.Int16, customerIncomeVo.currencyCodeAgriIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeAgriculturalIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.businessIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_BusinessIncome", DbType.Double, customerIncomeVo.businessIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_BusinessIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeBusinessIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeBusinessIncome", DbType.Int16, customerIncomeVo.currencyCodeBusinessIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeBusinessIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.otherSourceIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_OtherSourceIncome", DbType.Double, customerIncomeVo.otherSourceIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_OtherSourceIncome", DbType.Double, DBNull.Value);
                if (customerIncomeVo.currencyCodeOtherIncome != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeOtherSourceIncome", DbType.Int16, customerIncomeVo.currencyCodeOtherIncome);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@XC_CurrencyCodeOtherSourceIncome", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.rentalPropAccountId != 0)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CPA_AccountId", DbType.Int16, customerIncomeVo.rentalPropAccountId);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CPA_AccountId", DbType.Int16, DBNull.Value);
                if (customerIncomeVo.dateOfEntry != DateTime.MinValue)
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_DateOfEntry", DbType.DateTime, customerIncomeVo.dateOfEntry);
                else
                    db.AddInParameter(cmdUpdIncomeDetails, "@CI_DateOfEntry", DbType.DateTime, DBNull.Value);
                db.AddInParameter(cmdUpdIncomeDetails, "@rmUserId", DbType.Int32, rmUserId);
                if (db.ExecuteNonQuery(cmdUpdIncomeDetails) != 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:UpdateCustomerIncomeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Getting the property details of a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerPropertyDetails(int customerId)
        {
            Database db;
            DbCommand cmdGetPropDetails;
            DataTable dtPropDetails;
            DataSet dsPropDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetPropDetails = db.GetStoredProcCommand("SP_CustomerPropertyNetPositionGetDetails");
                db.AddInParameter(cmdGetPropDetails, "@C_CustomerId", DbType.Int32, customerId);
                dsPropDetails = db.ExecuteDataSet(cmdGetPropDetails);
                dtPropDetails = dsPropDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerPropertyDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtPropDetails;
        }

        /// <summary>
        /// Getting the details of Expense for a particular customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerExpenseDetails(int customerId)
        {
            Database db;
            DbCommand cmdGetExpenseDetails;
            DataTable dtExpenseDetails;
            DataSet dsExpenseDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetExpenseDetails = db.GetStoredProcCommand("SP_CustomerExpenseGetDetails");
                db.AddInParameter(cmdGetExpenseDetails, "@C_CustomerId", DbType.Int32, customerId);
                dsExpenseDetails = db.ExecuteDataSet(cmdGetExpenseDetails);
                dtExpenseDetails = dsExpenseDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerExpenseDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtExpenseDetails;
        }

        /// <summary>
        /// Adding Customer Expense Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerExpenseVo"></param>
        /// <returns></returns>
        public bool AddCustomerExpenseDetails(int rmUserId, int customerId, CustomerExpenseVo customerExpenseVo)
        {
            Database db;
            DbCommand cmdAddExpenseDetails;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdAddExpenseDetails = db.GetStoredProcCommand("SP_CustomerExpenseAddDetails");
                db.AddInParameter(cmdAddExpenseDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerExpenseVo.Transportation != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Transportation", DbType.Double, customerExpenseVo.Transportation);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Transportation", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeTransportation != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeTransportation", DbType.Int16, customerExpenseVo.CurrencyCodeTransportation);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeTransportation", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Food != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Food", DbType.Double, customerExpenseVo.Food);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Food", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeFood != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeFood", DbType.Int16, customerExpenseVo.CurrencyCodeFood);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeFood", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Clothing != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Clothing", DbType.Double, customerExpenseVo.Clothing);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Clothing", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeClothing != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeClothing", DbType.Int16, customerExpenseVo.CurrencyCodeClothing);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeClothing", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Home != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Home", DbType.Double, customerExpenseVo.Home);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Home", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeHome != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeHome", DbType.Int16, customerExpenseVo.CurrencyCodeHome);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeHome", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Utilities != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Utilities", DbType.Double, customerExpenseVo.Utilities);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Utilities", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeUtilities != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeUtilities", DbType.Int16, customerExpenseVo.CurrencyCodeUtilities);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeUtilities", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.SelfCare != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_SelfCare", DbType.Double, customerExpenseVo.SelfCare);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_SelfCare", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeSelfCare != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeSelfCare", DbType.Int16, customerExpenseVo.CurrencyCodeSelfCare);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeSelfCare", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.HealthCare != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_HealthCare", DbType.Double, customerExpenseVo.HealthCare);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_HealthCare", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeHealthCare != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeHealthCare", DbType.Int16, customerExpenseVo.CurrencyCodeHealthCare);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeHealthCare", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.DateOfEntry != DateTime.MinValue)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_DateOfEntry", DbType.DateTime, customerExpenseVo.DateOfEntry);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_DateOfEntry", DbType.DateTime, DBNull.Value);
                if (customerExpenseVo.Education != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Education", DbType.Double, customerExpenseVo.Education);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Education", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeEducation != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeEducation", DbType.Int16, customerExpenseVo.CurrencyCodeEducation);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeEducation", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Pets != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Pets", DbType.Double, customerExpenseVo.Pets);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Pets", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodePets != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodePets", DbType.Int16, customerExpenseVo.CurrencyCodePets);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodePets", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Entertainment != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Entertainment", DbType.Double, customerExpenseVo.Entertainment);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Entertainment", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeEntertainment != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeEntertainment", DbType.Int16, customerExpenseVo.CurrencyCodeEntertainment);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeEntertainment", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Miscellaneous != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Miscellaneous", DbType.Double, customerExpenseVo.Miscellaneous);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@CE_Miscellaneous", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeMiscellaneous != 0)
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeMiscellaneous", DbType.Int16, customerExpenseVo.CurrencyCodeMiscellaneous);
                else
                    db.AddInParameter(cmdAddExpenseDetails, "@XC_CurrencyCodeMiscellaneous", DbType.Int16, DBNull.Value);
                db.AddInParameter(cmdAddExpenseDetails, "@rmUserId", DbType.Int32, rmUserId);
                if (db.ExecuteNonQuery(cmdAddExpenseDetails) != 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerExpenseDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Update Customer Expense Details
        /// </summary>
        /// <param name="rmUserId"></param>
        /// <param name="customerId"></param>
        /// <param name="customerExpenseVo"></param>
        /// <returns></returns>
        public bool UpdateCustomerExpenseDetails(int rmUserId, int customerId, CustomerExpenseVo customerExpenseVo)
        {
            Database db;
            DbCommand cmdUpdExpenseDetails;
            bool bResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdUpdExpenseDetails = db.GetStoredProcCommand("SP_CustomerExpenseUpdateDetails");
                db.AddInParameter(cmdUpdExpenseDetails, "@C_CustomerId", DbType.Int32, customerId);
                if (customerExpenseVo.Transportation != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Transportation", DbType.Double, customerExpenseVo.Transportation);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Transportation", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeTransportation != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeTransportation", DbType.Int16, customerExpenseVo.CurrencyCodeTransportation);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeTransportation", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Food != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Food", DbType.Double, customerExpenseVo.Food);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Food", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeFood != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeFood", DbType.Int16, customerExpenseVo.CurrencyCodeFood);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeFood", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Clothing != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Clothing", DbType.Double, customerExpenseVo.Clothing);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Clothing", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeClothing != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeClothing", DbType.Int16, customerExpenseVo.CurrencyCodeClothing);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeClothing", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Home != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Home", DbType.Double, customerExpenseVo.Home);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Home", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeHome != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeHome", DbType.Int16, customerExpenseVo.CurrencyCodeHome);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeHome", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Utilities != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Utilities", DbType.Double, customerExpenseVo.Utilities);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Utilities", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeUtilities != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeUtilities", DbType.Int16, customerExpenseVo.CurrencyCodeUtilities);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeUtilities", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.SelfCare != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_SelfCare", DbType.Double, customerExpenseVo.SelfCare);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_SelfCare", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeSelfCare != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeSelfCare", DbType.Int16, customerExpenseVo.CurrencyCodeSelfCare);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeSelfCare", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.HealthCare != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_HealthCare", DbType.Double, customerExpenseVo.HealthCare);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_HealthCare", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeHealthCare != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeHealthCare", DbType.Int16, customerExpenseVo.CurrencyCodeHealthCare);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeHealthCare", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.DateOfEntry != DateTime.MinValue)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_DateOfEntry", DbType.DateTime, customerExpenseVo.DateOfEntry);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_DateOfEntry", DbType.DateTime, DBNull.Value);
                if (customerExpenseVo.Education != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Education", DbType.Double, customerExpenseVo.Education);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Education", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeEducation != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeEducation", DbType.Int16, customerExpenseVo.CurrencyCodeEducation);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeEducation", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Pets != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Pets", DbType.Double, customerExpenseVo.Pets);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Pets", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodePets != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodePets", DbType.Int16, customerExpenseVo.CurrencyCodePets);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodePets", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Entertainment != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Entertainment", DbType.Double, customerExpenseVo.Entertainment);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Entertainment", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeEntertainment != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeEntertainment", DbType.Int16, customerExpenseVo.CurrencyCodeEntertainment);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeEntertainment", DbType.Int16, DBNull.Value);
                if (customerExpenseVo.Miscellaneous != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Miscellaneous", DbType.Double, customerExpenseVo.Miscellaneous);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@CE_Miscellaneous", DbType.Double, DBNull.Value);
                if (customerExpenseVo.CurrencyCodeMiscellaneous != 0)
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeMiscellaneous", DbType.Int16, customerExpenseVo.CurrencyCodeMiscellaneous);
                else
                    db.AddInParameter(cmdUpdExpenseDetails, "@XC_CurrencyCodeMiscellaneous", DbType.Int16, DBNull.Value);
                db.AddInParameter(cmdUpdExpenseDetails, "@rmUserId", DbType.Int32, rmUserId);
                if (db.ExecuteNonQuery(cmdUpdExpenseDetails) != 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:UpdateCustomerExpenseDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// PAN Number Duplicate Check
        /// </summary>
        /// <param name="adviserId"></param>
        /// <param name="panNumber"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public bool PANNumberDuplicateCheck(int adviserId, string panNumber, int CustomerId)
        {
            Database db;
            DbCommand cmdPanDuplicateCheck;
            bool bResult = false;
            int res = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdPanDuplicateCheck = db.GetStoredProcCommand("SP_PANDuplicateCheck");
                db.AddInParameter(cmdPanDuplicateCheck, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdPanDuplicateCheck, "@PAN", DbType.String, panNumber);
                db.AddInParameter(cmdPanDuplicateCheck, "@C_CustomerId", DbType.Int32, CustomerId);

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
                FunctionInfo.Add("Method", "CustomerDao.cs:PANNumberDuplicateCheck()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = panNumber;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// Getting the Pan and Address of Customer for Group Account Setup
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable GetCustomerPanAddress(int customerId)
        {
            Database db;
            DbCommand cmdGetPanAddress;
            DataTable dtPanAddress;
            DataSet dsPanAddress = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetPanAddress = db.GetStoredProcCommand("SP_GetCustomerPanAddress");
                db.AddInParameter(cmdGetPanAddress, "@C_CustomerId", DbType.Int32, customerId);
                dsPanAddress = db.ExecuteDataSet(cmdGetPanAddress);
                dtPanAddress = dsPanAddress.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerExpenseDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtPanAddress;
        }

        /// <summary>
        ///  Get RM Group Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetParentCustomerName(string prefixText, int rmId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetParentCustomerNames");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@AR_RMId", DbType.Int32, rmId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetParentCustomerName()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        /// <summary>
        ///  Get RM Group Customers for Grouping
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetParentCustomers(string prefixText, int rmId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetParentCustomers");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@AR_RMId", DbType.Int32, rmId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetParentCustomerName()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        /// <summary>
        /// Get RM Individual Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetMemberCustomerName(string prefixText, int rmId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetMemberCustomerNames");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@AR_RMId", DbType.Int32, rmId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetMemberCustomerName()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        /// <summary>
        /// Get RM Individual Customer Names for Group Members
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="selectedParentId"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetMemberCustomerNamesForGrouping(string prefixText, int selectedParentId, int rmId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetMemberCustomerNamesForGrouping");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@selectedParentId", DbType.Int32, selectedParentId);
                db.AddInParameter(cmdGetCustomerNames, "@AR_RMId", DbType.Int32, rmId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetMemberCustomerNamesForGrouping()");


                object[] objects = new object[3];

                objects[0] = prefixText;
                objects[1] = rmId;
                objects[2] = selectedParentId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        /// <summary>
        /// No Use
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="rmId"></param>
        /// <returns></returns>
        public DataTable GetCustomerName(string prefixText, int rmId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetCustomerNames");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@AR_RMId", DbType.Int32, rmId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerName()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// Get Advisor Individual Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataTable GetAdviserCustomerName(string prefixText, int adviserId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetAdviserCustomerNames");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@A_AdviserId", DbType.Int32, adviserId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAdviserCustomerName()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// Get Advisor Group Customer Names
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="adviserId"></param>
        /// <returns></returns>
        public DataTable GetAdviserGroupCustomerName(string prefixText, int adviserId)
        {

            Database db;
            DbCommand cmdGetGroupCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetGroupCustomerNames = db.GetStoredProcCommand("SP_GetAdviserParentCustomerNames");
                db.AddInParameter(cmdGetGroupCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetGroupCustomerNames, "@A_AdviserId", DbType.Int32, adviserId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetGroupCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAdviserCustomerName()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        /// <summary>
        /// Function to check whether a customer is group head or not
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool CheckCustomerGroupHead(int customerId)
        {
            bool result = false;
            int res;
            Database db;
            DbCommand checkCustomerGrpHeadCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkCustomerGrpHeadCmd = db.GetStoredProcCommand("SP_ChkCustomerGrpHead");
                db.AddInParameter(checkCustomerGrpHeadCmd, "@CustomerId", DbType.Int32, customerId);

                res = int.Parse(db.ExecuteScalar(checkCustomerGrpHeadCmd).ToString());
                if (res > 1)
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

                FunctionInfo.Add("Method", "AdvisorLOBDao.cs:CheckCustomerGroupHead()");

                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        //FP SuperLite Related Functions
        //===================================================================================================================================

        /// <summary>
        /// Used to Get Customer Relation
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomerRelation()
        {
            Database db;
            DbCommand cmdGetCustomerRelation;
            DataTable dtGetCustomerRelation;
            DataSet dsGetCustomerRelation = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetCustomerRelation = db.GetStoredProcCommand("SP_GetCustomerRelation");
                dsGetCustomerRelation = db.ExecuteDataSet(cmdGetCustomerRelation);
                dtGetCustomerRelation = dsGetCustomerRelation.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerRelation()");
                object[] objects = new object[1];
                objects[0] = "Relationship problem";
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerRelation;
        }


        public DataTable GetCustomerDetailsForProspectList(int rmId)
        {
            Database db;
            DataTable dtGetCustomerDetails = null;
            DbCommand cmdGetCustomerDetails;
            DataSet dsGetCustomerDetails = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetCustomerDetails = db.GetStoredProcCommand("SP_GetCustomerDetailsForProspectList");
                db.AddInParameter(cmdGetCustomerDetails, "@AR_RMId", DbType.Int32, rmId);
                dsGetCustomerDetails = db.ExecuteDataSet(cmdGetCustomerDetails);
                dtGetCustomerDetails = dsGetCustomerDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerDetailsForProspectList(int rmId)");
                object[] objects = new object[1];
                objects[0] = rmId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerDetails;
        }
    }
}

