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
    public class RejectedRecordsDao
    {
       
        public DataSet getCAMSRejectedProfiles(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string PANFilter, string RejectReasonFilter, string NameFilter, string FolioFilter, string DoesCustExistFilter)
        {
            DataSet dsGetCAMSRejectedProfiles;
            Database db;
            DbCommand getCAMSRejectedProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCAMSRejectedProfilesCmd = db.GetStoredProcCommand("SP_GetCAMSUploadRejectsProfile");
                db.AddInParameter(getCAMSRejectedProfilesCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getCAMSRejectedProfilesCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getCAMSRejectedProfilesCmd, "@processIdSortOrder", DbType.String, SortExpression);
                if (IsRejectedFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isRejectedFilter", DbType.String, IsRejectedFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isRejectedFilter", DbType.String, DBNull.Value);
                if (PANFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@panFilter", DbType.String, PANFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@panFilter", DbType.String, DBNull.Value);
                if (FolioFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@folioFilter", DbType.String, FolioFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@folioFilter", DbType.String, DBNull.Value);
                if (NameFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@nameFilter", DbType.String, NameFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (RejectReasonFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, DBNull.Value);
                if (DoesCustExistFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isCustomerExistingFilter", DbType.String, DoesCustExistFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isCustomerExistingFilter", DbType.String, DBNull.Value);

                dsGetCAMSRejectedProfiles = db.ExecuteDataSet(getCAMSRejectedProfilesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getCAMSRejectedProfiles()");

                object[] objects = new object[9];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = PANFilter;
                objects[5] = RejectReasonFilter;
                objects[6] = NameFilter;
                objects[7] = FolioFilter;
                objects[8] = DoesCustExistFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetCAMSRejectedProfiles.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetCAMSRejectedProfiles;
        }
        public DataSet getMFRejectedFolios(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string PANFilter, string RejectReasonFilter, string NameFilter, string FolioFilter, string DoesCustExistFilter)
        {
            DataSet dsGetCAMSRejectedProfiles;
            Database db;
            DbCommand getCAMSRejectedProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCAMSRejectedProfilesCmd = db.GetStoredProcCommand("SP_GetMFUploadRejectsFolios");
                db.AddInParameter(getCAMSRejectedProfilesCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getCAMSRejectedProfilesCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getCAMSRejectedProfilesCmd, "@processIdSortOrder", DbType.String, SortExpression);
                if (IsRejectedFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isRejectedFilter", DbType.String, IsRejectedFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isRejectedFilter", DbType.String, DBNull.Value);
                if (PANFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@panFilter", DbType.String, PANFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@panFilter", DbType.String, DBNull.Value);
                if (FolioFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@folioFilter", DbType.String, FolioFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@folioFilter", DbType.String, DBNull.Value);
                if (NameFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@nameFilter", DbType.String, NameFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (RejectReasonFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, DBNull.Value);
                if (DoesCustExistFilter != "")
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isCustomerExistingFilter", DbType.String, DoesCustExistFilter);
                else
                    db.AddInParameter(getCAMSRejectedProfilesCmd, "@isCustomerExistingFilter", DbType.String, DBNull.Value);

                dsGetCAMSRejectedProfiles = db.ExecuteDataSet(getCAMSRejectedProfilesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getMFRejectedFolios()");

                object[] objects = new object[9];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = PANFilter;
                objects[5] = RejectReasonFilter;
                objects[6] = NameFilter;
                objects[7] = FolioFilter;
                objects[8] = DoesCustExistFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetCAMSRejectedProfiles.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetCAMSRejectedProfiles;
        }
        public DataSet getCAMSRejectedTrans(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string RejectReasonFilter, string FolioFilter)
        {
            DataSet dsGetKarvyRejectedProfiles;
            Database db;
            DbCommand getKarvyRejectedProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getKarvyRejectedProfilesCmd = db.GetStoredProcCommand("SP_GetCAMSUploadRejectsTransaction");
                db.AddInParameter(getKarvyRejectedProfilesCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getKarvyRejectedProfilesCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getKarvyRejectedProfilesCmd, "@processIdSortOrder", DbType.String, SortExpression);
                if (IsRejectedFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@isRejectedFilter", DbType.String, IsRejectedFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@isRejectedFilter", DbType.String, DBNull.Value);
                if (FolioFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@folioFilter", DbType.String, FolioFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@folioFilter", DbType.String, DBNull.Value);
                if (RejectReasonFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, DBNull.Value);

                dsGetKarvyRejectedProfiles = db.ExecuteDataSet(getKarvyRejectedProfilesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getCAMSRejectedTrans()");

                object[] objects = new object[6];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = RejectReasonFilter;
                objects[5] = FolioFilter;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetKarvyRejectedProfiles.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetKarvyRejectedProfiles;
        }

        public DataSet getKarvyRejectedProfile(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string PANFilter, string RejectReasonFilter, string NameFilter, string FolioFilter, string DoesCustExistFilter)
        {
            DataSet dsGetKarvyRejectedProfiles;
            Database db;
            DbCommand getKarvyRejectedProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getKarvyRejectedProfilesCmd = db.GetStoredProcCommand("SP_GetKarvyUploadRejectsProfile");
                db.AddInParameter(getKarvyRejectedProfilesCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getKarvyRejectedProfilesCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getKarvyRejectedProfilesCmd, "@processIdSortOrder", DbType.String, SortExpression);

                if (IsRejectedFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@isRejectedFilter", DbType.String, IsRejectedFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@isRejectedFilter", DbType.String, DBNull.Value);
                if (PANFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@panFilter", DbType.String, PANFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@panFilter", DbType.String, DBNull.Value);
                if (FolioFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@folioFilter", DbType.String, FolioFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@folioFilter", DbType.String, DBNull.Value);
                if (NameFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@nameFilter", DbType.String, NameFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@nameFilter", DbType.String, DBNull.Value);
                if (RejectReasonFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, DBNull.Value);
                if (DoesCustExistFilter != "")
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@isCustomerExistingFilter", DbType.String, DoesCustExistFilter);
                else
                    db.AddInParameter(getKarvyRejectedProfilesCmd, "@isCustomerExistingFilter", DbType.String, DBNull.Value);

                dsGetKarvyRejectedProfiles = db.ExecuteDataSet(getKarvyRejectedProfilesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getKarvyRejectedProfile()");

                object[] objects = new object[9];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = PANFilter;
                objects[5] = RejectReasonFilter;
                objects[6] = NameFilter;
                objects[7] = FolioFilter;
                objects[8] = DoesCustExistFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetKarvyRejectedProfiles.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetKarvyRejectedProfiles;
        }

        public DataSet getKarvyRejectedTrans(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string RejectReasonFilter, string FolioFilter)
        {
            DataSet dsGetKarvyRejectedTrans;
            Database db;
            DbCommand getKarvyRejectedTransCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getKarvyRejectedTransCmd = db.GetStoredProcCommand("SP_GetKarvyUploadRejectsTrans");
                db.AddInParameter(getKarvyRejectedTransCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getKarvyRejectedTransCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getKarvyRejectedTransCmd, "@processIdSortOrder", DbType.String, SortExpression);
                if (IsRejectedFilter != "")
                    db.AddInParameter(getKarvyRejectedTransCmd, "@isRejectedFilter", DbType.String, IsRejectedFilter);
                else
                    db.AddInParameter(getKarvyRejectedTransCmd, "@isRejectedFilter", DbType.String, DBNull.Value);
                if (FolioFilter != "")
                    db.AddInParameter(getKarvyRejectedTransCmd, "@folioFilter", DbType.String, FolioFilter);
                else
                    db.AddInParameter(getKarvyRejectedTransCmd, "@folioFilter", DbType.String, DBNull.Value);
                if (RejectReasonFilter != "")
                    db.AddInParameter(getKarvyRejectedTransCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);
                else
                    db.AddInParameter(getKarvyRejectedTransCmd, "@rejectReasonFilter", DbType.String, DBNull.Value);

                dsGetKarvyRejectedTrans = db.ExecuteDataSet(getKarvyRejectedTransCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getKarvyRejectedTrans()");

                object[] objects = new object[6];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = RejectReasonFilter;
                objects[5] = FolioFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetKarvyRejectedTrans.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetKarvyRejectedTrans;
        }

        public bool UpdateCAMSProfileStaging(int CAMSStagingID, string PanNumber, string Folio)
        {
            bool result = false;
            Database db;
            DbCommand UpdateProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateProfilesCmd = db.GetStoredProcCommand("SP_UpdateCAMSUploadProfile");
                db.AddInParameter(UpdateProfilesCmd, "@CMGCXPS_Id", DbType.Int32, CAMSStagingID);
                if (PanNumber != "")
                    db.AddInParameter(UpdateProfilesCmd, "@PanNumber", DbType.String, PanNumber);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@PanNumber", DbType.String, DBNull.Value);
                if (Folio != "")
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, Folio);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(UpdateProfilesCmd);
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

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateCAMSProfileStaging()");

                object[] objects = new object[3];
                objects[0] = CAMSStagingID;
                objects[1] = PanNumber;
                objects[2] = Folio;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool UpdateMFFolioStaging(int StagingID,int MainStagingId, string PanNumber, string Folio)
        {
            bool result = false;
            Database db;
            DbCommand UpdateProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateProfilesCmd = db.GetStoredProcCommand("SP_UpdateMFFolioStaging");
                db.AddInParameter(UpdateProfilesCmd, "@CMFFS_Id", DbType.Int32, StagingID);
                db.AddInParameter(UpdateProfilesCmd, "@CMFFS_MainStagingId", DbType.Int32, MainStagingId);
                if (PanNumber != "")
                    db.AddInParameter(UpdateProfilesCmd, "@PanNumber", DbType.String, PanNumber);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@PanNumber", DbType.String, DBNull.Value);
                if (Folio != "")
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, Folio);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(UpdateProfilesCmd);
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

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateMFFolioStaging()");

                object[] objects = new object[3];
                objects[0] = StagingID;
                objects[1] = PanNumber;
                objects[2] = Folio;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool UpdateKarvyProfileStaging(int KarvyStagingID, string PanNumber, string Folio)
        {
            bool result = false;
            Database db;
            DbCommand UpdateProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateProfilesCmd = db.GetStoredProcCommand("SP_UpdateKarvyUploadProfile");
                db.AddInParameter(UpdateProfilesCmd, "@CMFKXPS_Id", DbType.Int32, KarvyStagingID);
                if (PanNumber != "")
                    db.AddInParameter(UpdateProfilesCmd, "@PanNumber", DbType.String, PanNumber);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@PanNumber", DbType.String, DBNull.Value);
                if (Folio != "")
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, Folio);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, DBNull.Value);

                db.ExecuteNonQuery(UpdateProfilesCmd);
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

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateKarvyProfileStaging()");

                object[] objects = new object[3];
                objects[0] = KarvyStagingID;
                objects[1] = PanNumber;
                objects[2] = Folio;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool UpdateCAMSTransStaging(int CAMSStagingID, string TransactionNumber, string Folio)
        {
            bool result = false;
            Database db;
            DbCommand UpdateTranCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateTranCmd = db.GetStoredProcCommand("SP_UpdateCAMSUploadTrans");
                db.AddInParameter(UpdateTranCmd, "@CMCXTS_Id", DbType.Int32, CAMSStagingID);
                if (TransactionNumber != "")
                    db.AddInParameter(UpdateTranCmd, "@TransactionNumber", DbType.String, TransactionNumber);
                else
                    db.AddInParameter(UpdateTranCmd, "@TransactionNumber", DbType.String, DBNull.Value);
                if (Folio != "")
                    db.AddInParameter(UpdateTranCmd, "@Folio", DbType.String, Folio);
                else
                    db.AddInParameter(UpdateTranCmd, "@Folio", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(UpdateTranCmd);
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

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateCAMSTransStaging()");

                object[] objects = new object[3];
                objects[0] = CAMSStagingID;
                objects[1] = TransactionNumber;
                objects[2] = Folio;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public bool UpdateKArvyTransStaging(int KarvyStagingID, string TransactionNumber, string Folio)
        {
            bool result = false;
            Database db;
            DbCommand UpdateProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateProfilesCmd = db.GetStoredProcCommand("SP_UpdateKarvyUploadTrans");
                db.AddInParameter(UpdateProfilesCmd, "@CIMFKXTS_Id", DbType.Int32, KarvyStagingID);
                if (TransactionNumber != "")
                    db.AddInParameter(UpdateProfilesCmd, "@TransactionNumber", DbType.String, TransactionNumber);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@TransactionNumber", DbType.String, DBNull.Value);
                if (Folio != "")
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, Folio);
                else
                    db.AddInParameter(UpdateProfilesCmd, "@Folio", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(UpdateProfilesCmd);
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

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateKArvyTransStaging()");

                object[] objects = new object[3];
                objects[0] = KarvyStagingID;
                objects[1] = TransactionNumber;
                objects[2] = Folio;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return result;
        }

        public DataSet getWERPRejectedProfiles(int processId, int CurrentPage, out int Count, string SortExpression, string PANFilter, string RejectReasonFilter, string BrokerFilter, string CustomerNameFilter)
        {
            DataSet dsGetWERPRejectedProfiles;
            Database db;
            DbCommand getWERPRejectedProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getWERPRejectedProfilesCmd = db.GetStoredProcCommand("SP_GetWERPUploadRejectsProfile");
                db.AddInParameter(getWERPRejectedProfilesCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getWERPRejectedProfilesCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getWERPRejectedProfilesCmd, "@processIdSortOrder", DbType.String, SortExpression);

                if (PANFilter != "")
                    db.AddInParameter(getWERPRejectedProfilesCmd, "@panNumberFilter", DbType.String, PANFilter);

                if (CustomerNameFilter != "")
                    db.AddInParameter(getWERPRejectedProfilesCmd, "@customerNameFilter", DbType.String, CustomerNameFilter);

                if (BrokerFilter != "")
                    db.AddInParameter(getWERPRejectedProfilesCmd, "@brokerCodeFilter", DbType.String, BrokerFilter);

                if (RejectReasonFilter != "")
                    db.AddInParameter(getWERPRejectedProfilesCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);


                dsGetWERPRejectedProfiles = db.ExecuteDataSet(getWERPRejectedProfilesCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getWERPRejectedProfiles()");

                object[] objects = new object[9];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = PANFilter;
                objects[4] = RejectReasonFilter;
                objects[5] = CustomerNameFilter;
                objects[6] = BrokerFilter;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            Count = Int32.Parse(dsGetWERPRejectedProfiles.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetWERPRejectedProfiles;
        }

        public DataSet getWERPRejectedTransactions(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string RejectReasonFilter, string FolioFilter)
        {
            DataSet dsGetWERPRejectedTransactions;
            Database db;
            DbCommand getWERPRejectedTransactionsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getWERPRejectedTransactionsCmd = db.GetStoredProcCommand("SP_GetWERPUploadRejectsTransaction");
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@processIdSortOrder", DbType.String, SortExpression);
                if (IsRejectedFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@isRejectedFilter", DbType.String, IsRejectedFilter);
                else
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@isRejectedFilter", DbType.String, DBNull.Value);
                if (FolioFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@folioFilter", DbType.String, FolioFilter);
                else
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@folioFilter", DbType.String, DBNull.Value);
                if (RejectReasonFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);
                else
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@rejectReasonFilter", DbType.String, DBNull.Value);

                dsGetWERPRejectedTransactions = db.ExecuteDataSet(getWERPRejectedTransactionsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getWERPRejectedTransactions()");

                object[] objects = new object[6];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = RejectReasonFilter;
                objects[5] = FolioFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetWERPRejectedTransactions.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetWERPRejectedTransactions;
        }

        public DataSet GetRejectedEquityTransactionsStaging(int processId, int CurrentPage, out int Count,
           string SortExpression, string RejectReasonFilter, string PanNumberFilter, string ScripFilter, string ExchangeFilter, string TransactionTypeFilter)
        {
            DataSet dsGetWERPRejectedTransactions;
            Database db;
            DbCommand getWERPRejectedTransactionsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getWERPRejectedTransactionsCmd = db.GetStoredProcCommand("SP_GetUploadRejectsEquityTransactionStaging");
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@processIdSortOrder", DbType.String, SortExpression);



                if (RejectReasonFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);

                if (PanNumberFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@panNumberFilter", DbType.String, PanNumberFilter);

                if (ScripFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@scripFilter", DbType.String, ScripFilter);

                if (ExchangeFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@exchangeFilter", DbType.String, ExchangeFilter);

                if (TransactionTypeFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@transactionTypeFilter", DbType.String, TransactionTypeFilter);


                dsGetWERPRejectedTransactions = db.ExecuteDataSet(getWERPRejectedTransactionsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getWERPRejectedTransactions()");

                object[] objects = new object[6];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = RejectReasonFilter;
                objects[4] = PanNumberFilter;
                objects[5] = ScripFilter;
                objects[6] = ExchangeFilter;
                objects[7] = TransactionTypeFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetWERPRejectedTransactions.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetWERPRejectedTransactions;
        }

        public DataSet GetRejectedMFTransactionStaging(int adviserId, int CurrentPage, out int Count, string SortExpression, int processId, string RejectReasonFilter, string fileNameFilter, string FolioFilter, string TransactionTypeFilter, string investorNameFileter, string sourceTypeFilter, string schemeNameFilter)
        {
            DataSet dsGetMFRejectedTransactions;
            Database db;
            DbCommand getMFRejectedTransactionsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFRejectedTransactionsCmd = db.GetStoredProcCommand("SP_GetUploadRejectsMFTransactionStaging");
                db.AddInParameter(getMFRejectedTransactionsCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getMFRejectedTransactionsCmd, "@processIdSortOrder", DbType.String, SortExpression);
                db.AddInParameter(getMFRejectedTransactionsCmd, "@adviserId", DbType.Int32, adviserId);

                if (processId != 0)
                {
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@processId", DbType.Int32, processId);
                }
                else
                {
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@processId", DbType.Int32, DBNull.Value);
                }
                
                if (RejectReasonFilter != "")
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@rejectReasonFilter", DbType.String, RejectReasonFilter);

                if (fileNameFilter != "")
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@fileNameFilter", DbType.String, fileNameFilter);

                if (FolioFilter != "")
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@folioFilter", DbType.String, FolioFilter);

                if (TransactionTypeFilter != "")
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@transactionTypeFilter", DbType.String, TransactionTypeFilter);

                if (investorNameFileter != "")
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@investorNameFileter", DbType.String, investorNameFileter);

                if (sourceTypeFilter != "")
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@sourceTypeFilter", DbType.String, sourceTypeFilter);

                if (schemeNameFilter != "")
                    db.AddInParameter(getMFRejectedTransactionsCmd, "@schemeNameFilter", DbType.String, schemeNameFilter);

                dsGetMFRejectedTransactions = db.ExecuteDataSet(getMFRejectedTransactionsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getWERPRejectedTransactions()");

                object[] objects = new object[10];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = RejectReasonFilter;
                objects[4] = fileNameFilter;
                objects[5] = FolioFilter;
                objects[6] = TransactionTypeFilter;
                objects[7] = investorNameFileter;
                objects[8] = sourceTypeFilter;
                objects[9] = schemeNameFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetMFRejectedTransactions.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetMFRejectedTransactions;
        }
        
        public DataSet GetRejectedTradeAccountStaging(int processId, int CurrentPage, out int Count, string SortExpression, string TradeAccountNumFilter, string RejectReasonFilter, string PANFilter)
        {
            DataSet dsGetWERPRejectedTransactions;
            Database db;
            DbCommand getWERPRejectedTransactionsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getWERPRejectedTransactionsCmd = db.GetStoredProcCommand("SP_GetUploadRejectsEquityTradeAccountStaging");
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@processId", DbType.Int32, processId);
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@currentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getWERPRejectedTransactionsCmd, "@processIdSortOrder", DbType.String, SortExpression);

                if (TradeAccountNumFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@TradeAccountNumber", DbType.String, TradeAccountNumFilter);

                if (RejectReasonFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@RejectReasonFilter", DbType.String, RejectReasonFilter);

                if (PANFilter != "")
                    db.AddInParameter(getWERPRejectedTransactionsCmd, "@PANNum", DbType.String, PANFilter);

                dsGetWERPRejectedTransactions = db.ExecuteDataSet(getWERPRejectedTransactionsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:getWERPRejectedTransactions()");

                object[] objects = new object[6];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = TradeAccountNumFilter;
                objects[4] = RejectReasonFilter;
                objects[5] = PANFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsGetWERPRejectedTransactions.Tables[1].Rows[0]["CNT"].ToString());

            return dsGetWERPRejectedTransactions;
        }

        public bool UpdateWERPProfileStaging(int WerpStagingID, string PanNumber, string BrokerCode)
        {
            bool blresult = false;
            Database db;
            DbCommand UpdateProfilesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateProfilesCmd = db.GetStoredProcCommand("SP_UpdateWERPUploadProfile");
                db.AddInParameter(UpdateProfilesCmd, "@CPSId", DbType.Int32, WerpStagingID);
                if (PanNumber != "")
                    db.AddInParameter(UpdateProfilesCmd, "@PanNumber", DbType.String, PanNumber);
                if (BrokerCode != "")
                    db.AddInParameter(UpdateProfilesCmd, "@BrokerCode", DbType.String, BrokerCode);
                db.ExecuteNonQuery(UpdateProfilesCmd);
                blresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateWERPProfileStaging()");

                object[] objects = new object[3];
                objects[0] = WerpStagingID;
                objects[1] = PanNumber;
                objects[2] = BrokerCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blresult;
        }

        public bool UpdateWERPTransactionStaging(int WerpStagingID, string TransactionNumber, string Folio)
        {
            bool blresult = false;
            Database db;
            DbCommand UpdateTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateTransactionCmd = db.GetStoredProcCommand("SP_UpdateWERPUploadTransaction");
                db.AddInParameter(UpdateTransactionCmd, "@CMFXTS_Id", DbType.Int32, WerpStagingID);
                if (TransactionNumber != "")
                    db.AddInParameter(UpdateTransactionCmd, "@TransactionNumber", DbType.String, TransactionNumber);
                else
                    db.AddInParameter(UpdateTransactionCmd, "@TransactionNumber", DbType.String, DBNull.Value);
                if (Folio != "")
                    db.AddInParameter(UpdateTransactionCmd, "@Folio", DbType.String, Folio);
                else
                    db.AddInParameter(UpdateTransactionCmd, "@Folio", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(UpdateTransactionCmd);
                blresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateWERPTransactionStaging()");

                object[] objects = new object[3];
                objects[0] = WerpStagingID;
                objects[1] = TransactionNumber;
                objects[2] = Folio;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blresult;
        }

        public bool UpdateRejectedEquityTransactionStaging(int Id, string panNumber, string scripCode, string exchange, string price, string transactionType)
        {
            bool blresult = false;
            Database db;
            DbCommand UpdateTransactionCmd;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateTransactionCmd = db.GetStoredProcCommand("SP_UpdateUploadEquityTransactionStaging");
                db.AddInParameter(UpdateTransactionCmd, "@CETSId", DbType.Int32, Id);
                if (panNumber != "")
                    db.AddInParameter(UpdateTransactionCmd, "@PanNumber", DbType.String, panNumber);

                if (scripCode != "")
                    db.AddInParameter(UpdateTransactionCmd, "@ScripCodeName", DbType.String, scripCode);

                if (exchange != "")
                    db.AddInParameter(UpdateTransactionCmd, "@Exchange", DbType.String, exchange);

                if (price != "")
                    db.AddInParameter(UpdateTransactionCmd, "@Price", DbType.String, price);

                if (transactionType != "")
                    db.AddInParameter(UpdateTransactionCmd, "@TransactionType", DbType.String, transactionType);

                db.ExecuteNonQuery(UpdateTransactionCmd);
                blresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateWERPTransactionStaging()");
                object[] objects = new object[3];
                objects[0] = Id;
                objects[1] = panNumber;
                objects[2] = scripCode;
                objects[3] = exchange;
                objects[4] = transactionType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blresult;
        }

        public bool UpdateRejectedMFTransactionStaging(int Id, string panNumber, string folio, string price, string transactionType)
        {
            bool blresult = false;
            Database db;
            DbCommand UpdateTransactionCmd;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateTransactionCmd = db.GetStoredProcCommand("SP_UpdateUploadMFTransactionStaging");
                db.AddInParameter(UpdateTransactionCmd, "@CMFTSId", DbType.Int32, Id);
                if (panNumber != "")
                    db.AddInParameter(UpdateTransactionCmd, "@PanNumber", DbType.String, panNumber);

                if (folio != "")
                    db.AddInParameter(UpdateTransactionCmd, "@Folio", DbType.String, folio);

                if (price != "")
                    db.AddInParameter(UpdateTransactionCmd, "@Price", DbType.Double, price);

                if (transactionType != "")
                    db.AddInParameter(UpdateTransactionCmd, "@TransactionType", DbType.String, transactionType);

                db.ExecuteNonQuery(UpdateTransactionCmd);
                blresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateWERPTransactionStaging()");
                object[] objects = new object[5];
                objects[0] = Id;
                objects[1] = panNumber;
                objects[2] = folio;
                objects[3] = price;
                objects[4] = transactionType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blresult;
        }

        public bool UpdateRejectedTradeAccountStaging(int id, string TradeAccountNum, string PanNum)
        {
            bool blresult = false;
            Database db;
            DbCommand UpdateTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateTransactionCmd = db.GetStoredProcCommand("SP_UpdateUploadEquityTradeAccountStaging");
                db.AddInParameter(UpdateTransactionCmd, "@CETASId", DbType.Int32, id);
                if (TradeAccountNum != "")
                    db.AddInParameter(UpdateTransactionCmd, "@TradeAccountNumber", DbType.String, TradeAccountNum);

                if (PanNum != "")
                    db.AddInParameter(UpdateTransactionCmd, "@PanNumber", DbType.String, PanNum);

                db.ExecuteNonQuery(UpdateTransactionCmd);
                blresult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:UpdateWERPTransactionStaging()");

                object[] objects = new object[3];
                objects[0] = id;
                objects[1] = TradeAccountNum;
                objects[2] = PanNum;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blresult;
        }

        /// <summary>
        /// To get rejected records from input table for all types of profile or folio uploads
        /// </summary>
        /// <param name="ProcessID"></param> Process Id of the current adviser user
        /// <param name="UploadExternalType"></param>The Upload file type which are Karvy, CAMS, Templeton, Dutsche and Standard
        /// <param name="CurrentPage"></param>For paging purpose
        /// <param name="Count"></param>No of records for paging purpose
        /// <returns></returns>
        public DataSet GetProfileFolioInputRejects(int ProcessId, string UploadExternalType, int CurrentPage, out int Count)
        {
            DataSet dsProfileFolioRejectedRecords;
            Database db;
            DbCommand cmdgetRejectedRecords;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                if (UploadExternalType == "KA")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetKarvyProfileFolioInputRejects");
                else if (UploadExternalType == "CA")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetCAMSProfileFolioInputRejects");
                else if (UploadExternalType == "DT")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetDeutscheProfileFolioInputRejects");
                else if (UploadExternalType == "TN")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetTempletonProfileFolioInputRejects");
                else if(UploadExternalType == "ST")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetStandardProfileFolioInputRejects");
                else
                    cmdgetRejectedRecords = db.GetStoredProcCommand("");

                db.AddInParameter(cmdgetRejectedRecords, "@ProcessId", DbType.Int32, ProcessId);
                db.AddInParameter(cmdgetRejectedRecords, "@currentPage", DbType.Int32, CurrentPage);

                dsProfileFolioRejectedRecords = db.ExecuteDataSet(cmdgetRejectedRecords);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:GetProfileFolioInputRejects()");

                object[] objects = new object[3];
                objects[0] = ProcessId;
                objects[1] = CurrentPage;
                objects[2] = UploadExternalType;
                

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsProfileFolioRejectedRecords.Tables[1].Rows[0]["CNT"].ToString());

            return dsProfileFolioRejectedRecords;
        }

        /// <summary>
        /// To get rejected records from input table for all types of Traansaction uploads
        /// </summary>
        /// <param name="ProcessID"></param> Process Id of the current adviser user
        /// <param name="UploadExternalType"></param>The Upload file type which are Karvy, CAMS, Templeton, Dutsche and Standard
        /// <param name="CurrentPage"></param>For paging purpose
        /// <param name="Count"></param>No of records for paging purpose
        /// <returns></returns>
        public DataSet GetTransInputRejects(int ProcessId, string UploadExternalType, int CurrentPage, out int Count)
        {
            DataSet dsTransRejectedRecords;
            Database db;
            DbCommand cmdgetRejectedRecords;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                if (UploadExternalType == "KA")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetKarvyTransInputRejects");
                else if (UploadExternalType == "CA")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetCAMSTransInputRejects");
                else if (UploadExternalType == "DT")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetDeutscheTransInputRejects");
                else if (UploadExternalType == "TN")
                    cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetTempletonTransInputRejects");
                
                else
                    cmdgetRejectedRecords = db.GetStoredProcCommand("");

                db.AddInParameter(cmdgetRejectedRecords, "@ProcessId", DbType.Int32, ProcessId);
                db.AddInParameter(cmdgetRejectedRecords, "@currentPage", DbType.Int32, CurrentPage);

                dsTransRejectedRecords = db.ExecuteDataSet(cmdgetRejectedRecords);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:GetTransaInputRejects()");

                object[] objects = new object[3];
                objects[0] = ProcessId;
                objects[1] = CurrentPage;
                objects[2] = UploadExternalType;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsTransRejectedRecords.Tables[1].Rows[0]["CNT"].ToString());

            return dsTransRejectedRecords;
        }


        public DataSet GetUploadMFRejectsDistinctFolios(int adviserid, int CurrentPage, out int Count)
        {
            DataSet dsRejectRecords = new DataSet();
            Database db;
            DbCommand cmdgetRejectedRecords;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdgetRejectedRecords = db.GetStoredProcCommand("SP_GetUploadMFRejectsDistinctFoliosForAccountRejects");
                db.AddInParameter(cmdgetRejectedRecords, "@adviserId", DbType.Int32, adviserid);
                db.AddInParameter(cmdgetRejectedRecords, "@currentPage", DbType.Int32, CurrentPage);
                dsRejectRecords = db.ExecuteDataSet(cmdgetRejectedRecords);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:GetUploadMFRejectsDistinctFolios()");

                object[] objects = new object[2];
                objects[0] = adviserid;
                objects[1] = CurrentPage;
                


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            Count = Int32.Parse(dsRejectRecords.Tables[1].Rows[0]["CNT"].ToString());
            return dsRejectRecords;
        }

        public DataSet GetUploadProcessIDForSelectedFoliosANDAMC(int adviserid, int amccode, string folionum)
        {
            DataSet dsProcessIds = new DataSet();
            Database db;
            DbCommand cmdgetProcessids;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdgetProcessids = db.GetStoredProcCommand("SP_GetUploadProcessIDForSelectedFoliosANDAMC");
                db.AddInParameter(cmdgetProcessids, "@adviserId", DbType.Int32, adviserid);
                db.AddInParameter(cmdgetProcessids, "@folionumber", DbType.String, folionum);
                db.AddInParameter(cmdgetProcessids, "@AMCCode", DbType.Int32, amccode);
                dsProcessIds = db.ExecuteDataSet(cmdgetProcessids);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsDao.cs:GetUploadProcessIDForSelectedFoliosANDAMC()");

                object[] objects = new object[3];
                objects[0] = adviserid;
                objects[1] = amccode;
                objects[1] = folionum;



                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


            return dsProcessIds;
        }

        public void DeleteMFTransactionStaging(int StagingID)
      {
            
            Database db;
            DbCommand deletetransactions;

                db = DatabaseFactory.CreateDatabase("wealtherp");
                deletetransactions = db.GetStoredProcCommand("SP_DeleteStagingTransaction");
                db.AddInParameter(deletetransactions, "@StagingID", DbType.Int32, StagingID);
                db.ExecuteDataSet(deletetransactions);
            
        }
    }
}
