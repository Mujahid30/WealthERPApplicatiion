using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoUser;
using BoValuation;
using BoWerpAdmin;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace WERP_DAILY_MF_TRANSACTION_BALANCE_VALUATION
{
    public class MFTransactionBalanceProcessBo
    {
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
        List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
        BoValuation.MFEngineBo.ValuationLabel valuationFor = BoValuation.MFEngineBo.ValuationLabel.Advisor;
        MFEngineBo mfEngineBo = new MFEngineBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        public void CreateMFTransactionBalanceForAllAdviser(int isForAllAdviser, int adviserId)
        {

            if (isForAllAdviser == 1)
            {
                adviserVoList = adviserMaintenanceBo.GetAdviserList();
                for (int i = 0; i < adviserVoList.Count; i++)
                {

                    try
                    {
                        int logId = 0;
                        logId = CreateAdviserEODLog("MF_Balance", DateTime.Now, adviserVoList[i].advisorId);
                        mfEngineBo.MFBalanceCreation(adviserVoList[i].advisorId, 0, valuationFor);
                        UpdateAdviserEODLog("MF_Balance", 1, logId);

                    }
                    catch
                    {


                    }

                }

            }
            else
            {
                int logId = 0;
                logId = CreateAdviserEODLog("MF_Balance", DateTime.Now, adviserId);
                mfEngineBo.MFBalanceCreation(adviserId, 0, valuationFor);
                UpdateAdviserEODLog("MF_Balance", 1, logId);
            }


        }
        private int CreateAdviserEODLog(string assetType, DateTime dt, int adviserId)
        {
            int LogId = 0;

            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();

            adviserDaliyLOGVo.AdviserId = adviserId;
            adviserDaliyLOGVo.CreatedBy = 1000;
            adviserDaliyLOGVo.StartTime = DateTime.Now;
            adviserDaliyLOGVo.ProcessDate = dt;
            adviserDaliyLOGVo.AssetGroup = assetType;
            LogId = customerPortfolioBo.CreateAdviserEODLog(adviserDaliyLOGVo);

            return LogId;
        }


        protected void UpdateAdviserEODLog(string group, int IsComplete, int LogId)
        {
            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();

            adviserDaliyLOGVo.IsEquityCleanUpComplete = 0;
            adviserDaliyLOGVo.IsValuationComplete = IsComplete;
            adviserDaliyLOGVo.ModifiedBy = 1000;
            adviserDaliyLOGVo.EODLogId = LogId;
            adviserDaliyLOGVo.AssetGroup = group;
            adviserDaliyLOGVo.EndTime = DateTime.Now;
            customerPortfolioBo.UpdateAdviserEODLog(adviserDaliyLOGVo);


        }
    }
}
