using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
