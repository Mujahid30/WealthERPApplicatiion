using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class MFPortfolioNetPositionVo
    {
        public int MFPortfolioId { get; set; }
        public int CustomerId { get; set; }
        public string SchemePlan { get; set; }
        public int SchemePlanCode { get; set; }
        public string FolioNumber { get; set; }
        public string AssetInstrumentCategoryName { get; set; }
        public string AssetInstrumentCategoryCode { get; set; }
        public int AMCCode { get; set; }
        public int AccountId { get; set; }
        public double MarketPrice { get; set; }
        public DateTime ValuationDate { get; set; }
        public double NetHoldings { get; set; }
        public double SalesQuantity { get; set; }
        public double RedeemedAmount { get; set; }
        public double InvestedCost { get; set; }
        public double CurrentValue { get; set; }
        public double ReturnsAllTotalPL { get; set; }
        public double ReturnsAllAbsReturn { get; set; }
        public double ReturnsAllTotalXIRR { get; set; }
        public double ReturnsAllDVRAmt { get; set; }
        public double ReturnsAllDVPAmt { get; set; }
        public double ReturnsAllPrice { get; set; }
        public double ReturnsAllTotalDividends { get; set; }
        public double ReturnsHoldAcqCost { get; set; }
        public double ReturnsHoldTotalPL { get; set; }
        public double ReturnsHoldAbsReturn { get; set; }
        public double ReturnsHoldPurchaseUnit { get; set; }
        public double ReturnsHoldDVRUnits { get; set; }
        public double ReturnsHoldDVPAmt { get; set; }
        public double ReturnHoldDVRAmounts { get; set; }
        public DateTime NavDate { get; set; }
        public double ReturnsHoldXIRR { get; set; }
        public double ReturnsRealizedInvestedCost { get; set; }
        public double ReturnsRealizedDVPAmt { get; set; }
        public double ReturnsRealizedDVRAmt { get; set; }
        public double ReturnsRealizedTotalPL { get; set; }
        public double ReturnsRealizedAbsReturn { get; set; }
        public double ReturnsRealizedXIRR { get; set; }
        public double ReturnsRealizedTotalDividends { get; set; }
        public double TaxHoldPurchaseUnits { get; set; }
        public double TaxHoldBalanceAmt { get; set; }
        public double TaxHoldTotalPL { get; set; }
        public double TaxHoldEligibleSTCG { get; set; }
        public double TaxHoldEligibleLTCG { get; set; }
        public double TaxRealizedTotalPL { get; set; }
        public double TaxRealizedAcqCost { get; set; }
        public double TaxRealizedSTCG { get; set; }
        public double TaxRealizedLTCG { get; set; }
        public string AssetInstrumentSubCategoryName { get; set; }
        public DateTime FolioStartDate { get; set; }
        public DateTime InvestmentStartDate { get; set; }
        public double AnnualisedReturns { get; set; }
        public double WeightageNAV { get; set; }
        public int WeightageDays { get; set; }
        public string AmcName { get; set; }
        public bool IsSchemeSIPType { get; set; }
        public bool IsSchemePurchege { get; set; }
        public bool IsSchemeRedeem { get; set; }
        public int SchemeRatingOverall { get; set; }
        public DateTime SchemeRatingSubscriptionExpiryDtae { get; set; }

        public int SchemeRating3Year { get; set; }
        public int SchemeRating5Year { get; set; }
        public int SchemeRating10Year { get; set; }

        public string SchemeReturn3Year { get; set; }
        public string SchemeReturn5Year { get; set; }
        public string SchemeReturn10Year { get; set; }

        public string SchemeRisk3Year { get; set; }
        public string SchemeRisk5Year { get; set; }
        public string SchemeRisk10Year { get; set; }
        public string SchemeRatingDate { get; set; }
        
    }
}
