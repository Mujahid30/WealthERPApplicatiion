using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WERP_MF_Historical_ValuationQueue
{
    public class Program
    {
        static string isManual = ConfigurationSettings.AppSettings["IsManual"].ToString();
        static int adviserId;
        static int isCurrent;
        static DateTime valuationDate;
        static void Main(string[] args)
        {
            ProcessMFHistoricalValuationQueue();
        }

        public static void ProcessMFHistoricalValuationQueue()
        {
            MFHistoricalValuationQueue mfHistoricalValuationQueue = new MFHistoricalValuationQueue();
            if (isManual == "1")
            {
                adviserId = Convert.ToInt32(ConfigurationSettings.AppSettings["AdviserId"].ToString());
                isCurrent = Convert.ToInt32(ConfigurationSettings.AppSettings["IsCurrent"].ToString());
                valuationDate = Convert.ToDateTime(ConfigurationSettings.AppSettings["ValuationDate"].ToString());
                mfHistoricalValuationQueue.CreateMFHistoricalNetposition(valuationDate, adviserId, isCurrent);

            }
            else
            {
                mfHistoricalValuationQueue.ProcessMFAccountInstantValuation();

            }
        }
    }
}
