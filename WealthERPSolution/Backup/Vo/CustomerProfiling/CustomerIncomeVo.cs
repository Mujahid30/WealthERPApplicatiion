using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerProfiling
{
    public class CustomerIncomeVo
    {
        public string frequencyCode { get; set; }
        public double grossSalary { get; set; }
        public double grossSalaryYr { get; set; }
        public int currencyCodeGrossSalary { get; set; }
        public double takeHomeSalary { get; set; }
        public double takeHomeSalaryYr { get; set; }
        public int currencyCodeTakeHomeSalary { get; set; }
        public double rentalIncome { get; set; }
        public double rentalIncomeYr { get; set; }
        public int currencyCodeRentalIncome { get; set; }
        public int rentalPropAccountId { get; set; }
        public double pensionIncome { get; set; }
        public double pensionIncomeYr { get; set; }
        public int currencyCodePensionIncome { get; set; }
        public double AgriculturalIncome { get; set; }
        public double AgriculturalIncomeYr { get; set; }
        public int currencyCodeAgriIncome { get; set; }
        public double businessIncome { get; set; }
        public double businessIncomeYr { get; set; }
        public int currencyCodeBusinessIncome { get; set; }
        public double otherSourceIncome { get; set; }
        public double otherSourceIncomeYr { get; set; }
        public int currencyCodeOtherIncome { get; set; }
        public DateTime dateOfEntry { get; set; }
    }
}
