using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoCustomerProfiling
{
    public class CustomerExpenseVo
    {
        public int ExpenseId { set; get; }
        public int CustomerId { set; get; }
        public string FrequencyCode { set; get; }
        public double Transportation { set; get; }
        public double TransportationYr { set; get; }
        public int CurrencyCodeTransportation { set; get; }
        public double Food { set; get; }
        public double FoodYr { set; get; }
        public int CurrencyCodeFood { set; get; }
        public double Clothing { set; get; }
        public double ClothingYr { set; get; }
        public int CurrencyCodeClothing { set; get; }
        public double Home { set; get; }
        public double HomeYr { set; get; }
        public int CurrencyCodeHome { set; get; }
        public double Utilities { set; get; }
        public double UtilitiesYr { set; get; }
        public int CurrencyCodeUtilities { set; get; }
        public double SelfCare { set; get; }
        public double SelfCareYr { set; get; }
        public int CurrencyCodeSelfCare { set; get; }
        public double HealthCare { set; get; }
        public double HealthCareYr { set; get; }
        public int CurrencyCodeHealthCare { set; get; }
        public double Education { set; get; }
        public double EducationYr { set; get; }
        public int CurrencyCodeEducation { set; get; }
        public double Pets { set; get; }
        public double PetsYr { set; get; }
        public int CurrencyCodePets { set; get; }
        public double Entertainment { set; get; }
        public double EntertainmentYr { set; get; }
        public int CurrencyCodeEntertainment { set; get; }
        public double Miscellaneous { set; get; }
        public double MiscellaneousYr { set; get; }
        public int CurrencyCodeMiscellaneous { set; get; }
        public DateTime DateOfEntry { set; get; }
        public int CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public int ModifiedBy { set; get; }
        public DateTime ModifiedOn { set; get; }
    }
}
