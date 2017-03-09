using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VoCustomerPortfolio;
using BoCustomerPortfolio;


namespace EquitySpeculativeDeliveryFlagging
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime tradeDate = new DateTime();
            List<EQSpeculativeVo> eqSpeculativeVoList = new List<EQSpeculativeVo>();
            EQSpeculativeTradeBo eqSpeculativeTradeBo = new EQSpeculativeTradeBo();
            Console.WriteLine("Getting Transactions for Speculative Flagging....");
            //if (DateTime.Now.TimeOfDay.Hours < 1)
            //    tradeDate = DateTime.Today.AddDays(-1);
            //else
            //    tradeDate = DateTime.Today;
            tradeDate = new DateTime(2010, 03, 24);
            eqSpeculativeVoList = eqSpeculativeTradeBo.GetEquitySpeculativeTradeGroups(tradeDate);
            Console.WriteLine("Performing Speculative Delivery Flagging on " + eqSpeculativeVoList.Count.ToString() + "Sets");         
            eqSpeculativeTradeBo.PerformSpeculativeFlagging(eqSpeculativeVoList);
           
        }
    }
}
