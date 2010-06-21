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
    public class WerpUploadsDao
    {
        public List<WerpUploadsVo> GetWerpNewCustomers()
        {
            List<WerpUploadsVo> uploadsCustomerList = new List<WerpUploadsVo>();
            WerpUploadsVo WerpUploadsVo;
            Database db;
            DbCommand getNewCustomersCmd;
            DataSet getNewCustomersDs;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getNewCustomersCmd = db.GetStoredProcCommand("SP_UploadGetNewCustomersWerp");
                getNewCustomersDs = db.ExecuteDataSet(getNewCustomersCmd);
                foreach (DataRow dr in getNewCustomersDs.Tables[0].Rows)
                {
                    WerpUploadsVo = new WerpUploadsVo();


                    WerpUploadsVo.Address1City = dr["CEXPS_Adr1City"].ToString();
                    WerpUploadsVo.Address1Country = dr["CEXPS_Adr1Country"].ToString();
                    WerpUploadsVo.Address1Line1 = dr["CEXPS_Adr1Line1"].ToString();
                    WerpUploadsVo.Address1Line2 = dr["CEXPS_Adr1Line2"].ToString();
                    WerpUploadsVo.Address1Line3 = dr["CEXPS_Adr1Line3"].ToString();
                    WerpUploadsVo.Address1Pincode = dr["CEXPS_Adr1PinCode"].ToString();
                    WerpUploadsVo.Address1State = dr["CEXPS_Adr1State"].ToString();
                    WerpUploadsVo.Address2City = dr["CEXPS_Adr2City"].ToString();
                    WerpUploadsVo.Address2Country = dr["CEXPS_Adr2Country"].ToString();
                    WerpUploadsVo.Address2Line1 = dr["CEXPS_Adr2Line1"].ToString();
                    WerpUploadsVo.Address2Line2 = dr["CEXPS_Adr2Line2"].ToString();
                    WerpUploadsVo.Address2Line3 = dr["CEXPS_Adr2Line3"].ToString();
                    WerpUploadsVo.Address2Pincode = dr["CEXPS_Adr2PinCode"].ToString();
                    WerpUploadsVo.Address2State = dr["CEXPS_Adr2State"].ToString();
                    WerpUploadsVo.AltEmail = dr["CEXPS_AltEmail"].ToString();
                    WerpUploadsVo.CommencementDate = dr["CEXPS_CommencementDate"].ToString();
                    WerpUploadsVo.CompanyName = dr["CEXPS_CompanyName"].ToString();
                    WerpUploadsVo.CompanyWebsite = dr["CEXPS_CompanyWebsite"].ToString();
                    WerpUploadsVo.DOB = dr["CEXPS_DOB"].ToString();
                    WerpUploadsVo.Email = dr["CEXPS_Email"].ToString();
                    WerpUploadsVo.Fax = dr["CEXPS_Fax"].ToString();
                    WerpUploadsVo.FirstName = dr["CEXPS_FirstName"].ToString();
                    WerpUploadsVo.Gender = dr["CEXPS_Gender"].ToString();
                    WerpUploadsVo.ISDFax = dr["CEXPS_ISDFax"].ToString();
                    WerpUploadsVo.LastName = dr["CEXPS_LastName"].ToString();
//                    WerpUploadsVo.MaritalStatus = dr["CEXPS_MaritalStatus"].ToString();
                    WerpUploadsVo.MarriageDate = dr["CEXPS_MarriageDate"].ToString();
                    WerpUploadsVo.MiddleName = dr["CEXPS_MiddleName"].ToString();
                    WerpUploadsVo.Mobile1 = dr["CEXPS_Mobile1"].ToString();
                    WerpUploadsVo.Mobile2 = dr["CEXPS_Mobile2"].ToString();
//                    WerpUploadsVo.Nationality = dr["CEXPS_Nationality"].ToString();
//                    WerpUploadsVo.Occupation = dr["CEXPS_Occupation"].ToString();
                    WerpUploadsVo.OfcAddressCity = dr["CEXPS_OfcAdrCity"].ToString();
                    WerpUploadsVo.OfcAddressCountry = dr["CEXPS_OfcAdrCountry"].ToString();
                    WerpUploadsVo.OfcAddressLine1 = dr["CEXPS_OfcAdrLine1"].ToString();
                    WerpUploadsVo.OfcAddressLine2 = dr["CEXPS_OfcAdrLine2"].ToString();
                    WerpUploadsVo.OfcAddressLine3 = dr["CEXPS_OfcAdrLine3"].ToString();
                    WerpUploadsVo.OfcAddressPincode = dr["CEXPS_OfcAdrPinCode"].ToString();
                    WerpUploadsVo.OfcAddressState = dr["CEXPS_OfcAdrState"].ToString();
                    WerpUploadsVo.OfcFax = dr["CEXPS_OfcFax"].ToString();
                    WerpUploadsVo.OfcFaxISD = dr["CEXPS_OfcFaxISD"].ToString();
                    WerpUploadsVo.OfcFaxSTD = dr["CEXPS_OfcFaxSTD"].ToString();
                    WerpUploadsVo.OfcISDCode = dr["CEXPS_OfcISDCode"].ToString();
                    WerpUploadsVo.OfcPhoneNumber = dr["CEXPS_OfcPhoneNum"].ToString();
                    WerpUploadsVo.OfcSTDCode = dr["CEXPS_OfcSTDCode"].ToString();
                    WerpUploadsVo.PanNumber = dr["CEXPS_PANNum"].ToString();
//                    WerpUploadsVo.Qualification = dr["CEXPS_Qualification"].ToString();
                    WerpUploadsVo.RBIApprovalDate = dr["CEXPS_RBIApprovalDate"].ToString();
                    WerpUploadsVo.RBIRefNumber = dr["CEXPS_RBIRefNum"].ToString();
                    WerpUploadsVo.RegistrationDate = dr["CEXPS_RegistrationDate"].ToString();
                    WerpUploadsVo.RegistrationNumber = dr["CEXPS_RegistrationNum"].ToString();
                    WerpUploadsVo.RegistrationPlace = dr["CEXPS_RegistrationPlace"].ToString();
                    WerpUploadsVo.ResISDCode = dr["CEXPS_ResISDCode"].ToString();
                    WerpUploadsVo.ResPhoneNumber = dr["CEXPS_ResPhoneNum"].ToString();
                    WerpUploadsVo.ResSTDCode = dr["CEXPS_ResSTDCode"].ToString();
                    WerpUploadsVo.Salutation = dr["CEXPS_Salutation"].ToString();
                    WerpUploadsVo.STDFax = dr["CEXPS_STDFax"].ToString();
//                    WerpUploadsVo.SubType = dr["CEXPS_SubType"].ToString();
                    WerpUploadsVo.Type = dr["CEXPS_Type"].ToString();

                    uploadsCustomerList.Add(WerpUploadsVo);
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

                FunctionInfo.Add("Method", "WerpUploadsDao.cs:GetWerpNewCustomers()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return uploadsCustomerList;
        }

        public bool UpdateProfileStagingIsCustomerNew()
        {
            Database db;
            DbCommand updateStagingIsCustomerNew;

            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateStagingIsCustomerNew = db.GetStoredProcCommand("SP_UpdateWerpProfileStagingIsCustomerNew");
                db.ExecuteNonQuery(updateStagingIsCustomerNew);
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

                FunctionInfo.Add("Method", "MFUploadDao.cs:UpdateProfileStagingIsCustomerNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return result;
        }

    }
}
