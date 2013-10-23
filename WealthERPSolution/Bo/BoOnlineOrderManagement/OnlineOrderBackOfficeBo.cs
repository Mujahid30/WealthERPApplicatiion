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
using DaoOnlineOrderManagement;
using System.Data.OleDb;
using System.Text.RegularExpressions;


namespace BoOnlineOrderManagement
{
    public class OnlineOrderBackOfficeBo
    {
        OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao;

        public DataSet GetExtractType()
        {
            DataSet dsExtractType;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsExtractType = OnlineOrderBackOfficeDao.GetExtractType();

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

        public DataSet GetExtractTypeDataForFileCreation(DateTime orderDate,int AdviserId,int extractType)
        {
            DataSet dsExtractType;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsExtractType = OnlineOrderBackOfficeDao.GetExtractTypeDataForFileCreation(orderDate, AdviserId, extractType);
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

        private string GetDbType(string sDataType)
        {
            switch (sDataType)
            {
                case "bigint":
                    return "System.Int64";
                case "bit":
                    return "System.Boolean";
                case "char":
                    return "System.String";
                case "date":
                    return "System.DateTime";
                case "datetime":
                    return "System.DateTime";
                case "decimal":
                    return "System.Decimal";
                case "float":
                    return "System.Decimal";
                case "int":
                    return "System.Int32";
                case "nchar":
                    return "System.String";
                case "ntext":
                    return "System.String";
                case "numeric":
                    return "System.Decimal";
                case "nvarchar":
                    return "System.String";
                case "smalldatetime":
                    return "System.DateTime";
                case "smallint":
                    return "System.Int16";
                case "text":
                    return "System.String";
                case "time":
                    return "System.TimeSpan";
                case "tinyint":
                    return "System.Int16";
                case "uniqueidentifier":
                    return "System.Guid";
                case "varchar":
                    return "System.String";
                case "xml":
                    return "System.String";
                default:
                    return "System.String";
            }
        }
        public List<OnlineOrderBackOfficeVo> GetRtaColumnDetails(string RtaIdentifier)
        {

            List<OnlineOrderBackOfficeVo> lsHeaderMapping = new List<OnlineOrderBackOfficeVo>();
            try
            {
                OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
                DataSet dsHeaderMapping = daoOnlineOrderBackOffice.GetOrderExtractHeaderMapping(RtaIdentifier);

                if (dsHeaderMapping == null) return lsHeaderMapping;
                if (dsHeaderMapping.Tables.Count <= 0) return lsHeaderMapping;

                DataTable dtHeaderMapping = dsHeaderMapping.Tables[0];
                if (dtHeaderMapping.Rows.Count <= 0) return lsHeaderMapping;

                foreach (DataRow row in dtHeaderMapping.Rows)
                {
                    OnlineOrderBackOfficeVo headMap = new OnlineOrderBackOfficeVo();
                    headMap.HeaderName = row["WEEHM_HeaderName"].ToString().Trim();
                    headMap.HeaderSequence = int.Parse(row["WEEHM_HeaderSequence"].ToString().Trim());
                    headMap.WerpColumnName = row["AMFE_ColumnName"].ToString().Trim();
                    headMap.DataType = GetDbType(row["DATA_TYPE"].ToString());
                    //headMap.MaxLength = int.Parse(row["CHARACTER_MAXIMUM_LENGTH"].ToString().Trim());
                    headMap.IsNullable = row["IS_NULLABLE"].ToString() == "YES" ? true : false;
                    lsHeaderMapping.Add(headMap);
                }
                if (lsHeaderMapping.Count > 1) { lsHeaderMapping = lsHeaderMapping.OrderBy(o => o.HeaderSequence).ToList(); }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:GetOrderExtractForRta(string RtaIdentifier, DateTime ExecutionDate)");
                object[] objects = new object[1];
                objects[0] = RtaIdentifier;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return lsHeaderMapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExecutionDate"></param>
        /// <param name="AdviserId"></param>
        /// <param name="OrderType"></param>
        /// <param name="RtaIdentifier"></param>
        /// <returns></returns>
        public DataSet GetMfOrderExtract(DateTime ExecutionDate, int AdviserId, string OrderType, string RtaIdentifier, int AmcCode)
        {
            DataSet dsMfOrderExtract = null;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            OnlineMFOrderDao OnlineMFOrderDao = new OnlineMFOrderDao();
            try
            {
                dsMfOrderExtract = daoOnlineOrderBackOffice.GetMfOrderExtract(ExecutionDate, AdviserId, OrderType, RtaIdentifier, AmcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:GetMfOrderExtract()");

                object[] objects = new object[1];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dsMfOrderExtract;
        }

        /// <summary>
        /// Returns the data table for the order extract
        /// </summary>
        /// <param name="RtaIdentifier"></param>
        /// <param name="ExecutionDate"></param>
        /// <returns></returns>
        public DataTable GetOrderExtractForRta(DateTime ExecutionDate, int AdviserId, string OrderType, string RtaIdentifier, int AmcCode)
        {
            DataTable dtOrderExtract = new DataTable();
            try
            {
                List<OnlineOrderBackOfficeVo> headerMap = GetRtaColumnDetails(RtaIdentifier);
                DataSet dsOrderExtract = GetMfOrderExtract(ExecutionDate, AdviserId, OrderType, RtaIdentifier, AmcCode);
                dtOrderExtract = new DataTable("OrderExtract");
                foreach (OnlineOrderBackOfficeVo header in headerMap)
                {
                    dtOrderExtract.Columns.Add(header.HeaderName, System.Type.GetType(header.DataType));
                }
                foreach (DataRow row in dsOrderExtract.Tables[0].Rows)
                {
                    List<object> lsItems = new List<object>();
                    foreach (DataColumn dc in dtOrderExtract.Columns)
                    {
                        string werpColName = headerMap.Find(c => c.HeaderName == dc.ColumnName).WerpColumnName;
                        if (werpColName == "UNKNOWN") continue;
                        lsItems.Add(row[werpColName]);
                    }
                    dtOrderExtract.Rows.Add(lsItems.ToArray());
                }
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:GetOrderExtractForRta(string RtaIdentifier, DateTime ExecutionDate)");
                object[] objects = new object[2];
                objects[0] = RtaIdentifier;
                objects[1] = ExecutionDate;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtOrderExtract;
        }

        private string GetCsvColumnList(DataColumnCollection ColumnsCollection)
        {
            string colList = "";

            int cCols = ColumnsCollection.Count;
            foreach (DataColumn col in ColumnsCollection)
            {
                colList += col.ColumnName;
                --cCols;
                if (cCols > 0) colList += ",";
            }

            return colList;
        }

        public string CreatDbfFile(DataTable OrderExtract)
        {
            string dbfFile = OrderExtract.TableName.ToLower().Substring(0, 8) + ".DBF";

            try
            {
                string csvColList = GetCsvColumnList(OrderExtract.Columns);
                string workDir = @"c:\DBF\";

                string sqlIns = "INSERT INTO " + dbfFile + " (" + csvColList + ") VALUES (" + Regex.Replace(csvColList, @"[a-zA-Z_0-9]+", "?") + ")";
                string sqlSel = "SELECT " + csvColList + " FROM " + dbfFile;

                OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + workDir + ";Extended Properties=dBASE IV;");
                OleDbDataAdapter da = new OleDbDataAdapter(sqlSel, conn);

                OleDbCommand cmdIns = new OleDbCommand(sqlIns, conn);

                foreach (DataRow row in OrderExtract.Rows)
                {
                    foreach (DataColumn col in OrderExtract.Columns)
                    {
                        string colNam = col.ColumnName;
                        Object colval = row[col.ColumnName];
                        OleDbParameter param = cmdIns.Parameters.Add(new OleDbParameter(colNam, colval));
                    }
                }
                da.InsertCommand = cmdIns;
                conn.Open();

                da.Update(OrderExtract);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:CreatDbfFile(DataTable orderExtract)");
                object[] objects = new object[1];
                objects[0] = OrderExtract;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dbfFile;
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenerateOrderExtract()
        {
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            try
            {
                daoOnlineOrderBackOffice.GenerateOrderExtract();
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
        }
    }
}
