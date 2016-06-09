using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAdvisorProfiling
{
    public class AdviserStaffSMTPVo
    {
        private int m_RMId;

        public int RMId
        {
            get { return m_RMId; }
            set { m_RMId = value; }
        }
        private int m_AdvisorId;

        public int AdvisorId
        {
            get { return m_AdvisorId; }
            set { m_AdvisorId = value; }
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
        private string m_Email;

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }
        private string m_HostServer;

        public string HostServer
        {
            get { return m_HostServer; }
            set { m_HostServer = value; }
        }
        private int m_IsAuthenticationRequired;

        public int IsAuthenticationRequired
        {
            get { return m_IsAuthenticationRequired; }
            set { m_IsAuthenticationRequired = value; }
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
        private string m_Password;

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        private string m_Port;

        public string Port
        {
            get { return m_Port; }
            set { m_Port = value; }
        }
        private string m_SenderEmailAlias;
        public string SenderEmailAlias
        {
            get { return m_SenderEmailAlias; }
            set { m_SenderEmailAlias = value; }
        
        }
        private int m_SmsProviderId;
        public int SmsProviderId
        {
            get { return m_SmsProviderId; }
            set { m_SmsProviderId = value; }
        }
        private string m_SmsURL;
        public string SmsURL
        {
            get { return m_SmsURL; }
            set { m_SmsURL = value; }
        }
        private string m_SmsSenderId;
        public string SmsSenderId
        {
            get { return m_SmsSenderId; }
            set { m_SmsSenderId = value; }
        }
        private string m_SmsUserName;
        public string SmsUserName
        {
            get { return m_SmsUserName; }
            set { m_SmsUserName = value; }
        }
        private string m_Smspassword;
        public string Smspassword
        {
            get { return m_Smspassword; }
            set { m_Smspassword = value; }
        }
        private int m_SmsInitialcredit;
        public int SmsInitialcredit
        {
            get { return m_SmsInitialcredit; }
            set { m_SmsInitialcredit = value; }
        }
        private int m_SmsCreditLeft;
        public int SmsCreditLeft
        {
            get { return m_SmsCreditLeft; }
            set { m_SmsCreditLeft = value; }
        }
        private int m_SmsCreatedBy;
        public int SmsCreatedBy
        {
            get { return m_SmsCreatedBy; }
            set { m_SmsCreatedBy = value; }
        }
        private int m_SmsModifiedBy;
        public int SmsModifiedBy
        {
            get { return m_SmsModifiedBy; }
            set { m_SmsModifiedBy = value; }
        }

    }
}
