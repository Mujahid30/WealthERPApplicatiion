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

        public List<int> CreateOnlineSchemeSetUp(OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo, int userId)
        {
            List<int> SchemePlancodes = new List<int>();
            int SchemePlancode = 0;

            Database db;
            DbCommand createMFOnlineSchemeSetUpCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOnlineSchemeSetUpCmd = db.GetStoredProcCommand("SPROC_Onl_CreateOnlineSchemeSetUp");
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PA_AMCCode", DbType.Int32, OnlineOrderBackOfficeVo.AMCCode);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanName", DbType.String, OnlineOrderBackOfficeVo.SchemePlanName);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanName", DbType.String, OnlineOrderBackOfficeVo.SchemePlanName);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAISSC_AssetInstrumentSubSubCategoryCode", DbType.String, OnlineOrderBackOfficeVo.AssetSubSubCategory);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, OnlineOrderBackOfficeVo.AssetSubCategoryCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, OnlineOrderBackOfficeVo.AssetCategoryCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PAG_AssetGroupCode", DbType.String, OnlineOrderBackOfficeVo.Product);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_Status", DbType.String, OnlineOrderBackOfficeVo.Status);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_IsOnline", DbType.Int32, OnlineOrderBackOfficeVo.IsOnline);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_IsDirect", DbType.Int32, OnlineOrderBackOfficeVo.IsDirect);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_FaceValue", DbType.Double, OnlineOrderBackOfficeVo.FaceValue);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PSLV_LookupValueCodeForSchemeType", DbType.String, OnlineOrderBackOfficeVo.SchemeType);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PSLV_LookupValueCodeForSchemeOption", DbType.String, OnlineOrderBackOfficeVo.SchemeOption);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XF_DividendFrequency", DbType.String, OnlineOrderBackOfficeVo.DividendFrequency);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_BankName", DbType.String, OnlineOrderBackOfficeVo.BankName);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_AccountNumber", DbType.String, OnlineOrderBackOfficeVo.AccountNumber);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_Branch", DbType.String, OnlineOrderBackOfficeVo.Branch);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsNFO", DbType.Int32, OnlineOrderBackOfficeVo.IsNFO);
                if (OnlineOrderBackOfficeVo.NFOStartDate != DateTime.MinValue)
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOStartDate", DbType.DateTime, OnlineOrderBackOfficeVo.NFOStartDate);
                }
                else
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOStartDate", DbType.DateTime, DBNull.Value);
                }
                if (OnlineOrderBackOfficeVo.NFOEndDate != DateTime.MinValue)
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOEndDate", DbType.DateTime, OnlineOrderBackOfficeVo.NFOEndDate);
                }
                else
                {
                    db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_NFOEndDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_LockInPeriod", DbType.Int32, OnlineOrderBackOfficeVo.LockInPeriod);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_CutOffTime", DbType.String, OnlineOrderBackOfficeVo.CutOffTime.ToString());
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_EntryLoadPercentag", DbType.Double, OnlineOrderBackOfficeVo.EntryLoadPercentag);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_EntryLoadRemark", DbType.String, OnlineOrderBackOfficeVo.EntryLoadRemark);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_ExitLoadPercentage", DbType.Double, OnlineOrderBackOfficeVo.ExitLoadPercentage);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_ExitLoadRemark", DbType.String, OnlineOrderBackOfficeVo.ExitLoadRemark);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsPurchaseAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsPurchaseAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsRedeemAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsRedeemAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSIPAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSIPAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSWPAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSWPAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSwitchAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSwitchAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_IsSTPAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSTPAvailable);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_InitialPurchaseAmount", DbType.Double, OnlineOrderBackOfficeVo.InitialPurchaseAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_InitialMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.InitialMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_AdditionalPruchaseAmount", DbType.Double, OnlineOrderBackOfficeVo.AdditionalPruchaseAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_AdditionalMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.AdditionalMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinRedemptionAmount", DbType.Double, OnlineOrderBackOfficeVo.MinRedemptionAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_RedemptionMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.RedemptionMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinRedemptionUnits", DbType.Int32, OnlineOrderBackOfficeVo.MinRedemptionUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_RedemptionMultiplesUnits", DbType.Int32, OnlineOrderBackOfficeVo.RedemptionMultiplesUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinSwitchAmount", DbType.Double, OnlineOrderBackOfficeVo.MinSwitchAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_SwitchMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.SwitchMultipleAmount);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MinSwitchUnits", DbType.Int32, OnlineOrderBackOfficeVo.MinSwitchUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_SwitchMultiplesUnits", DbType.Int32, OnlineOrderBackOfficeVo.SwitchMultiplesUnits);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XF_FileGenerationFrequency", DbType.String, OnlineOrderBackOfficeVo.GenerationFrequency);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XES_SourceCode", DbType.String, OnlineOrderBackOfficeVo.SourceCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XES_SourceCode", DbType.String, DBNull.Value);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XCST_CustomerSubTypeCode", DbType.String, OnlineOrderBackOfficeVo.CustomerSubTypeCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_SecurityCode", DbType.String, OnlineOrderBackOfficeVo.SecurityCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_MaxInvestment", DbType.Double, OnlineOrderBackOfficeVo.PASPD_MaxInvestment);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@WERPBM_BankCode", DbType.String, OnlineOrderBackOfficeVo.WERPBM_BankCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@ExternalCode", DbType.String, OnlineOrderBackOfficeVo.ExternalCode);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@ExternalType", DbType.String, OnlineOrderBackOfficeVo.ExternalType);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_ModifiedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_CreatedBy", DbType.Int32, userId);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASPD_ModifiedBy", DbType.Int32, userId);
                // db.AddOutParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanCode", DbType.Int32, 10000);
                db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanCode", DbType.Int32, OnlineOrderBackOfficeVo.SchemePlanCode);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XF_DividendFrequency", DbType.String, OnlineOrderBackOfficeVo.DividendFrequency);
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
        public OnlineOrderBackOfficeVo GetOnlineSchemeSetUp(int SchemePlanCode)
        {


            OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo = new OnlineOrderBackOfficeVo();
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
                        OnlineOrderBackOfficeVo = new OnlineOrderBackOfficeVo();
                        OnlineOrderBackOfficeVo.AMCCode = int.Parse(dr["PA_AMCCode"].ToString());
                        OnlineOrderBackOfficeVo.SchemePlanName = dr["PASP_SchemePlanName"].ToString();
                        OnlineOrderBackOfficeVo.SchemePlanCode = int.Parse(dr["PASP_SchemePlanCode"].ToString());
                        OnlineOrderBackOfficeVo.AssetSubSubCategory = dr["PAISSC_AssetInstrumentSubSubCategoryCode"].ToString();
                        OnlineOrderBackOfficeVo.AssetSubCategoryCode = dr["PAISC_AssetInstrumentSubCategoryCode"].ToString();
                        OnlineOrderBackOfficeVo.AssetCategoryCode = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                        OnlineOrderBackOfficeVo.Product = dr["PAG_AssetGroupCode"].ToString();
                        OnlineOrderBackOfficeVo.Status = dr["PASP_Status"].ToString();
                        if (dr["PASP_IsOnline"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsOnline = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsOnline = 0;
                        }
                        if (OnlineOrderBackOfficeVo.IsDirect != 0)
                        {
                            OnlineOrderBackOfficeVo.IsDirect = int.Parse(dr["PASP_IsDirect"].ToString());
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsDirect = 0;
                        }
                        if (dr["PASPD_FaceValue"].ToString() != null && dr["PASPD_FaceValue"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.FaceValue = Convert.ToDouble(dr["PASPD_FaceValue"].ToString());
                        OnlineOrderBackOfficeVo.SchemeType = dr["PSLV_LookupValueCodeForSchemeType"].ToString();
                        OnlineOrderBackOfficeVo.SchemeOption = dr["PSLV_LookupValueCodeForSchemeOption"].ToString();
                        if (dr["XF_DividendFrequency"].ToString() != null && dr["XF_DividendFrequency"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.DividendFrequency = dr["XF_DividendFrequency"].ToString();
                        OnlineOrderBackOfficeVo.BankName = dr["PASPD_BankName"].ToString();
                        OnlineOrderBackOfficeVo.AccountNumber = dr["PASPD_AccountNumber"].ToString();
                        OnlineOrderBackOfficeVo.Branch = dr["PASPD_Branch"].ToString();
                        if (dr["PASPD_IsNFO"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsNFO = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsNFO = 0;
                        }
                        if (OnlineOrderBackOfficeVo.NFOStartDate != DateTime.MinValue)
                            OnlineOrderBackOfficeVo.NFOStartDate = DateTime.Parse(dr["PASPD_NFOStartDate"].ToString());
                        if (OnlineOrderBackOfficeVo.NFOEndDate != DateTime.MinValue)
                            OnlineOrderBackOfficeVo.NFOEndDate = DateTime.Parse(dr["PASPD_NFOEndDate"].ToString());
                        if (dr["PASPD_LockInPeriod"].ToString() != null && dr["PASPD_LockInPeriod"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.LockInPeriod = int.Parse(dr["PASPD_LockInPeriod"].ToString());
                        if (dr["PASPD_CutOffTime"].ToString() != null && dr["PASPD_CutOffTime"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.CutOffTime = TimeSpan.Parse(dr["PASPD_CutOffTime"].ToString());
                        if (dr["PASPD_EntryLoadPercentage"].ToString() != null && dr["PASPD_EntryLoadPercentage"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.EntryLoadPercentag = Convert.ToDouble(dr["PASPD_EntryLoadPercentage"].ToString());
                        if (dr["PASPD_EntryLoadRemark"].ToString() != null && dr["PASPD_EntryLoadRemark"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.EntryLoadRemark = dr["PASPD_EntryLoadRemark"].ToString();
                        if (dr["PASPD_ExitLoadPercentage"].ToString() != null && dr["PASPD_ExitLoadPercentage"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.ExitLoadPercentage = Convert.ToDouble(dr["PASPD_ExitLoadPercentage"].ToString());
                        if (dr["PASPD_ExitLoadRemark"].ToString() != null && dr["PASPD_ExitLoadRemark"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.ExitLoadRemark = dr["PASPD_ExitLoadRemark"].ToString();
                        if (dr["PASPD_IsPurchaseAvailable"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsPurchaseAvailable = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsPurchaseAvailable = 0;
                        }
                        if (dr["PASPD_IsRedeemAvailable"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsRedeemAvailable = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsRedeemAvailable = 0;
                        }
                        if (dr["PASPD_IsSIPAvailable"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsSIPAvailable = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsSIPAvailable = 0;
                        }
                        if (dr["PASPD_IsSWPAvailable"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsSWPAvailable = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsSWPAvailable = 0;
                        }
                        if (dr["PASPD_IsSwitchAvailable"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsSwitchAvailable = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsSwitchAvailable = 0;
                        }
                        if (dr["PASPD_IsSTPAvailable"].ToString() == "True")
                        {
                            OnlineOrderBackOfficeVo.IsSTPAvailable = 1;
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.IsSTPAvailable = 0;
                        }
                        if (dr["PASPD_InitialPurchaseAmount"].ToString() != null && dr["PASPD_InitialPurchaseAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.InitialPurchaseAmount = Convert.ToDouble(dr["PASPD_InitialPurchaseAmount"].ToString());
                        if (dr["PASPD_InitialMultipleAmount"].ToString() != null && dr["PASPD_InitialMultipleAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.InitialMultipleAmount = Convert.ToDouble(dr["PASPD_InitialMultipleAmount"].ToString());
                        if (dr["PASPD_InitialMultipleAmount"].ToString() != null && dr["PASPD_InitialMultipleAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.AdditionalPruchaseAmount = Convert.ToDouble(dr["PASPD_InitialMultipleAmount"].ToString());
                        if (dr["PASPD_AdditionalMultipleAmount"].ToString() != null && dr["PASPD_AdditionalMultipleAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.AdditionalMultipleAmount = Convert.ToDouble(dr["PASPD_AdditionalMultipleAmount"].ToString());
                        if (dr["PASPD_MinRedemptionAmount"].ToString() != null && dr["PASPD_MinRedemptionAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.MinRedemptionAmount = Convert.ToDouble(dr["PASPD_MinRedemptionAmount"].ToString());
                        if (dr["PASPD_RedemptionMultipleAmount"].ToString() != null && dr["PASPD_RedemptionMultipleAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.RedemptionMultipleAmount = Convert.ToDouble(dr["PASPD_RedemptionMultipleAmount"].ToString());
                        if (dr["PASPD_MinRedemptionUnits"].ToString() != null && dr["PASPD_MinRedemptionUnits"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.MinRedemptionUnits = int.Parse(dr["PASPD_MinRedemptionUnits"].ToString());
                        if (dr["PASPD_RedemptionMultiplesUnits"].ToString() != null && dr["PASPD_RedemptionMultiplesUnits"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.RedemptionMultiplesUnits = int.Parse(dr["PASPD_RedemptionMultiplesUnits"].ToString());
                        if (dr["PASPD_MinSwitchAmount"].ToString() != null && dr["PASPD_MinSwitchAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.MinSwitchAmount = Convert.ToDouble(dr["PASPD_MinSwitchAmount"].ToString());
                        if (dr["PASPD_SwitchMultipleAmount"].ToString() != null && dr["PASPD_SwitchMultipleAmount"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.SwitchMultipleAmount = Convert.ToDouble(dr["PASPD_SwitchMultipleAmount"].ToString());
                        if (dr["PASPD_MinSwitchUnits"].ToString() != null && dr["PASPD_MinSwitchUnits"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.MinSwitchUnits = int.Parse(dr["PASPD_MinSwitchUnits"].ToString());
                        if (dr["PASPD_SwitchMultiplesUnits"].ToString() != null && dr["PASPD_SwitchMultiplesUnits"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.SwitchMultiplesUnits = int.Parse(dr["PASPD_SwitchMultiplesUnits"].ToString());
                        if (dr["XF_FileGenerationFrequency"].ToString() != null && dr["XF_FileGenerationFrequency"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.GenerationFrequency = dr["XF_FileGenerationFrequency"].ToString();
                        if (!string.IsNullOrEmpty(OnlineOrderBackOfficeVo.SourceCode))
                        {
                            OnlineOrderBackOfficeVo.SourceCode = dr["XES_SourceCode"].ToString();
                        }
                        //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XES_SourceCode", DbType.String, DBNull.Value);
                        OnlineOrderBackOfficeVo.CustomerSubTypeCode = dr["XCST_CustomerSubTypeCode"].ToString();
                        if (dr["PASPD_SecurityCode"].ToString() != null && dr["PASPD_SecurityCode"].ToString() != string.Empty)
                            OnlineOrderBackOfficeVo.SecurityCode = dr["PASPD_SecurityCode"].ToString();
                        if (dr["PASPD_MaxInvestment"].ToString() != null && dr["PASPD_MaxInvestment"].ToString() != string.Empty)
                        {
                            OnlineOrderBackOfficeVo.PASPD_MaxInvestment = Convert.ToDouble(dr["PASPD_MaxInvestment"].ToString());
                        }
                        else
                        {
                            OnlineOrderBackOfficeVo.PASPD_MaxInvestment = 0;
                        }
                        if (!string.IsNullOrEmpty(dr["WERPBM_BankCode"].ToString()))
                        {
                            OnlineOrderBackOfficeVo.WERPBM_BankCode = dr["WERPBM_BankCode"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["PASC_AMC_ExternalCode"].ToString()))
                        {
                            OnlineOrderBackOfficeVo.ExternalCode = dr["PASC_AMC_ExternalCode"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["PASC_AMC_ExternalType"].ToString()))
                        {
                            OnlineOrderBackOfficeVo.ExternalType = dr["PASC_AMC_ExternalType"].ToString();
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
            return OnlineOrderBackOfficeVo;



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
        public bool CreateMapwithRTA(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo, int userID)
        {
            bool bResult = false;
            Database db;
            DbCommand createCmd;
            int affectedRecords = 0;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateMapwithRTA");
                db.AddInParameter(createCmd, "@WCMV_LookupId", DbType.Int32, onlineOrderBackOfficeVo.LookupID);
                db.AddInParameter(createCmd, "@XES_SourceCode", DbType.String, onlineOrderBackOfficeVo.SourceCode);
                db.AddInParameter(createCmd, "@WCMVXM_ExternalCode", DbType.String, onlineOrderBackOfficeVo.ExternalCode);
                db.AddInParameter(createCmd, "@WCMVXM_ExternalName", DbType.String, onlineOrderBackOfficeVo.ExternalName);
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
        public bool CreateNewWerpName(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo, int userID)
        {
            bool bResult = false;
            int affectedRecords = 0;
            Database db;
            DbCommand createCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_CreateNewWerpName");
                db.AddInParameter(createCmd, "@WCMV_Name", DbType.String, onlineOrderBackOfficeVo.WerpName);
                db.AddInParameter(createCmd, "@WCM_Id", DbType.Int32, onlineOrderBackOfficeVo.CategoryID);
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
        public bool RemoveMapingWIthRTA(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo)
        {
            bool bResult = false;
            Database db;
            DbCommand createMFOrderTrackingCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SPROC_RemoveMapwithRTA");
                db.AddInParameter(createMFOrderTrackingCmd, "@MapID", DbType.Int32, onlineOrderBackOfficeVo.MapID);

                db.ExecuteNonQuery(createMFOrderTrackingCmd);
                bResult = true;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            return bResult;
        }

        public bool UpdateWerpName(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo, int userID)
        {
            bool bResult = false;
            Database db;
            DbCommand createMFOrderTrackingCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createMFOrderTrackingCmd = db.GetStoredProcCommand("SPROC_UpdateInternalValues");
                db.AddInParameter(createMFOrderTrackingCmd, "@lookupID", DbType.Int32, onlineOrderBackOfficeVo.LookupID);
                db.AddInParameter(createMFOrderTrackingCmd, "@internalName", DbType.String, onlineOrderBackOfficeVo.WerpName);
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

        public bool DeleteWerpName(VoOnlineOrderManagemnet.OnlineOrderBackOfficeVo onlineOrderBackOfficeVo)
        {
            bool bResult = false;
            int affectedRecords=0;
            Database db;
            DbCommand createCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                createCmd = db.GetStoredProcCommand("SPROC_DeleteInternalValues");
                db.AddInParameter(createCmd, "@lookupID", DbType.Int32, onlineOrderBackOfficeVo.LookupID);

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


        public bool UpdateSchemeSetUpDetail(OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo, int SchemePlanCode)
        {
            bool blResult = false;
            Database db;
            DbCommand updateSchemeSetUpDetailsCmd;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                updateSchemeSetUpDetailsCmd = db.GetStoredProcCommand("SPROC_Onl_UpdateSchemeSetUpDetail");
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASP_SchemePlanCode", DbType.Int32, OnlineOrderBackOfficeVo.SchemePlanCode); //1
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_FaceValue", DbType.Double, OnlineOrderBackOfficeVo.FaceValue); //2
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PSLV_LookupValueCodeForSchemeType", DbType.String, OnlineOrderBackOfficeVo.SchemeType); //3
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PSLV_LookupValueCodeForSchemeOption", DbType.String, OnlineOrderBackOfficeVo.SchemeOption);//4
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@XF_DividendFrequency", DbType.String, OnlineOrderBackOfficeVo.DividendFrequency);//5
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_BankName", DbType.String, OnlineOrderBackOfficeVo.BankName);//6
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_AccountNumber", DbType.String, OnlineOrderBackOfficeVo.AccountNumber); //7
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_Branch", DbType.String, OnlineOrderBackOfficeVo.Branch);//8
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsNFO", DbType.Int32, OnlineOrderBackOfficeVo.IsNFO);//9

                if (OnlineOrderBackOfficeVo.NFOStartDate != DateTime.MinValue) //10
                {
                    db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_NFOStartDate", DbType.DateTime, OnlineOrderBackOfficeVo.NFOStartDate);
                }
                else
                {
                    db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_NFOStartDate", DbType.DateTime, DBNull.Value);
                }
                if (OnlineOrderBackOfficeVo.NFOEndDate != DateTime.MinValue)//11
                {
                    db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_NFOEndDate", DbType.DateTime, OnlineOrderBackOfficeVo.NFOEndDate);
                }
                else
                {
                    db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_NFOEndDate", DbType.DateTime, DBNull.Value);
                }
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_LockInPeriod", DbType.Int32, OnlineOrderBackOfficeVo.LockInPeriod); //12
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_CutOffTime", DbType.String, OnlineOrderBackOfficeVo.CutOffTime.ToString()); //13
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_EntryLoadPercentage", DbType.Double, OnlineOrderBackOfficeVo.EntryLoadPercentag);//14
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_EntryLoadRemark", DbType.String, OnlineOrderBackOfficeVo.EntryLoadRemark);//15
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_ExitLoadPercentage", DbType.Double, OnlineOrderBackOfficeVo.ExitLoadPercentage);//16
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_ExitLoadRemark", DbType.String, OnlineOrderBackOfficeVo.ExitLoadRemark);//17
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsPurchaseAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsPurchaseAvailable);//18
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsRedeemAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsRedeemAvailable);//19
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSIPAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSIPAvailable);//20
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSWPAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSWPAvailable);//21
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSwitchAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSwitchAvailable);//22
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_IsSTPAvailable", DbType.Int32, OnlineOrderBackOfficeVo.IsSTPAvailable);//23
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_InitialPurchaseAmount", DbType.Double, OnlineOrderBackOfficeVo.InitialPurchaseAmount);//24
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_InitialMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.InitialMultipleAmount);//25
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_AdditionalPruchaseAmount", DbType.Double, OnlineOrderBackOfficeVo.AdditionalPruchaseAmount);//26
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_AdditionalMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.AdditionalMultipleAmount);//27
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinRedemptionAmount", DbType.Double, OnlineOrderBackOfficeVo.MinRedemptionAmount);//28
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_RedemptionMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.RedemptionMultipleAmount);//29
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinRedemptionUnits", DbType.Int32, OnlineOrderBackOfficeVo.MinRedemptionUnits);//30
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_RedemptionMultiplesUnits", DbType.Int32, OnlineOrderBackOfficeVo.RedemptionMultiplesUnits);//31
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinSwitchAmount", DbType.Double, OnlineOrderBackOfficeVo.MinSwitchAmount);//32
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_SwitchMultipleAmount", DbType.Double, OnlineOrderBackOfficeVo.SwitchMultipleAmount);//33
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MinSwitchUnits", DbType.Int32, OnlineOrderBackOfficeVo.MinSwitchUnits);//34
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_SwitchMultiplesUnits", DbType.Int32, OnlineOrderBackOfficeVo.SwitchMultiplesUnits);//35
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@XF_FileGenerationFrequency", DbType.String, OnlineOrderBackOfficeVo.GenerationFrequency);//36
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@XES_SourceCode", DbType.String, DBNull.Value);//37
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@XCST_CustomerSubTypeCode", DbType.String, OnlineOrderBackOfficeVo.CustomerSubTypeCode);//38
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_SecurityCode", DbType.String, OnlineOrderBackOfficeVo.SecurityCode);//39
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_MaxInvestment", DbType.Double, OnlineOrderBackOfficeVo.PASPD_MaxInvestment);//40
                db.AddInParameter(updateSchemeSetUpDetailsCmd, "@WERPBM_BankCode", DbType.String, OnlineOrderBackOfficeVo.WERPBM_BankCode);//41
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PA_AMCCode", DbType.Int32, OnlineOrderBackOfficeVo.AMCCode);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanName", DbType.String, OnlineOrderBackOfficeVo.SchemePlanName);
                //db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASP_SchemePlanName", DbType.String, OnlineOrderBackOfficeVo.SchemePlanName);
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PAISSC_AssetInstrumentSubSubCategoryCode", DbType.String,OnlineOrderBackOfficeVo.AssetSubSubCategory);
                //db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PAISC_AssetInstrumentSubCategoryCode", DbType.String, OnlineOrderBackOfficeVo.AssetSubCategoryCode);
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PAIC_AssetInstrumentCategoryCode", DbType.String, OnlineOrderBackOfficeVo.AssetCategoryCode);
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PAG_AssetGroupCode",DbType.String, OnlineOrderBackOfficeVo.Product);
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASP_Status", DbType.String,OnlineOrderBackOfficeVo.Status);
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASP_IsOnline", DbType.Int32,OnlineOrderBackOfficeVo.IsOnline);
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASP_IsDirect", DbType.Int32,OnlineOrderBackOfficeVo.IsDirect);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XES_SourceCode", DbType.String, OnlineOrderBackOfficeVo.SourceCode);
                //db.AddInParameter(updateSchemeSetUpDetailsCmd, "@ExternalCode", DbType.String,OnlineOrderBackOfficeVo.ExternalCode);
                //db.AddInParameter(updateSchemeSetUpDetailsCmd, "@ExternalType", DbType.String,OnlineOrderBackOfficeVo.ExternalType);
                // db.AddInParameter(updateSchemeSetUpDetailsCmd, "@PASPD_ModifiedBy", DbType.Int32, UserId);
                // db.AddOutParameter(createMFOnlineSchemeSetUpCmd, "@PASP_SchemePlanCode",DbType.Int32, 10000);
                //db.AddInParameter(createMFOnlineSchemeSetUpCmd, "@XF_DividendFrequency",DbType.String, OnlineOrderBackOfficeVo.DividendFrequency);
                db.ExecuteNonQuery(updateSchemeSetUpDetailsCmd);

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
                objects[0] = OnlineOrderBackOfficeVo;
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
                dsOnlinebindRandT=db.ExecuteDataSet(OnlinebindRandTCmd);
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
                objects[0]=SchemPlaneCode;
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
        public bool CreateSystematicDetails(OnlineOrderBackOfficeVo OnlineOrderBackOfficeVo, int schemeplancode)
        {
            bool bResult = false;
            Database db;
            DbCommand CreateSystematicDetailsCmd;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                CreateSystematicDetailsCmd = db.GetStoredProcCommand("SPROC_ONL_CreatesystematicDetails");
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASP_SchemePlanCode", DbType.Int32,schemeplancode);
                db.AddInParameter(CreateSystematicDetailsCmd, "@XSTT_SystematicTypeCode", DbType.String, OnlineOrderBackOfficeVo.systematiccode);
                db.AddInParameter(CreateSystematicDetailsCmd, "@XF_SystematicFrequencyCode", DbType.String,OnlineOrderBackOfficeVo.frequency);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_StatingDates", DbType.String,OnlineOrderBackOfficeVo.startdate);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_MinDues", DbType.Int32,OnlineOrderBackOfficeVo.MinDues);
                db.AddInParameter(CreateSystematicDetailsCmd, "@PASPSD_MaxDues", DbType.Int32,OnlineOrderBackOfficeVo.MaxDues);
                db.AddInParameter(CreateSystematicDetailsCmd,"@PASPSD_MinAmount", DbType.Double,OnlineOrderBackOfficeVo.MinAmount);
                db.AddInParameter(CreateSystematicDetailsCmd,"@PASPSD_MultipleAmount", DbType.Double,OnlineOrderBackOfficeVo.MultipleAmount);
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
                objects[0] = OnlineOrderBackOfficeVo;
                objects[1] = schemeplancode;

                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;

            }
            return bResult;
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
                db.AddInParameter(createtradeBusinessDateCmd, "@WTBD_TradeId", DbType.Int32, tradeBusinessDateVo.TradeBusinessId);
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
    }
}
