using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LoanProposalStageLogVo
    {
        /* Proposal Stage Log Fields */
        public int ProposalStageLogId { get; set; }
        public int ProposalStageId { get; set; }
        public string DecisionCode { get; set; }
        public int DeclineReasonCode { get; set; }
        public DateTime LogDate { get; set; }
        public string Remark { get; set; }
    }
}
