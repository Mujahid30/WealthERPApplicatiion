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
    public class TempletonUploadsBo
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

        //Second Phase: Insert data from Templeton file to input table 
        public bool TempInsertToInputProfile(int processId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package tempProPkg1 = App.LoadPackage(Packagepath, null);
                tempProPkg1.Variables["varXMLFilePath"].Value = XMLFilepath;
                tempProPkg1.Variables["varProcessId"].Value = processId;
                tempProPkg1.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult1 = tempProPkg1.Execute();
                if (tempProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempInsertToInputProfile()");

                object[] objects = new object[4];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = XMLFilepath;
                objects[3] = configPath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Third Phase: Insert to Templeton staging table from input table and storing id
        public bool TempInsertToStagingProfile(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package tempProPkg2 = App.LoadPackage(Packagepath, null);
                tempProPkg2.Variables["varProcessId"].Value = processId;
                tempProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult2 = tempProPkg2.Execute();
                if (tempProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempInsertToStagingProfile()");

                object[] objects = new object[3];
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

        //Fourth Phase: Data Translation checks in Templeton staging table
        public bool TempProcessDataInStagingProfile(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package tempProPkg3 = App.LoadPackage(Packagepath, null);
                tempProPkg3.ImportConfigurationFile(configPath);
                tempProPkg3.Variables["varProcessId"].Value = processId;

                DTSExecResult tempProResult3 = tempProPkg3.Execute();
                if (tempProResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempProcessDataInStagingProfile()");

                object[] objects = new object[4];
                objects[0] = processId;
                objects[1] = AdviserId;
                objects[2] = Packagepath;
                objects[3] = configPath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Transfer profile details from Templeton staging to Profile Common staging
        public bool TempInsertToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package tempProPkg4 = App.LoadPackage(Packagepath, null);
                tempProPkg4.Variables["varProcessId"].Value = processId;
                tempProPkg4.Variables["varXMLFileTypeId"].Value = 16;
                tempProPkg4.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult4 = tempProPkg4.Execute();
                if (tempProResult4.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempInsertToCommonStaging()");

                object[] objects = new object[4];
                objects[0] = processId;
                objects[2] = Packagepath;
                objects[3] = configPath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        //Transfer the Folio details from Templeton staging to the Common Folio Staging
        public bool TempInsertFolioDataToFolioCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package tempProPkg5 = App.LoadPackage(Packagepath, null);
                tempProPkg5.Variables["varProcessId"].Value = processId;
                tempProPkg5.Variables["varXMLFileTypeId"].Value = 16;
                tempProPkg5.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult5 = tempProPkg5.Execute();
                if (tempProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempInsertFolioDataToFolioCommonStaging()");

                object[] objects = new object[3];
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

        // Insert Data From Transaction XML File to Input Table
        public bool TempletonInsertToInputTrans(int processId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package tempProPkg6 = App.LoadPackage(Packagepath, null);
                tempProPkg6.Variables["varProcessId"].Value = processId;
                tempProPkg6.Variables["varXMLFilePath"].Value = XMLFilepath;
                tempProPkg6.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult6 = tempProPkg6.Execute();
                if (tempProResult6.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempInsertFolioDataToFolioCommonStaging()");

                object[] objects = new object[4];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = XMLFilepath;
                objects[3] = configPath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        // Insert Data From Input Table to Staging Table
        public bool TempletonInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package tempProPkg2 = App.LoadPackage(Packagepath, null);
                tempProPkg2.Variables["varProcessId"].Value = processId;
                tempProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult2 = tempProPkg2.Execute();
                if (tempProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempInsertToStagingProfile()");

                object[] objects = new object[3];
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

        // Process Data In Templeton Transaction Table
        public bool TempletonProcessDataInStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package tempProPkg2 = App.LoadPackage(Packagepath, null);
                tempProPkg2.Variables["varProcessId"].Value = processId;
                tempProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult2 = tempProPkg2.Execute();
                if (tempProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempInsertToStagingProfile()");

                object[] objects = new object[3];
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

        // Insert Data from First Staging to Second Staging
        public bool TempletonInsertFromTempStagingTransToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package tempProPkg2 = App.LoadPackage(Packagepath, null);
                tempProPkg2.Variables["varProcessId"].Value = processId;
                tempProPkg2.Variables["varXMLFileTypeId"].Value = 15;
                tempProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult tempProResult2 = tempProPkg2.Execute();
                if (tempProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "TempletonUploadsBo.cs:TempletonInsertFromTempStagingTransToCommonStaging()");

                object[] objects = new object[3];
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

    }
}
