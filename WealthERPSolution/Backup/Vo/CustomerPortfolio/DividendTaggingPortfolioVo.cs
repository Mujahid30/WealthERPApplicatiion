using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace VoCustomerPortfolio
{
    public class DividendTaggingPortfolioVo
    {
        private int m_MfPortfolioId;

        public int MfPortfolioId
        {
            get { return m_MfPortfolioId; }
            set { m_MfPortfolioId = value; }
        }
        private int m_CustomerId;

        public int CustomerId
        {
            get { return m_CustomerId; }
            set { m_CustomerId = value; }
        }
        private int m_FundCode;

        public int FundCode
        {
            get { return m_FundCode; }
            set { m_FundCode = value; }
        }
        private string m_FundName;

        public string FundName
        {
            get { return m_FundName; }
            set { m_FundName = value; }
        }
        private int m_MFCode;

        public int MFCode
        {
            get { return m_MFCode; }
            set { m_MFCode = value; }
        }
        private string m_SchemePlan;

        public string SchemePlan
        {
            get { return m_SchemePlan; }
            set { m_SchemePlan = value; }
        }
        private string m_Folio;

        public string Folio
        {
            get { return m_Folio; }
            set { m_Folio = value; }
        }
        private int m_AccountId;

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }
        private DateTime m_ValuationDate;

        public DateTime ValuationDate
        {
            get { return m_ValuationDate; }
            set { m_ValuationDate = value; }
        }
        private string m_CategoryCode;

        public string CategoryCode
        {
            get { return m_CategoryCode; }
            set { m_CategoryCode = value; }
        }
        private string m_Category;

        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        private List<DividendTaggingTransactionVo> m_dividendTaggingTransactionVoList;

        public List<DividendTaggingTransactionVo> DividendTaggingTransactionVoList
        {
            get { return m_dividendTaggingTransactionVoList; }
            set { m_dividendTaggingTransactionVoList = value; }
        }
        
        //Realized Section
        private double m_OriginalUnitsSold;

        public double OriginalUnitsSold
        {
            get { return m_OriginalUnitsSold; }
            set { m_OriginalUnitsSold = value; }
        }
        private double m_ReinvestedUnitsSold;

        public double ReinvestedUnitsSold
        {
            get { return m_ReinvestedUnitsSold; }
            set { m_ReinvestedUnitsSold = value; }
        }
        private double m_TotalUnitsSold;

        public double TotalUnitsSold
        {
            get { return m_TotalUnitsSold; }
            set { m_TotalUnitsSold = value; }
        }
        private double m_CostOfSales;

        public double CostOfSales
        {
            get { return m_CostOfSales; }
            set { m_CostOfSales = value; }
        }
        private double m_SaleProceeds;

        public double SaleProceeds
        {
            get { return m_SaleProceeds; }
            set { m_SaleProceeds = value; }
        }
        private double m_RealizedDividendPayout;

        public double RealizedDividendPayout
        {
            get { return m_RealizedDividendPayout; }
            set { m_RealizedDividendPayout = value; }
        }
        private double m_RealizedDividendReinvested;

        public double RealizedDividendReinvested
        {
            get { return m_RealizedDividendReinvested; }
            set { m_RealizedDividendReinvested = value; }
        }
        private double m_RealizedDividendTotal;

        public double RealizedDividendTotal
        {
            get { return m_RealizedDividendTotal; }
            set { m_RealizedDividendTotal = value; }
        }
        private double m_RealizedPL;

        public double RealizedPL
        {
            get { return m_RealizedPL; }
            set { m_RealizedPL = value; }
        }
        private double m_TotalRealizedPL;

        public double TotalRealizedPL
        {
            get { return m_TotalRealizedPL; }
            set { m_TotalRealizedPL = value; }
        }
        private double m_RealizedAbsReturn;

        public double RealizedAbsReturn
        {
            get { return m_RealizedAbsReturn; }
            set { m_RealizedAbsReturn = value; }
        }
        private double m_RealizedXIRR;

        public double RealizedXIRR
        {
            get { return m_RealizedXIRR; }
            set { m_RealizedXIRR = value; }
        }

        //Holdings Section
        private double m_OutstandingUnits;

        public double OutstandingUnits
        {
            get { return m_OutstandingUnits; }
            set { m_OutstandingUnits = value; }
        }
        private double m_OutstandingOriginalUnits;

        public double OutstandingOriginalUnits
        {
            get { return m_OutstandingOriginalUnits; }
            set { m_OutstandingOriginalUnits = value; }
        }
        private double m_OutstandingDividendUnits;

        public double OutstandingDividendUnits
        {
            get { return m_OutstandingDividendUnits; }
            set { m_OutstandingDividendUnits = value; }
        }
        private double m_AcqCost;

        public double AcqCost
        {
            get { return m_AcqCost; }
            set { m_AcqCost = value; }
        }
        private double m_NetAcqCost;

        public double NetAcqCost
        {
            get { return m_NetAcqCost; }
            set { m_NetAcqCost = value; }
        }
        private double m_CurrentNAV;

        public double CurrentNAV
        {
            get { return m_CurrentNAV; }
            set { m_CurrentNAV = value; }
        }
        private double m_CurrentValue;

        public double CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }
        private double m_OutStandingDividendPayout;

        public double OutStandingDividendPayout
        {
            get { return m_OutStandingDividendPayout; }
            set { m_OutStandingDividendPayout = value; }
        }
        private double m_OutStandingDividendReinvested;

        public double OutStandingDividendReinvested
        {
            get { return m_OutStandingDividendReinvested; }
            set { m_OutStandingDividendReinvested = value; }
        }
        private double m_OutStandingDividendTotal;

        public double OutStandingDividendTotal
        {
            get { return m_OutStandingDividendTotal; }
            set { m_OutStandingDividendTotal = value; }
        }
        private double m_UnRealizedPL;

        public double UnRealizedPL
        {
            get { return m_UnRealizedPL; }
            set { m_UnRealizedPL = value; }
        }
        private double m_OutStandingTotalPL;

        public double OutStandingTotalPL
        {
            get { return m_OutStandingTotalPL; }
            set { m_OutStandingTotalPL = value; }
        }
        private double m_OutStandingAbsReturn;

        public double OutStandingAbsReturn
        {
            get { return m_OutStandingAbsReturn; }
            set { m_OutStandingAbsReturn = value; }
        }
        private double m_OutStandingXIRR;

        public double OutStandingXIRR
        {
            get { return m_OutStandingXIRR; }
            set { m_OutStandingXIRR = value; }
        }

        //All Section
        private double m_AvgCostPerUnit;

        public double AvgCostPerUnit
        {
            get { return m_AvgCostPerUnit; }
            set { m_AvgCostPerUnit = value; }
        }
        private double m_AllDividendPayout;

        public double AllDividendPayout
        {
          get { return m_AllDividendPayout; }
          set { m_AllDividendPayout = value; }
        }
        private double m_AllDividendReinvested;

        public double AllDividendReinvested
        {
            get { return m_AllDividendReinvested; }
            set { m_AllDividendReinvested = value; }
        }
        private double m_AllDividendTotal;

        public double AllDividendTotal
        {
            get { return m_AllDividendTotal; }
            set { m_AllDividendTotal = value; }
        }
        private double m_AllUnRealizedPL;

        public double AllUnRealizedPL
        {
            get { return m_AllUnRealizedPL; }
            set { m_AllUnRealizedPL = value; }
        }
        private double m_AllTotalPL;

        public double AllTotalPL
        {
            get { return m_AllTotalPL; }
            set { m_AllTotalPL = value; }
        }
        private double m_AllAbsReturn;

        public double AllAbsReturn
        {
            get { return m_AllAbsReturn; }
            set { m_AllAbsReturn = value; }
        }
        private double m_AllXIRR;

        public double AllXIRR
        {
            get { return m_AllXIRR; }
            set { m_AllXIRR = value; }
        }



    }
}
