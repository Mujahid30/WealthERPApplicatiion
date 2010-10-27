using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    /// <summary>
    /// added field name to capture original costumer name
    /// </summary>
    public class CustomerAccountsVo
    {
        #region Fields

        private int m_AccountId;
        private int m_PortfolioId;
        private string m_AccountNum;
        private string m_AssetClass;
        private string m_AssetCategory;
        private string m_AssetCategoryName;
        private string m_AssetSubCategory;
        private string m_AssetSubCategoryName;
        private int m_IsJointHolding;
        private string m_AccountSource;
        private int m_CustomerId;
        private string m_CustomerName;
        private string m_ModeOfHolding;
        private string m_ModeOfHoldingCode;
        private string m_BankName;
        private int m_AMCCode;
        private string m_AMCName;
        private string m_Name; // original costumer name
        private string m_PolicyNum;
        private DateTime m_AccountOpeningDate;
        private string m_BrokerCode;
        private string  m_TradeNum;
        private double m_BrokerageDeliveryPercentage;
        private double m_BrokerageSpeculativePercentage;
        private double m_OtherCharges;
        private string m_BrokerName;
        #endregion Fields


        #region Properties
        public string ModeOfHoldingCode
        {
            get { return m_ModeOfHoldingCode; }
            set { m_ModeOfHoldingCode = value; }
        }
        public string PolicyNum
        {
            get { return m_PolicyNum; }
            set { m_PolicyNum = value; }
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
        public string AccountNum
        {
            get { return m_AccountNum; }
            set { m_AccountNum = value; }
        }
        public string AssetClass
        {
            get { return m_AssetClass; }
            set { m_AssetClass = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public string CustomerName
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }
        public string ModeOfHolding
        {
            get { return m_ModeOfHolding; }
            set { m_ModeOfHolding = value; }
        }
        public string AssetCategory
        {
            get { return m_AssetCategory; }
            set { m_AssetCategory = value; }
        }
        public string AssetCategoryName
        {
            get { return m_AssetCategoryName; }
            set { m_AssetCategoryName = value; }
        }
        public string AssetSubCategory
        {
            get { return m_AssetSubCategory; }
            set { m_AssetSubCategory = value; }
        }
        public string AssetSubCategoryName
        {
            get { return m_AssetSubCategoryName; }
            set { m_AssetSubCategoryName = value; }
        }
        public int IsJointHolding
        {
            get { return m_IsJointHolding; }
            set { m_IsJointHolding = value; }
        }
        public string AccountSource
        {
            get { return m_AccountSource; }
            set { m_AccountSource = value; }
        }

        public string BankName
        {
            get { return m_BankName; }
            set { m_BankName = value; }
        }
        public DateTime AccountOpeningDate
        {
            get { return m_AccountOpeningDate; }
            set { m_AccountOpeningDate = value; }
        }
        public int AMCCode
        {
            get { return m_AMCCode; }
            set { m_AMCCode = value; }
        }

        public string AMCName
        {
            get { return m_AMCName; }
            set { m_AMCName = value; }
        }
        public string Name //original costumer name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public string BrokerCode
        {
            get { return m_BrokerCode; }
            set { m_BrokerCode = value; }
        }
        public string  TradeNum
        {
            get { return m_TradeNum; }
            set { m_TradeNum = value; }
        }
        public double BrokerageDeliveryPercentage
        {
            get { return m_BrokerageDeliveryPercentage; }
            set { m_BrokerageDeliveryPercentage = value; }
        }
        public double BrokerageSpeculativePercentage
        {
            get { return m_BrokerageSpeculativePercentage; }
            set { m_BrokerageSpeculativePercentage = value; }
        }
        public double OtherCharges
        {
            get { return m_OtherCharges; }
            set { m_OtherCharges = value; }
        }
        public string BrokerName
        {
            get { return m_BrokerName; }
            set { m_BrokerName = value; }
        }
        #endregion Properties



    }
}
