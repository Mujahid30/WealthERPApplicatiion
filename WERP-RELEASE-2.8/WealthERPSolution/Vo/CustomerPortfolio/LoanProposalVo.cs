using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerPortfolio
{
    public class LoanProposalVo
    {
        public int LoanProposalId { get; set; }
        public int BranchId { get; set; }
        public int LoanTypeId { get; set; }
        public int LoanPartnerId { get; set; }
        public int SchemeId { get; set; }
        public Int16 IsMainBorrowerMinor { get; set; }
        public int ApplicationNum { get; set; }
        public Double AppliedLoanAmount { get; set; }
        public int AppliedLoanPeriod { get; set; }
        public string Introducer { get; set; }
        public string Remark { get; set; }
        public string BankReferenceNum { get; set; }
        public DateTime SanctionDate { get; set; }
        public Double SanctionAmount { get; set; }
        public float SanctionInterestRate { get; set; }
        public Double EMIAmount { get; set; }
        public int EMIDate { get; set; }
        public string RepaymentType { get; set; }
        public string EMIFrequency { get; set; }
        public int NoOfInstallments { get; set; }
        public Double AmountPrepaid { get; set; }
        public DateTime InstallmentStartDate { get; set; }
        public DateTime InstallmentEndDate { get; set; }
        public Int16 IsFloatingRate { get; set; }
        public Int32 InterestCategoryId { get; set; }

        public string CustomerName { get; set; }
        public string LoanType { get; set; }
        public string LoanStage { get; set; }
        public string LoanPartner { get; set; }
        public Double Commission { get; set; }

        public string ClientId { get; set; }
        public string GuarantorId { get; set; }
    }
}
