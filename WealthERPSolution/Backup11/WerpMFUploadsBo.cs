using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using DaoUploads;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using BoUser;
using VoUser;
using BoCustomerProfiling;
using BoAdvisorProfiling;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.SqlServer;
using System.Data;
using System.Data.OleDb;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.SqlServer.Dts.Runtime;

namespace BoUploads
{
    public class WerpMFUploadsBo
    {
        Microsoft.SqlServer.Dts.Runtime.Application App = new Microsoft.SqlServer.Dts.Runtime.Application();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        PortfolioBo PortfolioBo = new PortfolioBo();
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        Random id = new Random();
        AdvisorStaffBo advisorstaffBo = new AdvisorStaffBo();
        int userId;


        public List<WerpMFUploadsVo> GetWerpMFProfNewCustomers(int processId)
        {
            List<WerpMFUploadsVo> UploadsCustomerList = new List<WerpMFUploadsVo>();

            WerpMFUploadsDao WerpMFUploadsDao = new WerpMFUploadsDao();
            UploadsCustomerList = WerpMFUploadsDao.GetWerpMFProfNewCustomers(processId);

            return UploadsCustomerList;
        }
        public bool UpdateWerpMFProfileStagingIsCustomerNew(int adviserId, int processId)
        {
            bool result = false;
            WerpMFUploadsDao WerpMFUploadsDao = new WerpMFUploadsDao();
            WerpMFUploadsDao.UpdateWerpMFProfileStagingIsCustomerNew(adviserId, processId);
            result = true;
            return result;
        }

        public DataSet GetWerpMFProfileNewFolios(int processId)
        {
            DataSet getNewfolioDs = new DataSet();

            WerpMFUploadsDao WerpMFUploadsDao = new WerpMFUploadsDao();
            getNewfolioDs = WerpMFUploadsDao.GetWerpMFProfileNewFolios(processId);

            return getNewfolioDs;
        }

        public bool UpdateWerpMFProfileStagingIsFolioNew(int processId)
        {
            bool result = false;
            WerpMFUploadsDao WerpMFUploadsDao = new WerpMFUploadsDao();
            WerpMFUploadsDao.UpdateWerpMFProfileStagingIsFolioNew(processId);
            result = true;
            return result;
        }

