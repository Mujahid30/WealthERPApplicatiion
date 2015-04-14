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
                db.AddInParameter(createCustomerCmd, "@C_Adr1PinCode", DbType.Int64, customerVo.Adr1PinCode);
                db.AddInParameter(createCustomerCmd, "@C_Adr1City", DbType.String, customerVo.Adr1City);
                db.AddInParameter(createCustomerCmd, "@C_Adr1State", DbType.String, customerVo.Adr1State);
                db.AddInParameter(createCustomerCmd, "@C_Adr1Country", DbType.String, customerVo.Adr1Country);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line1", DbType.String, customerVo.Adr2Line1);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line2", DbType.String, customerVo.Adr2Line2);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line3", DbType.String, customerVo.Adr2Line3);
                db.AddInParameter(createCustomerCmd, "@C_Adr2PinCode", DbType.Int64, customerVo.Adr2PinCode);
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
                db.AddInParameter(createCustomerCmd, "@C_OfcPhoneNum", DbType.Int64, customerVo.OfcPhoneNum);
                db.AddInParameter(createCustomerCmd, "@C_MfKYC", DbType.Int32, customerVo.MfKYC);
                db.AddInParameter(createCustomerCmd, "@C_Email", DbType.String, customerVo.Email);
                db.AddInParameter(createCustomerCmd, "@C_AltEmail", DbType.String, customerVo.AltEmail);
                db.AddInParameter(createCustomerCmd, "@C_Mobile1", DbType.Int64, customerVo.Mobile1);
                db.AddInParameter(createCustomerCmd, "@C_Mobile2", DbType.Int64, customerVo.Mobile2);
                db.AddInParameter(createCustomerCmd, "@C_ISDFax", DbType.Int32, customerVo.ISDFax);
                db.AddInParameter(createCustomerCmd, "@C_STDFax", DbType.Int32, customerVo.STDFax);
                db.AddInParameter(createCustomerCmd, "@C_Fax", DbType.Int64, customerVo.Fax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxISD", DbType.Int32, customerVo.OfcISDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxSTD", DbType.Int32, customerVo.OfcSTDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFax", DbType.Int64, customerVo.OfcFax);
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
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrPinCode", DbType.Int64, customerVo.OfcAdrPinCode);
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
                db.AddInParameter(createCustomerCmd, "@C_ProspectAddDate", DbType.DateTime, customerVo.ProspectAddDate);
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
        public DataTable GetISaList(int customerId)
        {
            CustomerVo customerVo = null;
            Database db;
            DbCommand GetISaListCmd;
            DataTable GetISaListDt;
            DataSet GetISaListDs;
            DataRow dr;
            try
            {

                customerVo = new CustomerVo();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetISaListCmd = db.GetStoredProcCommand("SP_GetIsaList");
                db.AddInParameter(GetISaListCmd, "@CustomerId", DbType.Int32, customerId);
                GetISaListDs = db.ExecuteDataSet(GetISaListCmd);
                GetISaListDt = GetISaListDs.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetSchemeDetails()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return GetISaListDt;
        }
        public DataSet GetSchemeDetails(int schemePlanCode)
        {
            CustomerVo customerVo = null;
            Database db;
            DbCommand getschemePlanCodeCmd;
            DataSet getschemePlanCodeDs;
            DataRow dr;
            try
            {
                customerVo = new CustomerVo();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getschemePlanCodeCmd = db.GetStoredProcCommand("SP_GetSchemePlanDetails");
                db.AddInParameter(getschemePlanCodeCmd, "@schemePlanCode", DbType.String, schemePlanCode.ToString());
                getschemePlanCodeDs = db.ExecuteDataSet(getschemePlanCodeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetSchemeDetails()");
                object[] objects = new object[1];
                objects[0] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getschemePlanCodeDs;
        }

        public DataSet GetSchemeMapDetails(string ExternalType, int AmcCode, string Category, string Type, int mtype)
        {
            CustomerVo customerVo = null;
            Database db;
            DbCommand getschemePlanCodeCmd;
            DataSet getschemePlanCodeDs;

            try
            {
                customerVo = new CustomerVo();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getschemePlanCodeCmd = db.GetStoredProcCommand("SP_BindSchemeDetails");
                //db.AddInParameter(getschemePlanCodeCmd, "@ExternalType", DbType.String, ExternalType);
                //db.AddInParameter(getschemePlanCodeCmd, "@AmcCode", DbType.Int32, AmcCode);
                //db.AddInParameter(getschemePlanCodeCmd, "@Category", DbType.String, Category);
                db.AddInParameter(getschemePlanCodeCmd, "@Type", DbType.Int32, mtype);
                getschemePlanCodeDs = db.ExecuteDataSet(getschemePlanCodeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetSchemeMapDetails()");
                object[] objects = new object[1];
                objects[0] = ExternalType;
                objects[1] = AmcCode;
                objects[2] = Category;
                objects[3] = Type;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getschemePlanCodeDs;
        }
        public DataSet GetTaxStatusList()
        {
            CustomerVo customerVo = null;
            Database db;
            DbCommand getCustomerCmd;
            DataSet getTaxStatusDs;
            DataRow dr;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerCmd = db.GetStoredProcCommand("SP_GetTaxStatusList");
                getTaxStatusDs = db.ExecuteDataSet(getCustomerCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetDataTransMapDetails()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getTaxStatusDs;
        }
        public DataSet GetDataTransMapDetails(string ExternalType)
        {
            CustomerVo customerVo = null;
            Database db;
            DbCommand getschemePlanCodeCmd;
            DataSet getschemePlanCodeDs;

            try
            {
                customerVo = new CustomerVo();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getschemePlanCodeCmd = db.GetStoredProcCommand("SP_BindDataTranslationMappingDetails");
                db.AddInParameter(getschemePlanCodeCmd, "@ExtractType", DbType.String, ExternalType);
                getschemePlanCodeDs = db.ExecuteDataSet(getschemePlanCodeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetDataTransMapDetails()");
                object[] objects = new object[1];
                objects[0] = ExternalType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getschemePlanCodeDs;
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
                db.AddInParameter(getCustomerCmd, "@C_CustomerId", DbType.Int32, customerId);
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
                    if (dr["AB_BranchId"].ToString() != string.Empty)
                        customerVo.BranchId = int.Parse(dr["AB_BranchId"].ToString());
                    if (dr["U_UMId"].ToString() != string.Empty)
                        customerVo.UserId = int.Parse(dr["U_UMId"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_FirstName"].ToString()))
                        customerVo.FirstName = dr["C_FirstName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_MiddleName"].ToString()))
                    {
                        customerVo.MiddleName = dr["C_MiddleName"].ToString();
                    }
                    else
                    {
                        customerVo.MiddleName = string.Empty;
                    }
                    if (dr["C_IsDummyPAN"].ToString() != string.Empty)
                        customerVo.DummyPAN = int.Parse(dr["C_IsDummyPAN"].ToString());
                    if (dr["C_IsActive"].ToString() != string.Empty)
                        customerVo.IsActive = int.Parse(dr["C_IsActive"].ToString());
                    if (dr["C_AlertViaSMS"].ToString() != string.Empty)
                        customerVo.ViaSMS = int.Parse(dr["C_AlertViaSMS"].ToString());
                    if (dr["C_AlertViaEmail"].ToString() != string.Empty)
                        customerVo.AlertViaEmail = int.Parse(dr["C_AlertViaEmail"].ToString());
                    if (dr["C_IsProspect"].ToString() != string.Empty)
                        customerVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_ProspectAddDate"].ToString()))
                        customerVo.ProspectAddDate = Convert.ToDateTime(dr["C_ProspectAddDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_Comments"].ToString()))
                        customerVo.AdviseNote = dr["C_Comments"].ToString();
                    if (dr["ACC_CustomerCategoryCode"].ToString() != string.Empty)
                        customerVo.CustomerClassificationID = int.Parse(dr["ACC_CustomerCategoryCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_LastName"].ToString()))
                    {
                        customerVo.LastName = dr["C_LastName"].ToString();
                    }
                    else
                    {
                        customerVo.LastName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dr["C_Gender"].ToString()))
                        customerVo.Gender = dr["C_Gender"].ToString();
                    if (!string.IsNullOrEmpty(dr["AB_BranchName"].ToString()))
                        customerVo.BranchName = dr["AB_BranchName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_DOB"].ToString()))
                        customerVo.Dob = Convert.ToDateTime(dr["C_DOB"].ToString());
                    //customerVo.Dob = DateTime.Parse(dr["C_DOB"].ToString());
                    if (!string.IsNullOrEmpty(dr["XCT_CustomerTypeCode"].ToString()))
                        customerVo.Type = dr["XCT_CustomerTypeCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["XCST_CustomerSubTypeCode"].ToString()))
                        customerVo.SubType = dr["XCST_CustomerSubTypeCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Salutation"].ToString()))
                        customerVo.Salutation = dr["C_Salutation"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_PANNum"].ToString()))
                        customerVo.PANNum = dr["C_PANNum"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr1Line1"].ToString()))
                        customerVo.Adr1Line1 = dr["C_Adr1Line1"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr1Line2"].ToString()))
                        customerVo.Adr1Line2 = dr["C_Adr1Line2"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr1Line3"].ToString()))
                        customerVo.Adr1Line3 = dr["C_Adr1Line3"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr1PinCode"].ToString()))
                        customerVo.Adr1PinCode = Int64.Parse(dr["C_Adr1PinCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["WCMV_City_Id"].ToString()))
                        customerVo.Adr1City = dr["WCMV_City_Id"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_WCMV_State_Id"].ToString()))
                        customerVo.Adr1State = dr["C_WCMV_State_Id"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr1Country"].ToString()))
                        customerVo.Adr1Country = dr["C_Adr1Country"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr2Line1"].ToString()))
                        customerVo.Adr2Line1 = dr["C_Adr2Line1"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr2Line2"].ToString()))
                        customerVo.Adr2Line2 = dr["C_Adr2Line2"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr2Line3"].ToString()))
                        customerVo.Adr2Line3 = dr["C_Adr2Line3"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr2PinCode"].ToString()))
                        customerVo.Adr2PinCode = Int64.Parse(dr["C_Adr2PinCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_Adr2City"].ToString()))
                        customerVo.Adr2City = dr["C_Adr2City"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr2State"].ToString()))
                        customerVo.Adr2State = dr["C_Adr2State"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr2Country"].ToString()))
                        customerVo.Adr2Country = dr["C_Adr2Country"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_ResidenceLivingDate"].ToString()))
                        customerVo.ResidenceLivingDate = Convert.ToDateTime(dr["C_ResidenceLivingDate"].ToString());
                    if (dr["C_ResISDCode"].ToString() != string.Empty)
                        customerVo.ResISDCode = Int64.Parse(dr["C_ResISDCode"].ToString());
                    if (dr["C_ResSTDCode"].ToString() != string.Empty)
                        customerVo.ResSTDCode = Int64.Parse(dr["C_ResSTDCode"].ToString());
                    if (dr["C_ResPhoneNum"].ToString() != string.Empty)
                        customerVo.ResPhoneNum = Int64.Parse(dr["C_ResPhoneNum"].ToString());
                    if (dr["C_OfcISDCode"].ToString() != string.Empty)
                        customerVo.OfcISDCode = Int64.Parse(dr["C_OfcISDCode"].ToString());
                    if (dr["C_OfcSTDCode"].ToString() != string.Empty)
                        customerVo.OfcSTDCode = Int64.Parse(dr["C_OfcSTDCode"].ToString());
                    if (dr["C_OfcPhoneNum"].ToString() != string.Empty)
                        customerVo.OfcPhoneNum = Int64.Parse(dr["C_OfcPhoneNum"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_Email"].ToString()))
                        customerVo.Email = dr["C_Email"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_AltEmail"].ToString()))
                        customerVo.AltEmail = dr["C_AltEmail"].ToString();
                    if (dr["C_Mobile1"].ToString() != string.Empty)
                        customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                    if (dr["C_Mobile2"].ToString() != string.Empty)
                        customerVo.Mobile2 = long.Parse(dr["C_Mobile2"].ToString());
                    if (dr["C_ISDFax"].ToString() != string.Empty)
                        customerVo.ISDFax = int.Parse(dr["C_ISDFax"].ToString());
                    if (dr["C_STDFax"].ToString() != string.Empty)
                        customerVo.STDFax = int.Parse(dr["C_STDFax"].ToString());
                    if (dr["C_Fax"].ToString() != string.Empty)
                        customerVo.Fax = Int64.Parse(dr["C_Fax"].ToString());
                    if (dr["C_OfcFaxISD"].ToString() != string.Empty)
                        customerVo.OfcISDFax = int.Parse(dr["C_OfcFaxISD"].ToString());
                    if (dr["C_OfcFaxSTD"].ToString() != string.Empty)
                        customerVo.OfcSTDFax = int.Parse(dr["C_OfcFaxSTD"].ToString());
                    if (dr["C_OfcFax"].ToString() != string.Empty)
                        customerVo.OfcFax = Int64.Parse(dr["C_OfcFax"].ToString());
                    if (!string.IsNullOrEmpty(dr["XO_OccupationCode"].ToString()))
                        customerVo.Occupation = dr["XO_OccupationCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["XQ_QualificationCode"].ToString()))
                        customerVo.Qualification = dr["XQ_QualificationCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["XMS_MaritalStatusCode"].ToString()))
                        customerVo.MaritalStatus = dr["XMS_MaritalStatusCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_MarriageDate"].ToString()))
                        customerVo.MarriageDate = Convert.ToDateTime(dr["C_MarriageDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["XN_NationalityCode"].ToString()))
                        customerVo.Nationality = dr["XN_NationalityCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_RBIRefNum"].ToString()))
                        customerVo.RBIRefNum = dr["C_RBIRefNum"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_RBIApprovalDate"].ToString()))
                        customerVo.RBIApprovalDate = Convert.ToDateTime(dr["C_RBIApprovalDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_CompanyName"].ToString()))
                        customerVo.CompanyName = dr["C_CompanyName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrLine1"].ToString()))
                        customerVo.OfcAdrLine1 = dr["C_OfcAdrLine1"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrLine2"].ToString()))
                        customerVo.OfcAdrLine2 = dr["C_OfcAdrLine2"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrLine3"].ToString()))
                        customerVo.OfcAdrLine3 = dr["C_OfcAdrLine3"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrPinCode"].ToString()))
                        customerVo.OfcAdrPinCode = Int64.Parse(dr["C_OfcAdrPinCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrCity"].ToString()))
                        customerVo.OfcAdrCity = dr["C_OfcAdrCity"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrState"].ToString()))
                        customerVo.OfcAdrState = dr["C_OfcAdrState"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrCountry"].ToString()))
                        customerVo.OfcAdrCountry = dr["C_OfcAdrCountry"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_JobStartDate"].ToString()))
                        customerVo.JobStartDate = Convert.ToDateTime(dr["C_JobStartDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_RegistrationDate"].ToString()))
                        customerVo.RegistrationDate = Convert.ToDateTime(dr["C_RegistrationDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_CommencementDate"].ToString()))
                        customerVo.CommencementDate = Convert.ToDateTime(dr["C_CommencementDate"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_RegistrationPlace"].ToString()))
                        customerVo.RegistrationPlace = dr["C_RegistrationPlace"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_RegistrationNum"].ToString()))
                        customerVo.RegistrationNum = dr["C_RegistrationNum"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_CompanyWebsite"].ToString()))
                        customerVo.CompanyWebsite = dr["C_CompanyWebsite"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_ContactGuardianMiddleName"].ToString()))
                        customerVo.ContactMiddleName = dr["C_ContactGuardianMiddleName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_ContactGuardianFirstName"].ToString()))
                        customerVo.ContactFirstName = dr["C_ContactGuardianFirstName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_ContactGuardianLastName"].ToString()))
                        customerVo.ContactLastName = dr["C_ContactGuardianLastName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_MothersMaidenName"].ToString()))
                        customerVo.MothersMaidenName = dr["C_MothersMaidenName"].ToString();
                    if (dr["CA_AssociationId"].ToString() != string.Empty)
                        customerVo.AssociationId = int.Parse(dr["CA_AssociationId"].ToString());
                    if (dr["XR_RelationshipCode"].ToString() != string.Empty)
                        customerVo.RelationShip = dr["XR_RelationshipCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["ParentCustomer"].ToString()))
                        customerVo.ParentCustomer = dr["ParentCustomer"].ToString();
                    if (!string.IsNullOrEmpty(dr["AR_RM_FullName"].ToString()))
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
                    if (!string.IsNullOrEmpty(dr["C_TaxSlab"].ToString()))
                        customerVo.TaxSlab = int.Parse(dr["C_TaxSlab"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_IsKYCAvailable"].ToString()))
                        customerVo.MfKYC = int.Parse(dr["C_IsKYCAvailable"].ToString());

                    if (!string.IsNullOrEmpty(dr["C_CustCode"].ToString()))
                        customerVo.AccountId = dr["C_CustCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_FatherHusbandName"].ToString()))
                        customerVo.FatherHusbandName = dr["C_FatherHusbandName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_IsRealInvestor"].ToString()))
                        customerVo.IsRealInvestor = bool.Parse(dr["C_IsRealInvestor"].ToString()) ? true : false;
                    if (!string.IsNullOrEmpty(dr["C_WCMV_TaxStatus_Id"].ToString()))
                        customerVo.TaxStatusCustomerSubTypeId = int.Parse(dr["C_WCMV_TaxStatus_Id"].ToString());

                    if (!string.IsNullOrEmpty(dr["C_WCMV_CorrAdrCity_Id"].ToString()))
                        customerVo.CorrespondenceCityId = int.Parse(dr["C_WCMV_CorrAdrCity_Id"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_WCMV_CorrAdrState_Id"].ToString()))
                        customerVo.CorrespondenceStateId = int.Parse(dr["C_WCMV_CorrAdrState_Id"].ToString());

                    if (!string.IsNullOrEmpty(dr["C_WCMV_PermaAdrCity_Id"].ToString()))
                        customerVo.PermanentCityId = int.Parse(dr["C_WCMV_PermaAdrCity_Id"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_WCMV_PermaAdrState_Id"].ToString()))
                        customerVo.PermanentStateId = int.Parse(dr["C_WCMV_PermaAdrState_Id"].ToString());

                    if (!string.IsNullOrEmpty(dr["C_WCMV_OfficeAdrCity_Id"].ToString()))
                        customerVo.OfficeCityId = int.Parse(dr["C_WCMV_OfficeAdrCity_Id"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_WCMV_OfficeAdrState_Id"].ToString()))
                        customerVo.OfficeStateId = int.Parse(dr["C_WCMV_OfficeAdrState_Id"].ToString());

                    if (!string.IsNullOrEmpty(dr["C_WCMV_Occupation_Id"].ToString()))
                        customerVo.OccupationId = int.Parse(dr["C_WCMV_Occupation_Id"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_WCMV_City_Id"].ToString()))
                        customerVo.customerCity = int.Parse(dr["C_WCMV_City_Id"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_WCMV_State_Id"].ToString()))
                        customerVo.customerState = int.Parse(dr["C_WCMV_State_Id"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_MinNO1"].ToString()))
                        customerVo.MinNo1 = dr["C_MinNO1"].ToString();
                    else
                        customerVo.MinNo1 = string.Empty;
                    if (!string.IsNullOrEmpty(dr["C_MinNO2"].ToString()))
                        customerVo.MinNo2 = dr["C_MinNO2"].ToString();
                    else
                        customerVo.MinNo2 = string.Empty;
                    if (!string.IsNullOrEmpty(dr["C_MinNO3"].ToString()))
                        customerVo.MinNo3 = dr["C_MinNO3"].ToString();
                    else
                        customerVo.MinNo3 = string.Empty;
                    if (!string.IsNullOrEmpty(dr["C_ESCNo"].ToString()))
                        customerVo.ESCNo = dr["C_ESCNo"].ToString();
                    else
                        customerVo.ESCNo = string.Empty;
                    if (!string.IsNullOrEmpty(dr["C_UINNo"].ToString()))
                        customerVo.UINNo = dr["C_UINNo"].ToString();
                    else
                        customerVo.UINNo = string.Empty;
                    if (!string.IsNullOrEmpty(dr["C_GuardianName"].ToString()))
                        customerVo.GuardianName = dr["C_GuardianName"].ToString();
                    else
                        customerVo.GuardianName = string.Empty;
                    if (!string.IsNullOrEmpty(dr["XR_GuardianRelation"].ToString()))
                        customerVo.GuardianRelation = dr["XR_GuardianRelation"].ToString();
                    else
                        customerVo.GuardianRelation = string.Empty;
                    if (!string.IsNullOrEmpty(dr["C_ContactGuardianPANNum"].ToString()))
                        customerVo.ContactGuardianPANNum = dr["C_ContactGuardianPANNum"].ToString();
                    else
                        customerVo.ContactGuardianPANNum = string.Empty;
                    if (!string.IsNullOrEmpty(dr["C_POA"].ToString()))
                        customerVo.POA = int.Parse(dr["C_POA"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_AnnualIncome"].ToString()))
                        customerVo.AnnualIncome = decimal.Parse(dr["C_AnnualIncome"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcPhoneExt"].ToString()))
                        customerVo.OfcPhoneExt = Int64.Parse(dr["C_OfcPhoneExt"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_GuardianDOB"].ToString()))
                        customerVo.GuardianDob = Convert.ToDateTime(dr["C_GuardianDOB"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_GuardianMinNo"].ToString()))
                        customerVo.GuardianMinNo = dr["C_GuardianMinNo"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_SubBroker"].ToString()))
                        customerVo.SubBroker = dr["C_SubBroker"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OtherBankName"].ToString()))
                        customerVo.OtherBankName = dr["C_OtherBankName"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_OtherCountry"].ToString()))
                        customerVo.OtherCountry = dr["C_OtherCountry"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr1City"].ToString()))
                        customerVo.Adr1City = dr["C_Adr1City"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr1State"].ToString()))
                        customerVo.Adr1State = dr["C_Adr1State"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_TaxStatus"].ToString()))
                        customerVo.TaxStatus = dr["C_TaxStatus"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Category"].ToString()))
                        customerVo.Category = dr["C_Category"].ToString();


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
                    if (!string.IsNullOrEmpty(dr["C_Adr1PinCode"].ToString()))
                        customerVo.Adr1PinCode = Int64.Parse(dr["C_Adr1PinCode"].ToString());
                    customerVo.Adr1City = dr["C_Adr1City"].ToString();
                    customerVo.Adr1State = dr["C_Adr1State"].ToString();
                    customerVo.Adr1Country = dr["C_Adr1Country"].ToString();
                    customerVo.Adr2Line1 = dr["C_Adr2Line1"].ToString();
                    customerVo.Adr2Line2 = dr["C_Adr2Line2"].ToString();
                    customerVo.Adr2Line3 = dr["C_Adr2Line3"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Adr2PinCode"].ToString()))
                        customerVo.Adr2PinCode = Int64.Parse(dr["C_Adr2PinCode"].ToString());
                    customerVo.Adr2City = dr["C_Adr2City"].ToString();
                    customerVo.Adr2State = dr["C_Adr2State"].ToString();
                    customerVo.Adr2Country = dr["C_Adr2Country"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_ResISDCode"].ToString()))
                        customerVo.ResISDCode = int.Parse(dr["C_ResISDCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_ResSTDCode"].ToString()))
                        customerVo.ResSTDCode = int.Parse(dr["C_ResSTDCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_ResPhoneNum"].ToString()))
                        customerVo.ResPhoneNum = Int64.Parse(dr["C_ResPhoneNum"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcISDCode"].ToString()))
                        customerVo.OfcISDCode = int.Parse(dr["C_OfcISDCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcSTDCode"].ToString()))
                        customerVo.OfcSTDCode = int.Parse(dr["C_OfcSTDCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcPhoneNum"].ToString()))
                        customerVo.OfcPhoneNum = Int64.Parse(dr["C_OfcPhoneNum"].ToString());
                    customerVo.Email = dr["C_Email"].ToString();
                    customerVo.AltEmail = dr["C_AltEmail"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_Mobile1"].ToString()))
                        customerVo.Mobile1 = long.Parse(dr["C_Mobile1"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_Mobile2"].ToString()))
                        customerVo.Mobile2 = long.Parse(dr["C_Mobile2"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_ISDFax"].ToString()))
                        customerVo.ISDFax = int.Parse(dr["C_ISDFax"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_STDFax"].ToString()))
                        customerVo.STDFax = int.Parse(dr["C_STDFax"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_Fax"].ToString()))
                        customerVo.Fax = Int64.Parse(dr["C_Fax"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcFaxISD"].ToString()))
                        customerVo.OfcISDFax = int.Parse(dr["C_OfcFaxISD"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcFax"].ToString()))
                        customerVo.OfcSTDFax = int.Parse(dr["C_OfcFax"].ToString());
                    if (!string.IsNullOrEmpty(dr["C_OfcFax"].ToString()))
                        customerVo.OfcFax = Int64.Parse(dr["C_OfcFax"].ToString());
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
                    if (!string.IsNullOrEmpty(dr["C_OfcAdrPinCode"].ToString()))
                        customerVo.OfcAdrPinCode = Int32.Parse(dr["C_OfcAdrPinCode"].ToString());
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
                    if (!string.IsNullOrEmpty(dr["ACC_CustomerCategoryCode"].ToString()))
                        customerVo.CustomerClassificationID = int.Parse(dr["ACC_CustomerCategoryCode"].ToString());
                    if (!string.IsNullOrEmpty(dr["U_AccountId"].ToString()))
                        customerVo.AccountId = dr["U_AccountId"].ToString();
                    if (!string.IsNullOrEmpty(dr["C_WCMV_TaxStatus_Id"].ToString()))
                        customerVo.TaxStatusCustomerSubTypeId = int.Parse(dr["C_WCMV_TaxStatus_Id"].ToString());

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

        public int GetCustomerSubType(int customerId)
        {
            Database db;
            DbCommand dbCommand;
            int typeCode = 0;
            try
            {


                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbCommand = db.GetStoredProcCommand("SP_GetCustomerSubType");
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Int32, customerId);
                if (db.ExecuteScalar(dbCommand) != null)
                    typeCode = int.Parse(db.ExecuteScalar(dbCommand).ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetCustomerSubType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return typeCode;
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
        public bool UpdateCustomer(CustomerVo customerVo, int userId)
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
                db.AddInParameter(editCustomerCmd, "@C_CustCode", DbType.String, customerVo.AccountId);
                db.AddInParameter(editCustomerCmd, "@C_Adr1Line1", DbType.String, customerVo.Adr1Line1);
                db.AddInParameter(editCustomerCmd, "@C_Adr1Line2", DbType.String, customerVo.Adr1Line2);
                db.AddInParameter(editCustomerCmd, "@C_Adr1Line3", DbType.String, customerVo.Adr1Line3);
                db.AddInParameter(editCustomerCmd, "@C_Adr1PinCode", DbType.Int64, customerVo.Adr1PinCode);
                db.AddInParameter(editCustomerCmd, "@C_Adr1City", DbType.String, customerVo.Adr1City);
                if (customerVo.Adr1State != "Select a State")
                    db.AddInParameter(editCustomerCmd, "@C_Adr1State", DbType.String, customerVo.Adr1State);
                else
                    db.AddInParameter(editCustomerCmd, "@C_Adr1State", DbType.String, "");
                db.AddInParameter(editCustomerCmd, "@C_Adr1Country", DbType.String, customerVo.Adr1Country);
                db.AddInParameter(editCustomerCmd, "@C_Adr2Line1", DbType.String, customerVo.Adr2Line1);
                db.AddInParameter(editCustomerCmd, "@C_Adr2Line2", DbType.String, customerVo.Adr2Line2);
                db.AddInParameter(editCustomerCmd, "@C_Adr2Line3", DbType.String, customerVo.Adr2Line3);
                db.AddInParameter(editCustomerCmd, "@C_Adr2PinCode", DbType.Int64, customerVo.Adr2PinCode);
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
                db.AddInParameter(editCustomerCmd, "@C_ResPhoneNum", DbType.Int64, customerVo.ResPhoneNum);
                db.AddInParameter(editCustomerCmd, "@C_OfcISDCode", DbType.Int32, customerVo.OfcISDCode);
                db.AddInParameter(editCustomerCmd, "@C_OfcSTDCode", DbType.Int32, customerVo.OfcSTDCode);
                db.AddInParameter(editCustomerCmd, "@C_OfcPhoneNum", DbType.Int64, customerVo.OfcPhoneNum);
                db.AddInParameter(editCustomerCmd, "@C_Email", DbType.String, customerVo.Email);
                db.AddInParameter(editCustomerCmd, "@C_AltEmail", DbType.String, customerVo.AltEmail);
                db.AddInParameter(editCustomerCmd, "@C_Mobile1", DbType.Int64, customerVo.Mobile1);
                db.AddInParameter(editCustomerCmd, "@C_Mobile2", DbType.Int64, customerVo.Mobile2);
                db.AddInParameter(editCustomerCmd, "@C_ISDFax", DbType.Int32, customerVo.ISDFax);
                db.AddInParameter(editCustomerCmd, "@C_STDFax", DbType.Int32, customerVo.STDFax);
                db.AddInParameter(editCustomerCmd, "@C_Fax", DbType.Int64, customerVo.Fax);
                db.AddInParameter(editCustomerCmd, "@C_OfcFaxISD", DbType.Int32, customerVo.OfcISDFax);
                db.AddInParameter(editCustomerCmd, "@C_OfcFaxSTD", DbType.Int32, customerVo.OfcSTDFax);
                db.AddInParameter(editCustomerCmd, "@C_OfcFax", DbType.Int64, customerVo.OfcFax);
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
                db.AddInParameter(editCustomerCmd, "@C_OfcAdrPinCode", DbType.Int64, customerVo.OfcAdrPinCode);
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
                if (customerVo.CustomerClassificationID != 0)
                    db.AddInParameter(editCustomerCmd, "@C_ClassCode", DbType.Int32, customerVo.CustomerClassificationID);
                db.AddInParameter(editCustomerCmd, "@C_ProspectAddDate", DbType.DateTime, customerVo.ProspectAddDate);
                db.AddInParameter(editCustomerCmd, "@C_TaxSlab", DbType.Int32, customerVo.TaxSlab);
                db.AddInParameter(editCustomerCmd, "@C_MfKYC", DbType.Int32, customerVo.MfKYC);
                db.AddInParameter(editCustomerCmd, "@C_IsRealInvestor", DbType.Boolean, customerVo.IsRealInvestor ? 1 : 0);
                db.AddInParameter(editCustomerCmd, "@C_WCMV_TaxStatus_Id", DbType.Int32, customerVo.TaxStatusCustomerSubTypeId);

                db.AddInParameter(editCustomerCmd, "@C_WCMV_City_Id", DbType.Int32, customerVo.CorrespondenceCityId);
                db.AddInParameter(editCustomerCmd, "@C_WCMV_State_Id", DbType.Int32, customerVo.CorrespondenceStateId);

                db.AddInParameter(editCustomerCmd, "@C_WCMV_PermaAdrCity_Id", DbType.Int32, customerVo.PermanentCityId);
                db.AddInParameter(editCustomerCmd, "@C_WCMV_PermaAdrState_Id", DbType.Int32, customerVo.PermanentStateId);

                db.AddInParameter(editCustomerCmd, "@C_WCMV_OfficeAdrCity_Id", DbType.Int32, customerVo.OfficeCityId);
                db.AddInParameter(editCustomerCmd, "@C_WCMV_OfficeAdrState_Id", DbType.Int32, customerVo.OfficeStateId);

                db.AddInParameter(editCustomerCmd, "@C_WCMV_Occupation_Id", DbType.Int32, customerVo.OccupationId);
                db.AddInParameter(editCustomerCmd, "@C_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(editCustomerCmd, "@C_FatherHusbandName", DbType.String, customerVo.FatherHusbandName);
                db.AddInParameter(editCustomerCmd, "@C_MinNO1", DbType.String, customerVo.MinNo1);
                db.AddInParameter(editCustomerCmd, "@C_MinNO2", DbType.String, customerVo.MinNo2);
                db.AddInParameter(editCustomerCmd, "@C_MinNO3", DbType.String, customerVo.MinNo3);
                db.AddInParameter(editCustomerCmd, "C_ESCNo", DbType.String, customerVo.ESCNo);
                db.AddInParameter(editCustomerCmd, "@C_UINNo", DbType.String, customerVo.UINNo);
                db.AddInParameter(editCustomerCmd, "@C_GuardianName", DbType.String, customerVo.GuardianName);
                db.AddInParameter(editCustomerCmd, "@XR_GuardianRelation", DbType.String, customerVo.GuardianRelation);
                db.AddInParameter(editCustomerCmd, "@C_ContactGuardianPANNum", DbType.String, customerVo.ContactGuardianPANNum);
                if (customerVo.POA != 0)
                    db.AddInParameter(editCustomerCmd, "@C_POA", DbType.Int32, customerVo.POA);
                if (customerVo.AnnualIncome != 0)
                    db.AddInParameter(editCustomerCmd, "@C_AnnualIncome", DbType.Decimal, customerVo.AnnualIncome);
                if (customerVo.OfcPhoneExt != 0)
                    db.AddInParameter(editCustomerCmd, "@C_OfcPhoneExt", DbType.Int64, customerVo.OfcPhoneExt);
                db.AddInParameter(editCustomerCmd, "@C_GuardianMinNo", DbType.String, customerVo.GuardianMinNo);
                db.AddInParameter(editCustomerCmd, "@C_SubBroker", DbType.String, customerVo.SubBroker);
                if (customerVo.GuardianDob != DateTime.MinValue)
                    db.AddInParameter(editCustomerCmd, "@C_GuardianDOB", DbType.DateTime, customerVo.GuardianDob);
                else
                    db.AddInParameter(editCustomerCmd, "@C_GuardianDOB", DbType.DateTime, DBNull.Value);
                db.AddInParameter(editCustomerCmd, "@C_OtherBankName", DbType.String, customerVo.OtherBankName);
                db.AddInParameter(editCustomerCmd, "@C_OtherCountry", DbType.String, customerVo.OtherCountry);
                db.AddInParameter(editCustomerCmd, "@C_TaxStatus", DbType.String, customerVo.TaxStatus);
                db.AddInParameter(editCustomerCmd, "@C_Category", DbType.String, customerVo.Category);
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



        public DataSet GetAMCExternalType()
        {
            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsAMCExternalType;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetAMCExternalType");
                dsAMCExternalType = db.ExecuteDataSet(cmdGetCustomerNames);
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
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAMCExternalType;
        }

        public bool EditProductAMCSchemeMapping(int schemePlanCode, string strExternalCodeToBeEdited, string strExtCode, int Isonline, string strExtName, DateTime createdDate, DateTime editedDate, DateTime deletedDate, int userid)
        {
            bool bResult = false;
            Database db;
            DbCommand editProductAMCSchemeMappingCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                editProductAMCSchemeMappingCmd = db.GetStoredProcCommand("SP_EditProductAMCSchemeMapping");
                db.AddInParameter(editProductAMCSchemeMappingCmd, "@schemePlanCode", DbType.Int32, schemePlanCode);
                db.AddInParameter(editProductAMCSchemeMappingCmd, "@externalCode", DbType.String, strExtCode);
                db.AddInParameter(editProductAMCSchemeMappingCmd, "@externalCodeToBeEdited", DbType.String, strExternalCodeToBeEdited);
                db.AddInParameter(editProductAMCSchemeMappingCmd, "@externalType", DbType.String, strExtName);
                // db.AddInParameter(editProductAMCSchemeMappingCmd, "@count", DbType.Int32, 0);
                db.AddInParameter(editProductAMCSchemeMappingCmd, "@Isonline", DbType.Int32, Isonline);
                if (createdDate != DateTime.MinValue)
                    db.AddInParameter(editProductAMCSchemeMappingCmd, "@createdDate", DbType.DateTime, createdDate);
                else
                    db.AddInParameter(editProductAMCSchemeMappingCmd, "@createdDate", DbType.DateTime, DBNull.Value);

                if (editedDate != DateTime.MinValue)
                    db.AddInParameter(editProductAMCSchemeMappingCmd, "@editedDate", DbType.DateTime, editedDate);
                else
                    db.AddInParameter(editProductAMCSchemeMappingCmd, "@editedDate", DbType.DateTime, DBNull.Value);

                if (deletedDate != DateTime.MinValue)
                    db.AddInParameter(editProductAMCSchemeMappingCmd, "@deletedDate", DbType.DateTime, deletedDate);
                else
                    db.AddInParameter(editProductAMCSchemeMappingCmd, "@deletedDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(editProductAMCSchemeMappingCmd, "@ModifiedBy", DbType.Int16, userid);
                db.AddInParameter(editProductAMCSchemeMappingCmd, "@CreatedBy", DbType.Int16, userid);
                if (db.ExecuteNonQuery(editProductAMCSchemeMappingCmd) != 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:DeleteMappedSchemeDetails()");
                object[] objects = new object[2];
                objects[0] = schemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool DeleteMappedSchemeDetails(int schemePlanCode, string strExtCode, string strExtName, DateTime createdDate, DateTime editedDate, DateTime deletedDate)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteMappedSchemeDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteMappedSchemeDetailsCmd = db.GetStoredProcCommand("SP_DeleteProductAMCSchemeMappingDetails");
                db.AddInParameter(deleteMappedSchemeDetailsCmd, "@schemePlanCode", DbType.Int32, schemePlanCode);
                db.AddInParameter(deleteMappedSchemeDetailsCmd, "@externalCode", DbType.String, strExtCode);
                db.AddInParameter(deleteMappedSchemeDetailsCmd, "@externalType", DbType.String, strExtName);

                if (createdDate != DateTime.MinValue)
                    db.AddInParameter(deleteMappedSchemeDetailsCmd, "@createdDate", DbType.DateTime, createdDate);
                else
                    db.AddInParameter(deleteMappedSchemeDetailsCmd, "@createdDate", DbType.DateTime, DBNull.Value);

                if (editedDate != DateTime.MinValue)
                    db.AddInParameter(deleteMappedSchemeDetailsCmd, "@editedDate", DbType.DateTime, editedDate);
                else
                    db.AddInParameter(deleteMappedSchemeDetailsCmd, "@editedDate", DbType.DateTime, DBNull.Value);

                if (deletedDate != DateTime.MinValue)
                    db.AddInParameter(deleteMappedSchemeDetailsCmd, "@deletedDate", DbType.DateTime, deletedDate);
                else
                    db.AddInParameter(deleteMappedSchemeDetailsCmd, "@deletedDate", DbType.DateTime, DBNull.Value);

                if (db.ExecuteNonQuery(deleteMappedSchemeDetailsCmd) != 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:DeleteMappedSchemeDetails()");
                object[] objects = new object[2];
                objects[0] = schemePlanCode;
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
                db.AddInParameter(createCustomerCmd, "@C_Adr1PinCode", DbType.Int64, customerVo.Adr1PinCode);
                db.AddInParameter(createCustomerCmd, "@C_Adr1City", DbType.String, customerVo.Adr1City);
                if (customerVo.Adr1State != "Select a State")
                    db.AddInParameter(createCustomerCmd, "@C_Adr1State", DbType.String, customerVo.Adr1State);
                else
                    db.AddInParameter(createCustomerCmd, "@C_Adr1State", DbType.String, "");
                db.AddInParameter(createCustomerCmd, "@C_Adr1Country", DbType.String, customerVo.Adr1Country);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line1", DbType.String, customerVo.Adr2Line1);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line2", DbType.String, customerVo.Adr2Line2);
                db.AddInParameter(createCustomerCmd, "@C_Adr2Line3", DbType.String, customerVo.Adr2Line3);
                db.AddInParameter(createCustomerCmd, "@C_Adr2PinCode", DbType.Int64, customerVo.Adr2PinCode);
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
                db.AddInParameter(createCustomerCmd, "@C_OfcPhoneNum", DbType.Int64, customerVo.OfcPhoneNum);
                db.AddInParameter(createCustomerCmd, "@C_Email", DbType.String, customerVo.Email);
                db.AddInParameter(createCustomerCmd, "@C_AltEmail", DbType.String, customerVo.AltEmail);
                db.AddInParameter(createCustomerCmd, "@C_Mobile1", DbType.Int64, customerVo.Mobile1);
                db.AddInParameter(createCustomerCmd, "@C_Mobile2", DbType.Int64, customerVo.Mobile2);
                db.AddInParameter(createCustomerCmd, "@C_ISDFax", DbType.Int32, customerVo.ISDFax);
                db.AddInParameter(createCustomerCmd, "@C_STDFax", DbType.Int32, customerVo.STDFax);
                db.AddInParameter(createCustomerCmd, "@C_Fax", DbType.Int64, customerVo.Fax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxISD", DbType.Int32, customerVo.OfcISDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFaxSTD", DbType.Int32, customerVo.OfcSTDFax);
                db.AddInParameter(createCustomerCmd, "@C_OfcFax", DbType.Int64, customerVo.OfcFax);
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
                db.AddInParameter(createCustomerCmd, "@C_OfcAdrPinCode", DbType.Int64, customerVo.OfcAdrPinCode);
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
                if (userVo.Email != null)
                    db.AddInParameter(createCustomerCmd, "@U_Email", DbType.String, userVo.Email.ToString());
                else
                    db.AddInParameter(createCustomerCmd, "@U_Email", DbType.String, DBNull.Value);

                db.AddInParameter(createCustomerCmd, "@U_UserType", DbType.String, "Customer");
                db.AddInParameter(createCustomerCmd, "@U_LoginId", DbType.String, userVo.LoginId);

                db.AddInParameter(createCustomerCmd, "@CP_IsMainPortfolio", DbType.Int16, customerPortfolioVo.IsMainPortfolio);
                db.AddInParameter(createCustomerCmd, "@XPT_PortfolioTypeCode", DbType.String, customerPortfolioVo.PortfolioTypeCode);
                db.AddInParameter(createCustomerCmd, "@CP_PMSIdentifier", DbType.String, customerPortfolioVo.PMSIdentifier);
                db.AddInParameter(createCustomerCmd, "@CP_PortfolioName", DbType.String, customerPortfolioVo.PortfolioName);
                db.AddInParameter(createCustomerCmd, "@C_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerCmd, "@C_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createCustomerCmd, "@C_DummyPAN", DbType.Int32, customerVo.DummyPAN);
                db.AddInParameter(createCustomerCmd, "@C_ProspectAddDate", DbType.DateTime, customerVo.ProspectAddDate);
                db.AddOutParameter(createCustomerCmd, "@C_CustomerId", DbType.Int32, 10);
                db.AddOutParameter(createCustomerCmd, "@U_UserId", DbType.Int32, 10);
                db.AddOutParameter(createCustomerCmd, "@CP_PortfolioId", DbType.Int32, 10);
                db.AddInParameter(createCustomerCmd, "@C_TaxSlab", DbType.Int32, customerVo.TaxSlab);
                db.AddInParameter(createCustomerCmd, "@C_AlertViaSMS", DbType.Int16, customerVo.ViaSMS);
                db.AddInParameter(createCustomerCmd, "@CPS_GuardPan", DbType.String, customerVo.GuardPANNum);

                db.AddInParameter(createCustomerCmd, "@C_MfKYC", DbType.Int32, customerVo.MfKYC);
                db.AddInParameter(createCustomerCmd, "@C_IsRealInvestor", DbType.Boolean, customerVo.IsRealInvestor ? 1 : 0);
                db.AddInParameter(createCustomerCmd, "@C_WCMV_TaxStatus_Id", DbType.Int16, customerVo.TaxStatusCustomerSubTypeId);


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
        public DataTable GetAllRMMemberCustomerName(string prefixText, int rmId)
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAllRMMemberCustomerName()");


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
        public DataTable GetAgentCodeAssociateDetails(string prefixText, int Adviserid)
        {

            Database db;
            DbCommand cmdGetAgentCodeAssociateDetails;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAgentCodeAssociateDetails = db.GetStoredProcCommand("GetAgentCodeAssociateDetails");
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@A_AdviserId", DbType.Int32, Adviserid);

                dsCustomerNames = db.ExecuteDataSet(cmdGetAgentCodeAssociateDetails);
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
        public DataTable GetAgentCodeDetails(string prefixText, int Adviserid)
        {

            Database db;
            DbCommand cmdGetAgentCodeAssociateDetails;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAgentCodeAssociateDetails = db.GetStoredProcCommand("SP_GetAgentCodeDetails");
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@A_AdviserId", DbType.Int32, Adviserid);

                dsCustomerNames = db.ExecuteDataSet(cmdGetAgentCodeAssociateDetails);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAgentCodeDetails()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public DataTable GetAssociateNameDetails(string prefixText, int Adviserid)
        {

            Database db;
            DbCommand cmdGetAgentCodeAssociateDetails;
            DataSet dsAssociatesNames;
            DataTable dtAssociatesNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAgentCodeAssociateDetails = db.GetStoredProcCommand("GetAssociateNameDetails");
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@A_AdviserId", DbType.Int32, Adviserid);
                dsAssociatesNames = db.ExecuteDataSet(cmdGetAgentCodeAssociateDetails);
                dtAssociatesNames = dsAssociatesNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAssociateNameDetails()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtAssociatesNames;
        }
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
        public DataTable GetAdviserCustomerPan(string prefixText, int adviserId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetIndividualCustomerPan");
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
        public DataTable GetAdviserAllCustomerPan(string prefixText, int register, int adviserId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetAllIndividualCustomerPan");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@Registration", DbType.Int32, register);
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
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetIndividualCustomerName");
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
        public DataTable GetAdviserAllCustomerName(string prefixText, int register, int adviserId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetIndividualAllCustomerName");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@Registration", DbType.Int32, register);
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
        public DataTable GetStaffName(string prefixText, int adviserId)
        {

            Database db;
            DbCommand cmdGetRMStaffList;
            DataSet dsGetRMStaffList;
            DataTable dtGetRMStaffList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRMStaffList = db.GetStoredProcCommand("SPROC_GetAdviserRmStaffList");
                db.AddInParameter(cmdGetRMStaffList, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetRMStaffList, "@adviserId", DbType.Int32, adviserId);
                dsGetRMStaffList = db.ExecuteDataSet(cmdGetRMStaffList);
                dtGetRMStaffList = dsGetRMStaffList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetRMStaffList;
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
        public int GetCustomerGroupHead(int customerId)
        {
            int result;
            Database db;
            DbCommand checkCustomerGrpHeadCmd;


            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkCustomerGrpHeadCmd = db.GetStoredProcCommand("SP_GetCustomerGrpHead");
                db.AddInParameter(checkCustomerGrpHeadCmd, "@CustomerId", DbType.Int32, customerId);

                result = int.Parse(db.ExecuteScalar(checkCustomerGrpHeadCmd).ToString());

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

        public DataSet GetCustomerPortfolioList(int customerId)
        {

            Database db;
            DbCommand getCustomerListCmd;
            DataSet getCustomerDs;

            db = DatabaseFactory.CreateDatabase("wealtherp");
            getCustomerListCmd = db.GetStoredProcCommand("SP_CustomerPortfolioList");
            db.AddInParameter(getCustomerListCmd, "@CustomerID", DbType.Int32, customerId);
            getCustomerDs = db.ExecuteDataSet(getCustomerListCmd);
            return getCustomerDs;


        }
        public DataTable GetAllCustomerName(string prefixText, int adviserId)
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAllCustomerName()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
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
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetAllIdividualRMCustomerNames");
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


        public DataTable BindDropDownassumption(string flag)
        {

            Database db;
            DbCommand cmdBindDropDownassumption;
            DataSet dsBindDropDownassumption;
            DataTable dtBindDropDownassumption;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdBindDropDownassumption = db.GetStoredProcCommand("SP_GetAssumptionList");
                db.AddInParameter(cmdBindDropDownassumption, "@flag", DbType.String, flag);
                dsBindDropDownassumption = db.ExecuteDataSet(cmdBindDropDownassumption);
                dtBindDropDownassumption = dsBindDropDownassumption.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtBindDropDownassumption;
        }



        public bool InsertProductAMCSchemeMappingDetalis(int schemePlanCode, string externalCode, string externalType, DateTime createdDate, DateTime editedDate, DateTime deletedDate)
        {
            bool isInserted = false;
            Database db;
            DbCommand cmdInsertProductAMCSchemeMappingDetalis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertProductAMCSchemeMappingDetalis = db.GetStoredProcCommand("SP_InsertIntoProductSchemePlanMapping");
                db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@schemePlanCode", DbType.Int32, schemePlanCode);
                db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@externalCode", DbType.String, externalCode);
                db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@externalType", DbType.String, externalType);

                if (createdDate != DateTime.MinValue)
                    db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@createdDate", DbType.DateTime, createdDate);
                else
                    db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@createdDate", DbType.DateTime, DBNull.Value);
                if (editedDate != DateTime.MinValue)
                    db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@editedDate", DbType.DateTime, editedDate);
                else
                    db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@editedDate", DbType.DateTime, DBNull.Value);
                if (deletedDate != DateTime.MinValue)
                    db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@deletedDate", DbType.DateTime, deletedDate);
                else
                    db.AddInParameter(cmdInsertProductAMCSchemeMappingDetalis, "@deletedDate", DbType.DateTime, DBNull.Value);
                db.ExecuteNonQuery(cmdInsertProductAMCSchemeMappingDetalis);
                isInserted = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isInserted;
        }

        public void InsertCustomerStaticDetalis(int userId, int customerId, decimal assumptionValue, string assumptionType)
        {

            Database db;
            DbCommand cmdInsertCustomerStaticDetalis;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdInsertCustomerStaticDetalis = db.GetStoredProcCommand("sp_InsertCustomerStaticAssumption");
                db.AddInParameter(cmdInsertCustomerStaticDetalis, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(cmdInsertCustomerStaticDetalis, "@WA_AssumptionId", DbType.String, assumptionType);
                db.AddInParameter(cmdInsertCustomerStaticDetalis, "@CSA_Value", DbType.Decimal, assumptionValue);
                db.AddInParameter(cmdInsertCustomerStaticDetalis, "@CSA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(cmdInsertCustomerStaticDetalis, "@CSA_ModifiedBy", DbType.Int32, userId);
                db.ExecuteDataSet(cmdInsertCustomerStaticDetalis);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        public void UpdateCustomerProjectedDetalis(int userId, int customerId, decimal assumptionValue, string assumptionType)
        {

            Database db;
            DbCommand cmdUpdateCustomerProjectedDetalis;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdUpdateCustomerProjectedDetalis = db.GetStoredProcCommand("SP_UpdateCustomerProjectedDetalis");
                db.AddInParameter(cmdUpdateCustomerProjectedDetalis, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(cmdUpdateCustomerProjectedDetalis, "@WA_AssumptionId", DbType.String, assumptionType);
                db.AddInParameter(cmdUpdateCustomerProjectedDetalis, "@CPA_Value", DbType.Decimal, assumptionValue);
                db.AddInParameter(cmdUpdateCustomerProjectedDetalis, "@CPA_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(cmdUpdateCustomerProjectedDetalis, "@CPA_ModifiedBy", DbType.Int32, userId);
                db.ExecuteDataSet(cmdUpdateCustomerProjectedDetalis);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public int ExpiryAgeOfAdviser(int adviserId, int customerId)
        {

            Database db;
            DbCommand cmdExpiryAgeOfAdviser;
            int expiryAge;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdExpiryAgeOfAdviser = db.GetStoredProcCommand("sp_ExpiryAgeOfAdviser");
                db.AddInParameter(cmdExpiryAgeOfAdviser, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdExpiryAgeOfAdviser, "@customerId", DbType.Int32, customerId);
                //db.AddOutParameter(cmdExpiryAgeOfAdviser, "@LE", DbType.Int32, 1000);
                db.AddOutParameter(cmdExpiryAgeOfAdviser, "@LE", DbType.Int16, 100);

                db.ExecuteNonQuery(cmdExpiryAgeOfAdviser);
                expiryAge = Convert.ToInt16(db.GetParameterValue(cmdExpiryAgeOfAdviser, "@LE").ToString());

                //expiryAge = (int)cmdExpiryAgeOfAdviser.Parameters["@LE"].Value;

                //Object objRes = db.GetParameterValue(cmdExpiryAgeOfAdviser, "@LE");
                //if (objRes != DBNull.Value)
                //    expiryAge = (int)db.GetParameterValue(cmdExpiryAgeOfAdviser, "@LE");
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return expiryAge;
        }

        public DataSet GetAllCustomersAssumptions(int customerId, int adviserid)
        {

            Database db;
            DbCommand cmdGetAllCustomersAssumptions;
            DataSet dsGetAllCustomersAssumptions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAllCustomersAssumptions = db.GetStoredProcCommand("SP_GetAllCustomersAssumptions");
                db.AddInParameter(cmdGetAllCustomersAssumptions, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmdGetAllCustomersAssumptions, "@AdviserId", DbType.Int32, adviserid);

                dsGetAllCustomersAssumptions = db.ExecuteDataSet(cmdGetAllCustomersAssumptions);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetAllCustomersAssumptions;
        }


        public void InsertPlanPreferences(int customerId, int calculationBasisId, int calculationId)
        {
            Database db;
            DbCommand cmdInsertPlanPreferences;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertPlanPreferences = db.GetStoredProcCommand("SP_InsertPlanPreferences");
                db.AddInParameter(cmdInsertPlanPreferences, "@C_CustomerId ", DbType.Int32, customerId);
                db.AddInParameter(cmdInsertPlanPreferences, "@CalculationBasisId ", DbType.Int32, calculationBasisId);
                db.AddInParameter(cmdInsertPlanPreferences, "@CalculationId", DbType.Int32, calculationId);
                db.AddOutParameter(cmdInsertPlanPreferences, "@res", DbType.Int32, 0);

                db.ExecuteNonQuery(cmdInsertPlanPreferences);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }


        }
        public DataSet SetDefaultPlanRetirementValueForCustomer(int customerId)
        {
            Database db;
            DbCommand cmdSetDefaultPlanRetirementValueForCustomer;
            DataSet dsSetDefaultPlanRetirementValueForCustomer;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdSetDefaultPlanRetirementValueForCustomer = db.GetStoredProcCommand("SP_PlanRetirementValue");
                db.AddInParameter(cmdSetDefaultPlanRetirementValueForCustomer, "@C_CustomerId ", DbType.Int32, customerId);
                dsSetDefaultPlanRetirementValueForCustomer = db.ExecuteDataSet(cmdSetDefaultPlanRetirementValueForCustomer);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSetDefaultPlanRetirementValueForCustomer;
        }
        public DataTable GetBMParentCustomerNames(string prefixText, int rmId)
        {

            Database db;
            DbCommand cmdGetBMParentCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetBMParentCustomerNames = db.GetStoredProcCommand("SP_GetBMParentCustomerNames");
                db.AddInParameter(cmdGetBMParentCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetBMParentCustomerNames, "@RMId", DbType.Int32, rmId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetBMParentCustomerNames);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetBMParentCustomerNames()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;


        }
        public DataTable GetBMIndividualCustomerNames(string prefixText, int rmId)
        {

            Database db;
            DbCommand cmdGetBMParentCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetBMParentCustomerNames = db.GetStoredProcCommand("SP_GetBMIndividualCustomerNames");
                db.AddInParameter(cmdGetBMParentCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetBMParentCustomerNames, "@RMId", DbType.Int32, rmId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetBMParentCustomerNames);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetBMIndividualCustomerNames()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;


        }
        // Added for FP Sectional Report.. Added on 13th June 2011
        public DataSet DefaultFPReportsAssumtion(int customerId)
        {
            Database db;
            DbCommand DefaultFPReportsAssumtionCmd;

            DataSet ds;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                DefaultFPReportsAssumtionCmd = db.GetStoredProcCommand("SP_DefaultCustomerFPReportsAssumption");
                db.AddInParameter(DefaultFPReportsAssumtionCmd, "@customerId", DbType.Int32, customerId);
                ds = db.ExecuteDataSet(DefaultFPReportsAssumtionCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return ds;
        }
        public bool ChckBussinessDate(DateTime chckdate)
        {
            DataSet dsBussinessDate = new DataSet();
            Database db;
            DbCommand ChckBussinessDatecmd;
            bool isCorrect;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                ChckBussinessDatecmd = db.GetStoredProcCommand("SP_ChckBussinessDate");
                db.AddInParameter(ChckBussinessDatecmd, "@bussdate", DbType.DateTime, chckdate);
                dsBussinessDate = db.ExecuteDataSet(ChckBussinessDatecmd);
                if (dsBussinessDate.Tables != null && dsBussinessDate.Tables.Count > 0 && dsBussinessDate.Tables[0].Rows.Count > 0)
                    isCorrect = true;
                else
                    isCorrect = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isCorrect;
        }

        public void CustomerFPReportsAssumption(int customerId, decimal assumptionInflation, decimal assumptionInvestment, decimal assumptionDr)
        {
            Database db;
            DbCommand CustomerAssumptionCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CustomerAssumptionCmd = db.GetStoredProcCommand("SP_UpdateCustomerFPReportsAssumption");
                db.AddInParameter(CustomerAssumptionCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(CustomerAssumptionCmd, "@assumptionInflation", DbType.Double, assumptionInflation);
                db.AddInParameter(CustomerAssumptionCmd, "@assumptionInvestment", DbType.Double, assumptionInvestment);
                db.AddInParameter(CustomerAssumptionCmd, "@assumptionDr", DbType.Double, assumptionDr);



                db.ExecuteNonQuery(CustomerAssumptionCmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        public void AddRMRecommendationForCustomer(int customerId, string strRMRecHTML)
        {
            Database db;
            DbCommand CustomerRMRecommendationCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CustomerRMRecommendationCmd = db.GetStoredProcCommand("SP_InsertCustomerRMRecommendationText");
                db.AddInParameter(CustomerRMRecommendationCmd, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(CustomerRMRecommendationCmd, "@RMRecHTML_Text", DbType.String, strRMRecHTML);
                db.ExecuteNonQuery(CustomerRMRecommendationCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }

        public string GetRMRecommendationForCustomer(int customerId)
        {
            Database db;
            DbCommand getCustomerRMRecommendationCmd;
            DataSet getCustomerRMRecommendationDS;
            string strRMRecommendationHTML;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerRMRecommendationCmd = db.GetStoredProcCommand("SP_GetCustomerRMRecommendationText");
                db.AddInParameter(getCustomerRMRecommendationCmd, "@CustomerId", DbType.Int32, customerId);
                db.ExecuteNonQuery(getCustomerRMRecommendationCmd);
                getCustomerRMRecommendationDS = db.ExecuteDataSet(getCustomerRMRecommendationCmd);
                if (getCustomerRMRecommendationDS.Tables[0].Rows.Count > 0)
                {
                    strRMRecommendationHTML = Convert.ToString(getCustomerRMRecommendationDS.Tables[0].Rows[0][0]);
                }
                else
                {
                    strRMRecommendationHTML = string.Empty;

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return strRMRecommendationHTML;
        }
        public DataSet GetCustomerTaxSlab(int CustomerID, int age, string Gender)
        {
            Database db;
            DbCommand cmdGetTaxSlab;
            DataSet dsGetTaxSlab = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetTaxSlab = db.GetStoredProcCommand("SP_GetTaxSlab");
                db.AddInParameter(cmdGetTaxSlab, "@C_Age", DbType.Int32, age);
                db.AddInParameter(cmdGetTaxSlab, "@C_Gender", DbType.String, Gender);
                db.AddInParameter(cmdGetTaxSlab, "@CustomerId", DbType.Int32, CustomerID);
                dsGetTaxSlab = db.ExecuteDataSet(cmdGetTaxSlab);
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
                object[] objects = new object[3];
                objects[0] = age;
                objects[1] = Gender;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetTaxSlab;
        }

        // To Check and Delete the Child Customers.. 
        // Added by Vinayak Patil..

        public int CheckAndDeleteTheChildCustomers(string Flag, int CustomerId)
        {
            CustomerDao customerDao = new CustomerDao();
            int associationStatus = 0;
            int getCount = 0;
            Database db;
            DbCommand cmddeleteChildCustomer;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmddeleteChildCustomer = db.GetStoredProcCommand("SP_CheckAndDeleteTheChildCustomers");
                db.AddInParameter(cmddeleteChildCustomer, "@Flag", DbType.String, Flag);
                db.AddInParameter(cmddeleteChildCustomer, "@C_CustomerId", DbType.Int32, CustomerId);
                db.AddOutParameter(cmddeleteChildCustomer, "@CountFlag", DbType.Int32, 0);
                getCount = db.ExecuteNonQuery(cmddeleteChildCustomer);
                getCount = (int)db.GetParameterValue(cmddeleteChildCustomer, "@CountFlag");

                if (getCount != 0)
                    associationStatus = 1;
                else
                    associationStatus = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:CheckAndDeleteTheChildCustomers()");


                object[] objects = new object[3];
                objects[0] = Flag;
                objects[1] = CustomerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return associationStatus;
        }

        // To delete the child customer <<Added by Vinayak Patil>>

        public bool DeleteChildCustomer(int customerId, string Flag)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteCustomerBankCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCustomerBankCmd = db.GetStoredProcCommand("SP_CheckAndDeleteTheChildCustomers");
                db.AddInParameter(deleteCustomerBankCmd, "@C_CustomerId", DbType.Int32, customerId);
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

        public void CheckSpouseRelationship(int customerId, out bool spouseRelationExist, out bool spouseDobExist, out bool spouseAssumptionExist)
        {
            bool result = false;
            Database db;
            DbCommand checkSpouseRelationshipCmd;
            DataSet dsCheckSpouseRelationship;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                checkSpouseRelationshipCmd = db.GetStoredProcCommand("SP_CheckSpouseRelationShip");
                db.AddInParameter(checkSpouseRelationshipCmd, "@customerId", DbType.Int32, customerId);
                db.AddOutParameter(checkSpouseRelationshipCmd, "@isSpouseRelationExist", DbType.Int32, 50);
                db.AddOutParameter(checkSpouseRelationshipCmd, "@isSpouseDobExist ", DbType.Int32, 50);
                db.AddOutParameter(checkSpouseRelationshipCmd, "@isSpouseAssemptionExist", DbType.Int32, 50);
                dsCheckSpouseRelationship = db.ExecuteDataSet(checkSpouseRelationshipCmd);

                Object objspCustomerId = db.GetParameterValue(checkSpouseRelationshipCmd, "@isSpouseRelationExist");
                Object objspDOB = db.GetParameterValue(checkSpouseRelationshipCmd, "@isSpouseDobExist ");
                Object objcount = db.GetParameterValue(checkSpouseRelationshipCmd, "@isSpouseAssemptionExist");
                if (objspCustomerId != DBNull.Value)
                {
                    if (int.Parse(db.GetParameterValue(checkSpouseRelationshipCmd, "@isSpouseRelationExist").ToString()) == 1)
                        spouseRelationExist = true;
                    else
                        spouseRelationExist = false;
                }
                else
                    spouseRelationExist = false;


                if (objspCustomerId != DBNull.Value)
                {
                    if (int.Parse(db.GetParameterValue(checkSpouseRelationshipCmd, "@isSpouseRelationExist").ToString()) == 1)
                        spouseDobExist = true;
                    else
                        spouseDobExist = false;

                }
                else
                    spouseDobExist = false;


                if (objspCustomerId != DBNull.Value)
                {
                    if (int.Parse(db.GetParameterValue(checkSpouseRelationshipCmd, "@isSpouseRelationExist").ToString()) > 0)
                        spouseAssumptionExist = true;
                    else
                        spouseAssumptionExist = false;
                }
                else
                    spouseAssumptionExist = false;

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:CheckSpouseRelationship()");
                object[] objects = new object[1];
                objects[0] = customerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        /// <summary>
        /// To get all Individual and Group Customers for the Brach and RM Selection.
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="rmId"></param>
        /// <param name="prefixText"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        /// 
        public DataTable GetRMBranchIndividualCustomerNames(string contextKey, string prefixText)
        {

            Database db;
            DbCommand cmdGetAllBMRMParentCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;
            string branchId = string.Empty;
            string rmId = string.Empty;


            string[] IDinKeys = contextKey.Split('~');
            foreach (string IDs in IDinKeys)
            {
                if (branchId == string.Empty)
                {
                    branchId = IDs;
                }
                else if (rmId == string.Empty)
                {
                    rmId = IDs;
                }
            }
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAllBMRMParentCustomerNames = db.GetStoredProcCommand("SP_GetBranchAndRMIndividualCustomers");

                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@AB_BranchId", DbType.Int32, int.Parse(branchId));
                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@AR_RMId", DbType.Int32, int.Parse(rmId));
                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@prefixText", DbType.String, prefixText);

                dsCustomerNames = db.ExecuteDataSet(cmdGetAllBMRMParentCustomerNames);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetBMIndividualCustomerNames()");
                object[] objects = new object[1];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetRMBranchGroupCustomerNames(string contextKey, string prefixText)
        {

            Database db;
            DbCommand cmdGetAllBMRMParentCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;
            string branchId = string.Empty;
            string rmId = string.Empty;

            string[] IDinKeys = contextKey.Split('~');
            foreach (string IDs in IDinKeys)
            {
                if (branchId == string.Empty)
                {
                    branchId = IDs;
                }
                else if (rmId == string.Empty)
                {
                    rmId = IDs;
                }
            }

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAllBMRMParentCustomerNames = db.GetStoredProcCommand("SP_GetBranchAndRMGroupCustomers");

                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@AB_BranchId", DbType.Int32, int.Parse(branchId));
                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@AR_RMId", DbType.Int32, int.Parse(rmId));
                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@prefixText", DbType.String, prefixText);

                dsCustomerNames = db.ExecuteDataSet(cmdGetAllBMRMParentCustomerNames);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetBMIndividualCustomerNames()");
                object[] objects = new object[1];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetPerticularBranchsAllIndividualCustomers(string contextKey, string prefixText)
        {

            Database db;
            DbCommand cmdGetAllBMRMParentCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAllBMRMParentCustomerNames = db.GetStoredProcCommand("SP_GetPerticularBranchIndividualCustomerNames");

                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@AB_BranchId", DbType.Int32, int.Parse(contextKey));
                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@prefixText", DbType.String, prefixText);

                dsCustomerNames = db.ExecuteDataSet(cmdGetAllBMRMParentCustomerNames);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetBMIndividualCustomerNames()");
                object[] objects = new object[1];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetPerticularBranchsAllGroupCustomers(string contextKey, string prefixText)
        {

            Database db;
            DbCommand cmdGetAllBMRMParentCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAllBMRMParentCustomerNames = db.GetStoredProcCommand("SP_GetPerticularBranchGroupCustomerNames");

                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@AB_BranchId", DbType.Int32, int.Parse(contextKey));
                db.AddInParameter(cmdGetAllBMRMParentCustomerNames, "@prefixText", DbType.String, prefixText);

                dsCustomerNames = db.ExecuteDataSet(cmdGetAllBMRMParentCustomerNames);
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

                FunctionInfo.Add("Method", "CustomerDao.cs:GetBMIndividualCustomerNames()");
                object[] objects = new object[1];
                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public bool PANNumberDuplicateChild(int adviserId, string Pan)
        {
            Database db;
            DbCommand cmdPanDuplicateCheck;
            bool bResult = false;
            int res = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdPanDuplicateCheck = db.GetStoredProcCommand("SP_PANDuplicateCheckForChild");
                db.AddInParameter(cmdPanDuplicateCheck, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdPanDuplicateCheck, "@PAN", DbType.String, Pan);
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
                FunctionInfo.Add("Method", "CustomerDao.cs:PANNumberDuplicateChild()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = Pan;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public DataSet GetExceptionType(bool isISA)
        {
            Database db;
            DataSet dsGetExceptionType = new DataSet();
            DbCommand cmdGetExceptionType;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdGetExceptionType = db.GetStoredProcCommand("GetExceptionType");
                db.AddInParameter(cmdGetExceptionType, "@isISA", DbType.Boolean, isISA);
                dsGetExceptionType = db.ExecuteDataSet(cmdGetExceptionType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:PANNumberDuplicateChild()");
                //object[] objects = new object[2];
                //objects[0] = adviserId;
                //objects[1] = Pan;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionType;
        }
        public DataSet GetExceptionList()
        {
            Database db;
            DataSet dsGetExceptionList = new DataSet();
            DbCommand cmdGetGetExceptionList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdGetGetExceptionList = db.GetStoredProcCommand("GetExceptionList");
                dsGetExceptionList = db.ExecuteDataSet(cmdGetGetExceptionList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:PANNumberDuplicateChild()");
                //object[] objects = new object[2];
                //objects[0] = adviserId;
                //objects[1] = Pan;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionList;
        }
        public DataTable GetCustomerProofTypes()
        {

            Database db;
            DbCommand cmdGetCustomerProofTypes;
            DataSet dsCustomerProofTypes;
            DataTable dtCustomerProofTypes;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerProofTypes = db.GetStoredProcCommand("SP_GetCustomerProofTypes");

                dsCustomerProofTypes = db.ExecuteDataSet(cmdGetCustomerProofTypes);
                dtCustomerProofTypes = dsCustomerProofTypes.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofTypes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerProofTypes;
        }

        public DataTable GetCustomerProofsForTypes(int proofTypeCode)
        {

            Database db;
            DbCommand cmdGetCustomerProofsForTypes;
            DataSet dsCustomerProofsForTypes;
            DataTable dtCustomerProofsForTypes;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerProofsForTypes = db.GetStoredProcCommand("SP_GetCustomerProofsForProofTypes");

                db.AddInParameter(cmdGetCustomerProofsForTypes, "@XPRTProofTypeCode", DbType.Int32, proofTypeCode);

                dsCustomerProofsForTypes = db.ExecuteDataSet(cmdGetCustomerProofsForTypes);
                dtCustomerProofsForTypes = dsCustomerProofsForTypes.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofTypes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerProofsForTypes;
        }

        public DataTable GetCustomerProofPurpose()
        {

            Database db;
            DbCommand cmdGetCustomerProofPurpose;
            DataSet dsGetCustomerProofPurpose;
            DataTable dtGetCustomerProofPurpose;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerProofPurpose = db.GetStoredProcCommand("SP_GetCustomerProofPurposes");

                dsGetCustomerProofPurpose = db.ExecuteDataSet(cmdGetCustomerProofPurpose);
                dtGetCustomerProofPurpose = dsGetCustomerProofPurpose.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofTypes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerProofPurpose;
        }

        public DataTable GetCustomerProofCopy()
        {

            Database db;
            DbCommand cmdGetCustomerProofCopy;
            DataSet dsGetCustomerProofCopy;
            DataTable dtGetCustomerProofCopy;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerProofCopy = db.GetStoredProcCommand("SP_GetCustomerProofCopy");

                dsGetCustomerProofCopy = db.ExecuteDataSet(cmdGetCustomerProofCopy);
                dtGetCustomerProofCopy = dsGetCustomerProofCopy.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofTypes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerProofCopy;
        }

        public bool CreateCustomersProofUploads(CustomerProofUploadsVO CPUVo, int ProofUploadId, string createOrUpdate)
        {
            bool bStatus = false;
            Database db;
            DbCommand cmdInsertPlanPreferences;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertPlanPreferences = db.GetStoredProcCommand("SP_CreateCustomerUploadedProofs");
                db.AddInParameter(cmdInsertPlanPreferences, "@CustomerID", DbType.Int32, CPUVo.CustomerId);
                db.AddInParameter(cmdInsertPlanPreferences, "@XPRT_ProofTypeCode", DbType.Int32, CPUVo.ProofTypeCode);
                db.AddInParameter(cmdInsertPlanPreferences, "@XP_ProofCode", DbType.Int32, CPUVo.ProofCode);
                db.AddInParameter(cmdInsertPlanPreferences, "@XPCT_ProofCopyTypeCode", DbType.String, CPUVo.ProofCopyTypeCode);
                if (!CPUVo.ProofImage.Equals(String.Empty))
                    db.AddInParameter(cmdInsertPlanPreferences, "@CPU_Image", DbType.String, CPUVo.ProofImage);
                else
                    db.AddInParameter(cmdInsertPlanPreferences, "@CPU_Image", DbType.String, DBNull.Value);
                db.AddInParameter(cmdInsertPlanPreferences, "@ProofIDUpdate", DbType.Int32, ProofUploadId);

                db.AddOutParameter(cmdInsertPlanPreferences, "@CPU_ProofUploadId", DbType.Int32, 0);

                if (db.ExecuteNonQuery(cmdInsertPlanPreferences) != 0)
                    bStatus = true;

                if (createOrUpdate == "Submit")
                    ProofUploadId = Convert.ToInt32(db.GetParameterValue(cmdInsertPlanPreferences, "@CPU_ProofUploadId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProofTypes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bStatus;
        }
        public void CreateCustomerOrderDocument(CustomerProofUploadsVO CPUVo, int OrderId)
        {

            Database db;
            DbCommand getCustcmd;
            try
            {


                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustcmd = db.GetStoredProcCommand("Sp_CreateCustomerOrderDocument");
                db.AddInParameter(getCustcmd, "@orderid", DbType.Int32, OrderId);
                db.AddInParameter(getCustcmd, "@ProofTypeCode", DbType.Int32, CPUVo.ProofTypeCode);
                db.AddInParameter(getCustcmd, "@ProofCode", DbType.Int32, CPUVo.ProofCode);
                db.AddInParameter(getCustcmd, "@image", DbType.String, CPUVo.ProofImage);
                db.ExecuteNonQuery(getCustcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }

        }

        public DataTable GetCustomerUploadedProofs(int customerId, int proofId)
        {

            Database db;
            DbCommand cmdGetCustomerUploadedProofs;
            DataSet dsGetCustomerUploadedProofs;
            DataTable dtGetCustomerUploadedProofs = new DataTable();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerUploadedProofs = db.GetStoredProcCommand("SP_GetCustomerUploadedProofs");

                db.AddInParameter(cmdGetCustomerUploadedProofs, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmdGetCustomerUploadedProofs, "@CPU_ProofUploadId", DbType.Int32, proofId);

                dsGetCustomerUploadedProofs = db.ExecuteDataSet(cmdGetCustomerUploadedProofs);
                if (dsGetCustomerUploadedProofs.Tables.Count != 0)
                    dtGetCustomerUploadedProofs = dsGetCustomerUploadedProofs.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerUploadedProofs(int customerId)");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerUploadedProofs;
        }

        public bool DeleteCustomerUploadedProofs(int customerId, int proofUploadID, float fBalanceStorage, int adviserId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteCustomerProofCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCustomerProofCmd = db.GetStoredProcCommand("SP_DeleteCustomerUploadedProofs");
                db.AddInParameter(deleteCustomerProofCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(deleteCustomerProofCmd, "@CPU_ProofUploadId", DbType.Int32, proofUploadID);
                db.AddInParameter(deleteCustomerProofCmd, "@BalanceStorage", DbType.Decimal, fBalanceStorage);
                db.AddInParameter(deleteCustomerProofCmd, "@AdviserId", DbType.Int32, adviserId);
                if (db.ExecuteNonQuery(deleteCustomerProofCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerDao.cs:DeleteCustomerUploadedProofs()");

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

        public bool CreateCustomersProofPurposes(int ProofUploadedID, string PurposeCode)
        {
            bool bStatus = false;
            Database db;
            DbCommand cmdInsertPlanPreferences;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertPlanPreferences = db.GetStoredProcCommand("SP_CreateCustomerProofPurposes");
                db.AddInParameter(cmdInsertPlanPreferences, "@CPU_ProofUploadId", DbType.Int32, ProofUploadedID);
                db.AddInParameter(cmdInsertPlanPreferences, "@CPP_PurposeCode", DbType.String, PurposeCode);

                db.AddOutParameter(cmdInsertPlanPreferences, "@CPP_ProofPurposeID", DbType.Int32, 0);


                if (db.ExecuteNonQuery(cmdInsertPlanPreferences) != 0)
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

                FunctionInfo.Add("Method", "CustomerDao.cs:CreateCustomersProofPurposes()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bStatus;
        }

        public DataTable GetCustomerUploadedProofPurposes(int proofId)
        {

            Database db;
            DbCommand cmdGetCustomerUploadedProofs;
            DataSet dsGetCustomerUploadedProofPurpose;
            DataTable dtGetCustomerUploadedProofPurpose = new DataTable();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerUploadedProofs = db.GetStoredProcCommand("SP_GetCustomerUploadedProofPurposes");

                db.AddInParameter(cmdGetCustomerUploadedProofs, "@CPP_ProofPurposeId", DbType.Int32, proofId);

                dsGetCustomerUploadedProofPurpose = db.ExecuteDataSet(cmdGetCustomerUploadedProofs);
                if (dsGetCustomerUploadedProofPurpose.Tables.Count != 0)
                    dtGetCustomerUploadedProofPurpose = dsGetCustomerUploadedProofPurpose.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerUploadedProofs(int customerId)");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerUploadedProofPurpose;
        }

        public List<int> CreateISACustomerRequest(CustomerVo customerVo, int custCreateFlag, string priority)
        {
            //bool bReturn = false;
            int customerId;
            int customerUserId;
            int requestId;
            int customerPortfolioId;
            List<int> customerIds = new List<int>();
            Database db;
            DbCommand createCustomerCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerCmd = db.GetStoredProcCommand("SP_CreateISACustomerRequest");

                if (custCreateFlag == 1)
                {
                    db.AddInParameter(createCustomerCmd, "@AR_RMId", DbType.Int32, customerVo.RmId);
                    db.AddInParameter(createCustomerCmd, "@CustId", DbType.Int32, customerVo.CustomerId);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@AR_RMId", DbType.Int32, DBNull.Value);
                    db.AddInParameter(createCustomerCmd, "@CustId", DbType.Int32, customerVo.CustomerId);
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
                db.AddInParameter(createCustomerCmd, "@custCreateFlag", DbType.Int32, custCreateFlag);
                db.AddInParameter(createCustomerCmd, "@CustomerCategoryCode", DbType.Int16, customerVo.CustomerCategoryCode);
                db.AddInParameter(createCustomerCmd, "@C_FirstName", DbType.String, customerVo.FirstName);
                db.AddInParameter(createCustomerCmd, "@C_Mobile1", DbType.String, customerVo.Mobile1);
                db.AddInParameter(createCustomerCmd, "@C_PanNum", DbType.String, customerVo.PANNum);
                db.AddInParameter(createCustomerCmd, "@C_EmailId", DbType.String, customerVo.Email);
                db.AddOutParameter(createCustomerCmd, "@C_CustomerId", DbType.Int32, 10);
                db.AddOutParameter(createCustomerCmd, "@RequestNumber", DbType.Int32, 10);
                db.AddOutParameter(createCustomerCmd, "@U_UserId", DbType.Int32, 10);
                db.AddOutParameter(createCustomerCmd, "@CP_PortfolioId", DbType.Int32, 10);
                db.AddInParameter(createCustomerCmd, "@C_CreatedBy", DbType.Int32, customerVo.UserId);
                db.AddInParameter(createCustomerCmd, "@Priority", DbType.String, priority);
                if (db.ExecuteNonQuery(createCustomerCmd) != 0)
                {

                    customerUserId = int.Parse(db.GetParameterValue(createCustomerCmd, "U_UserId").ToString());
                    customerId = int.Parse(db.GetParameterValue(createCustomerCmd, "C_CustomerId").ToString());
                    customerPortfolioId = int.Parse(db.GetParameterValue(createCustomerCmd, "CP_PortfolioId").ToString());
                    requestId = int.Parse(db.GetParameterValue(createCustomerCmd, "@RequestNumber").ToString());

                    customerIds.Add(customerUserId);
                    customerIds.Add(customerId);
                    customerIds.Add(customerPortfolioId);
                    customerIds.Add(requestId);
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

            return customerIds;
        }


        public void UpdateCustomerISAStageDetails(int requestNumber, string stageStatusCode, string priorityCode, string stepCode, string reasonCode, string comments, string stageToMarkReprocess)
        {
            Database db;
            DbCommand createCustomerCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerCmd = db.GetStoredProcCommand("SP_UpdateCustomerISAStageDetails");
                db.AddInParameter(createCustomerCmd, "@requestNumber", DbType.Int32, requestNumber);
                db.AddInParameter(createCustomerCmd, "@stageStatusCode", DbType.String, stageStatusCode);
                db.AddInParameter(createCustomerCmd, "@priorityCode", DbType.String, priorityCode);
                db.AddInParameter(createCustomerCmd, "@stepCode", DbType.String, stepCode);
                if (reasonCode != "Select" && reasonCode != "")
                {
                    db.AddInParameter(createCustomerCmd, "@reasonCode", DbType.String, reasonCode);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@reasonCode", DbType.String, DBNull.Value);
                }
                if (stageToMarkReprocess != "Select" && stageToMarkReprocess != "")
                {
                    db.AddInParameter(createCustomerCmd, "@stageToMarkReprocess", DbType.String, stageToMarkReprocess);
                }
                else
                {
                    db.AddInParameter(createCustomerCmd, "@stageToMarkReprocess", DbType.String, DBNull.Value);
                }
                db.AddInParameter(createCustomerCmd, "@comments", DbType.String, comments);


                db.ExecuteNonQuery(createCustomerCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public DataSet GetReasonAndStatus(string purpose)
        {
            Database db;
            DbCommand createCustomerCmd;
            DataSet dsGetReasonAndStatus = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerCmd = db.GetStoredProcCommand("SP_GetReasonAndStatus");
                db.AddInParameter(createCustomerCmd, "@purpose", DbType.String, purpose);
                dsGetReasonAndStatus = db.ExecuteDataSet(createCustomerCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetReasonAndStatus;
        }

        public DataSet GetISARequestDetails(int requestId)
        {
            Database db;
            DbCommand createCustomerCmd;
            DataSet dsGetISARequestDetails = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCustomerCmd = db.GetStoredProcCommand("SPROC_GetISARequestDetails");
                db.AddInParameter(createCustomerCmd, "@RequestId", DbType.Int32, requestId);
                dsGetISARequestDetails = db.ExecuteDataSet(createCustomerCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetISARequestDetails;
        }
        public DataTable GetMemberRelationShip()
        {
            Database db;
            DbCommand cmdGetMemberRelationShip;
            DataSet dsRelationship;
            DataTable dtRelationship;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetMemberRelationShip = db.GetStoredProcCommand("SPROC_GetMemberRealationShip");
                dsRelationship = db.ExecuteDataSet(cmdGetMemberRelationShip);
                dtRelationship = dsRelationship.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetMemberRelationShip()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtRelationship;
        }

        public DataTable GetCustomerISAAccounts(int customerId)
        {

            Database db;
            DbCommand cmdGetCustomerISAAccount;
            DataSet dsGetCustomerISAAccount;
            DataTable dtCustomerISAAccountList = new DataTable();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerISAAccount = db.GetStoredProcCommand("SPROC_GetCustomerISAAccounts");

                db.AddInParameter(cmdGetCustomerISAAccount, "@C_CustomerId", DbType.Int32, customerId);

                dsGetCustomerISAAccount = db.ExecuteDataSet(cmdGetCustomerISAAccount);
                if (dsGetCustomerISAAccount.Tables.Count != 0)
                    dtCustomerISAAccountList = dsGetCustomerISAAccount.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerISAAccounts(int customerId)");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerISAAccountList;
        }

        public int CreateCustomerBasicProfileDetails(CustomerVo customerVo, int cretaedBy, string PortfolioTypeCode, string PortfolioName)
        {
            int customerId = 0;
            Database db;
            DbCommand CreateCustomerBasicProfileCmd;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateCustomerBasicProfileCmd = db.GetStoredProcCommand("SP_CreateCustomerBasicProfileDetails");
                if (customerVo.ProcessId != 0)
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@ADUL_ProcessId", DbType.Int32, customerVo.ProcessId);
                else
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@ADUL_ProcessId", DbType.Int32, DBNull.Value);
                if (!string.IsNullOrEmpty(customerVo.CustCode))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_CustCode", DbType.String, customerVo.CustCode);
                else
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_CustCode", DbType.String, DBNull.Value);

                db.AddInParameter(CreateCustomerBasicProfileCmd, "@AR_RMId", DbType.Int32, customerVo.RmId);

                db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_ProfilingDate", DbType.DateTime, customerVo.ProfilingDate);
                db.AddInParameter(CreateCustomerBasicProfileCmd, "@AB_BranchId", DbType.Int32, customerVo.BranchId);
                if (!string.IsNullOrEmpty(customerVo.FirstName))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_FirstName", DbType.String, customerVo.FirstName);
                if (!string.IsNullOrEmpty(customerVo.MiddleName))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_MiddleName", DbType.String, customerVo.MiddleName.Trim());
                if (!string.IsNullOrEmpty(customerVo.LastName))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_LastName", DbType.String, customerVo.LastName.Trim());

                db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_Gender", DbType.String, customerVo.Gender.Trim());

                db.AddInParameter(CreateCustomerBasicProfileCmd, "@XCT_CustomerTypeCode", DbType.String, customerVo.Type);
                db.AddInParameter(CreateCustomerBasicProfileCmd, "@XCST_CustomerSubTypeCode", DbType.String, customerVo.SubType);
                if (!string.IsNullOrEmpty(customerVo.Salutation))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_Salutation", DbType.String, customerVo.Salutation);
                else
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_Salutation", DbType.String, DBNull.Value);

                db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_PANNum", DbType.String, customerVo.PANNum);
                if (!string.IsNullOrEmpty(customerVo.Email))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_Email", DbType.String, customerVo.Email);
                else
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_Email", DbType.String, DBNull.Value);
                if (!string.IsNullOrEmpty(customerVo.CompanyName))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_CompanyName", DbType.String, customerVo.CompanyName);
                else
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_CompanyName", DbType.String, DBNull.Value);

                if (!string.IsNullOrEmpty(customerVo.CompanyWebsite))
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_CompanyWebsite", DbType.String, customerVo.CompanyWebsite);
                else
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_CompanyWebsite", DbType.String, DBNull.Value);

                db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_CreatedBy", DbType.Int16, cretaedBy);
                db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_ModifiedBy", DbType.Int16, cretaedBy);

                db.AddInParameter(CreateCustomerBasicProfileCmd, "@U_UserType", DbType.String, customerVo.UserType);
                db.AddInParameter(CreateCustomerBasicProfileCmd, "@XPT_PortfolioTypeCode", DbType.String, PortfolioTypeCode);
                db.AddInParameter(CreateCustomerBasicProfileCmd, "@CP_PortfolioName", DbType.String, PortfolioName);

                //db.AddInParameter(CreateCustomerBasicProfileCmd, "@C_DummyPAN", DbType.Byte, customerVo.DummyPAN);
                if (customerVo.CustomerClassificationID != 0)
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@ACC_CustomerClassificationId", DbType.Int16, customerVo.CustomerClassificationID);
                else
                    db.AddInParameter(CreateCustomerBasicProfileCmd, "@ACC_CustomerClassificationId", DbType.Int16, DBNull.Value);

                db.AddOutParameter(CreateCustomerBasicProfileCmd, "@U_UserId", DbType.Int32, 10000);
                db.AddOutParameter(CreateCustomerBasicProfileCmd, "@C_CustomerId", DbType.Int32, 10000);
                db.AddOutParameter(CreateCustomerBasicProfileCmd, "@CP_PortfolioId", DbType.Int32, 10000);

                if (db.ExecuteNonQuery(CreateCustomerBasicProfileCmd) != 0)

                    customerId = int.Parse(db.GetParameterValue(CreateCustomerBasicProfileCmd, "@C_CustomerId").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:CreateCustomerBasicProfileDetails()");


                object[] objects = new object[4];
                objects[0] = customerVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerId;

        }

        public bool UpdateMemberRelation(int AssociationId, string relationCode, bool isrealInvestor, int iskyc, DateTime DOB, string txtPan)
        {
            bool isEdited = false;
            Database db;
            DbCommand updateMemberRelationCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMemberRelationCmd = db.GetStoredProcCommand("SP_UpdateMemberRelationShip");
                db.AddInParameter(updateMemberRelationCmd, "@associationId", DbType.Int32, AssociationId);
                db.AddInParameter(updateMemberRelationCmd, "@relationCode", DbType.String, relationCode);
                db.AddInParameter(updateMemberRelationCmd, "@IsRealInvestor", DbType.Boolean, isrealInvestor ? 1 : 0);
                db.AddInParameter(updateMemberRelationCmd, "@Iskyc", DbType.Int16, iskyc);
                if (DOB != DateTime.MinValue)
                {
                    db.AddInParameter(updateMemberRelationCmd, "@DOB", DbType.DateTime, DOB);
                }
                else
                {
                    db.AddInParameter(updateMemberRelationCmd, "@DOB", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(updateMemberRelationCmd, "@PANno", DbType.String, txtPan);

                if (db.ExecuteNonQuery(updateMemberRelationCmd) != 0)
                    isEdited = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isEdited;
        }
        public DataSet GetExceptionReportMismatchDetails(string userType, int adviserId, int rmId, int CustomerId, int branchheadId, int branchId, int All, int isIndividualOrGroup, string Explist, string Exptype, int Mismatch)
        {
            DataSet dsGetExceptionReportMismatchDetails = new DataSet();
            Database db;
            DbCommand GetExceptionReportMismatchDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetExceptionReportMismatchDetailsCmd = db.GetStoredProcCommand("GetExceptionReportMismatchDetails");
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@branchHeadId", DbType.Int32, branchheadId);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@all", DbType.Int32, All);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@isIndividualOrGroup", DbType.Int16, isIndividualOrGroup);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@ExpListCode", DbType.String, Explist);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@ExptypeCode", DbType.String, Exptype);
                db.AddInParameter(GetExceptionReportMismatchDetailsCmd, "@Mismatch", DbType.Int16, Mismatch);

                dsGetExceptionReportMismatchDetails = db.ExecuteDataSet(GetExceptionReportMismatchDetailsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetAllSystematicMISData()");
                //object[] objects = new object[16];
                //objects[0] = UserType;
                //objects[1] = AdviserId;
                //objects[2] = RmId;
                //objects[3] = CustomerId;
                //objects[4] = BranchHeadId;
                //objects[5] = BranchId;
                //objects[6] = All;
                //objects[7] = Category;
                //objects[8] = SysType;
                //objects[9] = AmcCode;
                //objects[10] = SchemePlanCode;
                //objects[11] = StartDate;
                //objects[12] = EndDate;
                //objects[13] = dtFrom;
                //objects[14] = dtTo;

                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionReportMismatchDetails;
        }
        public DataSet GetExceptionReportDetails(string userType, int adviserId, int rmId, int CustomerId, int branchheadId, int branchId, int All, int isIndividualOrGroup, string Explist, string Exptype, int Mismatch)
        {
            DataSet dsGetExceptionReportDetails = new DataSet();
            Database db;
            DbCommand GetExceptionReportDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetExceptionReportDetailsCmd = db.GetStoredProcCommand("GetExceptionReportDetails");
                db.AddInParameter(GetExceptionReportDetailsCmd, "@UserType", DbType.String, userType);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@branchHeadId", DbType.Int32, branchheadId);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@BranchId", DbType.Int32, branchId);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@all", DbType.Int32, All);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@isIndividualOrGroup", DbType.Int16, isIndividualOrGroup);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@ExpListCode", DbType.String, Explist);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@ExptypeCode", DbType.String, Exptype);
                db.AddInParameter(GetExceptionReportDetailsCmd, "@Mismatch", DbType.Int16, Mismatch);

                dsGetExceptionReportDetails = db.ExecuteDataSet(GetExceptionReportDetailsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "SystematicSetupDao.cs:GetAllSystematicMISData()");
                //object[] objects = new object[16];
                //objects[0] = UserType;
                //objects[1] = AdviserId;
                //objects[2] = RmId;
                //objects[3] = CustomerId;
                //objects[4] = BranchHeadId;
                //objects[5] = BranchId;
                //objects[6] = All;
                //objects[7] = Category;
                //objects[8] = SysType;
                //objects[9] = AmcCode;
                //objects[10] = SchemePlanCode;
                //objects[11] = StartDate;
                //objects[12] = EndDate;
                //objects[13] = dtFrom;
                //objects[14] = dtTo;

                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetExceptionReportDetails;
        }
        public bool EditData(string ProData, string FolioData, string FolioNumber, int CustomerId, string Explist)
        {
            bool bResult = false;
            //string Exp;
            Database db;
            DbCommand EditDataCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                EditDataCmd = db.GetStoredProcCommand("UpDateProfile");
                db.AddInParameter(EditDataCmd, "@ProfileData", DbType.String, ProData);
                db.AddInParameter(EditDataCmd, "@customerId", DbType.Int32, CustomerId);
                db.AddInParameter(EditDataCmd, "@folioData", DbType.String, FolioData);
                db.AddInParameter(EditDataCmd, "@folionum", DbType.String, FolioNumber);

                if (Explist == "Pan")
                    Explist = "C_PANNum";
                else if (Explist == "Email")
                    Explist = "C_Email";
                else if (Explist == "Mob(r)")
                    Explist = "C_ResPhoneNum";
                else if (Explist == "Dob")
                    Explist = "C_DOB";
                else if (Explist == "TS")
                    Explist = "XCST_CustomerSubTypeCode";
                //else if (Explist == "Add")
                //    Exp = "C_Adr1Line1";
                else if (Explist == "Mob(o)")
                    Explist = "C_OfcPhoneNum";
                db.AddInParameter(EditDataCmd, "@Explist", DbType.String, Explist);
                if (db.ExecuteNonQuery(EditDataCmd) != 0)
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
                FunctionInfo.Add("Method", "CustomerDao.cs:DeleteMappedSchemeDetails()");
                //object[] objects = new object[2];
                //objects[0] = schemePlanCode;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
        public Boolean GetFixedMapped(string explist)
        {
            bool Isfixed = false;
            DataSet result;
            object value;
            Database db;
            DbCommand GetFixedMappedCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetFixedMappedCmd = db.GetStoredProcCommand("GetFixedMapped");
                db.AddInParameter(GetFixedMappedCmd, "@explist", DbType.String, explist);
                result = (db.ExecuteDataSet(GetFixedMappedCmd));
                value = result.Tables[0].Rows[0][0];
                if (int.Parse(value.ToString()) == 1)
                    Isfixed = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBo.cs:GetSchemeDetails()");
                //object[] objects = new object[1];
                //objects[0] = customerId;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Isfixed;
        }
        public DataTable GetISAHoldings(int accountId)
        {
            Database db;
            DbCommand cmdGetISAHoldings;
            DataTable dtGetISAHoldings;
            DataSet dsGetISAHoldings = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetISAHoldings = db.GetStoredProcCommand("SPROC_GetISAHolding");
                db.AddInParameter(cmdGetISAHoldings, "@accountId", DbType.Int32, accountId);
                dsGetISAHoldings = db.ExecuteDataSet(cmdGetISAHoldings);
                dtGetISAHoldings = dsGetISAHoldings.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:GetISAHoldings(accountId)");
                object[] objects = new object[1];
                objects[0] = accountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetISAHoldings;
        }
        public DataTable GetholdersName(int ISANumber)
        {
            Database db;
            DbCommand cmdGetholdersName;
            DataTable dtGetholdersName;
            DataSet dsGetholdersName = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetholdersName = db.GetStoredProcCommand("GetHoldersName");
                db.AddInParameter(cmdGetholdersName, "@AccountNumber", DbType.Int32, ISANumber);
                dsGetholdersName = db.ExecuteDataSet(cmdGetholdersName);
                dtGetholdersName = dsGetholdersName.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                //FunctionInfo.Add("Method", "CustomerDao.cs:GetISAHoldings(accountId)");
                //object[] objects = new object[1];
                //objects[0] = accountId;
                //FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetholdersName;
        }

        public bool CheckIfISAAccountGenerated(int requestNumber)
        {
            Database db;
            bool result = false;
            DbCommand cmdCheckIfISAAccount;
            DataSet dsCheckIfISAAccount = new DataSet();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdCheckIfISAAccount = db.GetStoredProcCommand("SP_CheckIfISAAccountGenerated");
                db.AddInParameter(cmdCheckIfISAAccount, "@requestNumber", DbType.Int32, requestNumber);
                dsCheckIfISAAccount = db.ExecuteDataSet(cmdCheckIfISAAccount);
                if (dsCheckIfISAAccount.Tables[0].Rows.Count > 0)
                {
                    result = true;

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return result;
        }

        public DataTable GetBMParentCustomers(string prefixText, int bmId, int parentId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetBMParentCustomers");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@AB_BranchId", DbType.Int32, bmId);
                db.AddInParameter(cmdGetCustomerNames, "@selectedParentId", DbType.Int32, parentId);
                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dtCustomerNames;
        }

        public DataTable GetAdviserAllCustomerForAssociations(string prefixText, int adviserId, int parentId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetAdviserAllCustomerForAssociations");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCustomerNames, "@selectedParentId", DbType.Int32, parentId);

                dsCustomerNames = db.ExecuteDataSet(cmdGetCustomerNames);
                dtCustomerNames = dsCustomerNames.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

            return dtCustomerNames;
        }

        public DataTable GetAssociateCustomerName(string prefixText, int AgentId)
        {

            Database db;
            DbCommand cmdGetCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustomerNames = db.GetStoredProcCommand("SP_GetIndividualCustomerNameforAssociate");
                db.AddInParameter(cmdGetCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustomerNames, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
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
        public DataTable GetAssociateGroupCustomerName(string prefixText, int AgentId)
        {

            Database db;
            DbCommand cmdGetGroupCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetGroupCustomerNames = db.GetStoredProcCommand("SP_GetAssociateParentCustomerNames");
                db.AddInParameter(cmdGetGroupCustomerNames, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetGroupCustomerNames, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
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
        public DataTable GetAgentId(int adviserid, int agentid)
        {

            Database db;
            DbCommand cmdGetGroupCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetGroupCustomerNames = db.GetStoredProcCommand("SP_GETagentcode");
                db.AddInParameter(cmdGetGroupCustomerNames, "@adviserid", DbType.Int32, adviserid);
                db.AddInParameter(cmdGetGroupCustomerNames, "@agentId", DbType.Int32, agentid);

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

                objects[0] = adviserid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }
        public int ChkAssociateCode(int adviserid, string agentcode, string validateAgentCode, string userType)
        {

            Database db;
            DbCommand cmdGetGroupCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;
            int CountRecord = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetGroupCustomerNames = db.GetStoredProcCommand("ChkAssociateCode");
                db.AddInParameter(cmdGetGroupCustomerNames, "@adviserid", DbType.Int32, adviserid);
                db.AddInParameter(cmdGetGroupCustomerNames, "@agentcode", DbType.String, agentcode);
                db.AddInParameter(cmdGetGroupCustomerNames, "@validateAgentCode", DbType.String, validateAgentCode);
                db.AddInParameter(cmdGetGroupCustomerNames, "@userType", DbType.String, userType);

                dsCustomerNames = db.ExecuteDataSet(cmdGetGroupCustomerNames);
                CountRecord = Convert.ToInt32(dsCustomerNames.Tables[0].Rows[0][0]);



            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerDao.cs:ChkAssociateCode()");
                object[] objects = new object[1];

                objects[0] = adviserid;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return CountRecord;
        }

        public DataTable GetAssociateName(int adviserid, string agentcode)
        {

            Database db;
            DbCommand cmdGetGroupCustomerNames;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetGroupCustomerNames = db.GetStoredProcCommand("GetAssociateName");
                db.AddInParameter(cmdGetGroupCustomerNames, "@adviserid", DbType.Int32, adviserid);
                db.AddInParameter(cmdGetGroupCustomerNames, "@agentcode", DbType.String, agentcode);

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
                objects[0] = adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtCustomerNames;
        }

        public DataTable GetSubBrokerName(int agentId)
        {

            Database db;
            DbCommand cmdGetGroupSubBrokerName;
            DataSet dsSubBrokerName;
            DataTable dtSubBrokerName;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetGroupSubBrokerName = db.GetStoredProcCommand("GetAssociateNames");
                db.AddInParameter(cmdGetGroupSubBrokerName, "@agentId", DbType.Int32, agentId);
                dsSubBrokerName = db.ExecuteDataSet(cmdGetGroupSubBrokerName);
                dtSubBrokerName = dsSubBrokerName.Tables[0];
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
                objects[0] = agentId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtSubBrokerName;
        }


        //public DataTable GetAssociateName(int adviserid, string agentcode)
        //{

        //    Database db;
        //    DbCommand cmdGetGroupCustomerNames;
        //    DataSet dsCustomerNames;
        //    DataTable dtCustomerNames;

        //    try
        //    {
        //        db = DatabaseFactory.CreateDatabase("wealtherp");
        //        //To retreive data from the table 
        //        cmdGetGroupCustomerNames = db.GetStoredProcCommand("GetAssociateName");
        //        db.AddInParameter(cmdGetGroupCustomerNames, "@adviserid", DbType.Int32, adviserid);
        //        db.AddInParameter(cmdGetGroupCustomerNames, "@agentcode", DbType.String, agentcode);

        //        dsCustomerNames = db.ExecuteDataSet(cmdGetGroupCustomerNames);
        //        dtCustomerNames = dsCustomerNames.Tables[0];
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerDao.cs:GetAdviserCustomerName()");


        //        object[] objects = new object[1];

        //        objects[0] = adviserid;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return dtCustomerNames;
        //}


        public DataTable GetAgentCodeAssociateDetailsForAssociates(string prefixText, string agentcode, int adviserId)
        {

            Database db;
            DbCommand cmdGetAgentCodeAssociateDetails;
            DataSet dsCustomerNames;
            DataTable dtCustomerNames;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetAgentCodeAssociateDetails = db.GetStoredProcCommand("GetAgentCodeAssociateDetailsForAssociates");
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@agentcode", DbType.String, agentcode);
                db.AddInParameter(cmdGetAgentCodeAssociateDetails, "@adviserId", DbType.Int32, adviserId);

                dsCustomerNames = db.ExecuteDataSet(cmdGetAgentCodeAssociateDetails);
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
        public bool InsertDataTranslateMappingDetalis(string TransactionHead, string TransactionDescription, string TransactionType, string TransactionTypeFlag, string TransactionClassificationCode)
        {
            bool isInserted = false;
            Database db;
            DbCommand cmdInsertDataTranslateMappingDetalis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertDataTranslateMappingDetalis = db.GetStoredProcCommand("SP_InsertKarvyDataTranslationMappingDetails");
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionHead", DbType.String, TransactionHead);
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionDescription", DbType.String, TransactionDescription);
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionType", DbType.String, TransactionType);
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionTypeFlag", DbType.String, TransactionTypeFlag);
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, TransactionClassificationCode);
                db.ExecuteNonQuery(cmdInsertDataTranslateMappingDetalis);
                isInserted = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isInserted;
        }
        public bool EditDataTranslateMappingDetalis(string prevTransactionHead, string prevTransactionDescription, string prevTransactionType, string prevTransactionTypeFlag, string TransactionHead, string TransactionDescription, string TransactionType, string TransactionTypeFlag, string TransactionClassificationCode)
        {
            bool isUpdated = false;
            Database db;
            DbCommand cmdInsertDataTranslateMappingDetalis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertDataTranslateMappingDetalis = db.GetStoredProcCommand("SP_EditKarvyDataTranslationMappingDetails");
                if (TransactionHead != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionHead", DbType.String, prevTransactionHead);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionHead", DbType.String, DBNull.Value);
                if (TransactionDescription != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionDescription", DbType.String, prevTransactionDescription);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionDescription", DbType.String, DBNull.Value);
                if (TransactionType != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionType", DbType.String, prevTransactionType);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionType", DbType.String, DBNull.Value);
                if (TransactionTypeFlag != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionTypeFlag", DbType.String, prevTransactionTypeFlag);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_PrevTransactionTypeFlag", DbType.String, DBNull.Value);
                if (TransactionHead != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionHead", DbType.String, TransactionHead);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionHead", DbType.String, DBNull.Value);
                if (TransactionDescription != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionDescription", DbType.String, TransactionDescription);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionDescription", DbType.String, DBNull.Value);
                if (TransactionType != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionType", DbType.String, TransactionType);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionType", DbType.String, DBNull.Value);
                if (TransactionTypeFlag != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionTypeFlag", DbType.String, TransactionTypeFlag);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WKDTM_TransactionTypeFlag", DbType.String, DBNull.Value);
                if (TransactionClassificationCode != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, TransactionClassificationCode);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(cmdInsertDataTranslateMappingDetalis);
                isUpdated = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isUpdated;
        }
        public bool InsertCamsDataTranslateMappingDetalis(string TransactionType, string TransactionDescription, string TransactionClassificationCode)
        {
            bool isInserted = false;
            Database db;
            DbCommand cmdInsertDataTranslateMappingDetalis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertDataTranslateMappingDetalis = db.GetStoredProcCommand("SP_InsertCamsDataTranslationMappingDetails");
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_Transaction_type", DbType.String, TransactionType);
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_TransactionNature", DbType.String, TransactionDescription);
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, TransactionClassificationCode);
                db.ExecuteNonQuery(cmdInsertDataTranslateMappingDetalis);
                isInserted = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isInserted;
        }
        public bool EditCamsDataTranslateMappingDetalis(string prevTransactionType, string prevTransactionDescription, string TransactionType, string TransactionDescription, string TransactionClassificationCode)
        {
            bool isUpdated = false;
            Database db;
            DbCommand cmdInsertDataTranslateMappingDetalis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertDataTranslateMappingDetalis = db.GetStoredProcCommand("SP_EditCamsDataTranslationMappingDetails");
                if (TransactionType != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_PrevTransaction_type", DbType.String, prevTransactionType);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_PrevTransaction_type", DbType.String, DBNull.Value);
                if (TransactionDescription != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_PrevTransactionNature", DbType.String, prevTransactionDescription);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_PrevTransactionNature", DbType.String, DBNull.Value);
                if (TransactionType != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_Transaction_type", DbType.String, TransactionType);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_Transaction_type", DbType.String, DBNull.Value);
                if (TransactionDescription != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_TransactionNature", DbType.String, TransactionDescription);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WCDTM_TransactionNature", DbType.String, DBNull.Value);
                if (TransactionClassificationCode != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, TransactionClassificationCode);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(cmdInsertDataTranslateMappingDetalis);
                isUpdated = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isUpdated;
        }
        public bool InsertTempletonDataTranslateMappingDetalis(string TransactionType, string TransactionClassificationCode)
        {
            bool isInserted = false;
            Database db;
            DbCommand cmdInsertDataTranslateMappingDetalis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertDataTranslateMappingDetalis = db.GetStoredProcCommand("SP_InsertTempletonDataTranslationMappingDetails");
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WTTTDTM_TR_TYPE", DbType.String, TransactionType);
                db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, TransactionClassificationCode);
                db.ExecuteNonQuery(cmdInsertDataTranslateMappingDetalis);
                isInserted = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isInserted;
        }
        public bool EditTempletonDataTranslateMappingDetalis(string prevTransactionType, string TransactionType, string TransactionClassificationCode)
        {
            bool isUpdated = false;
            Database db;
            DbCommand cmdInsertDataTranslateMappingDetalis;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdInsertDataTranslateMappingDetalis = db.GetStoredProcCommand("SP_EditTempletonDataTranslationMappingDetails");
                if (TransactionType != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WTTTDTM_PrevTR_TYPE", DbType.String, prevTransactionType);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WTTTDTM_PrevTR_TYPE", DbType.String, DBNull.Value);
                if (TransactionType != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WTTTDTM_TR_TYPE", DbType.String, TransactionType);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WTTTDTM_TR_TYPE", DbType.String, DBNull.Value);
                if (TransactionClassificationCode != null)
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, TransactionClassificationCode);
                else
                    db.AddInParameter(cmdInsertDataTranslateMappingDetalis, "@WMTT_TransactionClassificationCode", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(cmdInsertDataTranslateMappingDetalis);
                isUpdated = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return isUpdated;
        }

        public DataSet GetCustomerProfileSetupLookupData()
        {
            Database db;
            DbCommand cmdCustomerProfileSetupLookupData;
            DataSet dsCustomerProfileSetupLookupData;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdCustomerProfileSetupLookupData = db.GetStoredProcCommand("SPROC_ONL_GetCustomerProfileLookup");
                dsCustomerProfileSetupLookupData = db.ExecuteDataSet(cmdCustomerProfileSetupLookupData);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProfileSetupLookupData()");
                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsCustomerProfileSetupLookupData;


        }
        public int ToCheckSchemeisonline(int schemeplanecode, int Isonline, string sourcecode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdToCheckSchemeisonline;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdToCheckSchemeisonline = db.GetStoredProcCommand("SPROC_ToValidateIsonline");
                db.AddInParameter(cmdToCheckSchemeisonline, "@schemeplancode", DbType.Int32, schemeplanecode);
                db.AddInParameter(cmdToCheckSchemeisonline, "@Isonline", DbType.Int32, Isonline);
                //db.AddInParameter(cmdToCheckSchemeisonline, "@sourcecode", DbType.String, sourcecode);
                db.AddOutParameter(cmdToCheckSchemeisonline, "@count", DbType.Int32, 0);
                //count = Convert.ToInt32(db.ExecuteScalar(cmdToCheckSchemeisonline).ToString());

                //ds = db.ExecuteDataSet(cmdToCheckSchemeisonline);
                if (db.ExecuteNonQuery(cmdToCheckSchemeisonline) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdToCheckSchemeisonline, "count").ToString());
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
                FunctionInfo.Add("Method", "AssociateDAO.cs:ToCheckSchemeisonline()");
                object[] objects = new object[3];
                objects[0] = schemeplanecode;
                objects[1] = sourcecode;
                objects[2] = Isonline;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }
        public DataTable GetCustCode(string prefixText, int rmId)
        {

            Database db;
            DbCommand cmdGetCustCode;
            DataSet dsGetCustCode;
            DataTable dtGetCustCode;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetCustCode = db.GetStoredProcCommand("SP_GetCustomercode");
                db.AddInParameter(cmdGetCustCode, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetCustCode, "@AR_RMId", DbType.Int32, rmId);
                dsGetCustCode = db.ExecuteDataSet(cmdGetCustCode);
                dtGetCustCode = dsGetCustCode.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustCode()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustCode;
        }
        public DataTable GetRequestId(string prefixText, int register, int adviserId)
        {

            Database db;
            DbCommand cmdGetRequestId;
            DataSet dsGetRequestId;
            DataTable dtGetRequestId;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetRequestId = db.GetStoredProcCommand("SP_GetRequestId");
                db.AddInParameter(cmdGetRequestId, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetRequestId, "@Registration", DbType.Int32, register);
                db.AddInParameter(cmdGetRequestId, "@A_AdviserId", DbType.Int32, adviserId);
                dsGetRequestId = db.ExecuteDataSet(cmdGetRequestId);
                dtGetRequestId = dsGetRequestId.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustCode()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetRequestId;
        }
        public DataTable GetSchemePlanName(string prefixText)
        {

            Database db;
            DbCommand cmdGetSchemePlanName;
            DataSet dsGetSchemePlanName;
            DataTable dtGetSchemePlanName;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetSchemePlanName = db.GetStoredProcCommand("SPROC_GetProductAmcSchemeList");
                db.AddInParameter(cmdGetSchemePlanName, "@prefixText", DbType.String, prefixText);
                dsGetSchemePlanName = db.ExecuteDataSet(cmdGetSchemePlanName);
                dtGetSchemePlanName = dsGetSchemePlanName.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustCode()");


                object[] objects = new object[1];

                objects[0] = prefixText;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetSchemePlanName;
        }
        public int CheckStaffCode(string prefixText)
        {
            Database db;
            DataSet dsCheckStaffCode;
            DbCommand cmdCheckStaffCode;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdCheckStaffCode = db.GetStoredProcCommand("SPROC_CheckDuplicateStaffcode");
                db.AddInParameter(cmdCheckStaffCode, "@StaffCode", DbType.String, prefixText);
                db.AddOutParameter(cmdCheckStaffCode, "@StaffCodeDupliate", DbType.Int32, 1000);
                dsCheckStaffCode = db.ExecuteDataSet(cmdCheckStaffCode);

                if (db.ExecuteScalar(cmdCheckStaffCode) != null)
                    count = Convert.ToInt32(db.ExecuteScalar(cmdCheckStaffCode).ToString());
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
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }
        public DataSet GetCustomerProfileAuditDetails(int customerId, DateTime fromModificationDate, DateTime toModificationDate, int advisorId, string TypeofAudit)
        {
            Database db;
            DbCommand cmdCustomerProfileAudit;
            DataSet dsCustomerProfileAudit;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdCustomerProfileAudit = db.GetStoredProcCommand("SPROC_ONL_GetCustomerProfileAuditDetails");
                db.AddInParameter(cmdCustomerProfileAudit, "@CustomerId", DbType.Int32, customerId);
                db.AddInParameter(cmdCustomerProfileAudit, "@FromModificationDate", DbType.DateTime, fromModificationDate);
                db.AddInParameter(cmdCustomerProfileAudit, "@ToModificationDate", DbType.DateTime, toModificationDate);
                db.AddInParameter(cmdCustomerProfileAudit, "@TypeofAudit", DbType.String, TypeofAudit);
                db.AddInParameter(cmdCustomerProfileAudit, "@AdvisorId", DbType.Int32, advisorId);
                dsCustomerProfileAudit = db.ExecuteDataSet(cmdCustomerProfileAudit);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProfileAuditDetails()");
                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = fromModificationDate;
                objects[2] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsCustomerProfileAudit;

        }
        public DataSet GetSchemePlanAuditDetails(int SchemePlancode, DateTime fromModificationDate, DateTime toModificationDate)
        {
            Database db;
            DbCommand cmdGetSchemePlanAuditDetails;
            DataSet dsGetSchemePlanAuditDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetSchemePlanAuditDetails = db.GetStoredProcCommand("SPROC_SchemePlan_Audit");
                db.AddInParameter(cmdGetSchemePlanAuditDetails, "@schemePlanCode", DbType.Int32, SchemePlancode);
                db.AddInParameter(cmdGetSchemePlanAuditDetails, "@FromModificationDate", DbType.DateTime, fromModificationDate);
                db.AddInParameter(cmdGetSchemePlanAuditDetails, "@ToModificationDate", DbType.DateTime, toModificationDate);
                dsGetSchemePlanAuditDetails = db.ExecuteDataSet(cmdGetSchemePlanAuditDetails);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetSchemePlanAuditDetails()");
                object[] objects = new object[3];
                objects[0] = SchemePlancode;
                objects[1] = fromModificationDate;
                objects[2] = toModificationDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetSchemePlanAuditDetails;

        }
        public DataSet GetStaffAuditDetail(int rmId, DateTime fromModificationDate, DateTime toModificationDate, int advisorId)
        {
            Database db;
            DbCommand cmdsStaffProfileAudit;
            DataSet dsStaffProfileAudit;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdsStaffProfileAudit = db.GetStoredProcCommand("SPROC_Staff_Audit");
                db.AddInParameter(cmdsStaffProfileAudit, "@RMId", DbType.Int32, rmId);
                db.AddInParameter(cmdsStaffProfileAudit, "@FromModificationDate", DbType.DateTime, fromModificationDate);
                db.AddInParameter(cmdsStaffProfileAudit, "@ToModificationDate", DbType.DateTime, toModificationDate);
                db.AddInParameter(cmdsStaffProfileAudit, "@AdvisorId", DbType.Int32, advisorId);
                dsStaffProfileAudit = db.ExecuteDataSet(cmdsStaffProfileAudit);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProfileAuditDetails()");
                object[] objects = new object[3];
                objects[0] = rmId;
                objects[1] = fromModificationDate;
                objects[2] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsStaffProfileAudit;

        }
        public DataSet GetAssociateAuditDetail(int AssociateId, DateTime fromModificationDate, DateTime toModificationDate, int advisorId)
        {
            Database db;
            DbCommand cmdsAssociateAudit;
            DataSet dsAssociateAudit;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdsAssociateAudit = db.GetStoredProcCommand("SPROC_GetAssociateAudit");
                db.AddInParameter(cmdsAssociateAudit, "@AdviserAssociateId", DbType.Int32, AssociateId);
                db.AddInParameter(cmdsAssociateAudit, "@FromModificationDate", DbType.DateTime, fromModificationDate);
                db.AddInParameter(cmdsAssociateAudit, "@ToModificationDate", DbType.DateTime, toModificationDate);
                db.AddInParameter(cmdsAssociateAudit, "@AdvisorId", DbType.Int32, advisorId);
                dsAssociateAudit = db.ExecuteDataSet(cmdsAssociateAudit);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAssociateAuditDetail()");
                object[] objects = new object[3];
                objects[0] = AssociateId;
                objects[1] = fromModificationDate;
                objects[2] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsAssociateAudit;

        }
        public DataSet GetSystematicAuditDetails(int systematicSetupId, DateTime fromModificationDate, DateTime toModificationDate, int advisorId)
        {
            Database db;
            DbCommand cmdsSystematicAudit;
            DataSet dsSystematicAudit;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdsSystematicAudit = db.GetStoredProcCommand("SPROC_GetSystematicAudit");
                db.AddInParameter(cmdsSystematicAudit, "@SystematicSetupId", DbType.Int32, systematicSetupId);
                db.AddInParameter(cmdsSystematicAudit, "@FromModificationDate", DbType.DateTime, fromModificationDate);
                db.AddInParameter(cmdsSystematicAudit, "@ToModificationDate", DbType.DateTime, toModificationDate);
                db.AddInParameter(cmdsSystematicAudit, "@AdvisorId", DbType.Int32, advisorId);
                dsSystematicAudit = db.ExecuteDataSet(cmdsSystematicAudit);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetSystematicAuditDetails()");
                object[] objects = new object[3];
                objects[0] = systematicSetupId;
                objects[1] = fromModificationDate;
                objects[2] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsSystematicAudit;

        }
        public DataTable GetRMStaffList(string prefixText, int herarchyId, int adviserId)
        {

            Database db;
            DbCommand cmdGetRMStaffList;
            DataSet dsGetRMStaffList;
            DataTable dtGetRMStaffList;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRMStaffList = db.GetStoredProcCommand("SPROC_GetRMandStaffList");
                db.AddInParameter(cmdGetRMStaffList, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetRMStaffList, "@herarchyId", DbType.Int32, herarchyId);
                db.AddInParameter(cmdGetRMStaffList, "@adviserId", DbType.Int32, adviserId);
                dsGetRMStaffList = db.ExecuteDataSet(cmdGetRMStaffList);
                dtGetRMStaffList = dsGetRMStaffList.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetRMStaffList;
        }

        public DataTable GetSystematicId(string prefixText, int Adviserid)
        {

            Database db;
            DbCommand cmdGetSystematicId;
            DataSet dsGetSystematicId;
            DataTable dtGetSystematicId;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSystematicId = db.GetStoredProcCommand("SP_GetSystematicId");
                db.AddInParameter(cmdGetSystematicId, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetSystematicId, "@A_AdviserId", DbType.Int32, Adviserid);
                dsGetSystematicId = db.ExecuteDataSet(cmdGetSystematicId);
                dtGetSystematicId = dsGetSystematicId.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetAssociateAuditDetail()");
                object[] objects = new object[3];
                objects[0] = prefixText;
                objects[1] = Adviserid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetSystematicId;
        }
        public DataTable GetASBABankLocation(string prefixText)
        {

            Database db;
            DbCommand cmdGetASBABankLocation;
            DataSet dsGetASBABankLocation;
            DataTable dtGetASBABankLocation;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdGetASBABankLocation = db.GetStoredProcCommand("SPROC_GetASBAAllLocation");
                db.AddInParameter(cmdGetASBABankLocation, "@prefixText", DbType.String, prefixText);
                dsGetASBABankLocation = db.ExecuteDataSet(cmdGetASBABankLocation);
                dtGetASBABankLocation = dsGetASBABankLocation.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetASBABankLocation;
        }
        public DataSet GetNcdIssueSetUp(int issueId, DateTime fromModificationDate, DateTime toModificationDate, int advisorId, string TypeofAudit, string category, string product)
        {
            Database db;
            DbCommand cmdNdcissueListAudit;
            DataSet dsNcdIssueListAudit;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //To retreive data from the table 
                cmdNdcissueListAudit = db.GetStoredProcCommand("SPROC_ONL_GetNcdIssueSetUpAuditDetails");
                db.AddInParameter(cmdNdcissueListAudit, "@IssueID", DbType.Int32, issueId);
                db.AddInParameter(cmdNdcissueListAudit, "@FromModificationDate", DbType.DateTime, fromModificationDate);
                db.AddInParameter(cmdNdcissueListAudit, "@ToModificationDate", DbType.DateTime, toModificationDate);
                db.AddInParameter(cmdNdcissueListAudit, "@TypeofAudit", DbType.String, TypeofAudit);
                db.AddInParameter(cmdNdcissueListAudit, "@AdvisorId", DbType.Int32, advisorId);
                db.AddInParameter(cmdNdcissueListAudit, "@category", DbType.String, category);
                db.AddInParameter(cmdNdcissueListAudit, "@product", DbType.String, product);
                dsNcdIssueListAudit = db.ExecuteDataSet(cmdNdcissueListAudit);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerDao.cs:GetCustomerProfileAuditDetails()");
                object[] objects = new object[3];
                objects[0] = issueId;
                objects[1] = fromModificationDate;
                objects[2] = advisorId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsNcdIssueListAudit;

        }
        public DataTable GetNcdIssuenameDetails(string prefixText,string category, int adviserId)
        {

            Database db;
            DbCommand cmdGetNcdIssuenameDetails;
            DataSet dsGetNcdIssuenameDetails;
            DataTable dtGetNcdIssuenameDetails;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetNcdIssuenameDetails = db.GetStoredProcCommand("SPROC_GetNcdIssueName");
                db.AddInParameter(cmdGetNcdIssuenameDetails, "@prefixText", DbType.String, prefixText);
                db.AddInParameter(cmdGetNcdIssuenameDetails, "@Category", DbType.String, category);
                db.AddInParameter(cmdGetNcdIssuenameDetails, "@adviserId", DbType.Int32, adviserId);
                dsGetNcdIssuenameDetails = db.ExecuteDataSet(cmdGetNcdIssuenameDetails);
                dtGetNcdIssuenameDetails = dsGetNcdIssuenameDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetNcdIssuenameDetails;
        }
        public DataTable GetDummyPanCustomer(string pan, string dob, string email, string moblile, int advisorId)
        {

            Database db;
            DbCommand cmdGetDummyPanCustomer;
            DataSet dsGetDummyPanCustomer;
            DataTable dtGetDummyPanCustomer;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetDummyPanCustomer = db.GetStoredProcCommand("SPROC_GetDummyPanCustomer");
                db.AddInParameter(cmdGetDummyPanCustomer, "@pan", DbType.String, pan);
                db.AddInParameter(cmdGetDummyPanCustomer, "@dob", DbType.String, dob);
                db.AddInParameter(cmdGetDummyPanCustomer, "@email", DbType.String, dob);
                db.AddInParameter(cmdGetDummyPanCustomer, "@moblile", DbType.String, dob);
                db.AddInParameter(cmdGetDummyPanCustomer, "@advisorId", DbType.String, advisorId);


                dsGetDummyPanCustomer = db.ExecuteDataSet(cmdGetDummyPanCustomer);
                dtGetDummyPanCustomer = dsGetDummyPanCustomer.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetDummyPanCustomer;
        }


        public DataTable GetCriteriaMatches(string pan, string dob, string email, string moblile, int customerId)
        {

            Database db;
            DbCommand cmdGetDummyPanCustomer;
            DataSet dsGetDummyPanCustomer;
            DataTable dtGetDummyPanCustomer;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetDummyPanCustomer = db.GetStoredProcCommand("SPROC_GetCriteria");
                db.AddInParameter(cmdGetDummyPanCustomer, "@pan", DbType.String, pan);
                db.AddInParameter(cmdGetDummyPanCustomer, "@dob", DbType.String, dob);
                db.AddInParameter(cmdGetDummyPanCustomer, "@email", DbType.String, email);
                db.AddInParameter(cmdGetDummyPanCustomer, "@mobile", DbType.String, moblile);
                db.AddInParameter(cmdGetDummyPanCustomer, "@customerId", DbType.Int32, customerId);


                dsGetDummyPanCustomer = db.ExecuteDataSet(cmdGetDummyPanCustomer);
                dtGetDummyPanCustomer = dsGetDummyPanCustomer.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetDummyPanCustomer;
        }

        public DataTable GetAutoMergeCriteria(string pan, string dob, string email, string moblile, int customerId)
        {
            Database db;
            DbCommand cmdGetDummyPanCustomer;
            DataSet dsGetDummyPanCustomer;
            DataTable dtGetDummyPanCustomer;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetDummyPanCustomer = db.GetStoredProcCommand("SPROC_GetAutoMergeCriteria");
                db.AddInParameter(cmdGetDummyPanCustomer, "@pan", DbType.String, pan);
                db.AddInParameter(cmdGetDummyPanCustomer, "@dob", DbType.String, dob);
                db.AddInParameter(cmdGetDummyPanCustomer, "@email", DbType.String, email);
                db.AddInParameter(cmdGetDummyPanCustomer, "@mobile", DbType.String, moblile);
                db.AddInParameter(cmdGetDummyPanCustomer, "@customerId", DbType.Int32, customerId);

                dsGetDummyPanCustomer = db.ExecuteDataSet(cmdGetDummyPanCustomer);
                dtGetDummyPanCustomer = dsGetDummyPanCustomer.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetDummyPanCustomer;
        }


        public int CreateCustomerMerge(int deletingCustomerId, int matchCustomerId)
        {
            int result = 0;
            Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_GetClinentMerge");
                db.AddInParameter(createCmd, "@deletingCustomerId", DbType.Int32, deletingCustomerId);
                db.AddInParameter(createCmd, "@matchCustomerId", DbType.Int32, matchCustomerId);
                result = db.ExecuteNonQuery(createCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }


    }
}
