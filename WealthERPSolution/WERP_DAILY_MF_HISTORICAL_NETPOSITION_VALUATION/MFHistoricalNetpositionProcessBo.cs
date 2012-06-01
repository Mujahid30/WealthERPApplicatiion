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

namespace WERP_DAILY_MF_HISTORICAL_NETPOSITION_VALUATION
{
    public class MFHistoricalNetpositionProcessBo
    {
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
        List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
        BoValuation.MFHistoricalValuationBo.ValuationLabel valuationFor = BoValuation.MFHistoricalValuationBo.ValuationLabel.Advisor;
        MFHistoricalValuationBo mfHistoricalValuationBo = new MFHistoricalValuationBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
       
        public void CreateMFHistoricalNetposition(DateTime dtHistoricalValuationDate)
        {
            adviserVoList = adviserMaintenanceBo.GetAdviserList();
            for (int i = 0; i < adviserVoList.Count; i++)
            {
                int logId = 0;
                logId = CreateAdviserEODLog("MF", dtHistoricalValuationDate, adviserVoList[i].advisorId);
                try
                {
                    mfHistoricalValuationBo.MFNetPositionCreation(adviserVoList[i].advisorId, 0, valuationFor, dtHistoricalValuationDate);                   
                    UpdateAdviserEODLog("MF", 1, logId);
                }
                catch
                {


                }

            }
        }

        public void CreateMFHistoricalNetposition(DateTime dtHistoricalValuationDate,int adviserId)
        {            
                int logId = 0;
                logId = CreateAdviserEODLog("MF", dtHistoricalValuationDate, adviserId);
                try
                {
                    mfHistoricalValuationBo.MFNetPositionCreation(adviserId, 0, valuationFor, dtHistoricalValuationDate);                   
                    UpdateAdviserEODLog("MF", 1, logId);
                }
                catch
                {


                }

           
        }

        private int CreateAdviserEODLog(string assetType, DateTime dt, int adviserId)
        {
            int LogId = 0;

            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();
            try
            {
                adviserDaliyLOGVo.AdviserId = adviserId;
                adviserDaliyLOGVo.CreatedBy = 1000;
                adviserDaliyLOGVo.StartTime = DateTime.Now;
                adviserDaliyLOGVo.ProcessDate = dt;
                adviserDaliyLOGVo.AssetGroup = assetType;
                LogId = customerPortfolioBo.CreateAdviserEODLog(adviserDaliyLOGVo);
            }
            catch (BaseApplicationException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFNetpositionCreation:CreateAdviserEODLog()");
                object[] objects = new object[2];
                objects[0] = assetType;
                objects[1] = dt;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return LogId;
        }

        protected void UpdateAdviserEODLog(string group, int IsComplete, int LogId)
        {
            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();
            try
            {
                adviserDaliyLOGVo.IsEquityCleanUpComplete = 0;
                adviserDaliyLOGVo.IsValuationComplete = IsComplete;
                adviserDaliyLOGVo.ModifiedBy = 1000;
                adviserDaliyLOGVo.EODLogId = LogId;
                adviserDaliyLOGVo.AssetGroup = group;
                adviserDaliyLOGVo.EndTime = DateTime.Now;
                customerPortfolioBo.UpdateAdviserEODLog(adviserDaliyLOGVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFNetpositionCreation:UpdateAdviserEODLog()");
                object[] objects = new object[3];
                objects[0] = group;
                objects[1] = IsComplete;
                objects[2] = LogId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
    }


}
