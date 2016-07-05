using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using VoCustomerPortfolio;
using VoUser;
using VoCustomerProfiling;

namespace DaoCustomerPortfolio
{
    /// <summary>
    /// alteration in the stored procedure --- SP_GetCustomerMFFolios
    /// </summary>
    public class CustomerTransactionDao
    {
        #region Equity Transactions
        public List<CustomerAccountsVo> GetCustomerEQAccount(int PortfolioId)
        {
            DataSet ds = null;
            Database db;
            DbCommand getCustomerEQAccCmd;
            List<CustomerAccountsVo> AccountList = null;
            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            DataTable dtEQAcc = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerEQAccCmd = db.GetStoredProcCommand("SP_GetCustomerTradeAccounts");
                db.AddInParameter(getCustomerEQAccCmd, "@PortfolioId", DbType.Int32, PortfolioId);

                ds = db.ExecuteDataSet(getCustomerEQAccCmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtEQAcc = ds.Tables[0];
                    AccountList = new List<CustomerAccountsVo>();
                    foreach (DataRow dr in dtEQAcc.Rows)
                    {
                        AccountVo = new CustomerAccountsVo();
                        AccountVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        AccountVo.BrokerName = dr["XB_BrokerName"].ToString();
                        AccountVo.TradeNum = dr["CETA_TradeAccountNum"].ToString();
                        if (dr["CETA_BrokerDeliveryPercentage"].ToString() != "" && dr["CETA_BrokerSpeculativePercentage"].ToString() != "" && dr["CETA_OtherCharges"].ToString() != "")
                        {
                            AccountVo.BrokerageDeliveryPercentage = double.Parse(dr["CETA_BrokerDeliveryPercentage"].ToString());
                            AccountVo.BrokerageSpeculativePercentage = double.Parse(dr["CETA_BrokerSpeculativePercentage"].ToString());
                            AccountVo.OtherCharges = double.Parse(dr["CETA_OtherCharges"].ToString());
                        }
                        else
                        {
                            AccountVo.BrokerageDeliveryPercentage = 0.0;
                            AccountVo.BrokerageSpeculativePercentage = 0.0;
                            AccountVo.OtherCharges = 0.0;

                        }
                        if (dr["CETA_AccountOpeningDate"].ToString() != string.Empty)
                            AccountVo.AccountOpeningDate = DateTime.Parse(dr["CETA_AccountOpeningDate"].ToString());
                        else
                            AccountVo.AccountOpeningDate = DateTime.MinValue;

                        AccountList.Add(AccountVo);

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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolios()");
                object[] objects = new object[2];
                objects[0] = PortfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return AccountList;
        }
        
        public bool AddEquityTransaction(EQTransactionVo eqTransactionVo, int userId)
        {
            bool bResult = false;
            int transactionId = 0;
            Database db;
            DbCommand createEquityTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createEquityTransactionCmd = db.GetStoredProcCommand("SP_AddEquityTransaction");

                db.AddInParameter(createEquityTransactionCmd, "@CETA_AccountId", DbType.Int32, eqTransactionVo.AccountId);
                if (eqTransactionVo.ScripCode != 0)
                    db.AddInParameter(createEquityTransactionCmd, "@PEM_ScripCode", DbType.String, eqTransactionVo.ScripCode);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@PEM_ScripCode", DbType.String, DBNull.Value);

                db.AddInParameter(createEquityTransactionCmd, "@CET_TradeNum", DbType.Int64, eqTransactionVo.TradeNum);
                db.AddInParameter(createEquityTransactionCmd, "@CET_OrderNum", DbType.Int64, eqTransactionVo.OrderNum);
                db.AddInParameter(createEquityTransactionCmd, "@CET_BuySell", DbType.String, eqTransactionVo.BuySell);
                db.AddInParameter(createEquityTransactionCmd, "@CET_IsSpeculative", DbType.Int16, eqTransactionVo.IsSpeculative);
                db.AddInParameter(createEquityTransactionCmd, "@XE_ExchangeCode", DbType.String, eqTransactionVo.Exchange);
                if (eqTransactionVo.TradeDate != DateTime.MinValue)
                    db.AddInParameter(createEquityTransactionCmd, "@CET_TradeDate", DbType.DateTime, eqTransactionVo.TradeDate);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@CET_TradeDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(createEquityTransactionCmd, "@CET_Rate", DbType.Decimal, eqTransactionVo.Rate);
                db.AddInParameter(createEquityTransactionCmd, "@CET_Quantity", DbType.Decimal, eqTransactionVo.Quantity);
                db.AddInParameter(createEquityTransactionCmd, "@CET_Brokerage", DbType.Decimal, eqTransactionVo.Brokerage);
                db.AddInParameter(createEquityTransactionCmd, "@CET_ServiceTax", DbType.Decimal, eqTransactionVo.ServiceTax);
                db.AddInParameter(createEquityTransactionCmd, "@CET_EducationCess", DbType.Decimal, eqTransactionVo.EducationCess);
                db.AddInParameter(createEquityTransactionCmd, "@CET_STT", DbType.Decimal, eqTransactionVo.STT);
                db.AddInParameter(createEquityTransactionCmd, "@CET_OtherCharges", DbType.Decimal, eqTransactionVo.OtherCharges);
                db.AddInParameter(createEquityTransactionCmd, "@CET_RateInclBrokerage", DbType.Decimal, eqTransactionVo.RateInclBrokerage);
                db.AddInParameter(createEquityTransactionCmd, "@CET_TradeTotal", DbType.Decimal, eqTransactionVo.TradeTotal);
                db.AddInParameter(createEquityTransactionCmd, "@XB_BrokerCode", DbType.String, eqTransactionVo.BrokerCode);
                db.AddInParameter(createEquityTransactionCmd, "@CET_IsSplit", DbType.Int16, eqTransactionVo.IsSplit);
                db.AddInParameter(createEquityTransactionCmd, "@CET_SplitCustEqTransId", DbType.Int32, eqTransactionVo.SplitTransactionId);
                db.AddInParameter(createEquityTransactionCmd, "@XES_SourceCode", DbType.String, eqTransactionVo.SourceCode);
                if (eqTransactionVo.TransactionCode != 0)
                    db.AddInParameter(createEquityTransactionCmd, "@WETT_TransactionCode", DbType.Int16, eqTransactionVo.TransactionCode);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@WETT_TransactionCode", DbType.Int16, DBNull.Value);
                db.AddInParameter(createEquityTransactionCmd, "@CET_IsSourceManual", DbType.Int16, eqTransactionVo.IsSourceManual);
                db.AddInParameter(createEquityTransactionCmd, "@CET_ModifiedBy", DbType.String, userId);
                db.AddInParameter(createEquityTransactionCmd, "@CET_CreatedBy", DbType.String, userId);
                db.AddInParameter(createEquityTransactionCmd, "@CET_ManagedBy", DbType.Int32, eqTransactionVo.ManagedBy);
                db.AddInParameter(createEquityTransactionCmd, "@CET_IsTradeType", DbType.Int32, eqTransactionVo.Type);
                if (eqTransactionVo.DemateAccountId != 0)
                    db.AddInParameter(createEquityTransactionCmd, "@CEDA_DematAccountId", DbType.Int32, eqTransactionVo.DemateAccountId);
                else
                {
                    db.AddInParameter(createEquityTransactionCmd, "@CEDA_DematAccountId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(createEquityTransactionCmd, "@CET_BillNo", DbType.String, eqTransactionVo.BillNo);
                db.AddInParameter(createEquityTransactionCmd, "@CET_SettlementNo", DbType.String, eqTransactionVo.SettlementNo);
                if (eqTransactionVo.SettlementDate != DateTime.MinValue)
                    db.AddInParameter(createEquityTransactionCmd, "@CET_SettlementDate", DbType.DateTime, eqTransactionVo.SettlementDate);
                else
                {
                    db.AddInParameter(createEquityTransactionCmd, "@CET_SettlementDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(createEquityTransactionCmd, "@CET_SebiTurnOverFee", DbType.Decimal, eqTransactionVo.SebiTurnOverFee);
                db.AddInParameter(createEquityTransactionCmd, "@CET_TrxnCharges", DbType.Decimal, eqTransactionVo.TransactionCharges);
                db.AddInParameter(createEquityTransactionCmd, "@CET_StampCharges", DbType.Decimal, eqTransactionVo.StampCharges);
                db.AddInParameter(createEquityTransactionCmd, "@CET_NoOfSharesEligibleForDiv", DbType.Decimal, eqTransactionVo.NoOfSharesForDiv);
                db.AddInParameter(createEquityTransactionCmd, "@CET_DifferenceInBrokerage", DbType.Decimal, eqTransactionVo.DifferenceInBrokerage);
                if (eqTransactionVo.TransactionCode == 6)
                    db.AddInParameter(createEquityTransactionCmd, "@CET_DividendRecieved", DbType.Boolean, eqTransactionVo.DividendRecieved);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@CET_DividendRecieved", DbType.Int32, DBNull.Value);
                db.AddInParameter(createEquityTransactionCmd, "@CET_DematCharge", DbType.Decimal, eqTransactionVo.DematCharge);
                db.AddInParameter(createEquityTransactionCmd, "CET_TrTotalIncBrokerage", DbType.Decimal, eqTransactionVo.TradeTotalIncBrokerage);
                db.AddInParameter(createEquityTransactionCmd, "@CET_RateIncBrokerageAllCharges", DbType.Decimal, eqTransactionVo.RateIncBrokerageAllCharges);
                db.AddInParameter(createEquityTransactionCmd, "@CET_BankReferenceNo", DbType.String, eqTransactionVo.BankReferenceNo);
                if (eqTransactionVo.DailyCorpAxnId != 0)
                    db.AddInParameter(createEquityTransactionCmd, "@PECA_DailyCorpAxnId", DbType.Int16, eqTransactionVo.DailyCorpAxnId);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@PECA_DailyCorpAxnId", DbType.Int32, DBNull.Value);
                db.AddInParameter(createEquityTransactionCmd, "@CET_GrossConsideration", DbType.Decimal, eqTransactionVo.GrossConsideration);


                db.AddOutParameter(createEquityTransactionCmd, "CET_EqTransId", DbType.Int32, 5000);
                db.AddInParameter(createEquityTransactionCmd, "@FXCurencyType", DbType.String, eqTransactionVo.FXCurencyType);
                db.AddInParameter(createEquityTransactionCmd, "@FXCurencyRate", DbType.Decimal, eqTransactionVo.FXCurencyRate);
                if (eqTransactionVo.TransactionCode != 19)
                    db.AddInParameter(createEquityTransactionCmd, "@CET_Remark", DbType.String, eqTransactionVo.Remark);
                else
                    db.AddInParameter(createEquityTransactionCmd, "@CET_Remark", DbType.String, DBNull.Value);
                db.ExecuteNonQuery(createEquityTransactionCmd);

                transactionId = int.Parse(db.GetParameterValue(createEquityTransactionCmd, "CET_EqTransId").ToString());

                if (transactionId > 0)
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:AddEquityTransaction()");


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


        public List<EQTransactionVo> GetEquityTransactions(int customerId,
                                                            int portfolioId,
                                                            DateTime FromDate, DateTime ToDate
                                                            )
        {
            List<EQTransactionVo> eqTransactionsList = null;
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            Database db;
            DbCommand getEquityTransactionsCmd;
            DataSet dsGetEquityTransactions;
            DataTable dtGetEquityTransactions;

            //genDictTranType = new Dictionary<string, string>();
            //genDictExchange = new Dictionary<string, string>();
            //genDictTradeDate = new Dictionary<string, string>();

            //Count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerEquityTransactions");
                db.AddInParameter(getEquityTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                //db.AddInParameter(getEquityTransactionsCmd, "@currentPage", DbType.Int32, currentPage);
                db.AddInParameter(getEquityTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                //db.AddInParameter(getEquityTransactionsCmd, "@export", DbType.Int32, export);
                //if (ScripFilter != "")
                //    db.AddInParameter(getEquityTransactionsCmd, "@scripFilter", DbType.String, ScripFilter);
                //else
                //    db.AddInParameter(getEquityTransactionsCmd, "@scripFilter", DbType.String, DBNull.Value);
                //if (TradeNumFilter != "")
                //    db.AddInParameter(getEquityTransactionsCmd, "@tradeNumFilter", DbType.String, TradeNumFilter);
                //else
                //    db.AddInParameter(getEquityTransactionsCmd, "@tradeNumFilter", DbType.String, DBNull.Value);
                //if (ExchangeFilter != "")
                //    db.AddInParameter(getEquityTransactionsCmd, "@exchangeFilter", DbType.String, ExchangeFilter);
                //else
                //    db.AddInParameter(getEquityTransactionsCmd, "@exchangeFilter", DbType.String, DBNull.Value);
                //if (TradeDateFilter != "")
                //    db.AddInParameter(getEquityTransactionsCmd, "@tradeDateFilter", DbType.String, TradeDateFilter);
                //else
                //    db.AddInParameter(getEquityTransactionsCmd, "@tradeDateFilter", DbType.String, DBNull.Value);
                //if (TranTypeFilter != "")
                //    db.AddInParameter(getEquityTransactionsCmd, "@tranTypeFilter", DbType.String, TranTypeFilter);
                //else
                //    db.AddInParameter(getEquityTransactionsCmd, "@tranTypeFilter", DbType.String, DBNull.Value);

                //db.AddInParameter(getEquityTransactionsCmd, "@sortExpression", DbType.String, SortExpression);
                db.AddInParameter(getEquityTransactionsCmd, "@fromDate", DbType.DateTime, FromDate);
                db.AddInParameter(getEquityTransactionsCmd, "@toDate", DbType.DateTime, ToDate);
                dsGetEquityTransactions = db.ExecuteDataSet(getEquityTransactionsCmd);
                if (dsGetEquityTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransactions = dsGetEquityTransactions.Tables[0];
                    eqTransactionsList = new List<EQTransactionVo>();
                    foreach (DataRow dr in dtGetEquityTransactions.Rows)
                    {
                        eqTransactionVo = new EQTransactionVo();
                        eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                        eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        eqTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                        eqTransactionVo.Ticker = dr["PEM_Ticker"].ToString();
                        eqTransactionVo.TradeAccountNum = (dr["CETA_TradeAccountNum"].ToString());
                        eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                        eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                        if (dr["CET_IsSpeculative"].ToString() == "1")
                            eqTransactionVo.TradeType = "S";
                        else
                            eqTransactionVo.TradeType = "D";

                        eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                        eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                        eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                        eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                        eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                        eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                        eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                        eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        eqTransactionVo.TradeTotal = double.Parse(dr["CET_TradeTotal"].ToString());
                        eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_IsSourceManual"].ToString()))
                            eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());

                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                        eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                        eqTransactionVo.IsCorpAction = int.Parse(dr["WETT_IsCorpAxn"].ToString());
                        eqTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                        eqTransactionsList.Add(eqTransactionVo);
                    }
                }

                //if (dsGetEquityTransactions.Tables[2].Rows.Count > 0)
                //{
                //    string type = string.Empty;
                //    string mode = string.Empty;
                //    string tranType = string.Empty;

                //    foreach (DataRow dr in dsGetEquityTransactions.Tables[2].Rows)
                //    {
                //        if (dr["CET_IsSpeculative"].ToString() == "1")
                //            mode = "Speculation";
                //        else if (dr["CET_IsSpeculative"].ToString() == "0")
                //            mode = "Delivery";
                //        if (dr["WETT_TransactionCode"].ToString() == "1")
                //            type = "Buy";
                //        else if (dr["WETT_TransactionCode"].ToString() == "2")
                //            type = "Sell";
                //        else if (dr["WETT_TransactionCode"].ToString() == "13")
                //            type = "Holdings";
                //        tranType = type + "/" + mode;

                //        //if (!genDictTranType.ContainsKey(tranType))
                //        //{
                //        //    genDictTranType.Add(tranType, tranType);
                //        //}
                //    }
                //}

                //if (dsGetEquityTransactions.Tables[3].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsGetEquityTransactions.Tables[3].Rows)
                //    {
                //        genDictExchange.Add(dr["XE_ExchangeCode"].ToString(), dr["XE_ExchangeCode"].ToString());
                //    }
                //}

                //if (dsGetEquityTransactions.Tables[4].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsGetEquityTransactions.Tables[4].Rows)
                //    {
                //        genDictTradeDate.Add(DateTime.Parse(dr["CET_TradeDate"].ToString()).ToShortDateString(), DateTime.Parse(dr["CET_TradeDate"].ToString()).ToShortDateString());
                //    }
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityTransactions()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            // if (dsGetEquityTransactions.Tables[1].Rows.Count > 0)
            // Count = Int32.Parse(dsGetEquityTransactions.Tables[1].Rows[0]["CNT"].ToString());

            return eqTransactionsList;
        }

        public List<EQTransactionVo> GetEquityTransactions(int customerId, int portfolioId)
        {
            List<EQTransactionVo> eqTransactionsList = null;
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            Database db;
            DbCommand getEquityTransactionsCmd;
            DataSet dsGetEquityTransactions;
            DataTable dtGetEquityTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioEquityTransactions");
                db.AddInParameter(getEquityTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquityTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);

                dsGetEquityTransactions = db.ExecuteDataSet(getEquityTransactionsCmd);
                if (dsGetEquityTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransactions = dsGetEquityTransactions.Tables[0];
                    eqTransactionsList = new List<EQTransactionVo>();
                    foreach (DataRow dr in dtGetEquityTransactions.Rows)
                    {
                        eqTransactionVo = new EQTransactionVo();
                        eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                        eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        eqTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                        eqTransactionVo.Ticker = dr["PEM_Ticker"].ToString();
                        eqTransactionVo.TradeAccountNum = (dr["CET_TradeNum"].ToString());
                        eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                        eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                        if (dr["CET_IsSpeculative"].ToString() == "1")
                            eqTransactionVo.TradeType = "S";
                        else
                            eqTransactionVo.TradeType = "D";

                        eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                        eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                        eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                        eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                        eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                        eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                        eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                        eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        eqTransactionVo.TradeTotal = float.Parse(dr["CET_TradeTotal"].ToString());
                        eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_IsSourceManual"].ToString()))
                            eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());
                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                        eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                        eqTransactionVo.IsCorpAction = int.Parse(dr["WETT_IsCorpAxn"].ToString());
                        eqTransactionsList.Add(eqTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityTransactions()");


                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }



            return eqTransactionsList;
        }

        public List<EQTransactionVo> GetEquityTransactions(int customerId, int eqCode, DateTime tradeDate)
        {
            List<EQTransactionVo> eqTransactionsList = null;

            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            Database db;
            DbCommand getEquityTransactionsCmd;
            DataSet dsGetEquityTransactions;
            DataTable dtGetEquityTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerEquitySpecificTransactions");
                db.AddInParameter(getEquityTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquityTransactionsCmd, "@PEM_ScripCode", DbType.Int32, eqCode);
                db.AddInParameter(getEquityTransactionsCmd, "@CET_TradeDate", DbType.DateTime, tradeDate);
                dsGetEquityTransactions = db.ExecuteDataSet(getEquityTransactionsCmd);
                if (dsGetEquityTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransactions = dsGetEquityTransactions.Tables[0];
                    eqTransactionsList = new List<EQTransactionVo>();
                    foreach (DataRow dr in dtGetEquityTransactions.Rows)
                    {
                        eqTransactionVo = new EQTransactionVo();
                        eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                        eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        eqTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                        eqTransactionVo.Ticker = dr["PEM_Ticker"].ToString();
                        eqTransactionVo.TradeAccountNum = (dr["CET_TradeNum"].ToString());
                        eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                        eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                        if (dr["CET_IsSpeculative"].ToString() == "1")
                            eqTransactionVo.TradeType = "S";
                        else
                            eqTransactionVo.TradeType = "D";

                        eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                        eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                        eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                        eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                        eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                        eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                        eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                        eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        eqTransactionVo.TradeTotal = float.Parse(dr["CET_TradeTotal"].ToString());
                        eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_IsSourceManual"].ToString()))
                            eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());
                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                        eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                        eqTransactionVo.IsCorpAction = int.Parse(dr["WETT_IsCorpAxn"].ToString());

                        eqTransactionsList.Add(eqTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eqCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionsList;
        }

        public List<EQTransactionVo> GetEquityTransactions(int customerId, int portfolioId, int eqCode, DateTime tradeDate, int accountId)
        {
            List<EQTransactionVo> eqTransactionsList = null;

            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            Database db;
            DbCommand getEquityTransactionsCmd;
            DataSet dsGetEquityTransactions;
            DataTable dtGetEquityTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioEquitySpecificTransactions");
                db.AddInParameter(getEquityTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquityTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getEquityTransactionsCmd, "@PEM_ScripCode", DbType.Int32, eqCode);
                db.AddInParameter(getEquityTransactionsCmd, "@CET_TradeDate", DbType.DateTime, tradeDate);
                db.AddInParameter(getEquityTransactionsCmd, "@AccountId", DbType.Int32, accountId);
                dsGetEquityTransactions = db.ExecuteDataSet(getEquityTransactionsCmd);
                if (dsGetEquityTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransactions = dsGetEquityTransactions.Tables[0];
                    eqTransactionsList = new List<EQTransactionVo>();
                    foreach (DataRow dr in dtGetEquityTransactions.Rows)
                    {
                        eqTransactionVo = new EQTransactionVo();
                        eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                        eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        eqTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                        eqTransactionVo.Ticker = dr["PEM_Ticker"].ToString();
                        eqTransactionVo.TradeAccountNum = (dr["CET_TradeNum"].ToString());
                        eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                        if (dr["CET_IsSpeculative"].ToString() != String.Empty)
                        {
                            eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                            if (dr["CET_IsSpeculative"].ToString() == "1")
                                eqTransactionVo.TradeType = "S";
                            else
                                eqTransactionVo.TradeType = "D";
                        }
                        else
                        {
                            eqTransactionVo.IsSpeculative = 0;
                            eqTransactionVo.TradeType = "D";
                        }
                        eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                        eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                        eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                        eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                        eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                        eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                        eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                        eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        eqTransactionVo.TradeTotal = float.Parse(dr["CET_TradeTotal"].ToString());
                        eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_IsSourceManual"].ToString()))
                            eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());

                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                        eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                        eqTransactionVo.IsCorpAction = int.Parse(dr["WETT_IsCorpAxn"].ToString());
                        eqTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();

                        eqTransactionsList.Add(eqTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = eqCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqTransactionsList;
        }

        public EQTransactionVo GetEquityTransaction(int eqTransactionId)
        {
            EQTransactionVo eqTransactionVo = null;

            Database db;
            DbCommand getEquityTransactionCmd;
            DataSet dsGetEquityTransaction;
            DataTable dtGetEquityTransaction;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionCmd = db.GetStoredProcCommand("SP_GetCustomerEquityTransaction");
                db.AddInParameter(getEquityTransactionCmd, "@CET_EqTransId", DbType.Int32, eqTransactionId);
                dsGetEquityTransaction = db.ExecuteDataSet(getEquityTransactionCmd);
                if (dsGetEquityTransaction.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransaction = dsGetEquityTransaction.Tables[0];

                    eqTransactionVo = new EQTransactionVo();

                    foreach (DataRow dr in dtGetEquityTransaction.Rows)
                    {
                        eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                        //eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        //eqTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        //eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                        //eqTransactionVo.Ticker = dr["PEM_Ticker"].ToString();
                        eqTransactionVo.TradeAccountNum = (dr["CET_TradeNum"].ToString());
                        eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                        eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                        if (dr["CET_IsSpeculative"].ToString() == "1")
                            eqTransactionVo.TradeType = "S";
                        else
                            eqTransactionVo.TradeType = "D";

                        eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                        eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                        eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                        eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                        eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                        eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                        eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                        eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        eqTransactionVo.TradeTotal = float.Parse(dr["CET_TradeTotal"].ToString());
                        eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_IsSourceManual"].ToString()))
                            eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());
                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                        //eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                        //eqTransactionVo.IsCorpAction = int.Parse(dr["WETT_IsCorpAxn"].ToString());

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityTransaction()");


                object[] objects = new object[1];
                objects[0] = eqTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return eqTransactionVo;
        }

        public DataSet PopulateDDExchange(int portfolioId)
        {
            Database db;
            DbCommand populateDDExchangeCmd;
            DataSet dsPopulateDDExchange = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                populateDDExchangeCmd = db.GetStoredProcCommand("SP_PopulateDDExchange");
                db.AddInParameter(populateDDExchangeCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                dsPopulateDDExchange = db.ExecuteDataSet(populateDDExchangeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:PopulateDDExchange()");

                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsPopulateDDExchange;
        }

        public bool UpdateEquityTransaction(EQTransactionVo eqTransactionVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateEQTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateEQTransactionCmd = db.GetStoredProcCommand("SP_UpdateEquityTransaction");

                db.AddInParameter(updateEQTransactionCmd, "CET_EqTransId", DbType.Int32, eqTransactionVo.TransactionId);
                db.AddInParameter(updateEQTransactionCmd, "@CETA_AccountId", DbType.Int32, eqTransactionVo.AccountId);
                db.AddInParameter(updateEQTransactionCmd, "@PEM_ScripCode", DbType.String, eqTransactionVo.ScripCode);
                if (eqTransactionVo.TradeAccountNum != "")
                    db.AddInParameter(updateEQTransactionCmd, "@CET_TradeNum", DbType.Decimal, eqTransactionVo.TradeAccountNum);
                else
                    db.AddInParameter(updateEQTransactionCmd, "@CET_TradeNum", DbType.Int64, DBNull.Value);

                db.AddInParameter(updateEQTransactionCmd, "@CET_OrderNum", DbType.Decimal, eqTransactionVo.OrderNum);
                db.AddInParameter(updateEQTransactionCmd, "@CET_BuySell", DbType.String, eqTransactionVo.BuySell);
                db.AddInParameter(updateEQTransactionCmd, "@CET_IsSpeculative", DbType.Int16, eqTransactionVo.IsSpeculative);
                db.AddInParameter(updateEQTransactionCmd, "@XE_ExchangeCode", DbType.String, eqTransactionVo.Exchange);
                db.AddInParameter(updateEQTransactionCmd, "@CET_TradeDate", DbType.DateTime, eqTransactionVo.TradeDate);
                db.AddInParameter(updateEQTransactionCmd, "@CET_Rate", DbType.Decimal, eqTransactionVo.Rate);
                db.AddInParameter(updateEQTransactionCmd, "@CET_Quantity", DbType.Decimal, eqTransactionVo.Quantity);
                db.AddInParameter(updateEQTransactionCmd, "@CET_Brokerage", DbType.Decimal, eqTransactionVo.Brokerage);
                db.AddInParameter(updateEQTransactionCmd, "@CET_ServiceTax", DbType.Decimal, eqTransactionVo.ServiceTax);
                db.AddInParameter(updateEQTransactionCmd, "@CET_EducationCess", DbType.Decimal, eqTransactionVo.EducationCess);
                db.AddInParameter(updateEQTransactionCmd, "@CET_STT", DbType.Decimal, eqTransactionVo.STT);
                db.AddInParameter(updateEQTransactionCmd, "@CET_OtherCharges", DbType.Decimal, eqTransactionVo.OtherCharges);
                db.AddInParameter(updateEQTransactionCmd, "@CET_RateInclBrokerage", DbType.Decimal, eqTransactionVo.RateInclBrokerage);
                db.AddInParameter(updateEQTransactionCmd, "@CET_TradeTotal", DbType.Decimal, eqTransactionVo.TradeTotal);
                db.AddInParameter(updateEQTransactionCmd, "@XB_BrokerCode", DbType.String, eqTransactionVo.BrokerCode);
                db.AddInParameter(updateEQTransactionCmd, "@CET_IsSplit", DbType.Int16, eqTransactionVo.IsSplit);
                db.AddInParameter(updateEQTransactionCmd, "@CET_SplitCustEqTransId", DbType.Int32, eqTransactionVo.SplitTransactionId);
                db.AddInParameter(updateEQTransactionCmd, "@XES_SourceCode", DbType.String, eqTransactionVo.SourceCode);
                db.AddInParameter(updateEQTransactionCmd, "@WETT_TransactionCode", DbType.Int16, eqTransactionVo.TransactionCode);
                db.AddInParameter(updateEQTransactionCmd, "@CET_IsSourceManual", DbType.Int16, eqTransactionVo.IsSourceManual);
                db.AddInParameter(updateEQTransactionCmd, "@CET_ModifiedBy", DbType.String, userId);
                if (db.ExecuteNonQuery(updateEQTransactionCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateEquityTransaction()");


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

        public bool DeleteEQTransaction(int eqTransactionId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteEquityTransactionCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteEquityTransactionCmd = db.GetStoredProcCommand("SP_DeleteEQTransactions");

                db.AddInParameter(deleteEquityTransactionCmd, "@CET_EqTransId", DbType.Int32, eqTransactionId);
                if (db.ExecuteNonQuery(deleteEquityTransactionCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:DeleteEQTransaction()");

                object[] objects = new object[1];
                objects[0] = eqTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public CustomerAccountsVo GetCustomerEQAccountDetails(int AccountId, int PortfolioId)
        {

            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            Database db;
            DbCommand getEQTransactionCmd;
            DataSet dsGetEQTransaction;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEQTransactionCmd = db.GetStoredProcCommand("SP_GetCustomerTradeAccountDetails");
                db.AddInParameter(getEQTransactionCmd, "@AccountId", DbType.Int32, AccountId);
                db.AddInParameter(getEQTransactionCmd, "@PortfolioId", DbType.Int32, PortfolioId);

                dsGetEQTransaction = db.ExecuteDataSet(getEQTransactionCmd);
                if (dsGetEQTransaction.Tables[0].Rows.Count > 0)
                {
                    dr = dsGetEQTransaction.Tables[0].Rows[0];
                    AccountVo = new CustomerAccountsVo();
                    AccountVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                    AccountVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    AccountVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                    AccountVo.TradeNum = dr["CETA_TradeAccountNum"].ToString();
                    if (dr["CETA_AccountOpeningDate"].ToString() != string.Empty)
                        AccountVo.AccountOpeningDate = DateTime.Parse(dr["CETA_AccountOpeningDate"].ToString());
                    else
                        AccountVo.AccountOpeningDate = DateTime.MinValue;
                    if (dr["CETA_BrokerDeliveryPercentage"].ToString() != "")
                        AccountVo.BrokerageDeliveryPercentage = double.Parse(dr["CETA_BrokerDeliveryPercentage"].ToString());
                    else
                        AccountVo.BrokerageDeliveryPercentage = 0;
                    if (dr["CETA_BrokerSpeculativePercentage"].ToString() != "")
                        AccountVo.BrokerageSpeculativePercentage = double.Parse(dr["CETA_BrokerSpeculativePercentage"].ToString());
                    else
                        AccountVo.BrokerageSpeculativePercentage = 0;
                    if (dr["CETA_OtherCharges"].ToString() != "")
                        AccountVo.OtherCharges = double.Parse(dr["CETA_OtherCharges"].ToString());
                    else
                        AccountVo.OtherCharges = 0;
                    if (dr["CB_CustBankAccId"].ToString() != "")
                        AccountVo.BankId = int.Parse(dr["CB_CustBankAccId"].ToString());
                    if (dr["CB_AccountNum"].ToString() != "")
                        AccountVo.BankAccountNum = dr["CB_AccountNum"].ToString();
                    if (dr["WERPBM_BankCode"].ToString() != "")
                        AccountVo.BankNameInExtFile = dr["WERPBM_BankCode"].ToString();
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolioDetails()");
                object[] objects = new object[1];
                //objects[0] = FolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return AccountVo;


        }

        public bool UpdateCustomerEQAccountDetails(CustomerAccountsVo AccountVo, int userId)
        {
            bool blResult = false;
            Database db;
            DbCommand updateMFFolioDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFFolioDetailsCmd = db.GetStoredProcCommand("SP_UpdateCustomerTradeAccountDetails");
                db.AddInParameter(updateMFFolioDetailsCmd, "@CETA_AccountId", DbType.Int32, AccountVo.AccountId);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CP_PortfolioId", DbType.Int32, AccountVo.PortfolioId);
                db.AddInParameter(updateMFFolioDetailsCmd, "@XB_BrokerCode", DbType.String, AccountVo.BrokerCode);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CETA_TradeAccountNum", DbType.String, AccountVo.TradeNum);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CETA_BrokerDeliveryPercentage", DbType.Double, AccountVo.BrokerageDeliveryPercentage);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CETA_BrokerSpeculativePercentage", DbType.Double, AccountVo.BrokerageSpeculativePercentage);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CETA_OtherCharges", DbType.Double, AccountVo.OtherCharges);
                if (AccountVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CETA_AccountOpeningDate", DbType.DateTime, AccountVo.AccountOpeningDate);
                db.AddInParameter(updateMFFolioDetailsCmd, "@ModifiedBy", DbType.Int32, userId);
                //db.AddInParameter(updateMFFolioDetailsCmd, "@BankId", DbType.Int32, AccountVo.BankId);
                //db.AddInParameter(updateMFFolioDetailsCmd, "@WERPBM_BankCode", DbType.Int32, AccountVo.BankNameInExtFile);

                if (!string.IsNullOrEmpty(AccountVo.BankNameInExtFile))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@WERPBM_BankCode", DbType.String, AccountVo.BankNameInExtFile);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@WERPBM_BankCode", DbType.String, DBNull.Value);
                }
                if (AccountVo.BankId != 0)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@BankId", DbType.String, AccountVo.BankId);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@BankId", DbType.String, DBNull.Value);
                }


                if (db.ExecuteNonQuery(updateMFFolioDetailsCmd) != 0)
                    blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateCustomerMFFolioDetails()");
                object[] objects = new object[2];
                objects[0] = AccountVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }


        #endregion Equity Transactions

        #region Mutual Fund Transactions
        public bool RunMFTRansactionsCancellationJob()
        {
            bool bResult = false;
            Database db;
            DbCommand MFCancellationJobCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                MFCancellationJobCmd = db.GetStoredProcCommand("SP_MFTransactionCancellationJob");
                MFCancellationJobCmd.CommandTimeout = 60 * 60;
                db.ExecuteNonQuery(MFCancellationJobCmd);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:AddMFTransaction()");


                object[] objects = new object[1];
                objects[0] = bResult;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public int AddMFTransaction(MFTransactionVo mfTransactionVo, int userId)
        {
            int transactionId = 0;
            Database db;
            DbCommand createMFTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFTransactionCmd = db.GetStoredProcCommand("SP_AddMutualFundTransaction");

                db.AddInParameter(createMFTransactionCmd, "@CMFA_AccountId", DbType.Int32, mfTransactionVo.AccountId);
                db.AddInParameter(createMFTransactionCmd, "@PASP_SchemePlanCode", DbType.Int32, mfTransactionVo.MFCode);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_TransactionDate", DbType.DateTime, mfTransactionVo.TransactionDate);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_BuySell", DbType.String, mfTransactionVo.BuySell);

                db.AddInParameter(createMFTransactionCmd, "@CMFT_DividendRate", DbType.Decimal, mfTransactionVo.DividendRate);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_NAV", DbType.Decimal, mfTransactionVo.NAV);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_Price", DbType.Decimal, mfTransactionVo.Price);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_Amount", DbType.Decimal, mfTransactionVo.Amount);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_Units", DbType.Decimal, mfTransactionVo.Units);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_STT", DbType.Decimal, mfTransactionVo.STT);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_IsSourceManual", DbType.Int32, mfTransactionVo.IsSourceManual);

                db.AddInParameter(createMFTransactionCmd, "@XES_SourceCode", DbType.String, mfTransactionVo.Source);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_SwitchSourceTrxId", DbType.Int32, mfTransactionVo.SwitchSourceTrxId);
                db.AddInParameter(createMFTransactionCmd, "@WMTT_TransactionClassificationCode", DbType.String, mfTransactionVo.TransactionClassificationCode);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_SubBrokerCode", DbType.String, mfTransactionVo.AgentCode);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_Area", DbType.String, mfTransactionVo.Area);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_EUIN", DbType.String, mfTransactionVo.EUIN);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createMFTransactionCmd, "@CMFT_CreatedBy", DbType.Int32, userId);
                db.AddOutParameter(createMFTransactionCmd, "@CMFT_MFTransId", DbType.Int32, 50000);

                if (db.ExecuteNonQuery(createMFTransactionCmd) != 0)
                    transactionId = int.Parse(db.GetParameterValue(createMFTransactionCmd, "@CMFT_MFTransId").ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:AddMFTransaction()");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return transactionId;
        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int portfolioId, DateTime FromDate, DateTime ToDate)
        {
            List<MFTransactionVo> mfTransactionsList = null;
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            //genDictTranType = new Dictionary<string, string>();
            //genDictTranTrigger = new Dictionary<string, string>();
            //genDictTranDate = new Dictionary<string, string>();

            //Count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerMutualFundTransactions");
                db.AddInParameter(getMFTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getMFTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                //db.AddInParameter(getMFTransactionsCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                //db.AddInParameter(getMFTransactionsCmd, "@export", DbType.Int32, export);
                //if (SchemeFilter != "")
                //    db.AddInParameter(getMFTransactionsCmd, "@schemeFilter", DbType.String, SchemeFilter);
                //else
                //    db.AddInParameter(getMFTransactionsCmd, "@schemeFilter", DbType.String, DBNull.Value);
                //if (folioFilter != "")
                //    db.AddInParameter(getMFTransactionsCmd, "@folioFilter", DbType.String, folioFilter);
                //else
                //    db.AddInParameter(getMFTransactionsCmd, "@folioFilter", DbType.String, DBNull.Value);
                //if (TypeFilter != "")
                //    db.AddInParameter(getMFTransactionsCmd, "@typeFilter", DbType.String, TypeFilter);
                //else
                //    db.AddInParameter(getMFTransactionsCmd, "@typeFilter", DbType.String, DBNull.Value);
                //if (TriggerFilter != "")
                //    db.AddInParameter(getMFTransactionsCmd, "@triggerFilter", DbType.String, TriggerFilter);
                //else
                //    db.AddInParameter(getMFTransactionsCmd, "@triggerFilter", DbType.String, DBNull.Value);
                //if (DateFilter != "")
                //    db.AddInParameter(getMFTransactionsCmd, "@dateFilter", DbType.String, DateFilter);
                //else
                //    db.AddInParameter(getMFTransactionsCmd, "@dateFilter", DbType.String, DBNull.Value);
                //if (TransactionCode != "")
                //    db.AddInParameter(getMFTransactionsCmd, "@TransactionStatusCode", DbType.Int16, Int16.Parse(TransactionCode));
                //else
                //    db.AddInParameter(getMFTransactionsCmd, "@TransactionStatusCode", DbType.Int16, 1);

                //if (ProcessId != 0)
                //    db.AddInParameter(getMFTransactionsCmd, "@ProcessId", DbType.String, ProcessId);
                //else
                //    db.AddInParameter(getMFTransactionsCmd, "@ProcessId", DbType.String, DBNull.Value);

                //db.AddInParameter(getMFTransactionsCmd, "@sortExpression", DbType.String, SortExpression);
                db.AddInParameter(getMFTransactionsCmd, "@FromTransactionDate", DbType.DateTime, FromDate);
                db.AddInParameter(getMFTransactionsCmd, "@ToTransactionDate", DbType.DateTime, ToDate);
                getMFTransactionsCmd.CommandTimeout = 60 * 60;
                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();
                        if (dr["ADUL_ProcessId"].ToString() != null && dr["ADUL_ProcessId"].ToString() != string.Empty)
                            mfTransactionVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        else
                            mfTransactionVo.ProcessId = 0;

                        if (dr["CMFT_SubBrokerCode"].ToString() != null && dr["CMFT_SubBrokerCode"].ToString() != string.Empty)
                            mfTransactionVo.SubBrokerCode = dr["CMFT_SubBrokerCode"].ToString();
                        else
                            mfTransactionVo.SubBrokerCode = "N/A";

                        if (dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != null && dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != string.Empty)
                            mfTransactionVo.SubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
                        else
                            mfTransactionVo.SubCategoryName = "N/A";
                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        if (!string.IsNullOrEmpty(dr["CMFT_IsSourceManual"].ToString()))
                            mfTransactionVo.IsSourceManual = int.Parse(dr["CMFT_IsSourceManual"].ToString());
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());
                        mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfTransactionVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        mfTransactionVo.AMCName = dr["PA_AMCName"].ToString();
                        mfTransactionVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfTransactionVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        if (dr["WTS_TransactionStatusCode"].ToString() != null && dr["WTS_TransactionStatusCode"].ToString() != string.Empty)
                        {
                            mfTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                            mfTransactionVo.TransactionStatusCode = int.Parse(dr["WTS_TransactionStatusCode"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.TransactionStatus = "OK";
                            mfTransactionVo.TransactionStatusCode = 1;

                        }
                        if (!string.IsNullOrEmpty(dr["CMFT_OriginalTransactionNumber"].ToString()))
                            mfTransactionVo.OriginalTransactionNumber = dr["CMFT_OriginalTransactionNumber"].ToString();
                        mfTransactionsList.Add(mfTransactionVo);
                    }
                }

                //if (dsGetMFTransactions.Tables[2].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsGetMFTransactions.Tables[2].Rows)
                //    {
                //        genDictTranType.Add(dr["WMTT_TransactionClassificationName"].ToString(), dr["WMTT_TransactionClassificationName"].ToString());
                //    }
                //}

                //if (dsGetMFTransactions.Tables[3].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsGetMFTransactions.Tables[3].Rows)
                //    {
                //        genDictTranDate.Add(DateTime.Parse(dr["CMFT_TransactionDate"].ToString()).ToShortDateString(), DateTime.Parse(dr["CMFT_TransactionDate"].ToString()).ToShortDateString());
                //    }
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFTransactions()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            //if (dsGetMFTransactions.Tables[1].Rows.Count > 0)
            //    Count = Int32.Parse(dsGetMFTransactions.Tables[1].Rows[0]["CNT"].ToString());

            return mfTransactionsList;
        }

        public int GetMFTransactions(int customerId, int portfolioId, string flag)
        {

            Database db;
            DbCommand getMFTransactionsCPCmd;
            DataSet dsGetMFTransactionsCP;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCPCmd = db.GetStoredProcCommand("SP_GetCustomerMutualFundTransactions");
                db.AddInParameter(getMFTransactionsCPCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getMFTransactionsCPCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getMFTransactionsCPCmd, "@Flag", DbType.String, flag);
                dsGetMFTransactionsCP = db.ExecuteDataSet(getMFTransactionsCPCmd);

            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFTransactions()");


                object[] objects = new object[3];
                objects[0] = flag;
                objects[1] = customerId;
                objects[2] = portfolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return int.Parse(dsGetMFTransactionsCP.Tables[0].Rows[0][0].ToString());

        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int portfolioId)
        {
            List<MFTransactionVo> mfTransactionsList = null;
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioMutualFundTransactions");
                db.AddInParameter(getMFTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getMFTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        if (!string.IsNullOrEmpty(dr["CMFT_IsSourceManual"].ToString()))
                            mfTransactionVo.IsSourceManual = int.Parse(dr["CMFT_IsSourceManual"].ToString());
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());


                        mfTransactionsList.Add(mfTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFTransactions()");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;
        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int accountId, int mfCode, DateTime tradeDate)
        {
            List<MFTransactionVo> mfTransactionsList = null;
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerMutualFundSpecificTransactions");
                db.AddInParameter(getMFTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getMFTransactionsCmd, "@PASP_SchemePlanCode", DbType.Int32, mfCode);
                db.AddInParameter(getMFTransactionsCmd, "@CMFA_AccountId", DbType.Int32, accountId);
                db.AddInParameter(getMFTransactionsCmd, "@CMFT_TransactionDate", DbType.DateTime, tradeDate);

                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        mfTransactionVo.IsSourceManual = int.Parse(dr["CMFT_IsSourceManual"].ToString());
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());

                        mfTransactionsList.Add(mfTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFTransactions()");


                object[] objects = new object[4];
                objects[0] = customerId;
                objects[1] = accountId;
                objects[2] = mfCode;
                objects[3] = tradeDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return mfTransactionsList;
        }

        public List<MFTransactionVo> GetMFTransactions(int customerId, int portfolioId, int accountId, int mfCode, DateTime tradeDate)
        {
            List<MFTransactionVo> mfTransactionsList = null;


            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioMutualFundSpecificTransactions");
                db.AddInParameter(getMFTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getMFTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getMFTransactionsCmd, "@PASP_SchemePlanCode", DbType.Int32, mfCode);
                db.AddInParameter(getMFTransactionsCmd, "@CMFA_AccountId", DbType.Int32, accountId);
                db.AddInParameter(getMFTransactionsCmd, "@CMFT_TransactionDate", DbType.DateTime, tradeDate);

                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        mfTransactionVo.Units = double.Parse(dr["CMFT_Units"].ToString());
                        if( !string.IsNullOrEmpty( dr["CMFT_STT"].ToString()))
                             mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        mfTransactionVo.IsSourceManual = int.Parse(dr["CMFT_IsSourceManual"].ToString());
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        if (dr["CMFT_SwitchSourceTrxId"] != null && dr["CMFT_SwitchSourceTrxId"].ToString() != String.Empty)
                            mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());

                        mfTransactionsList.Add(mfTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFTransactions()");


                object[] objects = new object[5];
                objects[0] = customerId;
                objects[1] = portfolioId;
                objects[2] = accountId;
                objects[3] = mfCode;
                objects[4] = tradeDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return mfTransactionsList;
        }

        public List<MFTransactionVo> GetMFRejectTransactions(int portfolioId, int adviserId)
        {
            List<MFTransactionVo> mfTransactionsList = null;
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetMFRejectTransactions");

                db.AddInParameter(getMFTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getMFTransactionsCmd, "@A_AdviserId", DbType.Int32, adviserId);
                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo.CustomerName = dr["NAME"].ToString();
                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        mfTransactionVo.IsSourceManual = int.Parse(dr["CMFT_IsSourceManual"].ToString());
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());


                        mfTransactionsList.Add(mfTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFRejectTransactions(int portfolioId)");


                object[] objects = new object[2];
                objects[0] = portfolioId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;
        }
        public List<MFTransactionVo> GetMFOriginalTransactions(int MFTransId)
        {
            List<MFTransactionVo> mfTransactionsList = null;
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetMFOriginalTransactions");

                db.AddInParameter(getMFTransactionsCmd, "@MFTransID", DbType.Int32, MFTransId);
                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        mfTransactionVo.IsSourceManual = int.Parse(dr["CMFT_IsSourceManual"].ToString());
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());


                        mfTransactionsList.Add(mfTransactionVo);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFOriginalTransactions(int portfolioId)");


                object[] objects = new object[2];
                objects[0] = MFTransId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfTransactionsList;
        }

        public List<MFSystematicVo> GetMFSystematicTransactionSetups(int adviserId, DateTime fromDate, DateTime toDate, string customerSearch, string schemeSearch, string transType, string portfolioType, out List<string> transactionTypeList)
        {
            List<MFSystematicVo> mfSystematicVoList = new List<MFSystematicVo>();
            MFSystematicVo mfSystematicVo;
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            DataTable dtTransactionType;
            transactionTypeList = new List<string>();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetMFSystematicTransactionSetups");

                db.AddInParameter(getMFTransactionsCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(getMFTransactionsCmd, "@StartDate", DbType.DateTime, fromDate);
                db.AddInParameter(getMFTransactionsCmd, "@EndDate", DbType.DateTime, toDate);
                if (schemeSearch != "")
                    db.AddInParameter(getMFTransactionsCmd, "@schemeFilter", DbType.String, schemeSearch);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@schemeFilter", DbType.DateTime, DBNull.Value);
                if (customerSearch != "")
                    db.AddInParameter(getMFTransactionsCmd, "@NameFilter", DbType.String, customerSearch);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@NameFilter", DbType.String, DBNull.Value);
                if (transType != "")
                    db.AddInParameter(getMFTransactionsCmd, "@typeFilter", DbType.String, transType);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@typeFilter", DbType.String, DBNull.Value);
                if (portfolioType != "")
                    db.AddInParameter(getMFTransactionsCmd, "@portfolioType", DbType.String, portfolioType);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@portfolioType", DbType.String, DBNull.Value);

                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];

                    mfSystematicVoList = new List<MFSystematicVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfSystematicVo = new MFSystematicVo();
                        mfSystematicVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfSystematicVo.AdviserId = int.Parse(dr["A_AdviserId"].ToString());
                        mfSystematicVo.Amount = double.Parse(dr["CMFSS_Amount"].ToString());
                        mfSystematicVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfSystematicVo.CustomerName = dr["CustomerName"].ToString();
                        mfSystematicVo.EndDate = DateTime.Parse(dr["CMFSS_EndDate"].ToString());
                        mfSystematicVo.FolioNum = dr["CMFA_FolioNum"].ToString();
                        mfSystematicVo.FrequencyCode = dr["XF_FrequencyCode"].ToString();
                        mfSystematicVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfSystematicVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfSystematicVo.SchemePlanName = dr["PASP_SchemePlanName"].ToString();
                        mfSystematicVo.StartDate = DateTime.Parse(dr["CMFSS_StartDate"].ToString());
                        if (!string.IsNullOrEmpty(dr["PASP_SchemePlanCodeSwitch"].ToString()))
                            mfSystematicVo.SwitchSchemePlanCode = int.Parse(dr["PASP_SchemePlanCodeSwitch"].ToString());
                        else
                            mfSystematicVo.SwitchSchemePlanCode = 0;
                        if (dr["SwitchScheme"] != null)
                            mfSystematicVo.SwitchSchemePlanName = dr["SwitchScheme"].ToString();
                        if (!string.IsNullOrEmpty(dr["CMFSS_SystematicDate"].ToString()))
                        mfSystematicVo.SystematicDay = int.Parse(dr["CMFSS_SystematicDate"].ToString());
                        mfSystematicVo.SystematicSetupId = int.Parse(dr["CMFSS_SystematicSetupId"].ToString());
                        mfSystematicVo.SystematicTypeCode = dr["XSTT_SystematicTypeCode"].ToString();



                        mfSystematicVoList.Add(mfSystematicVo);
                    }
                }
                if (dsGetMFTransactions.Tables[1].Rows.Count != 0)
                {
                    dtTransactionType = dsGetMFTransactions.Tables[1];
                    foreach (DataRow drt in dtTransactionType.Rows)
                    {
                        if (drt["XSTT_SystematicTypeCode"].ToString() != "STP")
                        {
                            transactionTypeList.Add(drt["XSTT_SystematicTypeCode"].ToString());
                        }
                        else
                        {
                            transactionTypeList.Add("STB");
                            transactionTypeList.Add("STS");
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFOriginalTransactions(int portfolioId)");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = fromDate;
                objects[2] = toDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfSystematicVoList;

        }

        public List<MFSystematicTransactionVo> GetMFSystematicTransactions(int adviserId, DateTime fromDate, DateTime toDate, string customerSearch, string schemeSearch, string transType, string portfolioType, out List<string> transactionTypeList)
        {
            List<MFSystematicTransactionVo> mfSystematicTransactionVoList = new List<MFSystematicTransactionVo>();
            MFSystematicTransactionVo mfSystematicTransactionVo;
            Database db;
            DbCommand getMFTransactionsCmd;
            DataSet dsGetMFTransactions;
            DataTable dtGetMFTransactions;
            DataTable dtTransactionType;
            transactionTypeList = new List<string>();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionsCmd = db.GetStoredProcCommand("SP_GetMFSystematicTransactions");

                db.AddInParameter(getMFTransactionsCmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(getMFTransactionsCmd, "@StartDate", DbType.DateTime, fromDate);
                db.AddInParameter(getMFTransactionsCmd, "@EndDate", DbType.DateTime, toDate);
                if (schemeSearch != "")
                    db.AddInParameter(getMFTransactionsCmd, "@schemeFilter", DbType.String, schemeSearch);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@schemeFilter", DbType.DateTime, DBNull.Value);
                if (customerSearch != "")
                    db.AddInParameter(getMFTransactionsCmd, "@NameFilter", DbType.String, customerSearch);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@NameFilter", DbType.String, DBNull.Value);
                if (transType != "")
                    db.AddInParameter(getMFTransactionsCmd, "@typeFilter", DbType.String, transType);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@typeFilter", DbType.String, DBNull.Value);
                if (portfolioType != "")
                    db.AddInParameter(getMFTransactionsCmd, "@portfolioType", DbType.String, portfolioType);
                else
                    db.AddInParameter(getMFTransactionsCmd, "@portfolioType", DbType.String, DBNull.Value);

                getMFTransactionsCmd.CommandTimeout = 60 * 60;
                dsGetMFTransactions = db.ExecuteDataSet(getMFTransactionsCmd);
                if (dsGetMFTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = dsGetMFTransactions.Tables[0];
                    mfSystematicTransactionVoList = new List<MFSystematicTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfSystematicTransactionVo = new MFSystematicTransactionVo();
                        mfSystematicTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfSystematicTransactionVo.Amount = double.Parse(dr["CMFT_Amount"].ToString());
                        mfSystematicTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfSystematicTransactionVo.CustomerName = dr["CustomerName"].ToString();
                        mfSystematicTransactionVo.FolioNum = dr["CMFA_FolioNum"].ToString();
                        mfSystematicTransactionVo.MFTransId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfSystematicTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfSystematicTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfSystematicTransactionVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfSystematicTransactionVo.SchemePlanName = dr["PASP_SchemePlanName"].ToString();
                        mfSystematicTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());



                        mfSystematicTransactionVoList.Add(mfSystematicTransactionVo);
                    }
                }
                if (dsGetMFTransactions.Tables[1].Rows.Count != 0)
                {
                    dtTransactionType = dsGetMFTransactions.Tables[1];
                    foreach (DataRow drt in dtTransactionType.Rows)
                    {
                        transactionTypeList.Add(drt["WMTT_TransactionClassificationCode"].ToString());
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFSystematicTransactions(int adviserId, DateTime fromDate, DateTime toDate)");


                object[] objects = new object[3];
                objects[0] = adviserId;
                objects[1] = fromDate;
                objects[2] = toDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfSystematicTransactionVoList;

        }


        public MFTransactionVo GetMFTransaction(int mfTransactionId)
        {
            MFTransactionVo mfTransactionVo = null;

            Database db;
            DbCommand getMFTransactionCmd;
            DataSet dsGetMFTransaction;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionCmd = db.GetStoredProcCommand("SP_GetCustomerMFSpecificTransaction");
                db.AddInParameter(getMFTransactionCmd, "@CMFT_MFTransId", DbType.Int32, mfTransactionId);

                dsGetMFTransaction = db.ExecuteDataSet(getMFTransactionCmd);
                if (dsGetMFTransaction.Tables[0].Rows.Count > 0)
                {
                    dr = dsGetMFTransaction.Tables[0].Rows[0];
                    mfTransactionVo = new MFTransactionVo();


                    mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                    mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                    mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                    mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                    mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                    mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                    mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                    mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                    mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                    mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                    mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                    mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                    mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                    mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                    mfTransactionVo.IsSourceManual = int.Parse(dr["CMFT_IsSourceManual"].ToString());
                    mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                    mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                    mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                    mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                    mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                    mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());
                    mfTransactionVo.AgentCode = dr["CMFT_SubBrokerCode"].ToString();
                    if (dr["WTS_TransactionStatusCode"].ToString() != null && dr["WTS_TransactionStatusCode"].ToString() != string.Empty)
                    {
                        mfTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                        mfTransactionVo.TransactionStatusCode = int.Parse(dr["WTS_TransactionStatusCode"].ToString());
                    }
                    else
                    {
                        mfTransactionVo.TransactionStatus = "OK";
                        mfTransactionVo.TransactionStatusCode = 1;

                    }
                    mfTransactionVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                    mfTransactionVo.AMCName = dr["PA_AMCName"].ToString();
                    mfTransactionVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    mfTransactionVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                    mfTransactionVo.OriginalTransactionNumber = dr["CMFT_OriginalTransactionNumber"].ToString();
                    if (dr["CMFT_UserTransactionNo"].ToString() != "")
                    mfTransactionVo.userTransactionNo = int.Parse(dr["CMFT_UserTransactionNo"].ToString());

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFTransaction()");


                object[] objects = new object[1];
                objects[0] = mfTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return mfTransactionVo;
        }

        public bool UpdateMFTransaction(MFTransactionVo mfTransactionVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateMFTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFTransactionCmd = db.GetStoredProcCommand("SP_UpdateMFTransaction");

                db.AddInParameter(updateMFTransactionCmd, "@CMFT_MFTransId", DbType.Int32, mfTransactionVo.TransactionId);
                db.AddInParameter(updateMFTransactionCmd, "@CMFA_AccountId", DbType.Int32, mfTransactionVo.AccountId);
                db.AddInParameter(updateMFTransactionCmd, "@PASP_SchemePlanCode", DbType.String, mfTransactionVo.MFCode);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_TransactionDate", DbType.DateTime, mfTransactionVo.TransactionDate);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_BuySell", DbType.String, mfTransactionVo.BuySell);

                db.AddInParameter(updateMFTransactionCmd, "@CMFT_DividendRate", DbType.Decimal, mfTransactionVo.DividendRate);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_NAV", DbType.String, mfTransactionVo.NAV);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_Price", DbType.Decimal, mfTransactionVo.Price);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_Amount", DbType.Decimal, mfTransactionVo.Amount);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_Units", DbType.Decimal, mfTransactionVo.Units);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_STT", DbType.Decimal, mfTransactionVo.STT);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_IsSourceManual", DbType.String, mfTransactionVo.IsSourceManual);

                db.AddInParameter(updateMFTransactionCmd, "@XES_SourceCode", DbType.String, mfTransactionVo.Source);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_SwitchSourceTrxId", DbType.String, mfTransactionVo.SwitchSourceTrxId);
                db.AddInParameter(updateMFTransactionCmd, "@WMTT_TransactionClassificationCode", DbType.String, mfTransactionVo.TransactionClassificationCode);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_SubBrokerCode", DbType.String, mfTransactionVo.AgentCode);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_ModifiedBy", DbType.String, userId);



                if (db.ExecuteNonQuery(updateMFTransactionCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:AddMFTransaction()");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool UpdateRejectedTransactionStatus(int MFTransId, int OriginalTransactionNumber)
        {
            bool bResult = false;
            Database db;
            DbCommand updateMFTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFTransactionCmd = db.GetStoredProcCommand("SP_UpdateMFRejectedTransactionStatus");

                db.AddInParameter(updateMFTransactionCmd, "@MFTransId", DbType.Int32, MFTransId);
                db.AddInParameter(updateMFTransactionCmd, "@OriginalTransId", DbType.Int32, OriginalTransactionNumber);




                if (db.ExecuteNonQuery(updateMFTransactionCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateRejectedTransactionStatus(int MFTransId,int OriginalTransactionNumber)");


                object[] objects = new object[2];
                objects[0] = MFTransId;
                objects[1] = OriginalTransactionNumber;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool DeleteMFTransaction(int mfTransactionId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteEquityTransactionCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteEquityTransactionCmd = db.GetStoredProcCommand("SP_DeleteMFTransactions");

                db.AddInParameter(deleteEquityTransactionCmd, "@CMFT_MFTransId", DbType.Int32, mfTransactionId);
                if (db.ExecuteNonQuery(deleteEquityTransactionCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:DeleteMFTransaction()");

                object[] objects = new object[1];
                objects[0] = mfTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }


        public bool DeleteMFTransaction(MFTransactionVo mfTransactionVo, int adviserId,int UserId)
        {
            bool bResult = false;
            Database db;
            DbCommand DeleteMFTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteMFTransactionCmd = db.GetStoredProcCommand("SP_DeleteMFTransaction");
                db.AddInParameter(DeleteMFTransactionCmd, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(DeleteMFTransactionCmd, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(DeleteMFTransactionCmd, "@cmftTransactionId", DbType.Int32, mfTransactionVo.TransactionId);
                //db.AddInParameter(DeleteMFTransactionCmd, "@CustomerId", DbType.Int32, mfTransactionVo.CustomerId);
                db.AddInParameter(DeleteMFTransactionCmd, "@SchemeCode", DbType.Int32, mfTransactionVo.MFCode);
                db.AddInParameter(DeleteMFTransactionCmd, "@accountId", DbType.Int32, mfTransactionVo.AccountId);
                db.AddInParameter(DeleteMFTransactionCmd, "@OriginalTrnxNo", DbType.String, mfTransactionVo.OriginalTransactionNumber);
                db.AddInParameter(DeleteMFTransactionCmd, "@stausCode", DbType.Int16, mfTransactionVo.TransactionStatusCode);

                db.ExecuteNonQuery(DeleteMFTransactionCmd);
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CancelMFTransaction(MFTransactionVo mfTransactionVo,int userId)");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        public bool CancelMFTransaction(MFTransactionVo mfTransactionVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateMFTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFTransactionCmd = db.GetStoredProcCommand("SP_CancelMutualFundTransaction");

                db.AddInParameter(updateMFTransactionCmd, "@CMFT_MFTransId", DbType.Int32, mfTransactionVo.TransactionId);
                db.AddInParameter(updateMFTransactionCmd, "@CMFA_AccountId", DbType.Int32, mfTransactionVo.AccountId);
                db.AddInParameter(updateMFTransactionCmd, "@PASP_SchemePlanCode", DbType.String, mfTransactionVo.MFCode);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_TransactionDate", DbType.DateTime, mfTransactionVo.TransactionDate);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_BuySell", DbType.String, mfTransactionVo.BuySell);

                db.AddInParameter(updateMFTransactionCmd, "@CMFT_DividendRate", DbType.Decimal, mfTransactionVo.DividendRate);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_NAV", DbType.String, mfTransactionVo.NAV);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_Price", DbType.Decimal, mfTransactionVo.Price);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_Amount", DbType.Decimal, mfTransactionVo.Amount);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_Units", DbType.Decimal, mfTransactionVo.Units);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_STT", DbType.Decimal, mfTransactionVo.STT);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_IsSourceManual", DbType.String, mfTransactionVo.IsSourceManual);

                db.AddInParameter(updateMFTransactionCmd, "@XES_SourceCode", DbType.String, mfTransactionVo.Source);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_SwitchSourceTrxId", DbType.String, mfTransactionVo.SwitchSourceTrxId);
                db.AddInParameter(updateMFTransactionCmd, "@WMTT_TransactionClassificationCode", DbType.String, mfTransactionVo.TransactionClassificationCode);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_ModifiedBy", DbType.String, userId);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_CreatedBy", DbType.String, userId);
                db.AddInParameter(updateMFTransactionCmd, "@CMFT_SubBrokerCode", DbType.String, mfTransactionVo.AgentCode);
                if (db.ExecuteNonQuery(updateMFTransactionCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CancelMFTransaction(MFTransactionVo mfTransactionVo,int userId)");


                object[] objects = new object[2];
                objects[0] = mfTransactionVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        #endregion Mutual Fund Transactions
        public string getId()
        {
            Guid id;
            id = Guid.NewGuid();
            return id.ToString();
        }

        #region MF MUltiple Transaction View

        public DataSet GetLastTradeDate()
        {
            DataSet ds = null;
            Database db;
            DbCommand getLastTradeDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLastTradeDateCmd = db.GetStoredProcCommand("SP_GetLastTradeDate");
                ds = db.ExecuteDataSet(getLastTradeDateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetLastTradeDate()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        /// <summary>
        /// To get the portfolio type for the folio number
        /// </summary>
        /// <returns></returns>
        public DataSet GetPortfolioType(string folionum)
        {
            DataSet ds = null;
            Database db;
            DbCommand getportfoliotype;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getportfoliotype = db.GetStoredProcCommand("SP_GetPortfolioType");
                db.AddInParameter(getportfoliotype, "@folionum", DbType.String, folionum);
                ds = db.ExecuteDataSet(getportfoliotype);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetPortfolioType()");
                object[] objects = new object[1];
                objects[0] = folionum;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        /// <summary>
        /// Returns List of MF Transactions for the Selected RMId based on the Parameters
        /// </summary>
        /// <param name="Count">Out Parameter returns the number of Records</param>
        /// <param name="CurrentPage">Passes the Current Page Number for Paging</param>
        /// <param name="RMId">RelationShip Manager Id</param>
        /// <param name="GroupHeadId">Passes the Groupo HeadId if the List is for a Group</param>
        /// <param name="From">From Date</param>
        /// <param name="To">To Date</param>
        /// <param name="Manage">Parameter to Check Managed and UnManaged Portfolios</param>
        /// <param name="CustomerName">Name of the Customer for Search Purpose</param>
        /// <param name="Scheme">Scheme Search String Parameter</param>
        /// <param name="TranType">Transactiion Type Search Parameter</param>
        /// <param name="transactionStatus">Transaction Status Search Parameter</param>
        /// <param name="genDictTranType">Out Parameter Returns Dictionary of Available Transaction Types</param>
        /// <param name="FolioNumber">MF Folio Number Search Parameter</param>
        /// <param name="PasssedFolioValue">Folio Value Search Parameter</param>
        /// <returns></returns>
        public List<MFTransactionVo> GetRMCustomerMFTransactions(int RMId, int AdviserID, int GroupHeadId,int IsfolioOnline, DateTime From, DateTime To, int Manage, int AccountId, bool isCustomerTransactionOnly, int SchemePlanCode, int AmcCode, string Category, int A_AgentCodeBased,string AgentCode,string UserType,int agentCode,int requestId)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFTransactionsCmd;
            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            MFTransactionVo mfTransactionVo = new MFTransactionVo();           
            DataTable dtGetMFTransactions;
            //genDictTranType = new Dictionary<string, string>();
            //genDictCategory = new Dictionary<string, string>();
            //genDictAMC = new Dictionary<string, int>();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //pramod
                //if (CurrentPage == 5000)
                //{
                //    getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SP_GetALLRMCustomerMFTransactions");

                //}
                //else
                //{
              //  getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SP_GetRMCustomerMFTransactions");//comment
                //}
                getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SPROC_GetMFOfflineTransaction");

                if (RMId != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@RMId", DbType.Int32, RMId);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@RMId", DbType.Int32, DBNull.Value);

                if (AdviserID != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, AdviserID);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, DBNull.Value);

                if (AgentCode != "0")
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AgentCode", DbType.String, DBNull.Value);
                if (GroupHeadId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, GroupHeadId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@isCustomerTransactionOnly", DbType.Int16, isCustomerTransactionOnly);
                //if (ProcessId != 0)
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@processId", DbType.Int32, ProcessId);
                //}
                //else
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@processId", DbType.Int32, DBNull.Value);
                //}
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@UserType", DbType.String, UserType);
                if (From != DateTime.MinValue)
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FromDate", DbType.DateTime, From);
                if (To != DateTime.MinValue)
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ToDate", DbType.DateTime, To);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Manage", DbType.Int32, Manage);
                if (AccountId != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.String, AccountId);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.String, DBNull.Value);
                if (SchemePlanCode != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SchemePlanCode", DbType.String, SchemePlanCode);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SchemePlanCode", DbType.String, DBNull.Value);

                if (AmcCode != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AmcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AmcCode", DbType.Int32, DBNull.Value);
                if (Category != "0")
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Category", DbType.String, Category);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Category", DbType.String, DBNull.Value);
                //if (AgentId != 0)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                //if (IsAssociates != 0)
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@IsAgentBasedCode", DbType.Int32, A_AgentCodeBased);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@IsfolioOnline", DbType.Int32, IsfolioOnline);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@IsagentCode", DbType.Int32, agentCode);
                if (requestId != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@RequestId", DbType.Int32, requestId);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@RequestId", DbType.Int32, DBNull.Value);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@IsAssociate", DbType.Int32, DBNull.Value);
                //if (All != 0)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@all", DbType.Int32, All);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@all", DbType.Int32, 0);

                //db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                //if (CustomerName != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, CustomerName);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, DBNull.Value);
                //if (Scheme != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scheme", DbType.String, Scheme);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scheme", DbType.String, DBNull.Value);
                //if (TranType != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, TranType);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, DBNull.Value);
                //if (FolioNumber != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, FolioNumber);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, DBNull.Value);
                //if (PasssedFolioValue != null)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@PasssedFolioValue", DbType.String, PasssedFolioValue);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@PasssedFolioValue", DbType.String, DBNull.Value);
                //if (transactionStatus == "")
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TransactionStatus", DbType.String, "1");
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TransactionStatus", DbType.String, transactionStatus);
                //if (categoryCode == "")
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CategoryCode", DbType.String, DBNull.Value);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CategoryCode", DbType.String, categoryCode);
                //if (AMCCode == 0)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMCCode", DbType.Int32, DBNull.Value);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMCCode", DbType.String, AMCCode);
                getRMCustomerMFTransactionsCmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(getRMCustomerMFTransactionsCmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = ds.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();

                        if (dr["WR_RequestId"].ToString() != null && dr["WR_RequestId"].ToString() != string.Empty)
                            mfTransactionVo.RequestId = int.Parse(dr["WR_RequestId"].ToString());
                        else
                            mfTransactionVo.RequestId = 0;

                        if (dr["CMFT_SubBrokerCode"].ToString() != null && dr["CMFT_SubBrokerCode"].ToString() != string.Empty)
                            mfTransactionVo.SubBrokerCode = dr["CMFT_SubBrokerCode"].ToString();
                        else
                            mfTransactionVo.SubBrokerCode = "N/A";

                        if (dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != null && dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != string.Empty)
                            mfTransactionVo.SubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
                        else
                            mfTransactionVo.SubCategoryName = "N/A";

                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.CustomerName = dr["Name"].ToString();
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        mfTransactionVo.AMCName = dr["PA_AMCName"].ToString();
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        mfTransactionVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        if (dr["CMFT_DividendRate"].ToString() != "")
                        {
                            mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        }
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        if (dr["CMFT_NAV"].ToString() != "")
                        {
                            mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        }
                        if (dr["CMFT_Price"].ToString() != "")
                        {
                            mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        }
                        if (dr["CMFT_Amount"].ToString() != "")
                        {
                            mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        }
                        if (dr["CMFT_Units"].ToString() != "")
                        {
                            mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        }
                        if ( dr["CMFT_STT"].ToString() !="")
                        {
                            mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        }
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());
                        mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfTransactionVo.PortfolioName = dr["CP_PortfolioName"].ToString();
                        mfTransactionVo.CreatedOn = DateTime.Parse(dr["CMFT_CreatedOn"].ToString());
                        //if (dr["CityGroup"].ToString() != "") 
                        mfTransactionVo.Citygroup =dr["CityGroup"].ToString();
                        //&& dr["CMFT_UserTransactionNo"].ToString() != string.Empty)
                        if (dr["CMFT_UserTransactionNo"].ToString() != string.Empty)
                        {
                            mfTransactionVo.UserTransactionNo = dr["CMFT_UserTransactionNo"].ToString();
                        }
                        else
                        {
                            mfTransactionVo.UserTransactionNo = "N/A";
                        }
                        if (1 == 1)
                        {
                            if (dr["ZonalManagerName"].ToString() != null && dr["ZonalManagerName"].ToString() != string.Empty)
                            {
                                mfTransactionVo.ZMName = dr["ZonalManagerName"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.ZMName = "N/A";
                            }
                            if (dr["AreaManager"].ToString() != null && dr["AreaManager"].ToString() != string.Empty)
                            {
                                mfTransactionVo.AName = dr["AreaManager"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.AName = "N/A";
                            }
                            if (dr["AssociatesName"].ToString() != null && dr["AssociatesName"].ToString() != string.Empty)
                            {
                                mfTransactionVo.SubbrokerName = dr["AssociatesName"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.SubbrokerName = "N/A";
                            }
                            if (dr["ChannelName"].ToString() != null && dr["ChannelName"].ToString() != string.Empty)
                            {
                                mfTransactionVo.Channel = dr["ChannelName"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.Channel = "N/A";
                            }

                            if (dr["Titles"].ToString() != null && dr["Titles"].ToString() != string.Empty)
                            {
                                mfTransactionVo.Titles = dr["Titles"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.Titles = "N/A";
                            }
                            if (dr["ClusterManager"].ToString() != null && dr["ClusterManager"].ToString() != string.Empty)
                            {
                                mfTransactionVo.ClusterMgr = dr["ClusterManager"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.ClusterMgr = "N/A";
                            }
                            if (dr["ReportingManagerName"].ToString() != null && dr["ReportingManagerName"].ToString() != string.Empty)
                            {
                                mfTransactionVo.ReportingManagerName = dr["ReportingManagerName"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.ReportingManagerName = "N/A";
                            }
                            if (dr["UserType"].ToString() != null && dr["UserType"].ToString() != string.Empty)
                            {
                                mfTransactionVo.UserType = dr["UserType"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.UserType = "N/A";
                            }
                            if (dr["DeputyHead"].ToString() != null && dr["DeputyHead"].ToString() != string.Empty)
                            {
                                mfTransactionVo.DeuptyHead = dr["DeputyHead"].ToString();
                            }
                            else
                            {
                                mfTransactionVo.DeuptyHead = "N/A";
                            }

                        }
                        if (dr["CMFT_EUIN"].ToString() != null && dr["CMFT_EUIN"].ToString() != string.Empty)
                        {
                            mfTransactionVo.EUIN = dr["CMFT_EUIN"].ToString();
                        }
                        else
                        {
                            mfTransactionVo.EUIN = "N/A";
                        }
                        if (dr["CMFT_Area"].ToString() != null && dr["CMFT_Area"].ToString() != string.Empty)
                        {
                            mfTransactionVo.Area = dr["CMFT_Area"].ToString();
                        }
                        else
                        {
                            mfTransactionVo.Area = "N/A";
                        }
                        if (dr["WTS_TransactionStatusCode"].ToString() != null && dr["WTS_TransactionStatusCode"].ToString() != string.Empty)
                        {
                            mfTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                            mfTransactionVo.TransactionStatusCode = int.Parse(dr["WTS_TransactionStatusCode"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.TransactionStatus = "OK";
                            mfTransactionVo.TransactionStatusCode = 1;

                        }
                        if (!string.IsNullOrEmpty(dr["CMFT_ExternalBrokerageAmount"].ToString()))
                            mfTransactionVo.BrokerageAmount = float.Parse(dr["CMFT_ExternalBrokerageAmount"].ToString());

                        mfTransactionsList.Add(mfTransactionVo);
                        //if (ds.Tables[0].Rows.Count == mfTransactionsList.Count)
                        //{
                        //    return mfTransactionsList;
                        //}
                    }
                   
                }
            }
            //pramod
            //if (ds.Tables.Count > 1)
            //{
            //    if (ds.Tables[2].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in ds.Tables[2].Rows)
            //        {
            //            genDictTranType.Add(dr["WMTT_TransactionClassificationName"].ToString(), dr["WMTT_TransactionClassificationName"].ToString());
            //        }
            //    }
            //    if (ds.Tables[3].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in ds.Tables[3].Rows)
            //        {
            //            genDictCategory.Add(dr["PAIC_AssetInstrumentCategoryName"].ToString(), dr["PAIC_AssetInstrumentCategoryCode"].ToString());
            //        }
            //    }
            //    if (ds.Tables[4].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in ds.Tables[4].Rows)
            //        {
            //            genDictAMC.Add(dr["PA_AMCName"].ToString(), int.Parse(dr["PA_AMCCode"].ToString()));
            //        }
            //    }
            //}
            //  }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[3];
                objects[0] = RMId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                //objects[5] = Scheme;
                //objects[6] = genDictTranType;
                //objects[7] = FolioNumber;
                //objects[8] = CustomerName;
                //objects[9] = CurrentPage;
                //objects[10] = PasssedFolioValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            //
            //if (ds.Tables.Count > 1)
            //{
            //    if (ds.Tables[1].Rows.Count > 0)
            //        Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());
            //}
            return mfTransactionsList;
        }


        public DataSet GetRMCustomerTrailCommission(int RMId, int AdviserID, int GroupHeadId, DateTime From, DateTime To, int Manage, int AccountId, int SchemePlanCode, int AmcCode, string Category, int A_AgentCodeBased, string AgentCode, string UserType)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFTransactionsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("Sp_getrmcustomertrailcommissiontemp");

                if (RMId != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@RMId", DbType.Int32, RMId);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@RMId", DbType.Int32, DBNull.Value);

                if (AdviserID != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, AdviserID);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, DBNull.Value);


                if (GroupHeadId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, GroupHeadId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, DBNull.Value);
                }

                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@UserType", DbType.String, UserType);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FromDate", DbType.DateTime, From);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ToDate", DbType.DateTime, To);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Manage", DbType.Int32, Manage);
                if (AccountId != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.String, AccountId);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.String, DBNull.Value);
                if (AmcCode != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AmcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AmcCode", DbType.Int32, DBNull.Value);
                if (Category != "0")
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Category", DbType.String, Category);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Category", DbType.String, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AgentCode", DbType.String, DBNull.Value);

                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@IsAgentBasedCode", DbType.Int32, A_AgentCodeBased);
                if (SchemePlanCode != 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SchemePlanCode", DbType.String, SchemePlanCode);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SchemePlanCode", DbType.String, DBNull.Value);
                getRMCustomerMFTransactionsCmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(getRMCustomerMFTransactionsCmd);
                

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[5];
                objects[0] = RMId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;

        }


        public List<MFTransactionVo> GetAdviserCustomerMFTransactions(out int Count, int CurrentPage, int adviserId, int GroupHeadId, DateTime From, DateTime To, int Manage, string CustomerName, string Scheme, string TranType, string transactionStatus, out Dictionary<string, string> genDictTranType, string FolioNumber, string PasssedFolioValue, string categoryCode, int AMCCode, out Dictionary<string, string> genDictCategory, out Dictionary<string, int> genDictAMC)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFTransactionsCmd;
            List<MFTransactionVo> mfTransactionsList = new List<MFTransactionVo>();
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            DataTable dtGetMFTransactions;
            Count = 0;
            genDictTranType = new Dictionary<string, string>();
            genDictCategory = new Dictionary<string, string>();
            genDictAMC = new Dictionary<string, int>();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //pramod
                if (CurrentPage == 5000)
                {
                    getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SP_GetALLAdviserCustomerMFTransactions");

                }
                else
                {
                    getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerMFTransactions");
                }

                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserId", DbType.Int32, adviserId);
                if (GroupHeadId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, GroupHeadId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FromDate", DbType.DateTime, From);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ToDate", DbType.DateTime, To);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Manage", DbType.Int32, Manage);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                if (CustomerName != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, CustomerName);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, DBNull.Value);
                if (Scheme != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scheme", DbType.String, Scheme);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scheme", DbType.String, DBNull.Value);
                if (TranType != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, TranType);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, DBNull.Value);
                if (FolioNumber != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, FolioNumber);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, DBNull.Value);
                if (PasssedFolioValue != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@PasssedFolioValue", DbType.String, PasssedFolioValue);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@PasssedFolioValue", DbType.String, DBNull.Value);
                if (transactionStatus == "")
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TransactionStatus", DbType.String, "1");
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TransactionStatus", DbType.String, transactionStatus);
                if (categoryCode == "")
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CategoryCode", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CategoryCode", DbType.String, categoryCode);
                if (AMCCode == 0)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMCCode", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMCCode", DbType.String, AMCCode);
                getRMCustomerMFTransactionsCmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(getRMCustomerMFTransactionsCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = ds.Tables[0];
                    mfTransactionsList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();
                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.CustomerName = dr["Name"].ToString();
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        mfTransactionVo.AMCName = dr["PA_AMCName"].ToString();
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        mfTransactionVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());
                        mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfTransactionVo.PortfolioName = dr["CP_PortfolioName"].ToString();
                        if (dr["WTS_TransactionStatusCode"].ToString() != null && dr["WTS_TransactionStatusCode"].ToString() != string.Empty)
                        {
                            mfTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                            mfTransactionVo.TransactionStatusCode = int.Parse(dr["WTS_TransactionStatusCode"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.TransactionStatus = "OK";
                            mfTransactionVo.TransactionStatusCode = 1;

                        }
                        mfTransactionsList.Add(mfTransactionVo);
                    }
                }
                //pramod
                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            genDictTranType.Add(dr["WMTT_TransactionClassificationName"].ToString(), dr["WMTT_TransactionClassificationName"].ToString());
                        }
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            genDictCategory.Add(dr["PAIC_AssetInstrumentCategoryName"].ToString(), dr["PAIC_AssetInstrumentCategoryCode"].ToString());
                        }
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[4].Rows)
                        {
                            genDictAMC.Add(dr["PA_AMCName"].ToString(), int.Parse(dr["PA_AMCCode"].ToString()));
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetAdviserCustomerMFTransactions()");
                object[] objects = new object[11];
                objects[0] = adviserId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                objects[5] = Scheme;
                objects[6] = genDictTranType;
                objects[7] = FolioNumber;
                objects[8] = CustomerName;
                objects[9] = CurrentPage;
                objects[10] = PasssedFolioValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            //
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                    Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());
            }
            return mfTransactionsList;
        }
        #endregion MF MUltiple Transaction View


        #region Equity Multiple Transaction View


        public DataSet GetRMCustomerEqTransactions(int RMId, int adviserId, int GroupHeadId, DateTime From, DateTime To, int Manage)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFTransactionsCmd;
            List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();
            EQTransactionVo EqTransactionVo = new EQTransactionVo();
            DataTable dtGetMFTransactions;
            //Count = 0;
            //genDictTranType = new Dictionary<string, string>();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SP_GetRMCustomerEQTransactions");
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@RMId", DbType.Int32, RMId);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, adviserId);
                if (GroupHeadId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, GroupHeadId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FromDate", DbType.DateTime, From);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ToDate", DbType.DateTime, To);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Manage", DbType.Int32, Manage);
               
                //db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CurrentPage", DbType.Int32, CurrentPage);

                //db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ExportFlag", DbType.Int32, exportFlag);

                //if (CustomerName != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, CustomerName);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, DBNull.Value);
                //if (Scrip != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scrip", DbType.String, Scrip);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scrip", DbType.String, DBNull.Value);
                //if (TranType != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, TranType);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, DBNull.Value);
                //if (FolioNumber != string.Empty)
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, FolioNumber);
                //else
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, DBNull.Value);


                ds = db.ExecuteDataSet(getRMCustomerMFTransactionsCmd);
                //PageSize = int.Parse(db.GetParameterValue(getRMCustomerMFTransactionsCmd, "PageSize").ToString());

                //if (ds.Tables.Count > 1 && ds.Tables[2].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in ds.Tables[2].Rows)
                //    {
                //        genDictTranType.Add(dr["WETT_TransactionTypeName"].ToString(), dr["WETT_TransactionCode"].ToString());
                //    }
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[5];
                objects[0] = RMId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            //if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            //    Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());

            return ds;
            //return eqTransactionsList;
        }
        public DataSet GetAdviserCustomerEqTransactions(out int Count, int CurrentPage, int adviserId, int GroupHeadId, DateTime From, DateTime To, int Manage, string CustomerName, string Scrip, string TranType, out Dictionary<string, string> genDictTranType, string FolioNumber, int exportFlag)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFTransactionsCmd;
            List<EQTransactionVo> eqTransactionsList = new List<EQTransactionVo>();
            EQTransactionVo EqTransactionVo = new EQTransactionVo();

            Count = 0;
            genDictTranType = new Dictionary<string, string>();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SP_GetAdviserCustomerEQTransactions");
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserId", DbType.Int32, adviserId);
                if (GroupHeadId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, GroupHeadId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@GroupHeadId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FromDate", DbType.DateTime, From);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ToDate", DbType.DateTime, To);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Manage", DbType.Int32, Manage);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CurrentPage", DbType.Int32, CurrentPage);

                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ExportFlag", DbType.Int32, exportFlag);

                if (CustomerName != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, CustomerName);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerName", DbType.String, DBNull.Value);
                if (Scrip != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scrip", DbType.String, Scrip);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Scrip", DbType.String, DBNull.Value);
                if (TranType != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, TranType);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@TranType", DbType.String, DBNull.Value);
                if (FolioNumber != string.Empty)
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, FolioNumber);
                else
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FolioNumber", DbType.String, DBNull.Value);


                ds = db.ExecuteDataSet(getRMCustomerMFTransactionsCmd);


                if (ds.Tables.Count > 1 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        genDictTranType.Add(dr["WETT_TransactionTypeName"].ToString(), dr["WETT_TransactionCode"].ToString());
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetAdviserCustomerMFTransactions()");
                object[] objects = new object[5];
                objects[0] = adviserId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());

            return ds;
            //return eqTransactionsList;
        }
        #endregion MF MUltiple Transaction View


        #region MFFolio

        public List<CustomerAccountsVo> GetCustomerMFFolios(int PortfolioId, int CustomerId)
        {
            DataSet ds = null;
            Database db;
            DbCommand getCustomerMFFOlioCmd;
            List<CustomerAccountsVo> AccountList = null;
            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            DataTable dtMFFOlio = new DataTable();
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerMFFOlioCmd = db.GetStoredProcCommand("SP_GetCustomerMFFolios");
                db.AddInParameter(getCustomerMFFOlioCmd, "@PortfolioId", DbType.Int32, PortfolioId);
                db.AddInParameter(getCustomerMFFOlioCmd, "@CustomerId", DbType.Int32, CustomerId);
                //db.AddInParameter(getCustomerMFFOlioCmd, "@currentPage", DbType.Int32, CurrentPage);
                ds = db.ExecuteDataSet(getCustomerMFFOlioCmd);

                DataRow[] drJointHolder;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtMFFOlio = ds.Tables[0];
                    AccountList = new List<CustomerAccountsVo>();
                    foreach (DataRow dr in dtMFFOlio.Rows)
                    {
                        string jointHolderName = "";
                        AccountVo = new CustomerAccountsVo();
                        AccountVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        AccountVo.AccountNum = dr["CMFA_FolioNum"].ToString();
                        if (dr["ADUL_ProcessId"].ToString() == null || dr["ADUL_ProcessId"].ToString() == "")
                        {
                            AccountVo.ProcessId = 0;
                        }
                        else
                        {
                            AccountVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        }
                        if (dr["CMFA_AccountOpeningDate"].ToString() != string.Empty)
                            AccountVo.AccountOpeningDate = DateTime.Parse(dr["CMFA_AccountOpeningDate"].ToString());
                        else
                            AccountVo.AccountOpeningDate = DateTime.MinValue;

                        if (dr["PA_AMCCode"].ToString() != string.Empty)
                            AccountVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        if (dr["PA_AMCName"].ToString() != string.Empty)
                            AccountVo.AMCName = dr["PA_AMCName"].ToString();
                        //AccountVo.Name = dr["CMFA_INV_NAME"].ToString(); // to capture the original cosumer name

                        if (dr["XMOH_ModeOfHolding"].ToString() != string.Empty)
                            AccountVo.ModeOfHolding = dr["XMOH_ModeOfHolding"].ToString();
                        if (dr["XMOH_ModeOfHoldingCode"].ToString() != string.Empty)

                            AccountVo.ModeOfHoldingCode = dr["XMOH_ModeOfHoldingCode"].ToString();
                        if (dr["CMFA_IsOnline"].ToString() != string.Empty)

                            AccountVo.IsOnline =Convert.ToInt32(dr["CMFA_IsOnline"]);

                        if (dr["CMFA_SubBrokerCode"].ToString() != string.Empty)

                            AccountVo.AssociateCode = dr["XMOH_ModeOfHoldingCode"].ToString();
                        if (AccountVo.ModeOfHoldingCode == "JO")
                        {
                            drJointHolder = ds.Tables[2].Select("CMFA_AccountId=" + AccountVo.AccountId + "AND CMFAA_AssociationType =" + "'" + "Joint Holder" + "'");

                            if (drJointHolder.Count() > 0)
                            {
                                foreach (DataRow dr1 in drJointHolder)
                                {
                                    if (jointHolderName != "")
                                    {
                                        jointHolderName = jointHolderName + "/" + dr1["JointHolderName"].ToString();
                                    }
                                    else
                                    {
                                        jointHolderName = dr1["JointHolderName"].ToString();
                                    }
                                }
                            }
                        }
                        AccountVo.Name = jointHolderName;
                        AccountList.Add(AccountVo);

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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolios()");
                object[] objects = new object[2];
                objects[0] = PortfolioId;
                objects[1] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            //Count = Int32.Parse(ds.Tables[1].Rows[0]["cnt"].ToString());
            return AccountList;
        }


        public CustomerAccountsVo GetCustomerMFFolioDetails(int FolioId)
        {

            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            CustomerBankAccountVo CustomerBankAccountVo = new CustomerBankAccountVo();


            Database db;
            DbCommand getMFTransactionCmd;
            DataSet dsGetMFTransaction;
            DataRow dr;
            DataRow dr1;
            int bankId = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFTransactionCmd = db.GetStoredProcCommand("SP_GetCustomerMFFolioDetails");
                db.AddInParameter(getMFTransactionCmd, "@FolioId", DbType.Int32, FolioId);

                dsGetMFTransaction = db.ExecuteDataSet(getMFTransactionCmd);
                if (dsGetMFTransaction.Tables[0].Rows.Count > 0)
                {
                    dr = dsGetMFTransaction.Tables[0].Rows[0];
                    AccountVo = new CustomerAccountsVo();
                    if (dr["CMFA_AccountId"].ToString() != string.Empty)
                        AccountVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                    AccountVo.AccountNum = dr["CMFA_FolioNum"].ToString();
                    if (dr["CMFA_AccountOpeningDate"].ToString() != string.Empty)
                        AccountVo.AccountOpeningDate = DateTime.Parse(dr["CMFA_AccountOpeningDate"].ToString());
                    else
                        AccountVo.AccountOpeningDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(dr["PA_AMCCode"].ToString()))
                        AccountVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                    Int32.TryParse(dr["CB_CustPrimaryBankAccId"].ToString(), out bankId);
                    if (dr["CMFA_IsJointlyHeld"].ToString() != string.Empty)
                        AccountVo.IsJointHolding = int.Parse(dr["CMFA_IsJointlyHeld"].ToString());
                    if (dr["CMFA_IsOnline"].ToString() != string.Empty)
                        AccountVo.IsOnline = int.Parse(dr["CMFA_IsOnline"].ToString());
                    AccountVo.ModeOfHoldingCode = dr["XMOH_ModeOfHoldingCode"].ToString();
                    AccountVo.BankId = bankId;
                    AccountVo.Name = dr["CMFA_INV_NAME"].ToString();
                    //if (dr["CMFA_SubBrokerCode"].ToString() != string.Empty)
                        AccountVo.AssociateCode = dr["CMFA_SubBrokerCode"].ToString();
                    //newly added
                    AccountVo.CAddress1 = dr["CMGCXP_ADDRESS1"].ToString();
                    AccountVo.CAddress2 = dr["CMGCXP_ADDRESS2"].ToString();
                    AccountVo.CAddress3 = dr["CMGCXP_ADDRESS3"].ToString();
                    AccountVo.CCity = dr["CMGCXP_CITY"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMGCXP_PINCODE"].ToString()))
                        AccountVo.CPinCode = int.Parse(dr["CMGCXP_PINCODE"].ToString());
                    AccountVo.JointName1 = dr["CMGCXP_JOINT_NAME1"].ToString();
                    AccountVo.JointName2 = dr["CMGCXP_JOINT_NAME2"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMGCXP_PHONE_OFF"].ToString()))
                        AccountVo.CPhoneOffice = Convert.ToInt64(dr["CMGCXP_PHONE_OFF"].ToString());
                    if (!string.IsNullOrEmpty(dr["CMGCXP_PHONE_RES"].ToString()))
                        AccountVo.CPhoneRes = Convert.ToInt64(dr["CMGCXP_PHONE_RES"].ToString());
                    if (!string.IsNullOrEmpty(dr["CMGCXP_PHONE_OFF"].ToString()))
                        AccountVo.CEmail = dr["CMGCXP_EMAIL"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMFA_PANNO"].ToString()))
                        AccountVo.PanNumber = dr["CMFA_PANNO"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMFA_BROKERCODE"].ToString()))
                        AccountVo.BrokerCode = dr["CMFA_BROKERCODE"].ToString();
                    if (!string.IsNullOrEmpty(dr["XCT_CustomerTypeCode"].ToString()))
                        AccountVo.XCT_CustomerTypeCode = dr["XCT_CustomerTypeCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["XCST_CustomerSubTypeCode"].ToString()))
                        AccountVo.XCST_CustomerSubTypeCode = dr["XCST_CustomerSubTypeCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["CMFA_BankNameInExtFile"].ToString()))
                        AccountVo.BankNameInExtFile = dr["CMFA_BankNameInExtFile"].ToString();


                    if (!string.IsNullOrEmpty(dr["CMGCXP_DOB"].ToString()))
                        AccountVo.CDOB = DateTime.Parse(dr["CMGCXP_DOB"].ToString());

                }

                if (dsGetMFTransaction.Tables[1].Rows.Count > 0)
                {
                    dr1 = dsGetMFTransaction.Tables[1].Rows[0];
                    AccountVo.AccountType = dr1["PAIC_AssetInstrumentCategoryCode"].ToString();
                    AccountVo.BankAccountNum = dr1["CB_AccountNum"].ToString();
                    AccountVo.ModeOfOperation = dr1["XMOH_ModeOfHoldingCode"].ToString();
                    if (!string.IsNullOrEmpty(dr1["WERPBDTM_BankName"].ToString()))
                        AccountVo.BankName = dr1["WERPBDTM_BankName"].ToString();
                    AccountVo.BranchName = dr1["CB_BranchName"].ToString();
                    AccountVo.BranchAdrLine1 = dr1["CB_BranchAdrLine1"].ToString();
                    AccountVo.BranchAdrLine2 = dr1["CB_BranchAdrLine2"].ToString();
                    AccountVo.BranchAdrLine3 = dr1["CB_BranchAdrLine3"].ToString();
                    AccountVo.BranchAdrCity = dr1["CB_BranchAdrCity"].ToString();
                    if (!string.IsNullOrEmpty(dr1["CB_BranchAdrPinCode"].ToString()))
                        AccountVo.BranchAdrPinCode = int.Parse(dr1["CB_BranchAdrPinCode"].ToString());
                    if (!string.IsNullOrEmpty(dr1["CB_MICR"].ToString()))
                        AccountVo.MICR = dr1["CB_MICR"].ToString();
                    if (!string.IsNullOrEmpty(dr1["CB_IFSC"].ToString()))
                        AccountVo.IFSC = dr1["CB_IFSC"].ToString();
                    if (!string.IsNullOrEmpty(dr1["WERPBM_BankCode"].ToString()))
                        AccountVo.MCmgcxpBankCode = dr1["WERPBM_BankCode"].ToString();
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetCustomerMFFolioDetails()");
                object[] objects = new object[1];
                objects[0] = FolioId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }



            return AccountVo;


        }

        public bool UpdateCustomerMFFolioDetails(CustomerAccountsVo AccountVo, int userId)
        {
            bool blResult = false;
            Database db;
            DbCommand updateMFFolioDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateMFFolioDetailsCmd = db.GetStoredProcCommand("SP_UpdateCustomerMFFolioDetails");
                db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_AccountId", DbType.Int32, AccountVo.AccountId);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_FolioNum", DbType.String, AccountVo.AccountNum);
                db.AddInParameter(updateMFFolioDetailsCmd, "@PA_AMCCode", DbType.Int32, AccountVo.AMCCode);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_IsJointlyHeld", DbType.Int32, AccountVo.IsJointHolding);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_IsOnline", DbType.Int32, AccountVo.IsOnline);
                db.AddInParameter(updateMFFolioDetailsCmd, "@WCMV_LookupId_BankId", DbType.Int32, AccountVo.BankId);
                db.AddInParameter(updateMFFolioDetailsCmd, "@WCMV_LookupId_AccType", DbType.Int32, AccountVo.BankAccTypeId);
                if (AccountVo.AccountOpeningDate != DateTime.MinValue)
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_AccountOpeningDate", DbType.DateTime, AccountVo.AccountOpeningDate);
                else
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_AccountOpeningDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateMFFolioDetailsCmd, "@XMOH_ModeOfHoldingCode", DbType.String, AccountVo.ModeOfHoldingCode);


                #region newly added
                db.AddInParameter(updateMFFolioDetailsCmd, "@CP_PortfolioId", DbType.Int32, AccountVo.PortfolioId);
                db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_SubBrokerCode", DbType.String, AccountVo.AssociateCode);
                db.AddInParameter(updateMFFolioDetailsCmd, "@PAG_AssetGroupCode", DbType.String, AccountVo.AssetClass);


                db.AddInParameter(updateMFFolioDetailsCmd, "@ModifiedBy", DbType.Int32, userId);
                if (AccountVo.BankId != 0)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_CustBankAccId", DbType.Int32, AccountVo.BankId);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_CustBankAccId", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.Name))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_InvestorName", DbType.String, AccountVo.Name);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_InvestorName", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.PanNumber))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_PANNO", DbType.String, AccountVo.PanNumber);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_PANNO", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.BrokerCode))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_BROKERCODE", DbType.String, AccountVo.BrokerCode);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMFA_BROKERCODE", DbType.String, DBNull.Value);
                }

                if (AccountVo.XCT_CustomerTypeCode != null && AccountVo.XCT_CustomerTypeCode != "0")
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@XCT_CustomerTypeCode", DbType.String, AccountVo.XCT_CustomerTypeCode);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@XCT_CustomerTypeCode", DbType.String, DBNull.Value);
                }

