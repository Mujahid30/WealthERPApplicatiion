using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LoanSchemeInterestVo
    {
        /* Loan Scheme Interest Rate */
        public int LoanSchemeInterestRateId { get; set; }
        public int LoanSchemeId { get; set; }
        public string InterestCategory { get; set; }
        public int DifferentialInterestRate { get; set; }
        public int ProcessingCharges { get; set; }
        public int PreClosingCharges { get; set; }
    }
}
