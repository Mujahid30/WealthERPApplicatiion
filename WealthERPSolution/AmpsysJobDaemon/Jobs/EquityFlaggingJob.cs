using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VoCustomerPortfolio;
using BoCustomerPortfolio;

namespace AmpsysJobDaemon
{
    class EquityFlaggingJob:Job
    {
        public override JobStatus Start(JobParams JP, out string ErrorMsg)
        {
            DateTime tradeDate = new DateTime();
            List<EQSpeculativeVo> eqSpeculativeVoList = new List<EQSpeculativeVo>();
            EQSpeculativeTradeBo eqSpeculativeTradeBo = new EQSpeculativeTradeBo();
            if (DateTime.Now.TimeOfDay.Hours < 1)
                tradeDate = DateTime.Today.AddDays(-1);
            else
                tradeDate = DateTime.Today;
            eqSpeculativeVoList = eqSpeculativeTradeBo.GetEquitySpeculativeTradeGroups(tradeDate);
            eqSpeculativeTradeBo.PerformSpeculativeFlagging(eqSpeculativeVoList);
            ErrorMsg = "";
            return JobStatus.SuccessFull;
        }
    }
}
