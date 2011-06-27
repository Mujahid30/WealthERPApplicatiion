using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LoanProposalStageVo
    {
        //private LoanApplicationEntryVo m_LoanApplEntryVo;

        ///* Proposal Stage Fields */
        //public LoanApplicationEntryVo LoanApplEntryVo 
        //{
        //    get { return m_LoanApplEntryVo; }
        //    set { m_LoanApplEntryVo = value; } 
        //}

        //public LoanEligibilityStatusVo LoanEligibilityStatusVo { get; set; }

        //public LoanSanctionVo LoanSanctionVo { get; set; }

        //public LoanDisbursalVo LoanDisbursalVo { get; set; }

        //public LoanClosureVo LoanClosureVo { get; set; }

        /* Application Entry */
        public int Application_ProposalStageId { get; set; }
        public int Application_LoanProposalId { get; set; }
        public Int16 Application_IsOpen { get; set; }
        public Int16 Application_DocumentCollection { get; set; }
        public Int16 Application_Entry { get; set; }
        public string Application_Remark { get; set; }

        /* Eligibility Status */
        public int Eligibility_LoanProposalId { get; set; }
        public int Eligibility_ProposalStageLogId { get; set; }
        public int Eligibility_ProposalStageId { get; set; }
        public string Eligibility_DecisionCode { get; set; }
        public int Eligibility_DeclineReasonCode { get; set; }
        public DateTime Eligibility_LogDate { get; set; }
        public string Eligibility_Remark { get; set; }
        public Int16 Eligibility_IsOpen { get; set; }

        /* Bank Sanction */
        public int BankSanction_LoanProposalId { get; set; }
        public int BankSanction_ProposalStageLogId { get; set; }
        public int BankSanction_ProposalStageId { get; set; }
        public string BankSanction_DecisionCode { get; set; }
        public int BankSanction_DeclineReasonCode { get; set; }
        public DateTime BankSanction_LogDate { get; set; }
        public string BankSanction_Remark { get; set; }
        public Int16 BankSanction_IsOpen { get; set; }

        /* Disbursal */
        public int Disbursal_LoanProposalId { get; set; }
        public int Disbursal_ProposalStageId { get; set; }
        public Int16 Disbursal_IsOpen { get; set; }
        public string Disbursal_Remark { get; set; }

        /* Closure */
        public int Closure_LoanProposalId { get; set; }
        public int Closure_ProposalStageId { get; set; }
        public Int16 Closure_IsOpen { get; set; }
        public string Closure_Remark { get; set; }
    }
}
