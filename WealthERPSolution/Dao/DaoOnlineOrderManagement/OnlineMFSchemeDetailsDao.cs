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
  public  class OnlineMFSchemeDetailsDao
    {
       public OnlineMFSchemeDetailsVo GetSchemeDetails(int amcCode,int schemeCode,string category)
       {
           OnlineMFSchemeDetailsVo OnlineMFSchemeDetailsVo = new OnlineMFSchemeDetailsVo();
            Database db;
            DataSet GetSchemeDetailsDs;
            DbCommand GetSchemeDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemeDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_GetMFSchemeDetails");
                db.AddInParameter(GetSchemeDetailsCmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(GetSchemeDetailsCmd, "@schemePlanCode", DbType.Int32, schemeCode);
                if(category!="0")
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
                        if(dr["PMFRD_FixedIncStyleBoxLong"].ToString()!="")
                        OnlineMFSchemeDetailsVo.schemeBox = int.Parse(dr["PMFRD_FixedIncStyleBoxLong"].ToString());
                        OnlineMFSchemeDetailsVo.SchemeReturn3Year = dr["SchemeReturn3Year"].ToString();
                        OnlineMFSchemeDetailsVo.SchemeReturn5Year = dr["SchemeReturn5Year"].ToString();
                        OnlineMFSchemeDetailsVo.SchemeReturn10Year = dr["SchemeReturn10Year"].ToString();
                        OnlineMFSchemeDetailsVo.benchmarkReturn1stYear = dr["PMFRD_Return1Year_BM"].ToString();
                        OnlineMFSchemeDetailsVo.benchmark3rhYear = dr["PMFRD_Return3Year_BM"].ToString();
                        OnlineMFSchemeDetailsVo.benchmark5thdYear = dr["PMFRD_Return5Year_BM"].ToString();
                        if( dr["SchemeRisk3Year"].ToString()!="")
                        OnlineMFSchemeDetailsVo.SchemeRisk3Year = dr["SchemeRisk3Year"].ToString();
                        if (dr["SchemeRisk5Year"].ToString() != "")
                        OnlineMFSchemeDetailsVo.SchemeRisk5Year = dr["SchemeRisk5Year"].ToString();
                        if (dr["SchemeRisk10Year"].ToString() != "")
                        OnlineMFSchemeDetailsVo.SchemeRisk10Year = dr["SchemeRisk10Year"].ToString();

                    }
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return OnlineMFSchemeDetailsVo;
       }
    }
}