                if (AccountVo.XCST_CustomerSubTypeCode != "0")
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@XCST_CustomerSubTypeCode", DbType.String, AccountVo.XCST_CustomerSubTypeCode);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@XCST_CustomerSubTypeCode", DbType.String, DBNull.Value);
                }



                if (!string.IsNullOrEmpty(AccountVo.CAddress1))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_ADDRESS1", DbType.String, AccountVo.CAddress1);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_ADDRESS1", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.CAddress2))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_ADDRESS2", DbType.String, AccountVo.CAddress2);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_ADDRESS2", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.CAddress3))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_ADDRESS3", DbType.String, AccountVo.CAddress3);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_ADDRESS3", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.CCity))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_CITY", DbType.String, AccountVo.CCity);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_CITY", DbType.String, DBNull.Value);
                }
                if (AccountVo.CPinCode != 0)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_PINCODE", DbType.Int32, AccountVo.CPinCode);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_PINCODE", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.JointName1))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_JOINT_NAME1", DbType.String, AccountVo.JointName1);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_JOINT_NAME1", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.JointName2))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_JOINT_NAME2", DbType.String, AccountVo.JointName2);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_JOINT_NAME2", DbType.String, DBNull.Value);
                }
                if (AccountVo.CPhoneOffice != 0)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_PHONE_OFF", DbType.Double, AccountVo.CPhoneOffice);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_PHONE_OFF", DbType.Double, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.Name))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_PHONE_RES", DbType.Double, AccountVo.CPhoneRes);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_PHONE_RES", DbType.Double, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.CEmail))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_EMAIL", DbType.String, AccountVo.CEmail);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_EMAIL", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.CMGCXP_BankCity))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_BankCity", DbType.String, AccountVo.CMGCXP_BankCity);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_BankCity", DbType.String, DBNull.Value);
                }
                if (AccountVo.CDOB != DateTime.MinValue)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_DOB", DbType.DateTime, AccountVo.CDOB);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CMGCXP_DOB", DbType.DateTime, DBNull.Value);
                }

                //added for Bank details 

                if (!string.IsNullOrEmpty(AccountVo.BankName))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@WERPBM_BankCode", DbType.String, AccountVo.BankName);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@WERPBM_BankCode", DbType.String, DBNull.Value);
                }

                if (AccountVo.CustomerId != 0)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@C_CustomerId", DbType.Int32, AccountVo.CustomerId);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@C_CustomerId", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.AccountType))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, AccountVo.AccountType);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(AccountVo.BankAccountNum))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_AccountNum", DbType.String, AccountVo.BankAccountNum);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_AccountNum", DbType.String, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(AccountVo.BranchName))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchName", DbType.String, AccountVo.BranchName);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchName", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.BranchAdrLine1))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrLine1", DbType.String, AccountVo.BranchAdrLine1);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrLine1", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.BranchAdrLine2))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrLine2", DbType.String, AccountVo.BranchAdrLine2);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrLine2", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.BranchAdrLine3))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrLine3", DbType.String, AccountVo.BranchAdrLine3);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrLine3", DbType.String, DBNull.Value);
                }
                if (AccountVo.BranchAdrPinCode != 0)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrPinCode", DbType.Int32, AccountVo.BranchAdrPinCode);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrPinCode", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.BranchAdrCity))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrCity", DbType.String, AccountVo.BranchAdrCity);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrCity", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.BranchAdrState))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrState", DbType.String, AccountVo.BranchAdrState);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrState", DbType.String, DBNull.Value);
                }

                db.AddInParameter(updateMFFolioDetailsCmd, "@CB_BranchAdrCountry", DbType.String, "India");


                if (AccountVo.Balance != 0)
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_Balance", DbType.Double, AccountVo.Balance);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_Balance", DbType.Double, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.MICR))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_MICR", DbType.String, AccountVo.MICR);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_MICR", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(AccountVo.IFSC))
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_IFSC", DbType.String, AccountVo.IFSC);
                }
                else
                {
                    db.AddInParameter(updateMFFolioDetailsCmd, "@CB_IFSC", DbType.String, DBNull.Value);
                }

              

               
               #endregion



                if (db.ExecuteNonQuery(updateMFFolioDetailsCmd) != 0)
                    blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateCustomerMFFolioDetails()");
                object[] objects = new object[2];
                objects[0] = AccountVo;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        public DataSet GetMFFolioAccountAssociates(int FolioId, int CustomerId)
        {


            Database db;
            DbCommand getMFFolioAccountAssociatesCmd;
            DataSet dsGetMFFolioAccountAssociates;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFFolioAccountAssociatesCmd = db.GetStoredProcCommand("SP_GetMFFolioAssociates");
                db.AddInParameter(getMFFolioAccountAssociatesCmd, "@CMFA_AccountId", DbType.Int32, FolioId);
                db.AddInParameter(getMFFolioAccountAssociatesCmd, "@CustomerId", DbType.Int32, CustomerId);
                dsGetMFFolioAccountAssociates = db.ExecuteDataSet(getMFFolioAccountAssociatesCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFFolioAccountAssociates()");
                object[] objects = new object[2];
                objects[0] = FolioId;
                objects[1] = CustomerId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsGetMFFolioAccountAssociates;


        }


        public bool DeleteMFFolioAccountAssociates(int FolioId)
        {
            bool blResult = false;
            Database db;
            DbCommand deleteMFFolioDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteMFFolioDetailsCmd = db.GetStoredProcCommand("SP_DeleteMFFolioAccountAssociates");
                db.AddInParameter(deleteMFFolioDetailsCmd, "@CMFA_AccountId", DbType.Int32, FolioId);

                if (db.ExecuteNonQuery(deleteMFFolioDetailsCmd) != 0)
                    blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:UpdateCustomerMFFolioDetails()");
                object[] objects = new object[1];
                objects[0] = FolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        #endregion MFFolio

        // To get whether supplied Trasaction Classification Name is Buy or sell
        public string GetTransactionType(string transname)
        {
            string transactionType = "";
            Database db;
            DbCommand dbTransactionType;
            DataSet dsTransactionType;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbTransactionType = db.GetStoredProcCommand("SP_GetTransactionType");
                db.AddInParameter(dbTransactionType, "@TransactionName", DbType.String, transname);
                dsTransactionType = db.ExecuteDataSet(dbTransactionType);
                transactionType = dsTransactionType.Tables[0].Rows[0]["WMTT_BuySell"].ToString();
            }
            catch (BaseApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(ex.Message, ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetTransactionType()");
                object[] objects = new object[1];
                objects[0] = transname;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return transactionType;
        }

        /// <summary>
        /// To check whether TradeAccount No. associated with Transaction <<Vinayak Patil>>
        /// </summary>
        /// <param name="eqTradeAccId"></param>
        /// <returns></returns>

        public bool CheckEQTradeAccNoAssociatedWithTransactions(int eqTradeAccId)
        {
            bool bResult = false;
            Database db;
            DbCommand CheckEQTradeAccNoAssociatedWithTransactionsCmd;
            DataSet dsCheckEQTradeAccNoAssociatedWithTransactions = new DataSet();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckEQTradeAccNoAssociatedWithTransactionsCmd = db.GetStoredProcCommand("CheckFolioAndTradeAssociation");

                db.AddInParameter(CheckEQTradeAccNoAssociatedWithTransactionsCmd, "@EQTrade_AccId", DbType.Int32, eqTradeAccId);
                dsCheckEQTradeAccNoAssociatedWithTransactions = db.ExecuteDataSet(CheckEQTradeAccNoAssociatedWithTransactionsCmd);
                if (dsCheckEQTradeAccNoAssociatedWithTransactions.Tables[0].Rows[0]["NoOfEQRecords"].ToString() != "0")

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CheckEQTradeAccNoAssociatedWithTransactions()");

                object[] objects = new object[1];
                objects[0] = eqTradeAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// To Delete Trade Account <<Vinayak Patil>>
        /// </summary>
        /// <param name="eqTradeAccId"></param>
        /// <returns></returns>

        public bool DeleteTradeAccount(int eqTradeAccId)
        {
            bool bResult = false;
            Database db;
            DbCommand DeleteTradeAccountCmd;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteTradeAccountCmd = db.GetStoredProcCommand("SP_DeleteEQTradeAccount");

                db.AddInParameter(DeleteTradeAccountCmd, "@EQTrade_AccId", DbType.Int32, eqTradeAccId);
                if (db.ExecuteNonQuery(DeleteTradeAccountCmd) != 0)

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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:DeleteTradeAccount()");
                object[] objects = new object[2];
                objects[0] = eqTradeAccId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }

        /// <summary>
        /// To check whether MFFolio associated with MF Transaction <<Vinayak Patil>>
        /// </summary>
        /// <param name="FolioId"></param>
        /// <returns></returns>

        public bool CheckMFFOlioAssociatedWithTransactions(int FolioId)
        {
            bool bResult = false;
            Database db;
            DbCommand CheckEQTradeAccNoAssociatedWithTransactionsCmd;
            DataSet dsCheckEQTradeAccNoAssociatedWithTransactions = new DataSet();

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CheckEQTradeAccNoAssociatedWithTransactionsCmd = db.GetStoredProcCommand("CheckFolioAndTradeAssociation");

                db.AddInParameter(CheckEQTradeAccNoAssociatedWithTransactionsCmd, "@MFFolio_AccId", DbType.Int32, FolioId);
                dsCheckEQTradeAccNoAssociatedWithTransactions = db.ExecuteDataSet(CheckEQTradeAccNoAssociatedWithTransactionsCmd);
                if (dsCheckEQTradeAccNoAssociatedWithTransactions.Tables[1].Rows[0]["NoOfMFRecords"].ToString() != "0")

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CheckEQTradeAccNoAssociatedWithTransactions()");

                object[] objects = new object[1];
                objects[0] = FolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        /// <summary>
        /// To Delete MF Folio <<Vinayak Patil>>
        /// </summary>
        /// <param name="FolioId"></param>
        /// <returns></returns>

        public bool DeleteMFFolio(int FolioId)
        {
            bool bResult = false;
            int count = 0;
            Database db;
            DbCommand DeleteTradeAccountCmd;
            DataSet dsDeleteTradeAccount = new DataSet();
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                DeleteTradeAccountCmd = db.GetStoredProcCommand("SP_DeleteMFFolio");

                db.AddInParameter(DeleteTradeAccountCmd, "@MFFolio_AccId", DbType.Int32, FolioId);
                db.AddOutParameter(DeleteTradeAccountCmd, "@CountFlag", DbType.Int32, 50);
                DeleteTradeAccountCmd.CommandTimeout = 60 * 60;
                dsDeleteTradeAccount = db.ExecuteDataSet(DeleteTradeAccountCmd);

                Object objFromDate = db.GetParameterValue(DeleteTradeAccountCmd, "@CountFlag");
                if (objFromDate != DBNull.Value)
                    count = Int32.Parse(db.GetParameterValue(DeleteTradeAccountCmd, "@CountFlag").ToString());

                if (count > 0)
                    bResult = false;

                else
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:DeleteTradeAccount()");
                object[] objects = new object[2];
                objects[0] = FolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool CancelEquityTransaction(EQTransactionVo eqTransactionVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand CancelEQTrnxCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CancelEQTrnxCmd = db.GetStoredProcCommand("SP_CancelEquityTransaction");
                db.AddInParameter(CancelEQTrnxCmd, "@EqTransId", DbType.Int32, eqTransactionVo.TransactionId);
                db.AddInParameter(CancelEQTrnxCmd, "@CETA_AccountId", DbType.Int32, eqTransactionVo.AccountId);
                db.AddInParameter(CancelEQTrnxCmd, "@PEM_ScripCode", DbType.Int32, eqTransactionVo.ScripCode);
                if (!string.IsNullOrEmpty(eqTransactionVo.TradeAccountNum))
                    db.AddInParameter(CancelEQTrnxCmd, "@CET_TradeNum", DbType.Decimal, eqTransactionVo.TradeAccountNum);
                else
                    db.AddInParameter(CancelEQTrnxCmd, "@CET_TradeNum", DbType.String, DBNull.Value);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_OrderNum", DbType.Decimal, eqTransactionVo.OrderNum);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_BuySell", DbType.String, eqTransactionVo.BuySell);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_IsSpeculative", DbType.String, eqTransactionVo.IsSpeculative);
                if (!string.IsNullOrEmpty(eqTransactionVo.Exchange))
                    db.AddInParameter(CancelEQTrnxCmd, "@XE_ExchangeCode", DbType.String, eqTransactionVo.Exchange);
                else
                    db.AddInParameter(CancelEQTrnxCmd, "@XE_ExchangeCode", DbType.String, DBNull.Value);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_TradeDate", DbType.DateTime, eqTransactionVo.TradeDate);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_Rate", DbType.Decimal, eqTransactionVo.Rate);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_Quantity", DbType.Decimal, eqTransactionVo.Quantity);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_Brokerage", DbType.Decimal, eqTransactionVo.Brokerage);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_ServiceTax", DbType.Decimal, eqTransactionVo.ServiceTax);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_EducationCess", DbType.Decimal, eqTransactionVo.EducationCess);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_STT", DbType.Decimal, eqTransactionVo.STT);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_OtherCharges", DbType.Decimal, eqTransactionVo.OtherCharges);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_RateInclBrokerage", DbType.Decimal, eqTransactionVo.RateInclBrokerage);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_TradeTotal", DbType.Decimal, eqTransactionVo.TradeTotal);
                db.AddInParameter(CancelEQTrnxCmd, "@XB_BrokerCode", DbType.String, eqTransactionVo.BrokerCode);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_IsSplit", DbType.Int16, eqTransactionVo.IsSplit);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_SplitCustEqTransId", DbType.Int32, eqTransactionVo.SplitTransactionId);
                //db.AddInParameter(CancelEQTrnxCmd, "@XES_SourceCode", DbType.String, eqTransactionVo.SourceCode);
                db.AddInParameter(CancelEQTrnxCmd, "@WETT_TransactionCode", DbType.Int16, eqTransactionVo.TransactionCode);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_IsSourceManual", DbType.Int16, eqTransactionVo.IsSourceManual);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_ModifiedBy", DbType.String, userId);
                db.AddInParameter(CancelEQTrnxCmd, "@CET_CreatedBy", DbType.String, userId);

                if (db.ExecuteNonQuery(CancelEQTrnxCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:CancelMFTransaction(MFTransactionVo mfTransactionVo,int userId)");


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

        public DataSet GetEquityMISDetails(int EQAccountId)
        {
            DataSet ds = null;
            Database db;
            DbCommand getLastTradeDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLastTradeDateCmd = db.GetStoredProcCommand("SPROC_GetEquityMISDetails");
                ds = db.ExecuteDataSet(getLastTradeDateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityMISDetails(int EQAccountId)");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public DataSet GetLastMFTradeDate()
        {
            DataSet ds = null;
            Database db;
            DbCommand getLastTradeDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getLastTradeDateCmd = db.GetStoredProcCommand("SP_GetLastMFValuationDate");
                ds = db.ExecuteDataSet(getLastTradeDateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetLastTradeDate()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public List<MFTransactionVo> GetRMCustomerMFBalance(int RMId, int AdviserID, int GroupHeadId, DateTime From, DateTime To, int Manage, int AccountId,int SchemePlanCode, int AmcCode, string Category, int A_AgentCodeBased, string AgentCode, string UserType)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFBalanceCmd;
            List<MFTransactionVo> mfBalanceList = new List<MFTransactionVo>();
            MFTransactionVo mfBalanceVo = new MFTransactionVo();
            DataTable dtGetMFBalance;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                getRMCustomerMFBalanceCmd = db.GetStoredProcCommand("SP_GetRMCustomerMFBalance");
                //}

                if (RMId != 0)
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@RMId", DbType.Int32, RMId);
                else
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@RMId", DbType.Int32, DBNull.Value);

                if (AdviserID != 0)
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AdviserID", DbType.Int32, AdviserID);
                else
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AdviserID", DbType.Int32, DBNull.Value);


                if (GroupHeadId != 0)
                {
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@GroupHeadId", DbType.Int32, GroupHeadId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@GroupHeadId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(getRMCustomerMFBalanceCmd, "@UserType", DbType.String,UserType);
                db.AddInParameter(getRMCustomerMFBalanceCmd, "@FromDate", DbType.DateTime, From);
                db.AddInParameter(getRMCustomerMFBalanceCmd, "@ToDate", DbType.DateTime, To);
                db.AddInParameter(getRMCustomerMFBalanceCmd, "@Manage", DbType.Int32, Manage);
                if (AccountId != 0)
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AccountId", DbType.String, AccountId);
                else
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AccountId", DbType.String, DBNull.Value);
                if (AgentCode != "0")
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AgentCode", DbType.String, AgentCode);
                else
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AgentCode", DbType.String, DBNull.Value);
                if (AmcCode != 0)
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AmcCode", DbType.Int32, AmcCode);
                else
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AmcCode", DbType.Int32, DBNull.Value);
                if (Category != "0")
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@Category", DbType.String, Category);
                else
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@Category", DbType.String, DBNull.Value);
                if (SchemePlanCode != 0)
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@SchemePlanCode", DbType.String, SchemePlanCode);
                else
                    db.AddInParameter(getRMCustomerMFBalanceCmd, "@SchemePlanCode", DbType.String, DBNull.Value);
                //if (AgentId != 0)
                //    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AAC_AdviserAgentId", DbType.Int32, AgentId);
                //else
                //    db.AddInParameter(getRMCustomerMFBalanceCmd, "@AAC_AdviserAgentId", DbType.Int32, DBNull.Value);
                db.AddInParameter(getRMCustomerMFBalanceCmd, "@IsAgentBasedCode", DbType.Int32, A_AgentCodeBased);
                
                getRMCustomerMFBalanceCmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(getRMCustomerMFBalanceCmd);
               
                if (ds.Tables[0].Rows.Count > 0)
                {
  
                    dtGetMFBalance = ds.Tables[0];
                    mfBalanceList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFBalance.Rows)
                    {
                        mfBalanceVo = new MFTransactionVo();
                        if (dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != null && dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != string.Empty)
                            mfBalanceVo.SubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
                        else
                            mfBalanceVo.SubCategoryName = "N/A";

                        mfBalanceVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfBalanceVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfBalanceVo.CustomerName = dr["Name"].ToString();
                        mfBalanceVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfBalanceVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfBalanceVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfBalanceVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfBalanceVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        mfBalanceVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfBalanceVo.AMCCode = int.Parse(dr["PA_AmcCode"].ToString());
                        if (dr["CMFT_SubBrokerCode"].ToString() != null && dr["CMFT_SubBrokerCode"].ToString() != string.Empty)
                        {
                            mfBalanceVo.SubBrokerCode = dr["CMFT_SubBrokerCode"].ToString();
                        }
                        else
                        {
                            mfBalanceVo.SubBrokerCode = "N/A";
                        }
                        if (dr["PA_AMCName"].ToString() != null && dr["PA_AMCName"].ToString()!=string.Empty)
                        {
                            mfBalanceVo.AMCName = dr["PA_AMCName"].ToString();
                        }
                        else
                        {
                            mfBalanceVo.AMCName = "N/A";
                        }
                        if (A_AgentCodeBased == 1)
                        {
                            if (dr["ZonalManagerName"].ToString() != null && dr["ZonalManagerName"].ToString() != string.Empty)
                            {
                                mfBalanceVo.ZMName = dr["ZonalManagerName"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.ZMName = "N/A";
                            }
                            if (dr["AreaManager"].ToString() != null && dr["AreaManager"].ToString() != string.Empty)
                            {
                                mfBalanceVo.AName = dr["AreaManager"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.AName = "N/A";
                            }
                            if (dr["AssociatesName"].ToString() != null && dr["AssociatesName"].ToString() != string.Empty)
                            {
                                mfBalanceVo.SubbrokerName = dr["AssociatesName"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.SubbrokerName = "N/A";
                            }
                            if (dr["ChannelName"].ToString() != null && dr["ChannelName"].ToString() != string.Empty)
                            {
                                mfBalanceVo.Channel = dr["ChannelName"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.Channel = "N/A";
                            }

                            if (dr["Titles"].ToString() != null && dr["Titles"].ToString() != string.Empty)
                            {
                                mfBalanceVo.Titles = dr["Titles"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.Titles = "N/A";
                            }
                            if (dr["ClusterManager"].ToString() != null && dr["ClusterManager"].ToString() != string.Empty)
                            {
                                mfBalanceVo.ClusterMgr = dr["ClusterManager"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.ClusterMgr = "N/A";
                            }
                            if (dr["ReportingManagerName"].ToString() != null && dr["ReportingManagerName"].ToString() != string.Empty)
                            {
                                mfBalanceVo.ReportingManagerName = dr["ReportingManagerName"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.ReportingManagerName = "N/A";
                            }
                            if (dr["UserType"].ToString() != null && dr["UserType"].ToString() != string.Empty)
                            {
                                mfBalanceVo.UserType = dr["UserType"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.UserType = "N/A";
                            }
                            if (dr["DeputyHead"].ToString() != null && dr["DeputyHead"].ToString() != string.Empty)
                            {
                                mfBalanceVo.DeuptyHead = dr["DeputyHead"].ToString();
                            }
                            else
                            {
                                mfBalanceVo.DeuptyHead = "N/A";
                            }


                        }
                        mfBalanceVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfBalanceVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        mfBalanceVo.Amount = double.Parse(dr["CMFT_Amount"].ToString());
                        mfBalanceVo.Units = double.Parse(dr["CMFT_Units"].ToString());
                        mfBalanceVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfBalanceVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfBalanceVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfBalanceVo.Age = int.Parse(dr["CMFTB_Age"].ToString());
                        mfBalanceVo.Balance = double.Parse(dr["ABS_Return"].ToString());
                        mfBalanceVo.CurrentValue = double.Parse(dr["CMFTB_CurrentValue"].ToString());
                        mfBalanceVo.NAV = float.Parse(dr["NAV"].ToString());
                        mfBalanceList.Add(mfBalanceVo);
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetRMCustomerMFBalance()");
                object[] objects = new object[3];
                objects[0] = RMId;
                objects[1] = GroupHeadId;
                objects[2] = From;
                objects[3] = To;
                objects[4] = Manage;
                //objects[5] = Scheme;
                //objects[6] = genDictTranType;
                //objects[7] = FolioNumber;
                //objects[8] = CustomerName;
                //objects[9] = CurrentPage;
                //objects[10] = PasssedFolioValue;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            //
            //if (ds.Tables.Count > 1)
            //{
            //    if (ds.Tables[1].Rows.Count > 0)
            //        Count = Int32.Parse(ds.Tables[1].Rows[0]["CNT"].ToString());
            //}
            return mfBalanceList;
        }

        public DataSet GetEquityLedgerMIS(int CustomerId, int TradeAccountId, int BankAccountId)
        {
            DataSet dsGetEqLedgerMIS;
            Database db;
            DbCommand getEqLedgerMISCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEqLedgerMISCmd = db.GetStoredProcCommand("SPROC_GetEquityLedgerMIS");
                db.AddInParameter(getEqLedgerMISCmd, "@CustomerId", DbType.Int32, CustomerId);
                db.AddInParameter(getEqLedgerMISCmd, "@TradeAccountId", DbType.Int32, TradeAccountId);
                db.AddInParameter(getEqLedgerMISCmd, "@BankAccountId", DbType.Int32, BankAccountId);

                dsGetEqLedgerMIS = db.ExecuteDataSet(getEqLedgerMISCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetLastTradeDate()");
                object[] objects = new object[3];
                objects[0] = CustomerId;
                objects[1] = TradeAccountId;
                objects[2] = BankAccountId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetEqLedgerMIS;
        }

        public bool MergeTrailDetailsWithTransaction(int accountId, int trailIdForMerge, int transactionIdForMerge, int IsCompleted, int isMergeManual, string folionoForMerge, int schemeplancodeForMerge, string transactionnoForMerge, double unitsForMerge, double amountForMerge, DateTime transactionDateForMerge, int adviserId)
        {
            bool result = false;
            Database db;
            DbCommand createEquityTransactionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createEquityTransactionCmd = db.GetStoredProcCommand("SP_UpdateTrailTable");

                db.AddInParameter(createEquityTransactionCmd, "@accountId", DbType.Int32, accountId);

                db.AddInParameter(createEquityTransactionCmd, "@trailIdForMerge", DbType.Int32, trailIdForMerge);

                db.AddInParameter(createEquityTransactionCmd, "@transactionIdForMerge", DbType.Int32, transactionIdForMerge);

                db.AddInParameter(createEquityTransactionCmd, "@isMergeManual", DbType.Int32, isMergeManual);

                if (!string.IsNullOrEmpty(folionoForMerge))
                db.AddInParameter(createEquityTransactionCmd, "@folionoForMerge", DbType.String, folionoForMerge);

                if (schemeplancodeForMerge!=0)
                db.AddInParameter(createEquityTransactionCmd, "@schemeplancodeForMerge", DbType.Int32, schemeplancodeForMerge);

                if (!string.IsNullOrEmpty(transactionnoForMerge))                
                db.AddInParameter(createEquityTransactionCmd, "@transactionnoForMerge", DbType.String, transactionnoForMerge);

                if(unitsForMerge!=0.0)
                db.AddInParameter(createEquityTransactionCmd, "@units", DbType.Double, unitsForMerge);


                if (amountForMerge != 0.0)
                db.AddInParameter(createEquityTransactionCmd, "@amount", DbType.Double, amountForMerge);

                if (transactionDateForMerge!=DateTime.MinValue)
                db.AddInParameter(createEquityTransactionCmd, "@transactionDate", DbType.DateTime, transactionDateForMerge);

                db.AddInParameter(createEquityTransactionCmd, "@adviserId", DbType.Int32, adviserId);



                db.AddOutParameter(createEquityTransactionCmd, "isMergeComplete", DbType.Int32, 5000);

                db.ExecuteNonQuery(createEquityTransactionCmd);

                IsCompleted = int.Parse(db.GetParameterValue(createEquityTransactionCmd, "isMergeComplete").ToString());

                if (IsCompleted > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public DataSet GetTransactionDetailsForTrail(int AccountId,int trailIdForMerge,string folionoForMerge,int schemeplancodeForMerge,string transactionnoForMerge,int advisorId)
        {
            DataSet ds = null;
            Database db;
            DbCommand getTransactionDetailsForTrailCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getTransactionDetailsForTrailCmd = db.GetStoredProcCommand("SPROC_GetTransactionDetailsForTrail");
                db.AddInParameter(getTransactionDetailsForTrailCmd, "@AccountId", DbType.Int32, AccountId);

                db.AddInParameter(getTransactionDetailsForTrailCmd, "@folionoForMerge", DbType.String, folionoForMerge);
                db.AddInParameter(getTransactionDetailsForTrailCmd, "@schemeplancodeForMerge", DbType.Int32, schemeplancodeForMerge);
                db.AddInParameter(getTransactionDetailsForTrailCmd, "@transactionnoForMerge", DbType.String, transactionnoForMerge);
                db.AddInParameter(getTransactionDetailsForTrailCmd, "@advisorId", DbType.Int32, advisorId);

                ds = db.ExecuteDataSet(getTransactionDetailsForTrailCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetTransactionDetailsForTrail(int AccountId)");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetTransactionType()
        {
            DataSet ds = null;
            Database db;
            DbCommand getTransactionTypeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getTransactionTypeCmd = db.GetStoredProcCommand("SP_GetEquityTranscationType");
                ds = db.ExecuteDataSet(getTransactionTypeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetTransactionType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public DataSet GetMFTransactionType()
        {
            DataSet ds = null;
            Database db;
            DbCommand getTransactionTypeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getTransactionTypeCmd = db.GetStoredProcCommand("SP_GetMFTranscationType");
                ds = db.ExecuteDataSet(getTransactionTypeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetMFTransactionType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public List<MFTransactionVo> GetCustomerTransactionsBook(int AdviserID, int CustomerId, DateTime From, DateTime To, int Manage, int AmcCode,int AccountId, int SchemePlanCode)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFTransactionsCmd;
            List<MFTransactionVo> mfTransactionsBookList = new List<MFTransactionVo>();
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            DataTable dtGetMFTransactions;
           
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SPROC_Onl_GetCustomerMFTransactionsBook");
                if (AdviserID != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, AdviserID);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, DBNull.Value);
                }
                if (CustomerId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerId", DbType.Int32, CustomerId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                }               
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@FromDate", DbType.DateTime, From);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@ToDate", DbType.DateTime, To);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Manage", DbType.Int32, Manage);
                if (AmcCode != 0)
                { db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMC", DbType.Int32, AmcCode); }

                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMC", DbType.Int32, 0);
                }
                if (AccountId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.Int32, AccountId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.Int32, 0);
                }
                //if (OrderStatus != "0")
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Status", DbType.String, OrderStatus);
                //}
                //else
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Status", DbType.String, DBNull.Value);
                //}

                if (SchemePlanCode != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SchemePlanCode", DbType.Int32, SchemePlanCode);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SchemePlanCode", DbType.Int32, DBNull.Value);
                }
                getRMCustomerMFTransactionsCmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(getRMCustomerMFTransactionsCmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = ds.Tables[0];
                    mfTransactionsBookList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();

                        if (dr["ADUL_ProcessId"].ToString() != null && dr["ADUL_ProcessId"].ToString() != string.Empty)
                            mfTransactionVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        else
                            mfTransactionVo.ProcessId = 0;

                        if (dr["CMFT_SubBrokerCode"].ToString() != null && dr["CMFT_SubBrokerCode"].ToString() != string.Empty)
                            mfTransactionVo.SubBrokerCode = dr["CMFT_SubBrokerCode"].ToString();
                        else
                            mfTransactionVo.SubBrokerCode = "N/A";

                        if (dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != null && dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != string.Empty)
                            mfTransactionVo.SubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
                        else
                            mfTransactionVo.SubCategoryName = "N/A";

                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.CustomerName = dr["Name"].ToString();
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        mfTransactionVo.AMCName = dr["PA_AMCName"].ToString();
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        mfTransactionVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        if(!string.IsNullOrEmpty (dr["CMFT_DividendRate"].ToString()))
                            mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        if (!string.IsNullOrEmpty(dr["CMFT_NAV"].ToString()))
                            mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        if (!string.IsNullOrEmpty(dr["CMFT_Price"].ToString()))
                            mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        if (dr["CMFT_Amount"].ToString() != null && dr["CMFT_Amount"].ToString() != string.Empty)
                        {
                            mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["CMFT_Units"].ToString()))
                            mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        if (dr["CMFT_STT"].ToString() != null && dr["CMFT_STT"].ToString() != string.Empty)
                        {
                            mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        }
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        if (!string.IsNullOrEmpty(dr["CMFT_SwitchSourceTrxId"].ToString()))
                            mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        if (!string.IsNullOrEmpty(dr["WMTT_FinancialFlag"].ToString()))
                            mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());
                        mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfTransactionVo.PortfolioName = dr["CP_PortfolioName"].ToString();
                        mfTransactionVo.DivReinvestmen = dr["CMFOD_DividendOption"].ToString();
                        mfTransactionVo.Divfrequency = dr["DivFrequency"].ToString();
                        if (dr["Co_OrderId"].ToString() != null && dr["Co_OrderId"].ToString() != string.Empty)
                        {
                            mfTransactionVo.orderNo = int.Parse(dr["Co_OrderId"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.orderNo = 0;
                        }
                        
                        mfTransactionVo.channel = dr["Channel"].ToString();
                        if (!string.IsNullOrEmpty(dr["NAV"].ToString()))
                            mfTransactionVo.latestNav = float.Parse(dr["NAV"].ToString());
                       // mfTransactionVo.TrxnNo = (dr["CMFT_TransactionNumber"].ToString());
                       // if (mfTransactionVo.OrdDate != DateTime.MinValue) 
                       if (dr["CO_OrderDate"].ToString() != null && dr["CO_OrderDate"].ToString() != string.Empty)
                        {
                            mfTransactionVo.OrdDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.OrdDate = DateTime.MinValue;
                        }
                       if (dr["CMFT_ELSSMaturityDate"].ToString() != null && dr["CMFT_ELSSMaturityDate"].ToString() != string.Empty)
                       {
                           mfTransactionVo.ELSSMaturityDate = DateTime.Parse(dr["CMFT_ELSSMaturityDate"].ToString());
                       }
                       else
                       {
                           mfTransactionVo.ELSSMaturityDate = DateTime.MinValue;
                       }
                        mfTransactionVo.CreatedOn = DateTime.Parse(dr["CMFT_CreatedOn"].ToString());                       
                        if (dr["CMFT_EUIN"].ToString() != null && dr["CMFT_EUIN"].ToString() != string.Empty)
                        {
                            mfTransactionVo.EUIN = dr["CMFT_EUIN"].ToString();
                        }
                        else
                        {
                            mfTransactionVo.EUIN = "N/A";
                        }
                        if (dr["CMFT_Area"].ToString() != null && dr["CMFT_Area"].ToString() != string.Empty)
                        {
                            mfTransactionVo.Area = dr["CMFT_Area"].ToString();
                        }
                        else
                        {
                            mfTransactionVo.Area = "N/A";
                        }
                        if (dr["WTS_TransactionStatusCode"].ToString() != null && dr["WTS_TransactionStatusCode"].ToString() != string.Empty)
                        {
                            mfTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                            mfTransactionVo.TransactionStatusCode = int.Parse(dr["WTS_TransactionStatusCode"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.TransactionStatus = "OK";
                            mfTransactionVo.TransactionStatusCode = 1;

                        }
                        if (!string.IsNullOrEmpty(dr["CMFT_ExternalBrokerageAmount"].ToString()))
                            mfTransactionVo.BrokerageAmount = float.Parse(dr["CMFT_ExternalBrokerageAmount"].ToString());

                        mfTransactionsBookList.Add(mfTransactionVo);
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[3];               
                objects[0] = From;
                objects[1] = To;
                objects[2] = Manage;              

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }            
            return mfTransactionsBookList;
        }


        public List<MFTransactionVo> GetCustomerTransactionsBookSIP(int AdviserID, int customerId, int SystematicId, int IsSourceAA, int AccountId, int SchemePlanCode, int amount, DateTime SIPStartDate)
        {
            DataSet ds = null;
            Database db;
            DbCommand getRMCustomerMFTransactionsCmd;
            List<MFTransactionVo> mfTransactionsBookList = new List<MFTransactionVo>();
            MFTransactionVo mfTransactionVo = new MFTransactionVo();
            DataTable dtGetMFTransactions;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getRMCustomerMFTransactionsCmd = db.GetStoredProcCommand("SPROC_ONL_GetCustomerMFSIPTransactionsBook");
                if (AdviserID != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, AdviserID);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AdviserID", DbType.Int32, DBNull.Value);
                }
                if (customerId != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerId", DbType.Int32, customerId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@CustomerId", DbType.Int32, DBNull.Value);
                }
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.Int32, AccountId);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SchemePlanCode", DbType.Int32, SchemePlanCode);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@IsSourceAA", DbType.Int32, IsSourceAA);
                db.AddInParameter(getRMCustomerMFTransactionsCmd, "@amount", DbType.Int32, amount);
                //db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SIPStartDate", DbType.DateTime, SIPStartDate);

                //if (AmcCode != 0)
                //{ db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMC", DbType.Int32, AmcCode); }

                //else
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AMC", DbType.Int32, 0);
                //}
                //if (AccountId != 0)
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.Int32, AccountId);
                //}
                //else
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@AccountId", DbType.Int32, 0);
                //}
                //if (OrderStatus != "0")
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Status", DbType.String, OrderStatus);
                //}
                //else
                //{
                //    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@Status", DbType.String, DBNull.Value);
                //}

                if (SchemePlanCode != 0)
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SystemeticId", DbType.Int32, SystematicId);
                }
                else
                {
                    db.AddInParameter(getRMCustomerMFTransactionsCmd, "@SystemeticId", DbType.Int32, DBNull.Value);
                }
                getRMCustomerMFTransactionsCmd.CommandTimeout = 60 * 60;
                ds = db.ExecuteDataSet(getRMCustomerMFTransactionsCmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtGetMFTransactions = ds.Tables[0];
                    mfTransactionsBookList = new List<MFTransactionVo>();
                    foreach (DataRow dr in dtGetMFTransactions.Rows)
                    {
                        mfTransactionVo = new MFTransactionVo();

                        if (dr["ADUL_ProcessId"].ToString() != null && dr["ADUL_ProcessId"].ToString() != string.Empty)
                            mfTransactionVo.ProcessId = int.Parse(dr["ADUL_ProcessId"].ToString());
                        else
                            mfTransactionVo.ProcessId = 0;

                        if (dr["CMFT_SubBrokerCode"].ToString() != null && dr["CMFT_SubBrokerCode"].ToString() != string.Empty)
                            mfTransactionVo.SubBrokerCode = dr["CMFT_SubBrokerCode"].ToString();
                        else
                            mfTransactionVo.SubBrokerCode = "N/A";

                        if (dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != null && dr["PAISC_AssetInstrumentSubCategoryName"].ToString() != string.Empty)
                            mfTransactionVo.SubCategoryName = dr["PAISC_AssetInstrumentSubCategoryName"].ToString();
                        else
                            mfTransactionVo.SubCategoryName = "N/A";

                        mfTransactionVo.TransactionId = int.Parse(dr["CMFT_MFTransId"].ToString());
                        mfTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfTransactionVo.CustomerName = dr["Name"].ToString();
                        mfTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        mfTransactionVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfTransactionVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        mfTransactionVo.AMCName = dr["PA_AMCName"].ToString();
                        mfTransactionVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfTransactionVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfTransactionVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        mfTransactionVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfTransactionVo.BuySell = dr["CMFT_BuySell"].ToString();
                        mfTransactionVo.DividendRate = float.Parse(dr["CMFT_DividendRate"].ToString());
                        mfTransactionVo.TransactionDate = DateTime.Parse(dr["CMFT_TransactionDate"].ToString());
                        mfTransactionVo.NAV = float.Parse(dr["CMFT_NAV"].ToString());
                        mfTransactionVo.Price = float.Parse(dr["CMFT_Price"].ToString());
                        if (dr["CMFT_Amount"].ToString() != null && dr["CMFT_Amount"].ToString() != string.Empty)
                        {
                            mfTransactionVo.Amount = float.Parse(dr["CMFT_Amount"].ToString());
                        }
                        mfTransactionVo.Units = float.Parse(dr["CMFT_Units"].ToString());
                        if (dr["CMFT_STT"].ToString() != null && dr["CMFT_STT"].ToString() != string.Empty)
                        {
                            mfTransactionVo.STT = float.Parse(dr["CMFT_STT"].ToString());
                        }
                        mfTransactionVo.Source = dr["XES_SourceCode"].ToString();
                        mfTransactionVo.SwitchSourceTrxId = int.Parse(dr["CMFT_SwitchSourceTrxId"].ToString());
                        mfTransactionVo.TransactionClassificationCode = dr["WMTT_TransactionClassificationCode"].ToString();
                        mfTransactionVo.TransactionType = dr["WMTT_TransactionClassificationName"].ToString();
                        mfTransactionVo.TransactionTrigger = dr["WMTT_Trigger"].ToString();
                        mfTransactionVo.FinancialFlag = int.Parse(dr["WMTT_FinancialFlag"].ToString());
                        mfTransactionVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfTransactionVo.PortfolioName = dr["CP_PortfolioName"].ToString();
                        mfTransactionVo.DivReinvestmen = dr["CMFOD_DividendOption"].ToString();
                        mfTransactionVo.Divfrequency = dr["DivFrequency"].ToString();
                        if (dr["Co_OrderId"].ToString() != null && dr["Co_OrderId"].ToString() != string.Empty)
                        {
                            mfTransactionVo.orderNo = int.Parse(dr["Co_OrderId"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.orderNo = 0;
                        }

                        mfTransactionVo.channel = dr["Channel"].ToString();
                        mfTransactionVo.latestNav = float.Parse(dr["NAV"].ToString());
                        // mfTransactionVo.TrxnNo = (dr["CMFT_TransactionNumber"].ToString());
                        // if (mfTransactionVo.OrdDate != DateTime.MinValue) 
                        //if (dr["CO_OrderDate"].ToString() != null && dr["CO_OrderDate"].ToString() != string.Empty)
                        //{
                        //    mfTransactionVo.OrdDate = DateTime.Parse(dr["CO_OrderDate"].ToString());
                        //}
                        //else
                        //{
                        //    mfTransactionVo.OrdDate = DateTime.MinValue;
                        //}
                        if (dr["CMFT_ELSSMaturityDate"].ToString() != null && dr["CMFT_ELSSMaturityDate"].ToString() != string.Empty)
                        {
                            mfTransactionVo.ELSSMaturityDate = DateTime.Parse(dr["CMFT_ELSSMaturityDate"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.ELSSMaturityDate = DateTime.MinValue;
                        }
                        mfTransactionVo.CreatedOn = DateTime.Parse(dr["CMFT_CreatedOn"].ToString());
                       
                        if (dr["CMFT_EUIN"].ToString() != null && dr["CMFT_EUIN"].ToString() != string.Empty)
                        {
                            mfTransactionVo.EUIN = dr["CMFT_EUIN"].ToString();
                        }
                        else
                        {
                            mfTransactionVo.EUIN = "N/A";
                        }
                        if (dr["CMFT_Area"].ToString() != null && dr["CMFT_Area"].ToString() != string.Empty)
                        {
                            mfTransactionVo.Area = dr["CMFT_Area"].ToString();
                        }
                        else
                        {
                            mfTransactionVo.Area = "N/A";
                        }
                        if (dr["WTS_TransactionStatusCode"].ToString() != null && dr["WTS_TransactionStatusCode"].ToString() != string.Empty)
                        {
                            mfTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                            mfTransactionVo.TransactionStatusCode = int.Parse(dr["WTS_TransactionStatusCode"].ToString());
                        }
                        else
                        {
                            mfTransactionVo.TransactionStatus = "OK";
                            mfTransactionVo.TransactionStatusCode = 1;

                        }
                        if (!string.IsNullOrEmpty(dr["CMFT_ExternalBrokerageAmount"].ToString()))
                            mfTransactionVo.BrokerageAmount = float.Parse(dr["CMFT_ExternalBrokerageAmount"].ToString());

                        mfTransactionsBookList.Add(mfTransactionVo);
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
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetRMCustomerMFTransactions()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }            
            return mfTransactionsBookList;
        }
        // add dao

        public float GetEQScripPrice(int ScripCode, DateTime navDate, String Currency)
        {
            float ScripPlanNAV = 0;
            Database db;
            DbCommand getEQScripPriceCmd;
            DataSet dsEQScripPrice;
            DataTable dtdsEQScripPrice;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEQScripPriceCmd = db.GetStoredProcCommand("SP_GetEquityScripPrice");

                db.AddInParameter(getEQScripPriceCmd, "@PEP_Date", DbType.DateTime, navDate);
                db.AddInParameter(getEQScripPriceCmd, "@PEM_ScripCode", DbType.Int32, ScripCode);
                if (Currency != null)
                {
                    db.AddInParameter(getEQScripPriceCmd, "@Currency", DbType.String, Currency);
                }
                else
                    db.AddInParameter(getEQScripPriceCmd, "@Currency", DbType.String, DBNull.Value);
                dsEQScripPrice = db.ExecuteDataSet(getEQScripPriceCmd);
                if (dsEQScripPrice.Tables[0].Rows.Count > 0)
                {
                    dtdsEQScripPrice = dsEQScripPrice.Tables[0];

                    foreach (DataRow dr in dtdsEQScripPrice.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["PESPH_ClosePrice"].ToString().Trim()))
                            ScripPlanNAV = float.Parse(dr["PESPH_ClosePrice"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetEQScripPrice(int scripCode, DateTime priceDate)");


                object[] objects = new object[2];
                objects[0] = ScripPlanNAV;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ScripPlanNAV;
        }


        public CustomerAccountsVo GetEquityRateForTransaction(int Accountid, int TransactionMode, string TransactionType, DateTime TradeDate)
        {
            Database db;
            DbCommand GetEquityRate;
            CustomerAccountsVo AccountVo = new CustomerAccountsVo();
            DataSet dsGetEquityRatesForTransaction;
            DataRow dr;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetEquityRate = db.GetStoredProcCommand("SPROC_GetRateForEquityTransaction");
                db.AddInParameter(GetEquityRate, "@Accountid", DbType.Int16, Accountid);
                db.AddInParameter(GetEquityRate, "@TransactiionMode", DbType.Int16, TransactionMode);
                db.AddInParameter(GetEquityRate, "@TransactionType", DbType.String, TransactionType);
                db.AddInParameter(GetEquityRate, "@TradeDate", DbType.DateTime, TradeDate);
                dsGetEquityRatesForTransaction = db.ExecuteDataSet(GetEquityRate);

                if (dsGetEquityRatesForTransaction.Tables[0].Rows.Count > 0)
                {
                    dr = dsGetEquityRatesForTransaction.Tables[0].Rows[0];
                    AccountVo = new CustomerAccountsVo();
                    if (dsGetEquityRatesForTransaction.Tables[0].Rows.Count > 0)
                    {
                        dr = dsGetEquityRatesForTransaction.Tables[0].Rows[0];
                        AccountVo = new CustomerAccountsVo();

                        AccountVo.AccountId = Convert.ToInt16(dr["CETA_AccountId"]);
                        //AccountVo.TradeNum = dr["CETA_TradeAccountNum"].ToString();
                        if (dr["CEB_TransactionMode"].ToString() != "")
                        {
                            AccountVo.TransactionMode = Convert.ToInt16(dr["CEB_TransactionMode"]);
                        }
                        if (dr["CEB_Type"].ToString() != "")
                        {
                            AccountVo.Type = dr["CEB_Type"].ToString();
                        }

                        if (dr["CEB_Rate"].ToString() != "")
                        {
                            AccountVo.Rate = double.Parse(dr["CEB_Rate"].ToString());
                        }
                        else
                        {
                            AccountVo.Rate = 0.0;
                        }

                        if (dr["CEB_SebiTurnOverFee"].ToString() != "")
                        {
                            AccountVo.SebiTurnOverFee = double.Parse(dr["CEB_SebiTurnOverFee"].ToString());
                        }
                        else
                        {
                            AccountVo.SebiTurnOverFee = 0.0;
                        }

                        if (dr["CEB_TransactionCharges"].ToString() != "")
                        {
                            AccountVo.TransactionCharges = double.Parse(dr["CEB_TransactionCharges"].ToString());
                        }
                        else
                        {
                            AccountVo.TransactionCharges = 0.0;
                        }
                        if (dr["CEB_StampCharges"].ToString() != "")
                        {
                            AccountVo.StampCharges = double.Parse(dr["CEB_StampCharges"].ToString());
                        }
                        else
                        {
                            AccountVo.StampCharges = 0.0;
                        }
                        if (dr["CEB_STT"].ToString() != string.Empty)
                        {
                            AccountVo.Stt = double.Parse(dr["CEB_STT"].ToString());
                        }
                        else
                        {
                            AccountVo.Stt = 0.0;
                        }
                        if (dr["CEB_ServiceTax"].ToString() != string.Empty)
                        {
                            AccountVo.ServiceTax = double.Parse(dr["CEB_ServiceTax"].ToString());
                        }
                        else
                        {
                            AccountVo.ServiceTax = 0.0;
                        }
                        if (dr["CEB_IsSebiApplicableToStax"].ToString() != string.Empty)
                        {
                            AccountVo.IsSebiApplicableToStax = Convert.ToInt16(dr["CEB_IsSebiApplicableToStax"]);
                        }
                        if (dr["CEB_IsTrxnApplicableToStax"].ToString() != string.Empty)
                        {
                            AccountVo.IsTrxnApplicableToStax = Convert.ToInt16(dr["CEB_IsTrxnApplicableToStax"]);
                        }
                        if (dr["CEB_IsStampApplicableToStax"].ToString() != string.Empty)
                        {
                            AccountVo.IsStampApplicableToStax = Convert.ToInt16(dr["CEB_IsStampApplicableToStax"]);
                        }
                        if (dr["CEB_IsBrApplicableToStax"].ToString() != string.Empty)
                        {
                            AccountVo.IsBrApplicableToStax = Convert.ToInt16(dr["CEB_IsBrApplicableToStax"]);
                        }

                        if (dr["CEB_StartDate"].ToString() != string.Empty)
                            AccountVo.StartDate = DateTime.Parse(dr["CEB_StartDate"].ToString());
                        else
                            AccountVo.StartDate = DateTime.MinValue;
                        if (dr["CEB_EndDate"].ToString() != string.Empty)
                            AccountVo.EndDate = DateTime.Parse(dr["CEB_EndDate"].ToString());
                        else
                            AccountVo.EndDate = DateTime.MinValue;



                    }
                }


            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetCustomerCapitalLedgerMIS()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AccountVo;

        }
        public void AddDividend(string CorpAxnCode, int scripcode, DateTime DivDeclaredDate, double DivPercentage, double facevalue)
        {

            Database db;
            DbCommand AddDividend;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AddDividend = db.GetStoredProcCommand("SPROC_CreateDividend");
                db.AddInParameter(AddDividend, "@PECAM_CorpAxnCode", DbType.String, CorpAxnCode);
                db.AddInParameter(AddDividend, "@PEM_ScripCode1", DbType.Int32, scripcode);
                db.AddInParameter(AddDividend, "@PECA_RecordDate", DbType.DateTime, DivDeclaredDate);
                db.AddInParameter(AddDividend, "@PECA_Ratio1", DbType.Double, DivPercentage);
                db.AddInParameter(AddDividend, "@PECA_FaceValueExisting", DbType.Double, facevalue);
                //db.AddInParameter(AddDividend, "@PECA_CreatedOn", DbType.,);
                //db.AddInParameter(AddDividend, "@PECA_ModifiedOn", DbType.Double,);
                db.ExecuteScalar(AddDividend);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:AddDividend()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        public int GetNoOfShares(int AccountId, int scripCode, DateTime TradeDate, int DematAccountNum, int managedby)
        {
            int NoOfShares;
            Database db;
            DbCommand GetNoOfShares;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNoOfShares = db.GetStoredProcCommand("SPROC_GetNoOfSharesForEquity");
                db.AddInParameter(GetNoOfShares, "@AccountId", DbType.Int32, AccountId);
                db.AddInParameter(GetNoOfShares, "@ScripCode", DbType.Int32, scripCode);
                db.AddInParameter(GetNoOfShares, "@TradeDate", DbType.DateTime, TradeDate);
                db.AddInParameter(GetNoOfShares, "@DematAccountNum", DbType.Int32, DematAccountNum);
                db.AddInParameter(GetNoOfShares, "@managedby", DbType.Int32, managedby);
                NoOfShares = Convert.ToInt32(db.ExecuteScalar(GetNoOfShares));
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetNoOfShares()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return NoOfShares;
        }
        public DataSet GetDividendHistory(int scripCode)
        {
            DataSet ds = null;
            Database db;
            DbCommand DividendHistory;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                DividendHistory = db.GetStoredProcCommand("SPROC_GetPastDividendHistory");
                db.AddInParameter(DividendHistory, "@ScripCode", DbType.Int32, scripCode);
                ds = db.ExecuteDataSet(DividendHistory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetDividendHistory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }


        public DataSet GetManagedby(int advisorid)
        {
            DataSet ds = null;
            Database db;
            DbCommand getmanagedbyCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getmanagedbyCmd = db.GetStoredProcCommand("SPROC_GetAllManagedBy");
                db.AddInParameter(getmanagedbyCmd, "AdvisorId", DbType.Int16, advisorid);
                ds = db.ExecuteDataSet(getmanagedbyCmd);
           }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetManagedby()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public DataSet GetDematAccountNumber(int portfolioid)
        {
            DataSet ds = null;
            Database db;
            DbCommand getDematAcctCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getDematAcctCmd = db.GetStoredProcCommand("SPROC_GetDematAccountNumber");
                db.AddInParameter(getDematAcctCmd, "@portfolioid", DbType.Int32, portfolioid);
                ds = db.ExecuteDataSet(getDematAcctCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetDematAccountNumber()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }

        public string GetDollarRate(DateTime TradeDate)
        {
            Database db;
            DbCommand CmdgetdollarRate;
            String Rate = string.Empty;
            DataSet ds;
            DataTable dtdollarprice;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdgetdollarRate = db.GetStoredProcCommand("SPROC_GetDollarRate");
                db.AddInParameter(CmdgetdollarRate, "@TradeDate", DbType.DateTime, TradeDate);
                ds = db.ExecuteDataSet(CmdgetdollarRate);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtdollarprice = ds.Tables[0];
                    foreach (DataRow dr in dtdollarprice.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["Rate"].ToString().Trim()))
                            Rate = dr["Rate"].ToString();
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:BulkEqTransactionDeletion()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return Rate;
        }
        public DataSet GetType()
        {
            DataSet ds = null;
            Database db;
            DbCommand gettypeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                gettypeCmd = db.GetStoredProcCommand("SPROC_GetEquityType");

                ds = db.ExecuteDataSet(gettypeCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetType()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public List<EQTransactionVo> GetEquityTransactions(int customerId,
                                                                  int portfolioId, int ScripCode,
                                                         DateTime FromDate, DateTime ToDate, string price
                                                                  )
        {
            List<EQTransactionVo> eqTransactionsList = null;
            EQTransactionVo eqTransactionVo = new EQTransactionVo();
            Database db;
            DbCommand getEquityTransactionsCmd;
            DataSet dsGetEquityTransactions;
            DataTable dtGetEquityTransactions;

            //genDictTranType = new Dictionary<string, string>();
            //genDictExchange = new Dictionary<string, string>();
            //genDictTradeDate = new Dictionary<string, string>();

            //Count = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionsCmd = db.GetStoredProcCommand("SP_GetCustomerEquityTransactions");
                db.AddInParameter(getEquityTransactionsCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquityTransactionsCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getEquityTransactionsCmd, "@ScripCode", DbType.Int32, ScripCode);
                db.AddInParameter(getEquityTransactionsCmd, "@fromDate", DbType.DateTime, FromDate);
                db.AddInParameter(getEquityTransactionsCmd, "@toDate", DbType.DateTime, ToDate);
                db.AddInParameter(getEquityTransactionsCmd, "@Currency", DbType.String, price);
                dsGetEquityTransactions = db.ExecuteDataSet(getEquityTransactionsCmd);
                if (dsGetEquityTransactions.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransactions = dsGetEquityTransactions.Tables[0];
                    eqTransactionsList = new List<EQTransactionVo>();
                    foreach (DataRow dr in dtGetEquityTransactions.Rows)
                    {
                        eqTransactionVo = new EQTransactionVo();
                        eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                        eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        eqTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());

                        if (!string.IsNullOrEmpty(dr["PEM_ScripCode"].ToString()))
                            eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        else

                            eqTransactionVo.ScripCode = 0;

                        eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                        eqTransactionVo.Ticker = dr["PEM_Ticker"].ToString();
                        eqTransactionVo.TradeAccountNum = (dr["CETA_TradeAccountNum"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_OrderNum"].ToString()))
                            eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                        eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                        if (dr["CET_IsSpeculative"].ToString() == "1")
                            eqTransactionVo.TradeType = "S";
                        else
                            eqTransactionVo.TradeType = "D";

                        if (!string.IsNullOrEmpty(dr["XE_ExchangeCode"].ToString()))
                            eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                        else
                            eqTransactionVo.Exchange = "";
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_Rate"].ToString()))
                            eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_Quantity"].ToString()))
                            eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_Brokerage"].ToString()))
                            eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_ServiceTax"].ToString()))
                            eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_EducationCess"].ToString()))
                            eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_STT"].ToString()))
                            eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_OtherCharges"].ToString()))
                            eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_RateInclBrokerage"].ToString()))
                            eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_TradeTotal"].ToString()))
                            eqTransactionVo.TradeTotal = double.Parse(dr["CET_TradeTotal"].ToString());
                        eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_IsSourceManual"].ToString()))
                            eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());

                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                        eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                        eqTransactionVo.IsCorpAction = int.Parse(dr["WETT_IsCorpAxn"].ToString());
                        eqTransactionVo.TransactionStatus = dr["WTS_TransactionStatus"].ToString();
                        eqTransactionVo.ManagerName = dr["ManagedBy"].ToString();
                        eqTransactionVo.BrokerName = dr["XB_BrokerName"].ToString();
                        if (!string.IsNullOrEmpty(dr["CEDA_DPClientId"].ToString()))
                            eqTransactionVo.DpclientId = dr["CEDA_DPClientId"].ToString();
                        else
                            eqTransactionVo.DpclientId = "";
                        eqTransactionVo.Purpose = dr["IsTradeType"].ToString();
                        eqTransactionVo.InvestorName = dr["InvesterName"].ToString();
                        eqTransactionVo.PanNo = dr["C_PANNum"].ToString();
                        eqTransactionVo.Scripcode = dr["PesmIdentifier"].ToString();
                        eqTransactionVo.SettlementNo = dr["CET_SettlementNo"].ToString();
                        eqTransactionVo.BillNo = dr["CET_BillNo"].ToString();

                        if (!string.IsNullOrEmpty(dr["CET_SebiTurnOverFee"].ToString()))
                            eqTransactionVo.SebiTurnOverFee = double.Parse(dr["CET_SebiTurnOverFee"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_TrxnCharges"].ToString()))
                            eqTransactionVo.TransactionCharges = double.Parse(dr["CET_TrxnCharges"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_StampCharges"].ToString()))
                            eqTransactionVo.StampCharges = double.Parse(dr["CET_StampCharges"].ToString());
                        if (!string.IsNullOrEmpty(dr["cet_stt"].ToString()))
                            eqTransactionVo.STT = float.Parse(dr["cet_stt"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_ServiceTax"].ToString()))
                            eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_DifferenceInBrokerage"].ToString()))
                            eqTransactionVo.DifferenceInBrokerage = double.Parse(dr["CET_DifferenceInBrokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_RateInclBrokerage"].ToString()))
                            eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_GrossConsideration"].ToString()))
                            eqTransactionVo.GrossConsideration = double.Parse(dr["CET_GrossConsideration"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_RateIncBrokerageAllCharges"].ToString()))
                            eqTransactionVo.RateIncBrokerageAllCharges = double.Parse(dr["CET_RateIncBrokerageAllCharges"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_TrTotalIncBrokerage"].ToString()))
                            eqTransactionVo.TradeTotalIncBrokerage = double.Parse(dr["CET_TrTotalIncBrokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_DividendRecieved"].ToString()))
                            eqTransactionVo.DividendRecieved = bool.Parse(dr["CET_DividendRecieved"].ToString());
                        else
                            eqTransactionVo.DividendRecieved = null;
                        if (!string.IsNullOrEmpty(dr["CET_DematCharge"].ToString()))
                            eqTransactionVo.DematCharge = float.Parse(dr["CET_DematCharge"].ToString());


                        eqTransactionVo.ForExRate = float.Parse(dr["ForExRate"].ToString());
                        if (!string.IsNullOrEmpty(dr["ForExDate"].ToString()))
                            eqTransactionVo.ForExRateDate = DateTime.Parse(dr["ForExDate"].ToString());
                        else
                            eqTransactionVo.ForExRateDate = DateTime.MinValue;
                        if (!string.IsNullOrEmpty(dr["CET_FXCurencyType"].ToString()))
                            eqTransactionVo.FXCurencyType = dr["CET_FXCurencyType"].ToString();
                        else
                            eqTransactionVo.FXCurencyType = null;

                        if (!string.IsNullOrEmpty(dr["CET_FXCurencyRate"].ToString()))
                            eqTransactionVo.FXCurencyRate = Convert.ToDouble(dr["CET_FXCurencyRate"].ToString());
                        else
                            eqTransactionVo.FXCurencyRate = 0.0;

                        if (!string.IsNullOrEmpty(dr["MktClosingForexRate"].ToString()))
                            eqTransactionVo.MktClosingForexRate = Convert.ToDouble(dr["MktClosingForexRate"].ToString());
                        else
                            eqTransactionVo.FXCurencyRate = 0.0;
                        eqTransactionVo.CreatedOn = dr["CET_CreatedOn"].ToString();

                        eqTransactionsList.Add(eqTransactionVo);
                    }
                }

                //if (dsGetEquityTransactions.Tables[2].Rows.Count > 0)
                //{
                //    string type = string.Empty;
                //    string mode = string.Empty;
                //    string tranType = string.Empty;

                //    foreach (DataRow dr in dsGetEquityTransactions.Tables[2].Rows)
                //    {
                //        if (dr["CET_IsSpeculative"].ToString() == "1")
                //            mode = "Speculation";
                //        else if (dr["CET_IsSpeculative"].ToString() == "0")
                //            mode = "Delivery";
                //        if (dr["WETT_TransactionCode"].ToString() == "1")
                //            type = "Buy";
                //        else if (dr["WETT_TransactionCode"].ToString() == "2")
                //            type = "Sell";
                //        else if (dr["WETT_TransactionCode"].ToString() == "13")
                //            type = "Holdings";
                //        tranType = type + "/" + mode;

                //        //if (!genDictTranType.ContainsKey(tranType))
                //        //{
                //        //    genDictTranType.Add(tranType, tranType);
                //        //}
                //    }
                //}

                //if (dsGetEquityTransactions.Tables[3].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsGetEquityTransactions.Tables[3].Rows)
                //    {
                //        genDictExchange.Add(dr["XE_ExchangeCode"].ToString(), dr["XE_ExchangeCode"].ToString());
                //    }
                //}

                //if (dsGetEquityTransactions.Tables[4].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsGetEquityTransactions.Tables[4].Rows)
                //    {
                //        genDictTradeDate.Add(DateTime.Parse(dr["CET_TradeDate"].ToString()).ToShortDateString(), DateTime.Parse(dr["CET_TradeDate"].ToString()).ToShortDateString());
                //    }
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityTransactions()");

                object[] objects = new object[1];
                objects[0] = customerId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            // if (dsGetEquityTransactions.Tables[1].Rows.Count > 0)
            // Count = Int32.Parse(dsGetEquityTransactions.Tables[1].Rows[0]["CNT"].ToString());

            return eqTransactionsList;
        }
        public bool MapEQTransactionToCIAndPI(string EQTraxnIds, string Type)
        {
            bool bResult = false;
            Database db;
            DbCommand MapEQTransactionToCIAndPICmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                MapEQTransactionToCIAndPICmd = db.GetStoredProcCommand("SPROC_MapEQTransactionToCIAndPI");
                db.AddInParameter(MapEQTransactionToCIAndPICmd, "@EQTraxnIds", DbType.String, EQTraxnIds);
                db.AddInParameter(MapEQTransactionToCIAndPICmd, "@Type", DbType.String, Type);
                if (db.ExecuteNonQuery(MapEQTransactionToCIAndPICmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:MapEQTransactionToCI()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool MapEQToManager(string EQTraxnIds)
        {
            bool bResult = false;
            Database db;
            DbCommand MapToManagerCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                MapToManagerCmd = db.GetStoredProcCommand("SPROC_MapEQToManager");
                db.AddInParameter(MapToManagerCmd, "@EQTraxnIds", DbType.String, EQTraxnIds);
                if (db.ExecuteNonQuery(MapToManagerCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:MapEQToManager()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool BulkEqTransactionDeletion(string EQTraxnIds)
        {
            bool bResult = false;
            Database db;
            DbCommand MapToManagerCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                MapToManagerCmd = db.GetStoredProcCommand("SPROC_DeleteEqTransactions");
                db.AddInParameter(MapToManagerCmd, "@EQTraxnIds", DbType.String, EQTraxnIds);
                if (db.ExecuteNonQuery(MapToManagerCmd) != 0)
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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:BulkEqTransactionDeletion()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool DeleteEquityTransaction(int eqTransId)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteEquityTransactionCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteEquityTransactionCmd = db.GetStoredProcCommand("SPROC_DeleteEquityTransaction");

                db.AddInParameter(deleteEquityTransactionCmd, "@CET_EqTransId", DbType.Int32, eqTransId);
                if (db.ExecuteNonQuery(deleteEquityTransactionCmd) != 0)

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:DeleteEQTransaction()");

                object[] objects = new object[1];
                objects[0] = eqTransId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }
        //public EQTransactionVo GetEquityTransaction(int eqTransactionId, String Currency)
        //{
        //    CustomerTransactionDao customerTransactionDao = new CustomerTransactionDao();

        //    EQTransactionVo eqTransactionVo = new EQTransactionVo();

        //    try
        //    {
        //        eqTransactionVo = customerTransactionDao.GetEquityTransaction(eqTransactionId, Currency);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "CustomerTransactionBo.cs:GetEquityTransaction()");


        //        object[] objects = new object[1];
        //        objects[0] = eqTransactionId;

        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return eqTransactionVo;
        //}
        public EQTransactionVo GetEquityTransaction(int eqTransactionId, String Currency)
        {
            EQTransactionVo eqTransactionVo = null;

            Database db;
            DbCommand getEquityTransactionCmd;
            DataSet dsGetEquityTransaction;
            DataTable dtGetEquityTransaction;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityTransactionCmd = db.GetStoredProcCommand("SP_GetCustomerEquityTransaction");
                db.AddInParameter(getEquityTransactionCmd, "@CET_EqTransId", DbType.Int32, eqTransactionId);
                db.AddInParameter(getEquityTransactionCmd, "@Currency", DbType.String, Currency);
                dsGetEquityTransaction = db.ExecuteDataSet(getEquityTransactionCmd);
                if (dsGetEquityTransaction.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityTransaction = dsGetEquityTransaction.Tables[0];

                    eqTransactionVo = new EQTransactionVo();

                    foreach (DataRow dr in dtGetEquityTransaction.Rows)
                    {
                        eqTransactionVo.TransactionId = int.Parse(dr["CET_EqTransId"].ToString());
                        //eqTransactionVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        //eqTransactionVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        eqTransactionVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        if (!string.IsNullOrEmpty(dr["PEM_ScripCode"].ToString()))
                            eqTransactionVo.ScripCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        else
                            eqTransactionVo.ScripCode = 0;
                        //eqTransactionVo.ScripName = dr["PEM_CompanyName"].ToString();
                        //eqTransactionVo.Ticker = dr["PEM_Ticker"].ToString();
                        eqTransactionVo.TradeAccountNum = (dr["CET_TradeNum"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_OrderNum"].ToString()))
                            eqTransactionVo.OrderNum = Int64.Parse(dr["CET_OrderNum"].ToString());
                        eqTransactionVo.BuySell = dr["CET_BuySell"].ToString();
                        if (!string.IsNullOrEmpty(dr["CEDA_DematAccountId"].ToString()))
                            eqTransactionVo.DematAccountNo = int.Parse(dr["CEDA_DematAccountId"].ToString());
                        eqTransactionVo.IsSpeculative = int.Parse(dr["CET_IsSpeculative"].ToString());
                        if (dr["CET_IsSpeculative"].ToString() == "1")
                            eqTransactionVo.TradeType = "S";
                        else
                            eqTransactionVo.TradeType = "D";

                        eqTransactionVo.Exchange = dr["XE_ExchangeCode"].ToString();
                        eqTransactionVo.TradeDate = DateTime.Parse(dr["CET_TradeDate"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_Rate"].ToString()))
                            eqTransactionVo.Rate = float.Parse(dr["CET_Rate"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_Quantity"].ToString()))
                            eqTransactionVo.Quantity = float.Parse(dr["CET_Quantity"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_Brokerage"].ToString()))
                            eqTransactionVo.Brokerage = float.Parse(dr["CET_Brokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_ServiceTax"].ToString()))
                            eqTransactionVo.ServiceTax = float.Parse(dr["CET_ServiceTax"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_EducationCess"].ToString()))
                            eqTransactionVo.EducationCess = float.Parse(dr["CET_EducationCess"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_STT"].ToString()))
                            eqTransactionVo.STT = float.Parse(dr["CET_STT"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_OtherCharges"].ToString()))
                            eqTransactionVo.OtherCharges = float.Parse(dr["CET_OtherCharges"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_RateInclBrokerage"].ToString()))
                            eqTransactionVo.RateInclBrokerage = float.Parse(dr["CET_RateInclBrokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_TradeTotal"].ToString()))
                            eqTransactionVo.TradeTotal = Convert.ToDouble((dr["CET_TradeTotal"].ToString()));
                        if (!string.IsNullOrEmpty(dr["XB_BrokerCode"].ToString()))
                            eqTransactionVo.BrokerCode = dr["XB_BrokerCode"].ToString();
                        else
                            eqTransactionVo.BrokerCode = "";
                        eqTransactionVo.IsSplit = int.Parse(dr["CET_IsSplit"].ToString());
                        eqTransactionVo.SplitTransactionId = int.Parse(dr["CET_SplitCustEqTransId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_IsSourceManual"].ToString()))
                            eqTransactionVo.IsSourceManual = int.Parse(dr["CET_IsSourceManual"].ToString());
                        eqTransactionVo.SourceCode = dr["XES_SourceCode"].ToString();
                        eqTransactionVo.TransactionCode = int.Parse(dr["WETT_TransactionCode"].ToString());
                        eqTransactionVo.ManagerName = dr["C_FirstName"].ToString();
                        if (!string.IsNullOrEmpty(dr["CET_DifferenceInBrokerage"].ToString()))
                            eqTransactionVo.DifferenceInBrokerage = double.Parse(dr["CET_DifferenceInBrokerage"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_SebiTurnOverFee"].ToString()))
                            eqTransactionVo.SebiTurnOverFee = double.Parse(dr["CET_SebiTurnOverFee"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_TrxnCharges"].ToString()))
                            eqTransactionVo.TransactionCharges = double.Parse(dr["CET_TrxnCharges"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_StampCharges"].ToString()))
                            eqTransactionVo.StampCharges = double.Parse(dr["CET_StampCharges"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_NoOfSharesEligibleForDiv"].ToString()))
                            eqTransactionVo.NoOfSharesForDiv = int.Parse(dr["CET_NoOfSharesEligibleForDiv"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_DividendRecieved"].ToString()))
                            eqTransactionVo.DividendRecieved = bool.Parse(dr["CET_DividendRecieved"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_BankReferenceNo"].ToString()))
                            eqTransactionVo.BankReferenceNo = dr["CET_BankReferenceNo"].ToString();
                        if (!string.IsNullOrEmpty(dr["PECA_DailyCorpAxnId"].ToString()))
                            eqTransactionVo.DailyCorpAxnId = int.Parse(dr["PECA_DailyCorpAxnId"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_BillNo"].ToString()))
                            eqTransactionVo.BillNo = dr["CET_BillNo"].ToString();
                        if (!string.IsNullOrEmpty(dr["CET_SettlementNo"].ToString()))
                            eqTransactionVo.SettlementNo = dr["CET_SettlementNo"].ToString();

                        if (!string.IsNullOrEmpty(dr["CET_SettlementDate"].ToString()))
                            eqTransactionVo.SettlementDate = Convert.ToDateTime(dr["CET_SettlementDate"].ToString());
                        else
                            eqTransactionVo.SettlementDate = DateTime.MinValue;
                        if (!string.IsNullOrEmpty(dr["CET_Managedby"].ToString()))
                            eqTransactionVo.ManagedBy = int.Parse(dr["CET_Managedby"].ToString());

                        if (!string.IsNullOrEmpty(dr["CET_IsTradeType"].ToString()))
                            eqTransactionVo.Type = int.Parse(dr["CET_IsTradeType"].ToString());
                        if (!string.IsNullOrEmpty(dr["CET_RateIncBrokerageAllCharges"].ToString()))

                            eqTransactionVo.RateIncBrokerageAllCharges = double.Parse(dr["CET_RateIncBrokerageAllCharges"].ToString());
                        eqTransactionVo.Currency = Currency.ToString();

                        if (!string.IsNullOrEmpty(dr["CET_FXCurencyType"].ToString()))
                            eqTransactionVo.FXCurencyType = dr["CET_FXCurencyType"].ToString();
                        else
                            eqTransactionVo.FXCurencyType = null;

                        if (!string.IsNullOrEmpty(dr["CET_FXCurencyRate"].ToString()))
                            eqTransactionVo.FXCurencyRate = Convert.ToDouble(dr["CET_FXCurencyRate"].ToString());
                        else
                            eqTransactionVo.FXCurencyRate = 0.0;

                        if (!string.IsNullOrEmpty(dr["CET_DematCharge"].ToString()))
                            eqTransactionVo.DematCharge = float.Parse(dr["CET_DematCharge"].ToString());



                        if (!string.IsNullOrEmpty(dr["CET_TrTotalIncBrokerage"].ToString()))
                            eqTransactionVo.TradeTotalIncBrokerage = double.Parse(dr["CET_TrTotalIncBrokerage"].ToString());

                        if (!string.IsNullOrEmpty(dr["CET_GrossConsideration"].ToString()))
                            eqTransactionVo.GrossConsideration = double.Parse(dr["CET_GrossConsideration"].ToString());


                        if (!string.IsNullOrEmpty(dr["CET_Remark"].ToString()))
                            eqTransactionVo.Remark = dr["CET_Remark"].ToString();

                        //eqTransactionVo.TransactionType = dr["WETT_TransactionTypeName"].ToString();
                        //eqTransactionVo.IsCorpAction = int.Parse(dr["WETT_IsCorpAxn"].ToString());

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

                FunctionInfo.Add("Method", "CustomerTransactionDao.cs:GetEquityTransaction()");


                object[] objects = new object[1];
                objects[0] = eqTransactionId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }


            return eqTransactionVo;
        }


    }
}
