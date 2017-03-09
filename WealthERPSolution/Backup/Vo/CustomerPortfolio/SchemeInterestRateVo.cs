using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class SchemeInterestRateVo
    {
        public int LoanSchemeInterestRateId { set; get; }
        public int LoanSchemeId { set; get; }
        public string InterestCategory { set; get; }
        public float MinimumFinance { set; get; }
        public float MaximumFinance { set; get; }
        public int MinimumPeriod { set; get; }
        public int MaximumPeriod { set; get; }
        public float DifferentialInterestRate { set; get; }
        public float ProcessingCharges { set; get; }
        public float PreClosingCharges { set; get; }
        public float MaximumFinancePer { set; get; }
        public int CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
       
    }

}
