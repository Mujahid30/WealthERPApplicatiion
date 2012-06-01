using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WERP_DAILY_MF_NETPOSITION_VALUATION
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProcessMFTransactionBalance();

        }

        public static void ProcessMFTransactionBalance()
        {
            MFNetpositionProcessBo MFNetposition = new MFNetpositionProcessBo();
            MFNetposition.CreateMFNetpositionForAllAdviser();
        }
    }
}
