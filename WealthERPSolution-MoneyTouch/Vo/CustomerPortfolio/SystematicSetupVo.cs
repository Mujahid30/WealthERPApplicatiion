using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class SystematicSetupVo
    {
        #region Fields

        private int m_SystematicSetupId;
        private int m_SchemePlanCode;
        private string m_SchemePlan;
        private int m_SchemePlanCodeSwitch;
        private string m_SchemePlanSwitch;
        private int m_AccountId;
        private string m_Folio;
        private string m_SystematicTypeCode;
        private string m_SystematicType;
        private DateTime m_StartDate;
        private DateTime m_EndDate;
        private int m_SystematicDate;
        private double m_Amount;
        private int m_IsManual;
        private string m_SourceCode;
        private string m_SourceName;
        private string m_FrequencyCode;
        private string m_Frequency;
        private string m_PaymentModeCode;
        private string m_PaymentMode;

        #endregion


        #region Properties


        public int SystematicSetupId
        {
            get { return m_SystematicSetupId; }
            set { m_SystematicSetupId = value; }
        }

        public int SchemePlanCode
        {
            get { return m_SchemePlanCode; }
            set { m_SchemePlanCode = value; }
        }

        public string SchemePlan
        {
            get { return m_SchemePlan; }
            set { m_SchemePlan = value; }
        }

        public int SchemePlanCodeSwitch
        {
            get { return m_SchemePlanCodeSwitch; }
            set { m_SchemePlanCodeSwitch = value; }
        }

        public string SchemePlanSwitch
        {
            get { return m_SchemePlanSwitch; }
            set { m_SchemePlanSwitch = value; }
        }

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public string Folio
        {
            get { return m_Folio; }
            set { m_Folio = value; }
        }

        public string SystematicTypeCode
        {
            get { return m_SystematicTypeCode; }
            set { m_SystematicTypeCode = value; }
        }

        public string SystematicType
        {
            get { return m_SystematicType; }
            set { m_SystematicType = value; }
        }

        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }

        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }

        public int SystematicDate
        {
            get { return m_SystematicDate; }
            set { m_SystematicDate = value; }
        }

        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        public int IsManual
        {
            get { return m_IsManual; }
            set { m_IsManual = value; }
        }

        public string SourceCode
        {
            get { return m_SourceCode; }
            set { m_SourceCode = value; }
        }

        public string SourceName
        {
            get { return m_SourceName; }
            set { m_SourceName = value; }
        }

        public string FrequencyCode
        {
            get { return m_FrequencyCode; }
            set { m_FrequencyCode = value; }
        }

        public string Frequency
        {
            get { return m_Frequency; }
            set { m_Frequency = value; }
        }

        public string PaymentModeCode
        {
            get { return m_PaymentModeCode; }
            set { m_PaymentModeCode = value; }
        }

        public string PaymentMode
        {
            get { return m_PaymentMode; }
            set { m_PaymentMode = value; }
        }
        #endregion

    }
}
