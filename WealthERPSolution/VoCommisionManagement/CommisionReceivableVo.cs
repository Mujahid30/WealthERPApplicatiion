using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCommisionManagement
{
    public class CommissionStructureMasterVo
    {
        public Int32 CommissionStructureId { get; set; }
        public int AdviserId { get; set; }
        public string ProductType { get; set; }
        //public bool IsMonetary { get; set; }
        public string AssetCategory { get; set; }
        public string Issuer { get; set; }      

        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
       
        public bool IsClawBackApplicable { get; set; }

        public bool IsArchived { get; set; }
        public DateTime ArchivedOn { get; set; }    

        public string CommissionStructureName { get; set; }
        public bool IsNonMonetaryReward { get; set; }
       
        public string StructureNote { get; set; }

        public int StructureMasterCreatedBy { get; set; }
        public int StructureMasterModifiedBy { get; set; }
        public DateTime StructureMasterCreatedOn { get; set; }
        public DateTime StructureMasterModifiedOn { get; set; }
        public StringBuilder AssetSubCategory { get; set; }

        public Dictionary<string, string> AssetSubCategoryDict { get; set; }

    }

    public class CommissionStructureRuleVo : CommissionStructureMasterVo
    {
        public Int32 IssueId { get; set; }

        public Int32 CommissionStructureRuleId { get; set; }
        public string CommissionType { get; set; }
        public string CustomerType { get; set; }

        public string ApplicableLevelCode { get; set; }
        public string AdviserCityGroupCode { get; set; }

        public bool IsServiceTaxReduced { get; set; }
        public bool IsTDSReduced { get; set; }
        public bool IsOtherTaxReduced { get; set; }

        public string ReceivableFrequency { get; set; }

        public decimal MinInvestmentAmount { get; set; }
        public decimal MaxInvestmentAmount { get; set; }

        public int TenureMin { get; set; }
        public int TenureMax { get; set; }
        public string TenureUnit { get; set; }

        public int MinInvestmentAge { get; set; }
        public int MaxInvestmentAge { get; set; }
        public string InvestmentAgeUnit { get; set; }

        public string TransactionType { get; set; }
        public string SIPFrequency { get; set; }
        public Int32 MinNumberofApplications { get; set; }

        public decimal BrokerageValue { get; set; }
        public string BrokerageUnitCode { get; set; }
        
        public string CalculatedOnCode { get; set; }
        public string AUMFrequency { get; set; }
        public decimal AUMMonth { get; set; }

        //public DateTime RuleValidityStartDate { get; set; }
        //public DateTime RuleValidityEndDate { get; set; }
        public string StructureRuleComment { get; set; }

        public int RuleCreatedBy { get; set; }
        public int RuleModifiedBy { get; set; }
        public DateTime RuleCreatedOn { get; set; }
        public DateTime RuleModifiedOn { get; set; }

    }

    
}
