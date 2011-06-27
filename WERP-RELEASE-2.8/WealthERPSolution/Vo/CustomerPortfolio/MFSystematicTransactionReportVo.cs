using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    [Serializable]
    public class MFSystematicTransactionReportVo
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
        private int m_PortfolioId;

        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
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
        private string m_SystematicTransacionType;

        public string SystematicTransacionType
        {
            get { return m_SystematicTransacionType; }
            set { m_SystematicTransacionType = value; }
        }
        private double m_SystematicAmount;

        public double SystematicAmount
        {
            get { return m_SystematicAmount; }
            set { m_SystematicAmount = value; }
        }
        private DateTime m_SystematicTransactionDate;

        public DateTime SystematicTransactionDate
        {
            get { return m_SystematicTransactionDate; }
            set { m_SystematicTransactionDate = value; }
        }
        private string m_OriginalTransactionType;

        public string OriginalTransactionType
        {
            get { return m_OriginalTransactionType; }
            set { m_OriginalTransactionType = value; }
        }
        private double m_OriginalTransactionAmount;

        public double OriginalTransactionAmount
        {
            get { return m_OriginalTransactionAmount; }
            set { m_OriginalTransactionAmount = value; }
        }
        private DateTime m_OriginalTransactionDate;

        public DateTime OriginalTransactionDate
        {
            get { return m_OriginalTransactionDate; }
            set { m_OriginalTransactionDate = value; }
        }

    }
}
