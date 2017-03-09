using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

using BoOnlineOrderManagement;

namespace OnlineOrderRMSAccountDebitProcess
{
    public class Program
    {

        static void Main(string[] args)
        {
            ProcesOnlineOrderRMSAccountDebit();

        }

        private static void ProcesOnlineOrderRMSAccountDebit()
        {
            OnlineOrderBo onlineOrderBo = new OnlineOrderBo();
            DataTable dtOrderList = new DataTable();

            dtOrderList = GetMFOnlineOrderListForRMSAccountDebit();
            foreach (DataRow dr in dtOrderList.Rows)
            {
                onlineOrderBo.DebitRMSUserAccountBalance(dr["C_CustCode"].ToString(), -Convert.ToDouble(dr["CMFOD_Amount"].ToString()), Convert.ToInt32(dr["CO_OrderId"].ToString()));

            }
        }

        public static DataTable GetMFOnlineOrderListForRMSAccountDebit()
        {
            Database db;
            DbCommand getMFOnlineOrderListCmd;
            DataSet dsMFOnlineOrderList;


            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                getMFOnlineOrderListCmd = db.GetStoredProcCommand("SPROC_ONL_GetOrderListForRMSAccountDebit");
                dsMFOnlineOrderList = db.ExecuteDataSet(getMFOnlineOrderListCmd);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderRMSAccountDebitProcess.cs:GetMFOnlineOrderListForRMSAccountDebit()");


                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

            return dsMFOnlineOrderList.Tables[0];

        }
    }
}
