using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using BoValuation;
using BoWerpAdmin;
using VoCustomerPortfolio;
using BoCustomerPortfolio;
using VoUser;

namespace WERP_MF_Historical_ValuationQueue
{
    public class MFHistoricalValuationQueue
    {
        MFHistoricalValuationQueueDao mfHistoricalValuationQueueDao = new MFHistoricalValuationQueueDao();
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
        List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();

        MFHistoricalValuationBo mfHistoricalValuationBo = new MFHistoricalValuationBo();
        BoValuation.MFHistoricalValuationBo.ValuationLabel valuationFor = BoValuation.MFHistoricalValuationBo.ValuationLabel.Advisor;
      
        public void ProcessMFAccountInstantValuation()
        {
            DataTable dtHistoricalValuationQueue = new DataTable();
            int flag = 0, adviserId = 0, isCurrent = 0;
            dtHistoricalValuationQueue = mfHistoricalValuationQueueDao.GetMFHistoricalValuationQueueDetails();
            DateTime valuationDate = new DateTime();
            foreach (DataRow dr in dtHistoricalValuationQueue.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(dr["A_AdviserId"].ToString()))
                    {
                        adviserId = int.Parse(dr["A_AdviserId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dr["MFVQ_ValuationDate"].ToString()))
                    {
                        valuationDate = Convert.ToDateTime(dr["MFVQ_ValuationDate"]);
                    }
                    if (!string.IsNullOrEmpty(dr["MFVQ_IsCurrentValuation"].ToString()))
                    {
                        isCurrent = Convert.ToInt16(dr["MFVQ_IsCurrentValuation"]);
                    }

                    if (adviserId == 0) //for all adviser for given Date
                    {
                        CreateMFHistoricalNetposition(valuationDate, isCurrent); 
                    }
                    else
                    {
                        CreateMFHistoricalNetposition(valuationDate, adviserId, isCurrent); //for one adviser for given Date
                    }
                  flag = 2;
                  mfHistoricalValuationQueueDao.UpdateHistoricalValuationQueueFlag(adviserId, valuationDate, flag);
                }
                catch (Exception Ex)
                {
                    flag = 0;
                    mfHistoricalValuationQueueDao.UpdateHistoricalValuationQueueFlag(adviserId, valuationDate, flag);
                }

            }

        }



        ///////////////////////////////////////////////////////////////////////////


        public void CreateMFHistoricalNetposition(DateTime dtHistoricalValuationDate, int iSForPreviousDate)
        {
            adviserVoList = adviserMaintenanceBo.GetAdviserList();
            for (int i = 0; i < adviserVoList.Count; i++)
            {
                int logId = 0;
                logId = CreateAdviserEODLog("MF", dtHistoricalValuationDate, adviserVoList[i].advisorId);
               
                    mfHistoricalValuationBo.MFNetPositionCreation(adviserVoList[i].advisorId, 0, valuationFor, dtHistoricalValuationDate, iSForPreviousDate);
                    UpdateAdviserEODLog("MF", 1, logId);
               

            }
        }

        public void CreateMFHistoricalNetposition(DateTime dtHistoricalValuationDate, int adviserId, int iSForPreviousDate)
        {
            int logId = 0;
            logId = CreateAdviserEODLog("MF", dtHistoricalValuationDate, adviserId);
           
                mfHistoricalValuationBo.MFNetPositionCreation(adviserId, 0, valuationFor, dtHistoricalValuationDate, iSForPreviousDate);
                UpdateAdviserEODLog("MF", 1, logId);
          
          
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
