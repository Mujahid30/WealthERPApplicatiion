using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VoCustomerPortfolio
{
   public  class SchemeDetailsVo
    {
        public int LoanSchemeId { set; get; }
        public string LoanSchemeName { set; get; }

        public int AdviserId { set; get; }
        public int LoanType { set; get; }
        public int LoanPartner { set; get; }
        public int CustomerCategory { set; get; }
        public double MinimunLoanAmount { set; get; }
        public double MaximumLoanAmount { set; get; }
        public int MinimumLoanPeriod { set; get; }
        public int MaximumLoanPeriod { set; get; }
        public double PLR { set; get; }
        public bool IsFloatingRateInterest { set; get; }
        public double MarginMaintained { set; get; }
        public int MinimumAge { set; get; }
        public int MaximumAge { set; get; }
        public float MinimumSalary { set; get; }
        public float MinimumProfitAmount { set; get; }
        public int MinimumProfitPeriod { set; get; }

        public string SourceLoanSchemeCode { set; get; }
        public string SourceName { set; get; }

        public string Remark { set; get; }
        public int CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
        public List<SchemeProof> schemeProofs = new List<SchemeProof>();
    }

   public class SchemeProof
   {
       public int proofTypeCode;
       public string proofTypeName;
       public List<Proof> proofs = new List<Proof>();
   }
   public struct Proof
   {
       public string proofCode;
       public string proofName;
       public bool isAdded;
   }
}
