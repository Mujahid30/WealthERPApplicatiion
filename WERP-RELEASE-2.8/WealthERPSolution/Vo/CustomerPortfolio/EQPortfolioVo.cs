using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class EQPortfolioVo
    {
        private int m_EqPortfolioId;
        private int m_CustomerId;
        private int m_PortfolioId;
        private int m_EQCode;
        private string m_EQCompanyName;
        private string m_EQTicker;
        private int m_AccountId;
        private DateTime m_ValuationDate;

        private List<EQPortfolioTransactionVo> m_EQPortfolioTransactionVo;     
        private float m_Quantity;
        private float m_AveragePrice;
        private double m_CostOfPurchase;
        private float m_MarketPrice;
        private double m_CurrentValue;
        private float m_SalesQuantity;       
        private float m_CostOfSales;     
        private float m_RealizedSalesProceed;
        private float m_NetCost;
        private string m_BuySell;

       
        private double m_UnRealizedPNL;
        private float m_RealizedPNL;
        private float m_SpeculativeSalesQuantity;
        private float m_SpeculativeCostOfSales;        
        private float m_SpeculativeRealizedSalesProceeds;      
        private float m_SpeculativeRealizedProfitLoss;        
        private float m_DeliverySalesQuantity;       
        private float m_DeliveryCostOfSales;        
        private float m_DeliveryRealizedSalesProceeds;      
        private float m_DeliveryRealizedProfitLoss;
        private double m_XIRR;
        private double m_AbsoluteReturn;

        public double AbsoluteReturn
        {
            get { return m_AbsoluteReturn; }
            set { m_AbsoluteReturn = value; }
        }
        private double m_AnnualReturn;

        public double AnnualReturn
        {
            get { return m_AnnualReturn; }
            set { m_AnnualReturn = value; }
        }

        public int EQNetPositionId { get; set; }

        public string ScripName { get; set; }

        public double XIRR
        {
            get { return m_XIRR; }
            set { m_XIRR = value; }
        }

        public int EqPortfolioId
        {
            get { return m_EqPortfolioId; }
            set { m_EqPortfolioId = value; }
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
        public int EQCode
        {
            get { return m_EQCode; }
            set { m_EQCode = value; }
        }

        public string EQCompanyName
        {
            get { return m_EQCompanyName; }
            set { m_EQCompanyName = value; }
        }
        public string BuySell
        {
            get { return m_BuySell; }
            set { m_BuySell = value; }
        }
        public string EQTicker
        {
            get { return m_EQTicker; }
            set { m_EQTicker = value; }
        }

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public DateTime ValuationDate
        {
            get { return m_ValuationDate; }
            set { m_ValuationDate = value; }
        }

        public List<EQPortfolioTransactionVo> EQPortfolioTransactionVo
        {
            get { return m_EQPortfolioTransactionVo; }
            set { m_EQPortfolioTransactionVo = value; }
        }

        public float Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        public float AveragePrice
        {
            get { return m_AveragePrice; }
            set { m_AveragePrice = value; }
        }

        public double CostOfPurchase
        {
            get { return m_CostOfPurchase; }
            set { m_CostOfPurchase = value; }
        }

        public float MarketPrice
        {
            get { return m_MarketPrice; }
            set { m_MarketPrice = value; }
        }

        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }
        public float SalesQuantity
        {
            get { return m_SalesQuantity; }
            set { m_SalesQuantity = value; }
        }
        public float CostOfSales
        {
            get { return m_CostOfSales; }
            set { m_CostOfSales = value; }
        }

        public float RealizedSalesProceed
        {
            get { return m_RealizedSalesProceed; }
            set { m_RealizedSalesProceed = value; }
        }


        public float NetCost
        {
            get { return m_NetCost; }
            set { m_NetCost = value; }
        }

        public double UnRealizedPNL
        {
            get { return m_UnRealizedPNL; }
            set { m_UnRealizedPNL = value; }
        }

        public float RealizedPNL
        {
            get { return m_RealizedPNL; }
            set { m_RealizedPNL = value; }
        }
        public float SpeculativeSalesQuantity
        {
            get { return m_SpeculativeSalesQuantity; }
            set { m_SpeculativeSalesQuantity = value; }
        }
        public float SpeculativeCostOfSales
        {
            get { return m_SpeculativeCostOfSales; }
            set { m_SpeculativeCostOfSales = value; }
        }
        public float SpeculativeRealizedSalesProceeds
        {
            get { return m_SpeculativeRealizedSalesProceeds; }
            set { m_SpeculativeRealizedSalesProceeds = value; }
        }
        public float SpeculativeRealizedProfitLoss
        {
            get { return m_SpeculativeRealizedProfitLoss; }
            set { m_SpeculativeRealizedProfitLoss = value; }
        }
        public float DeliverySalesQuantity
        {
            get { return m_DeliverySalesQuantity; }
            set { m_DeliverySalesQuantity = value; }
        }
        public float DeliveryCostOfSales
        {
            get { return m_DeliveryCostOfSales; }
            set { m_DeliveryCostOfSales = value; }
        }
        public float DeliveryRealizedSalesProceeds
        {
            get { return m_DeliveryRealizedSalesProceeds; }
            set { m_DeliveryRealizedSalesProceeds = value; }
        }
        public float DeliveryRealizedProfitLoss
        {
            get { return m_DeliveryRealizedProfitLoss; }
            set { m_DeliveryRealizedProfitLoss = value; }
        }

    }
}
