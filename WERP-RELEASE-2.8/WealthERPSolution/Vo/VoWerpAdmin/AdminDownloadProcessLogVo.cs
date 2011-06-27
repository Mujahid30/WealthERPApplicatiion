using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoWerpAdmin
{
    public class AdminDownloadProcessLogVo
    {
        #region Fields and their Encapsulations
        
        private int m_ProcessID;

        public int ProcessID
        {
            get { return m_ProcessID; }
            set { m_ProcessID = value; }
        }
        private string m_AssetClass;

        public string AssetClass
        {
            get { return m_AssetClass; }
            set { m_AssetClass = value; }
        }
        private string m_SourceName;

        public string SourceName
        {
            get { return m_SourceName; }
            set { m_SourceName = value; }
        }
        private DateTime m_StartTime;

        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }
        private DateTime m_EndTime;

        public DateTime EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }
        private int m_NoOfRecordsDownloaded;

        public int NoOfRecordsDownloaded
        {
            get { return m_NoOfRecordsDownloaded; }
            set { m_NoOfRecordsDownloaded = value; }
        }
        private int m_IsConnectionToSiteEstablished;

        public int IsConnectionToSiteEstablished
        {
            get { return m_IsConnectionToSiteEstablished; }
            set { m_IsConnectionToSiteEstablished = value; }
        }
        private int m_IsFileDownloaded;

        public int IsFileDownloaded
        {
            get { return m_IsFileDownloaded; }
            set { m_IsFileDownloaded = value; }
        }
        private int m_IsConversiontoXMLComplete;

        public int IsConversiontoXMLComplete
        {
            get { return m_IsConversiontoXMLComplete; }
            set { m_IsConversiontoXMLComplete = value; }
        }
        private int m_IsInsertiontoDBdone;

        public int IsInsertiontoDBdone
        {
            get { return m_IsInsertiontoDBdone; }
            set { m_IsInsertiontoDBdone = value; }
        }
        private string m_XMLFileName;

        public string XMLFileName
        {
            get { return m_XMLFileName; }
            set { m_XMLFileName = value; }
        }
        private int m_CreatedBy;

        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }
        private DateTime m_CreatedOn;

        public DateTime CreatedOn
        {
            get { return m_CreatedOn; }
            set { m_CreatedOn = value; }
        }
        private int m_ModifiedBy;

        public int ModifiedBy
        {
            get { return m_ModifiedBy; }
            set { m_ModifiedBy = value; }
        }
        private DateTime m_ModifiedOn;

        public DateTime ModifiedOn
        {
            get { return m_ModifiedOn; }
            set { m_ModifiedOn = value; }
        }

        private int m_NoOfRecordsInserted;

        public int NoOfRecordsInserted
        {
            get { return m_NoOfRecordsInserted; }
            set { m_NoOfRecordsInserted = value; }
        }

        private string m_Description;

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        private DateTime m_ForDate;

        public DateTime ForDate
        {
            get { return m_ForDate; }
            set { m_ForDate = value; }
        }
        #endregion

    }
}
