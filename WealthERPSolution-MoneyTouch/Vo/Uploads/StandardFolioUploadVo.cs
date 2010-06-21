using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoUploads
{
    public class StandardFolioUploadVo
    {
        #region Fields

        int m_FolioStagingId;
        int m_XmlFileTypeId;
        int m_MainStaingId;
        int m_AdviserId;
        int m_ProcessId;
        int m_CustomerId;
        int m_AMCWerpCode;
        int m_PortfolioId;
        int m_AccountId;
        string m_PANNum;
        string m_FolioNum;
        string m_AMCCode;
        DateTime m_AccountOpeningDate;
        string m_ModeOfHoldingCode;
        int m_RejectReasonCode;


        #endregion

        #region Properties

        public int FolioStagingId
        {
            get { return m_FolioStagingId; }
            set { m_FolioStagingId = value; }
        }

        public int XmlFileTypeId
        {
            get { return m_XmlFileTypeId; }
            set { m_XmlFileTypeId = value; }
        }

        public int MainStaingId
        {
            get { return m_MainStaingId; }
            set { m_MainStaingId = value; }
        }

        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }

        public int ProcessId
        {
            get { return m_ProcessId; }
            set { m_ProcessId = value; }
        }

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public int AMCWerpCode
        {
            get { return m_AMCWerpCode; }
            set { m_AMCWerpCode = value; }
        }
        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
        }

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public string PANNum
        {
            get { return m_PANNum; }
            set { m_PANNum = value; }
        }

        public string FolioNum
        {
            get { return m_FolioNum; }
            set { m_FolioNum = value; }
        }

        public string AMCCode
        {
            get { return m_AMCCode; }
            set { m_AMCCode = value; }
        }

        public DateTime AccountOpeningDate
        {
            get { return m_AccountOpeningDate; }
            set { m_AccountOpeningDate = value; }
        }

        public string ModeOfHoldingCode
        {
            get { return m_ModeOfHoldingCode; }
            set { m_ModeOfHoldingCode = value; }
        }

        public int RejectReasonCode
        {
            get { return m_RejectReasonCode; }
            set { m_RejectReasonCode = value; }
        }
        #endregion

    }
}
