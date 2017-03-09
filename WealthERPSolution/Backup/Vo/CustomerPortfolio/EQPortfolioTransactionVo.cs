using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class EQPortfolioTransactionVo
    {
        private DateTime m_TradeDate;
        private string m_TradeSide;
        private string m_TradeType;
        private float m_BuyQuantity;
        private float m_BuyPrice;
        private float m_SellQuantity;
        private float m_SellPrice;
        private float m_CostOfAcquisition;
        private float m_RealizedSalesValue;
        private float m_CostOfSales;
        private float m_NetCost;
        private float m_NetHoldings;
        private float m_AveragePrice;
        private float m_RealizedProfitLoss;
       
        public DateTime TradeDate
        {
            get { return m_TradeDate; }
            set { m_TradeDate = value; }
        }       

        public string TradeSide
        {
            get { return m_TradeSide; }
            set { m_TradeSide = value; }
        }       

        public string TradeType
        {
            get { return m_TradeType; }
            set { m_TradeType = value; }
        }
        
        public float BuyQuantity
        {
            get { return m_BuyQuantity; }
            set { m_BuyQuantity = value; }
        }        

        public float BuyPrice
        {
            get { return m_BuyPrice; }
            set { m_BuyPrice = value; }
        }        

        public float SellQuantity
        {
            get { return m_SellQuantity; }
            set { m_SellQuantity = value; }
        }       

        public float SellPrice
        {
            get { return m_SellPrice; }
            set { m_SellPrice = value; }
        }       

        public float CostOfAcquisition
        {
            get { return m_CostOfAcquisition; }
            set { m_CostOfAcquisition = value; }
        }        

        public float RealizedSalesValue
        {
            get { return m_RealizedSalesValue; }
            set { m_RealizedSalesValue = value; }
        }        

        public float CostOfSales
        {
            get { return m_CostOfSales; }
            set { m_CostOfSales = value; }
        }        

        public float NetCost
        {
            get { return m_NetCost; }
            set { m_NetCost = value; }
        }        

        public float NetHoldings
        {
            get { return m_NetHoldings; }
            set { m_NetHoldings = value; }
        }       

        public float AveragePrice
        {
            get { return m_AveragePrice; }
            set { m_AveragePrice = value; }
        }        

        public float RealizedProfitLoss
        {
            get { return m_RealizedProfitLoss; }
            set { m_RealizedProfitLoss = value; }
        }
    }
}
