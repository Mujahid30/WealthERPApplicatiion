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
using BoCommon;
using System.Configuration;

namespace BoOnlineOrderManagement
{
    public class OnlineOrderBackOfficeBo
    {
        OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao;
        CommonLookupBo boCommonLookup;
        DataTable dtAmcWithRta;

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

        public DataSet GetExtractTypeDataForFileCreation(DateTime orderDate, int AdviserId, int extractType, DateTime fromDate, DateTime toDate, string status)
        {
            DataSet dsExtractType;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsExtractType = OnlineOrderBackOfficeDao.GetExtractTypeDataForFileCreation(orderDate, AdviserId, extractType, fromDate, toDate, status);
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


        public List<RTAExtractHeadeInfoVo> GetRtaColumnDetails(string RtaIdentifier, bool isFatca)
        {
            List<RTAExtractHeadeInfoVo> lsHeaderMapping = new List<RTAExtractHeadeInfoVo>();
            try
            {
                OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
                DataSet dsHeaderMapping = daoOnlineOrderBackOffice.GetOrderExtractHeaderMapping(RtaIdentifier, isFatca);

                if (dsHeaderMapping == null) return lsHeaderMapping;
                if (dsHeaderMapping.Tables.Count <= 0) return lsHeaderMapping;

                DataTable dtHeaderMapping = dsHeaderMapping.Tables[0];
                if (dtHeaderMapping.Rows.Count <= 0) return lsHeaderMapping;

                foreach (DataRow row in dtHeaderMapping.Rows)
                {
                    RTAExtractHeadeInfoVo headMap = new RTAExtractHeadeInfoVo();
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

                List<RTAExtractHeadeInfoVo> headerMap = GetRtaColumnDetails((OrderType == "AMCBANK" || OrderType == "SIPBOOK") ? OrderType : RtaIdentifier, (OrderType == "FATCA_OTH" || OrderType == "FATCA_SIP") ? true : false);
                switch (OrderType)
                {
                    case "FATCA_OTH":
                        OrderType = "OTH";
                        break;
                    case "FATCA_SIP":
                        OrderType = "SIP";
                        break;
                }
                DataSet dsOrderExtract = GetMfOrderExtract(ExecutionDate, AdviserId, OrderType, RtaIdentifier, AmcCode);
                dtOrderExtract = new DataTable("OrderExtract");
                foreach (RTAExtractHeadeInfoVo header in headerMap)
                {
                    dtOrderExtract.Columns.Add(header.HeaderName, System.Type.GetType(header.DataType));
                }
                foreach (DataRow row in dsOrderExtract.Tables[0].Rows)
                {
                    List<object> lsItems = new List<object>();
                    foreach (DataColumn dc in dtOrderExtract.Columns)
                    {
                        string werpColName = headerMap.Find(c => c.HeaderName == dc.ColumnName).WerpColumnName;
                        if (werpColName == "UNKNOWN") { lsItems.Add(""); continue; }
                        lsItems.Add(row[werpColName]);
                    }
                    if (lsItems.Count > 0)
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

        public void CreateCalendar(int year)
        {
            OnlineOrderBackOfficeDao onlineorderbackofficedao = new OnlineOrderBackOfficeDao();

            try
            {
                onlineorderbackofficedao.CreateCalendar(year);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }


        private string GetCsvColumnList(DataColumnCollection ColumnsCollection)
        {
            StringBuilder colList = new StringBuilder();

            int cCols = ColumnsCollection.Count;
            foreach (DataColumn col in ColumnsCollection)
            {
                colList.Append(col.ColumnName);
                //colList += col.ColumnName;
                --cCols;
                if (cCols > 0) colList.Append(",");
            }

            return colList.ToString();
        }

        public string GetFileName(string ExtractType, string AmcName, int RowCount)
        {
            #region CustomFileName
            var random = new Random(System.DateTime.Now.Millisecond);
            string filename = string.Empty;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();

            string strAMCCodeRTName = daoOnlineOrderBackOffice.GetstrAMCCodeRTName(AmcName);

            int randomNumber = random.Next(0, 1000000);
            string rowCount = string.Empty;
            rowCount = RowCount.ToString();
            int totalLength = strAMCCodeRTName.Length;

            rowCount = rowCount.PadLeft((7 - totalLength), '0');

            if (ExtractType != "AMCBANK")
                filename = strAMCCodeRTName + rowCount + "0001" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + randomNumber;
            else
                filename = strAMCCodeRTName + "_" + "Bank" + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + randomNumber;
            #endregion

            return filename;
        }

        public string CreatDbfFile(DataTable OrderExtract, string RnTType, string workDir, string type, bool isFatca)
        {
            string seedFileName = (isFatca == true) ? RnTType + "_FATCA" : RnTType;


            switch (type)
            {
                case "AMCBANK":
                    seedFileName = "amcbank";
                    break;
                case "SIPBOOK":
                    seedFileName = "sipbook";
                    break;

            }


            string dbfFile = "ORDEREXT.DBF";
            string csvColList = GetCsvColumnList(OrderExtract.Columns);

            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + workDir + ";Extended Properties=dBASE IV;");

            string sqlIns = "INSERT INTO " + dbfFile + " (" + csvColList + ") VALUES (" + Regex.Replace(csvColList, @"[a-zA-Z_0-9]+", "?") + ")";
            string sqlSel = "SELECT " + csvColList + " INTO " + dbfFile + " FROM " + seedFileName + ".DBF";
            string sqlDel = "DELETE FROM " + seedFileName + ".DBF" + " WHERE 1 = 1";

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
        public List<int> CreateOnlineSchemeSetUp(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int userId)
        {
            List<int> SchemePlancodes = new List<int>();
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();

            try
            {
                SchemePlancodes = OnlineOrderBackOfficeDao.CreateOnlineSchemeSetUp(mfProductAMCSchemePlanDetailsVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return SchemePlancodes;
        }

        public MFProductAMCSchemePlanDetailsVo GetOnlineSchemeSetUp(int SchemePlanCode)
        {
            MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo = new MFProductAMCSchemePlanDetailsVo();
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                mfProductAMCSchemePlanDetailsVo = OnlineOrderBackOfficeDao.GetOnlineSchemeSetUp(SchemePlanCode);

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
            return mfProductAMCSchemePlanDetailsVo;
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
        public int ExternalcodeCheck(string externalcode)
        {
            int result = 0;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                result = OnlineOrderBackOfficeDao.ExternalcodeCheck(externalcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
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
                    DataView dvJointHolder = new DataView(dtCustomerJointNomineeDetails, "C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString() + "AND AMFE_UserTrxnNo=" + drProfileOrder["CO_OrderId"].ToString() + "AND CEDAA_AssociationType='" + "JH'", "C_CustomerId", DataViewRowState.CurrentRows);
                    DataView dvNominee = new DataView(dtCustomerJointNomineeDetails, "C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString() + "AND AMFE_UserTrxnNo=" + drProfileOrder["CO_OrderId"].ToString() + "AND CEDAA_AssociationType='" + "N'", "C_CustomerId", DataViewRowState.CurrentRows);
                    //DataView dvNominee = new DataView(dtCustomerJointNomineeDetails, "C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString() + "AND CEDAA_AssociationType='N'", "C_CustomerId", DataViewRowState.CurrentRows);
                    DataView dvCustomerBankDetails = new DataView(dtCustomerBankDeatils, "C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString() + "AND AMFE_UserTrxnNo=" + drProfileOrder["CO_OrderId"].ToString(), "C_CustomerId", DataViewRowState.CurrentRows);
                    //DataRow[] drJointNominee = dtCustomerJointNomineeDetails.Select("C_CustomerId=" + drProfileOrder["C_CustomerId"].ToString());

                    string AMFE_JointName1 = string.Empty, AMFE_JointName2 = string.Empty, AMFE_JointHolderRelation = string.Empty,
                           AMFE_JointDateofBirth = string.Empty, AMFE_JointGaurdianName = string.Empty, AMFE_JointHolder1Pan = string.Empty,
                           AMFE_JointHolder2Pan = string.Empty, AMFE_JH1PanValid = "N", AMFE_JH2PanValid = "N", AMFE_NomineeName = string.Empty,
                           AMFE_Nom2Name = string.Empty, AMFE_Nom2_Relationship = string.Empty, AMFE_NomineeRelation = string.Empty,
                           AMFE_NomineeDateofBirth = string.Empty, AMFE_NomineeGaurdianName = string.Empty, AMFE_NominationNotOpted = "Y", AMFE_Dp_Id = string.Empty;

                    #region   Old Joint Holder Logic
                    /*
                    if (dvJointHolder.ToTable().Rows.Count > 0)
                    {
                        int joint1 = 1;
                        foreach (DataRow drJoint in dvJointHolder.ToTable().Rows)
                        {
                            if (joint1 == 1)
                            {
                                AMFE_JointName1 = drJoint["AMFE_JointNomineeName"].ToString();
                                AMFE_JointHolderRelation = drJoint["AMFE_JointNomineeRelation"].ToString();
                                AMFE_JointDateofBirth = drJoint["AMFE_JointNomineeDateofBirth"].ToString();
                                AMFE_JointGaurdianName = drJoint["AMFE_JointNomineeGaurdianName"].ToString();
                                if (!string.IsNullOrEmpty(drJoint["AMFE_JointNomineePan"].ToString()))
                                {
                                    AMFE_JointHolder1Pan = drJoint["AMFE_JointNomineePan"].ToString();
                                    if (drJoint["AMFE_JointNomineeKYC"].ToString() == "1")
                                        AMFE_JH1PanValid = "Y";
                                }
                                AMFE_Dp_Id = drJoint["AMFE_Dp_Id"].ToString();

                                joint1 += 1;
                            }
                            else if (joint1 == 2)
                            {
                                AMFE_JointName2 = drJoint["AMFE_JointNomineeName"].ToString();
                                if (!string.IsNullOrEmpty(drJoint["AMFE_JointNomineePan"].ToString()))
                                {
                                    AMFE_JointHolder2Pan = drJoint["AMFE_JointNomineePan"].ToString();
                                    if (drJoint["AMFE_JointNomineeKYC"].ToString() == "1")
                                        AMFE_JH2PanValid = "Y";
                                }
                                if (string.IsNullOrEmpty(AMFE_Dp_Id))
                                    AMFE_Dp_Id = drJoint["AMFE_Dp_Id"].ToString();
                            }
                        }
                    }
                     
                   
                    
                    if (dvNominee.ToTable().Rows.Count > 0)
                    {
                        int nominee1 = 1;
                        foreach (DataRow drNominee in dvNominee.ToTable().Rows)
                        {
                            if (nominee1 == 1)
                            {
                                AMFE_NomineeName = drNominee["AMFE_JointNomineeName"].ToString();
                                AMFE_NomineeRelation = drNominee["AMFE_JointNomineeRelation"].ToString();
                                AMFE_NomineeDateofBirth = drNominee["AMFE_JointNomineeDateofBirth"].ToString();
                                AMFE_NomineeGaurdianName = drNominee["AMFE_JointNomineeGaurdianName"].ToString();
                                AMFE_NominationNotOpted = "Y";
                                nominee1 += 1;
                            }
                            else if (nominee1 == 2)
                            {
                                AMFE_Nom2Name = drNominee["AMFE_JointNomineeName"].ToString();
                                AMFE_Nom2_Relationship = drNominee["AMFE_JointNomineeRelation"].ToString();
                            }

                        }
                    }
                     * 
  */

                    #endregion

                    if (dvJointHolder.ToTable().Rows.Count > 0)
                    {
                        foreach (DataRow drJoint in dvJointHolder.ToTable().Rows)
                        {
                            switch (drJoint["CDAA_AssociateTypeNo"].ToString())
                            {
                                case "1":
                                    AMFE_JointName1 = drJoint["AMFE_JointNomineeName"].ToString();
                                    AMFE_JointHolderRelation = drJoint["AMFE_JointNomineeRelation"].ToString();
                                    AMFE_JointDateofBirth = drJoint["AMFE_JointNomineeDateofBirth"].ToString();
                                    AMFE_JointGaurdianName = drJoint["AMFE_JointNomineeGaurdianName"].ToString();
                                    if (!string.IsNullOrEmpty(drJoint["AMFE_JointNomineePan"].ToString()))
                                    {
                                        AMFE_JointHolder1Pan = drJoint["AMFE_JointNomineePan"].ToString();
                                        if (drJoint["AMFE_JointNomineeKYC"].ToString() == "1")
                                            AMFE_JH1PanValid = "Y";
                                    }
                                    AMFE_Dp_Id = drJoint["AMFE_Dp_Id"].ToString();
                                    break;
                                case "2":
                                    AMFE_JointName2 = drJoint["AMFE_JointNomineeName"].ToString();
                                    if (!string.IsNullOrEmpty(drJoint["AMFE_JointNomineePan"].ToString()))
                                    {
                                        AMFE_JointHolder2Pan = drJoint["AMFE_JointNomineePan"].ToString();
                                        if (drJoint["AMFE_JointNomineeKYC"].ToString() == "1")
                                            AMFE_JH2PanValid = "Y";
                                    }
                                    if (string.IsNullOrEmpty(AMFE_Dp_Id))
                                        AMFE_Dp_Id = drJoint["AMFE_Dp_Id"].ToString();
                                    break;
                            }

                        }

                    }

                    if (dvNominee.ToTable().Rows.Count > 0)
                    {
                        foreach (DataRow drNominee in dvNominee.ToTable().Rows)
                        {
                            switch (drNominee["CDAA_AssociateTypeNo"].ToString())
                            {
                                case "1":
                                    AMFE_NomineeName = drNominee["AMFE_JointNomineeName"].ToString();
                                    AMFE_NomineeRelation = drNominee["AMFE_JointNomineeRelation"].ToString();
                                    AMFE_NomineeDateofBirth = drNominee["AMFE_JointNomineeDateofBirth"].ToString();
                                    AMFE_NomineeGaurdianName = drNominee["AMFE_JointNomineeGaurdianName"].ToString();
                                    AMFE_NominationNotOpted = "N";
                                    break;
                                case "2":
                                    AMFE_Nom2Name = drNominee["AMFE_JointNomineeName"].ToString();
                                    AMFE_Nom2_Relationship = drNominee["AMFE_JointNomineeRelation"].ToString();
                                    break;

                            }

                        }
                    }


                    foreach (DataColumn dcc in dtRTAOrderExtract.Columns)
                    {
                        if (dcc.ToString() == "AMFE_JH1PanValid")
                        {

                        }

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
                        else if ((dvCustomerBankDetails.ToTable()).Columns.Contains(dcc.ToString()))
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
                            switch (dcc.ToString())
                            {
                                case "AMFE_JointName1":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JointName1;
                                    break;
                                case "AMFE_JointName2":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JointName2;
                                    break;
                                case "AMFE_JointHolderRelation":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JointHolderRelation;
                                    break;
                                case "AMFE_JointDateofBirth":
                                    if (!string.IsNullOrEmpty(AMFE_JointDateofBirth))
                                        drFinalRTAExtract[dcc.ToString()] = AMFE_JointDateofBirth;
                                    else
                                        drFinalRTAExtract[dcc.ToString()] = DBNull.Value;
                                    break;
                                case "AMFE_JointGaurdianName":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JointGaurdianName;
                                    break;
                                case "AMFE_JointHolder1Pan":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JointHolder1Pan;
                                    break;
                                case "AMFE_JointHolder2Pan":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JointHolder2Pan;
                                    break;
                                case "AMFE_JH1PanValid":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JH1PanValid;
                                    break;
                                case "AMFE_JH2PanValid":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_JH2PanValid;
                                    break;


                                case "AMFE_NomineeName":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_NomineeName;
                                    break;
                                case "AMFE_Nom2_Name":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_Nom2Name;
                                    break;
                                case "AMFE_Nom2_Relationship":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_Nom2_Relationship;
                                    break;
                                case "AMFE_NomineeRelation":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_NomineeRelation;
                                    break;
                                case "AMFE_NomineeDateofBirth":
                                    if (!string.IsNullOrEmpty(AMFE_NomineeDateofBirth))
                                        drFinalRTAExtract[dcc.ToString()] = AMFE_NomineeDateofBirth;
                                    else
                                        drFinalRTAExtract[dcc.ToString()] = DBNull.Value;

                                    break;
                                case "AMFE_NomineeGaurdianName":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_NomineeGaurdianName;
                                    break;
                                case "AMFE_NominationNotOpted":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_NominationNotOpted;
                                    break;
                                case "AMFE_Dp_Id":
                                    drFinalRTAExtract[dcc.ToString()] = AMFE_Dp_Id;
                                    break;
                                default:
                                    drFinalRTAExtract[dcc.ToString()] = DBNull.Value;
                                    break;
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
            dtAdviserExtractedOrderList.Columns.Add("AMFE_BankMandateProof", typeof(Char), null);

            dtAdviserExtractedOrderList.Columns.Add("AMFE_ExternalSourceId", typeof(Int32), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_LOG_WT", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_IsGuardianPANValid", typeof(string), null);
            dtAdviserExtractedOrderList.Columns.Add("AMFE_LOG", typeof(string), null);

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
        public DataSet GetRTALists()
        {
            DataSet dsGetRTAList;
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                dsGetRTAList = OnlineOrderBackOfficeDao.GetRTALists();

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetRTAList;
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
        public bool CreateMapwithRTA(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo, int userID)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.CreateMapwithRTA(werplookupCodeValueManagementVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }

        }
        public bool CreateNewWerpName(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo, int userID)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.CreateNewWerpName(werplookupCodeValueManagementVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool DeleteWerpName(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.DeleteWerpName(werplookupCodeValueManagementVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool UpdateWerpName(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo, int userID)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.UpdateWerpName(werplookupCodeValueManagementVo, userID);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool RemoveMapingWIthRTA(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo)
        {
            try
            {
                OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
                return OnlineOrderBackOfficeDao.RemoveMapingWIthRTA(werplookupCodeValueManagementVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public bool UpdateSchemeSetUpDetail(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int SchemePlanCode, int userid)
        {
            bool blResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                blResult = OnlineOrderBackOfficeDao.UpdateSchemeSetUpDetail(mfProductAMCSchemePlanDetailsVo, SchemePlanCode, userid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateSchemeSetUpDetail()");
                object[] objects = new object[3];
                objects[0] = mfProductAMCSchemePlanDetailsVo;
                objects[1] = SchemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        public DataTable OnlinebindRandT(int SchemPlaneCode)
        {
            DataTable dtRandT;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dtRandT = OnlineOrderBackOfficeDao.OnlinebindRandT(SchemPlaneCode);
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:OnlinebindRandT()");
                object[] objects = new object[1];
                objects[0] = SchemPlaneCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtRandT;
        }
        public DataSet GetSystematicDetails(int schemeplancode)
        {
            DataSet dsSystematicDetails;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dsSystematicDetails = daoOnlineOrderBackOffice.GetSystematicDetails(schemeplancode);
            return dsSystematicDetails;
        }
        public bool EditSystematicDetails(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int schemeplancode, int systematicdetailsid, int userId)
        {
            bool blResult = false;

            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                blResult = OnlineOrderBackOfficeDao.EditSystematicDetails(mfProductAMCSchemePlanDetailsVo, schemeplancode, systematicdetailsid, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:CreateSystematicDetails()");

                object[] objects = new object[2];
                objects[0] = mfProductAMCSchemePlanDetailsVo;
                objects[1] = schemeplancode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        public bool CreateSystematicDetails(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int schemeplancode, int userId)
        {
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            bool bResult = false;
            try
            {
                bResult = daoOnlineOrderBackOffice.CreateSystematicDetails(mfProductAMCSchemePlanDetailsVo, schemeplancode, userId);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:CreateSystematicDetails()");

                object[] objects = new object[2];
                objects[0] = mfProductAMCSchemePlanDetailsVo;
                objects[1] = schemeplancode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }


        private KeyValuePair<string, string>[] GetAMCList(string RtaCode)
        {
            List<KeyValuePair<string, string>> AmcList = new List<KeyValuePair<string, string>>();

            if (boCommonLookup == null) boCommonLookup = new CommonLookupBo();

            try
            {
                if (dtAmcWithRta == null) dtAmcWithRta = boCommonLookup.GetAmcWithRta();
                foreach (DataRow row in dtAmcWithRta.Rows)
                {
                    //if (row["RTA"].ToString().Equals(RtaCode) == false) continue;
                    string[] rtaList = row["RTA"].ToString().Split(',');

                    if (rtaList.Contains(RtaCode) == false) continue;
                    AmcList.Add(new KeyValuePair<string, string>(row["PA_AMCCode"].ToString(), row["PA_AMCName"].ToString()));
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
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetAMCList()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return AmcList.ToArray();
        }

        private KeyValuePair<string, string>[] GetRTAList()
        {
            List<KeyValuePair<string, string>> RTAList = new List<KeyValuePair<string, string>>();
            if (boCommonLookup == null) boCommonLookup = new CommonLookupBo();

            try
            {
                DataTable dtExtSrc = boCommonLookup.GetExternalSource(null);
                foreach (DataRow row in dtExtSrc.Rows)
                {
                    RTAList.Add(new KeyValuePair<string, string>(row["XES_SourceCode"].ToString(), row["XES_SourceName"].ToString()));
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
                FunctionInfo.Add("Method", "CommonLookupBo.cs:GetAMCList()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return RTAList.ToArray();
        }

        private KeyValuePair<string, string>[] GetOrderTypeList()
        {
            List<KeyValuePair<string, string>> OrderTypeList = new List<KeyValuePair<string, string>>();

            OrderTypeList.Add(new KeyValuePair<string, string>("OTH", "Normal"));
            OrderTypeList.Add(new KeyValuePair<string, string>("SIP", "SIP"));
            OrderTypeList.Add(new KeyValuePair<string, string>("NFO", "NFO"));
            OrderTypeList.Add(new KeyValuePair<string, string>("AMCBANK", "AMCBANK"));
            OrderTypeList.Add(new KeyValuePair<string, string>("SIPBOOK", "SIPBOOK"));
            //OrderTypeList.Add(new KeyValuePair<string, string>("FATCA_OTH", "FATCA_NORMAL"));
            //OrderTypeList.Add(new KeyValuePair<string, string>("FATCA_SIP", "FATCA_SIP"));
            return OrderTypeList.ToArray();

        }

        private void CreateTxtFile(DataTable dtOrderExtract, string filename, string rtaType, string filePath)
        {
            string dateFormat = "MM/dd/yyyy";


            switch (rtaType)
            {
                case "KA":
                case "CA":
                    dateFormat = "MM/dd/yyyy";
                    break;
                case "TN":
                    dateFormat = "dd-MM-yyyy";
                    break;
                case "SU":
                    dateFormat = "dd/MM/yyyy";
                    break;
            }
            string file = string.Empty;

            #region ExportDataTabletoFile
            StreamWriter str = new StreamWriter(filePath + filename + ".TXT", false, System.Text.Encoding.Default);

            if (rtaType != "CA")
            {
                string Columns = string.Empty;
                foreach (DataColumn column in dtOrderExtract.Columns)
                {
                    Columns += column.ColumnName + "|";
                }
                str.WriteLine(Columns.Remove(Columns.Length - 1, 1));
            }

            DataColumn[] arrCols = new DataColumn[dtOrderExtract.Columns.Count];
            dtOrderExtract.Columns.CopyTo(arrCols, 0);
            foreach (DataRow datarow in dtOrderExtract.Rows)
            {
                string row = string.Empty;
                int i = 0;
                foreach (object item in datarow.ItemArray)
                {
                    if (arrCols[i].DataType.FullName == "System.DateTime")
                    {
                        string strDate = string.IsNullOrEmpty(item.ToString()) ? "" : DateTime.Parse(item.ToString()).ToString(dateFormat);
                        row += strDate + "|";
                    }
                    else
                    {
                        row += item.ToString() + "|";
                    }
                    i++;
                }
                str.WriteLine(row.Remove(row.Length - 1, 1));
            }
            str.Flush();
            str.Close();
            #endregion
        }

        public void GenerateDailyOrderExtractFiles(string refFilePath, bool bOverwrite, int adviserId)
        {
            string extractPath = ConfigurationSettings.AppSettings["RTA_EXTRACT_PATH"];
            string dailyDirName = DateTime.Now.ToString("ddMMMyyyy");


            if (Directory.Exists(extractPath + @"\" + adviserId.ToString() + @"\" + dailyDirName) && bOverwrite == false) return;

            if (Directory.Exists(extractPath + @"\" + adviserId.ToString() + @"\" + dailyDirName) && bOverwrite == true) Directory.Delete(extractPath + @"\" + adviserId.ToString() + @"\" + dailyDirName, true);

            KeyValuePair<string, string>[] RtaList = GetRTAList();
            KeyValuePair<string, string>[] OrderTypeList = GetOrderTypeList();

            foreach (KeyValuePair<string, string> rta in RtaList)
            {
                KeyValuePair<string, string>[] AmcList = GetAMCList(rta.Key);

                foreach (KeyValuePair<string, string> amc in AmcList)
                {
                    foreach (KeyValuePair<string, string> OrderType in OrderTypeList)
                    {
                        if (rta.Key.Equals("CA") && OrderType.Key.Equals("FATCA_SIP"))
                        {

                        }
                        DataTable orderExtractForRta = GetOrderExtractForRta(DateTime.Now.Date, adviserId, OrderType.Key, rta.Key, int.Parse(amc.Key));


                        if (orderExtractForRta.Rows.Count <= 0) continue;

                        if (Directory.Exists(extractPath + @"\" + adviserId.ToString() + @"\" + dailyDirName + @"\" + rta.Value + @"\" + amc.Value + @"\" + OrderType.Value) == false)
                        {
                            Directory.CreateDirectory(extractPath + @"\" + adviserId.ToString() + @"\" + dailyDirName + @"\" + rta.Value + @"\" + amc.Value + @"\" + OrderType.Value);
                        }

                        string downloadFileName = GetFileName(OrderType.Key, amc.Key, orderExtractForRta.Rows.Count);
                        if (rta.Key.Equals("CA"))
                        {
                            CreateTxtFile(orderExtractForRta, downloadFileName, rta.Key, extractPath + @"\" + adviserId.ToString() + @"\" + dailyDirName + @"\" + rta.Value + @"\" + amc.Value + @"\" + OrderType.Value + @"\");

                        }

                        string localFilePath = CreatDbfFile(orderExtractForRta, rta.Key, refFilePath, OrderType.Key, (OrderType.Key == "FATCA_SIP" || OrderType.Key == "FATCA_OTH") ? true : false);
                        File.Copy(localFilePath, extractPath + @"\" + adviserId.ToString() + @"\" + dailyDirName + @"\" + rta.Value + @"\" + amc.Value + @"\" + OrderType.Value + @"\" + downloadFileName + ".DBF");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
        }


        public DataSet GetTradeBusinessDates()
        {
            DataSet dsGetTradeBusinessDate;
            OnlineOrderBackOfficeDao Onlineorderbackofficedao = new OnlineOrderBackOfficeDao();
            try
            {
                dsGetTradeBusinessDate = Onlineorderbackofficedao.GetTradeBusinessDates();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBO.cs:GetTradeBusinessDates()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetTradeBusinessDate;
        }

        public bool CreateTradeBusinessDate(TradeBusinessDateVo tradeBusinessDateVo)
        {

            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                return OnlineOrderBackOfficeDao.CreateTradeBusinessDate(tradeBusinessDateVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public DataTable GetAdviserClientKYCStatusList(int adviserId, string filterOn, string clientCode)
        {
            DataTable dtFinalAdviserClientKYCStatusList = new DataTable();
            DataSet dsAdviserClientKYCStatusList = new DataSet();
            OnlineOrderBackOfficeDao onlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dsAdviserClientKYCStatusList = onlineOrderBackOfficeDao.GetAdviserClientKYCStatusList(adviserId, filterOn, clientCode);
                dtFinalAdviserClientKYCStatusList = CreateFinalClientKYCDataTable(dsAdviserClientKYCStatusList);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:GetAdviserClientKYCStatusList()");


                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dtFinalAdviserClientKYCStatusList;
        }

        private DataTable CreateFinalClientKYCDataTable(DataSet dsClientKYCList)
        {
            DataTable dtFinalClientKYCList = new DataTable();
            Dictionary<string, string> kycStatus;
            dtFinalClientKYCList.Columns.Add("CustomerId");
            dtFinalClientKYCList.Columns.Add("ClientAccountCode");
            dtFinalClientKYCList.Columns.Add("DOB");
            dtFinalClientKYCList.Columns.Add("Name");
            dtFinalClientKYCList.Columns.Add("PAN");
            dtFinalClientKYCList.Columns.Add("KYCStatus");
            dtFinalClientKYCList.Columns.Add("Holding");
            dtFinalClientKYCList.Columns.Add("BeneficiaryAccountNum");
            dtFinalClientKYCList.Columns.Add("Nominee");
            dtFinalClientKYCList.Columns.Add("SecondHolder");
            dtFinalClientKYCList.Columns.Add("SecondHolderPAN");
            dtFinalClientKYCList.Columns.Add("SecondHolderKYC");
            dtFinalClientKYCList.Columns.Add("ThirdHolder");
            dtFinalClientKYCList.Columns.Add("ThirdHolderPAN");
            dtFinalClientKYCList.Columns.Add("SThirdHolderKYC");
            dtFinalClientKYCList.Columns.Add("Privilege");

            DataTable dtClientSelfDetails = dsClientKYCList.Tables[0];
            DataTable dtClientDematJointHolderDetails = dsClientKYCList.Tables[1];


            foreach (DataRow dr in dtClientSelfDetails.Rows)
            {
                kycStatus = new Dictionary<string, string>();
                //DataRow[] drClientDematJointList = dtClientDematJointHolderDetails.Select("C_CustomerId=" + dr["C_CustomerId"].ToString(), "CAS_AssociationId");
                DataRow[] drClientDematJointList = dtClientDematJointHolderDetails.Select("C_CustomerId=" + dr["C_CustomerId"].ToString());
                DataRow drFinalClientKYC = dtFinalClientKYCList.NewRow();
                drFinalClientKYC["CustomerId"] = dr["C_CustomerId"];
                drFinalClientKYC["ClientAccountCode"] = dr["C_CustCode"];
                drFinalClientKYC["Name"] = dr["ClientName"];
                drFinalClientKYC["PAN"] = dr["C_PANNum"];
                drFinalClientKYC["KYCStatus"] = dr["C_IsKYCAvailable"];
                drFinalClientKYC["DOB"] = dr["C_DOB"];
                drFinalClientKYC["BeneficiaryAccountNum"] = dr["AccountNo"];
                drFinalClientKYC["Nominee"] = dr["Nominee1"];

                if (drClientDematJointList.Count() > 0)
                    drFinalClientKYC["Holding"] = drClientDematJointList[0][7].ToString();
                else
                    drFinalClientKYC["Holding"] = dr["Holding"];
                kycStatus.Add("JOINT1", dr["C_IsKYCAvailable"].ToString() == "Y" ? "1" : "0");
                if (drClientDematJointList.Count() > 0)
                {
                    if (drClientDematJointList.Count() == 1)
                    {
                        drFinalClientKYC["SecondHolder"] = drClientDematJointList[0]["JointName"];
                        drFinalClientKYC["SecondHolderPAN"] = drClientDematJointList[0]["JointPAN"];
                        drFinalClientKYC["SecondHolderKYC"] = drClientDematJointList[0]["JointKYC"];

                        //drFinalClientKYC["Holding"] = "";
                        //drFinalClientKYC["Holding"] = "";
                        //drFinalClientKYC["Holding"] = "";

                        kycStatus.Add("JOINT2", drClientDematJointList[0]["JointKYC"].ToString() == "Y" ? "1" : "0");
                    }
                    else if (drClientDematJointList.Count() > 1)
                    {
                        //drFinalClientKYC["Holding"] = drClientDematJointList[0]["Holdings"]; 
                        drFinalClientKYC["SecondHolder"] = drClientDematJointList[0]["JointName"];
                        drFinalClientKYC["SecondHolderPAN"] = drClientDematJointList[0]["JointPAN"];
                        drFinalClientKYC["SecondHolderKYC"] = drClientDematJointList[0]["JointKYC"];

                        drFinalClientKYC["ThirdHolder"] = drClientDematJointList[1]["JointName"];
                        drFinalClientKYC["ThirdHolderPAN"] = drClientDematJointList[1]["JointPAN"];
                        drFinalClientKYC["SThirdHolderKYC"] = drClientDematJointList[1]["JointKYC"];

                        kycStatus.Add("JOINT2", drClientDematJointList[0]["JointKYC"].ToString() == "Y" ? "1" : "0");
                        kycStatus.Add("JOINT3", drClientDematJointList[1]["JointKYC"].ToString() == "Y" ? "1" : "0");

                    }
                }
                else
                {
                    drFinalClientKYC["SecondHolder"] = "";
                    drFinalClientKYC["SecondHolderPAN"] = "";
                    drFinalClientKYC["SecondHolderKYC"] = "";

                    drFinalClientKYC["ThirdHolder"] = "";
                    drFinalClientKYC["ThirdHolderPAN"] = "";
                    drFinalClientKYC["SThirdHolderKYC"] = "";
                }

                drFinalClientKYC["Privilege"] = GetClientPrivilege(kycStatus);
                dtFinalClientKYCList.Rows.Add(drFinalClientKYC);


            }

            return dtFinalClientKYCList;


        }


        private string GetClientPrivilege(Dictionary<string, string> kycStatus)
        {
            string privilege = "Partial";

            var kycNo = kycStatus.Where(pair => pair.Key.StartsWith("JOINT") && pair.Value == "0");
            var kycYes = kycStatus.Where(pair => pair.Key.StartsWith("JOINT") && pair.Value == "1");
            if (kycStatus.Count == kycYes.Count())
                privilege = "Full";
            else if (kycStatus.Count == kycNo.Count())
                privilege = "None";

            return privilege;

        }


        public bool updateTradeBusinessDate(int Tradebusinessid, string txt, DateTime date)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                return OnlineOrderBackOfficeDao.updateTradeBusinessDate(Tradebusinessid, txt, date);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public bool deleteTradeBusinessDate(int TradeBusinessId)
        {
            bool blResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                //return OnlineOrderBackOfficeDao.deleteTradeBusinessDate(tradeBusinessDateVo);
                blResult = OnlineOrderBackOfficeDao.deleteTradeBusinessDate(TradeBusinessId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;
        }
        public bool MakeTradeToHoliday(DateTime TradeBusinessDate, string datesToBeUpdated, TradeBusinessDateVo TradeBusinessDateVo)
        {
            bool blResult = false;

            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                blResult = OnlineOrderBackOfficeDao.MakeTradeToHoliday(TradeBusinessDate, datesToBeUpdated, TradeBusinessDateVo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:MakeTradeToHoliday()");

                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        //public bool MakeTradeToHoliday(DateTime TradeBusinessDate, string datesToBeUpdated)
        //{
        //    bool blResult = false;

        //    OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
        //    try
        //    {
        //        blResult = OnlineOrderBackOfficeDao.MakeTradeToHoliday(TradeBusinessDate, datesToBeUpdated);
        //    }
        //    catch (BaseApplicationException Ex)
        //    {
        //        throw Ex;
        //    }
        //    catch (Exception Ex)
        //    {
        //        BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
        //        NameValueCollection FunctionInfo = new NameValueCollection();

        //        FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:MakeTradeToHoliday()");

        //        object[] objects = new object[2];
        //        FunctionInfo = exBase.AddObject(FunctionInfo, objects);
        //        exBase.AdditionalInformation = FunctionInfo;
        //        ExceptionManager.Publish(exBase);
        //        throw exBase;

        //    }
        //    return blResult;
        //}
        public DataSet GetOnlineIssueExtractPreview(DateTime date)
        {
            DataSet dsGetOnlineIssueExtractPreview;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dsGetOnlineIssueExtractPreview = daoOnlineOrderBackOffice.GetOnlineNCDExtractPreview(date);
            return dsGetOnlineIssueExtractPreview;

        }
        public DataSet GetAllTradeBussiness(int year, int holiday)
        {
            DataSet dsGetAllTradeBussiness;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dsGetAllTradeBussiness = daoOnlineOrderBackOffice.GetAllTradeBussiness(year, holiday);
            return dsGetAllTradeBussiness;
        }
        public int YearCheck(int year)
        {
            int result = 0;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                result = OnlineOrderBackOfficeDao.YearCheck(year);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public bool Updateproductamcscheme(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int SchemePlanCode, int userid)
        {
            bool blResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                blResult = OnlineOrderBackOfficeDao.Updateproductamcscheme(mfProductAMCSchemePlanDetailsVo, SchemePlanCode, userid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateSchemeSetUpDetail()");
                object[] objects = new object[3];
                objects[0] = mfProductAMCSchemePlanDetailsVo;
                objects[1] = SchemePlanCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }
        public void CreateOnlineSchemeSetupPlan(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int userId, ref int schemeplancode)
        {

            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                OnlineOrderBackOfficeDao.CreateOnlineSchemeSetupPlan(mfProductAMCSchemePlanDetailsVo, userId, ref schemeplancode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public void CreateOnlineSchemeSetupPlanDetails(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int userId)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                OnlineOrderBackOfficeDao.CreateOnlineSchemeSetupPlanDetails(mfProductAMCSchemePlanDetailsVo, userId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }
        public string GetExtCode(int schemplancode, int isonline)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            string extCode = string.Empty;
            try
            {
                extCode = OnlineOrderBackOfficeDao.GetExtCode(schemplancode, isonline);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return extCode;
        }

        public DataTable GetSchemeForMarge(int AmcCode, int Schemeplanecode, string Type)
        {
            DataTable dtGetSchemeForMarge = new DataTable();
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            try
            {
                dtGetSchemeForMarge = daoOnlineOrderBackOffice.GetSchemeForMarge(AmcCode, Schemeplanecode, Type);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetSchemeForMarge;
        }
        public bool CreateMargeScheme(int SchemePlaneCode, int MargeScheme, DateTime Date, int UserId)
        {
            bool blResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                blResult = OnlineOrderBackOfficeDao.CreateMargeScheme(SchemePlaneCode, MargeScheme, Date, UserId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;
        }
        public int BussinessDateCheck(DateTime Date)
        {
            int result = 0;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                result = OnlineOrderBackOfficeDao.BussinessDateCheck(Date);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public String SchemeStatus(int schemeplanecode)
        {
            string status = "";
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                status = OnlineOrderBackOfficeDao.SchemeStatus(schemeplanecode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return status;
        }
        public DataTable GetMergeScheme(int Schemeplanecode)
        {

            DataTable dtGetMergeScheme = new DataTable();
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            try
            {
                dtGetMergeScheme = daoOnlineOrderBackOffice.GetMergeScheme(Schemeplanecode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMergeScheme;
        }
        public String DividentType(int schemeplanecode)
        {
            string Type = "";
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                Type = OnlineOrderBackOfficeDao.DividentType(schemeplanecode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return Type;

        }
        public String GetProductAddedCode(int schemeplanecode)
        {
            string Productcode = "";
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                Productcode = OnlineOrderBackOfficeDao.GetProductAddedCode(schemeplanecode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return Productcode;

        }
        public DataSet GetAdviserCustomersAllMFAccounts(int IsValued, int advisorId)
        {
            DataSet dsAdviserCustomersAllMFAccounts;
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();

            try
            {
                dsAdviserCustomersAllMFAccounts = OnlineOrderBackOfficeDao.GetAdviserCustomersAllMFAccounts(IsValued, advisorId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:GetAdviserCustomersAllMFAccounts()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return dsAdviserCustomersAllMFAccounts;
        }
        public void UpdateAdviserCustomersAllMFAccounts(string gvMFAId, int ModifiedBy)
        {
            OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                OnlineOrderBackOfficeDao.UpdateAdviserCustomersAllMFAccounts(gvMFAId, ModifiedBy);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:UpdateAdviserCustomersAllMFAccounts()");

                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }
        public DataSet Getproductcode(int schemeplancode)
        {
            DataSet dsGetproductcode;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dsGetproductcode = daoOnlineOrderBackOffice.Getproductcode(schemeplancode);
            return dsGetproductcode;
        }
        public bool Createproductcode(int Schemeplancode, string Productcode, string Externaltype, string XESSourcecode, int Userid)
        {
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();

            bool bResult = false;
            try
            {
                bResult = daoOnlineOrderBackOffice.Createproductcode(Schemeplancode, Productcode, Externaltype, XESSourcecode, Userid);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool UpdateProductcode(int Productamcdetailid, string Productcode, int userid)
        {
            bool blResult = false;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            try
            {
                blResult = daoOnlineOrderBackOffice.UpdateProductcode(Productamcdetailid, Productcode, userid);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return blResult;
        }
        public String ExternalCode(string Externaltype)
        {
            string Type = "";
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                Type = OnlineOrderBackOfficeDao.ExternalCode(Externaltype);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return Type;

        }
        public int GetSchemecode(int schemeplancode)
        {
            int schemecode = 0;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            schemecode = daoOnlineOrderBackOffice.GetSchemecode(schemeplancode);
            return schemecode;
        }
        public DataTable GetSchemeLookupType(string dividentType)
        {
            DataTable dtGetSchemeLookupType;
            OnlineOrderBackOfficeDao daoOnlineOrderBackOffice = new OnlineOrderBackOfficeDao();
            dtGetSchemeLookupType = daoOnlineOrderBackOffice.GetSchemeLookupType(dividentType);
            return dtGetSchemeLookupType;
        }
        public DataTable GetRTAInitialReport(string type, DateTime fromDate, DateTime toDate)
        {
            DataTable dt;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dt = OnlineOrderBackOfficeDao.GetRTAInitialReport(type, fromDate, toDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public DataTable GetAMCListRNTWise(string RNTType)
        {
            DataTable dtGetAMCListRNTWise;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dtGetAMCListRNTWise = OnlineOrderBackOfficeDao.GetAMCListRNTWise(RNTType);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAMCListRNTWise;
        }
        public DataTable GetSubBrokerCodeCleansing(string RNTType, int AMCCode, int schemePlanCode, int adviserId, int subBrokerCode)
        {
            DataTable dtGetSubBrokerCodeCleansing;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                dtGetSubBrokerCodeCleansing = OnlineOrderBackOfficeDao.GetSubBrokerCodeCleansing(RNTType, AMCCode, schemePlanCode, adviserId, subBrokerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetSubBrokerCodeCleansing;
        }
        public bool UpdateSubBrokerCode(string transactionId, string subBrokerCode)
        {
            bool bResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                bResult = OnlineOrderBackOfficeDao.UpdateSubBrokerCode(transactionId, subBrokerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool UpdateNewSubBrokerCode(DataTable dtSubBrokerCode)
        {
            bool bResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                bResult = OnlineOrderBackOfficeDao.UpdateNewSubBrokerCode(dtSubBrokerCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public int SchemeCode(string externalcode, int AMCCode)
        {
            int result = 0;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                result = OnlineOrderBackOfficeDao.SchemeCode(externalcode, AMCCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public string GetExternalCode(int AMCCode, int productmappingcode)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            string extCode = string.Empty;
            try
            {
                extCode = OnlineOrderBackOfficeDao.GetExternalCode(AMCCode, productmappingcode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return extCode;
        }
        public DataTable SearchOnPRoduct(int orderNo, int applicationNo)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            DataTable dtSearchOnPRoduct;
            try
            {
                dtSearchOnPRoduct = OnlineOrderBackOfficeDao.SearchOnPRoduct(orderNo, applicationNo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtSearchOnPRoduct;
        }
        public DataSet GetAMCList()
        {

            DataSet ds;
            OnlineOrderBackOfficeDao saAMCListDao = new OnlineOrderBackOfficeDao();

            try
            {
                ds = saAMCListDao.GetAMCList();
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:GetAMCList()");
                object[] objects = new object[2];

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

            return ds;
        }


        public bool CreateAMC(string amcName, int isOnline, int userId, string AmcCode)
        {
            bool bResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                bResult = OnlineOrderBackOfficeDao.CreateAMC(amcName, isOnline, userId, AmcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:CreateAMC()");
                object[] objects = new object[2];
                objects[0] = amcName;
                objects[1] = isOnline;
                //objects[2] = userId;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool UpdateAMC(string amcName, int isOnline, int userId, int amcCode, string AmcCode)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                return OnlineOrderBackOfficeDao.UpdateAMC(amcName, isOnline, userId, amcCode, AmcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:UpdateAMC()");
                object[] objects = new object[4];
                objects[0] = amcName;
                objects[1] = isOnline;
                objects[2] = userId;
                objects[3] = amcCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public bool deleteAMC(int amcCode)
        {
            bool blResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                //return OnlineOrderBackOfficeDao.deleteTradeBusinessDate(tradeBusinessDateVo);
                blResult = OnlineOrderBackOfficeDao.deleteAMC(amcCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeBo.cs:deleteAMC()");
                object[] objects = new object[4];
                objects[0] = amcCode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }
        public DataTable GetUTIAMCDetails(int adviserId, DateTime fromDate, DateTime toDate)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            DataTable dt;
            try
            {
                dt = OnlineOrderBackOfficeDao.GetUTIAMCDetails(adviserId, fromDate, toDate);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public bool ProductcodeDelete(int ScheneMappingId)
        {
            bool bResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                bResult = OnlineOrderBackOfficeDao.ProductcodeDelete(ScheneMappingId);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool InsertUpdateDeleteOnBannerDetails(int id, string assetGroupCode, int userId, string imageName, DateTime expiryDate, int isDelete)
        {
            bool bResult = false;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                bResult = OnlineOrderBackOfficeDao.InsertUpdateDeleteOnBannerDetails(id, assetGroupCode, userId, imageName, expiryDate, isDelete);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public DataTable GetBannerDetailsWithAssetGroup()
        {


            OnlineOrderBackOfficeDao onlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            DataTable dt;
            try
            {
                dt = onlineOrderBackOfficeDao.GetBannerDetailsWithAssetGroup();
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
        public int SchemeCodeonline(string externalcode, int AMCCode)
        {
            int result = 0;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                result = OnlineOrderBackOfficeDao.SchemeCodeonline(externalcode, AMCCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public string GetExternalCodeOnline(int AMCCode)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            string extCode = string.Empty;
            try
            {
                extCode = OnlineOrderBackOfficeDao.GetExternalCodeOnline(AMCCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return extCode;
        }
        public int GetAMCCode(string AMCCode)
        {
            int result = 0;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
                result = OnlineOrderBackOfficeDao.GetAMCCode(AMCCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return result;
        }
        public string CheckAMCCode(int AMCCode)
        {
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            string amcCode = string.Empty;
            try
            {
                amcCode = OnlineOrderBackOfficeDao.CheckAMCCode(AMCCode);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return amcCode;
        }
        public DataTable GetProductSearchType(string folioNo)
        {
            DataTable dt;
            OnlineOrderBackOfficeDao OnlineOrderBackOfficeDao = new OnlineOrderBackOfficeDao();
            try
            {
               dt= OnlineOrderBackOfficeDao.GetProductSearchType(folioNo);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dt;
        }
    }
}
