using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAlerts
{
    public class AlertsNotificationVo
    {
        #region Fields

        private string m_Category;
        private long m_EventQueueID;
        private long m_EventSetupID;
        private int m_EventID;
        private string m_EventMessage;
        private int m_SchemeID;
        private int m_TargetID;
        private Int16 m_ModeId;
        private string m_IsAlerted;
        private DateTime m_PopulatedDate;
        private string m_IsDeleted;
        private int m_CreatedBy;
        private string m_Name;
        private string m_EventCode;
        private string m_Reminder;

        #endregion

        #region Properties


        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        public long NotificationID
        {
            get { return m_EventQueueID; }
            set { m_EventQueueID = value; }
        }

        public long EventSetupID
        {
            get { return m_EventSetupID; }
            set { m_EventSetupID = value; }
        }

        public int EventID
        {
            get { return m_EventID; }
            set { m_EventID = value; }
        }

        public string EventMessage
        {
            get { return m_EventMessage; }
            set { m_EventMessage = value; }
        }

        public int SchemeID
        {
            get { return m_SchemeID; }
            set { m_SchemeID = value; }
        }

        public int CustomerID
        {
            get { return m_TargetID; }
            set { m_TargetID = value; }
        }

        public Int16 ModeId
        {
            get { return m_ModeId; }
            set { m_ModeId = value; }
        }

        public string IsAlerted
        {
            get { return m_IsAlerted; }
            set { m_IsAlerted = value; }
        }

        public DateTime PopulatedDate
        {
            get { return m_PopulatedDate; }
            set { m_PopulatedDate = value; }
        }

        public string IsDeleted
        {
            get { return m_IsDeleted; }
            set { m_IsDeleted = value; }
        }

        public string EventCode
        {
            get { return m_EventCode; }
            set { m_EventCode = value; }
        }

        public string Reminder
        {
            get { return m_Reminder; }
            set { m_Reminder = value; }
        }

        public int CreatedBy
        {
            get { return m_CreatedBy; }
            set { m_CreatedBy = value; }
        }


        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        #endregion
    }
}
