using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerProspect
    {
        private double FP_totalIncome;

       
        private double FP_totalExpense;

      
        private double FP_totalLiabilities;

        private double FP_totalAssets;

       
        private double FP_totalLifeInsurance;

        
        private double FP_totalGeneralInsurance;


        public double TotalIncome
        {
            get { return FP_totalIncome; }
            set { FP_totalIncome = value; }
        }
        public double TotalExpense
        {
            get { return FP_totalExpense; }
            set { FP_totalExpense = value; }
        }
        public double TotalLiabilities
        {
            get { return FP_totalLiabilities; }
            set { FP_totalLiabilities = value; }
        }
        public double TotalAssets
        {
            get { return FP_totalAssets; }
            set { FP_totalAssets = value; }
        }
        public double TotalLifeInsurance
        {
            get { return FP_totalLifeInsurance; }
            set { FP_totalLifeInsurance = value; }
        }
        public double TotalGeneralInsurance
        {
            get { return FP_totalGeneralInsurance; }
            set { FP_totalGeneralInsurance = value; }
        }
    }
}
