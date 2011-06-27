using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoEmailSMS
{
    public class EmailVo
    {
        private int m_EmailQueueId;
        private int m_AdviserId;
        private int m_CustomerId;
        private string m_EmailType;
        private int m_HasAttachment;
        private string m_AttachmentPath;
        private DateTime m_SentDate;
        private string m_FileName;
        private int m_ReportCode;
        private int m_Status;

        public int Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        public int ReportCode
        {
            get { return m_ReportCode; }
            set { m_ReportCode = value; }
        }

        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }
        public DateTime SentDate
        {
            get { return m_SentDate; }
            set { m_SentDate = value; }
        }
        public string AttachmentPath
        {
            get { return m_AttachmentPath; }
            set { m_AttachmentPath = value; }
        }
        public int HasAttachment
        {
            get { return m_HasAttachment; }
            set { m_HasAttachment = value; }
        }
        public string EmailType
        {
            get { return m_EmailType; }
            set { m_EmailType = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        public int EmailQueueId
        {
            get { return m_EmailQueueId; }
            set { m_EmailQueueId = value; }
        }
        

        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
    }
}
