using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFPortfolioVo
    {
        private int m_MfPortfolioId;

      
        private int m_CustomerId;
        private int m_FundCode;
        private string m_FundName;        
        private int m_MFCode;
        private string m_SchemePlan;        
        private string m_Folio;
        private int m_AccountId;
        private DateTime m_ValuationDate;
        private string m_CategoryCode;

        private string m_Category;

       
        private List<MFPortfolioTransactionVo> m_MFPortfolioTransactionVoList;
        private double m_Quantity;
        private double m_AveragePrice;
        private double m_CostOfPurchase;
        private double m_CurrentNAV;
        private double m_CurrentValue;
        private double m_RealizedPNL;
        private double m_UnRealizedPNL;
        private double m_TotalPNL;
        private double m_SalesQuantity;
        private double m_RealizedSalesProceed;
        private double m_CostOfSales;
        private double m_NetCost;
        private double m_XIRR;
        private double m_RealizedXIRR;

        private double m_AbsoluteReturn;
        private double m_AnnualReturn;
        private double m_DividendIncome;
        private double m_DividendPayout;
        private double m_DividendReinvested;
        private double m_AcqCostExclDivReinvst;
        private double m_LTCG;
        private double m_STCG;
        private double m_STCGEligible;

        public double STCGEligible
        {
            get { return m_STCGEligible; }
            set { m_STCGEligible = value; }
        }
        private double m_LTCGEligible;

        public double LTCGEligible
        {
            get { return m_LTCGEligible; }
            set { m_LTCGEligible = value; }
        }
        private DateTime m_DateOfAcq;
        private bool m_ContainsFolioTransfer;

        public DateTime DateOfAcq
        {
            get { return m_DateOfAcq; }
            set { m_DateOfAcq = value; }
        }

        public double STCG
        {
            get { return m_STCG; }
            set { m_STCG = value; }
        }

        public double LTCG
        {
            get { return m_LTCG; }
            set { m_LTCG = value; }
        }
        public double AcqCostExclDivReinvst
        {
            get { return m_AcqCostExclDivReinvst; }
            set { m_AcqCostExclDivReinvst = value; }
        }
        public double DividendPayout
        {
            get { return m_DividendPayout; }
            set { m_DividendPayout = value; }
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

        public double DividendReinvested
        {
            get { return m_DividendReinvested; }
            set { m_DividendReinvested = value; }
        }
        public double AnnualReturn
        {
            get { return m_AnnualReturn; }
            set { m_AnnualReturn = value; }
        }
        public double AbsoluteReturn
        {
            get { return m_AbsoluteReturn; }
            set { m_AbsoluteReturn = value; }
        }   


        public double XIRR
        {
            get { return m_XIRR; }
            set { m_XIRR = value; }
        }

        public double RealizedXIRR
        {
            get { return m_RealizedXIRR; }
            set { m_RealizedXIRR = value; }
        }

        public int MfPortfolioId
        {
            get { return m_MfPortfolioId; }
            set { m_MfPortfolioId = value; }
        }
        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }

        public int FundCode
        {
            get { return m_FundCode; }
            set { m_FundCode = value; }
        }

        public string FundName
        {
            get { return m_FundName; }
            set { m_FundName = value; }
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

        public string Folio
        {
            get { return m_Folio; }
            set { m_Folio = value; }
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

        public List<MFPortfolioTransactionVo> MFPortfolioTransactionVoList
        {
            get { return m_MFPortfolioTransactionVoList; }
            set { m_MFPortfolioTransactionVoList = value; }
        }

        public double Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        public double AveragePrice
        {
            get { return m_AveragePrice; }
            set { m_AveragePrice = value; }
        }

        public double CostOfPurchase
        {
            get { return m_CostOfPurchase; }
            set { m_CostOfPurchase = value; }
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

        public double RealizedPNL
        {
            get { return m_RealizedPNL; }
            set { m_RealizedPNL = value; }
        }

        public double UnRealizedPNL
        {
            get { return m_UnRealizedPNL; }
            set { m_UnRealizedPNL = value; }
        }

        public double TotalPNL
        {
            get { return m_TotalPNL; }
            set { m_TotalPNL = value; }
        }

        public double SalesQuantity
        {
            get { return m_SalesQuantity; }
            set { m_SalesQuantity = value; }
        }
        public double RealizedSalesProceed
        {
            get { return m_RealizedSalesProceed; }
            set { m_RealizedSalesProceed = value; }
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

        public double DividendIncome
        {
            get { return m_DividendIncome; }
            set { m_DividendIncome = value; }
        }


        public bool ContainsFolioTransfer
        {
            get { return m_ContainsFolioTransfer; }
            set { m_ContainsFolioTransfer = value; }
        }
    }
}
