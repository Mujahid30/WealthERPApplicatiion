using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using VoCustomerPortfolio;
using VoUser;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoCustomerPortfolio
{
    public class GoldDao
    {
        public bool CreateGoldNetPosition(GoldVo goldVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createGoldPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createGoldPortfolioCmd = db.GetStoredProcCommand("SP_CreateGoldNetPosition");
                db.AddInParameter(createGoldPortfolioCmd, "@CP_PortfolioId", DbType.Int32, goldVo.PortfolioId);
                db.AddInParameter(createGoldPortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, goldVo.AssetCategoryCode);
                db.AddInParameter(createGoldPortfolioCmd, "@PAG_AssetGroupCode", DbType.String, goldVo.AssetGroupCode);
                db.AddInParameter(createGoldPortfolioCmd, "@XMC_MeasureCode", DbType.String, goldVo.MeasureCode);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_Name", DbType.String, goldVo.Name);
                if (goldVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(createGoldPortfolioCmd, "@CGNP_PurchaseDate", DbType.DateTime, goldVo.PurchaseDate);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_PurchasePrice", DbType.Decimal, goldVo.PurchasePrice);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_Quantity", DbType.Decimal, goldVo.Quantity);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_PurchaseValue", DbType.Decimal, goldVo.PurchaseValue);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_CurrentPrice", DbType.Decimal, goldVo.CurrentPrice);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_CurrentValue", DbType.Decimal, goldVo.CurrentValue);
                if (goldVo.SellDate != DateTime.MinValue)
                    db.AddInParameter(createGoldPortfolioCmd, "@CGNP_SellDate", DbType.DateTime, goldVo.SellDate);
                else
                    db.AddInParameter(createGoldPortfolioCmd, "@CGNP_SellDate", DbType.DateTime, DBNull.Value);

                if (goldVo.SellPrice != 0)
                    db.AddInParameter(createGoldPortfolioCmd, "@CGNP_SellPrice", DbType.Decimal, goldVo.SellPrice);
                else
                    db.AddInParameter(createGoldPortfolioCmd, "@CGNP_SellPrice", DbType.Decimal, DBNull.Value);

                if (goldVo.SellValue != 0)
                    db.AddInParameter(createGoldPortfolioCmd, "@CGNP_SellValue", DbType.Decimal, goldVo.SellValue);
                else
                    db.AddInParameter(createGoldPortfolioCmd, "@CGNP_SellValue", DbType.Decimal, DBNull.Value);

                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_Remark", DbType.String, goldVo.Remarks);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createGoldPortfolioCmd, "@CGNP_ModifiedBy", DbType.Int32, userId);
                if (db.ExecuteNonQuery(createGoldPortfolioCmd) != 0)
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

                FunctionInfo.Add("Method", "GoldDao.cs:SP_CreateGoldNetPosition()");

                object[] objects = new object[2];
                objects[0] = goldVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public List<GoldVo> GetGoldNetPosition(int portfolioId, int CurrentPage, string SortOrder, out int Count)
        {
            List<GoldVo> goldList = null;
            GoldVo goldVo;
            Database db;
            DbCommand getGoldPortfolioCmd;
            DataSet dsGetGoldPortfolio;
            DataTable dtGetGoldPortfolio;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGoldPortfolioCmd = db.GetStoredProcCommand("SP_GetGoldNetPosition");
                db.AddInParameter(getGoldPortfolioCmd, "@CP_PortfolioId", DbType.Int32, portfolioId);
                db.AddInParameter(getGoldPortfolioCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getGoldPortfolioCmd, "@SortOrder", DbType.String, SortOrder);
                dsGetGoldPortfolio = db.ExecuteDataSet(getGoldPortfolioCmd);
                if (dsGetGoldPortfolio.Tables[0].Rows.Count > 0)
                {
                    dtGetGoldPortfolio = dsGetGoldPortfolio.Tables[0];

                    goldList = new List<GoldVo>();

                    foreach (DataRow dr in dtGetGoldPortfolio.Rows)
                    {
                        goldVo = new GoldVo();

                        goldVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        goldVo.GoldNPId = int.Parse(dr["CGNP_GoldNPId"].ToString());
                        goldVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        goldVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        goldVo.MeasureCode = dr["XMC_MeasureCode"].ToString();
                        goldVo.Name = dr["CGNP_Name"].ToString();
                        if (dr["CGNP_PurchaseDate"] != DBNull.Value)
                            goldVo.PurchaseDate = DateTime.Parse(dr["CGNP_PurchaseDate"].ToString());
                        goldVo.PurchasePrice = double.Parse(dr["CGNP_PurchasePrice"].ToString());
                        goldVo.Quantity = float.Parse(dr["CGNP_Quantity"].ToString());
                        goldVo.PurchaseValue = double.Parse(dr["CGNP_PurchaseValue"].ToString());
                        goldVo.CurrentPrice = double.Parse(dr["CGNP_CurrentPrice"].ToString());
                        goldVo.CurrentValue = double.Parse(dr["CGNP_CurrentValue"].ToString());
                        goldVo.Remarks = dr["CGNP_Remark"].ToString();
                        goldVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        //goldVo.SellDate=DateTime.Parse( dr["CGNP_SellDate"].ToString());
                        //goldVo.SellPrice=float.Parse( dr["CGNP_SellPrice"].ToString());
                        //goldVo.SellValue=float.Parse( dr["CGNP_SellValue"].ToString());

                        goldList.Add(goldVo);
                    }
                }
                if (dsGetGoldPortfolio.Tables[1] != null && dsGetGoldPortfolio.Tables[0].Rows.Count > 0)
                    Count = Int32.Parse(dsGetGoldPortfolio.Tables[1].Rows[0][0].ToString());
                else
                    Count = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "GoldDao.cs:GetGoldNetPosition()");


                object[] objects = new object[1];
                objects[0] = portfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return goldList;

        }

        public GoldVo GetGoldAsset(int GoldNPId)
        {

            GoldVo goldVo = null;
            Database db;
            DbCommand getGoldPortfolioCmd;
            DataSet dsGetGoldPortfolio;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getGoldPortfolioCmd = db.GetStoredProcCommand("SP_GetGoldAsset");
                db.AddInParameter(getGoldPortfolioCmd, "@CGNP_GoldNPId", DbType.Int32, GoldNPId);

                dsGetGoldPortfolio = db.ExecuteDataSet(getGoldPortfolioCmd);
                if (dsGetGoldPortfolio.Tables[0].Rows.Count > 0)
                {
                    dr = dsGetGoldPortfolio.Tables[0].Rows[0];

                    goldVo = new GoldVo();

                    goldVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    goldVo.GoldNPId = int.Parse(dr["CGNP_GoldNPId"].ToString());
                    goldVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    goldVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                    goldVo.MeasureCode = dr["XMC_MeasureCode"].ToString();
                    goldVo.Name = dr["CGNP_Name"].ToString();
                    if (dr["CGNP_PurchaseDate"] != null && dr["CGNP_PurchaseDate"] != DBNull.Value)
                        goldVo.PurchaseDate = DateTime.Parse(dr["CGNP_PurchaseDate"].ToString());
                    if (dr["CGNP_PurchasePrice"] != DBNull.Value && dr["CGNP_PurchasePrice"] != string.Empty)
                        goldVo.PurchasePrice = double.Parse(dr["CGNP_PurchasePrice"].ToString());
                    if (dr["CGNP_Quantity"] != DBNull.Value && dr["CGNP_Quantity"] != "")
                        goldVo.Quantity = float.Parse(dr["CGNP_Quantity"].ToString());
                    if (dr["CGNP_PurchaseValue"] != DBNull.Value && dr["CGNP_PurchaseValue"] != "")
                        goldVo.PurchaseValue = double.Parse(dr["CGNP_PurchaseValue"].ToString());
                    if (dr["CGNP_CurrentPrice"] != DBNull.Value && dr["CGNP_CurrentPrice"] != "")
                        goldVo.CurrentPrice = double.Parse(dr["CGNP_CurrentPrice"].ToString());
                    if (dr["CGNP_CurrentValue"] != DBNull.Value && dr["CGNP_CurrentValue"] != "")
                        goldVo.CurrentValue = double.Parse(dr["CGNP_CurrentValue"].ToString());
                    if (dr["CGNP_SellDate"].ToString() != "")
                        goldVo.SellDate = DateTime.Parse(dr["CGNP_SellDate"].ToString());
                    if (dr["CGNP_SellPrice"] != DBNull.Value && dr["CGNP_SellPrice"].ToString() != "")
                        goldVo.SellPrice = double.Parse(dr["CGNP_SellPrice"].ToString());
                    if (dr["CGNP_SellValue"] != DBNull.Value && dr["CGNP_SellValue"].ToString() != "")
                        goldVo.SellValue = double.Parse(dr["CGNP_SellValue"].ToString());
                    goldVo.Remarks = dr["CGNP_Remark"].ToString();
                    goldVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
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

                FunctionInfo.Add("Method", "GoldDao.cs:GetGoldAsset()");


                object[] objects = new object[1];
                objects[0] = GoldNPId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return goldVo;

        }

        public bool UpdateGoldNetPosition(GoldVo goldVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateGoldPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateGoldPortfolioCmd = db.GetStoredProcCommand("SP_UpdateGoldNetPosition");
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_GoldNPId", DbType.Int32, goldVo.GoldNPId);
                db.AddInParameter(updateGoldPortfolioCmd, "@CP_PortfolioId", DbType.Int32, goldVo.PortfolioId);
                db.AddInParameter(updateGoldPortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, goldVo.AssetCategoryCode);
                db.AddInParameter(updateGoldPortfolioCmd, "@PAG_AssetGroupCode", DbType.String, goldVo.AssetGroupCode);
                db.AddInParameter(updateGoldPortfolioCmd, "@XMC_MeasureCode", DbType.String, goldVo.MeasureCode);
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_Name", DbType.String, goldVo.Name);
                if (goldVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_PurchaseDate", DbType.DateTime, goldVo.PurchaseDate);
                else
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_PurchaseDate", DbType.DateTime, DBNull.Value);

                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_PurchasePrice", DbType.Decimal, goldVo.PurchasePrice);
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_Quantity", DbType.Decimal, goldVo.Quantity);
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_PurchaseValue", DbType.Decimal, goldVo.PurchaseValue);
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_CurrentPrice", DbType.Decimal, goldVo.CurrentPrice);
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_CurrentValue", DbType.Decimal, goldVo.CurrentValue);

                if (goldVo.SellDate != DateTime.MinValue)
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellDate", DbType.DateTime, goldVo.SellDate);
                else
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellDate", DbType.DateTime, DBNull.Value);

                if (goldVo.SellPrice != 0)
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellPrice", DbType.Decimal, goldVo.SellPrice);
                else
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellPrice", DbType.Decimal, DBNull.Value);

                if (goldVo.SellValue != 0)
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellValue", DbType.Decimal, goldVo.SellValue);
                else
                    db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellValue", DbType.Decimal, DBNull.Value);

                //db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellDate", DbType.DateTime, goldVo.SellDate);
                //db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellPrice", DbType.Decimal, goldVo.SellPrice);
                //db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_SellValue", DbType.Decimal, goldVo.SellValue);
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(updateGoldPortfolioCmd, "@CGNP_Remark", DbType.String, goldVo.Remarks);

                if (db.ExecuteNonQuery(updateGoldPortfolioCmd) != 0)
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

                FunctionInfo.Add("Method", "GoldDao.cs:UpdateGoldNetPosition()");

                object[] objects = new object[2];
                objects[0] = goldVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool DeleteGoldPortfolio(int goldId)
        {
            bool bResult = false;

            Database db;
            DbCommand deleteGoldPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteGoldPortfolioCmd = db.GetStoredProcCommand("SP_DeleteGoldNetPostion");

                db.AddInParameter(deleteGoldPortfolioCmd, "@CGNP_GoldNPId", DbType.Int32, goldId);

                if (db.ExecuteNonQuery(deleteGoldPortfolioCmd) != 0)
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
                FunctionInfo.Add("Method", "GoldDao.cs:DeleteGoldPortfolio()");
                object[] objects = new object[1];
                objects[0] = goldId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }
    }
}
