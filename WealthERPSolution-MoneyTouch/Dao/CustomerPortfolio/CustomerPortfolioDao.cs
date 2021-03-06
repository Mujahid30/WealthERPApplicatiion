﻿using System;
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

namespace DaoCustomerPortfolio
{
    public class CustomerPortfolioDao
    {
        #region Equity Portfolio
        public List<EQPortfolioVo> GetCustomerEquityPortfolio(int customerId, DateTime tradeDate)
        {
            List<EQPortfolioVo> eqPortfolioVoList = null;

            EQPortfolioVo eqPortfolioVo = new EQPortfolioVo();
            Database db;
            DbCommand getEquityPortfolioCmd;
            DataSet dsGetEquityPortfolio;
            DataTable dtGetEquityPortfolio;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerEquityTransactionScripCodes");
                db.AddInParameter(getEquityPortfolioCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquityPortfolioCmd, "@CET_tradeDate", DbType.DateTime, tradeDate.ToString());

                dsGetEquityPortfolio = db.ExecuteDataSet(getEquityPortfolioCmd);
                if (dsGetEquityPortfolio.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityPortfolio = dsGetEquityPortfolio.Tables[0];
                    eqPortfolioVoList = new List<EQPortfolioVo>();
                    foreach (DataRow dr in dtGetEquityPortfolio.Rows)
                    {
                        eqPortfolioVo = new EQPortfolioVo();
                        eqPortfolioVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        eqPortfolioVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        eqPortfolioVo.EQCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        eqPortfolioVo.EQCompanyName = dr["PEM_CompanyName"].ToString();
                        eqPortfolioVo.EQTicker = dr["PEM_Ticker"].ToString();

                        eqPortfolioVoList.Add(eqPortfolioVo);
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetCustomerPortfolio(int customerId)");

                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = tradeDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqPortfolioVoList;
        }
        public List<EQPortfolioVo> GetCustomerEquityPortfolio(int customerId, int portfolioId, DateTime tradeDate, string ScripNameFilter)
        {
            List<EQPortfolioVo> eqPortfolioVoList = null;
            EQPortfolioVo eqPortfolioVo = new EQPortfolioVo();
            Database db;
            DbCommand getEquityPortfolioCmd;
            DataSet dsGetEquityPortfolio;
            DataTable dtGetEquityPortfolio;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioEquityTransactionScripCodes");
                db.AddInParameter(getEquityPortfolioCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquityPortfolioCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getEquityPortfolioCmd, "@CET_tradeDate", DbType.DateTime, tradeDate.ToString());
                if (ScripNameFilter != "")
                    db.AddInParameter(getEquityPortfolioCmd, "@scripNameFilter", DbType.String, ScripNameFilter);
                else
                    db.AddInParameter(getEquityPortfolioCmd, "@scripNameFilter", DbType.String, DBNull.Value);

                dsGetEquityPortfolio = db.ExecuteDataSet(getEquityPortfolioCmd);
                if (dsGetEquityPortfolio.Tables[0].Rows.Count > 0)
                {
                    dtGetEquityPortfolio = dsGetEquityPortfolio.Tables[0];
                    eqPortfolioVoList = new List<EQPortfolioVo>();
                    foreach (DataRow dr in dtGetEquityPortfolio.Rows)
                    {
                        eqPortfolioVo = new EQPortfolioVo();
                        eqPortfolioVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        eqPortfolioVo.AccountId = int.Parse(dr["CETA_AccountId"].ToString());
                        eqPortfolioVo.EQCode = int.Parse(dr["PEM_ScripCode"].ToString());
                        eqPortfolioVo.EQCompanyName = dr["PEM_CompanyName"].ToString();
                        eqPortfolioVo.EQTicker = dr["PEM_Ticker"].ToString();
                        eqPortfolioVo.ValuationDate = tradeDate;

                        eqPortfolioVoList.Add(eqPortfolioVo);
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetCustomerPortfolio(int customerId)");


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
        public float GetEquityScripClosePrice(int scripCode, DateTime priceDate)
        {
            float scripClosePrice = 0;
            Database db;
            DbCommand getEquityScripPriceCmd;
            DataSet dsEquityScripPrice;
            DataTable dtEquityScripPrice;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityScripPriceCmd = db.GetStoredProcCommand("SP_GetEquityScripPrice");

                db.AddInParameter(getEquityScripPriceCmd, "@PEP_Date", DbType.DateTime, priceDate);
                db.AddInParameter(getEquityScripPriceCmd, "@PEM_ScripCode", DbType.Int32, scripCode);

                dsEquityScripPrice = db.ExecuteDataSet(getEquityScripPriceCmd);
                if (dsEquityScripPrice.Tables[0].Rows.Count > 0)
                {
                    dtEquityScripPrice = dsEquityScripPrice.Tables[0];
                    foreach (DataRow dr in dtEquityScripPrice.Rows)
                    {
                        if (dr["PESPH_ClosePrice"].ToString() != string.Empty)
                            scripClosePrice = float.Parse(dr["PESPH_ClosePrice"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetEquityScripClosePrice(int scripCode, DateTime priceDate)");


                object[] objects = new object[2];
                objects[0] = scripCode;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return scripClosePrice;
        }
        public float GetEquityScripSnapShotPrice(int scripCode, DateTime TradeDate)
        {
            float scripClosePrice = 0;
            Database db;
            DbCommand getEquityScripPriceCmd;
            DataSet dsEquityScripPrice;
            DataTable dtEquityScripPrice;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquityScripPriceCmd = db.GetStoredProcCommand("SP_GetEquityScripSnapShotPrice");


                db.AddInParameter(getEquityScripPriceCmd, "@PEM_ScripCode", DbType.Int32, scripCode);
                db.AddInParameter(getEquityScripPriceCmd, "@PEP_Date", DbType.DateTime, TradeDate);

                dsEquityScripPrice = db.ExecuteDataSet(getEquityScripPriceCmd);
                if (dsEquityScripPrice.Tables[0].Rows.Count > 0)
                {
                    dtEquityScripPrice = dsEquityScripPrice.Tables[0];
                    foreach (DataRow dr in dtEquityScripPrice.Rows)
                    {
                        if (dr["PESPS_ClosePrice"].ToString() != string.Empty)
                            scripClosePrice = float.Parse(dr["PESPS_ClosePrice"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetEquityScripClosePrice(int scripCode, DateTime priceDate)");


                object[] objects = new object[2];
                objects[0] = scripCode;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return scripClosePrice;
        }
        public float GetCustomerEquitySpeculativeAveragePrice(int customerId, int equityCode, DateTime tradeDate)
        {
            float speculativeAveragePrice = 0;
            Database db;
            DbCommand getEquitySpeculativeAvgPriceCmd;
            DataSet dsGetEquitySpeculativeAvgPrice;
            DataTable dtGetEquitySpeculativeAvgPrice;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquitySpeculativeAvgPriceCmd = db.GetStoredProcCommand("SP_GetCustomerEquitySpeculativeAveragePrice");
                db.AddInParameter(getEquitySpeculativeAvgPriceCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquitySpeculativeAvgPriceCmd, "@PEM_ScripCode", DbType.Int32, equityCode);
                db.AddInParameter(getEquitySpeculativeAvgPriceCmd, "@CET_TradeDate", DbType.DateTime, tradeDate);

                dsGetEquitySpeculativeAvgPrice = db.ExecuteDataSet(getEquitySpeculativeAvgPriceCmd);
                if (dsGetEquitySpeculativeAvgPrice.Tables[0].Rows.Count > 0)
                {
                    dtGetEquitySpeculativeAvgPrice = dsGetEquitySpeculativeAvgPrice.Tables[0];
                    foreach (DataRow dr in dtGetEquitySpeculativeAvgPrice.Rows)
                    {
                        speculativeAveragePrice = float.Parse(dr["AvgPrice"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetCustomerPortfolio(int customerId)");


                object[] objects = new object[2];
                objects[0] = customerId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return speculativeAveragePrice;
        }
        public float GetCustomerEquitySpeculativeAveragePrice(int customerId, int portfolioId, int equityCode, DateTime tradeDate)
        {
            float speculativeAveragePrice = 0;
            Database db;
            DbCommand getEquitySpeculativeAvgPriceCmd;
            DataSet dsGetEquitySpeculativeAvgPrice;
            DataTable dtGetEquitySpeculativeAvgPrice;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getEquitySpeculativeAvgPriceCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioEquitySpeculativeAveragePrice");
                db.AddInParameter(getEquitySpeculativeAvgPriceCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getEquitySpeculativeAvgPriceCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getEquitySpeculativeAvgPriceCmd, "@PEM_ScripCode", DbType.Int32, equityCode);
                db.AddInParameter(getEquitySpeculativeAvgPriceCmd, "@CET_TradeDate", DbType.DateTime, tradeDate);

                dsGetEquitySpeculativeAvgPrice = db.ExecuteDataSet(getEquitySpeculativeAvgPriceCmd);
                if (dsGetEquitySpeculativeAvgPrice.Tables[0].Rows.Count > 0)
                {
                    dtGetEquitySpeculativeAvgPrice = dsGetEquitySpeculativeAvgPrice.Tables[0];
                    foreach (DataRow dr in dtGetEquitySpeculativeAvgPrice.Rows)
                    {
                        speculativeAveragePrice = float.Parse(dr["AvgPrice"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetCustomerPortfolio(int customerId)");


                object[] objects = new object[2];
                objects[0] = customerId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return speculativeAveragePrice;
        }

        public int AddEquityNetPosition(EQPortfolioVo eqPortfolioVo, int userId)
        {
            int eqNPId = 0;
            Database db;
            DbCommand createEquityNetPositionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createEquityNetPositionCmd = db.GetStoredProcCommand("SP_AddEquityNetPosition");

                db.AddInParameter(createEquityNetPositionCmd, "@CETA_AccountId", DbType.Int32, eqPortfolioVo.AccountId);
                db.AddInParameter(createEquityNetPositionCmd, "@PEM_ScripCode", DbType.Int32, eqPortfolioVo.EQCode);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_NetHoldings", DbType.Decimal, eqPortfolioVo.Quantity);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_MarketPrice", DbType.Decimal, eqPortfolioVo.MarketPrice);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_CurrentValue", DbType.Double, Math.Round(eqPortfolioVo.CurrentValue));
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_ValuationDate", DbType.DateTime, eqPortfolioVo.ValuationDate);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_SaleQuantity", DbType.Decimal, eqPortfolioVo.SalesQuantity);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_AveragePrice", DbType.Decimal, eqPortfolioVo.AveragePrice);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_RealizedPNL", DbType.Decimal, eqPortfolioVo.RealizedPNL);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_CostOfSales", DbType.Decimal, eqPortfolioVo.CostOfSales);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_NetCost", DbType.Decimal, eqPortfolioVo.NetCost);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_SpeculativeSaleQuantity", DbType.Decimal, eqPortfolioVo.SpeculativeSalesQuantity);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_DeliverySaleQuantity", DbType.Decimal, eqPortfolioVo.DeliverySalesQuantity);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_RealizedPNLForSpeculative", DbType.Decimal, eqPortfolioVo.SpeculativeRealizedProfitLoss);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_RealizedPNLForDelivery", DbType.Decimal, eqPortfolioVo.DeliveryRealizedProfitLoss);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_CostOfSalesForSpeculative", DbType.Decimal, eqPortfolioVo.SpeculativeCostOfSales);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_CostofSalesforDelivery", DbType.Decimal, eqPortfolioVo.DeliveryCostOfSales);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_Deliverysaleproceeds", DbType.Decimal, eqPortfolioVo.DeliveryRealizedSalesProceeds);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_Speculativesalesproceeds", DbType.Decimal, eqPortfolioVo.SpeculativeRealizedSalesProceeds);
                if (eqPortfolioVo.XIRR.ToString().Contains('+'))
                    eqPortfolioVo.XIRR = 0;
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_XIRR", DbType.Double, Math.Round(eqPortfolioVo.XIRR,5));
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createEquityNetPositionCmd, "@CENP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(createEquityNetPositionCmd, "CENP_EquityNPId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(createEquityNetPositionCmd) != 0)
                    eqNPId = int.Parse(db.GetParameterValue(createEquityNetPositionCmd, "CENP_EquityNPId").ToString());


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
                objects[0] = eqPortfolioVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return eqNPId;
        }
        public bool DeleteEquityNetPosition(int customerId, DateTime valuationDate)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteEquityNetPositionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteEquityNetPositionCmd = db.GetStoredProcCommand("SP_DeleteEquityNP");

                db.AddInParameter(deleteEquityNetPositionCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(deleteEquityNetPositionCmd, "@CENP_ValuationDate", DbType.DateTime, valuationDate);

                if (db.ExecuteNonQuery(deleteEquityNetPositionCmd) != 0)

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

        #endregion Equity Portfolio

        #region Mutual Fund Portfolio

        public List<MFPortfolioVo> GetCustomerMFPortfolio(int customerId, DateTime tradeDate)
        {
            List<MFPortfolioVo> mfPortfolioVoList = null;
            MFPortfolioVo mfPortfolioVo = new MFPortfolioVo();
            Database db;
            DbCommand getMFPortfolioCmd;
            DataSet dsGetMFPortfolio;
            DataTable dtGetMFPortfolio;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerMutualFundTransactionSchemePlans");
                db.AddInParameter(getMFPortfolioCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getMFPortfolioCmd, "@CMFT_TransactionDate", DbType.DateTime, tradeDate.ToString());

                dsGetMFPortfolio = db.ExecuteDataSet(getMFPortfolioCmd);
                if (dsGetMFPortfolio.Tables[0].Rows.Count > 0)
                {
                    dtGetMFPortfolio = dsGetMFPortfolio.Tables[0];
                    mfPortfolioVoList = new List<MFPortfolioVo>();
                    foreach (DataRow dr in dtGetMFPortfolio.Rows)
                    {
                        mfPortfolioVo = new MFPortfolioVo();
                        mfPortfolioVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfPortfolioVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfPortfolioVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfPortfolioVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfPortfolioVo.Folio = dr["CMFA_FolioNum"].ToString();


                        mfPortfolioVoList.Add(mfPortfolioVo);
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetCustomerPortfolio(int customerId)");


                object[] objects = new object[2];
                objects[0] = customerId;
                objects[1] = tradeDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfPortfolioVoList;
        }

        public List<MFPortfolioVo> GetCustomerMFPortfolio(int customerId, int portfolioId, DateTime tradeDate, string SchemeNameFilter, string FolioFilter, string categoryFilter)
        {
            List<MFPortfolioVo> mfPortfolioVoList = null;
            MFPortfolioVo mfPortfolioVo = new MFPortfolioVo();
            Database db;
            DbCommand getMFPortfolioCmd;
            DataSet dsGetMFPortfolio;
            DataTable dtGetMFPortfolio;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFPortfolioCmd = db.GetStoredProcCommand("SP_GetCustomerPortfolioMutualFundTransactionSchemePlans");
                db.AddInParameter(getMFPortfolioCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(getMFPortfolioCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getMFPortfolioCmd, "@CMFT_TransactionDate", DbType.DateTime, tradeDate.ToString());

                if (SchemeNameFilter != "")
                    db.AddInParameter(getMFPortfolioCmd, "@nameSearch", DbType.String, SchemeNameFilter);
                else
                    db.AddInParameter(getMFPortfolioCmd, "@nameSearch", DbType.String, DBNull.Value);
                if (FolioFilter != "")
                    db.AddInParameter(getMFPortfolioCmd, "@folioSearch", DbType.String, FolioFilter);
                else
                    db.AddInParameter(getMFPortfolioCmd, "@folioSearch", DbType.String, DBNull.Value);
                if(categoryFilter!="")
                    db.AddInParameter(getMFPortfolioCmd, "@categorySearch", DbType.String, categoryFilter);
                else
                    db.AddInParameter(getMFPortfolioCmd, "@categorySearch", DbType.String, DBNull.Value);
                dsGetMFPortfolio = db.ExecuteDataSet(getMFPortfolioCmd);

                if (dsGetMFPortfolio.Tables[0].Rows.Count > 0)
                {
                    dtGetMFPortfolio = dsGetMFPortfolio.Tables[0];
                    mfPortfolioVoList = new List<MFPortfolioVo>();
                    foreach (DataRow dr in dtGetMFPortfolio.Rows)
                    {
                        mfPortfolioVo = new MFPortfolioVo();
                        mfPortfolioVo.CustomerId = int.Parse(dr["C_CustomerId"].ToString());
                        mfPortfolioVo.MFCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfPortfolioVo.SchemePlan = dr["PASP_SchemePlanName"].ToString();
                        mfPortfolioVo.AccountId = int.Parse(dr["CMFA_AccountId"].ToString());
                        mfPortfolioVo.Folio = dr["CMFA_FolioNum"].ToString();
                        mfPortfolioVo.Category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        mfPortfolioVo.CategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfPortfolioVoList.Add(mfPortfolioVo);
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetCustomerPortfolio(int customerId)");


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
        public DataSet GetProductAssetInstrumentCategory()
        {
            DataSet dsGetProductAssetInstrumentCategory;
            DbCommand dbGetProductAssetInstrumentCategory;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                dbGetProductAssetInstrumentCategory = db.GetStoredProcCommand("SP_GetProductAssetInstrumentCategory");
                dsGetProductAssetInstrumentCategory = db.ExecuteDataSet(dbGetProductAssetInstrumentCategory);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GetProductAssetInstrumentCategory()");


                object[] objects = new object[1];
                objects[0] = "Error in Getting Category code"; 
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsGetProductAssetInstrumentCategory;
        }
        public int IsSchemeEquity(int mfCode)
        {
            int isSchemeEquity = 0;
            Database db;
            DbCommand isMFSchemeEquityCmd;
            DataSet dsisMFSchemeEquity;
            DataTable dtisMFSchemeEquity;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                isMFSchemeEquityCmd = db.GetStoredProcCommand("SP_IsMFSchemeEquity");

                db.AddInParameter(isMFSchemeEquityCmd, "@PASP_SchemePlanCode", DbType.Int32, mfCode);

                dsisMFSchemeEquity = db.ExecuteDataSet(isMFSchemeEquityCmd);
                if (dsisMFSchemeEquity.Tables[0].Rows.Count > 0)
                {
                    dtisMFSchemeEquity = dsisMFSchemeEquity.Tables[0];
                    foreach (DataRow dr in dtisMFSchemeEquity.Rows)
                    {
                        isSchemeEquity = int.Parse(dr["IsEquity"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:IsSchemeEquity(int mfCode)");


                object[] objects = new object[2];
                objects[0] = mfCode;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return isSchemeEquity;
        }
        public DataTable GetCustomerType(int portfolioId)
        {
            
            Database db;
            DbCommand GetCustomerTypeCmd;
            DataSet dsGetCustomerType;
            DataTable dtGetCustomerType;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetCustomerTypeCmd = db.GetStoredProcCommand("SP_GetCustomerType");

                db.AddInParameter(GetCustomerTypeCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);

                dsGetCustomerType = db.ExecuteDataSet(GetCustomerTypeCmd);
                dtGetCustomerType = dsGetCustomerType.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetCustomerType(int portfolioId)");


                object[] objects = new object[2];
                objects[0] = portfolioId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetCustomerType;
        }
        public DataTable GetMFCapGainRate(string customerType,int IsSchemeEquity,DateTime tradeDate)
        {

            Database db;
            DbCommand GetMFCapGainRateCmd;
            DataSet dsGetMFCapGainRate;
            DataTable dtGetMFCapGainRate;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetMFCapGainRateCmd = db.GetStoredProcCommand("SP_GetMFCapitalGainRate");

                db.AddInParameter(GetMFCapGainRateCmd, "@CustomerType", DbType.String, customerType);
                db.AddInParameter(GetMFCapGainRateCmd, "@IsSchemeEquity", DbType.Int16, IsSchemeEquity);
                db.AddInParameter(GetMFCapGainRateCmd, "@TradeDate", DbType.DateTime, tradeDate);

                dsGetMFCapGainRate = db.ExecuteDataSet(GetMFCapGainRateCmd);
                dtGetMFCapGainRate = dsGetMFCapGainRate.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetMFCapGainRate(string customerType,int IsSchemeEquity,DateTime tradeDate)");


                object[] objects = new object[2];
                objects[0] = customerType;
                objects[1] = IsSchemeEquity;
                objects[2] = tradeDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetMFCapGainRate;
        }
        public float GetMFSchemePlanNAV(int schemePlanCode, DateTime navDate)
        {
            float schemePlanNAV = 0;
            Database db;
            DbCommand getMFSchemePlanNAVCmd;
            DataSet dsMFSchemePlanNAV;
            DataTable dtMFSchemePlanNAV;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFSchemePlanNAVCmd = db.GetStoredProcCommand("SP_GetSchemePlanPrice");

                db.AddInParameter(getMFSchemePlanNAVCmd, "@PSP_PostDate", DbType.DateTime, navDate);
                db.AddInParameter(getMFSchemePlanNAVCmd, "@PASP_SchemePlanCode", DbType.Int32, schemePlanCode);
                dsMFSchemePlanNAV = db.ExecuteDataSet(getMFSchemePlanNAVCmd);
                if (dsMFSchemePlanNAV.Tables[0].Rows.Count > 0)
                {
                    dtMFSchemePlanNAV = dsMFSchemePlanNAV.Tables[0];
                    foreach (DataRow dr in dtMFSchemePlanNAV.Rows)
                    {
                        schemePlanNAV = float.Parse(dr["PSP_NetAssetValue"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetMFScripClosePrice(int scripCode, DateTime priceDate)");


                object[] objects = new object[2];
                objects[0] = schemePlanNAV;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return schemePlanNAV;
        }
        public float GetMFSchemePlanSnapShotNAV(int schemePlanCode)
        {
            float schemePlanNAV = 0;
            Database db;
            DbCommand getMFSchemePlanNAVCmd;
            DataSet dsMFSchemePlanNAV;
            DataTable dtMFSchemePlanNAV;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFSchemePlanNAVCmd = db.GetStoredProcCommand("SP_GetSchemePlanSnapShotPrice");


                db.AddInParameter(getMFSchemePlanNAVCmd, "@PASP_SchemePlanCode", DbType.Int32, schemePlanCode);
                dsMFSchemePlanNAV = db.ExecuteDataSet(getMFSchemePlanNAVCmd);
                if (dsMFSchemePlanNAV.Tables[0].Rows.Count > 0)
                {
                    dtMFSchemePlanNAV = dsMFSchemePlanNAV.Tables[0];
                    foreach (DataRow dr in dtMFSchemePlanNAV.Rows)
                    {
                        schemePlanNAV = float.Parse(dr["PASPSP_NetAssetValue"].ToString());
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

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetMFScripClosePrice(int scripCode, DateTime priceDate)");


                object[] objects = new object[2];
                objects[0] = schemePlanNAV;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return schemePlanNAV;
        }
        public int AddMutualFundNetPosition(MFPortfolioVo mfPortfolioVo, int userId)
        {
            int mfNPId = 0;
            Database db;
            DbCommand createMFNetPositionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFNetPositionCmd = db.GetStoredProcCommand("SP_AddMutualFundNetPosition");

                db.AddInParameter(createMFNetPositionCmd, "@CMFA_AccountId", DbType.Int32, mfPortfolioVo.AccountId);
                db.AddInParameter(createMFNetPositionCmd, "@PASP_SchemePlanCode", DbType.Int32, mfPortfolioVo.MFCode);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_NetHoldings", DbType.Decimal, mfPortfolioVo.Quantity);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_MarketPrice", DbType.Decimal, mfPortfolioVo.CurrentNAV);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_DividendIncome", DbType.Decimal, mfPortfolioVo.DividendIncome);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_ValuationDate", DbType.DateTime, mfPortfolioVo.ValuationDate);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_SalesQuantity", DbType.Decimal, mfPortfolioVo.SalesQuantity);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_RealizedSaleProceeds", DbType.Decimal, mfPortfolioVo.RealizedSalesProceed);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_AveragePrice", DbType.Decimal, mfPortfolioVo.AveragePrice);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_RealizedPNL", DbType.Decimal, mfPortfolioVo.RealizedPNL);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_CostOfSales", DbType.Decimal, mfPortfolioVo.CostOfSales);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_NetCost", DbType.Decimal, mfPortfolioVo.NetCost);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_AbsoluteReturn", DbType.Double, mfPortfolioVo.AbsoluteReturn);
                if (mfPortfolioVo.XIRR.ToString().Contains('+'))
                    mfPortfolioVo.XIRR = 0;
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_XIRR", DbType.Double, mfPortfolioVo.XIRR);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_AnnualReturn", DbType.Double, mfPortfolioVo.AnnualReturn);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_CurrentValue", DbType.Decimal, mfPortfolioVo.CurrentValue);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_CostOfPurchase", DbType.Double, mfPortfolioVo.CostOfPurchase);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_NotionalPL", DbType.Double, mfPortfolioVo.UnRealizedPNL);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_TotalPL", DbType.Double, mfPortfolioVo.TotalPNL);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_DividendPayout", DbType.Double, mfPortfolioVo.DividendPayout);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_DividendReinvested", DbType.Double, mfPortfolioVo.DividendReinvested);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_AcqCostExclDivReinvst", DbType.Double, mfPortfolioVo.AcqCostExclDivReinvst);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_STCG", DbType.Double, mfPortfolioVo.STCG);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_LTCG", DbType.Double, mfPortfolioVo.LTCG);
                if( mfPortfolioVo.DateOfAcq!=null &&  mfPortfolioVo.DateOfAcq!=DateTime.MinValue)
                    db.AddInParameter(createMFNetPositionCmd, "@CMFNP_DateOfAcq", DbType.DateTime, mfPortfolioVo.DateOfAcq);
                else
                    db.AddInParameter(createMFNetPositionCmd, "@CMFNP_DateOfAcq", DbType.DateTime, DBNull.Value);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_STCGEligible", DbType.Double, mfPortfolioVo.STCGEligible);
                db.AddInParameter(createMFNetPositionCmd, "@CMFNP_LTCGEligible", DbType.Double, mfPortfolioVo.LTCGEligible);

                db.AddOutParameter(createMFNetPositionCmd, "CMFNP_MFNPId", DbType.Int32, 5000);
               if (db.ExecuteNonQuery(createMFNetPositionCmd) != 0)
                    mfNPId = int.Parse(db.GetParameterValue(createMFNetPositionCmd, "CMFNP_MFNPId").ToString());


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
                objects[0] = mfPortfolioVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return mfNPId;
        }

        public DataSet PopulateEQTradeYear()
        {
            DataSet ds = null;
            Database db;
            DbCommand populateEQTradeYearCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                populateEQTradeYearCmd = db.GetStoredProcCommand("SP_AdviserEquityDailyValuation_Year");
                ds = db.ExecuteDataSet(populateEQTradeYearCmd);
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
        public DataSet PopulateEQTradeMonth(int year)
        {
            DataSet ds = null;
            Database db;
            DbCommand populateEQTradeMonthCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                populateEQTradeMonthCmd = db.GetStoredProcCommand("SP_AdviserEquityDailyValuation_Month");
                db.AddInParameter(populateEQTradeMonthCmd, "@WTD_Year", DbType.Int16, year);
                ds = db.ExecuteDataSet(populateEQTradeMonthCmd);
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
        public int CreateAdviserEODLog(AdviserDailyLOGVo adviserDailyLOGVo)
        {
            int EODLogId = 0;
            Database db;
            DbCommand updateDailyLogCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateDailyLogCmd = db.GetStoredProcCommand("SP_CreateAdviserEODLog");

                db.AddInParameter(updateDailyLogCmd, "@ADEL_ProcessDate", DbType.DateTime, adviserDailyLOGVo.ProcessDate);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_StartTime", DbType.DateTime, adviserDailyLOGVo.StartTime);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_IsValuationComplete", DbType.Int32, adviserDailyLOGVo.IsValuationComplete);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_IsEquityCleanUpComplete", DbType.Int32, adviserDailyLOGVo.IsEquityCleanUpComplete);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_CreatedBy", DbType.Int32, adviserDailyLOGVo.CreatedBy);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_ModifiedBy", DbType.Int32, adviserDailyLOGVo.ModifiedBy);
                db.AddInParameter(updateDailyLogCmd, "@A_AdviserId", DbType.Int32, adviserDailyLOGVo.AdviserId);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_AssetGroup", DbType.String, adviserDailyLOGVo.AssetGroup);
                db.AddOutParameter(updateDailyLogCmd, "ADEL_EODLogId", DbType.Int32, 5000);

                if (db.ExecuteNonQuery(updateDailyLogCmd) != 0)
                    EODLogId = int.Parse(db.GetParameterValue(updateDailyLogCmd, "ADEL_EODLogId").ToString());


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:CreateAdviserEODLog()");


                object[] objects = new object[1];
                objects[0] = adviserDailyLOGVo;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return EODLogId;
        }
        public bool UpdateAdviserEODLog(AdviserDailyLOGVo adviserDailyLOGVo)
        {
            bool bResult = false;
            Database db;
            DbCommand updateDailyLogCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateDailyLogCmd = db.GetStoredProcCommand("SP_UpdateAdviserEODLog");

                db.AddInParameter(updateDailyLogCmd, "@ADEL_EODLogId", DbType.Int32, adviserDailyLOGVo.EODLogId);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_IsValuationComplete", DbType.Int32, adviserDailyLOGVo.IsValuationComplete);

                db.AddInParameter(updateDailyLogCmd, "@ADEL_ModifiedBy", DbType.Int32, adviserDailyLOGVo.ModifiedBy);
                db.AddInParameter(updateDailyLogCmd, "@ADEL_EndTime", DbType.DateTime, adviserDailyLOGVo.EndTime);


                if (db.ExecuteNonQuery(updateDailyLogCmd) != 0)

                    bResult = true;
                //  ADEL_EndTime,

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

        public List<int> GetAdviserCustomerList_EQ(int adviserId)
        {
            List<int> customerList = null;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet ds;

            try
            {
                customerList = new List<int>();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_AdviserDailyValuationEquityCustomerList");
                db.AddInParameter(getCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);


                ds = db.ExecuteDataSet(getCustomerListCmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<int>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["CustomerId"].ToString() != string.Empty)
                        {
                            int customerId = int.Parse(dr["CustomerId"].ToString());
                            customerList.Add(customerId);
                        }


                    }
                }
                else
                {
                    customerList = null;
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

        public List<int> GetAdviserCustomerList_MF(int adviserId)
        {
            List<int> customerList = null;
            Database db;
            DbCommand getCustomerListCmd;
            DataSet ds;

            try
            {
                customerList = new List<int>();
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCustomerListCmd = db.GetStoredProcCommand("SP_AdviserDailyValuationMFCustomerList");
                db.AddInParameter(getCustomerListCmd, "@A_AdviserId", DbType.Int32, adviserId);


                ds = db.ExecuteDataSet(getCustomerListCmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    customerList = new List<int>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int customerId = int.Parse(dr["CustomerId"].ToString());

                        customerList.Add(customerId);
                    }
                }
                else
                {
                    customerList = null;
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
                FunctionInfo.Add("Method", "AdvisorDao.cs:GetAdviserCustomerList_MF()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return customerList;
        }

        public DataSet GetAdviserValuationDate(int adviserId, string assetGroup, int Month, int Year)
        {
            DataSet ds = null;
            Database db;
            DbCommand populateEQTradeDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                populateEQTradeDateCmd = db.GetStoredProcCommand("SP_GetAdviserValuationDate");
                db.AddInParameter(populateEQTradeDateCmd, "@AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(populateEQTradeDateCmd, "@AssetGroup", DbType.String, assetGroup);
                db.AddInParameter(populateEQTradeDateCmd, "@Month", DbType.Int32, Month);
                db.AddInParameter(populateEQTradeDateCmd, "@Year", DbType.Int32, Year);

                ds = db.ExecuteDataSet(populateEQTradeDateCmd);
                // Count = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:GetAdviserValuationDate()");
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


        public bool UpdateAdviserDailyEODRevaluation(int adviserId, string assetGroup, DateTime ProcessDate)
        {

            Database db;
            DbCommand updateAdviserDailyEODRevaluationCmd;
            bool blResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateAdviserDailyEODRevaluationCmd = db.GetStoredProcCommand("SP_UpdateAdviserDailyEODRevaluation");

                db.AddInParameter(updateAdviserDailyEODRevaluationCmd, "@ProcessDate", DbType.DateTime, ProcessDate);
                db.AddInParameter(updateAdviserDailyEODRevaluationCmd, "@A_AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(updateAdviserDailyEODRevaluationCmd, "@AssetGroup", DbType.String, assetGroup);
                if (db.ExecuteNonQuery(updateAdviserDailyEODRevaluationCmd) != 0)
                    blResult = true;
                // Count = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:UpdateAdviserDailyEODRevaluation()");
                object[] objects = new object[3];
                objects[0] = assetGroup;
                objects[1] = adviserId;
                objects[2] = ProcessDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        public bool UpdateAdviserDailyEODLogRevaluateForTransaction(int adviserId, string assetGroup, DateTime ProcessDate)
        {

            Database db;
            DbCommand updateAdviserDailyEODRevaluationForTransactionCmd;
            bool blResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                updateAdviserDailyEODRevaluationForTransactionCmd = db.GetStoredProcCommand("SP_UpdateAdviserDailyEODLogRevaluateForTransaction");
                updateAdviserDailyEODRevaluationForTransactionCmd.CommandTimeout = 0;
                db.AddInParameter(updateAdviserDailyEODRevaluationForTransactionCmd, "@TransactionDate", DbType.DateTime, ProcessDate);
                db.AddInParameter(updateAdviserDailyEODRevaluationForTransactionCmd, "@AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(updateAdviserDailyEODRevaluationForTransactionCmd, "@AssetGroup", DbType.String, assetGroup);
                if (db.ExecuteNonQuery(updateAdviserDailyEODRevaluationForTransactionCmd) != 0)
                    blResult = true;
                // Count = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());

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
                object[] objects = new object[3];
                objects[0] = assetGroup;
                objects[1] = adviserId;
                objects[2] = ProcessDate;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }


        public bool DeleteAdviserEODLog(int adviserId, string assetGroup, DateTime ProcessDate, int IsValuationComplete)
        {

            Database db;
            DbCommand deleteAdviserEODLogCmd;
            bool blResult = false;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteAdviserEODLogCmd = db.GetStoredProcCommand("SP_DeleteAdviserEODLog");
                db.AddInParameter(deleteAdviserEODLogCmd, "@A_AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(deleteAdviserEODLogCmd, "@AssetGroup", DbType.String, assetGroup);
                db.AddInParameter(deleteAdviserEODLogCmd, "@ProcessDate", DbType.DateTime, ProcessDate);
                db.AddInParameter(deleteAdviserEODLogCmd, "@IsValuationComplete", DbType.Int16, IsValuationComplete);

                if (db.ExecuteNonQuery(deleteAdviserEODLogCmd) != 0)
                    blResult = true;
                // Count = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:DeleteAdviserEODLog()");
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

        public DataSet PopulateEQTradeDay(int year, int month, int adviserId, string assetGroup)
        {
            DataSet ds = null;
            Database db;
            DbCommand populateEQTradeDayCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                populateEQTradeDayCmd = db.GetStoredProcCommand("SP_AdviserEquityDailyValuation_TradeDate");
                db.AddInParameter(populateEQTradeDayCmd, "@WTD_Year", DbType.Int16, year);
                db.AddInParameter(populateEQTradeDayCmd, "@WTD_Month", DbType.Int16, month);
                db.AddInParameter(populateEQTradeDayCmd, "@A_AdviserId", DbType.Int16, adviserId);
                db.AddInParameter(populateEQTradeDayCmd, "@ADEL_AssetGroup", DbType.String, assetGroup);
                ds = db.ExecuteDataSet(populateEQTradeDayCmd);
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

        public DataSet PopulateEQTradeDate(int adviserId)
        {
            DataSet ds = null;
            Database db;
            DbCommand popualteEQTradeDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                popualteEQTradeDateCmd = db.GetStoredProcCommand("SP_AdviserDailyValuationEquityTradeDate");

                db.AddInParameter(popualteEQTradeDateCmd, "@A_AdviserId", DbType.Int32, adviserId);
                ds = db.ExecuteDataSet(popualteEQTradeDateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerPortfolioDao.cs:PopulateEQTradeDate()");


                object[] objects = new object[1];
                objects[0] = adviserId;


                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return ds;
        }
        public bool DeleteMutualFundNetPosition(int customerId, DateTime valuationDate)
        {
            bool bResult = false;
            Database db;
            DbCommand deleteMFNetPositionCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteMFNetPositionCmd = db.GetStoredProcCommand("SP_DeleteMutualFundNP");

                db.AddInParameter(deleteMFNetPositionCmd, "@C_CustomerId", DbType.Int32, customerId);
                db.AddInParameter(deleteMFNetPositionCmd, "@CMFNP_ValuationDate", DbType.DateTime, valuationDate);

                if (db.ExecuteNonQuery(deleteMFNetPositionCmd) != 0)

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
        #endregion

       
    }
}
