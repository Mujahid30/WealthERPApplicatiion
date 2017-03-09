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
        public enum ValuationLevel
        {
            AllAdviser = 1,
            Adviser = 2,
            Customer = 3,
            Account = 4
        }
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
        MFEngineBo mfEngineBo;
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();

        public void ProcessMFTransactionBalance(int commonId1,int commonId2, ValuationLevel Level)
        {
            try
             {

                switch (Level.ToString())
                {

                    case "AllAdviser":
                        {
                            mfEngineBo = new MFEngineBo();
                            List<AdvisorVo> adviserVoList = new List<AdvisorVo>();  
                            adviserVoList = adviserMaintenanceBo.GetAdviserList();
                            BoValuation.MFEngineBo.ValuationLabel valuationFor = BoValuation.MFEngineBo.ValuationLabel.Advisor;
                            for (int i = 0; i < adviserVoList.Count; i++)
                            {

                                try
                                {
                                    int logId = 0;
                                    logId = CreateAdviserEODLog("MF_Balance", DateTime.Now, adviserVoList[i].advisorId);
                                    mfEngineBo.MFBalanceCreation(commonId1, 0, valuationFor);
                                    UpdateAdviserEODLog("MF_Balance", 1, logId);

                                }
                                catch
                                {


                                }
                               
                            }
                            break;

                        }
                    case "Adviser":
                        {
                            try
                            {
                                mfEngineBo = new MFEngineBo();
                                BoValuation.MFEngineBo.ValuationLabel valuationFor = BoValuation.MFEngineBo.ValuationLabel.Advisor;
                                int logId = 0;
                                logId = CreateAdviserEODLog("MF_Balance", DateTime.Now, commonId1);
                                mfEngineBo.MFBalanceCreation(commonId1, 0, valuationFor);
                                UpdateAdviserEODLog("MF_Balance", 1, logId);
                            }
                            catch
                            {
 
                            }
                            break;
                        }
                    case "Customer":
                        {
                            try
                            {
                                mfEngineBo = new MFEngineBo();
                                mfEngineBo = new MFEngineBo();
                                BoValuation.MFEngineBo.ValuationLabel valuationFor = BoValuation.MFEngineBo.ValuationLabel.Customer;
                                mfEngineBo.MFBalanceCreation(commonId1, 0, valuationFor);
                            }
                            catch
                            {

                            }
                            break;
                        }
                    case "Account":
                        {
                            try
                            {
                                mfEngineBo = new MFEngineBo();
                                BoValuation.MFEngineBo.ValuationLabel valuationFor = BoValuation.MFEngineBo.ValuationLabel.AccountScheme;
                                mfEngineBo.MFBalanceCreation(commonId1, commonId2, valuationFor);
                            }
                            catch
                            {

                            }
                            break;
                        }
                }
            }
            catch (BaseApplicationException Ex)
            {
                //emailSMSBo.SendErrorExceptionMail(commonId, startFrom.ToString(),schemePlanCode, Ex.Message, "MFEngineBo.cs_MFBalanceCreation");
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "MFEngineBo.cs:MFBalanceCreation()");

                object[] objects = new object[3];
                objects[0] = commonId1;
                objects[1] = commonId1;
                objects[2] = Level;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

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
