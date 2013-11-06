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

        public DataSet GetExtractTypeDataForFileCreation(DateTime orderDate, int AdviserId, int extractType)
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
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();

            string strAMCCodeRTName = daoOnlineOrderBackOffice.GetstrAMCCodeRTName(AmcName);

            int randomNumber = random.Next(0, 1000000);
            string rowCount = string.Empty;
            rowCount = RowCount.ToString();
            int totalLength = strAMCCodeRTName.Length;

            rowCount = rowCount.PadLeft((7 - totalLength), '0');

            string filename = strAMCCodeRTName + rowCount + "0001" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + randomNumber;
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

                foreach (DataColumn col in dtEmpty.Columns)
                {
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
            finally
            {
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
        public DataSet GetSubCategory(string CategoryCode)
        {
            DataSet dsSubCategory;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dsSubCategory = daoOnlineOrderBackOffice.GetSubCategory(CategoryCode);
            return dsSubCategory;
        }
        public DataSet GetSubSubCategory(string CategoryCode, string SubCategoryCode)
        {

            DataSet dsSubSubCategory;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dsSubSubCategory = daoOnlineOrderBackOffice.GetSubSubCategory(CategoryCode, SubCategoryCode);
            return dsSubSubCategory;
        }
        public List<int> CreateOnlineSchemeSetUp(OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo, int userId)
        {
            List<int> SchemePlancodes = new List<int>();
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();

            try
            {
                SchemePlancodes = OnlineOrderBackOfficeDao.CreateOnlineSchemeSetUp(OnlineOrderBackOfficeVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return SchemePlancodes;
        }

        public OnlineOrderBackOfficeVo GetOnlineSchemeSetUp(int SchemePlanCode)
        {
            OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo = new OnlineOrderBackOfficeVo();
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                OnlineOrderBackOfficeVo = OnlineOrderBackOfficeDao.GetOnlineSchemeSetUp(SchemePlanCode);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "CustomerBankAccountBo.cs:GetOnlineSchemeSetUp()");
                object[] objects = new object[1];
                objects[0] = SchemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return OnlineOrderBackOfficeVo;
        }
        public DataSet GetSchemeSetUpFromOverAllCategoryList(int amcCode, string categoryCode)
        {
            DataSet dsSchemeSetUpFromOverAllCategoryList = new DataSet();
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsSchemeSetUpFromOverAllCategoryList = OnlineOrderBackOfficeDao.GetSchemeSetUpFromOverAllCategoryList(amcCode, categoryCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSchemeSetUpFromOverAllCategoryList;
        }
        public bool AMFIduplicateCheck(int schemeplancode, string externalcode)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            bool bResult = false;
            try
            {
                bResult = OnlineOrderBackOfficeDao.AMFIduplicateCheck(schemeplancode, externalcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }



        public bool ExtractDailyRTAOrderList(int adviserId, string transactionType, string rtaIdentifier, int amcCode, int userId)
        {
            DataSet dsMfOrderExtract = null;
            OnlineOrderBackOfficeDao onlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            DataTable dtFinalRTAOrderList = new DataTable();
            DataTable dtRTAExtractTableScheme = new DataTable();
            bool statusFlag = false;
            try
            {
                dsMfOrderExtract = onlineOrderBackOfficeDao.GetMFOrderDetailsForRTAExtract(adviserId, transactionType, rtaIdentifier, amcCode, userId);
                dtFinalRTAOrderList = onlineOrderBackOfficeDao.GetTableScheme("dbo.AdviserMFOrderExtract");
                dtFinalRTAOrderList = PrepareFinalRTAOrderExtract(dsMfOrderExtract, ref dtRTAExtractTableScheme);
                onlineOrderBackOfficeDao.CreateRTAEctractedOrderList(dtFinalRTAOrderList);
                statusFlag = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:ExtractDailyRTAOrderList()");

                object[] objects = new object[4];
                objects[0] = adviserId;
                objects[1] = transactionType;
                objects[2] = rtaIdentifier;
                objects[3] = amcCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return statusFlag;

        }

        private DataTable PrepareFinalRTAOrderExtract(DataSet dsMfOrderExtract, ref DataTable dtFinalRTAOrderList)
        {
            try
            {

                DataTable dtRTAOrderExtract = CreateAdviserOrderExtractTable();
                DataTable dtOrderCustomerProfileDetails = dsMfOrderExtract.Tables[0];
                DataTable dtCustomerJointNomineeDetails = dsMfOrderExtract.Tables[1];
                DataTable dtCustomerBankDeatils = dsMfOrderExtract.Tables[2];

                //DataTable dtFinalOrderCustomerProfileDetails = dtFinalRTAOrderList.Copy();
                //DataTable dtFinalCustomerJointNomineeDetails = dtFinalRTAOrderList.Copy();
                //DataTable dtFinalCustomerBankDeatils = dtFinalRTAOrderList.Copy();

                DataRow drFinalRTAExtract;

                foreach (DataRow drProfileOrder in dtOrderCustomerProfileDetails.Rows)
                {
                    drFinalRTAExtract = dtRTAOrderExtract.NewRow();
                    //string[] finaltableScheme = new string[dtFinalRTAOrderList.Columns.Count];
                    //finaltableScheme = dtFinalRTAOrderList.Columns;
                    //DataView dvJointHolder = new DataView(dtCustomerJointNomineeDetails, "C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString() + "AND CEDAA_AssociationType='JH'", "C_CustomerId", DataViewRowState.CurrentRows);
                    //DataView dvNominee = new DataView(dtCustomerJointNomineeDetails, "C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString() + "AND CEDAA_AssociationType='N'", "C_CustomerId", DataViewRowState.CurrentRows);
                    DataView dvCustomerBankDetails = new DataView(dtCustomerBankDeatils, "C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString(), "C_CustomerId", DataViewRowState.CurrentRows);
                    //DataRow[] drJointNominee = dtCustomerJointNomineeDetails.Select("C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString());
                    int joint = 1;
                    int nominee = 1;
                    int jointPan = 1;

                    foreach (DataColumn dcc in dtRTAOrderExtract.Columns)
                    {
                        var abccolumns = (dvCustomerBankDetails.ToTable()).Columns.Cast<DataColumn>().Where(c => c.ColumnName.StartsWith(dcc.ToString()));
                        if (drProfileOrder.Table.Columns.Contains(dcc.ToString()))
                        {
                            if (dcc.ToString() == "AMFE_TransactionTime")
                            {
                                drFinalRTAExtract[dcc.ToString()] = TimeSpan.Parse(drProfileOrder[dcc.ToString()].ToString());
                            }
                            else
                            {
                                drFinalRTAExtract[dcc.ToString()] = drProfileOrder[dcc.ToString()];
                            }
                        }
                        else if (!string.IsNullOrEmpty(abccolumns.ToString()))
                        {
                            foreach (DataRow drBank in dvCustomerBankDetails.ToTable().Rows)
                            {
                                if (drBank.Table.Columns.Contains(dcc.ToString()))
                                {
                                    drFinalRTAExtract[dcc.ToString()] = drBank[dcc.ToString()];
                                }

                            }
                        }
                        else
                        {

                            foreach (DataRow drJointNominee in dtCustomerJointNomineeDetails.Rows)
                            {
                                switch (dcc.ToString())
                                {
                                    case "AMFE_JointName1":
                                        if (drJointNominee["CEDAA_AssociationType"].ToString() == "JH" && joint == 1)
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeName"];
                                            joint = joint + 1;
                                        }
                                        break;
                                    case "AMFE_JointName2":
                                        if (joint == 2 && drJointNominee["CEDAA_AssociationType"].ToString() == "JH")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeName"];
                                            joint = joint + 1;
                                        }
                                        break;
                                    case "AMFE_JointHolderRelation":
                                        if (drJointNominee["CEDAA_AssociationType"].ToString() == "JH")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeRelation"];

                                        }
                                        break;
                                    case "AMFE_JointDateofBirth":
                                        if (drJointNominee["CEDAA_AssociationType"].ToString() == "JH")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeDateofBirth"];

                                        }
                                        break;
                                    case "AMFE_JointGaurdianName":
                                        if (drJointNominee["CEDAA_AssociationType"].ToString() == "JH")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeGaurdianName"];

                                        }
                                        break;
                                    case "AMFE_JointHolder1Pan":
                                        if (jointPan == 1 && drJointNominee["CEDAA_AssociationType"].ToString() == "JH")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeName"];
                                            jointPan = jointPan + 1;
                                        }
                                        break;
                                    case "AMFE_JointHolder2Pan":
                                        if (jointPan == 2 && drJointNominee["CEDAA_AssociationType"].ToString() == "JH")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeName"];
                                            jointPan = jointPan + 1;
                                        }
                                        break;







                                    case "AMFE_NomineeName1":
                                        if (nominee == 1 && drJointNominee["CEDAA_AssociationType"].ToString() == "N")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeName"];
                                            nominee = nominee + 1;
                                        }
                                        break;
                                    case "AMFE_NomineeName2":
                                        if (nominee == 2 && drJointNominee["CEDAA_AssociationType"].ToString() == "N")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeName"];
                                            nominee = nominee + 1;
                                        }
                                        break;
                                    case "AMFE_NomineeRelation":
                                        if (drJointNominee["CEDAA_AssociationType"].ToString() == "N")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeRelation"];

                                        }
                                        break;
                                    case "AMFE_NomineeDateofBirth":
                                        if (drJointNominee["CEDAA_AssociationType"].ToString() == "N")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeDateofBirth"];

                                        }
                                        break;
                                    case "AMFE_NomineeGaurdianName":
                                        if (drJointNominee["CEDAA_AssociationType"].ToString() == "N")
                                        {
                                            drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_JointNomineeGaurdianName"];

                                        }
                                        break;



                                    case "AMFE_Dp_Id":
                                        drFinalRTAExtract[dcc.ToString()] = drJointNominee["AMFE_Dp_Id"];
                                        break;

                                    default:
                                        drFinalRTAExtract[dcc.ToString()] = null;
                                        break;
                                }

                            }

                        }



                    }

                    dtRTAOrderExtract.Rows.Add(drFinalRTAExtract);

                }


                return dtRTAOrderExtract;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OrderBo.cs:ExtractDailyRTAOrderList()");

                object[] objects = new object[4];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        private void PrepareOrderProfileDataTable(DataTable dtOrderCustomerProfileDetails, ref DataTable dtFinalOrderCustomerProfileDetails)
        {


        }





        private DataTable CreateAdviserOrderExtractTable()
        {
            DataTable dtAdviserExtractedOrderList = new DataTable();
            //dtAdviserExtractedOrderList.Columns.Add("AMFE_Id", typeof(Int32));
            dtAdviserExtractedOrderList.Columns.Add("PA_AMCCode", typeof(Int32), null);
            dtAdviserExtractedOrderList.Columns.Add("CO_OrderId", typeof(Int32));
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AMCCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_BrokerCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_SubBrokerCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_UserCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_UserTrxnNo", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_ApplicationNumber", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_FolioNumber", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_CheckDigitNumber", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_TrxnType", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_SchemeCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_InvestorFirstName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JointName1", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JointName2", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AddressLine1", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AddressLine2", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AddressLine3", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_City", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Pincode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_PhoneOffice", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_TransactionDate", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_TransactionTime", typeof(TimeSpan), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Units", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Amount", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_CloseAC", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_DateofBirth", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_GuardianName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_PanNo", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_PhoneResidence", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_FaxOffice", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_FaxResidence", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_EmailID", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AccountNumber", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AccountType", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_BankName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_BranchName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_BankCity", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_ReinvestOption", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_HoldingNature", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_OccupationCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_TaxStatusCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Remarks", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_State", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_SubTrxnType", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_DivPayoutMechanism", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_ECSNumber", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_BankCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AltFolioNumber", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_AltBrokerCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_LocationCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_RedPayoutMechanism", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Pricing", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JointHolder1Pan", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JointHolder2Pan", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_NomineeName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_NomineeRelation", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_GuardianPAN", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_InstrmNo", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_UinNo", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_PANValid", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_GuardianPanValid", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JH1PanValid", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JH2PanValid", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_SIPRegisteredDate", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_FH_Min", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Jh1_Min", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Jh2_Min", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Guard_min", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Neft_Code", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Rtgs_Code", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Email_Acst", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Mobile_No", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Dp_Id", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Poa_Type", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_TrxnMode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Trxn_Sign_Confn", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Addl_Address1", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Addl_Address2", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Addl_Address3", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Addl_Address1_City", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Addl_Address1_State", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Addl_Address1_Country", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Addl_Address1_Pincode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Nom1_Applicable_Percentage", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Nom2_Name", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Nom2_Relationship", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("Nom2_Applicable_Percentage", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Nom3_Name", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Nom3_Relationship", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("Nom3_Applicable_Percentage", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Check_Flag", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Third_Party_Payment", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_KYC_Status", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_FIRCStatus", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_SIPRegistrationNumber", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_NoOfInstallments", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_SIPFrequency", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_StartDate", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_EndDate", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_InstallmentNumber", typeof(decimal), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_NomineeDateofBirth", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_NomineeMinorFlag", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_NomineeGaurdianName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_FirstHolderPanExempt", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JH1PanExempt", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_JH2PanExempt", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_GuardianPANExempt", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_FirstHolderExemptCategory", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Jh1ExemptCategory", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Jh2ExemptCategory", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_GaurdianExemptCategory", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Fh_KraExemptReferenceNo", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Jh1_KraExemptReferenceNo", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Jh2_KraExemptReferenceNo", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_GaurdianExemptReferenceNo", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_EuinOpted", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_Euin", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_NominationNotOpted", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_SubbrokerARNcode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_ExtractDateTime", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("C_CustCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("PASPD_BankName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("PASPD_AccountNumber", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("XES_SourceCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("A_AdviserId", typeof(Int32), null);
            dtAdviserExtractedOrderList.Columns.Add("PASP_SchemeName", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_BackOfficeExtractDateTime", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("WERPBM_BankCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("WERP_TransactionType", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("WMTT_TransactionClassificationCode", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_CreatedBy", typeof(Int32), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_CreatedOn", typeof(DateTime), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_DepositDate", typeof(DateTime), null);
            return dtAdviserExtractedOrderList;
        }
        public DataSet GetFrequency()
        {

            DataSet dsFrequency;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dsFrequency = daoOnlineOrderBackOffice.GetFrequency();
            return dsFrequency;
        }
        public DataSet GetLookupCategory()
        {
            DataSet dsLookupCategory;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsLookupCategory = OnlineOrderBackOfficeDao.GetLookupCategory();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBO.cs:GetLookupCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupCategory;
        }
        public DataSet GetWERPValues(int categoryID)
        {
            DataSet dsLookupCategory;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsLookupCategory = OnlineOrderBackOfficeDao.GetWERPValues(categoryID);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBO.cs:GetWERPValues()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsLookupCategory;
        }
        public DataSet GetRTA()
        {
            DataSet dsGetRTA;

            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                dsGetRTA = OnlineOrderBackOfficeDao.GetRTA();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:GetRTA()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetRTA;
        }
        public DataSet GetRtaWiseMapings(string sourceCode, int categoryID)
        {
            DataSet dsGetRtaWiseMapings;

            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                dsGetRtaWiseMapings = OnlineOrderBackOfficeDao.GetRtaWiseMapings(sourceCode, categoryID);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:GetRtaWiseMapings()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetRtaWiseMapings;
        }
        public bool CreateMapwithRTA(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo, int userID)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.CreateMapwithRTA(onlineOrderBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public bool CreateNewWerpName(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo, int userID)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.CreateNewWerpName(onlineOrderBackOfficeVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool RemoveMapingWIthRTA(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.RemoveMapingWIthRTA(onlineOrderBackOfficeVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool UpdateSchemeSetUpDetail(OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo, int SchemePlanCode)
        {
            bool blResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                blResult = OnlineOrderBackOfficeDao.UpdateSchemeSetUpDetail(OnlineOrderBackOfficeVo, SchemePlanCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineSchemeSetUp.cs:UpdateSchemeSetUpDetail()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

    }
}
