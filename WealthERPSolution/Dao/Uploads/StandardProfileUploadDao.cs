using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoUploads
{
    public class StandardProfileUploadDao
    {
        public List<StandardProfileUploadVo> GetProfileNewCustomers(int processId)
        {
            List<StandardProfileUploadVo> uploadsCustomerList = new List<StandardProfileUploadVo>();
            StandardProfileUploadVo StandardProfileUploadVo;
            Database db;
            DbCommand getNewCustomersCmd;
            DataSet getNewCustomersDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewCustomersCmd = db.GetStoredProcCommand("SP_UploadGetNewCustomersStd");
                db.AddInParameter(getNewCustomersCmd, "@processId", DbType.Int32, processId);
                getNewCustomersDs = db.ExecuteDataSet(getNewCustomersCmd);

                foreach (DataRow dr in getNewCustomersDs.Tables[0].Rows)
                {
                    StandardProfileUploadVo = new StandardProfileUploadVo();
                    StandardProfileUploadVo.PANNum = dr["CPS_PANNum"].ToString();
                    StandardProfileUploadVo.FirstName = dr["CPS_FirstName"].ToString();
                    StandardProfileUploadVo.MiddleName = dr["CPS_MiddleName"].ToString();
                    StandardProfileUploadVo.LastName = dr["CPS_LastName"].ToString();
                    if (dr["AR_RMId"] == null || dr["AR_RMId"].ToString() == "")
                    {
                        StandardProfileUploadVo.RMId = 0;
                    }
                    else
                    {
                        StandardProfileUploadVo.RMId = int.Parse(dr["AR_RMId"].ToString());
                    }
                    StandardProfileUploadVo.Adr1Line1 = dr["CPS_Adr1Line1"].ToString();
                    StandardProfileUploadVo.Adr1Line2 = dr["CPS_Adr1Line2"].ToString();
                    StandardProfileUploadVo.Adr1Line3 = dr["CPS_Adr1Line3"].ToString();
                    StandardProfileUploadVo.Adr1PinCode = dr["CPS_Adr1PinCode"].ToString();
                    StandardProfileUploadVo.Type = dr["CPS_Type"].ToString();
                    StandardProfileUploadVo.SubType = dr["CPS_SubType"].ToString();
                    StandardProfileUploadVo.Adr1City = dr["CPS_Adr1City"].ToString();
                    StandardProfileUploadVo.Adr1State = dr["CPS_Adr1State"].ToString();
                    StandardProfileUploadVo.Adr2Country = dr["CPS_Adr1Country"].ToString();
                    StandardProfileUploadVo.Adr2Line1 = dr["CPS_Adr2Line1"].ToString();
                    StandardProfileUploadVo.Adr2Line2 = dr["CPS_Adr2Line2"].ToString();
                    StandardProfileUploadVo.Adr2Line3 = dr["CPS_Adr2Line3"].ToString();
                    StandardProfileUploadVo.Adr2PinCode = dr["CPS_Adr2PinCode"].ToString();
                    StandardProfileUploadVo.Adr2City = dr["CPS_Adr2City"].ToString();
                    StandardProfileUploadVo.Adr2State = dr["CPS_Adr2State"].ToString();
                    StandardProfileUploadVo.Adr2Country = dr["CPS_Adr2Country"].ToString();
                    StandardProfileUploadVo.ResISDCode = dr["CPS_ResISDCode"].ToString();
                    StandardProfileUploadVo.ResSTDCode = dr["CPS_ResSTDCode"].ToString();
                    StandardProfileUploadVo.ResPhoneNum = dr["CPS_ResPhoneNum"].ToString();
                    StandardProfileUploadVo.OfcISDCode = dr["CPS_OfcISDCode"].ToString();
                    StandardProfileUploadVo.OfcSTDCode = dr["CPS_OfcSTDCode"].ToString();
                    StandardProfileUploadVo.OfcPhoneNum = dr["CPS_OfcPhoneNum"].ToString();
                    StandardProfileUploadVo.Email = dr["CPS_Email"].ToString();
                    StandardProfileUploadVo.AltEmail = dr["CPS_AltEmail"].ToString();
                    StandardProfileUploadVo.Mobile1 = dr["CPS_Mobile1"].ToString();
                    StandardProfileUploadVo.Mobile2 = dr["CPS_Mobile2"].ToString();
                    StandardProfileUploadVo.ISDFax = dr["CPS_ISDFax"].ToString();
                    StandardProfileUploadVo.STDFax = dr["CPS_STDFax"].ToString();
                    StandardProfileUploadVo.Fax = dr["CPS_Fax"].ToString();
                    StandardProfileUploadVo.OfcFax = dr["CPS_OfcFax"].ToString();
                    StandardProfileUploadVo.OfcFaxISD = dr["CPS_OfcFaxISD"].ToString();
                    StandardProfileUploadVo.OfcFaxSTD = dr["CPS_OfcFaxSTD"].ToString();
                    StandardProfileUploadVo.Occupation = dr["CPS_Occupation"].ToString();
                    StandardProfileUploadVo.Qualification = dr["CPS_Qualification"].ToString();
                    StandardProfileUploadVo.MarriageDate = dr["CPS_MarriageDate"].ToString();
                    StandardProfileUploadVo.MaritalStatus = dr["CPS_MaritalStatus"].ToString();
                    StandardProfileUploadVo.Nationality = dr["CPS_Nationality"].ToString();
                    StandardProfileUploadVo.RBIRefNum = dr["CPS_RBIRefNum"].ToString();
                    StandardProfileUploadVo.RBIApprovalDate = dr["CPS_RBIApprovalDate"].ToString();
                    StandardProfileUploadVo.CompanyName = dr["CPS_CompanyName"].ToString();
                    StandardProfileUploadVo.DOB = dr["CPS_DOB"].ToString();
                    StandardProfileUploadVo.OfcAdrLine1 = dr["CPS_OfcAdrLine1"].ToString();
                    StandardProfileUploadVo.OfcAdrLine2 = dr["CPS_OfcAdrLine2"].ToString();
                    StandardProfileUploadVo.OfcAdrLine3 = dr["CPS_OfcAdrLine3"].ToString();
                    StandardProfileUploadVo.OfcAdrPinCode = dr["CPS_OfcAdrPinCode"].ToString();
                    StandardProfileUploadVo.OfcAdrCity = dr["CPS_OfcAdrCity"].ToString();
                    StandardProfileUploadVo.OfcAdrState = dr["CPS_OfcAdrState"].ToString();
                    StandardProfileUploadVo.OfcAdrCountry = dr["CPS_OfcAdrCountry"].ToString();
                    StandardProfileUploadVo.RegistrationDate = dr["CPS_RegistrationDate"].ToString();
                    StandardProfileUploadVo.CommencementDate = dr["CPS_CommencementDate"].ToString();
                    StandardProfileUploadVo.RegistrationNum = dr["CPS_RegistrationNum"].ToString();
                    StandardProfileUploadVo.CompanyWebsite = dr["CPS_CompanyWebsite"].ToString();
                    StandardProfileUploadVo.BankName = dr["CPS_BankName"].ToString();
                    StandardProfileUploadVo.BankAccountType = dr["CPS_BankAccountType"].ToString();
                    StandardProfileUploadVo.BankAccountNum = dr["CPS_BankAccountNum"].ToString();
                    StandardProfileUploadVo.BankModeOfOperation = dr["CPS_BankModeOfOperation"].ToString();
                    StandardProfileUploadVo.BranchName = dr["CPS_BranchName"].ToString();
                    StandardProfileUploadVo.BranchAdrLine1 = dr["CPS_BranchAdrLine1"].ToString();
                    StandardProfileUploadVo.BranchAdrLine2 = dr["CPS_BranchAdrLine2"].ToString();
                    StandardProfileUploadVo.BranchAdrLine3 = dr["CPS_BranchAdrLine3"].ToString();
                    StandardProfileUploadVo.BranchAdrPinCode = dr["CPS_BranchAdrPinCode"].ToString();
                    StandardProfileUploadVo.BranchAdrCity = dr["CPS_BranchAdrCity"].ToString();
                    StandardProfileUploadVo.BranchAdrState = dr["CPS_BranchAdrState"].ToString();
                    StandardProfileUploadVo.BranchAdrCountry = dr["CPS_BranchAdrCountry"].ToString();
                    StandardProfileUploadVo.MICR = dr["CPS_MICR"].ToString();
                    StandardProfileUploadVo.IFSC = dr["CPS_IFSC"].ToString();
                    StandardProfileUploadVo.ContactGuardianFirstName = dr["CPS_ContactGuardianFirstName"].ToString();
                    StandardProfileUploadVo.ContactGuardianMiddleName = dr["CPS_ContactGuardianMiddleName"].ToString();
                    StandardProfileUploadVo.ContactGuardianLastName = dr["CPS_ContactGuardianLastName"].ToString();
                    if (dr["C_IsProspect"] == null || dr["C_IsProspect"].ToString() == "")
                    {
                        StandardProfileUploadVo.IsProspect = 0;
                    }
                    else
                    {
                        StandardProfileUploadVo.IsProspect = int.Parse(dr["C_IsProspect"].ToString());
                    }
                    uploadsCustomerList.Add(StandardProfileUploadVo);
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

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:GetCamsNewCustomers()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return uploadsCustomerList;
        }

        public bool UpdateProfileStagingIsCustomerNew(int adviserId, int processId,int branchId)
        {
            Database db;
            DbCommand updateProfileStagingIsCustomerNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateProfileStagingIsCustomerNew = db.GetStoredProcCommand("SP_UpdateProfileStagingIsCustomerNew");
                db.AddInParameter(updateProfileStagingIsCustomerNew, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(updateProfileStagingIsCustomerNew, "@processId", DbType.Int32, processId);
                db.AddInParameter(updateProfileStagingIsCustomerNew, "@branchId", DbType.Int32, branchId);
                db.ExecuteNonQuery(updateProfileStagingIsCustomerNew);
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

                FunctionInfo.Add("Method", "StandardProfileUploadDao.cs:UpdateProfileStagingIsCustomerNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool StdDeleteCommonStaging(int processId)
        {
            Database db;
            DbCommand deleteProfileStagingUnRejectedData;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteProfileStagingUnRejectedData = db.GetStoredProcCommand("SP_UploadsProfileCommonStagingDelete");
                db.AddInParameter(deleteProfileStagingUnRejectedData, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(deleteProfileStagingUnRejectedData);
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

                FunctionInfo.Add("Method", "StandardProfileUploadDao.cs:StdDeleteCommonStaging()");

                object[] objects = new object[0];
                objects[0]=processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public int GetBranchHeadId(int branchId)
        {
            Database db;
            DbCommand getBranchHeadId;
            int branchHeadId;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getBranchHeadId = db.GetStoredProcCommand("SP_GetBranchHeadId");
                db.AddInParameter(getBranchHeadId, "@branchId", DbType.Int32, branchId);
                branchHeadId=int.Parse(db.ExecuteScalar(getBranchHeadId).ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "StandardProfileUploadDao.cs:GetBranchHeadId()");

                object[] objects = new object[1];
                objects[0] = branchId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return branchHeadId;
        }
    }
}

