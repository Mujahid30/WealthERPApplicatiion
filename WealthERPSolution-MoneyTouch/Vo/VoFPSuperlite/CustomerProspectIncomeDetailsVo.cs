using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerProspectIncomeDetailsVo
    {
        private int FP_IncomeDetailsId;

        
        private int FPS_CP_IncomeCategoryCode;

        private double FPS_CP_IncomeValue;

        public int IncomeDetailsId
        {
            get { return FP_IncomeDetailsId; }
            set { FP_IncomeDetailsId = value; }
        }
        public int IncomeCategoryCode
        {
            get { return FPS_CP_IncomeCategoryCode; }
            set { FPS_CP_IncomeCategoryCode = value; }
        }
        public double IncomeValue
        {
            get { return FPS_CP_IncomeValue; }
            set { FPS_CP_IncomeValue = value; }
        }

    }
}
