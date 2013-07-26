using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Collections.Specialized;
using VoCommisionManagement;

namespace DaoCommisionManagement
{
    public class CommisionReceivableDao
    {
        /// <summary>
        /// Get all the lookup data for Receivable SetUP UI
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>

        public DataSet GetLookupDataForReceivableSetUP(int adviserId)
        {
            Database db;
            DbCommand cmdGetLookupDataForReceivable;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetLookupDataForReceivable = db.GetStoredProcCommand("SPROC_GetLookupDataForReceivableSetUP");
                db.AddInParameter(cmdGetLookupDataForReceivable, "@A_AdviserId", DbType.Int32, adviserId);
                ds = db.ExecuteDataSet(cmdGetLookupDataForReceivable);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:GetLookupDataForReceivableSetUP(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public void CreateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId, out Int32 instructureId)
        {
            Database db;
            DbCommand cmdCreateCommissionStructure;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructure = db.GetStoredProcCommand("SPROC_CreateCommissionStructureMastter");
                db.AddInParameter(cmdCreateCommissionStructure, "@A_AdviserId", DbType.Int32, commissionStructureMasterVo.AdviserId);
                db.AddInParameter(cmdCreateCommissionStructure, "@PAG_AssetCategoryCode", DbType.String, commissionStructureMasterVo.ProductType);
                db.AddInParameter(cmdCreateCommissionStructure, "@PAIC_AssetInstrumentCategoryCode", DbType.String, commissionStructureMasterVo.AssetCategory);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_CommissionStructureName", DbType.String, commissionStructureMasterVo.CommissionStructureName);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_Issuer", DbType.Int32, Convert.ToUInt32(commissionStructureMasterVo.Issuer.ToString()));
                db.AddInParameter(cmdCreateCommissionStructure, "@WCAL_ApplicableLevelCode", DbType.String, commissionStructureMasterVo.ApplicableLevelCode);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_ValidityStartDate", DbType.Date, commissionStructureMasterVo.ValidityStartDate);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_ValidityEndDate", DbType.Date, commissionStructureMasterVo.ValidityEndDate);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_IsServiceTaxReduced", DbType.Int16, commissionStructureMasterVo.IsServiceTaxReduced);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_IsTDSReduced", DbType.Int16, commissionStructureMasterVo.IsTDSReduced);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_IsOtherTaxReduced", DbType.Int16, commissionStructureMasterVo.IsOtherTaxReduced);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_IsNonMonetaryReward", DbType.Int16, commissionStructureMasterVo.IsNonMonetaryReward);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_IsClawBackApplicable", DbType.Int16, commissionStructureMasterVo.IsClawBackApplicable);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_ReceivableFrequency", DbType.String, commissionStructureMasterVo.ReceivableFrequency);
                db.AddInParameter(cmdCreateCommissionStructure, "@ACSM_Note", DbType.String, commissionStructureMasterVo.StructureNote);
                db.AddInParameter(cmdCreateCommissionStructure, "@AssetSubGroupCode", DbType.String, Convert.ToString(commissionStructureMasterVo.AssetSubCategory));
                db.AddInParameter(cmdCreateCommissionStructure, "@UserId", DbType.String, userId);
                db.AddOutParameter(cmdCreateCommissionStructure, "@CommissionStructureId", DbType.Int64, 1000000);
                db.ExecuteNonQuery(cmdCreateCommissionStructure);
                Object objCommissionStructureId = db.GetParameterValue(cmdCreateCommissionStructure, "@CommissionStructureId");
                if (objCommissionStructureId != DBNull.Value)
                    instructureId = Convert.ToInt32(objCommissionStructureId);
                else
                    instructureId = 0;
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:CreateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo)");
                object[] objects = new object[2];
                objects[0] = commissionStructureMasterVo.AdviserId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        public DataSet GetAdviserCommissionStructureRules(int adviserId, int structureId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_GetAdviserCommissionStructureRules");
                db.AddInParameter(cmdGetCommissionStructureRules, "@A_AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@StructureId", DbType.Int32, structureId);
                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:GetAdviserCommissionStructureRules(int adviserId)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public void CreateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId)
        {
            Database db;
            DbCommand cmdCreateCommissionStructureRule;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdCreateCommissionStructureRule = db.GetStoredProcCommand("SPROC_CreateCommissionStructureRule");
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSM_CommissionStructureId", DbType.Int64, commissionStructureRuleVo.CommissionStructureId);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@WCT_CommissionTypeCode", DbType.String, commissionStructureRuleVo.CommissionType);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@XCC_CustomerCategoryCode", DbType.String, commissionStructureRuleVo.CustomerType);

                if (commissionStructureRuleVo.MinInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MinInvestmentAmount);
                }
                if (commissionStructureRuleVo.MaxInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MaxInvestmentAmount);
                }

                if (commissionStructureRuleVo.TenureMax != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinTenure", DbType.Int32, commissionStructureRuleVo.TenureMin);
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxTenure", DbType.Int32, commissionStructureRuleVo.TenureMax);
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_TenureUnit", DbType.String, commissionStructureRuleVo.TenureUnit);
                }

                if (commissionStructureRuleVo.MaxInvestmentAge != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinInvestmentAge", DbType.Int32, commissionStructureRuleVo.MinInvestmentAge);
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MaxInvestmentAge", DbType.Int32, commissionStructureRuleVo.MaxInvestmentAge);
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_InvestmentAgeUnit", DbType.String, commissionStructureRuleVo.InvestmentAgeUnit);
                }

                if (!string.IsNullOrEmpty(commissionStructureRuleVo.TransactionType))
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_TransactionType", DbType.String, commissionStructureRuleVo.TransactionType);
                if (!string.IsNullOrEmpty(commissionStructureRuleVo.SIPFrequency))
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_SIPFrequency", DbType.String, commissionStructureRuleVo.SIPFrequency);


                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_BrokerageValue", DbType.Decimal, commissionStructureRuleVo.BrokerageValue);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@WCU_UnitCode", DbType.String, commissionStructureRuleVo.BrokerageUnitCode);


                db.AddInParameter(cmdCreateCommissionStructureRule, "@WCCO_CalculatedOnCode", DbType.String, commissionStructureRuleVo.CalculatedOnCode);
                if (commissionStructureRuleVo.AUMMonth != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSM_AUMFrequency", DbType.String, commissionStructureRuleVo.AUMFrequency);
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_AUMMonth", DbType.Int16, commissionStructureRuleVo.AUMMonth);
                }

                if (commissionStructureRuleVo.MinNumberofApplications != 0)
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinNumberOfApplications", DbType.Int32, commissionStructureRuleVo.MinNumberofApplications);
                if (!string.IsNullOrEmpty(commissionStructureRuleVo.StructureRuleComment))
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_Comment", DbType.String, commissionStructureRuleVo.StructureRuleComment);
                
                db.AddInParameter(cmdCreateCommissionStructureRule, "@UsetId", DbType.Int32, userId);

                db.ExecuteNonQuery(cmdCreateCommissionStructureRule);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:CreateCommissionStructureRule(CommissionStructureMasterVo commissionStructureMasterVo)");
                object[] objects = new object[2];
                objects[0] = commissionStructureRuleVo.CommissionStructureId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        /// <summary>
        /// Get all the commission rule for a particular structure.
        /// </summary>
        /// <param name="adviserId"></param>
        /// <returns></returns>

        public DataSet GetStructureCommissionAllRules(int structureId, string commissionType)
        {
            Database db;
            DbCommand cmdGetStructureCommissionRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructureCommissionRules = db.GetStoredProcCommand("SPROC_GetStructureCommissionAllRules");
                db.AddInParameter(cmdGetStructureCommissionRules, "@StructureId", DbType.Int32, structureId);
                db.AddInParameter(cmdGetStructureCommissionRules, "@CommissionType", DbType.String, commissionType);
                ds = db.ExecuteDataSet(cmdGetStructureCommissionRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:GetStructureCommissionAllRules(int structureId,string commissionType)");
                object[] objects = new object[2];
                objects[0] = structureId;
                objects[1] = commissionType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetProductType()
        {
            Database db;
            DbCommand cmdGetProdTypeCm;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProdTypeCm = db.GetStoredProcCommand("SPROC_GetProductTypeCM");
                ds = db.ExecuteDataSet(cmdGetProdTypeCm);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetProductType()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetCategories(string prodType)
        {
            Database db;
            DbCommand cmdGetCatCm;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCatCm = db.GetStoredProcCommand("SPROC_GetCategoryCM");
                db.AddInParameter(cmdGetCatCm, "@AssetGroupCode", DbType.String, prodType);
                ds = db.ExecuteDataSet(cmdGetCatCm);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetCategories(string prodType)");
                object[] objects = new object[1];
                objects[0] = prodType;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetSubCategories(string cat)
        {
            Database db;
            DbCommand cmdGetSubCatCm;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetSubCatCm = db.GetStoredProcCommand("SPROC_GetSubCategoryCM");
                db.AddInParameter(cmdGetSubCatCm, "@Category", DbType.String, cat);
                ds = db.ExecuteDataSet(cmdGetSubCatCm);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetSubCategories(string cat)");
                object[] objects = new object[1];
                objects[0] = cat;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetProdAmc()
        {
            Database db;
            DbCommand cmdGetProdAmc;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetProdAmc = db.GetStoredProcCommand("SP_GetProductAmc");
                ds = db.ExecuteDataSet(cmdGetProdAmc);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetProdAmc()");
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public DataSet GetAdviserCommissionStructureRules(int adviserId, string product, string cat, string subcat, int issuer, string validity)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            string parProd = product.ToLower().Equals("all") ? null : product;
            string parCat = cat.ToLower().Equals("all") ? null : cat;
            string parSubcat = subcat.ToLower().Equals("all") ? null : subcat;
            string parValid = validity.ToLower().Equals("all") ? null : validity;
            //For issuer, param dealt in SPROC

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_GetAdviserCommissionStructureRulesCM");
                db.AddInParameter(cmdGetCommissionStructureRules, "@AdviserId", DbType.Int32, adviserId);
                db.AddInParameter(cmdGetCommissionStructureRules, "@Product", DbType.String, parProd);
                db.AddInParameter(cmdGetCommissionStructureRules, "@Category", DbType.String, parCat);
                db.AddInParameter(cmdGetCommissionStructureRules, "@SubCategory", DbType.String, parSubcat);
                db.AddInParameter(cmdGetCommissionStructureRules, "@Issuer", DbType.Int32, issuer);
                db.AddInParameter(cmdGetCommissionStructureRules, "@Valid", DbType.String, parValid);
                ds = db.ExecuteDataSet(cmdGetCommissionStructureRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetAdviserCommissionStructureRules(int adviserId, string product, string cat, string subcat, int validity)");
                object[] objects = new object[1];
                objects[0] = adviserId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        public CommissionStructureMasterVo GetCommissionStructureMaster(int structureId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureMaster;
            CommissionStructureMasterVo commissionStructureMasterVo = new CommissionStructureMasterVo();
            DataSet ds;
            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureMaster = db.GetStoredProcCommand("SPROC_GetCommissionStructureMaster");
                db.AddInParameter(cmdGetCommissionStructureMaster, "@StructureId", DbType.Int32, structureId);
                ds = db.ExecuteDataSet(cmdGetCommissionStructureMaster);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    commissionStructureMasterVo.CommissionStructureId = Convert.ToInt32(dr["ACSM_CommissionStructureId"].ToString());
                    commissionStructureMasterVo.ProductType = dr["PAG_AssetGroupCode"].ToString();
                    commissionStructureMasterVo.AssetCategory = dr["PAIC_AssetInstrumentCategoryCode"].ToString();
                    commissionStructureMasterVo.CommissionStructureName = dr["ACSM_CommissionStructureName"].ToString();
                    commissionStructureMasterVo.Issuer = dr["ACSM_Issuer"].ToString();
                    commissionStructureMasterVo.ApplicableLevelCode = dr["WCAL_ApplicableLevelCode"].ToString();
                    commissionStructureMasterVo.ValidityStartDate = Convert.ToDateTime(dr["ACSM_ValidityStartDate"].ToString());
                    commissionStructureMasterVo.ValidityEndDate = Convert.ToDateTime(dr["ACSM_ValidityEndDate"].ToString());

                    commissionStructureMasterVo.IsServiceTaxReduced = Convert.ToBoolean(Convert.ToInt16(dr["ACSM_IsServiceTaxReduced"].ToString()));
                    commissionStructureMasterVo.IsTDSReduced = Convert.ToBoolean(Convert.ToInt16(dr["ACSM_IsTDSReduced"].ToString()));
                    commissionStructureMasterVo.IsOtherTaxReduced = Convert.ToBoolean(Convert.ToInt16(dr["ACSM_IsOtherTaxReduced"].ToString()));
                    commissionStructureMasterVo.IsNonMonetaryReward = Convert.ToBoolean(Convert.ToInt16(dr["ACSM_IsNonMonetaryReward"].ToString()));
                    commissionStructureMasterVo.IsClawBackApplicable = Convert.ToBoolean(Convert.ToInt16(dr["ACSM_IsClawBackApplicable"].ToString()));

                    commissionStructureMasterVo.ReceivableFrequency = dr["ACSM_ReceivableFrequency"].ToString();
                    commissionStructureMasterVo.AdviserCityGroupCode = dr["ACG_CityGroupID"].ToString();
                    commissionStructureMasterVo.StructureNote = dr["ACSM_Note"].ToString();
                }
                StringBuilder strSubCategoryCode = new StringBuilder();

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    strSubCategoryCode.Append(dr["PAISC_AssetInstrumentSubCategoryCode"].ToString());
                    strSubCategoryCode.Append("~");

                }
                if (!string.IsNullOrEmpty(strSubCategoryCode.ToString()))
                    strSubCategoryCode.Remove((strSubCategoryCode.Length - 1), 1);

                commissionStructureMasterVo.AssetSubCategory = strSubCategoryCode;


            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs: GetCommissionStructureMaster(int structureId)");
                object[] objects = new object[2];
                objects[0] = structureId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return commissionStructureMasterVo;
        }

        public void UpdateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId)
        {
            Database db;
            DbCommand cmdUpdateCommissionStructure;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCommissionStructure = db.GetStoredProcCommand("SPROC_UpdateCommissionStructureMaster");
                db.AddInParameter(cmdUpdateCommissionStructure, "@CommissionStructureId", DbType.Int32, commissionStructureMasterVo.CommissionStructureId);
                db.AddInParameter(cmdUpdateCommissionStructure, "@PAG_AssetCategoryCode", DbType.String, commissionStructureMasterVo.ProductType);
                db.AddInParameter(cmdUpdateCommissionStructure, "@PAIC_AssetInstrumentCategoryCode", DbType.String, commissionStructureMasterVo.AssetCategory);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_CommissionStructureName", DbType.String, commissionStructureMasterVo.CommissionStructureName);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_Issuer", DbType.Int32, Convert.ToUInt32(commissionStructureMasterVo.Issuer.ToString()));
                db.AddInParameter(cmdUpdateCommissionStructure, "@WCAL_ApplicableLevelCode", DbType.String, commissionStructureMasterVo.ApplicableLevelCode);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_ValidityStartDate", DbType.Date, commissionStructureMasterVo.ValidityStartDate);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_ValidityEndDate", DbType.Date, commissionStructureMasterVo.ValidityEndDate);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_IsServiceTaxReduced", DbType.Int16, commissionStructureMasterVo.IsServiceTaxReduced);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_IsTDSReduced", DbType.Int16, commissionStructureMasterVo.IsTDSReduced);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_IsOtherTaxReduced", DbType.Int16, commissionStructureMasterVo.IsOtherTaxReduced);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_IsNonMonetaryReward", DbType.Int16, commissionStructureMasterVo.IsNonMonetaryReward);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_IsClawBackApplicable", DbType.Int16, commissionStructureMasterVo.IsClawBackApplicable);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_ReceivableFrequency", DbType.String, commissionStructureMasterVo.ReceivableFrequency);
                db.AddInParameter(cmdUpdateCommissionStructure, "@ACSM_Note", DbType.String, commissionStructureMasterVo.StructureNote);
                db.AddInParameter(cmdUpdateCommissionStructure, "@AssetSubGroupCode", DbType.String, Convert.ToString(commissionStructureMasterVo.AssetSubCategory));
                db.AddInParameter(cmdUpdateCommissionStructure, "@UserId", DbType.String, userId);
                db.ExecuteNonQuery(cmdUpdateCommissionStructure);

            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:UpdateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId)");
                object[] objects = new object[2];
                objects[0] = commissionStructureMasterVo.AdviserId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }

        public void UpdateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId)
        {
            Database db;
            DbCommand cmdUpdateCommissionStructureRule;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdUpdateCommissionStructureRule = db.GetStoredProcCommand("SPROC_UpdateCommissionStructureRule");
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSM_CommissionStructureRuleId", DbType.Int64, commissionStructureRuleVo.CommissionStructureRuleId);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@WCT_CommissionTypeCode", DbType.String, commissionStructureRuleVo.CommissionType);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@XCC_CustomerCategoryCode", DbType.String, commissionStructureRuleVo.CustomerType);

                if (commissionStructureRuleVo.MinInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MinInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MinInvestmentAmount);

                }
                if (commissionStructureRuleVo.MaxInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MaxInvestmentAmount);
                }

                if (commissionStructureRuleVo.TenureMax != 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MinTenure", DbType.Int32, commissionStructureRuleVo.TenureMin);
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxTenure", DbType.Int32, commissionStructureRuleVo.TenureMax);
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_TenureUnit", DbType.String, commissionStructureRuleVo.TenureUnit);
                }

                if (commissionStructureRuleVo.MaxInvestmentAge != 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MinInvestmentAge", DbType.Int32, commissionStructureRuleVo.MinInvestmentAge);
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MaxInvestmentAge", DbType.Int32, commissionStructureRuleVo.MaxInvestmentAge);
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_InvestmentAgeUnit", DbType.String, commissionStructureRuleVo.InvestmentAgeUnit);
                }

                if (!string.IsNullOrEmpty(commissionStructureRuleVo.TransactionType))
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_TransactionType", DbType.String, commissionStructureRuleVo.TransactionType);               

                db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_BrokerageValue", DbType.Decimal, commissionStructureRuleVo.BrokerageValue);
                db.AddInParameter(cmdUpdateCommissionStructureRule, "@WCU_UnitCode", DbType.String, commissionStructureRuleVo.BrokerageUnitCode);


                db.AddInParameter(cmdUpdateCommissionStructureRule, "@WCCO_CalculatedOnCode", DbType.String, commissionStructureRuleVo.CalculatedOnCode);
                if (commissionStructureRuleVo.AUMMonth != 0)
                {
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSM_AUMFrequency", DbType.String, commissionStructureRuleVo.AUMFrequency);
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_AUMMonth", DbType.Int16, commissionStructureRuleVo.AUMMonth);
                }

                if (commissionStructureRuleVo.MinNumberofApplications != 0)
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_MinNumberOfApplications", DbType.Int32, commissionStructureRuleVo.MinNumberofApplications);
                if (!string.IsNullOrEmpty(commissionStructureRuleVo.StructureRuleComment))
                    db.AddInParameter(cmdUpdateCommissionStructureRule, "@ACSR_Comment", DbType.String, commissionStructureRuleVo.StructureRuleComment);

                db.AddInParameter(cmdUpdateCommissionStructureRule, "@UsetId", DbType.Int32, userId);

                db.ExecuteNonQuery(cmdUpdateCommissionStructureRule);

            }

            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:UpdateCommissionStructureRule(CommissionStructureRuleVo commissionStructureRuleVo, int userId)");
                object[] objects = new object[2];
                objects[0] = commissionStructureRuleVo.CommissionStructureId;
                objects[1] = userId;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }


        }

        public void DeleteCommissionStructureRule(int id, bool isAllRuleDelete)
        {
            Database db;
            DbCommand cmdDeleteStructureCommissionRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdDeleteStructureCommissionRules = db.GetStoredProcCommand("SPROC_DeleteCommissionStructureRules");
                if (isAllRuleDelete)
                    db.AddInParameter(cmdDeleteStructureCommissionRules, "@ACSM_CommissionStructureId", DbType.Int32, id);
                else
                    db.AddInParameter(cmdDeleteStructureCommissionRules, "@ACSR_CommissionStructureRuleId", DbType.String, id);
                ds = db.ExecuteDataSet(cmdDeleteStructureCommissionRules);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommisionReceivableDao.cs:DeleteCommissionStructureRule(int id, bool isAllRuleDelete)");
                object[] objects = new object[2];
                objects[0] = id;
                FunctionInfo = exBase.AddObject(FunctionInfo, objects);
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }

        }


        public DataSet GetCMStructNames(int adviserId, int cmStructId)
        {
            Database db;
            DbCommand cmdGetStructNames;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructNames = db.GetStoredProcCommand("SP_GetAllStrutureNamesById");
                db.AddInParameter(cmdGetStructNames, "@AdviserId", DbType.Int32, adviserId);
                if (cmStructId > 1)
                {
                    db.AddInParameter(cmdGetStructNames, "@StructId", DbType.Int32, cmStructId);
                }
                ds = db.ExecuteDataSet(cmdGetStructNames);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetAllStructureDetails(int cmStructId)");
                object[] objects = new object[1];
                objects[0] = cmStructId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }

        //GetMappedSchemes

        public DataSet GetMappedSchemes(int cmStructId)
        {
            Database db;
            DbCommand cmdGetStructNames;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetStructNames = db.GetStoredProcCommand("SP_GetMappedSchemes");
                if (cmStructId > 1)
                {
                    db.AddInParameter(cmdGetStructNames, "@StructId", DbType.Int32, cmStructId);
                }
                ds = db.ExecuteDataSet(cmdGetStructNames);
            }
            catch (BaseApplicationException Ex)
            {
                throw Ex;
            }
            catch (Exception Ex)
            {
                BaseApplicationException exBase = new BaseApplicationException(Ex.Message, Ex);
                NameValueCollection FunctionInfo = new NameValueCollection();
                FunctionInfo.Add("Method", "CommissionManagementDao.cs:GetAllStructureDetails(int cmStructId)");
                object[] objects = new object[1];
                objects[0] = cmStructId;
                exBase.AdditionalInformation = FunctionInfo;
                ExceptionManager.Publish(exBase);
                throw exBase;
            }
            return ds;
        }


    }
}
