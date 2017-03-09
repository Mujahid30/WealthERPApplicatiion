using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LoanDisbursalVo
    {
        public int LoanProposalId { get; set; }
        public int ProposalStageId { get; set; }
        public Int16 IsOpen { get; set; }
        public string Remark { get; set; }
    }
}
