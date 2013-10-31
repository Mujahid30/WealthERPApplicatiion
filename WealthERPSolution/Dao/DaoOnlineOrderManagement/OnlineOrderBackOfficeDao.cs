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
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;


namespace DaoOnlineOrderManagement
{
    public class OnlineOrderBackOfficeDao
    {
        public DataSet GetExtractType()
        {
            DataSet dsExtractType;
            Database db;
            DbCommand GetGetMfOrderExtractCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetMfOrderExtractCmd = db.GetStoredProcCommand("SPROC_GetExtractType");

                dsExtractType = db.ExecuteDataSet(GetGetMfOrderExtractCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }

        public DataSet GetExtractTypeDataForFileCreation(DateTime orderDate, int AdviserId, int extractType)
        {
            DataSet dsExtractType;
            Database db;
            DbCommand GetGetMfOrderExtractCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetGetMfOrderExtractCmd = db.GetStoredProcCommand("SPROC_GetExtractTypeDataForFileCreation");
                if (orderDate != DateTime.MinValue)
                    db.AddInParameter(GetGetMfOrderExtractCmd, "@orderDate", DbType.DateTime, orderDate);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@adviserId", DbType.Int32, AdviserId);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@extractType", DbType.Int32, extractType);
                dsExtractType = db.ExecuteDataSet(GetGetMfOrderExtractCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OperationDao.cs:GetMfOrderExtract()");
                object[] objects = new object[10];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsExtractType;
        }

        public DataSet GetMfOrderExtract(DateTime ExecutionDate, int AdviserId, string TransactionType, string RtaIdentifier, int AmcCode)
        {
            DataSet dsGetMfOrderExtract;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetAdviserMfOrderExtract");
                db.AddInParameter(cmd, "@WTBD_ExecutionDate", DbType.DateTime, ExecutionDate);
                db.AddInParameter(cmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, RtaIdentifier);
                if (string.IsNullOrEmpty(TransactionType) == false && TransactionType.ToUpper() != "ALL") { db.AddInParameter(cmd, "@WMTT_TransactionClassificationCode", DbType.String, TransactionType); }
                if (AmcCode > 0) { db.AddInParameter(cmd, "@PA_AMCCode", DbType.Int32, AmcCode); }

                dsGetMfOrderExtract = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetMfOrderExtract(DateTime ExecutionDate, int AdviserId, string TransactionType, string RtaIdentifier)");
                object[] objects = new object[4];
                objects[0] = ExecutionDate;
                objects[1] = AdviserId;
                objects[2] = TransactionType;
                objects[3] = RtaIdentifier;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMfOrderExtract;
        }

        public DataSet GetOrderExtractHeaderMapping(string RtaIdentifier)
        {
            DataSet dsHeaderMapping;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_OrderExtractHeaderMapping");
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, RtaIdentifier);

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
                FunctionInfo.Add("Method", "OperationDao.cs:GetOrderExtractHeaderMapping(string RtaIdentifier)");
                object[] objects = new object[1];
                objects[0] = RtaIdentifier;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsHeaderMapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExecutionDate"></param>
        /// <param name="AdviserId"></param>
        /// <param name="XES_SourceCode"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public int GenerateOrderExtract(int AmcCode, DateTime ExecutionDate, int AdviserId, string XES_SourceCode, string OrderType)
        {
            Database db;
            DbCommand cmd;
            int rowsCreated = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_CreateAdviserMFOrderExtract");
                db.AddInParameter(cmd, "@PA_AMCCode", DbType.Int32, AmcCode);
                db.AddInParameter(cmd, "@WTBD_ExecutionDate", DbType.Date, ExecutionDate);
                db.AddInParameter(cmd, "@A_AdviserId", DbType.Int32, AdviserId);
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, XES_SourceCode);
                if (string.IsNullOrEmpty(OrderType) == false && OrderType.ToUpper() != "ALL") { db.AddInParameter(cmd, "@WMTT_TransactionClassificationCode", DbType.String, OrderType); }
                db.AddOutParameter(cmd, "@OrderExtractCreated", DbType.Int32, 0);
                db.ExecuteDataSet(cmd);
                string paramOut = db.GetParameterValue(cmd, "@OrderExtractCreated").ToString();
                if (string.IsNullOrEmpty(paramOut) != true)
                    rowsCreated = int.Parse(paramOut);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GenerateOrderExtract()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return rowsCreated;
        }

