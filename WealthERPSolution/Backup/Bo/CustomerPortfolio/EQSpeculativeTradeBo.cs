using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.ApplicationBlocks.ExceptionManagement;

using System.Data.Common;
using System.Collections.Specialized;

using VoCustomerPortfolio;
using DaoCustomerPortfolio;

namespace BoCustomerPortfolio
{
    public class EQSpeculativeTradeBo
    {
        public List<EQSpeculativeVo> GetEquitySpeculativeTradeGroups(DateTime tradeDate)
        {
            List<EQSpeculativeVo> eqSpeculativeVoList = new List<EQSpeculativeVo>();
            EQSpeculativeDao eqSpeculativeDao = new EQSpeculativeDao();
            try
            {
                eqSpeculativeVoList = eqSpeculativeDao.GetEquitySpeculativeTradeGroups(tradeDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EQSpeculativeTradeBo.cs:GetEquitySpeculativeTradeGroups()");


                object[] objects = new object[2];
                objects[0] = eqSpeculativeVoList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return eqSpeculativeVoList;
        }

        public List<EQTransactionVo> GetEquityTransactionsForSpeculativeFlagging(int customerId, int accountId, int scripCode, string brokerCode, string tradeDate,string exchange)
        {
            List<EQTransactionVo> eqTransactionVoList = new List<EQTransactionVo>();
            EQSpeculativeDao eqSpeculativeDao = new EQSpeculativeDao();
            try
            {
                eqTransactionVoList = eqSpeculativeDao.GetEquityTransactionsForSpeculativeFlagging(customerId, accountId, scripCode, brokerCode, tradeDate,exchange);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EQSpeculativeTradeBo.cs:GetEquityTransactionsForSpeculativeFlagging(int customerId, int accountId, int scripCode, string brokerCode, string tradeDate)");


                object[] objects = new object[2];
                objects[0] = eqTransactionVoList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return eqTransactionVoList;
        }

        public void PerformSpeculativeFlagging(List<EQSpeculativeVo> eqSpeculativeVoList)
        {
            EQSpeculativeVo eqSpeculativeVo = new EQSpeculativeVo();
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            List<EQTransactionVo> eqTransactionVoList = new List<EQTransactionVo>();
            int speculativeGroupCount = eqSpeculativeVoList.Count;
            int speculativeTransactionCount = 0;
            float buyQuantity = 0;
            float sellQuantity = 0;

            float cummBuyQuantity = 0;
            float cummSellQuantity = 0;
            int i = 0;
            int j = 0;
            for (i = 0; i < speculativeGroupCount; i++)
            {
                eqTransactionVoList = new List<EQTransactionVo>();
                speculativeTransactionCount = eqSpeculativeVoList[i].EQTransactionVoList.Count;
                for (j = 0; j < speculativeTransactionCount; j++)
                {
                    if (eqSpeculativeVoList[i].EQTransactionVoList[j].BuySell == "B")
                        buyQuantity = buyQuantity + eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity;
                    else
                        sellQuantity = sellQuantity + eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity;
                }
                if (buyQuantity == sellQuantity)
                {
                    for (j = 0; j < speculativeTransactionCount; j++)
                    {
                        eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 1;
                    }
                }
                else if ((buyQuantity > 0 && sellQuantity == 0) || (sellQuantity > 0 && buyQuantity == 0))
                {
                    for (j = 0; j < speculativeTransactionCount; j++)
                    {
                        eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 0;
                    }
                }
                else if (buyQuantity > sellQuantity)
                {
                    cummSellQuantity = sellQuantity;
                    cummBuyQuantity = 0;
                    for (j = 0; j < speculativeTransactionCount; j++)
                    {

                        if (eqSpeculativeVoList[i].EQTransactionVoList[j].BuySell == "S")
                        {
                            eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 1;
                        }
                        else
                        {
                            cummBuyQuantity = cummBuyQuantity + eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity;
                            if (cummBuyQuantity <= sellQuantity)
                            {
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 1;
                                cummSellQuantity = cummSellQuantity - eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity;
                            }
                            else if (cummBuyQuantity > sellQuantity && cummSellQuantity > 0)
                            {

                                eqTransactionVo = new EQTransactionVo();


                                eqTransactionVo = (eqSpeculativeVoList[i].EQTransactionVoList[j]).Clone();

                                eqTransactionVo.IsSpeculative = 1;
                                eqTransactionVo.IsSplit = 1;
                                eqTransactionVo.Quantity = cummSellQuantity;
                                eqTransactionVo.SplitTransactionId = eqTransactionVo.TransactionId;
                                eqTransactionVoList.Add(eqTransactionVo);
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 0;
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSplit = 1;
                                eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity = eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity - cummSellQuantity;
                                cummSellQuantity = 0;
                            }
                            else
                            {
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 0;
                            }
                        }

                    }

                }
                else
                {
                    cummBuyQuantity = buyQuantity;
                    cummSellQuantity = 0;
                    for (j = 0; j < speculativeTransactionCount; j++)
                    {

                        if (eqSpeculativeVoList[i].EQTransactionVoList[j].BuySell == "B")
                        {
                            eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 1;
                        }
                        else
                        {
                            cummSellQuantity = cummSellQuantity + eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity;
                            if (cummSellQuantity <= buyQuantity)
                            {
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 1;
                                cummBuyQuantity = cummBuyQuantity - eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity;
                            }
                            else if (cummSellQuantity > buyQuantity && cummBuyQuantity > 0)
                            {
                                eqTransactionVo = new EQTransactionVo();
                                eqTransactionVo = (eqSpeculativeVoList[i].EQTransactionVoList[j]).Clone();
                                eqTransactionVo.IsSpeculative = 1;
                                eqTransactionVo.IsSplit = 1;
                                eqTransactionVo.Quantity = cummBuyQuantity;
                                eqTransactionVo.SplitTransactionId = eqTransactionVo.TransactionId;
                                eqTransactionVoList.Add(eqTransactionVo);
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 0;
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSplit = 1;
                                eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity = eqSpeculativeVoList[i].EQTransactionVoList[j].Quantity - cummBuyQuantity;
                                cummBuyQuantity = 0;
                            }
                            else
                            {
                                eqSpeculativeVoList[i].EQTransactionVoList[j].IsSpeculative = 0;
                            }
                        }

                    }
                }

                UpdateSpeculativeTrades(eqSpeculativeVoList[i].EQTransactionVoList);
                AddEquityTransaction(eqTransactionVoList, 1665);
            }


        }
        public bool UpdateSpeculativeTrades(List<EQTransactionVo> eqTransactionVoList)
        {
            bool bResult = false;
            int eqTransactionCount = eqTransactionVoList.Count;
            try
            {
                for (int i = 0; i < eqTransactionCount; i++)
                {
                    UpdateSpeculativeTrades(eqTransactionVoList[i]);
                }
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

                FunctionInfo.Add("Method", "EQSpeculativeTradeBo.cs:UpdateSpeculativeTrades(List<EQTransactionVo> eqTransactionVoList)");


                object[] objects = new object[2];
                objects[0] = eqTransactionVoList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool UpdateSpeculativeTrades(EQTransactionVo eqTransactionVo)
        {
            bool bResult = false;

            try
            {
                EQSpeculativeDao eqSpeculativeDao = new EQSpeculativeDao();

                bResult = eqSpeculativeDao.UpdateSpeculativeTrades(eqTransactionVo);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EQSpeculativeTradeBo.cs:UpdateSpeculativeTrades(EQTransactionVo eqTransactionVo)");


                object[] objects = new object[2];
                objects[0] = eqTransactionVo;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool AddEquityTransaction(List<EQTransactionVo> eqTransactionVoList, int userId)
        {
            bool bResult = false;
            int eqTransactionCount = eqTransactionVoList.Count;

            try
            {
                for (int i = 0; i < eqTransactionVoList.Count; i++)
                {
                    AddEquityTransaction(eqTransactionVoList[i], userId);
                }
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

                FunctionInfo.Add("Method", "EQSpeculativeTradeBo.cs:AddEquityTransaction()");


                object[] objects = new object[2];
                objects[0] = eqTransactionVoList;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }
        public bool AddEquityTransaction(EQTransactionVo eqTransactionVo, int userId)
        {
            bool bResult = false;

            try
            {
                EQSpeculativeDao eqSpeculativeDao = new EQSpeculativeDao();

                bResult = eqSpeculativeDao.AddEquityTransaction(eqTransactionVo, userId);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "EQSpeculativeDao.cs:AddEquityTransaction()");


                object[] objects = new object[2];
                objects[0] = eqTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }
    }
}
