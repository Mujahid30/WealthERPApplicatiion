using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoUploads
{
    public class UploadsCommonDao
    {
        //To retrieve all fieldnames for a type of file used for upload. These fields are used to map column names of 
        //of the file uploaded with fields that must actually be thee for the type of uploaded file

        public DataSet GetColumnNames(int XMLFileTypeId)
        {
            DataSet getColumnNamesds = new DataSet();
            Database db;
            DbCommand getColumnNamesCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getColumnNamesCmd = db.GetStoredProcCommand("SP_GetUploadColumnNames");
                db.AddInParameter(getColumnNamesCmd, "@XMLFileTypeId", DbType.Int16, XMLFileTypeId);
                getColumnNamesds = db.ExecuteDataSet(getColumnNamesCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:GetColumnNames()");

                object[] objects = new object[1];
                objects[0] = XMLFileTypeId;
               

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return getColumnNamesds;
        }

        public string GetLastUploadDate(int advisorid)
        {
            string lastUploadDate="";
            Database db;
            DbCommand getColumnNamesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getColumnNamesCmd = db.GetStoredProcCommand("SP_GetLastUploadDate");
                db.AddInParameter(getColumnNamesCmd, "@advisorId", DbType.Int32, advisorid);
                lastUploadDate = db.ExecuteDataSet(getColumnNamesCmd).Tables[0].Rows[0]["ADUL_StartTime"].ToString();                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:GetLastUploadDate()");
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
            DataSet ds = new DataSet();

            Database db;
            DbCommand getColumnNamesCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getColumnNamesCmd = db.GetStoredProcCommand("SP_GetUploadWERPNameForExternalColumnNames");

                db.AddInParameter(getColumnNamesCmd, "@XMLFileTypeId", DbType.Int32, xmlfiletypeId);
                
                ds = db.ExecuteDataSet(getColumnNamesCmd);
                //dr = getNewCustomersDs.Tables[0].Rows[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CamsUploadsDao.cs:GetColumnNames()");

                object[] objects = new object[1];
                objects[0] = xmlfiletypeId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;

        }

        public int CreateUploadProcess(UploadProcessLogVo processlogVo)
        {
            int processId = 0;
            Database db;
            DbCommand createProcessCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createProcessCmd = db.GetStoredProcCommand("SP_CreateAdvDailyUploadLog");
                db.AddInParameter(createProcessCmd, "@ADUL_FileName", DbType.String, processlogVo.FileName);
                db.AddInParameter(createProcessCmd, "@WUXFT_XMLFileTypeId", DbType.Int32, processlogVo.FileTypeId);
                db.AddInParameter(createProcessCmd, "@A_AdviserId", DbType.Int32, processlogVo.AdviserId);
                db.AddInParameter(createProcessCmd, "@AB_BranchId", DbType.Int32, processlogVo.BranchId);
                db.AddInParameter(createProcessCmd, "@ADUL_IsXMLConvesionComplete", DbType.Int16, processlogVo.IsExternalConversionComplete);
                db.AddInParameter(createProcessCmd, "@ADUL_NoOfTotalRecords", DbType.Int32, processlogVo.NoOfTotalRecords);
                db.AddInParameter(createProcessCmd, "@ADUL_StartTime", DbType.DateTime, processlogVo.StartTime);
                db.AddInParameter(createProcessCmd, "@ADUL_CreatedBy", DbType.Int32, processlogVo.CreatedBy);
                db.AddInParameter(createProcessCmd, "@ADUL_ModifiedBy", DbType.Int32, processlogVo.ModifiedBy);
                db.AddInParameter(createProcessCmd, "@U_UserId", DbType.Int32, processlogVo.UserId);
                db.AddOutParameter(createProcessCmd, "@ADUL_ProcessId", DbType.Int32, 10);
                db.ExecuteNonQuery(createProcessCmd);
                processId = int.Parse(db.GetParameterValue(createProcessCmd, "ADUL_ProcessId").ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonDao.cs:CreateUploadProcess()");


                object[] objects = new object[1];
                objects[0] = processlogVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return processId;
        }

        public UploadProcessLogVo GetProcessLogInfo(int processID)
        {
            UploadProcessLogVo uploadlogvo = new UploadProcessLogVo();
            Database db;
            DbCommand getUploadProceessLogCmd;
            DataSet getUploadProceessLogDs;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getUploadProceessLogCmd = db.GetStoredProcCommand("SP_GetAdvUploadProcessLogInfo");
                db.AddInParameter(getUploadProceessLogCmd, "@processId", DbType.Int32, processID);
                getUploadProceessLogDs = db.ExecuteDataSet(getUploadProceessLogCmd);
                if (getUploadProceessLogDs.Tables[0].Rows.Count > 0)
                {
                    dr = getUploadProceessLogDs.Tables[0].Rows[0];
                    uploadlogvo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                    uploadlogvo.FileName = dr["ADUL_FileName"].ToString();
                    uploadlogvo.FileTypeId = Int32.Parse(dr["WUXFT_XMLFileTypeId"].ToString());
                    uploadlogvo.XMLFileName = dr["ADUL_XMLFileName"].ToString();
                    uploadlogvo.UserId = Int32.Parse(dr["U_UserId"].ToString());
                    uploadlogvo.AdviserId = Int32.Parse(dr["A_AdviserId"].ToString());
                    uploadlogvo.BranchId = Int32.Parse(dr["AB_BranchId"].ToString());
                    uploadlogvo.StartTime = DateTime.Parse(dr["ADUL_StartTime"].ToString());
                    if (dr["ADUL_EndTime"] != null && dr["ADUL_EndTime"].ToString()!=string.Empty)
                        uploadlogvo.EndTime = DateTime.Parse(dr["ADUL_EndTime"].ToString());
                    if (dr["ADUL_TotalNoOfRecords"].ToString() != string.Empty)
                    uploadlogvo.NoOfTotalRecords = int.Parse(dr["ADUL_TotalNoOfRecords"].ToString());
                    if (dr["ADUL_NoOfCustomersCreated"].ToString() != string.Empty)
                    uploadlogvo.NoOfCustomerInserted = int.Parse(dr["ADUL_NoOfCustomersCreated"].ToString());
                    if (dr["ADUL_NoOfAccountsCreated"].ToString() != string.Empty)
                    uploadlogvo.NoOfAccountsInserted = int.Parse(dr["ADUL_NoOfAccountsCreated"].ToString());
                    if (dr["ADUL_NoOfTransactionsCreated"].ToString() != string.Empty)
                    uploadlogvo.NoOfTransactionInserted = int.Parse(dr["ADUL_NoOfTransactionsCreated"].ToString());
                    if (dr["ADUL_NoOfRejectRecords"].ToString() != string.Empty)
                    uploadlogvo.NoOfRejectedRecords = int.Parse(dr["ADUL_NoOfRejectRecords"].ToString());
                    if (dr["ADUL_IsXMLConvesionComplete"].ToString() != string.Empty)
                    uploadlogvo.IsExternalConversionComplete = Int16.Parse(dr["ADUL_IsXMLConvesionComplete"].ToString());
                    if (dr["ADUL_IsInsertionToFirstStagingComplete"].ToString() != string.Empty)
                    uploadlogvo.IsInsertionToFirstStagingComplete = Int16.Parse(dr["ADUL_IsInsertionToFirstStagingComplete"].ToString());
                    if (dr["ADUL_IsInsertionToSecondStagingComplete"].ToString() != string.Empty)
                    uploadlogvo.IsInsertionToSecondStagingComplete = Int16.Parse(dr["ADUL_IsInsertionToSecondStagingComplete"].ToString());
                    if (dr["ADUL_IsInsertionToInputComplete"].ToString() != string.Empty)
                    uploadlogvo.IsInsertionToInputComplete = Int16.Parse(dr["ADUL_IsInsertionToInputComplete"].ToString());
                    if (dr["ADUL_IsInsertionToWerpComplete"].ToString() != string.Empty)
                    uploadlogvo.IsInsertionToWERPComplete = Int16.Parse(dr["ADUL_IsInsertionToWerpComplete"].ToString());
                
                    if (dr["ADUL_NoOfRecordsInserted"].ToString() != "")
                        uploadlogvo.NoOfRecordsInserted = Int32.Parse(dr["ADUL_NoOfRecordsInserted"].ToString());
                    if (dr["ADUL_IsInsertionToXtrnlComplete"].ToString() != "")
                        uploadlogvo.IsInsertionToXtrnlComplete = Int16.Parse(dr["ADUL_IsInsertionToXtrnlComplete"].ToString());
                    if (dr["XUET_ExtractTypeCode"].ToString() != "")
                        uploadlogvo.ExtractTypeCode =(dr["XUET_ExtractTypeCode"].ToString());
                    if (dr["ADUL_NoOfCustomerDuplicates"].ToString() != "")
                        uploadlogvo.NoOfCustomerDuplicates = Int32.Parse(dr["ADUL_NoOfCustomerDuplicates"].ToString());
                    if (dr["ADUL_NoOfAccountDuplicate"].ToString() != "")
                        uploadlogvo.NoOfAccountDuplicates = Int32.Parse(dr["ADUL_NoOfAccountDuplicate"].ToString());
                    if (dr["ADUL_NoOfTransactionDuplicate"].ToString() != "")
                        uploadlogvo.NoOfTransactionDuplicates = Int32.Parse(dr["ADUL_NoOfTransactionDuplicate"].ToString());
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

                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetProcessLogInfo()");


                object[] objects = new object[1];
                objects[0] = processID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return uploadlogvo;
        }

        public bool UpdateUploadProcessLog(UploadProcessLogVo processlogVo)
        {
            bool updatedflag = false;

            Database db;
            DbCommand updateProcessCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateProcessCmd = db.GetStoredProcCommand("SP_UpdateAdvDailyUploadLog");
                db.AddInParameter(updateProcessCmd, "@ADUL_ProcessId", DbType.Int32, processlogVo.ProcessId);
                db.AddInParameter(updateProcessCmd, "@ADUL_FileName", DbType.String, processlogVo.FileName);
                db.AddInParameter(updateProcessCmd, "@WUXFT_XMLFileTypeId", DbType.Int32, processlogVo.FileTypeId);
                db.AddInParameter(updateProcessCmd, "@A_AdviserId", DbType.Int32, processlogVo.AdviserId);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfTotalRecords", DbType.Int32, processlogVo.NoOfTotalRecords);
                db.AddInParameter(updateProcessCmd, "@ADUL_XMLFileName", DbType.String, processlogVo.XMLFileName);
                //db.AddInParameter(updateProcessCmd, "@ADUL_StartTime", DbType.DateTime, processlogVo.StartTime);
                if (processlogVo.StartTime != DateTime.MinValue)
                    db.AddInParameter(updateProcessCmd, "@ADUL_StartTime", DbType.DateTime, processlogVo.StartTime);
                else
                {
                    processlogVo.StartTime = DateTime.Now;
                    db.AddInParameter(updateProcessCmd, "@ADUL_StartTime", DbType.DateTime, processlogVo.StartTime);
                }
                db.AddInParameter(updateProcessCmd, "@ADUL_ModifiedBy", DbType.Int32, processlogVo.ModifiedBy);
                db.AddInParameter(updateProcessCmd, "@U_UserId", DbType.Int32, processlogVo.UserId);
                db.AddInParameter(updateProcessCmd, "@ADUL_Comment", DbType.String, processlogVo.Comment);
                db.AddInParameter(updateProcessCmd, "@ADUL_EndTime", DbType.DateTime,DateTime.Now);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfRejectRecords", DbType.Int32, processlogVo.NoOfRejectedRecords);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfCustomersCreated", DbType.Int32, processlogVo.NoOfCustomerInserted);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfAccountsCreated", DbType.Int32, processlogVo.NoOfAccountsInserted);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfTransactionsCreated", DbType.Int32, processlogVo.NoOfTransactionInserted);
                db.AddInParameter(updateProcessCmd, "@ADUL_IsInsertionToInputComplete", DbType.Int16, processlogVo.IsInsertionToInputComplete);
                db.AddInParameter(updateProcessCmd, "@ADUL_IsInsertionToFirstStagingComplete", DbType.Int16, processlogVo.IsInsertionToFirstStagingComplete);
                db.AddInParameter(updateProcessCmd, "@ADUL_IsInsertionToSecondStagingComplete", DbType.Int16, processlogVo.IsInsertionToSecondStagingComplete);
                db.AddInParameter(updateProcessCmd, "@ADUL_IsInsertionToWerpComplete", DbType.Int16, processlogVo.IsInsertionToWERPComplete);
                db.AddInParameter(updateProcessCmd, "@ADUL_IsInsertionToXtrnlComplete", DbType.Int16, processlogVo.IsInsertionToXtrnlComplete);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfRecordsInserted", DbType.Int32, processlogVo.NoOfRecordsInserted);
                db.AddInParameter(updateProcessCmd, "@XUET_ExtractTypeCode", DbType.String, processlogVo.ExtractTypeCode);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfCustomerDuplicates", DbType.String, processlogVo.NoOfCustomerDuplicates);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfAccountDuplicate", DbType.String, processlogVo.NoOfAccountDuplicates);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfTransactionDuplicate", DbType.String, processlogVo.NoOfTransactionDuplicates);
                db.AddInParameter(updateProcessCmd, "@ADUL_NoOfInputRejects", DbType.String, processlogVo.NoOfInputRejects);
                db.ExecuteNonQuery(updateProcessCmd);
                updatedflag = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonDao.cs:UpdateUploadProcessLog()");


                object[] objects = new object[1];
                objects[0] = processlogVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return updatedflag;
        }

        public bool UpdateUploadProcessLog(int ProcessId, string Stage)
        {
            bool blResult = false;

            Database db;
            DbCommand updateUploadProcessLogCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateUploadProcessLogCmd = db.GetStoredProcCommand("SP_UpdateUploadProcessLogStageWise");
                db.AddInParameter(updateUploadProcessLogCmd, "@ADUL_ProcessId", DbType.Int32, ProcessId);
                db.AddInParameter(updateUploadProcessLogCmd, "@Stage", DbType.String, Stage);
                db.ExecuteNonQuery(updateUploadProcessLogCmd);
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

                FunctionInfo.Add("Method", "UploadCommonDao.cs:UpdateUploadProcessLog()");

                object[] objects = new object[2];
                objects[0] = ProcessId;
                objects[1] = Stage;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return blResult;
        }

        public DataSet GetUploadProcessLogAdmin(int adviserId, int CurrentPage, out int Count, string SortExpression)
        {
            Database db;
            DbCommand getProcessLogCmd;
            DataSet getProcessLogDs;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getProcessLogCmd = db.GetStoredProcCommand("SP_GetAdvDailyUploadLog");
                db.AddInParameter(getProcessLogCmd,"@A_AdviserId",DbType.Int32,adviserId);
                db.AddInParameter(getProcessLogCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getProcessLogCmd, "@processIdSort", DbType.String, SortExpression);
                getProcessLogDs = db.ExecuteDataSet(getProcessLogCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetUploadProcessLogAdmin()");

                object[] objects = new object[2];
                objects[0] = adviserId;
                objects[1] = CurrentPage;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(getProcessLogDs.Tables[1].Rows[0]["CNT"].ToString());

            return getProcessLogDs;
        }

        public bool Rollback(int ProcessId, Int16 FileType)
        {
            bool blResult = false;

            Database db;
            DbCommand rollbackProcessCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                rollbackProcessCmd = db.GetStoredProcCommand("SP_RollbackProcess");
                db.AddInParameter(rollbackProcessCmd, "@ADUL_ProcessId", DbType.Int32, ProcessId);
                db.AddInParameter(rollbackProcessCmd, "@WUXFT_XMLFileTypeId", DbType.Int16, FileType);
                db.ExecuteNonQuery(rollbackProcessCmd);
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

                FunctionInfo.Add("Method", "UploadCommonDao.cs:UpdateUploadProcessLog()");


                object[] objects = new object[2];
                objects[0] = ProcessId;
                objects[1] = FileType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        // Update AMC Code for Folio Reprocess
        public bool UpdateAMCForFolioReprocess(int ProcessId, string type)
        {
            //SP_UploadsUpdateAMCForReprocess
            bool blResult = false;

            Database db;
            DbCommand updateAMCForReprocessCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAMCForReprocessCmd = db.GetStoredProcCommand("SP_UploadsUpdateAMCForFolioReprocess");
                db.AddInParameter(updateAMCForReprocessCmd, "@processId", DbType.Int32, ProcessId);
                db.AddInParameter(updateAMCForReprocessCmd, "@type", DbType.String, type);
                db.ExecuteNonQuery(updateAMCForReprocessCmd);
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

                FunctionInfo.Add("Method", "UploadCommonDao.cs:UpdateAMCForFolioReprocess()");

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

        // Update AMC Code for Transaction Reprocess
        public bool UpdateAMCForTransactionReprocess(int ProcessId, string type)
        {
            //SP_UploadsUpdateAMCForReprocess
            bool blResult = false;

            Database db;
            DbCommand updateAMCForReprocessCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAMCForReprocessCmd = db.GetStoredProcCommand("SP_UploadsUpdateAMCForTransactionReprocess");
                db.AddInParameter(updateAMCForReprocessCmd, "@processId", DbType.Int32, ProcessId);
                db.AddInParameter(updateAMCForReprocessCmd, "@type", DbType.String, type);
                db.ExecuteNonQuery(updateAMCForReprocessCmd);
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

                FunctionInfo.Add("Method", "UploadCommonDao.cs:UpdateAMCForTransactionReprocess()");

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

        //Get the count for number of transactions that are uploaded into WERP
        public int GetTransUploadCount(int processID, string type)
        {
            int count= 0;

            Database db;
            DbCommand getCount;
            
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetCommonTransUploadCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@type", DbType.String, type);
                
                string result = (db.ExecuteScalar(getCount).ToString());
                count = int.Parse(result);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetTransUploadCount()");

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

        
        //Get the count for number of transactions that are rejected for an Upload into WERP
        public int GetTransUploadRejectCount(int processID, string assetType)
        {
            int count = 0;

            Database db;
            DbCommand getCount;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetCommonTransUploadRejectsCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@assetType", DbType.String, assetType);
                db.AddOutParameter(getCount, "@TotalRejectedRecords", DbType.Int32, 5000);
                db.ExecuteNonQuery(getCount);
                count  = Convert.ToInt32(db.GetParameterValue(getCount, "@TotalRejectedRecords"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetTransUploadRejectCount()");

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

        //Get the count for number of profiles that are rejected for a Profile Upload 
        public int GetProfileUploadRejectCount(int processID, string source)
        {
            int count = 0;

            Database db;
            DbCommand getCount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetProfileUploadRejectCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@source", DbType.String, source);
                db.AddOutParameter(getCount, "@totalRejectedCount", DbType.Int32, 5000);
                
                if(db.ExecuteNonQuery(getCount) != 0)
                    count = int.Parse(db.GetParameterValue(getCount, "totalRejectedCount").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetProfileUploadRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        //Get the count for number of profiles that are rejected in Profile Common staging for a Profile Upload 
        public int GetUploadProfileRejectCount(int processID, string source)
        {
            int count = 0;

            Database db;
            DbCommand getCount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetUploadProfileRejectCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@source", DbType.String, source);
                db.AddOutParameter(getCount, "@totalRejectedCount", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(getCount) != 0)
                    count = int.Parse(db.GetParameterValue(getCount, "totalRejectedCount").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetUploadProfileRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        //Get the count for number of profiles that are rejected in Input table for a Profile Upload 
        public int GetUploadProfileInputRejectCount(int processID, string source)
        {
            int count = 0;

            Database db;
            DbCommand getCount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetProfileUploadInputRejectCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@source", DbType.String, source);
                db.AddOutParameter(getCount, "@InputStageRejects", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(getCount) != 0)
                    count = int.Parse(db.GetParameterValue(getCount, "InputStageRejects").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetProfileUploadRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }
        //Vishal count number of uploaded records
        public int GetUploadFolioUploadCount(int processID, string source)
        {
            int count = 0;

            Database db;
            DbCommand getCount;
           
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_CountInsertedFolioRejected");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@source", DbType.String, source);
                db.AddOutParameter(getCount, "@InputStageValue", DbType.Int32, 1000);

                db.ExecuteNonQuery(getCount);
                count = Convert.ToInt32(db.GetParameterValue(getCount, "@InputStageValue").ToString());
                //string result = (db.ExecuteScalar(getCount).ToString());
                //count = int.Parse(result);
                   


            return count;
        }

        //Get the count for number of profiles that are rejected in Input table for a Transaction Upload 
        public int GetUploadTransactionInputRejectCount(int processID, string source)
        {
            int count = 0;

            Database db;
            DbCommand getCount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetTransactionUploadInputRejectCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@assetType", DbType.String, source);
                db.AddOutParameter(getCount, "@InputStageValue", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(getCount) != 0)
                    count = int.Parse(db.GetParameterValue(getCount, "InputStageValue").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetUploadTransactionInputRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        //Get the count for number of profiles that are rejected in Input table for a Transaction Upload 
        public int GetUploadTradeAccountInputRejectCount(int processID, string source)
        {
            int count = 0;

            Database db;
            DbCommand getCount;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetTradeAccountUploadInputRejectsCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@assetType", DbType.String, source);
                db.AddOutParameter(getCount, "@InputStageValue", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(getCount) != 0)
                    count = int.Parse(db.GetParameterValue(getCount, "InputStageValue").ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetUploadTradeAccountInputRejectCount()");

                object[] objects = new object[2];
                objects[0] = processID;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return count;
        }

        //Get the count for number of accounts/folio that are uploaded into WERP
        public int GetAccountsUploadCount(int processID, string type)
        {
            int count = 0;

            Database db;
            DbCommand getCount;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetCommonAccountsUploadCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@type", DbType.String, type);

                string result = (db.ExecuteScalar(getCount).ToString());
                count = int.Parse(result);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetTransUploadCount()");

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

        //Get the count for number of accounts/folio that are rejected for an Upload into WERP
        public int GetAccountsUploadRejectCount(int processID, string assetType)
        {
            int count = 0;

            Database db;
            DbCommand getCount;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCount = db.GetStoredProcCommand("SP_GetCommonAccountsUploadRejectsCount");
                db.AddInParameter(getCount, "@processId", DbType.Int32, processID);
                db.AddInParameter(getCount, "@assetType", DbType.String, assetType);
                db.AddOutParameter(getCount, "@TotalRejectedRecords", DbType.Int32, 5000);
                db.ExecuteNonQuery(getCount);
                count = Convert.ToInt32(db.GetParameterValue(getCount, "@TotalRejectedRecords"));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:GetTransUploadRejectCount()");

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

        public bool CustMFNetPositionUpdated(int ProcessID)
        {
            bool blResult = false;
            Database db;
            DbCommand cmdCustMFNetPositionUpdated;
            DataSet ds;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustMFNetPositionUpdated = db.GetStoredProcCommand("SP_IsCustMFNetPositionUpdatedByProcessId");
                db.AddInParameter(cmdCustMFNetPositionUpdated, "@processId", DbType.Int32, ProcessID);
                ds = db.ExecuteDataSet(cmdCustMFNetPositionUpdated);

                // If the count is greater than 0, then return true
                if (Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:CustMFNetPositionUpdated()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool CustEQNetPositionUpdated(int ProcessID)
        {
            bool blResult = false;
            Database db;
            DbCommand cmdCustEQNetPositionUpdated;
            DataSet ds;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustEQNetPositionUpdated = db.GetStoredProcCommand("SP_IsCustEQNetPositionUpdatedByProcessId");
                db.AddInParameter(cmdCustEQNetPositionUpdated, "@processId", DbType.Int32, ProcessID);
                ds = db.ExecuteDataSet(cmdCustEQNetPositionUpdated);

                // If the count is greater than 0, then return true
                if (Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:CustEQNetPositionUpdated()");

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
            Database db;
            DbCommand cmdCustEQNetTrans;
            DataSet ds;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustEQNetTrans = db.GetStoredProcCommand("SP_DoesEQTransactionExistByProcessId");
                db.AddInParameter(cmdCustEQNetTrans, "@processId", DbType.Int32, ProcessID);
                ds = db.ExecuteDataSet(cmdCustEQNetTrans);

                // If the count is greater than 0, then return true
                if (Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:CustEQTransactionsExist()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool CustomerAssetExists(int ProcessID)
        {
            bool blResult = false;
            Database db;
            DbCommand cmdCustomerAssetExists;
            DataSet ds;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerAssetExists = db.GetStoredProcCommand("SP_DoesCustomerAssetExistByProcessId");
                db.AddInParameter(cmdCustomerAssetExists, "@processId", DbType.Int32, ProcessID);
                ds = db.ExecuteDataSet(cmdCustomerAssetExists);

                // If the count is greater than 0, then return true
                if (Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:CustomerAssetExists()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool CustomerBankExists(int ProcessID)
        {
            bool blResult = false;
            Database db;
            DbCommand cmdCustomerBankExists;
            DataSet ds;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerBankExists = db.GetStoredProcCommand("SP_DoesCustomerBankExistByProcessId");
                db.AddInParameter(cmdCustomerBankExists, "@processId", DbType.Int32, ProcessID);
                ds = db.ExecuteDataSet(cmdCustomerBankExists);
                
                // If the count is greater than 0, then return true
                if (Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:CustomerBankExists()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool CustomerAssociateExists(int ProcessID)
        {
            bool blResult = false;
            Database db;
            DbCommand cmdCustomerAssociateExists;
            DataSet ds;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerAssociateExists = db.GetStoredProcCommand("SP_DoesCustomerAssociateExistByProcessId");
                db.AddInParameter(cmdCustomerAssociateExists, "@processId", DbType.Int32, ProcessID);
                ds = db.ExecuteDataSet(cmdCustomerAssociateExists);

                // If the count is greater than 0, then return true
                if (Int32.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:CustomerAssociateExists()");

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
        public bool RollbackOdinNSETransactionXtrnl(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdOdinNSETransactionXtrnl;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOdinNSETransactionXtrnl = db.GetStoredProcCommand("SP_RollbackOdinNSETransactionXtrnl");
                db.AddInParameter(cmdOdinNSETransactionXtrnl, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdOdinNSETransactionXtrnl);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackOdinNSETransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackOdinBSETransactionXtrnl(int ProcessID)
        {
            bool blResult = false;
            Database db;
            DbCommand cmdOdinBSETransactionXtrnl;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOdinBSETransactionXtrnl = db.GetStoredProcCommand("SP_RollbackOdinBSETransactionXtrnl");
                db.AddInParameter(cmdOdinBSETransactionXtrnl, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdOdinBSETransactionXtrnl);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackOdinBSETransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackKarvyProfileXtrnl(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdKarvyProfileXtrnl;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyProfileXtrnl = db.GetStoredProcCommand("SP_RollbackKarvyProfileXtrnl");
                db.AddInParameter(cmdKarvyProfileXtrnl, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdKarvyProfileXtrnl);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyProfileXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackKarvyTransactionXtrnl(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdKarvyTransactionXtrnl;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyTransactionXtrnl = db.GetStoredProcCommand("SP_RollbackKarvyTransactionXtrnl");
                db.AddInParameter(cmdKarvyTransactionXtrnl, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdKarvyTransactionXtrnl);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyTransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSTransactionXtrnl(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSTransactionXtrnl;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSTransactionXtrnl = db.GetStoredProcCommand("SP_RollbackCAMSTransactionXtrnl");
                db.AddInParameter(cmdCAMSTransactionXtrnl, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSTransactionXtrnl);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSTransactionXtrnl()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSProfileXtrnl(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSProfileXtrnl;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSProfileXtrnl = db.GetStoredProcCommand("SP_RollbackCAMSProfileXtrnl");
                db.AddInParameter(cmdCAMSProfileXtrnl, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSProfileXtrnl);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSProfileXtrnl()");

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
        public bool RollbackOdinNSETransactionWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdOdinNSETransactionWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOdinNSETransactionWerp = db.GetStoredProcCommand("SP_RollbackOdinNSETransactionWerp");
                db.AddInParameter(cmdOdinNSETransactionWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdOdinNSETransactionWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackOdinNSETransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackOdinBSETransactionWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdOdinBSETransactionWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOdinBSETransactionWerp = db.GetStoredProcCommand("SP_RollbackOdinBSETransactionWerp");
                db.AddInParameter(cmdOdinBSETransactionWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdOdinBSETransactionWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackOdinBSETransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQWERPProfileWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQWERPProfileWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQWERPProfileWerp = db.GetStoredProcCommand("SP_RollbackEQWERPProfileWerp");
                db.AddInParameter(cmdEQWERPProfileWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQWERPProfileWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQWERPProfileWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQWERPTransactionWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQWERPTransactionWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQWERPTransactionWerp = db.GetStoredProcCommand("SP_RollbackEQWERPTransactionWerp");
                db.AddInParameter(cmdEQWERPTransactionWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQWERPTransactionWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQWERPTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackMFWERPProfileWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdMFWERPProfileWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdMFWERPProfileWerp = db.GetStoredProcCommand("SP_RollbackMFWERPProfileWerp");
                db.AddInParameter(cmdMFWERPProfileWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdMFWERPProfileWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFWERPProfileWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackMFWERPTransactionWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdMFWERPTransactionWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdMFWERPTransactionWerp = db.GetStoredProcCommand("SP_RollbackMFWERPTransactionWerp");
                db.AddInParameter(cmdMFWERPTransactionWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdMFWERPTransactionWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFWERPTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackKarvyProfileWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdKarvyProfileWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyProfileWerp = db.GetStoredProcCommand("SP_RollbackKarvyProfileWerp");
                db.AddInParameter(cmdKarvyProfileWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdKarvyProfileWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyProfileWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackKarvyTransactionWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdKarvyTransactionWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyTransactionWerp = db.GetStoredProcCommand("SP_RollbackKarvyTransactionWerp");
                db.AddInParameter(cmdKarvyTransactionWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdKarvyTransactionWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSTransactionWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSTransactionWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSTransactionWerp = db.GetStoredProcCommand("SP_RollbackCAMSTransactionWerp");
                db.AddInParameter(cmdCAMSTransactionWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSTransactionWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSTransactionWerp()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSProfileWerp(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSProfileWerp;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSProfileWerp = db.GetStoredProcCommand("SP_RollbackCAMSProfileWerp");
                db.AddInParameter(cmdCAMSProfileWerp, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSProfileWerp);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSProfileWerp()");

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
        public bool RollbackOdinNSETransactionStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdOdinNSETransactionStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOdinNSETransactionStaging = db.GetStoredProcCommand("SP_RollbackOdinNSETransactionStaging");
                db.AddInParameter(cmdOdinNSETransactionStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdOdinNSETransactionStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackOdinNSETransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackOdinBSETransactionStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdOdinBSETransactionStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdOdinBSETransactionStaging = db.GetStoredProcCommand("SP_RollbackOdinBSETransactionStaging");
                db.AddInParameter(cmdOdinBSETransactionStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdOdinBSETransactionStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackOdinBSETransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQWERPProfileStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQWERPProfileStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQWERPProfileStaging = db.GetStoredProcCommand("SP_RollbackEQWERPProfileStaging");
                db.AddInParameter(cmdEQWERPProfileStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQWERPProfileStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQWERPProfileStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQWERPTransactionStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQWERPTransactionStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQWERPTransactionStaging = db.GetStoredProcCommand("SP_RollbackEQWERPTransactionStaging");
                db.AddInParameter(cmdEQWERPTransactionStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQWERPTransactionStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQWERPTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackMFWERPProfileStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdMFWERPProfileStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdMFWERPProfileStaging = db.GetStoredProcCommand("SP_RollbackMFWERPProfileStaging");
                db.AddInParameter(cmdMFWERPProfileStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdMFWERPProfileStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFWERPProfileStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackMFWERPTransactionStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdMFWERPTransactionStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdMFWERPTransactionStaging = db.GetStoredProcCommand("SP_RollbackMFWERPTransactionStaging");
                db.AddInParameter(cmdMFWERPTransactionStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdMFWERPTransactionStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFWERPTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackKarvyProfileStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdKarvyProfileStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyProfileStaging = db.GetStoredProcCommand("SP_RollbackKarvyProfileStaging");
                db.AddInParameter(cmdKarvyProfileStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdKarvyProfileStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyProfileStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackKarvyTransactionStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdKarvyTransactionStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyTransactionStaging = db.GetStoredProcCommand("SP_RollbackKarvyTransactionStaging");
                db.AddInParameter(cmdKarvyTransactionStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdKarvyTransactionStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSTransactionStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSTransactionStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSTransactionStaging = db.GetStoredProcCommand("SP_RollbackCAMSTransactionStaging");
                db.AddInParameter(cmdCAMSTransactionStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSTransactionStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSProfileStaging(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSProfileStaging;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSProfileStaging = db.GetStoredProcCommand("SP_RollbackCAMSProfileStaging");
                db.AddInParameter(cmdCAMSProfileStaging, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSProfileStaging);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSProfileStaging()");

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
        public bool RollbackKarvyProfileInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSProfileInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSProfileInput = db.GetStoredProcCommand("SP_RollbackKarvyProfileInput");
                db.AddInParameter(cmdCAMSProfileInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSProfileInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackKarvyTransactionInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSProfileInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSProfileInput = db.GetStoredProcCommand("SP_RollbackKarvyTransactionInput");
                db.AddInParameter(cmdCAMSProfileInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSProfileInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSTransactionInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSTransactionInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSTransactionInput = db.GetStoredProcCommand("SP_RollbackCAMSTransactionInput");
                db.AddInParameter(cmdCAMSTransactionInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSTransactionInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackCAMSProfileInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdCAMSProfileInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSProfileInput = db.GetStoredProcCommand("SP_RollbackCAMSProfileInput");
                db.AddInParameter(cmdCAMSProfileInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdCAMSProfileInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackMFWERPProfileInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdMFWERPProfileInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdMFWERPProfileInput = db.GetStoredProcCommand("SP_RollbackMFWERPProfileInput");
                db.AddInParameter(cmdMFWERPProfileInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdMFWERPProfileInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFWERPProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackMFWERPTransactionInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdMFWERPTransactionInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdMFWERPTransactionInput = db.GetStoredProcCommand("SP_RollbackMFWERPTransactionInput");
                db.AddInParameter(cmdMFWERPTransactionInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdMFWERPTransactionInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFWERPTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQOdineNSETransactionInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQOdineTransactionInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQOdineTransactionInput = db.GetStoredProcCommand("SP_RollbackEQOdineNSETransactionInput");
                db.AddInParameter(cmdEQOdineTransactionInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQOdineTransactionInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQOdineNSETransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQOdineBSETransactionInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQOdineTransactionInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQOdineTransactionInput = db.GetStoredProcCommand("SP_RollbackEQOdineBSETransactionInput");
                db.AddInParameter(cmdEQOdineTransactionInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQOdineTransactionInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQOdineBSETransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQWERPProfileInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQWERPProfileInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQWERPProfileInput = db.GetStoredProcCommand("SP_RollbackEQWERPProfileInput");
                db.AddInParameter(cmdEQWERPProfileInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQWERPProfileInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQWERPProfileInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool RollbackEQWERPTransactionInput(int ProcessID)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdEQWERPTransactionInput;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQWERPTransactionInput = db.GetStoredProcCommand("SP_RollbackEQWERPTransactionInput");
                db.AddInParameter(cmdEQWERPTransactionInput, "@processId", DbType.Int32, ProcessID);
                db.ExecuteNonQuery(cmdEQWERPTransactionInput);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQWERPTransactionInput()");

                object[] objects = new object[1];
                objects[0] = ProcessID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        //Methods to include Data Translation during reporcess
        //CAMS
        public bool UploadsCAMSDataTranslationForReprocess(int processId)
        {
            Database db;
            DbCommand cmdCheckDataTrans;


            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckDataTrans = db.GetStoredProcCommand("SP_UploadsCAMSDataTranslationForReprocess");
                db.AddInParameter(cmdCheckDataTrans, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(cmdCheckDataTrans);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:UploadsCAMSDataTranslationForReprocess()");

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
            Database db;
            DbCommand cmdCheckDataTrans;


            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckDataTrans = db.GetStoredProcCommand("SP_UploadsKarvyDataTranslationForReprocess");
                db.AddInParameter(cmdCheckDataTrans, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(cmdCheckDataTrans);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:UploadsKarvyDataTranslationForReprocess()");

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
            Database db;
            DbCommand cmdCheckDataTrans;


            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckDataTrans = db.GetStoredProcCommand("SP_UploadsTempletonDataTranslationForReprocess");
                db.AddInParameter(cmdCheckDataTrans, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(cmdCheckDataTrans);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:UploadsTempletonDataTranslationForReprocess()");

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
            Database db;
            DbCommand cmdCheckDataTrans;


            bool result = false;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCheckDataTrans = db.GetStoredProcCommand("SP_UploadsDeutscheDataTranslationForReprocess");
                db.AddInParameter(cmdCheckDataTrans, "@processId", DbType.Int32, processId);
                db.ExecuteNonQuery(cmdCheckDataTrans);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:UploadsDeutscheDataTranslationForReprocess()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }


        // Methods to reset Rejected Flags based on Process Id
        public bool ResetRejectedFlagByProcess(int ProcessID, int xmlFileTypeId)
        {
            bool blResult = false;

            Database db;
            DbCommand cmdRejectedFlagReset;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdRejectedFlagReset = db.GetStoredProcCommand("SP_ResetRejectedFlagByProcess");
                db.AddInParameter(cmdRejectedFlagReset, "@processId", DbType.Int32, ProcessID);
                db.AddInParameter(cmdRejectedFlagReset, "@xmlFileTypeId", DbType.Int32, xmlFileTypeId);
                db.ExecuteNonQuery(cmdRejectedFlagReset);
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:ResetRejectedFlagByProcess()");

                object[] objects = new object[2];
                objects[0] = ProcessID;
                objects[1] = xmlFileTypeId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool ProcessTransactionStaging()
        {
            bool blResult = false;

            Database db;
            

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
               
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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:ProcessTransactionStaging()");

                object[] objects = new object[0];
            

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public bool InsertTransactionsIntoWERP()
        {
            bool blResult = false;

            Database db;
            

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

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

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:InsertTransactionsIntoWERP()");

                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }

        public DataSet GetAdviserBranchList(int adviserId,string userType)
        {
            Database db;
            DbCommand cmd;
            DataSet ds=null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetAdviserBranchList");
                db.AddInParameter(cmd , "@Id", DbType.Int32, adviserId);
                db.AddInParameter(cmd, "@userType", DbType.String, userType);

                ds = db.ExecuteDataSet(cmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetAdviserBranchList()");

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

        public bool RollbackCAMSProfile(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdCAMSProfile;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCAMSProfile = db.GetStoredProcCommand("SP_RollbackCAMSProfileWerp");
                db.AddInParameter(cmdCAMSProfile, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdCAMSProfile, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdCAMSProfile);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackCAMSProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackKarvyProfile(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdKarvyProfile;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyProfile = db.GetStoredProcCommand("SP_RollbackKarvyProfileWerp");
                db.AddInParameter(cmdKarvyProfile, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdKarvyProfile, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdKarvyProfile);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackKarvyProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackStandardProfile(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdStdProfile;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdStdProfile = db.GetStoredProcCommand("SP_RollbackStdProfileWerp");
                db.AddInParameter(cmdStdProfile, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdStdProfile, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdStdProfile);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackStandardProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackDeutcheProfile(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdDeutcheProfile;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeutcheProfile = db.GetStoredProcCommand("SP_RollbackDeutcheProfileWerp");
                db.AddInParameter(cmdDeutcheProfile, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdDeutcheProfile, "@stage", DbType.String, stage);
                cmdDeutcheProfile.CommandTimeout = 60 * 60;
                db.ExecuteNonQuery(cmdDeutcheProfile);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackDeutcheProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackTempletonProfile(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdTempletonProfile;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdTempletonProfile = db.GetStoredProcCommand("SP_RollbackTempletonProfileWerp");
                db.AddInParameter(cmdTempletonProfile, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdTempletonProfile, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdTempletonProfile);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackTempletonProfile()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackMFCAMSTransaction(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdCamsTransaction;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCamsTransaction = db.GetStoredProcCommand("SP_RollbackCAMSTransactionWerp");
                db.AddInParameter(cmdCamsTransaction, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdCamsTransaction, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdCamsTransaction);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFCAMSTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackMFKarvyTransaction(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdKarvyTransaction;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdKarvyTransaction = db.GetStoredProcCommand("SP_RollbackKarvyTransactionWerp");
                db.AddInParameter(cmdKarvyTransaction, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdKarvyTransaction, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdKarvyTransaction);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFKarvyTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackMFDeutcheTransaction(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdDeutcheTransaction;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeutcheTransaction = db.GetStoredProcCommand("SP_RollbackDeutcheTransactionWerp");
                db.AddInParameter(cmdDeutcheTransaction, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdDeutcheTransaction, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdDeutcheTransaction);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFDeutcheTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackMFTempletonTransaction(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdTempletonTransaction;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdTempletonTransaction = db.GetStoredProcCommand("SP_RollbackTempletonTransactionWerp");
                db.AddInParameter(cmdTempletonTransaction, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdTempletonTransaction, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdTempletonTransaction);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackMFTempletonTransaction()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackEQStandardTradeAccount(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdEQStdTradeAcc;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQStdTradeAcc = db.GetStoredProcCommand("SP_RollbackStdEQTradeAccWerp");
                db.AddInParameter(cmdEQStdTradeAcc, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdEQStdTradeAcc, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdEQStdTradeAcc);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQStandardTradeAccount()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }

        public bool RollbackEQStandardTransactions(int processId, string stage)
        {
            bool b1Result = false;
            Database db;
            DbCommand cmdEQStdTransaction;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdEQStdTransaction = db.GetStoredProcCommand("SP_RollbackStdEQTransactionWerp");
                db.AddInParameter(cmdEQStdTransaction, "@processId", DbType.Int32, processId);
                db.AddInParameter(cmdEQStdTransaction, "@stage", DbType.String, stage);
                db.ExecuteNonQuery(cmdEQStdTransaction);
                b1Result = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "UploadsCommonDao.cs:RollbackEQStandardTransactions()");

                object[] objects = new object[1];
                objects[0] = processId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return b1Result;
        }



        #endregion

        public DataSet GetMinDateofUploadTrans(int ProcessID, string AssetGroup)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetMinDateofUploadTrans");
                db.AddInParameter(cmd, "@processId", DbType.Int32, ProcessID);
                db.AddInParameter(cmd, "@AssetGroup", DbType.String, AssetGroup);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetMinDateofUploadTrans()");
                object[] objects = new object[2];
                objects[2] = ProcessID;
                objects[1] = AssetGroup;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetUploadDistinctProcessIdForAdviser(int AdviserId)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetUploadDistinctProcessIdForAdviser");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, AdviserId);                
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetUploadDistinctProcessIdForAdviser()");
                object[] objects = new object[1];
                objects[2] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetWERPUploadProcessIdForAdviser(int AdviserId)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetWERPUploadProcessIdForAdviser");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, AdviserId);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetWERPUploadProcessIdForAdviser()");
                object[] objects = new object[1];
                objects[2] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetEquityTradeAccountStagingProcessId(int AdviserId)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetEquityTradeAccountStagingProcessId");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, AdviserId);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetEquityTradeAccountStagingProcessId()");
                object[] objects = new object[1];
                objects[2] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
        public DataSet GetEquityTransactionStagingProcessId(int AdviserId)
        {
            Database db;
            DbCommand cmd;
            DataSet ds = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetCustomerEqTransactionStagingProcessId");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, AdviserId);
                ds = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "UploadCommonDao.cs:GetEquityTransactionStagingProcessId()");
                object[] objects = new object[1];
                objects[2] = AdviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }
    }
}
