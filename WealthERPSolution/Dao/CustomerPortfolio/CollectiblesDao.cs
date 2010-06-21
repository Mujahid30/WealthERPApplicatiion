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
    public class CollectiblesDao
    {
        public bool CreateCollectiblesPortfolio(CollectiblesVo collectibleVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createCollectiblePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCollectiblePortfolioCmd = db.GetStoredProcCommand("SP_CreateCollectiblesNetPosition");
                db.AddInParameter(createCollectiblePortfolioCmd, "@CP_PortfolioId", DbType.Int32, collectibleVo.PortfolioId);
                db.AddInParameter(createCollectiblePortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, collectibleVo.AssetCategoryCode);
                db.AddInParameter(createCollectiblePortfolioCmd, "@PAG_AssetGroupCode", DbType.String, collectibleVo.AssetGroupCode);
                db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_Name", DbType.String, collectibleVo.Name);
                if(collectibleVo.PurchaseDate!=DateTime.MinValue)
                    db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_PurchaseDate", DbType.DateTime, collectibleVo.PurchaseDate);               
                else
                    db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_PurchaseDate", DbType.DateTime,DBNull.Value);               
                db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_PurchaseValue", DbType.Decimal, collectibleVo.PurchaseValue);                
                db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_CurrentValue", DbType.Decimal, collectibleVo.CurrentValue);
                db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_Remark", DbType.String, collectibleVo.Remark);
                db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createCollectiblePortfolioCmd, "@CCNP_ModifiedBy", DbType.Int32, userId);
               if( db.ExecuteNonQuery(createCollectiblePortfolioCmd)!=0)
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

                FunctionInfo.Add("Method", "CollectibleDao.cs:SP_CreateCollectiblesNetPosition()");


                object[] objects = new object[2];
                objects[0] = collectibleVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public List<CollectiblesVo> GetCollectiblesPortfolio(int PortfolioId, int CurrentPage, string sortOrder, out int count)
        {
            List<CollectiblesVo> collectiblesList = null;
            CollectiblesVo collectiblesVo;
            Database db;
            DbCommand getCollectiblesPortfolioCmd;
            DataSet dsGetCollectiblesPortfolio;
            DataTable dtGetCollectiblesPortfolio;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCollectiblesPortfolioCmd = db.GetStoredProcCommand("SP_GetCollectiblesNetPosition");
                db.AddInParameter(getCollectiblesPortfolioCmd, "@CP_PortfolioId", DbType.Int32, PortfolioId);
                db.AddInParameter(getCollectiblesPortfolioCmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(getCollectiblesPortfolioCmd, "@sortOrder", DbType.String, sortOrder);
                dsGetCollectiblesPortfolio = db.ExecuteDataSet(getCollectiblesPortfolioCmd);
                dtGetCollectiblesPortfolio = dsGetCollectiblesPortfolio.Tables[0];
                if (dsGetCollectiblesPortfolio.Tables[1] != null && dsGetCollectiblesPortfolio.Tables[1].Rows.Count > 0)
                    count = Int32.Parse(dsGetCollectiblesPortfolio.Tables[1].Rows[0][0].ToString());
                else count = 0;
                if (dsGetCollectiblesPortfolio.Tables[0].Rows.Count > 0)
                {
                    collectiblesList = new List<CollectiblesVo>();

                    foreach (DataRow dr in dtGetCollectiblesPortfolio.Rows)
                    {
                        collectiblesVo = new CollectiblesVo();

                        collectiblesVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                        collectiblesVo.CollectibleId = int.Parse(dr["CCNP_CollectibleNPId"].ToString());
                        collectiblesVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        collectiblesVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                        collectiblesVo.Name = dr["CCNP_Name"].ToString();
                        if(dr["CCNP_PurchaseDate"].ToString()!=string.Empty)
                        collectiblesVo.PurchaseDate = DateTime.Parse(dr["CCNP_PurchaseDate"].ToString());
                        collectiblesVo.PurchaseValue = float.Parse(dr["CCNP_PurchaseValue"].ToString());
                        collectiblesVo.CurrentValue = float.Parse(dr["CCNP_CurrentValue"].ToString());
                        collectiblesVo.Remark = dr["CCNP_Remark"].ToString();
                        collectiblesVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        collectiblesList.Add(collectiblesVo);
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

                FunctionInfo.Add("Method", "CollectiblesDao.cs:SP_GetCollectiblesNetPosition()");


                object[] objects = new object[1];
                objects[0] = PortfolioId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return collectiblesList;

        }

        public CollectiblesVo GetCollectibleAsset(int collectibleId)
        {
            CollectiblesVo collectiblesVo=null;
            Database db;
            DbCommand getCollectiblesPortfolioCmd;
            DataSet dsGetCollectiblesPortfolio;
            DataRow dr;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getCollectiblesPortfolioCmd = db.GetStoredProcCommand("SP_GetCollectibleAsset");
                db.AddInParameter(getCollectiblesPortfolioCmd, "@CCNP_CollectibleNPId", DbType.Int32, collectibleId);

                dsGetCollectiblesPortfolio = db.ExecuteDataSet(getCollectiblesPortfolioCmd);
                if (dsGetCollectiblesPortfolio.Tables[0].Rows.Count > 0)
                {
                    dr = dsGetCollectiblesPortfolio.Tables[0].Rows[0];

                    collectiblesVo = new CollectiblesVo();

                    collectiblesVo.PortfolioId = int.Parse(dr["CP_PortfolioId"].ToString());
                    collectiblesVo.CollectibleId = int.Parse(dr["CCNP_CollectibleNPId"].ToString());
                    collectiblesVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();

                    collectiblesVo.AssetGroupCode = dr["PAG_AssetGroupCode"].ToString();
                    collectiblesVo.Name = dr["CCNP_Name"].ToString();
                    if(dr["CCNP_PurchaseDate"].ToString()!=string.Empty)
                        collectiblesVo.PurchaseDate = DateTime.Parse(dr["CCNP_PurchaseDate"].ToString());
                    //   collectiblesVo.PurchasePrice = float.Parse(dr["CCNP_PurchasePrice"].ToString());
                    //collectiblesVo.Quantity = float.Parse(dr["CCNP_Quantity"].ToString());
                    collectiblesVo.PurchaseValue = float.Parse(dr["CCNP_PurchaseValue"].ToString());
                    //collectiblesVo.CurrentPrice = float.Parse(dr["CCNP_CurrentPrice"].ToString());
                    collectiblesVo.CurrentValue = float.Parse(dr["CCNP_CurrentValue"].ToString());
                    collectiblesVo.Remark = dr["CCNP_Remark"].ToString();
                    collectiblesVo.AssetCategoryName = dr["PAIC_AssetInstrumentCategoryName"].ToString();
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

                FunctionInfo.Add("Method", "CollectiblesDao.cs:GetCollectibleAsset()");


                object[] objects = new object[1];
                objects[0] = collectibleId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return collectiblesVo;

        }

        public bool UpdateCollectiblesPortfolio(CollectiblesVo collectibleVo, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand updateCollectiblePortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateCollectiblePortfolioCmd = db.GetStoredProcCommand("SP_UpdateCollectibleNetPosition");
                db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_CollectibleNPId", DbType.Int32, collectibleVo.CollectibleId);              
                db.AddInParameter(updateCollectiblePortfolioCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, collectibleVo.AssetCategoryCode);                
                db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_Name", DbType.String, collectibleVo.Name);
                if (collectibleVo.PurchaseDate != DateTime.MinValue)
                    db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_PurchaseDate", DbType.DateTime, collectibleVo.PurchaseDate);
                else
                    db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_PurchaseDate", DbType.DateTime, DBNull.Value);
                db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_PurchaseValue", DbType.Decimal, collectibleVo.PurchaseValue);             
                db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_CurrentValue", DbType.Decimal, collectibleVo.CurrentValue);
                db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_Remark", DbType.String, collectibleVo.Remark);
                db.AddInParameter(updateCollectiblePortfolioCmd, "@CCNP_ModifiedBy", DbType.Int32, userId);
               if( db.ExecuteNonQuery(updateCollectiblePortfolioCmd)!=0)
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

                FunctionInfo.Add("Method", "CollectibleDao.cs:SP_UpdateCollectibleNetPosition()");


                object[] objects = new object[2];
                objects[0] = collectibleVo;
                objects[1] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return bResult;
        }

        public bool DeleteCollectiblesPortfolio(int collectiblesId)
        {
            bool bResult = false;

            Database db;
            DbCommand deleteCollectiblesPortfolioCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                deleteCollectiblesPortfolioCmd = db.GetStoredProcCommand("SP_DeleteCollectiblesNetPostion");

                db.AddInParameter(deleteCollectiblesPortfolioCmd, "@CCNP_CollectibleNPId", DbType.Int32, collectiblesId);

               if( db.ExecuteNonQuery(deleteCollectiblesPortfolioCmd)!=0)
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
                FunctionInfo.Add("Method", "CollectiblesDao.cs:DeleteCollectiblesPortfolio()");
                object[] objects = new object[1];
                objects[0] = collectiblesId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return bResult;
        }

    }
}
