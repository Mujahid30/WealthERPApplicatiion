using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WERP_DAILY_MF_NETPOSITION_VALUATION
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string adviserListFlag = ConfigurationSettings.AppSettings["IS_HEAVY_DUTY_ADVISER_LIST"].ToString();
            ProcessMFTransactionBalance(bool.Parse(adviserListFlag.ToString()));

        }

        public static void ProcessMFTransactionBalance(bool adviserListFlag)
        {
            MFNetpositionProcessBo MFNetposition = new MFNetpositionProcessBo();
            MFNetposition.CreateMFNetpositionForAllAdviser(adviserListFlag);
        }
    }
}