        //Second phase: of the WerpMF Profile Upload; Insertion of WerpMF Data from XML to Input table, Cleaning
        public bool WerpMFInsertToInputProfile(string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package werpMFProPkg1 = App.LoadPackage(Packagepath, null);
                werpMFProPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                DTSExecResult werpMFProResult1 = werpMFProPkg1.Execute();
                if (werpMFProResult1.ToString() == "Success")
                    IsProcessComplete = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadsBo.cs:WerpMFInsertToInputProfile()");

                object[] objects = new object[2];
                objects[0] = Packagepath;
                objects[1] = XMLFilepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Third Phase: Insert to WerpMF Staging table from Input table
        public bool WerpMFInsertToStagingProfile(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package werpMFProPkg2 = App.LoadPackage(Packagepath, null);
                werpMFProPkg2.Variables["varProcessId"].Value = processId;
                DTSExecResult werpMFProResult2 = werpMFProPkg2.Execute();
                if (werpMFProResult2.ToString() == "Success")
                    IsProcessComplete = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadsBo.cs:WerpMFInsertToStagingProfile()");

                object[] objects = new object[2];
                objects[0] = processId;
                objects[1] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Fourth Phase: Checks and setting of flags in the WerpMF staging table
        public bool WerpMFProcessDataInStagingProfile(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                string query1 = "UPDATE CustomerMFXtrnlProfileStaging SET CMFXPS_IsCustomerNew = 0 , C_CustomerId = B.C_CustomerId " +
                                "FROM CustomerMFXtrnlProfileStaging A " +
                                "INNER JOIN Customer B " +
                                "ON A.CMFXPS_PANNum = B.C_PANNum " +
                                "INNER JOIN AdviserRM C " +
                                "ON B.AR_RMId = C.AR_RMId " +
                                "WHERE C.A_AdviserId = " + AdviserId + " and A.ADUL_ProcessId =" + processId;
                string query2 = "UPDATE CustomerMFXtrnlProfileStaging SET CMFXPS_IsFolioNew = 0 , CMFA_AccountId = B.CMFA_AccountId " +
                                "FROM CustomerMFXtrnlProfileStaging A " +
                                "INNER JOIN CustomerMutualFundAccount B " +
                                "ON A.CMFXPS_FolioNum = B.CMFA_FolioNum AND A.PA_AMCCode = B.PA_AMCCode " +
                                "INNER JOIN CustomerPortfolio C " +
                                "ON B.CP_PortfolioId = C.CP_PortfolioId " +
                                "INNER JOIN Customer D " +
                                "ON C.C_CustomerId = D.C_CustomerId " +
                                "INNER JOIN AdviserRM E " +
                                "ON D.AR_RMId = E.AR_RMId " +
                                "WHERE  E.A_AdviserId =" + AdviserId + " and A.ADUL_ProcessID =" + processId;

                Package werpMFProPkg3 = App.LoadPackage(Packagepath, null);
                werpMFProPkg3.Variables["varQueryCustomerCheck"].Value = query1;
                werpMFProPkg3.Variables["varQueryFolioCheck"].Value = query2;
                werpMFProPkg3.Variables["varProcessIdPanNullCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdTypeCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdSubTypeCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdQualificationCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdOccupationCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdNationalityCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdMaritalStatusCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdFolioModeOfOperatingCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdBankModeOfOperationCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdBankAccountTypeCheck"].Value = processId;
                werpMFProPkg3.Variables["varProcessIdAMCCheck"].Value = processId;

                DTSExecResult werpMFProResult3 = werpMFProPkg3.Execute();
                if (werpMFProResult3.ToString() == "Success")
                    IsProcessComplete = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadsBo.cs:WerpMFProcessDataInStagingProfile()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = AdviserId;
                objects[2] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Fifth Phase: Move WerpMF customer details to customer table
        public bool WerpMFInsertCustomerDetails(int adviserId, int processId, int rmId,out int countCustCreated,out int countFolioCreated)
        {
            bool IsProcessComplete = false;
            List<WerpMFUploadsVo> werpMFNewCustomerList = new List<WerpMFUploadsVo>();
            Nullable<DateTime> dt = new DateTime();
            WerpMFUploadsVo werpMFUploadsVo = new WerpMFUploadsVo();
            string resIsdCode = "", resStdCode = "", resPhoneNum = "", offIsdCode = "", offStdCode = "", offPhoneNum = "";
            string resFaxIsdCode = "", resFaxStdCode = "", resFaxNum = "", offFaxIsdCode = "", offFaxStdCode = "", offFaxNum = "";
            int lenPhoneNum;
            int lenFaxNum;
            countCustCreated = 0;
            countFolioCreated = 0;
            DataSet getNewFoliosDs = new DataSet();
            DataTable getNewFoliosDt = new DataTable();
            userId = advisorstaffBo.GetUserId(rmId);

            try
            {
                werpMFNewCustomerList = GetWerpMFProfNewCustomers(processId);
                for (int i = 0; i < werpMFNewCustomerList.Count; i++)
                {
                    customerVo = new CustomerVo();
                    userVo = new UserVo();
                    werpMFUploadsVo = new WerpMFUploadsVo();

                    werpMFUploadsVo = werpMFNewCustomerList[i];
                    userVo.FirstName = werpMFUploadsVo.FirstName;
                    userVo.MiddleName = werpMFUploadsVo.MiddleName;
                    userVo.LastName = werpMFUploadsVo.LastName;
                    userVo.Password = id.Next(10000, 99999).ToString();
                    userVo.Email = werpMFUploadsVo.Email;
                    userVo.LoginId = werpMFUploadsVo.Email;
                    userVo.UserType = "Customer";

                    //userId = userBo.CreateUser(userVo);

                    customerVo.UserId = userId;
                    customerVo.RmId = rmId;

                    customerVo.FirstName = werpMFUploadsVo.FirstName;
                    customerVo.MiddleName = werpMFUploadsVo.MiddleName;
                    customerVo.LastName = werpMFUploadsVo.LastName;
                    customerVo.Gender=werpMFUploadsVo.Gender;
                    if (werpMFUploadsVo.DOB == "")
                        customerVo.Dob = DateTime.Parse(dt.ToString());
                    else
                        customerVo.Dob = DateTime.Parse(werpMFUploadsVo.DOB);
                    if(werpMFUploadsVo.Type !="")
                    customerVo.Type=werpMFUploadsVo.Type;
                    if (werpMFUploadsVo.SubType != "")
                    customerVo.SubType=werpMFUploadsVo.SubType;
                    customerVo.Salutation = werpMFUploadsVo.Salutation;
                    customerVo.PANNum = werpMFUploadsVo.PanNumber;
                    customerVo.Adr1Line1 = werpMFUploadsVo.Address1;
                    customerVo.Adr1Line2 = werpMFUploadsVo.Address2;
                    customerVo.Adr1Line3 = werpMFUploadsVo.Address3;
                    if (werpMFUploadsVo.Pincode == "")
                        customerVo.Adr1PinCode = 0;
                    else
                    customerVo.Adr1PinCode = Int32.Parse(werpMFUploadsVo.Pincode);
                    customerVo.Adr1City = werpMFUploadsVo.City;
                    customerVo.Adr1State = werpMFUploadsVo.State;
                    customerVo.Adr1Country = werpMFUploadsVo.Country;
                    customerVo.Adr2Line1=werpMFUploadsVo.Address2Line1;
                    customerVo.Adr2Line2=werpMFUploadsVo.Address2Line2;
                    customerVo.Adr2Line3=werpMFUploadsVo.Address2Line3;
                    if (werpMFUploadsVo.Address2Pincode == "")
                        customerVo.Adr2PinCode = 0;
                    else
                        customerVo.Adr2PinCode=Int32.Parse(werpMFUploadsVo.Address2Pincode);
                    customerVo.Adr2City=werpMFUploadsVo.Address2City;
                    customerVo.Adr2Country=werpMFUploadsVo.Address2Country;
                    customerVo.Adr2State=werpMFUploadsVo.Address2State;
                    customerVo.Email=werpMFUploadsVo.Email;
                    customerVo.AltEmail=werpMFUploadsVo.AltEmail;
                    if (werpMFUploadsVo.Mobile1 == "")
                        customerVo.Mobile1 = 0;
                    else
                        customerVo.Mobile1 = Int32.Parse(werpMFUploadsVo.Mobile1);
                    if (werpMFUploadsVo.Mobile2 == "")
                        customerVo.Mobile2 = 0;
                    else
                        customerVo.Mobile2 = Int32.Parse(werpMFUploadsVo.Mobile2);
                    if (werpMFUploadsVo.Occupation != "")
                    customerVo.Occupation=werpMFUploadsVo.Occupation;
                    if(werpMFUploadsVo.Qualification!="")
                    customerVo.Qualification=werpMFUploadsVo.Qualification;
                    //customerVo.MarriageDate
                    if (werpMFUploadsVo.MaritalStatus != "")
                    customerVo.MaritalStatus=werpMFUploadsVo.MaritalStatus;
                    if (werpMFUploadsVo.Nationality != "")
                    customerVo.Nationality=werpMFUploadsVo.Nationality;
                    customerVo.RBIRefNum=werpMFUploadsVo.RBIRefNum;
                    if (werpMFUploadsVo.RBIApprovalDate == "")
                        customerVo.RBIApprovalDate = DateTime.Parse(dt.ToString());
                    else
                        customerVo.RBIApprovalDate=DateTime.Parse(werpMFUploadsVo.RBIApprovalDate.ToString());
                    customerVo.CompanyName=werpMFUploadsVo.CompanyName;
                    customerVo.OfcAdrLine1=werpMFUploadsVo.OfcAddress1;
                    customerVo.OfcAdrLine2=werpMFUploadsVo.OfcAddress2;
                    customerVo.OfcAdrLine3=werpMFUploadsVo.OfcAddress3;
                    if (werpMFUploadsVo.OfcAddressPincode == "")
                        customerVo.OfcAdrPinCode = 0;
                    else
                        customerVo.OfcAdrPinCode=Int32.Parse(werpMFUploadsVo.OfcAddressPincode);
                    customerVo.OfcAdrState=werpMFUploadsVo.State;
                    customerVo.OfcAdrCity=werpMFUploadsVo.OfcAddressCity;
                    customerVo.OfcAdrCountry=werpMFUploadsVo.OfcAddressCountry;
                    if (werpMFUploadsVo.RegistrationDate == "")
                        customerVo.RegistrationDate = DateTime.Parse(dt.ToString());
                    else
                        customerVo.RegistrationDate=DateTime.Parse(werpMFUploadsVo.RegistrationDate.ToString());
                    if (werpMFUploadsVo.CommencementDate == "")
                        customerVo.CommencementDate = DateTime.Parse(dt.ToString());
                    else
                        customerVo.CommencementDate=DateTime.Parse(werpMFUploadsVo.CommencementDate.ToString());
                    customerVo.RegistrationPlace=werpMFUploadsVo.RegistrationPlace;
                    customerVo.RegistrationNum=werpMFUploadsVo.ResPhoneNum;
                    customerVo.CompanyWebsite=werpMFUploadsVo.CompanyWebsite;
                    customerVo.LoginId = werpMFUploadsVo.Email;
                    customerVo.Password = id.Next(10000, 99999).ToString();
                    //**************************************************************************
                    //Office phone number
                    lenPhoneNum = werpMFUploadsVo.OfcPhoneNum.Length;
                    if (lenPhoneNum >= 8)
                    {
                        offPhoneNum = werpMFUploadsVo.OfcPhoneNum.Substring(lenPhoneNum - 8, 8);
                        if (lenPhoneNum >= 11)
                        {
                            offStdCode = werpMFUploadsVo.OfcPhoneNum.Substring(lenPhoneNum - 11, 3);
                            if (lenPhoneNum >= 12)
                                offIsdCode = werpMFUploadsVo.OfcPhoneNum.Substring(0, lenPhoneNum - 11);
                        }
                        else
                            offStdCode = werpMFUploadsVo.OfcPhoneNum.Substring(0, lenPhoneNum - 8);
                    }
                    else
                        offPhoneNum = werpMFUploadsVo.OfcPhoneNum;
                    if (offIsdCode != "")
                        customerVo.OfcISDCode = Int32.Parse(offIsdCode);
                    else
                        customerVo.OfcISDCode = 0;
                    if (offStdCode != "")
                        customerVo.OfcSTDCode = Int32.Parse(offStdCode);
                    else
                        customerVo.OfcSTDCode = 0;
                    if (offPhoneNum != "")
                        customerVo.OfcPhoneNum = Int32.Parse(offPhoneNum);
                    else
                        customerVo.OfcPhoneNum = 0;

                    //**************************************************************************
                    //residence phone number

                    lenPhoneNum = werpMFUploadsVo.ResPhoneNum.Length;
                    if (lenPhoneNum >= 8)
                    {
                        resPhoneNum = werpMFUploadsVo.ResPhoneNum.Substring(lenPhoneNum - 8, 8);
                        if (lenPhoneNum >= 11)
                        {
                            resStdCode = werpMFUploadsVo.ResPhoneNum.Substring(lenPhoneNum - 11, 3);
                            if (lenPhoneNum >= 12)
                                resIsdCode = werpMFUploadsVo.ResPhoneNum.Substring(0, lenPhoneNum - 11);
                        }
                        else
                            resStdCode = werpMFUploadsVo.ResPhoneNum.Substring(0, lenPhoneNum - 8);
                    }
                    else
                        resPhoneNum = werpMFUploadsVo.ResPhoneNum;
                    if (resIsdCode != "")
                        customerVo.ResISDCode = Int32.Parse(resIsdCode);
                    else
                        customerVo.ResISDCode = 0;
                    if (resStdCode != "")
                        customerVo.ResSTDCode = Int32.Parse(resStdCode);
                    else
                        customerVo.ResSTDCode = 0;
                    if (resPhoneNum != "")
                        customerVo.ResPhoneNum = Int32.Parse(resPhoneNum);
                    else
                        customerVo.ResPhoneNum = 0;
                    
                    //**************************************************************************
                       
                    //**************************************************************************
                    //Fax residence
                    lenFaxNum = werpMFUploadsVo.Fax.Length;
                    if (lenFaxNum >= 8)
                    {
                        resFaxNum = werpMFUploadsVo.Fax.Substring(lenFaxNum - 8, 8);
                        if (lenFaxNum >= 11)
                        {
                            resFaxStdCode = werpMFUploadsVo.Fax.Substring(lenFaxNum - 11, 3);
                            if (lenFaxNum >= 12)
                                resFaxIsdCode = werpMFUploadsVo.Fax.Substring(0, lenFaxNum - 11);
                        }
                        else
                            resFaxStdCode = werpMFUploadsVo.Fax.Substring(0, lenFaxNum - 8);
                    }
                    else
                        resFaxNum = werpMFUploadsVo.Fax;
                    if (resFaxIsdCode != "")
                        customerVo.ISDFax = Int32.Parse(resFaxIsdCode);
                    else
                        customerVo.ISDFax = 0;
                    if (resFaxStdCode != "")
                        customerVo.STDFax = Int32.Parse(resFaxStdCode);
                    else
                        customerVo.STDFax = 0;
                    if (resFaxNum != "")
                        customerVo.Fax = Int32.Parse(resFaxNum);
                    else
                        customerVo.Fax = 0;
                    //**************************************************************************
                    //Fax Office
                    lenFaxNum = werpMFUploadsVo.OfcFax.Length;
                    if (lenFaxNum >= 8)
                    {
                        offFaxNum = werpMFUploadsVo.OfcFax.Substring(lenFaxNum - 8, 8);
                        if (lenFaxNum >= 11)
                        {
                            offFaxStdCode = werpMFUploadsVo.OfcFax.Substring(lenFaxNum - 11, 3);
                            if (lenFaxNum >= 12)
                                offFaxIsdCode = werpMFUploadsVo.OfcFax.Substring(0, lenFaxNum - 11);
                        }
                        else
                            offFaxStdCode = werpMFUploadsVo.OfcFax.Substring(0, lenFaxNum - 8);
                    }
                    else
                        offFaxNum = werpMFUploadsVo.OfcFax;
                    if (offFaxIsdCode != "")
                        customerVo.OfcISDFax = Int32.Parse(offFaxIsdCode);
                    else
                        customerVo.OfcISDFax = 0;
                    if (offFaxStdCode != "")
                        customerVo.OfcSTDFax = Int32.Parse(offFaxStdCode);
                    else
                        customerVo.OfcSTDFax = 0;
                    if (offFaxNum != "")
                        customerVo.OfcFax = Int32.Parse(offFaxNum);
                    else
                        customerVo.OfcFax = 0;
                            

                    //customerId2 = customerBo.CreateCustomer(customerVo, rmId, userId);

                    //customerPortfolioVo.CustomerId = customerId2;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    //PortfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userId);

                    customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, userId);

                    countCustCreated++;


                }
                UpdateWerpMFProfileStagingIsCustomerNew(adviserId, processId);

                //*****New Folios Upload from CAMS*****
                getNewFoliosDs = GetWerpMFProfileNewFolios(processId);
                getNewFoliosDt = getNewFoliosDs.Tables[0];
                foreach (DataRow dr in getNewFoliosDt.Rows)
                {
                    customerAccountsVo.AccountNum = dr["CMFXPS_FolioNum"].ToString();
                    customerAccountsVo.PortfolioId = Int32.Parse(dr["CP_PortfolioId"].ToString());
                    customerAccountsVo.AssetClass = "MF";
                    customerAccountsVo.AMCCode = Int32.Parse(dr["PA_AMCCode"].ToString());
                    customerAccountBo.CreateCustomerMFAccount(customerAccountsVo, userId);

                    countFolioCreated++;
                }
                UpdateWerpMFProfileStagingIsFolioNew(processId);
                IsProcessComplete = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadsBo.cs:WerpMFInsertCustomerDetails()");

                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = processId;
                objects[2] = rmId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Sixth Phase: Creation of new bank accounts
        public bool WerpMFCreationOfNewBankAccounts(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                string query1 = "UPDATE CustomerMFXtrnlProfileStaging SET CMFXPS_IsBankAccountNew = 0 " +
                                "FROM CustomerMFXtrnlProfileStaging A " +
                                "INNER JOIN dbo.CustomerBank B ON " +
                                "A.CMFXPS_AccountNum = B.CB_AccountNum AND A.C_CustomerId = B.C_CustomerId " +
                                "WHERE A.ADUL_ProcessId = " + processId + " AND CMFXPS_IsRejected = 0 ";
                Package werpMFProPkg4 = App.LoadPackage(Packagepath, null);
                werpMFProPkg4.Variables["varQueryBankAccountCheck"].Value = query1;
                werpMFProPkg4.Variables["varProcessIdBankAccountCreation"].Value = processId;
                werpMFProPkg4.Variables["varProcessIdDeleteStaging"].Value = processId;
                DTSExecResult werpMFProResult4 = werpMFProPkg4.Execute();
                if (werpMFProResult4.ToString() == "Success")
                    IsProcessComplete = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "WerpMFUploadsBo.cs:WerpMFCreationOfNewBankAccounts()");

                object[] objects = new object[2];
                objects[0] = processId;
                objects[1] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        ////Seventh phase: Insert good records into CAMS External Profile table
        //public bool WerpMFInsertExternalProfile(int processId, string Packagepath)
        //{
        //    bool IsProcessComplete = false;
        //    try
        //    {
        //        Package werpMFProPkg5 = App.LoadPackage(Packagepath, null);
        //        werpMFProPkg5.Variables["varProcessId"].Value = processId;
        //        DTSExecResult werpMFProResult5 = werpMFProPkg5.Execute();
        //        if (werpMFProResult5.ToString() == "Success")
        //            IsProcessComplete = true;
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "WerpMFUploadsBo.cs:WerpMFInsertExternalProfile()");

        //        object[] objects = new object[2];
        //        objects[0] = processId;
        //        objects[1] = Packagepath;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return IsProcessComplete;
        //}


    }
}
