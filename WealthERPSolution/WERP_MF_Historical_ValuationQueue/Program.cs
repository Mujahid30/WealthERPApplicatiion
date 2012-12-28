using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WERP_MF_Historical_ValuationQueue
{
   public class Program
    {
        static void Main(string[] args)
        {
            ProcessMFHistoricalValuationQueue();
        }

        public static void ProcessMFHistoricalValuationQueue()
        {
            MFHistoricalValuationQueue mfHistoricalValuationQueue = new MFHistoricalValuationQueue();
            mfHistoricalValuationQueue.ProcessMFAccountInstantValuation();
        }
    }
}
