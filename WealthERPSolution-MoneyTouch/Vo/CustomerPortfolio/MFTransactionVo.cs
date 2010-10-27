using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    /// <summary>
    /// Class Containing MF Transaction Details.
    /// </summary>
    public class MFTransactionVo:ICloneable
    {
        #region Fields
        //Test Comment
        private int m_TransactionId;
        private int m_CustomerId;
        private int m_PortfolioId;
        private int m_AccountId;
        private string m_Folio;
        private string m_AMCName;

        private int m_AMCCode;

        private int m_MFCode;
        private string m_SchemePlan;
        private string m_Category;
        private string m_CategoryCode;

        private int m_FinancialFlag;
        private DateTime m_TransactionDate;
        private float m_DividendRate;
        private float m_NAV;
        private float m_Price;
        private double m_Amount;
        private double  m_Units;
        private float m_STT;
        private string m_Source;
        private int m_IsSourceManual;
        private int m_SwitchSourceTrxId;
        private string m_BuySell;
        private string m_TransactionType;
        private string m_TransactionClassificationCode;
        private string m_TransactionTrigger;
        private double m_XIRR;
        private string m_CustomerName;
        private string m_PortfolioName;
        private string m_TransactionStatus;

        private int m_TransactionStatusCode;


        #endregion Fields

        #region Properties
        public string PortfolioName
        {
            get { return m_PortfolioName; }
            set { m_PortfolioName = value; }
        }

        public string AMCName
        {
            get { return m_AMCName; }
            set { m_AMCName = value; }
        }
        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        public double XIRR
        {
            get { return m_XIRR; }
            set { m_XIRR = value; }
        }

        public int AMCCode
        {
            get { return m_AMCCode; }
            set { m_AMCCode = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public int PortfolioId
        {
            get { return m_PortfolioId; }
            set { m_PortfolioId = value; }
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

        public int TransactionId
        {
            get { return m_TransactionId; }
            set { m_TransactionId = value; }
        }

        public int MFCode
        {
            get { return m_MFCode; }
            set { m_MFCode = value; }
        }

        public string SchemePlan
        {
            get { return m_SchemePlan; }
            set { m_SchemePlan = value; }
        }
        

        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
       
        public string CategoryCode
        {
            get { return m_CategoryCode; }
            set { m_CategoryCode = value; }
        }
        public int FinancialFlag
        {
            get { return m_FinancialFlag; }
            set { m_FinancialFlag = value; }
        }

        public float DividendRate
        {
            get { return m_DividendRate; }
            set { m_DividendRate = value; }
        }

        public DateTime TransactionDate
        {
            get { return m_TransactionDate; }
            set { m_TransactionDate = value; }
        }

        public float NAV
        {
            get { return m_NAV; }
            set { m_NAV = value; }
        }

        public float Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }

        public double Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        public double Units
        {
            get { return m_Units; }
            set { m_Units = value; }
        }

        public float STT
        {
            get { return m_STT; }
            set { m_STT = value; }
        }

        public string Source
        {
            get { return m_Source; }
            set { m_Source = value; }
        }

        public int IsSourceManual
        {
            get { return m_IsSourceManual; }
            set { m_IsSourceManual = value; }
        }

        public int SwitchSourceTrxId
        {
            get { return m_SwitchSourceTrxId; }
            set { m_SwitchSourceTrxId = value; }
        }

        public string BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }

        public string TransactionType
        {
            get { return m_TransactionType; }
            set { m_TransactionType = value; }
        }

        public string TransactionClassificationCode
        {
            get { return m_TransactionClassificationCode; }
            set { m_TransactionClassificationCode = value; }
        }

        public string TransactionTrigger
        {
            get { return m_TransactionTrigger; }
            set { m_TransactionTrigger = value; }
        }

        public string TransactionStatus
        {
            get { return m_TransactionStatus; }
            set { m_TransactionStatus = value; }
        }

        public int TransactionStatusCode
        {
            get { return m_TransactionStatusCode; }
            set { m_TransactionStatusCode = value; }
        }
      
        #endregion Properties

        #region ICloneable Members

        public MFTransactionVo Clone()
        {
            MFTransactionVo clone = new MFTransactionVo();
            clone.m_AccountId = m_AccountId;
            clone.m_Amount = m_Amount;
            clone.m_BuySell = m_BuySell;
            clone.m_CustomerId = m_CustomerId;
            clone.m_CustomerName = m_CustomerName;
            clone.m_DividendRate = m_DividendRate;
            clone.m_FinancialFlag = m_FinancialFlag;
            clone.m_Folio = m_Folio;
            clone.m_IsSourceManual = m_IsSourceManual;
            clone.m_MFCode = m_MFCode;
            clone.m_NAV = m_NAV;
            clone.m_PortfolioId = m_PortfolioId;
            clone.m_PortfolioName = m_PortfolioName;
            clone.m_Price = m_Price;
            clone.m_SchemePlan = m_SchemePlan;
            clone.m_Category = m_Category;
            clone.m_CategoryCode = m_CategoryCode;
            clone.m_Source = m_Source;
            clone.m_STT = m_STT;
            clone.m_SwitchSourceTrxId = m_SwitchSourceTrxId;
            clone.m_TransactionClassificationCode = m_TransactionClassificationCode;
            clone.m_TransactionDate = m_TransactionDate;
            clone.m_TransactionId = m_TransactionId;
            clone.m_TransactionTrigger = m_TransactionTrigger;
            clone.m_TransactionType = m_TransactionType;
            clone.m_Units = m_Units;
            clone.m_XIRR = m_XIRR;
            clone.m_TransactionStatus = m_TransactionStatus;
            clone.m_TransactionStatusCode = m_TransactionStatusCode;
            return clone;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion ICloneable Members
    }

}
