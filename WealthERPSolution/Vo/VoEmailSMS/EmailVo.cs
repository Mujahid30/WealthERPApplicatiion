using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoEmailSMS
{
    public class EmailVo
    {
        //custom and standard email sending .. 

        #region for custom email       

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

        #endregion

        #region for sending the email to queue

        private string m_To;
        private string m_Cc;
        private string m_Bcc;
        private string m_Subject;
        private string m_Body;
        private int m_Sent;
        private int m_NoOfRetries;
        private string m_DaemonCode;
        private DateTime m_AllocatedOn;
        private string m_ErrorMsg;

        public string To            
        {
            get { return m_To; }
            set { m_To = value; }
        }

        public string Cc
        {
            get { return m_Cc; }
            set { m_Cc = value; }
        }

        public string Bcc
        {
            get { return m_Bcc; }
            set { m_Bcc = value; }
        }

        public string Subject
        {
            get { return m_Subject; }
            set { m_Subject = value; }
        }

        public string Body
        {
            get { return m_Body; }
            set { m_Body = value; }
        }

        public int Sent
        {
            get { return m_Sent; }
            set { m_Sent = value; }
        }

        public int NoOfRetries
        {
            get { return m_NoOfRetries; }
            set { m_NoOfRetries = value; }
        }

        public string DaemonCode
        {
            get { return m_DaemonCode; }
            set { m_DaemonCode = value; }
        }

        public DateTime AllocatedOn
        {
            get { return m_AllocatedOn; }
            set { m_AllocatedOn = value; }
        }

        public string ErrorMsg
        {
            get { return m_ErrorMsg; }
            set { m_ErrorMsg = value; }
        }
        #endregion
    }
}
