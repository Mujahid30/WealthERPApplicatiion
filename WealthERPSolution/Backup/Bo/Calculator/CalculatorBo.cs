using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numeric;
using System.Collections;
using System.Diagnostics;
using System.Data;
using VoCalculator;

namespace BoCalculator
{
    public class Calculator
    {
        public double GetLoanOutstanding(string frequencyCode, double loanAmount, DateTime startDate, DateTime endDate, int paymentOption, double installmentAmount,int noOfInstallments)
        {
            double outstandingLoanAmount = 0;
            DateTime currentDate = DateTime.Today;
            TimeSpan tsDateDiff = new TimeSpan();
            int noOfRemainingInstallments = 0;
            int months = 0;
            if (paymentOption == 1)
            {
                outstandingLoanAmount = loanAmount;
            }
            else
            {
                if (startDate <= currentDate && currentDate <= endDate)
                {
                    tsDateDiff = currentDate.Subtract(startDate);
                    months = tsDateDiff.Days / 29;
                    switch (frequencyCode)
                    {
                        case "MN":
                            noOfRemainingInstallments = noOfInstallments - months;
                            break;
                        case "QT":
                            noOfRemainingInstallments = noOfInstallments - (months / 3);
                            break;

                        case "HY":
                            noOfRemainingInstallments = noOfInstallments - (months / 6);
                            break;

                        case "YR":
                            noOfRemainingInstallments = noOfInstallments - (months / 12);
                            break;
                    }
                    outstandingLoanAmount = (loanAmount/noOfInstallments)*noOfRemainingInstallments;
                }
            }
            return outstandingLoanAmount;
        }
        public DateTime GetNextPremiumDate(DateTime startDate, DateTime endDate, string frequencyCode)
        {
            DateTime nextPremiumDate = new DateTime();
            DateTime dtNow = DateTime.Now;
            int compare;
            if (startDate <= dtNow & dtNow <= endDate)
            {
                switch (frequencyCode)
                {
                    case "AM":
                        break;
                    case "DA":
                        nextPremiumDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        nextPremiumDate = nextPremiumDate.AddDays(startDate.Day - 1);
                        compare = DateTime.Compare(dtNow, nextPremiumDate);
                        if (compare > 0)
                        { // Calculate the Next Payment Date and display it
                            nextPremiumDate = dtNow.AddDays(1);
                        }

                        break;
                    case "FN":
                        nextPremiumDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        nextPremiumDate = nextPremiumDate.AddDays(startDate.Day - 1);
                        compare = DateTime.Compare(dtNow, nextPremiumDate);
                        if (compare > 0)
                        { // Calculate the Next Payment Date and display it
                            nextPremiumDate = nextPremiumDate.AddDays(15);


                        }
                        break;
                    case "HY":
                        nextPremiumDate = new DateTime(DateTime.Today.Year, startDate.Month, 1);
                        nextPremiumDate = nextPremiumDate.AddDays(startDate.Day - 1);
                        compare = DateTime.Compare(dtNow, nextPremiumDate);

                        if (compare > 0)
                        { // Calculate the Next Payment Date and display it
                            TimeSpan tsDiff = dtNow.Subtract(nextPremiumDate);
                            if ((tsDiff.Days / 30) > 6)
                                nextPremiumDate = nextPremiumDate.AddMonths(12);
                            else
                                nextPremiumDate = nextPremiumDate.AddMonths(6);

                        }
                        compare = DateTime.Compare(dtNow, nextPremiumDate);
                        if(compare>0)
                            nextPremiumDate=nextPremiumDate.AddMonths(6);
                        break;
                    case "MN":
                        nextPremiumDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        nextPremiumDate = nextPremiumDate.AddDays(startDate.Day - 1);
                        compare = DateTime.Compare(dtNow, nextPremiumDate);
                        if (compare > 0)
                        { // Calculate the Next Payment Date and display it
                            nextPremiumDate = nextPremiumDate.AddMonths(1);

                        }

                        break;
                    case "NA":

                        break;
                    case "QT":
                        nextPremiumDate = new DateTime(DateTime.Today.Year, startDate.Month, 1);
                        nextPremiumDate = nextPremiumDate.AddDays(startDate.Day - 1);
                        compare = DateTime.Compare(dtNow, nextPremiumDate);
                        if (compare < 0)
                        { // Display the Premium Payment Date
                            TimeSpan tsDiff = nextPremiumDate.Subtract(dtNow);
                            if ((tsDiff.Days / 30) < 3)
                            {
                            }
                            else if ((tsDiff.Days / 30) < 6)
                            {
                                nextPremiumDate = nextPremiumDate.AddMonths(-3);
                            }
                            else if ((tsDiff.Days / 30) < 9)
                            {
                                nextPremiumDate = nextPremiumDate.AddMonths(-6);
                            }
                            else if ((tsDiff.Days / 30) < 12)
                            {
                                nextPremiumDate = nextPremiumDate.AddMonths(-9);
                            }

                        }
                        else if (compare > 0)
                        { // Calculate the Next Payment Date and display it
                            TimeSpan tsDiff = dtNow.Subtract(nextPremiumDate);
                            if ((tsDiff.Days / 30) > 9)
                            {
                                nextPremiumDate = nextPremiumDate.AddMonths(12);
                            }
                            else if ((tsDiff.Days / 30) > 6)
                            {
                                nextPremiumDate = nextPremiumDate.AddMonths(9);
                            }
                            else if ((tsDiff.Days / 30) > 3)
                            {
                                nextPremiumDate = nextPremiumDate.AddMonths(6);
                            }
                            else
                                nextPremiumDate = nextPremiumDate.AddMonths(3);

                        }
                        compare = DateTime.Compare(dtNow, nextPremiumDate);
                        if (DateTime.Compare(dtNow, nextPremiumDate) > 0)
                            nextPremiumDate=nextPremiumDate.AddMonths(3);
                        else if(DateTime.Compare(dtNow, nextPremiumDate) < 0)
                            nextPremiumDate = nextPremiumDate.AddMonths(-3);
                        break;
                    case "WK":
                        nextPremiumDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        nextPremiumDate = nextPremiumDate.AddDays(startDate.Day - 1);
                        compare = DateTime.Compare(dtNow, nextPremiumDate);

                        if (compare > 0)
                        { // Calculate the Next Payment Date and display it
                            TimeSpan tsDiff = dtNow.Subtract(nextPremiumDate);
                            if ((decimal.Parse(tsDiff.Days.ToString()) / 7 > 4))
                                nextPremiumDate = nextPremiumDate.AddDays(7 * 5);
                            else if ((decimal.Parse(tsDiff.Days.ToString()) / 7 > 3))
                                nextPremiumDate = nextPremiumDate.AddDays(7 * 4);
                            else if ((decimal.Parse(tsDiff.Days.ToString()) / 7 > 2))
                                nextPremiumDate = nextPremiumDate.AddDays(7 * 3);
                            else if ((decimal.Parse(tsDiff.Days.ToString()) / 7 > 1))
                                nextPremiumDate = nextPremiumDate.AddDays(7 * 2);

                        }

                        break;
                    case "YR":
                        nextPremiumDate = new DateTime(DateTime.Today.Year, startDate.Month, 1);
                        nextPremiumDate = nextPremiumDate.AddDays(startDate.Day - 1);
                        compare = DateTime.Compare(dtNow, nextPremiumDate);
                        if (compare > 0)
                        { // Calculate the Next Payment Date and display it
                            nextPremiumDate = nextPremiumDate = nextPremiumDate.AddYears(1);

                        }

                        break;
                }
            }
            return nextPremiumDate;
        }
        public double GetInstallmentAmount(int paymentType,string frequencyCode, DateTime startDate, DateTime endDate, double loanAmount, double interestRate, int noOfInstallments, out double interestRatePerPeriod)
        {
            double installmentAmount = 0;
            int frequencyCount = 0;         
            

            switch (frequencyCode)
                {
                    case "MN":
                        frequencyCount = 12;
                        break;
                    case "QT":
                        frequencyCount = 4;
                        break;

                    case "HY":
                        frequencyCount = 2;
                        break;

                    case "YR":
                        frequencyCount = 1;
                        break;
                    case "DA":
                        frequencyCount = 365;
                        break;
                    case "WK":
                        frequencyCount = 52;
                        break;
                    case "FN":
                        frequencyCount = 24;
                        break;
                }
                
               
                double noOfIns = 0;
                noOfIns = frequencyCount;
                double effectiveRate = (Math.Pow((1 + (interestRate / 100) / noOfIns), noOfIns)) - 1;
                double effectiveRatePerPeriod = Math.Pow(1 + effectiveRate, 1 / noOfIns) - 1;
                switch (paymentType.ToString())
                {
                    case "1":
                        installmentAmount = loanAmount / noOfInstallments;
                        break;
                    case "2":
                        installmentAmount = Math.Abs(System.Numeric.Financial.Pmt(effectiveRatePerPeriod, noOfInstallments, loanAmount, 0, 0));

                        break;
                }
                interestRatePerPeriod = effectiveRatePerPeriod;
                return installmentAmount;
        }
        public int GetNumberOfINstallments(int tenureYears,int tenureMonths,string frequencyCode)
        {
            int numberOfInstallments = 0;
            int noOfYears = tenureYears;
            int noOfMonths = tenureMonths;
           

            //bool result=int.TryParse(txtTenture.Text.ToString(),out i);

            switch (frequencyCode)
                {
                    case "MN":
                        numberOfInstallments = (noOfYears * 12) + noOfMonths;
                        break;
                    case "QT":
                        numberOfInstallments = (noOfYears * 4) + (noOfMonths / 3);
                        break;

                    case "HY":
                        numberOfInstallments = (noOfYears * 2) + (noOfMonths / 6);
                        break;

                    case "YR":
                        numberOfInstallments = (noOfYears) + (noOfMonths / 12);
                        break;
                    case "DA":
                        numberOfInstallments = (noOfYears * 365) + noOfMonths * 30;
                        break;
                    case "WK":
                        numberOfInstallments = (noOfYears * 52) + noOfMonths * 4;
                        break;
                    case "FN":
                        numberOfInstallments = (noOfYears * 24) + noOfMonths * 2;
                        break;

                }
                
            
            return numberOfInstallments;
        }

