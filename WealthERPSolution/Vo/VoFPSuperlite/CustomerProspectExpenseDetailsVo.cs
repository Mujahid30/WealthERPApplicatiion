using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoFPSuperlite
{
    public class CustomerProspectExpenseDetailsVo
    {
        private int FP_ExpenseDetailsId;

       
        private int FPS_CP_ExpenseCategoryCode;


        private double FPS_CP_ExpenseValue;

        public int ExpenseDetailsId
        {
            get { return FP_ExpenseDetailsId; }
            set { FP_ExpenseDetailsId = value; }
        }
        public int ExpenseCategoryCode
        {
            get { return FPS_CP_ExpenseCategoryCode; }
            set { FPS_CP_ExpenseCategoryCode = value; }
        }
        public double ExpenseValue
        {
            get { return FPS_CP_ExpenseValue; }
            set { FPS_CP_ExpenseValue = value; }
        }

    }
}
