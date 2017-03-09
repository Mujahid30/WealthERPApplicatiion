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

namespace WERP_DAILY_EQUITY_VALUATION
{
    public class EquityNetpositionProcessBo
    {
        AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
        List<AdvisorVo> adviserVoList = new List<AdvisorVo>();       
        MFEngineBo mfEngineBo = new MFEngineBo();
        CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
        List<int> customerList = new List<int>();
        List<CustomerPortfolioVo> customerPortfolioList = new List<CustomerPortfolioVo>();
        PortfolioBo portfolioBo = new PortfolioBo();
        List<EQPortfolioVo> eqPortfolioList = null;
        bool isValuationDateTradeDate = false;

        public void CreateEQNetpositionForAllAdviser()
        {
            int adviserId = 0;
            int customerId = 0;
            CheckForTradeDate(DateTime.Today.AddDays(-1));
            if (isValuationDateTradeDate)
            {
                adviserVoList = adviserMaintenanceBo.GetAdviserList();
                for (int i = 0; i < adviserVoList.Count; i++)
                {
                    adviserId = adviserVoList[i].advisorId;
                    int logId = 0;
                    logId = CreateAdviserEODLog("EQ", DateTime.Today.AddDays(-1), adviserId);
                    try
                    {
                        customerList = customerPortfolioBo.GetAdviserCustomerList_EQ(adviserId);

                        if (customerList != null && customerList.Count > 0)
                        {
                            for (int j = 0; j < customerList.Count; j++)
                            {
                                customerId = customerList[j];
                                try
                                {
                                    customerPortfolioList = portfolioBo.GetCustomerPortfolios(customerId);
                                    customerPortfolioBo.DeleteEquityNetPosition(customerId, DateTime.Today.AddDays(-1));
                                    if (customerPortfolioList != null)
                                    {
                                        for (int k = 0; k < customerPortfolioList.Count; k++)
                                        {
                                            eqPortfolioList = customerPortfolioBo.GetCustomerEquityPortfolio(customerId, customerPortfolioList[k].PortfolioId, DateTime.Today.AddDays(-1), string.Empty, string.Empty);
                                            if (eqPortfolioList != null)
                                            {
                                                customerPortfolioBo.AddEquityNetPosition(eqPortfolioList, 1000);

                                            }
                                        }
                                    }
                                }
                                catch (BaseApplicationException Ex)
                                {

                                    throw Ex;
                                }
                                catch (Exception Ex)
                                {
                                    BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                                    NameValueCollection FunctionInfo = new NameValueCollection();
                                    FunctionInfo.Add("Method", "EquityNetpositionProcessBo:CreateEQNetpositionForAllAdviser()");
                                    object[] objects = new object[2];
                                    objects[0] = "EQ";
                                    objects[1] = customerId;
                                    FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                                    exBase.AdditionalInformation = FunctionInfo;
                                    ExceptionManager.Publish(exBase);
                                    throw exBase;
                                }


                            }
                            UpdateAdviserEODLog("EQ", 1, logId);
                        }
                    }
                    catch (BaseApplicationException Ex)
                    {

                        throw Ex;
                    }
                    catch (Exception Ex)
                    {
                        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                        NameValueCollection FunctionInfo = new NameValueCollection();
                        FunctionInfo.Add("Method", "EquityNetpositionProcessBo:CreateEQNetpositionForAllAdviser()");
                        object[] objects = new object[2];
                        objects[0] = "EQ";
                        objects[1] = adviserId;
                        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                        exBase.AdditionalInformation = FunctionInfo;
                        ExceptionManager.Publish(exBase);
                        throw exBase;
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
                FunctionInfo.Add("Method", "EquityNetpositionProcessBo:CreateAdviserEODLog()");
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
                FunctionInfo.Add("Method", "EquityNetpositionProcessBo:UpdateAdviserEODLog()");
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



    }
}
