using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    /// <summary>
    /// Class Containing Equity Transaction Details.
    /// </summary>
    public class EQTransactionVo : ICloneable
    {
        #region fields

        private int m_TransactionId; 
        private int m_CustomerId;
        private int m_PortfolioId;
        private int m_AccountId;
        private int m_ScripCode;
        private string m_ScripName;
        private string m_Ticker;
        private string m_BuySell;
        private long m_TradeNum;
        private long m_OrderNum;
        private int m_IsSpeculative;
        private string m_TradeType;       
        private string m_Exchange;     
        private DateTime m_TradeDate;        
        private float m_Rate;        
        private float  m_Quantity;   
        private float m_Brokerage;        
        private float m_ServiceTax;
        private float m_EducationCess;       
        private float m_STT;
        private float m_OtherCharges;       
        private float m_RateInclBrokerage;        
        private float m_TradeTotal;
        private string m_BrokerCode;
        private int m_IsSplit;        
        private int m_SplitTransactionId;
        private string m_SourceCode;
        private int m_IsCorpAction;
        private int m_TransactionCode;
        private string m_TransactionType;
        private int m_IsSourceManual;       
        
        
        #endregion fields

        #region properties

        public int TransactionId
        {
            get { return m_TransactionId; }
            set { m_TransactionId = value; }
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

        public int ScripCode
        {
            get { return m_ScripCode; }
            set { m_ScripCode = value; }
        }

        public string ScripName
        {
            get { return m_ScripName; }
            set { m_ScripName = value; }
        }

        public string Ticker
        {
            get { return m_Ticker; }
            set { m_Ticker = value; }
        }

        public long TradeNum
        {
            get { return m_TradeNum; }
            set { m_TradeNum = value; }
        }

        public long OrderNum
        {
            get { return m_OrderNum; }
            set { m_OrderNum = value; }
        }

        public int IsSpeculative
        {
            get { return m_IsSpeculative; }
            set { m_IsSpeculative = value; }
        }

        public string BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }

        public string TradeType
        {
            get { return m_TradeType; }
            set { m_TradeType = value; }
        }        

        public string Exchange
        {
            get { return m_Exchange; }
            set { m_Exchange = value; }
        }       

        public DateTime  TradeDate
        {
            get { return m_TradeDate; }
            set { m_TradeDate = value; }
        }

        public float Rate
        {
            get { return m_Rate; }
            set { m_Rate = value; }
        }

        public float  Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        
        public float Brokerage
        {
            get { return m_Brokerage; }
            set { m_Brokerage = value; }
        }
        
        public float ServiceTax
        {
            get { return m_ServiceTax; }
            set { m_ServiceTax = value; }
        }

        public float EducationCess
        {
            get { return m_EducationCess; }
            set { m_EducationCess = value; }
        }

        public float STT
        {
            get { return m_STT; }
            set { m_STT = value; }
        }

        public float OtherCharges
        {
            get { return m_OtherCharges; }
            set { m_OtherCharges = value; }
        }

        public float RateInclBrokerage
        {
            get { return m_RateInclBrokerage; }
            set { m_RateInclBrokerage = value; }
        }
        
        public float TradeTotal
        {
            get { return m_TradeTotal; }
            set { m_TradeTotal = value; }
        }

        public string BrokerCode
        {
            get { return m_BrokerCode; }
            set { m_BrokerCode = value; }
        }

        public int IsSplit
        {
            get { return m_IsSplit; }
            set { m_IsSplit = value; }
        }

        public int SplitTransactionId
        {
            get { return m_SplitTransactionId; }
            set { m_SplitTransactionId = value; }
        }

        public int IsCorpAction
        {
            get { return m_IsCorpAction; }
            set { m_IsCorpAction = value; }
        }

        public int IsSourceManual
        {
            get { return m_IsSourceManual; }
            set { m_IsSourceManual = value; }
        }

        public string SourceCode
        {
            get { return m_SourceCode; }
            set { m_SourceCode = value; }
        }

        public int TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }

        public string TransactionType
        {
            get { return m_TransactionType; }
            set { m_TransactionType = value; }
        }

        #endregion properties

        public EQTransactionVo Clone()
        {
            EQTransactionVo clone = new EQTransactionVo();
            clone.m_AccountId = m_AccountId;
            clone.m_Brokerage = m_Brokerage;
            clone.m_BrokerCode = m_BrokerCode;
            clone.m_BuySell = m_BuySell;
            clone.m_CustomerId = m_CustomerId;
            clone.m_EducationCess = m_EducationCess;
            clone.m_ScripCode = m_ScripCode;
            clone.m_Exchange = m_Exchange;            
            clone.m_IsSourceManual = m_IsSourceManual;
            clone.m_IsSpeculative = m_IsSpeculative;
            clone.m_IsSplit = m_IsSplit;
            clone.m_OrderNum = m_OrderNum;
            clone.m_OtherCharges = m_OtherCharges;
            clone.m_Quantity = m_Quantity;
            clone.m_Rate = m_Rate;
            clone.m_RateInclBrokerage = m_RateInclBrokerage;
            clone.m_ScripName = m_ScripName;
            clone.m_ServiceTax = m_ServiceTax;
            clone.m_SourceCode = m_SourceCode;
            clone.m_SplitTransactionId = m_SplitTransactionId;
            clone.m_STT = m_STT;
            clone.m_TradeDate = m_TradeDate;
            clone.m_TradeNum = m_TradeNum;
            clone.m_TradeTotal = m_TradeTotal;
            clone.m_TransactionCode = m_TransactionCode;
            clone.m_TransactionId = m_TransactionId;
            clone.m_TransactionType = m_TransactionType;
            clone.m_SourceCode = m_SourceCode;

            return clone;
        }

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion


    }
}
