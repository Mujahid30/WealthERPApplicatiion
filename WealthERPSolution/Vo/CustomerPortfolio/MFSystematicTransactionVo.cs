using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFSystematicTransactionVo
    {
        private string m_CustomerName;

        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        private int m_AccountId;

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
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
        private string m_TransactionClassificationCode;

        public string TransactionClassificationCode
        {
            get { return m_TransactionClassificationCode; }
            set { m_TransactionClassificationCode = value; }
        }
        private double m_Amount;

        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }
        private DateTime m_TransactionDate;

        public DateTime TransactionDate
        {
            get { return m_TransactionDate; }
            set { m_TransactionDate = value; }
        }
        private int m_MFTransId;

        public int MFTransId
        {
            get { return m_MFTransId; }
            set { m_MFTransId = value; }
        }
        private int m_CustomerId;

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
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
        private bool m_MatchFound;

        public bool MatchFound
        {
            get { return m_MatchFound; }
            set { m_MatchFound = value; }
        }

    }
}
