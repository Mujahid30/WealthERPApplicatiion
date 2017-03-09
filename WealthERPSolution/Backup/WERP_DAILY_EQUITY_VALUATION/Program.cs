using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WERP_DAILY_EQUITY_VALUATION
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessEQValuation();
        }

        public static void ProcessEQValuation()
        {
            EquityNetpositionProcessBo EQNetposition = new EquityNetpositionProcessBo();
            EQNetposition.CreateEQNetpositionForAllAdviser();
        }
    }
}
