using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VoOnlineOrderManagemnet;



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



        public DataSet GetExtractTypeDataForFileCreation(DateTime orderDate, int AdviserId, int extractType, DateTime fromDate, DateTime toDate, string status)
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
                db.AddInParameter(GetGetMfOrderExtractCmd, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(GetGetMfOrderExtractCmd, "@status", DbType.String, status);



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
            //if (AmcCode == 19 && TransactionType == "AMCBANK")
            //{

            //}
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

        public DataSet GetOrderExtractHeaderMapping(string RtaIdentifier, bool isFatca)
        {
            DataSet dsHeaderMapping;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_OrderExtractHeaderMapping");
                db.AddInParameter(cmd, "@XES_SourceCode", DbType.String, RtaIdentifier);
                db.AddInParameter(cmd, "@IsFatca", DbType.Boolean, isFatca);

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
        public DataSet GetSubCategory(string CategoryCode)
        {
            Database db;
            DataSet dsSubCategory;
            DbCommand SubCategorycmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SubCategorycmd = db.GetStoredProcCommand("Sproc_BindSubCategory");
                db.AddInParameter(SubCategorycmd, "@CategoryCode", DbType.String, CategoryCode);
                dsSubCategory = db.ExecuteDataSet(SubCategorycmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetSubCategory()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSubCategory;
        }
        public DataSet GetSubSubCategory(string CategoryCode, string SubCategoryCode)
        {
            Database db;
            DataSet dsSubSubCategory;
            DbCommand SubSubCategorycmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SubSubCategorycmd = db.GetStoredProcCommand("Sproc_BindSubSubCategory");
                db.AddInParameter(SubSubCategorycmd, "@CategoryCode", DbType.String, CategoryCode);
                db.AddInParameter(SubSubCategorycmd, "@SubCategoryCode", DbType.String, SubCategoryCode);
                dsSubSubCategory = db.ExecuteDataSet(SubSubCategorycmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetSubSubCategory()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSubSubCategory;
        }

        public List<int> CreateOnlineSchemeSetUp(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int userId)
        {
            List<int> SchemePlancodes = new List<int>();


            Database db;
            DbCommand createMFOnlineSchemeSetUpCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOnlineSchemeSetUpCmd = db.GetStoredProcCommand("SPROC_Onl_CreateOnlineSchemeSetUp");
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PA_AMCCode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.AMCCode);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanName", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemePlanName);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanCode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SchemePlanCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanName", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemePlanName);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAISSC_AssetInstrumentSubSubCategoryCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetCategoryCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAG_AssetGroupCode", DbType.String, mfProductAMCSchemePlanDetailsVo.Product);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_Status", DbType.String, mfProductAMCSchemePlanDetailsVo.Status);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_IsOnline", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsOnline);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_IsDirect", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsDirect);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_FaceValue", DbType.Double, mfProductAMCSchemePlanDetailsVo.FaceValue);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PSLV_LookupValueCodeForSchemeType", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemeType);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PSLV_LookupValueCodeForSchemeOption", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemeOption);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XF_DividendFrequency", DbType.String, mfProductAMCSchemePlanDetailsVo.DividendFrequency);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_BankName", DbType.String, mfProductAMCSchemePlanDetailsVo.BankName);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_AccountNumber", DbType.String, mfProductAMCSchemePlanDetailsVo.AccountNumber);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_Branch", DbType.String, mfProductAMCSchemePlanDetailsVo.Branch);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@WCMV_Lookup_BankId", DbType.Int32, mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsNFO", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsNFO);
                if (mfProductAMCSchemePlanDetailsVo.NFOStartDate != DateTime.MinValue)
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOStartDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.NFOStartDate);
                }
                else
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOStartDate", DbType.DateTime, DBNull.Value);
                }
                if (mfProductAMCSchemePlanDetailsVo.NFOEndDate != DateTime.MinValue)
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOEndDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.NFOEndDate);
                }
                else
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOEndDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_LockInPeriod", DbType.Int32, mfProductAMCSchemePlanDetailsVo.LockInPeriod);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_CutOffTime", DbType.String, mfProductAMCSchemePlanDetailsVo.CutOffTime.ToString());
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_EntryLoadPercentag", DbType.Double, mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_EntryLoadRemark", DbType.String, mfProductAMCSchemePlanDetailsVo.EntryLoadRemark);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_ExitLoadPercentage", DbType.Double, mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_ExitLoadRemark", DbType.String, mfProductAMCSchemePlanDetailsVo.ExitLoadRemark);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsPurchaseAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsRedeemAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSIPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSIPAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSWPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSWPAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSwitchAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSTPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSTPAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_InitialPurchaseAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_InitialMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_AdditionalPruchaseAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_AdditionalMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinRedemptionAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_RedemptionMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinRedemptionUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_RedemptionMultiplesUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinSwitchAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinSwitchAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_SwitchMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinSwitchUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MinSwitchUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_SwitchMultiplesUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XF_FileGenerationFrequency", DbType.String, mfProductAMCSchemePlanDetailsVo.GenerationFrequency);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XES_SourceCode", DbType.String, mfProductAMCSchemePlanDetailsVo.SourceCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XES_SourceCode", DbType.String, DBNull.Value);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XCST_CustomerSubTypeCode", DbType.String, mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_SecurityCode", DbType.String, mfProductAMCSchemePlanDetailsVo.SecurityCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MaxInvestment", DbType.Double, mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@WCMV_Lookup_BankId", DbType.Int32, mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@ExternalCode", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@ExternalType", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalType);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_ModifiedBy", DbType.Int32, userId);
                // db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@WERPBM_BankCode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.Bankcode);
                // db.AddOutParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanCode", DbType.Int32, 10000);

                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XF_DividendFrequency", DbType.String, mfProductAMCSchemePlanDetailsVo.DividendFrequency);
                db.ExecuteNonQuery(createMFOnlineSchemeSetUpCmd);
                //{
                //    SchemePlancode = Convert.ToInt32(db.GetParameterValue(createMFOnlineSchemeSetUpCmd, "PASP_SchemePlanCode").ToString());
                //    SchemePlancodes.Add(SchemePlancode);


                //}
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
            Database db;
            DataSet getSchemeSetUpDs;
            DbCommand getSchemeSetUpCmd;

            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                getSchemeSetUpCmd = db.GetStoredProcCommand("Sproc_getOnlineschemeSetUp");
                db.AddInParameter(getSchemeSetUpCmd, "@PASP_SchemePlanCode", DbType.Int32, SchemePlanCode);
                getSchemeSetUpDs = db.ExecuteDataSet(getSchemeSetUpCmd);
                if (getSchemeSetUpDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in getSchemeSetUpDs.Tables[0].Rows)
                    {

                        mfProductAMCSchemePlanDetailsVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        mfProductAMCSchemePlanDetailsVo.SchemePlanName = dr["PASP_SchemePlanName"].ToString();
                        mfProductAMCSchemePlanDetailsVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory = dr["PAISSC_AssetInstrumentSubSubCategoryCode"].ToString();
                        mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                        mfProductAMCSchemePlanDetailsVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        mfProductAMCSchemePlanDetailsVo.Product = dr["PAG_AssetGroupCode"].ToString();
                        mfProductAMCSchemePlanDetailsVo.Status = dr["PASP_Status"].ToString();
                        if (dr["PASP_IsOnline"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsOnline = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsOnline = 0;
                        }
                        if (mfProductAMCSchemePlanDetailsVo.IsDirect != 0)
                        {
                            mfProductAMCSchemePlanDetailsVo.IsDirect = int.Parse(dr["PASP_IsDirect"].ToString());
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsDirect = 0;
                        }
                        if (dr["PASPD_FaceValue"].ToString() != null && dr["PASPD_FaceValue"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.FaceValue = Convert.ToDouble(dr["PASPD_FaceValue"].ToString());
                        mfProductAMCSchemePlanDetailsVo.SchemeType = dr["PSLV_LookupValueCodeForSchemeType"].ToString();
                        mfProductAMCSchemePlanDetailsVo.SchemeOption = dr["PSLV_LookupValueCodeForSchemeOption"].ToString();
                        if (dr["PSLV_DividentType"].ToString() != null) //&& dr["XF_DividendFrequency"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.DividendFrequency = dr["PSLV_DividentType"].ToString();
                        mfProductAMCSchemePlanDetailsVo.BankName = dr["PASPD_BankName"].ToString();
                        if (dr["WCMV_Lookup_BankId"].ToString() != string.Empty && dr["WCMV_Lookup_BankId"].ToString() != null)
                            mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId = int.Parse(dr["WCMV_Lookup_BankId"].ToString());

                        mfProductAMCSchemePlanDetailsVo.AccountNumber = dr["PASPD_AccountNumber"].ToString();
                        mfProductAMCSchemePlanDetailsVo.Branch = dr["PASPD_Branch"].ToString();
                        if (dr["PASPD_IsNFO"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsNFO = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsNFO = 0;
                        }
                        if (dr["PASP_NFOStartDate"].ToString() != string.Empty)

                            mfProductAMCSchemePlanDetailsVo.NFOStartDate = DateTime.Parse(dr["PASP_NFOStartDate"].ToString());
                        if (dr["PASP_NFOEndDate"].ToString() != string.Empty)

                            mfProductAMCSchemePlanDetailsVo.NFOEndDate = DateTime.Parse(dr["PASP_NFOEndDate"].ToString());
                        if (dr["PASPD_LockInPeriod"].ToString() != null && dr["PASPD_LockInPeriod"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.LockInPeriod = int.Parse(dr["PASPD_LockInPeriod"].ToString());
                        if (dr["PASPD_CutOffTime"].ToString() != null && dr["PASPD_CutOffTime"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.CutOffTime = TimeSpan.Parse(dr["PASPD_CutOffTime"].ToString());
                        if (dr["PASPD_EntryLoadPercentage"].ToString() != null && dr["PASPD_EntryLoadPercentage"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag = Convert.ToDouble(dr["PASPD_EntryLoadPercentage"].ToString());
                        if (dr["PASPD_EntryLoadRemark"].ToString() != null && dr["PASPD_EntryLoadRemark"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.EntryLoadRemark = dr["PASPD_EntryLoadRemark"].ToString();
                        if (dr["PASPD_ExitLoadPercentage"].ToString() != null && dr["PASPD_ExitLoadPercentage"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage = Convert.ToDouble(dr["PASPD_ExitLoadPercentage"].ToString());
                        if (dr["PASPD_ExitLoadRemark"].ToString() != null && dr["PASPD_ExitLoadRemark"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.ExitLoadRemark = dr["PASPD_ExitLoadRemark"].ToString();
                        if (dr["PASPD_IsPurchaseAvailable"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable = 0;
                        }
                        if (dr["PASPD_IsRedeemAvailable"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable = 0;
                        }
                        if (dr["PASPD_IsSIPAvailable"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSIPAvailable = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSIPAvailable = 0;
                        }
                        if (dr["PASPD_IsSWPAvailable"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSWPAvailable = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSWPAvailable = 0;
                        }
                        if (dr["PASPD_IsSwitchAvailable"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable = 0;
                        }
                        if (dr["PASPD_IsSTPAvailable"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSTPAvailable = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsSTPAvailable = 0;
                        }
                        if (dr["PASPD_InitialPurchaseAmount"].ToString() != null && dr["PASPD_InitialPurchaseAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount = Convert.ToDouble(dr["PASPD_InitialPurchaseAmount"].ToString());
                        if (dr["PASPD_InitialMultipleAmount"].ToString() != null && dr["PASPD_InitialMultipleAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount = Convert.ToDouble(dr["PASPD_InitialMultipleAmount"].ToString());
                        if (dr["PASPD_InitialMultipleAmount"].ToString() != null && dr["PASPD_InitialMultipleAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount = Convert.ToDouble(dr["PASPD_InitialMultipleAmount"].ToString());
                        if (dr["PASPD_AdditionalMultipleAmount"].ToString() != null && dr["PASPD_AdditionalMultipleAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount = Convert.ToDouble(dr["PASPD_AdditionalMultipleAmount"].ToString());
                        if (dr["PASPD_MinRedemptionAmount"].ToString() != null && dr["PASPD_MinRedemptionAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount = Convert.ToDouble(dr["PASPD_MinRedemptionAmount"].ToString());
                        if (dr["PASPD_AdditionalPruchaseAmount"].ToString() != null && dr["PASPD_AdditionalPruchaseAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount = Convert.ToDouble(dr["PASPD_AdditionalPruchaseAmount"].ToString());
                        if (dr["PASPD_RedemptionMultipleAmount"].ToString() != null && dr["PASPD_RedemptionMultipleAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount = Convert.ToDouble(dr["PASPD_RedemptionMultipleAmount"].ToString());
                        if (dr["PASPD_MinRedemptionUnits"].ToString() != null && dr["PASPD_MinRedemptionUnits"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits = Convert.ToDouble(dr["PASPD_MinRedemptionUnits"].ToString());
                        if (dr["PASPD_RedemptionMultiplesUnits"].ToString() != null && dr["PASPD_RedemptionMultiplesUnits"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits = Convert.ToDouble(dr["PASPD_RedemptionMultiplesUnits"].ToString());
                        if (dr["PASPD_MinSwitchAmount"].ToString() != null && dr["PASPD_MinSwitchAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.MinSwitchAmount = Convert.ToDouble(dr["PASPD_MinSwitchAmount"].ToString());
                        if (dr["PASPD_SwitchMultipleAmount"].ToString() != null && dr["PASPD_SwitchMultipleAmount"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount = Convert.ToDouble(dr["PASPD_SwitchMultipleAmount"].ToString());
                        if (dr["PASPD_MinSwitchUnits"].ToString() != null && dr["PASPD_MinSwitchUnits"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.MinSwitchUnits = int.Parse(dr["PASPD_MinSwitchUnits"].ToString());
                        if (dr["PASPD_SwitchMultiplesUnits"].ToString() != null && dr["PASPD_SwitchMultiplesUnits"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits = int.Parse(dr["PASPD_SwitchMultiplesUnits"].ToString());
                        if (dr["XF_FileGenerationFrequency"].ToString() != null && dr["XF_FileGenerationFrequency"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.GenerationFrequency = dr["XF_FileGenerationFrequency"].ToString();
                        if (!string.IsNullOrEmpty(dr["XMLSourceCode"].ToString()))
                        {
                            mfProductAMCSchemePlanDetailsVo.SourceCode = dr["XMLSourceCode"].ToString();
                        }
                        //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XES_SourceCode", DbType.String, DBNull.Value);
                        mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode = dr["XCST_CustomerSubTypeCode"].ToString();
                        if (dr["PASPD_SecurityCode"].ToString() != null && dr["PASPD_SecurityCode"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.SecurityCode = dr["PASPD_SecurityCode"].ToString();
                        if (dr["PASPD_MaxInvestment"].ToString() != null && dr["PASPD_MaxInvestment"].ToString() != string.Empty)
                        {
                            mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment = Convert.ToDouble(dr["PASPD_MaxInvestment"].ToString());
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment = 0;
                        }
                        //if (!string.IsNullOrEmpty(dr["WCMV_Lookup_BankId"].ToString()))
                        //{
                        //    mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId = int.Parse(dr["WCMV_Lookup_BankId"].ToString());
                        //}
                        if (!string.IsNullOrEmpty(dr["onlinecode"].ToString()))
                        {
                            mfProductAMCSchemePlanDetailsVo.ExternalCode = dr["onlinecode"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["offlinecode"].ToString()))
                        {
                            mfProductAMCSchemePlanDetailsVo.productcode = dr["offlinecode"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["productcode"].ToString()))
                        {
                            mfProductAMCSchemePlanDetailsVo.Allproductcode = dr["productcode"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["AMFIExternalcode"].ToString()))
                        {
                            mfProductAMCSchemePlanDetailsVo.AMFIcode = dr["AMFIExternalcode"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["PASC_AMC_ExternalType"].ToString()))
                        {
                            mfProductAMCSchemePlanDetailsVo.ExternalType = dr["PASC_AMC_ExternalType"].ToString();
                        }
                        if (dr["PASP_MargeToScheme"].ToString() != null && dr["PASP_MargeToScheme"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.Mergecode = int.Parse(dr["PASP_MargeToScheme"].ToString());
                        if (dr["PASP_ISIN"].ToString() != null && dr["PASP_ISIN"].ToString() != string.Empty)
                            mfProductAMCSchemePlanDetailsVo.ISINNo = dr["PASP_ISIN"].ToString();
                        if (dr["PASP_SchemeOpenDate"].ToString() != string.Empty)

                            mfProductAMCSchemePlanDetailsVo.SchemeStartDate = DateTime.Parse(dr["PASP_SchemeOpenDate"].ToString());
                        if (dr["PASP_MaturityDate"].ToString() != string.Empty)

                            mfProductAMCSchemePlanDetailsVo.MaturityDate = DateTime.Parse(dr["PASP_MaturityDate"].ToString());
                        if (dr["PASPD_IsOnlineEnablement"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement = 0;
                        }
                        if (dr["PASPD_IsETFType"].ToString() == "True")
                        {
                            mfProductAMCSchemePlanDetailsVo.IsETFT = 1;
                        }
                        else
                        {
                            mfProductAMCSchemePlanDetailsVo.IsETFT = 0;
                        }
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

                FunctionInfo.Add("Method", "CustomerBankAccountDao.cs:GetOnlineSchemeSetUp()");


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
            Database db;
            DbCommand CmdSchemeSetUpFromOverAllCategoryList;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CmdSchemeSetUpFromOverAllCategoryList = db.GetStoredProcCommand("SP_GetSchemeSetUpFromOverAllCategoryList");
                db.AddInParameter(CmdSchemeSetUpFromOverAllCategoryList, "@AmcCode", DbType.Int32, amcCode);
                db.AddInParameter(CmdSchemeSetUpFromOverAllCategoryList, "@CategoryCode", DbType.String, categoryCode);
                dsSchemeSetUpFromOverAllCategoryList = db.ExecuteDataSet(CmdSchemeSetUpFromOverAllCategoryList);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsSchemeSetUpFromOverAllCategoryList;
        }
        public int ExternalcodeCheck(string externalcode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdExternalcodeCheck;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdExternalcodeCheck = db.GetStoredProcCommand("SPROC_TocheckingExternalCode");
                db.AddInParameter(cmdExternalcodeCheck, "@Externalcode", DbType.String, externalcode);
                db.AddOutParameter(cmdExternalcodeCheck, "@count", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdExternalcodeCheck);
                if (db.ExecuteNonQuery(cmdExternalcodeCheck) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdExternalcodeCheck, "count").ToString());
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
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                objects[0] = externalcode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return count;
        }

        public string GetExtCode(int schemplancode, int isonline)
        {
            Database db;
            DataSet ds;
            DbCommand cmdGetExtCode;
            string extCode = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetExtCode = db.GetStoredProcCommand("SPROC_GetExtCOde");
                db.AddInParameter(cmdGetExtCode, "@SchemePlanCode", DbType.Int32, schemplancode);
                db.AddInParameter(cmdGetExtCode, "@isonline", DbType.Int32, isonline);
                db.AddOutParameter(cmdGetExtCode, "@PASC_AMC_ExternalCode", DbType.String, 20);
                ds = db.ExecuteDataSet(cmdGetExtCode);
                if (db.ExecuteNonQuery(cmdGetExtCode) != 0)
                {
                    extCode = db.GetParameterValue(cmdGetExtCode, "PASC_AMC_ExternalCode").ToString();
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
                FunctionInfo.Add("Method", "AssociateDAO.cs:ExternalcodeCheck()");
                object[] objects = new object[2];
                // objects[0] = externalcode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return extCode;
        }


        public bool AMFIduplicateCheck(int schemeplancode, string externalcode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdCodeduplicateCheck;
            bool bResult = false;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //Adding Data to the table 
                cmdCodeduplicateCheck = db.GetStoredProcCommand("SPROC_AMFICodeDuplicate");
                db.AddInParameter(cmdCodeduplicateCheck, "@PASP_SchemePlanName", DbType.Int32, schemeplancode);
                db.AddInParameter(cmdCodeduplicateCheck, "@ExternalCode", DbType.String, externalcode);
                db.AddOutParameter(cmdCodeduplicateCheck, "@count", DbType.Int32, 10);

                ds = db.ExecuteDataSet(cmdCodeduplicateCheck);
                Object objCount = db.GetParameterValue(cmdCodeduplicateCheck, "@count");
                if (objCount != DBNull.Value)
                    count = int.Parse(db.GetParameterValue(cmdCodeduplicateCheck, "@count").ToString());
                else
                    count = 0;
                if (count > 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:AMFIduplicateCheck()");
                object[] objects = new object[2];
                objects[0] = schemeplancode;
                objects[1] = externalcode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
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

        public DataSet GetMFOrderDetailsForRTAExtract(int adviserId, string transactionType, string rtaIdentifier, int amcCode, int userId)
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

        public DataSet GetFrequency()
        {
            Database db;
            DataSet dsFrequency;
            DbCommand Frequencycmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Frequencycmd = db.GetStoredProcCommand("SProc_onl_BindFrequency");
                dsFrequency = db.ExecuteDataSet(Frequencycmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetFrequency()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsFrequency;
        }
        public DataSet GetLookupCategory()
        {
            DataSet dsGetLookupCategory;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetLookupCategory");
                dsGetLookupCategory = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetLookupCategory()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetLookupCategory;
        }

        public DataSet GetWERPValues(int categoryID)
        {
            DataSet dsGetWERPValues;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetWERPValues");
                db.AddInParameter(cmd, "@categoryID", DbType.Int32, categoryID);
                dsGetWERPValues = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetWERPValues()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetWERPValues;
        }
        public DataSet GetRtaWiseMapings(string sourceCode, int categoryID)
        {
            DataSet dsGetRtaWiseMapings;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetRtaWiseMapings");
                db.AddInParameter(cmd, "@SourceCode", DbType.String, sourceCode);
                db.AddInParameter(cmd, "@CategoryID", DbType.String, categoryID);

                dsGetRtaWiseMapings = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetRtaWiseMapings()");
                object[] objects = new object[0];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetRtaWiseMapings;
        }


        public DataSet GetRTA()
        {
            DataSet dsGetRTA;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetRTA");
                dsGetRTA = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetRTA()");
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
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_Onl_GetRTA");
                dsGetRTAList = db.ExecuteDataSet(cmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dsGetRTAList;
        }
        public bool CreateMapwithRTA(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo, int userID)
        {
            bool bResult = false;
            Database db;
            DbCommand createCmd;
            int affectedRecords = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateMapwithRTA");
                db.AddInParameter(createCmd, "@WCMV_LookupId", DbType.Int32, werplookupCodeValueManagementVo.LookupID);
                db.AddInParameter(createCmd, "@XES_SourceCode", DbType.String, werplookupCodeValueManagementVo.SourceCode);
                db.AddInParameter(createCmd, "@WCMVXM_ExternalCode", DbType.String, werplookupCodeValueManagementVo.ExternalCode);
                db.AddInParameter(createCmd, "@WCMVXM_ExternalName", DbType.String, werplookupCodeValueManagementVo.ExternalName);
                db.AddInParameter(createCmd, "@UserId", DbType.Int32, userID);

                db.AddOutParameter(createCmd, "@IsExist", DbType.Int16, 0);
                if (db.ExecuteNonQuery(createCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(createCmd, "@IsExist").ToString());
                if (affectedRecords == 0)
                    bResult = true;
                else
                    bResult = false;
                //db.ExecuteNonQuery(createCmd);
                //bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool CreateNewWerpName(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo, int userID)
        {
            bool bResult = false;
            int affectedRecords = 0;
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateNewWerpName");
                db.AddInParameter(createCmd, "@WCMV_Name", DbType.String, werplookupCodeValueManagementVo.WerpName);
                db.AddInParameter(createCmd, "@WCM_Id", DbType.Int32, werplookupCodeValueManagementVo.CategoryID);
                db.AddInParameter(createCmd, "@UserId", DbType.Int32, userID);
                db.AddOutParameter(createCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(createCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(createCmd, "@IsSuccess").ToString());
                if (affectedRecords == 1)
                    bResult = true;
                else
                    bResult = false;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public bool RemoveMapingWIthRTA(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo)
        {
            bool bResult = false;
            Database db;
            DbCommand createMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SPROC_RemoveMapwithRTA");
                db.AddInParameter(createMFOrderTrackingCmd, "@MapID", DbType.Int32, werplookupCodeValueManagementVo.MapID);

                db.ExecuteNonQuery(createMFOrderTrackingCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public bool UpdateWerpName(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo, int userID)
        {
            bool bResult = false;
            Database db;
            DbCommand createMFOrderTrackingCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SPROC_UpdateInternalValues");
                db.AddInParameter(createMFOrderTrackingCmd, "@lookupID", DbType.Int32, werplookupCodeValueManagementVo.LookupID);
                db.AddInParameter(createMFOrderTrackingCmd, "@internalName", DbType.String, werplookupCodeValueManagementVo.WerpName);
                db.AddInParameter(createMFOrderTrackingCmd, "@userID", DbType.Int32, userID);

                db.ExecuteNonQuery(createMFOrderTrackingCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public bool DeleteWerpName(WERPlookupCodeValueManagementVo werplookupCodeValueManagementVo)
        {
            bool bResult = false;
            int affectedRecords = 0;
            Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_DeleteInternalValues");
                db.AddInParameter(createCmd, "@lookupID", DbType.Int32, werplookupCodeValueManagementVo.LookupID);

                db.AddOutParameter(createCmd, "@IsDeleted", DbType.Int32, 0);
                if (db.ExecuteNonQuery(createCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(createCmd, "@IsDeleted").ToString());
                if (affectedRecords == 1)
                    bResult = true;
                else
                    bResult = false;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }


        public bool UpdateSchemeSetUpDetail(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int SchemePlanCode, int userid)
        {
            bool blResult = false;
            Database db;
            DbCommand updateSchemeSetUpDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateSchemeSetUpDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_UpdateSchemeSetUpDetail");
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASP_SchemePlanCode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SchemePlanCode); //1
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_FaceValue", DbType.Double, mfProductAMCSchemePlanDetailsVo.FaceValue); //2
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PSLV_LookupValueCodeForSchemeType", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemeType); //3
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PSLV_LookupValueCodeForSchemeOption", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemeOption);//4
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@DividentType", DbType.String, mfProductAMCSchemePlanDetailsVo.DividendFrequency);//5
                //db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_BankName", DbType.String, mfProductAMCSchemePlanDetailsVo.BankName);//6
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_AccountNumber", DbType.String, mfProductAMCSchemePlanDetailsVo.AccountNumber); //7
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_Branch", DbType.String, mfProductAMCSchemePlanDetailsVo.Branch);//8
                //db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsNFO", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsNFO);//9
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_LockInPeriod", DbType.Int32, mfProductAMCSchemePlanDetailsVo.LockInPeriod); //12
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_CutOffTime", DbType.String, mfProductAMCSchemePlanDetailsVo.CutOffTime.ToString()); //13
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_EntryLoadPercentage", DbType.Double, mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag);//14
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_EntryLoadRemark", DbType.String, mfProductAMCSchemePlanDetailsVo.EntryLoadRemark);//15
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_ExitLoadPercentage", DbType.Double, mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage);//16
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_ExitLoadRemark", DbType.String, mfProductAMCSchemePlanDetailsVo.ExitLoadRemark);//17
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsPurchaseAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable);//18
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsRedeemAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable);//19
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSIPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSIPAvailable);//20
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSWPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSWPAvailable);//21
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSwitchAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable);//22
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSTPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSTPAvailable);//23
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_InitialPurchaseAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount);//24
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_InitialMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount);//25
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_AdditionalPruchaseAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount);//26
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_AdditionalMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount);//27
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinRedemptionAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount);//28
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_RedemptionMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount);//29
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinRedemptionUnits", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits);//30
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_RedemptionMultiplesUnits", DbType.Double, mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits);//31
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinSwitchAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinSwitchAmount);//32
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_SwitchMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount);//33
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinSwitchUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MinSwitchUnits);//34
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_SwitchMultiplesUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits);//35
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@XF_FileGenerationFrequency", DbType.String, mfProductAMCSchemePlanDetailsVo.GenerationFrequency);//36
                //37
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@XCST_CustomerSubTypeCode", DbType.String, mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode);//38
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_SecurityCode", DbType.String, mfProductAMCSchemePlanDetailsVo.SecurityCode);//39
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MaxInvestment", DbType.Double, mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment);//40
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@WCMV_Lookup_BankId", DbType.String, mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId);//41
                //db.AddInParameter(updateSchemeSetUpDetailsCmd, "@status", DbType.String, mfProductAMCSchemePlanDetailsVo.Status);
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@ExternalCode", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalCode);
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@ExternalType", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalType);
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_CreatedBy", DbType.Int32, userid);
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_ModifiedBy", DbType.Int32, userid);
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@XESExternal", DbType.String, mfProductAMCSchemePlanDetailsVo.SourceCode);
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsOnlineEnablement", DbType.Boolean, Convert.ToBoolean(mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement));
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@isETFL", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsETFT);//23


                // db.ExecuteNonQuery(updateSchemeSetUpDetailsCmd);
                if (db.ExecuteNonQuery(updateSchemeSetUpDetailsCmd) != 0)
                    blResult = true;
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
            DataSet dsOnlinebindRandT;
            DataTable dt;
            Database db;
            DbCommand OnlinebindRandTCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                OnlinebindRandTCmd = db.GetStoredProcCommand("SPROC_Onl_BindRandT");
                db.AddInParameter(OnlinebindRandTCmd, "@PASP_SchemPlaneCode", DbType.Int32, SchemPlaneCode);
                dsOnlinebindRandT = db.ExecuteDataSet(OnlinebindRandTCmd);
                dt = dsOnlinebindRandT.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:OnlinebindRandT()");
                object[] objects = new object[1];
                objects[0] = SchemPlaneCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dt;
        }
        public DataSet GetSystematicDetails(int schemeplancode)
        {
            Database db;
            DataSet dsSystematicDetails;
            DbCommand SystematicDetailscmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                SystematicDetailscmd = db.GetStoredProcCommand("SPROC_ONL_GetsystematicDetails");
                db.AddInParameter(SystematicDetailscmd, "@PASP_SchemePlanCode", DbType.Int32, schemeplancode);
                dsSystematicDetails = db.ExecuteDataSet(SystematicDetailscmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetSystematicDetails()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsSystematicDetails;
        }

        public bool CreateSystematicDetails(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int schemeplancode, int userId)
        {
            bool bResult = false;
            Database db;
            DbCommand CreateSystematicDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateSystematicDetailsCmd = db.GetStoredProcCommand("SPROC_ONL_CreatesystematicDetails");
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASP_SchemePlanCode", DbType.Int32, schemeplancode);
                db.AddInParameter(CreateSystematicDetailsCmd, "@XSTT_SystematicTypeCode", DbType.String, mfProductAMCSchemePlanDetailsVo.SystematicCode);
                db.AddInParameter(CreateSystematicDetailsCmd, "@XF_SystematicFrequencyCode", DbType.String, mfProductAMCSchemePlanDetailsVo.Frequency);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_StatingDates", DbType.String, mfProductAMCSchemePlanDetailsVo.StartDate);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_MinDues", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MinDues);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_MaxDues", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MaxDues);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_MinAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinAmount);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_MultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MultipleAmount);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_CreatedBy", DbType.Double, userId);
                if (db.ExecuteNonQuery(CreateSystematicDetailsCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:CreateSystematicDetails()");
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
        public bool EditSystematicDetails(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int schemeplancode, int systematicdetailsid, int userId)
        {
            bool blResult = false;
            Database db;
            DbCommand EditSystematicDetailscmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                EditSystematicDetailscmd = db.GetStoredProcCommand("SPROC_Onl_EditSystematicDetails");
                db.AddInParameter(EditSystematicDetailscmd, "@PASP_SchemePlanCode", DbType.Int32, schemeplancode);
                db.AddInParameter(EditSystematicDetailscmd, "@PASPSD_SystematicDetailsId", DbType.Int32, systematicdetailsid);
                db.AddInParameter(EditSystematicDetailscmd, "@XF_SystematicFrequencyCode", DbType.String, mfProductAMCSchemePlanDetailsVo.Frequency);
                db.AddInParameter(EditSystematicDetailscmd, "@PASPSD_StatingDates", DbType.String, mfProductAMCSchemePlanDetailsVo.StartDate);
                db.AddInParameter(EditSystematicDetailscmd, "@PASPSD_MinDues", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MinDues);
                db.AddInParameter(EditSystematicDetailscmd, "@PASPSD_MaxDues", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MaxDues);
                db.AddInParameter(EditSystematicDetailscmd, "@PASPSD_MinAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinAmount);
                db.AddInParameter(EditSystematicDetailscmd, "@PASPSD_MultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MultipleAmount);
                db.AddInParameter(EditSystematicDetailscmd, "@PASPSD_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(EditSystematicDetailscmd, "@XSTT_SystematicTypeCode", DbType.String, mfProductAMCSchemePlanDetailsVo.SystematicCode);
                db.ExecuteNonQuery(EditSystematicDetailscmd);
                if (db.ExecuteNonQuery(EditSystematicDetailscmd) != 0)
                    blResult = true;

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:EditSystematicDetails()");
                object[] objects = new object[2];
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return blResult;
        }


        public DataSet GetTradeBusinessDates()
        {
            DataSet dsGetTradeBusinessDate;
            Database db;
            DbCommand cmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmd = db.GetStoredProcCommand("SPROC_GetTradeBusinessDates");

                dsGetTradeBusinessDate = db.ExecuteDataSet(cmd);
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
            int affectedRecords = 0;
            bool bResult = false;
            Database db;
            DbCommand createtradeBusinessDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createtradeBusinessDateCmd = db.GetStoredProcCommand("SPROC_CreateTradeBusinessDate");
                //db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_TradeId", DbType.Int32, tradeBusinessDateVo.TradeBusinessId);
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_Date", DbType.DateTime, tradeBusinessDateVo.TradeBusinessDate);
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_ExecutionDate", DbType.DateTime, tradeBusinessDateVo.TradeBusinessExecutionDate);
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_isholiday", DbType.Int32, tradeBusinessDateVo.IsTradeBusinessDateHoliday);
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_isweekend", DbType.Int32, tradeBusinessDateVo.IsTradeBusinessDateWeekend);
                db.AddOutParameter(createtradeBusinessDateCmd, "@IsSuccess", DbType.Int16, 0);

                if (db.ExecuteNonQuery(createtradeBusinessDateCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(createtradeBusinessDateCmd, "@IsSuccess").ToString());
                if (affectedRecords == 1)
                    bResult = true;
                else
                    bResult = false;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:CreateTradeBusinessDate()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool updateTradeBusinessDate(int Tradebusinessid, string txt, DateTime date)
        {
            int affectedRecords = 0;
            bool bResult = false;
            Database db;
            DbCommand createtradeBusinessDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createtradeBusinessDateCmd = db.GetStoredProcCommand("SPROC_updateTradeBusinessDate");
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_TradeId", DbType.Int32, Tradebusinessid);
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_Date", DbType.DateTime, date);
                //db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_ExecutionDate", DbType.DateTime, TradeBusinessExecutionDate);
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_HolidayName", DbType.String, txt);
                db.AddOutParameter(createtradeBusinessDateCmd, "@IsSuccess", DbType.Int16, 0);

                if (db.ExecuteNonQuery(createtradeBusinessDateCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(createtradeBusinessDateCmd, "@IsSuccess").ToString());
                if (affectedRecords == 1)
                    bResult = true;
                else
                    bResult = false;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:updateTradeBusinessDate()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public bool deleteTradeBusinessDate(int TradeBusinessId)
        {
            int affectedRecords = 0;
            bool bResult = false;
            Database db;
            DbCommand createtradeBusinessDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createtradeBusinessDateCmd = db.GetStoredProcCommand("SPROC_deleteTradeBusinessDate");
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_TradeId", DbType.Int32, TradeBusinessId);
                //db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_Date", DbType.DateTime, tradeBusinessDateVo.TradeBusinessDate);
                db.AddOutParameter(createtradeBusinessDateCmd, "@IsSuccess", DbType.Int16, 0);
                if (db.ExecuteNonQuery(createtradeBusinessDateCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(createtradeBusinessDateCmd, "@IsSuccess").ToString());
                if (affectedRecords == 1)
                    bResult = true;
                else
                    bResult = false;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:deleteTradeBusinessDate()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }

        public void CreateCalendar(int year)
        {
            Database db;
            DbCommand createtradeBusinessDateCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createtradeBusinessDateCmd = db.GetStoredProcCommand("SPROC_CreateBusinessDayCalendar");
                db.AddInParameter(createtradeBusinessDateCmd, "@Year", DbType.Int32, year);
                db.ExecuteNonQuery(createtradeBusinessDateCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:CreateCalendar()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
        }


        public DataSet GetAdviserClientKYCStatusList(int adviserId, string filterOn, string clientCode)
        {

            Database db;
            DataSet dsAdviserClientKYCStatusList;
            DbCommand AdviserClientKYCStatusListcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AdviserClientKYCStatusListcmd = db.GetStoredProcCommand("SPROC_GetAdviserAllClientKYCStatus");
                db.AddInParameter(AdviserClientKYCStatusListcmd, "@AdviserId", DbType.Int32, adviserId);
                if (filterOn != null)
                {
                    db.AddInParameter(AdviserClientKYCStatusListcmd, "@filterOn", DbType.String, filterOn);
                }
                else
                {
                    db.AddInParameter(AdviserClientKYCStatusListcmd, "@filterOn", DbType.String, DBNull.Value);
                }
                if (clientCode != null)
                {
                    db.AddInParameter(AdviserClientKYCStatusListcmd, "@clientCode", DbType.String, clientCode);
                }
                else
                {
                    db.AddInParameter(AdviserClientKYCStatusListcmd, "@clientCode", DbType.String, DBNull.Value);
                }
                dsAdviserClientKYCStatusList = db.ExecuteDataSet(AdviserClientKYCStatusListcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "GetAdviserClientKYCStatusList(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserClientKYCStatusList;
        }
        public bool MakeTradeToHoliday(DateTime TradeBusinessDate, string datesToBeUpdated, TradeBusinessDateVo TradeBusinessDateVo)
        //int Id,DateTime dtDate)
        {
            bool bResult = false;
            Database db;
            DbCommand MakeTradeToHolidayCmd;

            try
            {
                //db = DatabaseFactory.CreateDatabase("wealtherp");
                //MakeTradeToHolidayCmd = db.GetStoredProcCommand("SPROC_MarkTradeBussinessHolidays");
                //db.AddInParameter(MakeTradeToHolidayCmd, "@WTBD_Date", DbType.DateTime, dtDate);


                //db.AddInParameter(MakeTradeToHolidayCmd, "@WTBD_Id", DbType.Int32, Id);

                //db.ExecuteNonQuery(MakeTradeToHolidayCmd);

                //bResult = true;
                db = DatabaseFactory.CreateDatabase("wealtherp");
                MakeTradeToHolidayCmd = db.GetStoredProcCommand("SPROC_MakeTradeBussinessHoliday");
                db.AddInParameter(MakeTradeToHolidayCmd, "@date", DbType.DateTime, TradeBusinessDate);
                db.AddInParameter(MakeTradeToHolidayCmd, "@datesToBeUpdated", DbType.String, datesToBeUpdated);
                db.AddInParameter(MakeTradeToHolidayCmd, "@WTBD_HolidayName", DbType.String, TradeBusinessDateVo.HolidayName);

                db.ExecuteNonQuery(MakeTradeToHolidayCmd);

                bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:MakeTradeToHoliday()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;
        }

        public DataSet GetOnlineNCDExtractPreview(DateTime date)
        {
            Database db;
            DataSet dsGetOnlineNCDExtractPreview;
            DbCommand GetOnlineNCDExtractPreviewcmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetOnlineNCDExtractPreviewcmd = db.GetStoredProcCommand("SPROC_PreviewNcdExtract");
                db.AddInParameter(GetOnlineNCDExtractPreviewcmd, "@Today", DbType.DateTime, date);
                dsGetOnlineNCDExtractPreview = db.ExecuteDataSet(GetOnlineNCDExtractPreviewcmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetOnlineNCDExtractPreview()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsGetOnlineNCDExtractPreview;
        }
        public DataSet GetAllTradeBussiness(int year, int holiday)
        {
            DataSet dsGetAllTradeBussiness;
            Database db;
            DbCommand GetAllTradeBussinessCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAllTradeBussinessCmd = db.GetStoredProcCommand("SPROC_GetAllTradeBussinessDay");
                db.AddInParameter(GetAllTradeBussinessCmd, "@year", DbType.Int32, year);
                if (holiday == 2)
                {
                    db.AddInParameter(GetAllTradeBussinessCmd, "@holiday", DbType.Int32, holiday);

                }
                else
                {
                    db.AddInParameter(GetAllTradeBussinessCmd, "@holiday", DbType.Int32, holiday);

                }
                dsGetAllTradeBussiness = db.ExecuteDataSet(GetAllTradeBussinessCmd);

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
            return dsGetAllTradeBussiness;
        }
        public int YearCheck(int year)
        {
            Database db;
            DataSet ds;
            DbCommand cmdYearCheck;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdYearCheck = db.GetStoredProcCommand("SPROC_CheckYear");
                db.AddInParameter(cmdYearCheck, "@year", DbType.Int32, year);
                db.AddOutParameter(cmdYearCheck, "@count", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdYearCheck);
                if (db.ExecuteNonQuery(cmdYearCheck) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdYearCheck, "count").ToString());
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
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                objects[0] = year;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }
        public bool Updateproductamcscheme(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int SchemePlanCode, int userid)
        {
            bool blResult = false;
            Database db;
            DbCommand UpdateproductamcschemeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateproductamcschemeCmd = db.GetStoredProcCommand("SPROC_Onl_UpdateSchemeplan");
                db.AddInParameter(UpdateproductamcschemeCmd, "@SchemePlancode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SchemePlanCode);
                db.AddInParameter(UpdateproductamcschemeCmd, "@Amc", DbType.Int32, mfProductAMCSchemePlanDetailsVo.AMCCode);
                db.AddInParameter(UpdateproductamcschemeCmd, "@Category", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetCategoryCode);
                db.AddInParameter(UpdateproductamcschemeCmd, "@SubCategory", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode);
                db.AddInParameter(UpdateproductamcschemeCmd, "@SubSubCategory", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory);
                db.AddInParameter(UpdateproductamcschemeCmd, "@SchemeName", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemePlanName);
                db.AddInParameter(UpdateproductamcschemeCmd, "@Status", DbType.String, mfProductAMCSchemePlanDetailsVo.Status);
                db.AddInParameter(UpdateproductamcschemeCmd, "@Isonline", DbType.String, mfProductAMCSchemePlanDetailsVo.IsOnline);
                db.AddInParameter(UpdateproductamcschemeCmd, "@ExternalCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AMFIcode);
                //db.AddInParameter(UpdateproductamcschemeCmd, "@ExternalType", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalType);@
                if (mfProductAMCSchemePlanDetailsVo.NFOStartDate != DateTime.MinValue)
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@PASP_NFOStartDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.NFOStartDate);
                }
                else
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@PASP_NFOStartDate", DbType.DateTime, DBNull.Value);
                }
                if (mfProductAMCSchemePlanDetailsVo.NFOEndDate != DateTime.MinValue)
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@PASP_NFOEndDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.NFOEndDate);
                }
                else
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@PASP_NFOEndDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(UpdateproductamcschemeCmd, "@PASP_CreatedBy", DbType.Int32, userid);
                db.AddInParameter(UpdateproductamcschemeCmd, "@PASP_ModifiedBy", DbType.Int32, userid);
                db.AddInParameter(UpdateproductamcschemeCmd, "@XESExternal", DbType.String, mfProductAMCSchemePlanDetailsVo.SourceCode);
                if (mfProductAMCSchemePlanDetailsVo.SchemeStartDate != DateTime.MinValue) //10
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@SchemeOpenDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.SchemeStartDate);
                }
                else
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@SchemeOpenDate", DbType.DateTime, DBNull.Value);
                }
                if (mfProductAMCSchemePlanDetailsVo.MaturityDate != DateTime.MinValue) //10
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@MaturityDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.MaturityDate);
                }
                else
                {
                    db.AddInParameter(UpdateproductamcschemeCmd, "@MaturityDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(UpdateproductamcschemeCmd, "@ISINNo", DbType.String, mfProductAMCSchemePlanDetailsVo.ISINNo);

                db.ExecuteNonQuery(UpdateproductamcschemeCmd);
                if (db.ExecuteNonQuery(UpdateproductamcschemeCmd) != 0)
                    blResult = true;
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
            //int schemeplancode = 0;


            Database db;
            DbCommand CreateOnlineSchemeSetupPlanCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateOnlineSchemeSetupPlanCmd = db.GetStoredProcCommand("SPROC_CreateOnlineschemebasicdetail");
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PA_AMCCode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.AMCCode);
                // db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_SchemePlanCode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SchemePlanCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_SchemePlanName", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemePlanName);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PAISSC_AssetInstrumentSubSubCategoryCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetSubSubCategory);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetSubCategoryCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AssetCategoryCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PAG_AssetGroupCode", DbType.String, mfProductAMCSchemePlanDetailsVo.Product);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_Status", DbType.String, mfProductAMCSchemePlanDetailsVo.Status);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_IsOnline", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsOnline);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_IsDirect", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsDirect);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_ModifiedBy", DbType.Int32, userId);
                db.AddOutParameter(CreateOnlineSchemeSetupPlanCmd, "@SchemePlanCode", DbType.Int32, 0);
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@ExternalCode", DbType.String, mfProductAMCSchemePlanDetailsVo.AMFIcode);
                //db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@ExternalType", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalType);
                if (mfProductAMCSchemePlanDetailsVo.NFOStartDate != DateTime.MinValue) //10
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_NFOStartDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.NFOStartDate);
                }
                else
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_NFOStartDate", DbType.DateTime, DBNull.Value);
                }
                if (mfProductAMCSchemePlanDetailsVo.NFOEndDate != DateTime.MinValue)//11
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_NFOEndDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.NFOEndDate);
                }
                else
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@PASP_NFOEndDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@XESExternal", DbType.String, mfProductAMCSchemePlanDetailsVo.SourceCode);

                //db.ExecuteNonQuery(CreateOnlineSchemeSetupPlanCmd);
                if (mfProductAMCSchemePlanDetailsVo.SchemeStartDate != DateTime.MinValue) //10
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@SchemeOpenDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.SchemeStartDate);
                }
                else
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@SchemeOpenDate", DbType.DateTime, DBNull.Value);
                }
                if (mfProductAMCSchemePlanDetailsVo.MaturityDate != DateTime.MinValue) //10
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@MaturityDate", DbType.DateTime, mfProductAMCSchemePlanDetailsVo.MaturityDate);
                }
                else
                {
                    db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@MaturityDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(CreateOnlineSchemeSetupPlanCmd, "@ISINNo", DbType.String, mfProductAMCSchemePlanDetailsVo.ISINNo);

                if (db.ExecuteNonQuery(CreateOnlineSchemeSetupPlanCmd) != 0)
                    schemeplancode = int.Parse(db.GetParameterValue(CreateOnlineSchemeSetupPlanCmd, "@SchemePlanCode").ToString());
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
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }

        }
        public void CreateOnlineSchemeSetupPlanDetails(MFProductAMCSchemePlanDetailsVo mfProductAMCSchemePlanDetailsVo, int userId)
        {
            //int schemeplancode = 0;


            Database db;
            DbCommand CreateOnlineSchemeSetupPlanDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateOnlineSchemeSetupPlanDetailsCmd = db.GetStoredProcCommand("SPROC_Toinsertproducctamcdetail");
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASP_SchemePlanCode", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SchemePlanCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@ExternalCode", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@ExternalType", DbType.String, mfProductAMCSchemePlanDetailsVo.ExternalType);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_FaceValue", DbType.Double, mfProductAMCSchemePlanDetailsVo.FaceValue);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PSLV_LookupValueCodeForSchemeType", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemeType);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PSLV_LookupValueCodeForSchemeOption", DbType.String, mfProductAMCSchemePlanDetailsVo.SchemeOption);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@DividentType", DbType.String, mfProductAMCSchemePlanDetailsVo.DividendFrequency);
                //db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_BankName", DbType.String, mfProductAMCSchemePlanDetailsVo.BankName);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_AccountNumber", DbType.String, mfProductAMCSchemePlanDetailsVo.AccountNumber);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_Branch", DbType.String, mfProductAMCSchemePlanDetailsVo.Branch);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@WCMV_Lookup_BankId", DbType.Int32, mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId);
                //db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsNFO", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsNFO);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_LockInPeriod", DbType.Int32, mfProductAMCSchemePlanDetailsVo.LockInPeriod);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_CutOffTime", DbType.String, mfProductAMCSchemePlanDetailsVo.CutOffTime.ToString());
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_EntryLoadPercentage", DbType.Double, mfProductAMCSchemePlanDetailsVo.EntryLoadPercentag);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_EntryLoadRemark", DbType.String, mfProductAMCSchemePlanDetailsVo.EntryLoadRemark);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_ExitLoadPercentage", DbType.Double, mfProductAMCSchemePlanDetailsVo.ExitLoadPercentage);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_ExitLoadRemark", DbType.String, mfProductAMCSchemePlanDetailsVo.ExitLoadRemark);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsPurchaseAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsPurchaseAvailable);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsRedeemAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsRedeemAvailable);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsSIPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSIPAvailable);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsSWPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSWPAvailable);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsSwitchAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSwitchAvailable);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsSTPAvailable", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsSTPAvailable);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_InitialPurchaseAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.InitialPurchaseAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_InitialMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.InitialMultipleAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_AdditionalPruchaseAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.AdditionalPruchaseAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_AdditionalMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.AdditionalMultipleAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_MinRedemptionAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinRedemptionAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_RedemptionMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.RedemptionMultipleAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_MinRedemptionUnits", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinRedemptionUnits);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_RedemptionMultiplesUnits", DbType.Double, mfProductAMCSchemePlanDetailsVo.RedemptionMultiplesUnits);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_MinSwitchAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.MinSwitchAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_SwitchMultipleAmount", DbType.Double, mfProductAMCSchemePlanDetailsVo.SwitchMultipleAmount);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_MinSwitchUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.MinSwitchUnits);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_SwitchMultiplesUnits", DbType.Int32, mfProductAMCSchemePlanDetailsVo.SwitchMultiplesUnits);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@XF_FileGenerationFrequency", DbType.String, mfProductAMCSchemePlanDetailsVo.GenerationFrequency);

                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@XCST_CustomerSubTypeCode", DbType.String, mfProductAMCSchemePlanDetailsVo.CustomerSubTypeCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_SecurityCode", DbType.String, mfProductAMCSchemePlanDetailsVo.SecurityCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_MaxInvestment", DbType.Double, mfProductAMCSchemePlanDetailsVo.PASPD_MaxInvestment);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@WCMV_Lookup_BankId", DbType.Int32, mfProductAMCSchemePlanDetailsVo.WCMV_Lookup_BankId);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@XESExternal", DbType.String, mfProductAMCSchemePlanDetailsVo.SourceCode);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@PASPD_IsOnlineEnablement", DbType.String, mfProductAMCSchemePlanDetailsVo.IsOnlineEnablement);
                db.AddInParameter(CreateOnlineSchemeSetupPlanDetailsCmd, "@isETFL", DbType.Int32, mfProductAMCSchemePlanDetailsVo.IsETFT);

                db.ExecuteNonQuery(CreateOnlineSchemeSetupPlanDetailsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
        }

        public DataTable GetSchemeForMarge(int AmcCode, int Schemeplanecode, string Type)
        {
            Database db;
            DbCommand cmdGetSchemeForMarge;
            DataTable dtGetSchemeForMarge;
            DataSet dsGetSchemeForMarge = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetSchemeForMarge = db.GetStoredProcCommand("SPROC_GetSchemeName");
                db.AddInParameter(cmdGetSchemeForMarge, "@AMCCode", DbType.Int32, AmcCode);
                db.AddInParameter(cmdGetSchemeForMarge, "@SchemePlanCode", DbType.Int32, Schemeplanecode);
                db.AddInParameter(cmdGetSchemeForMarge, "@Type", DbType.String, Type);
                dsGetSchemeForMarge = db.ExecuteDataSet(cmdGetSchemeForMarge);
                dtGetSchemeForMarge = dsGetSchemeForMarge.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetSchemeForMarge;
        }
        public bool CreateMargeScheme(int SchemePlaneCode, int MargeScheme, DateTime Date, int UserId)
        {
            bool bResult = false;
            Database db;
            DbCommand CreateMargeSchemeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateMargeSchemeCmd = db.GetStoredProcCommand("SPROC_CreateMargeScheme");
                db.AddInParameter(CreateMargeSchemeCmd, "@SchemePlanCode", DbType.Int32, SchemePlaneCode);
                db.AddInParameter(CreateMargeSchemeCmd, "@MargeSchemeCode", DbType.Int32, MargeScheme);
                db.AddInParameter(CreateMargeSchemeCmd, "@MargeDate", DbType.Date, Date);
                db.AddInParameter(CreateMargeSchemeCmd, "@Modifiedby", DbType.Int32, UserId);
                if (db.ExecuteNonQuery(CreateMargeSchemeCmd) != 0)
                    bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateUserrole()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public int BussinessDateCheck(DateTime Date)
        {
            Database db;
            DataSet ds;
            DbCommand cmdBussinessDateCheck;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdBussinessDateCheck = db.GetStoredProcCommand("SPROC_ToCheckBussinessDate");
                db.AddInParameter(cmdBussinessDateCheck, "@Date", DbType.Date, Date);
                db.AddOutParameter(cmdBussinessDateCheck, "@count", DbType.Int32, 0);
                if (db.ExecuteScalar(cmdBussinessDateCheck) != null)
                    count = Convert.ToInt32(db.ExecuteScalar(cmdBussinessDateCheck).ToString());
                //ds = db.ExecuteDataSet(cmdBussinessDateCheck);
                //if (db.ExecuteNonQuery(cmdBussinessDateCheck) != 0)
                //{
                //    count = Convert.ToInt32(db.GetParameterValue(cmdBussinessDateCheck, "count").ToString());
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return count;
        }
        public String SchemeStatus(int schemeplanecode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdSchemeStatus;
            string status = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdSchemeStatus = db.GetStoredProcCommand("SPROC_GETStatus");
                db.AddInParameter(cmdSchemeStatus, "@Schemeplancode", DbType.Int32, schemeplanecode);
                db.AddInParameter(cmdSchemeStatus, "@Status", DbType.String, 10);
                if (db.ExecuteScalar(cmdSchemeStatus) != null)
                    status = db.ExecuteScalar(cmdSchemeStatus).ToString();
                //ds = db.ExecuteDataSet(cmdBussinessDateCheck);
                //if (db.ExecuteNonQuery(cmdBussinessDateCheck) != 0)
                //{
                //    count = Convert.ToInt32(db.GetParameterValue(cmdBussinessDateCheck, "count").ToString());
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return status;
        }
        public DataTable GetMergeScheme(int Schemeplanecode)
        {
            Database db;
            DbCommand cmdGetMergeScheme;
            DataTable dtGetMergeScheme;
            DataSet dsGetMergeScheme = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetMergeScheme = db.GetStoredProcCommand("SPROC_GETMergeScheme");
                db.AddInParameter(cmdGetMergeScheme, "@SchemePlanCode", DbType.Int32, Schemeplanecode);
                dsGetMergeScheme = db.ExecuteDataSet(cmdGetMergeScheme);
                dtGetMergeScheme = dsGetMergeScheme.Tables[0];

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetMergeScheme;
        }
        public String DividentType(int schemeplanecode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdDividentType;
            string Type = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdDividentType = db.GetStoredProcCommand("SPROC_GET_Type");
                db.AddInParameter(cmdDividentType, "@Schemeplancode", DbType.Int32, schemeplanecode);
                db.AddOutParameter(cmdDividentType, "@Type", DbType.String, 10);
                if (db.ExecuteScalar(cmdDividentType) != null)
                    Type = db.ExecuteScalar(cmdDividentType).ToString();
                //ds = db.ExecuteDataSet(cmdBussinessDateCheck);
                //if (db.ExecuteNonQuery(cmdBussinessDateCheck) != 0)
                //{
                //    count = Convert.ToInt32(db.GetParameterValue(cmdBussinessDateCheck, "count").ToString());
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return Type;
        }
        public DataSet GetAdviserCustomersAllMFAccounts(int IsValued, int advisorId)
        {
            Database db;
            DataSet dsAdviserCustomersAllMFAccounts;
            DbCommand AdviserCustomersAllMFAccountsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                AdviserCustomersAllMFAccountsCmd = db.GetStoredProcCommand("SPROC_Onl_GetAdviserCustomersAllMFAccount");
                db.AddInParameter(AdviserCustomersAllMFAccountsCmd, "@IsValued", DbType.Int32, IsValued);
                db.AddInParameter(AdviserCustomersAllMFAccountsCmd, "@AdvisorId", DbType.Int32, advisorId);
                dsAdviserCustomersAllMFAccounts = db.ExecuteDataSet(AdviserCustomersAllMFAccountsCmd);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetAdviserCustomersAllMFAccounts()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return dsAdviserCustomersAllMFAccounts;
        }
        public void UpdateAdviserCustomersAllMFAccounts(string gvMFAId, int ModifiedBy)
        {
            Database db;

            DbCommand UpdateAdviserCustomersAllMFAccountsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAdviserCustomersAllMFAccountsCmd = db.GetStoredProcCommand("SPROC_Onl_MarkMFAccountReValuation");
                db.AddInParameter(UpdateAdviserCustomersAllMFAccountsCmd, "@MFAIdString", DbType.String, gvMFAId);
                db.AddInParameter(UpdateAdviserCustomersAllMFAccountsCmd, "@ModifiedBy", DbType.Int32, ModifiedBy);
                db.ExecuteNonQuery(UpdateAdviserCustomersAllMFAccountsCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateAdviserCustomersAllMFAccounts(string gvMFAId)");
                object[] objects = new object[1];
                objects[0] = gvMFAId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
        }
        public DataSet Getproductcode(int schemeplancode)
        {
            Database db;
            DataSet dsGetproductcode;
            DbCommand Getproductcodecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                Getproductcodecmd = db.GetStoredProcCommand("SPROC_GetProductCode");
                db.AddInParameter(Getproductcodecmd, "@SchemePlanCode", DbType.Int32, schemeplancode);
                dsGetproductcode = db.ExecuteDataSet(Getproductcodecmd);
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
            return dsGetproductcode;
        }
        public String GetProductAddedCode(int schemeplanecode)
        {
            Database db;
            DataSet dsGetProductAddedCode;
            DbCommand cmdGetProductAddedCode;
            string Productcode = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProductAddedCode = db.GetStoredProcCommand("SPROC_GetOfflineProductCode");
                db.AddInParameter(cmdGetProductAddedCode, "@Schemeplancode", DbType.Int32, schemeplanecode);
                dsGetProductAddedCode = db.ExecuteDataSet(cmdGetProductAddedCode);
                if (db.ExecuteScalar(cmdGetProductAddedCode) != null)
                    Productcode = db.ExecuteScalar(cmdGetProductAddedCode).ToString();
                //ds = db.ExecuteDataSet(cmdBussinessDateCheck);
                //if (db.ExecuteNonQuery(cmdBussinessDateCheck) != 0)
                //{
                //    count = Convert.ToInt32(db.GetParameterValue(cmdBussinessDateCheck, "count").ToString());
                //}
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return Productcode;
        }
        public bool Createproductcode(int Schemeplancode, string Productcode, string Externaltype, string XESSourcecode, int Userid)
        {
            bool bResult = false;
            Database db;
            DbCommand CreateproductcodeCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateproductcodeCmd = db.GetStoredProcCommand("SPROC_InsertProductCode");
                db.AddInParameter(CreateproductcodeCmd, "@Schemeplancode", DbType.Int32, Schemeplancode);
                db.AddInParameter(CreateproductcodeCmd, "@Productcode", DbType.String, Productcode);
                db.AddInParameter(CreateproductcodeCmd, "@ExternalType", DbType.String, Externaltype);
                db.AddInParameter(CreateproductcodeCmd, "@XESSoursecode", DbType.String, XESSourcecode);
                db.AddInParameter(CreateproductcodeCmd, "@Createdby", DbType.Int32, Userid);
                db.AddInParameter(CreateproductcodeCmd, "@Modifiedby", DbType.Int32, Userid);
                if (db.ExecuteNonQuery(CreateproductcodeCmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();

                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:CreateproductcodeCmd()");

                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public bool UpdateProductcode(int Productamcdetailid, string Productcode, int userid)
        {
            bool bResult = false;
            Database db;
            DbCommand UpdateProductcodedCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateProductcodedCmd = db.GetStoredProcCommand("SPROC_UpdateProductCode");
                db.AddInParameter(UpdateProductcodedCmd, "@Schemeplamappedcode", DbType.Int32, Productamcdetailid);
                db.AddInParameter(UpdateProductcodedCmd, "@Productcode", DbType.String, Productcode);
                db.AddInParameter(UpdateProductcodedCmd, "@Createdby", DbType.Int32, userid);
                db.AddInParameter(UpdateProductcodedCmd, "@Modifiedby", DbType.Int32, userid);
                if (db.ExecuteNonQuery(UpdateProductcodedCmd) != 0)
                    bResult = true;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateUserrole()");
                object[] objects = new object[3];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
        }
        public String ExternalCode(string Externaltype)
        {
            Database db;
            DataSet ds;
            DbCommand cmdExternalCode;
            string Type = "";
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdExternalCode = db.GetStoredProcCommand("SPROC_GETExternalSOurceType");
                db.AddInParameter(cmdExternalCode, "@Externaltype", DbType.String, Externaltype);
                db.AddOutParameter(cmdExternalCode, "@type", DbType.String, 10);
                if (db.ExecuteScalar(cmdExternalCode) != null)
                    Type = db.ExecuteScalar(cmdExternalCode).ToString();

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
            Database db;
            DbCommand GetSchemecodecmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetSchemecodecmd = db.GetStoredProcCommand("SPROC_Getschemedetails");
                db.AddInParameter(GetSchemecodecmd, "@schemeplancode", DbType.Int32, schemeplancode);
                db.AddOutParameter(GetSchemecodecmd, "@schemecode", DbType.Int32, 10);
                if (db.ExecuteScalar(GetSchemecodecmd).ToString() != string.Empty)
                    schemecode = Convert.ToInt32(db.ExecuteScalar(GetSchemecodecmd).ToString());
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "AssociateDAO.cs:CodeduplicateChack()");
                object[] objects = new object[2];
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return schemecode;
        }
        public DataTable GetSchemeLookupType(string dividentType)
        {
            Database db;
            DbCommand cmdGetSchemeLookupType;
            DataTable dtGetSchemeLookupType;
            DataSet dsGetSchemeLookupType = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");

                //To retreive data from the table 
                cmdGetSchemeLookupType = db.GetStoredProcCommand("SPROC_GetSchemeLookupValue");
                db.AddInParameter(cmdGetSchemeLookupType, "@DividentType", DbType.String, dividentType);
                dsGetSchemeLookupType = db.ExecuteDataSet(cmdGetSchemeLookupType);
                dtGetSchemeLookupType = dsGetSchemeLookupType.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetSchemeLookupType;
        }
        public DataTable GetRTAInitialReport(string type, DateTime fromDate, DateTime toDate)
        {
            Database db;
            DbCommand cmdGetRTAInitialReport;
            DataTable dtGetRTAInitialReport;
            DataSet dsGetRTAInitialReport = null;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetRTAInitialReport = db.GetStoredProcCommand("SPROC_GetInitialRTAReport");
                db.AddInParameter(cmdGetRTAInitialReport, "@AMCWise", DbType.String, type);
                db.AddInParameter(cmdGetRTAInitialReport, "@Fromdate", DbType.DateTime, fromDate);
                db.AddInParameter(cmdGetRTAInitialReport, "@Todate", DbType.DateTime, toDate);
                cmdGetRTAInitialReport.CommandTimeout = 60 * 60;
                dsGetRTAInitialReport = db.ExecuteDataSet(cmdGetRTAInitialReport);
                dtGetRTAInitialReport = dsGetRTAInitialReport.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetRTAInitialReport;
        }
        public DataTable GetAMCListRNTWise(string RNTType)
        {
            DataSet dsGetAMCListRNTWise;
            DataTable dtGetAMCListRNTWise;
            Database db;
            DbCommand cmdGetAMCListRNTWise;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetAMCListRNTWise = db.GetStoredProcCommand("SPROC_GetAMCListRNTWise");
                db.AddInParameter(cmdGetAMCListRNTWise, "@RNTType", DbType.String, RNTType);
                dsGetAMCListRNTWise = db.ExecuteDataSet(cmdGetAMCListRNTWise);
                dtGetAMCListRNTWise = dsGetAMCListRNTWise.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetAMCListRNTWise;
        }
        public DataTable GetSubBrokerCodeCleansing( int AMCCode, int schemePlanCode, int adviserId, int subBrokerCode)
        {
            DataSet dsGetSubBrokerCodeCleansing;
            DataTable dtGetSubBrokerCodeCleansing;
            Database db;
            DbCommand cmdGetSubBrokerCodeCleansing;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSubBrokerCodeCleansing = db.GetStoredProcCommand("SPROC_GetSubBrokerCleansing");
                if (schemePlanCode != 0)
                    db.AddInParameter(cmdGetSubBrokerCodeCleansing, "@SchemePlanCode", DbType.Int32, schemePlanCode);
                else
                    db.AddInParameter(cmdGetSubBrokerCodeCleansing, "@SchemePlanCode", DbType.Int32, DBNull.Value);
                if (AMCCode != 0)
                    db.AddInParameter(cmdGetSubBrokerCodeCleansing, "@AmcCode", DbType.Int32, AMCCode);
                else
                    db.AddInParameter(cmdGetSubBrokerCodeCleansing, "@AmcCode", DbType.Int32, DBNull.Value);
                db.AddInParameter(cmdGetSubBrokerCodeCleansing, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetSubBrokerCodeCleansing, "@subBrokerType", DbType.Int32, subBrokerCode);
                dsGetSubBrokerCodeCleansing = db.ExecuteDataSet(cmdGetSubBrokerCodeCleansing);
                dtGetSubBrokerCodeCleansing = dsGetSubBrokerCodeCleansing.Tables[0];
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
            Database db;
            DbCommand UpdateSubBrokerCodeCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateSubBrokerCodeCmd = db.GetStoredProcCommand("SPROC_UpdateSubBrokerCode");
                db.AddInParameter(UpdateSubBrokerCodeCmd, "@transactionId", DbType.String, transactionId);
                db.AddInParameter(UpdateSubBrokerCodeCmd, "@subBrokerCode", DbType.String, subBrokerCode);
                if (db.ExecuteNonQuery(UpdateSubBrokerCodeCmd) != 0)

                    bResult = true;
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
            Database db;
            DbCommand UpdateNewSubBrokerCode;
            DataSet dsUpdateNewSubBrokerCode = new DataSet();

            try
            {
                dsUpdateNewSubBrokerCode.Tables.Add(dtSubBrokerCode.Copy());
                dsUpdateNewSubBrokerCode.DataSetName = "SubBrokerCodeDS";
                dsUpdateNewSubBrokerCode.Tables[0].TableName = "SubBrokerCodeDT";
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateNewSubBrokerCode = db.GetStoredProcCommand("SPROC_UpdateNewSubBrokerCode");
                db.AddInParameter(UpdateNewSubBrokerCode, "@XMLSubBrokerCode", DbType.Xml, dsUpdateNewSubBrokerCode.GetXml().ToString());
                if (db.ExecuteNonQuery(UpdateNewSubBrokerCode) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }
        public int SchemeCode(string externalcode, int AMCCode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdSchemeCode;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdSchemeCode = db.GetStoredProcCommand("SPROC_GetSchemeCodeOnlineOffline");
                db.AddInParameter(cmdSchemeCode, "@Externalcode", DbType.String, externalcode);
                db.AddInParameter(cmdSchemeCode, "@AMCCode", DbType.Int32, AMCCode);
                db.AddOutParameter(cmdSchemeCode, "@count", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdSchemeCode);
                if (db.ExecuteNonQuery(cmdSchemeCode) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdSchemeCode, "count").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return count;
        }
        public string GetExternalCode(int AMCCode, int productmappingcode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdGetExternalCode;
            string extCode = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetExternalCode = db.GetStoredProcCommand("SPROC_GetSchemeCode");
                db.AddInParameter(cmdGetExternalCode, "@AMCCode", DbType.Int32, AMCCode);
                db.AddInParameter(cmdGetExternalCode, "@productmappingcode", DbType.Int32, productmappingcode);

                db.AddOutParameter(cmdGetExternalCode, "@PASC_AMC_ExternalCode", DbType.String, 20);
                ds = db.ExecuteDataSet(cmdGetExternalCode);
                if (db.ExecuteNonQuery(cmdGetExternalCode) != 0)
                {
                    extCode = db.GetParameterValue(cmdGetExternalCode, "PASC_AMC_ExternalCode").ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return extCode;
        }
        public DataTable SearchOnPRoduct(int orderNo, int applicationNo)
        {
            DataSet dsSearchOnPRoduct;
            DataTable dtSearchOnPRoduct;
            Database db;
            DbCommand cmdSearchOnPRoduct;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdSearchOnPRoduct = db.GetStoredProcCommand("SPROC_ProductWiseSearch");
                db.AddInParameter(cmdSearchOnPRoduct, "@OrderNo", DbType.Int32, orderNo);
                db.AddInParameter(cmdSearchOnPRoduct, "@applicationNo", DbType.Int32, applicationNo);
                dsSearchOnPRoduct = db.ExecuteDataSet(cmdSearchOnPRoduct);
                dtSearchOnPRoduct = dsSearchOnPRoduct.Tables[0];
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
            Database db;
            DbCommand GetAMCListCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                GetAMCListCmd = db.GetStoredProcCommand("SPROC_GetProductAMCList");
                ds = db.ExecuteDataSet(GetAMCListCmd);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:GetAMCList()");
                object[] objects = new object[1];

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
            Database db;
            DbCommand CreateAMCCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateAMCCmd = db.GetStoredProcCommand("SPROC_CreateAMC");
                db.AddInParameter(CreateAMCCmd, "@amcName", DbType.String, amcName);
                db.AddInParameter(CreateAMCCmd, "@isOnline", DbType.Int32, isOnline);
                db.AddInParameter(CreateAMCCmd, "@userId", DbType.Int32, userId);
                db.AddInParameter(CreateAMCCmd, "@AmcCode", DbType.String, AmcCode);
                db.ExecuteNonQuery(CreateAMCCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }


        public bool UpdateAMC(string amcName, int isOnline, int userId, int amcCode, string AmcCode)
        {
            bool blResult = false;
            Database db;
            DbCommand UpdateAMCCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                UpdateAMCCmd = db.GetStoredProcCommand("SPROC_UpdateAMC");
                db.AddInParameter(UpdateAMCCmd, "@amcName", DbType.String, amcName); //1
                db.AddInParameter(UpdateAMCCmd, "@isOnline", DbType.Int32, isOnline); //2
                db.AddInParameter(UpdateAMCCmd, "@userId", DbType.Int32, userId);//3
                db.AddInParameter(UpdateAMCCmd, "@amcCode", DbType.Int32, amcCode);
                db.AddInParameter(UpdateAMCCmd, "@AmcCodes", DbType.String, AmcCode);
                // db.ExecuteNonQuery(updateSchemeSetUpDetailsCmd);
                if (db.ExecuteNonQuery(UpdateAMCCmd) != 0)
                    blResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:UpdateAMC()");
                object[] objects = new object[3];
                objects[0] = amcName;
                objects[1] = isOnline;
                objects[2] = userId;
                objects[3] = amcCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return blResult;
        }

        public bool deleteAMC(int amcCode)
        {
            int affectedRecords = 0;
            bool bResult = false;
            Database db;
            DbCommand CreateAMCCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateAMCCmd = db.GetStoredProcCommand("SPROC_DeleteAMC");
                db.AddInParameter(CreateAMCCmd, "@amcCode", DbType.Int32, amcCode);
                if (db.ExecuteNonQuery(CreateAMCCmd) != 0)
                    affectedRecords = int.Parse(db.GetParameterValue(CreateAMCCmd, "@amcCode").ToString());
                if (affectedRecords == 1)
                    bResult = true;
                else
                    bResult = false;
            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineOrderBackOfficeDao.cs:deleteAMC()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return bResult;

        }
        public DataTable GetUTIAMCDetails(int adviserId, DateTime fromDate, DateTime toDate)
        {
            DataSet dsGetUTIAMCDetails;
            DataTable dtGetUTIAMCDetails;
            Database db;
            DbCommand cmdGetUTIAMCDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetUTIAMCDetails = db.GetStoredProcCommand("SPROC_GetUTISchemeDetais");
                db.AddInParameter(cmdGetUTIAMCDetails, "@adviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetUTIAMCDetails, "@Fromdate", DbType.DateTime, fromDate);
                db.AddInParameter(cmdGetUTIAMCDetails, "@Todate", DbType.DateTime, toDate);
                dsGetUTIAMCDetails = db.ExecuteDataSet(cmdGetUTIAMCDetails);
                dtGetUTIAMCDetails = dsGetUTIAMCDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetUTIAMCDetails;
        }
        public bool ProductcodeDelete(int ScheneMappingId)
        {
            bool bResult = false;
            Database db;
            DbCommand ProductcodeDeletecmd;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                ProductcodeDeletecmd = db.GetStoredProcCommand("SPROC_DeleteProductCode");
                db.AddInParameter(ProductcodeDeletecmd, "@ScheneMappingId", DbType.Int32, ScheneMappingId);
                if (db.ExecuteNonQuery(ProductcodeDeletecmd) != 0)
                    bResult = true;
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
            Database db;
            DbCommand insertUpdateDeleteOnBannerDetailscmd;
            try
            {

                db = DatabaseFactory.CreateDatabase("wealtherp");
                insertUpdateDeleteOnBannerDetailscmd = db.GetStoredProcCommand("SPROC_InsertUpdateDeleteOnBannerDetails");
                db.AddInParameter(insertUpdateDeleteOnBannerDetailscmd, "@AssetGroupCode", DbType.String, assetGroupCode);
                db.AddInParameter(insertUpdateDeleteOnBannerDetailscmd, "@UserId", DbType.Int32, userId);
                db.AddInParameter(insertUpdateDeleteOnBannerDetailscmd, "@ImageName", DbType.String, imageName);
                db.AddInParameter(insertUpdateDeleteOnBannerDetailscmd, "@ExpiryDate", DbType.DateTime, expiryDate);
                db.AddInParameter(insertUpdateDeleteOnBannerDetailscmd, "@ID", DbType.Int32, id);
                db.AddInParameter(insertUpdateDeleteOnBannerDetailscmd, "@IsDelete", DbType.Int32, isDelete);
                if (db.ExecuteNonQuery(insertUpdateDeleteOnBannerDetailscmd) != 0)
                    bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public DataTable GetBannerDetailsWithAssetGroup()
        {
            DataSet dsGetBannerDetailsWithAssetGroup;
            DataTable dtGetBannerDetailsWithAssetGroup;
            Database db;
            DbCommand cmdGetBannerDetailsWithAssetGroup;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetBannerDetailsWithAssetGroup = db.GetStoredProcCommand("SPROC_GetBannerDetailsWithAssetGroup");
                //db.AddInParameter(cmdGetUTIAMCDetails, "@adviserId", DbType.Int32, adviserId);

                dsGetBannerDetailsWithAssetGroup = db.ExecuteDataSet(cmdGetBannerDetailsWithAssetGroup);
                dtGetBannerDetailsWithAssetGroup = dsGetBannerDetailsWithAssetGroup.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetBannerDetailsWithAssetGroup;
        }
        public int SchemeCodeonline(string externalcode, int AMCCode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdSchemeCode;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdSchemeCode = db.GetStoredProcCommand("SPROC_GetSchemeCodeOnline");
                db.AddInParameter(cmdSchemeCode, "@Externalcode", DbType.String, externalcode);
                db.AddInParameter(cmdSchemeCode, "@AMCCode", DbType.Int32, AMCCode);
                db.AddOutParameter(cmdSchemeCode, "@count", DbType.Int32, 0);

                ds = db.ExecuteDataSet(cmdSchemeCode);
                if (db.ExecuteNonQuery(cmdSchemeCode) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdSchemeCode, "count").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return count;
        }
        public string GetExternalCodeOnline(int AMCCode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdGetExternalCode;
            string extCode = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetExternalCode = db.GetStoredProcCommand("SPROC_GetSchemeCodeOnlinescheme");
                db.AddInParameter(cmdGetExternalCode, "@AMCCode", DbType.Int32, AMCCode);
                db.AddOutParameter(cmdGetExternalCode, "@PASC_AMC_ExternalCode", DbType.String, 20);
                ds = db.ExecuteDataSet(cmdGetExternalCode);
                if (db.ExecuteNonQuery(cmdGetExternalCode) != 0)
                {
                    extCode = db.GetParameterValue(cmdGetExternalCode, "PASC_AMC_ExternalCode").ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return extCode;
        }
        public int GetAMCCode(string AMCCode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdGetAMCCode;
            int count = 0;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdGetAMCCode = db.GetStoredProcCommand("SPROC_AMCCodeCheck");
                db.AddInParameter(cmdGetAMCCode, "@AMCCode", DbType.String, AMCCode);
                db.AddOutParameter(cmdGetAMCCode, "@count", DbType.Int32, 0);
                ds = db.ExecuteDataSet(cmdGetAMCCode);
                if (db.ExecuteNonQuery(cmdGetAMCCode) != 0)
                {
                    count = Convert.ToInt32(db.GetParameterValue(cmdGetAMCCode, "count").ToString());
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return count;
        }
        public string CheckAMCCode(int AMCCode)
        {
            Database db;
            DataSet ds;
            DbCommand cmdCheckAMCCode;
            string amccode = string.Empty;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                //checking year
                cmdCheckAMCCode = db.GetStoredProcCommand("SPROC_CheckAMCCode");
                db.AddInParameter(cmdCheckAMCCode, "@amcId", DbType.Int32, AMCCode);
                db.AddOutParameter(cmdCheckAMCCode, "@amcCode", DbType.String, 20);
                ds = db.ExecuteDataSet(cmdCheckAMCCode);
                if (db.ExecuteNonQuery(cmdCheckAMCCode) != 0)
                {
                    amccode = db.GetParameterValue(cmdCheckAMCCode, "amcCode").ToString();
                }
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return amccode;
        }
        public DataTable GetProductSearchType(string folioNo)
        {
            DataSet dsGetProductSearchType;
            DataTable dtGetProductSearchType;
            Database db;
            DbCommand cmdGetProductSearchType;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProductSearchType = db.GetStoredProcCommand("SPROC_GetOrderDetailsForSearchType");
                db.AddInParameter(cmdGetProductSearchType, "@folioNo", DbType.String, folioNo);
                dsGetProductSearchType = db.ExecuteDataSet(cmdGetProductSearchType);
                dtGetProductSearchType = dsGetProductSearchType.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtGetProductSearchType;
        }
        public DataTable GetSchemeDetails(int AMCCode, int Schemeplanecode, string category, int customerId, bool SchemeDetails)
        {
            DataTable dtGetSchemeDetails;
            Database db;
            DataSet dsGetSchemeDetails;
            DbCommand cmdGetSchemeDetails;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSchemeDetails = db.GetStoredProcCommand("SPROC_ONL_GetProductMFSchemeDetails");
                db.AddInParameter(cmdGetSchemeDetails, "@amcCode", DbType.Int32, AMCCode);
                db.AddInParameter(cmdGetSchemeDetails, "@schemePlanCode", DbType.Int32, Schemeplanecode);
                if (category != "0")
                    db.AddInParameter(cmdGetSchemeDetails, "@category", DbType.String, category);
                if (customerId!=0)
                    db.AddInParameter(cmdGetSchemeDetails, "@customerId", DbType.Int64, customerId);
                db.AddInParameter(cmdGetSchemeDetails, "@SchemeDetails", DbType.Boolean, SchemeDetails);
                dsGetSchemeDetails = db.ExecuteDataSet(cmdGetSchemeDetails);
                dtGetSchemeDetails = dsGetSchemeDetails.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "OnlineMFSchemeDetailsDao.cs:CustomerAddMFSchemeToWatch()");
                object[] objects = new object[3];
                objects[0] = customerId;
                objects[1] = Schemeplanecode;
                objects[2] = SchemeDetails;
                objects[3] = AMCCode;
                objects[2] = SchemeDetails;
                objects[3] = AMCCode;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return dtGetSchemeDetails;
        }
        public DataTable CustomerGetRMSLog(DateTime fromDate,DateTime toDate,int adviserId)
        {
            DataSet dsCustomerGetRMSLog;
            DataTable dtCustomerGetRMSLog;
            Database db;
            DbCommand cmdCustomerGetRMSLog;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCustomerGetRMSLog = db.GetStoredProcCommand("SPROC_GetOnlineCustomerRMSLog");
                db.AddInParameter(cmdCustomerGetRMSLog, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(cmdCustomerGetRMSLog, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(cmdCustomerGetRMSLog, "@adviserId", DbType.Int32, adviserId);
                dsCustomerGetRMSLog = db.ExecuteDataSet(cmdCustomerGetRMSLog);
                dtCustomerGetRMSLog = dsCustomerGetRMSLog.Tables[0];
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return dtCustomerGetRMSLog;
        }
        
    }
}
