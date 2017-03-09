using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace BoCommon
{
    public class DateBo
    {
        public void CalculateFromToDatesUsingPeriod(string p, out DateTime dtFrom, out DateTime dtTo)
        {
            dtFrom = new DateTime();
            dtTo = new DateTime();

            switch (p)
            {
                case "1":
                    {
                        /* If Period is Last 7 days */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddDays(-7);
                        break;
                    }
                case "2":
                    {
                        /* If Period is Last 14 days */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddDays(-14);
                        break;
                    }
                case "3":
                    {
                        /* If Period is Last 30 days */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddMonths(-1);
                        break;
                    }
                case "4":
                    {
                        /* If Period is Last 90 days */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddMonths(-3);
                        break;
                    }
                case "5":
                    {
                        /* If Period is Last 182 days */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddDays(-182);
                        break;
                    }
                case "6":
                    {
                        /* If Period is Last 365 days */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddYears(-1);
                        break;
                    }
                case "7":
                    {
                        /* If Period is Last 2 years */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddYears(-2);
                        break;
                    }
                case "8":
                    {
                        /* If Period is Last 3 years */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddYears(-3);
                        break;
                    }
                case "9":
                    {
                        /* If Period is Last 5 years */
                        dtTo = DateTime.Now;
                        dtFrom = dtTo.AddYears(-5);
                        break;
                    }
                case "10":
                    {
                        /* If Period is Previous week */
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                        {
                            dtTo = DateTime.Now.AddDays(-1);
                            dtFrom = dtTo.AddDays(-6);
                        }
                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            dtTo = DateTime.Now.AddDays(-2);
                            dtFrom = dtTo.AddDays(-6);
                        }
                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                        {
                            dtTo = DateTime.Now.AddDays(-3);
                            dtFrom = dtTo.AddDays(-6);
                        }
                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                        {
                            dtTo = DateTime.Now.AddDays(-4);
                            dtFrom = dtTo.AddDays(-6);
                        }
                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                        {
                            dtTo = DateTime.Now.AddDays(-5);
                            dtFrom = dtTo.AddDays(-6);
                        }
                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                        {
                            dtTo = DateTime.Now.AddDays(-6);
                            dtFrom = dtTo.AddDays(-6);
                        }
                        else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                        {
                            dtTo = DateTime.Now.AddDays(-7);
                            dtFrom = dtTo.AddDays(-6);
                        }

                        break;
                    }
                case "11":
                    {
                        /* If Period is Previous month */
                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;
                        if (month != 1)
                        {
                            int noOfDays = DateTime.DaysInMonth(year, (month - 1));
                            dtFrom = DateTime.Parse(1 + "/" + (month - 1) + "/" + year);
                            dtTo = DateTime.Parse(noOfDays + "/" + (month - 1) + "/" + year);
                        }
                        else
                        {
                            int noOfDays = DateTime.DaysInMonth(year-1, 12);
                            dtFrom = DateTime.Parse(1 + "/" + "12" + "/" + (year - 1));
                            dtTo = DateTime.Parse(noOfDays + "/" + "12" + "/" + (year-1));
                        }
                        break;
                    }
                case "12":
                    {
                        /* If Period is Previous half year */
                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;

                        if (month >= 1 && month <= 6)
                        {
                            /* If month belongs to the first 6 months time frame*/
                            dtFrom = DateTime.Parse("1/7/" + (year - 1));
                            dtTo = DateTime.Parse("31/12/" + (year - 1));
                        }
                        else
                        {
                            dtFrom = DateTime.Parse("1/1/" + year);
                            dtTo = DateTime.Parse("30/6/" + year);
                        }
                        break;
                    }
                case "13":
                    {
                        /* If Period is Previous fiscal year */
                        int month = DateTime.Now.Month;
                        int year = DateTime.Now.Year;

                        if (month > 3)
                        {
                            /* If month belongs to the first 6 months time frame*/
                            dtFrom = DateTime.Parse("1/4/" + (year - 1));
                            dtTo = DateTime.Parse("31/3/" + year);
                        }
                        else
                        {
                            dtFrom = DateTime.Parse("1/4/" + (year - 2));
                            dtTo = DateTime.Parse("31/3/" + (year - 1));
                        }

                        break;
                    }
                case "14":
                    {
                        /* If Period is Previous Calendar year */
                        int year = DateTime.Now.Year;

                        /* If month belongs to the first 6 months time frame*/
                        dtFrom = DateTime.Parse("1/1/" + (year - 1));
                        dtTo = DateTime.Parse("31/12/" + (year - 1));

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Format date according to a culture
        /// </summary>
        
        public static DateTime GetFormattedDate(DateTime date, string culture)
        {
            CultureInfo ci = new CultureInfo(culture);
            DateTime convertedDate = new DateTime();
            convertedDate = Convert.ToDateTime(date, ci);
            return convertedDate;
        }
        /// <summary>
        /// Format date according to default culture "en-GB"
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFormattedDate(DateTime date)
        {
            string defaultCulture = "en-GB";
            return GetFormattedDate(date, defaultCulture);

        }

        ///// <summary>
        ///// Gets the number of Years for the date range specified
        ///// </summary>
        ///// <param name="dtFrom">From Date</param>
        ///// <param name="dtTo">To Date</param>
        ///// <returns>Integer specifying the number of years</returns>
        //public int GetDateRangeNumYears(DateTime dtFrom, DateTime dtTo)
        //{
 
        //}

        /// <summary>
        /// Gets the number of Months for the date range specified
        /// </summary>
        /// <param name="dtFrom">From Date</param>
        /// <param name="dtTo">To Date</param>
        /// <returns>Integer specifying the number of years</returns>
        public float GetDateRangeNumMonths(DateTime dtFrom, DateTime dtTo)
        {
            TimeSpan tsTimeDifference = dtTo.Subtract(dtFrom);
            int Days = Int32.Parse(tsTimeDifference.Days.ToString());
            float months = (float)Days / 30;
            return months;
        }

        /// <summary>
        /// Get the last day of the previous month.
        /// </summary>
        public static DateTime GetPreviousMonthLastDate(DateTime date)
        {
            DateTime dtPreviousMonthDate = date.AddMonths(-1);
            int daysInMonth = DateTime.DaysInMonth(dtPreviousMonthDate.Year, dtPreviousMonthDate.Month);
            return new DateTime(dtPreviousMonthDate.Year, dtPreviousMonthDate.Month, daysInMonth);
        }
        

    }


}
