using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VoWerpAdmin;

namespace DaoWerpAdmin
{
  public  class MappingDao
    {
        public DataSet GetInstrumentCategory(string AssetGroup)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetAssetInstrumentCat");
                db.AddInParameter(Cmd, "@PAG_AssetGroupCode", DbType.String, AssetGroup);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataSet GetAssetInstrumentSubCategory(string AssetGroup, string instrumentCategory)
        {
            Database db;
            DbCommand getAssetInstrumentSubCategoryCmd;
            DataSet assetSubCategories;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetInstrumentSubCategoryCmd = db.GetStoredProcCommand("SP_GetAssetInstrumentSubCat");
                db.AddInParameter(getAssetInstrumentSubCategoryCmd, "PAG_AssetGroupCode", DbType.String, AssetGroup);
                db.AddInParameter(getAssetInstrumentSubCategoryCmd, "PAIC_AssetInstrumentCategoryCode", DbType.String, instrumentCategory);
                assetSubCategories = db.ExecuteDataSet(getAssetInstrumentSubCategoryCmd);
                return assetSubCategories;
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }

        public DataSet GetAssetInstrumentSubSubCategory(string AssetGroup, string instrumentCategory, string instrumentSubCategory)
        {
            Database db;
            DbCommand getAssetInstrumentSubSubCategoryCmd;
            DataSet assetSubSubCategories;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getAssetInstrumentSubSubCategoryCmd = db.GetStoredProcCommand("SP_GetAssetInstrumentSubSubCat");
                db.AddInParameter(getAssetInstrumentSubSubCategoryCmd, "PAG_AssetGroupCode", DbType.String, AssetGroup);
                db.AddInParameter(getAssetInstrumentSubSubCategoryCmd, "PAIC_AssetInstrumentCategoryCode", DbType.String, instrumentCategory);
                db.AddInParameter(getAssetInstrumentSubSubCategoryCmd, "PAISC_AssetInstrumentSubCategoryCode", DbType.String, instrumentSubCategory);
                assetSubSubCategories = db.ExecuteDataSet(getAssetInstrumentSubSubCategoryCmd);
                return assetSubSubCategories;
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }

        public DataSet GetProductSectorClassification()
        {
            DataSet ds;
            DbCommand cmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetProductSectorClassification ");
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataSet GetProductMarketCapClassification()
        {
            DataSet ds;
            DbCommand cmd;
            Database db;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_GetProductMarketCapClassification");
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public string SubmitEquityMapping(string AssetGroup, MappingVo objVo)
        {

            Database db;
            DbCommand cmd;
            DataSet ds;
            string WERPCode = String.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_EquityMappingToScripMapping");
                db.AddInParameter(cmd, "@Mode", DbType.String, objVo.Mode);
                db.AddInParameter(cmd, "@WERPCODE", DbType.Int32, Convert.ToInt32(objVo.WERPCOde));
                db.AddInParameter(cmd, "@ScripName", DbType.String, objVo.ScripName);
                db.AddInParameter(cmd, "@Ticker", DbType.String, objVo.Ticker);
                db.AddInParameter(cmd, "@IncorporationDate", DbType.DateTime, objVo.IncorporationDate);
                db.AddInParameter(cmd, "@PublicIssueDate", DbType.DateTime, objVo.PublishDate);
                db.AddInParameter(cmd, "@MarketLot", DbType.Int32, objVo.MarketLot);
                db.AddInParameter(cmd, "@FaceValue", DbType.Decimal, objVo.FaceValue);
                db.AddInParameter(cmd, "@BookClosure", DbType.DateTime, objVo.BookClosure);
                db.AddInParameter(cmd, "@InstrumentCategory", DbType.String, objVo.InstrumentCategory);
                db.AddInParameter(cmd, "@SubCategory", DbType.String, objVo.SubCategory);
                //db.AddInParameter(cmd, "@Sector", DbType.Int32, objVo.Sector);
                //   db.AddInParameter(cmd, "@MarketCap", DbType.Int32, objVo.MarketCap);
                db.AddInParameter(cmd, "@BSE", DbType.String, objVo.BSE);
                db.AddInParameter(cmd, "@NSE", DbType.String, objVo.NSE);
                db.AddInParameter(cmd, "@CERC", DbType.String, objVo.CERC);
                //db.AddOutParameter(cmd, "WERPCode", DbType.Int32, 1000);
                // db.ExecuteNonQuery(cmd);
                ds = db.ExecuteDataSet(cmd);
                WERPCode = ds.Tables[0].Rows[0][0].ToString();
                return WERPCode;

            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public string SubmitMFMapping(string AssetGroup, MappingVo objVo)
        {
            Database db;
            DbCommand cmd;
            DataSet ds;
            string WERPCode = String.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SP_MFMappingToProductAMCSchemePlan");
                db.AddInParameter(cmd, "@Mode", DbType.String, objVo.Mode);
                db.AddInParameter(cmd, "@WERPCODE", DbType.Int32, Convert.ToInt32(objVo.MFWERPCode));
                db.AddInParameter(cmd, "@SchmPlanName", DbType.String, objVo.SchemePlanName);
                db.AddInParameter(cmd, "@InstrumentCategory", DbType.String, objVo.MFInstrumentCategory);
                db.AddInParameter(cmd, "@SubCategory", DbType.String, objVo.MFSubCategory);
                db.AddInParameter(cmd, "@SubSubCategory", DbType.String, objVo.MFSubSubCategory);
                db.AddInParameter(cmd, "@Sector", DbType.Int32, objVo.MFSector);
                db.AddInParameter(cmd, "@MarketCap", DbType.Int32, objVo.MFMarketCap);
                db.AddInParameter(cmd, "@AMFI", DbType.String, objVo.AMFI);
                db.AddInParameter(cmd, "@CAMS", DbType.String, objVo.CAMS);
                db.AddInParameter(cmd, "@Karvy", DbType.String, objVo.Karvy);
                db.AddOutParameter(cmd, "WERPCode", DbType.Int32, 1000);
                db.ExecuteNonQuery(cmd);
                ds = db.ExecuteDataSet(cmd);
                WERPCode = ds.Tables[0].Rows[0][0].ToString();
                return WERPCode;

            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public DataSet GetProductEquityScripDetails(int WERPCODE)
        {
            DataSet ds;
            Database db;
            DbCommand Cmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductEquityScripDetails");
                db.AddInParameter(Cmd, "@WERPCODE", DbType.Int32, WERPCODE);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public DataSet GetProductAMCSchemePlanDetails(int WERPCODE)
        {

            DataSet ds;
            Database db;
            DbCommand Cmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Cmd = db.GetStoredProcCommand("SP_GetProductAMCSchemePlanDetails");
                db.AddInParameter(Cmd, "@WERPCODE", DbType.Int32, WERPCODE);
                ds = db.ExecuteDataSet(Cmd);
                return ds;
            }
            catch (SqlTypeException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
    }
}
