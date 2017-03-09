using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoAlerts
{
    public class AlertsSetupVo
    {
        #region Fields

        private long m_EventSetupID;
        private int m_EventID;
        private string m_EventMessage;
        private int m_SchemeID;
        private int m_TargetID;
        private DateTime m_EventSubscriptionDate;
        private DateTime? m_NextOccurence;
        private DateTime? m_LastOccurence;
        private DateTime? m_EndDate;
        private int m_CustomerId;
        private string m_DeliveryMode;
        private string m_SentToQueue;
        //private int m_CreatedBy;
        //private int m_ModifiedBy;

        /********************************************/
        private string m_AllFieldNames;
        private string m_EventName;
        private DateTime? m_EventDate;
        private string m_SchemeName;
        private string m_CustomerName;
        private string m_Frequency;
        private int m_FrequencyCode;
        private string m_Reminder;
        private string m_Condition;
        private float m_CurrentValue;
        private float m_PresetValue;



        #endregion


        #region Properties

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

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public DateTime EventSubscriptionDate
        {
            get { return m_EventSubscriptionDate; }
            set { m_EventSubscriptionDate = value; }
        }

        public DateTime? NextOccurence
        {
            get { return m_NextOccurence; }
            set { m_NextOccurence = value; }
        }

        public DateTime? LastOccurence
        {
            get { return m_LastOccurence; }
            set { m_LastOccurence = value; }
        }

        public DateTime? EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }

        public string DeliveryMode
        {
            get { return m_DeliveryMode; }
            set { m_DeliveryMode = value; }
        }

        public string SentToQueue
        {
            get { return m_SentToQueue; }
            set { m_SentToQueue = value; }
        }

 

        /*************************************************/
        public string AllFieldNames
        {
            get { return m_AllFieldNames; }
            set { m_AllFieldNames = value; }
        }
        public string EventName
        {
            get { return m_EventName; }
            set { m_EventName = value; }
        }

        public string SchemeName
        {
            get { return m_SchemeName; }
            set { m_SchemeName = value; }
        }

        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        
        public string Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }

        public string Reminder
        {
            get { return m_Reminder; }
            set { m_Reminder = value; }
        }


        public int TargetID
        {
            get { return m_TargetID; }
            set { m_TargetID = value; }
        }


        public string Condition
        {
            get { return m_Condition; }
            set { m_Condition = value; }
        }


        public float PresetValue
        {
            get { return m_PresetValue; }
            set { m_PresetValue = value; }
        }


        public float CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }

        public int FrequencyCode
        {
            get { return m_FrequencyCode; }
            set { m_FrequencyCode = value; }
        }

        public DateTime? EventDate
        {
            get { return m_EventDate; }
            set { m_EventDate = value; }
        }

        #endregion
    }
}
