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
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.SqlServer;
using System.Data;
using System.Data.OleDb;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.SqlServer.Dts.Runtime;

namespace BoUploads
{
    public class WerpUploadsBo
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
        int userId;

        public List<WerpUploadsVo> GetWerpNewCustomers()
        {
            List<WerpUploadsVo> UploadsCustomerList = new List<WerpUploadsVo>();

            WerpUploadsDao WerpUploadsDao = new WerpUploadsDao();
            UploadsCustomerList = WerpUploadsDao.GetWerpNewCustomers();

            return UploadsCustomerList;
        }

        public bool UpdateProfileStagingIsCustomerNew()
        {
            bool result = false;
            WerpUploadsDao WerpUploadsDao = new WerpUploadsDao();
            WerpUploadsDao.UpdateProfileStagingIsCustomerNew();
            result = true;
            return result;
        }

        //Second phase: of the Upload; Insertion of Data from XML to Input table, Cleaning
        public bool WERPMFInsertToInputTrans(string Packagepath, string XMLFilepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package WERPMFTranPkg1 = App.LoadPackage(Packagepath, null);
                WERPMFTranPkg1.Variables["varXMLFilePath1"].Value = XMLFilepath;
                WERPMFTranPkg1.Configurations[0].ConfigurationString = configPath;
                DTSExecResult WERPMFTranResult1 = WERPMFTranPkg1.Execute();
                if (WERPMFTranResult1.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:WERPMFInsertToInputTrans()");

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
        public bool WERPMFInsertToStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package WERPMFTranPkg2 = App.LoadPackage(Packagepath, null);
                //WERPMFTranPkg2.Variables["varProcessId"].Value = processId;
                WERPMFTranPkg2.Configurations[0].ConfigurationString = configPath;
                DTSExecResult WERPMFTranResult2 = WERPMFTranPkg2.Execute();
                if (WERPMFTranResult2.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:WERPMFInsertToStagingTrans()");

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
        public bool WERPMFProcessDataInStagingTrans(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                string query1 = "Update dbo.CustomerMFXtrnlTransactionStaging set "+
                "dbo.CustomerMFXtrnlTransactionStaging.C_CustomerId=E.C_CustomerId, "+
                "dbo.CustomerMFXtrnlTransactionStaging.CMFA_AccountId = D.CMFA_AccountId  "+
                "from CustomerMFXtrnlTransactionStaging A  inner join CustomerMutualFundAccount D on "+
                "A.[CMFXTS_FolioNum]=D.[CMFA_FolioNum]  inner join CustomerPortfolio E on "+
                "D.CP_PortfolioId=E.CP_PortfolioId inner join Customer F on E.C_CustomerId=F.C_CustomerId "+
                "inner join AdviserRM G on F.AR_RMId=G.AR_RMId where  G.A_AdviserId= A.A_AdviserId " +
                "and A.ADUL_ProcessId = "+ processId;

                Package WERPMFTranPkg3 = App.LoadPackage(Packagepath, null);
                WERPMFTranPkg3.Variables["varProcessID"].Value = processId;
                WERPMFTranPkg3.Variables["varQueryFolioCheck"].Value = query1;
                WERPMFTranPkg3.Variables["varChecksProcessId"].Value = processId;
                WERPMFTranPkg3.Configurations[0].ConfigurationString = configPath;

                DTSExecResult WERPMFTranResult3 = WERPMFTranPkg3.Execute();
                if (WERPMFTranResult3.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:WERPMFProcessDataInStagingTrans()");

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
        //Fifth Phase: Move good data to Tables
        public bool WERPMFInsertTransDetails(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            try
            {
                Package WERPMFTranPkg4 = App.LoadPackage(Packagepath, null);
                WERPMFTranPkg4.Variables["varProcessId"].Value = processId;
                WERPMFTranPkg4.Variables["varDeleteStagingProcessId"].Value = processId;
                WERPMFTranPkg4.Configurations[0].ConfigurationString = configPath;
                DTSExecResult WERPMFTranResult4 = WERPMFTranPkg4.Execute();
                if (WERPMFTranResult4.ToString() == "Success")
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

                FunctionInfo.Add("Method", "KarvyUploadBo.cs:WERPMFInsertTransDetails()");

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
