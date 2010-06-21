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
    public class StandardFolioUploadBo
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

        public List<StandardFolioUploadVo> GetNewFoliosStandard(int processId)
        {
            List<StandardFolioUploadVo> UploadsFolioList = new List<StandardFolioUploadVo>();
            StandardFolioUploadDao standardFolioUploadDao = new StandardFolioUploadDao();
            try
            {
                UploadsFolioList = standardFolioUploadDao.GetNewFoliosStandard(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "StandardFolioUploadBo.cs:GetNewFoliosStandard()");

                object[] objects = new object[0];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return UploadsFolioList;
        
        }

        //For Making all the necessary Chks in the Standard Folio Staging such as PanNum, AMC Code etc
        public bool StdFolioChksInFolioStaging(string Packagepath,int adviserId, int processId, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package stdProPkg1 = App.LoadPackage(Packagepath, null);
                stdProPkg1.Variables["varAdviserId1"].Value = adviserId;
                stdProPkg1.Variables["varAdviserId3"].Value = adviserId;
                stdProPkg1.Variables["varAdviserId5"].Value = adviserId;
                stdProPkg1.Variables["varProcessId1"].Value = processId;
                stdProPkg1.Variables["varProcessId2"].Value = processId;
                stdProPkg1.Variables["varProcessId3"].Value = processId;
                stdProPkg1.Variables["varProcessId4"].Value = processId;
                stdProPkg1.Variables["varProcessId5"].Value = processId;
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

                FunctionInfo.Add("Method", "StandardFolioUploadBo.cs:StdFolioChksInFolioStaging()");

                object[] objects = new object[1];
                objects[0] = Packagepath;

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
            StandardFolioUploadDao StandardFolioUploadsDao = new StandardFolioUploadDao();
            try
            {
                StandardFolioUploadsDao.StdDeleteCommonStaging(processId);
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

                FunctionInfo.Add("Method", "StandardFolioUploadBo.cs:StdDeleteCommonStaging()");

                object[] objects = new object[0];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        //Creation of Folios For Customers
        public bool StdCustomerFolioCreation(string Packagepath,int adviserId, int processId, string configPath)
        {
            bool IsProcessComplete = false;

            try
            {
                Package stdProPkg1 = App.LoadPackage(Packagepath, null);
                stdProPkg1.Variables["varProcessId"].Value = processId;
                stdProPkg1.Variables["varAdviserId"].Value = adviserId;
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

                FunctionInfo.Add("Method", "StandardFolioUploadBo.cs:StdCustomerFolioCreation()");

                object[] objects = new object[1];
                objects[0] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return IsProcessComplete;
        }
    }
}
