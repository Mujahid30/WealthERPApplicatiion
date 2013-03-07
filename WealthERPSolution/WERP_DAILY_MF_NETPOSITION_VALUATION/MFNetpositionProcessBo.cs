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
using System.Data;
using System.Data.SqlClient;

namespace WERP_DAILY_MF_NETPOSITION_VALUATION
{
    public class MFNetpositionProcessBo
    {
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
        DataTable dtAdviserList;
        BoValuation.MFEngineBo.ValuationLabel valuationFor = BoValuation.MFEngineBo.ValuationLabel.Advisor;
        MFEngineBo mfEngineBo = new MFEngineBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        bool isValuationDateTradeDate = false;        

        public void CreateMFNetpositionForAllAdviser(bool adviserListFlag)
        {
            CheckForTradeDate(DateTime.Today.AddDays(-1));
            if (isValuationDateTradeDate)
            {
                dtAdviserList = GetAllAdviserListForValuation(adviserListFlag);
                foreach (DataRow dr in dtAdviserList.Rows)
                {
                    int logId = 0;
                    logId = CreateAdviserEODLog("MF", DateTime.Today.AddDays(-1), Convert.ToInt16(dr["A_AdviserId"].ToString()));
                    try
                    {
                        mfEngineBo.MFNetPositionCreation(Convert.ToInt16(dr["A_AdviserId"].ToString()), 0, valuationFor, DateTime.Today.AddDays(-1));
                        UpdateAdviserEODLog("MF", 1, logId);
                    }
                    catch
                    {


                    }

                }
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

        protected void CheckForTradeDate(DateTime valuationDate)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ValuationDate", valuationDate);
            Params[0].DbType = DbType.DateTime;
            DataSet DS = Utils.ExecuteDataSet("SPROC_CheckForTradeDate", Params);
            if (DS.Tables[0].Rows.Count > 0)
            {
                isValuationDateTradeDate = true;
            }
        }

        protected DataTable GetAllAdviserListForValuation(bool flag)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@AdviserListFlag", flag);
            Params[0].DbType = DbType.Int16;
            DataSet DS = Utils.ExecuteDataSet("SPROC_GetAllAdviserListForValuation", Params);
            return DS.Tables[0];
 
        }

    }
}
