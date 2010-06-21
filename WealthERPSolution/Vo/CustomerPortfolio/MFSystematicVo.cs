using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFSystematicVo
    {
        private string m_CustomerName;

        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        private int m_CustomerId;

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        private int m_SystematicSetupId;

        public int SystematicSetupId
        {
            get { return m_SystematicSetupId; }
            set { m_SystematicSetupId = value; }
        }
        private int m_AccountId;

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }
        private string m_FolioNum;

        public string FolioNum
        {
            get { return m_FolioNum; }
            set { m_FolioNum = value; }
        }
        private int m_SchemePlanCode;

        public int SchemePlanCode
        {
            get { return m_SchemePlanCode; }
            set { m_SchemePlanCode = value; }
        }
        private string m_SchemePlanName;

        public string SchemePlanName
        {
            get { return m_SchemePlanName; }
            set { m_SchemePlanName = value; }
        }
        private string m_SystematicTypeCode;

        public string SystematicTypeCode
        {
            get { return m_SystematicTypeCode; }
            set { m_SystematicTypeCode = value; }
        }
        private DateTime m_StartDate;

        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }
        private DateTime m_EndDate;

        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }
        private double m_Amount;

        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }
        private string m_FrequencyCode;

        public string FrequencyCode
        {
            get { return m_FrequencyCode; }
            set { m_FrequencyCode = value; }
        }
        private int m_SystematicDay;

        public int SystematicDay
        {
            get { return m_SystematicDay; }
            set { m_SystematicDay = value; }
        }
        private int m_PortfolioId;

        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
        }
        private int m_AdviserId;

        public int AdviserId
        {
            get { return m_AdviserId; }
            set { m_AdviserId = value; }
        }
        private int m_SwitchSchemePlanCode;

        public int SwitchSchemePlanCode
        {
            get { return m_SwitchSchemePlanCode; }
            set { m_SwitchSchemePlanCode = value; }
        }
        private string m_SwitchSchemePlanName;

        public string SwitchSchemePlanName
        {
            get { return m_SwitchSchemePlanName; }
            set { m_SwitchSchemePlanName = value; }
        }
    }
}
