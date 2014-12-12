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
        public DataTable GetAdviserIPOOrderBook(int adviserId,int issueId, string status, DateTime dtFrom, DateTime dtTo,int orderId)
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
                db.AddInParameter(cmd, "@AIMissue", DbType.Int32, issueId);
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, dtFrom);
                else
                    db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, DBNull.Value);
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(cmd, "@ToDate", DbType.DateTime, dtTo);
                else
                    db.AddInParameter(cmd, "@ToDate", DbType.DateTime, DBNull.Value);
                if(orderId !=0)
                    db.AddInParameter(cmd, "@OrderId", DbType.Int32, orderId);
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
        public DataTable GetIPOHoldings(int AdviserId, int AIMIssueId, int PageSize, int CurrentPage, string CustomerNamefilter, out int RowCount)
        {
            DataTable dtGetIPOHoldings;
            Database db;
            DataSet dsGetIPOHoldings;
            DbCommand GetIPOHoldingscmd;
            RowCount = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOHoldingscmd = db.GetStoredProcCommand("SPROC_GetAdviserIPOHoldings");
                db.AddInParameter(GetIPOHoldingscmd, "@AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetIPOHoldingscmd, "@AIMissue", DbType.Int32, AIMIssueId);
                db.AddInParameter(GetIPOHoldingscmd, "@CurrentPage", DbType.Int32, CurrentPage);
                db.AddInParameter(GetIPOHoldingscmd, "@CustomerNameFilter", DbType.String, CustomerNamefilter);
                db.AddInParameter(GetIPOHoldingscmd, "@PageSize", DbType.Int32, PageSize);
                db.AddOutParameter(GetIPOHoldingscmd, "@RowCount", DbType.Int32, 0);
                dsGetIPOHoldings = db.ExecuteDataSet(GetIPOHoldingscmd);
                dtGetIPOHoldings = dsGetIPOHoldings.Tables[0];
                if (db.ExecuteNonQuery(GetIPOHoldingscmd) != 0)
                {
                    if (db.GetParameterValue(GetIPOHoldingscmd, "RowCount").ToString() != "")
                    {
                        RowCount = Convert.ToInt32(db.GetParameterValue(GetIPOHoldingscmd, "RowCount").ToString());
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
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:Getproductcode()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetIPOHoldings;
        }
    }
}
