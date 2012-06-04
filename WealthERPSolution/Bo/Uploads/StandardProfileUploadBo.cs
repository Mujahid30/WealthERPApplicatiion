using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using BoAdvisorProfiling;
using BoCustomerPortfolio;
using BoCustomerProfiling;
using BoUser;
using BoCommon;
using DaoUploads;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Dts.Runtime;
using VoCustomerPortfolio;
using VoUploads;
using VoUser;
using VoCustomerProfiling;

namespace BoUploads
{
    public class StandardProfileUploadBo
    {
        Microsoft.SqlServer.Dts.Runtime.Application App = new Microsoft.SqlServer.Dts.Runtime.Application();
        CustomerVo customerVo = new CustomerVo();
        CustomerBo customerBo = new CustomerBo();
        CustomerPortfolioVo customerPortfolioVo = new CustomerPortfolioVo();
        CustomerAccountsVo customerAccountsVo = new CustomerAccountsVo();
        CustomerAccountBo customerAccountBo = new CustomerAccountBo();
        PortfolioBo PortfolioBo = new PortfolioBo();
        StandardProfileUploadDao StandardProfileUploadsDao = new StandardProfileUploadDao();
        
        UserVo userVo = new UserVo();
        UserBo userBo = new UserBo();
        Random id = new Random();
        AdvisorStaffBo advisorStaffBo = new AdvisorStaffBo();
        int userId;
        List<int> customerIds = null;

