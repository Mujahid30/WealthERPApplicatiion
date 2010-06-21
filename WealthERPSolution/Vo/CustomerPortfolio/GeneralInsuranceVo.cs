using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class GeneralInsuranceVo
    {
        public int AccountId{ set; get; }
        public int PortfolioId { set; get; }
        public string AssetInstrumentCategoryCode { set; get; }
        public string AssetInstrumentCategoryName { set; get; }
        public string AssetInstrumentSubCategoryCode { set; get; }
        public string AssetInstrumentSubCategoryName { set; get; }
        public string AssetGroupCode { set; get; }
        public string PolicyNumber { set; get; }
        public int ModifiedBy { set; get; }
        public int CreatedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
        public DateTime CreatedOn { set; get; }

        public int GINetPositionId {set; get;}
        public string InsIssuerCode { set; get; }
        public string InsIssuerName { set; get; }
        public string PolicyParticular { set; get; }
        public DateTime OriginalStartDate { set; get; }
        public int IsFamilyPolicy { set; get; }
        public string PolicyTypeCode { set; get; }
        public string PolicyType { set; get; }
        public double SumAssured { set; get; }
        public string TPAName { set; get; }
        public long TPAContactNumber { set; get; }
        public int IsEligibleFreeHealth { set; get; }
        public DateTime CheckupDate { set; get; }
        public DateTime ProposalDate { set; get; }
        public string ProposalNumber { set; get; }
        public DateTime PolicyValidityStartDate { set; get; }
        public DateTime PolicyValidityEndDate { set; get; }
        public double PremiumAmount { set; get; }
        public string FrequencyCode { set; get; }
        public string Frequency { set; get; }
        public string Remarks { set; get; }
        public int IsProvidedByEmployer { set; get; }
        
        public string AssetGroup { set; get; }
        public string AssetTable { set; get; }
        public int AssetId { set; get; }
    }
}
