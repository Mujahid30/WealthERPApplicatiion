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
using DaoWerpAdmin;
using VoWerpAdmin;

namespace BoWerpAdmin
{
   public class MappingBo
    {
        public DataSet GetInstrumentCategory(string AssetGroup)
        {
            MappingDao obj = new MappingDao();
            return obj.GetInstrumentCategory(AssetGroup);
        }

        public DataSet GetAssetInstrumentSubCategory(string AssetGroup, string instrumentCategory)
        {
            MappingDao obj = new MappingDao();
            return obj.GetAssetInstrumentSubCategory(AssetGroup, instrumentCategory);
        }

        public DataSet GetAssetInstrumentSubSubCategory(string AssetGroup, string instrumentCategory, string instrumentSubCategory)
        {
            MappingDao obj = new MappingDao();
            return obj.GetAssetInstrumentSubSubCategory(AssetGroup, instrumentCategory, instrumentSubCategory);
        }

        public DataSet GetProductSectorClassification()
        {
            MappingDao obj = new MappingDao();
            return obj.GetProductSectorClassification();
        }
        public DataSet GetProductMarketCapClassification()
        {
            MappingDao obj = new MappingDao();
            return obj.GetProductMarketCapClassification();
        }

        public string SubmitEquityMapping(string AssetGroup, MappingVo objVo)
        {
            MappingDao obj = new MappingDao();
            return obj.SubmitEquityMapping(AssetGroup, objVo);
        }

        public string SubmitMFMapping(string AssetGroup, MappingVo objVo)
        {
            MappingDao obj = new MappingDao();
            return obj.SubmitMFMapping(AssetGroup, objVo);

        }

        public MappingVo GetProductAMCSchemePlanDetails(int WERPCODE)
        {
            MappingDao objDao = new MappingDao();
            MappingVo objVo = new MappingVo();
            DataSet ds = objDao.GetProductAMCSchemePlanDetails(WERPCODE);
            DataRow dr = ds.Tables[0].Rows[0];
            objVo.SchemePlanName = dr["SchemePlanName"].ToString();
            objVo.MFWERPCode = dr["SchemePlanCode"].ToString();
            objVo.MFInstrumentCategory = dr["InstrumentCategoryCode"].ToString();
            objVo.MFSubCategory = dr["InstrumentSubCategoryCode"].ToString();
            objVo.MFSubSubCategory = dr["InstrumentSubSubCategoryCode"].ToString();
            //  objVo.MFSector = Convert.ToInt32(dr["SectorID"].ToString());
            // objVo.MarketCap = Convert.ToInt32(dr["MarketCap"].ToString());
            objVo.AMFI = dr["AMFICODE"].ToString();
            objVo.CAMS = dr["CAMSCODE"].ToString();
            objVo.Karvy = dr["KARVYCODE"].ToString();
            return objVo;

        }

        public MappingVo GetProductEquityScripDetails(int WERPCODE)
        {
            MappingDao objDao = new MappingDao();
            MappingVo objVo = new MappingVo();
            DataSet ds = objDao.GetProductEquityScripDetails(WERPCODE);
            DataRow dr;
            dr = ds.Tables[0].Rows[0];
            objVo.WERPCOde = dr["ScripCode"].ToString();
            objVo.ScripName = dr["ScripName"].ToString();
            objVo.Ticker = dr["Ticker"].ToString();
            objVo.IncorporationDate = Convert.ToDateTime(dr["IncorporationDate"].ToString());
            objVo.PublishDate = Convert.ToDateTime(dr["PublicIssueDate"].ToString());
            objVo.MarketLot = Convert.ToInt32(dr["MarketLot"].ToString());
            objVo.FaceValue = Convert.ToDecimal(dr["FaceValue"].ToString());
            objVo.BookClosure = Convert.ToDateTime(dr["BookClosure"].ToString());
            objVo.InstrumentCategory = dr["AssetInstrumentCategoryCode"].ToString();
            objVo.SubCategory = dr["AssetInstrumentSubCategoryCode"].ToString();
            //objVo.Sector = Convert.ToInt32(dr["SectorId"].ToString());
            //objVo.MarketCap = Convert.ToInt32(dr["MarketCap"].ToString());
            objVo.BSE = dr["BSECODE"].ToString();
            objVo.NSE = dr["NSECODE"].ToString();
            objVo.CERC = dr["CERCCODE"].ToString();
            return objVo;

        }
    }
}
