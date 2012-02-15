using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.FSharp;
using System.Numeric;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

using VoCustomerPortfolio;
using DaoCustomerPortfolio;
using BoCustomerPortfolio;

namespace BoCustomerPortfolio
{
    public class CustomerPortfolioBo
    {
        #region Equity Portfolio Valuation
        #region All Portfolios
        public List<EQPortfolioVo> GetCustomerEquityPortfolio(int customerId, int portfolioId, DateTime tradeDate)
        {
            List<EQPortfolioVo> eqPortfolioVoList = new List<EQPortfolioVo>();
            List<EQTransactionVo> eqTransactionVoList = new List<EQTransactionVo>();
            EQPortfolioVo eqPortfolioVo = new EQPortfolioVo();
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            EQPortfolioTransactionVo eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            float profitLoss = 0;
            float costOfSales = 0;

            float realizedSalesProceeds = 0;
            float sSalesQuantity = 0;
            float sCostOfSales = 0;
            float sRealizedSalesProceeds = 0;
            float dSalesQuantity = 0;
            float dCostOfSales = 0;
            float dRealizedSalesProceeds = 0;
            int portfolioTransactionCount = 0;
            eqPortfolioVoList = customerPortfolioDao.GetCustomerEquityPortfolio(customerId, tradeDate);
            for (int i = 0; i < eqPortfolioVoList.Count; i++)
            {
                //eqPortfolioVo = new EQPortfolioVo();
                //eqPortfolioVo = eqPortfolioVoList[i];
                eqTransactionVoList = new List<EQTransactionVo>();
                eqPortfolioVoList[i].EqPortfolioId = (i + 1);
                eqTransactionVoList = customerTransactionBo.GetEquityTransactions(eqPortfolioVoList[i].CustomerId, portfolioId, eqPortfolioVoList[i].EQCode, tradeDate,eqPortfolioVoList[i].AccountId);
                eqPortfolioVoList[i].EQPortfolioTransactionVo = ProcessEquityTransactions(eqTransactionVoList);
                profitLoss = 0;
                costOfSales = 0;
                realizedSalesProceeds = 0;
                sSalesQuantity = 0;
                sCostOfSales = 0;
                sRealizedSalesProceeds = 0;
                dSalesQuantity = 0;
                dCostOfSales = 0;
                dRealizedSalesProceeds = 0;
                portfolioTransactionCount = eqPortfolioVoList[i].EQPortfolioTransactionVo.Count;
                if (portfolioTransactionCount != 0)
                {
                    for (int j = 0; j < portfolioTransactionCount; j++)
                    {
                        profitLoss = profitLoss + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedProfitLoss;
                        costOfSales = costOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                        realizedSalesProceeds = realizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;
                        if (eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeType == "D")
                        {
                            dSalesQuantity = dSalesQuantity + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                            dCostOfSales = dCostOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                            dRealizedSalesProceeds = dRealizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;
                        }
                        else
                        {
                            sSalesQuantity = sSalesQuantity + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                            sCostOfSales = sCostOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                            sRealizedSalesProceeds = sRealizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;

                        }


                    }
                    eqPortfolioVoList[i].AveragePrice = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].AveragePrice;
                    eqPortfolioVoList[i].Quantity = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].NetHoldings;
                    eqPortfolioVoList[i].NetCost = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].NetCost;
                    eqPortfolioVoList[i].CostOfPurchase = eqPortfolioVoList[i].Quantity * eqPortfolioVoList[i].AveragePrice;
                    eqPortfolioVoList[i].RealizedPNL = profitLoss;
                    eqPortfolioVoList[i].MarketPrice = GetEquityScripClosePrice(eqPortfolioVoList[i].EQCode, tradeDate);
                    eqPortfolioVoList[i].CurrentValue = eqPortfolioVoList[i].MarketPrice * eqPortfolioVoList[i].Quantity;
                    eqPortfolioVoList[i].UnRealizedPNL = eqPortfolioVoList[i].CurrentValue - eqPortfolioVoList[i].CostOfPurchase;
                    eqPortfolioVoList[i].DeliverySalesQuantity = dSalesQuantity;
                    eqPortfolioVoList[i].DeliveryCostOfSales = dCostOfSales;
                    eqPortfolioVoList[i].DeliveryRealizedSalesProceeds = dRealizedSalesProceeds;
                    eqPortfolioVoList[i].DeliveryRealizedProfitLoss = dRealizedSalesProceeds - dCostOfSales;
                    eqPortfolioVoList[i].SpeculativeSalesQuantity = sSalesQuantity;
                    eqPortfolioVoList[i].SpeculativeCostOfSales = sCostOfSales;
                    eqPortfolioVoList[i].SpeculativeRealizedSalesProceeds = sRealizedSalesProceeds;
                    eqPortfolioVoList[i].SpeculativeRealizedProfitLoss = sRealizedSalesProceeds - sCostOfSales;
                    eqPortfolioVoList[i].ValuationDate = tradeDate;
                    eqPortfolioVoList[i].CostOfSales = costOfSales;
                    eqPortfolioVoList[i].RealizedSalesProceed = realizedSalesProceeds;

                }
            }

            return eqPortfolioVoList;
        }
        public List<EQPortfolioTransactionVo> ProcessEquityTransactions(List<EQTransactionVo> eqTransactionVoList)
        {
            List<EQPortfolioTransactionVo> eqPortfolioTransactionVoList = new List<EQPortfolioTransactionVo>();
            EQPortfolioTransactionVo eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
            int customerId = eqTransactionVoList[0].CustomerId;
            int equityCode = eqTransactionVoList[0].ScripCode;
            float speculativeAveragePrice = 0;
            DateTime currentTradeDate = new DateTime();

            for (int i = 0; i < eqTransactionVoList.Count; i++)
            {


                eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
                eqPortfolioTransactionVo.TradeDate = eqTransactionVoList[i].TradeDate;
                eqPortfolioTransactionVo.TradeSide = eqTransactionVoList[i].BuySell;
                eqPortfolioTransactionVo.TradeType = eqTransactionVoList[i].TradeType;
                if (eqPortfolioTransactionVo.TradeSide == "B")
                {
                    eqPortfolioTransactionVo.BuyQuantity = eqTransactionVoList[i].Quantity;
                    eqPortfolioTransactionVo.BuyPrice = eqTransactionVoList[i].Rate + eqTransactionVoList[i].Brokerage + eqTransactionVoList[i].ServiceTax + eqTransactionVoList[i].STT + eqTransactionVoList[i].EducationCess + eqTransactionVoList[i].OtherCharges;
                    eqPortfolioTransactionVo.SellQuantity = 0;
                    eqPortfolioTransactionVo.SellPrice = 0;
                }
                else
                {
                    eqPortfolioTransactionVo.BuyQuantity = 0;
                    eqPortfolioTransactionVo.BuyPrice = 0;
                    eqPortfolioTransactionVo.SellQuantity = eqTransactionVoList[i].Quantity;
                    eqPortfolioTransactionVo.SellPrice = eqTransactionVoList[i].Rate - eqTransactionVoList[i].Brokerage - eqTransactionVoList[i].ServiceTax - eqTransactionVoList[i].STT - eqTransactionVoList[i].EducationCess - eqTransactionVoList[i].OtherCharges;
                }
                eqPortfolioTransactionVo.CostOfAcquisition = eqPortfolioTransactionVo.BuyPrice * eqPortfolioTransactionVo.BuyQuantity;
                eqPortfolioTransactionVo.RealizedSalesValue = eqPortfolioTransactionVo.SellPrice * eqPortfolioTransactionVo.SellQuantity;

                eqPortfolioTransactionVoList.Add(eqPortfolioTransactionVo);





            }
            currentTradeDate = eqPortfolioTransactionVoList[0].TradeDate;
            speculativeAveragePrice = GetCustomerEquitySpeculativeAveragePrice(customerId, equityCode, currentTradeDate);
            for (int j = 0; j < eqPortfolioTransactionVoList.Count; j++)
            {
                //Cost Of Sales
                if (eqPortfolioTransactionVoList[j].TradeSide == "S")
                {
                    if (eqPortfolioTransactionVoList[j].TradeType == "D")
                    {
                        if (j != 0)
                        {
                            eqPortfolioTransactionVoList[j].CostOfSales = eqPortfolioTransactionVoList[j].SellQuantity * eqPortfolioTransactionVoList[j - 1].AveragePrice;
                        }
                        else
                        {
                            eqPortfolioTransactionVoList[j].CostOfSales = 0;
                        }

                    }
                    else
                    {
                        if (eqPortfolioTransactionVoList[j].TradeDate == currentTradeDate)
                        {
                            eqPortfolioTransactionVoList[j].CostOfSales = eqPortfolioTransactionVoList[j].SellQuantity * speculativeAveragePrice;
                        }
                        else
                        {
                            currentTradeDate = eqPortfolioTransactionVoList[j].TradeDate;
                            speculativeAveragePrice = GetCustomerEquitySpeculativeAveragePrice(customerId, equityCode, currentTradeDate);
                            eqPortfolioTransactionVoList[j].CostOfSales = eqPortfolioTransactionVoList[j].SellQuantity * speculativeAveragePrice;

                        }
                    }
                }
                else
                {
                    eqPortfolioTransactionVoList[j].CostOfSales = 0;
                }

                //Net Cost

                if (eqPortfolioTransactionVoList[j].TradeType == "D")
                {
                    if (eqPortfolioTransactionVoList[j].TradeSide == "B")
                    {
                        if (j != 0)
                        {
                            eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j - 1].NetCost + (eqPortfolioTransactionVoList[j].BuyQuantity * eqPortfolioTransactionVoList[j].BuyPrice);
                        }
                        else
                        {
                            eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j].BuyQuantity * eqPortfolioTransactionVoList[j].BuyPrice;
                        }
                    }
                    else
                    {
                        if (j != 0)
                        {
                            eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j - 1].NetCost - (eqPortfolioTransactionVoList[j].SellQuantity * eqPortfolioTransactionVoList[j].SellPrice);
                        }
                        else
                        {
                            eqPortfolioTransactionVoList[j].NetCost = -(eqPortfolioTransactionVoList[j].SellQuantity * eqPortfolioTransactionVoList[j].SellPrice);
                        }
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j - 1].NetCost;
                    }
                    else
                    {
                        eqPortfolioTransactionVoList[j].NetCost = 0;
                    }
                }

                //Net Holdings

                if (eqPortfolioTransactionVoList[j].TradeType == "D")
                {
                    if (eqPortfolioTransactionVoList[j].TradeSide == "B")
                    {
                        if (j != 0)
                        {
                            eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j - 1].NetHoldings + eqPortfolioTransactionVoList[j].BuyQuantity;
                        }
                        else
                        {
                            eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j].BuyQuantity;
                        }
                    }
                    else
                    {
                        if (j != 0)
                        {
                            eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j - 1].NetHoldings - eqPortfolioTransactionVoList[j].SellQuantity;
                        }
                        else
                        {
                            eqPortfolioTransactionVoList[j].NetHoldings = -eqPortfolioTransactionVoList[j].SellQuantity;
                        }
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j - 1].NetHoldings;
                    }
                    else
                    {
                        eqPortfolioTransactionVoList[j].NetHoldings = 0;
                    }
                }

                if (eqPortfolioTransactionVoList[j].NetHoldings != 0)
                    eqPortfolioTransactionVoList[j].AveragePrice = eqPortfolioTransactionVoList[j].NetCost / eqPortfolioTransactionVoList[j].NetHoldings;
                eqPortfolioTransactionVoList[j].RealizedProfitLoss = eqPortfolioTransactionVoList[j].RealizedSalesValue - eqPortfolioTransactionVoList[j].CostOfSales;

            }

            return eqPortfolioTransactionVoList;
        }

        public DataSet PopulateEQTradeYear()
        {
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            DataSet ds = null;
            try
            {
                ds = customerPortfolioDao.PopulateEQTradeYear();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:PopulateEQTradeYear()");
                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public bool UpdateAdviserEODLog(AdviserDailyLOGVo adviserDailyLOGVo)
        {
            bool bResult = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                bResult = customerPortfolioDao.UpdateAdviserEODLog(adviserDailyLOGVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:UpdateAdviserEODLog()");
                object[] objects = new object[1];

                objects[0] = adviserDailyLOGVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public int CreateAdviserEODLog(AdviserDailyLOGVo adviserDailyLOGVo)
        {
            int EODLogId = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                EODLogId = customerPortfolioDao.CreateAdviserEODLog(adviserDailyLOGVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:UpdateAdviserEODLog()");
                object[] objects = new object[1];

                objects[0] = adviserDailyLOGVo;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return EODLogId;
        }
        public List<int> GetAdviserCustomerList_MF(int adviserId)
        {
            List<int> customerList = null;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                customerList = customerPortfolioDao.GetAdviserCustomerList_MF(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetAdviserCustomerList_MF()");
                object[] objects = new object[1];

                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerList;
        }
        public List<int> GetAdviserCustomerList_EQ(int adviserId)
        {
            List<int> customerList = null;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                customerList = customerPortfolioDao.GetAdviserCustomerList_EQ(adviserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetAdviserCustomerList_EQ()");
                object[] objects = new object[1];

                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return customerList;
        }
        public DataSet PopulateEQTradeDay(int year, int month, int adviserId, string assetGroup)
        {
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            DataSet ds = null;
            try
            {
                ds = customerPortfolioDao.PopulateEQTradeDay(year, month, adviserId, assetGroup);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:PopulateEQTradeDay()");
                object[] objects = new object[3];
                objects[0] = year;
                objects[1] = month;
                objects[2] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public DataSet GetAdviserValuationDate(int adviserId, string assetGroup, int Month, int Year)
        {
            DataSet ds = null;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                ds = customerPortfolioDao.GetAdviserValuationDate(adviserId, assetGroup, Month, Year);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:GetAdviserValuationDate()");
                object[] objects = new object[4];
                objects[0] = assetGroup;
                objects[1] = adviserId;
                objects[2] = Month;
                objects[3] = Year;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public bool UpdateAdviserDailyEODLogRevaluateForTransaction(int adviserId, string assetGroup, DateTime ProcessDate)
        {
            bool bResult = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                bResult = customerPortfolioDao.UpdateAdviserDailyEODLogRevaluateForTransaction(adviserId, assetGroup, ProcessDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:UpdateAdviserDailyEODLogRevaluateForTransaction()");
                object[] objects = new object[1];

                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool DeleteAdviserEODLog(int adviserId, string assetGroup, DateTime ProcessDate, int IsValuationComplete)
        {
            bool blResult = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                blResult = customerPortfolioDao.DeleteAdviserEODLog(adviserId, assetGroup, ProcessDate, IsValuationComplete);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:DeleteAdviserEODLog()");
                object[] objects = new object[4];
                objects[0] = assetGroup;
                objects[1] = adviserId;
                objects[2] = ProcessDate;
                objects[3] = IsValuationComplete;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        public DataSet PopulateEQTradeMonth(int year)
        {
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            DataSet ds = null;
            try
            {
                ds = customerPortfolioDao.PopulateEQTradeMonth(year);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:PopulateEQTradeMonth()");
                object[] objects = new object[0];


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public float GetCustomerEquitySpeculativeAveragePrice(int customerId, int equityCode, DateTime tradeDate)
        {
            float speculativeAveragePrice = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            speculativeAveragePrice = customerPortfolioDao.GetCustomerEquitySpeculativeAveragePrice(customerId, equityCode, tradeDate);
            return speculativeAveragePrice;
        }
        #endregion All Portfolios
        #region Portfolio Specific
        public List<EQPortfolioVo> GetCustomerEquityPortfolio(int customerId, int portfolioId, DateTime tradeDate, string ScripNameFilter, string tradeAccountFilter)
        {
            List<EQPortfolioVo> eqPortfolioVoList = new List<EQPortfolioVo>();
            List<EQTransactionVo> eqTransactionVoList = null;
            EQPortfolioVo eqPortfolioVo = new EQPortfolioVo();
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            EQPortfolioTransactionVo eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();

            try
            {


                float profitLoss = 0;
                float costOfSales = 0;
                int cntCurrentValueBuy = 0;
                int cntCurrentValueSell = 0;
                float realizedSalesProceeds = 0;
                float sSalesQuantity = 0;
                float sCostOfSales = 0;
                float sRealizedSalesProceeds = 0;
                float dSalesQuantity = 0;
                float dCostOfSales = 0;
                float dRealizedSalesProceeds = 0;
                int portfolioTransactionCount = 0;

                eqPortfolioVoList = customerPortfolioDao.GetCustomerEquityPortfolio(customerId, portfolioId, tradeDate, ScripNameFilter, tradeAccountFilter);
                if (eqPortfolioVoList != null)
                {
                    for (int i = 0; i < eqPortfolioVoList.Count; i++)
                    {
                        //eqPortfolioVo = new EQPortfolioVo();
                        //eqPortfolioVo = eqPortfolioVoList[i];
                        eqTransactionVoList = new List<EQTransactionVo>();
                        eqPortfolioVoList[i].EqPortfolioId = (i + 1);
                        eqTransactionVoList = customerTransactionBo.GetEquityTransactions(eqPortfolioVoList[i].CustomerId, portfolioId, eqPortfolioVoList[i].EQCode, tradeDate,eqPortfolioVoList[i].AccountId);
                        eqPortfolioVoList[i].EQPortfolioTransactionVo = ProcessEquityTransactions(eqTransactionVoList, portfolioId);
                        profitLoss = 0;
                        costOfSales = 0;
                        realizedSalesProceeds = 0;
                        sSalesQuantity = 0;
                        sCostOfSales = 0;
                        sRealizedSalesProceeds = 0;
                        dSalesQuantity = 0;
                        dCostOfSales = 0;
                        dRealizedSalesProceeds = 0;
                        portfolioTransactionCount = eqPortfolioVoList[i].EQPortfolioTransactionVo.Count;
                        cntCurrentValueBuy = 0;
                        cntCurrentValueSell = 0;
                        double[] dlCurrentValueXIRR = new double[portfolioTransactionCount];
                        DateTime[] dtTranDateXIRR = new DateTime[portfolioTransactionCount];
                        float cntOpenTrade = 0;
                        DateTime tempBuy = new DateTime(1000, 12, 29);
                        tempBuy = DateTime.Parse(tempBuy.ToShortDateString().ToString());
                        DateTime tempSell = new DateTime(1000, 12, 29);
                        tempSell = DateTime.Parse(tempSell.ToShortDateString().ToString());
                        if (portfolioTransactionCount != 0)
                        {

                            for (int j = 0; j < portfolioTransactionCount; j++)
                            {
                                profitLoss = profitLoss + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedProfitLoss;
                                costOfSales = costOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                                realizedSalesProceeds = realizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;
                                if (eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeType == "D")
                                {
                                    dSalesQuantity = dSalesQuantity + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                                    dCostOfSales = dCostOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                                    dRealizedSalesProceeds = dRealizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;
                                }
                                else
                                {
                                    sSalesQuantity = sSalesQuantity + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                                    sCostOfSales = sCostOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                                    sRealizedSalesProceeds = sRealizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;

                                }
                                if ((eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeSide.ToString()) == "B")
                                {
                                    cntCurrentValueBuy = cntCurrentValueBuy + 1;
                                    dlCurrentValueXIRR[j] = eqPortfolioVoList[i].EQPortfolioTransactionVo[j].BuyQuantity * eqPortfolioVoList[i].EQPortfolioTransactionVo[j].BuyPrice;
                                    cntOpenTrade = eqPortfolioVoList[i].EQPortfolioTransactionVo[j].BuyQuantity + cntOpenTrade;
                                    // = (eqPortfolioVoList[i].EQPortfolioTransactionVo[j].NetCost);

                                }
                                else if ((eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeSide.ToString()) == "S")
                                {
                                    cntCurrentValueSell = cntCurrentValueSell + 1;
                                    dlCurrentValueXIRR[j] = -(eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity * eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellPrice);
                                    cntOpenTrade = cntOpenTrade - eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                                }
                                dtTranDateXIRR[j] = eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeDate;

                            }
                            if (cntCurrentValueSell == 0 && cntCurrentValueBuy >= 1)
                            {

                                double[] dlCurrentValueXIR = new double[portfolioTransactionCount + 1];
                                DateTime[] dtTranDateXIR = new DateTime[portfolioTransactionCount + 1];
                                float currentNAv = 0;
                                Array.Copy(dlCurrentValueXIRR, dlCurrentValueXIR, portfolioTransactionCount);
                                Array.Copy(dtTranDateXIRR, dtTranDateXIR, portfolioTransactionCount);
                                currentNAv = GetEquityScripClosePrice(eqPortfolioVoList[i].EQCode, tradeDate);
                                if (currentNAv == 0)
                                {
                                    eqPortfolioVoList[i].XIRR = 0;
                                }
                                else
                                {
                                    dtTranDateXIR[portfolioTransactionCount] = DateTime.Today;
                                    dlCurrentValueXIR[portfolioTransactionCount] = -(currentNAv * cntOpenTrade);
                                    eqPortfolioVoList[i].XIRR = (CalculateXIRR(dlCurrentValueXIR, dtTranDateXIR) * 100);
                                }


                            }

                            else if (cntCurrentValueBuy >= 1 && dlCurrentValueXIRR[0] >= 0 && cntCurrentValueSell >= 1)
                            {
                                if (cntOpenTrade > 0)
                                {
                                    double[] dlCurrentValueXIR = new double[portfolioTransactionCount + 1];
                                    DateTime[] dtTranDateXIR = new DateTime[portfolioTransactionCount + 1];
                                    float currentNAv = 0;
                                    Array.Copy(dlCurrentValueXIRR, dlCurrentValueXIR, portfolioTransactionCount);
                                    Array.Copy(dtTranDateXIRR, dtTranDateXIR, portfolioTransactionCount);
                                    currentNAv = GetEquityScripClosePrice(eqPortfolioVoList[i].EQCode, tradeDate);
                                    dtTranDateXIR[portfolioTransactionCount] = DateTime.Today;
                                    dlCurrentValueXIR[portfolioTransactionCount] = -(currentNAv * cntOpenTrade);
                                    eqPortfolioVoList[i].XIRR = (CalculateXIRR(dlCurrentValueXIR, dtTranDateXIR) * 100);
                                }
                                else if (cntOpenTrade < 0)
                                {
                                    eqPortfolioVoList[i].XIRR = 0;
                                }
                                else
                                {
                                    eqPortfolioVoList[i].XIRR = (CalculateXIRR(dlCurrentValueXIRR, dtTranDateXIRR) * 100);
                                }
                            }

                            else
                            {
                                eqPortfolioVoList[i].XIRR = 0;
                            }
                            eqPortfolioVoList[i].AveragePrice = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].AveragePrice;
                            eqPortfolioVoList[i].Quantity = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].NetHoldings;
                            eqPortfolioVoList[i].NetCost = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].NetCost;
                            eqPortfolioVoList[i].CostOfPurchase = eqPortfolioVoList[i].Quantity * eqPortfolioVoList[i].AveragePrice;
                            eqPortfolioVoList[i].RealizedPNL = profitLoss;
                            eqPortfolioVoList[i].MarketPrice = GetEquityScripClosePrice(eqPortfolioVoList[i].EQCode, tradeDate);
                            eqPortfolioVoList[i].CurrentValue = eqPortfolioVoList[i].MarketPrice * eqPortfolioVoList[i].Quantity;
                            eqPortfolioVoList[i].UnRealizedPNL = eqPortfolioVoList[i].CurrentValue - eqPortfolioVoList[i].CostOfPurchase;
                            eqPortfolioVoList[i].DeliverySalesQuantity = dSalesQuantity;
                            eqPortfolioVoList[i].DeliveryCostOfSales = dCostOfSales;
                            eqPortfolioVoList[i].DeliveryRealizedSalesProceeds = dRealizedSalesProceeds;
                            eqPortfolioVoList[i].DeliveryRealizedProfitLoss = dRealizedSalesProceeds - dCostOfSales;
                            eqPortfolioVoList[i].SpeculativeSalesQuantity = sSalesQuantity;
                            eqPortfolioVoList[i].SpeculativeCostOfSales = sCostOfSales;
                            eqPortfolioVoList[i].SpeculativeRealizedSalesProceeds = sRealizedSalesProceeds;
                            eqPortfolioVoList[i].SpeculativeRealizedProfitLoss = sRealizedSalesProceeds - sCostOfSales;
                            eqPortfolioVoList[i].ValuationDate = tradeDate;
                            eqPortfolioVoList[i].CostOfSales = costOfSales;
                            eqPortfolioVoList[i].RealizedSalesProceed = realizedSalesProceeds;
                            //if (cntOpenTrade == 0)
                            //{
                            //    if (eqPortfolioVoList[i].CostOfPurchase != 0)
                            //        eqPortfolioVoList[i].AbsoluteReturn = (((eqPortfolioVoList[i].RealizedSalesProceed - eqPortfolioVoList[i].CostOfPurchase) / (mfPortfolioVoList[i].CostOfPurchase)) * 100);
                            //    dateDiff = (TimeSpan)(tempSell.Subtract(tempBuy));
                            //    ageOfInvestment = dateDiff.Days;

                            //}
                            //else
                            //{
                            //    if (eqPortfolioVoList[i].CostOfPurchase != 0)
                            //        eqPortfolioVoList[i].AbsoluteReturn = (((eqPortfolioVoList[i].CurrentValue - eqPortfolioVoList[i].CostOfPurchase) / (mfPortfolioVoList[i].CostOfPurchase)) * 100);
                            //    dateDiff = (TimeSpan)(DateTime.Now.Subtract(tempBuy));
                            //    ageOfInvestment = dateDiff.Days;

                            //}
                            //eqPortfolioVoList[i].AnnualReturn = (double)((eqPortfolioVoList[i].AbsoluteReturn / ageOfInvestment) * 365);
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

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:GetCustomerEquityPortfolio()");
                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = portfolioId;
                objects[2] = tradeDate;
                objects[3] = ScripNameFilter;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqPortfolioVoList;
        }

        public List<EQPortfolioVo> GetCustomerEquityPortfolio(int customerId, int portfolioId, DateTime tradeDate, string ScripNameFilter)
        {
            List<EQPortfolioVo> eqPortfolioVoList = new List<EQPortfolioVo>();
            List<EQTransactionVo> eqTransactionVoList = null;
            EQPortfolioVo eqPortfolioVo = new EQPortfolioVo();
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            EQPortfolioTransactionVo eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();

            try
            {


                float profitLoss = 0;
                float costOfSales = 0;
                int cntCurrentValueBuy = 0;
                int cntCurrentValueSell = 0;
                float realizedSalesProceeds = 0;
                float sSalesQuantity = 0;
                float sCostOfSales = 0;
                float sRealizedSalesProceeds = 0;
                float dSalesQuantity = 0;
                float dCostOfSales = 0;
                float dRealizedSalesProceeds = 0;
                int portfolioTransactionCount = 0;

                eqPortfolioVoList = customerPortfolioDao.GetCustomerEquityPortfolio(customerId, portfolioId, tradeDate, ScripNameFilter);
                if (eqPortfolioVoList != null)
                {
                    for (int i = 0; i < eqPortfolioVoList.Count; i++)
                    {
                        //eqPortfolioVo = new EQPortfolioVo();
                        //eqPortfolioVo = eqPortfolioVoList[i];
                        eqTransactionVoList = new List<EQTransactionVo>();
                        eqPortfolioVoList[i].EqPortfolioId = (i + 1);
                        eqTransactionVoList = customerTransactionBo.GetEquityTransactions(eqPortfolioVoList[i].CustomerId, portfolioId, eqPortfolioVoList[i].EQCode, tradeDate, eqPortfolioVoList[i].AccountId);
                        eqPortfolioVoList[i].EQPortfolioTransactionVo = ProcessEquityTransactions(eqTransactionVoList, portfolioId);
                        profitLoss = 0;
                        costOfSales = 0;
                        realizedSalesProceeds = 0;
                        sSalesQuantity = 0;
                        sCostOfSales = 0;
                        sRealizedSalesProceeds = 0;
                        dSalesQuantity = 0;
                        dCostOfSales = 0;
                        dRealizedSalesProceeds = 0;
                        portfolioTransactionCount = eqPortfolioVoList[i].EQPortfolioTransactionVo.Count;
                        cntCurrentValueBuy = 0;
                        cntCurrentValueSell = 0;
                        double[] dlCurrentValueXIRR = new double[portfolioTransactionCount];
                        DateTime[] dtTranDateXIRR = new DateTime[portfolioTransactionCount];
                        float cntOpenTrade = 0;
                        DateTime tempBuy = new DateTime(1000, 12, 29);
                        tempBuy = DateTime.Parse(tempBuy.ToShortDateString().ToString());
                        DateTime tempSell = new DateTime(1000, 12, 29);
                        tempSell = DateTime.Parse(tempSell.ToShortDateString().ToString());
                        if (portfolioTransactionCount != 0)
                        {

                            for (int j = 0; j < portfolioTransactionCount; j++)
                            {
                                profitLoss = profitLoss + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedProfitLoss;
                                costOfSales = costOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                                realizedSalesProceeds = realizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;
                                if (eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeType == "D")
                                {
                                    dSalesQuantity = dSalesQuantity + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                                    dCostOfSales = dCostOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                                    dRealizedSalesProceeds = dRealizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;
                                }
                                else
                                {
                                    sSalesQuantity = sSalesQuantity + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                                    sCostOfSales = sCostOfSales + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].CostOfSales;
                                    sRealizedSalesProceeds = sRealizedSalesProceeds + eqPortfolioVoList[i].EQPortfolioTransactionVo[j].RealizedSalesValue;

                                }
                                if ((eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeSide.ToString()) == "B")
                                {
                                    cntCurrentValueBuy = cntCurrentValueBuy + 1;
                                    dlCurrentValueXIRR[j] = eqPortfolioVoList[i].EQPortfolioTransactionVo[j].BuyQuantity * eqPortfolioVoList[i].EQPortfolioTransactionVo[j].BuyPrice;
                                    cntOpenTrade = eqPortfolioVoList[i].EQPortfolioTransactionVo[j].BuyQuantity + cntOpenTrade;
                                    // = (eqPortfolioVoList[i].EQPortfolioTransactionVo[j].NetCost);

                                }
                                else if ((eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeSide.ToString()) == "S")
                                {
                                    cntCurrentValueSell = cntCurrentValueSell + 1;
                                    dlCurrentValueXIRR[j] = -(eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity * eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellPrice);
                                    cntOpenTrade = cntOpenTrade - eqPortfolioVoList[i].EQPortfolioTransactionVo[j].SellQuantity;
                                }
                                dtTranDateXIRR[j] = eqPortfolioVoList[i].EQPortfolioTransactionVo[j].TradeDate;

                            }
                            if (cntCurrentValueSell == 0 && cntCurrentValueBuy >= 1)
                            {

                                double[] dlCurrentValueXIR = new double[portfolioTransactionCount + 1];
                                DateTime[] dtTranDateXIR = new DateTime[portfolioTransactionCount + 1];
                                float currentNAv = 0;
                                Array.Copy(dlCurrentValueXIRR, dlCurrentValueXIR, portfolioTransactionCount);
                                Array.Copy(dtTranDateXIRR, dtTranDateXIR, portfolioTransactionCount);
                                currentNAv = GetEquityScripClosePrice(eqPortfolioVoList[i].EQCode, tradeDate);
                                if (currentNAv == 0)
                                {
                                    eqPortfolioVoList[i].XIRR = 0;
                                }
                                else
                                {
                                    dtTranDateXIR[portfolioTransactionCount] = DateTime.Today;
                                    dlCurrentValueXIR[portfolioTransactionCount] = -(currentNAv * cntOpenTrade);
                                    eqPortfolioVoList[i].XIRR = (CalculateXIRR(dlCurrentValueXIR, dtTranDateXIR) * 100);
                                }


                            }

                            else if (cntCurrentValueBuy >= 1 && dlCurrentValueXIRR[0] >= 0 && cntCurrentValueSell >= 1)
                            {
                                if (cntOpenTrade > 0)
                                {
                                    double[] dlCurrentValueXIR = new double[portfolioTransactionCount + 1];
                                    DateTime[] dtTranDateXIR = new DateTime[portfolioTransactionCount + 1];
                                    float currentNAv = 0;
                                    Array.Copy(dlCurrentValueXIRR, dlCurrentValueXIR, portfolioTransactionCount);
                                    Array.Copy(dtTranDateXIRR, dtTranDateXIR, portfolioTransactionCount);
                                    currentNAv = GetEquityScripClosePrice(eqPortfolioVoList[i].EQCode, tradeDate);
                                    dtTranDateXIR[portfolioTransactionCount] = DateTime.Today;
                                    dlCurrentValueXIR[portfolioTransactionCount] = -(currentNAv * cntOpenTrade);
                                    eqPortfolioVoList[i].XIRR = (CalculateXIRR(dlCurrentValueXIR, dtTranDateXIR) * 100);
                                }
                                else if (cntOpenTrade < 0)
                                {
                                    eqPortfolioVoList[i].XIRR = 0;
                                }
                                else
                                {
                                    eqPortfolioVoList[i].XIRR = (CalculateXIRR(dlCurrentValueXIRR, dtTranDateXIRR) * 100);
                                }
                            }

                            else
                            {
                                eqPortfolioVoList[i].XIRR = 0;
                            }
                            eqPortfolioVoList[i].AveragePrice = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].AveragePrice;
                            eqPortfolioVoList[i].Quantity = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].NetHoldings;
                            eqPortfolioVoList[i].NetCost = eqPortfolioVoList[i].EQPortfolioTransactionVo[portfolioTransactionCount - 1].NetCost;
                            eqPortfolioVoList[i].CostOfPurchase = eqPortfolioVoList[i].Quantity * eqPortfolioVoList[i].AveragePrice;
                            eqPortfolioVoList[i].RealizedPNL = profitLoss;
                            eqPortfolioVoList[i].MarketPrice = GetEquityScripClosePrice(eqPortfolioVoList[i].EQCode, tradeDate);
                            eqPortfolioVoList[i].CurrentValue = eqPortfolioVoList[i].MarketPrice * eqPortfolioVoList[i].Quantity;
                            eqPortfolioVoList[i].UnRealizedPNL = eqPortfolioVoList[i].CurrentValue - eqPortfolioVoList[i].CostOfPurchase;
                            eqPortfolioVoList[i].DeliverySalesQuantity = dSalesQuantity;
                            eqPortfolioVoList[i].DeliveryCostOfSales = dCostOfSales;
                            eqPortfolioVoList[i].DeliveryRealizedSalesProceeds = dRealizedSalesProceeds;
                            eqPortfolioVoList[i].DeliveryRealizedProfitLoss = dRealizedSalesProceeds - dCostOfSales;
                            eqPortfolioVoList[i].SpeculativeSalesQuantity = sSalesQuantity;
                            eqPortfolioVoList[i].SpeculativeCostOfSales = sCostOfSales;
                            eqPortfolioVoList[i].SpeculativeRealizedSalesProceeds = sRealizedSalesProceeds;
                            eqPortfolioVoList[i].SpeculativeRealizedProfitLoss = sRealizedSalesProceeds - sCostOfSales;
                            eqPortfolioVoList[i].ValuationDate = tradeDate;
                            eqPortfolioVoList[i].CostOfSales = costOfSales;
                            eqPortfolioVoList[i].RealizedSalesProceed = realizedSalesProceeds;
                            //if (cntOpenTrade == 0)
                            //{
                            //    if (eqPortfolioVoList[i].CostOfPurchase != 0)
                            //        eqPortfolioVoList[i].AbsoluteReturn = (((eqPortfolioVoList[i].RealizedSalesProceed - eqPortfolioVoList[i].CostOfPurchase) / (mfPortfolioVoList[i].CostOfPurchase)) * 100);
                            //    dateDiff = (TimeSpan)(tempSell.Subtract(tempBuy));
                            //    ageOfInvestment = dateDiff.Days;

                            //}
                            //else
                            //{
                            //    if (eqPortfolioVoList[i].CostOfPurchase != 0)
                            //        eqPortfolioVoList[i].AbsoluteReturn = (((eqPortfolioVoList[i].CurrentValue - eqPortfolioVoList[i].CostOfPurchase) / (mfPortfolioVoList[i].CostOfPurchase)) * 100);
                            //    dateDiff = (TimeSpan)(DateTime.Now.Subtract(tempBuy));
                            //    ageOfInvestment = dateDiff.Days;

                            //}
                            //eqPortfolioVoList[i].AnnualReturn = (double)((eqPortfolioVoList[i].AbsoluteReturn / ageOfInvestment) * 365);
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

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:GetCustomerEquityPortfolio()");
                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = portfolioId;
                objects[2] = tradeDate;
                objects[3] = ScripNameFilter;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqPortfolioVoList;
        }


        public List<EQPortfolioTransactionVo> ProcessEquityTransactions(List<EQTransactionVo> eqTransactionVoList, int portfolioId)
        {
            List<EQPortfolioTransactionVo> eqPortfolioTransactionVoList = new List<EQPortfolioTransactionVo>();
            EQPortfolioTransactionVo eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
            if (eqTransactionVoList != null)
            {
                if (eqTransactionVoList.Count > 0)
                {
                    int customerId = eqTransactionVoList[0].CustomerId;
                    int equityCode = eqTransactionVoList[0].ScripCode;
                    float speculativeAveragePrice = 0;
                    DateTime currentTradeDate = new DateTime();

                    for (int i = 0; i < eqTransactionVoList.Count; i++)
                    {


                        eqPortfolioTransactionVo = new EQPortfolioTransactionVo();
                        eqPortfolioTransactionVo.TradeDate = eqTransactionVoList[i].TradeDate;
                        eqPortfolioTransactionVo.TradeSide = eqTransactionVoList[i].BuySell;
                        eqPortfolioTransactionVo.TradeType = eqTransactionVoList[i].TradeType;
                        if (eqPortfolioTransactionVo.TradeSide == "B")
                        {
                            eqPortfolioTransactionVo.BuyQuantity = eqTransactionVoList[i].Quantity;
                            eqPortfolioTransactionVo.BuyPrice = eqTransactionVoList[i].Rate + eqTransactionVoList[i].Brokerage + eqTransactionVoList[i].ServiceTax + eqTransactionVoList[i].STT + eqTransactionVoList[i].EducationCess + eqTransactionVoList[i].OtherCharges;
                            eqPortfolioTransactionVo.SellQuantity = 0;
                            eqPortfolioTransactionVo.SellPrice = 0;
                        }
                        else
                        {
                            eqPortfolioTransactionVo.BuyQuantity = 0;
                            eqPortfolioTransactionVo.BuyPrice = 0;
                            eqPortfolioTransactionVo.SellQuantity = eqTransactionVoList[i].Quantity;
                            eqPortfolioTransactionVo.SellPrice = eqTransactionVoList[i].Rate - eqTransactionVoList[i].Brokerage - eqTransactionVoList[i].ServiceTax - eqTransactionVoList[i].STT - eqTransactionVoList[i].EducationCess - eqTransactionVoList[i].OtherCharges;
                        }
                        eqPortfolioTransactionVo.CostOfAcquisition = eqPortfolioTransactionVo.BuyPrice * eqPortfolioTransactionVo.BuyQuantity;
                        eqPortfolioTransactionVo.RealizedSalesValue = eqPortfolioTransactionVo.SellPrice * eqPortfolioTransactionVo.SellQuantity;

                        eqPortfolioTransactionVoList.Add(eqPortfolioTransactionVo);





                    }
                    currentTradeDate = eqPortfolioTransactionVoList[0].TradeDate;
                    speculativeAveragePrice = GetCustomerEquitySpeculativeAveragePrice(customerId, equityCode, currentTradeDate);
                    for (int j = 0; j < eqPortfolioTransactionVoList.Count; j++)
                    {
                        //Cost Of Sales
                        if (eqPortfolioTransactionVoList[j].TradeSide == "S")
                        {
                            if (eqPortfolioTransactionVoList[j].TradeType == "D")
                            {
                                if (j != 0)
                                {
                                    eqPortfolioTransactionVoList[j].CostOfSales = eqPortfolioTransactionVoList[j].SellQuantity * eqPortfolioTransactionVoList[j - 1].AveragePrice;
                                }
                                else
                                {
                                    eqPortfolioTransactionVoList[j].CostOfSales = 0;
                                }

                            }
                            else
                            {
                                if (eqPortfolioTransactionVoList[j].TradeDate == currentTradeDate)
                                {
                                    eqPortfolioTransactionVoList[j].CostOfSales = eqPortfolioTransactionVoList[j].SellQuantity * speculativeAveragePrice;
                                }
                                else
                                {
                                    currentTradeDate = eqPortfolioTransactionVoList[j].TradeDate;
                                    speculativeAveragePrice = GetCustomerEquitySpeculativeAveragePrice(customerId, portfolioId, equityCode, currentTradeDate);
                                    eqPortfolioTransactionVoList[j].CostOfSales = eqPortfolioTransactionVoList[j].SellQuantity * speculativeAveragePrice;

                                }
                            }
                        }
                        else
                        {
                            eqPortfolioTransactionVoList[j].CostOfSales = 0;
                        }

                        //Net Cost

                        if (eqPortfolioTransactionVoList[j].TradeType == "D")
                        {
                            if (eqPortfolioTransactionVoList[j].TradeSide == "B")
                            {
                                if (j != 0)
                                {
                                    eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j - 1].NetCost + (eqPortfolioTransactionVoList[j].BuyQuantity * eqPortfolioTransactionVoList[j].BuyPrice);
                                }
                                else
                                {
                                    eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j].BuyQuantity * eqPortfolioTransactionVoList[j].BuyPrice;
                                }
                            }
                            else
                            {
                                if (j != 0)
                                {
                                    eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j - 1].NetCost - (eqPortfolioTransactionVoList[j].SellQuantity * eqPortfolioTransactionVoList[j - 1].AveragePrice);
                                }
                                else
                                {
                                    eqPortfolioTransactionVoList[j].NetCost = -(eqPortfolioTransactionVoList[j].SellQuantity * eqPortfolioTransactionVoList[j].SellPrice);
                                }
                            }
                        }
                        else
                        {
                            if (j != 0)
                            {
                                eqPortfolioTransactionVoList[j].NetCost = eqPortfolioTransactionVoList[j - 1].NetCost;
                            }
                            else
                            {
                                eqPortfolioTransactionVoList[j].NetCost = 0;
                            }
                        }

                        //Net Holdings

                        if (eqPortfolioTransactionVoList[j].TradeType == "D")
                        {
                            if (eqPortfolioTransactionVoList[j].TradeSide == "B")
                            {
                                if (j != 0)
                                {
                                    eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j - 1].NetHoldings + eqPortfolioTransactionVoList[j].BuyQuantity;
                                }
                                else
                                {
                                    eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j].BuyQuantity;
                                }
                            }
                            else
                            {
                                if (j != 0)
                                {
                                    eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j - 1].NetHoldings - eqPortfolioTransactionVoList[j].SellQuantity;
                                }
                                else
                                {
                                    eqPortfolioTransactionVoList[j].NetHoldings = -eqPortfolioTransactionVoList[j].SellQuantity;
                                }
                            }
                        }
                        else
                        {
                            if (j != 0)
                            {
                                eqPortfolioTransactionVoList[j].NetHoldings = eqPortfolioTransactionVoList[j - 1].NetHoldings;
                            }
                            else
                            {
                                eqPortfolioTransactionVoList[j].NetHoldings = 0;
                            }
                        }
                        if (eqPortfolioTransactionVoList[j].NetHoldings != 0)
                        {
                            eqPortfolioTransactionVoList[j].AveragePrice = eqPortfolioTransactionVoList[j].NetCost / eqPortfolioTransactionVoList[j].NetHoldings;
                        }
                        eqPortfolioTransactionVoList[j].RealizedProfitLoss = eqPortfolioTransactionVoList[j].RealizedSalesValue - eqPortfolioTransactionVoList[j].CostOfSales;


                    }
                }
            }

            return eqPortfolioTransactionVoList;
        }
        public float GetCustomerEquitySpeculativeAveragePrice(int customerId, int portfolioId, int equityCode, DateTime tradeDate)
        {
            float speculativeAveragePrice = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            speculativeAveragePrice = customerPortfolioDao.GetCustomerEquitySpeculativeAveragePrice(customerId, portfolioId, equityCode, tradeDate);
            return speculativeAveragePrice;
        }
        #endregion Portfolio Specific

        public float GetEquityScripClosePrice(int scripCode, DateTime priceDate)
        {
            float scripClosePrice = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            scripClosePrice = customerPortfolioDao.GetEquityScripClosePrice(scripCode, priceDate);
            return scripClosePrice;
        }
        public float GetEquityScripSnapShotPrice(int scripCode, DateTime TradeDate)
        {
            float scripClosePrice = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            scripClosePrice = customerPortfolioDao.GetEquityScripSnapShotPrice(scripCode, TradeDate);
            return scripClosePrice;
        }
        public int AddEquityNetPosition(EQPortfolioVo eqPortfolioVo, int userId)
        {
            int eqNPId = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                eqNPId = customerPortfolioDao.AddEquityNetPosition(eqPortfolioVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:AddEquityNetPosition()");


                object[] objects = new object[2];
                objects[0] = eqPortfolioVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqNPId;
        }
        public void AddEquityNetPosition(List<EQPortfolioVo> eqPortfolioVoList, int userId)
        {

            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            int eqNPId = 0;
            try
            {
                for (int i = 0; i < eqPortfolioVoList.Count; i++)
                {
                    eqNPId = AddEquityNetPosition(eqPortfolioVoList[i], userId);
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

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:AddEquityNetPosition()");


                object[] objects = new object[2];
                objects[0] = eqPortfolioVoList;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }

        public DataSet PopulateEquityTradeDate(int adviserId)
        {
            DataSet ds;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {

                ds = customerPortfolioDao.PopulateEQTradeDate(adviserId);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:PopulateEquityTradeDate()");


                object[] objects = new object[1];
                objects[0] = adviserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public bool DeleteEquityNetPosition(int customerId, DateTime valuationDate)
        {
            bool bResult = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {

                customerPortfolioDao.DeleteEquityNetPosition(customerId, valuationDate);
                bResult = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:DeleteMutualFundNetPosition()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        #endregion Equity Portfolio Valuation

        #region MF Portfolio Valuation
        #region All Portfolios
        public List<MFPortfolioVo> GetCustomerMFPortfolio(int customerId, DateTime tradeDate)
        {
            List<MFPortfolioVo> mfPortfolioVoList = new List<MFPortfolioVo>();
            List<MFTransactionVo> mfTransactionVoList = new List<MFTransactionVo>();
            MFPortfolioVo mfPortfolioVo = new MFPortfolioVo();
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            MFPortfolioTransactionVo mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            double profitLoss = 0;
            double realizedSalesProceed = 0;

            double salesQuantity = 0;
            double costOfSales = 0;
            double dividendIncome = 0;
            int portfolioTransactionCount = 0;
            mfPortfolioVoList = customerPortfolioDao.GetCustomerMFPortfolio(customerId, tradeDate);
            for (int i = 0; i < mfPortfolioVoList.Count; i++)
            {
                //eqPortfolioVo = new EQPortfolioVo();
                //eqPortfolioVo = eqPortfolioVoList[i];
                mfTransactionVoList = new List<MFTransactionVo>();
                mfPortfolioVoList[i].MfPortfolioId = (i + 1);
                mfTransactionVoList = customerTransactionBo.GetMFTransactions(customerId, mfPortfolioVoList[i].AccountId, mfPortfolioVoList[i].MFCode, tradeDate);

                mfPortfolioVoList[i].MFPortfolioTransactionVoList = ProcessMFTransactions(mfTransactionVoList);
                profitLoss = 0;
                realizedSalesProceed = 0;
                salesQuantity = 0;

                costOfSales = 0;
                dividendIncome = 0;
                portfolioTransactionCount = mfPortfolioVoList[i].MFPortfolioTransactionVoList.Count;
                for (int j = 0; j < portfolioTransactionCount; j++)
                {
                    profitLoss = profitLoss + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].RealizedProfitLoss;
                    realizedSalesProceed = realizedSalesProceed + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].RealizedSalesValue;
                    salesQuantity = salesQuantity + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].SellQuantity;
                    costOfSales = costOfSales + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].CostOfSales;
                    if (mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode == "DVP")
                    {
                        dividendIncome = dividendIncome + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].RealizedProfitLoss;
                    }


                }
                mfPortfolioVoList[i].AveragePrice = mfPortfolioVoList[i].MFPortfolioTransactionVoList[portfolioTransactionCount - 1].AveragePrice;
                mfPortfolioVoList[i].Quantity = mfPortfolioVoList[i].MFPortfolioTransactionVoList[portfolioTransactionCount - 1].NetHoldings;
                mfPortfolioVoList[i].CostOfPurchase = mfPortfolioVoList[i].Quantity * mfPortfolioVoList[i].AveragePrice;
                mfPortfolioVoList[i].RealizedPNL = profitLoss;
                mfPortfolioVoList[i].CurrentNAV = GetMFSchemePlanNAV(mfPortfolioVoList[i].MFCode, tradeDate);
                mfPortfolioVoList[i].CurrentValue = mfPortfolioVoList[i].CurrentNAV * mfPortfolioVoList[i].Quantity;
                if (mfPortfolioVoList[i].CurrentValue != 0)
                    mfPortfolioVoList[i].UnRealizedPNL = mfPortfolioVoList[i].CurrentValue - mfPortfolioVoList[i].CostOfPurchase;
                else
                    mfPortfolioVoList[i].UnRealizedPNL = 0;
                mfPortfolioVoList[i].RealizedSalesProceed = realizedSalesProceed;
                mfPortfolioVoList[i].SalesQuantity = salesQuantity;
                mfPortfolioVoList[i].CostOfSales = costOfSales;
                mfPortfolioVoList[i].DividendIncome = dividendIncome;
                mfPortfolioVoList[i].ValuationDate = tradeDate;
                mfPortfolioVoList[i].NetCost = mfPortfolioVoList[i].MFPortfolioTransactionVoList[portfolioTransactionCount - 1].NetCost;
            }


            return mfPortfolioVoList;
        }
        public List<MFPortfolioTransactionVo> ProcessMFTransactions(List<MFTransactionVo> mfTransactionVoList)
        {
            List<MFPortfolioTransactionVo> mfPortfolioTransactionVoList = new List<MFPortfolioTransactionVo>();
            MFPortfolioTransactionVo mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
            int customerId = mfTransactionVoList[0].CustomerId;
            int mfCode = mfTransactionVoList[0].MFCode;



            for (int i = 0; i < mfTransactionVoList.Count; i++)
            {


                mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
                mfPortfolioTransactionVo.TransactionDate = mfTransactionVoList[i].TransactionDate;
                mfPortfolioTransactionVo.TransactionType = mfTransactionVoList[i].TransactionType;
                mfPortfolioTransactionVo.TransactionClassificationCode = mfTransactionVoList[i].TransactionClassificationCode;
                mfPortfolioTransactionVo.BuySell = mfTransactionVoList[i].BuySell;
                if (mfPortfolioTransactionVo.BuySell == "B")
                {
                    mfPortfolioTransactionVo.BuyQuantity = mfTransactionVoList[i].Units;
                    mfPortfolioTransactionVo.BuyPrice = mfTransactionVoList[i].Price;
                    mfPortfolioTransactionVo.SellQuantity = 0;
                    mfPortfolioTransactionVo.SellPrice = 0;
                }
                else
                {
                    mfPortfolioTransactionVo.BuyQuantity = 0;
                    mfPortfolioTransactionVo.BuyPrice = 0;
                    mfPortfolioTransactionVo.SellQuantity = mfTransactionVoList[i].Units;
                    mfPortfolioTransactionVo.SellPrice = mfTransactionVoList[i].Price;
                }
                mfPortfolioTransactionVo.CostOfAcquisition = mfPortfolioTransactionVo.BuyPrice * mfPortfolioTransactionVo.BuyQuantity;
                mfPortfolioTransactionVo.RealizedSalesValue = mfPortfolioTransactionVo.SellPrice * mfPortfolioTransactionVo.SellQuantity;

                mfPortfolioTransactionVoList.Add(mfPortfolioTransactionVo);





            }

            for (int j = 0; j < mfPortfolioTransactionVoList.Count; j++)
            {
                //Cost Of Sales
                if (mfPortfolioTransactionVoList[j].BuySell == "S")
                {
                    if (mfPortfolioTransactionVoList[j].TransactionClassificationCode == "PREJ")
                    {
                        mfPortfolioTransactionVoList[j].CostOfSales = mfPortfolioTransactionVoList[j].RealizedSalesValue;
                    }
                    else
                    {
                        if (j != 0)
                        {
                            mfPortfolioTransactionVoList[j].CostOfSales = Math.Round(mfPortfolioTransactionVoList[j].SellQuantity * mfPortfolioTransactionVoList[j - 1].AveragePrice, 5);
                        }
                        else
                        {
                            mfPortfolioTransactionVoList[j].CostOfSales = 0;
                        }
                    }
                }
                else
                {
                    mfPortfolioTransactionVoList[j].CostOfSales = 0;
                }

                //Net Cost
                if (mfPortfolioTransactionVoList[j].BuySell == "B")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetCost = Math.Round(mfPortfolioTransactionVoList[j - 1].NetCost + mfPortfolioTransactionVoList[j].CostOfAcquisition);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetCost = Math.Round(mfPortfolioTransactionVoList[j].CostOfAcquisition, 5);
                    }
                }
                else if (mfPortfolioTransactionVoList[j].BuySell == "S")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetCost = Math.Round(mfPortfolioTransactionVoList[j - 1].NetCost - mfPortfolioTransactionVoList[j].CostOfSales, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetCost = Math.Round(-mfPortfolioTransactionVoList[j].CostOfAcquisition, 5);
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetCost = Math.Round(mfPortfolioTransactionVoList[j - 1].NetCost, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetCost = 0;
                    }
                }


                //Net Holdings
                if (mfPortfolioTransactionVoList[j].BuySell == "B")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(mfPortfolioTransactionVoList[j - 1].NetHoldings + mfPortfolioTransactionVoList[j].BuyQuantity, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(mfPortfolioTransactionVoList[j].BuyQuantity, 5);
                    }
                }
                else if (mfPortfolioTransactionVoList[j].BuySell == "S")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(mfPortfolioTransactionVoList[j - 1].NetHoldings - mfPortfolioTransactionVoList[j].SellQuantity, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(-mfPortfolioTransactionVoList[j].SellQuantity, 5);
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(mfPortfolioTransactionVoList[j - 1].NetHoldings, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = 0;
                    }
                }


                if (mfPortfolioTransactionVoList[j].NetHoldings != 0)
                    mfPortfolioTransactionVoList[j].AveragePrice = Math.Round(mfPortfolioTransactionVoList[j].NetCost / mfPortfolioTransactionVoList[j].NetHoldings, 5);
                if (mfPortfolioTransactionVoList[j].TransactionClassificationCode == "DVP")
                    mfPortfolioTransactionVoList[j].RealizedProfitLoss = Math.Round(mfPortfolioTransactionVoList[j].SellPrice, 5);
                else
                    mfPortfolioTransactionVoList[j].RealizedProfitLoss = Math.Round(mfPortfolioTransactionVoList[j].RealizedSalesValue - mfPortfolioTransactionVoList[j].CostOfSales, 5);

            }

            return mfPortfolioTransactionVoList;
        }
        #endregion All Portfolios
        #region Portfolio Specific
        public List<MFPortfolioVo> GetCustomerMFPortfolio(int customerId, int portfolioId, DateTime tradeDate, string SchemeNameFilter, string FolioFilter, string categoryFilter)
        {
            List<MFPortfolioVo> mfPortfolioVoList = new List<MFPortfolioVo>();
            List<MFTransactionVo> mfTransactionVoList = new List<MFTransactionVo>();
            List<DividendTaggingTransactionVo> dividendTaggingTransactionVoList = new List<DividendTaggingTransactionVo>();
            List<DateTime> XIRRTransDateList = new List<DateTime>();
            List<double> XIRRTransValueList = new List<double>();
            List<DateTime> rXIRRTransDateList = new List<DateTime>();
            List<double> rXIRRTransValueList = new List<double>();
            MFPortfolioVo mfPortfolioVo = new MFPortfolioVo();
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            MFPortfolioTransactionVo mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            double actualProfitLoss = 0;
            double notionalProfitLoss = 0;
            double totalProfitLoss = 0;
            double realizedSalesProceed = 0;
            double salesQuantity = 0;
            double costOfSales = 0;
            double dividendIncome = 0;
            double dividendPayout = 0;
            double divPayoutTotal = 0;
            double divReinvestTotal = 0;
            double dividendreinvested = 0;
            double netHoldings = 0;
            double costOfAcquisition = 0;
            double acqCostExclDivReinvst = 0;
            double absReturns = 0;
            double annualReturns = 0;
            double currentValue = 0;
            double stcg = 0;
            double ltcg = 0;
            double stcgEligible = 0;
            double ltcgEligible = 0;
            int ageOfInvestment = 0;
            List<DateTime> acqDateList = new List<DateTime>();
            int portfolioTransactionCount = 0;
            int xirrTransCount = 0;
            int rxirrTransCount = 0;
            try
            {

                mfPortfolioVoList = customerPortfolioDao.GetCustomerMFPortfolio(customerId, portfolioId, tradeDate, SchemeNameFilter, FolioFilter, categoryFilter);
                if (mfPortfolioVoList != null)
                {
                    for (int i = 0; i < mfPortfolioVoList.Count; i++)
                    {

                        mfTransactionVoList = new List<MFTransactionVo>();
                        mfPortfolioVoList[i].MfPortfolioId = (i + 1);
                        mfTransactionVoList = customerTransactionBo.GetMFTransactions(customerId, portfolioId, mfPortfolioVoList[i].AccountId, mfPortfolioVoList[i].MFCode, tradeDate);
                       // dividendTaggingTransactionVoList = ProcessMFTransactionsForDivTagging(mfTransactionVoList, portfolioId);
                        mfPortfolioVoList[i].MFPortfolioTransactionVoList = ProcessMFTransactionsNew(mfTransactionVoList, portfolioId, mfPortfolioVoList[i].MFCode, tradeDate);
                        XIRRTransValueList = new List<double>();
                        XIRRTransDateList = new List<DateTime>();
                        rXIRRTransValueList = new List<double>();
                        rXIRRTransDateList = new List<DateTime>();
                        netHoldings = 0;
                        costOfAcquisition = 0;
                        actualProfitLoss = 0;
                        notionalProfitLoss = 0;
                        totalProfitLoss = 0;
                        realizedSalesProceed = 0;
                        salesQuantity = 0;
                        costOfSales = 0;
                        dividendIncome = 0;
                        dividendPayout = 0;
                        dividendreinvested = 0;
                        divPayoutTotal = 0;
                        divReinvestTotal = 0;
                        xirrTransCount = 0;
                        rxirrTransCount = 0;
                        absReturns = 0;
                        annualReturns = 0;
                        currentValue = 0;
                        stcg = 0;
                        ltcg = 0;
                        stcgEligible = 0;
                        ltcgEligible = 0;
                        acqCostExclDivReinvst = 0;
                        acqDateList = new List<DateTime>();
                        portfolioTransactionCount = mfPortfolioVoList[i].MFPortfolioTransactionVoList.Count;
                        double[] dlCurrentValueXIRR;
                        DateTime[] dtTranDateXIRR;

                        double[] dlrCurrentValueXIRR;
                        DateTime[] dtrTranDateXIRR;
                        int cntCurrentValueBuy = 0;
                        int cntCurrentValueSell = 0;
                        double cntOpenTrade = 0;
                        double cntTotalBuyQuan = 0;
                        double cntTotalSellQuan = 0;
                        DateTime tempBuy = new DateTime(1000, 12, 29);
                        tempBuy = DateTime.Parse(tempBuy.ToShortDateString().ToString());
                        DateTime tempSell = new DateTime(1000, 12, 29);
                        tempSell = DateTime.Parse(tempSell.ToShortDateString().ToString());
                        int resultDateCompare = 0;
                        TimeSpan dateDiff = new TimeSpan();
                        for (int j = 0; j < portfolioTransactionCount; j++)
                        {
                            actualProfitLoss = actualProfitLoss + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].RealizedProfitLoss;
                            notionalProfitLoss = notionalProfitLoss + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].NotionalProfitLoss;
                            totalProfitLoss = totalProfitLoss + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TotalProfitLoss;
                            if (mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode != "DVP" && mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode != "DRJ")
                            {
                                realizedSalesProceed = realizedSalesProceed + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].RealizedSalesValue;
                            }
                            salesQuantity = salesQuantity + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].SellQuantity;
                            absReturns = absReturns + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].AbsoluteReturns;
                            annualReturns = annualReturns + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].AnnualReturns;
                            currentValue = currentValue + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].CurrentValue;
                            stcg = stcg + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].STCGTax;
                            ltcg = ltcg + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].LTCGTax;
                            if (mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].Closed)
                            {
                                costOfSales = costOfSales + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].CostOfAcquisition;
                                rXIRRTransValueList.Add(mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].BuyPrice * mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].BuyQuantity);
                                rXIRRTransDateList.Add(mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].BuyDate);
                                rxirrTransCount++;
                                rXIRRTransValueList.Add(-(mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].SellPrice * mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].SellQuantity));
                                rXIRRTransDateList.Add(mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].SellDate);
                                rxirrTransCount++;
                            }
                            else
                            {
                                stcgEligible = stcgEligible + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].STCGTax;
                                ltcgEligible = ltcgEligible + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].LTCGTax;
                                netHoldings = netHoldings + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].BuyQuantity;
                                costOfAcquisition = costOfAcquisition + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].CostOfAcquisition;

                                if (mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode != "DVR" && mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode != "RRJ")
                                {
                                    acqCostExclDivReinvst = acqCostExclDivReinvst + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].CostOfAcquisition;
                                }
                                if (mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].BuyQuantity != 0)
                                {
                                    acqDateList.Add(mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].BuyDate);
                                }
                            }
                            if (mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode == "DVP" || mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode == "DRJ")
                            {
                                dividendPayout = dividendPayout + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].RealizedSalesValue;

                            }
                            if (mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode == "DVR" || mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].TransactionClassificationCode == "RRJ")
                            {
                                dividendreinvested = dividendreinvested + mfPortfolioVoList[i].MFPortfolioTransactionVoList[j].CostOfAcquisition;
                            }


                        }
                        dividendIncome = dividendPayout + dividendreinvested;
                        for (int k = 0; k < mfTransactionVoList.Count; k++)
                        {
                            if (mfTransactionVoList[k].TransactionClassificationCode == "BCI" || mfTransactionVoList[k].TransactionClassificationCode == "BCO")
                            {
                                mfPortfolioVoList[i].ContainsFolioTransfer = true;
                            }
                            if ((mfTransactionVoList[k].BuySell.ToString()) == "B" && mfTransactionVoList[k].TransactionClassificationCode != "DVR" && mfTransactionVoList[k].TransactionClassificationCode != "BNS" && mfTransactionVoList[k].TransactionClassificationCode != "BNR" && mfTransactionVoList[k].TransactionClassificationCode != "RRJ")
                            {
                                cntCurrentValueBuy = cntCurrentValueBuy + 1;
                                XIRRTransValueList.Add((mfTransactionVoList[k].Price * mfTransactionVoList[k].Units));
                                XIRRTransDateList.Add(mfTransactionVoList[k].TransactionDate);
                                cntTotalBuyQuan = (((double)cntTotalBuyQuan + mfTransactionVoList[k].Units));
                                resultDateCompare = DateTime.Compare(tempBuy, mfTransactionVoList[k].TransactionDate);
                                if (resultDateCompare < 0)
                                {
                                    tempBuy = mfTransactionVoList[k].TransactionDate;
                                }
                                xirrTransCount++;
                            }
                            else if ((mfTransactionVoList[k].BuySell.ToString()) == "S")
                            {
                                cntCurrentValueSell = cntCurrentValueSell + 1;
                                XIRRTransValueList.Add(-(mfTransactionVoList[k].Price * mfTransactionVoList[k].Units));
                                XIRRTransDateList.Add(mfTransactionVoList[k].TransactionDate);
                                cntTotalSellQuan = (((double)cntTotalSellQuan + (mfTransactionVoList[k].Units)));
                                resultDateCompare = DateTime.Compare(tempSell, mfTransactionVoList[k].TransactionDate);
                                if (resultDateCompare < 0)
                                {
                                    tempSell = mfTransactionVoList[k].TransactionDate;
                                }
                                xirrTransCount++;
                            }
                            else if (mfTransactionVoList[k].TransactionClassificationCode == "DVP" || mfTransactionVoList[k].TransactionClassificationCode == "DRJ")
                            {
                                cntCurrentValueSell = cntCurrentValueSell + 1;
                                XIRRTransValueList.Add(-(mfTransactionVoList[k].Amount));
                                XIRRTransDateList.Add(mfTransactionVoList[k].TransactionDate);
                                xirrTransCount++;
                            }
                        }
                        dlCurrentValueXIRR = new double[xirrTransCount];
                        dtTranDateXIRR = new DateTime[xirrTransCount];
                        for (int l = 0; l < xirrTransCount; l++)
                        {
                            dlCurrentValueXIRR[l] = XIRRTransValueList[l];
                            dtTranDateXIRR[l] = XIRRTransDateList[l];
                        }


                        //   
                        cntOpenTrade = (double)(Convert.ToDecimal(cntTotalBuyQuan) - Convert.ToDecimal(cntTotalSellQuan));
                        if (cntCurrentValueSell == 0 && cntCurrentValueBuy >= 1)
                        {
                            double[] dlCurrentValueXIR = new double[xirrTransCount + 1];
                            DateTime[] dtTranDateXIR = new DateTime[xirrTransCount + 1];
                            float currentNAv = 0;
                            Array.Copy(dlCurrentValueXIRR, dlCurrentValueXIR, xirrTransCount);
                            Array.Copy(dtTranDateXIRR, dtTranDateXIR, xirrTransCount);
                            currentNAv = GetMFSchemePlanNAV(mfPortfolioVoList[i].MFCode, tradeDate);
                            dtTranDateXIR[xirrTransCount] = DateTime.Today;
                            if (currentNAv == 0)
                            {
                                mfPortfolioVoList[i].XIRR = 0;
                            }
                            else
                            {
                                dlCurrentValueXIR[xirrTransCount] = -(currentNAv * netHoldings);
                                mfPortfolioVoList[i].XIRR = Math.Round((CalculateXIRR(dlCurrentValueXIR, dtTranDateXIR) * 100), 5);
                            }
                        }
                        else if (cntCurrentValueBuy >= 1 && cntCurrentValueSell >= 1 && dlCurrentValueXIRR[0] >= 0)
                        {
                            if (netHoldings > 0)
                            {
                                double[] dlCurrentValueXIR = new double[xirrTransCount + 1];
                                DateTime[] dtTranDateXIR = new DateTime[xirrTransCount + 1];
                                float currentNAv = 0;
                                Array.Copy(dlCurrentValueXIRR, dlCurrentValueXIR, xirrTransCount);
                                Array.Copy(dtTranDateXIRR, dtTranDateXIR, xirrTransCount);
                                currentNAv = GetMFSchemePlanNAV(mfPortfolioVoList[i].MFCode, tradeDate);
                                dtTranDateXIR[xirrTransCount] = DateTime.Today;
                                dlCurrentValueXIR[xirrTransCount] = -Math.Round((currentNAv * netHoldings), 5);
                                mfPortfolioVoList[i].XIRR = Math.Round((CalculateXIRR(dlCurrentValueXIR, dtTranDateXIR) * 100), 5);
                            }
                            else
                            {
                                mfPortfolioVoList[i].XIRR = Math.Round((CalculateXIRR(dlCurrentValueXIRR, dtTranDateXIRR) * 100), 5);
                            }
                        }
                        else
                        {
                            mfPortfolioVoList[i].XIRR = 0;
                        }
                        //Realized XIRR Calculation
                        dlrCurrentValueXIRR = new double[rxirrTransCount];
                        dtrTranDateXIRR = new DateTime[rxirrTransCount];
                        for (int m = 0; m < rxirrTransCount; m++)
                        {
                            dlrCurrentValueXIRR[m] = rXIRRTransValueList[m];
                            dtrTranDateXIRR[m] = rXIRRTransDateList[m];
                        }
                        if (rxirrTransCount % 2 == 0)
                        {
                            mfPortfolioVoList[i].RealizedXIRR = Math.Round((CalculateXIRR(dlrCurrentValueXIRR, dtrTranDateXIRR) * 100), 5);
                        }

                        if (netHoldings != 0)
                            mfPortfolioVoList[i].AveragePrice = Math.Round(costOfAcquisition / netHoldings, 5);
                        else
                            mfPortfolioVoList[i].AveragePrice = 0;
                        mfPortfolioVoList[i].Quantity = Math.Round(netHoldings, 5);
                        mfPortfolioVoList[i].CostOfPurchase = Math.Round(costOfAcquisition, 5);
                        mfPortfolioVoList[i].AcqCostExclDivReinvst = Math.Round(acqCostExclDivReinvst, 5);
                        mfPortfolioVoList[i].CurrentNAV = GetMFSchemePlanNAV(mfPortfolioVoList[i].MFCode, tradeDate);
                        //Formula Chnaged As per Vivek & KM Requirement
                        mfPortfolioVoList[i].RealizedPNL = Math.Round(realizedSalesProceed - costOfSales, 5) + Math.Round(dividendPayout, 5);

                        mfPortfolioVoList[i].CurrentValue = Math.Round(currentValue, 5);
                        if (notionalProfitLoss != 0)
                        {
                            //MJ Request Change in 5.5 Release
                            //mfPortfolioVoList[i].UnRealizedPNL = mfPortfolioVoList[i].CurrentValue - mfPortfolioVoList[i].AcqCostExclDivReinvst;
                            mfPortfolioVoList[i].UnRealizedPNL = Math.Round(currentValue - acqCostExclDivReinvst, 5);
                        }
                        else
                            mfPortfolioVoList[i].UnRealizedPNL = 0;
                        //Formula Chnaged As per Vivek & KM Requirement
                        //mfPortfolioVoList[i].TotalPNL = Math.Round(realizedSalesProceed - costOfSales + currentValue - acqCostExclDivReinvst + dividendIncome, 5);
                        //mfPortfolioVoList[i].TotalPNL = Math.Round((realizedSalesProceed - costOfSales + dividendPayout) + mfPortfolioVoList[i].UnRealizedPNL + dividendreinvested, 5);
                        //MJ Request Change-RE-5.5
                        mfPortfolioVoList[i].TotalPNL = Math.Round((mfPortfolioVoList[i].RealizedPNL + mfPortfolioVoList[i].UnRealizedPNL+dividendPayout), 5);


                        mfPortfolioVoList[i].RealizedSalesProceed = Math.Round(realizedSalesProceed, 5);
                        mfPortfolioVoList[i].SalesQuantity = Math.Round(salesQuantity, 5);
                        mfPortfolioVoList[i].CostOfSales = Math.Round(costOfSales, 5);
                        mfPortfolioVoList[i].DividendIncome = Math.Round(dividendIncome, 5);
                        mfPortfolioVoList[i].ValuationDate = tradeDate;
                        if (mfPortfolioVoList[i].CostOfPurchase != 0)
                            mfPortfolioVoList[i].AbsoluteReturn = Math.Round((mfPortfolioVoList[i].TotalPNL * 100) / mfPortfolioVoList[i].CostOfPurchase, 5);
                        mfPortfolioVoList[i].AnnualReturn = Math.Round(annualReturns, 5);
                        mfPortfolioVoList[i].DividendPayout = Math.Round(dividendPayout, 5);
                        mfPortfolioVoList[i].DividendReinvested = Math.Round(dividendreinvested, 5);
                        mfPortfolioVoList[i].STCG = stcg;
                        mfPortfolioVoList[i].LTCG = ltcg;
                        mfPortfolioVoList[i].STCGEligible = stcgEligible;
                        mfPortfolioVoList[i].LTCGEligible = ltcgEligible;
                        if (acqDateList.Count != 0)
                        {
                            acqDateList.Sort();
                            mfPortfolioVoList[i].DateOfAcq = acqDateList[0];
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

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:GetCustomerMFPortfolio(int customerId, int portfolioId, DateTime tradeDate, string SchemeNameFilter, string FolioFilter)");

                object[] objects = new object[5];
                objects[0] = customerId;
                objects[1] = portfolioId;
                objects[2] = tradeDate;
                objects[3] = SchemeNameFilter;
                objects[4] = FolioFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return mfPortfolioVoList;
        }
        public List<DividendTaggingPortfolioVo> GetCustomerMFPortfolioDivTagging(int customerId, int portfolioId, DateTime tradeDate, string SchemeNameFilter, string FolioFilter, string categoryFilter)
        {
           
            List<MFTransactionVo> mfTransactionVoList = new List<MFTransactionVo>();
            List<DividendTaggingTransactionVo> dividendTaggingTransactionVoList = new List<DividendTaggingTransactionVo>();
            List<DividendTaggingPortfolioVo> mfDivTaggingPortfolioVoList = new List<DividendTaggingPortfolioVo>();
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
           
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            CustomerTransactionBo customerTransactionBo = new CustomerTransactionBo();
            
            try
            {

                mfDivTaggingPortfolioVoList = customerPortfolioDao.GetCustomerMFPortfolioDivTagging(customerId, portfolioId, tradeDate, SchemeNameFilter, FolioFilter, categoryFilter);
                if (mfDivTaggingPortfolioVoList != null)
                {
                    for (int i = 0; i < mfDivTaggingPortfolioVoList.Count; i++)
                    {

                        mfTransactionVoList = new List<MFTransactionVo>();
                        mfDivTaggingPortfolioVoList[i].MfPortfolioId = (i + 1);
                        mfTransactionVoList = customerTransactionBo.GetMFTransactions(customerId, portfolioId, mfDivTaggingPortfolioVoList[i].AccountId, mfDivTaggingPortfolioVoList[i].MFCode, tradeDate);
                        mfDivTaggingPortfolioVoList[i] = ProcessMFTransactionsForDivTagging(mfTransactionVoList, mfDivTaggingPortfolioVoList[i], portfolioId,tradeDate);
                        
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

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:GetCustomerMFPortfolio(int customerId, int portfolioId, DateTime tradeDate, string SchemeNameFilter, string FolioFilter)");

                object[] objects = new object[5];
                objects[0] = customerId;
                objects[1] = portfolioId;
                objects[2] = tradeDate;
                objects[3] = SchemeNameFilter;
                objects[4] = FolioFilter;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return mfDivTaggingPortfolioVoList;
        }
        public double CalculateXIRR(System.Collections.Generic.IEnumerable<double> values, System.Collections.Generic.IEnumerable<DateTime> date)
        {

            double result = 0;
            try
            {
                result = System.Numeric.Financial.XIrr(values, date);
                //This 'if' loop is a temporary fix for the error where calculation is done for XIRR instead of average
                if (Convert.ToInt64(result).ToString().Length > 3 || result.ToString().Contains("E") || result.ToString().Contains("e"))
                {
                    result = 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                string e = ex.ToString();
                return result;
            }

        }

        public double CalculatePortfolioXIRR(System.Collections.Generic.IEnumerable<double> values, System.Collections.Generic.IEnumerable<DateTime> date)
        {

            double result = 0;
            try
            {
                result = System.Numeric.Financial.XIrr(values, date);
                //This 'if' loop is a temporary fix for the error where calculation is done for XIRR instead of average
                if (result.ToString().Contains("E") || result.ToString().Contains("e"))
                {
                    result = 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                string e = ex.ToString();
                return result;
            }

        }
        public List<MFPortfolioTransactionVo> ProcessMFTransactions(List<MFTransactionVo> mfTransactionVoList, int portfolioId)
        {
            List<MFPortfolioTransactionVo> mfPortfolioTransactionVoList = new List<MFPortfolioTransactionVo>();
            MFPortfolioTransactionVo mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
            int customerId = mfTransactionVoList[0].CustomerId;
            int mfCode = mfTransactionVoList[0].MFCode;



            for (int i = 0; i < mfTransactionVoList.Count; i++)
            {


                mfPortfolioTransactionVo = new MFPortfolioTransactionVo();
                mfPortfolioTransactionVo.TransactionDate = mfTransactionVoList[i].TransactionDate;
                mfPortfolioTransactionVo.TransactionType = mfTransactionVoList[i].TransactionType;
                mfPortfolioTransactionVo.TransactionClassificationCode = mfTransactionVoList[i].TransactionClassificationCode;
                mfPortfolioTransactionVo.BuySell = mfTransactionVoList[i].BuySell;
                if (mfPortfolioTransactionVo.BuySell == "B")
                {
                    mfPortfolioTransactionVo.BuyQuantity = mfTransactionVoList[i].Units;
                    mfPortfolioTransactionVo.BuyPrice = mfTransactionVoList[i].Price;
                    mfPortfolioTransactionVo.SellQuantity = 0;
                    mfPortfolioTransactionVo.SellPrice = 0;
                }
                else if (mfPortfolioTransactionVo.BuySell == "S")
                {
                    mfPortfolioTransactionVo.BuyQuantity = 0;
                    mfPortfolioTransactionVo.BuyPrice = 0;
                    mfPortfolioTransactionVo.SellQuantity = mfTransactionVoList[i].Units;
                    mfPortfolioTransactionVo.SellPrice = mfTransactionVoList[i].Price;
                }
                else if (mfPortfolioTransactionVo.TransactionClassificationCode == "DVP" || mfPortfolioTransactionVo.TransactionClassificationCode == "DRJ")
                {
                    mfPortfolioTransactionVo.BuyQuantity = 0;
                    mfPortfolioTransactionVo.BuyPrice = 0;
                    mfPortfolioTransactionVo.SellQuantity = 0;
                    mfPortfolioTransactionVo.SellPrice = mfTransactionVoList[i].Amount;
                }
                mfPortfolioTransactionVo.CostOfAcquisition = Math.Round(mfPortfolioTransactionVo.BuyPrice * mfPortfolioTransactionVo.BuyQuantity, 5);
                mfPortfolioTransactionVo.RealizedSalesValue = Math.Round(mfPortfolioTransactionVo.SellPrice * mfPortfolioTransactionVo.SellQuantity, 5);

                mfPortfolioTransactionVoList.Add(mfPortfolioTransactionVo);





            }

            for (int j = 0; j < mfPortfolioTransactionVoList.Count; j++)
            {
                //Cost Of Sales
                if (mfPortfolioTransactionVoList[j].BuySell == "S")
                {

                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].CostOfSales = Math.Round(mfPortfolioTransactionVoList[j].SellQuantity * mfPortfolioTransactionVoList[j - 1].AveragePrice, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].CostOfSales = 0;
                    }

                }
                else
                {
                    mfPortfolioTransactionVoList[j].CostOfSales = 0;
                }

                //Net Cost
                if (mfPortfolioTransactionVoList[j].BuySell == "B" && mfPortfolioTransactionVoList[j].TransactionClassificationCode != "DVR" && mfPortfolioTransactionVoList[j].TransactionClassificationCode != "BNS" && mfPortfolioTransactionVoList[j].TransactionClassificationCode != "BNR" && mfPortfolioTransactionVoList[j].TransactionClassificationCode != "RRJ")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetCost = Math.Round(mfPortfolioTransactionVoList[j - 1].NetCost + mfPortfolioTransactionVoList[j].CostOfAcquisition, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetCost = mfPortfolioTransactionVoList[j].CostOfAcquisition;
                    }
                }
                else if (mfPortfolioTransactionVoList[j].BuySell == "S")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetCost = Math.Round(mfPortfolioTransactionVoList[j - 1].NetCost - mfPortfolioTransactionVoList[j].CostOfSales, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetCost = -mfPortfolioTransactionVoList[j].CostOfAcquisition;
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetCost = mfPortfolioTransactionVoList[j - 1].NetCost;
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetCost = 0;
                    }
                }


                //Net Holdings
                if (mfPortfolioTransactionVoList[j].BuySell == "B")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(mfPortfolioTransactionVoList[j - 1].NetHoldings + mfPortfolioTransactionVoList[j].BuyQuantity, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(mfPortfolioTransactionVoList[j].BuyQuantity, 5);
                    }
                }
                else if (mfPortfolioTransactionVoList[j].BuySell == "S")
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(mfPortfolioTransactionVoList[j - 1].NetHoldings - mfPortfolioTransactionVoList[j].SellQuantity, 5);
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = Math.Round(-mfPortfolioTransactionVoList[j].SellQuantity, 5);
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = mfPortfolioTransactionVoList[j - 1].NetHoldings;
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[j].NetHoldings = 0;
                    }
                }

                if (mfPortfolioTransactionVoList[j].NetHoldings != 0)

                    mfPortfolioTransactionVoList[j].AveragePrice = Math.Round(mfPortfolioTransactionVoList[j].NetCost / mfPortfolioTransactionVoList[j].NetHoldings, 5);
                if (mfPortfolioTransactionVo.TransactionClassificationCode == "DVP" || mfPortfolioTransactionVo.TransactionClassificationCode == "DRJ")
                    mfPortfolioTransactionVoList[j].RealizedProfitLoss = Math.Round(mfPortfolioTransactionVoList[j].SellPrice, 5);
                else if (mfPortfolioTransactionVoList[j].TransactionClassificationCode == "DVR" || mfPortfolioTransactionVoList[j].TransactionClassificationCode == "BNS" || mfPortfolioTransactionVoList[j].TransactionClassificationCode == "BNR" || mfPortfolioTransactionVoList[j].TransactionClassificationCode == "RRJ")
                    mfPortfolioTransactionVoList[j].RealizedProfitLoss = mfPortfolioTransactionVoList[j].CostOfAcquisition;
                else
                    mfPortfolioTransactionVoList[j].RealizedProfitLoss = Math.Round(mfPortfolioTransactionVoList[j].RealizedSalesValue - mfPortfolioTransactionVoList[j].CostOfSales, 5);

            }

            return mfPortfolioTransactionVoList;
        }
        public List<MFPortfolioTransactionVo> ProcessMFTransactionsNew(List<MFTransactionVo> mfTransactionVoList, int portfolioId, int mfCode, DateTime tradeDate)
        {
            List<MFPortfolioTransactionVo> mfPortfolioTransactionVoList = new List<MFPortfolioTransactionVo>();
            List<MFPortfolioBuyTransactionVo> mfPortfolioBuyTransactionVoList = new List<MFPortfolioBuyTransactionVo>();
            List<MFPortfolioSellTransactionVo> mfPortfolioSellTransactionVoList = new List<MFPortfolioSellTransactionVo>();
            List<MFPortfolioDivPayoutTransactionVo> mfPortfolioDivPayoutTransactionVoList = new List<MFPortfolioDivPayoutTransactionVo>();
            MFPortfolioBuyTransactionVo mfPortfolioBuyTransactionVo;
            MFPortfolioSellTransactionVo mfPortfolioSellTransactionVo;
            MFPortfolioDivPayoutTransactionVo mfPortfolioDivPayoutTransactionVo;
            MFPortfolioTransactionVo mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
            float currentNAV = 0;
            int buyCount = 0;
            int sellCount = 0;
            int j = 0;
            int k = 0;
            int isEquityScheme = 0;
            DataTable dtCustomerType;
            DataTable dtMFCapGainRate;
            DataRow drMFCapGainRate;
            double buyReminder = 0;
            double sellReminder = 0;
            double cummBuyQuantity = 0;
            double cummSellQuantity = 0;
            //Logic Not Used
            currentNAV = GetMFSchemePlanNAV(mfCode, tradeDate);
            isEquityScheme = IsSchemeEquity(mfCode);
            dtCustomerType = GetCustomerType(portfolioId);
            DataRow drCustomerType = dtCustomerType.Rows[0];
            dtMFCapGainRate = GetMFCapGainRate(drCustomerType["XCST_CustomerSubTypeCode"].ToString(), isEquityScheme, tradeDate);
            if (dtMFCapGainRate != null && dtMFCapGainRate.Rows.Count != 0)
                drMFCapGainRate = dtMFCapGainRate.Rows[0];
            else
                drMFCapGainRate = null;
            //Logic Not Used

            #region Buy Sell Separation

            for (int i = 0; i < mfTransactionVoList.Count; i++)
            {
                if (mfTransactionVoList[i].BuySell == "B")
                {
                    mfPortfolioBuyTransactionVo = new MFPortfolioBuyTransactionVo();
                    mfPortfolioBuyTransactionVo.PurchaseDate = mfTransactionVoList[i].TransactionDate;
                    mfPortfolioBuyTransactionVo.PurchasePrice = mfTransactionVoList[i].Price;
                    mfPortfolioBuyTransactionVo.TransactionClassificationCode = mfTransactionVoList[i].TransactionClassificationCode;
                    mfPortfolioBuyTransactionVo.TranscationType = mfTransactionVoList[i].TransactionType;
                    mfPortfolioBuyTransactionVo.Units = mfTransactionVoList[i].Units;
                    mfPortfolioBuyTransactionVo.STT = mfTransactionVoList[i].STT;
                    mfPortfolioBuyTransactionVoList.Add(mfPortfolioBuyTransactionVo);
                    cummBuyQuantity = cummBuyQuantity + mfTransactionVoList[i].Units;
                    buyCount++;

                }
                else if (mfTransactionVoList[i].BuySell == "S" && mfTransactionVoList[i].TransactionClassificationCode != "DVP")
                {
                    mfPortfolioSellTransactionVo = new MFPortfolioSellTransactionVo();
                    mfPortfolioSellTransactionVo.SellDate = mfTransactionVoList[i].TransactionDate;
                    mfPortfolioSellTransactionVo.SellPrice = mfTransactionVoList[i].Price;                   
                    mfPortfolioSellTransactionVo.TransactionClassificationCode = mfTransactionVoList[i].TransactionClassificationCode;
                    mfPortfolioSellTransactionVo.TranscationType = mfTransactionVoList[i].TransactionType;
                    mfPortfolioSellTransactionVo.Units = mfTransactionVoList[i].Units;
                    mfPortfolioSellTransactionVo.STT = mfTransactionVoList[i].STT;
                    mfPortfolioSellTransactionVoList.Add(mfPortfolioSellTransactionVo);
                    cummSellQuantity = cummSellQuantity + mfTransactionVoList[i].Units;
                    sellCount++;
                }
                else
                {
                    mfPortfolioDivPayoutTransactionVo = new MFPortfolioDivPayoutTransactionVo();
                    mfPortfolioDivPayoutTransactionVo.TradeDate = mfTransactionVoList[i].TransactionDate;
                    mfPortfolioDivPayoutTransactionVo.Amount = mfTransactionVoList[i].Amount;

                    mfPortfolioDivPayoutTransactionVoList.Add(mfPortfolioDivPayoutTransactionVo);
                }
            }
            #endregion Buy Sell Separation

            #region Buy/Sell Tagging for Short Position Sell Quantity > Buy Quantity
            if (cummSellQuantity > cummBuyQuantity)
            {
                while (k < sellCount)
                {
                    while (j < buyCount)
                    {
                        if (buyReminder == 0 && sellReminder == 0)
                        {
                            if (mfPortfolioBuyTransactionVoList[j].Units == mfPortfolioSellTransactionVoList[k].Units)
                            {
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                j++;
                                k++;
                                if (k >= sellCount)
                                    break;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units > mfPortfolioSellTransactionVoList[k].Units)
                            {
                                buyReminder = mfPortfolioBuyTransactionVoList[j].Units - mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                k++;
                                if (k >= sellCount)
                                    break;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units < mfPortfolioSellTransactionVoList[k].Units)
                            {
                                sellReminder = mfPortfolioSellTransactionVoList[k].Units - mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                j++;

                            }
                        }
                        else if (buyReminder != 0)
                        {
                            if (buyReminder == mfPortfolioSellTransactionVoList[k].Units)
                            {
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = buyReminder;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                buyReminder = 0;
                                j++;
                                k++;
                                if (k >= sellCount)
                                    break;
                            }
                            else if (buyReminder > mfPortfolioSellTransactionVoList[k].Units)
                            {
                                buyReminder = buyReminder - mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                k++;
                                if (k >= sellCount)
                                    break;
                            }
                            else if (buyReminder < mfPortfolioSellTransactionVoList[k].Units)
                            {
                                sellReminder = mfPortfolioSellTransactionVoList[k].Units - buyReminder;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = buyReminder;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = buyReminder;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * buyReminder;

                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                buyReminder = 0;
                                j++;

                            }
                        }
                        else if (sellReminder != 0)
                        {
                            if (mfPortfolioBuyTransactionVoList[j].Units == sellReminder)
                            {
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = sellReminder;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * sellReminder;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                sellReminder = 0;
                                j++;
                                k++;
                                if (k >= sellCount)
                                    break;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units > sellReminder)
                            {
                                buyReminder = mfPortfolioBuyTransactionVoList[j].Units - sellReminder;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = sellReminder;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = sellReminder;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * sellReminder;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                sellReminder = 0;
                                k++;
                                if (k >= sellCount)
                                    break;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units < sellReminder)
                            {
                                sellReminder = sellReminder - mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * mfPortfolioBuyTransactionVoList[j].Units;

                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                j++;

                            }
                        }
                    }

                    if (j == buyCount && k < sellCount)
                    {
                        if (sellReminder != 0)
                        {
                            mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                            mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                            mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                            mfPortfoloTransactionVo.SellQuantity = sellReminder;
                            mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioSellTransactionVoList[k].TransactionClassificationCode;
                            mfPortfoloTransactionVo.TransactionType = mfPortfolioSellTransactionVoList[k].TranscationType;
                            mfPortfoloTransactionVo.BuyQuantity = 0;
                            mfPortfoloTransactionVo.Closed = false;
                            mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                            sellReminder = 0;
                            k++;
                            if (k >= sellCount)
                                break;
                        }
                        else
                        {
                            mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                            mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                            mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                            mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                            mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioSellTransactionVoList[k].TransactionClassificationCode;
                            mfPortfoloTransactionVo.TransactionType = mfPortfolioSellTransactionVoList[k].TranscationType;
                            mfPortfoloTransactionVo.Closed = false;
                            mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                            k++;
                            if (k >= sellCount)
                                break;
                        }
                    }

                }
            }
            #endregion      Buy/Sell Tagging for Short Position Sell Quantity > Buy Quantity

            #region Buy/Sell Tagging for Long/Closed Position Buy Quantity > Sell Quantity
            else
            {
                //Buy Sell Tagging
                while (j < buyCount)
                {
                    while (k < sellCount)
                    {
                        if (buyReminder == 0 && sellReminder == 0)
                        {
                            if (mfPortfolioBuyTransactionVoList[j].Units == mfPortfolioSellTransactionVoList[k].Units)
                            {
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                j++;
                                k++;
                                if (j >= buyCount)
                                    break;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units > mfPortfolioSellTransactionVoList[k].Units)
                            {
                                buyReminder = mfPortfolioBuyTransactionVoList[j].Units - mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                k++;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units < mfPortfolioSellTransactionVoList[k].Units)
                            {
                                sellReminder = mfPortfolioSellTransactionVoList[k].Units - mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                j++;
                                if (j >= buyCount)
                                    break;
                            }
                        }
                        else if (buyReminder != 0)
                        {
                            if (buyReminder == mfPortfolioSellTransactionVoList[k].Units)
                            {
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = buyReminder;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                buyReminder = 0;
                                j++;
                                k++;
                                if (j >= buyCount)
                                    break;
                            }
                            else if (buyReminder > mfPortfolioSellTransactionVoList[k].Units)
                            {
                                buyReminder = buyReminder - mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioSellTransactionVoList[k].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = mfPortfolioSellTransactionVoList[k].STT;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                k++;
                            }
                            else if (buyReminder < mfPortfolioSellTransactionVoList[k].Units)
                            {
                                sellReminder = mfPortfolioSellTransactionVoList[k].Units - buyReminder;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = buyReminder;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = buyReminder;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * buyReminder;

                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                buyReminder = 0;
                                j++;
                                if (j >= buyCount)
                                    break;
                            }
                        }
                        else if (sellReminder != 0)
                        {
                            if (mfPortfolioBuyTransactionVoList[j].Units == sellReminder)
                            {
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = sellReminder;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * sellReminder;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                sellReminder = 0;
                                j++;
                                k++;
                                if (j >= buyCount)
                                    break;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units > sellReminder)
                            {
                                buyReminder = mfPortfolioBuyTransactionVoList[j].Units - sellReminder;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = sellReminder;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = sellReminder;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * sellReminder;
                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                                sellReminder = 0;
                                k++;
                            }
                            else if (mfPortfolioBuyTransactionVoList[j].Units < sellReminder)
                            {
                                sellReminder = sellReminder - mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                                mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                                mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                                mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                                mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                                mfPortfoloTransactionVo.SellPrice = mfPortfolioSellTransactionVoList[k].SellPrice;
                                mfPortfoloTransactionVo.SellQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                                mfPortfoloTransactionVo.SellDate = mfPortfolioSellTransactionVoList[k].SellDate;
                                mfPortfoloTransactionVo.STT = (mfPortfolioSellTransactionVoList[k].STT / mfPortfolioSellTransactionVoList[k].Units) * mfPortfolioBuyTransactionVoList[j].Units;

                                mfPortfoloTransactionVo.Closed = true;
                                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);

                                j++;
                                if (j >= buyCount)
                                    break;
                            }
                        }
                    }

                    if (k == sellCount && j < buyCount)
                    {
                        if (buyReminder != 0)
                        {
                            mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                            mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                            mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                            mfPortfoloTransactionVo.BuyQuantity = buyReminder;
                            mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                            mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                            mfPortfoloTransactionVo.Closed = false;
                            mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                            buyReminder = 0;
                            j++;
                        }
                        else
                        {
                            mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                            mfPortfoloTransactionVo.BuyDate = mfPortfolioBuyTransactionVoList[j].PurchaseDate;
                            mfPortfoloTransactionVo.BuyPrice = mfPortfolioBuyTransactionVoList[j].PurchasePrice;
                            mfPortfoloTransactionVo.BuyQuantity = mfPortfolioBuyTransactionVoList[j].Units;
                            mfPortfoloTransactionVo.TransactionClassificationCode = mfPortfolioBuyTransactionVoList[j].TransactionClassificationCode;
                            mfPortfoloTransactionVo.TransactionType = mfPortfolioBuyTransactionVoList[j].TranscationType;
                            mfPortfoloTransactionVo.Closed = false;
                            mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
                            j++;
                        }
                    }

                }
            }
            #endregion Buy/Sell Tagging for Long/Closed Position Buy Quantity > Sell Quantity

            #region DivPayout Addition
            for (int i = 0; i < mfPortfolioDivPayoutTransactionVoList.Count; i++)
            {
                mfPortfoloTransactionVo = new MFPortfolioTransactionVo();
                mfPortfoloTransactionVo.TransactionType = "Dividend Payout";
                mfPortfoloTransactionVo.TransactionClassificationCode = "DVP";
                mfPortfoloTransactionVo.BuyDate = mfPortfolioDivPayoutTransactionVoList[i].TradeDate;
                mfPortfoloTransactionVo.RealizedSalesValue = mfPortfolioDivPayoutTransactionVoList[i].Amount;
                mfPortfoloTransactionVo.Closed = false;
                mfPortfolioTransactionVoList.Add(mfPortfoloTransactionVo);
            }
            #endregion DivPayout Addition

            #region Transaction Level Valuation Logic
            for (int i = 0; i < mfPortfolioTransactionVoList.Count; i++)
            {
                if (mfPortfolioTransactionVoList[i].Closed)
                {
                    mfPortfolioTransactionVoList[i].CostOfAcquisition = mfPortfolioTransactionVoList[i].BuyPrice * mfPortfolioTransactionVoList[i].BuyQuantity;
                    mfPortfolioTransactionVoList[i].RealizedSalesValue = mfPortfolioTransactionVoList[i].SellPrice * mfPortfolioTransactionVoList[i].SellQuantity;
                    mfPortfolioTransactionVoList[i].AgeOfInvestment = ((TimeSpan)mfPortfolioTransactionVoList[i].SellDate.Subtract(mfPortfolioTransactionVoList[i].BuyDate)).Days;
                    if (mfPortfolioTransactionVoList[i].TransactionClassificationCode == "BCO")
                    {
                        mfPortfolioTransactionVoList[i].RealizedProfitLoss = 0;
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[i].RealizedProfitLoss = mfPortfolioTransactionVoList[i].RealizedSalesValue - mfPortfolioTransactionVoList[i].CostOfAcquisition;
                    }
                    mfPortfolioTransactionVoList[i].NotionalProfitLoss = 0;
                    if (mfPortfolioTransactionVoList[i].TransactionClassificationCode == "DVR")
                    {
                        mfPortfolioTransactionVoList[i].RealizedProfitLoss = mfPortfolioTransactionVoList[i].RealizedProfitLoss + mfPortfolioTransactionVoList[i].CostOfAcquisition;
                    }
                    if (isEquityScheme == 1)
                    {
                        mfPortfolioTransactionVoList[i].NetSalesProceed = mfPortfolioTransactionVoList[i].RealizedSalesValue - mfPortfolioTransactionVoList[i].STT;
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[i].NetSalesProceed = mfPortfolioTransactionVoList[i].RealizedSalesValue;
                    }

                    if (mfPortfolioTransactionVoList[i].AgeOfInvestment < 365)
                    {
                        if (mfPortfolioTransactionVoList[i].RealizedProfitLoss != 0)
                        {
                            if (mfPortfolioTransactionVoList[i].TransactionClassificationCode == "DVR")
                                mfPortfolioTransactionVoList[i].STCGTax = mfPortfolioTransactionVoList[i].RealizedProfitLoss - mfPortfolioTransactionVoList[i].CostOfAcquisition;
                            else
                                mfPortfolioTransactionVoList[i].STCGTax = mfPortfolioTransactionVoList[i].RealizedProfitLoss;
                        }
                    }
                    else
                    {
                        if (mfPortfolioTransactionVoList[i].RealizedProfitLoss != 0)
                        {
                            if (mfPortfolioTransactionVoList[i].TransactionClassificationCode == "DVR")
                                mfPortfolioTransactionVoList[i].LTCGTax = mfPortfolioTransactionVoList[i].RealizedProfitLoss - mfPortfolioTransactionVoList[i].CostOfAcquisition;
                            else
                                mfPortfolioTransactionVoList[i].LTCGTax = mfPortfolioTransactionVoList[i].RealizedProfitLoss;

                        }
                    }

                }
                else
                {
                    mfPortfolioTransactionVoList[i].NewCostOfAcquisition = mfPortfolioTransactionVoList[i].BuyPrice * mfPortfolioTransactionVoList[i].BuyQuantity;
                    if (mfPortfolioTransactionVoList[i].BuyQuantity != 0)
                    {
                        mfPortfolioTransactionVoList[i].CostOfAcquisition = mfPortfolioTransactionVoList[i].BuyPrice * mfPortfolioTransactionVoList[i].BuyQuantity;
                        mfPortfolioTransactionVoList[i].AgeOfInvestment = ((TimeSpan)DateTime.Now.Subtract(mfPortfolioTransactionVoList[i].BuyDate)).Days;
                        mfPortfolioTransactionVoList[i].CurrentNAV = currentNAV;
                        mfPortfolioTransactionVoList[i].CurrentValue = mfPortfolioTransactionVoList[i].BuyQuantity * currentNAV;
                        if (mfPortfolioTransactionVoList[i].TransactionClassificationCode == "DVR")
                        {
                            mfPortfolioTransactionVoList[i].RealizedProfitLoss = mfPortfolioTransactionVoList[i].RealizedProfitLoss + mfPortfolioTransactionVoList[i].CostOfAcquisition;
                        }
                        else
                            mfPortfolioTransactionVoList[i].RealizedProfitLoss = 0;
                        if (mfPortfolioTransactionVoList[i].CurrentValue != 0)
                            mfPortfolioTransactionVoList[i].NotionalProfitLoss = mfPortfolioTransactionVoList[i].CurrentValue - mfPortfolioTransactionVoList[i].CostOfAcquisition;
                        if (mfPortfolioTransactionVoList[i].AgeOfInvestment < 365)
                        {
                            if (mfPortfolioTransactionVoList[i].NotionalProfitLoss != 0)
                            {
                                mfPortfolioTransactionVoList[i].STCGTax = mfPortfolioTransactionVoList[i].NotionalProfitLoss;
                            }
                        }
                        else
                        {
                            if (mfPortfolioTransactionVoList[i].NotionalProfitLoss != 0)
                            {
                                mfPortfolioTransactionVoList[i].LTCGTax = mfPortfolioTransactionVoList[i].NotionalProfitLoss;

                            }
                        }
                    }
                    else if (mfPortfolioTransactionVoList[i].SellQuantity != 0)
                    {
                        mfPortfolioTransactionVoList[i].RealizedProfitLoss = 0;
                        mfPortfolioTransactionVoList[i].NotionalProfitLoss = 0;
                        mfPortfolioTransactionVoList[i].AgeOfInvestment = 0;
                    }
                    else
                    {
                        mfPortfolioTransactionVoList[i].RealizedProfitLoss = mfPortfolioTransactionVoList[i].RealizedSalesValue;
                        mfPortfolioTransactionVoList[i].NotionalProfitLoss = 0;
                        mfPortfolioTransactionVoList[i].AgeOfInvestment = ((TimeSpan)DateTime.Now.Subtract(mfPortfolioTransactionVoList[i].BuyDate)).Days;
                    }
                }

                mfPortfolioTransactionVoList[i].TotalProfitLoss = mfPortfolioTransactionVoList[i].RealizedProfitLoss + mfPortfolioTransactionVoList[i].NotionalProfitLoss;
                if (mfPortfolioTransactionVoList[i].CostOfAcquisition != 0)
                    mfPortfolioTransactionVoList[i].AbsoluteReturns = (mfPortfolioTransactionVoList[i].TotalProfitLoss * 100) / mfPortfolioTransactionVoList[i].CostOfAcquisition;
                else
                    mfPortfolioTransactionVoList[i].AbsoluteReturns = 0;
                if (mfPortfolioTransactionVoList[i].AgeOfInvestment < 365)
                {
                    mfPortfolioTransactionVoList[i].AnnualReturns = mfPortfolioTransactionVoList[i].AbsoluteReturns;

                }
                else
                {
                    mfPortfolioTransactionVoList[i].AnnualReturns = (mfPortfolioTransactionVoList[i].AbsoluteReturns * 365) / mfPortfolioTransactionVoList[i].AgeOfInvestment;

                }
            }
            #endregion Transaction Level Valuation Logic



            return mfPortfolioTransactionVoList;
        }
        public DividendTaggingPortfolioVo ProcessMFTransactionsForDivTagging(List<MFTransactionVo> mfTransactionVoList, DividendTaggingPortfolioVo dividendTaggingPortfolioVo, int portfolioId, DateTime tradeDate)
        {
            List<DividendTaggingTransactionVo> mfDivTaggingTransactionVoList = new List<DividendTaggingTransactionVo>();
            DividendTaggingTransactionVo dividendTaggingTransactionVo = new DividendTaggingTransactionVo();
            int customerId = mfTransactionVoList[0].CustomerId;
            int mfCode = mfTransactionVoList[0].MFCode;
            List<double> divUnits = new List<double>();
            List<double> divRatio = new List<double>();
            List<double> buyUnits = new List<double>();
            List<double> buyPrice = new List<double>();
            List<double> amountListXIRR = new List<double>();
            List<DateTime> dateListXIRR = new List<DateTime>();
            double[] dlrCurrentValueXIRR;
            DateTime[] dtrTranDateXIRR;
            int rxirrTransCount = 0;
            double originalUnitsSold = 0;
            double reinvestedUnitsSold = 0;
            double costOfSales = 0;
            double saleProceeds = 0;
            double realizedDivPayout=0;
            double realizedPL = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("AccountId");
            dt.Columns.Add("FolioNum");
            dt.Columns.Add("PortfolioId");
            dt.Columns.Add("SchemePlanCode");
            dt.Columns.Add("Scheme");
            dt.Columns.Add("TransactionClassificationCode");
            dt.Columns.Add("BuySell");
            dt.Columns.Add("TransactionType");
            dt.Columns.Add("TransactionDate");
            dt.Columns.Add("TransactionAmount");
            dt.Columns.Add("TotalValue");
            dt.Columns.Add("CostOfHolding");
            dt.Columns.Add("CostOfSale");
            dt.Columns.Add("Price");
            dt.Columns.Add("Units");
            dt.Columns.Add("OriginalUnits");
            dt.Columns.Add("DivUnits");
            dt.Columns.Add("TotalOriginalUnits");
            dt.Columns.Add("TotalDivUnits");
            dt.Columns.Add("BalanceUnits");
            dt.Columns.Add("AveragePrice");
            dt.Columns.Add("DivRatio");
            dt.Columns.Add("UnitCostOfInvestment");
            dt.Columns.Add("RealizedPL");
            for (int i = 0; i < mfTransactionVoList.Count; i++)
            {
                dividendTaggingTransactionVo = new DividendTaggingTransactionVo();
                dividendTaggingTransactionVo.TransactionDate = mfTransactionVoList[i].TransactionDate;
                dividendTaggingTransactionVo.AccountId = mfTransactionVoList[i].AccountId;
                dividendTaggingTransactionVo.BuySell = mfTransactionVoList[i].BuySell;
                dividendTaggingTransactionVo.FolioNum = mfTransactionVoList[i].Folio;
                dividendTaggingTransactionVo.PortfolioId = mfTransactionVoList[i].PortfolioId;
                dividendTaggingTransactionVo.Price = mfTransactionVoList[i].Price;
                dividendTaggingTransactionVo.Scheme = mfTransactionVoList[i].SchemePlan;
                dividendTaggingTransactionVo.SchemePlanCode = mfTransactionVoList[i].MFCode;
                dividendTaggingTransactionVo.TransactionType = mfTransactionVoList[i].TransactionType;
                dividendTaggingTransactionVo.TransactionClassificationCode = mfTransactionVoList[i].TransactionClassificationCode;
                if (mfTransactionVoList[i].BuySell == "B")
                {
                    
                    dividendTaggingTransactionVo.Units = mfTransactionVoList[i].Units;
                    if (mfTransactionVoList[i].TransactionClassificationCode != "DVR")
                    {
                        dividendTaggingTransactionVo.TransactionAmount = mfTransactionVoList[i].Amount;
                        dividendTaggingTransactionVo.OriginalUnits = mfTransactionVoList[i].Units;
                        amountListXIRR.Add(dividendTaggingTransactionVo.TransactionAmount);
                        dateListXIRR.Add(mfTransactionVoList[i].TransactionDate);
                        rxirrTransCount++;
                        buyPrice.Add(mfTransactionVoList[i].Price);
                        buyUnits.Add(mfTransactionVoList[i].Units);
                    }
                    else
                        dividendTaggingTransactionVo.DivUnits = mfTransactionVoList[i].Units;

                }
                else
                {
                    dividendTaggingTransactionVo.TransactionAmount = -(mfTransactionVoList[i].Amount);
                    
                    amountListXIRR.Add(dividendTaggingTransactionVo.TransactionAmount);
                    dateListXIRR.Add(mfTransactionVoList[i].TransactionDate);
                    rxirrTransCount++;
                    if (mfTransactionVoList[i].TransactionClassificationCode != "DVP")
                    {
                        dividendTaggingTransactionVo.Units = -(mfTransactionVoList[i].Units);
                        
                    }
                    else
                        realizedDivPayout = realizedDivPayout + dividendTaggingTransactionVo.TransactionAmount;

                }

                mfDivTaggingTransactionVoList.Add(dividendTaggingTransactionVo);

            }

            for (int j = 0; j < mfDivTaggingTransactionVoList.Count; j++)
            {


                if (j == 0)
                {
                    if (mfDivTaggingTransactionVoList[j].BuySell == "B")
                    {
                        
                        mfDivTaggingTransactionVoList[j].TotalValue = mfDivTaggingTransactionVoList[j].TransactionAmount;
                        mfDivTaggingTransactionVoList[j].CostOfHolding = mfDivTaggingTransactionVoList[j].TotalValue;
                        mfDivTaggingTransactionVoList[j].BalanceUnits = mfDivTaggingTransactionVoList[j].Units;
                        mfDivTaggingTransactionVoList[j].TotalDivUnits = mfDivTaggingTransactionVoList[j].DivUnits;
                        if (mfDivTaggingTransactionVoList[j].TransactionClassificationCode != "DVR")
                        {
                            mfDivTaggingTransactionVoList[j].TotalOriginalUnits = mfDivTaggingTransactionVoList[j].OriginalUnits;
                            mfDivTaggingTransactionVoList[j].AveragePrice = mfDivTaggingTransactionVoList[j].CostOfHolding / mfDivTaggingTransactionVoList[j].TotalOriginalUnits;

                        }
                        else
                        {
                            mfDivTaggingTransactionVoList[j].AveragePrice = 0;
                            mfDivTaggingTransactionVoList[j].OriginalUnits = mfDivTaggingTransactionVoList[j].Units;
                            mfDivTaggingTransactionVoList[j].TotalOriginalUnits = mfDivTaggingTransactionVoList[j].OriginalUnits;
                            mfDivTaggingTransactionVoList[j].DivRatio = mfDivTaggingTransactionVoList[j].DivUnits / mfDivTaggingTransactionVoList[j].TotalOriginalUnits;
                            divUnits.Add(mfDivTaggingTransactionVoList[j].DivUnits);
                            divRatio.Add(mfDivTaggingTransactionVoList[j].DivRatio);
                            
                        }

                    }
                    else
                    {
                        mfDivTaggingTransactionVoList[j].CostOfHolding = mfDivTaggingTransactionVoList[j].TotalValue;
                        mfDivTaggingTransactionVoList[j].TotalValue = mfDivTaggingTransactionVoList[j].TransactionAmount;
                        mfDivTaggingTransactionVoList[j].BalanceUnits = mfDivTaggingTransactionVoList[j].Units;
                        mfDivTaggingTransactionVoList[j].TotalDivUnits = mfDivTaggingTransactionVoList[j].DivUnits;
                        mfDivTaggingTransactionVoList[j].TotalOriginalUnits = mfDivTaggingTransactionVoList[j].OriginalUnits;
                        mfDivTaggingTransactionVoList[j].OriginalUnits = mfDivTaggingTransactionVoList[j].Units;
                        originalUnitsSold = originalUnitsSold + mfDivTaggingTransactionVoList[j].OriginalUnits;
                        reinvestedUnitsSold = reinvestedUnitsSold + mfDivTaggingTransactionVoList[j].DivUnits;
                        costOfSales = costOfSales + mfDivTaggingTransactionVoList[j].CostOfSale;
                        saleProceeds = saleProceeds + mfDivTaggingTransactionVoList[j].TransactionAmount;
                        realizedPL = realizedPL + (mfDivTaggingTransactionVoList[j].Units * (mfDivTaggingTransactionVoList[j].Price - mfDivTaggingTransactionVoList[j].AveragePrice));
                    }


                }
                else
                {
                    if (mfDivTaggingTransactionVoList[j].BuySell == "B")
                    {
                        mfDivTaggingTransactionVoList[j].TotalValue = mfDivTaggingTransactionVoList[j - 1].TotalValue + (mfDivTaggingTransactionVoList[j].TransactionAmount);
                        mfDivTaggingTransactionVoList[j].CostOfHolding = mfDivTaggingTransactionVoList[j - 1].CostOfHolding + mfDivTaggingTransactionVoList[j].TransactionAmount;
                        mfDivTaggingTransactionVoList[j].TotalOriginalUnits = mfDivTaggingTransactionVoList[j - 1].TotalOriginalUnits + mfDivTaggingTransactionVoList[j].OriginalUnits;
                       
                        mfDivTaggingTransactionVoList[j].TotalDivUnits = mfDivTaggingTransactionVoList[j - 1].TotalDivUnits + mfDivTaggingTransactionVoList[j].DivUnits;
                        mfDivTaggingTransactionVoList[j].BalanceUnits = mfDivTaggingTransactionVoList[j - 1].BalanceUnits + mfDivTaggingTransactionVoList[j].Units;
                        if (mfDivTaggingTransactionVoList[j].TransactionClassificationCode != "DVR")
                        {
                            mfDivTaggingTransactionVoList[j].AveragePrice = mfDivTaggingTransactionVoList[j].CostOfHolding / mfDivTaggingTransactionVoList[j].TotalOriginalUnits;
                            mfDivTaggingTransactionVoList[j].DivRatio = mfDivTaggingTransactionVoList[j - 1].DivRatio;
                        }
                        else
                        {
                            mfDivTaggingTransactionVoList[j].AveragePrice = mfDivTaggingTransactionVoList[j - 1].AveragePrice;
                            mfDivTaggingTransactionVoList[j].DivRatio = mfDivTaggingTransactionVoList[j].DivUnits / mfDivTaggingTransactionVoList[j].TotalOriginalUnits;
                            divUnits.Add(mfDivTaggingTransactionVoList[j].DivUnits);
                            divRatio.Add(mfDivTaggingTransactionVoList[j].DivRatio);
                        }

                    }
                    else
                    {
                        mfDivTaggingTransactionVoList[j].DivRatio = mfDivTaggingTransactionVoList[j - 1].DivRatio;
                        mfDivTaggingTransactionVoList[j].AveragePrice = mfDivTaggingTransactionVoList[j - 1].AveragePrice;
                        if (divRatio.Count != 0 && divUnits.Count != 0)
                        {
                            for (int k = 0; k < divUnits.Count; k++)
                            {
                                if (divUnits[k] > 0)
                                {
                                    mfDivTaggingTransactionVoList[j].DivUnits = -(Math.Abs(mfDivTaggingTransactionVoList[j].Units) * divRatio[k]);
                                    divUnits[k] = divUnits[k] - Math.Abs(mfDivTaggingTransactionVoList[j].DivUnits);
                                    break;
                                }
                            }
                        }
                        mfDivTaggingTransactionVoList[j].OriginalUnits = -(Math.Abs(mfDivTaggingTransactionVoList[j].Units) - Math.Abs(mfDivTaggingTransactionVoList[j].DivUnits));
                        if (buyUnits.Count != 0 && buyPrice.Count != 0)
                        {
                            for (int m = 0; m < buyUnits.Count; m++)
                            {
                                if (buyUnits[m] > 0)
                                {
                                    mfDivTaggingTransactionVoList[j].CostOfSale = mfDivTaggingTransactionVoList[j].Units * buyPrice[m];
                                    buyUnits[m] = buyUnits[m] - Math.Abs(mfDivTaggingTransactionVoList[j].OriginalUnits);
                                    mfDivTaggingTransactionVoList[j].UnitCostOfInvestment = buyPrice[m];
                                    break;
                                }
                            }
                        }
                        mfDivTaggingTransactionVoList[j].TotalOriginalUnits = mfDivTaggingTransactionVoList[j - 1].TotalOriginalUnits + mfDivTaggingTransactionVoList[j].OriginalUnits;
                        mfDivTaggingTransactionVoList[j].TotalValue = mfDivTaggingTransactionVoList[j - 1].TotalValue + (mfDivTaggingTransactionVoList[j].TransactionAmount);
                        mfDivTaggingTransactionVoList[j].TotalDivUnits = mfDivTaggingTransactionVoList[j - 1].TotalDivUnits + mfDivTaggingTransactionVoList[j].DivUnits;
                        mfDivTaggingTransactionVoList[j].BalanceUnits = mfDivTaggingTransactionVoList[j - 1].BalanceUnits + mfDivTaggingTransactionVoList[j].Units;
                        mfDivTaggingTransactionVoList[j].RealizedPL = (Math.Abs(mfDivTaggingTransactionVoList[j].Units) * (mfDivTaggingTransactionVoList[j].Price - mfDivTaggingTransactionVoList[j].UnitCostOfInvestment));
                        mfDivTaggingTransactionVoList[j].CostOfHolding = mfDivTaggingTransactionVoList[j - 1].CostOfHolding - Math.Abs(mfDivTaggingTransactionVoList[j].CostOfSale);
                        originalUnitsSold = originalUnitsSold + mfDivTaggingTransactionVoList[j].OriginalUnits;
                        reinvestedUnitsSold = reinvestedUnitsSold + mfDivTaggingTransactionVoList[j].DivUnits;
                        costOfSales = costOfSales + mfDivTaggingTransactionVoList[j].CostOfSale;
                        saleProceeds = saleProceeds + mfDivTaggingTransactionVoList[j].TransactionAmount;
                        realizedPL = realizedPL + mfDivTaggingTransactionVoList[j].RealizedPL;
                    }

                }



            }
            for (int l = 0; l < mfDivTaggingTransactionVoList.Count; l++)
            {
                DataRow dr = dt.NewRow();
               dr["AccountId"]= mfDivTaggingTransactionVoList[l].AccountId;
               dr["FolioNum"]=mfDivTaggingTransactionVoList[l].FolioNum;
               dr["PortfolioId"]=mfDivTaggingTransactionVoList[l].PortfolioId;
               dr["SchemePlanCode"]=mfDivTaggingTransactionVoList[l].SchemePlanCode;
               dr["Scheme"]=mfDivTaggingTransactionVoList[l].Scheme;
               dr["TransactionClassificationCode"]=mfDivTaggingTransactionVoList[l].TransactionClassificationCode;
               dr["BuySell"]=mfDivTaggingTransactionVoList[l].BuySell;
               dr["TransactionType"]=mfDivTaggingTransactionVoList[l].TransactionType;
               dr["TransactionDate"]=mfDivTaggingTransactionVoList[l].TransactionDate;
               dr["TransactionAmount"]=mfDivTaggingTransactionVoList[l].TransactionAmount;
               dr["TotalValue"]=mfDivTaggingTransactionVoList[l].TotalValue;
               dr["CostOfHolding"]=mfDivTaggingTransactionVoList[l].CostOfHolding;
               dr["CostOfSale"]=mfDivTaggingTransactionVoList[l].CostOfSale;
               dr["Price"]=mfDivTaggingTransactionVoList[l].Price;
               dr["Units"]=mfDivTaggingTransactionVoList[l].Units;
               dr["OriginalUnits"]=mfDivTaggingTransactionVoList[l].OriginalUnits;
               dr["DivUnits"]=mfDivTaggingTransactionVoList[l].DivUnits;
               dr["TotalOriginalUnits"]=mfDivTaggingTransactionVoList[l].TotalOriginalUnits;
               dr["TotalDivUnits"]=mfDivTaggingTransactionVoList[l].TotalDivUnits;
               dr["BalanceUnits"]=mfDivTaggingTransactionVoList[l].BalanceUnits;
               dr["AveragePrice"]=mfDivTaggingTransactionVoList[l].AveragePrice;
               dr["DivRatio"]=mfDivTaggingTransactionVoList[l].DivRatio;
               dr["UnitCostOfInvestment"]=mfDivTaggingTransactionVoList[l].UnitCostOfInvestment;
               dr["RealizedPL"] = mfDivTaggingTransactionVoList[l].RealizedPL;
               dt.Rows.Add(dr);
            }
            int count=mfDivTaggingTransactionVoList.Count;
            dividendTaggingPortfolioVo.DividendTaggingTransactionVoList = mfDivTaggingTransactionVoList;
            //RealizedSection
            dividendTaggingPortfolioVo.OriginalUnitsSold = Math.Abs(originalUnitsSold);
            dividendTaggingPortfolioVo.ReinvestedUnitsSold = Math.Abs(reinvestedUnitsSold);
            dividendTaggingPortfolioVo.TotalUnitsSold = Math.Abs(originalUnitsSold) + Math.Abs(reinvestedUnitsSold);
            dividendTaggingPortfolioVo.CostOfSales = Math.Abs(costOfSales);
            dividendTaggingPortfolioVo.SaleProceeds = Math.Abs(saleProceeds);
            dividendTaggingPortfolioVo.RealizedDividendPayout = realizedDivPayout;
            dividendTaggingPortfolioVo.RealizedDividendReinvested = dividendTaggingPortfolioVo.ReinvestedUnitsSold * mfDivTaggingTransactionVoList[count - 1].AveragePrice;
            dividendTaggingPortfolioVo.RealizedDividendTotal = dividendTaggingPortfolioVo.RealizedDividendPayout + dividendTaggingPortfolioVo.RealizedDividendReinvested;
            dividendTaggingPortfolioVo.RealizedPL = realizedPL;
            dividendTaggingPortfolioVo.TotalRealizedPL = dividendTaggingPortfolioVo.RealizedPL + dividendTaggingPortfolioVo.RealizedDividendTotal;
            dividendTaggingPortfolioVo.RealizedAbsReturn = (dividendTaggingPortfolioVo.TotalRealizedPL / dividendTaggingPortfolioVo.CostOfSales)*100;

            //Holdings Section
            dividendTaggingPortfolioVo.OutstandingUnits = mfDivTaggingTransactionVoList[count - 1].BalanceUnits;
            dividendTaggingPortfolioVo.OutstandingOriginalUnits = mfDivTaggingTransactionVoList[count - 1].TotalOriginalUnits;
            dividendTaggingPortfolioVo.OutstandingDividendUnits = mfDivTaggingTransactionVoList[count - 1].TotalDivUnits;
            dividendTaggingPortfolioVo.AvgCostPerUnit = mfDivTaggingTransactionVoList[count - 1].AveragePrice;
            dividendTaggingPortfolioVo.AcqCost = mfDivTaggingTransactionVoList[count - 1].CostOfHolding;
            dividendTaggingPortfolioVo.OutStandingDividendPayout = realizedDivPayout;
            dividendTaggingPortfolioVo.OutStandingDividendReinvested = dividendTaggingPortfolioVo.OutstandingDividendUnits * dividendTaggingPortfolioVo.AvgCostPerUnit;
            dividendTaggingPortfolioVo.NetAcqCost = dividendTaggingPortfolioVo.AcqCost + dividendTaggingPortfolioVo.OutStandingDividendReinvested;
            dividendTaggingPortfolioVo.CurrentNAV = GetMFSchemePlanNAV(dividendTaggingPortfolioVo.MFCode, tradeDate);
            dividendTaggingPortfolioVo.CurrentValue = dividendTaggingPortfolioVo.OutstandingUnits * dividendTaggingPortfolioVo.CurrentNAV;
            dividendTaggingPortfolioVo.OutStandingDividendTotal = dividendTaggingPortfolioVo.OutStandingDividendPayout + dividendTaggingPortfolioVo.OutStandingDividendReinvested;
            dividendTaggingPortfolioVo.UnRealizedPL = dividendTaggingPortfolioVo.OutstandingUnits * (dividendTaggingPortfolioVo.CurrentNAV - dividendTaggingPortfolioVo.AvgCostPerUnit);
            dividendTaggingPortfolioVo.OutStandingTotalPL = dividendTaggingPortfolioVo.UnRealizedPL + dividendTaggingPortfolioVo.OutStandingDividendTotal;
            dividendTaggingPortfolioVo.OutStandingAbsReturn = (dividendTaggingPortfolioVo.OutStandingTotalPL / dividendTaggingPortfolioVo.AcqCost)*100;
            //XIRR Calculation
            if (dividendTaggingPortfolioVo.CurrentValue > 0)
            {
                amountListXIRR.Add(-dividendTaggingPortfolioVo.CurrentValue);
                dateListXIRR.Add(DateTime.Today);
                rxirrTransCount++;
            }
            else if (dividendTaggingPortfolioVo.CurrentValue < 0)
            {
                amountListXIRR.Add(dividendTaggingPortfolioVo.CurrentValue);
                dateListXIRR.Add(DateTime.Today);
                rxirrTransCount++;
            }

            dlrCurrentValueXIRR = new double[rxirrTransCount];
            dtrTranDateXIRR = new DateTime[rxirrTransCount];
            for (int m = 0; m < rxirrTransCount; m++)
            {
                dlrCurrentValueXIRR[m] = amountListXIRR[m];
                dtrTranDateXIRR[m] = dateListXIRR[m];
            }
            dividendTaggingPortfolioVo.OutStandingXIRR = Math.Round(CalculateXIRR(dlrCurrentValueXIRR, dtrTranDateXIRR),5)*100;
            
            //All Section
            dividendTaggingPortfolioVo.AllDividendPayout = realizedDivPayout;
            dividendTaggingPortfolioVo.AllDividendReinvested = dividendTaggingPortfolioVo.OutStandingDividendReinvested + dividendTaggingPortfolioVo.RealizedDividendReinvested;
            dividendTaggingPortfolioVo.AllDividendTotal = dividendTaggingPortfolioVo.AllDividendPayout + dividendTaggingPortfolioVo.AllDividendReinvested;
            dividendTaggingPortfolioVo.AllUnRealizedPL = dividendTaggingPortfolioVo.TotalUnitsSold * dividendTaggingPortfolioVo.AvgCostPerUnit;
            dividendTaggingPortfolioVo.AllTotalPL = dividendTaggingPortfolioVo.AllUnRealizedPL + dividendTaggingPortfolioVo.AllDividendTotal;
            dividendTaggingPortfolioVo.AllAbsReturn = (dividendTaggingPortfolioVo.AllTotalPL / dividendTaggingPortfolioVo.AcqCost)*100;
            dividendTaggingPortfolioVo.AllXIRR = Math.Round(CalculateXIRR(dlrCurrentValueXIRR, dtrTranDateXIRR), 5) * 100;

            return dividendTaggingPortfolioVo;
        }
        #endregion Portfolio Specific
        public int IsSchemeEquity(int mfCode)
        {
            int isSchemeEquity = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            isSchemeEquity = customerPortfolioDao.IsSchemeEquity(mfCode);
            return isSchemeEquity;
        }
        public DataTable GetCustomerType(int portfolioId)
        {
            DataTable dtCustomerType;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            dtCustomerType = customerPortfolioDao.GetCustomerType(portfolioId);
            return dtCustomerType;
        }
        public DataTable GetMFCapGainRate(string customerType, int IsSchemeEquity, DateTime tradeDate)
        {
            DataTable dtGetMFCapGainRate;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            dtGetMFCapGainRate = customerPortfolioDao.GetMFCapGainRate(customerType, IsSchemeEquity, tradeDate);
            return dtGetMFCapGainRate;
        }
        public float GetMFSchemePlanNAV(int schemePlanCode, DateTime navDate)
        {
            float schemePlanNAV = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            schemePlanNAV = customerPortfolioDao.GetMFSchemePlanNAV(schemePlanCode, navDate);
            return schemePlanNAV;
        }
        /// <summary>
        /// Added For Getting Equity Price.
        /// </summary>
        /// Added by: Vinayak Patil
        /// <param name="schemePlanCode"></param>
        /// <param name="navDate"></param>
        /// <returns></returns>

        public float GetEQScripPrice(int ScripCode, DateTime navDate)
        {
            float ScripPrice = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            ScripPrice = customerPortfolioDao.GetEQScripPrice(ScripCode, navDate);
            return ScripPrice;
        }
        /// <summary>
        /// ** End **
        /// </summary>
        /// <returns></returns>
      
        public float GetMFSchemePlanSnapShotNAV(int schemePlanCode)
        {
            float schemePlanNAV = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            schemePlanNAV = customerPortfolioDao.GetMFSchemePlanSnapShotNAV(schemePlanCode);
            return schemePlanNAV;
        }
        public int AddMutualFundNetPosition(MFPortfolioVo mfPortfolioVo, int userId)
        {
            int mfNPId = 0;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                mfNPId = customerPortfolioDao.AddMutualFundNetPosition(mfPortfolioVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:AddMutualFundNetPosition()");


                object[] objects = new object[2];
                objects[0] = mfPortfolioVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfNPId;
        }
        public void AddMutualFundNetPosition(List<MFPortfolioVo> mfPortfolioVoList, int userId)
        {
            int mfNPId = 0;

            try
            {
                for (int i = 0; i < mfPortfolioVoList.Count; i++)
                {
                    mfNPId = AddMutualFundNetPosition(mfPortfolioVoList[i], userId);
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

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:AddMutualFundNetPosition()");


                object[] objects = new object[2];
                objects[0] = mfPortfolioVoList;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        public bool DeleteMutualFundNetPosition(int customerId, DateTime valuationDate)
        {
            bool bResult = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                bResult = customerPortfolioDao.DeleteMutualFundNetPosition(customerId, valuationDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:DeleteMutualFundNetPosition()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }
        public bool DeleteMutualFundNetPosition(int schemePlanCode,int accountId,DateTime valuationDate)
        {
            bool bResult = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                bResult = customerPortfolioDao.DeleteMutualFundNetPosition(schemePlanCode,accountId,valuationDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioBo.cs:DeleteMutualFundNetPosition()");


                object[] objects = new object[3];
                objects[0] = schemePlanCode;
                objects[1] = accountId;
                objects[2] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;

        }
        #endregion MF Portfolio Valuation

        #region Portfolio Label XIRR
        public DataTable GetCustomerPortfolioLabelXIRR(string portfolioIds)
        {
            
            DataSet dsCustomerTransaction;
            PortfolioDao customerPortfolioDao = new PortfolioDao();
            string tempPortfoliId;
            DataTable dtCustomerPortfolio;
            DataTable dtCustomerTransaction;
            DataTable dtCustomerPortfolioNetHolding;
            DataTable dtCustomerPortfolioXIRR;
            double tempPortfolioXIRR;

            dsCustomerTransaction = customerPortfolioDao.GetCustomerTransactionDetailsForXIRR(portfolioIds);
            dtCustomerPortfolio = dsCustomerTransaction.Tables[0];
            dtCustomerTransaction = dsCustomerTransaction.Tables[1];
            dtCustomerPortfolioNetHolding = dsCustomerTransaction.Tables[2];
            DataRow[] drTransactionDateAmount;

            dtCustomerPortfolioXIRR = new DataTable();
            dtCustomerPortfolioXIRR.Columns.Add("CustomerId", typeof(Int32));
            dtCustomerPortfolioXIRR.Columns.Add("CustomerName", typeof(string));
            dtCustomerPortfolioXIRR.Columns.Add("PortfolioId", typeof(Int32));
            dtCustomerPortfolioXIRR.Columns.Add("PortfolioName", typeof(string));
            dtCustomerPortfolioXIRR.Columns.Add("XIRR", typeof(decimal));
            DataRow drXIRR;
            foreach (DataRow dr in dtCustomerPortfolio.Rows)
            {
                drXIRR=dtCustomerPortfolioXIRR.NewRow();
                tempPortfoliId = dr["CP_PortfolioId"].ToString();
                //var queryTransactionDate = from r in dtCustomerTransaction.AsEnumerable()
                //                           where r.Field<int>("CP_PortfolioId") == tempPortfoliId
                //                           select r.Field<DateTime>("CMFT_TransactionDate");

                //var queryTransactionAmount = from r in dtCustomerTransaction.AsEnumerable()
                //                             where r.Field<int>("CP_PortfolioId") == tempPortfoliId
                //                             select r.Field<double>("CMFT_Amount");

               //var data = from row in dtCustomerTransaction.AsEnumerable()
                drTransactionDateAmount = dtCustomerTransaction.Select("CP_PortfolioId=" + tempPortfoliId);

                double[] transactionAmount = new double[drTransactionDateAmount.Count()+1];
                DateTime[] transactionDate = new DateTime[drTransactionDateAmount.Count()+1];
                int tempCount=0;
                foreach (DataRow drAmountDate in drTransactionDateAmount)
                {                    
                        transactionAmount[tempCount] = double.Parse(drAmountDate["Calculated_Amount"].ToString());
                        transactionDate[tempCount] = DateTime.Parse(drAmountDate["CMFT_TransactionDate"].ToString());
                        tempCount++;
                   
                }
                foreach (DataRow drNetHolding in dtCustomerPortfolioNetHolding.Rows)
                {
                    if (drNetHolding["CP_PortfolioId"].ToString() == tempPortfoliId)
                    {
                        transactionAmount[tempCount] = double.Parse(drNetHolding["Holding_Amount"].ToString());
                        transactionDate[tempCount] = DateTime.Parse(drNetHolding["Holding_AsOn"].ToString());
                    }

                }
                tempPortfolioXIRR = CalculatePortfolioXIRR(transactionAmount, transactionDate);
                drXIRR["CustomerId"] = dr["C_CustomerId"];
                drXIRR["CustomerName"] = dr["C_CustomerName"];
                drXIRR["PortfolioId"] = tempPortfoliId;
                drXIRR["PortfolioName"] = dr["CP_PortfolioName"];
                drXIRR["XIRR"] = Math.Round(tempPortfolioXIRR*100,5);
                dtCustomerPortfolioXIRR.Rows.Add(drXIRR);
            }

            return dtCustomerPortfolioXIRR;
        }
        #endregion Portfolio Label XIRR

        public DataSet GetProductAssetInstrumentCategory()
        {
            CustomerPortfolioDao customerportfoliodao = new CustomerPortfolioDao();
            return customerportfoliodao.GetProductAssetInstrumentCategory();
        }

        public bool DeleteGIAccount(int Account, int InsuranceNo)
        {
            bool Delete;
            CustomerPortfolioDao customerAccountDao = new CustomerPortfolioDao();
            Delete = customerAccountDao.DeleteGIAccount(Account, InsuranceNo);
            return Delete;

        }

        /* Delete Eq NetPosition transaction entries for Latest val date */

        public bool DeleteEquityNetPosition(int scripCode, int TradeAccId, DateTime valuationDate)
        {
            bool bResult = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {

                customerPortfolioDao.DeleteEquityNetPosition(scripCode, TradeAccId, valuationDate);
                bResult = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:DeleteMutualFundNetPosition()");


                object[] objects = new object[3];
                objects[0] = scripCode;
                objects[1] = TradeAccId;
                objects[2] = valuationDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool CheckValuationDoneOrNotForThePickedDate(int adviserId, string assetGroupCode, DateTime transactionDate)
        {
            bool bCheckValuationForDate = false;
            CustomerPortfolioDao customerPortfolioDao = new CustomerPortfolioDao();
            try
            {
                bCheckValuationForDate = customerPortfolioDao.CheckValuationDoneOrNotForThePickedDate(adviserId, assetGroupCode, transactionDate);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:CheckValuationDoneOrNotForThePickedDate()");

                object[] objects = new object[5];
                objects[0] = adviserId;
                objects[1] = transactionDate;
                objects[2] = assetGroupCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bCheckValuationForDate;
        }
       
    }
}
