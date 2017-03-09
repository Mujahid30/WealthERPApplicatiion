using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WealthERP.CustomerPortfolio
{
    public class CommonClass
    {
        public static int CalculateAge(DateTime birthDate)
        {
            // cache the current time
            DateTime now = DateTime.Today; // today is fine, don't need the timestamp from now
            // get the difference in years
            int years = now.Year - birthDate.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;

            return years;
        }
    }
}
