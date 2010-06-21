using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VoCustomerPortfolio;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerPortfolio
{
    public class EQSpeculativeDao
    {
        public List<EQSpeculativeVo> GetEquitySpeculativeTradeGroups(DateTime tradeDate)
        {
            List<EQSpeculativeVo> eqSpeculativeVoList = new List<EQSpeculativeVo>();
            EQSpeculativeVo eqSpeculativeVo = new EQSpeculativeVo();
            Database db;
            DbCommand getEquitySpeculativeTradeGroupsCmd;
            DataSet dsEquitySpeculativeTradeGroups;
            DataTable dtEquitySpeculativeTradeGroups;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquitySpeculativeTradeGroupsCmd = db.GetStoredProcCommand("SP_GetEquityTradeGroups");
                db.AddInParameter(getEquitySpeculativeTradeGroupsCmd, "@Trade_Date", DbType.String, tradeDate);

                dsEquitySpeculativeTradeGroups = db.ExecuteDataSet(getEquitySpeculativeTradeGroupsCmd);
                dtEquitySpeculativeTradeGroups = dsEquitySpeculativeTradeGroups.Tables[0];
                eqSpeculativeVoList = new List<EQSpeculativeVo>();
                foreach (DataRow dr in dtEquitySpeculativeTradeGroups.Rows)
                {
                    eqSpeculativeVo = new EQSpeculativeVo();
                    if(dr["C_CustomerId"].ToString()!=null && dr["C_CustomerId"].ToString()!=string.Empty)
                        eqSpeculativeVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    if (dr["PEM_ScripCode"].ToString() != null && dr["PEM_ScripCode"].ToString() != string.Empty)
                        eqSpeculativeVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                    if (dr["TradeDate"].ToString() != null && dr["TradeDate"].ToString() != string.Empty)
                        eqSpeculativeVo.Date = dr["TradeDate"].ToString();
                    if (dr["CETA_AccountId"].ToString() != null && dr["CETA_AccountId"].ToString() != string.Empty)
                        eqSpeculativeVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                    eqSpeculativeVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                    eqSpeculativeVo.Exchange = dr["XE_ExchangeCode"].ToString();
                    eqSpeculativeVo.EQTransactionVoList = GetEquityTransactionsForSpeculativeFlagging(eqSpeculativeVo.CustomerId, eqSpeculativeVo.AccountId, eqSpeculativeVo.ScripCode, eqSpeculativeVo.BrokerCode, eqSpeculativeVo.Date,eqSpeculativeVo.Exchange);

                    eqSpeculativeVoList.Add(eqSpeculativeVo);
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

                FunctionInfo.Add("Method", "EQSpeculativeDao.cs:GetEquitySpeculativeTradeGroups()");


                object[] objects = new object[2];
                objects[0] = eqSpeculativeVoList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return eqSpeculativeVoList;
        }

        public List<EQTransactionVo> GetEquityTransactionsForSpeculativeFlagging(int customerId, int accountId, int scripCode, string brokerCode, string tradeDate, string exchangeCode)
        {
            List<EQTransactionVo> eqTransactionVoList = new List<EQTransactionVo>();
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            Database db;
            DbCommand getEquitySpeculativeTransactionsCmd;
            DataSet dsEquitySpeculativeTransactions;
            DataTable dtEquitySpeculativeTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquitySpeculativeTransactionsCmd = db.GetStoredProcCommand("SP_GetEquityDailyTransactions");
                db.AddInParameter(getEquitySpeculativeTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquitySpeculativeTransactionsCmd, "@CETA_AccountId", DbType.Int32, accountId);
                db.AddInParameter(getEquitySpeculativeTransactionsCmd, "@XB_BrokerCode", DbType.String, brokerCode);
                db.AddInParameter(getEquitySpeculativeTransactionsCmd, "@CET_TradeDate", DbType.String, tradeDate);
                db.AddInParameter(getEquitySpeculativeTransactionsCmd, "@PEM_ScripCode", DbType.Int32, scripCode);
                db.AddInParameter(getEquitySpeculativeTransactionsCmd, "@XE_ExchangeCode", DbType.String, exchangeCode);

                


                dsEquitySpeculativeTransactions = db.ExecuteDataSet(getEquitySpeculativeTransactionsCmd);
                dtEquitySpeculativeTransactions = dsEquitySpeculativeTransactions.Tables[0];
                eqTransactionVoList = new List<EQTransactionVo>();
                foreach (DataRow dr in dtEquitySpeculativeTransactions.Rows)
                {
                    eqTransactionVo = new EQTransactionVo();


                    eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                    eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                    eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                    eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                    if(dr["CET_TradeNum"].ToString()!=null &&dr["CET_TradeNum"].ToString()!=string.Empty)
                        eqTransactionVo.TradeNum = Int64.Parse(dr["CET_TradeNum"].ToString());
                    if (dr["CET_OrderNum"].ToString() != null && dr["CET_OrderNum"].ToString() != string.Empty)
                        eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                    if (dr["CET_BuySell"].ToString() != null && dr["CET_BuySell"].ToString() != string.Empty)
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                    if (dr["CET_IsSpeculative"].ToString() != null && dr["CET_IsSpeculative"].ToString() != string.Empty)
                        eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                    if (dr["XE_ExchangeCode"].ToString() != null && dr["XE_ExchangeCode"].ToString() != string.Empty)
                        eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                    if (dr["CET_TradeDate"].ToString() != null && dr["CET_TradeDate"].ToString() != string.Empty)
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                    if (dr["CET_Rate"].ToString() != null && dr["CET_Rate"].ToString() != string.Empty)
                        eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                    if (dr["CET_Quantity"].ToString() != null && dr["CET_Quantity"].ToString() != string.Empty)
                        eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                    if (dr["CET_Brokerage"].ToString() != null && dr["CET_Brokerage"].ToString() != string.Empty)
                        eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                    if (dr["CET_ServiceTax"].ToString() != null && dr["CET_ServiceTax"].ToString() != string.Empty)
                        eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                    if (dr["CET_EducationCess"].ToString() != null && dr["CET_EducationCess"].ToString() != string.Empty)
                        eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                    if (dr["CET_STT"].ToString() != null && dr["CET_STT"].ToString() != string.Empty)
                        eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                    if (dr["CET_OtherCharges"].ToString() != null && dr["CET_OtherCharges"].ToString() != string.Empty)
                        eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                    if (dr["CET_RateInclBrokerage"].ToString() != null && dr["CET_RateInclBrokerage"].ToString() != string.Empty)
                        eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                    if (dr["CET_TradeTotal"].ToString() != null && dr["CET_TradeTotal"].ToString() != string.Empty)
                        eqTransactionVo.TradeTotal = float.Parse(dr["CET_TradeTotal"].ToString());
                    if (dr["XB_BrokerCode"].ToString() != null && dr["XB_BrokerCode"].ToString() != string.Empty)
                        eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                    if (dr["CET_IsSplit"].ToString() != null && dr["CET_IsSplit"].ToString() != string.Empty)
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                    if (dr["CET_SplitCustEqTransId"].ToString() != null && dr["CET_SplitCustEqTransId"].ToString() != string.Empty)
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                    if (dr["XES_SourceCode"].ToString() != null && dr["XES_SourceCode"].ToString() != string.Empty)
                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                    if (dr["WETT_TransactionCode"].ToString() != null && dr["WETT_TransactionCode"].ToString() != string.Empty)
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                    if (dr["WETT_TransactionTypeName"].ToString() != null && dr["WETT_TransactionTypeName"].ToString() != string.Empty)
                        eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                    if (dr["CET_IsSourceManual"].ToString() != null && dr["CET_IsSourceManual"].ToString() != string.Empty)
                        eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());




                    eqTransactionVoList.Add(eqTransactionVo);
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

                FunctionInfo.Add("Method", "EQSpeculativeDao.cs:GetEquityTransactionsForSpeculativeFlagging(int customerId, int accountId, string brokerCode, string tradeDate)");


                object[] objects = new object[2];
                objects[0] = eqTransactionVoList;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionVoList;
        }
        public bool UpdateSpeculativeTrades(EQTransactionVo eqTransactionVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateSpeculativeTradesCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateSpeculativeTradesCmd = db.GetStoredProcCommand("SP_UpdateEquitySpeculativeTrades");
                db.AddInParameter(updateSpeculativeTradesCmd, "@CET_EqTransId", DbType.Int32, eqTransactionVo.TransactionId);
                db.AddInParameter(updateSpeculativeTradesCmd, "@CET_IsSpeculative", DbType.Int16, eqTransactionVo.IsSpeculative);
                db.AddInParameter(updateSpeculativeTradesCmd, "@CET_Quantity", DbType.Decimal, eqTransactionVo.Quantity);
                db.AddInParameter(updateSpeculativeTradesCmd, "@CET_IsSplit", DbType.Int16, eqTransactionVo.IsSplit);
                db.AddInParameter(updateSpeculativeTradesCmd, "@CET_SplitCustEqTransId", DbType.Int32, eqTransactionVo.SplitTransactionId);

                db.ExecuteNonQuery(updateSpeculativeTradesCmd);

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

                FunctionInfo.Add("Method", "EQSpeculativeDao.cs:UpdateSpeculativeTrades(EQTransactionVo eqTransactionVo)");


                object[] objects = new object[2];
                objects[0] = eqTransactionVo;


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
            Database db;
            DbCommand createEquityTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createEquityTransactionCmd = db.GetStoredProcCommand("SP_AddEquityTransaction");

                db.AddInParameter(createEquityTransactionCmd, "@CETA_AccountId", DbType.Int32, eqTransactionVo.AccountId);
                db.AddInParameter(createEquityTransactionCmd, "@PEM_ScripCode", DbType.String, eqTransactionVo.ScripCode);
                db.AddInParameter(createEquityTransactionCmd, "@CET_TradeNum", DbType.Int64, eqTransactionVo.TradeNum);
                db.AddInParameter(createEquityTransactionCmd, "@CET_OrderNum", DbType.Int64, eqTransactionVo.OrderNum);
                db.AddInParameter(createEquityTransactionCmd, "@CET_BuySell", DbType.String, eqTransactionVo.BuySell);
                db.AddInParameter(createEquityTransactionCmd, "@CET_IsSpeculative", DbType.Int16, eqTransactionVo.IsSpeculative);

                if(eqTransactionVo.Exchange=="")
                    db.AddInParameter(createEquityTransactionCmd, "@XE_ExchangeCode", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@XE_ExchangeCode", DbType.String, eqTransactionVo.Exchange);
                db.AddInParameter(createEquityTransactionCmd, "@CET_TradeDate", DbType.DateTime, eqTransactionVo.TradeDate);
                db.AddInParameter(createEquityTransactionCmd, "@CET_Rate", DbType.Decimal, eqTransactionVo.Rate);
                db.AddInParameter(createEquityTransactionCmd, "@CET_Quantity", DbType.Decimal, eqTransactionVo.Quantity);
                db.AddInParameter(createEquityTransactionCmd, "@CET_Brokerage", DbType.Decimal, eqTransactionVo.Brokerage);
                db.AddInParameter(createEquityTransactionCmd, "@CET_ServiceTax", DbType.Decimal, eqTransactionVo.ServiceTax);
                db.AddInParameter(createEquityTransactionCmd, "@CET_EducationCess", DbType.Decimal, eqTransactionVo.EducationCess);
                db.AddInParameter(createEquityTransactionCmd, "@CET_STT", DbType.Decimal, eqTransactionVo.STT);
                db.AddInParameter(createEquityTransactionCmd, "@CET_OtherCharges", DbType.Decimal, eqTransactionVo.OtherCharges);
                db.AddInParameter(createEquityTransactionCmd, "@CET_RateInclBrokerage", DbType.Decimal, eqTransactionVo.RateInclBrokerage);
                db.AddInParameter(createEquityTransactionCmd, "@CET_TradeTotal", DbType.Decimal, eqTransactionVo.TradeTotal);
                if(eqTransactionVo.BrokerCode=="")
                    db.AddInParameter(createEquityTransactionCmd, "@XB_BrokerCode", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@XB_BrokerCode", DbType.String, eqTransactionVo.BrokerCode);
                db.AddInParameter(createEquityTransactionCmd, "@CET_IsSplit", DbType.Int16, eqTransactionVo.IsSplit);
                db.AddInParameter(createEquityTransactionCmd, "@CET_SplitCustEqTransId", DbType.Int32, eqTransactionVo.SplitTransactionId);
                if (eqTransactionVo.SourceCode == "")
                    db.AddInParameter(createEquityTransactionCmd, "@XES_SourceCode", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@XES_SourceCode", DbType.String, eqTransactionVo.SourceCode);

                db.AddInParameter(createEquityTransactionCmd, "@WETT_TransactionCode", DbType.Int16, eqTransactionVo.TransactionCode);
                db.AddInParameter(createEquityTransactionCmd, "@CET_IsSourceManual", DbType.Int16, eqTransactionVo.IsSourceManual);
                db.AddInParameter(createEquityTransactionCmd, "@CET_ModifiedBy", DbType.String, userId);
                db.AddInParameter(createEquityTransactionCmd, "@CET_CreatedBy", DbType.String, userId);

                db.AddOutParameter(createEquityTransactionCmd, "@CET_EqTransId", DbType.Int32, 5000);
                db.ExecuteNonQuery(createEquityTransactionCmd);
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
