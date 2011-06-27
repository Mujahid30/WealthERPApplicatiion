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
    public class DeutscheUploadsBo
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
        public bool DeutscheInsertToInputProfile(int processId, string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package deutscheProPkg1 = App.LoadPackage(Packagepath, null);
                deutscheProPkg1.Variables["varXMLFilePath"].Value = XMLFilepath;
                deutscheProPkg1.Variables["varProcessId"].Value = processId;
                deutscheProPkg1.ImportConfigurationFile(configPath);
                DTSExecResult deutscheProResult1 = deutscheProPkg1.Execute();
                if (deutscheProResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheInsertToInputProfile()");

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
        public bool DeutscheInsertToStagingProfile(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package deutscheProPkg2 = App.LoadPackage(Packagepath, null);
                deutscheProPkg2.Variables["varProcessId"].Value = processId;
                deutscheProPkg2.ImportConfigurationFile(configPath);
                DTSExecResult deutscheProResult2 = deutscheProPkg2.Execute();
                if (deutscheProResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheInsertToStagingProfile()");

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
        public bool DeutscheProcessDataInStagingProfile(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package deutscheProPkg3 = App.LoadPackage(Packagepath, null);
                deutscheProPkg3.ImportConfigurationFile(configPath);
                deutscheProPkg3.Variables["varProcessId"].Value = processId;

                DTSExecResult deutscheProResult3 = deutscheProPkg3.Execute();
                if (deutscheProResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheProcessDataInStagingProfile()");

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
        public bool DeutscheInsertToCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {

                Package deutscheProPkg4 = App.LoadPackage(Packagepath, null);
                deutscheProPkg4.Variables["varProcessId"].Value = processId;
                deutscheProPkg4.Variables["varXMLFileTypeId"].Value = 18;
                deutscheProPkg4.ImportConfigurationFile(configPath);
                DTSExecResult deutscheProResult4 = deutscheProPkg4.Execute();
                if (deutscheProResult4.ToString() == "Success")
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheInsertToCommonStaging()");

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
        public bool DeutscheInsertFolioDataToFolioCommonStaging(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package deutscheProPkg5 = App.LoadPackage(Packagepath, null);
                deutscheProPkg5.Variables["varProcessId"].Value = processId;
                deutscheProPkg5.Variables["varXMLFileTypeId"].Value = 18;
                deutscheProPkg5.ImportConfigurationFile(configPath);
                DTSExecResult deutscheProResult5 = deutscheProPkg5.Execute();
                if (deutscheProResult5.ToString() == "Success")
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheInsertFolioDataToFolioCommonStaging()");

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



        #region Deutsche Transaction

        public bool DeutscheInsertToInputTrans(int ProcessId, string PackagePath, string XMLFilePath, string ConfigPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package deutscheTranPkg1 = App.LoadPackage(PackagePath, null);
                deutscheTranPkg1.Variables["varXMLFilePath"].Value = XMLFilePath;
                deutscheTranPkg1.Variables["varProcessId"].Value = ProcessId;
                deutscheTranPkg1.ImportConfigurationFile(ConfigPath);
                DTSExecResult camsTranResult1 = deutscheTranPkg1.Execute();
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheInsertToInputTrans()");

                object[] objects = new object[4];
                objects[0] = PackagePath;
                objects[1] = XMLFilePath;
                objects[2] = ProcessId;
                objects[3] = ConfigPath;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }

        public bool DeutscheInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package deutscheTranPkg2 = App.LoadPackage(Packagepath, null);
                deutscheTranPkg2.Variables["varProcessId"].Value = processId;
                deutscheTranPkg2.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult2 = deutscheTranPkg2.Execute();
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheInsertToStagingTrans()");

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
        public bool DeutscheProcessDataInStagingTrans(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package deutscheTranPkg3 = App.LoadPackage(Packagepath, null);            
                deutscheTranPkg3.Variables["varProcessId"].Value = processId;
                deutscheTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult3 = deutscheTranPkg3.Execute();
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheProcessDataInStagingTrans()");

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

        public bool DeutscheTransInsertToCommonTransStaging(int processId, int AdviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {



                Package deutscheTranPkg3 = App.LoadPackage(Packagepath, null);
                deutscheTranPkg3.Variables["varProcessId"].Value = processId;
                deutscheTranPkg3.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult3 = deutscheTranPkg3.Execute();
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

                FunctionInfo.Add("Method", "DeutscheUploadsBo.cs:DeutscheProcessDataInStagingTrans()");

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

        #endregion Deutsche Transaction
    }
}
