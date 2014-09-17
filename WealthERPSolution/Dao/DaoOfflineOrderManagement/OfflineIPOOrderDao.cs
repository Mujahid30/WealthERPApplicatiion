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
using System.IO;

namespace DaoOfflineOrderManagement
{
    public class OfflineIPOOrderDao
    {
        public int CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList, OnlineIPOOrderVo onlineIPOOrderVo)
        {
            int orderId = 0;
            Database db;
            DbCommand CreateIPOBidOrderCmd;
            DataSet dsIssueBidList = new DataSet();

            try
            {
                dsIssueBidList.Tables.Add(dtIPOBidList.Copy());
                dsIssueBidList.DataSetName = "IssueBidsDS";
                dsIssueBidList.Tables[0].TableName = "IssueBidsDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateIPOBidOrderCmd = db.GetStoredProcCommand("SPROC_OFF_CreateIPOBidOrderDetails");
                db.AddInParameter(CreateIPOBidOrderCmd, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@C_CustomerId", DbType.Int32, onlineIPOOrderVo.CustomerId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@OrderDate", DbType.Date, onlineIPOOrderVo.OrderDate);
                db.AddInParameter(CreateIPOBidOrderCmd, "@AIM_IssueId", DbType.Int32, onlineIPOOrderVo.IssueId);
                db.AddInParameter(CreateIPOBidOrderCmd, "@ApplicationNo", DbType.Int32, onlineIPOOrderVo.ApplicationNumber);
                db.AddInParameter(CreateIPOBidOrderCmd, "@PAG_AssetGroupCode", DbType.String, onlineIPOOrderVo.AssetGroup);
                db.AddInParameter(CreateIPOBidOrderCmd, "@CO_IsDeclarationAccepted", DbType.Int16, onlineIPOOrderVo.IsDeclarationAccepted ? 1 : 0);

                db.AddInParameter(CreateIPOBidOrderCmd, "@XMLIPOBids", DbType.Xml, dsIssueBidList.GetXml().ToString());
                db.AddOutParameter(CreateIPOBidOrderCmd, "@OrderId", DbType.Int32, 1000000);
                db.AddOutParameter(CreateIPOBidOrderCmd, "@ApplicationNo", DbType.String, 1000000);
                db.AddOutParameter(CreateIPOBidOrderCmd, "@AplicationNoStatus", DbType.String, 1000000);


                if (db.ExecuteNonQuery(CreateIPOBidOrderCmd) != 0)
                {
                    orderId = Convert.ToInt32(db.GetParameterValue(CreateIPOBidOrderCmd, "OrderId").ToString());

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
                FunctionInfo.Add("Method", "DaoOnlineOrderManagement.cs:CreateIPOBidOrderDetails(int adviserId, int userId, DataTable dtIPOBidList)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return orderId;

        }
    }
}
