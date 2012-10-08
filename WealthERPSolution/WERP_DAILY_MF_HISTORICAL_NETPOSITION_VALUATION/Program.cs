using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WERP_DAILY_MF_HISTORICAL_NETPOSITION_VALUATION
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime dtHistoricalValuationDate = DateTime.Parse(ConfigurationSettings.AppSettings["HISTORICAL_VALUATION_DATE"].ToString());
            int isForAllAdviser = int.Parse(ConfigurationSettings.AppSettings["IS_FOR_ALL_ADVISER"].ToString());
            int adviserId = int.Parse(ConfigurationSettings.AppSettings["ADVISER_ID"].ToString());
            int iSForPreviousDate = int.Parse(ConfigurationSettings.AppSettings["IS_FOR_PREVIOUSDATE"].ToString());

            if (isForAllAdviser == 1)
            {
                ProcessMFTransactionBalance(dtHistoricalValuationDate, iSForPreviousDate);
            }
            else
            {
                ProcessMFTransactionBalance(dtHistoricalValuationDate, adviserId, iSForPreviousDate);
            }


        }

        public static void ProcessMFTransactionBalance(DateTime dtHistoricalValuationDate, int iSForPreviousDate)
        {
            MFHistoricalNetpositionProcessBo MFNetposition = new MFHistoricalNetpositionProcessBo();
            MFNetposition.CreateMFHistoricalNetposition(dtHistoricalValuationDate, iSForPreviousDate);
        }

        public static void ProcessMFTransactionBalance(DateTime dtHistoricalValuationDate, int adviserId, int iSForPreviousDate)
        {
            MFHistoricalNetpositionProcessBo MFNetposition = new MFHistoricalNetpositionProcessBo();
            MFNetposition.CreateMFHistoricalNetposition(dtHistoricalValuationDate, adviserId, iSForPreviousDate);
        }
    }
}
