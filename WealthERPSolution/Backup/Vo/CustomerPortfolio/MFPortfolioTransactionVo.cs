using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFPortfolioTransactionVo
    {
        #region Fields

        private DateTime m_TransactionDate;
        private string m_TransactionType;
        private string m_TransactionClassificationCode;
        private DateTime m_BuyDate;
        private string m_BuySell;
        private double m_BuyQuantity;
        private double m_BuyPrice;
        private DateTime m_SellDate;        
        private double m_SellQuantity;
        private double m_SellPrice;
        private bool m_Closed;       
        private double m_CurrentNAV;      
        private double m_CurrentValue;        
        private double m_CostOfAcquisition;
        private double m_NewCostOfAcquisition;
        private double m_RealizedSalesValue;
        private double m_CostOfSales;
        private double m_NetCost;
        private int m_AgeOfInvestment;        
        private double m_NetHoldings;
        private double m_AveragePrice;
        private double m_RealizedProfitLoss;
        private double m_NotionalProfitLoss;        
        private double m_TotalProfitLoss;        
        private double m_AbsoluteReturns;        
        private double m_AnnualReturns;   
        private double m_XIRR;
        private double m_STCGTax;        
        private double m_LTCGTax;        
        private double m_NetSalesProceed;
        private double m_STT;

       

        #endregion Fields

        #region Properties

        public DateTime TransactionDate
        {
            get { return m_TransactionDate; }
            set { m_TransactionDate = value; }
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

        public string BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }

        public double BuyQuantity
        {
            get { return m_BuyQuantity; }
            set { m_BuyQuantity = value; }
        }

        public double BuyPrice
        {
            get { return m_BuyPrice; }
            set { m_BuyPrice = value; }
        }

        public double SellQuantity
        {
            get { return m_SellQuantity; }
            set { m_SellQuantity = value; }
        }

        public double SellPrice
        {
            get { return m_SellPrice; }
            set { m_SellPrice = value; }
        }

        public double CostOfAcquisition
        {
            get { return m_CostOfAcquisition; }
            set { m_CostOfAcquisition = value; }
        }
        public double NewCostOfAcquisition
        {
            get { return m_NewCostOfAcquisition; }
            set { m_NewCostOfAcquisition = value; }
        }

        public double RealizedSalesValue
        {
            get { return m_RealizedSalesValue; }
            set { m_RealizedSalesValue = value; }
        }

        public double CostOfSales
        {
            get { return m_CostOfSales; }
            set { m_CostOfSales = value; }
        }

        public double NetCost
        {
            get { return m_NetCost; }
            set { m_NetCost = value; }
        }

        public double NetHoldings
        {
            get { return m_NetHoldings; }
            set { m_NetHoldings = value; }
        }

        public double AveragePrice
        {
            get { return m_AveragePrice; }
            set { m_AveragePrice = value; }
        }

        public double RealizedProfitLoss
        {
            get { return m_RealizedProfitLoss; }
            set { m_RealizedProfitLoss = value; }
        }
        public double XIRR
        {
            get { return m_XIRR; }
            set { m_XIRR = value; }
        }
        public DateTime BuyDate
        {
            get { return m_BuyDate; }
            set { m_BuyDate = value; }
        }
        public DateTime SellDate
        {
            get { return m_SellDate; }
            set { m_SellDate = value; }
        }
        public bool Closed
        {
            get { return m_Closed; }
            set { m_Closed = value; }
        }
        public double CurrentNAV
        {
            get { return m_CurrentNAV; }
            set { m_CurrentNAV = value; }
        }
        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }
        public int AgeOfInvestment
        {
            get { return m_AgeOfInvestment; }
            set { m_AgeOfInvestment = value; }
        }
        public double NotionalProfitLoss
        {
            get { return m_NotionalProfitLoss; }
            set { m_NotionalProfitLoss = value; }
        }
        public double TotalProfitLoss
        {
            get { return m_TotalProfitLoss; }
            set { m_TotalProfitLoss = value; }
        }
        public double AbsoluteReturns
        {
            get { return m_AbsoluteReturns; }
            set { m_AbsoluteReturns = value; }
        }
        public double AnnualReturns
        {
            get { return m_AnnualReturns; }
            set { m_AnnualReturns = value; }
        }
        public double STCGTax
        {
            get { return m_STCGTax; }
            set { m_STCGTax = value; }
        }
        public double LTCGTax
        {
            get { return m_LTCGTax; }
            set { m_LTCGTax = value; }
        }
        public double NetSalesProceed
        {
            get { return m_NetSalesProceed; }
            set { m_NetSalesProceed = value; }
        }

        public double STT
        {
            get { return m_STT; }
            set { m_STT = value; }
        }
        #endregion Properties

    }
}
