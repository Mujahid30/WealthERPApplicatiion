using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LoanProposalDocVo
    {
        /* Proposal Document Fields */
        public int ProposalDocId { get; set; }
        public int LoanProposalId { get; set; }
        public int LiabilitiesAssociationId { get; set; }
        public int DocProofTypeCode { get; set; }
        public string DocProofName { get; set; }
        public Int16 IsAccepted { get; set; }
        public DateTime DocSubmissionDate { get; set; }
        public DateTime DocAcceptedDate { get; set; }
        public string DocAcceptedBy { get; set; }
        public string DocProofCopyTypeCode { get; set; }
        public string DocProofTypeName { get; set; }
    }
}
