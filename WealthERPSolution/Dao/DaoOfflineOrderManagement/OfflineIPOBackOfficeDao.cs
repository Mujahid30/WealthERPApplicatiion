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

namespace DaoOfflineOrderManagement
{
    public class OfflineIPOBackOfficeDao
    {
        public DataTable GetOfflineIPOOrderBook(int adviserId, int issueId, string status, DateTime dtFrom, DateTime dtTo, int orderId, string userType, string agentCode, string ModificationType, int userId)
        {
            Database db;
            DataSet dsIPOOrder;
            DataTable dtIPOOrder;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_OFF_GetAdviserIPOIssueOrderBooks");
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
                if (orderId != 0)
                    db.AddInParameter(cmd, "@OrderId", DbType.Int32, orderId);
                if (userType != "0")
                    db.AddInParameter(cmd, "@UserType", DbType.String, userType);
                else
                    db.AddInParameter(cmd, "@UserType", DbType.String, DBNull.Value);
                if (agentCode != "0")
                    db.AddInParameter(cmd, "@AgentCode", DbType.String, agentCode);
                else
                    db.AddInParameter(cmd, "@AgentCode", DbType.String, DBNull.Value);
                db.AddInParameter(cmd, "@ModificationType", DbType.String, ModificationType);
                db.AddInParameter(cmd, "@userId", DbType.Int32, userId);
                cmd.CommandTimeout = 60 * 60;
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
                FunctionInfo.Add("Method", "OfflineIPOBackOfficeDao.cs:GetOfflineIPOOrderBook()");
                object[] objects = new object[2];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrder;
        }
        public DataTable GetOfflineIPOOrderSubBook(int adviserId, int IssuerId, int orderid)
        {
            DataSet dsIPOOrderBook;
            DataTable dtIPOOrderBook;
            Database db;
            DbCommand GetIPOOrderBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOOrderBookcmd = db.GetStoredProcCommand("SPROC_OFF_GetAdviserIPOIssueOrderSubBooks");
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
                FunctionInfo.Add("Method", "OfflineIPOBackOfficeDao.cs:GetOfflineIPOOrderSubBook()");
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
                FunctionInfo.Add("Method", "OfflineOrderBackOfficeDao.cs:GetIPOHoldings()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtGetIPOHoldings;
        }
        public DataSet GetHeaderMapping(int mapType, string rtaType)
        {
            Database db;
            DataSet dsHeaderMapping;

            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetRTAFileTypeHeader");
                db.AddInParameter(cmd, "@MapType", DbType.Int32, mapType);
                if (rtaType != "0")
                    db.AddInParameter(cmd, "@RTAType", DbType.String, rtaType);
                else
                    db.AddInParameter(cmd, "@RTAType", DbType.String, DBNull.Value);

                dsHeaderMapping = db.ExecuteDataSet(cmd);


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OfflineIPOBackOfficeDao.cs:GetOfflineIPOOrderBook()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsHeaderMapping;
        }
        public int CreateUpdateExternalHeader(string externalHeader, int XMLHeaderId, string rtaType, string ecommand, string prevEHname, int externalHeaderId)
        {

            Microsoft.Practices.EnterpriseLibrary.Data.Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateExternalHeader");
                db.AddInParameter(createCmd, "@ExternalHeader", DbType.String, externalHeader);
                db.AddInParameter(createCmd, "@XMLHeaderId", DbType.Int32, XMLHeaderId);
                if (rtaType != "0")
                    db.AddInParameter(createCmd, "@RTAType", DbType.String, rtaType);
                else
                    db.AddInParameter(createCmd, "@RTAType", DbType.String, DBNull.Value);
                db.AddInParameter(createCmd, "@CommandType", DbType.String, ecommand);
                db.AddInParameter(createCmd, "@PrevExternalHeader", DbType.String, prevEHname);
                db.AddInParameter(createCmd, "@ExternalHeaderId", DbType.Int32, externalHeaderId);
                if (db.ExecuteNonQuery(createCmd) != 0)
                {
                    return XMLHeaderId;
                }
                else
                {
                    return 0;

                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public DataTable GetRTAHeaderType(int XMLHeaderFileId, int XMLHeaderId, string rtaType)
        {
            DataSet dsIPOOrderBook;
            DataTable dtIPOOrderBook;
            Database db;
            DbCommand GetIPOOrderBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOOrderBookcmd = db.GetStoredProcCommand("SPROC_GetExternalHeader");
                db.AddInParameter(GetIPOOrderBookcmd, "@XMLHeaderFileId", DbType.Int32, XMLHeaderFileId);
                db.AddInParameter(GetIPOOrderBookcmd, "@XMLHeaderId", DbType.Int32, XMLHeaderId);
                if (rtaType != "0")
                    db.AddInParameter(GetIPOOrderBookcmd, "@RTAType", DbType.String, rtaType);
                else
                    db.AddInParameter(GetIPOOrderBookcmd, "@RTAType", DbType.String, DBNull.Value);
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
                FunctionInfo.Add("Method", "OfflineIPOBackOfficeDao.cs:GetOfflineIPOOrderSubBook()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrderBook;
        }
        public DataTable GetRTASubHeaderType(int XMLHeaderId, string rtaType)
        {
            DataSet dsIPOOrderBook;
            DataTable dtIPOOrderBook;
            Database db;
            DbCommand GetIPOOrderBookcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetIPOOrderBookcmd = db.GetStoredProcCommand("SPROC_GetAllExternalHeader");
                //db.AddInParameter(GetIPOOrderBookcmd, "@XMLHeaderFileId", DbType.Int32, XMLHeaderFileId);
                db.AddInParameter(GetIPOOrderBookcmd, "@XMLHeaderId", DbType.Int32, XMLHeaderId);
                if (rtaType != "0")
                    db.AddInParameter(GetIPOOrderBookcmd, "@RTAType", DbType.String, rtaType);
                else
                    db.AddInParameter(GetIPOOrderBookcmd, "@RTAType", DbType.String, DBNull.Value);
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
                FunctionInfo.Add("Method", "OfflineIPOBackOfficeDao.cs:GetOfflineIPOOrderSubBook()");
                object[] objects = new object[1];
                //objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtIPOOrderBook;
        }
    }
}
