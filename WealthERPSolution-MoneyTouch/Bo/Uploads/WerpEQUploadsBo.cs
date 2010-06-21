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
    public class WerpEQUploadsBo
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

        public bool WerpEQInsertToInputTransaction(string Packagepath, string XMLFilepath, string configPath)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varXMLFilePath"].Value = XMLFilepath;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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
            return blResult;
        }

        public bool WerpEQInsertToFirstStagingTransaction(int processId, string Packagepath, string configPath)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varProcessIdCleanInput"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdRejectBadData"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdDelete"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdOLEDBSource"].Value = processId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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
                objects[0] = processId;
                objects[1] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WerpEQProcessDataInFirstStagingTrans(int processId, string Packagepath, string configPath, int adviserId)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varAdviserId"].Value = adviserId;
                werpEQTranPkg1.Variables["varProcessIdUpdateBrokerCode"].Value = processId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WerpEQInsertToSecondStagingTransaction(int processId, string Packagepath, string configPath, int xmlFileTypeId)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varProcessIdDataFlow"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdXMLFileTypeId"].Value = processId;
                werpEQTranPkg1.Variables["varXMLFileTypeId"].Value = xmlFileTypeId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = xmlFileTypeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WERPEQProcessDataInSecondStagingTrans(int processId, string Packagepath, string configPath, int adviserId)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varAdviserId"].Value = adviserId;
                werpEQTranPkg1.Variables["varProcessId"].Value = processId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WERPEQInsertTransDetails(int processId, string Packagepath, string configPath)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varProcessIdDataFlow"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdUpdateTable"].Value = processId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                {
                    //To update the least trans date for the process.
                    DataSet dsmindateval = new DataSet();
                    UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
                    CustomerPortfolioBo customerportfoliobo = new CustomerPortfolioBo();
                    bool valupdated = true;
                    dsmindateval = uploadscommonDao.GetMinDateofUploadTrans(processId, "EQ");
                    if (dsmindateval.Tables[0].Rows.Count != 0)
                    {
                        DateTime LeastTransDate = DateTime.Parse(dsmindateval.Tables[0].Rows[0]["MinDate"].ToString());
                        int AdvId = int.Parse(dsmindateval.Tables[0].Rows[0]["AdviserId"].ToString());
                        valupdated = customerportfoliobo.UpdateAdviserDailyEODLogRevaluateForTransaction(AdvId, "EQ", LeastTransDate);
                    }
                    if (valupdated)
                    {
                        blResult = true;
                    }
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

                FunctionInfo.Add("Method", "WerpMFUploadsBo.cs:WerpMFInsertToInputProfile()");

                object[] objects = new object[2];
                objects[0] = processId;
                objects[1] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        /**********************/
        /**********************/

        public bool WerpEQInsertToInputTradeAccount(string Packagepath, string XMLFilepath, string configPath)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varXMLPath"].Value = XMLFilepath;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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
            return blResult;
        }

        public bool WerpEQInsertToFirstStagingTradeAccount(int processId, string Packagepath, string configPath)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                werpEQTranPkg1.Variables["varProcessIdCleaning"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdDataFlow"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdDeleting"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdRejecting"].Value = processId;
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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
                objects[0] = processId;
                objects[1] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WerpEQProcessDataInFirstStagingTradeAccount(int processId, string Packagepath, string configPath, int adviserId)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varAdviserId"].Value = adviserId;
                werpEQTranPkg1.Variables["varProcessId"].Value = processId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WerpEQInsertToSecondStagingTradeAccount(int processId, string Packagepath, string configPath, int xmlFileTypeId)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varProcessIdDataFlow"].Value = processId;
                werpEQTranPkg1.Variables["varProcessIdXMLFileTypeId"].Value = processId;
                werpEQTranPkg1.Variables["varXMLFileTypeId"].Value = xmlFileTypeId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = xmlFileTypeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WERPEQProcessDataInSecondStagingTradeAccount(int processId, string Packagepath, string configPath, int adviserId)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varAdviserId"].Value = adviserId;
                werpEQTranPkg1.Variables["varProcessId"].Value = processId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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

                object[] objects = new object[3];
                objects[0] = processId;
                objects[1] = Packagepath;
                objects[2] = adviserId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool WERPEQInsertTradeAccountDetails(int processId, string Packagepath, string configPath)
        {
            bool blResult = false;

            try
            {
                Package werpEQTranPkg1 = App.LoadPackage(Packagepath, null);
                werpEQTranPkg1.Variables["varProcessIdDataFlow"].Value = processId;
                werpEQTranPkg1.ImportConfigurationFile(configPath);
                DTSExecResult werpEQTranResult1 = werpEQTranPkg1.Execute();
                if (werpEQTranResult1.ToString() == "Success")
                    blResult = true;
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
                objects[0] = processId;
                objects[1] = Packagepath;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }
    }
}
