using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUploads;
using DaoUploads;
using BoUser;
using VoUser;
using BoCustomerProfiling;
using System.Collections.Specialized;
using Microsoft.SqlServer;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace BoUploads
{
    public class RejectedRecordsBo
    {
        //public DataSet GetRejectedRecords(int adviserId, int processId, string UploadType)
        //{
        //    RejectedRecordsDao RejectedRecordsDao = new RejectedRecordsDao();
        //    DataSet getProfileRejectedCustomersDs; 
        //    try
        //    {
        //        getProfileRejectedCustomersDs = RejectedRecordsDao.GetRejectedRecords(adviserId,processId,UploadType);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetRejectedRecords()");

        //        object[] objects = new object[1];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return getProfileRejectedCustomersDs;
        //}

        //public CamsUploadsVo GetCAMSRejectedProfile(int rejectedId)
        //{
        //    RejectedRecordsDao objCAMSRejectedProfileDao = new RejectedRecordsDao();
        //    CamsUploadsVo CAMSUploadsVo;
        //    try
        //    {
        //        CAMSUploadsVo = objCAMSRejectedProfileDao.GetCAMSRejectedProfile(rejectedId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetCAMSRejectedProfile()");

        //        object[] objects = new object[1];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return CAMSUploadsVo;
        //}

        //public KarvyUploadsVo GetKarvyRejectedProfile(int rejectedId)
        //{
        //    RejectedRecordsDao objKarvyRejectedProfileDao = new RejectedRecordsDao();
        //    KarvyUploadsVo KarvyUploadsVo;
        //    try
        //    {
        //        KarvyUploadsVo = objKarvyRejectedProfileDao.GetKarvyRejectedProfile(rejectedId);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetKarvyRejectedProfile()");

        //        object[] objects = new object[1];

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;
        //    }
        //    return KarvyUploadsVo;
        //}

        //Get profile reject for CAMS uploads
        public DataSet getCAMSRejectedProfiles(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string PANFilter, string RejectReasonFilter, string NameFilter, string FolioFilter, string DoesCustExistFilter)
        {
            DataSet dsCAMSRejectedProfiles;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsCAMSRejectedProfiles = rejecetedRecords.getCAMSRejectedProfiles(processId, CurrentPage, out Count, SortExpression, IsRejectedFilter, PANFilter, RejectReasonFilter, NameFilter, FolioFilter, DoesCustExistFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getCAMSRejectedProfiles()");

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

            return dsCAMSRejectedProfiles;
        }
        public DataSet getMFRejectedFolios(int adviserId, int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string PANFilter, string RejectReasonFilter, string NameFilter, string FolioFilter)
        {
            DataSet dsCAMSRejectedProfiles;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsCAMSRejectedProfiles = rejecetedRecords.getMFRejectedFolios(adviserId, processId, CurrentPage, out Count, SortExpression, IsRejectedFilter, PANFilter, RejectReasonFilter, NameFilter, FolioFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getMFRejectedFolios()");

                object[] objects = new object[9];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = PANFilter;
                objects[5] = RejectReasonFilter;
                objects[6] = NameFilter;
                objects[7] = FolioFilter;
                //objects[8] = DoesCustExistFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCAMSRejectedProfiles;
        }
        public DataSet getCAMSRejectedTrans(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string RejectReasonFilter, string FolioFilter)
        {
            DataSet dsCAMSRejectedTrans;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsCAMSRejectedTrans = rejecetedRecords.getCAMSRejectedTrans(processId, CurrentPage, out Count, SortExpression, IsRejectedFilter, RejectReasonFilter, FolioFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getCAMSRejectedTrans()");

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

            return dsCAMSRejectedTrans;
        }

        public DataSet getKarvyRejectedProfile(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string PANFilter, string RejectReasonFilter, string NameFilter, string FolioFilter, string DoesCustExistFilter)
        {
            DataSet dsKarvyRejectedProfile;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsKarvyRejectedProfile = rejecetedRecords.getKarvyRejectedProfile(processId, CurrentPage, out Count, SortExpression, IsRejectedFilter, PANFilter, RejectReasonFilter, NameFilter, FolioFilter, DoesCustExistFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getKarvyRejectedProfile()");

                object[] objects = new object[2];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsKarvyRejectedProfile;
        }

        public DataSet getKarvyRejectedTrans(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string RejectReasonFilter, string FolioFilter)
        {
            DataSet dsKarvyRejectedTrans;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsKarvyRejectedTrans = rejecetedRecords.getKarvyRejectedTrans(processId, CurrentPage, out Count, SortExpression, IsRejectedFilter, RejectReasonFilter, FolioFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getKarvyRejectedTrans()");

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

            return dsKarvyRejectedTrans;
        }

        public bool UpdateCAMSProfileStaging(int CAMSStagingID, string PanNumber, string Folio)
        {
            bool result = false;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateCAMSProfileStaging(CAMSStagingID, PanNumber, Folio);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateCAMSProfileStaging()");

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
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateMFFolioStaging(StagingID,MainStagingId, PanNumber, Folio);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateMFFolioStaging()");

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
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateKarvyProfileStaging(KarvyStagingID, PanNumber, Folio);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateKarvyProfileStaging()");

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
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateCAMSTransStaging(CAMSStagingID, TransactionNumber, Folio);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateKarvyProfileStaging()");

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
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateKArvyTransStaging(KarvyStagingID, TransactionNumber, Folio);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateKArvyTransStaging()");

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

        public DataSet getWERPRejectedProfile(int adviserId, int processId, int CurrentPage, out int Count, string SortExpression, string PANFilter, string RejectReasonFilter, string BrokerFilter, string CustomerNameFilter)
        {
            DataSet dsWERPRejectedProfiles;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedProfiles = rejecetedRecords.getWERPRejectedProfiles(adviserId, processId, CurrentPage, out  Count, SortExpression, PANFilter, RejectReasonFilter, BrokerFilter, CustomerNameFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getWERPRejectedProfile()");

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

            return dsWERPRejectedProfiles;
        }

        public DataSet getWERPRejectedTransactions(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string RejectReasonFilter, string FolioFilter)
        {
            DataSet dsWERPRejectedTransactions;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedTransactions = rejecetedRecords.getWERPRejectedTransactions(processId, CurrentPage, out Count, SortExpression, IsRejectedFilter, RejectReasonFilter, FolioFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getWERPRejectedTransactions()");

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

            return dsWERPRejectedTransactions;
        }
     
        public DataSet GetRejectedEquityTransactionsStaging(int adviserId, int processId, int CurrentPage, out int Count,
            string SortExpression, string RejectReasonFilter,string PanNumberFilter,string ScripFilter, string ExchangeFilter, string TransactionTypeFilter)
        {
            DataSet dsWERPRejectedTransactions;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedTransactions = rejecetedRecords.GetRejectedEquityTransactionsStaging(adviserId, processId, CurrentPage, out Count,
             SortExpression, RejectReasonFilter, PanNumberFilter, ScripFilter, ExchangeFilter, TransactionTypeFilter);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getWERPRejectedTransactions()");

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

            return dsWERPRejectedTransactions;
        }

        public DataSet GetRejectedMFTransactionStaging(int adviserId, int CurrentPage, out int Count, string SortExpression, int processId, string RejectReasonFilter, string fileNameFilter, string FolioFilter, string TransactionTypeFilter, string investorNameFileter, string sourceTypeFilter, string schemeNameFilter)
        {
            DataSet dsWERPRejectedTransactions;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedTransactions = rejecetedRecords.GetRejectedMFTransactionStaging(adviserId, CurrentPage, out Count, SortExpression, processId, RejectReasonFilter, fileNameFilter, FolioFilter, TransactionTypeFilter, investorNameFileter, sourceTypeFilter, schemeNameFilter);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getWERPRejectedTransactions()");

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

            return dsWERPRejectedTransactions;
        }

        public DataSet GetRejectedTradeAccountStaging(int adviserId, int processId, int CurrentPage, out int Count, string SortExpression, string TradeAccountNumFilter, string RejectReasonFilter, string PanFilter)
        {
            DataSet dsWERPRejectedTransactions;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedTransactions = rejecetedRecords.GetRejectedTradeAccountStaging(adviserId, processId, CurrentPage, out Count, SortExpression, TradeAccountNumFilter, RejectReasonFilter, PanFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getWERPRejectedTransactions()");

                object[] objects = new object[6];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = TradeAccountNumFilter;
                objects[4] = RejectReasonFilter;
                objects[5] = PanFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsWERPRejectedTransactions;
        }


        
        public bool UpdateWERPProfileStaging(int WerpStagingID, string PanNumber, string BrokerCode)
        {
            bool result = false;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateWERPProfileStaging(WerpStagingID, PanNumber, BrokerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateWERPProfileStaging()");

                object[] objects = new object[3];
                objects[0] = WerpStagingID;
                objects[1] = PanNumber;
                objects[2] = BrokerCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }

        public bool UpdateWERPTransactionStaging(int WerpStagingID, string TransactionNumber, string Folio)
        {
            bool result = false;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateWERPTransactionStaging(WerpStagingID, TransactionNumber, Folio);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateWERPTransactionStaging()");

                object[] objects = new object[3];
                objects[0] = WerpStagingID;
                objects[1] = TransactionNumber;
                objects[2] = Folio;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }
        
        public bool UpdateRejectedEquityTransactionStaging(int Id, string panNumber, string scripCode, string exchange,string price, string transactionType)
        {
            bool result = false;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateRejectedEquityTransactionStaging(Id, panNumber,  scripCode, exchange,price, transactionType);
                
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateWERPTransactionStaging()");

                object[] objects = new object[5];
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

            return result;
        }

        public bool UpdateRejectedMFTransactionStaging(int Id, string panNumber, string folio,string price, string transactionType)
        {
            bool result = false;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateRejectedMFTransactionStaging(Id, panNumber, folio,price, transactionType);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateWERPTransactionStaging()");

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

            return result;
        }

        public bool UpdateRejectedTradeAccountStaging(int id, string TradeAccountNum, string PanNum)
        {
            bool result = false;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                result = rejecetedRecords.UpdateRejectedTradeAccountStaging(id, TradeAccountNum, PanNum);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:UpdateWERPTransactionStaging()");

                object[] objects = new object[3];
                objects[0] = id;
                objects[1] = TradeAccountNum;
                objects[2] = PanNum;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return result;
        }

        /// <summary>
        /// To get rejected records from input table for all types of ProfileFolio uploads
        /// </summary>
        /// <param name="ProcessID"></param> Process Id of the current adviser user
        /// <param name="UploadExternalType"></param>The Upload file type which are Karvy, CAMS, Templeton, Dutsche and Standard
        /// <param name="CurrentPage"></param>For paging purpose
        /// <param name="Count"></param>No of records for paging purpose
        /// <returns></returns>
        public DataSet GetProfileFolioInputRejects(int ProcessId, string UploadExternalType, int CurrentPage, out int Count)
        {
            DataSet dsRejectedRecords;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();

            try
            {
                dsRejectedRecords = rejecetedRecords.GetProfileFolioInputRejects(ProcessId, UploadExternalType, CurrentPage, out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetProfileFolioInputRejects()");

                object[] objects = new object[3];
                objects[0] = ProcessId;
                objects[1] = CurrentPage;
                objects[2] = UploadExternalType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsRejectedRecords;
        }

        /// <summary>
        /// To get rejected records from input table for all types of Transaction uploads
        /// </summary>
        /// <param name="ProcessID"></param> Process Id of the current adviser user
        /// <param name="UploadExternalType"></param>The Upload file type which are Karvy, CAMS, Templeton, Dutsche and Standard
        /// <param name="CurrentPage"></param>For paging purpose
        /// <param name="Count"></param>No of records for paging purpose
        /// <returns></returns>
        
        public DataSet GetTransInputRejects(int ProcessId, string UploadExternalType, int CurrentPage, out int Count)
        {
            DataSet dsRejectedRecords;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();

            try
            {
                dsRejectedRecords = rejecetedRecords.GetTransInputRejects(ProcessId, UploadExternalType, CurrentPage, out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetTransaInputRejects()");

                object[] objects = new object[3];
                objects[0] = ProcessId;
                objects[1] = CurrentPage;
                objects[2] = UploadExternalType;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsRejectedRecords;
        }

        public DataSet GetUploadMFRejectsDistinctFolios(int adviserid, int CurrentPage, out int Count)
        {
            DataSet dsRejectedRecords;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();

            try
            {
                dsRejectedRecords = rejecetedRecords.GetUploadMFRejectsDistinctFolios(adviserid, CurrentPage, out Count);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetUploadMFRejectsDistinctFolios()");

                object[] objects = new object[2];
                objects[0] = adviserid;
                objects[1] = CurrentPage;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsRejectedRecords;
        }


        public DataSet GetUploadProcessIDForSelectedFoliosANDAMC(int adviserid, int amccode, string folionum)
        {
            DataSet dsProcessIds;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();

            try
            {
                dsProcessIds = rejecetedRecords.GetUploadProcessIDForSelectedFoliosANDAMC(adviserid, amccode, folionum);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetUploadProcessIDForSelectedFoliosANDAMC()");
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
        public void DeleteMFTransactionStaging(string StagingID)
        {            
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                rejecetedRecords.DeleteMFTransactionStaging(StagingID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:DeleteMFTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = StagingID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void DeleteWERPRejectedProfile(int StagingID)
        {
            RejectedRecordsDao rejecetedRecordsDao = new RejectedRecordsDao();
            try
            {
                rejecetedRecordsDao.DeleteWERPRejectedProfile(StagingID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:DeleteWERPRejectedProfile()");

                object[] objects = new object[1];
                objects[0] = StagingID;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void DeleteRejectsEquityTradeAccountStaging(int StagingID)
        {
            RejectedRecordsDao rejecetedRecordsDao = new RejectedRecordsDao();
            try
            {
                rejecetedRecordsDao.DeleteRejectsEquityTradeAccountStaging(StagingID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:DeleteRejectsEquityTradeAccountStaging()");

                object[] objects = new object[1];
                objects[0] = StagingID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void DeleteRejectsEquityTransactionStaging(int StagingID)
        {
            RejectedRecordsDao rejecetedRecordsDao = new RejectedRecordsDao();
            try
            {
                rejecetedRecordsDao.DeleteRejectsEquityTransactionStaging(StagingID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:DeleteRejectsEquityTransactionStaging()");

                object[] objects = new object[1];
                objects[0] = StagingID;    
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public void DeleteMFRejectedFolios(string StagingID)
        {
            RejectedRecordsDao rejecetedRecordsDao = new RejectedRecordsDao();
            try
            {
                rejecetedRecordsDao.DeleteMFRejectedFolios(StagingID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:DeleteMFRejectedFolios()");

                object[] objects = new object[1];
                objects[0] = StagingID;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }

        public bool InsertProbableDuplicatesRejectedTransaction(string stagingIDs)
        {
            RejectedRecordsDao rejecetedRecordsDao = new RejectedRecordsDao();
            bool affectedRecords;
            try
            {
               affectedRecords= rejecetedRecordsDao.InsertProbableDuplicatesRejectedTransaction(stagingIDs);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return affectedRecords;
        }

        public bool DeleteProbableDuplicatesRejectedTransaction(string stagingIDs)
        {
            RejectedRecordsDao rejecetedRecordsDao = new RejectedRecordsDao();
            bool affectedRecords;
            try
            {
                affectedRecords = rejecetedRecordsDao.DeleteProbableDuplicatesRejectedTransaction(stagingIDs);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return affectedRecords;
        }

        public DataSet getSuperAdminWERPRejectedProfile(int processId, int CurrentPage, out int Count, string SortExpression, string PANFilter, string RejectReasonFilter, string BrokerFilter, string CustomerNameFilter)
        {
            DataSet dsWERPRejectedProfiles;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedProfiles = rejecetedRecords.getSuperAdminWERPRejectedProfile(processId, CurrentPage, out  Count, SortExpression, PANFilter, RejectReasonFilter, BrokerFilter, CustomerNameFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getSuperAdminWERPRejectedProfile()");

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

            return dsWERPRejectedProfiles;
        }

        public DataSet GetSuperAdminUploadRejectsMFTransactionStaging(int CurrentPage, out int Count, string SortExpression, int processId, string RejectReasonFilter, string fileNameFilter, string FolioFilter, string TransactionTypeFilter, string investorNameFileter, string sourceTypeFilter, string schemeNameFilter)
        {
            DataSet dsWERPRejectedTransactions;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedTransactions = rejecetedRecords.GetSuperAdminUploadRejectsMFTransactionStaging(CurrentPage, out Count, SortExpression, processId, RejectReasonFilter, fileNameFilter, FolioFilter, TransactionTypeFilter, investorNameFileter, sourceTypeFilter, schemeNameFilter);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetSuperAdminUploadRejectsMFTransactionStaging()");

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

            return dsWERPRejectedTransactions;
        }

        public DataSet getSuperAdminMFRejectedFolios(int processId, int CurrentPage, out int Count, string SortExpression, string IsRejectedFilter, string PANFilter, string RejectReasonFilter, string NameFilter, string FolioFilter)
        {
            DataSet dsCAMSRejectedProfiles;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsCAMSRejectedProfiles = rejecetedRecords.getSuperAdminMFRejectedFolios(processId, CurrentPage, out Count, SortExpression, IsRejectedFilter, PANFilter, RejectReasonFilter, NameFilter, FolioFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:getSuperAdminMFRejectedFolios()");

                object[] objects = new object[9];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = IsRejectedFilter;
                objects[4] = PANFilter;
                objects[5] = RejectReasonFilter;
                objects[6] = NameFilter;
                objects[7] = FolioFilter;
                //objects[8] = DoesCustExistFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsCAMSRejectedProfiles;
        }

        public DataSet GetSuperAdminRejectedTradeAccountStaging(int processId, int CurrentPage, out int Count, string SortExpression, string TradeAccountNumFilter, string RejectReasonFilter, string PanFilter)
        {
            DataSet dsWERPRejectedTransactions;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedTransactions = rejecetedRecords.GetSuperAdminRejectedTradeAccountStaging(processId, CurrentPage, out Count, SortExpression, TradeAccountNumFilter, RejectReasonFilter, PanFilter);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetSuperAdminRejectedTradeAccountStaging()");

                object[] objects = new object[6];
                objects[0] = processId;
                objects[1] = CurrentPage;
                objects[2] = SortExpression;
                objects[3] = TradeAccountNumFilter;
                objects[4] = RejectReasonFilter;
                objects[5] = PanFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsWERPRejectedTransactions;
        }
        public DataSet GetSuperAdminRejectedEquityTransactionsStaging(int processId, int CurrentPage, out int Count,
           string SortExpression, string RejectReasonFilter, string PanNumberFilter, string ScripFilter, string ExchangeFilter, string TransactionTypeFilter)
        {
            DataSet dsWERPRejectedTransactions;
            RejectedRecordsDao rejecetedRecords = new RejectedRecordsDao();
            try
            {
                dsWERPRejectedTransactions = rejecetedRecords.GetSuperAdminRejectedEquityTransactionsStaging(processId, CurrentPage, out Count,
             SortExpression, RejectReasonFilter, PanNumberFilter, ScripFilter, ExchangeFilter, TransactionTypeFilter);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "RejectedRecordsBo.cs:GetSuperAdminRejectedEquityTransactionsStaging()");

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

            return dsWERPRejectedTransactions;
        }
    }

    
}


