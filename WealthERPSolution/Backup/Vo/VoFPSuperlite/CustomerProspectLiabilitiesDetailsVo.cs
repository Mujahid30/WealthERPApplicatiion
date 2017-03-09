using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerProspectLiabilitiesDetailsVo
    {
        private int FP_LiabilitiesDetailsId;

        
        private int FP_LoanTypeCode;        
        private double FP_LoanOutstanding;       
        private int FP_Tenure;        
        private double FP_EMIAmount;
        private double FP_AdjustedEMIAmount;
        private double FP_TotalEMIAmount;
        private double FP_AdjustedLoan;

       

        public int LiabilitiesDetailsId
        {
            get { return FP_LiabilitiesDetailsId; }
            set { FP_LiabilitiesDetailsId = value; }
        }
        public int LoanTypeCode
        {
            get { return FP_LoanTypeCode; }
            set { FP_LoanTypeCode = value; }
        }
        public double LoanOutstanding
        {
            get { return FP_LoanOutstanding; }
            set { FP_LoanOutstanding = value; }
        }
        public int Tenure
        {
            get { return FP_Tenure; }
            set { FP_Tenure = value; }
        }
        public double EMIAmount
        {
            get { return FP_EMIAmount; }
            set { FP_EMIAmount = value; }
        }
        public double AdjustedEMIAmount
        {
            get { return FP_AdjustedEMIAmount; }
            set { FP_AdjustedEMIAmount = value; }
        }
        public double TotalEMIAmount
        {
            get { return FP_TotalEMIAmount; }
            set { FP_TotalEMIAmount = value; }
        }
        public double AdjustedLoan
        {
            get { return FP_AdjustedLoan; }
            set { FP_AdjustedLoan = value; }
        }
    }
}
