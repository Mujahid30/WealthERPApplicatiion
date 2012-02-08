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
using BoAdvisorProfiling;
using BoCustomerProfiling;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.SqlServer;
using System.Data;
using System.Data.OleDb;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.SqlServer.Dts.Runtime;



namespace BoUploads
{
    public class CamsUploadsBo
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
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int userId;
        
        public List<CamsUploadsVo> GetCamsNewCustomers(int processId)
        {
            List<CamsUploadsVo> UploadsCustomerList = new List<CamsUploadsVo>();
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                UploadsCustomerList = CamsUploadsDao.GetCamsNewCustomers(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:GetCamsNewCustomers()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return UploadsCustomerList;
        }

        public bool UpdateProfileStagingIsCustomerNew(int adviserId,int processId)
        {
            bool result = false;
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                CamsUploadsDao.UpdateProfileStagingIsCustomerNew(adviserId,processId);
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

                FunctionInfo.Add("Method", "CamsUploadBo.cs:UpdateProfileStagingIsCustomerNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public DataSet GetCamsNewFolios(int processId)
        {
            DataSet getNewfolioDs = new DataSet();
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                getNewfolioDs = CamsUploadsDao.GetCamsNewFolios(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:GetCamsNewFolios()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getNewfolioDs;
        }


        public bool CreateCamsNewFolios(int portfolioId, string folioNum, int userId)
        {
            bool result = false;
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                CamsUploadsDao.createCamsNewFolios(portfolioId, folioNum, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:CreateCamsNewFolios()");

                object[] objects = new object[3];
                objects[0] = portfolioId;
                objects[1] = folioNum;
                objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool UpdateProfileStagingIsFolioNew(int processId)
        {
            bool result = false;
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                CamsUploadsDao.UpdateProfileStagingIsFolioNew(processId);
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

                FunctionInfo.Add("Method", "CamsUploadBo.cs:UpdateProfileStagingIsFolioNew()");

                object[] objects = new object[0];
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool IsFolioNew(string FolioNumber)
        {
            bool result = false;
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                result = CamsUploadsDao.IsFolioNew(FolioNumber);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:IsFolioNew()");

                object[] objects = new object[1];
                objects[0] = FolioNumber;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool IsPANNew(string PAN)
        {
            bool result = false;
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                result = CamsUploadsDao.IsPANNew(PAN);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:IsPANNew()");

                object[] objects = new object[1];
                objects[0] = PAN;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }
//*******************************************************************************************************************
        //Second phase: of the Upload; Insertion of CAMS Data from XML to Input table, Cleaning
        public bool CAMSInsertToInputProfile(int processId,string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;
            
            try
            {
                Package camsProPkg1 = App.LoadPackage(Packagepath, null);
                camsProPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                camsProPkg1.Variables["varProcessId"].Value = processId;
                camsProPkg1.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult1 = camsProPkg1.Execute();
                if (camsProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToInputProfile()");

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

        //Third Phase: Insert to CAMS staging table from input table and storing id
        public bool CAMSInsertToStagingProfile(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                
                Package camsProPkg2 = App.LoadPackage(Packagepath, null);
                camsProPkg2.Variables["varProcessId"].Value = processId;
                camsProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult2 = camsProPkg2.Execute();
                if (camsProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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

        //Fourth Phase: Checks and setting of flags in the CAMS staging table
        public bool CAMSProcessDataInStagingProfile(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package camsProPkg3 = App.LoadPackage(Packagepath, null);
                camsProPkg3.ImportConfigurationFile(configPath);
                camsProPkg3.Variables["varProcessId"].Value = processId;
                
                DTSExecResult camsProResult3 = camsProPkg3.Execute();
                if (camsProResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingProfile()");

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

        ////Fifth Phase: Move CAMS good data to Werp Tables
        //public bool CAMSInsertCustomerDetails(int adviserId, int processId, int rmId, out int countCustCreated, out int countFolioCreated)
        //{
        //    bool IsProcessComplete = false;
        //    List<CamsUploadsVo> camsNewCustomerList = new List<CamsUploadsVo>();
        //    Nullable<DateTime> dt = new DateTime();
        //    CamsUploadsVo camsUploadsVo = new CamsUploadsVo();
        //    string resIsdCode = "",resStdCode = "",resPhoneNum = "",offIsdCode="",offStdCode="",offPhoneNum="";
        //    int lenPhoneNum,customerId2;
        //    countCustCreated = 0;
        //    countFolioCreated = 0;
        //    DataSet getNewFoliosDs = new DataSet();
        //    DataTable getNewFoliosDt = new DataTable();
        //    userId = advisorStaffBo.GetUserId(rmId);
                
        //    try
        //    {
        //        camsNewCustomerList = GetCamsNewCustomers(processId);
        //        for (int i = 0; i < camsNewCustomerList.Count; i++)
        //        {
        //            customerVo = new CustomerVo();
        //            userVo = new UserVo();
        //            camsUploadsVo = new CamsUploadsVo();
        //            camsUploadsVo = camsNewCustomerList[i];

        //            userVo.FirstName = "";
        //            userVo.MiddleName = "";
        //            userVo.LastName = camsUploadsVo.InvestorName;
        //            //userVo.Password = id.Next(10000, 99999).ToString();
        //            userVo.Email = camsUploadsVo.Email;
        //            //userVo.LoginId = camsUploadsVo.Email;
        //            userVo.UserType = "Customer";

        //            //userId = userBo.CreateUser(userVo);

        //            customerVo.UserId = userId;
        //            customerVo.RmId = rmId;
        //            customerVo.ProcessId = processId;
        //            customerVo.Adr1City = camsUploadsVo.City;
        //            customerVo.Adr1Line1 = camsUploadsVo.Address1;
        //            customerVo.Adr1Line2 = camsUploadsVo.Address2;
        //            customerVo.Adr1Line3 = camsUploadsVo.Address3;
        //            customerVo.Adr1PinCode = Int32.Parse(camsUploadsVo.Pincode);
        //            customerVo.Email = camsUploadsVo.Email;
        //            customerVo.FirstName = "";
        //            customerVo.MiddleName = "";
        //            customerVo.LastName = camsUploadsVo.InvestorName;
        //            customerVo.LoginId = camsUploadsVo.Email;
        //            customerVo.Type = camsUploadsVo.Type;
        //            customerVo.SubType = camsUploadsVo.SubType;

        //            lenPhoneNum = camsUploadsVo.PhoneOffice.Length;
        //            if (lenPhoneNum >= 8)
        //            {
        //                offPhoneNum = camsUploadsVo.PhoneOffice.Substring(lenPhoneNum - 8, 8);
        //                if (lenPhoneNum >= 11)
        //                {
        //                    offStdCode = camsUploadsVo.PhoneOffice.Substring(lenPhoneNum - 11, 3);
        //                    if (lenPhoneNum >= 12)
        //                        offIsdCode = camsUploadsVo.PhoneOffice.Substring(0, lenPhoneNum - 11);
        //                }
        //                else
        //                    offStdCode = camsUploadsVo.PhoneOffice.Substring(0, lenPhoneNum - 8);
        //            }
        //            else
        //                offPhoneNum = camsUploadsVo.PhoneOffice;
                    

        //            if (offIsdCode != "")
        //                customerVo.ResISDCode = Int32.Parse(offIsdCode);
        //            else
        //                customerVo.ResISDCode = 0;
        //            if (offStdCode != "")
        //                customerVo.ResSTDCode = Int32.Parse(offStdCode);
        //            else
        //                customerVo.ResSTDCode = 0;
        //            if (offPhoneNum != "")
        //                customerVo.ResPhoneNum = Int32.Parse(offPhoneNum);
        //            else
        //                customerVo.ResPhoneNum = 0;

                    

        //            bool s = false;
        //            int temp = 0;
        //            s = Int32.TryParse(camsUploadsVo.PhoneOffice, out temp);
        //            if (s)
        //            {
        //                customerVo.OfcPhoneNum = temp;
        //            }
        //            else
        //                customerVo.OfcPhoneNum = 0;
                    
        //            customerVo.PANNum = camsUploadsVo.PANNumber;
        //            customerVo.Password = id.Next(10000, 99999).ToString();




        //            lenPhoneNum = camsUploadsVo.PhoneResidence.Length;
        //            if (lenPhoneNum >= 8)
        //            {
        //                resPhoneNum = camsUploadsVo.PhoneResidence.Substring(lenPhoneNum - 8, 8);
        //                if (lenPhoneNum >= 11)
        //                {
        //                    resStdCode = camsUploadsVo.PhoneResidence.Substring(lenPhoneNum - 11, 3);
        //                    if (lenPhoneNum >= 12)
        //                        resIsdCode = camsUploadsVo.PhoneResidence.Substring(0, lenPhoneNum - 11);
        //                }
        //                else
        //                    resStdCode = camsUploadsVo.PhoneResidence.Substring(0, lenPhoneNum - 8);
        //            }
        //            else
        //                resPhoneNum = camsUploadsVo.PhoneResidence;
        //            if (resIsdCode != "")
        //                customerVo.ResISDCode = Int32.Parse(resIsdCode);
        //            else
        //                customerVo.ResISDCode = 0;
        //            if (resStdCode != "")
        //                customerVo.ResSTDCode = Int32.Parse(resStdCode);
        //            else
        //                customerVo.ResSTDCode = 0;
        //            if (resPhoneNum != "")
        //                customerVo.ResPhoneNum = Int32.Parse(resPhoneNum);
        //            else
        //                customerVo.ResPhoneNum = 0;

        //            customerVo.Dob = DateTime.Parse(dt.ToString());
        //            customerVo.ProfilingDate = DateTime.Today;
        //            customerVo.RBIApprovalDate = DateTime.Parse(dt.ToString());
        //            customerVo.RegistrationDate = DateTime.Parse(dt.ToString());
        //            customerVo.CommencementDate = DateTime.Parse(dt.ToString());
                    
        //            //customerId2 = customerBo.CreateCustomer(customerVo, rmId, userId);

        //            //customerPortfolioVo.CustomerId = customerId2;
        //            customerPortfolioVo.IsMainPortfolio = 1;
        //            customerPortfolioVo.PortfolioTypeCode = "RGL";
        //            //PortfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userId);
        //            customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, userId);
        //            countCustCreated++;
        //        }
                
        //        UpdateProfileStagingIsCustomerNew(adviserId,processId);

        //        //*****New Folios Upload from CAMS*****
        //        getNewFoliosDs = GetCamsNewFolios(processId);
        //        getNewFoliosDt = getNewFoliosDs.Tables[0];
        //        foreach (DataRow dr in getNewFoliosDt.Rows)
        //        {
        //             customerAccountsVo.AccountNum= dr["CMGCXPS_FOLIOCHK"].ToString();
        //             customerAccountsVo.PortfolioId= Int32.Parse(dr["CP_PortfolioId"].ToString());
        //             customerAccountsVo.AssetClass = "MF";
        //             customerAccountsVo.AMCCode = Int32.Parse(dr["PA_AMCCode"].ToString());
        //             customerAccountBo.CreateCustomerMFAccount(customerAccountsVo, userId);
        //             countFolioCreated++;
        //        }
        //        UpdateProfileStagingIsFolioNew(processId);
        //        IsProcessComplete = true;    
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertCustomerDetails()");

        //        object[] objects = new object[3];
        //        objects[0] = adviserId;
        //        objects[1] = processId;
        //        objects[2] = rmId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return IsProcessComplete;
        //}

        ////Sixth phase: Insert good records into CAMS External Profile table
        //public bool CAMSInsertExternalProfile(int processId, string Packagepath, string configPath)
        //{
        //    bool IsProcessComplete = false;
        //    try
        //    {
        //        Package camsProPkg4 = App.LoadPackage(Packagepath, null);
        //        camsProPkg4.Variables["varProcessId"].Value = processId;
        //        camsProPkg4.Variables["varProcessIdDeleteStaging"].Value = processId;
        //        camsProPkg4.Configurations[0].ConfigurationString = configPath;
        //        DTSExecResult camsProResult4 = camsProPkg4.Execute();
        //        if (camsProResult4.ToString() == "Success")
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

        //        FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertExternalProfile()");

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

        //Transfer to common staging
        public bool CAMSInsertToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package camsProPkg4 = App.LoadPackage(Packagepath, null);
                camsProPkg4.Variables["varProcessId"].Value = processId;
                camsProPkg4.Variables["varXMLFileTypeId"].Value = 2;
                camsProPkg4.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult4 = camsProPkg4.Execute();
                if (camsProResult4.ToString() == "Success")
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdInsertToFirstStaging()");

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

        public bool SundaramInsertToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package camsProPkg4 = App.LoadPackage(Packagepath, null);
                camsProPkg4.Variables["varProcessId"].Value = processId;
                camsProPkg4.Variables["varXMLFileTypeId"].Value = 21;
                camsProPkg4.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult4 = camsProPkg4.Execute();
                if (camsProResult4.ToString() == "Success")
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdInsertToFirstStaging()");

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
    //Transfer the Folio details from CAMS main staging to the Common Folio Staging
        public bool CAMSInsertFolioDataToFolioCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package camsProPkg5 = App.LoadPackage(Packagepath, null);
                camsProPkg5.Variables["varProcessId"].Value = processId;
                camsProPkg5.Variables["varXMLFileTypeId"].Value = 2;
                camsProPkg5.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult5 = camsProPkg5.Execute();
                if (camsProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertFolioDataToFolioCommonStaging()");

                object[] objects = new object[2];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = configPath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        public bool SundaramInsertFolioDataToFolioCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package camsProPkg5 = App.LoadPackage(Packagepath, null);
                camsProPkg5.Variables["varProcessId"].Value = processId;
                camsProPkg5.Variables["varXMLFileTypeId"].Value = 21;
                camsProPkg5.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult5 = camsProPkg5.Execute();
                if (camsProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertFolioDataToFolioCommonStaging()");

                object[] objects = new object[2];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = configPath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }
        //******************Folio and Transaction uploads start************************\\
        public bool StandardFolioAndTrxInsertToInputTrans(int ProcessId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package camsTranPkg1 = App.LoadPackage(Packagepath, null);
                camsTranPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                camsTranPkg1.Variables["varMovetoExternal"].Value = ProcessId;

                camsTranPkg1.Variables["varSetRejectedZero"].Value = ProcessId;
                camsTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult1 = camsTranPkg1.Execute();
                if (camsTranResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToInputTrans()");

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


        public bool StandardInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package camsTranPkg2 = App.LoadPackage(Packagepath, null);
                camsTranPkg2.Variables["varCheckProcessId"].Value = processId;
                camsTranPkg2.Variables["varProcessId"].Value = processId;
                camsTranPkg2.Variables["varClrIpTablePrId"].Value = processId;
                camsTranPkg2.Variables["varSourceProcessId"].Value = processId;
                camsTranPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult2 = camsTranPkg2.Execute();
                if (camsTranResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingTrans()");

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


        public bool StandardProcessDataInStagingTrans(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package camsTranPkg3 = App.LoadPackage(Packagepath, null);
                camsTranPkg3.Variables["varCheckDataTransProcessId"].Value = processId;

                camsTranPkg3.Variables["varProcessId"].Value = processId;

                camsTranPkg3.Variables["varCheckDataTransProcessId"].Value = processId;

                camsTranPkg3.ImportConfigurationFile(configPath);
                //camsTranPkg3.Configurations[0].ConfigurationString = configPath;
                DTSExecResult camsTranResult3 = camsTranPkg3.Execute();
                if (camsTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

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
        //******************Folio and Transaction uploads End************************\\

//************************************************************************************************************************

        //Second phase: of the Upload; Insertion of Data from XML to Input table, Cleaning
        //Second phase: of the Upload; Insertion of Data from XML to Input table, Cleaning
        public bool CAMSInsertToInputTrans(int ProcessId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package camsTranPkg1 = App.LoadPackage(Packagepath, null);
                camsTranPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                camsTranPkg1.Variables["varMovetoExternal"].Value = ProcessId;
                
                camsTranPkg1.Variables["varSetRejectedZero"].Value = ProcessId;
                camsTranPkg1.Configurations[0].ConfigurationString = configPath;
                DTSExecResult camsTranResult1 = camsTranPkg1.Execute();
                if (camsTranResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToInputTrans()");

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

        //Third Phase: Insert to staging table from input table and storing id
        public bool CAMSInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package camsTranPkg2 = App.LoadPackage(Packagepath, null);
                camsTranPkg2.Variables["varCheckProcessId"].Value = processId;
                camsTranPkg2.Variables["varProcessId"].Value = processId;
                camsTranPkg2.Variables["varClrIpTablePrId"].Value = processId;
                camsTranPkg2.Variables["varSourceProcessId"].Value = processId;
                camsTranPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult camsTranResult2 = camsTranPkg2.Execute();
                if (camsTranResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingTrans()");

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

        //Fourth Phase: Checks and setting of flags in the first staging and Move data to sencond staging
        public bool CAMSProcessDataInStagingTrans(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package camsTranPkg3 = App.LoadPackage(Packagepath, null);
                camsTranPkg3.Variables["varCheckDataTransProcessId"].Value = processId;

                camsTranPkg3.Variables["varProcessId"].Value = processId;

                camsTranPkg3.Variables["varCheckDataTransProcessId"].Value = processId;

                camsTranPkg3.ImportConfigurationFile(configPath);
               //camsTranPkg3.Configurations[0].ConfigurationString = configPath;
                DTSExecResult camsTranResult3 = camsTranPkg3.Execute();
                if (camsTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

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


        public bool UploadsCAMSDataTranslationForReprocess(int processId)
        {
            bool result = false;
            CamsUploadsDao CamsUploadsDao = new CamsUploadsDao();
            try
            {
                result = CamsUploadsDao.UploadsCAMSDataTranslationForReprocess(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadBo.cs:UploadsCAMSDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }


       //---------------------------SIP Uploads-------------------------------------\\

        //--------------------------Start Standard Uploads---------------------------------\\
        public bool StandardSIPInsertToInputTrans(int processId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package standardProPkg1 = App.LoadPackage(Packagepath, null);
                standardProPkg1.Variables["varXMLFilePath"].Value = XMLFilepath;
                standardProPkg1.Variables["varProcessId"].Value = processId;
                standardProPkg1.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult1 = standardProPkg1.Execute();
                if (camsProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToInputProfile()");

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

        public bool StandardSIPInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package standardProPkg2 = App.LoadPackage(Packagepath, null);
                standardProPkg2.Variables["varProcessId"].Value = processId;
                standardProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult2 = standardProPkg2.Execute();
                if (camsProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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

        public bool StandardSIPProcessDataInStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package standardTranPkg3 = App.LoadPackage(Packagepath, null);

                standardTranPkg3.Variables["varProcessId"].Value = processId;
                standardTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult3 = standardTranPkg3.Execute();
                if (camsTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[2] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }


        public bool StandardSIPStagingToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package standardProPkg2 = App.LoadPackage(Packagepath, null);
                standardProPkg2.Variables["varProcessId"].Value = processId;
                standardProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult2 = standardProPkg2.Execute();
                if (camsProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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

        public bool StandardSIPCommonStagingChk(int processId, string Packagepath, string configPath, string UploadTypeShortForm)
        {
            bool IsProcessComplete = false;
            try
            {

                Package standardProPkg2 = App.LoadPackage(Packagepath, null);
                standardProPkg2.Variables["varProcessId"].Value = processId;
                standardProPkg2.Variables["varUploadTypeShortForm"].Value = UploadTypeShortForm;
                standardProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult2 = standardProPkg2.Execute();
                if (camsProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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



        public bool StandardSIPCommonStagingToWERP(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package standardTranPkg3 = App.LoadPackage(Packagepath, null);

                standardTranPkg3.Variables["varProcessId"].Value = processId;

                standardTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult standardTranResult3 = standardTranPkg3.Execute();
                if (standardTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[2] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }


        //-------------------------End Standard Uploads---------------------------------\\


        public bool CAMSSIPInsertToInputTrans(int processId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package camsProPkg1 = App.LoadPackage(Packagepath, null);
                camsProPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                camsProPkg1.Variables["varProcessId"].Value = processId;
                camsProPkg1.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult1 = camsProPkg1.Execute();
                if (camsProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToInputProfile()");

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


        public bool CAMSSIPInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package camsProPkg2 = App.LoadPackage(Packagepath, null);
                camsProPkg2.Variables["varProcessId"].Value = processId;
                camsProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult2 = camsProPkg2.Execute();
                if (camsProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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

        public bool CAMSSIPProcessDataInStagingTrans(int processId,string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package camsTranPkg3 = App.LoadPackage(Packagepath, null);

                camsTranPkg3.Variables["varProcessId"].Value = processId;
                camsTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult3 = camsTranPkg3.Execute();
                if (camsTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[2] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }


        public bool CamsSIPStagingToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package camsProPkg2 = App.LoadPackage(Packagepath, null);
                camsProPkg2.Variables["varProcessId"].Value = processId;
                camsProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult2 = camsProPkg2.Execute();
                if (camsProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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

        public bool CamsSIPCommonStagingChk(int processId, string Packagepath, string configPath, string UploadTypeShortForm)  
             
        {
            bool IsProcessComplete = false;
            try
            {

                Package camsProPkg2 = App.LoadPackage(Packagepath, null);
                camsProPkg2.Variables["varProcessId"].Value = processId;
                camsProPkg2.Variables["varUploadTypeShortForm"].Value = UploadTypeShortForm;
                camsProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult2 = camsProPkg2.Execute();
                if (camsProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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
            
       

        public bool CamsSIPCommonStagingToWERP(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package camsTranPkg3 = App.LoadPackage(Packagepath, null);

                camsTranPkg3.Variables["varProcessId"].Value = processId;
                
                camsTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult3 = camsTranPkg3.Execute();
                if (camsTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[2] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //***************************************************************************************

        public bool SundramInsertToInputProfile(int processId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package camsProPkg1 = App.LoadPackage(Packagepath, null);
                camsProPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                camsProPkg1.Variables["varProcessId"].Value = processId;
                camsProPkg1.ImportConfigurationFile(configPath);
                DTSExecResult camsProResult1 = camsProPkg1.Execute();
                if (camsProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:SundramInsertToInputProfile()");

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


        //--------------------------Start SIP KARVY Uploads---------------------------------\\
        public bool KarvySIPInsertToInputTrans(int processId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package standardProPkg1 = App.LoadPackage(Packagepath, null);
                standardProPkg1.Variables["varXMLFilePath"].Value = XMLFilepath;
                standardProPkg1.Variables["varProcessId"].Value = processId;
                standardProPkg1.ImportConfigurationFile(configPath);
                DTSExecResult karvyProResult1 = standardProPkg1.Execute();
                if (karvyProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToInputProfile()");

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

        public bool KarvySIPInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package standardProPkg2 = App.LoadPackage(Packagepath, null);
                standardProPkg2.Variables["varProcessId"].Value = processId;
                standardProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult karvyProResult2 = standardProPkg2.Execute();
                if (karvyProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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

        public bool KarvySIPProcessDataInStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package standardTranPkg3 = App.LoadPackage(Packagepath, null);

                standardTranPkg3.Variables["varProcessId"].Value = processId;
                standardTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult karvyTranResult3 = standardTranPkg3.Execute();
                if (karvyTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[2] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }


        public bool KarvySIPStagingToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package standardProPkg2 = App.LoadPackage(Packagepath, null);
                standardProPkg2.Variables["varProcessId"].Value = processId;
                standardProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult karvyProResult2 = standardProPkg2.Execute();
                if (karvyProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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

        public bool KarvySIPCommonStagingChk(int processId, string Packagepath, string configPath, string UploadTypeShortForm)
        {
            bool IsProcessComplete = false;
            try
            {

                Package standardProPkg2 = App.LoadPackage(Packagepath, null);
                standardProPkg2.Variables["varProcessId"].Value = processId;
                standardProPkg2.Variables["varUploadTypeShortForm"].Value = UploadTypeShortForm;
                standardProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult karvyProResult2 = standardProPkg2.Execute();
                if (karvyProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertToStagingProfile()");

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



        public bool KarvySIPCommonStagingToWERP(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package standardTranPkg3 = App.LoadPackage(Packagepath, null);

                standardTranPkg3.Variables["varProcessId"].Value = processId;

                standardTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult standardTranResult3 = standardTranPkg3.Execute();
                if (standardTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSProcessDataInStagingTrans()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[2] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }


        //-------------------------End SIP KARVY Uploads---------------------------------\\

    }
}