        public List<StandardProfileUploadVo> GetProfileNewCustomers(int processId)
        {
            List<StandardProfileUploadVo> UploadsCustomerList = new List<StandardProfileUploadVo>();
            StandardProfileUploadDao StandardProfileUploadDao = new StandardProfileUploadDao();
            try
            {
                UploadsCustomerList = StandardProfileUploadDao.GetProfileNewCustomers(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:GetProfileNewCustomers()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return UploadsCustomerList;
        }

        public bool UpdateProfileStagingIsCustomerNew(int adviserId, int processId,int branchId)
        {
            bool result = false;
            
            try
            {
                StandardProfileUploadsDao.UpdateProfileStagingIsCustomerNew(adviserId, processId,branchId);
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:UpdateProfileStagingIsCustomerNew()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }


        //***************************************************************************************************************
        //Calling Packages

        public bool StdInsertToInputProfile(string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package stdProPkg1 = App.LoadPackage(Packagepath, null);
               // XMLFilepath = XMLFilepath.Replace("2169", "2168");
                stdProPkg1.Variables["varXmlFilePath"].Value = XMLFilepath;

                stdProPkg1.ImportConfigurationFile(configPath);
                //stdProPkg1.Configurations[0].ConfigurationString = configPath;
                DTSExecResult stdProResult1 = stdProPkg1.Execute();
                if (stdProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdInsertToInputProfile()");

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

        //Third Phase: Insert to Std First staging table from Std input table
        public bool StdInsertToFirstStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package stdProPkg2 = App.LoadPackage(Packagepath, null);
                stdProPkg2.Variables["varProcessId"].Value = processId;
                stdProPkg2.ImportConfigurationFile(configPath);
                //stdProPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult stdProResult2 = stdProPkg2.Execute();
                if (stdProResult2.ToString() == "Success")
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

        //Fourth Phase: Data translation check in std first staging
        public bool StdDataTranslationCheckInFirstStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package stdProPkg3 = App.LoadPackage(Packagepath, null);
                stdProPkg3.ImportConfigurationFile(configPath);
                //stdProPkg3.Configurations[0].ConfigurationString = configPath;
                stdProPkg3.Variables["varProcessId"].Value = processId;

                DTSExecResult stdProResult3 = stdProPkg3.Execute();
                if (stdProResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdDataTranslationCheckInFirstStaging()");

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Fifthth Phase: Insert to Common Profile staging from std profil input
        public bool StdInsertToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package stdProPkg4 = App.LoadPackage(Packagepath, null);
                stdProPkg4.Variables["varProcessId"].Value = processId;
                stdProPkg4.ImportConfigurationFile(configPath);
                //stdProPkg2.Configurations[0].ConfigurationString = configPath;
                stdProPkg4.Variables["varXMLFileTypeId"].Value = 7;
                DTSExecResult stdProResult4 = stdProPkg4.Execute();
                if (stdProResult4.ToString() == "Success")
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
        //Vishal
        public bool StdFolioStaggingToWERP(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package stdProPkg5 = App.LoadPackage(Packagepath, null);
                stdProPkg5.ImportConfigurationFile(configPath);
                //stdProPkg5.Configurations[0].ConfigurationString = configPath;
                stdProPkg5.Variables["varProcessId"].Value = processId;
               
                stdProPkg5.Variables["varAdviserId"].Value = AdviserId;
               

                DTSExecResult stdProResult5 = stdProPkg5.Execute();
                if (stdProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdCommonProfileChecks()");

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
        //Sixth Phase: Common Profile Checks
        public bool StdCommonProfileChecks(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package stdProPkg5 = App.LoadPackage(Packagepath, null);
                stdProPkg5.ImportConfigurationFile(configPath);
                //stdProPkg5.Configurations[0].ConfigurationString = configPath;
                stdProPkg5.Variables["varProcessId"].Value = processId;
                stdProPkg5.Variables["varProcessId"].Value = processId;
                stdProPkg5.Variables["varProcessId"].Value = processId;
                stdProPkg5.Variables["varProcessId"].Value = processId;
                stdProPkg5.Variables["varProcessId"].Value = processId;
                stdProPkg5.Variables["varAdviserId"].Value = AdviserId;
                stdProPkg5.Variables["varAdviserId"].Value = AdviserId;
                stdProPkg5.Variables["varAdviserId"].Value = AdviserId;

                DTSExecResult stdProResult5 = stdProPkg5.Execute();
                if (stdProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdCommonProfileChecks()");

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

        //Seventh phase: Profile creation
        public bool StdInsertCustomerDetails(int adviserId, int processId, int rmId,int branchId,string xmlPath, out int countCustCreated)
        {
            bool IsProcessComplete = false;
            List<StandardProfileUploadVo> stdNewCustomerList = new List<StandardProfileUploadVo>();
            Nullable<DateTime> dt = new DateTime();
            StandardProfileUploadVo StandardProfileUploadVo = new StandardProfileUploadVo();
            countCustCreated = 0;
            DataSet getNewFoliosDs = new DataSet();
            DataTable getNewFoliosDt = new DataTable();
            userId = advisorStaffBo.GetUserId(rmId);
            int lenPhoneNum = 0, lenFaxNum = 0;
            string resIsdCode = "", resStdCode = "", resPhoneNum = "", offIsdCode = "", offStdCode = "", offPhoneNum = "";
            string resFaxIsdCode = "", resFaxStdCode = "", resFaxNum = "", offFaxIsdCode = "", offFaxStdCode = "", offFaxNum = "";

            try
            {
                stdNewCustomerList = GetProfileNewCustomers(processId);
                for (int i = 0; i < stdNewCustomerList.Count; i++)
                {
                    customerVo = new CustomerVo();
                    userVo = new UserVo();
                    StandardProfileUploadVo = new StandardProfileUploadVo();
                    StandardProfileUploadVo = stdNewCustomerList[i];

                    userVo.FirstName = StandardProfileUploadVo.FirstName;
                    userVo.MiddleName = StandardProfileUploadVo.MiddleName;
                    userVo.LastName = StandardProfileUploadVo.LastName;

                    userVo.Email = StandardProfileUploadVo.Email ;
                    userVo.UserType = "Customer";

                    customerVo.UserId = userId;
                    customerVo.RmId = StandardProfileUploadsDao.GetBranchHeadId(branchId);
                    customerVo.BranchId = branchId;
                    customerVo.ProcessId = processId;
                    customerVo.Adr1City = StandardProfileUploadVo.Adr1City ;
                    customerVo.Adr1Line1 = StandardProfileUploadVo.Adr1Line1;
                    customerVo.Adr1Line2 = StandardProfileUploadVo.Adr1Line2;
                    customerVo.Adr1Line3 = StandardProfileUploadVo.Adr1Line3;
                    if(StandardProfileUploadVo.Adr1PinCode != "")
                        customerVo.Adr1PinCode = Int32.Parse(StandardProfileUploadVo.Adr1PinCode);
                    customerVo.Email = StandardProfileUploadVo.Email;
                    customerVo.FirstName = StandardProfileUploadVo.FirstName;
                    customerVo.MiddleName = StandardProfileUploadVo.MiddleName;
                    customerVo.LastName = StandardProfileUploadVo.LastName;
                    customerVo.Gender = StandardProfileUploadVo.Gender;
                    customerVo.Salutation = StandardProfileUploadVo.Salutation;
                    customerVo.Adr1City = StandardProfileUploadVo.Adr1City;
                    customerVo.Adr1Country = StandardProfileUploadVo.Adr1Country;
                    customerVo.Adr1State = XMLBo.GetStateCode(xmlPath,StandardProfileUploadVo.Adr1State);
                    customerVo.Adr2City = StandardProfileUploadVo.Adr2City;
                    customerVo.Adr2Country = StandardProfileUploadVo.Adr2Country;
                    customerVo.Adr2Line1 = StandardProfileUploadVo.Adr2Line1;
                    customerVo.Adr2Line2 = StandardProfileUploadVo.Adr2Line2;
                    customerVo.Adr2Line3 = StandardProfileUploadVo.Adr2Line3;
                    if (StandardProfileUploadVo.Adr2PinCode != "")
                        customerVo.Adr2PinCode = Int32.Parse( StandardProfileUploadVo.Adr2PinCode);
                    customerVo.Adr2State = StandardProfileUploadVo.Adr2State;
                    customerVo.AltEmail = StandardProfileUploadVo.AltEmail;
                    customerVo.AssignedRM =  (customerVo.RmId).ToString();
                    customerVo.CompanyName = StandardProfileUploadVo.CompanyName;
                    customerVo.CompanyWebsite = StandardProfileUploadVo.CompanyWebsite;
                    customerVo.ContactFirstName = StandardProfileUploadVo.ContactGuardianFirstName;
                    customerVo.ContactMiddleName = StandardProfileUploadVo.ContactGuardianMiddleName;
                    customerVo.ContactLastName = StandardProfileUploadVo.ContactGuardianLastName;
                    if(StandardProfileUploadVo.Mobile1 != "")
                        customerVo.Mobile1 = Int64.Parse(StandardProfileUploadVo.Mobile1);
                    if (StandardProfileUploadVo.Mobile2 != "")
                        customerVo.Mobile2 = Int64.Parse(StandardProfileUploadVo.Mobile2);
                    if(StandardProfileUploadVo.Nationality != "")
                        customerVo.Nationality = StandardProfileUploadVo.Nationality;
                    if(StandardProfileUploadVo.Occupation != "")
                        customerVo.Occupation = StandardProfileUploadVo.Occupation;
                    if (StandardProfileUploadVo.MaritalStatus != "")
                        customerVo.MaritalStatus = StandardProfileUploadVo.MaritalStatus;
                    if (StandardProfileUploadVo.Qualification != "")
                        customerVo.Qualification = StandardProfileUploadVo.Qualification;


                    customerVo.IsProspect = StandardProfileUploadVo.IsProspect;
                    customerVo.OfcAdrCity = StandardProfileUploadVo.OfcAdrCity;
                    customerVo.OfcAdrCountry = StandardProfileUploadVo.OfcAdrCountry;
                    customerVo.OfcAdrLine1 = StandardProfileUploadVo.OfcAdrLine1;
                    customerVo.OfcAdrLine2 = StandardProfileUploadVo.OfcAdrLine2;
                    customerVo.OfcAdrLine3 = StandardProfileUploadVo.OfcAdrLine3;
                    if (StandardProfileUploadVo.OfcAdrPinCode != "")
                        customerVo.OfcAdrPinCode = Int32.Parse(StandardProfileUploadVo.OfcAdrPinCode);
                    customerVo.OfcAdrState = StandardProfileUploadVo.OfcAdrState;
                    if(StandardProfileUploadVo.RBIApprovalDate != "") 
                        customerVo.RBIApprovalDate = DateTime.Parse(StandardProfileUploadVo.RBIApprovalDate);
                    
                    customerVo.RBIRefNum = StandardProfileUploadVo.RBIRefNum;
                    customerVo.RegistrationNum = StandardProfileUploadVo.RegistrationNum;
                    //customerVo.LoginId = StandardProfileUploadVo.Email;

                    if (StandardProfileUploadVo.Type != "")
                        customerVo.Type = StandardProfileUploadVo.Type;
                    if (StandardProfileUploadVo.SubType != "")
                        customerVo.SubType = StandardProfileUploadVo.SubType;


                    lenPhoneNum = StandardProfileUploadVo.OfcPhoneNum.Length;
                    if (lenPhoneNum > 9)
                    {
                        if (lenPhoneNum >= 8)
                        {
                            offPhoneNum = StandardProfileUploadVo.OfcPhoneNum.Substring(lenPhoneNum - 8, 8);
                            if (lenPhoneNum >= 11)
                            {
                                offStdCode = StandardProfileUploadVo.OfcPhoneNum.Substring(lenPhoneNum - 11, 3);
                                if (lenPhoneNum >= 12)
                                    offIsdCode = StandardProfileUploadVo.OfcPhoneNum.Substring(0, lenPhoneNum - 11);
                            }
                            else
                                offStdCode = StandardProfileUploadVo.OfcPhoneNum.Substring(0, lenPhoneNum - 8);
                        }
                        else
                            offPhoneNum = StandardProfileUploadVo.OfcPhoneNum;
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
                    }
                    else
                    {
                        if (StandardProfileUploadVo.OfcISDCode != "")
                            customerVo.OfcISDCode = Int32.Parse(StandardProfileUploadVo.OfcISDCode);
                        if (StandardProfileUploadVo.OfcSTDCode != "")
                            customerVo.OfcSTDCode = Int32.Parse(StandardProfileUploadVo.OfcSTDCode);
                        if (StandardProfileUploadVo.OfcPhoneNum != "")
                            customerVo.OfcPhoneNum = Int32.Parse(StandardProfileUploadVo.OfcPhoneNum);
                    }



                    lenPhoneNum = StandardProfileUploadVo.ResPhoneNum.Length;
                    if (lenPhoneNum > 9)
                    {
                        if (lenPhoneNum >= 8)
                        {
                            resPhoneNum = StandardProfileUploadVo.ResPhoneNum.Substring(lenPhoneNum - 8, 8);
                            if (lenPhoneNum >= 11)
                            {
                                resStdCode = StandardProfileUploadVo.ResPhoneNum.Substring(lenPhoneNum - 11, 3);
                                if (lenPhoneNum >= 12)
                                    resIsdCode = StandardProfileUploadVo.ResPhoneNum.Substring(0, lenPhoneNum - 11);
                            }
                            else
                                resStdCode = StandardProfileUploadVo.ResPhoneNum.Substring(0, lenPhoneNum - 8);
                        }
                        else
                            resPhoneNum = StandardProfileUploadVo.ResPhoneNum;


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
                    }
                    else
                    {

                        if (StandardProfileUploadVo.ResISDCode != "")
                            customerVo.ResISDCode = Int32.Parse(StandardProfileUploadVo.ResISDCode);
                        if (StandardProfileUploadVo.ResSTDCode != "")
                            customerVo.ResSTDCode = Int32.Parse(StandardProfileUploadVo.ResSTDCode);
                        if (StandardProfileUploadVo.ResPhoneNum != "")
                            customerVo.ResPhoneNum = Int32.Parse(StandardProfileUploadVo.ResPhoneNum);
                    }

                    lenFaxNum = StandardProfileUploadVo.OfcFax.Length;
                    if (lenFaxNum > 9)
                    {
                        
                        if (lenFaxNum >= 8)
                        {
                            offFaxNum = StandardProfileUploadVo.OfcFax.Substring(lenFaxNum - 8, 8);
                            if (lenFaxNum >= 11)
                            {
                                offFaxStdCode = StandardProfileUploadVo.OfcFax.Substring(lenFaxNum - 11, 3);
                                if (lenFaxNum >= 12)
                                    offFaxIsdCode = StandardProfileUploadVo.OfcFax.Substring(0, lenFaxNum - 11);
                            }
                            else
                                offFaxStdCode = StandardProfileUploadVo.OfcFax.Substring(0, lenFaxNum - 8);
                        }
                        else
                            offFaxNum = StandardProfileUploadVo.OfcFax;
                        if (offFaxIsdCode != "")
                            customerVo.ISDFax = Int32.Parse(offFaxIsdCode);
                        else
                            customerVo.ISDFax = 0;
                        if (offFaxStdCode != "")
                            customerVo.STDFax = Int32.Parse(offFaxStdCode);
                        else
                            customerVo.STDFax = 0;
                        if (offFaxNum != "")
                            customerVo.Fax = Int32.Parse(offFaxNum);
                        else
                            customerVo.Fax = 0;
                    }
                    else
                    {
                        if (StandardProfileUploadVo.OfcFaxISD != "")
                            customerVo.OfcISDFax = Int32.Parse(StandardProfileUploadVo.OfcFaxISD);
                        if (StandardProfileUploadVo.OfcFaxSTD != "")
                            customerVo.OfcSTDFax = Int32.Parse(StandardProfileUploadVo.OfcFaxSTD);
                        if (StandardProfileUploadVo.OfcFax != "")
                            customerVo.OfcFax = Int32.Parse(StandardProfileUploadVo.OfcFax);
                    }


                    lenFaxNum = StandardProfileUploadVo.Fax.Length;
                    if (lenFaxNum > 9)
                    {
                        if (lenFaxNum >= 8)
                        {
                            resFaxNum = StandardProfileUploadVo.Fax.Substring(lenFaxNum - 8, 8);
                            if (lenFaxNum >= 11)
                            {
                                resFaxStdCode = StandardProfileUploadVo.Fax.Substring(lenFaxNum - 11, 3);
                                if (lenFaxNum >= 12)
                                    resFaxIsdCode = StandardProfileUploadVo.Fax.Substring(0, lenFaxNum - 11);
                            }
                            else
                                resFaxStdCode = StandardProfileUploadVo.Fax.Substring(0, lenFaxNum - 8);
                        }
                        else
                            resFaxNum = StandardProfileUploadVo.Fax;
                        if (resFaxIsdCode != "")
                            customerVo.ISDFax = Int32.Parse(resFaxIsdCode);
                        else
                            customerVo.ISDFax = 0;
                        if (offFaxStdCode != "")
                            customerVo.STDFax = Int32.Parse(resFaxStdCode);
                        else
                            customerVo.STDFax = 0;
                        if (offFaxNum != "")
                            customerVo.Fax = Int32.Parse(resFaxNum);
                        else
                            customerVo.Fax = 0;
                    }
                    else
                    {
                        if (StandardProfileUploadVo.ISDFax != "")
                            customerVo.OfcISDFax = Int32.Parse(StandardProfileUploadVo.ISDFax);
                        if (StandardProfileUploadVo.STDFax != "")
                            customerVo.OfcSTDFax = Int32.Parse(StandardProfileUploadVo.STDFax);
                        if (StandardProfileUploadVo.Fax != "")
                            customerVo.OfcFax = Int32.Parse(StandardProfileUploadVo.Fax);
                    }


                    customerVo.ViaSMS = 1;
                    customerVo.PANNum = StandardProfileUploadVo.PANNum;
                    customerVo.Password = id.Next(10000, 99999).ToString();
                    if(StandardProfileUploadVo.DOB != "")
                        customerVo.Dob = DateTime.Parse(StandardProfileUploadVo.DOB);
                    customerVo.ProfilingDate = DateTime.Today;
                    if(StandardProfileUploadVo.RegistrationDate != "")
                        customerVo.RegistrationDate = DateTime.Parse(StandardProfileUploadVo.RegistrationDate);
                    if(StandardProfileUploadVo.CommencementDate != "")
                        customerVo.CommencementDate = DateTime.Parse(StandardProfileUploadVo.CommencementDate);
                    
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    customerPortfolioVo.PortfolioName = "MyPortfolio";
                    customerIds = customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, userId);

                    //Creating Customer Association
                    if (customerIds != null)
                    {
                        CustomerFamilyVo familyVo = new CustomerFamilyVo();
                        CustomerFamilyBo familyBo = new CustomerFamilyBo();
                        familyVo.AssociateCustomerId = customerIds[1];
                        familyVo.CustomerId = customerIds[1];
                        familyVo.Relationship = "SELF";
                        familyBo.CreateCustomerFamily(familyVo, customerIds[1], userVo.UserId);
                    }

                    countCustCreated++;

                }

                UpdateProfileStagingIsCustomerNew(adviserId, processId,branchId);
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdInsertCustomerDetails()");

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

        public bool StdDeleteCommonStaging(int processId)
        {

            bool result = false;
            StandardProfileUploadDao StandardProfileUploadsDao = new StandardProfileUploadDao();
            try
            {
                StandardProfileUploadsDao.StdDeleteCommonStaging(processId);
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdDeleteCommonStaging()");

                object[] objects = new object[0];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool StdCreationOfNewBankAccounts(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {


                Package stdProPkg5 = App.LoadPackage(Packagepath, null);
                stdProPkg5.Variables["varProcessId"].Value = processId;
                stdProPkg5.ImportConfigurationFile(configPath);
                DTSExecResult stdProResult5 = stdProPkg5.Execute();
                if (stdProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "StandardProfileUploadBo.cs:StdCreationOfNewBankAccounts()");

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

    }
}
