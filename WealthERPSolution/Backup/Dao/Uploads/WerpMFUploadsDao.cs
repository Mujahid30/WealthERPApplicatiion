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
    public class WerpMFUploadsDao
    {

        public List<WerpMFUploadsVo> GetWerpMFProfNewCustomers(int processId)
        {
            List<WerpMFUploadsVo> uploadsCustomerList = new List<WerpMFUploadsVo>();
            WerpMFUploadsVo WerpMFUploadsVo;
            Database db;
            DbCommand getNewCustomersCmd;
            DataSet getNewCustomersDs;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewCustomersCmd = db.GetStoredProcCommand("SP_UploadGetNewCustomersWerpMFProf");
                db.AddInParameter(getNewCustomersCmd, "@processId", DbType.Int32, processId);
                getNewCustomersDs = db.ExecuteDataSet(getNewCustomersCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
                foreach (DataRow dr in getNewCustomersDs.Tables[0].Rows)
                {
                    WerpMFUploadsVo = new WerpMFUploadsVo();

                    WerpMFUploadsVo.FirstName = dr["CMFXPS_FirstName"].ToString();
                    WerpMFUploadsVo.MiddleName = dr["CMFXPS_MiddleName"].ToString();
                    WerpMFUploadsVo.LastName = dr["CMFXPS_LastName"].ToString();
                    WerpMFUploadsVo.Gender = dr["CMFXPS_Gender"].ToString();
                    WerpMFUploadsVo.DOB = dr["CMFXPS_DOB"].ToString();
                    WerpMFUploadsVo.Type = dr["CMFXPS_Type"].ToString();
                    WerpMFUploadsVo.SubType = dr["CMFXPS_SubType"].ToString();
                    WerpMFUploadsVo.Salutation = dr["CMFXPS_Salutation"].ToString();
                    WerpMFUploadsVo.PanNumber = dr["CMFXPS_PANNum"].ToString();
                    WerpMFUploadsVo.Address1 = dr["CMFXPS_Adr1Line1"].ToString();
                    WerpMFUploadsVo.Address2 = dr["CMFXPS_Adr1Line2"].ToString();
                    WerpMFUploadsVo.Address3 = dr["CMFXPS_Adr1Line3"].ToString();
                    WerpMFUploadsVo.City = dr["CMFXPS_Adr1City"].ToString();
                    WerpMFUploadsVo.Pincode = dr["CMFXPS_Adr1PinCode"].ToString();
                    WerpMFUploadsVo.State = dr["CMFXPS_Adr1State"].ToString();
                    WerpMFUploadsVo.Country = dr["CMFXPS_Adr1Country"].ToString();
                    WerpMFUploadsVo.Address2Line1 = dr["CMFXPS_Adr2Line1"].ToString();
                    WerpMFUploadsVo.Address2Line2 = dr["CMFXPS_Adr2Line2"].ToString();
                    WerpMFUploadsVo.Address2Line3 = dr["CMFXPS_Adr2Line3"].ToString();
                    WerpMFUploadsVo.Address2City = dr["CMFXPS_Adr2City"].ToString();
                    WerpMFUploadsVo.Address2Pincode = dr["CMFXPS_Adr2PinCode"].ToString();
                    WerpMFUploadsVo.Address2State = dr["CMFXPS_Adr2State"].ToString();
                    WerpMFUploadsVo.Address2Country = dr["CMFXPS_Adr2Country"].ToString();
                    WerpMFUploadsVo.ResISDCode = dr["CMFXPS_ResISDCode"].ToString();
                    WerpMFUploadsVo.ResSTDCode = dr["CMFXPS_ResSTDCode"].ToString();
                    WerpMFUploadsVo.ResPhoneNum = dr["CMFXPS_ResPhoneNum"].ToString();
                    WerpMFUploadsVo.OfcISDCode = dr["CMFXPS_OfcISDCode"].ToString();
                    WerpMFUploadsVo.OfcSTDCode = dr["CMFXPS_OfcSTDCode"].ToString();
                    WerpMFUploadsVo.OfcPhoneNum = dr["CMFXPS_OfcPhoneNum"].ToString();
                    WerpMFUploadsVo.Email = dr["CMFXPS_Email"].ToString();
                    WerpMFUploadsVo.AltEmail = dr["CMFXPS_AltEmail"].ToString();
                    WerpMFUploadsVo.Mobile1 = dr["CMFXPS_Mobile1"].ToString();
                    WerpMFUploadsVo.Mobile2 = dr["CMFXPS_Mobile2"].ToString();
                    WerpMFUploadsVo.FaxISD = dr["CMFXPS_ISDFax"].ToString();
                    WerpMFUploadsVo.FaxSTD = dr["CMFXPS_STDFax"].ToString();
                    WerpMFUploadsVo.Fax = dr["CMFXPS_Fax"].ToString();
                    WerpMFUploadsVo.OfcFax = dr["CMFXPS_OfcFax"].ToString();
                    WerpMFUploadsVo.OfcFaxISD = dr["CMFXPS_OfcFaxISD"].ToString();
                    WerpMFUploadsVo.OfcFaxSTD = dr["CMFXPS_OfcFaxSTD"].ToString();
                    WerpMFUploadsVo.Occupation = dr["CMFXPS_Occupation"].ToString();
                    WerpMFUploadsVo.Qualification = dr["CMFXPS_Qualification"].ToString();
                    WerpMFUploadsVo.MarriageDate = dr["CMFXPS_MarriageDate"].ToString();
                    WerpMFUploadsVo.MaritalStatus = dr["CMFXPS_MaritalStatus"].ToString();
                    WerpMFUploadsVo.Nationality = dr["CMFXPS_Nationality"].ToString();
                    WerpMFUploadsVo.RBIRefNum = dr["CMFXPS_RBIRefNum"].ToString();
                    WerpMFUploadsVo.RBIApprovalDate = dr["CMFXPS_RBIApprovalDate"].ToString();
                    WerpMFUploadsVo.CompanyName = dr["CMFXPS_CompanyName"].ToString();
                    WerpMFUploadsVo.OfcAddress1 = dr["CMFXPS_OfcAdrLine1"].ToString();
                    WerpMFUploadsVo.OfcAddress2 = dr["CMFXPS_OfcAdrLine2"].ToString();
                    WerpMFUploadsVo.OfcAddress3 = dr["CMFXPS_OfcAdrLine3"].ToString();
                    WerpMFUploadsVo.OfcAddressPincode = dr["CMFXPS_OfcAdrPinCode"].ToString();
                    WerpMFUploadsVo.OfcAddressCity = dr["CMFXPS_OfcAdrCity"].ToString();
                    WerpMFUploadsVo.OfcAddressState = dr["CMFXPS_OfcAdrState"].ToString();
                    WerpMFUploadsVo.OfcAddressCountry = dr["CMFXPS_OfcAdrCountry"].ToString();
                    WerpMFUploadsVo.RegistrationDate = dr["CMFXPS_RegistrationDate"].ToString();
                    WerpMFUploadsVo.CommencementDate = dr["CMFXPS_CommencementDate"].ToString();
                    WerpMFUploadsVo.RegistrationPlace = dr["CMFXPS_RegistrationPlace"].ToString();
                    WerpMFUploadsVo.RegistrationNum = dr["CMFXPS_RegistrationNum"].ToString();
                    WerpMFUploadsVo.CompanyWebsite = dr["CMFXPS_CompanyWebsite"].ToString();
                    //WerpMFUploadsVo.BankName = dr["CMFXPS_BankName"].ToString();
                    //WerpMFUploadsVo.AccountType = dr["CMFXPS_AccountType"].ToString();
                    //WerpMFUploadsVo.AccountNum = dr["CMFXPS_AccountNum"].ToString();
                    //WerpMFUploadsVo.BankModeOfOperation = dr["CMFXPS_BankModeOfOperation"].ToString();
                    //WerpMFUploadsVo.BranchName = dr["CMFXPS_BranchName"].ToString();
                    //WerpMFUploadsVo.BranchAddress1 = dr["CMFXPS_BranchAdrLine1"].ToString();
                    //WerpMFUploadsVo.BranchAddress2 = dr["CMFXPS_BranchAdrLine2"].ToString();
                    //WerpMFUploadsVo.BranchAddress3 = dr["CMFXPS_BranchAdrLine3"].ToString();
                    //WerpMFUploadsVo.BranchAddressPincode = dr["CMFXPS_BranchAdrPinCode"].ToString();
                    //WerpMFUploadsVo.BranchAddressCity = dr["CMFXPS_BranchAdrCity"].ToString();
                    //WerpMFUploadsVo.BranchAddressState = dr["CMFXPS_BranchAdrState"].ToString();
                    //WerpMFUploadsVo.BranchAddressCountry = dr["CMFXPS_BranchAdrCountry"].ToString();
                    //WerpMFUploadsVo.MICR = dr["CMFXPS_MICR"].ToString();
                    //WerpMFUploadsVo.IFSC = dr["CMFXPS_IFSC"].ToString();
                    //WerpMFUploadsVo.FolioNum = dr["CMFXPS_FolioNum"].ToString();
                    //WerpMFUploadsVo.AccountOpeningDate = dr["CMFXPS_AccountOpeningDate"].ToString();
                    //WerpMFUploadsVo.FolioModeOfOperating = dr["CMFXPS_FolioModeOfOperating"].ToString();
                    
        

                    uploadsCustomerList.Add(WerpMFUploadsVo);
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

                FunctionInfo.Add("Method", "WerpMFUploadsDao.cs:GetWerpMFProfNewCustomers()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return uploadsCustomerList;
        }

        public bool UpdateWerpMFProfileStagingIsCustomerNew(int adviserId, int processId)
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateWerpMFProfileStagingIsCustomerNew");
                db.AddInParameter(updateStagingIsFolioNew, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(updateStagingIsFolioNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsFolioNew);
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

                FunctionInfo.Add("Method", "WerpMFUploadDao.cs:UpdateWerpMFProfileStagingIsCustomerNew()");

                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public bool UpdateWerpMFProfileStagingIsFolioNew(int processId)
        {
            Database db;
            DbCommand updateStagingIsFolioNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsFolioNew = db.GetStoredProcCommand("SP_UpdateWerpMFProfileStagingIsFolioNew");
                db.AddInParameter(updateStagingIsFolioNew, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(updateStagingIsFolioNew);
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

                FunctionInfo.Add("Method", "WerpMFUploadDao.cs:UpdateWerpMFProfileStagingIsFolioNew()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

        public DataSet GetWerpMFProfileNewFolios(int processId)
        {
            DataSet uploadsFolioList = new DataSet();
            //WerpMFUploadsVo WerpMFUploadsVo;
            Database db;
            DbCommand getNewFoliosCmd;
            DataSet getNewFoliosDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewFoliosCmd = db.GetStoredProcCommand("SP_UploadGetWerpMFProfileNewFolios");
                db.AddInParameter(getNewFoliosCmd, "@processId", DbType.Int32, processId);
                getNewFoliosDs = db.ExecuteDataSet(getNewFoliosCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadDao.cs:GetWerpMFProfileNewFolios()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return getNewFoliosDs;
        }

    }
}
