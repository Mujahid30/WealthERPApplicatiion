using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class DividendTaggingTransactionVo
    {
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
        private string m_Scheme;

        public string Scheme
        {
            get { return m_Scheme; }
            set { m_Scheme = value; }
        }
        private string m_TransactionClassificationCode;

        public string TransactionClassificationCode
        {
            get { return m_TransactionClassificationCode; }
            set { m_TransactionClassificationCode = value; }
        }
        private string m_BuySell;

        public string BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }
        private string m_TransactionType;

        public string TransactionType
        {
            get { return m_TransactionType; }
            set { m_TransactionType = value; }
        }
        private DateTime m_TransactionDate;

        public DateTime TransactionDate
        {
            get { return m_TransactionDate; }
            set { m_TransactionDate = value; }
        }
        private double m_TransactionAmount;

        public double TransactionAmount
        {
            get { return m_TransactionAmount; }
            set { m_TransactionAmount = value; }
        }
        private double m_TotalValue;

        public double TotalValue
        {
            get { return m_TotalValue; }
            set { m_TotalValue = value; }
        }
        private double m_CostOfHolding;

        public double CostOfHolding
        {
            get { return m_CostOfHolding; }
            set { m_CostOfHolding = value; }
        }
        private double m_CostOfSale;

        public double CostOfSale
        {
            get { return m_CostOfSale; }
            set { m_CostOfSale = value; }
        }
        private double m_Price;

        public double Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }
        private double m_Units;

        public double Units
        {
            get { return m_Units; }
            set { m_Units = value; }
        }
        private double m_OriginalUnits;

        public double OriginalUnits
        {
            get { return m_OriginalUnits; }
            set { m_OriginalUnits = value; }
        }
        private double m_DivUnits;

        public double DivUnits
        {
            get { return m_DivUnits; }
            set { m_DivUnits = value; }
        }
        private double m_TotalOriginalUnits;

        public double TotalOriginalUnits
        {
            get { return m_TotalOriginalUnits; }
            set { m_TotalOriginalUnits = value; }
        }
        private double m_TotalDivUnits;

        public double TotalDivUnits
        {
            get { return m_TotalDivUnits; }
            set { m_TotalDivUnits = value; }
        }
        private double m_BalanceUnits;

        public double BalanceUnits
        {
            get { return m_BalanceUnits; }
            set { m_BalanceUnits = value; }
        }
        private double m_AveragePrice;

        public double AveragePrice
        {
            get { return m_AveragePrice; }
            set { m_AveragePrice = value; }
        }
        private double m_DivRatio;

        public double DivRatio
        {
            get { return m_DivRatio; }
            set { m_DivRatio = value; }
        }
        private double m_UnitCostOfInvestment;

        public double UnitCostOfInvestment
        {
            get { return m_UnitCostOfInvestment; }
            set { m_UnitCostOfInvestment = value; }
        }
        private double m_RealizedPL;

        public double RealizedPL
        {
            get { return m_RealizedPL; }
            set { m_RealizedPL = value; }
        }
    }
}
