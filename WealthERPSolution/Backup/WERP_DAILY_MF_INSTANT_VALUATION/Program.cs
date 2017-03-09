using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace WERP_DAILY_MF_INSTANT_VALUATION
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProcessMFInstantValuation();
        }

        public static void ProcessMFInstantValuation()
        {
            MFInstantValuationProcessBo mfInstantValuationProcessBo = new MFInstantValuationProcessBo();
            mfInstantValuationProcessBo.ProcessMFAccountInstantValuation();
        }
    }
}