        public double GetLumpsumRepaymentAmount(double loanAmount,double interestRate,int tenureMonths,int tenureYears,string frequencyCode)
        {
            int frequencyCount = 0;
            double lumpSumAmount = 0;          
            int numberOfInstallments = 0;
            int noOfYears = tenureYears;
            int noOfMonths = tenureMonths;         
            switch (frequencyCode)
            {
                case "MN":
                    frequencyCount = 12;
                    numberOfInstallments = (noOfYears * 12) + noOfMonths;
                    break;
                case "QT":
                    frequencyCount = 4;
                    numberOfInstallments = (noOfYears * 4) + (noOfMonths / 3);
                    break;

                case "HY":
                    frequencyCount = 2;
                    numberOfInstallments = (noOfYears * 2) + (noOfMonths / 6);
                    break;

                case "YR":
                    frequencyCount = 1;
                    numberOfInstallments = (noOfYears) + (noOfMonths / 12);
                    break;
            }
            lumpSumAmount = Math.Abs(System.Numeric.Financial.Fv((interestRate / 100) / frequencyCount, numberOfInstallments, 0, loanAmount, 0));
            return lumpSumAmount;
                
        }
        public DataTable GetRepaymentSchedule(DateTime startDate,DateTime endDate,double loanAmount, string frequencyCode, double interestRate, double installmentAmount, int noOfInstallments)
        {
            DataTable dtPaymentSchedule = new DataTable();
            DataRow dr;
            List<InstallmentVo> installmentViList = new List<InstallmentVo>();
            InstallmentVo installmentVo = new InstallmentVo();
            DateTime premiumDate = new DateTime();
            dtPaymentSchedule.Columns.Add("Period");
            dtPaymentSchedule.Columns.Add("InstallmentDate");
            dtPaymentSchedule.Columns.Add("InstallmentValue");
            dtPaymentSchedule.Columns.Add("Principal");
            dtPaymentSchedule.Columns.Add("CummulativePrincipal");
            dtPaymentSchedule.Columns.Add("Interest");
            dtPaymentSchedule.Columns.Add("CummulativeInterest");
            dtPaymentSchedule.Columns.Add("Balance");
            for (int i = 0; i <= noOfInstallments; i++)
            {
                installmentVo = new InstallmentVo();
                if (i == 0)
                {
                    installmentVo.Period = i;
                    installmentVo.InstallmentValue = 0;
                    installmentVo.Principal = 0;
                    installmentVo.CummulativePrincipal = 0;
                    installmentVo.Interest = 0;
                    installmentVo.CummulativeInterestPaid = 0;
                    installmentVo.Balance = loanAmount;
                }
                else
                {
                    installmentVo.Period = i;
                    installmentVo.InstallmentValue = installmentAmount;
                    installmentVo.Interest=installmentViList[i-1].Balance*interestRate;
                    installmentVo.CummulativeInterestPaid=installmentViList[i-1].CummulativeInterestPaid+installmentVo.Interest;
                    installmentVo.Principal=installmentVo.InstallmentValue-installmentVo.Interest;
                    installmentVo.CummulativePrincipal=installmentViList[i-1].CummulativePrincipal+installmentVo.Principal;
                    installmentVo.Balance = loanAmount - installmentVo.CummulativePrincipal;
                    if (i == 1)
                    {
                        premiumDate=startDate;
                        
                    }
                    else
                    {
                        switch (frequencyCode)
                        {
                            case "AM":
                                break;
                            case "DA":
                                premiumDate = premiumDate.AddDays(1);
                                break;
                            case "FN":
                                premiumDate = premiumDate.AddDays(15);
                                break;
                            case "HY":
                                premiumDate = premiumDate.AddMonths(6);
                                break;
                            case "MN":
                                premiumDate = premiumDate.AddMonths(1);

                                break;
                            case "NA":

                                break;
                            case "QT":
                                premiumDate = premiumDate.AddMonths(3);
                                break;
                            case "WK":
                                premiumDate = premiumDate.AddDays(7);
                                break;
                            case "YR":
                                premiumDate = premiumDate.AddYears(1);

                                break;
                        }
                    }
                    installmentVo.InstallmentDate = premiumDate;
                    
                }
                installmentViList.Add(installmentVo);
            }
            for (int j = 0; j < installmentViList.Count; j++)
            {
                dr = dtPaymentSchedule.NewRow();
                dr["Period"]= installmentViList[j].Period.ToString();
                if (installmentViList[j].InstallmentDate != DateTime.MinValue)
                    dr["InstallmentDate"] = installmentViList[j].InstallmentDate.ToShortDateString();
                else
                    dr["InstallmentDate"] = "-";
                dr["InstallmentValue"] = installmentViList[j].InstallmentValue.ToString("f2");
                dr["Principal"] = installmentViList[j].Principal.ToString("f2");
                dr["CummulativePrincipal"] = installmentViList[j].CummulativePrincipal.ToString("f2");
                dr["Interest"] = installmentViList[j].Interest.ToString("f2");
                dr["CummulativeInterest"] = installmentViList[j].CummulativeInterestPaid.ToString("f2");
                dr["Balance"] = installmentViList[j].Balance.ToString("f2");
                dtPaymentSchedule.Rows.Add(dr);
            }
            return dtPaymentSchedule;
        }
        public double PresentValue(double interestRate, double noOfPayments,double payment,double FutureValue,int payment_type)
        {
            double presentValue = 0;
            if (interestRate != 0 && noOfPayments != 0 && (payment != 0 || FutureValue != 0))
            {
                if (payment_type == 0)
                    presentValue = System.Numeric.Financial.Pv(interestRate, noOfPayments, payment, FutureValue, PaymentDue.EndOfPeriod);
                else
                    presentValue = System.Numeric.Financial.Pv(interestRate, noOfPayments, payment, FutureValue, PaymentDue.BeginningOfPeriod);
            }
            return presentValue;
        }
        public double FutureValue(double interestRate, double noOfPayments, double payment, double presentValue, int payment_type)
        {
            double futureValue = 0;
            if (interestRate != 0 && noOfPayments != 0 && (payment != 0 || presentValue != 0))
            {
                if (payment_type == 0)
                    futureValue = System.Numeric.Financial.Fv(interestRate, noOfPayments, payment, presentValue, PaymentDue.EndOfPeriod);
                else
                    futureValue = System.Numeric.Financial.Fv(interestRate, noOfPayments, payment, presentValue, PaymentDue.BeginningOfPeriod);
            }
            return futureValue;
        }
    }
}
