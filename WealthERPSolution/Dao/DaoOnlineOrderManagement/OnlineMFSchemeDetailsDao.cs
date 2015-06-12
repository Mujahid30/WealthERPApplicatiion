using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Sql;
using VoOnlineOrderManagemnet;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;

namespace DaoOnlineOrderManagement
{
    public class OnlineMFSchemeDetailsDao
    {
            OnlineMFSchemeDetailsVo OnlineMFSchemeDetailsVo = new OnlineMFSchemeDetailsVo();
        public OnlineMFSchemeDetailsVo GetSchemeDetails(int amcCode, int schemeCode, string category)
        {
            Database db;
            DataSet GetSchemeDetailsDs;
            DbCommand GetSchemeDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_GetMFSchemeDetails");
                db.AddInParameter(GetSchemeDetailsCmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(GetSchemeDetailsCmd, "@schemePlanCode", DbType.Int32, schemeCode);
                if (category != "0")
                    db.AddInParameter(GetSchemeDetailsCmd, "@category", DbType.String, category);

                GetSchemeDetailsDs = db.ExecuteDataSet(GetSchemeDetailsCmd);
                if (GetSchemeDetailsDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in GetSchemeDetailsDs.Tables[0].Rows)
                    {
                        OnlineMFSchemeDetailsVo.amcName = dr["PA_AMCName"].ToString();
                        OnlineMFSchemeDetailsVo.schemeName = dr["PASP_SchemePlanName"].ToString();
                        OnlineMFSchemeDetailsVo.fundManager = dr["PMFRD_FundManagerName"].ToString();
                        OnlineMFSchemeDetailsVo.schemeBanchMark = dr["PMFRD_BenchmarksIndexName"].ToString();
                        OnlineMFSchemeDetailsVo.category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        OnlineMFSchemeDetailsVo.exitLoad = int.Parse(dr["PASPD_ExitLoadPercentage"].ToString());
                        if (dr["PMFRD_FixedIncStyleBoxLong"].ToString() != "")
                            OnlineMFSchemeDetailsVo.schemeBox = int.Parse(dr["PMFRD_FixedIncStyleBoxLong"].ToString());
                        OnlineMFSchemeDetailsVo.SchemeReturn3Year = dr["SchemeReturn3Year"].ToString();
                        OnlineMFSchemeDetailsVo.SchemeReturn5Year = dr["SchemeReturn5Year"].ToString();
                        OnlineMFSchemeDetailsVo.SchemeReturn10Year = dr["SchemeReturn10Year"].ToString();
                        OnlineMFSchemeDetailsVo.benchmarkReturn1stYear = dr["PMFRD_Return1Year_BM"].ToString();
                        OnlineMFSchemeDetailsVo.benchmark3rhYear = dr["PMFRD_Return3Year_BM"].ToString();
                        OnlineMFSchemeDetailsVo.benchmark5thdYear = dr["PMFRD_Return5Year_BM"].ToString();
                        if (dr["SchemeRisk3Year"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk3Year = dr["SchemeRisk3Year"].ToString();
                        if (dr["SchemeRisk5Year"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk5Year = dr["SchemeRisk5Year"].ToString();
                        if (dr["SchemeRisk10Year"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["SchemeRisk10Year"].ToString();
                        if (dr["PASPD_IsPurchaseAvailable"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["PASPD_IsPurchaseAvailable"].ToString();
                        if (dr["PASPD_IsRedeemAvailable"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["PASPD_IsRedeemAvailable"].ToString();
                        if (dr["PASPD_IsSIPAvailable"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["PASPD_IsSIPAvailable"].ToString();
                        OnlineMFSchemeDetailsVo.minmumInvestmentAmount = int.Parse(dr["PASPD_InitialPurchaseAmount"].ToString());
                        OnlineMFSchemeDetailsVo.multipleOf = int.Parse(dr["PASPD_InitialMultipleAmount"].ToString());
                        OnlineMFSchemeDetailsVo.minSIPInvestment = int.Parse(dr["PASPSD_MinAmount"].ToString());
                        OnlineMFSchemeDetailsVo.SIPmultipleOf = int.Parse(dr["PASPSD_MultipleAmount"].ToString());
                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return OnlineMFSchemeDetailsVo;
        }
        public bool CustomerAddMFSchemeToWatch(int customerId, int schemeCode, string assetGroup, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_ONL_AddProductToCustomerWatchList");
                db.AddInParameter(createCmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(createCmd, "@productId", DbType.Int32, schemeCode);
                db.AddInParameter(createCmd, "@assetGroup", DbType.String, assetGroup);
                db.AddInParameter(createCmd, "@userId", DbType.Int64, userId);
                if (db.ExecuteNonQuery(createCmd) != 0)
                    bResult = true;
                else
                    bResult = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineMFSchemeDetailsDao.cs:CustomerAddMFSchemeToWatch()");
                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = schemeCode;
                objects[2] = assetGroup;
                objects[3] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }


        public string GetCmotCode(int schemeplanCode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdGetCmotCode;
            string cmotCode = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetCmotCode = db.GetStoredProcCommand("SPROC_Onl_GetCMOTCode");
                db.AddInParameter(cmdGetCmotCode, "@schemePlanCode", DbType.Int32, schemeplanCode);
                db.AddOutParameter(cmdGetCmotCode, "@CMOTCode", DbType.String, 20);
                ds = db.ExecuteDataSet(cmdGetCmotCode);
                if (db.ExecuteNonQuery(cmdGetCmotCode) != 0)
                {
                    cmotCode = db.GetParameterValue(cmdGetCmotCode, "cmotCode").ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return cmotCode;
        }
    
        public List<OnlineMFSchemeDetailsVo> GetCompareMFSchemeDetails(string schemeCompareList)
        {
            List<OnlineMFSchemeDetailsVo> onlineMFSchemeDetailsList = new List<OnlineMFSchemeDetailsVo>();
            Database db;
            DataSet GetSchemeDetailsDs;
            DbCommand GetSchemeDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_GetMFSchemeCompareDetails");
                db.AddInParameter(GetSchemeDetailsCmd, "@schemePlanCode", DbType.String, schemeCompareList);
                GetSchemeDetailsDs = db.ExecuteDataSet(GetSchemeDetailsCmd);
                if (GetSchemeDetailsDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in GetSchemeDetailsDs.Tables[0].Rows)
                    {
                        OnlineMFSchemeDetailsVo.amcName = dr["PA_AMCName"].ToString();
                        OnlineMFSchemeDetailsVo.schemeName = dr["PASP_SchemePlanName"].ToString();
                        OnlineMFSchemeDetailsVo.fundManager = dr["PMFRD_FundManagerName"].ToString();
                        OnlineMFSchemeDetailsVo.schemeBanchMark = dr["PMFRD_BenchmarksIndexName"].ToString();
                        OnlineMFSchemeDetailsVo.category = dr["PAIC_AssetInstrumentCategoryName"].ToString();
                        OnlineMFSchemeDetailsVo.exitLoad = int.Parse(dr["PASPD_ExitLoadPercentage"].ToString());
                        if (dr["PMFRD_FixedIncStyleBoxLong"].ToString() != "")
                            OnlineMFSchemeDetailsVo.schemeBox = int.Parse(dr["PMFRD_FixedIncStyleBoxLong"].ToString());
                        OnlineMFSchemeDetailsVo.SchemeReturn3Year = dr["SchemeReturn3Year"].ToString();
                        OnlineMFSchemeDetailsVo.SchemeReturn5Year = dr["SchemeReturn5Year"].ToString();
                        OnlineMFSchemeDetailsVo.SchemeReturn10Year = dr["SchemeReturn10Year"].ToString();
                        OnlineMFSchemeDetailsVo.benchmarkReturn1stYear = dr["PMFRD_Return1Year_BM"].ToString();
                        OnlineMFSchemeDetailsVo.benchmark3rhYear = dr["PMFRD_Return3Year_BM"].ToString();
                        OnlineMFSchemeDetailsVo.benchmark5thdYear = dr["PMFRD_Return5Year_BM"].ToString();
                        if (dr["SchemeRisk3Year"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk3Year = dr["SchemeRisk3Year"].ToString();
                        if (dr["SchemeRisk5Year"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk5Year = dr["SchemeRisk5Year"].ToString();
                        if (dr["SchemeRisk10Year"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["SchemeRisk10Year"].ToString();
                        if (dr["PASPD_IsPurchaseAvailable"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["PASPD_IsPurchaseAvailable"].ToString();
                        if (dr["PASPD_IsRedeemAvailable"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["PASPD_IsRedeemAvailable"].ToString();
                        if (dr["PASPD_IsSIPAvailable"].ToString() != "")
                            OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["PASPD_IsSIPAvailable"].ToString();
                        OnlineMFSchemeDetailsVo.minmumInvestmentAmount = int.Parse(dr["PASPD_InitialPurchaseAmount"].ToString());
                        OnlineMFSchemeDetailsVo.multipleOf = int.Parse(dr["PASPD_InitialMultipleAmount"].ToString());
                        OnlineMFSchemeDetailsVo.minSIPInvestment = int.Parse(dr["PASPSD_MinAmount"].ToString());
                        OnlineMFSchemeDetailsVo.SIPmultipleOf = int.Parse(dr["PASPSD_MultipleAmount"].ToString());
                        onlineMFSchemeDetailsList.Add(OnlineMFSchemeDetailsVo);

                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return onlineMFSchemeDetailsList;
        }
        public DataSet GetSIPCustomeSchemePlan(int customerId, int AMCCode)
        {
            DataSet dsGetSIPCustomeSchemePlan;
            Database db;
            DbCommand GetSIPCustomeSchemePlancmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSIPCustomeSchemePlancmd = db.GetStoredProcCommand("SPROC_Onl_CustomerOrderSchemePlan");
                db.AddInParameter(GetSIPCustomeSchemePlancmd, "@customerId", DbType.Int32, customerId);
                db.AddInParameter(GetSIPCustomeSchemePlancmd, "@AMCCode", DbType.Int32, AMCCode);
                dsGetSIPCustomeSchemePlan = db.ExecuteDataSet(GetSIPCustomeSchemePlancmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw (Ex);
            }
            return dsGetSIPCustomeSchemePlan;
        }
    }
}
