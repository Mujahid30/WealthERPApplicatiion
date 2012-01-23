using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using DaoUploads;
using System.Web;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.SqlServer;
using System.Data.Sql;
using System.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.SqlServer.Dts.Runtime;
using System.Configuration;
using BoCustomerPortfolio;


namespace BoUploads
{
    public class UploadCommonBo
    {
        //To retrieve all fieldnames for a type of file used for upload. These fields are used to map column names of 
        //of the file uploaded with fields that must actually be thee for the type of uploaded file


        public DataSet GetColumnNames(int XMLFileTypeId)
        {
            DataSet dsColumnNames = new DataSet();
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                dsColumnNames = uploadscommonDao.GetColumnNames(XMLFileTypeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetColumnNames()");

                object[] objects = new object[1];
                objects[0] = XMLFileTypeId;
               

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsColumnNames;
        }
        public string GetLastUploadDate(int advisorid)
        {
            string lastUploadDate="";
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                lastUploadDate = uploadscommonDao.GetLastUploadDate(advisorid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetLastUploadDate()");
                object[] objects = new object[1];
                objects[0] = advisorid;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return lastUploadDate;
        }
        public DataSet GetUploadWERPNameForExternalColumnNames(int xmlfiletypeId)
        {
            DataSet dsColumnNames = new DataSet();
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                dsColumnNames = uploadscommonDao.GetUploadWERPNameForExternalColumnNames(xmlfiletypeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetColumnNames()");

                object[] objects = new object[1];
                objects[0] = null;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsColumnNames;
        }

        public int CreateUploadProcess(UploadProcessLogVo processlogVo)
        {
            int processid = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                processid = uploadDAO.CreateUploadProcess(processlogVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:CreateUploadProcess()");


                object[] objects = new object[1];
                objects[0] = processlogVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return processid;
        }

        public UploadProcessLogVo GetProcessLogInfo(int processId)
        {
            UploadProcessLogVo uploadlodvo = new UploadProcessLogVo();
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                uploadlodvo = uploadDAO.GetProcessLogInfo(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetProcessLogInfo()");


                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return uploadlodvo;
        }

        public bool UpdateUploadProcessLog(UploadProcessLogVo processlogVo)
        {
            bool updatedflag = false;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                updatedflag = uploadDAO.UpdateUploadProcessLog(processlogVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:UpdateUploadProcessLog()");


                object[] objects = new object[1];
                objects[0] = processlogVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return updatedflag;
        }

        public DataSet GetUploadProcessLogAdmin(int adviserId, int CurrentPage, out int Count, string SortExpression)
        {
            DataSet getProcessLogDs;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                getProcessLogDs = uploadDAO.GetUploadProcessLogAdmin(adviserId, CurrentPage, out Count, SortExpression);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonBO.cs:GetUploadProcessLogAdmin()");

                object[] objects = new object[1];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return getProcessLogDs;
        }

        public bool UpdateAMCForFolioReprocess(int ProcessId, string type)
        {
            bool blResult = false;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                blResult = uploadDAO.UpdateAMCForFolioReprocess(ProcessId, type);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:UpdateAMCForReprocess()");

                object[] objects = new object[2];
                objects[0] = ProcessId;
                objects[1] = type;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool UpdateAMCForTransactionReprocess(int ProcessId, string type)
        {
            bool blResult = false;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                blResult = uploadDAO.UpdateAMCForTransactionReprocess(ProcessId, type);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:UpdateAMCForReprocess()");

                object[] objects = new object[2];
                objects[0] = ProcessId;
                objects[1] = type;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool Rollback(int ProcessID, Int16 FileType)
        {
            bool blResult = false;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                blResult = uploadDAO.Rollback(ProcessID, FileType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:UpdateUploadProcessLog()");

                object[] objects = new object[2];
                objects[0] = ProcessID;
                objects[1] = FileType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public int GetTransUploadCount(int processID, string type)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetTransUploadCount(processID, type);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetTransUploadCount()");

                object[] objects = new object[2];
                objects[0] = processID;
                objects[1] = type;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public int GetTransUploadRejectCount(int processID, string assetType)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetTransUploadRejectCount(processID, assetType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetTransUploadRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;
                objects[1] = assetType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public int GetAccountsUploadCount(int processID, string type)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetAccountsUploadCount(processID, type);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetTransUploadCount()");

                object[] objects = new object[2];
                objects[0] = processID;
                objects[1] = type;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public int GetAccountsUploadRejectCount(int processID, string assetType)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetAccountsUploadRejectCount(processID, assetType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetTransUploadRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;
                objects[1] = assetType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public bool Rollback(int ProcessID, int FileTypeID, string InputInsertionStatus, string FirstStagingStatus, string SecondStagingStatus, string WERPInsertionStatus, out bool blCustAssociateExists, out bool blCustBankExists, out bool blCustAssetExists, out bool blCustEQTranNetPositionUpdated, out bool blCustMFTranNetPositionUpdated, out bool blCustEQTranExist)
        {
            bool blResult = false;

            blCustAssociateExists = false;
            blCustBankExists = false;
            blCustAssetExists = false;
            blCustEQTranNetPositionUpdated = false;
            blCustMFTranNetPositionUpdated = false;
            blCustEQTranExist = false;

            string Stage = string.Empty;

            UploadsCommonDao uploadsCommonDao = new UploadsCommonDao();

            if (InputInsertionStatus == "Y")
            {
                if (FirstStagingStatus == "Y")
                {
                    if (SecondStagingStatus == "Y")
                    {
                        if (WERPInsertionStatus == "Y")
                        {
                            Stage = "werp";

                            #region CAMS Profile
                            if (FileTypeID == 2)
                            {   // MF CAMS Profile Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from external tables onwards

                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.
                                blCustAssociateExists = CustomerAssociateExists(ProcessID);
                                blCustBankExists = CustomerBankExists(ProcessID);
                                blCustAssetExists = CustomerAssetExists(ProcessID);

                                if (blCustAssociateExists || blCustBankExists || blCustAssetExists)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackCAMSProfile(ProcessID, Stage);
                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion
                            #region CAMS Transaction
                            else if (FileTypeID == 1)
                            {   // MF CAMS Transaction Upload

                                // Rollback from WERP Table Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                // Checking for dependant data - Net Postion Updated for the transaction
                                //blCustMFTranNetPositionUpdated = CustMFNetPositionUpdated(ProcessID);

                                if (blCustMFTranNetPositionUpdated)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackMFCAMSTransaction(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion
                            #region Karvy Transaction
                            else if (FileTypeID == 3)
                            {   // MF Karvy Transaction Upload

                                // Checking for dependant data - Net Postion Updated for the transaction
                                //blCustMFTranNetPositionUpdated = CustMFNetPositionUpdated(ProcessID);

                                if (blCustMFTranNetPositionUpdated)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackMFKarvyTransaction(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }

                            }
                            #endregion
                            #region Karvy Profile
                            else if (FileTypeID == 4)
                            {   // MF Karvy Profile Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.
                                blCustAssociateExists = CustomerAssociateExists(ProcessID);
                                //blCustBankExists = CustomerBankExists(ProcessID);
                                blCustAssetExists = CustomerAssetExists(ProcessID);

                                if (blCustAssociateExists || blCustAssetExists)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackKarvyProfile(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion
                            #region Standard Profile

                            else if (FileTypeID == 7)
                            {   // Standard Profile Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.
                                blCustAssociateExists = CustomerAssociateExists(ProcessID);
                                //blCustBankExists = CustomerBankExists(ProcessID);
                                blCustAssetExists = CustomerAssetExists(ProcessID);

                                if (blCustAssociateExists || blCustAssetExists)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackStandardProfile(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }

                            #endregion
                            #region Standard Equity Trade Account

                            else if (FileTypeID == 13)
                            {   // Standard EQ Trade Account Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Trade account has any transactions associated to this
                                // 2) Remove details from werp tables onwards

                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.

                                blCustEQTranExist = CustEQTransactionsExist(ProcessID);

                                if (blCustEQTranExist)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackEQStandardTradeAccount(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }

                            #endregion
                            #region Standard Equity Transactions
                            else if (FileTypeID == 8)
                            {   // Standard Equity Transaction Upload

                                // Rollback from WERP Table Onwards
                                // Process:
                                // 1) Check if the Net Position Tables have been updated for these transactions
                                // 2) Remove details from werp tables onwards

                                // Checking for dependant data - Net Postion Updated for the transaction
                                //blCustMFTranNetPositionUpdated = CustMFNetPositionUpdated(ProcessID);

                               // blCustEQTranNetPositionUpdated = CustEQNetPositionUpdated(ProcessID);

                                if (blCustEQTranNetPositionUpdated)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackEQStandardTransactions(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion
                            #region Templeton Profile
                            else if (FileTypeID == 16)
                            {   // MF Templeton Profile Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.
                                blCustAssociateExists = CustomerAssociateExists(ProcessID);
                                //blCustBankExists = CustomerBankExists(ProcessID);
                                blCustAssetExists = CustomerAssetExists(ProcessID);

                                if (blCustAssociateExists || blCustAssetExists)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackTempletonProfile(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion
                            #region Deutche Profile
                            else if (FileTypeID == 18)
                            {   // MF Deutche Profile Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.
                                blCustAssociateExists = CustomerAssociateExists(ProcessID);
                                //blCustBankExists = CustomerBankExists(ProcessID);
                                blCustAssetExists = CustomerAssetExists(ProcessID);

                                if (blCustAssociateExists || blCustAssetExists)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackDeutcheProfile(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion
                            #region Templeton Transaction
                            else if (FileTypeID == 15)
                            {   // MF Templeton Transaction Upload

                                // Rollback from WERP Table Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                // Checking for dependant data - Net Postion Updated for the transaction
                               // blCustMFTranNetPositionUpdated = CustMFNetPositionUpdated(ProcessID);

                                if (blCustMFTranNetPositionUpdated)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackMFTempletonTransaction(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion
                            #region Deutche Transaction
                            else if (FileTypeID == 17)
                            {   // MF Deutche Transaction Upload

                                // Rollback from WERP Table Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                // Checking for dependant data - Net Postion Updated for the transaction
                               // blCustMFTranNetPositionUpdated = CustMFNetPositionUpdated(ProcessID);

                                if (blCustMFTranNetPositionUpdated)
                                {
                                    blResult = false;
                                }
                                else
                                {
                                    blResult = RollbackMFDeutcheTransaction(ProcessID, Stage);

                                    if (blResult)
                                    {
                                        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                    }
                                }
                            }
                            #endregion


                            #region Not Used
                            //else if (FileTypeID == 9 || FileTypeID == 10 || FileTypeID == 14 || FileTypeID == 15)
                            //{   // WERP Types will not be stored in external tables

                            //    // Do nothing
                            //}
                            //else if (FileTypeID == 12)
                            //{   // EQ Odin NSE Transaction Upload

                            //    // Checking for dependant data - Net Postion Updated for the transaction
                            //    blCustEQTranNetPositionUpdated = CustEQNetPositionUpdated(ProcessID);

                            //    if (blCustEQTranNetPositionUpdated)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        blResult = RollbackOdinNSETransactionXtrnl(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            //else if (FileTypeID == 13)
                            //{   // EQ Odin BSE Transaction Upload

                            //    // Checking for dependant data - Net Postion Updated for the transaction
                            //    blCustEQTranNetPositionUpdated = CustEQNetPositionUpdated(ProcessID);

                            //    if (blCustEQTranNetPositionUpdated)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        blResult = RollbackOdinBSETransactionXtrnl(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                        else
                        {

                            Stage = "secondstage";
                            #region CAMS Profile
                            if (FileTypeID == 2)
                            {   // MF CAMS Profile Upload

                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.

                                blResult = RollbackCAMSProfile(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion
                            #region CAMS Transaction
                            else if (FileTypeID == 1)
                            {   // MF CAMS Transaction Upload

                                blResult = RollbackMFCAMSTransaction(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion
                            #region Karvy Transaction
                            else if (FileTypeID == 3)
                            {   // MF Karvy Transaction Upload

                                blResult = RollbackMFKarvyTransaction(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion
                            #region Karvy Profile
                            else if (FileTypeID == 4)
                            {   // MF Karvy Profile Upload


                                // Checking if Customer Associates or Bank Details or Assets 
                                // are tied to the uploaded customer.

                                blResult = RollbackKarvyProfile(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion
                            #region Standard Profile

                            else if (FileTypeID == 7)
                            {   // Standard Profile Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Cust has any details in any of the tables
                                // 2) Remove details from werp tables onwards

                                blResult = RollbackStandardProfile(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }

                            #endregion
                            #region Standard Equity Trade Account

                            else if (FileTypeID == 13)
                            {   // Standard EQ Trade Account Upload

                                // Rollback from WERP Tables Onwards
                                // Process:
                                // 1) Check if Trade account has any transactions associated to this
                                // 2) Remove details from werp tables onwards

                                blResult = RollbackEQStandardTradeAccount(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }

                            #endregion
                            #region Standard Equity Transactions
                            else if (FileTypeID == 8)
                            {   // Standard Equity Transaction Upload

                                // Rollback from WERP Table Onwards
                                // Process:
                                // 1) Check if the Net Position Tables have been updated for these transactions
                                // 2) Remove details from werp tables onwards

                                blResult = RollbackEQStandardTransactions(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion
                            #region Templeton Profile
                            else if (FileTypeID == 16)
                            {   // MF Karvy Profile Upload


                                blResult = RollbackTempletonProfile(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion
                            #region Deutche Profile
                            else if (FileTypeID == 18)
                            {   // MF Deutche Profile Upload


                                blResult = RollbackDeutcheProfile(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion
                            #region Templeton Transaction
                            else if (FileTypeID == 15)
                            {   // MF Templeton Transaction Upload

                                blResult = RollbackMFTempletonTransaction(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }
                            }

                            #endregion
                            #region Deutche Transaction
                            else if (FileTypeID == 18)
                            {   // MF Deutche Transaction Upload

                                blResult = RollbackMFDeutcheTransaction(ProcessID, Stage);

                                if (blResult)
                                {
                                    blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                                }

                            }
                            #endregion


                            #region Not Used
                            //else if (FileTypeID == 9)
                            //{   // MF WERP Transaction Upload

                            //    // Checking for dependant data - Net Postion Updated for the transaction
                            //    blCustMFTranNetPositionUpdated = CustMFNetPositionUpdated(ProcessID);

                            //    if (blCustMFTranNetPositionUpdated)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        // Rollback from WERP Onwards
                            //        blResult = RollbackMFWERPTransactionWerp(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            //else if (FileTypeID == 10)
                            //{   // MF WERP Profile Upload

                            //    // Checking if Customer Associates or Bank Details or Assets 
                            //    // are tied to the uploaded customer.
                            //    blCustAssociateExists = CustomerAssociateExists(ProcessID);
                            //    //blCustBankExists = CustomerBankExists(ProcessID);
                            //    blCustAssetExists = CustomerAssetExists(ProcessID);

                            //    if (blCustAssociateExists || blCustBankExists || blCustAssetExists)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        // Rollback from WERP Onwards
                            //        blResult = RollbackMFWERPProfileWerp(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            //else if (FileTypeID == 14)
                            //{   // EQ WERP Transaction Upload

                            //    // Checking for dependant data - Net Postion Updated for the transaction
                            //    blCustEQTranNetPositionUpdated = CustEQNetPositionUpdated(ProcessID);

                            //    if (blCustEQTranNetPositionUpdated)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        // Rollback from WERP Onwards
                            //        blResult = RollbackEQWERPTransactionWerp(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            //else if (FileTypeID == 15)
                            //{   // EQ WERP Profile Upload

                            //    // Checking if Customer Associates or Bank Details or Assets 
                            //    // are tied to the uploaded customer.
                            //    blCustAssociateExists = CustomerAssociateExists(ProcessID);
                            //    //blCustBankExists = CustomerBankExists(ProcessID);
                            //    blCustAssetExists = CustomerAssetExists(ProcessID);

                            //    if (blCustAssociateExists || blCustBankExists || blCustAssetExists)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        // Rollback from WERP Onwards
                            //        blResult = RollbackEQWERPProfileWerp(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            //else if (FileTypeID == 12)
                            //{   // EQ Odin NSE Transaction Upload

                            //    // Checking for dependant data - Net Postion Updated for the transaction
                            //    blCustEQTranNetPositionUpdated = CustEQNetPositionUpdated(ProcessID);

                            //    if (blCustEQTranNetPositionUpdated)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        // Rollback from WERP Onwards
                            //        blResult = RollbackOdinNSETransactionWerp(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            //else if (FileTypeID == 13)
                            //{   // EQ Odin BSE Transaction Upload

                            //    // Checking for dependant data - Net Postion Updated for the transaction
                            //    blCustEQTranNetPositionUpdated = CustEQNetPositionUpdated(ProcessID);

                            //    if (blCustEQTranNetPositionUpdated)
                            //    {
                            //        blResult = false;
                            //    }
                            //    else
                            //    {
                            //        // Rollback from WERP Onwards
                            //        blResult = RollbackOdinBSETransactionWerp(ProcessID);

                            //        if (blResult)
                            //        {
                            //            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                    }
                    else
                    {

                        Stage = "firststage";
                        #region CAMS Profile
                        if (FileTypeID == 2)
                        {   // MF CAMS Profile Upload

                            blResult = RollbackCAMSProfile(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }
                        }
                        #endregion
                        #region CAMS Transaction
                        else if (FileTypeID == 1)
                        {   // MF CAMS Transaction Upload

                            blResult = RollbackMFCAMSTransaction(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }
                        }
                        #endregion
                        #region Karvy Transaction
                        else if (FileTypeID == 3)
                        {   // MF Karvy Transaction Upload

                            blResult = RollbackMFKarvyTransaction(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }
                        }
                        #endregion
                        #region Karvy Profile
                        else if (FileTypeID == 4)
                        {   // MF Karvy Profile Upload

                            blResult = RollbackKarvyProfile(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }
                        }
                        #endregion
                        #region Standard Profile

                        else if (FileTypeID == 7)
                        {   // Standard Profile Upload

                            // Rollback from WERP Tables Onwards
                            // Process:
                            // 1) Check if Cust has any details in any of the tables
                            // 2) Remove details from werp tables onwards

                            blResult = RollbackStandardProfile(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }

                        }

                        #endregion
                        #region Standard Equity Trade Account

                        else if (FileTypeID == 13)
                        {   // Standard EQ Trade Account Upload

                            // Rollback from WERP Tables Onwards
                            // Process:
                            // 1) Check if Trade account has any transactions associated to this
                            // 2) Remove details from werp tables onwards

                            blResult = RollbackEQStandardTradeAccount(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }

                        }

                        #endregion
                        #region Standard Equity Transactions
                        else if (FileTypeID == 8)
                        {   // Standard Equity Transaction Upload

                            // Rollback from WERP Table Onwards
                            // Process:
                            // 1) Check if the Net Position Tables have been updated for these transactions
                            // 2) Remove details from werp tables onwards

                            blResult = RollbackEQStandardTransactions(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }

                        }
                        #endregion
                        #region Templeton Profile
                        else if (FileTypeID == 16)
                        {   // MF Karvy Profile Upload


                            blResult = RollbackTempletonProfile(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }

                        }
                        #endregion
                        #region Deutche Profile
                        else if (FileTypeID == 18)
                        {   // MF Deutche Profile Upload


                            blResult = RollbackDeutcheProfile(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }

                        }
                        #endregion
                        #region Templeton Transaction
                        else if (FileTypeID == 15)
                        {   // MF Templeton Transaction Upload

                            blResult = RollbackMFTempletonTransaction(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }
                        }

                        #endregion
                        #region Deutche Transaction
                        else if (FileTypeID == 17)
                        {   // MF Deutche Transaction Upload

                            blResult = RollbackMFDeutcheTransaction(ProcessID, Stage);

                            if (blResult)
                            {
                                blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                            }

                        }
                        #endregion


                        #region Not Used
                        //else if (FileTypeID == 9)
                        //{   // MF WERP Transaction Upload

                        //    blResult = RollbackMFWERPTransactionStaging(ProcessID);

                        //    if (blResult)
                        //    {
                        //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        //    }
                        //}
                        //else if (FileTypeID == 10)
                        //{   // MF WERP Profile Upload

                        //    blResult = RollbackMFWERPProfileStaging(ProcessID);

                        //    if (blResult)
                        //    {
                        //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        //    }
                        //}
                        //else if (FileTypeID == 14)
                        //{   // EQ WERP Transaction Upload

                        //    blResult = RollbackEQWERPTransactionStaging(ProcessID);

                        //    if (blResult)
                        //    {
                        //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        //    }
                        //}
                        //else if (FileTypeID == 15)
                        //{   // EQ WERP Profile Upload

                        //    blResult = RollbackEQWERPProfileStaging(ProcessID);

                        //    if (blResult)
                        //    {
                        //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        //    }
                        //}
                        //else if (FileTypeID == 12)
                        //{   // EQ Odine NSE Transaction Upload

                        //    blResult = RollbackOdinNSETransactionStaging(ProcessID);

                        //    if (blResult)
                        //    {
                        //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        //    }
                        //}
                        //else if (FileTypeID == 13)
                        //{   // EQ Odine BSE Transaction Upload

                        //    blResult = RollbackOdinBSETransactionStaging(ProcessID);

                        //    if (blResult)
                        //    {
                        //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        //    }
                        //}
                        #endregion
                    }
                }
                else
                {

                    Stage = "input";
                    #region CAMS Profile
                    if (FileTypeID == 2)
                    {   // MF CAMS Profile Upload

                        blResult = RollbackCAMSProfile(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }
                    }
                    #endregion
                    #region CAMS Transaction
                    else if (FileTypeID == 1)
                    {   // MF CAMS Transaction Upload

                        blResult = RollbackMFCAMSTransaction(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }
                    }
                    #endregion
                    #region Karvy Transaction
                    else if (FileTypeID == 3)
                    {   // MF Karvy Transaction Upload

                        blResult = RollbackMFKarvyTransaction(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }
                    }
                    #endregion
                    #region Karvy Profile
                    else if (FileTypeID == 4)
                    {   // MF Karvy Profile Upload

                        blResult = RollbackKarvyProfile(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }
                    }
                    #endregion
                    #region Standard Profile

                    else if (FileTypeID == 7)
                    {   // Standard Profile Upload

                        // Rollback from WERP Tables Onwards
                        // Process:
                        // 1) Check if Cust has any details in any of the tables
                        // 2) Remove details from werp tables onwards

                        blResult = RollbackStandardProfile(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }

                    }

                    #endregion
                    #region Standard Equity Trade Account

                    else if (FileTypeID == 13)
                    {   // Standard EQ Trade Account Upload

                        // Rollback from WERP Tables Onwards
                        // Process:
                        // 1) Check if Trade account has any transactions associated to this
                        // 2) Remove details from werp tables onwards

                        blResult = RollbackEQStandardTradeAccount(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }

                    }

                    #endregion
                    #region Standard Equity Transactions
                    else if (FileTypeID == 8)
                    {   // Standard Equity Transaction Upload

                        // Rollback from WERP Table Onwards
                        // Process:
                        // 1) Check if the Net Position Tables have been updated for these transactions
                        // 2) Remove details from werp tables onwards

                        blResult = RollbackEQStandardTransactions(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }

                    }
                    #endregion
                    #region Templeton Profile
                    else if (FileTypeID == 16)
                    {   // MF Karvy Profile Upload


                        blResult = RollbackTempletonProfile(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }

                    }
                    #endregion
                    #region Deutche Profile
                    else if (FileTypeID == 18)
                    {   // MF Deutche Profile Upload


                        blResult = RollbackDeutcheProfile(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }

                    }
                    #endregion
                    #region Templeton Transaction
                    else if (FileTypeID == 15)
                    {   // MF Templeton Transaction Upload

                        blResult = RollbackMFTempletonTransaction(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }
                    }

                    #endregion
                    #region Deutche Transaction
                    else if (FileTypeID == 17)
                    {   // MF Deutche Transaction Upload

                        blResult = RollbackMFDeutcheTransaction(ProcessID, Stage);

                        if (blResult)
                        {
                            blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                        }

                    }
                    #endregion


                    #region Not Used
                    //else if (FileTypeID == 9)
                    //{   // MF WERP Transaction Upload

                    //    blResult = RollbackMFWERPTransactionInput(ProcessID);

                    //    if (blResult)
                    //    {
                    //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                    //    }
                    //}
                    //else if (FileTypeID == 10)
                    //{   // MF WERP Profile Upload

                    //    blResult = RollbackMFWERPProfileInput(ProcessID);

                    //    if (blResult)
                    //    {
                    //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                    //    }
                    //}
                    //else if (FileTypeID == 14)
                    //{   // EQ WERP Transaction Upload

                    //    blResult = RollbackEQWERPTransactionInput(ProcessID);

                    //    if (blResult)
                    //    {
                    //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                    //    }
                    //}
                    //else if (FileTypeID == 15)
                    //{   // EQ WERP Profile Upload

                    //    blResult = RollbackEQWERPProfileInput(ProcessID);

                    //    if (blResult)
                    //    {
                    //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                    //    }
                    //}
                    //else if (FileTypeID == 12)
                    //{   // EQ Odine NSE Transaction Upload

                    //    blResult = RollbackEQOdinNSETransactionInput(ProcessID);

                    //    if (blResult)
                    //    {
                    //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                    //    }
                    //}
                    //else if (FileTypeID == 13)
                    //{   // EQ Odine BSE Transaction Upload

                    //    blResult = RollbackEQOdinBSETransactionInput(ProcessID);

                    //    if (blResult)
                    //    {
                    //        blResult = uploadsCommonDao.UpdateUploadProcessLog(ProcessID, Stage);
                    //    }
                    //}
                    #endregion
                }
            }
            else
            {
                // No Rollback can be done
            }

            return blResult;
        }

        private bool CustMFNetPositionUpdated(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.CustMFNetPositionUpdated(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:CustMFNetPositionUpdated()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool CustEQNetPositionUpdated(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.CustEQNetPositionUpdated(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:CustEQNetPositionUpdated()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool CustomerAssetExists(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.CustomerAssetExists(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:CustomerAssetExists()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool CustomerBankExists(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.CustomerBankExists(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:CustomerBankExists()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool CustomerAssociateExists(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.CustomerAssociateExists(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:CustomerAssociateExists()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public bool CustEQTransactionsExist(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.CustEQTransactionsExist(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:CustomerAssociateExists()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;

        }

        // Rollback Methods from External Onwards
        private bool RollbackOdinNSETransactionXtrnl(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackOdinNSETransactionXtrnl(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackOdinNSETransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackOdinBSETransactionXtrnl(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackOdinBSETransactionXtrnl(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackOdinBSETransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackKarvyProfileXtrnl(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyProfileXtrnl(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyProfileXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackKarvyTransactionXtrnl(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyTransactionXtrnl(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyTransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSTransactionXtrnl(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSTransactionXtrnl(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSTransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSProfileXtrnl(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSProfileXtrnl(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSProfileXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }


        // Rollback Methods from WERP Onwards
        private bool RollbackOdinNSETransactionWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackOdinNSETransactionWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackOdinNSETransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackOdinBSETransactionWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackOdinBSETransactionWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackOdinBSETransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQWERPProfileWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQWERPProfileWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQWERPProfileWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQWERPTransactionWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQWERPTransactionWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQWERPTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackMFWERPProfileWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFWERPProfileWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFWERPProfileWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackMFWERPTransactionWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFWERPTransactionWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFWERPTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackKarvyProfileWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyProfileWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyProfileWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackKarvyTransactionWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyTransactionWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSTransactionWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSTransactionWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSProfileWerp(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSProfileWerp(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSProfileWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }


        // Rollback Methods from Staging Onwards
        private bool RollbackOdinNSETransactionStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackOdinNSETransactionStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackOdinNSETransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackOdinBSETransactionStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackOdinBSETransactionStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackOdinBSETransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQWERPProfileStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQWERPProfileStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQWERPProfileStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQWERPTransactionStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQWERPTransactionStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQWERPTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackMFWERPProfileStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFWERPProfileStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFWERPProfileStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackMFWERPTransactionStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFWERPTransactionStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFWERPTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackKarvyProfileStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyProfileStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyProfileStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackKarvyTransactionStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyTransactionStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSTransactionStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSTransactionStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSProfileStaging(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSProfileStaging(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSProfileStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }


        // Rollback Methods from Input Onwards
        private bool RollbackKarvyProfileInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyProfileInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackKarvyTransactionInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyTransactionInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSTransactionInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSTransactionInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackCAMSProfileInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSProfileInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQOdinNSETransactionInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQOdineNSETransactionInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQOdinNSETransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQOdinBSETransactionInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQOdineBSETransactionInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQOdinBSETransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQWERPProfileInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQWERPProfileInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQWERPProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackEQWERPTransactionInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQWERPTransactionInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQWERPTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackMFWERPProfileInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFWERPProfileInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFWERPProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        private bool RollbackMFWERPTransactionInput(int ProcessID)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFWERPTransactionInput(ProcessID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFWERPTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public int GetProfileUploadRejectCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetProfileUploadRejectCount(processID, source);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetProfileUploadRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public int GetUploadProfileRejectCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetUploadProfileRejectCount(processID, source);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetUploadProfileRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public int GetUploadProfileInputRejectCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetUploadProfileInputRejectCount(processID, source);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetProfileUploadRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }
        //vishal to count the uploaded records
        public int GetUploadFolioUploadCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();


                count = uploadDAO.GetUploadFolioUploadCount(processID, source);

            return count;
        }
        public int GetUploadTransactionInputRejectCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetUploadTransactionInputRejectCount(processID, source);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetUploadTransactionInputRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public int GetUploadTradeAccountInputRejectCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetUploadTradeAccountInputRejectCount(processID, source);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetUploadTradeAccountInputRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        //Methods to include Data Translation during reporcess

        //CAMS
        public bool UploadsCAMSDataTranslationForReprocess(int processId)
        {
            bool result = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                result = uploadscommonDao.UploadsCAMSDataTranslationForReprocess(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonBo.cs:UploadsCAMSDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        //Karvy
        public bool UploadsKarvyDataTranslationForReprocess(int processId)
        {
            bool result = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                result = uploadscommonDao.UploadsKarvyDataTranslationForReprocess(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonBo.cs:UploadsKarvyDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        //Templeton
        public bool UploadsTempletonDataTranslationForReprocess(int processId)
        {
            bool result = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                result = uploadscommonDao.UploadsTempletonDataTranslationForReprocess(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonBo.cs:UploadsTempletonDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        //Deutsche
        public bool UploadsDeutscheDataTranslationForReprocess(int processId)
        {
            bool result = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                result = uploadscommonDao.UploadsDeutscheDataTranslationForReprocess(processId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonBo.cs:UploadsDeutscheDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }




        // Method to Reset Rejected Flag for a Process Id
        public bool ResetRejectedFlagByProcess(int ProcessId, int xmlFileTypeId)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.ResetRejectedFlagByProcess(ProcessId, xmlFileTypeId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:ResetRejectedFlagByProcess()");

                object[] objects = new object[2];
                objects[0] = ProcessId;
                objects[1] = xmlFileTypeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }



        //Checks in Common Transaction staging
        public bool TransCommonChecks(int AdviserID, int processId, string Packagepath, string configPath, string UploadTypeShortForm, string UploadtypeFullName)
        {
            bool IsProcessComplete = false;

            //string strQuery1 = "Update CustomerMFTrasactionStaging set " +
            //"CustomerMFTrasactionStaging.C_CustomerId=E.C_CustomerId ,CustomerMFTrasactionStaging.CMFA_AccountId = D.CMFA_AccountId , " +
            //"CustomerMFTrasactionStaging.CMFTS_AMCCode = C.PA_AMCCode ,CustomerMFTrasactionStaging.PASP_SchemePlanCode = B.[PASP_SchemePlanCode] " +
            //"from CustomerMFTrasactionStaging A inner join ProductAMCSchemeMapping B on A.[CMFTS_SchemeCode]=B.[PASC_AMC_ExternalCode] " +
            //"inner join ProductAMCSchemePlan C on B.[PASP_SchemePlanCode]=C.[PASP_SchemePlanCode] " +
            //"inner join CustomerMutualFundAccount D on A.[CMFTS_FolioNum]=D.[CMFA_FolioNum] and C.[PA_AMCCode]=D.[PA_AMCCode] " +
            //"inner join CustomerPortfolio E on D.CP_PortfolioId=E.CP_PortfolioId inner join Customer F on E.C_CustomerId=F.C_CustomerId " +
            //"inner join AdviserRM G on F.AR_RMId=G.AR_RMId where  B.[PASC_AMC_ExternalType]= '" + UploadtypeFullName + "' and G.A_AdviserId= " + AdviserID + " and A.ADUL_ProcessId = " + processId + " and CMFTS_IsRejected is null";


            //string strQueryChkDuplicateTrans = "Update CustomerMFTrasactionStaging set " +
            //                        "CustomerMFTrasactionStaging.CMFTS_IsRejected = 1, " +
            //                        "CustomerMFTrasactionStaging.WRR_RejectReasonCode = 5 " +
            //                        "from CustomerMFTrasactionStaging  " +
            //                        "inner join CustomerMutualFundTransaction B on " +
            //                        "(CustomerMFTrasactionStaging .CMFA_AccountId = B.CMFA_AccountId) " +
            //                        "and (CustomerMFTrasactionStaging .CMFTS_TransactionNum = B.CMFT_TransactionNumber) " +
            //                        "where B.XES_SourceCode = '" + UploadTypeShortForm + "'" +
            //                        " and CustomerMFTrasactionStaging .CMFTS_IsRejected is null " +
            //                        "and CustomerMFTrasactionStaging .ADUL_ProcessId = " + processId;


            Microsoft.SqlServer.Dts.Runtime.Application App = new Microsoft.SqlServer.Dts.Runtime.Application();
            try
            {
                Package commontransstgcheck = App.LoadPackage(Packagepath, null);
                commontransstgcheck.Variables["varCheckBrokerCode"].Value = processId;
                commontransstgcheck.Variables["varChecksProcessId"].Value = processId;
                commontransstgcheck.Variables["varAssignAMCCode"].Value = processId;
                commontransstgcheck.Variables["varProcessId"].Value = processId;
                commontransstgcheck.Variables["varRejectforAMC"].Value = processId;
                //commontransstgcheck.Variables["varDuplicateTrans"].Value = strQueryChkDuplicateTrans;
                commontransstgcheck.Variables["varCheckBrokerCodeAdvId"].Value = AdviserID;
                commontransstgcheck.Variables["varUploadtypeFullName"].Value = UploadtypeFullName;
                commontransstgcheck.Variables["varUploadTypeShortForm"].Value = UploadTypeShortForm;

                commontransstgcheck.Configurations[0].ConfigurationString = configPath;
                DTSExecResult camsTranResult4 = commontransstgcheck.Execute();
                if (camsTranResult4.ToString() == "Success")
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:TransCommonChecks()");

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

        //Insert transaction from Common Transaction staaging
        public bool InsertMFStandardTransToWERP(int processId, int adviserId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            Microsoft.SqlServer.Dts.Runtime.Application App = new Microsoft.SqlServer.Dts.Runtime.Application();
            try
            {
                Package camsTranPkg4 = App.LoadPackage(Packagepath, null);
                camsTranPkg4.Variables["varProcessId"].Value = processId;
                camsTranPkg4.Variables["varAdviserId"].Value = adviserId;
                camsTranPkg4.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult4 = camsTranPkg4.Execute();
                if (camsTranResult4.ToString() == "Success")
                {
                    //To update the least trans date for the process.
                    DataSet dsmindateval = new DataSet();
                    UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
                    CustomerPortfolioBo customerportfoliobo = new CustomerPortfolioBo();
                    bool valupdated = true;
                    dsmindateval = uploadscommonDao.GetMinDateofUploadTrans(processId, "MF");

                    if (dsmindateval.Tables[0].Rows.Count != 0)
                    {
                        DateTime LeastTransDate = DateTime.Parse(dsmindateval.Tables[0].Rows[0]["MinDate"].ToString());
                        int AdvId = int.Parse(dsmindateval.Tables[0].Rows[0]["AdviserId"].ToString());
                        valupdated = customerportfoliobo.UpdateAdviserDailyEODLogRevaluateForTransaction(AdvId, "MF", LeastTransDate);
                    }


                    if (valupdated)
                    {
                        IsProcessComplete = true;
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertTransDetails()");

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


        //Insert transaction from Common Transaction staaging
        public bool InsertTransToWERP(int processId, string Packagepath, string configPath)
        {
            bool IsProcessComplete = false;
            Microsoft.SqlServer.Dts.Runtime.Application App = new Microsoft.SqlServer.Dts.Runtime.Application();
            try
            {
                Package camsTranPkg4 = App.LoadPackage(Packagepath, null);
                camsTranPkg4.Variables["varProcessId"].Value = processId;
                //camsTranPkg4.Variables["varDelStgValues"].Value = processId;
                camsTranPkg4.ImportConfigurationFile(configPath);
                DTSExecResult camsTranResult4 = camsTranPkg4.Execute();
                if (camsTranResult4.ToString() == "Success")
                {
                    //To update the least trans date for the process.
                    DataSet dsmindateval = new DataSet();
                    UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
                    CustomerPortfolioBo customerportfoliobo = new CustomerPortfolioBo();
                    bool valupdated = true;
                    dsmindateval = uploadscommonDao.GetMinDateofUploadTrans(processId, "MF");

                    if (dsmindateval.Tables[0].Rows.Count != 0)
                    {
                        DateTime LeastTransDate = DateTime.Parse(dsmindateval.Tables[0].Rows[0]["MinDate"].ToString());
                        int AdvId = int.Parse(dsmindateval.Tables[0].Rows[0]["AdviserId"].ToString());
                        valupdated = customerportfoliobo.UpdateAdviserDailyEODLogRevaluateForTransaction(AdvId, "MF", LeastTransDate);
                    }
                    

                    if (valupdated)
                    {
                        IsProcessComplete = true;
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

                FunctionInfo.Add("Method", "CamsUploadsBo.cs:CAMSInsertTransDetails()");

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

        //Populate the Branch drop down
        public DataSet GetAdviserBranchList(int adviserId,string userType)
        {
            DataSet ds = new DataSet();
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                ds = uploadscommonDao.GetAdviserBranchList(adviserId,userType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetAdviserBranchList()");

                object[] objects = new object[1];
                objects[0] = adviserId;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        #region New Rollback Functions

        /// <summary>
        /// Rollsback all the records that were uploaded with this CAMS Profile Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackCAMSProfile(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackCAMSProfile(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackCAMSProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Karvy Profile Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackKarvyProfile(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackKarvyProfile(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackKarvyProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Standard Profile Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackStandardProfile(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackStandardProfile(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackStandardProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Deutche Profile Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        public bool RollbackDeutcheProfile(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackDeutcheProfile(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackDeutcheProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Deutche Profile Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        public bool RollbackTempletonProfile(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackTempletonProfile(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackTempletonProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this CAMS Transaction Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackMFCAMSTransaction(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFCAMSTransaction(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFCAMSTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Karvy Transaction Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackMFKarvyTransaction(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFKarvyTransaction(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFKarvyTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Deutche Transaction Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackMFDeutcheTransaction(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFDeutcheTransaction(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFDeutcheTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Templeton Transaction Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackMFTempletonTransaction(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackMFTempletonTransaction(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackMFTempletonTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Equity Trade Account Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackEQStandardTradeAccount(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQStandardTradeAccount(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQStandardTradeAccount()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        /// <summary>
        /// Rollsback all the records that were uploaded with this Equity standard Transaction Upload process
        /// </summary>
        /// <param name="processId">The Id given to this process</param>
        /// <returns>Returns boolean variable to specify if the Rollback was successful</returns>
        private bool RollbackEQStandardTransactions(int processId, string stage)
        {
            bool blResult = false;
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();

            try
            {
                blResult = uploadscommonDao.RollbackEQStandardTransactions(processId, stage);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:RollbackEQStandardTransactions()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }


        #endregion

        public DataSet GetUploadDistinctProcessIdForAdviser(int AdviserId)
        {
            DataSet ds = new DataSet();
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                ds = uploadscommonDao.GetUploadDistinctProcessIdForAdviser(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetUploadDistinctProcessIdForAdviser()");

                object[] objects = new object[1];
                objects[0] = AdviserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetWERPUploadProcessIdForAdviser(int AdviserId)
        {
            DataSet ds = new DataSet();
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                ds = uploadscommonDao.GetWERPUploadProcessIdForAdviser(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetWERPUploadProcessIdForAdviser()");
                object[] objects = new object[1];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetEquityTradeAccountStagingProcessId(int AdviserId)
        {
            DataSet ds = new DataSet();
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                ds = uploadscommonDao.GetEquityTradeAccountStagingProcessId(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetEquityTradeAccountStagingProcessId()");
                object[] objects = new object[1];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetEquityTransactionStagingProcessId(int AdviserId)
        {
            DataSet ds = new DataSet();
            UploadsCommonDao uploadscommonDao = new UploadsCommonDao();
            try
            {
                ds = uploadscommonDao.GetEquityTransactionStagingProcessId(AdviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetEquityTransactionStagingProcessId()");
                object[] objects = new object[1];
                objects[0] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }


        public int GetUploadSystematicRejectCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetUploadSystematicRejectCount(processID, source);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetUploadProfileRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        public int GetUploadSystematicInsertCount(int processID, string source)
        {
            int count = 0;
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                count = uploadDAO.GetUploadSystematicInsertCount(processID, source);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetUploadSystematicInsertCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }


        public DataSet GetRejectedSIPRecords(int adviserId, int CurrentPage, out int Count, int processId, string RejectReasonFilter, string fileNameFilter, string FolioFilter, string TransactionTypeFilter, string investorNameFileter, string schemeNameFilter)
        {
            DataSet dsSIPRejectedDetails = new DataSet();
            UploadsCommonDao uploadDAO = new UploadsCommonDao();

            try
            {
                dsSIPRejectedDetails = uploadDAO.GetRejectedSIPRecords(adviserId, CurrentPage, out Count, processId, RejectReasonFilter, fileNameFilter, FolioFilter, TransactionTypeFilter, investorNameFileter, schemeNameFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetRejectedSIPRecords()");

                object[] objects = new object[2];
               


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsSIPRejectedDetails;
        }


        public DataSet GetSIPUploadRejectDistinctProcessIdForAdviser(int adviserId)
        {
            DataSet dsSIPRejectedData = new DataSet();
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                dsSIPRejectedData = uploadDAO.GetSIPUploadRejectDistinctProcessIdForAdviser(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonBo.cs:GetSIPUploadRejectDistinctProcessIdForAdviser()");

                object[] objects = new object[2];



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsSIPRejectedData;

        }

        public void DeleteMFSIPTransactionStaging(int StagingID)
        {
            UploadsCommonDao uploadDAO = new UploadsCommonDao();
            try
            {
                uploadDAO.DeleteMFSIPTransactionStaging(StagingID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:DeleteMFSIPTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = StagingID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        
    }
}
