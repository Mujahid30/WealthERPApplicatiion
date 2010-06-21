using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUploads
{
    public class UploadProcessLogVo
    {
        #region Fields

        private int m_ProcessId;
        private string m_FileName;
        private int m_FileTypeId;
        private int m_NoOfTotalRecords;
        private int m_UserId;
        private string m_XMLFileName;
        private int m_AdviserId;
        private string m_Comment;
        private DateTime m_StartTime;
        private DateTime m_EndTime;
        private int m_NoOfRejectedRecords;
        private int m_NoOfCustomerInserted;
        private int m_NoOfFolioInserted;
        private int m_NoOfTransactionInserted;
        private int m_IsExternalConversionComplete;
        private int m_IsInsertionToInputComplete;
        private int m_IsInsertionToStagingComplete;
        private int m_IsInsertionToWERPComplete;
        private int m_IsInsertionToXtrnlComplete;
        private int m_CreatedBy;
        private int m_ModifiedBy;
        private string m_ExtractTypeCode;
        private int m_BranchId;
        
        #endregion Fields

        #region Properties

        public int NoOfCustomerDuplicates { get; set; }
        public int NoOfAccountDuplicates { get; set; }
        public int NoOfTransactionDuplicates { get; set; }
        public int NoOfInputRejects { get; set; }

        public int BranchId
        {
            get { return m_BranchId; }
            set { m_BranchId = value; }
        }
        public string ExtractTypeCode
        {
            get { return m_ExtractTypeCode; }
            set { m_ExtractTypeCode = value; }
        }
        public int ProcessId
        {
            get { return m_ProcessId; }
            set { m_ProcessId = value; }
        }


        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }


        public int FileTypeId
        {
            get { return m_FileTypeId; }
            set { m_FileTypeId = value; }
        }

        public int NoOfTotalRecords
        {
            get { return m_NoOfTotalRecords; }
            set { m_NoOfTotalRecords = value; }
        }

        public int UserId
        {
            get { return m_UserId; }
            set { m_UserId = value; }
        }

        public string XMLFileName
        {
            get { return m_XMLFileName; }
            set { m_XMLFileName = value; }
        }

        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }

        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }

        public DateTime EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }

        public int NoOfRejectedRecords
        {
            get { return m_NoOfRejectedRecords; }
            set { m_NoOfRejectedRecords = value; }
        }

        public int NoOfCustomerInserted
        {
            get { return m_NoOfCustomerInserted; }
            set { m_NoOfCustomerInserted = value; }
        }

        public int NoOfAccountsInserted
        {
            get { return m_NoOfFolioInserted; }
            set { m_NoOfFolioInserted = value; }
        }

        public int NoOfTransactionInserted
        {
            get { return m_NoOfTransactionInserted; }
            set { m_NoOfTransactionInserted = value; }
        }

        public int IsExternalConversionComplete
        {
            get { return m_IsExternalConversionComplete; }
            set { m_IsExternalConversionComplete = value; }
        }

        public int IsInsertionToInputComplete
        {
            get { return m_IsInsertionToInputComplete; }
            set { m_IsInsertionToInputComplete = value; }
        }

        public int IsInsertionToFirstStagingComplete
        {
            get { return m_IsInsertionToStagingComplete; }
            set { m_IsInsertionToStagingComplete = value; }
        }

        public int IsInsertionToWERPComplete
        {
            get { return m_IsInsertionToWERPComplete; }
            set { m_IsInsertionToWERPComplete = value; }
        }

        public int IsInsertionToXtrnlComplete
        {
            get { return m_IsInsertionToXtrnlComplete; }
            set { m_IsInsertionToXtrnlComplete = value; }
        }

        public int NoOfRecordsInserted
        {
            get;
            set;
        }

        public int IsInsertionToSecondStagingComplete
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }

        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }

        #endregion Properties

    }
}
