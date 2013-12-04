using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace DaoOnlineOrderManagement
{
    public class OnlineIPOBackOfficeDao
    {
        public DataTable GetAdviserIPOOrderBook(int adviserId, string status, DateTime dtFrom, DateTime dtTo)
        {
            Database db;
            DataSet dsIPOOrder;
            DataTable dtIPOOrder;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_GetAdviserIPOIssueOrderBooks");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, adviserId);
                if (status != "0")
                    db.AddInParameter(cmd, "@Status", DbType.String, status);
                else
                    db.AddInParameter(cmd, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, dtFrom);
                db.AddInParameter(cmd, "@ToDate", DbType.DateTime, dtTo);
                dsIPOOrder = db.ExecuteDataSet(cmd);
                dtIPOOrder = dsIPOOrder.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineNCDBackOfficeDao.cs:GetAdviserIPOOrderBook()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrder;
        }
        public DataTable GetAdviserIPOOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataSet dsIPOOrderBook;
            DataTable dtIPOOrderBook;
            Database db;
            DbCommand GetIPOOrderBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOOrderBookcmd = db.GetStoredProcCommand("SPROC_ONL_GetAdviserIPOIssueOrderSubBooks");
                db.AddInParameter(GetIPOOrderBookcmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetIPOOrderBookcmd, "@IssuerId", DbType.Int32, IssuerId);
                db.AddInParameter(GetIPOOrderBookcmd, "@orderId", DbType.Int32, orderid);
                dsIPOOrderBook = db.ExecuteDataSet(GetIPOOrderBookcmd);
                dtIPOOrderBook = dsIPOOrderBook.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineBondOrderDao.cs:GetAdviserIPOOrderSubBook()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrderBook;
        }

    }
}
