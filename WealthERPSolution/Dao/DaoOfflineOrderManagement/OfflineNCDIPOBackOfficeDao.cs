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
using VoOnlineOrderManagemnet;
using System.Configuration;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;


namespace DaoOfflineOrderManagement
{
    public class OfflineNCDIPOBackOfficeDao
    {
        string allotmentDataTable;

        public DataTable GetOfflineCustomerNCDOrderBook(int adviserId, int issueNo, string status, DateTime dtFrom, DateTime dtTo, string userType, string agentCode, int orderNo,int authenticateType)
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DataSet dsNCDOrder;
            DataTable dtNCDOrder;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_OFF_GetAdviserNCDOrderBook");
                db.AddInParameter(cmd, "@AdviserId", DbType.Int32, adviserId);
                if (status != "0")
                    db.AddInParameter(cmd, "@Status", DbType.String, status);
                else
                    db.AddInParameter(cmd, "@Status", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@AIMissue", DbType.Int32, issueNo);
                if (dtFrom != DateTime.MinValue)
                    db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, dtFrom);
                else
                    db.AddInParameter(cmd, "@Fromdate", DbType.DateTime, DBNull.Value);
                if (dtTo != DateTime.MinValue)
                    db.AddInParameter(cmd, "@ToDate", DbType.DateTime, dtTo);
                else
                    db.AddInParameter(cmd, "@ToDate", DbType.DateTime, DBNull.Value);
                if (orderNo != 0)
                    db.AddInParameter(cmd, "@OrderId", DbType.Int32, orderNo);
                else
                    db.AddInParameter(cmd, "@OrderId", DbType.Int32, 0);
                if (userType != "0")
                    db.AddInParameter(cmd, "@UserType", DbType.String, userType);
                else
                    db.AddInParameter(cmd, "@UserType", DbType.String, DBNull.Value);
                if (agentCode != "0")
                    db.AddInParameter(cmd, "@AgentCode", DbType.String, agentCode);
                else
                    db.AddInParameter(cmd, "@AgentCode", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@AuthenticateStatus ", DbType.Int32, authenticateType);
                dsNCDOrder = db.ExecuteDataSet(cmd);
                dtNCDOrder = dsNCDOrder.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineNCDBackOfficeDao.cs:GetAdviserNCDOrderBook()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrder;
        }
        public DataTable GetOfflineCustomerNCDOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataSet dsNCDOrderBook;
            DataTable dtNCDOrderBook;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetNCDOrderBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNCDOrderBookcmd = db.GetStoredProcCommand("SPROC_OFF_GetAdviserBondOrdersubBook");
                db.AddInParameter(GetNCDOrderBookcmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(GetNCDOrderBookcmd, "@IssuerId", DbType.Int32, IssuerId);
                db.AddInParameter(GetNCDOrderBookcmd, "@orderId", DbType.Int32, orderid);
                dsNCDOrderBook = db.ExecuteDataSet(GetNCDOrderBookcmd);
                dtNCDOrderBook = dsNCDOrderBook.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineBondOrderDao.cs:GetAdviserNCDOrderSubBook()");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtNCDOrderBook;
        }
        public DataSet GetNCDIssueOrderDetails(int orderId)
        {
            DataSet dsGetNCDIssueOrderDetails;
            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand GetNCDIssueOrderDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetNCDIssueOrderDetailscmd = db.GetStoredProcCommand("SPROC_GetNCDOrderDetails");
                db.AddInParameter(GetNCDIssueOrderDetailscmd, "@orderId", DbType.Int32, orderId);
                dsGetNCDIssueOrderDetails = db.ExecuteDataSet(GetNCDIssueOrderDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetNCDIssueOrderDetails;
        }
        public bool UpdateNCDDetails(int orderid, int userid, DataTable dtOrderDetails)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand UpdateNCDDetailscmd;
            bool bResult = false;
            DataSet dsUpdateUpdateNCDDetails = new DataSet();
            try
            {
                dsUpdateUpdateNCDDetails.Tables.Add(dtOrderDetails.Copy());
                dsUpdateUpdateNCDDetails.DataSetName = "dtOrderDetailseDS";
                dsUpdateUpdateNCDDetails.Tables[0].TableName = "dtOrderDetailsDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateNCDDetailscmd = db.GetStoredProcCommand("SPROC_UpdateNCDOrder");
                db.AddInParameter(UpdateNCDDetailscmd, "@xmlBondsOrder", DbType.Xml, dsUpdateUpdateNCDDetails.GetXml().ToString());
                db.AddInParameter(UpdateNCDDetailscmd, "@orderId", DbType.Int32, orderid);
                db.AddInParameter(UpdateNCDDetailscmd, "@UserId", DbType.Int32, userid);
                
                
                db.ExecuteNonQuery(UpdateNCDDetailscmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
    }
}
