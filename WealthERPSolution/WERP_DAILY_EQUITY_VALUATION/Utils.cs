using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.Net.Mime;
using System.Net.Mail;
using System.Collections;
using System.IO;
using BoCommon;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace WERP_DAILY_EQUITY_VALUATION
{
    class Utils
    {
        public static DataSet ExecuteDataSet(string CommandText, SqlParameter[] Params)
        {
            Database D = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand DC = D.GetStoredProcCommand(CommandText);

            foreach (SqlParameter Param in Params)
            {
                D.AddInParameter(DC, Param.ParameterName, Param.DbType, Param.Value);
            }
            DataSet DS = D.ExecuteDataSet(DC);

            return DS;
        }

        public static void ExecuteNonQuery(string CommandText, SqlParameter[] Params)
        {
            Database D = DatabaseFactory.CreateDatabase("wealtherp");
            DbCommand DC = D.GetStoredProcCommand(CommandText);

            foreach (SqlParameter Param in Params)
            {
                D.AddInParameter(DC, Param.ParameterName, Param.DbType, Param.Value);
            }
            D.ExecuteNonQuery(DC);
        }
    }
}
