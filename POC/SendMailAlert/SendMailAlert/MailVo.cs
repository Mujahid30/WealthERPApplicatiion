using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendMailAlert
{
    public class MailVo
    {
        private string m_host;

        public string Host
        {
            get { return m_host; }
            set { m_host = value; }
        }
        private int m_port;

        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        private string m_From;

        public string From
        {
            get { return m_From; }
            set { m_From = value; }
        }
        private string m_UserName;

        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
        private string m_Password;

        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        private string[] m_To;

        public string[] To
        {
            get { return m_To; }
            set { m_To = value; }
        }
        private string[] m_Cc;

        public string[] Cc
        {
            get { return m_Cc; }
            set { m_Cc = value; }
        }
        private string[] m_Bcc;

        public string[] Bcc
        {
            get { return m_Bcc; }
            set { m_Bcc = value; }
        }
        private string m_Subject;

        public string Subject
        {
            get { return m_Subject; }
            set { m_Subject = value; }
        }

        private string m_Message;

        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
        private int m_IsBulk;

        public int IsBulk
        {
            get { return m_IsBulk; }
            set { m_IsBulk = value; }
        }

    }
}
