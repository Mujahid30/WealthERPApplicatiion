using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCommisionManagement
{
    public class ReceivableStructureMasterVo
    {
        public string CommissionTypeCode { get; set; }
        //public bool IsMonetary { get; set; }
        public int AdviserId { get; set; }
        public string ApplicableLEvelCode { get; set; }
        public string AssetGroupCode { get; set; }
        public bool IsArchived { get; set; }
        public DateTime ArchivedOn { get; set; }

        public bool IsServicetaxReduced { get; set; }
        public bool IsTDSReduced { get; set; }
        public bool IsOtherTAxReduced { get; set; }

        public string CommissionStructureName { get; set; }
        public bool IsNonMonetaryReward { get; set; }
        public string ReceivableFrequency { get; set; }

        public string RecurringiSIPFrequency { get; set; }
        public bool IsStructureFromIssuer { get; set; }
        public string Note { get; set; }

        public int MasterCreatedBy { get; set; }
        public int MasterModifiedBy { get; set; }
        public DateTime MasterCreatedOn { get; set; }
        public DateTime MasterModifiedOn { get; set; }

    }

    public class ReceivableStructureRuleVo : ReceivableStructureMasterVo
    {
        public string CalculatedOnCode { get; set; }
        public string AUMFrequency { get; set; }
        public decimal AUMMonth { get; set; }
        public decimal MinInvestmentAmount { get; set; }

        public decimal MaxInvestmentAmount { get; set; }
        public int TenureMin { get; set; }
        public int TenureMax { get; set; }
        public Int64 TenureUnit { get; set; }

        public string CustomerType { get; set; }
        public string TransType { get; set; }
        public Int32 MinNumberofApplications { get; set; }
        public decimal BrokerageValue { get; set; }

        public string UnitCode { get; set; }
        public int MinInvestmentAge { get; set; }
        public int MaxInvestmentAge { get; set; }
        public int InvestmentAgeUnit { get; set; }
       
        public string CityGroupCode { get; set; }
        public string BranchCategory { get; set; }

        public DateTime RuleValidityStartDate { get; set; }
        public DateTime RuleValidityEndDate { get; set; }

        public int RuleCreatedBy { get; set; }
        public int RuleModifiedBy { get; set; }
        public DateTime RuleCreatedOn { get; set; }
        public DateTime RuleModifiedOn { get; set; }

    }

    
}
