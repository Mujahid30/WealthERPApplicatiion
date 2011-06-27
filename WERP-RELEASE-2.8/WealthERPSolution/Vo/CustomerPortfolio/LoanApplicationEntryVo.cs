using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LoanApplicationEntryVo
    {
        public int ProposalStageId { get; set; }
        public int LoanProposalId { get; set; }
        public Int16 IsOpen { get; set; }
        public Int16 DocumentCollection { get; set; }
        public Int16 Entry { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// Documents Activity Id
        /// </summary>
        public int ApplActivityId1 { get; set; }
        /// <summary>
        /// Entry Activity Id
        /// </summary>
        public int ApplActivityId2 { get; set; }
    }
}
