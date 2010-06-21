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

    }
}