        public string GetstrAMCCodeRTName(string AmcCode)
        { 
            string strAMCCodeRTName;
            Database db;
            DataSet dsGetMFOrderDetails;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetstrAMCCodeRTName");
                db.AddInParameter(cmd, "@AmcCode", DbType.String, AmcCode);
             

                dsGetMFOrderDetails = db.ExecuteDataSet(cmd);
                strAMCCodeRTName = dsGetMFOrderDetails.Tables[0].Rows[0][0].ToString();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetMFOrderDetailsForRTAExtract(DateTime ExecutionDate, int AdviserId, string TransactionType, string RtaIdentifier)");
                object[] objects = new object[4];
                objects[0] = AmcCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return strAMCCodeRTName;

        }

        public DataSet GetMFOrderDetailsForRTAExtract(int adviserId, string transactionType, string rtaIdentifier, int amcCode,int userId)
        {
            DataSet dsGetMFOrderDetails;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_ONL_DailyAdviserRTAOrderExtract");
                //db.AddInParameter(cmd, "@WTBD_ExecutionDate", DbType.DateTime, ExecutionDate);
                db.AddInParameter(cmd, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, rtaIdentifier);
                if (string.IsNullOrEmpty(transactionType) == false && transactionType.ToUpper() != "ALL") { db.AddInParameter(cmd, "@WMTT_TransactionClassificationCode", DbType.String, transactionType); }
                if (amcCode > 0) { db.AddInParameter(cmd, "@PA_AMCCode", DbType.Int32, amcCode); }
                db.AddInParameter(cmd, "@U_UserId", DbType.Int32, userId);

                dsGetMFOrderDetails = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetMFOrderDetailsForRTAExtract(DateTime ExecutionDate, int AdviserId, string TransactionType, string RtaIdentifier)");
                object[] objects = new object[4];
                objects[0] = adviserId;
                objects[1] = transactionType;
                objects[2] = rtaIdentifier;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetMFOrderDetails;
        }

        public DataTable GetTableScheme(string tableName)
        {
            DataTable dtTableScheme = new DataTable();
            string conString;
            conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conString);
            string sql = @"select * from " + tableName.ToString() + " WHERE 1 = 2";

            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                dtTableScheme = reader.GetSchemaTable();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetTableScheme");
                object[] objects = new object[1];
                objects[0] = tableName;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            finally
            {
                sqlCon.Close();
            }

            return dtTableScheme;

        }

        public void CreateRTAEctractedOrderList(DataTable dtExtractedOrderList)
        {
            string conString;
            conString = ConfigurationManager.ConnectionStrings["wealtherp"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conString);

            try
            {
                SqlCommand cmd = new SqlCommand("SPROC_ONL_CreateRTAExtractOrderList", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlParameterMFNetPositionTable = new SqlParameter();
                sqlParameterMFNetPositionTable.ParameterName = "@AdviserRTAExtractedList";
                sqlParameterMFNetPositionTable.SqlDbType = SqlDbType.Structured;
                sqlParameterMFNetPositionTable.Value = dtExtractedOrderList;
                sqlParameterMFNetPositionTable.TypeName = "AdviserMFOrderExtract";
                cmd.Parameters.Add(sqlParameterMFNetPositionTable);

                sqlCon.Open();
                cmd.ExecuteNonQuery();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();


            }
            finally
            {
                sqlCon.Close();
            }
        }


    }
}
