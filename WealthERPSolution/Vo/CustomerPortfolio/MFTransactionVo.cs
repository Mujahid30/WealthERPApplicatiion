using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    /// <summary>
    /// Class Containing MF Transaction Details.
    /// </summary>
    public class MFTransactionVo : ICloneable
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
        private double m_BrokerageAmount;
        private double m_Units;
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
        private double m_CurrentValue;

        private int m_TransactionStatusCode;

        private int m_ProcessId;
        private string m_SubCategoryName;
        private string m_SubBrokerCode;
        private int m_Age;
        private double m_Balance;
        private DateTime m_CreatedOn;
        private string m_OriginalTransactionNumber;
        public int AgentId { get; set; }
        public string m_AgentCode;
        private string m_Area;
        private string m_EUIN;
        private string m_ZMName;
        private string m_AName;
        private string m_SubbrokerName;
        private string m_Channel;
        private string m_Titles;
        private string m_CircleManager;
        private string m_ReportingManagerName;
        private string m_UserType;
        private string m_DeuptyHead;
        private string m_ClusterMgr;
        private DateTime m_ELSSMaturityDate;
        private int m_RequestId;
        #endregion Fields

        #region Properties
        public string DivReinvestmen { get; set; }
        public string Divfrequency { get; set; }
        public float latestNav { get; set; }
        public int orderNo { get; set; }
        public string channel { get; set; }
        public string TrxnNo { get; set; }
        public string UserTransactionNo { get; set; }
        public DateTime OrdDate { get; set; }
        public int userTransactionNo { get; set; }
        public string PortfolioName
        {
            get { return m_PortfolioName; }
            set { m_PortfolioName = value; }
        }
        public string AgentCode
        {
            get { return m_AgentCode; }
            set { m_AgentCode = value; }
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
        public double BrokerageAmount
        {
            get { return m_BrokerageAmount; }
            set { m_BrokerageAmount = value; }
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

        public int ProcessId
        {
            get { return m_ProcessId; }
            set { m_ProcessId = value; }
        }

        public string SubCategoryName
        {
            get { return m_SubCategoryName; }
            set { m_SubCategoryName = value; }
        }

        public string SubBrokerCode
        {
            get { return m_SubBrokerCode; }
            set { m_SubBrokerCode = value; }
        }
        public int Age
        {
            get { return m_Age; }
            set { m_Age = value; }
        }


        public double Balance
        {
            get { return m_Balance; }
            set { m_Balance = value; }
        }
        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }


        public DateTime CreatedOn
        {
            get { return m_CreatedOn; }
            set { m_CreatedOn = value; }

        }
        public string OriginalTransactionNumber
        {
            get { return m_OriginalTransactionNumber; }
            set { m_OriginalTransactionNumber = value; }
        }
        public string Area
        {
            get { return m_Area; }
            set { m_Area = value; }

        }
        public string EUIN
        {
            get { return m_EUIN; }
            set { m_EUIN = value; }

        }
        public string ZMName
        {
            get { return m_ZMName; }
            set { m_ZMName = value; }

        }

        public string AName
        {
            get { return m_AName; }
            set { m_AName = value; }

        }
        public string SubbrokerName
        {
            get { return m_SubbrokerName; }
            set { m_SubbrokerName = value; }

        }
        public string Channel
        {
            get { return m_Channel; }
            set { m_Channel = value; }

        }

        public string Titles
        {
            get { return m_Titles; }
            set { m_Titles = value; }

        }
        public string CircleManager
        {
            get { return m_CircleManager; }
            set { m_CircleManager = value; }

        }
        public string ReportingManagerName
        {
            get { return m_ReportingManagerName; }
            set { m_ReportingManagerName = value; }

        }
        public string UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }

        }
          public string DeuptyHead
        {
            get { return m_DeuptyHead; }
            set { m_DeuptyHead = value; }

        }
          public string ClusterMgr
          {
              get { return m_ClusterMgr; }
              set { m_ClusterMgr = value; }

          }
          public DateTime ELSSMaturityDate
          {
              get { return m_ELSSMaturityDate; }
              set { m_ELSSMaturityDate = value; }

          }
          public int RequestId
          {
              get { return m_RequestId; }
              set { m_RequestId = value; }
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
            clone.m_RequestId = m_RequestId;
            return clone;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion ICloneable Members
    }

}
