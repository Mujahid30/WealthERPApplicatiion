using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoEmailSMS
{
    public class SMSVo
    {
        private int m_SMSId;

        public int SMSId
        {
            get { return m_SMSId; }
            set { m_SMSId = value; }
        }
        private long m_Mobile;

        public long Mobile
        {
            get { return m_Mobile; }
            set { m_Mobile = value; }
        }
        private string m_Message;

        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
        private int m_IsSent;

        public int IsSent
        {
            get { return m_IsSent; }
            set { m_IsSent = value; }
        }
        private int m_CustomerId;

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
    }
}
