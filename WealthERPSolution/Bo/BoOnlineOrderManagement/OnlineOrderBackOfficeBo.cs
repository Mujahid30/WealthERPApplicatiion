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
using System.IO;


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

        private OleDbType GetOleDbType(string sDataType)
        {
            switch (sDataType)
            {
                case "System.Int64":
                    return OleDbType.BigInt;
                case "System.Boolean":
                    return OleDbType.TinyInt;
                case "System.DateTime":
                    return OleDbType.Date;
                case "System.Decimal":
                    return OleDbType.Decimal;
                case "System.Int32":
                    return OleDbType.Integer;
                case "System.String":
                    return OleDbType.VarChar;
                case "System.Int16":
                    return OleDbType.SmallInt;
                case "System.TimeSpan":
                    return OleDbType.DBTime;
                default:
                    return OleDbType.VarChar;
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
                        if (werpColName == "UNKNOWN") { lsItems.Add(0); continue; }
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

        public string GetFileName(string ExtractType, string AmcName, int RowCount)
        {
            #region CustomFileName
            var random = new Random(System.DateTime.Now.Millisecond);
            int randomNumber = random.Next(0, 1000000);
            string rowCount = string.Empty;
            
            if (RowCount < 6) {
                int i = 0;

                rowCount = RowCount.ToString();
                int rowCountLength = rowCount.Length;
                while (i < (6 - rowCountLength)) {
                    rowCount += "0";
                    if (i < (6 - rowCountLength))
                        i++;
                }
            }
            string filename = "MF_ " + ExtractType + "_" + AmcName + rowCount + "0001" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + randomNumber;
            #endregion

            return filename;
        }

        public string CreatDbfFile(DataTable OrderExtract, string RnTType, string workDir)
        {
            string seedFileName = "";
            switch (RnTType)
            { 
                case "CA":
                    seedFileName = "cams";
                    break;
                case "KA":
                    seedFileName = "karvy";
                    break;
                case "TN":
                    seedFileName = "ft";
                    break;
                case "SU":
                    seedFileName = "sund";
                    break;
                default:
                    return null;
            }
            string dbfFile = "orderext.dbf";
            string csvColList = GetCsvColumnList(OrderExtract.Columns);
            //string workDir = Server.Mappath("~/ReferenceFiles/RTAExtractSampleFiles/");
            
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + workDir + ";Extended Properties=dBASE IV;");

            string sqlIns = "INSERT INTO " + dbfFile + " (" + csvColList + ") VALUES (" + Regex.Replace(csvColList, @"[a-zA-Z_0-9]+", "?") + ")";
            string sqlSel = "SELECT " + csvColList + " INTO " + dbfFile + " FROM " + seedFileName + ".dbf";
            string sqlDel = "DELETE FROM " + seedFileName + ".dbf" + " WHERE 1 = 1";

            OleDbDataAdapter daRead = new OleDbDataAdapter(sqlSel, conn);

            if (File.Exists(workDir + dbfFile)) File.Delete(workDir + dbfFile);

            daRead.AcceptChangesDuringUpdate = true;
            daRead.AcceptChangesDuringFill = true;

            OleDbCommand cmdSel = new OleDbCommand(sqlSel, conn);
            OleDbCommand cmdIns = new OleDbCommand(sqlIns, conn);
            OleDbCommand cmdDel = new OleDbCommand(sqlDel, conn);

            try
            {
                daRead.SelectCommand = cmdSel;
                daRead.DeleteCommand = cmdDel;
               
                conn.Open();
                DataTable dtEmpty = new DataTable();
                
                daRead.Fill(dtEmpty);
                sqlSel = "SELECT " + csvColList + " FROM " + seedFileName;

                daRead.SelectCommand = new OleDbCommand(sqlSel, conn);
                daRead.FillSchema(dtEmpty, SchemaType.Source);
                daRead.Update(dtEmpty);

                dtEmpty.BeginLoadData();
                dtEmpty.Merge(OrderExtract, true, MissingSchemaAction.Ignore);

                foreach (DataColumn col in dtEmpty.Columns) {
                    OleDbParameter param = cmdIns.Parameters.Add("@" + col.ColumnName, GetOleDbType(col.DataType.ToString()), col.MaxLength, col.ColumnName);
                }
                daRead.InsertCommand = cmdIns;
                
                daRead.Update(dtEmpty);

                conn.Close();
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
            finally {
                conn.Close();
            }
            return workDir + dbfFile;
        }

        /// <summary>
        /// 
        /// </summary>
        public int GenerateOrderExtract(int AmcCode, DateTime ExecutionDate, int AdviserId, string XES_SourceCode, string OrderType)
        {
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            int ordersCreated = 0;
            try
            {
                ordersCreated = daoOnlineOrderBackOffice.GenerateOrderExtract(AmcCode, ExecutionDate, AdviserId, XES_SourceCode, OrderType);
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
            return ordersCreated;
        }
    }
}
