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

        public void CreateCommissionStructureMastter(CommissionStructureMasterVo commissionStructureMasterVo, int userId, out int instructureId)
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
                    instructureId = (int)db.GetParameterValue(cmdCreateCommissionStructure, "@CommissionStructureId");
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


        public DataSet GetAdviserCommissionStructureRules(int adviserId)
        {
            Database db;
            DbCommand cmdGetCommissionStructureRules;
            DataSet ds = null;

            try
            {
                db = DatabaseFactory.CreateDatabase("wealtherp");
                cmdGetCommissionStructureRules = db.GetStoredProcCommand("SPROC_GetAdviserCommissionStructureRules");
                db.AddInParameter(cmdGetCommissionStructureRules, "@A_AdviserId", DbType.Int32, adviserId);
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

                if (commissionStructureRuleVo.MaxInvestmentAmount != 0)
                {
                    db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinInvestmentAmount", DbType.Decimal, commissionStructureRuleVo.MinInvestmentAmount);
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
                if (commissionStructureRuleVo.MinNumberofApplications!=0)
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_MinNumberOfApplications", DbType.Int32, commissionStructureRuleVo.MinNumberofApplications);

               
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_BrokerageValue", DbType.Decimal, commissionStructureRuleVo.BrokerageValue);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@WCU_UnitCode", DbType.String, commissionStructureRuleVo.BrokerageUnitCode);


                db.AddInParameter(cmdCreateCommissionStructureRule, "@WCCO_CalculatedOnCode", DbType.String, commissionStructureRuleVo.CalculatedOnCode);
                if(commissionStructureRuleVo.AUMMonth!=0)
                {
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSM_AUMFrequency", DbType.String, commissionStructureRuleVo.AUMFrequency);
                db.AddInParameter(cmdCreateCommissionStructureRule, "@ACSR_AUMMonth", DbType.Int16, commissionStructureRuleVo.AUMMonth);
                }

                db.AddInParameter(cmdCreateCommissionStructureRule, "@UsetId", DbType.Int32,userId);

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

        public DataSet GetStructureCommissionAllRules(int structureId,string commissionType)
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

    }
}
