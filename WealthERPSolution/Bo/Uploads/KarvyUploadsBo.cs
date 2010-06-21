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
    public class KarvyUploadsBo
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

        public List<KarvyUploadsVo> GetKarvyNewCustomers(int processId  )
        {
            List<KarvyUploadsVo> UploadsCustomerList = new List<KarvyUploadsVo>();

            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            UploadsCustomerList = KarvyUploadsDao.GetKarvyNewCustomers(processId);
            
            return UploadsCustomerList;
        }

        public List<KarvyUploadsVo> GetKarvyProfNewCustomers(int processId)
        {
            List<KarvyUploadsVo> UploadsCustomerList = new List<KarvyUploadsVo>();

            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            UploadsCustomerList = KarvyUploadsDao.GetKarvyProfNewCustomers(processId);

            return UploadsCustomerList;
        }

        public bool UpdateCombinationStagingIsCustomerNew()
        {
            bool result = false;
            KarvyUploadsDao KarvyUploadsDao =new KarvyUploadsDao();
            KarvyUploadsDao.UpdateCombinationStagingIsCustomerNew();
            result = true;
            return result;
        }

        public bool UpdateKarvyProfileStagingIsCustomerNew(int adviserId,int processId)
        {
            bool result = false;
            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            KarvyUploadsDao.UpdateKarvyProfileStagingIsCustomerNew(adviserId,processId);
            result = true;
            return result;
        }
        
        public DataSet GetKarvyNewFolios(int adviserId)
        {
            DataSet getNewfolioDs = new DataSet();
            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            getNewfolioDs = KarvyUploadsDao.GetKarvyNewFolios(adviserId);

            return getNewfolioDs;
        }

        public DataSet GetKarvyProfileNewFolios(int processId)
        {
            DataSet getNewfolioDs = new DataSet();

            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            getNewfolioDs = KarvyUploadsDao.GetKarvyProfileNewFolios(processId);

            return getNewfolioDs;
        }

        public bool CreateNewFolios(int portfolioId, string folioNum, int userId)
        {
            bool result = false;
            KarvyUploadsDao karvyUploadsDao = new KarvyUploadsDao();
            try
            {
                karvyUploadsDao.createNewFolios(portfolioId, folioNum, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:CreateNewFolios()");


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

        public bool UpdateCombinationStagingIsFolioNew()
        {
            bool result = false;
            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            KarvyUploadsDao.UpdateCombinationStagingIsFolioNew();
            result = true;
            return result;
        }

        public bool UpdateKarvyProfileStagingIsFolioNew(int processId)
        {
            bool result = false;
            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            KarvyUploadsDao.UpdateKarvyProfileStagingIsFolioNew(processId);
            result = true;
            return result;
        }

       
        //Second phase: of the Upload; Insertion of Data from XML to Input table, Cleaning
        public bool KarvyInsertToInputTrans(int ProcessId,string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package karvyTranPkg1 = App.LoadPackage(Packagepath, null);
                karvyTranPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                karvyTranPkg1.Variables["varChecklength"].Value = ProcessId;
                karvyTranPkg1.Variables["varMovToXtrnl"].Value = ProcessId;
                karvyTranPkg1.Variables["varSetIpValsZero"].Value = ProcessId;
                karvyTranPkg1.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyTranResult1 = karvyTranPkg1.Execute();
                if (karvyTranResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:KarvyInsertToInputTrans()");

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
        public bool KarvyInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package karvyTranPkg2 = App.LoadPackage(Packagepath, null);
                karvyTranPkg2.Variables["varChkLengthPrId"].Value = processId;
                karvyTranPkg2.Variables["varDelInputValuesPrId"].Value = processId;
                karvyTranPkg2.Variables["varProcessId"].Value = processId;
                karvyTranPkg2.Variables["varRoundOffPrId"].Value = processId;
                karvyTranPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyTranResult2 = karvyTranPkg2.Execute();
                if (karvyTranResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:KarvyInsertToStagingTrans()");

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

        //Fourth Phase: Checks and setting of flags in the staging
        public bool KarvyProcessDataInStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                
                Package karvyTranPkg3 = App.LoadPackage(Packagepath, null);
                karvyTranPkg3.Variables["varDataTransProcessId"].Value = processId;
                karvyTranPkg3.Variables["varProcessId"].Value = processId;
                karvyTranPkg3.Variables["varDeleteStagingPrId"].Value = processId;
               
                karvyTranPkg3.Configurations[0].ConfigurationString = configPath;

                DTSExecResult karvyTranResult3 = karvyTranPkg3.Execute();
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

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:KarvyProcessDataInStagingTrans()");

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
       




//****************************************************************************************************************************
        //
//******************************************************************************************************************************





        //Second phase: of the Karvy Profile Upload; Insertion of KARVY Data from XML to Input table, Cleaning (IN USE)
        public bool KARVYInsertToInputProfile(string Packagepath,int processId, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package karvyProPkg1 = App.LoadPackage(Packagepath, null);
                karvyProPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                karvyProPkg1.Variables["varProcessId"].Value = processId;
                karvyProPkg1.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyProResult1 = karvyProPkg1.Execute();
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

                FunctionInfo.Add("Method", "KavyUploadsBo.cs:KARVYInsertToInputProfile()");

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

        //Third Phase: Insert to KARVY Staging table from Input table (IN USE)
        public bool KARVYInsertToStagingProfile(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package karvyProPkg2 = App.LoadPackage(Packagepath, null);
                karvyProPkg2.Variables["varProcessId"].Value = processId;
                karvyProPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyProResult2 = karvyProPkg2.Execute();
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYInsertToStagingProfile()");

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

        //Checking if Data Translation will take place and Flaging if not (IN USE)
        public bool KARVYStagingDataTranslationCheck(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package karvyProPkg2 = App.LoadPackage(Packagepath, null);
                karvyProPkg2.Variables["varProcessId"].Value = processId;
                karvyProPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyProResult2 = karvyProPkg2.Execute();
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYStagingDataTranslationCheck()");

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

        //Inserting Karvy Staging Data to Profile Staging (IN USE)
        public bool KARVYStagingInsertToProfileStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package karvyProPkg2 = App.LoadPackage(Packagepath, null);
                karvyProPkg2.Variables["varProcessId"].Value = processId;
                karvyProPkg2.Variables["varProcessId1"].Value = processId;
                karvyProPkg2.Variables["varXMLFileType"].Value = 4;
                karvyProPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyProResult2 = karvyProPkg2.Execute();
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYStagingInsertToProfileStaging()");

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

        //Inserting Karvy Staging Data to Folio Staging (IN USE)
        public bool KARVYStagingInsertToFolioStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package karvyProPkg2 = App.LoadPackage(Packagepath, null);
                karvyProPkg2.Variables["varProcessId"].Value = processId;
                karvyProPkg2.Variables["varProcessId1"].Value = processId;
                karvyProPkg2.Variables["varXMLFileType"].Value = 4;
                karvyProPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyProResult2 = karvyProPkg2.Execute();
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYStagingInsertToFolioStaging()");

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

        //Fourth Phase: Checks and setting of flags in the Karvy staging table
        public bool KARVYProcessDataInStagingProfile(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                string query1 = "UPDATE CustomerMFKarvyXtrnlProfileStaging SET CMFKXPS_IsCustomerNew = 0 , C_CustomerId = B.C_CustomerId , CP_PortfolioId = D.CP_PortfolioId "+
                                "FROM CustomerMFKarvyXtrnlProfileStaging A "+
                                "INNER JOIN Customer B "+
                                "ON A.CMFKXPS_PANNumber = B.C_PANNum "+
                                "INNER JOIN CustomerPortfolio D "+
                                "ON B.C_CustomerId = D.C_CustomerId "+
                                "INNER JOIN AdviserRM C "+
                                "ON B.AR_RMId = C.AR_RMId "+
                                "WHERE C.A_AdviserId = "+ AdviserId +" and A.ADUL_ProcessId = "+ processId +" and D.CP_IsMainPortfolio = 1" ;
                string query2 = "UPDATE CustomerMFKarvyXtrnlProfileStaging SET CMFKXPS_IsFolioNew = 0 , CMFA_AccountId = B.CMFA_AccountId " +
                                "FROM CustomerMFKarvyXtrnlProfileStaging A " +
                                "INNER JOIN CustomerMutualFundAccount B " +
                                "ON A.CMFKXPS_Folio = B.CMFA_FolioNum AND A.PA_AMCCode = B.PA_AMCCode " +
                                "INNER JOIN CustomerPortfolio C " +
                                "ON B.CP_PortfolioId = C.CP_PortfolioId " +
                                "INNER JOIN Customer D " +
                                "ON C.C_CustomerId = D.C_CustomerId " +
                                "INNER JOIN AdviserRM E " +
                                "ON D.AR_RMId = E.AR_RMId " +
                                "WHERE  E.A_AdviserId =" + AdviserId + " and A.ADUL_ProcessID =" + processId;
                string query3 = "UPDATE CustomerMFKarvyXtrnlProfileStaging set PA_AMCCode = C.PA_AMCCode " +
                                "FROM CustomerMFKarvyXtrnlProfileStaging INNER JOIN ProductAMCSchemeMapping B ON " +
                                "CMFKXPS_ProductCode = B.PASC_AMC_ExternalCode " +
                                "INNER JOIN ProductAMCSchemePlan C ON " +
                                "B.PASP_SchemePlanCode=C.PASP_SchemePlanCode " +
                                "where ( CMFKXPS_IsRejected = 0 and ADUL_ProcessId = " + processId + " ) ";

                Package karvyProPkg3 = App.LoadPackage(Packagepath, null);
                karvyProPkg3.Variables["varQueryCustomerCheck"].Value = query1;
                karvyProPkg3.Variables["varQueryFolioCheck"].Value = query2;
                karvyProPkg3.Variables["varQueryAMCCheck"].Value = query3;
                karvyProPkg3.Variables["varProcessIdPanNullCheck"].Value = processId;
                karvyProPkg3.Variables["varProcessIdAMCCheck"].Value = processId;
                karvyProPkg3.Variables["varProcessIdUpdateAMCFlag"].Value = processId;
                karvyProPkg3.Variables["varProcessIdBrokerCodeCheck"].Value = processId;
                karvyProPkg3.Variables["varProcessIdBankAccountTypeTranslatorCheck"].Value = processId;
                karvyProPkg3.Variables["varProcessIdBankModeOfHoldingTranslatorCheck"].Value = processId;
                karvyProPkg3.Variables["varProcessIdCustomerTypeTranslatorCheck"].Value = processId;
                karvyProPkg3.Variables["varProcessIdOccupationCodeTranslatorCheck"].Value = processId;
                karvyProPkg3.Configurations[0].ConfigurationString = configPath;

                DTSExecResult karvyProResult3 = karvyProPkg3.Execute();
                if (karvyProResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYProcessDataInStagingProfile()");

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

        //Fifth Phase: Move KARVY customer details to customer table
        public bool KARVYInsertCustomerDetails(int adviserId, int processId, int rmId, out int countCustCreated, out int countFolioCreated)
        {
            bool IsProcessComplete = false;
            List<KarvyUploadsVo> karvyNewCustomerList = new List<KarvyUploadsVo>();
            Nullable<DateTime> dt = new DateTime();
            KarvyUploadsVo karvyUploadsVo = new KarvyUploadsVo();
            string resIsdCode = "", resStdCode = "", resPhoneNum = "";
            string offIsdCode = "", offStdCode = "", offPhoneNum = "";
            string resFaxIsdCode = "", resFaxStdCode = "", resFaxNum = "";
            string offFaxIsdCode = "", offFaxStdCode = "", offFaxNum = "";
            int lenPhoneNum,lenFaxNum;
            countCustCreated = 0;
            countFolioCreated = 0;
            DataSet getNewFoliosDs = new DataSet();
            DataTable getNewFoliosDt = new DataTable();
            userId = advisorStaffBo.GetUserId(rmId);

            try
            {
                karvyNewCustomerList = GetKarvyProfNewCustomers(processId);
                for (int i = 0; i < karvyNewCustomerList.Count; i++)
                {
                    customerVo = new CustomerVo();
                    userVo = new UserVo();
                    karvyUploadsVo = new KarvyUploadsVo();

                    karvyUploadsVo = karvyNewCustomerList[i];
                    userVo.FirstName = "";
                    userVo.MiddleName = "";
                    userVo.LastName = karvyUploadsVo.InvestorName;
                    //userVo.Password = id.Next(10000, 99999).ToString();
                    userVo.Email = karvyUploadsVo.Email;
                    //userVo.LoginId = karvyUploadsVo.Email;
                    userVo.UserType = "Customer";

                    //userId = userBo.CreateUser(userVo);

                    customerVo.ProcessId = processId;
                    customerVo.UserId = userId;
                    customerVo.RmId = rmId;
                    customerVo.Adr1City = karvyUploadsVo.City;
                    customerVo.Adr1Country = karvyUploadsVo.Country;
                    customerVo.Adr1Line1 = karvyUploadsVo.Address1;
                    customerVo.Adr1Line2 = karvyUploadsVo.Address2;
                    customerVo.Adr1Line3 = karvyUploadsVo.Address3;
                    customerVo.Adr1PinCode = Int32.Parse(karvyUploadsVo.Pincode);
                    customerVo.Adr2PinCode = 0;
                    customerVo.OfcAdrPinCode = 0;
                    customerVo.Adr1State = karvyUploadsVo.State;
                    customerVo.CommencementDate = DateTime.Parse(dt.ToString());
                    if (karvyUploadsVo.DateofBirth == "")
                        customerVo.Dob = DateTime.Parse(dt.ToString());
                    else
                        customerVo.Dob = DateTime.Parse(karvyUploadsVo.DateofBirth);
                    customerVo.Email = karvyUploadsVo.Email;
                    customerVo.FirstName = karvyUploadsVo.FirstName;
                    customerVo.MiddleName = karvyUploadsVo.MiddleName;
                    customerVo.LastName = karvyUploadsVo.InvestorName;
                    customerVo.LoginId = karvyUploadsVo.Email;
                    if (karvyUploadsVo.Mobile == "")
                        customerVo.Mobile1 = 0;
                    else
                        customerVo.Mobile1 = Int64.Parse(karvyUploadsVo.Mobile);
                    

                    lenFaxNum = karvyUploadsVo.FaxResidence.Length;
                    if (lenFaxNum >= 8)
                    {
                        resFaxNum = karvyUploadsVo.FaxResidence.Substring(lenFaxNum - 8, 8);
                        if (lenFaxNum >= 11)
                        {
                            resFaxStdCode = karvyUploadsVo.FaxResidence.Substring(lenFaxNum - 11, 3);
                            if (lenFaxNum >= 12)
                                resFaxIsdCode = karvyUploadsVo.FaxResidence.Substring(0, lenFaxNum - 11);
                        }
                        else
                            resFaxStdCode = karvyUploadsVo.FaxResidence.Substring(0, lenFaxNum - 8);
                    }
                    else
                        resFaxNum = karvyUploadsVo.FaxResidence;
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



                    lenFaxNum = karvyUploadsVo.FaxOffice.Length;
                    if (lenFaxNum >= 8)
                    {
                        offFaxNum = karvyUploadsVo.FaxOffice.Substring(lenFaxNum - 8, 8);
                        if (lenFaxNum >= 11)
                        {
                            offFaxStdCode = karvyUploadsVo.FaxOffice.Substring(lenFaxNum - 11, 3);
                            if (lenFaxNum >= 12)
                                offFaxIsdCode = karvyUploadsVo.FaxOffice.Substring(0, lenFaxNum - 11);
                        }
                        else
                            offFaxStdCode = karvyUploadsVo.FaxOffice.Substring(0, lenFaxNum - 8);
                    }
                    else
                        offFaxNum = karvyUploadsVo.FaxOffice;
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



                    lenPhoneNum = karvyUploadsVo.PhoneOffice.Length;
                    if (lenPhoneNum >= 8)
                    {
                        offPhoneNum = karvyUploadsVo.PhoneOffice.Substring(lenPhoneNum - 8, 8);
                        if (lenPhoneNum >= 11)
                        {
                            offStdCode = karvyUploadsVo.PhoneOffice.Substring(lenPhoneNum - 11, 3);
                            if (lenPhoneNum >= 12)
                                offIsdCode = karvyUploadsVo.PhoneOffice.Substring(0, lenPhoneNum - 11);
                        }
                        else
                            offStdCode = karvyUploadsVo.PhoneOffice.Substring(0, lenPhoneNum - 8);
                    }
                    else
                        offPhoneNum = karvyUploadsVo.PhoneOffice;
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




                    lenPhoneNum = karvyUploadsVo.PhoneResidence.Length;
                    if (lenPhoneNum >= 8)
                    {
                        resPhoneNum = karvyUploadsVo.PhoneResidence.Substring(lenPhoneNum - 8, 8);
                        if (lenPhoneNum >= 11)
                        {
                            resStdCode = karvyUploadsVo.PhoneResidence.Substring(lenPhoneNum - 11, 3);
                            if (lenPhoneNum >= 12)
                                resIsdCode = karvyUploadsVo.PhoneResidence.Substring(0, lenPhoneNum - 11);
                        }
                        else
                            resStdCode = karvyUploadsVo.PhoneResidence.Substring(0, lenPhoneNum - 8);
                    }
                    else
                        resPhoneNum = karvyUploadsVo.PhoneResidence;
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




                    customerVo.Type = karvyUploadsVo.TypeCode;
                    customerVo.SubType = karvyUploadsVo.SubTypeCode;
                    customerVo.Occupation = karvyUploadsVo.OccCode;
                    customerVo.PANNum = karvyUploadsVo.PANNumber;
                    customerVo.Password = id.Next(10000, 99999).ToString();
                    customerVo.ProfilingDate = DateTime.Today;
                    customerVo.RBIApprovalDate = DateTime.Parse(dt.ToString());
                    customerVo.RegistrationDate = DateTime.Parse(dt.ToString());
                    customerVo.CommencementDate = DateTime.Parse(dt.ToString());
                    //customerId2 = customerBo.CreateCustomer(customerVo, rmId, userId);

                    //customerPortfolioVo.CustomerId = customerId2;
                    customerPortfolioVo.IsMainPortfolio = 1;
                    customerPortfolioVo.PortfolioTypeCode = "RGL";
                    //PortfolioBo.CreateCustomerPortfolio(customerPortfolioVo, userId);

                    customerBo.CreateCompleteCustomer(customerVo, userVo, customerPortfolioVo, userId);

                    countCustCreated++;
                }
                UpdateKarvyProfileStagingIsCustomerNew(adviserId, processId);
                
                //*****New Folios Upload from CAMS*****
                getNewFoliosDs = GetKarvyProfileNewFolios(processId);
                getNewFoliosDt = getNewFoliosDs.Tables[0];
                foreach (DataRow dr in getNewFoliosDt.Rows)
                {
                    customerAccountsVo.AccountNum = dr["CMFKXPS_Folio"].ToString();
                    customerAccountsVo.PortfolioId = Int32.Parse(dr["CP_PortfolioId"].ToString());
                    customerAccountsVo.AssetClass = "MF";
                    customerAccountsVo.AMCCode = Int32.Parse(dr["PA_AMCCode"].ToString());
                    customerAccountBo.CreateCustomerMFAccount(customerAccountsVo, userId);
                    countFolioCreated++;
                }
                UpdateKarvyProfileStagingIsFolioNew(processId);
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYInsertCustomerDetails()");

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
        public bool KARVYCreationOfNewBankAccounts(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                string query1 = "UPDATE dbo.CustomerMFKarvyXtrnlProfileStaging SET CMFKXPS_IsBankAccountNew = 0 "+
                                "FROM dbo.CustomerMFKarvyXtrnlProfileStaging A "+
                                "INNER JOIN dbo.CustomerBank B ON "+
                                "A.CMFKXPS_BankAccno = B.CB_AccountNum AND A.C_CustomerId = B.C_CustomerId "+
                                "WHERE A.ADUL_ProcessId = "+ processId +" AND CMFKXPS_IsRejected = 0 ";
                Package karvyProPkg4 = App.LoadPackage(Packagepath, null);
                karvyProPkg4.Variables["varProcessIdBankAccountCreation"].Value = processId;
                karvyProPkg4.Variables["varQueryBankAccountCheck"].Value = query1;
                karvyProPkg4.Configurations[0].ConfigurationString = configPath;
                                
                DTSExecResult karvyProResult4 = karvyProPkg4.Execute();
                
                if (karvyProResult4.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYCreationOfNewBankAccounts()");

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

        //Seventh phase: Insert good records into CAMS External Profile table
        public bool KARVYInsertExternalProfile(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package karvyProPkg5 = App.LoadPackage(Packagepath, null);
                karvyProPkg5.Variables["varProcessId"].Value = processId;
                karvyProPkg5.Variables["varProcessIdDeleteStaging"].Value = processId;
                karvyProPkg5.Configurations[0].ConfigurationString = configPath;
                DTSExecResult karvyProResult5 = karvyProPkg5.Execute();
                if (karvyProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadsBo.cs:KARVYInsertExternalProfile()");

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

        public bool UploadsKarvyDataTranslationForReprocess(int processId)
        {
            bool result = false;
            KarvyUploadsDao KarvyUploadsDao = new KarvyUploadsDao();
            try
            {
                result = KarvyUploadsDao.UploadsKarvyDataTranslationForReprocess(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:UploadsKarvyDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }


    }
}
