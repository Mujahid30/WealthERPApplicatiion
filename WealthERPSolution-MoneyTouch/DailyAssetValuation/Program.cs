using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoCustomerPortfolio;
using VoUser;
using BoAdvisorProfiling;
using VoCustomerPortfolio;

using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using BoCommon;
using BoWerpAdmin;

namespace DailyAssetValuation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<AdvisorVo> adviserVoList = new List<AdvisorVo>();
            AdviserMaintenanceBo adviserMaintenanceBo = new AdviserMaintenanceBo();
            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            PortfolioBo portfolioBo = new PortfolioBo();
            List<CustomerPortfolioVo> customerPortfolioList = new List<CustomerPortfolioVo>();
            List<MFPortfolioVo> mfPortfolioList = new List<MFPortfolioVo>();
            List<EQPortfolioVo> eqPortfolioList = new List<EQPortfolioVo>();
            List<int> customerList_MF = new List<int>();
            List<int> customerList_EQ = new List<int>();
            DataSet dsMFValuationDate = new DataSet();
            DataSet dsEQValuationDate = new DataSet();
            DateTime tradeDate = new DateTime();
            int LogId = 0;
            if (DateTime.Now.TimeOfDay.Hours < 1)
                tradeDate = DateTime.Today.AddDays(-1);
            else
                tradeDate = DateTime.Today;

            DateTime MFValuationDate;
            DateTime EQValuationDate;
            try
            {
                adviserVoList = adviserMaintenanceBo.GetAdviserList();
                Console.WriteLine("Starting Valuation Process for " + adviserVoList.Count.ToString() + " Advisers");
                for (int i = 0; i < adviserVoList.Count; i++)
                {
                    dsMFValuationDate = customerPortfolioBo.GetAdviserValuationDate(adviserVoList[i].advisorId, "MF", tradeDate.Month, tradeDate.Year);
                    dsEQValuationDate = customerPortfolioBo.GetAdviserValuationDate(adviserVoList[i].advisorId, "EQ", tradeDate.Month, tradeDate.Year);
                    customerList_MF = customerPortfolioBo.GetAdviserCustomerList_MF(adviserVoList[i].advisorId);
                    customerList_EQ = customerPortfolioBo.GetAdviserCustomerList_EQ(adviserVoList[i].advisorId);
                    Console.WriteLine("Starting MF Valuation Process for Adviser " + i.ToString());

                    foreach (DataRow drMF in dsMFValuationDate.Tables[0].Rows)
                    {
                        MFValuationDate = DateTime.Parse(drMF["WTD_Date"].ToString());
                        if (drMF["STAT"].ToString() == "Pending. Changes Found")
                        {
                            customerPortfolioBo.DeleteAdviserEODLog(adviserVoList[i].advisorId, "MF", MFValuationDate, 0);
                        }
                        if (drMF["STAT"].ToString() != "Completed")
                        {
                            if (DateTime.Compare(MFValuationDate, DateTime.Today) <= 0)
                            {
                                if (customerList_MF != null && customerList_MF.Count != 0)
                                {
                                    LogId = CreateAdviserEODLog("MF", MFValuationDate, adviserVoList[i].advisorId);
                                    for (int j = 0; j < customerList_MF.Count; j++)
                                    {
                                        customerPortfolioList = portfolioBo.GetCustomerPortfolios(customerList_MF[j]);
                                        customerPortfolioBo.DeleteMutualFundNetPosition(customerList_MF[j], MFValuationDate);
                                        if (customerPortfolioList != null && customerPortfolioList.Count != 0)
                                        {
                                            for (int k = 0; k < customerPortfolioList.Count; k++)
                                            {
                                                Console.WriteLine("Starting MF Valuation Process for Customer:" + j.ToString() + " Portfolio:" + k.ToString() + " Date:" + MFValuationDate.ToShortDateString());
                                                try
                                                {
                                                    mfPortfolioList = customerPortfolioBo.GetCustomerMFPortfolio(customerList_MF[j], customerPortfolioList[k].PortfolioId, MFValuationDate, "", "","");
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Exception: " + ex.ToString());
                                                }
                                                if (mfPortfolioList != null && mfPortfolioList.Count != 0)
                                                {
                                                    try
                                                    {
                                                        customerPortfolioBo.AddMutualFundNetPosition(mfPortfolioList, adviserVoList[i].UserId);
                                                    }
                                                    catch (Exception Ex)
                                                    {
                                                        Console.WriteLine("Exception: " + Ex.ToString());
                                                    }

                                                }
                                            }
                                        }
                                    }
                                    UpdateAdviserEODLog("MF", 1, LogId);
                                }
                            }
                        }
                    }
                    #region Equity Valuation
                    Console.WriteLine("Starting EQ Valuation Process for Adviser " + i.ToString());

                    foreach (DataRow drEQ in dsEQValuationDate.Tables[0].Rows)
                    {
                        EQValuationDate = DateTime.Parse(drEQ["WTD_Date"].ToString());
                        if (drEQ["STAT"].ToString() == "Pending. Changes Found")
                        {
                            customerPortfolioBo.DeleteAdviserEODLog(adviserVoList[i].advisorId, "EQ", EQValuationDate, 0);
                        }
                        if (drEQ["STAT"].ToString() != "Completed")
                        {
                            if (DateTime.Compare(EQValuationDate, DateTime.Today) <= 0)
                            {
                                if (customerList_EQ != null && customerList_EQ.Count != 0)
                                {
                                    LogId = CreateAdviserEODLog("EQ", EQValuationDate, adviserVoList[i].advisorId);
                                    for (int j = 0; j < customerList_EQ.Count; j++)
                                    {
                                        customerPortfolioList = portfolioBo.GetCustomerPortfolios(customerList_EQ[j]);
                                        customerPortfolioBo.DeleteEquityNetPosition(customerList_EQ[j], EQValuationDate);
                                        if (customerPortfolioList != null && customerPortfolioList.Count != 0)
                                        {
                                            for (int k = 0; k < customerPortfolioList.Count; k++)
                                            {
                                                Console.WriteLine("Starting EQ Valuation Process for Customer:" + j.ToString() + " Portfolio:" + k.ToString() + " Date:" + EQValuationDate.ToShortDateString());
                                                try
                                                {
                                                    eqPortfolioList = customerPortfolioBo.GetCustomerEquityPortfolio(customerList_EQ[j], customerPortfolioList[k].PortfolioId, EQValuationDate, "");
                                                }
                                                catch (Exception Ex)
                                                {
                                                    Console.WriteLine("Exception: " + Ex.ToString());
                                                }
                                                if (mfPortfolioList != null && mfPortfolioList.Count != 0)
                                                {
                                                    try
                                                    {
                                                        customerPortfolioBo.AddEquityNetPosition(eqPortfolioList, adviserVoList[i].UserId);
                                                    }
                                                    catch (Exception Ex)
                                                    {
                                                        Console.WriteLine("Exception: " + Ex.ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    UpdateAdviserEODLog("EQ", 1, LogId);
                                }
                            }
                        }
                    }
                    #endregion Equity Valuation
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
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:CreateAdviserEODLog()");
                object[] objects = new object[2];
                objects[0] = 0;
                objects[1] = tradeDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            
        }
        private static int CreateAdviserEODLog(string p, DateTime dt, int adviserId)
        {
            int LogId = 0;

            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();
            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            try
            {
                adviserDaliyLOGVo.AdviserId = adviserId;
                adviserDaliyLOGVo.CreatedBy = 100;
                adviserDaliyLOGVo.StartTime = DateTime.Now;
                adviserDaliyLOGVo.ProcessDate = dt;
                adviserDaliyLOGVo.AssetGroup = p;
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
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:CreateAdviserEODLog()");
                object[] objects = new object[2];
                objects[0] = p;
                objects[1] = dt;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return LogId;
        }
        private static void UpdateAdviserEODLog(string group, int IsComplete, int LogId)
        {
            AdviserDailyLOGVo adviserDaliyLOGVo = new AdviserDailyLOGVo();
            CustomerPortfolioBo customerPortfolioBo = new CustomerPortfolioBo();
            try
            {
                adviserDaliyLOGVo.IsEquityCleanUpComplete = 0;
                adviserDaliyLOGVo.IsValuationComplete = IsComplete;
                adviserDaliyLOGVo.ModifiedBy = 100;
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
                FunctionInfo.Add("Method", "DailyValuation.ascx.cs:UpdateAdviserEODLog()");
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
