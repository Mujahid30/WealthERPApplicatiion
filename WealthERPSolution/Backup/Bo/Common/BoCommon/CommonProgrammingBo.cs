using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BoCommon
{
    public class CommonProgrammingBo 
    {
        string filename;
        string delimeter;

        public bool IsNumeric(string s)
        {
            double Result;
            return double.TryParse(s, out Result);
        }


        public string SetFileNameAndDelimeter(int FileID, DateTime dtExtractDate, string ProductIssueCOde)
        {
            string strExtractDate = Convert.ToDateTime(dtExtractDate).ToShortDateString();
            string[] strSplitExtractDate = strExtractDate.Split('/');

            string DD = strSplitExtractDate[0].ToString();
            string MM = strSplitExtractDate[1].ToString();
            string YYYY = strSplitExtractDate[2].ToString();

            if (FileID == 37)
            {
                filename = "sbiemf" + DD + MM + ".txt";
                delimeter = "#";
            }
            else if (FileID == 38)
            {
                filename = "sbipay" + DD + MM + ".txt";
                delimeter = "\t";
            }
            else if (FileID == 39)
            {
                filename = "HDFCPAY" + DD + MM + ".txt";
                delimeter = "\t";
            }
            else if (FileID == 40)
            {
                filename = "eMF-InProcess" + MM + DD + YYYY + ".txt";
                delimeter = "|";
            }
            else if (FileID == 41)
            {
                filename = "eMF-Executed" + MM + DD + YYYY + ".txt";
                delimeter = "|";
            }
            else if (FileID == 42)
            {
                filename = "SSL104" + DD + MM + ".txt";
                delimeter = ",";
            }
           //NCD PAYFILE FOR IIFCL BOND_09.12
            else if (FileID == 46)
            {
                filename = "NCD PAYFILE FOR" + ' ' + ProductIssueCOde+' '+ "BOND" + "_" + DD + '.' + MM + ".txt";
                delimeter = "\t";
            }
            else if (FileID == 47)
            {
                filename = "eNCD" + DD + MM + YYYY + '-' + ProductIssueCOde + ".txt";
                delimeter = "|";
            }
            else if (FileID == 48)
            {
                filename = "IPO PAYFILE FOR" + ' ' + ProductIssueCOde + ' ' + "BOND" + "_" + DD + '.' + MM + ".txt";
                delimeter = "\t";
            }
            else if (FileID == 49)
            {
                filename = "eNCD" + DD + MM + YYYY + '-' + ProductIssueCOde + ".txt";
                delimeter = "|";
            }
            return filename + "~" + delimeter;
        }


        public DataTable getHeaderNameNValue(DataTable dt,Dictionary<string, string> dHeaderText)
        {
            
            if (dHeaderText != null)
            {
                foreach (KeyValuePair<string, string> dHeader in dHeaderText)
                {
                    if (dt.Columns.Contains(dHeader.Key))
                    {
                        if (dt.Columns[dHeader.Key].ToString() == dHeader.Key)
                        {
                            dt.Columns[dHeader.Key].ColumnName = dHeader.Value;
                        }
                    }
                }
                dt.AcceptChanges();
            }
            return dt;

        }
    }
}
