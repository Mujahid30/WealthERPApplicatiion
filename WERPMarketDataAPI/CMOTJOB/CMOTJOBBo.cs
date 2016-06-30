using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace CMOTJOB
{
    class CMOTJOBBo
    {
        public DataSet GetDataFromAPI()
        {
            DataSet ds = new DataSet();
            CMOTJOBDao CMOTDao = new CMOTJOBDao();
            ds = CMOTDao.GetDataFromAPI();
            return ds;
        }
        public DataTable CombineGetSchemeDetails(DataSet ds)
        
        {
            DataTable dtSchemeDetails = new DataTable();
            dtSchemeDetails.Columns.Add("mf_schcode", typeof(string));
            dtSchemeDetails.Columns.Add("sch_name", typeof(string));
            dtSchemeDetails.Columns.Add("AMCName", typeof(string));
            dtSchemeDetails.Columns.Add("category", typeof(string));
            dtSchemeDetails.Columns.Add("FundManager", typeof(string));
            dtSchemeDetails.Columns.Add("Ret_1Yr", typeof(string));
            dtSchemeDetails.Columns.Add("Ret_3Yr", typeof(string));
            dtSchemeDetails.Columns.Add("Ret_5Yr", typeof(string));
            dtSchemeDetails.Columns.Add("bmindex", typeof(string));
            dtSchemeDetails.Columns.Add("Ret_1Yr_BM", typeof(string));
            dtSchemeDetails.Columns.Add("Ret_3Yr_BM", typeof(string));
            dtSchemeDetails.Columns.Add("Ret_5Yr_BM", typeof(string));
            dtSchemeDetails.Columns.Add("navdate", typeof(string));
            dtSchemeDetails.Columns.Add("NAV", typeof(string));
            dtSchemeDetails.Columns.Add("IntAmt", typeof(string));
            dtSchemeDetails.Columns.Add("IncAmt", typeof(string));
            dtSchemeDetails.Columns.Add("IntAmt_SIP", typeof(string));
            dtSchemeDetails.Columns.Add("IncAmt_SIP", typeof(string));
            dtSchemeDetails.Columns.Add("exitload", typeof(string));
            dtSchemeDetails.Columns.Add("fundcode", typeof(string));
            dtSchemeDetails.Columns.Add("Qualification", typeof(string));
            dtSchemeDetails.Columns.Add("Designation", typeof(string));
            dtSchemeDetails.Columns.Add("experience", typeof(string));
            
            var query =
    from tbl1 in ds.Tables[0].AsEnumerable()
    join tbl2 in ds.Tables[1].AsEnumerable() on tbl1["mf_schcode"] equals tbl2["mf_schcode"]
    select new
    {
        mf_schcode = (string)tbl1["mf_schcode"],
        sch_name = (string)tbl1["sch_name"],
        AMCName = (string)tbl1["AMCName"],
        category = (string)tbl1["category"],
        FundManager = (string)tbl1["FundManager"],
        Ret_1Yr = (string)tbl1["Ret_1Yr"],
        Ret_3Yr = (string)tbl1["Ret_3Yr"],
        Ret_5Yr = (string)tbl1["Ret_5Yr"],
        bmindex = (string)tbl1["bmindex"],
        Ret_1Yr_BM = (string)tbl1["Ret_1Yr_BM"],
        Ret_3Yr_BM = (string)tbl1["Ret_3Yr_BM"],
        Ret_5Yr_BM = (string)tbl1["Ret_5Yr_BM"],
        navdate = (string)tbl1["navdate"],
        NAV = (string)tbl1["NAV"],
        IntAmt = (string)tbl1["IntAmt"],
        IncAmt = (string)tbl1["IncAmt"],
        IntAmt_SIP = (string)tbl1["IntAmt_SIP"],
        IncAmt_SIP = (string)tbl1["IncAmt_SIP"],
        exitload = (string)tbl1["exitload"],
        fundcode = (string)tbl2["fundcode"],
        Qualification = (string)tbl2["Qualification"],
        Designation = (string)tbl2["Designation"],
        experience = (string)tbl2["experience"],

    };
            foreach (var item in query)
            {
                DataRow dr = dtSchemeDetails.NewRow();
                dr["mf_schcode"] = item.mf_schcode;
                dr["sch_name"] = item.sch_name;
                dr["AMCName"] = item.AMCName;
                dr["category"] = item.category;
                dr["FundManager"] = item.FundManager;
                dr["Ret_1Yr"] = item.Ret_1Yr;
                dr["Ret_3Yr"] = item.Ret_3Yr;
                dr["Ret_5Yr"] = item.Ret_5Yr;
                dr["bmindex"] = item.bmindex;
                dr["Ret_1Yr_BM"] = item.Ret_1Yr_BM;
                dr["Ret_3Yr_BM"] = item.Ret_3Yr_BM;
                dr["Ret_5Yr_BM"] = item.Ret_5Yr_BM;
                dr["navdate"] = item.navdate;
                dr["NAV"] = item.NAV;
                dr["IntAmt"] = item.IntAmt;
                dr["IncAmt"] = item.IncAmt;
                dr["IntAmt_SIP"] = item.IntAmt_SIP;
                dr["IncAmt_SIP"] = item.IncAmt_SIP;
                dr["exitload"] = item.exitload;
                dr["fundcode"] = item.fundcode;
                dr["Qualification"] = item.Qualification;
                dr["Designation"] = item.Designation;
                dr["experience"] = item.experience;

                dtSchemeDetails.Rows.Add(dr);

            }
            return dtSchemeDetails;

        }


        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type IcolType = GetProperty.PropertyType;

                        if ((IcolType.IsGenericType) && (IcolType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            IcolType = IcolType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, IcolType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo p in columns)
                {
                    dr[p.Name] = p.GetValue(Record, null) == null ? DBNull.Value : p.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }


       
        public int UpdateMarketResearchData(DataTable dtSchemeDetails)
        {
            int result = 0;
            CMOTJOBDao CMOTDao = new CMOTJOBDao();
            result=CMOTDao.UpdateMarketResearchData(dtSchemeDetails);
            return result;

        }
        private void CreateSchemeDetailsDataTable()
        {
            DataTable dtSchemeDetails = new DataTable();
            dtSchemeDetails.Columns.Add("mf_schcode", typeof(string));
            dtSchemeDetails.Columns.Add("sch_name", typeof(string));
            dtSchemeDetails.Columns.Add("amficode", typeof(string));
            dtSchemeDetails.Columns.Add("amc_code", typeof(string));
            dtSchemeDetails.Columns.Add("amcname", typeof(string));
            dtSchemeDetails.Columns.Add("maincategory", typeof(string));
            dtSchemeDetails.Columns.Add("categorycode", typeof(string));
            dtSchemeDetails.Columns.Add("categoryname", typeof(string));
            dtSchemeDetails.Columns.Add("ret_1month", typeof(string));
            dtSchemeDetails.Columns.Add("ret_6month", typeof(string));
            dtSchemeDetails.Columns.Add("ret_1yr", typeof(string));
            dtSchemeDetails.Columns.Add("ret_3yr", typeof(string));
            dtSchemeDetails.Columns.Add("ret_5yr", typeof(string));
            dtSchemeDetails.Columns.Add("inception", typeof(string));
            dtSchemeDetails.Columns.Add("mininvestment", typeof(string));
            dtSchemeDetails.Columns.Add("incamt", typeof(string));
            dtSchemeDetails.Columns.Add("sipdates", typeof(string));
            dtSchemeDetails.Columns.Add("sipfrequencyavailable", typeof(string));
            dtSchemeDetails.Columns.Add("intamt_sip", typeof(string));
            dtSchemeDetails.Columns.Add("incamt_sip", typeof(string));
            dtSchemeDetails.Columns.Add("installment_min_sip", typeof(string));
            dtSchemeDetails.Columns.Add("exitload", typeof(string));
            dtSchemeDetails.Columns.Add("fund_mgr", typeof(string));
            dtSchemeDetails.Columns.Add("nfoopendate", typeof(string));
            dtSchemeDetails.Columns.Add("nfoclosedate", typeof(string));
            dtSchemeDetails.Columns.Add("maturitydate", typeof(string));
            dtSchemeDetails.Columns.Add("registrar", typeof(string));
            dtSchemeDetails.Columns.Add("isin", typeof(string));
            dtSchemeDetails.Columns.Add("schemetype", typeof(string));
            dtSchemeDetails.Columns.Add("schemeoption", typeof(string));
            dtSchemeDetails.Columns.Add("purchaseavailable", typeof(string));
            dtSchemeDetails.Columns.Add("redeemavailable", typeof(string));
        }

        public int updateSchemeDetails(DataTable dtSchemeDetails)
        {
            int result = 0;
            CMOTJOBDao CMOTDao = new CMOTJOBDao();
            result = CMOTDao.updateSchemeDetails(dtSchemeDetails);
            return result;

        }


    }
}
